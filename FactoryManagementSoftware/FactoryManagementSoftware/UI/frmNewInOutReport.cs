using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace FactoryManagementSoftware.UI
{
    public partial class frmNewInOutReport : Form
    {
        public frmNewInOutReport()
        {
            InitializeComponent();
            tool.loadCustomerAndAllToComboBox(cmbType);
        }

        #region Variable/Object Declare
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        userDAL dalUser = new userDAL();
        Tool tool = new Tool();
        Text text = new Text();

        readonly string totalColName = "TOTAL";
        readonly string totalInColName = "TOTAL IN";
        readonly string totalOutColName = "TOTAL OUT";

        DataGridViewAutoSizeColumnMode Fill = DataGridViewAutoSizeColumnMode.Fill;
        DataGridViewAutoSizeColumnMode DisplayedCells = DataGridViewAutoSizeColumnMode.DisplayedCells;

        #endregion

        #region UI

        private DataTable NewDailyDataTable()
        {
            DataTable dt = new DataTable();
            string day = null;
            DateTime start = dtpStart.Value.Date;
            DateTime end = dtpEnd.Value.Date;

            if (end < start)
            {
                MessageBox.Show("Date Error. Please pick a current date period.");
            }
            else
            {
                //if(cmbType.Text.Equals("All"))
                //{
                //    dt.Columns.Add("TYPE", typeof(string));
                //}
                dt.Columns.Add("#", typeof(string));
                dt.Columns.Add(dalItem.ItemCode, typeof(string));
                dt.Columns.Add(dalItem.ItemName, typeof(string));
                dt.Columns.Add("TOTAL", typeof(float));
                for (DateTime current = start; current <= end; current = current.AddDays(1))
                {
                    Console.WriteLine(current.Day);
                    day = current.ToString("dd/MM");
                    dt.Columns.Add(day, typeof(string));
                }
            }

            return dt;
        }

        private DataTable NewNonDailyDataTable()
        {
            DataTable dt = new DataTable();
            DateTime start = dtpStart.Value.Date;
            DateTime end = dtpEnd.Value.Date;

            if (end < start)
            {
                MessageBox.Show("Date Error. Please pick a current date period.");
            }
            else
            {
                dt.Columns.Add("#", typeof(string));
                dt.Columns.Add(dalItem.ItemCode, typeof(string));
                dt.Columns.Add(dalItem.ItemName, typeof(string));

                if (cbIn.Checked && !cbOut.Checked)
                {
                    dt.Columns.Add(totalInColName, typeof(float));
                }
                else if (!cbIn.Checked && cbOut.Checked)
                {
                    dt.Columns.Add(totalOutColName, typeof(float));
                }
                else if (cbIn.Checked && cbOut.Checked)
                {
                    dt.Columns.Add(totalInColName, typeof(float));
                    dt.Columns.Add(totalOutColName, typeof(float));
                }
            }

            return dt;
        }

        private void dgvDailyUIEdit(DataGridView dgv)
        {
            dgv.Columns[dalItem.ItemCode].HeaderText = "CODE";
            dgv.Columns[dalItem.ItemName].HeaderText = "NAME";


            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.Columns[dalItem.ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[dalItem.ItemName].MinimumWidth = 250;

            dgv.Columns[totalColName].Frozen = true;
        }

        private void dgvNonDailyUIEdit(DataGridView dgv)
        {
            dgv.Columns[dalItem.ItemCode].HeaderText = "CODE";
            dgv.Columns[dalItem.ItemName].HeaderText = "NAME";

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv.Columns[dalItem.ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dgvInOutReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (cbDaily.Checked)
            {
                DataGridView dgv = dgvInOutReport;
                dgv.SuspendLayout();
                int row = e.RowIndex;
                int col = e.ColumnIndex;

                if (dgv.Columns[e.ColumnIndex].Name != "#" && dgv.Columns[e.ColumnIndex].Name != dalItem.ItemName && dgv.Columns[e.ColumnIndex].Name != dalItem.ItemCode && dgv.Columns[e.ColumnIndex].Name != "TOTAL")
                {
                    if (dgv.Rows[row].Cells[col].Value != null)
                    {
                        if (!string.IsNullOrEmpty(dgv.Rows[row].Cells[col].Value.ToString()))
                        {
                            //MessageBox.Show(row.ToString() + "/" + col.ToString());
                            if (cbIn.Checked && !cbOut.Checked)//in only
                            {
                                dgv.Rows[row].Cells[col].Style.BackColor = Color.LightGreen;
                            }

                            else if (!cbIn.Checked && cbOut.Checked)//out only
                            {
                                dgv.Rows[row].Cells[col].Style.BackColor = Color.LightPink;
                            }
                        }
                        else
                        {
                            dgv.Rows[row].Cells[col].Style.BackColor = Color.White;
                        }
                    }

                }

                dgv.ResumeLayout();
            }

        }

        #endregion

        #region Data Proccessing

        private DataTable FilterInOROutData(DataTable dtForDGV, DataTable dtTrfHist)
        {
            string day = "ZERo";
            string itemCode = null;
            float qty = 0;
            float total = 0;
            DataRow dr = dtForDGV.NewRow();

            foreach (DataRow row in dtTrfHist.Rows)
            {
                if (row["trf_result"].ToString().Equals("Passed"))
                {
                    if (itemCode == null)
                    {
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());
                        total = qty;

                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                    else if (itemCode == row["trf_hist_item_code"].ToString())
                    {
                        if (day == Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM"))
                        {
                            qty += Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += Convert.ToSingle(row["trf_hist_qty"].ToString());
                        }
                        else
                        {
                            day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                            qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += qty;
                        }

                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                    else
                    {
                        dtForDGV.Rows.Add(dr);
                        total = 0;
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                        total = qty;

                        dr = dtForDGV.NewRow();
                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                }

            }
            if (dr != null)
                dtForDGV.Rows.Add(dr);

            return dtForDGV;
        }

        private DataTable FilterDailyMatInData(DataTable dtForDGV, DataTable dtTrfHist)
        {
            string day = "ZERo";
            string itemCode = null, trfFrom, trfTo;
            float qty = 0;
            float total = 0;
            DataRow dr = dtForDGV.NewRow();

            foreach (DataRow row in dtTrfHist.Rows)
            {
                trfFrom = row[dalTrfHist.TrfFrom].ToString();
                trfTo = row[dalTrfHist.TrfTo].ToString();

                if (row["trf_result"].ToString().Equals("Passed") && (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1))
                {
                    if (itemCode == null)
                    {
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());
                        total = qty;

                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                    else if (itemCode == row["trf_hist_item_code"].ToString())
                    {
                        if (day == Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM"))
                        {
                            qty += Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += Convert.ToSingle(row["trf_hist_qty"].ToString());
                        }
                        else
                        {
                            day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                            qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += qty;
                        }

                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                    else
                    {
                        dtForDGV.Rows.Add(dr);
                        total = 0;
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                        total = qty;

                        dr = dtForDGV.NewRow();
                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                }

            }
            if (dr != null)
                dtForDGV.Rows.Add(dr);

            return dtForDGV;
        }

        private DataTable FilterDailyPartOutData(DataTable dtForDGV, DataTable dtTrfHist)
        {
            string day = "ZERo";
            string itemCode = null, trfFrom, trfTo;
            float qty = 0;
            float total = 0;
            DataRow dr = dtForDGV.NewRow();

            foreach (DataRow row in dtTrfHist.Rows)
            {
                //string test = row["trf_hist_item_code"].ToString();

                //if(test.Equals("V84KM4300"))
                //{
                //    float testing = 0;
                //}
                trfFrom = row[dalTrfHist.TrfFrom].ToString();
                trfTo = row[dalTrfHist.TrfTo].ToString();

                if (row["trf_result"].ToString().Equals("Passed") && (tool.getFactoryID(trfFrom) != -1 && tool.getCustID(trfTo) != -1))
                {
                    if (itemCode == null)
                    {
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());
                        total = qty;

                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[day] = qty;
                        dr[totalColName] = total;
                    }

                    else if (itemCode == row["trf_hist_item_code"].ToString())
                    {
                        if (day == Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM"))
                        {
                            qty += Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += Convert.ToSingle(row["trf_hist_qty"].ToString());
                        }
                        else
                        {
                            day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                            qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += qty;
                        }

                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                    else
                    {

                        dtForDGV.Rows.Add(dr);

                        total = 0;
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                        total = qty;

                        dr = dtForDGV.NewRow();
                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                }

            }
            if (dr != null)
                dtForDGV.Rows.Add(dr);

            return dtForDGV;
        }

        private DataTable FilterDailyMatOutData(DataTable dtForDGV, DataTable dtTrfHist)
        {
            string day = "ZERo";
            string itemCode = null, trfFrom, trfTo;
            float qty = 0;
            float total = 0;
            DataRow dr = dtForDGV.NewRow();

            foreach (DataRow row in dtTrfHist.Rows)
            {
                trfFrom = row[dalTrfHist.TrfFrom].ToString();
                trfTo = row[dalTrfHist.TrfTo].ToString();

                if (row["trf_result"].ToString().Equals("Passed") && (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1))
                {
                    if (itemCode == null)
                    {
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());
                        total = qty;

                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                    else if (itemCode == row["trf_hist_item_code"].ToString())
                    {
                        if (day == Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM"))
                        {
                            qty += Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += Convert.ToSingle(row["trf_hist_qty"].ToString());
                        }
                        else
                        {
                            day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                            qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += qty;
                        }

                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                    else
                    {
 
                        dtForDGV.Rows.Add(dr);
       
                        total = 0;
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                        total = qty;

                        dr = dtForDGV.NewRow();
                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[day] = qty;
                        dr[totalColName] = total;
                    }
                }

            }
            if (dr != null)
                dtForDGV.Rows.Add(dr);

            return dtForDGV;
        }

        private DataTable FilterNonDailyPartOutData(DataTable dtForDGV, DataTable dtTrfHist)
        {
            string day = "ZERo";
            string itemCode = null, trfFrom, trfTo;
            float qty = 0;
            float total = 0;
            DataRow dr = dtForDGV.NewRow();

            foreach (DataRow row in dtTrfHist.Rows)
            {
                trfFrom = row[dalTrfHist.TrfFrom].ToString();
                trfTo = row[dalTrfHist.TrfTo].ToString();

                if (row["trf_result"].ToString().Equals("Passed") && tool.getFactoryID(trfFrom) == -1 && tool.getCustID(trfTo) != -1)
                {
                    if (itemCode == null)
                    {
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());

                        total = qty;

                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        //dr[day] = qty;
                        dr[totalOutColName] = total;
                    }
                    else if (itemCode == row["trf_hist_item_code"].ToString())
                    {
                        if (day == Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM"))
                        {
                            qty += Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += Convert.ToSingle(row["trf_hist_qty"].ToString());
                        }
                        else
                        {
                            day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                            qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += qty;
                        }

                        //dr[day] = qty;
                        dr[totalOutColName] = total;
                    }
                    else
                    {
                    
                        dtForDGV.Rows.Add(dr);
              
                        total = 0;
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                        total = qty;

                        dr = dtForDGV.NewRow();
                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        //dr[day] = qty;
                        dr[totalOutColName] = total;
                    }
                }
            }
            if (dr != null)
                dtForDGV.Rows.Add(dr);

            return dtForDGV;
        }

        private DataTable FilterNonDailyMatOutData(DataTable dtForDGV, DataTable dtTrfHist)
        {
            string day = "ZERo";
            string itemCode = null, trfFrom, trfTo;
            float qty = 0;
            float total = 0;
            DataRow dr = dtForDGV.NewRow();

            foreach (DataRow row in dtTrfHist.Rows)
            {
                trfFrom = row[dalTrfHist.TrfFrom].ToString();
                trfTo = row[dalTrfHist.TrfTo].ToString();

                if (row["trf_result"].ToString().Equals("Passed") && tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                {
                    if (itemCode == null)
                    {
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());

                        total = qty;

                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        //dr[day] = qty;
                        dr[totalOutColName] = total;
                    }
                    else if (itemCode == row["trf_hist_item_code"].ToString())
                    {
                        if (day == Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM"))
                        {
                            qty += Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += Convert.ToSingle(row["trf_hist_qty"].ToString());
                        }
                        else
                        {
                            day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                            qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += qty;
                        }

                        //dr[day] = qty;
                        dr[totalOutColName] = total;
                    }
                    else
                    {

                        dtForDGV.Rows.Add(dr);

                        total = 0;
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                        total = qty;

                        dr = dtForDGV.NewRow();
                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        //dr[day] = qty;
                        dr[totalOutColName] = total;
                    }
                }
            }
            if (dr != null)
                dtForDGV.Rows.Add(dr);

            return dtForDGV;
        }

        private DataTable FilterNonDailyInData(DataTable dtForDGV, DataTable dtTrfHist)
        {
            string day = "ZERo";
            string itemCode = null, trfFrom, trfTo;
            float qty = 0;
            float total = 0;
            DataRow dr = dtForDGV.NewRow();

            foreach (DataRow row in dtTrfHist.Rows)
            {
                trfFrom = row[dalTrfHist.TrfFrom].ToString();
                trfTo = row[dalTrfHist.TrfTo].ToString();

                if (row["trf_result"].ToString().Equals("Passed") && (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1))
                {
                    if (itemCode == null)
                    {
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");

                        qty = Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());

                        total = qty;

                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        //dr[day] = qty;
                        dr[totalInColName] = total;
                    }
                    else if (itemCode == row["trf_hist_item_code"].ToString())
                    {
                        if (day == Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM"))
                        {
                            qty += Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += Convert.ToSingle(row["trf_hist_qty"].ToString());
                        }
                        else
                        {
                            day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                            qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                            total += qty;
                        }

                        //dr[day] = qty;
                        dr[totalInColName] = total;
                    }
                    else
                    {
                      
                        dtForDGV.Rows.Add(dr);
             
                        total = 0;
                        itemCode = row["trf_hist_item_code"].ToString();
                        day = Convert.ToDateTime(row["trf_hist_trf_date"].ToString()).ToString("dd/MM");
                        qty = Convert.ToSingle(row["trf_hist_qty"].ToString());
                        total = qty;

                        dr = dtForDGV.NewRow();
                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        //dr[day] = qty;
                        dr[totalInColName] = total;
                    }
                }
            }
            if (dr != null)
                dtForDGV.Rows.Add(dr);

            return dtForDGV;
        }

        private DataTable FilterNonDailyInAndOutPartData(DataTable dtForDGV, DataTable dtTrfHist)
        {
            string itemCode = null;
            string trfFrom, trfTo;
            float totalIn = 0, totalOut = 0, qty = 0;
            DataRow dr = dtForDGV.NewRow();
           
            foreach (DataRow row in dtTrfHist.Rows)
            {
                trfFrom = row[dalTrfHist.TrfFrom].ToString();
                trfTo = row[dalTrfHist.TrfTo].ToString();

                if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && ((tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)||(tool.getFactoryID(trfFrom) != -1 && tool.getCustID(trfTo) != -1)))
                {
                    

                    qty = Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());

                    if (itemCode == null)
                    {
                        itemCode = row[dalTrfHist.TrfItemCode].ToString();

                        if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                        {
                            //from non-factory to factory: IN
                            totalIn += qty;
                        }

                        //from factory to non-factory: OUT
                        else if (tool.getFactoryID(trfFrom) != -1 && tool.getCustID(trfTo) != -1)
                        {
                            totalOut += qty;
                        }

                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[totalInColName] = totalIn;
                        dr[totalOutColName] = totalOut;
                    }
                    else if (itemCode == row[dalTrfHist.TrfItemCode].ToString())
                    {
                        if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                        {
                            //from non-factory to factory: IN
                            totalIn += qty;
                        }

                        //from factory to non-factory: OUT
                        else if (tool.getFactoryID(trfFrom) != -1 && tool.getCustID(trfTo) != -1)
                        {
                            totalOut += qty;
                        }
                        dr[totalInColName] = totalIn;
                        dr[totalOutColName] = totalOut;
                    }
                    else
                    {
           
                        dtForDGV.Rows.Add(dr);
      
                        totalIn = 0;
                        totalOut = 0;
                        itemCode = row[dalTrfHist.TrfItemCode].ToString();

                        if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                        {
                            //from non-factory to factory: IN
                            totalIn += qty;
                        }

                        //from factory to non-factory: OUT
                        else if (tool.getFactoryID(trfFrom) != -1 && tool.getCustID(trfTo) != -1)
                        {
                            totalOut += qty;
                        }

                        dr = dtForDGV.NewRow();
                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[totalInColName] = totalIn;
                        dr[totalOutColName] = totalOut;
                    }
                }
            }
            if (dr != null)
                dtForDGV.Rows.Add(dr);

            return dtForDGV;
        }

        private DataTable FilterNonDailyInAndOutMatData(DataTable dtForDGV, DataTable dtTrfHist)
        {
            string itemCode = null;
            string trfFrom, trfTo;
            float totalIn = 0, totalOut = 0, qty = 0;
            DataRow dr = dtForDGV.NewRow();

            foreach (DataRow row in dtTrfHist.Rows)
            {
                trfFrom = row[dalTrfHist.TrfFrom].ToString();
                trfTo = row[dalTrfHist.TrfTo].ToString();

                if (row[dalTrfHist.TrfResult].ToString().Equals("Passed") && ((tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1) || (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1)))
                {
                    
                    qty = Convert.ToSingle(row[dalTrfHist.TrfQty].ToString());

                    if (itemCode == null)
                    {
                        itemCode = row[dalTrfHist.TrfItemCode].ToString();

                        if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                        {
                            //from non-factory to factory: IN
                            totalIn = qty;
                        }

                        //from factory to non-factory: OUT
                        else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1)
                        {
                            totalOut = qty;
                        }

                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[totalInColName] = totalIn;
                        dr[totalOutColName] = totalOut;
                    }
                    else if (itemCode == row[dalTrfHist.TrfItemCode].ToString())
                    {
                        if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                        {
                            //from non-factory to factory: IN
                            totalIn += qty;
                        }

                        //from factory to non-factory: OUT
                        else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1)
                        {
                            totalOut += qty;
                        }
                        dr[totalInColName] = totalIn;
                        dr[totalOutColName] = totalOut;
                    }
                    else
                    {
                        dtForDGV.Rows.Add(dr);
                        totalIn = 0;
                        totalOut = 0;
                        itemCode = row[dalTrfHist.TrfItemCode].ToString();

                        if (tool.getFactoryID(trfFrom) == -1 && tool.getFactoryID(trfTo) != -1)
                        {
                            //from non-factory to factory: IN
                            totalIn = qty;
                        }

                        //from factory to non-factory: OUT
                        else if (tool.getFactoryID(trfFrom) != -1 && tool.getFactoryID(trfTo) == -1)
                        {
                            totalOut = qty;
                        }

                        dr = dtForDGV.NewRow();
                        dr[dalItem.ItemCode] = itemCode;
                        dr[dalItem.ItemName] = dalItem.getItemName(itemCode);
                        dr[totalInColName] = totalIn;
                        dr[totalOutColName] = totalOut;
                    }
                }
            }

            if (dr != null)
                dtForDGV.Rows.Add(dr);

            return dtForDGV;
        }

        private void FilterDailyData()
        {
            DataTable dtSourceForDGV = NewDailyDataTable();
            DataTable dtTrfHist = new DataTable();
            string Type = cmbType.Text;
            string start = dtpStart.Value.ToString("yyyy/MM/dd");
            string end = dtpEnd.Value.ToString("yyyy/MM/dd");

            //in only
            if (cbIn.Checked && !cbOut.Checked)
            {
                if (cbPart.Checked && !cbSearch.Checked)
                {
                    if (Type.Equals("All"))
                    {
                        //dtTrfHist = dalTrfHist.rangePartTrfSearch(start, end);
                        dtTrfHist = dalTrfHist.rangeAllItemProductionSearch(start, end);
                    }
                    else
                    {
                        //if customer: show selected customer's part only
                        dtTrfHist = dalTrfHist.rangeItemProductionSearch(start, end, Type);
                    }
                }
                else if (cbMat.Checked && !cbSearch.Checked)
                {
                    //show material transfer history during selected period
                    dtTrfHist = dalTrfHist.rangeMaterialTrfSearch(start, end, Type);
                }
                else if(cbSearch.Checked)
                {
                    if (cbPart.Checked)
                    {
                        dtTrfHist = dalTrfHist.rangePartTrfKeywordSearch(txtSearch.Text, start, end);
                    }
                    else
                    {
                        dtTrfHist = dalTrfHist.rangeMatTrfKeywordSearch(txtSearch.Text, start, end);
                    }
                }

                dtSourceForDGV = FilterDailyMatInData(dtSourceForDGV, dtTrfHist);
            }

            //out only
            else if (!cbIn.Checked && cbOut.Checked)
            {
                if (cbPart.Checked && !cbSearch.Checked)
                {
                    if (Type.Equals("All"))
                    {
                        //dtTrfHist = dalTrfHist.rangePartTrfSearch(start, end);
                        dtTrfHist = dalTrfHist.rangeItemToAllCustomerSearch(start, end);
                    }
                    else
                    {
                        //if customer: show selected customer's part only
                        dtTrfHist = dalTrfHist.rangeItemToCustomerSearch(Type, start, end);
                    }
                    dtSourceForDGV = FilterDailyPartOutData(dtSourceForDGV, dtTrfHist);
                }
                else if (cbMat.Checked && !cbSearch.Checked)
                {
                    //show material transfer history during selected period
                    dtTrfHist = dalTrfHist.rangeMaterialTrfSearch(start, end, Type);
                    dtSourceForDGV = FilterDailyMatOutData(dtSourceForDGV, dtTrfHist);
                }
                else if (cbSearch.Checked)
                {
                    if (cbPart.Checked)
                    {
                        dtTrfHist = dalTrfHist.rangePartTrfKeywordSearch(txtSearch.Text, start, end);
                        dtSourceForDGV = FilterDailyPartOutData(dtSourceForDGV, dtTrfHist);
                    }
                    else
                    {
                        dtTrfHist = dalTrfHist.rangeMatTrfKeywordSearch(txtSearch.Text, start, end);
                        dtSourceForDGV = FilterDailyMatOutData(dtSourceForDGV, dtTrfHist);
                    }
                }

                
            }
            //in & out
            else if (cbIn.Checked && cbOut.Checked)
            {

            }

            if (dtSourceForDGV.Rows.Count > 0)
            {
                DataGridView dgv = dgvInOutReport;
                dgv.DataSource = null;

                DataView dv = dtSourceForDGV.DefaultView;
                dv.Sort = "item_name ASC";
                DataTable sortedDT = dv.ToTable();

                int indexNo = 1;
                foreach (DataRow row in sortedDT.Rows)
                {
                    row["#"] = indexNo;
                    indexNo++;
                }

                dgv.DataSource = sortedDT;
                dgvDailyUIEdit(dgv);

                dgv.ClearSelection();
                tool.DoubleBuffered(dgv, true);
            }

            else
            {
                lblNoData.Visible = true;
            }
        }

        private void FilterNonDailyData()
        {
            DataTable dtSourceForDGV = NewNonDailyDataTable();
            DataTable dtTrfHist = new DataTable();
            string Type = cmbType.Text;
            string start = dtpStart.Value.ToString("yyyy/MM/dd");
            string end = dtpEnd.Value.ToString("yyyy/MM/dd");

            itemCustDAL dalItemCust = new itemCustDAL();
            //in only
            if (cbIn.Checked && !cbOut.Checked)
            {
                if (cbPart.Checked && !cbSearch.Checked)
                {
                    if (Type.Equals("All"))
                    {
                        //dtTrfHist = dalTrfHist.rangePartTrfSearch(start, end);
                        dtTrfHist = dalTrfHist.rangeAllItemProductionSearch(start, end);
                    }
                    else
                    {
                        //if customer: show selected customer's part only
                        dtTrfHist = dalTrfHist.rangeItemProductionSearch(start, end, Type);
                    }
                }
                else if (cbMat.Checked && !cbSearch.Checked)
                {
                    //show material transfer history during selected period
                   
                    dtTrfHist = dalTrfHist.rangeMaterialTrfSearch(start, end, Type);
                  
                }
                else if (cbSearch.Checked)
                {
                    if (cbPart.Checked)
                    {
                        dtTrfHist = dalTrfHist.rangePartTrfKeywordSearch(txtSearch.Text, start, end);
                    }
                    else
                    {
                        dtTrfHist = dalTrfHist.rangeMatTrfKeywordSearch(txtSearch.Text, start, end);
                    }
                }

                dtSourceForDGV = FilterNonDailyInData(dtSourceForDGV, dtTrfHist);
            }

            //out only
            else if (!cbIn.Checked && cbOut.Checked)
            {
                if (cbPart.Checked && !cbSearch.Checked)
                {
                    if (Type.Equals("All"))
                    {
                        //dtTrfHist = dalTrfHist.rangePartTrfSearch(start, end);
                        dtTrfHist = dalTrfHist.rangeItemToAllCustomerSearch(start, end);
                    }
                    else
                    {
                        //if customer: show selected customer's part only
                        dtTrfHist = dalTrfHist.rangeItemToCustomerSearch(Type, start, end);
                    }
                    dtSourceForDGV = FilterNonDailyPartOutData(dtSourceForDGV, dtTrfHist);
                }
                else if (cbMat.Checked && !cbSearch.Checked)
                {
                    //show material transfer history during selected period
                    dtTrfHist = dalTrfHist.rangeMaterialTrfSearch(start, end, Type);
                    dtSourceForDGV = FilterNonDailyMatOutData(dtSourceForDGV, dtTrfHist);
                }
                else if (cbSearch.Checked)
                {
                    if (cbPart.Checked)
                    {
                        dtTrfHist = dalTrfHist.rangePartTrfKeywordSearch(txtSearch.Text, start, end);
                        dtSourceForDGV = FilterNonDailyPartOutData(dtSourceForDGV, dtTrfHist);
                    }
                    else
                    {
                        dtTrfHist = dalTrfHist.rangeMatTrfKeywordSearch(txtSearch.Text, start, end);
                        dtSourceForDGV = FilterNonDailyMatOutData(dtSourceForDGV, dtTrfHist);
                    }
                }

                
            }

            //in & out
            else if (cbIn.Checked && cbOut.Checked)
            {
                if (cbPart.Checked && !cbSearch.Checked)
                {
                    if (Type.Equals("All"))
                    {

                        dtTrfHist = dalTrfHist.rangePartTrfSearch(start, end);
                    }
                    else
                    {
                        //if customer: show selected customer's part only

                        dtTrfHist = dalTrfHist.rangePartTrfSearch(start, end, tool.getCustID(Type));
                    }

                    dtSourceForDGV = FilterNonDailyInAndOutPartData(dtSourceForDGV, dtTrfHist);
                }
                else if (cbMat.Checked && !cbSearch.Checked)
                {
                    //show material transfer history during selected period
                    dtTrfHist = dalTrfHist.rangeMaterialTrfSearch(start, end, Type);
                    dtSourceForDGV = FilterNonDailyInAndOutMatData(dtSourceForDGV, dtTrfHist);
                }
                else if (cbSearch.Checked)
                {
                    if(cbPart.Checked)
                    {
                        dtTrfHist = dalTrfHist.rangePartTrfKeywordSearch(txtSearch.Text, start, end);
                        dtSourceForDGV = FilterNonDailyInAndOutPartData(dtSourceForDGV, dtTrfHist);
                    }
                    else
                    {
                        dtTrfHist = dalTrfHist.rangeMatTrfKeywordSearch(txtSearch.Text, start, end);
                        dtSourceForDGV = FilterNonDailyInAndOutMatData(dtSourceForDGV, dtTrfHist);
                    }
                }
            }

            if (dtSourceForDGV.Rows.Count > 0)
            {
                DataGridView dgv = dgvInOutReport;
                dgv.DataSource = null;

                DataView dv = dtSourceForDGV.DefaultView;
                dv.Sort = "item_name ASC";
                DataTable sortedDT = dv.ToTable();

                int indexNo = 1;
                foreach (DataRow row in sortedDT.Rows)
                {
                    row["#"] = indexNo;
                    indexNo++;
                }

                dgv.DataSource = sortedDT;
                dgvNonDailyUIEdit(dgv);

                dgv.ClearSelection();
                tool.DoubleBuffered(dgv, true);
            }

            else
            {
                lblNoData.Visible = true;
            }
        }

        #endregion

        #region Click

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                lblNoData.Visible = false;
                dgvInOutReport.DataSource = null;
                dgvInOutReport.Columns.Clear();

                if (!string.IsNullOrEmpty(cmbType.Text) && !cbSearch.Checked)
                {
                    //daily data
                    if (cbDaily.Checked)
                    {
                        FilterDailyData();
                    }

                    //total data
                    else
                    {
                        FilterNonDailyData();
                    }
                }
                else if(cbSearch.Checked)
                {
                    string keywords = txtSearch.Text;

                    DataTable dt = dalItem.Search(keywords);
                    if(dt.Rows.Count <= 0)
                    {
                        lblInvalidSearch.Show();
                    }
                    else
                    {
                        lblInvalidSearch.Hide();
                        if (cbDaily.Checked)
                        {
                            FilterDailyData();
                        }

                        //total data
                        else
                        {
                            FilterNonDailyData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
            finally
            {
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        #endregion

        #region export to excel

        private string setFileName()
        {
            string fileName = "Test.xls";
            string title = cmbType.Text;
            DateTime currentDate = DateTime.Now;

            if (cbDaily.Checked)
            {
                title += "_Daily";
            }
            else
            {
                title += "_Total";
            }

            if (!cbIn.Checked && cbOut.Checked)
            {
                title += "_OutReport";
            }
            else if (cbIn.Checked && !cbOut.Checked)
            {
                title += "_IntReport";
            }
            else if(cbIn.Checked && cbOut.Checked)
            {
                title += "_InOutReport";
            }

            fileName = title + "(From_" + dtpStart.Text + "_To_" + dtpEnd.Text + ")_" + currentDate.ToString("ddMMyyyy_HHmmss") + ".xls";

            return fileName;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string path = @"D:\StockAssistant\Document\InOutReport";
                Directory.CreateDirectory(path);
                sfd.InitialDirectory = path;
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = setFileName();

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    tool.historyRecord(text.Excel, text.getExcelString(sfd.FileName), DateTime.Now, MainDashboard.USER_ID);
                    // Copy DataGridView results to clipboard
                    copyAlltoClipboard();
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Excel.Application xlexcel = new Microsoft.Office.Interop.Excel.Application();
                    xlexcel.PrintCommunication = false;
                    xlexcel.ScreenUpdating = false;
                    xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    xlexcel.Calculation = XlCalculation.xlCalculationManual;
                    Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    if (string.IsNullOrEmpty(cmbType.Text))
                    {
                        if (!string.IsNullOrEmpty(cmbType.Text))
                        {
                            xlWorkSheet.Name = cmbType.Text;
                        }
                        else
                        {
                            xlWorkSheet.Name = "NAME NOT FOUND";
                        }

                    }
                    else
                    {
                        xlWorkSheet.Name = cmbType.Text;
                    }


                    #region Save data to Sheet

                    string title = cmbType.Text;

                    if (cbDaily.Checked)
                    {
                        title += "_Daily";
                    }
                    else
                    {
                        title += "_Total";
                    }

                    if (!cbIn.Checked && cbOut.Checked)
                    {
                        title += "_OutReport";
                    }
                    else if (cbIn.Checked && !cbOut.Checked)
                    {
                        title += "_IntReport";
                    }
                    else if (cbIn.Checked && cbOut.Checked)
                    {
                        title += "_InOutReport";
                    }

                    xlWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&16 (" + dtpStart.Text + "-" + dtpEnd.Text + ")" + title;

                    //Header and Footer setup
                    xlWorkSheet.PageSetup.LeftHeader = "&\"Calibri,Bold\"&11 " + DateTime.Now.Date.ToString("dd/MM/yyyy"); ;
                    xlWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&11 PG -&P";
                    xlWorkSheet.PageSetup.CenterFooter = "Printed By " + dalUser.getUsername(MainDashboard.USER_ID);

                    //Page setup
                    xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;


                    if (cbIn.Checked && cbOut.Checked)
                    {
                        xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                    }
                    else
                    {
                        xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    }

                    xlWorkSheet.PageSetup.Zoom = false;
                    xlWorkSheet.PageSetup.CenterHorizontally = true;
                    xlWorkSheet.PageSetup.LeftMargin = 1;
                    xlWorkSheet.PageSetup.RightMargin = 1;
                    xlWorkSheet.PageSetup.FitToPagesWide = 1;
                    xlWorkSheet.PageSetup.FitToPagesTall = false;
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
                    tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    tRange.Borders.Weight = XlBorderWeight.xlThin;
                    tRange.Font.Size = 11;
                    tRange.Font.Name = "Calibri";
                    tRange.EntireColumn.AutoFit();
                    tRange.EntireRow.AutoFit();
                    tRange.Rows[1].interior.color = Color.FromArgb(237, 237, 237);

                    #endregion

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
                    dgvInOutReport.ClearSelection();

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
        }

        private void copyAlltoClipboard()
        {
            dgvInOutReport.SelectAll();
            DataObject dataObj = dgvInOutReport.GetClipboardContent();
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
 
        #region Changed

        private void cbPart_CheckedChanged(object sender, EventArgs e)
        {
            dgvInOutReport.DataSource = null;
            cmbType.DataSource = null;
            if (cbPart.Checked)
            {
                cbMat.Checked = false;
                //cbSearch.Checked = false;
                tool.loadCustomerAndAllToComboBox(cmbType);

                if (cbSearch.Checked)
                {
                    txtSearch.Show();
                    cmbType.Hide();
                }
                else
                {
                    txtSearch.Hide();
                    cmbType.Show();
                }
            }
            else
            {
                cbMat.Checked = true;
            }
        }

        private void cbMat_CheckedChanged(object sender, EventArgs e)
        {
            cmbType.DataSource = null;
            if (cbMat.Checked)
            {
                cbPart.Checked = false;
                //cbSearch.Checked = false;
                tool.loadMaterialAndAllToComboBox(cmbType);

                if (cbSearch.Checked)
                {
                    txtSearch.Show();
                    cmbType.Hide();
                }
                else
                {
                    txtSearch.Hide();
                    cmbType.Show();
                }
                
            }
            else
            {
                cbPart.Checked = true;
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvInOutReport.DataSource = null;
        }

        private void cbOut_CheckedChanged(object sender, EventArgs e)
        {
            dgvInOutReport.DataSource = null;

            //if (!cbDaily.Checked)
            //{
            //    cbOut.Checked = true;
            //}

            if (cbIn.Checked && cbOut.Checked)
            {
                cbDaily.Checked = false;
            }
        }

        private void cbIn_CheckedChanged(object sender, EventArgs e)
        {
            dgvInOutReport.DataSource = null;
            if (cbIn.Checked && cbOut.Checked)
            {
                cbDaily.Checked = false;
            }
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            dgvInOutReport.DataSource = null;
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            dgvInOutReport.DataSource = null;
        }

        private void cbDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDaily.Checked)
            {
                if (cbOut.Checked)
                {
                    cbIn.Checked = false;
                }
            }
        }

        private void frmNewInOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainDashboard.InOutReportFormOpen = false;
        }

        private void cbSearch_CheckedChanged(object sender, EventArgs e)
        {
            cmbType.DataSource = null;
            if (cbSearch.Checked)
            {
                txtSearch.Show();
                cmbType.Hide();
            }
            else
            {
                txtSearch.Hide();
                cmbType.Show();

                cmbType.DataSource = null;
                if (cbPart.Checked)
                {
                    tool.loadCustomerAndAllToComboBox(cmbType);
                }
                else
                {
                    tool.loadMaterialAndAllToComboBox(cmbType);
                }
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text.Equals("SEARCH"))
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Equals(""))
            {
                txtSearch.Text = "SEARCH";
                txtSearch.ForeColor = Color.LightGray;
            }
        }

        #endregion

        private void dgvInOutReport_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
           
        }
    }
}
