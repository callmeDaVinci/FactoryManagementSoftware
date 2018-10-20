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

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        trfCatBLL utrfCat = new trfCatBLL();
        trfCatDAL daltrfCat = new trfCatDAL();


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
                dgvItem.Rows[n].Cells["item_cat"].Value = item["item_cat"].ToString();
                dgvItem.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                dgvItem.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                dgvItem.Rows[n].Cells["item_qty"].Value = item["item_qty"].ToString();
                dgvItem.Rows[n].Cells["item_ord"].Value = item["item_ord"].ToString();
            }

        }

        private void resetForm()
        {
            loadData();

            //select item data from item category database
            DataTable dtItemCat = dalItemCat.Select();
            //remove repeating name in item_name
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            //sort the data according item_name
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            //set combobox datasource from table
            cmbTrfItemCat.DataSource = distinctTable;
            //show item_name data from table only
            cmbTrfItemCat.DisplayMember = "item_cat_name";

            /*
            //select item data from item database
            DataTable dtItem = dalItem.Select();
            //remove repeating name in item_name
            DataTable distinctTable2 = dtItem.DefaultView.ToTable(true, "item_name");
            //sort the data according item_name
            distinctTable2.DefaultView.Sort = "item_name ASC";
            //DataView dv = new DataView(distinctTable);
            //dv.RowFilter = "item_name <> 'PP'";
            //set combobox datasource from table
            cmbTrfItemName.DataSource = distinctTable2;
            //show item_name data from table only
            cmbTrfItemName.DisplayMember = "item_name";
            */

            //select item data from item database
            DataTable dtTrfCatFrm = daltrfCat.Select();
            //remove repeating name in item_name
            DataTable distinctTable3 = dtTrfCatFrm.DefaultView.ToTable(true, "trf_cat_name");
            //sort the data according item_name
            distinctTable3.DefaultView.Sort = "trf_cat_name ASC";
            //set combobox datasource from table
            cmbTrfFromCategory.DataSource = distinctTable3;
            //show item_name data from table only
            cmbTrfFromCategory.DisplayMember = "trf_cat_name";


            //select item data from item database
            DataTable dtTrfCatTo = daltrfCat.Select();
            //remove repeating name in item_name
            DataTable distinctTable4 = dtTrfCatTo.DefaultView.ToTable(true, "trf_cat_name");
            //sort the data according item_name
            distinctTable4.DefaultView.Sort = "trf_cat_name ASC";
            //set combobox datasource from table
            cmbTrfToCategory.DataSource = distinctTable4;
            //show item_name data from table only
            cmbTrfToCategory.DisplayMember = "trf_cat_name";


        }



        #endregion

        #region Data Grid View

        private void dgvItem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            cmbTrfItemCat.Text = dgvItem.Rows[rowIndex].Cells["item_cat"].Value.ToString();
            cmbTrfItemName.Text = dgvItem.Rows[rowIndex].Cells["item_name"].Value.ToString();
            cmbTrfItemCode.Text = dgvItem.Rows[rowIndex].Cells["item_code"].Value.ToString();
        }



        #endregion

        private void cmbTrfItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = cmbTrfItemName.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                
                //show user based on keywords
                DataTable dt = dalItem.Search(keywords);
                cmbTrfItemCode.DataSource = dt;
                cmbTrfItemCode.DisplayMember = "item_code";
                cmbTrfItemCode.ValueMember = "item_code";
                

            }
            else
            {
                cmbTrfItemCode.DataSource = null;
            }
        }

        private void cmbTrfItemCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get keyword from text box
            string keywords = cmbTrfItemCat.Text;

            //check if the keywords has value or not
            if (keywords != null)
            {
                //show user based on keywords
                DataTable dt = dalItem.catSearch(keywords);
                //remove repeating name in item_name
                DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                //sort the data according item_name
                distinctTable.DefaultView.Sort = "item_name ASC";
                cmbTrfItemName.DataSource = distinctTable;
                cmbTrfItemName.DisplayMember = "item_name";
                cmbTrfItemName.ValueMember = "item_name";

                if(string.IsNullOrEmpty(cmbTrfItemName.Text))
                {
                    cmbTrfItemCode.DataSource = null;
                }

            }
            else
            {
                cmbTrfItemName.DataSource = null;
            }
        }

        private void cmbTrfItemName_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cmbTrfItemName.Text))
            {
                cmbTrfItemCode.DataSource = null;
            }
        }
    }
}
