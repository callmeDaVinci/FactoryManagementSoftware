using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class itemBLL
    {
        public int item_ready_stock { get; set; }
        public int standard_packing_qty { get; set; }

        public string mat_code { get; set; }//item category
        public float mat_ratio { get; set; }

        public string item_cat { get; set; }//item category
        public string item_material { get; set; } = null;//item raw material
        public string item_recycle { get; set; } = null;
        public string item_name { get; set; }
        public string item_code { get; set; }
        public string packaging_code { get; set; }
        public string packaging_name { get; set; }
        public decimal item_min_stock_level { get; set; }

        public string item_code_present { get; set; }
        public string mould_code { get; set; }
        public int combination_code { get; set; }
        public string item_remark { get; set; }
        public string item_color { get; set; } = null;
        public int item_quo_ton { get; set; } = 0;//quotation ton
        public int item_best_ton { get; set; } = 0;
        public int item_pro_ton { get; set; } = 0;//item_pro_ton
        public string item_mb { get; set; } = null;
        public int item_quo_ct { get; set; } = 0;
        public int item_pro_ct_from { get; set; } = 0;
        public int item_pro_ct_to { get; set; } = 0;
        public int mould_ct { get; set; } = 0;
        public float item_mb_rate { get; set; } = 0;
        public bool raw_color { get; set; }
        public bool recycle_color { get; set; }
        public bool MouldDefaultSelection { get; set; } = false;
        public bool removed { get; set; } = false;
        public string item_unit { get; set; }
        public float unit_to_pcs_rate { get; set; } = 1;

        public float item_raw_ratio { get; set; } = 0;
        public float item_recycle_ratio { get; set; } = 0;

        public float item_quo_pw_pcs { get; set; } = 0;
        public float item_quo_rw_pcs { get; set; } = 0;
        public float item_pro_pw_pcs { get; set; } = 0; //item_pro_pw_pcs
        public float item_pro_rw_pcs { get; set; } = 0;//item_pro_rw_pcs
        public float item_pro_pw_shot { get; set; } = 0;
        public float item_pro_rw_shot { get; set; } = 0;

        public float item_pw_shot { get; set; } = 0;
        public float item_rw_shot { get; set; } = 0;

        public int item_cavity { get; set; } = 0;
        public int mould_cavity { get; set; } = 0;
        public int item_pro_cooling { get; set; } = 0;
        public float item_wastage_allowed { get; set; } = 0;
        
        public float item_ord { get; set; }
        public DateTime item_added_date { get; set; }

        public DateTime updated_date { get; set; }
        public int updated_by { get; set; }

        public DateTime added_date { get; set; }
        public int added_by { get; set; }
        public int mat_formula_group { get; set; }
        public float kg_per_bag { get; set; }

        public int item_added_by { get; set; }
        public DateTime item_updtd_date { get; set; }
        public int item_updtd_by { get; set; }

        public DateTime item_current_month { get; set; }
        public float item_last_qty { get; set; }
        public float item_qty { get; set; }
        public float item_last_pmma_qty { get; set; }
        public float item_last_permabonn_qty { get; set; }
        public float item_pmma_qty { get; set; }
        public float item_permabonn_qty { get; set; }

        public int item_assembly { get; set; }
        public int item_production { get; set; }
        public int item_sbb { get; set; }

        public int tbl_code { get; set; }
        public int Size_tbl_code_1 { get; set; }
        public int Size_tbl_code_2 { get; set; }
        public int Size_tbl_code_3 { get; set; }
        public int Type_tbl_code { get; set; }
        public int Category_tbl_code { get; set; }
        public int Stdpacking_tbl_code { get; set; }
    }
}
