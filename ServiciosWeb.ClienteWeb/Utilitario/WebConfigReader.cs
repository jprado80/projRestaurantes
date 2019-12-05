namespace SeServiciosWeb.ClienteWeb.Utilitario
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    public sealed class WebConfigReader
    {
        public sealed class Mailer
        {
            public static string Body { get { return Convert.ToString(ConfigurationManager.AppSettings["Body"]); } }
            public static string From { get { return Convert.ToString(ConfigurationManager.AppSettings["From"]); } }
            public static string Host { get { return Convert.ToString(ConfigurationManager.AppSettings["Host"]); } }
            public static int Port { get { return Convert.ToInt32(ConfigurationManager.AppSettings["Port"]); } }
            public static bool EnabledSSL { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["EnabledSSL"]); } }
            public static bool UseDefaultCredentials { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]); } }
            public static string CredentialsUser { get { return Convert.ToString(ConfigurationManager.AppSettings["CredentialsUser"]); } }
            public static string CredentialsClave { get { return Convert.ToString(ConfigurationManager.AppSettings["CredentialsClave"]); } }

        }

        public static class Log
        {
            public static string MainPath { get { return ConfigurationManager.AppSettings["logMainPath"]; } }
            public static string NameExceptionLog { get { return ConfigurationManager.AppSettings["logNameExceptionLog"]; } }
        }
        public static class Variables
        {
            public static string IdSucursal { get { return ConfigurationManager.AppSettings["OrdenSalidaIdSucursal"]; } }
        }
        public sealed class ServicesHost
        {}

         public static class Parametros
        {
             public static string codigo_rol_administrador { get { return ConfigurationManager.AppSettings["Codigo_Rol_Administrador"]; } }
        }
        
    }
}
