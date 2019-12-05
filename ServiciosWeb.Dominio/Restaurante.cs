using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
    public class Restaurante
    {
      

        public long usua_id { get; set; }



        [StringLength(11, ErrorMessage = "No puede tener más de 11 caracteres")]
        [DisplayName("RUC (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string rest_ruc { get; set; }



        [StringLength(100, ErrorMessage = "No puede tener más de 100 caracteres")]
        [DisplayName("Rezon Social (*)")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string rest_rz { get; set; }


        public string rest_nomcomer { get; set; }


        public int esti_id { get; set; }


        public string esti_descripcion { get; set; }

        public string rest_descrip { get; set; }
        public bool? rest_delivery { get; set; }
        public bool? rest_reservalocal { get; set; }
        public bool? rest_estado { get; set; }


        public string uscta_numero { get; set; }
        public string uscta_titular { get; set; }
        public int ticta_id { get; set; }
        public string ticta_descrip { get; set; }






    }
}
