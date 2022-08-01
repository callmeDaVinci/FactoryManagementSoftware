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
            DATA_UPDATED = false;

            InitializeComponent();
            loadItemCategoryData();
        }

        public frmJoinEdit(joinBLL u, bool closeAfterSave)
        {
            CLOSE_PAGE_AFTER_SAVED = closeAfterSave;
            DATA_UPDATED = false;

            InitializeComponent();
            uJoin = u;
            loadItemCategoryData();

            cmbParentName.Text = tool.getItemName(u.join_parent_code);
            cmbParentCode.Text = u.join_parent_code;

            cmbChildCat.Text = dalItem.getCatName(u.join_child_code);
            cmbChildName.Text = tool.getItemName(u.join_child_code);
            LoadChidCodeCMB();
            cmbChildCode.Text = u.join_child_code;

            txtChildQty.Text = u.join_qty.ToString();
            txtMax.Text = u.join_max.ToString();
            txtMin.Text = u.join_min.ToString();

            if (!string.IsNullOrEmpty(u.join_parent_code))
            {
                cmbParentCat.Enabled = false;
                cmbParentName.Enabled = false;
                cmbParentCode.Enabled = false;
            }

            if (!string.IsNullOrEmpty(u.join_child_code))
            {
                cmbChildCat.Enabled = false;
                cmbChildName.Enabled = false;
                cmbChildCode.Enabled = false;

                cmbChildName.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            cbMainCarton.Checked = u.join_main_carton;

            updateTestName();
        }

        itemCatDAL dALItemCat = new itemCatDAL();

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        Tool tool = new Tool();
        Text text = new Text();
        SBBDataDAL dalSBB = new SBBDataDAL();

        private bool CLOSE_PAGE_AFTER_SAVED = false;
        private bool PARENT_JOIN_MIN_MAX_MODE = false;
        static public bool DATA_UPDATED = false;

        private readonly string LBL_PARENT_MAX = "PARENT QTY (MAX)";
        private readonly string LBL_PARENT_NORMAL = "PARENT QTY";

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
            childTable.Rows.Add("Packaging");
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
            ParentQtyMaxMode(false);
        }

        private void ParentQtyMaxMode(bool isMaxMinMode)
        {
            if(isMaxMinMode)
            {
                lblParentQtyMAX.Text = LBL_PARENT_MAX;

                tlpJoin.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 160f);
                tlpJoin.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 20f);
                tlpJoin.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 160f);

            }
            else
            {
                lblParentQtyMAX.Text = LBL_PARENT_NORMAL;

                tlpJoin.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpJoin.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpJoin.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 340f);

              
            }
        }
        private string GetChildCode(DataTable dt, string parentCode)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (parentCode == row[dalJoin.ParentCode].ToString())
                {
                    string childCode = row[dalJoin.ChildCode].ToString();

                    if (childCode.Substring(0, 2) == "CF")
                    {
                        parentCode = childCode;

                        break;
                    }
                }

            }

            return parentCode;
        }

        private void UpdateSBBItemPackaging()
        {
            bool success = true;
            string itemCust = text.SPP_BrandName;
            DataTable dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);
            //DataTable dt_Product = dalItemCust.SBBItemSelect(itemCust); 
            // DataTable dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(itemCust);
            DataTable dt_Item = dalItem.Select();
            DataTable dt_Join = dalJoin.SelectWithChildCat();

            uJoin.join_updated_date = DateTime.Now;
            uJoin.join_updated_by = MainDashboard.USER_ID;
            uJoin.join_added_date = DateTime.Now;
            uJoin.join_added_by = MainDashboard.USER_ID;

            foreach (DataRow row in dt_Product.Rows)
            {
                string itemCode = row[dalSBB.ItemCode].ToString();

                if (itemCode[7].ToString() == "E" && itemCode[8].ToString() != "C")
                {
                    itemCode = GetChildCode(dt_Join, itemCode);
                }

                int qtyPerPacket = int.TryParse(row[dalSBB.QtyPerPacket].ToString(), out qtyPerPacket) ? qtyPerPacket : 0;
                int qtyPerBag = int.TryParse(row[dalSBB.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                uJoin.join_parent_code = itemCode;

                uJoin.join_child_code = cmbChildCode.Text;

                if(cbFollowPacketQty.Checked)
                {
                    uJoin.join_max = qtyPerPacket;
                    uJoin.join_min = qtyPerPacket;
                }
                else if(cbFollowBagQty.Checked)
                {
                    uJoin.join_max = qtyPerBag;
                    uJoin.join_min = qtyPerBag;
                }
                else
                {
                    uJoin.join_max = Convert.ToInt32(txtMax.Text);
                    uJoin.join_min = Convert.ToInt32(txtMin.Text);
                }
                
                uJoin.join_qty = Convert.ToInt32(txtChildQty.Text);

                DataTable dt_existCheck = dalJoin.existCheck(uJoin.join_parent_code, uJoin.join_child_code);

                if (dt_existCheck.Rows.Count > 0)
                {
                    //update data
                    if (cbRemoveItem.Checked)
                    {
                        success = dalJoin.Delete(uJoin);
                    }
                    else
                    {
                        success = dalJoin.UpdateWithMaxMin(uJoin);

                    }
                    
                    if (!success)
                    {
                        MessageBox.Show("Failed to update join");


                    }
                }
                else
                {
                    //insert new data
                    if (!cbRemoveItem.Checked && uJoin.join_max!= 0 && uJoin.join_min != 0)
                    {
                        success = dalJoin.InsertWithMaxMin(uJoin);

                        if (!success)
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new join");
                        }
                    }

                   
                }
            }

            if (success)
            {
                MessageBox.Show("Updated successful!");
            }

        }

        private void cmbParentCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keywords = cmbParentCat.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                errorProvider1.Clear();
                DataTable dt = dalItem.CatSearch(keywords);
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
                    txtChildQty.Enabled = true;
                }
                else
                {
                    txtMax.Enabled = true;
                    txtMin.Enabled = true;
                    txtChildQty.Enabled = true;
                }

                DataTable dt = dalItem.CatSearch(keywords);

                if(dt.Rows.Count > 0)
                {
                    DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                    distinctTable.DefaultView.Sort = "item_name ASC";
                    cmbChildName.DataSource = distinctTable;
                    cmbChildName.DisplayMember = "item_name";
                    cmbChildName.ValueMember = "item_name";

                }

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
                DataTable dt = dalItem.nameSearch(keywords);
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

        private void LoadChidCodeCMB()
        {
            errorProvider5.Clear();
            string keywords = cmbChildName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
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

        private void cmbChildName_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider5.Clear();
            string keywords = cmbChildName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
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

            if (string.IsNullOrEmpty(txtMax.Text))
            {
                errorProvider7.SetError(txtMax, "Max Qty Required");
                result = false;
            }

            if (string.IsNullOrEmpty(txtMin.Text))
            {
                errorProvider8.SetError(txtMin, "Min Qty Required");
                result = false;
            }

            if (string.IsNullOrEmpty(txtChildQty.Text))
            {
                errorProvider7.SetError(txtChildQty, "Join Qty Required");
                result = false;
            }

            if (cmbParentCode.Text.Equals(cmbChildCode.Text))
            {
                MessageBox.Show("Parent cannot same with Child.");
                result = false;
            }
            return result;
        }

        private bool SBBValidation()
        {
            bool result = true;

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

         
            if (string.IsNullOrEmpty(txtChildQty.Text))
            {
                errorProvider7.SetError(txtChildQty, "Join Qty Required");
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

                    uJoin.join_max = int.TryParse(txtMax.Text, out int JoinINT) ? JoinINT : 1;
                    uJoin.join_min = int.TryParse(txtMin.Text, out JoinINT) ? JoinINT : uJoin.join_max;

                    if (cmbChildCat.Text.Equals("Carton"))
                    {
                        uJoin.join_qty = Convert.ToInt32(txtChildQty.Text);
                    }
                    else
                    {
                        uJoin.join_qty = Convert.ToSingle(txtChildQty.Text);
                    }


                    uJoin.join_main_carton = cbMainCarton.Checked;
                    uJoin.join_stock_out = cbStockOut.Checked;

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
                            DATA_UPDATED = true;
                        }
                        else
                        {
                            //Failed to update data
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
                            DATA_UPDATED = true;
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new join");
                        }
                    }

                    if (success && CLOSE_PAGE_AFTER_SAVED)
                    {
                        Close();
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(!PARENT_JOIN_MIN_MAX_MODE)
                txtMin.Text = txtMax.Text;

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
            if (!txtTestParentQty.Text.Equals("") && !txtChildQty.Text.Equals(""))
            {
               
                int maxQty = txtMax.Text.Equals("") || txtMax.Text.Equals("0") ? 1 : Convert.ToInt32(txtMax.Text);
                int minQty = txtMin.Text.Equals("") || txtMin.Text.Equals("0") ? 1 : Convert.ToInt32(txtMin.Text);
                int childQty = txtChildQty.Text.Equals("") || txtChildQty.Text.Equals("0") ? 1 : Convert.ToInt32(txtChildQty.Text);

                int testParentQty = Convert.ToInt32(txtTestParentQty.Text);

                int fullCartonQty = testParentQty / maxQty;

                int notFullCartonQty = testParentQty % maxQty;

                int testChildQty = fullCartonQty * childQty;

                if(notFullCartonQty >= minQty)
                {
                    testChildQty += childQty;
                }

                txtTestChildQty.Text = testChildQty.ToString();

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

        private void lblItemName_Click(object sender, EventArgs e)
        {

        }

        private void lblItemCode_Click(object sender, EventArgs e)
        {

        }

        private void btnSetAllSBBItem_Click(object sender, EventArgs e)
        {
            if(SBBValidation())
            UpdateSBBItemPackaging();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbChildName_DropDown(object sender, EventArgs e)
        {

        }

        private void cmbChildName_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { cmbChildName.Select(0, 0); }));
        }

        private void lblParentQtyMAX_Click(object sender, EventArgs e)
        {
            if(lblParentQtyMAX.Text.Equals(LBL_PARENT_MAX))
            {
                ParentQtyMaxMode(false);
            }
            else
            {
                ParentQtyMaxMode(true);
            }
        }
    }
}
