using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;

namespace FactoryManagementSoftware.Module
{
    class Tool
    {

        custDAL dalCust = new custDAL();
        facDAL dalFac = new facDAL();
        itemCatDAL dalItemCat = new itemCatDAL();
        itemDAL dalItem = new itemDAL();
        joinDAL dalJoin = new joinDAL();
        itemCustDAL dalItemCust = new itemCustDAL();

        #region UI design

        //without min width
        public void AddTextBoxColumns(DataGridView dgv,string HeaderText, string Name, DataGridViewAutoSizeColumnMode autoSize)
        {
            var col = new DataGridViewTextBoxColumn
            {
                HeaderText = HeaderText,
                Name = Name,
                AutoSizeMode = autoSize
            };

            dgv.Columns.Add(col);
        }

        //with min width
        public void AddTextBoxColumns(DataGridView dgv, string HeaderText, string Name, DataGridViewAutoSizeColumnMode autoSize, int minWidth)
        {
            var col = new DataGridViewTextBoxColumn
            {
                HeaderText = HeaderText,
                Name = Name,
                AutoSizeMode = autoSize,
                MinimumWidth = minWidth
            };

            dgv.Columns.Add(col);
        }

        public void listPaint(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            //dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgv.ClearSelection();
        }

        public void listPaintGreyHeader(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 237, 237);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgv.ClearSelection();
        }
        #endregion

        #region Convert variable

        public int Int_TryParse(string value)
        {
            int result;
            int.TryParse(value, out result);
            return result;
        }

        public float Float_TryParse(string value)
        {
            float result;
            float.TryParse(value, out result);
            return result;
        }

        #endregion

        #region History Record

        public void historyRecord(string action, string detail, DateTime date, int by)
        {
            //save history
            historyDAL dalHistory = new historyDAL();
            historyBLL uHistory = new historyBLL();

            uHistory.history_date = date;
            uHistory.history_by = by;
            uHistory.history_action = action;
            uHistory.history_detail = detail;

            bool result = dalHistory.insert(uHistory);

            if(!result)
            {
                MessageBox.Show("Failed to add new history");
            }
        }

        #endregion

        #region Load Data

        public void loadItemCategoryDataToComboBox(ComboBox cmb)
        {
            DataTable dtItemCat = dalItemCat.Select();
            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "item_cat_name";
        }

        public void loadItemNameDataToComboBox(ComboBox cmb, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.catSearch(keywords);
                DataTable dtItemName = dt.DefaultView.ToTable(true, "item_name");

                dtItemName.DefaultView.Sort = "item_name ASC";
                cmb.DataSource = dtItemName;
                cmb.DisplayMember = "item_name";
            }
            else
            {
                cmb.DataSource = null;
            }
        }

        public void loadItemCodeDataToComboBox(ComboBox cmb, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
                DataTable dtItemCode = dt.DefaultView.ToTable(true, "item_code");

                dtItemCode.DefaultView.Sort = "item_code ASC";
                cmb.DataSource = dtItemCode;
                cmb.DisplayMember = "item_code";
            }
            else
            {
                cmb.DataSource = null;
            }
        }

        public void loadCustomerToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = -1;
        }

        public void loadCustomerAndAllToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "cust_name ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = 0;
        }

        public void loadMaterialAndAllToComboBox(ComboBox cmb)
        {
            DataTable dtItemCat = dalItemCat.Select();

            DataTable distinctTable = dtItemCat.DefaultView.ToTable(true, "item_cat_name");
            distinctTable.Rows.Add("All");
            distinctTable.DefaultView.Sort = "item_cat_name ASC";
            for (int i = distinctTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = distinctTable.Rows[i];
                if (dr["item_cat_name"].ToString() == "Part")
                {
                    distinctTable.Rows.Remove(dr);
                }
            }
            distinctTable.AcceptChanges();
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "item_cat_name";
            cmb.SelectedIndex = 0;
        }

        public int getCustID(string custName)
        {
            string custID = "";

            DataTable dtCust = dalCust.nameSearch(custName);

            foreach (DataRow Cust in dtCust.Rows)
            {
                custID = Cust["cust_id"].ToString();
            }
            
            if(!string.IsNullOrEmpty(custID))
            {
                return Convert.ToInt32(custID);
            }
            else
            {
                return -1;
            }
            
        }

        public string getCustName(int custID)
        {
            string custName = "";

            DataTable dtCust = dalCust.idSearch(custID.ToString());

            foreach (DataRow Cust in dtCust.Rows)
            {
                custName = Cust["cust_name"].ToString();
            }

            return custName;
        }

        public int getFactoryID(string factoryName)
        {
            string factoryID = "";

            DataTable dtFac = dalFac.nameSearch(factoryName);

            if(dtFac.Rows.Count > 0)
            {
                foreach (DataRow fac in dtFac.Rows)
                {
                    factoryID = fac["fac_id"].ToString();
                }
                return Convert.ToInt32(factoryID);
            }
            else
            {
                return -1;
            }
        }

        public DataTable RemoveDuplicates(DataTable dt, string columnName)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        break;
                    }
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (dt.Rows[i][columnName].ToString() == dt.Rows[j][columnName].ToString())
                        {
                            dt.Rows[i].Delete();
                            break;
                        }
                    }
                }
                dt.AcceptChanges();

            }
            return dt;
        }

        #endregion

        #region Validation

        public bool ifGotChild(string itemCode)
        {
            bool result = false;
            DataTable dtJoin = dalJoin.parentCheck(itemCode);
            if (dtJoin.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        public bool IfExists(string itemCode, string custName)
        {
            DataTable dt = dalItemCust.existsSearch(itemCode, getCustID(custName).ToString());

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public float stillNeedCheck(float n)
        {
            if (n >= 0)
            {
                n = 0;
            }
            else
            {
                n *= -1;
            }
            return n;
        }

        #endregion
    }
}
