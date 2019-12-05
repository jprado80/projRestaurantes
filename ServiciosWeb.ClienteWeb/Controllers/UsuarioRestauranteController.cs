using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using Newtonsoft.Json;
using ServiciosWeb.ClienteWeb.Utilitario;
using ServiciosWeb.Dominio;
using ServiciosWeb.Dominio.Control;
using ServiciosWeb.Dominio.Request;
using ServiciosWeb.Dominio.Response;
using ServiciosWeb.DominioResponse;
using UPC.ApiServicesProxy;

namespace ServiciosWeb.ClienteWeb.Controllers
{
    public class UsuarioRestauranteController : Controller
    {

        [HttpGet]
        [AllowAnonymous]
        // GET: UsuarioRestaurante
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        // GET: UsuarioRestaurante/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        // GET: UsuarioRestaurante/Create
        public ActionResult Create()
        {
            UsuarioRestaurante model = new UsuarioRestaurante();
            ProxyApiComun api = new ProxyApiComun();

            ObtenerTipoTelefonoResponse responseTipoTelefono= api.ObtenerTipoTelefonos();
            model.ListTipoTelefono = new List<SelectListItemCustom>();
            model.ListTipoTelefono.Add(new SelectListItemCustom { Text = "Seleccionar", Value = "0" , Selected=true});

            foreach (TipoTelefono item in responseTipoTelefono.TipoTelefonos) {
                model.ListTipoTelefono.Add(new SelectListItemCustom { Text = item.DescripcionTelefono, Value = item.CodigoTelefono.ToString() });
            }


            model.Telefonos = String.Empty;

            return View(model);
        }

        // POST: UsuarioRestaurante/Create
        
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(UsuarioRestaurante view, FormCollection formCollection)
        {
            UsuarioRestaurante model = view;
            ProxyApiComun api = new ProxyApiComun();
            List<Telefono> listTelefonoRegistrar = new List<Telefono>();
          
            try
            {
                model.Telefonos = JsonConvert.SerializeObject(listTelefonoRegistrar);

                ObtenerTipoTelefonoResponse responseTipoTelefono = api.ObtenerTipoTelefonos();


                model.ListTipoTelefono = new List<SelectListItemCustom>();
                model.ListTipoTelefono.Add(new SelectListItemCustom { Text = "Seleccionar", Value = "0", Selected = true });

                foreach (TipoTelefono item in responseTipoTelefono.TipoTelefonos)
                {
                    model.ListTipoTelefono.Add(new SelectListItemCustom { Text = item.DescripcionTelefono, Value = item.CodigoTelefono.ToString() });
                }


                if (formCollection[$"slCodigoContacto"] !=null && formCollection[$"txtCodigoContacto"] !=null)
                {
                    string codigoTipoTelefno = "";
                    string numeroTelefno = "";

                    codigoTipoTelefno = formCollection["slCodigoContacto"].ToString();
                    numeroTelefno = formCollection["txtCodigoContacto"].ToString();

                    listTelefonoRegistrar.Add(new Telefono()
                    {
                        CodigoTipoTelefono = codigoTipoTelefno.Trim() == string.Empty ? 0 : Convert.ToInt32(codigoTipoTelefno),
                        NumeroTelefono = numeroTelefno,
                        CodigoUsuario= 0
                    });


                }

                int contador = 1;

                for (int i = 0; i < formCollection.Count; i++)
                {


                    if (formCollection[$"slCodigoContacto{contador}"] != null
                        && formCollection[$"txtCodigoContacto{contador}"] != null)
                    {
                        string codigoTipoTelefno = "";
                        string numeroTelefno = "";
                        codigoTipoTelefno = formCollection[$"slCodigoContacto{contador}"].ToString();
                        numeroTelefno = formCollection[$"txtCodigoContacto{contador}"].ToString();


                        listTelefonoRegistrar.Add(new Telefono()
                        {
                            CodigoTipoTelefono = codigoTipoTelefno.Trim()==string.Empty?0: Convert.ToInt32(codigoTipoTelefno),
                            NumeroTelefono = numeroTelefno,
                            CodigoUsuario = 0
                        });

                    }

                    contador++;
                }

                model.Telefonos =  JsonConvert.SerializeObject(listTelefonoRegistrar);


         




                // TODO: Add insert logic here

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("MensajeGeneral", "Ingrese los campos obligatorio");
                    return View(model);
                }
                else
                {
                    if (!this.IsCaptchaValid(""))
                    {
                        ModelState.AddModelError("MensajeGeneral", "Invalid Captcha");
                        return View(model);
                    }
                    else
                    {
                        RegistrarUsuarioRestauranteRequest reqeust = new RegistrarUsuarioRestauranteRequest();

                        reqeust.Usuario =model.Usuario;
                        reqeust.Restaurante = model.Restaurante;
                        reqeust.Telefonos = listTelefonoRegistrar;

                        RegistrarUsuarioRestauranteResponse objRespuesta = new ProxyApiUsuario().RegistrarUsuarioRestaurante(reqeust);

                        if (objRespuesta.status.estado == 0)
                        {

                            string link = "http://localhost:59052/Usuario/ActivarCuenta?CodigoUsuario=" + objRespuesta.CodigoUsuario;
                            string DetalleMensaje = " Para activar la cuenta ingrese al siguiente enlace ";

                            Mailer CorreoSolicitud = new Mailer();
                            List<String> listCorreso = new List<string>();
                            listCorreso.Add(model.Usuario.usua_email);
                            CorreoSolicitud.Notificacion.CorreosPara = listCorreso;
                            CorreoSolicitud.Notificacion.ConCopia = "";
                            CorreoSolicitud.Notificacion.Asunto = " Activar cuenta";
                            CorreoSolicitud.Notificacion.Cuerpo = new FormatoCorreo().BodyMensaje(model.Usuario.usua_nomb, DetalleMensaje, link);


                            CorreoSolicitud.Enviar();
                            return View("Correcto",model);
                        }
                        else 
                        {                       
                            ModelState.AddModelError("MensajeGeneral", objRespuesta.status.mensaje);

                            return View(model);
                        }

                    }
                }
            }
            catch (Exception err)
            {
                ModelState.AddModelError("MensajeGeneral", err);

                return View(model);
            }
        }

        [HttpGet]   
        [AllowAnonymous]
        // GET: UsuarioRestaurante/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioRestaurante/Edit/5
        [HttpPost]
        
        [AllowAnonymous]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        // GET: UsuarioRestaurante/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        // GET: UsuarioRestaurante/Delete/5
        public ActionResult Correcto(int id)
        {
            return View();
        }

        // POST: UsuarioRestaurante/Delete/5
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

