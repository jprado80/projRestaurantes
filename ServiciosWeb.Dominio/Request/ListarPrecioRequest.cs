using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio.Request
{
   public class ListarPrecioRequest
    {
        public string RucRestaurante { get; set; }
        public Int32 prm_reginicio { get; set; }
        public Int32 prm_regfin { get; set; }
        public Int32 total_registros { get; set; }

    }
}