using ServiciosWeb.Dominio.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServiciosWeb.Dominio
{
    public class UsuarioRestaurante
    {
        public Usuario Usuario { get; set; }
        public Restaurante Restaurante { get; set; }
        public int CodigoContacto { get; set; }
        public string Telefonos { get; set; }
        public List<SelectListItemCustom> ListTipoTelefono { get; set; }
    }
}
