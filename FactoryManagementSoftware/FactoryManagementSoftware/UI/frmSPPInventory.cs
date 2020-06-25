using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSPPInventory : Form
    {
        public frmSPPInventory()
        {
            frmLoading.ShowLoadingScreen();
            InitializeComponent();
            InitializeFilterData();
            tool.DoubleBuffered(dgvUnique, true);
            tool.DoubleBuffered(dgvCommon, true);
            //dt_spp = dalItem.SPPCommonSelect();

            frmLoading.CloseForm();
        }

        SPPDataDAL dalData = new SPPDataDAL();
        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        userDAL dalUser = new userDAL();

        private readonly string textShowFilters = "SHOW FILTER";
        private readonly string textHideFilters = "HIDE FILTER";

        private readonly string textShowAllSize = "show all size";
        private readonly string textShowAllCommonPart = "show all type of common part";
        private readonly string textShowAllUniquePart = "show all type of unique part";

        //private DataTable dt_spp;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadSizeData()
        {
            DataTable dt = dalData.SizeSelect();

            DataTable dt_Size = new DataTable();

            dt_Size.Columns.Add("SIZE");
            dt_Size.Rows.Add(textShowAllSize);

           

            foreach (DataRow row in dt.Rows)
            {
                bool isRemoved = Boolean.TryParse(row[dalData.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if(!isRemoved)
                {
                    string size = null;
                    int numerator = int.TryParse(row[dalData.SizeNumerator].ToString(), out numerator) ? numerator : 0;
                    int denominator = int.TryParse(row[dalData.SizeDenominator].ToString(), out denominator) ? denominator : 0;
                    string unit = row[dalData.SizeUnit].ToString();
                    string slash = "/";


                    if (numerator > 0)
                    {
                        size = numerator.ToString();

                        if (denominator > 1)
                        {
                            size += slash + denominator.ToString();
                        }

                        size += " " + unit;
                    }

                    if (size != null)
                    {
                        dt_Size.Rows.Add(size);
                    }

                }

                cmbSize.DataSource = dt_Size;
                cmbSize.DisplayMember = "SIZE";
            }
               
        }

        private void LoadTypeData()
        {


            DataTable dt = dalData.TypeSelect();

            DataTable dt_Common = new DataTable();
            dt_Common.Columns.Add("COMMON");
            dt_Common.Rows.Add(textShowAllCommonPart);

            //load type of unique part:socket, reducing...
            DataTable dt_Unique = new DataTable();
            dt_Unique.Columns.Add("UNIQUE");
            dt_Unique.Rows.Add(textShowAllUniquePart);

            foreach (DataRow row in dt.Rows)
            {
                bool isRemoved = Boolean.TryParse(row[dalData.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                if (!isRemoved)
                {
                    string name = row[dalData.TypeName].ToString();

                    bool isCommon = Boolean.TryParse(row[dalData.IsCommon].ToString(), out isCommon) ? isCommon : false;

                    if(isCommon)
                    {
                        dt_Common.Rows.Add(name);
                    }
                    else
                    {
                        dt_Unique.Rows.Add(name);
                    }
                }

                cmbCommonType.DataSource = dt_Common;
                cmbCommonType.DisplayMember = "COMMON";

                cmbUniqueType.DataSource = dt_Unique;
                cmbUniqueType.DisplayMember = "UNIQUE";
            }

        }

        private void InitializeFilterData()
        {

            LoadSizeData();

            LoadTypeData();

        }

        private void ShowOrHideFilterOption()
        {
            dgvCommon.SuspendLayout();
            dgvUnique.SuspendLayout();

            string text = btnFilter.Text;

            if (text == textShowFilters)
            {
                tlpInventory.RowStyles[1] = new RowStyle(SizeType.Absolute, 84f);

                dgvCommon.ResumeLayout();
                dgvUnique.ResumeLayout();

                btnFilter.Text = textHideFilters;
            }
            else if (text == textHideFilters)
            {
                tlpInventory.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgvCommon.ResumeLayout();
                dgvUnique.ResumeLayout();

                btnFilter.Text = textShowFilters;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }
        }

        private void LoadUniquePart()
        {
            DataTable dt = dalItem.SPPUniqueSelectWithoutAssembledItem();
            dt.Columns.Add("STOCK");
            dt.Columns.Add("DELIVERY QTY");
            dt.Columns.Add("PCS/BAG");
            dt.Columns.Add("TOTAL BAG(S)");
            dt.Columns.Add("MAX STOCK LEVEL");
            
            string preSize = "";
            string currentSize = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currentSize = dt.Rows[i]["SIZE"].ToString();
                int qtyPerBag = int.TryParse(dt.Rows[i]["STD_PACKING"].ToString(), out qtyPerBag)? qtyPerBag : 0;
                int stockQty = int.TryParse(dt.Rows[i]["QUANTITY"].ToString(), out stockQty) ? stockQty : 0;
                int toDeliveryQty = int.TryParse(dt.Rows[i]["TO_DELIVERY_QTY"].ToString(), out toDeliveryQty) ? toDeliveryQty : 0;
                int maxStockLevel = int.TryParse(dt.Rows[i]["MAX_LEVEL"].ToString(), out maxStockLevel) ? maxStockLevel : 0;

                dt.Rows[i]["STOCK"] = stockQty;

                if (qtyPerBag > 0)
                {
                    dt.Rows[i]["DELIVERY QTY"] = toDeliveryQty;
                    int bagQty = stockQty / qtyPerBag;

                    //dt.Rows[i]["STOCK"] = stockQty + " ("+bagQty+" bags)";

                    dt.Rows[i]["PCS/BAG"] = qtyPerBag+"/bag";
                    
                    dt.Rows[i]["TOTAL BAG(S)"] = bagQty;

                    //int maxStockLevel = 0;

                    //if(currentSize == "20")
                    //{
                    //    maxStockLevel = text.StockLevel_20;
                    //}
                    //else if (currentSize == "25")
                    //{
                    //    maxStockLevel = text.StockLevel_25;
                    //}
                    //else if (currentSize == "32")
                    //{
                    //    maxStockLevel = text.StockLevel_32;
                    //}
                    //else if (currentSize == "50")
                    //{
                    //    maxStockLevel = text.StockLevel_50;
                    //}
                    //else if (currentSize == "63")
                    //{
                    //    maxStockLevel = text.StockLevel_63;
                    //}

                    dt.Rows[i]["MAX STOCK LEVEL"] = maxStockLevel;
                }

                if (preSize == "")
                {
                    preSize = currentSize;
                }
                else if (preSize != currentSize)
                {
                    DataRow toInsert = dt.NewRow();
                    dt.Rows.InsertAt(toInsert, i);
                    preSize = currentSize;
                }
            }
            dt.Columns["STOCK"].ColumnName = "STOCK(PCS)";
            dt.Columns.Remove("QUANTITY");
            dt.Columns.Remove("STD_PACKING");
            dt.Columns.Remove("TO_DELIVERY_QTY");
            dt.Columns.Remove("MAX_LEVEL");
            dgvUnique.DataSource = dt;
         
            dgvUnique.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgvUnique.Columns["PCS/BAG"].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgvUnique.Columns["MAX STOCK LEVEL"].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgvUnique.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvUnique.Columns["PCS/BAG"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUnique.Columns["MAX STOCK LEVEL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUnique.Columns["TOTAL BAG(S)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUnique.Columns["SIZE"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvUnique.Columns["UNIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvUnique.Columns["DELIVERY QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUnique.Columns["DELIVERY QTY"].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            dgvUnique.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

            dgvUnique.ClearSelection();
        }

        private void LoadCommonPart()
        {
            DataTable dt = dalItem.SPPCommonSelect();

            string preSize = "";
            string currentSize = "";
            dt.Columns["STOCK"].ColumnName = "STOCK(PCS)";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currentSize = dt.Rows[i]["SIZE"].ToString();

                if(preSize == "")
                {
                    preSize = currentSize;
                }
                else if(preSize != currentSize)
                {
                    DataRow toInsert = dt.NewRow();
                    dt.Rows.InsertAt(toInsert, i);
                    preSize = currentSize;
                }
            }

            dgvCommon.DataSource = dt;
            dgvCommon.Columns["TYPE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCommon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgvCommon.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvCommon.Columns["SIZE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCommon.Columns["UNIT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCommon.Columns["SIZE"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvCommon.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            dgvCommon.ClearSelection();
        }

        private void frmSPPInventory_Load(object sender, EventArgs e)
        {
            ShowOrHideFilterOption();
            LoadCommonPart();
            LoadUniquePart();

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilterOption();
        }

        private void dgvCommon_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void dgvCommon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvCommon;
            dgv.SuspendLayout();
            int n = e.RowIndex;

            if (dgv.Columns[e.ColumnIndex].Name == "SIZE")
            {
                if(dgv.Rows[n].Cells["SIZE"].Value == DBNull.Value)
                {
                    dgv.Rows[n].Height = 5;
                    dgv.Rows[n].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    dgv.Rows[n].Height = 50;

                }
            }

           

            dgv.ResumeLayout();
        }

        private void dgvUnique_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvUnique;
            dgv.SuspendLayout();

            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if (dgv.Columns[e.ColumnIndex].Name == "TYPE")
            {
                if (dgv.Rows[rowIndex].Cells["TYPE"].Value == DBNull.Value)
                {
                    dgv.Rows[rowIndex].Height = 5;
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(64,64,64);
                }
                else
                {
                    dgv.Rows[rowIndex].Height = 50;

                }
            }
            else if (dgv.Columns[colIndex].Name == "TOTAL BAG(S)")
            {
                int bagQty = int.TryParse(dgv.Rows[rowIndex].Cells["TOTAL BAG(S)"].Value.ToString(), out bagQty) ? bagQty : -1;
                int size = int.TryParse(dgv.Rows[rowIndex].Cells["SIZE"].Value.ToString(), out size) ? size : 0;
                int stockLevel = int.TryParse(dgv.Rows[rowIndex].Cells["MAX STOCK LEVEL"].Value.ToString(), out stockLevel) ? stockLevel : 0;

                if (bagQty != -1)
                {
                    //if (size == 20)
                    //{
                    //    stockLevel = text.StockLevel_20;
                    //}
                    //else if (size == 25)
                    //{
                    //    stockLevel = text.StockLevel_25;
                    //}
                    //else if (size == 32)
                    //{
                    //    stockLevel = text.StockLevel_32;
                    //}
                    //else if (size == 50)
                    //{
                    //    stockLevel = text.StockLevel_50;
                    //}
                    //else if (size == 63)
                    //{
                    //    stockLevel = text.StockLevel_63;
                    //}

                    if (bagQty >= stockLevel)
                    {
                        dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(0, 184, 148);//green
                    }
                    else if (bagQty < stockLevel / 2)
                    {
                        dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(255, 118, 117);//red
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(253, 203, 110);//yellow
                    }
                }
               else
                {
                    if (dgv.Rows[rowIndex].Cells["TYPE"].Value == DBNull.Value)
                    {
                        dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(64, 64, 64);
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.White;

                    }
                }

            }

            dgv.ResumeLayout();
        }

        private void dgvUnique_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvUnique;
            int rowIndex = dgv.CurrentCell.RowIndex;
            //int col = dgv.CurrentCell.ColumnIndex;

            string size = dgv.Rows[rowIndex].Cells["SIZE"].Value.ToString();

            bool firstDisplaySet = false;
            //int firstFoundRowIndex = -1;

            foreach(DataGridViewRow row in dgvCommon.Rows)
            {
                string commonSize = row.Cells["SIZE"].Value.ToString();

                if(commonSize == size)
                {
                    if(!firstDisplaySet)
                    {
                        //firstFoundRowIndex = row.Index;
                        dgvCommon.FirstDisplayedScrollingRowIndex = row.Index;
                        //dgvCommon.CurrentCell = row.Cells[0];
                        firstDisplaySet = true;
                    }
                    
                    row.Selected = true;
                }
                else
                {
                    row.Selected = false;
                }
            }

            //if(firstFoundRowIndex != -1)
            //{
            //    dgvCommon.FirstDisplayedScrollingRowIndex = firstFoundRowIndex;
            //    //dgvCommon.CurrentCell = dgvCommon.Rows[firstFoundRowIndex].Cells[0];
            //}
        }

        #region export to excel

        static void OpenCSVWithExcel(string path)
        {
            var ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Workbooks.OpenText(path, Comma: true);

            ExcelApp.Visible = true;
        }

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            fileName = "SPP Inventory Report_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            return fileName;
        }

        private void copyDGVtoClipboard(DataGridView dgv)
        {
            dgv.SelectAll();
            DataObject dataObj = dgv.GetClipboardContent();
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

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string path2 = @"D:\StockAssistant\Document\SPP Inventory Report";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "SPP Inventory Report_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);
                    string path = Path.GetFullPath(sfd.FileName);
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = Missing.Value;
                    Excel.Application xlexcel = new Excel.Application
                    {
                        PrintCommunication = false,
                        ScreenUpdating = false,
                        DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                    };
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    //Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName,
                        XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue,
                        XlSaveAsAccessMode.xlExclusive,
                        misValue, misValue, misValue, misValue, misValue);

                    InsertAllDataToSheet(path, sfd.FileName);
                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dgvCommon.ClearSelection();
                    dgvUnique.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void InsertAllDataToSheet(string path, string fileName)
        {
            DataGridView dgv = dgvCommon;
            DataTable dt = (DataTable)dgv.DataSource;

            Excel.Application excelApp = new Excel.Application
            {
                Visible = true
            };

            Workbook g_Workbook = excelApp.Workbooks.Open(
               path,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing);

            object misValue = Missing.Value;

            if (dt.Rows.Count > 0)//if datagridview have data
            {
                Worksheet xlWorkSheet = null;

                int count = g_Workbook.Worksheets.Count;

                xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                        g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

                xlWorkSheet.Name = "COMMON PART";

                xlWorkSheet.PageSetup.LeftHeader = "&\"Courier New\"&8 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                xlWorkSheet.PageSetup.CenterHeader = "&\"Courier New\"&12 SPP INVENTORY REPORT: COMMON PART";
                xlWorkSheet.PageSetup.RightHeader = "&\"Courier New\"&8 PG -&P";
                xlWorkSheet.PageSetup.CenterFooter = "&\"Courier New\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                xlWorkSheet.PageSetup.CenterHorizontally = true;
                xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                xlWorkSheet.PageSetup.Zoom = false;

                xlWorkSheet.PageSetup.FitToPagesWide = 1;
                xlWorkSheet.PageSetup.FitToPagesTall = false;

                double pointToCMRate = 0.035;
                xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
                xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
                xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
                xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                xlWorkSheet.PageSetup.LeftMargin = 0.7 / pointToCMRate;
                xlWorkSheet.PageSetup.RightMargin = 0.7 / pointToCMRate;

                xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



                // Paste clipboard results to worksheet range
                copyDGVtoClipboard(dgvCommon);

                xlWorkSheet.Select();
                Range CR = (Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                #region old format setting

                Range tRange = xlWorkSheet.UsedRange;
                tRange.Font.Size = 11;
                tRange.RowHeight = 30;
                tRange.VerticalAlignment = XlHAlign.xlHAlignCenter;
                tRange.Font.Name = "Courier New";

                tRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);

                //tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                //tRange.Borders.Weight = XlBorderWeight.xlThin;

                //Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];
                Range FirstRow = xlWorkSheet.get_Range("a1:d1").Cells;
                FirstRow.WrapText = true;
                FirstRow.Font.Size = 8;

                FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
                FirstRow.RowHeight = 20;
                FirstRow.Interior.Color = Color.WhiteSmoke;
                FirstRow.Borders.LineStyle = XlLineStyle.xlContinuous;
                FirstRow.Borders.Weight = XlBorderWeight.xlThin;

                //tRange.EntireColumn.AutoFit();

                //DataTable dt = (DataTable)dgv.DataSource;

              
                int TypeIndex = dgv.Columns["TYPE"].Index;
                int SizeIndex = dgv.Columns["SIZE"].Index;
                int UnitIndex = dgv.Columns["UNIT"].Index;
                int StockIndex = dgv.Columns["STOCK(PCS)"].Index;


               
                xlWorkSheet.Cells[1, TypeIndex + 1].ColumnWidth = 25;
                xlWorkSheet.Cells[1, SizeIndex + 1].ColumnWidth = 6;
                xlWorkSheet.Cells[1, UnitIndex + 1].ColumnWidth = 6;
                xlWorkSheet.Cells[1, StockIndex + 1].ColumnWidth = 25;
              

                //xlWorkSheet.Cells[1, StdIndex + 1].Font.Italic = true;
                //rangeForecast1.Font.Italic = true;

                //DataTable dt_Source = (DataTable)dgv.DataSource;

                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {
                   
                    Range rangeStock = (Range)xlWorkSheet.Cells[j + 2, StockIndex + 1];
                    Range rangeType = (Range)xlWorkSheet.Cells[j + 2, TypeIndex + 1];

                    rangeStock.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    rangeType.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    System.Drawing.Color color = System.Drawing.Color.Black;
                    rangeStock.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                    rangeStock.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                    rangeType.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                    rangeType.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                 
          

                }

                #endregion

                releaseObject(xlWorkSheet);
                Clipboard.Clear();

                dgvCommon.ClearSelection();
            }


            dgv = dgvUnique;
            dt = (DataTable)dgv.DataSource;

            #region divide by type
            DataTable dt_type = dt.Clone();

            string previousType = null;
            string currentType = null;

            foreach (DataRow row in dt.Rows)
            {
                //get type. if previous type is null, set previous = current type
                currentType = row["TYPE"].ToString();

                if (string.IsNullOrEmpty(currentType))
                {
                    currentType = previousType;
                }

                if (previousType == null || previousType == currentType)
                {
                    previousType = currentType;
                    //move data to new table
                    dt_type.ImportRow(row);
                    
                }
                else if (previousType != currentType)
                {
                    DataRow lastRow = dt_type.Rows[dt_type.Rows.Count - 1];

                    if(string.IsNullOrEmpty(lastRow["CODE"].ToString()))
                    {
                        dt_type.Rows.RemoveAt(dt_type.Rows.Count - 1);
                    }
                    

                    dgvUnique.DataSource = dt_type;
                    MoveUniqueDataToSheet(g_Workbook, previousType);
                    previousType = currentType;
                    dt_type = dt.Clone();

                    if (!string.IsNullOrEmpty(row["TYPE"].ToString()))
                    {
                        dt_type.ImportRow(row);
                    }
                }


            }

            if (dt_type.Rows.Count > 0)
            {
                dgvUnique.DataSource = dt_type;

                MoveUniqueDataToSheet(g_Workbook, previousType);
            }

            dgvUnique.DataSource = dt;
            #endregion

            g_Workbook.Worksheets.Item[1].Delete();
            g_Workbook.Save();
            releaseObject(g_Workbook);
            //frmLoading.CloseForm();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void MoveUniqueDataToSheet(Workbook g_Workbook, string TypeName)
        {
            DataGridView dgv = dgvUnique;
            #region move data to sheet
            Worksheet xlWorkSheet = null;

            int count = g_Workbook.Worksheets.Count;

            xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                    g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

            xlWorkSheet.Name = TypeName;

            xlWorkSheet.PageSetup.LeftHeader = "&\"Courier New\"&8 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            xlWorkSheet.PageSetup.CenterHeader = "&\"Courier New\"&12 SPP INVENTORY REPORT: " + TypeName;
            xlWorkSheet.PageSetup.RightHeader = "&\"Courier New\"&8 PG -&P";
            xlWorkSheet.PageSetup.CenterFooter = "&\"Courier New\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

            xlWorkSheet.PageSetup.CenterHorizontally = true;
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;

            xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 1.2 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 1.2 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 0.6 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0.7 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 0.7 / pointToCMRate;

            xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";



            // Paste clipboard results to worksheet range
            copyDGVtoClipboard(dgvUnique);

            xlWorkSheet.Select();
            Range CR = (Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            #region old format setting

            Range tRange = xlWorkSheet.UsedRange;
            tRange.Font.Size = 11;
            tRange.RowHeight = 30;
            tRange.VerticalAlignment = XlHAlign.xlHAlignCenter;
            tRange.Font.Name = "Courier New";

            tRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);

            //tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
            //tRange.Borders.Weight = XlBorderWeight.xlThin;

            //Range FirstRow = (Range)xlWorkSheet.Application.Rows[1, Type.Missing];
            Range FirstRow = xlWorkSheet.get_Range("a1:i1").Cells;
            FirstRow.WrapText = true;
            FirstRow.Font.Size = 8;

            FirstRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            FirstRow.VerticalAlignment = XlHAlign.xlHAlignCenter;
            FirstRow.RowHeight = 20;
            FirstRow.Interior.Color = Color.WhiteSmoke;
            FirstRow.Borders.LineStyle = XlLineStyle.xlContinuous;
            FirstRow.Borders.Weight = XlBorderWeight.xlThin;

            //tRange.EntireColumn.AutoFit();

            //DataTable dt = (DataTable)dgv.DataSource;

            int CatIndex = dgv.Columns["CATEGORY"].Index;
            int TypeIndex = dgv.Columns["TYPE"].Index;
            int SizeIndex = dgv.Columns["SIZE"].Index;
            int UnitIndex = dgv.Columns["UNIT"].Index;
            int CodeIndex = dgv.Columns["CODE"].Index;
            int StockIndex = dgv.Columns["STOCK(PCS)"].Index;
            int StdIndex = dgv.Columns["PCS/BAG"].Index;
            int BagIndex = dgv.Columns["TOTAL BAG(S)"].Index;
            int StockLevelIndex = dgv.Columns["MAX STOCK LEVEL"].Index;

            xlWorkSheet.Cells[1, CatIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, TypeIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, SizeIndex + 1].ColumnWidth = 3;
            xlWorkSheet.Cells[1, UnitIndex + 1].ColumnWidth = 3;
            xlWorkSheet.Cells[1, CodeIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, StockIndex + 1].ColumnWidth = 10;
            xlWorkSheet.Cells[1, StdIndex + 1].ColumnWidth = 10;
            xlWorkSheet.Cells[1, BagIndex + 1].ColumnWidth = 10;

            xlWorkSheet.Cells[1, StdIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Cells[1, StockLevelIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Cells[1, BagIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;

            //xlWorkSheet.Cells[1, StdIndex + 1].Font.Italic = true;
            //rangeForecast1.Font.Italic = true;

            DataTable dt = (DataTable)dgv.DataSource;

            for (int j = 0; j <= dt.Rows.Count - 1; j++)
            {
                Range rangeStd = (Range)xlWorkSheet.Cells[j + 2, StdIndex + 1];
                rangeStd.Font.Italic = true;
                rangeStd.Font.Size = 8;
                rangeStd.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                Range rangeStockLevel = (Range)xlWorkSheet.Cells[j + 2, StockLevelIndex + 1];
                rangeStockLevel.Font.Italic = true;
                rangeStockLevel.Font.Size = 8;
                rangeStockLevel.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                Range rangeBag = (Range)xlWorkSheet.Cells[j + 2, BagIndex + 1];
                rangeBag.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                Range rangeCode = (Range)xlWorkSheet.Cells[j + 2, CodeIndex + 1];
                rangeCode.HorizontalAlignment = XlHAlign.xlHAlignRight;

                Range rangeUnit = (Range)xlWorkSheet.Cells[j + 2, UnitIndex + 1];
                Range rangeCat = (Range)xlWorkSheet.Cells[j + 2, CatIndex + 1];

                System.Drawing.Color color = System.Drawing.Color.Black;
                rangeStd.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeStd.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                rangeBag.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeBag.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                rangeCode.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeCode.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                rangeUnit.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeCat.Borders[XlBordersIndex.xlEdgeRight].Color = color;

                //Range rangeRow = (Range)xlWorkSheet.Rows[j + 2];
                ////rangeRow.Rows.RowHeight = 40;
                //rangeRow.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;

                //rangeIndex.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                //rangeWeight.Cells.HorizontalAlignment = XlVAlign.xlVAlignCenter;

                ////color bal font
                //Range rangeBal1 = (Range)xlWorkSheet.Cells[j + 2, bal1Index + 1];
                //rangeBal1.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal1Index].InheritedStyle.ForeColor);

                //Range rangeBal2 = (Range)xlWorkSheet.Cells[j + 2, bal2Index + 1];
                //rangeBal2.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal2Index].InheritedStyle.ForeColor);

                //string itemCode = dgv.Rows[j].Cells[headerPartCode].Value.ToString();

                ////color empty space
                //if (string.IsNullOrEmpty(itemCode))
                //{
                //    rangeRow.Rows.RowHeight = 2;
                //    Range rng = xlWorkSheet.Range[xlWorkSheet.Cells[j + 2, 1], xlWorkSheet.Cells[j + 2, xlWorkSheet.UsedRange.Columns.Count]];
                //    //Range rng2 = xlWorkSheet.Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, xlWorkSheet.UsedRange.Columns.Count]);
                //    //Range changeRowBackColor = xlWorkSheet.get_Range(i+2, last);
                //    rng.Interior.Color = Color.Black;
                //}
                //else
                //{
                //    rangeName.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[nameIndex].InheritedStyle.ForeColor);
                //    rangeCode.Font.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[codeIndex].InheritedStyle.ForeColor);

                //    rangeName.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[nameIndex].InheritedStyle.BackColor);
                //    rangeCode.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[codeIndex].InheritedStyle.BackColor);

                //    rangeStock.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[stockIndex].InheritedStyle.BackColor);
                //    rangeEstimate.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[estimateIndex].InheritedStyle.BackColor);

                //    rangeForecast1.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[forecast1Index].InheritedStyle.BackColor);
                //    rangeForecast2.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[forecast2Index].InheritedStyle.BackColor);
                //    rangeForecast3.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[forecast3Index].InheritedStyle.BackColor);

                //    rangeBal1.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal1Index].InheritedStyle.BackColor);
                //    rangeBal2.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[bal2Index].InheritedStyle.BackColor);

                //    rangeOut.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[outIndex].InheritedStyle.BackColor);
                //    rangeOutStd.Interior.Color = ColorTranslator.ToOle(dgv.Rows[j].Cells[outStdIndex].InheritedStyle.BackColor);
                //}

                //string parentColor = dt.Rows[j][headerParentColor].ToString();
                //string type = dt.Rows[j][headerType].ToString();
                //string balType = dt.Rows[j][headerBalType].ToString();

                ////change parent color
                //if (parentColor.Equals(AssemblyMarking))
                //{
                //    rangeName.Font.Underline = true;
                //    rangeCode.Font.Underline = true;
                //}
                //else if (parentColor.Equals(ProductionMarking))
                //{
                //    rangeName.Font.Underline = true;
                //    rangeCode.Font.Underline = true;
                //}
                //else if (parentColor.Equals(ProductionAndAssemblyMarking))
                //{
                //    rangeName.Font.Underline = true;
                //    rangeCode.Font.Underline = true;
                //}

                //if (type.Equals(typeChild))
                //{
                //    rangeForecast1.Font.Italic = true;
                //    rangeForecast2.Font.Italic = true;
                //    rangeForecast3.Font.Italic = true;
                //}

                //if (balType.Equals(balType_Total))
                //{
                //    rangeBal1.Font.Bold = true;
                //    rangeBal1.Font.Italic = true;
                //    rangeBal1.Font.Underline = true;

                //    rangeBal2.Font.Bold = true;
                //    rangeBal2.Font.Italic = true;
                //    rangeBal2.Font.Underline = true;

                //}
                //else
                //{
                //    rangeBal1.Font.Bold = true;
                //    rangeBal2.Font.Bold = true;
                //}

            }

            #endregion

            releaseObject(xlWorkSheet);
            Clipboard.Clear();

            dgvUnique.ClearSelection();
            #endregion
        }

        #endregion

        private void btnFilterApply_Click(object sender, EventArgs e)
        {

        }
    }
}
