using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Runtime.InteropServices;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMaterialUsedReport : Form
    {
        public frmMaterialUsedReport()
        {
            InitializeComponent();
        }

        #region Valiable Declare

        private string colorName = "Black";

        #endregion

        #region create class object (database)

        custBLL uCust = new custBLL();
        custDAL dalCust = new custDAL();

        facBLL uFac = new facBLL();
        facDAL dalFac = new facDAL();

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        trfCatBLL utrfCat = new trfCatBLL();
        trfCatDAL daltrfCat = new trfCatDAL();

        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

        facStockBLL uStock = new facStockBLL();
        facStockDAL dalStock = new facStockDAL();

        itemCustBLL uItemCust = new itemCustBLL();
        itemCustDAL dalItemCust = new itemCustDAL();

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        forecastBLL uForecast = new forecastBLL();
        forecastDAL dalForecast = new forecastDAL();

        materialUsedBLL uMatUsed = new materialUsedBLL();
        materialUsedDAL dalMatUsed = new materialUsedDAL();

        #endregion

        #region load/close data

        private void insertItemQuantityOrderData()
        {
            string custName = cmbCust.Text;

            if (!string.IsNullOrEmpty(custName))
            {
                DataTable dt = dalItemCust.custSearch(custName);

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    int outStock = 0;
                    string itemCode;
                    string start = dtpStart.Value.ToString("yyyy/MM/dd");
                    string end = dtpEnd.Value.ToString("yyyy/MM/dd");

                    // string forecastNO = "";
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();

                        DataTable dt3 = daltrfHist.rangeSearch(cmbCust.Text, start, end, itemCode);

                        outStock = 0;

                        if (dt3.Rows.Count > 0)
                        {

                            foreach (DataRow outRecord in dt3.Rows)
                            {
                                outStock += Convert.ToInt32(outRecord["trf_hist_qty"]);

                            }
                        }

                        if (ifGotChild(itemCode))
                        {
                            DataTable dtJoin = dalJoin.parentCheck(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = Join["join_child_code"].ToString();
                                uMatUsed.quantity_order = outStock;

                                bool result = dalMatUsed.Insert(uMatUsed);
                                if (!result)
                                {
                                    MessageBox.Show("failed to insert material used data");
                                    return;
                                }
                                else
                                {
                                    forecastIndex++;
                                }
                            }
                        }
                        else
                        {
                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = itemCode;
                            uMatUsed.quantity_order = outStock;

                            bool result = dalMatUsed.Insert(uMatUsed);
                            if (!result)
                            {
                                MessageBox.Show("failed to insert material used data");
                                return;
                            }
                            else
                            {
                                forecastIndex++;
                            }
                        }
                    }
                }
            }

        }

        private void loadMaterialUsedList()
        {
            insertItemQuantityOrderData();
            int index = 1;
            string materialType = null;
            int OrderQty;
            float totalMaterialUsed = 0;
            float materialUsed = 0;
            float wastageUsed = 0;
            float wastagePercetage = 0;
            float itemWeight;

            DataTable dt = dalMatUsed.Select();
            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();

            dgvMaterialUsedRecord.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dgvMaterialUsedRecord.Rows.Add();

                itemWeight = Convert.ToSingle(item["item_part_weight"].ToString());
                OrderQty = Convert.ToInt32(item["quantity_order"].ToString());
                wastagePercetage = Convert.ToSingle(item["item_wastage_allowed"].ToString());

                materialUsed = OrderQty * itemWeight / 1000;

                wastageUsed = materialUsed * wastagePercetage;


                if (string.IsNullOrEmpty(materialType))
                {
                    materialType = item["item_material"].ToString();

                    totalMaterialUsed = materialUsed + wastageUsed;
                }
                else if (materialType == item["item_material"].ToString())
                {
                    //same data
                    totalMaterialUsed += materialUsed + wastageUsed;
                }
                else
                {
                    //first data
                    dgvMaterialUsedRecord.Rows[n - 1].Cells["total_material_used"].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                    dgvMaterialUsedRecord.Rows[n - 1].Cells["total_material_used"].Value = totalMaterialUsed;

                    materialType = item["item_material"].ToString();
                    totalMaterialUsed = materialUsed + wastageUsed;
                    n = dgvMaterialUsedRecord.Rows.Add();
                }

                if (dt.Rows.IndexOf(item) == dt.Rows.Count - 1)
                {
                    // this is the last item
                    totalMaterialUsed = materialUsed + wastageUsed;
                    dgvMaterialUsedRecord.Rows[n].Cells["total_material_used"].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                    dgvMaterialUsedRecord.Rows[n].Cells["total_material_used"].Value = totalMaterialUsed;
                }

                dgvMaterialUsedRecord.Rows[n].Cells["no"].Value = index;
                dgvMaterialUsedRecord.Rows[n].Cells["item_material"].Value = item["item_material"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["item_ord"].Value = item["quantity_order"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["item_part_weight"].Value = itemWeight;
                dgvMaterialUsedRecord.Rows[n].Cells["wastage_allowed"].Value = item["item_wastage_allowed"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["material_used"].Value = materialUsed;
                dgvMaterialUsedRecord.Rows[n].Cells["material_used_include_wastage"].Value = materialUsed + wastageUsed;


                index++;

            }

            listPaint(dgvMaterialUsedRecord);

        }

        private void frmMaterialUsedReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.MaterialUsedReportFormOpen = false;
        }

        private void frmMaterialUsedReport_Load(object sender, EventArgs e)
        {
            loadCustomerList();
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

            //bool rowColorChange = true;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;
                if (row.Cells["item_name"].Value == null)
                {
                    row.Height = 3;
                    row.DefaultCellStyle.BackColor = Color.FromName(colorName);

                }
            }

            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.ClearSelection();
        }

        private void loadCustomerList()
        {
            DataTable dt = dalCust.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmbCust.DataSource = distinctTable;
            cmbCust.DisplayMember = "cust_name";
            cmbCust.SelectedIndex = -1;
        }

        #endregion

        #region Data Checking/Get Data

        private string getCustID(string custName)
        {
            string custID = "";

            DataTable dtCust = dalCust.nameSearch(custName);

            foreach (DataRow Cust in dtCust.Rows)
            {
                custID = Cust["cust_id"].ToString();
            }
            return custID;
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

        #endregion

        #region function

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
          
            bool result = dalMatUsed.Delete();

            if (!result)
            {
                MessageBox.Show("Failed to reset forecast data");
            }
            else
            {
                loadMaterialUsedList();
                
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void frmMaterialUsedReport_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            dgvMaterialUsedRecord.ClearSelection();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        private void cmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbCust.Text.Equals("All"))
            {
            
            }
            else
            {

            }
        }

        #region export to excel

        //private string getNextFileName(string fileName)
        //{
        //    string extension = Path.GetExtension(fileName);

        //    int i = 0;
        //    while (File.Exists(fileName))
        //    {
        //        if (i == 0)
        //            fileName = fileName.Replace(extension, "(" + ++i + ")" + extension);
        //        else
        //            fileName = fileName.Replace("(" + i + ")" + extension, "(" + ++i + ")" + extension);
        //    }

        //    return fileName;
        //}

        private string setFileName()
        {
            string fileName = "Test.xls";
            DateTime currentDate = DateTime.Now.Date;
            fileName = "MaterialUsedReport(" + cmbCust.Text + ":"+dtpStart.Text+">"+dtpEnd.Text+")_" + currentDate.ToString("ddMMyyyy") + ".xls";
            return fileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = setFileName();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Copy DataGridView results to clipboard
                copyAlltoClipboard();

                object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                // Paste clipboard results to worksheet range
                Range CR = (Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                Range rng = xlWorkSheet.get_Range("A:Q").Cells; ;
                rng.EntireColumn.AutoFit();

                for (int i = 0; i <= dgvMaterialUsedRecord.RowCount - 2; i++)
                {
                    for (int j = 0; j <= dgvMaterialUsedRecord.ColumnCount - 1; j++)
                    {
                        Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];

                        if (i == 0)
                        {
                            Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                            header.Interior.Color = ColorTranslator.ToOle(dgvMaterialUsedRecord.Rows[i].Cells[j].InheritedStyle.BackColor);
                        }

                        if (dgvMaterialUsedRecord.Rows[i].Cells[j].InheritedStyle.BackColor == SystemColors.Window)
                        {
                            range.Interior.Color = ColorTranslator.ToOle(Color.White);
                            if (i == 0)
                            {
                                Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                header.Interior.Color = ColorTranslator.ToOle(Color.White);
                            }
                        }
                        else if (dgvMaterialUsedRecord.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Black)
                        {
                            range.Rows.RowHeight = 3;
                            range.Interior.Color = ColorTranslator.ToOle(dgvMaterialUsedRecord.Rows[i].Cells[j].InheritedStyle.BackColor);
                            if (i == 0)
                            {
                                Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                header.Interior.Color = ColorTranslator.ToOle(dgvMaterialUsedRecord.Rows[i].Cells[j].InheritedStyle.BackColor);
                            }
                        }
                        else
                        {
                            range.Interior.Color = ColorTranslator.ToOle(dgvMaterialUsedRecord.Rows[i].Cells[j].InheritedStyle.BackColor);

                            if (i == 0)
                            {
                                Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                header.Interior.Color = ColorTranslator.ToOle(dgvMaterialUsedRecord.Rows[i].Cells[j].InheritedStyle.BackColor);
                            }
                        }
                        range.Font.Color = dgvMaterialUsedRecord.Rows[i].Cells[j].Style.ForeColor;
                        if (dgvMaterialUsedRecord.Rows[i].Cells[j].Style.ForeColor == Color.Blue)
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
                dgvMaterialUsedRecord.ClearSelection();

                // Open the newly saved excel file
                if (File.Exists(sfd.FileName))
                    System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void copyAlltoClipboard()
        {
            dgvMaterialUsedRecord.SelectAll();
            DataObject dataObj = dgvMaterialUsedRecord.GetClipboardContent();
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
