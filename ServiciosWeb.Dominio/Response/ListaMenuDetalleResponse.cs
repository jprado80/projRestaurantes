using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio.Response
{
   public class ListaMenuDetalleResponse : Respuesta
    
    {
        public List<MenuDetalle> Hits { get; set; }
        
        public Int32 totalregistros { get; set; }

    }
}
