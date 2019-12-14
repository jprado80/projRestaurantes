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
    public class ProductoController : ApiController
    {
        BDRestaurantesEntities BD = new BDRestaurantesEntities();

        public IHttpActionResult Get()
        {
            ListarProductoResponse objresponse = new ListarProductoResponse();

            objresponse.status = new ProcesoResponse();
            objresponse.Hits = new List<Dominio.Producto>();

            try
            {
                var query = BD.t_producto.ToList();

                foreach (var item in query)
                {
                    objresponse.Hits.Add(new Dominio.Producto()
                    {
                        prod_descrip = item.prod_descrip,
                        prod_nombre = item.prod_nombre,
                        prod_id = item.prod_id,
                        prod_precio = item.prod_precio,
                        rest_ruc = item.prod_nombre,
                        tico_descrip = item.prod_nombre,
                        tico_id = item.tico_id
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

        public IHttpActionResult Get(int id)
        {

            LeerProductoResponse objresponse = new LeerProductoResponse();

            objresponse.status = new ProcesoResponse();
            objresponse.Hit = new Dominio.Producto();

            try
            {
              

               
                    var item = BD.t_producto.Where(x => x.prod_id==id).FirstOrDefault();

                
                        objresponse.Hit =new Dominio.Producto()
                        {
                            prod_descrip = item.prod_nombre,
                            prod_nombre = item.prod_nombre,
                            prod_id = item.prod_id,
                            prod_precio = item.prod_precio,
                            rest_ruc = item.prod_nombre,
                            tico_descrip = item.prod_nombre,
                            tico_id = item.tico_id
                        };
                    

                
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

        [HttpGet]
        [Route("api/ProductosPorUsuario/{id}")]
        public IHttpActionResult ProductosPorUsuario(int id)
        {

            ListarProductoResponse objresponse = new ListarProductoResponse();

            objresponse.status = new ProcesoResponse();
            objresponse.Hits = new List<Dominio.Producto>();

            try
            {
                var restaurante = BD.t_restaurante.Where(z => z.usua_id == id).FirstOrDefault();


                if (restaurante != null)
                {
                    var query = BD.t_producto.Where(x => x.rest_ruc == restaurante.rest_ruc);

                    foreach (var item in query)
                    {
                        objresponse.Hits.Add(new Dominio.Producto()
                        {
                            prod_descrip = item.prod_descrip,
                            prod_nombre = item.prod_nombre,
                            prod_id = item.prod_id,
                            prod_precio = item.prod_precio,
                            rest_ruc = item.prod_nombre,
                            tico_descrip = item.prod_nombre,
                            tico_id = item.tico_id
                        });
                    }

                }







                objresponse.totalregistros = objresponse.Hits.Count();
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

        //INSERCIÖN
        [HttpPost]
        public IHttpActionResult Post(RegistrarProductoRequest request)
        {


            RegistrarProductoResponse objresponse = new RegistrarProductoResponse();
            objresponse.status = new ProcesoResponse();


            try
            {


                t_producto tProduc = new t_producto();

                tProduc.prod_descrip =request.Producto.prod_descrip;
                tProduc.prod_id = request.Producto.prod_id;
                tProduc.prod_nombre = request.Producto.prod_nombre;
                tProduc.prod_precio = request.Producto.prod_precio;
                tProduc.rest_ruc = request.Producto.rest_ruc;
                tProduc.tico_id = request.Producto.tico_id;



                BD.t_producto.Add(tProduc);


           

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



     
     
        public void Delete( int id)
        {


            RegistrarProductoResponse objresponse = new RegistrarProductoResponse();
            objresponse.status = new ProcesoResponse();


            try
            {


                t_producto tProduc = BD.t_producto.FirstOrDefault(x=>x.prod_id==id);



                if (tProduc != null) {
                    BD.t_producto.Remove(tProduc);
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

          

        }


        // PUT: api/Producto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Producto/5
  
    }
}
