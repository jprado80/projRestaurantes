using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWeb.Dominio
{
    public class DataTableRequest
    {

        private int atr_iDisplayLength;
        private int atr_iDisplayStart;
        private int atr_iSortCol_0;
        private string atr_sEcho;
        private string atr_sSearch;
        private string atr_sSortDir;

        public DataTableRequest()
        {

        }

        ~DataTableRequest()
        {

        }

        public int iDisplayLength
        {
            get
            {
                return atr_iDisplayLength;
            }
            set
            {
                atr_iDisplayLength = value;
            }
        }

        public int iDisplayStart
        {
            get
            {
                return atr_iDisplayStart;
            }
            set
            {
                atr_iDisplayStart = value;
            }
        }

        public int iSortCol_0
        {
            get
            {
                return atr_iSortCol_0;
            }
            set
            {
                atr_iSortCol_0 = value;
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

        public string sSearch
        {
            get
            {
                return atr_sSearch;
            }
            set
            {
                atr_sSearch = value;
            }
        }

        public string sSortDir
        {
            get
            {
                return atr_sSortDir;
            }
            set
            {
                atr_sSortDir = value;
            }
        }

    }//end DataTableRequest
}
