﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio.Request
{
   public class ListarProductoRequest : Request
    {
        
        public Int32 prm_reginicio { get; set; }
        public Int32 prm_regfin { get; set; }
 

    }
}