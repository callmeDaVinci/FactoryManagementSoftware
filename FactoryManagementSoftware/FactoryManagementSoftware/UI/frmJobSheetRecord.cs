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

            NewJobSheet();


            dt_ItemInfo = dalItem.Select();
            dt_JoinInfo = dalJoin.SelectAll();
            dt_Mac = dalMac.Select();
        }

        public frmJobSheetRecord(DataTable dt_JobList, int jobListRowIndex, DataTable dt_JobRecordHistory, int jobRecordRowIndex)
        {
            InitializeComponent();

            DT_JOB_LIST = dt_JobList;
            JOB_LIST_SELECTED_ROW_INDEX = jobListRowIndex;

            DT_JOB_RECORD = dt_JobRecordHistory;
            JOB_RECORD_SELECTED_ROW_INDEX = jobRecordRowIndex;

            LoadJobSheet();

            dt_ItemInfo = dalItem.Select();
            dt_JoinInfo = dalJoin.SelectAll();
            dt_Mac = dalMac.Select();
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
        private string SHEET_ID = "";

        readonly private string string_NewSheet = "NEW";
        readonly private string string_Multi = "MULTI";
        readonly private string string_MultiPackaging = "MULTI PACKAGING";

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
            dt.Columns.Add(text.Header_Packing_Max_Qty, typeof(int));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_Packaging_Qty, typeof(int));
            dt.Columns.Add(text.Header_Packaging_Stock_Out, typeof(bool));

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

            if(DT_JOB_LIST?.Rows.Count > 0 && JOB_LIST_SELECTED_ROW_INDEX > -1 && JOB_LIST_SELECTED_ROW_INDEX <= DT_JOB_LIST.Rows.Count - 1)
            {
                ITEM_CODE = DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_ItemCode].ToString();
                ITEM_NAME = DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_ItemName].ToString();
                JOB_NO = DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_JobNo].ToString();

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

            

        }

        private void LoadJobSheet()
        {
            btnSave.Visible = false;

            tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);
            tlpButton.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0);

            LoadJobData();

            string sheetID = "";
            string rawLotNo = "";
            string colorLotNo = "";
            string Shift = "";
            string MeterStart = "0";
            string MeterEnd = "0";
            string Operator = "";
            string Cavity = "";
            string Remark = "";

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

            }

            if (DT_JOB_LIST?.Rows.Count > 0 && JOB_LIST_SELECTED_ROW_INDEX > -1 && JOB_LIST_SELECTED_ROW_INDEX <= DT_JOB_LIST.Rows.Count - 1)
            {
                if (Cavity == "" || Cavity == "0" || string.IsNullOrEmpty(Cavity))
                {
                    Cavity = DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_Cavity].ToString();
                }

                CYCLE_TIME = int.TryParse(DT_JOB_LIST.Rows[JOB_LIST_SELECTED_ROW_INDEX][text.Header_ProCT].ToString(), out CYCLE_TIME) ? CYCLE_TIME : 0;
            }

            txtSheetID.Text = sheetID;
            txtRawMatLotNo.Text = rawLotNo;
            txtColorMatLotNo.Text = colorLotNo;
            txtCavity.Text = Cavity;

            txtMeterStart.Text = MeterStart;
            txtMeterEnd.Text = MeterEnd;
            txtOperator.Text = Operator;

            dtpProDate.Value = ProductionDate.Date;

            if (Shift == text.Shift_Morning)
            {
                cbMorning.Checked = true;
            }
            else
            {
                cbNight.Checked = true;
              
            }

            int idealHourlyShot = (int)((double)3600 / CYCLE_TIME);

            lblIdealHourlyShot.Text = "Ideal Hourly Shot: " + idealHourlyShot;

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
            //string planID = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();
            string jobNo = "";

            DateTime proDate = dtpProDate.Value;

            DataTable dt_Trf;

            if (OLD_PRO_DATE == DateTime.MaxValue)
            {
                dt_Trf = dalTrf.rangeTrfSearch(proDate.ToString("yyyy/MM/dd"), proDate.ToString("yyyy/MM/dd"));

            }
            else
            {
                dt_Trf = dalTrf.rangeTrfSearch(OLD_PRO_DATE.ToString("yyyy/MM/dd"), OLD_PRO_DATE.ToString("yyyy/MM/dd"));

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
                        if (productionInfo[i].ToString() == "P")
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

                    bool dataMatch = shift == _shift && jobNo == _planID;

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
            string jobNo = "";

            DateTime proDate = dtpProDate.Value;

            DataTable dt_Trf;

            if (OLD_PRO_DATE == DateTime.MaxValue)
            {
                dt_Trf = dalTrf.rangeTrfSearch(proDate.ToString("yyyy/MM/dd"), proDate.ToString("yyyy/MM/dd"));

            }
            else
            {
                dt_Trf = dalTrf.rangeTrfSearch(OLD_PRO_DATE.ToString("yyyy/MM/dd"), OLD_PRO_DATE.ToString("yyyy/MM/dd"));

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
                        if (productionInfo[i].ToString() == "P")
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

                    bool dataMatch = shift == _shift && jobNo == _planID;

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
            if (txtSheetID.Text == string_NewSheet)
            {
                DataTable dt = DT_JOB_RECORD.Copy(); ;

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
                            _ = MessageBox.Show("There is a existing daily job sheet under your selected date and shift!");
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
                        newRow[text.Header_Packing_Max_Qty] = MaxQty;
                        newRow[text.Header_Packaging_Qty] = childQty;
                        newRow[text.Header_Packaging_Stock_Out] = StockOut;

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
                        newRow[text.Header_Packing_Max_Qty] = MaxQty;
                        newRow[text.Header_Packaging_Qty] = childQty;
                        newRow[text.Header_Packaging_Stock_Out] = cartonStockOut;

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
                        int boxQty = int.TryParse(row[text.Header_Packaging_Qty].ToString(), out boxQty) ? boxQty : 0;
                        int maxQty = int.TryParse(row[text.Header_Packing_Max_Qty].ToString(), out maxQty) ? maxQty : 0;

                        totalFullBox += boxQty;

                        totalStockIn += boxQty * maxQty;
                    }


                    txtStockInContainerQty.Text = totalFullBox.ToString();


                }
                else if (rowCount == 1)
                {

                    foreach (DataRow row in DT_CARTON.Rows)
                    {
                        lblPackagingInfo.Text = row[text.Header_Packing_Max_Qty].ToString();
                        txtStockInContainerQty.Text = row[text.Header_Packaging_Qty].ToString();
                    }


                }
            }

        }


        private bool SaveSuccess()
        {
            return true;

            //bool success = false;

            //if(Validation())
            //{
            //frmLoading.ShowLoadingScreen();


            //string Shift = text.Shift_Night;
            //string PackagingCode = cmbPackingCode.Text;
            //int PackagingQty = int.TryParse(txtPackingMaxQty.Text, out PackagingQty)? PackagingQty : 0;
            //int meterStart = Convert.ToInt32(txtMeterStart.Text);
            //int meterEnd = meterStart;
            //int lastShiftBalance = Convert.ToInt32(txtBalanceOfLastShift.Text);
            //int thisShiftBalance = Convert.ToInt32(txtBalanceOfThisShift.Text);

            //int directIn = int.TryParse(txtStockInPcsQty.Text, out directIn) ? directIn : 0;
            //int directOut = int.TryParse(txtOut.Text, out directOut) ? directOut : 0;

            //int fullBox = Convert.ToInt32(txtFullBox.Text);
            //int TotalStockIn = Convert.ToInt32(txtTotalProduce.Text);
            //int TotalReject = Convert.ToInt32(txtTotalReject.Text);
            //int TotalActualReject =  int.TryParse(txtActualTotalReject.Text, out TotalActualReject) ? TotalActualReject : 0;

            //double RejectPercentage = Convert.ToDouble(txtRejectPercentage.Text);
            //string ParentStockIn = cmbParentList.Text;
            //string note = txtNote.Text ;
            //string itemCode = lblPartCode.Text;

            //JOB_NO = GetINTOnlyFromLotNo(txtJobNo.Text);

            //DateTime updateTime = DateTime.Now;

            //if (cbMorning.Checked)
            //{
            //    Shift = text.Shift_Morning;
            //}

            //uProRecord.active = true;
            //uProRecord.plan_id = Convert.ToInt16(JobNo);
            //uProRecord.production_date = dtpProDate.Value;
            //uProRecord.shift = Shift;
            //uProRecord.packaging_code = PackagingCode;
            //uProRecord.packaging_qty = PackagingQty;
            //uProRecord.mac_no = macNo;
            //uProRecord.production_lot_no = JOB_NO.ToString();
            //uProRecord.raw_mat_lot_no = txtRawMatLotNo.Text;
            //uProRecord.color_mat_lot_no = txtColorMatLotNo.Text;
            //uProRecord.meter_start = meterStart;
            //uProRecord.parent_code = ParentStockIn;
            //uProRecord.note = note;
            //uProRecord.directIn = directIn;
            //uProRecord.directOut = directOut;

            //foreach (DataGridViewRow row in dgvMeterReading.Rows)
            //{
            //    if(row.Cells[header_MeterReading].Value != DBNull.Value)
            //    {
            //        int i = 0;
            //        if (int.TryParse(row.Cells[header_MeterReading].Value.ToString(), out i))
            //        {
            //            meterEnd = i;
            //        }
            //    }

            //}

            //int userID = MainDashboard.USER_ID;
            //uProRecord.meter_end = meterEnd;

            //uProRecord.last_shift_balance = lastShiftBalance;
            //uProRecord.current_shift_balance = thisShiftBalance;
            //uProRecord.full_box = fullBox;
            //uProRecord.total_produced = TotalStockIn;
            //uProRecord.total_reject = TotalReject;
            //uProRecord.total_actual_reject = TotalActualReject;
            //uProRecord.reject_percentage = RejectPercentage;
            //uProRecord.updated_date = updateTime;
            //uProRecord.updated_by = userID;
            //uProRecord.sheet_id = sheetID;

            //success = false;

            //if(addingNewSheet)
            //{
            //    success = dalProRecord.InsertProductionRecord(uProRecord);
            //}
            //else
            //{
            //    //update data
            //    success = dalProRecord.ProductionRecordUpdate(uProRecord);
            //}

            //    if(success)
            //    {
            //        dataSaved = true;

            //        CheckIfLatestRecord(uProRecord.production_lot_no,uProRecord.production_date, uProRecord.shift);

            //        if (addingNewSheet)
            //        {
            //            //new sheet: sheet id + item
            //            tool.historyRecord(text.AddDailyJobSheet, "Job No.: "+ JobNo + "(" + itemCode + ")", updateTime, userID);
            //        }
            //        else
            //        {
            //            //edit/modify sheet
            //            tool.historyRecord(text.EditDailyJobSheet, "Sheet ID: " + sheetID + "(" + itemCode + ")", updateTime, userID);
            //        }

            //        dt_ProductionRecord = dalProRecord.Select();
            //        dgvItemList.Focus();
            //        LoadDailyRecord();

            //        if(uProRecord.sheet_id == -1)
            //        {
            //            uProRecord.sheet_id = dalProRecord.GetLastInsertedSheetID();
            //        }

            //        //remove old meter data
            //        //remove data under same sheet id
            //        dalProRecord.DeleteMeterData(uProRecord);
            //        dalProRecord.DeleteDefectData(uProRecord);

            //        //insert new meter data
            //        DataTable dt_Meter = (DataTable)dgvMeterReading.DataSource;

            //        foreach(DataRow row in dt_Meter.Rows)
            //        {
            //            DateTime time = Convert.ToDateTime(row[header_Time].ToString());
            //            string proOperator = row[header_Operator].ToString();
            //            int meterReading = int.TryParse(row[header_MeterReading].ToString(), out meterReading) ? meterReading : -1;

            //            uProRecord.time = time;
            //            uProRecord.production_operator = proOperator;
            //            uProRecord.meter_reading = meterReading;

            //            if(meterReading != -1)
            //            {
            //                if (!dalProRecord.InsertSheetMeter(uProRecord))
            //                {
            //                    break;
            //                }
            //            }

            //            foreach(DataColumn col in dt_Meter.Columns)
            //            {
            //                string colName = col.ColumnName;

            //                if(colName.Contains(text.Header_DefectRemark))
            //                {
            //                    string qtyRejectColName = text.Header_QtyReject + colName.Replace(text.Header_DefectRemark, "");

            //                    string defectRemarkData = row[colName].ToString();
            //                    int qtyReject = int.TryParse(row[qtyRejectColName].ToString(), out qtyReject) ? qtyReject : -1;


            //                    if(qtyReject > -1 && !string.IsNullOrEmpty(defectRemarkData))
            //                    {
            //                        uProRecord.defect_remark = defectRemarkData;
            //                        uProRecord.reject_qty = qtyReject;

            //                        if (!dalProRecord.InsertSheetDefectRemark(uProRecord))
            //                        {
            //                            MessageBox.Show("Defect Remark and Qty Reject cannot be saved!");
            //                        }

            //                    }
            //                }
            //            }

            //        }

            //        //remove packaging data from db and insert new data
            //        dalProRecord.DeletePackagingData(uProRecord);

            //        if (DT_CARTON != null)
            //        {
            //            //save packaging data
            //            foreach(DataRow row in DT_CARTON.Rows)
            //            {
            //                uProRecord.packaging_code = row[text.Header_ItemCode].ToString();
            //                uProRecord.packaging_max = Convert.ToInt32(row[text.Header_Packing_Max_Qty].ToString());
            //                uProRecord.packaging_qty = Convert.ToInt32(row[text.Header_Packaging_Qty].ToString());
            //                uProRecord.packaging_stock_out = bool.TryParse(row[text.Header_Packaging_Stock_Out].ToString(), out bool stockOUt) ? stockOUt : true;

            //                if (!dalProRecord.InsertProductionPackaging(uProRecord))
            //                {
            //                    MessageBox.Show("Failed to save packaging data!");
            //                }
            //            }
            //        }

            //        //update lot no
            //        if(JOB_NO > 0)
            //        {
            //            uMac.mac_id = macID;
            //            uMac.mac_lot_no = JOB_NO;
            //            uMac.mac_updated_date = updateTime;
            //            uMac.mac_updated_by = MainDashboard.USER_ID;

            //            if (!dalMac.UpdateLotNo(uMac))
            //            {
            //                MessageBox.Show("Failed to update machine lot no!");
            //            }

            //        }

            //        frmLoading.CloseForm();

            //        MessageBox.Show("Data saved!");
            //        //CheckIfBalanceClear();

            //    }
            //    else
            //    {
            //        MessageBox.Show("Failed to save data");
            //    }
            //    //save meter data

            //}
            ////dt_Trf = dalTrf.Select();
            //frmLoading.CloseForm();
            //return success;
        }

        private void NewSaveAndStock()
        {
            //bool OKToProcess = true;

            //if (CheckIfPreviousRecordExist())
            //{
            //    DialogResult dialogResult = MessageBox.Show("This action will UNDO all the previous record,\nare you sure you want to continue?", "Message",
            //                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dialogResult != DialogResult.Yes)
            //    {
            //        OKToProcess = false;
            //    }
            //}

            //if (OKToProcess && SaveSuccess())
            //{
            //    UndoPreviousRecordIfExist();

            //    DataTable dt = NewStockInTable();
            //    DataRow dt_Row;

            //    //get totalStockIn qty from box qty and max pcs per box
            //    //check if one or multi packaging box

            //    int totalStockIn = 0;
            //    int directStockIn = int.TryParse(txtStockInPcsQty.Text, out directStockIn) ? directStockIn : 0;
            //    int directStockOut = int.TryParse(txtOut.Text, out directStockOut) ? directStockOut : 0;

            //    int selectedItem = dgvItemList.CurrentCell.RowIndex;

            //    string planStatus = dgvItemList.Rows[selectedItem].Cells[header_Status].Value.ToString();
            //    int balance = int.TryParse(txtBalanceOfThisShift.Text, out balance) ? balance : 0;

            //    int totalShot = int.TryParse(txtTotalShot.Text, out totalShot) ? totalShot : 0;


            //    if (DT_CARTON != null && DT_CARTON.Rows.Count >= 2)
            //    {
            //        int totalFullBox = 0;

            //        foreach (DataRow row in DT_CARTON.Rows)
            //        {
            //            string packingCode = row[text.Header_ItemCode].ToString();

            //            string itemType = packingCode.Substring(0, 3);

            //            int boxQty = Convert.ToInt32(row[text.Header_Packaging_Qty].ToString());
            //            int maxQty = Convert.ToInt32(row[text.Header_Packing_Max_Qty].ToString());

            //            bool cartonStockOut = bool.TryParse(row[text.Header_Packaging_Stock_Out].ToString(), out cartonStockOut) ? cartonStockOut : true;

            //            totalFullBox += boxQty;

            //            totalStockIn += boxQty * maxQty;

            //            if (!string.IsNullOrEmpty(packingCode) && boxQty > 0 && cartonStockOut)
            //            {


            //                dt_Row = dt.NewRow();
            //                dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
            //                dt_Row[header_ItemCode] = packingCode;
            //                dt_Row[header_Cat] = tool.getItemCat(packingCode);

            //                dt_Row[header_From] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();
            //                dt_Row[header_To] = text.Production;

            //                dt_Row[header_Qty] = boxQty;
            //                dt_Row[text.Header_JobNo] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            //                if (cbMorning.Checked)
            //                {
            //                    dt_Row[header_Shift] = "M";
            //                }
            //                else if (cbNight.Checked)
            //                {
            //                    dt_Row[header_Shift] = "N";
            //                }


            //                dt.Rows.Add(dt_Row);
            //            }

            //        }
            //    }
            //    else
            //    {

            //        int packingMaxQty = int.TryParse(txtPackingMaxQty.Text, out packingMaxQty) ? packingMaxQty : 0;

            //        int fullBoxQty = int.TryParse(txtFullBox.Text, out fullBoxQty) ? fullBoxQty : 0;

            //        bool cartonStockOut = true;

            //        if (DT_CARTON != null)
            //        {
            //            foreach (DataRow row in DT_CARTON.Rows)
            //            {
            //                string packingCode = row[text.Header_ItemCode].ToString();

            //                if (cmbPackingCode.Text.Equals(packingCode))
            //                    cartonStockOut = bool.TryParse(row[text.Header_Packaging_Stock_Out].ToString(), out cartonStockOut) ? cartonStockOut : true;
            //            }
            //        }

            //        totalStockIn += packingMaxQty * fullBoxQty;

            //        if (!string.IsNullOrEmpty(cmbPackingCode.Text) && fullBoxQty > 0 && cartonStockOut)
            //        {
            //            dt_Row = dt.NewRow();
            //            dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
            //            dt_Row[header_ItemCode] = cmbPackingCode.Text;
            //            dt_Row[header_Cat] = tool.getItemCat(cmbPackingCode.Text);

            //            dt_Row[header_From] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();
            //            dt_Row[header_To] = text.Production;

            //            dt_Row[header_Qty] = fullBoxQty;
            //            dt_Row[text.Header_JobNo] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            //            if (cbMorning.Checked)
            //            {
            //                dt_Row[header_Shift] = "M";
            //            }
            //            else if (cbNight.Checked)
            //            {
            //                dt_Row[header_Shift] = "N";
            //            }

            //            dt.Rows.Add(dt_Row);
            //        }

            //    }

            //    if (!string.IsNullOrEmpty(lblPartCode.Text))
            //    {
            //        string ProductionOrAssembly = text.Production;
            //        string StockInItemCode = lblPartCode.Text;


            //        if (!string.IsNullOrEmpty(cmbParentList.Text))
            //        {
            //            if (totalStockIn > 0)
            //            {
            //                dt_Row = dt.NewRow();
            //                dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
            //                dt_Row[header_ItemCode] = StockInItemCode;
            //                dt_Row[header_Cat] = text.Cat_Part;

            //                dt_Row[header_From] = text.Production;
            //                dt_Row[header_To] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();

            //                dt_Row[header_Qty] = totalStockIn;
            //                dt_Row[text.Header_JobNo] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            //                if (cbMorning.Checked)
            //                {
            //                    dt_Row[header_Shift] = "M";
            //                }
            //                else if (cbNight.Checked)
            //                {
            //                    dt_Row[header_Shift] = "N";
            //                }

            //                dt.Rows.Add(dt_Row);
            //            }

            //            if (directStockIn > 0)
            //            {
            //                dt_Row = dt.NewRow();
            //                dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
            //                dt_Row[header_ItemCode] = StockInItemCode;
            //                dt_Row[header_Cat] = text.Cat_Part;

            //                dt_Row[header_From] = text.Production;
            //                dt_Row[header_To] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();

            //                dt_Row[header_Qty] = directStockIn;
            //                dt_Row[text.Header_JobNo] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            //                if (planStatus == text.planning_status_completed)
            //                {
            //                    if (cbMorning.Checked)
            //                    {
            //                        dt_Row[header_Shift] = "MB";
            //                    }
            //                    else if (cbNight.Checked)
            //                    {
            //                        dt_Row[header_Shift] = "NB";
            //                    }
            //                }
            //                else
            //                {
            //                    if (cbMorning.Checked)
            //                    {
            //                        dt_Row[header_Shift] = "M";
            //                    }
            //                    else if (cbNight.Checked)
            //                    {
            //                        dt_Row[header_Shift] = "N";
            //                    }
            //                }


            //                dt.Rows.Add(dt_Row);
            //            }

            //            if (directStockOut > 0)
            //            {
            //                dt_Row = dt.NewRow();
            //                dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
            //                dt_Row[header_ItemCode] = StockInItemCode;
            //                dt_Row[header_Cat] = text.Cat_Part;

            //                dt_Row[header_From] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();
            //                dt_Row[header_To] = text.Other;

            //                dt_Row[header_Qty] = directStockOut;
            //                dt_Row[text.Header_JobNo] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            //                if (cbMorning.Checked)
            //                {
            //                    dt_Row[header_Shift] = "MBO";
            //                }
            //                else if (cbNight.Checked)
            //                {
            //                    dt_Row[header_Shift] = "NBO";
            //                }

            //                dt.Rows.Add(dt_Row);
            //            }

            //            StockInItemCode = cmbParentList.Text;
            //            ProductionOrAssembly = text.Assembly;
            //            float joinQty = tool.getJoinQty(StockInItemCode, lblPartCode.Text);

            //            if (joinQty <= 0)
            //            {
            //                joinQty = 1;
            //            }

            //            totalStockIn = totalStockIn / (int)joinQty;
            //        }

            //        if (totalStockIn > 0)
            //        {
            //            dt_Row = dt.NewRow();
            //            dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
            //            dt_Row[header_ItemCode] = StockInItemCode;
            //            dt_Row[header_Cat] = text.Cat_Part;

            //            dt_Row[header_From] = ProductionOrAssembly;
            //            dt_Row[header_To] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();

            //            dt_Row[header_Qty] = totalStockIn;
            //            dt_Row[text.Header_JobNo] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            //            if (cbMorning.Checked)
            //            {
            //                dt_Row[header_Shift] = "M";
            //            }
            //            else if (cbNight.Checked)
            //            {
            //                dt_Row[header_Shift] = "N";
            //            }

            //            dt.Rows.Add(dt_Row);
            //        }

            //        if (directStockIn > 0)
            //        {
            //            dt_Row = dt.NewRow();
            //            dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
            //            dt_Row[header_ItemCode] = StockInItemCode;
            //            dt_Row[header_Cat] = text.Cat_Part;

            //            dt_Row[header_From] = ProductionOrAssembly;
            //            dt_Row[header_To] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();

            //            dt_Row[header_Qty] = directStockIn;
            //            dt_Row[text.Header_JobNo] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            //            if (planStatus == text.planning_status_completed)
            //            {
            //                if (cbMorning.Checked)
            //                {
            //                    dt_Row[header_Shift] = "MB";
            //                }
            //                else if (cbNight.Checked)
            //                {
            //                    dt_Row[header_Shift] = "NB";
            //                }
            //            }
            //            else
            //            {
            //                if (cbMorning.Checked)
            //                {
            //                    dt_Row[header_Shift] = "M";
            //                }
            //                else if (cbNight.Checked)
            //                {
            //                    dt_Row[header_Shift] = "N";
            //                }
            //            }


            //            dt.Rows.Add(dt_Row);
            //        }

            //        if (directStockOut > 0)
            //        {
            //            dt_Row = dt.NewRow();
            //            dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
            //            dt_Row[header_ItemCode] = StockInItemCode;
            //            dt_Row[header_Cat] = text.Cat_Part;

            //            dt_Row[header_From] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();
            //            dt_Row[header_To] = text.Other;

            //            dt_Row[header_Qty] = directStockOut;
            //            dt_Row[text.Header_JobNo] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            //            if (cbMorning.Checked)
            //            {
            //                dt_Row[header_Shift] = "MBO";
            //            }
            //            else if (cbNight.Checked)
            //            {
            //                dt_Row[header_Shift] = "NBO";
            //            }

            //            dt.Rows.Add(dt_Row);
            //        }

            //        if(totalShot > 0)
            //        {
            //            float totalWeightPerShot = PW_PER_SHOT + RW_PER_SHOT;
            //            float totalweightProduced_kg = totalWeightPerShot * totalShot / 1000;

            //            float raw_Material_Used_kg = totalweightProduced_kg / (1 + COLOR_MAT_RATE);

            //            float color_Material_Used_kg = raw_Material_Used_kg * COLOR_MAT_RATE;

            //            if (!string.IsNullOrEmpty(RAW_MAT_CODE) && SearchItem(dt_ItemInfo, RAW_MAT_CODE, dalItem.ItemCode))
            //            {
            //                dt_Row = dt.NewRow();
            //                dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
            //                dt_Row[header_ItemCode] = RAW_MAT_CODE;
            //                dt_Row[header_Cat] = text.Cat_RawMat;

            //                dt_Row[header_From] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();
            //                dt_Row[header_To] = text.Production;


            //                dt_Row[header_Qty] = raw_Material_Used_kg;

            //                dt_Row[text.Header_JobNo] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            //                if (cbMorning.Checked)
            //                {
            //                    dt_Row[header_Shift] = "M";
            //                }
            //                else if (cbNight.Checked)
            //                {
            //                    dt_Row[header_Shift] = "N";
            //                }

            //                dt.Rows.Add(dt_Row);
            //            }

            //            if (!string.IsNullOrEmpty(COLOR_MAT_CODE) && SearchItem(dt_ItemInfo, COLOR_MAT_CODE, dalItem.ItemCode))
            //            {
            //                dt_Row = dt.NewRow();
            //                dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");

            //                dt_Row[header_ItemCode] = COLOR_MAT_CODE;

            //                dt_Row[header_Cat] = tool.getCatNameFromDataTable(dt_ItemInfo, COLOR_MAT_CODE);

            //                dt_Row[header_From] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();
            //                dt_Row[header_To] = text.Production;


            //                dt_Row[header_Qty] = color_Material_Used_kg;

            //                dt_Row[text.Header_JobNo] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            //                if (cbMorning.Checked)
            //                {
            //                    dt_Row[header_Shift] = "M";
            //                }
            //                else if (cbNight.Checked)
            //                {
            //                    dt_Row[header_Shift] = "N";
            //                }

            //                dt.Rows.Add(dt_Row);
            //            }

            //        }
            //    }

            //    #region process to in/out edit page

            //    frmInOutEdit frm = new frmInOutEdit(dt, true);
            //    frm.StartPosition = FormStartPosition.CenterScreen;
            //    frm.WindowState = FormWindowState.Normal;
            //    frm.ShowDialog();//Item Edit

            //    if (!frmInOutEdit.TrfSuccess)
            //    {
            //        MessageBox.Show("Transfer failed or cancelled!");
            //    }

            //    ResetInputField();
            //    #endregion
            //}
        }

        private bool FORM_LOADED = false;

        private void frmJobSheetRecord_Load(object sender, EventArgs e)
        {
            FORM_LOADED = true;
        }


        private void OnlyNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        }

        private void btnSaveAndStock_Click(object sender, EventArgs e)
        {
            NewSaveAndStock();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (SaveSuccess())
            {
                _ = MessageBox.Show("Job sheet record saved!");
                Close();
            }
        }

        private void txtBalanceOfThisShift_TextChanged(object sender, EventArgs e)
        {

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
            int totalShot = int.TryParse(txtTotalShot.Text, out totalShot) ? totalShot : 0;

            if(totalShot > 0)
            {
                TimeSpan difference = dtpTimeEnd.Value - dtpTimeStart.Value;

                double totalHours = difference.TotalHours;

                totalHours = totalHours < 0 ? totalHours * -1 : totalHours;

                lblAvgHourlyShot.Text = "Avg Hourly Shot: " + ((int)(totalShot / totalHours)).ToString();
            }
            else
            {
                lblAvgHourlyShot.Text = "Avg Hourly Shot: ERROR";
            }
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
            }

        }




        private void cbEndProduction_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEndProduction.Checked)
            {
                txtNote.Text = txtNote.Text + "[END PRODUCTION]";
            }
            else
            {
                txtNote.Text = txtNote.Text.Replace("[END PRODUCTION]", "");
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

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dtpTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            calculateAvgHourlyShot();
        }

        private void dtpTimeStart_ValueChanged(object sender, EventArgs e)
        {
            calculateAvgHourlyShot();
        }
    }
}
