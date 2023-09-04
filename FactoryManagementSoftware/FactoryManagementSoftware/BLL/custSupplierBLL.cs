using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class custSupplierBLL
    {
        public int cust_id { get; set; }
        public int supplier_id { get; set; }
        public string cust_name { get; set; }
        public string supplier_name { get; set; }
        public DateTime added_date { get; set; }
        public int added_by { get; set; }
        public DateTime updated_date { get; set; }
        public int updated_by { get; set; }
        public bool isRemoved { get; set; } = false;

        public int cust_main { get; set; }
        public DateTime cust_added_date { get; set; }
        public int cust_added_by { get; set; }
        public DateTime cust_updtd_date { get; set; }
        public int cust_updtd_by { get; set; }
    }
}
