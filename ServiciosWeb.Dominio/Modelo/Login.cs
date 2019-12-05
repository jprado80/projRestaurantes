using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio.Modelo
{
    public class Login
    {

        [Required]
        public string correo { get; set; }
        [Required]
        public string password { get; set; }

        public string ReturnUrl { get; set; }

    }
}
