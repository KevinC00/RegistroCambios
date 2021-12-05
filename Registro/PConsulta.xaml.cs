using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Registro.modelos;
using System.Net.Http.Headers;
using Registro.servicios;
namespace Registro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PConsulta : ContentPage
    {
        public PConsulta()
        {
            MostrarDatos();
            InitializeComponent();
        }
      

       private async void MostrarDatos()
        
        {

            var token = Application.Current.Properties["token"] as string;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://tuidea.herokuapp.com/api/usuario/leer");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            var authHeader = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Authorization = authHeader;
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<LeerUsuarios>>(content);
                ListaUsuarios.ItemsSource = resultado;
                
            }
        }
        
        private void ListaUsuarios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detailsUser = e.Item as LeerUsuarios;
            Navigation.PushAsync(new DetailsPage(detailsUser.id, detailsUser.nombreUsuario, detailsUser.nombre, detailsUser.apellidoPaterno, detailsUser.apellidoMaterno, detailsUser.correo));
            
          
        }
    }
}