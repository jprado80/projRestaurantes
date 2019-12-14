using ServiciosWeb.Datos.Modelo;
using ServiciosWeb.Dominio.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiciosWeb.WebApi.Areas.Restaurante.Controllers
{
    public class MenuPublicarController : ApiController
    {
        BDRestaurantesEntities BD = new BDRestaurantesEntities();

        // GET: api/MenuPublicar
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MenuPublicar/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MenuPublicar
        public void Post([FromBody]MenuRegistrarRequest request)
        {



            try
            {


                t_menu tMenu = BD.t_menu.FirstOrDefault(x => x.menu_id == request.Menu.menu_id);
                tMenu.menu_publicado = request.Menu.menu_publicado;
                BD.SaveChanges();


            }
            catch (Exception err)
            {

                throw err;

            }

        }

        // PUT: api/MenuPublicar/5
        public void Put(int id, [FromBody]string value)
        {


        }

        // DELETE: api/MenuPublicar/5
        public void Delete(int id)
        {
        }
    }
}
