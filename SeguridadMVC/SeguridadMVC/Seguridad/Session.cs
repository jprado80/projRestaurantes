using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeguridadMVC.Seguridad
{
    public class Session
    {
        public Int32 CodigoSession { get; set; }

        public String IpCliente { get; set; }


        public DateTime? FechaSession { get; set; }

    }
}