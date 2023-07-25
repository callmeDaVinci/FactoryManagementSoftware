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

        public frmMatAddOrEdit(DataTable dt)
        {
            InitializeComponent();
            loadItemCategoryData();
            dt_matplan = dt;
        }

        public frmMatAddOrEdit(matPlanBLL u, bool addingMaterial)
        {
            InitializeComponent();

            editMode = true;
            addMode = addingMaterial;
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

            if (!addingMaterial)
            {
                cmbChildCat.Text = u.mat_cat;

                cmbChildName.Items.Add(u.mat_name);
                cmbChildName.SelectedIndex = 0;

                cmbChildCode.Items.Add(u.mat_code);
                cmbChildCode.SelectedIndex = 0;

                txtMatUseQty.Text = u.plan_to_use.ToString();

                cmbChildCat.Enabled = false;
                cmbChildName.Enabled = false;
                cmbChildCode.Enabled = false;
            }

            cmbParentName.Enabled = false;
            cmbParentCode.Enabled = false;
            cmbPlanID.Enabled = false;

            updateTestName();
        }

        public frmMatAddOrEdit(DataTable dt, string ParentCode, bool AddMaterial)
        {
            InitializeComponent();
            dt_matplan = dt;
            editMode = false;
            addMode = AddMaterial;

            loadItemCategoryData();

            cmbParentName.Text = tool.getItemName(ParentCode);
            cmbParentCode.Text = ParentCode;

            loadPlanID();
            //LoadPlanData();
            cmbPlanID.SelectedIndex = 0;
            cmbParentName.Enabled = false;
            cmbParentCode.Enabled = false;
            //cmbPlanID.Enabled = false;

            txtMatUseQty.Clear();
            updateTestName();

        }
        public frmMatAddOrEdit(DataTable dt, matPlanBLL u, string ParentCode, bool AddMaterial)
        {
            InitializeComponent();
            dt_matplan = dt;

            if(!AddMaterial)
            editMode = false;

            addMode = AddMaterial;
            
            loadItemCategoryData();

            cmbParentName.Text = tool.getItemName(ParentCode);
            cmbParentCode.Text = ParentCode;

            loadPlanID();
            LoadPlanData(u.plan_id);
            if (!AddMaterial)
            {
                editMode = true;
                cmbChildCat.Text = u.mat_cat;

                cmbChildName.Items.Add(u.mat_name);
                cmbChildName.SelectedIndex = 0;

                cmbChildCode.Items.Add(u.mat_code);
                cmbChildCode.SelectedIndex = 0;

                txtMatUseQty.Text = u.plan_to_use.ToString();

                cmbChildCat.Enabled = false;
                cmbChildName.Enabled = false;
                cmbChildCode.Enabled = false;
            }

            cmbPlanID.SelectedIndex = 0;
            cmbParentName.Enabled = false;
            cmbParentCode.Enabled = false;
            //cmbPlanID.Enabled = false;

            updateTestName();
        }

        #region variable/ object declare

        itemCatDAL dALItemCat = new itemCatDAL();

        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();
        planningDAL dalPlan = new planningDAL();
        MacDAL dalMac = new MacDAL();
        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        itemDAL dalItem = new itemDAL();

        Tool tool = new Tool();
        Text text = new Text();

        DataTable dt_matplan;
        private bool editMode = false;
        private bool addMode = false;
        private bool loaded = false;
        private bool parentNameSelected = false;
        static public bool dataSaved = false;
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
            if (!txtTestParentQty.Text.Equals("") && !txtChildQty.Text.Equals(""))
            {

                int maxQty = txtMax.Text.Equals("") || txtMax.Text.Equals("0") ? 1 : Convert.ToInt32(txtMax.Text);
                int minQty = txtMin.Text.Equals("") || txtMin.Text.Equals("0") ? 1 : Convert.ToInt32(txtMin.Text);
                int childQty = txtChildQty.Text.Equals("") || txtChildQty.Text.Equals("0") ? 1 : Convert.ToInt32(txtChildQty.Text);

                int testParentQty = Convert.ToInt32(txtTestParentQty.Text);

                int fullCartonQty = testParentQty / maxQty;

                int notFullCartonQty = testParentQty % maxQty;

                int testChildQty = fullCartonQty * childQty;

                if (notFullCartonQty >= minQty)
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

        #endregion

        #region Validation

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(cmbParentCat.Text))
            {

                errorProvider1.SetError(lblParentCat, "Item Category Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbParentName.Text))
            {

                errorProvider2.SetError(lblParentName, "Item Name Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbParentCode.Text))
            {

                errorProvider3.SetError(lblParentCode, "Item Code Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbPlanID.Text))
            {

                errorProvider4.SetError(lblPlanID, "Plan ID Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbChildCat.Text))
            {

                errorProvider5.SetError(lblChildCat, "Item Category Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbChildName.Text))
            {

                errorProvider6.SetError(lblChildName, "Item Name Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbChildCode.Text))
            {

                errorProvider7.SetError(lblChildCode, "Item code Required");
                result = false;
            }

            if (string.IsNullOrEmpty(txtMatUseQty.Text))
            {

                errorProvider8.SetError(lblMatUseQty, "Material Use Qty Required");
                result = false;
            }

            if (cbEditJoin.Checked)
            {
                if (string.IsNullOrEmpty(txtMax.Text))
                {
                    errorProvider9.SetError(lblMax, "Max Qty Required");
                    result = false;
                }

                if (string.IsNullOrEmpty(txtMin.Text))
                {
                    errorProvider10.SetError(lblMin, "Min Qty Required");
                    result = false;
                }

                if (string.IsNullOrEmpty(txtChildQty.Text))
                {
                    errorProvider11.SetError(lblChildQty, "Join Qty Required");
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
                string itemCat = cmbChildCat.Text;

                if(itemCat == text.Cat_RawMat || itemCat == text.Cat_MB || itemCat == text.Cat_Pigment)
                {
                    MessageBox.Show("Raw Material/Pigment/Master Batch cannot be join to the part.");
                    cbEditJoin.Checked = false;
                }
                else
                {
                    gbJoin.Enabled = true;
                    gbTest.Enabled = true;

                    txtMax.BackColor = SystemColors.Info;
                    txtMin.BackColor = SystemColors.Info;
                    txtChildQty.BackColor = SystemColors.Info;
                }
                
            }
            else
            {
                gbJoin.Enabled = false;
                gbTest.Enabled = false;

                txtMax.BackColor = Color.White;
                txtMin.BackColor = Color.White;
                txtChildQty.BackColor = Color.White;
            }
        }

        #endregion

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
            if(!editMode || addMode)
            {
                string keywords = cmbChildCat.Text;

                if (!string.IsNullOrEmpty(keywords))
                {
                    errorProvider5.Clear();

                    if (keywords.Equals("Carton"))
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
            parentNameSelected = false;
            string keywords = cmbParentName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
                cmbParentCode.DataSource = dt;
                cmbParentCode.DisplayMember = "item_code";
                cmbParentCode.ValueMember = "item_code";

                cmbParentCode.SelectedIndex = -1;
            }
            else
            {
                cmbParentCode.DataSource = null;
            }

            updateTestName();
            parentNameSelected = true;
        }

        private void cmbChildName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!editMode || addMode)
            {
                errorProvider6.Clear();
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

        private void loadPlanID()
        {
            if(!editMode)
            {
                bool planFound = false;
                string itemCode = cmbParentCode.Text;
                cmbPlanID.DataSource = null;
               
                foreach (DataRow rows in dt_matplan.Rows)
                {
                    string status = rows[dalPlan.planStatus].ToString();

                    if(status != text.planning_status_completed && status != text.planning_status_cancelled)
                    {
                        string partCode = rows[dalPlan.partCode].ToString();

                        if(itemCode == partCode)
                        {
                            planFound = true;
                            bool duplicate = false;
                            string planID = rows[dalPlan.jobNo].ToString();
                            foreach (object item in cmbPlanID.Items)
                            {
                                string text = (item as string);
                                //do stuff with the text

                                if(!string.IsNullOrEmpty(text))
                                {
                                    if(text == planID)
                                    {
                                        duplicate = true;
                                    }
                                }
                            }

                            if(!duplicate)
                            cmbPlanID.Items.Add(planID);
                        }
                    }
                }


                if (!planFound)
                {
                    MessageBox.Show("Plan not found!");
                }
            }
        }

        private void cmbParentCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();

            string text = cmbParentCode.GetItemText(cmbParentCode.SelectedItem);
            if (parentNameSelected && loaded && !editMode && cmbParentCode.SelectedIndex >= 0 && !string.IsNullOrEmpty(text) && text != "System.Data.DataRowView")
            {
                //load mat plan data
                loadPlanID();
            }
            updateTestName();
        }

        private void cmbChildCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider7.Clear();
            updateTestName();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                DialogResult dialogResult;
                DateTime date = DateTime.Now;
                bool readyToClose = false;

                //update mat plan data
                if (editMode && !addMode)
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
                            readyToClose = false;
                        }
                        else
                        {
                            MessageBox.Show("Data Updated!");
                            readyToClose = true;
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
                            readyToClose = false;
                        }
                        else
                        {
                            MessageBox.Show("Data inserted!");
                            readyToClose = true;
                        }
                    }


                }

                //save Join Data
                if (cbEditJoin.Checked)
                {
                    uJoin.join_parent_code = cmbParentCode.Text;

                    uJoin.join_child_code = cmbChildCode.Text;

                    frmJoin.editedParentCode = uJoin.join_parent_code;
                    frmJoin.editedChildCode = uJoin.join_child_code;

                    uJoin.join_max = Convert.ToInt32(txtMax.Text);
                    uJoin.join_min = Convert.ToInt32(txtMin.Text);
                    uJoin.join_qty = Convert.ToInt32(txtChildQty.Text);

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
                            readyToClose = true;

                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to update join");
                            readyToClose = false;
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
                            readyToClose = true;
                        }
                        else
                        {
                            //Failed to insert data
                            MessageBox.Show("Failed to add new join");
                            readyToClose = false;
                        }
                    }
                }

                if (readyToClose)
                {
                    dataSaved = true;
                    Close();
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
            errorProvider10.Clear();
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
            errorProvider9.Clear();
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


        private void LoadPlanData()
        {
            int planID = int.TryParse(cmbPlanID.Text, out int i) ? Convert.ToInt32(cmbPlanID.Text) : -1;

            foreach (DataRow row in dt_matplan.Rows)
            {
                if (planID.ToString() == row[dalPlan.jobNo].ToString())
                {
                    //get data
                    txtFac.Text = row[dalMac.MacLocationName].ToString();
                    txtMac.Text = row[dalPlan.machineID].ToString();

                    txtAblePro.Text = row[dalPlan.ableQty].ToString();
                    txtTargetQty.Text = row[dalPlan.targetQty].ToString();

                    txtStart.Text = Convert.ToDateTime(row[dalPlan.productionStartDate]).ToShortDateString();
                    txtEnd.Text = Convert.ToDateTime(row[dalPlan.productionEndDate]).ToShortDateString();

                    break;
                }
            }
        }

        private void LoadPlanData(int planID)
        {

            foreach (DataRow row in dt_matplan.Rows)
            {
                if (planID.ToString() == row[dalPlan.jobNo].ToString())
                {
                    //get data
                    txtFac.Text = row[dalMac.MacLocationName].ToString();
                    txtMac.Text = row[dalPlan.machineID].ToString();

                    txtAblePro.Text = row[dalPlan.ableQty].ToString();
                    txtTargetQty.Text = row[dalPlan.targetQty].ToString();

                    txtStart.Text = Convert.ToDateTime(row[dalPlan.productionStartDate]).ToShortDateString();
                    txtEnd.Text = Convert.ToDateTime(row[dalPlan.productionEndDate]).ToShortDateString();

                    break;
                }
            }
        }

        private void cmbPlanID_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider4.Clear();

            if (!editMode)
            {
                LoadPlanData();

            }
        }

        private void txtMatUseQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider8.Clear();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            errorProvider11.Clear();
        }

        private void txtTestParentQty_TextChanged(object sender, EventArgs e)
        {
            updateTestQty();
        }

        private void txtTestChildQty_TextChanged(object sender, EventArgs e)
        {
            updateTestQty();
        }
    }
}
