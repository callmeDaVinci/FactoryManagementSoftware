using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using FactoryManagementSoftware.UI;

namespace FactoryManagementSoftware
{
    public partial class frmCartonSetting : Form
    {
        Tool tool = new Tool();
        Text text = new Text();

        joinDAL dalJoin = new joinDAL();
        joinBLL uJoin = new joinBLL();
        itemDAL dalItem = new itemDAL();
        static public DataTable _DT_CARTON;

        public frmCartonSetting()
        {
            InitializeComponent();
        }

        public frmCartonSetting(DataTable dt)
        {
            InitializeComponent();

            dgvCarton.DataSource = dt;

            dgvCartonEdit(dgvCarton);

            LoadCMBData();

        }

        private void LoadCMBData()
        {
            tool.loadItemCategoryDataToComboBox(cmbCategory, text.Cat_Carton);

            string keywords = cmbCategory.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.CatSearch(keywords);

                if (dt.Rows.Count > 0)
                {
                    DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                    distinctTable.DefaultView.Sort = "item_name ASC";
                    cmbName.DataSource = distinctTable;
                    cmbName.DisplayMember = "item_name";
                    cmbName.ValueMember = "item_name";
                    cmbName.SelectedIndex = -1;
                }

                if (string.IsNullOrEmpty(cmbName.Text))
                {
                    cmbCode.DataSource = null;
                }
            }
            else
            {
                cmbCode.DataSource = null;
            }

