using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SeguridadMVC.Seguridad
{

    public class SessionExpiredAttribute : ActionFilterAttribute
    {
       

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ctx = HttpContext.Current;

            if (ctx.Session != null)
            {

                if (ctx.Session.IsNewSession )
                {
                    var sessionCookie = ctx.Request.Headers["Cookie"];
                    if ((null != sessionCookie) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0) && ctx.Session["CreateSession"] == null)
                    {
                        UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
                        string redirectOnSuccess = filterContext.HttpContext.Request.Url.PathAndQuery;
                        if (redirectOnSuccess.Contains("Home"))
                        {
                            redirectOnSuccess = url.Action("Index", "Home", new { area = "" });  //redirectOnSuccess = "%2f";

                            string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);

                            string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;

                            if (ctx.Request.IsAuthenticated)
                            {
                                FormsAuthentication.SignOut();
                            }

                            ctx.Session["CreateSession"] = DateTime.Now;
                            RedirectResult rr = new RedirectResult(loginUrl);
                            filterContext.Result = rr;
                        }
                        else
                        {


                        }
                            



                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}