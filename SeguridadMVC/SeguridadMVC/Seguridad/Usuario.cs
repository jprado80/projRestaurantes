using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeguridadMVC.Seguridad
{
    public class Usuario
    {
        private long idUsuario;
        private string codigousuario;
        private string nombreusuario;
        private string emailusuario;
        private DateTime fecharegistro;
        private bool isapproved;
        private bool islockedout;
        private bool isonline;
        private DateTime lastactivitydate;
        private DateTime lastlockeddate;
        private DateTime lastlogindate;
        private int idusuariopwd;
        private string recordatoriopwd;
  


        public Usuario
        (
            long IdUsuario,
            string NombreUsuario,
            string ApellidoUsuario,
            string pCodigoUsuario,
            string EmailUsuario,
            int? UsuarioAprobado,
            int? UsuarioBloqueado,
            int? UsuarioOnline,
            DateTime? FechaRegistro,
            DateTime? UltimaFechaBloqueo,
            DateTime? UltimaFechaLogin
        )
        {
            this.codigousuario = pCodigoUsuario;
            this.nombreusuario = string.Format("{0} {1}", NombreUsuario, ApellidoUsuario);
            this.emailusuario = EmailUsuario;
            this.fecharegistro = FechaRegistro.HasValue ? (DateTime)FechaRegistro : DateTime.Now;
            this.isapproved = UsuarioAprobado.HasValue ? Convert.ToBoolean(UsuarioAprobado) : false;
            this.islockedout = UsuarioBloqueado.HasValue ? Convert.ToBoolean(UsuarioBloqueado) : false;
            this.isonline = UsuarioOnline.HasValue ? Convert.ToBoolean(UsuarioOnline) : false;
            this.lastactivitydate = DateTime.Now;
            this.lastlockeddate = UltimaFechaBloqueo.HasValue ? Convert.ToDateTime(UltimaFechaBloqueo) : new DateTime(1900, 1, 1);
            this.lastlogindate = UltimaFechaLogin.HasValue ? Convert.ToDateTime(UltimaFechaLogin) : DateTime.Now;
            this.idUsuario = IdUsuario;



        }


        public Usuario
        (
            long IdUsuario,
            string NombreUsuario,

            string EmailUsuario
   

        )
        {
            
            this.nombreusuario = string.Format("{0}", NombreUsuario);
            this.emailusuario = EmailUsuario;
            
            this.lastactivitydate = DateTime.Now;
            
            this.idUsuario = IdUsuario;



        }


        public long Idusuario { get { return idUsuario; } }
        public string CodigoUsuario { get { return codigousuario; } }
        public string NombreUsuario { get { return nombreusuario; } }
        public string EmailUsuario { get { return emailusuario; } }
        public DateTime FechaRegistro { get { return fecharegistro; } }
        public bool IsApproved { get { return isapproved; } }
        public bool IsLockedout { get { return islockedout; } }
        public bool IsOnline { get { return isonline; } }
        public DateTime LastActivityDate { get { return lastactivitydate; } }
        public DateTime LastLockedOutDate { get { return lastlockeddate; } }
        public DateTime LastLoginDate { get { return lastlogindate; } }
        public int IdUsuarioPwd { get { return idusuariopwd; } }
        public string RecordatorioPwd { get { return recordatoriopwd; } }

        
    }
}