using FactoryManagementSoftware.DAL;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace FactoryManagementSoftware.UI
{
    public partial class frmStockReport : Form
    {

        #region Variable

        private string categoryColumnName = "item_cat";
        private string codeColumnName = "item_code";
        private string nameColumnName = "item_name";
        private string factoryColumnName = "";
        private string totakStockColumnName = "item_qty";
        private string unitColumnName = "stock_unit";

        #endregion

        #region Class Object

        facDAL dalFac = new facDAL();
        facStockDAL dalStock = new facStockDAL();
        itemDAL dalItem = new itemDAL();
        itemCatDAL dalItemCat = new itemCatDAL();

        #endregion

        #region UI setting

        public frmStockReport()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            InitializeComponent();
            createDatagridview();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void datagridviewUI(DataGridView dgv)
        {
            dgv.Columns[categoryColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[codeColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[nameColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[totakStockColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[unitColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void AddColumns(string headText, string name)
        {
            var col = new DataGridViewTextBoxColumn();

            col.HeaderText = headText;
            col.Name = name;

            dgvStockReport.Columns.Add(col);
        }

        private void listPaint(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            //dgv.RowTemplate.Height = 40;


            dgv.ClearSelection();
        }

        private void createDatagridview()
        {
            //add category column
            AddColumns("Category", categoryColumnName);

            //add code column
            AddColumns("Code", codeColumnName);

            //add name column
            AddColumns("Name", nameColumnName);

            //add factory columns
            DataTable dt = dalFac.Select();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    factoryColumnName = stock["fac_name"].ToString();
                    AddColumns(factoryColumnName, factoryColumnName);
                    dgvStockReport.Columns[factoryColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
            }

            //add total qty column
            AddColumns("Total Stock", totakStockColumnName);

            //add total unit column
            AddColumns("Unit", unitColumnName);

            datagridviewUI(dgvStockReport);

        }

        #endregion

        #region load data

        private void frmStockReport_Load(object sender, EventArgs e)
        {
            loadItemCategoryData();
            dgvStockReport.ClearSelection();
        }

        private void frmStockReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.stockReportFormOpen = false;
        }

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dalItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbCat.DataSource = distinctTable;
            cmbCat.DisplayMember = "item_cat_name";
            cmbCat.SelectedIndex = 0;
        }

        private void loadItemStockData()
        {
            //get keyword from text box
            string keywords = txtItemSearch.Text;
            DataTable dt;

            if (!string.IsNullOrEmpty(keywords))
            {
                dt = dalItem.Search(keywords);//search item code and item name
            }
            else if (string.IsNullOrEmpty(cmbCat.Text) || cmbCat.Text.Equals("All"))
            {
                //show all item from the database
                dt = dalItem.Select();
            }
            else
            {
                dt = dalItem.catSearch(cmbCat.Text);
            }

            dgvStockReport.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                if (item["item_cat"].ToString().Equals(cmbCat.Text))//show data under choosen category
                {
                    int n = dgvStockReport.Rows.Add();
                    dataInsertToStockDataGridView(dgvStockReport, item, n);
                }
                else if (cmbCat.Text.Equals("All") || string.IsNullOrEmpty(cmbCat.Text))//show all data
                {
                    int n = dgvStockReport.Rows.Add();
                    dataInsertToStockDataGridView(dgvStockReport, item, n);
                }
            }
            listPaint(dgvStockReport);
        }

        private void dataInsertToStockDataGridView(DataGridView dgv, DataRow row, int n)
        {
            dgv.Rows[n].Cells["item_cat"].Value = row["item_cat"].ToString();
            dgv.Rows[n].Cells["item_code"].Value = row["item_code"].ToString();
            dgv.Rows[n].Cells["item_name"].Value = row["item_name"].ToString();
            dgv.Rows[n].Cells["item_qty"].Value = Convert.ToSingle(row["item_qty"]).ToString("0.00");

            loadStockList(row["item_code"].ToString(), n);
        }

        private void loadStockList(string itemCode,int n)
        {
            DataTable dt = dalStock.Select(itemCode);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    string factoryName = stock["fac_name"].ToString();
                    string qty = stock["stock_qty"].ToString();
                    dgvStockReport.Rows[n].Cells[factoryName].Value = qty;
                    dgvStockReport.Rows[n].Cells[unitColumnName].Value = stock["stock_unit"].ToString();
                }
            }
        }

        private void dgvStockReport_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            string factoryName = "";

            DataTable dt = dalFac.Select();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    factoryName = stock["fac_name"].ToString();
                    e.Row.Cells[factoryName].Value = "0";
                }
            }
            
        }

        #endregion

        #region click/index changed

        private void frmStockReport_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            dgvStockReport.ClearSelection();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            loadItemStockData();
            listPaint(dgvStockReport);
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void txtItemSearch_TextChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            loadItemStockData();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now.Date;
            fileName = "StockReport(" + cmbCat.Text + ")_" + currentDate.ToString("ddMMyyyy") + ".xls";
            return fileName;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = setFileName();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Copy DataGridView results to clipboard
                copyAlltoClipboard();

                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Format column D as text before pasting results, this was required for my data


                // Paste clipboard results to worksheet range
                Range CR = (Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                Range rng = xlWorkSheet.get_Range("A:Q").Cells;
                rng.EntireColumn.AutoFit();

                for (int i = 0; i <= dgvStockReport.RowCount - 2; i++)
                {
                    for (int j = 0; j <= dgvStockReport.ColumnCount - 1; j++)
                    {
                        Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];

                        if (i == 0)
                        {
                            Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                            header.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[i].Cells[j].InheritedStyle.BackColor);

                            //header = (Range)xlWorkSheet.Cells[1, 9];
                            //header.Font.Color = Color.Blue;

                            //header = (Range)xlWorkSheet.Cells[1, 13];
                            //header.Font.Color = Color.Red;

                            //header = (Range)xlWorkSheet.Cells[1, 14];
                            //header.Font.Color = Color.Red;

                            //header = (Range)xlWorkSheet.Cells[1, 16];
                            //header.Font.Color = Color.Red;
                        }

                        if (dgvStockReport.Rows[i].Cells[j].InheritedStyle.BackColor == SystemColors.Window)
                        {
                            range.Interior.Color = ColorTranslator.ToOle(Color.White);
                            if (i == 0)
                            {
                                Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                header.Interior.Color = ColorTranslator.ToOle(Color.White);
                            }
                        }
                        else if (dgvStockReport.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Black)
                        {
                            range.Rows.RowHeight = 3;
                            range.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[i].Cells[j].InheritedStyle.BackColor);
                            if (i == 0)
                            {
                                Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                header.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[i].Cells[j].InheritedStyle.BackColor);
                            }
                        }
                        else
                        {
                            range.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[i].Cells[j].InheritedStyle.BackColor);

                            if (i == 0)
                            {
                                Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                header.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[i].Cells[j].InheritedStyle.BackColor);
                            }
                        }
                        range.Font.Color = dgvStockReport.Rows[i].Cells[j].Style.ForeColor;
                        if (dgvStockReport.Rows[i].Cells[j].Style.ForeColor == Color.Blue)
                        {
                            Range header = (Range)xlWorkSheet.Cells[i + 2, 2];
                            header.Font.Underline = true;

                            header = (Range)xlWorkSheet.Cells[i + 2, 3];
                            header.Font.Underline = true;
                        }

                    }
                }


                // Save the excel file under the captured location from the SaveFileDialog
                xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlexcel);

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();
                dgvStockReport.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void copyAlltoClipboard()
        {
            dgvStockReport.SelectAll();
            DataObject dataObj = dgvStockReport.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);

        }

        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion
    }
}
