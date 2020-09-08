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
    public partial class frmSBBMatPLan : Form
    {
        public frmSBBMatPLan()
        {
            InitializeComponent();

            tool.DoubleBuffered(dgvItemList, true);
            tool.DoubleBuffered(dgvDeliveryList, true);
        }

        #region Object/ Variable Declare

        itemDAL dalItem = new itemDAL();
        itemCustDAL dalItemCust = new itemCustDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        facDAL dalFac = new facDAL();
        pmmaDateDAL dalPMMADate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        SBBDataDAL dalSPP = new SBBDataDAL();
        SBBDataBLL uSpp = new SBBDataBLL();
        historyDAL dalHistory = new historyDAL();
        historyBLL uHistory = new historyBLL();
        planningDAL dalPlan = new planningDAL();
        facStockDAL dalStock = new facStockDAL();
        joinDAL dalJoin = new joinDAL();

        Tool tool = new Tool();
        Text text = new Text();

        private bool Loaded = false;
        private bool isAssemblyMode = true;

        private DataTable dt_ItemList;
        private DataTable dt_DeliveryList;

        
        #endregion

        #region Header Name

        private readonly string header_MatTblCode = "MAT TBL CODE";
        private readonly string header_Index = "#";
        private readonly string header_Select = "SELECT";

        //ITEM
        private readonly string header_ItemCode = "ITEM CODE";
        private readonly string header_ItemName = "ITEM NAME";
        private readonly string header_ItemSize = "SIZE";
        private readonly string header_ItemUnit = "UNIT";
        private readonly string header_ItemType = "TYPE";
        private readonly string header_ItemString = "ITEM";

        //PLAN ITEM
        private readonly string header_PlanID = "PLAN ID";
        private readonly string header_PlanItemCode = "PLAN ITEM CODE";
        private readonly string header_PlanItemName = "PLAN ITEM NAME";
        private readonly string header_PlanItemSize = "PLAN SIZE";
        private readonly string header_PlanItemUnit = "PLAN UNIT";
        private readonly string header_PlanItemType = "PLAN TYPE";
        private readonly string header_PlanItemString = "PLAN";

        //LOCATION
        private readonly string header_Area = "AREA";
        private readonly string header_Line = "LINE";
        private readonly string header_Location = "LOCATION";
        private readonly string header_From = "FROM";
        private readonly string header_To = "TO";

        //DATE
        private readonly string header_DateStart = "DATE START";
        private readonly string header_DateEnd = "DATE END";
        private readonly string header_Date = "DATE";

        private readonly string header_RequiredQty = "REQUIRED QTY";
        private readonly string header_DeliveredQty = "DELIVERED QTY";
        private readonly string header_Preparing = "PREPARING";

        private readonly string header_DeliveryBag = "DELIVERY (BAG)";
        private readonly string header_StdPacking = "STD PACKING";
        private readonly string header_DeliveryPcs = "DELIVERY (PCS)";

        private readonly string header_DeliveredDate = "DELIVERED";
        private readonly string header_TrfTblID = "TRF ID";

        private readonly string header_PreTblCode = "PRE TBL CODE";

        #endregion

        #region Text Name

        private readonly string text_NewDelivery = "new delivery";
        private readonly string text_Complete = "complete";
        private readonly string text_UndoDelivered = "undo delivered";
        private readonly string text_CancelDelivery = "cancel delivery";

        #endregion

        private DataTable NewItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_MatTblCode, typeof(int));

            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_ItemString, typeof(string));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));
            //dt.Columns.Add(header_ItemSize, typeof(string));
            //dt.Columns.Add(header_ItemUnit, typeof(string));
            //dt.Columns.Add(header_ItemType, typeof(string));

            dt.Columns.Add(header_PlanID, typeof(int));
            //dt.Columns.Add(header_PlanItemCode, typeof(string));
            //dt.Columns.Add(header_PlanItemName, typeof(string));
            //dt.Columns.Add(header_PlanItemSize, typeof(string));
            //dt.Columns.Add(header_PlanItemUnit, typeof(string));
            dt.Columns.Add(header_PlanItemType, typeof(string));
            dt.Columns.Add(header_PlanItemString, typeof(string));

            dt.Columns.Add(header_Location, typeof(string));

            //dt.Columns.Add(header_DateStart, typeof(DateTime));
            //dt.Columns.Add(header_DateEnd, typeof(DateTime));
            dt.Columns.Add(header_Date, typeof(DateTime));

            dt.Columns.Add(header_RequiredQty, typeof(int));
            dt.Columns.Add(header_DeliveredQty, typeof(int));
            dt.Columns.Add(header_Preparing, typeof(int));

            return dt;
        }

        private DataTable NewDeliveryTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_MatTblCode, typeof(int));

            dt.Columns.Add(header_PreTblCode, typeof(int));

            dt.Columns.Add(header_Index, typeof(int));

            dt.Columns.Add(header_DeliveryBag, typeof(int));
            dt.Columns.Add(header_StdPacking, typeof(int));
            dt.Columns.Add(header_DeliveryPcs, typeof(int));
            dt.Columns.Add(header_From, typeof(string));
            dt.Columns.Add(header_DeliveredDate, typeof(DateTime));

            return dt;
        }

        private DataTable NewDeliveredTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_DeliveredDate, typeof(DateTime));
            dt.Columns.Add(header_ItemCode, typeof(string));
            dt.Columns.Add(header_ItemName, typeof(string));
            dt.Columns.Add(header_From, typeof(string));
            dt.Columns.Add(header_To, typeof(string));
            dt.Columns.Add(header_DeliveryPcs, typeof(int));

            dt.Columns.Add(header_PlanID, typeof(string));
            dt.Columns.Add(header_PlanItemType, typeof(string));

            dt.Columns.Add(header_PreTblCode, typeof(string));
            dt.Columns.Add(header_MatTblCode, typeof(string));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            if(dgv == dgvItemList)
            {
                dgv.Columns[header_PlanItemString].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[header_PlanID].Visible = false;
                dgv.Columns[header_MatTblCode].Visible = false;
                dgv.Columns[header_ItemCode].Visible = false;
                dgv.Columns[header_ItemName].Visible = false;
                dgv.Columns[header_PlanItemType].Visible = false;

                for (int i = 0; i < dgv.RowCount; i ++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        dgv.Rows[i ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                        dgv.Rows[i].Cells[header_PlanItemString].Style.Alignment = DataGridViewContentAlignment.TopLeft;
                        dgv.Rows[i].Cells[header_Location].Style.Alignment = DataGridViewContentAlignment.TopLeft;
                        //dgv.Rows[i].Cells[header_Date].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgv.Columns[header_Date].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                        //dgv.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
                        //dgv.Rows[i - 2].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);
                        //dgv.Rows[i - 2].Cells[header_Type].InheritedStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
                        //dgv.Rows[i - 2].Cells[header_StockString].InheritedStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                    }
                    else
                    {
                        dgv.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
                        dgv.Rows[i].Cells[header_PlanItemString].Style.Alignment = DataGridViewContentAlignment.BottomLeft;
                        dgv.Rows[i].Cells[header_Location].Style.Alignment = DataGridViewContentAlignment.BottomLeft;

                        dgv.Rows[i].Cells[header_PlanItemString].Style.ForeColor = Color.FromArgb(64, 64, 64);
                        dgv.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                    }
                }
            }
            else if(dgv == dgvDeliveryList)
            {
                dgv.Columns[header_From].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv.Columns[header_MatTblCode].Visible = false;
                dgv.Columns[header_PreTblCode].Visible = false;
                dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[header_DeliveryBag].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                dgv.Columns[header_DeliveryPcs].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                dgv.Columns[header_StdPacking].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[header_DeliveredDate].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[header_StdPacking].DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            }

            dgv.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);

        }

        private void LoadItemList()
        {
            Loaded = false;
            dt_ItemList = NewItemTable();
            dt_DeliveryList = NewDeliveryTable();

            //temp test
            #region Load Item Data
            //1st data
            DataRow row = dt_ItemList.NewRow();

            row[header_PlanItemString] = " PLAN 2696";
            row[header_Location] = "STORE";
            row[header_Date] = DateTime.Now.Date;
            dt_ItemList.Rows.Add(row);

            row = dt_ItemList.NewRow();


            row[header_MatTblCode] = 212;
            row[header_Index] = 1;
            row[header_ItemString] = " 32MM BUSH ";
            row[header_ItemCode] = "CF32B";
            row[header_ItemName] = "32MM BUSH";

            row[header_PlanID] = 2696;
            row[header_PlanItemType] = "EQUAL SOCKET";
            row[header_PlanItemString] = " 32 EQUAL SOCKET ";
            row[header_Location] = "LINE 1";
            row[header_ItemString] = " 32MM BUSH ";
            row[header_Date] = DateTime.Now.Date;
            row[header_RequiredQty] = 2800;
            row[header_DeliveredQty] = 600;
            row[header_Preparing] = 2400;
            dt_ItemList.Rows.Add(row);

            //2nd data
            row = dt_ItemList.NewRow();

            row[header_PlanItemString] = " PLAN 2697 ";
            row[header_Location] = "STORE";
            row[header_Date] = DateTime.Now.Date;
            dt_ItemList.Rows.Add(row);

            row = dt_ItemList.NewRow();
            row[header_MatTblCode] = 213;
            row[header_Index] = 2;
            row[header_ItemString] = " 32MM BUSH ";
            row[header_ItemCode] = "CF32B";
            row[header_ItemName] = "32MM BUSH";

            row[header_PlanID] = 2697;
            row[header_PlanItemType] = "EQUAL ELBOW";
            row[header_PlanItemString] = " 32 EQUAL ELBOW ";
            row[header_Location] = "LINE 1";
            row[header_ItemString] = " 32MM BUSH ";
            row[header_Date] = DateTime.Now.Date;
            row[header_RequiredQty] = 2800;
            row[header_DeliveredQty] = 600;
            row[header_Preparing] = 2400;
            dt_ItemList.Rows.Add(row);
            #endregion

            #region Load Delivery Data

            DataRow row2 = dt_DeliveryList.NewRow();

            row2[header_PreTblCode] = 1;
            row2[header_Index] = 1;
            row2[header_MatTblCode] = 212;
            row2[header_DeliveryBag] = 1;
            row2[header_StdPacking] = 600;
            row2[header_DeliveryPcs] = 600;
            row2[header_From] = "No.11";
            row2[header_DeliveredDate] = DateTime.Now.Date;

            dt_DeliveryList.Rows.Add(row2);


            row2 = dt_DeliveryList.NewRow();

            row2[header_PreTblCode] = 2;
            row2[header_Index] = 2;
            row2[header_MatTblCode] = 213;
            row2[header_DeliveryBag] = 2;
            row2[header_StdPacking] = 600;
            row2[header_DeliveryPcs] = 1200;
            row2[header_From] = "No.11";
            
            dt_DeliveryList.Rows.Add(row2);


            row2 = dt_DeliveryList.NewRow();

            row2[header_PreTblCode] = 3;
            row2[header_Index] = 3;
            row2[header_MatTblCode] = 212;
            row2[header_DeliveryBag] = 1;
            row2[header_StdPacking] = 300;
            row2[header_DeliveryPcs] = 300;
            row2[header_From] = "No.11";

            dt_DeliveryList.Rows.Add(row2);
            #endregion

            dgvItemList.DataSource = dt_ItemList;
            DgvUIEdit(dgvItemList);
            dgvItemList.ClearSelection();

            dgvDeliveryList.DataSource = null;

            Loaded = true;

        }

        private void LoadDeliveryList()
        {
            //get rowIndex
            //check if item list selected
            //get mat table code
            //load data
            
            DataGridView dgv = dgvItemList;
            int rowIndex = dgv.CurrentCell.RowIndex;
            DataTable dt = dt_DeliveryList.Clone();

            if (dgv.SelectedRows.Count > 0 && rowIndex != -1 && dgv.Rows.Count > 0)
            {
                int matTblCode = 0;

                if((rowIndex + 1) % 2 == 0)
                {
                    matTblCode = int.TryParse(dgv.Rows[rowIndex].Cells[header_MatTblCode].Value.ToString(), out matTblCode) ? matTblCode : 0;
                }
                else
                {
                    matTblCode = int.TryParse(dgv.Rows[rowIndex + 1].Cells[header_MatTblCode].Value.ToString(), out matTblCode) ? matTblCode : 0;
                }


                foreach(DataRow row in dt_DeliveryList.Rows)
                {
                    int _matTblCode = int.TryParse(row[header_MatTblCode].ToString(), out _matTblCode) ? _matTblCode : 0;

                    if(matTblCode == _matTblCode)
                    {

                        dt.ImportRow(row);


                    }
                }
            }

            dt.AcceptChanges();

            int index = 1;
            foreach(DataRow row in dt.Rows)
            {
                row[header_Index] = index++;
            }

            dgvDeliveryList.DataSource = dt;
            DgvUIEdit(dgvDeliveryList);
            dgvDeliveryList.ClearSelection();
        }

        private void frmSBBMatPLan_Load(object sender, EventArgs e)
        {
            LoadItemList();
            Loaded = true;
            
        }

        private void btnAssembly_Click(object sender, EventArgs e)
        {
            btnAssembly.BackColor = Color.FromArgb(52, 160, 225);
            btnProduction.BackColor = Color.White;

            isAssemblyMode = true;

            LoadItemList();
        }

        private void btnProduction_Click(object sender, EventArgs e)
        {
            btnProduction.BackColor = Color.FromArgb(52, 160, 225);
            btnAssembly.BackColor = Color.White;

            isAssemblyMode = false;

            LoadItemList();
        }

        private void dgvItemList_SelectionChanged(object sender, EventArgs e)
        {
            if (Loaded)
            {
                DataGridView dgv = (DataGridView)sender;
                bool rowSelected = dgv.SelectedRows.Count > 0 ? true : false;
                int rowIndex = dgv.CurrentCell.RowIndex;
                int colIndex = dgv.CurrentCell.ColumnIndex;

                if (rowSelected && rowIndex != -1)
                {
                    btnAdd.Visible = true;
                    btnEdit.Visible = false;
                    LoadDeliveryList();

                    if (dgv.Rows[rowIndex].Cells[colIndex] is DataGridViewCheckBoxCell)
                    {
                        bool selected = bool.TryParse(dgv.Rows[rowIndex].Cells[colIndex].Value.ToString(), out selected) ? selected : false;

                        dgv.Rows[rowIndex].Cells[colIndex].Value = !selected;

                        int matTblCode = int.TryParse(dgv.Rows[rowIndex].Cells[header_MatTblCode].Value.ToString(), out matTblCode) ? matTblCode : 0;

                        foreach (DataRow row in dt_DeliveryList.Rows)
                        {
                            int _matTblCode = int.TryParse(row[header_MatTblCode].ToString(), out _matTblCode) ? _matTblCode : -1;

                            if (matTblCode == _matTblCode)
                            {
                                row[header_Select] = !selected;
                            }
                        }

                        LoadDeliveryList();

                        bool selectedCheck = true;
                        foreach (DataRow row in dt_DeliveryList.Rows)
                        {
                            selectedCheck = bool.TryParse(row[header_Select].ToString(), out selectedCheck) ? selectedCheck : false;

                            if (!selectedCheck)
                            {
                                break;
                            }
                        }

                        Loaded = false;
                        cbSelectAll.Checked = selectedCheck;
                        Loaded = true;

                    }
                }
                else
                {
                    btnAdd.Visible = false;

                }
            }
        }

     

        private void ColumnSelectAction()
        {
            //Loaded = false;
            bool selected = cbSelectAll.Checked;

            if(!dt_ItemList.Columns.Contains(header_Select))
            {
                
                dt_ItemList.Columns.Add(header_Select, typeof(string));

                
            }

            int index = 1;
            foreach (DataRow row in dt_ItemList.Rows)
            {
                if (index % 2 == 0)
                {
                    DataGridViewCheckBoxCell CheckBoxCell = new DataGridViewCheckBoxCell();

                    CheckBoxCell.Style.Alignment = DataGridViewContentAlignment.TopCenter;

                    dgvItemList.Rows[index - 1].Cells[header_Select] = CheckBoxCell;

                    dgvItemList.Rows[index - 1].Cells[header_Select].Value = selected;
                    row[header_Select] = selected;
                }
                   
                else
                {
                    DataGridViewTextBoxCell TextBoxCell = new DataGridViewTextBoxCell();
                    dgvItemList.Rows[index - 1].Cells[header_Select] = TextBoxCell;
                    //dgvItemList.Rows[index - 1].Cells[header_Select].Value = DBNull.Value;
                }

                index++;
            }

            if (!dt_DeliveryList.Columns.Contains(header_Select))
            {
                dt_DeliveryList.Columns.Add(header_Select, typeof(bool));
            }

            foreach (DataRow row in dt_DeliveryList.Rows)
            {
                row[header_Select] = selected;
            }


            DataTable dt = (DataTable)dgvDeliveryList.DataSource;

            if(dt != null)
            {
                if (!dt.Columns.Contains(header_Select))
                {
                    dt.Columns.Add(header_Select, typeof(bool));
                }

                foreach (DataRow row in dt.Rows)
                {
                    row[header_Select] = selected;
                }

            }

            DgvUIEdit(dgvItemList);

            Loaded = false;
            dgvItemList.ClearSelection();

            dgvDeliveryList.DataSource = null;

            Loaded = true;
        }

        private void ColumnSelectActionFromCheckBox()
        {
            //Loaded = false;
            bool selected = cbSelectAll.Checked;

            if (!dt_ItemList.Columns.Contains(header_Select))
            {

                dt_ItemList.Columns.Add(header_Select, typeof(string));


            }

            int index = 1;
            foreach (DataRow row in dt_ItemList.Rows)
            {
                if (index % 2 == 0)
                {
                    DataGridViewCheckBoxCell CheckBoxCell = new DataGridViewCheckBoxCell();

                    CheckBoxCell.Style.Alignment = DataGridViewContentAlignment.TopCenter;

                    dgvItemList.Rows[index - 1].Cells[header_Select] = CheckBoxCell;

                    dgvItemList.Rows[index - 1].Cells[header_Select].Value = selected;
                    row[header_Select] = selected;
                }

                else
                {
                    DataGridViewTextBoxCell TextBoxCell = new DataGridViewTextBoxCell();
                    dgvItemList.Rows[index - 1].Cells[header_Select] = TextBoxCell;
                    //dgvItemList.Rows[index - 1].Cells[header_Select].Value = DBNull.Value;
                }

                index++;
            }

            if (!dt_DeliveryList.Columns.Contains(header_Select))
            {
                dt_DeliveryList.Columns.Add(header_Select, typeof(bool));
            }

            foreach (DataRow row in dt_DeliveryList.Rows)
            {
                row[header_Select] = selected;
            }


            DataTable dt = (DataTable)dgvDeliveryList.DataSource;

            if (dt != null)
            {
                if (!dt.Columns.Contains(header_Select))
                {
                    dt.Columns.Add(header_Select, typeof(bool));
                }

                foreach (DataRow row in dt.Rows)
                {
                    row[header_Select] = selected;
                }

            }

            DgvUIEdit(dgvItemList);

            //Loaded = false;
            dgvDeliveryList.ClearSelection();

            //dgvDeliveryList.DataSource = null;

            //Loaded = true;
        }

        private void CancelSelectMode()
        {

            if (dt_ItemList.Columns.Contains(header_Select))
            {

                dt_ItemList.Columns.Remove(header_Select);


            }

            if (dt_DeliveryList.Columns.Contains(header_Select))
            {
                dt_DeliveryList.Columns.Remove(header_Select);
            }

            DataTable dt = (DataTable)dgvDeliveryList.DataSource;

            if (dt != null)
            {
                if (dt.Columns.Contains(header_Select))
                {
                    dt.Columns.Remove(header_Select);
                }

                

            }

            DgvUIEdit(dgvItemList);
            dgvItemList.ClearSelection();
            dgvDeliveryList.ClearSelection();
        }


        private DataTable GetSelectedData()
        {
            DataTable dt = NewDeliveredTable();

            if(dt_ItemList.Columns.Contains(header_Select))
            {
                int itemIndex = 0;
                foreach (DataRow row in dt_ItemList.Rows)
                {
                    bool itemSelected = bool.TryParse(row[header_Select].ToString(), out itemSelected) ? itemSelected : false ;

                    if(itemSelected && (itemIndex + 1) % 2 == 0)
                    {
                        string locationTo = dt_ItemList.Rows[itemIndex - 1][header_Location].ToString();
                        int itemMatTblCode = int.TryParse(row[header_MatTblCode].ToString(), out itemMatTblCode) ? itemMatTblCode : -1;
                        string itemCode = row[header_ItemCode].ToString().ToString();
                        string itemName = row[header_ItemName].ToString().ToString();
                        string planID = row[header_PlanID].ToString().ToString();
                        string planItemType = row[header_PlanItemType].ToString().ToString();

                        foreach (DataRow deliveryRow in dt_DeliveryList.Rows)
                        {
                            bool deliverySelected = bool.TryParse(deliveryRow[header_Select].ToString(), out deliverySelected) ? deliverySelected : false;
                            int deliveryMatTblCode = int.TryParse(deliveryRow[header_MatTblCode].ToString(), out deliveryMatTblCode) ? deliveryMatTblCode :0;
                            int delivertPreTblCode = int.TryParse(deliveryRow[header_PreTblCode].ToString(), out delivertPreTblCode) ? delivertPreTblCode : 0;

                            if (deliverySelected && deliveryMatTblCode == itemMatTblCode)
                            {
                                int deliveryPcs = int.TryParse(deliveryRow[header_DeliveryPcs].ToString(), out deliveryPcs) ? deliveryPcs : 0;
                                string locationFrom = deliveryRow[header_From].ToString().ToString();

                                DataRow newRow = dt.NewRow();

                                newRow[header_ItemCode] = itemCode;
                                newRow[header_ItemName] = itemName;
                                newRow[header_From] = locationFrom;
                                newRow[header_To] = locationTo; 
                                 newRow[header_DeliveryPcs] = deliveryPcs;

                                newRow[header_PlanID] = planID;
                                newRow[header_PlanItemType] = planItemType;

                                newRow[header_MatTblCode] = deliveryMatTblCode;
                                newRow[header_PreTblCode] = delivertPreTblCode;

                                dt.Rows.Add(newRow);

                            }
                        }

                    }

                    itemIndex++;

                }
            }
          
            return dt;
        }

        private void btnDelivered_Click(object sender, EventArgs e)
        {
            if(btnDelivered.Text =="CONFIRM")
            {
                //get data
                DataTable dt = GetSelectedData();

                //check if data exist
                if (dt.Rows.Count > 0)
                {
                    frmDeliveryDate frm = new frmDeliveryDate();
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();

                    if (frmDeliveryDate.selectedDate != DateTimePicker.MinimumDateTime && frmDeliveryDate.selectedDate != DateTime.MaxValue)
                    {

                        foreach(DataRow row in dt.Rows)
                        {
                            row[header_DeliveredDate] = frmDeliveryDate.selectedDate.Date;
                        }

                        //process to In/Out

                        frmInOutEdit frm2 = new frmInOutEdit(dt,0);
                        frm2.StartPosition = FormStartPosition.CenterScreen;
                        frm2.ShowDialog();

                      
                    }
                    else
                    {
                        MessageBox.Show("Date invalid!");
                    }
                }

              
                
            }
            else
            {
                btnDelivered.BackColor = Color.FromArgb(0, 184, 148);
                btnDelivered.Text = "CONFIRM";
                cbSelectAll.Visible = true;
                cbSelectAll.Checked = true;
                btnCancel.Visible = true;
                btnAdd.Visible = false;

                Loaded = false;
                dgvItemList.ClearSelection();
                dgvDeliveryList.DataSource = null;
               

                ColumnSelectAction();

                Loaded = true;
            }

        }

        private void dgvItemList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                DataGridView dgv = (DataGridView)sender;

                //handle the row selection on right click
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    ContextMenuStrip my_menu = new ContextMenuStrip();


                    dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    // Can leave these here - doesn't hurt
                    dgv.Rows[e.RowIndex].Selected = true;
                    dgv.Focus();
                    int rowIndex = dgv.CurrentCell.RowIndex;

                    if(dgv == dgvItemList)
                    {
                        my_menu.Items.Add(text_NewDelivery).Name = text_NewDelivery;
                        my_menu.Items.Add(text_Complete).Name = text_Complete;

                    }
                    else if(dgv == dgvDeliveryList)
                    {
                        btnEdit.Visible = true;
                        my_menu.Items.Add(text_CancelDelivery).Name = text_CancelDelivery;

                        if(DateTime.TryParse(dgv.Rows[e.RowIndex].Cells[header_DeliveredDate].Value.ToString(), out DateTime delivered))
                        {
                            my_menu.Items.Add(text_UndoDelivered).Name = text_UndoDelivered;
                        }
                  
                    }

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(ItemList_ItemClicked);

                }
                else
                {
                    //btnEdit.Visible = false;
                }
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void ItemList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DataGridView dgv = dgvItemList;

                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex >= 0)
                {
                    //string ClickedItem = e.ClickedItem.Name.ToString();
                    //if (ClickedItem.Equals(text_CompleteDO))
                    //{
                    //    CompleteDO(rowIndex);
                    //}
                    //else if (ClickedItem.Equals(text_InCompleteDO))
                    //{
                    //    IncompleteDO(rowIndex);
                    //    //MessageBox.Show("incomplete do");
                    //}
                    //else if (ClickedItem.Equals(text_UndoRemove))
                    //{
                    //    DOUndoRemove(rowIndex);
                    //    //MessageBox.Show("undo remove");
                    //}
                    //else if (ClickedItem.Equals(text_ChangeDONumber))
                    //{
                    //    ChangeDONumber(rowIndex);
                    //    //MessageBox.Show("Change D/O Number");
                    //}
                }



                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void frmSBBMatPLan_Shown(object sender, EventArgs e)
        {
            Loaded = false;
            dgvItemList.ClearSelection();
            dgvDeliveryList.DataSource = null;
            Loaded = true;
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if(Loaded)
                ColumnSelectActionFromCheckBox();
        }

        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Loaded)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    btnAdd.Visible = true;
                    btnEdit.Visible = false;
                    //LoadDeliveryList();


                    DataGridView dgv = (DataGridView)sender;
                    if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
                    {
                        bool selected = bool.TryParse(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out selected) ? selected : false;

                        dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !selected;

                        int matTblCode = int.TryParse(dgv.Rows[e.RowIndex].Cells[header_MatTblCode].Value.ToString(), out matTblCode) ? matTblCode : 0;

                        foreach (DataRow row in dt_DeliveryList.Rows)
                        {
                            int _matTblCode = int.TryParse(row[header_MatTblCode].ToString(), out _matTblCode) ? _matTblCode : -1;

                            if (matTblCode == _matTblCode)
                            {
                                row[header_Select] = !selected;
                            }
                        }

                        LoadDeliveryList();

                        bool selectedCheck = true;
                        foreach (DataRow row in dt_DeliveryList.Rows)
                        {
                            selectedCheck = bool.TryParse(row[header_Select].ToString(), out selectedCheck) ? selectedCheck : false;

                            if (!selectedCheck)
                            {
                                break;
                            }
                        }

                        Loaded = false;
                        cbSelectAll.Checked = selectedCheck;
                        Loaded = true;

                    }
                }
                else
                {
                    btnAdd.Visible = false;

                }
            }

        }

        private void dgvItemList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //ControlPaint.DrawCheckBox(e.Graphics, 11, 11, 22, 22, ButtonState.Checked);
            //ControlPaint.DrawCheckBox(e.Graphics, 11, 44, 33, 33, ButtonState.Checked | ButtonState.Flat);
            //ControlPaint.DrawCheckBox(e.Graphics, 11, 88, 44, 44, ButtonState.Checked | ButtonState.Flat | ButtonState.Inactive);
            //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //{
            //    DataGridView dgv = (DataGridView)sender;

            //    if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            //    {
            //        e.PaintBackground(e.CellBounds, true);
            //        ControlPaint.DrawCheckBox(e.Graphics, e.CellBounds.X + 10, e.CellBounds.Y + 10,
            //   e.CellBounds.Width - 20, e.CellBounds.Height - 20,
            //   (bool)e.FormattedValue ? ButtonState.Checked : ButtonState.Flat);
            //        e.Handled = true;
            //    }
            //}
        }

        private void dgvDeliveryList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                btnEdit.Visible = true;

                DataGridView dgv = (DataGridView)sender;

                if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
                {
                    bool selected = bool.TryParse(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out selected) ? selected : false;

                    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !selected;

                    bool selectedCheck = false;

                    //check if any rows selected
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if(bool.TryParse(row.Cells[header_Select].Value.ToString(), out bool check) ? check : false)
                        {
                            selectedCheck = true;
                            break;
                        }
                    }

                    int matTblCode = int.TryParse(dgv.Rows[e.RowIndex].Cells[header_MatTblCode].Value.ToString(), out matTblCode) ? matTblCode : 0;

                    foreach (DataRow row in dt_ItemList.Rows)
                    {
                        int _matTblCode = int.TryParse(row[header_MatTblCode].ToString(), out _matTblCode) ? _matTblCode : -1;

                        if (matTblCode == _matTblCode)
                        {
                            row[header_Select] = selectedCheck;
                        }
                    }

                    //update dt_Delivery
                    int masterIndex = int.TryParse(dgv.Rows[e.RowIndex].Cells[header_PreTblCode].Value.ToString(), out masterIndex) ? masterIndex : 0;

                   

                    foreach (DataRow row in dt_DeliveryList.Rows)
                    {
                        int _masterIndex = int.TryParse(row[header_PreTblCode].ToString(), out _masterIndex) ? _masterIndex : -1;

                        if(masterIndex == _masterIndex)
                        {
                            row[header_Select] = !selected;
                        }

                        if(!bool.TryParse(row[header_Select].ToString(), out bool check)? check : false)
                        {
                            selectedCheck = false;
                        }
                    }

                    selectedCheck = true;
                    foreach (DataRow row in dt_DeliveryList.Rows)
                    {
                        selectedCheck = bool.TryParse(row[header_Select].ToString(), out selectedCheck) ? selectedCheck : false;

                        if (!selectedCheck)
                        {
                            break;
                        }
                    }

                    Loaded = false;
                    cbSelectAll.Checked = selectedCheck;
                    Loaded = true;
                }
            }
            else
            {
                if(dgvDeliveryList.SelectedRows.Count <= 0)
                btnEdit.Visible = false;

            }
        }

        private void dgvItemList_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnDelivered.Text == "CONFIRM")
            {
                btnDelivered.BackColor = Color.FromArgb(253, 203, 110);
                btnDelivered.Text = "DELIVERED";
                cbSelectAll.Visible = false;
                btnCancel.Visible = false;
                btnAdd.Visible = false;
                LoadItemList();
                dgvDeliveryList.DataSource = null;

                //CancelSelectMode();
                
            }
        }
    }
}
