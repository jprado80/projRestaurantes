using SeguridadMVC.Seguridad;
using ServiciosWeb.Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ServiciosWeb.ClienteWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Restaurante", new { });
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl = "")
        {
            var modelo = new Login();
            if (User.Identity.IsAuthenticated)
            {
                return SignOut();
            }
            modelo.ReturnUrl = returnUrl;
            return View(modelo);
        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(Login login, string returnUrl = "")
        {
            var usuariovalido = false;
            var msj = string.Empty;

            if (!ModelState.IsValid)
            {
                return View();
            }

            usuariovalido = Membership.ValidateUser(login.correo, login.password);

            if (Session["mensaje_validateuser"] != null) msj = Convert.ToString(Session["mensaje_validateuser"]);

            if (string.IsNullOrEmpty(msj) == false) ViewBag.Mensaje = msj;

            if (usuariovalido)
            {
                var usuario = (CustomMembershipUser)Membership.GetUser(login.correo);
                if (usuario != null && Session["mensaje_validateuser"] == null)
                {



                    FormsAuthentication.RedirectFromLoginPage(usuario.NombreUsuario, false);

                }
                else
                {
                    ModelState.AddModelError("", Convert.ToString(Session["mensaje_validateuser"]));
                }
            }

            return View(login);
        }
    }
}