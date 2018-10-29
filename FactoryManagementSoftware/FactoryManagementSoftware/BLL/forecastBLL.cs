using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class forecastBLL
    {
        public int cust_id { get; set; }
        public string item_code { get; set; }
        public float forecast_one { get; set; }
        public float forecast_two { get; set; }
        public float forecast_three { get; set; }
        public DateTime forecast_updtd_date { get; set; }
        public int forecast_updtd_by { get; set; }
    }
}
