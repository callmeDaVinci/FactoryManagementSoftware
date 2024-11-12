using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Drawing;
using System.Linq;
using Font = System.Drawing.Font;



namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderRequestNotice : Form
    {
        private MainDashboard mainDashboardInstance;

        public frmOrderRequestNotice()
        {
            InitializeComponent();
            LoadRequestingOrder();
        }

        public frmOrderRequestNotice(MainDashboard dashboard)
        {
            InitializeComponent();
            LoadRequestingOrder();
            mainDashboardInstance = dashboard;
        }

        #region variable declare


        orderActionBLL uOrderAction = new orderActionBLL();
        orderActionDAL dalOrderAction = new orderActionDAL();
        ordBLL uOrd = new ordBLL();
        ordDAL dalOrd = new ordDAL();

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        joinDAL dalJoin = new joinDAL();
        userDAL dalUser = new userDAL();

        Tool tool = new Tool();

        Text text = new Text();

        
        //readonly string headerPONO = "P/O NO";
        readonly string headerID = "ID";
        readonly string headerDateRequired = "DATE REQUIRED";
        readonly string headerCat = "CATEGORY";
        readonly string headerOrdered = "ORDERED";
        //readonly string headerPending = "PENDING / OVER";
        //readonly string headerReceived = "RECEIVED";
        readonly string headerUnit = "UNIT";
        readonly string headerStatus = "STATUS";
        readonly string headerType = "TYPE";
        readonly string headerCode = "CODE";
        readonly string headerName = "NAME";
        #endregion

        #region UI Design

        private DataTable NewOrderRecordTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerID, typeof(int));
            dt.Columns.Add(headerDateRequired, typeof(DateTime));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerCat, typeof(string));
            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerName, typeof(string));
            //dt.Columns.Add(headerPONO, typeof(int));
            dt.Columns.Add(headerOrdered, typeof(float));
            //dt.Columns.Add(headerPending, typeof(float));
            //dt.Columns.Add(headerReceived, typeof(float));
            dt.Columns.Add(headerUnit, typeof(string));
            dt.Columns.Add(headerStatus, typeof(string));

            return dt;
        }


        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerOrdered].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerDateRequired].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerType].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerCat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerUnit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[headerOrdered].DefaultCellStyle.Format = "0.###";

            dgv.Columns[headerCode].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            dgv.Columns[headerName].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[headerType].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgv.Columns[headerDateRequired].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            dgv.Columns[headerCat].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgv.Columns[headerUnit].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
            dgv.Columns[headerStatus].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);

        }

        private void dgvMatUsedReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            //DataGridView dgv = dgvAlertSummary;
            //dgv.SuspendLayout();

            //int row = e.RowIndex;
            //int col = e.ColumnIndex;

            //string colName = dgv.Columns[col].Name;

            //if (colName == text.Header_MatCode)
            //{
            //    string matCode = dgv.Rows[row].Cells[text.Header_MatCode].Value.ToString();

            //    if (matCode == "")
            //    {
            //        dgv.Rows[row].Height = 3;
            //        dgv.Rows[row].DefaultCellStyle.BackColor = Color.LightGray;
            //        //dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            //    }
            //    else
            //    {
            //        dgv.Rows[row].Height = 50;
            //    }
            //}
            //else if (colName.Contains(text.str_EstBalance))
            //{
            //    float bal = float.TryParse(dgv.Rows[row].Cells[colName].Value.ToString(), out float x) ? x : 0;

            //    if (bal < 0)
            //    {
            //        dgv.Rows[row].Cells[colName].Style.ForeColor = Color.Red;
            //    }
            //    else
            //    {
            //        dgv.Rows[row].Cells[colName].Style.ForeColor = Color.Black;

            //    }
            //}
            //dgv.ResumeLayout();
        }

        private void dgvMatUsedReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //dgvMatUsedUIEdit(dgvAlertSummary);
            //dgvAlertSummary.AutoResizeColumns();
        }

        #endregion

        #region DB DATA Control

        private void LoadRequestingOrder()
        {
            DataTable dtOrder = NewOrderRecordTable();
            DataRow dtOrder_row;

            string statusSearch = "REQUESTING";
            DataTable dt = dalOrd.StatusSearch(statusSearch);

            dt.DefaultView.Sort = "ord_added_date DESC";
            DataTable sortedDt = dt.DefaultView.ToTable();

            foreach (DataRow ord in sortedDt.Rows)
            {
                string ordStatus = ord["ord_status"].ToString();

                if (ordStatus.Equals(statusSearch))
                {
                    int orderID = Convert.ToInt32(ord["ord_id"].ToString());

                    if (orderID >= 1)
                    {
                        dtOrder_row = dtOrder.NewRow();

                        string itemCode = ord["ord_item_code"].ToString();
                        string ordID = ord["ord_id"].ToString();

                        dtOrder_row[headerID] = ordID;
                        //lblDebug.Text = "before date assign";

                        DateTime requiredDate = DateTime.ParseExact(Convert.ToDateTime(ord["ord_required_date"]).ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
                        //lblDebug.Text = "After requiredDateAssign";
                        dtOrder_row[headerDateRequired] = requiredDate;
                        //lblDebug.Text = "After date assign";
                        dtOrder_row[headerType] = ord["ord_type"].ToString();
                        dtOrder_row[headerCat] = ord["item_cat"].ToString();
                        dtOrder_row[headerCode] = itemCode;
                        dtOrder_row[headerName] = ord["item_name"].ToString();
                        dtOrder_row[headerOrdered] = ord["ord_qty"].ToString();
                        dtOrder_row[headerUnit] = ord["ord_unit"].ToString();
                        dtOrder_row[headerStatus] = ordStatus;
                        dtOrder.Rows.Add(dtOrder_row);
                    }
                }
            }

            dgvAlertSummary.DataSource = null;

            if (dtOrder.Rows.Count > 0)
            {
                dtOrder.DefaultView.Sort = "ID DESC";
                dgvAlertSummary.DataSource = dtOrder;
                dgvUIEdit(dgvAlertSummary);
                dgvAlertSummary.ClearSelection();
            }
            else
            {
                Close();
            }

        }

        #endregion
        private void frmOrderAlert_NEW_Shown(object sender, EventArgs e)
        {
            dgvAlertSummary.ClearSelection();

        }

        static public bool PROCCED_TO_ORDER_PAGE = false;
        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            PROCCED_TO_ORDER_PAGE = true;

            if (!MainDashboard.NewOrdFormOpen)
            {
                frmLoading.ShowLoadingScreen();
                frmOrderAlert_NEW ord = new frmOrderAlert_NEW
                {
                    MdiParent = mainDashboardInstance, // Set MdiParent to MainDashboard
                    StartPosition = FormStartPosition.CenterScreen,
                    WindowState = FormWindowState.Maximized
                };
                ord.Show();
                MainDashboard.NewOrdFormOpen = true;
                frmLoading.CloseForm();
            }
            else
            {
                if (Application.OpenForms.OfType<frmOrderAlert_NEW>().Count() == 1)
                {
                    Application.OpenForms.OfType<frmOrderAlert_NEW>().First().BringToFront();
                }
            }

            Close();
        }
    }
}


