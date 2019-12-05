using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServiciosWeb.Dominio.Control
{

    public class SelectListItemCustom : SelectListItem
    {
        public IDictionary<string, string> itemsHtmlAttributes { get; set; }
    }

}
