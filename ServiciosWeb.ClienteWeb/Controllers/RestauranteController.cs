﻿using Newtonsoft.Json;
using ServiciosWeb.ClienteWeb.Utilitario;
using ServiciosWeb.Dominio;
using ServiciosWeb.Dominio.Control;
using ServiciosWeb.Dominio.Modelo;
using ServiciosWeb.Dominio.Request;
using ServiciosWeb.Dominio.Response;
using ServiciosWeb.DominioResponse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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


        public ActionResult MantenimientoListaPrecio()
        {
            MantenimientoListaPrecioModel model = new MantenimientoListaPrecioModel();
            ProxyApiComun proxyComun = new ProxyApiComun();
            var responseTipoComun = proxyComun.ObtenerTipoComida();

            model.ListComida = new List<SelectListItemCustom>();

            foreach (var item in responseTipoComun.TipoComida)
            {
                model.ListComida.Add(new SelectListItemCustom()
                {
                    Text = item.tico_descrip,
                    Value = item.tico_id.ToString()
                });
            }



            return View(model);
        }

        [HttpPost]
        public ActionResult MantenimientoListaPrecio(MantenimientoListaPrecioModel model)
        {





            try
            {

                ProxyApiComun proxyComun = new ProxyApiComun();
                var responseTipoComun = proxyComun.ObtenerTipoComida();

                model.ListComida = new List<SelectListItemCustom>();

                foreach (var item in responseTipoComun.TipoComida)
                {
                    model.ListComida.Add(new SelectListItemCustom()
                    {
                        Text = item.tico_descrip,
                        Value = item.tico_id.ToString()
                    });
                }




                SeguridadMVC.Seguridad.SessionWrapper objSesion = new SeguridadMVC.Seguridad.SessionWrapper();



                ProxyApiUsuario apiUsuario = new ProxyApiUsuario();

                var responseUsuario = apiUsuario.ObtenerUsuario(objSesion.Usuario.Idusuario);





                RegistrarProductoRequest registrarProducto = new RegistrarProductoRequest();
                registrarProducto.Producto = new Producto();
                registrarProducto.Producto.tico_id = model.CodigoTipoComida;
                registrarProducto.Producto.prod_descrip = model.DescripcionProducto;
                registrarProducto.Producto.prod_nombre = model.DescripcionProducto;
                registrarProducto.Producto.prod_precio = Convert.ToDecimal(model.PrecioProducto);
                registrarProducto.Producto.rest_ruc = responseUsuario.Restaurante.rest_ruc;

                registrarProducto.Producto.tico_id = model.CodigoTipoComida;


                ProxyApiProducto proxyProducto = new ProxyApiProducto();

                RegistrarProductoResponse result = proxyProducto.RegistrarProducto(registrarProducto);


                if (result.status.estado == 0)
                {
                    model.CodigoTipoComida = 0;
                    
                    model.DescripcionProducto = "";
                    model.PrecioProducto = "";

                    return RedirectToAction("MantenimientoListaPrecio");
                }
                else {

                    ModelState.AddModelError("MensajeGeneral", result.status.mensaje);
                }



            }
            catch (Exception err)
            {
                ModelState.AddModelError("MensajeGeneral", err.Message);

            }

            return View(model);
        }

        public ActionResult GestionMenu()
        {
            GestionMenuModel model = new GestionMenuModel();
            model.Menu = new MantenimientoMenuModel();
            model.MenuDetalle = new MantenimientoMenuDetalleModel();

            SeguridadMVC.Seguridad.SessionWrapper sesionSeguridad = new SeguridadMVC.Seguridad.SessionWrapper();


            ProxyApiProducto proxyComun = new ProxyApiProducto();

            var responseProducto = proxyComun.ListarProductoPorUsuario(sesionSeguridad.Usuario.Idusuario);

            model.MenuDetalle.ListProducto = new List<SelectListItemCustom>();

            foreach (var item in responseProducto.Hits)
            {
                model.MenuDetalle.ListProducto.Add(new SelectListItemCustom()
                {
                    Text = item.prod_descrip,
                    Value = item.prod_id.ToString()
                });
            }



            return View(model);
        }


        public PartialViewResult MantMenu(MantenimientoMenuModel model)
        {



            if (ModelState.IsValid)
            {

                SeguridadMVC.Seguridad.SessionWrapper sesionUsuario = new SeguridadMVC.Seguridad.SessionWrapper();

                ProxyApiUsuario proxyUsuario = new ProxyApiUsuario();
                var responseUsuario = proxyUsuario.ObtenerUsuario(sesionUsuario.Usuario.Idusuario);


                ProxyApiRestaurante proxyRestaurante = new ProxyApiRestaurante();
                MenuRegistrarRequest request = new MenuRegistrarRequest();
                request.Menu = new Menu();
                request.Menu.menu_nombre = model.DescripcionMenu;
                request.Menu.menu_estado = false;
                request.Menu.menu_publicado = false;
                request.Menu.menu_ruc = responseUsuario.Restaurante.rest_ruc;



                var response = proxyRestaurante.RegistrarMenu(request);




            }
            else
            {


            }



            return PartialView(model);

        }


        public PartialViewResult MantMenuDetalle(MantenimientoMenuDetalleModel model)
        {

            SeguridadMVC.Seguridad.SessionWrapper sesionSeguridad = new SeguridadMVC.Seguridad.SessionWrapper();


            ProxyApiProducto proxyProducto = new ProxyApiProducto();

            var responseProducto = proxyProducto.ListarProductoPorUsuario(sesionSeguridad.Usuario.Idusuario);


            var objProducto = proxyProducto.LeerProducto(model.CodigoProducto);


            model.ListProducto = new List<SelectListItemCustom>();

            foreach (var item in responseProducto.Hits)
            {
                model.ListProducto.Add(new SelectListItemCustom()
                {
                    Text = item.prod_descrip,
                    Value = item.prod_id.ToString()
                });
            }




            if (ModelState.IsValid)
            {
                if (model.CodigoMenu != 0)
                {

                    ProxyApiRestaurante objProxy = new ProxyApiRestaurante();
                    MenuDetalleRegistrarRequest request = new MenuDetalleRegistrarRequest();
                    request.MenuDetalle = new MenuDetalle();
                    request.MenuDetalle.mede_disponible = true;
                    request.MenuDetalle.mede_precio = objProducto.Hit.prod_precio;
                    request.MenuDetalle.menu_id = model.CodigoMenu;
                    request.MenuDetalle.prod_id = model.CodigoProducto;

                    var response = objProxy.RegistrarMenuDetalle(request);


                }
            }
            else { 
            
            
            }




           return PartialView(model);

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
            model.usua_rutaimagen = usuarioResponse.Usuario.usua_rutaiamgen;



            if (usuarioResponse.Restaurante != null)
            {

                model.rest_rz = usuarioResponse.Restaurante.rest_rz;
                model.rest_nomcomer = usuarioResponse.Restaurante.rest_nomcomer;
                model.rest_ruc = usuarioResponse.Restaurante.rest_ruc;

                model.esti_id = usuarioResponse.Restaurante.esti_id;
                model.rest_descrip = usuarioResponse.Restaurante.rest_descrip;

                if (usuarioResponse.Restaurante.rest_reservalocal != null)
                {
                    model.rest_reservalocal = Convert.ToBoolean(usuarioResponse.Restaurante.rest_reservalocal);
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




            var respuestaTelefonos = api.ObtenerTelefonosUsuario(objSesion.Usuario.Idusuario);

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
        public ActionResult MisDatos(UsuarioRestauranteModel model, FormCollection formCollection)
        {


            string vRutaParcial = "/File/Foto/";
            string archivo = "";
            string vRuta;
            string vExtension;

            

            model.Distritos = new List<SelectListItemCustom>();
            model.EspecialidadTipo = new List<SelectListItemCustom>();
            model.TipoCuenta = new List<SelectListItemCustom>();

            model.MensajeSucces = string.Empty;



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







                    if (model.uploadFile != null)
                    {
                        if (model.uploadFile.ContentLength > 5242880)
                        {

                            ProcesoResponse resonseStatus = new ProcesoResponse();
                            resonseStatus.estado = -1;
                            resonseStatus.mensaje = "No debe exceder los 5MB";
                            


                            
                            return RedirectToAction("NuevoColaborador", new { });
                        }
                    }




                    if (model.uploadFile != null)
                    {

                        vExtension = Path.GetExtension(model.uploadFile.FileName);
                        archivo = (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + model.rest_ruc).ToLower() + vExtension;
                        vRutaParcial = vRutaParcial + archivo;
                        vRuta = System.Web.Hosting.HostingEnvironment.MapPath("~" + vRutaParcial);


                        model.uploadFile.SaveAs(vRuta);



                        model.usua_rutaimagen = vRutaParcial;
                    }




                    RegistrarUsuarioRestauranteRequest request = new RegistrarUsuarioRestauranteRequest();
                    request.Usuario = new Usuario();
                    request.Usuario.usua_id = model.usua_id;
                    request.Usuario.dist_id = model.dist_id;
                    request.Usuario.usua_refedirec = model.usua_refedirec;
                    request.Usuario.usua_direc = model.usua_direc;
                    request.Usuario.usua_rutaiamgen = model.usua_rutaimagen;


                    request.Restaurante = new Restaurante();
                    request.Restaurante.usua_id = model.usua_id;
                    request.Restaurante.rest_descrip = model.rest_descrip;
                    request.Restaurante.rest_delivery = model.rest_delivery;
                    request.Restaurante.rest_reservalocal = model.rest_reservalocal;
                    request.Restaurante.esti_id = model.esti_id;
                    request.Restaurante.rest_nomcomer = model.rest_nomcomer;
                    request.Restaurante.uscta_numero = model.uscta_numero;
                    request.Restaurante.uscta_titular = model.uscta_titular;
                    request.Restaurante.ticta_id = model.ticta_id;

                    request.Telefonos = listTelefonoRegistrar;

                    var objRespuesta = apiUsuario.ActualizarUsuarioRestaurante(request);


                    if (objRespuesta.status.estado == 0)
                    {

                        model.MensajeSucces = "Actualizado";
                        return View(model);
                    }
                    else
                    {
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


        public JsonResult EliminarProducto(string datos)
        {
            ProcesoResponse respuesta = new ProcesoResponse();
            ProxyApiProducto    proxyProducto = new ProxyApiProducto();

            foreach (string item in datos.Split(';')) 
            {
                if(  item != string.Empty)
                {
                    proxyProducto.Eliminar(Convert.ToInt32(item));
                }

            }

            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            jsonResult.Data = respuesta;

            return jsonResult;
        }


        public JsonResult EliminarMenuDetalle(string datos)
        {
            ProcesoResponse respuesta = new ProcesoResponse();
            ProxyApiRestaurante proxyRestaurante = new ProxyApiRestaurante();

            foreach (string item in datos.Split(';'))
            {
                if (item != string.Empty)
                {
                    proxyRestaurante.EliminarMenuDetalle(Convert.ToInt32(item));
                }

            }

            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            jsonResult.Data = respuesta;

            return jsonResult;
        }


        public JsonResult EliminarMenu(string datos)
        {
            ProcesoResponse respuesta = new ProcesoResponse();
            ProxyApiRestaurante proxyRestaurante = new ProxyApiRestaurante();

            foreach (string item in datos.Split(';'))
            {
                if (item != string.Empty)
                {
                    proxyRestaurante.EliminarMenu(Convert.ToInt32(item));
                }

            }

            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            jsonResult.Data = respuesta;

            return jsonResult;
        }



        public JsonResult PublicarMenu(string datos, bool select)
        {
            ProcesoResponse respuesta = new ProcesoResponse();
            ProxyApiRestaurante proxyRestaurante = new ProxyApiRestaurante();

            foreach (string item in datos.Split(';'))
            {
                if (item != string.Empty)
                {
                    proxyRestaurante.MenuPublicar(Convert.ToInt32(item),select);
                }

            }

            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            jsonResult.Data = respuesta;

            return jsonResult;
        }

        public JsonResult LitaPrecios(FormCollection frm)
        {
            string iDisplayLength = HttpContext.Request.Form["iDisplayLength"];
            string iDisplayStart = HttpContext.Request.Form["iDisplayStart"];
            string sEcho = HttpContext.Request.Form["sEcho"];
            string sData = HttpContext.Request.Form["sData"];

            ResponseOperacionBE o_ResponseOperacion = new ResponseOperacionBE();
            o_ResponseOperacion.OperacionType = new OperacionType();
            o_ResponseOperacion.OperacionType.codigo_operacion = "LISTA_PRECIOS";
            o_ResponseOperacion.OperacionType.nombre_operacion = "Listar precios";
            o_ResponseOperacion.OperacionType.mensaje_operacion = "Listado con éxito";
            o_ResponseOperacion.OperacionType.estado_operacion = "0";

            RequestOperacionBE Request = new RequestOperacionBE();
            Request = new JavaScriptSerializer().Deserialize<RequestOperacionBE>(sData);
            Request.DataTableRquest = new DataTableRequest();

            Request.DataTableRquest.iDisplayLength = Convert.ToInt32(iDisplayLength);
            Request.DataTableRquest.iDisplayStart = Convert.ToInt32(iDisplayStart);
            Request.DataTableRquest.sEcho = sEcho;


            DataTableResponse ResponseOperacion = new DataTableResponse();
            int nIdIniComp = Request.DataTableRquest.iDisplayStart;
            int nIdFinComp = Request.DataTableRquest.iDisplayLength;

            nIdFinComp = nIdIniComp + nIdFinComp;
            nIdIniComp = nIdIniComp + 1;

            try
            {

                SeguridadMVC.Seguridad.SessionWrapper objSesion = new SeguridadMVC.Seguridad.SessionWrapper();
                ProxyApiUsuario apiUsuario = new ProxyApiUsuario();

                var responseUsuario = apiUsuario.ObtenerUsuario(objSesion.Usuario.Idusuario);


                var deserailizar = new JsonSerializerSettings();
                deserailizar.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

                deserailizar.Culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                var parameter = Newtonsoft.Json.JsonConvert.DeserializeObject<ListarPrecioRequest>(Request.OperacionType.Objeto1.ToString(), deserailizar);

                parameter.prm_reginicio = nIdIniComp;
                parameter.prm_regfin = nIdFinComp;



                parameter.RucRestaurante = responseUsuario.Restaurante.rest_ruc;

                ProxyApiProducto proxyProducto = new ProxyApiProducto();

                ListaPrecioResponse result = proxyProducto.ListarPrecio(parameter);



                ResponseOperacion.aaData = result.Hits;
                ResponseOperacion.iTotalRecords = Request.DataTableRquest.iDisplayLength;
                ResponseOperacion.iTotalDisplayRecords = result.totalregistros;
                ResponseOperacion.sEcho = Request.DataTableRquest.sEcho;

                o_ResponseOperacion.DataTableResponse = ResponseOperacion;

            }
            catch (Exception err)
            {

                o_ResponseOperacion.OperacionType.mensaje_operacion = "Error inesperado";
                o_ResponseOperacion.OperacionType.estado_operacion = "-1";
            }

            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            jsonResult.Data = o_ResponseOperacion;
            return jsonResult;
        }

        public JsonResult LitaMenu(FormCollection frm)
        {
            string iDisplayLength = HttpContext.Request.Form["iDisplayLength"];
            string iDisplayStart = HttpContext.Request.Form["iDisplayStart"];
            string sEcho = HttpContext.Request.Form["sEcho"];
            string sData = HttpContext.Request.Form["sData"];

            ResponseOperacionBE o_ResponseOperacion = new ResponseOperacionBE();
            o_ResponseOperacion.OperacionType = new OperacionType();
            o_ResponseOperacion.OperacionType.codigo_operacion = "LISTAR_MENU";
            o_ResponseOperacion.OperacionType.nombre_operacion = "Listar precios";
            o_ResponseOperacion.OperacionType.mensaje_operacion = "Listado con éxito";
            o_ResponseOperacion.OperacionType.estado_operacion = "0";

            RequestOperacionBE Request = new RequestOperacionBE();
            Request = new JavaScriptSerializer().Deserialize<RequestOperacionBE>(sData);
            Request.DataTableRquest = new DataTableRequest();

            Request.DataTableRquest.iDisplayLength = Convert.ToInt32(iDisplayLength);
            Request.DataTableRquest.iDisplayStart = Convert.ToInt32(iDisplayStart);
            Request.DataTableRquest.sEcho = sEcho;


            DataTableResponse ResponseOperacion = new DataTableResponse();
            int nIdIniComp = Request.DataTableRquest.iDisplayStart;
            int nIdFinComp = Request.DataTableRquest.iDisplayLength;

            nIdFinComp = nIdIniComp + nIdFinComp;
            nIdIniComp = nIdIniComp + 1;

            try
            {

                SeguridadMVC.Seguridad.SessionWrapper objSesion = new SeguridadMVC.Seguridad.SessionWrapper();
                ProxyApiUsuario apiUsuario = new ProxyApiUsuario();

                var responseUsuario = apiUsuario.ObtenerUsuario(objSesion.Usuario.Idusuario);


                var deserailizar = new JsonSerializerSettings();
                deserailizar.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

                deserailizar.Culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                var parameter = Newtonsoft.Json.JsonConvert.DeserializeObject<ListarMenuRequest>(Request.OperacionType.Objeto1.ToString(), deserailizar);

                parameter.prm_reginicio = nIdIniComp;
                parameter.prm_regfin = nIdFinComp;



                parameter.CodigoUsuario = responseUsuario.Usuario.usua_id;

                ProxyApiRestaurante proxyRestauramte = new ProxyApiRestaurante();

                ListaMenuResponse result = proxyRestauramte.ListarMenu(parameter);



                ResponseOperacion.aaData = result.Hits;
                ResponseOperacion.iTotalRecords = Request.DataTableRquest.iDisplayLength;
                ResponseOperacion.iTotalDisplayRecords = result.totalregistros;
                ResponseOperacion.sEcho = Request.DataTableRquest.sEcho;

                o_ResponseOperacion.DataTableResponse = ResponseOperacion;

            }
            catch (Exception err)
            {
                o_ResponseOperacion.OperacionType.mensaje_operacion = err.Message;
                o_ResponseOperacion.OperacionType.estado_operacion = "-1";
            }

            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            jsonResult.Data = o_ResponseOperacion;
            return jsonResult;
        }



        public JsonResult LitaMenuDetalle(FormCollection frm)
        {
            string iDisplayLength = HttpContext.Request.Form["iDisplayLength"];
            string iDisplayStart = HttpContext.Request.Form["iDisplayStart"];
            string sEcho = HttpContext.Request.Form["sEcho"];
            string sData = HttpContext.Request.Form["sData"];

            ResponseOperacionBE o_ResponseOperacion = new ResponseOperacionBE();
            o_ResponseOperacion.OperacionType = new OperacionType();
            o_ResponseOperacion.OperacionType.codigo_operacion = "LISTAR_MENU_DETALLE";
            o_ResponseOperacion.OperacionType.nombre_operacion = "Listar menu detalle";
            o_ResponseOperacion.OperacionType.mensaje_operacion = "Listado con éxito";
            o_ResponseOperacion.OperacionType.estado_operacion = "0";

            RequestOperacionBE Request = new RequestOperacionBE();
            Request = new JavaScriptSerializer().Deserialize<RequestOperacionBE>(sData);
            Request.DataTableRquest = new DataTableRequest();

            Request.DataTableRquest.iDisplayLength = Convert.ToInt32(iDisplayLength);
            Request.DataTableRquest.iDisplayStart = Convert.ToInt32(iDisplayStart);
            Request.DataTableRquest.sEcho = sEcho;


            DataTableResponse ResponseOperacion = new DataTableResponse();
            int nIdIniComp = Request.DataTableRquest.iDisplayStart;
            int nIdFinComp = Request.DataTableRquest.iDisplayLength;

            nIdFinComp = nIdIniComp + nIdFinComp;
            nIdIniComp = nIdIniComp + 1;

            try
            {

               
                var deserailizar = new JsonSerializerSettings();
                deserailizar.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

                deserailizar.Culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                var parameter = Newtonsoft.Json.JsonConvert.DeserializeObject<ListarMenuDetalleRequest>(Request.OperacionType.Objeto1.ToString(), deserailizar);

                parameter.prm_reginicio = nIdIniComp;
                parameter.prm_regfin = nIdFinComp;



                

                ProxyApiRestaurante proxyRestauramte = new ProxyApiRestaurante();

                ListaMenuDetalleResponse result = proxyRestauramte.ListarMenuDetalle(parameter);



                ResponseOperacion.aaData = result.Hits;
                ResponseOperacion.iTotalRecords = Request.DataTableRquest.iDisplayLength;
                ResponseOperacion.iTotalDisplayRecords = result.totalregistros;
                ResponseOperacion.sEcho = Request.DataTableRquest.sEcho;

                o_ResponseOperacion.DataTableResponse = ResponseOperacion;

            }
            catch (Exception err)
            {
                o_ResponseOperacion.OperacionType.mensaje_operacion = err.Message;
                o_ResponseOperacion.OperacionType.estado_operacion = "-1";
            }

            var jsonResult = new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            jsonResult.Data = o_ResponseOperacion;
            return jsonResult;
        }


    }
}

