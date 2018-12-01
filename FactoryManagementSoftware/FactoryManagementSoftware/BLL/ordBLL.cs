using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class ordBLL
    {
        public int ord_id { get; set; }
        public string ord_item_code { get; set; }
        public float ord_qty { get; set; } = 0;
        public float ord_pending { get; set; } = 0;
        public float ord_received { get; set; } = 0;
        public string ord_unit { get; set; }
        public string ord_to { get; set; } = "";
        public string ord_status { get; set; }
        public DateTime ord_required_date { get; set; }
        public DateTime ord_added_date { get; set; }
        public int ord_added_by { get; set; }
        public string ord_note { get; set; }
        public DateTime ord_updated_date { get; set; }
        public int ord_updated_by { get; set; }
    }
}
