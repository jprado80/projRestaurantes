//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiciosWeb.Datos.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_usuario
    {
        public long usua_id { get; set; }
        public string usua_email { get; set; }
        public string usua_pass { get; set; }
        public string usua_nomb { get; set; }
        public string usua_dni { get; set; }
        public string usua_direc { get; set; }
        public Nullable<int> dist_id { get; set; }
        public string usua_refedirec { get; set; }
        public Nullable<System.DateTime> usua_fecnac { get; set; }
        public string usua_rutaimagen { get; set; }
        public Nullable<bool> usua_esta { get; set; }
    }
}
