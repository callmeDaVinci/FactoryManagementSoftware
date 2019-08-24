using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmPlanningAddSubItem : Form
    {
        Tool tool = new Tool();
        itemDAL dalItem = new itemDAL();

        private string ableQty = null;
        static public bool dataSaved = false;
        static public string itemCat;
        static public string itemCode;
        static public string itemName;
        static public string itemStock;
        static public string itemPlannedUse;
        static public string itemAvailableQty;
        static public string itemRequiredQty;
        static public string itemShort;

        public frmPlanningAddSubItem()
        {
            InitializeComponent();
            tool.loadItemCategoryDataToComboBox(cmbCategory);
            cmbCategory.SelectedIndex = -1;
        }

        public frmPlanningAddSubItem(string ableToProduce)
        {
            InitializeComponent();
            ableQty = ableToProduce;
            tool.loadItemCategoryDataToComboBox(cmbCategory);
            cmbCategory.SelectedIndex = -1;
        }

        public frmPlanningAddSubItem(string itemCat, string itemCode, string itemName, string RequiredQty)
        {
            InitializeComponent();
            ableQty = RequiredQty;
            tool.loadItemCategoryDataToComboBox(cmbCategory);
            cmbCategory.Text = itemCat;
            cmbPartName.Text = itemName;
            cmbPartCode.Text = itemCode;
            txtRequiredQty.Text = RequiredQty;

            cmbCategory.Enabled = false;
            cmbPartName.Enabled = false;
            cmbPartCode.Enabled = false;

            loadItemStock();
            txtRequiredQty.Focus();
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtPlannedUse_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtAvailable_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            string keywords = cmbCategory.Text;
            tool.loadItemNameDataToComboBox(cmbPartName, keywords);
        }

        private void loadItemStock()
        {
            string keywords = cmbPartCode.Text;
            if (!string.IsNullOrEmpty(keywords))
            {
                float stock = dalItem.getStockQty(keywords);

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////pending load data from database
                float plannedUse = 0;
                float requiredQty = ableQty == null ? 0 : Convert.ToSingle(ableQty);
                txtStock.Text = stock.ToString();
                txtPlannedUse.Text = plannedUse.ToString();
                txtAvailable.Text = (stock - plannedUse).ToString();
                txtRequiredQty.Text = requiredQty.ToString();
                txtShort.Text = (stock - plannedUse - requiredQty).ToString();

                if (stock - plannedUse - requiredQty < 0)
                {
                    txtShort.ForeColor = Color.Red;
                }
                else
                {
                    txtShort.Text = "0";
                    txtShort.ForeColor = Color.Black;
                }
            }
        }

        private void cmbPartCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
            loadItemStock();
        }

        private void cmbPartName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            string keywords = cmbPartName.Text;
            tool.loadItemCodeDataToComboBox(cmbPartCode, keywords);
        }

        private void txtRequiredQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider4.Clear();
            float stock = txtStock.Text == ""? 0 : Convert.ToSingle(txtStock.Text);

            float plannedUse = txtPlannedUse.Text == "" ? 0 : Convert.ToSingle(txtPlannedUse.Text);

            float requiredQty = txtRequiredQty.Text == "" ? 0 : Convert.ToSingle(txtRequiredQty.Text);

            txtShort.Text = (stock - plannedUse - requiredQty).ToString();

            if (stock - plannedUse - requiredQty < 0)
            {
                txtShort.ForeColor = Color.Red;
            }
            else
            {
                txtShort.Text = "0";
                txtShort.ForeColor = Color.Black;
            }
        }

        private void txtRequiredQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbCategory.Text))
            {
                result = false;
                errorProvider1.SetError(cmbCategory, "Item Category Required");
            }

            if (string.IsNullOrEmpty(cmbPartName.Text))
            {
                result = false;
                errorProvider2.SetError(cmbPartName, "Item Name Required");
            }

            if (string.IsNullOrEmpty(cmbPartCode.Text))
            {
                result = false;
                errorProvider3.SetError(cmbPartCode, "Item Code Required");
            }

            if (string.IsNullOrEmpty(txtRequiredQty.Text))
            {
                result = false;
                errorProvider4.SetError(txtRequiredQty, "Required Qty need");
            }

            return result;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                itemCat = cmbCategory.Text;
                itemName = cmbPartName.Text;
                itemCode = cmbPartCode.Text;
                itemStock = txtStock.Text == ""? "0" : txtStock.Text;
                itemPlannedUse = txtPlannedUse.Text == "" ? "0" : txtPlannedUse.Text;
                itemAvailableQty = txtAvailable.Text == "" ? "0" : txtAvailable.Text;
                itemRequiredQty = txtRequiredQty.Text == "" ? "0" : txtRequiredQty.Text;
                itemShort = txtShort.Text == "" ? "0" : txtShort.Text;

                dataSaved = true;

                MessageBox.Show("Data save successfully!");
                Close();
            }
        }

        private void frmPlanningAddSubItem_Load(object sender, EventArgs e)
        {
            ActiveControl = txtRequiredQty;
        }

        private void txtRequiredQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave_Click(sender, e);
            }
        }

        private void txtPlannedUse_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
