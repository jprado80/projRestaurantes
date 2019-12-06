using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
    public class ResponseOperacionBE
    {

        private OperacionType attr_OperacionType;
        private OperacionType m_OperacionType;
        private DataTableResponse atr_DataTableResponse;

        public ResponseOperacionBE()
        {

        }

        ~ResponseOperacionBE()
        {

        }

        public OperacionType OperacionType
        {
            get
            {
                return attr_OperacionType;
            }
            set
            {
                attr_OperacionType = value;
            }
        }


        public DataTableResponse DataTableResponse
        {
            get
            {
                return atr_DataTableResponse;
            }
            set
            {
                atr_DataTableResponse = value;
            }
        }


    }

}
