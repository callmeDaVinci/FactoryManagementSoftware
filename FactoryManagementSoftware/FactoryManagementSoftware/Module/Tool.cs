﻿using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.UI;

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
        materialDAL dalMaterial = new materialDAL();
        trfHistBLL utrfHist = new trfHistBLL();
        trfHistDAL daltrfHist = new trfHistDAL();

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

        #region Load/Update Data

        public void DoubleBuffered(DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

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

        public void loadCustomerWithoutOtherToComboBox(ComboBox cmb)
        {
            DataTable dt = dalCust.Select();
            DataTable distinctTable = dt.DefaultView.ToTable(true, "cust_name");
            distinctTable.DefaultView.Sort = "cust_name ASC";

            distinctTable.AcceptChanges();
            foreach (DataRow row in distinctTable.Rows)
            {
                if (row["cust_name"].ToString().Equals("OTHER"))
                {
                    row.Delete();
                }
            }
            distinctTable.AcceptChanges();

            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "cust_name";
            cmb.SelectedIndex = -1;
        }

        public void loadRAWMaterialToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMaterial.catSearch("RAW Material");
            DataTable distinctTable = dt.DefaultView.ToTable(true, "material_code");
            distinctTable.DefaultView.Sort = "material_code ASC";
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "material_code";
            cmb.SelectedIndex = -1;
        }

        public void loadMasterBatchToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMaterial.catSearch("Master Batch");
            DataTable distinctTable = dt.DefaultView.ToTable(true, "material_code");
            distinctTable.DefaultView.Sort = "material_code ASC";
            distinctTable.Rows.Add("COMPOUND");
            distinctTable.Rows.Add("NATURAL");
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "material_code";
            cmb.SelectedIndex = -1;
        }

        public void loadPigmentToComboBox(ComboBox cmb)
        {
            DataTable dt = dalMaterial.catSearch("Pigment");
            DataTable distinctTable = dt.DefaultView.ToTable(true, "material_code");
            distinctTable.DefaultView.Sort = "material_code ASC";
            distinctTable.Rows.Add("COMPOUND");
            distinctTable.Rows.Add("NATURAL");
            cmb.DataSource = distinctTable;
            cmb.DisplayMember = "material_code";
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

        public string getItemName(string itemCode)
        {
            string itemName = "";

            DataTable dtItem = dalItem.codeSearch(itemCode);

            foreach (DataRow item in dtItem.Rows)
            {
                itemName = item["item_name"].ToString();
            }

            return itemName;
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

        public string getFactoryName(string factoryID)
        {
            string factoryName = "";

            DataTable dtFac = dalFac.idSearch(factoryID);

            if (dtFac.Rows.Count > 0)
            {
                foreach (DataRow fac in dtFac.Rows)
                {
                    factoryName = fac["fac_name"].ToString();
                }
            }

            return factoryName;
        }

        public string getCustomerName(string itemCode)
        {
            string CustomerName = "";
            
            DataTable dt = dalItemCust.checkItemCustTable(itemCode);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow cust in dt.Rows)
                {
                    CustomerName = getCustName(Convert.ToInt32(cust["cust_id"].ToString()));
                }
            }

            return CustomerName;
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

        public void changeTransferRecord(string stockResult, int id)
        {
            if(id == -1)
            {
                MessageBox.Show("Failed to change transfer record,id == -1, transfer record not found");
                historyRecord("System", "id == -1, transfer record not found", utrfHist.trf_hist_updated_date, MainDashboard.USER_ID);
            }
            else
            {
                utrfHist.trf_hist_updated_date = DateTime.Now;
                utrfHist.trf_hist_updated_by = MainDashboard.USER_ID;
                utrfHist.trf_hist_id = id;
                utrfHist.trf_result = stockResult;

                //Inserting Data into Database
                bool success = daltrfHist.Update(utrfHist);
                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show("Failed to change transfer record");
                    historyRecord("System", "Failed to change transfer record", utrfHist.trf_hist_updated_date, MainDashboard.USER_ID);
                }
            }
            
        }

        public string getCatNameFromDataTable(DataTable dt,string itemCode)
        {
            string catName = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if(row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        catName = row["item_cat"].ToString();
                        return catName;
                    }
                }
            }
            return catName;
        }

        public float getStockQtyFromDataTable(DataTable dt, string itemCode)
        {
            float stockQty = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        stockQty = Convert.ToSingle(row["item_qty"].ToString());
                        return stockQty;
                    }
                }
            }
            return stockQty;
        }

        public string getMaterialNameFromDataTable(DataTable dt, string materialCode)
        {
            string materialName = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(materialCode))
                    {
                        materialName = row["item_name"].ToString();

                        return materialName;
                    }
                }
            }
            return materialName;
        }

        public float getOrderQtyFromDataTable(DataTable dt, string itemCode)
        {
            float orderQty = 0;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        orderQty = Convert.ToSingle(row["item_ord"].ToString());

                        return orderQty;
                    }
                }
            }
           
            return orderQty;
        }

        public float getPMMAQtyFromDataTable(DataTable dt, string itemCode)
        {
            float PMMAQty = -1;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[dalItem.ItemCode].ToString().Equals(itemCode))
                    {
                        PMMAQty = row[dalItem.ItemPMMAQty] == DBNull.Value? -1 : Convert.ToSingle(row[dalItem.ItemPMMAQty].ToString());

                        return PMMAQty;
                    }
                }
            }

            return PMMAQty;
        }

        #endregion

        #region Validation

        public bool ifGotChild(string itemCode, DataTable dt)
        {
            bool result = false;

            foreach (DataRow join in dt.Rows)
            {
                if(join["parent_code"].ToString().Equals(itemCode))
                {
                    return true;
                }
            }
            return result;
        }

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

        public bool IfProductsExists(string productCode)
        {
            DataTable dt;

            dt = dalItem.codeSearch(productCode);

            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

       
        #endregion

        #region System

        public void saveToText(Exception ex)//error message
        {
            
            Directory.CreateDirectory(@"D:\StockAssistant\SystemError");
            string today = DateTime.Now.Date.ToString("yyyy_MM_dd");
            string filePath = @"D:\StockAssistant\SystemError\Error_" + today + ".txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine();
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine();
                    writer.WriteLine("StackTrace : ");
                    writer.WriteLine(ex.StackTrace);

                    ex = ex.InnerException;
                }
            }

            Directory.CreateDirectory(@"D:\StockAssistant\SystemHistory");
            filePath = @"D:\StockAssistant\SystemHistory\History_" + today + ".txt";

            if (ex != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();
                    writer.WriteLine("Action : SYSTEM ERROR");
                    writer.WriteLine();
                    writer.WriteLine("Detail : " + ex.Message);
                    writer.WriteLine();
                }
            }
        }

        public void saveToTextAndMessageToUser(Exception ex)
        {
            Directory.CreateDirectory(@"D:\StockAssistant\SystemError");
            string today = DateTime.Now.Date.ToString("yyyy_MM_dd");
            string filePath = @"D:\StockAssistant\SystemError\Error_" + today + ".txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                while (ex != null)
                {
                    MessageBox.Show("An unexpected error has occurred. Please contact your system administrator.\n\n"+ex.Message, "System Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine();
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine();
                    writer.WriteLine("StackTrace : ");
                    writer.WriteLine(ex.StackTrace);

                    ex = ex.InnerException;
                }
            }

            Directory.CreateDirectory(@"D:\StockAssistant\SystemHistory");
            filePath = @"D:\StockAssistant\SystemHistory\History_" + today + ".txt";

            if(ex != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();
                    writer.WriteLine("Action : SYSTEM ERROR");
                    writer.WriteLine();
                    writer.WriteLine("Detail : " + ex.Message);
                    writer.WriteLine();
                }
            }
           
        }

        public void historyRecord(string action, string detail, DateTime date, int by)
        {
            //save history
            historyDAL dalHistory = new historyDAL();
            historyBLL uHistory = new historyBLL();

            userDAL dalUser = new userDAL();
            uHistory.history_date = date;
            uHistory.history_by = by;
            uHistory.history_action = "["+ dalUser.getUsername(by)+"] "+action;
            uHistory.history_detail = detail;

            bool result = dalHistory.insert(uHistory);

            if (!result)
            {
                MessageBox.Show("Failed to add new history");
            }

            Directory.CreateDirectory(@"D:\StockAssistant\SystemHistory");
            string today = DateTime.Now.Date.ToString("yyyy_MM_dd");
            string filePath = @"D:\StockAssistant\SystemHistory\History_" + today + ".txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();
                writer.WriteLine("Action : "+action);
                writer.WriteLine();
                writer.WriteLine("Detail : " + detail);
                writer.WriteLine();
                writer.WriteLine("By : " + dalUser.getUsername(by));
                writer.WriteLine();
            }
        }

        #endregion
    }
}
