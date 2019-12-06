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
    public class MantenimientoListaPrecioModel
    {
        
        public long CodigoUsuario { get; set; }


        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public int CodigoTipoComida { get; set; }
        public string RestRuc { get; set; }
        public List<SelectListItemCustom> ListComida { get; set; }

        [StringLength(200, ErrorMessage = "No puede tener más de 200 caracteres")]
        [DisplayName("Descripcion")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string DescripcionProducto { get; set; }

        [RegularExpression(@"^(\d{1}\.)?(\d+\.?)+(,\d{2})?$", ErrorMessage = "El formato no es correcto")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [DisplayName("Precio")]
        public string PrecioProducto { get; set; }

    }
}
