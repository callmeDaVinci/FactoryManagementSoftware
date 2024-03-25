using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Data;

using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmDOCompleteSetting : Form
    {
        public frmDOCompleteSetting()
        {
            InitializeComponent();
            dateEditOnly = true;
        }

        public frmDOCompleteSetting(DateTime oldDeliveredDate)
        {
            InitializeComponent();
            dateEditOnly = true;
            dtpDeliveredDate.Value = oldDeliveredDate;

        }

        public frmDOCompleteSetting(DataTable dt, bool ougtosmy, bool smytooug)
        {
            InitializeComponent();

            DT_DELIVERED_ITEM = dt;

            INTERNAL_OUG_TO_SMY = ougtosmy;
            INTERNAL_SMY_TO_OUG = smytooug;
        }

      
        public frmDOCompleteSetting(string DateType)
        {

            InitializeComponent();
            lblDeliveredDate.Text = DateType;
            dateEditOnly = true;
        }

        private DataTable DT_DELIVERED_ITEM;
        static public bool transferred = false;
        private bool dateEditOnly = false;
        private bool dateClear = false;
        Tool tool = new Tool();
        Text text = new Text();

        facDAL dalFac = new facDAL();
        trfCatDAL daltrfCat = new trfCatDAL();

        private bool INTERNAL_OUG_TO_SMY = false;
        private bool INTERNAL_SMY_TO_OUG = false;
        string DEFAULT_FAC_OUT = "";
        string DEFAULT_FAC_IN = "";

        static public DateTime selectedDate = DateTime.MaxValue;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            transferred = false;
            Close();
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(dtpDeliveredDate.Text))
            {
                errorProvider1.SetError(lblDeliveredDate, "Delivered Date Required");
                return false;
            }

            if (string.IsNullOrEmpty(cmbFrom.Text))
            {
                errorProvider2.SetError(groupBox2, "Delivered From Required");
                return false;
            }

            if (string.IsNullOrEmpty(cmbTo.Text) && !INTERNAL_SMY_TO_OUG)
            {
                errorProvider3.SetError(groupBox1, "Delivered To Required");
                return false;
            }

          
            return result;

        }

      

        private void btnFilterApply_Click(object sender, EventArgs e)
        {

            if (Validation())//check data field 
            {
                selectedDate = dtpDeliveredDate.Value;

                string catTo = cmbToCat.Text;
                string to = cmbTo.Text;
                string from = cmbFrom.Text;

                //update location from and to
                foreach(DataRow row in DT_DELIVERED_ITEM.Rows)
                {
                    row[text.Header_DeliveredDate] = selectedDate;
                    row[text.Header_From_Cat] = text.Factory;
                    row[text.Header_From] = from;
                    row[text.Header_To_Cat] = catTo;
                    row[text.Header_ShipTo] = to;
                 
                }
                frmInOutEdit frm = new frmInOutEdit(DT_DELIVERED_ITEM, 1.0);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();//Item Edit

                transferred = frmInOutEdit.TrfSuccess;

                Close();

            }

            #region old code

            //if (dateEditOnly)
            //{
            //    DialogResult dialogResult = MessageBox.Show("Confirm to set delivered date to: " + dtpDeliveredDate.Value.ToString("yyyy/MM/dd") + " ?", "Message",
            //                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        selectedDate = dtpDeliveredDate.Value;
            //    }

            //    if (dateClear)
            //    {
            //        selectedDate = DateTimePicker.MinimumDateTime;
            //    }
            //}
            //else
            //{
            //    frmInOutEdit frm = new frmInOutEdit(dt_Delivered, dtpDeliveredDate.Value.Date);
            //    frm.StartPosition = FormStartPosition.CenterScreen;
            //    frm.ShowDialog();//Item Edit

            //    transferred = frmInOutEdit.TrfSuccess;

            //}
            #endregion



        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            dtpDeliveredDate.Value = DateTimePicker.MinimumDateTime;
            dateClear = true;
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDeliveredDate.Value == DateTimePicker.MinimumDateTime)
            {
                dtpDeliveredDate.Value = DateTime.Now; // This is required in order to show current month/year when user reopens the date popup.
                dtpDeliveredDate.Format = DateTimePickerFormat.Custom;
                dtpDeliveredDate.CustomFormat = " ";
            }
            else
            {
                dtpDeliveredDate.Format = DateTimePickerFormat.Custom;
                dtpDeliveredDate.CustomFormat = "ddMMMMyy";
                dateClear = false;
            }
        }

        private bool FAC_FROM_DATA_LOADED = true;
        private bool FAC_TO_CAT_DATA_LOADED = true;

        private DataTable DT_FAC;

        private void InitialCombobox(ComboBox cmb, DataTable dt, string displayColumn, string valueMember, string selectedText)
        {
            cmb.DataSource = dt;
            cmb.DisplayMember = displayColumn;
            cmb.ValueMember = valueMember;

            if(string.IsNullOrEmpty(selectedText))
            {
                cmb.SelectedIndex = -1;
            }
            else
            {
                cmb.Text = selectedText;
            }
        }

       
        private void frmDeliveryDate_Load(object sender, EventArgs e)
        {
           

            if(INTERNAL_SMY_TO_OUG)
            {
                DEFAULT_FAC_OUT = "Semenyih";
                DEFAULT_FAC_IN = "OUG";
            }
            else if (INTERNAL_OUG_TO_SMY)
            {
                DEFAULT_FAC_OUT = "Store";
                DEFAULT_FAC_IN = "Semenyih";
            }

            DataTable dt = dalFac.NewSelectDESC();

            FAC_FROM_DATA_LOADED = false;
            InitialCombobox(cmbFrom, dt.Copy(), dalFac.FacName, dalFac.FacID, DEFAULT_FAC_OUT);
            FAC_FROM_DATA_LOADED = true;

            FAC_TO_CAT_DATA_LOADED = false;

            DataTable dtlocationCat = daltrfCat.Select();
            DataTable fromCatTable = dtlocationCat.DefaultView.ToTable(true, "trf_cat_name");
            //fromCatTable.DefaultView.Sort = "trf_cat_name ASC";
            cmbToCat.DataSource = fromCatTable;
            cmbToCat.DisplayMember = "trf_cat_name";

            if(INTERNAL_SMY_TO_OUG)
            {
                cmbToCat.Text = "OUG";
            }
            else
            {
                InitialCombobox(cmbTo, dt, dalFac.FacName, dalFac.FacID, DEFAULT_FAC_IN);
            }
          
            FAC_TO_CAT_DATA_LOADED = true;


        }

        private void cmbToCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(FAC_TO_CAT_DATA_LOADED)
            {
                cmbTo.DataSource = null;

                string toCat = cmbToCat.Text;
                if (toCat.Equals("Factory"))
                {
                    DataTable dt = dalFac.NewSelectDESC();
                    InitialCombobox(cmbTo, dt, dalFac.FacName, dalFac.FacID, DEFAULT_FAC_IN);

                }
                else if (toCat.Equals("Customer"))
                {
                    //if (cmbTrfItemCode.Text.Length > 3 && cmbTrfItemCode.Text.Substring(0, 3) == text.Inspection_Pass)
                    //{
                    //    DataTable dt = dalData.CustomerWithoutRemovedDataSelect();
                    //    loadLocationData(dt, cmbTrfTo, dalData.ShortName);
                    //}
                    //else
                    //{
                    //    DataTable dt = dalCust.CustSelectAll();
                    //    loadLocationData(dt, cmbTrfTo, "cust_name");

                    //    cmbTrfTo.Text = tool.getCustomerName(cmbTrfItemCode.Text);
                    //}


                }
               
            }
        }
    }
}
