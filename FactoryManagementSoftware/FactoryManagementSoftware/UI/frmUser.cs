using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using System;
using System.Data;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }
        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;


        Tool tool = new Tool();
        userDAL dalUser = new userDAL();
        userBLL uUser = new userBLL();

        historyDAL dalHistory = new historyDAL();
        historyBLL uHistory = new historyBLL();

        //create column
        private void AddColumns(DataGridView dgv)
        {
            dgv.Columns.Clear();
            tool.AddTextBoxColumns(dgv, "ID", dalUser.UserID, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Username", dalUser.Username, Fill);
            tool.AddTextBoxColumns(dgv, "Permissions", dalUser.Permission, Fill);
            tool.AddTextBoxColumns(dgv, "Added Date", dalUser.AddedDate, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Added By", dalUser.AddedBy, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Updated Date", dalUser.UpdatedDate, DisplayedCells);
            tool.AddTextBoxColumns(dgv, "Updated By", dalUser.UpdatedBy, DisplayedCells);
        }

        private void LoadUserList(DataGridView dgv)
        {
            DataTable dt = dalUser.Select();
            dgv.Rows.Clear();

            foreach (DataRow user in dt.Rows)
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[dalUser.UserID].Value = user[dalUser.UserID].ToString();
                dgv.Rows[n].Cells[dalUser.Username].Value = user[dalUser.Username].ToString();
                dgv.Rows[n].Cells[dalUser.Permission].Value = user[dalUser.Permission].ToString();
                dgv.Rows[n].Cells[dalUser.AddedDate].Value = user[dalUser.AddedDate].ToString();
                dgv.Rows[n].Cells[dalUser.UpdatedDate].Value = user[dalUser.UpdatedDate].ToString();
                dgv.Rows[n].Cells[dalUser.UpdatedBy].Value = user[dalUser.UpdatedBy].ToString();

                if(Convert.ToInt32(user[dalUser.AddedBy]) <= 0)
                {
                    dgv.Rows[n].Cells[dalUser.AddedBy].Value = "ADMIN";
                }
                else
                {
                    dgv.Rows[n].Cells[dalUser.AddedBy].Value = dalUser.getUsername(Convert.ToInt32(user[dalUser.AddedBy]));
                }

                if(int.TryParse(user[dalUser.UpdatedBy].ToString(), out int by))
                {
                    dgv.Rows[n].Cells[dalUser.UpdatedBy].Value = dalUser.getUsername(Convert.ToInt32(user[dalUser.UpdatedBy]));
                }

            }
            tool.listPaint(dgv);
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            DataGridView dgv = dgvUser;
            AddColumns(dgv);
            LoadUserList(dgv);
            dgv.ClearSelection();
        }

        private void frmUser_Click(object sender, EventArgs e)
        {
            dgvUser.ClearSelection();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            frmUserEdit frm = new frmUserEdit();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            LoadUserList(dgvUser);
            dgvUser.ClearSelection();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgvUser.SelectedRows.Count > 0)
            {
                int rowIndex = dgvUser.CurrentCell.RowIndex;

                uUser.user_id = Convert.ToInt32(dgvUser.Rows[rowIndex].Cells[dalUser.UserID].Value);
                uUser.user_name = dgvUser.Rows[rowIndex].Cells[dalUser.Username].Value.ToString();
                uUser.user_password = dalUser.getPassword(uUser.user_id);
                uUser.user_permissions = Convert.ToInt32(dgvUser.Rows[rowIndex].Cells[dalUser.Permission].Value);

                frmUserEdit frm = new frmUserEdit(uUser);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();//Item Edit

                LoadUserList(dgvUser);
                dgvUser.ClearSelection();
            }
            else
            {
                MessageBox.Show("Please select a user.");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvUser;

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            if (dgv.SelectedRows.Count > 0)
            {
                int n = dgv.CurrentCell.RowIndex;
                DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this user?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {

                    int rowIndex = dgvUser.CurrentCell.RowIndex;

                    uUser.user_id = Convert.ToInt32(dgvUser.Rows[rowIndex].Cells[dalUser.UserID].Value);

                    if(!dalUser.Delete(uUser))
                    {
                        MessageBox.Show("Failed to delete user.");
                    }
                    else
                    {
                        LoadUserList(dgvUser);
                        dgvUser.ClearSelection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user");
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvUser;
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            //MessageBox.Show("double click");
            int rowIndex = dgv.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                int userID = Convert.ToInt32(dgv.Rows[rowIndex].Cells[dalUser.UserID].Value);

                DataTable dt = dalHistory.userSearch(userID);
                if (dt.Rows.Count > 0)
                {
                    frmUserActionHistory frm = new frmUserActionHistory(dt);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit
                }
                else
                {
                    MessageBox.Show("No action record under this user yet.");
                }
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
        }
    }
}
