using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiciosWeb.Dominio.Response
{
    public class AutenticacionResponse : Respuesta
    {
        
        public long CodigoUsuario { get; set; }
        public string CorreoElectronico { get; set; }

    }
}
