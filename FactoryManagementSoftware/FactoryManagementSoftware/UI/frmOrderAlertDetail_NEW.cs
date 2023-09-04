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
using System.ComponentModel.Design;
using CheckBox = System.Windows.Forms.CheckBox;

namespace FactoryManagementSoftware.UI
{
    public partial class frmOrderAlertDetail_NEW : Form
    {
        public frmOrderAlertDetail_NEW(DataTable dt_Product_Info, DataTable dt_Balance_Info ,string matCode, DateTime dateFrom, DateTime dateTo, bool deductUsedStock)
        {
            InitializeComponent();

            DEDUCT_STOCK = deductUsedStock;
            DT_PRODUCT = dt_Product_Info;
            DT_BALANCE_INFO = dt_Balance_Info;
            MAT_CODE = matCode;
            DATE_FROM = dateFrom;
            DATE_TO = dateTo;

            tool.DoubleBuffered(dgvMaterialForecastInfo, true);
            pageSetup();

        }

        #region variable 

        private string MAT_CODE;
        private string HIGHLIGHTED_ITEM;
        DataTable DT_PRODUCT;
        DataTable DT_BALANCE_INFO;
        DataTable DT_FULLDETAIL;
        DateTime DATE_FROM;
        DateTime DATE_TO;
        private bool DEDUCT_STOCK;

        //private string string_Forecast = " FORECAST";
        //private string string_StillNeed = " STILL NEED";
        //private string string_Delivered = " DELIVERED";

        //private string string_Product_Forecast = " PRODUCT FORECAST";
        //private string string_Product_StillNeed = " PRODUCT STILL NEED";
        //private string string_Product_Delivered = " PRODUCT DELIVERED";

        //private string string_EstBalance = " EST. BAL.";

        itemForecastDAL dalItemForecast = new itemForecastDAL();
        itemForecastBLL uItemForecast = new itemForecastBLL();
        itemCustDAL dalItemCust = new itemCustDAL();
        itemDAL dalItem = new itemDAL();
        trfHistDAL dalTrfHist = new trfHistDAL();
        pmmaDateDAL dalPmmaDate = new pmmaDateDAL();
        joinDAL dalJoin = new joinDAL();
        userDAL dalUser = new userDAL();

        custSupplierDAL dalCust = new custSupplierDAL();
        materialDAL dalMat = new materialDAL();
        dataTrfBLL uData = new dataTrfBLL();

        bool PageLoaded = false;

        Tool tool = new Tool();

        Text text = new Text();

        #endregion

        private DataTable MaterialForecastSummaryTable(String month)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_DirectUseOn, typeof(string));
            dt.Columns.Add(text.Header_ReadyStock, typeof(float));

            dt.Columns.Add(month + text.str_RequiredQty, typeof(float));
            dt.Columns.Add(month + text.str_InsufficientQty, typeof(float));

            dt.Columns.Add(text.Header_PartWeight_G, typeof(float));
            dt.Columns.Add(text.Header_RunnerWeight_G, typeof(float));
            dt.Columns.Add(text.Header_Parent_Weight_G_Piece, typeof(float));

            dt.Columns.Add(text.Header_JoinQty, typeof(float));
            dt.Columns.Add(text.Header_JoinMax, typeof(float));
            dt.Columns.Add(text.Header_JoinMin, typeof(float));
            dt.Columns.Add(text.Header_JoinRatio_Product_Mat, typeof(string));

            dt.Columns.Add(text.Header_WastageAllowed_Percentage, typeof(float));
            dt.Columns.Add(text.Header_ColorRate, typeof(float));

            dt.Columns.Add(text.Header_MaterialRequiredIncludedWastage, typeof(float));

