using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
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

        private string IndexColumnName = "NO";
        private string codeColumnName = "item_code";
        private string nameColumnName = "item_name";
        private string factoryColumnName = "";
        private string totakStockColumnName = "item_qty";
        private string unitColumnName = "stock_unit";
        readonly string CMBPartHeader = "Parts";
        readonly string CMBMaterialHeader = "Materials";
        private int index = 0;
        private int alphbet = 65;


        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;
      
        #endregion

        #region Class Object

        facDAL dalFac = new facDAL();
        facStockDAL dalStock = new facStockDAL();
        itemDAL dalItem = new itemDAL();
        joinDAL dalJoin = new joinDAL();
        custDAL dalCust = new custDAL();
        itemCatDAL dalItemCat = new itemCatDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        CheckChildBLL uCheckChild = new CheckChildBLL();
        Tool tool = new Tool();

        

        #endregion

        #region UI setting

        public frmStockReport()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            InitializeComponent();
            createDatagridview();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void listPaint(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            //dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            //dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237,237,237);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            //dgv.RowTemplate.Height = 40;


            dgv.ClearSelection();
        }

        private void createDatagridview()
        {
            DataGridView dgv = dgvStockReport;
            //add category column
            tool.AddTextBoxColumns(dgv, IndexColumnName, IndexColumnName, DisplayedCells);

            //add name column
            tool.AddTextBoxColumns(dgv, "Name", nameColumnName, Fill);

            //add code column
            tool.AddTextBoxColumns(dgv, "Code", codeColumnName, Fill);

            //add factory columns
            DataTable dt = dalFac.Select();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    factoryColumnName = stock["fac_name"].ToString();
                    tool.AddTextBoxColumns(dgv, factoryColumnName, factoryColumnName, DisplayedCells);
                    dgv.Columns[factoryColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            //add total qty column
            tool.AddTextBoxColumns(dgv, "Total Stock", totakStockColumnName, DisplayedCells);
            dgv.Columns[totakStockColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //add total unit column
            tool.AddTextBoxColumns(dgv, "Unit", unitColumnName, DisplayedCells);
        }

        #endregion

        #region load data

        private void frmStockReport_Load(object sender, EventArgs e)
        {
            loadTypeData();
            dgvStockReport.ClearSelection();
        }

        private void frmStockReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.stockReportFormOpen = false;
        }

        private void loadTypeData()
        {
            cmbType.Items.Clear();
            cmbType.Items.Add(CMBPartHeader);
            cmbType.Items.Add(CMBMaterialHeader);
        }

        private void loadMaterialToComboBox(ComboBox cmb)
        {
            DataTable dtItemCat = dalItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            for (int i = distinctTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = distinctTable.Rows[i];
                if (dr["item_cat_name"].ToString() == "Part")
                {
                    distinctTable.Rows.Remove(dr);
                }
            }
            distinctTable.AcceptChanges();
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "item_cat_name";
            cmb.SelectedIndex = -1;
        }

        private void loadMaterialStockData()
        {
            index = 1;
            DataTable dt;
            dt = dalItem.catSearch(cmbSubType.Text);

            dgvStockReport.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                if (item["item_cat"].ToString().Equals(cmbSubType.Text))//show data under choosen category
                {
                    int n = dgvStockReport.Rows.Add();
                    dataInsertToStockDGV(dgvStockReport, item, n,index.ToString());
                    index++;
                }
            }
            listPaint(dgvStockReport);
        }

        private void dataInsertToStockDGV(DataGridView dgv, DataRow row, int n, string indexNo)
        {
            dgv.Rows[n].Cells[IndexColumnName].Value = indexNo;
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
                    string qty = Convert.ToSingle(stock["stock_qty"]).ToString("0.00");
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

        private bool ifGotChild(string itemCode)
        {
            bool result = false;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        private void loadPartStockData()
        {
            DataGridView dgv = dgvStockReport;
            DataTable dt = dalItemCust.custSearch(cmbSubType.Text);//load customer's item list

            if (dt.Rows.Count <= 0)
            {
                //MessageBox.Show("no data under this record.");
            }
            else
            {
                dgv.Rows.Clear();
                dgv.Refresh();

                //load single data
                index = 1;
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item[codeColumnName].ToString();

                    if (!ifGotChild(itemCode))
                    {
                        int n = dgv.Rows.Add();

                        dataInsertToStockDGV(dgvStockReport, item, n, index.ToString());
                        index++;
                    }
                }
               
                //load parent and child data
                foreach (DataRow item in dt.Rows)
                {
                    string itemCode = item[codeColumnName].ToString();

                    if (ifGotChild(itemCode))
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].DefaultCellStyle.BackColor = Color.Black;
                        dgv.Rows[n].Height = 3;

                        n = dgv.Rows.Add();
                        dgv.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        dgv.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        dataInsertToStockDGV(dgvStockReport, item, n, index.ToString());
                        loadChildParStocktData(itemCode, index);
                        index++;
                       
                    }
                    alphbet = 65;
                }
            }
            dgv.ClearSelection();
        }

        private void loadChildParStocktData(string itemCode, int no)
        {
            DataGridView dgv = dgvStockReport;
            string parentItemCode = itemCode;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    int n = dgv.Rows.Add();

                    dgv.Rows[n].Cells["item_code"].Value = Join["join_child_code"].ToString();

                    DataTable dtItem = dalItem.codeSearch(Join["join_child_code"].ToString());

                    if (dtItem.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtItem.Rows)
                        {
                            string indexNo = no.ToString() + "(" + (char)alphbet + ")";
                            dataInsertToStockDGV(dgvStockReport, item, n, indexNo);
                        }
                        alphbet++;
                    }
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

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            dgvStockReport.Rows.Clear();
            dgvStockReport.Refresh();
            if (cmbType.Text.Equals(CMBPartHeader))
            {
                tool.loadCustomerToComboBox(cmbSubType);
            }
            else if(cmbType.Text.Equals(CMBMaterialHeader))
            {
                loadMaterialToComboBox(cmbSubType);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void txtItemSearch_TextChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            loadMaterialStockData();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvStockReport_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            object tempObject1 = e.CellValue1;
            object tempObject2 = e.CellValue2;
            if (!(tempObject1 is null) && !(tempObject2 is null))
            {
                if (float.TryParse(tempObject1.ToString(), out float tmp) && float.TryParse(tempObject2.ToString(), out tmp))
                {
                    e.SortResult = float.Parse(tempObject1.ToString()).CompareTo(float.Parse(tempObject2.ToString()));
                    e.Handled = true;//pass by the default sorting
                }
            }
        }

        private void cmbSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            if (cmbType.Text.Equals(CMBPartHeader))
            {
                loadPartStockData();
            }
            else if (cmbType.Text.Equals(CMBMaterialHeader))
            {
                loadMaterialStockData();
            }


            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now.Date;
            fileName = "StockReport(" + cmbType.Text + ")_" + currentDate.ToString("ddMMyyyy") + ".xls";
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
