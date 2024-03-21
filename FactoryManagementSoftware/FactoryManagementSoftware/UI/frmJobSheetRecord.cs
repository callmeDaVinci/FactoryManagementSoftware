using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmJobSheetRecord : Form
    {
        public frmJobSheetRecord()
        {
            InitializeComponent();

            dt_ItemInfo = dalItem.Select();
            dt_JoinInfo = dalJoin.SelectAll();
            dt_Mac = dalMac.Select();
        }

        public frmJobSheetRecord(DataTable dt_JobList, int jobListRowIndex, DataTable dt_JobRecordHistory)
        {
            InitializeComponent();

            DT_JOB_LIST = dt_JobList;
            JOB_LIST_SELECTED_ROW_INDEX = jobListRowIndex;

            DT_JOB_RECORD = dt_JobRecordHistory;

            dt_ItemInfo = dalItem.Select();
            dt_JoinInfo = dalJoin.SelectAll();
            dt_Mac = dalMac.Select();

            NewJobSheet();
        }

        public frmJobSheetRecord(DataTable dt_JobList, int jobListRowIndex, DataTable dt_JobRecordHistory, int jobRecordRowIndex)
        {
            //Load Existing Job Sheet Record

            InitializeComponent();

            DT_JOB_LIST = dt_JobList;
            JOB_LIST_SELECTED_ROW_INDEX = jobListRowIndex;

            DT_JOB_RECORD = dt_JobRecordHistory;
            JOB_RECORD_SELECTED_ROW_INDEX = jobRecordRowIndex;

            dt_ItemInfo = dalItem.Select();
            dt_JoinInfo = dalJoin.SelectAll();
            dt_Mac = dalMac.Select();

            LoadJobSheet();

          
        }
       
        private int JOB_LIST_SELECTED_ROW_INDEX = -1;
        private int JOB_RECORD_SELECTED_ROW_INDEX = -1;

        Text text = new Text();
        Tool tool = new Tool();


        itemDAL dalItem = new itemDAL();
        planningDAL dalPlan = new planningDAL();
        PlanningBLL uPlan = new PlanningBLL();
        MacDAL dalMac = new MacDAL();
        MacBLL uMac = new MacBLL();
        userDAL dalUser = new userDAL();
        joinDAL dalJoin = new joinDAL();
        joinBLL uJoin = new joinBLL();
        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();
        ProductionRecordBLL uProRecord = new ProductionRecordBLL();
        trfHistDAL dalTrf = new trfHistDAL();

        DataTable dt_Plan;
        DataTable dt_ProductionRecord;
        DataTable dt_ItemInfo;
        DataTable dt_JoinInfo;
        DataTable dt_MultiPackaging;
        DataTable DT_CARTON;
        DataTable dt_MultiParent;
        DataTable dt_Mac;
        // DataTable dt_Trf;

        DateTime OLD_PRO_DATE = DateTime.MaxValue;
        DataTable DT_JOB_LIST = new DataTable();
        DataTable DT_JOB_RECORD = new DataTable();

        private string ITEM_CODE = "";
        private string ITEM_NAME = "";
        private string JOB_NO = "";
        private string MAC_ID = "";
        private string SHEET_ID = "";

        readonly private string string_NewSheet = "NEW";
        readonly private string string_Multi = "MULTI";
        readonly private string string_MultiPackaging = "MULTI PACKAGING";

        private bool DATA_EDITED = false;
        //private DataTable NewPackagingTable()
        //{
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add(text.Header_ItemCode, typeof(string));
        //    dt.Columns.Add(text.Header_ItemName, typeof(string));
        //    dt.Columns.Add(text.Header_Packing_Max_Qty, typeof(int));
        //    dt.Columns.Add(text.Header_Packaging_Qty, typeof(int));

        //    return dt;
        //}

        private DataTable NewCartonTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(text.Header_Qty_Per_Container, typeof(int));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_Container_Qty, typeof(int));
            dt.Columns.Add(text.Header_Container_Stock_Out, typeof(bool));

            return dt;
        }

        //private DataTable NewStockInTable()
        //{
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add(header_Cat, typeof(string));
        //    dt.Columns.Add(header_ProDate, typeof(string));
        //    dt.Columns.Add(header_ItemCode, typeof(string));
        //    dt.Columns.Add(header_From, typeof(string));
        //    dt.Columns.Add(header_To, typeof(string));
        //    dt.Columns.Add(header_Qty, typeof(string));
        //    dt.Columns.Add(text.Header_JobNo, typeof(string));
        //    dt.Columns.Add(header_Shift, typeof(string));

        //    return dt;
        //}

        private void LoadJobData()
        {
            ITEM_CODE = "";
            ITEM_NAME = "";
            JOB_NO = "";
            MAC_ID = "";

            if (DT_JOB_LIST?.Rows.Count > 0 && JOB_LIST_SELECTED_ROW_INDEX > -1 && JOB_LIST_SELECTED_ROW_INDEX <= DT_JOB_LIST.Rows.Count - 1)
            {
                ITEM_CODE = DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_ItemCode].ToString();
                ITEM_NAME = DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_ItemName].ToString();
                JOB_NO = DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_JobNo].ToString();
                MAC_ID = DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_MacID].ToString();

                string itemDescription = tool.getItemNameAndCodeString(ITEM_CODE, ITEM_NAME);

                lblITitle.Text = "(Job No.: " +JOB_NO + ")_" + itemDescription;
            }
        }

        private int CYCLE_TIME = 0;
        private void NewJobSheet()
        {
            LoadJobData();

            txtSheetID.Text = string_NewSheet;

            string rawLotNo = "";
            string colorLotNo = "";
            string latestShift = text.Shift_Morning;
            string latestMeterEnd = "0";
            string latestMorningShiftOperator = "";
            string latestNightShiftOperator = "";
            string latestCavity = "";
            DateTime latestProductionDate = DateTime.MinValue;

            if (DT_JOB_RECORD?.Rows.Count > 0 )
            {
                foreach (DataRow row in DT_JOB_RECORD.Rows)
                {
                    DateTime productionDate = DateTime.TryParse(row[text.Header_ProductionDate].ToString(), out productionDate) ? productionDate : DateTime.Now;

                    if (productionDate > latestProductionDate)
                    {
                        latestProductionDate = productionDate;

                        rawLotNo = row[text.Header_RawMat_Lot_No].ToString();
                        colorLotNo = row[text.Header_ColorMat_Lot_No].ToString();
                        latestShift = row[text.Header_Shift].ToString();
                        latestMeterEnd = row[text.Header_MeterEnd].ToString();
                        latestMorningShiftOperator = row[text.Header_Operator].ToString();
                        latestCavity = row[text.Header_Cavity].ToString();
                    }
                    else if (productionDate == latestProductionDate)
                    {
                        if(row[text.Header_Shift].ToString() == text.Shift_Night)
                        {
                            rawLotNo = row[text.Header_RawMat_Lot_No].ToString();
                            colorLotNo = row[text.Header_ColorMat_Lot_No].ToString();
                            latestShift = row[text.Header_Shift].ToString();
                            latestMeterEnd = row[text.Header_MeterEnd].ToString();
                            latestNightShiftOperator = row[text.Header_Operator].ToString();
                            latestCavity = row[text.Header_Cavity].ToString();

                        }
                    }
                }
            }

            if (DT_JOB_LIST?.Rows.Count > 0 && JOB_LIST_SELECTED_ROW_INDEX > -1 && JOB_LIST_SELECTED_ROW_INDEX <= DT_JOB_LIST.Rows.Count - 1)
            {
                if (latestProductionDate == DateTime.MinValue)
                {
                    latestProductionDate = DateTime.TryParse(DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_DateStart].ToString(), out latestProductionDate)? latestProductionDate : DateTime.Now;
                }

                if (latestCavity == "" || latestCavity == "0" || string.IsNullOrEmpty(latestCavity))
                {
                    latestCavity = DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_Cavity].ToString();
                }

                CYCLE_TIME = int.TryParse(DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_ProCT].ToString(), out CYCLE_TIME)? CYCLE_TIME : 0;
            }


            txtRawMatLotNo.Text = rawLotNo;
            txtColorMatLotNo.Text = colorLotNo;
            txtCavity.Text = latestCavity;
            txtMeterStart.Text = latestMeterEnd;
            txtMeterEnd.Text = "";

            if (latestShift == text.Shift_Morning)
            {
                cbNight.Checked = true;
                txtOperator.Text = latestNightShiftOperator;
            }
            else
            {
                cbMorning.Checked = true;

                txtOperator.Text = latestMorningShiftOperator;

                DateTime nextWorkingDay = latestProductionDate.AddDays(1);

                while (nextWorkingDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    nextWorkingDay = nextWorkingDay.AddDays(1);
                }

                latestProductionDate = nextWorkingDay;
            }

            dtpProDate.Value = latestProductionDate;

            int idealHourlyShot = (int) ((double) 3600 / CYCLE_TIME);

            lblIdealHourlyShot.Text = "Ideal Hourly Shot: " + idealHourlyShot;

            LoadCartonData();

        }

        private int ORIGINAL_TOTAL_STOCK_IN_QTY = -1;

        private void LoadJobSheet()
        {
            //btnSave.Visible = false;

            //tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);
            //tlpButton.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0);

            LoadJobData();

            string sheetID = "";
            string rawLotNo = "";
            string colorLotNo = "";
            string Shift = "";
            string MeterStart = "0";
            string MeterEnd = "0";
            string Operator = "";
            string Cavity = "";
            string FullBox = "";
            string QtyPerBox = "";
            string Remark = "";
            string balanceStockIn = "0";
            string timeStart = "";
            string timeEnd = "";

            DateTime ProductionDate = DateTime.MinValue;

            if (DT_JOB_RECORD?.Rows.Count > 0 && JOB_RECORD_SELECTED_ROW_INDEX > -1 && JOB_RECORD_SELECTED_ROW_INDEX <= DT_JOB_RECORD.Rows.Count - 1)
            {
                ProductionDate = DateTime.TryParse(DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_ProductionDate].ToString(), out ProductionDate) ? ProductionDate : DateTime.Now;
              
                sheetID = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_SheetID].ToString();
                Remark = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_Remark].ToString();

                rawLotNo = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_RawMat_Lot_No].ToString();
                colorLotNo = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_ColorMat_Lot_No].ToString();

                Shift = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_Shift].ToString();
                MeterStart = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_MeterStart].ToString();
                MeterEnd = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_MeterEnd].ToString();
                Operator = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_Operator].ToString();
                Cavity = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_Cavity].ToString();
                FullBox = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_StockIn_Container].ToString();
                QtyPerBox = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_Qty_Per_Container].ToString();
                balanceStockIn = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_StockIn_Balance_Qty].ToString();

                timeStart = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_TimeStart].ToString();
                timeEnd = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_TimeEnd].ToString();
            }

            if (DT_JOB_LIST?.Rows.Count > 0 && JOB_LIST_SELECTED_ROW_INDEX > -1 && JOB_LIST_SELECTED_ROW_INDEX <= DT_JOB_LIST.Rows.Count - 1)
            {
                if (Cavity == "" || Cavity == "0" || string.IsNullOrEmpty(Cavity))
                {
                    Cavity = DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_Cavity].ToString();
                }

                CYCLE_TIME = int.TryParse(DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_ProCT].ToString(), out CYCLE_TIME) ? CYCLE_TIME : 0;
            }

            SHEET_ID = sheetID;
            txtSheetID.Text = sheetID;
            txtRawMatLotNo.Text = rawLotNo;
            txtColorMatLotNo.Text = colorLotNo;
            txtCavity.Text = Cavity;

            txtMeterStart.Text = MeterStart;
            txtMeterEnd.Text = MeterEnd;
            txtOperator.Text = Operator;
            txtNote.Text = Remark;

            txtStockInPcsQty.Text = balanceStockIn;

            dtpProDate.Value = ProductionDate.Date;

            if (Shift == text.Shift_Morning)
            {
                cbMorning.Checked = true;
            }
            else
            {
                cbNight.Checked = true;
              
            }

            LoadCartonData(FullBox,QtyPerBox,balanceStockIn);

            
            if (!string.IsNullOrEmpty(timeStart))
            {
                DateTime parsedDate;
                if (DateTime.TryParse(timeStart, out parsedDate))
                {
                    dtpTimeStart.Value = parsedDate;
                }
            }

            if (!string.IsNullOrEmpty(timeEnd))
            {
                DateTime parsedDate;
                if (DateTime.TryParse(timeEnd, out parsedDate))
                {
                    dtpTimeEnd.Value = parsedDate;
                }
            }

            int idealHourlyShot = (int)((double)3600 / CYCLE_TIME);

            lblIdealHourlyShot.Text = "Ideal Hourly Shot: " + idealHourlyShot;

        }

        private void LoadCartonData()
        {
            string itemCode = ITEM_CODE;
            string sheetID = SHEET_ID;
            int MainCartonQty = 0;

            DT_CARTON = null;

            //Adding New Job Sheet
            if (string.IsNullOrEmpty(sheetID) || sheetID.Equals(string_NewSheet))
            {
                //find main carton from item group and load to dt_carton
                foreach (DataRow row in dt_JoinInfo.Rows)
                {
                    string parentCode = row[dalJoin.JoinParent].ToString();

                    bool mainCarton = bool.TryParse(row[dalJoin.JoinMainCarton].ToString(), out mainCarton) ? mainCarton : false;
                    bool StockOut = bool.TryParse(row[dalJoin.JoinStockOut].ToString(), out StockOut) ? StockOut : true;

                    if (itemCode == parentCode && mainCarton)
                    {
                        MainCartonQty++;

                        //adddata to dt_carton
                        if (DT_CARTON == null)
                        {
                            DT_CARTON = NewCartonTable();
                        }

                        DataRow newRow = DT_CARTON.NewRow();

                        string cartonCode = row[dalJoin.JoinChild].ToString();

                        int MaxQty = int.TryParse(row[dalJoin.JoinMax].ToString(), out MaxQty) ? MaxQty : 0;
                        int childQty = int.TryParse(row[dalJoin.JoinQty].ToString(), out childQty) ? childQty : 0;

                        newRow[text.Header_ItemCode] = cartonCode;
                        newRow[text.Header_ItemName] = tool.getItemName(cartonCode);
                        newRow[text.Header_Qty_Per_Container] = MaxQty;
                        newRow[text.Header_Container_Qty] = 0;
                        newRow[text.Header_Container_Stock_Out] = StockOut;

                        DT_CARTON.Rows.Add(newRow);

                    }
                }
            }
            
            //Editing Job Sheet record
            else
            {
                //load carton data from carton db to dt_carton
                DataTable db_carton = dalProRecord.PackagingRecordSelect(int.TryParse(sheetID, out int i) ? i : 0);

                if (db_carton != null && db_carton.Rows.Count > 0)
                {
                    foreach (DataRow row in db_carton.Rows)
                    {

                        //adddata to dt_carton
                        if (DT_CARTON == null)
                        {
                            DT_CARTON = NewCartonTable();
                        }

                        DataRow newRow = DT_CARTON.NewRow();

                        string cartonCode = row[dalProRecord.PackagingCode].ToString();

                        int MaxQty = int.TryParse(row[dalProRecord.PackagingMax].ToString(), out MaxQty) ? MaxQty : 0;
                        int childQty = int.TryParse(row[dalProRecord.PackagingQty].ToString(), out childQty) ? childQty : 0;

                        bool cartonStockOut = bool.TryParse(row[dalProRecord.PackagingStockOut].ToString(), out cartonStockOut) ? cartonStockOut : true;

                        newRow[text.Header_ItemCode] = cartonCode;
                        newRow[text.Header_ItemName] = tool.getItemName(cartonCode);
                        newRow[text.Header_Qty_Per_Container] = MaxQty;
                        newRow[text.Header_Container_Qty] = childQty;
                        newRow[text.Header_Container_Stock_Out] = cartonStockOut;

                        DT_CARTON.Rows.Add(newRow);
                    }
                }

            }


            string AlertMessage = "";

            totalStockUpdate();

            //check if more than 1 main carton
            if (MainCartonQty > 1)
            {
                AlertMessage = "More than 1 main carton detected!\nYou can edit it at:\n1. Carton Setting\n2. Item Group Edit";
            }

            if (!string.IsNullOrEmpty(AlertMessage))
                MessageBox.Show(AlertMessage, "Carton Alert",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        DataTable DT_ORIGINAL_CARTON = null;

        private void LoadCartonData(string fullBox, string qtyPerBox, string balPcsStockIn)
        {
            string itemCode = ITEM_CODE;
            string sheetID = SHEET_ID;
            int MainCartonQty = 0;

            DT_CARTON = null;

            //Adding New Job Sheet
            if (string.IsNullOrEmpty(sheetID) || sheetID.Equals(string_NewSheet))
            {
                //find main carton from item group and load to dt_carton
                foreach (DataRow row in dt_JoinInfo.Rows)
                {
                    string parentCode = row[dalJoin.JoinParent].ToString();

                    bool mainCarton = bool.TryParse(row[dalJoin.JoinMainCarton].ToString(), out mainCarton) ? mainCarton : false;
                    bool StockOut = bool.TryParse(row[dalJoin.JoinStockOut].ToString(), out StockOut) ? StockOut : true;

                    if (itemCode == parentCode && mainCarton)
                    {
                        MainCartonQty++;

                        //adddata to dt_carton
                        if (DT_CARTON == null)
                        {
                            DT_CARTON = NewCartonTable();
                        }

                        DataRow newRow = DT_CARTON.NewRow();

                        string cartonCode = row[dalJoin.JoinChild].ToString();

                        int MaxQty = int.TryParse(row[dalJoin.JoinMax].ToString(), out MaxQty) ? MaxQty : 0;
                        int childQty = int.TryParse(row[dalJoin.JoinQty].ToString(), out childQty) ? childQty : 0;

                        newRow[text.Header_ItemCode] = cartonCode;
                        newRow[text.Header_ItemName] = tool.getItemName(cartonCode);
                        newRow[text.Header_Qty_Per_Container] = MaxQty;
                        newRow[text.Header_Container_Qty] = 0;
                        newRow[text.Header_Container_Stock_Out] = StockOut;

                        DT_CARTON.Rows.Add(newRow);

                    }
                }
            }

            //Editing Job Sheet record
            else
            {
                //load carton data from carton db to dt_carton
                DataTable db_carton = dalProRecord.PackagingRecordSelect(int.TryParse(sheetID, out int i) ? i : 0);

                if (db_carton != null && db_carton.Rows.Count > 0)
                {
                    foreach (DataRow row in db_carton.Rows)
                    {

                        //adddata to dt_carton
                        if (DT_CARTON == null)
                        {
                            DT_CARTON = NewCartonTable();
                        }

                        DataRow newRow = DT_CARTON.NewRow();

                        string cartonCode = row[dalProRecord.PackagingCode].ToString();

                        int MaxQty = int.TryParse(row[dalProRecord.PackagingMax].ToString(), out MaxQty) ? MaxQty : 0;
                        int childQty = int.TryParse(row[dalProRecord.PackagingQty].ToString(), out childQty) ? childQty : 0;

                        bool cartonStockOut = bool.TryParse(row[dalProRecord.PackagingStockOut].ToString(), out cartonStockOut) ? cartonStockOut : true;

                        newRow[text.Header_ItemCode] = cartonCode;
                        newRow[text.Header_ItemName] = tool.getItemName(cartonCode);
                        newRow[text.Header_Qty_Per_Container] = MaxQty;
                        newRow[text.Header_Container_Qty] = childQty;
                        newRow[text.Header_Container_Stock_Out] = cartonStockOut;

                        DT_CARTON.Rows.Add(newRow);
                    }
                }

                if (DT_CARTON == null || DT_CARTON.Rows.Count <= 0)
                {
                    DT_CARTON = NewCartonTable();

                    DataRow newRow = DT_CARTON.NewRow();

                    int MaxQty = int.TryParse(qtyPerBox, out MaxQty) ? MaxQty : 0;
                    int childQty = int.TryParse(fullBox, out childQty) ? childQty : 0;

                    newRow[text.Header_ItemCode] = "unknown";
                    newRow[text.Header_ItemName] = "unknown";
                    newRow[text.Header_Qty_Per_Container] = MaxQty;
                    newRow[text.Header_Container_Qty] = childQty;
                    newRow[text.Header_Container_Stock_Out] = false;

                    DT_CARTON.Rows.Add(newRow);
                }
            }


            string AlertMessage = "";

            totalStockUpdate();

            //check if more than 1 main carton
            if (MainCartonQty > 1)
            {
                AlertMessage = "More than 1 main carton detected!\nYou can edit it at:\n1. Carton Setting\n2. Item Group Edit";
            }

            if (!string.IsNullOrEmpty(AlertMessage))
                MessageBox.Show(AlertMessage, "Carton Alert",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if(DT_ORIGINAL_CARTON == null && DT_CARTON != null)
            {
                DT_ORIGINAL_CARTON = DT_CARTON.Copy();
            }
        }
        private void ClearAllError()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            errorProvider7.Clear();
            errorProvider8.Clear();
            errorProvider9.Clear();
            errorProvider10.Clear();
        }

        private bool Validation()
        {
            ClearAllError();
            bool passed = true;

            string date = dtpProDate.Value.ToString();

            if (string.IsNullOrEmpty(date))
            {
                errorProvider1.SetError(lblDate, "Please select a production's date");
                passed = false;
            }

            if (CheckIfDateOrShiftDuplicate())
            {
                errorProvider1.SetError(lblDate, "Please select a different date");
                errorProvider2.SetError(lblShift, "Please select a different shift");

                passed = false;
            }

            if (!cbMorning.Checked && !cbNight.Checked)
            {
                errorProvider2.SetError(lblShift, "Please select shift");
                passed = false;
            }

            if (string.IsNullOrEmpty(txtMeterStart.Text))
            {
                errorProvider5.SetError(lblMeterStart, "Please input meter start value");
                passed = false;
            }

            if (string.IsNullOrEmpty(txtMeterStart.Text))
            {
                errorProvider6.SetError(lblMeterEnd, "Please input meter end value");
                passed = false;
            }

            return passed;
        }

        private void UndoPreviousRecordIfExist()
        {
            DateTime proDate = dtpProDate.Value;

            DataTable dt_Trf;

            if (OLD_PRO_DATE == DateTime.MaxValue)
            {
                dt_Trf = dalTrf.rangeTrfSearchWithItemInfo(proDate.ToString("yyyy/MM/dd"), proDate.ToString("yyyy/MM/dd"));

            }
            else
            {
                dt_Trf = dalTrf.rangeTrfSearchWithItemInfo(OLD_PRO_DATE.ToString("yyyy/MM/dd"), OLD_PRO_DATE.ToString("yyyy/MM/dd"));

            }

            string shift = text.Shift_Morning;

            if (cbNight.Checked)
            {
                shift = text.Shift_Night;
            }

            dt_Trf.DefaultView.Sort = dalTrf.TrfDate + " DESC";
            dt_Trf = dt_Trf.DefaultView.ToTable();
            foreach (DataRow row in dt_Trf.Rows)
            {
                string status = row[dalTrf.TrfResult].ToString();

                if (status == "Passed")
                {
                    _ = Convert.ToDateTime(row[dalTrf.TrfDate].ToString());

                    string productionInfo = row[dalTrf.TrfNote].ToString();
                    string _shift = "";
                    string _planID = "";
                    string balanceClearType = "";
                    bool startCopy = false;
                    bool IDCopied = false;
                    bool ShiftCopied = false;
                    bool StopIDCopy = false;

                    for (int i = 0; i < productionInfo.Length; i++)
                    {
                        if (productionInfo[i].ToString() == "J")
                        {
                            startCopy = true;
                        }
                        else if (ShiftCopied && (productionInfo[i].ToString() == "O" || productionInfo[i].ToString() == "B"))
                        {
                            startCopy = true;
                        }
                        else if (ShiftCopied && productionInfo[i].ToString() == "]")
                        {
                            startCopy = false;
                            StopIDCopy = true;
                        }

                        if (startCopy)
                        {

                            if (char.IsDigit(productionInfo[i]) && !StopIDCopy)
                            {
                                IDCopied = true;
                                _planID += productionInfo[i];
                            }
                            else if (IDCopied && i + 1 < productionInfo.Length)
                            {
                                _shift += productionInfo[i + 1];
                                IDCopied = false;
                                ShiftCopied = true;
                                startCopy = false;
                            }
                            else if (ShiftCopied)
                            {
                                balanceClearType += productionInfo[i];
                            }

                        }
                    }

                    if (_shift.ToUpper() == "M")
                    {
                        _shift = text.Shift_Morning;
                    }
                    else if (_shift.ToUpper() == "N")
                    {
                        _shift = text.Shift_Night;
                    }

                    bool dataMatch = shift == _shift && JOB_NO == _planID;

                    if (dataMatch)
                    {
                        int trfTableCode = int.TryParse(row[dalTrf.TrfID].ToString(), out trfTableCode) ? trfTableCode : -1;
                        if (trfTableCode != -1)
                            _ = tool.UndoTransferRecord(trfTableCode);

                    }
                }

            }

        }

        private bool CheckIfPreviousRecordExist()
        {
            DateTime proDate = dtpProDate.Value;

            DataTable dt_Trf;

            if (OLD_PRO_DATE == DateTime.MaxValue)
            {
                dt_Trf = dalTrf.rangeTrfSearchWithItemInfo(proDate.ToString("yyyy/MM/dd"), proDate.ToString("yyyy/MM/dd"));

            }
            else
            {
                dt_Trf = dalTrf.rangeTrfSearchWithItemInfo(OLD_PRO_DATE.ToString("yyyy/MM/dd"), OLD_PRO_DATE.ToString("yyyy/MM/dd"));

            }

            string shift = text.Shift_Morning;

            if (cbNight.Checked)
            {
                shift = text.Shift_Night;
            }

            dt_Trf.DefaultView.Sort = dalTrf.TrfDate + " DESC";
            dt_Trf = dt_Trf.DefaultView.ToTable();

            foreach (DataRow row in dt_Trf.Rows)
            {
                string status = row[dalTrf.TrfResult].ToString();

                if (status == "Passed")
                {
                    DateTime trfDate = Convert.ToDateTime(row[dalTrf.TrfDate].ToString());

                    string productionInfo = row[dalTrf.TrfNote].ToString();
                    string _shift = "";
                    string _planID = "";
                    string balanceClearType = "";
                    bool startCopy = false;
                    bool IDCopied = false;
                    bool ShiftCopied = false;
                    bool StopIDCopy = false;

                    for (int i = 0; i < productionInfo.Length; i++)
                    {
                        if (productionInfo[i].ToString() == "J")
                        {
                            startCopy = true;
                        }
                        else if (ShiftCopied && (productionInfo[i].ToString() == "O" || productionInfo[i].ToString() == "B"))
                        {
                            startCopy = true;
                        }
                        else if (ShiftCopied && productionInfo[i].ToString() == "]")
                        {
                            startCopy = false;
                            StopIDCopy = true;
                        }

                        if (startCopy)
                        {

                            if (char.IsDigit(productionInfo[i]) && !StopIDCopy)
                            {
                                IDCopied = true;
                                _planID += productionInfo[i];
                            }
                            else if (IDCopied && i + 1 < productionInfo.Length)
                            {
                                _shift += productionInfo[i + 1];
                                IDCopied = false;
                                ShiftCopied = true;
                                startCopy = false;
                            }
                            else if (ShiftCopied)
                            {
                                balanceClearType += productionInfo[i];
                            }

                        }
                    }

                    if (_shift.ToUpper() == "M")
                    {
                        _shift = text.Shift_Morning;
                    }
                    else if (_shift.ToUpper() == "N")
                    {
                        _shift = text.Shift_Night;
                    }

                    bool dataMatch = shift == _shift && JOB_NO == _planID;

                    if (dataMatch)
                    {
                        return true;

                    }
                }

            }

            return false;
        }

        private bool CheckIfDateOrShiftDuplicate()
        {
            ClearAllError();
            bool ifDuplicateData = false;
            string sheetID = txtSheetID.Text;

            if (sheetID == string_NewSheet)
            {
               
            }

            DataTable dt = DT_JOB_RECORD.Copy();

            DateTime selectedDate = dtpProDate.Value;

            string shift = text.Shift_Night;

            if (cbMorning.Checked)
            {
                shift = text.Shift_Morning;
            }

            if (shift != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DateTime recordedDate = Convert.ToDateTime(row[text.Header_ProductionDate].ToString());
                    string recordedShift = row[text.Header_Shift].ToString();
                    string recordedLotNo = row[text.Header_JobNo].ToString();

                    ifDuplicateData = recordedDate == selectedDate;
                    ifDuplicateData &= recordedShift == shift;

                    if (ifDuplicateData)
                    {
                        if(sheetID != row[text.Header_SheetID].ToString() && sheetID != SHEET_ID)
                        {
                            MessageBox.Show("There is a existing daily job sheet under your selected date and shift!");
                            return true;
                        }
                        else
                        {
                            ifDuplicateData = false;
                        }
                    }
                }
            }

            return ifDuplicateData;
        }

        private void LoadCartonSetting()
        {
            //get sheet Id
            string itemCode = ITEM_CODE;
            string sheetID = SHEET_ID;
            int MainCartonQty = 0;

            DT_CARTON = null;

            if (string.IsNullOrEmpty(sheetID) || sheetID.Equals(string_NewSheet))
            {
                //new sheet
                //find main carton from item group and load to dt_carton
                foreach (DataRow row in dt_JoinInfo.Rows)
                {
                    string parentCode = row[dalJoin.JoinParent].ToString();

                    bool mainCarton = bool.TryParse(row[dalJoin.JoinMainCarton].ToString(), out mainCarton) ? mainCarton : false;
                    bool StockOut = bool.TryParse(row[dalJoin.JoinStockOut].ToString(), out StockOut) ? StockOut : true;

                    if (itemCode == parentCode && mainCarton)
                    {

                        MainCartonQty++;

                        //adddata to dt_carton
                        if (DT_CARTON == null)
                        {
                            DT_CARTON = NewCartonTable();
                        }

                        DataRow newRow = DT_CARTON.NewRow();

                        string cartonCode = row[dalJoin.JoinChild].ToString();

                        int MaxQty = int.TryParse(row[dalJoin.JoinMax].ToString(), out MaxQty) ? MaxQty : 0;
                        int childQty = int.TryParse(row[dalJoin.JoinQty].ToString(), out childQty) ? childQty : 0;

                        newRow[text.Header_ItemCode] = cartonCode;
                        newRow[text.Header_ItemName] = tool.getItemName(cartonCode);
                        newRow[text.Header_Qty_Per_Container] = MaxQty;
                        newRow[text.Header_Container_Qty] = childQty;
                        newRow[text.Header_Container_Stock_Out] = StockOut;

                        DT_CARTON.Rows.Add(newRow);

                    }
                }
            }
            else
            {
                //old sheet
                //load carton data from carton db to dt_carton
                DataTable db_carton = dalProRecord.PackagingRecordSelect(int.TryParse(sheetID, out int i) ? i : 0);

                if (db_carton != null && db_carton.Rows.Count > 0)
                {
                    foreach (DataRow row in db_carton.Rows)
                    {

                        //adddata to dt_carton
                        if (DT_CARTON == null)
                        {
                            DT_CARTON = NewCartonTable();
                        }

                        DataRow newRow = DT_CARTON.NewRow();

                        string cartonCode = row[dalProRecord.PackagingCode].ToString();

                        int MaxQty = int.TryParse(row[dalProRecord.PackagingMax].ToString(), out MaxQty) ? MaxQty : 0;
                        int childQty = int.TryParse(row[dalProRecord.PackagingQty].ToString(), out childQty) ? childQty : 0;

                        bool cartonStockOut = bool.TryParse(row[dalProRecord.PackagingStockOut].ToString(), out cartonStockOut) ? cartonStockOut : true;

                        newRow[text.Header_ItemCode] = cartonCode;
                        newRow[text.Header_ItemName] = tool.getItemName(cartonCode);
                        newRow[text.Header_Qty_Per_Container] = MaxQty;
                        newRow[text.Header_Container_Qty] = childQty;
                        newRow[text.Header_Container_Stock_Out] = cartonStockOut;

                        DT_CARTON.Rows.Add(newRow);
                    }
                }

            }

            if (DT_CARTON != null && DT_CARTON.Rows.Count > 0)
            {
                int totalStockIn = 0;
                int totalFullBox = 0;
                _ = DT_CARTON.Rows[0][text.Header_ItemName].ToString();
                _ = DT_CARTON.Rows[0][text.Header_ItemCode].ToString();

                int rowCount = DT_CARTON.Rows.Count;

                if (rowCount >= 2)
                {

                    lblPackagingInfo.Text = "MULTI";


                    foreach (DataRow row in DT_CARTON.Rows)
                    {
                        int boxQty = int.TryParse(row[text.Header_Container_Qty].ToString(), out boxQty) ? boxQty : 0;
                        int maxQty = int.TryParse(row[text.Header_Qty_Per_Container].ToString(), out maxQty) ? maxQty : 0;

                        totalFullBox += boxQty;

                        totalStockIn += boxQty * maxQty;
                    }


                    txtFullBoxQty.Text = totalFullBox.ToString();


                }
                else if (rowCount == 1)
                {

                    foreach (DataRow row in DT_CARTON.Rows)
                    {
                        lblPackagingInfo.Text = row[text.Header_Qty_Per_Container].ToString();
                        txtFullBoxQty.Text = row[text.Header_Container_Qty].ToString();
                    }


                }
            }

        }

     

        private bool DATA_SAVED = false;
        private bool STOCK_UPDATE_REQUIRED = false;

        public bool AreTablesEqual(DataTable dt1, DataTable dt2)
        {
            if (dt1 == null && dt2 == null)
            {
                return true;
            }

            if (dt1 == null || dt2 == null)
            {
                return false;
            }

            if (dt1.Rows.Count != dt2.Rows.Count || dt1.Columns.Count != dt2.Columns.Count)
            {
                return false;
            }

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    if (!object.Equals(dt1.Rows[i][j], dt2.Rows[i][j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }



        private bool SaveSuccess()
        {
            bool success = false;
            DATA_SAVED = false;

            if (Validation())
            {
                if(txtSheetID.Text != string_NewSheet)
                {
                    //check if stock update required
                    int totalStockIn = int.TryParse(txtTotalStockInQty.Text, out int i) ? i : 0;

                    bool stockUpdateRequired = false;

                    if(totalStockIn != ORIGINAL_TOTAL_STOCK_IN_QTY || !AreTablesEqual(DT_CARTON, DT_ORIGINAL_CARTON))
                    {
                        stockUpdateRequired = true;

                    }

                    if(stockUpdateRequired && CheckIfPreviousRecordExist())
                    {
                        string message = "A stock record already exists for this Job Sheet.\n\r Do you want to  undo the existing stock record?";

                        DialogResult dialogResult = MessageBox.Show(message, "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            UndoPreviousRecordIfExist();
                        }
                    }

                }

                frmLoading.ShowLoadingScreen();

                DateTime updateTime = DateTime.Now;

                string itemCode = ITEM_CODE;

                string Shift = text.Shift_Night;
                int pcsPerBox = 0;


                if (cbMorning.Checked)
                {
                    Shift = text.Shift_Morning;
                }

                string PackagingCode = "";

                if (DT_CARTON != null)
                {
                    int rowCount = DT_CARTON.Rows.Count;
                    pcsPerBox = int.TryParse(DT_CARTON.Rows[0][text.Header_Qty_Per_Container].ToString(), out pcsPerBox) ? pcsPerBox : 0;

                    if (rowCount == 1)
                    {
                        PackagingCode = DT_CARTON.Rows[0][text.Header_ItemCode].ToString();
                       

                    }
                    else if(rowCount > 1)
                    {
                        PackagingCode = string_MultiPackaging;
                       
                    }

                }

                int meterStart = int.TryParse(txtMeterStart.Text, out meterStart) ? meterStart : 0;
                int meterEnd = int.TryParse(txtMeterEnd.Text, out meterEnd) ? meterEnd : 0;

                int balPieceStockIn = int.TryParse(txtStockInPcsQty.Text, out balPieceStockIn) ? balPieceStockIn : 0;

                int fullBox = int.TryParse(txtFullBoxQty.Text, out fullBox) ? fullBox : 0;
                int TotalStockIn = int.TryParse(txtTotalStockInQty.Text, out TotalStockIn) ? TotalStockIn : 0;
                int maxOutputQty = int.TryParse(lblMaxQty.Text, out maxOutputQty) ? maxOutputQty : 0;


                string note = txtNote.Text;

                uProRecord.active = true;
                uProRecord.plan_id = Convert.ToInt16(JOB_NO);
                uProRecord.production_date = dtpProDate.Value;
                uProRecord.production_time_start = dtpTimeStart.Value;
                uProRecord.production_time_end = dtpTimeEnd.Value;
                uProRecord.shift = Shift;
                uProRecord.packaging_code = PackagingCode;
                uProRecord.packaging_qty = pcsPerBox;
                uProRecord.mac_no = Convert.ToInt16(MAC_ID);
                uProRecord.production_lot_no = JOB_NO;
                uProRecord.raw_mat_lot_no = txtRawMatLotNo.Text;
                uProRecord.color_mat_lot_no = txtColorMatLotNo.Text;
                uProRecord.meter_start = meterStart;
                uProRecord.meter_end = meterEnd;
                uProRecord.note = note;
                uProRecord.directIn = balPieceStockIn;
                uProRecord.parent_code = "";
                uProRecord.production_operator = txtOperator.Text;

                int userID = MainDashboard.USER_ID;

                uProRecord.full_box = fullBox;
                uProRecord.total_stocked_in = TotalStockIn;
                uProRecord.max_output_qty = maxOutputQty;
                uProRecord.updated_date = updateTime;
                uProRecord.updated_by = userID;

                int sheetID = int.TryParse(txtSheetID.Text, out sheetID) ? sheetID : -1;

                uProRecord.sheet_id = sheetID;

                success = false;

                if (sheetID == -1)
                {
                    success = dalProRecord.Semenyih_InsertProductionRecord(uProRecord);
                }
                else
                {
                    //update data
                    success = dalProRecord.Semenyih_ProductionRecordUpdate(uProRecord);
                }

                if (success)
                {
                    DATA_SAVED = true;


                    if (sheetID == -1)
                    {
                        //new sheet: sheet id + item
                        tool.historyRecord(text.AddDailyJobSheet, "Job No.: " + JOB_NO + "(" + itemCode + ")", updateTime, userID);
                    }
                    else
                    {
                        //edit/modify sheet
                        tool.historyRecord(text.EditDailyJobSheet, "Sheet ID: " + sheetID + "(" + itemCode + ")", updateTime, userID);
                    }

                    if (uProRecord.sheet_id == -1)
                    {
                        uProRecord.sheet_id = dalProRecord.GetLastInsertedSheetID();
                    }

                    #region Meter Reading Update
                    ////remove old meter data
                    ////remove data under same sheet id
                    //dalProRecord.DeleteMeterData(uProRecord);
                    //dalProRecord.DeleteDefectData(uProRecord);

                    ////insert new meter data
                    //DataTable dt_Meter = (DataTable)dgvMeterReading.DataSource;

                    //foreach (DataRow row in dt_Meter.Rows)
                    //{
                    //    DateTime time = Convert.ToDateTime(row[header_Time].ToString());
                    //    string proOperator = row[header_Operator].ToString();
                    //    int meterReading = int.TryParse(row[header_MeterReading].ToString(), out meterReading) ? meterReading : -1;

                    //    uProRecord.time = time;
                    //    uProRecord.production_operator = proOperator;
                    //    uProRecord.meter_reading = meterReading;

                    //    if (meterReading != -1)
                    //    {
                    //        if (!dalProRecord.InsertSheetMeter(uProRecord))
                    //        {
                    //            break;
                    //        }
                    //    }

                    //    foreach (DataColumn col in dt_Meter.Columns)
                    //    {
                    //        string colName = col.ColumnName;

                    //        if (colName.Contains(text.Header_DefectRemark))
                    //        {
                    //            string qtyRejectColName = text.Header_QtyReject + colName.Replace(text.Header_DefectRemark, "");

                    //            string defectRemarkData = row[colName].ToString();
                    //            int qtyReject = int.TryParse(row[qtyRejectColName].ToString(), out qtyReject) ? qtyReject : -1;


                    //            if (qtyReject > -1 && !string.IsNullOrEmpty(defectRemarkData))
                    //            {
                    //                uProRecord.defect_remark = defectRemarkData;
                    //                uProRecord.reject_qty = qtyReject;

                    //                if (!dalProRecord.InsertSheetDefectRemark(uProRecord))
                    //                {
                    //                    MessageBox.Show("Defect Remark and Qty Reject cannot be saved!");
                    //                }

                    //            }
                    //        }
                    //    }

                    //}
                    #endregion


                    //remove packaging data from db and insert new data
                    dalProRecord.DeletePackagingData(uProRecord);

                    if (DT_CARTON != null)
                    {
                        //save packaging data
                        foreach (DataRow row in DT_CARTON.Rows)
                        {
                            uProRecord.packaging_code = row[text.Header_ItemCode].ToString();
                            uProRecord.packaging_max = Convert.ToInt32(row[text.Header_Qty_Per_Container].ToString());
                            uProRecord.packaging_qty = Convert.ToInt32(row[text.Header_Container_Qty].ToString());
                            uProRecord.packaging_stock_out = bool.TryParse(row[text.Header_Container_Stock_Out].ToString(), out bool stockOUt) ? stockOUt : true;

                            if (!dalProRecord.InsertProductionPackaging(uProRecord))
                            {
                                MessageBox.Show("Failed to save packaging data!");
                            }
                        }
                    }

                    frmLoading.CloseForm();

                    MessageBox.Show("Data saved!");

                }
                else
                {
                    MessageBox.Show("Failed to save data");
                }
                //save meter data

            }
            frmLoading.CloseForm();

            return success;
        }

        readonly string header_Cat = "Cat";
        readonly string header_ProDate = "DATE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_From = "FROM";
        readonly string header_To = "TO";
        readonly string header_Qty = "QTY";
        readonly private string header_JobNo = "JOB NO.";
        readonly private string header_Shift = "SHIFT";

        private DataTable NewStockInTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Cat, typeof(string));
            dt.Columns.Add(header_ProDate, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_From, typeof(string));
            dt.Columns.Add(header_To, typeof(string));
            dt.Columns.Add(header_Qty, typeof(string));
            dt.Columns.Add(header_JobNo, typeof(string));
            dt.Columns.Add(header_Shift, typeof(string));

            return dt;
        }

        private void NewSaveAndStock()
        {
            bool OKToProcess = true;

            if (CheckIfPreviousRecordExist())
            {
                DialogResult dialogResult = MessageBox.Show("This action will UNDO all the previous record,\nare you sure you want to continue?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    OKToProcess = false;
                }
            }

            if (OKToProcess && SaveSuccess())
            {
                UndoPreviousRecordIfExist();

                DataTable dt = NewStockInTable();
                DataRow dt_Row;

                //get totalStockIn qty from box qty and max pcs per box
                //check if one or multi packaging box

                int totalStockIn = int.TryParse(txtTotalStockInQty.Text, out totalStockIn) ? totalStockIn : 0;

                int directStockIn = int.TryParse(txtStockInPcsQty.Text, out directStockIn) ? directStockIn : 0;

                int selectedItem = JOB_LIST_SELECTED_ROW_INDEX;

                string planStatus = DT_JOB_LIST.Rows[selectedItem][text.Header_Status].ToString();
                string factory = DT_JOB_LIST.Rows[selectedItem][text.Header_Fac].ToString();

                int totalShot = int.TryParse(txtTotalShot.Text, out totalShot) ? totalShot : 0;
                int fullBoxQty = int.TryParse(txtFullBoxQty.Text, out fullBoxQty) ? fullBoxQty : 0;


                if(totalStockIn > 0)
                {
                    if (DT_CARTON != null && DT_CARTON.Rows.Count >= 2)
                    {

                        foreach (DataRow row in DT_CARTON.Rows)
                        {
                            string packingCode = row[text.Header_ItemCode].ToString();

                            string itemType = packingCode.Substring(0, 3);

                            int boxQty = Convert.ToInt32(row[text.Header_Container_Qty].ToString());
                            int maxQty = Convert.ToInt32(row[text.Header_Qty_Per_Container].ToString());

                            bool cartonStockOut = bool.TryParse(row[text.Header_Container_Stock_Out].ToString(), out cartonStockOut) ? cartonStockOut : true;

                            if (!string.IsNullOrEmpty(packingCode) && boxQty > 0 && cartonStockOut)
                            {
                                dt_Row = dt.NewRow();
                                dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                                dt_Row[header_ItemCode] = packingCode;
                                dt_Row[header_Cat] = tool.getItemCat(packingCode);

                                dt_Row[header_From] = factory;
                                dt_Row[header_To] = text.Production;

                                dt_Row[header_Qty] = boxQty;
                                dt_Row[text.Header_JobNo] = JOB_NO;

                                if (cbMorning.Checked)
                                {
                                    dt_Row[header_Shift] = "M";
                                }
                                else if (cbNight.Checked)
                                {
                                    dt_Row[header_Shift] = "N";
                                }


                                dt.Rows.Add(dt_Row);
                            }

                        }
                    }
                    else
                    {
                        bool cartonStockOut = true;
                        string packingCode = "";

                        if (DT_CARTON != null)
                        {
                            foreach (DataRow row in DT_CARTON.Rows)
                            {
                                packingCode = row[text.Header_ItemCode].ToString();
                                cartonStockOut = bool.TryParse(row[text.Header_Container_Stock_Out].ToString(), out cartonStockOut) ? cartonStockOut : true;
                            }
                        }


                        if (!string.IsNullOrEmpty(packingCode) && fullBoxQty > 0 && cartonStockOut)
                        {
                            dt_Row = dt.NewRow();
                            dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                            dt_Row[header_ItemCode] = packingCode;
                            dt_Row[header_Cat] = tool.getItemCat(packingCode);

                            dt_Row[header_From] = factory;
                            dt_Row[header_To] = text.Production;

                            dt_Row[header_Qty] = fullBoxQty;
                            dt_Row[text.Header_JobNo] = JOB_NO;

                            if (cbMorning.Checked)
                            {
                                dt_Row[header_Shift] = "M";
                            }
                            else if (cbNight.Checked)
                            {
                                dt_Row[header_Shift] = "N";
                            }

                            dt.Rows.Add(dt_Row);
                        }

                    }

                    if (!string.IsNullOrEmpty(ITEM_CODE))
                    {
                        string ProductionOrAssembly = text.Production;
                        string StockInItemCode = ITEM_CODE;

                        if (totalStockIn > 0)
                        {
                            dt_Row = dt.NewRow();
                            dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                            dt_Row[header_ItemCode] = StockInItemCode;
                            dt_Row[header_Cat] = text.Cat_Part;

                            dt_Row[header_From] = ProductionOrAssembly;
                            dt_Row[header_To] = factory;

                            dt_Row[header_Qty] = totalStockIn;
                            dt_Row[text.Header_JobNo] = JOB_NO;

                            if (cbMorning.Checked)
                            {
                                dt_Row[header_Shift] = "M";
                            }
                            else if (cbNight.Checked)
                            {
                                dt_Row[header_Shift] = "N";
                            }

                            dt.Rows.Add(dt_Row);
                        }

                        if (directStockIn > 0)
                        {
                            //dt_Row = dt.NewRow();
                            //dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                            //dt_Row[header_ItemCode] = StockInItemCode;
                            //dt_Row[header_Cat] = text.Cat_Part;

                            //dt_Row[header_From] = ProductionOrAssembly;
                            //dt_Row[header_To] = factory;

                            //dt_Row[header_Qty] = directStockIn;
                            //dt_Row[text.Header_JobNo] = JOB_NO;

                            //if (planStatus == text.planning_status_completed)
                            //{
                            //    if (cbMorning.Checked)
                            //    {
                            //        dt_Row[header_Shift] = "MB";
                            //    }
                            //    else if (cbNight.Checked)
                            //    {
                            //        dt_Row[header_Shift] = "NB";
                            //    }
                            //}
                            //else
                            //{
                            //    if (cbMorning.Checked)
                            //    {
                            //        dt_Row[header_Shift] = "M";
                            //    }
                            //    else if (cbNight.Checked)
                            //    {
                            //        dt_Row[header_Shift] = "N";
                            //    }
                            //}


                            //dt.Rows.Add(dt_Row);
                        }

                        if (totalShot > 0)
                        {
                            //float totalWeightPerShot = PW_PER_SHOT + RW_PER_SHOT;
                            //float totalweightProduced_kg = totalWeightPerShot * totalShot / 1000;

                            //float raw_Material_Used_kg = totalweightProduced_kg / (1 + COLOR_MAT_RATE);

                            //float color_Material_Used_kg = raw_Material_Used_kg * COLOR_MAT_RATE;

                            //if (!string.IsNullOrEmpty(RAW_MAT_CODE) && SearchItem(dt_ItemInfo, RAW_MAT_CODE, dalItem.ItemCode))
                            //{
                            //    dt_Row = dt.NewRow();
                            //    dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                            //    dt_Row[header_ItemCode] = RAW_MAT_CODE;
                            //    dt_Row[header_Cat] = text.Cat_RawMat;

                            //    dt_Row[header_From] = factory;
                            //    dt_Row[header_To] = text.Production;


                            //    dt_Row[header_Qty] = raw_Material_Used_kg;

                            //    dt_Row[text.Header_JobNo] = JOB_NO;

                            //    if (cbMorning.Checked)
                            //    {
                            //        dt_Row[header_Shift] = "M";
                            //    }
                            //    else if (cbNight.Checked)
                            //    {
                            //        dt_Row[header_Shift] = "N";
                            //    }

                            //    dt.Rows.Add(dt_Row);
                            //}

                            //if (!string.IsNullOrEmpty(COLOR_MAT_CODE) && SearchItem(dt_ItemInfo, COLOR_MAT_CODE, dalItem.ItemCode))
                            //{
                            //    dt_Row = dt.NewRow();
                            //    dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");

                            //    dt_Row[header_ItemCode] = COLOR_MAT_CODE;

                            //    dt_Row[header_Cat] = tool.getCatNameFromDataTable(dt_ItemInfo, COLOR_MAT_CODE);

                            //    dt_Row[header_From] = factory;
                            //    dt_Row[header_To] = text.Production;


                            //    dt_Row[header_Qty] = color_Material_Used_kg;

                            //    dt_Row[text.Header_JobNo] = JOB_NO;

                            //    if (cbMorning.Checked)
                            //    {
                            //        dt_Row[header_Shift] = "M";
                            //    }
                            //    else if (cbNight.Checked)
                            //    {
                            //        dt_Row[header_Shift] = "N";
                            //    }

                            //    dt.Rows.Add(dt_Row);
                            //}

                        }
                    }

                    #region process to in/out edit page

                    frmInOutEdit frm = new frmInOutEdit(dt, true);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Normal;
                    frm.ShowDialog();//Item Edit

                    if (!frmInOutEdit.TrfSuccess)
                    {
                        MessageBox.Show("Transfer failed or cancelled!");
                    }
                    else
                    {
                        Close();
                    }

                    #endregion
                }
                else
                {
                    MessageBox.Show("no stock update needed.");
                }
            }
        }

        private bool FORM_LOADED = false;

        private void frmJobSheetRecord_Load(object sender, EventArgs e)
        {
            FORM_LOADED = true;
        }


        private void OnlyNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(sender == txtFullBoxQty)
            {
                if (DT_CARTON != null && DT_CARTON.Rows.Count > 1)
                {
                    MessageBox.Show("Multiple packaging types found!\nPlease adjust the packaging settings by clicking only the (Blue Text) Packaging Info.");
                    e.Handled = true;
                    return;
                }
            }

            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void UnableKeyIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        private void cbMorning_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            DATA_EDITED = true;

            if (cbMorning.Checked)
            {
                dtpTimeStart.Value = new DateTime(dtpProDate.Value.Year, dtpProDate.Value.Month, dtpProDate.Value.Day, 8, 0, 0);
                dtpTimeEnd.Value = new DateTime(dtpProDate.Value.Year, dtpProDate.Value.Month, dtpProDate.Value.Day, 20, 0, 0);

                cbNight.Checked = false;

                if (CheckIfDateOrShiftDuplicate())
                {
                    ClearAllError();
                    errorProvider1.SetError(lblDate, "Please select a different date");
                    errorProvider2.SetError(lblShift, "Please select a different shift");
                }

            }




        }

        private void cbNight_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            DATA_EDITED = true;

            if (cbNight.Checked)
            {
                dtpTimeStart.Value = new DateTime(dtpProDate.Value.Year, dtpProDate.Value.Month, dtpProDate.Value.Day, 20, 0, 0);
                dtpTimeEnd.Value = new DateTime(dtpProDate.Value.Year, dtpProDate.Value.Month, dtpProDate.Value.Day, 08, 0, 0);

                cbMorning.Checked = false;

                if (CheckIfDateOrShiftDuplicate())
                {
                    ClearAllError();
                    errorProvider1.SetError(lblDate, "Please select a different date");
                    errorProvider2.SetError(lblShift, "Please select a different shift");
                }
            }

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool oktoClose = true;

            if (DATA_EDITED && !DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to close this form without saving?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    oktoClose = false;
                }
            }

            if (oktoClose)
            {
                Close(); 
            }
            
        }

        private void btnSaveAndStock_Click(object sender, EventArgs e)
        {
            NewSaveAndStock();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveSuccess())
            {
                MessageBox.Show("Job sheet record saved!");
                Close();
            }
        }

        private void txtBalanceOfThisShift_TextChanged(object sender, EventArgs e)
        {
            totalStockUpdate();
        }


        private void txtTotalShot_TextChanged(object sender, EventArgs e)
        {

            int totalShot = int.TryParse(txtTotalShot.Text, out totalShot) ? totalShot : 0;
            lblMaxQty.Text = "0";

            if (totalShot > 0)
            {
                int cavity = int.TryParse(txtCavity.Text, out cavity) ? cavity : 0;
                int maxQty = cavity * totalShot;

                lblMaxQty.Text = maxQty.ToString();

                calculateAvgHourlyShot();
            }


        }

        private void calculateAvgHourlyShot()
        {
            DATA_EDITED = true;

            int totalShot = int.TryParse(txtTotalShot.Text, out totalShot) ? totalShot : 0;

            string message = "Avg Hourly Shot: ERROR";

            if (totalShot > 0)
            {
                TimeSpan difference = dtpTimeEnd.Value - dtpTimeStart.Value;

                double totalHours = difference.TotalHours;

                totalHours = totalHours < 0 ? totalHours * -1 : totalHours;

                int avgHourlyShot = (int)( totalShot / totalHours);

                if(avgHourlyShot >= 0)
                {
                    message = "Avg Hourly Shot: " + avgHourlyShot.ToString();
                }
            }

            lblAvgHourlyShot.Text = message;
        }

        private void dtpProDate_ValueChanged_1(object sender, EventArgs e)
        {
            if (FORM_LOADED)
            {
                if (CheckIfDateOrShiftDuplicate())
                {
                    ClearAllError();
                    errorProvider1.SetError(lblDate, "Please select a different date");
                    errorProvider2.SetError(lblShift, "Please select a different shift");
                }

                DATA_EDITED = true;

            }

        }




        private void cbEndProduction_CheckedChanged(object sender, EventArgs e)
        {
            DATA_EDITED = true;

            if (cbEndProduction.Checked)
            {
                txtNote.Text = text.job_end_production + txtNote.Text ;
            }
            else
            {
                txtNote.Text = txtNote.Text.Replace(text.job_end_production, "");
            }
        }

        private void txtMeterStart_TextChanged_1(object sender, EventArgs e)
        {
            int meterStart = int.TryParse(txtMeterStart.Text, out meterStart) ? meterStart : 0;
            int meterEnd = int.TryParse(txtMeterEnd.Text, out meterEnd) ? meterEnd : 0;

            int totalShot = meterEnd - meterStart;
            txtTotalShot.Text = totalShot.ToString();

        }

        private void txtMeterEnd_TextChanged(object sender, EventArgs e)
        {
            int meterStart = int.TryParse(txtMeterStart.Text, out meterStart) ? meterStart : 0;
            int meterEnd = int.TryParse(txtMeterEnd.Text, out meterEnd) ? meterEnd : 0;

            int totalShot = meterEnd - meterStart;
            txtTotalShot.Text = totalShot.ToString();
        }

        private void txtTotalStockInQty_TextChanged(object sender, EventArgs e)
        {
            DATA_EDITED = true;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            int sheetID = int.TryParse(SHEET_ID, out sheetID)? sheetID : 0;

            if (DT_CARTON == null || DT_CARTON.Rows.Count <= 0)
            {
                DT_CARTON = NewCartonTable();
            }

            frmCartonSetting frm = new frmCartonSetting(DT_CARTON,ITEM_CODE,ITEM_NAME)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();

            DT_CARTON = frmCartonSetting._DT_CARTON;

            string AlertMessage = "";

            if (DT_CARTON.Rows.Count > 0)
            {
                totalStockUpdate();
            }
            else
            {
                if(txtSheetID.Text != string_NewSheet)
                {
                    string FullBox = "";
                    string QtyPerBox = "";
                    string balanceStockIn = "0";

                    if (DT_JOB_RECORD?.Rows.Count > 0 && JOB_RECORD_SELECTED_ROW_INDEX > -1 && JOB_RECORD_SELECTED_ROW_INDEX <= DT_JOB_RECORD.Rows.Count - 1)
                    {
                        FullBox = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_StockIn_Container].ToString();
                        QtyPerBox = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_Qty_Per_Container].ToString();
                        balanceStockIn = DT_JOB_RECORD.Rows[JOB_RECORD_SELECTED_ROW_INDEX][text.Header_StockIn_Balance_Qty].ToString();
                    }

                    AlertMessage = "Packaging data not found!\nLoading the saved data...";

                    LoadCartonData(FullBox, QtyPerBox, balanceStockIn);
                    totalStockUpdate();
                }
                else
                {
                    AlertMessage = "Packaging data not found!\nPlease add a main carton to this item at:\n1. Item Group Edit";
                }
            }

            if (!string.IsNullOrEmpty(AlertMessage))
                MessageBox.Show(AlertMessage, "Carton Alert",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dtpTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            calculateAvgHourlyShot();
        }

        private void dtpTimeStart_ValueChanged(object sender, EventArgs e)
        {
            calculateAvgHourlyShot();

        }

        bool TOTAL_STOCK_IN_QTY_UPDATING = false;

        private void totalStockUpdate()
        {
            if(!TOTAL_STOCK_IN_QTY_UPDATING)
            {
                int totalStockInQtyFromFullBox = 0;
                int totalFullBox = 0;
                string packagingInfo = "";

                TOTAL_STOCK_IN_QTY_UPDATING = true;

                if (DT_CARTON != null && DT_CARTON.Rows.Count > 0)
                {
                    foreach (DataRow row in DT_CARTON.Rows)
                    {
                        string name = row[text.Header_ItemName].ToString();
                        string code = row[text.Header_ItemCode].ToString();

                        int boxQty = int.TryParse(row[text.Header_Container_Qty].ToString(), out boxQty) ? boxQty : 0;
                        int qtyPerBox = int.TryParse(row[text.Header_Qty_Per_Container].ToString(), out qtyPerBox) ? qtyPerBox : 0;

                        totalFullBox += boxQty;
                        totalStockInQtyFromFullBox += boxQty * qtyPerBox;

                        packagingInfo += qtyPerBox + "/" + name + " * " + boxQty + ";\n";
                       
                    }
                }

                if (!string.IsNullOrEmpty(packagingInfo))
                {
                    packagingInfo = packagingInfo.TrimEnd('\n');  // Remove the last newline character if there's one.
                    packagingInfo = packagingInfo.Remove(packagingInfo.Length - 1) + ".";  // Replace the last character with a period.

                }
                else
                {
                    packagingInfo = "Packaging data not found!\nClick here adjust setting.";
                }

                lblPackagingInfo.Text = packagingInfo;

                txtFullBoxQty.Text = totalFullBox.ToString();

                int balPcsStockIN = int.TryParse(txtStockInPcsQty.Text, out balPcsStockIN) ? balPcsStockIN : 0;

                int totalStockIn = totalStockInQtyFromFullBox + balPcsStockIN;

                txtTotalStockInQty.Text = totalStockIn.ToString();

                TOTAL_STOCK_IN_QTY_UPDATING = false;
            }
        }

        private void txtFullBoxQty_TextChanged(object sender, EventArgs e)
        {
            if(DT_CARTON != null && DT_CARTON.Rows.Count == 1)
            {
                int boxQty = int.TryParse(txtFullBoxQty.Text, out boxQty) ? boxQty : 0;
                DT_CARTON.Rows[0][text.Header_Container_Qty] = boxQty;

                totalStockUpdate();
            }
        }

        private void frmJobSheetRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!DATA_SAVED && DATA_EDITED)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to close this form without saving?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;  // This will prevent the form from closing
                }
            }
           
        }

        private void frmJobSheetRecord_Shown(object sender, EventArgs e)
        {
            DATA_EDITED = false;

            if(ORIGINAL_TOTAL_STOCK_IN_QTY == -1)
            {
                ORIGINAL_TOTAL_STOCK_IN_QTY = int.TryParse(txtTotalStockInQty.Text, out int i) ? i : 0;
            }
        }

        private void txtRawMatLotNo_TextChanged(object sender, EventArgs e)
        {
            DATA_EDITED = true;
        }

        private void txtColorMatLotNo_TextChanged(object sender, EventArgs e)
        {
            DATA_EDITED = true;

        }

        private void txtCavity_TextChanged(object sender, EventArgs e)
        {
            DATA_EDITED = true;

        }

        private void txtOperator_TextChanged(object sender, EventArgs e)
        {
            DATA_EDITED = true;

        }

        private void txtNote_TextChanged(object sender, EventArgs e)
        {
            DATA_EDITED = true;

        }
    }
}
