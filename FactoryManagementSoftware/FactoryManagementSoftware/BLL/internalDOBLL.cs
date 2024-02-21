using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class internalDOBLL
    {
        public int tbl_code { get; set; }
        public int do_format_tbl_code { get; set; }
        public int running_no { get; set; }
        public string do_no_string { get; set; }
        public int company_tbl_code { get; set; }
        public int internal_from_address_tbl_code { get; set; }
        public int shipping_address_tbl_code { get; set; }
        public int billing_address_tbl_code { get; set; }
        public string shipping_method { get; set; }
        public DateTime delivery_date { get; set; }
        public bool isDraft { get; set; }
        public bool isProcessing { get; set; }
        public bool isCompleted { get; set; }
        public bool isCancelled { get; set; }
        public string remark { get; set; }
        public DateTime updated_date { get; set; }
        public int updated_by { get; set; }
    }
}
