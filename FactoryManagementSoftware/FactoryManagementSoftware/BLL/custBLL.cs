using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class custBLL
    {
        public int cust_id { get; set; }
        public string cust_name { get; set; }
        public int cust_main { get; set; }
        public DateTime cust_added_date { get; set; }
        public int cust_added_by { get; set; }
        public DateTime cust_updtd_date { get; set; }
        public int cust_updtd_by { get; set; }
    }
}
