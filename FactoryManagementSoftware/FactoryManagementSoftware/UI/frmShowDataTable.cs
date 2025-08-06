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

namespace FactoryManagementSoftware.UI
{
    public partial class frmShowDataTable : Form
    {
        public frmShowDataTable()
        {
            InitializeComponent();
        }

        public frmShowDataTable(DataTable dt)
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvList, true);
            LoadPartProductionInfo(dt);

        }
        readonly string header_Index = "#";
        readonly string header_BalAfter = "BAL. AFTER";
        readonly string header_Stock= "STOCK";
        readonly string header_ItemCode = "CODE";

        readonly string header_ToProduce = "TO PRODUCE";
        readonly string header_AutoAdjusted= "AUTO ADJUSTED";
        readonly string header_ProHoursPerDay= "PRO HOURS/DAY";
        readonly string header_Category= "CATEGORY";
        readonly string header_Type= "TYPE";
        readonly string header_Ton= "TON";
        readonly string header_ProDaysNeeded = "PRO DAY(24HRS) NEEDED";
        readonly string header_Cavity = "CAVITY";
        readonly string header_CT_SEC = "CT(s)";


        Tool tool = new Tool();
        itemDAL dalItem = new itemDAL();
        SBBDataBLL uSBB = new SBBDataBLL();

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgv.Columns[header_BalAfter].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[header_ToProduce].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_AutoAdjusted].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[header_Cavity].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_CT_SEC].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[header_ProDaysNeeded].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void frmShowDataTable_Load(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }

        private void LoadPartProductionInfo(DataTable itemList)
        {
            if(!itemList.Columns.Contains(header_ToProduce))
            {
                itemList.Columns.Add(header_ToProduce, typeof(int));
            }

            if (!itemList.Columns.Contains(header_AutoAdjusted))
            {
                itemList.Columns.Add(header_AutoAdjusted, typeof(int));
            }

            if (!itemList.Columns.Contains(header_Cavity))
            {
                itemList.Columns.Add(header_Cavity, typeof(int));
            }

            if (!itemList.Columns.Contains(header_CT_SEC))
            {
                itemList.Columns.Add(header_CT_SEC, typeof(int));
            }

            if (!itemList.Columns.Contains(header_ProDaysNeeded))
            {
                itemList.Columns.Add(header_ProDaysNeeded, typeof(float));
            }


            DataTable dt = dalItem.SBBPartSearch();

            if (itemList.Rows.Count > 0)
            {
                uSBB.Production_Hours_PerDay = 24;

                foreach (DataRow itemRow in itemList.Rows)
                {
                    string itemCode = itemRow[header_ItemCode].ToString(); 

                    if(!string.IsNullOrEmpty(itemCode))
                    {
                        uSBB.Item_Stock = int.TryParse(itemRow[header_Stock].ToString(), out int k) ? k : 0;

                        int balAfter = int.TryParse(itemRow[header_BalAfter].ToString(), out  k) ? k : 0;


                        if(balAfter < 0)
                        {
                            uSBB.Target_qty = balAfter * -1;
                        }
                        else
                        {
                            uSBB.Target_qty = 0;
                        }

                        uSBB.Production_TargetQty_Adjusted_BySystem = 0;

                        foreach (DataRow row in dt.Rows)
                        {
                            if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                            {
                                uSBB.Production_PW_shot = float.TryParse(row[dalItem.ItemProPWShot].ToString(), out float i) ? i : 0;
                                uSBB.Production_RW_shot = float.TryParse(row[dalItem.ItemProRWShot].ToString(), out  i) ? i : 0;

                                if (itemCode == "CF25B")
                                {
                                    var checkpoint = 1;
                                }
                                //148202.03125
                                string stockString = row[dalItem.ItemStock].ToString();
                                int itemStock = decimal.TryParse(row[dalItem.ItemStock].ToString(), out decimal stockValue) ? (int)stockValue : 0;

                                uSBB.Production_cavity = int.TryParse(row[dalItem.ItemCavity].ToString(), out  k) ? k : 0;
                                uSBB.Production_ct_sec = int.TryParse(row[dalItem.ItemProCTTo].ToString(), out k) ? k : 0;

                                if (uSBB.Production_ct_sec == 0)
                                {
                                    uSBB.Production_ct_sec = int.TryParse(row[dalItem.ItemQuoCT].ToString(), out k) ? k : 0;
                                }

                                itemRow[header_ToProduce] = uSBB.Target_qty;
                                itemRow[header_Cavity] = uSBB.Production_cavity;
                                itemRow[header_CT_SEC] = uSBB.Production_ct_sec;

                                float TotalProDaysNeeded = tool.GetProductionDayNeeded(uSBB);

                                itemRow[header_AutoAdjusted] = uSBB.Production_TargetQty_Adjusted_BySystem;
                                itemRow[header_ProDaysNeeded] = TotalProDaysNeeded;
                            }
                        }
                    }
                }

                dgvList.DataSource = itemList;
                DgvUIEdit(dgvList);
            }
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvList;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if(dgv.Columns[col].Name == header_BalAfter || dgv.Columns[col].Name == header_Stock)
            {
                int balanceAfter = int.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out balanceAfter) ? balanceAfter : 0;

                if(balanceAfter < 0)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
            }
        }
    }
}
