using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioCommon
{
   public static class Parametros
    {

        public static string URLServicio = ConfigurationManager.AppSettings["URI"].ToString();
        public static string COLA_RESTAURANTE_QUEUE = ConfigurationManager.AppSettings["Queue"].ToString();


    }
}
