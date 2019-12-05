using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeguridadMVC.Seguridad
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            filters.Add(new CustomAuthorizeAttribute(), 2);
            filters.Add(new SessionExpiredAttribute(), 3);
    

    

        }
    }
}