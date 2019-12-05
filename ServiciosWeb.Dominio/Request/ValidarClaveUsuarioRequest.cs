using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio.Request
{
   public class ValidarClaveUsuarioRequest
    {

      public  long CodigoUsuario { get; set; }

        public string Clave { get; set; }


    }
}
