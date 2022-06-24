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
    public partial class frmItemMasterList : Form
    {
        Tool tool = new Tool();
        Text text = new Text();

        readonly string text_ShowFilter = "SHOW FILTER";
        readonly string text_HideFilter = "HIDE FILTER";

        public frmItemMasterList()
        {
            InitializeComponent();
        }

        private void frmItemMasterList_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.NewItemListFormOpen = false;
        }

        private void frmItemMasterList_Load(object sender, EventArgs e)
        {
            ShowFilter(false);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowFilter();
        }

        private void ShowFilter()
        {
            string filterText = btnFilter.Text;

            if (filterText == text_ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpPage.RowStyles[1] = new RowStyle(SizeType.Absolute, 150f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpPage.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void ShowFilter(bool ShowFilter)
        {

            if (ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpPage.RowStyles[1] = new RowStyle(SizeType.Absolute, 150f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpPage.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void ShowSubListButton(bool toShow)
        {
            if (toShow)
            {
                tlpSubList.RowStyles[1] = new RowStyle(SizeType.Absolute, 50f);
            }
            else
            {
                tlpSubList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }
    }
}
