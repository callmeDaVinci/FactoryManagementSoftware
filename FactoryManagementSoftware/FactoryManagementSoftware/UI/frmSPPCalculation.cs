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
    public partial class frmSPPCalculation : Form
    {
        public frmSPPCalculation()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvTargetItem, true);
            tool.DoubleBuffered(dgvMatPrepareList, true);

            dt_Source = NewSourceTable();
        }

        SPPDataDAL dalData = new SPPDataDAL();
        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        userDAL dalUser = new userDAL();
        joinDAL dalJoin = new joinDAL();

        DataTable dt_Source;
  
        private readonly string header_Size = "SIZE";
        private readonly string header_Unit = "UNIT";
        private readonly string header_Type = "TYPE";
        private readonly string header_Category = "CATEGORY";
        private readonly string header_Code = "CODE";
        private readonly string header_Stock = "STOCK";
        private readonly string header_RequiredQty = "REQUIRED QTY";
        private readonly string header_Target_Qty = "TARGET QTY";
        private readonly string header_Target_Pcs = "TARGET PCS";
        private readonly string header_Target_Bag = "TARGET BAG";

        private DataTable NewSourceTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_Target_Pcs, typeof(int));
            dt.Columns.Add(header_Target_Bag, typeof(int));

            return dt;
        }

        private DataTable NewTargetTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_Target_Qty, typeof(string));

            return dt;
        }

        private DataTable NewMaterialTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Category, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_Stock, typeof(int));
            dt.Columns.Add(header_RequiredQty, typeof(int));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[header_Size].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            if (dgv == dgvTargetItem)
            {
                dgv.Columns[header_Code].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
               
                dgv.Columns[header_Code].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Target_Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if(dgv == dgvMatPrepareList)
            {
                dgv.Columns[header_Stock].DefaultCellStyle.Font= new Font("Segoe UI", 8F, FontStyle.Italic);
                dgv.Columns[header_RequiredQty].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                dgv.Columns[header_Code].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[header_Code].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Category].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[header_RequiredQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
            frmSPPAddItem frm = new frmSPPAddItem
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();

            if(frmSPPAddItem.itemAdded)
            {
                //add data to datarow
                //add datarow to table

                DataRow dt_Row = dt_Source.NewRow();

                dt_Row[header_Size] = frmSPPAddItem.item_Size;
                dt_Row[header_Unit] = "MM";
                dt_Row[header_Type] = frmSPPAddItem.item_Type;
                dt_Row[header_Code] = frmSPPAddItem.item_Code;
                dt_Row[header_Target_Pcs] = frmSPPAddItem.item_Target_Pcs;
                dt_Row[header_Target_Bag] = frmSPPAddItem.item_Target_Bags;

                dt_Source.Rows.Add(dt_Row);

                //add data to target list
                ShowTargetList();
                //calculate material qty
                ShowMaterialList();
                frmSPPAddItem.itemAdded = false;
            }
        }

        private void ShowTargetList()
        {
            dt_Source.DefaultView.Sort = header_Size+" ASC, " + header_Type + " ASC, " + header_Code + " ASC";
            dt_Source = dt_Source.DefaultView.ToTable();

            string lastItem = null;

            for (int i = 0; i < dt_Source.Rows.Count; i++)
            {
                string currentItem = dt_Source.Rows[i][header_Code].ToString();

                if(lastItem == null)
                {
                    lastItem = currentItem;
                }
                else if(lastItem == currentItem && i != 0)
                {
                    int lastTargetPcs = int.TryParse(dt_Source.Rows[i - 1][header_Target_Pcs].ToString(), out lastTargetPcs) ? lastTargetPcs : 0;
                    int lastTargetBag = int.TryParse(dt_Source.Rows[i - 1][header_Target_Bag].ToString(), out lastTargetBag) ? lastTargetBag : 0;

                    int currentTargetPcs = int.TryParse(dt_Source.Rows[i][header_Target_Pcs].ToString(), out currentTargetPcs) ? currentTargetPcs : 0;
                    int currentTargetBag = int.TryParse(dt_Source.Rows[i][header_Target_Bag].ToString(), out currentTargetBag) ? currentTargetBag : 0;

                    dt_Source.Rows[i - 1][header_Target_Pcs] = lastTargetPcs + currentTargetPcs;
                    dt_Source.Rows[i - 1][header_Target_Bag] = lastTargetBag + currentTargetBag;

                    dt_Source.Rows.RemoveAt(i);
                    i -= 1;
                }
                else
                {
                    lastItem = currentItem;
                }
            }

            DataTable dt_Target = NewTargetTable();
            DataRow dt_Row;

            foreach (DataRow row in dt_Source.Rows)
            {
                dt_Row = dt_Target.NewRow();

                dt_Row[header_Size] = row[header_Size].ToString();
                dt_Row[header_Unit] = row[header_Unit].ToString();
                dt_Row[header_Type] = row[header_Type].ToString();
                dt_Row[header_Code] = row[header_Code].ToString();
                dt_Row[header_Target_Qty] = row[header_Target_Pcs].ToString() + " (" + row[header_Target_Bag].ToString()+" BAGS)";

                dt_Target.Rows.Add(dt_Row);
            }

            dgvTargetItem.DataSource = dt_Target;
            DgvUIEdit(dgvTargetItem);
            dgvTargetItem.ClearSelection();
        }

        private DataRow GetItemInfo(string itemCode, DataTable dt)
        {

            foreach(DataRow row in dt.Rows)
            {
                if(row["CODE"].ToString() == itemCode)
                {
                    return row;
                }
            }

            return null;
        }

        private void ShowMaterialList()
        {
            //loop dt source to get parent code,get target pcs
            string parentItemCode = null;
            string childItemCode;

            DataTable dt_Mat = NewMaterialTable();
            DataTable dt_NonReadyGoods = dalItem.SPPNonReadyGoodsSelect();

            foreach (DataRow row in dt_Source.Rows)
            {
                parentItemCode = row[header_Code].ToString();
                int qty = Convert.ToInt32(row[header_Target_Pcs].ToString());

                DataTable dtSPP = dalJoin.loadChildList(parentItemCode);

                foreach (DataRow SPP in dtSPP.Rows)
                {
                    parentItemCode = SPP["join_child_code"].ToString();
                }

                if (parentItemCode != null)
                {
                    DataTable dtJoin = dalJoin.loadChildList(parentItemCode);

                    if (dtJoin.Rows.Count > 0)
                    {
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            float childQty = qty;

                            float joinQty = float.TryParse(Join["join_qty"].ToString(), out float i) ? Convert.ToSingle(Join["join_qty"].ToString()) : 1;

                            childQty = childQty * joinQty;

                            childItemCode = Join["join_child_code"].ToString();

                            DataRow nonReadyGoods_row = GetItemInfo(childItemCode, dt_NonReadyGoods);

                            if (nonReadyGoods_row != null)
                            {
                                DataRow dt_Row = dt_Mat.NewRow();

                                dt_Row[header_Size] = nonReadyGoods_row["SIZE"].ToString();
                                dt_Row[header_Unit] = "MM";
                                dt_Row[header_Category] = nonReadyGoods_row["CATEGORY"].ToString();
                                dt_Row[header_Type] = nonReadyGoods_row["TYPE"].ToString();
                                dt_Row[header_Code] = nonReadyGoods_row["CODE"].ToString();
                                dt_Row[header_Stock] = nonReadyGoods_row["QUANTITY"].ToString();
                                dt_Row[header_RequiredQty] = childQty;

                                dt_Mat.Rows.Add(dt_Row);
                            }
                        }
                    }
                }
            }

            dt_Mat.DefaultView.Sort =  header_Size + " ASC, " + header_Category + " ASC, " + header_Type + " ASC, " + header_Code + " ASC";
            dt_Mat = dt_Mat.DefaultView.ToTable();

            string lastItem = null;

            for (int i = 0; i < dt_Mat.Rows.Count; i++)
            {
                string currentItem = dt_Mat.Rows[i][header_Code].ToString();

                if (lastItem == null)
                {
                    lastItem = currentItem;
                }
                else if (lastItem == currentItem && i != 0)
                {
                    int lastRequiredQty = int.TryParse(dt_Mat.Rows[i - 1][header_RequiredQty].ToString(), out lastRequiredQty) ? lastRequiredQty : 0;

                    int currentRequiredQty = int.TryParse(dt_Mat.Rows[i][header_RequiredQty].ToString(), out currentRequiredQty) ? currentRequiredQty : 0;

                    dt_Mat.Rows[i - 1][header_RequiredQty] = lastRequiredQty + currentRequiredQty;

                    dt_Mat.Rows.RemoveAt(i);
                    i -= 1;
                }
                else
                {
                    lastItem = currentItem;
                }
            }

            dgvMatPrepareList.DataSource = dt_Mat;
            DgvUIEdit(dgvMatPrepareList);
            dgvMatPrepareList.ClearSelection();

        }

        private void dgvTargetItem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            DataGridView dgv = dgvTargetItem;

            dgv.SuspendLayout();
            int n = e.RowIndex;

            if (dgv.Columns[e.ColumnIndex].Name == "SIZE")
            {
                //dgv.Rows[n].Height = 50;

            }



            dgv.ResumeLayout();
        }

        private void dgvTargetItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.Visible = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //get item info
            DataTable dt = (DataTable)dgvTargetItem.DataSource;
            int rowIndex = dgvTargetItem.CurrentRow.Index;

            //string code, string type, string size, string unit

            string code = dt.Rows[rowIndex][header_Code].ToString();
            string type = dt.Rows[rowIndex][header_Type].ToString();
            string size = dt.Rows[rowIndex][header_Size].ToString();
            string unit = dt.Rows[rowIndex][header_Unit].ToString();
            string targetPcs = dt_Source.Rows[rowIndex][header_Target_Pcs].ToString();
            if (rowIndex >= 0)
            {
                frmSPPAddItem frm = new frmSPPAddItem(code, type, size, unit, targetPcs)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };



                frm.ShowDialog();

                if (frmSPPAddItem.itemEdited)
                {
                    btnEdit.Visible = false;
                    //add data to datarow
                    //add datarow to table
                    dt_Source.Rows.RemoveAt(rowIndex);
                    DataRow dt_Row = dt_Source.NewRow();

                    dt_Row[header_Size] = frmSPPAddItem.item_Size;
                    dt_Row[header_Unit] = "MM";
                    dt_Row[header_Type] = frmSPPAddItem.item_Type;
                    dt_Row[header_Code] = frmSPPAddItem.item_Code;
                    dt_Row[header_Target_Pcs] = frmSPPAddItem.item_Target_Pcs;
                    dt_Row[header_Target_Bag] = frmSPPAddItem.item_Target_Bags;

                    dt_Source.Rows.Add(dt_Row);

                    //add data to target list
                    ShowTargetList();
                    //calculate material qty
                    ShowMaterialList();
                    frmSPPAddItem.itemEdited = false;
                }
                else if(frmSPPAddItem.itemRemoved)
                {
                    btnEdit.Visible = false;
                    dt_Source.Rows.RemoveAt(rowIndex);
                    dgvTargetItem.DataSource = dt_Source;

                   

                    //add data to target list
                    ShowTargetList();
                    //calculate material qty
                    ShowMaterialList();
                    frmSPPAddItem.itemRemoved = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a row!");
            }

          
        }

        private void dgvTargetItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //get item info
            DataTable dt = (DataTable)dgvTargetItem.DataSource;
            int rowIndex = dgvTargetItem.CurrentRow.Index;

            //string code, string type, string size, string unit

            string code = dt.Rows[rowIndex][header_Code].ToString();
            string type = dt.Rows[rowIndex][header_Type].ToString();
            string size = dt.Rows[rowIndex][header_Size].ToString();
            string unit = dt.Rows[rowIndex][header_Unit].ToString();
            string targetPcs = dt_Source.Rows[rowIndex][header_Target_Pcs].ToString();

            if (rowIndex >= 0)
            {
                frmSPPAddItem frm = new frmSPPAddItem(code, type, size, unit, targetPcs)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };



                frm.ShowDialog();

                if (frmSPPAddItem.itemEdited)
                {
                    btnEdit.Visible = false;
                    //add data to datarow
                    //add datarow to table
                    dt_Source.Rows.RemoveAt(rowIndex);
                    DataRow dt_Row = dt_Source.NewRow();

                    dt_Row[header_Size] = frmSPPAddItem.item_Size;
                    dt_Row[header_Unit] = "MM";
                    dt_Row[header_Type] = frmSPPAddItem.item_Type;
                    dt_Row[header_Code] = frmSPPAddItem.item_Code;
                    dt_Row[header_Target_Pcs] = frmSPPAddItem.item_Target_Pcs;
                    dt_Row[header_Target_Bag] = frmSPPAddItem.item_Target_Bags;

                    dt_Source.Rows.Add(dt_Row);

                    //add data to target list
                    ShowTargetList();
                    //calculate material qty
                    ShowMaterialList();
                    frmSPPAddItem.itemEdited = false;
                }
                else if (frmSPPAddItem.itemRemoved)
                {
                    btnEdit.Visible = false;
                    dt_Source.Rows.RemoveAt(rowIndex);
                    dgvTargetItem.DataSource = dt_Source;



                    //add data to target list
                    ShowTargetList();
                    //calculate material qty
                    ShowMaterialList();
                    frmSPPAddItem.itemRemoved = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a row!");
            }
        }

        private void dgvMatPrepareList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMatPrepareList;

            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if(dgv.Columns[colIndex].Name == header_RequiredQty)
            {
                int stock = int.TryParse(dgv.Rows[rowIndex].Cells[header_Stock].Value.ToString(), out stock) ? stock : 0;
                int requiredQty = int.TryParse(dgv.Rows[rowIndex].Cells[header_RequiredQty].Value.ToString(), out requiredQty) ? requiredQty : 0;
                int size = int.TryParse(dgv.Rows[rowIndex].Cells[header_Size].Value.ToString(), out size) ? size : 0;
                int stockLevel = 0;

                if (requiredQty > stock)
                {
                    dgv.Rows[rowIndex].Cells[header_RequiredQty].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[rowIndex].Cells[header_RequiredQty].Style.ForeColor = Color.Black;
                }

                if(size == 20)
                {
                    stockLevel = text.StockLevel_20;
                }
                else if (size == 25)
                {
                    stockLevel = text.StockLevel_25;
                }
                else if (size == 32)
                {
                    stockLevel = text.StockLevel_32;
                }
                else if (size == 50)
                {
                    stockLevel = text.StockLevel_50;
                }
                else if (size == 63)
                {
                    stockLevel = text.StockLevel_63;
                }


            }
        }

        #region export to excel

        static void OpenCSVWithExcel(string path)
        {
            var ExcelApp = new Excel.Application();
            ExcelApp.Workbooks.OpenText(path, Comma: true);

            ExcelApp.Visible = true;
        }

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            fileName = "SPP Material Prepare List_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
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
                string path2 = @"D:\StockAssistant\Document\SPP Material Prepare List";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "SPP Material Prepare List_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

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
                    dgvTargetItem.ClearSelection();
                    dgvMatPrepareList.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void InsertAllDataToSheet(string path, string fileName)
        {
            DataGridView dgv = dgvTargetItem;
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

                xlWorkSheet.Name = "TARGET LIST";

                xlWorkSheet.PageSetup.LeftHeader = "&\"Courier New\"&8 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                xlWorkSheet.PageSetup.CenterHeader = "&\"Courier New\"&12 SPP TARGET LIST";
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
                copyDGVtoClipboard(dgvTargetItem);

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
                Range FirstRow = xlWorkSheet.get_Range("a1:e1").Cells;
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


                int TypeIndex = dgv.Columns[header_Type].Index;
                int SizeIndex = dgv.Columns[header_Size].Index;
                int UnitIndex = dgv.Columns[header_Unit].Index;
                int CodeIndex = dgv.Columns[header_Code].Index;
                int TargetIndex = dgv.Columns[header_Target_Qty].Index;


                xlWorkSheet.Cells[1, SizeIndex + 1].ColumnWidth = 6;
                xlWorkSheet.Cells[1, UnitIndex + 1].ColumnWidth = 6;
                xlWorkSheet.Cells[1, TypeIndex + 1].ColumnWidth = 25;
                xlWorkSheet.Cells[1, CodeIndex + 1].ColumnWidth = 25;
                xlWorkSheet.Cells[1, TargetIndex + 1].ColumnWidth = 15;

                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                {

                    Range rangeSize = (Range)xlWorkSheet.Cells[j + 2, SizeIndex + 1];
                    Range rangeUnit = (Range)xlWorkSheet.Cells[j + 2, UnitIndex + 1];
                    Range rangeType = (Range)xlWorkSheet.Cells[j + 2, TypeIndex + 1];
                    Range rangeTarget = (Range)xlWorkSheet.Cells[j + 2, TargetIndex + 1];

                    rangeSize.HorizontalAlignment = XlHAlign.xlHAlignRight;
                    rangeUnit.HorizontalAlignment = XlHAlign.xlHAlignLeft;

                    rangeTarget.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    System.Drawing.Color color = System.Drawing.Color.Black;
                    rangeType.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                    rangeType.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                    rangeTarget.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                    rangeTarget.Borders[XlBordersIndex.xlEdgeLeft].Color = color;



                }

                #endregion

                releaseObject(xlWorkSheet);
                Clipboard.Clear();

                dgvTargetItem.ClearSelection();
            }


            dgv = dgvMatPrepareList;
            dt = (DataTable)dgv.DataSource;

            MoveMaterialPrepareDataToSheet(g_Workbook);

            g_Workbook.Worksheets.Item[1].Delete();
            g_Workbook.Save();
            releaseObject(g_Workbook);
            //frmLoading.CloseForm();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void MoveMaterialPrepareDataToSheet(Workbook g_Workbook)
        {
            DataGridView dgv = dgvMatPrepareList;
            #region move data to sheet
            Worksheet xlWorkSheet = null;

            int count = g_Workbook.Worksheets.Count;

            xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                    g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

            xlWorkSheet.Name = "MATERIAL LIST";

            xlWorkSheet.PageSetup.LeftHeader = "&\"Courier New\"&8 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            xlWorkSheet.PageSetup.CenterHeader = "&\"Courier New\"&12 SPP MATERIAL PREPARE LIST";
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
            copyDGVtoClipboard(dgvMatPrepareList);

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
            Range FirstRow = xlWorkSheet.get_Range("a1:g1").Cells;
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

            int SizeIndex = dgv.Columns[header_Size].Index;
            int UnitIndex = dgv.Columns[header_Unit].Index;
            int CatIndex = dgv.Columns[header_Category].Index;
            int TypeIndex = dgv.Columns[header_Type].Index;
            int CodeIndex = dgv.Columns[header_Code].Index;
            int StockIndex = dgv.Columns[header_Stock].Index;
            int RequiredQtyIndex = dgv.Columns[header_RequiredQty].Index;

            xlWorkSheet.Cells[1, SizeIndex + 1].ColumnWidth = 6;
            xlWorkSheet.Cells[1, UnitIndex + 1].ColumnWidth = 6;
            xlWorkSheet.Cells[1, CatIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, TypeIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, CodeIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, StockIndex + 1].ColumnWidth = 10;
            xlWorkSheet.Cells[1, RequiredQtyIndex + 1].ColumnWidth = 10;

            DataTable dt = (DataTable)dgv.DataSource;

            for (int j = 0; j <= dt.Rows.Count - 1; j++)
            {
                Range rangeStock = (Range)xlWorkSheet.Cells[j + 2, StockIndex + 1];
                rangeStock.Font.Italic = true;
                rangeStock.Font.Size = 8;
                rangeStock.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                Range rangeRequiredQty = (Range)xlWorkSheet.Cells[j + 2, RequiredQtyIndex + 1];
                rangeRequiredQty.Font.Bold = true;
                //rangeRequiredQty.Font.Size = 8;
                rangeRequiredQty.HorizontalAlignment = XlHAlign.xlHAlignRight;

                Range rangeSize = (Range)xlWorkSheet.Cells[j + 2, SizeIndex + 1];
                Range rangeUnit = (Range)xlWorkSheet.Cells[j + 2, UnitIndex + 1];

                Range rangeType = (Range)xlWorkSheet.Cells[j + 2, TypeIndex + 1];

                Range rangeCode = (Range)xlWorkSheet.Cells[j + 2, CodeIndex + 1];
                rangeCode.HorizontalAlignment = XlHAlign.xlHAlignLeft;

                Color color = Color.Black;
                rangeStock.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeStock.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                rangeType.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeType.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                rangeCode.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeCode.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                rangeUnit.Borders[XlBordersIndex.xlEdgeRight].Color = color;



            }

            #endregion

            releaseObject(xlWorkSheet);
            Clipboard.Clear();

            dgvMatPrepareList.ClearSelection();
            #endregion
        }

        #endregion
    }
}
