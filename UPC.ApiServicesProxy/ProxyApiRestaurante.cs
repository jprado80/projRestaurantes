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
    public class ProxyApiRestaurante
    {

        public ListaMenuResponse ListarMenu(ListarMenuRequest request)
        {

            ListaMenuResponse response = new ListaMenuResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                 var responseTask = client.GetAsync("api/Menu/" + request.CodigoUsuario);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ListaMenuResponse>(colaboradorResponse);
                }
            }

            return response;

        }


        public ListaMenuDetalleResponse ListarMenuDetalle(ListarMenuDetalleRequest request)
        {

            ListaMenuDetalleResponse response = new ListaMenuDetalleResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                var responseTask = client.GetAsync("api/MenuDetalle/" + request.CodigoMenu);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ListaMenuDetalleResponse>(colaboradorResponse);
                }
            }

            return response;

        }


        public MenuRegistrarResponse RegistrarMenu(MenuRegistrarRequest request)
        {
            MenuRegistrarResponse response = new MenuRegistrarResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PostAsync("api/Menu/", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var usuarioResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<MenuRegistrarResponse>(usuarioResponse);
                }
            }

            return response;

        }


        public MenuDetalleRegistrarResponse RegistrarMenuDetalle(MenuDetalleRegistrarRequest request)
        {
            MenuDetalleRegistrarResponse response = new MenuDetalleRegistrarResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PostAsync("api/MenuDetalle/", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var usuarioResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<MenuDetalleRegistrarResponse>(usuarioResponse);
                }
            }

            return response;

        }


        public void EliminarMenu(int codigo)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                var responseTask = client.DeleteAsync("api/Menu/" + codigo);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;
                    
                }
            }

            

        }


        public void EliminarMenuDetalle(int codigo)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                var responseTask = client.DeleteAsync("api/MenuDetalle/" + codigo);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;

                }
            }



        }


        public void MenuPublicar(int codigo, bool select)
        {

            MenuRegistrarRequest request = new MenuRegistrarRequest();
            request.Menu = new ServiciosWeb.Dominio.Menu();
            request.Menu.menu_id =codigo;
            request.Menu.menu_publicado = select;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PostAsync("api/MenuPublicar/", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var usuarioResponse = result.Content.ReadAsStringAsync().Result;
                    
                }
            }

            



        }



    }
}
