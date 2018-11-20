using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMaterialUsedReport : Form
    {
        public frmMaterialUsedReport()
        {
            InitializeComponent();
        }

        #region Valiable Declare

        enum Month
        {
            January = 1,
            Feburary,
            March,
            Apirl,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        enum color
        {
            Gold = 0,
            LightGreen,
            DeepSkyBlue,
            Lavender,
            LightBlue,
            LightCyan,
            LightPink,
            Orange,
            Pink,
            RoyalBlue,
            Silver,
            SteelBlue,
            Tomato
        }

        private int colorOrder = 0;
        private string colorName = "Black";
        private int redAlertLevel = 0;

        #endregion

        #region create class object (database)

        custBLL uCust = new custBLL();
        custDAL dalCust = new custDAL();

        facBLL uFac = new facBLL();
        facDAL dalFac = new facDAL();

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        itemCatBLL uItemCat = new itemCatBLL();
        itemCatDAL dalItemCat = new itemCatDAL();

        trfCatBLL utrfCat = new trfCatBLL();
        trfCatDAL daltrfCat = new trfCatDAL();

        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

        facStockBLL uStock = new facStockBLL();
        facStockDAL dalStock = new facStockDAL();

        itemCustBLL uItemCust = new itemCustBLL();
        itemCustDAL dalItemCust = new itemCustDAL();

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        forecastBLL uForecast = new forecastBLL();
        forecastDAL dalForecast = new forecastDAL();

        materialUsedBLL uMatUsed = new materialUsedBLL();
        materialUsedDAL dalMatUsed = new materialUsedDAL();

        #endregion

        #region load/close data

        private void insertItemQuantityOrderData()
        {
            string custName = cmbCust.Text;

            if (!string.IsNullOrEmpty(custName))
            {
                DataTable dt = dalItemCust.custSearch(custName);

                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("no data under this record.");
                }
                else
                {
                    int outStock = 0;
                    string itemCode;
                    string start = dtpStart.Value.ToString("yyyy/MM/dd");
                    string end = dtpEnd.Value.ToString("yyyy/MM/dd");

                    // string forecastNO = "";
                    int forecastIndex = 1;

                    foreach (DataRow item in dt.Rows)
                    {
                        itemCode = item["item_code"].ToString();

                        DataTable dt3 = daltrfHist.rangeSearch(cmbCust.Text, start, end, itemCode);

                        outStock = 0;

                        if (dt3.Rows.Count > 0)
                        {

                            foreach (DataRow outRecord in dt3.Rows)
                            {
                                outStock += Convert.ToInt32(outRecord["trf_hist_qty"]);

                            }
                        }

                        if (ifGotChild(itemCode))
                        {
                            DataTable dtJoin = dalJoin.parentCheck(itemCode);
                            foreach (DataRow Join in dtJoin.Rows)
                            {
                                uMatUsed.no = forecastIndex;
                                uMatUsed.item_code = Join["join_child_code"].ToString();
                                uMatUsed.quantity_order = outStock;

                                bool result = dalMatUsed.Insert(uMatUsed);
                                if (!result)
                                {
                                    MessageBox.Show("failed to insert material used data");
                                    return;
                                }
                                else
                                {
                                    forecastIndex++;
                                }
                            }
                        }
                        else
                        {
                            uMatUsed.no = forecastIndex;
                            uMatUsed.item_code = itemCode;
                            uMatUsed.quantity_order = outStock;

                            bool result = dalMatUsed.Insert(uMatUsed);
                            if (!result)
                            {
                                MessageBox.Show("failed to insert material used data");
                                return;
                            }
                            else
                            {
                                forecastIndex++;
                            }
                        }
                    }
                }
            }

        }

        private void loadMaterialUsedList()
        {
            insertItemQuantityOrderData();
            int index = 1;
            string materialType = null;
            int OrderQty;
            float totalMaterialUsed = 0;
            float materialUsed = 0;
            float wastageUsed = 0;
            float wastagePercetage = 0;
            float itemWeight;

            DataTable dt = dalMatUsed.Select();
            dt.DefaultView.Sort = "item_material ASC";
            dt = dt.DefaultView.ToTable();

            dgvMaterialUsedRecord.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dgvMaterialUsedRecord.Rows.Add();

                itemWeight = Convert.ToSingle(item["item_part_weight"].ToString());
                OrderQty = Convert.ToInt32(item["quantity_order"].ToString());
                wastagePercetage = Convert.ToSingle(item["item_wastage_allowed"].ToString());

                materialUsed = OrderQty * itemWeight / 1000;

                wastageUsed = materialUsed * wastagePercetage;


                if (string.IsNullOrEmpty(materialType))
                {
                    materialType = item["item_material"].ToString();

                    totalMaterialUsed = materialUsed + wastageUsed;
                }
                else if (materialType == item["item_material"].ToString())
                {
                    //same data
                    totalMaterialUsed += materialUsed + wastageUsed;
                }
                else
                {
                    //first data
                    dgvMaterialUsedRecord.Rows[n - 1].Cells["total_material_used"].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                    dgvMaterialUsedRecord.Rows[n - 1].Cells["total_material_used"].Value = totalMaterialUsed;

                    materialType = item["item_material"].ToString();
                    totalMaterialUsed = materialUsed + wastageUsed;
                    n = dgvMaterialUsedRecord.Rows.Add();
                }

                if (dt.Rows.IndexOf(item) == dt.Rows.Count - 1)
                {
                    // this is the last item
                    totalMaterialUsed = materialUsed + wastageUsed;
                    dgvMaterialUsedRecord.Rows[n].Cells["total_material_used"].Style.Font = new System.Drawing.Font(dgvMaterialUsedRecord.Font, FontStyle.Bold);
                    dgvMaterialUsedRecord.Rows[n].Cells["total_material_used"].Value = totalMaterialUsed;
                }

                dgvMaterialUsedRecord.Rows[n].Cells["no"].Value = index;
                dgvMaterialUsedRecord.Rows[n].Cells["item_material"].Value = item["item_material"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["item_color"].Value = item["item_color"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["item_ord"].Value = item["quantity_order"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["item_part_weight"].Value = itemWeight;
                dgvMaterialUsedRecord.Rows[n].Cells["wastage_allowed"].Value = item["item_wastage_allowed"].ToString();
                dgvMaterialUsedRecord.Rows[n].Cells["material_used"].Value = materialUsed;
                dgvMaterialUsedRecord.Rows[n].Cells["material_used_include_wastage"].Value = materialUsed + wastageUsed;


                index++;

            }

            listPaint(dgvMaterialUsedRecord);

        }

        private void frmMaterialUsedReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.MaterialUsedReportFormOpen = false;
        }

        private void frmMaterialUsedReport_Load(object sender, EventArgs e)
        {
            loadCustomerList();
        }

        private void listPaint(DataGridView dgv)
        {
            bool rowColorChange = true;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;
                if (row.Cells["item_name"].Value == null)
                {
                    row.Height = 3;
                    row.DefaultCellStyle.BackColor = Color.FromName(colorName);

                }
                else if (rowColorChange)
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = SystemColors.Control;
                    rowColorChange = false;
                }
                else
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    rowColorChange = true;
                }



            }

            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.ClearSelection();
        }

        private void loadCustomerList()
        {
            DataTable dt = dalCust.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmbCust.DataSource = distinctTable;
            cmbCust.DisplayMember = "cust_name";
            cmbCust.SelectedIndex = -1;
        }

        #endregion

        #region Data Checking/Get Data

        private int getMonthValue(string keyword)
        {

            int month = 0;
            if (string.IsNullOrEmpty(keyword))
            {
                month = Convert.ToInt32(DateTime.Now.Month.ToString());
            }
            else
            {
                month = (int)(Month)Enum.Parse(typeof(Month), keyword);
            }


            return month;
        }

        private int getNextMonth(string keyword)
        {

            int month = getMonthValue(keyword);
            int nextMonth = 0;

            if (month == 12)
            {
                nextMonth = 1;
            }
            else
            {
                nextMonth = month + 1;
            }
            return nextMonth;
        }

        private int getNextNextMonth(string keyword)
        {

            int nextMonth = getNextMonth(keyword);
            int nextNextMonth = 0;

            if (nextMonth == 12)
            {
                nextNextMonth = 1;
            }
            else
            {
                nextNextMonth = nextMonth + 1;
            }
            return nextNextMonth;
        }

        private string getShortMonth(string month, int forecast)
        {
            string shortMonth = "";
            int monthValue = 0;
            switch (forecast)
            {
                case 1:
                    monthValue = getMonthValue(month);
                    break;
                case 2:
                    monthValue = getNextMonth(month);
                    break;
                case 3:
                    monthValue = getNextNextMonth(month);
                    break;
                default:
                    monthValue = 0;
                    break;
            }

            switch (monthValue)
            {
                case 1:
                    shortMonth = "Jan";
                    break;
                case 2:
                    shortMonth = "Feb";
                    break;
                case 3:
                    shortMonth = "Mar";
                    break;
                case 4:
                    shortMonth = "Apr";
                    break;
                case 5:
                    shortMonth = "May";
                    break;
                case 6:
                    shortMonth = "Jun";
                    break;
                case 7:
                    shortMonth = "Jul";
                    break;
                case 8:
                    shortMonth = "Aug";
                    break;
                case 9:
                    shortMonth = "Sep";
                    break;
                case 10:
                    shortMonth = "Oct";
                    break;
                case 11:
                    shortMonth = "Nov";
                    break;
                case 12:
                    shortMonth = "Dec";
                    break;
            }


            return shortMonth;
        }

        private string getCustID(string custName)
        {
            string custID = "";

            DataTable dtCust = dalCust.nameSearch(custName);

            foreach (DataRow Cust in dtCust.Rows)
            {
                custID = Cust["cust_id"].ToString();
            }
            return custID;
        }

        private bool ifGotChild(string itemCode)
        {
            bool result = false;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        #endregion

        #region function

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
          
            bool result = dalMatUsed.Delete();

            if (!result)
            {
                MessageBox.Show("Failed to reset forecast data");
            }
            else
            {
                loadMaterialUsedList();
                
            }

            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void frmMaterialUsedReport_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            dgvMaterialUsedRecord.ClearSelection();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

        private void cmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbCust.Text.Equals("All"))
            {

            }
            else
            {

            }
        }
    }
}
