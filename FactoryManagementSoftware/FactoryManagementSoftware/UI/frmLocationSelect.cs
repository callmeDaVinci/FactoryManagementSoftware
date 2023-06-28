using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.ReportingServices.Interfaces;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Syncfusion.XlsIO.Parser.Biff_Records.PivotTable.PivotViewItemRecord;

namespace FactoryManagementSoftware.UI
{
    public partial class frmLocationSelect : Form
    {
        #region Variable Declare
        //itemDAL dalItem = new itemDAL();
        //itemCustDAL dalItemCust = new itemCustDAL();
        //userDAL dalUser = new userDAL();
        //joinDAL dalJoin = new joinDAL();
        //trfHistDAL dalTrfHist = new trfHistDAL();
        //Tool tool = new Tool();
        //Text text = new Text();
        //matPlanDAL dalmatPlan = new matPlanDAL();
        //PlanningBLL uPlanning = new PlanningBLL();

        //habitDAL dalHabit = new habitDAL();
        //habitBLL uHabit = new habitBLL();

        //planningDAL dalPlan = new planningDAL();
        //PlanningBLL uPlan = new PlanningBLL();

        //planningActionDAL dalPlanAction = new planningActionDAL();
        //itemForecastDAL dalItemForecast = new itemForecastDAL();
        //pmmaDateDAL dalPmmaDate = new pmmaDateDAL();

        //dataTrfBLL uData = new dataTrfBLL();

        //private DataTable DT_ITEM;
        //private DataTable DT_MOULD_ITEM;
        //static public string ITEM_CODE_SELECTED;
        //private readonly string ITEM_TYPE;

        static public bool OUG_SELECTED = true;
        static public bool SEMENYIH_SELECTED = false;

        #endregion

        public frmLocationSelect()
        {
            InitializeComponent();
        }

        #region UI/UX

        #endregion

        private void btnCancelPartInfoEdit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLocationSelected_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool checkBoxProcessing = false;

        private void cbOUG_CheckedChanged(object sender, EventArgs e)
        {
            if(!checkBoxProcessing)
            {
                checkBoxProcessing = true;

                if (!cbOUG.Checked && !cbSemenyih.Checked)
                {
                    MessageBox.Show("Need to select at least one location.");
                    cbOUG.Checked = true;
                }

                OUG_SELECTED = cbOUG.Checked;

                checkBoxProcessing = false;

            }
        }

        private void cbSemenyih_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxProcessing)
            {
                checkBoxProcessing = true;

                if (!cbOUG.Checked && !cbSemenyih.Checked)
                {
                    MessageBox.Show("Need to select at least one location.");
                    cbSemenyih.Checked = true;
                }

                SEMENYIH_SELECTED = cbSemenyih.Checked;

                checkBoxProcessing = false;

            }
        }
    }
}
