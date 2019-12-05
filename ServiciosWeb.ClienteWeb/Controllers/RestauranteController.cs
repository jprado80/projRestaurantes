using Newtonsoft.Json;
using ServiciosWeb.ClienteWeb.Utilitario;
using ServiciosWeb.Dominio;
using ServiciosWeb.Dominio.Control;
using ServiciosWeb.Dominio.Request;
using ServiciosWeb.Dominio.Response;
using ServiciosWeb.DominioResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPC.ApiServicesProxy;

namespace ServiciosWeb.ClienteWeb.Controllers
{
    public class RestauranteController : Controller
    {

     


        // GET: Restaurante
        public ActionResult Index()
        {
            return View();
        }

        // GET: Restaurante/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Restaurante/Create
        public ActionResult Create()
        {
            return View();
        }



        public ActionResult MisDatos()
        {

            UsuarioRestauranteModel model = new UsuarioRestauranteModel();
            model.Distritos = new List<SelectListItemCustom>();
            model.EspecialidadTipo = new List<SelectListItemCustom>();
            model.TipoCuenta = new List<SelectListItemCustom>();



           SeguridadMVC.Seguridad.SessionWrapper objSesion = new SeguridadMVC.Seguridad.SessionWrapper();

            ProxyApiComun api = new ProxyApiComun();
            ProxyApiUsuario apiUsuario = new ProxyApiUsuario();

            List<Telefono> listTelefonoRegistrar = new List<Telefono>();


            ObtenerUsuarioResponse usuarioResponse = apiUsuario.ObtenerUsuario(objSesion.Usuario.Idusuario);


            model.usua_email = usuarioResponse.Usuario.usua_nomb;
            model.usua_refedirec = usuarioResponse.Usuario.usua_refedirec;
            model.usua_direc = usuarioResponse.Usuario.usua_direc;
            model.dist_id = usuarioResponse.Usuario.dist_id;



            if (usuarioResponse.Restaurante != null)
            {

                model.rest_rz = usuarioResponse.Restaurante.rest_rz;
                model.rest_nomcomer = usuarioResponse.Restaurante.rest_nomcomer;
                model.rest_ruc = usuarioResponse.Restaurante.rest_ruc;

                model.esti_id = usuarioResponse.Restaurante.esti_id;
                model.rest_descrip = usuarioResponse.Restaurante.rest_descrip;

                if(usuarioResponse.Restaurante.rest_reservalocal !=null)
                {
                    model.rest_reservalocal = Convert.ToBoolean( usuarioResponse.Restaurante.rest_reservalocal);
                }
                else
                {
                    model.rest_reservalocal = false;
                }

                if (usuarioResponse.Restaurante.rest_delivery != null)
                {
                    model.rest_delivery = Convert.ToBoolean(usuarioResponse.Restaurante.rest_delivery);
                }
                else
                {
                    model.rest_delivery = false;
                }
              

                model.uscta_numero = usuarioResponse.Restaurante.uscta_numero;
                model.ticta_id = usuarioResponse.Restaurante.ticta_id;
                model.uscta_titular = usuarioResponse.Restaurante.uscta_titular;


            }


            ObtenerComunResponse responseComun = api.ObtenerComun();
     

            foreach (Distrito item in responseComun.Distritos)
            {
                model.Distritos.Add(new SelectListItemCustom { Text = item.dist_nombre, Value = item.dist_id.ToString() });
            }

            foreach (EspecialidadTipo itemEspe in responseComun.EspecialidadesTipo)
            {
                model.EspecialidadTipo.Add(new SelectListItemCustom { Text = itemEspe.esti_descrip, Value = itemEspe.esti_id.ToString() });
            }

            foreach (TipoCuenta itemCuenta in responseComun.TipoCuentas)
            {
                model.TipoCuenta.Add(new SelectListItemCustom { Text = itemCuenta.tico_descrip, Value = itemCuenta.tico_id.ToString() });
            }


            ObtenerTipoTelefonoResponse responseTipoTelefono = api.ObtenerTipoTelefonos();
            model.ListTipoTelefono = new List<SelectListItemCustom>();
            model.ListTipoTelefono.Add(new SelectListItemCustom { Text = "Seleccionar", Value = "0", Selected = true });

            foreach (TipoTelefono item in responseTipoTelefono.TipoTelefonos)
            {
                model.ListTipoTelefono.Add(new SelectListItemCustom { Text = item.DescripcionTelefono, Value = item.CodigoTelefono.ToString() });
            }




            var respuestaTelefonos =  api.ObtenerTelefonosUsuario(objSesion.Usuario.Idusuario);

            if (respuestaTelefonos.status.estado == 0)
            {


                foreach (Telefono itemTelefono in respuestaTelefonos.Telefonos)
                {
                    listTelefonoRegistrar.Add(new Telefono()
                    {
                        CodigoTipoTelefono = itemTelefono.CodigoTipoTelefono,
                        NumeroTelefono = itemTelefono.NumeroTelefono,
                        CodigoUsuario = itemTelefono.CodigoUsuario
                    });

                }

            }

            model.Telefonos = JsonConvert.SerializeObject(listTelefonoRegistrar);



            return View(model);
        }

