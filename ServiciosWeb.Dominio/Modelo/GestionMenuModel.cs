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
   public  class GestionMenuModel
    {




        [DisplayName("Nro")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string NroMenu { get; set; }

        [DisplayName("Descripcion (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string DescripcionMenu { get; set; }

        [DisplayName("Descripcion (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public int CodigoProducto { get; set; }
        public List<SelectListItemCustom> ListProducto { get; set; }

    }
}