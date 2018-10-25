using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmReceiveConfirm : Form
    {
        public frmReceiveConfirm()
        {
            InitializeComponent();
        }

        trfCatBLL utrfCat = new trfCatBLL();
        trfCatDAL daltrfCat = new trfCatDAL();

        private void frmReceiveConfirm_Load(object sender, EventArgs e)
        {
            //select category list from category database
            DataTable dtTrfCatFrm = daltrfCat.Select();
            //remove repeating name in trf_cat_name
            DataTable distinctTable3 = dtTrfCatFrm.DefaultView.ToTable(true, "trf_cat_name");
            //sort the data according trf_cat_name
            distinctTable3.DefaultView.Sort = "trf_cat_name ASC";
            //set combobox datasource from table
            cmbFrom.DataSource = distinctTable3;
            //show trf_cat_name data from table only
            cmbFrom.DisplayMember = "trf_cat_name";
        }
    }
}
