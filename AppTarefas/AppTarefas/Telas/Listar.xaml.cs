using AppTarefas.Banco;
using AppTarefas.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTarefas.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listar : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Tarefa> _lista;
        public ObservableCollection<Tarefa> Lista
        {
            get
            {
                return _lista;
            }
            set
            {
                _lista = value;
                NotifyPropertyChanged("Lista");
            }
        }

        

        public Listar()
        {
            InitializeComponent();

            AtualizarDataCalendario(DateTime.Now);

            

            MessagingCenter.Subscribe<Listar, Tarefa>(this, "AdicionarTarefa", (pagina, tarefa) =>
            {
                Lista.Add(tarefa);
            });

            
        }

        private void BtnCadastrar(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Cadastrar());
        }

        private void BtnVisualizar(object sender, EventArgs e)
        {
            var evento = (TappedEventArgs)e;
            var tarefa = (Tarefa)evento.Parameter;




            Navigation.PushAsync(new Visualizar(tarefa));
        }

        private async void BtnExcluir(object sender, EventArgs e)
        {
            bool pergunta = await DisplayAlert("Excluir", "Tem certeza que deseja excluir esse registro?", "Sim", "Não");

            if (pergunta)
            {
                // Excluir Tarefa do Banco
                var swipeItem = (SwipeItem)sender;
                Tarefa tarefa = (Tarefa)swipeItem.CommandParameter;

                var excluido = await new TarefaDB().ExcluirAsync(tarefa.Id);

                if (excluido)
                {
                    Lista.Remove(tarefa);
                }
                
            }
        }
        private async void CheckedChangeAction(object sender, CheckedChangedEventArgs e)
        {
            // Pegar a tarefa
            var checkbox = (CheckBox)sender;
            var label = checkbox.Parent.FindByName<Label>("LblTarefaDetalhe");
            if(label != null)
            {
                var tapGesture = (TapGestureRecognizer)label.GestureRecognizers[0];

                if(tapGesture != null)
                {
                    var tarefa = (Tarefa)tapGesture.CommandParameter;

                    if(tarefa != null)
                    {
                        // Salvar a alteração da propriedade finalizada no banco
                        await new TarefaDB().AtualizarAsync(tarefa);
                        Tachado(label, tarefa.Finalizado);
                    }
                }
                
            }          
           
        }

        private void AbrirCalendario(object sender, EventArgs e)
        {
            DPCalendario.Focus();
        }

        private void DateSelectedAction(object sender, DateChangedEventArgs e)
        {
            AtualizarDataCalendario(e.NewDate);
            //e.NewDate;
        }
        private void AtualizarDataCalendario(DateTime data)
        {
            Task.Run(() => {
                Device.BeginInvokeOnMainThread(async () => {
                    Lista = new ObservableCollection<Tarefa>(
                        await new TarefaDB().PesquisarAsync(data)
                        );
                    CVListaDeTarefas.ItemsSource = Lista;
                    QuantidadeDeTarefas.Text = Lista.Count.ToString();
                });
            });

            var idioma = CultureInfo.CurrentCulture;

            Dia.Text = data.Day.ToString();
            Mes.Text = PrimeiraLetraMaiuscula(data.ToString("MMMM", idioma).Substring(0, 3));

            DiaDaSemana.Text = PrimeiraLetraMaiuscula (idioma.DateTimeFormat.GetDayName(data.DayOfWeek));
        }

        private void Tachado(Label label, bool finalizado)
        {
            if (finalizado)
            {
                label.TextDecorations = TextDecorations.Strikethrough;
            }
            else
            {
                label.TextDecorations = TextDecorations.None;
            }
        }
        private string PrimeiraLetraMaiuscula(string palavra)
        {
            return char.ToUpper(palavra[0]) + palavra.Substring(1);
        }

    }
}