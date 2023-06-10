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
    public partial class frmItemSearch : Form
    {
        #region Variable Declare
        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        userDAL dalUser = new userDAL();
        joinDAL dalJoin = new joinDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        Tool tool = new Tool();
        Text text = new Text();
        matPlanDAL dalmatPlan = new matPlanDAL();
        PlanningBLL uPlanning = new PlanningBLL();

        habitDAL dalHabit = new habitDAL();
        habitBLL uHabit = new habitBLL();

        planningDAL dalPlan = new planningDAL();
        PlanningBLL uPlan = new PlanningBLL();

        planningActionDAL dalPlanAction = new planningActionDAL();
        itemForecastDAL dalItemForecast = new itemForecastDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();

        dataTrfBLL uData = new dataTrfBLL();

        private DataTable DT_ITEM;
        private DataTable DT_MOULD_ITEM;
        bool CMB_CODE_READY = false;
        static public string ITEM_CODE_SELECTED;
        private readonly string ITEM_TYPE;
        #endregion

        public frmItemSearch()
        {
            ITEM_CODE_SELECTED = "";
            InitializeComponent();
            InitialSetting();
        }

        public frmItemSearch(string itemType)
        {
            ITEM_TYPE = itemType;
            DT_ITEM = dalItem.CatSearch(itemType);

            ITEM_CODE_SELECTED = "";

            InitializeComponent();
            InitialSetting();
        }

        public frmItemSearch(DataTable dt, string itemType)
        {
            ITEM_TYPE = itemType;
            DT_ITEM = dt;
            ITEM_CODE_SELECTED = "";

            InitializeComponent();
            InitialSetting();
        }

        private void frmPlanning_Load(object sender, EventArgs e)
        {
            
        }
       

        private void ctbPartName_TextChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            CMB_CODE_READY = false;
            cmbPartCode.DataSource = null;

            errorProvider1.Clear();
            string keywords = txtPartName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = DT_ITEM.Copy();

                // Only call AcceptChanges once, improving performance
                dt.AcceptChanges();

                var rows = dt.AsEnumerable()
                    .Where(r => r.Field<string>(dalItem.ItemName) == keywords);

                if(rows.Any())
                {
                    // Convert filtered enumerable back to DataTable
                    dt = rows.CopyToDataTable();

                    foreach (DataRow row in dt.Rows)
                    {
                        string itemCat = row[dalItem.ItemCat].ToString();

                        if (!itemCat.Equals(ITEM_TYPE))
                        {
                            row.Delete();
                        }
                    }

                    dt.AcceptChanges();

                    cmbPartCode.DataSource = dt;
                    cmbPartCode.DisplayMember = "item_code";
                    cmbPartCode.ValueMember = "item_code";



                    int count = cmbPartCode.Items.Count;
                    cmbPartCode.SelectedIndex = -1;

                    if (count == 1)
                    {
                        cmbPartCode.SelectedIndex = 0;

                    }
                    else
                    {
                        cmbPartCode.DroppedDown = true;
                    }

                    CMB_CODE_READY = true;
                }
               

            }
         

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

     

        #region UI/UX

        private void InitialSetting()
        {
            InitialNameTextBox();
        }

        #endregion

        #region Data Loading

        private void InitialNameTextBox()
        {
            DataTable dt;

            if (DT_ITEM?.Rows.Count > 0)
            {
                dt = DT_ITEM;
            }
            else
            {
                dt = dalItem.CatSearch(text.Cat_Part);

            }

            dt = dt.DefaultView.ToTable(true, "item_name");
            dt.DefaultView.Sort = "item_name ASC";

            string[] stringArray = new string[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stringArray[i] = dt.Rows[i][0].ToString();
            }

            txtPartName.Values = stringArray;

        }


        #endregion

        private void frmPlanningNEWV2_Shown(object sender, EventArgs e)
        {
            txtPartName.Focus();
        }

        private void txtPartName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {

                //cmbPartCode.DroppedDown = true;
                //cmbPartCode.Focus();

                //CMB_CODE_READY = true;

            }
        }

        private void btnCancelPartInfoEdit_Click(object sender, EventArgs e)
        {
            ITEM_CODE_SELECTED = "";

            Close();
        }

        private void btnMouldSelected_Click(object sender, EventArgs e)
        {
            ITEM_CODE_SELECTED = cmbPartCode.Text;
            Close();
        }
    }
}
