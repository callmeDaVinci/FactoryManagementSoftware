using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMatPlanningList : Form
    {
        public frmMatPlanningList()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvMatListByMat, true);
            ShowFilter(false);
            //loadSortingData();

            LoadMatListByMat();
        }

        #region Variable / Object Declare

        facStockDAL dalStock = new facStockDAL();
        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();
        planningDAL dalPlan = new planningDAL();
        itemDAL dalItem = new itemDAL();
        MacDAL dalMac = new MacDAL();
        Tool tool = new Tool();
        Text text = new Text();
        joinBLL uJoin = new joinBLL();

        readonly string Filter_ShowMore = "MORE FILTER";
        readonly string Filter_HIDE = "HIDE FILTER";

        readonly string headerID = "PLAN";
        readonly string headerMatUse = "MAT. USE (KG/PIECE)";

        readonly string headerTargetQty = "TARGET QTY";
        readonly string headerAbleProduceQty = "ABLE PRODUCE";
        readonly string headerPartCode = "PART CODE";
        readonly string headerPartName = "PART NAME";

        //readonly string headerItem = "PLAN FOR";
        readonly string ContextEditMaterial = "Edit Material";
        readonly string ContextJoinMaterial = "Join Material";
        readonly string ContextAddMaterial = "Add Material";

        readonly string headerType = "TYPE";
        readonly string headerMatCode = "MATERIAL";
        readonly string headerPlanID = "PLAN";
        readonly string headerFac = "FAC.";
        readonly string headerStart = "START";
        readonly string headerEnd = "END";
        readonly string headerMac = "MAC.";
        readonly string headerFrom = "FROM";
        readonly string headerTransferPending = "TRANSFER PENDING (KG/PIECE)";
        readonly string headerItem= "PLAN FOR";
        readonly string headerPlanToUse = "PLAN TO USE QTY";
        readonly string headerStatus = "STATUS";
        readonly string headerBalance = "BALANCE";
        readonly string headerTransfering = "TRANSFERING";
        readonly string headerUnuse = "UNUSE";

        readonly string headerStillNeed = "STILL NEED (KG/PIECE)";
        readonly string headerStock = "FAC. STOCK (KG/PIECE)";
        readonly string headerTransferred = "TRANSFERRED (KG/PIECE)";
        //readonly string headerTransferCheck = "TRANSFER CHECK";
        readonly string headerPrepare = "PREPARING... (KG/PIECE)";
        readonly string sortByMat = "Material";
        readonly string sortByFac= "Factory";
        readonly string sortByMac = "Machine";
        readonly string sortByPlan = "Plan";
        readonly string sortByPart = "Part";
        readonly string OutOfStock = "OUT OF STOCK";
        private bool closeForm = false;
        private bool loaded = false;
        private bool matPrepare = false;
        private bool dataChanged = false;
        private string oldValue = null;
        //private string editingValue = null;
        private DataTable dt_MatStock;
        private DataTable dt_MaterialPlan;
        private bool switchToByPlanPage = false;

        private DataTable dt_ItemInfo;
        private DataTable dt_MatPlan;
        private DataTable dt_Stock;

        #endregion

        #region UI Setting


        private void loadSortingData()
        {
            //DataTable dt = new DataTable();

            //dt.Columns.Add("sort");
            //dt.Rows.Add(sortByFac);
            //dt.Rows.Add(sortByMac);
            //dt.Rows.Add(sortByMat);
            //dt.Rows.Add(sortByPart);
            //dt.Rows.Add(sortByPlan);

            //dt.DefaultView.Sort = "sort ASC";
            //cmbSort.DataSource = dt;
            //cmbSort.DisplayMember = "sort";
            //cmbSort.Text = "Material";

            //string test = cmbSort.Text;
        }

        private DataTable NewMatStockTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerFac, typeof(string));
            dt.Columns.Add(headerStillNeed, typeof(float));
            dt.Columns.Add(headerStock, typeof(float));
            dt.Columns.Add(headerBalance, typeof(float));
            dt.Columns.Add(headerTransfering, typeof(float));
            dt.Columns.Add(headerUnuse, typeof(float));

            return dt;
        }

        private DataTable NewMatListByPlanTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerFac, typeof(string));
            dt.Columns.Add(headerMac, typeof(string));
            dt.Columns.Add(headerID, typeof(int));
            //dt.Columns.Add(headerItem, typeof(string));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerPartName, typeof(string));

            dt.Columns.Add(headerStart, typeof(DateTime));
            dt.Columns.Add(headerEnd, typeof(DateTime));

            dt.Columns.Add(headerAbleProduceQty, typeof(float));
            dt.Columns.Add(headerTargetQty, typeof(float));

            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerMatUse, typeof(float));
            return dt;
        }

        private DataTable NewMatListByMatTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerStatus, typeof(string));
            dt.Columns.Add(headerPlanID, typeof(int));
            dt.Columns.Add(headerFac, typeof(string));
            dt.Columns.Add(headerMac, typeof(string));
            dt.Columns.Add(headerItem, typeof(string));
            dt.Columns.Add(headerStart, typeof(DateTime));
            dt.Columns.Add(headerEnd, typeof(DateTime));
            dt.Columns.Add(headerPlanToUse, typeof(float));
            
            dt.Columns.Add(headerStillNeed, typeof(float));
            dt.Columns.Add(headerStock, typeof(float));

            //dt.Columns.Add(headerTransfered, typeof(float));
            dt.Columns.Add(headerTransferred, typeof(float));
            dt.Columns.Add(headerTransferPending, typeof(float));
            dt.Columns.Add(headerPrepare, typeof(float));
            dt.Columns.Add(headerFrom, typeof(string));
            dt.Columns.Add(text.Header_Unit, typeof(string));

            //dt.Columns.Add(headerTransferCheck, typeof(bool));

            return dt;
        }

        private void dgvMatListByMatUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerType].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[headerMatCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[headerPlanID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerFac].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMac].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerItem].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerPlanToUse].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerStillNeed].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerStock].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTransferPending].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerTransfered].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPrepare].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerFrom].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////
            dgv.Columns[headerPlanID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPlanToUse].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStillNeed].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerFac].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerMac].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTransferPending].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTransferred].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPrepare].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[headerTransferCheck].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgv.Columns[headerTransferCheck].Visible = false;
            dgv.Columns[headerFac].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);

            dgv.Columns[headerTransferPending].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.Columns[headerType].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            dgv.Columns[headerItem].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            dgv.Columns[headerPlanID].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            dgv.Columns[headerStart].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            dgv.Columns[headerEnd].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            dgv.Columns[headerMatCode].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);

            dgv.Columns[headerFrom].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[headerFrom].MinimumWidth = 200;

            dgv.Columns[headerItem].MinimumWidth = 250;

            dgv.Columns[headerItem].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgv.Columns[headerItem].Frozen = true;


        }

        private void dgvMatListByPlanUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerFac].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMac].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerStart].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerEnd].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerAbleProduceQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTargetQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMatCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerMatUse].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////
            dgv.Columns[headerFac].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerMac].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerStart].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerEnd].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerMatUse].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTargetQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerAbleProduceQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgv.Columns[headerFac].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            //dgv.Columns[headerTransferPending].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            //dgv.Columns[headerFrom].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);



        }
        #endregion

        #region Load Data

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

        private void LoadMatListByPlan()
        {
            dgvMatListByPlan.DataSource = null;
            lblUpdatedTime.Text = DateTime.Now.ToString();
            #region variable

            DataTable dt_MAT = NewMatListByPlanTable();
            DataRow row_dtMat;
            DataTable dt_ItemInfo = dalItem.Select();


            float prepareQty = 0;
            string prePartCode = null;
            string partCode = null;
            bool isSamePlan = false;

            DataTable dt = dalMatPlan.Select();
            DataTable dt_Stock = dalStock.Select();
            //string sortBy = cmbSort.Text;
            #endregion

            DataView dv = dt.DefaultView;
            dv.Sort = dalMac.MacID + " asc, " + dalPlan.productionStartDate + " asc, " + dalPlan.partCode + " asc";
            dt = dv.ToTable();

            dt = specialDataSort(dt);

            dt_MaterialPlan = dt;

            #region data proccessing

            foreach (DataRow row in dt.Rows)
            {
                bool active = Convert.ToBoolean(row[dalMatPlan.Active]);
                if (active)
                {
                    partCode = row[dalPlan.partCode].ToString();

                    if (prePartCode == null || prePartCode == partCode)
                    {
                        if (prePartCode != null)
                        {
                            isSamePlan = true;
                        }
                        prePartCode = partCode;
                    }
                    else
                    {
                        row_dtMat = dt_MAT.NewRow();
                        dt_MAT.Rows.Add(row_dtMat);
                        prePartCode = partCode;
                    }

                    row_dtMat = dt_MAT.NewRow();

                    if (!isSamePlan)
                    {
                        //row_dtMat[headerFac] = row[dalMac.MacLocation].ToString();
                        //row_dtMat[headerMac] = row[dalPlan.machineID];

                        //row_dtMat[headerPartCode] = partCode;
                        //row_dtMat[headerPartName] = tool.getItemNameFromDataTable(dt_ItemInfo, partCode);
                        //row_dtMat[headerStart] = row[dalPlan.productionStartDate];
                        //row_dtMat[headerEnd] = row[dalPlan.productionEndDate];

                        //row_dtMat[headerAbleProduceQty] = row[dalPlan.ableQty];
                        //row_dtMat[headerTargetQty] = row[dalPlan.targetQty];
                    }
                    row_dtMat[headerFac] = row[dalMac.MacLocation].ToString();
                    row_dtMat[headerMac] = row[dalPlan.machineID];

                    row_dtMat[headerPartCode] = partCode;
                    row_dtMat[headerPartName] = tool.getItemNameFromDataTable(dt_ItemInfo, partCode);
                    row_dtMat[headerStart] = row[dalPlan.productionStartDate];
                    row_dtMat[headerEnd] = row[dalPlan.productionEndDate];

                    row_dtMat[headerAbleProduceQty] = row[dalPlan.ableQty];
                    row_dtMat[headerTargetQty] = row[dalPlan.targetQty];

                    row_dtMat[headerID] = row[dalPlan.planID].ToString();


                    row_dtMat[headerMatCode] = row[dalMatPlan.MatCode];
                    row_dtMat[headerMatUse] = row[dalMatPlan.PlanToUse];


                    dt_MAT.Rows.Add(row_dtMat);
                    isSamePlan = false;
                }
            }

            #endregion

            #region set dgv data source
            if (dt_MAT.Rows.Count > 0)
            {
                dgvMatListByPlan.DataSource = dt_MAT;

                dgvMatListByPlanUIEdit(dgvMatListByPlan);

                dgvMatListByPlan.ClearSelection();

                //addComboBoxToDGVCell();
            }
            else
            {
                MessageBox.Show("No data found!");
            }
            #endregion
        }

        private bool ItemCatCheck(string itemCat)
        {
            if(itemCat.Equals(text.Cat_Carton) && cbFilterCarton.Checked)
            {
                return true;
            }
            else if (itemCat.Equals(text.Cat_RawMat) && cbFilterRawMaterial.Checked)
            {
                return true;
            }
            else if (itemCat.Equals(text.Cat_MB) && cbFilterMasterBatch.Checked)
            {
                return true;
            }
            else if (itemCat.Equals(text.Cat_Pigment) && cbFilterPigment.Checked)
            {
                return true;
            }
            else if (itemCat.Equals(text.Cat_SubMat) && cbFilterSubMaterial.Checked)
            {
                return true;
            }
            else if (itemCat.Equals(text.Cat_Part) && cbFilterPart.Checked)
            {
                return true;
            }

            return false;
        }

        private void DatabaseRefresh()
        {
            dt_ItemInfo = dalItem.Select();
            dt_MatPlan = dalMatPlan.Select();
            dt_Stock = dalStock.Select();
        }

        private void LoadMatListByMat()
        {
            frmLoading.ShowLoadingScreen();

            lblUpdatedTime.Text = DateTime.Now.ToString();

            dgvMatListByMat.DataSource = null;
            dataChanged = false;
            btnSave.Visible = false;

            #region variable
            DataTable dt_MAT = NewMatListByMatTable();
            DataRow row_dtMat;


            if(dt_ItemInfo == null || dt_MatPlan == null || dt_Stock == null)
            {
                DatabaseRefresh();
            }
            //DataTable dt_ItemInfo = dalItem.Select();
            //DataTable dt_MatPlan = dalMatPlan.Select();
            //DataTable dt_Stock = dalStock.Select();

            float stillNeed = 0;
            float remainingStock = 0;
            float TransferPending = 0;
            float prepareQty = 0;
            float stockQty = 0;
            string preMatCode = null;

            string matCode = null;
            string matFrom = null;

            
            dt_MaterialPlan = dt_MatPlan.Copy();
            //string sortBy = cmbSort.Text;
            #endregion

            DataView dv = dt_MatPlan.DefaultView;
            dv.Sort = dalItem.ItemCat + " asc, " + "mat_code asc, " + dalMac.MacLocation + " asc, " + dalPlan.productionStartDate + " asc";
            dt_MatPlan = dv.ToTable();


           
            dv = dt_MaterialPlan.DefaultView;
            dv.Sort = dalMac.MacID + " asc, " + dalPlan.productionStartDate + " asc, " + dalPlan.partCode + " asc";
            dt_MaterialPlan = dv.ToTable();

            dt_MaterialPlan = specialDataSort(dt_MaterialPlan);

            #region data proccessing

            string preFacName = null;
            string facName = null;

            foreach (DataRow row in dt_MatPlan.Rows)
            {
                bool active = Convert.ToBoolean(row[dalMatPlan.Active]);
                string status = row[dalPlan.planStatus].ToString();

                if(active && status != text.planning_status_cancelled && status != text.planning_status_completed)
                {
                    facName = row[dalMac.MacLocation].ToString();
                    matCode = row[dalMatPlan.MatCode].ToString();

                    if(matCode.Equals("CTN 590 X 310 X 388 MM"))
                    {
                        float test = 0;
                    }
                    matFrom = row[dalMatPlan.MatFrom].ToString();
                    string partCode = row[dalPlan.partCode].ToString();
                    
                    float transferred = float.TryParse(row[dalMatPlan.Transferred].ToString(), out float i) ? Convert.ToSingle(row[dalMatPlan.Transferred]) : 0;
                    float planToUse = float.TryParse(row[dalMatPlan.PlanToUse].ToString(), out float j) ? Convert.ToSingle(row[dalMatPlan.PlanToUse]) : 0;
                    prepareQty = float.TryParse(row[dalMatPlan.Prepare].ToString(), out float k) ? Convert.ToSingle(row[dalMatPlan.Prepare]) : 0;

                    if (preMatCode == null || preMatCode == matCode)
                    {
                        preMatCode = matCode;

                        if(preFacName == null)
                        {
                            TransferPending = 0;
                            preFacName = facName;
                            stockQty = tool.getStockBalance(matCode, facName, dt_Stock);

                            remainingStock = stockQty;
                        }
                        else if(preFacName == facName)
                        {
                            //if(TransferPending < 0)
                            //{
                            //    remainingStock = TransferPending * -1;
                            //}
                            //else
                            //{
                            //    remainingStock = remainingStock - TransferPending < 0 ? 0 : remainingStock - TransferPending;
                            //}

                           
                        }
                        else
                        {
                            preFacName = facName;
                            TransferPending = 0;
                            stockQty = tool.getStockBalance(matCode, facName, dt_Stock);
                            remainingStock = stockQty;
                        }
                    }   
                    else if(dt_MAT != null && dt_MAT.Rows.Count > 0)
                    {
                        row_dtMat = dt_MAT.NewRow();
                        dt_MAT.Rows.Add(row_dtMat);
                        preMatCode = matCode;
                        preFacName = facName;
                        stockQty = tool.getStockBalance(matCode, facName, dt_Stock);
                        remainingStock = stockQty;
                        TransferPending = 0;
                    }
                    else if(preMatCode != matCode)
                    {
                        stockQty = tool.getStockBalance(matCode, facName, dt_Stock);
                        remainingStock = stockQty;
                    }

                    string itemCat = tool.getItemCatFromDataTable(dt_ItemInfo, row[dalMatPlan.MatCode].ToString());


                    if(ItemCatCheck(itemCat))
                    {
                        row_dtMat = dt_MAT.NewRow();
                        row_dtMat[headerType] = itemCat;
                        row_dtMat[headerMatCode] = matCode;
                        row_dtMat[headerStatus] = status;
                        row_dtMat[headerPlanID] = row[dalMatPlan.PlanID];
                        row_dtMat[headerFac] = facName;
                        row_dtMat[headerMac] = row[dalPlan.machineID];
                        row_dtMat[headerItem] = tool.getItemNameFromDataTable(dt_ItemInfo, partCode) + " (" + partCode + ")";
                        row_dtMat[headerStart] = row[dalPlan.productionStartDate];
                        row_dtMat[headerEnd] = row[dalPlan.productionEndDate];

                        row_dtMat[headerPlanToUse] = planToUse;
                        row_dtMat[headerTransferred] = transferred;

                        stillNeed = planToUse - transferred < 0 ? 0 : planToUse - transferred;

                        //stillNeed = Convert.ToSingle(stillNeed.ToString("0.##"));

                        //double test = stillNeed;
                        row_dtMat[headerStillNeed] = stillNeed;

                        remainingStock = Convert.ToSingle(Math.Round(Convert.ToDouble(remainingStock), 2));

                        row_dtMat[headerStock] = remainingStock;

                        TransferPending = planToUse - remainingStock;

                        TransferPending = (float)Math.Round(TransferPending * 100f) / 100f;

                        //TransferPending = stillNeed - remainingStock;
                        row_dtMat[headerTransferPending] = TransferPending < 0 ? 0 : TransferPending;

                        row_dtMat[headerPrepare] = prepareQty;

                        if(prepareQty == 0)
                        {
                           // row_dtMat[headerFrom] = matFrom;

                        }
                        else
                        {
                            row_dtMat[headerFrom] = matFrom;

                        }


                        dt_MAT.Rows.Add(row_dtMat);

                        remainingStock = remainingStock - planToUse < 0 ? 0 : remainingStock - planToUse;
                    }
                   
                }
            }

            #endregion

            #region set dgv data source

            if (dt_MAT.Rows.Count > 0)
            {
                dgvMatListByMat.DataSource = dt_MAT;

                dgvMatListByMatUIEdit(dgvMatListByMat);
                CheckifRunning(dgvMatListByMat);

                dgvMatListByMat.Columns[headerStatus].Visible = false;

                dgvMatListByMat.Columns[headerStillNeed].Visible = false;

                dgvMatListByMat.ClearSelection();

            }
            else
            {
                MessageBox.Show("No data found!");
                closeForm = true;
            }
            #endregion

            ShowFilter(false);
            frmLoading.CloseForm();

        }

        private void CheckifRunning(DataGridView dgv)
        {
            foreach(DataGridViewRow row in dgv.Rows)
            {
                string status = row.Cells[headerStatus].Value.ToString();
                float trfPending = float.TryParse(row.Cells[headerTransferPending].Value.ToString(), out float i) ? Convert.ToSingle(row.Cells[headerTransferPending].Value.ToString()) : 0;

                if (string.IsNullOrEmpty(status))
                {
                    row.Cells[headerTransferPending].Style.BackColor = Color.Gainsboro;
                }
                else if(status == text.planning_status_pending)
                {
                    if(trfPending > 0)
                    row.Cells[headerTransferPending].Style.BackColor = Color.FromArgb(255, 118, 117);
                }
            }
        }

        #region old load material list
        //private void LoadMatList()
        //{
        //    dgvMatList.DataSource = null;
        //    dataChanged = false;
        //    btnSave.Visible = false;

        //    #region variable
        //    DataTable dt_MAT = NewMatListTable();
        //    DataRow row_dtMat;
        //    DataTable dt_ItemInfo = dalItem.Select();

        //    float total = 0;
        //    float readyStock = 0;
        //    float TransferPending = 0;
        //    float prepareQty = 0;
        //    string matCode = null;
        //    string matFrom = null;

        //    DataTable dt = dalMatPlan.Select();
        //    DataTable dt_Stock = dalStock.Select();
        //    //string sortBy = cmbSort.Text;
        //    #endregion

        //    DataView dv = dt.DefaultView;
        //    dv.Sort = dalItem.ItemCat + " asc, " + "mat_code asc, " + dalMac.MacLocation + " asc, " + dalPlan.productionStartDate + " asc";
        //    dt = dv.ToTable();

        //    #region sorting
        //    //if (sortBy.Equals(sortByFac))
        //    //{
        //    //    DataView dv = dt.DefaultView;
        //    //    //dv.Sort = dalMac.MacLocation+ " asc";
        //    //    //dv.Sort = dalMac.MacLocation + " asc, " + dalMac.MacID + " asc";
        //    //    dv.Sort = dalMac.MacLocation + " asc, " + dalItem.ItemCat + " asc, " + "mat_code asc, " + dalMac.MacID + " asc, " + dalPlan.productionStartDate + " asc";
        //    //    dt = dv.ToTable();
        //    //}
        //    //else if (sortBy.Equals(sortByMac))
        //    //{
        //    //    DataView dv = dt.DefaultView;
        //    //    dv.Sort = dalMac.MacID + " asc";
        //    //    dt = dv.ToTable();
        //    //}
        //    //else if (sortBy.Equals(sortByPlan))
        //    //{
        //    //    DataView dv = dt.DefaultView;
        //    //    dv.Sort = dalPlan.planID + " asc";
        //    //    dt = dv.ToTable();
        //    //}
        //    //else if (sortBy.Equals(sortByPart))
        //    //{
        //    //    DataView dv = dt.DefaultView;
        //    //    dv.Sort = dalPlan.partCode + " asc";
        //    //    dt = dv.ToTable();
        //    //}
        //    //else if (sortBy.Equals(sortByMat))
        //    //{
        //    //    DataView dv = dt.DefaultView;
        //    //    dv.Sort = dalItem.ItemCat + " asc, " + "mat_code asc, " + dalMac.MacLocation + " asc, " + dalPlan.productionStartDate + " asc";
        //    //    dt = dv.ToTable();
        //    //}
        //    #endregion

        //    #region data proccessing

        //    string preFacName = null;
        //    string facName = null;
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        bool active = Convert.ToBoolean(row[dalMatPlan.Active]);
        //        if (active)
        //        {

        //            facName = row[dalMac.MacLocation].ToString();

        //            if (matCode == null)
        //            {
        //                if (preFacName == null)
        //                {
        //                    preFacName = facName;
        //                }
        //                else if (facName != preFacName)
        //                {
        //                    preFacName = facName;
        //                }

        //                matCode = row[dalMatPlan.MatCode].ToString();


        //                total = Convert.ToSingle(row[dalMatPlan.PlanToUse]);
        //                readyStock = tool.getStockBalance(matCode, facName, dt_Stock);

        //                prepareQty = row[dalMatPlan.Prepare] == DBNull.Value ? 0 : Convert.ToSingle(row[dalMatPlan.Prepare]);
        //                matFrom = row[dalMatPlan.MatFrom].ToString();

        //            }
        //            else if (matCode == row[dalMatPlan.MatCode].ToString())
        //            {
        //                if (facName != preFacName)
        //                {
        //                    float stockQty = tool.getStockBalance(matCode, preFacName, dt_Stock);
        //                    dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerStock] = stockQty;
        //                    dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerTotal] = total;
        //                    dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerPrepare] = prepareQty;
        //                    dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerFrom] = matFrom;
        //                    prepareQty = row[dalMatPlan.Prepare] == DBNull.Value ? 0 : Convert.ToSingle(row[dalMatPlan.Prepare]);
        //                    //dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerTransfered] = 0;

        //                    TransferPending = stockQty - total;

        //                    if (TransferPending >= 0)
        //                    {
        //                        TransferPending = 0;
        //                    }
        //                    else
        //                    {
        //                        TransferPending *= -1;
        //                    }

        //                    dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerTransferPending] = TransferPending;
        //                    //dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerPrepare] = TransferPending;

        //                    total = Convert.ToSingle(row[dalMatPlan.PlanToUse]);
        //                    matFrom = row[dalMatPlan.MatFrom].ToString();
        //                    preFacName = facName;
        //                }
        //                else
        //                {
        //                    total += Convert.ToSingle(row[dalMatPlan.PlanToUse]);
        //                    matFrom = row[dalMatPlan.MatFrom].ToString();
        //                    prepareQty = row[dalMatPlan.Prepare] == DBNull.Value ? 0 : Convert.ToSingle(row[dalMatPlan.Prepare]);
        //                }

        //            }
        //            else//new data
        //            {

        //                //if (matCode == "A 887042-3%")
        //                //{
        //                //    float TEST = 0;
        //                //}

        //                readyStock = tool.getStockBalance(matCode, preFacName, dt_Stock);
        //                dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerTotal] = total;
        //                dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerStock] = readyStock;
        //                dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerPrepare] = prepareQty;
        //                dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerFrom] = matFrom;
        //                prepareQty = row[dalMatPlan.Prepare] == DBNull.Value ? 0 : Convert.ToSingle(row[dalMatPlan.Prepare]);
        //                //dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerTransfered] = 0;
        //                if (preFacName == null)
        //                {
        //                    preFacName = facName;
        //                }
        //                else if (facName != preFacName)
        //                {
        //                    preFacName = facName;
        //                }

        //                TransferPending = readyStock - total;

        //                if (TransferPending >= 0)
        //                {
        //                    TransferPending = 0;
        //                }
        //                else
        //                {
        //                    TransferPending *= -1;
        //                }

        //                dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerTransferPending] = TransferPending;
        //                //dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerPrepare] = TransferPending;

        //                matCode = row[dalMatPlan.MatCode].ToString();
        //                matFrom = row[dalMatPlan.MatFrom].ToString();
        //                readyStock = tool.getStockBalance(matCode, facName, dt_Stock);
        //                total = Convert.ToSingle(row[dalMatPlan.PlanToUse]);
        //                row_dtMat = dt_MAT.NewRow();
        //                dt_MAT.Rows.Add(row_dtMat);

        //            }

        //            row_dtMat = dt_MAT.NewRow();
        //            row_dtMat[headerType] = tool.getItemCatFromDataTable(dt_ItemInfo, row[dalMatPlan.MatCode].ToString());
        //            row_dtMat[headerMatCode] = matCode;
        //            row_dtMat[headerPlanID] = row[dalMatPlan.PlanID];
        //            row_dtMat[headerFac] = facName;
        //            row_dtMat[headerMac] = row[dalPlan.machineID];
        //            string partCode = row[dalPlan.partCode].ToString();
        //            row_dtMat[headerItem] = tool.getItemNameFromDataTable(dt_ItemInfo, partCode) + "(" + partCode + ")";
        //            row_dtMat[headerStart] = row[dalPlan.productionStartDate];
        //            row_dtMat[headerEnd] = row[dalPlan.productionEndDate];
        //            row_dtMat[headerPlanToUse] = row[dalMatPlan.PlanToUse];

        //            dt_MAT.Rows.Add(row_dtMat);
        //        }
        //    }

        //    #endregion

        //    #region set dgv data source
        //    if (dt_MAT.Rows.Count > 0)
        //    {
        //        dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerTotal] = total;
        //        TransferPending = readyStock - total;

        //        if (TransferPending >= 0)
        //        {
        //            TransferPending = 0;
        //        }
        //        else
        //        {
        //            TransferPending *= -1;
        //        }

        //        dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerTransferPending] = TransferPending;
        //        //dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerPrepare] = TransferPending;
        //        dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerPrepare] = prepareQty;
        //        dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerFrom] = matFrom;
        //        dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerStock] = tool.getStockBalance(matCode, preFacName, dt_Stock);
        //        dgvMatList.DataSource = dt_MAT;

        //        dgvMatListUIEdit(dgvMatList);

        //        dgvMatList.ClearSelection();

        //        //addComboBoxToDGVCell();
        //    }
        //    else
        //    {
        //        MessageBox.Show("No data found!");
        //        closeForm = true;
        //    }
        //    #endregion
        //}
        #endregion

        private void addComboBoxToDGVCell()
        {
            DataGridView dgv = dgvMatListByMat;

            DataTable dt_RequiredList = (DataTable)dgv.DataSource;

            DataTable dt_stock = dalStock.Select();

            dt_MatStock = NewMatStockTable();

            DataRow row_MatStock;

            string previousMat = null;

            foreach(DataRow row in dt_RequiredList.Rows)
            {
                string total = row[headerStillNeed].ToString();

                if(!string.IsNullOrEmpty(total))
                {
                    string MatCode = row[headerMatCode].ToString();
                    string Factory = row[headerFac].ToString();
                    string TrfPending = row[headerTransferPending].ToString();
                    float FacStock = row[headerStock] == DBNull.Value ? 0 : Convert.ToSingle(row[headerStock]);
                    float totalNeed = Convert.ToSingle(total);

                    if (previousMat != MatCode)
                    {
                        previousMat = MatCode;

                        //get fac data
                        foreach (DataRow facStock in dt_stock.Rows)
                        {
                            string ItemCode = facStock[dalItem.ItemCode].ToString();

                            if(ItemCode == MatCode)
                            {
                                row_MatStock = dt_MatStock.NewRow();
                                row_MatStock[headerMatCode] = MatCode;
                                row_MatStock[headerFac] = facStock["fac_name"].ToString();
                                row_MatStock[headerStock] = facStock["stock_qty"].ToString();
                                row_MatStock[headerStillNeed] = 0;
                                row_MatStock[headerBalance] = facStock["stock_qty"].ToString();
                                row_MatStock[headerTransfering] = 0;
                                row_MatStock[headerUnuse] = facStock["stock_qty"].ToString();

                                dt_MatStock.Rows.Add(row_MatStock);
                            }
                        }

                    }

                    foreach(DataRow matStock in dt_MatStock.Rows)
                    {
                        if(MatCode == matStock[headerMatCode].ToString() && Factory == matStock[headerFac].ToString())
                        {
                            matStock[headerStillNeed] = total;

                            float balance = FacStock - totalNeed;

                            if (balance < 0)
                            {
                                balance = 0;
                            }

                            matStock[headerBalance] = balance;
                            matStock[headerTransfering] = 0;
                            matStock[headerUnuse] = balance;
                        }
                    }
                }
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                string total = row.Cells[headerStillNeed].Value.ToString();

                if (!string.IsNullOrEmpty(total))
                {
                    
                    string TrfPending = row.Cells[headerTransferPending].Value.ToString();

                    if (TrfPending != "0" && !string.IsNullOrEmpty(TrfPending))
                    {
                        string MatCode = row.Cells[headerMatCode].Value.ToString();
                        string Factory = row.Cells[headerFac].Value.ToString();
                        float preparing = float.TryParse(row.Cells[headerPrepare].Value.ToString(), out preparing) ? preparing : 0;

                        DataGridViewComboBoxCell ComboBoxCell = new DataGridViewComboBoxCell();
                        foreach (DataRow matStock in dt_MatStock.Rows)
                        {
                            string ItemCode = matStock[headerMatCode].ToString();
                            string FacName = matStock[headerFac].ToString();
                            string Balance = matStock[headerBalance].ToString();

                            if (ItemCode == MatCode && FacName != Factory && Balance != "0")
                            {
                                string itemAdd = FacName + " (AVAILABLE STOCK: " + Balance + ")";
                                ComboBoxCell.Items.Add(FacName + " (AVAILABLE STOCK: " + Balance + ")");
                            }
                        }

                        ComboBoxCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                        if (ComboBoxCell.Items.Count <= 0)
                        {
                            string PreparingQty = row.Cells[headerPrepare].Value.ToString();

                            ComboBoxCell.Items.Add(OutOfStock);
                            row.Cells[headerFrom] = ComboBoxCell;

                            if (PreparingQty != "0")
                            {
                                row.Cells[headerPrepare].Style.ForeColor = Color.Red;
                                row.Cells[headerFrom].Value = OutOfStock;
                            }
                        }
                        else
                        {
                           
                            row.Cells[headerFrom] = ComboBoxCell;
                        }

                        string locationName = row.Cells[headerFrom].Value.ToString();
                        locationName = GetFacName(locationName);
                        if (locationName != null)
                        {
                            DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[headerFrom];

                            for (int x = 0; x <= cbCell.Items.Count - 1; x++)
                            {
                                string item = cbCell.Items[x].ToString();
                                string facName = GetFacName(item);

                                if (facName == locationName)
                                {
                                    cbCell.Value = item;
                                    break;
                                }
                                else
                                {
                                    cbCell.Value = null;
                                }
                            }
                        }
                    }

                }
            }
        }

        private string GetFacName(string s)
        {
            string rtn = null;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                {
                    rtn += s[i];
                }
                else
                {
                    return rtn;
                }
            }
            return rtn;
        }

        #endregion

        private void frmMatPlanningList_Load(object sender, EventArgs e)
        {
            matPrepare = false;

            lblFilter.Text = Filter_ShowMore;

            DatabaseRefresh();

            switchToByPlanPage = false;
            SwitchPage();
            loaded = true;
            
            if (closeForm)
            {
                Close();
            }

            CheckifRunning(dgvMatListByMat);

            if(dgvMatListByMat.Columns[headerStatus] != null)
            dgvMatListByMat.Columns[headerStatus].Visible = false;

            dgvMatListByMat.ClearSelection();
           
        }

        private void dgvMatList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            DataGridView dgv = dgvMatListByMat;

            if(switchToByPlanPage)
            {
                dgv = dgvMatListByPlan;
            }

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerMatCode)
            {
                string matCode = dgv.Rows[row].Cells[headerMatCode].Value.ToString();

                if (string.IsNullOrEmpty(matCode))
                {
                    dgv.Rows[row].Height = 3;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.Gainsboro;
                }
                else
                {
                    dgv.Rows[row].Height = 60;
                    //dgv.Rows[row].DefaultCellStyle.BackColor = Color.White;

                }
            }
            
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

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Thread t = null;
            //bool aborted = false;
            if(loaded)
            {
                try
                {
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                                                 //t = new Thread(new ThreadStart(StartForm));
                    LoadMatListByMat();
                }
                //catch (ThreadAbortException)
                //{
                //    // ignore it
                //    //aborted = true;
                //}
                catch (Exception ex)
                {
                    tool.saveToTextAndMessageToUser(ex);
                }
                finally
                {
                    //if (!aborted)
                    //    t.Abort();

                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
            }
           

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvMatListByMat;
  
            if(matPrepare)
            {
                matPrepare = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.ReadOnly = true;
                dgv.Columns[headerPrepare].DefaultCellStyle.BackColor = Color.White;
                dgv.ReadOnly = true;
                dgv.Columns[headerFrom].Visible = true;

                if(!dataChanged)
                {
                    btnSave.Visible = false;
                }
            }
            else
            {
                matPrepare = true;
                dgv.ReadOnly = false;
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgv.ReadOnly = false;
                dgv.Columns[headerPrepare].DefaultCellStyle.BackColor = SystemColors.Info;

                dgv.Columns[headerFrom].Visible = true;

                addComboBoxToDGVCell();
                stockDataUpdate();
                //cmbEmployeeStatus.SelectedIndex = cmbEmployeeStatus.FindString(employee.employmentstatus);
                //dgv.Rows[0].Cells[headerFrom].Value = "No.2";
                //dgv.Rows[4].Cells[headerFrom].Value = "STORE";
                //DataTable test = dt_MatStock;

            }



        }

        private void dgvMatList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvMatListByMat;
            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;

            bool validClick = row != -1 && col != -1; //Make sure the clicked row/column is valid.
           
            if(validClick && matPrepare)
            {
                if (dgv.Rows[row].Cells[col] is DataGridViewComboBoxCell )
                {
                    dgv.BeginEdit(true);
                    ((ComboBox)dgv.EditingControl).DroppedDown = true;
                }
                //else if (dgv.Columns[col].Name == headerPrepare)
                //{
                //    dgv.BeginEdit(true);
                //}
            }
            
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataGridView dgv = dgvMatListByMat;
            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;
            string previousValue = dgvMatListByMat.CurrentCell.EditedFormattedValue.ToString();
            char key = e.KeyChar;
            string newValue = null;

            if(key == '\b')
            {
                int index = previousValue.Length - 1;

                if(index > 0)
                newValue = previousValue.Remove(previousValue.Length - 1);
            }
            else
            {
                newValue = previousValue + key;
            }

         bool result = double.TryParse(newValue, out double i);

            if (dgv.Columns[col].Name == headerPrepare)
            {
                
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' || (!result && !string.IsNullOrEmpty(newValue)))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }

        }

        private void refreshMatStock()
        {
            foreach (DataRow row in dt_MatStock.Rows)
            {
                string itemCode = row[headerMatCode].ToString();

                    row[headerTransfering] = 0;
                    row[headerUnuse] = row[headerBalance];
            }
        }

        private void refreshMatStock(string matCode)
        {
            foreach(DataRow row in dt_MatStock.Rows)
            {
                string itemCode = row[headerMatCode].ToString();

                if(itemCode == matCode)
                {
                    row[headerTransfering] = 0;
                    row[headerUnuse] = row[headerBalance];

                }
            }
        }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void stockDataUpdate()
        {
            DataGridView dgv = dgvMatListByMat;

            refreshMatStock();
            foreach (DataGridViewRow MR in dgv.Rows)
            {
                float i = 0;
                string itemCode = MR.Cells[headerMatCode].Value.ToString();
                string total = MR.Cells[headerStillNeed].Value.ToString();
                string trfPending = MR.Cells[headerTransferPending].Value.ToString();
                
                if (!string.IsNullOrEmpty(total) && trfPending != "0")
                {
                    MR.Cells[headerPrepare].Style.ForeColor = Color.Black;
                    string runningLocation = MR.Cells[headerFac].Value.ToString();
                    string locationFrom = MR.Cells[headerFrom].Value.ToString();
                    string locationName = GetFacName(locationFrom);

                    float preparing = float.TryParse(MR.Cells[headerPrepare].Value.ToString(), out i) ?Convert.ToSingle(MR.Cells[headerPrepare].Value):0;
                    //float locationBalanceStock = 0;

                    
                    DataGridViewComboBoxCell ComboBoxCell = new DataGridViewComboBoxCell();
                    foreach (DataRow matStock in dt_MatStock.Rows)
                    {
                        string matStockItemCode = matStock[headerMatCode].ToString();
                        string matStockFac = matStock[headerFac].ToString();

                        float transferring = Convert.ToSingle(matStock[headerTransfering]);
                        float unuse = Convert.ToSingle(matStock[headerUnuse]);

                        #region add combobox
                        

                        if (matStockItemCode == itemCode && matStockFac != runningLocation && unuse > 0)
                        {
                            string itemAdd = matStockFac + " (AVAILABLE STOCK: " + unuse + ")";
                            ComboBoxCell.Items.Add(matStockFac + " (AVAILABLE STOCK: " + unuse + ")");
                        }

                        ComboBoxCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                        

                        #endregion

                        if(locationName == matStockFac && matStockItemCode == itemCode)
                        {
                            transferring += preparing;
                            unuse -= preparing;

                            if(unuse < 0)
                            {
                                unuse = 0;
                                MR.Cells[headerPrepare].Style.ForeColor = Color.Red;
                            }
                           
                            matStock[headerTransfering] = transferring;
                            matStock[headerUnuse] = unuse;

                            dt_MatStock.AcceptChanges();
                        }
                    }

                    if (ComboBoxCell.Items.Count <= 0)
                    {

                        ComboBoxCell.Items.Add(OutOfStock);
                        MR.Cells[headerFrom] = ComboBoxCell;

                        if (preparing > 0)
                        {
                            MR.Cells[headerPrepare].Style.ForeColor = Color.Red;
                            MR.Cells[headerFrom].Value = OutOfStock;
                        }
                        else
                        {
                            MR.Cells[headerPrepare].Style.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        MR.Cells[headerFrom] = ComboBoxCell;
                    }

                    if (locationName != null)
                    {
                        DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)MR.Cells[headerFrom];

                        for (int x = 0; x <= cbCell.Items.Count - 1; x++)
                        {
                            string item = cbCell.Items[x].ToString();
                            string facName = GetFacName(item);

                            if (facName == locationName)
                            {
                                cbCell.Value = item;
                                break;
                            }
                            else
                            {
                                cbCell.Value = null;
                            }
                        }
                    }
                    //string from = MR.Cells[headerFrom].Value.ToString();
                    //string facName = GetFacName(locationFrom);
                    //string item = MR.Cells[headerMatCode].Value.ToString();
                    



                    if (locationName == null)
                    {
                        if (preparing > 0)
                        {
                            MR.Cells[headerPrepare].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            MR.Cells[headerPrepare].Style.ForeColor = Color.Black;
                        }
                    }
                    //else
                    //{
                    //    if (preparing > locationBalanceStock)
                    //    {
                    //        MR.Cells[headerPrepare].Style.ForeColor = Color.Red;
                    //    }
                    //    else
                    //    {
                    //        MR.Cells[headerPrepare].Style.ForeColor = Color.Black;
                    //    }
                    //}

                }
            }

        }

        private void stockDataUpdate(int row)
        {
            DataGridView dgv = dgvMatListByMat;

            string matCode = dgv.Rows[row].Cells[headerMatCode].Value.ToString();
            refreshMatStock(matCode);

            foreach (DataGridViewRow MR in dgv.Rows)
            {
                float i = 0;
                string itemCode = MR.Cells[headerMatCode].Value.ToString();
                string total = MR.Cells[headerStillNeed].Value.ToString();
                string trfPending = MR.Cells[headerTransferPending].Value.ToString();

                if (!string.IsNullOrEmpty(total) && itemCode == matCode && trfPending != "0")
                {
                    MR.Cells[headerPrepare].Style.ForeColor = Color.Black;
                    string runningLocation = MR.Cells[headerFac].Value.ToString();
                    string locationFrom = MR.Cells[headerFrom].Value.ToString();
                    string locationName = GetFacName(locationFrom);

                    float preparing = float.TryParse(MR.Cells[headerPrepare].Value.ToString(), out i) ? Convert.ToSingle(MR.Cells[headerPrepare].Value) : 0;
                    //float locationBalanceStock = 0;


                    DataGridViewComboBoxCell ComboBoxCell = new DataGridViewComboBoxCell();
                    foreach (DataRow matStock in dt_MatStock.Rows)
                    {
                        string matStockItemCode = matStock[headerMatCode].ToString();
                        string matStockFac = matStock[headerFac].ToString();

                        float transferring = Convert.ToSingle(matStock[headerTransfering]);
                        float unuse = Convert.ToSingle(matStock[headerUnuse]);

                        #region add combobox


                        if (matStockItemCode == matCode && matStockFac != runningLocation && unuse > 0)
                        {
                            string itemAdd = matStockFac + " (AVAILABLE STOCK: " + unuse.ToString("0.00") + ")";
                            ComboBoxCell.Items.Add(matStockFac + " (AVAILABLE STOCK: " + unuse.ToString("0.00") + ")");
                        }

                        ComboBoxCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;


                        #endregion

                        if (locationName == matStockFac && matStockItemCode == matCode)
                        {
                            transferring += preparing;
                            unuse -= preparing;

                            if (unuse < 0)
                            {
                                unuse = 0;
                                MR.Cells[headerPrepare].Style.ForeColor = Color.Red;
                            }

                            matStock[headerTransfering] = transferring;
                            matStock[headerUnuse] = unuse;

                            dt_MatStock.AcceptChanges();
                        }
                    }

                    if (ComboBoxCell.Items.Count <= 0)
                    {

                        ComboBoxCell.Items.Add(OutOfStock);
                        MR.Cells[headerFrom] = ComboBoxCell;

                        if (preparing > 0)
                        {
                            MR.Cells[headerPrepare].Style.ForeColor = Color.Red;
                            MR.Cells[headerFrom].Value = OutOfStock;
                        }
                        else
                        {
                            MR.Cells[headerPrepare].Style.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        MR.Cells[headerFrom] = ComboBoxCell;
                    }

                    if (locationName != null)
                    {
                        DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)MR.Cells[headerFrom];

                        for (int x = 0; x <= cbCell.Items.Count - 1; x++)
                        {
                            string item = cbCell.Items[x].ToString();
                            string facName = GetFacName(item);

                            if (facName == locationName)
                            {
                                cbCell.Value = item;
                                break;
                            }
                            else
                            {
                                cbCell.Value = null;
                            }
                        }
                    }
                    //string from = MR.Cells[headerFrom].Value.ToString();
                    //string facName = GetFacName(locationFrom);
                    //string item = MR.Cells[headerMatCode].Value.ToString();




                    if (locationName == null)
                    {
                        if (preparing > 0)
                        {
                            MR.Cells[headerPrepare].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            MR.Cells[headerPrepare].Style.ForeColor = Color.Black;
                        }
                    }
                    //else
                    //{
                    //    if (preparing > locationBalanceStock)
                    //    {
                    //        MR.Cells[headerPrepare].Style.ForeColor = Color.Red;
                    //    }
                    //    else
                    //    {
                    //        MR.Cells[headerPrepare].Style.ForeColor = Color.Black;
                    //    }
                    //}

                }
            }

        }

        private void dgvMatList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            DataGridView dgv = dgvMatListByMat;
            string newValue = dgvMatListByMat.CurrentCell.EditedFormattedValue.ToString();
            int row = dgvMatListByMat.CurrentCell.RowIndex;
            int col = dgvMatListByMat.CurrentCell.ColumnIndex;

            if (dgv.Columns[col].Name == headerPrepare)
            {
                if (oldValue != newValue && dgvMatListByMat.IsCurrentCellDirty)
                {
                    dataChanged = true;
                }
            }
            else if (dgv.Columns[col].Name == headerFrom)
            {
                if (oldValue != newValue && dgvMatListByMat.IsCurrentCellDirty)
                {
                    dataChanged = true;
                }

                dgv.CurrentCell = dgvMatListByMat[col - 1, row];
                dgv.ClearSelection();

                
            }

        }

        private void dgvMatList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldValue = dgvMatListByMat[e.ColumnIndex, e.RowIndex].Value.ToString();
        }

        private void dgvMatList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);

                System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;

                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvMatList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            float TransferredQty = float.TryParse(dgvMatListByMat.Rows[e.RowIndex].Cells[headerTransferred].Value.ToString(), out float i)? i : 0;
            float PreparingQty = float.TryParse(dgvMatListByMat.Rows[e.RowIndex].Cells[headerPrepare].Value.ToString(), out i) ? i : 0;


            string editingHeader = dgvMatListByMat.Columns[e.ColumnIndex].Name;

            bool OkToProcess = true;

            if(PreparingQty == 0)
            {
                dgvMatListByMat.Rows[e.RowIndex].Cells[headerFrom].Value = null;

            }
            else if (TransferredQty > 0 && editingHeader.Equals(headerPrepare))
            {
                OkToProcess = MessageBox.Show("Material delivery has already been carried out for this plan, are you sure you want to deliver more materials?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

                if(!OkToProcess)
                {
                    dgvMatListByMat.Rows[e.RowIndex].Cells[headerPrepare].Value = oldValue;
                }
            }

            if(OkToProcess)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    //editingValue = null;
                    stockDataUpdate(e.RowIndex);
                    //BeginInvoke(new MethodInvoker(PopulateControl));
                }
                catch (Exception ex)
                {
                    tool.saveToTextAndMessageToUser(ex);
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
         

        }

        private void dgvMatList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private bool validation()
        {
            bool passed = true;

            foreach(DataGridViewRow row in dgvMatListByMat.Rows)
            {
                string total = row.Cells[headerStillNeed].Value.ToString();

                if(!string.IsNullOrEmpty(total))
                {
                    Color prepareForeColor = row.Cells[headerPrepare].Style.ForeColor;

                    if(prepareForeColor == Color.Red)
                    {
                        return false;
                    }
                }
            }
            return passed;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            try
            {
                Cursor = Cursors.WaitCursor;
                if(validation())
                {
                    bool dataSaved = true;
                    DialogResult dialogResult = MessageBox.Show("Confirm to save these data?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DataTable dt = (DataTable)dgvMatListByMat.DataSource;
                        DateTime now = DateTime.Now;
                        foreach (DataRow row in dt.Rows)
                        {
                            string total = row[headerStillNeed].ToString();

                            if (!string.IsNullOrEmpty(total))
                            {
                                string matCode = row[headerMatCode].ToString();
                                string facName = row[headerFrom].ToString();
                                facName = GetFacName(facName);

                                if (facName == null)
                                {
                                    facName = "";
                                }


                                int PlanID = int.TryParse(row[headerPlanID].ToString(), out int i) ? Convert.ToInt32(row[headerPlanID]) : 0;
                                float Transferred = int.TryParse(row[headerTransferred].ToString(), out int k) ? Convert.ToInt32(row[headerTransferred]) : 0;
                                float preparingQty = float.TryParse(row[headerPrepare].ToString(), out float j) ? Convert.ToSingle(row[headerPrepare]) : 0;

                                //if(matCode == "A 887545-3%")
                                //{
                                //    string test = row[headerPrepare].ToString();
                                //}
                                

                                uMatPlan.mat_code = matCode;
                                uMatPlan.plan_id = PlanID;
                                uMatPlan.mat_transferred = Transferred;
                                uMatPlan.mat_preparing = preparingQty;
                                uMatPlan.mat_from = facName;
                                uMatPlan.updated_date = now;
                                uMatPlan.updated_by = MainDashboard.USER_ID;

                                if (!dalMatPlan.MatPrepareUpdate(uMatPlan))
                                {
                                    dataSaved = false;

                                    MessageBox.Show("Failed to update material preparing data.");
                                }
                            }
                        }

                        if (dataSaved)
                        {
                            MessageBox.Show("Material preparing's data updated successfully!");
                            btnSave.Visible = false;
                            dataChanged = false;
                            if (matPrepare)
                            {
                                matPrepare = false;
                                dgvMatListByMat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                                dgvMatListByMat.Columns[headerPrepare].DefaultCellStyle.BackColor = Color.Gainsboro;
                                dgvMatListByMat.ReadOnly = true;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("It is shown as insufficient stock number (RED COLOUR) in one of the row at 'preparing column', please amend before saving.");
                }
                
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
           
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                DatabaseRefresh();

                if (switchToByPlanPage)
                {
                    LoadMatListByPlan();
                }
                else
                {
                    LoadMatListByMat();
                    if (matPrepare)
                    {
                        matPrepare = false;
                        dgvMatListByMat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvMatListByMat.Columns[headerPrepare].DefaultCellStyle.BackColor = Color.Gainsboro;
                        dgvMatListByMat.ReadOnly = true;

                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                if (switchToByPlanPage)
                {
                    dgvMatListByPlan.ClearSelection();
                }
                else
                {
                    dgvMatListByMat.ClearSelection();
                }

                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void dgvMatList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnCheckList_Click(object sender, EventArgs e)
        {
            frmMatCheckList frm = new frmMatCheckList();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            LoadMatListByMat();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            switchToByPlanPage = true;
            SwitchPage();

            //frmMatListEdit frm = new frmMatListEdit();
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.ShowDialog();

            //LoadMatList();
  
        }

        private void SwitchPage()
        {
            Cursor = Cursors.WaitCursor;
            tlpMaterialList.SuspendLayout();

            if (switchToByPlanPage)
            {
                ShowFilter(false);
                lblFilter.Visible = false;

                btnByMat.ForeColor = Color.Black;
                btnByPlan.ForeColor = Color.FromArgb(52, 139, 209);

                btnCheckList.Visible = false;
                btnPrepare.Visible = false;

                tlpHeaderButton.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpHeaderButton.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpHeaderButton.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 150f);

                tlpMaterialList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
                tlpMaterialList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);

                LoadMatListByPlan();
            }
            else
            {
                ShowFilter(false);
                lblFilter.Visible = true;

                btnByPlan.ForeColor = Color.Black;
                btnByMat.ForeColor = Color.FromArgb(52, 139, 209);

                btnCheckList.Visible = true;
                btnPrepare.Visible = true;

                tlpHeaderButton.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 150f);
                tlpHeaderButton.ColumnStyles[5] = new ColumnStyle(SizeType.Absolute, 150f);
                tlpHeaderButton.ColumnStyles[6] = new ColumnStyle(SizeType.Absolute, 0f);

                tlpMaterialList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMaterialList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);

                LoadMatListByMat();
            }
            tlpMaterialList.ResumeLayout();
            Cursor = Cursors.Arrow;
        }

        private void btnByMat_Click(object sender, EventArgs e)
        {
           
            switchToByPlanPage = false;
            SwitchPage();
           
        }

        private void dgvMatListByPlan_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvMatListByPlan;

            if(!switchToByPlanPage)
            {
                dgv = dgvMatListByMat;
            }

            //handle the row selection on right click
            int rowIndex = dgv.CurrentCell.RowIndex;

            string matCode = dgv.Rows[rowIndex].Cells[headerMatCode].Value.ToString();

            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && !string.IsNullOrEmpty(matCode))
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();

                try
                {
                    my_menu.Items.Add(ContextAddMaterial).Name = ContextAddMaterial;
                    my_menu.Items.Add(ContextEditMaterial).Name = ContextEditMaterial;
                    my_menu.Items.Add(ContextJoinMaterial).Name = ContextJoinMaterial;
                   

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

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvMatListByPlan;

            if (!switchToByPlanPage)
            {
                dgv = dgvMatListByMat;
            }

            dgv.SuspendLayout();
            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;


            int planID = dgv.Rows[rowIndex].Cells[headerID].Value == DBNull.Value ? -1 : Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerID].Value);

            if (planID != -1)
            {
                if (itemClicked.Equals(ContextEditMaterial))
                {
                    EditMaterial(rowIndex);

                }
                else if (itemClicked.Equals(ContextJoinMaterial))
                {

                    PairMaterial(rowIndex, planID);
                }
                else if (itemClicked.Equals(ContextAddMaterial))
                {

                    AddMaterial(rowIndex);
                }
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();


            //loadScheduleData();
            //dgvSchedule.ClearSelection();

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }


        private void PairMaterial(int row, int planID)
        {
            DataGridView dgv = dgvMatListByPlan;

            if (!switchToByPlanPage)
            {
                dgv = dgvMatListByMat;
            }

            string parentCode = "";
            //get parent code by searching plain id in mat_plan db
            DataTable dt_Plan = dalPlan.idSearch(planID.ToString());


            foreach (DataRow row_Plan in dt_Plan.Rows)
            {
                parentCode = row_Plan[dalItem.ItemCode].ToString();
            }

            uJoin.join_parent_code = parentCode;

            uJoin.join_child_code = "";

            uJoin.join_qty = 1;
            uJoin.join_max = 1;
            uJoin.join_min = 1;

            frmJoinEdit frm = new frmJoinEdit(uJoin, true);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            if(switchToByPlanPage)
            {
                LoadMatListByPlan();
            }
            else
            {
                LoadMatListByMat();
            }
            

            dgv.FirstDisplayedScrollingRowIndex = row;
            dgv.Rows[row].Selected = true;
        }

        private void EditMaterial(int row)
        {
            DataGridView dgv = dgvMatListByPlan;
            string useQty = headerMatUse;

            if (!switchToByPlanPage)
            {
                dgv = dgvMatListByMat;
                useQty = headerPlanToUse;
            }

            int planID = int.TryParse(dgv.Rows[row].Cells[headerID].Value.ToString(), out int i) ? Convert.ToInt32(dgv.Rows[row].Cells[headerID].Value.ToString()) : -1;
            float planToUse = float.TryParse(dgv.Rows[row].Cells[useQty].Value.ToString(), out float j) ? Convert.ToSingle(dgv.Rows[row].Cells[useQty].Value.ToString()) : 0;

            uMatPlan.plan_id = planID;
            //uMatPlan.part_code = dgv.Rows[row].Cells[headerPartCode].Value.ToString();
            //uMatPlan.part_name = dgv.Rows[row].Cells[headerPartName].Value.ToString();

            //uMatPlan.pro_location = dgv.Rows[row].Cells[headerFac].Value.ToString();
            //uMatPlan.pro_machine = dgv.Rows[row].Cells[headerMac].Value.ToString();
            //uMatPlan.pro_max_qty = dgv.Rows[row].Cells[headerAbleProduceQty].Value.ToString();
            //uMatPlan.pro_target_qty = dgv.Rows[row].Cells[headerTargetQty].Value.ToString();
            //uMatPlan.pro_start = dgv.Rows[row].Cells[headerStart].Value.ToString();
            //uMatPlan.pro_end = dgv.Rows[row].Cells[headerEnd].Value.ToString();

            uMatPlan.mat_code = dgv.Rows[row].Cells[headerMatCode].Value.ToString();
            uMatPlan.mat_name = tool.getItemName(uMatPlan.mat_code);
            uMatPlan.mat_cat = tool.getItemCat(uMatPlan.mat_code);
            uMatPlan.plan_to_use = planToUse;


            string parentCode = "";
            //get parent code by searching plain id in mat_plan db
            DataTable dt_Plan = dalPlan.idSearch(planID.ToString());


            foreach (DataRow row_Plan in dt_Plan.Rows)
            {
                parentCode = row_Plan[dalItem.ItemCode].ToString();
            }


            //frmMatAddOrEdit frm = new frmMatAddOrEdit(uMatPlan, false);
            frmMatAddOrEdit frm = new frmMatAddOrEdit(dt_MaterialPlan, uMatPlan, parentCode, false);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            if (switchToByPlanPage)
            {
                LoadMatListByPlan();
            }
            else
            {
                LoadMatListByMat();
            }


            dgv.FirstDisplayedScrollingRowIndex = row;
            dgv.Rows[row].Selected = true;
        }

        private void AddMaterial(int row)
        {
            DataGridView dgv = dgvMatListByPlan;

            if (!switchToByPlanPage)
            {
                dgv = dgvMatListByMat;
            }

            int planID = int.TryParse(dgv.Rows[row].Cells[headerID].Value.ToString(), out int i) ? Convert.ToInt32(dgv.Rows[row].Cells[headerID].Value.ToString()) : -1;
            //float planToUse = float.TryParse(dgv.Rows[row].Cells[headerMatUse].Value.ToString(), out float j) ? Convert.ToSingle(dgv.Rows[row].Cells[headerMatUse].Value.ToString()) : 0;

            //uMatPlan.plan_id = planID;
            //uMatPlan.part_code = dgv.Rows[row].Cells[headerPartCode].Value.ToString();
            //uMatPlan.part_name = dgv.Rows[row].Cells[headerPartName].Value.ToString();

            //uMatPlan.pro_location = dgv.Rows[row].Cells[headerFac].Value.ToString();
            //uMatPlan.pro_machine = dgv.Rows[row].Cells[headerMac].Value.ToString();
            //uMatPlan.pro_max_qty = dgv.Rows[row].Cells[headerAbleProduceQty].Value.ToString();
            //uMatPlan.pro_target_qty = dgv.Rows[row].Cells[headerTargetQty].Value.ToString();
            //uMatPlan.pro_start = dgv.Rows[row].Cells[headerStart].Value.ToString();
            //uMatPlan.pro_end = dgv.Rows[row].Cells[headerEnd].Value.ToString();

            //uMatPlan.mat_code = dgv.Rows[row].Cells[headerMatCode].Value.ToString();
            //uMatPlan.mat_name = tool.getItemName(uMatPlan.mat_code);
            //uMatPlan.mat_cat = tool.getItemCat(uMatPlan.mat_code);
            //uMatPlan.plan_to_use = planToUse;


            string parentCode = "";
            //get parent code by searching plain id in mat_plan db
            DataTable dt_Plan = dalPlan.idSearch(planID.ToString());


            foreach (DataRow row_Plan in dt_Plan.Rows)
            {
                parentCode = row_Plan[dalItem.ItemCode].ToString();

            }

            frmMatAddOrEdit frm = new frmMatAddOrEdit(dt_MaterialPlan, parentCode, true);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            if (switchToByPlanPage)
            {
                LoadMatListByPlan();
            }
            else
            {
                LoadMatListByMat();
            }


            if (frmMatAddOrEdit.dataSaved)
                row++;

            dgv.FirstDisplayedScrollingRowIndex = row;
            dgv.Rows[row].Selected = true;
        }

        private void btnAddMat_Click(object sender, EventArgs e)
        {
            frmMatAddOrEdit frm = new frmMatAddOrEdit(dt_MaterialPlan);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            if(switchToByPlanPage)
            {
                LoadMatListByPlan();
            }
            else
            {
                LoadMatListByMat();
            }

        }

        private void dgvMatListByMat_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var ht = dgvMatListByMat.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvMatListByMat.ClearSelection();
            }
        }

        private void dgvMatListByPlan_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var ht = dgvMatListByPlan.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvMatListByPlan.ClearSelection();
            }
        }

        private void dgvMatListByMat_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvMatListByMat.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvMatListByMat.ClearSelection();
            }
        }

        private void dgvMatListByPlan_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dgvMatListByPlan.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                //clicked on grey area
                dgvMatListByPlan.ClearSelection();
            }
        }

        private void frmMatPlanningList_MouseClick(object sender, MouseEventArgs e)
        {
            if(switchToByPlanPage)
            {
                dgvMatListByPlan.ClearSelection();
            }
            else
            {
                dgvMatListByMat.ClearSelection();
            }
            
        }

        private void tlpHeaderButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (switchToByPlanPage)
            {
                dgvMatListByPlan.ClearSelection();
            }
            else
            {
                dgvMatListByMat.ClearSelection();
            }
        }

        private void ShowFilter(bool Show)
        {
            if(Show)
            {
                lblFilter.Text = Filter_HIDE;

                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Absolute, 100f);
            }
            else
            {
                lblFilter.Text = Filter_ShowMore;

                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }
        private void lblFilter_Click(object sender, EventArgs e)
        {
            if(lblFilter.Text.Equals(Filter_ShowMore))
            {
                //show filter
                ShowFilter(true);
            }
            else
            {
                //hide filter
                ShowFilter(false);

            }
        }

        private void cbFilterRawMaterial_CheckedChanged(object sender, EventArgs e)
        {
            ClearData();

        }

        private void cbFilterMasterBatch_CheckedChanged(object sender, EventArgs e)
        {
            ClearData();


        }

        private void cbFilterPigment_CheckedChanged(object sender, EventArgs e)
        {
            ClearData();


        }

        private void cbFilterSubMaterial_CheckedChanged(object sender, EventArgs e)
        {
            ClearData();


        }

        private void cbFilterPart_CheckedChanged(object sender, EventArgs e)
        {
            ClearData();


        }

        private void cbFilterCarton_CheckedChanged(object sender, EventArgs e)
        {
            ClearData();


        }

        private void cbFilterMasterBatch_CheckedChanged_1(object sender, EventArgs e)
        {
            ClearData();


        }

        private void cbFilterRawMaterial_CheckedChanged_1(object sender, EventArgs e)
        {
            ClearData();


        }

        private void cbFilterPigment_CheckedChanged_1(object sender, EventArgs e)
        {
            ClearData();


        }

        private void cbFilterPart_CheckedChanged_1(object sender, EventArgs e)
        {
            ClearData();

        }

        private void cbFilterSubMaterial_CheckedChanged_1(object sender, EventArgs e)
        {
            ClearData();


        }

        private void cbFilterCarton_CheckedChanged_1(object sender, EventArgs e)
        {
            ClearData();

        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            LoadMatListByMat();
            
        }

        private void ClearData()
        {
            dgvMatListByMat.DataSource = null;
        }

    }
}
