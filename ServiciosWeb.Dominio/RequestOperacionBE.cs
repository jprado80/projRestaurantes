using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
    public class RequestOperacionBE
    {

 
        private OperacionType m_OperacionType;
        private DataTableRequest atr_DataTableRquest;

        public RequestOperacionBE()
        {

        }

        ~RequestOperacionBE()
        {

        }

        public OperacionType OperacionType
        {
            get
            {
                return m_OperacionType;
            }
            set
            {
                m_OperacionType = value;
            }
        }

        public DataTableRequest DataTableRquest
        {
            get
            {
                return atr_DataTableRquest;
            }
            set
            {
                atr_DataTableRquest = value;
            }
        }

    }//end RequestOperacionBE
}
