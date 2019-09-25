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

        matPlanDAL dalMatPlan = new matPlanDAL();
        planningDAL dalPlan = new planningDAL();
        itemDAL dalItem = new itemDAL();
        MacDAL dalMac = new MacDAL();
        Tool tool = new Tool();

        readonly string headerType = "TYPE";
        readonly string headerMatCode = "MATERIAL";
        readonly string headerPlanID = "PLAN ID";
        readonly string headerFac = "FAC.";
        readonly string headerMac = "MAC.";
        readonly string headerItem= "PLAN FOR";
        readonly string headerPlanToUse = "PLAN TO USE QTY";
        readonly string headerTotal = "TOTAL(KG)";
        readonly string sortByMat = "Material";
        readonly string sortByFac= "Factory";
        readonly string sortByMac = "Machine";
        readonly string sortByPlan = "Plan";
        readonly string sortByPart = "Part";

        private bool closeForm = false;
        private bool loaded = false;
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
            cmbSort.Text = "Factory";

            string test = cmbSort.Text;
        }

        private DataTable NewMatListTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerPlanID, typeof(int));
            dt.Columns.Add(headerFac, typeof(string));
            dt.Columns.Add(headerMac, typeof(string));
            dt.Columns.Add(headerItem, typeof(string));
            dt.Columns.Add(headerPlanToUse, typeof(float));
            dt.Columns.Add(headerTotal, typeof(float));

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
            dgv.Columns[headerTotal].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////
            dgv.Columns[headerPlanID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPlanToUse].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTotal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerFac].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerMac].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[headerTotal].DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        #region Load Data

        private void LoadMatList()
        {
            DataTable dt_MAT = NewMatListTable();

            DataRow row_dtMat;

            float total = 0;
            string matCode = null;

            DataTable dt = dalMatPlan.Select();

            string sortBy = cmbSort.Text;

            if(sortBy.Equals(sortByFac))
            {
                DataView dv = dt.DefaultView;
                dv.Sort = dalMac.MacLocation+ " asc, "+ dalMac.MacID+" asc";
                dt = dv.ToTable();
            }
            else if (sortBy.Equals(sortByMac))
            {
                DataView dv = dt.DefaultView;
                dv.Sort = dalMac.MacID + " asc";
                dt = dv.ToTable();
            }
            else if (sortBy.Equals(sortByPlan))
            {
                DataView dv = dt.DefaultView;
                dv.Sort = dalPlan.planID + " asc";
                dt = dv.ToTable();
            }
            else if (sortBy.Equals(sortByPart))
            {
                DataView dv = dt.DefaultView;
                dv.Sort = dalPlan.partCode + " asc";
                dt = dv.ToTable();
            }

            foreach (DataRow row in dt.Rows)
            {
                bool active = Convert.ToBoolean(row[dalMatPlan.Active]);
                if(active)
                {
                    if (matCode == null)
                    {
                        matCode = row[dalMatPlan.MatCode].ToString();
                        total = Convert.ToSingle(row[dalMatPlan.PlanToUse]);
                    }
                    else if (matCode == row[dalMatPlan.MatCode].ToString())
                    {
                        total += Convert.ToSingle(row[dalMatPlan.PlanToUse]);
                    }
                    else//new data
                    {
                        dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerTotal] = total;
                        matCode = row[dalMatPlan.MatCode].ToString();
                        total = Convert.ToSingle(row[dalMatPlan.PlanToUse]);
                        row_dtMat = dt_MAT.NewRow();
                        dt_MAT.Rows.Add(row_dtMat);
                    }

                    row_dtMat = dt_MAT.NewRow();

                    row_dtMat[headerType] = dalItem.getCatName(row[dalMatPlan.MatCode].ToString());
                    row_dtMat[headerMatCode] = matCode;
                    row_dtMat[headerPlanID] = row[dalMatPlan.PlanID];
                    row_dtMat[headerFac] = row[dalMac.MacLocation];
                    row_dtMat[headerMac] = row[dalPlan.machineID];
                    string partCode = row[dalPlan.partCode].ToString();
                    row_dtMat[headerItem] = tool.getItemName(partCode) + "(" + partCode + ")";
                    row_dtMat[headerPlanToUse] = row[dalMatPlan.PlanToUse];
                    //row_dtMat[heade] = row[dalMatPlan.MatUsed];

                    dt_MAT.Rows.Add(row_dtMat);
                }
            }


           
            //add datatable to datagridview if got data
            if (dt_MAT.Rows.Count > 0)
            {
                dt_MAT.Rows[dt_MAT.Rows.Count - 1][headerTotal] = total;
                dgvMatList.DataSource = dt_MAT;
                dgvMatListUIEdit(dgvMatList);
                dgvMatList.ClearSelection();
            }
            else
            {
                MessageBox.Show("No data found!");
                closeForm = true;
            }
        }

        #endregion

        private void frmMatPlanningList_Load(object sender, EventArgs e)
        {
            loaded = true;

            if (closeForm)
            {
                Close();
            }
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

                if(string.IsNullOrEmpty(matCode))
                {
                    dgv.Rows[row].Height = 4;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    dgv.Rows[row].Height = 50;
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
    }
}
