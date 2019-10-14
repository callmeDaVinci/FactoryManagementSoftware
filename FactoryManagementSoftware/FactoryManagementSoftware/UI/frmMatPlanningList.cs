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
            tool.DoubleBuffered(dgvMatList, true);

            loadSortingData();

            LoadMatList();
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

        readonly string headerType = "TYPE";
        readonly string headerMatCode = "MATERIAL";
        readonly string headerPlanID = "PLAN ID";
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

        #endregion

        #region UI Setting


        private void loadSortingData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("sort");
            dt.Rows.Add(sortByFac);
            dt.Rows.Add(sortByMac);
            dt.Rows.Add(sortByMat);
            dt.Rows.Add(sortByPart);
            dt.Rows.Add(sortByPlan);

            dt.DefaultView.Sort = "sort ASC";
            cmbSort.DataSource = dt;
            cmbSort.DisplayMember = "sort";
            cmbSort.Text = "Material";

            string test = cmbSort.Text;
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

        private DataTable NewMatListTable()
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
            //dt.Columns.Add(headerTransferCheck, typeof(bool));

            return dt;
        }

        private void dgvMatListUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerType].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMatCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPlanID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerFac].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMac].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerItem].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            dgv.Columns[headerFac].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.Columns[headerTransferPending].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.Columns[headerFrom].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[headerFrom].MinimumWidth = 200;

        }

        #endregion

        #region Load Data

        private void LoadMatList()
        {
            
            dgvMatList.DataSource = null;
            dataChanged = false;
            btnSave.Visible = false;

            #region variable
            DataTable dt_MAT = NewMatListTable();
            DataRow row_dtMat;
            DataTable dt_ItemInfo = dalItem.Select();

            float stillNeed = 0;
            float remainingStock = 0;
            float TransferPending = 0;
            float prepareQty = 0;
            float stockQty = 0;
            string preMatCode = null;

            string matCode = null;
            string matFrom = null;

            DataTable dt = dalMatPlan.Select();
            DataTable dt_Stock = dalStock.Select();
            //string sortBy = cmbSort.Text;
            #endregion

            DataView dv = dt.DefaultView;
            dv.Sort = dalItem.ItemCat + " asc, " + "mat_code asc, " + dalMac.MacLocation + " asc, " + dalPlan.productionStartDate + " asc";
            dt = dv.ToTable();

            #region sorting
            //if (sortBy.Equals(sortByFac))
            //{
            //    DataView dv = dt.DefaultView;
            //    //dv.Sort = dalMac.MacLocation+ " asc";
            //    //dv.Sort = dalMac.MacLocation + " asc, " + dalMac.MacID + " asc";
            //    dv.Sort = dalMac.MacLocation + " asc, " + dalItem.ItemCat + " asc, " + "mat_code asc, " + dalMac.MacID + " asc, " + dalPlan.productionStartDate + " asc";
            //    dt = dv.ToTable();
            //}
            //else if (sortBy.Equals(sortByMac))
            //{
            //    DataView dv = dt.DefaultView;
            //    dv.Sort = dalMac.MacID + " asc";
            //    dt = dv.ToTable();
            //}
            //else if (sortBy.Equals(sortByPlan))
            //{
            //    DataView dv = dt.DefaultView;
            //    dv.Sort = dalPlan.planID + " asc";
            //    dt = dv.ToTable();
            //}
            //else if (sortBy.Equals(sortByPart))
            //{
            //    DataView dv = dt.DefaultView;
            //    dv.Sort = dalPlan.partCode + " asc";
            //    dt = dv.ToTable();
            //}
            //else if (sortBy.Equals(sortByMat))
            //{
            //    DataView dv = dt.DefaultView;
            //    dv.Sort = dalItem.ItemCat + " asc, " + "mat_code asc, " + dalMac.MacLocation + " asc, " + dalPlan.productionStartDate + " asc";
            //    dt = dv.ToTable();
            //}
            #endregion

            #region data proccessing

            string preFacName = null;
            string facName = null;
            foreach (DataRow row in dt.Rows)
            {
                bool active = Convert.ToBoolean(row[dalMatPlan.Active]);
                string status = row[dalPlan.planStatus].ToString();

                if(active && status != text.planning_status_cancelled && status != text.planning_status_completed)
                {
                    facName = row[dalMac.MacLocation].ToString();
                    matCode = row[dalMatPlan.MatCode].ToString();
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
                    else
                    {
                        row_dtMat = dt_MAT.NewRow();
                        dt_MAT.Rows.Add(row_dtMat);
                        preMatCode = matCode;
                        preFacName = facName;
                        stockQty = tool.getStockBalance(matCode, facName, dt_Stock);
                        remainingStock = stockQty;
                        TransferPending = 0;
                    }

                    row_dtMat = dt_MAT.NewRow();
                    row_dtMat[headerType] = tool.getItemCatFromDataTable(dt_ItemInfo, row[dalMatPlan.MatCode].ToString());
                    row_dtMat[headerMatCode] = matCode;
                    row_dtMat[headerStatus] = status;
                    row_dtMat[headerPlanID] = row[dalMatPlan.PlanID];
                    row_dtMat[headerFac] = facName;
                    row_dtMat[headerMac] = row[dalPlan.machineID];
                    row_dtMat[headerItem] = tool.getItemNameFromDataTable(dt_ItemInfo, partCode) + "(" + partCode + ")";
                    row_dtMat[headerStart] = row[dalPlan.productionStartDate];
                    row_dtMat[headerEnd] = row[dalPlan.productionEndDate];

                    row_dtMat[headerPlanToUse] = planToUse;
                    row_dtMat[headerTransferred] = transferred;

                    stillNeed = planToUse - transferred < 0 ? 0 : planToUse - transferred;

                    row_dtMat[headerStillNeed] = stillNeed;
                    row_dtMat[headerStock] = remainingStock;

                    TransferPending = planToUse - remainingStock;
                    //TransferPending = stillNeed - remainingStock;
                    row_dtMat[headerTransferPending] = TransferPending < 0 ? 0 : TransferPending;

                    row_dtMat[headerPrepare] = prepareQty;
                    row_dtMat[headerFrom] = matFrom;
                    

                    dt_MAT.Rows.Add(row_dtMat);

                    remainingStock = remainingStock - planToUse < 0 ? 0 : remainingStock - planToUse;
                }
            }

            #endregion

            #region set dgv data source
            if (dt_MAT.Rows.Count > 0)
            {
                dgvMatList.DataSource = dt_MAT;

                dgvMatListUIEdit(dgvMatList);
                CheckifRunning(dgvMatList);

                dgvMatList.Columns[headerStatus].Visible = false;

                dgvMatList.Columns[headerStillNeed].Visible = false;

                dgvMatList.ClearSelection();

            }
            else
            {
                MessageBox.Show("No data found!");
                closeForm = true;
            }
            #endregion
        }

        private void CheckifRunning(DataGridView dgv)
        {
            foreach(DataGridViewRow row in dgv.Rows)
            {
                string status = row.Cells[headerStatus].Value.ToString();
                float trfPending = float.TryParse(row.Cells[headerTransferPending].Value.ToString(), out float i) ? Convert.ToSingle(row.Cells[headerTransferPending].Value.ToString()) : 0;

                if (string.IsNullOrEmpty(status))
                {
                    row.Cells[headerTransferPending].Style.BackColor = Color.DimGray;
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
            DataGridView dgv = dgvMatList;

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
                        float preparing = float.TryParse(row.Cells[headerPrepare].Value.ToString(), out float i) ? Convert.ToSingle(row.Cells[headerPrepare].Value) : 0;

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
            loaded = true;
            
            if (closeForm)
            {
                Close();
            }

            CheckifRunning(dgvMatList);

            dgvMatList.Columns[headerStatus].Visible = false;

            dgvMatList.ClearSelection();
           
        }

        private void dgvMatList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMatList;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerMatCode)
            {
                string matCode = dgv.Rows[row].Cells[headerMatCode].Value.ToString();

                if (string.IsNullOrEmpty(matCode))
                {
                    dgv.Rows[row].Height = 5;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.DimGray;
                }
                else
                {
                    dgv.Rows[row].Height = 60;
                }
            }
            //else if (dgv.Columns[col].Name == headerTransferPending)
            //{
            //    string checkIfNull = dgv.Rows[row].Cells[headerTransferPending].Value.ToString();

            //    if (!string.IsNullOrEmpty(checkIfNull))
            //    {
            //        float trfPending = float.TryParse(dgv.Rows[row].Cells[headerTransferPending].Value.ToString(), out float i) ? Convert.ToSingle(dgv.Rows[row].Cells[headerTransferPending].Value.ToString()) : 0;

            //        if (trfPending <= 0)
            //        {
            //            dgv.Rows[row].Cells[headerTransferPending].Style.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            dgv.Rows[row].Cells[headerTransferPending].Style.BackColor = Color.FromArgb(255, 118, 117);
            //        }
            //    }
            //    else
            //    {
            //        dgv.Rows[row].Cells[headerTransferPending].Style.BackColor = Color.DimGray;
            //    }

            //}
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
                    LoadMatList();
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
            DataGridView dgv = dgvMatList;
  
            if(matPrepare)
            {
                matPrepare = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            DataGridView dgv = dgvMatList;
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
            DataGridView dgv = dgvMatList;
            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;
            string previousValue = dgvMatList.CurrentCell.EditedFormattedValue.ToString();
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
            DataGridView dgv = dgvMatList;

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

                            if(facName == locationName)
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
            DataGridView dgv = dgvMatList;

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
                            string itemAdd = matStockFac + " (AVAILABLE STOCK: " + unuse + ")";
                            ComboBoxCell.Items.Add(matStockFac + " (AVAILABLE STOCK: " + unuse + ")");
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
            DataGridView dgv = dgvMatList;
            string newValue = dgvMatList.CurrentCell.EditedFormattedValue.ToString();
            int row = dgvMatList.CurrentCell.RowIndex;
            int col = dgvMatList.CurrentCell.ColumnIndex;

            if (dgv.Columns[col].Name == headerPrepare)
            {
                if (oldValue != newValue && dgvMatList.IsCurrentCellDirty)
                {
                    dataChanged = true;
                }
            }
            else if (dgv.Columns[col].Name == headerFrom)
            {
                if (oldValue != newValue && dgvMatList.IsCurrentCellDirty)
                {
                    dataChanged = true;
                }

                dgv.CurrentCell = dgvMatList[col - 1, row];
                dgv.ClearSelection();

                
            }

        }

        private void dgvMatList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldValue = dgvMatList[e.ColumnIndex, e.RowIndex].Value.ToString();
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

        private void dgvMatList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private bool validation()
        {
            bool passed = true;

            foreach(DataGridViewRow row in dgvMatList.Rows)
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
                        DataTable dt = (DataTable)dgvMatList.DataSource;
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
                                dgvMatList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                                dgvMatList.Columns[headerPrepare].DefaultCellStyle.BackColor = Color.Gainsboro;
                                dgvMatList.ReadOnly = true;
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
                
                LoadMatList();

                if (matPrepare)
                {
                    matPrepare = false;
                    dgvMatList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvMatList.Columns[headerPrepare].DefaultCellStyle.BackColor = Color.Gainsboro;
                    dgvMatList.ReadOnly = true;

                }

            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {

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

            LoadMatList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmMatListEdit frm = new frmMatListEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            LoadMatList();
        }
    }
}
