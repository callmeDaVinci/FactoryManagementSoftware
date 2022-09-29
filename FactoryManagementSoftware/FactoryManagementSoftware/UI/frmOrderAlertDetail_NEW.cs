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
using Font = System.Drawing.Font;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections;
using Microsoft.Office.Interop.Word;
using Syncfusion.XlsIO.Parser.Biff_Records;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using System.DirectoryServices.ActiveDirectory;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderAlertDetail_NEW : Form
    {
        public frmOrderAlertDetail_NEW(DataTable dt_Product_Info, DataTable dt_materialSummary ,string matCode, DateTime dateFrom, DateTime dateTo)
        {
            InitializeComponent();
            DT_PRODUCT = dt_Product_Info;
            DT_MATERIAL_SUMMARY = dt_materialSummary;
            MAT_CODE = matCode;
            DATE_FROM = dateFrom;
            DATE_TO = dateTo;

            pageSetup();

        }

        #region variable 

        private string MAT_CODE;
        DataTable DT_PRODUCT;
        DataTable DT_MATERIAL_SUMMARY;
        DateTime DATE_FROM;
        DateTime DATE_TO;

        private string string_Forecast = " FORECAST";
        private string string_StillNeed = " STILL NEED";
        private string string_Delivered = " DELIVERED";
        private string string_EstBalance = " EST. BAL.";

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        joinDAL dalJoin = new joinDAL();
        userDAL dalUser = new userDAL();

        custDAL dalCust = new custDAL();
        materialDAL dalMat = new materialDAL();
        dataTrfBLL uData = new dataTrfBLL();

        Tool tool = new Tool();

        Text text = new Text();

        #endregion

        private void dgvUIEdit(DataGridView dgv)
        {
            if (dgv == dgvMaterialForecastSummary)
            {
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                //dgv.Columns[text.Header_PartName].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);

                dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_PartCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);
                dgv.Columns[text.Header_Type].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                dgv.Columns[text.Header_ReadyStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (DT_MATERIAL_SUMMARY != null)
                {
                    foreach (DataColumn col in DT_MATERIAL_SUMMARY.Columns)
                    {
                        string colName = col.ColumnName;

                        if (colName.Contains(string_Forecast) || colName.Contains(string_Delivered) || colName.Contains(string_StillNeed))
                        {
                            dgv.Columns[colName].Visible = false;
                        }
                        else if (colName.Contains(string_EstBalance))
                        {
                            dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }

                dgv.Columns[text.Header_Unit].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

                dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_PendingOrder].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgv.Columns[text.Header_PartName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            }

        }

        private void pageSetup()
        {
            dgvMaterialForecastSummary.DataSource = null;

            if(DT_MATERIAL_SUMMARY != null)
            {
                dgvMaterialForecastSummary.DataSource = DT_MATERIAL_SUMMARY;
                dgvUIEdit(dgvMaterialForecastSummary);
                dgvMaterialForecastSummary.ClearSelection();
            }
        }

        private void cmbDateSetup()
        {
            int monthFrom = DATE_FROM.Month;

            for (var date = DATE_FROM; date <= DATE_TO; date = date.AddMonths(1))
            {
                var i = date.Year;
                var j = date.Month;

                if (date == DATE_FROM && j < monthFrom)
                {
                    date = date.AddMonths(1);
                    i = date.Year;
                    j = date.Month;
                    date = new DateTime(i, j, 1);
                }

                MonthlyForecast = j + "/" + i + string_Forecast;
            }
        }

        private void dgvMaterialForecastSummary_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMaterialForecastSummary;
            dgv.SuspendLayout();

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            string colName = dgv.Columns[col].Name;

            if (colName == text.Header_MatCode)
            {
                string matCode = dgv.Rows[row].Cells[text.Header_MatCode].Value.ToString();

                if (matCode == "")
                {
                    dgv.Rows[row].Height = 3;
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.LightGray;
                    //dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
                }
                else
                {
                    dgv.Rows[row].Height = 50;
                }
            }
            else if (colName.Contains(string_EstBalance))
            {
                float bal = float.TryParse(dgv.Rows[row].Cells[colName].Value.ToString(), out float x) ? x : 0;

                if (bal < 0)
                {
                    dgv.Rows[row].Cells[colName].Style.ForeColor = Color.Red;
                }
                else
                {
                    dgv.Rows[row].Cells[colName].Style.ForeColor = Color.Black;

                }
            }
            dgv.ResumeLayout();

            dgvMaterialForecastSummary.ClearSelection();
        }
    }
}
