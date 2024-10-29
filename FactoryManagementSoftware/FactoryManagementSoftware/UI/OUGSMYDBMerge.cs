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
            fd.Filter = "CSV files (*.csv)|*.csv";

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

        private DataTable DT_ITEM_SMY;

        private void ListFilter()
        {
            // Fetch existing items in SMY database table
            DT_ITEM_SMY = dalItem.Select();

            // Create a HashSet to store normalized existing item codes for efficient lookup
            HashSet<string> existingItemCodes = new HashSet<string>(
                DT_ITEM_SMY.AsEnumerable()
                    .Select(r => NormalizeItemCode(r[dalItem.ItemCode].ToString()))
            );

            // Create a new DataTable to store the filtered results
            DataTable filteredTable = DT_SOURCE.Clone(); // Clone structure but no data

            foreach (DataRow row in DT_SOURCE.Rows)
            {
                string itemCode = NormalizeItemCode(row[dalItem.ItemCode]?.ToString());

                // Check if the normalized itemCode exists in the HashSet
                if (!existingItemCodes.Contains(itemCode))
                {
                    filteredTable.ImportRow(row); // Add only non-existing items
                }
            }

            // Display filtered data in dgvSubList
            dgvSubList.DataSource = filteredTable;
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

        private void btnConfirmUpdate_Click(object sender, EventArgs e)
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
                bool exists = ItemExistsInDatabase(itemCode);

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

            MessageBox.Show("Update operation completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Helper method to check if an item exists in the database by item_code
        private bool ItemExistsInDatabase(string itemCode)
        {
            return DT_ITEM_SMY.AsEnumerable()
                .Any(row => row[dalItem.ItemCode].ToString() == itemCode);
        }


    }
}
