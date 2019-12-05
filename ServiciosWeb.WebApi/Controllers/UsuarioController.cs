using ServiciosWeb.Datos.Modelo;
using ServiciosWeb.Dominio.Request;
using ServiciosWeb.Dominio.Response;
using ServiciosWeb.DominioResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiciosWeb.WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        BDRestaurantesEntities BD = new BDRestaurantesEntities();

        [HttpGet]
        public IEnumerable<t_usuario> Get()
        {
            var listado = BD.t_usuario.ToList();
            return listado;
        }

        //GET api/Usuario/{id}
        /// <summary>
        /// Permite consultar la información de todos los libros
        /// </summary>
        /// <param name="id">Identificador del objeto GET</param>
        /// <returns></returns>

        [HttpGet]
        public IHttpActionResult Get(int id)
        {

            ObtenerUsuarioResponse objresponse = new ObtenerUsuarioResponse();

            objresponse.status = new ProcesoResponse();

            try
            {

                var usuario = BD.t_usuario.FirstOrDefault(x => x.usua_id == id);



                var Restaurante = BD.t_restaurante.FirstOrDefault(z => z.usua_id == id);

                if (usuario != null)
                {
                    objresponse.status.estado = 0;
                    objresponse.Usuario = new Dominio.Usuario();

                    objresponse.Usuario.usua_nomb = usuario.usua_nomb;

                    objresponse.Usuario.usua_id = usuario.usua_id;

                    objresponse.Usuario.usua_email = usuario.usua_email;

                    objresponse.Usuario.usua_dni = usuario.usua_dni;
                    objresponse.Usuario.usua_direc = usuario.usua_direc;
                    objresponse.Usuario.usua_refedirec = usuario.usua_refedirec;



                    var tDistrito = BD.t_distrito.FirstOrDefault(x => x.dist_id == usuario.dist_id);
                    if (tDistrito != null)
                    {


                        objresponse.Usuario.dist_id = tDistrito.dist_id;
                        objresponse.Usuario.dist_nombre = tDistrito.dist_nombre;

                    }



                    if (Restaurante != null)
                    {
                        objresponse.Restaurante = new Dominio.Restaurante();
                        objresponse.Restaurante.rest_nomcomer = Restaurante.rest_nomcomer;
                        objresponse.Restaurante.rest_ruc = Restaurante.rest_ruc;
                        objresponse.Restaurante.rest_rz = Restaurante.rest_rz;
                        objresponse.Restaurante.rest_delivery = Restaurante.rest_delivery;
                        objresponse.Restaurante.rest_descrip = Restaurante.rest_descrip;
                        objresponse.Restaurante.rest_estado = Restaurante.rest_estado;
                        objresponse.Restaurante.rest_reservalocal = Restaurante.rest_reservalocal;
                        objresponse.Restaurante.rest_rz = Restaurante.rest_rz;


                        var tEspecialidadTipo = BD.t_especialidadtipo.FirstOrDefault(x => x.esti_id == Restaurante.esti_id);

                        if (tEspecialidadTipo != null)
                        {
                            objresponse.Restaurante.esti_id = tEspecialidadTipo.esti_id;
                            objresponse.Restaurante.esti_descripcion = tEspecialidadTipo.esti_descrip;
                            objresponse.Restaurante.esti_id = tEspecialidadTipo.esti_id;

                        }

                        var tCuenta = BD.t_usuariocuenta.FirstOrDefault(z => z.usua_id == usuario.usua_id);


                        if (tCuenta != null)
                        {
                            objresponse.Restaurante.uscta_numero = tCuenta.uscta_numero;
                            objresponse.Restaurante.uscta_titular = tCuenta.uscta_titular;
                            var TipoCuenta = BD.t_tipocta.FirstOrDefault(x => x.ticta_id == tCuenta.ticta_id);
                            if (TipoCuenta != null)
                            {
                                objresponse.Restaurante.ticta_id = TipoCuenta.ticta_id;
                                objresponse.Restaurante.ticta_descrip = TipoCuenta.ticta_descrip;

                            }
                        }

                    }

                    objresponse.status.mensaje = "Exitoso";
                }
                else
                {

                    objresponse.status.estado = 1;
                    objresponse.status.mensaje = "No se encontro";

                }


            }
            catch (Exception err)
            {
                objresponse.status.estado = -1;
                objresponse.status.mensaje = err.Message;
                throw err;

            }

            return Ok(objresponse);


        }

        [Route("api/usuario/activarcuenta/")]
        [HttpGet]
        public IHttpActionResult ActivarCuenta(int id)
        {

            ActivarCuentaResponse objresponse = new ActivarCuentaResponse();

            objresponse.status = new ProcesoResponse();
            objresponse.Usuario = new Dominio.Usuario();

            try
            {
                var usuario = BD.t_usuario.FirstOrDefault(x => x.usua_id == id);

                if (usuario != null)
                {
                    usuario.usua_esta = true;
                    BD.SaveChanges();

                    objresponse.Usuario.usua_id = usuario.usua_id;
                    objresponse.Usuario.usua_nomb = usuario.usua_nomb;

                    objresponse.status.mensaje = "Activado con exito";
                }
                else
                {
                    objresponse.status.estado = 1;
                    objresponse.status.mensaje = "No se encontro";

                }


            }
            catch (Exception err)
            {
                objresponse.status.estado = -1;
                objresponse.status.mensaje = err.Message;
                throw err;

            }

            return Ok(objresponse);


        }

        [Route("api/usuario/search_correo/")]
        [HttpGet]
        public IHttpActionResult GetPorCorreo(string correo)
        {
            ObtenerUsuarioResponse objresponse = new ObtenerUsuarioResponse();
            objresponse.status = new ProcesoResponse();

            try
            {
                var usuario = BD.t_usuario.FirstOrDefault(x => x.usua_email == correo);

                if (usuario != null)
                {
                    objresponse.status.estado = 0;
                    objresponse.Usuario = new Dominio.Usuario();
                    objresponse.Usuario.usua_nomb = usuario.usua_nomb;

                    objresponse.Usuario.usua_id = usuario.usua_id;

                    objresponse.Usuario.usua_email = usuario.usua_email;

                    objresponse.Usuario.usua_dni = usuario.usua_dni;
                    objresponse.status.mensaje = "Exitoso";
                }
                else
                {
                    objresponse.status.estado = 1;
                    objresponse.status.mensaje = "No se encontro";
                }

            }
            catch (Exception err)
            {
                objresponse.status.estado = -1;
                objresponse.status.mensaje = err.Message;
                throw err;

            }

            return Ok(objresponse);


        }


        [Route("api/usuario/validar_clave/")]
        [HttpPost]
        public IHttpActionResult ValidarClave(ValidarClaveUsuarioRequest request)
        {
            ValidarClaveUsuarioResponse objresponse = new ValidarClaveUsuarioResponse();
            objresponse.status = new ProcesoResponse();

            try
            {
                var usuario = BD.t_usuario.FirstOrDefault(x => x.usua_pass == request.Clave && x.usua_id==request.CodigoUsuario);

                if (usuario != null)
                {
                    objresponse.status.estado = 0;
                    objresponse.Usuario = new Dominio.Usuario();
                    objresponse.Usuario.usua_nomb = usuario.usua_nomb;
                    objresponse.Usuario.usua_id = usuario.usua_id;
                    objresponse.Usuario.usua_email = usuario.usua_email;
                    objresponse.Usuario.usua_dni = usuario.usua_dni;
                    objresponse.status.mensaje = "Clave correcto";
                }
                else
                {
                    objresponse.status.estado = 1;
                    objresponse.status.mensaje = "La clave es incorrecto";
                }
            }
            catch (Exception err)
            {
                objresponse.status.estado = -1;
                objresponse.status.mensaje = err.Message;
                throw err;
            }
            return Ok(objresponse);
        }


        [Route("api/usuario/cambiar_clave/")]
        [HttpPost]
        public IHttpActionResult CambiarClave(CambiarClaveUsuarioRequest request)
        {
            CambiarClaveUsuarioResponse objresponse = new CambiarClaveUsuarioResponse();
            objresponse.status = new ProcesoResponse();

            try
            {
                var usuario = BD.t_usuario.FirstOrDefault(x => x.usua_id == request.CodigoUsuario);

                if (usuario != null)
                {

                    usuario.usua_pass =request.Clave;
                    BD.SaveChanges();


                    objresponse.status.estado = 0;

                    objresponse.Usuario = new Dominio.Usuario();
                    objresponse.Usuario.usua_nomb = usuario.usua_nomb;
                    objresponse.Usuario.usua_id = usuario.usua_id;
                    objresponse.Usuario.usua_email = usuario.usua_email;
                    objresponse.Usuario.usua_dni = usuario.usua_dni;

                    objresponse.status.mensaje = "Clave actualizado";
                }
                else
                {
                    objresponse.status.estado = 1;
                    objresponse.status.mensaje = "No se econtro el usuario";
                }
            }
            catch (Exception err)
            {
                objresponse.status.estado = -1;
                objresponse.status.mensaje = err.Message;
                throw err;
            }
            return Ok(objresponse);
        }


        //INSERCIÖN
        [HttpPost]
        public IHttpActionResult Post(RegistrarUsuarioRestauranteRequest request)
        {


            RegistrarUsuarioRestauranteResponse objresponse = new RegistrarUsuarioRestauranteResponse();
            objresponse.status = new ProcesoResponse();


            try
            {
                var usuario = BD.t_usuario.FirstOrDefault(x => x.usua_email == request.Usuario.usua_email);


                var restaurante = BD.t_restaurante.FirstOrDefault(x => x.rest_ruc == request.Restaurante.rest_ruc);

                if (restaurante != null)
                {
                    objresponse.status.estado = 2;
                    objresponse.status.mensaje = "Ya existe una empresa con el mismo RUC";

                    return Ok(objresponse);
                }


                if (usuario != null)
                {
                    objresponse.status.estado = 1;
                    objresponse.status.mensaje = "Ya existe un usuario registrado con el mismo correo";

                    return Ok(objresponse);
                }



                t_usuario nueUsuario = new t_usuario();
                nueUsuario.usua_direc = null;
                nueUsuario.usua_dni = request.Usuario.usua_dni;
                nueUsuario.usua_email = request.Usuario.usua_email;
                nueUsuario.usua_esta = false;
                nueUsuario.usua_fecnac = null;
                nueUsuario.usua_nomb = request.Usuario.usua_nomb;
                nueUsuario.usua_pass = request.Usuario.usua_pass;
                nueUsuario.usua_refedirec = null;
                nueUsuario.usua_rutaimagen = null;

                BD.t_usuario.Add(nueUsuario);
                BD.SaveChanges();


                objresponse.CodigoUsuario = nueUsuario.usua_id;


                t_restaurante nuevoRestauramte = new t_restaurante();
                nuevoRestauramte.usua_id = nueUsuario.usua_id;
                nuevoRestauramte.rest_delivery = null;
                nuevoRestauramte.rest_descrip = null;
                nuevoRestauramte.rest_estado = false;
                nuevoRestauramte.rest_nomcomer = null;
                nuevoRestauramte.rest_reservalocal = null;
                nuevoRestauramte.rest_ruc = request.Restaurante.rest_ruc;
                nuevoRestauramte.rest_rz = request.Restaurante.rest_rz;

                BD.t_restaurante.Add(nuevoRestauramte);


                foreach (var item in request.Telefonos)
                {


                    t_telefono nuevoTelefono = new t_telefono();
                    nuevoTelefono.tele_nume = item.NumeroTelefono;
                    nuevoTelefono.usua_id = nueUsuario.usua_id;
                    nuevoTelefono.tite_id = item.CodigoTipoTelefono;



                    BD.t_telefono.Add(nuevoTelefono);
                }


                BD.SaveChanges();

                objresponse.status.estado = 0;
                objresponse.status.mensaje = "Registrado con exito";


            }
            catch (Exception err)
            {
                objresponse.status.estado = -1;
                objresponse.status.mensaje = err.Message;
                throw err;

            }

            return Ok(objresponse);

        }

        [HttpPut]
        public IHttpActionResult Put(RegistrarUsuarioRestauranteRequest request)
        {
            RegistrarUsuarioRestauranteResponse objresponse = new RegistrarUsuarioRestauranteResponse();
            objresponse.status = new ProcesoResponse();


            try
            {


                var usuarioActualizar = BD.t_usuario.FirstOrDefault(x => x.usua_id == request.Usuario.usua_id);

                usuarioActualizar.usua_direc = request.Usuario.usua_direc;
                usuarioActualizar.dist_id = request.Usuario.dist_id;
                usuarioActualizar.usua_refedirec = request.Usuario.usua_refedirec;

                var tRestuarante = BD.t_restaurante.FirstOrDefault(x => x.usua_id == request.Usuario.usua_id);
                tRestuarante.rest_delivery = request.Restaurante.rest_delivery;
                tRestuarante.rest_reservalocal = request.Restaurante.rest_reservalocal;
                tRestuarante.rest_nomcomer = request.Restaurante.rest_nomcomer;
                tRestuarante.esti_id = request.Restaurante.esti_id;
                tRestuarante.rest_descrip = request.Restaurante.rest_descrip;


                var objUsuarioCuenta = BD.t_usuariocuenta.FirstOrDefault(x => x.usua_id == request.Usuario.usua_id);

                if (objUsuarioCuenta == null)
                {
                    t_usuariocuenta objCuenta = new t_usuariocuenta();
                    objCuenta.uscta_titular = request.Restaurante.uscta_titular;
                    objCuenta.uscta_numero = request.Restaurante.uscta_numero;
                    objCuenta.usua_id = request.Usuario.usua_id;
                    objCuenta.ticta_id = request.Restaurante.ticta_id;

                    BD.t_usuariocuenta.Add(objCuenta);

                }

                else
                {
                    objUsuarioCuenta.uscta_titular = request.Restaurante.uscta_titular;
                    objUsuarioCuenta.uscta_numero = request.Restaurante.uscta_numero;
                    objUsuarioCuenta.usua_id = request.Usuario.usua_id;
                }


                var tTelefonosExistentes = BD.t_telefono.Where(z => z.usua_id == request.Usuario.usua_id);


                foreach (var item in tTelefonosExistentes)
                {
                    BD.t_telefono.Remove(item);

                }



                foreach (var item in request.Telefonos)
                {


                    t_telefono nuevoTelefono = new t_telefono();
                    nuevoTelefono.tele_nume = item.NumeroTelefono;
                    nuevoTelefono.usua_id = request.Usuario.usua_id;
                    nuevoTelefono.tite_id = item.CodigoTipoTelefono;

                    BD.t_telefono.Add(nuevoTelefono);
                }


                BD.SaveChanges();



            }
            catch (Exception err)
            {
                objresponse.status.estado = -1;
                objresponse.status.mensaje = err.Message;
                throw err;

            }

            return Ok(objresponse);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var usuarioEliminar = BD.t_usuario.FirstOrDefault(x => x.usua_id == id);
            BD.t_usuario.Remove(usuarioEliminar);
            return BD.SaveChanges() > 0;
        }

    }
}