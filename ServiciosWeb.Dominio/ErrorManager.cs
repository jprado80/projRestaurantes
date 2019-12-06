using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
    public class ErrorManager
    {

        private int atr_codigo_error;
        private string atr_decripcion_error;
        private int atr_severidad_error;

        public ErrorManager()
        {

        }

        ~ErrorManager()
        {

        }

        public int codigo_error
        {
            get
            {
                return atr_codigo_error;
            }
            set
            {
                atr_codigo_error = value;
            }
        }

        public string decripcion_error
        {
            get
            {
                return atr_decripcion_error;
            }
            set
            {
                atr_decripcion_error = value;
            }
        }

        public int severidad_error
        {
            get
            {
                return atr_severidad_error;
            }
            set
            {
                atr_severidad_error = value;
            }
        }

    }//end ErrorManager
}
