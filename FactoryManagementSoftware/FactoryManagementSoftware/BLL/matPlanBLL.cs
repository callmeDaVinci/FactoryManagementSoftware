using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class matPlanBLL
    {
        public string mat_code { get; set; }
        public float plan_to_use { get; set; }
        public float mat_used { get; set; }
        public int plan_id { get; set; }
        public string mat_note { get; set; }
        public bool active { get; set; }
        public DateTime updated_date { get; set; }
        public int updated_by { get; set; }
    }
}
