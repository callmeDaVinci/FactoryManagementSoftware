using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class stockCountListBLL
    {
        public int tbl_code { get; set; }
        public string list_description { get; set; }
        public int default_factory_tbl_code { get; set; }
        public string remark { get; set; }
        public bool isRemoved { get; set; }
        public int updated_by { get; set; }
        public DateTime updated_date { get; set; }
    }
}
