using System.Web;
using System.Web.Mvc;

namespace ServiciosWeb.ClienteWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
