﻿using ServiciosWeb.Dominio.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio.Modelo
{
   public  class GestionMenuModel
    {


        public MantenimientoMenuModel Menu { get; set; }
        public MantenimientoMenuDetalleModel MenuDetalle { get; set; }


    }
}