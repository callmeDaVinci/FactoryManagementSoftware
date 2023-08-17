using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class joinBLL
    {
        public string join_parent_code { get; set; }
        public string join_parent_name { get; set; }
        public string join_child_code { get; set; }
        public string join_child_name { get; set; }
        public float join_qty { get; set; }
        
        public int join_max { get; set; }
        public int join_min { get; set; }

        public bool join_main_carton { get; set; }
        public bool join_stock_out { get; set; }


        public DateTime join_added_date { get; set; }
        public int join_added_by { get; set; }

        public DateTime join_updated_date { get; set; }
        public int join_updated_by { get; set; }
    }
}
