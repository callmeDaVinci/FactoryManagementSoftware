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
        public float pmma_in { get; set; } = 0;
        public float pmma_out { get; set; } = 0;
        public float pmma_direct_out { get; set; }
        public float pmma_bal_stock { get; set; }
        public float pmma_wastage { get; set; }
        public float pmma_adjust { get; set; } = 0;
        public string pmma_note { get; set; } = "";

        public string pmma_from { get; set; }
        public string pmma_to { get; set; }

        public string pmma_month { get; set; }
        public string pmma_year { get; set; }
        public string pmma_supplier { get; set; }

        public DateTime pmma_added_date { get; set; }
        public int pmma_added_by { get; set; }
        public DateTime pmma_updated_date { get; set; }
        public int pmma_updated_by { get; set; }

        public bool pmma_lock { get; set; }

    }
}
