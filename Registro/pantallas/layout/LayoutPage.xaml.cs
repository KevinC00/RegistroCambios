using Newtonsoft.Json;
using Registro.modelos;
using Registro.pantallas.auth;
using Registro.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Registro.pantallas.layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class LayoutPage : MasterDetailPage 
    {
      
        private AuthService authService = new AuthService();
     private LeerUsuarios leerUsuarios = new LeerUsuarios();
        public LayoutPage()
        {
           
            InitializeComponent();
          
            this.lsvMenu.ItemsSource =  CargarMenu();
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Inicio)));
            this.lsvMenu.ItemSelected += Lsvmenu_ItemSelected;
           

        }


        private List<MenuPagina> CargarMenu()
        {
              MostrarRol();

            string rolito = leerUsuarios.rol;

            var menu = new List<MenuPagina>();
            
            if( rolito == "ADMIN")
            {
                menu.Add(new MenuPagina() { Titulo = "Usuarios", Pagina = typeof(PConsulta) });
                menu.Add(new MenuPagina() { Titulo = "Perfil", Pagina = typeof(Perfil) });
                menu.Add(new MenuPagina() { Titulo = "Cerrar Sesion", Pagina = null });
                return menu;

          
            }
            else 
            {

              //  var menu = new List<MenuPagina>();
                menu.Add(new MenuPagina() { Titulo = "Perfil", Pagina = typeof(Perfil) });
                menu.Add(new MenuPagina() { Titulo = "Cerrar Sesion", Pagina = null });
                return menu;


           }
        }

        private  async void Lsvmenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MenuPagina pagina = e.SelectedItem as MenuPagina;
            if (pagina.Pagina != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(pagina.Pagina));

                IsPresented = false;
            }
       

           else if (pagina.Pagina == null || pagina.Titulo == "Cerrar Sesion")
            {
                var result = await  DisplayAlert("Confirmar", "Estas seguro de cerrar sesión", "SI", "NO");

                await authService.Salir();
                if (result)
                {
                    

                    Application.Current.MainPage = new NavigationPage( new LoginPage());
                   
                }
                else 
                {
                    return;
                }

           } 
            

        }


        public async void MostrarRol()

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

                this.leerUsuarios = resultado;



            }

        }



    }
}