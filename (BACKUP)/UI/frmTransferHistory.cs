using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmTransferHistory : Form
    {
        Tool tool = new Tool();
        Text text = new Text();
        trfCatDAL daltrfCat = new trfCatDAL();
        facDAL dalFac = new facDAL();
        custDAL dalCust = new custDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        itemCatDAL dalItemCat = new itemCatDAL();
        joinDAL dalJoin = new joinDAL();
        itemDAL dalItem = new itemDAL();
        planningDAL dalPlan = new planningDAL();
        PlanningBLL uPlan = new PlanningBLL();

        DataTable dt_TrfHist;
        DataTable dt_TrfData;
        DataTable dt_Item;

        readonly string headerID = "ID";
        readonly string headerAddedDate = "ADDED DATE";
        readonly string headerTrfDate = "TRANSFER DATE";
        readonly string headerCode = "CODE";
        readonly string headerName = "NAME";
        readonly string headerFrom = "FROM";
        readonly string headerTo = "TO";
        readonly string headerQty = "QTY";
        readonly string headerUnit = "UNIT";
        readonly string headerNote = "NOTE";
        readonly string headerResult = "RESULT";
        readonly string headerBalance = "BALANCE";
        readonly string headerPlanID = "PLAN ID";
        readonly string headerPlan = "PLAN";

        readonly string headerBag = "BAG";

        public frmTransferHistory()
        {
            InitializeComponent();
            dt_Item = dalItem.Select();

            tool.DoubleBuffered(dgvTrf, true);
            loadItemCategoryData(cmbTrfItemCat);

            loadLocationCategoryData();
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        public frmTransferHistory(string itemCode)
        {
            InitializeComponent();
            dt_Item = dalItem.Select();

            tool.DoubleBuffered(dgvTrf, true);
            loadItemCategoryData(cmbTrfItemCat);

            loadLocationCategoryData();

            txtSearch.Text = itemCode;


            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            autoLoad = true;

           
        }

        bool autoLoad = false;

        public DataTable NewTrfHistTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerID, typeof(int));
            dt.Columns.Add(headerAddedDate, typeof(DateTime));
            dt.Columns.Add(headerTrfDate, typeof(DateTime));

            dt.Columns.Add(headerCode, typeof(string));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerFrom, typeof(string));
            dt.Columns.Add(headerTo, typeof(string));
            dt.Columns.Add(headerQty, typeof(float));
            dt.Columns.Add(headerUnit, typeof(string));
            dt.Columns.Add(headerNote, typeof(string));
            dt.Columns.Add(headerResult, typeof(string));
            dt.Columns.Add(headerBalance, typeof(float));
            dt.Columns.Add(headerPlanID, typeof(string));
            dt.Columns.Add(headerPlan, typeof(string));

            //dt.Columns.Add(headerBag, typeof(int));

            return dt;
        }

        private void dgvTrfUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerAddedDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTrfDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[headerFrom].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTo].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerUnit].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerNote].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerResult].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerPlan].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerPlanID].Visible = false;

            dgv.Columns[headerQty].DefaultCellStyle.Format = "0.##";
        }

        private void loadItemCategoryData(ComboBox cmb)
        {
            DataTable dtItemCat = dalItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "item_cat_name";
            cmb.SelectedIndex = 0;
        }

        private void loadLocationCategoryData()
        {
            DataTable dtlocationCat = daltrfCat.Select();

            DataTable fromCatTable = dtlocationCat.DefaultView.ToTable(true, "trf_cat_name");
            //fromCatTable.DefaultView.Sort = "trf_cat_name ASC";
            cmbTrfFromCategory.DataSource = fromCatTable;
            cmbTrfFromCategory.DisplayMember = "trf_cat_name";
            cmbTrfFromCategory.SelectedIndex = -1;


            DataTable toCatTable = dtlocationCat.DefaultView.ToTable(true, "trf_cat_name");
            //toCatTable.DefaultView.Sort = "trf_cat_name ASC";
            cmbTrfToCategory.DataSource = toCatTable;
            cmbTrfToCategory.DisplayMember = "trf_cat_name";
            cmbTrfToCategory.SelectedIndex = -1;

            if (cmbTrfFromCategory.Text.Equals("Assembly"))
            {
                cmbTrfToCategory.SelectedIndex = 0;
                cmbTrfTo.SelectedIndex = 4;
            }
        }

        private void frmTransferHistory_Load(object sender, EventArgs e)
        {
           
        }

        private void loadLocationData(DataTable dt, ComboBox cmb, string columnName)
        {
            DataTable lacationTable = dt.DefaultView.ToTable(true, columnName);

            //lacationTable.DefaultView.Sort = columnName+" ASC";
            cmb.DataSource = lacationTable;
            cmb.DisplayMember = columnName;
        }

        private void cmbTrfFromCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvTrf.DataSource = null;
            lblTotalQty.Text = "";
            if (cmbTrfFromCategory.Text.Equals("Factory"))
            {
                DataTable dt = dalFac.SelectDESC();
                loadLocationData(dt, cmbTrfFrom, "fac_name");

            }
            else if (cmbTrfFromCategory.Text.Equals("Customer"))
            {
                DataTable dt = dalCust.FullSelect();
                loadLocationData(dt, cmbTrfFrom, "cust_name");
                //cmbTrfFrom.Text = tool.getCustomerName(cmbTrfItemCode.Text);
            }
            else if (cmbTrfFromCategory.Text.Equals("Assembly"))
            {
                cmbTrfFrom.DataSource = null;
                cmbTrfToCategory.SelectedIndex = 0;
                cmbTrfTo.SelectedIndex = 4;
            }
            else
            {
                cmbTrfFrom.DataSource = null;
            }
        }

        private void cmbTrfToCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvTrf.DataSource = null;
            lblTotalQty.Text = "";
            if (cmbTrfToCategory.Text.Equals("Factory"))
            {
                DataTable dt = dalFac.SelectDESC();
                loadLocationData(dt, cmbTrfTo, "fac_name");

            }
            else if (cmbTrfToCategory.Text.Equals("Customer"))
            {
                DataTable dt = dalCust.FullSelect();
                loadLocationData(dt, cmbTrfTo, "cust_name");

               // cmbTrfTo.Text = tool.getCustomerName(cmbTrfItemCode.Text);

            }
            else
            {
                cmbTrfTo.DataSource = null;
            }
        }

        private string getBetween(string strSource, string strStart, string strEnd1, string strEnd2)
        {

          

            if (strSource.Contains(strStart) && (strSource.Contains(strEnd1) || strSource.Contains(strEnd2)))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd1, Start);

                if(End <= -1)
                {
                    End = strSource.IndexOf(strEnd2, Start);

                    if (End <= -1)
                    {
                        return "Failed to get Plan ID";
                    }
                }

               

                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        private string GetPlanID(string note)
        {
            note = note.ToUpper();

            string planID = "";

            if(note.Contains("PLAN"))
                planID = getBetween(note, "PLAN ", "(", "]");


            return planID;
            
        }

        private int GetTotalBag(string note)
        {
            int totalBag = 0;

            bool dotFound = false;
            bool readyToGet = false;

            string text = "";
            for(int i = 0; i < note.Length; i++)
            {
                if(note[i].ToString() == ":")
                {
                    dotFound = true;
                }

                if(dotFound && note[i].ToString() == " ")
                {
                    if(!readyToGet)
                    {
                        readyToGet = true;
                    }
                    else
                    {
                        readyToGet = false;
                        break;
                    }
                }

                if(readyToGet && int.TryParse(note[i].ToString(), out int x))
                {
                    text += note[i].ToString();
                }

            }

            totalBag = int.TryParse(text, out totalBag) ? totalBag : 0;

            return totalBag;
        }

        private string GetPlanItem(string PlanID)
        {
            string planItem = "";

            DataTable dt_plan = dalPlan.idSearch(PlanID);

            foreach (DataRow row in dt_plan.Rows)
            {
                string itemCode = row[dalPlan.partCode].ToString();
                string itemName = tool.getItemName(itemCode);

                planItem = itemCode + "_" + itemName;
                break;

            }

            return planItem;

        }

        private void filterData()
        {
            dgvTrf.DataSource = null;

            string keyword = txtSearch.Text;

            if(keyword == null)
            {
                dt_TrfHist = dalTrfHist.SelectWithBalance();
            }
            else
            {
                dt_TrfHist = dalTrfHist.SearchIncludeID(keyword);
            }
            
            dt_TrfData = NewTrfHistTable();
            DataRow row_TrfData;

            DateTime startDate = Convert.ToDateTime(dtpStartDate.Value).Date;
            DateTime endDate = Convert.ToDateTime(dtpEndDate.Value).Date;

            bool periodWanted = false, fromWanted = false, toWanted = false, catWanted = false;

            foreach (DataRow row in dt_TrfHist.Rows)
            {
                DateTime date;
                string result = row[dalTrfHist.TrfResult].ToString();

                if(result == "Passed")
                {
                    if (cbTransferType.Checked)
                    {
                        date = Convert.ToDateTime(row[dalTrfHist.TrfDate]).Date;
                    }
                    else
                    {
                        date = Convert.ToDateTime(row[dalTrfHist.TrfAddedDate]).Date;
                    }

                    //Filter Date
                    if (date <= endDate && date >= startDate)
                    {
                        periodWanted = true;
                    }
                    else
                    {
                        periodWanted = false;
                    }

                    //Filter Location From
                    if (cmbTrfFromCategory.SelectedIndex == -1)
                    {
                        fromWanted = true;
                    }
                    else
                    {
                        string from = row[dalTrfHist.TrfFrom].ToString();

                        if (from.Equals(cmbTrfFromCategory.Text) || from.Equals(cmbTrfFrom.Text))
                        {
                            fromWanted = true;
                        }
                        else
                        {
                            fromWanted = false;
                        }
                    }

                    //Filter Location To
                    if (cmbTrfToCategory.SelectedIndex == -1)
                    {
                        toWanted = true;
                    }
                    else
                    {
                        string to = row[dalTrfHist.TrfTo].ToString();

                        if (to.Equals(cmbTrfToCategory.Text) || to.Equals(cmbTrfTo.Text))
                        {
                            toWanted = true;
                        }
                        else
                        {
                            toWanted = false;
                        }
                    }

                    //Filter Category
                    if (cmbTrfItemCat.SelectedIndex == -1 || cmbTrfItemCat.Text.Equals("All"))
                    {
                        catWanted = true;
                    }
                    else
                    {
                        string cat = row[dalTrfHist.TrfItemCat].ToString();

                        if (cat.Equals(cmbTrfItemCat.Text))
                        {
                            catWanted = true;
                        }
                        else
                        {
                            catWanted = false;
                        }
                    }

                    //Save data wanted
                    if (periodWanted && fromWanted && toWanted && catWanted)
                    {
                        row_TrfData = dt_TrfData.NewRow();

                        row_TrfData[headerID] = row[dalTrfHist.TrfID];
                        row_TrfData[headerAddedDate] = row[dalTrfHist.TrfAddedDate];
                        row_TrfData[headerTrfDate] = Convert.ToDateTime(row[dalTrfHist.TrfDate]).Date;
                        row_TrfData[headerCode] = row[dalTrfHist.TrfItemCode];
                        row_TrfData[headerName] = row[dalTrfHist.TrfItemName];
                        row_TrfData[headerFrom] = row[dalTrfHist.TrfFrom];
                        row_TrfData[headerTo] = row[dalTrfHist.TrfTo];
                        row_TrfData[headerQty] = row[dalTrfHist.TrfQty];
                        row_TrfData[headerUnit] = row[dalTrfHist.TrfUnit];
                        row_TrfData[headerNote] = row[dalTrfHist.TrfNote];
                        row_TrfData[headerResult] = row[dalTrfHist.TrfResult];
                        row_TrfData[headerBalance] = row[dalTrfHist.Balance];

                        string PlanID = GetPlanID(row[dalTrfHist.TrfNote].ToString());
                        row_TrfData[headerPlanID] = PlanID;

                        if(!string.IsNullOrEmpty(PlanID))
                        row_TrfData[headerPlan] = GetPlanItem(PlanID);

                        dt_TrfData.Rows.Add(row_TrfData);


                        periodWanted = false;
                        fromWanted = false;
                        toWanted = false;
                        catWanted = false;
                    }
                }

                
            }

            if (dt_TrfData.Rows.Count > 0)
            {
                //dgvNewStock.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

                if (cbTransferType.Checked)
                {
                    dt_TrfData.DefaultView.Sort = headerTrfDate + " DESC";
                }
                else
                {
                    dt_TrfData.DefaultView.Sort = headerAddedDate + " DESC";
                }
               
                dgvTrf.DataSource = dt_TrfData;
                dgvTrfUIEdit(dgvTrf);

                dgvTrf.ClearSelection();
            }
            else
            {
                MessageBox.Show("No data found!");
            }
        }

        private void LoadHistory()
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                filterData();
                calculateTotal();
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

            LoadHistory();

        }

        private void cbTransferType_CheckedChanged(object sender, EventArgs e)
        {
            dgvTrf.DataSource = null;
            if (cbTransferType.Checked)
            {
                cbAddedType.Checked = false;
            }
            else
            {
                cbAddedType.Checked = true;
            }
        }

        private void cbAddedType_CheckedChanged(object sender, EventArgs e)
        {
            dgvTrf.DataSource = null;
            if (cbAddedType.Checked)
            {
                cbTransferType.Checked = false;
            }
            else
            {
                cbTransferType.Checked = true;
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            dgvTrf.DataSource = null;
            lblTotalQty.Text = "";
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            dgvTrf.DataSource = null;
            lblTotalQty.Text = "";
        }

        private void cmbTrfFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvTrf.DataSource = null;
            lblTotalQty.Text = "";
        }

        private void cmbTrfItemCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvTrf.DataSource = null;
            lblTotalQty.Text = "";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvTrf.DataSource = null;
            lblTotalQty.Text = "";
        }

        private void cmbTrfTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvTrf.DataSource = null;
            lblTotalQty.Text = "";
        }

        private bool ifGotChild(string itemCode)
        {
            bool result = false;
            DataTable dtJoin = dalJoin.loadChildList(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        private void dgvTrf_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvTrf;
            dgv.SuspendLayout();
            int n = e.RowIndex;
            string itemCode = null;

            if (dgv.Columns[e.ColumnIndex].Name == headerCode)
            {
                itemCode = dgv.Rows[n].Cells[headerCode].Value.ToString();

                if (ifGotChild(itemCode))
                {
                    bool assembly = dalItem.checkIfAssembly(itemCode, dt_Item);
                    bool production = dalItem.checkIfProduction(itemCode, dt_Item);

                    if (assembly && production)
                    {
                        //dgv.Rows[n].Cells[dalTrfHist.TrfItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells[headerName].Style = new DataGridViewCellStyle { ForeColor = Color.Purple, Font = new Font(dgv.Font, FontStyle.Underline) };

                    }
                    else if (!assembly && production)
                    {
                        //dgv.Rows[n].Cells[dalTrfHist.TrfItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells[headerName].Style = new DataGridViewCellStyle { ForeColor = Color.Green, Font = new Font(dgv.Font, FontStyle.Underline) };

                    }
                    else
                    {
                        //dgv.Rows[n].Cells[dalTrfHist.TrfItemCode].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells[headerName].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new Font(dgv.Font, FontStyle.Underline) };
                    }

                    if (itemCode.Substring(0, 3) == text.Inspection_Pass)
                    {
                        dgv.Rows[n].Cells[headerName].Style = new DataGridViewCellStyle { ForeColor = Color.Peru, Font = new Font(dgv.Font, FontStyle.Underline) };
                    }
                }
            }

            //else if (dgv.Columns[e.ColumnIndex].Name == headerResult)
            //{
            //    if (dgv.Rows[n].Cells[headerResult].Value != null)
            //    {
            //        if (dgv.Rows[n].Cells[headerResult].Value.ToString().Equals("Undo"))
            //        {
            //            dgv.Rows[n].DefaultCellStyle.ForeColor = Color.Red;
            //            dgv.Rows[n].DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
            //        }
            //        else if (dgv.Rows[n].Cells[headerResult].Value.ToString().Equals("Failed"))
            //        {
            //            dgv.Rows[n].DefaultCellStyle.ForeColor = Color.Red;
            //            //dgv.Rows[n].DefaultCellStyle.Font = new Font(Font, FontStyle.Strikeout);
            //        }
            //        else
            //        {
            //            dgv.Rows[n].DefaultCellStyle.ForeColor = Color.Black;
            //            dgv.Rows[n].DefaultCellStyle.Font = new Font(Font, FontStyle.Regular);
            //        }
            //    }
            //}
        

            dgv.ResumeLayout();
        }

        private void calculateTotal()
        {
            if (cbCalculateTotal.Checked)
            {
                if (dt_TrfData.Rows.Count > 0)
                {
                    float total = 0;
                    foreach (DataRow row in dt_TrfData.Rows)
                    {
                        total += Convert.ToSingle(row[headerQty]);
                    }
                    lblTotalQty.Text = total.ToString();
                }
            }
            else
            {
                lblTotalQty.Text = "";
            }

        }
        private void cbCalculateTotal_CheckedChanged(object sender, EventArgs e)
        {
            calculateTotal();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            cmbTrfFromCategory.SelectedIndex = -1;
            cmbTrfFrom.SelectedIndex = -1;
            cmbTrfToCategory.SelectedIndex = -1;
            cmbTrfTo.SelectedIndex = -1;
            cmbTrfItemCat.SelectedIndex = 0;

            cbTransferType.Checked = true;

            txtSearch.Clear();

            dtpStartDate.Text = "28/2/2019";
            dtpEndDate.Value = DateTime.Today;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            cmbTrfFromCategory.SelectedIndex = -1;
            cmbTrfFrom.SelectedIndex = -1;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            cmbTrfToCategory.SelectedIndex = -1;
            cmbTrfTo.SelectedIndex = -1;
        }

        private void frmTransferHistory_Shown(object sender, EventArgs e)
        {
            if(autoLoad)
            {
                LoadHistory();
            }
        }
    }
}
