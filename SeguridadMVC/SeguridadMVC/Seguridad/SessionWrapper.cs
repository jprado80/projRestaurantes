using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeguridadMVC.Seguridad
{
    public class SessionWrapper
    {
        const string InfoPerfil = "SessionWrapperPerfil";
        const string InfoSesion = "SessionWrapperSesion";
        const string InfoUsuario = "SessionWrapperUsuario";
        const string InfoPrimeraConexion = "SessionWrapperConexion";


        public Perfil Perfil
        {
            get
            {
                if (null != HttpContext.Current.Session[InfoPerfil])
                    return HttpContext.Current.Session[InfoPerfil] as Perfil;
                return null;
            }
            set
            {
                HttpContext.Current.Session[InfoPerfil] = value;
            }
        }


        public Session Session
        {
            get
            {
                if (null != HttpContext.Current.Session[InfoSesion])
                    return HttpContext.Current.Session[InfoSesion] as Session;
                return null;
            }
            set
            {
                HttpContext.Current.Session[InfoSesion] = value;
            }
        }

        public Usuario Usuario
        {
            get
            {
                if (null != HttpContext.Current.Session[InfoUsuario])
                    return HttpContext.Current.Session[InfoUsuario] as Usuario;
                return null;
            }
            set
            {
                HttpContext.Current.Session[InfoUsuario] = value;
            }
        }

        public bool PrimeraConexion
        {
            get
            {
                if (null != HttpContext.Current.Session[InfoPrimeraConexion])
                    return Convert.ToBoolean(HttpContext.Current.Session[InfoPrimeraConexion]);
                return false;
            }
            set
            {
                HttpContext.Current.Session[InfoPrimeraConexion] = value;
            }
        }
    }
}