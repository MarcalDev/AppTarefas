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

            bool certo = await new TarefaDB().CadastrarAsync(tarefa);

            if (certo)
            {                
                // MessagingCenter Retornar a Tarefa para a tela de Listagem.
                MessagingCenter.Send<Listar, Tarefa>(new Listar(), "OnTarefaCadastrada", tarefa);
                // Fechar o modal
                Navigation.PopModalAsync();                
            }                                        

        }
        //private async Task<bool> ValidacaoAsync(Tarefa tarefa)
        //{
        //    bool validacao = true;
        //}
    }
}