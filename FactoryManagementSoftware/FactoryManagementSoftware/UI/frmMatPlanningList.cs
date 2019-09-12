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
            LoadMatList();
        }

        matPlanDAL dalMatPlan = new matPlanDAL();
        planningDAL dalPlan = new planningDAL();
        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();

        readonly string headerType = "TYPE";
        readonly string headerMatCode = "MATERIAL";
        readonly string headerPlanID = "PLAN ID";
        readonly string headerItem= "PLAN FOR";
        readonly string headerPlanToUse = "PLAN TO USE QTY";
        readonly string headerTotal = "TOTAL(KG)";

        private bool closeForm = false;
        #region UI Setting

        private DataTable NewMatListTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerPlanID, typeof(int));
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
            dgv.Columns[headerItem].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerPlanToUse].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTotal].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////
            dgv.Columns[headerPlanID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPlanToUse].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTotal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            foreach(DataRow row in dt.Rows)
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

        private void dgvMatList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void frmMatPlanningList_Load(object sender, EventArgs e)
        {
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
            }
                
        }
    }
}
