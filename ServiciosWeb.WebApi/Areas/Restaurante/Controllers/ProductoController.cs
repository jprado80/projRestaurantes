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

        // GET: api/Producto
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Producto/5
        public string Get(int id)
        {
            return "value";
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
