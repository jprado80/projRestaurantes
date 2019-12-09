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
    public class ListaMenuRestauranteController : ApiController
    {

        BDRestaurantesEntities BD = new BDRestaurantesEntities();

        // GET: api/ListaPrecio
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ListaPrecio/5
        public IHttpActionResult Get(int id)
        {
            ListaMenuResponse objresponse = new ListaMenuResponse();

            objresponse.status = new ProcesoResponse();
            objresponse.Hits = new List<Dominio.Menu>();



            try
            {


                var query = from m in BD.t_menu
                            join r in BD.t_restaurante on m.menu_ruc equals r.rest_ruc
                            join u in BD.t_usuario on r.usua_id equals u.usua_id

                           where u.usua_id== id
                           
                            select new
                            {
                               m.menu_estado,
                               m.menu_id,
                               m.menu_nombre,
                               m.menu_publicado,
                               m.menu_ruc,
                            };


                foreach (var item in query)
                {

                    objresponse.Hits.Add(new Dominio.Menu()
                    {

                             menu_estado=item.menu_estado,
                        menu_id = item.menu_id,
                        menu_nombre = item.menu_nombre,
                        menu_publicado = item.menu_publicado,
                        menu_ruc = item.menu_ruc




                    });
                }

                objresponse.totalregistros = query.Count();
                objresponse.status.estado = 0;
                objresponse.status.mensaje = "Info de menu";


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
