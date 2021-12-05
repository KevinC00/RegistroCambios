using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Registro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {

            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PEditar());
        }

        private void Eliminar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PEliminar());
        }

        private void Consultar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PConsulta());
        }
        
    }
}