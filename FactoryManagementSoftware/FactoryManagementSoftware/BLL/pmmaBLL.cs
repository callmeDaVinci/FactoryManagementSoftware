using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class pmmaBLL
    {
        public string pmma_item_code { get; set; }
        public DateTime pmma_date { get; set; }
        public float pmma_openning_stock { get; set; }
        public float pmma_bal_stock { get; set; }
        public float pmma_percentage { get; set; }
        public DateTime pmma_added_date { get; set; }
        public int pmma_added_by { get; set; }
        public DateTime pmma_updated_date { get; set; }
        public int pmma_updated_by { get; set; }
    }
}
