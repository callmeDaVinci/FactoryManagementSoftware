using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSPPPOList : Form
    {
        public frmSPPPOList()
        {
            InitializeComponent();
            btnFilter.Text = text_HideFilter;
            dt_POList = dalSPP.POSelect();
            LoadCustomerList();
        }

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        facDAL dalFac = new facDAL();
        pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        SPPDataDAL dalSPP = new SPPDataDAL();

        Tool tool = new Tool();
        Text text = new Text();

        readonly string text_ShowFilter = "SHOW FILTER...";
        readonly string text_HideFilter = "HIDE FILTER";
        readonly string header_POCode = "PO CODE";
        readonly string header_PODate = "PO DATE";
        readonly string header_Customer = "CUSTOMER";
        readonly string header_Progress = "PROGRESS";

        readonly string header_Index = "#";
        readonly string header_Size = "SIZE";
        readonly string header_Unit = "UNIT";
        readonly string header_Type = "TYPE";
        readonly string header_ItemCode = "ITEM CODE";
        readonly string header_OrderQty = "ORDER QTY";
        readonly string header_DeliveredQty = "DELIVERED QTY";
        readonly string header_Note = "NOTE";

        private DataTable dt_POList;

        private DataTable NewPOTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_POCode, typeof(int));
            dt.Columns.Add(header_PODate, typeof(DateTime));
            dt.Columns.Add(header_Customer, typeof(string));
            dt.Columns.Add(header_Progress, typeof(string));

            return dt;
        }

        private DataTable NewPOItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
         
            dt.Columns.Add(header_Size, typeof(string));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_DeliveredQty, typeof(int));
            dt.Columns.Add(header_OrderQty, typeof(int));
            dt.Columns.Add(header_Note, typeof(string));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if(dgv == dgvPOList)
            {
                dgv.Columns[header_POCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_PODate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Customer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Progress].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgv == dgvPOItemList)
            {
                dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Size].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_ItemCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgv.Columns[header_DeliveredQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_OrderQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[header_Note].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

        }

        private void ShowOrHideFilter()
        {
            string filterText = btnFilter.Text;

            if(filterText == text_ShowFilter)
            {
                btnFilter.Text = text_HideFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 125f);
            }
            else
            {
                btnFilter.Text = text_ShowFilter;

                tlpPOList.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowOrHideFilter();
        }

        private void frmSPPPOList_Load(object sender, EventArgs e)
        {
            ShowOrHideFilter();
            LoadPOList();
        }

        private void LoadCustomerList()
        {
            DataTable dt = dalSPP.CustomerWithoutRemovedDataSelect();

            DataTable locationTable = dt.DefaultView.ToTable(true, dalSPP.FullName);

            DataRow dr;
            dr = locationTable.NewRow();
            dr[dalSPP.FullName] = "ALL";

            locationTable.Rows.InsertAt(dr, 0);

            cmbCustomer.DataSource = locationTable;
            cmbCustomer.DisplayMember = dalSPP.FullName;

        }

        private void LoadPOList()
        {
            btnEdit.Visible = false;
            DataTable dt = NewPOTable();
            DataRow dt_row;

            int prePOCode = -1;
            foreach(DataRow row in dt_POList.Rows)
            {
                int poCode = int.TryParse(row[dalSPP.POCode].ToString(), out poCode) ? poCode : -1;

                if(prePOCode == -1 || prePOCode != poCode)
                {
                    prePOCode = poCode;
                    int orderQty = int.TryParse(row[dalSPP.POQty].ToString(), out orderQty) ? orderQty : 0;
                    int deliveredQty = int.TryParse(row[dalSPP.DeliveredQty].ToString(), out deliveredQty) ? deliveredQty : 0;

                    int progress = deliveredQty / orderQty * 100;

                    bool isRemoved = bool.TryParse(row[dalSPP.IsRemoved].ToString(), out isRemoved) ? isRemoved : false;

                    bool dataMatched = !isRemoved;

                    #region PO TYPE

                    if (progress < 100 && cbInProgressPO.Checked)
                    {
                        dataMatched = dataMatched && true;
                    }
                    else if(progress >= 100 && cbCompletedPO.Checked)
                    {
                        dataMatched = dataMatched && true;
                    }
                    else
                    {
                        dataMatched = false;
                    }

                    #endregion

                    #region Customer Filter

                    string customer = cmbCustomer.Text;

                    if(string.IsNullOrEmpty(customer) || customer == "ALL")
                    {
                        dataMatched = dataMatched && true;
                    }
                    else if(row[dalSPP.FullName].ToString() == customer)
                    {
                        dataMatched = dataMatched && true;
                    }
                    else
                    {
                        dataMatched = false;
                    }

                    #endregion

                    if (dataMatched)
                    {
                        dt_row = dt.NewRow();

                        dt_row[header_POCode] = poCode;
                        dt_row[header_PODate] = Convert.ToDateTime(row[dalSPP.PODate]).Date;
                        dt_row[header_Customer] = row[dalSPP.ShortName];
                        dt_row[header_Progress] = progress + "%";
                        dt.Rows.Add(dt_row);
                    }
                   
                }
               
            }

            dgvPOList.DataSource = dt;
            DgvUIEdit(dgvPOList);
            dgvPOList.ClearSelection();

        }

        private void btnAddNewPO_Click(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
            frmSPPNewPO frm = new frmSPPNewPO
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            

            frm.ShowDialog();
            dt_POList = dalSPP.POSelect();
            LoadPOList();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            LoadPOList();
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvPOItemList.DataSource = null;
        }

        private void cbInProgressPO_CheckedChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvPOItemList.DataSource = null;
        }

        private void cbCompletedPO_CheckedChanged(object sender, EventArgs e)
        {
            dgvPOList.DataSource = null;
            dgvPOItemList.DataSource = null;
        }

        private void dgvPOList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.Visible = true;
            DataTable dt = (DataTable)dgvPOList.DataSource;
            int rowIndex = e.RowIndex;
            DataGridView dgv = dgvPOList;

            if (rowIndex >= 0)
            {
                string code = dt.Rows[rowIndex][header_POCode].ToString();
                ShowPOItem(code);
            }
            else
            {
                dgvPOItemList.DataSource = null;
                btnEdit.Visible = false;
            }
        }

        private void ShowPOItem(string poCode)
        {
            DataTable dt = dalSPP.POSelectWithSizeAndType();

            DataTable dt_POItemList = NewPOItemTable();
            DataRow dt_Row;
            int index = 1;
            foreach (DataRow row in dt.Rows)
            {
                if (poCode == row[dalSPP.POCode].ToString())
                {

                    int deliveredQty = row[dalSPP.DeliveredQty] == DBNull.Value ? 0 : Convert.ToInt32(row[dalSPP.DeliveredQty].ToString());

                    dt_Row = dt_POItemList.NewRow();
                    dt_Row[header_Index] = index;
                    dt_Row[header_Size] = row[dalSPP.SizeNumerator];
                    dt_Row[header_Unit] = row[dalSPP.SizeUnit].ToString().ToUpper();
                    dt_Row[header_Type] = row[dalSPP.TypeName];
                    dt_Row[header_ItemCode] = row[dalSPP.ItemCode];
                    dt_Row[header_DeliveredQty] = deliveredQty;
                    dt_Row[header_OrderQty] = row[dalSPP.POQty];
                    dt_Row[header_Note] = row[dalSPP.PONote];

                    dt_POItemList.Rows.Add(dt_Row);
                    index++;
                }
            }

            dgvPOItemList.DataSource = dt_POItemList;
            DgvUIEdit(dgvPOItemList);
            dgvPOItemList.ClearSelection();
        }

        private void EditPO()
        {
            //get item info
            DataTable dt = (DataTable)dgvPOList.DataSource;
            int rowIndex = dgvPOList.CurrentRow.Index;

            if (rowIndex >= 0)
            {
                string code = dt.Rows[rowIndex][header_POCode].ToString();

                frmSPPNewPO frm = new frmSPPNewPO(code)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };



                frm.ShowDialog();

                if (frmSPPNewPO.poEdited || frmSPPNewPO.poRemoved)
                {
                    btnEdit.Visible = false;
                    dt_POList = dalSPP.POSelect();
                    LoadPOList();
                }

            }
            else
            {
                MessageBox.Show("Please select a row!");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditPO();
        }

        private void dgvPOList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditPO();
        }

        private void dgvPOList_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = dgvPOList;
            int rowIndex = -1;

            if (dgv.SelectedRows.Count <= 0)
            {
                rowIndex = -1;
            }
            else
            {
                rowIndex = dgv.CurrentRow.Index;
            }

            btnEdit.Visible = true;
            DataTable dt = (DataTable)dgvPOList.DataSource;

            if (rowIndex >= 0)
            {
                string code = dt.Rows[rowIndex][header_POCode].ToString();
                ShowPOItem(code);
            }
            else
            {
                dgvPOItemList.DataSource = null;
                btnEdit.Visible = false;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //get po list
            //sort po list by customer asc, po code asc
            //foreach po list
            //get po code
            //show item data
            //get customer
            //if customer different with previous customer, create new sheet
            //else, combine all order item data from same customer into one datatable, sort by po code desc, po date desc, size asc, type asc
            //set dgvItemList datasource to table, copyclipboard()
            //export dgv data to sheet

        }
    }
}
