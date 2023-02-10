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
using System.ComponentModel;
using System.Threading;
using System.Globalization;
using System.Linq;
using Microsoft.VisualBasic.ApplicationServices;

namespace FactoryManagementSoftware.UI
{
    public partial class frmForecast_NEW : Form
    {
        public frmForecast_NEW()
        {
            InitializeComponent();
            tool.DoubleBuffered(dgvForecast, true);

            if(tool.GetPermissionLevel(MainDashboard.USER_ID) >= MainDashboard.ACTION_LVL_THREE || MainDashboard.USER_ID == -1)

            {
                cbEditMode.Enabled = true;
            }
            else
            {
                cbEditMode.Enabled = false;
            }

            tool.loadCustomerWithoutOtherToComboBox(cmbCustomer);
            cmbCustomer.Text = tool.getCustName(1);
            InitializeFilterData();
        }

        private string textMoreFilters = "MORE FILTERS ...";
        private string textHideFilters = "HIDE FILTERS";
        string oldForecast, newForecast;
        int currentColumn = -1;
        int currentRow = -1;
        private bool loaded = false;
        private bool ableLoadCodeData = false;
        private int forecastMonthQty = 4;

        readonly string headerIndex = "#";
        readonly string headerPartCode = "CODE";
        readonly string headerPartName = "NAME";
        readonly string headerYear = "YEAR";
        readonly string headerMonth = "MONTH";
        readonly string headerForecast = "FORECAST";
        readonly string headerForecast1 = "FORECAST 1";
        readonly string headerUpdatedDate = "UPDATED DATE";
        readonly string headerUpdatedBy = "UPDATED BY";

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        userDAL dalUser = new userDAL();
        Tool tool = new Tool();
        Text text = new Text();


        private DataTable NewForecastTable()
        {
            DataTable dt = new DataTable();

            //dt.Columns.Add(headerIndex, typeof(int));
            //dt.Columns.Add(headerPartCode, typeof(string));
            //dt.Columns.Add(headerPartName, typeof(string));
            //dt.Columns.Add(headerYear, typeof(int));
            //dt.Columns.Add(headerMonth, typeof(string));
            //dt.Columns.Add(headerForecast, typeof(float));
            //dt.Columns.Add(headerUpdatedDate, typeof(DateTime));
            //dt.Columns.Add(headerUpdatedBy, typeof(int));

            string monthFrom = cmbMonthFrom.Text;

            string headerName = string.IsNullOrEmpty(monthFrom) || cmbMonthFrom.SelectedIndex == -1 ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(1) : monthFrom;
            string year = string.IsNullOrEmpty(cmbYearFrom.Text) || cmbYearFrom.SelectedIndex == -1 ? DateTime.Now.Year.ToString() : cmbYearFrom.Text;

            dt.Columns.Add(headerIndex, typeof(int));
            dt.Columns.Add(headerPartCode, typeof(string));
            dt.Columns.Add(headerPartName, typeof(string));

            for (int i = 1; i <= forecastMonthQty; i++)
            {
                dt.Columns.Add(headerName.ToUpper()+" "+year, typeof(float));
                year = DateTime.Parse("1-" + headerName + "-" + year).AddMonths(1).Year.ToString();
                headerName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Parse("1-" + headerName + "-2008").AddMonths(1).Month).ToUpper();
            }

            //dt.Columns.Add(headerForecast, typeof(float));
            dt.Columns.Add(headerUpdatedDate, typeof(DateTime));
            dt.Columns.Add(headerUpdatedBy, typeof(string));

            return dt;
        }

