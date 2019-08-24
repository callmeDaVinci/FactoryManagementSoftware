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
        itemDAL dalItem = new itemDAL();

        readonly string headerType = "TYPE";
        readonly string headerMatCode = "MATERIAL";
        readonly string headerPlanToUse = "PLAN TO USE QTY";

        #region UI Setting

        private DataTable NewMatListTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerPlanToUse, typeof(float));

            return dt;
        }

        private void dgvMatListUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerType].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMatCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerPlanToUse].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
         
            //alignment//////////////////////////////////////////////////////////////////////////////////////////////////
            dgv.Columns[headerPlanToUse].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           
        }

        #endregion

        #region Load Data

        private void LoadMatList()
        {
            DataTable dt_MAT = NewMatListTable();

            DataRow row_dtMat;


            DataTable dt = dalMatPlan.Select();

            foreach(DataRow row in dt.Rows)
            {
                row_dtMat = dt_MAT.NewRow();
                
                row_dtMat[headerMatCode] = row[dalMatPlan.MatCode];
                row_dtMat[headerType] = dalItem.getCatName(row[dalMatPlan.MatCode].ToString());
                row_dtMat[headerPlanToUse] = row[dalMatPlan.PlanToUse];

                dt_MAT.Rows.Add(row_dtMat);
            }
            


            //add datatable to datagridview if got data
            if (dt_MAT.Rows.Count > 0)
            {
                dgvMatList.DataSource = dt_MAT;
                dgvMatListUIEdit(dgvMatList);
                dgvMatList.ClearSelection();
            }
        }

        #endregion

        private void dgvMatList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmMatPlanningList_Load(object sender, EventArgs e)
        {
            dgvMatList.ClearSelection();
        }
    }
}
