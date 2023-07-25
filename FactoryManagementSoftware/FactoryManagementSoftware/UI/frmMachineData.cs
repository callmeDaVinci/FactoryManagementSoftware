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
    public partial class frmMachineData : Form
    {
        Tool tool = new Tool();
        Text text = new Text();
        MacDAL dalMac = new MacDAL();
        MacBLL uMac = new MacBLL();

        readonly string headerID = "ID";
        readonly string headerName = "NAME";
        readonly string headerTon = "TON";
        readonly string headerLocation = "LOCATION";
        readonly string headerAddedBy = "ADDED BY";
        readonly string headerAddedDate = "ADDED DATE";
        readonly string headerUpdatedBy = "UPDATED BY";
        readonly string headerUpdatedDate = "UPDATED DATE";

        private int ID = -1;

        public frmMachineData()
        {
            InitializeComponent();
            tool.loadFactory(cmbLocation);
            cmbLocation.SelectedIndex = -1;
            loadMacData();
        }

        private DataTable NewMacTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerID, typeof(int));
            dt.Columns.Add(headerName, typeof(string));
            dt.Columns.Add(headerTon, typeof(int));
            dt.Columns.Add(headerLocation, typeof(string));
            dt.Columns.Add(headerAddedDate, typeof(DateTime));
            dt.Columns.Add(headerAddedBy, typeof(string));
            dt.Columns.Add(headerUpdatedDate, typeof(DateTime));
            dt.Columns.Add(headerUpdatedBy, typeof(string));

            return dt;
        }

        private void dgvMacUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerTon].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerLocation].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerAddedDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerAddedBy].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerUpdatedDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerUpdatedBy].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

        }

        private void loadMacData()
        {
            DataTable dt = dalMac.Select();

            DataTable dt_Mac = NewMacTable();
            DataRow row_Mac;

            foreach (DataRow row in dt.Rows)
            {
                row_Mac = dt_Mac.NewRow();

                row_Mac[headerID] = row[dalMac.MacID];
                row_Mac[headerName] = row[dalMac.MacName];
                row_Mac[headerTon] = row[dalMac.MacTon];
                row_Mac[headerLocation] = row[dalMac.MacLocationName];
                row_Mac[headerAddedDate] = row[dalMac.MacAddedDate];
                row_Mac[headerAddedBy] = row[dalMac.MacAddedBy];

                if(row[dalMac.MacUpdatedDate] != DBNull.Value)
                {
                    row_Mac[headerUpdatedDate] = Convert.ToDateTime(row[dalMac.MacUpdatedDate]);
                }
                
                if(row[dalMac.MacUpdatedBy] != DBNull.Value)
                {
                    row_Mac[headerUpdatedBy] = Convert.ToInt32(row[dalMac.MacUpdatedBy]);
                }
                

                dt_Mac.Rows.Add(row_Mac);
            }

            dgvMac.DataSource = null;
            if (dt_Mac.Rows.Count > 0)
            {
                dgvMac.DataSource = dt_Mac;
                dgvMacUIEdit(dgvMac);
                dgvMac.ClearSelection();
            }
        }

        private bool Validation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(txtName.Text))
            {
                result = false;
                errorProvider1.SetError(txtName, "Machine Name Required");
            }

            if (string.IsNullOrEmpty(txtTon.Text))
            {
                result = false;
                errorProvider2.SetError(txtTon, "Machine Ton Required");
            }

            if (string.IsNullOrEmpty(cmbLocation.Text))
            {
                result = false;
                errorProvider3.SetError(cmbLocation, "Machine Location Required");
            }

            return result;

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void txtTon_TextChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider3.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                string macName = txtName.Text;
                string macTon = txtTon.Text;
                string macLocation = cmbLocation.Text;

                DateTime datetime = DateTime.Now;
                uMac.mac_name = macName;
                uMac.mac_ton = Convert.ToInt32(macTon);
                uMac.mac_location = macLocation;
                uMac.mac_lot_no = 1;

                bool success = false;

                if (ID == -1)
                {
                    uMac.mac_added_date = datetime;
                    uMac.mac_added_by = MainDashboard.USER_ID;

                    success = dalMac.Insert(uMac);
                }
                else
                {
                    uMac.mac_id = ID;
                    uMac.mac_updated_date = datetime;
                    uMac.mac_updated_by = MainDashboard.USER_ID;
                    success = dalMac.Update(uMac);
                    ID = -1;
                }

                
                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show("Failed to save Machine data");
                    tool.historyRecord(text.System, "Failed to save Machine data", datetime, MainDashboard.USER_ID);
                }
                else
                {
                    tool.historyRecord(text.System, "Save Machine Data: " + macName + " (TON: " + macTon + " Location: " + Location + ")", datetime, MainDashboard.USER_ID);
                    loadMacData();
                    ClearData();
                    
                }
            }
        }

        private void txtTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void frmMachineData_Load(object sender, EventArgs e)
        {
            dgvMac.ClearSelection();
            ActiveControl = txtName;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            txtName.Clear();
            txtTon.Clear();
            cmbLocation.SelectedIndex = -1;
            btnSave.Text = "ADD";
            ID = -1;
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
        }

        private void dgvMac_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            btnSave.Text = "UPDATE";
            ID = Convert.ToInt32(dgvMac.Rows[row].Cells[headerID].Value);
            txtName.Text = dgvMac.Rows[row].Cells[headerName].Value == DBNull.Value? "" : dgvMac.Rows[row].Cells[headerName].Value.ToString();
            txtTon.Text = dgvMac.Rows[row].Cells[headerTon].Value == DBNull.Value ? "" : dgvMac.Rows[row].Cells[headerTon].Value.ToString();
            cmbLocation.Text = dgvMac.Rows[row].Cells[headerLocation].Value == DBNull.Value ? "" : dgvMac.Rows[row].Cells[headerLocation].Value.ToString();

           
        }
    }
}
