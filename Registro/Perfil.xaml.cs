using Newtonsoft.Json;
using Registro.modelos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Registro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
            MostrarDato();
        }
        private async void MostrarDato()

        {

            var token = Application.Current.Properties["token"] as string;
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://tuidea.herokuapp.com/api/auth/me");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            var client = new HttpClient();
            var authHeader = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Authorization = authHeader;
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();


                var resultado = JsonConvert.DeserializeObject<LeerUsuarios>(content);
                               
                List<LeerUsuarios> datosU = new List<LeerUsuarios>();
                datosU.Add(resultado);
                NombreCompleto.ItemsSource = datosU;
                Accesorios.ItemsSource = datosU;

            }        
     
       }

       
    }
}