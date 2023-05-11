using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using iTextSharp.text.pdf;

namespace FactoryManagementSoftware.UI
{
    public partial class frmJobSheetViewMode : Form
    {
        public frmJobSheetViewMode(string JobNo, string SheetID, string itemCode)
        {

            ReportViewMode_JobNo = JobNo;
            ReportViewMode_SheetID = SheetID;
            ReportViewMode_PartCode = itemCode;
            InitializeComponent();
            AutoScroll = true;
            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
            lblJobNo.Visible = true;

            loadPackingData();
            CreateMeterReadingData();

            tool.DoubleBuffered(dgvRecordHistory, true);
            tool.DoubleBuffered(dgvMeterReading, true);

            dt_ItemInfo = dalItem.Search(itemCode);

            dt_JoinInfo = dalJoin.SelectAll();
            dt_ProductionRecord = dalProRecord.Select();
            dt_Mac = dalMac.Select();
        }

        readonly private string ReportViewMode_JobNo = "";
        readonly private string ReportViewMode_SheetID = "";
        readonly private string ReportViewMode_PartCode = "";
        readonly private string ReportViewMode_PartName = "";
        readonly private string ReportViewMode_Mac = "";
        readonly private string ReportViewMode_JobStatus = "";


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

        private bool ProductionReportViewMode = false;

        private DateTime OLD_PRO_DATE = DateTime.MaxValue;

        int userPermission = -1;
        readonly private string string_NewSheet = "NEW";
        readonly private string string_Multi = "MULTI";
        readonly private string string_MultiPackaging = "MULTI PACKAGING";

        readonly private string header_Time = "TIME";
        readonly private string header_Operator = "OPERATOR";
        readonly private string header_MeterReading = "METER READING";
        readonly private string header_Hourly = "HOURLY";

        //readonly private string header_ProLotNo = "PRO LOT NO";
        readonly private string header_Machine = "MAC.";
        readonly private string header_Factory = "FAC.";
        readonly private string header_JobNo = "JOB NO.";
        readonly private string header_PartName = "NAME";
        readonly private string header_PartCode = "CODE";
        readonly private string header_Status = "STATUS";
        readonly private string header_SheetID = "SHEET ID";
        readonly private string header_ProductionDate = "PRO. DATE";
        readonly private string header_Shift = "SHIFT";
        readonly private string header_ProducedQty = "PRODUCED QTY";
        readonly private string header_StockIn = "STOCK IN";
        readonly private string header_UpdatedDate = "UPDATED DATE";
        readonly private string header_UpdatedBy = "UPDATED BY";
        readonly private string header_TotalProduced = "TOTAL PRODUCED";
        readonly private string header_TotalStockIn = "TOTAL STOCK IN";

        readonly static public string header_PackagingCode = "CODE";
        readonly static public string header_PackagingName = "NAME";
        readonly static public string header_PackagingQty = "CARTON QTY";
        readonly static public string header_PackagingMax = "PART QTY PER BOX";
        readonly static public string header_PackagingStockOut = "STOCK OUT";

        readonly private string text_RemoveRecord = "Remove from this list.";
        readonly private string text_ChangeJobNo = "Change Job To";
        readonly private string string_CheckBy= "CHECK BY: ";
        readonly private string string_CheckDate = "DATE: ";

        readonly string header_Cat = "Cat";
        readonly string header_ProDate = "DATE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_From = "FROM";
        readonly string header_To = "TO";
        readonly string header_Qty = "QTY";

        private int macID = 0;
        private int JOB_NO = 0;

        private bool loaded = false;
        private bool addingNewSheet = false;
        private bool sheetLoaded = false;
        private bool dailyRecordLoaded = false;
        private bool stopCheckedChange = false;
        private bool dataSaved = true;
        private bool balanceOutEdited = false;
        private bool balanceInEdited = false;

        private DataTable NewPackagingTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_PackagingCode, typeof(string));
            dt.Columns.Add(header_PackagingName, typeof(string));
            dt.Columns.Add(header_PackagingMax, typeof(int));
            dt.Columns.Add(header_PackagingQty, typeof(int));

