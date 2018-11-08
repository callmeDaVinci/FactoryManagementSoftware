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
        public float forecast_one { get; set; }
        public float forecast_two { get; set; }
        public float forecast_three { get; set; }
        public string forecast_current_month { get; set; }
        public DateTime forecast_updated_date { get; set; }
        public int forecast_updated_by { get; set; }
    }
}
