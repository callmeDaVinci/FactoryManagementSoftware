using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using System.Linq;
using FactoryManagementSoftware.DAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ExplorerBar;
using System.Security.Cryptography.X509Certificates;
using FactoryManagementSoftware.BLL;
using static Syncfusion.XlsIO.Parser.Biff_Records.PaletteRecord;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;

namespace FactoryManagementSoftware.UI
{
    public partial class OUGSMYDBMerge : Form
    {
        Tool tool = new Tool();
        Text text = new Text();
        facStockDAL dalStock = new facStockDAL();
        itemDAL dalItem = new itemDAL();

        #region Data String

        private string path = null;
        private string excelName = null;


        #endregion


        public OUGSMYDBMerge()
        {
            InitializeComponent();
        }

        #region New Table Setting


        #endregion

        // Step 1: Browse and select CSV file
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            txtPath.BackColor = SystemColors.Window;
            txtPath.Clear();
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "PDF Files (*.pdf)|*.pdf";
            fd.Title = "Select P/O PDF";

            DialogResult re = fd.ShowDialog();
            if (re == DialogResult.OK)
            {
                path = fd.FileName;
                txtPath.Text = path;
                txtPath.ForeColor = Color.Black;
            }
        }

        // Step 2: Convert CSV to DataTable and display in DataGridView
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("Please select a CSV file first.");
                return;
            }

            DataTable csvDataTable = ReadCsvToDataTable(txtPath.Text);
            dgvMainList.DataSource = csvDataTable;
        }


        private DataTable DT_SOURCE;

        // Helper method to read CSV and convert to DataTable
        private DataTable ReadCsvToDataTable(string filePath)
        {
            DT_SOURCE = new DataTable();

            using (var reader = new StreamReader(filePath))
            {
                var headers = reader.ReadLine()?.Split(',');
                if (headers == null) return DT_SOURCE;

                foreach (var header in headers)
                {
                    DT_SOURCE.Columns.Add(header);
                }

                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine()?.Split(',');

                    // Only add rows with the correct number of columns
                    if (row != null && row.Length == DT_SOURCE.Columns.Count)
                    {
                        DT_SOURCE.Rows.Add(row);
                    }
                }
            }

            return DT_SOURCE;
        }

        private DataTable DT_SMY_TABLE;

        
        private void ListFilter()
        {
            //DT_SMY_TABLE = dalItemCust.SelectAll();
            
            //DT_SMY_TABLE = dalItemForecast.Select();

            //DT_SMY_TABLE = dalItem.Select();

            //// Create a HashSet to store normalized existing item codes for efficient lookup
            //HashSet<string> existingItemCodes = new HashSet<string>(
            //    DT_SMY_TABLE.AsEnumerable()
            //        .Select(r => NormalizeItemCode(r[dalItem.ItemCode].ToString()))
            //);

            //// Create a new DataTable to store the filtered results
            //DataTable filteredTable = DT_SOURCE.Clone(); // Clone structure but no data

            //foreach (DataRow row in DT_SOURCE.Rows)
            //{
            //    //String itemCode = row[dalItem.ItemCode]?.ToString();
            //    string itemCode = row[dalItemCust.ItemCode]?.ToString();
            //    int custID = int.TryParse(row[dalItemCust.CustID]?.ToString(), out int i) ? i : -1;

            //    // Check if the normalized itemCode exists in the HashSet
            //    if (!IfItemCustExist(itemCode, custID)) //IfItemCustExist(itemCode,custID)//!IfItemExist(itemCode)
            //    {
            //        filteredTable.ImportRow(row); // Add only non-existing items
            //    }
            //}

            //// Display filtered data in dgvSubList
            //dgvSubList.DataSource = filteredTable;
            dgvSubList.DataSource = DT_SOURCE;
        }

        // Helper method to normalize item codes
        private string NormalizeItemCode(string itemCode)
        {
            if (string.IsNullOrEmpty(itemCode)) return string.Empty;

            // Normalize by trimming, converting to uppercase, and removing extra spaces
            return string.Concat(itemCode.Trim().ToUpper()
                .Where(c => !char.IsWhiteSpace(c))); // Remove all whitespace (optional)
        }



        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Apply the filter and display the result in dgvSubList
            ListFilter();
        }

        private void InsertOrUpdateItemData()
        {
            foreach (DataGridViewRow row in dgvSubList.Rows)
            {
                // Retrieve item code
                string itemCode = row.Cells[dalItem.ItemCode].Value?.ToString();

                // Create and populate itemBLL object
                itemBLL item = new itemBLL
                {
                    item_cat = row.Cells[dalItem.ItemCat].Value?.ToString(),
                    item_code = itemCode,
                    item_name = row.Cells[dalItem.ItemName].Value?.ToString(),
                    item_unit = row.Cells[dalItem.ItemUnit].Value?.ToString(),
                    item_material = row.Cells[dalItem.ItemMaterial].Value?.ToString(),
                    item_mb = row.Cells[dalItem.ItemMBatch].Value?.ToString(),
                    item_mb_rate = float.TryParse(row.Cells[dalItem.ItemMBRate].Value?.ToString(), out var mbRate) ? mbRate : 0,
                    item_color = row.Cells[dalItem.ItemColor].Value?.ToString(),
                    item_quo_ton = int.TryParse(row.Cells[dalItem.ItemQuoTon].Value?.ToString(), out var quoTon) ? quoTon : 0,
                    item_best_ton = int.TryParse(row.Cells[dalItem.ItemBestTon].Value?.ToString(), out var bestTon) ? bestTon : 0,
                    item_pro_ton = int.TryParse(row.Cells[dalItem.ItemProTon].Value?.ToString(), out var proTon) ? proTon : 0,
                    item_quo_ct = int.TryParse(row.Cells[dalItem.ItemQuoCT].Value?.ToString(), out var quoCT) ? quoCT : 0,
                    item_pro_ct_from = int.TryParse(row.Cells[dalItem.ItemProCTFrom].Value?.ToString(), out var proCTFrom) ? proCTFrom : 0,
                    item_pro_ct_to = int.TryParse(row.Cells[dalItem.ItemProCTTo].Value?.ToString(), out var proCTTo) ? proCTTo : 0,
                    item_cavity = int.TryParse(row.Cells[dalItem.ItemCavity].Value?.ToString(), out var cavity) ? cavity : 0,
                    item_quo_pw_pcs = float.TryParse(row.Cells[dalItem.ItemQuoPWPcs].Value?.ToString(), out var quoPWPcs) ? quoPWPcs : 0,
                    item_quo_rw_pcs = float.TryParse(row.Cells[dalItem.ItemQuoRWPcs].Value?.ToString(), out var quoRWPcs) ? quoRWPcs : 0,
                    item_pro_pw_pcs = float.TryParse(row.Cells[dalItem.ItemProPWPcs].Value?.ToString(), out var proPWPcs) ? proPWPcs : 0,
                    item_pro_rw_pcs = float.TryParse(row.Cells[dalItem.ItemProRWPcs].Value?.ToString(), out var proRWPcs) ? proRWPcs : 0,
                    item_pro_pw_shot = float.TryParse(row.Cells[dalItem.ItemProPWShot].Value?.ToString(), out var proPWShot) ? proPWShot : 0,
                    item_pro_rw_shot = float.TryParse(row.Cells[dalItem.ItemProRWShot].Value?.ToString(), out var proRWShot) ? proRWShot : 0,
                    item_pro_cooling = int.TryParse(row.Cells[dalItem.ItemProCooling].Value?.ToString(), out var proCooling) ? proCooling : 0,
                    item_wastage_allowed = float.TryParse(row.Cells[dalItem.ItemWastage].Value?.ToString(), out var wastageAllowed) ? wastageAllowed : 0,
                    item_added_date = DateTime.TryParse(row.Cells[dalItem.ItemAddDate].Value?.ToString(), out var addDate) ? addDate : DateTime.Now,
                    item_added_by = int.TryParse(row.Cells[dalItem.ItemAddBy].Value?.ToString(), out var addedBy) ? addedBy : 0,
                    item_assembly = int.TryParse(row.Cells[dalItem.ItemAssemblyCheck].Value?.ToString(), out var assembly) ? assembly : 0,
                    item_production = int.TryParse(row.Cells[dalItem.ItemProductionCheck].Value?.ToString(), out var production) ? production : 0,
                    item_sbb = int.TryParse(row.Cells[dalItem.ItemSBBCheck].Value?.ToString(), out var sbb) ? sbb : 0,
                    unit_to_pcs_rate = float.TryParse(row.Cells[dalItem.ItemUnitToPCSRate].Value?.ToString(), out var unitToPcsRate) ? unitToPcsRate : 1,
                    Type_tbl_code = int.TryParse(row.Cells[dalItem.TypeTblCode].Value?.ToString(), out var typeTblCode) ? typeTblCode : 0,
                    Category_tbl_code = int.TryParse(row.Cells[dalItem.CategoryTblCode].Value?.ToString(), out var categoryTblCode) ? categoryTblCode : 0,
                    Size_tbl_code_1 = int.TryParse(row.Cells[dalItem.ItemSize1].Value?.ToString(), out var sizeTblCode1) ? sizeTblCode1 : 0,
                    Size_tbl_code_2 = int.TryParse(row.Cells[dalItem.ItemSize2].Value?.ToString(), out var sizeTblCode2) ? sizeTblCode2 : 0
                };

                // Check if the item exists in the database
                bool exists = ExistsInDatabase(itemCode,dalItem.ItemCode);

                bool success = false;
                if (exists)
                {
                    //success = dalItem.ItemMasterList_ItemUpdate(item); // Update existing item
                }
                else
                {
                    success = dalItem.ItemMasterList_ItemAdd(item); // Insert new item
                }

                if (!success)
                {
                    MessageBox.Show($"Failed to update item with code {itemCode}.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       
        custSupplierDAL dalCust = new custSupplierDAL();
        itemCustDAL dalItemCust  = new itemCustDAL();
        itemForecastDAL dalItemForecast  = new itemForecastDAL();

        private void InsertOrUpdateCustData()
        {
            foreach (DataGridViewRow row in dgvSubList.Rows)
            {
                // Retrieve item code
                int custID = int.TryParse(row.Cells[dalCust.CustID].Value?.ToString(), out var id) ? id : -1;

                // Create and populate object
                custSupplierBLL cust = new custSupplierBLL
                {
                    cust_id = custID,
                    cust_name = row.Cells[dalCust.CustName].Value?.ToString(),
                    cust_added_by = int.TryParse(row.Cells[dalCust.CustAddedBy].Value?.ToString(), out var addedBy) ? addedBy : 0,
                    cust_updtd_by = int.TryParse(row.Cells[dalCust.CustUpdatedBy].Value?.ToString(), out var updatedBy) ? updatedBy : 0,
                    cust_added_date = DateTime.TryParse(row.Cells[dalCust.CustAddedDate].Value?.ToString(), out var addDate) ? addDate : DateTime.Now,
                    cust_updtd_date = DateTime.TryParse(row.Cells[dalCust.CustUpdatedDate].Value?.ToString(), out var updatedDate) ? updatedDate : DateTime.Now,
                    cust_main = int.TryParse(row.Cells[dalCust.MainCust].Value?.ToString(), out var MainCust) ? MainCust : 0
                };

                // Check if the item exists in the database
                bool exists = ExistsInDatabase(custID.ToString(),dalCust.CustID);

                bool success = false;

                if (exists)
                {
                    success = dalCust.UpdateCust(cust);
                }
                else
                {
                    success = dalCust.InsertCust(cust);

                }

                if (!success)
                {
                    MessageBox.Show($"Failed to update item with code {custID}.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IfItemCustExist(string itemCode, int custID)
        {
            // Use LINQ to check if any row matches both itemCode and custID in DT_SMY_TABLE
            bool exists = DT_SMY_TABLE.AsEnumerable().Any(row =>
                row.Field<string>(dalItemCust.ItemCode) == itemCode &&
                row.Field<int>(dalItemCust.CustID) == custID);

            return exists;
        }

        private bool IfItemExist(string itemCode)
        {
            // Trim spaces from itemCode and compare with trimmed values in DT_SMY_TABLE
            bool exists = DT_SMY_TABLE.AsEnumerable().Any(row =>
                row.Field<string>(dalItem.ItemCode).Trim() == itemCode.Trim());

            return exists;
        }


        private void InsertOrUpdateCustItemData()
        {
            foreach (DataGridViewRow row in dgvSubList.Rows)
            {
                // Retrieve item code
                int custID = int.TryParse(row.Cells[dalItemCust.CustID].Value?.ToString(), out var id) ? id : -1;
                string itemCode = row.Cells[dalItemCust.ItemCode].Value?.ToString();

                // Create and populate object
                itemCustBLL ItemCust = new itemCustBLL
                {
                    cust_id = custID,
                    item_code = itemCode,
                    item_cust_added_date = DateTime.TryParse(row.Cells["item_cust_added_date"].Value?.ToString(), out var addDate) ? addDate : DateTime.Now,
                    item_cust_added_by = 1,
                    forecast_one = int.TryParse(row.Cells["forecast_one"].Value?.ToString(), out var forecast1) ? forecast1 : 0,
                    forecast_two = int.TryParse(row.Cells["forecast_two"].Value?.ToString(), out var forecast2) ? forecast2 : 0,
                    forecast_three = int.TryParse(row.Cells["forecast_three"].Value?.ToString(), out var forecast3) ? forecast3 : 0,
                    forecast_four = int.TryParse(row.Cells["forecast_four"].Value?.ToString(), out var forecast4) ? forecast4 : 0,
                    forecast_current_month = row.Cells["forecast_current_month"].Value?.ToString(),
                    forecast_updated_by = int.TryParse(row.Cells["forecast_updated_by"].Value?.ToString(), out var updatedBy) ? updatedBy : 0,
                    forecast_updated_date = DateTime.TryParse(row.Cells["forecast_updated_date"].Value?.ToString(), out var updatedDate) ? updatedDate : DateTime.Now
                };

                if (!IfItemCustExist(itemCode, custID))
                {
                    bool success = dalItemCust.InsertALL(ItemCust);

                    if (!success)
                    {
                        //Failed to insert data
                        MessageBox.Show($"Failed to add new {itemCode} record");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Data already exist.");
                }
            }
        }

        private void InsertItemForeacastData()
        {
            foreach (DataGridViewRow row in dgvSubList.Rows)
            {
                // Retrieve item code
                int custID = int.TryParse(row.Cells[dalItemForecast.CustID].Value?.ToString(), out var id) ? id : -1;
                int forecastYear = int.TryParse(row.Cells[dalItemForecast.ForecastYear].Value?.ToString(), out forecastYear) ? forecastYear : 0;
                int forecastMonth = int.TryParse(row.Cells[dalItemForecast.ForecastMonth].Value?.ToString(), out forecastMonth) ? forecastMonth : 0;
                string itemCode = row.Cells[dalItemForecast.ItemCode].Value?.ToString();

                // Create and populate object
                itemForecastBLL itemForecast = new itemForecastBLL
                {
                    cust_id = custID,
                    item_code = itemCode,
                    forecast_year = forecastYear,
                    forecast_month = forecastMonth,
                    forecast_qty = float.TryParse(row.Cells[dalItemForecast.ForecastQty].Value?.ToString(), out var ForecastQty) ? ForecastQty : 0,
                    updated_by = int.TryParse(row.Cells[dalItemForecast.UpdatedBy].Value?.ToString(), out var updatedBy) ? updatedBy : 0,
                    updated_date = DateTime.TryParse(row.Cells[dalItemForecast.UpdatedDate].Value?.ToString(), out var updatedDate) ? updatedDate : DateTime.Now
                };

                bool success = dalItemForecast.Insert(itemForecast);

                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show($"Failed to add new {itemCode} {custID} {forecastYear} {forecastMonth} record");
                }

            }
        }

    
        ordDAL dalOrd = new ordDAL();

        private void getDataFromUser()
        {
           
        }

        private void InsertOrderData()
        {
            foreach (DataGridViewRow row in dgvSubList.Rows)
            {
                string itemCode = row.Cells["ord_item_code"].Value?.ToString();
                int poNo = int.TryParse(row.Cells["ord_po_no"].Value?.ToString(), out var poNoInt) ? poNoInt : 0;
                float ordQty = float.TryParse(row.Cells["ord_qty"].Value?.ToString(), out var QrdQty) ? QrdQty : 0;
                DateTime requiredDate = DateTime.TryParse(row.Cells["ord_required_date"].Value?.ToString(), out var updatedDate) ? updatedDate : DateTime.Now;
                ordBLL uOrd = new ordBLL
                {
                    ord_item_code = itemCode,
                    ord_po_no = poNo,
                    ord_qty = ordQty,
                    ord_pending = float.TryParse(row.Cells["ord_pending"].Value?.ToString(), out var pendingQty) ? pendingQty : 0,
                    ord_received = float.TryParse(row.Cells["ord_received"].Value?.ToString(), out var receivedQty) ? receivedQty : 0,
                    ord_required_date = requiredDate,
                    ord_note = row.Cells["ord_note"].Value?.ToString(),
                    ord_unit = row.Cells["ord_unit"].Value?.ToString(),
                    ord_status = row.Cells["ord_status"].Value?.ToString(),
                    ord_type = row.Cells["ord_type"].Value?.ToString(),
                    ord_added_by = int.TryParse(row.Cells["ord_added_by"].Value?.ToString(), out var addedBy) ? addedBy : 0,
                    ord_added_date = DateTime.TryParse(row.Cells["ord_added_date"].Value?.ToString(), out var addedDate) ? addedDate : DateTime.Now
                };

                bool success = dalOrd.DataMerge(uOrd);

                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show($"Failed to add new {itemCode} ,pono:{poNo} ,ordQty:{ordQty}, required Date: {requiredDate} record");
                }

            }

        }

        pmmaDAL dalPMMA = new pmmaDAL();
        pmmaBLL uPMMA = new pmmaBLL();
        private void InsertPMMAZeroCOstData()
        {

            foreach (DataGridViewRow row in dgvSubList.Rows)
            {
                string itemCode = row.Cells[dalPMMA.itemCode].Value?.ToString();

                float openingStock = float.TryParse(row.Cells[dalPMMA.OpenStock].Value?.ToString(), out openingStock) ? openingStock : 0;
                float balanceQty = float.TryParse(row.Cells[dalPMMA.BalStock].Value?.ToString(), out balanceQty) ? balanceQty : 0;
                float directOutQty = float.TryParse(row.Cells[dalPMMA.DirectOut].Value?.ToString(), out directOutQty) ? directOutQty : 0;
                float adjustQty = float.TryParse(row.Cells[dalPMMA.Adjust].Value?.ToString(), out adjustQty) ? adjustQty : 0;
                string note = row.Cells[dalPMMA.Note].Value?.ToString();
                string month = row.Cells[dalPMMA.Month].Value?.ToString();
                string year = row.Cells[dalPMMA.Year].Value?.ToString();

                DateTime addedDate = DateTime.TryParse(row.Cells[dalPMMA.AddedDate].Value?.ToString(), out addedDate) ? addedDate : DateTime.Now;
                DateTime updatedDate = DateTime.TryParse(row.Cells[dalPMMA.UpdatedDate].Value?.ToString(), out updatedDate) ? updatedDate : DateTime.Now;
                int addedBy = int.TryParse(row.Cells[dalPMMA.AddedBy].Value?.ToString(), out addedBy) ? addedBy : 0;
                int udatedBy = int.TryParse(row.Cells[dalPMMA.UpdatedBy].Value?.ToString(), out udatedBy) ? udatedBy : 0;

                if (!string.IsNullOrEmpty(itemCode))
                {
                    pmmaBLL uPMMA = new pmmaBLL
                    {
                        pmma_item_code = itemCode,
                        pmma_openning_stock = openingStock,
                        pmma_bal_stock = balanceQty,
                        pmma_direct_out = directOutQty,
                        pmma_adjust = adjustQty,
                        pmma_note = note,
                        pmma_month = month,
                        pmma_year = year,
                        pmma_updated_date = updatedDate,
                        pmma_updated_by = udatedBy,
                        pmma_added_date = addedDate,
                        pmma_added_by = addedBy
                    };

                    bool success = dalPMMA.InsertOrUpdate(uPMMA);

                    if (!success)
                    {
                        //Failed to insert data
                        MessageBox.Show($"Failed to add new {itemCode} ,Month:{month} ,Year:{year} record");
                    }
                }
            }

        }
        private void InsertItemSMYStockData()
        {
           
        }


        private void InsertTrfHistData()
        {
            foreach (DataGridViewRow row in dgvSubList.Rows)
            {
                // Retrieve item code
                int custID = int.TryParse(row.Cells[dalItemForecast.CustID].Value?.ToString(), out var id) ? id : -1;
                int forecastYear = int.TryParse(row.Cells[dalItemForecast.ForecastYear].Value?.ToString(), out forecastYear) ? forecastYear : 0;
                int forecastMonth = int.TryParse(row.Cells[dalItemForecast.ForecastMonth].Value?.ToString(), out forecastMonth) ? forecastMonth : 0;
                string itemCode = row.Cells[dalItemForecast.ItemCode].Value?.ToString();

                // Create and populate object
                itemForecastBLL itemForecast = new itemForecastBLL
                {
                    cust_id = custID,
                    item_code = itemCode,
                    forecast_year = forecastYear,
                    forecast_month = forecastMonth,
                    forecast_qty = float.TryParse(row.Cells[dalItemForecast.ForecastQty].Value?.ToString(), out var ForecastQty) ? ForecastQty : 0,
                    updated_by = int.TryParse(row.Cells[dalItemForecast.UpdatedBy].Value?.ToString(), out var updatedBy) ? updatedBy : 0,
                    updated_date = DateTime.TryParse(row.Cells[dalItemForecast.UpdatedDate].Value?.ToString(), out var updatedDate) ? updatedDate : DateTime.Now
                };

                bool success = dalItemForecast.Insert(itemForecast);

                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show($"Failed to add new {itemCode} {custID} {forecastYear} {forecastMonth} record");
                }

            }
        }
        private void btnConfirmUpdate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            //InsertOrUpdateItemData();
            //InsertOrUpdateCustData();
            //InsertOrUpdateCustItemData();
            //InsertItemForeacastData();
            //InsertItemSMYStockData();
            //InsertOrderData();
            InsertPMMAZeroCOstData();
            Cursor = Cursors.Arrow;
            MessageBox.Show("Import operation completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Helper method to check if an item exists in the database by item_code
        private bool ExistsInDatabase(string keyID, string PrimaryColumn)
        {
            return DT_SMY_TABLE.AsEnumerable()
                .Any(row => row[PrimaryColumn].ToString() == keyID);
        }


    }
}