        // POST: Restaurante/Create
        [HttpPost]
        public ActionResult MisDatos(UsuarioRestauranteModel view, FormCollection formCollection)
        {

            UsuarioRestauranteModel model = view;

            model.Distritos = new List<SelectListItemCustom>();
            model.EspecialidadTipo = new List<SelectListItemCustom>();
            model.TipoCuenta = new List<SelectListItemCustom>();


            ProxyApiComun api = new ProxyApiComun();
            ProxyApiUsuario apiUsuario = new ProxyApiUsuario();


            List<Telefono> listTelefonoRegistrar = new List<Telefono>();

            try
            {

                SeguridadMVC.Seguridad.SessionWrapper objSesion = new SeguridadMVC.Seguridad.SessionWrapper();



                ObtenerUsuarioResponse usuarioResponse = apiUsuario.ObtenerUsuario(objSesion.Usuario.Idusuario);


                model.usua_email = usuarioResponse.Usuario.usua_email;


                if (usuarioResponse.Restaurante != null)
                {
                    model.rest_rz = usuarioResponse.Restaurante.rest_rz;
                    model.rest_ruc = usuarioResponse.Restaurante.rest_ruc;
                }


                ObtenerComunResponse responseComun = api.ObtenerComun();


                foreach (Distrito item in responseComun.Distritos)
                {
                    model.Distritos.Add(new SelectListItemCustom { Text = item.dist_nombre, Value = item.dist_id.ToString() });
                }

                foreach (EspecialidadTipo itemEspe in responseComun.EspecialidadesTipo)
                {
                    model.EspecialidadTipo.Add(new SelectListItemCustom { Text = itemEspe.esti_descrip, Value = itemEspe.esti_id.ToString() });
                }

                foreach (TipoCuenta itemCuenta in responseComun.TipoCuentas)
                {
                    model.TipoCuenta.Add(new SelectListItemCustom { Text = itemCuenta.tico_descrip, Value = itemCuenta.tico_id.ToString() });
                }


                model.Telefonos = JsonConvert.SerializeObject(listTelefonoRegistrar);

                ObtenerTipoTelefonoResponse responseTipoTelefono = api.ObtenerTipoTelefonos();


                model.ListTipoTelefono = new List<SelectListItemCustom>();
                model.ListTipoTelefono.Add(new SelectListItemCustom { Text = "Seleccionar", Value = "0", Selected = true });

                foreach (TipoTelefono item in responseTipoTelefono.TipoTelefonos)
                {
                    model.ListTipoTelefono.Add(new SelectListItemCustom { Text = item.DescripcionTelefono, Value = item.CodigoTelefono.ToString() });
                }


                if (formCollection[$"slCodigoContacto"] != null && formCollection[$"txtCodigoContacto"] != null)
                {
                    string codigoTipoTelefno = "";
                    string numeroTelefno = "";

                    codigoTipoTelefno = formCollection["slCodigoContacto"].ToString();
                    numeroTelefno = formCollection["txtCodigoContacto"].ToString();

                    listTelefonoRegistrar.Add(new Telefono()
                    {
                        CodigoTipoTelefono = codigoTipoTelefno.Trim() == string.Empty ? 0 : Convert.ToInt32(codigoTipoTelefno),
                        NumeroTelefono = numeroTelefno,
                        CodigoUsuario = 0
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
                            CodigoTipoTelefono = codigoTipoTelefno.Trim() == string.Empty ? 0 : Convert.ToInt32(codigoTipoTelefno),
                            NumeroTelefono = numeroTelefno,
                            CodigoUsuario = 0
                        });

                    }

                    contador++;
                }

                model.Telefonos = JsonConvert.SerializeObject(listTelefonoRegistrar);


                model.usua_id = objSesion.Usuario.Idusuario;

                // TODO: Add insert logic here

                if (!ModelState.IsValid)
                {
                    

                    return View(model);
                }
                else
                {

                    RegistrarUsuarioRestauranteRequest request = new RegistrarUsuarioRestauranteRequest();
                    request.Usuario = new Usuario();
                    request.Usuario.usua_id =model.usua_id;
                    request.Usuario.dist_id =model.dist_id;
                    request.Usuario.usua_refedirec = model.usua_refedirec;
                    request.Usuario.usua_direc = model.usua_direc;


                    request.Restaurante = new Restaurante();
                    request.Restaurante.usua_id =model.usua_id;
                    request.Restaurante.rest_descrip =model.rest_descrip;
                    request.Restaurante.rest_delivery = model.rest_delivery;
                    request.Restaurante.rest_reservalocal = model.rest_reservalocal;
                    request.Restaurante.esti_id = model.esti_id;
                    request.Restaurante.rest_nomcomer = model.rest_nomcomer;
                    request.Restaurante.uscta_numero = model.uscta_numero;
                    request.Restaurante.uscta_titular = model.uscta_titular;
                    request.Restaurante.ticta_id = model.ticta_id;

                    request.Telefonos= listTelefonoRegistrar;

                    var objRespuesta =  apiUsuario.ActualizarUsuarioRestaurante(request);


                    if (objRespuesta.status.estado == 0)
                    {

                        return View(model);
                    }
                    else {
                        ModelState.AddModelError("MensajeGeneral", objRespuesta.status.mensaje);
                        return View(model);
                    }
                    


                    


                    //if (objRespuesta.status.estado == 0)
                    //    {



                    //        return View("Correcto", model);
                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("MensajeGeneral", objRespuesta.status.mensaje);

                    //        return View(model);
                    //    }


                }
            }
            catch (Exception err)
            {
                ModelState.AddModelError("MensajeGeneral", err);

                return View(model);
            }
        }

        // GET: Restaurante/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Restaurante/Edit/5
        [HttpPost]
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

        // GET: Restaurante/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Restaurante/Delete/5
        [HttpPost]
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
