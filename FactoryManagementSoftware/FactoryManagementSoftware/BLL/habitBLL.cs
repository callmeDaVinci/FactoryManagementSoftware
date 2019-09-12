using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class habitBLL
    {
        public string belong_to { get; set; }
        public string habit_name { get; set; }
        public string habit_data { get; set; }
        public DateTime added_date { get; set; }
        public int added_by { get; set; }
        public DateTime updated_date { get; set; }
        public int updated_by { get; set; }
    }
}
