using ServiciosWeb.Datos.Modelo;
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
    public class ComunController : ApiController
    {

        BDRestaurantesEntities BD = new BDRestaurantesEntities();

        // GET: api/TipoTelefono
        public IHttpActionResult Get()
        {
            ObtenerComunResponse objresponse = new ObtenerComunResponse();
            objresponse.Distritos = new List<Dominio.Distrito>();
            objresponse.EspecialidadesTipo = new List<Dominio.EspecialidadTipo>();
            objresponse.TipoCuentas = new List<Dominio.TipoCuenta>();
            objresponse.status = new ProcesoResponse();

            try
            {
                var tDistrito = BD.t_distrito.ToList();
                var tTipoCuenta = BD.t_tipocta.ToList();
                var tEspecialidadTipo = BD.t_especialidadtipo.ToList();

                foreach (var itemDistrito in tDistrito)
                {
                    objresponse.Distritos.Add(new Dominio.Distrito() { dist_id = itemDistrito.dist_id, dist_nombre=itemDistrito.dist_nombre  }) ;
                }


                foreach (var itemEspecialidad in tEspecialidadTipo)
                {
                    objresponse.EspecialidadesTipo.Add(new Dominio.EspecialidadTipo()
                    {
                        esti_descrip = itemEspecialidad.esti_descrip,
                        esti_id= itemEspecialidad.esti_id
                    });   

                }

                foreach (var itemTipoCuenta in tTipoCuenta)
                {
                    objresponse.TipoCuentas.Add(new Dominio.TipoCuenta()
                    {
                        tico_id  = itemTipoCuenta.ticta_id,
                        tico_descrip = itemTipoCuenta.ticta_descrip
                    });

                }

                objresponse.status.estado = 0;
                objresponse.status.mensaje = "Exitoso";

            }
            catch (Exception err)
            {
                objresponse.status.estado = -1;
                objresponse.status.mensaje = err.Message;
                throw err;
            }

            return Ok(objresponse);

        }


        [Route("api/comun/tipocomida/")]
        public IHttpActionResult ObtenerTipoComida()
        {
            ObtenerTipoComidaResponse objresponse = new ObtenerTipoComidaResponse();
            objresponse.TipoComida = new List<Dominio.TipoComida>();
            objresponse.status = new ProcesoResponse();

            try
            {
                var tTipoComida = BD.t_tipocomida.ToList();

                foreach (var item in tTipoComida)
                {
                    objresponse.TipoComida.Add(new Dominio.TipoComida()
                    { tico_id = item.tico_id,
                        tico_descrip = item.tico_descrip });
                }

                objresponse.status.estado = 0;
                objresponse.status.mensaje = "Exitoso";
            }
            catch (Exception err)
            {
                objresponse.status.estado = -1;
                objresponse.status.mensaje = err.Message;
                throw err;
            }

            return Ok(objresponse);

        }

        // GET: api/Comun/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Comun
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Comun/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Comun/5
        public void Delete(int id)
        {
        }
    }
}
