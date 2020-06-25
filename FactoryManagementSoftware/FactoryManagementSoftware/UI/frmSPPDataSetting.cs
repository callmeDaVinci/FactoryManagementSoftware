using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using System.Threading.Tasks;
using System.Linq;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSPPDataSetting : Form
    {
        public frmSPPDataSetting()
        {
            InitializeComponent();
            InitializeData();
        }

        SPPDataDAL dalData = new SPPDataDAL();
        SPPDataBLL uData = new SPPDataBLL();

        itemDAL dalItem = new itemDAL();

        Tool tool = new Tool();
        Text text = new Text();

        private readonly string text_Add = "ADD";
        private readonly string text_Edit = "UPDATE";

        private readonly string text_ItemDataList = "SPP ITEM";
        private readonly string text_StdPackingList = "STANDARD PACKING LIST";
        private readonly string text_SizeDataList = "SIZE";
        private readonly string text_TypeDataList = "TYPE";
        private readonly string text_CategoryDataList = "CATEGORY";

        private bool loaded = false;
        private bool DataEdit = false;
        private void InitializeData()
        {
            //load size data:20mm,25mm...
            DataTable dt_DataList = new DataTable();
            dt_DataList.Columns.Add("ID");
            dt_DataList.Columns.Add("DATA LIST");

            dt_DataList.Rows.Add("1", text_ItemDataList);
            dt_DataList.Rows.Add("2",text_SizeDataList);
            dt_DataList.Rows.Add("3",text_TypeDataList);
            dt_DataList.Rows.Add("4",text_CategoryDataList);
            dt_DataList.Rows.Add("5", text_StdPackingList);

            cmbDataList.DataSource = dt_DataList;
            cmbDataList.DisplayMember = "DATA LIST";
            cmbDataList.SelectedIndex = -1;

            DataTable dt_Unit = new DataTable();
            dt_Unit.Columns.Add("UNIT");
            dt_Unit.Rows.Add(text.Unit_Millimetre);
            dt_Unit.Rows.Add(text.Unit_Inch);

            cmbUnit.DataSource = dt_Unit;
            cmbUnit.DisplayMember = "UNIT";

        }

        private void frmSPPDataSetting_Load(object sender, EventArgs e)
        {
            ShowOrHideDataEdit(false);
            loaded = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowOrHideDataEdit(bool showDataEdit)
        {
            dgvData.SuspendLayout();

            string text = btnAddData.Text;

            if (showDataEdit)
            {
                tlpDataSetting.RowStyles[1] = new RowStyle(SizeType.Absolute, 158f);

                dgvData.ResumeLayout();

            }
            else
            {
                tlpDataSetting.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgvData.ResumeLayout();
            }

        }

        private void ChangeEditPanel()
        {
            dgvData.SuspendLayout();

            string selectedDataList = cmbDataList.Text;

            if (selectedDataList == text_SizeDataList)
            {
                tlpDataEdit.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 243f);
                tlpDataEdit.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpDataEdit.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpDataEdit.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0f);
            }
            else if (selectedDataList == text_TypeDataList)
            {
                tlpDataEdit.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpDataEdit.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 339f);
                tlpDataEdit.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpDataEdit.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0f);

                cbIsCommon.Visible = true;
            }
            else if (selectedDataList == text_CategoryDataList)
            {
                tlpDataEdit.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpDataEdit.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 339f);
                tlpDataEdit.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpDataEdit.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 0f);

                cbIsCommon.Visible = false;
            }
            else if (selectedDataList == text_StdPackingList)
            {
                tlpDataEdit.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpDataEdit.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpDataEdit.ColumnStyles[3] = new ColumnStyle(SizeType.Absolute, 0f);
                tlpDataEdit.ColumnStyles[4] = new ColumnStyle(SizeType.Absolute, 442f);

                tool.loadSPPItemNameDataToComboBox(cmbName);
                cmbName.SelectedIndex = -1;

                cbIsCommon.Visible = false;
            }

            dgvData.ResumeLayout();
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if(cmbDataList.SelectedIndex != -1)
            {
                ChangeEditPanel();
                ShowOrHideDataEdit(true);
            }
            else
            {
                errorProvider1.SetError(lblDataList, "Data List Required");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.Text = text_Add;
            ClearDataField();
            ClearError();
            dgvData.ClearSelection();
            DataEdit = false;
            ShowOrHideDataEdit(false);
        }

        private void cmbDataList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(loaded)
            {
                errorProvider1.Clear();
                LoadData();
                ChangeEditPanel();

                //DataRowView vrow = (DataRowView)cmbDataList.SelectedItem;
                //DataRow row = vrow.Row;

                //string sdsd = row["ID"].ToString();

                //MessageBox.Show(sdsd);
            }
        }

        private void LoadData()
        {
            DataGridView dgv = dgvData;
            DataTable dt = new DataTable();
            string selectedDataList = cmbDataList.Text;

            if (selectedDataList == text_SizeDataList)
            {
                dt = dalData.SizeSelect();
            }
            else if (selectedDataList == text_TypeDataList)
            {
                dt = dalData.TypeSelect();
            }
            else if (selectedDataList == text_CategoryDataList)
            {
                dt = dalData.CategorySelect();
            }
            else if (selectedDataList == text_ItemDataList)
            {
                dt = dalData.ItemSelect();
            }
            else if (selectedDataList == text_StdPackingList)
            {
                dt = dalData.StdPackingSelect();
            }

            dgv.DataSource = dt;
            dgv.ClearSelection();
        }

        private void ClearError()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
        }
        private bool Validation()
        {
            bool Result = true;
            string selectedDataList = cmbDataList.Text;

            if(DataEdit)
            {
                if (string.IsNullOrEmpty(txtDataCode.Text))
                {
                    errorProvider6.SetError(txtDataCode, "Data code Required");

                    Result = false;
                }
            }

            if (selectedDataList == text_SizeDataList)
            {
                if(string.IsNullOrEmpty(txtNumerator.Text))
                {
                    errorProvider2.SetError(lblNumerator, "Numerator Required");

                    Result = false;
                }

                if (string.IsNullOrEmpty(txtDenominator.Text))
                {
                    errorProvider3.SetError(lblDenominator, "Denominator Required");

                    Result = false;
                }

                if (string.IsNullOrEmpty(cmbUnit.Text))
                {
                    errorProvider4.SetError(lblUnit, "Unit Required");

                    Result = false;
                }

            }
            else if (selectedDataList == text_TypeDataList || selectedDataList == text_CategoryDataList)
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    errorProvider5.SetError(lblName, "Name Required");

                    Result = false;
                }
            }
            else if (selectedDataList == text_StdPackingList)
            {
                if (string.IsNullOrEmpty(txtQtyPerPacket.Text))
                {
                    errorProvider2.SetError(lblQtyPacket, "Qty per packet Required");

                    Result = false;
                }

                if (string.IsNullOrEmpty(txtQtyPerBag.Text))
                {
                    errorProvider3.SetError(lblQtyBag, "Qty per bag Required");

                    Result = false;
                }

                if (string.IsNullOrEmpty(cmbCode.Text))
                {
                    errorProvider4.SetError(lblCode, "Item Code Required");

                    Result = false;
                }

            }


            return Result;
        }

        private void InsertData()
        {
            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Confirm to insert new data to data base?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(dialogResult == DialogResult.Yes)
                {
                    string selectedDataList = cmbDataList.Text;

                    uData.Updated_Date = DateTime.Now;
                    uData.Updated_By = MainDashboard.USER_ID;

                    if (selectedDataList == text_SizeDataList)
                    {
                        uData.Size_Numerator = Convert.ToInt16(txtNumerator.Text);
                        uData.Size_Denominator = Convert.ToInt16(txtDenominator.Text);
                        uData.Size_Unit = cmbUnit.Text;

                        if (!dalData.InsertSize(uData))
                        {
                            MessageBox.Show("Failed to insert size data to DB.");
                        }
                        else
                        {
                            LoadData();
                            ClearDataField();
                            ClearError();
                        }

                    }
                    else if (selectedDataList == text_TypeDataList)
                    {
                        uData.Type_Name = txtName.Text.ToUpper();

                        if (cbIsCommon.Checked)
                        {
                            uData.IsCommon = true;
                        }
                        else
                        {
                            uData.IsCommon = false;
                        }

                        if (!dalData.InsertType(uData))
                        {
                            MessageBox.Show("Failed to insert type data to DB.");
                        }
                        else
                        {
                            LoadData();
                            ClearDataField();
                            ClearError();
                        }

                    }
                    else if (selectedDataList == text_CategoryDataList)
                    {
                        uData.Category_Name = txtName.Text.ToUpper();

                        if (!dalData.InsertCategory(uData))
                        {
                            MessageBox.Show("Failed to insert category data to DB.");
                        }
                        else
                        {
                            LoadData();
                            ClearDataField();
                            ClearError();
                        }
                    }
                    else if (selectedDataList == text_StdPackingList)
                    {
                        uData.Max_Lvl = Convert.ToInt16(txtMaxLevel.Text);
                        uData.Qty_Per_Packet = Convert.ToInt16(txtQtyPerPacket.Text);
                        uData.Qty_Per_Bag = Convert.ToInt16(txtQtyPerBag.Text);
                        uData.Item_code = cmbCode.Text;

                        if (!dalData.InsertStdPacking(uData))
                        {
                            MessageBox.Show("Failed to insert standard packing data to DB.");
                        }
                        else
                        {
                            LoadData();
                            ClearDataField();
                            ClearError();
                        }
                    }
                }
               
            }
            
        }

        private void UpdateData()
        {
            if (Validation())
            {
                DialogResult dialogResult = MessageBox.Show("Confirm to update data to data base?", "Message",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    string selectedDataList = cmbDataList.Text;

                    uData.Table_Code = Convert.ToInt16(txtDataCode.Text);
                    uData.Updated_Date = DateTime.Now;
                    uData.Updated_By = MainDashboard.USER_ID;

                    if (cbRemoveData.Checked)
                    {
                        uData.IsRemoved = true;
                    }
                    else
                    {
                        uData.IsRemoved = false;
                    }

                    if (selectedDataList == text_SizeDataList)
                    {
                        uData.Size_Numerator = Convert.ToInt16(txtNumerator.Text);
                        uData.Size_Denominator = Convert.ToInt16(txtDenominator.Text);
                        uData.Size_Unit = cmbUnit.Text;

                        if (!dalData.SizeUpdate(uData))
                        {
                            MessageBox.Show("Failed to update size data to DB.");
                        }
                        else
                        {
                            LoadData();
                            ClearDataField();
                        }

                    }
                    else if (selectedDataList == text_TypeDataList)
                    {
                        uData.Type_Name = txtName.Text.ToUpper();

                        if (cbIsCommon.Checked)
                        {
                            uData.IsCommon = true;
                        }
                        else
                        {
                            uData.IsCommon = false;
                        }

                        if (!dalData.TypeUpdate(uData))
                        {
                            MessageBox.Show("Failed to update type data to DB.");
                        }
                        else
                        {
                            LoadData();
                            ClearDataField();
                        }

                    }
                    else if (selectedDataList == text_CategoryDataList)
                    {
                        uData.Category_Name = txtName.Text.ToUpper();

                        if (!dalData.CategoryUpdate(uData))
                        {
                            MessageBox.Show("Failed to update category data to DB.");
                        }
                        else
                        {
                            LoadData();
                            ClearDataField();
                        }
                    }
                    else if (selectedDataList == text_StdPackingList)
                    {
                        uData.Max_Lvl = Convert.ToInt16(txtMaxLevel.Text);
                        uData.Qty_Per_Packet = Convert.ToInt16(txtQtyPerPacket.Text);
                        uData.Qty_Per_Bag = Convert.ToInt16(txtQtyPerBag.Text);
                        uData.Item_code = cmbCode.Text;

                        if (!dalData.StdPackingUpdate(uData))
                        {
                            MessageBox.Show("Failed to update standard packing data to DB.");
                        }
                        else
                        {
                            LoadData();
                            ClearDataField();
                            ClearError();
                        }
                    }
                }

            }

        }
        private void ClearDataField()
        {
            txtDataCode.Clear();
            txtNumerator.Clear();
            txtDenominator.Text = "1";
            txtName.Clear();
            cbIsCommon.Checked = false;
            cmbName.SelectedIndex = -1;
            cmbCode.SelectedIndex = -1;
            txtQtyPerPacket.Clear();
            txtQtyPerBag.Clear();
            txtMaxLevel.Clear();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtDataCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(!DataEdit)
            {
                InsertData();
            }
            else
            {
                cbRemoveData.Visible = true;
                UpdateData();
                ResetDataField();
                DataEdit = false;
            }
        }

        private void ResetDataField()
        {
            cbRemoveData.Visible = false;
            btnAdd.Text = text_Add;
            ClearDataField();
            ClearError();
        }
        private void dgvData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int rowIndex = dgvData.CurrentRow.Index;
            
            DataGridView dgv = dgvData;
            if(rowIndex > -1)
            {
                DataEdit = true;
                cbRemoveData.Visible = true;
                ChangeEditPanel();
                ShowOrHideDataEdit(true);
                btnAdd.Text = text_Edit;
                txtDataCode.Text = dgv.Rows[rowIndex].Cells[dalData.TableCode].Value.ToString();

                if(dgv.Rows[rowIndex].Cells[dalData.IsRemoved].Value != DBNull.Value)
                {
                    bool isRemoved = Convert.ToBoolean(dgv.Rows[rowIndex].Cells[dalData.IsRemoved].Value);

                    cbRemoveData.Checked = isRemoved;
                }
               

                string selectedDataList = cmbDataList.Text;

                if (selectedDataList == text_SizeDataList)
                {
                    txtNumerator.Text = dgv.Rows[rowIndex].Cells[dalData.SizeNumerator].Value.ToString();
                    txtDenominator.Text = dgv.Rows[rowIndex].Cells[dalData.SizeDenominator].Value.ToString();
                    cmbUnit.Text = dgv.Rows[rowIndex].Cells[dalData.SizeUnit].Value.ToString();
                }
                else if (selectedDataList == text_TypeDataList)
                {
                    txtName.Text = dgv.Rows[rowIndex].Cells[dalData.TypeName].Value.ToString();

                    bool isCommon = Convert.ToBoolean(dgv.Rows[rowIndex].Cells[dalData.IsCommon].Value);

                    cbIsCommon.Checked = isCommon;
                }
                else if (selectedDataList == text_CategoryDataList)
                {
                    txtName.Text = dgv.Rows[rowIndex].Cells[dalData.CategoryName].Value.ToString();
                }
                else if (selectedDataList == text_StdPackingList)
                {
                    string itemCode = dgv.Rows[rowIndex].Cells[dalData.ItemCode].Value.ToString();
                    cmbName.Text = tool.getItemName(itemCode);
                    cmbCode.Text = itemCode;

                    txtQtyPerPacket.Text = dgv.Rows[rowIndex].Cells[dalData.QtyPerPacket].Value.ToString();
                    txtQtyPerBag.Text = dgv.Rows[rowIndex].Cells[dalData.QtyPerBag].Value.ToString();
                    txtMaxLevel.Text = dgv.Rows[rowIndex].Cells[dalData.MaxLevel].Value.ToString();

                }
            }
            else
            {
                btnAdd.Text = text_Add; 
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ClearError();
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
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

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumerator_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearError();
        }

        private void txtQtyPerPacket_TextChanged(object sender, EventArgs e)
        {
            ClearError();
        }
    }
}
