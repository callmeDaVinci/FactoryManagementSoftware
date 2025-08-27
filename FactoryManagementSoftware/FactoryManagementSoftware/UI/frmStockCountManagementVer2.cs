using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using Font = System.Drawing.Font;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Tls;
using System.Xml.Linq;

namespace FactoryManagementSoftware.UI
{
    public partial class frmStockCountManagementVer2 : Form
    {
       
        #region Default Settings

        Tool tool = new Tool();
        Text text = new Text();
        facDAL dalFac = new facDAL();
        stockCountListDAL dalStockCountList = new stockCountListDAL();
        stockCountListBLL uStockCountList = new stockCountListBLL();

        stockCountListItemDAL dalStockCountListItem = new stockCountListItemDAL();

        DataTable DT_STOCK_COUNT_LIST;

        private bool INITIAL_SETTING_LOADED = false;

        private bool DATA_SAVED = true;

        public frmStockCountManagementVer2()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvStockCountList, true);

            InitialSetting();
            dtpStockCountDate.Value = DateTime.Now;
        }
      
        private void InitialSetting()
        {

            LoadStockCountListData();
            InitialStockCountListComboBox(DT_STOCK_COUNT_LIST);
         
        }

        private void InitialStockCountListComboBox(DataTable dt)
        {
            cmbCustomer.DataSource = null;

            if(dt?.Rows.Count > 0)
            {
                cmbCustomer.DataSource = dt;
                cmbCustomer.DisplayMember = dalStockCountList.ListDescription;
                cmbCustomer.ValueMember = dalStockCountList.TblCode;
                cmbCustomer.SelectedIndex = -1;
            }
        }

        private void LoadStockCountListData()
        {
            DT_STOCK_COUNT_LIST = dalStockCountList.SelectAll();
        }

      
        private void frmStockCountManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved data. Leave without saving? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                }
                else
                {
                    e.Cancel = true;
                }

            }
        }

        #endregion


        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            if (dgv == dgvStockCountList)
            {
                //dgv.Columns[header_PODate].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                //dgv.Columns[header_PONo].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);

                dgv.Columns[text.Header_StockLocation].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_SystemStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_StockCount].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_CountUnit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_UnitConversionRate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_TotalQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_Difference].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_TableCode].Visible = false;
                dgv.Columns[text.Header_StockLocation_TblCode].Visible = false;
                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
                dgv.Columns[text.Header_InFrom_TblCode].Visible = false;
                dgv.Columns[text.Header_OutTo_TblCode].Visible = false;

                dgv.Columns[text.Header_StockCount].DefaultCellStyle.BackColor = SystemColors.Info;
                dgv.Columns[text.Header_Difference].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                dgv.Columns[text.Header_Difference].DefaultCellStyle.Font = new Font(dgvStockCountList.DefaultCellStyle.Font, FontStyle.Bold);

                // Make all columns read-only initially
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    column.ReadOnly = true;
                }

                // Enable editing for specific columns by name
                dgv.Columns[text.Header_StockCount].ReadOnly = false;
                dgv.Columns[text.Header_Selection].ReadOnly = false;


            }


        }

        private void cmbStockCountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void lblEditList_Click(object sender, EventArgs e)
        {
            // Get the DataRowView for the selected item
            DataRowView selectedRow = (DataRowView)cmbCustomer.SelectedItem;

            int tblCode = int.TryParse(selectedRow[dalStockCountList.TblCode].ToString(), out int i) ? i : -1;

            frmStockCountListSetting frm = new frmStockCountListSetting(tblCode);

            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.ShowDialog();
            INITIAL_SETTING_LOADED = false;

            LoadStockCountListData();
            InitialStockCountListComboBox(DT_STOCK_COUNT_LIST);

            INITIAL_SETTING_LOADED = true;

        }

        trfCatDAL daltrfCat = new trfCatDAL();
        itemDAL dalItem = new itemDAL();
        facStockDAL dalFacStock = new facStockDAL();

        DataTable DT_LOCATION_CATEGORY;
        DataTable DT_ITEM;
        DataTable DT_FAC_STOCK;

        private void LoadLocationCategoryData()
        {
            DataTable dtlocationCat = daltrfCat.Select();
            dtlocationCat.DefaultView.Sort = "trf_cat_name ASC";
            DT_LOCATION_CATEGORY = dtlocationCat.DefaultView.ToTable();

        }

        private void LoadItemData()
        {
            DT_ITEM = dalItem.Select();
        }

        private void LoadFacStockData()
        {
            DT_FAC_STOCK = dalFacStock.Select();
        }


        private void btnAddItem_Click(object sender, EventArgs e)
        {
               
        }

        private void frmStockCountManagement_Shown(object sender, EventArgs e)
        {
            INITIAL_SETTING_LOADED = true;
        }

        private void dgvStockCountList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Temporarily store the current row index
            int selectedRowIndex = e.RowIndex;

            btnEdit.Visible = selectedRowIndex >= 0;

            // Change the selection mode based on the clicked column
            if (dgvStockCountList.Columns[e.ColumnIndex].Name == text.Header_ItemDescription)
            {
                dgvStockCountList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            else
            {
                dgvStockCountList.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }


            // Check if the row index is valid to avoid selecting header row
            if (selectedRowIndex >= 0)
            {
                // Clear existing selection to ensure a clean state
                dgvStockCountList.ClearSelection();

                // Check the selection mode to determine how to reselect
                if (dgvStockCountList.SelectionMode == DataGridViewSelectionMode.FullRowSelect)
                {
                    // Select the entire row
                    dgvStockCountList.Rows[selectedRowIndex].Selected = true;
                }
                else
                {
                    // Select the specific cell
                    dgvStockCountList.Rows[selectedRowIndex].Cells[e.ColumnIndex].Selected = true;
                }
            }
        }

        private void dgvStockCountList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);

            // Check if the current column is "columnA"
            if (dgvStockCountList.CurrentCell.ColumnIndex == dgvStockCountList.Columns[text.Header_StockCount].Index)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }
        }

        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow number, backspace, and single decimal point
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void dgvStockCountList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the edited column is "Stock Count"
            if (dgvStockCountList.Columns[e.ColumnIndex].Name == text.Header_StockCount)
            {
                double stockCount = 0;
                double unitConversionRate = 1; // Default value
                double totalQty = 0;

                // Try to get the stock count value
                if (!double.TryParse(dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_StockCount].Value?.ToString(), out stockCount))
                {
                    // Invalid stock count, clear the Total Qty cell
                    dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_TotalQty].Value = DBNull.Value;
                    dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Selection].Value = false;
                    return;
                }

                // Try to get the unit conversion rate, use default if invalid
                var conversionRateCell = dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_UnitConversionRate].Value;
                if (conversionRateCell != null && !double.TryParse(conversionRateCell.ToString(), out unitConversionRate))
                {
                    unitConversionRate = 1; // Use default if conversion rate is not valid
                }

                // Calculate total quantity
                totalQty = stockCount * unitConversionRate;

               
                // Update the Total Qty cell
                dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_TotalQty].Value = totalQty;
                dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Selection].Value = true;

                double systemStock = double.TryParse(dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_SystemStock].Value.ToString(), out systemStock) ? systemStock : 0;

                double diff = Math.Round(totalQty - systemStock, 2);
                //double diff = totalQty - systemStock;
                string actionPreview = "";
                string unit = dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Unit].Value.ToString();

                var outToCell = dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_OutTo];
                var inFromCell = dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_InFrom];

                inFromCell.Style.BackColor = dgvStockCountList.DefaultCellStyle.BackColor; // Reset to default if previously set to red
                outToCell.Style.BackColor = dgvStockCountList.DefaultCellStyle.BackColor; // Reset to default if previously set to red
                // Update action preview based on the difference

                dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Selection].Value = false;
                dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Difference].Style.ForeColor = Color.Black;

                if (diff > 0)
                {
                    // Retrieve "In From" location
                    
                    string inFrom = inFromCell.Value?.ToString() ?? "";
                    dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Selection].Value = true;

                    if (string.IsNullOrWhiteSpace(inFrom))
                    {
                        inFromCell.Style.BackColor = Color.Red;
                        inFrom = "[Missing Data]";
                        dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Selection].Value = false;

                    }


                    actionPreview = "Stock in " + Math.Abs(diff) + " " + unit + " from " + inFrom;
                    dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Difference].Style.ForeColor = Color.Green;

                }
                else if(diff < 0)
                {
                    // Retrieve "Out To" location
                   
                    string outTo = outToCell.Value?.ToString() ?? "";
                    dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Selection].Value = true;
                    if (string.IsNullOrWhiteSpace(outTo))
                    {
                        outToCell.Style.BackColor = Color.Red;
                        outTo = "[Missing Data]";
                        dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Selection].Value = false;
                    }

                    actionPreview = "Stock out " + Math.Abs(diff) + " " + unit + " to " + outTo;
                    dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Difference].Style.ForeColor = Color.Red;
                    

                }


                dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_Difference].Value = diff;

                dgvStockCountList.Rows[e.RowIndex].Cells[text.Header_ActionPreview].Value = actionPreview;
            }
        }

        private bool STOCK_UPDATED = false;
        private void btnStockUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to process to Stock Update ?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                DataTable dt = (DataTable)dgvStockCountList.DataSource;

                frmInOutEdit frm = new frmInOutEdit(dt, dtpStockCountDate.Value.Date, true);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();//Item Edit

                STOCK_UPDATED = frmInOutEdit.TrfSuccess;

                if(STOCK_UPDATED)
                {
                    dgvStockCountList.DataSource = null;

                    LoadFacStockData();
                    //LoadStockCountListItem();
                }
            }


        }

        private void frmStockCountManagementVer2_Load(object sender, EventArgs e)
        {

        }

        private string HEADER_READY_STOCK = "";
        private DataTable NewForecastReportTable()
        {
           
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_TableCode, typeof(int));
            dt.Columns.Add(text.Header_Index, typeof(int));

            if (cmbCustomer.Text.Equals(text.Cmb_All))
            {
                dt.Columns.Add(text.Header_Customer, typeof(string));
            }
            
            dt.Columns.Add(text.Header_Category, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));

            dt.Columns.Add(text.Header_Qty_per_Packing, typeof(decimal));

            dt.Columns.Add(text.Header_StdPacking_Info, typeof(string));

            HEADER_READY_STOCK = text.Header_SystemStock;

            dt.Columns.Add(HEADER_READY_STOCK, typeof(decimal));

            dt.Columns.Add(text.Header_StockCount, typeof(int));

            dt.Columns.Add(text.Header_Balance, typeof(decimal));
            dt.Columns.Add(text.Header_TotalQty, typeof(decimal));


            dt.Columns.Add(text.Header_Difference, typeof(double));
            dt.Columns.Add(text.Header_Unit, typeof(string));


            dt.Columns.Add(text.Header_ActionPreview, typeof(string));

            dt.Columns.Add(text.Header_Selection, typeof(bool));


            return dt;
        }


        //private DataTable FullDetailForecastData()
        //{
        //    #region Setting

        //    Cursor = Cursors.WaitCursor;
           
        //    DataTable dt_Data = NewForecastReportTable();

        //    DataRow dt_Row;

        //    int index = 1;

        //    #endregion

        //    #region Load Data

        //    #region load single part
        //    //308ms 
        //    foreach (DataRow row in DT_ITEM_CUST.Rows)
        //    {

        //        string itemSearch = row[dalItem.ItemCode].ToString();

        //        if (itemSearch == "A0LK160R0")
        //        {
        //            var checkpoint = 1;
        //        }

        //        bool gotNotPackagingChild = bool.TryParse(row[text.Header_GotNotPackagingChild].ToString(), out bool GotChild) ? GotChild : false;

        //        if (!gotNotPackagingChild)
        //        {
        //            uData.part_code = row[dalItem.ItemCode].ToString();
        //            uData.customer_name = row[dalItemCust.CustName].ToString();
        //            uData.cust_id = row[dalItemCust.CustID].ToString();

        //            var forecastData = GetCustomerThreeMonthsForecastQty(dt_ItemForecast, uData.cust_id, uData.part_code, 1, 2, 3);
        //            uData.forecast1 = forecastData.Item1;
        //            uData.forecast2 = forecastData.Item2;
        //            uData.forecast3 = forecastData.Item3;
        //            var estimate = EstimateNextOrderAndCheckIfStillActive(DT_PMMA_DATE, dt_TrfHist, uData.part_code, uData.customer_name);

        //            uData.estimate = estimate.Item1;
        //            bool NonActiveItem = estimate.Item2;
        //            uData.deliveredOut = (float)estimate.Item3;
        //            if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && cbRemoveNoOrderItem.Checked) || uData.deliveredOut > 0 || !(NonActiveItem && (cbRemoveNoDeliveredItem.Checked || cbRemoveNoOrderItem.Checked)))
        //            {
        //                uData.item_remark = row[dalItem.ItemRemark].ToString();
        //                int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
        //                int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);

        //                uData.index = index;
        //                uData.part_name = row[dalItem.ItemName].ToString();
        //                uData.color_mat = row[dalItem.ItemMBatch].ToString();
        //                uData.color = row[dalItem.ItemColor].ToString();
        //                uData.raw_mat = row[dalItem.ItemMaterial].ToString();
        //                uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);

        //                //uData.pw_per_shot = (float) Decimal.Round((decimal) uData.pw_per_shot, 2);

        //                uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);

        //                //uData.rw_per_shot = (float) Decimal.Round((decimal) uData.rw_per_shot, 2);

        //                uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
        //                uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
        //                uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

        //                decimal SemenyihStock = deductSemenyihStock(uData.part_code);


        //                //uData.ready_stock = uData.ready_stock - (float) SemenyihStock;
        //                uData.ready_stock = uData.ready_stock;


        //                if (myconnstrng == text.DB_Semenyih)
        //                {
        //                    uData.ready_stock = (float)SemenyihStock;
        //                }

        //                var result = GetProduceQty(uData.part_code, DT_MACHINE_SCHEDULE);
        //                uData.toProduce = result.Item1;
        //                uData.Produced = result.Item2;
        //                uData.prodinfo = result.Item3;

        //                uData.purchaseinfo = loadOrderRecord(DT_PURCAHSE_ORDER_RECORD, uData.part_code);

        //                #region Balance Calculation

        //                uData.outStd = uData.forecast1 - uData.deliveredOut;

        //                if (uData.forecast1 == -1)
        //                {
        //                    uData.outStd = 0;
        //                }
        //                else if (uData.forecast1 > -1)
        //                {
        //                    uData.outStd = uData.forecast1 - uData.deliveredOut;
        //                }
        //                uData.bal1 = uData.ready_stock;

        //                if (uData.outStd >= 0)
        //                {
        //                    uData.bal1 = uData.ready_stock - uData.outStd;
        //                }
        //                uData.bal2 = uData.bal1 - uData.forecast2;

        //                if (uData.forecast2 == -1)
        //                {
        //                    uData.bal2 = uData.bal1 - (cbDeductEstimate.Checked ? uData.estimate : 0);

        //                }
        //                else if (uData.forecast2 > -1)
        //                {
        //                    uData.bal2 = uData.bal1 - uData.forecast2;
        //                }
        //                uData.bal3 = uData.bal2 - uData.forecast3;

        //                if (uData.forecast3 == -1)
        //                {
        //                    uData.bal3 = uData.bal2 - (cbDeductEstimate.Checked ? uData.estimate : 0);

        //                }
        //                else if (uData.forecast3 > -1)
        //                {
        //                    uData.bal3 = uData.bal2 - uData.forecast3;
        //                }

        //                #endregion

        //                if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
        //                {
        //                    uData.color_mat += " (" + uData.color + ")";
        //                }
        //                dt_Row = dt_Data.NewRow();

        //                if (uData.toProduce > 0)
        //                {
        //                    dt_Row[headerToProduce] = uData.toProduce;
        //                }

        //                if (uData.Produced > 0)
        //                {
        //                    dt_Row[headerProduced] = uData.Produced;
        //                }



        //                dt_Row[headerProdInfo] = uData.prodinfo;
        //                dt_Row[headerPurchaseInfo] = uData.purchaseinfo;

        //                dt_Row[headerItemRemark] = uData.item_remark;
        //                dt_Row[headerIndex] = uData.index;

        //                if (dt_Data.Columns.Contains(text.Header_Customer))
        //                    dt_Row[text.Header_Customer] = uData.customer_name;

        //                dt_Row[headerType] = typeSingle;
        //                dt_Row[headerBalType] = balType_Unique;
        //                dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
        //                dt_Row[headerRawMat] = uData.raw_mat;
        //                dt_Row[headerPartCode] = uData.part_code;
        //                dt_Row[headerPartName] = uData.part_name;
        //                dt_Row[headerColorMat] = uData.color_mat;
        //                dt_Row[text.Header_ColorMatCode] = row[dalItem.ItemMBatch].ToString();
        //                dt_Row[headerPartWeight] = (uData.pw_per_shot / uData.cavity).ToString("0.##") + " (" + (uData.rw_per_shot / uData.cavity).ToString("0.##") + ")";
        //                dt_Row[headerReadyStock] = uData.ready_stock;
        //                dt_Row[headerEstimate] = uData.estimate;
        //                dt_Row[headerOut] = uData.deliveredOut;
        //                dt_Row[headerOutStd] = uData.outStd;
        //                dt_Row[headerBal1] = uData.bal1;
        //                dt_Row[headerBal2] = uData.bal2;
        //                dt_Row[headerBal3] = uData.bal3;

        //                dt_Row[headerForecast1] = uData.forecast1;
        //                dt_Row[headerForecast2] = uData.forecast2;
        //                dt_Row[headerForecast3] = uData.forecast3;

        //                dt_Data.Rows.Add(dt_Row);
        //                index++;
        //            }
        //        }

        //    }

        //    #endregion
        //    //^^^3930ms -> 1497ms -> 1000ms (17/3)

        //    #region load assembly part

        //    foreach (DataRow row in DT_ITEM_CUST.Rows)
        //    {

        //        string itemSearch = row[dalItem.ItemCode].ToString();

        //        if (itemSearch == "A0LK160R0")
        //        {
        //            var checkpoint = 1;
        //        }

        //        int assembly = row[dalItem.ItemAssemblyCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemAssemblyCheck]);
        //        int production = row[dalItem.ItemProductionCheck] == DBNull.Value ? 0 : Convert.ToInt32(row[dalItem.ItemProductionCheck]);
        //        bool gotNotPackagingChild = bool.TryParse(row[text.Header_GotNotPackagingChild].ToString(), out bool GotChild) ? GotChild : false;

        //        //check if got child part also
        //        if ((assembly == 1 || production == 1) && gotNotPackagingChild)//tool.ifGotChild2(uData.part_code, dt_Join)
        //        {
        //            uData.cust_id = row[dalItemCust.CustID].ToString();
        //            uData.customer_name = row[dalItemCust.CustName].ToString();
        //            uData.part_code = row[dalItem.ItemCode].ToString();

        //            var forecastData = GetCustomerThreeMonthsForecastQty(dt_ItemForecast, uData.cust_id, uData.part_code, 1, 2, 3);
        //            uData.forecast1 = forecastData.Item1;
        //            uData.forecast2 = forecastData.Item2;
        //            uData.forecast3 = forecastData.Item3;
        //            var estimate = EstimateNextOrderAndCheckIfStillActive(DT_PMMA_DATE, dt_TrfHist, uData.part_code, uData.customer_name);
        //            uData.estimate = estimate.Item1;
        //            bool NonActiveItem = estimate.Item2;

        //            uData.deliveredOut = (float)estimate.Item3;

        //            if (!(uData.forecast1 <= 0 && uData.forecast2 <= 0 && uData.forecast3 <= 0 && cbRemoveNoOrderItem.Checked) || uData.deliveredOut > 0 || !(NonActiveItem && (cbRemoveNoDeliveredItem.Checked || cbRemoveNoOrderItem.Checked)))
        //            {
        //                uData.item_remark = row[dalItem.ItemRemark].ToString();
        //                uData.index = index;
        //                uData.part_name = row[dalItem.ItemName].ToString();
        //                uData.color_mat = row[dalItem.ItemMBatch].ToString();
        //                uData.color = row[dalItem.ItemColor].ToString();
        //                uData.raw_mat = row[dalItem.ItemMaterial].ToString();
        //                uData.pw_per_shot = row[dalItem.ItemProPWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProPWShot]);
        //                uData.rw_per_shot = row[dalItem.ItemProRWShot] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProRWShot]);
        //                uData.cavity = row[dalItem.ItemCavity] == DBNull.Value ? 1 : Convert.ToSingle(row[dalItem.ItemCavity]);
        //                uData.cavity = uData.cavity == 0 ? 1 : uData.cavity;
        //                uData.ready_stock = row[dalItem.ItemStock] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemStock]);

        //                decimal SemenyihStock = deductSemenyihStock(uData.part_code);
        //                //uData.ready_stock = uData.ready_stock - (float)SemenyihStock;

        //                if (myconnstrng == text.DB_Semenyih)
        //                {
        //                    uData.ready_stock = (float)SemenyihStock;
        //                }

        //                var result = GetProduceQty(uData.part_code, DT_MACHINE_SCHEDULE);
        //                uData.toProduce = result.Item1;
        //                uData.Produced = result.Item2;
        //                uData.prodinfo = result.Item3;
        //                uData.purchaseinfo = loadOrderRecord(DT_PURCAHSE_ORDER_RECORD, uData.part_code);

        //                #region Balance Calculation

        //                uData.outStd = uData.forecast1 - uData.deliveredOut;
        //                if (uData.forecast1 == -1)
        //                {
        //                    uData.outStd = 0;

        //                }
        //                else if (uData.forecast1 > -1)
        //                {
        //                    uData.outStd = uData.forecast1 - uData.deliveredOut;
        //                }
        //                uData.bal1 = uData.ready_stock;

        //                if (uData.outStd >= 0)
        //                {
        //                    uData.bal1 = uData.ready_stock - uData.outStd;
        //                }
        //                uData.bal2 = uData.bal1 - uData.forecast2;

        //                if (uData.forecast2 == -1)
        //                {
        //                    uData.bal2 = uData.bal1 - (cbDeductEstimate.Checked ? uData.estimate : 0);

        //                }
        //                else if (uData.forecast2 > -1)
        //                {
        //                    uData.bal2 = uData.bal1 - uData.forecast2;
        //                }
        //                uData.bal3 = uData.bal2 - uData.forecast3;

        //                if (uData.forecast3 == -1)
        //                {
        //                    uData.bal3 = uData.bal2 - (cbDeductEstimate.Checked ? uData.estimate : 0);

        //                }
        //                else if (uData.forecast3 > -1)
        //                {
        //                    uData.bal3 = uData.bal2 - uData.forecast3;
        //                }

        //                #endregion

        //                if (!uData.color.Equals(uData.color_mat) && !string.IsNullOrEmpty(uData.color_mat))
        //                {
        //                    uData.color_mat += " (" + uData.color + ")";
        //                }
        //                dt_Row = dt_Data.NewRow();

        //                if (uData.toProduce > 0)
        //                {
        //                    dt_Row[headerToProduce] = uData.toProduce;
        //                }

        //                if (uData.Produced > 0)
        //                {
        //                    dt_Row[headerProduced] = uData.Produced;
        //                }

        //                dt_Row[headerProdInfo] = uData.prodinfo;
        //                dt_Row[headerPurchaseInfo] = uData.purchaseinfo;

        //                dt_Row[headerItemRemark] = uData.item_remark;
        //                dt_Row[headerIndex] = uData.index;

        //                if (dt_Data.Columns.Contains(text.Header_Customer))
        //                    dt_Row[text.Header_Customer] = uData.customer_name;

        //                dt_Row[headerItemType] = row[dalItem.ItemCat].ToString();
        //                dt_Row[headerType] = typeParent;
        //                dt_Row[headerBalType] = balType_Unique;
        //                dt_Row[headerRawMat] = uData.raw_mat;
        //                dt_Row[headerPartCode] = uData.part_code;
        //                dt_Row[headerPartName] = uData.part_name;
        //                dt_Row[headerColorMat] = uData.color_mat;
        //                dt_Row[text.Header_ColorMatCode] = row[dalItem.ItemMBatch].ToString();
        //                dt_Row[headerPartWeight] = (uData.pw_per_shot / uData.cavity).ToString("0.##") + " (" + (uData.rw_per_shot / uData.cavity).ToString("0.##") + ")";
        //                dt_Row[headerReadyStock] = uData.ready_stock;
        //                dt_Row[headerEstimate] = uData.estimate;
        //                dt_Row[headerOut] = uData.deliveredOut;
        //                dt_Row[headerOutStd] = uData.outStd;
        //                dt_Row[headerBal1] = uData.bal1;
        //                dt_Row[headerBal2] = uData.bal2;
        //                dt_Row[headerBal3] = uData.bal3;

        //                dt_Row[headerForecast1] = uData.forecast1;
        //                dt_Row[headerForecast2] = uData.forecast2;
        //                dt_Row[headerForecast3] = uData.forecast3;

        //                if (assembly == 1 && production == 0)
        //                {
        //                    dt_Row[headerParentColor] = AssemblyMarking;
        //                }
        //                else if (assembly == 0 && production == 1)
        //                {
        //                    dt_Row[headerParentColor] = InsertMoldingMarking;
        //                }
        //                else if (assembly == 1 && production == 1)
        //                {
        //                    dt_Row[headerParentColor] = AssemblyAfterProductionMarking;
        //                }

        //                if (uData.part_code.Substring(0, 3) == text.Inspection_Pass)
        //                {
        //                    dt_Row[headerParentColor] = InspectionMarking;
        //                }


        //                dt_Data.Rows.Add(dt_Data.NewRow());
        //                dt_Data.Rows.Add(dt_Row);
        //                index++;

        //                LoadChild(dt_Data, uData, 0.1m);
        //            }
        //        }
        //    }

        //    #endregion
        //    //2654ms -> 1320ms (16/3) -> 895ms (17/3)

        //    #endregion

        //    if (dt_Data.Rows.Count > 0)
        //    {
        //        dt_Data = CalAllCustomerRepeatedData(dt_Data);

        //        dt_Data = ItemSearch(dt_Data);

        //        dtMasterData = Sorting(dt_Data);

        //        if (cbIncludeProInfo.Checked)
        //        {
        //            foreach (DataRow row in dtMasterData.Rows)
        //            {
        //                string itemCode = row[headerPartCode].ToString();

        //                foreach (DataRow itemRow in DT_ITEM.Rows)
        //                {
        //                    if (itemCode.Equals(itemRow[dalItem.ItemCode].ToString()))
        //                    {
        //                        row[headerQuoTon] = itemRow[dalItem.ItemQuoTon].ToString();
        //                        row[headerProTon] = itemRow[dalItem.ItemProTon].ToString();
        //                        row[headerCavity] = itemRow[dalItem.ItemCavity].ToString();
        //                        row[headerQuoCT] = itemRow[dalItem.ItemQuoCT].ToString();
        //                        row[headerProCT] = itemRow[dalItem.ItemProCTTo].ToString();
        //                        row[headerPWPerShot] = itemRow[dalItem.ItemProPWShot].ToString();
        //                        row[headerRWPerShot] = itemRow[dalItem.ItemProRWShot].ToString();

        //                        break;

        //                        //row[headerTotalOut] = uData.color_mat;
        //                        //row[headerMonthlyOut] = uData.color_mat;
        //                    }
        //                }
        //            }
        //        }

        //        if (cbShowProDayNeeded.Checked)
        //        {
        //            DT_MACHINE_SCHEDULE_COPY = DT_MACHINE_SCHEDULE.Copy();

        //            DT_MACHINE_SCHEDULE_COPY.DefaultView.Sort = dalPlanning.machineID + " ASC";
        //            DT_MACHINE_SCHEDULE_COPY = DT_MACHINE_SCHEDULE_COPY.DefaultView.ToTable();

        //            foreach (DataRow row in dtMasterData.Rows)
        //            {
        //                string itemCode = row[headerPartCode].ToString();
        //                string balType = row[headerBalType].ToString();
        //                float bal3 = float.TryParse(row[headerBal3].ToString(), out bal3) ? bal3 : 0;

        //                string rawMat = row[headerRawMat].ToString();

        //                if ((balType == balType_Total || balType == balType_Unique) && bal3 < 0 && !string.IsNullOrEmpty(rawMat))
        //                {
        //                    foreach (DataRow itemRow in DT_ITEM.Rows)
        //                    {
        //                        if (itemCode.Equals(itemRow[dalItem.ItemCode].ToString()))
        //                        {

        //                            int Cavity = int.TryParse(itemRow[dalItem.ItemCavity].ToString(), out Cavity) ? Cavity : 0;

        //                            float proCT = float.TryParse(itemRow[dalItem.ItemProCTTo].ToString(), out proCT) ? proCT : 0;

        //                            if (proCT <= 0)
        //                            {
        //                                proCT = float.TryParse(itemRow[dalItem.ItemQuoCT].ToString(), out proCT) ? proCT : 0;
        //                            }


        //                            if (proCT > 0 && Cavity > 0)
        //                            {
        //                                int totalShot = (int)Math.Ceiling(bal3 * -1 / Cavity);

        //                                float totalSec = totalShot * proCT;

        //                                //int totalDay = (int)Math.Truncate(totalSec / 3600 / 22);

        //                                //calculate total hours need
        //                                float totalHrs = Convert.ToSingle(totalSec) / 3600;

        //                                int hoursPerDay = 22;

        //                                int totalDay = 0;

        //                                if (hoursPerDay != 0)
        //                                {
        //                                    totalDay = Convert.ToInt32(Math.Truncate(totalHrs / Convert.ToSingle(hoursPerDay)));
        //                                }

        //                                string proDaysNeededInfo = "ERROR";

        //                                if (totalDay > 0)
        //                                    proDaysNeededInfo = totalDay.ToString() + " Day (" + hoursPerDay + " hrs)";

        //                                float balHrs = (totalHrs - Convert.ToSingle(totalDay * hoursPerDay));

        //                                if (balHrs > 0)
        //                                {
        //                                    if (totalDay > 0)
        //                                    {
        //                                        proDaysNeededInfo += " + ";
        //                                        proDaysNeededInfo += balHrs.ToString("0.##") + " hrs";
        //                                    }
        //                                    else
        //                                    {
        //                                        proDaysNeededInfo = balHrs.ToString("0.##") + " hrs";

        //                                    }

        //                                }

        //                                int totalProducedQty = totalShot * Cavity;

        //                                proDaysNeededInfo += "\n" + " to Produce " + totalProducedQty + " pcs; ";

        //                                string macHistory = "";

        //                                if (cbIncludeMacRecord.Checked)
        //                                {
        //                                    macHistory = GetMachineHistory(itemCode);

        //                                }

        //                                row[headerItemRemark] = proDaysNeededInfo + "\n" + macHistory;



        //                            }
        //                            else
        //                            {
        //                                string macHistory = "";

        //                                if (cbIncludeMacRecord.Checked)
        //                                {
        //                                    macHistory = GetMachineHistory(itemCode);

        //                                }


        //                                row[headerItemRemark] = "ERROR: CT=" + proCT + " , Cavity=" + Cavity + "\n" + macHistory;
        //                            }

        //                            break;

        //                            //row[headerTotalOut] = uData.color_mat;
        //                            //row[headerMonthlyOut] = uData.color_mat;
        //                        }
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    else
        //    {
        //        dtMasterData = null;
        //    }



        //    btnFullReport.Enabled = true;
        //    btnSummary.Enabled = true;
        //    btnRefresh.Enabled = true;
        //    btnExcel.Enabled = true;
        //    btnExcelAll.Enabled = true;

        //    Cursor = Cursors.Arrow;

        //    return dtMasterData;

        //}
    }
}
