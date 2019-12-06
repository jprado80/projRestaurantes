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
    public class ProxyApiProducto
    {

        


   
        public ListaPrecioResponse ListarPrecio(ListarPrecioRequest request)
        {

            ListaPrecioResponse response = new ListaPrecioResponse();

         

     
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask = client.PostAsync("api/restaurante/producto", httpContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var usuarioResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ListaPrecioResponse>(usuarioResponse);
                }
            }

            return response;

        }



    }
}
