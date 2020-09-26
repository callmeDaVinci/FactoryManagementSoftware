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
    public partial class frmSBBAssemblyPlanning : Form
    {
        public frmSBBAssemblyPlanning()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvItemList, true);
            tool.DoubleBuffered(dgvMatList, true);
            tool.loadFactory(cmbSiteLocation);
            cmbSiteLocation.Text = text.Factory_Store;
        }

        public frmSBBAssemblyPlanning(DataTable dt)
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvItemList, true);
            tool.DoubleBuffered(dgvMatList, true);
            callFromPOVSStock = true;
            tool.loadFactory(cmbSiteLocation);
            cmbSiteLocation.Text = text.Factory_Store;
            //cmbSiteLocation.SelectedIndex = -1;

            LoadItemListFromPlanner(dt);
            //dt_FromPlanner = dt;
            
        }

        #region Object and Variable declare

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        facDAL dalFac = new facDAL();
        pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        SBBDataDAL dalSPP = new SBBDataDAL();
        SBBDataBLL uSpp = new SBBDataBLL();
        historyDAL dalHistory = new historyDAL();
        historyBLL uHistory = new historyBLL();
        planningDAL dalPlan = new planningDAL();
        facStockDAL dalStock = new facStockDAL();
        joinDAL dalJoin = new joinDAL();

        Tool tool = new Tool();
        Text text = new Text();

        DataTable dt_MatStock;
        DataTable dt_MatListForEachItem;
        private int oldTargetBag = 0;

        readonly string header_Index = "#";
        readonly string header_Size = "SIZE INT";
        readonly string header_Unit = "UNIT";
        readonly string header_ItemString = "ITEM";
        readonly string header_Type = "TYPE";
        readonly string header_Code = "CODE";
        readonly string header_StockPcs = "STOCK (PCS)";
        readonly string header_StockBag = "STOCK (BAG)";
        readonly string header_StockString = "STOCK";
        readonly string header_QtyPerBag = "QTY PER BAG";
        readonly string header_TargetBag = "TARGET (BAG)";
        readonly string header_TargetPcs = "TARGET (PCS)";
        readonly string header_MaxAvailability = "MAX (PCS)";
        readonly string header_DateStart = "START";
        readonly string header_DateEnd = "END";
        readonly string header_Mac = "MAC.";
        //readonly string header_OriMaxAvailability = "ORI MAX (PCS)";

        readonly string header_BalAfterDeliveryInPcs = "BAL. AFTER DELIVERY IN PCS";

        //readonly string header_OrigRequiredQty = "ORIG REQUIRED QTY";

        readonly string header_TotalStock = "TOTAL STOCK";
        readonly string header_ParentCode = "PARENT CODE";
        readonly string header_RequiredQty = "REQUIRED QTY";
        readonly string header_RequiredBag = "REQUIRED BAG";
        readonly string header_SiteBalance = "SITE BAL.";
        readonly string header_JoinQty = "JOIN QTY";
        readonly string header_ToDeliverBag = "TO DELIVER (BAG)";
        readonly string header_ToDeliverPCS = "TO DELIVER (PCS)";
        //readonly string header_MinCheck = "MIN CHECK";
        readonly string header_DeliverFrom = "FROM";

        readonly string headerType = "TYPE";

        readonly string unit_Bag = "BAG";
        readonly string unit_BagBushGrip = "BAG(BUSH & GRIP)";
        readonly string unit_Container = "CTR";
        readonly string unit_ContainerBushGrip = "CTR(BUSH & GRIP)";
        readonly string unit_HeaderName = "PACKAGING UNIT";

        //schedule table 
        readonly string header_Status = "STATUS";
        readonly string header_EstimateEnd = "ESTIMATE END";
        readonly string header_PcsPerManHour = "PCS/MAN HOUR";
        readonly string header_Manpower = "MANPOWER";
        readonly string header_HoursPerDay = "HOURS/DAY";
        readonly string header_PlanID = "PLAN ID";
        readonly string header_Qty = "QTY";
        readonly string header_AddedStatus = "ADDED STATUS";

        readonly string headerMatCode = "MATERIAL";
        readonly string headerPlanID = "PLAN";
        readonly string headerFac = "FAC.";
        readonly string headerStart = "START";
        readonly string headerEnd = "END";
        readonly string headerMac = "MAC.";
        readonly string headerFrom = "FROM";
        readonly string headerTransferPending = "TRANSFER PENDING (KG/PIECE)";
        readonly string headerItem = "PLAN FOR";
        readonly string headerPlanToUse = "PLAN TO USE QTY";
        readonly string headerStatus = "STATUS";
        readonly string headerBalance = "BALANCE";
        readonly string headerTransfering = "TRANSFERING";
        readonly string headerUnuse = "UNUSE";

        readonly string headerStillNeed = "STILL NEED (KG/PIECE)";
        readonly string headerStock = "FAC. STOCK (KG/PIECE)";
        readonly string headerTransferred = "TRANSFERRED (KG/PIECE)";

        readonly string text_OutOfStock = "OUT OF STOCK";
        readonly string text_Remove = "REMOVE";
        readonly string text_Added = "ADDED";
        readonly string text_ToAdd = "TO ADD";
        readonly string text_EditStartDate = "Edit Start Date";
        readonly string text_RearrangeDate = "Auto Reschedule";

        private bool Loaded = false;
        private bool ItemEditChanged = false;
        private bool DateSelectMode = false;
        private bool callFromPOVSStock = false;

        static public string pcsPerManHourString = "100";
        static public string manpowerString = "4";
        static public string hoursPerDayString = "10";

        #endregion

        private DataTable NewMatStockTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerFac, typeof(string));
            dt.Columns.Add(headerStillNeed, typeof(float));
            dt.Columns.Add(headerStock, typeof(float));
            dt.Columns.Add(headerBalance, typeof(float));
            dt.Columns.Add(headerTransfering, typeof(float));
            dt.Columns.Add(headerUnuse, typeof(float));

            return dt;
        }

        private DataTable NewItemTable()
        {
            DataTable dt = new DataTable();


            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_ItemString, typeof(string));

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));

            dt.Columns.Add(header_QtyPerBag, typeof(int));

            dt.Columns.Add(header_StockPcs, typeof(int));
            dt.Columns.Add(header_StockBag, typeof(int));
            dt.Columns.Add(header_StockString, typeof(string));

            dt.Columns.Add(header_RequiredBag, typeof(int));
            dt.Columns.Add(header_TargetBag, typeof(int));
            dt.Columns.Add(header_TargetPcs, typeof(int));
            dt.Columns.Add(header_MaxAvailability, typeof(int));

            dt.Columns.Add(header_DateStart, typeof(DateTime));
            dt.Columns.Add(header_DateEnd, typeof(DateTime));
            dt.Columns.Add(header_Mac, typeof(int));

            dt.Columns.Add(header_AddedStatus, typeof(string));

            return dt;
        }

        private DataTable NewMaterialTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_ParentCode, typeof(string));
            dt.Columns.Add(header_ItemString, typeof(string));
            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_JoinQty, typeof(int));
            

            dt.Columns.Add(header_TotalStock, typeof(int));
            dt.Columns.Add(header_SiteBalance, typeof(int));

            dt.Columns.Add(header_RequiredQty, typeof(int));

            dt.Columns.Add(header_QtyPerBag, typeof(int));
            dt.Columns.Add(header_ToDeliverBag, typeof(int));
            dt.Columns.Add(header_ToDeliverPCS, typeof(int));

            dt.Columns.Add(unit_HeaderName, typeof(string));
            dt.Columns.Add(header_DeliverFrom, typeof(string));

            return dt;
        }

        private DataTable NewScheduleTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_Mac, typeof(int));
            dt.Columns.Add(header_PlanID, typeof(string));
            dt.Columns.Add(header_Status, typeof(string));
            dt.Columns.Add(header_ItemString, typeof(string));
           

            dt.Columns.Add(header_DateStart, typeof(DateTime));
            dt.Columns.Add(header_EstimateEnd, typeof(DateTime));

            dt.Columns.Add(header_Qty, typeof(int));

            dt.Columns.Add(header_PcsPerManHour, typeof(int));
            dt.Columns.Add(header_Manpower, typeof(int));
            dt.Columns.Add(header_HoursPerDay, typeof(int));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            

            if (dgv == dgvItemList)
            {
                dgv.Columns[header_TargetBag].DefaultCellStyle.BackColor = SystemColors.Info;
                dgv.Columns[header_Code].Visible = false;
                dgv.Columns[header_Size].Visible = false;
                dgv.Columns[header_Unit].Visible = false;
                dgv.Columns[header_Type].Visible = false;
                dgv.Columns[header_StockPcs].Visible = false;
                dgv.Columns[header_StockBag].Visible = false;
                dgv.Columns[header_StockString].Visible = false;
                dgv.Columns[header_RequiredBag].Visible = false;
                dgv.Columns[header_AddedStatus].Visible = false;
                //dgv.Columns[header_MaxAvailability].Visible = false;

                dgv.Columns[header_DateStart].Visible = false;
                dgv.Columns[header_DateEnd].Visible = false;
                dgv.Columns[header_Mac].Visible = false;

                dgv.Columns[header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

                dgv.Columns[header_TargetBag].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

                dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ItemString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_QtyPerBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_RequiredBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_TargetBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_TargetPcs].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_MaxAvailability].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[header_DateStart].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_DateEnd].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Mac].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[header_DateStart].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                dgv.Columns[header_DateEnd].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

            }
            else if (dgv == dgvSchedule)
            {
                dgv.Columns[header_Index].Visible = false;
                dgv.Columns[header_PlanID].Visible = false;
                dgv.Columns[header_PcsPerManHour].Visible = false;
                dgv.Columns[header_Manpower].Visible = false;
                dgv.Columns[header_HoursPerDay].Visible = false;

                dgv.Columns[header_ItemString].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[header_Status].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ItemString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Mac].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_DateStart].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_EstimateEnd].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
                //dgv.Columns[header_DateStart].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            }
            else if(dgv == dgvMatList)
            {
                dgv.Columns[header_QtyPerBag].DefaultCellStyle.BackColor = SystemColors.Info;

                dgv.Columns[header_Code].Visible = false;
                dgv.Columns[header_Size].Visible = false;
                dgv.Columns[header_Unit].Visible = false;
                dgv.Columns[header_Type].Visible = false;
                dgv.Columns[header_ParentCode].Visible = false;
                dgv.Columns[header_JoinQty].Visible = false;

                if(!cbStockInclude.Checked)
                {
                    dgv.Columns[header_SiteBalance].Visible = false;
                }
                else
                {
                    dgv.Columns[header_SiteBalance].Visible = true;
                }

                dgv.Columns[header_DeliverFrom].MinimumWidth = 150;

                dgv.Columns[header_DeliverFrom].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                dgv.Columns[header_Type].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

                dgv.Columns[header_ToDeliverBag].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

                dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_TotalStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ToDeliverPCS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ItemString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_QtyPerBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_RequiredQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_SiteBalance].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ToDeliverBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }


        }

        //private void DateAdjust()
        //{

        //}

        //private void DateAdjust(int macID)
        //{

        //}

        private bool DateInitial(int rowIndex, int maxAvailability)
        {
            bool dateInitial = false;
            DataGridView dgv = dgvSchedule;
            DataTable dt = (DataTable)dgv.DataSource;

            int macID_Target = int.TryParse(dt.Rows[rowIndex][header_Mac].ToString(), out macID_Target)? macID_Target : -1;

            DateTime dateStart = DateTime.Today.Date;

            if (rowIndex > 0 && macID_Target != -1)
            {
                //check if previous row under same machine
                int macID_Previous = int.TryParse(dt.Rows[rowIndex - 1][header_Mac].ToString(), out macID_Previous) ? macID_Previous : -1;

                if(macID_Previous == macID_Target)
                {
                    dateStart = DateTime.TryParse(dt.Rows[rowIndex - 1][header_EstimateEnd].ToString(), out dateStart) ? dateStart : DateTime.MaxValue;

                    //check if same day
                    if(!cbSameDay.Checked)
                    {
                        dateStart = dateStart.AddDays(1);

                    }

                    if (tool.checkIfSunday(dateStart) && cbSkipSunday.Checked)
                    {
                        dateStart = dateStart.AddDays(1);
                    }
                }
            }

            //estimate end date
            DateTime dateEnd = EstimateDateEnd(dateStart, maxAvailability);

            //insert

            if(dateStart != DateTime.MaxValue)
            {
                dateInitial = true;

                dt.Rows[rowIndex][header_DateStart] = dateStart;
                dt.Rows[rowIndex][header_EstimateEnd] = dateEnd;

                int itemListRowIndex = int.TryParse(dt.Rows[rowIndex][header_Index].ToString(), out itemListRowIndex) ? itemListRowIndex : -1;

                itemListRowIndex--;

                dgvItemList.Rows[itemListRowIndex].Cells[header_DateStart].Value = dateStart;
                dgvItemList.Rows[itemListRowIndex].Cells[header_DateEnd].Value = dateEnd;
                dgvItemList.Rows[itemListRowIndex].Cells[header_Mac].Value = macID_Target;


                //update the rest row under same machine
                UpdateScheduleAfterNewRowInserted(macID_Target, rowIndex);
            }
            

            if (!dateInitial)
            {
                MessageBox.Show("FAILED TO INITIAL DATE!");
            }

            return dateInitial;
        }

        private void UpdateScheduleDate()
        {
            DataTable dt = (DataTable)dgvSchedule.DataSource;

            int previousMacNo = -1;

            foreach(DataRow row in dt.Rows)
            {
                int MacNo = int.TryParse(row[header_Mac].ToString(), out MacNo) ? MacNo : 0;

                if(previousMacNo != MacNo)
                {
                    previousMacNo = MacNo;
                    UpdateScheduleAfterNewRowInserted(MacNo);
                }
            }
        }

        private void UpdateScheduleAfterNewRowInserted(int macNo)
        {
            DataGridView dgv = dgvSchedule;
            DataTable dt = (DataTable)dgv.DataSource;

            DateTime dateStart = DateTime.MaxValue;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int macID_Next = int.TryParse(dt.Rows[i][header_Mac].ToString(), out macID_Next) ? macID_Next : -1;
                int assemblyQty = int.TryParse(dt.Rows[i][header_Qty].ToString(), out assemblyQty) ? assemblyQty : -1;

                if (macID_Next == macNo)
                {
                    string status = dt.Rows[i][header_Status].ToString();

                    if(status == text.planning_status_pending || status == text_ToAdd)
                    {
                        if (dateStart == DateTime.MaxValue)
                        {
                            dateStart = DateTime.TryParse(dt.Rows[i][header_DateStart].ToString(), out dateStart) ? dateStart : DateTime.MinValue;

                            DateTime dateEnd = EstimateDateEnd(dateStart, assemblyQty);

                            dt.Rows[i][header_DateStart] = dateStart;
                            dt.Rows[i][header_EstimateEnd] = dateEnd;

                            dateStart = dateEnd;


                        }
                        else if (dateStart != DateTime.MinValue)
                        {
    
                            //check if same day
                            if (!cbSameDay.Checked)
                            {
                                dateStart = dateStart.AddDays(1);

                            }

                            if (tool.checkIfSunday(dateStart) && cbSkipSunday.Checked)
                            {
                                dateStart = dateStart.AddDays(1);
                            }

                            DateTime dateEnd = EstimateDateEnd(dateStart, assemblyQty);

                            dt.Rows[i][header_DateStart] = dateStart;
                            dt.Rows[i][header_EstimateEnd] = dateEnd;

                            int itemListRowIndex = int.TryParse(dt.Rows[i][header_Index].ToString(), out itemListRowIndex) ? itemListRowIndex : -1;

                            itemListRowIndex--;

                            dgvItemList.Rows[itemListRowIndex].Cells[header_DateStart].Value = dateStart;
                            dgvItemList.Rows[itemListRowIndex].Cells[header_DateEnd].Value = dateEnd;
                            dgvItemList.Rows[itemListRowIndex].Cells[header_Mac].Value = macNo;

                            dateStart = dateEnd;
                        }
                    }
                   
                 
                }
            }
        }

        private void UpdateScheduleAfterNewRowInserted(int macNo, int rowIndex)
        {
            DataGridView dgv = dgvSchedule;
            DataTable dt = (DataTable)dgv.DataSource;

            DateTime dateStart = DateTime.TryParse(dt.Rows[rowIndex][header_EstimateEnd].ToString(), out dateStart) ? dateStart : DateTime.MaxValue;

            for (int i = rowIndex + 1; i < dt.Rows.Count; i++)
            {
                int macID_Next = int.TryParse(dt.Rows[i][header_Mac].ToString(), out macID_Next) ? macID_Next : -1;

                if(macID_Next == macNo)
                {
                    int assemblyQty = int.TryParse(dt.Rows[i][header_Qty].ToString(), out assemblyQty) ? assemblyQty : -1;

                    //check if same day
                    if (!cbSameDay.Checked)
                    {
                        dateStart = dateStart.AddDays(1);

                    }

                    if (tool.checkIfSunday(dateStart) && cbSkipSunday.Checked)
                    {
                        dateStart = dateStart.AddDays(1);
                    }

                    DateTime dateEnd = EstimateDateEnd(dateStart, assemblyQty);

                    dt.Rows[i][header_DateStart] = dateStart;
                    dt.Rows[i][header_EstimateEnd] = dateEnd;

                    int itemListRowIndex = int.TryParse(dt.Rows[i][header_Index].ToString(), out itemListRowIndex) ? itemListRowIndex : -1;

                    itemListRowIndex--;

                    dgvItemList.Rows[itemListRowIndex].Cells[header_DateStart].Value = dateStart;
                    dgvItemList.Rows[itemListRowIndex].Cells[header_DateEnd].Value = dateEnd;
                    dgvItemList.Rows[itemListRowIndex].Cells[header_Mac].Value = macNo;

                }
            }
        }

        private void LoadItemListFromPlanner(DataTable dt)
        {
            Loaded = false;
            DataTable dt_Item = NewItemTable();

            foreach(DataRow row in dt.Rows)
            {
                int BalAfterDelivery = int.TryParse(row[header_BalAfterDeliveryInPcs].ToString(), out BalAfterDelivery) ? BalAfterDelivery : 0;
                int qtyPerBag = int.TryParse(row[header_QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;

                if (BalAfterDelivery < 0)
                {
                    DataRow new_Row = dt_Item.NewRow();

                    string size = row[header_Size].ToString();
                    string unit = row[header_Unit].ToString();
                    string type = row[header_Type].ToString();
                    string code = row[header_Code].ToString();

                    new_Row[header_Index] = row[header_Index];
                    new_Row[header_Size] = row[header_Size];
                    new_Row[header_Unit] = row[header_Unit];
                    new_Row[header_Type] = row[header_Type];
                    new_Row[header_Code] = row[header_Code];

                    new_Row[header_ItemString] = size + " " + unit + " " + type;

                    new_Row[header_QtyPerBag] = row[header_QtyPerBag];

                    new_Row[header_StockPcs] = row[header_StockPcs];
                    new_Row[header_StockBag] = row[header_StockBag];
                    new_Row[header_StockString] = row[header_StockString];

                    new_Row[header_RequiredBag] = BalAfterDelivery / qtyPerBag * -1;
                    new_Row[header_TargetBag] = BalAfterDelivery/qtyPerBag * -1;
                    new_Row[header_TargetPcs] = BalAfterDelivery * -1;
                    new_Row[header_MaxAvailability] = BalAfterDelivery * -1;

                    dt_Item.Rows.Add(new_Row);
                }
               

            }

            dgvItemList.DataSource = dt_Item;
            DgvUIEdit(dgvItemList);
            dgvItemList.ClearSelection();

            LoadMatPartList();
            //LoadMatPartListWithTargetPcs();
            Loaded = true;
        }

        private string GetChildCode(DataTable dt, string parentCode)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (parentCode == row[dalJoin.ParentCode].ToString())
                {
                    string childCode = row[dalJoin.ChildCode].ToString();

                    if (childCode.Substring(0, 2) == "CF")
                    {
                        parentCode = childCode;

                        break;
                    }
                }

            }

            return parentCode;
        }

        private string GetFacName(string s)
        {
            string rtn = null;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != ' ')
                {
                    rtn += s[i];
                }
                else
                {
                    return rtn;
                }
            }
            return rtn;
        }

        private void AddComboBoxToDGVCell()
        {
            DataGridView dgv = dgvMatList;

            DataTable dt_RequiredList = (DataTable)dgv.DataSource;

            DataTable dt_stock = dalStock.Select();

            dt_MatStock = NewMatStockTable();

            DataRow row_MatStock;

            string previousMat = null;
            string Factory = cmbSiteLocation.Text;

            foreach (DataRow row in dt_RequiredList.Rows)
            {
                int toDeliverBag = int.TryParse(row[header_ToDeliverBag].ToString(), out toDeliverBag) ? toDeliverBag : -2;
                if (toDeliverBag > -2)
                {
                    string MatCode = row[header_Code].ToString();
                    int stdPacking = int.TryParse(row[header_QtyPerBag].ToString(), out stdPacking) ? stdPacking : 0;
                    int siteBal = int.TryParse(row[header_SiteBalance].ToString(), out siteBal) ? siteBal : 0;

                    if (previousMat != MatCode)
                    {
                        previousMat = MatCode;

                        //get fac data
                        foreach (DataRow facStock in dt_stock.Rows)
                        {
                            string ItemCode = facStock[dalItem.ItemCode].ToString();

                            if (ItemCode == MatCode)
                            {
                                row_MatStock = dt_MatStock.NewRow();
                                row_MatStock[headerMatCode] = MatCode;
                                row_MatStock[headerFac] = facStock["fac_name"].ToString();
                                row_MatStock[headerStock] = facStock["stock_qty"].ToString();
                                row_MatStock[headerStillNeed] = 0;
                                row_MatStock[headerBalance] = facStock["stock_qty"].ToString();
                                row_MatStock[headerTransfering] = 0;
                                row_MatStock[headerUnuse] = facStock["stock_qty"].ToString();

                                dt_MatStock.Rows.Add(row_MatStock);
                            }
                        }

                    }

                    foreach (DataRow matStock in dt_MatStock.Rows)
                    {
                        if (MatCode == matStock[headerMatCode].ToString() && Factory == matStock[headerFac].ToString())
                        {
                            matStock[headerStillNeed] = toDeliverBag;

                            int balance = siteBal - toDeliverBag;

                            if (balance < 0)
                            {
                                balance = 0;
                            }

                            matStock[headerBalance] = balance;
                            matStock[headerTransfering] = 0;
                            matStock[headerUnuse] = balance;
                        }
                    }
                }
            }

          

            foreach (DataGridViewRow row in dgv.Rows)
            {

                int toDeliver = int.TryParse(row.Cells[header_ToDeliverBag].Value.ToString(), out toDeliver) ? toDeliver : 0;
                int toDeliverPcs = int.TryParse(row.Cells[header_ToDeliverPCS].Value.ToString(), out toDeliverPcs) ? toDeliverPcs : 0;

                if (toDeliver > 0)
                {
                    DataGridViewComboBoxCell ComboBoxCellUnit = new DataGridViewComboBoxCell();

                    ComboBoxCellUnit.Items.Add(unit_Bag);
                    ComboBoxCellUnit.Items.Add(unit_BagBushGrip);
                    ComboBoxCellUnit.Items.Add(unit_Container);
                    ComboBoxCellUnit.Items.Add(unit_ContainerBushGrip);
                    
                    ComboBoxCellUnit.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    
                    row.Cells[unit_HeaderName] = ComboBoxCellUnit;
                    row.Cells[unit_HeaderName].Value = unit_Bag;

                    string MatCode = row.Cells[header_Code].Value.ToString();

                    DataGridViewComboBoxCell ComboBoxCell = new DataGridViewComboBoxCell();

                    string locationFrom = "";
                    string selectedLocation = "";

                    foreach (DataRow matStock in dt_MatStock.Rows)
                    {
                        string ItemCode = matStock[headerMatCode].ToString();
                        string FacName = matStock[headerFac].ToString();
                        string Balance = matStock[headerBalance].ToString();

                        if (ItemCode == MatCode && FacName != Factory && Balance != "0")
                        {
                            locationFrom = FacName + " (BAL.: " + Balance + ")";
                            ComboBoxCell.Items.Add(locationFrom);


                            int BalInt = int.TryParse(Balance, out BalInt) ? BalInt : 0;

                            if (BalInt >= toDeliverPcs)
                            {
                                selectedLocation = locationFrom;
                            }
                        }
                    }

                    ComboBoxCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                    if (ComboBoxCell.Items.Count <= 0)
                    {
                        ComboBoxCell.Items.Add(text_OutOfStock);
                        row.Cells[header_DeliverFrom] = ComboBoxCell;
                    }
                    else
                    {
                        row.Cells[header_DeliverFrom] = ComboBoxCell;
                    }

                    if (selectedLocation != "")
                    {
                        row.Cells[header_DeliverFrom].Value = selectedLocation;
                        row.Cells[header_ToDeliverPCS].Style.ForeColor = Color.Black;
                        row.Cells[header_ToDeliverBag].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.Cells[header_ToDeliverBag].Style.ForeColor = Color.Red;
                        row.Cells[header_ToDeliverPCS].Style.ForeColor = Color.Red;
                    }

                }
                else
                {
                    row.Cells[unit_HeaderName].Value = DBNull.Value;
                    row.Cells[header_DeliverFrom].Value = DBNull.Value;
                }
            }
        }

        private DataTable RemoveRowIfRequiredQtyEqualZero(DataTable dt)
        {
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                int requiredQty = int.TryParse(row[header_RequiredQty].ToString(), out requiredQty) ? requiredQty : 0;

                if (requiredQty == 0)
                {
                    row.Delete();

                }
            }

            dt.AcceptChanges();

            return dt;
        }

        private DataTable ClearRequiredQty(DataTable dt)
        {
            foreach(DataRow row in dt.Rows)
            {
                row[header_RequiredQty] = 0;
            }

            return dt;
        }

        private DataTable MergeSameMaterial(DataTable dt_Mat)
        {
            DataTable dt = dt_Mat.Clone();

            dt_Mat.DefaultView.Sort = header_Code + " ASC";
            dt_Mat = dt_Mat.DefaultView.ToTable();

            string preChildCode = "";

            foreach (DataRow row in dt_Mat.Rows)
            {
                string childCode = row[header_Code].ToString();
                int newRequiredQty = int.TryParse(row[header_RequiredQty].ToString(), out newRequiredQty) ? newRequiredQty : 0;

                if (childCode != preChildCode)
                {
                    dt.ImportRow(row);
                    preChildCode = childCode;
                }
                else
                {
                    foreach(DataRow dt_row in dt.Rows)
                    {
                        if (dt_row[header_Code].ToString() == childCode)
                        {
                            int oldRequiredQty = int.TryParse(dt_row[header_RequiredQty].ToString(), out oldRequiredQty) ? oldRequiredQty : 0;
                            int siteBal = int.TryParse(dt_row[header_SiteBalance].ToString(), out siteBal) ? siteBal : 0;
                            int qtyperBag = int.TryParse(dt_row[header_QtyPerBag].ToString(), out qtyperBag) ? qtyperBag : 0;

                            int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - newRequiredQty - oldRequiredQty;

                            //check if site have enough balance, if so no need to deliver
                            bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                            bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyperBag > 0 ? qtyperBag : 1));

                            bagToDeliver = qtyperBag > 0 ? bagToDeliver : -1;

                            dt_row[header_RequiredQty] = newRequiredQty + oldRequiredQty;

                            dt_row[header_ToDeliverBag] = bagToDeliver;

                            int pcsTodeliver = 0;

                            if (bagToDeliver > 0)
                            {
                                pcsTodeliver = bagToDeliver * qtyperBag;
                            }

                            dt_row[header_ToDeliverPCS] = pcsTodeliver;

                            break;
                        }
                    }
                }
           
            }

            return dt;
        }

        private void LoadMatPartList()
        {
            #region Pre Setting: datatable and variable

            DataTable dt_Fac = dalStock.Select();
            DataTable dtJoin = dalJoin.SelectWithSBBInfo();
            DataTable dt_Item = (DataTable)dgvItemList.DataSource;
            DataTable dt_Mat = NewMaterialTable();

            if (dgvMatList.Rows.Count > 0)
            {
                dt_Mat = (DataTable)dgvMatList.DataSource;

                //clear required qty
                dt_Mat = ClearRequiredQty(dt_Mat);
            }

            string preChildCode = "";
            int index = 1;

            #endregion

            //Load Material List
            foreach (DataRow row in dt_Item.Rows)
            {
                string parentCode = GetChildCode(dtJoin, row[header_Code].ToString());
                string GoodsCode = row[header_Code].ToString();

                int targetPcs = int.TryParse(row[header_TargetPcs].ToString(), out targetPcs) ? targetPcs : 0;

                row[header_MaxAvailability] = targetPcs;

                foreach (DataRow rowJoin in dtJoin.Rows)
                {
                    if (rowJoin[dalJoin.ParentCode].ToString() == parentCode && preChildCode != rowJoin[dalJoin.ChildCode].ToString())
                    {
                        string childCode = rowJoin[dalJoin.ChildCode].ToString();
                        string childName = rowJoin[dalJoin.ChildName].ToString();

                        preChildCode = childCode;

                        int qtyPerBag = int.TryParse(rowJoin[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                        int numerator = int.TryParse(rowJoin[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                        int denominator = int.TryParse(rowJoin[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                        string sizeUnit = rowJoin[dalSPP.SizeUnit].ToString().ToUpper();
                        string typeName = rowJoin[dalSPP.TypeName].ToString();

                        string sizeString = "";
                        int size = 1;

                        if (denominator == 1)
                        {
                            size = numerator;
                            sizeString = numerator + " " + sizeUnit;

                        }
                        else
                        {
                            size = numerator / denominator;
                            sizeString = numerator + "/" + denominator + " " + sizeUnit;
                        }

                        int joinQty = int.TryParse(rowJoin[dalJoin.JoinQty].ToString(), out joinQty) ? joinQty : 0;
                        int joinMax = int.TryParse(rowJoin[dalJoin.JoinMax].ToString(), out joinMax) ? joinMax : 0;
                        int joinMin = int.TryParse(rowJoin[dalJoin.JoinMin].ToString(), out joinMin) ? joinMin : 0;

                        int child_StillNeed = 0;

                        joinMax = joinMax <= 0 ? 1 : joinMax;
                        joinMin = joinMin <= 0 ? 1 : joinMin;

                        child_StillNeed = targetPcs / joinMax * joinQty;

                        child_StillNeed = targetPcs % joinMax >= joinMin ? child_StillNeed + joinQty : child_StillNeed;


                        bool itemFound = false;

                        foreach (DataRow rowMat in dt_Mat.Rows)
                        {
                            if (rowMat[header_Code].ToString() == childCode && rowMat[header_ParentCode].ToString() == GoodsCode)
                            {
                                int oldRequiredQty = int.TryParse(rowMat[header_RequiredQty].ToString(), out oldRequiredQty) ? oldRequiredQty : 0;

                                int siteBal = 0;
                                int totalStock = 0;

                                foreach (DataRow stock in dt_Fac.Rows)
                                {
                                    if (cmbSiteLocation.Text == stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                    {
                                        siteBal = int.TryParse(stock["stock_qty"].ToString(), out siteBal) ? siteBal : 0;
                                    }

                                    if (cmbSiteLocation.Text != stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                    {
                                        totalStock = int.TryParse(stock["stock_qty"].ToString(), out totalStock) ? totalStock : 0;
                                    }
                                }

                                if (cbStockInclude.Checked)
                                {
                                    totalStock += siteBal;
                                }
                                else
                                {
                                    siteBal = 0;
                                }

                                int qtyperBag_2 = int.TryParse(rowMat[header_QtyPerBag].ToString(), out qtyperBag_2) ? qtyperBag_2 : 0;

                                int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - child_StillNeed - oldRequiredQty;

                                //check if site have enough balance, if so no need to deliver
                                bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                                bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyperBag_2 > 0 ? qtyperBag_2 : 1));

                                bagToDeliver = qtyperBag_2 > 0 ? bagToDeliver : -1;

                                if (!itemFound)
                                {
                                    rowMat[header_RequiredQty] = child_StillNeed + oldRequiredQty;

                                    rowMat[header_ToDeliverBag] = bagToDeliver;

                                    rowMat[header_SiteBalance] = siteBal;
                                    rowMat[header_TotalStock] = totalStock;

                                    int pcsTodeliver = 0;

                                    if (bagToDeliver > 0)
                                    {
                                        pcsTodeliver = bagToDeliver * qtyperBag_2;
                                    }

                                    rowMat[header_ToDeliverPCS] = pcsTodeliver;
                                }

                                itemFound = true;

                                break;
                            }
                        }

                        if (!itemFound)
                        {
                            int siteBal = 0;
                            int totalStock = 0;

                            foreach (DataRow stock in dt_Fac.Rows)
                            {
                                if (cmbSiteLocation.Text == stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                {
                                    siteBal = int.TryParse(stock["stock_qty"].ToString(), out siteBal) ? siteBal : 0;
                                }

                                if (cmbSiteLocation.Text != stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                {
                                    totalStock = int.TryParse(stock["stock_qty"].ToString(), out totalStock) ? totalStock : 0;
                                }
                            }

                            if (cbStockInclude.Checked)
                            {
                                totalStock += siteBal;
                            }
                            else
                            {
                                siteBal = 0;
                            }

                            DataRow new_Row = dt_Mat.NewRow();

                            int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - child_StillNeed;

                            //check if site have enough balance, if so no need to deliver
                            bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                            bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyPerBag > 0 ? qtyPerBag : 1));

                            bagToDeliver = qtyPerBag > 0 ? bagToDeliver : -1;

                            int pcsTodeliver = 0;

                            if (bagToDeliver > 0)
                            {
                                pcsTodeliver = bagToDeliver * qtyPerBag;
                            }

                            new_Row[header_Index] = index++;
                            new_Row[header_ParentCode] = GoodsCode;
                            new_Row[header_ItemString] = childName;
                            new_Row[header_Size] = size;
                            new_Row[header_Unit] = sizeUnit;
                            new_Row[header_Type] = typeName;
                            new_Row[header_Code] = childCode;
                            new_Row[header_TotalStock] = totalStock;
                            new_Row[header_JoinQty] = joinQty;
                            new_Row[header_QtyPerBag] = qtyPerBag;
                            new_Row[header_RequiredQty] = child_StillNeed;
                            new_Row[header_SiteBalance] = siteBal;
                            new_Row[header_ToDeliverBag] = bagToDeliver;
                            new_Row[header_ToDeliverPCS] = pcsTodeliver;
                            dt_Mat.Rows.Add(new_Row);
                        }

                    }
                }
            }

            //remove row if required qty = 0
            dt_Mat = RemoveRowIfRequiredQtyEqualZero(dt_Mat);

            if(cbMaxAvailability.Checked)
            {
                //update item max qty
                UpdateItemMaxAvailability(dt_Mat);

                //update material list: change required qty & to deliver bag only
                dt_Mat = UpdateMatPartList(dt_Mat);
            }


            //calculate Min availability for item
            UpdateItemMinAvailability(dt_Mat);

            dgvItemList.Update();
            dgvItemList.Refresh();

            dt_MatListForEachItem = dt_Mat;

            //Merge same material in dt_Mat
            dt_Mat = MergeSameMaterial(dt_Mat);

            int indexReset = 1;

            foreach (DataGridViewRow row in dgvMatList.Rows)
            {
                row.Cells[header_Index].Value = indexReset++;
            }

            dgvMatList.DataSource = dt_Mat;
            DgvUIEdit(dgvMatList);
            dgvMatList.ReadOnly = false;
            dgvMatList.SelectionMode = DataGridViewSelectionMode.CellSelect;

            AddComboBoxToDGVCell();

            dgvMatList.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

            dgvMatList.ClearSelection();
        }

        private DataTable UpdateMatPartList(DataTable dt_Mat)
        {
            #region Pre Setting: datatable and variable

            DataTable dt_Fac = dalStock.Select();
            DataTable dtJoin = dalJoin.SelectWithSBBInfo();
            DataTable dt_Item = (DataTable)dgvItemList.DataSource;

            dt_Mat = ClearRequiredQty(dt_Mat);

            string preChildCode = "";
            int index = 1;

            #endregion

            //Load Material List
            foreach (DataRow row in dt_Item.Rows)
            {
                string parentCode = GetChildCode(dtJoin, row[header_Code].ToString());
                string GoodsCode = row[header_Code].ToString();
                int targetPcs = int.TryParse(row[header_MaxAvailability].ToString(), out targetPcs) ? targetPcs : 0;

                if(targetPcs == 0)
                {
                    targetPcs = int.TryParse(row[header_TargetPcs].ToString(), out targetPcs) ? targetPcs : 0;
                }

                if(targetPcs != 0)
                {
                    foreach (DataRow rowJoin in dtJoin.Rows)
                    {
                        if (rowJoin[dalJoin.ParentCode].ToString() == parentCode && preChildCode != rowJoin[dalJoin.ChildCode].ToString())
                        {
                            string childCode = rowJoin[dalJoin.ChildCode].ToString();
                            string childName = rowJoin[dalJoin.ChildName].ToString();

                            preChildCode = childCode;

                            int qtyPerBag = int.TryParse(rowJoin[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                            int numerator = int.TryParse(rowJoin[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                            int denominator = int.TryParse(rowJoin[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                            string sizeUnit = rowJoin[dalSPP.SizeUnit].ToString().ToUpper();
                            string typeName = rowJoin[dalSPP.TypeName].ToString();

                            string sizeString = "";
                            int size = 1;

                            if (denominator == 1)
                            {
                                size = numerator;
                                sizeString = numerator + " " + sizeUnit;

                            }
                            else
                            {
                                size = numerator / denominator;
                                sizeString = numerator + "/" + denominator + " " + sizeUnit;
                            }

                            int joinQty = int.TryParse(rowJoin[dalJoin.JoinQty].ToString(), out joinQty) ? joinQty : 0;
                            int joinMax = int.TryParse(rowJoin[dalJoin.JoinMax].ToString(), out joinMax) ? joinMax : 0;
                            int joinMin = int.TryParse(rowJoin[dalJoin.JoinMin].ToString(), out joinMin) ? joinMin : 0;

                            int child_StillNeed = 0;

                            joinMax = joinMax <= 0 ? 1 : joinMax;
                            joinMin = joinMin <= 0 ? 1 : joinMin;

                            child_StillNeed = targetPcs / joinMax * joinQty;

                            child_StillNeed = targetPcs % joinMax >= joinMin ? child_StillNeed + joinQty : child_StillNeed;


                            bool itemFound = false;

                            foreach (DataRow rowMat in dt_Mat.Rows)
                            {
                                if (rowMat[header_Code].ToString() == childCode && rowMat[header_ParentCode].ToString() == GoodsCode)
                                {
                                    int oldRequiredQty = int.TryParse(rowMat[header_RequiredQty].ToString(), out oldRequiredQty) ? oldRequiredQty : 0;

                                    int siteBal = 0;
                                    int totalStock = 0;

                                    foreach (DataRow stock in dt_Fac.Rows)
                                    {
                                        if (cmbSiteLocation.Text == stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                        {
                                            siteBal = int.TryParse(stock["stock_qty"].ToString(), out siteBal) ? siteBal : 0;
                                        }

                                        if (cmbSiteLocation.Text != stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                        {
                                            totalStock = int.TryParse(stock["stock_qty"].ToString(), out totalStock) ? totalStock : 0;
                                        }
                                    }

                                    if (cbStockInclude.Checked)
                                    {
                                        totalStock += siteBal;
                                    }
                                    else
                                    {
                                        siteBal = 0;
                                    }

                                    int qtyperBag_2 = int.TryParse(rowMat[header_QtyPerBag].ToString(), out qtyperBag_2) ? qtyperBag_2 : 0;

                                    int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - child_StillNeed - oldRequiredQty;

                                    //check if site have enough balance, if so no need to deliver
                                    bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                                    bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyperBag_2 > 0 ? qtyperBag_2 : 1));

                                    bagToDeliver = qtyperBag_2 > 0 ? bagToDeliver : -1;

                                    if (!itemFound)
                                    {
                                        if(child_StillNeed + oldRequiredQty > 0)
                                        {
                                            rowMat[header_RequiredQty] = child_StillNeed + oldRequiredQty;

                                            rowMat[header_ToDeliverBag] = bagToDeliver;

                                            rowMat[header_SiteBalance] = siteBal;
                                            rowMat[header_TotalStock] = totalStock;

                                            int pcsTodeliver = 0;

                                            if (bagToDeliver > 0)
                                            {
                                                pcsTodeliver = bagToDeliver * qtyperBag_2;
                                            }

                                            rowMat[header_ToDeliverPCS] = pcsTodeliver;
                                        }
                                       
                                    }

                                    itemFound = true;

                                    break;
                                }
                            }

                            if (!itemFound)
                            {
                                int siteBal = 0;
                                int totalStock = 0;

                                foreach (DataRow stock in dt_Fac.Rows)
                                {
                                    if (cmbSiteLocation.Text == stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                    {
                                        siteBal = int.TryParse(stock["stock_qty"].ToString(), out siteBal) ? siteBal : 0;
                                    }

                                    if (cmbSiteLocation.Text != stock["fac_name"].ToString() && childCode == stock[dalItem.ItemCode].ToString())
                                    {
                                        totalStock = int.TryParse(stock["stock_qty"].ToString(), out totalStock) ? totalStock : 0;
                                    }
                                }

                                if (cbStockInclude.Checked)
                                {
                                    totalStock += siteBal;
                                }
                                else
                                {
                                    siteBal = 0;
                                }

                                DataRow new_Row = dt_Mat.NewRow();

                                int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - child_StillNeed;

                                //check if site have enough balance, if so no need to deliver
                                bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                                bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyPerBag > 0 ? qtyPerBag : 1));

                                bagToDeliver = qtyPerBag > 0 ? bagToDeliver : -1;

                                int pcsTodeliver = 0;

                                if (bagToDeliver > 0)
                                {
                                    pcsTodeliver = bagToDeliver * qtyPerBag;
                                }

                                new_Row[header_Index] = index++;
                                new_Row[header_ParentCode] = GoodsCode;
                                new_Row[header_ItemString] = childName;
                                new_Row[header_Size] = size;
                                new_Row[header_Unit] = sizeUnit;
                                new_Row[header_Type] = typeName;
                                new_Row[header_Code] = childCode;
                                new_Row[header_JoinQty] = joinQty;
                                new_Row[header_QtyPerBag] = qtyPerBag;
                                new_Row[header_RequiredQty] = child_StillNeed;
                                new_Row[header_SiteBalance] = siteBal;
                                new_Row[header_TotalStock] = totalStock;
                                new_Row[header_ToDeliverBag] = bagToDeliver;
                                new_Row[header_ToDeliverPCS] = pcsTodeliver;
                                dt_Mat.Rows.Add(new_Row);
                            }

                        }
                    }
                }

            }

            return dt_Mat;
        }

        private void UpdateItemMaxAvailability(DataTable dt_Mat)
        {
            foreach (DataRow row in dt_Mat.Rows)
            {
                string mat_Type = row[header_Type].ToString();
                string parentCode = row[header_ParentCode].ToString();

                for (int i = 0; i < dgvItemList.Rows.Count; i++)
                {
                    string item_Type = dgvItemList.Rows[i].Cells[header_Type].Value.ToString();
                    string itemCode = dgvItemList.Rows[i].Cells[header_Code].Value.ToString();

                    if (mat_Type == item_Type && itemCode == parentCode)
                    {
                        int qtyPerBag = int.TryParse(row[header_QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                        int bagToDeliver = int.TryParse(row[header_ToDeliverBag].ToString(), out bagToDeliver) ? bagToDeliver : 0;

                        int joinQty = int.TryParse(row[header_JoinQty].ToString(), out joinQty) ? joinQty : 1;

                        int pcsToDeliver = qtyPerBag * bagToDeliver / joinQty;
                        int requiredQty = int.TryParse(row[header_RequiredQty].ToString(), out requiredQty) ? requiredQty : 0;

                        if (pcsToDeliver < requiredQty && cbStockInclude.Checked)
                        {
                            int siteBal = int.TryParse(row[header_SiteBalance].ToString(), out siteBal) ? siteBal : 0;

                            if (siteBal >= requiredQty)
                            {
                                pcsToDeliver = requiredQty / joinQty;
                            }
                            else
                            {
                                pcsToDeliver = (siteBal + qtyPerBag * bagToDeliver) / joinQty;
                            }
                        }



                        //int targetPcs = int.TryParse(dgvItemList.Rows[i].Cells[header_TargetPcs].Value.ToString(), out targetPcs) ? targetPcs : 0;

                        dgvItemList.Rows[i].Cells[header_MaxAvailability].Value = pcsToDeliver;
 

                        break;
                    }
                }
            }

        }

        private void UpdateItemMinAvailability(DataTable dt_Mat)
        {
            for (int i = 0; i < dgvItemList.Rows.Count; i++)
            {
                string item_Type = dgvItemList.Rows[i].Cells[header_Type].Value.ToString();
                string itemCode = dgvItemList.Rows[i].Cells[header_Code].Value.ToString();
                int maxQty = int.TryParse(dgvItemList.Rows[i].Cells[header_MaxAvailability].Value.ToString(), out maxQty) ? maxQty : 0;

                foreach (DataRow row in dt_Mat.Rows)
                {
                    string parentCode = row[header_ParentCode].ToString();

                    if (itemCode == parentCode)
                    {
                        int joinQty = int.TryParse(row[header_JoinQty].ToString(), out joinQty) ? joinQty : 1;

                        int qtyPerBag = int.TryParse(row[header_QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                        int bagToDeliver = int.TryParse(row[header_ToDeliverBag].ToString(), out bagToDeliver) ? bagToDeliver : 0;

                        int pcsToDeliver = qtyPerBag * bagToDeliver / joinQty;
                        int requiredQty = int.TryParse(row[header_RequiredQty].ToString(), out requiredQty) ? requiredQty : 0;

                        if (pcsToDeliver < requiredQty && cbStockInclude.Checked)
                        {
                            int siteBal = int.TryParse(row[header_SiteBalance].ToString(), out siteBal) ? siteBal : 0;
                           
                            if (siteBal >= requiredQty)
                            {
                                pcsToDeliver = requiredQty / joinQty;
                            }
                            else
                            {
                                pcsToDeliver = (siteBal + qtyPerBag * bagToDeliver) / joinQty;
                            }
                        }
                      

                        if (pcsToDeliver < maxQty)
                        {
                            maxQty = pcsToDeliver;
                            dgvItemList.Rows[i].Cells[header_MaxAvailability].Value = maxQty;
                        }
                    }
                }
            }
        }

        private void frmSBBAssemblyPlanning_Load(object sender, EventArgs e)
        {
            //tlpMaterialList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            tlpSetting.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0f);
            tlpSetting.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);

            tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);
            tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);

            //if(dt_FromPlanner != null)
            //LoadItemListFromPlanner(dt_FromPlanner);

            if(callFromPOVSStock)
            AddComboBoxToDGVCell();

            dgvItemList.ClearSelection();
            dgvMatList.ClearSelection();
            Loaded = true;
        }

        private void cmbSiteLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Loaded)
            {
                //update site balance
                LoadMatPartList();
            }
        }

        private void dgvItemList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                DataGridView dgv = (DataGridView)sender;

                //int rowIndex = e.RowIndex;

                //if (rowIndex != -1 && DateSelectMode)
                //{
                //    DateTime selectedDateStart = DateTime.TryParse(dgv.Rows[rowIndex].Cells[header_DateStart].Value.ToString(), out selectedDateStart) ? selectedDateStart : DateTime.Today.Date;

                //    dtpStart.Value = selectedDateStart;

                //    string macID = dgv.Rows[rowIndex].Cells[header_Mac].Value.ToString();
                //    cmbMac.Text = macID;

                //    CalculateTimeNeeded();
                //}

                //handle the row selection on right click
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();


                    dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    dgv.Rows[e.RowIndex].Selected = true;
                    dgv.Focus();
                    int rowIndex = dgv.CurrentCell.RowIndex;

                    if (dgv == dgvItemList)
                    {
                        my_menu.Items.Add(text_Remove).Name = text_Remove;
                    }
                  

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(ItemList_ItemClicked);

                }
                else
                {
                    //btnEdit.Visible = false;
                }
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void RemoveItem(int rowIndex)
        {
            DataTable dt = (DataTable)dgvItemList.DataSource;

            dt.Rows[rowIndex].Delete();

            dt.AcceptChanges();

            int index = 1;
            foreach(DataRow row in dt.Rows)
            {
                row[header_Index] = index++;
            }

            LoadMatPartList();
        }

        private void ItemList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DataGridView dgv = dgvItemList;

                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0)
                {
                    string ClickedItem = e.ClickedItem.Name.ToString();

                    if (ClickedItem.Equals(text_Remove))
                    {
                        RemoveItem(rowIndex);
                    }

                    //else if (ClickedItem.Equals(text_InCompleteDO))
                    //{
                    //    IncompleteDO(rowIndex);
                    //    //MessageBox.Show("incomplete do");
                    //}
                    //else if (ClickedItem.Equals(text_UndoRemove))
                    //{
                    //    DOUndoRemove(rowIndex);
                    //    //MessageBox.Show("undo remove");
                    //}
                    //else if (ClickedItem.Equals(text_ChangeDONumber))
                    //{
                    //    ChangeDONumber(rowIndex);
                    //    //MessageBox.Show("Change D/O Number");
                    //}
                }



                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvItemList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = dgvItemList;

            if (e.RowIndex != -1)
            {
                int col = e.ColumnIndex;
                int row = e.RowIndex;

                if(dgv.Columns[col].Name == header_TargetBag)
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = false;
                }
                else
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = true;
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;


        }

        private void dgvItemList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView dgv = dgvItemList;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                int targetBag = int.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out targetBag) ? targetBag : -1;

                if (dgv.Columns[colIndex].Name.Contains(header_TargetBag) ) //Desired Column
                {
                    oldTargetBag = targetBag;

                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {

                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvItemList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //compare old value with new value
            DataGridView dgv = dgvItemList;
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            int targetBag = int.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out targetBag) ? targetBag : -1;

            if (rowIndex > -1 && targetBag != -1 && targetBag != oldTargetBag)
            {
                oldTargetBag = 0;
                ItemEditChanged = true;

                int qtyPerBag = int.TryParse(dgv.Rows[rowIndex].Cells[header_QtyPerBag].Value.ToString(), out qtyPerBag) ? qtyPerBag : 0;

                dgv.Rows[rowIndex].Cells[header_TargetPcs].Value = targetBag * qtyPerBag;
                dgv.Rows[rowIndex].Cells[header_MaxAvailability].Value = targetBag * qtyPerBag;

                dgv.Rows[rowIndex].Cells[header_DateStart].Value = DBNull.Value;
                dgv.Rows[rowIndex].Cells[header_DateEnd].Value = DBNull.Value;
                dgv.Rows[rowIndex].Cells[header_Mac].Value = DBNull.Value;

                //update material list
                LoadMatPartList();

                ItemEditChanged = false;

            }

        }

        private void dgvMatList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvMatList;
            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;

            bool validClick = row != -1 && col != -1; //Make sure the clicked row/column is valid.

            if (validClick)
            {
                if (dgv.Rows[row].Cells[col] is DataGridViewComboBoxCell)
                {
                    dgv.BeginEdit(true);
                    ((ComboBox)dgv.EditingControl).DroppedDown = true;
                }
                //else if (dgv.Columns[col].Name == headerPrepare)
                //{
                //    dgv.BeginEdit(true);
                //}
            }
        }

        private string GetFacName(DataTable dt_Fac, string location)
        {
            foreach(DataRow row in dt_Fac.Rows)
            {
                string facName = row["fac_name"].ToString();

                if(location.Contains(facName))
                {
                    return facName;
                }
            }

            return "";
        }

        private void btnText_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvMatList.DataSource;
            DataTable dt_MaterialList = dt.Copy();
            DataTable dt_Fac = dalFac.Select();

            dt_MaterialList.DefaultView.Sort = header_DeliverFrom + " ASC";
            dt_MaterialList = dt_MaterialList.DefaultView.ToTable();



            string assemblySite = cmbSiteLocation.Text;
            string materialPrepareList = "";
            string materialPrepareListForWhatsApp = "Assembly Items:\n\n";
            string preDeliverFrom = "";
            
            foreach(DataRow row in dt_MaterialList.Rows)
            {
                int toDeliver = int.TryParse(row[header_ToDeliverBag].ToString(), out toDeliver) ? toDeliver : 0;
                string from = GetFacName(dt_Fac, row[header_DeliverFrom].ToString());
                string toDeliverUnit = row[unit_HeaderName].ToString();

                toDeliverUnit = toDeliverUnit.ToLower();

                if (toDeliver > 0 && !string.IsNullOrEmpty(from))
                {
                    string item = row[header_ItemString].ToString();
                    
                    string stdPacking = row[header_QtyPerBag].ToString();

                    item = item.Replace("CF ", "");
                    item = item.ToLower();

                    if(preDeliverFrom != from)
                    {
                        
                        if (preDeliverFrom == "")
                        {
                            materialPrepareList += "From " + from + " To " + assemblySite;
                            materialPrepareListForWhatsApp += "_From " + from + " To " + assemblySite + "_";
                        }
                        else
                        {
                            materialPrepareList += "\n\n" + "From " + from + " To " + assemblySite;
                            materialPrepareListForWhatsApp += "\n\n" + "_From " + from + " To " + assemblySite + "_";
                        }

                        preDeliverFrom = from;

                    }

                    if(cbWithStdPacking.Checked)
                    {
                        materialPrepareList += "\n" + item + "   " + toDeliver + " " + toDeliverUnit + " (x" + stdPacking + ")";
                        materialPrepareListForWhatsApp += "\n" + item + "   *" + toDeliver + "* " + " " + toDeliverUnit + " (x" + stdPacking + ")";
                    }
                    else
                    {
                        materialPrepareList += "\n" + item + "   " + toDeliver + " " + toDeliverUnit ;
                        materialPrepareListForWhatsApp += "\n" + item + "   *" + toDeliver + "* " + " " + toDeliverUnit ;
                    }
                    
                }
            }

            MessageBox.Show(materialPrepareList + "\n\n Text has been copied to clipboard.");
            Clipboard.SetText(materialPrepareListForWhatsApp);
        }

        private void dgvMatList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView dgv = dgvMatList;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                //int targetBag = int.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out targetBag) ? targetBag : -1;

                if (dgv.Columns[colIndex].Name.Contains(header_QtyPerBag)) //Desired Column
                {
                    //oldTargetBag = targetBag;

                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {

                    if (e.Control is TextBox tb)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvMatList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //compare old value with new value
            DataGridView dgv = dgvMatList;
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if (rowIndex > -1 && dgv.Columns[colIndex].Name == header_QtyPerBag)
            {
                //int qtyPerBag = int.TryParse(dgv.Rows[rowIndex].Cells[header_QtyPerBag].Value.ToString(), out qtyPerBag) ? qtyPerBag : 0;

                //int siteBal = int.TryParse(dgv.Rows[rowIndex].Cells[header_SiteBalance].Value.ToString(), out siteBal) ? siteBal : 0;

                //int requiredQty = int.TryParse(dgv.Rows[rowIndex].Cells[header_OrigRequiredQty].Value.ToString(), out requiredQty) ? requiredQty : 0;

                //int bagToDeliver = (siteBal < 0 ? 0 : siteBal) - requiredQty;

                //string childCode = dgv.Rows[rowIndex].Cells[header_Code].Value.ToString();

                ////check if site have enough balance, if so no need to deliver
                //bagToDeliver = bagToDeliver >= 0 ? 0 : bagToDeliver * -1;

                //bagToDeliver = (int)Math.Ceiling((double)bagToDeliver / (qtyPerBag > 0 ? qtyPerBag : 1));

                //bagToDeliver = qtyPerBag > 0 ? bagToDeliver : -1;

                //dgv.Rows[rowIndex].Cells[header_ToDeliverBag].Value = bagToDeliver;
                //dgv.Rows[rowIndex].Cells[header_ToDeliverPCS].Value = bagToDeliver * qtyPerBag;


                //UpdateQtyPerBagForMatDetailList(childCode, qtyPerBag);

                LoadMatPartList();
            }
        }

        private void UpdateQtyPerBagForMatDetailList(string childCode,int qtyPerBag)
        {
            foreach(DataRow row in dt_MatListForEachItem.Rows)
            {
                if(row[header_Code].ToString() == childCode)
                {
                    row[header_QtyPerBag] = qtyPerBag;
                }
            }
        }

        private void btnAddNewDO_Click(object sender, EventArgs e)
        {
            frmNewSBBAddItem frm = new frmNewSBBAddItem()
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();

            string selectedItemCode = frmNewSBBAddItem.itemCode;

            DataTable dt_Product = dalItemCust.SPPCustSearchWithTypeAndSize(text.SPP_BrandName);

            DataTable dt_Item = (DataTable)dgvItemList.DataSource;

            if (dt_Item == null )
            {
                dt_Item = NewItemTable();
            }

            bool itemFound = false;

            

            foreach (DataRow row in dt_Item.Rows)
            {
                if (row[header_Code].ToString() == selectedItemCode)
                {
                    itemFound = true;
                    break;
                }
            }


            if (!itemFound)
            {
                foreach (DataRow row in dt_Product.Rows)
                {

                    string itemCode = row[dalSPP.ItemCode].ToString();

                    if (itemCode == selectedItemCode)
                    {
                        int readyStock = int.TryParse(row[dalItem.ItemStock].ToString(), out readyStock) ? readyStock : 0;
                        int qtyPerPacket = int.TryParse(row[dalSPP.QtyPerPacket].ToString(), out qtyPerPacket) ? qtyPerPacket : 0;
                        int qtyPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out qtyPerBag) ? qtyPerBag : 0;
                        int maxLevel = int.TryParse(row[dalSPP.MaxLevel].ToString(), out maxLevel) ? maxLevel : 0;
                        int numerator = int.TryParse(row[dalSPP.SizeNumerator].ToString(), out numerator) ? numerator : 1;
                        int denominator = int.TryParse(row[dalSPP.SizeDenominator].ToString(), out denominator) ? denominator : 1;
                        string sizeUnit = row[dalSPP.SizeUnit].ToString().ToUpper();

                        string typeName = row[dalSPP.TypeName].ToString();

                        int pcsStock = readyStock;
                        int bagStock = pcsStock / qtyPerBag;


                        string sizeString = "";
                        int size = 1;
                        if (denominator == 1)
                        {
                            size = numerator;
                            sizeString = numerator + " " + sizeUnit;

                        }
                        else
                        {
                            size = numerator / denominator;
                            sizeString = numerator + "/" + denominator + " " + sizeUnit;
                        }
                        string stockString = pcsStock + " PCS";

                        DataRow new_Row = dt_Item.NewRow();

                        new_Row[header_Size] = size;
                        new_Row[header_Unit] = sizeUnit;
                        new_Row[header_Type] = typeName;
                        new_Row[header_Code] = itemCode;

                        new_Row[header_ItemString] = size + " " + sizeUnit + " " + typeName;

                        new_Row[header_QtyPerBag] = qtyPerBag;

                        new_Row[header_StockPcs] = pcsStock;
                        new_Row[header_StockBag] = bagStock;
                        new_Row[header_StockString] = stockString;

                        new_Row[header_RequiredBag] = 0;
                        new_Row[header_TargetBag] = 0;
                        new_Row[header_TargetPcs] = 0;

                        dt_Item.Rows.Add(new_Row);

                        break;
                    }

                }
                dgvItemList.DataSource = dt_Item;

                DgvUIEdit(dgvItemList);

                tool.AddIndexNumberToDGV(dgvItemList,header_Index);

                cbMaxAvailability.Checked = false;

                dgvItemList.ClearSelection();
            }
           else
            {
                MessageBox.Show("Item already added!");
            }

          

        }

        private void cbMaxAvailability_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatPartList();

            //if(cbMaxAvailability.Checked)
            //{
            //    dgvItemList.Columns[header_MaxAvailability].Visible = true;
            //}
            //else
            //{
            //    dgvItemList.Columns[header_MaxAvailability].Visible = false;
            //}
        }

        private void dgvMatList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cbStockInclude_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatPartList();
        }

        private int GetBalanceFromLocationString(string str)
        {
            string bal = "";

            bool startConvert = false;
            for(int i = 0; i < str.Length; i++)
            {
                if(str[i].ToString() == "(")
                {
                    startConvert = true;
                }

                if(startConvert && int.TryParse(str[i].ToString(), out int x))
                {
                    bal += str[i].ToString();
                }
            }

            int balInt = int.TryParse(bal, out balInt) ? balInt : 0;
            return balInt;
        }

        private void dgvMatList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvMatList;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if(Loaded && dgv.Columns[col].Name == header_DeliverFrom)
            {
                // My combobox column is the second one so I hard coded a 1, flavor to taste
               //DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dgv.Rows[row].Cells[col];

                int toDeliverPcs = int.TryParse(dgv.Rows[row].Cells[header_ToDeliverPCS].Value.ToString(), out toDeliverPcs) ? toDeliverPcs : 0;

                //string cbValue = cb.Value.ToString();
                string cbValue = dgv.CurrentCell.EditedFormattedValue.ToString();
                //get balance
                int bal = GetBalanceFromLocationString(cbValue);

                if(bal < toDeliverPcs)
                {
                    dgv.Rows[row].Cells[header_ToDeliverPCS].Style.ForeColor = Color.Red;
                    dgv.Rows[row].Cells[header_ToDeliverBag].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[header_ToDeliverPCS].Style.ForeColor = Color.Black;
                    dgv.Rows[row].Cells[header_ToDeliverBag].Style.ForeColor = Color.Black;
                }
            }
            
        }

        private void DateSelectUIMode()
        {
            DataGridView dgv = dgvItemList;

            dgv.Columns[header_TargetBag].Visible = false;
            dgv.Columns[header_TargetPcs].Visible = false;
            dgv.Columns[header_MaxAvailability].Visible = true;
            dgv.Columns[header_DateStart].Visible = true;
            dgv.Columns[header_DateEnd].Visible = true;
            dgv.Columns[header_Mac].Visible = true;

            btnAdd.Visible = false;
            cbMaxAvailability.Visible = false;

            lblSubList.Text = "SCHEDULE LIST";
            btnPreviousStep.Visible = true;
            btnNextStep.Text = "CONFIRM";
            btnNextStep.Enabled = false;
            btnNextStep.BackColor = Color.FromArgb(0, 184, 148);

            tlpSetting.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);
            tlpSetting.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 100f);

            tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
            tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);

        }

        private void MaterialListUIMode()
        {
            DataGridView dgv = dgvItemList;

            dgv.Columns[header_TargetBag].Visible = true;
            dgv.Columns[header_TargetPcs].Visible = true;
            
            if(!cbMaxAvailability.Checked)
            dgv.Columns[header_MaxAvailability].Visible = false;

            dgv.Columns[header_DateStart].Visible = false;
            dgv.Columns[header_DateEnd].Visible = false;
            dgv.Columns[header_Mac].Visible = false;

            btnAdd.Visible = true;
            cbMaxAvailability.Visible = true;

            lblSubList.Text = "MATERIAL LIST";
            btnPreviousStep.Visible = false;
            btnNextStep.Text = "DATE SELECT  >";
            btnNextStep.Enabled = true;
            btnNextStep.BackColor = Color.FromArgb(253, 203, 110);

           

            tlpSetting.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 0f);
            tlpSetting.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);

            tlpList.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);
            tlpList.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);

        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            DateSelectMode = true;
            AllowDrop = true;
            dgvItemList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItemList.ClearSelection();
            dgvItemList.CurrentCell = null;
            IfDateClash();
            DateSelectUIMode();
            LoadSchedule();


        }

        private void LoadSchedule()
        {
            DataTable dt_Schedule = NewScheduleTable();


            //get data from DB
            //loop machine and check if idle

            //if idle, show Idle message
            DataRow newRow = dt_Schedule.NewRow();

            newRow[header_Mac] = 1;
            newRow[header_Status] = text.planning_status_idle;
            dt_Schedule.Rows.Add(newRow);

            newRow = dt_Schedule.NewRow();

            newRow[header_Mac] = 2;
            newRow[header_Status] = text.planning_status_idle;
            dt_Schedule.Rows.Add(newRow);

            dgvSchedule.DataSource = dt_Schedule;
            DgvUIEdit(dgvSchedule);

            dgvSchedule.ClearSelection();
        }

       // private void 
        private void btnPreviousStep_Click(object sender, EventArgs e)
        {
            DateSelectMode = false;
            MaterialListUIMode();
            ResetItemList();
        }

        private void ResetItemList()
        {
            for(int i = 0; i < dgvItemList.RowCount; i++)
            {
                dgvItemList.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                dgvItemList.Rows[i].Cells[header_AddedStatus].Value = text_ToAdd;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void CalculateTimeNeeded()
        {
            DataGridView dgv = dgvItemList;
            string timeNeededString = "Please Select a Valid Plan";

            if (dgv.CurrentCell != null)
            {
                int rowIndex = dgv.CurrentCell.RowIndex;

                int maxAvailability = int.TryParse(dgv.Rows[rowIndex].Cells[header_MaxAvailability].Value.ToString(), out maxAvailability) ? maxAvailability : -1;

                int pcsPerManHour = int.TryParse(pcsPerManHourString, out pcsPerManHour) ? pcsPerManHour : 0;
                int manpower = int.TryParse(manpowerString, out manpower) ? manpower : 0;
                int hourPerDay = int.TryParse(hoursPerDayString, out hourPerDay) ? hourPerDay : 0;

                bool validData = pcsPerManHour > 0 && manpower > 0 && hourPerDay > 0;

                int totalHourNeeded = 0;
                int totalDayNeeded = 0;

                int balanceHour = 0;

                if (maxAvailability != -1)
                {
                    
                    if (validData)
                    {
                        totalHourNeeded = maxAvailability / (pcsPerManHour * manpower);
                        totalDayNeeded = totalHourNeeded / hourPerDay;

                        balanceHour = totalHourNeeded - (totalDayNeeded * hourPerDay);


                        timeNeededString = totalDayNeeded + " Day & " + balanceHour + " Hour(s)";
                    }
                }

            }

            lblTimesNeeded.Text = timeNeededString;
        }

        private void CalculateTimeNeeded(int rowIndex)
        {
            DataGridView dgv = dgvItemList;
            string timeNeededString = "Please Select a Valid Plan";

            if (rowIndex >= 0 && rowIndex < dgv.RowCount)
            {
                int maxAvailability = int.TryParse(dgv.Rows[rowIndex].Cells[header_MaxAvailability].Value.ToString(), out maxAvailability) ? maxAvailability : -1;

                int pcsPerManHour = int.TryParse(pcsPerManHourString, out pcsPerManHour) ? pcsPerManHour : 0;
                int manpower = int.TryParse(manpowerString, out manpower) ? manpower : 0;
                int hourPerDay = int.TryParse(hoursPerDayString, out hourPerDay) ? hourPerDay : 0;

                bool validData = pcsPerManHour > 0 && manpower > 0 && hourPerDay > 0;

                int totalHourNeeded = 0;
                int totalDayNeeded = 0;

                int balanceHour = 0;

                if (maxAvailability != -1)
                {

                    if (validData)
                    {
                        totalHourNeeded = maxAvailability / (pcsPerManHour * manpower);
                        totalDayNeeded = totalHourNeeded / hourPerDay;

                        balanceHour = totalHourNeeded - (totalDayNeeded * hourPerDay);


                        timeNeededString = totalDayNeeded + " Day & " + balanceHour + " Hour(s)";
                    }
                }

            }

            lblTimesNeeded.Text = timeNeededString;
        }

        private DateTime EstimateDateEnd(DateTime startDate, int maxAvailability)
        {
            DataGridView dgv = dgvItemList;
            DateTime endDate = new DateTime();

            if (dgv.CurrentCell != null)
            {
                int rowIndex = dgv.CurrentCell.RowIndex;

                int pcsPerManHour = int.TryParse(pcsPerManHourString, out pcsPerManHour) ? pcsPerManHour : 0;
                int manpower = int.TryParse(manpowerString, out manpower) ? manpower : 0;
                int hourPerDay = int.TryParse(hoursPerDayString, out hourPerDay) ? hourPerDay : 0;

                bool validData = pcsPerManHour > 0 && manpower > 0 && hourPerDay > 0;

                int totalHourNeeded = 0;
                int totalDayNeeded = 0;

                int balanceHour = 0;

                if (maxAvailability != -1)
                {
                    if (validData)
                    {
                        totalHourNeeded = maxAvailability / (pcsPerManHour * manpower);
                        totalDayNeeded = totalHourNeeded / hourPerDay;

                        balanceHour = totalHourNeeded - (totalDayNeeded * hourPerDay);


                        //timeNeededString = totalDayNeeded + " Day & " + balanceHour + " Hour(s)";
                    }
                }

                if (totalDayNeeded > 0)
                {
                    totalDayNeeded--;
                }

                endDate = startDate.AddDays(totalDayNeeded);

                if (balanceHour > 0)
                {
                    endDate = endDate.AddDays(1);
                }

                if (cbSkipSunday.Checked)
                {
                    int totalSunday = 0;

                    while (startDate != endDate)
                    {
                        if (startDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            totalSunday++;
                        }

                        startDate = startDate.AddDays(1);
                    }

                    if (endDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        totalSunday++;
                    }

                    endDate = endDate.AddDays(totalSunday);

                }

                //if (cbEstimate.Checked)
                //{
                //    dtpEnd.Value = endDate;
                //}
            }

            return endDate;
        }

        private void txtPcsPerManHour_TextChanged(object sender, EventArgs e)
        {
            CalculateTimeNeeded();
        }

        private void txtManpower_TextChanged(object sender, EventArgs e)
        {
            CalculateTimeNeeded();
        }

        private void txtHoursPerDay_TextChanged(object sender, EventArgs e)
        {
            int hourPerDay = int.TryParse(pcsPerManHourString, out hourPerDay) ? hourPerDay : 0;

            if(hourPerDay > 24)
            {
                hoursPerDayString = "24";
            }
            else
            {
                CalculateTimeNeeded();
            }
           
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            CalculateTimeNeeded();
        }

        private void cbSkipSunday_CheckedChanged(object sender, EventArgs e)
        {
            UpdateScheduleDate();
        }

        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            int rowIndex = e.RowIndex;

            if(rowIndex != -1 && DateSelectMode)
            {
                //DateTime selectedDateStart = DateTime.TryParse(dgv.Rows[rowIndex].Cells[header_DateStart].Value.ToString(), out selectedDateStart) ? selectedDateStart : DateTime.Today.Date;

                //dtpStart.Value = selectedDateStart;

                //string macID = dgv.Rows[rowIndex].Cells[header_Mac].Value.ToString();
                //cmbMac.Text = macID;

                CalculateTimeNeeded();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            frmTimeNeededSetting frm = new frmTimeNeededSetting(pcsPerManHourString,manpowerString,hoursPerDayString)
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            frm.ShowDialog();

            if(frmTimeNeededSetting.dataSaved)
            {
                pcsPerManHourString = frmTimeNeededSetting.pcsPerManHour;
                manpowerString = frmTimeNeededSetting.manpower;
                hoursPerDayString = frmTimeNeededSetting.hoursPerDay;

                CalculateTimeNeeded();
            }

        }

        private void btnDateSelect_Click(object sender, EventArgs e)
        {
            //bool ifDataValid = false;

            //if (dgvItemList.CurrentCell != null)
            //{
            //    int rowIndex = dgvItemList.CurrentCell.RowIndex;

            //    ifDataValid = dgvItemList.Rows[rowIndex].Selected;

            //    DateTime dateStart = dtpStart.Value;
            //    DateTime dateEnd = dtpEnd.Value;
            //    string lineNo = cmbMac.Text;

            //    ifDataValid &= cmbMac.SelectedIndex != -1;
            //    ifDataValid &= dateStart != null;
            //    ifDataValid &= dateEnd != null;

            //    if(ifDataValid)
            //    {
            //        dgvItemList.Rows[rowIndex].Cells[headerStart].Value = dateStart.Date;
            //        dgvItemList.Rows[rowIndex].Cells[headerEnd].Value = dateEnd.Date;
            //        dgvItemList.Rows[rowIndex].Cells[header_Mac].Value = lineNo;
            //    }
            //}

            //if(!ifDataValid)
            //{
            //    MessageBox.Show("Invalid Date");
            //}
            //else
            //{
            //    IfDateClash();
            //}
        }

        private bool IfDateClash()
        {
            bool clashed = false;

            DataTable dt = (DataTable)dgvItemList.DataSource;
           

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvItemList.Rows[i].Cells[header_DateStart].Style.ForeColor = Color.Black;
                dgvItemList.Rows[i].Cells[header_DateEnd].Style.ForeColor = Color.Black;
                dgvItemList.Rows[i].Cells[header_Mac].Style.ForeColor = Color.Black;

                dgvItemList.Rows[i].Cells[header_DateStart].Style.BackColor = Color.White;
                dgvItemList.Rows[i].Cells[header_DateEnd].Style.BackColor = Color.White;
                dgvItemList.Rows[i].Cells[header_Mac].Style.BackColor = Color.White;

            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime dateStart = DateTime.TryParse(dt.Rows[i][header_DateStart].ToString(), out dateStart) ? dateStart : DateTime.MinValue;
                DateTime dateEnd = DateTime.TryParse(dt.Rows[i][header_DateEnd].ToString(), out dateEnd) ? dateEnd : DateTime.MaxValue;
                string macID = dt.Rows[i][header_Mac].ToString();

                if(dateStart != DateTime.MinValue && dateEnd != DateTime.MaxValue && !string.IsNullOrEmpty(macID))
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        DateTime dateStartCompare = DateTime.TryParse(dt.Rows[j][header_DateStart].ToString(), out dateStartCompare) ? dateStartCompare : DateTime.MinValue;
                        DateTime dateEndCompare = DateTime.TryParse(dt.Rows[j][header_DateEnd].ToString(), out dateEndCompare) ? dateEndCompare : DateTime.MaxValue;
                        string macIDCompare = dt.Rows[j][header_Mac].ToString();

                        if (macIDCompare == macID && dateStartCompare != DateTime.MinValue && dateEndCompare != DateTime.MaxValue)
                        {
                            ////if start date clash
                            //if(dateStart < dateEndCompare && dateStart > dateStartCompare || (dateEnd > dateStartCompare && dateEnd < dateEndCompare))
                            //{
                                
                            //}

                            if(dateStart == dateStartCompare)
                            {
                                //yellow color
                                dgvItemList.Rows[i].Cells[header_DateStart].Style.BackColor = Color.Yellow;
                                dgvItemList.Rows[i].Cells[header_DateEnd].Style.BackColor = Color.Yellow;
                                dgvItemList.Rows[i].Cells[header_Mac].Style.BackColor = Color.Yellow;

                                dgvItemList.Rows[j].Cells[header_DateStart].Style.BackColor = Color.Yellow;
                                dgvItemList.Rows[j].Cells[header_DateEnd].Style.BackColor = Color.Yellow;
                                dgvItemList.Rows[j].Cells[header_Mac].Style.BackColor = Color.Yellow;
                            }

                            if (dateEnd < dateStartCompare || dateEnd == dateStartCompare)
                            {

                            }
                            else if (dateStart > dateEndCompare || dateStart == dateEndCompare)
                            {

                            }
                            else
                            {
                                //red color
                                dgvItemList.Rows[i].Cells[header_DateStart].Style.ForeColor = Color.Red;
                                dgvItemList.Rows[i].Cells[header_DateEnd].Style.ForeColor = Color.Red;
                                dgvItemList.Rows[i].Cells[header_Mac].Style.ForeColor = Color.Red;

                                dgvItemList.Rows[j].Cells[header_DateStart].Style.ForeColor = Color.Red;
                                dgvItemList.Rows[j].Cells[header_DateEnd].Style.ForeColor = Color.Red;
                                dgvItemList.Rows[j].Cells[header_Mac].Style.ForeColor = Color.Red;

                                clashed = true;
                            }

                            
                        }
                    }
                }
            }
            return clashed;
        }

        private void dgvItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #region Drag And Drop

        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        //private bool dragFromItemList = false;
        private bool dragFromSchedule = false;

        // private bool dragFromItemList = false;

        private void dgvItemList_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                string addedStatus = "";

                if(rowIndexFromMouseDown != -1)
                {
                    addedStatus =  dgvItemList.Rows[rowIndexFromMouseDown].Cells[header_AddedStatus].Value.ToString();
                }

                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.  
                    if(addedStatus == text_Added)
                    {
                        DragDropEffects dropEffect = dgv.DoDragDrop(
                        dgv.Rows[rowIndexFromMouseDown],
                        DragDropEffects.None);
                    }
                    else if (dragFromSchedule)
                    {
                        DragDropEffects dropEffect = dgv.DoDragDrop(
                        dgv.Rows[rowIndexFromMouseDown],
                        DragDropEffects.Copy);
                    }
                    else
                    {
                        DragDropEffects dropEffect = dgv.DoDragDrop(
                         dgv.Rows[rowIndexFromMouseDown],
                         DragDropEffects.Move | DragDropEffects.Copy);
                    }
                }
            }
        }

        private void dgvItemList_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dragFromSchedule = false;
            //dragFromItemList = true;

            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dgv.HitTest(e.X, e.Y).RowIndex;

            if (rowIndexFromMouseDown != -1)
            {
                CalculateTimeNeeded(rowIndexFromMouseDown);
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dgvItemList_DragOver(object sender, DragEventArgs e)
        {
            if (!dragFromSchedule)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void dgvItemList_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dgv.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
                dgv.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Copy)
            {
                DataTable dt_Schedule = (DataTable)dgvSchedule.DataSource;

                if (rowIndexFromMouseDown >= 0)
                {
                    ResumeItemRow(rowIndexFromMouseDown);

                    int macID = int.TryParse(dgvSchedule.Rows[rowIndexFromMouseDown].Cells[header_Mac].Value.ToString(), out macID)? macID : -1;

                    dt_Schedule.Rows.RemoveAt(rowIndexFromMouseDown);

                    //check if machine in idle status
                    IfMachineIdle(macID);
                    //if(macID != -1)
                    //{
                    //    bool isIdle = true;
                    //    int indexToInsert = 0;
                    //    int index = 0;

                    //    foreach(DataRow row in dt_Schedule.Rows)
                    //    {
                    //        int macID_Check = int.TryParse(row[header_Mac].ToString(), out macID_Check) ? macID_Check : -1;

                    //        if (macID == macID_Check)
                    //        {
                    //            isIdle = false;
                    //        }

                    //        if (macID_Check < macID)
                    //        {
                    //            indexToInsert = index + 1;
                    //        }

                    //        index++;
                    //    }
                    //    //for(int i = 0; i < dt_Schedule.Rows.Count; i ++)
                    //    //{
                    //    //    int macID_Check = int.TryParse(dt_Schedule.Rows[i][header_Mac].ToString(), out macID_Check) ? macID_Check : -1;

                    //    //    if (macID == macID_Check)
                    //    //    {
                    //    //        isIdle = false;
                    //    //    }

                    //    //    if(macID_Check < macID)
                    //    //    {
                    //    //        indexToInsert = i++;
                    //    //    }
                    //    //}

                    //    if(isIdle)
                    //    {
                    //        //insert idle row at indexToInsert
                    //        DataRow newRow = dt_Schedule.NewRow();

                    //        newRow[header_Mac] = macID;
                    //        newRow[header_Status] = text.planning_status_idle;

                    //        dt_Schedule.Rows.InsertAt(newRow, indexToInsert);

                    //        dgvSchedule.Rows[indexToInsert].Selected = true;
                    //    }


                    //}
                }

            }
        }

        private void IfMachineIdle(int macID)
        {
            if (macID != -1)
            {
                DataTable dt_Schedule = (DataTable)dgvSchedule.DataSource;

                bool isIdle = true;
                int indexToInsert = 0;
                int index = 0;

                foreach (DataRow row in dt_Schedule.Rows)
                {
                    int macID_Check = int.TryParse(row[header_Mac].ToString(), out macID_Check) ? macID_Check : -1;

                    if (macID == macID_Check)
                    {
                        isIdle = false;
                    }

                    if (macID_Check < macID)
                    {
                        indexToInsert = index + 1;
                    }

                    index++;
                }
                
                if (isIdle)
                {
                    //insert idle row at indexToInsert
                    DataRow newRow = dt_Schedule.NewRow();

                    newRow[header_Mac] = macID;
                    newRow[header_Status] = text.planning_status_idle;

                    dt_Schedule.Rows.InsertAt(newRow, indexToInsert);

                    dgvSchedule.Rows[indexToInsert].Selected = true;
                }


            }
        }

        private void dgvSchedule_DragOver(object sender, DragEventArgs e)
        {
            if (dragFromSchedule)
            {
                DataGridView dgv = (DataGridView)sender;
                Point clientPoint = dgv.PointToClient(new Point(e.X, e.Y));
                int indexToDrop = dgv.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                string first_MacID = "1";
                string second_MacID = "2";

                if (dgv.HitTest(clientPoint.X, clientPoint.Y).Type == DataGridViewHitTestType.None)
                {
                    //get index of last row of schedule list
                    indexToDrop = dgv.Rows.Count - 1;
                }
                else if (indexToDrop < 0)
                {
                    //get index of first row of schedule list
                    indexToDrop = 0;
                }

                if (indexToDrop != -1 && rowIndexFromMouseDown != -1)
                {
                    first_MacID = dgv.Rows[rowIndexFromMouseDown].Cells[header_Mac].Value.ToString();
                    second_MacID = dgv.Rows[indexToDrop].Cells[header_Mac].Value.ToString();
                }

                string macStatus = dgv.Rows[rowIndexFromMouseDown].Cells[headerStatus].Value.ToString();

                if (macStatus != text.planning_status_idle)//first_MacID == second_MacID
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
                

            }
            else
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void dgvSchedule_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dragFromSchedule = false;
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dgv.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
                dgv.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            DataTable dt_ItemList = (DataTable)dgvItemList.DataSource;
            DataTable dt_Schedule = (DataTable)dgvSchedule.DataSource;

            string addedStatus = "";

            if(rowIndexFromMouseDown <= dgvItemList.Rows.Count - 1)
            {
                addedStatus = dgvItemList.Rows[rowIndexFromMouseDown].Cells[header_AddedStatus].Value.ToString();
            }

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Copy && addedStatus != text_Added)
            {
                dgv.ClearSelection();


                if (dgv.HitTest(clientPoint.X, clientPoint.Y).Type == DataGridViewHitTestType.None)
                {
                    //get index of last row of schedule list
                    rowIndexOfItemUnderMouseToDrop = dt_Schedule.Rows.Count - 1;
                }
                else if (rowIndexOfItemUnderMouseToDrop < 0)
                {
                    //get index of first row of schedule list
                    rowIndexOfItemUnderMouseToDrop = 0;
                }

                RowDataTransfer(rowIndexFromMouseDown, rowIndexOfItemUnderMouseToDrop);
                GreyItemRow(rowIndexFromMouseDown);

                #region old way
                //DataGridViewRow rowToMove = e.Data.GetData(
                //    typeof(DataGridViewRow)) as DataGridViewRow;

                //DataRow toInsert = dt_Schedule.NewRow();
                //DataRow sourceRow = dt_ItemList.Rows[rowIndexFromMouseDown];
                //toInsert.ItemArray = sourceRow.ItemArray.Clone() as object[];

                // insert in the desired place

                //if (rowIndexOfItemUnderMouseToDrop < 0)
                //{
                //    dt_Schedule.Rows.Add(toInsert);
                //    dgv.Rows[dt_Schedule.Rows.Count - 1].Selected = true;
                //}
                //else
                //{
                //    dt_Schedule.Rows.InsertAt(toInsert, rowIndexOfItemUnderMouseToDrop);
                //    dgv.Rows[rowIndexOfItemUnderMouseToDrop].Selected = true;
                //}

                //DgvUIEdit(dgvSchedule);
                #endregion
            }
            else if(e.Effect == DragDropEffects.Move)
            {
                //compare if machine id same
                if(rowIndexFromMouseDown != -1)
                {
                    string first_MacID = dt_Schedule.Rows[rowIndexFromMouseDown][header_Mac].ToString();

                    if (rowIndexOfItemUnderMouseToDrop != -1)
                    {
                        string second_MacID = dt_Schedule.Rows[rowIndexOfItemUnderMouseToDrop][header_Mac].ToString();

                        if(first_MacID == second_MacID)
                        {
                            RowMove(rowIndexFromMouseDown, rowIndexOfItemUnderMouseToDrop);
                            
                        }
                        else if (first_MacID != second_MacID)
                        {
                            DialogResult dialogResult = MessageBox.Show("Confirm to change machine from "+first_MacID+" to "+second_MacID+"?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dialogResult == DialogResult.Yes)
                            {
                                RowMove(rowIndexFromMouseDown, rowIndexOfItemUnderMouseToDrop);
                                
                            }
                        }
                      
                    }
                    else if (dgv.HitTest(clientPoint.X, clientPoint.Y).Type == DataGridViewHitTestType.None)
                    {
                        string second_MacID = dt_Schedule.Rows[dt_Schedule.Rows.Count - 1][header_Mac].ToString();

                        if (first_MacID == second_MacID && dt_Schedule.Rows.Count - 1 != rowIndexFromMouseDown)
                        {
                            RowMoveToLast(dt_Schedule, rowIndexFromMouseDown);
                            
                        }
                        else if(first_MacID != second_MacID)
                        {
                            DialogResult dialogResult = MessageBox.Show("Confirm to change machine from " + first_MacID + " to " + second_MacID + "?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dialogResult == DialogResult.Yes)
                            {
                                RowMoveToLast(dt_Schedule, rowIndexFromMouseDown);
                               
                            }
                        }

                    }
                    else if (rowIndexOfItemUnderMouseToDrop < 0)
                    {
                        string second_MacID = dt_Schedule.Rows[0][header_Mac].ToString();

                        if (first_MacID == second_MacID && rowIndexFromMouseDown != 0 )
                        {
                            RowMoveToFirst(dt_Schedule, rowIndexFromMouseDown);
                            
                        }
                        else if(first_MacID != second_MacID)
                        {
                            DialogResult dialogResult = MessageBox.Show("Confirm to change machine from " + first_MacID + " to " + second_MacID + "?", "Message",
                                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dialogResult == DialogResult.Yes)
                            {
                                RowMoveToFirst(dt_Schedule, rowIndexFromMouseDown);
                               
                            }
                        }


                    }
                }
               
            }
        }

        private void dgvSchedule_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.  
                    DragDropEffects dropEffect = dgv.DoDragDrop(
                         dgv.Rows[rowIndexFromMouseDown],
                         DragDropEffects.Move | DragDropEffects.Copy);
                }
            }
        }

        private void dgvSchedule_MouseDown(object sender, MouseEventArgs e)
        {
            dgvItemList.ClearSelection();
            DataGridView dgv = (DataGridView)sender;
            dragFromSchedule = true;
            //dragFromItemList = false;

            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dgv.HitTest(e.X, e.Y).RowIndex;
            int colIndex = dgv.HitTest(e.X, e.Y).ColumnIndex;

            if (e.Button == MouseButtons.Right && rowIndexFromMouseDown >= 0)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();


                dgv.CurrentCell = dgv.Rows[rowIndexFromMouseDown].Cells[colIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[rowIndexFromMouseDown].Selected = true;
                dgv.Focus();

                string macStatus = dgv.Rows[rowIndexFromMouseDown].Cells[header_Status].Value.ToString();

                if (macStatus == text.planning_status_pending || macStatus == text_ToAdd)
                {
                    my_menu.Items.Add(text_EditStartDate).Name = text_EditStartDate;
                    my_menu.Items.Add(text_RearrangeDate).Name = text_RearrangeDate;
                }

                my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                my_menu.ItemClicked += new ToolStripItemClickedEventHandler(Schedule_ItemClicked);

            }

            else if (rowIndexFromMouseDown != -1)
            {
                int itemListRowIndex = int.TryParse(dgv.Rows[rowIndexFromMouseDown].Cells[header_Index].Value.ToString(), out itemListRowIndex) ? itemListRowIndex : -1;
                CalculateTimeNeeded(itemListRowIndex - 1);
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            }

            else
            {
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
            }
                

           
        }

        private void Schedule_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DataGridView dgv = dgvSchedule;

                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0)
                {
                    string ClickedItem = e.ClickedItem.Name.ToString();

                    //if (ClickedItem.Equals(text_CompleteDO))
                    //{
                    //    CompleteDO(rowIndex);
                    //}
                    //else if (ClickedItem.Equals(text_InCompleteDO))
                    //{
                    //    IncompleteDO(rowIndex);
                    //    //MessageBox.Show("incomplete do");
                    //}
                    //else if (ClickedItem.Equals(text_UndoRemove))
                    //{
                    //    DOUndoRemove(rowIndex);
                    //    //MessageBox.Show("undo remove");
                    //}
                    //else if (ClickedItem.Equals(text_ChangeDONumber))
                    //{
                    //    ChangeDONumber(rowIndex);
                    //    //MessageBox.Show("Change D/O Number");
                    //}
                    //else if (ClickedItem.Equals(text_MasterList))
                    //{
                    //    GetMasterList();
                    //}
                }



                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void GreyItemRow(int rowIndex)
        {
            dgvItemList.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvItemList.Rows[rowIndex].Cells[header_AddedStatus].Value = text_Added;
        }

        private void ResumeItemRow(int scheduleRowIndex)
        {
            scheduleRowIndex = int.TryParse(dgvSchedule.Rows[scheduleRowIndex].Cells[header_Index].Value.ToString(), out scheduleRowIndex) ? scheduleRowIndex : -1;

            if (scheduleRowIndex != -1)
            {
                for (int i = 0; i < dgvItemList.RowCount; i++)
                {
                    int itemIndex = int.TryParse(dgvItemList.Rows[i].Cells[header_Index].Value.ToString(), out itemIndex) ? itemIndex : -1;

                    if (itemIndex == scheduleRowIndex)
                    {
                        dgvItemList.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        dgvItemList.Rows[i].Cells[header_AddedStatus].Value = text_ToAdd;

                        dgvItemList.Rows[i].Cells[header_DateStart].Value = DBNull.Value;
                        dgvItemList.Rows[i].Cells[header_DateEnd].Value = DBNull.Value;
                        dgvItemList.Rows[i].Cells[header_Mac].Value = DBNull.Value;

                        return;
                    }
                }

            }

        }

        private void RowDataTransfer(int sourceRowIndex, int RowIndexToDrop)
        {
            DataGridView dgv = dgvSchedule;
            DataTable dt_Schedule = (DataTable)dgvSchedule.DataSource;
            DataTable dt_ItemList = (DataTable)dgvItemList.DataSource;

            string macStatus = dt_Schedule.Rows[RowIndexToDrop][header_Status].ToString();

            string macID = dt_Schedule.Rows[RowIndexToDrop][header_Mac].ToString();

            if (macStatus == text.planning_status_running || macStatus == text.planning_status_pending || macStatus == text_ToAdd)
            {
                RowIndexToDrop++;
            }

            DataRow newRow = dt_Schedule.NewRow();

            newRow[header_Index] = dt_ItemList.Rows[sourceRowIndex][header_Index];
            newRow[header_Status] = "TO ADD";
            newRow[header_Mac] = macID;
            newRow[header_ItemString] = dt_ItemList.Rows[sourceRowIndex][header_ItemString];
            newRow[header_DateStart] = dt_ItemList.Rows[sourceRowIndex][header_DateStart];
            newRow[header_EstimateEnd] = dt_ItemList.Rows[sourceRowIndex][header_DateEnd];
            newRow[header_Qty] = dt_ItemList.Rows[sourceRowIndex][header_MaxAvailability];

            int maxAvailability = int.TryParse(dt_ItemList.Rows[sourceRowIndex][header_MaxAvailability].ToString(), out maxAvailability) ? maxAvailability : -1;

            int pcsPerManHour = int.TryParse(pcsPerManHourString, out pcsPerManHour) ? pcsPerManHour : 0;
            int manpower = int.TryParse(manpowerString, out manpower) ? manpower : 0;
            int hourPerDay = int.TryParse(hoursPerDayString, out hourPerDay) ? hourPerDay : 0;

            newRow[header_PcsPerManHour] = pcsPerManHour;
            newRow[header_Manpower] = manpower;
            newRow[header_HoursPerDay] = hourPerDay;

            bool validData = pcsPerManHour > 0 && manpower > 0 && hourPerDay > 0;

            if (RowIndexToDrop < 0)
            {
                dt_Schedule.Rows.Add(newRow);
                dgv.Rows[dt_Schedule.Rows.Count - 1].Selected = true;

                DateInitial(dt_Schedule.Rows.Count - 1, maxAvailability);
            }
            else
            {
                if (macStatus == text.planning_status_idle)
                {
                    //remove idle row
                    DataRow selected_Row = dt_Schedule.Rows[RowIndexToDrop];
                    dt_Schedule.Rows.Remove(selected_Row);
                }

                dt_Schedule.Rows.InsertAt(newRow, RowIndexToDrop);
                dgv.Rows[RowIndexToDrop].Selected = true;
                DateInitial(RowIndexToDrop, maxAvailability);
            }

            #region date calculate
            //int totalHourNeeded = 0;
            //int totalDayNeeded = 0;

            //int balanceHour = 0;

            //if (maxAvailability != -1)
            //{
            //    DateTime startDate = dtpStart.Value.Date;
            //    if (validData)
            //    {
            //        totalHourNeeded = maxAvailability / (pcsPerManHour * manpower);
            //        totalDayNeeded = totalHourNeeded / hourPerDay;

            //        balanceHour = totalHourNeeded - (totalDayNeeded * hourPerDay);
            //    }


            //    if (totalDayNeeded > 0)
            //    {
            //        totalDayNeeded--;
            //    }

            //    DateTime endDate = startDate.AddDays(totalDayNeeded);

            //    if (totalHourNeeded > hourPerDay)
            //    {
            //        endDate = endDate.AddDays(1);
            //    }

            //    if (cbSkipSunday.Checked)
            //    {
            //        int totalSunday = 0;
            //        while (startDate != endDate)
            //        {
            //            if (startDate.DayOfWeek == DayOfWeek.Sunday)
            //            {
            //                totalSunday++;
            //            }

            //            startDate = startDate.AddDays(1);
            //        }

            //        if (endDate.DayOfWeek == DayOfWeek.Sunday)
            //        {
            //            totalSunday++;
            //        }

            //        endDate = endDate.AddDays(totalSunday);

            //    }

            //    if (cbEstimate.Checked)
            //    {
            //        dtpEnd.Value = endDate;
            //    }
            //}
            #endregion

            DgvUIEdit(dgvSchedule);
        }

        private void RowMove(int row_1, int row_2)
        {
            if (row_1 != row_2)
            {
                DataTable dt = (DataTable)dgvSchedule.DataSource;
                DataRow selected_Row = dt.Rows[row_1];
                DataRow target_Row = dt.Rows[row_2];

                DataRow newRow = dt.NewRow();
                newRow.ItemArray = selected_Row.ItemArray; // copying data

                dt.Rows.Remove(selected_Row);

                bool machineNoChanged = false;
                int oldMachineNo = int.TryParse(newRow[header_Mac].ToString(), out oldMachineNo) ? oldMachineNo : -1;

                //compare mac id
                if (newRow[header_Mac].ToString() != target_Row[header_Mac].ToString())
                {
                    newRow[header_Mac] = target_Row[header_Mac];
                    row_2++;

                    machineNoChanged = true;
                }

                int rowCount = dt.Rows.Count;

                if (row_2 > rowCount)
                {
                    row_2 = rowCount;
                }

                dt.Rows.InsertAt(newRow, row_2); // index 2

                dt.AcceptChanges();

               

                UpdateScheduleAfterNewRowInserted(int.TryParse(dgvSchedule.Rows[row_2].Cells[header_Mac].Value.ToString(), out int newMachineNo) ? newMachineNo : -1);
                if (machineNoChanged)
                {
                    IfMachineIdle(oldMachineNo);
                    UpdateScheduleAfterNewRowInserted(oldMachineNo);
                    if (target_Row[header_Status].ToString() == text.planning_status_idle)
                    {
                        dt.Rows.Remove(target_Row);
                    }
                }

                rowCount = dt.Rows.Count;

                if (row_2 > rowCount)
                {
                    row_2 = rowCount;
                }

                dgvSchedule.Rows[row_2].Selected = true;
            }

        }

        private void RowMoveToLast(DataTable dt, int row)
        {
            DataRow selectedRow = dt.Rows[row]; // YourDatatable.Rows(0)
            DataRow target_Row = dt.Rows[dt.Rows.Count - 1];

            DataRow newRow = dt.NewRow();

            newRow.ItemArray = selectedRow.ItemArray; // copying data

            dt.Rows.Remove(selectedRow);
            bool machineNoChanged = false;

            int oldMachineNo = int.TryParse(newRow[header_Mac].ToString(), out oldMachineNo) ? oldMachineNo : -1;

            //compare mac id
            if (newRow[header_Mac].ToString() != target_Row[header_Mac].ToString())
            {
                newRow[header_Mac] = target_Row[header_Mac];

                machineNoChanged = true;
            }

            dt.Rows.Add(newRow);

            

            UpdateScheduleAfterNewRowInserted(int.TryParse(dgvSchedule.Rows[dgvSchedule.Rows.Count - 1].Cells[header_Mac].Value.ToString(), out int newMachineNo) ? newMachineNo : -1);

            if (machineNoChanged)
            {
                IfMachineIdle(oldMachineNo);
                UpdateScheduleAfterNewRowInserted(oldMachineNo);

                if (target_Row[header_Status].ToString() == text.planning_status_idle)
                {
                    dt.Rows.Remove(target_Row);
                }
            }
            dgvSchedule.Rows[dgvSchedule.Rows.Count - 1].Selected = true;
        }

        private void RowMoveToFirst(DataTable dt, int row)
        {
            DataRow selectedRow = dt.Rows[row];
            DataRow target_Row = dt.Rows[0];
            DataRow newRow = dt.NewRow();

            newRow.ItemArray = selectedRow.ItemArray; // copying data

            dt.Rows.Remove(selectedRow);
            bool machineNoChanged = false;

            int oldMachineNo = int.TryParse(newRow[header_Mac].ToString(), out oldMachineNo) ? oldMachineNo : -1;

            //compare mac id
            if (newRow[header_Mac].ToString() != target_Row[header_Mac].ToString())
            {
                newRow[header_Mac] = target_Row[header_Mac];
                machineNoChanged = true;
            }

            dt.Rows.InsertAt(newRow, 0);

            

            UpdateScheduleAfterNewRowInserted(int.TryParse(dgvSchedule.Rows[0].Cells[header_Mac].Value.ToString(), out int newMachineNo) ? newMachineNo : -1);

            if (machineNoChanged)
            {
                IfMachineIdle(oldMachineNo);

                UpdateScheduleAfterNewRowInserted(oldMachineNo);

                if (target_Row[header_Status].ToString() == text.planning_status_idle)
                {
                    dt.Rows.Remove(target_Row);
                }
            }
            dgvSchedule.Rows[0].Selected = true;
        }

        #endregion

        private void cbSameDay_CheckedChanged(object sender, EventArgs e)
        {
            UpdateScheduleDate();
        }
    }
}
