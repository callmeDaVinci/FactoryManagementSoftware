using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Runtime.InteropServices;
using System.Text;

namespace FactoryManagementSoftware.Module
{
    class ExcelTool
    {


        public int Int_TryParse(string value)
        {
            int result;
            int.TryParse(value, out result);
            return result;
        }

        public float Float_TryParse(string value)
        {
            float result;
            float.TryParse(value, out result);
            return result;
        }

        #region export to excel

        private string setFileName(string ReportType)
        {
            Text text = new Text();

            if (ReportType.Equals(text.Report_Type_Production))
            {
                return ReportType + " " + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";
            }
            

            return "UnnamedReport" + " " + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";
        }

        private StringBuilder GetStringBuilder(DataTable dataTable)
        {
            StringBuilder sbData = new StringBuilder();

            // Only return Null if there is no structure.
            if (dataTable.Columns.Count == 0)
                return null;

            foreach (var col in dataTable.Columns)
            {
                if (col == null)
                    sbData.Append(", ");
                else
                    sbData.Append("\"" + col.ToString().Replace("\"", "\"\"") + "\",");
            }

            sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    if (column == null)
                        sbData.Append(",");
                    else
                        sbData.Append("\"" + column.ToString().Replace("\"", "\"\"") + "\",");
                }
                sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);
            }

            return sbData;
        }

        public void ExportToExcel(string ReportType, DataTable dt_Source, DataObject dataObj)
        {
           

            if(dt_Source != null && dt_Source.Rows.Count > 0)
            {
                Tool tool = new Tool();
                Text text = new Text();

                userDAL dalUser = new userDAL();

                SaveFileDialog sfd = new SaveFileDialog();

                string path = @"D:\StockAssistant\Document\" + ReportType;

                Directory.CreateDirectory(path);

                sfd.InitialDirectory = path;

                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = setFileName(ReportType);


                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    frmLoading.ShowLoadingScreen();

                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                    // Copy DataGridView results to clipboard
                    //copyAlltoClipboard(dt_Source);

                    //StringBuilder sbData =  GetStringBuilder(dt_Source);


                    Clipboard.SetDataObject(dataObj);
                    //Clipboard.SetText(sbData);

                    object misValue = Missing.Value;
                    Excel.Application xlexcel = new Excel.Application();
                    xlexcel.PrintCommunication = false;
                    xlexcel.ScreenUpdating = false;
                    xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    xlexcel.Calculation = XlCalculation.xlCalculationManual;
                    Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    //xlWorkSheet.Name = cmbCustomer.Text;

                    #region Save data to Sheet

                    //Header and Footer setup
                    string centerHeader = "&\"Calibri,Bold\"&16" + ReportType;

                    //xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + dtpFrom.Text + ">" + dtpTo.Text;

                    xlWorkSheet.PageSetup.CenterHeader = centerHeader;

                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";

                    xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy") + " Printed By " + dalUser.getUsername(MainDashboard.USER_ID);


                    //Page setup
                    if (ReportType.Equals(text.Report_Type_Production))
                    {
                        double pointToCMRate = 0.035;

                        xlWorkSheet.PageSetup.LeftMargin = 1 / pointToCMRate;
                        xlWorkSheet.PageSetup.RightMargin = 1 / pointToCMRate;

                        xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                        xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                        xlWorkSheet.PageSetup.Zoom = false;
                        xlWorkSheet.PageSetup.CenterHorizontally = true;

                        xlWorkSheet.PageSetup.FitToPagesWide = 1;
                        xlWorkSheet.PageSetup.FitToPagesTall = false;
                        xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";
                    }
                    else
                    {
                        xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                        xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                        xlWorkSheet.PageSetup.Zoom = false;
                        xlWorkSheet.PageSetup.CenterHorizontally = true;
                        xlWorkSheet.PageSetup.LeftMargin = 1;
                        xlWorkSheet.PageSetup.RightMargin = 1;
                        xlWorkSheet.PageSetup.FitToPagesWide = 1;
                        xlWorkSheet.PageSetup.FitToPagesTall = false;
                        xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";
                    }

                    xlexcel.PrintCommunication = true;
                    xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;

                    // Paste clipboard results to worksheet range
                    xlWorkSheet.Select();
                    Range CR = (Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                    //content edit
                    Range tRange = xlWorkSheet.UsedRange;
                    //tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    //tRange.Borders.Weight = XlBorderWeight.xlThin;
                    tRange.Font.Size = 11;
                    tRange.Font.Name = "Calibri";
                    tRange.EntireColumn.AutoFit();
                    tRange.EntireRow.AutoFit();
                    tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);

                    if (ReportType.Equals(text.Report_Type_Production))
                    {
                        tRange.RowHeight = 30;
                        tRange.VerticalAlignment = XlVAlign.xlVAlignCenter;

                        Color color = Color.Gray;
                        tRange.Borders[XlBordersIndex.xlInsideHorizontal].Color = color;
                        tRange.Borders[XlBordersIndex.xlInsideVertical].Color = color;

                        tRange.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                        tRange.Borders[XlBordersIndex.xlEdgeLeft].Color = color;
                        tRange.Borders[XlBordersIndex.xlEdgeTop].Color = color;
                        tRange.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

                        //DataTable dt = (DataTable)dgvMatUsedReport.DataSource;

                        //int TotalColNumber = -1;

                        //if (dt.Columns.Contains(header_TotalOut))
                        //{
                        //    TotalColNumber = dt.Columns[header_TotalOut].Ordinal - 7;
                        //    int lastRowNumber = dt.Rows.Count - 1;

                        //    Range TotalHeader = (Range)xlWorkSheet.Cells[1, TotalColNumber];
                        //    TotalHeader.HorizontalAlignment = XlHAlign.xlHAlignRight;
                        //    TotalHeader.Font.Bold = true;

                        //    Range boldRange = xlWorkSheet.Columns[TotalColNumber];
                        //    boldRange.Font.Bold = true;

                        //    Range firstCol = xlWorkSheet.Columns[1];
                        //    firstCol.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        //}

                        //if (dt.Columns.Contains(header_Bal))
                        //{
                        //    int BalColNumber = dt.Columns[header_Bal].Ordinal - 7;

                        //    Range BalHeader = (Range)xlWorkSheet.Cells[1, BalColNumber];
                        //    BalHeader.HorizontalAlignment = XlHAlign.xlHAlignRight;

                        //    foreach (DataRow row in dt.Rows)
                        //    {
                        //        float bal = float.TryParse(row[header_Bal].ToString(), out float x) ? x : 0;

                        //        int index = dt.Rows.IndexOf(row) + 2;

                        //        Range balRow = (Range)xlWorkSheet.Cells[index, BalColNumber];

                        //        if (bal < 0)
                        //        {
                        //            balRow.Font.Bold = true;
                        //        }
                        //        else
                        //        {
                        //            balRow.Font.Bold = false;
                        //        }
                        //    }

                        //}
                    }

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

                    frmLoading.CloseForm();

                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                        System.Diagnostics.Process.Start(sfd.FileName);
                }

            }
            else
            {
                frmLoading.CloseForm();

                MessageBox.Show("Data not found.");
            }

           

           

        }

        private void copyAlltoClipboard(DataTable dt)
        {
            DataGridView dgv = new DataGridView();
            dgv.DataSource = dt;

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

        #endregion
    }
}
