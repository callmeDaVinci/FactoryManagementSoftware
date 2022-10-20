using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class BalForecastBLL
    {
        public int Cust_ID { get; set; }
        public string Cust_Name { get; set; }
        public DateTime Date_From { get; set; }
        public DateTime Date_To { get; set; }
        public int Month_From { get; set; }
        public int Month_To { get; set; }
        public int Year_To { get; set; }
        public int Year_From { get; set; }
        public string Item_Type { get; set; }
        public bool Deduct_Delivered { get; set; }
        public bool ZeroCost_Material { get; set; }
        public bool ZeroCost_Stock { get; set; }
        public bool Deduct_Stock { get; set; }


    }
}
