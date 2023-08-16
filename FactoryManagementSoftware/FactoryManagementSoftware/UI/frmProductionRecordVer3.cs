using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FactoryManagementSoftware.UI
{
    public partial class frmProductionRecordVer3 : Form
    {
        public frmProductionRecordVer3()
        {
            InitializeComponent();
           
            tool.DoubleBuffered(dgvActiveJobList, true);
            tool.DoubleBuffered(dgvRecordHistory, true);

            DT_ITEM_INFO = dalItem.Select();
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
        DataTable DT_JOB_DAILY_RECORD;
        DataTable DT_ITEM_INFO;
        DataTable dt_JoinInfo;
        DataTable dt_MultiPackaging;
        DataTable DT_CARTON;
        DataTable dt_MultiParent;
        DataTable dt_Mac;
        // DataTable dt_Trf;


        //private DateTime OLD_PRO_DATE = DateTime.MaxValue;

        //int userPermission = -1;
        //readonly private string string_NewSheet = "NEW";
        //readonly private string string_Multi = "MULTI";
        //readonly private string string_MultiPackaging = "MULTI PACKAGING";

        //readonly private string header_Time = "TIME";
        //readonly private string text.Header_Operator = "OPERATOR";
        //readonly private string header_MeterReading = "METER READING";
        //readonly private string header_Hourly = "HOURLY";

        ////readonly private string header_ProLotNo = "PRO LOT NO";
        //readonly private string text.Header_MacID = "MAC.";
        //readonly private string text.Header_Fac = "FAC.";
        //readonly private string text.Header_JobNo = "JOB NO.";
        //readonly private string text.Header_ItemName = "NAME";
        //readonly private string text.Header_ItemCode = "CODE";
        //readonly private string text.Header_Status = "STATUS";
        //readonly private string text.Header_SheetID = "SHEET ID";
        //readonly private string text.Header_ProductionDate = "PRO. DATE";
        //readonly private string text.Header_Shift = "SHIFT";
        //readonly private string header_ProducedQty = "PRODUCED QTY";
        //readonly private string text.Header_TotalStockIn = "STOCK IN";
        //readonly private string text.Header_UpdatedDate = "UPDATED DATE";
        //readonly private string text.Header_UpdatedBy = "UPDATED BY";
        //readonly private string header_TotalProduced = "TOTAL PRODUCED";
        //readonly private string text.Header_TotalStockIn = "TOTAL STOCK IN";

        //readonly static public string header_PackagingCode = "CODE";
        //readonly static public string header_PackagingName = "NAME";
        //readonly static public string header_PackagingQty = "CARTON QTY";
        //readonly static public string header_PackagingMax = "PART QTY PER BOX";
        //readonly static public string header_PackagingStockOut = "STOCK OUT";

        readonly private string text_RemoveRecord = "Remove from this list.";
        readonly private string text_ChangeJobNo = "Change Job To";
        readonly private string string_CheckBy= "CHECK BY: ";
        readonly private string string_CheckDate = "DATE: ";

        //readonly string header_Cat = "Cat";
        //readonly string text.Header_ProductionDate = "DATE";
        //readonly string header_ItemCode = "ITEM CODE";
        //readonly string header_From = "FROM";
        //readonly string header_To = "TO";
        //readonly string header_Qty = "QTY";

        private int macID = 0;
        private int JOB_NO = 0;

        private bool loaded = false;
        private bool addingNewSheet = false;
        private bool sheetLoaded = false;
        //private bool dailyRecordLoaded = false;
        private bool stopCheckedChange = false;
        private bool DATA_SAVED = true;
        private bool balanceOutEdited = false;
        private bool balanceInEdited = false;
      

        private DataTable NewItemListTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Fac, typeof(string));
            dt.Columns.Add(text.Header_MacID, typeof(int));
            dt.Columns.Add(text.Header_JobNo, typeof(int));
            dt.Columns.Add(text.Header_Status, typeof(string));
            dt.Columns.Add(text.Header_ItemNameAndCode, typeof(string));

            dt.Columns.Add(text.Header_TargetQty, typeof(int));
            dt.Columns.Add(text.Header_Production_Max_Qty, typeof(int));
            dt.Columns.Add(text.Header_TotalStockIn, typeof(int));

            dt.Columns.Add(text.Header_DateStart, typeof(DateTime));
            dt.Columns.Add(text.Header_EstDateEnd, typeof(DateTime));
            dt.Columns.Add(text.Header_Cavity, typeof(int));
            dt.Columns.Add(text.Header_ProCT, typeof(int));
            dt.Columns.Add(text.Header_QCPassedQty, typeof(int));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            return dt;
        }


        private DataTable NewJobDailyRecordTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_SheetID, typeof(int));
            dt.Columns.Add(text.Header_ProductionDate, typeof(DateTime));
            dt.Columns.Add(text.Header_Shift, typeof(string));
            dt.Columns.Add(text.Header_Operator, typeof(string));
            dt.Columns.Add(text.Header_MeterStart, typeof(int));
            dt.Columns.Add(text.Header_MeterEnd, typeof(int));
            dt.Columns.Add(text.Header_TimeStart, typeof(string));
            dt.Columns.Add(text.Header_TimeEnd, typeof(string));
            dt.Columns.Add(text.Header_Cavity, typeof(int));
            dt.Columns.Add(text.Header_Production_Max_Qty, typeof(int));
            dt.Columns.Add(text.Header_StockIn_Remark, typeof(string));
            dt.Columns.Add(text.Header_RawMat_Lot_No, typeof(string));
            dt.Columns.Add(text.Header_ColorMat_Lot_No, typeof(string));

            dt.Columns.Add(text.Header_Remark, typeof(string));
            dt.Columns.Add(text.Header_UpdatedDate, typeof(DateTime));
            dt.Columns.Add(text.Header_UpdatedBy, typeof(string));

            dt.Columns.Add(text.Header_TotalStockIn, typeof(int));
            dt.Columns.Add(text.Header_StockIn_Balance_Qty, typeof(int));
            dt.Columns.Add(text.Header_StockIn_Container, typeof(int));
            dt.Columns.Add(text.Header_Qty_Per_Container, typeof(int));
            dt.Columns.Add(text.Header_JobNo, typeof(string));
            //dt.Columns.Add(header_ProducedQty, typeof(double));
            //dt.Columns.Add(header_TotalProduced, typeof(double));
            //dt.Columns.Add(text.Header_TotalStockIn, typeof(double));

            return dt;
        }

        private void dgvActiveJobListStyleEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[text.Header_ItemNameAndCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dgv.Columns[text.Header_MacID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[text.Header_Fac].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[text.Header_JobNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[text.Header_Status].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.Columns[text.Header_ItemNameAndCode].MinimumWidth = 150;

            //dgv.Columns[text.Header_Status].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns[text.Header_QCPassedQty].Visible = false;
            dgv.Columns[text.Header_ItemName].Visible = false;
            dgv.Columns[text.Header_ItemCode].Visible = false;

            dgv.Columns[text.Header_DateStart].Visible = false;
            dgv.Columns[text.Header_EstDateEnd].Visible = false;
            dgv.Columns[text.Header_Cavity].Visible = false;
            dgv.Columns[text.Header_ProCT].Visible = false;
           
        }

        private void dgvSheetRecordStyleEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            //dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            dgv.Columns[text.Header_SheetID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_ProductionDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_Shift].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_ProducedQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_UpdatedDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_UpdatedBy].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_TotalProduced].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[text.Header_Cavity].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_RawMat_Lot_No].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_ColorMat_Lot_No].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[text.Header_UpdatedDate].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgv.Columns[text.Header_Shift].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            dgv.Columns[text.Header_SheetID].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgv.Columns[text.Header_RawMat_Lot_No].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            dgv.Columns[text.Header_ColorMat_Lot_No].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            dgv.Columns[text.Header_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
            //dgv.Columns[text.Header_MeterStart].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.Columns[text.Header_MeterEnd].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.Columns[text.Header_Cavity].DefaultCellStyle.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            //dgv.Columns[text.Header_Production_Max_Qty].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            //dgv.Columns[text.Header_StockIn_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            dgv.Columns[text.Header_TotalStockIn].DefaultCellStyle.BackColor = SystemColors.Info;

            dgv.Columns[text.Header_TotalStockIn].Visible = false;
            dgv.Columns[text.Header_StockIn_Balance_Qty].Visible = false;
            dgv.Columns[text.Header_StockIn_Container].Visible = false;
            dgv.Columns[text.Header_JobNo].Visible = false;
         
        }

        private void dgvActiveJobListCellFormatting(DataGridView dgv)
        {
            dgv.SuspendLayout();

            DataTable dt = (DataTable)dgv.DataSource;

            foreach (DataRow row in dt.Rows)
            {
                dgv.Rows[dt.Rows.IndexOf(row)].Cells[text.Header_Status].Style.BackColor = tool.GetColorSetFromPlanStatus(row[text.Header_Status].ToString(), dgv);
            }
           
            dgv.ResumeLayout();
        }

        private void dgvSheetRecordListCellFormatting(DataGridView dgv)
        {
            dgv.SuspendLayout();

            DataTable dt = (DataTable)dgv.DataSource;

            //foreach (DataRow row in dt.Rows)
            //{
            //    string shift = row[text.Header_Shift].ToString();

            //    if (shift.ToUpper() == "NIGHT")
            //    {
            //        //dgv.Rows[dt.Rows.IndexOf(row)].Cells[text.Header_Shift].Style.BackColor = Color.LightGray;
            //        dgv.Rows[dt.Rows.IndexOf(row)].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            //    }
            //    else
            //    {
            //        //dgv.Rows[dt.Rows.IndexOf(row)].Cells[text.Header_Shift].Style.BackColor = Color.White;
            //        dgv.Rows[dt.Rows.IndexOf(row)].DefaultCellStyle.BackColor = Color.White;

            //    }
            //}

            foreach (DataGridViewRow dgvRow in dgvRecordHistory.Rows)
            {
                int trfQty = int.TryParse(dgvRow.Cells[text.Header_StockIn_Remark].Value.ToString(), out trfQty) ? trfQty : 0;

                if (trfQty > 0)
                {
                    dgvRow.Cells[text.Header_StockIn_Remark].Style.BackColor = Color.LightGreen;
                }
                else
                {
                    dgvRow.Cells[text.Header_StockIn_Remark].Style.BackColor = Color.Pink;
                }
            }

            //foreach(DataGridViewRow row in dgvRecordHistory.Rows)
            //{
            //    string shift = row.Cells[text.Header_Shift].Value.ToString();

            //    if (shift.ToUpper() == "NIGHT")
            //    {
            //        row.Cells[text.Header_SheetID].Style.BackColor = Color.Black;
            //    }
            //    else
            //    {
            //       //row.Cells[text.Header_Shift].Style.BackColor = Color.White;

            //    }
            //}

            dgv.ResumeLayout();
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
            DT_JOB_DAILY_RECORD = dalProRecord.SelectActiveDailyJobRecordOnly();
            
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

                    dt_Row[text.Header_MacID] = row[dalPlan.machineID];
                    dt_Row[text.Header_Fac] = row[dalMac.MacLocationName];
                    dt_Row[text.Header_JobNo] = row[dalPlan.jobNo];

                    string itemName = row[dalItem.ItemName].ToString();
                    string itemCode = row[dalItem.ItemCode].ToString();
                    string itemCodePresent = row[dalItem.ItemCodePresent].ToString();

                    if (string.IsNullOrEmpty(itemCodePresent))
                    {
                        itemCodePresent = itemCode;
                    }

                    string itemNameAndCodeString = tool.getItemNameAndCodeString(itemCodePresent, itemName);

                    dt_Row[text.Header_ItemName] = itemName;
                    dt_Row[text.Header_ItemCode] = itemCodePresent;
                    dt_Row[text.Header_ItemNameAndCode] = itemNameAndCodeString;

                    dt_Row[text.Header_Status] = tool.ConvertToTitleCase(row[dalPlan.planStatus].ToString());

                    dt_Row[text.Header_TargetQty] = row[dalPlan.targetQty];
                    dt_Row[text.Header_QCPassedQty] = row[dalPlan.planProduced];

                    dt_Row[text.Header_DateStart] = row[dalPlan.productionStartDate];
                    dt_Row[text.Header_EstDateEnd] = row[dalPlan.productionEndDate];
                    dt_Row[text.Header_Cavity] = row[dalPlan.planCavity];
                    dt_Row[text.Header_ProCT] = row[dalPlan.planCT];


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
                dgvActiveJobListCellFormatting(dgv);
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
                
                dt.DefaultView.Sort = text.Header_ProductionDate + " ASC";
                dt = dt.DefaultView.ToTable();

                DateTime previousDate = DateTime.MaxValue;
                int totalProducedQty = 0;
                int totalStockIn = 0;
                string itemCode = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[text.Header_ItemCode].Value.ToString();
                string planID = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();
                DataTable transferData = dalTrf.codeLikeSearch(itemCode);//88ms

                foreach (DataRow row in dt.Rows)
                {
                    DateTime proDate = Convert.ToDateTime(row[text.Header_ProductionDate].ToString());
                    //int producedQty = int.TryParse(row[header_ProducedQty].ToString(), out producedQty) ? producedQty : 0;

                    if(previousDate == DateTime.MaxValue)
                    {
                        previousDate = proDate;
                        //totalProducedQty += producedQty;
                    }
                    else if(previousDate == proDate)
                    {
                        //totalProducedQty += producedQty;
                    }
                    else
                    {
                        foreach(DataGridViewRow dgvRow in dgvRecordHistory.Rows)
                        {
                            if(previousDate == Convert.ToDateTime(dgvRow.Cells[text.Header_ProductionDate].Value.ToString()))
                            {
                                //check if parent
                                string sheetID = dgvRow.Cells[text.Header_SheetID].Value.ToString();

                                itemCode = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[text.Header_ItemCode].Value.ToString();
                               // transferData = dalTrf.codeLikeSearch(itemCode);

                                int trfQty = tool.TotalProductionStockInInOneDay(transferData, itemCode, previousDate, planID, dgvRow.Cells[text.Header_Shift].Value.ToString());
                               
                                if(trfQty > 0)
                                {
                                    dgvRow.Cells[text.Header_StockIn_Remark].Style.BackColor = Color.LightGreen;
                                }
                                else
                                {
                                    dgvRow.Cells[text.Header_StockIn_Remark].Style.BackColor = Color.Pink;
                                }

                                if (trfQty == -1)
                                {
                                    //dgvRow.Cells[text.Header_SheetID].Style.BackColor = Color.Pink;
                                }
                                else if (totalProducedQty == trfQty)
                                {
                                    totalStockIn += trfQty;
                                    //dgvRow.Cells[text.Header_SheetID].Style.BackColor = Color.LightGreen;
                                    dgvRow.Cells[text.Header_TotalStockIn].Value = trfQty;
                                    dgvRow.Cells[text.Header_StockIn_Remark].Value = trfQty;

                                }
                                else if (totalProducedQty != trfQty)
                                {
                                    totalStockIn += trfQty;
                                    //dgvRow.Cells[text.Header_SheetID].Style.BackColor = Color.LightBlue;
                                    dgvRow.Cells[text.Header_TotalStockIn].Value = trfQty;
                                    dgvRow.Cells[text.Header_StockIn_Remark].Value = trfQty;

                                }
                            }
                        }

                        previousDate = proDate;
                        //totalProducedQty = producedQty;
                    }

                }
                //^575ms > 122ms(21/3)

                //change last row color
                foreach (DataGridViewRow dgvRow in dgvRecordHistory.Rows)
                {
                    if (previousDate == Convert.ToDateTime(dgvRow.Cells[text.Header_ProductionDate].Value.ToString()))
                    {
                        //check if parent
                        string sheetID = dgvRow.Cells[text.Header_SheetID].Value.ToString();

                        itemCode = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[text.Header_ItemCode].Value.ToString();
                        //transferData = dalTrf.codeLikeSearch(itemCode);

                        int trfQty_ = tool.TotalProductionStockInInOneDay(transferData, itemCode, previousDate, planID, dgvRow.Cells[text.Header_Shift].Value.ToString());

                        if (trfQty_ == -1)
                        {
                            //dgvRow.Cells[text.Header_SheetID].Style.BackColor = Color.Pink;
                        }
                        else if (totalProducedQty == trfQty_)
                        {
                            totalStockIn += trfQty_;
                            //dgvRow.Cells[text.Header_SheetID].Style.BackColor = Color.LightGreen;
                            dgvRow.Cells[text.Header_TotalStockIn].Value = totalStockIn;
                            dgvRow.Cells[text.Header_StockIn_Remark].Value = trfQty_;

                        }
                        else if (totalProducedQty != trfQty_)
                        {
                            totalStockIn += trfQty_;
                            //dgvRow.Cells[text.Header_SheetID].Style.BackColor = Color.LightBlue;
                            dgvRow.Cells[text.Header_TotalStockIn].Value = totalStockIn;
                            dgvRow.Cells[text.Header_StockIn_Remark].Value = trfQty_;

                        }
                    }
                }

               //^148ms > 36ms(21/3)
                txtQtyStockedIn.Text = totalStockIn.ToString();
            }

        }


        private void LoadJobDailyRecord()
        {
            LOADING_JOB_RECORD = true;

            DataGridView dgv = dgvActiveJobList;

            int itemRow = -1;

            if (dgvActiveJobList.CurrentCell != null)
            {
                itemRow = dgvActiveJobList.CurrentCell.RowIndex;
            }
            

            if(itemRow >= 0)
            {
                //btnShowDailyRecord.Visible = true;
                string itemCode = dgv.Rows[itemRow].Cells[text.Header_ItemCode].Value.ToString();
                string JobNo = dgv.Rows[itemRow].Cells[text.Header_JobNo].Value.ToString();
                string planStatus = dgv.Rows[itemRow].Cells[text.Header_Status].Value.ToString();
                macID = int.TryParse(dgv.Rows[itemRow].Cells[text.Header_MacID].Value.ToString(), out macID) ? macID : 0;

                DataTable dt = NewJobDailyRecordTable();
                DataRow dt_Row;
                
                if(DT_JOB_DAILY_RECORD?.Rows.Count <= 0)
                {
                    DT_JOB_DAILY_RECORD = dalProRecord.SelectActiveDailyJobRecordOnly();
                }

                int totalStockedIn = 0;
                int maxOutputQty = 0;

                foreach (DataRow row in DT_JOB_DAILY_RECORD.Rows)
                {
                    bool active = Convert.ToBoolean(row[dalProRecord.Active].ToString());
                    if(JobNo == row[dalProRecord.JobNo].ToString() && active)
                    {
                        dt_Row = dt.NewRow();

                        int stockedIn = Convert.ToInt32(row[dalProRecord.TotalStockedIn].ToString());
                        //int produced = Convert.ToInt32(row[dalProRecord.max].ToString());
                        int proLotNo = int.TryParse(row[dalProRecord.ProLotNo].ToString(), out proLotNo) ? proLotNo : -1;
                        totalStockedIn += stockedIn;

                        maxOutputQty += int.TryParse(row[dalProRecord.MaxOutputQty].ToString(), out int x) ? x : 0;

                        if (proLotNo != -1)
                        {
                           // dt_Row[text.Header_JobNo] = SetAlphabetToProLotNo(macID, proLotNo);
                            dt_Row[text.Header_JobNo] = JobNo;
                        }

                        //get item cavity
                        int cavity = int.TryParse(row[dalProRecord.ProCavity].ToString(), out cavity) ? cavity : tool.getItemCavityFromDataTable(DT_ITEM_INFO, itemCode);

                        int meterStart = int.TryParse(row[dalProRecord.MeterStart].ToString(), out meterStart) ? meterStart : 0;
                        int meterEnd = int.TryParse(row[dalProRecord.MeterEnd].ToString(), out meterEnd) ? meterEnd : 0;

                        int totalShot = meterEnd - meterStart;

                        int maxQty = totalShot * cavity;

                        dt_Row[text.Header_SheetID] = row[dalProRecord.SheetID].ToString();
                        dt_Row[text.Header_Shift] = row[dalProRecord.Shift].ToString();
                        dt_Row[text.Header_ProductionDate] = row[dalProRecord.ProDate].ToString();
                        dt_Row[text.Header_MeterStart] = row[dalProRecord.MeterStart];
                        dt_Row[text.Header_MeterEnd] = row[dalProRecord.MeterEnd];
                        dt_Row[text.Header_Cavity] = cavity;
                        dt_Row[text.Header_Production_Max_Qty] = maxQty;

                        dt_Row[text.Header_Operator] = row[dalProRecord.ProOperator].ToString();
                        dt_Row[text.Header_TotalStockIn] = row[dalProRecord.TotalStockIn];

                        dt_Row[text.Header_RawMat_Lot_No] = row[dalProRecord.RawMatLotNo].ToString();
                        dt_Row[text.Header_ColorMat_Lot_No] = row[dalProRecord.ColorMatLotNo].ToString();

                        dt_Row[text.Header_StockIn_Container] = row[dalProRecord.FullBox].ToString();
                        dt_Row[text.Header_StockIn_Balance_Qty] = row[dalProRecord.directIn].ToString();
                        dt_Row[text.Header_Qty_Per_Container] = row[dalProRecord.PackagingQty];
                        dt_Row[text.Header_Remark] = row[dalProRecord.Note];

                        if (row[dalProRecord.TimeStart] is DateTime TimeStart)
                        {
                            dt_Row[text.Header_TimeStart] = TimeStart.ToString("HH:mm tt");
                        }

                        if (row[dalProRecord.TimeEnd] is DateTime TimeEnd)
                        {
                            dt_Row[text.Header_TimeEnd] = TimeEnd.ToString("HH:mm tt");
                        }
                       
                        //dt_Row[header_ProducedQty] = produced;
                        dt_Row[text.Header_UpdatedDate] = row[dalProRecord.UpdatedDate].ToString();
                        dt_Row[text.Header_UpdatedBy] = dalUser.getUsername(Convert.ToInt32(row[dalProRecord.UpdatedBy].ToString()));

                        //dt_Row[header_TotalProduced] = totalProducedQty;

                        //if(dt.Rows.Count > 0)
                        //dt.Rows[dt.Rows.Count - 1][header_TotalProduced] = DBNull.Value;

                        dt.Rows.Add(dt_Row);
                        
                        

                    }
                }

                txtTotalMaxOutputQty.Text = maxOutputQty.ToString();
                txtQtyStockedIn.Text = totalStockedIn.ToString();

                ProducedVSTargetQty();
                //update total produced
                uPlan.plan_id = Convert.ToInt32(JobNo);
                uPlan.plan_produced = totalStockedIn;
                if(!dalPlan.TotalProducedUpdate(uPlan))
                {
                    MessageBox.Show("Failed to update total produced qty!");
                }

                dgvRecordHistory.DataSource = dt;
                

                //load checked status
                LoadCheckedStatus(JobNo, planStatus);

                CheckIfStockIn();
                dgvSheetRecordStyleEdit(dgvRecordHistory);

                dgvSheetRecordListCellFormatting(dgvRecordHistory);

                dgvRecordHistory.ClearSelection();

            }

            LOADING_JOB_RECORD = false;
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

                        cbFinalDataReview.Visible = true;
                        lblFinalDataReview.Visible = true;
                        if (Checked)
                        {
                            stopCheckedChange = true;
                            cbFinalDataReview.Checked = true;
                            stopCheckedChange = false;

                            int checkedBy = int.TryParse(row[dalPlan.CheckedBy].ToString(), out checkedBy) ? checkedBy : -1;
                            string userName = dalUser.getUsername(checkedBy);
                            string checkedDate = row[dalPlan.CHeckedDate].ToString();

                            lblFinalDataReviewString.Text = string_CheckBy + userName + ", " + string_CheckDate + checkedDate;
                        }
                        else
                        {
                            stopCheckedChange = true;
                            cbFinalDataReview.Checked = false;
                            stopCheckedChange = false;

                            lblFinalDataReviewString.Text = "";
                        }

                        break;
                    }

                }
            }
            else
            {
                cbFinalDataReview.Visible = false;
                lblFinalDataReview.Visible = false;
                lblFinalDataReviewString.Text = "";
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
            //    string partName = dgvActiveJobList.Rows[selectedItem].Cells[text.Header_ItemName].Value.ToString();
            //    string partCode = dgvActiveJobList.Rows[selectedItem].Cells[text.Header_ItemCode].Value.ToString();
            //    string jobNo = dgvActiveJobList.Rows[selectedItem].Cells[text.Header_JobNo].Value.ToString();
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

            lblFinalDataReviewString.Text = "";
            txtTotalMaxOutputQty.Text = "";

            //ProducedVSTargetQty();
            txtQtyStockedIn.Text = "";
            cbFinalDataReview.Checked = false;
            loaded = true;
        }

        private void frmProductionRecord_Load(object sender, EventArgs e)
        {
            LoadPage();
        }


        private void frmProductionRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.NewDailyJobSheetFormOpenVer3 = false;
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

        private void UndoPreviousRecordIfExist()
        {
            string planID = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();

            DateTime proDate = DateTime.TryParse(dgvRecordHistory.Rows[dgvRecordHistory.CurrentCell.RowIndex].Cells[text.Header_ProductionDate].Value.ToString() , out proDate)? proDate : DateTime.MaxValue;

            DataTable dt_Trf;

            dt_Trf = dalTrf.rangeTrfSearch(proDate.ToString("yyyy/MM/dd"), proDate.ToString("yyyy/MM/dd"));

            string shift = dgvRecordHistory.Rows[dgvRecordHistory.CurrentCell.RowIndex].Cells[text.Header_Shift].Value.ToString();


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

        private void dgvDailyRecord_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvRecordHistory;
            dgv.SuspendLayout();

            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            int sheetID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[text.Header_SheetID].Value);
            string itemName = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[text.Header_ItemName].Value.ToString();
            string itemCode = dgvActiveJobList.Rows[dgvActiveJobList.CurrentCell.RowIndex].Cells[text.Header_ItemCode].Value.ToString();

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
                        UndoPreviousRecordIfExist();
                        tool.historyRecord(text.RemoveDailyJobSheet, "Sheet ID: " + sheetID + "(" + itemCode + ")", updateTime, userID);
                        DT_JOB_DAILY_RECORD = dalProRecord.SelectActiveDailyJobRecordOnly();

                        LoadJobDailyRecord();
                    }
                }

            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
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
            //if(dgvRecordHistory.CurrentCell != null)
            //{
            //    int row = dgvRecordHistory.CurrentCell.RowIndex;

            //    if (row >= 0 && dgvRecordHistory.SelectedRows.Count > 0)
            //    {
            //        if (dailyRecordLoaded)
            //        {
                       
            //            int rowIndex = dgvRecordHistory.CurrentCell.RowIndex;

            //            if (rowIndex >= 0)
            //            {
            //                addingNewSheet = false;
            //                //AddNewSheetUI(true);
            //                //LoadExistingSheetData();
            //            }
            //        }
            //    }
            //}
          
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
            dgvRecordHistory.Columns[text.Header_TotalStockIn].InheritedStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
        }

        private void updateDiff()
        {
            string maxOutputQty = txtTotalMaxOutputQty.Text;
            string totalStockIn = txtQtyStockedIn.Text;

            int maxOutputQty_INT = int.TryParse(maxOutputQty, out maxOutputQty_INT) ? maxOutputQty_INT : 0;
            int stockInQty = int.TryParse(totalStockIn, out stockInQty) ? stockInQty : 0;
            txtPendingStockIn.Text = (maxOutputQty_INT - stockInQty).ToString();

            if (totalStockIn != maxOutputQty)
            {

                txtPendingStockIn.BackColor = Color.Pink;
            }
            else
            {
                if (!string.IsNullOrEmpty(maxOutputQty) && !string.IsNullOrEmpty(totalStockIn))
                {
                    txtPendingStockIn.BackColor = Color.LightGreen;
                }
                else
                {
                    txtPendingStockIn.BackColor = Color.White;
                }

            }
        }
        private void txtQtyStockedIn_TextChanged(object sender, EventArgs e)
        {
            updateDiff();
        }

        private void UpdateCheckedStatus(bool Checked, int checkBy, DateTime date)
        {
            
            DataGridView dgv = dgvActiveJobList;
            int rowIndex = dgv.CurrentCell.RowIndex;
            int JobNo = Convert.ToInt32(dgv.Rows[rowIndex].Cells[text.Header_JobNo].Value);
            string planStatus = dgv.Rows[rowIndex].Cells[text.Header_Status].Value.ToString() ;
            string itemCode = dgv.Rows[rowIndex].Cells[text.Header_ItemCode].Value.ToString();

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

                DT_JOB_DAILY_RECORD = dalProRecord.Select();
            }

        }
        private void cbFinalDataReview_CheckedChanged(object sender, EventArgs e)
        {
            if(!stopCheckedChange && loaded)
            {
                int userID = MainDashboard.USER_ID;
                int userPermission = dalUser.getPermissionLevel(userID);
                string userName = dalUser.getUsername(userID);
                DateTime dateNow = DateTime.Now;
                bool valid = userPermission >= MainDashboard.ACTION_LVL_FIVE || userID == 6;
                bool checkedStatus = cbFinalDataReview.Checked;


                if (checkedStatus)
                {
                    if (valid)
                    {
                        lblFinalDataReviewString.Text = string_CheckBy + userName + ", " + string_CheckDate + dateNow;
                        //update
                        UpdateCheckedStatus(checkedStatus, userID, dateNow);

                    }
                    else if (!stopCheckedChange)
                    {
                        stopCheckedChange = true;
                        cbFinalDataReview.Checked = false;
                        stopCheckedChange = false;
                    }
                }
                else
                {
                    if (valid)
                    {
                        lblFinalDataReviewString.Text = "";
                        //update
                        UpdateCheckedStatus(checkedStatus, userID, dateNow);
                    }
                    else
                    {
                        stopCheckedChange = true;
                        cbFinalDataReview.Checked = true;
                        stopCheckedChange = false;
                    }
                }
            }

            

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            ShowDailyRecordUI();
        }

        private void txtTotalMaxOutputQty_TextChanged(object sender, EventArgs e)
        {
            updateDiff();
        }
       
        private void ProducedVSTargetQty()
        {
        //    int totalProduced = int.TryParse(txtTotalMaxOutputQty.Text, out int x) ? x : 0;

        //    int targetQty = int.TryParse(txtTargetQty.Text, out x) ? x : 0;

        //    if (totalProduced >= targetQty)
        //    {
        //        txtTargetQty.BackColor = Color.LightGreen;
        //    }
        //    else
        //    {
        //        txtTargetQty.BackColor = Color.White;

        //    }
        }


        bool LOADING_JOB_RECORD = false;
        
        private void JobSelected()
        {
            if (loaded && !LOADING_JOB_RECORD)
            {
                if (!DATA_SAVED)
                {
                    DialogResult dialogResult = MessageBox.Show(text.Message_DataNotSaved, "Message",
                                                         MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.OK)
                    {
                        DATA_SAVED = true;

                        //load record history
                        LoadJobDailyRecord();
                        dgvRecordHistory.ClearSelection();
                    }
                }
                else
                {

                    //load record history
                    LoadJobDailyRecord();//989ms >392ms(21/3)
                    dgvRecordHistory.ClearSelection();

                }
            }
        }

        private void dgvActiveJobList_SelectionChanged(object sender, EventArgs e)
        {
            JobSelected();

            dgvRecordHistory.ClearSelection();
        }

        private int GetSelectedRowIndex(DataGridView dataGridView)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                return dataGridView.SelectedRows[0].Index;
            }
            else
            {
                return -1;  // Indicates no row is selected
            }
        }

        private void btnAddJobSheet_Click(object sender, EventArgs e)
        {
            int JobListSelectedIndex = GetSelectedRowIndex(dgvActiveJobList);

            if (JobListSelectedIndex == -1)
            {
                MessageBox.Show("Please select a job from the Active Job List.");
            }
            else
            {
                DataTable dt_JobList = (DataTable)dgvActiveJobList.DataSource;
                DataTable dt_JobHistoryRecord = (DataTable)dgvRecordHistory.DataSource;

                frmJobSheetRecord frm = new frmJobSheetRecord(dt_JobList, JobListSelectedIndex, dt_JobHistoryRecord);

                frm.StartPosition = FormStartPosition.CenterScreen;

                frm.ShowDialog();

                DT_JOB_DAILY_RECORD = dalProRecord.SelectActiveDailyJobRecordOnly();
                LoadJobDailyRecord();
            }

        }

        private void btnEditJobSheet_Click(object sender, EventArgs e)
        {
            int JobListSelectedIndex = GetSelectedRowIndex(dgvActiveJobList);
            int JobRecordSelectedIndex = GetSelectedRowIndex(dgvRecordHistory);

            if (JobRecordSelectedIndex == -1)
            {
                MessageBox.Show("Please select a job record to edit.");
            }
            else
            {
                DataTable dt_JobList = (DataTable)dgvActiveJobList.DataSource;
                DataTable dt_JobHistoryRecord = (DataTable)dgvRecordHistory.DataSource;

                frmJobSheetRecord frm = new frmJobSheetRecord(dt_JobList, JobListSelectedIndex, dt_JobHistoryRecord, JobRecordSelectedIndex);

                frm.StartPosition = FormStartPosition.CenterScreen;

                frm.ShowDialog();

                DT_JOB_DAILY_RECORD = dalProRecord.SelectActiveDailyJobRecordOnly();
                LoadJobDailyRecord();
            }
        }

        private void dgvActiveJobList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            loaded = false;

            int userID = MainDashboard.USER_ID;
            int userPermission = dalUser.getPermissionLevel(userID);

            //handle the row selection on right click

            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && (userPermission >= MainDashboard.ACTION_LVL_FIVE || userID == 6))// && userPermission >= MainDashboard.ACTION_LVL_THREE
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgvActiveJobList.CurrentCell = dgvActiveJobList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvActiveJobList.Rows[e.RowIndex].Selected = true;
                dgvActiveJobList.Focus();
                int rowIndex = dgvActiveJobList.CurrentCell.RowIndex;

                try
                {
                    my_menu.Items.Add(text_RemoveRecord).Name = text_RemoveRecord;
                    //my_menu.Items.Add(text_ChangeJobNo).Name = text_ChangeJobNo;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(dgvActiveJobList_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            loaded = true;
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvActiveJobList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvActiveJobList;
            dgv.SuspendLayout();

            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            int JobNo = Convert.ToInt32(dgv.Rows[rowIndex].Cells[text.Header_JobNo].Value);
            string itemCode = dgv.Rows[rowIndex].Cells[text.Header_ItemCode].Value.ToString();
            string itemName = dgv.Rows[rowIndex].Cells[text.Header_ItemName].Value.ToString();
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text_RemoveRecord))
            {
                if (MessageBox.Show("Are you sure you want to remove (Job No.: " + JobNo + ") " + itemName + " from this list?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    uPlan.plan_id = JobNo;
                    uPlan.recording = false;

                    if (dalPlan.RecordingUpdate(uPlan))
                    {
                        loaded = false;
                        MessageBox.Show("Item removed!");

                        DataTable dt = (DataTable)dgv.DataSource;

                        dt.Rows.RemoveAt(rowIndex);
                        dt.AcceptChanges();

                        dgvActiveJobList.ClearSelection();

                        //if (dt == null || dt.Rows.Count <= 0)
                        //{
                        //    lblClearList.Hide();
                        //    lblRemoveCompleted.Hide();
                        //}

                        loaded = true;
                        //LoadItemListData();
                    }
                }

            }

           

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void tlpMainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tlpMainPanel_MouseClick(object sender, MouseEventArgs e)
        {
            dgvRecordHistory.ClearSelection();
        }

        private void tableLayoutPanel3_MouseClick(object sender, MouseEventArgs e)
        {
            dgvRecordHistory.ClearSelection();
        }

        private void tableLayoutPanel15_MouseClick(object sender, MouseEventArgs e)
        {
            dgvRecordHistory.ClearSelection();
        }

        private void tlpButton_MouseClick(object sender, MouseEventArgs e)
        {
            dgvRecordHistory.ClearSelection();
        }

        private void txtQtyStockedIn_TextChanged_1(object sender, EventArgs e)
        {
            int maxQty = int.TryParse(txtTotalMaxOutputQty.Text, out maxQty) ? maxQty : 0;
            int stockedIn = int.TryParse(txtQtyStockedIn.Text, out stockedIn) ? stockedIn : 0;

            int pendingStockIn = maxQty - stockedIn;

            txtPendingStockIn.Text = pendingStockIn.ToString();
        }

        private void txtTotalMaxOutputQty_TextChanged_1(object sender, EventArgs e)
        {
            int maxQty = int.TryParse(txtTotalMaxOutputQty.Text, out maxQty) ? maxQty : 0;
            int stockedIn = int.TryParse(txtQtyStockedIn.Text, out stockedIn) ? stockedIn : 0;

            int pendingStockIn = maxQty - stockedIn;

            txtPendingStockIn.Text = pendingStockIn.ToString();
        }

        private void dgvRecordHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int rowindex = e.RowIndex;
            int colindex = e.ColumnIndex;

            string colName = dgvRecordHistory.Columns[colindex].Name;

            if(colName == text.Header_StockIn_Remark)
            {
                int trfQty = int.TryParse(dgvRecordHistory.Rows[rowindex].Cells[colindex].Value.ToString(), out trfQty) ? trfQty : 0;

                if (trfQty > 0)
                {
                    dgvRecordHistory.Rows[rowindex].Cells[text.Header_StockIn_Remark].Style.BackColor = Color.LightGreen;
                }
                else
                {
                    dgvRecordHistory.Rows[rowindex].Cells[text.Header_StockIn_Remark].Style.BackColor = Color.Pink;
                }
            }
           
        }
    }
}
