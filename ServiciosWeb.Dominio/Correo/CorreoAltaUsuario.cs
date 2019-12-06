using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio.Correo
{
    public class CorreoAltaUsuario
    {


        public long CodigoUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string Asunto { get; set; }

        public string Mensaje { get; set; }


    }
}

