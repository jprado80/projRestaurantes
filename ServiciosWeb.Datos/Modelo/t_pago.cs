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
    
    public partial class t_pago
    {
        public long pago_id { get; set; }
        public Nullable<System.DateTime> pago_fechhora { get; set; }
        public string pago_nrotarjeta { get; set; }
        public Nullable<int> pago_codseguridad { get; set; }
        public Nullable<decimal> pago_monto { get; set; }
        public Nullable<long> pedi_id { get; set; }
        public Nullable<long> usu_id { get; set; }
        public Nullable<int> ticta_id { get; set; }
        public string uscta_numero { get; set; }
        public string uscta_titular { get; set; }
    }
}
