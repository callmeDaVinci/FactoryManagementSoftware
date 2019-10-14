using System;
using System.Data;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System.Windows.Forms;
using System.Drawing;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMatAddOrEdit : Form
    {
        public frmMatAddOrEdit()
        {
            InitializeComponent();
            loadItemCategoryData();
        }

        public frmMatAddOrEdit(matPlanBLL u)
        {
            InitializeComponent();

            editMode = true;

            uMatPlan = u;
            loadItemCategoryData();

            cmbParentName.Text = u.part_name;
            cmbParentCode.Text = u.part_code;
            cmbPlanID.Items.Add(u.plan_id.ToString());
            cmbPlanID.SelectedIndex = 0;
           
            txtFac.Text = u.pro_location;
            txtMac.Text = u.pro_machine;

            txtAblePro.Text = u.pro_max_qty;
            txtTargetQty.Text = u.pro_target_qty;

            txtStart.Text = Convert.ToDateTime(u.pro_start).ToShortDateString();
            txtEnd.Text = Convert.ToDateTime(u.pro_end).ToShortDateString();

            cmbChildCat.Text = u.mat_cat;

            cmbChildName.Items.Add(u.mat_name);
            cmbChildName.SelectedIndex = 0;

            cmbChildCode.Items.Add(u.mat_code);
            cmbChildCode.SelectedIndex = 0;

            txtMatUseQty.Text = u.plan_to_use.ToString();

            updateTestName();

            cmbParentName.Enabled = false;
            cmbParentCode.Enabled = false;
            cmbPlanID.Enabled = false;

            cmbChildCat.Enabled = false;
            cmbChildName.Enabled = false;
            cmbChildCode.Enabled = false;

        }

        #region variable/ object declare

        itemCatDAL dALItemCat = new itemCatDAL();

        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();
        planningDAL dalPlan = new planningDAL();

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        itemDAL dalItem = new itemDAL();

        Tool tool = new Tool();
        Text text = new Text();

        private bool editMode = false;
        private bool loaded = false;
        #endregion

        #region Load Data

        private void loadItemCategoryData()
        {
            DataTable childTable = new DataTable();
            childTable.Columns.Add("CATEGORY");
            childTable.Rows.Add("Carton");
            childTable.Rows.Add("Part");
            childTable.Rows.Add("Poly Bag");
            childTable.Rows.Add("Sub Material");
            childTable.Rows.Add("Raw Material");
            childTable.Rows.Add("Master Batch");
            childTable.Rows.Add("Pigment");

            tool.loadItemCategoryDataToComboBox(cmbChildCat);
            //cmbChildCat.DataSource = childTable;

            //cmbChildCat.DisplayMember = "CATEGORY";

            cmbChildCat.SelectedIndex = -1;

            cmbParentCat.SelectedIndex = 0;
        }

        private void updateTestQty()
        {
            if (!txtTestParentQty.Text.Equals("") && !txtQty.Text.Equals(""))
            {
                if (!cmbChildCat.Text.Equals("Carton"))
                {
                    float joinQty = Convert.ToSingle(txtQty.Text);
                    int testParentQty = Convert.ToInt32(txtTestParentQty.Text);
                    txtTestChildQty.Text = (joinQty * testParentQty).ToString();
                }
                else
                {
                    int maxQty = txtMax.Text.Equals("") || txtMax.Text.Equals("0") ? 1 : Convert.ToInt16(txtMax.Text);
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

        #endregion

        #region Validation

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

            if (string.IsNullOrEmpty(cmbPlanID.Text))
            {

                errorProvider6.SetError(cmbChildCode, "Item code Required");
                result = false;
            }

            if (string.IsNullOrEmpty(txtMatUseQty.Text))
            {

                errorProvider6.SetError(cmbChildCode, "Item code Required");
                result = false;
            }

            if (cbEditJoin.Checked)
            {
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
            DataTable dt = dalJoin.existCheck(parentCode, childCode);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region UI Action

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(cbEditJoin.Checked)
            {
                gbJoin.Enabled = true;
                gbTest.Enabled = true;

                txtMax.BackColor = SystemColors.Info;
                txtMin.BackColor = SystemColors.Info;
                txtQty.BackColor = SystemColors.Info;
            }
            else
            {
                gbJoin.Enabled = false;
                gbTest.Enabled = false;

                txtMax.BackColor = Color.White;
                txtMin.BackColor = Color.White;
                txtQty.BackColor = Color.White;
            }
        }

        #endregion

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
            if(!editMode)
            {
                string keywords = cmbChildCat.Text;

                if (!string.IsNullOrEmpty(keywords))
                {
                    errorProvider4.Clear();

                    if (keywords.Equals("Carton"))
                    {
                        txtMax.Enabled = true;
                        txtMin.Enabled = true;
                        txtQty.Enabled = true;
                    }
                    else
                    {
                        txtMax.Enabled = true;
                        txtMin.Enabled = true;
                        txtQty.Enabled = true;
                    }

                    DataTable dt = dalItem.catSearch(keywords);

                    if (dt.Rows.Count > 0)
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

        private void cmbChildName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!editMode)
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
            
        }

        private void cmbParentCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();

            if(loaded && !editMode)
            {
                //load mat plan data
            }
            updateTestName();
        }

        private void cmbChildCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider6.Clear();
            updateTestName();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            DateTime date = DateTime.Now;
            //update mat plan data

            if (Validation())
            {
                if (editMode)
                {
                    dialogResult = MessageBox.Show("Are you sure want to update data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        //update mat plan to use qty
                        uMatPlan.mat_code = cmbChildCode.Text;
                        uMatPlan.plan_id = Convert.ToInt32(cmbPlanID.Text);
                        uMatPlan.plan_to_use = Convert.ToSingle(txtMatUseQty.Text);
                        uMatPlan.updated_date = date;
                        uMatPlan.updated_by = MainDashboard.USER_ID;

                        if (!dalMatPlan.MatPlanToUseUpdate(uMatPlan))
                        {
                            MessageBox.Show("Failed to update material plan to use data.");
                        }
                        else
                        {
                            MessageBox.Show("Data Updated!");
                            Close();
                        }
                    }

                }
                else
                {
                    dialogResult = MessageBox.Show("Are you sure want to add new data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        uMatPlan.mat_code = cmbChildCode.Text;
                        uMatPlan.plan_id = Convert.ToInt32(cmbPlanID.Text);
                        uMatPlan.plan_to_use = Convert.ToInt32(txtMatUseQty.Text);
                        uMatPlan.mat_used = 0;
                        uMatPlan.active = true;
                        uMatPlan.mat_note = "";
                        uMatPlan.updated_date = date;
                        uMatPlan.updated_by = MainDashboard.USER_ID;

                        //tool.matPlanAddQty(dt, matCode, planToUse);

                        if (!dalMatPlan.Insert(uMatPlan))
                        {
                            //MessageBox.Show("Failed to insert material plan data.");
                            tool.historyRecord(text.System, "Failed to insert material plan data.", date, MainDashboard.USER_ID);
                        }
                        else
                        {
                            MessageBox.Show("Data inserted!");
                            Close();
                        }
                    }

                        
                }

            }

            //save Join Data
            if (Validation() && cbEditJoin.Checked)
            {
                dialogResult = MessageBox.Show("Are you sure want to insert data to database?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        uJoin.join_qty = Convert.ToInt32(txtQty.Text);
                    }
                    else
                    {
                        uJoin.join_max = Convert.ToInt32(txtMax.Text);
                        uJoin.join_min = Convert.ToInt32(txtMin.Text);
                        uJoin.join_qty = Convert.ToSingle(txtQty.Text);
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

                    if (success)
                    {
                        Close();
                    }
                }
            }
        }

        private void txtMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtMin_TextChanged(object sender, EventArgs e)
        {
            updateTestQty();
            errorProvider8.Clear();
        }

        private void txtMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtMax_TextChanged(object sender, EventArgs e)
        {
            updateTestQty();
            errorProvider7.Clear();
        }

        private void txtFac_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtMac_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtAblePro_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtTargetQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (Keys)e.KeyChar != Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void frmMatAddOrEdit_Load(object sender, EventArgs e)
        {
            loaded = true;
        }
    }
}
