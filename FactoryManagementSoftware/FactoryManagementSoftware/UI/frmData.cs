using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmData : Form
    {
        public frmData()
        {
            InitializeComponent();
        }

        dataDAL dalData = new dataDAL();





        #region Load or Reset Form

        private void frmData_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.dataFormOpen = false;
        }

        private void loadData(string tableName)
        {
            DataTable dt = dalData.Select(tableName);
            dgvData.DataSource = dt;
        }

        private void resetForm()
        {
            DataTable dt = dalData.SelectTableName();
            cmbTableName.DataSource = dt;
            cmbTableName.DisplayMember = "TABLE_NAME";
        }

        private void frmData_Load(object sender, EventArgs e)
        {
            resetForm();
        }


        #endregion

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string tableName = cmbTableName.Text;

            if (!string.IsNullOrEmpty(tableName))
            {
                loadData(tableName);
            }
        }

        private void cmbTableName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
