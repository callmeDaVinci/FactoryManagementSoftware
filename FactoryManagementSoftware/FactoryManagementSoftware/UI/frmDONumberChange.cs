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
    public partial class frmDONumberChange : Form
    {
        public frmDONumberChange()
        {
            InitializeComponent();
        }

        public frmDONumberChange(string oldDONo)
        {
            InitializeComponent();
            txtOldDONo.Text = oldDONo;
            lblAvailableResult.Visible = false;
        }

        Tool tool = new Tool();
      

        private readonly string text_Available = "Number is Available !";
        private readonly string text_Used = "Number is Used !";

        static public string NewDONumber = "";
        static public bool numberSelected = false;

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

        private bool CheckIfNumberAvailable()
        {
            bool result = false;
            string NewDONo = txtNewDONo.Text;

            if(!string.IsNullOrEmpty(NewDONo))
            {
                lblAvailableResult.Visible = true;
                if (tool.IfDONoExist(NewDONo))
                {
                    result = false;
                }
                else
                {
                    result = true;
                }

                if (result)
                {
                    lblAvailableResult.Text = text_Available;
                    lblAvailableResult.ForeColor = Color.Green;
                }
                else
                {
                    lblAvailableResult.Text = text_Used;
                    lblAvailableResult.ForeColor = Color.Red;
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                if (CheckIfNumberAvailable())
                {
                    NewDONumber = txtNewDONo.Text;
                    numberSelected = true;
                    Close();
                }
            }
            else
            {
                numberSelected = false;
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
