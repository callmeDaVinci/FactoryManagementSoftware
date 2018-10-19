using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmInOut : Form
    {
        public frmInOut()
        {
            InitializeComponent();
        }

        custBLL uCust = new custBLL();
        custDAL dalCust = new custDAL();

        facBLL uFac = new facBLL();
        facDAL dalFac = new facDAL();

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();


        #region Load or Reset Form

        private void frmInOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.inOutFormOpen = false;
        }

        private void frmInOut_Load(object sender, EventArgs e)
        {
            resetForm();
        }

        private void loadData()
        {
            DataTable dtItem = dalItem.Select();
            dgvItem.Rows.Clear();
            foreach (DataRow item in dtItem.Rows)
            {
                int n = dgvItem.Rows.Add();
                dgvItem.Rows[n].Cells["dgvcItemCode"].Value = item["item_code"].ToString();
                dgvItem.Rows[n].Cells["dgvcItemName"].Value = item["item_name"].ToString();
                dgvItem.Rows[n].Cells["dgvcQty"].Value = item["item_qty"].ToString();
                dgvItem.Rows[n].Cells["dgvcOrd"].Value = item["item_ord"].ToString();
            }

        }

        private void resetForm()
        {
            loadData();

            //select item data from database
            DataTable dtItem = dalItem.Select();
            //remove repeating name in item_name
            DataTable distinctTable = dtItem.DefaultView.ToTable(true, "item_name");
            //sort the data according item_name
            distinctTable.DefaultView.Sort = "item_name ASC";
            //DataView dv = new DataView(distinctTable);
            //dv.RowFilter = "item_name <> 'PP'";
            //set combobox datasource from table
            cmbTrfItemName.DataSource = distinctTable;
            //show item_name data from table only
            cmbTrfItemName.DisplayMember = "item_name";

        }

        #endregion

        #region Data Grid View

     

        #endregion

     

    }
}
