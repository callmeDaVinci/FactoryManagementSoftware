using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;
using System;
using System.Data;
using System.Drawing;
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
                dgv.Rows[n].Cells[dalUser.AddedBy].Value = user[dalUser.AddedBy].ToString();
                dgv.Rows[n].Cells[dalUser.UpdatedDate].Value = user[dalUser.UpdatedDate].ToString();
                dgv.Rows[n].Cells[dalUser.UpdatedBy].Value = user[dalUser.UpdatedBy].ToString();

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
    }
}
