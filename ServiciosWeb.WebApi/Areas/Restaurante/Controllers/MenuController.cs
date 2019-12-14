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

namespace ServiciosWeb.WebApi.Areas.Restaurante.Controllers
{
    public class MenuController : ApiController
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
        public IHttpActionResult Post([FromBody]MenuRegistrarRequest request)
        {

            MenuRegistrarResponse objresponse = new MenuRegistrarResponse();
            objresponse.status = new ProcesoResponse();


            try
            {

                t_menu tMenu = new t_menu();
                tMenu.menu_estado = request.Menu.menu_estado;
                tMenu.menu_nombre = request.Menu.menu_nombre;
                tMenu.menu_publicado = request.Menu.menu_publicado;
                tMenu.menu_ruc = request.Menu.menu_ruc;
                
                BD.t_menu.Add(tMenu);
                BD.SaveChanges();


                objresponse.Menu = request.Menu;
                objresponse.Menu.menu_id = tMenu.menu_id;



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

        // PUT: api/ListaPrecio/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ListaPrecio/5
        public void Delete(int id)
        {




            try
            {


                t_menu tMenu = BD.t_menu.FirstOrDefault(x => x.menu_id == id);

                if (tMenu != null)
                {
                    BD.t_menu.Remove(tMenu);
                }

                BD.SaveChanges();




            }
            catch (Exception err)
            {

                throw err;

            }



        }
    }
}
