using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class historyBLL
    {
        public int history_id { get; set; }
        public DateTime history_date { get; set; }
        public int history_by { get; set; }
        public string history_action { get; set; }
        public string history_detail { get; set; }
    }
}
