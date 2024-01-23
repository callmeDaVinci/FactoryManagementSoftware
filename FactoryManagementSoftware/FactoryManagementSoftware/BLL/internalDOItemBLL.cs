using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class internalDOItemBLL
    {
        public int tbl_code { get; set; }
        public int internal_do_tbl_code { get; set; }
        public string item_code { get; set; }
        public decimal total_qty { get; set; }
        public string qty_unit { get; set; }
        public int qty_per_box { get; set; }
        public int box_qty { get; set; }
        public string box_unit { get; set; }
        public decimal balance_qty { get; set; }
        public bool search_mode { get; set; }
        public string item_description { get; set; }
        public string description { get; set; }
        public bool description_packing { get; set; }
        public bool description_category { get; set; }
        public bool description_remark { get; set; }
        public bool isRemoved { get; set; }

        public string remark { get; set; }
        public DateTime updated_date { get; set; }
        public int updated_by { get; set; }
    }
}
