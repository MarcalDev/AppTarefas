﻿using System;
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

        private void SalvarTarefa(object sender, EventArgs e)
        {
            // Pegar os dados da Tela e Criar uma Tarefa
            // Salvar a Tarefa no Banco

            
        }
    }
}