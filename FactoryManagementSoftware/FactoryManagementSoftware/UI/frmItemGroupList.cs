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
    public partial class frmItemGroupList : Form
    {
        Tool tool = new Tool();
        Text text = new Text();

        joinDAL dalJoin = new joinDAL();
        joinBLL uJoin = new joinBLL();

        public frmItemGroupList()
        {
            InitializeComponent();
        }


        public frmItemGroupList(string itemCode, string itemName)
        {
            InitializeComponent();
            _ITEMCODE = itemCode;
            _ITEMNAME = itemName;
        }

        readonly private string header_Index = "#";
        readonly private string header_PartQty_Max = "PART QTY MAX";
        readonly private string header_PartQty_Min = "PART QTY MIN";
        readonly private string header_PartQtyString = "PART QTY";
        readonly private string header_ChildItemCode = "CHILD ITEM CODE";
        readonly private string header_ChildItemName = "CHILD ITEM NAME";
        readonly private string header_ChildItemString = "CHILD ITEM";
        readonly private string header_ChildItemQty = "CHILD ITEM QTY";
        readonly private string header_ChildStockOut = "CHILD STOCK OUT";
        readonly private string header_Using = "USING";
        readonly private string header_MainCarton = "MAIN CARTON";

        readonly private string text_EditItem = "EDIT";
        readonly private string text_RemoveItem = "REMOVE";
        readonly private string text_ItemGroupTitle = "ITEM GROUP: ";

        private string _ITEMCODE = null;
        private string _ITEMNAME = null;
        private ContextMenuStrip my_menu;

        private DataTable NewItemGroupTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Index, typeof(int));
            dt.Columns.Add(header_PartQty_Min, typeof(int));
            dt.Columns.Add(header_PartQty_Max, typeof(int));
            dt.Columns.Add(header_PartQtyString, typeof(string));
            dt.Columns.Add(header_ChildItemCode, typeof(string));
            dt.Columns.Add(header_ChildItemName, typeof(string));
            dt.Columns.Add(header_ChildItemString, typeof(string));
            dt.Columns.Add(header_ChildItemQty, typeof(decimal));
            //dt.Columns.Add(header_Using, typeof(bool));
            dt.Columns.Add(header_MainCarton, typeof(bool));
            dt.Columns.Add(header_ChildStockOut, typeof(bool));

            return dt;
        }

        private void dgvItemGroupEdit(DataGridView dgv)
        {
            //dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            //dgv.RowsDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

            //dgv.Columns[header_Time].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_Operator].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_MeterReading].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Hourly].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgv.Columns[header_Operator].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[header_PartQty_Min].Visible = false;
            dgv.Columns[header_PartQty_Max].Visible = false;
            dgv.Columns[header_ChildItemCode].Visible = false;
            dgv.Columns[header_ChildItemName].Visible = false;

            dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_PartQtyString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ChildItemQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ChildItemString].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns[header_ChildItemString].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            // dgv.Columns[header_MeterReading].DefaultCellStyle.BackColor = SystemColors.Info;
        }

        private void LoadItemGroup(bool CheckIfDataEmpty)
        {
            dgvItemGroup.DataSource = null;
            lblGroupTitle.Text = text_ItemGroupTitle + _ITEMCODE + " " + _ITEMNAME;

            DataTable dt_ItemGroup = dalJoin.Search(_ITEMCODE);

            DataTable dt = NewItemGroupTable();

            if(dt_ItemGroup != null)
            {
                int index = 1;

                foreach (DataRow row in dt_ItemGroup.Rows)
                {
                    DataRow dt_Row;

                    string parentCode = row["parent_code"].ToString();

                    if(parentCode == _ITEMCODE)
                    {
                        dt_Row = dt.NewRow();

                        string childCode = row["child_code"].ToString();
                        string childName = row["child_name"].ToString();

                        float joinQty = row["join_qty"] == DBNull.Value ? 0 : Convert.ToSingle(row["join_qty"].ToString());

                        float joinMax = row["join_max"] == DBNull.Value ? 0 : Convert.ToSingle(row["join_max"].ToString());
                        float joinMin = row["join_min"] == DBNull.Value ? 0 : Convert.ToSingle(row["join_min"].ToString());

                        bool mainCarton = bool.TryParse(row["join_main_carton"].ToString(), out bool test) ? test : false;
                        bool stockOut = bool.TryParse(row["join_stock_out"].ToString(), out  stockOut) ? stockOut : true;

                        dt_Row[header_Index] = index;
                        dt_Row[header_PartQty_Min] = joinMin;
                        dt_Row[header_PartQty_Max] = joinMax;

                        if(joinMax == joinMin)
                        {
                            dt_Row[header_PartQtyString] = joinMax;

                        }
                        else
                        {
                            dt_Row[header_PartQtyString] = joinMin + " ~ " + joinMax;

                        }

                        dt_Row[header_ChildItemCode] = childCode;
                        dt_Row[header_ChildItemName] = childName;
                        dt_Row[header_ChildItemString] = "[" + childCode + "]  " + childName;
                        dt_Row[header_ChildItemQty] = joinQty;
                        dt_Row[header_MainCarton] = mainCarton;
                        dt_Row[header_ChildStockOut] = stockOut;

                        dt.Rows.Add(dt_Row);
                    }
                    
                }
            }
           
            if(dt.Rows.Count > 0)
            {
                dgvItemGroup.DataSource = dt;

                dgvItemGroupEdit(dgvItemGroup);

                dgvItemGroup.ClearSelection();
            }
            else if(CheckIfDataEmpty)
            {
                MessageBox.Show("Item group not found!");
            }
        }


        private void frmItemGroupList_Load(object sender, EventArgs e)
        {
            LoadItemGroup(true);
        }

        private void dgvItemGroup_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvItemGroup;

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();
                int rowIndex = dgv.CurrentCell.RowIndex;

                try
                {
                    my_menu.Items.Add(text_EditItem).Name = text_EditItem;
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
            my_menu.Hide();
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvItemGroup;
            dgv.SuspendLayout();

            string itemClicked = e.ClickedItem.Name.ToString();
            int rowIndex = dgv.CurrentCell.RowIndex;

            string childName = dgv.Rows[rowIndex].Cells[header_ChildItemName].Value.ToString();

            if (itemClicked.Equals(text_EditItem))
            {
                uJoin.join_parent_code = _ITEMCODE;

                uJoin.join_child_code = dgv.Rows[rowIndex].Cells[header_ChildItemCode].Value.ToString();

                uJoin.join_qty = Convert.ToSingle(dgv.Rows[rowIndex].Cells[header_ChildItemQty].Value.ToString());
                uJoin.join_max = Convert.ToInt16(dgv.Rows[rowIndex].Cells[header_PartQty_Max].Value.ToString());
                uJoin.join_min = Convert.ToInt16(dgv.Rows[rowIndex].Cells[header_PartQty_Min].Value.ToString());

                uJoin.join_main_carton = bool.TryParse(dgv.Rows[rowIndex].Cells[header_MainCarton].Value.ToString(), out bool mainCarton) ? mainCarton : false;

                frmJoinEdit frm = new frmJoinEdit(uJoin, true);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();//Item Edit

                LoadItemGroup(true);
            }

            else if (itemClicked.Equals(text_RemoveItem))
            {

                if (MessageBox.Show("Are you sure you want to remove " + childName +" from this Item Group? ", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    uJoin.join_parent_code = _ITEMCODE;
                    uJoin.join_child_code = dgv.Rows[rowIndex].Cells[header_ChildItemCode].Value.ToString();

                    bool success = dalJoin.Delete(uJoin);

                    if (success == true)
                    {
                        //item deleted successfully
                        MessageBox.Show("Item deleted successfully");
                        LoadItemGroup(false);
                    }
                    else
                    {
                        //Failed to delete item
                        MessageBox.Show("Failed to delete item");
                    }

                }
            }
            Cursor = Cursors.Arrow; // change cursor to normal type
            dgv.ResumeLayout();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

            uJoin.join_parent_code = _ITEMCODE;

            uJoin.join_child_code = "";

            frmJoinEdit frm = new frmJoinEdit(uJoin, true);


            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit

            LoadItemGroup(true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
