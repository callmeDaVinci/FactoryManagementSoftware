using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmInOutReport : Form
    {

        #region Variable

        readonly string codeColumnName = "item_code";
        readonly string nameColumnName = "item_name";
        readonly string dateColumnName = "DATE";
        readonly string inQtyColumnName = "IN QTY";
        readonly string outQtyColumnName = "OUT QTY";

        readonly string CMBAllHeader = "All";
        readonly string CMBPartHeader = "Parts";
        readonly string CMBMaterialHeader = "Materials";
        readonly string cmbByItem = "Item";
        readonly string cmbByDate = "Date";

        readonly string lblItemCat = "CATEGORY";
        readonly string lblItemName = "NAME";
        readonly string lblItemCode = "CODE";

        readonly string cmbItemTypeDay = "Day";
        readonly string cmbItemTypeMonth = "Month";
        readonly string cmbItemTypeYear = "Year";

        readonly string cmbItemSubTypeInOut = "In/Out";
        readonly string cmbItemSubTypeIn = "In Only";
        readonly string cmbItemSubTypeOut = "Out Only";

        readonly string lblDateStart = "START";
        readonly string lblDateEnd = "END";

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;

        #endregion

        #region Class Object
        facDAL dalFac = new facDAL();
        facStockDAL dalStock = new facStockDAL();
        itemDAL dalItem = new itemDAL();
        joinDAL dalJoin = new joinDAL();
        custDAL dalCust = new custDAL();
        itemCatDAL dalItemCat = new itemCatDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        CheckChildBLL uCheckChild = new CheckChildBLL();
        userDAL dalUser = new userDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        Tool tool = new Tool();
        #endregion

        #region UI setting

        public frmInOutReport()
        {      
            InitializeComponent();
            addDataSourceTocmbBy();
            
        }

        private void showItemInput()
        {
            dtpStart.Hide();
            dtpEnd.Hide();

            lblStartOrCat.Text = lblItemCat;
            lblEndOrName.Text = lblItemName;
            lblCode.Text = lblItemCode;

            lblStartOrCat.Show();
            lblEndOrName.Show();
            lblCode.Show();

            cmbCat.Show();
            cmbName.Show();
            cmbCode.Show();
        }

        private void showDateInput()
        {           
            cmbCat.Hide();
            cmbName.Hide();
            cmbCode.Hide();
            lblCode.Hide();

            lblStartOrCat.Text = lblDateStart;
            lblEndOrName.Text = lblDateEnd;

            lblStartOrCat.Show();
            lblEndOrName.Show();

            dtpStart.Show();
            dtpEnd.Show();
        }

        private void hideInput()
        {
            cmbCat.Hide();
            cmbName.Hide();
            cmbCode.Hide();
            dtpStart.Hide();
            dtpEnd.Hide();

            lblStartOrCat.Hide();
            lblEndOrName.Hide();
            lblCode.Hide();
        }

        private void createByDateDatagridview(DataGridView dgv)
        {
            dgv.Columns.Clear();
            //add ID column
            tool.AddTextBoxColumns(dgv, "ID", dalTrfHist.TrfID, DisplayedCells);

            //add category column
            tool.AddTextBoxColumns(dgv, "CATEGORY", dalTrfHist.TrfItemCat, DisplayedCells);

            //add name column
            tool.AddTextBoxColumns(dgv, "CODE", dalTrfHist.TrfItemCode, Fill);

            //add code column
            tool.AddTextBoxColumns(dgv, "NAME", dalTrfHist.TrfItemName, Fill);

            //add total qty column
            tool.AddTextBoxColumns(dgv, "TRANSFER", dalTrfHist.TrfDate, DisplayedCells);

            tool.AddTextBoxColumns(dgv, "FROM", dalTrfHist.TrfFrom, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "TO", dalTrfHist.TrfTo, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "QTY", dalTrfHist.TrfQty, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "UNIT", dalTrfHist.TrfUnit, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "NOTE", dalTrfHist.TrfNote, DisplayedCells);
            
            tool.AddTextBoxColumns(dgv, "ADDED DATE", dalTrfHist.TrfAddedDate, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "ADDED BY", dalTrfHist.TrfAddedBy, DisplayedCells);

            tool.AddTextBoxColumns(dgv, "UPDATED DATE", dalTrfHist.TrfUpdatedDate, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "UPDATED BY", dalTrfHist.TrfUpdatedBy, DisplayedCells);

            dgv.Columns[dalTrfHist.TrfQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            tool.listPaintGreyHeader(dgv);
        }

        private void createByItemDatagridview(DataGridView dgv)
        {
            dgv.Columns.Clear();
           
            tool.AddTextBoxColumns(dgv, "CODE", codeColumnName, Fill);

            //add code column
            tool.AddTextBoxColumns(dgv, "NAME", nameColumnName, Fill);

            //add total qty column
            tool.AddTextBoxColumns(dgv, dateColumnName, dateColumnName, DisplayedCells);

            tool.AddTextBoxColumns(dgv, inQtyColumnName, inQtyColumnName, DisplayedCells);
            tool.AddTextBoxColumns(dgv, outQtyColumnName, outQtyColumnName, DisplayedCells);

            dgv.Columns[inQtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[outQtyColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            tool.listPaintGreyHeader(dgv);
        }

        #endregion

        #region Load Data

        private void addDataSourceTocmbBy()
        {
            cmbBy.Items.Clear();
            cmbBy.Items.Add(cmbByDate);
            cmbBy.Items.Add(cmbByItem);
            cmbBy.SelectedIndex = -1;
            hideInput();
        }

        private void addDataSourceToItemType()
        {
            cmbType.Items.Clear();
            cmbType.Items.Add(cmbItemTypeDay);
            cmbType.Items.Add(cmbItemTypeMonth);
            cmbType.Items.Add(cmbItemTypeYear);
            cmbType.SelectedIndex = -1;

            cmbSubType.DataSource = null;
            cmbSubType.Items.Add(cmbItemSubTypeInOut);
            cmbSubType.Items.Add(cmbItemSubTypeIn);
            cmbSubType.Items.Add(cmbItemSubTypeOut);
            cmbSubType.SelectedIndex = -1;
        }

        private void addDataSourceToDateType()
        {
            cmbType.Items.Clear();
            cmbType.Items.Add(CMBAllHeader);
            cmbType.Items.Add(CMBPartHeader);
            cmbType.Items.Add(CMBMaterialHeader);
            cmbType.SelectedIndex = 0;

            cmbSubType.Items.Clear();
        }

        private void loadTrfHistData(DataGridView dgv)
        {
            createByDateDatagridview(dgv);
            DataTable dt = null;
            string start = dtpStart.Value.ToString("yyyy/MM/dd");
            string end = dtpEnd.Value.ToString("yyyy/MM/dd");
            string Type = cmbType.Text;
            string subType = cmbSubType.Text;

            if (Type.Equals(CMBAllHeader))
            {
                //check type: all/part/material
                //if all: show all trf hist during selected period
                dt = dalTrfHist.rangeTrfSearch(start, end);
            }
            else if(Type.Equals(CMBPartHeader))
            {
                //if part: check sub type: all/customer
                //if all: show all part trf hist during selected period
                if(subType.Equals(CMBAllHeader))
                {
                    dt = dalTrfHist.rangePartTrfSearch(start, end);
                }
                else if(!string.IsNullOrEmpty(subType))
                {
                    //if customer: show selected customer's part only
                    dt = dalTrfHist.rangePartTrfSearch(start, end, tool.getCustID(subType));
                }
            }
            else if(Type.Equals(CMBMaterialHeader))
            {
                //show material transfer history during selected period
                dt = dalTrfHist.rangeMaterialTrfSearch(start, end, subType);
            }

            if(dt != null)
            insertByDateDataToDGV(dgv, dt);
        }

        private void insertByDateDataToDGV(DataGridView dgv, DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                dgv.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    if (item["trf_result"].ToString().Equals("Passed"))
                    {
                        int n = dgv.Rows.Add();

                        if(cmbType.Text.Equals(CMBPartHeader) && cmbSubType.Text != CMBAllHeader)
                        {
                            dgv.Rows[n].Cells[dalTrfHist.TrfItemCat].Value = cmbSubType.Text;
                        }
                        else
                        {
                            dgv.Rows[n].Cells[dalTrfHist.TrfItemCat].Value = item[dalTrfHist.TrfItemCat].ToString();
                        }
                        dgv.Rows[n].Cells[dalTrfHist.TrfID].Value = item[dalTrfHist.TrfID].ToString();
                        
                        dgv.Rows[n].Cells[dalTrfHist.TrfItemCode].Value = item[dalTrfHist.TrfItemCode].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfItemName].Value = item[dalTrfHist.TrfItemName].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfDate].Value = Convert.ToDateTime(item[dalTrfHist.TrfDate]).ToString("dd/MM/yyyy");
                        dgv.Rows[n].Cells[dalTrfHist.TrfFrom].Value = item[dalTrfHist.TrfFrom].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfTo].Value = item[dalTrfHist.TrfTo].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfQty].Value = item[dalTrfHist.TrfQty].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfUnit].Value = item[dalTrfHist.TrfUnit].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfNote].Value = item[dalTrfHist.TrfNote].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfAddedDate].Value = item[dalTrfHist.TrfAddedDate].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfAddedBy].Value = item[dalTrfHist.TrfAddedBy].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfUpdatedDate].Value = item[dalTrfHist.TrfUpdatedDate].ToString();
                        dgv.Rows[n].Cells[dalTrfHist.TrfUpdatedBy].Value = item[dalTrfHist.TrfUpdatedBy].ToString();
                    } 
                }
            }
            else
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[dalTrfHist.TrfItemName].Value = "No data exist";
            }
        }

        private void frmInOutReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.InOutReportFormOpen = false;
        }

        #endregion

        #region Text/Index Changed

        private void cmbBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            
            if (cmbBy.Text.Equals(cmbByItem))
            {
                showItemInput();
                tool.loadItemCategoryDataToComboBox(cmbCat);
                addDataSourceToItemType();
                cmbCat.SelectedIndex = -1;
                //createByItemDatagridview();
            }
            else if(cmbBy.Text.Equals(cmbByDate))
            {
                cmbCat.DataSource = null;
                cmbName.DataSource = null;
                cmbCode.DataSource = null;
                showDateInput();
                addDataSourceToDateType();
                loadTrfHistData(dgvInOutReport);
                //createByDateDatagridview();
            }
            else
            {
                hideInput();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            tool.loadItemNameDataToComboBox(cmbName, cmbCat.Text);
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tool.loadItemCodeDataToComboBox(cmbCode, cmbName.Text);
        }

        private void cmbCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBy.Text.Equals(cmbByDate))
            {
                if(cmbType.Text.Equals(CMBAllHeader))
                {
                    cmbSubType.DataSource = null;
                }
                else if (cmbType.Text.Equals(CMBPartHeader))
                {
                    tool.loadCustomerAndAllToComboBox(cmbSubType);
                }
                else if (cmbType.Text.Equals(CMBMaterialHeader))
                {
                    tool.loadMaterialAndAllToComboBox(cmbSubType);
                }
                loadTrfHistData(dgvInOutReport);
            }
        }


        #endregion

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            loadTrfHistData(dgvInOutReport);
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            loadTrfHistData(dgvInOutReport);
        }

        private void cmbSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadTrfHistData(dgvInOutReport);
        }
    }
}
