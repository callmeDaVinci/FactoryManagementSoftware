using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class pmmaDateBLL
    {
        public int month { get; set; }
        public int year { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }
        public int updated_by { get; set; }
        public DateTime updated_date { get; set; }
    }
}
