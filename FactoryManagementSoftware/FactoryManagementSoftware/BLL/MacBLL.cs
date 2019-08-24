using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class MacBLL
    {
        public int mac_id { get; set; }
        public string mac_name { get; set; }
        public int mac_ton { get; set; }
        public string mac_location { get; set; }

        public int mac_added_by { get; set; }
        public DateTime mac_added_date { get; set; }
        public int mac_updated_by { get; set; }
        public DateTime mac_updated_date { get; set; }
    }
}
