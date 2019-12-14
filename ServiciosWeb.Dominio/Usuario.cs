using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
    public class Usuario
    {
        public long usua_id { get; set; }

        [StringLength(70, ErrorMessage = "No puede tener más de 70 caracteres")]
        [EmailAddress(ErrorMessage = "Correo invalido")]
        [DisplayName("Email Usuario (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string usua_email { get; set; }

        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres")]
        [DisplayName("Contrasena (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string usua_pass { get; set; }


        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres")]
        [DisplayName("Repetir Contrasena (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string usua_pass_repetir  { get; set; }


        [StringLength(70, ErrorMessage = "No puede tener más de 70 caracteres")]
        [DisplayName("Nombre Completo (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string usua_nomb { get; set; }


        [StringLength(8, ErrorMessage = "No Puede tener Mmás de 8 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [RegularExpression(@"^(\d{8})*$", ErrorMessage = "Solo se permite formato DNI de 8 digitos")]
        [DisplayName("DNI (*)")]
        public string usua_dni { get; set; }



        public int dist_id { get; set; }
        public string dist_nombre { get; set; }

        public string usua_direc { get; set; }
        public string usua_refedirec { get; set; }

        public string usua_rutaiamgen { get; set; }


    }
}
