using ServiciosWeb.Dominio.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using System.IO;
using System.Web;

namespace ServiciosWeb.Dominio
{
    public class UsuarioRestauranteModel
    {

        public long usua_id { get; set; }

        public string usua_email { get; set; }


        [StringLength(100, ErrorMessage = "No puede tener más de 100 caracteres")]
        [DisplayName("Nombre Comercial (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string rest_nomcomer { get; set; }


        [DisplayName("Distrito (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public int dist_id { get; set; }

        [StringLength(100, ErrorMessage = "No Puede tener Mmás de 100 caracteres")]
        [DisplayName("Referencia ")]
        public string usua_refedirec { get; set; }


        [StringLength(100, ErrorMessage = "No Puede tener Mmás de 100 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [DisplayName("Direccion (*)")]
        public string usua_direc { get; set; }


        public System.DateTime? usua_fecnac { get; set; }
        public string usua_rutaimagen { get; set; }
        public bool? usua_esta { get; set; }


        [DisplayName("RUC")]
        public string rest_ruc { get; set; }

        
        [DisplayName("Rezon Social")]
        public string rest_rz { get; set; }

    

        [DisplayName("Especialiadad / Tipo (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public int esti_id { get; set; }



        [StringLength(300, ErrorMessage = "No puede tener más de 300 caracteres")]
        [DisplayName("Breve descripcion")]
        public string rest_descrip { get; set; }

        
        [DisplayName("Hace Delivery")]
        public bool rest_delivery { get; set; }

        [DisplayName("Hace Reserva Local")]
        public bool rest_reservalocal { get; set; }


        [StringLength(20, ErrorMessage = "No puede tener más de 20 caracteres")]
        [DisplayName("Nro de Cuenta")]
        public string uscta_numero { get; set; }


        [DisplayName("Tipo")]
        public int ticta_id { get; set; }


        [StringLength(70, ErrorMessage = "No puede tener más de 70 caracteres")]
        [DisplayName("A Nombre De")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string uscta_titular { get; set; }

        
        public int CodigoContacto { get; set; }

        public string Telefonos { get; set; }


        public string EspecialidaDescripcion { get; set; }

        public string MensajeSucces { get; set; }


        public List<SelectListItemCustom> Distritos { get; set; }
        public List<SelectListItemCustom> EspecialidadTipo { get; set; }
        public List<SelectListItemCustom> TipoCuenta { get; set; }

        public List<SelectListItemCustom> ListTipoTelefono { get; set; }



        [DisplayName("Foto")]
   
        public HttpPostedFileBase uploadFile { get; set; }

    }
}