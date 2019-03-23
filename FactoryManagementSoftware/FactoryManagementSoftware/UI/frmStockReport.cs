﻿using FactoryManagementSoftware.DAL;
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
using FactoryManagementSoftware.Properties;
using System.ComponentModel;

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
        //private int alphbet = 65;
        private string editedOldValue, editedNewValue, editedFactory, editedItem, editedTotal, editedUnit;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

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
        userDAL dalUser = new userDAL();
        Tool tool = new Tool();
        Text text = new Text();
        #endregion

        #region UI setting

        public frmStockReport()
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            InitializeComponent();
            createDatagridview();
            listPaint(dgvStockReport);
            progressBar1.Hide();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void listPaint(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237,237,237);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.ClearSelection();
        }

        private void createDatagridview()
        {
            DataGridView dgv = dgvStockReport;

            dgv.Columns.Clear();
            //add category column
            tool.AddTextBoxColumns(dgv, IndexColumnName, IndexColumnName, DisplayedCells);

            //add name column
            tool.AddTextBoxColumns(dgv, "NAME", nameColumnName, Fill);

            //add code column
            tool.AddTextBoxColumns(dgv, "CODE", codeColumnName, Fill);

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
            tool.AddTextBoxColumns(dgv, "TOTAL", totakStockColumnName, DisplayedCells);
            dgv.Columns[totakStockColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //add total unit column
            tool.AddTextBoxColumns(dgv, "UNIT", unitColumnName, DisplayedCells);

            if(dalUser.getPermissionLevel(MainDashboard.USER_ID) < 5)
            {
                dgv.ReadOnly = true;
            }
            else
            {
                dgv.Columns[IndexColumnName].ReadOnly = true;
                dgv.Columns[nameColumnName].ReadOnly = true;
                dgv.Columns[codeColumnName].ReadOnly = true;
                dgv.Columns[totakStockColumnName].ReadOnly = true;
                dgv.Columns[unitColumnName].ReadOnly = true;
            }
        }

        #endregion

        #region load data

        private void frmStockReport_Load(object sender, EventArgs e)
        {
            loadTypeData();
            tool.DoubleBuffered(dgvStockReport, true);
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

        private bool loadMaterialStockData()
        {
            bool gotData = true;
            index = 1;
            DataTable dt;
            dt = dalItem.catSearch(cmbSubType.Text);
            dt.DefaultView.Sort = "item_name ASC";
            dt = dt.DefaultView.ToTable();

            dgvStockReport.Rows.Clear();
            dgvStockReport.Refresh();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["item_cat"].ToString().Equals(cmbSubType.Text))//show data under choosen category
                    {
                        int n = dgvStockReport.Rows.Add();
                        dataInsertToStockDGV(dgvStockReport, item, n, index.ToString());
                        index++;
                    }
                }
            }
            else
            {
                gotData = false;
            }

            //listPaint(dgvStockReport);
            tool.listPaint(dgvStockReport);
            return gotData;
        }

        private void dataInsertToStockDGV(DataGridView dgv, DataRow row, int n, string indexNo)
        {
            float readyStock = Convert.ToSingle(row["item_qty"]);
            dgv.Rows[n].Cells[IndexColumnName].Value = indexNo;
            dgv.Rows[n].Cells["item_code"].Value = row["item_code"].ToString();
            dgv.Rows[n].Cells["item_name"].Value = row["item_name"].ToString();
            dgv.Rows[n].Cells["item_qty"].Value = readyStock.ToString("0.00");
           
            if(readyStock < 0)
            {
                dgv.Rows[n].Cells["item_qty"].Style.ForeColor = Color.Red;

            }

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
                    
                    if(tool.ifGotChild(itemCode))
                    {
                        dgvStockReport.Rows[n].Cells[unitColumnName].Value = "set";
                    }
                    else
                    {
                        dgvStockReport.Rows[n].Cells[unitColumnName].Value = stock["stock_unit"].ToString();
                    }
                    
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

        private bool loadPartStockData()
        {
            bool gotData = true;
            DataGridView dgv = dgvStockReport;
            DataTable dt = dalItemCust.custSearch(cmbSubType.Text);//load customer's item list
            dt.DefaultView.Sort = "item_name ASC";
            dt = dt.DefaultView.ToTable();
            dgv.Rows.Clear();
            dgv.Refresh();
            if (dt.Rows.Count <= 0)
            {
                //MessageBox.Show("no data under this record.");
                gotData = false;
            }
            else
            {
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

                        if(dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            dgv.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };

                        }
                        else if (!dalItem.checkIfAssembly(itemCode) && dalItem.checkIfProduction(itemCode))
                        {
                            dgv.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            dgv.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };

                        }
                        else
                        {
                            dgv.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            dgv.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        }

                        dataInsertToStockDGV(dgvStockReport, item, n, index.ToString());
                        loadChildParStocktData(itemCode, index.ToString());
                        index++;
                       
                    }
                    //alphbet = 65;
                }
            }
            listPaint(dgvStockReport);
            dgv.ClearSelection();

            return gotData;
        }

        private void loadChildParStocktData(string itemCode, string no)
        {
            int index = 1;
            string childIndex = no+"."+index.ToString();

            DataGridView dgv = dgvStockReport;
            string parentItemCode = itemCode;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                foreach (DataRow Join in dtJoin.Rows)
                {
                    if(dalItem.getCatName(Join["join_child_code"].ToString()).Equals("Part"))
                    {
                        int n = dgv.Rows.Add();

                        dgv.Rows[n].Cells["item_code"].Value = Join["join_child_code"].ToString();

                        string childItemCode = Join["join_child_code"].ToString();

                        if (ifGotChild(childItemCode))
                        {
                            if(dalItem.checkIfAssembly(childItemCode) && dalItem.checkIfProduction(childItemCode))
                            {
                                dgv.Rows[n].Cells[codeColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                dgv.Rows[n].Cells[nameColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            }
                            else if (dalItem.checkIfAssembly(childItemCode) && !dalItem.checkIfProduction(childItemCode))
                            {
                                dgv.Rows[n].Cells[codeColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                dgv.Rows[n].Cells[nameColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            }
                            else if (!dalItem.checkIfAssembly(childItemCode) && dalItem.checkIfProduction(childItemCode))
                            {
                                dgv.Rows[n].Cells[codeColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                                dgv.Rows[n].Cells[nameColumnName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                            }

                            loadChildParStocktData(Join["join_child_code"].ToString(), childIndex);
                        }

                        DataTable dtItem = dalItem.codeSearch(Join["join_child_code"].ToString());

                        ifRepeat(Join["join_child_code"].ToString(), n);

                        if (dtItem.Rows.Count > 0)
                        {
                            foreach (DataRow item in dtItem.Rows)
                            {
                                dataInsertToStockDGV(dgvStockReport, item, n, childIndex);
                            }
                            //alphbet++;
                            index++;
                            childIndex = no + "." + index.ToString();
                        }
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
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                dgvStockReport.Rows.Clear();
                dgvStockReport.Refresh();
                if (cmbType.Text.Equals(CMBPartHeader))
                {
                    tool.loadCustomerToComboBox(cmbSubType);
                }
                else if (cmbType.Text.Equals(CMBMaterialHeader))
                {
                    loadMaterialToComboBox(cmbSubType);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }         
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
           
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            fileName = "StockReport(" + cmbSubType.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            //NOTE : DONT play with the UI thread here...
            // Do Whatever work you are doing and for which you need to show    progress bar
            //CopyLotsOfFiles() // This is the function which is being run in the background
            e.Result = true;// Tell that you are done
        }

        void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Access Main UI Thread here
            progressBar1.Value = e.ProgressPercentage;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            bgWorker.DoWork += BackgroundWorkerDoWork;
            bgWorker.ProgressChanged += BackgroundWorkerProgressChanged;
            try
            {
                
                dgvStockReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                SaveFileDialog sfd = new SaveFileDialog();

                string path = @"D:\StockAssistant\Document\StockReport";
                Directory.CreateDirectory(path);
                sfd.InitialDirectory = path;

                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = setFileName();

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    progressBar1.Show();
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                    // Copy DataGridView results to clipboard
                    copyAlltoClipboard();
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application();
                    xlexcel.PrintCommunication = false;
                    xlexcel.ScreenUpdating = false;
                    xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    xlexcel.Calculation = XlCalculation.xlCalculationManual;
                    Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Name = cmbSubType.Text;

                    #region Save data to Sheet

                    //Header and Footer setup
                    //xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy");
                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "                                        (" + cmbSubType.Text + ") STOCK LIST                                         PG -&P";
                    //xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                    //Page setup
                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                    xlWorkSheet.PageSetup.Zoom = false;
                    xlWorkSheet.PageSetup.CenterHorizontally = true;
                    xlWorkSheet.PageSetup.LeftMargin = 30;
                    xlWorkSheet.PageSetup.RightMargin = 30;
                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;

                    
                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

                    xlexcel.PrintCommunication = true;
                    xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;
                    // Paste clipboard results to worksheet range
                    xlWorkSheet.Select();
                    Range CR = (Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                    
                    //content edit
                    Range tRange = xlWorkSheet.UsedRange;
                    tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    tRange.Borders.Weight = XlBorderWeight.xlThin;
                    tRange.Font.Size = 11;
                    tRange.Font.Name = "Calibri";
                    tRange.EntireColumn.AutoFit();
                    tRange.EntireRow.AutoFit();
                    tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);

                    #endregion
                    if (true)//cmbSubType.Text.Equals("PMMA")
                    {
                        for (int i = 0; i <= dgvStockReport.RowCount - 2; i++)
                        {
                            for (int j = 0; j <= dgvStockReport.ColumnCount - 1; j++)
                            {
                                Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];

                                if (i == 0)
                                {
                                    Range header = (Range)xlWorkSheet.Cells[i + 1, j + 1];
                                    header.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[i].Cells[j].InheritedStyle.BackColor);

                                    header = (Range)xlWorkSheet.Cells[1, 10];
                                    header.Font.Color = Color.Blue;

                                    header = (Range)xlWorkSheet.Cells[1, 14];
                                    header.Font.Color = Color.Red;

                                    header = (Range)xlWorkSheet.Cells[1, 16];
                                    header.Font.Color = Color.Red;


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

                            Int32 percentage = ((i + 1) * 100) / (dgvStockReport.RowCount - 2);
                            if (percentage >= 100)
                            {
                                percentage = 100;
                            }
                            bgWorker.ReportProgress(percentage);
                        }
                    }
                        

                    bgWorker.ReportProgress(100);
                    System.Threading.Thread.Sleep(1000);

                    //Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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
                dgvStockReport.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                progressBar1.Visible = false;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
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

        private void btnExportAllToExcel_Click(object sender, EventArgs e)
        {
            bgWorker.DoWork += BackgroundWorkerDoWork;
            bgWorker.ProgressChanged += BackgroundWorkerProgressChanged;
            try
            {
                dgvStockReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                SaveFileDialog sfd = new SaveFileDialog();
                string path2 = @"D:\StockAssistant\Document\StockReport";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "StockReport(ALL)_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    progressBar1.Show();
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                    string path = Path.GetFullPath(sfd.FileName);
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application();
                    xlexcel.PrintCommunication = false;
                    xlexcel.ScreenUpdating = false;
                    xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    //Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName,
                        XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue,
                        XlSaveAsAccessMode.xlExclusive,
                        misValue, misValue, misValue, misValue, misValue);

                    insertDataToSheet(path, sfd.FileName);

                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dgvStockReport.ClearSelection();
                }

                bgWorker.ReportProgress(100);
                System.Threading.Thread.Sleep(1000);
                dgvStockReport.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                progressBar1.Visible = false;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void insertDataToSheet(string path, string fileName)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application
                {
                    Visible = true
                };

                Workbook g_Workbook = excelApp.Workbooks.Open(
                    path,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

                bool gotData = false;
                object misValue = System.Reflection.Missing.Value;

                //load different data list to datagridview but changing the comboBox selected index


                for (int i = 0; i <= cmbType.Items.Count - 1; i++)
                {
                    cmbType.SelectedIndex = i;
                    for (int j = 0; j <= cmbSubType.Items.Count - 1; j++)
                    {
                        cmbSubType.SelectedIndex = j;
                        if (cmbType.Text.Equals(CMBPartHeader))
                        {
                            gotData = loadPartStockData();//if data != empty return true, else false
                        }
                        else if (cmbType.Text.Equals(CMBMaterialHeader))
                        {
                            gotData = loadMaterialStockData();//if data != empty return true, else false
                        }

                        if (gotData)//if datagridview have data
                        {
                            Worksheet addedSheet = null;

                            int count = g_Workbook.Worksheets.Count;

                            addedSheet = g_Workbook.Worksheets.Add(Type.Missing,
                                    g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

                            addedSheet.Name = cmbSubType.Text;

                            addedSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                            addedSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") STOCK LIST";
                            addedSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                            addedSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                            //Page setup
                            addedSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                            addedSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                            addedSheet.PageSetup.Zoom = false;
                            addedSheet.PageSetup.CenterHorizontally = true;
                            addedSheet.PageSetup.LeftMargin = 1;
                            addedSheet.PageSetup.RightMargin = 1;
                            addedSheet.PageSetup.FitToPagesWide = 1;
                            addedSheet.PageSetup.FitToPagesTall = false;
                            addedSheet.PageSetup.PrintTitleRows = "$1:$1";

                            copyAlltoClipboard();
                            addedSheet.Select();
                            Range CR = (Range)addedSheet.Cells[1, 1];
                            CR.Select();
                            addedSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                            Range tRange = addedSheet.UsedRange;
                            tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                            tRange.Borders.Weight = XlBorderWeight.xlThin;
                            tRange.Font.Size = 11;
                            tRange.Font.Name = "Calibri";
                            tRange.EntireRow.AutoFit();
                            tRange.EntireColumn.AutoFit();
                            tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);//change first row back color to light grey

                            if (true)//cmbSubType.Text.Equals("PMMA")
                            {
                                for (int x = 0; x <= dgvStockReport.RowCount - 2; x++)
                                {
                                    for (int y = 0; y <= dgvStockReport.ColumnCount - 1; y++)
                                    {
                                        Range range = (Range)addedSheet.Cells[x + 2, y + 1];

                                        if (x == 0)
                                        {
                                            Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                            header.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor);

                                            header = (Range)addedSheet.Cells[1, 10];
                                            header.Font.Color = Color.Blue;

                                            header = (Range)addedSheet.Cells[1, 14];
                                            header.Font.Color = Color.Red;

                                            header = (Range)addedSheet.Cells[1, 16];
                                            header.Font.Color = Color.Red;


                                        }

                                        if (dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor == SystemColors.Window)
                                        {
                                            range.Interior.Color = ColorTranslator.ToOle(Color.White);
                                            if (x == 0)
                                            {
                                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                                header.Interior.Color = ColorTranslator.ToOle(Color.White);
                                            }
                                        }
                                        else if (dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor == Color.Black)
                                        {
                                            range.Rows.RowHeight = 3;
                                            range.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor);
                                            if (x == 0)
                                            {
                                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                                header.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor);
                                            }
                                        }
                                        else
                                        {
                                            range.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor);

                                            if (x == 0)
                                            {
                                                Range header = (Range)addedSheet.Cells[x + 1, y + 1];
                                                header.Interior.Color = ColorTranslator.ToOle(dgvStockReport.Rows[x].Cells[y].InheritedStyle.BackColor);
                                            }
                                        }
                                        range.Font.Color = dgvStockReport.Rows[x].Cells[y].Style.ForeColor;
                                        if (dgvStockReport.Rows[x].Cells[y].Style.ForeColor == Color.Blue)
                                        {
                                            Range header = (Range)addedSheet.Cells[x + 2, 2];
                                            header.Font.Underline = true;

                                            header = (Range)addedSheet.Cells[x + 2, 3];
                                            header.Font.Underline = true;
                                        }

                                    }

                                    Int32 percentage = ((x + 1) * 100) / (dgvStockReport.RowCount - 2);
                                    if (percentage >= 100)
                                    {
                                        percentage = 100;
                                    }
                                    bgWorker.ReportProgress(percentage);
                                }
                            }
                            
                            releaseObject(addedSheet);
                            Clipboard.Clear();
                            dgvStockReport.ClearSelection();
                        }
                    }
                }
                g_Workbook.Worksheets.Item[1].Delete();
                g_Workbook.Save();
                releaseObject(g_Workbook);
            }
  
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                tool.historyRecord(text.System, e.Message, DateTime.Now, MainDashboard.USER_ID);
            }
        }

        #endregion

        #region test

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void dgvStockReport_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            int columnIndex = dgvStockReport.CurrentCell.ColumnIndex;
            string columnName = dgvStockReport.Columns[columnIndex].Name;
            if (tool.getFactoryID(columnName) != -1) //Desired Column
            {
                System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                if (cmbType.Text.Equals(CMBPartHeader) && !string.IsNullOrEmpty(cmbSubType.Text))
                {
                    loadPartStockData();
                }
                else if (cmbType.Text.Equals(CMBMaterialHeader) && !string.IsNullOrEmpty(cmbSubType.Text))
                {
                    loadMaterialStockData();
                }
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvStockReport_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //var datagridview = sender as DataGridView;
            //DataGridView dgv = dgvStockReport;
            //DateTime currentDate = DateTime.Now;

            //int rowIndex = e.RowIndex;

            //float openningStock = Convert.ToSingle(dgv.Rows[rowIndex].Cells[dalPMMA.OpenStock].Value.ToString());
            //float inQty = Convert.ToSingle(dgv.Rows[rowIndex].Cells[IndexInName].Value.ToString());
            //float outQty = Convert.ToSingle(dgv.Rows[rowIndex].Cells[IndexOutName].Value.ToString());
            //float bal = openningStock + inQty - outQty;
            //float percentage = Convert.ToSingle(dgv.Rows[rowIndex].Cells[IndexPercentageName].Value.ToString());
            //float wastage = outQty * percentage;
            //string itemCode = dgv.Rows[rowIndex].Cells[dalItem.ItemCode].Value.ToString();
            //float adjust = 0;
            //DateTime date = Convert.ToDateTime(dtpDate.Text).Date;
            //if (dgv.Rows[rowIndex].Cells[dalPMMA.Adjust].Value != null)
            //{
            //    adjust = Convert.ToSingle(dgv.Rows[rowIndex].Cells[dalPMMA.Adjust].Value.ToString());
            //}

            //string note = "";
            //if (dgv.Rows[rowIndex].Cells[dalPMMA.Note].Value != null)
            //{
            //    note = dgv.Rows[rowIndex].Cells[dalPMMA.Note].Value.ToString();
            //}


            //dgv.Rows[rowIndex].Cells[IndexBalName].Value = bal.ToString("0.00");
            //dgv.Rows[rowIndex].Cells[IndexWastageName].Value = wastage.ToString("0.00");
            //dgv.Rows[rowIndex].Cells[dalPMMA.BalStock].Value = (bal - wastage + adjust).ToString("0.00");

            //uPMMA.pmma_item_code = itemCode;
            //uPMMA.pmma_date = date;
            //uPMMA.pmma_openning_stock = openningStock;
            //uPMMA.pmma_percentage = percentage;
            //uPMMA.pmma_adjust = adjust;
            //uPMMA.pmma_note = note;
            //uPMMA.pmma_bal_stock = bal - wastage + adjust;
            //uPMMA.pmma_updated_date = DateTime.Now;
            //uPMMA.pmma_updated_by = MainDashboard.USER_ID;


            //bool success = dalPMMA.update(uPMMA);

            //if (!success)
            //{
            //    MessageBox.Show("Failed to updated PMMA item");
            //    tool.historyRecord("StockReportEdit", "Failed to edit Stock Report(frmPMMA)", DateTime.Now, MainDashboard.USER_ID);
            //}
            //else
            //{
            //    tool.historyRecord("StockReportEdit", text.getPMMAEditString(itemCode, date.ToString("MMMMyy"), editedHeaderText, editedOldValue, editedNewValue), DateTime.Now, MainDashboard.USER_ID);
            //}
        }

        private void dgvStockReport_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var itemCode = dgvStockReport[codeColumnName, e.RowIndex].Value;
            var oldValue = dgvStockReport[e.ColumnIndex, e.RowIndex].Value;
            var newValue = e.FormattedValue;
            var total = dgvStockReport[totakStockColumnName, e.RowIndex].Value;
            var unit = dgvStockReport[unitColumnName, e.RowIndex].Value;
            string facName = dgvStockReport.Columns[e.ColumnIndex].HeaderText;

            if(itemCode != null)
            {
                editedItem = itemCode.ToString();
            }
            else
            {
                editedItem = "";
                //MessageBox.Show("Item Error");
                return;
            }

            if (total != null)
            {
                if(float.TryParse(dgvStockReport[totakStockColumnName, e.RowIndex].Value.ToString(), out float i))
                {
                    editedTotal = i.ToString();
                }
                else
                {
                    editedTotal = dalItem.getStockQty(editedItem).ToString();
                }
            }

            if(oldValue == null)
            {
                oldValue = 0;
            }

            if (newValue == null)
            {
                oldValue = 0;
                newValue = 0;
            }

            if(unit != null)
            {
                editedUnit = unit.ToString();
            }
            else
            {
                if(tool.ifGotChild(editedItem))
                {
                    editedUnit = "set";
                }
                else
                {
                    editedUnit = "";
                }
            }

            if (!oldValue.ToString().Equals(newValue.ToString()) && !string.IsNullOrEmpty(newValue.ToString()))
            {
                editedOldValue = oldValue.ToString();
                editedNewValue = newValue.ToString();
                editedFactory = facName;

                bool outSuccess = stockOut(facName, editedItem, Convert.ToSingle(oldValue.ToString()), editedUnit);
                bool inSuccess = stockIn(facName, editedItem, Convert.ToSingle(newValue.ToString()), editedUnit);

                if(outSuccess && inSuccess)
                {
                    float newTotal = Convert.ToSingle(editedTotal) - Convert.ToSingle(editedOldValue) + Convert.ToSingle(editedNewValue);
                    dgvStockReport[totakStockColumnName, e.RowIndex].Value = newTotal;

                    if(newTotal < 0)
                    {
                        dgvStockReport[totakStockColumnName, e.RowIndex].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dgvStockReport[totakStockColumnName, e.RowIndex].Style.ForeColor = Color.Black;

                    }

                }
                else
                {
                    MessageBox.Show("stock change error");
                }

            }
        }

        private bool stockIn(string factoryName, string itemCode, float qty, string sub_unit)
        {
            bool successFacStockIn;

            successFacStockIn = dalStock.facStockIn(tool.getFactoryID(factoryName).ToString(), itemCode, qty, sub_unit);

            return successFacStockIn;
        }

        private bool stockOut(string factoryName, string itemCode, float qty, string sub_unit)
        {
            bool successFacStockOut;

            successFacStockOut = dalStock.facStockOut(tool.getFactoryID(factoryName).ToString(), itemCode, qty, sub_unit);

            return successFacStockOut;
        }
        #endregion

        private void ifRepeat(string itemCode, int index)
        {
            DataGridView dgv = dgvStockReport;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[codeColumnName].Value != null)
                {
                    if (row.Cells[codeColumnName].Value.ToString().Equals(itemCode) && row.Index < index)
                    {
                        dgv.Rows[index].DefaultCellStyle.BackColor = Color.Lavender;
                        return;
                    }
                }
            }
        }
    }
}
