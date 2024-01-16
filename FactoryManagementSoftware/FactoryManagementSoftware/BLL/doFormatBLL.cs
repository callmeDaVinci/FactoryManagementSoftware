using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class doFormatBLL
    {
        public int tbl_code { get; set; }
        public int letter_head_tbl_code { get; set; }
        public int sheet_format_tbl_code { get; set; }
        public string do_type { get; set; }
        public string no_format { get; set; }
        public string prefix { get; set; }
        public string date_format { get; set; }
        public string suffix { get; set; }
        public int running_number_length { get; set; }
        public int next_start_number { get; set; }
        public string reset_running_number { get; set; }
        public bool isInternal { get; set; }
        public string remark { get; set; }
        public bool isRemoved { get; set; }
        public int updated_by { get; set; }
        public DateTime updated_date { get; set; }
    }
}