        private void dgvForecastUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Regular);
            
            //dgv.Columns[headerIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ////dgv.Columns[headerPartCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ////dgv.Columns[headerPartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgv.Columns[headerUpdatedDate].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgv.Columns[headerUpdatedBy].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            for (int i = 0; i <= forecastMonthQty; i++)
            {
                dgv.Columns[3 + i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dgv.Columns[3 + i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            dgv.Columns[headerPartName].Frozen = true;

        }

        private void InitializeFilterData()
        {
            loaded = false;
            cmbMonthFrom.DataSource = CultureInfo.InvariantCulture.DateTimeFormat
                                                     .MonthNames.Take(12).ToList();

            cmbYearFrom.DataSource = Enumerable.Range(2019, DateTime.Now.Year - 2019 + 2).ToList();

            cmbMonthFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            //cmbMonthFrom.Text = DateTime.Now.Month.ToString();
            cmbYearFrom.Text = DateTime.Now.Year.ToString();

            cmbYearTo.Text = DateTime.Now.AddMonths(forecastMonthQty - 1).Year.ToString();
            cmbMonthTo.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(forecastMonthQty - 1).Month);
            

            cmbPartName.Text = null;
            cmbPartCode.Text = null;
            //cmbMonthFrom.SelectedIndex = -1;
            //cmbMonthTo.Text = null;
            //cmbYearFrom.Text = null;

            loaded = true;
        }

        private void frmForecast_NEW_Load(object sender, EventArgs e)
        {
            dgvForecast.SuspendLayout();

            tlpForecast.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

            dgvForecast.ResumeLayout();

            btnFilter.Text = textMoreFilters;
            // cmbMonthFrom.SelectedIndex = -1;
            cmbMonthFrom.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month); 

            loaded = true;
        }

        private void CalTotalMonth()
        {
            forecastMonthQty = 4;

            int temp = 0;
            string monthFrom = cmbMonthFrom.Text;
            string monthTo = cmbMonthTo.Text;

            int yearFrom = int.TryParse(cmbYearFrom.Text, out int i)? Convert.ToInt32(cmbYearFrom.Text) : -1;
            int yearTo = int.TryParse(cmbYearTo.Text, out int j) ? Convert.ToInt32(cmbYearTo.Text) : -1;

            if (cmbYearFrom.SelectedIndex != -1)
            {
                if (cmbMonthFrom.SelectedIndex != -1)
                {
                    if(cmbYearTo.SelectedIndex != -1 && yearTo >= yearFrom)
                    {
                        if (cmbMonthTo.SelectedIndex != -1)
                        {
                            if(yearTo == yearFrom)
                            {
                                temp = DateTime.Parse("1." + monthTo + " 2008").Month - DateTime.Parse("1." + monthFrom + " 2008").Month + 1;
                            }
                            else
                            {
                                if(yearTo == yearFrom + 1 )
                                {
                                    temp += 12 - DateTime.Parse("1." + monthFrom + " 2008").Month + 1 + DateTime.Parse("1." + monthTo + " 2008").Month;
                                }
                                else
                                {
                                    temp += (yearTo - yearFrom - 1) * 12 + 12 - DateTime.Parse("1." + monthFrom + " 2008").Month +1 + DateTime.Parse("1." + monthTo + " 2008").Month;
                                }
                            }
                            
                        }
                        else
                        {
                            if(yearTo == yearFrom)
                            {
                                temp = 1;
                            }
                            else
                            {
                                temp = (yearTo - yearFrom) * 12 + 12 - DateTime.Parse("1." + monthFrom + " 2008").Month + 1;
                            }
                           
                        }
                    }
                    else
                    {
                        temp = 1;
                    }
                }
                else if(cmbYearTo.SelectedIndex != -1 && yearTo >= yearFrom)
                {
                    temp = 12;
                    if(cmbMonthTo.SelectedIndex != -1)
                    {
                        temp += (yearTo - yearFrom) * 12 + DateTime.Parse("1." + monthTo + " 2008").Month; 
                    }
                    else
                    {
                        temp += (yearTo - yearFrom) * 12;
                    }
                }
                else
                {
                    temp = 12;
                }
            }
            else
            {
                temp = 4;
            }

            //lblMonthToReset.Text = temp.ToString();
            forecastMonthQty = temp;
        }

        private string getMonthName(string keyword)
        {
            string monthName = null;

            for(int i = 0; i < keyword.Length; i++)
            {
                char c = keyword[i];

                if(c != ' ')
                {
                    monthName += c;
                }
                else
                {
                    break;
                }
            }

            return monthName;
        }

        private string getYear(string keyword)
        {
            string year = null;
            bool isYear = false;

            for (int i = 0; i < keyword.Length; i++)
            {
                char c = keyword[i];

                if(isYear)
                {
                    year += c;
                }
                if (c == ' ')
                {
                    isYear = true;
                }
               
            }

            return year;
        }

        private DataTable RemoveTerminatedItem(DataTable dt)
        {
            //"(TERMINATED)"
            DataTable dt_NEW = dt.Copy();

            dt_NEW.AcceptChanges();

            if(!cbShowTerminatedItem.Checked)
            {
                foreach (DataRow row in dt_NEW.Rows)
                {
                    // If this row is offensive then
                    string itemName = row[dalItem.ItemName].ToString();

                    if (itemName.Contains(text.Cat_Terminated))
                        if (itemName.Contains(text.Cat_Terminated))
                        {
                            row.Delete();

                        }
                }
                dt_NEW.AcceptChanges();
            }
         

            return dt_NEW;

        }

        private void loadForecastList()
        {
            Cursor = Cursors.WaitCursor;

            btnSearch.Enabled = false;
            frmLoading.ShowLoadingScreen();

            cbEditMode.Checked = false;
            dgvForecast.DataSource = null;
            string keywords = cmbCustomer.Text;
            CalTotalMonth();
            //MessageBox.Show(getYear("NOVEMBER 2019"));

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                string itemNameKeyword = cmbPartName.Text;
                string itemCodeKeyword = cmbPartCode.Text;

                DataTable dt_Data = NewForecastTable();
                DataRow dt_Row;
                DataTable dt = dalItemCust.custSearch(keywords);
                DataTable dt_ItemForecast = dalItemForecast.Select(tool.getCustID(keywords).ToString());
                int index = 1;

                dt.DefaultView.Sort = "item_name ASC";

                dt = dt.DefaultView.ToTable();

                dt = RemoveTerminatedItem(dt);

                foreach (DataRow row in dt.Rows)
                {
                    string itemName = row[dalItem.ItemName].ToString();
                    string itemCode = row[dalItem.ItemCode].ToString();

                    bool itemMatch = itemName.Contains(itemNameKeyword.ToUpper()) ;

                    itemMatch = itemMatch || string.IsNullOrEmpty(itemNameKeyword);

                    itemMatch = itemMatch && (itemCodeKeyword.Equals(itemCode, StringComparison.InvariantCulture) || string.IsNullOrEmpty(itemCodeKeyword) );

                    if (itemMatch)
                    {
                        dt_Row = dt_Data.NewRow();

                        dt_Row[headerIndex] = index;
                        dt_Row[headerPartCode] = itemCode;
                        dt_Row[headerPartName] = itemName;

                        DateTime date = DateTime.MaxValue;

                        for (int i = 0; i < forecastMonthQty; i++)
                        {
                            //get month and year
                            //MessageBox.Show(getYear(dt_Data.Columns[3 + i].ColumnName));

                            string headerText = dt_Data.Columns[3 + i].ColumnName;
                            int year = Convert.ToInt32(getYear(headerText));
                            int month = DateTime.Parse("1." + getMonthName(headerText) + " 2008").Month;

                            //search data
                            DataRow data = tool.getItemForecastDataRow(dt_ItemForecast, itemCode, year, month);

                            if (data != null)
                            {
                                dt_Row[3 + i] = data[dalItemForecast.ForecastQty];

                                DateTime UpdatedDate = Convert.ToDateTime(data[dalItemForecast.UpdatedDate]);

                                if (date == DateTime.MaxValue || date < UpdatedDate)
                                {
                                    date = UpdatedDate;
                                    dt_Row[headerUpdatedDate] = data[dalItemForecast.UpdatedDate];

                                    int updatedID = int.TryParse(data[dalItemForecast.UpdatedBy].ToString(), out updatedID) ? updatedID : 0;

                                    if (updatedID <= 0)
                                    {
                                        dt_Row[headerUpdatedBy] = "ADMIN";
                                    }
                                    else
                                    {
                                        dt_Row[headerUpdatedBy] = dalUser.getUsername(updatedID);
                                    }
                                    //dt_Row[headerUpdatedBy] = data[dalItemForecast.UpdatedBy];
                                }
                            }
                            else
                            {
                                dt_Row[3 + i] = 0;
                            }
                        }

                        dt_Data.Rows.Add(dt_Row);
                        index++;
                    }
                    

                }


                if (dt_Data.Rows.Count > 0)
                {
                    dgvForecast.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dgvForecast.ColumnHeadersVisible = false;
                    dgvForecast.DataSource = dt_Data;
                    dgvForecastUIEdit(dgvForecast);
                    dgvForecast.ColumnHeadersVisible = true;
                    dgvForecast.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                    //dgvForecast.AutoResizeColumns();

                    //dgvForecast.Columns[headerUpdatedBy].Width = 500;
                    dgvForecast.ClearSelection();
                }
            }

            frmLoading.CloseForm();

            btnSearch.Enabled = true;

            Cursor = Cursors.Arrow;
        }

        private void loadForecastListAndComboBox()
        {
            Cursor = Cursors.WaitCursor;

            cbEditMode.Checked = false;

            btnSearch.Enabled = false;
            frmLoading.ShowLoadingScreen();

            dgvForecast.DataSource = null;
            string keywords = cmbCustomer.Text;
            CalTotalMonth();
            //MessageBox.Show(getYear("NOVEMBER 2019"));

            //check if the keywords has value or not
            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt_Data = NewForecastTable();
                DataRow dt_Row;
                DataTable dt = dalItemCust.custSearch(keywords);
                DataTable dt_ItemForecast = dalItemForecast.Select(tool.getCustID(keywords).ToString());

                int index = 1;

                dt.DefaultView.Sort = "item_name ASC";

                dt = dt.DefaultView.ToTable();

                ableLoadCodeData = false;
                cmbPartName.DataSource = null;
                cmbPartCode.DataSource = null;

                dt = RemoveTerminatedItem(dt);

                cmbPartName.DisplayMember = "item_name";
                cmbPartName.ValueMember = "item_name";
                cmbPartName.DataSource = tool.RemoveDuplicates(dt.Copy(),"item_name");

                cmbPartName.SelectedIndex = -1;

                
                ableLoadCodeData = true;

                foreach (DataRow row in dt.Rows)
                {
                    dt_Row = dt_Data.NewRow();
                    string itemCode = row[dalItem.ItemCode].ToString();
                    dt_Row[headerIndex] = index;
                    dt_Row[headerPartCode] = itemCode;
                    dt_Row[headerPartName] = row[dalItem.ItemName];

                    DateTime date = DateTime.MaxValue;
                    for (int i = 0; i < forecastMonthQty; i++)
                    {
                        //get month and year
                        //MessageBox.Show(getYear(dt_Data.Columns[3 + i].ColumnName));

                        string headerText = dt_Data.Columns[3 + i].ColumnName;
                        int year = Convert.ToInt32(getYear(headerText));
                        int month = DateTime.Parse("1." + getMonthName(headerText) + " 2008").Month;

                        //search data
                        DataRow data = tool.getItemForecastDataRow(dt_ItemForecast, itemCode, year, month);

                        if(data != null)
                        {
                            dt_Row[3 + i] = data[dalItemForecast.ForecastQty];

                            if(date == DateTime.MaxValue || date < Convert.ToDateTime(data[dalItemForecast.UpdatedDate]))
                            {
                                date = Convert.ToDateTime(data[dalItemForecast.UpdatedDate]);
                                dt_Row[headerUpdatedDate] = data[dalItemForecast.UpdatedDate];

                                int updatedID = int.TryParse(data[dalItemForecast.UpdatedBy].ToString(), out updatedID) ? updatedID : 0;

                                if (updatedID <= 0)
                                {
                                    dt_Row[headerUpdatedBy] = "ADMIN";
                                }
                                else
                                {
                                    dt_Row[headerUpdatedBy] = dalUser.getUsername(updatedID);
                                }
                            }  
                        }
                        else
                        {
                            dt_Row[3 + i] = 0;
                        }
                        
                    }

                    

                    dt_Data.Rows.Add(dt_Row);
                    index++;

                }


                if (dt_Data.Rows.Count > 0)
                {
                    dgvForecast.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dgvForecast.ColumnHeadersVisible = false;
                    dgvForecast.DataSource = dt_Data;
                    dgvForecastUIEdit(dgvForecast);
                    dgvForecast.ColumnHeadersVisible = true;
                    dgvForecast.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                    //dgvForecast.AutoResizeColumns();

                    //dgvForecast.Columns[headerUpdatedBy].Width = 500;
                    dgvForecast.ClearSelection();
                }
            }


            frmLoading.CloseForm();

            btnSearch.Enabled = true;

            Cursor = Cursors.Arrow;
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void btnFilter_Click(object sender, EventArgs e)
        {
            dgvForecast.SuspendLayout();
            string text = btnFilter.Text;

            if(text == textMoreFilters)
            {
                tlpForecast.RowStyles[1] = new RowStyle(SizeType.Absolute, 115f);

                dgvForecast.ResumeLayout();

                btnFilter.Text = textHideFilters;
            }
            else if(text == textHideFilters)
            {
                tlpForecast.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);

                dgvForecast.ResumeLayout();

                btnFilter.Text = textMoreFilters;
            }
            else
            {
                MessageBox.Show("Button Error!");
            }

        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            //cmbMonthFrom.DataSource = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToList();

            
            string yearFrom = cmbYearFrom.Text;

            if(cmbYearFrom.SelectedIndex != -1 && int.TryParse(yearFrom, out int i))
            {
                int year = Convert.ToInt32(yearFrom);
                cmbYearTo.DataSource = Enumerable.Range(year, DateTime.Now.Year - year + 2).ToList();
            }
            else
            {
                cmbYearTo.DataSource = null;
            }
            
        }

        private void lblPartNameReset_Click(object sender, EventArgs e)
        {
            cmbPartName.Text = null;
        }

        private void lblPartCodeReset_Click(object sender, EventArgs e)
        {
            cmbPartCode.Text = null;
        }

        private void lblYearReset_Click(object sender, EventArgs e)
        {
            cmbYearFrom.Text = null;
            cmbMonthFrom.Text = null;
            cmbYearTo.Text = null;
            cmbMonthTo.Text = null;
        }

        private void lblMonthReset_Click(object sender, EventArgs e)
        {
            cmbMonthFrom.Text = null;
        }

      

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = btnFilter.Text;

            if(text == textHideFilters)
            {
                InitializeFilterData();
            }

            dgvForecast.DataSource = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer.");
            }
            else if (Validation())
            {
                loadForecastListAndComboBox();
            }
           
        }

        private void lblYearToReset_Click(object sender, EventArgs e)
        {
            cmbYearTo.Text = null;
            cmbMonthTo.Text = null;
        }

        private void cmbYearTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            string text = cmbYearTo.Text;

            if(!string.IsNullOrEmpty(text) && cmbYearTo.SelectedIndex != -1)
            {
                cmbMonthTo.DataSource = CultureInfo.InvariantCulture.DateTimeFormat
                                                     .MonthNames.Take(12).ToList();

                cmbMonthTo.SelectedIndex = -1;

            }
            else
            {
                cmbMonthTo.DataSource = null;
            }
        }

        private void getRemainMonth()
        {

        }

        private bool Validation()
        {
            bool result = true;

            int yearFrom =  cmbYearFrom.SelectedIndex == -1 || string.IsNullOrEmpty(cmbYearFrom.Text) ? 9999: Convert.ToInt32(cmbYearFrom.Text);
            int yearTo = cmbYearTo.SelectedIndex == -1 || string.IsNullOrEmpty(cmbYearTo.Text) ? 9999 : Convert.ToInt32(cmbYearTo.Text);
            int monthFrom = cmbYearTo.SelectedIndex == -1 || string.IsNullOrEmpty(cmbMonthFrom.Text) ? 9999 : DateTime.Parse("1." + cmbMonthFrom.Text + " 2008").Month;
            int monthTo = cmbYearTo.SelectedIndex == -1 || string.IsNullOrEmpty(cmbMonthTo.Text) ? 9999 : DateTime.Parse("1." + cmbMonthTo.Text + " 2008").Month;

            if (yearTo < yearFrom)
            {
                result = false;
                errorProvider1.SetError(cmbYearTo, "Year to cannot less than year from");
            }

            if (monthTo < monthFrom && yearFrom == yearTo)
            {
                result = false;
                errorProvider2.SetError(cmbMonthTo, "Month to cannot less than month from");
            }

            return result;

        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                //CalTotalMonth();
                loadForecastList();
            }
        }

        private void dgvForecast_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //dgvForecast.AutoResizeColumns();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblMonthToReset_Click(object sender, EventArgs e)
        {
            cmbMonthTo.Text = null;
        }

        private void cmbMonthTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                loadForecastList();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbPartCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbEditMode_CheckedChanged(object sender, EventArgs e)
        {
            DataGridView dgv = dgvForecast;

            if (cbEditMode.Checked)
            {
                if(dgv.DataSource != null)
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.ReadOnly = false;

                    for (int i = 0; i < forecastMonthQty; i++)
                    {
                        dgv.Columns[3 + i].DefaultCellStyle.BackColor = SystemColors.Info;
                    }
                }
                else
                {
                    MessageBox.Show("No data.");
                    cbEditMode.Checked = false;
                }
                

            }
            else
            {
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.ReadOnly = true;

                if (dgv.DataSource != null)
                {
                    for (int i = 0; i < forecastMonthQty; i++)
                    {
                        dgv.Columns[3 + i].DefaultCellStyle.BackColor = Color.Gainsboro;
                    }
                }
                    
            }
        }

        private void cmbPartName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ableLoadCodeData)
            {
                string keywords = cmbPartName.Text;

                if (!string.IsNullOrEmpty(keywords))
                {
                    DataTable dt = dalItem.nameSearch(keywords);
                    DataTable dtItemCode = dt.DefaultView.ToTable(true, "item_code");

                    dtItemCode.DefaultView.Sort = "item_code ASC";
                    cmbPartCode.DataSource = dtItemCode;
                    cmbPartCode.DisplayMember = "item_code";
                }
                else
                {
                    cmbPartCode.DataSource = null;

                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        public void historyRecord(string customer, string yearFrom, string monthFrom, string yearTo, string monthTo ,string itemcode, string itemname)
        {
            //save history
            historyDAL dalHistory = new historyDAL();

            DataTable DB_History = dalHistory.ForecastEditHistorySelect();

            DataTable dt = DB_History.Clone();

            int yearFrom_INT = int.TryParse(yearFrom, out yearFrom_INT) ? yearFrom_INT : 0;
            int yearTo_INT = int.TryParse(yearTo, out yearTo_INT) ? yearTo_INT : 0;
            int monthFrom_INT = int.TryParse(monthFrom, out monthFrom_INT) ? monthFrom_INT : 0;
            int monthTo_INT = int.TryParse(monthTo, out monthTo_INT) ? monthTo_INT : 0;

            DateTime Start = DateTime.Parse(1 + monthFrom + yearFrom);
            DateTime End = DateTime.Parse(1 + monthTo + yearTo);

            if (DB_History != null)
            {
                foreach(DataRow row in DB_History.Rows)
                {
                    string historyDetail = row[dalHistory.HistoryDetail].ToString();

                    //get month and year and customer data
                    string historyCustomer = "";
                    string historyItem = "";
                    string historyMonthAndYear = "";
                    string historyMonth = "";
                    string historyYear = "";

                    bool gettingCustomerInfo = false;
                    bool gettingMonthAndYearInfo = false;
                    bool gettingItemInfo = false;

                    DateTime HistoryDate = DateTime.MaxValue;


                    for (int i = 0; i < historyDetail.Length; i++) 
                    {
                        if (historyDetail[i].ToString() == ":")
                        {
                            gettingItemInfo = false;
                        }

                        if (gettingItemInfo)
                        {
                            historyItem += historyDetail[i].ToString();
                        }

                        if (historyDetail[i].ToString() == "]")
                        {
                            gettingMonthAndYearInfo = false;
                            gettingItemInfo = true;
                        }

                        if (gettingMonthAndYearInfo)
                        {
                            historyMonthAndYear += historyDetail[i].ToString();
                        }

                        if (historyDetail[i].ToString() == "_")
                        {
                            gettingCustomerInfo = false;
                            gettingMonthAndYearInfo = true;
                        }

                        if (gettingCustomerInfo)
                        {
                            historyCustomer += historyDetail[i].ToString();
                        }

                        if (historyDetail[i].ToString() == "[") 
                        {
                            gettingCustomerInfo = true;
                        }
                    }

                    if(historyMonthAndYear.Length > 4)
                    {
                        for (int i = historyMonthAndYear.Length - 4; i < historyMonthAndYear.Length; i++)
                        {
                            historyYear += historyMonthAndYear[i].ToString();
                        }

                        historyMonth = historyMonthAndYear.Replace(historyYear, "");
                        HistoryDate = DateTime.TryParse(1.ToString() + "/" + historyMonth + "/" +historyYear, out DateTime test)? test : DateTime.MaxValue;

                    }




                    //date inspection
                    bool dataMatched = true;

                    dataMatched = historyItem.Contains(itemcode)? dataMatched : false;
                    dataMatched = historyCustomer == customer ? dataMatched : false;


                    dataMatched = HistoryDate >= Start && HistoryDate <= End ? dataMatched : false;

                    dataMatched = HistoryDate == DateTime.MaxValue ? false : dataMatched;


                    if (dataMatched)
                    {
                        dt.Rows.Add(row.ItemArray);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    frmUserActionHistory frm = new frmUserActionHistory(dt,true);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();//Item Edit
                }
                else
                {
                    MessageBox.Show("History not found.");
                }
            }
            else
            {
                MessageBox.Show("History not found.");
            }
        }


        private void dgvForecast_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string newForecast = dgvForecast.Rows[currentRow].Cells[currentColumn].Value.ToString();

            if(string.IsNullOrEmpty(newForecast))
            {
                newForecast = "0";
            }

            if (string.IsNullOrEmpty(oldForecast))
            {
                oldForecast = "0";
            }

            //get data
            if(!oldForecast.Equals(newForecast))
            {
                string headerText = dgvForecast.Columns[currentColumn].HeaderText;
                uItemForecast.cust_id = tool.getCustID(cmbCustomer.Text);
                uItemForecast.item_code = dgvForecast.Rows[currentRow].Cells[headerPartCode].Value.ToString();
                uItemForecast.forecast_year = Convert.ToInt32(getYear(headerText));
                uItemForecast.forecast_month = DateTime.Parse("1." + getMonthName(headerText) + " 2008").Month;
                uItemForecast.forecast_old_qty = float.TryParse(oldForecast, out float i)? Convert.ToSingle(oldForecast): 0;
                uItemForecast.forecast_qty = float.TryParse(newForecast, out float j) ? Convert.ToSingle(newForecast) : 0;
                uItemForecast.updated_date = DateTime.Now;
                uItemForecast.updated_by = MainDashboard.USER_ID;


                //insert or update data
                DataTable dt = dalItemForecast.ItemSelect(uItemForecast.item_code, uItemForecast.cust_id.ToString(), uItemForecast.forecast_year.ToString(), uItemForecast.forecast_month.ToString());
                bool success = false;
                if(dt.Rows.Count > 0)
                {
                    //update
                    success =  dalItemForecast.Update(uItemForecast);
                }
                else
                {
                    //insert
                    success = dalItemForecast.Insert(uItemForecast);
                }

                if(success)
                {
                    //DataTable dt_source = (DataTable)dgvForecast.DataSource;
                    //dgvForecast.Columns[headerUpdatedDate].ReadOnly = false;
                    //dt_source.Rows[currentRow][headerUpdatedDate] = uItemForecast.updated_date;
                    //dgvForecast.Rows[currentRow].Cells[headerUpdatedDate].ReadOnly = false;
                    dgvForecast.Rows[currentRow].Cells[headerUpdatedDate].Value = uItemForecast.updated_date;
                    dgvForecast.Rows[currentRow].Cells[headerUpdatedBy].Value = uItemForecast.updated_by;

                }
            }



        }

        private void frmForecast_NEW_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.forecastInputFormOpen = false;
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvForecast_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string itemCode = dgvForecast.Rows[e.RowIndex].Cells[headerPartCode].Value.ToString();
            string itemName = dgvForecast.Rows[e.RowIndex].Cells[headerPartName].Value.ToString();

            historyRecord(cmbCustomer.Text, cmbYearFrom.Text, cmbMonthFrom.Text, cmbYearTo.Text, cmbMonthTo.Text,itemCode,itemName);
        }

        private void dgvForecast_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

                if (dgvForecast.CurrentCell.ColumnIndex >= 3 && dgvForecast.CurrentCell.ColumnIndex < 3 + forecastMonthQty) //Desired Column
                {
                    currentColumn = dgvForecast.CurrentCell.ColumnIndex;
                    currentRow = dgvForecast.CurrentCell.RowIndex;
                    oldForecast = dgvForecast.CurrentCell.Value.ToString();
                    System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
                else
                {
                    System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;

                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }
    }
}
