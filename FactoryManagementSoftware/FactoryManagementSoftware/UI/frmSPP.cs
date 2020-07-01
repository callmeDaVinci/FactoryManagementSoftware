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

namespace FactoryManagementSoftware.UI
{
    public partial class frmSPP : Form
    {
        public frmSPP()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvStockAlert, true);
            _instance = this;
        }

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        facDAL dalFac = new facDAL();
        pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        SPPDataDAL dalSPP = new SPPDataDAL();
        SPPDataBLL uSpp = new SPPDataBLL();
        historyDAL dalHistory = new historyDAL();
        historyBLL uHistory = new historyBLL();
        planningDAL dalPlan = new planningDAL();

        joinDAL dalJoin = new joinDAL();

        Tool tool = new Tool();
        Text text = new Text();

        private static frmSPP _instance;

        readonly string header_Index = "#";
        readonly string header_Status = "STATUS";
        readonly string header_ItemCode = "CODE";
        readonly string header_ItemName = "NAME";
        readonly string header_Stock = "STOCK";
        string header_BalAfter1 = "BAL. AFTER ";
        string header_BalAfter2 = "BAL. AFTER ";
        string header_BalAfter3 = "BAL. AFTER ";
        readonly string header_Produced = "PRODUCED";
        readonly string header_ProduceTarget = "PRODUCE TARGET";
        readonly string header_ProductionString = "PRODUCTION";

        readonly string Type_Product = "PRODUCT";
        readonly string Type_Part = "PART";

        private bool Loaded = false;
        public void LoadTypeCMB(ComboBox cmb)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("TYPE");

            dt.Rows.Add(Type_Part);
            dt.Rows.Add(Type_Product);
            
            cmb.DataSource = dt;
            cmb.DisplayMember = "TYPE";
        }

        private void ResetBalanceHeaderName()
        {
            header_BalAfter1 = "BAL. AFTER ";
            header_BalAfter2 = "BAL. AFTER ";
            header_BalAfter3 = "BAL. AFTER ";
        }

        private DataTable NewStockAlertTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));

            dt.Columns.Add(header_Stock, typeof(int));

            ResetBalanceHeaderName();
            int Month_1 = DateTime.Now.Month;
            int Month_2 = tool.getNextMonth(Month_1);
            int Month_3 = tool.getNextMonth(Month_2);

            //header_BalAfter1 = header_BalAfter1 + new DateTimeFormatInfo().GetAbbreviatedMonthName(Month_1).ToUpper().ToString();
            //header_BalAfter2 = header_BalAfter2 + new DateTimeFormatInfo().GetAbbreviatedMonthName(Month_2).ToUpper().ToString();
            //header_BalAfter3 = header_BalAfter3 + new DateTimeFormatInfo().GetAbbreviatedMonthName(Month_3).ToUpper().ToString();

            header_BalAfter1 = header_BalAfter1 + Month_1.ToString();
            header_BalAfter2 = header_BalAfter2 + Month_2.ToString();
            header_BalAfter3 = header_BalAfter3 + Month_3.ToString();


            dt.Columns.Add(header_BalAfter1, typeof(int));
            dt.Columns.Add(header_BalAfter2, typeof(int));
            dt.Columns.Add(header_BalAfter3, typeof(int));

            dt.Columns.Add(header_Produced, typeof(int));
            dt.Columns.Add(header_ProduceTarget, typeof(int));
            dt.Columns.Add(header_ProductionString, typeof(string));

            dt.Columns.Add(header_Status, typeof(string));
            return dt;
        }


        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            //dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dgv.Columns[header_Produced].Visible = false;
            dgv.Columns[header_ProduceTarget].Visible = false;
            dgv.Columns[header_Status].Visible = false;

            dgv.Columns[header_ItemCode].DefaultCellStyle.ForeColor = Color.Gray;
            dgv.Columns[header_ItemCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            dgv.Columns[header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_BalAfter1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_BalAfter2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_BalAfter3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_ProduceTarget].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_Produced].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_ProductionString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_PODate].DefaultCellStyle.ForeColor = Color.Gray;
            ////dgv.Columns[header_DONo].DefaultCellStyle.ForeColor = Color.Gray;
            //dgv.Columns[header_POCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_PONoString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[header_PODate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[header_Progress].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_POCode].Visible = false;
            //dgv.Columns[header_CustomerCode].Visible = false;



        }

        private void frmSPP_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.SPPFormOpen = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSPPInventory frm = new frmSPPInventory
            {
                StartPosition = FormStartPosition.CenterScreen
            };

           
            frm.ShowDialog();
            
        }

        private string GetChildCode(DataTable dt, string parentCode)
        {
            foreach (DataRow row in dt.Rows)
            {
                if(parentCode == row[dalJoin.ParentCode].ToString())
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

        private DataTable LoadMatPartList(DataTable dt_Product)
        {
            DataTable dt_MatPart = NewStockAlertTable();
            DataTable dt_Plan = dalPlan.Select();

            int index = 1;
            DataTable dtJoin = dalJoin.Select();

            foreach (DataRow row in dt_Product.Rows)
            {
                string parentCode = GetChildCode(dtJoin, row[header_ItemCode].ToString());

                int stillNeed_1 = int.TryParse(row[header_BalAfter1].ToString(), out stillNeed_1) ? stillNeed_1 : 0;
                int stillNeed_2 = int.TryParse(row[header_BalAfter2].ToString(), out stillNeed_2) ? stillNeed_2 : 0;
                int stillNeed_3 = int.TryParse(row[header_BalAfter3].ToString(), out stillNeed_3) ? stillNeed_3 : 0;

                stillNeed_1 = stillNeed_1 > 0 ? 0 : stillNeed_1 * -1;
                stillNeed_2 = stillNeed_2 > 0 ? 0 : stillNeed_2 * -1;
                stillNeed_3 = stillNeed_3 > 0 ? 0 : stillNeed_3 * -1;

                foreach(DataRow rowJoin in dtJoin.Rows)
                {
                    if(rowJoin[dalJoin.ParentCode].ToString() == parentCode)
                    {
                        string childCode = rowJoin[dalJoin.ChildCode].ToString();
                        string childName = rowJoin[dalJoin.ChildName].ToString();

                        int readyStock = int.TryParse(rowJoin[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;
                        int joinQty = int.TryParse(rowJoin[dalJoin.JoinQty].ToString(), out joinQty) ? joinQty : 0;
                        int joinMax = int.TryParse(rowJoin[dalJoin.JoinMax].ToString(), out joinMax) ? joinMax : 0;
                        int joinMin = int.TryParse(rowJoin[dalJoin.JoinMin].ToString(), out joinMin) ? joinMin : 0;

                        int child_StillNeed_1 = 0;
                        int child_StillNeed_2 = 0;
                        int child_StillNeed_3 = 0;

                        joinMax = joinMax <= 0 ? 1 : joinMax;
                        joinMin = joinMin <= 0 ? 1 : joinMin;

                        child_StillNeed_1 = stillNeed_1 / joinMax * joinQty;

                        child_StillNeed_1 = stillNeed_1 % joinMax >= joinMin? child_StillNeed_1 + joinQty : child_StillNeed_1;

                        child_StillNeed_2 = stillNeed_2 / joinMax * joinQty;

                        child_StillNeed_2 = stillNeed_2 % joinMax >= joinMin ? child_StillNeed_2 + joinQty : child_StillNeed_2;

                        child_StillNeed_3 = stillNeed_3 / joinMax * joinQty;

                        child_StillNeed_3 = stillNeed_3 % joinMax >= joinMin ? child_StillNeed_3 + joinQty : child_StillNeed_3;

                        int bal_1 = readyStock - child_StillNeed_1;
                        int bal_2 = readyStock - child_StillNeed_2;
                        int bal_3 = readyStock - child_StillNeed_3;

                        bool childInserted = false;

                        foreach (DataRow mat_row in dt_MatPart.Rows)
                        {
                            if (childCode == mat_row[header_ItemCode].ToString())
                            {
                                childInserted = true;

                                int previousBal_1 = int.TryParse(mat_row[header_BalAfter1].ToString(), out previousBal_1) ? previousBal_1 : 0;
                                int previousBal_2 = int.TryParse(mat_row[header_BalAfter2].ToString(), out previousBal_2) ? previousBal_2 : 0;
                                int previousBal_3 = int.TryParse(mat_row[header_BalAfter3].ToString(), out previousBal_3) ? previousBal_3 : 0;

                                bal_1 = previousBal_1 - child_StillNeed_1;
                                bal_2 = previousBal_2 - child_StillNeed_2;
                                bal_3 = previousBal_3 - child_StillNeed_3;

                                mat_row[header_BalAfter1] = bal_1;
                                mat_row[header_BalAfter2] = bal_2;
                                mat_row[header_BalAfter3] = bal_3;

                                break;
                            }
                        }

                        if (!childInserted)//!childInserted
                        {
                            var productionQty = GetProductionTargetAndProducedQty(dt_Plan, childCode);

                            int targetQty = productionQty.Item1;
                            int producedQty = productionQty.Item2;
                            string planStatus = productionQty.Item3;

                            DataRow alert_row = dt_MatPart.NewRow();

                            alert_row[header_Index] = index;
                            alert_row[header_ItemCode] = childCode;
                            alert_row[header_ItemName] = childName;
                            alert_row[header_Stock] = readyStock;
                            alert_row[header_BalAfter1] = bal_1;
                            alert_row[header_BalAfter2] = bal_2;
                            alert_row[header_BalAfter3] = bal_3;

                            alert_row[header_ProduceTarget] = targetQty;
                            alert_row[header_Produced] = producedQty;
                            alert_row[header_Status] = planStatus;

                            if (targetQty > 0)
                            {
                                alert_row[header_ProductionString] = producedQty + "/" + targetQty;
                            }
                            

                            dt_MatPart.Rows.Add(alert_row);
                            index++;
                        }
                       

                    }
                }
            }

            return dt_MatPart;
        }

        private Tuple<int,int,string> GetProductionTargetAndProducedQty(DataTable dt_Plan, string itemCode)
        {
            int targetQty = 0;
            int producedQty = 0;
            string running = "";
            foreach(DataRow row in dt_Plan.Rows)
            {
                string planStatus = row[dalPlan.planStatus].ToString();
                string planItem = row[dalPlan.partCode].ToString();

                if (planItem == itemCode && (planStatus == text.planning_status_running || planStatus == text.planning_status_pending))
                {
                    targetQty += int.TryParse(row[dalPlan.targetQty].ToString(), out int i) ? i : 0;
                    producedQty += int.TryParse(row[dalPlan.planProduced].ToString(), out i) ? i : 0;

                    if(planStatus == text.planning_status_running)
                    {
                        running = planStatus;
                    }
                }
            }

            return Tuple.Create(targetQty, producedQty, running);
        }

        private void LoadStockAlert()
        {
            Cursor = Cursors.WaitCursor;

            #region indicate start and end date

            string start;
            string end;
            int yearStart = -1, yearEnd = -1;

            int monthStart = DateTime.Now.Month;
            int monthEnd = monthStart + 2;

            string Month_1 = monthStart.ToString();
            string Month_2 = (monthStart + 1).ToString("MMMM");
            string Month_3 = monthEnd.ToString("MMMM");

            yearStart = DateTime.Now.Year;
            yearEnd = monthEnd < monthStart ? yearStart + 1 : yearStart;

            start = new DateTime(yearStart, monthStart, 1).ToString("yyyy/MM/dd");
            end = new DateTime(yearEnd, monthEnd, DateTime.DaysInMonth(yearEnd, monthEnd)).ToString("yyyy/MM/dd");

            #endregion

            #region Load data from database

            DataTable dt = NewStockAlertTable();

            DataTable dt_Product_StockAlert = NewStockAlertTable();

            string itemCust = text.SPP_BrandName;
            DataTable dt_Product = dalItemCust.SPPCustSearch(itemCust);
            DataTable dt_TrfHist = dalTrfHist.SPPItemToCustomerSearch(start, end, itemCust);
            DataTable dt_Item = dalItem.Select();
            DataTable dt_SppCustomer = dalSPP.CustomerWithoutRemovedDataSelect();
            

            //int index = 1;
            #endregion

            foreach (DataRow row in dt_Product.Rows)
            {
                string itemCode = row[dalSPP.ItemCode].ToString();
                string itemName = row[dalSPP.ItemName].ToString();
                int readyStock = int.TryParse(row[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;
                int qtyPerPacket = int.TryParse(row[dalSPP.QtyPerPacket].ToString(), out qtyPerPacket) ? qtyPerPacket : 0;
                int qtyPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                int maxLevel = int.TryParse(row[dalSPP.MaxLevel].ToString(), out maxLevel) ? maxLevel : 0;

                var deliveredQty = GetDeliveredQty(dt_SppCustomer, dt_TrfHist, monthStart, monthStart+1, monthStart+2, itemCode);

                int deliveredQty_1 = deliveredQty.Item1;
                int deliveredQty_2 = deliveredQty.Item2;
                int deliveredQty_3 = deliveredQty.Item3;

                int stillNeed_1 = maxLevel * qtyPerBag - deliveredQty_1 > 0 ? maxLevel * qtyPerBag - deliveredQty_1 : 0;
                int stillNeed_2 = maxLevel * qtyPerBag - deliveredQty_2 > 0 ? maxLevel * qtyPerBag - deliveredQty_2 : 0;
                int stillNeed_3 = maxLevel * qtyPerBag - deliveredQty_3 > 0 ? maxLevel * qtyPerBag - deliveredQty_3 : 0;

                int bal_1 = readyStock - stillNeed_1;
                int bal_2 = bal_1 - stillNeed_2;
                int bal_3 = bal_2 - stillNeed_3;

                DataRow alert_row = dt_Product_StockAlert.NewRow();

                int divideBy = 1;

                if(cbInBagUnit.Checked)
                {
                    divideBy = qtyPerBag;
                }

                //alert_row[header_Index] = index;
                alert_row[header_ItemCode] = itemCode;
                alert_row[header_ItemName] = itemName;
                alert_row[header_Stock] = readyStock / divideBy;
                alert_row[header_BalAfter1] = bal_1 / divideBy;
                alert_row[header_BalAfter2] = bal_2 / divideBy;
                alert_row[header_BalAfter3] = bal_3 / divideBy;

                dt_Product_StockAlert.Rows.Add(alert_row);
                //index++;
            }

            string selectedType = cmbType.Text;

            if (selectedType == Type_Part)
            {
                dt = LoadMatPartList(dt_Product_StockAlert);
            }
            else
            {
                dt = dt_Product_StockAlert;
            }

            dt.DefaultView.Sort = header_BalAfter3 + " ASC";
            dt = dt.DefaultView.ToTable();
            dt.AcceptChanges();

            RearrangeIndex(dt);

            dgvStockAlert.DataSource = dt;
           
            DgvUIEdit(dgvStockAlert);

            dgvStockAlert.ClearSelection();

            Cursor = Cursors.Arrow;
        }
        
        private DataTable RearrangeIndex(DataTable dt)
        {
            int index = 1;
            
            foreach(DataRow row in dt.Rows)
            {
                row[header_Index] = index;
                index++;
            }

            return dt;
        }
        private Tuple<int, int, int> GetDeliveredQty(DataTable dt_SppCustomer, DataTable dt_Trf, int month_1, int month_2, int month_3, string itemCode)
        {
            int Out_1 = 0, Out_2 = 0, Out_3 = 0;
            dt_Trf.DefaultView.Sort = dalItem.ItemCode + " ASC";
            dt_Trf = dt_Trf.DefaultView.ToTable();
            dt_Trf.AcceptChanges();

           foreach(DataRow row in dt_Trf.Rows)
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
                        string fullName = rowSPP[dalSPP.FullName].ToString();
                        string shortName = rowSPP[dalSPP.ShortName].ToString();

                        if (trfTo == fullName || trfTo == shortName || trfTo == "SPP" || trfTo == "OTHER")
                        {
                            listedCustomer = true;
                            break;
                        }
                    }

                    if (listedCustomer)
                    {
                        if(trfMonth == month_1)
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

            DataTable dt_SppCustomer = dalSPP.CustomerWithoutRemovedDataSelect();

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

        private void frmSPP_Load(object sender, EventArgs e)
        {
            LoadTypeCMB(cmbType);

            RefreshPage();

            Loaded = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmSPPDataSetting frm = new frmSPPDataSetting
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Control ctrl = (Control)sender;
            //ctrl.BackColor = Color.Yellow;
            frmSPPCalculation frm = new frmSPPCalculation
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void OpenPOList(object sender, EventArgs e)
        {
            frmSPPPOList frm = new frmSPPPOList
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmSPPNewPO frm = new frmSPPNewPO
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
        }

        private void LoadPendingSummary()
        {
            DataTable dt_DOList = dalSPP.DOWithInfoSelect();

            var result = tool.GetPendingPOQty(dt_DOList);
            lblPendingPO.Text = result.Item1.ToString();
            lblPendingPOCust.Text = result.Item2.ToString();

            var result2 = tool.GetPendingDOQty(dt_DOList);
            lblPendingDO.Text = result2.Item1.ToString();
            lblPendingDOCust.Text = result2.Item2.ToString();
            lblDOBagQty.Text = result2.Item3.ToString();
        }

        private void RefreshPage()
        {
            frmLoading.ShowLoadingScreen();
           
            LoadStockAlert();

            LoadPendingSummary();

            frmLoading.CloseForm();
        }


        public static void Reload()
        {
            _instance.RefreshPage();
        }

        private void OpenDOList(object sender, EventArgs e)
        {
            frmSPPDOList frm = new frmSPPDOList
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();
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

        private void button12_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WorkInProgressMessage();
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

            if (dgv.Columns[col].Name == header_BalAfter1 || dgv.Columns[col].Name == header_BalAfter2 || dgv.Columns[col].Name == header_BalAfter3)
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
            else if(dgv.Columns[col].Name == header_ProductionString)
            {
                string status = dgv.Rows[row].Cells[header_Status].Value.ToString();

                if (status == text.planning_status_running)
                {
                    dgv.Rows[row].Cells[header_ProductionString].Style.ForeColor = Color.FromArgb(52, 139, 209);
                }
                else
                {
                    dgv.Rows[row].Cells[header_ProductionString].Style.ForeColor = Color.Black;
                }
            }

            dgv.ResumeLayout();

     
        }

        private void cbInPcsUnit_CheckedChanged(object sender, EventArgs e)
        {
            if(Loaded)
            {
                if (cbInPcsUnit.Checked)
                {
                    if (cmbType.Text == Type_Product)
                        cbInBagUnit.Checked = false;

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
            if(Loaded)
            {
                if (cbInBagUnit.Checked)
                {
                    
                    if (cmbType.Text == Type_Product)
                    {
                        cbInPcsUnit.Checked = false;
                        LoadStockAlert();
                    }
                    else
                    {
                        cbInBagUnit.Checked = false;
                        cbInPcsUnit.Checked = true;
                    }

                }


                //dgvStockAlert.DataSource = null;
            }
            
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Loaded)
            {
                dgvStockAlert.DataSource = null;

                if (cmbType.Text == Type_Product)
                {
                    cbInBagUnit.Visible = true;
                    cbInPcsUnit.Enabled = true;
                }
                else
                {
                    cbInPcsUnit.Checked = true;
                    cbInBagUnit.Checked = false;
                    cbInPcsUnit.Enabled = false;
                    cbInBagUnit.Visible = false;
                }
                LoadStockAlert();
            }
           
        }

        private void btnLOAD_Click(object sender, EventArgs e)
        {
            LoadStockAlert();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            LoadStockAlert();
        }
    }
}
