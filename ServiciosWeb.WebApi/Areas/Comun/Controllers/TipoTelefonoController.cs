using ServiciosWeb.Datos.Modelo;
using ServiciosWeb.Dominio;
using ServiciosWeb.Dominio.Response;
using ServiciosWeb.DominioResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiciosWeb.WebApi.Areas.Comun.Controllers
{
    public class TipoTelefonoController : ApiController
    {

        BDRestaurantesEntities BD = new BDRestaurantesEntities();

        // GET: api/TipoTelefono
        public IHttpActionResult Get()
        {
            ObtenerTipoTelefonoResponse objresponse = new ObtenerTipoTelefonoResponse();
            objresponse.TipoTelefonos = new List<TipoTelefono>();
            objresponse.status = new ProcesoResponse();

            try
            {
                var tipoTelefono = BD.t_tipotelefono.ToList();

                if (tipoTelefono.Count() >0)
                {
                    foreach (var item in tipoTelefono)

                    objresponse.TipoTelefonos.Add(new TipoTelefono() 
                    {     
                        CodigoTelefono= item.tite_id, 
                        DescripcionTelefono= item.tite_descrip
                    }) ;

                    objresponse.status.estado = 0;
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

        // GET: api/TipoTelefono/5
        [Route("api/telefono/usuario/")]
        public IHttpActionResult Get(int id)
        {
            ObtenerTelefonoUsuarioResponse objresponse = new ObtenerTelefonoUsuarioResponse();

            objresponse.Telefonos = new List<Telefono>();
            objresponse.status = new ProcesoResponse();

            try
            {
                var tTelefonos = BD.t_telefono.Where(x=>x.usua_id==id);

                if (tTelefonos.Count() > 0)
                {
                    foreach (var item in tTelefonos)

                        objresponse.Telefonos.Add(new Telefono()
                        {
                            CodigoTipoTelefono = item.tite_id,
                            NumeroTelefono = item.tele_nume,
                            CodigoUsuario=item.usua_id
                        });

                    objresponse.status.estado = 0;
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

        // POST: api/TipoTelefono
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TipoTelefono/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TipoTelefono/5
        public void Delete(int id)
        {
        }
    }
}
