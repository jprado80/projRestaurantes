using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
    public class OperacionType
    {

        private string atr_codigo_operacion;
        private ErrorManager atr_ErrorManager;
        private string atr_estado_operacion;
        private string atr_mensaje_operacion;
        private string atr_nombre_operacion;
        private Object atr_Objeto1;
        private Object atr_Objeto2;
        private Object atr_Objeto3;
        private Object atr_Objeto4;

        public OperacionType()
        {

        }

        ~OperacionType()
        {

        }

        public string codigo_operacion
        {
            get
            {
                return atr_codigo_operacion;
            }
            set
            {
                atr_codigo_operacion = value;
            }
        }

        public ErrorManager ErrorManager
        {
            get
            {
                return atr_ErrorManager;
            }
            set
            {
                atr_ErrorManager = value;
            }
        }

        public string estado_operacion
        {
            get
            {
                return atr_estado_operacion;
            }
            set
            {
                atr_estado_operacion = value;
            }
        }

        public string mensaje_operacion
        {
            get
            {
                return atr_mensaje_operacion;
            }
            set
            {
                atr_mensaje_operacion = value;
            }
        }

        public string nombre_operacion
        {
            get
            {
                return atr_nombre_operacion;
            }
            set
            {
                atr_nombre_operacion = value;
            }
        }

        public Object Objeto1
        {
            get
            {
                return atr_Objeto1;
            }
            set
            {
                atr_Objeto1 = value;
            }
        }

        public Object Objeto2
        {
            get
            {
                return atr_Objeto2;
            }
            set
            {
                atr_Objeto2 = value;
            }
        }

        public Object Objeto3
        {
            get
            {
                return atr_Objeto3;
            }
            set
            {
                atr_Objeto3 = value;
            }
        }

        public Object Objeto4
        {
            get
            {
                return atr_Objeto4;
            }
            set
            {
                atr_Objeto4 = value;
            }
        }

    }//end OperacionType
}
