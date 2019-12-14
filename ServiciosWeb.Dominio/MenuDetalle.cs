using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
    public class MenuDetalle
    {
        public long mede_id  { get;set;}
        public long menu_id { get; set; }
        public string pro_descripcion { get; set; }
        public long prod_id { get; set; }
        public decimal? mede_precio { get; set; }
        public bool? mede_disponible { get; set; }
    }
}
