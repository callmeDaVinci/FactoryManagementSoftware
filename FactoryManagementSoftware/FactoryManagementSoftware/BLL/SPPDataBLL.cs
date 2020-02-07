using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    class SPPDataBLL
    {
        //common
        public int Table_Code { get; set; }

        //item table
        public string Item_code { get; set; }
        public string Item_name { get; set; }
        public int Color_tbl_code { get; set; }
        public int Ton_tbl_code { get; set; }
        public decimal Item_ct { get; set; }
        public decimal Item_shot_pw { get; set; }
        public decimal Item_shot_rw { get; set; }
        public int Item_cavity { get; set; }
        public int Size_tbl_code_1 { get; set; }
        public int Size_tbl_code_2 { get; set; }
        public int Size_tbl_code_3 { get; set; }
        public int Type_tbl_code { get; set; }
        public int Category_tbl_code { get; set; }

        //color table
        public string Color_name { get; set; }

        //ton table
        public int Ton_number { get; set; }

        //stdPacking table
        public int Qty_Per_Bag { get; set; }
        public int Qty_Per_Packet { get; set; }

        //size table
        public int Size_Numerator { get; set; }
        public int Size_Denominator { get; set; }
        public string Size_Unit { get; set; }

        //type table
        public string Type_Name { get; set; }
        public bool IsCommon { get; set; }

        //category table
        public string Category_Name { get; set; }

        //common
        public DateTime Updated_Date { get; set; }
        public int Updated_By { get; set; }
        public bool IsRemoved { get; set; }
    }
}
