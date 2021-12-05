using Newtonsoft.Json;
using Registro.modelos;
using Registro.pantallas.layout;
using Registro.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Registro.pantallas.auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private AuthService authService = new AuthService();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistroPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            var ingresarUsuario = new IngresarUsuario() { nombreUsuario = this.nombreUsuario.Text, contrasena = this.contrasena.Text};

            if (Validar(ingresarUsuario))
            {
                var response = await authService.Ingresar(ingresarUsuario);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var jwt = JsonConvert.DeserializeObject<Jwt>(content);

                    if (jwt.token != null)
                    {
                        Application.Current.Properties["token"] = jwt.token;
                        Application.Current.Properties["usuario"] = jwt.nombreUsuario;

                        Application.Current.MainPage = new LayoutPage();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Intentalo más tarde", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Algo salio mal", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Debes llenar todos los campos", "Ok");
            }
        }

        private bool Validar(IngresarUsuario ingresarUsuario)
        {
            bool flag = true;

            flag = flag && ingresarUsuario.nombreUsuario != null;
            flag = flag && ingresarUsuario.contrasena != null;

            return flag;
        }
    }
}