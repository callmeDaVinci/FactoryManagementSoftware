using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class AddressBookBLL
    {
        public int tbl_code { get; set; }
        public int cust_tbl_code { get; set; }
        public int route_tbl_code { get; set; }
        public string full_name { get; set; }
        public string short_name { get; set; }
        public string registration_no { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string address_3 { get; set; }
        public string address_state { get; set; }
        public string address_postal_code { get; set; }
        public string address_country { get; set; }
        public string fax_no { get; set; }
        public string contact_number_1 { get; set; }
        public string contact_name_1 { get; set; }
        public string contact_number_2 { get; set; }
        public string contact_name_2 { get; set; }
        public string email_address { get; set; }
        public string website { get; set; }
        public string remark { get; set; }
        public bool isRemoved { get; set; }
        public int updated_by { get; set; }

        public DateTime updated_date { get; set; }
      

    }
}
