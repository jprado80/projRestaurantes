using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
    public class DataTableResponse
    {

        private Object atr_aaData;
        private int? atr_iTotalDisplayRecords;
        private int? atr_iTotalRecords;
        private string atr_sEcho;

        public DataTableResponse()
        {

        }

        ~DataTableResponse()
        {

        }

        public int? iTotalDisplayRecords
        {
            get
            {
                return atr_iTotalDisplayRecords;
            }
            set
            {
                atr_iTotalDisplayRecords = value;
            }
        }

        public int? iTotalRecords
        {
            get
            {
                return atr_iTotalRecords;
            }
            set
            {
                atr_iTotalRecords = value;
            }
        }

        public string sEcho
        {
            get
            {
                return atr_sEcho;
            }
            set
            {
                atr_sEcho = value;
            }
        }

        public Object aaData
        {
            get
            {
                return atr_aaData;
            }
            set
            {
                atr_aaData = value;
            }
        }
    }//end DataTableResponse
}
