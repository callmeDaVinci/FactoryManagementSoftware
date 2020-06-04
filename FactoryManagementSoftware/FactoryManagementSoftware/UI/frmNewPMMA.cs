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

namespace FactoryManagementSoftware.UI
{
    public partial class frmNewPMMA : Form
    {

        #region declare Variable/Object

        materialDAL dalMat = new materialDAL();
        itemDAL dalItem = new itemDAL();
        itemBLL uItem = new itemBLL();
        joinDAL dalJoin = new joinDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        custDAL dalCust = new custDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        pmmaDAL dalPMMA = new pmmaDAL();
        pmmaBLL uPMMA = new pmmaBLL();
        userDAL dalUser = new userDAL();

        Tool tool = new Tool();
        Text text = new Text();

        private DataTable dt_MatUsed;
        private DataTable dt_MatStock;
        private DataTable dt_ZeroCostMat;
        private DataTable dt_Item;
        private DataTable dt_PMMA;
        private DataTable dt_DeliveredData;

        private readonly string GroupBoxStockList = "ZERO COST MATERIAL STOCK LIST";
        private readonly string GroupBoxMatUsedList = "ZERO COST MATERIAL USED LIST";

        private string oldAdjustQty = null;
        private string oldNote = null;

        private bool checkMatUsedOnly = false;
        private bool Loaded = false;
        private bool editModeAvailable = true;
        private int editedCellRow = -1;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

        #endregion

        #region Initial Data

        public frmNewPMMA()
        {
            InitializeComponent();
            loadMonthDataToCMB(cmbStockMonth);
            loadYearDataToCMB(cmbStockYear);
            dateSetup();
            tool.DoubleBuffered(dgvMatUsed, true);
            tool.DoubleBuffered(dgvMatStock, true);

            dt_MatUsed = NewMatUsedTable();
            dt_DeliveredData = NewDeliveredDataTable();
        }

        private void loadMonthDataToCMB(ComboBox cmb)
        {
            DataTable dt_month = new DataTable();

            dt_month.Columns.Add("month");

            for (int i = 1; i <= 12; i++)
            {
                dt_month.Rows.Add(i);
            }

            cmb.DataSource = dt_month;
            cmb.DisplayMember = "month";
        }

        private void loadYearDataToCMB(ComboBox cmb)
        {
            DataTable dt_year = new DataTable();

            dt_year.Columns.Add("year");

            for (int i = 2018; i <= 2018 + 100; i++)
            {
                dt_year.Rows.Add(i);
            }

            cmb.DataSource = dt_year;
            cmb.DisplayMember = "year";
        }

        private void dateSetup()
        {

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            dtpFrom.Value = tool.GetPMMAStartDate(month, year);
            dtpTo.Value = tool.GetPMMAEndDate(month, year);

            cmbStockMonth.Text = month.ToString();
            cmbStockYear.Text = year.ToString();
        }

        private void getStartandEndDate()
        {
            if(Loaded)
            {
                string monthString = cmbStockMonth.Text;
                string yearString = cmbStockYear.Text;

                int year = string.IsNullOrEmpty(cmbStockYear.Text) ? 0 : Convert.ToInt32(cmbStockYear.Text);
                int month = string.IsNullOrEmpty(cmbStockMonth.Text) ? 0 : Convert.ToInt32(cmbStockMonth.Text);

                dtpFrom.Value = tool.GetPMMAStartDate(month, year);
                dtpTo.Value = tool.GetPMMAEndDate(month, year);
            }   
        }

        #endregion

        #region UI Design

        private DataTable NewDeliveredDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_Delivered, typeof(float));

