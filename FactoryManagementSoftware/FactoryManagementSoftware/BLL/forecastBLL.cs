using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class forecastBLL
    {
        public string forecast_no { get; set; }
        public string item_code { get; set; }
        public float forecast_ready_stock { get; set; }
        public string forecast_current_month { get; set; }
        public float forecast_one { get; set; }
        public float forecast_two { get; set; }
        public float forecast_three { get; set; }
        public float forecast_out_stock { get; set; }
        public float forecast_osant { get; set; }
        public float forecast_shot_one { get; set; }
        public float forecast_shot_two { get; set; }
        public DateTime forecast_updtd_date { get; set; }
        public int forecast_updtd_by { get; set; }
    }
}
