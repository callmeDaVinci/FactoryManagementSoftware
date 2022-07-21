using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
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
        itemDAL dalItem = new itemDAL();

        private int currentRowIndex = -1;
        private int selectedRow = -1;
        static public string editedParentCode = "";
        static public string editedChildCode = "";

        readonly string headerIndex = "#";
        readonly string headerParentCode = "PARENT CODE";
        readonly string headerParentName = "PARENT NAME";
        readonly string headerChildCode = "CHILD CODE";
        readonly string headerChildName = "CHILD NAME";
        readonly string headerQty = "QTY(1:X)";
        readonly string headerMax = "MAX";
        readonly string headerMin = "MIN";
        readonly string headerUpdatedDate = "UPDATED DATE";
        readonly string headerUpdatedBy = "UPDATED BY";
        readonly string headerColorFlag = "Color Flag";
        readonly string GainsboroColor = "Gainsboro";
        readonly string WhiteColor = "White";

        private bool colorGainsboro = true;
        private bool sorting = false;

        private string lastParentCode = "No Data";
        #region Create Datatable

        private DataTable NewJoinTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerIndex, typeof(int));

            dt.Columns.Add(headerParentCode, typeof(string));
            dt.Columns.Add(headerParentName, typeof(string));

            dt.Columns.Add(headerChildCode, typeof(string));
            dt.Columns.Add(headerChildName, typeof(string));

            dt.Columns.Add(headerQty, typeof(float));
            dt.Columns.Add(headerMax, typeof(int));
            dt.Columns.Add(headerMin, typeof(int));

            dt.Columns.Add(headerUpdatedDate, typeof(DateTime));
            dt.Columns.Add(headerUpdatedBy, typeof(int));

            dt.Columns.Add(headerColorFlag, typeof(string));
            return dt;
        }

        private void dgvJoinUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerParentCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerParentName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[headerChildCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerChildName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[headerQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMax].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMin].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerUpdatedDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerUpdatedBy].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerColorFlag].Visible = false;
        }

        #endregion
        private void loadData()
        {
            sorting = false;
            DataTable dtJoin = NewJoinTable();

            DataRow dtJoin_row;

            DataGridView dgv = dgvJoin;
            string keywords = txtSearch.Text;
            int index = 1;
            if (keywords != null)
            {
                DataTable dt_joinData = dalJoin.OldSearch(keywords);

                foreach (DataRow row in dt_joinData.Rows)
                {
                    if (row["child_code"].ToString().Equals("V76KM4000 0.360"))
                    {
                        float jointest = 0;
                    }

                    dtJoin_row = dtJoin.NewRow();

                    //string itemCode = row["ord_item_code"].ToString();

                    dtJoin_row[headerIndex] = index;

                    dtJoin_row[headerParentCode] = row["parent_code"].ToString();
                    dtJoin_row[headerParentName] = row["parent_name"].ToString();
                
                    dtJoin_row[headerChildCode] = row["child_code"].ToString();
                    dtJoin_row[headerChildName] = row["child_name"].ToString();

                    if (!editedParentCode.Equals(""))
                    {
                        if (editedParentCode.Equals(row["parent_code"].ToString()) && editedChildCode.Equals(row["child_code"].ToString()))
                        {
                            selectedRow = index - 1;
                        }
                    }
                    float joinQty = row["join_qty"] == DBNull.Value ? 0 : Convert.ToSingle(row["join_qty"].ToString());
                    dtJoin_row[headerQty] = joinQty;
                    dtJoin_row[headerMax] = row["join_max"] == DBNull.Value ? 0 : Convert.ToInt32(row["join_max"].ToString());
                    dtJoin_row[headerMin] = row["join_min"] == DBNull.Value ? 0 : Convert.ToInt32(row["join_min"].ToString());

                    if(!row["join_updated_date"].ToString().Equals(""))
                    {
                        dtJoin_row[headerUpdatedDate] = row["join_updated_date"].ToString();
                        dtJoin_row[headerUpdatedBy] = row["join_updated_by"].ToString();
                    }
                    else
                    {
                        dtJoin_row[headerUpdatedDate] = row["join_added_date"].ToString();
                        dtJoin_row[headerUpdatedBy] = row["join_added_by"].ToString();
                    }
                    

                    if (lastParentCode.Equals("No Data"))
                    {
                        lastParentCode = row["parent_code"].ToString();

                        dtJoin_row[headerColorFlag] = GainsboroColor;
                    }
                    else if (lastParentCode == row["parent_code"].ToString())
                    {
                        if (colorGainsboro)
                        {
                            dtJoin_row[headerColorFlag] = GainsboroColor;
                        }
                        else
                        {
                            dtJoin_row[headerColorFlag] = WhiteColor;
                        }
                    }
                    else if (lastParentCode != row["parent_code"].ToString())
                    {
                        if (colorGainsboro)
                        {
                            colorGainsboro = false;
                        }
                        else
                        {
                            colorGainsboro = true;
                        }

                        if (colorGainsboro)
                        {
                            dtJoin_row[headerColorFlag] = GainsboroColor;
                        }
                        else
                        {
                            dtJoin_row[headerColorFlag] = WhiteColor;
                        }

                        lastParentCode = row["parent_code"].ToString();
                    }

                    dtJoin.Rows.Add(dtJoin_row);
                    index++;
                }
            }

            dgv.DataSource = null;

            if (dtJoin.Rows.Count > 0)
            {
                dtJoin.DefaultView.Sort = "PARENT CODE ASC";
                dgv.DataSource = dtJoin;
                dgvJoinUIEdit(dgv);

                if(selectedRow > -1 && selectedRow < dgv.Rows.Count)
                {
                    dgv.ClearSelection();
                    dgv.FirstDisplayedScrollingRowIndex = selectedRow;
                    dgv.Rows[selectedRow].Selected = true;
                } 
            }
        }

        private void frmJoin_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.joinFormOpen = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
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
            loadData();
        }

        private void dgvJoin_Sorted(object sender, EventArgs e)
        {
            //bool rowColorChange = true;
            //foreach (DataGridViewRow row in dgvJoin.Rows)
            //{
            //    int n = row.Index;
            //    if (rowColorChange)
            //    {
            //        dgvJoin.Rows[n].DefaultCellStyle.BackColor = Control.DefaultBackColor;
            //        rowColorChange = false;
            //    }
            //    else
            //    {
            //        dgvJoin.Rows[n].DefaultCellStyle.BackColor = Color.White;
            //        rowColorChange = true;
            //    }
            //}
            //dgvJoin.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvJoin.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    int rowIndex = dgvJoin.CurrentRow.Index;
                    uJoin.join_parent_code = dgvJoin.Rows[rowIndex].Cells[headerParentCode].Value.ToString();
                    uJoin.join_child_code = dgvJoin.Rows[rowIndex].Cells[headerChildCode].Value.ToString();

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

        private void dgvJoin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvJoin;

            int col = e.ColumnIndex;
            int row = e.RowIndex;

            if (dgv.Columns[e.ColumnIndex].Name == headerParentCode && !sorting)
            {
                string colorFlag = dgv.Rows[row].Cells[headerColorFlag].Value.ToString();

                if (colorFlag.Equals(GainsboroColor))
                {
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.Gainsboro;
                }
                else
                {
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.White;
                }
            }
            else if(sorting)
            {
                dgv.Rows[row].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void dgvJoin_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            currentRowIndex = row;
            DataGridView dgv = dgvJoin;
            
            uJoin.join_parent_code = dgv.Rows[row].Cells[headerParentCode].Value.ToString();

            uJoin.join_child_code = dgv.Rows[row].Cells[headerChildCode].Value.ToString();

            uJoin.join_qty = Convert.ToSingle(dgv.Rows[row].Cells[headerQty].Value.ToString());
            uJoin.join_max = Convert.ToInt16(dgv.Rows[row].Cells[headerMax].Value.ToString());
            uJoin.join_min = Convert.ToInt16(dgv.Rows[row].Cells[headerMin].Value.ToString());

            txtSearch.Clear();
            frmJoinEdit frm = new frmJoinEdit(uJoin,false);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            loadData();
        }

        private void dgvJoin_Sorted_1(object sender, EventArgs e)
        {
            sorting = true;
        }

        private void dgvJoin_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvJoin_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
