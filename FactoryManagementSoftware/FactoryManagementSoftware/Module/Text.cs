using System;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.Module
{
    class Text
    {
        Tool tool = new Tool();
        itemDAL dalItem = new itemDAL();
        planningActionBLL uPlanningAction = new planningActionBLL();


        #region Action String

        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string System { get; } = "System";

        //password
        public string PW_Level_1 { get; } = "038989";
        public string PW_Level_2 { get; } = "038989!";
        public string PW_Level_3 { get; } = "Safplas038989!";
        public string PW_Level_4 { get; } = "Safplas19892020!";

        #region Admin
        public string Terminated { get; } = "Terminated";
        public string Activate { get; } = "Activate";

        public string TransferHistory { get; } = "Transfer History";
        public string AddingItem { get; } = "Add Item";
        public string RemoveItem { get; } = "Remove";
        public string JobPurpose { get; } = "Job Purpose";
        public string AddingNewMould { get; } = "Add New Mould";
        public string AddingExistingMould { get; } = "Add Existing Mould";
        public string AutoSearchFamilyItem { get; } = "Auto Search Family Item";


        public string LogIn { get; } = "LogIn";
        public string LogOut { get; } = "LogOut";

        public string AddItem { get; } = "AddItem";
        public string AddTransaction { get; } = "Add New Transaction";
        public string UsingRecycledMaterial { get; } = "Using Recycled Material";
        public string ItemEdit { get; } = "ItemEdit";
        public string ItemDelete { get; } = "ItemDelete";

        public string CheckedJobSheet { get; } = "CheckedJobSheet";
        public string UncheckedJobSheet { get; } = "UncheckedJobSheet";
        public string AddDailyJobSheet { get; } = "AddDailyJobSheet";
        public string EditDailyJobSheet { get; } = "EditDailyJobSheet";
        public string RemoveDailyJobSheet { get; } = "RemoveDailyJobSheet";

        public string AddItemJoin { get; } = "AddItemJoin";
        public string ItemJoinEdit { get; } = "ItemJoinEdit";
        public string ItemJoinDelete { get; } = "ItemJoinDelete";

        public string AddFactory { get; } = "AddFactory";
        public string FactoryEdit { get; } = "FactoryEdit";
        public string FactoryDelete { get; } = "FactoryDelete";

        public string AddCustomer { get; } = "AddCustomer";
        public string CustomerEdit { get; } = "CustomerEdit";
        public string CustomerDelete { get; } = "CustomerDelete";

        public string AddUser { get; } = "AddUser";
        public string UserEdit { get; } = "UserEdit";
        public string UserDelete { get; } = "UserDelete";
        #endregion

        #region Company Info

        public string Company_Name_CN { get; } = "安全塑膠有限公司";
        public string Company_Name_EN { get; } = "SAFETY PLASTICS SDN. BHD.";
        public string Company_SST_Reg_No { get; } = "SST Reg. No. : W10-1808-21023454";
        public string Company_RegistrationNo { get; } = "No. Syarikat: 198901011676 (188981-U)";

        public string Company_OUG_AddressAndContact { get; } =
 @"No. 2, Jalan 10/152, Taman Perindustrian O.U.G, Batu 6,Jalan Puchong, 58200 Kuala Lumpur.
(Tel) 03 - 7785 5278, 03 - 7782 0399 (Fax) 03 - 7782 0399 (Email) safety_plastic@yahoo.com";

        public string Company_Semenyih_AddressAndContact { get; } =
 @"No.17, PT 2507, Jalan Hi-Tech 2, Kawasan Perindustrian Hi-Tech, Jalan Sungai Lalang, 43500 Semenyih, Selangor.
(Tel) 016 - 282 8195 (Email) sales@safetyplastics.com.my";

        public string SBB_DO_Version_Control { get; } = "SBB P08-F01A (A) (4/12/23)";
        public string SBB_Invoice_Version_Control { get; } = "SBB P07-F05A (A) (10/07/24)";
        public string SBB_InternalTransferNote_FrmSMY_Version_Control { get; } = "SBB P10-F04A (A) (16/8/24)";
        public string SBB_DO_Version_Control_Old { get; } = "F 8.5.4-01 (A) (23/08/02)";

        #endregion

        #region Font Type

        public string Font_Type_KaiTi { get; } = "KaiTi";
        public string Font_Type_TimesNewRoman { get; } = "Times New Roman";

        #endregion

        #region History Action
        public string Transfer { get; } = "Transfer";
        public string TransferUndo { get; } = "TransferUndo";
        public string TransferUndoBySystem { get; } = "TransferUndoBySystem";
        public string TransferRedo { get; } = "TransferRedo";
        public string Undo { get; } = "Undo";
        public string Passed { get; } = "Passed";

        public string AddOrder { get; } = "AddOrder";
        public string OrderRequest { get; } = "OrderRequest";
        public string OrderApprove { get; } = "OrderApprove";
        public string OrderCancel { get; } = "OrderCancel";
        public string OrderReceive { get; } = "OrderReceive";
        public string OrderFollowUp { get; } = "OrderFllowUp";
        public string OrderClose { get; } = "OrderClose";
        public string OrderActionEdit { get; } = "OrderActionEdit";
        public string OrderActionUndo { get; } = "OrderActionUndo";

        public string DataAdded { get; } = "Data Added";
        public string DataUpdated { get; } = "Data Updated";

        public string ForecastEdit { get; } = "ForecastEdit";
        public string ForecastInsert { get; } = "ForecastInsert";
        public string ForecastReport { get; } = "ForecastReport";
        public string StockReport { get; } = "StockReport";
        public string TransferReport { get; } = "TransferReport";
        public string OrderReport { get; } = "OrderReport";
        public string MaterialUsedReport { get; } = "MaterialUsedReport";

        public string Excel { get; } = "Excel";
        public string PMMAEdit { get; } = "PMMA Edit";

        public string DO_Added { get; } = "D/O ADDED";
        public string DO_Edited { get; } = "D/O EDITED";
        public string DO_Removed { get; } = "D/O REMOVED";
        public string DO_Delivered { get; } = "D/O DELIVERED";
        public string DO_Exported { get; } = "D/O EXPORTED";
        public string DO_Incomplete { get; } = "D/O INCOMPLETE";
        public string DO_UndoRemove { get; } = "D/O UNDO REMOVE";
        public string DO_ChangeDONumber { get; } = "D/O NUMBER CHANGE";
        public string DO_AddedFromPlanner { get; } = "D/O ADDED FROM PLANNER";

        public string PO_Added { get; } = "P/O ADDED";
        public string PO_Edited { get; } = "P/O EDITED";
        public string PO_Removed { get; } = "P/O REMOVED";

       // public string PO_Delivered { get; } = "D/O DELIVERED";
        //public string PO_Exported { get; } = "D/O EXPORTED";

        public string DataType_ToDelivery { get; } = "To Delivery";

        #endregion

        #region SBB ITEM TYPE

        public string SBB_TYPE_EQUAL { get; } = "EQUAL";
        public string SBB_TYPE_SPRAYJET{ get; } = "SPRAY JET";
        public string SBB_TYPE_SPRINKLER{ get; } = "SPRINKLER";
        public string SBB_TYPE_SPRAYJETSMALL{ get; } = "SPRAY JET SMALL";
        public string SBB_TYPE_SPRAYJETBIG{ get; } = "SPRAY JET BIG";
        public string SBB_TYPE_MTA { get; } = "MTA";
        public string SBB_TYPE_FTA { get; } = "FTA";
        public string SBB_TYPE_ENDCAP { get; } = "END CAP";
        public string SBB_TYPE_POLYORING { get; } = "POLY O RING";
        public string SBB_TYPE_REDUCING { get; } = "REDUCING";
        public string SBB_TYPE_MALE { get; } = "MALE";
        public string SBB_TYPE_FEMALE { get; } = "FEMALE";

        #endregion

        #region Report Type

        public string Report_Type_Production { get; } = "ProductionReport";

        #endregion

        #region Planning 
        public string planning_status_idle { get; } = "IDLE";//blue
        public string planning_status_pending { get; } = "PENDING";//blue
        public string planning_status_cancelled { get; } = "CANCELLED";//white
        public string planning_status_warning { get; } = "WARNING";//red
        public string planning_status_draft { get; } = "DRAFT";
        public string planning_status_new_draft { get; } = "NEW DRAFT";
        public string planning_status_edting { get; } = "EDITING";
        public string planning_status_to_update { get; } = "TO UPDATE";
        public string planning_status_running { get; } = "RUNNING";//green
        public string planning_status_delayed { get; } = "DELAYED";//yellow
        public string planning_status_completed { get; } = "COMPLETED";//white
        public string planning_Family_mould_Remark { get; } = "[FAMILY MOULD]";
        public string planning_Material_Summary { get; } = "Material Summary";

        //PLANNING ACTION HISTORY////////////////////////////////////////////////////////////////////////////
        public string plan_Added { get; } = "JOB ADD";
        public string plan_Updated { get; } = "JOB UPDATE";
        public string plan_Target_Qty_Change { get; } = "JOB TARGET QTY CHANGE";
        public string plan_schedule_change { get; } = "JOB SCHEDULE CHANGE";
        public string plan_status_change { get; } = "JOB STATUS CHANGE";
        public string plan_machine_change { get; } = "JOB MACHINE CHANGE";
        public string plan_remark_change { get; } = "JOB REMARK CHANGE";
        public string plan_family_with_change { get; } = "JOB FAMILY CHANGE";
        public string plan_proday_change { get; } = "JOB PRODUCTION DAY CHANGE";
        public string plan_prohour_change { get; } = "JOB PRODUCTION HOUR CHANGE";
        public string plan_prohourperday_change { get; } = "JOB PRODUCTION HOUR PER DAY CHANGE";
        #endregion

        #region Habit
        public string habit_insert { get; } = "HABIT INSERT";
        public string habit_belongTo_PlanningPage { get; } = "PLANNING PAGE";
        public string habit_planning_HourPerDay { get; } = "PLANNING: HOUR PER DAY";
        public string habit_planning_Wastage { get; } = "PLANNING: MATERIAL WASTAGE %";
        public string habit_belongTo_ForecastReport { get; } = "FORECAST REPORT PAGE";
        public string habit_ForecastReport_OutFrom { get; } = "FORECAST REPORT: OUT FROM";
        public string habit_ForecastReport_OutTo { get; } = "FORECAST REPORT: OUT To";

        public string habit_ForecastReport_ForecastHistoryChecked { get; } = "FORECAST HISTORY CHECKED";

        #endregion

        #region Defect Remark
        public string Defect_Short { get; } = "1. Short";
        public string Defect_Flashing { get; } = "2. Flashing";
        public string Defect_Dirty { get; } = "3. Dirty";
        public string Defect_Black_White_Dot { get; } = "4. Black/White Dot";
        public string Defect_Oily { get; } = "5. Oily";
        public string Defect_Bubble { get; } = "6. Bubble";
        public string Defect_Wavy { get; } = "7. Wavy";
        public string Defect_Burn_Mark { get; } = "8. Burn Mark";
        public string Defect_Sink_Mark { get; } = "9. Sink Mark";
        public string Defect_Colour_Out { get; } = "10. Colour Out";
        public string Defect_Flow_Mark { get; } = "11. Flow Mark";
        public string Defect_Scratches { get; } = "12. Scratches";
        public string Defect_Hardness { get; } = "13. Hardness";

        public string Defect_Silver_White_Mark { get; } = "14. Silver/White Mark";

        #endregion


        #region Location

        public string Factory { get; } = "Factory";

        public string Factory_2 { get; } = "No.2";
        public string Factory_9 { get; } = "No.9";
        public string Factory_11 { get; } = "No.11";
        public string Factory_30 { get; } = "No.30";
        public string Factory_40 { get; } = "No.40";
        
        public string Factory_Store { get; } = "STORE";
        public string Factory_Semenyih { get; } = "Semenyih";
        public string Factory_SMY_AssemblyLine { get; } = "AssemblyLine";
        public string Factory_OUG { get; } = "OUG";
        public string Factory_Bina { get; } = "BINA";


        public string Other { get; } = "Other";
        public string Customer { get; } = "Customer";
        public string Production { get; } = "Production";
        public string Assembly { get; } = "Assembly";
        public string Inspection { get; } = "Inspection";
        public string Inspection_Pass { get; } = "(OK";

        #endregion

        #region Database String
         //<add name = "connstrng" connectionString="SERVER=DESKTOP-MFUKGH2;DATABASE=Factory;USER ID=stock;PASSWORD=stock"/>

        public string DB_Semenyih { get; } = "SERVER=ADMIN001;DATABASE=Factory;USER ID=StockAdmin;PASSWORD=stock";
        //SERVER=ADMIN001;DATABASE=Factory;USER ID=StockAdmin;PASSWORD=stock

        //public string DB_Semenyih { get; } = "SERVER=DESKTOP-MFUKGH2;DATABASE=Factory;USER ID=stock;PASSWORD=stock";
        public string DB_OUG { get; } = "SERVER=192.168.0.149;DATABASE=Factory;USER ID=stock;PASSWORD=stock";
        //SERVER=192.168.0.149;DATABASE=Factory;USER ID=stock;PASSWORD=stock

        public string DB_JunPC { get; } = @"Data Source=.\SQLEXPRESS01;Initial Catalog=Factory;Integrated Security=True";
        //Data Source=.\SQLEXPRESS01;Initial Catalog=Factory;Integrated Security=True

        #endregion

        #region Shift
        public string Shift_Morning { get; } = "MORNING";
        public string Shift_Night { get; } = "NIGHT";
        #endregion

        #region Unit
        public string Unit_KG { get; } = "kg";
        public string Unit_g { get; } = "g";
        public string Unit_Set { get; } = "set";
        public string Unit_Piece { get; } = "piece";
        public string Unit_Container { get; } = "container";
        public string Unit_Packet { get; } = "packet";
        //public string Unit_PCS { get; } = "pcs";
        public string Unit_Meter { get; } = "meter";
        public string Unit_Bag{ get; } = "bag";
        public string Unit_Millimetre { get; } = "mm";
        public string Unit_Inch { get; } = "in";
        public string Unit_Degree { get; } = "°";
        public string Unit_Roll { get; } = "roll";
        #endregion

        #region Item Category

        public string Cat_RawMat { get; } = "RAW Material";
        public string Cat_MB { get; } = "Master Batch";
        public string Cat_ColorMat { get; } = "Color Material";

        public string Cat_Pigment { get; } = "Pigment";
        public string Cat_Part { get; } = "Part";
        public string Cat_Carton { get; } = "Carton";
        public string Cat_PolyBag { get; } = "Poly Bag";
        public string Cat_SubMat { get; } = "Sub Material";
        public string Cat_Mould { get; } = "Mould";
        public string Cat_Packaging { get; } = "Packaging";

        public string Cat_Terminated { get; } = "(TERMINATED)";


        #endregion

        #region  DGV TABLE HEADER NAME

        public string Header_IndexMarking { get; } = "#Marking";

        public string Header_Index { get; } = "#";

        public string Header_Fac { get; } = "Fac.";
        public string Header_FacID { get; } = "FAC. ID";
        public string Header_Mac{ get; } = "Mac.";
        public string Header_MacID{ get; } = "MAC. ID";
        public string Header_MacName{ get; } = "MAC.";
        public string Header_MeterStart{ get; } = "Meter Start";
        public string Header_MeterEnd{ get; } = "Meter End";

        public string Header_TimeStart { get; } = "Time Start";
        public string Header_TimeEnd { get; } = "Time End";

        public string Header_StockIn_Remark{ get; } = "Stock In Remark";
        public string Header_StockIn_Container{ get; } = "Stock In Qty (Container)";
        public string Header_StockIn_Balance_Qty{ get; } = "Stock In Balance Qty";
        public string Header_Production_Max_Qty{ get; } = "Production Max Qty";
        public string Header_Ori_DateStart { get; } = "ORI. START";
        public string Header_DateStart { get; } = "Start";
        public string Header_Ori_EstDateEnd { get; } = "ORI. EST. END";
        public string Header_EstDateEnd { get; } = "Est. End";
        public string Header_EstMacFreeDate { get; } = "EST. MACHINE FREE DATE";

        public string Header_DateFrom{ get; } = "FROM";
        public string Header_DateTo{ get; } = "TO";
        public string Header_JobNo{ get; } = "Job No.";
        public string Header_SheetID{ get; } = "SHEET ID";
        public string Header_Shift{ get; } = "SHIFT";
        public string Header_Operator{ get; } = "Operator";
        public string Header_QCPassedQty { get; } = "QC PASSED QTY";
        public string Header_TotalReject { get; } = "Rejected Qty";

        public string Header_RejectRate { get; } = "Rejected %";
        public string Header_MaxOutput { get; } = "MAX Output";
        public string Header_ProducedQty { get; } = "Produced Qty";
        public string Header_YieldRate { get; } = "Yield %";
        public string Header_IdealHourlyShot { get; } = "Ideal Hourly Shot";
        public string Header_AvgHourlyShot { get; } = "Avg. Hourly Shot";

        public string Header_EfficiencyRate { get; } = "Efficiency %"; 
        public string Header_TotalStockIn { get; } = "Stock In Qty";
        public string Header_TotalSales { get; } = "TOTAL SALES";
        public string Header_TargetQty { get; } = "Target Qty";
        public string Header_Qty { get; } = "QUANTITY";
        public string Header_DOQty { get; } = "NUMBER OF D/O";

        public string Header_TotalQty { get; } = "Total Qty";
        public string Header_TotalQtyUnit { get; } = "Total Qty Unit";

        public string Header_QtyPerBox { get; } = "Qty Per Box";
        public string Header_BoxQty { get; } = "Box Qty";
        public string Header_BoxUnit { get; } = "Box Unit";
       


        public string Header_AutoQtyAdjustment { get; } = "MAX Qty";
        public string Header_Job_Purpose { get; } = "Job Purpose";


        public string Header_DefectRemark { get; } = "Defect Remark";
        public string Header_QtyReject { get; } = "Reject Qty";


        public string Header_ParentOrProduct { get; } = "Parent/Product";

        public string Header_DirectUseOn { get; } = "DIRECT USE ON (PARENT)";
        public string Header_GroupLevel { get; } = "LEVEL";
        public string Header_GroupCode { get; } = "Group Code";
        public string Header_TrfTableKey { get; } = "TrfTableKey";
        public string Header_ID { get; } = "ID";
        public string Header_CustomerCode { get; } = "CUSTOMER CODE";
        public string Header_Customer { get; } = "CUSTOMER";
        public string Header_PartCodeWithParent { get; } = "PART CODE_(PARENT)";
        public string Header_PartCode { get; } = "PART CODE";
        public string Header_ItemCode { get; } = "ITEM CODE";
        public string Header_ItemCode_Present { get; } = "ITEM CODE PRESENT";
        public string Header_MouldCode { get; } = "Mould Code";
        public string Header_MouldSelection { get; } = "Mould Selection";
        public string Header_ItemSelection { get; } = "Item Selection";
        public string Header_Selection { get; } = "Selection";
        public string Header_MouldDescription{ get; } = "Mould Description";
        public string Header_ItemName { get; } = "ITEM NAME";
        public string Header_CountedQty { get; } = "COUNTED QTY";
        public string Header_SystemQty { get; } = "SYSTEM QTY";
        public string Header_Difference{ get; } = "DIFFERENCE";

        public string Header_ItemDescription { get; } = "Item Description";
        public string Header_Stocktake { get; } = "STOCKTAKE";


        public string Header_JoinQty { get; } = "JOIN QTY";
        public string Header_JoinMax { get; } = "JOIN MAX";
        public string Header_JoinMin { get; } = "JOIN MIN";
        public string Header_JoinWastage { get; } = "JOIN WASTAGE";
        public string Header_PartNameWithParent { get; } = "PART NAME_(PARENT)";
        public string Header_PartName { get; } = "PART NAME";
        public string Header_ItemNameAndCode { get; } = "Item Name (CODE)";
        public string Header_Parent { get; } = "PARENT";
        public string Header_Product { get; } = "OUTGOING PRODUCT";
        public string Header_OpeningStock { get; } = "OPENING STOCK";
        public string Header_In_KG_Piece { get; } = "IN(KG/PIECE)";
        public string Header_Used_KG_Piece { get; } = "USED(KG/PIECE)";
        public string Header_DirectOut_KG_Piece { get; } = "DIRECT OUT(KG/PIECE)";
        public string Header_Balance { get; } = "balance";
        public string Header_BalStock { get; } = "Bal. Stock";
        public string Header_ReadyStock { get; } = "Ready Stock";
        public string Header_SystemStock { get; } = "System Stock";
        public string Header_StockCount { get; } = "Stock Count";
        public string Header_StockLocation { get; } = "Stock Location";
        public string Header_StockLocation_TblCode { get; } = "Stock Location TblCode";
        public string Header_CountUnit { get; } = "Count Unit";
        public string Header_UnitConversionRate { get; } = "Unit Conversion Rate";
        public string Header_ActionPreview { get; } = "Action Preview";
        public string Header_OutTo { get; } = "OutTo";
        public string Header_InFrom { get; } = "InFrom";
        public string Header_OutTo_TblCode { get; } = "OutTo Tbl Code";
        public string Header_InFrom_TblCode { get; } = "InFrom Tbl Code";
        public string Header_TableCode { get; } = "Table Code";
        public string Header_RunningNo { get; } = "Running No";
        public string Header_DONo { get; } = "D/O No.";
        public string Header_DO_Table_Code { get; } = "D/O Table Code";
        public string Header_Trf_Table_Code { get; } = "Tranfer Table Code";
        public string Header_Random_Code { get; } = "Random Code";
        public string Header_DOType { get; } = "D/O Type";
        public string Header_From_Cat { get; } = "Cat From";
        public string Header_From { get; } = "From";
        public string Header_BillTo{ get; } = "Bill To";
        public string Header_ShipTo{ get; } = "Ship To";
        public string Header_To_Cat {get; } = "Cat Ship To";
        public string Header_DODate{ get; } = "D/O Date";
        public string Header_CompletedDate{ get; } = "Completed Date";
        public string Header_DeliveredDate{ get; } = "Delivered Date";
        public string Header_ReservedForOtherJobs { get; } = "Reserved For Other Jobs";
        public string Header_RequiredForCurrentJob { get; } = "Required For Current Job";
        public string Header_ParentStock { get; } = "PARENT STOCK";
        public string Header_ProductStock { get; } = "PRODUCT STOCK";

        public string Header_PendingOrder { get; } = "PENDING ORDER";

        public string Header_PendingOrder_ZeroCost { get; } = "PENDING ORDER (ZERO COST)";
        public string Header_PendingOrder_Purchase { get; } = "PENDING ORDER (PURCHASE)";
        public string Header_Order_Requesting { get; } = "ORDER REQUESTING";


        public string Header_Unit { get; } = "UOM";
        public string Header_SearchMode { get; } = "SearchMode";
        public string Header_DescriptionIncludeCategory { get; } = "DescriptionIncludeCategory";
        public string Header_DescriptionIncludePackaging { get; } = "DescriptionIncludePackaging";
        public string Header_DescriptionIncludeRemark { get; } = "DescriptionIncludeRemark";
        public string Header_ToRemove { get; } = "ToRemove";


        public string Header_Percentage { get; } = "%";
        public string Header_Adjust { get; } = "ADJUST";
        public string Header_Remark { get; } = "Remark";
        public string Header_Note { get; } = "NOTE";
        public string Header_Category { get; } = "Category";
        public string Header_Status { get; } = "Status";
        public string Header_Ori_Status { get; } = "ORI. STATUS";
        public string Header_Ori_Machine_Name { get; } = "ORI. Mac. Name";
        public string Header_Type { get; } = "TYPE";
        public string Header_MatType { get; } = "MAT. TYPE";
        public string Header_Mat { get; } = "MATERIAL";
        public string Header_RawMat_1 { get; } = "RAW MATERIAL";
        public string Header_RawMat_1_Ratio { get; } = "RAW MATERIAL 1 Ratio";
        public string Header_RawMat_2_Ratio { get; } = "RAW MATERIAL 2 Ratio";
        public string Header_RawMat_2 { get; } = "RAW MATERIAL 2";
        public string Header_RawMat_Lot_No { get; } = "Raw Mat. Lot No.";
        public string Header_ColorMat_Lot_No { get; } = "Color Mat. Lot No.";

        public string Header_RawMat_String { get; } = "Raw Mat.";
        //public string Header_RawMat_KG { get; } = "Raw Mat. KG";
        //public string Header_RawMat_Bag { get; } = "Raw Mat. Bag";

        public string Header_RawMat_KG_1 { get; } = "Raw 1 Mat. KG";
        public string Header_RawMat_KG_2 { get; } = "Raw Mat. KG";

        public string Header_RawMat_Bag_1 { get; } = "Raw 1 Mat. Bag ";
        public string Header_RawMat_Bag_2 { get; } = "Raw 2 Mat. Bag ";
        public string Header_RawMat_Qty { get; } = "Raw Mat. Qty";
        public string Header_Recycle_Qty { get; } = "Recycle Mat. Qty";
        public string Header_MatCode { get; } = "MAT. CODE";
        public string Header_MatName { get; } = "MAT. NAME";
        public string Header_Color { get; } = "Color";

        public string Header_ColorMat_KG { get; } = "Color Mat. (KG)";

        public string Header_ItemWeight_KG { get; } = "ITEM WEIGHT(KG)";
        public string Header_KG { get; } = "kg";
        public string Header_Qty_Required_KG { get; } = "Qty Required (KG)";
        public string Header_Qty_Required_Bag { get; } = "Qty Required (Bag)";
        public string Header_BAG { get; } = "Bag";
        public string Header_KGPERBAG { get; } = "kg/Bag";

        public string Header_RunnerWeight_G { get; } = "RUNNER WEIGHT(G)";
        public string Header_PartWeight_G { get; } = "PART WEIGHT(G)";

        public string Header_ItemWeight_G { get; } = "ITEM WEIGHT(g)";

        public string Header_ItemWeight_G_Piece { get; } = "ITEM WEIGHT/Pcs (g)";

        public string Header_Parent_Weight_G_Piece { get; } = "PARENT WEIGHT/Pcs (g)";

        private string header_ParentIndex = "PARENT #";

        public string Header_ParentIndex { get; } = "PARENT #";

        public string Header_ParentStillNeed { get; } = "PARENT STILL NEED";
        public string Header_StillNeed { get; } = "STILL NEED";
        public string Header_Delivered { get; } = "DELIVERED";
        public string Header_MaterialUsed_G { get; } = "MAT. USED(g)";
        public string Header_MaterialUsed_KG_Piece { get; } = "MAT. USED(KG/PIECE)";
        public string Header_MaterialUsed_KG { get; } = "MAT. USED(KG)";
        public string Header_MaterialUsedWithWastage { get; } = "MAT. USED AFTER WASTAGE";
        public string Header_MaterialRequiredIncludedWastage { get; } = "MAT. REQUIRED INCLUDED WASTAGE";
        public string Header_TotalMaterialUsed_KG_Piece { get; } = "TOTAL MAT. USED(KG/PIECE)";
        public string Header_TotalMaterialUsed_KG { get; } = "TOTAL MAT. USED(KG)";

        public string Header_QuoTon { get; } = "QUOTATION TON";
        public string Header_BestTon { get; } = "BEST TON";
        public string Header_ProTon { get; } = "Mac. Ton";
        public string Header_ColorMat { get; } = "Color Mat.";
        public string Header_ColorMatCode { get; } = "COLOR MATERIAL CODE";
        public string Header_Month { get; } = "MONTH";
        public string Header_OldValue { get; } = "OLD VALUE";
        public string Header_NewValue { get; } = "NEW VALUE";

        public string Header_ColorRate { get; } = "Color Mat. Rate (%)";
        public string Header_Ratio { get; } = "Ratio (%)";
        public string Header_RoundUp_ToBag { get; } = "Round Up To Bag";

        public string Header_QuoCT { get; } = "QUOTATION CYCLE TIME";
        public string Header_ProCT { get; } = "Pro. Cycle Time";

        public string Header_QuoPwPcs { get; } = "QUOTATION PART WEIGHT PER PCS (g)";
        public string Header_QuoRwPcs { get; } = "QUOTATION RUNNER WEIGHT PER PCS (g)";

        public string Header_ProPwPcs { get; } = "PART WEIGHT PER PCS (g)";
        public string Header_ProRwPcs { get; } = "RUNNER WEIGHT PER PCS (g)";

        public string Header_ProPwShot { get; } = "Part Weight Per Shot (g)";
        public string Header_ProRwShot { get; } = "Ruuner Weight Per Shot (g)";

        public string Header_FamilyWithJobNo { get; } = "FAMILY WITH";
        public string Header_ProductionDate { get; } = "PRO. Date";
        public string Header_ProductionDay { get; } = "PRO DAY";
        public string Header_ProductionHour { get; } = "PRO HOUR";
        public string Header_ProductionHourPerDay { get; } = "PRO HOUR PER DAY";

        public string Header_Cavity { get; } = "Cavity";
        public string Header_Cooling { get; } = "COOLING TIME (s)";

        public string Header_Wastage { get; } = "WASTAGE";
        public string Header_WastageAllowed_Percentage { get; } = "MAT. WASTAGE ALLOWED (%)";
        public string Cmb_All { get; } = "ALL";
        public string All_Item { get; } = "ALL ITEM";
        public string Internal { get; } = "Internal";

        public string Jump { get; } = "Jump";

        public string Header_GotNotPackagingChild { get; } = "GotNotPackagingChild";

        public string Header_Qty_Per_Container { get; } = "Qty / Container";
        public string Header_Container_Qty { get; } = "Container Qty";
        public string Header_Container_Stock_Out { get; } = "Container Stock Out";

        public string Header_Forecast_1 { get; } = "Forecast 1";
        public string Header_Forecast_2 { get; } = "Forecast 2";
        public string Header_Forecast_3 { get; } = "Forecast 3";

        public string Header_Date { get; } = "DATE";
        public string Header_AddedDate { get; } = "ADDED DATE";
        public string Header_AddedBy { get; } = "ADDED BY";
        public string Header_EditedBy { get; } = "EDITED BY";

        public string Header_UpdatedDate { get; } = "UPDATED DATE";
        public string Header_UpdatedBy { get; } = "UPDATED BY";

        public string Str_MoreDetail { get; } = "More Details";
        public string Str_OrderRequest { get; } = "Order Request";
        public string Str_OrderRecordSearch { get; } = "Search Order Record";

        public string str_Forecast { get; } = " FORECAST";
        public string str_Delivered { get; } = " DELIVERED";
        public string str_EstBalance { get; } = " EST. BAL.";
        public string str_InsufficientQty { get; } = " INSUFFICIENT QTY";
        public string str_RequiredQty { get; } = " REQUIRED QTY";

        public string job_end_production { get; } = "[END PRODUCTION]";

        public string Header_Data { get; } = "DATA DB";
        public string Header_DataName { get; } = "DATA";
        public string Header_ChildCode { get; } = "CHILD CODE";
        public string Header_ChildName { get; } = "CHILD NAME";
        public string Header_JoinRatio { get; } = "PARENT:CHILD";
        public string Header_JoinRatio_Product_Mat { get; } = "PRODUCT:MAT.";
        public string Header_ParentMax { get; } = "PARENT MAX";
        public string Header_ParentMin { get; } = "PARENT MIN";
        public string Header_ChildQty { get; } = "CHILD QTY";

        public string Header_Description { get; } = "DESCRIPTION";
        public string Header_UnitPrice { get; } = "U.PRICE (RM)";
        public string Header_DiscountRate { get; } = "DISC. (%)";
        public string Header_NetAmount { get; } = "NET AMOUNT (RM)";

        #endregion

        #region SPP/SBB Info; Item Type

        public string SPP_BrandName { get; } = "SPP";
        public string SBB_BrandName { get; } = "SBB";


        public string SprayJet_Short { get; } = "SJ";
        public string SprayJetSmall_Short { get; } = "SJS";
        public string SprayJetBig_Short { get; } = "SJB";
        public string Sprinkler_Short { get; } = "SP";
        public string EqualElbow_Short { get; } = "EE";
        public string EqualSocket_Short { get; } = "ES";
        public string EqualTee_Short { get; } = "ET";

        public string ReducingSocket_Short { get; } = "RS";
        public string ReducingElbow_Short { get; } = "RE";
        public string ReducingTee_Short { get; } = "RT";

        public string MTA_Short { get; } = "MTA";
        public string FTA_Short { get; } = "FTA";

        public string MaleElbow_Short { get; } = "ME";
        public string MaleTee_Short { get; } = "MT";

        public string FemaleElbow_Short { get; } = "FE";
        public string FemaleTee_Short { get; } = "FT";

        public string EndCap_Short { get; } = "ENDC";

        public string PolyORing_Short { get; } = "OR";

        public string ClampSaddle_Short { get; } = "CS";
        public string PolyNipple_Short { get; } = "PN";
        public string PolyReducingBush_Short { get; } = "PRB";


        #endregion

        #region SPP/SBB CATEGORY
        public string Cat_CommonPart { get; } = "COMMON PART";
        public string Cat_Body { get; } = "BODY";
        public string Cat_Assembled { get; } = "ASSEMBLED";
        public string Cat_ReadyGoods { get; } = " READY GOODS";
        #endregion

        #region SPP/SBB TYPE
        public string Type_EqualSocket { get; } = "EQUAL SOCKET";
        public string Type_SprayJet { get; } = "SPRAY JET";
        public string Type_SprayJetSmall { get; } = "SPRAY JET SMALL";
        public string Type_SprayJetBig { get; } = "SPRAY JET BIG";
        public string Type_Sprinkler { get; } = "SPRINKLER";
        public string Type_EqualElbow { get; } = "EQUAL ELBOW";
        public string Type_EqualTee { get; } = "EQUAL TEE";
        public string Type_Bush { get; } = "BUSH";
        public string Type_Grip { get; } = "GRIP";
        public string Type_Oring { get; } = "O RING";
        public string Type_Cap { get; } = "CAP";

        public string Type_ReducingSocket { get; } = "REDUCING SOCKET";
        public string Type_ReducingElbow { get; } = "REDUCING ELBOW";
        public string Type_ReducingTee { get; } = "REDUCING TEE";

        public string Type_MTA { get; } = "MTA";
        public string Type_FTA { get; } = "FTA";

        public string Type_MaleElbow { get; } = "MALE ELBOW";
        public string Type_MaleTee { get; } = "MALE TEE";

        public string Type_FemaleElbow { get; } = "FEMALE ELBOW";
        public string Type_FemaleTee { get; } = "FEMALE TEE";

        public string Type_EndCap { get; } = "END CAP";

        public string Type_PolyNipple { get; } = "POLY NIPPLE";

        public string Type_PolyReducingBush { get; } = "POLY R.BUSH";

        public string Type_PolyORing { get; } = "POLY O RING";
        public string Type_ClampSaddle { get; } = "CLAMP SADDLE";



        #endregion

        #region Message
        public string Message_DataNotSaved { get; } = "You changes have not been saved.\nDiscard changes?";
        public string Note_OldBalanceStockOut { get; } = "Old Balance Stock Out";
        public string Note_BalanceStockIn { get; } = "Balance Stock In";
        #endregion

        #region SPP/SBB STOCK LEVEL
        public int StockLevel_20 { get; } = 50;
        public int StockLevel_25 { get; } = 60;
        public int StockLevel_32 { get; } = 40;
        public int StockLevel_50 { get; } = 30;
        public int StockLevel_63 { get; } = 0;
        #endregion

        #region Delivery Status

        public string Delivery_Processing { get; } = "PROCESSING";
        public string Delivery_DOPending { get; } = "D/O PENDING";
        public string Delivery_DOOpened { get; } = "D/O OPENED";
        public string Delivery_ToDeliver { get; } = "TO DELIVER";
        public string Delivery_Delayed { get; } = "DELAYED";
        public string Delivery_Cancelled { get; } = "CANCELLED";
        public string Delivery_Delivered { get; } = "DELIVERED";
        public string Delivery_Removed { get; } = "REMOVED";
        public string Status_InProgress { get; } = "In Progress";
        public string Status_Draft { get; } = "Drafting";
        public string Status_Completed { get; } = "Completed";
        public string Status_Cancelled { get; } = "Cancelled";

        #endregion

        public string ReportType_ByJobNo { get; } = "By Job No.";
        public string ReportType_ByDateAndShift { get; } = "By Date and Shift";
        public string ReportType_NoShow { get; } = "No Show";
        public string ReportType_SheetList { get; } = "Sheet List";
        public string ReportType_SheetData { get; } = "Sheet Data";


        public string ViewJobSheetRecord { get; } = "View Job Sheet Record";
        public string EditMode_Open { get; } = "Edit Mode";
        public string EditMode_Close { get; } = "Close Edit Mode";
        public string Search_DefaultText { get; } = "Search";
        public string DeliveredSummary { get; } = "Delivered Summary";
        public string ProductionHistory { get; } = "Production History";
        public string ForecastRecord { get; } = "Forecast Record";
        public string JobPlanning { get; } = "Job Planning";
        public string StockLocation { get; } = "Stock Location";

        public string DO_CustOwnDO { get; } = "*USE CUSTOMER OWN D/O";


        public string DailyAction_ChangePlan { get; } = "CHANGE PLAN TO";

        #endregion

        #region CSV File Name

        //string path = @"G:\Other computers\Semenyih666\Server\(SOFTWARE SYSTEM)\(CSV)\";

        public string CSV_SemenyihServer_Path { get; } = @"\\DESKTOP-MFUKGH2\Server\(SOFTWARE SYSTEM)\(CSV)\";

        public string CSV_Jun_GoogleDrive_SemenyihServer_Path { get; } = @"G:\Other computers\Semenyih666\Server\(SOFTWARE SYSTEM)\(CSV)\";

        public string CSV_dalItemCust_SPPCustSearchWithTypeAndSize { get; } = "dalItemCust_SPPCustSearchWithTypeAndSize.db";
        public string CSV_dalItem_Select { get; } = "dalItem_Select.db";
        public string CSV_dalSBB_DOWithTrfInfoSelect_StartEnd { get; } = "dalSBB_DOWithTrfInfoSelect_StartEnd.db";
        //public string CSV_dalSBB_DOWithTrfInfoSelect_StartEnd { get; } = "dalSBB_DOWithTrfInfoSelect_StartEnd.csv";



        #endregion


        #region History Detail String

        public string Success { get; } = "Success";
        public string Failed { get; } = "Access Denied";

        public string GetPONumberAndCustomer(string PONo, string customer)
        {
            return "PO NO. :" + PONo + "( " + customer + ")";
        }

        public string GetDONumberAndCustomer(int DONo, string customer)
        {
            return "DO NO. :"+DONo.ToString("D6") + "( "+customer+")";
        }

        public string ChangeDONumber(int DONo, string customer, string oldData, string newData)
        {
            return "DO NO. :" + DONo.ToString("D6") + "( " + customer + ") " + " change D/O number from " + oldData + " to " + newData;
        }

        public string GetDOEditDetail(int DONo, string customer, string size, string type, string dataType, string oldData, string newData)
        {
            return "DO NO. :" + DONo.ToString("D6") + "( " + customer + ") " + size+" "+type +" "+dataType+" amend from "+oldData+" to "+newData;
        }

        public string GetDOExportDetail(bool openFile, bool printFile, bool printPreview)
        {

            string detail = "";

            if(openFile)
            {
                detail = "Open File; ";
            }

            if (printFile)
            {
                if(printPreview)
                {
                    detail += "Print File(with print preview);";
                }
                else
                {
                    detail += "Print File;";
                }
                
            }

            return detail;
        }


        public string getTransferDetailString(int ID, float qty, string unit, string itemCode, string from, string to)
        {
            string detail = "(ID:"+ID.ToString()+") "+qty.ToString()+" "+unit+" ("+itemCode+") "+tool.getItemName(itemCode)+" from "+from+" to "+to;

            return detail;
        }

        public string getNewOrderString(int orderID, float qty, string unit, string itemCode, string date)
        {
            string detail = "(ID:" + orderID.ToString() + ") " + qty.ToString() + " " + unit + " (" + itemCode + ") " + tool.getItemName(itemCode) + " at " + date;

            return detail;
        }

        public string getNewPlanningDetail(PlanningBLL u)
        {
            string detail = u.part_name +"( "+u.part_code+") :"+u.production_target_qty+" Target QTY run at machine "+u.machine_id;

            return detail;
        }

        public string getPlanningTargetQtyChangeDetail(PlanningBLL u)
        {
            //string detail = u.part_name + "( " + u.part_code + ") :" + u.production_Old_target_qty + " Target QTY run at machine " + u.machine_id;

            string detail = "[Plan's Target Qty Changed : "+ u.plan_id + "]" + u.production_Old_target_qty + " -> " + u.production_target_qty;

            return detail;
        }

        //public string getPlanningScheduleChangeDetail(PlanningBLL u)
        //{
        //    string detail = u.part_name + "( " + u.part_code + ") :" + u.production_target_qty + " Target QTY run at machine " + u.machine_id;

        //    return detail;
        //}

        public string getOrderString(int ID, float qty, string unit, string itemCode, string date)
        {
            string detail = "(ID:" + ID.ToString() + ") " + qty.ToString() + " " + unit + " (" + itemCode + ") " + tool.getItemName(itemCode) + " estimate received date at " + date;

            return detail;
        }

        public string getOrderStatusChangeString(int orderID)
        {
            string detail = "ID:" + orderID.ToString();

            return detail;
        }

        public string getForecastMonthEditString(string outMonth, string newMonth)
        {
            string detail = "Forecast 1 Month change from "+outMonth+" to "+newMonth;

            return detail;
        }

        public string getForecastEditString(string customer, string forecastNum, string itemCode, string from, string to)
        {
            if(int.TryParse(customer, out int i))
            {
                customer = tool.getCustName(Convert.ToInt32(customer));
            }

            string detail = "[" + customer + "_" + forecastNum + "] " + tool.getItemName(itemCode) + " (" + itemCode + "):" + from + " -> " + to;

            return detail;
        }

        public string getForecastInsertString(itemForecastBLL u)
        {
            string customer = "null";
            if (int.TryParse(u.cust_id.ToString(), out int i))
            {
                customer = tool.getCustName(Convert.ToInt32(u.cust_id));
            }

            string detail = "[" + customer + "_" + u.forecast_month + u.forecast_year + "] " + tool.getItemName(u.item_code) + " (" + u.item_code + "):" + u.forecast_qty;

            return detail;

        }

        public string getForecastEditString(itemForecastBLL u)
        {
            string customer = "null";
            if (int.TryParse(u.cust_id.ToString(), out int i))
            {
                customer = tool.getCustName(Convert.ToInt32(u.cust_id));
            }

            string detail = "[" + customer + "_" + u.forecast_month +u.forecast_year+ "] " + tool.getItemName(u.item_code) + " (" + u.item_code + "):" + u.forecast_old_qty + " -> " + u.forecast_qty;

            return detail;
        }

        public string getExcelString(string fileName)
        {
            string detail = fileName;

            return detail;
        }

        public string getPMMAEditString(string itemCode, string date, string subject, string oldValue, string newValue)
        {
            string detail = "("+itemCode+"_"+date+") Change "+subject+" from "+oldValue+" to "+newValue;

            return detail;
        }

        #endregion
    }
}
