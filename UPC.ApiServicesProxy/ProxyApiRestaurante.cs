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
                var responseTask = client.GetAsync("api/ListaMenuRestaurante/" + request.CodigoUsuario);
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
    }
}
