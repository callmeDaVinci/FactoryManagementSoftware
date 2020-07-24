using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace FactoryManagementSoftware.UI
{
    public partial class frmDeliverySchedule : Form
    {
        public frmDeliverySchedule()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvMainList, true);
            tool.DoubleBuffered(dgvSubList, true);

            btnFilter.Text = text_HideFilter;


         
        }

        #region variable/object declare

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


        Tool tool = new Tool();
        Text text = new Text();

        readonly string text_ShowFilter = "SHOW FILTER...";
        readonly string text_HideFilter = "HIDE FILTER";
     

        readonly string header_Status = "STATUS";
        readonly string header_TripNo = "TRIP #";
        readonly string header_StopNo = "STOP #";
        readonly string header_DeliveryDate = "DELIVERY DATE";
        readonly string header_Route = "ROUTE";
        readonly string header_RouteName = "ROUTE NAME";
        readonly string header_RouteTblCode = "ROUTE TBL CODE";
        readonly string header_Customer = "CUSTOMER";
        readonly string header_CustomerName = "CUSTOMER NAME";
        readonly string header_PONo = "P/O NO";
        readonly string header_POTableCode = "P/O TBL CODE";
        readonly string header_POCode = "P/O CODE";
        readonly string header_Pcs = "PCS";
        readonly string header_Bags = "BAG(S)";
        readonly string header_Total = "TOTAL";

        readonly string header_Size = "SIZE";
        readonly string header_SizeString = "SIZE STRING";
        readonly string header_Unit = "UNIT";
        readonly string header_Type = "TYPE";
        readonly string header_ItemName = "ITEM NAME";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_PcsPerBag = "PCS PER BAG";

        readonly string header_DeliveredPcs = "DELIVERED PCS";
        readonly string header_OrderedPcs = "ORDERED PCS";

        #endregion

        private DataTable NewScheduleTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Status, typeof(string));
            dt.Columns.Add(header_TripNo, typeof(int));
            dt.Columns.Add(header_StopNo, typeof(int));
            dt.Columns.Add(header_DeliveryDate, typeof(DateTime));

            dt.Columns.Add(header_RouteName, typeof(string));
            dt.Columns.Add(header_RouteTblCode, typeof(int));
            dt.Columns.Add(header_Route, typeof(string));

            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PONo, typeof(string));
            dt.Columns.Add(header_POTableCode, typeof(int));

            dt.Columns.Add(header_CustomerName, typeof(string));
            dt.Columns.Add(header_Customer, typeof(string));

            dt.Columns.Add(header_Bags, typeof(int));
            dt.Columns.Add(header_Pcs, typeof(int));
            dt.Columns.Add(header_Total, typeof(int));

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_SizeString, typeof(string));
            dt.Columns.Add(header_Unit, typeof(int));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_PcsPerBag, typeof(int));

            dt.Columns.Add(header_DeliveredPcs, typeof(int));
            dt.Columns.Add(header_OrderedPcs, typeof(int));
            return dt;
        }

        private void ShowOrHideFilter()
        {
            string filterText = btnFilter.Text;

            if (filterText == text_ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpDeliveryList.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpDeliveryList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            dgv.Columns[header_RouteName].Visible = false;
            dgv.Columns[header_RouteTblCode].Visible = false;

            //dgv.Columns[header_POCode].Visible = false;
            dgv.Columns[header_PONo].Visible = false;
            //dgv.Columns[header_POTableCode].Visible = false;

            dgv.Columns[header_CustomerName].Visible = false;
            dgv.Columns[header_Pcs].Visible = false;

            dgv.Columns[header_Size].Visible = false;
            dgv.Columns[header_SizeString].Visible = false;
            dgv.Columns[header_Unit].Visible = false;
            dgv.Columns[header_Type].Visible = false;

            dgv.Columns[header_ItemName].Visible = false;
            dgv.Columns[header_ItemCode].Visible = false;

            dgv.Columns[header_DeliveredPcs].Visible = false;
            dgv.Columns[header_OrderedPcs].Visible = false;
            dgv.Columns[header_PcsPerBag].Visible = false;

            //dgv.Columns[header_Code].DefaultCellStyle.ForeColor = Color.Gray;
            //dgv.Columns[header_Code].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

            dgv.Columns[header_Route].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            dgv.Columns[header_Status].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.Columns[header_Total].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            dgv.Columns[header_Bags].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_TripNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_StopNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Status].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_DeliveryDate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Route].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgv.Columns[header_SizeString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_BalAfterDelivery].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;


        }

        private void LoadDeliverySchedule()
        {
            //get data from DB
            DataTable dt_DeliveryData = dalSPP.DeliveryWithInfoSelect();
            DataTable dt_Schedule = NewScheduleTable();

            //filter data
            foreach(DataRow row in dt_DeliveryData.Rows)
            {
                string deliveryTblCode = row[dalSPP.TableCode].ToString();

                if(!string.IsNullOrEmpty(deliveryTblCode))
                {
                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;
                    bool isDelivered = bool.TryParse(row[dalSPP.IsDelivered].ToString(), out isDelivered) ? isDelivered : false;

                    int pcsPerBag = int.TryParse(row[dalSPP.QtyPerBag].ToString(), out pcsPerBag) ? pcsPerBag : 1;
                    int toDeliverQty = int.TryParse(row[dalSPP.DeliverPcs].ToString(), out toDeliverQty) ? toDeliverQty : 0;

                    int deliverBag = toDeliverQty / pcsPerBag;

                    string custShortName = row[dalSPP.ShortName].ToString();
                    string poNo = row[dalSPP.PONo].ToString();
                    int poCode = Convert.ToInt32(row[dalSPP.POCode].ToString());
                    int poTblCode = Convert.ToInt32(row[dalSPP.POTableCode].ToString());
                    int OrderedQty = Convert.ToInt32(row[dalSPP.POQty].ToString());
                    int deliveredQty = Convert.ToInt32(row[dalSPP.DeliveredQty].ToString());
                    int routeTblCode = Convert.ToInt32(row[dalSPP.RouteTblCode].ToString());
                    string itemCode = row[dalSPP.ItemCode].ToString();
                    string itemName = row[dalSPP.ItemName].ToString();
                    string routeName = row[dalSPP.RouteName].ToString();

                    DataRow scheduleRow = dt_Schedule.NewRow();

                    scheduleRow[header_Status] = row[dalSPP.DeliveryStatus].ToString();
                    scheduleRow[header_Customer] = "   "+custShortName + "(" + poNo + ")   ";
                    scheduleRow[header_CustomerName] = custShortName;
                    scheduleRow[header_Pcs] = toDeliverQty;
                    scheduleRow[header_PcsPerBag] = pcsPerBag;
                    scheduleRow[header_Bags] = deliverBag;

                    scheduleRow[header_PONo] = poNo;
                    scheduleRow[header_POCode] = poCode;
                    scheduleRow[header_POTableCode] = poTblCode;

                    scheduleRow[header_OrderedPcs] = OrderedQty;
                    scheduleRow[header_DeliveredPcs] = deliveredQty;
                    scheduleRow[header_ItemCode] = itemCode;
                    scheduleRow[header_ItemName] = itemName;

                    scheduleRow[header_RouteTblCode] = routeTblCode;
                    scheduleRow[header_RouteName] = routeName;
                    scheduleRow[header_Route] = "   ("+ routeTblCode+")"+routeName+"   ";

                    dt_Schedule.Rows.Add(scheduleRow);

                }

            }
            //merge data
            //set data source to dgv
            //edit dgv style

            dgvMainList.DataSource = dt_Schedule;
            DgvUIEdit(dgvMainList);
            dgvMainList.ClearSelection();


        }

        private void frmDeliverySchedule_Load(object sender, EventArgs e)
        {
            ShowOrHideFilter();

            if (dalUser.getPermissionLevel(MainDashboard.USER_ID) <= 3)
            {
                tlpDeliveryList.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
            }

            LoadDeliverySchedule();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilter();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvMainList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = dgvMainList;
            dgv.SuspendLayout();

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (dgv.Columns[j].Name == header_Status)
                    {
                        if (dgv.Rows[i].Cells[j].Value.ToString() == text.Delivery_Processing)
                        {
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(224,224,224);
                        }
                        else
                        {
                            dgv.Rows[i].DefaultCellStyle.BackColor = Color.White;

                            if(dgv.Rows[i].Cells[j].Value.ToString() == text.Delivery_DOOpened)
                            {
                                dgv.Rows[i].Cells[header_Status].Style.BackColor = Color.White;

                            }
                            else if(dgv.Rows[i].Cells[j].Value.ToString() == text.Delivery_DOPending)
                            {
                                dgv.Rows[i].Cells[header_Status].Style.BackColor = Color.FromArgb(253, 203, 110);
                            }
                            else if (dgv.Rows[i].Cells[j].Value.ToString() == text.Delivery_ToDeliver)
                            {
                                dgv.Rows[i].Cells[header_Status].Style.BackColor = Color.FromArgb(52, 139, 209);
                            }
                        }
                       
                    }
                }
            }

            dgv.ResumeLayout();

            dgv.ClearSelection();
        }
    }
}
