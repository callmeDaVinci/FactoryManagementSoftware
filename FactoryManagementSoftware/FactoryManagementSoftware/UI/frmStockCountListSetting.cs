using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System.Linq;
using System.Threading;
using Syncfusion.XlsIO.Parser.Biff_Records;
using Guna.UI.WinForms;

namespace FactoryManagementSoftware.UI
{
    public partial class frmStockCountListSetting : Form
    {
        #region Default Settings

        Tool tool = new Tool();
        Text text = new Text();
        facDAL dalFac = new facDAL();
        stockCountListDAL dalStockCountList = new stockCountListDAL();
        stockCountListBLL uStockCountList = new stockCountListBLL();

        DataTable DT_STOCK_COUNT_LIST;
        DataTable DT_ACTIVE_FACTORY_LIST;

        private string BUTTON_INSERT_TEXT = "Add List Type";
        private string BUTTON_UPDATE_TEXT = "Update List Type";
        private bool DATA_SAVED = false;

        private int TBL_CODE = -1;


        public frmStockCountListSetting()
        {
            InitializeComponent();
            InitialSetting();
        }

        public frmStockCountListSetting(int tableCode)
        {
            InitializeComponent();

            TBL_CODE = tableCode;

            InitialSetting();

        }
        private void InitialSetting()
        {

            txtListName.Text = "";
            lblListNameErrorMessage.Visible = false;

            LoadStockCountListData();
            LoadActiveFactoryListData();

            InitialLocationComboBox(DT_ACTIVE_FACTORY_LIST);
            InsertButtonTextChange();

            if (TBL_CODE > 0)
            {
                LoadExistingData();
            }
        }

        private void InitialLocationComboBox(DataTable dt)
        {
            cmbStockLocation.DataSource = dt;
            cmbStockLocation.DisplayMember = dalFac.FacName;
            cmbStockLocation.ValueMember = dalFac.FacID;
            cmbStockLocation.SelectedIndex = -1;
        }

        private void LoadStockCountListData()
        {
            DT_STOCK_COUNT_LIST = dalStockCountList.SelectAll();
        }

        private void LoadActiveFactoryListData()
        {
            DT_ACTIVE_FACTORY_LIST = dalFac.NewSelectDESC();
        }

        private void LoadExistingData()
        {
           if(DT_STOCK_COUNT_LIST?.Rows.Count > 0 && TBL_CODE > 0)
            {
                foreach(DataRow row in DT_STOCK_COUNT_LIST.Rows)
                {
                    string tblCode = row[dalStockCountList.TblCode].ToString();

                    if(tblCode == TBL_CODE.ToString())
                    {
                        txtListName.Text = row[dalStockCountList.ListDescription].ToString();

                        string facID = row[dalStockCountList.DefaultFactoryTblCode].ToString();

                        foreach(DataRow rowFac in DT_ACTIVE_FACTORY_LIST.Rows)
                        {
                            if(facID == rowFac[dalFac.FacID].ToString())
                            {
                                cmbStockLocation.Text = rowFac[dalFac.FacName].ToString();

                                break;
                            }    
                        }

                        break;
                    }
                }
            }

        }

        private bool Validation()
        {
            lblListNameErrorMessage.Visible = false;
            errorProvider1.Clear();
            errorProvider2.Clear();
            bool result = true;

            if (string.IsNullOrEmpty(txtListName.Text))
            {
                errorProvider1.SetError(lblListName, "List Name Required");
                result = false;
            }

            if (string.IsNullOrEmpty(cmbStockLocation.Text))
            {
                errorProvider2.SetError(lblStockLocation, "Stock Location Required");
                result = false;
            }

            if (DT_STOCK_COUNT_LIST?.Rows.Count > 0)
            {
                string newListName = txtListName.Text.ToUpper().Replace(" ", "");
                foreach (DataRow row in DT_STOCK_COUNT_LIST.Rows)
                {
                    string listName = row[dalStockCountList.ListDescription].ToString().ToUpper().Replace(" ", "");
                    string tblCode = row[dalStockCountList.TblCode].ToString();

                    if (listName == newListName)
                    {
                        if((TBL_CODE != -1 && tblCode != TBL_CODE.ToString()) || TBL_CODE == -1)
                        {
                            lblListNameErrorMessage.Text = "Name already taken, please choose a different one.";
                            errorProvider1.SetError(lblListName, "Name already taken, please choose a different one.");
                            lblListNameErrorMessage.Visible = true;
                            result = false;
                            break;
                        }
                    }

                }
            }

            return result;
        }

        private void ListTypeInsert()
        {
            dalStockCountList.CreateTable();

            uStockCountList.list_description = txtListName.Text;

            if (cmbStockLocation.SelectedIndex != -1)
            {
                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbStockLocation.SelectedItem;
             
                int facID = int.TryParse(selectedRow[dalFac.FacID].ToString(), out int i) ? i : 3;
                uStockCountList.default_factory_tbl_code = facID;
            }
           
            uStockCountList.isRemoved = false;
            uStockCountList.remark = "";
            uStockCountList.updated_by = MainDashboard.USER_ID;
            uStockCountList.updated_date = DateTime.Now;

            if(!dalStockCountList.Insert(uStockCountList))
            {
                MessageBox.Show("Failed to insert new type of Stock Count List!");
            }
            else
            {
                MessageBox.Show("New type of Stock Count List added!");
                DATA_SAVED = true;
                Close();
            }
        }

        private void ListTypeUpdate()
        {

            uStockCountList.list_description = txtListName.Text;

            if (cmbStockLocation.SelectedIndex != -1)
            {
                // Get the DataRowView for the selected item
                DataRowView selectedRow = (DataRowView)cmbStockLocation.SelectedItem;

                int facID = int.TryParse(selectedRow[dalFac.FacID].ToString(), out int i) ? i : 3;
                uStockCountList.default_factory_tbl_code = facID;
            }
            uStockCountList.isRemoved = false;
            uStockCountList.remark = "";
            uStockCountList.updated_by = MainDashboard.USER_ID;
            uStockCountList.updated_date = DateTime.Now;
            uStockCountList.tbl_code = TBL_CODE;

            if (!dalStockCountList.Update(uStockCountList))
            {
                MessageBox.Show("Failed to update Stock Count List!");
            }
            else
            {
                MessageBox.Show("Stock Count List updated!");
                DATA_SAVED = true;

                Close();
            }
        }
        private void frmStockCountListEditing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved data. Leave without saving? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                }
                else
                {
                    e.Cancel = true;
                }

            }
        }

        #endregion

      
        private void InsertButtonTextChange()
        {
            if (TBL_CODE == -1)
            {
                btnInsert.Text = BUTTON_INSERT_TEXT;
            }
            else
            {
                btnInsert.Text = BUTTON_UPDATE_TEXT;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved data. Leave without saving? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Close();
                }
                

            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                if(TBL_CODE == -1)
                {
                    DialogResult dialogResult = MessageBox.Show("Confirm adding new type of stock count list?", "Message",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        ListTypeInsert();
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Confirm to make changes? ", "Message",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        ListTypeUpdate();
                    }
                }
               
            }
            
        }

        private void txtListName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void cmbStockLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }
    }
}
