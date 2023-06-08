using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using Syncfusion.XlsIO.Implementation.XmlSerialization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmItemAndMouldConfiguration : Form
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

        DataTable DT_ITEM;
        DataTable DT_MOULD_ITEM;
        private readonly string ITEM_CODE;
        bool dataChanged = false;
        bool MouldListEditMode = false;

        int EDIT_MODE = 0;

        private Color EditCellColor = SystemColors.Info;
        private Color DisableEditCellColor = Color.DimGray;
        private Color AutoFillCellColor = Color.White;

        #endregion

        public frmItemAndMouldConfiguration()
        {
            InitializeComponent();
            MouldListEditMode = false;

        }

        public frmItemAndMouldConfiguration(string itemCode, int editMode)
        {
            InitializeComponent();
            EDIT_MODE = editMode;

            ITEM_CODE = itemCode;
            DT_ITEM = dalItem.Select();
            MouldListEditMode = false;
            //edit mode
            //0 : add new mould data from null
            //1 : mould data found , want to add new mould data or edit existing mould data
            if (EDIT_MODE == 0)
            {
                //UI Change : show suggestion List
                ShowSuggestionList(true);
                //Load suggestion List
                LoadSuggestionMouldList(itemCode);
            }
            else
            {
                MouldListEditMode = true;

                //UI change : hide suggestion List
                ShowSuggestionList(false);
                LoadMouldList(itemCode);

            }

        }

        #region Data Loading/Checking/Save
        private void LoadMouldList(string itemCode)
        {
            dgvMouldList.DataSource = null;

            if (!string.IsNullOrEmpty(itemCode))
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

                    if (true)//DB_ItemCode == itemCode
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
                        if (mouldCount > 1)
                        {
                            dt_MouldList.Rows.Add(dt_MouldList.NewRow());
                        }

                        DataRow newRow = CreateNewRow(dt_MouldList, row, itemCode, tool.getItemNameFromDataTable(DT_ITEM,itemCode));
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
                                    DataRow masterRow = CreateMasterRow(dt_MouldList, rowItem, mouldCode, tool.getItemNameFromDataTable(DT_ITEM,mouldCode), AllItemItemDesciption);
                                    dt_MouldList.Rows.Add(masterRow);
                                    break;
                                }
                            }
                        }
                    }
                }
                if (dt_MouldList.Rows.Count > 0)
                {
                    dgvMouldList.DataSource = dt_MouldList;
                    dgvUIEdit(dgvMouldList);
                    ListCellFormatting(dgvMouldList);

                    dgvMouldList.ClearSelection();
                }
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

        private bool ValidateDataTable(DataTable dt)
        {
            // Set to track the combinations
            HashSet<string> combinations = new HashSet<string>();

            // Check each row in the DataTable
            foreach (DataRow row in dt.Rows)
            {
                // Fetch the item code and mould code from the row


                // Check if any cell in the row is empty
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    if (string.IsNullOrWhiteSpace(row[i].ToString()))
                //    {

                //        string itemDescription = row[text.Header_ItemDescription].ToString();
                //        string colName = dt.Columns[i].ColumnName;

                //        if(colName == text.Header_ProTon && itemDescription.Contains(text.All_Item) == false)
                //        {

                //        }
                //        else
                //        {
                //            MessageBox.Show($"Error: Empty cell found at row {dt.Rows.IndexOf(row) + 1} column {dt.Columns[i].ColumnName}");
                //            return false;
                //        }
                       

                //    }
                //}

                string itemCode = row[text.Header_ItemCode].ToString();
                string mouldCode = row[text.Header_MouldCode].ToString();

                // Create a composite key
                string compositeKey = $"{mouldCode}-{itemCode}";

                // If the composite key is already in the set, return an error
                if (combinations.Contains(compositeKey))
                {
                    MessageBox.Show($"Error: Duplicate pair (Mould Code: {mouldCode}, Item Code: {itemCode}) found");
                    return false;
                }
                else
                {
                    // Otherwise, add the composite key to the set
                    combinations.Add(compositeKey);
                }
            }

            // If no errors found, return an empty string
            return true;
        }

        private bool UpsertUniqueMouldItemData(itemBLL u)
        {
            bool isSuccess = false;

            // Search for a matching row in DT_MOULD_LIST
            DataRow[] existingRows = DT_MOULD_ITEM.Select($"mould_code = '{u.mould_code}' AND item_code = '{u.item_code}' AND combination_code = 0");

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

        private bool removeMouldItemData(itemBLL u)
        {
            return dalItem.MouldItemRemove(u);
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

        private bool UpsertItemData(itemBLL u)
        {
            bool isSuccess = false;

            // Search for a matching row in DT_MOULD_LIST
            DataRow[] existingRows = DT_ITEM.Select($"item_code = '{u.mould_code}'");

            if (existingRows.Length > 0)
            {
                isSuccess = updateItem(u);
            }
            else
            {
                // If no match was found, call the insert function
                isSuccess = insertItem(u);
            }

            return isSuccess;
        }

        private bool updateItem(itemBLL u)
        {
            // Your update logic here
            return dalItem.MouldUpdate(u);
        }

        private bool insertItem(itemBLL u)
        {
            // Your insert logic here
            return dalItem.MouldInsert(u);

        }

        private bool RemoveNonExistentMouldData(string mainItemCode, DataTable dt_MouldList)
        {
            // Initialize a success flag
            bool success = true;
            uItem.updated_date = DateTime.Now;
            uItem.updated_by = MainDashboard.USER_ID;
           

            // Get all mould codes related to the main item code
            DataTable DB_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();

            var relatedMouldCodes = DB_MOULD_ITEM.AsEnumerable()
                .Where(row => row.Field<string>(dalItem.ItemCode) == mainItemCode)
                .Select(row => row.Field<string>(dalItem.MouldCode))
                .Distinct()
                .ToList();

            // Get all items related to the above mould codes
            var relatedRows = DB_MOULD_ITEM.AsEnumerable()
                .Where(row => relatedMouldCodes.Contains(row.Field<string>(dalItem.MouldCode)))
                .ToList();

            // Loop through each related row
            foreach (DataRow row in relatedRows)
            {

                string mouldCode = row[dalItem.MouldCode].ToString();
                string itemCode = row[dalItem.ItemCode].ToString();

                // Check if the mould-item pair exists in dt_MouldList
                bool existsInDtMouldList = dt_MouldList.AsEnumerable()
                    .Any(r => r.Field<string>(text.Header_MouldCode) == mouldCode && r.Field<string>(text.Header_ItemCode) == itemCode);

                // If it doesn't exist, mark it as removed in the database
                if (!existsInDtMouldList)
                {
                    uItem.tbl_code = int.TryParse(row[dalItem.TblCode].ToString(), out int tableCode) ? tableCode : 0;
                    uItem.removed = true;

                    bool updateSuccessful = removeMouldItemData(uItem);

                    // If the update was not successful, set the success flag to false
                    if (!updateSuccessful)
                    {
                        success = false;
                        MessageBox.Show("Failed to auto remove Mould Item Data! [frmItemAndMouldConfiguration]");
                    }
                }
            }

            uItem.removed = false;

            // Return the success flag
            return success;
        }



        private bool SaveMouldData()
        {
            bool dataSaved = false;
            DT_MOULD_ITEM = dalItem.MouldItemSelect();
            DT_ITEM = dalItem.Select();
            if (dgvMouldList?.Rows.Count > 0)
            {
                DataTable dt_MouldList = (DataTable)dgvMouldList.DataSource;

                //validation check: empty data, repeated data
                if (ValidateDataTable(dt_MouldList))
                {
                    dt_MouldList.DefaultView.Sort = text.Header_MouldCode + " ASC";
                    dt_MouldList.DefaultView.ToTable();

                    uItem.updated_date = DateTime.Now;
                    uItem.updated_by = MainDashboard.USER_ID;

                    uItem.item_updtd_date = uItem.updated_date;
                    uItem.item_updtd_by = uItem.updated_by;

                    uItem.item_added_date = uItem.updated_date;
                    uItem.item_added_by = uItem.updated_by;

                    uItem.removed = false;

                    //save all item info to tbl_item mould code
                    //save item code info to tbl_mould_item
                    foreach (DataRow row in dt_MouldList.Rows)
                    {
                        string mouldCode = row[text.Header_MouldCode].ToString();
                        string itemCode = row[text.Header_ItemCode].ToString();
                        string itemName = row[text.Header_ItemName].ToString();
                        string itemDescription = row[text.Header_ItemDescription].ToString();

                        int ton = int.TryParse(row[text.Header_ProTon].ToString(), out ton) ? ton : 0;
                        int cavity = int.TryParse(row[text.Header_Cavity].ToString(), out cavity) ? cavity : 0;
                        int cycleTime = int.TryParse(row[text.Header_ProCT].ToString(), out cycleTime) ? cycleTime : 0;

                        float PWPerShot = float.TryParse(row[text.Header_ProPwShot].ToString(), out PWPerShot) ? PWPerShot : 0;
                        float RWPerShot = float.TryParse(row[text.Header_ProRwShot].ToString(), out RWPerShot) ? RWPerShot : 0;


                        if (itemDescription.Contains(text.All_Item) && !string.IsNullOrEmpty(mouldCode))
                        {
                            //update tbl_item
                            uItem.item_cat = text.Cat_Mould;
                            uItem.item_code = mouldCode;
                            uItem.item_code_present = mouldCode;
                            uItem.item_name = itemName;
                            uItem.item_cavity = cavity;
                            uItem.item_pro_ton = ton;
                            uItem.item_pro_ct_to = cycleTime;
                            uItem.item_pro_pw_shot = PWPerShot;
                            uItem.item_pro_rw_shot = RWPerShot;

                            dataSaved = UpsertItemData(uItem);
                        }
                        else if (!string.IsNullOrEmpty(itemCode))
                        {
                            //update or insert tbl_mould_item
                            uItem.mould_code = mouldCode;
                            uItem.item_code = itemCode;
                            uItem.mould_cavity = cavity;
                            uItem.mould_ct = cycleTime;
                            uItem.item_pw_shot = PWPerShot;
                            uItem.item_rw_shot = RWPerShot;
                            uItem.MouldDefaultSelection = false;
                            uItem.combination_code = 0;
                            uItem.removed = false;

                            dataSaved = UpsertUniqueMouldItemData(uItem);

                        }
                    }

                    if(dataSaved)
                    {
                        RemoveNonExistentMouldData(ITEM_CODE, dt_MouldList);
                    }
                }
            }
            else
            {
                MessageBox.Show("No mould data found!");
            }

            return dataSaved;
        }

        private bool HasSelectedRow(DataGridView dgv)
        {
            return dgv?.SelectedRows.Count > 0;
        }

        private void MoveSuggestMouldToMouldList(string mouldCode)
        {
            if (AddMouldToMouldList(mouldCode))
            {
                //Mould List UI Update

                AfterMoveRightUIUpdate(mouldCode);
            }
        }

        private bool AddMouldToMouldList(string mouldCode)
        {
            DataTable dt_MouldList;

            if (dgvMouldList?.SelectedRows.Count <= 0)
            {
                dt_MouldList = NewMouldListTable();
            }
            else
            {
                //check if mould existing in mould list
                dt_MouldList = (DataTable)dgvMouldList.DataSource;

                DataRow[] foundRows = dt_MouldList.Select(text.Header_MouldCode + " = '" + mouldCode + "'");

                if (foundRows.Length > 0)
                {
                    MessageBox.Show("The mould code was found in the list.\nPlease right-click on the Mould List to insert item.");
                    return false;
                }
            }

            //add item info
            return AddItemToMouldList(mouldCode, ITEM_CODE);
        }

        private DataTable LoadOtherItemPairedWithMould(DataTable dt_MouldList, string mouldCode, string MainItemCode)
        {
            if (DT_MOULD_ITEM == null)
            {
                DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
            }

            if (DT_ITEM == null)
            {
                DT_ITEM = dalItem.Select();
            }

            foreach (DataRow row in DT_MOULD_ITEM.Rows)
            {
                string itemCode = row[dalItem.ItemCode].ToString();
                string combinationCode = row[dalItem.CombinationCode].ToString();

                if (mouldCode == row[dalItem.MouldCode].ToString() && itemCode != MainItemCode && (string.IsNullOrEmpty(combinationCode) || combinationCode == "0"))
                {
                    foreach (DataRow rowItem in DT_ITEM.Rows)
                    {
                        if (itemCode == rowItem[dalItem.ItemCode].ToString())
                        {
                            string itemName = rowItem[dalItem.ItemName].ToString();
                            string itemDesription = itemName + " (" + itemCode + ")";

                            DataRow itemNewRow = dt_MouldList.NewRow();

                            itemNewRow[text.Header_MouldCode] = mouldCode;
                            //itemNewRow[text.Header_ProTon] = rowItem[dalItem.ItemProTon].ToString();//item_best_ton
                            itemNewRow[text.Header_ItemDescription] = itemDesription;
                            itemNewRow[text.Header_ItemCode] = itemCode;
                            itemNewRow[text.Header_ItemName] = itemName;
                            itemNewRow[text.Header_Cavity] = rowItem[dalItem.ItemCavity].ToString();
                            //itemNewRow[text.Header_ProCT] = rowItem[dalItem.ItemProCTTo].ToString();
                            itemNewRow[text.Header_ProPwShot] = rowItem[dalItem.ItemProPWShot].ToString();
                            //itemNewRow[text.Header_ProRwShot] = rowItem[dalItem.ItemProRWShot].ToString();
                            dt_MouldList.Rows.Add(itemNewRow);
                            break;
                        }
                    }
                }
            }

            return dt_MouldList;
            //if (dt_MouldList.Rows.Count > 0)
            //{
            //    dgvMouldList.DataSource = dt_MouldList;
            //    dgvUIEdit(dgvMouldList);
            //    ListCellFormatting(dgvMouldList);

            //    dgvMouldList.ClearSelection();
            //}
        }

        private bool AddItemToMouldList(string mouldCode, string itemCode)
        {
            bool itemAdded = false;
            DataTable dt_MouldList = NewMouldListTable();

            DataRow itemNewRow = dt_MouldList.NewRow();
            DataRow mouldNewRow = dt_MouldList.NewRow();

            bool itemDataFound = false;
            bool mouldDataFound = false;

            foreach (DataRow row in DT_ITEM.Rows)
            {
                if (itemCode == row[dalItem.ItemCode].ToString())
                {

                    string itemName = row[dalItem.ItemName].ToString();
                    string itemDesription = itemName + " (" + itemCode + ")";

                    itemNewRow[text.Header_MouldCode] = mouldCode;
                    //itemNewRow[text.Header_ProTon] = row[dalItem.ItemProTon].ToString();//item_best_ton
                    itemNewRow[text.Header_ItemDescription] = itemDesription;
                    itemNewRow[text.Header_ItemCode] = itemCode;
                    itemNewRow[text.Header_ItemName] = itemName;
                    itemNewRow[text.Header_Cavity] = row[dalItem.ItemCavity].ToString();
                    //itemNewRow[text.Header_ProCT] = row[dalItem.ItemProCTTo].ToString();
                    itemNewRow[text.Header_ProPwShot] = row[dalItem.ItemProPWShot].ToString();
                    //itemNewRow[text.Header_ProRwShot] = row[dalItem.ItemProRWShot].ToString();

                    itemDataFound= true;
                }

                if (mouldCode == row[dalItem.ItemCode].ToString())
                {
                    //add mould all item production mode info
                    mouldNewRow[text.Header_MouldCode] = mouldCode;
                    mouldNewRow[text.Header_ItemCode] = mouldCode;
                    mouldNewRow[text.Header_ItemName] = tool.getItemNameFromDataTable(DT_ITEM, mouldCode);
                    mouldNewRow[text.Header_ProTon] = row[dalItem.ItemProTon].ToString();
                    mouldNewRow[text.Header_ItemDescription] = text.All_Item;
                    mouldNewRow[text.Header_Cavity] = row[dalItem.ItemCavity].ToString();
                    mouldNewRow[text.Header_ProCT] = row[dalItem.ItemProCTTo].ToString();
                    mouldNewRow[text.Header_ProPwShot] = row[dalItem.ItemProPWShot].ToString();
                    mouldNewRow[text.Header_ProRwShot] = row[dalItem.ItemProRWShot].ToString();

                    mouldDataFound= true;
                }

                if(mouldDataFound && itemDataFound)
                {
                    break;
                }
            }

            if (dgvMouldList == null || dgvMouldList?.SelectedRows.Count <= 0)
            {
                dt_MouldList.Rows.Add(itemNewRow);

                //load other item paird with the mould
                LoadOtherItemPairedWithMould(dt_MouldList, mouldCode, itemCode);

                dt_MouldList.Rows.Add(mouldNewRow);

                itemAdded = true;
            }
            else
            {
                //check if mould existing in mould list, get row index if found
                dt_MouldList = (DataTable)dgvMouldList.DataSource;
                int MouldIndex = -1;

                foreach(DataRow row in dt_MouldList.Rows)
                {
                    if (row[text.Header_MouldCode].ToString().Equals(mouldCode) && row[text.Header_ItemDescription].ToString().Contains(text.All_Item))
                    {
                        //get mould master row index
                        MouldIndex = dt_MouldList.Rows.IndexOf(row);
                        break;
                    }
                }

                if(MouldIndex != -1)
                {
                    dt_MouldList.Rows.InsertAt(itemNewRow, MouldIndex);
                    itemAdded = true;
                }
                else
                {
                    //create a new row
                    dt_MouldList.Rows.Add(itemNewRow);

                    LoadOtherItemPairedWithMould(dt_MouldList, mouldCode, itemCode);
                    dt_MouldList.Rows.Add(mouldNewRow);
                    itemAdded = true;
                }

               
            }

            if(itemAdded)
            {
                dgvMouldList.DataSource = dt_MouldList;
                ResetAllItemCounting(mouldCode);
                
                dgvUIEdit(dgvMouldList);
                ListCellFormatting(dgvMouldList);
                ShowSaveButton(true);
            }

            return itemAdded;
        }

        private void ResetAllItemCounting()
        {
            DataTable dt_MouldList = (DataTable) dgvMouldList.DataSource;

            int itemCount = 0;
            int cavity = 0;
            decimal partWeightPerShot = 0;

            string MouldCode_Searching = "";

            dt_MouldList.AcceptChanges();

            foreach (DataRow row in dt_MouldList.Rows)
            {
                string mouldCode = row[text.Header_MouldCode].ToString();
                
                if(mouldCode != MouldCode_Searching)
                {
                    itemCount = 0;
                    cavity = 0;
                    partWeightPerShot = 0;

                    MouldCode_Searching = mouldCode;
                }

                if (!row[text.Header_ItemDescription].ToString().Contains(text.All_Item))
                {
                    itemCount++;
                    cavity += int.TryParse(row[text.Header_Cavity].ToString(), out int i) ? i : 0;
                    partWeightPerShot += decimal.TryParse(row[text.Header_ProPwShot].ToString(), out decimal k) ? k : 0;


                }
                else
                {
                    if(itemCount == 0)
                    {
                        //remove row
                        row.Delete();
                    }
                    else
                    {
                        string AllItemItemDesciption = CreateItemDescription(itemCount);

                        row[text.Header_ItemDescription] = AllItemItemDesciption;
                        row[text.Header_Cavity] = cavity;
                        row[text.Header_ProPwShot] = decimal.Round(partWeightPerShot,2);
                    }
                    
                }

            }

            dt_MouldList.AcceptChanges();

            //to remove row if last row is empty row (item description)

            if (dt_MouldList.Rows.Count > 0) // check if the DataTable is not empty
            {
                DataRow lastRow = dt_MouldList.Rows[dt_MouldList.Rows.Count - 1]; // get the last row

                if (string.IsNullOrEmpty(lastRow[text.Header_ItemDescription].ToString().Trim())) // check if item_description column is empty
                {
                    dt_MouldList.Rows.RemoveAt(dt_MouldList.Rows.Count - 1); // remove the last row
                }
            }

        }

        private void ResetAllItemCounting(string MouldCode_Searching)
        {
            DataTable dt_MouldList = (DataTable)dgvMouldList.DataSource;

            if(dt_MouldList != null)
            {
                int itemCount = 0;
                int cavity = 0;
                decimal partWeightPerShot = 0;

                dt_MouldList.AcceptChanges();

                foreach (DataRow row in dt_MouldList.Rows)
                {
                    string mouldCode = row[text.Header_MouldCode].ToString();

                    if (mouldCode == MouldCode_Searching)
                    {
                        if (!row[text.Header_ItemDescription].ToString().Contains(text.All_Item))
                        {
                            itemCount++;
                            cavity += int.TryParse(row[text.Header_Cavity].ToString(), out int i) ? i : 0;
                            partWeightPerShot += decimal.TryParse(row[text.Header_ProPwShot].ToString(), out decimal k) ? k : 0;
                        }
                        else
                        {
                            if (itemCount == 0)
                            {
                                //remove row
                                row.Delete();
                            }
                            else
                            {
                                string AllItemItemDesciption = CreateItemDescription(itemCount);
                                row[text.Header_ItemDescription] = AllItemItemDesciption;
                                row[text.Header_Cavity] = cavity;
                                row[text.Header_ProPwShot] = decimal.Round(partWeightPerShot, 2);
                            }

                        }
                    }



                }

                dt_MouldList.AcceptChanges();

                //to remove row if last row is empty row (item description)

                if (dt_MouldList.Rows.Count > 0) // check if the DataTable is not empty
                {
                    DataRow lastRow = dt_MouldList.Rows[dt_MouldList.Rows.Count - 1]; // get the last row

                    if (string.IsNullOrEmpty(lastRow[text.Header_ItemDescription].ToString().Trim())) // check if item_description column is empty
                    {
                        dt_MouldList.Rows.RemoveAt(dt_MouldList.Rows.Count - 1); // remove the last row
                    }
                }
            }
          

        }

        private void ResetAllItemCounting(string MouldCode_Searching, string Header_Searching)
        {
            DataTable dt_MouldList = (DataTable)dgvMouldList.DataSource;

            int cavity = 0;
            decimal partWeightPerShot = 0;

            dt_MouldList.AcceptChanges();

            foreach (DataRow row in dt_MouldList.Rows)
            {
                string mouldCode = row[text.Header_MouldCode].ToString();

                if (mouldCode == MouldCode_Searching)
                {
                    if (!row[text.Header_ItemDescription].ToString().Contains(text.All_Item))
                    {
                        if(Header_Searching.Equals(text.Header_ProPwShot))
                        {
                            partWeightPerShot += decimal.TryParse(row[Header_Searching].ToString(), out decimal i) ? i : 0;
                        }
                        else
                        {
                            cavity += int.TryParse(row[Header_Searching].ToString(), out int i) ? i : 0;
                        }
                    }
                    else
                    {
                        if (Header_Searching.Equals(text.Header_ProPwShot))
                        {
                            row[Header_Searching] = decimal.Round(partWeightPerShot, 2);
                        }
                        else
                        {
                            row[Header_Searching] = cavity;
                        }
                    }
                }



            }

            dt_MouldList.AcceptChanges();

            //to remove row if last row is empty row (item description)

            if (dt_MouldList.Rows.Count > 0) // check if the DataTable is not empty
            {
                DataRow lastRow = dt_MouldList.Rows[dt_MouldList.Rows.Count - 1]; // get the last row

                if (string.IsNullOrEmpty(lastRow[text.Header_ItemDescription].ToString().Trim())) // check if item_description column is empty
                {
                    dt_MouldList.Rows.RemoveAt(dt_MouldList.Rows.Count - 1); // remove the last row
                }
            }

        }
        private void MoveMouldListToSuggestMould(string mouldCode)
        {

            if (MessageBox.Show("Are you sure you want to move this mould " + mouldCode + " to the suggestion list?", "Confirm Action", MessageBoxButtons.YesNo) == DialogResult.Yes)//MessageBox.Show("Are you sure you want to move this mould "+mouldCode+" to the suggestion list?", "Confirm Action", MessageBoxButtons.YesNo) == DialogResult.Yes
            {
                dgvSuggestionList.Focus();
                DataTable dt_SuggestionList;

                if (dgvSuggestionList?.RowCount <= 0)
                {
                    dt_SuggestionList = NewSuggestionMouldListTable();

                }
                else
                {
                    dt_SuggestionList = (DataTable)dgvSuggestionList.DataSource;

                }

                DataRow newRow = dt_SuggestionList.NewRow();

                newRow[text.Header_MouldCode] = mouldCode;
                newRow[text.Header_MouldDescription] = tool.getItemNameFromDataTable(DT_ITEM, mouldCode);

                dt_SuggestionList.Rows.Add(newRow);


                dgvSuggestionList.DataSource = dt_SuggestionList;
                dgvUIEdit(dgvSuggestionList);
                AfterMoveLeftUIUpdate(mouldCode);

            }

            
        }
        private void LoadSuggestionMouldList(string itemCode)
        {
            dgvSuggestionList.DataSource = null;

            if(!string.IsNullOrEmpty(itemCode))
            {
                string itemName = tool.getItemName(itemCode).ToUpper().Replace(" ","");

                DataTable dt_SuggestionMouldList = NewSuggestionMouldListTable();

                DataTable DB_Item = dalItem.CatSearch(text.Cat_Mould);

                foreach(DataRow row in DB_Item.Rows)
                {
                    string mouldName = row[dalItem.ItemName].ToString();

                    if(mouldName.ToUpper().Replace(" ", "").Contains(itemName) || mouldName.ToUpper().Replace(" ", "").Contains(itemCode.ToUpper().Replace(" ", "")))
                    {
                        DataRow newRow = dt_SuggestionMouldList.NewRow();

                        newRow[text.Header_MouldCode] = row[dalItem.ItemCode].ToString();
                        newRow[text.Header_MouldDescription] = mouldName;

                        dt_SuggestionMouldList.Rows.Add(newRow);

                    }
                }

                dgvSuggestionList.DataSource = dt_SuggestionMouldList;
                dgvUIEdit(dgvSuggestionList);
                dgvSuggestionList.ClearSelection();
            }
        }

        #endregion

        #region UI/UX

        private void ClearAllListSelectionIfAny()
        {
            if(HasSelectedRow(dgvMouldList))
            {
                dgvMouldList.ClearSelection();

            }

            if (HasSelectedRow(dgvSuggestionList))
            {
                dgvSuggestionList.ClearSelection();

            }
        }

        private void AfterMoveRightUIUpdate(string mouldCode)
        {
            //remove from suggestion list
            DeleteSelectedRows(dgvSuggestionList, mouldCode);

            //get rows selected, also update all item qty
            UpdateMouldListAllItemCountAndSelectRows(dgvMouldList, mouldCode);

            //show save button
            ShowSaveButton(true);
        }

        private void AfterMoveLeftUIUpdate(string mouldCode)
        {
            //remove from suggestion list
            DeleteSelectedRows(dgvMouldList, mouldCode);

            //get rows selected, also update all item qty
            SelectRowsBasedOnMouldCode(dgvSuggestionList, mouldCode);

            ShowSaveButton(dataChanged);

            if(dgvMouldList.Rows.Count <= 0)
            {
                MouldListEditMode = false;
            }
        }

        private void MouldListEditModeChangeColumnColor()
        {
            //DataGridView dgv = dgvMouldList;

            //if (MouldListEditMode && dgvMouldList?.Rows.Count > 0)
            //{
            //    dgv.Columns[text.Header_ProTon].DefaultCellStyle.BackColor = EditCellColor;
            //    dgv.Columns[text.Header_Cavity].DefaultCellStyle.BackColor = EditCellColor;
            //    dgv.Columns[text.Header_ProCT].DefaultCellStyle.BackColor = EditCellColor;
            //    dgv.Columns[text.Header_ProPwShot].DefaultCellStyle.BackColor = EditCellColor;
            //    dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.BackColor = EditCellColor;
            //}
            //else
            //{
            //    dgv.Columns[text.Header_ProTon].DefaultCellStyle.BackColor = AutoFillCellColor;
            //    dgv.Columns[text.Header_Cavity].DefaultCellStyle.BackColor = AutoFillCellColor;
            //    dgv.Columns[text.Header_ProCT].DefaultCellStyle.BackColor = AutoFillCellColor;
            //    dgv.Columns[text.Header_ProPwShot].DefaultCellStyle.BackColor = AutoFillCellColor;
            //    dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.BackColor = AutoFillCellColor;

            //}
        }

        public void DeleteSelectedRows(DataGridView dgv, string mouldCode)
        {
            for (int i = dgv.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = dgv.Rows[i];
                if (row.IsNewRow) continue; // Skip the new row at the end of the grid.

                // Replace "YourConditionHere" with the condition you want to use to delete rows.
                if (row.Cells[text.Header_MouldCode].Value.ToString() == mouldCode)
                {
                    dgv.Rows.RemoveAt(i);
                }
            }
        }

        public void SelectRowsBasedOnMouldCode(DataGridView dgv, string mouldCode)
        {
            dgv.ClearSelection(); // Clear any existing selection.

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                DataGridViewRow row = dgv.Rows[i];
                if (row.IsNewRow) continue; // Skip the new row at the end of the grid.

                // Replace "YourConditionHere" with the condition you want to use to select rows.
                if (row.Cells[text.Header_MouldCode].Value.ToString() == mouldCode)
                {
                    row.Selected = true;
                }
            }
        }

        public void UpdateMouldListAllItemCountAndSelectRows(DataGridView dgv, string mouldCode)
        {
            dgv.ClearSelection(); // Clear any existing selection.
            int AllItemRowIndex = -1;
            int itemCount = 0;
            int cavity = 0;
            decimal partWeightPerShot = 0;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                DataGridViewRow row = dgv.Rows[i];
                if (row.IsNewRow) continue; // Skip the new row at the end of the grid.

                // Replace "YourConditionHere" with the condition you want to use to select rows.
                if (row.Cells[text.Header_MouldCode].Value.ToString() == mouldCode)
                {
                    string itemDescription = row.Cells[text.Header_ItemDescription].Value.ToString();

                    if(itemDescription.Contains(text.All_Item))
                    {
                        AllItemRowIndex = row.Index;
                    }
                    else
                    {
                        itemCount++;
                        cavity += int.TryParse(row.Cells[text.Header_Cavity].Value.ToString(), out int x) ? x : 0;
                        partWeightPerShot += decimal.TryParse(row.Cells[text.Header_ProPwShot].Value.ToString(), out decimal k) ? k : 0;
                    }
                    row.Selected = true;
                }
            }

            if(AllItemRowIndex > -1)
            {
                string AllItemItemDesciption = text.All_Item;

                if(itemCount > 1)
                {
                    AllItemItemDesciption += "S (" + itemCount + ")"; 
                }
                else
                {
                    AllItemItemDesciption += " (" + itemCount + ")";

                }
                dgv.Rows[AllItemRowIndex].Cells[text.Header_Cavity].Value = cavity;
                dgv.Rows[AllItemRowIndex].Cells[text.Header_ProPwShot].Value = decimal.Round(partWeightPerShot, 2);

                dgv.Rows[AllItemRowIndex].Cells[text.Header_ItemDescription].Value = AllItemItemDesciption;
                dgv.Rows[AllItemRowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
                dgv.Rows[AllItemRowIndex].Cells[text.Header_ItemDescription].Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold | FontStyle.Italic);

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
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = DisableEditCellColor;
                }
                else
                {
                    dgv.Rows[rowIndex].Height = 50;
                }

                if (itemDescription.Contains(text.All_Item))
                {
                    dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                    dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold | FontStyle.Italic);

                    //default back color
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = AutoFillCellColor;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_ProTon].Style.BackColor = EditCellColor;
                    dgv.Rows[rowIndex].Cells[text.Header_ProCT].Style.BackColor = EditCellColor;
                    dgv.Rows[rowIndex].Cells[text.Header_ProRwShot].Style.BackColor = EditCellColor;

                    //auto fill in data by ststem
                    dgv.Rows[rowIndex].Cells[text.Header_ProPwShot].Style.BackColor = AutoFillCellColor;
                }
                else
                {
                    //default back color
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = AutoFillCellColor;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_Cavity].Style.BackColor = EditCellColor;
                    dgv.Rows[rowIndex].Cells[text.Header_ProPwShot].Style.BackColor = EditCellColor;

                    //not allow user to edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_ProCT].Style.BackColor = DisableEditCellColor;

                    dgv.Rows[rowIndex].Cells[text.Header_ProRwShot].Style.BackColor = DisableEditCellColor;

                    dgv.Rows[rowIndex].Cells[text.Header_ProTon].Style.BackColor = DisableEditCellColor;

                }
            }

            dgv.ResumeLayout();
        }


        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

            if (dgv == dgvSuggestionList)
            {
                //dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                ////dgv.Columns[headerPartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                ////dgv.Columns[headerPartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dgv.Columns[headerUpdatedDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //dgv.Columns[headerUpdatedBy].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_MouldDescription].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[text.Header_MouldDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }

            else if(dgv == dgvMouldList)
            {
                dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 200;


                //dgv.Columns[text.Header_Cavity].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                //dgv.Columns[text.Header_ProTon].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                //dgv.Columns[text.Header_ProCT].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                //dgv.Columns[text.Header_ProPwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                //dgv.Columns[text.Header_ProRwShot].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                //int ProductionInfoColumnWidth = 50;

                //dgv.Columns[text.Header_Cavity].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProTon].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProCT].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProPwShot].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProRwShot].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProTon].Width = ProductionInfoColumnWidth;


                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;

                //dgv.Columns[text.Header_Cavity].Visible = false;
                //dgv.Columns[text.Header_ProCT].Visible = false;
                //dgv.Columns[text.Header_ProPwShot].Visible = false;
                //dgv.Columns[text.Header_ProRwShot].Visible = false;

                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            }


        }

        private DataTable NewMouldListTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_MouldCode, typeof(string));
            dt.Columns.Add(text.Header_ProTon, typeof(string));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_Cavity, typeof(string));
            dt.Columns.Add(text.Header_ProCT, typeof(string));
            dt.Columns.Add(text.Header_ProPwShot, typeof(string));
            dt.Columns.Add(text.Header_ProRwShot, typeof(string));

            return dt;
        }

        private DataTable NewSuggestionMouldListTable()
        {
            DataTable dt = new DataTable();
          
            dt.Columns.Add(text.Header_MouldCode, typeof(string));
            dt.Columns.Add(text.Header_MouldDescription, typeof(string));

            return dt;
        }

        private void ShowSuggestionList(bool show)
        {
            if(show)
            {
                tlpMain.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 35f);
                tlpMain.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 30f);
                tlpMain.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 65f);
            }
            else
            {
                tlpMain.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
                tlpMain.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);
                tlpMain.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 100f);
            }
            
        }

        private void ShowSaveButton(bool show)
        {
            btnSave.Visible = show;

            if (show)
            {
                tlpButton.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 100f);
                tlpButton.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 5f);
                tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 100f);

            }
            else
            {
                tlpButton.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpButton.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 100f);
                tlpButton.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 5f);
                tlpButton.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0);
            }

        }

        private void EnableMoveRightButton(bool Enable)
        {
            btnMoveRight.Enabled= Enable;
        }

        private void EnableMoveLeftButton(bool Enable)
        {
            btnMoveLeft.Enabled = Enable;

            

        }

        #endregion
      
        #region Form Action
        private void frmItemAndMouldConfiguration_Load(object sender, EventArgs e)
        {
            if(dgvSuggestionList.DataSource != null)
            {
                dgvSuggestionList.ClearSelection();
            }

            if (dgvMouldList.DataSource != null)
            {
                if(EDIT_MODE == 1)
                {
                    MouldListEditModeChangeColumnColor();
                }

                dgvMouldList.ClearSelection();
            }

            btnMoveLeft.Enabled = false;
            btnMoveRight.Enabled = false;

            ShowSaveButton(false);
        }

        private void dgvSuggestionList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    btnMoveRight.Enabled = true;
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(btnSave.Visible)
            {
                DialogResult result = MessageBox.Show("You have unsaved changes that will be lost if you close this page. Are you sure you want to proceed?", "Unsaved Changes Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
           

        }

       
        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if(HasSelectedRow(dgvSuggestionList))
            {
                int rowIndex = dgvSuggestionList.CurrentCell.RowIndex;

                MoveSuggestMouldToMouldList(dgvSuggestionList.Rows[rowIndex].Cells[text.Header_MouldCode].Value.ToString());
            }
           
        }


        private void frmItemAndMouldConfiguration_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();
        }

        private void tableLayoutPanel10_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();

        }

        private void tableLayoutPanel2_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();

        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();

        }

        private void tableLayoutPanel25_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();

        }

        private void tlpButton_Click(object sender, EventArgs e)
        {
            ClearAllListSelectionIfAny();

        }

        private void dgvSuggestionList_SelectionChanged(object sender, EventArgs e)
        {
            EnableMoveRightButton(HasSelectedRow(dgvSuggestionList));
        }

        private void dgvMouldList_SelectionChanged(object sender, EventArgs e)
        {
            EnableMoveLeftButton(HasSelectedRow(dgvMouldList));

            if (!listReadOnlyModeChanging)
            {
                listReadOnlyModeChanging = true;

                if (dgvMouldList.SelectedCells.Count > 0)  // checking if any cell is selected
                {
                    int rowIndex = dgvMouldList.SelectedCells[0].RowIndex; // Get the row index of first selected cell
                    int columnIndex = dgvMouldList.SelectedCells[0].ColumnIndex; // Get the column index of first selected cell

                    dgvReadOnlyModeUpdate(dgvMouldList, rowIndex, columnIndex);
                }

                listReadOnlyModeChanging = false;
            }
        }

        private void dgvMouldList_MouseClick(object sender, MouseEventArgs e)
        {
            var hti = dgvMouldList.HitTest(e.X, e.Y);

            if (hti.Type == DataGridViewHitTestType.None || hti.Type == DataGridViewHitTestType.ColumnHeader)
            {
                dgvMouldList.ClearSelection();
            }
        }

        private void dgvSuggestionList_MouseClick(object sender, MouseEventArgs e)
        {
            var hti = dgvSuggestionList.HitTest(e.X, e.Y);

            if (hti.Type == DataGridViewHitTestType.None || hti.Type == DataGridViewHitTestType.ColumnHeader)
            {
                dgvSuggestionList.ClearSelection();
            }
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            if (HasSelectedRow(dgvMouldList))
            {
                int rowIndex = dgvMouldList.CurrentCell.RowIndex;

                MoveMouldListToSuggestMould(dgvMouldList.Rows[rowIndex].Cells[text.Header_MouldCode].Value.ToString());

                btnMoveLeft.OnHoverBaseColor1 = AutoFillCellColor;
                btnMoveLeft.OnHoverBaseColor2 = AutoFillCellColor;
                btnMoveLeft.OnHoverBorderColor = Color.Black;

               

            }
        }

        private void btnMoveLeft_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void btnMoveLeft_MouseEnter(object sender, EventArgs e)
        {
            if(btnMoveLeft.Enabled)
            {
                btnMoveLeft.OnHoverBaseColor1 = Color.FromArgb(155, 145, 221);
                btnMoveLeft.OnHoverBaseColor2 = Color.FromArgb(255, 85, 255);
                btnMoveLeft.OnHoverBorderColor = Color.Black;
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
                string itemDescription = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

                if(!string.IsNullOrEmpty(itemDescription))
                {
                    try
                    {
                        if (EDIT_MODE == 0)
                        {
                            if (!MouldListEditMode)
                            {
                                my_menu.Items.Add(text.EditMode_Open).Name = text.EditMode_Open;

                            }
                            else
                            {
                                my_menu.Items.Add(text.EditMode_Close).Name = text.EditMode_Close;

                            }
                        }

                        my_menu.Items.Add(text.AddingItem).Name = text.AddingItem;
                        my_menu.Items.Add(text.AddingNewMould).Name = text.AddingNewMould;
                        my_menu.Items.Add(text.AddingExistingMould).Name = text.AddingExistingMould;
                        //my_menu.Items.Add(text.AutoSearchFamilyItem).Name = text.AutoSearchFamilyItem;

                        if (!itemDescription.Contains(text.All_Item))
                            my_menu.Items.Add(text.RemoveItem).Name = text.RemoveItem;

                        my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                        contextMenuStrip1 = my_menu;
                        my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
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
            int rowIndex = dgv.CurrentCell.RowIndex;

            string mouldCode = dgv.Rows[rowIndex].Cells[text.Header_MouldCode].Value.ToString();
            //string jobNo = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[text.Header_JobNo].Value.ToString();
            //string sheetNo = "";

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.EditMode_Open))
            {
                MouldListEditMode = true;

                MouldListEditModeChangeColumnColor();
            }
            else if (itemClicked.Equals(text.EditMode_Close))
            {
                MouldListEditMode = false;

                MouldListEditModeChangeColumnColor();
            }
            else if (itemClicked.Equals(text.AddingItem))
            {
                DataTable dt = dalItem.CatSearch(text.Cat_Part);

                DataTable dt_MouldList = (DataTable)dgvMouldList.DataSource;

                List<string> itemsToRemove = new List<string>();

                foreach (DataRow row in dt_MouldList.Rows)
                {
                    string itemDescription = row[text.Header_ItemDescription].ToString();

                    if (!itemDescription.Contains(text.All_Item))
                    {
                        string itemCode = row[text.Header_ItemCode].ToString();

                        // Add to list of items to remove
                        itemsToRemove.Add(itemCode);
                    }
                }

                if (itemsToRemove.Count > 0)
                {
                    // Only call AcceptChanges once, improving performance
                    dt.AcceptChanges();

                    // Use LINQ to efficiently filter out the rows
                    var rows = dt.AsEnumerable()
                        .Where(r => !itemsToRemove.Contains(r.Field<string>(dalItem.ItemCode)));

                    // Convert filtered enumerable back to DataTable
                    dt = rows.CopyToDataTable();
                }

                frmItemSearch frm = new frmItemSearch(dt, text.Cat_Part);

                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.WindowState = FormWindowState.Normal;
                //frm.Size = new Size(1650, 900);
                frm.ShowDialog();

                frmLoading.CloseForm();

                if(frmItemSearch.ITEM_CODE_SELECTED != "")
                {
                    // add item to same mould code area, pro info
                    // update mould item count
                    AddItemToMouldList(mouldCode, frmItemSearch.ITEM_CODE_SELECTED);

                }
            }
            else if (itemClicked.Equals(text.RemoveItem))
            {
                OkToProcessListCellFormatting = false;

                DataTable dt_MouldList = (DataTable)dgvMouldList.DataSource;

                if (dt_MouldList.Rows.Count > rowIndex) // check if the row index exists in the table
                {
                    dt_MouldList.Rows.RemoveAt(rowIndex); // remove the row
                }


                ResetAllItemCounting();

                OkToProcessListCellFormatting = true;
                ListCellFormatting(dgvMouldList);
                ShowSaveButton(true);

            }
            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm save?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Save
                if(SaveMouldData())
                {
                    MessageBox.Show("Data Saved!");


                    Close();
                }
            }

        }

        #endregion

        private bool listReadOnlyModeChanging = false;

            
        private void dgvReadOnlyModeUpdate(DataGridView dgv, int rowIndex, int colIndex)
        {
            if (rowIndex >= 0 && rowIndex < dgv.Rows.Count && colIndex >= 0 && colIndex < dgv.Columns.Count)
            {
                string itemDescription = dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Value.ToString();
                bool masterRow = itemDescription.Contains(text.All_Item);
                string colName = dgvMouldList.Columns[colIndex].Name;
                bool readOnlyMode = false;

                if (masterRow && !text.Header_ProTon.Equals(colName) && !text.Header_ProRwShot.Equals(colName) && !text.Header_ProCT.Equals(colName))
                {
                    readOnlyMode = true;
                }

                if (!masterRow && !text.Header_Cavity.Equals(colName) && !text.Header_ProPwShot.Equals(colName))
                {
                    readOnlyMode = true;
                }

                if (readOnlyMode)
                {
                    dgvMouldList.ReadOnly = true; // Prevent user interaction with the rest of the DataGridView

                    // Change back to full row selection mode for any other column.
                    dgvMouldList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvMouldList.CurrentCell = dgvMouldList.Rows[rowIndex].Cells[colIndex];
                    dgvMouldList.Rows[rowIndex].Cells[colIndex].Selected = true;
                }
                else
                {
                    // Change to cell selection mode.
                    dgvMouldList.ReadOnly = false; // Temporarily allow user interaction

                    dgvMouldList.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgvMouldList.CurrentCell = dgvMouldList.Rows[rowIndex].Cells[colIndex];
                    dgvMouldList.Rows[rowIndex].Cells[colIndex].Selected = true;
                }
            }
          
        }

        private void dgvMouldList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!listReadOnlyModeChanging)
            {
                listReadOnlyModeChanging = true;
                dgvReadOnlyModeUpdate(dgvMouldList, e.RowIndex, e.ColumnIndex);
                listReadOnlyModeChanging = false;
            }

            //string itemDescription = dgvMouldList.Rows[e.RowIndex].Cells[text.Header_ItemDescription].Value.ToString();
            //bool masterRow = itemDescription.Contains(text.All_Item);
            //string colName = dgvMouldList.Columns[e.ColumnIndex].Name;
            //bool readOnlyMode = false;

            //if (masterRow && !text.Header_ProTon.Equals(colName) && !text.Header_ProRwShot.Equals(colName) && !text.Header_ProCT.Equals(colName))
            //{
            //    readOnlyMode = true;
            //}

            //if (!masterRow && !text.Header_Cavity.Equals(colName) && !text.Header_ProPwShot.Equals(colName))
            //{
            //    readOnlyMode = true;
            //}

            //if (readOnlyMode)
            //{
            //    dgvMouldList.ReadOnly = true; // Prevent user interaction with the rest of the DataGridView

            //    // Change back to full row selection mode for any other column.
            //    dgvMouldList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //    dgvMouldList.CurrentCell = dgvMouldList.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //    dgvMouldList.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            //}
            //else
            //{
            //    // Change to cell selection mode.
            //    dgvMouldList.ReadOnly = false; // Temporarily allow user interaction

            //    dgvMouldList.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //    dgvMouldList.CurrentCell = dgvMouldList.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //    dgvMouldList.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            //}
           
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

            TextBox tb = sender as TextBox;

            if (tb != null)
            {
                // If it's not a number, not a control key (like backspace), and it's not the first decimal point
                if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.' || tb.Text.Contains('.')))
                {
                    e.Handled = true;
                }
            }
        }

        private void dgvMouldList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (e.Control is TextBox tb)
            {
                // Unsubscribe the event handlers
                tb.KeyPress -= IntegerOnly_KeyPress;
                tb.KeyPress -= FloatOnly_KeyPress;
                tb.KeyPress -= CannotEdit_KeyPress;

                var backColor = dgv.CurrentCell.Style.BackColor.Name;

                // Determine the column type
                string colName = dgv.CurrentCell.OwningColumn.Name;

                var integerColumns = new HashSet<string> { text.Header_ProTon, text.Header_Cavity, text.Header_ProCT };
                var floatColumns = new HashSet<string> { text.Header_ProPwShot, text.Header_ProRwShot };

                if(backColor == DisableEditCellColor.ToString() || backColor == AutoFillCellColor.ToString())
                {
                    tb.KeyPress += CannotEdit_KeyPress;
                }
                else
                {
                    if (integerColumns.Contains(colName))
                    {
                        tb.KeyPress += IntegerOnly_KeyPress;
                    }
                    else if (floatColumns.Contains(colName))
                    {
                        tb.KeyPress += FloatOnly_KeyPress;
                    }
                    else
                    {
                        tb.KeyPress += CannotEdit_KeyPress;
                    }
                }
            }
        }

        private void dgvMouldList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ShowSaveButton(true);

            //update master row data : cavity, part weight per shot

            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            string colName = dgvMouldList.Columns[colIndex].Name;
            string mouldCode = dgvMouldList.Rows[rowIndex].Cells[text.Header_MouldCode].Value.ToString();

            listReadOnlyModeChanging = true;

            ResetAllItemCounting(mouldCode, colName);

            listReadOnlyModeChanging = false;

            if (!listReadOnlyModeChanging)
            {
                listReadOnlyModeChanging = true;

                if (dgvMouldList.SelectedCells.Count > 0)  // checking if any cell is selected
                {
                    dgvReadOnlyModeUpdate(dgvMouldList, rowIndex, colIndex);
                }

                listReadOnlyModeChanging = false;
            }
        }

        private void dgvMouldList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //int rowIndex = e.RowIndex;
            //int colIndex = e.ColumnIndex;
            //DataGridView dgv = dgvMouldList;

            //if (dgv.Columns[colIndex].Name == text.Header_ItemDescription && dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
            //{
            //    dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            //    dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold | FontStyle.Italic);
            //}
        }

        private bool OkToProcessListCellFormatting = true;

        private void dgvMouldList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if(OkToProcessListCellFormatting)
                ListCellFormatting(dgvMouldList);

        }
    }
}
