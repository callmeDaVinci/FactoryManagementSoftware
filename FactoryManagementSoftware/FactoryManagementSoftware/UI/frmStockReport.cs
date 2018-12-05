using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmStockReport : Form
    {
        private string categoryColumnName = "item_cat";
        private string codeColumnName = "item_code";
        private string nameColumnName = "item_name";
        private string factoryColumnName = "";
        private string totakStockColumnName = "item_qty";
        private string unitColumnName = "stock_unit";

        facDAL dalFac = new facDAL();
        facStockDAL dalStock = new facStockDAL();
        itemDAL dalItem = new itemDAL();

        public frmStockReport()
        {
            InitializeComponent();
            createDatagridview();
        }

        private void datagridviewUI(DataGridView dgv)
        {
            dgv.Columns[categoryColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[codeColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[nameColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[totakStockColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[unitColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void createDatagridview()
        {
            //add category column
            AddColumns("Category", categoryColumnName);
           
            //add code column
            AddColumns("Code", codeColumnName);
           
            //add name column
            AddColumns("Name", nameColumnName);
            
            //add factory columns
            DataTable dt = dalFac.Select();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    factoryColumnName = stock["fac_name"].ToString();
                    AddColumns(factoryColumnName, factoryColumnName);
                    dgvStockReport.Columns[factoryColumnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
            }

            //add total qty column
            AddColumns("Total Stock", totakStockColumnName);

            //add total unit column
            AddColumns("Unit", unitColumnName);

            datagridviewUI(dgvStockReport);
            insertDataToDatagridview();
            listPaint(dgvStockReport);
        }

        private void AddColumns(string headText, string name)
        {
            var col = new DataGridViewTextBoxColumn();

            col.HeaderText = headText;
            col.Name = name;

            dgvStockReport.Columns.Add(col);
        }

        private string getFactoryName(string facID)
        {
            string factoryName = "";

            DataTable dtFac = dalFac.idSearch(facID);

            foreach (DataRow fac in dtFac.Rows)
            {
                factoryName = fac["fac_name"].ToString();
            }
            return factoryName;
        }

        private void loadStockList(string itemCode,int n)
        {
            DataTable dt = dalStock.Select(itemCode);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    string factoryName = stock["fac_name"].ToString();
                    string qty = stock["stock_qty"].ToString();
                    dgvStockReport.Rows[n].Cells[factoryName].Value = qty;
                    dgvStockReport.Rows[n].Cells[unitColumnName].Value = stock["stock_unit"].ToString();
                }
            }
        }

        private void listPaint(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //dgv.RowTemplate.Height = 40;

           
            dgv.ClearSelection();
        }

        private void insertDataToDatagridview()
        {
            DataTable dtItem;

            //if (string.IsNullOrEmpty(cmbSearchCat.Text) || cmbSearchCat.Text.Equals("All"))
            //{
            //    //show all item from the database
            //    dtItem = dalItem.Select();
            //}
            //else
            //{
            //    dtItem = dalItem.catSearch(cmbSearchCat.Text);
            //}

            dtItem = dalItem.Select();
            dgvStockReport.Rows.Clear();
            foreach (DataRow item in dtItem.Rows)
            {
                int n = dgvStockReport.Rows.Add();
                dgvStockReport.Rows[n].Cells["item_cat"].Value = item["item_cat"].ToString();
                dgvStockReport.Rows[n].Cells["item_code"].Value = item["item_code"].ToString();
                dgvStockReport.Rows[n].Cells["item_name"].Value = item["item_name"].ToString();
                dgvStockReport.Rows[n].Cells["item_qty"].Value = Convert.ToSingle(item["item_qty"]).ToString("0.00");

                loadStockList(item["item_code"].ToString(), n);
              
            }
        }

        private void dgvStockReport_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            string factoryName = "";

            DataTable dt = dalFac.Select();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow stock in dt.Rows)
                {
                    factoryName = stock["fac_name"].ToString();
                    e.Row.Cells[factoryName].Value = "0";
                }
            }
            
        }

        private void frmStockReport_Load(object sender, EventArgs e)
        {
            dgvStockReport.ClearSelection();
        }
    }
}
