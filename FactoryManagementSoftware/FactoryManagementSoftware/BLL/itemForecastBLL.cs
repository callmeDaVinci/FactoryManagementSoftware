using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class itemForecastBLL
    {
        public int cust_id { get; set; }
        public string item_code { get; set; }

        public int forecast_month { get; set; }
        public int forecast_year { get; set; }

        public float forecast_qty { get; set; }
        public float forecast_old_qty { get; set; }

        public int updated_by { get; set; }
        public DateTime updated_date { get; set; }
    }
}
