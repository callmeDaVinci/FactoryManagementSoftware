using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Statistics.Kernels;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.ReportingServices.Interfaces;
using Syncfusion.XlsIO;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using Syncfusion.XlsIO.Parser.Biff_Records;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FactoryManagementSoftware.UI
{
    public partial class frmPlanningVer2dot1 : Form
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

        private DataTable DT_ITEM_LIST;

        bool CMB_CODE_READY = false;
        bool PRO_INFO_LOADING = false;

        readonly private string BTN_INFO_SAVE_CONFIRM_MOULD = "INFO SAVE & MOULD CONFIRM";
        readonly private string BTN_CONFIRM_MOULD = "MOULD CONFIRM";


        private int MIN_SHOT = 0;

        private bool PRO_INFO_CHANGE = false;
        #endregion

        public frmPlanningVer2dot1()
        {
            //Testing();
            InitializeComponent();
            InitialSetting();

            LoadDB();
        }

        private void frmPlanning_Load(object sender, EventArgs e)
        {
            
        }

        private DataTable NewItemList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_MouldCode, typeof(string));
            dt.Columns.Add(text.Header_ProTon, typeof(string));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_Cavity, typeof(string));
            dt.Columns.Add(text.Header_ProCT, typeof(string));
            dt.Columns.Add(text.Header_ProPwShot, typeof(string));
            dt.Columns.Add(text.Header_ProRwShot, typeof(string));
            dt.Columns.Add(text.Header_TargetQty, typeof(int));
            dt.Columns.Add(text.Header_AutoQtyAdjustment, typeof(int));

            return dt;
        }

        private DataTable NewRawMaterialList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_ItemSelection, typeof(bool));
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_BAG, typeof(int));

            dt.Columns.Add(text.Header_KGPERBAG, typeof(decimal));
            dt.Columns.Add(text.Header_KG, typeof(decimal));

            dt.Columns.Add(text.Header_Remark, typeof(string));

            return dt;
        }

        private DataTable NewColorMaterialList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_ItemSelection, typeof(bool));
            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_ItemDescription, typeof(string));
            dt.Columns.Add(text.Header_Color, typeof(string));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_Percentage, typeof(decimal));
            dt.Columns.Add(text.Header_KG, typeof(decimal));
            dt.Columns.Add(text.Header_Remark, typeof(string));

            return dt;
        }

        private DataTable NewMatSummaryList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_Description, typeof(string));
            dt.Columns.Add(text.Header_Qty, typeof(string));
            dt.Columns.Add(text.Header_Remark, typeof(string));

            return dt;
        }
        #region UI/UX

        private void InitialSetting()
        {
            ShowBtnItemSave(false);
            StepsUIUpdate(1);
            MIN_SHOT = 0;
        }

        private void btnSaveProductionInfoAndMouldConfirmMode(bool active)
        {

        }

        private void NextToMachineSelection()
        {
            btnNextToMachineSelection.Visible = false;

            tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
            tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);
        }

        private void BackToPartInfo()
        {
            //btnNextToMachineSelection.Visible = true;

            //tlpMainPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
            //tlpMainPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);
        }

        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            int smallColumnWidth = 60;
            if (dgv == dgvItemList)
            {
                //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_TargetQty].HeaderCell.Style.BackColor = Color.FromArgb(255, 153, 153);

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

                dgv.Columns[text.Header_MouldCode].Width = smallColumnWidth;
                dgv.Columns[text.Header_Cavity].Width = smallColumnWidth;
                //dgv.Columns[text.Header_MouldSelection].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ItemSelection].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_MouldCode].Width = ProductionInfoColumnWidth;
                //dgv.Columns[text.Header_ProTon].Width = ProductionInfoColumnWidth;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;

                //dgv.Columns[text.Header_Cavity].Visible = false;
                dgv.Columns[text.Header_ProTon].Visible = false;
                dgv.Columns[text.Header_ProCT].Visible = false;
                //dgv.Columns[text.Header_ProPwShot].Visible = false;
                dgv.Columns[text.Header_ProRwShot].Visible = false;


                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else if (dgv == dgvRawMatList)
            {
                //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_BAG].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_KG].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_KGPERBAG].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                

                dgv.Columns[text.Header_ItemSelection].Width = smallColumnWidth;
                dgv.Columns[text.Header_Index].Width = smallColumnWidth;
                dgv.Columns[text.Header_KG].Width = smallColumnWidth;
                dgv.Columns[text.Header_KGPERBAG].Width = smallColumnWidth;
                dgv.Columns[text.Header_BAG].Width = smallColumnWidth;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
            }
            else if (dgv == dgvColorMatList)
            {
                //dgv.Columns[text.Header_MouldCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_ItemDescription].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgv.Columns[text.Header_ItemDescription].MinimumWidth = 100;
                dgv.Columns[text.Header_ItemDescription].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[text.Header_Percentage].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_KG].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_Color].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                dgv.Columns[text.Header_Remark].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                int columnWidth = 60;

                dgv.Columns[text.Header_ItemSelection].Width = columnWidth;
                dgv.Columns[text.Header_Index].Width = columnWidth;
                dgv.Columns[text.Header_KG].Width = columnWidth;
                dgv.Columns[text.Header_Percentage].Width = columnWidth;
                dgv.Columns[text.Header_Color].Width = columnWidth;

                dgv.Columns[text.Header_ItemCode].Visible = false;
                dgv.Columns[text.Header_ItemName].Visible = false;
            }
        }

        #endregion

        #region Data Loading

        private void IndexReset(DataGridView dgv)
        {
            int index = 1;

            if (dgv?.Rows.Count > 0)
            {
                DataTable dt = (DataTable)dgv.DataSource;

                if(dt.Columns.Contains(text.Header_Index))
                {
                    
                    foreach(DataRow row in dt.Rows)
                    {
                        row[text.Header_Index] = index++;
                    }

                    
                }
                else
                {
                    MessageBox.Show("Index column not found , cannot reset index.");
                }
            }

            if(index == 1)
            {
                btnItemInfoSave.Visible = false;
            }    
        }

        private void loadRawMaterialList()
        {
            
        }

        private void LoadSingleMaterialList(string itemCode)
        {
            if (DT_ITEM == null)
            {
                DT_ITEM = dalItem.Select();
            }

            DataTable dt_RawMat = NewRawMaterialList();
            DataTable dt_ColorMat = NewColorMaterialList();

            int rawIndex = 1;
            int colorIndex = 1;

            foreach (DataRow row in DT_ITEM.Rows)
            {
                if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                {

                    string rawMaterial = row[dalItem.ItemMaterial].ToString();

                    var newRawRow = dt_RawMat.NewRow();

                    newRawRow[text.Header_ItemSelection] = true;
                    newRawRow[text.Header_Index] = rawIndex++;
                    newRawRow[text.Header_ItemDescription] = rawMaterial;
                    newRawRow[text.Header_ItemCode] = rawMaterial;
                    newRawRow[text.Header_KGPERBAG] = 25;

                    dt_RawMat.Rows.Add(newRawRow);

                    dgvRawMatList.DataSource = dt_RawMat;
                    dgvUIEdit(dgvRawMatList);
                    RawMatListCellFormatting(dgvRawMatList);
                    dgvRawMatList.ClearSelection();

                    string colorMaterial = row[dalItem.ItemMBatch].ToString();
                    string colorName = row[dalItem.ItemColor].ToString();
                    decimal colorRate = decimal.TryParse(row[dalItem.ItemMBRate].ToString(), out colorRate) ? colorRate : 0;

                    if (colorRate < 1)
                    {
                        colorRate *= 100;
                    }

                    colorRate = decimal.Round(colorRate,0);

                    var newColorRow = dt_ColorMat.NewRow();
                    newColorRow[text.Header_ItemSelection] = true;
                    newColorRow[text.Header_Index] = colorIndex++;
                    newColorRow[text.Header_ItemDescription] = colorMaterial;
                    newColorRow[text.Header_ItemCode] = colorMaterial;
                    newColorRow[text.Header_Color] = colorName;
                    newColorRow[text.Header_Percentage] = colorRate;

                    dt_ColorMat.Rows.Add(newColorRow);
                    dgvColorMatList.DataSource = dt_ColorMat;
                    dgvUIEdit(dgvColorMatList);
                    ColorMatListCellFormatting(dgvColorMatList);
                    dgvColorMatList.ClearSelection();


                    break;

                }
            }
        }

        private void TotalMaterialCalculation()
        {
            #region info from user
            bool RunnerRecycle = true;
            bool RawMat_RoundUpToBag = true;

            double colorRatio = 0.01;
            double newMatWastage = 0.05;
            double recycleWastage = 0;
            double KGperBag = 25;

            int minShots = 100;

            double PW_Shot = 4;
            double RW_Shot = 6;

            //double PW_Shot = double.TryParse(txtPWPerShot.Text, out PW_Shot) ? PW_Shot : 0;
            //double RW_Shot = double.TryParse(txtRWPerShot.Text, out RW_Shot) ? RW_Shot : 0;

            double TotalWeight_Shot = PW_Shot + RW_Shot;

            colorRatio = colorRatio >= 1 ? colorRatio /= 100 : colorRatio;
            newMatWastage = newMatWastage >= 1 ? newMatWastage /= 100 : newMatWastage;
            recycleWastage = recycleWastage >= 1 ? recycleWastage /= 100 : recycleWastage;
            #endregion

            #region Calculation

            //TO:DO Calculate the below result
            double TotalNewRawMat = 0;
            double TotalNewColorMat = 0;
            double TotalMat = 0;

            int maxShots = minShots;

            double TotalRecycleMat = 0;
            int extraShotGetFromRecycle = 0;

            if (RunnerRecycle)
            {
                TotalRecycleMat = (minShots - 1) * RW_Shot * (1 - recycleWastage);

                extraShotGetFromRecycle = (int)Math.Floor(TotalRecycleMat / TotalWeight_Shot);

                maxShots += extraShotGetFromRecycle;
            }

            TotalMat = minShots * TotalWeight_Shot;

            double TotalNewMat = 0;
            
            TotalNewMat  = TotalMat - TotalRecycleMat;

            //TotalNewMat = TotalNewRawMat + TotalNewRawMat * colorRatio
            TotalNewRawMat = TotalNewMat / (1 + colorRatio);
            TotalNewColorMat = TotalNewRawMat * colorRatio;

            double Prepare_RawMat = TotalNewRawMat * (1 + newMatWastage);
            double Prepare_ColorMat = Prepare_RawMat * colorRatio;

            maxShots = (int) Math.Floor(TotalMat / TotalWeight_Shot);

            if (RawMat_RoundUpToBag)
            {
                Prepare_RawMat = Math.Ceiling(Prepare_RawMat/ 1000 / KGperBag) * KGperBag * 1000;

                Prepare_ColorMat = Prepare_RawMat * colorRatio;

                double RoundUp_Pro_RawMat = Prepare_RawMat * ( 1 - newMatWastage );

                double RoundUp_Pro_ColorMat = RoundUp_Pro_RawMat * colorRatio;

                double Total_RoundUp_Pro_New_Mat = RoundUp_Pro_RawMat + RoundUp_Pro_ColorMat;

                maxShots = (int) Math.Floor(Total_RoundUp_Pro_New_Mat / TotalWeight_Shot);

                if (RunnerRecycle)
                {
                    TotalRecycleMat = (maxShots - 1) * RW_Shot * (1 - recycleWastage);

                    extraShotGetFromRecycle = (int)Math.Floor(TotalRecycleMat / TotalWeight_Shot);

                    maxShots += extraShotGetFromRecycle;
                }

                TotalMat = Prepare_RawMat + Prepare_ColorMat + TotalRecycleMat;
            }

            #endregion
        }

        private void AddItemToList(string itemCode)
        {
            PRO_INFO_LOADING = true;

            if (!string.IsNullOrEmpty(itemCode))
            {
                if (DT_ITEM == null)
                {
                    DT_ITEM = dalItem.Select();
                }


                int index = 1;

                if (dgvItemList?.Rows.Count > 0)
                {
                    DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                    //get last index
                    index = int.TryParse(dgvItemList.Rows[dgvItemList.Rows.Count - 1].Cells[text.Header_Index].Value.ToString(), out index)? index++ : 1 ;
                }
                else
                {
                    DT_ITEM_LIST = NewItemList();
                }

                
                bool itemFound = false;

                foreach (DataRow row in DT_ITEM_LIST.Rows)
                {
                    if (itemCode == row[text.Header_ItemCode].ToString())
                    {
                        itemFound = true;
                        int rowIndex = DT_ITEM_LIST.Rows.IndexOf(row);

                        MessageBox.Show("Same item code found in the list (row: " + rowIndex + ").");
                        return;
                    }
                }

                if (!itemFound)
                    foreach (DataRow row in DT_ITEM.Rows)
                    {
                        if (itemCode == row[dalItem.ItemCode].ToString())
                        {
                            DT_ITEM_LIST.Rows.Add(CreateNewItemRow(row, index++));

                            itemFound = true;

                            break;
                        }
                    }

                if(itemFound)
                {
                    dgvItemList.DataSource = DT_ITEM_LIST;
                    IndexReset(dgvItemList);
                    dgvUIEdit(dgvItemList);
                    ItemListCellFormatting(dgvItemList);

                    LoadSummary_ProductionInfo();

                    dgvReadOnlyModeUpdate(dgvItemList, dgvItemList.Rows.Count - 1, dgvItemList.Columns[text.Header_TargetQty].Index);
                }
                else
                {
                    DT_ITEM = dalItem.Select();
                    AddItemToList(itemCode);
                }
            }

            PRO_INFO_LOADING = false;

        }

        private void RemoveItemFromList(DataGridView dgv ,int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dgv.Rows.Count) // Ensure the index is within the valid range
            {
                dgv.Rows.RemoveAt(rowIndex);
                LoadSummary_ProductionInfo();
                IndexReset(dgvItemList);

            }
            else
            {
                throw new ArgumentOutOfRangeException("rowIndex", "The row index provided is out of range.");
            }
        }
        private DataRow CreateNewItemRow(DataRow row, int index)
        {
            DataRow newRow = DT_ITEM_LIST.NewRow();

            string itemCode = row[dalItem.ItemCodePresent].ToString();
            itemCode = string.IsNullOrEmpty(itemCode) ? row[dalItem.ItemCode].ToString() : itemCode;

            string itemName = row[dalItem.ItemName].ToString();

            string itemDescription = itemName + "\n(" + itemCode + ")";

            newRow[text.Header_Index] = index;
            newRow[text.Header_ItemDescription] = itemDescription;
            newRow[text.Header_ItemCode] = itemCode;
            newRow[text.Header_ItemName] = itemName;
            newRow[text.Header_Cavity] = row[dalItem.ItemCavity].ToString();
            newRow[text.Header_ProCT] = row[dalItem.ItemProCTTo].ToString();
            newRow[text.Header_ProPwShot] = row[dalItem.ItemProPWShot].ToString();
            newRow[text.Header_ProRwShot] = row[dalItem.ItemProRWShot].ToString();

            return newRow;
        }

        private void LoadSummary_ProductionInfo()
        {
            ClearProductionInfo();

            if(dgvItemList?.Rows.Count > 0)
            {
                DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                int totalCavity = 0;
                int totalShot = 0;
                decimal cycleTime = 0;

                decimal partWeightPerShot = 0;
                decimal runnerWeightPerShot = 0;

                foreach (DataRow row in DT_ITEM_LIST.Rows)
                {
                    totalCavity += int.TryParse(row[text.Header_Cavity].ToString(), out int cavity) ? cavity : 0;
                    partWeightPerShot += decimal.TryParse(row[text.Header_ProPwShot].ToString(), out decimal pwPerShot) ? pwPerShot : 0;

                    decimal rwPerShot = decimal.TryParse(row[text.Header_ProRwShot].ToString(), out rwPerShot) ? rwPerShot : 0;

                    if (rwPerShot > runnerWeightPerShot)
                    {
                        runnerWeightPerShot = rwPerShot;
                    }


                    decimal ct = decimal.TryParse(row[text.Header_ProCT].ToString(), out ct)? ct : 0;

                    if(ct > cycleTime)
                    {
                        cycleTime = ct;
                    }

                    int targetQty = int.TryParse(row[text.Header_AutoQtyAdjustment].ToString(), out targetQty) ? targetQty : 0;

                    if(cavity > 0)
                    {
                        int shotQty = targetQty / cavity;

                        if(shotQty > totalShot)
                        {
                            totalShot = shotQty;
                        }
                    }
                }

                txtCavity.Text = totalCavity.ToString();
                txtCycleTime.Text = cycleTime.ToString("0");
                txtPWPerShot.Text = partWeightPerShot.ToString("0.##");
                txtRWPerShot.Text = runnerWeightPerShot.ToString("0.##");
                MIN_SHOT = (int)totalShot;



            }

        }
        private void LoadProductionInfoToField(DataRow[] existingRows)
        {
            txtCavity.Text = existingRows[0][dalItem.MouldCavity].ToString();
            txtCycleTime.Text = existingRows[0][dalItem.MouldCT].ToString();
            txtPWPerShot.Text = existingRows[0][dalItem.ItemPWShot].ToString();
            txtRWPerShot.Text = existingRows[0][dalItem.ItemRWShot].ToString();


        }

        private void ClearProductionInfo()
        {
            txtCavity.Text = "";
            txtCycleTime.Text = "";
            txtPWPerShot.Text = "";
            txtRWPerShot.Text = "";
            MIN_SHOT = 0;
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

        private void AddItem()
        {
            DataTable dt = dalItem.CatSearch(text.Cat_Part);

            List<string> itemsToRemove = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                string itemName = row[dalItem.ItemName].ToString();

                if (itemName.ToUpper().Contains(text.Terminated.ToUpper()))
                {
                    string itemCode = row[dalItem.ItemCode].ToString();

                    // Add to list of items to remove
                    itemsToRemove.Add(itemCode);
                }
            }

            if(dgvItemList?.Rows.Count > 0)
            {
                DataTable dt_itemList = (DataTable)dgvItemList.DataSource;

                foreach(DataRow row in dt_itemList.Rows)
                {
                    string itemCode = row[text.Header_ItemCode].ToString();
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
            frm.ShowDialog();

            frmLoading.CloseForm();

            if (frmItemSearch.ITEM_CODE_SELECTED != "")
            {
                AddItemToList(frmItemSearch.ITEM_CODE_SELECTED);
            }
        }
        private void frmPlanningNEWV2_Shown(object sender, EventArgs e)
        {
            //load item search page
            AddItem();
        }

        private void ItemListCellFormatting(DataGridView dgv)
        {
            if(dgv?.Rows.Count > 0)
            {
                dgv.SuspendLayout();

                DataTable dt = (DataTable)dgv.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    string itemDescription = row[text.Header_ItemDescription].ToString();

                   
                    //default back color
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_Cavity].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_ProPwShot].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_TargetQty].Style.BackColor = SystemColors.Info;

                }

                dgv.ResumeLayout();
            }
          
        }

        private void RawMatListCellFormatting(DataGridView dgv)
        {
            if (dgv?.Rows.Count > 0)
            {
                dgv.SuspendLayout();

                DataTable dt = (DataTable)dgv.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    //default back color
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_BAG].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_KGPERBAG].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_Remark].Style.BackColor = SystemColors.Info;
                }

                dgv.ResumeLayout();
            }

        }

        private void ColorMatListCellFormatting(DataGridView dgv)
        {
            if (dgv?.Rows.Count > 0)
            {
                dgv.SuspendLayout();

                DataTable dt = (DataTable)dgv.DataSource;
                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dt.Rows.IndexOf(row);

                    //default back color
                    dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;

                    //able to let user edit the cell
                    dgv.Rows[rowIndex].Cells[text.Header_Percentage].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_Color].Style.BackColor = SystemColors.Info;
                    dgv.Rows[rowIndex].Cells[text.Header_Remark].Style.BackColor = SystemColors.Info;
                }

                dgv.ResumeLayout();
            }

        }
        private void dgvItemList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //int rowIndex = e.RowIndex;
            //int colIndex = e.ColumnIndex;
            //DataGridView dgv = dgvItemList;

            //if (dgv.Columns[colIndex].Name == text.Header_ItemDescription && dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
            //{
            //    string itemDescription = dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Value.ToString();

            //    if(string.IsNullOrEmpty(itemDescription))
            //    {
            //        //dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Black;
            //        //dgv.Rows[rowIndex].Height = 20;
            //    }
            //    else
            //    {
            //        //dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
            //        //dgv.Rows[rowIndex].Height = 50;

            //        dgv.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            //        dgv.Rows[rowIndex].Cells[text.Header_ItemDescription].Style.Font = new Font("Segoe UI", 6F, FontStyle.Bold | FontStyle.Italic);
            //    }
               
            //}
           
           
        }

        private void tableLayoutPanel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvItemList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //foreach (DataGridViewRow row in dgvItemList.Rows)
            //{
            //    //DataGridViewCheckBoxCell checkBoxCell = row.Cells[text.Header_MouldSelection] as DataGridViewCheckBoxCell;
            //    //if (row.Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item))
            //    //{
            //    //    checkBoxCell.ReadOnly = false;  // allow checkbox to be checked/unchecked
            //    //}
            //    //else
            //    //{
            //    //    checkBoxCell.ReadOnly = true;  // disable checkbox
            //    //    //checkBoxCell.Style.BackColor = Color.DimGray;  // indicate disabled state
            //    //}
            //}
        }

        private void dgvItemList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        //    if (e.ColumnIndex == dgvItemList.Columns[text.Header_MouldSelection].Index &&
        //e.RowIndex >= 0 &&
        //dgvItemList.Rows[e.RowIndex].Cells[text.Header_ItemDescription].Value.ToString().Contains(text.All_Item) == false)
        //    {
        //        e.PaintBackground(e.CellBounds, true);
        //        e.Handled = true;
        //    }

        //    if (e.ColumnIndex == dgvItemList.Columns[text.Header_ItemSelection].Index &&
        //e.RowIndex >= 0 &&
        //string.IsNullOrEmpty(dgvItemList.Rows[e.RowIndex].Cells[text.Header_ItemDescription].Value.ToString()))
        //    {
        //        e.PaintBackground(e.CellBounds, true);
        //        e.Handled = true;
        //    }
        }

        private void dgvReadOnlyModeUpdate(DataGridView dgv, int rowIndex, int colIndex)
        {
            listReadOnlyModeChanging = true;

            if (rowIndex >= 0 && rowIndex < dgv.Rows.Count && colIndex >= 0 && colIndex < dgv.Columns.Count)
            {
                string colName = dgv.Columns[colIndex].Name;

                bool readOnlyMode = true;

                if (colName.Equals(text.Header_TargetQty) || colName.Equals(text.Header_Cavity) || colName.Equals(text.Header_ProPwShot))
                {
                    readOnlyMode = false;
                }

                if (readOnlyMode)
                {
                    dgv.ReadOnly = true; // Prevent user interaction with the rest of the DataGridView
                    // Change back to full row selection mode for any other column.
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.CurrentCell = dgv.Rows[rowIndex].Cells[colIndex];
                    dgv.Rows[rowIndex].Cells[colIndex].Selected = true;
                }
                else
                {
                    // Change to cell selection mode.
                    dgv.ReadOnly = false; // Temporarily allow user interaction

                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.CurrentCell = dgv.Rows[rowIndex].Cells[colIndex];
                    dgv.Rows[rowIndex].Cells[colIndex].Selected = true;
                }
            }

            listReadOnlyModeChanging = false;

        }

        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!listReadOnlyModeChanging)
                dgvReadOnlyModeUpdate(dgvItemList,e.RowIndex, e.ColumnIndex);
        }
        private bool userInitiatedChange = true;

        private void refreshCavityMatchedQty()
        {
            if(dgvItemList?.Rows.Count > 0)
            {
                DT_ITEM_LIST = (DataTable)dgvItemList.DataSource;

                decimal totalShot = 0;

                foreach(DataRow row in DT_ITEM_LIST.Rows)
                {
                    decimal targetQty = decimal.TryParse(row[text.Header_TargetQty].ToString(), out targetQty) ? targetQty : 0;
                    decimal cavity = decimal.TryParse(row[text.Header_Cavity].ToString(), out cavity) ? cavity : 0;
                    decimal cavityMatchedQty = 0;

                    if(cavity > 0)
                    {
                        cavityMatchedQty = Math.Ceiling(targetQty / cavity);

                        if(cavityMatchedQty > totalShot)
                        {
                            totalShot = cavityMatchedQty;
                        }
                        cavityMatchedQty *= cavity;
                    }


                    row[text.Header_AutoQtyAdjustment] = (int) cavityMatchedQty;
                }

                MIN_SHOT = (int) totalShot;
            }
        }

        private void StepsUIUpdate(int step)
        {
            //// reset all fonts to normal
            //ResetFonts();

            //Font focusFont = new Font("Segoe UI", 8F, FontStyle.Bold);
            //// update the font for the current step
            //switch (step)
            //{
            //    case 1:
            //        gbItem.Font = focusFont;
            //        break;
            //    case 2:
            //        gbRawColorMat.Font = focusFont;
            //        break;
            //    case 3:
            //        gbTime.Font = focusFont;
            //        break;
            //    case 4:
            //        gbStockCheck.Font = focusFont;
            //        break;
            //    case 5:
            //        gbMachineSchedule.Font = focusFont;
            //        break;
            //}
        }

        private void ResetFonts()
        {
            Font normalFont = new Font("Segoe UI", 6F, FontStyle.Bold);

            gbItem.Font = normalFont;
            gbRawColorMat.Font = normalFont;
            gbTime.Font = normalFont;
            gbStockCheck.Font = normalFont;
            gbMachineSchedule.Font = normalFont;
        }

        private void dgvItemList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (!PRO_INFO_LOADING && (e.ColumnIndex == dgvItemList.Columns[text.Header_Cavity].Index || e.ColumnIndex == dgvItemList.Columns[text.Header_ProPwShot].Index))
            {
                ShowBtnItemSave(true);
            }

            PRO_INFO_LOADING = true;

            if (e.ColumnIndex != dgvItemList.Columns[text.Header_TargetQty].Index &&
                e.ColumnIndex != dgvItemList.Columns[text.Header_Cavity].Index &&
                e.ColumnIndex != dgvItemList.Columns[text.Header_ProPwShot].Index)
                return;

            // Ignore any changes that occur programmatically to avoid infinite loops
            if (!userInitiatedChange) return;

            try
            {
                // Temporarily disable changes from triggering this event
                userInitiatedChange = false;

                DataGridViewRow currentRow = dgvItemList.Rows[e.RowIndex];


                if (e.ColumnIndex == dgvItemList.Columns[text.Header_TargetQty].Index)
                {
                    refreshCavityMatchedQty();
                }
                else if (e.ColumnIndex == dgvItemList.Columns[text.Header_Cavity].Index || e.ColumnIndex == dgvItemList.Columns[text.Header_TargetQty].Index || e.ColumnIndex == dgvItemList.Columns[text.Header_ProPwShot].Index)
                {
                    LoadSummary_ProductionInfo();
                }

               

            }
            finally
            {
                // Allow changes to trigger this event again
                userInitiatedChange = true;
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
            //// If the clicked column is "Mould Selection" or "Item Selection".
            //if (e.ColumnIndex == dgvItemList.Columns[text.Header_ItemSelection].Index)
            //{
            //    // Commit the edit immediately for the checkbox cell.
            //    dgvItemList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //}
        }

        private void dgvMouldList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvItemList;

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

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridView dgv = dgvItemList;

            dgv.SuspendLayout();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;
         
            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.RemoveItem))
            {
                RemoveItemFromList(dgv, rowIndex);
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
            dgvItemList.ClearSelection();
        }

        private void tableLayoutPanel26_MouseClick(object sender, MouseEventArgs e)
        {
            dgvItemList.ClearSelection();
        }

        private void ShowBtnItemSave(bool show)
        {
            btnItemInfoSave.Visible = show;
            PRO_INFO_CHANGE = show;
        }
        private void ProductionInfo_TextChanged(object sender, EventArgs e)
        {
            if(!PRO_INFO_LOADING)
            {
                ShowBtnItemSave(true);
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
            //DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();

            //DataTable dt_MouldList = (DataTable) dgvItemList.DataSource;

            //List<Tuple<string, string>> selectedItems = new List<Tuple<string, string>>();

            //foreach (DataRow row in dt_MouldList.Rows)
            //{
            //    bool itemSelected = bool.TryParse(row[text.Header_ItemSelection].ToString(), out bool parsedSelection) ? parsedSelection : false;

            //    if (itemSelected)
            //    {
            //        string itemCode = row.Field<string>(text.Header_ItemCode);
            //        string itemDescription = row[text.Header_ItemDescription].ToString();
            //        string mouldCode = row[text.Header_MouldCode].ToString();

            //        uItem.mould_code = mouldCode;
            //        uItem.item_code = itemCode;

            //        if (!itemDescription.Contains(text.All_Item))
            //            selectedItems.Add(new Tuple<string, string>(mouldCode, itemCode));
            //    }
            //}

            //if (selectedItems.Select(x => x.Item1).Distinct().Count() > 1 && selectedItems.Count > 1)
            //{
            //    throw new Exception("All selected items must have the same mould code");
            //}

            ////update unique production info data
            //uItem.updated_date = DateTime.Now;
            //uItem.updated_by = MainDashboard.USER_ID;
            //uItem.item_updtd_date = uItem.updated_date;
            //uItem.item_updtd_by = uItem.updated_by;
            //uItem.item_added_date = uItem.updated_date;
            //uItem.item_added_by = uItem.updated_by;
            //uItem.removed = false;

            //uItem.mould_cavity = int.TryParse(txtCavity.Text, out int y) ? y : 0;
            //uItem.mould_ct = int.TryParse(txtCycleTime.Text, out y) ? y : 0;
            //uItem.item_pw_shot = float.TryParse(txtPWPerShot.Text, out float k) ? k : 0;
            //uItem.item_rw_shot = float.TryParse(txtRWPerShot.Text, out k) ? k : 0;

            //uItem.MouldDefaultSelection = true;
            //uItem.combination_code = 0;

            //if (selectedItems.Count == 1)
            //{
            //    UpsertUniqueMouldItemData(uItem);

            //}
            //else if (selectedItems.Count > 1)
            //{
            //    bool foundCombination = false;

            //    foreach (var combination in DT_MOULD_ITEM.AsEnumerable()
            //                                                        .Where(r => r.Field<string>(dalItem.MouldCode) == uItem.mould_code && r.Field<int>(dalItem.CombinationCode) != 0)
            //                                                        .GroupBy(r => r.Field<int>(dalItem.CombinationCode)))
            //    {
            //        var itemsInCombination = combination.Select(r => r.Field<string>(dalItem.ItemCode)).ToArray();
            //        var selectedItemCodes = selectedItems.Select(i => i.Item2).ToArray();

            //        if (!selectedItemCodes.Except(itemsInCombination).Any() && !itemsInCombination.Except(selectedItemCodes).Any())
            //        {
            //            uItem.combination_code = combination.Key;
            //            foundCombination = true;
            //            dalItem.MouldItemProInfoUpdateByCombinationCode(uItem);
            //            break;
            //        }
            //    }

            //    if (!foundCombination)
            //    {
            //        // If we haven't found a combination, generate a new combination code
            //        var maxCombinationCode = DT_MOULD_ITEM.AsEnumerable()
            //                                              .Where(r => r.Field<string>("mould_code") == uItem.mould_code)
            //                                              .Max(r => r.Field<int>("combination_code"));

            //        uItem.combination_code = maxCombinationCode + 1;

            //        // Call insert method here
            //        InsertNewCombinationGroup(uItem, selectedItems);
            //    }

            //}
            //else
            //{
            //    ClearProductionInfo();

            //}

            //DT_MOULD_ITEM = dalItem.MouldItemNonRemovedSelect();
        }

        private void Step2_MaterialSetting()
        {
            dgvItemList.ClearSelection();

            StepsUIUpdate(2);
            if (dgvItemList?.Rows.Count == 1)
            {
                string itemCode = dgvItemList.Rows[dgvItemList.Rows.Count - 1].Cells[text.Header_ItemCode].Value.ToString();

                LoadSingleMaterialList(itemCode);
            }
        }
      
        private void label7_Click(object sender, EventArgs e)
        {
            AddItem();

        }

        private void label7_DoubleClick(object sender, EventArgs e)
        {
            AddItem();
        }

        bool listReadOnlyModeChanging = false;

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            if (!listReadOnlyModeChanging)
            {
                listReadOnlyModeChanging = true;

                if (dgvItemList.SelectedCells.Count > 0)  // checking if any cell is selected
                {
                    int rowIndex = dgvItemList.SelectedCells[0].RowIndex; // Get the row index of first selected cell
                    int columnIndex = dgvItemList.SelectedCells[0].ColumnIndex; // Get the column index of first selected cell

                    dgvReadOnlyModeUpdate(dgvItemList, rowIndex, columnIndex);
                }

                listReadOnlyModeChanging = false;
            }
        }

        private void SaveItemProductionInfo(string messageToUser)
        {
            DialogResult dialogResult = MessageBox.Show(messageToUser, "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                itemBLL uItem = new itemBLL();

                if(dgvItemList?.Rows.Count > 0)
                {
                    DataTable dt = (DataTable)dgvItemList.DataSource;

                    foreach(DataRow row in dt.Rows)
                    {
                        string itemCode = row[text.Header_ItemCode].ToString();

                        if (!string.IsNullOrEmpty(itemCode))
                        {
                            uItem.item_code = itemCode;
                            uItem.item_cavity = int.TryParse(txtCavity.Text, out int i) ? i : 0;
                            uItem.item_pro_ct_to = int.TryParse(txtCycleTime.Text, out  i) ? i : 0;
                            uItem.item_pro_pw_shot = int.TryParse(txtPWPerShot.Text, out  i) ? i : 0;
                            uItem.item_pro_rw_shot = int.TryParse(txtRWPerShot.Text, out  i) ? i : 0;
                            uItem.item_updtd_date = DateTime.Now;
                            uItem.item_updtd_by = MainDashboard.USER_ID;

                            if (dalItem.updateItemProductionInfoAndHistoryRecord(uItem))
                            {
                                MessageBox.Show("Data saved!");
                                PRO_INFO_CHANGE = false;
                                btnItemInfoSave.Visible = false;

                                break;
                            }
                        }
                    }
                }

                Cursor = Cursors.Arrow; // change cursor to normal type


            }
        }
        private void btnItemInfoSave_Click(object sender, EventArgs e)
        {
            //check total row in item list
            if(dgvItemList?.Rows.Count > 0)
            {
                if(dgvItemList.Rows.Count < 2)
                {
                    //one item only, save item info
                    SaveItemProductionInfo("Confirm: Save the data?");
                }
                else
                {
                    //save mould_item data
                }
            }
        }
    }
}
