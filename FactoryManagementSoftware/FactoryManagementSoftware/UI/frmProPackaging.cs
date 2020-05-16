using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmProPackaging : Form
    {
        public frmProPackaging()
        {
            InitializeComponent();
            LoadPackingToCMB();
        }

        public frmProPackaging(int sheetID)
        {
            InitializeComponent();

            LoadPackingToCMB();

            LoadPackagingList(sheetID);
        }

        public frmProPackaging(DataTable dt)
        {
            InitializeComponent();

            LoadPackingToCMB();

            dgvPackaging.DataSource = dt;
        }

        readonly static public string header_PackagingCode = "CODE";
        readonly static public string header_PackagingName = "NAME";
        readonly static public string header_PackagingQty = "TOTAL QTY";
        readonly static public string header_PackagingMax = "QTY/BOX";

        readonly private string string_AddItem = "ADD ITEM";
        readonly private string string_EditItem = "EDIT ITEM";

        private int EditingRowIndex = -1;
        static public bool dataSaved = false;

        static public DataTable dt_Packaging;

        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();

        itemDAL dalItem = new itemDAL();

        Tool tool = new Tool();
        Text text = new Text();

        private DataTable NewPackagingTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_PackagingCode, typeof(string));
            dt.Columns.Add(header_PackagingName, typeof(string));
            dt.Columns.Add(header_PackagingMax, typeof(int));
            dt.Columns.Add(header_PackagingQty, typeof(int));

            return dt;
        }

        private void LoadPackingToCMB()
        {
            DataTable dt_Packaging = dalItem.CatSearch(text.Cat_Packaging);
            DataTable dt_Packaging_ItemName = dt_Packaging.DefaultView.ToTable(true, "item_name");

            DataTable dt_Carton = dalItem.CatSearch(text.Cat_Carton);
            DataTable dt_Carton_ItemName = dt_Carton.DefaultView.ToTable(true, "item_name");

            DataTable dt_All = dt_Packaging.Copy();
            dt_All.Merge(dt_Carton);

            dt_All.DefaultView.Sort = "item_name ASC";

            cmbPackingName.DataSource = dt_All;
            cmbPackingName.DisplayMember = "item_name";

            cmbPackingName.SelectedIndex = -1;

            cmbPackingCode.DataSource = null;

        }

        private void ClearAllError()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
        }

        private bool Validation()
        {
            ClearAllError();
            bool passed = true;

            if (string.IsNullOrEmpty(txtPackagingMax.Text))
            {
                errorProvider1.SetError(lblPcs, "Please key in packaging max qty per box.");
                passed = false;
            }

            if (string.IsNullOrEmpty(cmbPackingName.Text))
            {
                errorProvider2.SetError(lblBoxName, "Please select a packaging box");
                passed = false;
            }

            if (string.IsNullOrEmpty(cmbPackingCode.Text))
            {
                errorProvider3.SetError(lblBoxCode, "Please select a packaging box");
                passed = false;
            }

            if (string.IsNullOrEmpty(txtTotalBox.Text))
            {
                errorProvider4.SetError(lblTotalBox, "Please key in qty of total box.");
                passed = false;
            }

            return passed;
        }

        private void LoadPackagingList(int sheetID)
        {
            DataGridView dgv = dgvPackaging;

            dgv.DataSource = null;

            DataTable dt = NewPackagingTable();

            DataTable dt_PackagingFromDB = dalProRecord.PackagingRecordSelect(sheetID);

            foreach(DataRow row in dt_PackagingFromDB.Rows)
            {
                DataRow dt_Row = dt.NewRow();

                string packagingCode = row[dalProRecord.PackagingCode].ToString();

                dt_Row[header_PackagingCode] = packagingCode;
                dt_Row[header_PackagingName] = tool.getItemName(packagingCode);
                dt_Row[header_PackagingMax] = row[dalProRecord.PackagingMax].ToString();
                dt_Row[header_PackagingQty] = row[dalProRecord.PackagingQty].ToString();

                dt.Rows.Add(dt_Row);
            }

            dgv.DataSource = dt;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            dataSaved = false;
            Close();
        }

        private void frmProPackaging_Load(object sender, EventArgs e)
        {
            
        }

        private void cmbPackingName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tool.loadItemCodeDataToComboBox(cmbPackingCode, cmbPackingName.Text);
        }

        private void txtPackingQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtTotalBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void ClearData()
        {
            txtPackagingMax.Clear();
            txtTotalBox.Clear();
            cmbPackingName.SelectedIndex = -1;

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                int maxQty = int.TryParse(txtPackagingMax.Text, out maxQty) ? maxQty : 0;
                int totalBox = int.TryParse(txtTotalBox.Text, out totalBox) ? totalBox : 0;

                string packagingCode = cmbPackingCode.Text;
                string packagingName = cmbPackingName.Text;

                DataTable dt = (DataTable)dgvPackaging.DataSource;

                if(EditingRowIndex == -1)
                {
                    DataRow dt_Row = dt.NewRow();

                    dt_Row[header_PackagingCode] = packagingCode;
                    dt_Row[header_PackagingName] = packagingName;
                    dt_Row[header_PackagingMax] = maxQty;
                    dt_Row[header_PackagingQty] = totalBox;

                    dt.Rows.Add(dt_Row);
                }
                else
                {
                    dt.Rows[EditingRowIndex][header_PackagingCode] = packagingCode;
                    dt.Rows[EditingRowIndex][header_PackagingName] = packagingName;
                    dt.Rows[EditingRowIndex][header_PackagingMax] = maxQty;
                    dt.Rows[EditingRowIndex][header_PackagingQty] = totalBox;

                    EditingRowIndex = -1;
                    btnAddItem.Text = string_AddItem;
                }
               

                ClearData();
            }
        }

        private bool CheckIfNullItemExist(DataTable dt)
        {
            bool result = false;

            foreach(DataRow row in dt.Rows)
            {
                string code = row[header_PackagingCode].ToString();

                if(string.IsNullOrEmpty(code))
                {
                    return true;
                }
            }

            return result;
        }
        private void btnSaveAndStock_Click(object sender, EventArgs e)
        {
            dt_Packaging = (DataTable)dgvPackaging.DataSource;

            if (dt_Packaging.Rows.Count > 0)
            {
                if(CheckIfNullItemExist(dt_Packaging))
                {
                    MessageBox.Show("Packaging item cannot be EMPTY!\n(double click to edit row)");
                }
                else
                {
                    dataSaved = true;
                    MessageBox.Show("Packaging data added!");
                    Close();
                }
                
            }
            else
            {
                dataSaved = true;
                Close();
            }
        }

        private void dgvPackaging_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvPackaging;
            dgv.SuspendLayout();

            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;
           
            contextMenuStrip1.Hide();

            if (itemClicked.Equals("Remove from this list."))
            {
                DataTable dt = (DataTable)dgv.DataSource;

                dt.Rows.RemoveAt(rowIndex);
                dt.AcceptChanges();

            }

            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void dgvPackaging_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            DataGridView dgv = dgvPackaging;
            //handle the row selection on right click

            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();
                int rowIndex = dgv.CurrentCell.RowIndex;

                try
                {
                    my_menu.Items.Add("Remove from this list.").Name = "Remove from this list.";

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1 = my_menu;
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(dgvPackaging_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void RowEdit(DataGridView dgv,int row)
        {
            btnAddItem.Text = string_EditItem;
            EditingRowIndex = row;
            string qtyPerBox = dgv.Rows[row].Cells[header_PackagingMax].Value.ToString();
            string code = dgv.Rows[row].Cells[header_PackagingCode].Value.ToString();
            string name = dgv.Rows[row].Cells[header_PackagingName].Value.ToString();
            string totalBox = dgv.Rows[row].Cells[header_PackagingQty].Value.ToString();

            txtPackagingMax.Text = qtyPerBox;
            txtTotalBox.Text = totalBox;

            cmbPackingName.Text = name;
            cmbPackingCode.Text = code;
        }

        private void dgvPackaging_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvPackaging;

            if(dgv.CurrentCell != null)
            {
                int row = dgv.CurrentCell.RowIndex;

                RowEdit(dgv, row);
            }
        }
    }
}
