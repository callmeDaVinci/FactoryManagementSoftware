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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FactoryManagementSoftware.UI
{
    public partial class frmPlanningNEWV2 : Form
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

        #endregion

        #region LastInventoryZeroDate

        public new string Name { get; set; }
       

        public static DateTime CalculateZeroInventoryDate(DataTable dt, string customerName)
        {
            var customerRows = dt.Select($"Customer = '{customerName}'")
         .OrderBy(row => row["ReceiveDate"]).ToList();

            DateTime zeroInventoryDate = (DateTime)customerRows[0]["ReceiveDate"];
            int remainingInventory = (int)customerRows[0]["Quantity"];

            for (int i = 1; i < customerRows.Count; i++)
            {
                DateTime nextReceiveDate = (DateTime)customerRows[i]["ReceiveDate"];
                int daysDifference = (nextReceiveDate - zeroInventoryDate).Days;

                if (remainingInventory - daysDifference <= 0)
                {
                    zeroInventoryDate = nextReceiveDate;
                    remainingInventory = (int)customerRows[i]["Quantity"];
                }
                else
                {
                    remainingInventory = remainingInventory - daysDifference + (int)customerRows[i]["Quantity"];
                    zeroInventoryDate = zeroInventoryDate.AddDays(daysDifference);
                }
            }

            zeroInventoryDate = zeroInventoryDate.AddDays(remainingInventory - 1);

            return zeroInventoryDate;
        }


        private void Testing()
        {
            DataTable dt = new DataTable("PurchaseData");

            dt.Columns.Add("Customer", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("ReceiveDate", typeof(DateTime));

            dt.Rows.Add("Customer1", 10, new DateTime(2023, 5, 1));
            dt.Rows.Add("Customer1", 10, new DateTime(2023, 5, 5));
            dt.Rows.Add("Customer1", 10, new DateTime(2023, 6, 1));

            dt.Rows.Add("Customer2", 10, new DateTime(2023, 5, 1));
            dt.Rows.Add("Customer2", 10, new DateTime(2023, 5, 1));
            dt.Rows.Add("Customer2", 10, new DateTime(2023, 6, 1));

            dt.Rows.Add("Customer3", 10, new DateTime(2023, 5, 1));

            dt.Rows.Add("Customer4", 10, new DateTime(2023, 5, 1));
            dt.Rows.Add("Customer4", 10, new DateTime(2023, 5, 1));
            dt.Rows.Add("Customer4", 10, new DateTime(2023, 5, 15));


            string toFollowUpDate = CalculateZeroInventoryDate(dt, "Customer1").ToString("dd/MMM/yyyy");

            string toFollowUpDate2 = CalculateZeroInventoryDate(dt, "Customer2").ToString("dd/MMM/yyyy");
            string toFollowUpDate3 = CalculateZeroInventoryDate(dt, "Customer3").ToString("dd/MMM/yyyy");
            string toFollowUpDate4 = CalculateZeroInventoryDate(dt, "Customer4").ToString("dd/MMM/yyyy");


        }
        #endregion


        public frmPlanningNEWV2()
        {
            //Testing();
            InitializeComponent();
            InitialSetting();

            LoadDB();
        }

        private void frmPlanning_Load(object sender, EventArgs e)
        {
            
        }

        private void ctbPartName_TextChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            CMB_CODE_READY = false;

            errorProvider1.Clear();
            string keywords = txtPartName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);

                foreach (DataRow row in dt.Rows)
                {
                    string itemCat = row[dalItem.ItemCat].ToString();

                    if (!itemCat.Equals(text.Cat_Part))
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
                    //cmbPartCode.SelectedIndex = 0;
                    
                }
                else
                {
                   
                }
                CMB_CODE_READY = true;

            }
            else
            {
                cmbPartCode.DataSource = null;
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void CmbPartCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void cmbPartCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMouldList(cmbPartCode.Text);
        }

        #region UI/UX

        private void InitialSetting()
        {
            BackToPartInfo();

            InitialNameTextBox();

        }

        private void NextToMachineSelection()
        {
            btnNextToMachineSelection.Visible = false;

            tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
            tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);
        }

        private void BackToPartInfo()
        {
            btnNextToMachineSelection.Visible = true;

            tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
            tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);
        }


        #endregion

        #region Data Loading

        private void LoadMouldList(string itemCode)
        {
            dgvMouldList.DataSource = null;

            if(!string.IsNullOrEmpty(itemCode) && CMB_CODE_READY)
            {
                if (DT_MOULD_ITEM == null)
                {
                    DT_MOULD_ITEM = dalItem.MouldItemSelect();
                }

                bool itemFound = false;

                //DataTable dt_MouldList = NewMouldListDataTable();

                foreach (DataRow row in DT_MOULD_ITEM.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        if (!itemFound)
                        {
                            itemFound = true;
                        }


                    }
                }

                if (itemFound)
                {
                    //dgvMouldList UI formatting

                }
                else
                {
                    CMB_CODE_READY = false;

                    //System.Threading.Thread.Sleep(1000);
                    MessageBox.Show("Item Mould List not found!\nPlease create a mould data for this item.");
                    //call item & mould configuration form
                    frmItemAndMouldConfiguration frm = new frmItemAndMouldConfiguration(itemCode,0);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                    //if data updated, DT_MOULD_ITEM = dalItem.MouldItemSelect();
                    //LoadMouldList(string itemCode)

                }
            }
           
        }

        private void InitialNameTextBox()
        {
            DataTable dt = dalItem.CatSearch(text.Cat_Part);
            dt = dt.DefaultView.ToTable(true, "item_name");
            dt.DefaultView.Sort = "item_name ASC";

            string[] stringArray = new string[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stringArray[i] = dt.Rows[i][0].ToString();
            }

            txtPartName.Values = stringArray;

        }

        private void LoadDB()
        {
            DT_ITEM = dalItem.Select();
            DT_MOULD_ITEM = dalItem.MouldItemSelect();
        }

        #endregion

        private void btnNextToMachineSelection_Click(object sender, EventArgs e)
        {
            NextToMachineSelection();
        }

        private void btnBackToPartInfo_Click(object sender, EventArgs e)
        {
            BackToPartInfo();
        }

        private void cmbPartCode_DisplayMemberChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbPartCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if(!CMB_CODE_READY)
            //{
            //    LoadMouldList(cmbPartCode.Text);
            //}
        }

        private void cmbPartCode_DataSourceChanged(object sender, EventArgs e)
        {
            //int count = cmbPartCode.Items.Count;
            //cmbPartCode.SelectedIndex = -1;
            //if (count == 1)
            //{
            //    //cmbPartCode.SelectedIndex = 0;

            //}
        }

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
    }
}
