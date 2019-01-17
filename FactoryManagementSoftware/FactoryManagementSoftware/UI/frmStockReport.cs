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
        private string editedOldValue, editedNewValue, editedHeaderText;


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

        private bool loadMaterialStockData()
        {
            bool gotData = true;
            index = 1;
            DataTable dt;
            dt = dalItem.catSearch(cmbSubType.Text);

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
            
            listPaint(dgvStockReport);
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
                        dgv.Rows[n].Cells["item_code"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        dgv.Rows[n].Cells["item_name"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline | FontStyle.Bold) };
                        dataInsertToStockDGV(dgvStockReport, item, n, index.ToString());
                        loadChildParStocktData(itemCode, index);
                        index++;
                       
                    }
                    alphbet = 65;
                }
            }
            listPaint(dgvStockReport);
            dgv.ClearSelection();

            return gotData;
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

            DateTime currentDate = DateTime.Now;
            fileName = "StockReport(" + cmbSubType.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = setFileName();
            
            if (sfd.ShowDialog() == DialogResult.OK)
            {
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
                xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") Stock List";
                xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                xlWorkSheet.PageSetup.CenterFooter = "Printed By "+dalUser.getUsername(MainDashboard.USER_ID);

                //Page setup
                xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                xlWorkSheet.PageSetup.Zoom = false;
                xlWorkSheet.PageSetup.CenterHorizontally = true;
                xlWorkSheet.PageSetup.LeftMargin = 1;
                xlWorkSheet.PageSetup.RightMargin = 1;
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
                tRange.EntireColumn.AutoFit();
                tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);

                #endregion
                
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
            SaveFileDialog sfd = new SaveFileDialog();
    
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "StockReport(ALL)_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
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
     
                insertDataToSheet(path,sfd.FileName);

                xlexcel.DisplayAlerts = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlexcel.Quit();

                releaseObject(xlWorkBook);
                releaseObject(xlexcel);

                // Clear Clipboard and DataGridView selection
                Clipboard.Clear();
                dgvStockReport.ClearSelection();
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
                            tRange.EntireColumn.AutoFit();
                            tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);//change first row back color to light grey

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

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = setFileName();

            if (sfd.ShowDialog() == DialogResult.OK)
            {
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


                Worksheet xlWorkSheet;
                Worksheet xlWorkSheet2;

                var xlSheets = xlWorkBook.Sheets as Sheets;
                //var xlNewSheet = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                var xlNewSheet2 = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                //xlWorkSheet = xlNewSheet;
                xlWorkSheet2 = xlNewSheet2;
                xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet2 = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
                xlWorkSheet.Name = "first";
                xlWorkSheet2.Name = "second";

                #region Save data to Sheet1
                //xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
                string centerHeader = xlWorkSheet.PageSetup.CenterHeader;
                //"Arial Unicode MS" is font name, "18" is font size
                centerHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") Stock List";
                string LeftHeader = xlWorkSheet.PageSetup.CenterHeader;
                //"Arial Unicode MS" is font name, "18" is font size
                LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy");
                string RightHeader = xlWorkSheet.PageSetup.CenterHeader;
                //"Arial Unicode MS" is font name, "18" is font size
                RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                xlWorkSheet.PageSetup.LeftHeader = LeftHeader;
                xlWorkSheet.PageSetup.CenterHeader = centerHeader;
                xlWorkSheet.PageSetup.RightHeader = RightHeader;
                xlWorkSheet.PageSetup.CenterFooter = "footer here hahahahahaha";
                xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                xlWorkSheet.PageSetup.Zoom = false;
                xlWorkSheet.PageSetup.CenterHorizontally = true;
                xlWorkSheet.PageSetup.LeftMargin = 1;
                xlWorkSheet.PageSetup.RightMargin = 1;
                xlWorkSheet.PageSetup.FitToPagesWide = 1;
                xlWorkSheet.PageSetup.FitToPagesTall = false;
                xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

                // Paste clipboard results to worksheet range
                xlWorkSheet.Select();
                Range CR = (Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                Range tRange = xlWorkSheet.UsedRange;
                tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                tRange.Borders.Weight = XlBorderWeight.xlThin;
                tRange.Font.Size = 11;
                tRange.EntireColumn.AutoFit();

                ////first column: index 
                //Range indexCol = xlWorkSheet.Columns[1];
                //indexCol.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                //indexCol.VerticalAlignment = XlHAlign.xlHAlignCenter;
                //xlWorkSheet.Cells[1, 1].EntireRow.Font.Bold = true;


                Range dad = xlWorkSheet.UsedRange.Rows[1];
                dad.Interior.Color = Color.FromArgb(237, 237, 237);
                #endregion

                copyAlltoClipboard();
                //xlWorkSheet2 = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
                centerHeader = xlWorkSheet2.PageSetup.CenterHeader;
                //"Arial Unicode MS" is font name, "18" is font size
                centerHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") Stock List";
                LeftHeader = xlWorkSheet2.PageSetup.CenterHeader;
                //"Arial Unicode MS" is font name, "18" is font size
                LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy");
                RightHeader = xlWorkSheet2.PageSetup.CenterHeader;
                //"Arial Unicode MS" is font name, "18" is font size
                RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                xlWorkSheet2.PageSetup.LeftHeader = LeftHeader;
                xlWorkSheet2.PageSetup.CenterHeader = centerHeader;
                xlWorkSheet2.PageSetup.RightHeader = RightHeader;
                xlWorkSheet2.PageSetup.CenterFooter = "footer here hahahahahaha";
                xlWorkSheet2.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                xlWorkSheet2.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                xlWorkSheet2.PageSetup.Zoom = false;
                xlWorkSheet2.PageSetup.CenterHorizontally = true;
                xlWorkSheet2.PageSetup.LeftMargin = 1;
                xlWorkSheet2.PageSetup.RightMargin = 1;
                xlWorkSheet2.PageSetup.FitToPagesWide = 1;
                xlWorkSheet2.PageSetup.FitToPagesTall = false;
                xlWorkSheet2.PageSetup.PrintTitleRows = "$1:$1";
                // Paste clipboard results to worksheet range
                xlWorkSheet2.Select();
                CR = (Range)xlWorkSheet2.Cells[1, 1];
                CR.Select();
                xlWorkSheet2.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                tRange = xlWorkSheet2.UsedRange;
                tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                tRange.Borders.Weight = XlBorderWeight.xlThin;
                tRange.Font.Size = 11;
                tRange.EntireColumn.AutoFit();


                dad = xlWorkSheet2.UsedRange.Rows[1];
                dad.Interior.Color = Color.FromArgb(237, 237, 237);

                //Save the excel file under the captured location from the SaveFileDialog
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

        private void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            Workbook g_Workbook = excelApp.Workbooks.Open(
                @"D:\TestAddSheet.xlsx",
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
   

                        int count = g_Workbook.Worksheets.Count;
                        Worksheet addedSheet = g_Workbook.Worksheets.Add(Type.Missing,
                                g_Workbook.Worksheets[count], Type.Missing, Type.Missing);
                        addedSheet.Name = cmbSubType.Text;

                        addedSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                        addedSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") Stock List";
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
                        tRange.EntireColumn.AutoFit();
                        tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);//change first row back color to light grey

                        Clipboard.Clear();
                        dgvStockReport.ClearSelection();
                    }
                }
            }


        }

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

        //copy data from datagridview to clipboard and paste to excel sheet
        private void saveDataToSheet(Workbook xlWorkBook, string filename)
        {
            int sheetNo = 1;

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
                        copyAlltoClipboard();//select all from datagridview and copy to clipboard

                        //create new sheet
                        //var xlSheets = xlWorkBook.Sheets as Sheets;
                        //var xlNewSheet = (Worksheet)xlSheets.Add(xlSheets[sheetNo], Type.Missing, Type.Missing, Type.Missing);
                        //xlWorkSheet = xlNewSheet;
                        //Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(sheetNo);
                        //xlWorkSheet.Name = cmbSubType.Text;


                        int count = xlWorkBook.Worksheets.Count;
                        Worksheet addedSheet = xlWorkBook.Worksheets.Add(Type.Missing,
                                xlWorkBook.Worksheets[count], Type.Missing, Type.Missing);
                        addedSheet.Name = cmbSubType.Text;

                        addedSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                        addedSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") Stock List";
                        addedSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                        addedSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                        ////Header and Footer setup
                        //xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                        //xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") Stock List";
                        //xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                        //xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

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

                        // Paste clipboard results to worksheet range
                        addedSheet.Select();
                        Range CR = (Range)addedSheet.Cells[1, 1];
                        CR.Select();
                        addedSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                        //content edit
                        Range tRange = addedSheet.UsedRange;
                        tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                        tRange.Borders.Weight = XlBorderWeight.xlThin;
                        tRange.Font.Size = 11;
                        tRange.EntireColumn.AutoFit();
                        tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);//change first row back color to light grey

                        sheetNo++;

                        xlWorkBook.SaveAs(filename, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                        // Clear Clipboard and DataGridView selection
                        Clipboard.Clear();
                        dgvStockReport.ClearSelection();
                        releaseObject(addedSheet);
                    }
                }
            }
        }

        //Worksheet xlWorkSheet;
        //Worksheet xlWorkSheet2;

        //var xlSheets = xlWorkBook.Sheets as Sheets;
        ////var xlNewSheet = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
        //var xlNewSheet2 = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
        ////xlWorkSheet = xlNewSheet;
        //xlWorkSheet2 = xlNewSheet2;
        //xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
        //xlWorkSheet2 = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
        //xlWorkSheet.Name = "first";
        //xlWorkSheet2.Name = "second";

        //#region Save data to Sheet1
        ////xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
        //string centerHeader = xlWorkSheet.PageSetup.CenterHeader;
        ////"Arial Unicode MS" is font name, "18" is font size
        //centerHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") Stock List";
        //string LeftHeader = xlWorkSheet.PageSetup.CenterHeader;
        ////"Arial Unicode MS" is font name, "18" is font size
        //LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy");
        //string RightHeader = xlWorkSheet.PageSetup.CenterHeader;
        ////"Arial Unicode MS" is font name, "18" is font size
        //RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
        //xlWorkSheet.PageSetup.LeftHeader = LeftHeader;
        //xlWorkSheet.PageSetup.CenterHeader = centerHeader;
        //xlWorkSheet.PageSetup.RightHeader = RightHeader;
        //xlWorkSheet.PageSetup.CenterFooter = "footer here hahahahahaha";
        //xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
        //xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
        //xlWorkSheet.PageSetup.Zoom = false;
        //xlWorkSheet.PageSetup.CenterHorizontally = true;
        //xlWorkSheet.PageSetup.LeftMargin = 1;
        //xlWorkSheet.PageSetup.RightMargin = 1;
        //xlWorkSheet.PageSetup.FitToPagesWide = 1;
        //xlWorkSheet.PageSetup.FitToPagesTall = false;
        //xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

        //// Paste clipboard results to worksheet range
        //xlWorkSheet.Select();
        //Range CR = (Range)xlWorkSheet.Cells[1, 1];
        //CR.Select();
        //xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

        //Range tRange = xlWorkSheet.UsedRange;
        //tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
        //tRange.Borders.Weight = XlBorderWeight.xlThin;
        //tRange.Font.Size = 11;
        //tRange.EntireColumn.AutoFit();

        //////first column: index 
        ////Range indexCol = xlWorkSheet.Columns[1];
        ////indexCol.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        ////indexCol.VerticalAlignment = XlHAlign.xlHAlignCenter;
        ////xlWorkSheet.Cells[1, 1].EntireRow.Font.Bold = true;


        //Range dad = xlWorkSheet.UsedRange.Rows[1];
        //dad.Interior.Color = Color.FromArgb(237, 237, 237);
        //#endregion

        //copyAlltoClipboard();
        ////xlWorkSheet2 = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
        //centerHeader = xlWorkSheet2.PageSetup.CenterHeader;
        ////"Arial Unicode MS" is font name, "18" is font size
        //centerHeader = "&\"Calibri,Bold\"&16 (" + cmbSubType.Text + ") Stock List";
        //LeftHeader = xlWorkSheet2.PageSetup.CenterHeader;
        ////"Arial Unicode MS" is font name, "18" is font size
        //LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy");
        //RightHeader = xlWorkSheet2.PageSetup.CenterHeader;
        ////"Arial Unicode MS" is font name, "18" is font size
        //RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
        //xlWorkSheet2.PageSetup.LeftHeader = LeftHeader;
        //xlWorkSheet2.PageSetup.CenterHeader = centerHeader;
        //xlWorkSheet2.PageSetup.RightHeader = RightHeader;
        //xlWorkSheet2.PageSetup.CenterFooter = "footer here hahahahahaha";
        //xlWorkSheet2.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
        //xlWorkSheet2.PageSetup.Orientation = XlPageOrientation.xlPortrait;
        //xlWorkSheet2.PageSetup.Zoom = false;
        //xlWorkSheet2.PageSetup.CenterHorizontally = true;
        //xlWorkSheet2.PageSetup.LeftMargin = 1;
        //xlWorkSheet2.PageSetup.RightMargin = 1;
        //xlWorkSheet2.PageSetup.FitToPagesWide = 1;
        //xlWorkSheet2.PageSetup.FitToPagesTall = false;
        //xlWorkSheet2.PageSetup.PrintTitleRows = "$1:$1";
        //// Paste clipboard results to worksheet range
        //xlWorkSheet2.Select();
        //CR = (Range)xlWorkSheet2.Cells[1, 1];
        //CR.Select();
        //xlWorkSheet2.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

        //tRange = xlWorkSheet2.UsedRange;
        //tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
        //tRange.Borders.Weight = XlBorderWeight.xlThin;
        //tRange.Font.Size = 11;
        //tRange.EntireColumn.AutoFit();


        //dad = xlWorkSheet2.UsedRange.Rows[1];
        //dad.Interior.Color = Color.FromArgb(237, 237, 237);

        #endregion

        private void dgvStockReport_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var oldValue = dgvStockReport[e.ColumnIndex, e.RowIndex].Value;
            var newValue = e.FormattedValue;
            int columnIndex = dgvStockReport.CurrentCell.ColumnIndex;
            string columnName = dgvStockReport.Columns[columnIndex].HeaderText;

            if (!oldValue.ToString().Equals(newValue.ToString()))
            {
                editedOldValue = oldValue.ToString();
                editedNewValue = newValue.ToString();
                editedHeaderText = columnName;
            }
        }
    }
}
