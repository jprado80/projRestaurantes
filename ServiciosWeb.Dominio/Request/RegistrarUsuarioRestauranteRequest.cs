using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiciosWeb.Dominio.Request
{
    public class RegistrarUsuarioRestauranteRequest : Request
    {
        
        public Usuario Usuario { get; set; }
        public Restaurante Restaurante { get; set; }
        public List<Telefono> Telefonos { get; set; }

    }
}
