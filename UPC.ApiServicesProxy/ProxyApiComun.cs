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
    public class ProxyApiComun
    {
      
       
        public ObtenerTipoTelefonoResponse ObtenerTipoTelefonos()
        {

            ObtenerTipoTelefonoResponse response = new ObtenerTipoTelefonoResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                var responseTask = client.GetAsync("/api/tipotelefono/" );
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ObtenerTipoTelefonoResponse>(colaboradorResponse);
                }
            }

            return response;

        }

        public ObtenerTelefonoUsuarioResponse ObtenerTelefonosUsuario(long CodigoUsuario)
        {

            ObtenerTelefonoUsuarioResponse response = new ObtenerTelefonoUsuarioResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                var responseTask = client.GetAsync("/api/telefono/usuario?id="+ CodigoUsuario);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ObtenerTelefonoUsuarioResponse>(colaboradorResponse);
                }
            }

            return response;

        }



        
        public ObtenerTipoComidaResponse ObtenerTipoComida()
        {
            ObtenerTipoComidaResponse response = new ObtenerTipoComidaResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                var responseTask = client.GetAsync("/api/comun/tipocomida/");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ObtenerTipoComidaResponse>(colaboradorResponse);
                }
            }

            return response;
        }

            public ObtenerComunResponse ObtenerComun()
        {

            ObtenerComunResponse response = new ObtenerComunResponse();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServicioCommon.Parametros.URLServicio);
                var responseTask = client.GetAsync("/api/comun/");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var colaboradorResponse = result.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<ObtenerComunResponse>(colaboradorResponse);
                }
            }

            return response;

        }


    }
}
