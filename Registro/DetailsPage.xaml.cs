using Registro.modelos;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Registro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
       

        private static readonly HttpClient httpClient = new HttpClient();
        private const string url = "https://tuidea.herokuapp.com/api/usuario/";
        public DetailsPage(int id,string nameUser, string name , string apellidoMaterno,string apellidoPaterno,string correo)
        {
            InitializeComponent();
            idEntry.Text = id.ToString();
            NameUserEntry.Text = nameUser;
            NameEntry.Text = name;
            ApellidoPaEntry.Text = apellidoPaterno;
            ApellidoMaEntry.Text = apellidoMaterno;
            EmailEntry.Text = correo;
        }

        public async Task DeleteUserAsync(int id)

        {
            var token = Application.Current.Properties["token"] as string;
            var authHeader = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Authorization = authHeader;

               Uri uri = new Uri("https://tuidea.herokuapp.com/api/usuario/eliminar");

            HttpResponseMessage response = await httpClient.DeleteAsync(uri+"/"+id);
            if (response.IsSuccessStatusCode)
            {
               await DisplayAlert("Eliminado","Se elimno :D","Ok");
                
              
            }
           
        }

       private async  void Button_Clicked(object sender, EventArgs e)
        {
            var userIdDelete = idEntry.Text;
           
            await DeleteUserAsync(Int32.Parse( userIdDelete));
        }
    }
}