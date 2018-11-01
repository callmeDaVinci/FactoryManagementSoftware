using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class itemBLL
    {
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string item_cat { get; set; }
        public float item_qty { get; set; }
        public float item_ord { get; set; }
        public string item_color { get; set; }
        public float item_part_weight { get; set; }
        public float item_runner_weight { get; set; }
        public DateTime item_added_date { get; set; }
        public int item_added_by { get; set; }
        public DateTime item_updtd_date { get; set; }
        public int item_updtd_by { get; set; }
    }
}
