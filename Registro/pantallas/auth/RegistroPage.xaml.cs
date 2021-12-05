using Newtonsoft.Json;
using Registro.modelos;
using Registro.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Registro.pantallas.auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : ContentPage
    {
        private GeneroService generoService = new GeneroService();
        private RolService rolService = new RolService();
        private AuthService authService = new AuthService();

        public RegistroPage()
        {
            InitializeComponent();
            this.pckSexo.ItemsSource = generoService.ObtenerGeneros();
            this.pckRol.ItemsSource = rolService.ObtenerRoles();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var registrarUsuario = new RegistroUsuario()
            {
                nombre = this.txtNombre.Text.ToUpper(),
                nombreUsuario = this.txtNombreUsuario.Text.ToUpper(),
                apellidoPaterno = this.txtApellidoPaterno.Text.ToUpper(),
                apellidoMaterno = this.txtApellidoMaterno.Text.ToUpper(),
                fechaNacimiento = this.dteFechaNacimiento.Date,
                sexo = this.pckSexo.SelectedItem.ToString(),
                telefono = this.txtTelefono.Text,
                correo = this.txtCorreo.Text.ToUpper(),
                contrasena = this.txtContrasena.Text.ToUpper(),
                rol = this.pckRol.SelectedItem.ToString()
            };

            if (Validar(registrarUsuario))
            {
                var response = await authService.Registrar(registrarUsuario);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var respuesta = JsonConvert.DeserializeObject<Respuesta>(content);

                    if (respuesta.mensaje != null)
                    {
                        await DisplayAlert("Mensaje", respuesta.mensaje, "Ok");
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

        private bool Validar(RegistroUsuario registroUsuario)
        {
            bool flag = true;

            flag = flag && registroUsuario.nombre != null;
            flag = flag && registroUsuario.nombreUsuario != null;
            flag = flag && registroUsuario.apellidoPaterno != null;
            flag = flag && registroUsuario.apellidoMaterno != null;
            flag = flag && registroUsuario.fechaNacimiento != null;
            flag = flag && registroUsuario.sexo != null;
            flag = flag && registroUsuario.telefono != null;
            flag = flag && registroUsuario.correo != null;
            flag = flag && registroUsuario.contrasena != null;
            flag = flag && registroUsuario.rol != null;

            return flag;
        }
    }
}