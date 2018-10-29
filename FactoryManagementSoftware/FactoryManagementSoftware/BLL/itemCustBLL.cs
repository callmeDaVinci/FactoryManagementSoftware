using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class itemCustBLL
    {
        public string item_code { get; set; }
        public int cust_id { get; set; }
        public DateTime item_cust_added_date { get; set; }
        public int item_cust_added_by { get; set; }
    }
}
