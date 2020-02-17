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
using System.ComponentModel;
using System.Threading;
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSPPPOList : Form
    {
        public frmSPPPOList()
        {
            InitializeComponent();
            btnFilter.Text = text_HideFilter;
            dt_POList = dalSPP.POSelect();
            LoadCustomerList();
        }

        #region variable/object declare

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        facDAL dalFac = new facDAL();
        pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        SPPDataDAL dalSPP = new SPPDataDAL();

        Tool tool = new Tool();
        Text text = new Text();

        readonly string text_ShowFilter = "SHOW FILTER...";
        readonly string text_HideFilter = "HIDE FILTER";
        readonly string header_POCode = "PO CODE";
        readonly string header_PONo = "PO NO";
        readonly string header_PODate = "PO DATE";
        readonly string header_CustomerCode = "CUSTOMER CODE";
        readonly string header_Customer = "CUSTOMER";
        readonly string header_Progress = "PROGRESS";

        readonly string header_Index = "#";
        readonly string header_Size = "SIZE";
        readonly string header_Unit = "UNIT";
        readonly string header_Type = "TYPE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_OrderQty = "ORDER QTY";
        readonly string header_DeliveredQty = "DELIVERED QTY";
        readonly string header_Note = "NOTE";

        private DataTable dt_POList;

        #endregion

        private DataTable NewExcelTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_PONo, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveredQty, typeof(int));
            dt.Columns.Add(header_OrderQty, typeof(int));
            dt.Columns.Add(header_Note, typeof(string));

            return dt;
        }

        private DataTable NewPOTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_CustomerCode, typeof(int));
            dt.Columns.Add(header_Progress, typeof(string));

            return dt;
        }

        private DataTable NewPOItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
         
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveredQty, typeof(int));
            dt.Columns.Add(header_OrderQty, typeof(int));
            dt.Columns.Add(header_Note, typeof(string));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            if (dgv == dgvPOList)
            {
                dgv.Columns[header_POCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_PONo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_PODate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Progress].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_POCode].Visible = false;
                dgv.Columns[header_CustomerCode].Visible = false;
            }

            if (dgv == dgvPOItemList)
            {
                dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Size].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgv.Columns[header_DeliveredQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_OrderQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Note].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

        }

        private void ShowOrHideFilter()
        {
            string filterText = btnFilter.Text;

            if(filterText == text_ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilter();
        }

        private void frmSPPPOList_Load(object sender, EventArgs e)
        {
            ShowOrHideFilter();
            LoadPOList();
        }

        private void LoadCustomerList()
        {
            DataTable dt = dalSPP.CustomerWithoutRemovedDataSelect();

            DataTable locationTable = dt.DefaultView.ToTable(true, dalSPP.FullName);

            DataRow dr;
            dr = locationTable.NewRow();
            dr[dalSPP.FullName] = "ALL";

            locationTable.Rows.InsertAt(dr, 0);

            cmbCustomer.DataSource = locationTable;
            cmbCustomer.DisplayMember = dalSPP.FullName;

        }

        private DataTable AddSpaceToList(DataTable dt)
        {
            string preType = null;
            DataTable dt_Copy = dt.Copy();

            foreach(DataRow row in dt.Rows)
            {
                string type = row[header_Type].ToString();

                if(preType == null)
                {
                    preType = type;
                }
                else if(preType != type)
                {
                    preType = type;
                    dt.Rows.Add();
                    dt.AcceptChanges();
                }
            }

            return dt;
        }

        private void LoadPOList()
        {
            btnEdit.Visible = false;
            dgvPOItemList.DataSource = null;

            DataTable dt = NewPOTable();
            DataRow dt_row;

            int prePOCode = -1;
            foreach(DataRow row in dt_POList.Rows)
            {
                int poCode = int.TryParse(row[dalSPP.POCode].ToString(), out poCode) ? poCode : -1;

                if(prePOCode == -1 || prePOCode != poCode)
                {
                    prePOCode = poCode;
                    int orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                    int deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

                    int progress = deliveredQty / orderQty * 100;

                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                    bool dataMatched = !isRemoved;

                    #region PO TYPE

                    if (progress < 100 && cbInProgressPO.Checked)
                    {
                        dataMatched = dataMatched && true;
                    }
                    else if(progress >= 100 && cbCompletedPO.Checked)
                    {
                        dataMatched = dataMatched && true;
                    }
                    else
                    {
                        dataMatched = false;
                    }

                    #endregion

                    #region Customer Filter

                    string customer = cmbCustomer.Text;

                    if(string.IsNullOrEmpty(customer) || customer == "ALL")
                    {
                        dataMatched = dataMatched && true;
                    }
                    else if(row[dalSPP.FullName].ToString() == customer)
                    {
                        dataMatched = dataMatched && true;
                    }
                    else
                    {
                        dataMatched = false;
                    }

                    #endregion

                    if (dataMatched)
                    {
                        dt_row = dt.NewRow();

                        dt_row[header_POCode] = poCode;
                        dt_row[header_PONo] = row[dalSPP.PONo];
                        dt_row[header_PODate] = Convert.ToDateTime(row[dalSPP.PODate]).Date;
                        dt_row[header_Customer] = row[dalSPP.ShortName];
                        dt_row[header_CustomerCode] = row[dalSPP.CustomerTableCode];
                        dt_row[header_Progress] = progress + "%";
                        dt.Rows.Add(dt_row);
                    }
                   
                }
               
            }

            
            dgvPOList.DataSource = dt;
            DgvUIEdit(dgvPOList);
            dgvPOList.ClearSelection();

        }

        private void btnAddNewPO_Click(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
            frmSPPNewPO frm = new frmSPPNewPO
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            

            frm.ShowDialog();
            dt_POList = dalSPP.POSelect();
            LoadPOList();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            LoadPOList();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvPOItemList.DataSource = null;
        }

        private void cbInProgressPO_CheckedChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvPOItemList.DataSource = null;
        }

        private void cbCompletedPO_CheckedChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvPOItemList.DataSource = null;
        }

        private void dgvPOList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.Visible = true;
           // DataTable dt = (DataTable)dgvPOList.DataSource;
            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvPOList;

            if (rowIndex >= 0)
            {
                string code = dgv.Rows[rowIndex].Cells[header_POCode].Value.ToString();
                ShowPOItem(code);
            }
            else
            {
                dgvPOItemList.DataSource = null;
                btnEdit.Visible = false;
            }
        }

        private void ShowPOItem(string poCode)
        {
            DataTable dt = dalSPP.POSelectWithSizeAndType();

            DataTable dt_POItemList = NewPOItemTable();
            DataRow dt_Row;
            int index = 1;
            string preType = null;
            foreach (DataRow row in dt.Rows)
            {
                if (poCode == row[dalSPP.POCode].ToString())
                {

                    int deliveredQty = row[dalSPP.DeliveredQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.DeliveredQty].ToString());

                    string type = row[dalSPP.TypeName].ToString();

                    if(preType == null)
                    {
                        preType = type;
                    }
                    else if(preType != type)
                    {
                        preType = type;
                        dt_Row = dt_POItemList.NewRow();
                        dt_POItemList.Rows.Add(dt_Row);
                    }

                    dt_Row = dt_POItemList.NewRow();
                    dt_Row[header_Index] = index;
                    dt_Row[header_Size] = row[dalSPP.SizeNumerator];
                    dt_Row[header_Unit] = row[dalSPP.SizeUnit].ToString().ToUpper();
                    dt_Row[header_Type] = type;
                    dt_Row[header_ItemCode] = row[dalSPP.ItemCode];
                    dt_Row[header_DeliveredQty] = deliveredQty;
                    dt_Row[header_OrderQty] = row[dalSPP.POQty];
                    dt_Row[header_Note] = row[dalSPP.PONote];

                    dt_POItemList.Rows.Add(dt_Row);
                    index++;
                }
            }

            //dt_POItemList = AddSpaceToList(dt_POItemList);
            dgvPOItemList.DataSource = dt_POItemList;
            DgvUIEdit(dgvPOItemList);
            dgvPOItemList.ClearSelection();
        }

        private void EditPO()
        {
            //get item info
            DataTable dt = (DataTable)dgvPOList.DataSource;
            int rowIndex = dgvPOList.CurrentRow.Index;

            if (rowIndex >= 0)
            {
                string code = dt.Rows[rowIndex][header_POCode].ToString();

                frmSPPNewPO frm = new frmSPPNewPO(code)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };



                frm.ShowDialog();

                if (frmSPPNewPO.poEdited || frmSPPNewPO.poRemoved)
                {
                    btnEdit.Visible = false;
                    dt_POList = dalSPP.POSelect();
                    LoadPOList();
                }

            }
            else
            {
                MessageBox.Show("Please select a row!");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditPO();
        }

        private void dgvPOList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditPO();
        }

        private void dgvPOList_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = dgvPOList;
            int rowIndex = -1;

            if (dgv.SelectedRows.Count <= 0)
            {
                rowIndex = -1;
            }
            else
            {
                rowIndex = dgv.CurrentRow.Index;
            }

            btnEdit.Visible = true;
            DataTable dt = (DataTable)dgvPOList.DataSource;

            if (rowIndex >= 0)
            {
                string code = dt.Rows[rowIndex][header_POCode].ToString();
                ShowPOItem(code);
            }
            else
            {
                dgvPOItemList.DataSource = null;
                btnEdit.Visible = false;
            }
        }

        private void CombineCustomerData()
        {
            DataGridView dgv = dgvPOList;

            DataTable dt_TargetPO = (DataTable)dgv.DataSource;
            DataTable dt_DBSource = dalSPP.POSelectWithSizeAndType();

            dt_TargetPO.DefaultView.Sort = header_CustomerCode + " ASC, " + header_PODate + " DESC," + header_POCode + " DESC";
            dt_TargetPO = dt_TargetPO.DefaultView.ToTable();

            dt_DBSource.DefaultView.Sort = dalSPP.CustomerTableCode + " ASC, " + dalSPP.PODate + " DESC, " + dalSPP.POCode + " DESC";
            dt_DBSource = dt_DBSource.DefaultView.ToTable();

            int preCustomerCode = -1;

            DataTable dt_Excel = NewExcelTable();
            DataRow dt_Row;

            foreach (DataRow row in dt_TargetPO.Rows)
            {
                int poCode = Convert.ToInt32(row[header_POCode].ToString());

                foreach (DataRow db in dt_DBSource.Rows)
                {
                    int dbCode = Convert.ToInt32(db[dalSPP.POCode].ToString());

                    if (dbCode == poCode)
                    {
                        int customerCode = Convert.ToInt32(db[dalSPP.CustomerTableCode].ToString());
                        string customerShortName = db[dalSPP.ShortName].ToString();

                        if (preCustomerCode == customerCode)
                        {
                            //combine data in same sheet

                        }
                        else
                        {

                            if (preCustomerCode != -1)
                            {
                                dt_Row = dt_Excel.NewRow();
                                dt_Excel.Rows.Add(dt_Row);
                            }

                            preCustomerCode = customerCode;
                            //save data in new sheet
                        }

                        dt_Row = dt_Excel.NewRow();

                        dt_Row[header_POCode] = db[dalSPP.POCode];
                        dt_Row[header_PODate] = db[dalSPP.PODate];
                        dt_Row[header_Size] = db[dalSPP.SizeNumerator];
                        dt_Row[header_Unit] = db[dalSPP.SizeUnit] + " " + customerShortName;
                        dt_Row[header_Type] = db[dalSPP.TypeName];
                        dt_Row[header_ItemCode] = db[dalSPP.ItemCode];
                        dt_Row[header_DeliveredQty] = db[dalSPP.DeliveredQty];
                        dt_Row[header_OrderQty] = db[dalSPP.POQty];
                        dt_Row[header_Note] = db[dalSPP.PONote];

                        dt_Excel.Rows.Add(dt_Row);

                    }
                }
            }
            dgvPOItemList.DataSource = null;
            dgvPOItemList.DataSource = dt_Excel;
            dgvPOItemList.ClearSelection();
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
            fileName = "SPP PO REPORT_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
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
                string path2 = @"D:\StockAssistant\Document\SPP PO Report";
                Directory.CreateDirectory(path2);
                sfd.InitialDirectory = path2;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "SPP PO Report_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

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
                    LoadPOList();
                    dgvPOList.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void InsertAllDataToSheet(string path, string fileName)
        {
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


            DataGridView dgv = dgvPOList;

            DataTable dt_TargetPO = (DataTable)dgv.DataSource;
            DataTable dt_DBSource = dalSPP.POSelectWithSizeAndType();

            dt_TargetPO.DefaultView.Sort = header_CustomerCode + " ASC, " + header_PODate + " DESC," + header_POCode + " DESC";
            dt_TargetPO = dt_TargetPO.DefaultView.ToTable();

            dt_DBSource.DefaultView.Sort = dalSPP.CustomerTableCode + " ASC, " + dalSPP.PODate + " DESC, " + dalSPP.POCode + " DESC";
            dt_DBSource = dt_DBSource.DefaultView.ToTable();

            int preCustomerCode = -1;
            string customerShortName = "";
            DataTable dt_Excel = NewExcelTable();
            DataRow dt_Row;

            int preCode = -1;

            foreach (DataRow row in dt_TargetPO.Rows)
            {
                int poCode = Convert.ToInt32(row[header_POCode].ToString());

                foreach (DataRow db in dt_DBSource.Rows)
                {
                    int dbCode = Convert.ToInt32(db[dalSPP.POCode].ToString());

                    if (dbCode == poCode)
                    {
                        int customerCode = Convert.ToInt32(db[dalSPP.CustomerTableCode].ToString());
                        

                        if (preCustomerCode == customerCode)
                        {
                            //combine data in same sheet
                            if(preCode == -1)
                            {
                                preCode = dbCode;
                            }
                            else if(preCode != dbCode)
                            {
                                dt_Excel.Rows.Add(dt_Excel.NewRow());
                                preCode = dbCode;
                            }
                        }
                        else
                        {
                            if (preCustomerCode != -1)
                            {
                                dgvPOItemList.DataSource = null;
                                dgvPOItemList.DataSource = dt_Excel;
                                dgvPOItemList.ClearSelection();

                                MoveUniqueDataToSheet(g_Workbook, customerShortName);

                                dgvPOItemList.DataSource = null;
                                dt_Excel = NewExcelTable();
                                preCode = -1;

                            }

                            preCustomerCode = customerCode;
                            //save data in new sheet
                        }


                        customerShortName = db[dalSPP.ShortName].ToString();
                        dt_Row = dt_Excel.NewRow();

                        dt_Row[header_PONo] = db[dalSPP.PONo];
                        dt_Row[header_PODate] = Convert.ToDateTime(db[dalSPP.PODate]).Date;
                        dt_Row[header_Size] = db[dalSPP.SizeNumerator];
                        dt_Row[header_Unit] = db[dalSPP.SizeUnit] ;
                        dt_Row[header_Type] = db[dalSPP.TypeName];
                        dt_Row[header_ItemCode] = db[dalSPP.ItemCode];
                        dt_Row[header_DeliveredQty] = db[dalSPP.DeliveredQty];
                        dt_Row[header_OrderQty] = db[dalSPP.POQty];
                        dt_Row[header_Note] = db[dalSPP.PONote];

                        dt_Excel.Rows.Add(dt_Row);
                        

                    }
                }
            }
         
            if(dt_Excel.Rows.Count > 0)
            {
                dgvPOItemList.DataSource = null;
                dgvPOItemList.DataSource = dt_Excel;
                dgvPOItemList.ClearSelection();

                MoveUniqueDataToSheet(g_Workbook, customerShortName);

                dgvPOItemList.DataSource = null;
                dt_Excel = NewExcelTable();
            }

            
            #endregion

            g_Workbook.Worksheets.Item[1].Delete();
            g_Workbook.Save();
            releaseObject(g_Workbook);
            //frmLoading.CloseForm();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void MoveUniqueDataToSheet(Workbook g_Workbook, string CustomerName)
        {
            DataGridView dgv = dgvPOItemList;

            #region move data to sheet

            Worksheet xlWorkSheet = null;

            int count = g_Workbook.Worksheets.Count;

            xlWorkSheet = g_Workbook.Worksheets.Add(Type.Missing,
                    g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

            xlWorkSheet.Name = CustomerName;

            xlWorkSheet.PageSetup.LeftHeader = "&\"Courier New\"&8 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            xlWorkSheet.PageSetup.CenterHeader = "&\"Courier New\"&10 SPP PO REPORT: " + CustomerName;
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
            copyDGVtoClipboard(dgvPOItemList);

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

            tRange.EntireColumn.AutoFit();


            DataTable dt = (DataTable)dgvPOItemList.DataSource;

            int PONOIndex = dgv.Columns[header_PONo].Index;
            int PODateIndex = dgv.Columns[header_PODate].Index;
            int SizeIndex = dgv.Columns[header_Size].Index;
            int UnitIndex = dgv.Columns[header_Unit].Index;
            int TypeIndex = dgv.Columns[header_Type].Index;
            int CodeIndex = dgv.Columns[header_ItemCode].Index;
            int DeliveredQtyIndex = dgv.Columns[header_DeliveredQty].Index;
            int OrderQtyIndex = dgv.Columns[header_OrderQty].Index;
            int NoteIndex = dgv.Columns[header_Note].Index;

            xlWorkSheet.Cells[1, PONOIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, PODateIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, SizeIndex + 1].ColumnWidth = 3;
            xlWorkSheet.Cells[1, UnitIndex + 1].ColumnWidth = 3;
            xlWorkSheet.Cells[1, TypeIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, CodeIndex + 1].ColumnWidth = 12;
            xlWorkSheet.Cells[1, DeliveredQtyIndex + 1].ColumnWidth = 10;
            xlWorkSheet.Cells[1, OrderQtyIndex + 1].ColumnWidth = 10;
            xlWorkSheet.Cells[1, NoteIndex + 1].ColumnWidth = 10;

            xlWorkSheet.Cells[1, PONOIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, PODateIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Cells[1, SizeIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[1, UnitIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, TypeIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, CodeIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[1, DeliveredQtyIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[1, OrderQtyIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[1, NoteIndex + 1].HorizontalAlignment = XlHAlign.xlHAlignLeft;

            for (int j = 0; j <= dt.Rows.Count - 1; j++)
            {
                Range rangePODate = (Range)xlWorkSheet.Cells[j + 2, PODateIndex + 1];
                rangePODate.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                Range rangeType = (Range)xlWorkSheet.Cells[j + 2, TypeIndex + 1];

                Range rangeDelievered = (Range)xlWorkSheet.Cells[j + 2, DeliveredQtyIndex + 1];
                rangeDelievered.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                Range rangeNote = (Range)xlWorkSheet.Cells[j + 2, NoteIndex + 1];


                Color color = Color.Black;
                rangePODate.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangePODate.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                

                rangeType.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeType.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

                rangeDelievered.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeDelievered.Borders[XlBordersIndex.xlEdgeLeft].Color = color;
                

                rangeNote.Borders[XlBordersIndex.xlEdgeRight].Color = color;
                rangeNote.Borders[XlBordersIndex.xlEdgeLeft].Color = color;

            }

            #endregion

            releaseObject(xlWorkSheet);
            Clipboard.Clear();

            dgvPOItemList.ClearSelection();
            #endregion
        }

    
    }
}
