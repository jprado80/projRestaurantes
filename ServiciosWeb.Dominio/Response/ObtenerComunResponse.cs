using ServiciosWeb.Dominio;
using ServiciosWeb.Dominio.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiciosWeb.DominioResponse
{
    public class ObtenerComunResponse : Respuesta
    {
        public List<TipoCuenta> TipoCuentas { get; set; }
        public List<EspecialidadTipo> EspecialidadesTipo { get; set; }

        public List<Distrito> Distritos { get; set; }


    }
}
