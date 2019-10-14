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
    public partial class frmMatListEdit : Form
    {
        public frmMatListEdit()
        {
            InitializeComponent();
        }

        #region Variable / Object Declare

        facStockDAL dalStock = new facStockDAL();
        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();
        planningDAL dalPlan = new planningDAL();
        itemDAL dalItem = new itemDAL();
        MacDAL dalMac = new MacDAL();
        joinDAL dalJoin = new joinDAL();
        joinBLL uJoin = new joinBLL();

        Tool tool = new Tool();

        readonly string headerID = "PLAN";
        readonly string headerMatCode = "MATERIAL";
        readonly string headerMatUse = "MAT. USE (KG/PIECE)";

        readonly string headerFac = "FAC.";
        readonly string headerStart = "START";
        readonly string headerEnd = "END";
        readonly string headerMac = "MAC.";

        readonly string headerTargetQty = "TARGET QTY";
        readonly string headerAbleProduceQty = "ABLE PRODUCE";
        readonly string headerPartCode = "PART CODE";
        readonly string headerPartName = "PART NAME";

        //readonly string headerItem = "PLAN FOR";
        readonly string ContextEditMaterial = "Edit Material";
        readonly string ContextPairMaterial = "Pair Material";

        private DataTable dt_MaterialPlan;

        #endregion

        #region UI Setting

        private DataTable NewMatlistTable()
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

        private void dgvMatListUIEdit(DataGridView dgv)
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

        private void LoadMatList()
        {
            dgvMatList.DataSource = null;

            #region variable
            DataTable dt_MAT = NewMatlistTable();
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
            //dv.Sort = dalItem.ItemCat + " asc, " + "mat_code asc, " + dalMac.MacLocation + " asc, " + dalPlan.productionStartDate + " asc";
            dv.Sort = dalMac.MacID + " asc, " + dalPlan.productionStartDate + " asc, " + dalPlan.partCode + " asc"; 
            //dv.Sort = dalMac.MacLocation + " asc, " + dalMac.MacID + " asc, " + dalPlan.productionStartDate + " asc, " + dalPlan.partCode + " asc, " + "mat_code asc ";
            dt = dv.ToTable();

            dt = specialDataSort(dt);

            dt_MaterialPlan = dt;
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

            foreach (DataRow row in dt.Rows)
            {
                bool active = Convert.ToBoolean(row[dalMatPlan.Active]);
                if (active)
                {
                    partCode = row[dalPlan.partCode].ToString();

                    if(prePartCode == null || prePartCode == partCode)
                    {
                        if(prePartCode != null)
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
                    
                    if(!isSamePlan)
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
                dgvMatList.DataSource = dt_MAT;

                dgvMatListUIEdit(dgvMatList);

                dgvMatList.ClearSelection();

                //addComboBoxToDGVCell();
            }
            else
            {
                MessageBox.Show("No data found!");
            }
            #endregion
        }

        #endregion

        private void frmMatListEdit_Load(object sender, EventArgs e)
        {
            LoadMatList();
        }

        private void dgvMatList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvMatList;

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
                    
                    my_menu.Items.Add(ContextEditMaterial).Name = ContextEditMaterial;
                    my_menu.Items.Add(ContextPairMaterial).Name = ContextPairMaterial;

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

            DataGridView dgv = dgvMatList;
            dgv.SuspendLayout();
            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
            
            
            int planID = dgv.Rows[rowIndex].Cells[headerID].Value == DBNull.Value ? -1 : Convert.ToInt32(dgv.Rows[rowIndex].Cells[headerID].Value);

            if(planID != -1)
            {
                if (itemClicked.Equals(ContextEditMaterial))
                {
                    EditMaterial(rowIndex);

                }
                else if (itemClicked.Equals(ContextPairMaterial))
                {

                    PairMaterial(rowIndex, planID);
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
            DataGridView dgv = dgvMatList;

            string parentCode = "";
            //get parent code by searching plain id in mat_plan db
            DataTable dt_Plan = dalPlan.idSearch(planID.ToString());


            foreach(DataRow row_Plan in dt_Plan.Rows)
            {
                parentCode = row_Plan[dalItem.ItemCode].ToString();
            }

            uJoin.join_parent_code = parentCode;

            uJoin.join_child_code = "";

            uJoin.join_qty = 1;
            uJoin.join_max = 1;
            uJoin.join_min = 1;

            frmJoinEdit frm = new frmJoinEdit(uJoin,true);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit
            LoadMatList();
        }

        private void EditMaterial(int row)
        {
            DataGridView dgv = dgvMatList;

            int planID = int.TryParse(dgv.Rows[row].Cells[headerID].Value.ToString(), out int i) ? Convert.ToInt32(dgv.Rows[row].Cells[headerID].Value.ToString()) : -1;
            float planToUse = float.TryParse(dgv.Rows[row].Cells[headerMatUse].Value.ToString(), out float j) ? Convert.ToSingle(dgv.Rows[row].Cells[headerMatUse].Value.ToString()) : 0;

            uMatPlan.plan_id = planID;
            uMatPlan.part_code = dgv.Rows[row].Cells[headerPartCode].Value.ToString();
            uMatPlan.part_name = dgv.Rows[row].Cells[headerPartName].Value.ToString();

            uMatPlan.pro_location = dgv.Rows[row].Cells[headerFac].Value.ToString();
            uMatPlan.pro_machine = dgv.Rows[row].Cells[headerMac].Value.ToString();
            uMatPlan.pro_max_qty = dgv.Rows[row].Cells[headerAbleProduceQty].Value.ToString();
            uMatPlan.pro_target_qty = dgv.Rows[row].Cells[headerTargetQty].Value.ToString();
            uMatPlan.pro_start = dgv.Rows[row].Cells[headerStart].Value.ToString();
            uMatPlan.pro_end = dgv.Rows[row].Cells[headerEnd].Value.ToString();

            uMatPlan.mat_code = dgv.Rows[row].Cells[headerMatCode].Value.ToString();
            uMatPlan.mat_name = tool.getItemName(uMatPlan.mat_code);
            uMatPlan.mat_cat = tool.getItemCat(uMatPlan.mat_code);
            uMatPlan.plan_to_use = planToUse;

            frmMatAddOrEdit frm = new frmMatAddOrEdit(uMatPlan);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit
            LoadMatList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMatList();
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
        }

        private void btnAddMat_Click(object sender, EventArgs e)
        {
            //DataGridView dgv = dgvMatList;

            //string parentCode = "";
            ////get parent code by searching plain id in mat_plan db
            //DataTable dt_Plan = dalPlan.idSearch(planID.ToString());


            //foreach (DataRow row_Plan in dt_Plan.Rows)
            //{
            //    parentCode = row_Plan[dalItem.ItemCode].ToString();
            //}

            //uJoin.join_parent_code = parentCode;

            //uJoin.join_child_code = "";

            //uJoin.join_qty = 1;
            //uJoin.join_max = 1;
            //uJoin.join_min = 1;

            frmMatAddOrEdit frm = new frmMatAddOrEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit
            LoadMatList();
        }
    }
}
