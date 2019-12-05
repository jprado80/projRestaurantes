using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio.Modelo
{
    public class CambiarContrasenModel
    {

        
        public long CodigoUsuario { get; set; }

        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres")]
        [DisplayName("Ingrese Contraseña Actual (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string ContrasenaActual { get; set; }


        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres")]
        [DisplayName("Ingrese Contraseña Nueva (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string ContrasenaNueva { get; set; }

        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres")]
        [DisplayName("Reingrese Nueva Contraseña (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string ContrasenaRepetir { get; set; }


    }
}
