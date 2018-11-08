using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class joinBLL
    {
        public string join_parent_code { get; set; }
        public string join_child_code { get; set; }
        public int join_qty { get; set; }
        public DateTime join_added_date { get; set; }
        public int join_added_by { get; set; }      
    }
}
