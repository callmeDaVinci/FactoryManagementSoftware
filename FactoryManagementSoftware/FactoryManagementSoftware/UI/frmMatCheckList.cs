using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMatCheckList : Form
    {
        public frmMatCheckList()
        {
            InitializeComponent();
        }

        #region variable/object declare

        facStockDAL dalStock = new facStockDAL();
        matPlanDAL dalMatPlan = new matPlanDAL();
        matPlanBLL uMatPlan = new matPlanBLL();
        planningDAL dalPlan = new planningDAL();
        itemDAL dalItem = new itemDAL();
        MacDAL dalMac = new MacDAL();
        Tool tool = new Tool();
        Text text = new Text();
        userDAL dalUser = new userDAL();

        readonly string headerIndex = "#";
        readonly string headerPlanID = "PLAN ID";
        readonly string headerType = "TYPE";
        readonly string headerMatCode = "MATERIAL";
        readonly string headerItem = "PLAN FOR";

        readonly string headerCollectBag = "COLLECT BAG";
        readonly string headerDeliverBag = "DELIVER BAG";
        readonly string headerFrom = "FROM";
        readonly string headerTo = "TO";
        readonly string headerReceivedBy = "RECEIVED BY";
        readonly string headerCheck = "CHECK";
        readonly string headerQty = "QTY (KG/PIECE)";

        readonly string headerTotalCollect = "TOTAL COLLECT (KG/PIECE)";

        private bool switchToDeliverPage = false;
        private bool itemChecking = false;

        static public string MatTrfDate;
        #endregion

        #region UI Setting

        private DataTable NewTransferTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerIndex, typeof(int));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerTotalCollect, typeof(float));
            dt.Columns.Add(headerCollectBag, typeof(string));
            dt.Columns.Add(headerFrom, typeof(string));
            dt.Columns.Add(headerTo, typeof(string));
            dt.Columns.Add(headerQty, typeof(float));
            dt.Columns.Add(headerDeliverBag, typeof(string));

            return dt;
        }

        private DataTable NewDeliverTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(headerIndex, typeof(int));
            dt.Columns.Add(headerFrom, typeof(string));
            dt.Columns.Add(headerTo, typeof(string));
            dt.Columns.Add(headerType, typeof(string));
            dt.Columns.Add(headerMatCode, typeof(string));
            dt.Columns.Add(headerPlanID, typeof(int));
            dt.Columns.Add(headerItem, typeof(string));
            dt.Columns.Add(headerQty, typeof(float));
            dt.Columns.Add(headerDeliverBag, typeof(string));
            dt.Columns.Add(headerReceivedBy, typeof(string));
            dt.Columns.Add(headerCheck, typeof(bool));

            return dt;
        }

        private void dgvTransferUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerType].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMatCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerTotalCollect].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerFrom].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTo].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[headerCollectBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerDeliverBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTotalCollect].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerFrom].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerType].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[headerTotalCollect].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.Columns[headerQty].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
        }

        private void dgvDeliverUIEdit(DataGridView dgv)
        {
            dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerFrom].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerTo].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerType].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerMatCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerPlanID].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerItem].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[headerQty].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerReceivedBy].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[headerCheck].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgv.Columns[headerDeliverBag].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerPlanID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerFrom].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerTo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[headerType].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dgv.Columns[headerMatCode].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.Columns[headerQty].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            dgv.Columns[headerCheck].Visible = false;
            dgv.Columns[headerReceivedBy].Visible = false;
            dgv.Columns[headerCheck].DefaultCellStyle.BackColor = SystemColors.Info;
        }

        private void dgvTransfer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvTransfer;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerMatCode)
            {
                string matCode = dgv.Rows[row].Cells[headerMatCode].Value.ToString();

                if (string.IsNullOrEmpty(matCode))
                {
                    dgv.Rows[row].Height = 2;
                    //dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.DimGray;
                }
                else
                {
                    dgv.Rows[row].Height = 60;
                }
            }
        }
        #endregion

        #region Load Data

        private void LoadTransferData()
        {
            #region pre setup
            dgvTransfer.DataSource = null;
            
            DataTable dt_SK = NewTransferTable();
            DataRow row_SK;

            int index = 1;
            DataTable dt = dalMatPlan.Select();

            DataView dv = dt.DefaultView;
            dv.Sort = dalItem.ItemCat + " asc, " + "mat_code asc, " + dalMac.MacLocation + " asc, " + dalPlan.productionStartDate + " asc";
            dt = dv.ToTable();

            string preMatCode = null, matCode = null, preFrom = null, from, preTo = null, to, cat = null;

            float fromTotal = 0, toTotal = 0, prepareQty = 0;

            bool active = false;
            bool unprintData = false;
            #endregion

            #region data proccessing

            foreach (DataRow row in dt.Rows)
            {
                active = Convert.ToBoolean(row[dalMatPlan.Active]);
                
                from = row[dalMatPlan.MatFrom].ToString();
                to = row[dalMac.MacLocation].ToString();
                prepareQty = float.TryParse(row[dalMatPlan.Prepare].ToString(), out float i) ? Convert.ToSingle(row[dalMatPlan.Prepare].ToString()) : 0;

                if (active && !string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to) && prepareQty > 0)
                {
                    cat = row[dalItem.ItemCat].ToString();
                    matCode = row[dalMatPlan.MatCode].ToString();
                    unprintData = true;

                    if (preMatCode == null)
                    {
                        preMatCode = matCode;
                    }

                    if (preFrom == null)
                    {
                        preFrom = from;
                    }

                    if (preTo == null)
                    {
                        preTo = to;
                    }


                    if(preMatCode == matCode)
                    {
                        if(preFrom == from)
                        {
                            fromTotal += prepareQty;

                            if(preTo == to)
                            {
                                toTotal += prepareQty;
                            }
                            else
                            {
                                row_SK = dt_SK.NewRow();
                                row_SK[headerIndex] = index;
                                row_SK[headerType] = cat;
                                row_SK[headerMatCode] = matCode;
                                row_SK[headerTotalCollect] = DBNull.Value;
                                row_SK[headerFrom] = from;
                                row_SK[headerTo] = preTo;
                                row_SK[headerQty] = toTotal;

                                if(cat == text.Cat_RawMat || cat == text.Cat_Pigment || cat == text.Cat_MB)
                                {
                                    string deliverBag = (int)toTotal / 25 + " BAG";

                                    if (toTotal % 25 != 0)
                                    {
                                        deliverBag += " + " + toTotal % 25 + "KG";
                                    }
                                    row_SK[headerDeliverBag] = deliverBag;
                                }
                               

                                dt_SK.Rows.Add(row_SK);
                                index++;

                                preTo = to;
                                toTotal = prepareQty;
                                
                            }
                        }
                        else
                        {
                            row_SK = dt_SK.NewRow();
                            row_SK[headerIndex] = index;
                            row_SK[headerType] = cat;
                            row_SK[headerMatCode] = matCode;
                            row_SK[headerTotalCollect] = fromTotal;
                            row_SK[headerFrom] = preFrom;
                            row_SK[headerTo] = preTo;
                            row_SK[headerQty] = toTotal;

                            if (cat == text.Cat_RawMat || cat == text.Cat_Pigment || cat == text.Cat_MB)
                            {
                                string collectBag = (int)fromTotal / 25 + " BAG";
                                string deliverBag = (int)toTotal / 25 + " BAG";

                                if (fromTotal % 25 != 0)
                                {
                                    collectBag += " + " + fromTotal % 25 + "KG";
                                }

                                if (toTotal % 25 != 0)
                                {
                                    deliverBag += " + " + toTotal % 25 + "KG";
                                }

                                row_SK[headerCollectBag] = collectBag;
                                row_SK[headerDeliverBag] = deliverBag;
                            }

                            dt_SK.Rows.Add(row_SK);
                            index++;

                            preFrom = from;
                            preTo = to;
                            fromTotal = prepareQty;
                            toTotal = prepareQty;
                            
                        }
                    }
                    else
                    {
                       

                        row_SK = dt_SK.NewRow();
                        row_SK[headerIndex] = index;
                        row_SK[headerType] = cat;
                        row_SK[headerMatCode] = preMatCode;
                        row_SK[headerTotalCollect] = fromTotal;
                        row_SK[headerFrom] = preFrom;
                        row_SK[headerTo] = preTo;
                        row_SK[headerQty] = toTotal;

                        if (cat == text.Cat_RawMat || cat == text.Cat_Pigment || cat == text.Cat_MB)
                        {
                            string collectBag = (int)fromTotal / 25 + " BAG";
                            string deliverBag = (int)toTotal / 25 + " BAG";

                            if (fromTotal % 25 != 0)
                            {
                                collectBag += " + " + fromTotal % 25 + "KG";
                            }

                            if (toTotal % 25 != 0)
                            {
                                deliverBag += " + " + toTotal % 25 + "KG";
                            }

                            row_SK[headerCollectBag] = collectBag;
                            row_SK[headerDeliverBag] = deliverBag;
                        }

                        dt_SK.Rows.Add(row_SK);
                        index++;

                        preMatCode = matCode;
                        preFrom = from;
                        preTo = to;
                        fromTotal = prepareQty;
                        toTotal = prepareQty;

                        row_SK = dt_SK.NewRow();
                        dt_SK.Rows.Add(row_SK);
                    }
                   

                }
            }

            #endregion

            if (unprintData)
            {
                row_SK = dt_SK.NewRow();
                row_SK[headerIndex] = index;
                row_SK[headerType] = cat;
                row_SK[headerMatCode] = preMatCode;
                row_SK[headerTotalCollect] = fromTotal;
                row_SK[headerFrom] = preFrom;
                row_SK[headerTo] = preTo;
                row_SK[headerQty] = toTotal;

                if (cat == text.Cat_RawMat || cat == text.Cat_Pigment || cat == text.Cat_MB)
                {
                    string collectBag = (int)fromTotal / 25 + " BAG";
                    string deliverBag = (int)toTotal / 25 + " BAG";

                    if (fromTotal % 25 != 0)
                    {
                        collectBag += " + " + fromTotal % 25 + "KG";
                    }

                    if (toTotal % 25 != 0)
                    {
                        deliverBag += " + " + toTotal % 25 + "KG";
                    }

                    row_SK[headerCollectBag] = collectBag;
                    row_SK[headerDeliverBag] = deliverBag;
                }

                dt_SK.Rows.Add(row_SK);
                index++;
            }

            #region set dgv data source
            if (dt_SK.Rows.Count > 0)
            {
               

                dgvTransfer.DataSource = dt_SK;

                dgvTransferUIEdit(dgvTransfer);

                dgvTransfer.ClearSelection();
            }
            else
            {
                MessageBox.Show("No data found!");
            }
            #endregion
        }

        private void LoadDeliverData()
        {
            #region pre setup
            dgvDeliver.DataSource = null;
            


            DataTable dt = NewDeliverTable();
            DataRow dt_row;

            float total = 0;
            string preMatCode = null;
            string matCode = null;
            
            string facName = null;
            string from = null;
            string preFrom = null;
            string preFacName = null;
            string fromLocation = null;
            float preparingQty = 0;

            int index = 1;
            DataTable dt_MatPlan = dalMatPlan.Select();
            DataTable dt_ItemInfo = dalItem.Select();
            DataView dv = dt_MatPlan.DefaultView;
            dv.Sort = dalMac.MacLocation + " asc, " + dalItem.ItemCat + " asc, " + "mat_code asc";
            dt_MatPlan = dv.ToTable();
            #endregion

            #region data proccessing

            foreach (DataRow row in dt_MatPlan.Rows)
            {
                bool active = Convert.ToBoolean(row[dalMatPlan.Active]);
                from = row[dalMatPlan.MatFrom].ToString();
                string partCode = row[dalPlan.partCode].ToString();
                preparingQty = float.TryParse(row[dalMatPlan.Prepare].ToString(), out float i) ? Convert.ToSingle(row[dalMatPlan.Prepare].ToString()) : 0;
                
                if (active && !string.IsNullOrEmpty(from) && preparingQty > 0)
                {
                    string cat = row[dalItem.ItemCat].ToString();
                    facName = row[dalMac.MacLocation].ToString();
                    matCode = row[dalMatPlan.MatCode].ToString();

                    if(preFacName == null)
                    {
                        preFacName = facName;
                    }
                    else if(preFacName == facName)
                    {

                    }
                    else
                    {
                        dt_row = dt.NewRow();
                        dt.Rows.Add(dt_row);
                        preFacName = facName;
                    }

                    dt_row = dt.NewRow();
                    dt_row[headerIndex] = index;
                    dt_row[headerType] = cat;
                    dt_row[headerMatCode] = matCode;
                    dt_row[headerPlanID] = row[dalMatPlan.PlanID];
                    dt_row[headerItem] = tool.getItemNameFromDataTable(dt_ItemInfo, partCode) + "(" + partCode + ")";
                    dt_row[headerFrom] = from;
                    dt_row[headerTo] = facName;
                    dt_row[headerQty] = preparingQty;

                   
                    if (cat == text.Cat_RawMat || cat == text.Cat_Pigment || cat == text.Cat_MB)
                    {
                       
                        string deliverBag = (int)preparingQty / 25 + " BAG";

                       
                        if (preparingQty % 25 != 0)
                        {
                            deliverBag += " + " + preparingQty % 25 + "KG";
                        }

                        dt_row[headerDeliverBag] = deliverBag;
                    }

                    dt.Rows.Add(dt_row);
                    index++;
                }
            }

            #endregion

            #region set dgv data source
            if (dt.Rows.Count > 0)
            {
                dgvDeliver.DataSource = dt;

                dgvDeliverUIEdit(dgvDeliver);

                dgvDeliver.ClearSelection();
            }
            else
            {
                MessageBox.Show("No data found!");
            }
            #endregion
        }

        private void frmMatCheckList_Load(object sender, EventArgs e)
        {
            SwitchPage();
        }

        #endregion

        #region button action

        private void SwitchPage()
        {
            if(switchToDeliverPage)
            {
                btnItemCheck.Visible = true;
               
                btnTrfChecklist.ForeColor = Color.Black;
                btnDeliverChecklist.ForeColor = Color.FromArgb(52, 139, 209);

                tlpMatChecklist.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 0f);
                tlpMatChecklist.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100f);

                dgvDeliver.ResumeLayout();
                LoadDeliverData();

            }
            else
            {
                if(dgvDeliver.DataSource != null)
                dgvDeliver.Columns[headerCheck].Visible = false;

                btnItemCheck.Visible = false;

                itemChecking = false;

                btnItemCheck.Text = "ITEM CHECK";
                btnAutoInOut.Visible = false;
                
                cbCheckAll.Checked = false;
                cbCheckAll.Visible = false;
                dtpTrfDate.Visible = false;
                lblTrfDate.Visible = false;

                btnDeliverChecklist.ForeColor = Color.Black;
                btnTrfChecklist.ForeColor = Color.FromArgb(52, 139, 209);

                tlpMatChecklist.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);
                tlpMatChecklist.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 0f);

                dgvTransfer.ResumeLayout();
                LoadTransferData();
            }
            
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            switchToDeliverPage = false;
            SwitchPage();
            
        }

        private void btnDeliver_Click(object sender, EventArgs e)
        {
            switchToDeliverPage = true;
            SwitchPage();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Close();
        }



        #endregion

        private void dgvDeliver_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvDeliver;

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (dgv.Columns[col].Name == headerMatCode)
            {
                string matCode = dgv.Rows[row].Cells[headerMatCode].Value.ToString();

                if (string.IsNullOrEmpty(matCode))
                {
                    dgv.Rows[row].Height = 1;
                    //dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.DimGray;
                }
                else
                {
                    dgv.Rows[row].Height = 60;
                }
            }
        }

        private void btnItemCheck_Click(object sender, EventArgs e)
        {
            if(itemChecking)
            {
                itemChecking = false;

                btnItemCheck.Text = "ITEM CHECK";
                btnAutoInOut.Visible = false;
                dgvDeliver.Columns[headerCheck].Visible = false;
                cbCheckAll.Checked = false;
                cbCheckAll.Visible = false;
                dtpTrfDate.Visible = false;
                lblTrfDate.Visible = false;
            }
            else
            {
                itemChecking = true;
                btnItemCheck.Text = "CANCEL";
                btnAutoInOut.Visible = true;
                dgvDeliver.Columns[headerCheck].Visible = true;
                cbCheckAll.Visible = true;
                dtpTrfDate.Visible = true;
                lblTrfDate.Visible = true;
            }

            

        }

        private void dgvDeliver_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(itemChecking)
            {
                DataGridView dgv = dgvDeliver;
                int rowIndex = e.RowIndex;
                int colIndex = e.ColumnIndex;

                bool check = dgv.Rows[rowIndex].Cells[headerCheck].Value == DBNull.Value ? false : Convert.ToBoolean(dgv.Rows[rowIndex].Cells[headerCheck].Value);
                string matCode = dgv.Rows[rowIndex].Cells[headerMatCode].Value.ToString();

                if (!string.IsNullOrEmpty(matCode))
                {
                    if (check)
                    {
                        dgv.Rows[rowIndex].Cells[headerCheck].Value = false;

                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[headerCheck].Value = true;

                    }
                }
            }
           
            

        }

        private void checkedAll()
        {
            DataGridView dgv = dgvDeliver;

            foreach(DataGridViewRow row in dgv.Rows)
            {
                string matCode = row.Cells[headerMatCode].Value.ToString();

                if (!string.IsNullOrEmpty(matCode))
                {
                    row.Cells[headerCheck].Value = true;
                }
            }

            
        }

        private void UnCheckAll()
        {
            DataGridView dgv = dgvDeliver;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                string matCode = row.Cells[headerMatCode].Value.ToString();

                if (!string.IsNullOrEmpty(matCode))
                {
                    row.Cells[headerCheck].Value = false;
                }
            }


        }

        private void cbCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if(cbCheckAll.Checked)
            {
                checkedAll();
            }
            else
            {
                UnCheckAll();
            }
        }

        private void btnAutoInOut_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Confirm to process In/Out action?", "Message",
                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                DataTable dt = NewDeliverTable();
                //DataRow dt_row;

                DataTable dt_deliver = (DataTable)dgvDeliver.DataSource;
                foreach (DataRow row in dt_deliver.Rows)
                {
                    string matCode = row[headerMatCode].ToString();
                    bool itemChecked = row[headerCheck] == DBNull.Value ? false : Convert.ToBoolean(row[headerCheck].ToString());

                    if (!string.IsNullOrEmpty(matCode) && itemChecked)
                    {
                        dt.ImportRow(row);
                    }
                }

                MatTrfDate = dtpTrfDate.Text;
                frmInOutEdit frm = new frmInOutEdit(dt);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();//Item Edit
                DataTable dt_MatPlan = dalMatPlan.Select();

                if (frmInOutEdit.matTrfSuccess)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        string from = row[headerFrom].ToString();
                        float preparingQty = float.TryParse(row[headerQty].ToString(), out float i) ? Convert.ToSingle(row[headerQty].ToString()) : 0;
                        string to = row[headerTo].ToString();
                        string matCode = row[headerMatCode].ToString();

                        foreach(DataRow mat in dt_MatPlan.Rows)
                        {
                            bool active = Convert.ToBoolean(mat[dalMatPlan.Active]);

                            if(active)
                            {
                                float checkingQty = float.TryParse(mat[dalMatPlan.Prepare].ToString(), out float j) ? Convert.ToSingle(mat[dalMatPlan.Prepare].ToString()) : 0;

                                bool match = true;

                                if (matCode != mat[dalMatPlan.MatCode].ToString())
                                {
                                    match = false;
                                }

                                else if (from != mat[dalMatPlan.MatFrom].ToString())
                                {
                                    match = false;
                                }

                                else if (to != mat[dalMac.MacLocation].ToString())
                                {
                                    match = false;
                                }

                                else if (preparingQty != checkingQty)
                                {
                                    match = false;
                                }

                                if(match)
                                {
                                    //get plan id
                                    int PlanID = int.TryParse(mat[dalMatPlan.PlanID].ToString(), out int k) ? Convert.ToInt32(mat[dalMatPlan.PlanID]) : 0;
                                    float Transferred = float.TryParse(mat[dalMatPlan.Transferred].ToString(), out float l) ? Convert.ToSingle(mat[dalMatPlan.Transferred].ToString()) : 0;

                                    uMatPlan.mat_code = matCode;
                                    uMatPlan.plan_id = PlanID;
                                    uMatPlan.mat_transferred = preparingQty + Transferred;
                                    uMatPlan.mat_preparing = 0;
                                    uMatPlan.mat_from = "";
                                    uMatPlan.updated_date = DateTime.Now;
                                    uMatPlan.updated_by = MainDashboard.USER_ID;

                                    if (!dalMatPlan.MatPrepareUpdate(uMatPlan))
                                    {
                                        MessageBox.Show("Failed to update material preparing data.");
                                    }
                                }
                            }
                           

                        }
                    }

                    itemChecking = false;

                    btnItemCheck.Text = "ITEM CHECK";
                    btnAutoInOut.Visible = false;
                    dgvDeliver.Columns[headerCheck].Visible = false;
                    cbCheckAll.Checked = false;
                    cbCheckAll.Visible = false;
                    dtpTrfDate.Visible = false;

                    LoadDeliverData();
                }
            }

               
        }

        #region Excel

        private string setFileName()
        {
            string fileName = "Test.xls";

            DateTime currentDate = DateTime.Now;
            if (!switchToDeliverPage)
            {
                fileName = "TransferChecklist_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            }
            else
            {
                fileName = "DeliveryChecklist" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";
            }
            return fileName;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                SaveFileDialog sfd = new SaveFileDialog();
                DataGridView dgv;
                string path = @"D:\StockAssistant\Document\Checklist";
                Directory.CreateDirectory(path);
                sfd.InitialDirectory = path;

                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = setFileName();

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);

                    if (switchToDeliverPage)
                    {
                        dgv = dgvDeliver;
                    }
                    else
                    {
                        dgv = dgvTransfer;
                    }

                    dgv.Focus();

                    // Copy DataGridView results to clipboard
                    copyAlltoClipboard(dgv);
                    object misValue = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application();
                    xlexcel.PrintCommunication = false;
                    xlexcel.ScreenUpdating = false;
                    xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    xlexcel.Calculation = XlCalculation.xlCalculationManual;
                    Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    if (switchToDeliverPage)
                    {
                        xlWorkSheet.Name = "DELIVERY CHECKLIST";
                        xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri\"&16 DELIVERY CHECKLIST";
                        xlWorkSheet.PageSetup.LeftFooter = "&\"Calibri\"&11 " + "DELIVERED BY\n                DATE";
                        xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri\"&11 " + "CHECKED BY\n            DATE";
                    }
                    else
                    {
                        xlWorkSheet.Name = "TRANSFER CHECKLIST";
                        xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri\"&16 TRANSFER CHECKLIST";
                        xlWorkSheet.PageSetup.LeftFooter = "&\"Calibri\"&11 " + "APPROVED BY\n                DATE";
                        //xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri\"&11 " + "CHECKED\n  DATE";
                    }



                    #region Save data to Sheet

                    //Header and Footer setup
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri\"&8 (" + dalUser.getUsername(MainDashboard.USER_ID) + ") " + DateTime.Now;

                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri\"&8 PG -&P";
                    //xlWorkSheet.PageSetup.CenterFooter = "&\"Calibri\"&8 Printed By " + dalUser.getUsername(MainDashboard.USER_ID) + " at " + DateTime.Now;


                    
                    //Page setup
                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;


                    xlWorkSheet.PageSetup.Zoom = false;
                    xlWorkSheet.PageSetup.CenterHorizontally = true;

                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;

                    double pointToCMRate = 0.035;
                    xlWorkSheet.PageSetup.TopMargin = 1.8 / pointToCMRate;
                    xlWorkSheet.PageSetup.BottomMargin = 4.0 / pointToCMRate;
                    xlWorkSheet.PageSetup.HeaderMargin = 1.0 / pointToCMRate;
                    xlWorkSheet.PageSetup.FooterMargin = 2.0 / pointToCMRate;
                    xlWorkSheet.PageSetup.LeftMargin = 1 / pointToCMRate;
                    xlWorkSheet.PageSetup.RightMargin = 1 / pointToCMRate;

                    xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

                    xlexcel.PrintCommunication = true;
                    xlexcel.Calculation = XlCalculation.xlCalculationAutomatic;
                    // Paste clipboard results to worksheet range
                    xlWorkSheet.Select();
                    Range CR = (Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                    //content edit
                    Range tRange = xlWorkSheet.UsedRange;
                    //tRange.Borders.LineStyle = XlLineStyle.;
                    //tRange.Borders.Weight = XlBorderWeight.xlThin;
                    tRange.Font.Size = 12;
                    tRange.Font.Name = "Calibri";
                    tRange.EntireColumn.AutoFit();
                    tRange.EntireRow.AutoFit();
                    tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);

                    int iTotalColumns = xlWorkSheet.UsedRange.Columns.Count;
                    int iTotalRows = xlWorkSheet.UsedRange.Rows.Count;

                    char letter = 'a';

                    for (int i = 0; i < iTotalColumns; i++)
                    {
                        Range column = xlWorkSheet.get_Range(letter+"1:"+ letter + iTotalRows).Cells;
                        column.BorderAround(Type.Missing, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Type.Missing);
                        letter++;
                    }

                    #endregion

                    if (true)//cmbSubType.Text.Equals("PMMA")
                    {
                        for (int i = 0; i <= dgv.RowCount - 1; i++)
                        {
                            for (int j = 0; j <= dgv.ColumnCount - 1; j++)
                            {
                                Range range = (Range)xlWorkSheet.Cells[i + 2, j + 1];
                               
                                //range.Rows.RowHeight = 30;
                                range.Font.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[j].InheritedStyle.ForeColor);

                                if(switchToDeliverPage)
                                {
                                    if(j == 9)
                                    {
                                        range.Cells.Value = "";
                                    }

                                    if (j == 6)
                                    {
                                        //range.Cells.Value = "";
                                        range.Columns.ColumnWidth = 20;
                                    }

                                    if (j == 0 || j == 1 || j == 2 || j == 3 || j == 5 || j == 7)
                                    {
                                        range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                        range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                    }
                                    else
                                    {

                                        range.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                        range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                    }
                                }
                                else
                                {
                                    if (j == 2)
                                    {
                                        range.Columns.ColumnWidth = 20;
                                    }

                                    if (j == 0 || j == 3 || j == 4 || j == 5 || j == 6)
                                    {
                                        range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                        range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                    }
                                    else
                                    {

                                        range.Cells.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                                        range.Cells.VerticalAlignment = XlVAlign.xlVAlignCenter;
                                    }
                                }
                                

                                if (dgv.Rows[i].Cells[j].InheritedStyle.BackColor == Color.White)
                                {
                                    range.Rows.RowHeight = 40;
                                }
                                else if (dgv.Rows[i].Cells[j].InheritedStyle.BackColor == Color.DimGray )
                                {
                                    range.Rows.RowHeight = 2;
                                    range.Interior.Color = Color.DimGray;
                                }
                                else
                                {
                                    if(j != 9)
                                    {
                                        range.Interior.Color = ColorTranslator.ToOle(dgv.Rows[i].Cells[j].InheritedStyle.BackColor);

                                    }
                                    else
                                    {
                                        //range.Interior.Color = Color.White;
                                    }
                                    range.Rows.RowHeight = 40;
                                }
                            }
                        }
                    }
                    //Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookNormal,
                        misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlexcel.DisplayAlerts = true;

                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dgv.ClearSelection();

                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                        System.Diagnostics.Process.Start(sfd.FileName);
                }

                Cursor = Cursors.Arrow; // change cursor to normal type

            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }

            finally
            {
                if (switchToDeliverPage && !itemChecking)
                {
                    dgvDeliver.Columns[headerCheck].Visible = false;
                    dgvDeliver.Columns[headerReceivedBy].Visible = false;
                }
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            

        }

        private void copyAlltoClipboard()
        {
            DataObject dataObj;
            DataGridView dgv;

            if (switchToDeliverPage)
            {
                dgv = dgvDeliver;
                dgv.Columns[headerCheck].Visible = true;
                dgv.Columns[headerReceivedBy].Visible = true;
            }
            else
            {
                dgv = dgvTransfer;
            }

            dgv.SelectAll();
            dataObj = dgv.GetClipboardContent();

            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void copyAlltoClipboard(DataGridView dgv)
        {
            if(switchToDeliverPage)
            {
                dgv.Columns[headerCheck].Visible = true;
                dgv.Columns[headerReceivedBy].Visible = true;
            }

            DataObject dataObj;
            dgv.SelectAll();
            dataObj = dgv.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion
    }
}
