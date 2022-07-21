using System;
using System.Data;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmMultiParent : Form
    {
        public frmMultiParent()
        {
            InitializeComponent();
           
        }

        public frmMultiParent(string itemCode)
        {
            InitializeComponent();
            dtJoin = dalJoin.SelectwithParentInfo();
            LoadParentList(itemCode);
        }

        public frmMultiParent(int sheetID, string itemCode)
        {
            InitializeComponent();

            dtJoin = dalJoin.SelectwithParentInfo();
            LoadParentList(itemCode);


            LoadPackagingList(sheetID);
        }

        public frmMultiParent(DataTable dt, string itemCode)
        {
            InitializeComponent();

            dtJoin = dalJoin.SelectwithParentInfo();
            LoadParentList(itemCode);

            dgvList.DataSource = dt;
        }

        joinDAL dalJoin = new joinDAL();
        itemDAL dalItem = new itemDAL();
        ProductionRecordDAL dalProRecord = new ProductionRecordDAL();

        Tool tool = new Tool();
        Text text = new Text();

        DataTable dt_Parent;
        DataTable dtJoin;
        DataTable dt_List;

        readonly private string header_Code = "CODE";
        readonly private string header_Name = "NAME";
        readonly private string header_Qty = "QTY";

        private DataTable NewItemTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(header_Code, typeof(string));
            dt.Columns.Add(header_Name, typeof(string));
            dt.Columns.Add(header_Qty, typeof(int));

            return dt;

        }
        private void frmMultiParent_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadParentList(string itemCode)
        {
            dt_Parent = GetParentList(itemCode);

            if (dt_Parent.Rows.Count > 0)
            {
                dt_Parent.DefaultView.Sort = header_Name+" ASC";

                cmbName.DataSource = dt_Parent;
                cmbName.DisplayMember = header_Name;
                cmbName.SelectedIndex = -1;
            }
            else
            {
                cmbName.DataSource = null;
            }
        }

        private DataTable GetParentList(string itemCode)
        {
            DataTable dt = new DataTable();

            if (dtJoin.Rows.Count > 0)
            {
                dt.Columns.Add(header_Code);
                dt.Columns.Add(header_Name);

                foreach (DataRow row in dtJoin.Rows)
                {
                    string childCode = row["child_code"].ToString();

                    if(childCode == itemCode)
                    {
                        bool duplicate = false;
                        string parentCode = row["parent_code"].ToString();
                        string parentName = row["parent_name"].ToString();

                        DataTable dt_GrandParentList = GetParentList(parentCode);

                        if (dt_GrandParentList.Rows.Count > 0)
                        {
                            dt.Merge(dt_GrandParentList);
                        }

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row2 in dt.Rows)
                            {
                                string list_ParentCode = row2[header_Code].ToString();

                                if (list_ParentCode == parentCode)
                                {
                                    duplicate = true;
                                    break;
                                }
                            }
                        }

                        if (!duplicate)
                        {
                            dt.Rows.Add(parentCode, parentName);
                        }
                    }
                   

                }


            }

            return dt;
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Name = cmbName.Text;

            if(!string.IsNullOrEmpty(Name))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(header_Code);

                foreach(DataRow row in dt_Parent.Rows)
                {
                    if(Name == row[header_Name].ToString())
                    {
                        dt.Rows.Add(row[header_Code].ToString());
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    dt.DefaultView.Sort = header_Code + " ASC";

                    cmbCode.DataSource = dt;
                    cmbCode.DisplayMember = header_Code;
                    //cmbCode.SelectedIndex = -1;
                }
                else
                {
                    cmbCode.DataSource = null;
                }
            }
        }

        private void ClearAllError()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();

        }

        private bool Validation()
        {
            ClearAllError();
            bool passed = true;

            if (string.IsNullOrEmpty(cmbName.Text))
            {
                errorProvider1.SetError(lblName, "Please select a item name!");
                passed = false;
            }

            if (string.IsNullOrEmpty(cmbCode.Text))
            {
                errorProvider2.SetError(lblCode, "Please select a item code!");
                passed = false;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                errorProvider3.SetError(lblQty, "Please input qty!");
                passed = false;
            }

            return passed;
        }

        private void ClearData()
        {
            txtQty.Clear();
            cmbName.SelectedIndex = -1;
        }

        private void LoadPackagingList(int sheetID)
        {
            DataGridView dgv = dgvList;

            dgv.DataSource = null;

            DataTable dt = NewItemTable();

            DataTable dt_ParentFromDB = dalProRecord.ParentRecordSelect(sheetID);

            foreach (DataRow row in dt_ParentFromDB.Rows)
            {
                DataRow dt_Row = dt.NewRow();

                string parentCode = row[dalProRecord.ParentCode].ToString();

                dt_Row[header_Code] = parentCode;
                dt_Row[header_Name] = tool.getItemName(parentCode);
                dt_Row[header_Qty] = row[dalProRecord.ParentQty].ToString();

                dt.Rows.Add(dt_Row);
            }

            dgv.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                int Qty = int.TryParse(txtQty.Text, out Qty) ? Qty : 0;

                string itemCode = cmbCode.Text;
                string itemName = cmbName.Text;

                DataTable dt = (DataTable)dgvList.DataSource;

                DataRow dt_Row = dt.NewRow();

                dt_Row[header_Code] = itemCode;
                dt_Row[header_Name] = itemName;
                dt_Row[header_Qty] = Qty;

                dt.Rows.Add(dt_Row);

                ClearData();
            }
        }
    }
}
