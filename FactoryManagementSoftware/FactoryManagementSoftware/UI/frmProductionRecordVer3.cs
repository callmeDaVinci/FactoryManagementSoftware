using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmProductionRecordVer3 : Form
    {
        public frmProductionRecordVer3()
        {
            InitializeComponent();
            AutoScroll = true;
            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
           
            tool.DoubleBuffered(dgvActiveJobList, true);
            tool.DoubleBuffered(dgvRecordHistory, true);

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

        DataTable dt_Plan;
        DataTable dt_ProductionRecord;
        DataTable dt_ItemInfo;
        DataTable dt_JoinInfo;
        DataTable dt_MultiPackaging;
        DataTable DT_CARTON;
        DataTable dt_MultiParent;
        DataTable dt_Mac;
        // DataTable dt_Trf;


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

            dt.Columns.Add(header_JobNo, typeof(int));
            dt.Columns.Add(header_Machine, typeof(int));
            dt.Columns.Add(header_Factory, typeof(string));
            dt.Columns.Add(header_PartName, typeof(string));
            dt.Columns.Add(header_PartCode, typeof(string));
            dt.Columns.Add(header_Status, typeof(string));
            dt.Columns.Add(text.Header_TargetQty, typeof(int));
            dt.Columns.Add(text.Header_QCPassedQty, typeof(int));

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
            dgv.Columns[header_MeterReading].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.Columns[header_Hourly].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                string colName = dgv.Columns[i].Name;

                if(colName.Contains(text.Header_DefectRemark))
                {
                    dgv.Columns[colName].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                    dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.Columns[colName].ReadOnly = true;
                }
                else if (colName.Contains(text.Header_QtyReject))
                {
                    dgv.Columns[colName].DefaultCellStyle.BackColor = SystemColors.Info;
                    dgv.Columns[colName].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                    dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }

            dgv.Columns[header_Time].Frozen = true;

        }

        private void dgvActiveJobListStyleEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[header_PartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

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
          
            //if(!cbMorning.Checked && !cbNight.Checked)
            //{
            //    errorProvider2.SetError(lblShift, "Please select shift");
            //    passed = false;
            //}

            //if(string.IsNullOrEmpty(lblPartName.Text))
            //{
            //    errorProvider3.SetError(lblPartNameHeader, "Please select a item on the left table");
            //    passed = false;
            //}

            //if (string.IsNullOrEmpty(lblPartCode.Text))
            //{
            //    errorProvider4.SetError(lblPartCodeHeader, "Please select a item on the left table");
            //    passed = false;
            //}

            //if (string.IsNullOrEmpty(txtMeterStart.Text))
            //{
            //    errorProvider5.SetError(lblMeterStartAt, "Please input meter start value");
            //    passed = false;
            //}

            //if (string.IsNullOrEmpty(txtBalanceOfLastShift.Text))
            //{
            //    errorProvider6.SetError(lblBalanceLast, "Please input balance of last shift");
            //    passed = false;
            //}

            //if (string.IsNullOrEmpty(txtBalanceOfThisShift.Text))
            //{
            //    errorProvider7.SetError(lblBalanceCurrent, "Please input balance of this shift");
            //    passed = false;
            //}

            //if (string.IsNullOrEmpty(txtFullBox.Text))
            //{
            //    errorProvider8.SetError(lblFullCarton, "Please input full carton qty");
            //    passed = false;
            //}

            //if (string.IsNullOrEmpty(txtTotalProduce.Text))
            //{
            //    errorProvider9.SetError(lblTotalStockIn, "Please input total stock in qty");
            //    passed = false;
            //}

            //if (string.IsNullOrEmpty(txtPackingMaxQty.Text))
            //{
            //    errorProvider10.SetError(lblPacking, "Please input total qty per box");
            //    passed = false;
            //}

            return passed;
        }

        private void LoadItemListData()
        {
            dt_ProductionRecord = dalProRecord.SelectActiveDailyJobRecordOnly();
            
            DataGridView dgv = dgvActiveJobList;
            DataTable dt = NewItemListTable();

            //dt_Plan = dalPlan.Select();
            dt_Plan = dalPlan.SelectRecordingPlan();

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
                    dt_Row[header_JobNo] = row[dalPlan.jobNo];
                    dt_Row[header_PartName] = row[dalItem.ItemName];
                    dt_Row[header_PartCode] = row[dalItem.ItemCode];
                    dt_Row[header_Status] = row[dalPlan.planStatus];
                    dt_Row[text.Header_TargetQty] = row[dalPlan.targetQty];
                    dt_Row[text.Header_QCPassedQty] = row[dalPlan.planProduced];

                    dt.Rows.Add(dt_Row);
                }

            }

            dgv.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                //if (userPermission >= MainDashboard.ACTION_LVL_FOUR)
                //{
                //    lblRemoveCompleted.Show();
                //    lblClearList.Show();
                //}

                dgv.DataSource = dt;
                dgvActiveJobListStyleEdit(dgv);
                dgv.ClearSelection();
            }
            //else
            //{
            //    lblRemoveCompleted.Hide();
            //    lblClearList.Hide();
            //}

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
                string itemCode = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[header_PartCode].Value.ToString();
                string planID = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[header_JobNo].Value.ToString();
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

                                itemCode = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[header_PartCode].Value.ToString();
                               // transferData = dalTrf.codeLikeSearch(itemCode);

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
                //^575ms > 122ms(21/3)

                //change last row color
                foreach (DataGridViewRow dgvRow in dgvRecordHistory.Rows)
                {
                    if (previousDate == Convert.ToDateTime(dgvRow.Cells[header_ProductionDate].Value.ToString()))
                    {
                        //check if parent
                        string sheetID = dgvRow.Cells[header_SheetID].Value.ToString();

                        itemCode = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[header_PartCode].Value.ToString();
                        //transferData = dalTrf.codeLikeSearch(itemCode);

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

               //^148ms > 36ms(21/3)
                txtTotalStockInRecord.Text = totalStockIn.ToString();
            }

        }


        private void UndoPreviousRecordIfExist()
        {
            DataGridView dgv = dgvActiveJobList;

            string planID = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[header_JobNo].Value.ToString();

            DateTime proDate = DateTime.TryParse(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[header_ProDate].Value.ToString(), out proDate) ? proDate : DateTime.MaxValue;

            DataTable dt_Trf;

            if (OLD_PRO_DATE == DateTime.MaxValue)
            {
                dt_Trf = dalTrf.rangeTrfSearch(proDate.ToString("yyyy/MM/dd"), proDate.ToString("yyyy/MM/dd"));

            }
            else
            {
                dt_Trf = dalTrf.rangeTrfSearch(OLD_PRO_DATE.ToString("yyyy/MM/dd"), OLD_PRO_DATE.ToString("yyyy/MM/dd"));

            }

           

            string shift = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[header_Shift].Value.ToString();


            dt_Trf.DefaultView.Sort = dalTrf.TrfDate + " DESC";
            dt_Trf = dt_Trf.DefaultView.ToTable();

            int trfTableCode = -1;

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

                    bool dataMatch = shift == _shift && planID == _planID;

                    if (dataMatch)
                    {
                        trfTableCode = int.TryParse(row[dalTrf.TrfID].ToString(), out trfTableCode) ? trfTableCode : -1;

                        if (trfTableCode != -1)
                            tool.UndoTransferRecord(trfTableCode);

                    }
                }

            }

        }

        private bool CheckIfPreviousRecordExist()
        {
            DataGridView dgv = dgvActiveJobList;

            DateTime proDate = DateTime.TryParse(dgv.Rows[dgv.CurrentCell.RowIndex].Cells[header_ProDate].Value.ToString(), out proDate) ? proDate : DateTime.MaxValue;

            string planID = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[header_JobNo].Value.ToString();

            DataTable dt_Trf;

            if (OLD_PRO_DATE == DateTime.MaxValue)
            {
                dt_Trf = dalTrf.rangeTrfSearch(proDate.ToString("yyyy/MM/dd"), proDate.ToString("yyyy/MM/dd"));

            }
            else
            {
                dt_Trf = dalTrf.rangeTrfSearch(OLD_PRO_DATE.ToString("yyyy/MM/dd"), OLD_PRO_DATE.ToString("yyyy/MM/dd"));

            }

            string shift = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[header_Shift].Value.ToString();

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

                    bool dataMatch = shift == _shift && planID == _planID;

                    if (dataMatch)
                    {
                        return true;

                    }
                }

            }

            return false;

        }

        private void LoadDailyRecord()
        {
            dailyRecordLoaded = false;

            int itemRow = -1;
            if (dgvActiveJobList.CurrentCell != null)
            {
                itemRow = dgvActiveJobList.CurrentCell.RowIndex;
            }
            
            if(itemRow >= 0)
            {
                //btnShowDailyRecord.Visible = true;
                string JobNo = dgvActiveJobList.Rows[itemRow].Cells[header_JobNo].Value.ToString();
                string planStatus = dgvActiveJobList.Rows[itemRow].Cells[header_Status].Value.ToString();
                macID = int.TryParse(dgvActiveJobList.Rows[itemRow].Cells[header_Machine].Value.ToString(), out macID) ? macID : 0;

                DataTable dt = NewSheetRecordTable();
                DataRow dt_Row;
                
                int totalProducedQty = 0;
                foreach(DataRow row in dt_ProductionRecord.Rows)
                {
                    bool active = Convert.ToBoolean(row[dalProRecord.Active].ToString());
                    if(JobNo == row[dalProRecord.JobNo].ToString() && active)
                    {
                        dt_Row = dt.NewRow();

                        int produced = Convert.ToInt32(row[dalProRecord.TotalProduced].ToString());
                        int proLotNo = int.TryParse(row[dalProRecord.ProLotNo].ToString(), out proLotNo) ? proLotNo : -1;
                        totalProducedQty += produced;

                       

                        if(proLotNo != -1)
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

                        if(dt.Rows.Count > 0)
                        dt.Rows[dt.Rows.Count - 1][header_TotalProduced] = DBNull.Value;

                        dt.Rows.Add(dt_Row);
                        
                        

                    }
                }

                txtTotalProducedRecord.Text = totalProducedQty.ToString();
                txtTargetQty.Text = dgvActiveJobList.Rows[itemRow].Cells[text.Header_TargetQty].Value.ToString();

                ProducedVSTargetQty();
                //update total produced
                uPlan.plan_id = Convert.ToInt32(JobNo);
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
                LoadCheckedStatus(JobNo, planStatus);

                CheckIfStockIn();
            }
            else
            {
                //btnShowDailyRecord.Visible = false;
            }
           
        }
        private void LoadCheckedStatus(string jobNo, string planStatus)
        {
            if(planStatus == text.planning_status_completed)
            {
                
                DataTable dt = dalPlan.idSearch(jobNo);
                foreach (DataRow row in dt.Rows)
                {
                    if (jobNo == row[dalPlan.jobNo].ToString())
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
     
        private DateTime GetStatusUpdatedDate(string jobNo)
        {
            DateTime UpdatedDate = DateTime.MaxValue;

            foreach(DataRow row in dt_Plan.Rows)
            {
                if(jobNo == row[dalPlan.jobNo].ToString())
                {
                    //UpdatedDate = Convert.ToDateTime();
                    UpdatedDate = DateTime.TryParse(row[dalPlan.planUpdatedDate].ToString(), out UpdatedDate) ? UpdatedDate : DateTime.MaxValue;

                    return UpdatedDate;
                }
            }

            return UpdatedDate;
        }

        private string RAW_MAT_CODE = "";
        private string COLOR_MAT_CODE = "";
        private float COLOR_MAT_RATE = 0;
        private float PW_PER_SHOT = 0;
        private float RW_PER_SHOT = 0;

        private void LoadPlanInfo()
        {
            //RAW_MAT_CODE = "";
            //COLOR_MAT_CODE = "";
            //COLOR_MAT_RATE = 0;
            //PW_PER_SHOT = 0;
            //RW_PER_SHOT = 0;

            //int selectedItem = dgvActiveJobList.CurrentCell.RowIndex;

            //if (selectedItem <= -1)
            //{
            //    MessageBox.Show("Please select a item before adding new sheet.");
            //}
            //else
            //{
            //    string partName = dgvActiveJobList.Rows[selectedItem].Cells[header_PartName].Value.ToString();
            //    string partCode = dgvActiveJobList.Rows[selectedItem].Cells[header_PartCode].Value.ToString();
            //    string jobNo = dgvActiveJobList.Rows[selectedItem].Cells[header_JobNo].Value.ToString();
            //    string targetQty = dgvActiveJobList.Rows[selectedItem].Cells[text.Header_TargetQty].Value.ToString();

            //    lblCustomer.Text = "";
            //    lblPartName.Text = partName;
            //    lblPartCode.Text = partCode;
            //    txtTargetQty.Text = targetQty;

            //    lblPlanUpdatedDate.Text = GetStatusUpdatedDate(jobNo).ToString();

            //    foreach (DataRow row in dt_ItemInfo.Rows)
            //    {
            //        if (row[dalItem.ItemCode].ToString().Equals(partCode))
            //        {
            //            float pwPerShot = float.TryParse(row[dalItem.ItemProPWShot].ToString(), out float i) ? i : 0;
            //            float rwPerShot = float.TryParse(row[dalItem.ItemProRWShot].ToString(), out float k) ? k : 0;

            //            PW_PER_SHOT = pwPerShot;
            //            RW_PER_SHOT = rwPerShot;

            //            lblJobNo.Text = jobNo;
                      
            //            lblPW.Text = pwPerShot.ToString("0.##");
            //            lblRW.Text = rwPerShot.ToString("0.##");

            //            lblCavity.Text = row[dalItem.ItemCavity] == DBNull.Value ? "" : row[dalItem.ItemCavity].ToString();
            //            lblCycleTime.Text = row[dalItem.ItemProCTTo] == DBNull.Value ? "" : row[dalItem.ItemProCTTo].ToString();

            //            lblRawMat.Text = row[dalItem.ItemMaterial] == DBNull.Value ? "" : row[dalItem.ItemMaterial].ToString();
            //            RAW_MAT_CODE = lblRawMat.Text;

            //            string colorMat = row[dalItem.ItemMBatch] == DBNull.Value ? "" : row[dalItem.ItemMBatch].ToString();
            //            COLOR_MAT_CODE = colorMat;

            //            float colorRate = row[dalItem.ItemMBRate] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemMBRate]);
            //            string colorUsage = (colorRate * 100).ToString("0.##");

            //            COLOR_MAT_RATE = colorRate >= 1? colorRate/100: colorRate;

            //            lblColorMat.Text = colorMat + " ( " + colorUsage + "%)";

            //        }

            //    }

               
            //}

        }
       
        private void LoadPage()
        {
            //dt_Trf = dalTrf.Select();
            loaded = false;
           // btnShowDailyRecord.Visible = false;
            LoadItemListData();//1329ms
            ShowProductionListUI();
            uProRecord.active = true;
            dgvActiveJobList.ClearSelection();
            dgvRecordHistory.DataSource = null;

            lblCheckBy.Text = "";
            lblCheckDate.Text = "";
            txtTotalProducedRecord.Text = "";
            ProducedVSTargetQty();
            txtTotalStockInRecord.Text = "";
            cbChecked.Checked = false;
            loaded = true;
        }

        private void frmProductionRecord_Load(object sender, EventArgs e)
        {
            LoadPage();
        }


        private void frmProductionRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.NewDailyJobSheetFormOpen = false;
        }

        private void ShowDailyRecordUI()
        {
            LoadPlanInfo();
            //tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0);
            //tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);

            //tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            //tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 850);


            //tlpTotalProducedTotalStockIn.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            //tlpTotalProducedTotalStockIn.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 100f);
            //tlpTotalProducedTotalStockIn.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 95f);
            //tlpTotalProducedTotalStockIn.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 95f);
            //tlpTotalProducedTotalStockIn.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 70f);

        }

        private void ShowProductionListUI()
        {

            //tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0);
            //tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);

            //tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            //tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 400);
        }

        private void UnableKeyIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dgvRecordHistory_SelectionChanged(object sender, EventArgs e)
        {
           
        }
       
        bool CellEditingMode = false;

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
            dgvActiveJobList.ClearSelection();
            loaded = true;
        }

        private void dgvDailyRecord_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
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
      
    
        private void dgvRecordHistory_Click(object sender, EventArgs e)
        {
            if(dgvRecordHistory.CurrentCell != null)
            {
                int row = dgvRecordHistory.CurrentCell.RowIndex;

                if (row >= 0 && dgvRecordHistory.SelectedRows.Count > 0)
                {
                    if (dailyRecordLoaded)
                    {
                        errorProvider3.Clear();
                        errorProvider4.Clear();

                        int rowIndex = dgvRecordHistory.CurrentCell.RowIndex;

                        if (rowIndex >= 0)
                        {
                            addingNewSheet = false;
                            //AddNewSheetUI(true);
                            //LoadExistingSheetData();
                        }
                    }
                }
            }
          
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            frmLoading.ShowLoadingScreen();
            LoadPage();
            frmLoading.CloseForm();
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
            
            DataGridView dgv = dgvActiveJobList;
            int rowIndex = dgv.CurrentCell.RowIndex;
            int JobNo = Convert.ToInt32(dgv.Rows[rowIndex].Cells[header_JobNo].Value);
            string planStatus = dgv.Rows[rowIndex].Cells[header_Status].Value.ToString() ;
            string itemCode = dgv.Rows[rowIndex].Cells[header_PartCode].Value.ToString();

            uPlan.plan_id = JobNo;
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
                    tool.historyRecord(text.CheckedJobSheet, "Job No.: " + JobNo + "(" + itemCode + ")", date, checkBy);

                   
                }
                
                else
                {
                    tool.historyRecord(text.UncheckedJobSheet, "Job No.: " + JobNo + "(" + itemCode + ")", date, checkBy);
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

        private void button4_Click(object sender, EventArgs e)
        {
           
            ShowDailyRecordUI();
        }

        private void txtTotalProducedRecord_TextChanged(object sender, EventArgs e)
        {
            updateDiff();
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

    }
}