            keywords = cmbName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
                cmbCode.DataSource = dt;
                cmbCode.DisplayMember = "item_code";
                cmbCode.ValueMember = "item_code";
            }
            else
            {
                cmbCode.DataSource = null;
            }

        }

        //readonly private string header_Index = "#";
        //readonly private string header_PartQty_Max = "PART QTY MAX";
        //readonly private string header_PartQty_Min = "PART QTY MIN";
        //readonly private string header_PartQtyString = "PART QTY";
        //readonly private string header_ChildItemCode = "CHILD ITEM CODE";
        //readonly private string header_ChildItemName = "CHILD ITEM NAME";
        //readonly private string header_ChildItemString = "CHILD ITEM";
        //readonly private string header_ChildItemQty = "CHILD ITEM QTY";
        //readonly private string header_Using = "USING";
        //readonly private string header_MainCarton = "MAIN CARTON";
        //readonly private string header_StockOut = "STOCK OUT";

        //readonly private string text_EditItem = "EDIT";
        readonly private string text_RemoveItem = "REMOVE";
        //readonly private string text_ItemGroupTitle = "ITEM GROUP: ";

        private string header_PackagingCode = "CODE";
        private string header_PackagingName = "NAME";
        private string header_PackagingQty = "CARTON QTY";
        private string header_PackagingMax = "PART QTY PER BOX";
        private string header_PackagingStockOut = "STOCK OUT";

        private DataTable NewCartonTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_PackagingCode, typeof(string));
            dt.Columns.Add(header_PackagingName, typeof(string));
            dt.Columns.Add(header_PackagingMax, typeof(int));
            dt.Columns.Add(header_PackagingQty, typeof(int));
            dt.Columns.Add(header_PackagingStockOut, typeof(bool));

            return dt;
        }

        private void dgvCartonEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            dgv.Columns[header_PackagingMax].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_PackagingQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_PackagingCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_PackagingName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[header_MeterReading].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Hourly].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgv.Columns[header_Operator].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dgv.Columns[header_PartQty_Min].Visible = false;
            //dgv.Columns[header_PartQty_Max].Visible = false;
            //dgv.Columns[header_ChildItemCode].Visible = false;
            //dgv.Columns[header_ChildItemName].Visible = false;

            //dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_PartQtyString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_ChildItemQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_ChildItemString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns[header_PackagingCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            // dgv.Columns[header_MeterReading].DefaultCellStyle.BackColor = SystemColors.Info;
        }

        private void frmItemGroupList_Load(object sender, EventArgs e)
        {
         
        }

        private void dgvCarton_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvCarton;

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
                    //my_menu.Items.Add(text_EditItem).Name = text_EditItem;
                    my_menu.Items.Add(text_RemoveItem).Name = text_RemoveItem;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);
                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(dgvItemGroup_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dgvItemGroup_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvCarton;
            dgv.SuspendLayout();

            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;

            if (itemClicked.Equals(text_RemoveItem))
            {
                dgv.Rows.Remove(dgv.Rows[rowIndex]);
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string itemCode = cmbCode.Text;
            string itemName = cmbName.Text;

            if(!string.IsNullOrEmpty(itemCode))
            {
                DataTable dt = (DataTable)dgvCarton.DataSource;

                if(dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        if(row[header_PackagingCode].ToString().Equals(itemCode))
                        {
                            MessageBox.Show("Item added!");
                            return;

                        }
                    }
                }

                DataRow newRow = dt.NewRow();

                newRow[header_PackagingName] = itemName;
                newRow[header_PackagingCode] = itemCode;
                newRow[header_PackagingStockOut] = true;

                dt.Rows.Add(newRow);

            }
            else
            {
                MessageBox.Show("Item Code cannot be empty.");
            }
           
            
           
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keywords = cmbCategory.Text;
            

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.CatSearch(keywords);

                if (dt.Rows.Count > 0)
                {
                    DataTable distinctTable = dt.DefaultView.ToTable(true, "item_name");
                    distinctTable.DefaultView.Sort = "item_name ASC";
                    cmbName.DataSource = distinctTable;
                    cmbName.DisplayMember = "item_name";
                    cmbName.ValueMember = "item_name";

                }

                if (string.IsNullOrEmpty(cmbName.Text))
                {
                    cmbCode.DataSource = null;
                }
            }
            else
            {
                cmbCode.DataSource = null;
            }
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string keywords = cmbName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
                cmbCode.DataSource = dt;
                cmbCode.DisplayMember = "item_code";
                cmbCode.ValueMember = "item_code";
            }
            else
            {
                cmbCode.DataSource = null;
            }
        }

        private void frmCartonSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            //check if all filled
            _DT_CARTON = (DataTable)dgvCarton.DataSource;

            bool emplyCellFound = false;

            for (int i = 0; i < dgvCarton.Rows.Count; i++)
            {
                string max = dgvCarton.Rows[i].Cells[header_PackagingMax].Value.ToString();
                string qty = dgvCarton.Rows[i].Cells[header_PackagingQty].Value.ToString();

                if (string.IsNullOrEmpty(max))
                {
                    dgvCarton.Rows[i].Cells[header_PackagingMax].Style.BackColor = Color.LightYellow;
                    emplyCellFound = true;
                }
                else
                {
                    dgvCarton.Rows[i].Cells[header_PackagingMax].Style.BackColor = Color.White;
                }

                if (string.IsNullOrEmpty(qty))
                {
                    dgvCarton.Rows[i].Cells[header_PackagingQty].Style.BackColor = Color.LightYellow;
                    emplyCellFound = true;
                }
                else
                {
                    dgvCarton.Rows[i].Cells[header_PackagingQty].Style.BackColor = Color.White;
                }
            }

            if (emplyCellFound)
            {
                MessageBox.Show("Please fill in the empty cell.");
                e.Cancel = true;
            }
           

        }

        private void frmCartonSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            _DT_CARTON = (DataTable)dgvCarton.DataSource;

        }

        private void dgvCarton_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvCarton;

            if (e.RowIndex != -1)
            {
                int col = e.ColumnIndex;
                int row = e.RowIndex;

                string colName = dgv.Columns[col].Name;

                if(colName.Equals(header_PackagingMax) || colName.Equals(header_PackagingQty) || colName.Equals(header_PackagingStockOut))
                {
                    dgv.ReadOnly = false;
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                }
                else
                {
                    dgv.ReadOnly = true;
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                }
                dgv.Rows[row].Cells[col].Selected = true;

            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void dgvCarton_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView dgv = dgvCarton;

                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);


                string colName = dgv.Columns[dgv.CurrentCell.ColumnIndex].Name;

                if (colName.Equals(header_PackagingMax) || colName.Equals(header_PackagingQty))
                {
                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {
                    TextBox tb = e.Control as TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                    }
                }

            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void dgvCarton_CellClick(object sender, MouseEventArgs e)
        {
        }

        private void dgvCarton_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //check if all filled
            _DT_CARTON = (DataTable)dgvCarton.DataSource;

            bool emplyCellFound = false;

            for(int i =0; i < dgvCarton.Rows.Count; i++)
            {
                string max = dgvCarton.Rows[i].Cells[header_PackagingMax].Value.ToString();
                string qty = dgvCarton.Rows[i].Cells[header_PackagingQty].Value.ToString();

                if (string.IsNullOrEmpty(max))
                {
                    dgvCarton.Rows[i].Cells[header_PackagingMax].Style.BackColor = Color.LightYellow;
                    emplyCellFound = true;
                }
                else
                {
                    dgvCarton.Rows[i].Cells[header_PackagingMax].Style.BackColor = Color.White;
                }

                if (string.IsNullOrEmpty(qty))
                {
                    dgvCarton.Rows[i].Cells[header_PackagingQty].Style.BackColor = Color.LightYellow;
                    emplyCellFound = true;
                }
                else
                {
                    dgvCarton.Rows[i].Cells[header_PackagingQty].Style.BackColor = Color.White;
                }
            }

            if(emplyCellFound)
            {
                MessageBox.Show("Please fill in the empty cell.");
            }
            else
            {
                Close();

            }

        }
    }
}