            dt.Columns.Add(text.Header_Unit, typeof(string));
            return dt;
        }

        private DataTable FullDetailTable(String month)
        {
            DataTable dt = new DataTable();

            //to hide

            dt.Columns.Add(text.Header_IndexMarking, typeof(int));
            dt.Columns.Add(text.Header_ParentIndex, typeof(int));
            dt.Columns.Add(text.Header_GroupLevel, typeof(int));
            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));
            dt.Columns.Add(text.Header_PartWeight_G, typeof(float));
            dt.Columns.Add(text.Header_RunnerWeight_G, typeof(float));
            dt.Columns.Add(text.Header_JoinQty, typeof(float));
            dt.Columns.Add(text.Header_JoinMax, typeof(float));
            dt.Columns.Add(text.Header_JoinMin, typeof(float));
            dt.Columns.Add(text.Header_JoinWastage, typeof(float));


            //to show
            dt.Columns.Add(text.Header_Customer, typeof(string));

            dt.Columns.Add(text.Header_Index, typeof(int));

            dt.Columns.Add(text.Header_ItemNameAndCode, typeof(string));
            dt.Columns.Add(text.Header_ReadyStock, typeof(float));

            dt.Columns.Add(month + text.str_Forecast, typeof(float));
            dt.Columns.Add(month + text.str_Delivered, typeof(float));
            dt.Columns.Add(month + text.str_RequiredQty, typeof(float));

            dt.Columns.Add(text.Header_ItemWeight_G_Piece, typeof(float));
            dt.Columns.Add(text.Header_ColorRate, typeof(float));

            dt.Columns.Add(text.Header_JoinRatio_Product_Mat, typeof(string));
            dt.Columns.Add(text.Header_WastageAllowed_Percentage, typeof(float));

            dt.Columns.Add(month + text.str_EstBalance, typeof(float));
            dt.Columns.Add(text.Header_Unit, typeof(string));

            return dt;
        }

        private void dgvUIEdit(DataGridView dgv)
        {
            if (dgv == dgvMaterialForecastInfo)
            {
                DataTable dt_Material_Forecast_Detail = (DataTable)dgv.DataSource;

                foreach (DataColumn col in dt_Material_Forecast_Detail.Columns)
                {
                    string colName = col.ColumnName;

                    if (colName.Contains(text.str_InsufficientQty))
                    {
                        if(DEDUCT_STOCK)
                        {
                            dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgv.Columns[colName].Visible = true;


                        }
                        else
                        {
                            dgv.Columns[colName].Visible = false;

                        }

                    }
                    else if (colName.Contains(text.str_Delivered))
                    {
                        dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                    else if (colName.Contains(text.str_RequiredQty))
                    {
                        dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                    else if (colName.Contains(text.str_Forecast))
                    {
                        dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                    else if (cmbMonthYear.SelectedIndex > 0)
                    {
                        int selectedIndex = cmbMonthYear.SelectedIndex;
                        string BalStock_ColName = cmbMonthYear.GetItemText(cmbMonthYear.Items[selectedIndex - 1]);
                       
                        if (colName.Contains(text.Header_ReadyStock) || colName.Contains(text.str_EstBalance))
                        {
                            dgv.Columns[colName].HeaderText = BalStock_ColName + text.str_EstBalance;
                        }
                        
                    }
                    else if(cmbMonthYear.SelectedIndex == 0 && colName.Contains(text.str_EstBalance))
                    {
                        dgv.Columns[colName].HeaderText = text.Header_ReadyStock;
                    }
                }

               

                //dgv.Columns[text.Header_ReadyStock].Visible = cbShowStock.Checked;

                if (PageLoaded)
                {
                    dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                    //dgv.Columns[text.Header_PartName].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);

                    dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_DirectUseOn].DefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                    //dgv.Columns[text.Header_Product].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                    dgv.Columns[text.Header_ReadyStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


                   

                    dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_PartCode].Visible = false;
                    dgv.Columns[text.Header_PartName].Visible = false;
                    dgv.Columns[text.Header_PartWeight_G].Visible = false;
                    dgv.Columns[text.Header_RunnerWeight_G].Visible = false;
                    dgv.Columns[text.Header_JoinQty].Visible = false;
                    dgv.Columns[text.Header_JoinMax].Visible = false;
                    dgv.Columns[text.Header_JoinMin].Visible = false;

                    string MatType = tool.getItemCat(MAT_CODE);

                    if (MatType == text.Cat_RawMat || MatType == text.Cat_MB || MatType == text.Cat_Pigment)
                    {
                        dgv.Columns[text.Header_JoinRatio_Product_Mat].Visible = false;

                        dgv.Columns[text.Header_Parent_Weight_G_Piece].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        if (MatType == text.Cat_MB || MatType == text.Cat_Pigment)
                        {
                            dgv.Columns[text.Header_ColorRate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            dgv.Columns[text.Header_ColorRate].Visible = false;
                        }
                    }
                    else
                    {
                        dgv.Columns[text.Header_Parent_Weight_G_Piece].Visible = false;
                        dgv.Columns[text.Header_ColorRate].Visible = false;
                        dgv.Columns[text.Header_JoinRatio_Product_Mat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }

                    dgv.Columns[text.Header_DirectUseOn].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    //dgv.Columns[text.Header_Product].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    dgv.Columns[text.Header_WastageAllowed_Percentage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_Unit].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

                    dgv.Columns[text.Header_MaterialRequiredIncludedWastage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_MaterialRequiredIncludedWastage].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                    dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_DirectUseOn].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                }
               
            }

        }

        private void dgvFullDetailUIEdit(DataGridView dgv)
        {
            if (dgv == dgvMaterialForecastInfo)
            {
                DataTable dt_FullDetail = (DataTable)dgv.DataSource;
                string Month = cmbMonthYear.Text;

                foreach (DataColumn col in dt_FullDetail.Columns)
                {
                    string colName = col.ColumnName;

                    if (colName.Contains(text.str_Delivered))
                    {
                        dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                    else if (colName.Contains(text.str_RequiredQty))
                    {
                        dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv.Columns[colName].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                    }
                    else if (colName.Contains(text.str_EstBalance))
                    {
                        dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //dgv.Columns[colName].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                    }
                    else if (colName.Contains(text.str_Forecast))
                    {
                        dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                    else if (cmbMonthYear.SelectedIndex > 0)
                    {
                        int selectedIndex = cmbMonthYear.SelectedIndex;
                        string BalStock_ColName = cmbMonthYear.GetItemText(cmbMonthYear.Items[selectedIndex - 1]);
                       

                        if ((colName.Contains(text.Header_ReadyStock) || colName.Contains(text.str_EstBalance)) && !colName.Contains(Month))
                        {
                            dgv.Columns[colName].HeaderText = BalStock_ColName + text.str_EstBalance;
                        }

                    }
                    else if (cmbMonthYear.SelectedIndex == 0 && colName.Contains(text.str_EstBalance) && !colName.Contains(Month))
                    {
                        dgv.Columns[colName].HeaderText = text.Header_ReadyStock;
                    }
                }

               
                if (PageLoaded)
                {
                    dgv.DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
                    //dgv.Columns[text.Header_PartName].DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);

                    dgv.Columns[text.Header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);

                    dgv.Columns[text.Header_ReadyStock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_GroupLevel].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    #region Hide Column

                    dgv.Columns[text.Header_IndexMarking].Visible = false;
                    dgv.Columns[text.Header_ParentIndex].Visible = false;
                    dgv.Columns[text.Header_Index].Visible = false;
                    dgv.Columns[text.Header_JoinWastage].Visible = false;
                    dgv.Columns[text.Header_PartName].Visible = false;
                    dgv.Columns[text.Header_PartWeight_G].Visible = false;
                    dgv.Columns[text.Header_RunnerWeight_G].Visible = false;
                    dgv.Columns[text.Header_JoinQty].Visible = false;
                    dgv.Columns[text.Header_JoinMax].Visible = false;
                    dgv.Columns[text.Header_JoinMin].Visible = false;

                    #endregion

                    string MatType = tool.getItemCat(MAT_CODE);

                    if (MatType == text.Cat_RawMat || MatType == text.Cat_MB || MatType == text.Cat_Pigment)
                    {
                        dgv.Columns[text.Header_JoinRatio_Product_Mat].Visible = false;

                        dgv.Columns[text.Header_ItemWeight_G_Piece].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        if (MatType == text.Cat_MB || MatType == text.Cat_Pigment)
                        {
                            dgv.Columns[text.Header_ColorRate].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            dgv.Columns[text.Header_ColorRate].Visible = false;
                        }
                    }
                    else
                    {
                        dgv.Columns[text.Header_ItemWeight_G_Piece].Visible = false;
                        dgv.Columns[text.Header_ColorRate].Visible = false;
                        dgv.Columns[text.Header_JoinRatio_Product_Mat].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }

                    dgv.Columns[text.Header_ItemNameAndCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    dgv.Columns[text.Header_WastageAllowed_Percentage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_Unit].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

                    dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_ItemNameAndCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
                    dgv.Columns[text.Header_Customer].DefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Italic);

                    dgv.Columns[text.Header_PartCode].Width = 1;

                    dgv.Columns[text.Header_Customer].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgv.Columns[text.Header_ItemNameAndCode].MinimumWidth = 250;

                    dgv.Columns[text.Header_PartCode].Visible = false;



                }

            }

        }

        private void pageSetup()
        {
            PageLoaded = false;

            lblMaterial.Text = "Material: " + tool.getItemName(MAT_CODE) + " (" + MAT_CODE + ")";

            cmbDateSetup();
            dgvMaterialForecastInfo.DataSource = null;

            PageLoaded = true;
            DataFilter(MAT_CODE);
        }

        private string LevelSpacing(int level)
        {
            string Spacing = "";

            for (int i = 1; i < level; i++)
            {
                Spacing += "            ";
            }

            Spacing += "-";
            return Spacing;
        }


        private void SetRequiredQtyToChild(string ParentIndex, float Required)
        {
            if (DT_FULLDETAIL != null)
            {
                DataRowView drv = (DataRowView)cmbMonthYear.SelectedItem;
                String month = drv[text.Header_Month].ToString();

                foreach (DataRow row in DT_FULLDETAIL.Rows)
                {
                    if (row[text.Header_ParentIndex].ToString() == ParentIndex)
                    {
                        row[month + text.str_RequiredQty] = Required;
                    }
                }
            }
        }

        private void FullDetailDataCalculation(string LevelChecking)
        {
            if (DT_FULLDETAIL != null)
            {
                string ProductIndex = "0";
                DataRowView drv = (DataRowView)cmbMonthYear.SelectedItem;
                String month = drv[text.Header_Month].ToString();

                foreach (DataRow row in DT_FULLDETAIL.Rows)
                {
                    string ParentIndex = row[text.Header_ParentIndex].ToString();
                    string Level = row[text.Header_GroupLevel].ToString();

                    if (Level == LevelChecking)
                    {
                        ProductIndex = row[text.Header_Index].ToString();

                        string BalStock_ColName ;
                        float ReadyStock = 0;

                        if (Level == "1")
                        {
                            BalStock_ColName = text.Header_ReadyStock;
                            if (cmbMonthYear.SelectedIndex > 0)
                            {
                                int selectedIndex = cmbMonthYear.SelectedIndex;
                                BalStock_ColName = cmbMonthYear.GetItemText(cmbMonthYear.Items[selectedIndex - 1]);

                                BalStock_ColName += text.str_EstBalance;
                            }

                            ReadyStock = float.TryParse(row[BalStock_ColName].ToString(), out ReadyStock) ? ReadyStock : 0;
                        }
                        else
                        {
                            //get latest balance
                        }

                     

                        float Forecast = float.TryParse(row[month + text.str_Forecast].ToString(), out Forecast) ? Forecast : 0;
                        float Delivered = float.TryParse(row[month + text.str_Delivered].ToString(), out Delivered) ? Delivered : 0;
                        float Required = Forecast - Delivered;

                        Required = Required < 0 ? 0 : Required;

                        float EstBalance = ReadyStock - Required;

                        row[month + text.str_EstBalance] = EstBalance;

                        EstBalance = EstBalance < 0 ? EstBalance * -1 : 0;

                        SetRequiredQtyToChild(row[text.Header_Index].ToString(), EstBalance);

                    }






                }
            }
        }

        private DataRow SearchProductInfo(string index)
        {
            if(DT_PRODUCT != null)
            foreach(DataRow row in DT_PRODUCT.Rows)
            {
                    if (row[text.Header_Index].ToString() == index)
                    {
                        float parentIndex = float.TryParse(row[text.Header_ParentIndex].ToString(), out parentIndex)? parentIndex : 0;

                        if(parentIndex == 0)
                        {
                            return row;
                        }
                        else
                        {
                            return SearchProductInfo(parentIndex.ToString());
                        }

                    }
            }
            return null;
        }

        private DataRow SearchParentInfo(string parentIndex)
        {
            if (DT_PRODUCT != null)
                foreach (DataRow row in DT_PRODUCT.Rows)
                {
                    if (row[text.Header_Index].ToString() == parentIndex)
                    {
                        return row;

                    }
                }
            return null;
        }

        private float GetLastMonthBalStock(string itemCode, float ReadyStock, string Month)
        {
            float BalStock = ReadyStock;

            if(DT_PRODUCT != null)
            {
                foreach(DataColumn col in DT_PRODUCT.Columns)
                {
                    string colName = col.ColumnName;

                    if (colName.Contains(text.str_RequiredQty) && !colName.Contains(Month))
                    {
                        float Required = 0;

                        foreach (DataRow row in DT_PRODUCT.Rows)
                        {
                            if (itemCode == row[text.Header_PartCode].ToString())
                            {
                                Required += float.TryParse(row[colName].ToString(), out float i) ? i : 0;
                            }
                        }

                        BalStock -= Required;
                    }
                    else if(colName.Contains(Month))
                    {
                        break;
                    }
                }
                
            }

            BalStock = BalStock < 0 ? 0 : BalStock;

            return BalStock;
        }

        private void DataFilter(string HighLightedItem)
        {
            HIGHLIGHTED_ITEM = HighLightedItem;

            if (cbShowFullDetail.Checked)
            {
                lblTotal.Visible = false;

                ShowFullDetail();

                for (int i = 0; i < dgvMaterialForecastInfo.Columns.Count; i++)
                {
                    dgvMaterialForecastInfo.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                txtOutgoingProduct.Visible = true;
            }
            else
            {
                txtOutgoingProduct.Visible = false;

                dgvMaterialForecastInfo.DataSource = null;
                lblTotal.Visible = true;

                float totalMatUsed = 0;
                string unit = "";

                if (DT_PRODUCT != null && cmbMonthYear.SelectedIndex > -1)
                {
                    DataRowView drv = (DataRowView)cmbMonthYear.SelectedItem;
                    String month = drv[text.Header_Month].ToString();

                    DataTable dt_Forecast_Detail = MaterialForecastSummaryTable(month);

                    int index = 1;

                    foreach (DataRow row in DT_PRODUCT.Rows)
                    {
                        if (row[text.Header_PartCode].ToString() == MAT_CODE)
                        {
                            float childQty = 0;

                            string parentIndex = row[text.Header_ParentIndex].ToString();

                            string Index = row[text.Header_Index].ToString();

                            string ItemType = row[text.Header_Type].ToString();

                            unit = row[text.Header_Unit].ToString();

                            DataRow parentDataRow = SearchParentInfo(parentIndex);

                            if (parentDataRow != null)
                            {
                                string ParentCode = parentDataRow[text.Header_PartCode].ToString();

                                string parent = parentDataRow[text.Header_PartName] + " (" + parentDataRow[text.Header_PartCode] + ")";

                                float ReadyStock = float.TryParse(parentDataRow[text.Header_ReadyStock].ToString(), out ReadyStock) ? ReadyStock : 0;
                                float BalStock = GetLastMonthBalStock(ParentCode, ReadyStock, month);

                                ReadyStock = BalStock;

                                float Required = float.TryParse(parentDataRow[month + text.str_RequiredQty].ToString(), out Required) ? Required : 0;
                                float Insufficient = BalStock - Required;
                                Insufficient = Insufficient > 0 ? 0 : Insufficient;

                                float ChildQtyRequired_Target = 0;

                                if(DEDUCT_STOCK)
                                {
                                    ChildQtyRequired_Target = Insufficient;
                                }
                                else
                                {
                                    ChildQtyRequired_Target = Required > 0 ? Required * -1 : 0 ;

                                }


                                float partWeight = float.TryParse(parentDataRow[text.Header_PartWeight_G].ToString(), out partWeight) ? partWeight : 0;
                                float runnerWeight = float.TryParse(parentDataRow[text.Header_RunnerWeight_G].ToString(), out runnerWeight) ? runnerWeight : 0;

                                float itemWeight = partWeight + runnerWeight;

                                float Wastage = float.TryParse(parentDataRow[text.Header_WastageAllowed_Percentage].ToString(), out Wastage) ? Wastage : 0;
                                float colorRate = float.TryParse(parentDataRow[text.Header_ColorRate].ToString(), out colorRate) ? colorRate : 0;

                                bool ifParentFound = false;

                                #region join ratio

                                float joinMin = float.TryParse(row[text.Header_JoinMin].ToString(), out joinMin) ? joinMin : 1;
                                float joinMax = float.TryParse(row[text.Header_JoinMax].ToString(), out joinMax) ? joinMax : 1;
                                float joinQty = float.TryParse(row[text.Header_JoinQty].ToString(), out joinQty) ? joinQty : 1;

                                string joinMin_Str = row[text.Header_JoinMin].ToString();
                                string joinMax_Str = row[text.Header_JoinMax].ToString();
                                string joinQty_Str = row[text.Header_JoinQty].ToString();

                                string ratio;

                                float childQty_Float = float.TryParse(joinQty_Str, out float f) ? f : -1;

                                if (childQty_Float != -1)
                                {
                                    joinQty_Str = childQty_Float.ToString("0.##");
                                }

                                if (joinMin_Str == joinMax_Str)
                                {
                                    if (joinMax_Str == "" || joinMax_Str == "0")
                                    {
                                        joinMax_Str = "1";
                                    }

                                    ratio = joinMax_Str + ":" + joinQty_Str;
                                }
                                else
                                {
                                    ratio = joinMin_Str + "~" + joinMax_Str + ":" + joinQty_Str;
                                }
                                #endregion

                                foreach (DataRow dt_Row in dt_Forecast_Detail.Rows)
                                {
                                    if (dt_Row[text.Header_PartCode].ToString() == ParentCode)
                                    {
                                        ifParentFound = true;

                                        Required += float.TryParse(dt_Row[month + text.str_RequiredQty].ToString(), out Required) ? Required : 0;

                                        dt_Row[month + text.str_RequiredQty] = Required;

                                        Insufficient = ReadyStock - Required;

                                        Insufficient = Insufficient > 0 ? 0 : Insufficient;

                                        dt_Row[month + text.str_InsufficientQty] = Insufficient;

                                        if (DEDUCT_STOCK)
                                        {
                                            ChildQtyRequired_Target = Insufficient;
                                        }
                                        else
                                        {
                                            ChildQtyRequired_Target = Required > 0 ? Required * -1 : 0;

                                        }

                                        if (ChildQtyRequired_Target < 0)
                                        {
                                            ChildQtyRequired_Target *= -1;

                                            #region total qty required calculation

                                            if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                            {
                                                childQty = (float)decimal.Round((decimal)(ChildQtyRequired_Target * itemWeight * (1 + Wastage)), 3);

                                                if (ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                                {
                                                    childQty = (float)decimal.Round((decimal)(ChildQtyRequired_Target * itemWeight * colorRate * (1 + Wastage)), 3);
                                                }
                                            }
                                            else
                                            {
                                                joinMax = joinMax <= 0 ? 1 : joinMax;

                                                if (ItemType == text.Cat_Part)
                                                {
                                                    childQty = ChildQtyRequired_Target / joinMax * joinQty;

                                                }
                                                else
                                                {
                                                    childQty = (float)Math.Ceiling(ChildQtyRequired_Target / joinMax * joinQty * (1 + Wastage));
                                                }
                                            }

                                            childQty = childQty < 0 ? 0 : childQty;

                                            if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                                childQty = (float)decimal.Round((decimal)(childQty / 1000), 3);



                                            totalMatUsed += childQty;

                                            #endregion
                                        }

                                        dt_Row[text.Header_MaterialRequiredIncludedWastage] = childQty;

                                        break;
                                    }
                                }

                                if (!ifParentFound)
                                {
                                    if (ChildQtyRequired_Target < 0)
                                    {
                                        ChildQtyRequired_Target *= -1;

                                        #region total qty required calculation

                                        if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                        {
                                            childQty = (float)decimal.Round((decimal)(ChildQtyRequired_Target * itemWeight * (1 + Wastage)), 3);

                                            if (ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                            {
                                                childQty = (float)decimal.Round((decimal)(ChildQtyRequired_Target * itemWeight * colorRate * (1 + Wastage)), 3);
                                            }
                                        }
                                        else
                                        {
                                            joinMax = joinMax <= 0 ? 1 : joinMax;

                                            if (ItemType == text.Cat_Part)
                                            {
                                                childQty = ChildQtyRequired_Target / joinMax * joinQty;

                                            }
                                            else
                                            {
                                                childQty = (float)Math.Ceiling(ChildQtyRequired_Target / joinMax * joinQty * (1 + Wastage));
                                            }
                                        }

                                        childQty = childQty < 0 ? 0 : childQty;

                                        if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                            childQty = (float)decimal.Round((decimal)(childQty / 1000), 3);



                                        totalMatUsed += childQty;

                                        #endregion

                                    }

                                    if (Insufficient > 0)
                                    {
                                        Insufficient *= -1;
                                    }

                                    DataRow newDataRow = dt_Forecast_Detail.NewRow();
                                    newDataRow[text.Header_Index] = index++;
                                    newDataRow[text.Header_PartCode] = parentDataRow[text.Header_PartCode];
                                    newDataRow[text.Header_PartName] = parentDataRow[text.Header_PartName];
                                    newDataRow[text.Header_DirectUseOn] = parent;
                                    newDataRow[text.Header_ReadyStock] = BalStock;
                                    newDataRow[month + text.str_RequiredQty] = Required;
                                    newDataRow[month + text.str_InsufficientQty] = Insufficient;
                                    newDataRow[text.Header_PartWeight_G] = parentDataRow[text.Header_PartWeight_G];
                                    newDataRow[text.Header_RunnerWeight_G] = parentDataRow[text.Header_RunnerWeight_G];
                                    newDataRow[text.Header_Parent_Weight_G_Piece] = itemWeight;
                                    newDataRow[text.Header_JoinQty] = row[text.Header_JoinQty];
                                    newDataRow[text.Header_JoinMax] = row[text.Header_JoinMax];
                                    newDataRow[text.Header_JoinMin] = row[text.Header_JoinMin];
                                    newDataRow[text.Header_JoinRatio_Product_Mat] = ratio;
                                    newDataRow[text.Header_ColorRate] = parentDataRow[text.Header_ColorRate];
                                    newDataRow[text.Header_WastageAllowed_Percentage] = Wastage * 100;
                                    newDataRow[text.Header_Unit] = unit;
                                    newDataRow[text.Header_MaterialRequiredIncludedWastage] = childQty;
                                    dt_Forecast_Detail.Rows.Add(newDataRow);
                                }
                            }

                        }
                    }

                    dgvMaterialForecastInfo.DataSource = dt_Forecast_Detail;
                    dgvUIEdit(dgvMaterialForecastInfo);
                    dgvMaterialForecastInfo.ClearSelection();

                    totalMatUsed = 0;
                    foreach (DataRow row in dt_Forecast_Detail.Rows)
                    {
                        totalMatUsed += float.TryParse(row[text.Header_MaterialRequiredIncludedWastage].ToString(), out float i) ? i : 0;

                    }
                }


                for (int i = 0; i < dgvMaterialForecastInfo.Columns.Count; i++)
                {
                    dgvMaterialForecastInfo.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                }

                lblTotal.Text = "Total Material Required Included Wastage: " + totalMatUsed + " " + unit;
            }
        }

        private bool CheckIfIndexExist(DataTable dt, string index)
        {
            bool IndexFound = false;

            if (dt != null)
            {
                foreach(DataRow row in dt.Rows)
                {
                    if (row[text.Header_Index].ToString() == index)
                    {
                        IndexFound = true;
                        break;
                    }
                  
                }
            }

            return IndexFound;
        }

        private bool CheckIfItemExist(DataTable dt, string itemCode, string custName)
        {
            bool ItemFound = false;

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[text.Header_PartCode].ToString() == itemCode && row[text.Header_Customer].ToString() == custName)
                    {
                        ItemFound = true;
                        break;
                    }

                }
            }

            return ItemFound;
        }

        private bool PrintDatatoTable(string index, string month)
        {
            bool printed = false;

            if(DT_PRODUCT != null)
            {
                if(DT_FULLDETAIL == null)
                {
                    DT_FULLDETAIL = FullDetailTable(month);
                }

                if (!CheckIfIndexExist(DT_FULLDETAIL,index))
                {
                    foreach (DataRow row in DT_PRODUCT.Rows)
                    {
                        if (row[text.Header_Index].ToString() == index)
                        {
                            #region print data 
                            DataRow newRow = DT_FULLDETAIL.NewRow();

                            string custName = row[text.Header_Customer].ToString();

                            string itemCode = row[text.Header_PartCode].ToString();
                            string itemName = row[text.Header_PartName].ToString();

                            float PartWeight = float.TryParse(row[text.Header_PartWeight_G].ToString(), out float i) ? i : 0;
                            float RunnerWeight = float.TryParse(row[text.Header_RunnerWeight_G].ToString(), out i) ? i : 0;

                            int ParentIndex = int.TryParse(row[text.Header_ParentIndex].ToString(), out int x) ? x : 0;
                            int GroupLevel = int.TryParse(row[text.Header_GroupLevel].ToString(), out  x) ? x : 0;

                            newRow[text.Header_IndexMarking] = row[text.Header_IndexMarking];

                            newRow[text.Header_ParentIndex] = row[text.Header_ParentIndex];
                            newRow[text.Header_GroupLevel] = row[text.Header_GroupLevel];
                            newRow[text.Header_Customer] = custName;
                            newRow[text.Header_PartCode] = itemCode;
                            newRow[text.Header_PartName] = itemName;
                            newRow[text.Header_PartWeight_G] = row[text.Header_PartWeight_G];
                            newRow[text.Header_RunnerWeight_G] = row[text.Header_RunnerWeight_G];
                            newRow[text.Header_JoinQty] = row[text.Header_JoinQty];
                            newRow[text.Header_JoinMax] = row[text.Header_JoinMax];
                            newRow[text.Header_JoinMin] = row[text.Header_JoinMin];
                            newRow[text.Header_JoinWastage] = row[text.Header_JoinWastage];

                            newRow[text.Header_Index] = row[text.Header_Index];

                            newRow[text.Header_ItemNameAndCode] = LevelSpacing(GroupLevel) + itemName + " (" + itemCode + ")";

                            if(!CheckIfItemExist(DT_FULLDETAIL,itemCode,custName))
                            {
                                newRow[text.Header_ReadyStock] = row[text.Header_ReadyStock];
                            }
                          

                            if(ParentIndex == 0)
                            {
                                newRow[month + text.str_Forecast] = row[month + text.str_Forecast];
                                newRow[month + text.str_Delivered] = row[month + text.str_Delivered];
                               
                            }

                            newRow[month + text.str_RequiredQty] = row[month + text.str_RequiredQty];

                            if(PartWeight + RunnerWeight > 0)
                            newRow[text.Header_ItemWeight_G_Piece] = PartWeight + RunnerWeight;

                            newRow[text.Header_ColorRate] = row[text.Header_ColorRate];

                            #region join ratio

                            float joinMin = float.TryParse(row[text.Header_JoinMin].ToString(), out joinMin) ? joinMin : 1;
                            float joinMax = float.TryParse(row[text.Header_JoinMax].ToString(), out joinMax) ? joinMax : 1;
                            float joinQty = float.TryParse(row[text.Header_JoinQty].ToString(), out joinQty) ? joinQty : 1;

                            string joinMin_Str = row[text.Header_JoinMin].ToString();
                            string joinMax_Str = row[text.Header_JoinMax].ToString();
                            string joinQty_Str = row[text.Header_JoinQty].ToString();

                            string ratio;

                            float childQty_Float = float.TryParse(joinQty_Str, out float f) ? f : -1;

                            if (childQty_Float != -1)
                            {
                                joinQty_Str = childQty_Float.ToString("0.##");
                            }

                            if (joinMin_Str == joinMax_Str)
                            {
                                if (joinMax_Str == "" || joinMax_Str == "0")
                                {
                                    joinMax_Str = "1";
                                }

                                ratio = joinMax_Str + ":" + joinQty_Str;
                            }
                            else
                            {
                                ratio = joinMin_Str + "~" + joinMax_Str + ":" + joinQty_Str;
                            }
                            #endregion

                            newRow[text.Header_JoinRatio_Product_Mat] = ratio;

                            newRow[text.Header_WastageAllowed_Percentage] = row[text.Header_WastageAllowed_Percentage];
                            //newRow[month + text.str_EstBalance] = row[text.Header_BalStock];
                            newRow[text.Header_Unit] = row[text.Header_Unit];

                            DT_FULLDETAIL.Rows.Add(newRow);
                            #endregion

                            if (ParentIndex > 0)
                            {
                                PrintDatatoTable(ParentIndex.ToString(), month);
                            }

                            printed = true;
                            break;
                        }
                    }
                }
              
            }

            return printed;
        }

        private float GetMonthlyBalance(string itemCode, string month)
        {
            float BalStock = 0;

            if(DT_BALANCE_INFO != null)
            {
                if(itemCode == "A0LK150V0")
                {
                    float checkPoint = 1;
                }

                foreach(DataRow row in DT_BALANCE_INFO.Rows)
                {
                    if(itemCode == row[text.Header_PartCode].ToString())
                    {
                        BalStock = float.TryParse(row[month + text.str_EstBalance].ToString(), out BalStock) ? BalStock : 0;
                        break;

                    }
                }
            }
            return BalStock;
        }
        private void BalCalculation(string month)
        {
            if(DT_FULLDETAIL != null)
            {
                DT_FULLDETAIL.DefaultView.Sort = text.Header_PartCode + " ASC," + text.Header_IndexMarking + " ASC";
                DT_FULLDETAIL = DT_FULLDETAIL.DefaultView.ToTable();

                string previousItemCode = "";
                float BalStock = 0;

                string BalStock_ColName = text.Header_ReadyStock;

                foreach (DataColumn col in DT_FULLDETAIL.Columns)
                {
                    string colName = col.ColumnName;

                    if ((colName.Contains(text.Header_ReadyStock) || colName.Contains(text.str_EstBalance)) && !colName.Contains(month))
                    {
                        BalStock_ColName = colName;
                        break;
                    }
                }

                foreach (DataRow row in DT_FULLDETAIL.Rows)
                {
                    string ItemCode = row[text.Header_PartCode].ToString();

                    if(previousItemCode != ItemCode)
                    {
                        if(cmbMonthYear.SelectedIndex > 0)
                        {
                            int selectedIndex = cmbMonthYear.SelectedIndex;
                            string previousMonth = cmbMonthYear.GetItemText(cmbMonthYear.Items[selectedIndex - 1]);
                            BalStock = GetMonthlyBalance(ItemCode, previousMonth);
                            row[text.Header_ReadyStock] = BalStock;
                        }
                        else
                        {
                            BalStock = float.TryParse(row[BalStock_ColName].ToString(), out float i) ? i : 0;

                        }


                        previousItemCode = ItemCode;

                    }

                    float Required = float.TryParse(row[month + text.str_RequiredQty].ToString(), out Required) ? Required : 0;

                    BalStock -= Required;

                    row[month + text.str_EstBalance] = BalStock;
                }
            }
        }
        private void ShowFullDetail()
        {
            dgvMaterialForecastInfo.DataSource = null;

            if (DT_PRODUCT != null && cmbMonthYear.SelectedIndex > -1)
            {
                DataRowView drv = (DataRowView)cmbMonthYear.SelectedItem;
                String month = drv[text.Header_Month].ToString();

                DT_FULLDETAIL  = FullDetailTable(month);

                foreach (DataRow row in DT_PRODUCT.Rows)
                {
                    if (row[text.Header_PartCode].ToString() == MAT_CODE)
                    {
                        string parentIndex = row[text.Header_ParentIndex].ToString();

                        string Index = row[text.Header_Index].ToString();

                        PrintDatatoTable(Index, month);
                    }
                }


                BalCalculation(month);

                DT_FULLDETAIL.DefaultView.Sort = text.Header_IndexMarking + " ASC";
                DT_FULLDETAIL = DT_FULLDETAIL.DefaultView.ToTable();

                FullDetailUIEdit();

                dgvMaterialForecastInfo.DataSource = DT_FULLDETAIL;
                dgvFullDetailUIEdit(dgvMaterialForecastInfo);
                dgvMaterialForecastInfo.ClearSelection();
            }
        }

        private void cmbDateSetup()
        {
            int monthFrom = DATE_FROM.Month;

            DataTable dt = new DataTable();
            dt.Columns.Add(text.Header_Month);

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

                string MonthYear = j + "/" + i;

                dt.Rows.Add(MonthYear);
            }
            cmbMonthYear.DataSource = dt;
            cmbMonthYear.DisplayMember = text.Header_Month;
        }

        private void FullDetailUIEdit()
        {
            if(DT_FULLDETAIL != null)
            {
                DataTable dt = DT_FULLDETAIL.Clone();

                string previousProductIndex = "";

                int ProductIndex = 1;

                foreach(DataRow row in DT_FULLDETAIL.Rows)
                {
                    string ParentIndex = row[text.Header_ParentIndex].ToString();
                    string PartCode = row[text.Header_PartCode].ToString();

                    if (ParentIndex == "0")
                    {
                        if (previousProductIndex != "")
                        {
                            DataRow newRow = dt.NewRow();
                            dt.Rows.Add(newRow);
                        }

                        //row[text.Header_Index] = ProductIndex++;

                        previousProductIndex = ParentIndex;
                        
                    }
                    else if(!string.IsNullOrEmpty(PartCode))
                    {
                        //row[text.Header_Index] = 0;

                    }

                    dt.ImportRow(row);
                }

                DT_FULLDETAIL = dt;
            }

            
        }

        private void frmOrderAlertDetail_NEW_Load(object sender, EventArgs e)
        {
            PageLoaded = true;
        }

        private void cmbMonthYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PageLoaded)
            {
                DataFilter(HIGHLIGHTED_ITEM);
            }
        }

        private void cbShowFullDetail_CheckedChanged(object sender, EventArgs e)
        {
           if(PageLoaded)
            {
                DataFilter(MAT_CODE);
            }
        }

        private void dgvMaterialForecastInfo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMaterialForecastInfo;
            dgv.SuspendLayout();

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            string colName = dgv.Columns[col].Name;

            if (colName.Contains(text.str_InsufficientQty) && DEDUCT_STOCK)
            {
                float Insufficient = float.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out float i) ? i : 0;


                if (Insufficient < 0)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                    dgv.Rows[row].Cells[col].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                    dgv.Rows[row].Cells[col].Style.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

                }
            }

            else if (colName.Contains(text.str_RequiredQty) && !DEDUCT_STOCK)
            {
                float Required = float.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out float i) ? i : 0;


                if (Required > 0)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                    dgv.Rows[row].Cells[col].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                    dgv.Rows[row].Cells[col].Style.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

                }
            }

            else if (colName.Contains(text.str_EstBalance) || colName.Equals(text.Header_ReadyStock))
            {
                float Bal = float.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out float i) ? i : 0;


                if (Bal < 0)
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Red;
                    dgv.Rows[row].Cells[col].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                    dgv.Rows[row].Cells[col].Style.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

                }
            }

            else if (colName.Equals(text.Header_GroupLevel))
            {
                int Level = int.TryParse(dgv.Rows[row].Cells[col].Value.ToString(), out int i) ? i : 0;


                if (Level == 1)
                {
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(253, 203, 110);
                    dgv.Rows[row].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Bold);


                }
                else if (Level == 0)
                {
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(245, 247, 255);
                    dgv.Rows[row].DefaultCellStyle.ForeColor = Color.FromArgb(245, 247, 255);
                    dgv.Rows[row].Height = 20;

                }
                else
                {
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.White;
                    dgv.Rows[row].Height = 50;
                    dgv.Rows[row].DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);

                }
            }

            if (colName.Equals(text.Header_ItemNameAndCode))
            {
                string ItemCode = dgv.Rows[row].Cells[col].Value.ToString();

                if (ItemCode.Contains(HIGHLIGHTED_ITEM))
                {
                    DataRowView drv = (DataRowView)cmbMonthYear.SelectedItem;
                    String month = drv[text.Header_Month].ToString();

                    dgv.Rows[row].Cells[text.Header_ItemNameAndCode].Style.ForeColor = Color.FromArgb(52, 139, 209);
                    dgv.Rows[row].Cells[text.Header_ItemNameAndCode].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                    dgv.Rows[row].Cells[month + text.str_RequiredQty].Style.ForeColor = Color.FromArgb(52, 139, 209);
                    dgv.Rows[row].Cells[month + text.str_RequiredQty].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                }
               
            }

            dgv.ResumeLayout();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            DataFilter(MAT_CODE);
        }

        private void dgvMaterialForecastInfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvMaterialForecastInfo.ClearSelection();
        }

        private void dgvMaterialForecastInfo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            //handle the row selection on right click
            if (e.Button == MouseButtons.Right && e.RowIndex > -1 && !cbShowFullDetail.Checked)
            {
                DataGridView dgv = dgvMaterialForecastInfo;

                ContextMenuStrip my_menu = new ContextMenuStrip();
                dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dgv.Rows[e.RowIndex].Selected = true;
                dgv.Focus();
                int rowIndex = dgv.CurrentCell.RowIndex;


                try
                {

                    my_menu.Items.Add(text.Str_MoreDetail).Name = text.Str_MoreDetail;

                    contextMenuStrip1 = my_menu;

                    my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                    my_menu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemClicked);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void my_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataGridView dgv = dgvMaterialForecastInfo;

            string itemClicked = e.ClickedItem.Name.ToString();

            int rowIndex = dgv.CurrentCell.RowIndex;

            string itemCode = dgv.Rows[rowIndex].Cells[text.Header_PartCode].Value.ToString();

            contextMenuStrip1.Hide();

            if (itemClicked.Equals(text.Str_MoreDetail))
            {
                PageLoaded = false;
                cbShowFullDetail.Checked = true;
                PageLoaded = true;
                DataFilter(itemCode);
            }
          

            Cursor = Cursors.Arrow; // change cursor to normal type

        }
    }
}
