using ServiciosWeb.Datos.Modelo;
using ServiciosWeb.Dominio.Request;
using ServiciosWeb.Dominio.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ServiciosWeb.WebApi.Areas.Seguridad.Controllers
{
    public class AutenticarController : ApiController
    {

        BDRestaurantesEntities BD = new BDRestaurantesEntities();

        // POST: api/ColaboradorAutenticacion
        public IHttpActionResult Post([FromBody]AutenticacionRequest  request)
        {
            AutenticacionResponse objresponse = new AutenticacionResponse();
            objresponse.status = new ProcesoResponse();

            try
            {

                var usuario = BD.t_usuario.FirstOrDefault(x => x.usua_email ==  request.CorreoElectronico 
                && request.Clave==x.usua_pass );

                if (usuario != null)
                {
                    if (usuario.usua_esta == false)
                    {
                        
                    objresponse.status.estado = 2;

                    objresponse.status.mensaje = "Su cuenta no esta activo";

                    }
                    else
                    {

                        objresponse.status.estado = 0;

                        objresponse.CodigoUsuario = usuario.usua_id;
                        objresponse.CorreoElectronico = usuario.usua_email;
                        objresponse.status.mensaje = "Autenticado";

                    }





                }
                else {
                    objresponse.CodigoUsuario = 0;
                    objresponse.status.estado = 1;
                    objresponse.status.mensaje="No autenticado";

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


   
    }
}
