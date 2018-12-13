using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class itemBLL
    {
        public string item_cat { get; set; }//item category
        public string item_material { get; set; } = null;//item raw material
        public string item_name { get; set; }
        public string item_code { get; set; }
        public string item_color { get; set; } = null;
        public int item_quo_ton { get; set; } = 0;//quotation ton
        public int item_best_ton { get; set; } = 0;
        public int item_pro_ton { get; set; } = 0;//item_pro_ton
        public string item_mb { get; set; } = null;
        public int item_quo_ct { get; set; } = 0;
        public int item_pro_ct_from { get; set; } = 0;
        public int item_pro_ct_to { get; set; } = 0;
        public float item_quo_pw_pcs { get; set; } = 0;
        public float item_quo_rw_pcs { get; set; } = 0;
        public float item_pro_pw_pcs { get; set; } = 0; //item_pro_pw_pcs
        public float item_pro_rw_pcs { get; set; } = 0;//item_pro_rw_pcs
        public float item_pro_pw_shot { get; set; } = 0;
        public float item_pro_rw_shot { get; set; } = 0;
        public int item_capacity { get; set; } = 0;
        public int item_pro_cooling { get; set; } = 0;
        public float item_wastage_allowed { get; set; } = 0;
        public float item_qty { get; set; }
        public float item_ord { get; set; }
        public DateTime item_added_date { get; set; }
        public int item_added_by { get; set; }
        public DateTime item_updtd_date { get; set; }
        public int item_updtd_by { get; set; }
    }
}
