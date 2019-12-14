
using ServiciosWeb.Dominio.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio.Modelo
{
   public  class MantenimientoMenuDetalleModel
    {


        
        [Required(ErrorMessage = "Campo obligatorio")]
        public long CodigoMenu { get; set; }


        [DisplayName("Descripcion (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public int CodigoProducto { get; set; }

        public List<SelectListItemCustom> ListProducto { get; set; }

    }
}