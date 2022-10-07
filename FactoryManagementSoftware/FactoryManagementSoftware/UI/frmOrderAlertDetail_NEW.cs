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
        public frmOrderAlertDetail_NEW(DataTable dt_Product_Info,string matCode, DateTime dateFrom, DateTime dateTo)
        {
            InitializeComponent();
            DT_PRODUCT = dt_Product_Info;
            MAT_CODE = matCode;
            DATE_FROM = dateFrom;
            DATE_TO = dateTo;

            pageSetup();

        }

        #region variable 

        private string MAT_CODE;
        DataTable DT_PRODUCT;
        DateTime DATE_FROM;
        DateTime DATE_TO;

        private string string_Forecast = " FORECAST";
        private string string_StillNeed = " STILL NEED";
        private string string_Delivered = " DELIVERED";

        private string string_Product_Forecast = " PRODUCT FORECAST";
        private string string_Product_StillNeed = " PRODUCT STILL NEED";
        private string string_Product_Delivered = " PRODUCT DELIVERED";

        //private string string_EstBalance = " EST. BAL.";

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

        bool PageLoaded = false;

        Tool tool = new Tool();

        Text text = new Text();

        #endregion

        private DataTable NewMaterialForecastSummaryTable(String month)
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

            dt.Columns.Add(text.Header_MaterialUsedWithWastage, typeof(float));

            dt.Columns.Add(text.Header_Unit, typeof(string));
            return dt;
        }

        private DataTable OldMaterialForecastDetailTable(String month)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));

            dt.Columns.Add(text.Header_PartCode, typeof(string));
            dt.Columns.Add(text.Header_PartName, typeof(string));

            dt.Columns.Add(text.Header_DirectUseOn, typeof(string));

            dt.Columns.Add(text.Header_ParentStock, typeof(float));

            dt.Columns.Add(text.Header_Product, typeof(string));

            dt.Columns.Add(text.Header_ProductStock, typeof(float));

            //dt.Columns.Add(text.Header_ReadyStock, typeof(float));

            dt.Columns.Add(month + string_Product_Forecast, typeof(float));
            dt.Columns.Add(month + string_Product_Delivered, typeof(float));
            dt.Columns.Add(month + string_Product_StillNeed, typeof(float));

            dt.Columns.Add(text.Header_PartWeight_G, typeof(float));
            dt.Columns.Add(text.Header_RunnerWeight_G, typeof(float));
            dt.Columns.Add(text.Header_Parent_Weight_G_Piece, typeof(float));

            dt.Columns.Add(text.Header_JoinQty, typeof(float));
            dt.Columns.Add(text.Header_JoinMax, typeof(float));
            dt.Columns.Add(text.Header_JoinMin, typeof(float));
            dt.Columns.Add(text.Header_JoinRatio_Product_Mat, typeof(string));

            dt.Columns.Add(text.Header_WastageAllowed_Percentage, typeof(float));
            dt.Columns.Add(text.Header_ColorRate, typeof(float));

            dt.Columns.Add(text.Header_MaterialUsedWithWastage, typeof(float));

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

                    if (!cbShowForecastQty.Checked && colName.Contains(string_Product_Forecast))
                    {
                        dgv.Columns[colName].Visible = false;
                    }
                    else if (colName.Contains(string_Product_Forecast))
                    {
                        dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv.Columns[colName].Visible = true;

                    }

                    if (!cbShowDeliveredQty.Checked && colName.Contains(string_Product_Delivered))
                    {
                        dgv.Columns[colName].Visible = false;
                    }
                    else if (colName.Contains(string_Product_Delivered))
                    {
                        dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv.Columns[colName].Visible = true;


                    }
                    if (!cbShowStillNeedQty.Checked && colName.Contains(string_Product_StillNeed))
                    {
                        dgv.Columns[colName].Visible = false;
                    }
                    else if (colName.Contains(string_Product_StillNeed))
                    {
                        dgv.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv.Columns[colName].Visible = true;


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

                    dgv.Columns[text.Header_MaterialUsedWithWastage].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_MaterialUsedWithWastage].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                    dgv.Columns[text.Header_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgv.Columns[text.Header_DirectUseOn].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
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
            DataFilter();
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

        private float IfParentExistReturnRequiredQty(DataTable dt, string itemCode, string month)
        {
            float Required = 0;

            if(dt != null)
            {
                foreach(DataRow row in dt.Rows)
                {
                    if(itemCode == row[text.Header_PartCode].ToString())
                    {
                        Required = float.TryParse(row[month + text.str_RequiredQty].ToString(), out Required) ? Required : 0;

                        break;
                    }
                }
            }
            return Required;
        }
        private void DataFilter()
        {
            dgvMaterialForecastInfo.DataSource = null;

            float totalMatUsed = 0;
            string unit = "";

            if (DT_PRODUCT != null && cmbMonthYear.SelectedIndex > -1)
            {
                DataRowView drv = (DataRowView)cmbMonthYear.SelectedItem;
                String month = drv[text.Header_Month].ToString();

                DataTable dt_Forecast_Detail = NewMaterialForecastSummaryTable(month);

                int index = 1;

                foreach(DataRow row in DT_PRODUCT.Rows)
                {
                    if (row[text.Header_PartCode].ToString() == MAT_CODE)
                    {
                        float childQty = 0;

                        string parentIndex = row[text.Header_ParentIndex].ToString();

                        string Index = row[text.Header_Index].ToString();

                        string ItemType = row[text.Header_Type].ToString();

                        unit = row[text.Header_Unit].ToString();

                        DataRow parentDataRow = SearchParentInfo(parentIndex);

                        if(parentDataRow != null)
                        {
                            string ParentCode = parentDataRow[text.Header_PartCode].ToString();

                            

                            string parent = parentDataRow[text.Header_PartName] + " (" + parentDataRow[text.Header_PartCode] + ")";

                            float ReadyStock = float.TryParse(parentDataRow[text.Header_ReadyStock].ToString(), out ReadyStock) ? ReadyStock : 0;
                            float Required = float.TryParse(parentDataRow[month + text.str_RequiredQty].ToString(), out Required) ? Required : 0;
                            float Insufficient = ReadyStock - Required;

                            Insufficient = Insufficient > 0 ? 0 : Insufficient;


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

                                    if (Insufficient < 0)
                                    {
                                        Insufficient *= -1;

                                        #region total qty required calculation

                                        if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                        {
                                            childQty = (float)decimal.Round((decimal)(Insufficient * itemWeight * (1 + Wastage)), 3);

                                            if (ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                            {
                                                childQty = (float)decimal.Round((decimal)(Insufficient * itemWeight * colorRate * (1 + Wastage)), 3);
                                            }
                                        }
                                        else
                                        {
                                            joinMax = joinMax <= 0 ? 1 : joinMax;

                                            if (ItemType == text.Cat_Part)
                                            {
                                                childQty = Insufficient / joinMax * joinQty;

                                            }
                                            else
                                            {
                                                childQty = (float)Math.Ceiling(Insufficient / joinMax * joinQty * (1 + Wastage));
                                            }
                                        }

                                        childQty = childQty < 0 ? 0 : childQty;

                                        if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                            childQty = (float)decimal.Round((decimal)(childQty / 1000), 3);



                                        totalMatUsed += childQty;

                                        #endregion
                                    }
                                    dt_Row[text.Header_MaterialUsedWithWastage] = childQty;

                                    break;
                                }
                            }

                            if(!ifParentFound)
                            {
                                if (Insufficient < 0)
                                {
                                    Insufficient *= -1;

                                    #region total qty required calculation

                                    if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                    {
                                        childQty = (float)decimal.Round((decimal)(Insufficient * itemWeight * (1 + Wastage)), 3);

                                        if (ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                        {
                                            childQty = (float)decimal.Round((decimal)(Insufficient * itemWeight * colorRate * (1 + Wastage)), 3);
                                        }
                                    }
                                    else
                                    {
                                        joinMax = joinMax <= 0 ? 1 : joinMax;

                                        if (ItemType == text.Cat_Part)
                                        {
                                            childQty = Insufficient / joinMax * joinQty;

                                        }
                                        else
                                        {
                                            childQty = (float)Math.Ceiling(Insufficient / joinMax * joinQty * (1 + Wastage));
                                        }
                                    }

                                    childQty = childQty < 0 ? 0 : childQty;

                                    if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                        childQty = (float)decimal.Round((decimal)(childQty / 1000), 3);



                                    totalMatUsed += childQty;

                                    #endregion

                                }

                                DataRow newDataRow = dt_Forecast_Detail.NewRow();
                                newDataRow[text.Header_Index] = index++;
                                newDataRow[text.Header_PartCode] = parentDataRow[text.Header_PartCode];
                                newDataRow[text.Header_PartName] = parentDataRow[text.Header_PartName];
                                newDataRow[text.Header_DirectUseOn] = parent;
                                newDataRow[text.Header_ReadyStock] = parentDataRow[text.Header_ReadyStock];
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
                                newDataRow[text.Header_MaterialUsedWithWastage] = childQty;
                                dt_Forecast_Detail.Rows.Add(newDataRow);
                            }
                        }
                       
                    }
                }

                dgvMaterialForecastInfo.DataSource = dt_Forecast_Detail;
                dgvUIEdit(dgvMaterialForecastInfo);
                dgvMaterialForecastInfo.ClearSelection();

            }

            lblTotal.Text = "Total " + totalMatUsed + " " + unit;

        }

        private void OLD_DataFilter()
        {
            dgvMaterialForecastInfo.DataSource = null;

            float totalMatUsed = 0;
            string unit = "";

            if (DT_PRODUCT != null && cmbMonthYear.SelectedIndex > -1)
            {
                DataRowView drv = (DataRowView)cmbMonthYear.SelectedItem;
                String month = drv[text.Header_Month].ToString();

                DataTable dt_Forecast_Detail = NewMaterialForecastSummaryTable(month);

                int index = 1;

                foreach (DataRow row in DT_PRODUCT.Rows)
                {
                    if (row[text.Header_PartCode].ToString() == MAT_CODE)
                    {
                        string parentIndex = row[text.Header_ParentIndex].ToString();

                        string Index = row[text.Header_Index].ToString();

                        string ItemType = row[text.Header_Type].ToString();

                        unit = row[text.Header_Unit].ToString();

                        DataRow parentDataRow = SearchParentInfo(parentIndex);

                        DataRow productDataRow = SearchProductInfo(Index);

                        if (parentDataRow != null)
                        {
                            DataRow newDataRow = dt_Forecast_Detail.NewRow();

                            newDataRow[text.Header_Index] = index++;

                            newDataRow[text.Header_PartCode] = parentDataRow[text.Header_PartCode];
                            newDataRow[text.Header_PartName] = parentDataRow[text.Header_PartName];

                            string parent = parentDataRow[text.Header_PartName] + " (" + parentDataRow[text.Header_PartCode] + ")";

                            newDataRow[text.Header_DirectUseOn] = parent;

                            if (productDataRow != null)
                            {
                                string product = productDataRow[text.Header_PartName] + " (" + productDataRow[text.Header_PartCode] + ")";

                                newDataRow[text.Header_Product] = product;

                                //if (parent != product)
                                //{
                                //    newDataRow[text.Header_Product] = product;

                                //}

                            }

                            float ReadyStock = float.TryParse(parentDataRow[text.Header_ReadyStock].ToString(), out ReadyStock) ? ReadyStock : 0;
                            float Forecast = float.TryParse(parentDataRow[month + string_Forecast].ToString(), out Forecast) ? Forecast : 0;
                            float Delivered = float.TryParse(parentDataRow[month + string_Delivered].ToString(), out Delivered) ? Delivered : 0;
                            float StillNeed = float.TryParse(parentDataRow[month + string_StillNeed].ToString(), out StillNeed) ? StillNeed : 0;

                            newDataRow[text.Header_ReadyStock] = parentDataRow[text.Header_ReadyStock];
                            newDataRow[month + string_Product_Forecast] = parentDataRow[month + string_Forecast];
                            newDataRow[month + string_Product_Delivered] = parentDataRow[month + string_Delivered];
                            newDataRow[month + string_Product_StillNeed] = parentDataRow[month + string_StillNeed];

                            newDataRow[text.Header_PartWeight_G] = parentDataRow[text.Header_PartWeight_G];
                            newDataRow[text.Header_RunnerWeight_G] = parentDataRow[text.Header_RunnerWeight_G];

                            float partWeight = float.TryParse(parentDataRow[text.Header_PartWeight_G].ToString(), out partWeight) ? partWeight : 0;
                            float runnerWeight = float.TryParse(parentDataRow[text.Header_RunnerWeight_G].ToString(), out runnerWeight) ? runnerWeight : 0;

                            float itemWeight = partWeight + runnerWeight;

                            newDataRow[text.Header_Parent_Weight_G_Piece] = itemWeight;


                            //float joinQty = float.TryParse(parentDataRow[text.Header_JoinQty].ToString(), out joinQty) ? joinQty : 0;
                            //float joinMax = float.TryParse(parentDataRow[text.Header_JoinMax].ToString(), out joinMax) ? joinMax : 0;
                            //float joinMin = float.TryParse(parentDataRow[text.Header_JoinMin].ToString(), out joinMin) ? joinMin : 0;

                            newDataRow[text.Header_JoinQty] = row[text.Header_JoinQty];
                            newDataRow[text.Header_JoinMax] = row[text.Header_JoinMax];
                            newDataRow[text.Header_JoinMin] = row[text.Header_JoinMin];

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

                            newDataRow[text.Header_JoinRatio_Product_Mat] = ratio;

                            newDataRow[text.Header_ColorRate] = parentDataRow[text.Header_ColorRate];

                            float Wastage = float.TryParse(parentDataRow[text.Header_WastageAllowed_Percentage].ToString(), out Wastage) ? Wastage : 0;
                            float colorRate = float.TryParse(parentDataRow[text.Header_ColorRate].ToString(), out colorRate) ? colorRate : 0;

                            newDataRow[text.Header_WastageAllowed_Percentage] = Wastage * 100;

                            newDataRow[text.Header_Unit] = unit;

                            float parentQty = 0;

                            if (cbShowStillNeedQty.Checked)
                            {
                                if (cbShowStock.Checked)
                                {
                                    parentQty = StillNeed - ReadyStock < 0 ? 0 : StillNeed - ReadyStock;


                                    parentQty = parentQty < 0 ? 0 : parentQty;

                                    newDataRow[month + string_Product_StillNeed] = parentQty;
                                }
                                else
                                {
                                    parentQty = StillNeed;
                                }
                            }
                            else if (cbShowForecastQty.Checked && !cbShowDeliveredQty.Checked)
                            {
                                parentQty = Forecast;
                            }
                            else if (!cbShowForecastQty.Checked && cbShowDeliveredQty.Checked)
                            {
                                parentQty = Delivered;
                            }

                            parentQty = parentQty < 0 ? 0 : parentQty;


                            #region total qty required calculation

                            float childQty = 0;

                            if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                            {
                                childQty = (float)decimal.Round((decimal)(parentQty * itemWeight * (1 + Wastage)), 3);

                                if (ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                {
                                    childQty = (float)decimal.Round((decimal)(parentQty * itemWeight * colorRate * (1 + Wastage)), 3);
                                }
                            }
                            else
                            {
                                joinMax = joinMax <= 0 ? 1 : joinMax;

                                if (ItemType == text.Cat_Part)
                                {
                                    childQty = parentQty / joinMax * joinQty;

                                }
                                else
                                {
                                    childQty = (float)Math.Ceiling(parentQty / joinMax * joinQty * (1 + Wastage));
                                }
                            }

                            childQty = childQty < 0 ? 0 : childQty;

                            if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                                childQty = (float)decimal.Round((decimal)(childQty / 1000), 3);

                            newDataRow[text.Header_MaterialUsedWithWastage] = childQty;

                            totalMatUsed += childQty;

                            #endregion

                            dt_Forecast_Detail.Rows.Add(newDataRow);

                        }



                        //loop search product name
                    }
                }

                dgvMaterialForecastInfo.DataSource = dt_Forecast_Detail;
                dgvUIEdit(dgvMaterialForecastInfo);
                dgvMaterialForecastInfo.ClearSelection();

            }

            lblTotal.Text = "Total " + totalMatUsed + " " + unit;

        }
        private void DataFilterUpdate(CheckBox cb)
        {
            PageLoaded = false;

            #region checkbox filter
            if (cb == cbShowForecastQty)
            {
                cbShowStillNeedQty.Checked = cbShowDeliveredQty.Checked && cbShowForecastQty.Checked;

                if (!cbShowForecastQty.Checked)
                {
                    cbShowDeliveredQty.Checked = true;
                    cbShowStillNeedQty.Checked = false;
                    cbShowStock.Checked = false;
                }
            }
            else if (cb == cbShowDeliveredQty)
            {
                cbShowStillNeedQty.Checked = cbShowDeliveredQty.Checked && cbShowForecastQty.Checked;

                if (!cbShowDeliveredQty.Checked)
                {
                    cbShowForecastQty.Checked = true;
                    cbShowStillNeedQty.Checked = false;
                    cbShowStock.Checked = false;
                }

            }
            else if (cb == cbShowStillNeedQty)
            {
                if (cbShowStillNeedQty.Checked)
                {
                    cbShowForecastQty.Checked = true;
                    cbShowDeliveredQty.Checked = true;
                }
                else
                {
                    cbShowStock.Checked = false;

                    if (cbShowForecastQty.Checked)
                    {
                        cbShowDeliveredQty.Checked = false;
                    }
                    else if (cbShowDeliveredQty.Checked)
                    {
                        cbShowForecastQty.Checked = false;
                    }
                    else
                    {
                        cbShowForecastQty.Checked = true;
                    }
                }
            }
            else if (cb == cbShowStock)
            {
                if (cbShowStock.Checked)
                {
                    cbShowForecastQty.Checked = true;
                    cbShowDeliveredQty.Checked = true;
                    cbShowStillNeedQty.Checked = true;
                }
            }
            #endregion

            float totalMatUsed = 0;
            string unit = "";

            DataTable dt_Forecast_Detail = (DataTable)dgvMaterialForecastInfo.DataSource;

            if (dt_Forecast_Detail != null && cmbMonthYear.SelectedIndex > -1)
            {
                DataRowView drv = (DataRowView)cmbMonthYear.SelectedItem;
                String month = drv[text.Header_Month].ToString();

                foreach (DataRow row in dt_Forecast_Detail.Rows)
                {
                    string ItemType = tool.getItemCat(MAT_CODE);
                    unit = row[text.Header_Unit].ToString();
                    string index = row[text.Header_Index].ToString();

                    float ReadyStock = float.TryParse(row[text.Header_ReadyStock].ToString(), out ReadyStock) ? ReadyStock : 0;
                    float Forecast = float.TryParse(row[month + string_Product_Forecast].ToString(), out Forecast) ? Forecast : 0;
                    float Delivered = float.TryParse(row[month + string_Product_Delivered].ToString(), out Delivered) ? Delivered : 0;

                    float partWeight = float.TryParse(row[text.Header_PartWeight_G].ToString(), out partWeight) ? partWeight : 0;
                    float runnerWeight = float.TryParse(row[text.Header_RunnerWeight_G].ToString(), out runnerWeight) ? runnerWeight : 0;

                    float itemWeight = partWeight + runnerWeight;


                    float joinMin = float.TryParse(row[text.Header_JoinMin].ToString(), out joinMin) ? joinMin : 1;
                    float joinMax = float.TryParse(row[text.Header_JoinMax].ToString(), out joinMax) ? joinMax : 1;
                    float joinQty = float.TryParse(row[text.Header_JoinQty].ToString(), out joinQty) ? joinQty : 1;

                    float Wastage = float.TryParse(row[text.Header_WastageAllowed_Percentage].ToString(), out Wastage) ? Wastage : 0;

                    Wastage /= 100;

                    float colorRate = float.TryParse(row[text.Header_ColorRate].ToString(), out colorRate) ? colorRate : 0;

                    float parentQty = 0;

                    if (cbShowStillNeedQty.Checked)
                    {
                        if (cbShowStock.Checked)
                        {
                            parentQty = Forecast - Delivered - ReadyStock < 0 ? 0 : Forecast - Delivered - ReadyStock;

                            row[month + string_Product_StillNeed] = parentQty;
                        }
                        else
                        {
                            parentQty = Forecast - Delivered;
                        }
                    }
                    else if (cbShowForecastQty.Checked && !cbShowDeliveredQty.Checked)
                    {
                        parentQty = Forecast;
                    }
                    else if (!cbShowForecastQty.Checked && cbShowDeliveredQty.Checked)
                    {
                        parentQty = Delivered;
                    }

                    float childQty = 0;

                    if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                    {
                        childQty = (float)decimal.Round((decimal)(parentQty * itemWeight * (1 + Wastage)), 3);

                        if (ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                        {
                            childQty = (float)decimal.Round((decimal)(parentQty * itemWeight * colorRate * (1 + Wastage)), 3);
                        }
                    }
                    else
                    {
                        joinMax = joinMax <= 0 ? 1 : joinMax;

                        if (ItemType == text.Cat_Part)
                        {
                            childQty = parentQty / joinMax * joinQty;

                        }
                        else
                        {
                            childQty = (float)Math.Ceiling(parentQty / joinMax * joinQty * (1 + Wastage));
                        }
                    }


                    parentQty = parentQty < 0 ? 0 : parentQty;

                    childQty = childQty < 0 ? 0 : childQty;

                    if (ItemType == text.Cat_RawMat || ItemType == text.Cat_MB || ItemType == text.Cat_Pigment)
                        childQty = (float)decimal.Round((decimal)(childQty / 1000), 3);

                    row[month + string_Product_StillNeed] = parentQty;
                    row[text.Header_MaterialUsedWithWastage] = childQty;

                    totalMatUsed += childQty;
                }

                dgvUIEdit(dgvMaterialForecastInfo);
                dgvMaterialForecastInfo.ClearSelection();

            }

            lblTotal.Text = "Total " + totalMatUsed + " " + unit;

            PageLoaded = true;
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

        private void cbShowDeliveredQty_CheckedChanged(object sender, EventArgs e)
        {

            if (PageLoaded)
                DataFilterUpdate(cbShowDeliveredQty);
        }

      

        private void cbShowForecastQty_CheckedChanged(object sender, EventArgs e)
        {
            if(PageLoaded)
                DataFilterUpdate(cbShowForecastQty);
        }

        private void frmOrderAlertDetail_NEW_Load(object sender, EventArgs e)
        {
            PageLoaded = true;
        }

        private void cmbMonthYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PageLoaded)
            {
                DataFilter();
            }
        }

        private void cbShowStillNeedQty_CheckedChanged(object sender, EventArgs e)
        {
            if (PageLoaded)
                DataFilterUpdate(cbShowStillNeedQty);
        }

        private void cbShowStock_CheckedChanged(object sender, EventArgs e)
        {
            if (PageLoaded)
                DataFilterUpdate(cbShowStock);
        }

        private void dgvMaterialForecastInfo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvMaterialForecastInfo;
            dgv.SuspendLayout();

            int row = e.RowIndex;
            int col = e.ColumnIndex;

            string colName = dgv.Columns[col].Name;

            if (colName == text.Header_Product)
            {
                string Product = dgv.Rows[row].Cells[col].Value.ToString();
                string Parent = dgv.Rows[row].Cells[text.Header_DirectUseOn].Value.ToString();

                if (Product == Parent)
                {
                    dgv.Rows[row].Cells[col].Style.BackColor = Color.LightGray;
                    dgv.Rows[row].Cells[text.Header_DirectUseOn].Style.BackColor = Color.LightGray;

                }
                else
                {
                    dgv.Rows[row].Cells[col].Style.BackColor = Color.White;
                    dgv.Rows[row].Cells[text.Header_DirectUseOn].Style.BackColor = Color.White;
                }
            }

            dgv.ResumeLayout();
        }

        private void btnFilterApply_Click(object sender, EventArgs e)
        {
            DataFilter();
        }

        private void dgvMaterialForecastInfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvMaterialForecastInfo.ClearSelection();
        }
    }
}
