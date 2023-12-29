using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class facStockBLL
    {
        public string stock_item_code { get; set; }
        public int stock_fac_id { get; set; }
        public float stock_qty { get; set; }
        public string stock_cat { get; set; }
        public string stock_unit { get; set; }
        public DateTime stock_updtd_date { get; set; }
        public int stock_updtd_by { get; set; }
    }
}
