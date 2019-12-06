
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;

using System.Web;


using System;
using System.Net;

using System.Collections.Generic;

using System.Globalization;
using System.Threading;
using SeguridadMVC.Seguridad;

namespace SeguridadMVC
{
 public   class CommonHttpApplication : HttpApplication
    {
        protected virtual void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null) return;
            if (!this.Request.IsAuthenticated) return;
            var identity = new CustomIdentity(HttpContext.Current.User.Identity);
            var principal = new CustomPrincipal(identity);
            HttpContext.Current.User = principal;

        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {
            //Exception exception = this.Server.GetLastError().GetBaseException();
            //HttpException httpException = exception as HttpException;

            //int error = httpException != null ? httpException.GetHttpCode() : 0;

            //if (httpException == null)
            //{
            //    //Log.Error(exception.Message, exception);
            //    Server.ClearError();
            //    Response.Redirect(String.Format("~/Error/?error={0}", error, exception.Message));
            //}
            //else
            //{
            //    int statusCode = httpException.GetHttpCode();

            //    if (statusCode != (int)HttpStatusCode.NotFound && statusCode != (int)HttpStatusCode.ServiceUnavailable)
            //    {
            //        // Log.Error(exception.Message, httpException);
            //        Server.ClearError();
            //        Response.Redirect(String.Format("~/Error/?error={0}", error, exception.Message));
            //    }
            //    else {

            //        Server.ClearError();
            //        Response.Redirect(String.Format("~/Error/?error={0}", error, exception.Message));
            //    }
            //}
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            //Se Habilita para evitar error en las URLs q no contienen el "/" al final
            if (String.Compare(Request.Path, Request.ApplicationPath, StringComparison.InvariantCultureIgnoreCase) == 0
                && !(Request.Path.EndsWith("/")))
                Response.Redirect(Request.Path + "/");

            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {

            var context = (sender as HttpApplication).Context;

            if (context.Response.StatusCode == 401)
            {
                var noRedirect = context.Items["NoRedirect"];

                if (noRedirect == null)
                {
                    var route = new RouteValueDictionary(new Dictionary<string, object>
                        {
                            { "Controller", "Home" },
                            { "Action", "SignIn" },
                            { "ReturnUrl", HttpUtility.UrlEncode(context.Request.RawUrl, context.Request.ContentEncoding) }
                        });

                    Response.RedirectToRoute(route);
                }
            }
        }

        protected virtual void Application_End()
        {

            HttpRuntime runtime = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime",
                BindingFlags.NonPublic
                | BindingFlags.Static
                | BindingFlags.GetField,
                null,
                null,
                null);

            if (runtime == null)
                return;

            string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage",
                BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.GetField,
                null,
                runtime,
                null);

            string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack",
                BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.GetField,
                null,
                runtime,
                null);


        }
    }
}
