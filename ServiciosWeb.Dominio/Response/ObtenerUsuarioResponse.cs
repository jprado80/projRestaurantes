﻿using ServiciosWeb.Dominio;
using ServiciosWeb.Dominio.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiciosWeb.DominioResponse
{
    public class ObtenerUsuarioResponse : Respuesta
    {
        public Usuario Usuario { get; set; }
        public Restaurante Restaurante { get; set; }


    }
}
