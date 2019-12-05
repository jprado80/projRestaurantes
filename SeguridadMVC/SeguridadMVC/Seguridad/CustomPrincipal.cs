using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using SeguridadMVC.Seguridad;

namespace SeguridadMVC.Seguridad
{

    [Serializable]
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(CustomIdentity identity)
        {
            Identity = identity;
        }
        public IIdentity Identity { get; private set; }
        public CustomIdentity CustomIdentity { get { return (CustomIdentity)Identity; } set { Identity = value; } }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}