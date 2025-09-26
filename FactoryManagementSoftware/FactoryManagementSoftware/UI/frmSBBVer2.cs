using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Xml.Linq;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using static Guna.UI2.Native.WinApi;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System.Security.Cryptography.X509Certificates;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBBVer2 : Form
    {
        public frmSBBVer2()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvStockAlert, true);
            _instance = this;

            //MonthlyDateStart = tool.GetSBBMonthlyStartDate();
            //MonthlyDateEnd = tool.GetSBBMonthlyEndDate();

            MonthlyDateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            MonthlyDateEnd = MonthlyDateStart.AddMonths(1).AddDays(-1);

            InitialMonthlyDate();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparam = base.CreateParams;
                handleparam.ExStyle |= 0x02000000;
                return handleparam;
            }
        }

        private void InitialMonthlyDate()
        {
            if (MonthlyDateStart == null)
            {
                //MonthlyDateStart = tool.GetSBBMonthlyStartDate();
                MonthlyDateStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            if (MonthlyDateEnd == null)
            {
                //MonthlyDateEnd = tool.GetSBBMonthlyEndDate();
                MonthlyDateEnd = MonthlyDateStart.AddMonths(1).AddDays(-1);

            }

            lblMonthlyDeliveredBag.Text = Text_GB_MonthlyDeliveredBags + " (" + MonthlyDateStart.ToString("dd/MM") + " - " + MonthlyDateEnd.ToString("dd/MM") + ")";

        }


        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        joinBLL uJoin = new joinBLL();
        //facDAL dalFac = new facDAL();
        //pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        //userDAL dalUser = new userDAL();

        SBBDataDAL dalSBB = new SBBDataDAL();
        SBBDataBLL uSBB = new SBBDataBLL();

        facStockDAL dalStock = new facStockDAL();

        //historyDAL dalHistory = new historyDAL();
        //historyBLL uHistory = new historyBLL();
        planningDAL dalPlan = new planningDAL();

        joinDAL dalJoin = new joinDAL();

        Tool tool = new Tool();
        Text text = new Text();

        private static frmSBBVer2 _instance;

        private int DeliveredBag_FontSize = 60;

        DataTable dt_DOWithTrfInfoSelectedPeriod;
        DataTable dt_Item;
        DataTable dt_SBBCustSearchWithTypeAndSize;
        DataTable dt_SBBItemSelect;
        DataTable dt_JoinSelectWithChildCat;
        DataTable dt_TrfRangeUsageSearch;
        DataTable dt_POSelectWithSizeAndType;
        DataTable dt_PendingPOSelect;
        DataTable dt_SBBCustWithoutRemovedDataSelect;
        DataTable dt_SBBItemUpload;
        DataTable dt_DOWithTrfInfoSelect;
        DataTable dt_Stock;

        public static DateTime MonthlyDateStart;
        public static DateTime MonthlyDateEnd;

        readonly string header_BalAfter = "BAL. AFTER (FILL ALL)";
        readonly string header_BalAfterBag = "BAL. AFTER (BAG)";
        readonly string header_BalAfterPcs = "BAL. AFTER (PCS)";
        readonly string header_QtyPerBag = "QTY PER BAG";

        readonly string header_BalAfterPlan = "BAL. AFTER (PLAN)";
        readonly string header_BalAfterPlanBag = "BAL. AFTER PLAN (BAG)";
        readonly string header_BalAfterPlanPcs = "BAL. AFTER PLAN (PCS)";

        readonly string MonthlyDate_Normal = "1-30/31";
        readonly string MonthlyDate_Safety = "23-22";


        readonly string DataSource_GoogleDrive = "GOOGLE DRIVE(SEMENYIH)";
        readonly string DataSource_LocalDB = "LOCAL DB";

        readonly string Text_GB_MonthlyDeliveredBags = "Monthly Delivered Bags";
        private bool DeliveredBags_FullScreen = false;


        readonly string header_ItemCategory = "CATEGORY";

        readonly string header_Index = "#";
        readonly string header_Status = "STATUS";
        readonly string header_ItemCode = "CODE";
        readonly string header_ItemName = "NAME";
        string header_Stock = "STOCK (All)";
        //string header_BalAfter1 = "BAL. AFTER ";
        //string header_BalAfter2 = "BAL. AFTER ";
        //string header_BalAfter3 = "BAL. AFTER ";
        readonly string header_Produced = "PRODUCED";
        readonly string header_ProduceTarget = "PRODUCE TARGET";
        readonly string header_ProDaysNeeded = "PRO DAY(24HRS) NEEDED";
        readonly string header_MaxProdPerDay = "MAX Prod./Day";
        readonly string header_MonthlyUsage = "Monthly Usage";

        readonly string header_StdPacking_Bag = "PCS/BAG";
        readonly string header_StdPacking_Ctn = "PCS/CTN";
        readonly string header_StdPacking_String = "STD PACKING";
        readonly string header_Balance = "Balance";
        readonly string header_StockMinLvl = "Min Lvl";

        readonly string header_ActualStock_PCS = "PCS";
        readonly string header_ActualStock_BAG = "BAG";
        readonly string header_ActualStock_CTN = "CTN";
        readonly string header_ActualStock = "ACTUAL STOCK";

        readonly string header_StockDiff = "STOCK DIFF.";
        readonly string header_Remark = "Remark";

        readonly string header_TotalProduction = "Tot Prod.";
        readonly string header_TotalAssembly = "TOT. ASSY (PCS)";
        string header_DailyUsage = "Daily Usage";

        readonly string Type_Product = "PRODUCT";
        readonly string Type_Part = "PART/MAT./PACKAGING";

        readonly string header_StdPacking_White = "StdPacking - White";
        readonly string header_StdPacking_BLue = "StdPacking - Blue";
        readonly string header_StdPacking_Yellow = "StdPacking - Yellow";

        readonly string header_Qty_Yellow = "Yellow Qty";
        readonly string header_Qty_White = "White Qty";
        readonly string header_Qty_Blue = "Blue Qty";
        readonly string header_Est_Stock_Zero_Date = "Est. Stock Zero";
        readonly string header_Start_Prod_By_Date = "Est. Start Prod. By";



        private bool Loaded = false;
        private bool STOCK_CHECK_MODE = false;

        private readonly string header_ItemSize_Numerator = "SIZE_NUMERATOR";
        private readonly string header_ItemSize_Denominator = "SIZE_DENOMINATOR";
        private readonly string header_SizeUnit = "SIZE UNIT";
        private readonly string header_ItemType = "ITEM TYPE";
        private readonly string header_Size_1 = "Size 1";
        private readonly string header_Size_2 = "Size 2";
        private readonly string header_SBB_Type = "SBB Type";
        private readonly string header_SBB_Category = "SBB Category";
        private readonly string header_ItemString = "ITEM";


        private readonly string header_QtyPerPkt = "QTY PER PKT";

        private readonly string header_CustID = "CUST ID";
        private readonly string header_CustShortName = "CUSTOMER";

        private readonly string header_TotalBag = "TOTAL BAGS";
        private readonly string header_TotalPkt = "TOTAL PKTS";
        private readonly string header_TotalPcs = "TOTAL PCS";
        private readonly string header_TotalBalPcs = "TOTAL BAL PCS";

        private readonly string header_TotalString = "TOTAL";

        private readonly string header_DeliveredPkt = "DELIVERED PKT";
        private readonly string header_DeliveredPcs = "DELIVERED PCS";
        private readonly string header_DeliveredBag = "DELIVERED BAG";


        readonly string header_POTblCode = "P/O TBL CODE";
        readonly string header_POCode = "P/O CODE";
        readonly string header_PONoString = "P/O NO";
        readonly string header_PODate = "P/O RECEIVED DATE";
        readonly string header_CustomerCode = "CUSTOMER CODE";
        readonly string header_Customer = "CUSTOMER";
        readonly string header_Progress = "PROGRESS";
        readonly string header_Selected = "SELECTED";
        readonly string header_DataMode = "DATA MODE";
        readonly string header_DONoString = "D/O NO";


        readonly string header_Size = "SIZE";
        readonly string header_Unit = "UNIT";
        readonly string header_Type = "TYPE";
        readonly string header_OrderQty = "ORDER QTY";
        readonly string header_DeliveredQty = "DELIVERED QTY";
        readonly string header_Note = "NOTE";
        readonly string header_StockCheck = "Stock Check";
        readonly string header_DONo = "D/O #";
        readonly string header_PONo = "P/O NO";
        readonly string header_DataType = "DATA TYPE";
        readonly string header_DeliveredDate = "DELIVERED";

        private DataTable NewPOTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(header_PODate, typeof(DateTime));

            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONoString, typeof(string));



            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_Progress, typeof(string));

            dt.Columns.Add(header_DONoString, typeof(string));

            return dt;
        }

        private DataTable NewDOTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DataType, typeof(string));
            dt.Columns.Add(header_DONo, typeof(int));
            dt.Columns.Add(header_DONoString, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));

            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_DeliveredDate, typeof(DateTime));


            return dt;
        }

        private DataTable NewDeliveredTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));

            //dt.Columns.Add(header_ItemCode, typeof(string));
            //dt.Columns.Add(header_ItemSize_Numerator, typeof(int));
            //dt.Columns.Add(header_ItemSize_Denominator, typeof(int));
            //dt.Columns.Add(header_SizeUnit, typeof(string));
            //dt.Columns.Add(header_ItemType, typeof(string));
            //dt.Columns.Add(header_ItemString, typeof(string));

            //dt.Columns.Add(header_QtyPerBag, typeof(int));

            dt.Columns.Add(header_CustID, typeof(int));
            dt.Columns.Add(header_CustShortName, typeof(string));

            dt.Columns.Add(header_TotalPcs, typeof(int));

            dt.Columns.Add(header_TotalBag, typeof(int));
            dt.Columns.Add(header_TotalPkt, typeof(int));

            dt.Columns.Add(header_TotalBalPcs, typeof(int));

            //dt.Columns.Add(header_TotalString, typeof(string));

            return dt;
        }

        public void LoadDataSourceCMB(ComboBox cmb)
        {
            cmb.DataSource = null;

            DataTable dt = new DataTable();
            dt.Columns.Add("SOURCE");

            dt.Rows.Add(DataSource_GoogleDrive);


            if (MainDashboard.myconnstrng == text.DB_Semenyih)
            {
                dt.Rows.Add(DataSource_LocalDB + "(SEMENYIH)");
            }
            else if (MainDashboard.myconnstrng == text.DB_OUG)
            {
                dt.Rows.Add(DataSource_LocalDB + "(OUG)");

            }
            else if (MainDashboard.myconnstrng == text.DB_JunPC)
            {

                dt.Rows.Add(DataSource_LocalDB + "(JUN)");
            }
            else
            {

                dt.Rows.Add(DataSource_LocalDB + "(UNKNOWN)");
            }


            cmb.DataSource = dt;
            cmb.DisplayMember = "SOURCE";
        }

        public void LoadStockLocationCMB(ComboBox cmb)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("LOCATION");

            dt.Rows.Add(text.Factory_Semenyih);
            dt.Rows.Add(text.Factory_SMY_AssemblyLine);
            dt.Rows.Add("ALL");

            cmb.DataSource = dt;
            cmb.DisplayMember = "LOCATION";

            cmb.Text = "ALL";
        }

        public void LoadTypeCMB(ComboBox cmb)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("TYPE");

            dt.Rows.Add(Type_Part);
            dt.Rows.Add(Type_Product);

            cmb.DataSource = dt;
            cmb.DisplayMember = "TYPE";
        }

        private string STOCKALERT_SORTBY_BALANCE_AFTER = "Bal. After";
        private string STOCKALERT_SORTBY_TYPE = "Item Type";
        public void LoadStockAlertSortTypeCMB(ComboBox cmb)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Sort Type");

            dt.Rows.Add(STOCKALERT_SORTBY_BALANCE_AFTER);
            dt.Rows.Add(STOCKALERT_SORTBY_TYPE);

            cmb.DataSource = dt;
            cmb.DisplayMember = "Sort Type";
        }

        private DataTable NewStockInfoTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_ItemType, typeof(int));
            dt.Columns.Add(header_ItemCategory, typeof(int));

            dt.Columns.Add(header_SBB_Type, typeof(int));
            dt.Columns.Add(header_SBB_Category, typeof(int));
            dt.Columns.Add(header_Size_1, typeof(int));
            dt.Columns.Add(header_Size_2, typeof(int));

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));

            dt.Columns.Add(header_Stock, typeof(int));
            dt.Columns.Add(header_BalAfter, typeof(double));
            dt.Columns.Add(header_QtyPerBag, typeof(int));
            dt.Columns.Add(header_BalAfterBag, typeof(int));
            dt.Columns.Add(header_BalAfterPcs, typeof(int));

            dt.Columns.Add(header_BalAfterPlan, typeof(double));
            dt.Columns.Add(header_BalAfterPlanBag, typeof(int));
            dt.Columns.Add(header_BalAfterPlanPcs, typeof(int));

            dt.Columns.Add(header_Produced, typeof(int));
            dt.Columns.Add(header_ProduceTarget, typeof(int));
            dt.Columns.Add(header_ProDaysNeeded, typeof(float));
            dt.Columns.Add(header_Status, typeof(string));
            dt.Columns.Add(header_Note, typeof(string));
            dt.Columns.Add(header_StockMinLvl, typeof(int));

            dt.Columns.Add(header_StdPacking_Bag, typeof(int));
            dt.Columns.Add(header_StdPacking_Ctn, typeof(int));
            dt.Columns.Add(header_StdPacking_String, typeof(string));

            dt.Columns.Add(header_StdPacking_White, typeof(int));
            dt.Columns.Add(header_Qty_White, typeof(int));
            dt.Columns.Add(header_StdPacking_BLue, typeof(int));
            dt.Columns.Add(header_Qty_Blue, typeof(int));
            dt.Columns.Add(header_StdPacking_Yellow, typeof(int));
            dt.Columns.Add(header_Qty_Yellow, typeof(int));
            dt.Columns.Add(header_Balance, typeof(int));

            dt.Columns.Add(header_ActualStock, typeof(int));
            dt.Columns.Add(header_StockDiff, typeof(int));
            dt.Columns.Add(text.Header_Transfer_Qty, typeof(int));


            dt.Columns.Add(header_TotalProduction, typeof(int));
            dt.Columns.Add(header_TotalAssembly, typeof(int));


            DateTime dateStart = dtpDate1.Value;
            DateTime dateEnd = dtpDate2.Value;

            if (dateStart > dateEnd)
            {
                dateStart = dtpDate2.Value;
                dateEnd = dtpDate1.Value;
            }

            int totalDay = tool.getNumberOfDayBetweenTwoDate(dateStart, dateEnd, false);

            header_DailyUsage = "Daily Usage (" + totalDay + " days)";

            dt.Columns.Add(header_DailyUsage, typeof(int));
            dt.Columns.Add(header_MonthlyUsage, typeof(int));

            dt.Columns.Add(header_Est_Stock_Zero_Date, typeof(DateTime));
            dt.Columns.Add(header_Start_Prod_By_Date, typeof(DateTime));
            dt.Columns.Add(header_MaxProdPerDay, typeof(int));
            

            dt.Columns.Add(header_Remark, typeof(string));



            return dt;
        }

        private DataTable NewUsageTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));

            dt.Columns.Add(header_TotalProduction, typeof(int));
            dt.Columns.Add(header_TotalAssembly, typeof(int));


            DateTime dateStart = dtpDate1.Value;
            DateTime dateEnd = dtpDate2.Value;

            if (dateStart > dateEnd)
            {
                dateStart = dtpDate2.Value;
                dateEnd = dtpDate1.Value;
            }

            int totalDay = tool.getNumberOfDayBetweenTwoDate(dateStart, dateEnd, false);

            header_DailyUsage = "Daily Usage (" + totalDay + " days)";

            dt.Columns.Add(header_DailyUsage, typeof(int));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            //dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgv.Columns[header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_BalAfter].DefaultCellStyle.Format = "0.###";
            dgv.Columns[header_BalAfterPlan].DefaultCellStyle.Format = "0.###";
            dgv.Columns[header_ItemCode].Visible = false;

            if (dgv == dgvStockAlert)
            {

                dgv.Columns[header_Produced].Visible = false;
                dgv.Columns[header_ProduceTarget].Visible = false;
                dgv.Columns[header_Status].Visible = false;
                dgv.Columns[header_QtyPerBag].Visible = false;
                dgv.Columns[header_ItemType].Visible = false;
                dgv.Columns[header_ItemCategory].Visible = false;
                dgv.Columns[header_ItemCode].Visible = true;

                dgv.Columns[header_Remark].Visible = false;
                dgv.Columns[header_StdPacking_Bag].Visible = false;
                dgv.Columns[header_StdPacking_Ctn].Visible = false;
                dgv.Columns[header_StdPacking_String].Visible = false;
               // dgv.Columns[header_ActualStock_PCS].Visible = false;
                //dgv.Columns[header_ActualStock_BAG].Visible = false;
                //dgv.Columns[header_ActualStock_CTN].Visible = false;

                dgv.Columns[header_SBB_Type].Visible = false;
                dgv.Columns[header_SBB_Category].Visible = false;
                dgv.Columns[header_Size_1].Visible = false;
                dgv.Columns[header_Size_2].Visible = false;

                dgv.Columns[header_Qty_White].Visible = false;
                dgv.Columns[header_StdPacking_White].Visible = false;
                dgv.Columns[header_Qty_Blue].Visible = false;
                dgv.Columns[header_StdPacking_BLue].Visible = false;
                dgv.Columns[header_StdPacking_Yellow].Visible = false;
                dgv.Columns[header_Qty_Yellow].Visible = false;
                dgv.Columns[header_Balance].Visible = false;
                dgv.Columns[text.Header_Transfer_Qty].Visible = false;
                dgv.Columns[header_ProDaysNeeded].Visible = false;

                dgv.Columns[header_Qty_White].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_StdPacking_White].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Qty_Blue].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_StdPacking_BLue].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_StdPacking_Yellow].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Qty_Yellow].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Balance].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



                //dgv.Columns[header_ItemCode].DefaultCellStyle.ForeColor = Color.Gray;
                //dgv.Columns[header_ItemCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

                dgv.Columns[header_ItemName].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                dgv.Columns[header_ItemCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                dgv.Columns[header_StdPacking_String].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                dgv.Columns[header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                dgv.Columns[header_StockMinLvl].DefaultCellStyle.BackColor = Color.LightYellow;
                dgv.Columns[header_BalAfter].DefaultCellStyle.BackColor = Color.LightBlue;
                dgv.Columns[header_BalAfterPlan].DefaultCellStyle.BackColor = Color.LightCyan;

                dgv.Columns[header_Qty_Blue].DefaultCellStyle.BackColor = Color.Blue;
                dgv.Columns[header_Qty_Blue].DefaultCellStyle.ForeColor = Color.White;
                dgv.Columns[header_Qty_Yellow].DefaultCellStyle.BackColor = Color.Yellow;
                dgv.Columns[header_Qty_White].DefaultCellStyle.BackColor = Color.LightGray;

                dgv.Columns[header_Qty_White].Width = 100;
                dgv.Columns[header_Qty_Blue].Width = 100;
                dgv.Columns[header_Qty_Yellow].Width = 100;


                dgv.Columns[header_Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgv.Columns[header_ItemName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgv.Columns[header_BalAfter].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_BalAfterPlan].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ActualStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[header_StockDiff].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[header_BalAfterBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_BalAfterPcs].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_StockMinLvl].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_TotalProduction].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_TotalAssembly].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_DailyUsage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ActualStock].DefaultCellStyle.BackColor = SystemColors.Info;


                if (STOCK_CHECK_MODE)
                {
                    dgv.Columns[header_BalAfter].Visible = false;
                    dgv.Columns[header_BalAfterPlan].Visible = false;
                    
                }
                else
                {
                    dgv.Columns[header_ActualStock].Visible = false;
                    dgv.Columns[header_StockDiff].Visible = false;

                    if (STOCK_INFO_ITEM_TYPE == Type_Part)
                    {
                        dgv.Columns[header_Stock].Visible = true;

                        dgv.Columns[header_BalAfter].Visible = true;
                        dgv.Columns[header_BalAfterBag].Visible = false;
                        dgv.Columns[header_BalAfterPcs].Visible = false;

                        dgv.Columns[header_BalAfterPlan].Visible = true;
                        dgv.Columns[header_BalAfterPlanBag].Visible = false;
                        dgv.Columns[header_BalAfterPlanPcs].Visible = false;

                        //dgv.Columns[header_ProDaysNeeded].Visible = true;
                        dgv.Columns[header_Note].Visible = true;
                    }
                    else if (STOCK_INFO_ITEM_TYPE == Type_Product)
                    {
                        dgv.Columns[header_Stock].Visible = false;
                        //dgv.Columns[header_ProDaysNeeded].Visible = false;
                        dgv.Columns[header_Note].Visible = false;

                        if (cbInBagUnit.Checked)
                        {

                            dgv.Columns[header_BalAfter].Visible = false;
                            dgv.Columns[header_BalAfterBag].Visible = true;
                            dgv.Columns[header_BalAfterPcs].Visible = true;

                            dgv.Columns[header_BalAfterPlan].Visible = false;
                            dgv.Columns[header_BalAfterPlanBag].Visible = true;
                            dgv.Columns[header_BalAfterPlanPcs].Visible = true;

                        }
                        else if (cbInPcsUnit.Checked)
                        {

                            dgv.Columns[header_BalAfter].Visible = true;
                            dgv.Columns[header_BalAfterBag].Visible = false;
                            dgv.Columns[header_BalAfterPcs].Visible = false;

                            dgv.Columns[header_BalAfterPlan].Visible = true;
                            dgv.Columns[header_BalAfterPlanBag].Visible = false;
                            dgv.Columns[header_BalAfterPlanPcs].Visible = false;

                        }

                    }


                    //dgv.Columns[header_ProDaysNeeded].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns[header_Note].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[header_Note].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgv.Columns[header_ActualStock].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                }
            }

            //else if (dgv == dgvUsage)
            //{
            //    dgv.Columns[header_TotalProduction].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    dgv.Columns[header_TotalAssembly].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    dgv.Columns[header_AVGAssembly].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}

        }

        private void frmSPP_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.SBBFormOpen = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
            //frmSPPInventory frm = new frmSPPInventory
            //{
            //    StartPosition = FormStartPosition.CenterScreen
            //};


            //frm.ShowDialog();

        }

        private string GetChildCode(DataTable dt, string parentCode)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (parentCode == row[dalJoin.ParentCode].ToString())
                {
                    string childCode = row[dalJoin.ChildCode].ToString();

                    if (childCode.Substring(0, 2) == "CF")
                    {
                        parentCode = childCode;

                        break;
                    }
                }

            }

            return parentCode;
        }


        private DataTable NEWLoadMatPartList(DataTable dt_Product)
        {
            DataTable dt_ChildPart = NewStockInfoTable();
            DataTable dt_ChildMat = NewStockInfoTable();
            DataTable dt_Packaging = NewStockInfoTable();

            string stockLocation = cmbLocation.Text;


            foreach (DataRow row in dt_Product.Rows)
            {
                string itemCode = row[header_ItemCode].ToString();

                var size_1 = row[header_Size_1];
                var size_2 = row[header_Size_2];
                //var SBB_Category = row[header_SBB_Category];
                var SBB_Type = row[header_SBB_Type];

                string parentCode = itemCode;

                if (itemCode[7].ToString() == "E" && itemCode[8].ToString() != "C" && itemCode[10].ToString() != "9" && itemCode[10].ToString() != "1")
                {
                    parentCode = GetChildCode(dt_JoinSelectWithChildCat, row[header_ItemCode].ToString());
                }

                int stillNeed = int.TryParse(row[header_BalAfter].ToString(), out stillNeed) ? stillNeed : 0;
                int stillNeedAfterPlan = int.TryParse(row[header_BalAfterPlan].ToString(), out stillNeedAfterPlan) ? stillNeedAfterPlan : 0;

                stillNeed = stillNeed > 0 ? 0 : stillNeed * -1;
                stillNeedAfterPlan = stillNeedAfterPlan > 0 ? 0 : stillNeedAfterPlan * -1;

                foreach (DataRow rowJoin in dt_JoinSelectWithChildCat.Rows)
                {
                    if (rowJoin[dalJoin.ParentCode].ToString() == parentCode)
                    {
                        int itemType = int.TryParse(rowJoin[dalSBB.TypeTblCode].ToString(), out int i) ? i : 99;
                        int itemCat = int.TryParse(rowJoin[dalSBB.CategoryTblCode].ToString(), out i) ? i : 99;

                        string childCat = rowJoin[dalJoin.ChildCat].ToString();
                        string childCode = rowJoin[dalJoin.ChildCode].ToString();
                        string childName = rowJoin[dalJoin.ChildName].ToString();

                        if (childCode == "CF20G")
                        {
                            var checkpoint = 1;
                        }


                        if (!string.IsNullOrEmpty(childCode))
                        {
                            string childMat = rowJoin[dalItem.ItemMaterial].ToString();
                            string childRecycle = rowJoin[dalItem.ItemRecycleMat].ToString();
                            string childColorMat = rowJoin[dalItem.ItemMBatch].ToString();


                            float childRawRatio = float.TryParse(rowJoin[dalItem.ItemRawRatio].ToString(), out childRawRatio) ? childRawRatio : 0;
                            float childRecycleRatio = float.TryParse(rowJoin[dalItem.ItemRecycleRatio].ToString(), out childRecycleRatio) ? childRecycleRatio : 0;
                            float childPWPerShot = float.TryParse(rowJoin[dalItem.ItemProPWShot].ToString(), out childPWPerShot) ? childPWPerShot : 0;
                            float childRWPerShot = float.TryParse(rowJoin[dalItem.ItemProRWShot].ToString(), out childRWPerShot) ? childRWPerShot : 0;
                            float childCavity = float.TryParse(rowJoin[dalItem.ItemCavity].ToString(), out childCavity) ? childCavity : 1;
                            float childColorRate = float.TryParse(rowJoin[dalItem.ItemMBRate].ToString(), out childColorRate) ? childColorRate : 0;

                            if (childColorRate >= 1)
                            {
                                childColorRate /= 100;
                            }

                            float RawMaterialPercentage = 1 - childColorRate;

                            float PWRWPerPcs = (childPWPerShot + childRWPerShot) / childCavity;

                            if (childCavity <= 0)
                            {
                                PWRWPerPcs = 0;
                            }



                            //int readyStock = int.TryParse(rowJoin[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;

                            int readyStock = double.TryParse(rowJoin[dalItem.ItemStock].ToString(), out double tempStock) ? (int)tempStock : 0;

                            if (stockLocation != "ALL")
                            {
                                readyStock = (int)loadStockList(childCode, stockLocation);
                            }
                            else
                            {
                                readyStock = (int)loadStockList(childCode, text.Factory_Semenyih);
                                readyStock += (int)loadStockList(childCode, text.Factory_SMY_AssemblyLine);

                            }

                            int joinQty = int.TryParse(rowJoin[dalJoin.JoinQty].ToString(), out joinQty) ? joinQty : 0;
                            int joinMax = int.TryParse(rowJoin[dalJoin.JoinMax].ToString(), out joinMax) ? joinMax : 0;
                            int joinMin = int.TryParse(rowJoin[dalJoin.JoinMin].ToString(), out joinMin) ? joinMin : 0;



                            float rawRatio = 0;
                            float recycleRatio = 0;

                            if (childRawRatio > 0 && childRecycleRatio <= 0)
                            {
                                rawRatio = 1;
                                recycleRatio = 0;
                            }
                            else if (childRecycleRatio > 0 && childRawRatio <= 0)
                            {
                                recycleRatio = 1;
                                rawRatio = 0;
                            }
                            else if (childRecycleRatio != 0 && childRawRatio != 0)
                            {
                                rawRatio = childRawRatio / (childRawRatio + childRecycleRatio);
                                recycleRatio = childRecycleRatio / (childRawRatio + childRecycleRatio);
                            }

                            float rawMat_Need = 0;
                            float recycleMat_Need = 0;
                            float colorMat_Need = 0;

                            float rawMat_NeedAfterPlan = 0;
                            float recycleMat_NeedAfterPlan = 0;
                            float colorMat_NeedAfterPlan = 0;

                            int child_StillNeed = 0;

                            joinMax = joinMax <= 0 ? 1 : joinMax;
                            joinMin = joinMin <= 0 ? 1 : joinMin;

                            child_StillNeed = stillNeed / joinMax * joinQty;

                            child_StillNeed = stillNeed % joinMax >= joinMin ? child_StillNeed + joinQty : child_StillNeed;

                            int bal = readyStock - child_StillNeed;
                            float totalMaterialNeededInKG = child_StillNeed * PWRWPerPcs / 1000;

                            int child_StillNeedAfterPlan = 0;
                            child_StillNeedAfterPlan = stillNeedAfterPlan / joinMax * joinQty;
                            child_StillNeedAfterPlan = stillNeedAfterPlan % joinMax >= joinMin ? child_StillNeedAfterPlan + joinQty : child_StillNeedAfterPlan;

                            int balAfterPlan = readyStock - child_StillNeedAfterPlan;

                            float totalMaterialNeededInKGAfterPlan = child_StillNeedAfterPlan * PWRWPerPcs / 1000;


                            rawMat_NeedAfterPlan = (float)Math.Round(totalMaterialNeededInKG * RawMaterialPercentage * rawRatio, 3);
                            recycleMat_NeedAfterPlan = (float)Math.Round(totalMaterialNeededInKG * recycleRatio, 3);
                            colorMat_NeedAfterPlan = (float)Math.Round(totalMaterialNeededInKG * childColorRate * rawRatio, 3);


                            rawMat_Need = (float)Math.Round(totalMaterialNeededInKGAfterPlan * RawMaterialPercentage * rawRatio, 3);
                            recycleMat_Need = (float)Math.Round(totalMaterialNeededInKGAfterPlan * recycleRatio, 3);
                            colorMat_Need = (float)Math.Round(totalMaterialNeededInKGAfterPlan * childColorRate * rawRatio, 3);

                            bool childInserted = false;

                            foreach (DataRow mat_row in dt_ChildPart.Rows)
                            {
                                if (childCode == mat_row[header_ItemCode].ToString())
                                {
                                    childInserted = true;

                                    int previousBal = int.TryParse(mat_row[header_BalAfter].ToString(), out previousBal) ? previousBal : 0;
                                    int previousBalAfterPlan = int.TryParse(mat_row[header_BalAfterPlan].ToString(), out previousBalAfterPlan) ? previousBalAfterPlan : 0;

                                    bal = previousBal - child_StillNeed;

                                    balAfterPlan = previousBalAfterPlan - child_StillNeedAfterPlan;


                                    mat_row[header_BalAfter] = bal;
                                    mat_row[header_BalAfterPlan] = balAfterPlan;



                                    break;
                                }
                            }

                            if (!childInserted && childCat == text.Cat_Part)
                            {
                                DataRow alert_row = dt_ChildPart.NewRow();

                                alert_row[header_ItemType] = itemType;
                                alert_row[header_ItemCategory] = itemCat;
                                alert_row[header_ItemCode] = childCode;
                                alert_row[header_ItemName] = childName;
                                alert_row[header_BalAfter] = bal;
                                alert_row[header_BalAfterPlan] = balAfterPlan;
                                alert_row[header_Stock] = readyStock;

                                alert_row[header_SBB_Category] = rowJoin[dalItem.CategoryTblCode];
                                alert_row[header_SBB_Type] = rowJoin[dalItem.TypeTblCode];
                                alert_row[header_Size_1] = rowJoin[dalItem.ItemSize1];
                                alert_row[header_Size_2] = rowJoin[dalItem.ItemSize2];

                                dt_ChildPart.Rows.Add(alert_row);

                                bool matFound = false;
                                bool recycleFound = false;
                                bool colorMatFound = false;

                                foreach (DataRow part in dt_ChildMat.Rows)
                                {
                                    double balAfter = double.TryParse(part[header_BalAfter].ToString(), out balAfter) ? balAfter : 0;
                                    double balAfterPlan_mat = double.TryParse(part[header_BalAfterPlan].ToString(), out balAfterPlan_mat) ? balAfterPlan_mat : 0;

                                    if (childMat == part[header_ItemCode].ToString())
                                    {
                                        matFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - rawMat_Need, 3);
                                        part[header_BalAfterPlan] = Math.Round(balAfterPlan_mat - rawMat_NeedAfterPlan, 3);

                                    }

                                    if (childRecycle == part[header_ItemCode].ToString())
                                    {
                                        recycleFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - recycleMat_Need, 3);
                                        part[header_BalAfterPlan] = Math.Round(balAfterPlan_mat - recycleMat_NeedAfterPlan, 3);
                                    }

                                    if (childColorMat == part[header_ItemCode].ToString())
                                    {
                                        colorMatFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - colorMat_Need, 3);
                                        part[header_BalAfterPlan] = Math.Round(balAfterPlan_mat - colorMat_NeedAfterPlan, 3);
                                    }
                                }

                                if (!matFound && !string.IsNullOrEmpty(childMat))
                                {
                                    alert_row = dt_ChildMat.NewRow();

                                    float stock = tool.getStockQtyFromDataTable(dt_Item, childMat);
                                    //int rawMat_Int
                                    alert_row[header_ItemType] = 91;
                                    alert_row[header_ItemCategory] = 91;
                                    alert_row[header_ItemCode] = childMat;
                                    alert_row[header_ItemName] = tool.getItemNameFromDataTable(dt_Item, childMat);
                                    alert_row[header_BalAfter] = rawMat_Need;
                                    alert_row[header_BalAfterPlan] = rawMat_NeedAfterPlan;
                                    alert_row[header_Stock] = stock;

                                    dt_ChildMat.Rows.Add(alert_row);
                                }

                                if (!recycleFound && !string.IsNullOrEmpty(childRecycle))
                                {
                                    alert_row = dt_ChildMat.NewRow();

                                    float stock = tool.getStockQtyFromDataTable(dt_Item, childRecycle);

                                    alert_row[header_ItemType] = 92;
                                    alert_row[header_ItemCategory] = 92;
                                    alert_row[header_ItemCode] = childRecycle;
                                    alert_row[header_ItemName] = tool.getItemNameFromDataTable(dt_Item, childRecycle);
                                    alert_row[header_BalAfter] = recycleMat_Need;
                                    alert_row[header_BalAfterPlan] = recycleMat_NeedAfterPlan;
                                    alert_row[header_Stock] = stock;

                                    dt_ChildMat.Rows.Add(alert_row);
                                }

                                if (!colorMatFound && !string.IsNullOrEmpty(childColorMat))
                                {
                                    alert_row = dt_ChildMat.NewRow();

                                    float stock = tool.getStockQtyFromDataTable(dt_Item, childColorMat);

                                    alert_row[header_ItemType] = 93;
                                    alert_row[header_ItemCategory] = 93;
                                    alert_row[header_ItemCode] = childColorMat;
                                    alert_row[header_ItemName] = tool.getItemNameFromDataTable(dt_Item, childColorMat);
                                    alert_row[header_BalAfter] = colorMat_Need;
                                    alert_row[header_BalAfterPlan] = colorMat_NeedAfterPlan;
                                    alert_row[header_Stock] = stock;

                                    dt_ChildMat.Rows.Add(alert_row);
                                }

                            }
                            else if (childCat == text.Cat_Part)
                            {

                                DataRow alert_row;
                                bool matFound = false;
                                bool recycleFound = false;
                                bool colorMatFound = false;

                                foreach (DataRow part in dt_ChildMat.Rows)
                                {
                                    double balAfter = double.TryParse(part[header_BalAfter].ToString(), out balAfter) ? balAfter : 0;
                                    double balAfterPlan_mat = double.TryParse(part[header_BalAfterPlan].ToString(), out balAfterPlan_mat) ? balAfterPlan_mat : 0;

                                    if (childMat == part[header_ItemCode].ToString())
                                    {
                                        matFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - rawMat_Need, 3);
                                        part[header_BalAfterPlan] = Math.Round(balAfterPlan_mat - rawMat_NeedAfterPlan, 3);

                                    }

                                    if (childRecycle == part[header_ItemCode].ToString())
                                    {
                                        recycleFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - recycleMat_Need, 3);
                                        part[header_BalAfterPlan] = Math.Round(balAfterPlan_mat - recycleMat_NeedAfterPlan, 3);
                                    }

                                    if (childColorMat == part[header_ItemCode].ToString())
                                    {
                                        colorMatFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - colorMat_Need, 3);
                                        part[header_BalAfterPlan] = Math.Round(balAfterPlan_mat - colorMat_NeedAfterPlan, 3);
                                    }
                                }

                                if (!matFound && !string.IsNullOrEmpty(childMat))
                                {
                                    alert_row = dt_ChildMat.NewRow();

                                    float stock = tool.getStockQtyFromDataTable(dt_Item, childMat);
                                    //int rawMat_Int
                                    alert_row[header_ItemType] = 94;
                                    alert_row[header_ItemCategory] = 94;
                                    alert_row[header_ItemCode] = childMat;
                                    alert_row[header_ItemName] = tool.getItemNameFromDataTable(dt_Item, childMat);
                                    alert_row[header_BalAfter] = rawMat_Need;
                                    alert_row[header_BalAfterPlan] = rawMat_NeedAfterPlan;
                                    alert_row[header_Stock] = stock;
                                    alert_row[header_SBB_Category] = 94;
                                    dt_ChildMat.Rows.Add(alert_row);
                                }

                                if (!recycleFound && !string.IsNullOrEmpty(childRecycle))
                                {
                                    alert_row = dt_ChildMat.NewRow();

                                    float stock = tool.getStockQtyFromDataTable(dt_Item, childRecycle);

                                    alert_row[header_ItemType] = 95;
                                    alert_row[header_ItemCategory] = 95;
                                    alert_row[header_SBB_Category] = 95;

                                    alert_row[header_ItemCode] = childRecycle;
                                    alert_row[header_ItemName] = tool.getItemNameFromDataTable(dt_Item, childRecycle);
                                    alert_row[header_BalAfter] = recycleMat_Need;
                                    alert_row[header_BalAfterPlan] = recycleMat_NeedAfterPlan;
                                    alert_row[header_Stock] = stock;

                                    dt_ChildMat.Rows.Add(alert_row);
                                }

                                if (!colorMatFound && !string.IsNullOrEmpty(childColorMat))
                                {
                                    alert_row = dt_ChildMat.NewRow();

                                    float stock = tool.getStockQtyFromDataTable(dt_Item, childColorMat);

                                    alert_row[header_ItemType] = 96;
                                    alert_row[header_ItemCategory] = 96;
                                    alert_row[header_SBB_Category] = 96;

                                    alert_row[header_ItemCode] = childColorMat;
                                    alert_row[header_ItemName] = tool.getItemNameFromDataTable(dt_Item, childColorMat);
                                    alert_row[header_BalAfter] = colorMat_Need;
                                    alert_row[header_BalAfterPlan] = colorMat_NeedAfterPlan;
                                    alert_row[header_Stock] = stock;

                                    dt_ChildMat.Rows.Add(alert_row);
                                }
                            }
                            else if (childCat != text.Cat_Part)
                            {
                                DataRow alert_row;
                                bool otherMaterialFound = false;

                                foreach (DataRow part in dt_Packaging.Rows)
                                {
                                    if (childCode == part[header_ItemCode].ToString())
                                    {
                                        otherMaterialFound = true;

                                        int previousBal = int.TryParse(part[header_BalAfter].ToString(), out previousBal) ? previousBal : 0;
                                        int previousBalAfterPlan = int.TryParse(part[header_BalAfterPlan].ToString(), out previousBalAfterPlan) ? previousBalAfterPlan : 0;

                                        bal = previousBal - child_StillNeed;
                                        balAfterPlan = previousBalAfterPlan - child_StillNeedAfterPlan;

                                        part[header_BalAfter] = bal;
                                        part[header_BalAfterPlan] = balAfterPlan;

                                        break;
                                    }

                                }

                                if (!otherMaterialFound && !string.IsNullOrEmpty(childCode))
                                {

                                    alert_row = dt_Packaging.NewRow();
                                    alert_row[header_ItemType] = itemType;
                                    alert_row[header_ItemCategory] = itemCat;
                                    alert_row[header_ItemCode] = childCode;
                                    alert_row[header_ItemName] = childName;
                                    alert_row[header_BalAfter] = bal;
                                    alert_row[header_BalAfterPlan] = balAfterPlan;
                                    alert_row[header_Stock] = readyStock;

                                    dt_Packaging.Rows.Add(alert_row);

                                }
                            }
                        }



                    }
                }
            }

            if (dt_ChildMat != null && dt_ChildMat.Rows.Count > 0)//&& cbMat.Checked
            {
                foreach (DataRow row in dt_ChildMat.Rows)
                {
                    DataRow newRow = dt_ChildPart.NewRow();

                    newRow[header_ItemType] = 98;
                    newRow[header_ItemCategory] = 98;
                    newRow[header_SBB_Category] = 98;

                    newRow[header_ItemCode] = row[header_ItemCode];
                    newRow[header_ItemName] = row[header_ItemName];
                    newRow[header_BalAfter] = row[header_BalAfter];
                    newRow[header_Stock] = row[header_Stock];

                    dt_ChildPart.Rows.Add(newRow);
                }
            }

            if (dt_Packaging != null && dt_Packaging.Rows.Count > 0) //&& cbPackaging.Checked
            {
                foreach (DataRow row in dt_Packaging.Rows)
                {
                    DataRow newRow = dt_ChildPart.NewRow();

                    newRow[header_ItemType] = 97;
                    newRow[header_ItemCategory] = 97;
                    newRow[header_SBB_Category] = 97;

                    newRow[header_ItemCode] = row[header_ItemCode];
                    newRow[header_ItemName] = row[header_ItemName];
                    newRow[header_BalAfter] = row[header_BalAfter];
                    newRow[header_Stock] = row[header_Stock];

                    dt_ChildPart.Rows.Add(newRow);
                }
            }


            return dt_ChildPart;
        }

        private DataTable RearrangeIndex(DataTable dt)
        {
            int index = 1;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row[header_Index] = index++;
                }

                dt.AcceptChanges();
            }

            return dt;
        }

        private DataTable LoadMatPartList(DataTable dt_Product)
        {
            DataTable dt_MatPart = NewStockInfoTable();
            //DataTable dt_Plan = dalPlan.Select();

            //int index = 1;
            //DataTable dtJoin = dalJoin.Select();

            //foreach (DataRow row in dt_Product.Rows)
            //{
            //    string parentCode = row[header_ItemCode].ToString();

            //    if(!string.IsNullOrEmpty(parentCode))
            //    {
            //        if (parentCode[7].ToString() == "E")
            //        {
            //            parentCode = GetChildCode(dtJoin, parentCode);
            //        }

            //        int stillNeed_1 = int.TryParse(row[header_BalAfter1].ToString(), out stillNeed_1) ? stillNeed_1 : 0;
            //        int stillNeed_2 = int.TryParse(row[header_BalAfter2].ToString(), out stillNeed_2) ? stillNeed_2 : 0;
            //        int stillNeed_3 = int.TryParse(row[header_BalAfter3].ToString(), out stillNeed_3) ? stillNeed_3 : 0;

            //        stillNeed_1 = stillNeed_1 > 0 ? 0 : stillNeed_1 * -1;
            //        stillNeed_2 = stillNeed_2 > 0 ? 0 : stillNeed_2 * -1;
            //        stillNeed_3 = stillNeed_3 > 0 ? 0 : stillNeed_3 * -1;

            //        foreach (DataRow rowJoin in dtJoin.Rows)
            //        {
            //            if (rowJoin[dalJoin.ParentCode].ToString() == parentCode)
            //            {
            //                string childCode = rowJoin[dalJoin.ChildCode].ToString();
            //                string childName = rowJoin[dalJoin.ChildName].ToString();

            //                int readyStock = int.TryParse(rowJoin[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;
            //                int joinQty = int.TryParse(rowJoin[dalJoin.JoinQty].ToString(), out joinQty) ? joinQty : 0;
            //                int joinMax = int.TryParse(rowJoin[dalJoin.JoinMax].ToString(), out joinMax) ? joinMax : 0;
            //                int joinMin = int.TryParse(rowJoin[dalJoin.JoinMin].ToString(), out joinMin) ? joinMin : 0;

            //                int child_StillNeed_1 = 0;
            //                int child_StillNeed_2 = 0;
            //                int child_StillNeed_3 = 0;

            //                joinMax = joinMax <= 0 ? 1 : joinMax;
            //                joinMin = joinMin <= 0 ? 1 : joinMin;

            //                child_StillNeed_1 = stillNeed_1 / joinMax * joinQty;

            //                child_StillNeed_1 = stillNeed_1 % joinMax >= joinMin ? child_StillNeed_1 + joinQty : child_StillNeed_1;

            //                child_StillNeed_2 = stillNeed_2 / joinMax * joinQty;

            //                child_StillNeed_2 = stillNeed_2 % joinMax >= joinMin ? child_StillNeed_2 + joinQty : child_StillNeed_2;

            //                child_StillNeed_3 = stillNeed_3 / joinMax * joinQty;

            //                child_StillNeed_3 = stillNeed_3 % joinMax >= joinMin ? child_StillNeed_3 + joinQty : child_StillNeed_3;

            //                int bal_1 = readyStock - child_StillNeed_1;
            //                int bal_2 = readyStock - child_StillNeed_2;
            //                int bal_3 = readyStock - child_StillNeed_3;

            //                bool childInserted = false;

            //                foreach (DataRow mat_row in dt_MatPart.Rows)
            //                {
            //                    if (childCode == mat_row[header_ItemCode].ToString())
            //                    {
            //                        childInserted = true;

            //                        int previousBal_1 = int.TryParse(mat_row[header_BalAfter1].ToString(), out previousBal_1) ? previousBal_1 : 0;
            //                        int previousBal_2 = int.TryParse(mat_row[header_BalAfter2].ToString(), out previousBal_2) ? previousBal_2 : 0;
            //                        int previousBal_3 = int.TryParse(mat_row[header_BalAfter3].ToString(), out previousBal_3) ? previousBal_3 : 0;

            //                        bal_1 = previousBal_1 - child_StillNeed_1;
            //                        bal_2 = previousBal_2 - child_StillNeed_2;
            //                        bal_3 = previousBal_3 - child_StillNeed_3;

            //                        mat_row[header_BalAfter1] = bal_1;
            //                        mat_row[header_BalAfter2] = bal_2;
            //                        mat_row[header_BalAfter3] = bal_3;

            //                        break;
            //                    }
            //                }

            //                if (!childInserted)//!childInserted
            //                {
            //                    var productionQty = GetProductionTargetAndProducedQty(dt_Plan, childCode);

            //                    int targetQty = productionQty.Item1;
            //                    int producedQty = productionQty.Item2;
            //                    string planStatus = productionQty.Item3;

            //                    DataRow alert_row = dt_MatPart.NewRow();

            //                    alert_row[header_Index] = index;
            //                    alert_row[header_ItemCode] = childCode;
            //                    alert_row[header_ItemName] = childName;
            //                    alert_row[header_Stock] = readyStock;
            //                    alert_row[header_BalAfter1] = bal_1;
            //                    alert_row[header_BalAfter2] = bal_2;
            //                    alert_row[header_BalAfter3] = bal_3;

            //                    alert_row[header_ProduceTarget] = targetQty;
            //                    alert_row[header_Produced] = producedQty;
            //                    alert_row[header_Status] = planStatus;

            //                    if (targetQty > 0)
            //                    {
            //                        alert_row[header_ProductionString] = producedQty + "/" + targetQty;
            //                    }


            //                    dt_MatPart.Rows.Add(alert_row);
            //                    index++;
            //                }


            //            }
            //        }
            //    }


            //}

            return dt_MatPart;
        }

        private Tuple<int, int, string> GetProductionTargetAndProducedQty(DataTable dt_Plan, string itemCode)
        {
            int targetQty = 0;
            int producedQty = 0;
            string running = "";
            foreach (DataRow row in dt_Plan.Rows)
            {
                string planStatus = row[dalPlan.planStatus].ToString();
                string planItem = row[dalPlan.partCode].ToString();

                if (planItem == itemCode && (planStatus == text.planning_status_running || planStatus == text.planning_status_pending))
                {
                    targetQty += int.TryParse(row[dalPlan.targetQty].ToString(), out int i) ? i : 0;
                    producedQty += int.TryParse(row[dalPlan.planProduced].ToString(), out i) ? i : 0;

                    if (planStatus == text.planning_status_running)
                    {
                        running = planStatus;
                    }
                }
            }

            return Tuple.Create(targetQty, producedQty, running);
        }


        private float loadStockList(string itemCode, string Fac)
        {
            float stockQty = 0;

            if (dt_Stock.Rows.Count > 0)
            {
                foreach (DataRow stock in dt_Stock.Rows)
                {
                    string stockItem = stock[dalItem.ItemCode].ToString();
                    string stockFacName = stock["fac_name"].ToString();

                    if (itemCode.Equals(stockItem) && Fac.Equals(stockFacName))
                    {
                        stockQty = float.TryParse(stock["stock_qty"].ToString(), out stockQty) ? stockQty : 0;
                        return stockQty;
                    }
                }
            }
            return stockQty;
        }

        //private void OLD_LoadStockAlert()
        //{
        //    Cursor = Cursors.WaitCursor;

        //    //DataTable dt_PendingPOSelect = dalSBB.PendingPOSelect();

        //    DataTable dt_FillAll = NewStockAlertTable();

        //    string stockLocation = "ALL";

        //    //Load SBB GOODS FILL ALL ALERT
        //    foreach (DataRow pendingRow in dt_PendingPOSelect.Rows)
        //    {
        //        string itemCode = pendingRow[dalSBB.ItemCode].ToString();
        //        string itemName = pendingRow[dalSBB.ItemName].ToString();

        //        if(!string.IsNullOrEmpty(itemCode))
        //        {
        //            int itemStock = int.TryParse(pendingRow[dalItem.ItemStock].ToString(), out itemStock) ? itemStock : 0;

        //            if (stockLocation != "ALL")
        //            {
        //                itemStock = (int)loadStockList(itemCode, stockLocation);
        //            }


        //            int itemPOQty = int.TryParse(pendingRow[dalSBB.POQty].ToString(), out itemPOQty) ? itemPOQty : 0;
        //            int itemDeliveredQty = int.TryParse(pendingRow[dalSBB.DeliveredQty].ToString(), out itemDeliveredQty) ? itemDeliveredQty : 0;
        //            int itemPerBag = int.TryParse(pendingRow[dalSBB.QtyPerBag].ToString(), out itemPerBag) ? itemPerBag : 0;

        //            int pendingQty = itemPOQty - itemDeliveredQty;

        //            DataRow newRow;
        //            bool itemFound = false;

        //            if (dt_PendingPOSelect != null && dt_PendingPOSelect.Rows.Count > 0)
        //            {
        //                foreach (DataRow fillAllRow in dt_FillAll.Rows)
        //                {
        //                    if (itemCode == fillAllRow[header_ItemCode].ToString())
        //                    {
        //                        itemFound = true;

        //                        int itemBal = int.TryParse(fillAllRow[header_BalAfter].ToString(), out itemBal) ? itemBal : 0;

        //                        fillAllRow[header_BalAfter] = itemBal - pendingQty;

        //                        fillAllRow[header_BalAfterBag] = (itemBal - pendingQty) / itemPerBag;
        //                        fillAllRow[header_BalAfterPcs] = (itemBal - pendingQty) % itemPerBag;

        //                        break;
        //                    }
        //                }

        //            }

        //            if (!itemFound)
        //            {
        //                newRow = dt_FillAll.NewRow();

        //                newRow[header_ItemCode] = itemCode;
        //                newRow[header_ItemName] = itemName;
        //                newRow[header_BalAfter] = itemStock - pendingQty;
        //                newRow[header_QtyPerBag] = itemPerBag;

        //                if(itemPerBag == 0)
        //                {
        //                    itemPerBag = 1;
        //                }
        //                newRow[header_BalAfterBag] = (itemStock - pendingQty) / itemPerBag;
        //                newRow[header_BalAfterPcs] = (itemStock - pendingQty) % itemPerBag;

        //                dt_FillAll.Rows.Add(newRow);
        //            }

        //        }
        //    }

        //    if (STOCK_INFO_ITEM_TYPE == Type_Part)
        //    {
        //        dt_FillAll = NEWLoadMatPartList(dt_FillAll);
        //    }

        //    //dt_FillAll.DefaultView.Sort = header_BalAfter + " ASC";
        //    //dt_FillAll = dt_FillAll.DefaultView.ToTable();


        //    List<DataRow> toDelete = new List<DataRow>();

        //    foreach (DataRow row in dt_FillAll.Rows)
        //    {
        //        string itemCode = row[header_ItemCode].ToString();
        //        int bal = int.TryParse(row[header_BalAfter].ToString(), out int i) ? i : 0;

        //        if(string.IsNullOrEmpty(row[header_ItemName].ToString()))
        //        {
        //            toDelete.Add(row);
        //        }
        //        else if(bal < 0)
        //        {
        //            row[header_ProDaysNeeded] = tool.GetProductionDayNeeded(dt_Item, itemCode, bal * -1);
        //        }

        //    }

        //    foreach (DataRow dr in toDelete)
        //    {
        //        dt_FillAll.Rows.Remove(dr);
        //    }


        //    dt_FillAll.AcceptChanges();

        //    DataView dv = dt_FillAll.DefaultView;
        //    dv.Sort = header_BalAfter + " ASC," + header_ItemType + " ASC," + header_ItemName + " ASC";
        //    //dv.Sort = header_ItemCategory + " ASC," + header_ItemType + " ASC," + header_ItemName + " ASC";

        //    dt_FillAll = dv.ToTable();

        //    RearrangeIndex(dt_FillAll);

        //    dgvStockAlert.DataSource = dt_FillAll;

        //    DgvUIEdit(dgvStockAlert);

        //    dgvStockAlert.ClearSelection();


        //    Cursor = Cursors.Arrow;
        //}

        planningDAL dalPlanning = new planningDAL();

        DataTable DT_STOCK_ALERT_MASTER_LIST = null;


        private string STOCK_INFO_ITEM_TYPE = "";

        private void LoadStockInfo()
        {
            Cursor = Cursors.WaitCursor;
            STOCK_ALERT_DATA_LOADED = false;

            DT_STOCK_ALERT_MASTER_LIST = null;

            //DataTable dt_PendingPOSelect = dalSBB.PendingPOSelect();

            DataTable dt_FillAll = NewStockInfoTable();

            string stockLocation = cmbLocation.Text;

            header_Stock = "Stock (" + stockLocation + ")";

            //Load SBB GOODS FILL ALL ALERT
            foreach (DataRow pendingRow in dt_PendingPOSelect.Rows)
            {
                string itemCode = pendingRow[dalSBB.ItemCode].ToString();
                string itemName = pendingRow[dalSBB.ItemName].ToString();

                if (!string.IsNullOrEmpty(itemCode))
                {
                    int itemStock = int.TryParse(pendingRow[dalItem.ItemStock].ToString(), out itemStock) ? itemStock : 0;

                    if (stockLocation != "ALL")
                    {
                        itemStock = (int)loadStockList(itemCode, stockLocation);
                    }


                    int itemPOQty = int.TryParse(pendingRow[dalSBB.POQty].ToString(), out itemPOQty) ? itemPOQty : 0;
                    int itemtoDeliverQty = int.TryParse(pendingRow[dalSBB.ToDeliveryQty].ToString(), out itemtoDeliverQty) ? itemtoDeliverQty : 0;
                    int itemDeliveredQty = int.TryParse(pendingRow[dalSBB.DeliveredQty].ToString(), out itemDeliveredQty) ? itemDeliveredQty : 0;
                    int itemPerBag = int.TryParse(pendingRow[dalSBB.QtyPerBag].ToString(), out itemPerBag) ? itemPerBag : 0;

                    int pendingQty = itemPOQty - itemDeliveredQty;

                    DataRow newRow;
                    bool itemFound = false;

                    if (dt_PendingPOSelect != null && dt_PendingPOSelect.Rows.Count > 0)
                    {
                        foreach (DataRow fillAllRow in dt_FillAll.Rows)
                        {
                            if (itemCode == fillAllRow[header_ItemCode].ToString())
                            {
                                itemFound = true;

                                int itemBal = int.TryParse(fillAllRow[header_BalAfter].ToString(), out itemBal) ? itemBal : 0;
                                int itemBalAfterPlan = int.TryParse(fillAllRow[header_BalAfterPlan].ToString(), out itemBalAfterPlan) ? itemBalAfterPlan : 0;

                                fillAllRow[header_BalAfter] = itemBal - pendingQty;

                                fillAllRow[header_BalAfterBag] = (itemBal - pendingQty) / itemPerBag;
                                fillAllRow[header_BalAfterPcs] = (itemBal - pendingQty) % itemPerBag;

                                fillAllRow[header_BalAfterPlan] = itemBalAfterPlan - itemtoDeliverQty;

                                fillAllRow[header_BalAfterPlanBag] = (itemBalAfterPlan - itemtoDeliverQty) / itemPerBag;
                                fillAllRow[header_BalAfterPlanPcs] = (itemBalAfterPlan - itemtoDeliverQty) % itemPerBag;

                                break;
                            }
                        }

                    }

                    if (!itemFound)
                    {
                        newRow = dt_FillAll.NewRow();

                        newRow[header_ItemCode] = itemCode;
                        newRow[header_ItemName] = itemName;
                        newRow[header_QtyPerBag] = itemPerBag;


                        if (itemPerBag == 0)
                        {
                            itemPerBag = 1;
                        }

                        if(itemCode == "(OK) CFEC 20")
                        {
                            var checkpoint = 1;
                        }

                        newRow[header_BalAfter] = itemStock - pendingQty;
                        newRow[header_BalAfterBag] = (itemStock - pendingQty) / itemPerBag;
                        newRow[header_BalAfterPcs] = (itemStock - pendingQty) % itemPerBag;

                        newRow[header_BalAfterPlan] = itemStock - itemtoDeliverQty;
                        newRow[header_BalAfterPlanBag] = (itemStock - itemtoDeliverQty) / itemPerBag;
                        newRow[header_BalAfterPlanPcs] = (itemStock - itemtoDeliverQty) % itemPerBag;

                        newRow[header_SBB_Category] = pendingRow[dalItem.CategoryTblCode];
                        newRow[header_SBB_Type] = pendingRow[dalItem.TypeTblCode];
                        newRow[header_Size_1] = pendingRow[dalItem.ItemSize1];
                        newRow[header_Size_2] = pendingRow[dalItem.ItemSize2];

                        dt_FillAll.Rows.Add(newRow);
                    }

                }
            }

            //dt_SBBCustSearchWithTypeAndSize
            //to add no order product
            foreach (DataRow SBBRow in dt_SBBCustSearchWithTypeAndSize.Rows)
            {
                string SBBItemCode = SBBRow[dalItem.ItemCode].ToString();
                bool itemFound = false;

                foreach (DataRow fillAllRow in dt_FillAll.Rows)
                {
                    string itemCode = fillAllRow[header_ItemCode].ToString();

                    if (itemCode == SBBItemCode)
                    {
                        itemFound = true;
                        break;
                    }

                }

                if (!itemFound)
                {
                    DataRow newRow = dt_FillAll.NewRow();
                    newRow[header_ItemCode] = SBBItemCode;
                    newRow[header_ItemName] = SBBRow[dalItem.ItemName].ToString();

                    int itemStock = int.TryParse(SBBRow[dalItem.ItemStock].ToString(), out itemStock) ? itemStock : 0;

                    newRow[header_BalAfter] = itemStock;

                    newRow[header_SBB_Category] = SBBRow[dalItem.CategoryTblCode];
                    newRow[header_SBB_Type] = SBBRow[dalItem.TypeTblCode];
                    newRow[header_Size_1] = SBBRow[dalItem.ItemSize1];
                    newRow[header_Size_2] = SBBRow[dalItem.ItemSize2];


                    dt_FillAll.Rows.Add(newRow);
                }

            }


            if (STOCK_INFO_ITEM_TYPE == Type_Part)
            {
                dt_FillAll = NEWLoadMatPartList(dt_FillAll);
            }

            //dt_FillAll.DefaultView.Sort = header_BalAfter + " ASC";
            //dt_FillAll = dt_FillAll.DefaultView.ToTable();


            List<DataRow> toDelete = new List<DataRow>();

            DataTable DT_MACHINE_SCHEDULE = dalPlanning.SelectActivePlanning();

            if (dt_Item == null || dt_Item.Rows.Count < 0)
                dt_Item = dalItem.Select();

            foreach (DataRow row in dt_FillAll.Rows)
            {
                string itemCode = row[header_ItemCode].ToString();
                int bal = int.TryParse(row[header_BalAfter].ToString(), out int i) ? i : 0;

                if (string.IsNullOrEmpty(row[header_ItemName].ToString()))
                {
                    toDelete.Add(row);
                }
                else if (bal < 0)
                {
                    row[header_ProDaysNeeded] = tool.GetProductionDayNeeded(dt_Item, itemCode, bal * -1);
                }
                

                    
                row[header_MaxProdPerDay] = tool.GetDailyProductionCapacity(dt_Item, itemCode, 24);

                row[header_Note] = GetProduceQty(itemCode, DT_MACHINE_SCHEDULE);
                row[header_StockMinLvl] = GetMinStockLevel(itemCode, dt_Item);
            }

            foreach (DataRow dr in toDelete)
            {
                dt_FillAll.Rows.Remove(dr);
            }


            dt_FillAll.AcceptChanges();

            DT_STOCK_ALERT_MASTER_LIST = dt_FillAll;

            if (STOCK_INFO_ITEM_TYPE == Type_Part)
            {
                dt_FillAll = FilterItemType();

            }

            DataView dv = dt_FillAll.DefaultView;
            //dv.Sort = header_ItemCategory + " ASC," + header_ItemType + " ASC," + header_ItemName + " ASC";
            //dv.Sort = header_BalAfter + " ASC," + header_ItemType + " ASC," + header_ItemName + " ASC";

            dv.Sort = header_SBB_Category + " ASC," + header_SBB_Type + " ASC," + header_Size_1 + " ASC," + header_Size_2 + " ASC," + header_ItemName + " ASC";


            dt_FillAll = dv.ToTable();

            RearrangeIndex(dt_FillAll);

            dgvStockAlert.DataSource = dt_FillAll;
            LoadUsage();

            DgvUIEdit(dgvStockAlert);
            dgvStockAlert.ClearSelection();
            STOCK_ALERT_DATA_LOADED = true;
            CheckStockLevels();


            Cursor = Cursors.Arrow;
        }

        private void SortByType()
        {
            DataTable dt_FillAll = (DataTable)dgvStockAlert.DataSource;
            DataView dv = dt_FillAll.DefaultView;

            dv.Sort = header_SBB_Category + " ASC," + header_SBB_Type + " ASC," + header_Size_1 + " ASC," + header_Size_2 + " ASC," + header_ItemName + " ASC";

            dt_FillAll = dv.ToTable();

            RearrangeIndex(dt_FillAll);

            dgvStockAlert.DataSource = dt_FillAll;

        }

        private DataTable FilterItemType()
        {
            DataTable FilteredTable = null;

            if (DT_STOCK_ALERT_MASTER_LIST != null)
            {
                FilteredTable = DT_STOCK_ALERT_MASTER_LIST.Copy();
                List<DataRow> toDelete = new List<DataRow>();

                foreach (DataRow row in FilteredTable.Rows)
                {
                    string itemType = row[header_ItemType].ToString();

                    if ((!cbOtherMaterial.Checked && (itemType == "98" || itemType == "97")) ||
                        (!cbChildPart.Checked && itemType != "97" && itemType != "98"))
                    {
                        toDelete.Add(row);
                    }
                }

                foreach (DataRow dr in toDelete)
                {
                    FilteredTable.Rows.Remove(dr);
                }


                FilteredTable.AcceptChanges();
            }
            //else
            //{
            //    LoadStockAlert();
            //}

            return FilteredTable;

        }
        //private Tuple<int, int, string> GetProduceQty(string ItemCode, DataTable dt_MacSechedule)
        //{
        //    int totalTarget = 0, totalProduced = 0;
        //    string mac = "Mac:";

        //    // Use a list to store rows to remove (can't modify collection while iterating)
        //    List<DataRow> rowsToRemove = new List<DataRow>();

        //    foreach (DataRow row in dt_MacSechedule.Rows)
        //    {
        //        string itemCode = row[dalPlanning.partCode].ToString();
        //        if (itemCode == ItemCode)
        //        {
        //            string status = row[dalPlanning.planStatus].ToString();

        //            if (status != text.planning_status_draft)
        //            {
        //                totalTarget += int.TryParse(row[dalPlanning.targetQty].ToString(), out int x) ? x : 0;
        //                totalProduced += int.TryParse(row[dalPlanning.planProduced].ToString(), out int y) ? y : 0;

        //                mac += row[dalPlanning.machineName].ToString() + ";";

        //            }

        //            // Mark this row for removal
        //            rowsToRemove.Add(row);
        //        }

        //    }

        //    // Remove all matching rows
        //    foreach (DataRow rowToRemove in rowsToRemove)
        //    {
        //        dt_MacSechedule.Rows.Remove(rowToRemove);
        //    }

        //    return Tuple.Create(totalTarget, totalProduced, mac);
        //}

        private string GetProduceQty(string ItemCode, DataTable dt_MacSechedule)
        {
            int totalTarget = 0, totalProduced = 0;
            string mac = "Mac:";
            string note = "";

            // Use a list to store rows to remove (can't modify collection while iterating)
            List<DataRow> rowsToRemove = new List<DataRow>();

            foreach (DataRow row in dt_MacSechedule.Rows)
            {
                string itemCode = row[dalPlanning.partCode].ToString();
                if (itemCode == ItemCode)
                {
                    string status = row[dalPlanning.planStatus].ToString();

                    totalTarget = int.TryParse(row[dalPlanning.targetQty].ToString(), out int x) ? x : 0;
                    totalProduced = int.TryParse(row[dalPlanning.planProduced].ToString(), out int y) ? y : 0;

                    mac = row[dalPlanning.machineName].ToString();

                    if (status == text.planning_status_draft)
                    {
                        note += "[(DRAFT)" + mac + ": " + totalProduced.ToString("N0") + "/" + totalTarget.ToString("N0") + "] ";

                    }
                    else
                    {
                        note += "[" + mac + ": " + totalProduced.ToString("N0") + "/" + totalTarget.ToString("N0") + "] ";

                    }

                    // Mark this row for removal
                    rowsToRemove.Add(row);
                }

            }

            // Remove all matching rows
            foreach (DataRow rowToRemove in rowsToRemove)
            {
                dt_MacSechedule.Rows.Remove(rowToRemove);
            }

            return note;
        }
        private int GetMinStockLevel(string ItemCode, DataTable dt_Item)
        {
            int MinStockLevel = 0;

            foreach (DataRow row in dt_Item.Rows)
            {
                string itemCode = row[dalItem.ItemCode].ToString();

                if (itemCode == ItemCode)
                {
                    decimal minStock = decimal.TryParse(row[dalItem.ItemMinStockLevel].ToString(), out decimal parsedValue) ? parsedValue : 0;

                    return (int) minStock;
                }

            }

            return MinStockLevel;
        }

        private Tuple<int, int, int> GetDeliveredQty(DataTable dt_SppCustomer, DataTable dt_Trf, int month_1, int month_2, int month_3, string itemCode)
        {
            int Out_1 = 0, Out_2 = 0, Out_3 = 0;
            dt_Trf.DefaultView.Sort = dalItem.ItemCode + " ASC";
            dt_Trf = dt_Trf.DefaultView.ToTable();
            dt_Trf.AcceptChanges();

            foreach (DataRow row in dt_Trf.Rows)
            {
                bool listedCustomer = false;
                string trfItemCode = row[dalItem.ItemCode].ToString();
                string passed = row[dalTrfHist.TrfResult].ToString();
                string trfTo = row[dalTrfHist.TrfTo].ToString();
                int trfQty = int.TryParse(row[dalTrfHist.TrfQty].ToString(), out trfQty) ? trfQty : 0;
                int trfMonth = DateTime.TryParse(row[dalTrfHist.TrfDate].ToString(), out DateTime trfDate) ? trfDate.Month : -1;

                if (passed == "Passed" && trfItemCode == itemCode)
                {
                    foreach (DataRow rowSPP in dt_SppCustomer.Rows)
                    {
                        string fullName = rowSPP[dalSBB.FullName].ToString();
                        string shortName = rowSPP[dalSBB.ShortName].ToString();

                        if (trfTo == fullName || trfTo == shortName || trfTo == "SPP" || trfTo == "OTHER")
                        {
                            listedCustomer = true;
                            break;
                        }
                    }

                    if (listedCustomer)
                    {
                        if (trfMonth == month_1)
                        {
                            Out_1 += trfQty;
                        }
                        else if (trfMonth == month_2)
                        {
                            Out_2 += trfQty;
                        }
                        else if (trfMonth == month_3)
                        {
                            Out_3 += trfQty;
                        }
                    }
                }

            }

            dt_Trf.AcceptChanges();



            return Tuple.Create(Out_1, Out_2, Out_3);
        }

        private DataTable SimplifyMatPartData(DataTable dt_MatPart)
        {
            dt_MatPart.DefaultView.Sort = text.Header_PartCode + " ASC";
            dt_MatPart = dt_MatPart.DefaultView.ToTable();
            dt_MatPart.AcceptChanges();

            //DataTable dt_SBBCustWithoutRemovedDataSelect = dalSBB.CustomerWithoutRemovedDataSelect();

            string previousItemCode = "";
            int totalNeed = 0;

            for (int i = 0; i < dt_MatPart.Rows.Count; i++)
            {
                string itemCode = dt_MatPart.Rows[i][text.Header_PartCode].ToString();
                int stillNeed = int.TryParse(dt_MatPart.Rows[i][text.Header_StillNeed].ToString(), out stillNeed) ? stillNeed : 0;
                if (previousItemCode != itemCode)
                {
                    previousItemCode = itemCode;
                    totalNeed = stillNeed;
                }
                else
                {


                    totalNeed += stillNeed;
                    dt_MatPart.Rows[i][text.Header_StillNeed] = totalNeed;

                    //dt_Trf.Rows[i - 1].Delete();
                }
            }

            dt_MatPart.AcceptChanges();

            previousItemCode = "";

            for (int i = 0; i < dt_MatPart.Rows.Count; i++)
            {
                string trfItemCode = dt_MatPart.Rows[i][text.Header_PartCode].ToString();

                if (previousItemCode != trfItemCode)
                {
                    previousItemCode = trfItemCode;
                }
                else
                {
                    dt_MatPart.Rows[i - 1].Delete();
                }
            }
            dt_MatPart.AcceptChanges();

            return dt_MatPart;
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }

        private void WriteFileToDrive(DataTable dt, string path, string fileName)
        {
            var csvData = DatatableToCsv(dt);

            string fullFilePath = path + fileName;
            FileInfo file = new FileInfo(path + fileName);
            //check if file exist
            if (File.Exists(fullFilePath) && IsFileLocked(file))
            {
                //if file locked

            }
            File.WriteAllText(path + fileName, csvData.ToString());
        }

        private bool IfExists(string itemCode, string custName)
        {
            itemCustDAL dalItemCust = new itemCustDAL();
            DataTable dt = dalItemCust.existsSearch(itemCode, tool.getCustID(custName).ToString());

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void pairCustomer(string itemCode)
        {
            itemCustBLL uItemCust = new itemCustBLL();
            itemCustDAL dalItemCust = new itemCustDAL();

            string cust = text.SPP_BrandName;

            if (!IfExists(itemCode, cust))
            {
                uItemCust.cust_id = Convert.ToInt32(tool.getCustID(cust));
                uItemCust.item_code = itemCode;
                uItemCust.item_cust_added_date = DateTime.Now;
                uItemCust.item_cust_added_by = MainDashboard.USER_ID;
                uItemCust.forecast_one = 0;
                uItemCust.forecast_two = 0;
                uItemCust.forecast_three = 0;
                uItemCust.forecast_current_month = DateTime.Now.ToString("MMMM");

                bool success = dalItemCust.Insert(uItemCust);

                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show("Failed to add new item_cust record");
                }
            }
            else
            {
                // MessageBox.Show("Data already exist.");
            }

        }



        private int ItemStdPackingFound(DataTable dt, string itemCode)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (itemCode.Equals(row[dalSBB.ItemCode].ToString()))
                {
                    int tblCode = int.TryParse(row[dalSBB.TableCode].ToString(), out tblCode) ? tblCode : -1;

                    return tblCode;
                }
            }

            return -1;

        }

        private void TempChildStdPackingUpload()
        {
            string path = @"D:\Users\Jun\Desktop\";


            string fileName = "SBBItemUpload.csv";

            DataTable dt_StdPacking = dalSBB.StdPackingSelect();

            if (dt_Item == null || dt_Item.Rows.Count < 0)
                dt_Item = dalItem.Select();

            if (Directory.Exists(path) && File.Exists(path + fileName))
            {
                dt_SBBItemUpload = CsvToDatatable(path + fileName, true);

                uSBB.Updated_Date = DateTime.Now;
                uSBB.Updated_By = MainDashboard.USER_ID;
                uSBB.IsRemoved = false;
                uSBB.Qty_Per_Packet = 0;


                foreach (DataRow row in dt_SBBItemUpload.Rows)
                {
                    string itemCode = row["CODE"].ToString();

                    //check if item code exist
                    if (!string.IsNullOrEmpty(tool.getItemNameFromDataTable(dt_Item, itemCode)))
                    {
                        int qty_per_bag = int.TryParse(row["BAG"].ToString(), out int i) ? i : 0;
                        int qty_per_container = int.TryParse(row["CONTAINER"].ToString(), out i) ? i : 0;

                        uSBB.Item_code = itemCode;
                        uSBB.Qty_Per_Bag = qty_per_bag;
                        uSBB.Qty_Per_Container = qty_per_container;

                        int tblCode = ItemStdPackingFound(dt_StdPacking, itemCode);

                        //std packing table update or insert
                        if (tblCode != -1)
                        {

                            uSBB.Table_Code = tblCode;

                            //update
                            if (!dalSBB.StdPackingBagContainerUpdate(uSBB))
                            {
                                MessageBox.Show("Failed to update standard packing (bag and container)");
                            }
                        }
                        else
                        {
                            //insert
                            if (!dalSBB.InsertStdPackingBagContainer(uSBB))
                            {
                                MessageBox.Show("Failed to insert standard packing (bag and container)");
                            }
                        }


                    }

                }
            }
        }


        private DataTable DOWithTrfInfo_SelectedPeriod(DataTable dt, DateTime start, DateTime end)
        {
            DataTable dt_SelectedPeriod = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DateTime trfDate = DateTime.TryParse(row[dalTrfHist.TrfDate].ToString(), out trfDate) ? trfDate : DateTime.MaxValue;

                if (trfDate != DateTime.MaxValue && trfDate.Date >= start.Date && trfDate.Date <= end.Date)
                {
                    dt_SelectedPeriod.ImportRow(row);
                }
            }

            return dt_SelectedPeriod;

        }

        private void GetPreLoadDataFromLoginPage()
        {
            dt_SBBCustWithoutRemovedDataSelect = frmLogIn.dt_SBBCustWithoutRemovedDataSelect;
            dt_PendingPOSelect = frmLogIn.dt_PendingPOSelect;
            dt_JoinSelectWithChildCat = frmLogIn.dt_JoinSelectWithChildCat;
            dt_SBBItemSelect = frmLogIn.dt_SBBItemSelect;
            dt_POSelectWithSizeAndType = frmLogIn.dt_POSelectWithSizeAndType;
            dt_DOWithTrfInfoSelectedPeriod = frmLogIn.DB_DO_TRF_INFO;
            dt_SBBCustSearchWithTypeAndSize = frmLogIn.dt_SBBCustSearchWithTypeAndSize;
            dt_Item = frmLogIn.dt_Item;

            if (MainDashboard.myconnstrng == text.DB_Semenyih)
            {
                //write data to CSV
                Text = "SBB Dashboard (Semenyih DB)";
            }

        }
        private void NewInitialDBData()
        {
            string start;
            string end;
            string itemCust = text.SPP_BrandName;

            DateTime now = DateTime.Now;

            start = MonthlyDateStart.ToString("yyyy/MM/dd");
            end = MonthlyDateEnd.ToString("yyyy/MM/dd");

            dt_SBBCustWithoutRemovedDataSelect = dalSBB.CustomerWithoutRemovedDataSelect();//144

            //dalSBB.TestDateAdd();
             dt_PendingPOSelect = dalSBB.SBBPagePendingPOSelect();//14ms
           
           
            //dt_PendingPOSelect = dalSBB.SBBPagePendingPOSelect();//14ms
                                                                 
            //dt_PendingPOSelect = dalSBB.SBBPagePendingPOSelect_Custom();//14ms


            dt_JoinSelectWithChildCat = dalJoin.SelectWithChildCat();//174ms
            dt_SBBItemSelect = dalItemCust.SBBItemSelect(itemCust);//98ms
            dt_POSelectWithSizeAndType = dalSBB.SBBPagePOSelectWithSizeAndType();//137ms


            if (Loaded)
            {
                dt_DOWithTrfInfoSelectedPeriod = dalSBB.SBBPageDOWithTrfInfoSelect(start, end);//1757
                dt_Stock = dalStock.StockDataSelect();//350ms

            }
            else
            {
                if (frmLogIn.DB_DO_TRF_INFO != null)
                {
                    dt_DOWithTrfInfoSelectedPeriod = frmLogIn.DB_DO_TRF_INFO.Copy();
                    dt_Stock = frmLogIn.DB_STOCK.Copy();
                }
                else
                {
                   // dt_DOWithTrfInfoSelectedPeriod = dalSBB.TrfInfoSelectSimple(MonthlyDateStart, MonthlyDateEnd);//11300
                    dt_DOWithTrfInfoSelectedPeriod = dalSBB.SBBPageDOWithTrfInfoSelect(start, end);//11300
                    //dt_DOWithTrfInfoSelectedPeriod = dalSBB.SBBPageDOWithTrfInfoSelect_Optimized(start, end);//11300
                   
                    dt_Stock = dalStock.StockDataSelect();//350ms
                }

            }



            dt_SBBCustSearchWithTypeAndSize = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);//26

            if (dt_SBBCustSearchWithTypeAndSize.Rows.Count > 0)
                dt_SBBCustSearchWithTypeAndSize.DefaultView.Sort = dalSBB.TypeName + " ASC," + dalSBB.SizeNumerator + " ASC," + dalSBB.SizeWeight + "1 ASC";

            dt_SBBCustSearchWithTypeAndSize = dt_SBBCustSearchWithTypeAndSize.DefaultView.ToTable();

            if (dt_Item == null || dt_Item.Rows.Count < 0)
                dt_Item = dalItem.Select();

            if (MainDashboard.myconnstrng == text.DB_Semenyih)
            {
                //write data to CSV
                Text = "SBB Dashboard (Semenyih DB)";
            }
        }

        private void InitialDBData()
        {
            //string start;
            //string end;
            //string itemCust = text.SPP_BrandName;
            //string path;

            ////DateTime dateStart = dtpDate1.Value;
            ////DateTime dateEnd = dtpDate2.Value;

            //DateTime now = DateTime.Now;

            ////string startNow = new DateTime(now.Year, now.Month, 1).ToString("yyyy/MM/dd");
            ////string endNow = new DateTime(now.Year, now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd");

            ////if (MonthlyDateStart > MonthlyDateEnd)
            ////{
            ////    dateStart = dtpDate2.Value;
            ////    dateEnd = dtpDate1.Value;
            ////}

            //start = MonthlyDateStart.ToString("yyyy/MM/dd");
            //end = MonthlyDateEnd.ToString("yyyy/MM/dd");

            ////path = text.CSV_Jun_GoogleDrive_SemenyihServer_Path;

            ////if (MainDashboard.myconnstrng == text.DB_Semenyih)//MainDashboard.myconnstrng == text.DB_Semenyih || cmbDataSource.Text.Contains(DataSource_LocalDB)
            ////{
            ////    path = text.CSV_SemenyihServer_Path;

            ////}


            ////string fileName = "SBBItemUpload.csv";

            ////dt_SBBItemUpload = CsvToDatatable(path + fileName, true);

            //if (true)//MainDashboard.myconnstrng == text.DB_Semenyih || cmbDataSource.Text.Contains(DataSource_LocalDB)
            //{
            //    dt_TrfRangeUsageSearch = dalTrfHist.RangeUsageSearch(start, end);
            //    dt_SBBCustWithoutRemovedDataSelect = dalSBB.CustomerWithoutRemovedDataSelect();
            //    dt_PendingPOSelect = dalSBB.PendingPOSelect();

            //    dt_JoinSelectWithChildCat = dalJoin.SelectWithChildCat();
            //    dt_SBBItemSelect = dalItemCust.SBBItemSelect(itemCust);
            //    dt_POSelectWithSizeAndType = dalSBB.POSelectWithSizeAndType();
            //    dt_DOWithTrfInfoSelect = dalSBB.DOWithInfoSelect();
            //    dt_POSelectWithSizeAndType = dalSBB.POSelectWithSizeAndType();
            //    dt_DOWithTrfInfoSelectedPeriod = dalSBB.DOWithTrfInfoSelect(start, end);

            //    dt_SBBCustSearchWithTypeAndSize = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);
            //    dt_SBBCustSearchWithTypeAndSize.DefaultView.Sort = dalSBB.TypeName + " ASC," + dalSBB.SizeNumerator + " ASC," + dalSBB.SizeWeight + "1 ASC";
            //    dt_SBBCustSearchWithTypeAndSize = dt_SBBCustSearchWithTypeAndSize.DefaultView.ToTable();

            //    dt_Item = dalItem.Select();

            //    if (MainDashboard.myconnstrng == text.DB_Semenyih)
            //    {
            //        //write data to CSV
            //        Text = "SBB Dashboard (!Semenyih DB)";

            //        //path = text.CSV_Jun_GoogleDrive_SemenyihServer_Path;

            //        //if (MainDashboard.myconnstrng == text.DB_Semenyih)
            //        //{
            //        //    path = text.CSV_SemenyihServer_Path;

            //        //    Text = "SBB Dashboard (Semenyih DB)";
            //        //}

            //        //if (Directory.Exists(path))
            //        //{
            //        //    WriteDatatableToCsv(dt_DOWithTrfInfoSelect, path, text.CSV_dalSBB_DOWithTrfInfoSelect_StartEnd);
            //        //    WriteDatatableToCsv(dt_SBBCustSearchWithTypeAndSize, path, text.CSV_dalItemCust_SPPCustSearchWithTypeAndSize);
            //        //    WriteDatatableToCsv(dt_Item, path, text.CSV_dalItem_Select);
            //        //}
            //        //else
            //        //{
            //        //    MessageBox.Show("Path not found!\nPlease contact System Engineer.");
            //        //}


            //    }
            //}

            //else//get data from Google Drive
            //{
            //    //path = text.CSV_Jun_GoogleDrive_SemenyihServer_Path;

            //    //    string fileName = text.CSV_dalItemCust_SPPCustSearchWithTypeAndSize;
            //    //    dt_SPPCustSearchWithTypeAndSize = CsvToDatatable(path + fileName, true);

            //    //    //if(dt_Product == null || dt_Product.Rows.Count <= 0)
            //    //    //{
            //    //    //    dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);

            //    //    //}

            //    //    dt_SPPCustSearchWithTypeAndSize.DefaultView.Sort = dalSBB.TypeName + " ASC," + dalSBB.SizeNumerator + " ASC," + dalSBB.SizeWeight + "1 ASC";
            //    //    dt_SPPCustSearchWithTypeAndSize = dt_SPPCustSearchWithTypeAndSize.DefaultView.ToTable();

            //    //    fileName = text.CSV_dalItem_Select;
            //    //    dt_Item = CsvToDatatable(path + fileName, true);

            //    //    //if (dt_Item == null || dt_Item.Rows.Count <= 0)
            //    //    //{
            //    //    //    dt_Item = dalItem.Select();
            //    //    //}

            //    //    fileName = text.CSV_dalSBB_DOWithTrfInfoSelect_StartEnd;
            //    //    dt_DOWithTrfInfoSelect = CsvToDatatable(path + fileName, true);

            //    //    //if (dt_DOList == null || dt_DOList.Rows.Count <= 0)
            //    //    //{
            //    //    //    dt_DOList = dalSBB.DOWithTrfInfoSelect(start, end);
            //    //    //}
            //}
        }

        private void WriteDatatableToCsv(DataTable dataTable, string pathOnly, string filename)
        {
            StringBuilder sbData = new StringBuilder();

            // Only return Null if there is no structure.
            if (dataTable.Columns.Count == 0)
                return;

            foreach (var col in dataTable.Columns)
            {
                if (col == null)
                    sbData.Append(", ");
                else
                    sbData.Append("\"" + col.ToString().Replace("\"", "\"\"") + "\",");
            }

            sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    if (column == null)
                        sbData.Append(",");
                    else
                        sbData.Append("\"" + column.ToString().Replace("\"", "\"\"") + "\",");
                }
                sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);
            }

            File.WriteAllText(pathOnly + filename, sbData.ToString());

        }

        private DataTable CsvToDatatable(string path, bool isFirstRowHeader)
        {
            string header = isFirstRowHeader ? "Yes" : "No";

            string pathOnly = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);
            FileInfo f;

            if (File.Exists(path) && !File.Exists(path.Replace(".db", ".csv")))//looking for db file
            {
                f = new FileInfo(path);

                f.MoveTo(Path.ChangeExtension(path, ".csv"));


            }

            fileName = fileName.Replace(".db", ".csv");
            path = path.Replace(".db", ".csv");

            if (File.Exists(path))
            {
                string sql = @"SELECT * FROM [" + fileName + "]";

                using (OleDbConnection connection = new OleDbConnection(
                          @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                          ";Extended Properties=\"Text;HDR=" + header + "\""))
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    DataTable dataTable = new DataTable
                    {
                        Locale = CultureInfo.CurrentCulture
                    };

                    adapter.Fill(dataTable);

                    //f = new FileInfo(pathOnly +"/" + fileName);

                    //f.MoveTo(Path.ChangeExtension(pathOnly + "/" + fileName, ".db"));

                    return dataTable;
                }
            }
            else
            {
                //path not found
                return new DataTable();
            }


        }

        public static string DatatableToCsv(DataTable dataTable)
        {
            StringBuilder sbData = new StringBuilder();

            // Only return Null if there is no structure.
            if (dataTable.Columns.Count == 0)
                return null;

            foreach (var col in dataTable.Columns)
            {
                if (col == null)
                    sbData.Append(", ");
                else
                    sbData.Append("\"" + col.ToString().Replace("\"", "\"\"") + "\",");
            }

            sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    if (column == null)
                        sbData.Append(",");
                    else
                        sbData.Append("\"" + column.ToString().Replace("\"", "\"\"") + "\",");
                }
                sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);
            }

            return sbData.ToString();
        }

        private void ShowDataSourceUI()
        {
            tlpDashBoard.RowStyles[0] = new RowStyle(SizeType.Absolute, 0f);
            //if (MainDashboard.myconnstrng == text.DB_Semenyih)
            //{
            //    tlpSummary.RowStyles[0] = new RowStyle(SizeType.Absolute, 0f);
            //}
            //else
            //{
            //    tlpSummary.RowStyles[0] = new RowStyle(SizeType.Absolute, 30f);
            //    LoadDataSourceCMB(cmbDataSource);
            //}
        }

        private void LayoutPanelDisplay(bool show)
        {
            tlpSBB.Visible = show;

            tableLayoutPanel15.Visible = show;
            tableLayoutPanel16.Visible = show;
            tableLayoutPanel17.Visible = show;
            tableLayoutPanel18.Visible = show;
            tableLayoutPanel19.Visible = show;
            tableLayoutPanel20.Visible = show;
            tlpStockReport.Visible = show;
            tableLayoutPanel23.Visible = show;
            //tableLayoutPanel24.Visible = show;
            //ableLayoutPanel25.Visible = show;
            //tableLayoutPanel26.Visible = show;
            //tableLayoutPanel27.Visible = show;
            //tableLayoutPanel28.Visible = show;
            tableLayoutPanel30.Visible = show;
            tableLayoutPanel33.Visible = show;
            tableLayoutPanel34.Visible = show;

            lblPendingDO.Visible = show;
            tlpDashBoard.Visible = show;



        }
        private void frmSBB_Load(object sender, EventArgs e)
        {
            //TempChildStdPackingUpload();
            Loaded = false;
            
            cmbTransferFrom.Text = text.Factory_Semenyih;
            cmbTransferTo.Text = text.Factory_SMY_AssemblyLine;

            SuspendLayout();
            HideFilter(true);
            StockInfoShowRemark(false);
            tlpStockInfoMainSetting.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 150f);
            tlpStockInfoMainSetting.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 0f);
            cmbLocation.Text = "All";

            //SBBItemUpload();
            STOCK_INFO_ITEM_TYPE = Type_Part;
            LayoutPanelDisplay(false);

            //ShowDataSourceUI();
            dtpDate1.Value = DateTime.Now;

            //LoadTypeCMB(cmbType);
            //LoadStockLocationCMB(cmbStockLocation);
            //LoadStockAlertSortTypeCMB(cmbSortType);

            RefreshPage();

            Loaded = true;
            CalculatePeriodDetails();
            dgvStockAlert.ClearSelection();
            //dgvUsage.ClearSelection();

            //var now = DateTime.Now;
            //var startOfMonth = new DateTime(now.Year, now.Month, 1);
            LayoutPanelDisplay(true);

            //NewItemUpdates();
            WindowState = FormWindowState.Maximized;

            ResumeLayout();
        }
        SBBDataDAL dalData = new SBBDataDAL();
        SBBDataBLL uData = new SBBDataBLL();

        private DataTable DT_SIZE;
        private DataTable DT_TYPE;
        private DataTable DT_CATEGORY;

        private string GetTblCode(DataTable dt, string data)
        {
            string tblCode = "-1";
            string colName = "";
            if (dt.Columns.Contains(dalData.SizeNumerator))
            {
                colName = dalData.SizeNumerator;
            }
            else if (dt.Columns.Contains(dalData.TypeName))
            {
                colName = dalData.TypeName;
            }
            else if (dt.Columns.Contains(dalData.CategoryName))
            {
                colName = dalData.CategoryName;
            }
            if (!string.IsNullOrEmpty(colName))
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[colName].ToString().ToUpper() == data.ToUpper())
                    {
                        tblCode = row[dalData.TableCode].ToString();
                        return tblCode;
                    }
                }
            }
            else
            {
                MessageBox.Show("Column not found!");
            }
            return tblCode;
        }
        private void InsertNewSize(float numerator, float denominator, string unit)
        {

            if (DT_SIZE?.Rows.Count <= 0)
            {
                DT_SIZE = dalData.SizeSelect();
            }

            foreach (DataRow row in DT_SIZE.Rows)
            {
                if (row[dalData.SizeNumerator].ToString() == numerator.ToString())
                {
                    return;
                }
            }


            uData.Updated_Date = DateTime.Now;
            uData.Updated_By = MainDashboard.USER_ID;

            float weight = numerator / denominator;

            uData.Size_Numerator = (int)numerator;
            uData.Size_Denominator = (int)denominator;
            uData.Size_Weight = weight;
            uData.Size_Unit = unit;

            if (!dalData.InsertSize(uData))
            {
                MessageBox.Show("Failed to insert size data to DB.");
            }

        }

        private void InsertNewType(string typeName, bool isCommon)
        {
            if (DT_TYPE?.Rows.Count <= 0)
            {
                DT_TYPE = dalData.TypeSelect();
            }

            foreach (DataRow row in DT_TYPE.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalData.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (row[dalData.TypeName].ToString().ToUpper() == typeName.ToUpper() && !isRemoved)
                {

                    return;
                }
            }


            uData.Updated_Date = DateTime.Now;
            uData.Updated_By = MainDashboard.USER_ID;

            uData.Type_Name = typeName.ToUpper();
            uData.IsCommon = isCommon;

            if (!dalData.InsertType(uData))
            {
                MessageBox.Show("Failed to insert type data to DB.");
            }
        }

        private bool IfItemAddedToDB(String productCode)
        {
            return tool.getItemCatFromDataTable(dt_Item, productCode) != "";
        }

        static public itemBLL uItem = new itemBLL();

        private void ChildPartInsert(string itemCode, string itemName)
        {
            if (!IfItemAddedToDB(itemCode))
            {
                //General Data
                uItem.item_cat = text.Cat_Part;
                uItem.item_code = itemCode;
                uItem.item_name = itemName;
                uItem.item_unit = text.Unit_Piece;
                uItem.item_wastage_allowed = 0;
                uItem.item_assembly = 0;
                uItem.item_production = 1;
                uItem.item_sbb = 1;

                //Quotation Data
                uItem.item_quo_ton = 0;
                uItem.item_quo_ct = 0;
                uItem.item_quo_pw_pcs = 0;
                uItem.item_quo_rw_pcs = 0;

                //Production Data
                uItem.item_best_ton = 0;
                uItem.item_pro_ton = 0;
                uItem.item_pro_ct_from = 0;
                uItem.item_pro_ct_to = 0;
                uItem.item_cavity = 0;
                uItem.item_pro_cooling = 0;

                uItem.item_pro_pw_shot = 0;
                uItem.item_pro_rw_shot = 0;

                uItem.item_pro_pw_pcs = 0;
                uItem.item_pro_rw_pcs = 0;

                //Material Data
                uItem.item_material = "";
                uItem.item_mb = "";
                uItem.item_mb_rate = 0;
                uItem.item_color = "";

                uItem.unit_to_pcs_rate = 1;

                DateTime now = DateTime.Now;
                uItem.item_added_date = now;
                uItem.item_added_by = MainDashboard.USER_ID;

                uItem.item_updtd_date = now;
                uItem.item_updtd_by = MainDashboard.USER_ID;

                //Inserting Data into Database
                bool success = dalItem.ItemMasterList_ItemAdd(uItem);
                //If the data is successfully inserted then the value of success will be true else false
                if (!success)
                {
                    MessageBox.Show("Failed to add new item");
                }
            }
        }

        private void FinishedGoodesInsert(string itemCode, string itemName)
        {
            if (!IfItemAddedToDB(itemCode))
            {
                //General Data
                uItem.item_cat = text.Cat_Part;
                uItem.item_code = itemCode;
                uItem.item_name = itemName;
                uItem.item_unit = text.Unit_Set;
                uItem.item_wastage_allowed = 0;
                uItem.item_assembly = 1;
                uItem.item_production = 0;
                uItem.item_sbb = 1;

                //Quotation Data
                uItem.item_quo_ton = 0;
                uItem.item_quo_ct = 0;
                uItem.item_quo_pw_pcs = 0;
                uItem.item_quo_rw_pcs = 0;

                //Production Data
                uItem.item_best_ton = 0;
                uItem.item_pro_ton = 0;
                uItem.item_pro_ct_from = 0;
                uItem.item_pro_ct_to = 0;
                uItem.item_cavity = 0;
                uItem.item_pro_cooling = 0;

                uItem.item_pro_pw_shot = 0;
                uItem.item_pro_rw_shot = 0;

                uItem.item_pro_pw_pcs = 0;
                uItem.item_pro_rw_pcs = 0;

                //Material Data
                uItem.item_material = "";
                uItem.item_mb = "";
                uItem.item_mb_rate = 0;
                uItem.item_color = "";

                uItem.unit_to_pcs_rate = 1;

                DateTime now = DateTime.Now;
                uItem.item_added_date = now;
                uItem.item_added_by = MainDashboard.USER_ID;

                uItem.item_updtd_date = now;
                uItem.item_updtd_by = MainDashboard.USER_ID;

                //Inserting Data into Database
                bool success = dalItem.ItemMasterList_ItemAdd(uItem);
                //If the data is successfully inserted then the value of success will be true else false
                if (!success)
                {
                    MessageBox.Show("Failed to add new item");
                }
            }
        }

        private void UpdateStdPacking(string itemCode, int pcsPerPacket, int pcsPerBag, int pcsPerContainer)
        {

            DataTable dt = dalData.StdPackingSelect();

            foreach (DataRow row in dt.Rows)
            {
                if (row[dalData.ItemCode].ToString() == itemCode)
                {
                    return;
                }
            }

            if (pcsPerPacket > 0 || pcsPerBag > 0 || pcsPerContainer > 0)
            {
                SBBDataBLL uData = new SBBDataBLL();
                SBBDataDAL dalData = new SBBDataDAL();

                uData.Max_Lvl = 0;

                uData.Updated_Date = DateTime.Now;
                uData.Updated_By = MainDashboard.USER_ID;

                uData.Qty_Per_Container = pcsPerContainer;
                uData.Qty_Per_Packet = pcsPerPacket;
                uData.Qty_Per_Bag = pcsPerBag;

                uData.Item_code = itemCode;


                if (!dalData.InsertStdPacking(uData))
                {
                    MessageBox.Show("Failed to insert standard packing data to DB.");
                }
            }
        }

        SBBDataDAL dalSPP = new SBBDataDAL();
        SBBDataBLL uSpp = new SBBDataBLL();
        private void UpdatePrice(string itemCode, decimal defaultPrice, decimal defaultDiscount)
        {
            DataTable dt_ItemPrice = dalSPP.PriceSelect();

            uSpp.Updated_Date = DateTime.Now;
            uSpp.Updated_By = MainDashboard.USER_ID;
            uSpp.Item_tbl_code = itemCode;
            uSpp.Default_price = defaultPrice;
            uSpp.Default_discount = defaultDiscount;

            //update or insert
            bool dataSaved = false;

            if (dt_ItemPrice != null && dt_ItemPrice.Rows.Count > 0)
            {
                foreach (DataRow priceRow in dt_ItemPrice.Rows)
                {
                    if (uSpp.Item_tbl_code == priceRow[dalSPP.ItemTblCode].ToString())
                    {
                        //get table code
                        int tableCode = int.TryParse(priceRow[dalSPP.TableCode].ToString(), out tableCode) ? tableCode : -1;
                        uSpp.Table_Code = tableCode;
                        if (tableCode != -1)
                        {
                            if (!dalSPP.PriceUpdate(uSpp))
                            {
                                MessageBox.Show("Failed to update item default price info!");
                                return;
                            }
                            else
                            {

                                dataSaved = true;
                            }

                            break;
                        }
                        else
                        {
                            MessageBox.Show("Failed to get table code of item default price table!");
                            break;
                        }



                    }
                }
            }

            if (!dataSaved)
            {
                //insert data
                if (!dalSPP.InsertPrice(uSpp))
                {
                    MessageBox.Show("Failed to insert item default price info!");
                    return;
                }
                else
                {
                    dataSaved = true;
                }
            }
        }

        private void UpdateItemGroup(string parentCode, string childCode)
        {
            uJoin.join_parent_code = parentCode;
            uJoin.join_parent_name = tool.getItemNameFromDataTable(dt_Item, parentCode);

            uJoin.join_child_code = childCode;
            uJoin.join_child_name = tool.getItemNameFromDataTable(dt_Item, childCode);

            uJoin.join_max = 1;
            uJoin.join_min = 1;

            uJoin.join_qty = 1;

            uJoin.join_stock_out = true;

            DataTable dt_existCheck = dalJoin.existCheck(uJoin.join_parent_code, uJoin.join_child_code);

            bool success = false;

            if (dt_existCheck.Rows.Count > 0)
            {
                uJoin.join_updated_date = DateTime.Now;
                uJoin.join_updated_by = MainDashboard.USER_ID;
                //update data
                success = dalJoin.UpdateWithMaxMin(uJoin);
                //If the data is successfully inserted then the value of success will be true else false
                if (!success)
                {
                    //Failed to update data
                    MessageBox.Show("Failed to update join");
                }

            }
            else
            {
                uJoin.join_added_date = DateTime.Now;
                uJoin.join_added_by = MainDashboard.USER_ID;
                //insert new data
                success = dalJoin.InsertWithMaxMin(uJoin);
                //If the data is successfully inserted then the value of success will be true else false
                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show("Failed to add new join");
                }

            }
        }

        private void NewItemUpdates()
        {
            bool Validation = true;
            Validation &= MainDashboard.USER_ID == 1;
            Validation &= DateTime.Now.Year == 2024 && DateTime.Now.Month == 1 && DateTime.Now.Day == 24;

            if (Validation)
            {
                string Sprinkler323Code = "(OK) SP323";

                if (dt_Item == null || dt_Item.Rows.Count < 0)
                    dt_Item = dalItem.Select();

                if (IfItemAddedToDB(Sprinkler323Code))
                {
                    return;
                }

                DT_SIZE = dalData.SizeSelect();
                DT_TYPE = dalData.TypeSelect();

                //insert new type and new size
                int sizeType = 323;
                string SprinklerTypeName = "Sprinkler";
                string ArmTypeName = "SP323 Arm";
                string Nozzle1TypeName = "SP323 Nozzle 1";
                string Nozzle2TypeName = "SP323 Nozzle 2";
                string RodTypeName = "SP323 Rod";
                string AdjusterTypeName = "SP323 Adjuster";
                string GasketTypeName = "SP323 Gasket";
                string TopBodyTypeName = "SP323 Top Body";
                string BottomBushTypeName = "SP323 Bottom Bush";
                string EllenBodyTypeName = "SP323 Ellen Body";

                InsertNewSize(sizeType, 1, " ");

                InsertNewType(SprinklerTypeName, false);
                InsertNewType(ArmTypeName, true);
                InsertNewType(Nozzle1TypeName, true);
                InsertNewType(Nozzle2TypeName, true);
                InsertNewType(RodTypeName, true);
                InsertNewType(AdjusterTypeName, true);
                InsertNewType(GasketTypeName, true);
                InsertNewType(TopBodyTypeName, true);
                InsertNewType(BottomBushTypeName, true);
                InsertNewType(EllenBodyTypeName, true);

                //insert new sprinkler child part
                DT_SIZE = dalData.SizeSelect();
                DT_TYPE = dalData.TypeSelect();
                DT_CATEGORY = dalData.CategorySelect();

                uItem.Size_tbl_code_1 = int.TryParse(GetTblCode(DT_SIZE, sizeType.ToString()), out int i) ? i : -1;
                uItem.Size_tbl_code_2 = -1;
                uItem.Category_tbl_code = int.TryParse(GetTblCode(DT_CATEGORY, text.Cat_CommonPart), out i) ? i : -1;

                string ArmCode = "SP323AR";
                string Nozzle1Code = "SP323N1";
                string Nozzle2Code = "SP323N2";
                string RodCode = "SP323RD";
                string AdjusterCode = "SP323AD";
                string GasketCode = "SP323GK";
                string TopBodyCode = "SP323TB";
                string BottomBushCode = "SP323BB";
                string EllenBodyCode = "SP323EB";

                uItem.Type_tbl_code = int.TryParse(GetTblCode(DT_TYPE, ArmTypeName), out i) ? i : -1;
                ChildPartInsert(ArmCode, "SPRINKLER 323 ARM");

                uItem.Type_tbl_code = int.TryParse(GetTblCode(DT_TYPE, Nozzle1TypeName), out i) ? i : -1;
                ChildPartInsert(Nozzle1Code, "SPRINKLER 323 NOZZLE 1");

                uItem.Type_tbl_code = int.TryParse(GetTblCode(DT_TYPE, Nozzle2TypeName), out i) ? i : -1;
                ChildPartInsert(Nozzle2Code, "SPRINKLER 323 NOZZLE 2");

                uItem.Type_tbl_code = int.TryParse(GetTblCode(DT_TYPE, RodTypeName), out i) ? i : -1;
                ChildPartInsert(RodCode, "SPRINKLER 323 ROD");

                uItem.Type_tbl_code = int.TryParse(GetTblCode(DT_TYPE, AdjusterTypeName), out i) ? i : -1;
                ChildPartInsert(AdjusterCode, "SPRINKLER 323 ADJUSTER");

                uItem.Type_tbl_code = int.TryParse(GetTblCode(DT_TYPE, GasketTypeName), out i) ? i : -1;
                ChildPartInsert(GasketCode, "SPRINKLER 323 GASKET");

                uItem.Type_tbl_code = int.TryParse(GetTblCode(DT_TYPE, TopBodyTypeName), out i) ? i : -1;
                ChildPartInsert(TopBodyCode, "SPRINKLER 323 TOP BODY");

                uItem.Type_tbl_code = int.TryParse(GetTblCode(DT_TYPE, BottomBushTypeName), out i) ? i : -1;
                ChildPartInsert(BottomBushCode, "SPRINKLER 323 BOTTOM BUSH");

                uItem.Type_tbl_code = int.TryParse(GetTblCode(DT_TYPE, EllenBodyTypeName), out i) ? i : -1;
                ChildPartInsert(EllenBodyCode, "SPRINKLER 323 ELLEN BODY");

                //insert new sprinkler 323 finished goods 

                uItem.Category_tbl_code = int.TryParse(GetTblCode(DT_CATEGORY, text.Cat_ReadyGoods), out i) ? i : -1;
                uItem.Type_tbl_code = int.TryParse(GetTblCode(DT_TYPE, SprinklerTypeName), out i) ? i : -1;
                FinishedGoodesInsert(Sprinkler323Code, "(OK) SPRINKLER 323");

                pairCustomer(Sprinkler323Code);

                //insert new sprinkler 323  item group
                UpdateItemGroup(Sprinkler323Code, ArmCode);
                UpdateItemGroup(Sprinkler323Code, Nozzle1Code);
                UpdateItemGroup(Sprinkler323Code, Nozzle2Code);
                UpdateItemGroup(Sprinkler323Code, RodCode);
                UpdateItemGroup(Sprinkler323Code, AdjusterCode);
                UpdateItemGroup(Sprinkler323Code, GasketCode);
                UpdateItemGroup(Sprinkler323Code, TopBodyCode);
                UpdateItemGroup(Sprinkler323Code, BottomBushCode);
                UpdateItemGroup(Sprinkler323Code, EllenBodyCode);

                //update sprinkler 323 default price and discount rate
                UpdatePrice(Sprinkler323Code, (decimal)6.5, 80);

                //update certain customer price and discount rate

                //update sprinkler 323 std packing
                UpdateStdPacking(Sprinkler323Code, 40, 400, 0);


            }


        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            //frmSBBPrice frm = new frmSBBPrice
            //{
            //    StartPosition = FormStartPosition.CenterScreen
            //};




            frmSBBDataSetting frm = new frmSBBDataSetting
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
            //if (!MainDashboard.ProductionFormOpen)
            //{
            //    frmLoading.ShowLoadingScreen();
            //    frmSBBproductionSchedule frm = new frmSBBproductionSchedule
            //    {
            //        MdiParent = this.ParentForm,
            //        StartPosition = FormStartPosition.CenterScreen,
            //        WindowState = FormWindowState.Maximized
            //    };
            //    frm.Show();
            //    MainDashboard.ProductionFormOpen = true;
            //    frmLoading.CloseForm();
            //}
            //else
            //{
            //    if (Application.OpenForms.OfType<frmSBBproductionSchedule>().Count() == 1)
            //    {
            //        Application.OpenForms.OfType<frmSBBproductionSchedule>().First().BringToFront();
            //    }
            //}
        }

        private void OpenPOList(object sender, EventArgs e)
        {

            int count = Application.OpenForms.OfType<frmSBBPOList>().Count();


            if (count == 1)
            {
                //Form is already open
                Application.OpenForms.OfType<frmSBBPOList>().First().BringToFront();
                Application.OpenForms.OfType<frmSBBPOList>().First().WindowState = FormWindowState.Normal;
            }
            else
            {
                // Form is not open
                frmSBBPOList frm = new frmSBBPOList
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                frm.Show();
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
            //frmSBBProductionPlanning frm = new frmSBBProductionPlanning
            //{
            //    StartPosition = FormStartPosition.CenterScreen
            //};


            //frm.ShowDialog();


            ////WorkInProgressMessage();
            //frmPlanningType frm = new frmPlanningType
            //{
            //    StartPosition = FormStartPosition.CenterScreen
            //};


            //frm.ShowDialog();
        }

        private void ResetTopCustomerData()
        {
            lblTopCust_1.Text = "";
            lblTopCustomer_Bag_1.Text = "";
            lblTopCustomer_Bal_1.Text = "";

            lblTopCust_2.Text = "";
            lblTopCustomer_Bag_2.Text = "";
            lblTopCustomer_Bal_2.Text = "";

            lblTopCust_3.Text = "";
            lblTopCustomer_Bag_3.Text = "";
            lblTopCustomer_Bal_3.Text = "";

            lblTopCust_4.Text = "";
            lblTopCustomer_Bag_4.Text = "";
            lblTopCustomer_Bal_4.Text = "";

            lblTopCust_5.Text = "";
            lblTopCustomer_Bag_5.Text = "";
            lblTopCustomer_Bal_5.Text = "";

            lblTopCust_6.Text = "";
            lblTopCustomer_Bag_6.Text = "";
            lblTopCustomer_Bal_6.Text = "";

            lblTopCust_7.Text = "";
            lblTopCustomer_Bag_7.Text = "";
            lblTopCustomer_Bal_7.Text = "";

            lblTopCust_8.Text = "";
            lblTopCustomer_Bag_8.Text = "";
            lblTopCustomer_Bal_8.Text = "";

            lblTopCust_9.Text = "";
            lblTopCustomer_Bag_9.Text = "";
            lblTopCustomer_Bal_9.Text = "";

            lblTopCust_10.Text = "";
            lblTopCustomer_Bag_10.Text = "";
            lblTopCustomer_Bal_10.Text = "";

        }

        private void ResetDeliveredData()
        {
            ResetTopCustomerData();
            lblMonthlyDelivered.Text = "-";
            lblDeliveredBalance.Text = "-";
        }

        private void ShowDeliverdPercentage(int totalBagDelivered)
        {
            double Percentage = 0;

            if (int.TryParse(lblTopCustomer_Bag_1.Text, out int deliverd))
            {
                Percentage = (double)deliverd / totalBagDelivered * 100;
                lblTopCustomer_Bag_1.Text += " (" + Percentage.ToString("0.#") + "%)";
            }

            if (int.TryParse(lblTopCustomer_Bag_2.Text, out deliverd))
            {
                Percentage = (double)deliverd / totalBagDelivered * 100;

                lblTopCustomer_Bag_2.Text += " (" + Percentage.ToString("0.#") + "%)";
            }

            if (int.TryParse(lblTopCustomer_Bag_3.Text, out deliverd))
            {
                Percentage = (double)deliverd / totalBagDelivered * 100;

                lblTopCustomer_Bag_3.Text += " (" + Percentage.ToString("0.#") + "%)";
            }

            if (int.TryParse(lblTopCustomer_Bag_4.Text, out deliverd))
            {
                Percentage = (double)deliverd / totalBagDelivered * 100;

                lblTopCustomer_Bag_4.Text += " (" + Percentage.ToString("0.#") + "%)";
            }

            if (int.TryParse(lblTopCustomer_Bag_5.Text, out deliverd))
            {
                Percentage = (double)deliverd / totalBagDelivered * 100;

                lblTopCustomer_Bag_5.Text += " (" + Percentage.ToString("0.#") + "%)";
            }

            if (int.TryParse(lblTopCustomer_Bag_6.Text, out deliverd))
            {
                Percentage = (double)deliverd / totalBagDelivered * 100;

                lblTopCustomer_Bag_6.Text += " (" + Percentage.ToString("0.#") + "%)";
            }

            if (int.TryParse(lblTopCustomer_Bag_7.Text, out deliverd))
            {
                Percentage = (double)deliverd / totalBagDelivered * 100;

                lblTopCustomer_Bag_7.Text += " (" + Percentage.ToString("0.#") + "%)";
            }

            if (int.TryParse(lblTopCustomer_Bag_8.Text, out deliverd))
            {
                Percentage = (double)deliverd / totalBagDelivered * 100;

                lblTopCustomer_Bag_8.Text += " (" + Percentage.ToString("0.#") + "%)";
            }

            if (int.TryParse(lblTopCustomer_Bag_9.Text, out deliverd))
            {
                Percentage = (double)deliverd / totalBagDelivered * 100;

                lblTopCustomer_Bag_9.Text += " (" + Percentage.ToString("0.#") + "%)";
            }

            if (int.TryParse(lblTopCustomer_Bag_10.Text, out deliverd))
            {
                if (deliverd > 0)
                {
                    Percentage = (double)deliverd / totalBagDelivered * 100;

                    lblTopCustomer_Bag_10.Text += " (" + Percentage.ToString("0.#") + "%)";
                }
                else
                {
                    lblTopCustomer_Bag_10.Text = "";
                }

            }
        }


        private Tuple<int, int, int, int, int> LoadPendingPOQty()//DataTable dt_DOList
        {
            int pendingPOQty = 0;
            int pendingCustQty = 0;

            int pendingBags = 0, pendingPkts = 0, pendingPcs = 0;

            DataTable dt = NewPOTable();

            //DataTable dt_POSelectWithSizeAndType = dalSBB.POSelectWithSizeAndType();

            DataRow dt_row;

            int poCode = -1;
            int prePOCode = -1;
            int orderQty = 0;
            int deliveredQty = 0;
            int CustTblCode = -1;
            int progress = -1;
            bool dataMatched = true;
            string PONo = null, ShortName = null, FullName = null;
            //string customer = null;

            DateTime PODate = DateTime.MaxValue;

            foreach (DataRow row in dt_POSelectWithSizeAndType.Rows)
            {
                bool isRemoved = bool.TryParse(row[dalSBB.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (!isRemoved)
                {
                    poCode = int.TryParse(row[dalSBB.POCode].ToString(), out poCode) ? poCode : -1;

                    int orderedQty = int.TryParse(row[dalSBB.POQty].ToString(), out orderedQty) ? orderedQty : 0;
                    int delivered = int.TryParse(row[dalSBB.DeliveredQty].ToString(), out delivered) ? delivered : 0;

                    int pendingDeliver = orderedQty - delivered;
                    int QtyPerBag = int.TryParse(row[dalSBB.QtyPerBag].ToString(), out QtyPerBag) ? QtyPerBag : 1;
                    int QtyPerPkt = int.TryParse(row[dalSBB.QtyPerPacket].ToString(), out QtyPerPkt) ? QtyPerPkt : 1;

                    if (QtyPerBag == 0)
                    {
                        QtyPerBag = 1;
                    }



                    if (pendingDeliver < 0)
                    {
                        pendingDeliver = 0;

                    }

                    pendingBags += pendingDeliver / QtyPerBag;

                    if (QtyPerPkt != 0)
                    {
                        pendingPkts += pendingDeliver % QtyPerBag / QtyPerPkt;
                        pendingPcs += pendingDeliver % QtyPerPkt;
                    }
                    else
                    {
                        pendingPcs += pendingDeliver % QtyPerBag;
                    }


                    if (prePOCode == -1)
                    {
                        prePOCode = poCode;
                        orderQty = int.TryParse(row[dalSBB.POQty].ToString(), out orderQty) ? orderQty : 0;
                        deliveredQty = int.TryParse(row[dalSBB.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

                        PONo = row[dalSBB.PONo].ToString();

                        PODate = Convert.ToDateTime(row[dalSBB.PODate]).Date;

                        ShortName = row[dalSBB.ShortName].ToString();
                        FullName = row[dalSBB.FullName].ToString();

                        CustTblCode = int.TryParse(row[dalSBB.CustTblCode].ToString(), out CustTblCode) ? CustTblCode : -1;
                    }
                    else if (prePOCode == poCode)
                    {
                        orderQty += int.TryParse(row[dalSBB.POQty].ToString(), out int temp) ? temp : 0;
                        deliveredQty += int.TryParse(row[dalSBB.DeliveredQty].ToString(), out temp) ? temp : 0;
                    }
                    else if (prePOCode != poCode)
                    {
                        progress = (int)((float)deliveredQty / orderQty * 100);
                        dataMatched = true;

                        #region PO TYPE

                        if (progress < 100)
                        {
                            dataMatched = dataMatched && true;
                        }
                        else
                        {
                            dataMatched = false;
                        }

                        #endregion

                        if (dataMatched)
                        {
                            dt_row = dt.NewRow();

                            dt_row[header_POCode] = prePOCode;
                            dt_row[header_PONoString] = PONo;
                            dt_row[header_PODate] = PODate;
                            dt_row[header_Customer] = ShortName;
                            dt_row[header_CustomerCode] = CustTblCode;
                            dt_row[header_Progress] = progress + "%";
                            dt_row[header_DONoString] = "";
                            dt.Rows.Add(dt_row);
                        }

                        prePOCode = poCode;
                        orderQty = int.TryParse(row[dalSBB.POQty].ToString(), out orderQty) ? orderQty : 0;
                        deliveredQty = int.TryParse(row[dalSBB.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

                        PONo = row[dalSBB.PONo].ToString();

                        PODate = Convert.ToDateTime(row[dalSBB.PODate]).Date;

                        ShortName = row[dalSBB.ShortName].ToString();
                        FullName = row[dalSBB.FullName].ToString();
                        CustTblCode = int.TryParse(row[dalSBB.CustTblCode].ToString(), out CustTblCode) ? CustTblCode : -1;
                    }
                }

            }

            progress = (int)((float)deliveredQty / orderQty * 100);
            dataMatched = true;

            #region PO TYPE

            if (progress < 100)
            {
                dataMatched = dataMatched && true;
            }
            else
            {
                dataMatched = false;
            }

            #endregion

            if (dataMatched)
            {
                dt_row = dt.NewRow();

                dt_row[header_POCode] = prePOCode;
                dt_row[header_PONoString] = PONo;
                dt_row[header_PODate] = PODate;
                dt_row[header_Customer] = ShortName;
                dt_row[header_CustomerCode] = CustTblCode;
                dt_row[header_Progress] = progress + "%";
                dt_row[header_DONoString] = "";
                dt.Rows.Add(dt_row);
            }

            string previousCustCode = "";

            dt.DefaultView.Sort = header_CustomerCode + " ASC";
            dt = dt.DefaultView.ToTable();

            foreach (DataRow row in dt.Rows)
            {
                pendingPOQty++;
                string custCode = row[header_CustomerCode].ToString();
                if (previousCustCode == "")
                {
                    previousCustCode = custCode;
                    pendingCustQty++;
                }
                else if (previousCustCode != custCode)
                {
                    previousCustCode = custCode;
                    pendingCustQty++;
                }
            }

            return Tuple.Create(pendingPOQty, pendingCustQty, pendingBags, pendingPkts, pendingPcs);
        }

        private Tuple<int, int, int> LoadPendingDOQty()//DataTable dt_DOList
        {
            int pendingDOQty = 0;
            int pendingCustQty = 0;
            int totalBag = 0;
            SBBDataDAL dalSPP = new SBBDataDAL();

            DataTable dt_DOList = dalSPP.SBBPageDOWithInfoSelect();

            DataTable dt = NewDOTable();
            DataRow dt_row;

            int preDOCode = -999999;

            foreach (DataRow row in dt_DOList.Rows)
            {
                int doCode = int.TryParse(row[dalSPP.DONo].ToString(), out doCode) ? doCode : -999999;

                int deliveryQty = int.TryParse(row[dalSPP.ToDeliveryQty].ToString(), out deliveryQty) ? deliveryQty : 0;

                int stdPacking = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;


                int bag = 0;

                if (stdPacking > 0)
                {
                    bag = deliveryQty / stdPacking;
                }

                bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                bool isDelivered = bool.TryParse(row[dalSPP.IsDelivered].ToString(), out isDelivered) ? isDelivered : false;

                if (deliveryQty > 0 && !isRemoved && !isDelivered)
                {
                    totalBag += bag;
                }

                if ((preDOCode == -999999 || preDOCode != doCode) && row[dalSPP.DONo] != DBNull.Value)
                {

                    if (deliveryQty > 0)
                    {

                        //string trfID = row[dalSPP.TrfTableCode].ToString();

                        bool dataMatched = true;

                        #region DO TYPE

                        if (!isDelivered)
                        {
                            dataMatched = dataMatched && true;
                        }
                        else
                        {
                            dataMatched = false;
                        }

                        if (isRemoved)
                        {
                            dataMatched = false;
                        }

                        #endregion

                        if (dataMatched)
                        {
                            preDOCode = doCode;
                            dt_row = dt.NewRow();

                            dt_row[header_CustomerCode] = row[dalSPP.CustTblCode];

                            dt.Rows.Add(dt_row);

                        }
                    }


                }

            }


            string previousCustCode = "";

            dt.DefaultView.Sort = header_CustomerCode + " ASC";
            dt = dt.DefaultView.ToTable();
            //908
            foreach (DataRow row in dt.Rows)
            {
                pendingDOQty++;
                string custCode = row[header_CustomerCode].ToString();
                if (previousCustCode == "")
                {
                    previousCustCode = custCode;
                    pendingCustQty++;
                }
                else if (previousCustCode != custCode)
                {
                    previousCustCode = custCode;
                    pendingCustQty++;
                }
            }
            //1
            return Tuple.Create(pendingDOQty, pendingCustQty, totalBag);
        }


        private void ShowTopCustomerRanking(DataTable dt_DeliveredData)
        {
            dt_DeliveredData.DefaultView.Sort = header_CustID + " ASC";
            dt_DeliveredData = dt_DeliveredData.DefaultView.ToTable();
            dt_DeliveredData.AcceptChanges();

            DataTable dt_Merge = dt_DeliveredData.Copy();


            string header_Removed = "REMOVED";

            dt_Merge.Columns.Add(header_Removed, typeof(string));

            string preCustID = null;

            int totalBagDelivered = 0;
            int totalBalPktDelivered = 0;
            int totalBalPcsDelivered = 0;
            int totalPcsDelivered = 0;

            for (int i = 0; i < dt_Merge.Rows.Count; i++)
            {
                string custID = dt_Merge.Rows[i][header_CustID].ToString();

                if (preCustID == null || preCustID != custID)
                {
                    preCustID = custID;
                }
                else if (preCustID == custID && i > 0)
                {

                    int newDeliveredBag = 0;
                    int newDeliveredPcs = 0;
                    int newDeliveredPkt = 0;
                    int newDeliveredPcsBal = 0;

                    int oldDeliveredPcs = int.TryParse(dt_Merge.Rows[i - 1][header_TotalPcs].ToString(), out oldDeliveredPcs) ? oldDeliveredPcs : 0;

                    newDeliveredPcs = int.TryParse(dt_Merge.Rows[i][header_TotalPcs].ToString(), out newDeliveredPcs) ? newDeliveredPcs : 0;

                    newDeliveredPcs += oldDeliveredPcs;

                    int oldDeliveredBag = int.TryParse(dt_Merge.Rows[i - 1][header_TotalBag].ToString(), out oldDeliveredBag) ? oldDeliveredBag : 0;

                    newDeliveredBag = int.TryParse(dt_Merge.Rows[i][header_TotalBag].ToString(), out newDeliveredBag) ? newDeliveredBag : 0;

                    newDeliveredBag += oldDeliveredBag;

                    int oldDeliveredPkt = int.TryParse(dt_Merge.Rows[i - 1][header_TotalPkt].ToString(), out oldDeliveredPkt) ? oldDeliveredPkt : 0;

                    newDeliveredPkt = int.TryParse(dt_Merge.Rows[i][header_TotalPkt].ToString(), out newDeliveredPkt) ? newDeliveredPkt : 0;

                    newDeliveredPkt += oldDeliveredPkt;

                    int oldDeliveredPcsBal = int.TryParse(dt_Merge.Rows[i - 1][header_TotalBalPcs].ToString(), out oldDeliveredPcsBal) ? oldDeliveredPcsBal : 0;

                    newDeliveredPcsBal = int.TryParse(dt_Merge.Rows[i][header_TotalBalPcs].ToString(), out newDeliveredPcsBal) ? newDeliveredPcsBal : 0;

                    newDeliveredPcsBal += oldDeliveredPcsBal;


                    dt_Merge.Rows[i][header_TotalPcs] = newDeliveredPcs;
                    dt_Merge.Rows[i][header_TotalBag] = newDeliveredBag;

                    dt_Merge.Rows[i - 1][header_Removed] = 1.ToString();
                }

            }

            var ORingDeliveredInfo = GetOringDeliveredQty();//1682ms

            int ORingDeliveredQty = ORingDeliveredInfo.Item2;
            DataTable dt_OringDelivered = ORingDeliveredInfo.Item1;

            if (ORingDeliveredQty > 0)
            {
                lblORingDelivered.Text = "+ O Ring " + ORingDeliveredQty.ToString() + " Packet(s)";
            }
            else
            {
                lblORingDelivered.Text = "-";

            }

            dt_Merge.AcceptChanges();

            dt_Merge.DefaultView.Sort = header_TotalBag + " DESC";
            dt_Merge = dt_Merge.DefaultView.ToTable();
            dt_Merge.AcceptChanges();

            foreach (DataRow row in dt_Merge.Rows)
            {
                string removed = row[header_Removed].ToString();

                if (removed == "1")
                    row.Delete();
            }

            dt_Merge.Columns.Remove(header_Removed);
            dt_Merge.AcceptChanges();

            //ResetTopCustomerData();

            for (int i = 0; i < dt_Merge.Rows.Count; i++)
            {
                string deliveredBag = dt_Merge.Rows[i][header_TotalBag].ToString();
                string custID = dt_Merge.Rows[i][header_CustID].ToString();
                int oringDeliveredBag = 0;
                //get oring deliverd for customer
                foreach (DataRow row in dt_OringDelivered.Rows)
                {
                    if (custID.Equals(row[header_CustID].ToString()))
                    {
                        oringDeliveredBag += int.TryParse(row[header_TotalBag].ToString(), out oringDeliveredBag) ? oringDeliveredBag : 0;
                    }
                }

                int temp = int.TryParse(deliveredBag, out temp) ? temp : 0;

                dt_Merge.Rows[i][header_TotalBag] = (temp - oringDeliveredBag).ToString();
            }
            dt_Merge.DefaultView.Sort = header_TotalBag + " DESC";
            dt_Merge = dt_Merge.DefaultView.ToTable();
            dt_Merge.AcceptChanges();

            int otherBagDelivered = 0;
            int otherBalPacketDelivered = 0;
            int otherBalPcsDelivered = 0;
            int otherORingDelivered = 0;

            for (int i = 0; i < dt_Merge.Rows.Count; i++)
            {
                totalBagDelivered += int.TryParse(dt_Merge.Rows[i][header_TotalBag].ToString(), out totalBagDelivered) ? totalBagDelivered : 0;
                totalBalPktDelivered += int.TryParse(dt_Merge.Rows[i][header_TotalPkt].ToString(), out totalBalPktDelivered) ? totalBalPktDelivered : 0;
                totalBalPcsDelivered += int.TryParse(dt_Merge.Rows[i][header_TotalBalPcs].ToString(), out totalBalPcsDelivered) ? totalBalPcsDelivered : 0;
                totalPcsDelivered += int.TryParse(dt_Merge.Rows[i][header_TotalPcs].ToString(), out totalPcsDelivered) ? totalPcsDelivered : 0;

                string custShortName = dt_Merge.Rows[i][header_CustShortName].ToString();
                string deliveredBag = dt_Merge.Rows[i][header_TotalBag].ToString();
                string deliveredBalPkt = dt_Merge.Rows[i][header_TotalPkt].ToString();
                string deliveredBalPcs = dt_Merge.Rows[i][header_TotalBalPcs].ToString();

                string custID = dt_Merge.Rows[i][header_CustID].ToString();

                string bal = "";


                //get oring deliverd for customer
                foreach (DataRow row in dt_OringDelivered.Rows)
                {
                    if (custID.Equals(row[header_CustID].ToString()))
                    {
                        int oringDeliveredBag = int.TryParse(row[header_TotalBag].ToString(), out oringDeliveredBag) ? oringDeliveredBag : 0;

                        int temp = int.TryParse(deliveredBag, out temp) ? temp : 0;

                        int excludeORingDeliveredBagQty = temp - oringDeliveredBag;

                        if (excludeORingDeliveredBagQty < 0)
                        {
                            excludeORingDeliveredBagQty = 0;
                        }

                        // deliveredBag = excludeORingDeliveredBagQty.ToString();

                        if (i >= 9)
                        {
                            otherORingDelivered += oringDeliveredBag;
                        }


                    }
                }

                if (!string.IsNullOrEmpty(deliveredBalPkt) && deliveredBalPkt != "0")
                {
                    bal += "+ " + deliveredBalPkt + " PKTS";
                }

                if (!string.IsNullOrEmpty(deliveredBalPcs) && deliveredBalPcs != "0")
                {
                    if (!string.IsNullOrEmpty(bal))
                    {
                        bal += " + " + deliveredBalPcs + " PCS";
                    }
                    else
                    {
                        bal += deliveredBalPcs + " PCS";
                    }
                }

                if (i == 0)
                {
                    lblTopCust_1.Text = custShortName;
                    lblTopCustomer_Bag_1.Text = deliveredBag;
                    lblTopCustomer_Bal_1.Text = bal;
                }
                else if (i == 1)
                {
                    lblTopCust_2.Text = custShortName;
                    lblTopCustomer_Bag_2.Text = deliveredBag;
                    lblTopCustomer_Bal_2.Text = bal;
                }
                else if (i == 2)
                {

                    lblTopCust_3.Text = custShortName;
                    lblTopCustomer_Bag_3.Text = deliveredBag;
                    lblTopCustomer_Bal_3.Text = bal;
                }
                else if (i == 3)
                {
                    lblTopCust_4.Text = custShortName;
                    lblTopCustomer_Bag_4.Text = deliveredBag;
                    lblTopCustomer_Bal_4.Text = bal;
                }
                else if (i == 4)
                {
                    lblTopCust_5.Text = custShortName;
                    lblTopCustomer_Bag_5.Text = deliveredBag;
                    lblTopCustomer_Bal_5.Text = bal;
                }
                else if (i == 5)
                {
                    lblTopCust_6.Text = custShortName;
                    lblTopCustomer_Bag_6.Text = deliveredBag;
                    lblTopCustomer_Bal_6.Text = bal;
                }
                else if (i == 6)
                {
                    lblTopCust_7.Text = custShortName;
                    lblTopCustomer_Bag_7.Text = deliveredBag;
                    lblTopCustomer_Bal_7.Text = bal;
                }
                else if (i == 7)
                {
                    lblTopCust_8.Text = custShortName;
                    lblTopCustomer_Bag_8.Text = deliveredBag;
                    lblTopCustomer_Bal_8.Text = bal;
                }
                else if (i == 8)
                {
                    lblTopCust_9.Text = custShortName;
                    lblTopCustomer_Bag_9.Text = deliveredBag;
                    lblTopCustomer_Bal_9.Text = bal;
                }
                else if (i >= 9)
                {

                    otherBagDelivered += int.TryParse(dt_Merge.Rows[i][header_TotalBag].ToString(), out int x) ? x : 0;
                    otherBalPacketDelivered += int.TryParse(dt_Merge.Rows[i][header_TotalPkt].ToString(), out int y) ? y : 0;
                    otherBalPcsDelivered += int.TryParse(dt_Merge.Rows[i][header_TotalBalPcs].ToString(), out int z) ? z : 0;

                }

            }


            string otherbal = "";

            if (otherBalPacketDelivered > 0)
            {
                otherbal += "+ " + otherBalPacketDelivered + " PKTS";
            }

            if (otherBalPcsDelivered > 0)
            {
                if (!string.IsNullOrEmpty(otherbal))
                {
                    otherbal += " + " + otherBalPcsDelivered + " PCS";
                }
                else
                {
                    otherbal += otherBalPcsDelivered + " PCS";
                }
            }
            if (otherBagDelivered > 0 || otherBalPacketDelivered > 0 || otherBalPcsDelivered > 0)
                lblTopCust_10.Text = "OTHER";

            if (otherBagDelivered - otherORingDelivered < 0)
            {
                lblTopCustomer_Bag_10.Text = "0";

            }
            else
            {
                lblTopCustomer_Bag_10.Text = (otherBagDelivered - otherORingDelivered).ToString();

            }

            //if (!string.IsNullOrEmpty(otherbal))
            //{
            //    lblTopCustomer_Bal_10.Text = otherbal;
            //}
            //else
            //{
            //    lblTopCustomer_Bal_10.Text = "";
            //}
            lblTopCustomer_Bal_10.Text = "";
            lblMonthlyDelivered.Text = (totalBagDelivered - ORingDeliveredQty).ToString();

            ShowDeliverdPercentage(totalBagDelivered);

            string balDelivered = "";

            if (totalBalPktDelivered > 0)
            {
                balDelivered += " + " + totalBalPktDelivered + " PKTS";
            }

            if (totalBalPcsDelivered > 0)
            {
                balDelivered += " + " + totalBalPcsDelivered + " PCS";

            }

            balDelivered += " (TOTAL " + totalPcsDelivered + " PCS)";

            lblDeliveredBalance.Text = balDelivered;//1948ms
        }




        private void LoadDeliveredData()
        {
            //reset delivered data
            ResetDeliveredData();

            DataTable dt_DeliveredReport = NewDeliveredTable();

            DateTime now = DateTime.Now;
            string start = MonthlyDateStart.ToString("yyyy/MM/dd");
            string end = MonthlyDateEnd.ToString("yyyy/MM/dd");

            foreach (DataRow row_DO in dt_DOWithTrfInfoSelectedPeriod.Rows)
            {
                string trfResult = row_DO[dalTrfHist.TrfResult].ToString();

                if (trfResult == text.Passed)
                {
                    int custID = int.TryParse(row_DO[dalSBB.CustTblCode].ToString(), out custID) ? custID : -1;
                    string shortName = row_DO[dalSBB.ShortName].ToString();

                    int QtyPerBag = int.TryParse(row_DO[dalSBB.QtyPerBag].ToString(), out QtyPerBag) ? QtyPerBag : 1;
                    int QtyPerPkt = int.TryParse(row_DO[dalSBB.QtyPerPacket].ToString(), out QtyPerPkt) ? QtyPerPkt : 1;



                    int deliveredPcs = int.TryParse(row_DO[dalTrfHist.TrfQty].ToString(), out deliveredPcs) ? deliveredPcs : 0;
                    int deliveredBag = deliveredPcs / QtyPerBag;

                    int deliveredPkt = 0;

                    int deliveredBalPcs = deliveredPcs % QtyPerBag;

                    if (QtyPerPkt != 0)
                    {
                        deliveredPkt = deliveredPcs % QtyPerBag / QtyPerPkt;
                        deliveredBalPcs = deliveredPcs % QtyPerPkt;
                    }
                    else
                    {
                        deliveredBalPcs = deliveredPcs % QtyPerBag;
                    }



                    DataRow newRow = dt_DeliveredReport.NewRow();

                    newRow[header_CustID] = custID;
                    newRow[header_CustShortName] = shortName;

                    newRow[header_TotalPcs] = deliveredPcs;
                    newRow[header_TotalBag] = deliveredBag;
                    newRow[header_TotalPkt] = deliveredPkt;
                    newRow[header_TotalBalPcs] = deliveredBalPcs;

                    dt_DeliveredReport.Rows.Add(newRow);
                }

            }
            //2
            if (dt_DeliveredReport != null && dt_DeliveredReport.Rows.Count > 0)
                ShowTopCustomerRanking(dt_DeliveredReport);
            //105
        }

        private Tuple<DataTable, int> GetOringDeliveredQty()
        {
            DataTable dt = NewDeliveredTable();
            int deliveredBag = 0;


            if(dt_DOWithTrfInfoSelectedPeriod == null || dt_DOWithTrfInfoSelectedPeriod.Rows.Count <= 0)
            {
                dt_DOWithTrfInfoSelectedPeriod = dalSBB.SBBPageDOWithTrfInfoSelect(MonthlyDateStart.ToString("yyyy/MM/dd"), MonthlyDateEnd.ToString("yyyy/MM/dd"));
            }

            DataTable dt_DOList = dt_DOWithTrfInfoSelectedPeriod.Copy();

            foreach (DataRow row in dt_DOList.Rows)
            {
                string trfResult = row[dalTrfHist.TrfResult].ToString();
                string itemCode = row[dalSBB.ItemCode].ToString();

                if (trfResult == "Passed" && itemCode.Contains("CFPOR"))
                {
                    int deliveredPcs = int.TryParse(row[dalTrfHist.TrfQty].ToString(), out deliveredPcs) ? deliveredPcs : 0;

                    int qtyPerBag = int.TryParse(row[dalSBB.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                    deliveredBag += deliveredPcs / qtyPerBag;

                    int custID = int.TryParse(row[dalSBB.CustTblCode].ToString(), out custID) ? custID : 0;


                    DataRow newRow = dt.NewRow();

                    newRow[header_CustID] = custID;
                    //newRow[header_CustShortName] = shortName;

                    newRow[header_TotalPcs] = deliveredPcs;
                    newRow[header_TotalBag] = deliveredPcs / qtyPerBag;
                    // newRow[header_TotalPkt] = deliveredPkt;
                    //newRow[header_TotalBalPcs] = deliveredBalPcs;

                    dt.Rows.Add(newRow);
                }
            }

            return Tuple.Create(dt, deliveredBag);
        }

        private void LoadPendingSummary()
        {
            //string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

            LoadDeliveredData();//1794ms

            var result = LoadPendingPOQty();

            lblPendingPO.Text = result.Item1.ToString();
            lblPendingPOCust.Text = result.Item2.ToString();
            lblPOBagQty.Text = result.Item3.ToString();

            if (result.Item4 > 0)
            {
                lblBalancePkt.Text = " + " + result.Item4.ToString() + " PKT(s)";
            }
            else
            {
                lblBalancePkt.Text = "";
            }

            if (result.Item5 > 0)
            {
                lblBalancePcs.Text = " + " + result.Item5.ToString() + " PC(s)";
            }
            else
            {
                lblBalancePcs.Text = "";
            }
            //262->226
            var result2 = LoadPendingDOQty();//729>>3181ms>>84ms(07/11/2024)
            lblPendingDO.Text = result2.Item1.ToString();
            lblPendingDOCust.Text = result2.Item2.ToString();
            lblDOBagQty.Text = result2.Item3.ToString();//574
        }

        private void RefreshPage()
        {
            NewInitialDBData();//429ms>>461ms>>946ms>>18132ms

            LoadPendingSummary();//1289ms>>2005ms

            LoadStockInfo();//376ms>>312ms

            //LoadUsage();//1142ms>>1251ms
        }

        private void RefreshPage(bool showUpdatedDataOnly)
        {

            frmLoading.ShowLoadingScreen();

            NewInitialDBData();//429ms>>461ms>>366ms>>3597ms>>752ms(07/11/2024)

            LoadPendingSummary();//1289ms>>2005ms>>5147ms>>1926ms(07/11/2024)

            LoadStockInfo();//376ms>>312ms>>205ms

            frmLoading.CloseForm();
        }

        private void RefreshPage(int mode)
        {
            //1: from PO List

            frmLoading.ShowLoadingScreen();

            NewInitialDBData();//429ms>>461ms>>366ms>>3597ms>>752ms(07/11/2024)

            if (mode == 1)
            {
                var result = LoadPendingPOQty();

                lblPendingPO.Text = result.Item1.ToString();
                lblPendingPOCust.Text = result.Item2.ToString();
                lblPOBagQty.Text = result.Item3.ToString();

                if (result.Item4 > 0)
                {
                    lblBalancePkt.Text = " + " + result.Item4.ToString() + " PKT(s)";
                }
                else
                {
                    lblBalancePkt.Text = "";
                }

                if (result.Item5 > 0)
                {
                    lblBalancePcs.Text = " + " + result.Item5.ToString() + " PC(s)";
                }
                else
                {
                    lblBalancePcs.Text = "";
                }
            }
            else
            {
                LoadPendingSummary();//1289ms>>2005ms>>5147ms>>1926ms(07/11/2024)
            }


            LoadStockInfo();//376ms>>312ms>>205ms

            frmLoading.CloseForm();
        }

        //private DataTable LoadUsageItemList()
        //{
        //    //DataTable dt_Item = dalItem.Select();
        //    //string itemCust = text.SPP_BrandName;
        //    //DataTable dt_SBBItemSelect = dalItemCust.SBBItemSelect(itemCust);

        //    DataTable dt_UsageItemList = NewUsageTable();
        //    DataTable dt_ChildPart = NewUsageTable();

        //    DataTable dt_ChildMat = new DataTable();

        //    dt_ChildMat.Columns.Add(dalSBB.ItemCode, typeof(string));
        //    dt_ChildMat.Columns.Add(dalSBB.ItemName, typeof(string));

        //    DataTable dt_Packaging = new DataTable();

        //    dt_Packaging.Columns.Add(dalSBB.ItemCode, typeof(string));
        //    dt_Packaging.Columns.Add(dalSBB.ItemName, typeof(string));

        //    foreach (DataRow row in dt_SBBItemSelect.Rows)
        //    {
        //        string parentCode = row[dalSBB.ItemCode].ToString();

        //        if (!string.IsNullOrEmpty(parentCode) && parentCode.Length > 7 && parentCode[7].ToString() == "E" && parentCode[8].ToString() != "C")
        //        {
        //            parentCode = GetChildCode(dt_JoinSelectWithChildCat, parentCode);
        //        }

        //        //if parentCode is production part

        //        foreach (DataRow join in dt_JoinSelectWithChildCat.Rows)
        //        {
        //            if (parentCode == join[dalJoin.ParentCode].ToString())
        //            {
        //                string childCat = join[dalJoin.ChildCat].ToString();
        //                string childCode = join[dalJoin.ChildCode].ToString();
        //                string childName = join[dalJoin.ChildName].ToString();

        //                string childMat = join[dalItem.ItemMaterial].ToString();
        //                string childRecycle = join[dalItem.ItemRecycleMat].ToString();
        //                string childColorMat = join[dalItem.ItemMBatch].ToString();


        //                bool childPartDuplicate = false;

        //                if (dt_ChildPart != null && dt_ChildPart.Rows.Count > 0)
        //                {
        //                    foreach (DataRow part in dt_ChildPart.Rows)
        //                    {
        //                        if (childCode == part[header_ItemCode].ToString())
        //                        {
        //                            childPartDuplicate = true;
        //                            break;
        //                        }
        //                    }
        //                }

        //                if (!childPartDuplicate && childCat == text.Cat_Part)
        //                {
        //                    DataRow newRow = dt_ChildPart.NewRow();

        //                    newRow[header_ItemCode] = childCode;
        //                    newRow[header_ItemName] = childName;

        //                    dt_ChildPart.Rows.Add(newRow);

        //                    bool matFound = false;
        //                    bool recycleFound = false;
        //                    bool colorMatFound = false;

        //                    foreach (DataRow part in dt_ChildMat.Rows)
        //                    {
        //                        if (childMat == part[dalSBB.ItemCode].ToString())
        //                        {
        //                            matFound = true;
        //                        }

        //                        if (childRecycle == part[dalSBB.ItemCode].ToString())
        //                        {
        //                            recycleFound = true;
        //                        }

        //                        if (childColorMat == part[dalSBB.ItemCode].ToString())
        //                        {
        //                            colorMatFound = true;
        //                        }
        //                    }

        //                    if (!matFound && !string.IsNullOrEmpty(childMat))
        //                    {
        //                        newRow = dt_ChildMat.NewRow();

        //                        newRow[dalSBB.ItemCode] = childMat;
        //                        newRow[dalSBB.ItemName] = tool.getItemNameFromDataTable(dt_Item, childMat);

        //                        dt_ChildMat.Rows.Add(newRow);
        //                    }

        //                    if (!recycleFound && !string.IsNullOrEmpty(childRecycle))
        //                    {
        //                        newRow = dt_ChildMat.NewRow();

        //                        newRow[dalSBB.ItemCode] = childRecycle;
        //                        newRow[dalSBB.ItemName] = tool.getItemNameFromDataTable(dt_Item, childRecycle);

        //                        dt_ChildMat.Rows.Add(newRow);
        //                    }

        //                    if (!colorMatFound && !string.IsNullOrEmpty(childColorMat))
        //                    {
        //                        newRow = dt_ChildMat.NewRow();

        //                        newRow[dalSBB.ItemCode] = childColorMat;
        //                        newRow[dalSBB.ItemName] = tool.getItemNameFromDataTable(dt_Item, childColorMat);

        //                        dt_ChildMat.Rows.Add(newRow);
        //                    }
        //                }
        //                else if (childCat != text.Cat_Part)
        //                {
        //                    bool otherMaterialFound = false;

        //                    foreach (DataRow part in dt_Packaging.Rows)
        //                    {
        //                        if (childCode == part[dalSBB.ItemCode].ToString())
        //                        {
        //                            otherMaterialFound = true;
        //                        }

        //                    }

        //                    if (!otherMaterialFound && !string.IsNullOrEmpty(childCode))
        //                    {
        //                        DataRow newRow = dt_Packaging.NewRow();

        //                        newRow[dalSBB.ItemCode] = childCode;
        //                        newRow[dalSBB.ItemName] = childName;

        //                        dt_Packaging.Rows.Add(newRow);
        //                    }
        //                }
        //            }
        //        }


        //    }

        //    if (dt_ChildPart != null && dt_ChildPart.Rows.Count > 0 && cbParts.Checked)
        //    {
        //        foreach (DataRow row in dt_ChildPart.Rows)
        //        {
        //            DataRow newRow = dt_UsageItemList.NewRow();

        //            newRow[header_ItemCode] = row[header_ItemCode];
        //            newRow[header_ItemName] = row[header_ItemName];

        //            dt_UsageItemList.Rows.Add(newRow);
        //        }
        //    }

        //    if (dt_ChildMat != null && dt_ChildMat.Rows.Count > 0 && cbMat.Checked)
        //    {
        //        foreach (DataRow row in dt_ChildMat.Rows)
        //        {
        //            DataRow newRow = dt_UsageItemList.NewRow();

        //            newRow[header_ItemCode] = row[dalSBB.ItemCode];
        //            newRow[header_ItemName] = row[dalSBB.ItemName];

        //            dt_UsageItemList.Rows.Add(newRow);
        //        }
        //    }

        //    if (dt_Packaging != null && dt_Packaging.Rows.Count > 0 && cbPackaging.Checked)
        //    {
        //        foreach (DataRow row in dt_Packaging.Rows)
        //        {
        //            DataRow newRow = dt_UsageItemList.NewRow();

        //            newRow[header_ItemCode] = row[dalSBB.ItemCode];
        //            newRow[header_ItemName] = row[dalSBB.ItemName];

        //            dt_UsageItemList.Rows.Add(newRow);
        //        }
        //    }

        //    return dt_UsageItemList;
        //}

        private void OLDLoadUsage()
        {
            Cursor = Cursors.WaitCursor;

            #region indicate start and end date

            string start;
            string end;

            DateTime dateStart = dtpDate1.Value;
            DateTime dateEnd = dtpDate2.Value;

            if (dateStart > dateEnd)
            {
                dateStart = dtpDate2.Value;
                dateEnd = dtpDate1.Value;
            }
            start = dateStart.ToString("yyyy/MM/dd");
            end = dateEnd.ToString("yyyy/MM/dd");

            //int totalDay = tool.getNumberOfDayBetweenTwoDate(dateStart, dateEnd, false);

            int totalDay = TOTAL_DAYS_IN_PERIOD;
            int totalSunday = TOTAL_SUNDAYS_IN_PERIOD;

            bool IncludeSunday = cbSundayisWorkday.Checked;

            dt_TrfRangeUsageSearch = dalTrfHist.SBBPageRangeUsageSearch(start, end);

            #endregion

            //DataTable dt_ItemList = LoadUsageItemList();
            //dt_ItemList = RearrangeIndex(dt_ItemList);

            DataTable dt_ItemList = (DataTable)dgvStockAlert.DataSource;

            foreach (DataRow row in dt_ItemList.Rows)
            {
                string itemCode = row[header_ItemCode].ToString();

                int totPro = 0;
                int totAssy = 0;

                foreach (DataRow trfRow in dt_TrfRangeUsageSearch.Rows)
                {
                    string trfResult = trfRow[dalTrfHist.TrfResult].ToString();
                    string trfItemCode = trfRow[dalTrfHist.TrfItemCode].ToString();

                    if (trfResult == text.Passed && itemCode == trfItemCode)
                    {
                        int trfQty = int.TryParse(trfRow[dalTrfHist.TrfQty].ToString(), out trfQty) ? trfQty : 0;

                        string trfFrom = trfRow[dalTrfHist.TrfFrom].ToString();
                        string trfTo = trfRow[dalTrfHist.TrfTo].ToString();

                        if (trfFrom == text.Production || trfTo == text.Production)
                        {
                            totPro += trfQty;
                        }

                        if (trfTo == text.Assembly)
                        {
                            totAssy += trfQty;
                        }

                        row[header_TotalProduction] = totPro;
                        row[header_TotalAssembly] = totAssy;
                        row[header_DailyUsage] = totAssy / totalDay;
                    }
                }
            }


            Cursor = Cursors.Arrow;

            //dgvUsage.DataSource = dt_ItemList;

            // DgvUIEdit(dgvUsage);

            //dgvUsage.ClearSelection();
            btnStockInfoReload.Visible = false;

        }

        private void LoadUsage()
        {
            Cursor = Cursors.WaitCursor;

            #region Calculate Period Details
            DateTime dateStart = dtpDate1.Value;
            DateTime dateEnd = dtpDate2.Value;
            if (dateStart > dateEnd)
            {
                DateTime temp = dateStart;
                dateStart = dateEnd;
                dateEnd = temp;
            }

            string start = dateStart.ToString("yyyy/MM/dd");
            string end = dateEnd.ToString("yyyy/MM/dd");

            int workingDays = cbSundayisWorkday.Checked ? TOTAL_DAYS_IN_PERIOD : (TOTAL_DAYS_IN_PERIOD - TOTAL_SUNDAYS_IN_PERIOD);
            int prodLeadDays = int.TryParse(txtProdLeadTime.Text, out int leadDays) ? leadDays : 5;

            if (dgvStockAlert.Columns.Contains(header_DailyUsage))
            {
                dgvStockAlert.Columns[header_DailyUsage].HeaderText = "Daily Usage (" + workingDays + " days)";
            }

            if (dgvStockAlert.Columns.Contains(header_MonthlyUsage))
            {
                dgvStockAlert.Columns[header_MonthlyUsage].HeaderText = "Monthly Usage (" + (cbSundayisWorkday.Checked ? 30 : 26) + " days)";
            }

            dt_TrfRangeUsageSearch = dalTrfHist.SBBPageRangeUsageSearch(start, end);
            #endregion

            DataTable dt_ItemList = (DataTable)dgvStockAlert.DataSource;

            foreach (DataRow row in dt_ItemList.Rows)
            {
                string itemCode = row[header_ItemCode].ToString();   
                string itemCat = tool.getItemCatFromDataTable(dt_Item, itemCode);

                if(itemCode == "PP PD855")
                {
                    var checkpoint = 1;
                }

                int totPro = 0;
                int totUsage = 0;
                int materialUsedforProductionInKG = 0;


                foreach (DataRow trfRow in dt_TrfRangeUsageSearch.Rows)
                {
                    string trfResult = trfRow[dalTrfHist.TrfResult].ToString();
                    string trfItemCode = trfRow[dalTrfHist.TrfItemCode].ToString();
                    string trfUnit = trfRow[dalTrfHist.TrfUnit].ToString();

                    if (trfResult == text.Passed && itemCode == trfItemCode)
                    {
                        string trfstring = trfRow[dalTrfHist.TrfQty].ToString();
                        //int trfQty = int.TryParse(trfRow[dalTrfHist.TrfQty].ToString(), out trfQty) ? trfQty : 0;

                        double dblVal = double.TryParse(trfstring, out double d) ? d : 0;
                        int trfQty = (int)Math.Round(dblVal);   // or (int)d if you want truncation

                        string trfFrom = trfRow[dalTrfHist.TrfFrom].ToString();
                        string trfTo = trfRow[dalTrfHist.TrfTo].ToString();

                        // Production: Production → Semenyih
                        if (trfFrom == text.Production && trfTo == text.Factory_Semenyih )
                        {
                            totPro += trfQty;
                        }
                       

                        // Usage based on category using your helper methods
                        if (IsPartOrPackagingOrSubMaterial(itemCat))
                        {
                            // Parts/Packaging/SubMaterial: → Assembly
                            if (trfTo == text.Assembly)
                            {
                                totUsage += trfQty;
                            }
                        }
                        else if (IsMaterial(itemCat))
                        {
                            // Materials: → Production
                            if (trfTo == text.Production)
                            {
                                totUsage += trfQty;

                                if(trfUnit.ToUpper().Equals("G"))
                                {
                                    materialUsedforProductionInKG += (int) Math.Ceiling( (decimal) trfQty / 1000);
                                }
                                else
                                {
                                    materialUsedforProductionInKG += (int)Math.Ceiling((decimal)trfQty);
                                }
                            }
                        }
                    }
                }

                // Calculate daily usage and dates
                double dailyUsage = workingDays > 0 ? (double)totUsage / workingDays : 0;
                int currentStock = int.TryParse(row[header_Stock].ToString(), out currentStock) ? currentStock : 0;


                DateTime stockZeroDate = DateTime.MaxValue;
                DateTime startProdDate = DateTime.MaxValue;

                if (dailyUsage > 0)
                {
                    double daysUntilZero = currentStock / dailyUsage;
                    stockZeroDate = CalculateWorkingDate(DateTime.Today, (int)Math.Ceiling(daysUntilZero), cbSundayisWorkday.Checked);
                    startProdDate = CalculateWorkingDate(stockZeroDate, -prodLeadDays, cbSundayisWorkday.Checked);
                }

                if (IsMaterial(itemCat))
                {
                    if (itemCode.Contains("PD"))
                    {
                        var checkpoint = 1;
                    }

                    row[header_TotalProduction] = materialUsedforProductionInKG;
                    row[header_TotalAssembly] = DBNull.Value;

                    dailyUsage = workingDays > 0 ? (double)materialUsedforProductionInKG / workingDays : 0;

                    row[header_DailyUsage] = Math.Round(dailyUsage, 1);
                    row[header_MonthlyUsage] = Math.Round(dailyUsage, 1) * (cbSundayisWorkday.Checked ? 30 : 26);

                    if (dailyUsage > 0)
                    {
                        double daysUntilZero = currentStock / dailyUsage;

                        stockZeroDate = CalculateWorkingDate(DateTime.Today, (int)Math.Ceiling(daysUntilZero), cbSundayisWorkday.Checked);

                        //row[header_Est_Stock_Zero_Date] = stockZeroDate.ToString("dd/MM/yyyy");
                        row[header_Est_Stock_Zero_Date] = stockZeroDate;
                    }

                }
                else
                {
                    row[header_TotalProduction] = totPro;
                    row[header_TotalAssembly] = totUsage;
                    row[header_DailyUsage] = Math.Round(dailyUsage, 1);
                    row[header_MonthlyUsage] = Math.Round(dailyUsage, 1) * (cbSundayisWorkday.Checked ? 30 : 26);

                    // Add new columns using your header constants
                    //row[header_Est_Stock_Zero_Date] = stockZeroDate.ToString("dd/MM/yyyy");

                    //
                    //if (itemCat == text.Cat_Part)
                    //    row[header_Start_Prod_By_Date] = startProdDate.ToString("dd/MM/yyyy");

                    if (itemCat == text.Cat_Part)
                    {
                        row[header_Start_Prod_By_Date] = startProdDate;
                        row[header_Est_Stock_Zero_Date] = stockZeroDate;
                    }
                    else
                    {
                        row[header_Start_Prod_By_Date] = DBNull.Value;
                        row[header_Est_Stock_Zero_Date] = DBNull.Value;
                    }
                }

                //if(TOTAL_DAYS_IN_PERIOD < 30)
                //{
                //    row[header_Est_Stock_Zero_Date] = "Period too short(<30)";
                //    row[header_Start_Prod_By_Date] = "Period too short(<30)";
                //}
                //else
                //{



                //}

            }

            btnStockInfoReload.Visible = false;
            SetupDateColumnFormatting(dgvStockAlert);
            Cursor = Cursors.Arrow;
        }

        private void SetupDateColumnFormatting(DataGridView dgv)
        {
            // Set date format for display (Malaysian format: dd/MM/yyyy)
            if (dgv.Columns.Contains(header_Est_Stock_Zero_Date))
            {
                dgv.Columns[header_Est_Stock_Zero_Date].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgv.Columns[header_Est_Stock_Zero_Date].DefaultCellStyle.NullValue = "";
            }

            if (dgv.Columns.Contains(header_Start_Prod_By_Date))
            {
                dgv.Columns[header_Start_Prod_By_Date].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgv.Columns[header_Start_Prod_By_Date].DefaultCellStyle.NullValue = "";
            }
        }

        // Helper method for working date calculation
        private DateTime CalculateWorkingDate(DateTime startDate, int workingDays, bool sundayIsWorkday)
        {
            DateTime result = startDate;
            int daysProcessed = 0;
            int direction = workingDays >= 0 ? 1 : -1;
            int totalDaysNeeded = Math.Abs(workingDays);

            while (daysProcessed < totalDaysNeeded)
            {
                result = result.AddDays(direction);

                // Skip Sunday if it's not a working day
                if (!sundayIsWorkday && result.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                daysProcessed++;
            }

            // If result falls on Sunday and Sunday is not workday, move to Monday
            if (!sundayIsWorkday && result.DayOfWeek == DayOfWeek.Sunday)
                result = result.AddDays(1);

            return result;
        }

        // Helper methods to determine item category
        private bool IsPartOrPackagingOrSubMaterial(string itemCategory)
        {
            // Add your logic to identify parts and packaging categories
            return itemCategory == text.Cat_Part || itemCategory == text.Cat_Packaging || itemCategory == text.Cat_SubMat; ; // Adjust as needed
        }

        private bool IsMaterial(string itemCategory)
        {
            // Add your logic to identify material categories  
            return itemCategory == text.Cat_RawMat || itemCategory == text.Cat_MB || itemCategory == text.Cat_Pigment; // Adjust as needed
        }

        public static void Reload()
        {
            _instance.RefreshPage(true);
        }

        public static void Reload(int mode)
        {
            //1: from PO List
            _instance.RefreshPage(1);
        }

        private void OpenDOList(object sender, EventArgs e)
        {
            //int count = Application.OpenForms.OfType<frmFinex>().Count();


            //if (count == 1)
            //{
            //    //Form is already open
            //    Application.OpenForms.OfType<frmFinex>().First().BringToFront();
            //    Application.OpenForms.OfType<frmFinex>().First().WindowState = FormWindowState.Normal;
            //}
            //else
            //{
            //    // Form is not open
            //    frmFinex frm = new frmFinex
            //    {
            //        StartPosition = FormStartPosition.CenterScreen
            //    };

            //    frm.Show();
            //}


            int count = Application.OpenForms.OfType<frmSBBDOListVer3>().Count();


            if (count == 1)
            {
                //Form is already open
                Application.OpenForms.OfType<frmSBBDOListVer3>().First().BringToFront();
                Application.OpenForms.OfType<frmSBBDOListVer3>().First().WindowState = FormWindowState.Normal;
            }
            else
            {
                // Form is not open
                frmSBBDOListVer3 frm = new frmSBBDOListVer3
                {
                    StartPosition = FormStartPosition.CenterScreen
                };


                frm.Show();
            }



        }

        private void WorkInProgressMessage()
        {
            // MessageBox.Show("work in progress...");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void DailyJobSheet_Click(object sender, EventArgs e)
        {
            int count = Application.OpenForms.OfType<frmProductionRecordVer3>().Count();


            if (count == 1)
            {
                //Form is already open
                Application.OpenForms.OfType<frmProductionRecordVer3>().First().BringToFront();
                Application.OpenForms.OfType<frmProductionRecordVer3>().First().WindowState = FormWindowState.Normal;
            }
            else
            {
                // Form is not open
                frmProductionRecordVer3 frm = new frmProductionRecordVer3
                {
                    StartPosition = FormStartPosition.CenterScreen
                };


                frm.Show();
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //frmSBBPrice frm = new frmSBBPrice
            //{
            //    StartPosition = FormStartPosition.CenterScreen
            //};

            //frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = Application.OpenForms.OfType<frmSBBPOVSStock>().Count();


            if (count == 1)
            {
                //Form is already open
                Application.OpenForms.OfType<frmSBBPOVSStock>().First().BringToFront();
                Application.OpenForms.OfType<frmSBBPOVSStock>().First().WindowState = FormWindowState.Maximized;
            }
            else
            {
                // Form is not open
                frmSBBPOVSStock frm = new frmSBBPOVSStock
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                frm.Show();
            }

        }

        private void OpenInternalDOManagement(object sender, EventArgs e)
        {

            int count = Application.OpenForms.OfType<frmDOManagement>().Count();


            if (count == 1)
            {
                //Form is already open
                Application.OpenForms.OfType<frmDOManagement>().First().BringToFront();
                Application.OpenForms.OfType<frmDOManagement>().First().WindowState = FormWindowState.Normal;
            }
            else
            {
                // Form is not open
                frmDOManagement frm = new frmDOManagement
                {
                    StartPosition = FormStartPosition.CenterScreen
                };


                frm.ShowDialog();
                // frm.Activate();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();

            //frmSBBMatPlan frm = new frmSBBMatPlan
            //{
            //    StartPosition = FormStartPosition.CenterScreen
            //};

            //frm.ShowDialog();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

            RefreshPage();

        }

        private void dgvStockAlert_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvStockAlert;
            dgv.SuspendLayout();
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == header_Stock || dgv.Columns[col].Name == header_BalAfter || dgv.Columns[col].Name == header_BalAfterBag || dgv.Columns[col].Name == header_BalAfterPcs)
            {
                if (dgv.Rows[row].Cells[col].Value != null)
                {
                    if (!string.IsNullOrEmpty(dgv.Rows[row].Cells[col].Value.ToString()))
                    {
                        float num = dgv.Rows[row].Cells[col].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[col].Value.ToString());

                        if (num < 0)
                        {
                            dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                        }
                    }
                }
            }
            else if (dgv.Columns[col].Name == header_ProDaysNeeded)
            {
                string status = dgv.Rows[row].Cells[header_Status].Value.ToString();

                if (status == text.planning_status_running)
                {
                    //dgv.Rows[row].Cells[header_ProDaysNeeded].Style.ForeColor = Color.FromArgb(52, 139, 209);
                }
                else
                {
                    //dgv.Rows[row].Cells[header_ProDaysNeeded].Style.ForeColor = Color.Black;
                }
            }

            dgv.ResumeLayout();


        }

        private void cbInPcsUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (Loaded)
            {
                if (cbInPcsUnit.Checked)
                {
                    if (STOCK_INFO_ITEM_TYPE == Type_Product)
                        cbInBagUnit.Checked = false;

            
                    LoadStockInfo();
                }
                else
                {
                    if (STOCK_INFO_ITEM_TYPE == Type_Product)
                    {
                        cbInBagUnit.Checked = true;
                    }
                    else
                    {
                        cbInPcsUnit.Checked = true;
                    }


                }

                // dgvStockAlert.DataSource = null;
            }

        }

        private void cbInBagUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (Loaded)
            {
                if (cbInBagUnit.Checked)
                {

                    if (STOCK_INFO_ITEM_TYPE == Type_Product)
                    {
                        cbInPcsUnit.Checked = false;
                        //LoadStockAlert();
                        LoadStockInfo();
                    }
                    else
                    {
                        cbInBagUnit.Checked = false;
                        cbInPcsUnit.Checked = true;
                    }

                }
                else if (!cbInPcsUnit.Checked)
                {
                    cbInBagUnit.Checked = true;
                }

                //dgvStockAlert.DataSource = null;
            }

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }


        private void StockInfoShowRemark(bool Show)
        {
            if(Show)
            {
                tlpStockInfoMainSetting.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 450f);
                txtRemark.Visible = true;
                lblSetRemarkToAll.Visible = true;

            }
            else
            {
                tlpStockInfoMainSetting.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0f);
                txtRemark.Visible = false;
                lblSetRemarkToAll.Visible = false;

            }

        }

        private void ResetStdPackingTracking()
        {
            hasStdPackingChanges = false;
            changedItemCodes.Clear();
            btnSaveStdPacking.Visible = false;
        }

        private void StockCheckUIMode(bool turnOn)
        {
            DataGridView dgv = dgvStockAlert;

            HideFilter(turnOn);
            btnSaveStdPacking.Visible = false;

            ToggleUIExpansion();
            if (turnOn)
            {
                cbEditStdPacking.Checked = false;
                dgvStockAlert.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgvStockAlert.ReadOnly = false;
                dgvStockAlert.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                tlpStockInfoMainSetting.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpStockInfoMainSetting.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpStockInfoMainSetting.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpStockInfoMainSetting.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 114f);

                StockInfoShowRemark(true);

                //dgv.Columns[header_ItemName].Frozen = true;
                STOCK_CHECK_MODE = true;

                btnCancelStockCheck.Visible = true;
                btnStockCheck.Enabled = false;
                btnStockCheck.Text = "TALLY ALL";
                lblStockAlert.Text = "Stock Check";

                btnStockRefresh.Visible = false;
                cbProduct.Visible = false;
                cbInPcsUnit.Visible = false;
                cbInBagUnit.Visible = false;

               

                dgv.Columns[header_Qty_White].Visible = true;
                dgv.Columns[header_Qty_Blue].Visible = true;
                dgv.Columns[header_Qty_Yellow].Visible = true;
                dgv.Columns[header_Balance].Visible = true;

                dgv.Columns[header_StockMinLvl].Visible = false;
                dgv.Columns[header_Note].Visible = false;

                dgv.Columns[header_Remark].Visible = true;

                dgv.Columns[header_BalAfter].Visible = false;
                dgv.Columns[header_BalAfterBag].Visible = false;
                dgv.Columns[header_BalAfterPcs].Visible = false;

                dgv.Columns[header_ProDaysNeeded].Visible = false;

                dgv.Columns[header_ActualStock].Visible = true;
                //dgv.Columns[header_ActualStock].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[header_ActualStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[header_StockDiff].Visible = true;
                dgv.Columns[header_StockDiff].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[header_ActualStock].Visible = true;

                //dgv.Columns[header_StdPacking_Bag].Visible = false;
                //dgv.Columns[header_StdPacking_Ctn].Visible = false;
                dgv.Columns[header_StdPacking_String].Visible = true;
               // dgv.Columns[header_ActualStock_PCS].Visible = false;
               //// dgv.Columns[header_ActualStock_BAG].Visible = false;
                //dgv.Columns[header_ActualStock_CTN].Visible = false;

                dgv.Columns[header_TotalAssembly].Visible = false;
                dgv.Columns[header_TotalProduction].Visible = false;
                dgv.Columns[header_DailyUsage].Visible = false;

                dgv.Columns[header_MonthlyUsage].Visible = false;
                dgv.Columns[header_Est_Stock_Zero_Date].Visible = false;
                dgv.Columns[header_MaxProdPerDay].Visible = false;
                dgv.Columns[header_Start_Prod_By_Date].Visible = false;
                //DataTable dt = (DataTable)dgv.DataSource;

                //dt.DefaultView.Sort = header_ItemName + " asc";
                //dt = dt.DefaultView.ToTable();
            }
            else
            {
                ResetStdPackingTracking();
                dgvStockAlert.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                STOCK_CHECK_MODE = false;

                dgv.Columns[header_ItemName].Frozen = false;
                tlpStockInfoMainSetting.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 40f);
                tlpStockInfoMainSetting.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 105f);
                tlpStockInfoMainSetting.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 150f);
                tlpStockInfoMainSetting.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 0f);
                StockInfoShowRemark(false);

                dgv.Columns[header_Qty_White].Visible = false;
                dgv.Columns[header_StdPacking_White].Visible = false;
                dgv.Columns[header_Qty_Blue].Visible = false;
                dgv.Columns[header_StdPacking_BLue].Visible = false;
                dgv.Columns[header_StdPacking_Yellow].Visible = false;
                dgv.Columns[header_Qty_Yellow].Visible = false;
                dgv.Columns[header_Balance].Visible = false;

                dgv.Columns[header_ActualStock].Visible = false;
                dgv.Columns[header_StockDiff].Visible = false;
                dgv.Columns[header_Remark].Visible = false;

                //dgv.Columns[header_StdPacking_Bag].Visible = false;
                //dgv.Columns[header_StdPacking_Ctn].Visible = false;
                dgv.Columns[header_StdPacking_String].Visible = false;
                //dgv.Columns[header_ActualStock_PCS].Visible = false;
                //dgv.Columns[header_ActualStock_BAG].Visible = false;
                //dgv.Columns[header_ActualStock_CTN].Visible = false;

                dgv.Columns[header_BalAfter].Visible = true;
                dgv.Columns[header_StockMinLvl].Visible = true;
                dgv.Columns[header_Note].Visible = true;
                dgv.Columns[header_TotalAssembly].Visible = true;
                dgv.Columns[header_TotalProduction].Visible = true;
                dgv.Columns[header_DailyUsage].Visible = true;
                dgv.Columns[header_ProDaysNeeded].Visible = true;
                dgv.Columns[header_MonthlyUsage].Visible = true;
                dgv.Columns[header_Est_Stock_Zero_Date].Visible = true;
                dgv.Columns[header_MaxProdPerDay].Visible = true;
                dgv.Columns[header_Start_Prod_By_Date].Visible = true;

                btnCancelStockCheck.Visible = false;

                btnStockCheck.Enabled = true;

                btnStockCheck.Text = "Stock Check";
                lblStockAlert.Text = "Stock Info";

                btnStockRefresh.Visible = true;
                cbProduct.Visible = true;

                cbInPcsUnit.Visible = true;
                cbInBagUnit.Visible = true;

                //change table 
                //LoadStockAlert();
                LoadStockInfo();

            }
        }

        private void LoadProductionPlanning(string itemCode, string itemName)
        {
            frmSBBProductionPlanning frm = new frmSBBProductionPlanning(itemName, itemCode)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.Show();
        }

        private void btnStockCheck_Click(object sender, EventArgs e)
        {
            if(cmbLocation.Text == "ALL")
            {
                MessageBox.Show("Please select valid Stock Location. (cannot be ALL)");
                return;
            }
            if (cbProduct.Checked)
            {
                MessageBox.Show("Cannot make Stock Tally Action to Product!");
                return;
            }

            // Check for unsaved standard packing changes before proceeding
            if (hasStdPackingChanges && changedItemCodes.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    $"You have unsaved standard packing changes for {changedItemCodes.Count} items.\n\n" +
                    "Do you want to save them before proceeding with stock tally?",
                    "Unsaved Standard Packing Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Save changes first
                    SaveStdPackingChanges();
                }
                else if (result == DialogResult.Cancel)
                {
                    // Don't proceed with tally
                    return;
                }
                // If No, continue without saving std packing changes
            }


            if (lblStockAlert.Text == "Stock Info" )
            {
                StockCheckUIMode(true);
                LoadStdPackingData();
            }
            else
            {
                string stockLocation = cmbLocation.Text;

                if (stockLocation != "ALL")
                {
                    frmInOutEdit frm = new frmInOutEdit((DataTable)dgvStockAlert.DataSource, true, true, stockLocation);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit

                    if (frmInOutEdit.TrfSuccess)
                    {
                        StockCheckUIMode(false);
                        LoadStockInfo();
                    }
                }
                else
                {
                    MessageBox.Show("Stock Tally Location Invalid!");
                }

            }
            dgvStockAlert.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private bool hasStdPackingChanges = false;
        private HashSet<string> changedItemCodes = new HashSet<string>();

        private void LoadStdPackingData()
        {
            DataTable dt_StdPacking = dalSBB.StdPackingSelect();

            DataTable dt_StockInfo = (DataTable)dgvStockAlert.DataSource;

            foreach(DataRow row in dt_StockInfo.Rows)
            {
                string itemCode = row[header_ItemCode].ToString();

                foreach(DataRow stdPackingRow in dt_StdPacking.Rows)
                {
                    string itemCode_stdPacking = stdPackingRow[dalSBB.ItemCode].ToString();

                    if(itemCode == itemCode_stdPacking)
                    {
                        int stdQty_White = int.TryParse(stdPackingRow[dalSBB.QtyPerWhiteContainer].ToString(), out int x) ? x : 0;
                        int stdQty_Blue = int.TryParse(stdPackingRow[dalSBB.QtyPerBlueContainer].ToString(), out  x) ? x : 0;
                        int stdQty_Yellow = int.TryParse(stdPackingRow[dalSBB.QtyPerYellowContainer].ToString(), out  x) ? x : 0;

                        row[header_StdPacking_White] = stdPackingRow[dalSBB.QtyPerWhiteContainer];
                        row[header_StdPacking_BLue] = stdPackingRow[dalSBB.QtyPerBlueContainer];
                        row[header_StdPacking_Yellow] = stdPackingRow[dalSBB.QtyPerYellowContainer];

                        string StdPackingRemark = stdQty_White > 0 ? stdQty_White + "/White; " : "";

                        StdPackingRemark += stdQty_Blue > 0 ? stdQty_Blue + "/Blue; " : "";
                        StdPackingRemark += stdQty_Yellow > 0 ? stdQty_Yellow + "/Yellow; " : "";

                        row[header_StdPacking_String] = StdPackingRemark;

                        break;
                    }
                }
            }

        }

        private void UpdateStdPackingString(DataGridView dgv, int rowIndex)
        {
            try
            {
                // Get the std packing values
                int stdQty_White = int.TryParse(dgv.Rows[rowIndex].Cells[header_StdPacking_White].Value?.ToString(), out int x) ? x : 0;
                int stdQty_Blue = int.TryParse(dgv.Rows[rowIndex].Cells[header_StdPacking_BLue].Value?.ToString(), out x) ? x : 0;
                int stdQty_Yellow = int.TryParse(dgv.Rows[rowIndex].Cells[header_StdPacking_Yellow].Value?.ToString(), out x) ? x : 0;

                // Build the std packing string
                string StdPackingRemark = stdQty_White > 0 ? stdQty_White + "/White; " : "";
                StdPackingRemark += stdQty_Blue > 0 ? stdQty_Blue + "/Blue; " : "";
                StdPackingRemark += stdQty_Yellow > 0 ? stdQty_Yellow + "/Yellow; " : "";

                // Update the std packing string column
                dgv.Rows[rowIndex].Cells[header_StdPacking_String].Value = StdPackingRemark;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating std packing string: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            if(Loaded)
            {
                dt_PendingPOSelect = dalSBB.SBBPagePendingPOSelect();//14ms

                string itemCust = text.SPP_BrandName;
                dt_SBBCustSearchWithTypeAndSize = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);//26
                dt_Stock = dalStock.StockDataSelect();//350ms
                dt_Item = dalItem.Select();

                LoadStockInfo();

                btnStockInfoReload.Visible = false;

            }

        }

        private void tableLayoutPanel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancelStockCheck_Click(object sender, EventArgs e)
        {

            if(STOCK_TRANSFER_MODE)
            {
                string message = "Cancel will lose all the unsaved transfer data.\nAre you sure you want to cancel stock transfer?";
    
    if (MessageBox.Show(message, "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
        StockTransferUIMode(false);
        dgvStockAlert.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    }
            }
            else if(STOCK_CHECK_MODE)
            {
                string message = "Cancel will lose all the unsaved changes.\nAre you sure you want to cancel stock check?";

                // Check for unsaved standard packing changes
                if (hasStdPackingChanges && changedItemCodes.Count > 0)
                {
                    DialogResult stdPackingResult = MessageBox.Show(
                        $"You have unsaved standard packing changes for {changedItemCodes.Count} items.\n\n" +
                        "Do you want to save them before canceling?",
                        "Unsaved Standard Packing Changes",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (stdPackingResult == DialogResult.Yes)
                    {
                        // Save standard packing changes
                        SaveStdPackingChanges();

                        // Then ask about canceling stock check
                        if (MessageBox.Show("Standard packing saved. Do you want to cancel stock check?",
                                          "Cancel Stock Check",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            StockCheckUIMode(false);
                            dgvStockAlert.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }
                    else if (stdPackingResult == DialogResult.No)
                    {
                        // Don't save std packing, but ask about canceling with custom message
                        message = $"This will lose standard packing changes for {changedItemCodes.Count} items and all other unsaved changes.\n\nAre you sure you want to cancel stock check?";

                        if (MessageBox.Show(message, "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            ResetStdPackingTracking(); // Clear tracking
                            StockCheckUIMode(false);
                            dgvStockAlert.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        }
                    }
                    // If Cancel, do nothing - stay in stock check mode
                }
                else
                {
                    // No std packing changes, use original logic
                    if (MessageBox.Show(message, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        StockCheckUIMode(false);
                        dgvStockAlert.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    }
                }
            }
            btnStockInfoReload.Visible = false;


        }
        private void HandleStockMinLevelUpdate(DataGridView dgv, int rowIndex, int colIndex)
        {
            string itemCode = dgv.Rows[rowIndex].Cells[header_ItemCode].Value?.ToString() ?? "";
            string itemName = dgv.Rows[rowIndex].Cells[header_ItemName].Value?.ToString() ?? "";
            string newMinLevelStr = dgv.Rows[rowIndex].Cells[colIndex].Value?.ToString() ?? "";
            string oldMinLevel = originalCellValue?.ToString() ?? "";

            decimal newMinLevel = decimal.TryParse(newMinLevelStr, out decimal parsedMinLevel) ? parsedMinLevel : 0m;
            decimal oldMinLevelDecimal = decimal.TryParse(oldMinLevel, out decimal parsedOldLevel) ? parsedOldLevel : 0m;

            if (newMinLevel != oldMinLevelDecimal)
            {
                string message = $"Do you want to save the stock minimum level change?\n\n" +
                                $"Item Code: {itemCode}\n" +
                                $"Item Name: {itemName}\n" +
                                $"Old Min Level: {oldMinLevel}\n" +
                                $"New Min Level: {newMinLevel:F2}";

                DialogResult confirmResult = MessageBox.Show(message, "Confirm Min Level Update",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    bool success = UpdateStockMinLevel(itemCode, newMinLevel);
                    if (success)
                    {
                        MessageBox.Show("Stock minimum level updated successfully!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CheckStockLevels();
                    }
                }
                else
                {
                    dgv.Rows[rowIndex].Cells[colIndex].Value = originalCellValue;
                }
            }
        }

        private void dgvStockAlert_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = dgvStockAlert;
            if (e.RowIndex != -1 && STOCK_CHECK_MODE)
            {
                int col = e.ColumnIndex;
                int row = e.RowIndex;

                // Check if clicked column is one of the editable columns
                if (dgv.Columns[col].Name == header_ActualStock ||
                    dgv.Columns[col].Name == header_StockMinLvl ||
                    dgv.Columns[col].Name == header_StdPacking_White ||
                    dgv.Columns[col].Name == header_StdPacking_BLue ||
                    dgv.Columns[col].Name == header_StdPacking_Yellow ||
                    dgv.Columns[col].Name == header_Qty_White ||
                    dgv.Columns[col].Name == header_Qty_Blue ||
                    dgv.Columns[col].Name == header_Qty_Yellow ||
                    dgv.Columns[col].Name == header_Balance)
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = false;
                }
                else
                {
                    //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = true;
                }
            }
        }
        private object originalCellValue; // Add this as class-level variable
        private void dgvStockAlert_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvStockAlert;
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            string columnName = dgv.Columns[colIndex].Name;

            string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value?.ToString() ?? "";

            //if (string.IsNullOrEmpty(cellValue))
            //{
            //    return;
            //}

            // Handle std packing columns - update string
            if (columnName.Contains(header_StdPacking_White) ||
                columnName.Contains(header_StdPacking_BLue) ||
                columnName.Contains(header_StdPacking_Yellow))
            {
                UpdateStdPackingString(dgv, rowIndex);
                CalculateActualStock(dgv, rowIndex);

                // Track this item for saving
                string itemCode = dgv.Rows[rowIndex].Cells[header_ItemCode].Value?.ToString();
                if (!string.IsNullOrEmpty(itemCode))
                {
                    changedItemCodes.Add(itemCode);
                    hasStdPackingChanges = true;
                    btnSaveStdPacking.Visible = true; // Show save button
                }
            }
            else if (columnName.Contains(header_Qty_White) ||
                columnName.Contains(header_Qty_Blue) ||
                columnName.Contains(header_Qty_Yellow) ||
                columnName.Contains(header_Balance))
            {
                if (STOCK_TRANSFER_MODE)
                {
                    CalculateTransferPcs(dgv, rowIndex);
                }
                else
                {
                    CalculateActualStock(dgv, rowIndex); // Existing stock check logic
                }
            }

            //// Handle quantity and balance columns - calculate actual stock
            //else if (columnName.Contains(header_Qty_White) ||
            //         columnName.Contains(header_Qty_Blue) ||
            //         columnName.Contains(header_Qty_Yellow) ||
            //         columnName.Contains(header_Balance))
            //{
            //    CalculateActualStock(dgv, rowIndex);
            //}   
            // Handle stock min level
            else if (columnName.Contains(header_StockMinLvl))
            {
                HandleStockMinLevelUpdate(dgv, rowIndex, colIndex);
            }
        }

        private void CalculateActualStock(DataGridView dgv, int rowIndex)
        {
            try
            {
                // Get standard packing values (pieces per container)
                int stdPackingWhite = int.TryParse(dgv.Rows[rowIndex].Cells[header_StdPacking_White].Value?.ToString(), out int whiteStd) ? whiteStd : 0;
                int stdPackingBlue = int.TryParse(dgv.Rows[rowIndex].Cells[header_StdPacking_BLue].Value?.ToString(), out int blueStd) ? blueStd : 0;
                int stdPackingYellow = int.TryParse(dgv.Rows[rowIndex].Cells[header_StdPacking_Yellow].Value?.ToString(), out int yellowStd) ? yellowStd : 0;

                // Get quantity values (number of containers)
                int qtyWhite = int.TryParse(dgv.Rows[rowIndex].Cells[header_Qty_White].Value?.ToString(), out int qtyW) ? qtyW : 0;
                int qtyBlue = int.TryParse(dgv.Rows[rowIndex].Cells[header_Qty_Blue].Value?.ToString(), out int qtyB) ? qtyB : 0;
                int qtyYellow = int.TryParse(dgv.Rows[rowIndex].Cells[header_Qty_Yellow].Value?.ToString(), out int qtyY) ? qtyY : 0;

                // Get balance (loose pieces)
                int balance = int.TryParse(dgv.Rows[rowIndex].Cells[header_Balance].Value?.ToString(), out int bal) ? bal : 0;


                //to send warning message, if transfer qty > 0, but std packing = 0
                if (qtyWhite > 0 && stdPackingWhite == 0)
                {
                    MessageBox.Show("White transfer QTY cannot be set because STD. PACKING is 0.\nIt has been cleared.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dgv.Rows[rowIndex].Cells[header_Qty_White].Value = DBNull.Value;
                    qtyWhite = 0;
                }

                if (qtyBlue > 0 && stdPackingBlue == 0)
                {
                    MessageBox.Show("Blue transfer QTY cannot be set because STD. PACKING is 0.\nIt has been cleared.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dgv.Rows[rowIndex].Cells[header_Qty_Blue].Value = DBNull.Value;
                    qtyBlue = 0;
                }

                if (qtyYellow > 0 && stdPackingYellow == 0)
                {
                    MessageBox.Show("Yellow transfer QTY cannot be set because STD. PACKING is 0.\nIt has been cleared.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dgv.Rows[rowIndex].Cells[header_Qty_Yellow].Value = DBNull.Value;
                    qtyYellow = 0;
                }


                int totalActualStock = (qtyWhite * stdPackingWhite) +
                                      (qtyBlue * stdPackingBlue) +
                                      (qtyYellow * stdPackingYellow) +
                                      balance;

                int qtyWhiteCheck = int.TryParse(dgv.Rows[rowIndex].Cells[header_Qty_White].Value?.ToString(), out qtyW) ? qtyW : -1;
                int qtyBlueCheck = int.TryParse(dgv.Rows[rowIndex].Cells[header_Qty_Blue].Value?.ToString(), out qtyB) ? qtyB : -1;
                int qtyYellowCheck = int.TryParse(dgv.Rows[rowIndex].Cells[header_Qty_Yellow].Value?.ToString(), out qtyY) ? qtyY : -1;
                int balanceCheck = int.TryParse(dgv.Rows[rowIndex].Cells[header_Balance].Value?.ToString(), out bal) ? bal : -1;

                if (totalActualStock == 0 && qtyWhiteCheck == -1 && qtyBlueCheck == -1 && qtyYellowCheck == -1 && balanceCheck == -1)
                {
                    dgv.Rows[rowIndex].Cells[header_ActualStock].Value = DBNull.Value;
                    dgv.Rows[rowIndex].Cells[header_StockDiff].Value = DBNull.Value;
                }
                else
                {
                    // Update the Actual Stock column
                    dgv.Rows[rowIndex].Cells[header_ActualStock].Value = totalActualStock;

                    int systemStock = int.TryParse(dgv.Rows[rowIndex].Cells[header_Stock].Value?.ToString(), out int sysStock) ? sysStock : 0;

                    // Now calculate stock difference
                    int stockDiff = totalActualStock - systemStock;

                    dgv.Rows[rowIndex].Cells[header_StockDiff].Value = stockDiff;

                    // Update stock diff color
                    if (stockDiff > 0)
                    {
                        dgv.Rows[rowIndex].Cells[header_StockDiff].Style.ForeColor = Color.Blue;
                        btnStockCheck.Enabled = true;
                    }
                    else if (stockDiff < 0)
                    {
                        dgv.Rows[rowIndex].Cells[header_StockDiff].Style.ForeColor = Color.Red;
                        btnStockCheck.Enabled = true;
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[header_StockDiff].Style.ForeColor = Color.Black;
                        btnStockCheck.Enabled = StockDiffExist();
                    }
                }
                    

               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating actual stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int currentLowStockIndex = -1;
        private List<int> lowStockRowIndices = new List<int>();

        private void CheckStockLevels(Color lowStockColor = default, Color normalColor = default)
        {
            // Set default colors if not provided
            if (lowStockColor == default) lowStockColor = Color.Red;
            if (normalColor == default) normalColor = Color.White;
            int count = 0;
            lowStockRowIndices.Clear();
            currentLowStockIndex = -1;

            foreach (DataGridViewRow row in dgvStockAlert.Rows)
            {
                if (row.IsNewRow) continue;

                try
                {
                    decimal stock = decimal.TryParse(row.Cells[header_Stock].Value?.ToString(), out decimal parsedStock)
                        ? parsedStock : 0m;

                    decimal minStockLevel = decimal.TryParse(row.Cells[header_StockMinLvl].Value?.ToString(), out decimal parsedMinLevel)
                        ? parsedMinLevel : 0m;

                    if (stock <= minStockLevel && minStockLevel > 0) // Only check if min level is set
                    {
                        row.Cells[header_Stock].Style.BackColor = lowStockColor;
                        row.Cells[header_Stock].Style.ForeColor = Color.White;

                        string indexValue = row.Cells[header_Index].Value?.ToString();

                        lowStockRowIndices.Add(int.Parse(indexValue));
                        //lowStockRowIndices.Add(indexValue);
                        count++;
                    }
                    else
                    {
                        row.Cells[header_Stock].Style.BackColor = normalColor;
                        row.Cells[header_Stock].Style.ForeColor = Color.Black;
                    }
                }
                catch
                {
                    // Reset to normal on error
                    row.Cells[header_Stock].Style.BackColor = normalColor;
                    row.Cells[header_Stock].Style.ForeColor = Color.Black;
                }
            }

            string message = "";

            if (count > 0)
            {
                message = $"{count} items below min level - Restock needed";
            }

            lblShortMinStockLevelAlert.Text = message;

        }
        private bool UpdateStockMinLevel(string itemCode, decimal newMinLevel)
        {
            bool result = false;
            try
            {
                // Assuming you have a similar structure for stock updates
                uItem.item_code = itemCode;
                uItem.item_min_stock_level = newMinLevel; // decimal value
                uItem.item_updtd_date = DateTime.Now;
                uItem.item_updtd_by = MainDashboard.USER_ID;

                // Call appropriate DAL method for stock update
                result = dalItem.ItemMinStockLevelUpdate(uItem); // You'll need this method

                if (!result)
                {
                    MessageBox.Show("Failed to update stock minimum level");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating stock min level: {ex.Message}");
            }

            return result;
        }

        private bool StockDiffExist()
        {
            if (STOCK_CHECK_MODE)
            {
                DataTable dt = (DataTable)dgvStockAlert.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    string stockDiff = row[header_StockDiff].ToString();

                    if (!string.IsNullOrEmpty(stockDiff) && stockDiff != "0")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;


        }


        private void dgvStockAlert_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {

                DataGridView dgv = dgvStockAlert;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                if (dgv.Columns[colIndex].Name.Contains(header_ActualStock) && STOCK_CHECK_MODE) //Desired Column
                {
                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else if (dgv.Columns[colIndex].Name.Contains(header_StockMinLvl)) //Desired Column
                {
                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else if ((dgv.Columns[colIndex].Name.Contains(header_StdPacking_White) ||
                 dgv.Columns[colIndex].Name.Contains(header_StdPacking_BLue) ||
                 dgv.Columns[colIndex].Name.Contains(header_StdPacking_Yellow) ||
                 dgv.Columns[colIndex].Name.Contains(header_Qty_White) ||
                 dgv.Columns[colIndex].Name.Contains(header_Qty_Blue) ||
                 dgv.Columns[colIndex].Name.Contains(header_Qty_Yellow) ||
                 dgv.Columns[colIndex].Name.Contains(header_Balance)) && (STOCK_CHECK_MODE || STOCK_TRANSFER_MODE)) 
                {
                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {

                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void tableLayoutPanel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            NewInitialDBData();
            LoadPendingSummary();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            NewInitialDBData();
            LoadPendingSummary();
        }

        private void dgvPartsAlert_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            ////DataGridView dgv = dgvUsage;

            //dgv.SuspendLayout();
            //int row = e.RowIndex;
            //int col = e.ColumnIndex;

            //if (dgv.Columns[col].Name == header_BalAfter)
            //{
            //    if (dgv.Rows[row].Cells[col].Value != null)
            //    {
            //        if (!string.IsNullOrEmpty(dgv.Rows[row].Cells[col].Value.ToString()))
            //        {
            //            float num = dgv.Rows[row].Cells[col].Value == DBNull.Value ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[col].Value.ToString());

            //            if (num < 0)
            //            {
            //                dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
            //            }
            //            else
            //            {
            //                dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
            //            }
            //        }
            //    }
            //}

            //dgv.ResumeLayout();
        }

        private void dgvStockAlert_Sorted(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)dgvStockAlert.DataSource;

            //dt = RearrangeIndex(dt);

            //dgvStockAlert.DataSource = dt;

            dgvStockAlert.ClearSelection();
        }

        private int TOTAL_DAYS_IN_PERIOD = 0;
        private int TOTAL_SUNDAYS_IN_PERIOD = 0;

        private void CalculatePeriodDetails()
        {
            DateTime startDate = dtpDate1.Value.Date;
            DateTime endDate = dtpDate2.Value.Date;

            // Calculate total days (inclusive)
            TOTAL_DAYS_IN_PERIOD = (int)(endDate - startDate).TotalDays + 1;

            // Count Sundays in the period
            TOTAL_SUNDAYS_IN_PERIOD = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    TOTAL_SUNDAYS_IN_PERIOD++;
                }
            }

            // Calculate working days based on Sunday checkbox
            int workingDays = cbSundayisWorkday.Checked ? TOTAL_DAYS_IN_PERIOD : (TOTAL_DAYS_IN_PERIOD - TOTAL_SUNDAYS_IN_PERIOD);

            // Update label with short message
            string sundayText = cbSundayisWorkday.Checked ? "inc" : "exc";
            lblPeriodInfo.Text = $"{workingDays} days ({sundayText} {TOTAL_SUNDAYS_IN_PERIOD} SUN)";
        }

        private void dtpDate1_ValueChanged(object sender, EventArgs e)
        {
            if (Loaded)
            {

                //LoadUsage();
                CalculatePeriodDetails();
                btnStockInfoReload.Visible = true;
            }
        }

        private void dtpDate2_ValueChanged(object sender, EventArgs e)
        {
            if (Loaded)
            {
                CalculatePeriodDetails();
                //LoadUsage();
                btnStockInfoReload.Visible = true;

            }

        }

        private void frmSBB_Shown(object sender, EventArgs e)
        {


            dgvStockAlert.ClearSelection();
            //dgvUsage.ClearSelection();

            CheckStockLevels();
        }

        private void cbParts_CheckedChanged(object sender, EventArgs e)
        {
            if (Loaded)
                LoadUsage();
        }

        private void cbMat_CheckedChanged(object sender, EventArgs e)
        {
            if (Loaded)
                LoadUsage();
        }

        private void cbPackaging_CheckedChanged(object sender, EventArgs e)
        {
            if (Loaded)
                LoadUsage();
        }

        private void dgvStockAlert_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (lblStockAlert.Text == "Stock Alert")
            //{
            //    DataGridView dgv = dgvStockAlert;

            //    int rowIndex = dgv.CurrentCell.RowIndex;

            //    if (rowIndex >= 0)
            //    {
            //        try
            //        {
            //            string itemCode = dgv.Rows[rowIndex].Cells[header_ItemCode].Value.ToString();
            //            string itemName = dgv.Rows[rowIndex].Cells[header_ItemName].Value.ToString();

            //            if (tool.getItemCat(itemCode) == text.Cat_Part)
            //            {
            //                LoadProductionPlanning(itemCode, itemName);

            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            tool.saveToTextAndMessageToUser(ex);
            //        }
            //    }
            //}

        }

        private void lblMonthlyDelivered_Click(object sender, EventArgs e)
        {
            if (!DeliveredBags_FullScreen)
            {
                int count = Application.OpenForms.OfType<frmSBBDeliveredReport>().Count();

                if (count == 1)
                {
                    //Form is already open
                    Application.OpenForms.OfType<frmSBBDeliveredReport>().First().BringToFront();
                    Application.OpenForms.OfType<frmSBBDeliveredReport>().First().WindowState = FormWindowState.Normal;
                }
                else
                {
                    // Form is not open
                    frmSBBDeliveredReport frm = new frmSBBDeliveredReport
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };

                    //frm.WindowState = FormWindowState.Maximized;


                    frm.Show();

                    MainDashboard.SBBDeliveredFormOpen = true;
                }

            }

        }

        private void cmbDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loaded)
            {
                RefreshPage();
            }
        }

        private void MonthlyDeliveredBagFullScreen()
        {
            //if(DeliveredBags_FullScreen)
            //{
            //    btnDeliveredFullScreen.BackgroundImage = Properties.Resources.icons8_exit_full_screen_64;

            //    tlpMonthlyDeliveredBag.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
            //    tlpMonthlyDeliveredBag.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);

            //    tlpDashBoard.RowStyles[0] = new RowStyle(SizeType.Percent, 0f);
            //    tlpDashBoard.RowStyles[1] = new RowStyle(SizeType.Percent, 100f);
            //    tlpDashBoard.RowStyles[2] = new RowStyle(SizeType.Percent, 0f);


            //    tlpDeliveredQtyAndDetail.RowStyles[0] = new RowStyle(SizeType.Percent, 100f);
            //    tlpDeliveredQtyAndDetail.RowStyles[1] = new RowStyle(SizeType.Absolute, 0);

            //    DeliveredBag_FontSize = 250;

            //    lblMonthlyDelivered.Font = new Font("Segoe UI", DeliveredBag_FontSize, FontStyle.Regular);

            //    tlpSummary.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
            //    tlpSummary.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f);
            //    tlpSummary.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 100f);

            //    tlpSBB.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
            //    tlpSBB.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f);

            //    //btnFullScreenRefresh.Visible = true;
            //}
            //else
            //{
            //    btnDeliveredFullScreen.BackgroundImage = Properties.Resources.icons8_toggle_full_screen_50;

            //    tlpMonthlyDeliveredBag.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 300f);
            //    tlpMonthlyDeliveredBag.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);

            //    tlpDashBoard.RowStyles[0] = new RowStyle(SizeType.Absolute, 30f);
            //    tlpDashBoard.RowStyles[1] = new RowStyle(SizeType.Absolute, 270f);
            //    tlpDashBoard.RowStyles[2] = new RowStyle(SizeType.Percent, 100f);

            //    tlpDeliveredQtyAndDetail.RowStyles[0] = new RowStyle(SizeType.Absolute, 163f);
            //    tlpDeliveredQtyAndDetail.RowStyles[1] = new RowStyle(SizeType.Percent, 100f);

            //    DeliveredBag_FontSize = 60;
            //    lblMonthlyDelivered.Font = new Font("Segoe UI", DeliveredBag_FontSize, FontStyle.Regular);

            //    tlpSummary.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 230f);
            //    tlpSummary.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 230f);
            //    tlpSummary.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 100f);

            //    tlpSBB.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
            //    tlpSBB.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 372f);

            //    //btnFullScreenRefresh.Visible = false;

            //}
        }

        private void btnDeliveredFullScreen_Click(object sender, EventArgs e)
        {
            //if(DeliveredBags_FullScreen)
            //{
            //    DeliveredBags_FullScreen = false;


            //}
            //else
            //{
            //    DeliveredBags_FullScreen = true;

            //}

            //MonthlyDeliveredBagFullScreen();
        }

        private void btnFullScreenRefresh_Click(object sender, EventArgs e)
        {
            NewInitialDBData();
            LoadPendingSummary();
        }

        private void label12_Click(object sender, EventArgs e)
        {


            frmChangeDate frm = new frmChangeDate(MonthlyDateStart, MonthlyDateEnd);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frmChangeDate.dateChanged)
            {
                frmLoading.ShowLoadingScreen();
                InitialMonthlyDate();
                NewInitialDBData();
                LoadPendingSummary();
                frmLoading.CloseForm();

            }

        }

        private void label13_Click(object sender, EventArgs e)
        {
            if (DeliveredBags_FullScreen)
            {
                DeliveredBag_FontSize += 20;

            }
            else
            {
                DeliveredBag_FontSize += 5;

            }

            lblMonthlyDelivered.Font = new Font("Segoe UI", DeliveredBag_FontSize, FontStyle.Regular);
        }

        private void label19_Click(object sender, EventArgs e)
        {
            if (DeliveredBags_FullScreen)
            {
                DeliveredBag_FontSize -= 20;


            }
            else
            {
                DeliveredBag_FontSize -= 5;


            }
            lblMonthlyDelivered.Font = new Font("Segoe UI", DeliveredBag_FontSize, FontStyle.Regular);

        }

        private void cmbStockLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Loaded)
            {
                string stockLocation = "ALL";

                if (stockLocation.Equals("ALL"))
                {
                    btnStockCheck.Enabled = false;
                }
                else
                {
                    btnStockCheck.Enabled = true;
                }


                header_Stock = "STOCK ";


                header_Stock += stockLocation + " (PCS / KG)";

                LoadStockInfo();//166
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmSPPCustomerEdit frm = new frmSPPCustomerEdit
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void tableLayoutPanel17_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool STOCK_ALERT_DATA_LOADED = false;

        private void ApplyItemTypeFilter()
        {
            if (STOCK_ALERT_DATA_LOADED)
            {
                DataTable dt_FillAll = FilterItemType();

                if (dt_FillAll != null)
                {
                    DataView dv = dt_FillAll.DefaultView;
                    //dv.Sort = header_ItemCategory + " ASC," + header_ItemType + " ASC," + header_ItemName + " ASC";
                    dv.Sort = header_SBB_Category + " ASC," + header_SBB_Type + " ASC," + header_Size_1 + " ASC," + header_Size_2 + " ASC," + header_ItemName + " ASC";

                    dt_FillAll = dv.ToTable();

                    RearrangeIndex(dt_FillAll);
                    dgvStockAlert.DataSource = dt_FillAll;

                    DgvUIEdit(dgvStockAlert);
                    CheckStockLevels();

                    dgvStockAlert.ClearSelection();
                }

            }

        }
        private void cbPart_CheckedChanged(object sender, EventArgs e)
        {
            if(!STOP_STOCK_INFO_LOADING && Loaded)
            {
                STOP_STOCK_INFO_LOADING = true;

                if(cbChildPart.Checked)
                {
                    STOCK_INFO_ITEM_TYPE = Type_Part;

                    if(cbProduct.Checked)
                    {
                        cbProduct.Checked = false;
                        LoadStockInfo();
                    }
                    else
                    {
                        ApplyItemTypeFilter();

                    }
                }
                else
                {
                    if(!cbOtherMaterial.Checked && !cbProduct.Checked)
                    {
                        cbChildPart.Checked = true;

                        MessageBox.Show("Need to select at least One(1) Item Type!");
                    }
                    else
                    {
                        ApplyItemTypeFilter();
                    }

                }


                STOP_STOCK_INFO_LOADING = false;
            }
        }

        private void cbMats_CheckedChanged(object sender, EventArgs e)
        {
            if (!STOP_STOCK_INFO_LOADING && Loaded)
            {
                STOP_STOCK_INFO_LOADING = true;

                if (cbOtherMaterial.Checked)
                {
                    STOCK_INFO_ITEM_TYPE = Type_Part;

                    if (cbProduct.Checked)
                    {
                        cbProduct.Checked = false;
                        LoadStockInfo();
                    }
                    else
                    {
                        ApplyItemTypeFilter();
                    }
                }
                else
                {
                    if (!cbChildPart.Checked && !cbProduct.Checked)
                    {
                        cbOtherMaterial.Checked = true;

                        MessageBox.Show("Need to select at least One(1) Item Type!");
                    }
                }
                {
                    ApplyItemTypeFilter();
                }

                

                STOP_STOCK_INFO_LOADING = false;
            }

        }


        private void dgvStockAlert_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Get item code from clicked row
                string itemCode = dgvStockAlert.Rows[e.RowIndex].Cells[header_ItemCode].Value?.ToString();

                if (!string.IsNullOrEmpty(itemCode))
                {
                    ShowMachineHistory(itemCode); 
                }
            }

            else if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                

                DataGridView dgv = dgvStockAlert;
                // Get the column indices of all editable columns
                int stockMinLvlColumnIndex = dgv.Columns[header_StockMinLvl].Index;
                int stockActualStockColumnIndex = dgv.Columns[header_ActualStock].Index;
                int whitestdpackingIndex = dgv.Columns[header_StdPacking_White].Index;
                int bluestdpackingIndex = dgv.Columns[header_StdPacking_BLue].Index;
                int yellowstdpackingIndex = dgv.Columns[header_StdPacking_Yellow].Index;
                int qtyWhiteIndex = dgv.Columns[header_Qty_White].Index;
                int qtyBlueIndex = dgv.Columns[header_Qty_Blue].Index;
                int qtyYellowIndex = dgv.Columns[header_Qty_Yellow].Index;
                int balanceIndex = dgv.Columns[header_Balance].Index;

                if (e.ColumnIndex == stockMinLvlColumnIndex ||
                    e.ColumnIndex == stockActualStockColumnIndex ||
                    e.ColumnIndex == whitestdpackingIndex ||
                    e.ColumnIndex == bluestdpackingIndex ||
                    e.ColumnIndex == yellowstdpackingIndex ||
                    e.ColumnIndex == qtyWhiteIndex ||
                    e.ColumnIndex == qtyBlueIndex ||
                    e.ColumnIndex == qtyYellowIndex ||
                    e.ColumnIndex == balanceIndex)
                {
                    // Switch to cell selection mode and enable editing
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.ReadOnly = false;
                    dgv.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                    // Make only this column editable
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        dgv.Columns[i].ReadOnly = (i != e.ColumnIndex);
                    }
                    // Optional: Start editing immediately
                    dgv.CurrentCell = dgv[e.ColumnIndex, e.RowIndex];
                    dgv.BeginEdit(true);
                }
                else
                {
                    // Switch to full row selection and make readonly
                    //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.ReadOnly = true;
                    dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
                    // End any current editing
                    dgv.EndEdit();
                }
            }
        }

        private void dgvStockAlert_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = dgvStockAlert;
            string columnName = dgv.Columns[e.ColumnIndex].Name;

            // List of non-editable columns
            bool isReadOnlyColumn = columnName == header_Index ||
                                   columnName == header_ItemCode ||
                                   columnName == header_ItemName ||
                                   columnName == header_Stock ||
                                   columnName == header_StdPacking_String ||
                                  columnName == header_ActualStock ||
                                   columnName == header_StockDiff;

            if (isReadOnlyColumn)
            {
                e.Cancel = true; // Prevent editing
            }
            else
            {
                // Store original value for comparison
                originalCellValue = dgv[e.ColumnIndex, e.RowIndex].Value;
            }
        }

        private void lblShortMinStockLevelAlert_Click(object sender, EventArgs e)
        {
            if (lowStockRowIndices.Count == 0)
                return;

            // Move to next low stock row
            currentLowStockIndex++;

            // Loop back to first if we've reached the end
            if (currentLowStockIndex >= lowStockRowIndices.Count)
            {
                currentLowStockIndex = 0;
            }

            // Get the row index to navigate to
            int targetRowIndex = lowStockRowIndices[currentLowStockIndex];

            foreach (DataGridViewRow row in dgvStockAlert.Rows)
            {
                if (row.Cells[header_Index].Value?.ToString() == targetRowIndex.ToString())
                {
                    targetRowIndex = row.Index;
                    break;
                }
            }

            // Navigate to the row
            dgvStockAlert.ClearSelection();
            dgvStockAlert.Rows[targetRowIndex].Selected = true;
            dgvStockAlert.CurrentCell = dgvStockAlert.Rows[targetRowIndex].Cells[header_ItemName]; // Focus on item name column

            // Scroll to make sure the row is visible
            dgvStockAlert.FirstDisplayedScrollingRowIndex = targetRowIndex;

            // Update label to show current position
            lblShortMinStockLevelAlert.Text = $"⚠️ {lowStockRowIndices.Count} items below minimum level ({currentLowStockIndex + 1}/{lowStockRowIndices.Count})";
        }


        private bool STOCK_ALERT_IS_EXPANDED = false; // Class-level variable to track state

        private void ToggleUIExpansion()
        {
            if (!STOCK_ALERT_IS_EXPANDED)
            {
                // EXPAND MODE - Hide rows 1 and 2, show only row 3
                tlpDashBoard.RowStyles[0].SizeType = SizeType.Absolute;
                tlpDashBoard.RowStyles[0].Height = 0; // Hide row 1 (original size 250)

                tlpDashBoard.RowStyles[1].SizeType = SizeType.Absolute;
                tlpDashBoard.RowStyles[1].Height = 0; // Hide row 2 (original size 10)

                // Modify tlpStockReport columns
                //tlpStockReport.ColumnStyles[0].SizeType = SizeType.Percent;
                //tlpStockReport.ColumnStyles[0].Width = 100; // Change from 880 to 100%

                //tlpStockReport.ColumnStyles[1].SizeType = SizeType.Absolute;
                //tlpStockReport.ColumnStyles[1].Width = 0; // Hide col 2 (original 10 absolute)

                //tlpStockReport.ColumnStyles[2].SizeType = SizeType.Percent;
                //tlpStockReport.ColumnStyles[2].Width = 0; // Hide col 3 (original 100%)

                STOCK_ALERT_IS_EXPANDED = true;

                // Change button image to exit full screen
                btnUIExpand.BackgroundImage = Properties.Resources.icons8_exit_full_screen_64;

                // Increase font size for NAME column in Stock Alert datagridview
               // dgvStockAlert.Columns[header_ItemName].DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
                //dgvStockAlert.Columns[header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            else
            {
                // COLLAPSE MODE - Restore original layout
                tlpDashBoard.RowStyles[0].SizeType = SizeType.Absolute;
                tlpDashBoard.RowStyles[0].Height = 250; // Restore row 1 original size

                tlpDashBoard.RowStyles[1].SizeType = SizeType.Absolute;
                tlpDashBoard.RowStyles[1].Height = 10; // Restore row 2 original size

                // Restore tlpStockReport columns
                //tlpStockReport.ColumnStyles[0].SizeType = SizeType.Absolute;
                //tlpStockReport.ColumnStyles[0].Width = 880; // Restore col 1 original size

                //tlpStockReport.ColumnStyles[1].SizeType = SizeType.Absolute;
                //tlpStockReport.ColumnStyles[1].Width = 10; // Restore col 2 original size

                //tlpStockReport.ColumnStyles[2].SizeType = SizeType.Percent;
                //tlpStockReport.ColumnStyles[2].Width = 100; // Restore col 3 original size (100%)

                STOCK_ALERT_IS_EXPANDED = false;

                // Change button image to toggle full screen
                btnUIExpand.BackgroundImage = Properties.Resources.icons8_toggle_full_screen_50;

                // Restore original font size for NAME column in Stock Alert datagridview
                //dgvStockAlert.Columns[header_ItemName].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                //dgvStockAlert.Columns[header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            }

            // Refresh the layouts to apply changes
            tlpDashBoard.PerformLayout();
            tlpStockReport.PerformLayout();
        }

        private void btnUIExpand_Click(object sender, EventArgs e)
        {
            ToggleUIExpansion();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            SortByType();
        }

        private void txtItemSearch_Enter(object sender, EventArgs e)
        {
            if (txtItemSearch.Text == text.Search_DefaultText)
            {
                txtItemSearch.Text = "";
                txtItemSearch.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtItemSearch_Leave(object sender, EventArgs e)
        {
            if (txtItemSearch.Text.Length == 0)
            {
                txtItemSearch.Text = text.Search_DefaultText;
                txtItemSearch.ForeColor = SystemColors.GrayText;

                ItemSearchUIReset();

            }
        }

        private void txtItemSearch_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }




        private List<int> Row_Index_Found;

        int CURRENT_ROW_JUMP = -1;

        private void ItemSearchUIReset()
        {
            CURRENT_ROW_JUMP = -1;
            lblSearchInfo.Text = "";
            Row_Index_Found = new List<int>();

            lblResultNo.Text = "";
            //lblSearchClear.Visible = false;
            btnPreviousSearchResult.Enabled = false;
            btnNextSearchResult.Enabled = false;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            if (txtItemSearch.Text.Length == 0 || txtItemSearch.Text == text.Search_DefaultText)
            {
                lblSearchClear.Visible = false;
                ItemSearchUIReset();
            }
            else
            {
                lblSearchClear.Visible = true;
                ItemSearch();
            }

            //dgvForecastReport.DataSource = null;

            Cursor = Cursors.Arrow; // change cursor to normal type




        }

        private void ItemSearch()
        {
            ItemSearchUIReset();
            string searchText = txtItemSearch.Text;

            if (searchText != "SEARCH" && !string.IsNullOrWhiteSpace(searchText))
            {
                DataGridView dgv = dgvStockAlert;

                if (dgv.DataSource != null)
                {
                    // Split search into words
                    string[] searchWords = searchText.ToUpper().Split(' ');

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string itemCode = row.Cells[header_ItemCode].Value?.ToString().ToUpper() ?? "";
                        string itemName = row.Cells[header_ItemName].Value?.ToString().ToUpper() ?? "";

                        // Remove apostrophes and combine text
                        string combinedText = (itemCode + " " + itemName).Replace("'", "");

                        // Check if all search words exist
                        bool allWordsFound = true;
                        foreach (string word in searchWords)
                        {
                            if (!combinedText.Contains(word))
                            {
                                allWordsFound = false;
                                break;
                            }
                        }

                        if (allWordsFound)
                        {
                            // Save the Index column value instead of row index
                            string indexValue = row.Cells[header_Index].Value?.ToString();
                            if (!string.IsNullOrEmpty(indexValue))
                            {
                                Row_Index_Found.Add(int.Parse(indexValue));
                            }
                        }
                    }

                    int itemFoundCount = Row_Index_Found.Count;
                    lblSearchInfo.Text = itemFoundCount + " items found";

                    if (itemFoundCount > 0)
                    {
                        JumpToNextRow();
                        btnPreviousSearchResult.Enabled = false;
                        btnNextSearchResult.Enabled = itemFoundCount > 1;
                    }
                }
            }
        }

        private void btnNextSearchResult_Click(object sender, EventArgs e)
        {
            JumpToNextRow();

        }

        private void btnPreviousSearchResult_Click(object sender, EventArgs e)
        {
            BackToPreviousRow();
        }


        private void lblSearchClear_Click(object sender, EventArgs e)
        {
            txtItemSearch.Text = text.Search_DefaultText;
            txtItemSearch.ForeColor = SystemColors.GrayText;
            ItemSearchUIReset();

            lblSearchClear.Visible = false;
        }

        private void JumpToNextRow()
        {
            if (Row_Index_Found == null || Row_Index_Found.Count == 0)
                return;

            // Find next index value greater than current
            int nextIndexValue = -1;
            int currentPosition = -1;

            if (CURRENT_ROW_JUMP == -1)
            {
                nextIndexValue = Row_Index_Found[0];
                currentPosition = 0;
            }
            else
            {
                for (int i = 0; i < Row_Index_Found.Count; i++)
                {


                    if (Row_Index_Found[i] == CURRENT_ROW_JUMP)
                    {
                        if (i == Row_Index_Found.Count - 1)
                        {
                            currentPosition = 0;
                            nextIndexValue = Row_Index_Found[currentPosition];
                        }
                        else
                        {
                            currentPosition = i + 1;
                            nextIndexValue = Row_Index_Found[currentPosition];
                        }
                        break;
                    }
                }
            }
               

            // If no next found, we're at the end
            if (nextIndexValue == -1) return;

            // Update current position
            CURRENT_ROW_JUMP = nextIndexValue;

            // Find the actual row with this index value
            DataGridViewRow targetRow = null;
            foreach (DataGridViewRow row in dgvStockAlert.Rows)
            {
                if (row.Cells[header_Index].Value?.ToString() == nextIndexValue.ToString())
                {
                    targetRow = row;
                    break;
                }
            }

            if (targetRow != null)
            {
                // Clear selection and select new row
                dgvStockAlert.ClearSelection();
                targetRow.Selected = true;
                dgvStockAlert.FirstDisplayedScrollingRowIndex = targetRow.Index;

                // Update buttons
                btnPreviousSearchResult.Enabled = true;
                btnNextSearchResult.Enabled = (currentPosition < Row_Index_Found.Count - 1);

                // Show position
                lblResultNo.Text = $"#{currentPosition + 1} of {Row_Index_Found.Count}";
            }
        }

        private void BackToPreviousRow()
        {
            if (Row_Index_Found == null || Row_Index_Found.Count == 0)
                return;

            // Find previous index value less than current
            int prevIndexValue = -1;
            int currentPosition = -1;
            
            for (int i = Row_Index_Found.Count - 1; i >= 0; i--)
            {
                //if (Row_Index_Found[i] < CURRENT_ROW_JUMP)
                //{
                //    prevIndexValue = Row_Index_Found[i];
                //    currentPosition = i;
                //    break;
                //}

                if (Row_Index_Found[i] == CURRENT_ROW_JUMP)
                {
                    if (i == 0)
                    {
                        currentPosition = Row_Index_Found.Count - 1;
                        prevIndexValue = Row_Index_Found[currentPosition];
                    }
                    else
                    {
                        currentPosition = i - 1;
                        prevIndexValue = Row_Index_Found[currentPosition];
                    }
                    break;
                }

            }

            // If no previous found, we're at the beginning
            if (prevIndexValue == -1) return;

            // Update current position
            CURRENT_ROW_JUMP = prevIndexValue;

            // Find the actual row with this index value
            DataGridViewRow targetRow = null;
            foreach (DataGridViewRow row in dgvStockAlert.Rows)
            {
                if (row.Cells[header_Index].Value?.ToString() == prevIndexValue.ToString())
                {
                    targetRow = row;
                    break;
                }
            }

            if (targetRow != null)
            {
                // Clear selection and select new row
                dgvStockAlert.ClearSelection();
                targetRow.Selected = true;
                dgvStockAlert.FirstDisplayedScrollingRowIndex = targetRow.Index;

                // Update buttons
                btnPreviousSearchResult.Enabled = (currentPosition > 0);
                btnNextSearchResult.Enabled = true;

                // Show position
                lblResultNo.Text = $"#{currentPosition + 1} of {Row_Index_Found.Count}";
            }
        }


        private bool STOP_STOCK_INFO_LOADING = false;

        private void StockInfoItemTypeUpdate()
        {
            if(Loaded && !STOP_STOCK_INFO_LOADING)
            {
                if (cbProduct.Checked)
                {
                    STOCK_INFO_ITEM_TYPE = Type_Product;

                    cbInBagUnit.Visible = true;
                    cbInPcsUnit.Enabled = true;

                    btnStockCheck.Enabled = false;

                    cbChildPart.Checked = false;
                    cbOtherMaterial.Checked = false;
                }
                else

                {
                    STOCK_INFO_ITEM_TYPE = Type_Part;

                    cbInPcsUnit.Checked = true;
                    cbInBagUnit.Checked = false;
                    cbInPcsUnit.Enabled = false;
                    cbInBagUnit.Visible = false;

                    btnStockCheck.Enabled = true;

                    cbChildPart.Checked = true;
                    cbOtherMaterial.Checked = true;

                }

                dgvStockAlert.DataSource = null;
                LoadStockInfo();
            }
           
        }

        private void cbProduct_CheckedChanged(object sender, EventArgs e)
        {
            if(!STOP_STOCK_INFO_LOADING)
            {
                STOP_STOCK_INFO_LOADING = true;

                cbChildPart.Checked = !cbProduct.Checked;
                cbOtherMaterial.Checked = !cbProduct.Checked;

                STOP_STOCK_INFO_LOADING = false;

                StockInfoItemTypeUpdate();
            }
        }

        private bool FILTER_HIDE = true;
        private void HideFilter(bool hide)
        {
           

            if (hide)
            {
                btnFilter.Text = "Show Filter";
                tlpStockInfo.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
                FILTER_HIDE = true;
            }
            else
            {
                btnFilter.Text = "Hide Filter";
                tlpStockInfo.RowStyles[2] = new RowStyle(SizeType.Absolute, 170f);
                FILTER_HIDE = false;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (FILTER_HIDE)
            {
                FILTER_HIDE = false;
            }
            else
            {
                FILTER_HIDE = true;
            }

            HideFilter(FILTER_HIDE);
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Loaded)
            {
                LoadStockInfo();
            }
        }

        private void lblSetRemarkToAll_Click(object sender, EventArgs e)
        {
            string remark = txtRemark.Text;

            foreach (DataGridViewRow row in dgvStockAlert.Rows)
            {
                if (!row.IsNewRow)
                {
                    // Get the Stock Diff value
                    var stockDiffValue = row.Cells[header_StockDiff].Value;

                    if (stockDiffValue != null &&
                        !string.IsNullOrEmpty(stockDiffValue.ToString()) &&
                        stockDiffValue.ToString() != "0")
                    {
                        // Check if the value is greater than 0
                        if (int.TryParse(stockDiffValue.ToString(), out int stockDiff))
                        {
                            // Set the remark to the Remark column
                            row.Cells["Remark"].Value = remark;
                        }
                    }
                }
            }
        }

        private void cbEditStdPacking_CheckedChanged(object sender, EventArgs e)
        {
            dgvStockAlert.Columns[header_StdPacking_White].Visible = cbEditStdPacking.Checked;
            dgvStockAlert.Columns[header_StdPacking_BLue].Visible = cbEditStdPacking.Checked;
            dgvStockAlert.Columns[header_StdPacking_Yellow].Visible = cbEditStdPacking.Checked;
        }

        private void dgvStockAlert_SelectionChanged(object sender, EventArgs e)
        {
            if (STOCK_CHECK_MODE)
            {
                DataGridView dgv = dgvStockAlert;

                if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex >= 0 && dgv.CurrentCell.ColumnIndex >= 0)
                {
                    int colIndex = dgv.CurrentCell.ColumnIndex;

                    // Get the column indices of all editable columns
                    int stockMinLvlColumnIndex = dgv.Columns[header_StockMinLvl].Index;
                    int stockActualStockColumnIndex = dgv.Columns[header_ActualStock].Index;
                    int whitestdpackingIndex = dgv.Columns[header_StdPacking_White].Index;
                    int bluestdpackingIndex = dgv.Columns[header_StdPacking_BLue].Index;
                    int yellowstdpackingIndex = dgv.Columns[header_StdPacking_Yellow].Index;
                    int qtyWhiteIndex = dgv.Columns[header_Qty_White].Index;
                    int qtyBlueIndex = dgv.Columns[header_Qty_Blue].Index;
                    int qtyYellowIndex = dgv.Columns[header_Qty_Yellow].Index;
                    int balanceIndex = dgv.Columns[header_Balance].Index;

                    // Check if current cell is in an editable column
                    if (colIndex == stockMinLvlColumnIndex ||
                        colIndex == stockActualStockColumnIndex ||
                        colIndex == whitestdpackingIndex ||
                        colIndex == bluestdpackingIndex ||
                        colIndex == yellowstdpackingIndex ||
                        colIndex == qtyWhiteIndex ||
                        colIndex == qtyBlueIndex ||
                        colIndex == qtyYellowIndex ||
                        colIndex == balanceIndex)
                    {
                        // Enable editing for editable columns
                        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dgv.ReadOnly = false;
                        dgv.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

                        // Make only the current column editable
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            dgv.Columns[i].ReadOnly = (i != colIndex);
                        }
                    }
                    else
                    {
                        // Disable editing for non-editable columns
                        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgv.ReadOnly = true;
                        dgv.EditMode = DataGridViewEditMode.EditProgrammatically;

                        // Make all columns read-only
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            dgv.Columns[i].ReadOnly = true;
                        }

                        // End any current editing
                        dgv.EndEdit();
                    }
                }
            }
            else if (STOCK_TRANSFER_MODE)
            {
                DataGridView dgv = dgvStockAlert;

                if (dgv.CurrentCell != null && dgv.CurrentCell.RowIndex >= 0 && dgv.CurrentCell.ColumnIndex >= 0)
                {
                    int colIndex = dgv.CurrentCell.ColumnIndex;

                    // Get the column indices of all editable columns
                   // int stockMinLvlColumnIndex = dgv.Columns[header_StockMinLvl].Index;
                   // int stockActualStockColumnIndex = dgv.Columns[header_ActualStock].Index;
                    //int whitestdpackingIndex = dgv.Columns[header_StdPacking_White].Index;
                   // int bluestdpackingIndex = dgv.Columns[header_StdPacking_BLue].Index;
                   // int yellowstdpackingIndex = dgv.Columns[header_StdPacking_Yellow].Index;
                    int qtyWhiteIndex = dgv.Columns[header_Qty_White].Index;
                    int qtyBlueIndex = dgv.Columns[header_Qty_Blue].Index;
                    int qtyYellowIndex = dgv.Columns[header_Qty_Yellow].Index;
                    int balanceIndex = dgv.Columns[header_Balance].Index;
                    int stdPackingBlueIndex = dgv.Columns[header_StdPacking_BLue].Index;
                    int stdPackingWhiteIndex = dgv.Columns[header_StdPacking_White].Index;
                    int stdPackingYellowIndex = dgv.Columns[header_StdPacking_Yellow].Index;

                    // Check if current cell is in an editable column
                    if (colIndex == qtyWhiteIndex ||
                        colIndex == qtyBlueIndex ||
                        colIndex == qtyYellowIndex || colIndex == stdPackingBlueIndex || colIndex == stdPackingWhiteIndex || colIndex == stdPackingYellowIndex ||
                        colIndex == balanceIndex)
                    {
                        // Enable editing for editable columns
                        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dgv.ReadOnly = false;
                        dgv.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

                        // Make only the current column editable
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            dgv.Columns[i].ReadOnly = (i != colIndex);
                        }
                    }
                    else
                    {
                        // Disable editing for non-editable columns
                        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgv.ReadOnly = true;
                        dgv.EditMode = DataGridViewEditMode.EditProgrammatically;

                        // Make all columns read-only
                        for (int i = 0; i < dgv.Columns.Count; i++)
                        {
                            dgv.Columns[i].ReadOnly = true;
                        }

                        // End any current editing
                        dgv.EndEdit();
                    }
                }
            }
        }

        private void btnSaveStdPacking_Click(object sender, EventArgs e)
        {
            if (!hasStdPackingChanges || changedItemCodes.Count == 0)
            {
                MessageBox.Show("No changes to save.");
                return;
            }

            DialogResult result = MessageBox.Show($"Save standard packing changes for {changedItemCodes.Count} items?",
                                                 "Confirm Save",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SaveStdPackingChanges();
            }
        }

        private void SaveStdPackingChanges()
        {
            int successCount = 0;
            int errorCount = 0;

            foreach (string itemCode in changedItemCodes)
            {
                // Find the row for this item code
                DataGridViewRow targetRow = null;
                foreach (DataGridViewRow row in dgvStockAlert.Rows)
                {
                    if (row.Cells[header_ItemCode].Value?.ToString() == itemCode)
                    {
                        targetRow = row;
                        break;
                    }
                }

                if (targetRow != null)
                {
                    // Get the values from the row
                    int qtyWhite = int.TryParse(targetRow.Cells[header_StdPacking_White].Value?.ToString(), out int white) ? white : 0;
                    int qtyBlue = int.TryParse(targetRow.Cells[header_StdPacking_BLue].Value?.ToString(), out int blue) ? blue : 0;
                    int qtyYellow = int.TryParse(targetRow.Cells[header_StdPacking_Yellow].Value?.ToString(), out int yellow) ? yellow : 0;

                    // Prepare the BLL object with default values for new records
                    uSBB.Item_code = itemCode;
                    uSBB.Qty_Per_Container = qtyWhite; // White container
                    uSBB.Qty_Per_Blue_Container = qtyBlue;
                    uSBB.Qty_Per_Yellow_Container = qtyYellow;
                    uSBB.Qty_Per_Packet = 0; // Default for new records
                    uSBB.Qty_Per_Bag = 0; // Default for new records
                    uSBB.Max_Lvl = 0; // Default for new records
                    uSBB.Updated_Date = DateTime.Now;
                    uSBB.Updated_By = MainDashboard.USER_ID;
                    uSBB.IsRemoved = false; // Default for new records
                    uSBB.Note = ""; // Default for new records

                    // Use Upsert method (Insert or Update automatically)
                    if (dalSBB.UpsertStdPackingContainers(uSBB))
                    {
                        successCount++;
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }

            // Show result message
            string message = $"Standard packing saved successfully for {successCount} items.";
            if (errorCount > 0)
            {
                message += $"\n{errorCount} items failed to save.";
            }

            MessageBox.Show(message, "Save Complete", MessageBoxButtons.OK,
                           errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            // Reset tracking
            hasStdPackingChanges = false;
            changedItemCodes.Clear();
            btnSaveStdPacking.Visible = false;
        }


        private bool STOCK_TRANSFER_MODE = false;

        private void StockTransferModeOn(bool On)
        {
            STOCK_TRANSFER_MODE = On;
            gbStockTransferLocation.Enabled = On;
            

            if(On)
            {
                tlpStockInfoMainSetting.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 150f);
                tlpStockInfoMainSetting.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 114f);
                tlpStockInfoMainSetting.ColumnStyles[7] = new ColumnStyle(SizeType.Absolute, 0f);

            }
            else
            {
                tlpStockInfoMainSetting.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpStockInfoMainSetting.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpStockInfoMainSetting.ColumnStyles[7] = new ColumnStyle(SizeType.Absolute, 120f);
            }

            //column change
        }
        private void btnNewJob_Click(object sender, EventArgs e)
        {
            btnStockInfoReload.Visible = false;
            if (cbProduct.Checked)
            {
                MessageBox.Show("Cannot make Stock Transfer to Product!");
                return;
            }

            // Validate transfer locations
            if (string.IsNullOrEmpty(cmbTransferFrom.Text) || string.IsNullOrEmpty(cmbTransferTo.Text))
            {
                MessageBox.Show("Please select both Transfer From and Transfer To locations.");

                gbStockTransferLocation.Enabled = true;
                HideFilter(false);
                return;
            }

            if (cmbTransferFrom.Text == cmbTransferTo.Text)
            {
                MessageBox.Show("Transfer From and Transfer To locations cannot be the same.");
                return;
            }

            if (lblStockAlert.Text == "Stock Info")
            {
                StockTransferUIMode(true);
                LoadStdPackingData();
            }
            else if (lblStockAlert.Text == "Stock Transfer")
            {
                // Process the transfer - you can implement this part later
                ProcessStockTransfer();
            }
        }

        private void ProcessStockTransfer()
        {
            // Count items with transfer quantities
            int transferItemCount = 0;
            string transferSummary = "";

            foreach (DataGridViewRow row in dgvStockAlert.Rows)
            {
                if (row.IsNewRow) continue;

                int totalTransferPcs = int.TryParse(row.Cells[text.Header_Transfer_Qty].Value?.ToString(), out int total) ? total : 0;

                if (totalTransferPcs > 0)
                {
                    transferItemCount++;

                    string itemCode = row.Cells[header_ItemCode].Value?.ToString() ?? "";
                    string itemName = row.Cells[header_ItemName].Value?.ToString() ?? "";
                    int transferQtyWhite = int.TryParse(row.Cells[header_Qty_White].Value?.ToString(), out int white) ? white : 0;
                    int transferQtyBlue = int.TryParse(row.Cells[header_Qty_Blue].Value?.ToString(), out int blue) ? blue : 0;
                    int transferQtyYellow = int.TryParse(row.Cells[header_Qty_Yellow].Value?.ToString(), out int yellow) ? yellow : 0;
                    int transferBalance = int.TryParse(row.Cells[header_Balance].Value?.ToString(), out int balance) ? balance : 0;
                    string remark = row.Cells[header_Remark].Value?.ToString() ?? "";

                    // Build summary for confirmation
                    transferSummary += $"{itemCode} - {totalTransferPcs} pcs\n";
                }
            }

            if (transferItemCount == 0)
            {
                MessageBox.Show("No items selected for transfer.");
                return;
            }

            // Show confirmation with summary
            string confirmMessage = $"Confirm transfer of {transferItemCount} items from {cmbTransferFrom.Text} to {cmbTransferTo.Text}?\n\nItems:\n{transferSummary}";

            if (MessageBox.Show(confirmMessage, "Confirm Transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string stockTrfFrom = cmbTransferFrom.Text;
                string stockTrfTo = cmbTransferTo.Text;

                frmInOutEdit frm = new frmInOutEdit((DataTable)dgvStockAlert.DataSource,stockTrfFrom, stockTrfTo, dtpTransferDate.Value);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();//Item Edit

                if (frmInOutEdit.TrfSuccess)
                {
                    StockCheckUIMode(false);
                    LoadStockInfo();
                }

            }




        }

        private void StockTransferUIMode(bool turnOn)
        {
            DataGridView dgv = dgvStockAlert;
            dtpTransferDate.Value = DateTime.Today;
            HideFilter(false);
            btnSaveStdPacking.Visible = false;

            ToggleUIExpansion();
            gbStockTransferLocation.Enabled = turnOn;

            if (turnOn)
            {
                cbEditStdPacking.Checked = false;
                dgvStockAlert.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgvStockAlert.ReadOnly = false;
                dgvStockAlert.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

                // Adjust column layout for stock transfer
                tlpStockInfoMainSetting.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f); // Hide refresh
                tlpStockInfoMainSetting.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f); // Hide filter
                tlpStockInfoMainSetting.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 150f); // Show transfer location
                tlpStockInfoMainSetting.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 114f); // Show cancel
                tlpStockInfoMainSetting.ColumnStyles[7] = new ColumnStyle(SizeType.Absolute, 0f); // Hide stock check

                StockInfoShowRemark(true);

                STOCK_TRANSFER_MODE = true;

                btnCancelStockCheck.Visible = true;
                btnCancelStockCheck.Text = "Cancel Transfer";
                btnStockTransfer.Enabled = false;
                btnStockTransfer.Text = "TRANSFER ITEMS";
                lblStockAlert.Text = "Stock Transfer";

                btnStockRefresh.Visible = false;
                cbProduct.Visible = false;
                cbInPcsUnit.Visible = false;
                cbInBagUnit.Visible = false;

                // Show transfer quantity columns (reuse existing qty columns)
                dgv.Columns[header_Qty_White].Visible = true;
                dgv.Columns[header_Qty_Blue].Visible = true;
                dgv.Columns[header_Qty_Yellow].Visible = true;
                dgv.Columns[header_Balance].Visible = true;
                dgv.Columns[header_Remark].Visible = true;
                dgv.Columns[text.Header_Transfer_Qty].Visible = true;

                // Show standard packing columns for reference
                dgv.Columns[header_StdPacking_White].Visible = false;
                dgv.Columns[header_StdPacking_BLue].Visible = false;
                dgv.Columns[header_StdPacking_Yellow].Visible = false;

                // Hide columns not needed for transfer
                dgv.Columns[header_StockMinLvl].Visible = false;
                dgv.Columns[header_Note].Visible = false;
                dgv.Columns[header_BalAfter].Visible = false;
                dgv.Columns[header_BalAfterBag].Visible = false;
                dgv.Columns[header_BalAfterPcs].Visible = false;
                dgv.Columns[header_ProDaysNeeded].Visible = false;
                dgv.Columns[header_ActualStock].Visible = false;
                dgv.Columns[header_StockDiff].Visible = false;

                dgv.Columns[header_TotalAssembly].Visible = false;
                dgv.Columns[header_TotalProduction].Visible = false;
                dgv.Columns[header_DailyUsage].Visible = false;

                dgv.Columns[header_MonthlyUsage].Visible = false;
                dgv.Columns[header_Est_Stock_Zero_Date].Visible = false;
                dgv.Columns[header_MaxProdPerDay].Visible = false;
                dgv.Columns[header_Start_Prod_By_Date].Visible = false;

                // Show std packing string for reference
                dgv.Columns[header_StdPacking_String].Visible = true;

                // Set column colors for transfer (reuse existing qty columns)
                dgv.Columns[header_Qty_White].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.Columns[header_Qty_Blue].DefaultCellStyle.BackColor = Color.Blue;
                dgv.Columns[header_Qty_Blue].DefaultCellStyle.ForeColor = Color.White;
                dgv.Columns[header_Qty_Yellow].DefaultCellStyle.BackColor = Color.Yellow;
                dgv.Columns[text.Header_Transfer_Qty].DefaultCellStyle.BackColor = Color.LightBlue;

                // Set alignment
                dgv.Columns[header_Qty_White].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Qty_Blue].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Qty_Yellow].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Balance].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_Transfer_Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                ResetStdPackingTracking();
                dgvStockAlert.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                STOCK_TRANSFER_MODE = false;

                dgv.Columns[header_ItemName].Frozen = false;
                tlpStockInfoMainSetting.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 40f);
                tlpStockInfoMainSetting.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 105f);
                //tlpStockInfoMainSetting.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0f); // Hide transfer location
                tlpStockInfoMainSetting.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 0f); // Hide cancel
                tlpStockInfoMainSetting.ColumnStyles[7] = new ColumnStyle(SizeType.Absolute, 120f); // Show stock check
                StockInfoShowRemark(false);

                // Hide transfer columns (reuse existing qty columns)
                dgv.Columns[text.Header_Transfer_Qty].Visible = false;

                dgv.Columns[header_Qty_White].Visible = false;
                dgv.Columns[header_StdPacking_White].Visible = false;
                dgv.Columns[header_Qty_Blue].Visible = false;
                dgv.Columns[header_StdPacking_BLue].Visible = false;
                dgv.Columns[header_StdPacking_Yellow].Visible = false;
                dgv.Columns[header_Qty_Yellow].Visible = false;
                dgv.Columns[header_Balance].Visible = false;
                dgv.Columns[header_Remark].Visible = false;

                dgv.Columns[header_ActualStock].Visible = false;
                dgv.Columns[header_StockDiff].Visible = false;
                dgv.Columns[header_Remark].Visible = false;

                dgv.Columns[header_StdPacking_String].Visible = false;

                dgv.Columns[header_BalAfter].Visible = true;
                dgv.Columns[header_StockMinLvl].Visible = true;
                dgv.Columns[header_Note].Visible = true;
                dgv.Columns[header_TotalAssembly].Visible = true;
                dgv.Columns[header_TotalProduction].Visible = true;
                dgv.Columns[header_DailyUsage].Visible = true;
                dgv.Columns[header_ProDaysNeeded].Visible = true;

                dgv.Columns[header_MonthlyUsage].Visible = true;
                dgv.Columns[header_Est_Stock_Zero_Date].Visible = true;
                dgv.Columns[header_MaxProdPerDay].Visible = true;
                dgv.Columns[header_Start_Prod_By_Date].Visible = true;

                btnCancelStockCheck.Visible = false;
                btnCancelStockCheck.Text = "Cancel";

                btnStockTransfer.Enabled = true;
                btnStockTransfer.Text = "Stock Transfer";
                lblStockAlert.Text = "Stock Info";

                btnStockRefresh.Visible = true;
                cbProduct.Visible = true;
                cbInPcsUnit.Visible = true;
                cbInBagUnit.Visible = true;

                // Clear transfer data and reload stock info
                LoadStockInfo();
            }
        }

        private void CalculateTransferPcs(DataGridView dgv, int rowIndex)
        {
            try
            {
                // Get standard packing values (pieces per container)
                int stdPackingWhite = int.TryParse(dgv.Rows[rowIndex].Cells[header_StdPacking_White].Value?.ToString(), out int whiteStd) ? whiteStd : 0;
                int stdPackingBlue = int.TryParse(dgv.Rows[rowIndex].Cells[header_StdPacking_BLue].Value?.ToString(), out int blueStd) ? blueStd : 0;
                int stdPackingYellow = int.TryParse(dgv.Rows[rowIndex].Cells[header_StdPacking_Yellow].Value?.ToString(), out int yellowStd) ? yellowStd : 0;

                // Get transfer quantity values (number of containers to transfer) - reuse existing qty columns
                int transferQtyWhite = int.TryParse(dgv.Rows[rowIndex].Cells[header_Qty_White].Value?.ToString(), out int transferW) ? transferW : 0;
                int transferQtyBlue = int.TryParse(dgv.Rows[rowIndex].Cells[header_Qty_Blue].Value?.ToString(), out int transferB) ? transferB : 0;
                int transferQtyYellow = int.TryParse(dgv.Rows[rowIndex].Cells[header_Qty_Yellow].Value?.ToString(), out int transferY) ? transferY : 0;

                // Get balance transfer (loose pieces)
                int transferBalance = int.TryParse(dgv.Rows[rowIndex].Cells[header_Balance].Value?.ToString(), out int transferBal) ? transferBal : 0;


                //to send warning message, if transfer qty > 0, but std packing = 0
                if (transferQtyWhite > 0 && stdPackingWhite == 0)
                {
                    MessageBox.Show("White transfer QTY cannot be set because STD. PACKING is 0.\nIt has been cleared.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dgv.Rows[rowIndex].Cells[header_Qty_White].Value = 0;
                    transferQtyWhite = 0;
                }

                if (transferQtyBlue > 0 && stdPackingBlue == 0)
                {
                    MessageBox.Show("Blue transfer QTY cannot be set because STD. PACKING is 0.\nIt has been cleared.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dgv.Rows[rowIndex].Cells[header_Qty_Blue].Value = 0;
                    transferQtyBlue = 0;
                }

                if (transferQtyYellow > 0 && stdPackingYellow == 0)
                {
                    MessageBox.Show("Yellow transfer QTY cannot be set because STD. PACKING is 0.\nIt has been cleared.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dgv.Rows[rowIndex].Cells[header_Qty_Yellow].Value = 0;
                    transferQtyYellow = 0;
                }

                // Calculate total transfer pieces: (containers × pieces per container) + balance
                int totalTransferPcs = (transferQtyWhite * stdPackingWhite) +
                                      (transferQtyBlue * stdPackingBlue) +
                                      (transferQtyYellow * stdPackingYellow) +
                                      transferBalance;


                string remark = (transferQtyWhite * stdPackingWhite) > 0 ? transferQtyWhite + "W" : "";

                remark += (transferQtyBlue * stdPackingBlue) > 0 ? remark != "" ? " + " + transferQtyBlue + "B" : transferQtyBlue + "B" : "";
                remark += (transferQtyYellow * stdPackingYellow) > 0 ? remark != "" ? " + " + transferQtyYellow + "Y" : transferQtyYellow + "Y" : "";
                remark += transferBalance > 0 ? remark != "" ? " +Bal. " + transferBalance + "pcs" : "Bal. " + transferBalance + "pcs" : "";


                // Update the Total Transfer PCS column
                dgv.Rows[rowIndex].Cells[text.Header_Transfer_Qty].Value = totalTransferPcs;
                dgv.Rows[rowIndex].Cells[header_Remark].Value = remark;

                // Enable transfer button if any transfer quantity is entered
                if (totalTransferPcs > 0)
                {
                    btnStockTransfer.Enabled = true;
                }
                else
                {
                    btnStockTransfer.Enabled = HasAnyTransferQty();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating transfer pieces: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to check if any transfer quantity exists
        private bool HasAnyTransferQty()
        {
            if (STOCK_TRANSFER_MODE)
            {
                DataTable dt = (DataTable)dgvStockAlert.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    string totalTransferPcs = row[text.Header_Transfer_Qty].ToString();

                    if (!string.IsNullOrEmpty(totalTransferPcs) && totalTransferPcs != "0")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnStockInfoReload.Visible = true;
        }

        private void txtProdLeadTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters (backspace, delete, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnStockInfoReload_Click(object sender, EventArgs e)
        {
            LoadUsage();

        }

        private void cbSundayisWorkday_CheckedChanged(object sender, EventArgs e)
        {
            CalculatePeriodDetails();
            btnStockInfoReload.Visible = true;

        }

        private void dgvStockAlert_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        private void ShowMachineHistory(string itemCode)
        {
            try
            {
                // Get data from planning table
                planningDAL dalPlanning = new planningDAL();
                DataTable dt = dalPlanning.SelectCompletedOrRunningPlan();

                // Collect history records and machines
                var historyList = new List<(string machine, int jobNo, int produced, DateTime date)>();
                var machineSet = new HashSet<string>();

                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalPlanning.partCode].ToString() == itemCode)
                    {
                        string macName = row[dalPlanning.machineName]?.ToString();
                        int jobNo = int.TryParse(row[dalPlanning.jobNo]?.ToString(), out jobNo) ? jobNo : 0;
                        int produced = int.TryParse(row[dalPlanning.planProduced]?.ToString(), out produced) ? produced : 0;
                        DateTime date = DateTime.TryParse(row[dalPlanning.productionStartDate]?.ToString(), out date) ? date : DateTime.MinValue;

                        if (!string.IsNullOrEmpty(macName))
                        {
                            historyList.Add((macName, jobNo, produced, date));
                            machineSet.Add(macName);
                        }
                    }
                }

                // Show popup form
                ShowProductionHistoryPopup(itemCode, historyList, machineSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading production history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add this new method for the popup:
        private void ShowProductionHistoryPopup(string itemCode, List<(string machine, int jobNo, int produced, DateTime date)> historyList, HashSet<string> machineSet)
        {
            // Create popup form
            Form frmHistory = new Form();
            frmHistory.Text = "Production History";
            frmHistory.StartPosition = FormStartPosition.CenterParent;
            frmHistory.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmHistory.MaximizeBox = false;
            frmHistory.MinimizeBox = false;
            frmHistory.BackColor = Color.White;
            frmHistory.Size = new Size(550, 500);

            // Create scrollable panel
            Panel pnlContent = new Panel();
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.AutoScroll = true;
            pnlContent.BackColor = Color.White;
            pnlContent.Padding = new Padding(20);
            // Create content
            string content = GenerateProductionHistoryText(itemCode, historyList, machineSet);

            Label lblContent = new Label();
            lblContent.Text = content;
            lblContent.AutoSize = true;
            lblContent.Font = new Font("Consolas", 9F); // Monospace font for alignment
            lblContent.ForeColor = Color.FromArgb(1, 33, 71);
            lblContent.Location = new Point(20, 10); // Move it 20px from left, 10px from top

            pnlContent.Controls.Add(lblContent);
            frmHistory.Controls.Add(pnlContent);

            // Show popup
            frmHistory.ShowDialog(this);
        }

        // Add this method to format the text:
        private string GenerateProductionHistoryText(string itemCode, List<(string machine, int jobNo, int produced, DateTime date)> historyList, HashSet<string> machineSet)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("===============================================");
            sb.AppendLine($"  PRODUCTION HISTORY - {itemCode}");
            sb.AppendLine("===============================================");
            sb.AppendLine();

            if (historyList.Count == 0)
            {
                sb.AppendLine("No production history found for this item.");
            }
            else
            {
                // Show machines
                var sortedMachines = machineSet.OrderBy(m => m).ToList();
                sb.AppendLine($"MACHINES: {string.Join(", ", sortedMachines)}");
                sb.AppendLine();
                sb.AppendLine("RECORDS (Latest First):");
                sb.AppendLine();

                // Sort by date (latest first)
                var sortedHistory = historyList.OrderByDescending(h => h.date).ToList();

                foreach (var record in sortedHistory)
                {
                    string dateStr = record.date.ToString("dd/MM/yyyy");
                    sb.AppendLine($"{record.machine,-4} | Job #{record.jobNo,-6} | {record.produced,8:N0} produced | {dateStr}");
                }
            }

            return sb.ToString();
        }

        private void dgvStockAlert_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !dgvStockAlert.IsCurrentCellInEditMode)
                dgvStockAlert.BeginEdit(true);
        }

        private void dgvStockAlert_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }

        private void dgvStockAlert_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridView dgv = dgvStockAlert;
            //int rowIndex = e.RowIndex;
            //int colIndex = e.ColumnIndex;

            //if(rowIndex >= 0 && colIndex >=0)
            //{
            //    string columnName = dgv.Columns[colIndex].Name;

            //    string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value?.ToString() ?? "";

            //    if (string.IsNullOrEmpty(cellValue))
            //    {
            //        return;
            //    }

            //    // Handle std packing columns - update string
            //    if (columnName.Contains(header_StdPacking_White) ||
            //        columnName.Contains(header_StdPacking_BLue) ||
            //        columnName.Contains(header_StdPacking_Yellow))
            //    {
            //        UpdateStdPackingString(dgv, rowIndex);
            //        CalculateActualStock(dgv, rowIndex);

            //        // Track this item for saving
            //        string itemCode = dgv.Rows[rowIndex].Cells[header_ItemCode].Value?.ToString();
            //        if (!string.IsNullOrEmpty(itemCode))
            //        {
            //            changedItemCodes.Add(itemCode);
            //            hasStdPackingChanges = true;
            //            btnSaveStdPacking.Visible = true; // Show save button
            //        }
            //    }
            //    else if (columnName.Contains(header_Qty_White) ||
            //        columnName.Contains(header_Qty_Blue) ||
            //        columnName.Contains(header_Qty_Yellow) ||
            //        columnName.Contains(header_Balance))
            //    {
            //        if (STOCK_TRANSFER_MODE)
            //        {
            //            CalculateTransferPcs(dgv, rowIndex);
            //        }
            //        else
            //        {
            //            CalculateActualStock(dgv, rowIndex); // Existing stock check logic
            //        }
            //    }

            //    //// Handle quantity and balance columns - calculate actual stock
            //    //else if (columnName.Contains(header_Qty_White) ||
            //    //         columnName.Contains(header_Qty_Blue) ||
            //    //         columnName.Contains(header_Qty_Yellow) ||
            //    //         columnName.Contains(header_Balance))
            //    //{
            //    //    CalculateActualStock(dgv, rowIndex);
            //    //}   
            //    // Handle stock min level
            //    else if (columnName.Contains(header_StockMinLvl))
            //    {
            //        HandleStockMinLevelUpdate(dgv, rowIndex, colIndex);
            //    }
            //}
          
        }
    }
}
