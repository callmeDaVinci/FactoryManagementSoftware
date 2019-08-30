using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class planningActionBLL
    {
        public int planning_id { get; set; } = -1;
        public DateTime added_date { get; set; }
        public int added_by { get; set; } = 0;
        public string action { get; set; } = "";
        public string action_detail { get; set; } = "";
        public string action_from { get; set; } = "";
        public string action_to { get; set; } = "";
        public string note { get; set; } = "";
    }
}
