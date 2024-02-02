using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class stockCountListItemBLL
    {
        public int tbl_code { get; set; }
        public int list_tbl_code { get; set; }
        public int index_level { get; set; }
        public string item_code { get; set; }
        public int factory_tbl_code { get; set; }
        public int default_out_tbl_code { get; set; }
        public int default_in_tbl_code { get; set; }
        public string count_unit { get; set; }
        public double unit_conversion_rate { get; set; }
        public string remark { get; set; }
        public bool isRemoved { get; set; }
        public int updated_by { get; set; }
        public DateTime updated_date { get; set; }
    }
}
