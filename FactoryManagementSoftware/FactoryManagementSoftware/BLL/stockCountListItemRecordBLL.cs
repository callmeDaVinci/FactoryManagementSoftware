using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class stockCountListItemRecordBLL
    {
        public int tbl_code { get; set; }
        public int list_item_tbl_code { get; set; }
        public int total_unit_qty { get; set; }
        public string count_unit { get; set; }
        public double unit_conversion_rate { get; set; }
        public double total_pcs { get; set; }
        public string remark { get; set; }
        public int updated_by { get; set; }
        public DateTime updated_date { get; set; }
        public DateTime stock_count_date { get; set; }
    }
}
