using System;
using System.Data;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmJoinEdit : Form
    {
        public frmJoinEdit()
        {
            InitializeComponent();
        }

        itemCatDAL dALItemCat = new itemCatDAL();

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        itemDAL dalItem = new itemDAL();


        #region Load/Close

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dALItemCat.Select();

            DataTable childTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            childTable.Rows.Add("ALL");
            childTable.DefaultView.Sort = "item_cat_name ASC";

            cmbChildCat.DataSource = childTable;

            cmbChildCat.DisplayMember = "item_cat_name";

            cmbChildCat.SelectedIndex = -1;

            cmbParentCat.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmJoinEdit_Load(object sender, EventArgs e)
        {
            loadItemCategoryData();
        }

        private void cmbParentCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keywords = cmbParentCat.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                errorProvider1.Clear();
                DataTable dt = dalItem.catSearch(keywords);
                DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                distinctTable.DefaultView.Sort = "item_name ASC";
                cmbParentName.DataSource = distinctTable;
                cmbParentName.DisplayMember = "item_name";
                cmbParentName.ValueMember = "item_name";

                if (string.IsNullOrEmpty(cmbParentName.Text))
                {
                    cmbParentCode.DataSource = null;
                }
            }
            else
            {
                cmbParentName.DataSource = null;
            }

            cmbParentName.SelectedIndex = -1;
        }

        private void cmbChildCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keywords = cmbChildCat.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                errorProvider4.Clear();
                DataTable dt = dalItem.catSearch(keywords);
                DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                distinctTable.DefaultView.Sort = "item_name ASC";
                cmbChildName.DataSource = distinctTable;
                cmbChildName.DisplayMember = "item_name";
                cmbChildName.ValueMember = "item_name";

                if (string.IsNullOrEmpty(cmbChildName.Text))
                {
                    cmbChildCode.DataSource = null;
                }
            }
            else
            {
                cmbChildCode.DataSource = null;
            }
        }

        private void cmbParentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            string keywords = cmbParentName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.Search(keywords);
                cmbParentCode.DataSource = dt;
                cmbParentCode.DisplayMember = "item_code";
                cmbParentCode.ValueMember = "item_code";
            }
            else
            {
                cmbParentCode.DataSource = null;
            }
           
        }

        private void cmbChildName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider5.Clear();
            string keywords = cmbChildName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.Search(keywords);
                cmbChildCode.DataSource = dt;
                cmbChildCode.DisplayMember = "item_code";
                cmbChildCode.ValueMember = "item_code";
            }
            else
            {
                cmbChildCode.DataSource = null;
            }
        }

        #endregion

        #region validation

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbParentCat.Text))
            {

                errorProvider1.SetError(cmbParentCat, "Item Category Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbParentName.Text))
            {

                errorProvider2.SetError(cmbParentName, "Item Name Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbParentCode.Text))
            {

                errorProvider3.SetError(cmbParentCode, "Item Code Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbChildCat.Text))
            {

                errorProvider4.SetError(cmbChildCat, "Item Category Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbChildName.Text))
            {

                errorProvider5.SetError(cmbChildName, "Item name Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbChildCode.Text))
            {

                errorProvider6.SetError(cmbChildCode, "Item code Required");
                result = false;
            }

            if(cmbParentCode.Text.Equals(cmbChildCode.Text))
            {
                MessageBox.Show("Parent cannot same with Child.");
                result = false;
            }
            return result;
        }

        private bool IfProductsExists(string parentCode, string childCode)
        {
            DataTable dt = dalJoin.existCheck(parentCode,childCode);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void cmbParentCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
        }

        private void cmbChildCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider6.Clear();
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (IfProductsExists(cmbParentCode.Text, cmbChildCode.Text))
                    {
                        MessageBox.Show("record already exist");
                    }
                    else
                    {
                        uJoin.join_parent_code = cmbParentCode.Text;
                     
                        uJoin.join_child_code = cmbChildCode.Text;
                      
                        uJoin.join_added_date = DateTime.Now;
                        uJoin.join_added_by = -1;

                        if (!string.IsNullOrEmpty(txtQty.Text))
                        {
                            uJoin.join_qty = Convert.ToInt32(txtQty.Text);
                        }
                        else
                        {
                            uJoin.join_qty = 1;
                        }
                        
                        bool success = dalJoin.Insert(uJoin);
                        //If the data is successfully inserted then the value of success will be true else false
                        if (success == true)
                        {
                            //Data Successfully Inserted
                            MessageBox.Show("Join successfully created");
                            this.Close();
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new join");
                        }
                    }
                }
            }
        }
    }
}
