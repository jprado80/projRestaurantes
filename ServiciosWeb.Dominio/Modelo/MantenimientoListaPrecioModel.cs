using ServiciosWeb.Dominio.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio.Modelo
{
    public class MantenimientoListaPrecioModel
    {
        

        public long CodigoUsuario { get; set; }

        public int CodigoTipoComida { get; set; }
        public string RestRuc { get; set; }
        public List<SelectListItemCustom> ListComida { get; set; }

        public string DescripcionProducto { get; set; }

        public string PrecioProducto { get; set; }



    }
}
