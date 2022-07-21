using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System.Drawing;
using System.Globalization;

namespace FactoryManagementSoftware.UI
{
    public partial class frmAddItem : Form
    {

        #region VARIABLE/OBJECT DECLARE

        Tool tool = new Tool();
        itemDAL dalItem = new itemDAL();
        facStockDAL dalFacStock = new facStockDAL();
        facStockBLL uStock = new facStockBLL();
        private int rowCount = 0;
        //private int rowTotal = 0;
        private string path = null;
        private string excelName = null;
        private string cust, index, matCode, itemName, itemCode, color, mbCode, mbType, jointType, jointLevel, jointQty, factory;

        readonly string colCustName = "CUSTOMER";
        readonly string colNoName = "#";
        readonly string colMBTypeName = "MB TYPE";
        readonly string colJointTypeName = "JOINT TYPE";
        readonly string colJointLevelName = "JOINT LEVEL";
        readonly string colJointQtyName = "JOINT QTY";

        readonly string header_No = "#";
        readonly string header_Code = "CODE";
        readonly string header_Name = "NAME";
        readonly string header_Factory = "FACTORY";

        #endregion

        #region LOAD

        public frmAddItem()
        {
            InitializeComponent();
            //tool.loadCustomerToComboBox(cmbCust);
            //tool.loadItemCategoryDataToComboBox(cmbCat);
            tool.DoubleBuffered(dgvList, true);
        }

        #endregion

        #region FUNCTION

        private string checkMaterial(string itemCode)
        {
            //search by code
            //search by name
            //still cannot found, then ask user want to add new mat or not

            //return material item code
            return itemCode;
        }

        private void pairCustomer(string cust, string itemCode)
        {
            itemCustBLL uItemCust = new itemCustBLL();
            itemCustDAL dalItemCust = new itemCustDAL();
           
            if (!tool.IfExists(itemCode, cust))
            {
                uItemCust.cust_id = Convert.ToInt32(tool.getCustID(cust));
                uItemCust.item_code = itemCode;
                uItemCust.item_cust_added_date = DateTime.Now;
                uItemCust.item_cust_added_by = MainDashboard.USER_ID;
                uItemCust.forecast_one = 0;
                uItemCust.forecast_two = 0;
                uItemCust.forecast_three = 0;
                uItemCust.forecast_current_month = DateTime.Now.ToString("MMMM");

                bool success = dalItemCust.Insert(uItemCust);

                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show("Failed to add new item_cust record");
                }
            }
            else
            {
                MessageBox.Show("Data already exist.");
            }
            
        }

        private void updateItem(string category, string itemCode, string itemName)
        {
            //Update data
            itemBLL u = new itemBLL();
            u.item_cat = category;
            u.item_code = itemCode;
            u.item_name = itemName;

            u.item_material = "";
            u.item_mb = "";
            u.item_color = "";

            u.item_quo_ton = 0;
            u.item_best_ton = 0;
            u.item_pro_ton = 0;

            u.item_quo_ct = 0;
            u.item_pro_ct_from = 0;
            u.item_pro_ct_to = 0;
            u.item_cavity = 0;

            u.item_quo_pw_pcs = 0;
            u.item_quo_rw_pcs = 0;
            u.item_pro_pw_pcs = 0;
            u.item_pro_rw_pcs = 0;

            u.item_pro_pw_shot = 0;
            u.item_pro_rw_shot = 0;
            u.item_pro_cooling = 0;
            u.item_wastage_allowed = 0.05f;

            u.item_assembly = 0;
            u.item_production = 0;


            u.item_updtd_date = DateTime.Now;
            u.item_updtd_by = MainDashboard.USER_ID;
            //Updating data into database
            //bool success = dalItem.Update(u);
            bool success = dalItem.NewUpdate(u);
            //if data is updated successfully then the value = true else false
            if (success == true)
            {
                //data updated successfully
                //MessageBox.Show("Item successfully updated ");

            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to updated item");
            }
        }

        private void updateItem(string category,string cust, string itemCode, string itemName, string Material, string MB, string Color)
        {
            //Update data
            itemBLL u = new itemBLL();
            u.item_cat = category;
            u.item_code = itemCode;
            u.item_name = itemName;

            u.item_material = Material;
            u.item_mb = MB;
            u.item_color = Color;

            u.item_quo_ton = 0;
            u.item_best_ton = 0;
            u.item_pro_ton = 0;

            u.item_quo_ct = 0;
            u.item_pro_ct_from = 0;
            u.item_pro_ct_to = 0;
            u.item_cavity = 0;

            u.item_quo_pw_pcs = 0;
            u.item_quo_rw_pcs = 0;
            u.item_pro_pw_pcs = 0;
            u.item_pro_rw_pcs = 0;

            u.item_pro_pw_shot = 0;
            u.item_pro_rw_shot = 0;
            u.item_pro_cooling = 0;
            u.item_wastage_allowed = 0.05f;

            u.item_assembly = 0;
            u.item_production = 0;
            

            u.item_updtd_date = DateTime.Now;
            u.item_updtd_by = MainDashboard.USER_ID;
            //Updating data into database
            //bool success = dalItem.Update(u);
            bool success = dalItem.NewUpdate(u);
            //if data is updated successfully then the value = true else false
            if (success == true)
            {
                //data updated successfully
                //MessageBox.Show("Item successfully updated ");
                pairCustomer(cust,itemCode);
                
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to updated item");
            }
        }

