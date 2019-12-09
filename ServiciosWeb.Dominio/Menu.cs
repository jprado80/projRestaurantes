using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
    public class Menu
    {

        public long menu_id { get;set;}
        public string menu_nombre { get; set; }
        public bool? menu_estado { get; set; }
        public bool? menu_publicado { get; set; }

        public string menu_ruc { get; set; }
        
    }
}
