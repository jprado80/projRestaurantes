using ServiciosWeb.Datos.Modelo;
using ServiciosWeb.Dominio.Request;
using ServiciosWeb.Dominio.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiciosWeb.WebApi.Areas.Restaurante.Controllers
{
    public class ListaPrecioController : ApiController
    {

        BDRestaurantesEntities BD = new BDRestaurantesEntities();

        // GET: api/ListaPrecio
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ListaPrecio/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/restaurante/producto/")]
        [HttpPost]
        public IHttpActionResult GetListaPrecio(ListarPrecioRequest reqest)
        {
            ListaPrecioResponse objresponse = new ListaPrecioResponse();

            objresponse.status = new ProcesoResponse();
            objresponse.Hits = new List<Dominio.Producto>();



            try
            {
                var tProductos = BD.t_producto.Where(x => x.rest_ruc == reqest.RucRestaurante);


                foreach (var item in tProductos)
                {

                    objresponse.Hits.Add(new Dominio.Producto() {

                        prod_descrip =item.prod_descrip,
                        prod_id = item.prod_id,
                        prod_nombre = item.prod_nombre,
                        prod_precio = item.prod_precio,
                        rest_ruc = item.rest_ruc
                         


                    }) ;
                }

                objresponse.totalregistros = tProductos.Count();

                objresponse.status.estado = 0;

                objresponse.status.mensaje = "Info de Precios";
              
                


            }
            catch (Exception err)
            {
                objresponse.status.estado = -1;
                objresponse.status.mensaje = err.Message;
                throw err;

            }

            return Ok(objresponse);

        }

        // POST: api/ListaPrecio
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ListaPrecio/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ListaPrecio/5
        public void Delete(int id)
        {
        }
    }
}
