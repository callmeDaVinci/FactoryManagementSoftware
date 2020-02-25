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

        //customer table
        public string Full_Name { get; set; }
        public string Short_Name { get; set; }
        public string Registration_No { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string Address_City { get; set; }
        public string Address_State { get; set; }
        public string Address_Postal_Code { get; set; }
        public string Address_Country { get; set; }
        public string Fax { get; set; }
        public string Phone_1 { get; set; }
        public string Phone_2 { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        //po table
        public int PO_code { get; set; }
        public string PO_no { get; set; }
        public DateTime PO_date { get; set; }
        public int Customer_tbl_code { get; set; }
        public int PO_qty { get; set; }
        public int Delivered_qty { get; set; }
        public bool DefaultShippingAddress { get; set; }
        public string PO_note { get; set; }
        public int To_delivery_qty { get; set; }

        //do table
        public int DO_no { get; set; }
        public int PO_tbl_code { get; set; }
        public int DO_to_delivery_qty { get; set; }
        public DateTime DO_date { get; set; }
        public bool IsDelivered { get; set; }

        //common
        public DateTime Updated_Date { get; set; }
        public int Updated_By { get; set; }
        public bool IsRemoved { get; set; }

        
    }
}
