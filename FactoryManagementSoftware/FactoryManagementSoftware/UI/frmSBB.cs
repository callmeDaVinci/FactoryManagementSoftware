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

namespace FactoryManagementSoftware.UI
{
    public partial class frmSBB : Form
    {
        public frmSBB()
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

        private void InitialMonthlyDate()
        {
            if(MonthlyDateStart == null)
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

        private static frmSBB _instance;

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
        string header_Stock = "STOCK SEMENYIH (PCS/KG)";
        //string header_BalAfter1 = "BAL. AFTER ";
        //string header_BalAfter2 = "BAL. AFTER ";
        //string header_BalAfter3 = "BAL. AFTER ";
        readonly string header_Produced = "PRODUCED";
        readonly string header_ProduceTarget = "PRODUCE TARGET";
        readonly string header_ProDaysNeeded = "PRO DAY(24HRS) NEEDED";

        readonly string header_StdPacking_Bag = "PCS/BAG";
        readonly string header_StdPacking_Ctn = "PCS/CTN";
        readonly string header_StdPacking_String = "STD PACKING";

        readonly string header_ActualStock_PCS = "PCS";
        readonly string header_ActualStock_BAG = "BAG";
        readonly string header_ActualStock_CTN = "CTN";
        readonly string header_ActualStock = "ACTUAL STOCK";

        readonly string header_StockDiff = "STOCK DIFF.";

        readonly string header_TotalProduction = "TOT. PRO. (PCS/KG)";
        readonly string header_TotalAssembly = "TOT. ASSY (PCS)";
        string header_AVGAssembly = "AVG. ASSY";

        readonly string Type_Product = "PRODUCT";
        readonly string Type_Part = "PART/MAT./PACKAGING";

        private bool Loaded = false;
        private bool stockCheckMode = false;

        private readonly string header_ItemSize_Numerator = "SIZE_NUMERATOR";
        private readonly string header_ItemSize_Denominator = "SIZE_DENOMINATOR";
        private readonly string header_SizeUnit = "SIZE UNIT";
        private readonly string header_ItemType = "ITEM TYPE";

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
            dt.Rows.Add(text.Factory_Bina);
            dt.Rows.Add("ALL");

            cmb.DataSource = dt;
            cmb.DisplayMember = "LOCATION";
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

        private DataTable NewStockAlertTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_ItemType, typeof(int));
            dt.Columns.Add(header_ItemCategory, typeof(int));

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));

            dt.Columns.Add(header_Stock, typeof(int));
            dt.Columns.Add(header_BalAfter, typeof(double));
            dt.Columns.Add(header_QtyPerBag, typeof(int));
            dt.Columns.Add(header_BalAfterBag, typeof(int));
            dt.Columns.Add(header_BalAfterPcs, typeof(int));

            dt.Columns.Add(header_Produced, typeof(int));
            dt.Columns.Add(header_ProduceTarget, typeof(int));
            dt.Columns.Add(header_ProDaysNeeded, typeof(float));
            dt.Columns.Add(header_Status, typeof(string));

            dt.Columns.Add(header_StdPacking_Bag, typeof(int));
            dt.Columns.Add(header_StdPacking_Ctn, typeof(int));
            dt.Columns.Add(header_StdPacking_String, typeof(string));

            dt.Columns.Add(header_ActualStock_PCS, typeof(int));
            dt.Columns.Add(header_ActualStock_BAG, typeof(int));
            dt.Columns.Add(header_ActualStock_CTN, typeof(int));

            dt.Columns.Add(header_ActualStock, typeof(int));
            dt.Columns.Add(header_StockDiff, typeof(int));

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

            header_AVGAssembly = "AVG. ASSY ("+totalDay+" days)" ;

            dt.Columns.Add(header_AVGAssembly, typeof(int));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            //dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgv.Columns[header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

                dgv.Columns[header_StdPacking_Bag].Visible = false;
                dgv.Columns[header_StdPacking_Ctn].Visible = false;
                dgv.Columns[header_StdPacking_String].Visible = false;
                dgv.Columns[header_ActualStock_PCS].Visible = false;
                dgv.Columns[header_ActualStock_BAG].Visible = false;
                dgv.Columns[header_ActualStock_CTN].Visible = false;

                //dgv.Columns[header_ItemCode].DefaultCellStyle.ForeColor = Color.Gray;
                //dgv.Columns[header_ItemCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

                dgv.Columns[header_ItemName].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                dgv.Columns[header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                

                dgv.Columns[header_Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[header_BalAfter].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ActualStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[header_StockDiff].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[header_BalAfterBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_BalAfterPcs].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[header_ActualStock].DefaultCellStyle.BackColor = SystemColors.Info;


                if (stockCheckMode)
                {
                    dgv.Columns[header_BalAfter].Visible = false;
                    dgv.Columns[header_ProDaysNeeded].Visible = false;
                }
                else
                {
                    dgv.Columns[header_ActualStock].Visible = false;
                    dgv.Columns[header_StockDiff].Visible = false;

                    if (cmbType.Text == Type_Part)
                    {
                        dgv.Columns[header_Stock].Visible = true;

                        dgv.Columns[header_BalAfter].Visible = true;
                        dgv.Columns[header_BalAfterBag].Visible = false;
                        dgv.Columns[header_BalAfterPcs].Visible = false;
                        dgv.Columns[header_ProDaysNeeded].Visible = true;
                    }
                    else if (cmbType.Text == Type_Product)
                    {
                        dgv.Columns[header_Stock].Visible = false;
                        dgv.Columns[header_ProDaysNeeded].Visible = false;
                        if (cbInBagUnit.Checked)
                        {

                            dgv.Columns[header_BalAfter].Visible = false;
                            dgv.Columns[header_BalAfterBag].Visible = true;
                            dgv.Columns[header_BalAfterPcs].Visible = true;

                        }
                        else if (cbInPcsUnit.Checked)
                        {

                            dgv.Columns[header_BalAfter].Visible = true;
                            dgv.Columns[header_BalAfterBag].Visible = false;
                            dgv.Columns[header_BalAfterPcs].Visible = false;

                        }

                    }


                    dgv.Columns[header_ProDaysNeeded].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            else if (dgv == dgvUsage)
            {
                dgv.Columns[header_TotalProduction].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_TotalAssembly].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_AVGAssembly].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

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
            DataTable dt_ChildPart = NewStockAlertTable();
            DataTable dt_ChildMat = NewStockAlertTable();
            DataTable dt_Packaging = NewStockAlertTable();

            string stockLocation = cmbStockLocation.Text;

            if (string.IsNullOrEmpty(stockLocation))
            {
                cmbStockLocation.Text = text.Factory_Semenyih;
                stockLocation = text.Factory_Semenyih;
            }

            foreach (DataRow row in dt_Product.Rows)
            {
                string itemCode = row[header_ItemCode].ToString();

                
                string parentCode = itemCode;

                if (itemCode[7].ToString() == "E" && itemCode[8].ToString() != "C")
                {
                    parentCode = GetChildCode(dt_JoinSelectWithChildCat, row[header_ItemCode].ToString());
                }

                int stillNeed = int.TryParse(row[header_BalAfter].ToString(), out stillNeed) ? stillNeed : 0;

                stillNeed = stillNeed > 0 ? 0 : stillNeed * -1;

                foreach (DataRow rowJoin in dt_JoinSelectWithChildCat.Rows)
                {
                    if (rowJoin[dalJoin.ParentCode].ToString() == parentCode)
                    {
                        int itemType = int.TryParse(rowJoin[dalSBB.TypeTblCode].ToString(), out int i) ? i : 99;
                        int itemCat = int.TryParse(rowJoin[dalSBB.CategoryTblCode].ToString(), out  i) ? i : 99;

                        string childCat = rowJoin[dalJoin.ChildCat].ToString();
                        string childCode = rowJoin[dalJoin.ChildCode].ToString();
                        string childName = rowJoin[dalJoin.ChildName].ToString();

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

                            int readyStock = int.TryParse(rowJoin[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;


                            if (stockLocation != "ALL")
                            {
                                readyStock = (int)loadStockList(childCode, stockLocation);
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

                            int child_StillNeed = 0;

                            joinMax = joinMax <= 0 ? 1 : joinMax;
                            joinMin = joinMin <= 0 ? 1 : joinMin;

                            child_StillNeed = stillNeed / joinMax * joinQty;

                            child_StillNeed = stillNeed % joinMax >= joinMin ? child_StillNeed + joinQty : child_StillNeed;


                            int bal = readyStock - child_StillNeed;

                            float totalMaterialNeededInKG = child_StillNeed * PWRWPerPcs / 1000;

                            rawMat_Need = (float)Math.Round(totalMaterialNeededInKG * RawMaterialPercentage * rawRatio, 3);
                            recycleMat_Need = (float)Math.Round(totalMaterialNeededInKG * recycleRatio, 3);
                            colorMat_Need = (float)Math.Round(totalMaterialNeededInKG * childColorRate * rawRatio, 3);

                            bool childInserted = false;

                            foreach (DataRow mat_row in dt_ChildPart.Rows)
                            {
                                if (childCode == mat_row[header_ItemCode].ToString())
                                {
                                    childInserted = true;

                                    int previousBal = int.TryParse(mat_row[header_BalAfter].ToString(), out previousBal) ? previousBal : 0;

                                    bal = previousBal - child_StillNeed;

                                    mat_row[header_BalAfter] = bal;


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
                                alert_row[header_Stock] = readyStock;

                                dt_ChildPart.Rows.Add(alert_row);

                                bool matFound = false;
                                bool recycleFound = false;
                                bool colorMatFound = false;

                                foreach (DataRow part in dt_ChildMat.Rows)
                                {
                                    double balAfter = double.TryParse(part[header_BalAfter].ToString(), out balAfter) ? balAfter : 0;

                                    if (childMat == part[header_ItemCode].ToString())
                                    {
                                        matFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - rawMat_Need, 3);

                                    }

                                    if (childRecycle == part[header_ItemCode].ToString())
                                    {
                                        recycleFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - recycleMat_Need, 3);
                                    }

                                    if (childColorMat == part[header_ItemCode].ToString())
                                    {
                                        colorMatFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - colorMat_Need, 3);
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

                                    if (childMat == part[header_ItemCode].ToString())
                                    {
                                        matFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - rawMat_Need, 3);

                                    }

                                    if (childRecycle == part[header_ItemCode].ToString())
                                    {
                                        recycleFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - recycleMat_Need, 3);
                                    }

                                    if (childColorMat == part[header_ItemCode].ToString())
                                    {
                                        colorMatFound = true;
                                        part[header_BalAfter] = Math.Round(balAfter - colorMat_Need, 3);
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
                                    alert_row[header_Stock] = stock;

                                    dt_ChildMat.Rows.Add(alert_row);
                                }

                                if (!recycleFound && !string.IsNullOrEmpty(childRecycle))
                                {
                                    alert_row = dt_ChildMat.NewRow();

                                    float stock = tool.getStockQtyFromDataTable(dt_Item, childRecycle);

                                    alert_row[header_ItemType] = 95;
                                    alert_row[header_ItemCategory] = 95;
                                    alert_row[header_ItemCode] = childRecycle;
                                    alert_row[header_ItemName] = tool.getItemNameFromDataTable(dt_Item, childRecycle);
                                    alert_row[header_BalAfter] = recycleMat_Need;
                                    alert_row[header_Stock] = stock;

                                    dt_ChildMat.Rows.Add(alert_row);
                                }

                                if (!colorMatFound && !string.IsNullOrEmpty(childColorMat))
                                {
                                    alert_row = dt_ChildMat.NewRow();

                                    float stock = tool.getStockQtyFromDataTable(dt_Item, childColorMat);

                                    alert_row[header_ItemType] = 96;
                                    alert_row[header_ItemCategory] = 96;
                                    alert_row[header_ItemCode] = childColorMat;
                                    alert_row[header_ItemName] = tool.getItemNameFromDataTable(dt_Item, childColorMat);
                                    alert_row[header_BalAfter] = colorMat_Need;
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

                                        bal = previousBal - child_StillNeed;

                                        part[header_BalAfter] = bal;

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
                                    alert_row[header_Stock] = readyStock;

                                    dt_Packaging.Rows.Add(alert_row);

                                }
                            }
                        }

                        

                    }
                }
            }

            if (dt_ChildMat != null && dt_ChildMat.Rows.Count > 0 && cbMat.Checked)
            {
                foreach (DataRow row in dt_ChildMat.Rows)
                {
                    DataRow newRow = dt_ChildPart.NewRow();

                    newRow[header_ItemType] = 98;
                    newRow[header_ItemCategory] = 98;
                    newRow[header_ItemCode] = row[header_ItemCode];
                    newRow[header_ItemName] = row[header_ItemName];
                    newRow[header_BalAfter] = row[header_BalAfter];
                    newRow[header_Stock] = row[header_Stock];

                    dt_ChildPart.Rows.Add(newRow);
                }
            }

            if (dt_Packaging != null && dt_Packaging.Rows.Count > 0 && cbPackaging.Checked)
            {
                foreach (DataRow row in dt_Packaging.Rows)
                {
                    DataRow newRow = dt_ChildPart.NewRow();

                    newRow[header_ItemType] = 97;
                    newRow[header_ItemCategory] = 97;
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
            DataTable dt_MatPart = NewStockAlertTable();
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

                    if(itemCode.Equals(stockItem) && Fac.Equals(stockFacName))
                    {
                        stockQty = float.TryParse(stock["stock_qty"].ToString(), out stockQty) ? stockQty : 0;
                        return stockQty;
                    }
                }
            }
            return stockQty;
        }

        private void LoadStockAlert()
        {
            Cursor = Cursors.WaitCursor;

            //DataTable dt_PendingPOSelect = dalSBB.PendingPOSelect();

            DataTable dt_FillAll = NewStockAlertTable();

            string stockLocation = cmbStockLocation.Text;

            if(string.IsNullOrEmpty(stockLocation))
            {
                cmbStockLocation.Text = text.Factory_Semenyih;
                stockLocation = text.Factory_Semenyih;
            }

            //Load SBB GOODS FILL ALL ALERT
            foreach (DataRow pendingRow in dt_PendingPOSelect.Rows)
            {
                string itemCode = pendingRow[dalSBB.ItemCode].ToString();
                string itemName = pendingRow[dalSBB.ItemName].ToString();

                if(!string.IsNullOrEmpty(itemCode))
                {
                    int itemStock = int.TryParse(pendingRow[dalItem.ItemStock].ToString(), out itemStock) ? itemStock : 0;

                    if (stockLocation != "ALL")
                    {
                        itemStock = (int)loadStockList(itemCode, stockLocation);
                    }


                    int itemPOQty = int.TryParse(pendingRow[dalSBB.POQty].ToString(), out itemPOQty) ? itemPOQty : 0;
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

                                fillAllRow[header_BalAfter] = itemBal - pendingQty;

                                fillAllRow[header_BalAfterBag] = (itemBal - pendingQty) / itemPerBag;
                                fillAllRow[header_BalAfterPcs] = (itemBal - pendingQty) % itemPerBag;

                                break;
                            }
                        }

                    }

                    if (!itemFound)
                    {
                        newRow = dt_FillAll.NewRow();

                        newRow[header_ItemCode] = itemCode;
                        newRow[header_ItemName] = itemName;
                        newRow[header_BalAfter] = itemStock - pendingQty;
                        newRow[header_QtyPerBag] = itemPerBag;

                        if(itemPerBag == 0)
                        {
                            itemPerBag = 1;
                        }
                        newRow[header_BalAfterBag] = (itemStock - pendingQty) / itemPerBag;
                        newRow[header_BalAfterPcs] = (itemStock - pendingQty) % itemPerBag;

                        dt_FillAll.Rows.Add(newRow);
                    }

                }
            }

            if (cmbType.Text == Type_Part)
            {
                dt_FillAll = NEWLoadMatPartList(dt_FillAll);
            }

            //dt_FillAll.DefaultView.Sort = header_BalAfter + " ASC";
            //dt_FillAll = dt_FillAll.DefaultView.ToTable();


            List<DataRow> toDelete = new List<DataRow>();
           
            foreach (DataRow row in dt_FillAll.Rows)
            {
                string itemCode = row[header_ItemCode].ToString();
                int bal = int.TryParse(row[header_BalAfter].ToString(), out int i) ? i : 0;

                if(string.IsNullOrEmpty(row[header_ItemName].ToString()))
                {
                    toDelete.Add(row);
                }
                else if(bal < 0)
                {
                    row[header_ProDaysNeeded] = tool.GetProductionDayNeeded(dt_Item, itemCode, bal * -1);
                }

            }

            foreach (DataRow dr in toDelete)
            {
                dt_FillAll.Rows.Remove(dr);
            }


            dt_FillAll.AcceptChanges();

            DataView dv = dt_FillAll.DefaultView;
            dv.Sort = header_ItemCategory + " ASC," + header_ItemType + " ASC," + header_ItemName + " ASC";


            dt_FillAll = dv.ToTable();

            RearrangeIndex(dt_FillAll);

            dgvStockAlert.DataSource = dt_FillAll;

            DgvUIEdit(dgvStockAlert);

            dgvStockAlert.ClearSelection();


            Cursor = Cursors.Arrow;
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

        private void WriteFileToDrive(DataTable dt,string path, string fileName)
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

            string cust = "SPP";

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

        private void insertItem(string itemCode, string itemName, bool bodyItem, int size1, int size2, int cat, int type)
        {
            //Add data
            itemBLL u = new itemBLL();
            u.item_cat = text.Cat_Part;

            u.item_code = itemCode;
            u.item_name = itemName;

            u.Type_tbl_code = type;
            u.Category_tbl_code = cat;

            u.Size_tbl_code_1 = size1;
            u.Size_tbl_code_2 = size2;


            if (bodyItem)
            {
                u.item_assembly = 0;
                u.item_production = 1;
                u.Category_tbl_code = 2;
            }
            else
            {
                u.item_assembly = 1;
                u.item_production = 0;
            }

            u.item_added_date = DateTime.Now;
            u.item_added_by = MainDashboard.USER_ID;

            //Inserting Data into Database
            bool success = dalItem.SBBItemInsert(u);
            //If the data is successfully inserted then the value of success will be true else false
            if (success)
            {
                //Data Successfully Inserted
                //MessageBox.Show("Item successfully created");
                if (!bodyItem)
                {
                    pairCustomer(itemCode);
                }
            }
            else
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new item");
            }
        }

        private void updateItem(string itemCode, string itemName, bool bodyItem, int size1, int size2, int cat, int type)
        {
            //Add data
            itemBLL u = new itemBLL();
            u.item_cat = text.Cat_Part;

            u.item_code = itemCode;
            u.item_name = itemName;

            u.Type_tbl_code = type;
            u.Category_tbl_code = cat;

            u.Size_tbl_code_1 = size1;
            u.Size_tbl_code_2 = size2;


            if (bodyItem)
            {
                u.item_assembly = 0;
                u.item_production = 1;
                u.Category_tbl_code = 2;
            }
            else
            {
                u.item_assembly = 1;
                u.item_production = 0;
            }

            u.item_updtd_date = DateTime.Now;
            u.item_updtd_by = MainDashboard.USER_ID;

            //Inserting Data into Database
            bool success = dalItem.SBBItemUpdate(u);
            //If the data is successfully inserted then the value of success will be true else false
            if (!success)
            {
                MessageBox.Show("Failed to update SBB item");

            }
           
        }

        private bool JoinItem(string parentCode, string childCode, int joinMax, int joinMin, int joinQty)
        {
            bool success = true;

            uJoin.join_updated_date = DateTime.Now;
            uJoin.join_updated_by = MainDashboard.USER_ID;
            uJoin.join_added_date = DateTime.Now;
            uJoin.join_added_by = MainDashboard.USER_ID;

            uJoin.join_parent_code = parentCode;

            uJoin.join_child_code = childCode;

            uJoin.join_max = joinMax;
            uJoin.join_min = joinMin;

            uJoin.join_qty = joinQty;

            DataTable dt_existCheck = dalJoin.existCheck(uJoin.join_parent_code, uJoin.join_child_code);

            if (dt_existCheck.Rows.Count > 0)
            {
                //update data
                success = dalJoin.UpdateWithMaxMin(uJoin);

                if (!success)
                {
                    MessageBox.Show("Failed to update join");


                }
            }
            else
            {
                //insert new data
                success = dalJoin.InsertWithMaxMin(uJoin);

                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show("Failed to add new join");
                }


            }

            return success;
        }

        private bool SaveCustomerPriceData(int custTblCode,string itemCode,decimal price)
        {
            //insert or update
            DataTable dt_CustPrice = dalSBB.DiscountSelect();

            decimal Discount = 80;

            foreach (DataRow priceRow in dt_CustPrice.Rows)
            {
                int db_CustTblCode = int.TryParse(priceRow[dalSBB.CustTblCode].ToString(), out db_CustTblCode) ? db_CustTblCode : -2;
                if (db_CustTblCode == custTblCode)
                {
                    //get table code
                    Discount = decimal.TryParse(priceRow[dalSBB.DiscountRate].ToString(), out Discount) ? Discount : 80;
                    break;
                }
            }

            uSBB.Updated_Date = DateTime.Now;
            uSBB.Updated_By = MainDashboard.USER_ID;
            uSBB.Customer_tbl_code = custTblCode;

            //get data
            uSBB.Item_tbl_code = itemCode;
            uSBB.Unit_price = price;
            uSBB.Discount_rate = Discount;

            //update or insert
            bool dataSaved = false;

            if (dt_CustPrice != null && dt_CustPrice.Rows.Count > 0)
            {
                foreach (DataRow priceRow in dt_CustPrice.Rows)
                {
                    int db_CustTblCode = int.TryParse(priceRow[dalSBB.CustTblCode].ToString(), out db_CustTblCode) ? db_CustTblCode : -2;
                    if (uSBB.Item_tbl_code == priceRow[dalSBB.ItemTblCode].ToString() && db_CustTblCode == custTblCode)
                    {
                        //get table code
                        int tableCode = int.TryParse(priceRow[dalSBB.TableCode].ToString(), out tableCode) ? tableCode : -1;
                        uSBB.Table_Code = tableCode;

                        if (tableCode != -1)
                        {
                            if (!dalSBB.DiscountUpdate(uSBB))
                            {
                                MessageBox.Show("Failed to update customer discount info!");
                                return false;
                            }
                            else
                            {

                                dataSaved = true;
                            }

                            break;
                        }
                        else
                        {
                            MessageBox.Show("Failed to get table code of customer discount table!");
                            break;
                        }
                        //updated data


                    }
                }
            }

            if (!dataSaved)
            {
                //insert data
                if (!dalSBB.InsertDiscount(uSBB))
                {
                    MessageBox.Show("Failed to insert customer discount info!");
                    return false;
                }
                else
                {
                    dataSaved = true;
                }
            }

            return true;
        }

        private void SBBItemUpload()
        {
            //string path = text.CSV_Jun_GoogleDrive_SemenyihServer_Path;
            ////DataTable dt_ItemPrice = dalSBB.PriceSelect();
            ////DataTable dt_StdPacking = dalSBB.StdPackingSelect();
            ////DataTable dt_SBBCust = dalSBB.CustomerWithoutRemovedDataSelect();

            ////if (MainDashboard.myconnstrng == text.DB_Semenyih)//MainDashboard.myconnstrng == text.DB_Semenyih || cmbDataSource.Text.Contains(DataSource_LocalDB)
            ////{
            ////    path = text.CSV_SemenyihServer_Path;

            ////}

            //string fileName = "SBBItemUpload.csv";

            //if (Directory.Exists(path) && File.Exists(path + fileName))
            //{
            //    dt_SBBItemUpload = CsvToDatatable(path + fileName, true);


            //    string itemCust = text.SPP_BrandName;
            //    DataTable dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);
            //    //DataTable dt_Product = dalItemCust.SBBItemSelect(itemCust); 
            //    // DataTable dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);
            //    DataTable dt_Item = dalItem.Select();
            //    DataTable dt_Join = dalJoin.SelectWithChildCat();

            //    foreach (DataRow row in dt_SBBItemUpload.Rows)
            //    {
            //        string itemCode = row[dalSBB.ItemCode].ToString();

            //        if(string.IsNullOrEmpty(itemCode))
            //        {
            //            break;
            //        }
            //        else
            //        {
            //            string itemName = row[dalSBB.ItemName].ToString();
            //            string _itemSize_1_TblCode = row["size_tbl_code_1"].ToString();
            //            string _itemSize_2_TblCode = row["size_tbl_code_2"].ToString();

            //            string _qtyPerPacket = row["qty_per_packet"].ToString();
            //            string _qtyPerBag = row["qty_per_bag"].ToString();
            //            string _CategoryTblCode = row["category_tbl_code"].ToString();
            //            string _type_tbl_code = row["type_tbl_code"].ToString();
            //            string _price = row["price"].ToString();
            //            string _benton_price = row["benton_price"].ToString();
            //            string body_code = row["body_code"].ToString();
            //            string body_name = row["body_name"].ToString();

            //            string cap = row["cap"].ToString();
            //            string grip = row["grip"].ToString();
            //            string bush = row["bush"].ToString();
            //            string oring = row["oring"].ToString();

            //            string packet = row["packet"].ToString();
            //            string bag = row["bag"].ToString();

            //            int qtyPerPacket = int.TryParse(_qtyPerPacket, out qtyPerPacket) ? qtyPerPacket : 0;
            //            int qtyPerBag = int.TryParse(_qtyPerBag, out qtyPerBag) ? qtyPerBag : 0;

            //            int CategoryTblCode = int.TryParse(_CategoryTblCode, out CategoryTblCode) ? CategoryTblCode : 0;
            //            int type_tbl_code = int.TryParse(_type_tbl_code, out type_tbl_code) ? type_tbl_code : 0;

            //            int itemSize_1_TblCode = int.TryParse(_itemSize_1_TblCode, out itemSize_1_TblCode) ? itemSize_1_TblCode : 0;
            //            int itemSize_2_TblCode = int.TryParse(_itemSize_2_TblCode, out itemSize_2_TblCode) ? itemSize_2_TblCode : 0;

            //            decimal price = decimal.TryParse(_price, out price) ? price : 0;
            //            decimal benton_price = decimal.TryParse(_benton_price, out benton_price) ? benton_price : 0;

            //            #region Add to Item List
            //            bool itemFound = false, bodyFound = false;
            //            //insert to item list if not exist
            //            foreach (DataRow itemRow in dt_Item.Rows)
            //            {
            //                if (itemRow[dalItem.ItemCode].ToString().Equals(itemCode))
            //                {
            //                    itemFound = true;

            //                }

            //                if (itemRow[dalItem.ItemCode].ToString().Equals(body_code))
            //                {
            //                    bodyFound = true;

            //                }

            //                if (itemFound && bodyFound)
            //                {
            //                    break;
            //                }
            //            }

            //            if (!itemFound)
            //            {
            //                //add goods item to item list
            //                insertItem(itemCode, itemName, false, itemSize_1_TblCode, itemSize_2_TblCode, CategoryTblCode, type_tbl_code);
            //            }
            //            else
            //            {
            //                updateItem(itemCode, itemName, false, itemSize_1_TblCode, itemSize_2_TblCode, CategoryTblCode, type_tbl_code);

            //            }
            //            if (!bodyFound)
            //            {
            //                //add body item to item list
            //                insertItem(body_code, body_name, true, itemSize_1_TblCode, itemSize_2_TblCode, CategoryTblCode, type_tbl_code);

            //            }
            //            else
            //            {
            //                updateItem(body_code, body_name, true, itemSize_1_TblCode, itemSize_2_TblCode, CategoryTblCode, type_tbl_code);

            //            }

            //            #endregion

            //            #region Create Join Group

            //            JoinItem(itemCode, cap, 1, 1, 1);
            //            JoinItem(itemCode, grip, 1, 1, 1);
            //            JoinItem(itemCode, bush, 1, 1, 1);
            //            JoinItem(itemCode, oring, 1, 1, 1);

            //            if (!itemCode.Equals("(OK) CFFTA 20 1/2"))
            //                JoinItem(itemCode, body_code, 1, 1, 1);

            //            JoinItem(itemCode, packet, qtyPerPacket, qtyPerPacket, 1);
            //            JoinItem(itemCode, bag, qtyPerBag, qtyPerBag, 1);

            //            #endregion

            //            #region Std Packing

            //            uSBB.Updated_Date = DateTime.Now;
            //            uSBB.Updated_By = MainDashboard.USER_ID;
            //            uSBB.Max_Lvl = qtyPerBag;
            //            uSBB.Qty_Per_Packet = qtyPerPacket;
            //            uSBB.Qty_Per_Bag = qtyPerBag;
            //            uSBB.Item_code = itemCode;


            //            int tableCode = -1;
            //            foreach (DataRow packingRow in dt_StdPacking.Rows)
            //            {
            //                if (packingRow[dalSBB.ItemCode].ToString() == uSBB.Item_code)
            //                {
            //                    tableCode = int.TryParse(packingRow[dalSBB.TableCode].ToString(), out tableCode) ? tableCode : -1;
            //                }
            //            }

            //            if (tableCode > 0)//update data
            //            {
            //                uSBB.Table_Code = tableCode;
            //                dalSBB.StdPackingUpdate(uSBB);
            //            }
            //            else
            //            {
            //                dalSBB.InsertStdPacking(uSBB);
            //            }

            //            #endregion

            //            #region Add Price

            //            uSBB.Item_tbl_code = itemCode;
            //            uSBB.Default_price = price;
            //            uSBB.Default_discount = 80;

            //            //update or insert
            //            bool dataSaved = false;

            //            if (dt_ItemPrice != null && dt_ItemPrice.Rows.Count > 0)
            //            {
            //                foreach (DataRow priceRow in dt_ItemPrice.Rows)
            //                {
            //                    if (uSBB.Item_tbl_code == priceRow[dalSBB.ItemTblCode].ToString())
            //                    {
            //                        //get table code
            //                        tableCode = int.TryParse(priceRow[dalSBB.TableCode].ToString(), out tableCode) ? tableCode : -1;
            //                        uSBB.Table_Code = tableCode;
            //                        if (tableCode != -1)
            //                        {
            //                            if (!dalSBB.PriceUpdate(uSBB))
            //                            {
            //                                MessageBox.Show("Failed to update item default price info!");
            //                            }
            //                            else
            //                            {

            //                                dataSaved = true;
            //                            }

            //                            break;
            //                        }
            //                        else
            //                        {
            //                            MessageBox.Show("Failed to get table code of item default price table!");
            //                            break;
            //                        }
            //                        //updated data


            //                    }
            //                }
            //            }

            //            if (!dataSaved)
            //            {
            //                //insert data
            //                if (!dalSBB.InsertPrice(uSBB))
            //                {
            //                    MessageBox.Show("Failed to insert item default price info!");
            //                }
            //                else
            //                {
            //                    dataSaved = true;
            //                }
            //            }
            //            #endregion

            //            #region Customer Price

            //            foreach (DataRow CustRow in dt_SBBCust.Rows)
            //            {

            //                int custTblCode = int.TryParse(CustRow[dalSBB.TableCode].ToString(), out custTblCode) ? custTblCode : 0;

            //                if (custTblCode == 17)//benton
            //                {
            //                    SaveCustomerPriceData(custTblCode, itemCode, benton_price);

            //                }
            //                else
            //                {
            //                    SaveCustomerPriceData(custTblCode, itemCode, price);

            //                }

            //            }

            //            #endregion
            //        }


            //    }
            //}


        }

        private int ItemStdPackingFound(DataTable dt,string itemCode)
        {
            foreach(DataRow row in dt.Rows)
            {
                if(itemCode.Equals(row[dalSBB.ItemCode].ToString()))
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
                uSBB.IsRemoved =false;
                uSBB.Qty_Per_Packet = 0;


                foreach (DataRow row in dt_SBBItemUpload.Rows)
                {
                    string itemCode = row["CODE"].ToString();

                    //check if item code exist
                    if(!string.IsNullOrEmpty(tool.getItemNameFromDataTable(dt_Item, itemCode)))
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
                            if(!dalSBB.StdPackingBagContainerUpdate(uSBB))
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

                if(trfDate != DateTime.MaxValue && trfDate.Date >= start.Date && trfDate.Date <= end.Date)
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
            dt_DOWithTrfInfoSelectedPeriod = frmLogIn.dt_DOWithTrfInfoSelectedPeriod;
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

            dt_SBBCustWithoutRemovedDataSelect = dalSBB.CustomerWithoutRemovedDataSelect();//84

            dt_PendingPOSelect = dalSBB.SBBPagePendingPOSelect();//456


            dt_JoinSelectWithChildCat = dalJoin.SelectWithChildCat();//98
            dt_SBBItemSelect = dalItemCust.SBBItemSelect(itemCust);//49
            dt_POSelectWithSizeAndType = dalSBB.SBBPagePOSelectWithSizeAndType();//60

            dt_DOWithTrfInfoSelectedPeriod = dalSBB.SBBPageDOWithTrfInfoSelect(start, end);//765

            dt_Stock = dalStock.StockDataSelect();


            dt_SBBCustSearchWithTypeAndSize = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);//26
            dt_SBBCustSearchWithTypeAndSize.DefaultView.Sort = dalSBB.TypeName + " ASC," + dalSBB.SizeNumerator + " ASC," + dalSBB.SizeWeight + "1 ASC";
            dt_SBBCustSearchWithTypeAndSize = dt_SBBCustSearchWithTypeAndSize.DefaultView.ToTable();

            if(dt_Item == null || dt_Item.Rows.Count < 0)
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

        private void frmSBB_Load(object sender, EventArgs e)
        {
            //TempChildStdPackingUpload();

            SuspendLayout();
            //SBBItemUpload();

            //ShowDataSourceUI();
            dtpDate1.Value = DateTime.Now;

            LoadTypeCMB(cmbType);
            LoadStockLocationCMB(cmbStockLocation);

            RefreshPage();

            Loaded = true;

            dgvStockAlert.ClearSelection();
            dgvUsage.ClearSelection();

            //var now = DateTime.Now;
            //var startOfMonth = new DateTime(now.Year, now.Month, 1);
            
            ResumeLayout();
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
                Percentage = (double)deliverd / totalBagDelivered * 100;

                lblTopCustomer_Bag_10.Text += " (" + Percentage.ToString("0.#") + "%)";
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

                    if(QtyPerBag == 0)
                    {
                        QtyPerBag = 1;
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

                if(stdPacking > 0)
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


        private void ShowData(DataTable dt)
        {
            dt.DefaultView.Sort = header_CustID + " ASC";
            dt = dt.DefaultView.ToTable();
            dt.AcceptChanges();

            DataTable dt_Merge = dt.Copy();


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

            var ORingDeliveredInfo = GetOringDeliveredQty();

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

                        deliveredBag = excludeORingDeliveredBagQty.ToString();

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

            lblTopCust_10.Text = "OTHER";

            if(otherBagDelivered - otherORingDelivered < 0)
            {
                lblTopCustomer_Bag_10.Text = "0";

            }
            else
            {
                lblTopCustomer_Bag_10.Text = (otherBagDelivered - otherORingDelivered).ToString();

            }

            if (!string.IsNullOrEmpty(otherbal))
            {
                lblTopCustomer_Bal_10.Text = otherbal;
            }
            else
            {
                lblTopCustomer_Bal_10.Text = "";
            }

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

            lblDeliveredBalance.Text = balDelivered;
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
            if(dt_DeliveredReport != null && dt_DeliveredReport.Rows.Count > 0)
            ShowData(dt_DeliveredReport);
            //105
        }

        private Tuple<DataTable, int> GetOringDeliveredQty()
        {
            DataTable dt = NewDeliveredTable();
            int deliveredBag = 0;

            DataTable dt_DOList = dalSBB.DOWithTrfInfoSelect(MonthlyDateStart.ToString("yyyy/MM/dd"), MonthlyDateEnd.ToString("yyyy/MM/dd"));

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

            LoadDeliveredData();

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
            var result2 = LoadPendingDOQty();//729
            lblPendingDO.Text = result2.Item1.ToString();
            lblPendingDOCust.Text = result2.Item2.ToString();
            lblDOBagQty.Text = result2.Item3.ToString();//574
        }

        private void RefreshPage()
        {
            NewInitialDBData();
         
            LoadPendingSummary();//923->283

            LoadStockAlert();//166

            LoadUsage();//1421->564

        }

        private DataTable LoadUsageItemList()
        {
            //DataTable dt_Item = dalItem.Select();
            //string itemCust = text.SPP_BrandName;
            //DataTable dt_SBBItemSelect = dalItemCust.SBBItemSelect(itemCust);

            DataTable dt_UsageItemList = NewUsageTable();
            DataTable dt_ChildPart = NewUsageTable();

            DataTable dt_ChildMat = new DataTable();

            dt_ChildMat.Columns.Add(dalSBB.ItemCode, typeof(string));
            dt_ChildMat.Columns.Add(dalSBB.ItemName, typeof(string));

            DataTable dt_Packaging = new DataTable();

            dt_Packaging.Columns.Add(dalSBB.ItemCode, typeof(string));
            dt_Packaging.Columns.Add(dalSBB.ItemName, typeof(string));

            foreach (DataRow row in dt_SBBItemSelect.Rows)
            {
                string parentCode = row[dalSBB.ItemCode].ToString();

                if (!string.IsNullOrEmpty(parentCode) && parentCode.Length > 7 && parentCode[7].ToString() == "E" && parentCode[8].ToString() != "C")
                {
                    parentCode = GetChildCode(dt_JoinSelectWithChildCat, parentCode);
                }

                //if parentCode is production part

                foreach (DataRow join in dt_JoinSelectWithChildCat.Rows)
                {
                    if (parentCode == join[dalJoin.ParentCode].ToString())
                    {
                        string childCat = join[dalJoin.ChildCat].ToString();
                        string childCode = join[dalJoin.ChildCode].ToString();
                        string childName = join[dalJoin.ChildName].ToString();

                        string childMat = join[dalItem.ItemMaterial].ToString();
                        string childRecycle = join[dalItem.ItemRecycleMat].ToString();
                        string childColorMat = join[dalItem.ItemMBatch].ToString();


                        bool childPartDuplicate = false;

                        if (dt_ChildPart != null && dt_ChildPart.Rows.Count > 0)
                        {
                            foreach (DataRow part in dt_ChildPart.Rows)
                            {
                                if (childCode == part[header_ItemCode].ToString())
                                {
                                    childPartDuplicate = true;
                                    break;
                                }
                            }
                        }

                        if (!childPartDuplicate && childCat == text.Cat_Part)
                        {
                            DataRow newRow = dt_ChildPart.NewRow();

                            newRow[header_ItemCode] = childCode;
                            newRow[header_ItemName] = childName;

                            dt_ChildPart.Rows.Add(newRow);

                            bool matFound = false;
                            bool recycleFound = false;
                            bool colorMatFound = false;

                            foreach (DataRow part in dt_ChildMat.Rows)
                            {
                                if (childMat == part[dalSBB.ItemCode].ToString())
                                {
                                    matFound = true;
                                }

                                if (childRecycle == part[dalSBB.ItemCode].ToString())
                                {
                                    recycleFound = true;
                                }

                                if (childColorMat == part[dalSBB.ItemCode].ToString())
                                {
                                    colorMatFound = true;
                                }
                            }

                            if(!matFound && !string.IsNullOrEmpty(childMat))
                            {
                                newRow = dt_ChildMat.NewRow();

                                newRow[dalSBB.ItemCode] = childMat;
                                newRow[dalSBB.ItemName] = tool.getItemNameFromDataTable(dt_Item,childMat);

                                dt_ChildMat.Rows.Add(newRow);
                            }

                            if (!recycleFound && !string.IsNullOrEmpty(childRecycle))
                            {
                                newRow = dt_ChildMat.NewRow();

                                newRow[dalSBB.ItemCode] = childRecycle;
                                newRow[dalSBB.ItemName] = tool.getItemNameFromDataTable(dt_Item, childRecycle);

                                dt_ChildMat.Rows.Add(newRow);
                            }

                            if (!colorMatFound && !string.IsNullOrEmpty(childColorMat))
                            {
                                newRow = dt_ChildMat.NewRow();

                                newRow[dalSBB.ItemCode] = childColorMat;
                                newRow[dalSBB.ItemName] = tool.getItemNameFromDataTable(dt_Item, childColorMat);

                                dt_ChildMat.Rows.Add(newRow);
                            }
                        }
                        else if(childCat != text.Cat_Part)
                        {
                            bool otherMaterialFound = false;

                            foreach (DataRow part in dt_Packaging.Rows)
                            {
                                if (childCode == part[dalSBB.ItemCode].ToString())
                                {
                                    otherMaterialFound = true;
                                }

                            }

                            if (!otherMaterialFound && !string.IsNullOrEmpty(childCode))
                            {
                                DataRow newRow = dt_Packaging.NewRow();

                                newRow[dalSBB.ItemCode] = childCode;
                                newRow[dalSBB.ItemName] = childName;

                                dt_Packaging.Rows.Add(newRow);
                            }
                        }
                    }
                }


            }

            if (dt_ChildPart != null && dt_ChildPart.Rows.Count > 0 && cbParts.Checked)
            {
                foreach (DataRow row in dt_ChildPart.Rows)
                {
                    DataRow newRow = dt_UsageItemList.NewRow();

                    newRow[header_ItemCode] = row[header_ItemCode];
                    newRow[header_ItemName] = row[header_ItemName];

                    dt_UsageItemList.Rows.Add(newRow);
                }
            }

            if (dt_ChildMat != null && dt_ChildMat.Rows.Count > 0 && cbMat.Checked)
            {
                foreach(DataRow row in dt_ChildMat.Rows)
                {
                    DataRow newRow = dt_UsageItemList.NewRow();

                    newRow[header_ItemCode] = row[dalSBB.ItemCode];
                    newRow[header_ItemName] = row[dalSBB.ItemName];

                    dt_UsageItemList.Rows.Add(newRow);
                }
            }

            if (dt_Packaging != null && dt_Packaging.Rows.Count > 0 && cbPackaging.Checked)
            {
                foreach (DataRow row in dt_Packaging.Rows)
                {
                    DataRow newRow = dt_UsageItemList.NewRow();

                    newRow[header_ItemCode] = row[dalSBB.ItemCode];
                    newRow[header_ItemName] = row[dalSBB.ItemName];

                    dt_UsageItemList.Rows.Add(newRow);
                }
            }

            return dt_UsageItemList;
        }

        private void LoadUsage()
        {
            Cursor = Cursors.WaitCursor;

            #region indicate start and end date

            string start;
            string end;

            DateTime dateStart = dtpDate1.Value;
            DateTime dateEnd = dtpDate2.Value;

            if(dateStart > dateEnd)
            {
                dateStart = dtpDate2.Value;
                dateEnd = dtpDate1.Value;
            }
            start = dateStart.ToString("yyyy/MM/dd");
            end = dateEnd.ToString("yyyy/MM/dd");

            int totalDay = tool.getNumberOfDayBetweenTwoDate(dateStart, dateEnd, false);
            dt_TrfRangeUsageSearch = dalTrfHist.SBBPageRangeUsageSearch(start, end);
            //if (Loaded)
            //{
            //    dt_TrfRangeUsageSearch = dalTrfHist.SBBPageRangeUsageSearch(start, end);

            //}
            //else
            //{
            //    dt_TrfRangeUsageSearch = frmLogIn.dt_TrfRangeUsageSearch;

            //}

            #endregion

            DataTable dt_ItemList = LoadUsageItemList();
            dt_ItemList = RearrangeIndex(dt_ItemList);

            //dt_TrfRangeUsageSearch = dalTrfHist.RangeUsageSearch(start, end);

            foreach(DataRow row in dt_ItemList.Rows)
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
                        int trfQty = int.TryParse(trfRow[dalTrfHist.TrfQty].ToString(),out  trfQty) ? trfQty : 0;

                        string trfFrom = trfRow[dalTrfHist.TrfFrom].ToString();
                        string trfTo = trfRow[dalTrfHist.TrfTo].ToString();

                        if(trfFrom == text.Production || trfTo == text.Production)
                        {
                            totPro += trfQty;
                        }

                        if (trfTo == text.Assembly)
                        {
                            totAssy += trfQty;
                        }

                        row[header_TotalProduction] = totPro;
                        row[header_TotalAssembly] = totAssy;
                        row[header_AVGAssembly] = totAssy / totalDay;
                    }
                }
            }

           
            Cursor = Cursors.Arrow;

            dgvUsage.DataSource = dt_ItemList;

            DgvUIEdit(dgvUsage);

            dgvUsage.ClearSelection();
        }

        public static void Reload()
        {
            _instance.RefreshPage();
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


            int count = Application.OpenForms.OfType<frmSBBDOList>().Count();


            if (count == 1)
            {
                //Form is already open
                Application.OpenForms.OfType<frmSBBDOList>().First().BringToFront();
                Application.OpenForms.OfType<frmSBBDOList>().First().WindowState = FormWindowState.Normal;
            }
            else
            {
                // Form is not open
                frmSBBDOList frm = new frmSBBDOList
                {
                    StartPosition = FormStartPosition.CenterScreen
                };


                frm.Show();
            }



        }

        private void WorkInProgressMessage()
        {
            MessageBox.Show("work in progress...");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void WorkInProgress_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
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

        private void OpenDeliveryPlanning(object sender, EventArgs e)
        {
            WorkInProgressMessage();

            //frmDeliverySchedule frm = new frmDeliverySchedule
            //{
            //    StartPosition = FormStartPosition.CenterScreen
            //};

            //frm.ShowDialog();

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

            if (dgv.Columns[col].Name == header_Stock ||  dgv.Columns[col].Name == header_BalAfter || dgv.Columns[col].Name == header_BalAfterBag || dgv.Columns[col].Name == header_BalAfterPcs)
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
                    if (cmbType.Text == Type_Product)
                        cbInBagUnit.Checked = false;

                    //LoadStockAlert();
                    LoadStockAlert();
                }
                else
                {
                    if (cmbType.Text == Type_Product)
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

                    if (cmbType.Text == Type_Product)
                    {
                        cbInPcsUnit.Checked = false;
                        //LoadStockAlert();
                        LoadStockAlert();
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
            if (Loaded)
            {
                dgvStockAlert.DataSource = null;

                if (cmbType.Text == Type_Product)
                {
                    cbInBagUnit.Visible = true;
                    cbInPcsUnit.Enabled = true;

                    btnStockCheck.Enabled = false;
                }
                else
                {
                    cbInPcsUnit.Checked = true;
                    cbInBagUnit.Checked = false;
                    cbInPcsUnit.Enabled = false;
                    cbInBagUnit.Visible = false;

                    btnStockCheck.Enabled = true;

                }
                //LoadStockAlert();
                LoadStockAlert();
            }

        }

        private void StockCheckUIMode(bool turnOn)
        {
            DataGridView dgv = dgvStockAlert;

            if (turnOn)
            {
                tlpStockAlert.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpStockAlert.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f);
                dgv.Columns[header_ItemName].Frozen = true;
                cmbStockLocation.Enabled = false;
                stockCheckMode = true;

                btnCancelStockCheck.Visible = true;
                btnStockCheck.Enabled = false;

                btnStockCheck.Text = "TALLY ALL";
                lblStockAlert.Text = "Stock Check";

                btnStockRefresh.Visible = false;
                cmbType.Visible = false;
                cbInPcsUnit.Visible = false;
                cbInBagUnit.Visible = false;

                dgv.Columns[header_BalAfter].Visible = false;
                dgv.Columns[header_BalAfterBag].Visible = false;
                dgv.Columns[header_BalAfterPcs].Visible = false;

                dgv.Columns[header_ProDaysNeeded].Visible = false;

                dgv.Columns[header_ActualStock].Visible = true;
                dgv.Columns[header_StockDiff].Visible = true;

                dgv.Columns[header_StdPacking_Bag].Visible = false;
                dgv.Columns[header_StdPacking_Ctn].Visible = false;
                dgv.Columns[header_StdPacking_String].Visible = false;
                dgv.Columns[header_ActualStock_PCS].Visible = false;
                dgv.Columns[header_ActualStock_BAG].Visible = false;
                dgv.Columns[header_ActualStock_CTN].Visible = false;

                //DataTable dt = (DataTable)dgv.DataSource;

                //dt.DefaultView.Sort = header_ItemName + " asc";
                //dt = dt.DefaultView.ToTable();
            }
            else
            {
                cmbStockLocation.Enabled = true;
                dgv.Columns[header_ItemName].Frozen = false;
                tlpStockAlert.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 40f);
                tlpStockAlert.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 184f);
                dgv.Columns[header_ActualStock].Visible = false;
                dgv.Columns[header_StockDiff].Visible = false;

                dgv.Columns[header_StdPacking_Bag].Visible = false;
                dgv.Columns[header_StdPacking_Ctn].Visible = false;
                //dgv.Columns[header_StdPacking_String].Visible = false;
                dgv.Columns[header_ActualStock_PCS].Visible = false;
                dgv.Columns[header_ActualStock_BAG].Visible = false;
                dgv.Columns[header_ActualStock_CTN].Visible = false;

                dgv.Columns[header_BalAfter].Visible = true;

                dgv.Columns[header_ProDaysNeeded].Visible = true;

                stockCheckMode = false;

                btnCancelStockCheck.Visible = false;

                btnStockCheck.Enabled = true;

                btnStockCheck.Text = "Stock Check";
                lblStockAlert.Text = "Stock Alert";

                btnStockRefresh.Visible = true;
                cmbType.Visible = true;
                cbInPcsUnit.Visible = true;
                cbInBagUnit.Visible = true;

                //change table 
                //LoadStockAlert();
                LoadStockAlert();

            }
        }

        private void LoadProductionPlanning(string itemCode, string itemName)
        {
            frmSBBProductionPlanning frm = new frmSBBProductionPlanning(itemName,itemCode)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.Show();
        }

        private void btnLOAD_Click(object sender, EventArgs e)
        {
            if (lblStockAlert.Text == "Stock Alert")
                StockCheckUIMode(true);
            else
            {
                string stockLocation = cmbStockLocation.Text;

                if(string.IsNullOrEmpty(stockLocation))
                {
                    stockLocation = text.Factory_Semenyih;
                    cmbStockLocation.Text = stockLocation;

                }

                if(stockLocation != "ALL")
                {
                    frmInOutEdit frm = new frmInOutEdit((DataTable)dgvStockAlert.DataSource, true, true, stockLocation);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit

                    if (frmInOutEdit.TrfSuccess)
                    {
                        StockCheckUIMode(false);
                    }
                }
                else
                {
                    MessageBox.Show("Stock Tally Location Invalid!");
                }
            
            }
            dgvStockAlert.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            LoadStockAlert();
        }

        private void tableLayoutPanel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancelStockCheck_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cancel will lose all the unsaved changes.\nAre you sure you want to cancel stock check?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                StockCheckUIMode(false);

            }
            dgvStockAlert.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void dgvStockAlert_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = dgvStockAlert;

            if (e.RowIndex != -1 && stockCheckMode)
            {
                int col = e.ColumnIndex;
                int row = e.RowIndex;

                if (dgv.Columns[col].Name == header_ActualStock)
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = false;
                }
                else
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = true;
                }
            }
        }

        private void dgvStockAlert_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //compare old value with new value
            DataGridView dgv = dgvStockAlert;

            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            int actualStock = int.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out actualStock) ? actualStock : 0;
            int systemStock = int.TryParse(dgv.Rows[rowIndex].Cells[header_Stock].Value.ToString(), out systemStock) ? systemStock : 0;

            if (rowIndex > -1 && dgv.Columns[colIndex].Name.Contains(header_ActualStock))
            {
                int stockDiff = actualStock - systemStock;

                dgv.Rows[rowIndex].Cells[header_StockDiff].Value = stockDiff;

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


        private bool StockDiffExist()
        {
            if (stockCheckMode)
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

                if (dgv.Columns[colIndex].Name.Contains(header_ActualStock) && stockCheckMode) //Desired Column
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
            DataGridView dgv = dgvUsage;

            dgv.SuspendLayout();
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == header_BalAfter)
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

            dgv.ResumeLayout();
        }

        private void dgvStockAlert_Sorted(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)dgvStockAlert.DataSource;

            //dt = RearrangeIndex(dt);

            //dgvStockAlert.DataSource = dt;

            dgvStockAlert.ClearSelection();
        }

        private void dtpDate1_ValueChanged(object sender, EventArgs e)
        {
            if(Loaded)
            {
               
                LoadUsage();

            }
        }

        private void dtpDate2_ValueChanged(object sender, EventArgs e)
        {
            if (Loaded)
                LoadUsage();
        }

        private void frmSBB_Shown(object sender, EventArgs e)
        {
            dgvStockAlert.ClearSelection();
            dgvUsage.ClearSelection();
        }

        private void cbParts_CheckedChanged(object sender, EventArgs e)
        {
            if(Loaded)
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
            if(lblStockAlert.Text == "Stock Alert")
            {
                DataGridView dgv = dgvStockAlert;

                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0 )
                {
                    try
                    {
                        string itemCode = dgv.Rows[rowIndex].Cells[header_ItemCode].Value.ToString();
                        string itemName = dgv.Rows[rowIndex].Cells[header_ItemName].Value.ToString();

                        if(tool.getItemCat(itemCode) == text.Cat_Part)
                        {
                            LoadProductionPlanning(itemCode, itemName);

                        }
                    }
                    catch (Exception ex)
                    {
                        tool.saveToTextAndMessageToUser(ex);
                    }
                }
            }
           
        }

        private void lblMonthlyDelivered_Click(object sender, EventArgs e)
        {
            if(!DeliveredBags_FullScreen)
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
            if(Loaded)
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
           

            frmChangeDate frm = new frmChangeDate(MonthlyDateStart,MonthlyDateEnd);
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
            if(DeliveredBags_FullScreen)
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
            if(Loaded)
            {
                string stockLocation = cmbStockLocation.Text;

                if (stockLocation.Equals("ALL"))
                {
                    btnStockCheck.Enabled = false;
                }
                else
                {
                    btnStockCheck.Enabled = true;
                }


                header_Stock = "STOCK ";
                if (string.IsNullOrEmpty(stockLocation))
                {
                    stockLocation = text.Factory_Semenyih;
                    cmbStockLocation.Text = text.Factory_Semenyih;

                }

                header_Stock += stockLocation + " (PCS / KG)";

                LoadStockAlert();//166
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
    }
}
