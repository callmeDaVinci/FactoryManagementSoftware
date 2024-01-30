using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO.Ports;
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
using Syncfusion.XlsIO.Parser.Biff_Records;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FactoryManagementSoftware.UI
{
    public partial class frmPlanningNEWV2 : Form
    {
        #region Variable Declare
        itemDAL dalItem = new itemDAL();
        itemBLL uItem = new itemBLL();
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
        bool PRO_INFO_LOADING = false;

        readonly private string BTN_INFO_SAVE_CONFIRM_MOULD = "INFO SAVE & MOULD CONFIRM";
        readonly private string BTN_CONFIRM_MOULD = "MOULD CONFIRM";
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
        private DataTable NewMouldListTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_MouldSelection, typeof(bool));
            dt.Columns.Add(text.Header_MouldCode, typeof(string));
            dt.Columns.Add(text.Header_ProTon, typeof(string));
            dt.Columns.Add(text.Header_ItemSelection, typeof(bool));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_Cavity, typeof(string));
            dt.Columns.Add(text.Header_ProCT, typeof(string));
            dt.Columns.Add(text.Header_ProPwShot, typeof(string));
            dt.Columns.Add(text.Header_ProRwShot, typeof(string));

            return dt;
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
            ClearProductionInfo();
            LoadMouldList(cmbPartCode.Text);
        }

        #region UI/UX

        private void InitialSetting()
        {
            BackToPartInfo();

            InitialNameTextBox();

        }

        private void btnSaveProductionInfoAndMouldConfirmMode(bool active)
        {
            btnMouldSelected.Visible = true;

            if(active)
            {
                tlpMouldConfirmButton.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
                tlpMouldConfirmButton.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);
                btnMouldSelected.Text = BTN_INFO_SAVE_CONFIRM_MOULD;
            }
            else
            {
                tlpMouldConfirmButton.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMouldConfirmButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 150f);
                btnMouldSelected.Text = BTN_CONFIRM_MOULD;

            }

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

        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

            
            if (dgv == dgvMouldList)
            {
                dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;


                dgv.Columns[text.Header_Cavity].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProTon].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProCT].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProPwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                int ProductionInfoColumnWidth = 60;

                dgv.Columns[text.Header_MouldSelection].Width = ProductionInfoColumnWidth;
                dgv.Columns[text.Header_ItemSelection].Width = ProductionInfoColumnWidth;
                dgv.Columns[text.Header_MouldCode].Width = ProductionInfoColumnWidth;
                dgv.Columns[text.Header_ProTon].Width = ProductionInfoColumnWidth;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;

                dgv.Columns[text.Header_Cavity].Visible = false;
                //dgv.Columns[text.Header_ProTon].Visible = false;
                dgv.Columns[text.Header_ProCT].Visible = false;
                dgv.Columns[text.Header_ProPwShot].Visible = false;
                dgv.Columns[text.Header_ProRwShot].Visible = false;


                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


        }

        #endregion

        #region Data Loading

        private void LoadMouldList(string itemCode)
        {
            dgvMouldList.DataSource = null;

            if (!string.IsNullOrEmpty(itemCode) && CMB_CODE_READY)
            {
                if (DT_MOULD_ITEM == null)
                {
                    DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
                }

                DataTable dt_MouldList = NewMouldListTable();
                int mouldCount = 0;
                int itemCount = 0;

                Dictionary<string, List<DataRow>> itemsByMouldCode = new Dictionary<string, List<DataRow>>();

                foreach (DataRow row in DT_MOULD_ITEM.Rows)
                {
                    string mouldCode = row[dalItem.MouldCode].ToString();
                    string DB_ItemCode = row[dalItem.ItemCode].ToString();

                    if(true)//DB_ItemCode == itemCode
                    {
                        if (!itemsByMouldCode.ContainsKey(mouldCode))
                        {
                            itemsByMouldCode[mouldCode] = new List<DataRow>();
                        }
                        itemsByMouldCode[mouldCode].Add(row);
                    }
                }

                foreach (DataRow row in DT_MOULD_ITEM.Rows)
                {

                    if (row[dalItem.ItemCode].ToString().Equals(itemCode) && row[dalItem.CombinationCode].ToString().Equals("0"))
                    {
                        string mouldCode = row[dalItem.MouldCode].ToString();
                        mouldCount++;
                        itemCount = 0;

                        if(mouldCount > 1)
                        {
                            dt_MouldList.Rows.Add(dt_MouldList.NewRow());
                        }

                        DataRow newRow = CreateNewRow(dt_MouldList, row, itemCode, txtPartName.Text);
                        dt_MouldList.Rows.Add(newRow);
                        itemCount++;

                        //load other item under same mould
                        if (itemsByMouldCode.ContainsKey(mouldCode))
                        {
                            foreach (DataRow row2 in itemsByMouldCode[mouldCode])
                            {
                                if (row2[dalItem.ItemCode].ToString() != itemCode && row2[dalItem.CombinationCode].ToString().Equals("0") && row2[dalItem.MouldCode].ToString().Equals(mouldCode))
                                {
                                    DataRow newRow2 = CreateNewRow(dt_MouldList, row2, row2[dalItem.ItemCode].ToString(), tool.getItemNameFromDataTable(DT_ITEM, row2[dalItem.ItemCode].ToString()));
                                    dt_MouldList.Rows.Add(newRow2);
                                    itemCount++;

                                }
                            }

                            // Add master mould row with all item in item description
                            foreach (DataRow rowItem in DT_ITEM.Rows)
                            {
                                if (mouldCode == rowItem[dalItem.ItemCode].ToString())
                                {
                                    string AllItemItemDesciption = CreateItemDescription(itemCount);
                                    DataRow masterRow = CreateMasterRow(dt_MouldList, rowItem, mouldCode, txtPartName.Text, AllItemItemDesciption);
                                    dt_MouldList.Rows.Add(masterRow);
                                    break;
                                }
                            }
                        }
                    }
                }

                HandleMouldList(dt_MouldList, itemCode, mouldCount);
            }
        }





        private DataRow CreateNewRow(DataTable dt_MouldList, DataRow row, string itemCode, string itemName)
        {
            DataRow newRow = dt_MouldList.NewRow();

            string itemDescription = itemName + "\n(" + itemCode + ")";

            newRow[text.Header_MouldCode] = row[dalItem.MouldCode].ToString();
            newRow[text.Header_ItemDescription] = itemDescription;
            newRow[text.Header_ItemCode] = itemCode;
            newRow[text.Header_ItemName] = itemName;
            newRow[text.Header_Cavity] = row[dalItem.MouldCavity].ToString();
            newRow[text.Header_ProCT] = row[dalItem.MouldCT].ToString();
            newRow[text.Header_ProPwShot] = row[dalItem.ItemPWShot].ToString();
            newRow[text.Header_ProRwShot] = row[dalItem.ItemRWShot].ToString();

            return newRow;
        }

        private DataRow CreateMasterRow(DataTable dt_MouldList, DataRow rowItem, string mouldCode, string itemName, string AllItemItemDescription)
        {
            DataRow newRow = dt_MouldList.NewRow();

            newRow[text.Header_MouldCode] = mouldCode;
            newRow[text.Header_ItemCode] = mouldCode;
            newRow[text.Header_ItemName] = itemName;
            newRow[text.Header_ProTon] = rowItem[dalItem.ItemProTon].ToString();
            newRow[text.Header_ItemDescription] = AllItemItemDescription;
            newRow[text.Header_Cavity] = rowItem[dalItem.ItemCavity].ToString();
            newRow[text.Header_ProCT] = rowItem[dalItem.ItemProCTTo].ToString();
            newRow[text.Header_ProPwShot] = rowItem[dalItem.ItemProPWShot].ToString();
            newRow[text.Header_ProRwShot] = rowItem[dalItem.ItemProRWShot].ToString();

            return newRow;
        }

        private string CreateItemDescription(int itemCount)
        {
            string AllItemItemDescription = text.All_Item;

            if (itemCount > 1)
            {
                AllItemItemDescription += "S (" + itemCount + ")";
            }
            else
            {
                AllItemItemDescription += " (" + itemCount + ")";
            }

            return AllItemItemDescription;
        }

        private void HandleMouldList(DataTable dt_MouldList, string itemCode, int mouldCount)
        {
            if (dt_MouldList.Rows.Count <= 0) // check if datatable have data
            {
                CMB_CODE_READY = false;

                MessageBox.Show("Item Mould List not found!\nPlease create a mould data for this item.");
                frmItemAndMouldConfiguration frm = new frmItemAndMouldConfiguration(itemCode, 0);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
                DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
                LoadMouldList(itemCode);

                CMB_CODE_READY = true;
            }
            else
            {
                dgvMouldList.DataSource = dt_MouldList;
                dgvUIEdit(dgvMouldList);
                ListCellFormatting(dgvMouldList);
                dgvMouldList.ClearSelection();

                lblMouldQtyFound.Text = mouldCount > 1
                                        ? $"({mouldCount} Moulds Found)"
                                        : $"({mouldCount} Mould Found)";
            }
        }


        private void LoadProductionInfo(string mouldCode)
        {
            DataTable dt_MouldList = dgvMouldList.DataSource as DataTable;

            List<Tuple<string, string>> selectedItems = new List<Tuple<string, string>>();

            foreach (DataRow row in dt_MouldList.Rows)
            {
                bool itemSelected = bool.TryParse(row[text.Header_ItemSelection].ToString(), out bool parsedSelection) ? parsedSelection : false;
                uItem.mould_code = row[text.Header_MouldCode].ToString();

                if (itemSelected)
                {
                    string itemCode = row.Field<string>(text.Header_ItemCode);
                    string itemDescription = row[text.Header_ItemDescription].ToString();

                    if (!itemDescription.Contains(text.All_Item))
                        selectedItems.Add(new Tuple<string, string>(mouldCode, itemCode));
                }
            }

            if (selectedItems.Select(x => x.Item1).Distinct().Count() > 1 && selectedItems.Count > 1)
            {
                throw new Exception("All selected items must have the same mould code");
            }

            if (selectedItems.Count == 1)
            {
                DataRow[] existingRows = DT_MOULD_ITEM.Select($"mould_code = '{selectedItems[0].Item1}' AND item_code = '{selectedItems[0].Item2}' AND combination_code = 0");

                if (existingRows.Length > 0)
                {
                    LoadProductionInfoToField(existingRows);
                }

                btnSaveProductionInfoAndMouldConfirmMode(false);

            }
            else if (selectedItems.Count > 1)
            {
                bool foundCombination = false;

                foreach (var combination in DT_MOULD_ITEM.AsEnumerable()
    .Where(r => r.Field<string>("mould_code") == uItem.mould_code && r.Field<int>("combination_code") != 0)
    .GroupBy(r => r.Field<int>("combination_code")))
                {
                    // Debug output to see the groupings
                    //Console.WriteLine($"Combination Code: {combination.Key}, Count: {combination.Count()}");

                    // Get the item codes in the current combination.
                    var itemsInCombination = combination.Select(r => r.Field<string>("item_code")).ToArray();
                    var selectedItemCodes = selectedItems.Select(i => i.Item2).ToArray();

                    // Debug output to see the item codes
                    //Console.WriteLine($"Selected Item Codes: {string.Join(", ", selectedItemCodes)}");
                    //Console.WriteLine($"Item Codes in Combination: {string.Join(", ", itemsInCombination)}");

                    // If the selected items and the items in the current combination are the same...
                    if (!selectedItemCodes.Except(itemsInCombination).Any() && !itemsInCombination.Except(selectedItemCodes).Any())
                    {
                        LoadProductionInfoToField(combination.ToArray());
                        foundCombination = true;
                        break;
                    }
                }

                if (!foundCombination)
                {
                    ClearProductionInfo();
                    MessageBox.Show("Production info for this combination of items could not be found.\nPlease fill in the production info and save.");
                }
                else
                {
                    btnSaveProductionInfoAndMouldConfirmMode(false);
                }
            }
            else
            {
                ClearProductionInfo();

            }
        }


        private void LoadProductionInfoToField(DataRow[] existingRows)
        {
            txtCavity.Text = existingRows[0][dalItem.MouldCavity].ToString();
            txtCycleTime.Text = existingRows[0][dalItem.MouldCT].ToString();
            txtPWPerShot.Text = existingRows[0][dalItem.ItemPWShot].ToString();
            txtRWPerShot.Text = existingRows[0][dalItem.ItemRWShot].ToString();

            btnMouldSelected.Visible = true;

        }

        private void ClearProductionInfo()
        {
            txtCavity.Text = "";
            txtCycleTime.Text = "";
            txtPWPerShot.Text = "";
            txtRWPerShot.Text = "";

            btnMouldSelected.Visible = false;
        }






        #region OLD Code
        //private void LoadMouldList(string itemCode)
        //{
        //    dgvMouldList.DataSource = null;

        //    if(!string.IsNullOrEmpty(itemCode) && CMB_CODE_READY)
        //    {
        //        if (DT_MOULD_ITEM == null)
        //        {
        //            DT_MOULD_ITEM = dalItem.MouldItemSelect();
        //        }

        //        bool itemFound = false;

        //        DataTable dt_MouldList = NewMouldListTable();

        //        int mouldCount = 0;

        //        foreach (DataRow row in DT_MOULD_ITEM.Rows)
        //        {
        //            if (row[dalItem.ItemCode].ToString().Equals(itemCode))
        //            {
        //                if (!itemFound)
        //                {
        //                    itemFound = true;
        //                }

        //                string mouldCode = row[dalItem.MouldCode].ToString();
        //                mouldCount++;


        //                DataRow newRow = dt_MouldList.NewRow();

        //                string itemName = txtPartName.Text;
        //                string itemDesription = itemName + " (" + itemCode + ")";

        //                newRow[text.Header_MouldCode] = mouldCode;
        //                //newRow[text.Header_ProTon] = row[dalItem.ItemProTon].ToString();//item_best_ton
        //                newRow[text.Header_ItemDescription] = itemDesription;
        //                newRow[text.Header_ItemCode] = itemCode;
        //                newRow[text.Header_ItemName] = itemName;
        //                newRow[text.Header_Cavity] = row[dalItem.MouldCavity].ToString();
        //                newRow[text.Header_ProCT] = row[dalItem.MouldCT].ToString();
        //                newRow[text.Header_ProPwShot] = row[dalItem.ItemPWShot].ToString();
        //                newRow[text.Header_ProRwShot] = row[dalItem.ItemRWShot].ToString();

        //                dt_MouldList.Rows.Add(newRow);

        //                //load other item under same mould
        //                int itemCount = 1;
        //                foreach(DataRow row2 in DT_MOULD_ITEM.Rows)
        //                {
        //                    if (row2[dalItem.ItemCode].ToString() != itemCode && row2[dalItem.MouldCode].ToString() == mouldCode)
        //                    {
        //                        itemCount++;

        //                        newRow = dt_MouldList.NewRow();
        //                        string otherItemCode = row2[dalItem.ItemCode].ToString();

        //                        itemName = tool.getItemNameFromDataTable(DT_ITEM, otherItemCode);
        //                        itemDesription = itemName + " (" + otherItemCode + ")";

        //                        newRow[text.Header_MouldCode] = mouldCode;
        //                        //newRow[text.Header_ProTon] = row[dalItem.ItemProTon].ToString();//item_best_ton
        //                        newRow[text.Header_ItemDescription] = itemDesription;
        //                        newRow[text.Header_ItemCode] = otherItemCode;
        //                        newRow[text.Header_ItemName] = itemName;
        //                        newRow[text.Header_Cavity] = row2[dalItem.MouldCavity].ToString();
        //                        newRow[text.Header_ProCT] = row2[dalItem.MouldCT].ToString();
        //                        newRow[text.Header_ProPwShot] = row2[dalItem.ItemPWShot].ToString();
        //                        newRow[text.Header_ProRwShot] = row2[dalItem.ItemRWShot].ToString();

        //                        dt_MouldList.Rows.Add(newRow);
        //                    }
        //                }

        //                //add master mould row with all item in item description
        //                foreach (DataRow rowItem in DT_ITEM.Rows)
        //                {
        //                    if (mouldCode == rowItem[dalItem.ItemCode].ToString())
        //                    {
        //                        //add mould all item production mode info

        //                        string AllItemItemDesciption = text.All_Item;

        //                        if (itemCount > 1)
        //                        {
        //                            AllItemItemDesciption += "S (" + itemCount + ")";
        //                        }
        //                        else
        //                        {
        //                            AllItemItemDesciption += " (" + itemCount + ")";

        //                        }

        //                        newRow = dt_MouldList.NewRow();

        //                        newRow[text.Header_MouldCode] = mouldCode;
        //                        newRow[text.Header_ItemCode] = mouldCode;
        //                        newRow[text.Header_ItemName] = itemDesription;
        //                        newRow[text.Header_ProTon] = rowItem[dalItem.ItemProTon].ToString();
        //                        newRow[text.Header_ItemDescription] = AllItemItemDesciption;
        //                        newRow[text.Header_Cavity] = rowItem[dalItem.ItemCavity].ToString();
        //                        newRow[text.Header_ProCT] = rowItem[dalItem.ItemProCTTo].ToString();
        //                        newRow[text.Header_ProPwShot] = rowItem[dalItem.ItemProPWShot].ToString();
        //                        newRow[text.Header_ProRwShot] = rowItem[dalItem.ItemProRWShot].ToString();

        //                        dt_MouldList.Rows.Add(newRow);

        //                        break;
        //                    }
        //                }

        //            }
        //            else if(itemFound)
        //            {
        //                break;
        //            }
        //        }

        //        if(dt_MouldList.Rows.Count <= 0)// check if datatable have data
        //        {
        //            CMB_CODE_READY = false;

        //            //System.Threading.Thread.Sleep(1000);
        //            MessageBox.Show("Item Mould List not found!\nPlease create a mould data for this item.");
        //            //call item & mould configuration form
        //            frmItemAndMouldConfiguration frm = new frmItemAndMouldConfiguration(itemCode,0);
        //            frm.StartPosition = FormStartPosition.CenterScreen;
        //            frm.ShowDialog();
        //            DT_MOULD_ITEM = dalItem.MouldItemSelect();
        //            LoadMouldList(itemCode);

        //        }
        //        else
        //        {
        //            dgvMouldList.DataSource= dt_MouldList;
        //            dgvUIEdit(dgvMouldList);
        //            dgvMouldList.ClearSelection();

        //            if(mouldCount > 1)
        //            {
        //                lblMouldQtyFound.Text = $"({mouldCount} Moulds Found)";

        //            }
        //            else
        //            {
        //                lblMouldQtyFound.Text = $"({mouldCount} Mould Found)";

        //            }
        //        }
        //    }

        //}

        #endregion
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
            DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
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

        private void ListCellFormatting(DataGridView dgv)
        {
            dgv.SuspendLayout();

            DataTable dt = (DataTable)dgv.DataSource;


            foreach (DataRow row in dt.Rows)
            {
                int rowIndex = dt.Rows.IndexOf(row);

                string itemDescription = row[text.Header_ItemDescription].ToString();

            

                if (string.IsNullOrEmpty(itemDescription))
                {
                    dgv.Rows[rowIndex].Height = 10;
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.DimGray;
                }
                else
                {
                    dgv.Rows[rowIndex].Height = 40;
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
                }

            }

            dgv.ResumeLayout();
        }

        private void dgvMouldList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            DataGridView dgv = dgvMouldList;

            if (dgv.Columns[colIndex].Name == text.Header_ItemDescription && dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
            {
                string itemDescription = dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Value.ToString();

                if(string.IsNullOrEmpty(itemDescription))
                {
                    //dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Black;
                    //dgv.Rows[rowIndex].Height = 20;
                }
                else
                {
                    //dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
                    //dgv.Rows[rowIndex].Height = 50;

                    dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
                    dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold | FontStyle.Italic);
                }
               
            }
           
           
        }

        private void tableLayoutPanel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvMouldList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvMouldList.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells[text.Header_MouldSelection] as DataGridViewCheckBoxCell;
                if (row.Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
                {
                    checkBoxCell.ReadOnly = false;  // allow checkbox to be checked/unchecked
                }
                else
                {
                    checkBoxCell.ReadOnly = true;  // disable checkbox
                    //checkBoxCell.Style.BackColor = Color.DimGray;  // indicate disabled state
                }
            }
        }

        private void dgvMouldList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dgvMouldList.Columns[text.Header_MouldSelection].Index &&
        e.RowIndex >= 0 &&
        dgvMouldList.Rows[e.RowIndex].Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item) == false)
            {
                e.PaintBackground(e.CellBounds, true);
                e.Handled = true;
            }

            if (e.ColumnIndex == dgvMouldList.Columns[text.Header_ItemSelection].Index &&
        e.RowIndex >= 0 &&
        string.IsNullOrEmpty(dgvMouldList.Rows[e.RowIndex].Cells[text.Header_ItemDescription].Value.ToString()))
            {
                e.PaintBackground(e.CellBounds, true);
                e.Handled = true;
            }
        }

        private void dgvMouldList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked column is "Mould Selection" or "Item Selection".
            if ((e.ColumnIndex == dgvMouldList.Columns[text.Header_MouldSelection].Index &&
                dgvMouldList.Rows[e.RowIndex].Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
                || e.ColumnIndex == dgvMouldList.Columns[text.Header_ItemSelection].Index)
            {
                // Change to cell selection mode.
                dgvMouldList.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgvMouldList.ReadOnly = false; // Temporarily allow user interaction

                // Toggle the checkbox value.
                DataGridViewRow row = dgvMouldList.Rows[e.RowIndex];
                var cell = row.Cells[e.ColumnIndex];

                // Make sure the cell isn't read-only before trying to change its value.
                if (!cell.ReadOnly)
                {
                    if (bool.TryParse(cell.Value?.ToString(), out bool value))
                    {
                        cell.Value = !value;
                        // End the edit mode so changes are committed immediately.
                        dgvMouldList.EndEdit();
                    }
                }
            }
            else
            {
                // Change back to full row selection mode for any other column.
                dgvMouldList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMouldList.ReadOnly = true; // Prevent user interaction with the rest of the DataGridView
            }
        }
        private bool userInitiatedChange = true;

        private void dgvMouldList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            PRO_INFO_LOADING = true;

            // Ignore changes in any column other than 'MouldSelection' and 'ItemSelection'
            if (e.ColumnIndex != dgvMouldList.Columns[text.Header_MouldSelection].Index &&
                e.ColumnIndex != dgvMouldList.Columns[text.Header_ItemSelection].Index)
                return;

            // Ignore any changes that occur programmatically to avoid infinite loops
            if (!userInitiatedChange) return;

            string currentMouldCode = "";

            try
            {
                // Temporarily disable changes from triggering this event
                userInitiatedChange = false;

                DataGridViewRow currentRow = dgvMouldList.Rows[e.RowIndex];
                currentMouldCode = currentRow.Cells[text.Header_MouldCode].Value.ToString();

                bool.TryParse(currentRow.Cells[e.ColumnIndex].Value?.ToString(), out bool valueChanged);

                // If 'MouldSelection' or 'ItemSelection' is selected or deselected, update the corresponding checkboxes
                if (currentRow.Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
                {
                    foreach (DataGridViewRow row in dgvMouldList.Rows)
                    {
                        if (row.Cells[text.Header_MouldCode].Value.ToString() == currentMouldCode)
                        {
                            row.Cells[text.Header_MouldSelection].Value = valueChanged;
                            row.Cells[text.Header_ItemSelection].Value = valueChanged;
                        }
                        else
                        {
                            row.Cells[text.Header_MouldSelection].Value = false;
                            row.Cells[text.Header_ItemSelection].Value = false;
                        }
                    }
                }
                else
                {
                    bool allItemsSelected = true;
                    bool anyItemSelected = false;

                    foreach (DataGridViewRow row in dgvMouldList.Rows)
                    {
                        if (row.Cells[text.Header_MouldCode].Value.ToString() == currentMouldCode &&
                            row.Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item) == false)
                        {
                            bool.TryParse(row.Cells[text.Header_ItemSelection].Value?.ToString(), out bool itemSelected);
                            if (!itemSelected)
                            {
                                allItemsSelected = false;
                            }
                            else
                            {
                                anyItemSelected = true;
                            }
                        }
                    }

                    foreach (DataGridViewRow row in dgvMouldList.Rows)
                    {
                        if (row.Cells[text.Header_MouldCode].Value.ToString() == currentMouldCode &&
                            row.Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
                        {
                            row.Cells[text.Header_ItemSelection].Value = allItemsSelected;
                            row.Cells[text.Header_MouldSelection].Value = anyItemSelected;
                            break;
                        }
                    }
                }
            }
            finally
            {
                // Allow changes to trigger this event again
                userInitiatedChange = true;
                LoadProductionInfo(currentMouldCode);
            }

            PRO_INFO_LOADING = false;

        }

        private void IntegerOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void CannotEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void FloatOnly_KeyPress(object sender, KeyPressEventArgs e)
        {

            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (tb != null)
            {
                // If it's not a number, not a control key (like backspace), and it's not the first decimal point
                if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.' || tb.Text.Contains('.')))
                {
                    e.Handled = true;
                }
            }

            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void dgvMouldList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // If the clicked column is "Mould Selection" or "Item Selection".
            if (e.ColumnIndex == dgvMouldList.Columns[text.Header_MouldSelection].Index ||
                e.ColumnIndex == dgvMouldList.Columns[text.Header_ItemSelection].Index)
            {
                // Commit the edit immediately for the checkbox cell.
                dgvMouldList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvMouldList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvMouldList;

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();
                int rowIndex = dgv.CurrentCell.RowIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;

                string currentHeader = dgv.Columns[colIndex].Name;

                try
                {
                    my_menu.Items.Add(text.EditMode_Open).Name = text.EditMode_Open;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = dgvMouldList;

            dgv.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemClicked = e.ClickedItem.Name.ToString();

            string itemCode = cmbPartCode.Text;

            //string jobNo = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();
            //string sheetNo = "";

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.EditMode_Open))
            {
                frmItemAndMouldConfiguration frm = new frmItemAndMouldConfiguration(itemCode, 1);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
                DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
                DT_ITEM = dalItem.Select();

                LoadMouldList(itemCode);
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void tableLayoutPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void tableLayoutPanel12_MouseClick(object sender, MouseEventArgs e)
        {
            dgvMouldList.ClearSelection();
        }

        private void tableLayoutPanel26_MouseClick(object sender, MouseEventArgs e)
        {
            dgvMouldList.ClearSelection();
        }

        private void ProductionInfo_TextChanged(object sender, EventArgs e)
        {
            if(!PRO_INFO_LOADING)
            {
                btnSaveProductionInfoAndMouldConfirmMode(true);
            }
        }

        private bool UpsertUniqueMouldItemData(itemBLL u)
        {
            bool isSuccess = false;

            // Search for a matching row in DT_MOULD_LIST
            DataRow[] existingRows = DT_MOULD_ITEM.Select($"mould_code = '{u.mould_code}' AND item_code = '{u.item_code}' AND combination_code = '{u.combination_code}'");

            if (existingRows.Length > 0)
            {
                // If a match was found, call the update function
                u.tbl_code = (int)existingRows[0]["tbl_code"];
                isSuccess = updateMouldItem(u);
            }
            else
            {
                // If no match was found, call the insert function
                u.combination_code = 0; // Set combination_code as 0 for unique item info
                isSuccess = insertMouldItem(u);
            }

            return isSuccess;
        }

        private bool InsertNewCombinationGroup(itemBLL u, List<Tuple<string, string>> selectedItems)
        {
            foreach (var item in selectedItems)
            {
                // Item2 of the tuple is itemCode
                u.item_code = item.Item2;

                // Call the method to insert the item
                if (!insertMouldItem(u))
                {
                    MessageBox.Show($"Failed to insert item with item code: {u.item_code}.", "Insertion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }



        private bool updateMouldItem(itemBLL u)
        {
            // Your update logic here
            return dalItem.MouldItemUpdate(u);
        }

        private bool insertMouldItem(itemBLL u)
        {
            // Your insert logic here
            return dalItem.MouldItemInsert(u);

        }

        private void SaveProductionInfo()
        {
            DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();

            DataTable dt_MouldList = (DataTable) dgvMouldList.DataSource;

            List<Tuple<string, string>> selectedItems = new List<Tuple<string, string>>();

            foreach (DataRow row in dt_MouldList.Rows)
            {
                bool itemSelected = bool.TryParse(row[text.Header_ItemSelection].ToString(), out bool parsedSelection) ? parsedSelection : false;

                if (itemSelected)
                {
                    string itemCode = row.Field<string>(text.Header_ItemCode);
                    string itemDescription = row[text.Header_ItemDescription].ToString();
                    string mouldCode = row[text.Header_MouldCode].ToString();

                    uItem.mould_code = mouldCode;
                    uItem.item_code = itemCode;

                    if (!itemDescription.Contains(text.All_Item))
                        selectedItems.Add(new Tuple<string, string>(mouldCode, itemCode));
                }
            }

            if (selectedItems.Select(x => x.Item1).Distinct().Count() > 1 && selectedItems.Count > 1)
            {
                throw new Exception("All selected items must have the same mould code");
            }

            //update unique production info data
            uItem.updated_date = DateTime.Now;
            uItem.updated_by = MainDashboard.USER_ID;
            uItem.item_updtd_date = uItem.updated_date;
            uItem.item_updtd_by = uItem.updated_by;
            uItem.item_added_date = uItem.updated_date;
            uItem.item_added_by = uItem.updated_by;
            uItem.removed = false;

            uItem.mould_cavity = int.TryParse(txtCavity.Text, out int y) ? y : 0;
            uItem.mould_ct = int.TryParse(txtCycleTime.Text, out y) ? y : 0;
            uItem.item_pw_shot = float.TryParse(txtPWPerShot.Text, out float k) ? k : 0;
            uItem.item_rw_shot = float.TryParse(txtRWPerShot.Text, out k) ? k : 0;

            uItem.MouldDefaultSelection = true;
            uItem.combination_code = 0;

            if (selectedItems.Count == 1)
            {
                UpsertUniqueMouldItemData(uItem);

            }
            else if (selectedItems.Count > 1)
            {
                bool foundCombination = false;

                foreach (var combination in DT_MOULD_ITEM.AsEnumerable()
                                                                    .Where(r => r.Field<string>(dalItem.MouldCode) == uItem.mould_code && r.Field<int>(dalItem.CombinationCode) != 0)
                                                                    .GroupBy(r => r.Field<int>(dalItem.CombinationCode)))
                {
                    var itemsInCombination = combination.Select(r => r.Field<string>(dalItem.ItemCode)).ToArray();
                    var selectedItemCodes = selectedItems.Select(i => i.Item2).ToArray();

                    if (!selectedItemCodes.Except(itemsInCombination).Any() && !itemsInCombination.Except(selectedItemCodes).Any())
                    {
                        uItem.combination_code = combination.Key;
                        foundCombination = true;
                        dalItem.MouldItemProInfoUpdateByCombinationCode(uItem);
                        break;
                    }
                }

                if (!foundCombination)
                {
                    // If we haven't found a combination, generate a new combination code
                    var maxCombinationCode = DT_MOULD_ITEM.AsEnumerable()
                                                          .Where(r => r.Field<string>("mould_code") == uItem.mould_code)
                                                          .Max(r => r.Field<int>("combination_code"));

                    uItem.combination_code = maxCombinationCode + 1;

                    // Call insert method here
                    InsertNewCombinationGroup(uItem, selectedItems);
                }

            }
            else
            {
                ClearProductionInfo();

            }

            DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
        }

        private void btnMouldSelected_Click(object sender, EventArgs e)
        {
            if(btnMouldSelected.Text == BTN_INFO_SAVE_CONFIRM_MOULD)
            {
                //SAVE PRODUCTION INFO
                //SEARCH IF SELECTED ITEMS AND COMBINATION CODE EXIST
                //IF NOT, CREATE A NEW COMBINATION CODE
                SaveProductionInfo();

            }

            btnSaveProductionInfoAndMouldConfirmMode(false);
            btnMouldSelected.Visible = false;

            //MOULD Selected
            //step 2: monthly balance estimation, production history (included machine no)
            MessageBox.Show("Step 2");

            //step 3: process to production requirement steps
            //raw material
            //color material
            //
        }

        private void txtHoursPerDay_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