        private void insertItem(string category, string itemCode, string itemName)
        {
            //Add data
            itemBLL u = new itemBLL();
            u.item_cat = category;



            u.item_code = itemCode;
            u.item_name = itemName;

            u.item_material = "";
            u.item_mb = "";
            u.item_color = "";

            u.item_quo_ton = 0;
            u.item_best_ton = 0;
            u.item_pro_ton = 0;

            u.item_quo_ct = 0;
            u.item_pro_ct_from = 0;
            u.item_pro_ct_to = 0;
            u.item_cavity = 0;

            u.item_quo_pw_pcs = 0;
            u.item_quo_rw_pcs = 0;
            u.item_pro_pw_pcs = 0;
            u.item_pro_rw_pcs = 0;

            u.item_pro_pw_shot = 0;
            u.item_pro_rw_shot = 0;
            u.item_pro_cooling = 0;
            u.item_wastage_allowed = 0.05f;

            u.item_assembly = 0;
            u.item_production = 0;


            u.item_added_date = DateTime.Now;
            u.item_added_by = MainDashboard.USER_ID;

            //Inserting Data into Database
            bool success = dalItem.NewInsert(u);
            //If the data is successfully inserted then the value of success will be true else false
            if (success)
            {
                //Data Successfully Inserted
                //MessageBox.Show("Item successfully created");

            }
            else
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new item");
            }
        }

        private void insertItem(string category, string cust, string itemCode, string itemName, string Material, string MB, string Color)
        {
            //Add data
            itemBLL u = new itemBLL();
            u.item_cat = category;



            u.item_code = itemCode;
            u.item_name = itemName;

            u.item_material = Material;
            u.item_mb = MB;
            u.item_color = Color;

            u.item_quo_ton = 0;
            u.item_best_ton = 0;
            u.item_pro_ton = 0;

            u.item_quo_ct = 0;
            u.item_pro_ct_from = 0;
            u.item_pro_ct_to = 0;
            u.item_cavity = 0;

            u.item_quo_pw_pcs = 0;
            u.item_quo_rw_pcs = 0;
            u.item_pro_pw_pcs = 0;
            u.item_pro_rw_pcs = 0;

            u.item_pro_pw_shot = 0;
            u.item_pro_rw_shot = 0;
            u.item_pro_cooling = 0;
            u.item_wastage_allowed = 0.05f;

            u.item_assembly = 0;
            u.item_production = 0;


            u.item_added_date = DateTime.Now;
            u.item_added_by = MainDashboard.USER_ID;

            //Inserting Data into Database
            bool success = dalItem.NewInsert(u);
            //If the data is successfully inserted then the value of success will be true else false
            if (success)
            {
                //Data Successfully Inserted
                //MessageBox.Show("Item successfully created");

                pairCustomer(cust,itemCode);
                
            }
            else
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new item");
            }
        }

        private bool updateMaterial(string category, string itemCode, string itemName)
        {
            materialBLL uMaterial = new materialBLL();
            materialDAL dalMaterial = new materialDAL();

            //Update data
            uMaterial.material_cat = category;
            uMaterial.material_code = itemCode;
            uMaterial.material_name = itemName;

            uMaterial.material_zero_cost = 0;

            bool success = false;

            if(tool.IfMaterialExists(itemCode))
            {
                success = dalMaterial.Update(uMaterial);
            }
            else
            {
                success = dalMaterial.Insert(uMaterial);
            }
            
            if (success == true)
            {
                //data updated successfully
                //MessageBox.Show("Material successfully updated ");
                updateItem(category, itemCode, itemName);
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to updated material");
            }
            return success;
        }

        private bool insertMaterial(string category, string itemCode, string itemName)
        {
            materialBLL uMaterial = new materialBLL();
            materialDAL dalMaterial = new materialDAL();

            //Add data
            uMaterial.material_cat = category;
            uMaterial.material_code = itemCode;
            uMaterial.material_name = itemName;
            uMaterial.material_zero_cost = 0;

            bool success = dalMaterial.Insert(uMaterial);
            //If the data is successfully inserted then the value of success will be true else false
            if (success == true)
            {
                //Data Successfully Inserted
                //MessageBox.Show("Material successfully created");
                insertItem(category, itemCode, itemName);
            }
            else
            {
                //Failed to insert data
                dalMaterial.Delete(uMaterial);
                MessageBox.Show("Failed to add new material");
            }

            return success;
        }

        private void clearPreviousStock(string itemCode)
        {
            DataTable dt;

            dt = dalFacStock.Select(itemCode);

            if (dt.Rows.Count > 0)
            {
                int facID;

                foreach(DataRow row in dt.Rows)
                {
                    facID = row["fac_name"] == DBNull.Value ? -1 : Convert.ToInt32(tool.getFactoryID(row["fac_name"].ToString()));

                    uStock.stock_item_code = itemCode;
                    uStock.stock_fac_id = facID;
                    uStock.stock_qty = 0;
                    uStock.stock_unit = "Set";
                    uStock.stock_updtd_date = DateTime.Now;
                    uStock.stock_updtd_by = MainDashboard.USER_ID;

                    if (dalFacStock.IfExists(itemCode, facID.ToString()))
                    {
                        //Updating data into database
                        bool success = dalFacStock.Update(uStock);

                        if (!success)
                        {
                            //failed to update user
                            MessageBox.Show("Failed to updated stock");
                        }
                    }
                }
            }
              
        }

        private void addNewStock(string itemCode, string factory)
        {
            int facID = Convert.ToInt32(tool.getFactoryID(factory));
            uStock.stock_item_code = itemCode;
            uStock.stock_fac_id = facID;
            uStock.stock_qty = 1;
            uStock.stock_unit = "Set";
            uStock.stock_updtd_date = DateTime.Now;
            uStock.stock_updtd_by = MainDashboard.USER_ID;

            if (dalFacStock.IfExists(itemCode, facID.ToString()))
            {
                //Updating data into database
                bool success = dalFacStock.Update(uStock);

                if (!success)
                {
                    //failed to update user
                    MessageBox.Show("Failed to updated stock");
                }
            }
            else
            {
                //Inserting Data into Database
                bool success = dalFacStock.Insert(uStock);
                //If the data is successfully inserted then the value of success will be true else false
                if (!success)
                {
                    //Failed to insert data
                    MessageBox.Show("Failed to add new stock");
                }
            }

            dalItem.updateTotalStock(itemCode);
        }

        private void loopDataSource()
        {
            DataTable dt = dgvList.DataSource as DataTable;

            DataGridView dgv = dgvList;
            dgv.Enabled = false;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                int n = row.Index;

                itemCode = dgv.Rows[n].Cells[header_Code].Value.ToString();
                itemName = dgv.Rows[n].Cells[header_Name].Value.ToString();
                factory = dgv.Rows[n].Cells[header_Factory].Value.ToString();

                if(!factory.Equals("STORE"))
                {
                    factory = "No." + factory;
                }

                if(tool.IfFactoryExists(factory))
                {
                    if (tool.IfProductsExists(itemCode))
                    {
                        if (updateMaterial("Mould", itemCode, itemName))
                        {
                            dgv.Rows[n].Cells[header_Code].Style.BackColor = Color.LightYellow;

                            //delete previos factory stock 
                            clearPreviousStock(itemCode);

                            //add to factory
                            addNewStock(itemCode, factory);
                        }
                    }
                    else
                    {
                        if (insertMaterial("Mould", itemCode, itemName))
                        {
                            dgv.Rows[n].Cells[header_Code].Style.BackColor = Color.LightGreen;

                            //add to factory
                            addNewStock(itemCode, factory);
                        }
                    }
                }
                else
                {
                    dgv.Rows[n].Cells[header_Factory].Style.BackColor = Color.Red;
                }
               
            }
            dgv.Enabled = true;
        }

        private void frmAddItem_Load(object sender, EventArgs e)
        {
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            txtPath.BackColor = SystemColors.Window;
            txtPath.Clear();
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "File Excel|*.xlsx";

            DialogResult re = fd.ShowDialog();
            excelName = fd.SafeFileName;

            if(re == DialogResult.OK)
            {
                path = fd.FileName;

                //string extension = System.IO.Path.GetExtension(path);
                //MessageBox.Show(extension);
                //if (".csv".Equals(extension))

                txtPath.Text = path;
                txtPath.BackColor = Color.DarkSeaGreen;
            }
        }

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            if(path != null)
            {
                ReadExcel(path);
            }
            else
            {
                MessageBox.Show("path not found");
            }
        }

        private void AddDataToDatabase(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            try
            {
                Application.UseWaitCursor = true;
                lblUploadRow.Text = "0";
                if(Convert.ToInt32(lblTotalRowCount.Text) > 0)
                {
                    loopDataSource();
                }   
                else
                {
                    MessageBox.Show("No data to update!");
                }

            
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                Application.UseWaitCursor = false;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        #endregion

        #region DATAGRIDVIEW ACTION

        private void dataGridView_List_acc_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            object tempObject1 = e.CellValue1;
            object tempObject2 = e.CellValue2;
            if (!(tempObject1 is null) && !(tempObject2 is null))
            {
                if (float.TryParse(tempObject1.ToString(), out float tmp) && float.TryParse(tempObject2.ToString(), out tmp))
                {
                    e.SortResult = float.Parse(tempObject1.ToString()).CompareTo(float.Parse(tempObject2.ToString()));
                    e.Handled = true;//pass by the default sorting
                }
            }
        }

        private void columnAdd(object sender, EventArgs e)
        {
            DataTable dt = dgvList.DataSource as DataTable;
            
            //Create the new row
            DataRow row = dt.NewRow();
            //Add the row to data table
            dt.Rows.Add(row);
            rowCount++;
            
            lblTotalRowCount.Text = rowCount.ToString();
        }

        private void columnRemove(object sender, EventArgs e)
        {
            DataTable dt = dgvList.DataSource as DataTable;

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(dt.Rows.Count - 1);
                    
                    if(rowCount > 0)
                    {
                        rowCount--;

                    }
                    else
                    {
                        rowCount = 0;
                    }

                    lblTotalRowCount.Text = rowCount.ToString();
                }
                    
            }

        }

        #endregion

        #region UI

        private void dgvUIForPartEdit(DataGridView dgv)
        {
            dgv.Columns[dalItem.ItemCode].HeaderText = "CODE";
            dgv.Columns[dalItem.ItemName].HeaderText = "NAME";
            dgv.Columns[dalItem.ItemMaterial].HeaderText = "RAW MAT";
            dgv.Columns[dalItem.ItemMBatch].HeaderText = "MBATCH";
            dgv.Columns[dalItem.ItemColor].HeaderText = "COLOR";

            dgv.Columns[colCustName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[colNoName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[dalItem.ItemMaterial].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[dalItem.ItemCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dalItem.ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[dalItem.ItemColor].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[dalItem.ItemMBatch].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[colMBTypeName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgv.Columns[colJointTypeName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[colJointLevelName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[colJointQtyName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


        }

        private void dgvUIForMouldEdit(DataGridView dgv)
        { 
            dgv.Columns[header_No].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[header_Code].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[header_Name].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[header_Factory].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }


        #endregion

        #region EXCEL

        public DataTable addColumnToDataTable(DataTable dt)
        {
            dt.Columns.Add(colCustName);
            dt.Columns.Add(colNoName);

            dt.Columns.Add(dalItem.ItemMaterial);
            dt.Columns.Add(dalItem.ItemCode);
            dt.Columns.Add(dalItem.ItemName);

            dt.Columns.Add(dalItem.ItemColor);

            dt.Columns.Add(dalItem.ItemMBatch);
            dt.Columns.Add(colMBTypeName);

            dt.Columns.Add(colJointTypeName);
            dt.Columns.Add(colJointLevelName);
            dt.Columns.Add(colJointQtyName);

            return dt;
        }

        public DataTable addMouldColumnToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(header_No);
            dt.Columns.Add(header_Code);
            dt.Columns.Add(header_Name);
            dt.Columns.Add(header_Factory);

            return dt;
        }

        public void ReadExcel(string Path)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            rowCount = 0;
            DataTable dt = addMouldColumnToDataTable();

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            int rCnt;
            int rw = 0;
            int cl = 0;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(Path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;

            for (rCnt = 2; rCnt <= rw; rCnt++)
            {
                index = Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2);
                itemCode = Convert.ToString((range.Cells[rCnt, 2] as Excel.Range).Value2);
                itemName = Convert.ToString((range.Cells[rCnt, 3] as Excel.Range).Value2);
                factory = Convert.ToString((range.Cells[rCnt, 4] as Excel.Range).Value2);

                if (!string.IsNullOrEmpty(itemCode))
                {
                    DataRow row = dt.NewRow();

                    row[header_No] = index;
                    row[header_Code] = itemCode;
                    row[header_Name] = itemName;
                    row[header_Factory] = factory;

                    dt.Rows.Add(row);
                    rowCount++;
                    lblTotalRowCount.Text = rowCount.ToString();
                    itemCode = null;
                }
            }

            dgvList.DataSource = dt;
            dgvUIForMouldEdit(dgvList);

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            lblTotalRowCount.Text = rowCount.ToString();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        #endregion

    }
}
