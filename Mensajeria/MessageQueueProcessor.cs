using MessageQueueProcessor.Utilitario;

using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Messaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mensajeria
{

    [Transaction(TransactionOption.Required)]
    [ObjectPooling(true)]
    [Serializable()]
    [JustInTimeActivation(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Synchronization(SynchronizationOption.Required)]
    [ComVisible(true)]
    public class MessageQueueProcessor : ServicedComponent
    {
    
        public void ProcessMessage(object MsgBody, string MQPath)
        {
            try
            {

                //// get the response message queue path from the response format name
                //// string MQResponsePath = RespFormatName.Split(':')[1];
                //// open the respone message queue
                //MessageQueue MQueue = new MessageQueue(MQPath);
                //// create a message out of the message body we got passed along
                //// so we can read the message body in the format originally sent
                //Message Msg = new Message();

                //Msg.BodyStream = new System.IO.MemoryStream((Byte[])MsgBody);
                //Msg.Formatter = new XmlMessageFormatter(new Type[] { typeof(CorreoAltaUsuario) });
                //// gets the Order from the message body passed along
                //CorreoAltaUsuario TheOrder = (CorreoAltaUsuario)Msg.Body;
                //// send the order unprocessed to the response message queue
                //// specified by the received message (so by the message sender)
                //MQueue.Send(TheOrder);
                //// close the response message queue again






                System.Messaging.MessageQueue mq;

                if (MessageQueue.Exists(@".\Private$\RestauranteQueue"))
                    mq = new System.Messaging.MessageQueue(@".\Private$\RestauranteQueue");
                else
                    mq = MessageQueue.Create(@".\Private$\RestauranteQueue");

                System.Messaging.Message mes;
                string m;


                mes = mq.Receive(new TimeSpan(0, 0, 3));
                mes.Formatter = new XmlMessageFormatter(new Type[] { typeof(CorreoAltaUsuario) });
                CorreoAltaUsuario TheOrder = (CorreoAltaUsuario)mes.Body;




                //Mailer CorreoSolicitud = new Mailer();
                //List<String> listCorreso = new List<string>();
                //listCorreso.Add(TheOrder.CorreoUsuario);
                //CorreoSolicitud.Notificacion.CorreosPara = listCorreso;
                //CorreoSolicitud.Notificacion.ConCopia = "";
                //CorreoSolicitud.Notificacion.Asunto = TheOrder.Asunto;
                //CorreoSolicitud.Notificacion.Cuerpo = TheOrder.Mensaje;
                //CorreoSolicitud.Enviar();


                //MQueue.Close();
            }
            // catch any exception to keep the MSMQ trigger service ok
            catch (Exception)
            {


            }
        }
    }

}