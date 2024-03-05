using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmDONumberChange_v2 : Form
    {
        public frmDONumberChange_v2()
        {
            InitializeComponent();
        }

        public frmDONumberChange_v2(string oldDONo)
        {
            InitializeComponent();
            txtOldDONo.Text = oldDONo;
            lblAvailableResult.Visible = false;
        }

        public frmDONumberChange_v2(internalDOBLL u, bool isInternal)
        {
            InitializeComponent();

            uInternalDO = u;

            txtOldDONo.Text = uInternalDO.old_do;
            lblAvailableResult.Visible = false;
         
            IS_INTERNAL = isInternal;
        }


        private bool IS_INTERNAL = false;

        Tool tool = new Tool();
        doInternalDAL dalInternalDO = new doInternalDAL();
        internalDOBLL uInternalDO = new internalDOBLL();

        private readonly string text_Available = "Number is Available !";
        private readonly string text_Used = "Number is Used !";

        static public string NewDONumber = "";
        static public bool numberSelected = false;
        static public bool DO_NUMBER_UPDATED = false;

        private void frmDONumberChange_Load(object sender, EventArgs e)
        {

        }

        private void lblAvailableCheck_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                CheckIfNumberAvailable();
            }
           
        }

        private bool Validation()
        {
            bool passed = true;
            lblAvailableResult.Visible = false;
            if (string.IsNullOrEmpty(txtNewDONo.Text))
            {
                errorProvider1.SetError(lblNewDONo, "Please input new D/O number");
                passed = false;
            }

            return passed;
        }

        private DataTable DT_INTERNAL_DO;
        private void LoadInternalDO()
        {
            DT_INTERNAL_DO = dalInternalDO.SelectAll();
        }

        private bool ifDONoExist(string NewDoNo)
        {
            if(IS_INTERNAL)
            {
                LoadInternalDO();

                foreach (DataRow row in DT_INTERNAL_DO.Rows)
                {
                    string DOFormatTblCode = row[dalInternalDO.DOFormatTblCode].ToString();
                    string usedDONumber = row[dalInternalDO.DoNoString].ToString().Replace(" ", "").ToUpper();
                    bool isCancelled = bool.TryParse(row[dalInternalDO.IsCancelled].ToString(), out bool rowIsCancelled) ? rowIsCancelled : false;

                    if (!isCancelled && uInternalDO.do_format_tbl_code.ToString() == DOFormatTblCode)
                    {
                        bool DoNoExist = usedDONumber == NewDoNo.Replace(" ", "").ToUpper();

                        if (DoNoExist)
                        {
                            //MessageBox.Show("Duplicate DO number found. Attempting to find a unique number.", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return DoNoExist;
                            //break; // Exit the foreach loop to start the search again with the new doCode
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckIfNumberAvailable()
        {
            bool result = false;
            string NewDONo = txtNewDONo.Text;

            if(!string.IsNullOrEmpty(NewDONo))
            {
                lblAvailableResult.Visible = true;

                if (ifDONoExist(NewDONo))
                {
                    result = false;

                    lblAvailableResult.Text = text_Used;
                    lblAvailableResult.BackColor = Color.FromArgb(255, 192, 159);
                }
                else
                {
                    result = true;

                    lblAvailableResult.Text = text_Available;
                    lblAvailableResult.BackColor = Color.FromArgb(173, 241, 218);
                }
            }
            else
            {
                lblAvailableResult.Visible = false;
            }
            

            return result;
        }

        private void OnlyNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtOldDONo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private bool UpdateDONumber(String NewDONumber)
        {
            bool success = false;

            if (IS_INTERNAL)
            {
                uInternalDO.updated_by = MainDashboard.USER_ID;
                uInternalDO.updated_date = DateTime.Now;
                uInternalDO.do_no_string= NewDONumber;

                success = dalInternalDO.UpdateDoNo(uInternalDO);

                if(!success)
                {
                    MessageBox.Show("Failed to change DO number!");
                }
                else
                {
                    MessageBox.Show("DO Number Updated!");
                }
            }
            else
            {

            }

            return success;
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                if (CheckIfNumberAvailable())
                {
                    NewDONumber = txtNewDONo.Text;
                    DO_NUMBER_UPDATED =  UpdateDONumber(NewDONumber);
                    Close();
                }
            }
            else
            {
                DO_NUMBER_UPDATED = false;
            }

           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtNewDONo_TextChanged(object sender, EventArgs e)
        {
            lblAvailableResult.Visible = false;
            errorProvider1.Clear();
        }
    }
}
