using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.BLL
{
    public class SBBDataBLL
    {
        //common
        public int Table_Code { get; set; }

        //item table
        public string Item_code { get; set; }
        public string Item_tbl_code { get; set; }
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

        //mould table
        public string Mould_tbl_code { get; set; }
        public string Mould_code { get; set; }
        public string Mould_name { get; set; }
        public int Mould_ton { get; set; }
        public float Mould_width { get; set; }
        public float Mould_height { get; set; }
        public float Mould_length { get; set; }
        public DateTime Mould_startdate { get; set; }
        public float Mould_cycletime { get; set; }
        public float Mould_pwpershot { get; set; }
        public float Mould_rwpershot { get; set; }
        public int Mould_Total_cavity { get; set; }
        public int Mould_item_cavity { get; set; }
        public string Group_code { get; set; }


        //route table
        public string Route_name { get; set; }

        //delivery table
        public int Route_tbl_code { get; set; }
        public int Trip_no { get; set; }
        public DateTime? Delivery_date{ get; set; }
        public string Delivery_status { get; set; }
        public int Deliver_pcs { get; set; }
        public int DO_tbl_code { get; set; }
        public int Planning_no { get; set; }
        public bool Cust_Own_DO { get; set; }

        //color table
        public string Color_name { get; set; }

        //ton table
        public int Ton_number { get; set; }

        //stdPacking table
        public int Qty_Per_Container { get; set; }
        public int Qty_Per_Bag { get; set; }
        public int Qty_Per_Packet { get; set; }
        public int Max_Lvl { get; set; }

        //size table
        public int Size_Numerator { get; set; }
        public int Size_Denominator { get; set; }
        public string Size_Unit { get; set; }
        public float Size_Weight { get; set; }

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
        public string Address_3 { get; set; }

        public string Address_City { get; set; }
        public string Address_State { get; set; }
        public string Address_Postal_Code { get; set; }
        public string Address_Country { get; set; }
        public string Fax { get; set; }
        public string Phone_1 { get; set; }
        public string Phone_2 { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        //public bool ShippingSameAsBilling { get; set; }
        public string Shipping_Full_Name { get; set; }
        public string Shipping_Short_Name { get; set; }
        public string Shipping_Transporter { get; set; }
        public decimal Discount_Adjust { get; set; }

        //po table
        public int PO_code { get; set; }
        public string PO_no { get; set; }
        public DateTime PO_date { get; set; }
        public int Customer_tbl_code { get; set; }
        public int PO_qty { get; set; }
        public int Delivered_qty { get; set; }
        public bool UseBillingAddress { get; set; }
        public string PO_note { get; set; }
        public int To_delivery_qty { get; set; }
        public string remark_in_do { get; set; }

        //do table
        public int DO_no { get; set; }
        public int PO_tbl_code { get; set; }
        public int Trf_tbl_code { get; set; }
        public int DO_to_delivery_qty { get; set; }
        public DateTime DO_date { get; set; }
        public bool IsDelivered { get; set; }

        //Plan
        public int Location_area { get; set; }
        public int Location_line { get; set; }
        public DateTime Date_start { get; set; }
        public DateTime Date_end { get; set; }
        public DateTime Target_Delivery_Date { get; set; }
        public DateTime Item_Target_Delivery_Date { get; set; }
        public int Target_qty { get; set; }
        public int Max_qty { get; set; }
        public string Plan_status { get; set; }
        public string Plan_type { get; set; }
        public string Plan_note { get; set; }
        public int Production_ct_sec { get; set; }
        public int Production_cavity { get; set; }
        public float Production_PW_shot { get; set; }
        public float Production_RW_shot { get; set; }
        public int Production_Hours_PerDay { get; set; }
        public int Production_TargetQty_Adjusted_BySystem { get; set; }
        public int Item_Stock { get; set; }

        //Material Plan
        public int Plan_code { get; set; }
        public int Required_qty { get; set; }
        public int Preparing_qty { get; set; }
        public string Note { get; set; }
        public bool IsCompleted { get; set; }

        //Material Plan Prepare
        public int Mat_plan_code { get; set; }
        public int Std_packing { get; set; }
        public int Delivery_bag { get; set; }
        public int Location_from { get; set; }

        //common
        public DateTime Updated_Date { get; set; }
        public int Updated_By { get; set; }
        public bool IsRemoved { get; set; }
        public bool Freeze { get; set; }
        public int Priority_level { get; set; }
        public int Item_Priority_level { get; set; }

        //price
        public decimal Default_price { get; set; }
        public decimal Default_discount { get; set; }
    
        //customer price
        public decimal Unit_price { get; set; }
        public decimal Discount_rate { get; set; }
        public decimal Sub_total { get; set; }
    }
}
