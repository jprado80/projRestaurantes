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
    public class MenuDetalleController : ApiController
    {

        BDRestaurantesEntities BD = new BDRestaurantesEntities();

        // GET: api/MenuDetalle
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public IHttpActionResult Get(int id)
        {
            ListaMenuDetalleResponse objresponse = new ListaMenuDetalleResponse();

            objresponse.status = new ProcesoResponse();
            objresponse.Hits = new List<Dominio.MenuDetalle>();



            try
            {

                var query = from md in BD.t_menudetalle
                            join p in BD.t_producto on md.prod_id equals p.prod_id 
                            where md.menu_id == id
                            select new
                            {
                                md.mede_id,
                                md.mede_disponible,
                                md.mede_precio,
                                md.menu_id,
                                md.prod_id,
                                p.prod_nombre,
                                p.prod_precio
                            };


                foreach (var item in query)
                {
                    objresponse.Hits.Add(new Dominio.MenuDetalle()
                    {
                        mede_disponible=item.mede_disponible,
                        mede_id=item.mede_id,
                        mede_precio=item.mede_precio,
                        menu_id=item.menu_id,
                        prod_id=item.prod_id,
                        pro_descripcion=item.prod_nombre
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

        public IHttpActionResult Post([FromBody]MenuDetalleRegistrarRequest request)
        {
            MenuDetalleRegistrarResponse objresponse = new MenuDetalleRegistrarResponse();
            objresponse.status = new ProcesoResponse();

            try
            {
                t_menudetalle tMenuDetalle = new t_menudetalle();
                tMenuDetalle.mede_disponible = request.MenuDetalle.mede_disponible;
                tMenuDetalle.mede_precio = request.MenuDetalle.mede_precio;
                tMenuDetalle.prod_id = request.MenuDetalle.prod_id;
                tMenuDetalle.menu_id = request.MenuDetalle.menu_id;

                BD.t_menudetalle.Add(tMenuDetalle);
                BD.SaveChanges();

                objresponse.MenuDetalle = request.MenuDetalle;
                objresponse.MenuDetalle.mede_id = request.MenuDetalle.mede_id;

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

        // PUT: api/MenuDetalle/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MenuDetalle/5
        public void Delete(int id)
        {


   

            try
            {


                t_menudetalle t_menuDetalle = BD.t_menudetalle.FirstOrDefault(x => x.mede_id == id);

                if (t_menuDetalle != null)
                {
                    BD.t_menudetalle.Remove(t_menuDetalle);
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
