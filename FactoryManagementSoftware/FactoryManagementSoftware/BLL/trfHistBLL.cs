using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class trfHistBLL
    {
        public int trf_hist_id { get; set; }
        public string trf_hist_item_code { get; set; }
        public string trf_hist_from { get; set; }
        public string trf_hist_to { get; set; }
        public float trf_hist_qty { get; set; }
        public string trf_hist_unit { get; set; }
        public DateTime trf_hist_trf_date { get; set; }
        public string trf_hist_note { get; set; }
        public DateTime trf_hist_added_date { get; set; }
        public int trf_hist_added_by { get; set; }
        public string trf_result { get; set; }
        public DateTime trf_hist_updated_date { get; set; }
        public int trf_hist_updated_by { get; set; }
        public int trf_hist_from_order { get; set; }

    }
}
