using Registro.pantallas.auth;
using Registro.pantallas.layout;
using Registro.servicios;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Registro
{
    public partial class App : Application
    {
        private AuthService authService = new AuthService();

        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new LoginPage());
           
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
