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


                var query = from p in BD.t_producto
                            join c in BD.t_tipocomida on p.tico_id equals c.tico_id
                            where p.rest_ruc.Equals(reqest.RucRestaurante)
                            select new {  p.prod_descrip, p.prod_id,p.prod_nombre,p.prod_precio,p.rest_ruc,
                                p.tico_id,c.tico_descrip };


                foreach (var item in query)
                {

                    objresponse.Hits.Add(new Dominio.Producto() {

                        prod_descrip =item.prod_descrip,
                        prod_id = item.prod_id,
                        prod_nombre = item.prod_nombre,
                        prod_precio = item.prod_precio,
                        rest_ruc = item.rest_ruc,
                        tico_descrip = item.tico_descrip



                    }) ;
                }

                objresponse.totalregistros = query.Count();

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
