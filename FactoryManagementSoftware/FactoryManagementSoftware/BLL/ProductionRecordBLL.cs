using System;

namespace FactoryManagementSoftware.BLL
{
    class ProductionRecordBLL
    {
        public int sheet_id { get; set; }
        public int plan_id { get; set; }
        public DateTime production_date { get; set; }
        public string shift { get; set; }
        public string production_lot_no { get; set; }
        public string raw_mat_lot_no { get; set; }
        public string color_mat_lot_no { get; set; }
        public int meter_start { get; set; }
        public int meter_end { get; set; }
        public int last_shift_balance { get; set; }
        public int current_shift_balance { get; set; }
        public int full_box { get; set; }
        public int total_produced { get; set; }
        public int total_reject { get; set; }
        public double reject_percentage { get; set; }
        public DateTime updated_date { get; set; }
        public int updated_by { get; set; }
        public bool active { get; set; }
        public string packaging_code { get; set; }
        public int packaging_qty { get; set; }

        public DateTime time { get; set; }
        public string production_operator { get; set; }
        public int meter_reading { get; set; }
    }
}
