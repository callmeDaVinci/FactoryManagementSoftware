using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class orderActionBLL
    {
        public int ord_id { get; set; }
        public string action { get; set; }
        public DateTime added_date { get; set; }
        public int added_by { get; set; }
    }
}