            return dt;
        }

        private DataTable NewStockTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_MatCode, typeof(string));
            dt.Columns.Add(text.Header_MatName, typeof(string));
            
            dt.Columns.Add(text.Header_OpeningStock, typeof(float));

            dt.Columns.Add(text.Header_In_KG_Piece, typeof(float));
            dt.Columns.Add(text.Header_Out_KG_Piece, typeof(float));
            dt.Columns.Add(text.Header_Wastage, typeof(float));
            dt.Columns.Add(text.Header_Adjust, typeof(float));
            dt.Columns.Add(text.Header_Note, typeof(string));
            dt.Columns.Add(text.Header_BalStock, typeof(float));

            return dt;
        }

        private DataTable NewMatUsedTable()
        {
            DataTable dt = new DataTable();

            
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_MatCode, typeof(string));
            dt.Columns.Add(text.Header_MatName, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_Parent, typeof(string));
            dt.Columns.Add(text.Header_Delivered, typeof(int));
            dt.Columns.Add(text.Header_ItemWeight_G, typeof(float));
            dt.Columns.Add(text.Header_MaterialUsed_KG_Piece, typeof(float));
            dt.Columns.Add(text.Header_Wastage, typeof(float));
            dt.Columns.Add(text.Header_MaterialUsedWithWastage, typeof(float));
            dt.Columns.Add(text.Header_TotalMaterialUsed_KG_Piece, typeof(float));
 

            return dt;
        }

        private void dgvStockUIEdit(DataGridView dgv)
        {
            dgv.Columns[text.Header_Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
           
            dgv.Columns[text.Header_MatName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[text.Header_MatCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[text.Header_OpeningStock].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_In_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_Out_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_Wastage].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_Adjust].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_BalStock].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_OpeningStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_In_KG_Piece].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_Out_KG_Piece].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[text.Header_Wastage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_Adjust].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_BalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void dgvMatUsedUIEdit(DataGridView dgv)
        {
            //dgv.Columns[text.Header_PartName].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            dgv.Columns[text.Header_Parent].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Italic);

            dgv.Columns[text.Header_PartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_PartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MatCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MatName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_Parent].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_Delivered].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_ItemWeight_G].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MaterialUsed_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_Wastage].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_MaterialUsedWithWastage].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_Delivered].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_ItemWeight_G].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_MaterialUsed_KG_Piece].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_Wastage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[text.Header_MaterialUsedWithWastage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.Columns[text.Header_TotalMaterialUsed_KG_Piece].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[text.Header_MatName].Frozen = true;

            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 6F, FontStyle.Regular);
        }

        #endregion

        #region Switch Data

        private void switchToMatUsedReport()
        {
            btnSwitchToStockCheck.ForeColor = Color.Black;
            btnSwitchToMatUsed.ForeColor = Color.FromArgb(52, 139, 209);

            tlpPMMA.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
            tlpPMMA.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);

            dgvMatUsed.ResumeLayout();

            gbPMMA.Text = GroupBoxMatUsedList;
        }

        private void switchToStockList()
        {
            btnSwitchToMatUsed.ForeColor = Color.Black;
            btnSwitchToStockCheck.ForeColor = Color.FromArgb(52, 139, 209);

            tlpPMMA.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
            tlpPMMA.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);

            dgvMatStock.ResumeLayout();

            gbPMMA.Text = GroupBoxStockList;
        }

        private void btnStockCheck_Click(object sender, EventArgs e)
        {
            checkMatUsedOnly = false;
            switchToStockList();
        }

        private void btnMatUsed_Click(object sender, EventArgs e)
        {
            checkMatUsedOnly = true;
            switchToMatUsedReport();
        }
        #endregion

        #region Load Data

        private void frmNewPMMA_Load(object sender, EventArgs e)
        {
            switchToStockList();
            Loaded = true;

            int userPermission = dalUser.getPermissionLevel(MainDashboard.USER_ID);

            if (userPermission >= MainDashboard.ACTION_LVL_TWO)
            {
                cbEditMode.Visible = true;
            }
        }

        private void btnMatUsedCheck_Click(object sender, EventArgs e)
        {
            LoadMatUsedData();
        }

        private void LoadMatUsedData()
        {
            #region Datatable Data

            //get all pmma item
            string PMMA2 = tool.getCustName(1);
            DataTable dt_PMMAItem = dalItemCust.custSearch(PMMA2);

            //create datatable
            dt_MatUsed = NewMatUsedTable();
            DataRow row_DGVSouce;

            //get all item info
            dt_Item = dalItem.Select();

            //get zero cost material list
            dt_ZeroCostMat = dalMat.SelectZeroCostMaterial();

            //get all part list
            DataTable dt_Part = dalItem.catSelect(text.Cat_Part);
            DataTable dt_PartForChecking = dt_Part.Copy();

            //get all join list(for sub material checking)
            DataTable dt_Join = dalJoin.Select();
            DataTable dt_JoinforChecking = dt_Join.Copy();

            //get all transfer history(for in out)
            string from = dtpFrom.Value.ToString("yyyy/MM/dd");
            string to = dtpTo.Value.ToString("yyyy/MM/dd");
            string PMMA = tool.getCustName(1);

            DataTable dt_PartTrfToPMMAHist = dalTrfHist.rangeItemToCustomerSearch(PMMA, from, to);
            #endregion

            int index = 1;
            int dgvRowIndex = 0;
            string previousMatCode = null;
            float totalMatUsed = 0;
            //foreach material list
            foreach (DataRow matRow in dt_ZeroCostMat.Rows)
            {
                string matCode = matRow[dalMat.MatCode].ToString();
                string matName = matRow[dalMat.MatName].ToString();
                string matCat = matRow[dalMat.MatCat].ToString();

                if (previousMatCode == null)
                {
                    previousMatCode = matCode;
                    totalMatUsed = 0;
                }

                else if (previousMatCode != matCode)
                {
                    if (dgvRowIndex - 1 >= 0)
                        dt_MatUsed.Rows[dgvRowIndex - 1][text.Header_TotalMaterialUsed_KG_Piece] = Math.Round(totalMatUsed, 2);

                    totalMatUsed = 0;

                    previousMatCode = matCode;
                    row_DGVSouce = dt_MatUsed.NewRow();
                    dt_MatUsed.Rows.Add(row_DGVSouce);
                    dgvRowIndex++;
                }

                if (matCat == text.Cat_SubMat)
                {
                    //foreach all join list (for sub material)
                    foreach (DataRow joinRow in dt_Join.Rows)
                    {
                        if (joinRow.RowState != DataRowState.Deleted)
                        {
                            string childCode = joinRow["child_code"].ToString();

                            if (childCode == matCode)
                            {
                                string parentCode = joinRow["parent_code"].ToString();

                                if (ifPMMAItem(parentCode, dt_JoinforChecking, dt_PMMAItem))
                                {

                                    if(matCode == "V44K61000")
                                    {
                                        float TEST = 0;
                                    }

                                    string parentName = joinRow["parent_name"].ToString();

                                    //Get delivered out data
                                    dt_DeliveredData.Rows.Clear();
                                    float deliveredQty = DeliveredToPMMAQty(parentCode, dt_JoinforChecking, dt_PartTrfToPMMAHist);

                                    float itemWeight = Convert.ToSingle(joinRow[dalItem.ItemQuoPWPcs].ToString()) + Convert.ToSingle(joinRow[dalItem.ItemQuoRWPcs].ToString());

                                    if (itemWeight <= 0)
                                    {
                                        itemWeight = Convert.ToSingle(joinRow[dalItem.ItemProPWPcs].ToString()) + Convert.ToSingle(joinRow[dalItem.ItemProRWPcs].ToString());
                                    }

                                    //float matUsedInKG = deliveredQty * itemWeight / 1000;
                                    float wastage = tool.getItemWastageAllowedFromDataTable(dt_Item, matCode);
                                    //int MatUsedWithWastage = (int)Math.Ceiling(deliveredQty + deliveredQty * wastage);

                                    float wastageAdd = deliveredQty * wastage;
                                    int MatUsedWithWastage = (int)Math.Ceiling(deliveredQty + wastageAdd);
                                    totalMatUsed += MatUsedWithWastage;

                                    #region New Test: with parent code/name
                                    foreach (DataRow row in dt_DeliveredData.Rows)
                                    {
                                        string DeliveredCode = row[text.Header_PartCode] == DBNull.Value? null : row[text.Header_PartCode].ToString();
                                        string DeliveredName = row[text.Header_PartName] == DBNull.Value ? null : row[text.Header_PartName].ToString();

                                        float qty = Convert.ToSingle(row[text.Header_Delivered]);
                                        string parent = DeliveredName + "(" + DeliveredCode + ")";

                                        if ((DeliveredName != null && DeliveredCode != null) || (deliveredQty == 0 && parentCode == DeliveredCode))
                                        {

                                            if (DeliveredCode == parentCode)
                                            {
                                                parent = "";
                                            }

                                            //matUsedInKG = qty * itemWeight / 1000;

                                            wastageAdd = qty * wastage;
                                            MatUsedWithWastage = (int)Math.Ceiling(qty + wastageAdd);

                                            row_DGVSouce = dt_MatUsed.NewRow();
                                            row_DGVSouce[text.Header_Index] = index;
                                            row_DGVSouce[text.Header_MatCode] = matCode;
                                            row_DGVSouce[text.Header_MatName] = matName;
                                            row_DGVSouce[text.Header_PartName] = parentName;
                                            row_DGVSouce[text.Header_PartCode] = parentCode;
                                            row_DGVSouce[text.Header_Parent] = parent;
                                            row_DGVSouce[text.Header_ItemWeight_G] = itemWeight;
                                            row_DGVSouce[text.Header_Wastage] = wastage;
                                            row_DGVSouce[text.Header_Delivered] = Math.Round(qty, 2);
                                            row_DGVSouce[text.Header_MaterialUsed_KG_Piece] = qty;
                                            row_DGVSouce[text.Header_MaterialUsedWithWastage] = MatUsedWithWastage;

                                            dt_MatUsed.Rows.Add(row_DGVSouce);
                                            dgvRowIndex++;
                                            index++;
                                        }
                                    }
                                    //joinRow.Delete();

                                    #endregion


                                   
                                }
                            }
                        }
                    }
                    dt_Join.AcceptChanges();
                }
                else
                {
                    //foreach all part list(for raw material)
                    foreach (DataRow partRow in dt_Part.Rows)
                    {
                        if (partRow.RowState != DataRowState.Deleted)
                        {
                            string itemMatCode = partRow[dalItem.ItemMaterial].ToString();

                            if (itemMatCode == matCode)
                            {
                                string itemCode = partRow[dalItem.ItemCode].ToString();

                                
                                //check if item belong to PMMA
                                if (ifPMMAItem(itemCode,dt_JoinforChecking,dt_PMMAItem))
                                {
                                    string itemName = partRow[dalItem.ItemName].ToString();

                                    float itemWeight = Convert.ToSingle(partRow[dalItem.ItemQuoPWPcs].ToString()) + Convert.ToSingle(partRow[dalItem.ItemQuoRWPcs].ToString());

                                    if (itemWeight <= 0)
                                    {
                                        itemWeight = Convert.ToSingle(partRow[dalItem.ItemProPWPcs].ToString()) + Convert.ToSingle(partRow[dalItem.ItemProRWPcs].ToString());
                                    }

                                    float wastage = partRow[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(partRow[dalItem.ItemWastage]);

                                    //Get delivered out data
                                    dt_DeliveredData.Rows.Clear();
                                    float deliveredQty = DeliveredToPMMAQty(itemCode, dt_JoinforChecking, dt_PartTrfToPMMAHist);
                                    float matUsedInKG = deliveredQty * itemWeight / 1000;
                                    totalMatUsed += (float)Math.Round(matUsedInKG + matUsedInKG * wastage, 2);


                                    #region New Test: with parent code/name
                                    foreach (DataRow row in dt_DeliveredData.Rows)
                                    {
                                        string DeliveredCode = row[text.Header_PartCode] == DBNull.Value ? null : row[text.Header_PartCode].ToString();

                                        //if(itemCode == "V99CJP0M0")
                                        //{
                                        //    float TEST = 0;
                                        //}
                                        string DeliveredName = row[text.Header_PartName] == DBNull.Value ? null : row[text.Header_PartName].ToString();
                                        float qty = Convert.ToSingle(row[text.Header_Delivered]);
                                        string parent = DeliveredName + "(" + DeliveredCode + ")";

                                        if ((DeliveredName != null && DeliveredCode != null) || (deliveredQty == 0 && itemCode == DeliveredCode))
                                        {
                                            row_DGVSouce = dt_MatUsed.NewRow();

                                            if (DeliveredCode == itemCode)
                                            {
                                                parent = "";
                                            }

                                            matUsedInKG = qty * itemWeight / 1000;

                                            row_DGVSouce[text.Header_Index] = index;
                                            row_DGVSouce[text.Header_MatCode] = matCode;
                                            row_DGVSouce[text.Header_MatName] = matName;
                                            row_DGVSouce[text.Header_PartName] = itemName;
                                            row_DGVSouce[text.Header_PartCode] = itemCode;
                                            row_DGVSouce[text.Header_Parent] = parent;
                                            row_DGVSouce[text.Header_ItemWeight_G] = itemWeight;
                                            row_DGVSouce[text.Header_Wastage] = wastage;
                                            row_DGVSouce[text.Header_Delivered] = Math.Round(qty, 2);
                                            row_DGVSouce[text.Header_MaterialUsed_KG_Piece] = Math.Round(matUsedInKG, 2);
                                            row_DGVSouce[text.Header_MaterialUsedWithWastage] = Math.Round(matUsedInKG + matUsedInKG * wastage, 2);

                                            dt_MatUsed.Rows.Add(row_DGVSouce);
                                            index++;
                                            dgvRowIndex++;
                                        }
                                    }

                                    //partRow.Delete();
                                    #endregion

                                    #region old way
                                    //row_DGVSouce = dt_MatUsed.NewRow();

                                    //row_DGVSouce[text.Header_Index] = index;
                                    //row_DGVSouce[text.Header_MatCode] = matCode;
                                    //row_DGVSouce[text.Header_MatName] = matName;
                                    //row_DGVSouce[text.Header_PartName] = itemName;
                                    //row_DGVSouce[text.Header_PartCode] = itemCode;
                                    //row_DGVSouce[text.Header_ItemWeight_G] = itemWeight;
                                    //row_DGVSouce[text.Header_Wastage] = wastage;
                                    //row_DGVSouce[text.Header_Delivered] = Math.Round(deliveredQty, 2);
                                    //row_DGVSouce[text.Header_MaterialUsed_KG_Piece] = Math.Round(matUsedInKG, 2);
                                    //row_DGVSouce[text.Header_MaterialUsedWithWastage] = Math.Round(matUsedInKG + matUsedInKG * wastage, 2);

                                    //dt_MatUsed.Rows.Add(row_DGVSouce);
                                    //index++;
                                    //dgvRowIndex++;

                                    //partRow.Delete();
                                    #endregion
                                }
                            }

                            if (!ifZeroCostMat(itemMatCode, dt_ZeroCostMat))
                            {
                                //partRow.Delete();
                            }
                        }
                    }
                    //dt_Part.AcceptChanges();
                }
            }

            if (dgvRowIndex - 1 >= 0)
                dt_MatUsed.Rows[dgvRowIndex - 1][text.Header_TotalMaterialUsed_KG_Piece] = Math.Round(totalMatUsed, 2); 

            if (dt_MatUsed.Rows.Count > 0)
            {
                dgvMatUsed.DataSource = dt_MatUsed;
                dgvMatUsed.ClearSelection();
            }
   
        }

        private float getMatInQty(string matCode, DataTable dt)
        {
            float InQty = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                {
                    string trfFrom = row[dalTrfHist.TrfFrom].ToString();
                    string trfTo = row[dalTrfHist.TrfTo].ToString();
                    string item = row[dalTrfHist.TrfItemCode].ToString();
                    //from PMMA to factory: IN
                    if (matCode == item && tool.getFactoryID(trfTo) != -1)
                    {
                        if (float.TryParse(row[dalTrfHist.TrfQty].ToString(), out float i))
                        {
                            InQty += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                        }

                    }
                }
            }

            return InQty;
        }

        private float getMatOutQty(string matCode, DataTable dt)
        {
            float OutQty = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row[dalTrfHist.TrfResult].ToString().Equals("Passed"))
                {
                    string trfFrom = row[dalTrfHist.TrfFrom].ToString();
                    string trfTo = row[dalTrfHist.TrfTo].ToString();
                    string item = row[dalTrfHist.TrfItemCode].ToString();
                    //from PMMA to factory: IN
                    if (matCode == item && tool.getFactoryID(trfFrom) != -1)
                    {
                        if (float.TryParse(row[dalTrfHist.TrfQty].ToString(), out float i))
                        {
                            OutQty += Convert.ToSingle(row[dalTrfHist.TrfQty]);
                        }

                    }
                }
            }

            return OutQty;

        }
        private float getMatWastage(string matCode, DataTable dt)
        {
            float wastage = 0;

            foreach(DataRow row in dt.Rows)
            {
                string itemCode = row[dalItem.ItemCode].ToString();
                if(matCode == itemCode)
                {
                    string itemCat = row[dalItem.ItemCat].ToString();
                    if(itemCat == text.Cat_SubMat)
                    {
                        wastage = row[dalItem.ItemWastage] == DBNull.Value ? 0 : Convert.ToSingle(row[dalItem.ItemWastage]);
                    }
                }
            }

            return wastage;
        }

        private float getOpenStockFromLastMonthBal(string matCode,DataTable dt)
        {
            float openStock = -1;
           
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string itemCode = row[dalPMMA.itemCode].ToString();
                    if (matCode == itemCode &&  float.TryParse(row[dalPMMA.OpenStock].ToString(), out float i))
                    {
                        openStock = Convert.ToSingle(row[dalPMMA.BalStock]);
                        
                    }

                }
                
            }

            return openStock;
        }

        private DataRow getPMMADataRow(string matCode, string month, string year, DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string itemCode = row[dalPMMA.itemCode].ToString();
                    string PMMAMonth = row[dalPMMA.Month].ToString();
                    string PMMAYear = row[dalPMMA.Year].ToString();

                    if (matCode == itemCode && PMMAMonth == month && PMMAYear == year)
                    {
                        return row;
                    }

                }

            }

            return null;
        }

        private void LoadMatStockData()
        {
            int index = 1;
            dt_MatStock = NewStockTable();
            DataRow row_StockSouce;

            //get mat in history)
            string from = dtpFrom.Value.ToString("yyyy/MM/dd");
            string to = dtpTo.Value.ToString("yyyy/MM/dd");
            string PMMA = tool.getCustName(1);
            string month = cmbStockMonth.Text;
            string year = cmbStockYear.Text;

            string nextMonth;
            string nextYear;
            if (month.Equals("12"))
            {
                nextMonth = "1";
                nextYear = (Convert.ToInt32(year) + 1).ToString();
            }
            else
            {
                nextMonth = (Convert.ToInt32(month) + 1).ToString();
                nextYear = year;
            }

            string lastMonth;
            string lastYear;
            if (month.Equals("1"))
            {
                lastMonth = "12";
                lastYear = (Convert.ToInt32(year) - 1).ToString();
            }
            else
            {
                lastMonth = (Convert.ToInt32(month) - 1).ToString();
                lastYear = year;
            }

            DataTable dt_MatInFromPMMAHist = dalTrfHist.rangeMaterialInFromPMMASearch(from, to);

            DataTable dt_MatOutToPMMAHist = dalTrfHist.rangeMaterialOutToPMMASearch(from, to);

            dt_PMMA = dalPMMA.SearchByNewDate(month, year);
            DataTable dt_PMMALastMonth = dalPMMA.SearchByNewDate(lastMonth, lastYear);

            foreach (DataRow row in dt_MatUsed.Rows)
            {
                float Out = row[text.Header_TotalMaterialUsed_KG_Piece] == DBNull.Value ? -1 : (float)Math.Round(Convert.ToDouble(row[text.Header_TotalMaterialUsed_KG_Piece]), 2);

                if (Out != -1)
                {
                    string matCode = row[text.Header_MatCode].ToString();
                    float adjustQty = 0;
                    string note = "";
                    DataRow MatData = getPMMADataRow(matCode, month, year, dt_PMMA);

                    string matName = row[text.Header_MatName].ToString();
                    float openStock = getOpenStockFromLastMonthBal(matCode, dt_PMMALastMonth);

                    if(MatData != null)
                    {
                        adjustQty = MatData[dalPMMA.Adjust] == DBNull.Value ? 0 : (float)Math.Round(Convert.ToSingle(MatData[dalPMMA.Adjust]), 2);
                        note = MatData[dalPMMA.Note] == DBNull.Value ? "" : MatData[dalPMMA.Note].ToString();
                    }

                    float outToPMMAQty = getMatOutQty(matCode, dt_MatOutToPMMAHist);
                    Out += outToPMMAQty;

                    float inQty = getMatInQty(matCode, dt_MatInFromPMMAHist);
                    float wastage = getMatWastage(matCode, dt_Item);
                    float balance = (float)Math.Round(openStock + inQty - Out + adjustQty, 2);

                  
                    //check transfer record if transfered to PMMA within current period

                    row_StockSouce = dt_MatStock.NewRow();

                    row_StockSouce[text.Header_Index] = index;
                    row_StockSouce[text.Header_MatCode] = matCode;
                    row_StockSouce[text.Header_MatName] = matName;
                    row_StockSouce[text.Header_OpeningStock] = openStock;
                    row_StockSouce[text.Header_In_KG_Piece] = inQty;
                    row_StockSouce[text.Header_Out_KG_Piece] = Out;
                    row_StockSouce[text.Header_Wastage] = wastage;
                    row_StockSouce[text.Header_Adjust] = adjustQty;
                    row_StockSouce[text.Header_Note] = note;
                    row_StockSouce[text.Header_BalStock] = balance;

                    dt_MatStock.Rows.Add(row_StockSouce);

                    index++;

                    if (Convert.ToInt32(month) == DateTime.Now.Month || Convert.ToInt32(year) > DateTime.Now.Year)
                    {
                        uItem.item_code = matCode;
                        uItem.item_last_pmma_qty = 0;
                        uItem.item_pmma_qty = balance;
                        uItem.item_updtd_date = DateTime.Now;
                        uItem.item_updtd_by = MainDashboard.USER_ID;

                        bool itemPMMMAQtyUpdateSuccess = dalItem.UpdatePMMAQty(uItem);

                        if (!itemPMMMAQtyUpdateSuccess)
                        {
                            MessageBox.Show("Failed to updated item pmma qty(@item dal)");
                        }
                    }


                    //update/insert to database
                    uPMMA.pmma_item_code = matCode;
                    uPMMA.pmma_openning_stock = openStock;
                    uPMMA.pmma_in = inQty;
                    uPMMA.pmma_out = Out;
                    uPMMA.pmma_wastage = wastage;
                    uPMMA.pmma_adjust = adjustQty;
                    uPMMA.pmma_note = note;
                    uPMMA.pmma_bal_stock = balance;
                    uPMMA.pmma_month = month;
                    uPMMA.pmma_year = year;
                    uPMMA.pmma_added_date = DateTime.Now;
                    uPMMA.pmma_added_by = MainDashboard.USER_ID;
                    uPMMA.pmma_updated_date = uPMMA.pmma_added_date;
                    uPMMA.pmma_updated_by = uPMMA.pmma_added_by;

                    if (!dalPMMA.InsertOrUpdate(uPMMA))
                    {
                        MessageBox.Show("Failed to insert/update data for selected month!");
                    }
                    else
                    {
                        //insert/update next month open stock
                        uPMMA.pmma_openning_stock = balance;
                        uPMMA.pmma_month = nextMonth;
                        uPMMA.pmma_year = nextYear;

                        if (!dalPMMA.InsertOrUpdateNextMonthOpenStock(uPMMA))
                        {
                            MessageBox.Show("Failed to insert/update open stock for next month!");
                        }
                    }
                }
               
            }

            if (dt_MatStock.Rows.Count > 0)
            {
                dgvMatStock.DataSource = dt_MatStock;
                dgvMatStock.ClearSelection();
            }
        }

        private bool ifPMMAItem(string itemCode, DataTable dt_Join, DataTable dt_PMMAItem)
        {
            bool isPMMAItem = false;

            foreach(DataRow pmma in dt_PMMAItem.Rows)
            {
                if(itemCode == pmma[dalItem.ItemCode].ToString())
                {
                    return true;
                }
            }

            foreach(DataRow join in dt_Join.Rows)
            {
                if(itemCode == join["child_code"].ToString())
                {
                    string parentCode = join["parent_code"].ToString();
                    isPMMAItem = ifPMMAItem(parentCode, dt_Join, dt_PMMAItem);
                }
            }

            return isPMMAItem;
        }

        private float DeliveredToPMMAQty(string itemCode, DataTable dt_Join, DataTable dt_ToPMMA)
        {
            float DeliveredQty = 0;
            string dbItemCode = null, dbItemName = null;
            DataRow row_Delivered;
            row_Delivered = dt_DeliveredData.NewRow();

            foreach (DataRow trfHistRow in dt_ToPMMA.Rows)
            {
                dbItemCode = trfHistRow[dalTrfHist.TrfItemCode].ToString();
                string result = trfHistRow[dalTrfHist.TrfResult].ToString();
                //get single out
                if (itemCode == dbItemCode && result == "Passed")
                {
                    dbItemName = trfHistRow[dalItem.ItemName].ToString();
                    int qty = trfHistRow[dalTrfHist.TrfQty] == DBNull.Value ? 0 : Convert.ToInt32(trfHistRow[dalTrfHist.TrfQty]);
                    DeliveredQty += qty;
                }

            }

            row_Delivered[text.Header_PartCode] = itemCode;
            row_Delivered[text.Header_PartName] = dbItemName;
            row_Delivered[text.Header_Delivered] = DeliveredQty;
            dt_DeliveredData.Rows.Add(row_Delivered);

            foreach (DataRow join in dt_Join.Rows)
            {
                if (itemCode == join["child_code"].ToString())
                {
                    float joinQty = Convert.ToSingle(join["join_qty"]);
                    string parentCode = join["parent_code"].ToString();
                    DeliveredQty += joinQty * DeliveredToPMMAQty(parentCode, dt_Join, dt_ToPMMA);
                }
            }

            return DeliveredQty;
        }

        private bool ifZeroCostMat(string matCode, DataTable dt_ZeroCostMat)
        {
            foreach(DataRow row in dt_ZeroCostMat.Rows)
            {
                if(matCode == row[dalMat.MatCode].ToString())
                {
                    return true;
                }
            }

            return false;
        }

        private void cmbStockMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMatStock.DataSource = null;
            dgvMatUsed.DataSource = null;
            getStartandEndDate();
            editModeAvailable = false;
            cbEditMode.Checked = false;
            editModeAvailable = true;
        }

        public void StartForm()
        {
            
            try
            {
                System.Windows.Forms.Application.Run(new frmLoading());
            }
            catch (ThreadAbortException)
            {

            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            //Thread t = null;
            //bool aborted = false;

            try
            {
                //t = new Thread(new ThreadStart(StartForm));
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                frmLoading.ShowLoadingScreen();
                editModeAvailable = false;
                cbEditMode.Checked = false;
                editModeAvailable = true;

                editedCellRow = -1;

                LoadMatUsedData();

                if (!checkMatUsedOnly)
                {
                    LoadMatStockData();
                }
            }
            catch (ThreadAbortException)
            {
                // ignore it
                //aborted = true;
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                //if (!aborted)
                //    t.Abort();
                frmLoading.CloseForm();
                Cursor = Cursors.Arrow; // change cursor to normal type
            }

        }

        private void lblStockChangeDate_Click(object sender, EventArgs e)
        {
            lblChangeDate.ForeColor = Color.Purple;
            frmPMMADateEdit frm = new frmPMMADateEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            lblChangeDate.ForeColor = Color.Blue;

            if(frmPMMADateEdit.dateChanged)
            {
                dgvMatStock.DataSource = null;
                dgvMatUsed.DataSource = null;
                
                getStartandEndDate();
            }
        }

        #endregion

        private void dgvMatUsed_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvMatUsedUIEdit(dgvMatUsed);
            dgvMatUsed.AutoResizeColumns();
        }

        private void dgvMatUsed_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMatUsed;
            dgv.SuspendLayout();

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == text.Header_MatCode)
            {
                string matCode = dgv.Rows[row].Cells[text.Header_MatCode].Value.ToString();

                if (matCode == "")
                {
                    dgv.Rows[row].Height = 5;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    dgv.Rows[row].Height = 50;
                }
            }

            dgv.ResumeLayout();
        }

        private void dgvMatUsed_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbStockYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStartandEndDate();
        }

        private void dgvMatStock_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvStockUIEdit(dgvMatStock);
            dgvMatStock.AutoResizeColumns();
        }

        private void dgvMatStock_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMatStock;
            dgv.SuspendLayout();

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == text.Header_MatCode)
            {
                string matCode = dgv.Rows[row].Cells[text.Header_MatCode].Value.ToString();

                if (matCode != "")
                {
                    dgv.Rows[row].Height = 50;
                }
            }
            else if (dgv.Columns[col].Name == text.Header_OpeningStock)
            {
                float balance = dgv.Rows[row].Cells[text.Header_OpeningStock].Value == null ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[text.Header_OpeningStock].Value);

                if (balance < 0)
                {
                    dgv.Rows[row].Cells[text.Header_OpeningStock].Style.ForeColor = Color.Red;
                }
            }

            else if(dgv.Columns[col].Name == text.Header_BalStock)
            {
                float balance = dgv.Rows[row].Cells[text.Header_BalStock].Value == null ? 0 : Convert.ToSingle(dgv.Rows[row].Cells[text.Header_BalStock].Value);

                if(balance < 0)
                {
                    dgv.Rows[row].Cells[text.Header_BalStock].Style.ForeColor = Color.Red;
                }
            }
            dgv.ResumeLayout();
        }

        private void dgvMatStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbEditMode_CheckedChanged(object sender, EventArgs e)
        {
            if(editModeAvailable)
            {
                DataGridView dgv = dgvMatStock;
                DataTable dt = (DataTable)dgvMatStock.DataSource;

                if (dt == null)
                {
                    MessageBox.Show("No data to edit.");
                    editModeAvailable = false;
                    cbEditMode.Checked = false;
                    editModeAvailable = true;
                }
                else
                {
                    if (cbEditMode.Checked)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

                            dgv.Columns[text.Header_Adjust].DefaultCellStyle.BackColor = SystemColors.Info;
                            //dgv.Columns[text.Header_Wastage].DefaultCellStyle.BackColor = SystemColors.Info;
                            dgv.Columns[text.Header_Note].DefaultCellStyle.BackColor = SystemColors.Info;
                        }
                        else
                        {
                            MessageBox.Show("No data to edit.");
                            editModeAvailable = false;
                            cbEditMode.Checked = false;
                            editModeAvailable = true;
                        }
                    }
                    else
                    {
                        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        dgv.Columns[text.Header_Adjust].DefaultCellStyle.BackColor = Color.Gainsboro;
                        //dgv.Columns[text.Header_Wastage].DefaultCellStyle.BackColor = Color.Gainsboro;
                        dgv.Columns[text.Header_Note].DefaultCellStyle.BackColor = Color.Gainsboro;
                    }
                }
            }
        }

        private void dgvMatStock_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvMatStock;

            int col = e.ColumnIndex;
            int row = e.RowIndex;

            if (cbEditMode.Checked &&( dgv.Columns[col].Name == text.Header_Adjust  || dgv.Columns[col].Name == text.Header_Note))
            {
                oldAdjustQty = dgv.Rows[row].Cells[text.Header_Adjust].Value.ToString();
                oldNote = dgv.Rows[row].Cells[text.Header_Note].Value.ToString();

                dgv.ReadOnly = false;
                dgvMatStock.BeginEdit(true);
            }
            else
            {
                dgv.ReadOnly = true;
            }
        }

        private void dgvMatStock_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Adjust_KeyPress);

            int currentCol = dgvMatStock.CurrentCell.ColumnIndex;
            if (currentCol == 6 || currentCol == 7) //Desired Column
            {
                editedCellRow = dgvMatStock.CurrentCell.RowIndex;
                if (e.Control is System.Windows.Forms.TextBox tb)
                {
                    tb.KeyPress += new KeyPressEventHandler(Adjust_KeyPress);
                }
            }
            else if (currentCol == 8)
            {
                editedCellRow = dgvMatStock.CurrentCell.RowIndex;
            }
        }

        private void Adjust_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.' & e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void dgvMatStock_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvMatStock;
            string adjust = dgv.Rows[editedCellRow].Cells[text.Header_Adjust].Value.ToString();
            string note = dgv.Rows[editedCellRow].Cells[text.Header_Note].Value.ToString();

            if (editedCellRow != -1 && (adjust != oldAdjustQty || note != oldNote))
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                string matCode = dgv.Rows[editedCellRow].Cells[text.Header_MatCode].Value.ToString();
                string matName = dgv.Rows[editedCellRow].Cells[text.Header_MatName].Value.ToString();

                string open = dgv.Rows[editedCellRow].Cells[text.Header_OpeningStock].Value.ToString();
                string StockIn = dgv.Rows[editedCellRow].Cells[text.Header_In_KG_Piece].Value.ToString();
                string StockOut = dgv.Rows[editedCellRow].Cells[text.Header_Out_KG_Piece].Value.ToString();
               
                string balance = dgv.Rows[editedCellRow].Cells[text.Header_BalStock].Value.ToString();
                

                float openQty = string.IsNullOrEmpty(open) ? 0 : Convert.ToSingle(open);
                float inQty = string.IsNullOrEmpty(StockIn) ? 0 : Convert.ToSingle(StockIn);
                float outQty = string.IsNullOrEmpty(StockOut) ? 0 : Convert.ToSingle(StockOut);

                float adjustQty = string.IsNullOrEmpty(adjust) ? 0 : Convert.ToSingle(adjust);
                float balanceQty = string.IsNullOrEmpty(balance) ? 0 : Convert.ToSingle(balance);

                balanceQty = (float)Math.Round(openQty + inQty - outQty + adjustQty, 2);

                //change dgv data
                dgv.Rows[editedCellRow].Cells[text.Header_Adjust].Value = adjustQty;
                dgv.Rows[editedCellRow].Cells[text.Header_BalStock].Value = balanceQty;

                if(balanceQty < 0)
                dgv.Rows[editedCellRow].Cells[text.Header_BalStock].Style.ForeColor = Color.Red;
                else
                    dgv.Rows[editedCellRow].Cells[text.Header_BalStock].Style.ForeColor = Color.Black;
                string month = cmbStockMonth.Text;
                string year = cmbStockYear.Text;

                //update bal to db
                uPMMA.pmma_item_code = matCode;
                uPMMA.pmma_openning_stock = openQty;
                uPMMA.pmma_bal_stock = balanceQty;
                uPMMA.pmma_adjust = adjustQty;
                uPMMA.pmma_note = note;
                uPMMA.pmma_month = month;
                uPMMA.pmma_year = year;
                uPMMA.pmma_updated_date = DateTime.Now;
                uPMMA.pmma_updated_by = MainDashboard.USER_ID;

                if (!dalPMMA.InsertOrUpdate(uPMMA))
                {
                    MessageBox.Show("Failed to update adjusted balance stock!");
                }

                //update next month open stock
                //insert/update next month open stock
                string nextMonth;
                string nextYear;
                if (month.Equals("12"))
                {
                    nextMonth = "1";
                    nextYear = (Convert.ToInt32(year) + 1).ToString();
                }
                else
                {
                    nextMonth = (Convert.ToInt32(month) + 1).ToString();
                    nextYear = year;
                }

                uPMMA.pmma_item_code = matCode;
                uPMMA.pmma_openning_stock = balanceQty;
                uPMMA.pmma_month = nextMonth;
                uPMMA.pmma_year = nextYear;
                uPMMA.pmma_added_date = DateTime.Now;
                uPMMA.pmma_added_by = MainDashboard.USER_ID;

                if (!dalPMMA.InsertOrUpdateNextMonthOpenStock(uPMMA))
                {
                    MessageBox.Show("Failed to insert/update open stock for next month!");
                }
                else
                {
                    if(string.IsNullOrEmpty(oldNote))
                    {
                        oldNote = "EMPTY";
                    }

                    if (string.IsNullOrEmpty(note))
                    {
                        note = "EMPTY";
                    }

                    string monthName = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1).ToString("MMM", CultureInfo.InvariantCulture).ToUpper();

                    //history
                    if (oldAdjustQty != adjust)
                    tool.historyRecord("PMMA ZERO COST ADJUST", matCode+"("+ monthName +year+") :" + "ADJUST QTY FROM "+oldAdjustQty+" TO "+adjust, uPMMA.pmma_added_date, uPMMA.pmma_added_by);

                    if (oldNote != note)
                        tool.historyRecord("PMMA ZERO COST ADJUST", matCode + "(" + monthName + year + ") :" + "NOTE EDITED FROM " + oldNote + " TO " + note, uPMMA.pmma_added_date, uPMMA.pmma_added_by);
                }
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            if(!checkMatUsedOnly)
            {
                fileName = "ZeroCostReport(STOCK)_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            }
            else
            {
                fileName = "ZeroCostReport(MATERIAL USED)_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            }
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
            cbEditMode.Checked = false;

            Thread t = null;
            bool aborted = false;

            if ((checkMatUsedOnly && dgvMatUsed == null) || (!checkMatUsedOnly && dgvMatStock == null))
            {
                MessageBox.Show("No data found!");
            }
            else if ((checkMatUsedOnly && dgvMatUsed.Rows.Count <= 0 ) || (!checkMatUsedOnly && dgvMatStock.Rows.Count <= 0))
            {
                MessageBox.Show("No data found!");
            }
            else
            {
                try
                {
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    t = new Thread(new ThreadStart(StartForm));
                    
                    if (checkMatUsedOnly)
                    dgvMatUsed.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    else
                        dgvMatStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    SaveFileDialog sfd = new SaveFileDialog();
                    DataGridView dgv;
                    string path = @"D:\StockAssistant\Document\ZeroCostReport";
                    Directory.CreateDirectory(path);
                    sfd.InitialDirectory = path;

                    sfd.Filter = "Excel Documents (*.xls)|*.xls";
                    sfd.FileName = setFileName();

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        progressBar1.Show();
                        //t.Start();
                        tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                        if (checkMatUsedOnly)
                        {
                            dgv = dgvMatUsed;
                        }


                        else
                        {
                            dgv = dgvMatStock;
                        }

                        dgv.MultiSelect = true;

                        dgv.Focus();
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

                        string from = dtpFrom.Text.ToString();
                        string to = dtpTo.Text.ToString();

                        int month = Convert.ToInt32(cmbStockMonth.Text);
                        int year = Convert.ToInt32(cmbStockYear.Text);

                        string monthName = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture).ToUpper();

                        

                        if (checkMatUsedOnly)
                        {
                            xlWorkSheet.Name = "ZERO COST MAT USED";
                            xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 ZERO COST MAT USED ( "+from+"-->"+ to+" )";
                            dgv = dgvMatUsed;
                        }
                           

                        else
                        {
                            xlWorkSheet.Name = "ZERO COST STOCK";
                            xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&12 ZERO COST STOCK ( " + from + "-->" + to + " )";
                            dgv = dgvMatStock;
                        }
                           


                        #region Save data to Sheet

                        
                        //Header and Footer setup
                        xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + monthName + " "+year;
                        
                        xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                        xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri,Bold\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + " at " + DateTime.Now;

                        //Page setup
                        xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                        xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;


                        xlWorkSheet.PageSetup.Zoom = false;
                        xlWorkSheet.PageSetup.CenterHorizontally = true;

                        xlWorkSheet.PageSetup.FitToPagesWide = 1;
                        xlWorkSheet.PageSetup.FitToPagesTall = false;

                        double pointToCMRate = 0.035;
                        xlWorkSheet.PageSetup.TopMargin = 1.8 / pointToCMRate;
                        xlWorkSheet.PageSetup.BottomMargin = 1.0 / pointToCMRate;
                        xlWorkSheet.PageSetup.HeaderMargin = 1.0 / pointToCMRate;
                        xlWorkSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                        xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                        xlWorkSheet.PageSetup.RightMargin = 0.5 / pointToCMRate;

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

                        if (true)
                        {
                            int rowMax = dgv.RowCount - 1;
                            int colMax = dgv.ColumnCount - 1;

                            for (int i = 0; i <= rowMax; i++)
                            {
                                for (int j = 0; j <= colMax; j++)
                                {
                                    if (checkMatUsedOnly)
                                    {
                                        Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];
                                        //range.Rows.RowHeight = 30;

                                        range.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[j].InheritedStyle.ForeColor);

                                        if (j == 5)
                                        {
                                            range.Cells.Font.Italic = true;
                                            //range.Font.Size = 16;
                                            //range.Cells.Font.Color = ColorTranslator.ToOle(Color.Black);
                                        }

                                        if (j == 11)
                                        {
                                            range.Cells.Font.Bold = true;
                                            range.Font.Size = 16;
                                            //range.Cells.Font.Color = ColorTranslator.ToOle(Color.Black);
                                        }

                                        if (j <= 5 && j >= 1)
                                        {
                                            range.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                            range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                        }
                                        else
                                        {
                                            range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                            range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                        }

                                        if (dgv.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Gainsboro)
                                        {
                                            range.Interior.Color = Color.White;
                                            range.Rows.RowHeight = 26;
                                        }
                                        else if (dgv.Rows[i].Cells[j].InheritedStyle.BackColor == Color.FromArgb(64, 64, 64))
                                        {
                                            range.Rows.RowHeight = 2;
                                            range.Interior.Color = Color.Black;
                                        }
                                        else
                                        {
                                            range.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[j].InheritedStyle.BackColor);
                                            range.Rows.RowHeight = 26;
                                        }
                                    }
                                    else
                                    {
                                        Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];
                                        //range.Rows.RowHeight = 30;

                                        range.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[j].InheritedStyle.ForeColor);

                                        if (j == 3 || j == 9)
                                        {
                                            //range.Cells.Font.Bold = true;
                                            //range.Font.Size = 14;

                                            float qty = Convert.ToSingle(dgv.Rows[i].Cells[j].Value);

                                            if(qty < 0)
                                            {
                                                range.Cells.Font.Color = ColorTranslator.ToOle(Color.Red);
                                            }
                                            else
                                            {
                                                range.Cells.Font.Color = ColorTranslator.ToOle(Color.Black);
                                            }

                                        }

                                        //if (j == 1 || j == 2)
                                        //{
                                        //    range.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                        //    range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                        //}
                                        //else
                                        //{
                                        //    range.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                                        //    range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                        //}
                                    }                                           
                                }

                                Int32 percentage = ((i + 1) * 100) / (dgv.RowCount - 2);
                                if (percentage >= 100)
                                {
                                    percentage = 100;
                                }
                                bgWorker.ReportProgress(percentage);
                            }
                        }


                        bgWorker.ReportProgress(100);
                        Thread.Sleep(1000);

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
                        dgv.ClearSelection();

                        // Open the newly saved excel file
                        if (File.Exists(sfd.FileName))
                            System.Diagnostics.Process.Start(sfd.FileName);
                    }

                    Cursor = Cursors.Arrow; // change cursor to normal type
                    
                }
                catch (ThreadAbortException)
                {
                    // ignore it
                    aborted = true;
                }
                catch (Exception ex)
                {
                    tool.saveToTextAndMessageToUser(ex);
                }
                
                finally
                {
                    if (!aborted)
                        t.Abort();

                    progressBar1.Visible = false;
                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
            }

        }

        private void copyAlltoClipboard()
        {
            DataObject dataObj;

            if (checkMatUsedOnly)
            {
                dgvMatUsed.SelectAll();
                dataObj = dgvMatUsed.GetClipboardContent();
            }
                
            else
            {
                dgvMatStock.SelectAll();
                dataObj = dgvMatStock.GetClipboardContent();
            }
               
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void copyAlltoClipboard(DataGridView dgv)
        {
            DataObject dataObj;
            dgv.SelectAll();
            dataObj = dgv.GetClipboardContent();
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

            cbEditMode.Checked = false;
            if ( dgvMatUsed == null || dgvMatStock == null)
            {
                MessageBox.Show("No data found!");
            }
            else if (dgvMatUsed.Rows.Count <= 0 || dgvMatStock.Rows.Count <= 0)
            {
                MessageBox.Show("No data found!");
            }
            else
            {
                try
                {
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    cbEditMode.Checked = false;

                    //dgvMatStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    //dgvMatStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    SaveFileDialog sfd = new SaveFileDialog();
                    string path2 = @"D:\StockAssistant\Document\ZeroCostReport";

                    Directory.CreateDirectory(path2);
                    sfd.InitialDirectory = path2;
                    sfd.Filter = "Excel Documents (*.xls)|*.xls";
                    sfd.FileName = "ZeroCostReport(ALL)_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";

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
                        dgvMatStock.ClearSelection();
                        dgvMatUsed.ClearSelection();
                    }

                    bgWorker.ReportProgress(100);
                    Thread.Sleep(1000);

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

                object misValue = System.Reflection.Missing.Value;

                //load different data list to datagridview but changing the comboBox selected index


                #region Mat Stock To Sheet

                switchToStockList();

                Worksheet addedSheet = null;

                int count = g_Workbook.Worksheets.Count;
                addedSheet = g_Workbook.Worksheets.Add(Type.Missing,
                        g_Workbook.Worksheets[count], Type.Missing, Type.Missing);
                addedSheet.Name = "STOCK";

                DataGridView dgv;
                dgv = dgvMatStock;
                string from = dtpFrom.Text.ToString();
                string to = dtpTo.Text.ToString();
                int month = Convert.ToInt32(cmbStockMonth.Text);
                int year = Convert.ToInt32(cmbStockYear.Text);
                string monthName = new DateTime(year, month, 1).ToString("MMM", CultureInfo.InvariantCulture).ToUpper();
                addedSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&12 ZERO COST STOCK ( " + from + "-->" + to + " )";


                //Header and Footer setup
                addedSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + monthName + " " + year;

                addedSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                addedSheet.PageSetup.CenterFooter = "&\"Calibri,Bold\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + " at " + DateTime.Now;

                //Page setup
                addedSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                addedSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                addedSheet.PageSetup.Zoom = false;
                addedSheet.PageSetup.CenterHorizontally = true;
                addedSheet.PageSetup.FitToPagesWide = 1;
                addedSheet.PageSetup.FitToPagesTall = false;
                double pointToCMRate = 0.035;
                addedSheet.PageSetup.TopMargin = 1.8 / pointToCMRate;
                addedSheet.PageSetup.BottomMargin = 1.0 / pointToCMRate;
                addedSheet.PageSetup.HeaderMargin = 1.0 / pointToCMRate;
                addedSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                addedSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                addedSheet.PageSetup.RightMargin = 0.5 / pointToCMRate;
                addedSheet.PageSetup.PrintTitleRows = "$1:$1";

                copyAlltoClipboard(dgv);
                addedSheet.Select();

                Range CR = (Range)addedSheet.Cells[1, 1];
                CR.Select();
                addedSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                Range tRange = addedSheet.UsedRange;
                tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                tRange.Borders.Weight = XlBorderWeight.xlThin;
                tRange.Font.Size = 12;
                tRange.Font.Name = "Calibri";
                tRange.EntireRow.AutoFit();
                tRange.EntireColumn.AutoFit();
                tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);//change first row back color to light grey

                if (!cbFastMode.Checked)
                {
                    int rowMax = dgv.RowCount - 1;
                    int colMax = dgv.ColumnCount - 1;

                    for (int i = 0; i <= rowMax; i++)
                    {
                        for (int j = 0; j <= colMax; j++)
                        {
                            Range range = (Range)addedSheet.Cells[i + 2, j + 1];
                            //range.Rows.RowHeight = 30;

                            range.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[j].InheritedStyle.ForeColor);

                            if (j == 3 || j == 9)
                            {
                                //range.Cells.Font.Bold = true;
                                //range.Font.Size = 14;

                                float qty = Convert.ToSingle(dgv.Rows[i].Cells[j].Value);

                                if (qty < 0)
                                {
                                    range.Cells.Font.Color = ColorTranslator.ToOle(Color.Red);
                                }
                                else
                                {
                                    range.Cells.Font.Color = ColorTranslator.ToOle(Color.Black);
                                }

                            }

                            //if (j == 1 || j == 2)
                            //{
                            //    range.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                            //    range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                            //}
                            //else
                            //{
                            //    range.Cells.HorizontalAlignment = XlHAlign.xlHAlignRight;
                            //    range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                            //}
                        }

                        Int32 percentage = ((i + 1) * 100 / (dgv.RowCount - 2)/2);
                        if (percentage >= 100)
                        {
                            percentage = 100;
                        }
                        bgWorker.ReportProgress(percentage);
                    }
                }

                releaseObject(addedSheet);
                Clipboard.Clear();
                dgv.ClearSelection();

                #endregion

                #region Mat Used To Sheet

                switchToMatUsedReport();

                addedSheet = null;

                count = g_Workbook.Worksheets.Count;
                addedSheet = g_Workbook.Worksheets.Add(Type.Missing,
                        g_Workbook.Worksheets[count], Type.Missing, Type.Missing);

                addedSheet.Name = "MAT USED";

                dgv = dgvMatUsed;
                
                addedSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 ZERO COST MATERIAL USED REPORT( " + from + "-->" + to + " )";


                //Header and Footer setup
                addedSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + monthName + " " + year;

                addedSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                addedSheet.PageSetup.CenterFooter = "&\"Calibri,Bold\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + " at " + DateTime.Now;

                //Page setup
                addedSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                addedSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                addedSheet.PageSetup.Zoom = false;
                addedSheet.PageSetup.CenterHorizontally = true;
                addedSheet.PageSetup.FitToPagesWide = 1;
                addedSheet.PageSetup.FitToPagesTall = false;
                pointToCMRate = 0.035;
                addedSheet.PageSetup.TopMargin = 1.8 / pointToCMRate;
                addedSheet.PageSetup.BottomMargin = 1.0 / pointToCMRate;
                addedSheet.PageSetup.HeaderMargin = 1.0 / pointToCMRate;
                addedSheet.PageSetup.FooterMargin = 0.6 / pointToCMRate;
                addedSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
                addedSheet.PageSetup.RightMargin = 0.5 / pointToCMRate;
                addedSheet.PageSetup.PrintTitleRows = "$1:$1";

                copyAlltoClipboard(dgv);
                addedSheet.Select();

                CR = (Range)addedSheet.Cells[1, 1];
                CR.Select();
                addedSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                tRange = addedSheet.UsedRange;
                tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                tRange.Borders.Weight = XlBorderWeight.xlThin;
                tRange.Font.Size = 12;
                tRange.Rows.RowHeight = 26;
                tRange.Font.Name = "Calibri";
                tRange.EntireRow.AutoFit();
                tRange.EntireColumn.AutoFit();
                tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);//change first row back color to light grey

                if (!cbFastMode.Checked)
                {
                    int rowMax = dgv.RowCount - 1;
                    int colMax = dgv.ColumnCount - 1;

                    for (int i = 0; i <= rowMax; i++)
                    {
                        for (int j = 0; j <= colMax; j++)
                        {
                            Range range = (Range)addedSheet.Cells[i + 2, j + 1];
                            //range.Rows.RowHeight = 30;

                            range.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[j].InheritedStyle.ForeColor);

                            if (j == 5)
                            {
                                range.Cells.Font.Italic = true;
                                //range.Font.Size = 16;
                                //range.Cells.Font.Color = ColorTranslator.ToOle(Color.Black);
                            }

                            if (j == 11)
                            {
                                range.Cells.Font.Bold = true;
                                range.Font.Size = 16;
                                //range.Cells.Font.Color = ColorTranslator.ToOle(Color.Black);
                            }

                            if (j <= 5 && j >= 1)
                            {
                                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                            }
                            else
                            {
                                range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                            }

                            if (dgv.Rows[i].Cells[j].InheritedStyle.BackColor == Color.Gainsboro)
                            {
                                //range.Interior.Color = Color.White;
                                //range.Rows.RowHeight = 26;
                            }
                            else if (dgv.Rows[i].Cells[j].InheritedStyle.BackColor == Color.FromArgb(64, 64, 64))
                            {
                                range.Rows.RowHeight = 2;
                                range.Interior.Color = Color.Black;
                            }
                            else
                            {
                                //range.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[j].InheritedStyle.BackColor);
                                //range.Rows.RowHeight = 26;
                            }
                        }

                        Int32 percentage = ((i + 1) * 100 / (dgv.RowCount - 2))/2 + 50;
                        if (percentage >= 100)
                        {
                            percentage = 100;
                        }
                        bgWorker.ReportProgress(percentage);
                    }
                }

                releaseObject(addedSheet);
                Clipboard.Clear();
                dgv.ClearSelection();

                #endregion

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

        private void frmNewPMMA_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.PMMAFormOpen = false;
        }
    }
}