            return dt;
        }

        private DataTable NewCartonTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_PackagingMax, typeof(int));
            dt.Columns.Add(header_PackagingCode, typeof(string));
            dt.Columns.Add(header_PackagingName, typeof(string));
            dt.Columns.Add(header_PackagingQty, typeof(int));
            dt.Columns.Add(header_PackagingStockOut, typeof(bool));

            return dt;
        }


        private DataTable NewItemListTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Machine, typeof(int));
            dt.Columns.Add(header_Factory, typeof(string));
            dt.Columns.Add(header_PartName, typeof(string));
            dt.Columns.Add(header_PartCode, typeof(string));
            dt.Columns.Add(header_Status, typeof(string));
            dt.Columns.Add(header_JobNo, typeof(int));

            return dt;
        }

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

        private DataTable NewSheetRecordTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_SheetID, typeof(int));
            dt.Columns.Add(header_ProductionDate, typeof(DateTime));
            dt.Columns.Add(header_JobNo, typeof(string));
            dt.Columns.Add(header_Shift, typeof(string));
            dt.Columns.Add(header_ProducedQty, typeof(double));
            dt.Columns.Add(header_StockIn, typeof(double));
            dt.Columns.Add(header_UpdatedDate, typeof(DateTime));
            dt.Columns.Add(header_UpdatedBy, typeof(string));
            dt.Columns.Add(header_TotalProduced, typeof(double));
            dt.Columns.Add(header_TotalStockIn, typeof(double));

            return dt;
        }

        private DataTable NewMeterReadingTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Time, typeof(string));
            dt.Columns.Add(header_Operator, typeof(string));
            dt.Columns.Add(header_MeterReading, typeof(double));
            dt.Columns.Add(header_Hourly, typeof(double));

            dt.Columns.Add(text.Header_DefectRemark, typeof(string));
            dt.Columns.Add(text.Header_QtyReject, typeof(int));

            return dt;
        }

        private void dgvMeterStyleEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

            dgv.Columns[header_Time].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_Operator].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_MeterReading].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Hourly].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[header_Operator].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgv.Columns[header_Operator].DefaultCellStyle.BackColor = SystemColors.Info;
            dgv.Columns[header_MeterReading].DefaultCellStyle.BackColor = SystemColors.Info;

            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                string colName = dgv.Columns[i].Name;

                if (colName.Contains(text.Header_DefectRemark))
                {
                    dgv.Columns[colName].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                    dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[colName].ReadOnly = true;
                }
                else if (colName.Contains(text.Header_QtyReject))
                {
                    dgv.Columns[colName].DefaultCellStyle.BackColor = SystemColors.Info;
                    dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }

            dgv.Columns[header_Time].Frozen = true;
        }

        private void dgvItemListStyleEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.Columns[header_Machine].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Factory].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_JobNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Status].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_PartName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_PartCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        }

        private void dgvSheetRecordStyleEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            dgv.Columns[header_SheetID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ProductionDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Shift].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ProducedQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_UpdatedDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_UpdatedBy].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_TotalProduced].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_StockIn].DefaultCellStyle.BackColor = SystemColors.Info;

            dgv.Columns[header_JobNo].Visible = false;
            dgv.Columns[header_TotalProduced].Visible = false;
            dgv.Columns[header_TotalStockIn].Visible = false;
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

            if(string.IsNullOrEmpty(date))
            {
                errorProvider1.SetError(lblDate, "Please select a production's date");
                passed = false;
            }

            if(!cbMorning.Checked && !cbNight.Checked)
            {
                errorProvider2.SetError(lblShift, "Please select shift");
                passed = false;
            }

            if(string.IsNullOrEmpty(lblPartName.Text))
            {
                errorProvider3.SetError(lblPartNameHeader, "Please select a item on the left table");
                passed = false;
            }

            if (string.IsNullOrEmpty(lblPartCode.Text))
            {
                errorProvider4.SetError(lblPartCodeHeader, "Please select a item on the left table");
                passed = false;
            }

            if (string.IsNullOrEmpty(txtMeterStart.Text))
            {
                errorProvider5.SetError(lblMeterStartAt, "Please input meter start value");
                passed = false;
            }

            if (string.IsNullOrEmpty(txtBalanceOfLastShift.Text))
            {
                errorProvider6.SetError(lblBalanceLast, "Please input balance of last shift");
                passed = false;
            }

            if (string.IsNullOrEmpty(txtBalanceOfThisShift.Text))
            {
                errorProvider7.SetError(lblBalanceCurrent, "Please input balance of this shift");
                passed = false;
            }

            if (string.IsNullOrEmpty(txtFullBox.Text))
            {
                errorProvider8.SetError(lblFullCarton, "Please input full carton qty");
                passed = false;
            }

            if (string.IsNullOrEmpty(txtTotalProduce.Text))
            {
                errorProvider9.SetError(lblTotalStockIn, "Please input total stock in qty");
                passed = false;
            }

            if (string.IsNullOrEmpty(txtPackingMaxQty.Text))
            {
                errorProvider10.SetError(lblPacking, "Please input total qty per box");
                passed = false;
            }

            return passed;
        }

        private void CheckIfStockIn()
        {
            if (dgvRecordHistory.DataSource != null)
            {
                DataTable dt = (DataTable)dgvRecordHistory.DataSource;
                
                dt.DefaultView.Sort = header_ProductionDate + " ASC";
                dt = dt.DefaultView.ToTable();

                DateTime previousDate = DateTime.MaxValue;
                int totalProducedQty = 0;
                int totalStockIn = 0;
                string itemCode = ReportViewMode_PartCode;
                string JobNo = ReportViewMode_JobNo;
                DataTable transferData = dalTrf.codeLikeSearch(itemCode);//88ms

                foreach (DataRow row in dt.Rows)
                {
                    DateTime proDate = Convert.ToDateTime(row[header_ProductionDate].ToString());
                    int producedQty = int.TryParse(row[header_ProducedQty].ToString(), out producedQty) ? producedQty : 0;

                    if(previousDate == DateTime.MaxValue)
                    {
                        previousDate = proDate;
                        totalProducedQty += producedQty;
                    }
                    else if(previousDate == proDate)
                    {
                        totalProducedQty += producedQty;
                    }
                    else
                    {
                        foreach(DataGridViewRow dgvRow in dgvRecordHistory.Rows)
                        {
                            if(previousDate == Convert.ToDateTime(dgvRow.Cells[header_ProductionDate].Value.ToString()))
                            {
                                //check if parent
                                string sheetID = dgvRow.Cells[header_SheetID].Value.ToString();

                               // transferData = dalTrf.codeLikeSearch(itemCode);

                                int trfQty = tool.TotalProductionStockInInOneDay(transferData, itemCode, previousDate, JobNo, dgvRow.Cells[header_Shift].Value.ToString());
                               
                                if (trfQty == -1)
                                {
                                    dgvRow.Cells[header_SheetID].Style.BackColor = Color.Pink;
                                }
                                else if (totalProducedQty == trfQty)
                                {
                                    totalStockIn += trfQty;
                                    dgvRow.Cells[header_SheetID].Style.BackColor = Color.LightGreen;
                                    dgvRow.Cells[header_StockIn].Value = trfQty;
                                }
                                else if (totalProducedQty != trfQty)
                                {
                                    totalStockIn += trfQty;
                                    dgvRow.Cells[header_SheetID].Style.BackColor = Color.LightBlue;
                                    dgvRow.Cells[header_StockIn].Value = trfQty;
                                }
                            }
                        }

                        previousDate = proDate;
                        totalProducedQty = producedQty;
                    }

                }
                //^575ms > 122ms(21/3)

                //change last row color
                foreach (DataGridViewRow dgvRow in dgvRecordHistory.Rows)
                {
                    if (previousDate == Convert.ToDateTime(dgvRow.Cells[header_ProductionDate].Value.ToString()))
                    {
                        //check if parent
                        string sheetID = dgvRow.Cells[header_SheetID].Value.ToString();

                        //transferData = dalTrf.codeLikeSearch(itemCode);

                        int trfQty_ = tool.TotalProductionStockInInOneDay(transferData, itemCode, previousDate, JobNo, dgvRow.Cells[header_Shift].Value.ToString());

                        if (trfQty_ == -1)
                        {
                            dgvRow.Cells[header_SheetID].Style.BackColor = Color.Pink;
                        }
                        else if (totalProducedQty == trfQty_)
                        {
                            totalStockIn += trfQty_;
                            dgvRow.Cells[header_SheetID].Style.BackColor = Color.LightGreen;
                            dgvRow.Cells[header_StockIn].Value = trfQty_;
                            dgvRow.Cells[header_TotalStockIn].Value = totalStockIn;
                        }
                        else if (totalProducedQty != trfQty_)
                        {
                            totalStockIn += trfQty_;
                            dgvRow.Cells[header_SheetID].Style.BackColor = Color.LightBlue;
                            dgvRow.Cells[header_StockIn].Value = trfQty_;
                            dgvRow.Cells[header_TotalStockIn].Value = totalStockIn;
                        }
                    }
                }

               //^148ms > 36ms(21/3)
                txtTotalStockInRecord.Text = totalStockIn.ToString();
            }

        }

        private void ProducedVSTargetQty()
        {
            int totalProduced = int.TryParse(txtTotalProducedRecord.Text, out int x) ? x : 0;

            int targetQty = int.TryParse(txtTargetQty.Text, out x) ? x : 0;

            if (totalProduced >= targetQty)
            {
                txtTargetQty.BackColor = Color.LightGreen;
            }
            else
            {
                txtTargetQty.BackColor = Color.White;

            }
        }

        private void LoadDailyRecord()
        {
            dailyRecordLoaded = false;

            //btnShowDailyRecord.Visible = true;
            string JobNo = ReportViewMode_JobNo;

            macID = int.TryParse(ReportViewMode_Mac, out int i) ? i:-1 ;

            DataTable dt = NewSheetRecordTable();
            DataRow dt_Row;

            int totalProducedQty = 0;
            foreach (DataRow row in dt_ProductionRecord.Rows)
            {
                bool active = Convert.ToBoolean(row[dalProRecord.Active].ToString());
                if (JobNo == row[dalProRecord.JobNo].ToString() && active)
                {
                    txtTargetQty.Text = row[dalPlan.targetQty].ToString();
                    macID = int.TryParse(row[dalPlan.machineID].ToString(), out int x)? x : -1;

                    txtMac.Text = macID.ToString();
                    dt_Row = dt.NewRow();

                    int produced = Convert.ToInt32(row[dalProRecord.TotalProduced].ToString());
                    int proLotNo = int.TryParse(row[dalProRecord.ProLotNo].ToString(), out proLotNo) ? proLotNo : -1;
                    totalProducedQty += produced;



                    if (proLotNo != -1)
                    {
                        // dt_Row[header_JobNo] = SetAlphabetToProLotNo(macID, proLotNo);
                        dt_Row[header_JobNo] = JobNo;
                    }

                    dt_Row[header_SheetID] = row[dalProRecord.SheetID].ToString();
                    dt_Row[header_ProductionDate] = row[dalProRecord.ProDate].ToString();
                    dt_Row[header_Shift] = row[dalProRecord.Shift].ToString();
                    dt_Row[header_ProducedQty] = produced;
                    dt_Row[header_UpdatedDate] = row[dalProRecord.UpdatedDate].ToString();
                    dt_Row[header_UpdatedBy] = dalUser.getUsername(Convert.ToInt32(row[dalProRecord.UpdatedBy].ToString()));

                    dt_Row[header_TotalProduced] = totalProducedQty;

                    if (dt.Rows.Count > 0)
                        dt.Rows[dt.Rows.Count - 1][header_TotalProduced] = DBNull.Value;

                    dt.Rows.Add(dt_Row);



                }
            }

            txtTotalProducedRecord.Text = totalProducedQty.ToString();
            //update total produced
            uPlan.plan_id = Convert.ToInt32(JobNo);
            uPlan.plan_produced = totalProducedQty;
            if (!dalPlan.TotalProducedUpdate(uPlan))
            {
                MessageBox.Show("Failed to update total produced qty!");
            }

            dgvRecordHistory.DataSource = null;

            dgvRecordHistory.DataSource = dt;

            dgvSheetRecordStyleEdit(dgvRecordHistory);
            dgvRecordHistory.ClearSelection();

           

            //load checked status
            //LoadCheckedStatus(JobNo, planStatus);

            CheckIfStockIn();
            ProducedVSTargetQty();

        }

        private void LoadCheckedStatus(string planID, string planStatus)
        {
            if(planStatus == text.planning_status_completed)
            {
                
                DataTable dt = dalPlan.idSearch(planID);
                foreach (DataRow row in dt.Rows)
                {
                    if (planID == row[dalPlan.planID].ToString())
                    {
                        bool Checked = bool.TryParse(row[dalPlan.Checked].ToString(), out Checked) ? Checked : false;

                        cbChecked.Visible = true;
                        if (Checked)
                        {
                            stopCheckedChange = true;
                            cbChecked.Checked = true;
                            stopCheckedChange = false;

                            int checkedBy = int.TryParse(row[dalPlan.CheckedBy].ToString(), out checkedBy) ? checkedBy : -1;
                            string userName = dalUser.getUsername(checkedBy);
                            string checkedDate = row[dalPlan.CHeckedDate].ToString();

                            lblCheckBy.Text = string_CheckBy + userName;
                            lblCheckDate.Text = string_CheckDate + checkedDate;
                        }
                        else
                        {
                            stopCheckedChange = true;
                            cbChecked.Checked = false;
                            stopCheckedChange = false;

                            lblCheckBy.Text = "";
                            lblCheckDate.Text = "";
                        }

                        break;
                    }

                }
            }
            else
            {
                cbChecked.Visible = false;

                lblCheckBy.Text = "";
                lblCheckDate.Text = "";
            }
          
        }

        private void LoadSheetRecordHistoryData()
        {
            DataGridView dgv = dgvRecordHistory;
            DataTable dt = NewSheetRecordTable();

            dgv.DataSource = dt;
        }

        private void LoadParentList(string itemCode)
        {
            DataTable dt_Parent = GetParentList(itemCode);

            if (dt_Parent.Rows.Count > 0)
            {
                dt_Parent.DefaultView.Sort = "Parent ASC";

                cmbParentList.DataSource = dt_Parent;
                cmbParentList.DisplayMember = "Parent";
                cmbParentList.SelectedIndex = -1;
            }
        
            else
            {
                cmbParentList.Enabled = false;
            }
        }

        private DataTable GetParentList(string itemCode)
        {
            DataTable dtJoin = dalJoin.loadParentList(itemCode);
            DataTable dt = new DataTable();

            if (dtJoin.Rows.Count > 0)
            {
                cmbParentList.Enabled = true;
               
                dt.Columns.Add("Parent");

                foreach (DataRow row in dtJoin.Rows)
                {
                    bool duplicate = false;
                    string parentCode = row[dalJoin.JoinParent].ToString();

                    DataTable dt_GrandParentList = GetParentList(parentCode);

                    if (dt_GrandParentList.Rows.Count > 0)
                    {
                        dt.Merge(dt_GrandParentList);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row2 in dt.Rows)
                        {
                            string list_ParentCode = row2["Parent"].ToString();

                            if (list_ParentCode == parentCode)
                            {
                                duplicate = true;
                                break;
                            }
                        }
                    }

                    if (!duplicate)
                    {
                        dt.Rows.Add(parentCode);
                    }

                }

               
            }
           
            return dt;
        }

        private void LoadCartonSetting()
        {
            //get sheet Id
            string itemCode = lblPartCode.Text;
            string sheetID = txtSheetID.Text;
            int MainCartonQty = 0;
            txtFullBox.Enabled = true;

            DT_CARTON = null;

            if (string.IsNullOrEmpty(sheetID) || sheetID.Equals(string_NewSheet))
            {
                //new sheet
                //find main carton from item group and load to dt_carton
                foreach(DataRow row in dt_JoinInfo.Rows)
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

                        newRow[header_PackagingCode] = cartonCode;
                        newRow[header_PackagingName] = tool.getItemName(cartonCode);
                        newRow[header_PackagingMax] = MaxQty;
                        newRow[header_PackagingQty] = childQty;
                        newRow[header_PackagingStockOut] = StockOut;

                        DT_CARTON.Rows.Add(newRow);

                    }
                }
            }
            else
            {
                //old sheet
                //load carton data from carton db to dt_carton
                DataTable db_carton = dalProRecord.PackagingRecordSelect(int.TryParse(sheetID, out int i)? i:0);

                if(db_carton != null && db_carton.Rows.Count > 0)
                {
                    foreach(DataRow row in db_carton.Rows)
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

                        newRow[header_PackagingCode] = cartonCode;
                        newRow[header_PackagingName] = tool.getItemName(cartonCode);
                        newRow[header_PackagingMax] = MaxQty;
                        newRow[header_PackagingQty] = childQty;
                        newRow[header_PackagingStockOut] = cartonStockOut;

                        DT_CARTON.Rows.Add(newRow);
                    }
                }

            }

            string AlertMessage = "";

            if (DT_CARTON != null &&  DT_CARTON.Rows.Count > 0)
            {
                int totalStockIn = 0;
                int totalFullBox = 0;

                string name = DT_CARTON.Rows[0][header_PackagingName].ToString();
                string code = DT_CARTON.Rows[0][header_PartCode].ToString();

                int rowCount = DT_CARTON.Rows.Count;

                if (rowCount >= 2)
                {
                    LoadPackingToCMB(string_MultiPackaging);
                    txtPackingMaxQty.Text = "MULTI";

                    txtFullBox.Enabled = false;
                  

                    foreach (DataRow row in DT_CARTON.Rows)
                    {
                        int boxQty = int.TryParse(row[header_PackagingQty].ToString(), out boxQty) ? boxQty : 0;
                        int maxQty = int.TryParse(row[header_PackagingMax].ToString(), out maxQty) ? maxQty : 0;

                        totalFullBox += boxQty;

                        totalStockIn += boxQty * maxQty;
                    }


                    txtFullBox.Text = totalFullBox.ToString();

                }
                else if (rowCount == 1)
                {
                    loadPackingData();

                    cmbPackingName.Text = name;
                    cmbPackingCode.Text = code;

                    foreach (DataRow row in DT_CARTON.Rows)
                    {
                        txtPackingMaxQty.Text = row[header_PackagingMax].ToString();
                        txtFullBox.Text = row[header_PackagingQty].ToString();
                    }


                }
            }
            else
            {
                //txtPackingMaxQty.Text = "";
                cmbPackingName.SelectedIndex = -1;
                cmbPackingCode.SelectedIndex = -1;

                //AlertMessage = "Carton data not found!\nPlease add a main carton to this item at:\n1. Item Group Edit";
            }

            //check if more than 1 main carton
            if(MainCartonQty > 1)
            {
                AlertMessage = "More than 1 main carton detected!\nYou can edit it at:\n1. Carton Setting\n2. Item Group Edit";
            }

            if(!string.IsNullOrEmpty(AlertMessage))
            MessageBox.Show(AlertMessage, "Carton Alert",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void GetPackagingDataForNewSheet(string itemCode)
        {
            DataTable dt = NewPackagingTable();

            //get packaging data
            foreach (DataRow row in dt_JoinInfo.Rows)
            {
                string parentCode = row[dalJoin.JoinParent].ToString();

                if (parentCode == itemCode)
                {
                    string childCode = row[dalJoin.JoinChild].ToString();

                    foreach (DataRow rowItem in dt_ItemInfo.Rows)
                    {
                        if (rowItem[dalItem.ItemCode].ToString().Equals(childCode))
                        {
                            string cat = rowItem[dalItem.ItemCat].ToString();
                            string packagingCode = rowItem[dalItem.ItemCode].ToString();
                            string packagingName = rowItem[dalItem.ItemName].ToString();
                            string packagingQty = int.TryParse(row[dalJoin.JoinMax].ToString(), out int i) ? i.ToString() : "1" ;


                            //ADD DATA TO PACKAGING TABLE
                            DataRow dt_Row = dt.NewRow();

                            int TotalBox = 0;

                            dt_Row[header_PackagingCode] = packagingCode;
                            dt_Row[header_PackagingName] = tool.getItemName(packagingCode);
                            dt_Row[header_PackagingMax] = packagingQty;
                            dt_Row[header_PackagingQty] = TotalBox;

                            dt.Rows.Add(dt_Row);

                            if ((cat.Equals(text.Cat_Carton) && (packagingCode.Contains("CTN") || packagingCode.Contains("CTR"))) || cat.Equals(text.Cat_Packaging))
                            {
                                txtPackingMaxQty.Text = packagingQty;
                                cmbPackingName.Text = packagingName;
                                cmbPackingCode.Text = packagingCode;
                            }
                        }
                    }
                }

            }

            dt_MultiPackaging = dt.Copy();

            if (dt_MultiPackaging.Rows.Count > 0)
            {
                string name = dt_MultiPackaging.Rows[0][header_PackagingName].ToString();
                string code = dt_MultiPackaging.Rows[0][header_PartCode].ToString();

                int rowCount = dt_MultiPackaging.Rows.Count;

                if (rowCount >= 2)
                {
                    LoadPackingToCMB(string_MultiPackaging);
                    txtPackingMaxQty.Text = "MULTI";

                    int totalStockIn = 0;
                    int totalFullBox = 0;

                    foreach (DataRow row in dt_MultiPackaging.Rows)
                    {
                        int boxQty = Convert.ToInt32(row[frmProPackaging.header_PackagingQty].ToString());
                        int maxQty = Convert.ToInt32(row[frmProPackaging.header_PackagingMax].ToString());

                        totalFullBox += boxQty;

                        totalStockIn += boxQty * maxQty;
                    }


                    txtFullBox.Text = totalFullBox.ToString();

                }

                else if (rowCount == 1)
                {
                    loadPackingData();

                    cmbPackingName.Text = name;
                    cmbPackingCode.Text = code;

                    foreach (DataRow row in dt_MultiPackaging.Rows)
                    {
                        txtPackingMaxQty.Text = row[header_PackagingMax].ToString();
                        txtFullBox.Text = row[header_PackagingQty].ToString();
                    }


                }
            }

        }

        private DateTime GetStatusUpdatedDate(string planID)
        {
            DateTime UpdatedDate = DateTime.MaxValue;

            foreach(DataRow row in dt_Plan.Rows)
            {
                if(planID == row[dalPlan.planID].ToString())
                {
                    //UpdatedDate = Convert.ToDateTime();
                    UpdatedDate = DateTime.TryParse(row[dalPlan.planUpdatedDate].ToString(), out UpdatedDate) ? UpdatedDate : DateTime.MaxValue;

                    return UpdatedDate;
                }
            }

            return UpdatedDate;
        }

        private void LoadJobInfo()
        {

            string partName = tool.getItemNameFromDataTable(dt_ItemInfo, ReportViewMode_PartCode);
            string partCode = ReportViewMode_PartCode;
            string jobNo = ReportViewMode_JobNo;

            lblCustomer.Text = "";
            lblPartName.Text = partName;
            lblPartCode.Text = partCode;

            foreach (DataRow row in dt_ItemInfo.Rows)
            {
                if (row[dalItem.ItemCode].ToString().Equals(partCode))
                {
                    float partWeight = float.TryParse(row[dalItem.ItemProPWShot].ToString(), out float i) ? Convert.ToSingle(row[dalItem.ItemProPWShot].ToString()) : -1;
                    float runnerWeight = float.TryParse(row[dalItem.ItemProRWShot].ToString(), out float k) ? Convert.ToSingle(row[dalItem.ItemProRWShot].ToString()) : -1;

                    lblJobNo.Text = jobNo;

                    lblPW.Text = partWeight.ToString("0.##");
                    lblRW.Text = runnerWeight.ToString("0.##");
                    lblCavity.Text = row[dalItem.ItemCavity] == DBNull.Value ? "" : row[dalItem.ItemCavity].ToString();
                    lblCycleTime.Text = row[dalItem.ItemProCTTo] == DBNull.Value ? "" : row[dalItem.ItemProCTTo].ToString();

                    lblRawMat.Text = row[dalItem.ItemMaterial] == DBNull.Value ? "" : row[dalItem.ItemMaterial].ToString();

                    string colorMat = row[dalItem.ItemMBatch] == DBNull.Value ? "" : row[dalItem.ItemMBatch].ToString();

                    float colorRate = row[dalItem.ItemMBRate] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemMBRate]);
                    string colorUsage = (colorRate * 100).ToString("0.##");

                    lblColorMat.Text = colorMat + " ( " + colorUsage + "%)";


                }

            }


        }

        
        private void LoadExistingSheetData()
        {
            sheetLoaded = false;

            int selectedDailyRecord = dgvRecordHistory.CurrentCell.RowIndex;

            string itemName = ReportViewMode_PartName;
            string itemCode = ReportViewMode_PartCode;
            string jobNo = ReportViewMode_JobNo;
            string sheetID = dgvRecordHistory.Rows[selectedDailyRecord].Cells[header_SheetID].Value.ToString();
            string shift = dgvRecordHistory.Rows[selectedDailyRecord].Cells[header_Shift].Value.ToString();

            LoadParentList(itemCode);
            uProRecord.sheet_id = Convert.ToInt32(sheetID);
            DataTable dt_ProductionRecord = dalProRecord.ProductionRecordSelect(uProRecord);

            lblPartName.Text = itemName;
            lblPartCode.Text = itemCode;
            lblJobNo.Text = jobNo;
            txtSheetID.Text = sheetID;

            int cycleTime = int.TryParse(lblCycleTime.Text, out cycleTime) ? cycleTime : 0;

            int IdealHourlyShot = cycleTime > 0 ? 3600 / cycleTime : 0;

            txtIdealHourlyShotQty.Text = IdealHourlyShot > 0 ? IdealHourlyShot.ToString() : "ERROR";

            if (shift == "MORNING")
            {
                cbMorning.Checked = true;
            }
            else if (shift == "NIGHT")
            {
                cbNight.Checked = true;
            }

            string totalRejectQty = "";
            string fullBoxQty = "";

            foreach (DataRow row in dt_ProductionRecord.Rows)
            {
                if (row[dalProRecord.SheetID].ToString().Equals(sheetID))
                {
                    float partWeight = float.TryParse(row[dalPlan.planPW].ToString(), out float i) ? Convert.ToSingle(row[dalPlan.planPW].ToString()) : -1;
                    float runnerWeight = float.TryParse(row[dalPlan.planRW].ToString(), out float k) ? Convert.ToSingle(row[dalPlan.planRW].ToString()) : -1;

                    lblPW.Text = partWeight.ToString("0.##");
                    lblRW.Text = runnerWeight.ToString("0.##");

                    lblCavity.Text = row[dalPlan.planCavity] == DBNull.Value ? "" : row[dalPlan.planCavity].ToString();
                    lblCycleTime.Text = row[dalPlan.planCT] == DBNull.Value ? "" : row[dalPlan.planCT].ToString();

                    lblRawMat.Text = row[dalPlan.materialCode] == DBNull.Value ? "" : row[dalPlan.materialCode].ToString();
                    string colorMat = row[dalPlan.colorMaterialCode] == DBNull.Value ? "" : row[dalPlan.colorMaterialCode].ToString();


                    float colorRate = row[dalPlan.colorMaterialUsage] == DBNull.Value ? 0 : Convert.ToSingle(row[dalPlan.colorMaterialUsage]);
                    string colorUsage = colorRate.ToString("0.##");

                    lblColorMat.Text = colorMat + " ( " + colorUsage + "%)";


                    dtpProDate.Value = Convert.ToDateTime(row[dalProRecord.ProDate].ToString());

                    OLD_PRO_DATE = dtpProDate.Value;

                    int _ProLotNo = int.TryParse(row[dalProRecord.ProLotNo].ToString(), out _ProLotNo) ? _ProLotNo : -1;

                    //if (_ProLotNo != -1)
                    //{
                    //    txtJobNo.Text = SetAlphabetToProLotNo(macID, _ProLotNo);
                    //}
                    //else
                    //{
                    //    txtJobNo.Text = row[dalProRecord.ProLotNo].ToString();
                    //}
                    txtJobNo.Text = jobNo;

                    txtRawMatLotNo.Text = row[dalProRecord.RawMatLotNo].ToString();
                    txtColorMatLotNo.Text = row[dalProRecord.ColorMatLotNo].ToString();
                    txtMeterStart.Text = row[dalProRecord.MeterStart].ToString();
                    txtBalanceOfLastShift.Text = row[dalProRecord.LastShiftBalance].ToString();
                    txtBalanceOfThisShift.Text = row[dalProRecord.CurrentShiftBalance].ToString();
                    txtIn.Text = row[dalProRecord.directIn].ToString();
                    txtOut.Text = row[dalProRecord.directOut].ToString();
                    txtFullBox.Text = row[dalProRecord.FullBox].ToString();

                    fullBoxQty = row[dalProRecord.FullBox].ToString();

                    txtTotalProduce.Text = row[dalProRecord.TotalProduced].ToString();
                    cmbParentList.Text = row[dalProRecord.ParentCode].ToString();
                    txtNote.Text = row[dalProRecord.Note].ToString();
                    txtPackingMaxQty.Text = row[dalProRecord.PackagingQty].ToString();

                    txtMac.Text = macID.ToString();

                    //cmbPackingName.Text = tool.getItemName(row[dalProRecord.PackagingCode].ToString());
                    //cmbPackingCode.Text = row[dalProRecord.PackagingCode].ToString();
                    //txtPackingMaxQty.Text = row[dalProRecord.PackagingQty].ToString();

                    totalRejectQty = row[dalProRecord.TotalActualReject].ToString();
                    //txtActualTotalReject.Text = row[dalProRecord.TotalActualReject].ToString();

                    // txtRejectPercentage.Text = row[dalProRecord.ProLotNo].ToString();

                    break;
                }

            }

            LoadMeterReadingData(Convert.ToInt32(sheetID));

            CalculateHourlyShot();

            LoadCartonSetting();

            txtFullBox.Text = fullBoxQty;
            txtActualTotalReject.Text = totalRejectQty;




            sheetLoaded = true;

        }

        private void LoadExistingSheetData(string sheetID)
        {
            sheetLoaded = false;

            string itemName = ReportViewMode_PartName;
            string itemCode = ReportViewMode_PartCode;
            string jobNo = ReportViewMode_JobNo;

            LoadParentList(itemCode);
            uProRecord.sheet_id = Convert.ToInt32(sheetID);
            DataTable dt_ProductionRecord = dalProRecord.ProductionRecordSelect(uProRecord);

            lblPartName.Text = itemName;
            lblPartCode.Text = itemCode;
            lblJobNo.Text = jobNo;
            txtSheetID.Text = sheetID;

            int cycleTime = int.TryParse(lblCycleTime.Text, out cycleTime) ? cycleTime : 0;

            int IdealHourlyShot = cycleTime > 0 ? 3600 / cycleTime : 0;

            txtIdealHourlyShotQty.Text = IdealHourlyShot > 0 ? IdealHourlyShot.ToString() : "ERROR";

            string totalRejectQty = "";
            string fullBoxQty = "";

            foreach (DataRow row in dt_ProductionRecord.Rows)
            {
                if (row[dalProRecord.SheetID].ToString().Equals(sheetID))
                {
                    string shift = row[dalProRecord.Shift].ToString();

                    if (shift == "MORNING")
                    {
                        cbMorning.Checked = true;
                    }
                    else if (shift == "NIGHT")
                    {
                        cbNight.Checked = true;
                    }

                    float partWeight = float.TryParse(row[dalPlan.planPW].ToString(), out float i) ? Convert.ToSingle(row[dalPlan.planPW].ToString()) : -1;
                    float runnerWeight = float.TryParse(row[dalPlan.planRW].ToString(), out float k) ? Convert.ToSingle(row[dalPlan.planRW].ToString()) : -1;

                    lblPW.Text = partWeight.ToString("0.##");
                    lblRW.Text = runnerWeight.ToString("0.##");

                    lblCavity.Text = row[dalPlan.planCavity] == DBNull.Value ? "" : row[dalPlan.planCavity].ToString();
                    lblCycleTime.Text = row[dalPlan.planCT] == DBNull.Value ? "" : row[dalPlan.planCT].ToString();

                    lblRawMat.Text = row[dalPlan.materialCode] == DBNull.Value ? "" : row[dalPlan.materialCode].ToString();
                    string colorMat = row[dalPlan.colorMaterialCode] == DBNull.Value ? "" : row[dalPlan.colorMaterialCode].ToString();


                    float colorRate = row[dalPlan.colorMaterialUsage] == DBNull.Value ? 0 : Convert.ToSingle(row[dalPlan.colorMaterialUsage]);
                    string colorUsage = colorRate.ToString("0.##");

                    lblColorMat.Text = colorMat + " ( " + colorUsage + "%)";


                    dtpProDate.Value = Convert.ToDateTime(row[dalProRecord.ProDate].ToString());

                    OLD_PRO_DATE = dtpProDate.Value;

                    int _ProLotNo = int.TryParse(row[dalProRecord.ProLotNo].ToString(), out _ProLotNo) ? _ProLotNo : -1;

                    txtJobNo.Text = jobNo;

                    txtRawMatLotNo.Text = row[dalProRecord.RawMatLotNo].ToString();
                    txtColorMatLotNo.Text = row[dalProRecord.ColorMatLotNo].ToString();
                    txtMeterStart.Text = row[dalProRecord.MeterStart].ToString();
                    txtBalanceOfLastShift.Text = row[dalProRecord.LastShiftBalance].ToString();
                    txtBalanceOfThisShift.Text = row[dalProRecord.CurrentShiftBalance].ToString();
                    txtIn.Text = row[dalProRecord.directIn].ToString();
                    txtOut.Text = row[dalProRecord.directOut].ToString();
                    txtFullBox.Text = row[dalProRecord.FullBox].ToString();

                    fullBoxQty = row[dalProRecord.FullBox].ToString();

                    txtTotalProduce.Text = row[dalProRecord.TotalProduced].ToString();
                    cmbParentList.Text = row[dalProRecord.ParentCode].ToString();
                    txtNote.Text = row[dalProRecord.Note].ToString();
                    txtPackingMaxQty.Text = row[dalProRecord.PackagingQty].ToString();

                    txtMac.Text = macID.ToString();

                    //cmbPackingName.Text = tool.getItemName(row[dalProRecord.PackagingCode].ToString());
                    //cmbPackingCode.Text = row[dalProRecord.PackagingCode].ToString();
                    //txtPackingMaxQty.Text = row[dalProRecord.PackagingQty].ToString();

                    totalRejectQty = row[dalProRecord.TotalActualReject].ToString();
                    //txtActualTotalReject.Text = row[dalProRecord.TotalActualReject].ToString();

                    // txtRejectPercentage.Text = row[dalProRecord.ProLotNo].ToString();

                    break;
                }

            }

            LoadMeterReadingData(Convert.ToInt32(sheetID));

            CalculateHourlyShot();

            LoadCartonSetting();

            txtFullBox.Text = fullBoxQty;
            txtActualTotalReject.Text = totalRejectQty;




            sheetLoaded = true;

        }
        private void LoadMeterReadingData(int sheedID)
        {
            CreateMeterReadingData();
            uProRecord.sheet_id = sheedID;
            DataTable dt = dalProRecord.MeterRecordSelect(uProRecord);
            DataTable dt_DefectRemark = dalProRecord.DefectRemarkRecordSelect(uProRecord);
            DataTable dt_Meter = (DataTable)dgvMeterReading.DataSource;

            foreach (DataRow row in dt.Rows)
            {
                string timeFromDB = Convert.ToDateTime(row[dalProRecord.ProTime]).ToShortTimeString();

                foreach(DataRow dgvRow in dt_Meter.Rows)
                {
                    string timeFromDGV = Convert.ToDateTime(dgvRow[header_Time]).ToShortTimeString();

                    if(timeFromDB == timeFromDGV)
                    {
                        string proOperator = row[dalProRecord.ProOperator].ToString();
                        string proMeter = row[dalProRecord.ProMeterReading].ToString();

                        dgvRow[header_Operator] = proOperator;
                        dgvRow[header_MeterReading] = proMeter;

                        break;
                    }
                }
            }

            foreach (DataRow row in dt_DefectRemark.Rows)
            {
                string timeFromDB = Convert.ToDateTime(row[dalProRecord.ProTime]).ToShortTimeString();

                foreach (DataRow dgvRow in dt_Meter.Rows)
                {
                    int defectColumnNo = 1;

                    string timeFromDGV = Convert.ToDateTime(dgvRow[header_Time]).ToShortTimeString();

                    if (timeFromDB == timeFromDGV)
                    {
                        string defectRemark_ColToInsert = "";
                        string QtyReject_ColToInsert = "";

                        foreach (DataColumn dgvCol in dt_Meter.Columns)
                        {
                            string colName = dgvCol.ColumnName;
                            if (colName.Contains(text.Header_DefectRemark))
                            {
                                string DefectNo = colName.Replace(text.Header_DefectRemark, "");

                                string qtyReject_ColName = text.Header_QtyReject + DefectNo;

                                if (!string.IsNullOrEmpty(DefectNo))
                                {
                                    DefectNo = DefectNo.Replace("_", "");

                                    int DefectNo_INT = int.TryParse(DefectNo, out DefectNo_INT) ? DefectNo_INT : -1;

                                    defectColumnNo = DefectNo_INT > defectColumnNo ? DefectNo_INT : defectColumnNo;
                                }

                                string defectRemarkData = dgvRow[colName].ToString();

                                if (string.IsNullOrEmpty(defectRemarkData))
                                {
                                    defectRemark_ColToInsert = colName;
                                    QtyReject_ColToInsert = text.Header_QtyReject + colName.Replace(text.Header_DefectRemark, "");
                                    break;
                                }

                            }
                        }



                        if (string.IsNullOrEmpty(defectRemark_ColToInsert))
                        {
                            //add new columns
                            if (dt_Meter.Columns.Contains(text.Header_DefectRemark))
                            {
                                dt_Meter.Columns[text.Header_DefectRemark].ColumnName = text.Header_DefectRemark + "_1";
                                dt_Meter.Columns[text.Header_QtyReject].ColumnName = text.Header_QtyReject + "_1";

                                defectRemark_ColToInsert = text.Header_DefectRemark + "_2";
                                QtyReject_ColToInsert = text.Header_QtyReject + "_2";
                                dt_Meter.Columns.Add(defectRemark_ColToInsert, typeof(string));
                                dt_Meter.Columns.Add(QtyReject_ColToInsert, typeof(int));
                            }
                            else
                            {
                                defectColumnNo++;

                                defectRemark_ColToInsert = text.Header_DefectRemark + "_" + defectColumnNo;
                                QtyReject_ColToInsert = text.Header_QtyReject + "_" + defectColumnNo;

                                dt_Meter.Columns.Add(defectRemark_ColToInsert, typeof(string));
                                dt_Meter.Columns.Add(QtyReject_ColToInsert, typeof(int));
                            }
                        }

                        string defectRemark = row[dalProRecord.DefectRemark].ToString();
                        string rejectQty = row[dalProRecord.RejectQty].ToString();

                        dgvRow[defectRemark_ColToInsert] = defectRemark;
                        dgvRow[QtyReject_ColToInsert] = rejectQty;



                        break;
                    }
                }


            }

            dgvMeterStyleEdit(dgvMeterReading);

        }

        private void CreateMeterReadingData()
        {
            DataTable dt = NewMeterReadingTable();
            DataRow dt_Row;

            if (cbMorning.Checked || cbNight.Checked)
            {
                dgvMeterReading.DataSource = null;

                bool MorningShift = true;

                if (cbNight.Checked)
                {
                    MorningShift = false;
                }

                if(MorningShift)
                {
                    DateTime dateNow = DateTime.Now;
                    DateTime date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 9, 0, 0);

                    for(int i = 0; i < 12; i++)
                    {
                        dt_Row = dt.NewRow();
                        int time = date.AddHours(i).Hour;

                        DateTime TimeSet = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, time, 0, 0);
                        dt_Row[header_Time] = TimeSet.ToShortTimeString();

                        //TO-DO: read record if exist

                        dt.Rows.Add(dt_Row);
                    }
                }
                else
                {
                    DateTime dateNow = DateTime.Now;
                    DateTime date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 21, 0, 0);

                    for (int i = 0; i < 12; i++)
                    {
                        dt_Row = dt.NewRow();
                        int time = date.AddHours(i).Hour;

                        DateTime TimeSet = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, time, 0, 0);
                        dt_Row[header_Time] = TimeSet.ToShortTimeString();

                        //TO-DO: read record if exist

                        dt.Rows.Add(dt_Row);
                    }
                }

                if(dt.Rows.Count > 0)
                {
                    dgvMeterReading.DataSource = dt;
                    dgvMeterStyleEdit(dgvMeterReading);
                }
            }
            else
            {
               // errorProvider2.SetError(lblShift, "Please select shift");
            }
            
        }


        private void loadPackingData()
        {
            DataTable dt_Packaging = dalItem.CatSearch(text.Cat_Packaging);
            DataTable dt_Packaging_ItemName = dt_Packaging.DefaultView.ToTable(true, "item_name");

            DataTable db_Carton = dalItem.CatSearch(text.Cat_Carton);
            DataTable dt_Carton_ItemName = db_Carton.DefaultView.ToTable(true, "item_name");

            DataTable dt_All = dt_Packaging.Copy();
            dt_All.Merge(db_Carton);

            dt_All.DefaultView.Sort = "item_name ASC";

            cmbPackingName.DataSource = dt_All;
            cmbPackingName.DisplayMember = "item_name";

            cmbPackingName.SelectedIndex = -1;

            cmbPackingCode.DataSource = null;
           
        }

        private void LoadPackingToCMB(string dataToShow)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Data");
            dt.Rows.Add(dataToShow);

            cmbPackingName.DataSource = dt;
            cmbPackingName.DataSource = dt;
            cmbPackingName.DisplayMember = "Data";

            cmbPackingCode.DataSource = dt;
            cmbPackingCode.DisplayMember = "Data";

        }

        private void frmProductionRecord_Load(object sender, EventArgs e)
        {
            LoadJobInfo();
            LoadDailyRecord();

            if (!string.IsNullOrEmpty(ReportViewMode_SheetID) && dgvRecordHistory != null)
            {
                for (int i = 0; i < dgvRecordHistory.Rows.Count; i++)
                {
                    string sheetID = dgvRecordHistory.Rows[i].Cells[header_SheetID].Value.ToString();

                    if (sheetID == ReportViewMode_SheetID)
                    {
                        dgvRecordHistory.Rows[i].Selected = true;
                        dgvRecordHistory.CurrentCell = dgvRecordHistory.Rows[i].Cells[header_SheetID];
                        break;
                    }
                }

                LoadExistingSheetData(ReportViewMode_SheetID);
            }
            else
            {
                dgvRecordHistory.ClearSelection();
            }


        }

        private void txtMeterStart_TextChanged(object sender, EventArgs e)
        {
            if(sheetLoaded)
            {
                dataSaved = false;
            }
            
            errorProvider5.Clear();
            CalculateHourlyShot();
        }

        private void frmProductionRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.NewDailyJobSheetFormOpen = false;
        }

        private void ShowDailyRecordUI()
        {
            LoadJobInfo();
            tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
            tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);

            tlpStockInCheck.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            tlpStockInCheck.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 100f);
            tlpStockInCheck.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 95f);
            tlpStockInCheck.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 95f);
            tlpStockInCheck.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 40f);

        }

        private void ShowProductionListUI()
        {

            tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
            tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
        }

        private void txtTotalStockIn_TextChanged(object sender, EventArgs e)
        {
            if (sheetLoaded)
            {
                dataSaved = false;
            }
            errorProvider9.Clear();
            CalculateTotalRejectAndPercentage();
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

        private void cmbPackingName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tool.loadItemCodeDataToComboBox(cmbPackingCode,cmbPackingName.Text);

            if(sheetLoaded)
            {
                dataSaved = false;
            }
        }

        private void cbMorning_CheckedChanged(object sender, EventArgs e)
        {
           

        }

        private void cbNight_CheckedChanged(object sender, EventArgs e)
        {
            
        }

    

        private void tlpSheet_Paint(object sender, PaintEventArgs e)
        {

        }



    
        private void dgvRecordHistory_SelectionChanged(object sender, EventArgs e)
        {
            if (dailyRecordLoaded)
            {
                errorProvider3.Clear();
                errorProvider4.Clear();

                if(dgvRecordHistory.CurrentCell != null)
                {
                    int rowIndex = dgvRecordHistory.CurrentCell.RowIndex;

                    if (rowIndex >= 0)
                    {
                        LoadExistingSheetData();
                    }
                }
            }  
        }

        private void txtBalanceOfLastShift_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtBalanceOfThisShift_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtFullBox_TextChanged(object sender, EventArgs e)
        {
           
            
        }

      


        private void dgvMeterReading_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void CalculateHourlyShot()
        {
            int meterStart = 0;
            int totalShot = 0;
            int dividend = 0;

            if (string.IsNullOrEmpty(txtMeterStart.Text))
            {
                errorProvider5.Clear();
                //errorProvider5.SetError(lblMeterStartAt, "Please input meter start value");
            }
            else
            {
                meterStart = int.TryParse(txtMeterStart.Text, out meterStart) ? meterStart : 0;
            }

            DataTable dt = (DataTable)dgvMeterReading.DataSource;

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int meterReading = int.TryParse(row[header_MeterReading].ToString(), out meterReading) ? meterReading : -1;

                    if (meterReading != -1)
                    {
                        int hourlyShot = meterReading - meterStart;
                        row[header_Hourly] = hourlyShot;

                        meterStart = meterReading;
                        totalShot += hourlyShot;
                        dividend++;
                    }
                    else
                    {
                        row[header_Hourly] = 0;
                    }
                }
            }

            //CalculateTotalShot();
            txtTotalShot.Text = totalShot.ToString();
            int avgHourlyShot = dividend > 0 ? totalShot / dividend : 0;

            txtActualAvgHourlyShotQty.Text = avgHourlyShot.ToString();

        }

        private void CalculateTotalShot()
        {
            if(true)
            {
                DataTable dt = (DataTable)dgvMeterReading.DataSource;

                int totalShot = 0;

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int hourly = int.TryParse(row[header_Hourly].ToString(), out hourly) ? hourly : 0;

                        totalShot += hourly;
                    }
                }


                txtTotalShot.Text = totalShot.ToString();
            }
        }

        private void CalculateTotalProduce()
        {
            if(true)
            {
                int balanceLeftLastShift = int.TryParse(txtBalanceOfLastShift.Text, out balanceLeftLastShift) ? balanceLeftLastShift : 0;
                int balanceLeftThisShift = int.TryParse(txtBalanceOfThisShift.Text, out balanceLeftThisShift) ? balanceLeftThisShift : 0;

                int directIn = int.TryParse(txtIn.Text, out directIn) ? directIn : 0;
                int directOut = int.TryParse(txtOut.Text, out directOut) ? directOut : 0;

                balanceLeftThisShift += directIn;
                balanceLeftLastShift += directOut;
                if (DT_CARTON != null && DT_CARTON.Rows.Count >= 2)
                {
                    LoadPackingToCMB(string_MultiPackaging);
                    txtPackingMaxQty.Text = "MULTI";

                    int totalStockIn = 0;
                    int totalFullBox = 0;

                    foreach (DataRow row in DT_CARTON.Rows)
                    {
                       
                        string packingCode = row[header_PackagingCode].ToString();

                        if(!string.IsNullOrEmpty(packingCode))
                        {
                            int boxQty = Convert.ToInt32(row[header_PackagingQty].ToString());
                            int maxQty = Convert.ToInt32(row[header_PackagingMax].ToString());

                            string itemType = packingCode.Substring(0, 3);


                            if (itemType == "CTN" || itemType == "CTR")
                            {
                                totalFullBox += boxQty;

                                totalStockIn += boxQty * maxQty;
                            }
                        }
                       

                    }

                    totalStockIn += balanceLeftThisShift - balanceLeftLastShift;
                    txtFullBox.Text = totalFullBox.ToString();
                    txtTotalProduce.Text = totalStockIn.ToString();

                }
                else
                {
                    int packingMaxQty = int.TryParse(txtPackingMaxQty.Text, out packingMaxQty) ? packingMaxQty : 0;

                    int fullBoxQty = int.TryParse(txtFullBox.Text, out fullBoxQty) ? fullBoxQty : 0;

                    txtTotalProduce.Text = (packingMaxQty * fullBoxQty + balanceLeftThisShift - balanceLeftLastShift).ToString();
                }
            }
        }

        private void CalculateTotalRejectAndPercentage()
        {
            if(true)
            {
                int availableQty = int.TryParse(txtAvailableQty.Text, out availableQty) ? availableQty : 0;
                int totalIn = int.TryParse(txtTotalProduce.Text, out totalIn) ? totalIn : 0;

                int totalReject = availableQty - totalIn;
                txtTotalReject.Text = totalReject.ToString();

                string rejectPercentageString = "NULL";

                if (availableQty <= 0)
                {
                    rejectPercentageString = "NULL";
                }
                else
                {
                    decimal rejectPercentage = (decimal)totalReject / availableQty * 100;

                    rejectPercentage = Decimal.Round(rejectPercentage, 2);

                    rejectPercentageString = rejectPercentage.ToString();
                }

                txtRejectPercentage.Text = rejectPercentageString;
            }
        }

        private void dgvMeterReading_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMeterReading;
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if(dgv.Columns[col].Name == header_Hourly)
            {
                int hourly = int.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out hourly) ? hourly : 0;

                if(hourly < 0)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
        }

        private void txtPackingQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider10.Clear();
            if(sheetLoaded)
            {
                dataSaved = false;
                CalculateTotalProduce();

                if (cmbPackingName.Text != string_MultiPackaging)
                {
                    SavePackagingToDT();
                }
            }
        }

        private void txtTotalShot_TextChanged(object sender, EventArgs e)
        {
            int totalShot = int.TryParse(txtTotalShot.Text, out totalShot) ? totalShot : 0;
            int cavity = int.TryParse(lblCavity.Text, out cavity) ? cavity : 0;

            txtAvailableQty.Text = (totalShot * cavity).ToString();

            
        }



        private void dgvDailyRecord_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvRecordHistory;
            dgv.SuspendLayout();

            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            int sheetID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_SheetID].Value);
            string itemName = ReportViewMode_PartName;


            contextMenuStrip1.Hide();

          

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void txtPlanID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtSheetID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbPackingCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (sheetLoaded)
            //{
            //    timer1.Stop();
            //    timer1.Start();
                
            //}

            //if (cmbPackingName.Text != string_MultiPackaging)
            //{
            //    //dt_MultiPackaging = null;
            //    SavePackagingToDT();
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(sheetLoaded)
            {
                timer1.Stop();
                //UpdatePackagingData();
            }
            
        }

        private void dgvRecordHistory_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //errorProvider3.Clear();
            //errorProvider4.Clear();

            //int rowIndex = dgvRecordHistory.CurrentCell.RowIndex;

            //if (rowIndex > 0)
            //{
            //    addingNewSheet = false;
            //    AddNewSheetUI(true);
            //}
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            cmbPackingName.SelectedIndex = -1;
            cmbPackingCode.SelectedIndex = -1;
        }

        private void dgvRecordHistory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvRecordHistory;
            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
            //handle the row selection on right click

            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && userPermission >= MainDashboard.ACTION_LVL_THREE)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();
                int rowIndex = dgv.CurrentCell.RowIndex;

                try
                {
                    my_menu.Items.Add("Remove from this list.").Name = "Remove from this list.";

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(dgvDailyRecord_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void AddMorePackaging_Click(object sender, EventArgs e)
        {
            int sheetID = int.TryParse(txtSheetID.Text, out sheetID) ? sheetID : -1;

            frmProPackaging frm;

            if (dt_MultiPackaging != null && dt_MultiPackaging.Rows.Count > 0)
            {
                if (cmbPackingName.Text != string_MultiPackaging)
                {
                    dt_MultiPackaging = null;
                    SavePackagingToDT();
                }
                

                frm = new frmProPackaging(dt_MultiPackaging)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
            }
            else
            {
                frm = new frmProPackaging(sheetID)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
            }

            frm.ShowDialog();

            if(frmProPackaging.dataSaved)
            {
                SavePackagingToDT(frmProPackaging.dt_Packaging);
 
            }
           
        }

        private void SavePackagingToDT()//for only one packaging data
        {
            if(sheetLoaded)
            {
                bool DataPassed = cmbPackingName.Text != string_MultiPackaging;

                DataPassed &= cmbPackingCode.Text != string_MultiPackaging;

                DataPassed &= txtPackingMaxQty.Text != string_Multi;

                if (DataPassed && DT_CARTON != null && DT_CARTON.Rows.Count > 0)
                {
                    int maxQty = int.TryParse(txtPackingMaxQty.Text, out maxQty) ? maxQty : 0;
                    int totalBox = int.TryParse(txtFullBox.Text, out totalBox) ? totalBox : 0;

                    string packagingCode = cmbPackingCode.Text;
                    string packagingName = cmbPackingName.Text;

                    foreach (DataRow row in DT_CARTON.Rows)
                    {
                        string itemcode = row[header_PackagingCode].ToString();

                        if(itemcode.Equals(packagingCode))
                        {
                            row[header_PackagingMax] = maxQty;
                            row[header_PackagingQty] = totalBox;
                        }
                    }
                }
            }
           
        }

        private void SavePackagingToDT(DataTable dt)
        {
            dt_MultiPackaging = null;
            dt_MultiPackaging = dt.Copy();

            int rowCount = dt_MultiPackaging.Rows.Count;

            if (rowCount >= 2)
            {
                LoadPackingToCMB(string_MultiPackaging);
                txtPackingMaxQty.Text = string_Multi;

                int totalStockIn = 0;
                int totalFullBox = 0;

                foreach (DataRow row in dt_MultiPackaging.Rows)
                {
                    string packingCode = row[frmProPackaging.header_PackagingCode].ToString();

                    int boxQty = Convert.ToInt32(row[frmProPackaging.header_PackagingQty].ToString());
                    int maxQty = Convert.ToInt32(row[frmProPackaging.header_PackagingMax].ToString());

                    string itemType = packingCode.Substring(0, 3);


                    if (itemType == "CTN" || itemType == "CTR")
                    {
                        totalFullBox += boxQty;

                        totalStockIn += boxQty * maxQty;
                    }

                 
                }


                txtFullBox.Text = totalFullBox.ToString();
                //txtTotalProduce.Text = totalStockIn.ToString();
                //CalculateTotalProduce();
            }
            else if (rowCount == 1)
            {
                loadPackingData();

                string fullboxQty = dt_MultiPackaging.Rows[0][frmProPackaging.header_PackagingQty].ToString();
                string pcsPerBox = dt_MultiPackaging.Rows[0][frmProPackaging.header_PackagingMax].ToString();
                string packingCode = dt_MultiPackaging.Rows[0][frmProPackaging.header_PackagingCode].ToString();

                cmbPackingName.Text = dt_MultiPackaging.Rows[0][frmProPackaging.header_PackagingName].ToString();
                tool.loadItemCodeDataToComboBox(cmbPackingCode, cmbPackingName.Text);
                cmbPackingCode.Text = packingCode;
                txtFullBox.Text = fullboxQty;
                txtPackingMaxQty.Text = pcsPerBox;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtAvailableQty_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalRejectAndPercentage();
        }

        private void txtProLotNo_TextChanged(object sender, EventArgs e)
        {
            if (sheetLoaded)
            {
                dataSaved = false;
            }

            //lotNo = int.TryParse(txtProLotNo.Text, out lotNo) ? lotNo : 0;
        }

       
        private void txtProLotNo_Leave(object sender, EventArgs e)
        {

            //JOB_NO = int.TryParse(txtJobNo.Text, out JOB_NO) ? JOB_NO : 0;

            //txtJobNo.Text = SetAlphabetToProLotNo(macID, JOB_NO);
            //txtProLotNo.Text = tool.GetAlphabet(macID-1) + " - " + lotNo;
        }

        private void txtProLotNo_Enter(object sender, EventArgs e)
        {
            //if(!string.IsNullOrEmpty(txtJobNo.Text))
            //txtJobNo.Text = JOB_NO.ToString();
        }

        private void dgvRecordHistory_Click(object sender, EventArgs e)
        {
            if(dgvRecordHistory.CurrentCell != null)
            {
                int row = dgvRecordHistory.CurrentCell.RowIndex;

                if (row >= 0 && dgvRecordHistory.SelectedRows.Count > 0)
                {
                    errorProvider3.Clear();
                    errorProvider4.Clear();

                    int rowIndex = dgvRecordHistory.CurrentCell.RowIndex;

                    if (rowIndex >= 0)
                    {
                        LoadExistingSheetData();
                    }
                }
            }
          
        }

        private void dtpProDate_ValueChanged_1(object sender, EventArgs e)
        {
            
        }

       

        private void txtBalanceOfLastShift_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtBalanceOfThisShift_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtFullBox_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtTotalStockIn_KeyUp(object sender, KeyEventArgs e)
        {
            
        }



        private void label14_Click_1(object sender, EventArgs e)
        {
            cmbParentList.SelectedIndex = -1;
        }

       

        private void dgvRecordHistory_Sorted(object sender, EventArgs e)
        {
            
        }

     

        private void lblIn_DoubleClick(object sender, EventArgs e)
        {
           

        }

        private void lblIn_Click(object sender, EventArgs e)
        {
            int balThisShift = int.TryParse(txtBalanceOfThisShift.Text, out balThisShift) ? balThisShift : 0;

            int directIn = int.TryParse(txtIn.Text, out directIn) ? directIn : 0;

            if (directIn <= 0)
            {
                txtBalanceOfThisShift.Text = "0";
                txtIn.Text = (balThisShift + directIn).ToString();
            }

           
        }

        private void lblOut_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void dgvRecordHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvRecordHistory.Columns[header_TotalStockIn].InheritedStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
        }

        private void updateDiff()
        {
            string totalProduced = txtTotalProducedRecord.Text;
            string totalStockIn = txtTotalStockInRecord.Text;

            int producedQty = int.TryParse(totalProduced, out producedQty) ? producedQty : 0;
            int stockInQty = int.TryParse(totalStockIn, out stockInQty) ? stockInQty : 0;
            txtDiff.Text = (producedQty - stockInQty).ToString();

            if (totalStockIn != totalProduced)
            {

                txtDiff.BackColor = Color.Pink;
            }
            else
            {
                if (!string.IsNullOrEmpty(totalProduced) && !string.IsNullOrEmpty(totalStockIn))
                {
                    txtDiff.BackColor = Color.LightGreen;
                }
                else
                {
                    txtDiff.BackColor = Color.White;
                }

            }
        }
        private void txtTotalStockInRecord_TextChanged(object sender, EventArgs e)
        {
            updateDiff();
        }

        private void UpdateCheckedStatus(bool Checked, int checkBy, DateTime date)
        {
            
            

        }
        private void cbChecked_CheckedChanged(object sender, EventArgs e)
        {
           
            

        }


        private void lblOut_Click(object sender, EventArgs e)
        {
            int balLastShift = int.TryParse(txtBalanceOfLastShift.Text, out balLastShift) ? balLastShift : 0;

            int directOut = int.TryParse(txtOut.Text, out directOut) ? directOut : 0;

            if(directOut <= 0)
            {
                txtBalanceOfLastShift.Text = "0";
                txtOut.Text = (balLastShift + directOut).ToString();
            }
            
        }


        private void txtOut_TextChanged(object sender, EventArgs e)
        {
            errorProvider6.Clear();
            //CalculateTotalRejectAndPercentage();
            CalculateTotalProduce();

            if(sheetLoaded)
            {
                balanceOutEdited = true;
            }
        }

        private void txtRawMatLotNo_TextChanged(object sender, EventArgs e)
        {
            if (sheetLoaded)
            {
                dataSaved = false;
            }
        }

        private void txtColorMatLotNo_TextChanged(object sender, EventArgs e)
        {
            if (sheetLoaded)
            {
                dataSaved = false;
            }
        }

        private void cmbParentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sheetLoaded)
            {
                dataSaved = false;
            }
        }

        private void txtTotalProducedRecord_TextChanged(object sender, EventArgs e)
        {
            updateDiff();
        }

        private void lblAddMoreParent_Click(object sender, EventArgs e)
        {
            int sheetID = int.TryParse(txtSheetID.Text, out sheetID) ? sheetID : -1;

            frmMultiParent frm = new frmMultiParent(lblPartCode.Text);

            frm.ShowDialog();
          
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            frmItemGroupList frm = new frmItemGroupList(lblPartCode.Text, lblPartName.Text)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();

            dt_JoinInfo = dalJoin.SelectAll();

            if (DT_CARTON == null || DT_CARTON.Rows.Count <= 0)
            {
                LoadCartonSetting();

            }


        }

        private void label20_Click(object sender, EventArgs e)
        {
            int sheetID = int.TryParse(txtSheetID.Text, out sheetID) ? sheetID : -1;

            if(DT_CARTON == null || DT_CARTON.Rows.Count <=0)
            {
                DT_CARTON = NewCartonTable();
            }

            frmCartonSetting frm = new frmCartonSetting(DT_CARTON)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();

            DT_CARTON = frmCartonSetting._DT_CARTON;

            string AlertMessage = "";

            if (DT_CARTON.Rows.Count > 0)
            {
                int totalStockIn = 0;
                int totalFullBox = 0;

                string name = DT_CARTON.Rows[0][header_PackagingName].ToString();
                string code = DT_CARTON.Rows[0][header_PartCode].ToString();

                int rowCount = DT_CARTON.Rows.Count;

                if (rowCount >= 2)
                {
                    LoadPackingToCMB(string_MultiPackaging);
                    txtPackingMaxQty.Text = "MULTI";

                    txtFullBox.Enabled = false;
                  

                    foreach (DataRow row in DT_CARTON.Rows)
                    {
                        int boxQty = int.TryParse(row[header_PackagingQty].ToString(), out boxQty) ? boxQty : 0;
                        int maxQty = int.TryParse(row[header_PackagingMax].ToString(), out maxQty) ? maxQty : 0;

                        totalFullBox += boxQty;

                        totalStockIn += boxQty * maxQty;
                    }


                    txtFullBox.Text = totalFullBox.ToString();

                }
                else if (rowCount == 1)
                {
                    loadPackingData();
                    txtFullBox.Enabled = true;

                    cmbPackingName.Text = name;
                    cmbPackingCode.Text = code;

                    foreach (DataRow row in DT_CARTON.Rows)
                    {
                        txtPackingMaxQty.Text = row[header_PackagingMax].ToString();
                        txtFullBox.Text = row[header_PackagingQty].ToString();
                    }


                }
            }
            else
            {
                txtPackingMaxQty.Text = "";
                cmbPackingName.SelectedIndex = -1;
                cmbPackingCode.SelectedIndex = -1;

                AlertMessage = "Carton data not found!\nPlease add a main carton to this item at:\n1. Item Group Edit";
            }

            if(!string.IsNullOrEmpty(AlertMessage))
            MessageBox.Show(AlertMessage, "Carton Alert",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtFullBox_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("click test");
        }

        private void txtActualTotalReject_TextChanged(object sender, EventArgs e)
        {
            CalculateActualTotalRejectAndPercentage();
        }

        private void CalculateActualTotalRejectAndPercentage()
        {
            int availableQty = int.TryParse(txtAvailableQty.Text, out availableQty) ? availableQty : 0;
            int totalActualReject = int.TryParse(txtActualTotalReject.Text, out totalActualReject) ? totalActualReject : 0;

            string rejectPercentageString = "NULL";

            if (availableQty > 0)
            {
                decimal rejectPercentage = (decimal)totalActualReject / availableQty * 100;

                rejectPercentage = Decimal.Round(rejectPercentage, 2);

                rejectPercentageString = rejectPercentage.ToString();
            }

            txtActualRejectPercentage.Text = rejectPercentageString;

            if (txtActualTotalReject.Text != txtTotalReject.Text)
            {
               // errorProvider11.SetError(lblActualRejectQty, "Reject Qty not Tally!");
            }
            else
            {
                errorProvider11.Clear();
            }

        }

        private void txtActualRejectPercentage_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvItemList_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void frmJobSheetViewMode_Shown(object sender, EventArgs e)
        {
            dgvRecordHistory.Focus();

        }
    }
}
