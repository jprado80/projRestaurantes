using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using ServiciosWeb.Dominio.Modelo;
using ServiciosWeb.Dominio.Response;
using ServiciosWeb.DominioResponse;
using ServiciosWeb.Dominio.Request;

namespace UPC.ApiServicesProxy
{
    public class ProxyApiUsuario
    {

        


        public CambiarClaveUsuarioResponse CambiarClave(CambiarClaveUsuarioRequest request)
        {

            CambiarClaveUsuarioResponse response = new CambiarClaveUsuarioResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PostAsync("api/usuario/cambiar_clave/", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var usuarioResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<CambiarClaveUsuarioResponse>(usuarioResponse);
                }
            }

            return response;

        }

        public ValidarClaveUsuarioResponse ValidarClave(long CodigoUsuario, string password)
        {
            ValidarClaveUsuarioRequest request = new ValidarClaveUsuarioRequest()
            {
                Clave = password,
                CodigoUsuario = CodigoUsuario
            };

            ValidarClaveUsuarioResponse response = new ValidarClaveUsuarioResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PostAsync("api/usuario/validar_clave/", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var usuarioResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ValidarClaveUsuarioResponse>(usuarioResponse);
                }
            }

            return response;

        }

        public Respuesta validarCredenciales(string user, string password)
        {
            AutenticacionRequest request = new AutenticacionRequest() {
                Clave =  password ,
                CorreoElectronico=user
            };
            
            Respuesta response = new Respuesta();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request ), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PostAsync("api/Autenticar", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var usuarioResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Respuesta>(usuarioResponse);
                }
            }

            return response;

        }



        public RegistrarUsuarioRestauranteResponse RegistrarUsuarioRestaurante(RegistrarUsuarioRestauranteRequest request)
        {
            RegistrarUsuarioRestauranteResponse response = new RegistrarUsuarioRestauranteResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PostAsync("api/usuario", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var usuarioResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<RegistrarUsuarioRestauranteResponse>(usuarioResponse);
                }
            }

            return response;

        }



        public RegistrarUsuarioRestauranteResponse ActualizarUsuarioRestaurante(RegistrarUsuarioRestauranteRequest request)
        {
            RegistrarUsuarioRestauranteResponse response = new RegistrarUsuarioRestauranteResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PutAsync("api/usuario", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var usuarioResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<RegistrarUsuarioRestauranteResponse>(usuarioResponse);
                }
            }

            return response;

        }


        public ObtenerUsuarioResponse ObtenerUsuario(long Codigo)
        {

            ObtenerUsuarioResponse response = new ObtenerUsuarioResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                var responseTask = client.GetAsync("/api/usuario/" + Codigo);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ObtenerUsuarioResponse>(colaboradorResponse);
                }
            }

            return response;

        }


        public ActivarCuentaResponse ActivarCuenta(long Codigo)
        {

            ActivarCuentaResponse response = new ActivarCuentaResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                var responseTask = client.GetAsync("/api/usuario/activarcuenta?id=" + Codigo);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ActivarCuentaResponse>(colaboradorResponse);
                }
            }

            return response;

        }

        
        public ObtenerUsuarioResponse ObtenerUsuarioPorCorreo(string Correo)
        {

            ObtenerUsuarioResponse response = new ObtenerUsuarioResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                var responseTask = client.GetAsync("/api/usuario/search_correo/?correo=" + Correo);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ObtenerUsuarioResponse>(colaboradorResponse);
                }
            }

            return response;

        }

    }
}
