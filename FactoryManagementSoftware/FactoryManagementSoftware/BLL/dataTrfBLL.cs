using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class dataTrfBLL
    {
        public double index { get; set; }
        public string raw_mat { get; set; }
        public string part_code { get; set; }
        public string part_name { get; set; }
        public string color_mat { get; set; }
        public string color { get; set; }
        public float pw_per_shot { get; set; }
        public float rw_per_shot { get; set; }
        public float cavity { get; set; }
        public float ready_stock { get; set; }
        public float estimate { get; set; }
        public float deliveredOut { get; set; }
        public float outStd { get; set; }
        public float bal1 { get; set; }
        public float bal2 { get; set; }
        public float bal3 { get; set; }
        public float bal4 { get; set; }
        public float forecast1 { get; set; }
        public float forecast2 { get; set; }
        public float forecast3 { get; set; }
        public float forecast4 { get; set; }
        public int toProduce { get; set; }
        public int Produced { get; set; }
    }
}
