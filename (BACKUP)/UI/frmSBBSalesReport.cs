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
    public partial class frmSBBSalesReport: Form
    {
        Tool tool = new Tool();
        Text text = new Text();

        SBBDataDAL dalSBB = new SBBDataDAL();
        SBBDataBLL uSBB = new SBBDataBLL();

        readonly string text_ShowFilter = "SHOW FILTER";
        readonly string text_HideFilter = "HIDE FILTER";

        public frmSBBSalesReport()
        {
            InitializeComponent();
        }

        private void LoadSalesReport()
        {
            DateTime now = DateTime.Now;
            string start = new DateTime(now.Year, 8, 1).ToString("yyyy/MM/dd");
            string end = new DateTime(now.Year, 8, 1).AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd");

            DataTable dt_DO = dalSBB.DOWithTrfInfoSelect(start, end);


            dgvSalesReport.DataSource = dt_DO;
            //DgvUIEdit(dgvDOList);
            dgvSalesReport.ClearSelection();

        }

        private void ShowOrHideFilter()
        {
            string filterText = btnFilter.Text;

            if (filterText == text_ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilter();
        }
    }
}
