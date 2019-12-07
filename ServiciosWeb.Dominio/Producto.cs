using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
   public class Producto
    {

        public string prod_nombre { get; set; }
        public long prod_id { get; set; }

        public string prod_descrip { get; set; }

        public decimal? prod_precio { get; set; }
        public int tico_id { get; set; }
        public string tico_descrip { get; set; }
        public string rest_ruc { get; set; }


    }
}
