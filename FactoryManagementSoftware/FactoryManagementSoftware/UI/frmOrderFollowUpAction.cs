using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderFollowUpAction : Form
    {
        private int orderID;

        userDAL dalUser = new userDAL();
        orderActionDAL dalOrderAction = new orderActionDAL();

        public frmOrderFollowUpAction()
        {
            InitializeComponent();
        }

        public frmOrderFollowUpAction(int ID)
        {
            InitializeComponent();

            orderID = ID;
            txtFromWho.Text = dalUser.getUsername(MainDashboard.USER_ID);
        }

        private bool validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txtFromWho.Text))
            {
                result = false;
                errorProvider1.SetError(txtFromWho, "From Who Required");
            }

            if (string.IsNullOrEmpty(txtToWho.Text))
            {
                result = false;
                errorProvider2.SetError(txtToWho, "To Who Required");
            }

            if (string.IsNullOrEmpty(txtFeedback.Text))
            {
                result = false;
                errorProvider3.SetError(txtFeedback, "Feedback Required");
            }

            return result;
        }

        private void saveActionNote()
        {
            string actionDate = dtpActionDate.Value.ToShortDateString();
            string fromWho = txtFromWho.Text;
            string toWho = txtToWho.Text;
            string feedback = txtFeedback.Text;

            dalOrderAction.orderFollowUp(orderID, actionDate, feedback, fromWho, toWho);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel this action?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to add this action record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes && validation())
            {
                saveActionNote();
                Close();
            }
        }
    }
}
