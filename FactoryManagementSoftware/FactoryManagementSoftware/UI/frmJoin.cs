using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmJoin : Form
    {
        public frmJoin()
        {
            InitializeComponent();
        }

        joinBLL uJoin = new joinBLL();
        joinDAL dalJoin = new joinDAL();

        private int currentRowIndex;

        private void loadData()
        {
            bool rowColorChange = true;

            DataTable dt = dalJoin.Select();

            dgvJoin.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dgvJoin.Rows.Add();
                if (rowColorChange)
                {
                    dgvJoin.Rows[n].DefaultCellStyle.BackColor = Control.DefaultBackColor;
                    rowColorChange = false;
                }
                else
                {
                    dgvJoin.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    rowColorChange = true;
                }

                dgvJoin.Rows[n].Cells["join_parent_code"].Value = item["join_parent_code"].ToString();
                dgvJoin.Rows[n].Cells["join_parent_name"].Value = item["join_parent_name"].ToString();
                dgvJoin.Rows[n].Cells["join_child_code"].Value = item["join_child_code"].ToString();
                dgvJoin.Rows[n].Cells["join_child_name"].Value = item["join_child_name"].ToString();
                dgvJoin.Rows[n].Cells["join_qty"].Value = item["join_qty"].ToString();


            }

            dgvJoin.ClearSelection();  
        }

        private void frmJoin_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.joinFormOpen = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmJoinEdit frm = new frmJoinEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit
            loadData();
        }

        private void frmJoin_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtSearch.Text))
            {
                loadData();
            }
            else
            {
                DataTable dt;
                string keyword = txtSearch.Text;
                if (cmbCat.Text.Equals("Parent"))
                {
                    dt = dalJoin.parentSearch(keyword);
                }
                else if (cmbCat.Text.Equals("Child"))
                {
                    dt = dalJoin.childSearch(keyword);
                }
                else
                {
                    dt = dalJoin.Search(keyword);
                }

                bool rowColorChange = true;
                dgvJoin.Rows.Clear();

                foreach (DataRow item in dt.Rows)
                {
                    int n = dgvJoin.Rows.Add();
                    if (rowColorChange)
                    {
                        dgvJoin.Rows[n].DefaultCellStyle.BackColor = Control.DefaultBackColor;
                        rowColorChange = false;
                    }
                    else
                    {
                        dgvJoin.Rows[n].DefaultCellStyle.BackColor = Color.White;
                        rowColorChange = true;
                    }

                    dgvJoin.Rows[n].Cells["join_parent_code"].Value = item["join_parent_code"].ToString();
                    dgvJoin.Rows[n].Cells["join_parent_name"].Value = item["join_parent_name"].ToString();
                    dgvJoin.Rows[n].Cells["join_child_code"].Value = item["join_child_code"].ToString();
                    dgvJoin.Rows[n].Cells["join_child_name"].Value = item["join_child_name"].ToString();
                    dgvJoin.Rows[n].Cells["join_qty"].Value = item["join_qty"].ToString();
                }
                dgvJoin.ClearSelection();
            }
        }

        private void dgvJoin_Sorted(object sender, EventArgs e)
        {
            bool rowColorChange = true;
            foreach (DataGridViewRow row in dgvJoin.Rows)
            {
                int n = row.Index;
                if (rowColorChange)
                {
                    dgvJoin.Rows[n].DefaultCellStyle.BackColor = Control.DefaultBackColor;
                    rowColorChange = false;
                }
                else
                {
                    dgvJoin.Rows[n].DefaultCellStyle.BackColor = Color.White;
                    rowColorChange = true;
                }
            }
            dgvJoin.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvJoin.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    uJoin.join_parent_code = dgvJoin.Rows[currentRowIndex].Cells["join_parent_code"].Value.ToString();
                    uJoin.join_child_code = dgvJoin.Rows[currentRowIndex].Cells["join_child_code"].Value.ToString();

                    bool success = dalJoin.Delete(uJoin);

                    if (success == true)
                    {
                        //item deleted successfully
                        MessageBox.Show("Item deleted successfully");
                        loadData();
                    }
                    else
                    {
                        //Failed to delete item
                        MessageBox.Show("Failed to delete item");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a data");
            }
        }

        private void dgvJoin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentRowIndex = e.RowIndex;
        }

        private void dgvJoin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentRowIndex = e.RowIndex;
        }
    }
}
