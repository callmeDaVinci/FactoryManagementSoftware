using System;
using System.Data;
using System.Drawing;
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
            loadPackingData();
            CreateMeterReadingData();

            dt_ItemInfo = dalItem.Select();
            dt_JoinInfo = dalJoin.SelectAll();
        }

        Text text = new Text();
        Tool tool = new Tool();

        itemDAL dalItem = new itemDAL();
        planningDAL dalPlan = new planningDAL();
        PlanningBLL uPlan = new PlanningBLL();
        MacDAL dalMac = new MacDAL();
        userDAL dalUser = new userDAL();
        joinDAL dalJoin = new joinDAL();
        joinBLL uJoin = new joinBLL();
        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();
        ProductionRecordBLL uProRecord = new ProductionRecordBLL();

        DataTable dt_ItemInfo;
        DataTable dt_JoinInfo;

        readonly private string string_NewSheet = "NEW SHEET";
        readonly private string header_Time = "TIME";
        readonly private string header_Operator = "OPERATOR";
        readonly private string header_MeterReading = "METER READING";
        readonly private string header_Hourly = "HOURLY";

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
        readonly private string header_UpdatedDate = "UPDATED DATE";
        readonly private string header_UpdatedBy = "UPDATED BY";
        readonly private string header_Total = "TOTAL";

        readonly string header_Cat = "Cat";
        readonly string header_ProDate = "DATE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_From = "FROM";
        readonly string header_To = "TO";
        readonly string header_Qty = "QTY";

        private bool addingNewSheet = false;
        private bool sheetLoaded = false;
        private bool dailyRecordLoaded = false;
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

            return dt;
        }

        private DataTable NewSheetRecordTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_SheetID, typeof(int));
            dt.Columns.Add(header_ProductionDate, typeof(DateTime));
            dt.Columns.Add(header_Shift, typeof(string));
            dt.Columns.Add(header_ProducedQty, typeof(double));
            dt.Columns.Add(header_UpdatedDate, typeof(DateTime));
            dt.Columns.Add(header_UpdatedBy, typeof(string));
            dt.Columns.Add(header_Total, typeof(double));

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
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.Columns[header_SheetID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ProductionDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Shift].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ProducedQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_UpdatedDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_UpdatedBy].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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

            if (string.IsNullOrEmpty(txtTotalStockIn.Text))
            {
                errorProvider9.SetError(lblTotalStockIn, "Please input total stock in qty");
                passed = false;
            }

            return passed;
        }

        private void AddNewSheetUI(bool newSheet)
        {
            dgvMeterReading.SuspendLayout();

            if (newSheet)
            {
                tlpSheet.RowStyles[0] = new RowStyle(SizeType.Percent, 0);
                tlpSheet.RowStyles[1] = new RowStyle(SizeType.Percent, 100);

                btnNewSheet.Visible = false;
                dgvMeterReading.ResumeLayout();
            }
            else
            {
                ClearData();
                sheetLoaded = false;
                addingNewSheet = false;
                tlpSheet.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                tlpSheet.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                btnNewSheet.Visible = true;
                dgvMeterReading.ResumeLayout();
            }
          
        }

        private void LoadItemListData()
        {
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
                    dt_Row[header_Factory] = row[dalMac.MacLocation];
                    dt_Row[header_PlanID] = row[dalPlan.planID];
                    dt_Row[header_PartName] = row[dalItem.ItemName];
                    dt_Row[header_PartCode] = row[dalItem.ItemCode];
                    dt_Row[header_Status] = row[dalPlan.planStatus];

                    dt.Rows.Add(dt_Row);
                }

            }

            dgv.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                dgv.DataSource = dt;
                dgvItemListStyleEdit(dgv);
                //dgv.ClearSelection();
            }
        }

        private void LoadDailyRecord()
        {
            dailyRecordLoaded = false;

            int itemRow = -1;
            if (dgvItemList.CurrentCell != null)
            {
                itemRow = dgvItemList.CurrentCell.RowIndex;
            }
            

            if(itemRow > 0)
            {
                string planID = dgvItemList.Rows[itemRow].Cells[header_PlanID].Value.ToString();
                DataTable dt = NewSheetRecordTable();
                DataRow dt_Row;
                DataTable dt_ProductionRecord = dalProRecord.Select();
                int total = 0;
                foreach(DataRow row in dt_ProductionRecord.Rows)
                {
                    if(planID == row[dalProRecord.PlanID].ToString())
                    {
                        dt_Row = dt.NewRow();

                        int produced = Convert.ToInt32(row[dalProRecord.TotalProduced].ToString());

                        total += produced;
                        
                        dt_Row[header_SheetID] = row[dalProRecord.SheetID].ToString();
                        dt_Row[header_ProductionDate] = row[dalProRecord.ProDate].ToString();
                        dt_Row[header_Shift] = row[dalProRecord.Shift].ToString();
                        dt_Row[header_ProducedQty] = produced;
                        dt_Row[header_UpdatedDate] = row[dalProRecord.UpdatedDate].ToString();
                        dt_Row[header_UpdatedBy] = dalUser.getUsername(Convert.ToInt32(row[dalProRecord.UpdatedBy].ToString()));

                        dt_Row[header_Total] = total;

                        if(dt.Rows.Count > 0)
                        dt.Rows[dt.Rows.Count - 1][header_Total] = DBNull.Value;

                        dt.Rows.Add(dt_Row);
                        
                        

                    }
                }

                dgvRecordHistory.DataSource = null;

                dgvRecordHistory.DataSource = dt;
                dgvSheetRecordStyleEdit(dgvRecordHistory);
                dgvRecordHistory.ClearSelection();

                //if (dt.Rows.Count > 0)
                //{
                    
                //}
            }

            dailyRecordLoaded = true;
        }

        private void LoadSheetRecordHistoryData()
        {
            DataGridView dgv = dgvRecordHistory;
            DataTable dt = NewSheetRecordTable();

            dgv.DataSource = dt;
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

                                    txtPackingQty.Text = packagingQty;
                                    cmbPackingName.Text = packagingName;
                                    cmbPackingCode.Text = packagingCode;

                                    break;
                                }
                            }
                        }
                    }

                }

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

                        txtProLotNo.Text = row[dalProRecord.ProLotNo].ToString();
                        txtRawMatLotNo.Text = row[dalProRecord.RawMatLotNo].ToString();
                        txtColorMatLotNo.Text = row[dalProRecord.ColorMatLotNo].ToString();
                        txtMeterStart.Text = row[dalProRecord.MeterStart].ToString();
                        txtBalanceOfLastShift.Text = row[dalProRecord.LastShiftBalance].ToString();
                        txtBalanceOfThisShift.Text = row[dalProRecord.CurrentShiftBalance].ToString();
                        txtFullBox.Text = row[dalProRecord.FullBox].ToString();
                        txtTotalStockIn.Text = row[dalProRecord.TotalProduced].ToString();
                        //txtTotalReject.Text = row[dalProRecord.ProLotNo].ToString();
                       // txtRejectPercentage.Text = row[dalProRecord.ProLotNo].ToString();
                    }

                }

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

                                if (cat.Equals(text.Cat_Carton) || cat.Equals(text.Cat_Packaging))
                                {
                                    string packagingName = rowItem[dalItem.ItemName].ToString();
                                    string packagingCode = rowItem[dalItem.ItemCode].ToString();
                                    string packagingQty = row[dalJoin.JoinMax].ToString();

                                    txtPackingQty.Text = packagingQty;
                                    cmbPackingName.Text = packagingName;
                                    cmbPackingCode.Text = packagingCode;

                                    break;
                                }
                            }
                        }
                    }

                }

            }

            sheetLoaded = true;

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
                int qty = int.TryParse(txtPackingQty.Text, out qty) ? qty : -1;
                string packagingName = cmbPackingName.Text;
                string packagingCode = cmbPackingCode.Text;

                if (qty != -1 && !string.IsNullOrEmpty(packagingName) && !string.IsNullOrEmpty(packagingCode))
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

        private void SaveOnly()
        {
            if(Validation())
            {
                //TO-DO:
                //save sheet data
                string planID = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PlanID].Value.ToString();
                int sheetID = int.TryParse(txtSheetID.Text, out sheetID)? sheetID : -1;

                string Shift = text.Shift_Night;
                int meterStart = Convert.ToInt32(txtMeterStart.Text);
                int meterEnd = meterStart;
                int lastShiftBalance = Convert.ToInt32(txtBalanceOfLastShift.Text);
                int thisShiftBalance = Convert.ToInt32(txtBalanceOfThisShift.Text);
                int fullBox = Convert.ToInt32(txtFullBox.Text);
                int TotalStockIn = Convert.ToInt32(txtTotalStockIn.Text);
                int TotalReject = Convert.ToInt32(txtTotalReject.Text);
                double RejectPercentage = Convert.ToDouble(txtRejectPercentage.Text);

                DateTime updateTime = DateTime.Now;

                if (cbMorning.Checked)
                {
                    Shift = text.Shift_Morning;
                }

                uProRecord.plan_id = Convert.ToInt16(planID);
                uProRecord.production_date = dtpProDate.Value;
                uProRecord.shift = Shift;
                uProRecord.production_lot_no = txtProLotNo.Text;
                uProRecord.raw_mat_lot_no = txtRawMatLotNo.Text;
                uProRecord.color_mat_lot_no = txtColorMatLotNo.Text;
                uProRecord.meter_start = meterStart;

                foreach(DataGridViewRow row in dgvMeterReading.Rows)
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
                uProRecord.meter_end = meterEnd;

                uProRecord.last_shift_balance = lastShiftBalance;
                uProRecord.current_shift_balance = thisShiftBalance;
                uProRecord.full_box = fullBox;
                uProRecord.total_produced = TotalStockIn;
                uProRecord.total_reject = TotalReject;
                uProRecord.reject_percentage = RejectPercentage;
                uProRecord.updated_date = updateTime;
                uProRecord.updated_by = MainDashboard.USER_ID;
                uProRecord.sheet_id = sheetID;

                bool success = false;
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

                    MessageBox.Show("Data saved!");
                }
                else
                {
                    MessageBox.Show("Failed to save data");
                }
                //save meter data

            }
        }

        private void SaveAndStock()
        {
            if (Validation())
            { 
                //TO-DO:
                //save sheet data
                //save meter data

                MessageBox.Show("Data saved!");

                DataTable dt = NewStockInTable();
                DataRow dt_Row = dt.NewRow();

                dt_Row[header_ProDate] = dtpProDate.Value.ToString("ddMMMMyy");
                dt_Row[header_ItemCode] = lblPartCode.Text;
                dt_Row[header_Cat] = text.Cat_Part;

                dt_Row[header_From] = text.Production;
                dt_Row[header_To] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_Factory].Value.ToString();
                
                dt_Row[header_Qty] = txtTotalStockIn.Text;
                dt_Row[header_PlanID] = dgvItemList.Rows[dgvItemList.CurrentCell.RowIndex].Cells[header_PlanID].Value.ToString();

                dt.Rows.Add(dt_Row);

                //MatTrfDate = dtpTrfDate.Text;
                frmInOutEdit frm = new frmInOutEdit(dt,true);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Normal;
                frm.ShowDialog();//Item Edit
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

        private void frmProductionRecord_Load(object sender, EventArgs e)
        {
            AddNewSheetUI(false);
            LoadItemListData();
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            errorProvider5.Clear();
            CalculateHourlyShot();
        }

        private void frmProductionRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.DailyJobSheetFormOpen = false;
        }

        private void txtMeterStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtBalanceOfLastShift_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtBalanceOfThisShift_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtFullBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtTotalStockIn_TextChanged(object sender, EventArgs e)
        {
            errorProvider9.Clear();
            CalculateTotalRejectAndPercentage();
        }

        private void txtTotalStockIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtPackingQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtTotalShot_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtTotalReject_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtRejectPercentage_KeyPress(object sender, KeyPressEventArgs e)
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

            //foreach(DataRow row in ((DataTable)dgvMeterReading.DataSource).Rows)
            //{
                


                
            //}

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
        }

        private void btnNewSheet_Click(object sender, EventArgs e)
        {
            sheetLoaded = false;
            addingNewSheet = true;
            AddNewSheetUI(true);

            LoadNewSheetData();

            sheetLoaded = true;
        }

        private void tlpSheet_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AddNewSheetUI(false);
        }

        private void btnSaveAndStock_Click(object sender, EventArgs e)
        {
            SaveAndStock();
            AddNewSheetUI(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveOnly();
            AddNewSheetUI(false);
        }

        private void ClearData()
        {
            sheetLoaded = false;
            cbMorning.Checked = false;
            cbNight.Checked = false;
            dgvMeterReading.DataSource = null;

            cmbPackingName.SelectedIndex = -1;
            cmbPackingCode.SelectedIndex = -1;

            txtProLotNo.Clear();
            txtPackingQty.Clear();
            txtRawMatLotNo.Clear();
            txtColorMatLotNo.Clear();
            txtMeterStart.Clear();
            txtBalanceOfLastShift.Clear();
            txtBalanceOfThisShift.Clear();
            txtFullBox.Clear();
            txtTotalStockIn.Clear();
            txtTotalReject.Clear();
            txtRejectPercentage.Clear();
        }

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
            errorProvider4.Clear();
            AddNewSheetUI(false);

            //load record history
            LoadDailyRecord();

           
            //MessageBox.Show("selection changed");
        }

        private void dgvRecordHistory_SelectionChanged(object sender, EventArgs e)
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

        private void dtpProDate_ValueChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtBalanceOfLastShift_TextChanged(object sender, EventArgs e)
        {
            errorProvider6.Clear();
            CalculateTotalRejectAndPercentage();
        }

        private void txtBalanceOfThisShift_TextChanged(object sender, EventArgs e)
        {
            errorProvider7.Clear();
            CalculateTotalRejectAndPercentage();
        }

        private void txtFullBox_TextChanged(object sender, EventArgs e)
        {
            errorProvider8.Clear();
            CalculateTotalStockIn();
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

            if(dt != null)
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

        private void CalculateTotalShot()
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

        private void CalculateTotalStockIn()
        {
            int packingMaxQty = int.TryParse(txtPackingQty.Text, out packingMaxQty) ? packingMaxQty : 0;

            int fullBoxQty = int.TryParse(txtFullBox.Text, out fullBoxQty) ? fullBoxQty : 0;

            txtTotalStockIn.Text = (packingMaxQty * fullBoxQty).ToString();

            

        }

        private void CalculateTotalRejectAndPercentage()
        {
            int totalShot = int.TryParse(txtTotalShot.Text, out totalShot) ? totalShot : 0;
            int cavity = int.TryParse(lblCavity.Text, out cavity) ? cavity : 0;
            int totalIn = int.TryParse(txtTotalStockIn.Text, out totalIn) ? totalIn : 0;
            int balanceOfLastShift = int.TryParse(txtBalanceOfLastShift.Text, out balanceOfLastShift) ? balanceOfLastShift : 0;
            int balanceOfThisShift = int.TryParse(txtBalanceOfThisShift.Text, out balanceOfThisShift) ? balanceOfThisShift : 0;

            int totalReject = totalShot * cavity - (totalIn + balanceOfThisShift - balanceOfLastShift);
            txtTotalReject.Text = totalReject.ToString();

            if(totalShot * cavity == 0)
            {
                totalShot = 1;
                cavity = 1;
            }
            double rejectPercentage = (double) totalReject / (totalShot * cavity) * 100;
            txtRejectPercentage.Text = rejectPercentage.ToString();
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
            if(sheetLoaded)
            {
                timer1.Stop();
                timer1.Start();
            }
           
        }

        private void txtTotalShot_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalRejectAndPercentage();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            //open machine schedule page
            frmMachineSchedule frm = new frmMachineSchedule(true)
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();

            LoadItemListData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvItemList.SelectedRows)
            {
                string name = row.Cells[header_PartName].Value.ToString();
                MessageBox.Show(name);
            }
        }

        private void dgvItemList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
            //handle the row selection on right click

            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && userPermission >= MainDashboard.ACTION_LVL_THREE)
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
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
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
                        MessageBox.Show("Item removed!");

                        DataTable dt = (DataTable)dgv.DataSource;
                       
                        dt.Rows.RemoveAt(rowIndex);
                        dt.AcceptChanges();
                        

                        //LoadItemListData();
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
            if(sheetLoaded)
            {
                UpdatePackagingData();
            }
            
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
    }
}
