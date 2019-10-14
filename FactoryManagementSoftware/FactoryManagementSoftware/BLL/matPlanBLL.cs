using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class matPlanBLL
    {
        public string mat_code { get; set; }
        public string mat_name { get; set; }
        public string mat_cat { get; set; }
        public string part_code { get; set; }
        public string part_name { get; set; }
        public string pro_location { get; set; }
        public string pro_machine { get; set; }
        public string pro_max_qty { get; set; }
        public string pro_target_qty { get; set; }
        public string pro_start { get; set; }
        public string pro_end { get; set; }
        public float plan_to_use { get; set; }
        public float mat_used { get; set; }
        public int plan_id { get; set; }
        public string mat_note { get; set; }
        public bool active { get; set; }
        public float mat_preparing { get; set; }
        public float mat_transferred { get; set; }
        public string mat_from { get; set; }
        public DateTime updated_date { get; set; }
        public int updated_by { get; set; }
    }
}
