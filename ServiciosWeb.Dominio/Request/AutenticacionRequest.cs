using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiciosWeb.Dominio.Request
{
    public class AutenticacionRequest : Request
    {
        
        public string Clave { get; set; }
        public string CorreoElectronico { get; set; }

    }
}
