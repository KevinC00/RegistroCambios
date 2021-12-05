using Newtonsoft.Json;
using Registro.modelos;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Registro.servicios
{
    class AuthService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string url = "https://tuidea.herokuapp.com/api/auth/";
        //private const string url = "http://192.168.1.95:8080/api/auth/";

        public async Task<HttpResponseMessage> ValidarToken(string token)
        {
            var jwt = new Jwt() { token = token, nombreUsuario = "empty" };
            var data = new StringContent(JsonConvert.SerializeObject(jwt), Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(url + "validate", data);
        }

        public async Task<HttpResponseMessage> Ingresar(IngresarUsuario ingresarUsuario)
        {
            var data = new StringContent(JsonConvert.SerializeObject(ingresarUsuario), Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(url + "ingresar", data);
           
        }

        public async Task<HttpResponseMessage> Registrar(RegistroUsuario registroUsuario)
        {
            var data = new StringContent(JsonConvert.SerializeObject(registroUsuario), Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(url + "registrar", data);
            
        }
        public async Task<HttpResponseMessage> Salir()
        {
            var data = new StringContent("",Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(url + "salir",data);

        }

       





    }
}
