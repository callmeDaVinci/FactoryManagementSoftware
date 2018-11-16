using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderInput : Form
    {
        public frmOrderInput()
        {
            InitializeComponent();
        }

        #region create class object (database)

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

        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

        facStockBLL uStock = new facStockBLL();
        facStockDAL dalStock = new facStockDAL();

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        materialBLL uMaterial = new materialBLL();
        materialDAL dalMaterial = new materialDAL();

        childTrfHistBLL uChildTrfHist = new childTrfHistBLL();
        childTrfHistDAL dalChildTrfHist = new childTrfHistDAL();


        #endregion

        #region load data

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dalItemCat.Select();
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbItemCat.DataSource = distinctTable;
            cmbItemCat.DisplayMember = "item_cat_name";
        }

        private void loadItemNameData()
        {
            string keywords = cmbItemCat.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.catSearch(keywords);
                DataTable dtItemName = dt.DefaultView.ToTable(true, "item_name");

                dtItemName.DefaultView.Sort = "item_name ASC";
                cmbItemName.DataSource = dtItemName;
                cmbItemName.DisplayMember = "item_name";
            }
            else
            {
                cmbItemName.DataSource = null;
            }
        }

        private void loadItemCodeData()
        {
            string keywords = cmbItemName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
                DataTable dtItemCode = dt.DefaultView.ToTable(true, "item_code");

                dtItemCode.DefaultView.Sort = "item_code ASC";
                cmbItemCode.DataSource = dtItemCode;
                cmbItemCode.DisplayMember = "item_code";
            }
            else
            {
                cmbItemCode.DataSource = null;
            }
        }

        private void unitDataSource()
        {
            string itemCat = cmbItemCat.Text;
            DataTable dt = new DataTable();
            dt.Columns.Add("item_unit");

            if (itemCat.Equals("RAW Material") || itemCat.Equals("Master Batch") || itemCat.Equals("Pigment"))
            {
                dt.Clear();
                dt.Rows.Add("kg");
                dt.Rows.Add("g");

            }
            else if (itemCat.Equals("Part") || itemCat.Equals("Carton"))
            {
                dt.Clear();
                dt.Rows.Add("set");
                dt.Rows.Add("piece");
                cmbQtyUnit.DataSource = dt;
            }
            else if (!string.IsNullOrEmpty(itemCat))
            {
                dt.Clear();
                dt.Rows.Add("set");
                dt.Rows.Add("piece");
                dt.Rows.Add("kg");
                dt.Rows.Add("g");

            }
            else
            {
                dt = null;
            }

            cmbQtyUnit.DataSource = dt;
            cmbQtyUnit.DisplayMember = "item_unit";
            cmbQtyUnit.SelectedIndex = -1;

        }

        #endregion

        #region validation

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbItemCat.Text))
            {
                result = false;
                errorProvider1.SetError(cmbItemCat, "Item Category Required");
            }

            if (string.IsNullOrEmpty(cmbItemName.Text))
            {
                result = false;
                errorProvider2.SetError(cmbItemName, "Item Name Required");
            }

            if (string.IsNullOrEmpty(cmbItemCode.Text))
            {
                result = false;
                errorProvider3.SetError(cmbItemCode, "Item Code Required");
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                result = false;
                errorProvider4.SetError(txtQty, "Item order qty Required");
            }

            if (string.IsNullOrEmpty(cmbQtyUnit.Text))
            {
                result = false;
                errorProvider5.SetError(cmbQtyUnit, "Item order unit Required");
            }

            if (string.IsNullOrEmpty(dtpForecastDate.Text))
            {
                result = false;
                errorProvider6.SetError(dtpForecastDate, "Item forecast date Required");
            }

            return result;
        }

        #endregion

        private void frmOrderInput_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            loadItemCategoryData();

            cmbItemCat.SelectedIndex = -1;

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void cmbItemCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            loadItemNameData();
            unitDataSource();
        }

        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            loadItemCodeData();
        }

        private void cmbItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider4.Clear();
        }

        private void cmbQtyUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider5.Clear();
        }

        private void dtpForecastDate_ValueChanged(object sender, EventArgs e)
        {
            errorProvider6.Clear();
        }


        #region function: order/cancel

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Validation();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
