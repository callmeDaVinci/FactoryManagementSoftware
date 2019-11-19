using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using System.Threading.Tasks;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMachineSchedule : Form
    {
        public frmMachineSchedule()
        {
            InitializeComponent();
            InitializeData();

            userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_TWO)
            {
                btnPlan.Show();
            }
            else
            {
                btnPlan.Hide();
            }

            tool.DoubleBuffered(dgvSchedule, true);
            FilterHideOrShow(filterHide);
        }

        #region Variable/ object setting

        userDAL dalUser = new userDAL();

        planningDAL dalPlanning = new planningDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        planningActionDAL dalPlanningAction = new planningActionDAL();
        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();
        MacDAL dalMac = new MacDAL();
        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();

        habitDAL dalHabit = new habitDAL();

        int userPermission = -1;
        readonly string headerID = "PLAN";
        readonly string headerStartDate = "START";
        readonly string headerEndDate = "ESTIMATE END";
        //readonly string headerProductionDay = "PRODUCTION DAY";
        readonly string headerFactory = "FAC.";
        readonly string headerMachine = "MAC.";
        readonly string headerPartName = "NAME";
        readonly string headerPartCode = "CODE";
        readonly string headerPartCycleTime = "CYCLE TIME";

        readonly string headerProductionPurpose = "PRODUCTION FOR";
        readonly string headerTargetQty = "TARGET QTY";
        readonly string headerAbleProduceQty = "ABLE PRODUCE";
        //readonly string headerProducedQty = "PRODUCED QTY";

        readonly string headerMaterial = "MATERIAL";
        readonly string headerMaterialBag = "BAG";
        readonly string headerRecycle = "RECYCLE (KG)";
        
        //readonly string headerColor = "COLOR";
        readonly string headerColorMaterial = "COLOR MATERIAL";
        //readonly string headerColorMaterialUsage = "USAGE %";
        readonly string headerColorMaterialQty = "QTY (KG)";

        readonly string headerNote = "REMARK";

        readonly string headerStatus = "STATUS";

        private string startDateChecking = null;

        private bool loaded = false;
        private bool ableLoadData = true;
        private bool filterHide = true;
        #endregion

        #region UI Setting

        private void FilterHideOrShow(bool hide)
        {
            if(hide)
            {
                btnFilter.Text = "SHOW FILTER";
                tlpMainSchedule.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                tlpMainSchedule.RowStyles[2] = new RowStyle(SizeType.Percent, 0);
                tlpMainSchedule.RowStyles[4] = new RowStyle(SizeType.Percent, 85f);
                //tlpMainSchedule.RowStyles[5] = new RowStyle(SizeType.Percent, 83f);
            }
            else
            {
                btnFilter.Text = "HIDE FILTER";
                tlpMainSchedule.RowStyles[1] = new RowStyle(SizeType.Percent, 20f);
                tlpMainSchedule.RowStyles[2] = new RowStyle(SizeType.Percent, 7f);
                tlpMainSchedule.RowStyles[4] = new RowStyle(SizeType.Percent, 62f);
            }
        }

        private DataTable NewScheduleTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerID, typeof(int));
            dt.Columns.Add(headerStatus, typeof(string));
            
            dt.Columns.Add(headerFactory, typeof(string));
            dt.Columns.Add(headerMachine, typeof(int));
            dt.Columns.Add(headerStartDate, typeof(DateTime));
            dt.Columns.Add(headerEndDate, typeof(DateTime));
            //dt.Columns.Add(headerProductionDay, typeof(string));

            
            
            dt.Columns.Add(headerPartName, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerPartCycleTime, typeof(int));
            
            dt.Columns.Add(headerTargetQty, typeof(int));
            dt.Columns.Add(headerAbleProduceQty, typeof(int));
            //dt.Columns.Add(headerProducedQty, typeof(int));

            dt.Columns.Add(headerMaterial, typeof(string));
            dt.Columns.Add(headerMaterialBag, typeof(int));
            dt.Columns.Add(headerRecycle, typeof(int));

            //dt.Columns.Add(headerColor, typeof(string));
            dt.Columns.Add(headerColorMaterial, typeof(string));
            //dt.Columns.Add(headerColorMaterialUsage, typeof(int));
            dt.Columns.Add(headerColorMaterialQty, typeof(float));

           
            dt.Columns.Add(headerNote, typeof(string));
            dt.Columns.Add(headerProductionPurpose, typeof(string));


            return dt;
        }

        private void dgvScheduleUIEdit(DataGridView dgv)
        {

            dgv.Columns[headerPartCode].Frozen = true;
            dgv.Columns[headerID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerStartDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerEndDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerProductionDay].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerFactory].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMachine].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerPartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPartCycleTime].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerProductionPurpose].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTargetQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerAbleProduceQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerProducedQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerMaterial].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMaterialBag].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerRecycle].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //dgv.Columns[headerColor].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerColorMaterial].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerColorMaterialUsage].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerColorMaterialQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerNote].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerStatus].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////

            dgv.Columns[headerID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStartDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerEndDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[headerProductionDay].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[headerFactory].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerMachine].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgv.Columns[headerColor].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgv.Columns[headerTargetQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerAbleProduceQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[headerProducedQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStatus].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[headerRecycle].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerMaterialBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerColorMaterialQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPartCycleTime].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //forecolor///////////////////////////////////////////////////////////////////////////////////////////////////

            Color normalColor = Color.Black;
            

            dgv.Columns[headerStartDate].DefaultCellStyle.ForeColor = normalColor;
            dgv.Columns[headerEndDate].DefaultCellStyle.ForeColor = normalColor;
            //dgv.Columns[headerProductionDay].DefaultCellStyle.ForeColor = Color.White;

            dgv.Columns[headerFactory].DefaultCellStyle.ForeColor = normalColor;
            dgv.Columns[headerID].DefaultCellStyle.ForeColor = normalColor;
            //dgv.Columns[headerID].Visible = false;

            //int colorR = 192;
            //int colorG = 0;
            //int colorB= 0;

            ////int colorR = 255;
            ////int colorG = 215;
            ////int colorB = 0;
            //192, 0, 0
            Color importantColor = Color.OrangeRed;
            //importantColor = Color.FromArgb(192, 0, 0);
            dgv.Columns[headerMachine].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[headerPartName].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[headerPartCode].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[headerPartCycleTime].DefaultCellStyle.ForeColor = normalColor;
            dgv.Columns[headerProductionPurpose].DefaultCellStyle.ForeColor = normalColor;

            dgv.Columns[headerTargetQty].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[headerAbleProduceQty].DefaultCellStyle.ForeColor = normalColor;
            //dgv.Columns[headerProducedQty].DefaultCellStyle.ForeColor = Color.White;


            dgv.Columns[headerMaterial].DefaultCellStyle.ForeColor = normalColor;
            dgv.Columns[headerMaterialBag].DefaultCellStyle.ForeColor = importantColor;
            dgv.Columns[headerRecycle].DefaultCellStyle.ForeColor = normalColor;

            //dgv.Columns[headerColor].DefaultCellStyle.ForeColor = Color.White;
            dgv.Columns[headerColorMaterial].DefaultCellStyle.ForeColor = normalColor;
            //dgv.Columns[headerColorMaterialUsage].DefaultCellStyle.ForeColor = Color.White;
            dgv.Columns[headerColorMaterialQty].DefaultCellStyle.ForeColor = importantColor;


            //dgv.Columns[headerStatus].DefaultCellStyle.ForeColor = Color.FromArgb(52, 160, 225);
            dgv.Columns[headerNote].DefaultCellStyle.ForeColor = Color.FromArgb(192, 0, 0);

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Courier New", 8F, FontStyle.Regular);

            //dgv.Columns[headerStatus].DefaultCellStyle.Font = new Font("Courier New", 10F, FontStyle.Bold);
        }

        #endregion

        #region Load Data

        private void InitializeData()
        {
            tool.loadFactoryAndAll(cmbFactory);
            ResetData();
        }

        private void loadMachine()
        {
            if(cmbFactory.SelectedIndex != -1)
            {
                string fac = cmbFactory.Text;

                if (string.IsNullOrEmpty(fac))
                {
                    tool.loadMacIDToComboBox(cmbMachine);
                    cmbMachine.SelectedIndex = -1;
                }
                else if (fac.Equals("All"))
                {
                    tool.loadMacIDToComboBox(cmbMachine);
                    cmbMachine.SelectedIndex = -1;
                }
                else
                {
                    tool.loadMacIDByFactoryToComboBox(cmbMachine, fac);
                }
            }
            
        }

        private DataTable specialDataSort(DataTable dt)
        {
            DataTable sortedDt = dt.Clone();
            string Fac = null;
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    string newFac = row[dalMac.MacLocation].ToString();
                    if (Fac != newFac)
                    {
                        foreach (DataRow row2 in dt.Rows)
                        {
                            if (row2.RowState != DataRowState.Deleted)
                            {
                                string FacSearch = row2[dalMac.MacLocation].ToString();

                                if (Fac == FacSearch)
                                {
                                    DataRow newRow2 = sortedDt.NewRow();
                                    newRow2 = row2;
                                    sortedDt.ImportRow(newRow2);
                                    row2.Delete();
                                }
                            }


                        }
                    }

                    DataRow newRow = sortedDt.NewRow();
                    newRow = row;
                    sortedDt.ImportRow(newRow);
                    row.Delete();
                    Fac = newFac;
                }

            }

            return sortedDt;
        }

        private void loadScheduleData()
        {
            #region Search Filtering
            DataTable dt;
            string Keywords = txtSearch.Text;

            if (!string.IsNullOrEmpty(Keywords))
            {
                if(cbItem.Checked)
                {
                    //search item
                    dt = dalPlanning.itemSearch(Keywords);
                }
                else
                {
                    //search id
                    dt = dalPlanning.idSearch(Keywords);
                }
            }
            else
            {
                dt = dalPlanning.Select();

            }

            dt = specialDataSort(dt);

            #endregion

            DataTable dt_Schedule = NewScheduleTable();
            DataRow row_Schedule;
            bool match = true;
            string previousLocation = null;
            foreach (DataRow row in dt.Rows)
            {
                match = true;

                #region Status Filtering

                string status = row[dalPlanning.planStatus].ToString();

                if(string.IsNullOrEmpty(status))
                {
                    match = false;
                }

                if(!cbPending.Checked &&status.Equals(text.planning_status_pending))
                {
                    match = false;
                }

                if (!cbRunning.Checked && status.Equals(text.planning_status_running))
                {
                    match = false;
                }

                if (!cbWarning.Checked && status.Equals(text.planning_status_warning))
                {
                    match = false;
                }

                if (!cbCancelled.Checked && status.Equals(text.planning_status_cancelled))
                {
                    match = false;
                }

                if (!cbCompleted.Checked && status.Equals(text.planning_status_completed))
                {
                    match = false;
                }

                #endregion

                #region Period Filtering

                DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                DateTime from = dtpFrom.Value;
                DateTime to = dtpTo.Value;

                if(!((start.Date >= from.Date && start.Date <= to.Date) || (end.Date >= from.Date && end.Date <= to.Date)))
                {
                    match = false;
                }

                #endregion

                #region Location Filtering

                string factoryFilter = cmbFactory.Text;
                string machineFilter = cmbMachine.Text;

                string factory = row[dalMac.MacLocation].ToString();
                string machine = row[dalMac.MacID].ToString();

                if (!factoryFilter.Equals("All") && !string.IsNullOrEmpty(factoryFilter))
                {
                    if(!factory.Equals(factoryFilter))
                    {
                        match = false;
                    }

                    if(!string.IsNullOrEmpty(machineFilter))
                    {
                        if (!machine.Equals(machineFilter))
                        {
                            match = false;
                        }
                    }
                }

                #endregion

                if (match)
                {
                    if(previousLocation == null)
                    {
                        previousLocation = row[dalMac.MacLocation].ToString();
                    }
                    else if(!previousLocation.Equals(row[dalMac.MacLocation].ToString()))
                    {
                        row_Schedule = dt_Schedule.NewRow();
                        dt_Schedule.Rows.Add(row_Schedule);
                        previousLocation = row[dalMac.MacLocation].ToString();
                    }
                    row_Schedule = dt_Schedule.NewRow();

                    row_Schedule[headerID] = row[dalPlanning.planID];
                    row_Schedule[headerStartDate] = row[dalPlanning.productionStartDate];
                    row_Schedule[headerEndDate] = row[dalPlanning.productionEndDate];

                    row_Schedule[headerFactory] = row[dalMac.MacLocation];
                    row_Schedule[headerMachine] = row[dalMac.MacID];

                    row_Schedule[headerPartName] = row[dalItem.ItemName];
                    row_Schedule[headerPartCode] = row[dalItem.ItemCode];

                    float ct = row[dalPlanning.planCT] == DBNull.Value ? row[dalItem.ItemProCTTo] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemProCTTo]) : Convert.ToSingle(row[dalPlanning.planCT]);
                    row_Schedule[headerPartCycleTime] = ct;
                    row_Schedule[headerProductionPurpose] = row[dalPlanning.productionPurpose];

                    row_Schedule[headerTargetQty] = row[dalPlanning.targetQty];
                    row_Schedule[headerAbleProduceQty] = row[dalPlanning.ableQty];
                    //row_Schedule[headerProducedQty] = 0;

                    row_Schedule[headerMaterial] = row[dalPlanning.materialCode];
                    row_Schedule[headerMaterialBag] = row[dalPlanning.materialBagQty];
                    row_Schedule[headerRecycle] = row[dalPlanning.materialRecycleUse];

                    //row_Schedule[headerColor] = row[dalItem.ItemColor];
                    row_Schedule[headerColorMaterial] = row[dalPlanning.colorMaterialCode];
                    row_Schedule[headerColorMaterialQty] = row[dalPlanning.colorMaterialQty];

                    row_Schedule[headerNote] = row[dalPlanning.planNote];
                    row_Schedule[headerStatus] = row[dalPlanning.planStatus];

                    dt_Schedule.Rows.Add(row_Schedule);
                }
               
            }

            dgvSchedule.DataSource = null;

            if (dt_Schedule.Rows.Count > 0)
            {
                dgvSchedule.DataSource = dt_Schedule;
                dgvScheduleUIEdit(dgvSchedule);
                dgvSchedule.ClearSelection();
            }
        }

        private void checkIfFamilyMould(DataTable dt)
        {

        }

        private void frmMachineSchedule_Load(object sender, EventArgs e)
        {
            dgvSchedule.ClearSelection();
            loaded = true;
            loadMachine();
            cmbFactory.SelectedIndex = 0;
            loadScheduleData();
        }

        #endregion

        #region Button Click to Plan Page

        private void btnPlan_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            frmPlanning frm = new frmPlanning();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            loadScheduleData();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";
            string title = "MouldChangePlan";
            DateTime currentDate = DateTime.Now;

            fileName = title + "_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";

            return fileName;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                
                SaveFileDialog sfd = new SaveFileDialog();
                string path = @"D:\StockAssistant\Document\MouldChangeReport";
                Directory.CreateDirectory(path);
                sfd.InitialDirectory = path;
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
                    DateTime dateNow = DateTime.Now;
                    xlWorkSheet.Name = "MOULD CHANGE PLAN ";
                    
                    
                    #region Save data to Sheet

                    string title = "MOULD CHANGE PLAN " + dateNow.ToString("dd/MM/yyyy");

                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 " + title;

                    string date = dateNow.ToString("dd/MM/yyyy hh:mm:ss");
                    //Header and Footer setup
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + dateNow.ToString("dd/MM/yyyy"); 
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID)+" At "+ date;

                    //Page setup
                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    

                    xlWorkSheet.PageSetup.Zoom = false;
                    xlWorkSheet.PageSetup.CenterHorizontally = true;

                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;

                    double pointToCMRate = 0.035;
                    xlWorkSheet.PageSetup.TopMargin = 1.5 / pointToCMRate;
                    xlWorkSheet.PageSetup.BottomMargin = 1.0 / pointToCMRate;
                    xlWorkSheet.PageSetup.HeaderMargin = 1.0 / pointToCMRate;
                    xlWorkSheet.PageSetup.FooterMargin = 1.0 / pointToCMRate;
                    xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                    xlWorkSheet.PageSetup.RightMargin = 0 / pointToCMRate;

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
                    tRange.Font.Size = 12;
                    tRange.Font.Name = "Calibri";
                    tRange.EntireColumn.AutoFit();
                    tRange.EntireRow.AutoFit();
                   
                    tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);

                    #endregion

                    DataTable dt_test = (DataTable)dgvSchedule.DataSource;

                    //int row = dgvSchedule.RowCount - 1;
                    //int col = dgvSchedule.ColumnCount - 1;

                    //MessageBox.Show("row: " + row + " col: " + col);
                    if (true)//cmbSubType.Text.Equals("PMMA")
                    {
                        for (int i = 0; i <= dgvSchedule.RowCount - 1; i++)
                        {
                            for (int j = 0; j <= dgvSchedule.ColumnCount - 1; j++)
                            {
                                Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];
                                range.Rows.RowHeight = 30;
                                range.Font.Color = ColorTranslator.ToOle(dgvSchedule.Rows[i].Cells[j].InheritedStyle.ForeColor);

                                if (j == 3 || j == 6 || j == 7 || j == 9 || j == 12 || j == 15 || j == 11 || j == 14)
                                {
                                    range.Cells.Font.Bold = true;
                                    range.Font.Size = 16;
                                    range.Cells.Font.Color = ColorTranslator.ToOle(Color.Black);
                                }

                                if (j == 4 || j == 5)
                                {
                                    range.ColumnWidth = 10;
                                }

                                if (j == 6 || j == 7 || j == 11 || j == 14 || j == 16)
                                {
                                    range.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                    range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                }
                                else
                                {
                                    range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                    range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                }

                                if (dgvSchedule.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Gainsboro)
                                {
                                    range.Interior.Color = Color.White;
                                    range.Rows.RowHeight = 40;
                                }
                                else if (dgvSchedule.Rows[i].Cells[j].InheritedStyle.BackColor == Color.FromArgb(64, 64, 64))
                                {
                                    range.Rows.RowHeight = 4;
                                    range.Interior.Color = Color.Black;
                                }
                                else
                                {
                                    range.Interior.Color = ColorTranslator.ToOle(dgvSchedule.Rows[i].Cells[j].InheritedStyle.BackColor);
                                    range.Rows.RowHeight = 40;
                                }
                            }
                        }
                    }

                    //Range header2 = (Range)xlWorkSheet.Cells[1, 1];
                    //header2.Interior.Color = Color.Gold;

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
                    dgvSchedule.ClearSelection();

                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                        System.Diagnostics.Process.Start(sfd.FileName);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void copyAlltoClipboard()
        {
            dgvSchedule.SelectAll();
            DataObject dataObj = dgvSchedule.GetClipboardContent();
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

        #region Filter Data

        private void cbItem_CheckedChanged(object sender, EventArgs e)
        {
            if (cbItem.Checked)
            {
                cbPlanningID.Checked = false;
            }
        }

        private void cbPlanningID_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPlanningID.Checked)
            {
                cbItem.Checked = false;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

            if (!string.IsNullOrEmpty(keyword))
            {

            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFactory.Text != null && loaded && ableLoadData)
            {
                ableLoadData = false;

                loadMachine();
                ableLoadData = true;
            }
        }

        private void cmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMachine.Text != null && ableLoadData)
            {


                ableLoadData = false;
                cmbFactory.Text = tool.getFactoryNameFromMachineID(cmbMachine.Text);
                ableLoadData = true;
            }
        }

        private void ResetStatusToDefault()
        {
            cbPending.Checked = true;
            cbRunning.Checked = true;
            cbWarning.Checked = true;
            cbCompleted.Checked = false;
            cbCancelled.Checked = false;
        }

        private void ResetData()
        {
            cmbFactory.SelectedIndex = 0;
            txtSearch.Clear();
            cbItem.Checked = true;
            ResetStatusToDefault();

            DateTime todayDate = DateTime.Today;
            dtpFrom.Value = todayDate.AddDays(-180);
            dtpTo.Value = todayDate.AddDays(180);
            ActiveControl = dgvSchedule;

        }

        private void ResetAll_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            ResetData();

            dgvSchedule.ClearSelection();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        public void StartForm()
        {
            System.Windows.Forms.Application.Run(new frmLoading());
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {


            
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                frmLoading.ShowLoadingScreen();
                loadScheduleData();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                frmLoading.CloseForm();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbPlanningID.Checked)
            {
                if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region form close
        private void frmMachineSchedule_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.ProductionFormOpen = false;
        }
        #endregion

        #region material list check

        private void btnMatList_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            frmMatPlanningList frm = new frmMatPlanningList();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }
        #endregion

        #region datagridview cell formatting

        private void dgvSchedule_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvSchedule;
            dgv.SuspendLayout();

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerStatus)
            {
                string value = dgv.Rows[row].Cells[headerStatus].Value.ToString();
                Color ColorSet = dgv.DefaultCellStyle.BackColor;

                if (value.Equals(text.planning_status_cancelled))
                {
                    ColorSet = Color.White;
                }
                else if (value.Equals(text.planning_status_completed))
                {
                    ColorSet = Color.Gainsboro;
                }
                else if (value.Equals(text.planning_status_delayed))
                {
                    ColorSet = Color.FromArgb(255, 255, 128);
                }
                else if (value.Equals(text.planning_status_pending))
                {
                    ColorSet = Color.Gainsboro;
                }
                else if (value.Equals(text.planning_status_running))
                {
                    ColorSet = Color.FromArgb(0, 184, 148);
                }
                else if (value.Equals(text.planning_status_warning))
                {
                    ColorSet = Color.LightGreen;
                }
                else if (value.Equals(""))
                {
                    ColorSet = Color.FromArgb(64, 64, 64);
                }

                dgv.Rows[row].Cells[headerStatus].Style.BackColor = ColorSet;
                dgv.Rows[row].Cells[headerStatus].Style.ForeColor = Color.Black;

                if (value == "")
                {
                    dgv.Rows[row].Height = 12;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    dgv.Rows[row].Height = 50;
                }
            }

            else if (dgv.Columns[col].Name == headerStartDate)
            {
                string status = dgv.Rows[row].Cells[headerStatus].Value.ToString();

                if (!status.Equals(text.planning_status_cancelled) && !status.Equals(text.planning_status_completed) && status != "")
                {
                    DataTable dt = (DataTable)dgv.DataSource;
                    DateTime startDate = Convert.ToDateTime(dgv.Rows[row].Cells[headerStartDate].Value);
                    int macID = Convert.ToInt32(dgv.Rows[row].Cells[headerMachine].Value);

                    if(row+1 <= dgv.Rows.Count - 1)
                    {
                        for (int i = row + 1; i < dgv.Rows.Count; i++)
                        {
                            string otherStatus = dgv.Rows[i].Cells[headerStatus].Value.ToString();
                            if (!otherStatus.Equals(text.planning_status_cancelled) && !otherStatus.Equals(text.planning_status_completed) && otherStatus != "")
                            {
                                DateTime otherStart = Convert.ToDateTime(dgv.Rows[i].Cells[headerStartDate].Value);
                                int otherMacID = Convert.ToInt32(dgv.Rows[i].Cells[headerMachine].Value);
                                if (startDate == otherStart && macID == otherMacID)
                                {
                                    dgv.Rows[row].Cells[headerStartDate].Style.BackColor = Color.Yellow;
                                    dgv.Rows[i].Cells[headerStartDate].Style.BackColor = Color.Yellow;

                                }
                            }
                                
                        }
                    }
                    




                    //dgv.Rows[row].Cells[headerStartDate].Style.BackColor = Color.Gainsboro;

                }
            }

            dgv.ResumeLayout();
        }
        #endregion

        #region plan status change

        private bool planRunning(int rowIndex, string presentStatus)
        {
            bool statusChanged = false;

            uPlanning.plan_id = (int)dgvSchedule.Rows[rowIndex].Cells[headerID].Value;
            uPlanning.machine_id = (int)dgvSchedule.Rows[rowIndex].Cells[headerMachine].Value;
            uPlanning.production_start_date = (DateTime)dgvSchedule.Rows[rowIndex].Cells[headerStartDate].Value;
            uPlanning.production_end_date = (DateTime)dgvSchedule.Rows[rowIndex].Cells[headerEndDate].Value;


            if (tool.ifProductionDateAvailable(uPlanning.machine_id.ToString()))
            {
                frmMachineScheduleAdjustFromMain frm = new frmMachineScheduleAdjustFromMain(uPlanning, true);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                if (frmMachineScheduleAdjustFromMain.applied)
                {
                    //loadScheduleData();

                    statusChanged = true;
                    //inactive mat plan

                    uMatPlan.plan_id = uPlanning.plan_id;
                    uMatPlan.active = true;
                    uMatPlan.updated_date = DateTime.Now;
                    uMatPlan.updated_by = MainDashboard.USER_ID;

                    dalMatPlan.ActiveUpdate(uMatPlan);

                    if (presentStatus == text.planning_status_cancelled || presentStatus == text.planning_status_completed)
                    {
                        //get habit data
                        float oldHourPerDay = 0;
                        float newHourPerDay = 0;

                        DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_PlanningPage, text.habit_planning_HourPerDay);

                        foreach (DataRow row in dt.Rows)
                        {
                            newHourPerDay = Convert.ToSingle(row[dalHabit.HabitData]);
                        }

                        //get this plan hour per day data
                        DataTable dt_plan = dalPlanning.idSearch(uPlanning.plan_id.ToString());

                        foreach (DataRow row in dt_plan.Rows)
                        {
                            oldHourPerDay = Convert.ToSingle(row[dalPlanning.productionHourPerDay]);

                            //compare
                            if (oldHourPerDay != newHourPerDay)
                            {
                                int oldProDay = 0, newProDay = 0;
                                float oldProHour = 0, newProHour = 0;

                                oldProDay = Convert.ToInt32(row[dalPlanning.productionDay]);
                                oldProHour = Convert.ToSingle(row[dalPlanning.productionHour]);

                                float totalHour = oldProDay * oldHourPerDay + oldProHour;

                                newProDay = Convert.ToInt32(totalHour / newHourPerDay);
                                newProHour = totalHour - newProDay * newHourPerDay;

                                if (newProHour < 0)
                                {
                                    newProHour = 0;
                                }

                                DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                                DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                                //change this plan data
                                //update
                                //change production day & hour data
                                uPlanning.plan_id = uPlanning.plan_id;
                                uPlanning.production_hour = newProHour.ToString();
                                uPlanning.production_day = newProDay.ToString();
                                uPlanning.production_hour_per_day = newHourPerDay.ToString();
                                uPlanning.production_start_date = start.Date;
                                uPlanning.production_end_date = end.Date;
                                uPlanning.plan_updated_date = DateTime.Now;
                                uPlanning.plan_updated_by = MainDashboard.USER_ID;

                                //update to db
                                dalPlanningAction.planningScheduleAndProDayChange(uPlanning, oldProDay.ToString(), oldProHour.ToString(), oldHourPerDay.ToString(), start.Date.ToString(), end.Date.ToString());
                            }
                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("Machine "+ uPlanning.machine_id + " is running now, please stop it before start a new plan.");
            }
            
            

            Cursor = Cursors.Arrow; // change cursor to normal type
            return statusChanged;
        }

        private bool planPending(int planID, string presentStatus)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            bool statusChanged = false;
            DateTime date = DateTime.Now;
            uPlanning.plan_id = planID;
            uPlanning.plan_status = text.planning_status_pending;
            uPlanning.plan_updated_date = date;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;

            //bool success = dalPlanning.statusUpdate(uPlanning);
            bool success = dalPlanningAction.planningStatusChange(uPlanning, presentStatus);

            if (!success)
            {
                MessageBox.Show("Status update not successful! ");
            }
            else
            {
                //inactive mat plan
                statusChanged = true;
                uMatPlan.plan_id = planID;
                uMatPlan.active = true;
                uMatPlan.updated_date = date;
                uMatPlan.updated_by = MainDashboard.USER_ID;

                dalMatPlan.ActiveUpdate(uMatPlan);

                if (presentStatus == text.planning_status_cancelled || presentStatus == text.planning_status_completed)
                {
                    //get habit data
                    float oldHourPerDay = 0;
                    float newHourPerDay = 0;

                    DataTable dt = dalHabit.HabitSearch(text.habit_belongTo_PlanningPage, text.habit_planning_HourPerDay);

                    foreach (DataRow row in dt.Rows)
                    {
                        newHourPerDay = Convert.ToSingle(row[dalHabit.HabitData]);
                    }

                    //get this plan hour per day data
                    DataTable dt_plan = dalPlanning.idSearch(planID.ToString());

                    foreach (DataRow row in dt_plan.Rows)
                    {
                        oldHourPerDay = Convert.ToSingle(row[dalPlanning.productionHourPerDay]);

                        //compare
                        if (oldHourPerDay != newHourPerDay)
                        {
                            int oldProDay = 0, newProDay = 0;
                            float oldProHour = 0, newProHour = 0;

                            oldProDay = Convert.ToInt32(row[dalPlanning.productionDay]);
                            oldProHour = Convert.ToSingle(row[dalPlanning.productionHour]);

                            float totalHour = oldProDay * oldHourPerDay + oldProHour;

                            newProDay = Convert.ToInt32(totalHour / newHourPerDay);
                            newProHour = totalHour - newProDay * newHourPerDay;

                            if(newProHour < 0)
                            {
                                newProHour = 0;
                            }

                            DateTime start = Convert.ToDateTime(row[dalPlanning.productionStartDate]);
                            DateTime end = Convert.ToDateTime(row[dalPlanning.productionEndDate]);

                            //change this plan data
                            //update
                            //change production day & hour data
                            uPlanning.plan_id = planID;
                            uPlanning.production_hour = newProHour.ToString();
                            uPlanning.production_day = newProDay.ToString();
                            uPlanning.production_hour_per_day = newHourPerDay.ToString();
                            uPlanning.production_start_date = start.Date;
                            uPlanning.production_end_date = end.Date;
                            uPlanning.plan_updated_date = DateTime.Now;
                            uPlanning.plan_updated_by = MainDashboard.USER_ID;

                            //update to db
                            dalPlanningAction.planningScheduleAndProDayChange(uPlanning, oldProDay.ToString(), oldProHour.ToString(), oldHourPerDay.ToString(), start.Date.ToString(), end.Date.ToString());
                        }
                    }
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

            return statusChanged;
        }

        private bool planComplete(int planID, string presentStatus)
        {
            bool statusChanged = false;
            DateTime date = DateTime.Now;
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            uPlanning.plan_id = planID;
            uPlanning.plan_status = text.planning_status_completed;
            uPlanning.plan_updated_date = date;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;

            bool success = dalPlanningAction.planningStatusChange(uPlanning, presentStatus);

            if (!success)
            {
                MessageBox.Show("Status update not successful! ");
            }
            else
            {
                //inactive mat plan
                statusChanged = true;
                uMatPlan.plan_id = planID;
                uMatPlan.active = false;
                uMatPlan.updated_date = date;
                uMatPlan.updated_by = MainDashboard.USER_ID;

                dalMatPlan.ActiveUpdate(uMatPlan);
            }
            Cursor = Cursors.Arrow; // change cursor to normal type

            return statusChanged;
        }

        private bool planCancel(int planID, string presentStatus)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            bool statusChanged = false;
            DateTime date = DateTime.Now;
            uPlanning.plan_id = planID;
            uPlanning.plan_status = text.planning_status_cancelled;
            uPlanning.plan_updated_date = date;
            uPlanning.plan_updated_by = MainDashboard.USER_ID;

            bool success = dalPlanningAction.planningStatusChange(uPlanning, presentStatus);

            if (!success)
            {
                MessageBox.Show("Status update not successful! ");
            }
            else
            {
                //inactive mat plan
                statusChanged = true;
                uMatPlan.plan_id = planID;
                uMatPlan.active = false;
                uMatPlan.updated_date = date;
                uMatPlan.updated_by = MainDashboard.USER_ID;

                dalMatPlan.ActiveUpdate(uMatPlan);
            }

            
            Cursor = Cursors.Arrow; // change cursor to normal type
            return statusChanged;
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            dgvSchedule.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            DataGridView dgv = dgvSchedule;
            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            int planID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerID].Value);

            int macID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerMachine].Value);

            string presentStatus = dgv.Rows[rowIndex].Cells[headerStatus].Value.ToString();
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();
            if (itemClicked.Equals(text.planning_status_pending))
            {
                if (MessageBox.Show("Are you sure you want to switch this plan to PENDING status?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (planPending(planID, presentStatus))
                    {
                        DataTable dt = (DataTable)dgvSchedule.DataSource;
                        if (cbPending.Checked)
                        {
                            dt.Rows[rowIndex][headerStatus] = text.planning_status_pending;
                        }
                        else
                        {
                            dt.Rows.RemoveAt(rowIndex);
                            dt.AcceptChanges();
                        }
                    }
                }
                    
            }
            else if (itemClicked.Equals(text.planning_status_running))
            {              
                if (planRunning(rowIndex, presentStatus))
                {
                    DataTable dt = (DataTable)dgvSchedule.DataSource;
                    if (cbRunning.Checked)
                    {
                        dt.Rows[rowIndex][headerStatus] = text.planning_status_running;
                    }
                    else
                    {
                        dt.Rows.RemoveAt(rowIndex);
                        dt.AcceptChanges();
                    }
                }
            }
            else if (itemClicked.Equals(text.planning_status_completed))
            {
                if (MessageBox.Show("Are you sure you want to switch this plan to COMPLETED status?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (planComplete(planID, presentStatus))
                    {
                        DataTable dt = (DataTable)dgvSchedule.DataSource;
                        if (cbCompleted.Checked)
                        {
                            dt.Rows[rowIndex][headerStatus] = text.planning_status_completed;
                        }
                        else
                        {
                            dt.Rows.RemoveAt(rowIndex);
                            dt.AcceptChanges();
                        }

                        var w = new Form() { Size = new Size(0, 0) };
                        Task.Delay(TimeSpan.FromSeconds(3))
                            .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());

                        MessageBox.Show(w,  "PLAN " + planID + " completed!", "SYSTEM");
                    }
                }
            }
            else if (itemClicked.Equals(text.planning_status_cancelled))
            {
                if (MessageBox.Show("Are you sure you want to CANCEL this plan?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(planCancel(planID, presentStatus))
                    {
                        DataTable dt = (DataTable)dgvSchedule.DataSource;
                        if (cbCancelled.Checked)
                        {
                            dt.Rows[rowIndex][headerStatus] = text.planning_status_cancelled;
                        }
                        else
                        {
                            dt.Rows.RemoveAt(rowIndex);
                            dt.AcceptChanges();
                        }

                        var w = new Form() { Size = new Size(0, 0) };
                        Task.Delay(TimeSpan.FromSeconds(3))
                            .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());

                        MessageBox.Show(w, "PLAN "+planID+" cancelled!", "SYSTEM");
                    }
                }
                
            }
            if (itemClicked.Equals("Edit Schedule"))
            {
                editSchedule(rowIndex);
            }

            //loadScheduleData();
            //dgvSchedule.ClearSelection();

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgvSchedule.ResumeLayout();
        }

        private void editSchedule(int rowIndex)
        {
            uPlanning.plan_id = (int)dgvSchedule.Rows[rowIndex].Cells[headerID].Value;
            uPlanning.machine_id = (int)dgvSchedule.Rows[rowIndex].Cells[headerMachine].Value;
            uPlanning.production_start_date = (DateTime)dgvSchedule.Rows[rowIndex].Cells[headerStartDate].Value;
            uPlanning.production_end_date = (DateTime)dgvSchedule.Rows[rowIndex].Cells[headerEndDate].Value;

            frmMachineScheduleAdjustFromMain frm = new frmMachineScheduleAdjustFromMain(uPlanning, false);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if(frmMachineScheduleAdjustFromMain.applied)
            {
                loadScheduleData();
                dgvSchedule.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }

        private void dgvSchedule_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && userPermission >= MainDashboard.ACTION_LVL_THREE)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgvSchedule.CurrentCell = dgvSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgvSchedule.Rows[e.RowIndex].Selected = true;
                dgvSchedule.Focus();
                int rowIndex = dgvSchedule.CurrentCell.RowIndex;

                try
                {
                    string result = dgvSchedule.Rows[rowIndex].Cells[headerStatus].Value.ToString();

                    if (result.Equals(text.planning_status_pending))
                    {
                        my_menu.Items.Add("Run").Name = text.planning_status_running;
                        my_menu.Items.Add("Cancel").Name = text.planning_status_cancelled;
                        my_menu.Items.Add("Complete").Name = text.planning_status_completed;                        
                    }
                    else if (result.Equals(text.planning_status_cancelled))
                    {

                        my_menu.Items.Add("Run").Name = text.planning_status_running;
                        my_menu.Items.Add("Pending").Name = text.planning_status_pending;

                        
                    }
                    else if (result.Equals(text.planning_status_running))
                    {
                        my_menu.Items.Add("Pending").Name = text.planning_status_pending;
                        my_menu.Items.Add("Cancel").Name = text.planning_status_cancelled;
                        my_menu.Items.Add("Complete").Name = text.planning_status_completed;
                    }
                    else if (result.Equals(text.planning_status_completed))
                    {
                        my_menu.Items.Add("Run").Name = text.planning_status_running;
                        my_menu.Items.Add("Pending").Name = text.planning_status_pending;
                    }

                    my_menu.Items.Add("Edit Schedule").Name = "Edit Schedule";

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        #region plan action history check
        private void dgvSchedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string planID = dgvSchedule.Rows[e.RowIndex].Cells[headerID].Value.ToString();

            //MessageBox.Show("Plan ID: "+planID);
            if(planID != null)
            {
                frmPlanningActionHistory frm = new frmPlanningActionHistory(Convert.ToInt32(planID));
                frm.StartPosition = FormStartPosition.CenterScreen;

                if(!frmPlanningActionHistory.noData)
                {
                    frm.ShowDialog();
                }
            } 
        }
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Thread t = null;
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                t = new Thread(new ThreadStart(StartForm));
                loadScheduleData();
            }
            catch (ThreadAbortException)
            {
                // ignore it
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                t.Abort();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (filterHide)
            {
                filterHide = false;
            }
            else
            {
                filterHide = true;
            }

            FilterHideOrShow(filterHide);
        }
    }
}
