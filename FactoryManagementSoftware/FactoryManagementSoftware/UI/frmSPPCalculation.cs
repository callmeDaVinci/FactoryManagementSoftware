using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSPPCalculation : Form
    {
        public frmSPPCalculation()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvTargetItem, true);
            tool.DoubleBuffered(dgvMatPrepareList, true);

            dt_Source = NewSourceTable();
        }

        SPPDataDAL dalData = new SPPDataDAL();
        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        userDAL dalUser = new userDAL();
        joinDAL dalJoin = new joinDAL();

        DataTable dt_Source;
  
        private readonly string header_Size = "SIZE";
        private readonly string header_Unit = "UNIT";
        private readonly string header_Type = "TYPE";
        private readonly string header_Category = "CATEGORY";
        private readonly string header_Code = "CODE";
        private readonly string header_Stock = "STOCK";
        private readonly string header_RequiredQty = "REQUIRED QTY";
        private readonly string header_Target_Qty = "TARGET QTY";
        private readonly string header_Target_Pcs = "TARGET PCS";
        private readonly string header_Target_Bag = "TARGET BAG";

        private DataTable NewSourceTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_Target_Pcs, typeof(int));
            dt.Columns.Add(header_Target_Bag, typeof(int));

            return dt;
        }

        private DataTable NewTargetTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_Target_Qty, typeof(string));

            return dt;
        }

        private DataTable NewMaterialTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Size, typeof(int));
            dt.Columns.Add(header_Unit, typeof(string));
            dt.Columns.Add(header_Category, typeof(string));
            dt.Columns.Add(header_Type, typeof(string));
            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_Stock, typeof(int));
            dt.Columns.Add(header_RequiredQty, typeof(int));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[header_Size].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[header_Type].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            if (dgv == dgvTargetItem)
            {
                dgv.Columns[header_Code].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
               
                dgv.Columns[header_Code].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Target_Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else if(dgv == dgvMatPrepareList)
            {
                dgv.Columns[header_Stock].DefaultCellStyle.Font= new Font("Segoe UI", 8F, FontStyle.Italic);
                dgv.Columns[header_RequiredQty].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                dgv.Columns[header_Code].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[header_Code].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Category].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[header_Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[header_RequiredQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
            frmSPPAddItem frm = new frmSPPAddItem
            {
                StartPosition = FormStartPosition.CenterScreen
            };


            frm.ShowDialog();

            if(frmSPPAddItem.itemAdded)
            {
                //add data to datarow
                //add datarow to table

                DataRow dt_Row = dt_Source.NewRow();

                dt_Row[header_Size] = frmSPPAddItem.item_Size;
                dt_Row[header_Unit] = "MM";
                dt_Row[header_Type] = frmSPPAddItem.item_Type;
                dt_Row[header_Code] = frmSPPAddItem.item_Code;
                dt_Row[header_Target_Pcs] = frmSPPAddItem.item_Target_Pcs;
                dt_Row[header_Target_Bag] = frmSPPAddItem.item_Target_Bags;

                dt_Source.Rows.Add(dt_Row);

                //add data to target list
                ShowTargetList();
                //calculate material qty
                ShowMaterialList();
                frmSPPAddItem.itemAdded = false;
            }
        }

        private void ShowTargetList()
        {
            dt_Source.DefaultView.Sort = header_Size+" ASC, " + header_Type + " ASC, " + header_Code + " ASC";
            dt_Source = dt_Source.DefaultView.ToTable();

            string lastItem = null;

            for (int i = 0; i < dt_Source.Rows.Count; i++)
            {
                string currentItem = dt_Source.Rows[i][header_Code].ToString();

                if(lastItem == null)
                {
                    lastItem = currentItem;
                }
                else if(lastItem == currentItem && i != 0)
                {
                    int lastTargetPcs = int.TryParse(dt_Source.Rows[i - 1][header_Target_Pcs].ToString(), out lastTargetPcs) ? lastTargetPcs : 0;
                    int lastTargetBag = int.TryParse(dt_Source.Rows[i - 1][header_Target_Bag].ToString(), out lastTargetBag) ? lastTargetBag : 0;

                    int currentTargetPcs = int.TryParse(dt_Source.Rows[i][header_Target_Pcs].ToString(), out currentTargetPcs) ? currentTargetPcs : 0;
                    int currentTargetBag = int.TryParse(dt_Source.Rows[i][header_Target_Bag].ToString(), out currentTargetBag) ? currentTargetBag : 0;

                    dt_Source.Rows[i - 1][header_Target_Pcs] = lastTargetPcs + currentTargetPcs;
                    dt_Source.Rows[i - 1][header_Target_Bag] = lastTargetBag + currentTargetBag;

                    dt_Source.Rows.RemoveAt(i);
                    i -= 1;
                }
                else
                {
                    lastItem = currentItem;
                }
            }

            DataTable dt_Target = NewTargetTable();
            DataRow dt_Row;

            foreach (DataRow row in dt_Source.Rows)
            {
                dt_Row = dt_Target.NewRow();

                dt_Row[header_Size] = row[header_Size].ToString();
                dt_Row[header_Unit] = row[header_Unit].ToString();
                dt_Row[header_Type] = row[header_Type].ToString();
                dt_Row[header_Code] = row[header_Code].ToString();
                dt_Row[header_Target_Qty] = row[header_Target_Pcs].ToString() + " (" + row[header_Target_Bag].ToString()+" BAGS)";

                dt_Target.Rows.Add(dt_Row);
            }

            dgvTargetItem.DataSource = dt_Target;
            DgvUIEdit(dgvTargetItem);
            dgvTargetItem.ClearSelection();
        }

        private DataRow GetItemInfo(string itemCode, DataTable dt)
        {

            foreach(DataRow row in dt.Rows)
            {
                if(row["CODE"].ToString() == itemCode)
                {
                    return row;
                }
            }

            return null;
        }

        private void ShowMaterialList()
        {
            //loop dt source to get parent code,get target pcs
            string parentItemCode = null;
            string childItemCode;

            DataTable dt_Mat = NewMaterialTable();
            DataTable dt_NonReadyGoods = dalItem.SPPNonReadyGoodsSelect();

            foreach (DataRow row in dt_Source.Rows)
            {
                parentItemCode = row[header_Code].ToString();
                int qty = Convert.ToInt32(row[header_Target_Pcs].ToString());

                DataTable dtSPP = dalJoin.loadChildList(parentItemCode);

                foreach (DataRow SPP in dtSPP.Rows)
                {
                    parentItemCode = SPP["join_child_code"].ToString();
                }

                if (parentItemCode != null)
                {
                    DataTable dtJoin = dalJoin.loadChildList(parentItemCode);

                    if (dtJoin.Rows.Count > 0)
                    {
                        foreach (DataRow Join in dtJoin.Rows)
                        {
                            float childQty = qty;

                            float joinQty = float.TryParse(Join["join_qty"].ToString(), out float i) ? Convert.ToSingle(Join["join_qty"].ToString()) : 1;

                            childQty = childQty * joinQty;

                            childItemCode = Join["join_child_code"].ToString();

                            DataRow nonReadyGoods_row = GetItemInfo(childItemCode, dt_NonReadyGoods);

                            if (nonReadyGoods_row != null)
                            {
                                DataRow dt_Row = dt_Mat.NewRow();

                                dt_Row[header_Size] = nonReadyGoods_row["SIZE"].ToString();
                                dt_Row[header_Unit] = "MM";
                                dt_Row[header_Category] = nonReadyGoods_row["CATEGORY"].ToString();
                                dt_Row[header_Type] = nonReadyGoods_row["TYPE"].ToString();
                                dt_Row[header_Code] = nonReadyGoods_row["CODE"].ToString();
                                dt_Row[header_Stock] = nonReadyGoods_row["QUANTITY"].ToString();
                                dt_Row[header_RequiredQty] = childQty;

                                dt_Mat.Rows.Add(dt_Row);
                            }
                        }
                    }
                }
            }

            dt_Mat.DefaultView.Sort =  header_Size + " ASC, " + header_Category + " ASC, " + header_Type + " ASC, " + header_Code + " ASC";
            dt_Mat = dt_Mat.DefaultView.ToTable();

            string lastItem = null;

            for (int i = 0; i < dt_Mat.Rows.Count; i++)
            {
                string currentItem = dt_Mat.Rows[i][header_Code].ToString();

                if (lastItem == null)
                {
                    lastItem = currentItem;
                }
                else if (lastItem == currentItem && i != 0)
                {
                    int lastRequiredQty = int.TryParse(dt_Mat.Rows[i - 1][header_RequiredQty].ToString(), out lastRequiredQty) ? lastRequiredQty : 0;

                    int currentRequiredQty = int.TryParse(dt_Mat.Rows[i][header_RequiredQty].ToString(), out currentRequiredQty) ? currentRequiredQty : 0;

                    dt_Mat.Rows[i - 1][header_RequiredQty] = lastRequiredQty + currentRequiredQty;

                    dt_Mat.Rows.RemoveAt(i);
                    i -= 1;
                }
                else
                {
                    lastItem = currentItem;
                }
            }

            dgvMatPrepareList.DataSource = dt_Mat;
            DgvUIEdit(dgvMatPrepareList);
            dgvMatPrepareList.ClearSelection();

        }

        private void dgvTargetItem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            DataGridView dgv = dgvTargetItem;

            dgv.SuspendLayout();
            int n = e.RowIndex;

            if (dgv.Columns[e.ColumnIndex].Name == "SIZE")
            {
                //dgv.Rows[n].Height = 50;

            }



            dgv.ResumeLayout();
        }

        private void dgvTargetItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEdit.Visible = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //get item info
            DataTable dt = (DataTable)dgvTargetItem.DataSource;
            int rowIndex = dgvTargetItem.CurrentRow.Index;

            //string code, string type, string size, string unit

            string code = dt.Rows[rowIndex][header_Code].ToString();
            string type = dt.Rows[rowIndex][header_Type].ToString();
            string size = dt.Rows[rowIndex][header_Size].ToString();
            string unit = dt.Rows[rowIndex][header_Unit].ToString();
            string targetPcs = dt_Source.Rows[rowIndex][header_Target_Pcs].ToString();
            if (rowIndex >= 0)
            {
                frmSPPAddItem frm = new frmSPPAddItem(code, type, size, unit, targetPcs)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };



                frm.ShowDialog();

                if (frmSPPAddItem.itemEdited)
                {
                    btnEdit.Visible = false;
                    //add data to datarow
                    //add datarow to table
                    dt_Source.Rows.RemoveAt(rowIndex);
                    DataRow dt_Row = dt_Source.NewRow();

                    dt_Row[header_Size] = frmSPPAddItem.item_Size;
                    dt_Row[header_Unit] = "MM";
                    dt_Row[header_Type] = frmSPPAddItem.item_Type;
                    dt_Row[header_Code] = frmSPPAddItem.item_Code;
                    dt_Row[header_Target_Pcs] = frmSPPAddItem.item_Target_Pcs;
                    dt_Row[header_Target_Bag] = frmSPPAddItem.item_Target_Bags;

                    dt_Source.Rows.Add(dt_Row);

                    //add data to target list
                    ShowTargetList();
                    //calculate material qty
                    ShowMaterialList();
                    frmSPPAddItem.itemEdited = false;
                }
                else if(frmSPPAddItem.itemRemoved)
                {
                    btnEdit.Visible = false;
                    dt_Source.Rows.RemoveAt(rowIndex);
                    dgvTargetItem.DataSource = dt_Source;

                   

                    //add data to target list
                    ShowTargetList();
                    //calculate material qty
                    ShowMaterialList();
                    frmSPPAddItem.itemRemoved = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a row!");
            }

          
        }

        private void dgvTargetItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //get item info
            DataTable dt = (DataTable)dgvTargetItem.DataSource;
            int rowIndex = dgvTargetItem.CurrentRow.Index;

            //string code, string type, string size, string unit

            string code = dt.Rows[rowIndex][header_Code].ToString();
            string type = dt.Rows[rowIndex][header_Type].ToString();
            string size = dt.Rows[rowIndex][header_Size].ToString();
            string unit = dt.Rows[rowIndex][header_Unit].ToString();
            string targetPcs = dt_Source.Rows[rowIndex][header_Target_Pcs].ToString();
            if (rowIndex >= 0)
            {
                frmSPPAddItem frm = new frmSPPAddItem(code, type, size, unit, targetPcs)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };



                frm.ShowDialog();

                if (frmSPPAddItem.itemEdited)
                {
                    btnEdit.Visible = false;
                    //add data to datarow
                    //add datarow to table
                    dt_Source.Rows.RemoveAt(rowIndex);
                    DataRow dt_Row = dt_Source.NewRow();

                    dt_Row[header_Size] = frmSPPAddItem.item_Size;
                    dt_Row[header_Unit] = "MM";
                    dt_Row[header_Type] = frmSPPAddItem.item_Type;
                    dt_Row[header_Code] = frmSPPAddItem.item_Code;
                    dt_Row[header_Target_Pcs] = frmSPPAddItem.item_Target_Pcs;
                    dt_Row[header_Target_Bag] = frmSPPAddItem.item_Target_Bags;

                    dt_Source.Rows.Add(dt_Row);

                    //add data to target list
                    ShowTargetList();
                    //calculate material qty
                    ShowMaterialList();
                    frmSPPAddItem.itemEdited = false;
                }
                else if (frmSPPAddItem.itemRemoved)
                {
                    btnEdit.Visible = false;
                    dt_Source.Rows.RemoveAt(rowIndex);
                    dgvTargetItem.DataSource = dt_Source;



                    //add data to target list
                    ShowTargetList();
                    //calculate material qty
                    ShowMaterialList();
                    frmSPPAddItem.itemRemoved = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a row!");
            }
        }
    }
}
