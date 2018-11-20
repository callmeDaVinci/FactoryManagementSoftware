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
        public frmStockReport()
        {
            InitializeComponent();
        }

        itemBLL uItem = new itemBLL();
        itemDAL dalItem = new itemDAL();

        materialDAL dalMaterial = new materialDAL();
        materialBLL uMaterial = new materialBLL();

        itemCatDAL dALItemCat = new itemCatDAL();

        joinDAL dalJoin = new joinDAL();
        joinBLL uJoin = new joinBLL();

        static public string itemCode;
        private bool formLoaded = false;

        private void loadItemCategoryData()
        {
            DataTable dtItemCat = dALItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmbCat.DataSource = distinctTable;
            cmbCat.DisplayMember = "item_cat_name";

            cmbCat.SelectedIndex = -1;
        }

        private void frmStockReport_Load(object sender, EventArgs e)
        {
            loadItemCategoryData();
            formLoaded = true;
        }

        private void frmStockReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.stockReportFormOpen = false;
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

        private void listPaint(DataGridView dgv)
        {
            bool rowColorChange = true;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;
                if (rowColorChange)
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = SystemColors.Control;
                    rowColorChange = false;
                }
                else
                {
                    dgv.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    rowColorChange = true;
                }

                string itemCode = "";
                if (dgv == dgvStockReport)
                {
                    itemCode = dgv.Rows[n].Cells["dgvcItemCode"].Value.ToString();
                    float qty = 0;

                    if (dgv.Rows[n].Cells["dgvcQty"] != null)
                    {
                        float.TryParse(dgv.Rows[n].Cells["dgvcQty"].Value.ToString(), out (qty));
                    }

                    if (ifGotChild(itemCode))
                    {
                        dgv.Rows[n].Cells["dgvcItemCode"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline) };
                        dgv.Rows[n].Cells["dgvcItemName"].Style = new DataGridViewCellStyle { ForeColor = Color.Blue, Font = new System.Drawing.Font(dgv.Font, FontStyle.Underline) };
                    }
                    if (qty < 0)
                    {
                        dgv.Rows[n].Cells["dgvcQty"].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }
                }
            }


            dgv.ClearSelection();
        }

        private void loadItemData()
        {
            DataTable dt;
            if(cmbCat.Text.Equals("All"))
            {
                dt = dalItem.Select();
            }
            else
            {
                dt = dalItem.catSearch(cmbCat.Text);
            }
            
            dgvStockReport.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dgvStockReport.Rows.Add();

                dgvStockReport.Rows[n].Cells["Category"].Value = item["item_cat"].ToString();
                dgvStockReport.Rows[n].Cells["dgvcItemCode"].Value = item["item_code"].ToString();
                dgvStockReport.Rows[n].Cells["dgvcItemName"].Value = item["item_name"].ToString();
                dgvStockReport.Rows[n].Cells["dgvcQty"].Value = item["item_qty"].ToString();
                dgvStockReport.Rows[n].Cells["dgvcOrd"].Value = item["item_ord"].ToString();
            }
            listPaint(dgvStockReport);

            if (dt.Rows.Count <= 0 && formLoaded)
            {
                MessageBox.Show("no data under this record");
            }
        }

        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            if (formLoaded)
            {
                loadItemData();
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvStockReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            //MessageBox.Show("double click");
            int rowIndex = dgvStockReport.CurrentCell.RowIndex;

            if (rowIndex >= 0)
            {
                itemCode = dgvStockReport.Rows[rowIndex].Cells["dgvcItemCode"].Value.ToString();
               
                if (!string.IsNullOrEmpty(itemCode) || itemCode != "null")
                {
                    frmStockReportFactoryQty frm = new frmStockReportFactoryQty();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit 
                }
            }
      
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void frmStockReport_Click(object sender, EventArgs e)
        {
            dgvStockReport.ClearSelection();
        }
    }
}
