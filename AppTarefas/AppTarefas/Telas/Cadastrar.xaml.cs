using AppTarefas.Banco;
using AppTarefas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTarefas.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastrar : ContentPage
    {
        public Cadastrar()
        {
            InitializeComponent();
            LblData.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void FecharModal(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void SalvarTarefa(object sender, EventArgs e)
        {
            // Pegar os dados da Tela e Criar uma Tarefa
            Tarefa tarefa = new Tarefa();
            tarefa.Nome = Nome.Text;
            tarefa.Nota = Nota.Text;
            tarefa.Data = Data.Date;
            tarefa.HorarioInicial = HorarioInicial.Time;
            tarefa.HorarioFinal = HorarioFinal.Time;
            tarefa.Finalizado = false;


            // Validação dos dados
            if(await ValidacaoAsync(tarefa))
            {
                bool certo = await new TarefaDB().CadastrarAsync(tarefa);

                if (certo)
                {
                    // MessagingCenter Retornar a Tarefa para a tela de Listagem.
                    MessagingCenter.Send<Listar, Tarefa>(new Listar(), "OnTarefaCadastrada", tarefa);
                    // Fechar o modal
                    await Navigation.PopModalAsync();

                }
            }

                                            

        }

        private async Task<bool> ValidacaoAsync(Tarefa tarefa)
        {

            bool validacao = true;

            if(tarefa.HorarioInicial > tarefa.HorarioFinal)
            {
                await DisplayAlert("Erro", "O horário inicial não pode ser maior que o horário final", "OK");
                validacao = false;
            }

            if (tarefa.Nome == null)
            {
                await DisplayAlert("Erro", "O nome da tarefa precisa ser preenchido!", "OK");
                validacao = false;
            }

            if(tarefa.Nome != null & tarefa.Nome.Length < 5)
            {
                await DisplayAlert("Erro", "O nome da tarefa precisa ter pelo menos 5 caracteres!", "OK");
            }

            return validacao;
        }

        private void Data_Unfocused(object sender, FocusEventArgs e)
        {
            LblData.Text = ((DatePicker)sender).Date.ToString("dd/MM/yyyy");
        }

        private void AcionarDatePicker(object sender, EventArgs e)
        {
            Data.Focus();
        }

        private void AcionarTimePicker(object sender, EventArgs e)
        {
            HorarioInicial.Focus();
        }

        private void HorarioInicial_Unfocused(object sender, FocusEventArgs e)
        {
            LblHorarioInicial.Text = ((TimePicker)sender).Time.ToString(@"hh\:mm");
            HorarioFinal.Focus();
        }

        private void HorarioFinal_Unfocused(object sender, FocusEventArgs e)
        {
            LblHorarioFinal.Text = ((TimePicker)sender).Time.ToString(@"hh\:mm");
        }
        //private async Task<bool> ValidacaoAsync(Tarefa tarefa)
        //{
        //    bool validacao = true;
        //}
    }
}