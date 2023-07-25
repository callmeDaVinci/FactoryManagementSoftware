using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmProductionRecord : Form
    {
        public frmProductionRecord()
        {
            InitializeComponent();
            AutoScroll = true;
            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_FOUR)
            {
                lblRemoveCompleted.Show();
                lblClearList.Show();
            }
            else
            {
                lblRemoveCompleted.Hide();
                lblClearList.Hide();
            }

            loadPackingData();
            CreateMeterReadingData();

            tool.DoubleBuffered(dgvItemList, true);
            tool.DoubleBuffered(dgvRecordHistory, true);
            tool.DoubleBuffered(dgvMeterReading, true);

            dt_ItemInfo = dalItem.Select();
            dt_JoinInfo = dalJoin.SelectAll();
            dt_Mac = dalMac.Select();

        }

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

        DataTable dt_ProductionRecord ;
        DataTable dt_ItemInfo;
        DataTable dt_JoinInfo;
        DataTable dt_MultiPackaging;
        DataTable dt_Mac;
       // DataTable dt_Trf;

        int userPermission = -1;
        readonly private string string_NewSheet = "NEW";
        readonly private string string_Multi = "MULTI";
        readonly private string string_MultiPackaging = "MULTI PACKAGING";

        readonly private string header_Time = "TIME";
        readonly private string header_Operator = "OPERATOR";
        readonly private string header_MeterReading = "METER READING";
        readonly private string header_Hourly = "HOURLY";

        readonly private string header_ProLotNo = "PRO LOT NO";
        readonly private string header_Machine = "MAC.";
        readonly private string header_Factory = "FAC.";
        readonly private string header_PlanID = "PLAN ID";
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
        readonly static public string header_PackagingQty = "TOTAL QTY";
        readonly static public string header_PackagingMax = "QTY/BOX";

        readonly private string string_CheckBy= "CHECK BY: ";
        readonly private string string_CheckDate = "DATE: ";

        readonly string header_Cat = "Cat";
        readonly string header_ProDate = "DATE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_From = "FROM";
        readonly string header_To = "TO";
        readonly string header_Qty = "QTY";

        private int macID = 0;
        private int lotNo = 0;

        private bool loaded = false;
        private bool addingNewSheet = false;
        private bool sheetLoaded = false;
        private bool dailyRecordLoaded = false;
        private bool stopCheckedChange = false;

        private DataTable NewPackagingTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_PackagingCode, typeof(string));
            dt.Columns.Add(header_PackagingName, typeof(string));
            dt.Columns.Add(header_PackagingMax, typeof(int));
            dt.Columns.Add(header_PackagingQty, typeof(int));

            return dt;
        }

        private DataTable NewItemListTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Machine, typeof(int));
            dt.Columns.Add(header_Factory, typeof(string));
            dt.Columns.Add(header_PlanID, typeof(int));
            dt.Columns.Add(header_PartName, typeof(string));
            dt.Columns.Add(header_PartCode, typeof(string));
            dt.Columns.Add(header_Status, typeof(string));

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
            dt.Columns.Add(header_PlanID, typeof(string));
            dt.Columns.Add(header_Shift, typeof(string));

            return dt;
        }

        private DataTable NewSheetRecordTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_SheetID, typeof(int));
            dt.Columns.Add(header_ProductionDate, typeof(DateTime));
            dt.Columns.Add(header_ProLotNo, typeof(string));
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

            return dt;
        }

        private void dgvMeterStyleEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[header_Time].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_Operator].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_MeterReading].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Hourly].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[header_Operator].DefaultCellStyle.BackColor = SystemColors.Info;
            dgv.Columns[header_MeterReading].DefaultCellStyle.BackColor = SystemColors.Info;
        }

        private void dgvItemListStyleEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.Columns[header_Machine].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Factory].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_PlanID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Status].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_PartName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_PartCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        }

        private void dgvSheetRecordStyleEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.Columns[header_SheetID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ProductionDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Shift].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ProducedQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_UpdatedDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_UpdatedBy].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_TotalProduced].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_StockIn].DefaultCellStyle.BackColor = SystemColors.Info;
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

            if(CheckIfDateOrShiftDuplicate())
            {
                errorProvider1.SetError(lblDate, "Please select a different date");
                errorProvider2.SetError(lblShift, "Please select a different shift");
                
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

            if (string.IsNullOrEmpty(txtTotalStockIn.Text))
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

        private void AddNewSheetUI(bool newSheet)
        {
            dgvMeterReading.SuspendLayout();
            ClearData();

            if (newSheet)
            {
                if (dgvItemList.SelectedRows.Count > 0)
                {
                    tlpSheet.RowStyles[0] = new RowStyle(SizeType.Percent, 0);
                    tlpSheet.RowStyles[1] = new RowStyle(SizeType.Percent, 100);

                    btnNewSheet.Visible = false;
                    dgvMeterReading.ResumeLayout();
                }
                    
            }
            else
            {
                
                sheetLoaded = false;
                addingNewSheet = false;
                dgvRecordHistory.ClearSelection();
                tlpSheet.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                tlpSheet.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                btnNewSheet.Visible = true;
                dgvMeterReading.ResumeLayout();
            }
          
        }

        private void LoadItemListData()
        {
            dt_ProductionRecord = dalProRecord.Select();
            DataGridView dgv = dgvItemList;
            DataTable dt = NewItemListTable();

            DataTable dt_Plan = dalPlan.Select();

            foreach (DataRow row in dt_Plan.Rows)
            {
                bool match = true;

                #region Recording Filtering

                if(row[dalPlan.recording] != DBNull.Value)
                {
                    bool recording = Convert.ToBoolean(row[dalPlan.recording]);

                    if(!recording)
                    {
                        match = false;
                    }
                }
                else
                {
                    match = false;
                }

                #endregion

                if (match)
                {
                    DataRow dt_Row = dt.NewRow();

                    dt_Row[header_Machine] = row[dalPlan.machineID];
                    dt_Row[header_Factory] = row[dalMac.MacLocationName];
                    dt_Row[header_PlanID] = row[dalPlan.jobNo];
                    dt_Row[header_PartName] = row[dalItem.ItemName];
                    dt_Row[header_PartCode] = row[dalItem.ItemCode];
                    dt_Row[header_Status] = row[dalPlan.planStatus];

                    dt.Rows.Add(dt_Row);
                }

            }

            dgv.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                if (userPermission >= MainDashboard.ACTION_LVL_FOUR)
                {
                    lblRemoveCompleted.Show();
                    lblClearList.Show();
                }

                dgv.DataSource = dt;
                dgvItemListStyleEdit(dgv);
                dgv.ClearSelection();
            }
            else
            {
                lblRemoveCompleted.Hide();
                lblClearList.Hide();
            }

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

                string itemCode = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PartCode].Value.ToString();
                string planID = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PlanID].Value.ToString();
                
                DataTable transferData = dalTrf.codeLikeSearch(itemCode);

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
                        //search transfer table
                        
                        
                        //change color
                        foreach(DataGridViewRow dgvRow in dgvRecordHistory.Rows)
                        {
                            if(previousDate == Convert.ToDateTime(dgvRow.Cells[header_ProductionDate].Value.ToString()))
                            {

                                //check if parent
                                string sheetID = dgvRow.Cells[header_SheetID].Value.ToString();

                                foreach(DataRow rowPR in dt_ProductionRecord.Rows)
                                {
                                    if(sheetID == rowPR[dalProRecord.SheetID].ToString())
                                    {
                                        string parentCode = rowPR[dalProRecord.ParentCode].ToString();

                                        if(!string.IsNullOrEmpty(parentCode))
                                        {
                                            itemCode = parentCode;
                                            transferData = dalTrf.codeLikeSearch(itemCode);
                                        }
                                        break;
                                    }
                                }

                                int trfQty = tool.TotalProductionStockInInOneDay(transferData, itemCode, previousDate, planID, dgvRow.Cells[header_Shift].Value.ToString());
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

                //change last row color
                foreach (DataGridViewRow dgvRow in dgvRecordHistory.Rows)
                {
                    if (previousDate == Convert.ToDateTime(dgvRow.Cells[header_ProductionDate].Value.ToString()))
                    {
                        int trfQty_ = tool.TotalProductionStockInInOneDay(transferData, itemCode, previousDate, planID, dgvRow.Cells[header_Shift].Value.ToString());

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

               
                txtTotalStockInRecord.Text = totalStockIn.ToString();
            }

        }

        private bool CheckIfDateOrShiftDuplicate()
        {
            ClearAllError();
            bool ifDuplicateData = false;
            if (addingNewSheet)
            {
                
                DataTable dt = (DataTable)dgvRecordHistory.DataSource;

                DateTime selectedDate = dtpProDate.Value;
                string shift = null;

                if (cbMorning.Checked)
                {
                    shift = text.Shift_Morning;
                }
                else if (cbNight.Checked)
                {
                    shift = text.Shift_Night;
                }

                if (shift != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DateTime recordedDate = Convert.ToDateTime(row[header_ProductionDate].ToString());
                        string recordedShift = row[header_Shift].ToString();
                        string recordedLotNo = row[header_ProLotNo].ToString();



                        ifDuplicateData = recordedDate == selectedDate;
                        ifDuplicateData &= recordedShift == shift;

                        if (ifDuplicateData)
                        {
                            MessageBox.Show("There is a existing daily job sheet under your selected date and shift!");
                        }
                    }
                }
            }
           

            return ifDuplicateData;
        }

        private int GetLastestLotNoFromDGV()
        {
            int LastestLotNo = -1;

            DataTable dt = (DataTable)dgvRecordHistory.DataSource;

            foreach (DataRow row in dt.Rows)
            {
                string recordedLotNo = row[header_ProLotNo].ToString();

                int numberOnly = GetINTOnlyFromLotNo(recordedLotNo);

                if (numberOnly > LastestLotNo)
                {
                    LastestLotNo = numberOnly;
                }
            }

            return LastestLotNo;
        }

        private void LoadDailyRecord()
        {
            dailyRecordLoaded = false;

            int itemRow = -1;
            if (dgvItemList.CurrentCell != null)
            {
                itemRow = dgvItemList.CurrentCell.RowIndex;
            }
            
            if(itemRow >= 0)
            {
                string planID = dgvItemList.Rows[itemRow].Cells[header_PlanID].Value.ToString();
                string planStatus = dgvItemList.Rows[itemRow].Cells[header_Status].Value.ToString();
                macID = int.TryParse(dgvItemList.Rows[itemRow].Cells[header_Machine].Value.ToString(), out macID) ? macID : 0;

                DataTable dt = NewSheetRecordTable();
                DataRow dt_Row;
                
                int totalProducedQty = 0;
                foreach(DataRow row in dt_ProductionRecord.Rows)
                {
                    bool active = Convert.ToBoolean(row[dalProRecord.Active].ToString());
                    if(planID == row[dalProRecord.JobNo].ToString() && active)
                    {
                        dt_Row = dt.NewRow();

                        int produced = Convert.ToInt32(row[dalProRecord.TotalProduced].ToString());
                        int proLotNo = int.TryParse(row[dalProRecord.ProLotNo].ToString(), out proLotNo) ? proLotNo : -1;
                        totalProducedQty += produced;
                        
                        if(proLotNo != -1)
                        {
                            dt_Row[header_ProLotNo] = SetAlphabetToProLotNo(macID, proLotNo);
                        }

                        dt_Row[header_SheetID] = row[dalProRecord.SheetID].ToString();
                        dt_Row[header_ProductionDate] = row[dalProRecord.ProDate].ToString();
                        dt_Row[header_Shift] = row[dalProRecord.Shift].ToString();
                        dt_Row[header_ProducedQty] = produced;
                        dt_Row[header_UpdatedDate] = row[dalProRecord.UpdatedDate].ToString();
                        dt_Row[header_UpdatedBy] = dalUser.getUsername(Convert.ToInt32(row[dalProRecord.UpdatedBy].ToString()));

                        dt_Row[header_TotalProduced] = totalProducedQty;

                        if(dt.Rows.Count > 0)
                        dt.Rows[dt.Rows.Count - 1][header_TotalProduced] = DBNull.Value;

                        dt.Rows.Add(dt_Row);
                        
                        

                    }
                }

                txtTotalProducedRecord.Text = totalProducedQty.ToString();
                //update total produced
                uPlan.plan_id = Convert.ToInt32(planID);
                uPlan.plan_produced = totalProducedQty;
                if(!dalPlan.TotalProducedUpdate(uPlan))
                {
                    MessageBox.Show("Failed to update total produced qty!");
                }

                dgvRecordHistory.DataSource = null;

                dgvRecordHistory.DataSource = dt;
                dgvSheetRecordStyleEdit(dgvRecordHistory);
                dgvRecordHistory.ClearSelection();

                //load checked status
                LoadCheckedStatus(planID, planStatus);
            }

            CheckIfStockIn();
        }

        private void LoadCheckedStatus(string planID, string planStatus)
        {
            if(planStatus == text.planning_status_completed)
            {
                
                DataTable dt = dalPlan.idSearch(planID);
                foreach (DataRow row in dt.Rows)
                {
                    if (planID == row[dalPlan.jobNo].ToString())
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
            DataTable dtJoin = dalJoin.loadParentList(itemCode);

            if (dtJoin.Rows.Count > 0)
            {
                cmbParentList.Enabled = true;
                DataTable dt = new DataTable();
                dt.Columns.Add("Parent");

                foreach(DataRow row in dtJoin.Rows)
                {
                    bool duplicate = false;
                    string parentCode = row[dalJoin.JoinParent].ToString();

                    if (dt.Rows.Count > 0)
                    {
                        foreach(DataRow row2 in dt.Rows)
                        {
                            string list_ParentCode = row2["Parent"].ToString();

                            if(list_ParentCode == parentCode)
                            {
                                duplicate = true;
                                break;
                            }
                        }
                    }

                    if(!duplicate)
                    {
                        dt.Rows.Add(parentCode);
                    }
                        
                }

                if (dt.Rows.Count > 0)
                {
                    dt.DefaultView.Sort = "Parent ASC";

                    cmbParentList.DataSource = dt;
                    cmbParentList.DisplayMember = "Parent";
                    cmbParentList.SelectedIndex = -1;
                }
            }
            else
            {
                cmbParentList.Enabled = false;
            }
        }

        private void LoadNewSheetData()
        {
            //if new
            //if row selected

            sheetLoaded = false;

            int selectedItem = dgvItemList.CurrentCell.RowIndex;

            if(selectedItem <= -1)
            {
                MessageBox.Show("Please select a item before adding new sheet.");
            }
            else
            {
                string itemName = dgvItemList.Rows[selectedItem].Cells[header_PartName].Value.ToString();
                string itemCode = dgvItemList.Rows[selectedItem].Cells[header_PartCode].Value.ToString();
                string planID = dgvItemList.Rows[selectedItem].Cells[header_PlanID].Value.ToString();

                LoadParentList(itemCode);

                int macID = Convert.ToInt32(dgvItemList.Rows[selectedItem].Cells[header_Machine].Value.ToString());


                lotNo = tool.GetNextLotNo(dt_Mac, macID);
                int lotNoFromDGV = GetLastestLotNoFromDGV();

                if(lotNoFromDGV != -1)
                {
                    lotNo = lotNoFromDGV;
                }
              
                //if(lotNoFromDGV != -1 && lotNo - lotNoFromDGV > 1)
                //{
                //    lotNo = lotNoFromDGV + 1;
                //}

                txtProLotNo.Text = SetAlphabetToProLotNo(macID, lotNo);
                lblCustomer.Text = "";
                lblPartName.Text = itemName;
                lblPartCode.Text = itemCode;

                foreach (DataRow row in dt_ItemInfo.Rows)
                {
                    if(row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        float partWeight = float.TryParse(row[dalItem.ItemProPWShot].ToString(), out float i) ? Convert.ToSingle(row[dalItem.ItemProPWShot].ToString()) : -1;
                        float runnerWeight = float.TryParse(row[dalItem.ItemProRWShot].ToString(), out float k) ? Convert.ToSingle(row[dalItem.ItemProRWShot].ToString()) : -1;

                        txtPlanID.Text = planID;
                        txtSheetID.Text = string_NewSheet;
                        lblPW.Text = partWeight.ToString("0.##");
                        lblRW.Text = runnerWeight.ToString("0.##");
                        lblCavity.Text = row[dalItem.ItemCavity] == DBNull.Value ? "" : row[dalItem.ItemCavity].ToString();
                        lblRawMat.Text = row[dalItem.ItemMaterial] == DBNull.Value ? "" : row[dalItem.ItemMaterial].ToString();
                        lblColorMat.Text = row[dalItem.ItemMBatch] == DBNull.Value ? "" : row[dalItem.ItemMBatch].ToString();

                        float colorRate = row[dalItem.ItemMBRate] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemMBRate]);
                        lblColorUsage.Text = (colorRate * 100).ToString("0.##");
                    }
                       
                }

                //get packaging data
                foreach (DataRow row in dt_JoinInfo.Rows)
                {
                    string parentCode = row[dalJoin.JoinParent].ToString();

                    if(parentCode == itemCode)
                    {
                        string childCode = row[dalJoin.JoinChild].ToString();

                        foreach(DataRow rowItem in dt_ItemInfo.Rows)
                        {
                            if (rowItem[dalItem.ItemCode].ToString().Equals(childCode))
                            {
                                string cat = rowItem[dalItem.ItemCat].ToString();

                                if(cat.Equals(text.Cat_Carton) || cat.Equals(text.Cat_Packaging))
                                {
                                    string packagingName = rowItem[dalItem.ItemName].ToString();
                                    string packagingCode= rowItem[dalItem.ItemCode].ToString();
                                    string packagingQty = row[dalJoin.JoinMax].ToString();

                                    txtPackingMaxQty.Text = packagingQty;
                                    cmbPackingName.Text = packagingName;
                                    cmbPackingCode.Text = packagingCode;

                                    break;
                                }
                            }
                        }
                    }

                }


                //get balance of last shift
                txtBalanceOfLastShift.Text = GetLatestBalanceLeft(dalProRecord.Select(), planID).ToString();
            }

            sheetLoaded = true;

        }

        private void LoadExistingSheetData()
        {
            sheetLoaded = false;
            
            int selectedItem = dgvItemList.CurrentCell.RowIndex;
            int selectedDailyRecord = dgvRecordHistory.CurrentCell.RowIndex;
            if (selectedItem <= -1)
            {
                MessageBox.Show("Please select a item before adding new sheet.");
            }
            else
            {
                string itemName = dgvItemList.Rows[selectedItem].Cells[header_PartName].Value.ToString();
                string itemCode = dgvItemList.Rows[selectedItem].Cells[header_PartCode].Value.ToString();
                string planID = dgvItemList.Rows[selectedItem].Cells[header_PlanID].Value.ToString();
                string sheetID = dgvRecordHistory.Rows[selectedDailyRecord].Cells[header_SheetID].Value.ToString();
                string shift = dgvRecordHistory.Rows[selectedDailyRecord].Cells[header_Shift].Value.ToString();

                LoadParentList(itemCode);
                uProRecord.sheet_id = Convert.ToInt32(sheetID);
                DataTable dt_ProductionRecord = dalProRecord.ProductionRecordSelect(uProRecord);

                lblPartName.Text = itemName;
                lblPartCode.Text = itemCode;
                txtPlanID.Text = planID;
                txtSheetID.Text = sheetID;

                if (shift == "MORNING")
                {
                    cbMorning.Checked = true;
                }
                else if(shift == "NIGHT")
                {
                    cbNight.Checked = true;
                }

                foreach (DataRow row in dt_ProductionRecord.Rows)
                {
                    if (row[dalProRecord.SheetID].ToString().Equals(sheetID))
                    {
                        float partWeight = float.TryParse(row[dalPlan.planPW].ToString(), out float i) ? Convert.ToSingle(row[dalPlan.planPW].ToString()) : -1;
                        float runnerWeight = float.TryParse(row[dalPlan.planRW].ToString(), out float k) ? Convert.ToSingle(row[dalPlan.planRW].ToString()) : -1;

                        lblPW.Text = partWeight.ToString("0.##");
                        lblRW.Text = runnerWeight.ToString("0.##");

                        lblCavity.Text = row[dalPlan.planCavity] == DBNull.Value ? "" : row[dalPlan.planCavity].ToString();
                        lblRawMat.Text = row[dalPlan.materialCode] == DBNull.Value ? "" : row[dalPlan.materialCode].ToString();
                        lblColorMat.Text = row[dalPlan.colorMaterialCode] == DBNull.Value ? "" : row[dalPlan.colorMaterialCode].ToString();

                        
                        float colorRate = row[dalPlan.colorMaterialUsage] == DBNull.Value ? 0 : Convert.ToSingle(row[dalPlan.colorMaterialUsage]);
                        lblColorUsage.Text = colorRate.ToString("0.##");

                        dtpProDate.Value = Convert.ToDateTime(row[dalProRecord.ProDate].ToString());

                        int _ProLotNo = int.TryParse(row[dalProRecord.ProLotNo].ToString(), out _ProLotNo) ? _ProLotNo : -1;

                        if(_ProLotNo != -1)
                        {
                            txtProLotNo.Text = SetAlphabetToProLotNo(macID, _ProLotNo);
                        }
                        else
                        {
                            txtProLotNo.Text = row[dalProRecord.ProLotNo].ToString();
                        }
                        
                        txtRawMatLotNo.Text = row[dalProRecord.RawMatLotNo].ToString();
                        txtColorMatLotNo.Text = row[dalProRecord.ColorMatLotNo].ToString();
                        txtMeterStart.Text = row[dalProRecord.MeterStart].ToString();
                        txtBalanceOfLastShift.Text = row[dalProRecord.LastShiftBalance].ToString();
                        txtBalanceOfThisShift.Text = row[dalProRecord.CurrentShiftBalance].ToString();
                        txtIn.Text = row[dalProRecord.directIn].ToString();
                        txtOut.Text = row[dalProRecord.directOut].ToString();
                        txtFullBox.Text = row[dalProRecord.FullBox].ToString();
                        txtTotalStockIn.Text = row[dalProRecord.TotalProduced].ToString();
                        cmbParentList.Text = row[dalProRecord.ParentCode].ToString();
                        txtNote.Text = row[dalProRecord.Note].ToString();
                        //cmbPackingName.Text = tool.getItemName(row[dalProRecord.PackagingCode].ToString());
                        //cmbPackingCode.Text = row[dalProRecord.PackagingCode].ToString();
                        //txtPackingMaxQty.Text = row[dalProRecord.PackagingQty].ToString();

                        //txtTotalReject.Text = row[dalProRecord.ProLotNo].ToString();
                        // txtRejectPercentage.Text = row[dalProRecord.ProLotNo].ToString();
                    }

                }

                LoadMeterReadingData(Convert.ToInt32(sheetID));
                CalculateHourlyShot();

                //get packaging data
                LoadPackagingList(Convert.ToInt32(sheetID));

                //foreach (DataRow row in dt_JoinInfo.Rows)
                //{
                //    string parentCode = row[dalJoin.JoinParent].ToString();

                //    if (parentCode == itemCode)
                //    {
                //        string childCode = row[dalJoin.JoinChild].ToString();

                //        foreach (DataRow rowItem in dt_ItemInfo.Rows)
                //        {
                //            if (rowItem[dalItem.ItemCode].ToString().Equals(childCode))
                //            {
                //                string cat = rowItem[dalItem.ItemCat].ToString();

                //                if (cat.Equals(text.Cat_Carton) || cat.Equals(text.Cat_Packaging))
                //                {
                //                    string packagingName = rowItem[dalItem.ItemName].ToString();
                //                    string packagingCode = rowItem[dalItem.ItemCode].ToString();
                //                    string packagingQty = row[dalJoin.JoinMax].ToString();

                //                    txtPackingMaxQty.Text = packagingQty;
                //                    cmbPackingName.Text = packagingName;
                //                    cmbPackingCode.Text = packagingCode;

                //                    break;
                //                }
                //            }
                //        }
                //    }

                //}

            }

            sheetLoaded = true;

        }

        private void LoadMeterReadingData(int sheedID)
        {
            CreateMeterReadingData();
            uProRecord.sheet_id = sheedID;
            DataTable dt = dalProRecord.MeterRecordSelect(uProRecord);
            DataTable dt_Meter = (DataTable)dgvMeterReading.DataSource;

            foreach(DataRow row in dt.Rows)
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
                errorProvider2.SetError(lblShift, "Please select shift");
            }
            
        }

        private void UpdatePackagingData()
        {
            if(sheetLoaded)
            {
                int qty = int.TryParse(txtPackingMaxQty.Text, out qty) ? qty : -1;
                string packagingName = cmbPackingName.Text;
                string packagingCode = cmbPackingCode.Text;

                if (qty != -1 && !string.IsNullOrEmpty(packagingName) && !string.IsNullOrEmpty(packagingCode) && packagingCode != string_MultiPackaging)
                {
                    string itemCode = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PartCode].Value.ToString();
                    string itemName = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PartName].Value.ToString();

                    string messageToUser = null;

                    messageToUser = "ITEM            : " + itemName + " (" + itemCode + ")\n";
                    messageToUser += "QTY              : " + qty + " PCS\n";
                    messageToUser += "PACKAGING : " + packagingName + " (" + packagingCode + ")\n\n";
                    messageToUser += "Do you want to update these packaging data to database?";

                    DialogResult dialogResult = MessageBox.Show(messageToUser, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        uJoin.join_parent_code = itemCode;

                        uJoin.join_child_code = packagingCode;

                        uJoin.join_max = qty;
                        uJoin.join_min = qty;
                        uJoin.join_qty = 1;

                        DataTable dt_existCheck = dalJoin.existCheck(uJoin.join_parent_code, uJoin.join_child_code);

                        bool success = false;

                        if (dt_existCheck.Rows.Count > 0)
                        {
                            uJoin.join_updated_date = DateTime.Now;
                            uJoin.join_updated_by = MainDashboard.USER_ID;
                            //update data
                            success = dalJoin.UpdateWithMaxMin(uJoin);
                            //If the data is successfully inserted then the value of success will be true else false
                            if (success)
                            {
                                //Data Successfully Inserted
                                MessageBox.Show("Join updated.");
                                dt_JoinInfo = dalJoin.SelectAll();


                            }
                            else
                            {
                                //Failed to insert data
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
                            if (success)
                            {
                                //Data Successfully Inserted
                                MessageBox.Show("Join successfully created");
                                dt_JoinInfo = dalJoin.SelectAll();
                                // this.Close();
                            }
                            else
                            {
                                //Failed to insert data
                                MessageBox.Show("Failed to add new join");
                            }
                        }
                    }
                }
            }
           
        }

        private void LoadPackagingList(int sheetID)
        {
            DataTable dt = NewPackagingTable();

            DataTable dt_PackagingFromDB = dalProRecord.PackagingRecordSelect(sheetID);

            foreach (DataRow row in dt_PackagingFromDB.Rows)
            {
                DataRow dt_Row = dt.NewRow();

                string packagingCode = row[dalProRecord.PackagingCode].ToString();

                int QtyPerBox = Convert.ToInt16(row[dalProRecord.PackagingMax].ToString());
                int TotalBox = Convert.ToInt16(row[dalProRecord.PackagingQty].ToString());

                dt_Row[header_PackagingCode] = packagingCode;
                dt_Row[header_PackagingName] = tool.getItemName(packagingCode);
                dt_Row[header_PackagingMax] = QtyPerBox;
                dt_Row[header_PackagingQty] = TotalBox;

                dt.Rows.Add(dt_Row);
            }

            dt_MultiPackaging = dt.Copy();

            if (dt_MultiPackaging.Rows.Count > 0)
            {
                string name = dt_MultiPackaging.Rows[0][header_PackagingName].ToString();
                string code = dt_MultiPackaging.Rows[0][header_PartCode].ToString();

                int rowCount = dt_MultiPackaging.Rows.Count;

                if (rowCount >= 2)
                {
                    lblClear.Visible = false;
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
                    lblClear.Visible = true;
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

        private bool SaveSuccess()
        {
            bool success = false;
            if(Validation())
            {
                frmLoading.ShowLoadingScreen();
                //TO-DO:
                //save sheet data
                string planID = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PlanID].Value.ToString();
                int sheetID = int.TryParse(txtSheetID.Text, out sheetID)? sheetID : -1;

                string Shift = text.Shift_Night;
                string PackagingCode = cmbPackingCode.Text;
                int PackagingQty = int.TryParse(txtPackingMaxQty.Text, out PackagingQty)? PackagingQty : 0;
                int meterStart = Convert.ToInt32(txtMeterStart.Text);
                int meterEnd = meterStart;
                int lastShiftBalance = Convert.ToInt32(txtBalanceOfLastShift.Text);
                int thisShiftBalance = Convert.ToInt32(txtBalanceOfThisShift.Text);

                int directIn = int.TryParse(txtIn.Text, out directIn) ? directIn : 0;
                int directOut = int.TryParse(txtOut.Text, out directOut) ? directOut : 0;

                int fullBox = Convert.ToInt32(txtFullBox.Text);
                int TotalStockIn = Convert.ToInt32(txtTotalStockIn.Text);
                int TotalReject = Convert.ToInt32(txtTotalReject.Text);
                double RejectPercentage = Convert.ToDouble(txtRejectPercentage.Text);
                string ParentStockIn = cmbParentList.Text;
                string note = txtNote.Text ;
                string itemCode = lblPartCode.Text;
                lotNo = GetINTOnlyFromLotNo(txtProLotNo.Text);

                DateTime updateTime = DateTime.Now;

                if (cbMorning.Checked)
                {
                    Shift = text.Shift_Morning;
                }

                uProRecord.active = true;
                uProRecord.plan_id = Convert.ToInt16(planID);
                uProRecord.production_date = dtpProDate.Value;
                uProRecord.shift = Shift;
                uProRecord.packaging_code = PackagingCode;
                uProRecord.packaging_qty = PackagingQty;
                uProRecord.production_lot_no = lotNo.ToString();
                uProRecord.raw_mat_lot_no = txtRawMatLotNo.Text;
                uProRecord.color_mat_lot_no = txtColorMatLotNo.Text;
                uProRecord.meter_start = meterStart;
                uProRecord.parent_code = ParentStockIn;
                uProRecord.note = note;
                uProRecord.directIn = directIn;
                uProRecord.directOut = directOut;

                foreach (DataGridViewRow row in dgvMeterReading.Rows)
                {
                    if(row.Cells[header_MeterReading].Value != DBNull.Value)
                    {
                        int i = 0;
                        if (int.TryParse(row.Cells[header_MeterReading].Value.ToString(), out i))
                        {
                            meterEnd = i;
                        }
                    }
                    
                }

                int userID = MainDashboard.USER_ID;
                uProRecord.meter_end = meterEnd;

                uProRecord.last_shift_balance = lastShiftBalance;
                uProRecord.current_shift_balance = thisShiftBalance;
                uProRecord.full_box = fullBox;
                uProRecord.total_produced = TotalStockIn;
                uProRecord.total_reject = TotalReject;
                uProRecord.reject_percentage = RejectPercentage;
                uProRecord.updated_date = updateTime;
                uProRecord.updated_by = userID;
                uProRecord.sheet_id = sheetID;

                success = false;
                if(addingNewSheet)
                {
                    success = dalProRecord.InsertProductionRecord(uProRecord);
                }
                else
                {
                    //update data
                    success = dalProRecord.ProductionRecordUpdate(uProRecord);
                }

                if(success)
                {
                    if(addingNewSheet)
                    {
                        //new sheet: sheet id + item
                        tool.historyRecord(text.AddDailyJobSheet, "Plan ID: "+ planID + "(" + itemCode + ")", updateTime, userID);
                    }
                    else
                    {
                        //edit/modify sheet
                        tool.historyRecord(text.EditDailyJobSheet, "Sheet ID: " + sheetID + "(" + itemCode + ")", updateTime, userID);
                    }

                    dgvItemList.Focus();
                    LoadDailyRecord();

                    if(uProRecord.sheet_id == -1)
                    {
                        uProRecord.sheet_id = dalProRecord.GetLastInsertedSheetID();
                    }

                    //remove old meter data
                    //remove data under same sheet id
                    dalProRecord.DeleteMeterData(uProRecord);

                    //insert new meter data
                    DataTable dt_Meter = (DataTable)dgvMeterReading.DataSource;

                    foreach(DataRow row in dt_Meter.Rows)
                    {
                        DateTime time = Convert.ToDateTime(row[header_Time].ToString());
                        string proOperator = row[header_Operator].ToString();
                        int meterReading = int.TryParse(row[header_MeterReading].ToString(), out meterReading) ? meterReading : -1;

                        uProRecord.time = time;
                        uProRecord.production_operator = proOperator;
                        uProRecord.meter_reading = meterReading;

                        if(meterReading != -1)
                        {
                            if (!dalProRecord.InsertSheetMeter(uProRecord))
                            {
                                break;
                            }
                        }
                        
                    }


                    //remove packaging data from db and insert new data
                    dalProRecord.DeletePackagingData(uProRecord);

                    if (dt_MultiPackaging != null)
                    {
                        //save packaging data
                        foreach(DataRow row in dt_MultiPackaging.Rows)
                        {
                            uProRecord.packaging_code = row[header_PackagingCode].ToString();
                            uProRecord.packaging_max = Convert.ToInt32(row[header_PackagingMax].ToString());
                            uProRecord.packaging_qty = Convert.ToInt32(row[header_PackagingQty].ToString());

                            if(!dalProRecord.InsertProductionPackaging(uProRecord))
                            {
                                MessageBox.Show("Failed to save packaging data!");
                            }
                        }
                    }

                    //update lot no
                    if(lotNo > 0)
                    {
                        uMac.mac_id = macID;
                        uMac.mac_lot_no = lotNo;
                        uMac.mac_updated_date = updateTime;
                        uMac.mac_updated_by = MainDashboard.USER_ID;

                        if (!dalMac.UpdateLotNo(uMac))
                        {
                            MessageBox.Show("Failed to update machine lot no!");
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
            //dt_Trf = dalTrf.Select();
            dt_ProductionRecord = dalProRecord.Select();
            return success;
        }

        private void SaveAndStock()
        {
            if (Validation())
            { 
                if(SaveSuccess())//SaveSuccess()
                {
                    DataTable dt = NewStockInTable();
                    DataRow dt_Row ;

                    //get totalStockIn qty from box qty and max pcs per box
                    //check if one or multi packaging box
                    int totalStockIn = 0;
                    int directStockIn = int.TryParse(txtIn.Text, out directStockIn) ? directStockIn : 0;

                    int selectedItem = dgvItemList.CurrentCell.RowIndex;

                    string planStatus = dgvItemList.Rows[selectedItem].Cells[header_Status].Value.ToString();
                    int balance = int.TryParse(txtBalanceOfThisShift.Text, out balance) ? balance : 0;

                    if (dt_MultiPackaging != null && dt_MultiPackaging.Rows.Count >= 2)
                    { 
                        int totalFullBox = 0;

                        foreach (DataRow row in dt_MultiPackaging.Rows)
                        {
                            
                            string packingCode = row[frmProPackaging.header_PackagingCode].ToString();

                            string itemType = packingCode.Substring(0, 3);

                            int boxQty = Convert.ToInt32(row[frmProPackaging.header_PackagingQty].ToString());
                            int maxQty = Convert.ToInt32(row[frmProPackaging.header_PackagingMax].ToString());

                            if(itemType == "CTN" || itemType == "CTR")
                            {
                                totalFullBox += boxQty;

                                totalStockIn += boxQty * maxQty;
                            }

                            if (!string.IsNullOrEmpty(packingCode) && itemType == "CTN")
                            {
                                dt_Row = dt.NewRow();
                                dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                                dt_Row[header_ItemCode] = packingCode;
                                dt_Row[header_Cat] = tool.getItemCat(packingCode);

                                dt_Row[header_From] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();
                                dt_Row[header_To] = text.Production;

                                dt_Row[header_Qty] = boxQty;
                                dt_Row[header_PlanID] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PlanID].Value.ToString();

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
                        
                        int packingMaxQty = int.TryParse(txtPackingMaxQty.Text, out packingMaxQty) ? packingMaxQty : 0;

                        int fullBoxQty = int.TryParse(txtFullBox.Text, out fullBoxQty) ? fullBoxQty : 0;

                        totalStockIn += packingMaxQty * fullBoxQty;

                        string itemType = cmbPackingCode.Text.Substring(0, 3);

                        if (!string.IsNullOrEmpty(cmbPackingCode.Text) && itemType == "CTN")
                        {
                            dt_Row = dt.NewRow();
                            dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                            dt_Row[header_ItemCode] = cmbPackingCode.Text;
                            dt_Row[header_Cat] = tool.getItemCat(cmbPackingCode.Text);

                            dt_Row[header_From] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();
                            dt_Row[header_To] = text.Production;

                            dt_Row[header_Qty] = fullBoxQty;
                            dt_Row[header_PlanID] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PlanID].Value.ToString();

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


                    if(!string.IsNullOrEmpty(lblPartCode.Text))
                    {
                        //get itemCode
                        //get join qty
                        //get stock in qty
                        string StockInItemCode;
                        
                        if(!string.IsNullOrEmpty(cmbParentList.Text))
                        {
                            StockInItemCode = cmbParentList.Text;

                            float joinQty = tool.getJoinQty(StockInItemCode, lblPartCode.Text);

                            totalStockIn = totalStockIn / (int)joinQty;
                        }
                        else
                        {
                            StockInItemCode = lblPartCode.Text;
                        }

                        dt_Row = dt.NewRow();
                        dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                        dt_Row[header_ItemCode] = StockInItemCode;
                        dt_Row[header_Cat] = text.Cat_Part;

                        dt_Row[header_From] = text.Production;
                        dt_Row[header_To] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();

                        dt_Row[header_Qty] = totalStockIn;
                        dt_Row[header_PlanID] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PlanID].Value.ToString();

                        if (cbMorning.Checked)
                        {
                            dt_Row[header_Shift] = "M";
                        }
                        else if (cbNight.Checked)
                        {
                            dt_Row[header_Shift] = "N";
                        }

                        dt.Rows.Add(dt_Row);

                        if(directStockIn > 0)
                        {
                            dt_Row = dt.NewRow();
                            dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                            dt_Row[header_ItemCode] = StockInItemCode;
                            dt_Row[header_Cat] = text.Cat_Part;

                            dt_Row[header_From] = text.Production;
                            dt_Row[header_To] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();

                            dt_Row[header_Qty] = directStockIn;
                            dt_Row[header_PlanID] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PlanID].Value.ToString();

                            if(planStatus == text.planning_status_completed)
                            {
                                if (cbMorning.Checked)
                                {
                                    dt_Row[header_Shift] = "MB";
                                }
                                else if (cbNight.Checked)
                                {
                                    dt_Row[header_Shift] = "NB";
                                }
                            }
                            else
                            {
                                if (cbMorning.Checked)
                                {
                                    dt_Row[header_Shift] = "M";
                                }
                                else if (cbNight.Checked)
                                {
                                    dt_Row[header_Shift] = "N";
                                }
                            }
                           

                            dt.Rows.Add(dt_Row);
                        }
                    }
                   

                    //MatTrfDate = dtpTrfDate.Text;
                    frmInOutEdit frm = new frmInOutEdit(dt, true);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Normal;
                    frm.ShowDialog();//Item Edit
                }
               
            }
        }

        private void DirectIn()
        {
            if (Validation())
            {
                if (SaveSuccess())//SaveSuccess()
                {
                    DataTable dt = NewStockInTable();
                    DataRow dt_Row;

                    int directStockIn = int.TryParse(txtIn.Text, out directStockIn) ? directStockIn : 0;

                    int selectedItem = dgvItemList.CurrentCell.RowIndex;

                    string planStatus = dgvItemList.Rows[selectedItem].Cells[header_Status].Value.ToString();
                    int balance = int.TryParse(txtBalanceOfThisShift.Text, out balance) ? balance : 0;

                    if (!string.IsNullOrEmpty(lblPartCode.Text))
                    {
                        //get itemCode
                        //get join qty
                        //get stock in qty
                        string StockInItemCode;

                        if (!string.IsNullOrEmpty(cmbParentList.Text))
                        {
                            StockInItemCode = cmbParentList.Text;

                            float joinQty = tool.getJoinQty(StockInItemCode, lblPartCode.Text);

                            directStockIn = directStockIn / (int)joinQty;
                        }
                        else
                        {
                            StockInItemCode = lblPartCode.Text;
                        }

                        dt_Row = dt.NewRow();
                        dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                        dt_Row[header_ItemCode] = StockInItemCode;
                        dt_Row[header_Cat] = text.Cat_Part;

                        dt_Row[header_From] = text.Production;
                        dt_Row[header_To] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();

                        dt_Row[header_Qty] = directStockIn;
                        dt_Row[header_PlanID] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PlanID].Value.ToString();

                        if (cbMorning.Checked)
                        {
                            dt_Row[header_Shift] = "MB";
                        }
                        else if (cbNight.Checked)
                        {
                            dt_Row[header_Shift] = "NB";
                        }

                        dt.Rows.Add(dt_Row);
                    }


                    //MatTrfDate = dtpTrfDate.Text;
                    frmInOutEdit frm = new frmInOutEdit(dt, true);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Normal;
                    frm.ShowDialog();//Item Edit
                }

            }
        }

        private void DirectOut()
        {
            if (Validation())
            {
                if (SaveSuccess())//SaveSuccess()
                {
                    DataTable dt = NewStockInTable();
                    DataRow dt_Row;

                    int directStockOut = int.TryParse(txtOut.Text, out directStockOut) ? directStockOut : 0;

                    int selectedItem = dgvItemList.CurrentCell.RowIndex;

                    if (!string.IsNullOrEmpty(lblPartCode.Text))
                    {
                        //get itemCode
                        //get join qty
                        //get stock in qty
                        string StockInItemCode;

                        if (!string.IsNullOrEmpty(cmbParentList.Text))
                        {
                            StockInItemCode = cmbParentList.Text;

                            float joinQty = tool.getJoinQty(StockInItemCode, lblPartCode.Text);

                            directStockOut = directStockOut / (int)joinQty;
                        }
                        else
                        {
                            StockInItemCode = lblPartCode.Text;
                        }

                        dt_Row = dt.NewRow();
                        dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                        dt_Row[header_ItemCode] = StockInItemCode;
                        dt_Row[header_Cat] = text.Cat_Part;

                        dt_Row[header_From] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();
                        dt_Row[header_To] =  text.Other;

                        dt_Row[header_Qty] = directStockOut;
                        dt_Row[header_PlanID] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PlanID].Value.ToString();

                        if (cbMorning.Checked)
                        {
                            dt_Row[header_Shift] = "MBO";
                        }
                        else if (cbNight.Checked)
                        {
                            dt_Row[header_Shift] = "NBO";
                        }

                        dt.Rows.Add(dt_Row);
                    }


                    //MatTrfDate = dtpTrfDate.Text;
                    frmInOutEdit frm = new frmInOutEdit(dt, true);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Normal;
                    frm.ShowDialog();//Item Edit
                }

            }
        }

        private void loadPackingData()
        {
            DataTable dt_Packaging = dalItem.CatSearch(text.Cat_Packaging);
            DataTable dt_Packaging_ItemName = dt_Packaging.DefaultView.ToTable(true, "item_name");

            DataTable dt_Carton = dalItem.CatSearch(text.Cat_Carton);
            DataTable dt_Carton_ItemName = dt_Carton.DefaultView.ToTable(true, "item_name");

            DataTable dt_All = dt_Packaging.Copy();
            dt_All.Merge(dt_Carton);

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
            cmbPackingName.DisplayMember = "Data";

            cmbPackingCode.DataSource = dt;
            cmbPackingCode.DisplayMember = "Data";

        }

        private int GetLatestBalanceLeft(DataTable dt, string planID)
        {
            int balance = 0;

            DateTime date = DateTime.MaxValue;

            foreach(DataRow row in dt.Rows)
            {
                bool active = Convert.ToBoolean(row[dalProRecord.Active]);
                if(planID == row[dalProRecord.JobNo].ToString() && active)
                {
                    DateTime proDate = Convert.ToDateTime(row[dalProRecord.ProDate]);
                    int balanceLeft = int.TryParse(row[dalProRecord.CurrentShiftBalance].ToString(), out balanceLeft) ? balanceLeft : 0;

                    if (date == DateTime.MaxValue)
                    {
                        date = proDate;
                        balance = balanceLeft;
                    }
                    else if (proDate > date)
                    {
                        date = proDate;
                        balance = balanceLeft;
                    }
                    else if (proDate == date)
                    {
                        string shift = row[dalProRecord.Shift].ToString();

                        if (shift == text.Shift_Night)
                        {
                            balance = balanceLeft;
                        }
                    }
                }
              
            }

            return balance;
        }

        private void LoadPage()
        {
            //dt_Trf = dalTrf.Select();
            loaded = false;
            AddNewSheetUI(false);
            LoadItemListData();

            uProRecord.active = true;
            dgvItemList.ClearSelection();
            dgvRecordHistory.DataSource = null;

            lblCheckBy.Text = "";
            lblCheckDate.Text = "";
            txtTotalProducedRecord.Text = "";
            txtTotalStockInRecord.Text = "";
            cbChecked.Checked = false;
            loaded = true;
        }

        private void frmProductionRecord_Load(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void txtMeterStart_TextChanged(object sender, EventArgs e)
        {
            errorProvider5.Clear();
            CalculateHourlyShot();
        }

        private void frmProductionRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.DailyJobSheetFormOpen = false;
            MainDashboard.NewDailyJobSheetFormOpen = false;
        }

        private void txtTotalStockIn_TextChanged(object sender, EventArgs e)
        {
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
        }

        private void cbMorning_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();

            if(cbMorning.Checked)
            {
                cbNight.Checked = false;
                CreateMeterReadingData();
            }
            else if(!cbNight.Checked)
            {
                dgvMeterReading.DataSource = null;
            }

            if (CheckIfDateOrShiftDuplicate())
            {
                ClearAllError();
                errorProvider1.SetError(lblDate, "Please select a different date");
                errorProvider2.SetError(lblShift, "Please select a different shift");
            }

        }

        private void cbNight_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();

            if (cbNight.Checked)
            {
                cbMorning.Checked = false;
                CreateMeterReadingData();
            }
            else if (!cbMorning.Checked)
            {
                dgvMeterReading.DataSource = null;
            }

            if (CheckIfDateOrShiftDuplicate())
            {
                ClearAllError();
                errorProvider1.SetError(lblDate, "Please select a different date");
                errorProvider2.SetError(lblShift, "Please select a different shift");
            }
        }

        private void btnNewSheet_Click(object sender, EventArgs e)
        {
            if (dgvItemList.SelectedRows.Count > 0)
            {
                dt_Mac = dalMac.Select();

                DateTime proDate = DateTime.Today.AddDays(-1);

                dtpProDate.Value = tool.SubtractDayIfSunday(proDate);
                dt_MultiPackaging = null;
                sheetLoaded = false;

                addingNewSheet = true;
                AddNewSheetUI(true);
                LoadNewSheetData();

                sheetLoaded = true;
            }
            else
            {
                MessageBox.Show("Please select a item before adding new sheet.");
            }
                
        }

        private void tlpSheet_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AddNewSheetUI(false);
            dailyRecordLoaded = true;
        }

        private void btnSaveAndStock_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                SaveAndStock();

                AddNewSheetUI(false);
                LoadDailyRecord();
                dgvRecordHistory.ClearSelection();
                dailyRecordLoaded = true;
            }
               
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("lot no: "+ lotNo);
            if(SaveSuccess())
            {
                AddNewSheetUI(false);

                //load record history
                LoadDailyRecord();
                dgvRecordHistory.ClearSelection();

                dailyRecordLoaded = true;
            }
        }

        private int GetINTOnlyFromLotNo(string proLotNo)
        {
            string LotNo_String = Regex.Match(proLotNo, @"\d+").Value;

            int LotNo_INT = int.TryParse(LotNo_String, out LotNo_INT) ? LotNo_INT : -1;

            return LotNo_INT;
        }

        private void ClearData()
        {
            sheetLoaded = false;
            cbMorning.Checked = false;
            cbNight.Checked = false;
            dgvMeterReading.DataSource = null;

            cmbPackingName.SelectedIndex = -1;
            cmbPackingCode.SelectedIndex = -1;
            cmbParentList.SelectedIndex = -1;

            txtProLotNo.Clear();
            txtPackingMaxQty.Clear();
            txtRawMatLotNo.Clear();
            txtColorMatLotNo.Clear();
            txtMeterStart.Clear();
            txtBalanceOfLastShift.Clear();
            txtBalanceOfThisShift.Clear();
            txtIn.Text = "0";
            txtFullBox.Clear();
            txtTotalStockIn.Clear();
            txtTotalReject.Clear();
            txtRejectPercentage.Clear();
            txtNote.Clear();
        }

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            if(loaded)
            {
                //int rowIndex = dgvItemList.CurrentCell.RowIndex;

                // frmLoading.ShowLoadingScreen();
                Cursor = Cursors.WaitCursor;
                errorProvider3.Clear();
                errorProvider4.Clear();
                AddNewSheetUI(false);

                //load record history
                LoadDailyRecord();

                dgvRecordHistory.ClearSelection();
                //frmLoading.CloseForm();
                Cursor = Cursors.Arrow;
                dailyRecordLoaded = true;
                //MessageBox.Show("selection changed");
            }

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
                        addingNewSheet = false;
                        AddNewSheetUI(true);
                        LoadExistingSheetData();
                    }
                }
            }  
        }

        private void dtpProDate_ValueChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtBalanceOfLastShift_TextChanged(object sender, EventArgs e)
        {
            errorProvider6.Clear();
            //CalculateTotalRejectAndPercentage();
            CalculateTotalStockIn();
        }

        private void txtBalanceOfThisShift_TextChanged(object sender, EventArgs e)
        {
            errorProvider7.Clear();
            CalculateTotalStockIn();
            //CalculateTotalRejectAndPercentage();
        }

        private void txtFullBox_TextChanged(object sender, EventArgs e)
        {
            errorProvider8.Clear();
            CalculateTotalStockIn();

            if(cmbPackingName.Text != string_MultiPackaging)
            SavePackagingToDT();
        }

        private void dgvMeterReading_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(ColumnMeter_KeyPress);
            e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);


            if (dgvMeterReading.CurrentCell.ColumnIndex == 2)//meter
            {
                TextBox tb = e.Control as TextBox;

                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(ColumnMeter_KeyPress);
                }
            }
            else if(dgvMeterReading.CurrentCell.ColumnIndex != 1)
            {
                TextBox tb = e.Control as TextBox;

                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                }
            }
        }

        private void ColumnOperator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void ColumnMeter_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dgvMeterReading_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //calculate hourly
            CalculateHourlyShot();
        }

        private void CalculateHourlyShot()
        {
            if(true)
            {
                int meterStart = 0;

                if (string.IsNullOrEmpty(txtMeterStart.Text))
                {
                    errorProvider5.Clear();
                    errorProvider5.SetError(lblMeterStartAt, "Please input meter start value");
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
                            row[header_Hourly] = meterReading - meterStart;
                            meterStart = meterReading;
                        }
                        else
                        {
                            row[header_Hourly] = 0;
                        }
                    }
                }


                CalculateTotalShot();
            }
            

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

        private void CalculateTotalStockIn()
        {
            if(true)
            {
                int balanceLeftLastShift = int.TryParse(txtBalanceOfLastShift.Text, out balanceLeftLastShift) ? balanceLeftLastShift : 0;
                int balanceLeftThisShift = int.TryParse(txtBalanceOfThisShift.Text, out balanceLeftThisShift) ? balanceLeftThisShift : 0;

                int directIn = int.TryParse(txtIn.Text, out directIn) ? directIn : 0;

                balanceLeftThisShift += directIn;

                if (dt_MultiPackaging != null && dt_MultiPackaging.Rows.Count >= 2)
                {
                    lblClear.Visible = false;
                    LoadPackingToCMB(string_MultiPackaging);
                    txtPackingMaxQty.Text = "MULTI";

                    int totalStockIn = 0;
                    int totalFullBox = 0;

                    foreach (DataRow row in dt_MultiPackaging.Rows)
                    {
                       
                        string packingCode = row[frmProPackaging.header_PackagingCode].ToString();

                        int boxQty = Convert.ToInt32(row[frmProPackaging.header_PackagingQty].ToString());
                        int maxQty = Convert.ToInt32(row[frmProPackaging.header_PackagingMax].ToString());

                        string itemType = packingCode.Substring(0, 3);


                        if (itemType == "CTN" || itemType =="CTR")
                        {
                            totalFullBox += boxQty;

                            totalStockIn += boxQty * maxQty;
                        }

                    }

                    totalStockIn += balanceLeftThisShift - balanceLeftLastShift;
                    txtFullBox.Text = totalFullBox.ToString();
                    txtTotalStockIn.Text = totalStockIn.ToString();

                }
                else
                {
                    int packingMaxQty = int.TryParse(txtPackingMaxQty.Text, out packingMaxQty) ? packingMaxQty : 0;

                    int fullBoxQty = int.TryParse(txtFullBox.Text, out fullBoxQty) ? fullBoxQty : 0;

                    txtTotalStockIn.Text = (packingMaxQty * fullBoxQty + balanceLeftThisShift - balanceLeftLastShift).ToString();
                }
            }
        }

        private void CalculateTotalRejectAndPercentage()
        {
            if(true)
            {
                int availableQty = int.TryParse(txtAvailableQty.Text, out availableQty) ? availableQty : 0;
                int totalIn = int.TryParse(txtTotalStockIn.Text, out totalIn) ? totalIn : 0;

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
                timer1.Stop();
                timer1.Start();

                CalculateTotalStockIn();
                SavePackagingToDT();
            }
        }

        private void txtTotalShot_TextChanged(object sender, EventArgs e)
        {
            int totalShot = int.TryParse(txtTotalShot.Text, out totalShot) ? totalShot : 0;
            int cavity = int.TryParse(lblCavity.Text, out cavity) ? cavity : 0;

            txtAvailableQty.Text = (totalShot * cavity).ToString();

            
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            //open machine schedule page
            
            frmMachineSchedule frm = new frmMachineSchedule(true)
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
            loaded = false;
            LoadItemListData();
            dgvItemList.ClearSelection();
            loaded = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvItemList.SelectedRows)
            {
                string name = row.Cells[header_PartName].Value.ToString();
                MessageBox.Show(name);
            }
        }

        private void RemoveCompleted()
        {
            loaded = false;
            DataGridView dgv = dgvItemList;
            DataTable dt = (DataTable)dgv.DataSource;

            if(dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string planStatus = row[header_Status].ToString();

                    if (planStatus == text.planning_status_completed)
                    {
                        int planID = Convert.ToInt32(row[header_PlanID].ToString());

                        uPlan.plan_id = planID;
                        uPlan.recording = false;

                        if (!dalPlan.RecordingUpdate(uPlan))
                        {

                            MessageBox.Show("Failed to update daily plan!");


                        }
                    }
                }

                LoadItemListData();
                loaded = true;
            }
           
        }

        private void ClearList()
        {
            loaded = false;
            DataGridView dgv = dgvItemList;
            DataTable dt = (DataTable)dgv.DataSource;

            if(dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string planStatus = row[header_Status].ToString();

                    int planID = Convert.ToInt32(row[header_PlanID].ToString());

                    uPlan.plan_id = planID;
                    uPlan.recording = false;

                    if (!dalPlan.RecordingUpdate(uPlan))
                    {

                        MessageBox.Show("Failed to update daily plan!");


                    }
                }

                LoadItemListData();
                loaded = true;
            }
            
        }
        private void dgvItemList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            loaded = false;

            int userID = MainDashboard.USER_ID;
            int userPermission = dalUser.getPermissionLevel(userID);

            //handle the row selection on right click
            
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && (userPermission >= MainDashboard.ACTION_LVL_FIVE || userID == 6))// && userPermission >= MainDashboard.ACTION_LVL_THREE
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgvItemList.CurrentCell = dgvItemList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvItemList.Rows[e.RowIndex].Selected = true;
                dgvItemList.Focus();
                int rowIndex = dgvItemList.CurrentCell.RowIndex;

                try
                {
                    my_menu.Items.Add("Remove from this list.").Name = "Remove from this list.";

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(dgvItemList_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            loaded = true;
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvItemList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvItemList;
            dgv.SuspendLayout();

            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            int planID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_PlanID].Value);
            string itemName = dgv.Rows[rowIndex].Cells[header_PartName].Value.ToString();
            contextMenuStrip1.Hide();
            if (itemClicked.Equals("Remove from this list."))
            {
                if (MessageBox.Show("Are you sure you want to remove (PLAN ID: "+planID+") "+itemName+" from this list?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    uPlan.plan_id = planID;
                    uPlan.recording = false;

                    if(dalPlan.RecordingUpdate(uPlan))
                    {
                        loaded = false;
                        MessageBox.Show("Item removed!");

                        DataTable dt = (DataTable)dgv.DataSource;
                       
                        dt.Rows.RemoveAt(rowIndex);
                        dt.AcceptChanges();

                        dgvItemList.ClearSelection();

                        if (dt == null || dt.Rows.Count <= 0)
                        {
                            lblClearList.Hide();
                            lblRemoveCompleted.Hide();
                        }

                        loaded = true;
                        //LoadItemListData();
                    }
                }

            }
         
            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void dgvDailyRecord_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvRecordHistory;
            dgv.SuspendLayout();

            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            int sheetID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_SheetID].Value);
            string itemName = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PartName].Value.ToString();

            contextMenuStrip1.Hide();

            if (itemClicked.Equals("Remove from this list."))
            {
                if (MessageBox.Show("Are you sure you want to remove (SHEET ID: " + sheetID + ") " + itemName + " from this list?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int userID = MainDashboard.USER_ID;
                    DateTime updateTime = DateTime.Now;
                    uProRecord.sheet_id = sheetID;
                    uProRecord.active = false;
                    uProRecord.updated_date = updateTime;
                    uProRecord.updated_by = userID;

                    if (dalProRecord.RemoveSheetData(uProRecord))
                    {
                        MessageBox.Show("Item removed!");
                        tool.historyRecord(text.RemoveDailyJobSheet, "Sheet ID: " + sheetID + "(" + lblPartCode.Text + ")", updateTime, userID);
                        LoadDailyRecord();
                        AddNewSheetUI(false);
                    }
                }

            }

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
            if (sheetLoaded)
            {
                timer1.Stop();
                timer1.Start();
                
            }

            SavePackagingToDT();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(sheetLoaded)
            {
                timer1.Stop();
                UpdatePackagingData();
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

                if (DataPassed)
                {
                    dt_MultiPackaging = null;


                    dt_MultiPackaging = NewPackagingTable();

                    int maxQty = int.TryParse(txtPackingMaxQty.Text, out maxQty) ? maxQty : 0;
                    int totalBox = int.TryParse(txtFullBox.Text, out totalBox) ? totalBox : 0;

                    string packagingCode = cmbPackingCode.Text;
                    string packagingName = cmbPackingName.Text;

                    DataRow dt_Row = dt_MultiPackaging.NewRow();

                    dt_Row[header_PackagingCode] = packagingCode;
                    dt_Row[header_PackagingName] = packagingName;
                    dt_Row[header_PackagingMax] = maxQty;
                    dt_Row[header_PackagingQty] = totalBox;

                    dt_MultiPackaging.Rows.Add(dt_Row);
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
                lblClear.Visible = false;
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
                txtTotalStockIn.Text = totalStockIn.ToString();

            }
            else if (rowCount == 1)
            {
                lblClear.Visible = true;
                loadPackingData();

                cmbPackingName.Text = dt_MultiPackaging.Rows[0][frmProPackaging.header_PackagingName].ToString();
                cmbPackingCode.Text = dt_MultiPackaging.Rows[0][frmProPackaging.header_PackagingCode].ToString();

                txtPackingMaxQty.Text = dt_MultiPackaging.Rows[0][frmProPackaging.header_PackagingMax].ToString();
                txtFullBox.Text = dt_MultiPackaging.Rows[0][frmProPackaging.header_PackagingQty].ToString();
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
            //lotNo = int.TryParse(txtProLotNo.Text, out lotNo) ? lotNo : 0;
        }

        private string SetAlphabetToProLotNo(int macID, int lotNo)
        {
            return tool.GetAlphabet(macID - 1) + " - " + lotNo;
        }

        private void txtProLotNo_Leave(object sender, EventArgs e)
        {

            lotNo = int.TryParse(txtProLotNo.Text, out lotNo) ? lotNo : 0;

            txtProLotNo.Text = SetAlphabetToProLotNo(macID, lotNo);
            //txtProLotNo.Text = tool.GetAlphabet(macID-1) + " - " + lotNo;
        }

        private void txtProLotNo_Enter(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtProLotNo.Text))
            txtProLotNo.Text = lotNo.ToString();
        }

        private void dgvRecordHistory_Click(object sender, EventArgs e)
        {
            int row = dgvRecordHistory.CurrentCell.RowIndex;

            if(row >= 0 && dgvRecordHistory.SelectedRows.Count > 0)
            {
                if (dailyRecordLoaded)
                {
                    errorProvider3.Clear();
                    errorProvider4.Clear();

                    int rowIndex = dgvRecordHistory.CurrentCell.RowIndex;

                    if (rowIndex >= 0)
                    {
                        addingNewSheet = false;
                        AddNewSheetUI(true);
                        LoadExistingSheetData();
                    }
                }
            }
        }

        private void dtpProDate_ValueChanged_1(object sender, EventArgs e)
        {
            if(CheckIfDateOrShiftDuplicate())
            {
                ClearAllError();
                errorProvider1.SetError(lblDate, "Please select a different date");
                errorProvider2.SetError(lblShift, "Please select a different shift");
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                if (Validation())
                {
                    SaveAndStock();

                    AddNewSheetUI(false);
                    LoadDailyRecord();
                    dgvRecordHistory.ClearSelection();
                    dailyRecordLoaded = true;
                }
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            
            Cursor = Cursors.WaitCursor;
            RemoveCompleted();
            Cursor = Cursors.Arrow;
        }

        private void lblClearList_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ClearList();
            Cursor = Cursors.Arrow;
        }

        private void label14_Click_1(object sender, EventArgs e)
        {
            cmbParentList.SelectedIndex = -1;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void dgvRecordHistory_Sorted(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvRecordHistory.DataSource;

            int total = 0;
            //get total
            //foreach(DataRow row in dt.Rows)
            //{
            //    string total_str = row[]
            //}
        }

        private void txtUsed_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtdirectIn_TextChanged(object sender, EventArgs e)
        {
            errorProvider7.Clear();
            CalculateTotalStockIn();
        }

        private void txtUsed_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dgvItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvItemList;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == header_Status)
            {
                string status = dgv.Rows[row].Cells[header_Status].Value.ToString();

                if(status == text.planning_status_completed)
                {
                    dgv.Rows[row].Cells[header_PlanID].Style.BackColor = Color.LightBlue;
                }
                else
                {
                    dgv.Rows[row].Cells[header_PlanID].Style.BackColor = Color.White;
                }
            }
        }

        private void lblIn_DoubleClick(object sender, EventArgs e)
        {
            //direct save and In
            DirectIn();
            AddNewSheetUI(false);
            LoadDailyRecord();
            dgvRecordHistory.ClearSelection();
            dailyRecordLoaded = true;

        }

        private void lblIn_Click(object sender, EventArgs e)
        {

        }

        private void lblOut_DoubleClick(object sender, EventArgs e)
        {
            DirectOut();

            AddNewSheetUI(false);
            LoadDailyRecord();
            dgvRecordHistory.ClearSelection();
            dailyRecordLoaded = true;
        }

        private void dgvRecordHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvRecordHistory.Columns[header_TotalStockIn].InheritedStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
        }

        private void txtTotalStockInRecord_TextChanged(object sender, EventArgs e)
        {
            string totalProduced = txtTotalProducedRecord.Text;
            string totalStockIn = txtTotalStockInRecord.Text;

            if(totalStockIn != totalProduced)
            {
                txtTotalStockInRecord.BackColor = Color.Pink;
            }
            else
            {
                if(!string.IsNullOrEmpty(totalProduced) && !string.IsNullOrEmpty(totalStockIn))
                {
                    txtTotalStockInRecord.BackColor = Color.LightGreen;
                }
                else
                {
                    txtTotalStockInRecord.BackColor = Color.White;
                }
                
            }
        }

        private void UpdateCheckedStatus(bool Checked, int checkBy, DateTime date)
        {
            
            DataGridView dgv = dgvItemList;
            int rowIndex = dgv.CurrentCell.RowIndex;
            int planID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_PlanID].Value);
            string planStatus = dgv.Rows[rowIndex].Cells[header_Status].Value.ToString() ;
            string itemCode = dgv.Rows[rowIndex].Cells[header_PartCode].Value.ToString();

            uPlan.plan_id = planID;
            uPlan.CheckedDate = date;
            uPlan.CheckedBy = checkBy;
            uPlan.Checked = Checked;

            
            if (dalPlan.CheckedUpdate(uPlan))
            {
                if (planStatus == text.planning_status_completed)
                {
                    uPlan.recording = !Checked;

                    if (!dalPlan.RecordingUpdate(uPlan))
                    {
                        MessageBox.Show("Failed to update recording status!");

                    }

                }
                if (Checked)
                {
                    tool.historyRecord(text.CheckedJobSheet, "Plan ID: " + planID + "(" + itemCode + ")", date, checkBy);

                   
                }
                
                else
                {
                    tool.historyRecord(text.UncheckedJobSheet, "Plan ID: " + planID + "(" + itemCode + ")", date, checkBy);
                }

                dt_ProductionRecord = dalProRecord.Select();
            }

        }
        private void cbChecked_CheckedChanged(object sender, EventArgs e)
        {
            if(!stopCheckedChange && loaded)
            {
                int userID = MainDashboard.USER_ID;
                int userPermission = dalUser.getPermissionLevel(userID);
                string userName = dalUser.getUsername(userID);
                DateTime dateNow = DateTime.Now;
                bool valid = userPermission >= MainDashboard.ACTION_LVL_FIVE || userID == 6;
                bool checkedStatus = cbChecked.Checked;


                if (checkedStatus)
                {
                    if (valid)
                    {
                        lblCheckBy.Text = string_CheckBy + userName;
                        lblCheckDate.Text = string_CheckDate + dateNow;
                        //update
                        UpdateCheckedStatus(checkedStatus, userID, dateNow);

                    }
                    else if (!stopCheckedChange)
                    {
                        stopCheckedChange = true;
                        cbChecked.Checked = false;
                        stopCheckedChange = false;
                    }
                }
                else
                {
                    if (valid)
                    {
                        lblCheckBy.Text = "";
                        lblCheckDate.Text = "";
                        //update
                        UpdateCheckedStatus(checkedStatus, userID, dateNow);
                    }
                    else
                    {
                        stopCheckedChange = true;
                        cbChecked.Checked = true;
                        stopCheckedChange = false;
                    }
                }
            }

            

        }
    }
}
