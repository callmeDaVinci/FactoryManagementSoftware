using System;
using System.Data;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System.Windows.Forms;


namespace FactoryManagementSoftware.UI
{
    public partial class frmJoinEdit : Form
    {
        public frmJoinEdit()
        {
            InitializeComponent();
            loadItemCategoryData();
        }

        public frmJoinEdit(joinBLL u)
        {
            InitializeComponent();
            uJoin = u;
            loadItemCategoryData();

            cmbParentName.Text = tool.getItemName(u.join_parent_code);
            cmbParentCode.Text = u.join_parent_code;

            cmbChildCat.Text = dalItem.getCatName(u.join_child_code);
            cmbChildName.Text = tool.getItemName(u.join_child_code);
            cmbChildCode.Text = u.join_child_code;

            txtQty.Text = u.join_qty.ToString();
            txtMax.Text = u.join_max.ToString();
            txtMin.Text = u.join_min.ToString();

            updateTestName();
        }

        itemCatDAL dALItemCat = new itemCatDAL();

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        itemDAL dalItem = new itemDAL();

        Tool tool = new Tool();

        #region Load/Close

        private void loadItemCategoryData()
        {
            //DataTable dtItemCat = dALItemCat.Select();

            //DataTable childTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            //childTable.Rows.Add("ALL");
            //childTable.DefaultView.Sort = "item_cat_name ASC";

            DataTable childTable = new DataTable();
            childTable.Columns.Add("CATEGORY");
            childTable.Rows.Add("Carton");
            childTable.Rows.Add("Part");
            childTable.Rows.Add("Poly Bag");
            childTable.Rows.Add("Sub Material");

            cmbChildCat.DataSource = childTable;

            cmbChildCat.DisplayMember = "CATEGORY";

            

            
            cmbChildCat.SelectedIndex = -1;

            cmbParentCat.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmJoinEdit_Load(object sender, EventArgs e)
        {
            
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
            updateTestName();
        }

        private void cmbChildCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keywords = cmbChildCat.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                errorProvider4.Clear();

                if(keywords.Equals("Carton"))
                {
                    txtMax.Enabled = true;
                    txtMin.Enabled = true;
                    txtQty.Enabled = false;
                }
                else
                {
                    txtMax.Enabled = false;
                    txtMin.Enabled = false;
                    txtQty.Enabled = true;
                }

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
            updateTestName();
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

            updateTestName();

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

            updateTestName();
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

            if (cmbChildCat.Text.Equals("Carton"))
            {
                if(string.IsNullOrEmpty(txtMax.Text))
                {
                    errorProvider7.SetError(txtMax, "Max Qty Required");
                    result = false;
                }

                if (string.IsNullOrEmpty(txtMin.Text))
                {
                    errorProvider8.SetError(txtMin, "Min Qty Required");
                    result = false;
                }

            }
            else
            {
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    errorProvider7.SetError(txtQty, "Join Qty Required");
                    result = false;
                }
            }

            if (cmbParentCode.Text.Equals(cmbChildCode.Text))
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
            updateTestName();
        }

        private void cmbChildCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider6.Clear();
            updateTestName();
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                   
                    uJoin.join_parent_code = cmbParentCode.Text;
                     
                    uJoin.join_child_code = cmbChildCode.Text;

                    frmJoin.editedParentCode = uJoin.join_parent_code;
                    frmJoin.editedChildCode = uJoin.join_child_code;

                    if (cmbChildCat.Text.Equals("Carton"))
                    {
                        uJoin.join_max = Convert.ToInt32(txtMax.Text);
                        uJoin.join_min = Convert.ToInt32(txtMin.Text);
                        uJoin.join_qty = 0;
                    }
                    else
                    {
                        uJoin.join_max = 0;
                        uJoin.join_min = 0;
                        uJoin.join_qty = Convert.ToInt32(txtQty.Text);
                    }

                    

                    DataTable dt_existCheck = dalJoin.existCheck(uJoin.join_parent_code, uJoin.join_child_code);

                    bool success = false;

                    if (dt_existCheck.Rows.Count > 0)
                    {
                        uJoin.join_updated_date = DateTime.Now;
                        uJoin.join_updated_by = MainDashboard.USER_ID;
                        //update data
                        success = dalJoin.UpdateWithMaxMin(uJoin);
                        //If the data is successfully inserted then the value of success will be true else false
                        if (success)
                        {
                            //Data Successfully Inserted
                            MessageBox.Show("Join updated.");
                            // this.Close();
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to update join");
                        }
                    }
                    else
                    {
                        uJoin.join_added_date = DateTime.Now;
                        uJoin.join_added_by = MainDashboard.USER_ID;
                        //insert new data
                        success = dalJoin.InsertWithMaxMin(uJoin);
                        //If the data is successfully inserted then the value of success will be true else false
                        if (success)
                        {
                            //Data Successfully Inserted
                            MessageBox.Show("Join successfully created");
                            // this.Close();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateTestQty();
            errorProvider7.Clear();
        }

        private void txtTestParentQty_TextChanged(object sender, EventArgs e)
        {
            updateTestQty();

        }

        private void txtMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtTestParentQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtTestChildQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            updateTestQty();
            errorProvider7.Clear();
        }

        private void updateTestQty()
        {
            if (!txtTestParentQty.Text.Equals("") && !txtQty.Text.Equals(""))
            {
                if(!cmbChildCat.Text.Equals("Carton"))
                {
                    int joinQty = Convert.ToInt32(txtQty.Text);
                    int testParentQty = Convert.ToInt32(txtTestParentQty.Text);
                    txtTestChildQty.Text = (joinQty * testParentQty).ToString();
                }
                else
                {
                    int maxQty = txtMax.Text.Equals("") || txtMax.Text.Equals("0") ? 1: Convert.ToInt16(txtMax.Text);
                    int minQty = txtMin.Text.Equals("") || txtMin.Text.Equals("0") ? 1 : Convert.ToInt16(txtMin.Text);
                    int testParentQty = Convert.ToInt32(txtTestParentQty.Text);

                    int fullCartonQty = testParentQty / maxQty;

                    int notFullCartonQty = testParentQty % maxQty / minQty;

                    txtTestChildQty.Text = (fullCartonQty + notFullCartonQty).ToString();
                }
                
            }
            else
            {
                txtTestChildQty.Text = 0.ToString();
            }
        }

        private void updateTestName()
        {
            txtParentCode.Text = cmbParentName.Text + "(" + cmbParentCode.Text + ")";
            txtChildCode.Text = cmbChildName.Text + "(" + cmbChildCode.Text + ")";
        }

        private void txtMin_TextChanged(object sender, EventArgs e)
        {
            updateTestQty();
            errorProvider8.Clear();
        }
    }
}
