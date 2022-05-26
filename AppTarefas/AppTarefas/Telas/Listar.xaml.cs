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
    public partial class Listar : ContentPage
    {
        public Listar()
        {
            InitializeComponent();
        }

        private void BtnCadastrar(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Cadastrar());
        }

        private void BtnVisualizar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Telas.Visualizar());
        }
    }
}