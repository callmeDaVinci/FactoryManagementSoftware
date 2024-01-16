using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class companyBLL
    {
        public int tbl_code { get; set; }
        public int primary_billing_address_code { get; set; }
        public int primary_shipping_address_code { get; set; }
        public string full_name { get; set; }
        public string short_name { get; set; }
        public string registration_no { get; set; }
        public bool isInternal { get; set; }
        public string remark { get; set; }
        public bool isRemoved { get; set; }
        public int updated_by { get; set; }
        public DateTime updated_date { get; set; }
    }
}
