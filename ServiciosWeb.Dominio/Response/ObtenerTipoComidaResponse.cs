using ServiciosWeb.Dominio;
using ServiciosWeb.Dominio.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiciosWeb.DominioResponse
{
    public class ObtenerTipoComidaResponse : Respuesta
    {
        public List<TipoComida> TipoComida { get; set; }
       

    }
}
