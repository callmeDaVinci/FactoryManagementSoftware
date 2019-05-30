using System;

namespace FactoryManagementSoftware.BLL
{
    class orderActionBLL
    {
        public int ord_id { get; set; } = -1;
        public int trf_id { get; set; }
        public DateTime added_date { get; set; }
        public int added_by { get; set; } = 0;
        public string action { get; set; } = "";
        public string action_detail { get; set; } = "";
        public string action_from { get; set; } = "";
        public string action_to { get; set; } = "";
        public string note { get; set; } = "";
        public bool active { get; set; } = true;

    }
}
