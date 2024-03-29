﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using System.Linq;


namespace FactoryManagementSoftware.UI
{
    public partial class FinexDataUpload : Form
    {
        Tool tool = new Tool();
        Text text = new Text();



        #region Data String

        private string path = null;
        private string excelName = null;
        private int rowCount = 0;

        //table header
        private string HEADER_INDEX = "#";
        private string HEADER_TIMESTAMP = "TIMESTAMP";
        private string HEADER_TESTATOR = "TESTATOR";
        private string HEADER_STATUS = "STATUS";

        private string HEADER_INFORMATION = "INFORMATION";
        private string HEADER_DESCRIPTION = "DESCRIPTION";

        //category
        private string PRIMARYKEY_TIMESTAMP = "TIMESTAMP";

        private string PART_EP = "(EP)";
      
        private string PART_A = "(A)";
        private string PART_A_TESTATOR = "TESTATOR";

        private string PART_B1 = "(B1)";
        private string PART_B2 = "(B2)";
        private string PART_B1_EXECUTOR_1 = "EXECUTOR 1";
        private string PART_B2_EXECUTOR_2 = "EXECUTOR 2";

        private string PART_C0 = "(C0)";
        private string PART_C1 = "(C1)";
        private string PART_C2 = "(C2)";
        private string PART_C3 = "(C3)";
        private string PART_C4 = "(C4)";
        private string PART_C5 = "(C5)";
        private string PART_C6 = "(C6)";

        private string PART_C0_TOTAL_BENEFICIARIES = "TOTAL BENEFICIARIES";
        private string PART_C1_BENEFICIARY_1 = "BENEFICIARY 1";
        private string PART_C2_BENEFICIARY_2 = "BENEFICIARY 2";
        private string PART_C3_BENEFICIARY_3 = "BENEFICIARY 3";
        private string PART_C4_BENEFICIARY_4 = "BENEFICIARY 4";
        private string PART_C5_BENEFICIARY_5 = "BENEFICIARY 5";

        private string PART_C1_0 = "(C1.0)";
        private string PART_C1_1 = "(C1.1)";
        private string PART_C1_2 = "(C1.2)";
        private string PART_C1_3 = "(C1.3)";
        private string PART_C1_4 = "(C1.4)";
        private string PART_C1_5 = "(C1.5)";

        private string PART_C1_0_TOTAL_BENEFICIARIES = "TOTAL SUBSTITUTE BENEFICIARIES";
        private string PART_C1_1_BENEFICIARY_1 = "SUBSTITUTE BENEFICIARY 1";
        private string PART_C1_2_BENEFICIARY_2 = "SUBSTITUTE BENEFICIARY 2";
        private string PART_C1_3_BENEFICIARY_3 = "SUBSTITUTE BENEFICIARY 3";
        private string PART_C1_4_BENEFICIARY_4 = "SUBSTITUTE BENEFICIARY 4";
        private string PART_C1_5_BENEFICIARY_5 = "SUBSTITUTE BENEFICIARY 5";

        private string PART_D1 = "(D1)";
        private string PART_D2 = "(D2)";
        private string PART_D1_GUARDIAN_1 = "GUARDIAN 1";
        private string PART_D2_GUARDIAN_2 = "GUARDIAN 2";

        private string PART_E1 = "(E1)";
        private string PART_E2 = "(E2)";
        private string PART_E3 = "(E3)";
        private string PART_E1_PROPERTIES_1 = "IMMOVABLE PROPERTIES 1";
        private string PART_E2_PROPERTIES_2 = "IMMOVABLE PROPERTIES 2";
        private string PART_E3_PROPERTIES_3 = "IMMOVABLE PROPERTIES 3";

        //private string PART_F1 = "(F1)";
        //private string PART_F2 = "(F2)";
        //private string PART_F3 = "(F3)";
        //private string PART_F4 = "(F4)";
        //private string PART_F1_FUNDING_WIFE = "FUNDING: WIFE";
        //private string PART_F2_FUNDING_CHILDREN = "FUNDING: CHILDREN";
        //private string PART_F3_FUNDING_PARENTS = "FUNDING: PARENTS";
        //private string PART_F4_FUNDING_GUARDIAN = "FUNDING: GUARDIAN";

        private string PART_G1 = "(G1)";
        private string PART_G2 = "(G2)";
        private string PART_G1_INSURANCE_1 = "INSURANCE 1";
        private string PART_G2_INSURANCE_2 = "INSURANCE 2";

        //private string PART_H1 = "(H1)";
        private string PART_H2 = "(H2)";
        private string PART_H3 = "(H3)";
        private string PART_H2_RELIGION = "FINAL RESTING PLACE: RELIGION";
        private string PART_H3_METHOD = "FINAL RESTING PLACE: METHOD";

        private string PART_J1 = "(J1)";
        private string PART_J2 = "(J2)";


        //Data
        private string HEADER_FULLNAME = "FULL NAME";
        private string HEADER__IC = "NRIC";
        private string HEADER__BIRTHDAY = "BIRTHDAY";
        private string HEADER__HP = "HANDPHONE NO.";
        private string HEADER__EMAIL = "EMAIL";
        private string HEADER__ADDRESS = "ADDRESS";
        private string HEADER__MARITAL = "MARITAL STATUS";
        private string HEADER__RELIGION = "RELIGION";
        private string HEADER__SPECIALFAMILYCONDITION = "SPECIAL FAMILY CONDITION";
        private string HEADER__RELATIONSHIP = "RELATIONSHIP";
        private string HEADER__TOTAL_BENEFICIARIES = "TOTAL BENEFICIARIES";
        private string HEADER__SHARE_PERCENTAGE = "SHARE";
        private string HEADER__PROPERTY_ADDRESS = "ADDRESS OF THE PROPERTY";
        private string HEADER__TITLE_DETAILS = "TITLE DETAILS";
        private string HEADER__OWNERSHIP = "OWNERSHIP";
        private string HEADER__PROPERTY_BENEFICIARIES = "BENEFICIARIES";
        private string HEADER__HOUSEHOLD_EXPENSES = "HOUSEHOLD EXPENSES";
        private string HEADER__GUARDIAN_ALLOWANCE = "GUARDIAN'S MONTHLY ALLOWANCE";
        private string HEADER__INSURANCE_COMPANY = "INSURANCE COMPANY";
        private string HEADER__INSURANCE_POLICY_NO = "POLICY NUMBER";
        private string HEADER__RESTING_METHOD = "METHOD";
        private string HEADER__ASSETS = "ASSETS";
        private string HEADER__STATE = "STATE";
        private string HEADER__POSTCODE = "POSTCODE";

        private string HEADER_PLANNER_IN_CHARGE = "PLANNER-IN-CHARGE";
        private string HEADER_CONTACT = "CONTACT";
        private string HEADER_EP_Code = "PLANNER CODE";
        private string HEADER_EP_PACKAGE = "PACKAGE";
        private string HEADER_DEATH_OF_MAIN_BENEFICIARY = "DEATH OF MAIN BENEFICIARY";
        private string HEADER_ADDITIONAL_INSTRUCTIONS = "ADDITIONAL INSTRUCTIONS";

        #endregion

        DataTable dt_DataSource;

        public FinexDataUpload()
        {
            InitializeComponent();
            
        }

        #region New Table Setting

        private DataTable NewDataSourceTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(HEADER_INDEX, typeof(int));
            dt.Columns.Add(HEADER_TIMESTAMP, typeof(DateTime));

            dt.Columns.Add(PART_EP + " " + HEADER_PLANNER_IN_CHARGE, typeof(string));
            dt.Columns.Add(PART_EP + " " + HEADER_CONTACT, typeof(string));
            dt.Columns.Add(PART_EP + " " + HEADER__EMAIL, typeof(string));
            dt.Columns.Add(PART_EP + " " + HEADER_EP_Code, typeof(string));
            dt.Columns.Add(PART_EP + " " + HEADER_EP_PACKAGE, typeof(string));

            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER__BIRTHDAY, typeof(string));
            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER__HP, typeof(string));
            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER__EMAIL, typeof(string));
            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER__ADDRESS, typeof(string));
            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER__POSTCODE, typeof(string));
            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER__STATE, typeof(string));
            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER__MARITAL, typeof(string));
            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER__RELIGION, typeof(string));
            dt.Columns.Add(PART_A + PART_A_TESTATOR + " " + HEADER__SPECIALFAMILYCONDITION, typeof(string));

            dt.Columns.Add(PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__HP, typeof(string));
            dt.Columns.Add(PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__EMAIL, typeof(string));
            dt.Columns.Add(PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__ADDRESS, typeof(string));
            dt.Columns.Add(PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__POSTCODE, typeof(string));
            dt.Columns.Add(PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__STATE, typeof(string));

            dt.Columns.Add(PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__HP, typeof(string));
            dt.Columns.Add(PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__EMAIL, typeof(string));
            dt.Columns.Add(PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__ADDRESS, typeof(string));
            dt.Columns.Add(PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__POSTCODE, typeof(string));
            dt.Columns.Add(PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__STATE, typeof(string));

            dt.Columns.Add(PART_C0 + " " + HEADER__TOTAL_BENEFICIARIES, typeof(string));

            dt.Columns.Add(PART_C1 + PART_C1_BENEFICIARY_1 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_C1 + PART_C1_BENEFICIARY_1 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_C1 + PART_C1_BENEFICIARY_1 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_C1 + PART_C1_BENEFICIARY_1 + " " + HEADER__SHARE_PERCENTAGE, typeof(string));

            dt.Columns.Add(PART_C2 + PART_C2_BENEFICIARY_2 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_C2 + PART_C2_BENEFICIARY_2 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_C2 + PART_C2_BENEFICIARY_2 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_C2 + PART_C2_BENEFICIARY_2 + " " + HEADER__SHARE_PERCENTAGE, typeof(string));

            dt.Columns.Add(PART_C3 + PART_C3_BENEFICIARY_3 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_C3 + PART_C3_BENEFICIARY_3 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_C3 + PART_C3_BENEFICIARY_3 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_C3 + PART_C3_BENEFICIARY_3 + " " + HEADER__SHARE_PERCENTAGE, typeof(string));

            dt.Columns.Add(PART_C4 + PART_C4_BENEFICIARY_4 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_C4 + PART_C4_BENEFICIARY_4 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_C4 + PART_C4_BENEFICIARY_4 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_C4 + PART_C4_BENEFICIARY_4 + " " + HEADER__SHARE_PERCENTAGE, typeof(string));

            dt.Columns.Add(PART_C5 + PART_C5_BENEFICIARY_5 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_C5 + PART_C5_BENEFICIARY_5 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_C5 + PART_C5_BENEFICIARY_5 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_C5 + PART_C5_BENEFICIARY_5 + " " + HEADER__SHARE_PERCENTAGE, typeof(string));

            dt.Columns.Add(PART_C6 + " " + HEADER_DEATH_OF_MAIN_BENEFICIARY, typeof(string));

            dt.Columns.Add(PART_C1_0 + " " + HEADER__TOTAL_BENEFICIARIES, typeof(string));

            dt.Columns.Add(PART_C1_1 + PART_C1_1_BENEFICIARY_1 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_C1_1 + PART_C1_1_BENEFICIARY_1 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_C1_1 + PART_C1_1_BENEFICIARY_1 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_C1_1 + PART_C1_1_BENEFICIARY_1 + " " + HEADER__SHARE_PERCENTAGE, typeof(string));

            dt.Columns.Add(PART_C1_2 + PART_C1_2_BENEFICIARY_2 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_C1_2 + PART_C1_2_BENEFICIARY_2 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_C1_2 + PART_C1_2_BENEFICIARY_2 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_C1_2 + PART_C1_2_BENEFICIARY_2 + " " + HEADER__SHARE_PERCENTAGE, typeof(string));

            dt.Columns.Add(PART_C1_3 + PART_C1_3_BENEFICIARY_3 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_C1_3 + PART_C1_3_BENEFICIARY_3 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_C1_3 + PART_C1_3_BENEFICIARY_3 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_C1_3 + PART_C1_3_BENEFICIARY_3 + " " + HEADER__SHARE_PERCENTAGE, typeof(string));

            dt.Columns.Add(PART_C1_4 + PART_C1_4_BENEFICIARY_4 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_C1_4 + PART_C1_4_BENEFICIARY_4 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_C1_4 + PART_C1_4_BENEFICIARY_4 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_C1_4 + PART_C1_4_BENEFICIARY_4 + " " + HEADER__SHARE_PERCENTAGE, typeof(string));

            dt.Columns.Add(PART_C1_5 + PART_C1_5_BENEFICIARY_5 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_C1_5 + PART_C1_5_BENEFICIARY_5 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_C1_5 + PART_C1_5_BENEFICIARY_5 + " " + HEADER__RELATIONSHIP, typeof(string));
            dt.Columns.Add(PART_C1_5 + PART_C1_5_BENEFICIARY_5 + " " + HEADER__SHARE_PERCENTAGE, typeof(string));

            dt.Columns.Add(PART_D1 + PART_D1_GUARDIAN_1 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_D1 + PART_D1_GUARDIAN_1 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_D1 + PART_D1_GUARDIAN_1 + " " + HEADER__HP, typeof(string));
            dt.Columns.Add(PART_D1 + PART_D1_GUARDIAN_1 + " " + HEADER__EMAIL, typeof(string));
            dt.Columns.Add(PART_D1 + PART_D1_GUARDIAN_1 + " " + HEADER__ADDRESS, typeof(string));
            dt.Columns.Add(PART_D1 + PART_D1_GUARDIAN_1 + " " + HEADER__POSTCODE, typeof(string));
            dt.Columns.Add(PART_D1 + PART_D1_GUARDIAN_1 + " " + HEADER__STATE, typeof(string));
            dt.Columns.Add(PART_D1 + PART_D1_GUARDIAN_1 + " " + HEADER__RELATIONSHIP, typeof(string));
            
            dt.Columns.Add(PART_D2 + PART_D2_GUARDIAN_2 + " " + HEADER_FULLNAME, typeof(string));
            dt.Columns.Add(PART_D2 + PART_D2_GUARDIAN_2 + " " + HEADER__IC, typeof(string));
            dt.Columns.Add(PART_D2 + PART_D2_GUARDIAN_2 + " " + HEADER__HP, typeof(string));
            dt.Columns.Add(PART_D2 + PART_D2_GUARDIAN_2 + " " + HEADER__EMAIL, typeof(string));
            dt.Columns.Add(PART_D2 + PART_D2_GUARDIAN_2 + " " + HEADER__ADDRESS, typeof(string));
            dt.Columns.Add(PART_D2 + PART_D2_GUARDIAN_2 + " " + HEADER__POSTCODE, typeof(string));
            dt.Columns.Add(PART_D2 + PART_D2_GUARDIAN_2 + " " + HEADER__STATE, typeof(string));
            dt.Columns.Add(PART_D2 + PART_D2_GUARDIAN_2 + " " + HEADER__RELATIONSHIP, typeof(string));

            dt.Columns.Add(PART_E1 + PART_E1_PROPERTIES_1 + " " + HEADER__PROPERTY_ADDRESS, typeof(string));
            dt.Columns.Add(PART_E1 + PART_E1_PROPERTIES_1 + " " + HEADER__POSTCODE, typeof(string));
            dt.Columns.Add(PART_E1 + PART_E1_PROPERTIES_1 + " " + HEADER__STATE, typeof(string));
            dt.Columns.Add(PART_E1 + PART_E1_PROPERTIES_1 + " " + HEADER__TITLE_DETAILS, typeof(string));
            dt.Columns.Add(PART_E1 + PART_E1_PROPERTIES_1 + " " + HEADER__OWNERSHIP, typeof(string));
            dt.Columns.Add(PART_E1 + PART_E1_PROPERTIES_1 + " " + HEADER__PROPERTY_BENEFICIARIES, typeof(string));

            dt.Columns.Add(PART_E2 + PART_E2_PROPERTIES_2 + " " + HEADER__PROPERTY_ADDRESS, typeof(string));
            dt.Columns.Add(PART_E2 + PART_E2_PROPERTIES_2 + " " + HEADER__POSTCODE, typeof(string));
            dt.Columns.Add(PART_E2 + PART_E2_PROPERTIES_2 + " " + HEADER__STATE, typeof(string));
            dt.Columns.Add(PART_E2 + PART_E2_PROPERTIES_2 + " " + HEADER__TITLE_DETAILS, typeof(string));
            dt.Columns.Add(PART_E2 + PART_E2_PROPERTIES_2 + " " + HEADER__OWNERSHIP, typeof(string));
            dt.Columns.Add(PART_E2 + PART_E2_PROPERTIES_2 + " " + HEADER__PROPERTY_BENEFICIARIES, typeof(string));

            dt.Columns.Add(PART_E3 + PART_E3_PROPERTIES_3 + " " + HEADER__PROPERTY_ADDRESS, typeof(string));
            dt.Columns.Add(PART_E3 + PART_E3_PROPERTIES_3 + " " + HEADER__POSTCODE, typeof(string));
            dt.Columns.Add(PART_E3 + PART_E3_PROPERTIES_3 + " " + HEADER__STATE, typeof(string));
            dt.Columns.Add(PART_E3 + PART_E3_PROPERTIES_3 + " " + HEADER__TITLE_DETAILS, typeof(string));
            dt.Columns.Add(PART_E3 + PART_E3_PROPERTIES_3 + " " + HEADER__OWNERSHIP, typeof(string));
            dt.Columns.Add(PART_E3 + PART_E3_PROPERTIES_3 + " " + HEADER__PROPERTY_BENEFICIARIES, typeof(string));

            //dt.Columns.Add(PART_F1 + PART_F1_FUNDING_WIFE + " " + HEADER__HOUSEHOLD_EXPENSES, typeof(string));
            //dt.Columns.Add(PART_F2 + PART_F2_FUNDING_CHILDREN + " " + HEADER__HOUSEHOLD_EXPENSES, typeof(string));
            //dt.Columns.Add(PART_F3 + PART_F3_FUNDING_PARENTS + " " + HEADER__HOUSEHOLD_EXPENSES, typeof(string));
            //dt.Columns.Add(PART_F4 + PART_F4_FUNDING_GUARDIAN + " " + HEADER__GUARDIAN_ALLOWANCE, typeof(string));

            dt.Columns.Add(PART_G1 + PART_G1_INSURANCE_1 + " " + HEADER__INSURANCE_COMPANY, typeof(string));
            dt.Columns.Add(PART_G1 + PART_G1_INSURANCE_1 + " " + HEADER__INSURANCE_POLICY_NO, typeof(string));

            dt.Columns.Add(PART_G2 + PART_G2_INSURANCE_2 + " " + HEADER__INSURANCE_COMPANY, typeof(string));
            dt.Columns.Add(PART_G2 + PART_G2_INSURANCE_2 + " " + HEADER__INSURANCE_POLICY_NO, typeof(string));

            dt.Columns.Add(PART_H2 + PART_H2_RELIGION + " " + HEADER__RELIGION, typeof(string));
            dt.Columns.Add(PART_H3 + PART_H3_METHOD + " " + HEADER__RESTING_METHOD, typeof(string));

            dt.Columns.Add(PART_J1 + " " + HEADER__ASSETS, typeof(string));
            dt.Columns.Add(PART_J2 + " " + HEADER_ADDITIONAL_INSTRUCTIONS, typeof(string));




            return dt;
        }

        private DataTable NewMainTestatorTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(HEADER_INDEX, typeof(int));
            dt.Columns.Add(HEADER_TIMESTAMP, typeof(string));
            dt.Columns.Add(HEADER_TESTATOR, typeof(string));
            dt.Columns.Add(HEADER_STATUS, typeof(string));

            return dt;
        }

        private DataTable NewWillDetailTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(HEADER_INDEX, typeof(int));
            dt.Columns.Add(HEADER_INFORMATION, typeof(string));
            dt.Columns.Add(HEADER_DESCRIPTION, typeof(string));

            return dt;
        }

        #endregion

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            //dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            //dgv.Columns[header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_ItemCode].Visible = false;

            if(dgv == dgvDetail)
            {
                dgv.Columns[HEADER_INFORMATION].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                dgv.Columns[HEADER_DESCRIPTION].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgv.Columns[HEADER_INDEX].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[HEADER_INFORMATION].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[HEADER_DESCRIPTION].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            else if(dgv == dgvTestatorList)
            {
                dgv.Columns[HEADER_TESTATOR].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            txtPath.BackColor = SystemColors.Window;
            txtPath.Clear();
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "File Excel|*.xlsx";

            DialogResult re = fd.ShowDialog();
            excelName = fd.SafeFileName;

            if (re == DialogResult.OK)
            {
                path = fd.FileName;

                //string extension = System.IO.Path.GetExtension(path);
                //MessageBox.Show(extension);
                //if (".csv".Equals(extension))

                txtPath.Text = path;
                txtPath.ForeColor = Color.Black;
                //txtPath.BackColor = Color.DarkSeaGreen;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                frmLoading.ShowLoadingScreen();
                ReadExcel(path);
                btnWill.Visible = false;
                frmLoading.CloseForm();
            }
            else
            {
                MessageBox.Show("path not found");
            }
        }

        public void ReadExcel(string Path)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            rowCount = 0;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            int row;
            int col;
            int totalRow = 0;
            int totalCol = 0;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(Path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            totalRow = range.Rows.Count;
            totalCol = range.Columns.Count;

            int headerRow = int.TryParse(txtHeaderRow.Text, out headerRow)? headerRow : 1;

            bool headerFound = false;

            for (col = 1; col <= totalCol; col++)
            {
                string test = Convert.ToString((range.Cells[headerRow, col] as Excel.Range).Value2);

                if (test.ToUpper().Contains(PRIMARYKEY_TIMESTAMP))
                {
                    headerFound = true;
                    break;
                }
            }

            if(headerFound)
            {
                int index = 1;

                if (dt_DataSource == null || dt_DataSource.Rows.Count <= 0)
                {
                    dt_DataSource = NewDataSourceTable();
                }
                else
                {
                    index = int.TryParse(dt_DataSource.Rows[dt_DataSource.Rows.Count - 1][HEADER_INDEX].ToString(), out index)? index + 1 : 1;
                }

                DataRow dtRow;


                for (row = headerRow + 1; row <= totalRow; row++)
                {
                    bool validTimeStamp = false;
                    DateTime TimeStamp = DateTime.MaxValue;

                    for (int timeStampCol = 1; timeStampCol <= totalCol; timeStampCol++)
                    {
                        string header = Convert.ToString((range.Cells[headerRow, timeStampCol] as Excel.Range).Value2);

                        if (header != null)
                        {
                            header = header.ToUpper();

                            object value = (range.Cells[row, timeStampCol] as Excel.Range).Value2;

                            if (value != null && !validTimeStamp)
                            {
                                if (value is double)
                                {
                                    TimeStamp = DateTime.FromOADate((double)value);
                                }
                                else
                                {
                                    DateTime.TryParse((string)value, out TimeStamp);
                                }

                                if (TimeStamp != null && TimeStamp != DateTime.MaxValue && TimeStamp != DateTime.MinValue)
                                {
                                    validTimeStamp = true;

                                    break;
                                }
                            }
                        }
                    }

                    if(validTimeStamp)
                    {
                        dtRow = dt_DataSource.NewRow();
                        dtRow[HEADER_INDEX] = index++;
                        dtRow[HEADER_TIMESTAMP] = TimeStamp;

                        for (col = 1; col <= totalCol; col++)
                        {
                            string header = Convert.ToString((range.Cells[headerRow, col] as Excel.Range).Value2);
                            string DataID = "";
                            string DataTitle = "";
                            if (header != null)
                            {
                                header = header.ToUpper();

                                object value = (range.Cells[row, col] as Excel.Range).Value2;

                                #region EP

                                if (header.Contains(PART_EP) && header.ToUpper().Contains(HEADER_PLANNER_IN_CHARGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_EP + " " + HEADER_PLANNER_IN_CHARGE] = data.ToUpper();
                                }
                                else if (header.Contains(PART_EP) && header.Contains(HEADER_CONTACT))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_EP + " " + HEADER_CONTACT] = data;
                                }
                                else if (header.Contains(PART_EP) && header.Contains(HEADER__EMAIL))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_EP + " " + HEADER__EMAIL] = data;
                                }
                                else if (header.Contains(PART_EP) && header.Contains(HEADER_EP_Code))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_EP + " " + HEADER_EP_Code] = data;
                                }
                                else if (header.Contains(PART_EP) && header.Contains(HEADER_EP_PACKAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_EP + " " + HEADER_EP_PACKAGE] = data;
                                }
                                #endregion

                                #region Testator Section

                                if (header.Contains(PART_A_TESTATOR) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER_FULLNAME] = data.ToUpper();

                                }
                                else if (header.Contains(PART_A) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(PART_A) && header.Contains(HEADER__BIRTHDAY))
                                {
                                    DateTime birthday;
                                    if (value is double)
                                    {
                                        birthday = DateTime.FromOADate((double)value);
                                    }
                                    else
                                    {
                                        DateTime.TryParse((string)value, out birthday);
                                    }

                                    
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER__BIRTHDAY] = birthday.ToString("dd/MM/yyyy");
                                }
                                else if (header.Contains(PART_A) && header.Contains(HEADER__HP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER__HP] = data;
                                }
                                else if (header.Contains(PART_A) && header.Contains(HEADER__EMAIL))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER__EMAIL] = data;
                                }
                                else if (header.Contains(PART_A) && header.Contains(HEADER__ADDRESS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER__ADDRESS] = data;
                                }
                                else if (header.Contains(PART_A) && header.Contains(HEADER__POSTCODE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER__POSTCODE] = data;
                                }
                                else if (header.Contains(PART_A) && header.Contains(HEADER__STATE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER__STATE] = data;
                                }
                                else if (header.Contains(PART_A) && header.Contains(HEADER__MARITAL))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER__MARITAL] = data;
                                }
                                else if (header.Contains(PART_A) && header.Contains(HEADER__RELIGION))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER__RELIGION] = data;
                                }
                                else if (header.Contains(PART_A) && header.Contains(HEADER__SPECIALFAMILYCONDITION))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_A + PART_A_TESTATOR + " " + HEADER__SPECIALFAMILYCONDITION] = data;
                                }

                                #endregion

                                #region Executor 1

                                else if (header.Contains(PART_B1) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(PART_B1) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(PART_B1) && header.Contains(HEADER__HP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__HP] = data;
                                }
                                else if (header.Contains(PART_B1) && header.Contains(HEADER__EMAIL))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__EMAIL] = data;
                                }
                                else if (header.Contains(PART_B1) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(PART_B1) && header.Contains(HEADER__ADDRESS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__ADDRESS] = data;
                                }
                                else if (header.Contains(PART_B1) && header.Contains(HEADER__POSTCODE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__POSTCODE] = data;
                                }
                                else if (header.Contains(PART_B1) && header.Contains(HEADER__STATE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B1 + PART_B1_EXECUTOR_1 + " " + HEADER__STATE] = data;
                                }

                                #endregion

                                #region Executor 2

                                else if (header.Contains(PART_B2) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(PART_B2) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(PART_B2) && header.Contains(HEADER__HP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__HP] = data;
                                }
                                else if (header.Contains(PART_B2) && header.Contains(HEADER__EMAIL))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__EMAIL] = data;
                                }
                                else if (header.Contains(PART_B2) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(PART_B2) && header.Contains(HEADER__ADDRESS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__ADDRESS] = data;
                                }
                                else if (header.Contains(PART_B2) && header.Contains(HEADER__POSTCODE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__POSTCODE] = data;
                                }
                                else if (header.Contains(PART_B2) && header.Contains(HEADER__STATE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_B2 + PART_B2_EXECUTOR_2 + " " + HEADER__STATE] = data;
                                }
                                #endregion

                                #region Beneficiaries

                                else if (header.Contains(PART_C0) && header.Contains(HEADER__TOTAL_BENEFICIARIES))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C0 + " " + HEADER__TOTAL_BENEFICIARIES] = data;
                                }
                                else if (header.Contains(PART_C6) && header.Contains(HEADER_DEATH_OF_MAIN_BENEFICIARY))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C6 + " " + HEADER_DEATH_OF_MAIN_BENEFICIARY] = data;
                                }

                                #region BENEFICIAR 1

                                else if (header.Contains(PART_C1) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C1 + PART_C1_BENEFICIARY_1 + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(PART_C1) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C1 + PART_C1_BENEFICIARY_1 + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(PART_C1) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C1 + PART_C1_BENEFICIARY_1 + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(PART_C1) && header.Contains(HEADER__SHARE_PERCENTAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C1 + PART_C1_BENEFICIARY_1 + " " + HEADER__SHARE_PERCENTAGE] = data;
                                }

                                #endregion

                                #region BENEFICIAR 2

                                DataID = PART_C2;
                                DataTitle = PART_C2_BENEFICIARY_2;

                                if (header.Contains(DataID) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__SHARE_PERCENTAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__SHARE_PERCENTAGE] = data;
                                }

                                #endregion

                                #region BENEFICIAR 3

                                DataID = PART_C3;
                                DataTitle = PART_C3_BENEFICIARY_3;

                                if (header.Contains(DataID) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__SHARE_PERCENTAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__SHARE_PERCENTAGE] = data;
                                }

                                #endregion

                                #region BENEFICIAR 4

                                DataID = PART_C4;
                                DataTitle = PART_C4_BENEFICIARY_4;

                                if (header.Contains(DataID) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__SHARE_PERCENTAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__SHARE_PERCENTAGE] = data;
                                }

                                #endregion

                                #region BENEFICIAR 5

                                DataID = PART_C5;
                                DataTitle = PART_C5_BENEFICIARY_5;

                                if (header.Contains(DataID) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__SHARE_PERCENTAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__SHARE_PERCENTAGE] = data;
                                }

                                #endregion


                                #endregion

                                #region Substitute Beneficiaries

                                else if (header.Contains(PART_C1_0) && header.Contains(HEADER__TOTAL_BENEFICIARIES))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C1_0 + " " + HEADER__TOTAL_BENEFICIARIES] = data;
                                }

                                #region Substitute BENEFICIAR 1

                                else if (header.Contains(PART_C1_1) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C1_1 + PART_C1_1_BENEFICIARY_1 + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(PART_C1_1) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C1_1 + PART_C1_1_BENEFICIARY_1 + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(PART_C1_1) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C1_1 + PART_C1_1_BENEFICIARY_1 + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(PART_C1_1) && header.Contains(HEADER__SHARE_PERCENTAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_C1_1 + PART_C1_1_BENEFICIARY_1 + " " + HEADER__SHARE_PERCENTAGE] = data;
                                }

                                #endregion

                                #region Substitute BENEFICIAR 2

                                DataID = PART_C1_2;
                                DataTitle = PART_C1_2_BENEFICIARY_2;

                                if (header.Contains(DataID) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__SHARE_PERCENTAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__SHARE_PERCENTAGE] = data;
                                }

                                #endregion

                                #region Substitute BENEFICIAR 3

                                DataID = PART_C1_3;
                                DataTitle = PART_C1_3_BENEFICIARY_3;

                                if (header.Contains(DataID) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__SHARE_PERCENTAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__SHARE_PERCENTAGE] = data;
                                }

                                #endregion

                                #region Substitute BENEFICIAR 4

                                DataID = PART_C1_4;
                                DataTitle = PART_C1_4_BENEFICIARY_4;

                                if (header.Contains(DataID) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__SHARE_PERCENTAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__SHARE_PERCENTAGE] = data;
                                }

                                #endregion

                                #region Substitute BENEFICIAR 5

                                DataID = PART_C1_5;
                                DataTitle = PART_C1_5_BENEFICIARY_5;

                                if (header.Contains(DataID) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__RELATIONSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__SHARE_PERCENTAGE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__SHARE_PERCENTAGE] = data;
                                }

                                #endregion


                                #endregion

                                #region Guardian 1

                                DataID = PART_D1;
                                DataTitle = PART_D1_GUARDIAN_1;

                                if (header.Contains(DataID) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__HP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__HP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__EMAIL))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__EMAIL] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__ADDRESS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__ADDRESS] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__STATE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__STATE] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__POSTCODE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__POSTCODE] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__RELATIONSHIP] = data;
                                }


                                #endregion

                                #region Guardian 2

                                DataID = PART_D2;
                                DataTitle = PART_D2_GUARDIAN_2;

                                if (header.Contains(DataID) && header.Contains(HEADER_FULLNAME))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER_FULLNAME] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__IC))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__IC] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__HP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__HP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__EMAIL))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__EMAIL] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__ADDRESS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__ADDRESS] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__STATE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__STATE] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__POSTCODE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__POSTCODE] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__RELATIONSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__RELATIONSHIP] = data;
                                }

                                #endregion

                                #region Immovable Properties 1

                                DataID = PART_E1;
                                DataTitle = PART_E1_PROPERTIES_1;

                                if (header.Contains(DataID) && header.Contains(HEADER__PROPERTY_ADDRESS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__PROPERTY_ADDRESS] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__POSTCODE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__POSTCODE] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__STATE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__STATE] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__TITLE_DETAILS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__TITLE_DETAILS] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__OWNERSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__OWNERSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__PROPERTY_BENEFICIARIES))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__PROPERTY_BENEFICIARIES] = data;
                                }

                                #endregion

                                #region Immovable Properties 2

                                DataID = PART_E2;
                                DataTitle = PART_E2_PROPERTIES_2;

                                if (header.Contains(DataID) && header.Contains(HEADER__PROPERTY_ADDRESS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__PROPERTY_ADDRESS] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__POSTCODE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__POSTCODE] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__STATE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__STATE] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__TITLE_DETAILS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__TITLE_DETAILS] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__OWNERSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__OWNERSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__PROPERTY_BENEFICIARIES))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__PROPERTY_BENEFICIARIES] = data;
                                }

                                #endregion

                                #region Immovable Properties 3

                                DataID = PART_E3;
                                DataTitle = PART_E3_PROPERTIES_3;

                                if (header.Contains(DataID) && header.Contains(HEADER__PROPERTY_ADDRESS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__PROPERTY_ADDRESS] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__POSTCODE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__POSTCODE] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__STATE))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__STATE] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__TITLE_DETAILS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__TITLE_DETAILS] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__OWNERSHIP))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__OWNERSHIP] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__PROPERTY_BENEFICIARIES))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__PROPERTY_BENEFICIARIES] = data;
                                }

                                #endregion

                                #region Household Expenses

                                //if (header.Contains(PART_F1) && header.Contains(HEADER__HOUSEHOLD_EXPENSES))
                                //{
                                //    string data = Convert.ToString(value);
                                //    dtRow[PART_F1 + PART_F1_FUNDING_WIFE + " " + HEADER__HOUSEHOLD_EXPENSES] = data;
                                //}
                                //else if (header.Contains(PART_F2) && header.Contains(HEADER__HOUSEHOLD_EXPENSES))
                                //{
                                //    string data = Convert.ToString(value);
                                //    dtRow[PART_F2 + PART_F2_FUNDING_CHILDREN + " " + HEADER__HOUSEHOLD_EXPENSES] = data;
                                //}
                                //else if (header.Contains(PART_F3) && header.Contains(HEADER__HOUSEHOLD_EXPENSES))
                                //{
                                //    string data = Convert.ToString(value);
                                //    dtRow[PART_F3 + PART_F3_FUNDING_PARENTS + " " + HEADER__HOUSEHOLD_EXPENSES] = data;
                                //}
                                //else if (header.Contains(PART_F4) && header.Contains(HEADER__GUARDIAN_ALLOWANCE))
                                //{
                                //    string data = Convert.ToString(value);
                                //    dtRow[PART_F4 + PART_F4_FUNDING_GUARDIAN + " " + HEADER__GUARDIAN_ALLOWANCE] = data;
                                //}
                                #endregion

                                #region Insurance 1

                                DataID = PART_G1;
                                DataTitle = PART_G1_INSURANCE_1;

                                if (header.Contains(DataID) && header.Contains(HEADER__INSURANCE_COMPANY))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__INSURANCE_COMPANY] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__INSURANCE_POLICY_NO))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__INSURANCE_POLICY_NO] = data;
                                }

                                #endregion

                                #region Insurance 2

                                DataID = PART_G2;
                                DataTitle = PART_G2_INSURANCE_2;

                                if (header.Contains(DataID) && header.Contains(HEADER__INSURANCE_COMPANY))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__INSURANCE_COMPANY] = data;
                                }
                                else if (header.Contains(DataID) && header.Contains(HEADER__INSURANCE_POLICY_NO))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[DataID + DataTitle + " " + HEADER__INSURANCE_POLICY_NO] = data;
                                }

                                #endregion

                                #region Final Resting Place

                                if (header.Contains(PART_H2) && header.Contains(HEADER__RELIGION))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_H2 + PART_H2_RELIGION + " " + HEADER__RELIGION] = data;
                                }
                                else if (header.Contains(PART_H3) && header.Contains(HEADER__RESTING_METHOD))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_H3 + PART_H3_METHOD + " " + HEADER__RESTING_METHOD] = data;
                                }

                                #endregion

                                #region Additional

                                if (header.Contains(PART_J1) && header.Contains(HEADER__ASSETS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_J1 + " " + HEADER__ASSETS] = data;
                                }
                                else if (header.Contains(PART_J2) && header.Contains(HEADER_ADDITIONAL_INSTRUCTIONS))
                                {
                                    string data = Convert.ToString(value);
                                    dtRow[PART_J2 + " " + HEADER_ADDITIONAL_INSTRUCTIONS] = data;
                                }

                                #endregion

                            }

                        }

                        dt_DataSource.Rows.Add(dtRow);
                    }
                    
                }

            }
            else
            {
                MessageBox.Show("Header not found!\nPlease change the row of header in yellow field,\nor upload another file.");
            }



            
            
            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            LoadTestatorList();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void LoadTestatorList()
        {
            dgvTestatorList.DataSource = null;
            dgvDetail.DataSource = null;

            DataTable dt = NewMainTestatorTable();
            DataRow dtRow;
            foreach(DataRow row in dt_DataSource.Rows)
            {
                dtRow = dt.NewRow();

                dtRow[HEADER_INDEX] = row[HEADER_INDEX];
                dtRow[HEADER_TIMESTAMP] = row[HEADER_TIMESTAMP];
                dtRow[HEADER_TESTATOR] = row[PART_A + PART_A_TESTATOR + " " + HEADER_FULLNAME];

                dt.Rows.Add(dtRow);
            }
            dgvTestatorList.DataSource = dt;

            DgvUIEdit(dgvTestatorList);

            dgvTestatorList.ClearSelection();

        }

        private void ExportToWord2()
        {
            string path = @"D:\Finex\Will";

            Directory.CreateDirectory(path);

            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = path,

                Filter = "Word Document (*.docx)|*.docx",
                FileName = "Testing" + ".docx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)//sfd.ShowDialog() == DialogResult.OK
            {
                try
                {
                    frmLoading.ShowLoadingScreen();

                    #region Creating Word Document
                    //Create an instance for word winword  
                    Word.Application winword = new Word.Application
                    {

                        //Set animation status for word application  
                        //ShowAnimation = false,

                        //Set status for word application is to be visible or not.  
                        Visible = false
                    };

                    
                    //THE LOCATION OF THE TEMPLATE FILE ON THE MACHINE  
                    //Object oTemplatePath = @"D:\Users\Jun\Desktop\Template free will - with 3 properties.dotx";
                    //ADDING A NEW DOCUMENT FROM A TEMPLATE  
                    //oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

                    //Create a missing variable for missing value  
                    object missing = Missing.Value;
                    object oFalse = false;
                    object oTrue = true;

                    //Create a new document  
                    Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                    document.Sections[1].PageSetup.DifferentFirstPageHeaderFooter = -1;
                    //Section section2 = document.Sections[0];
                    //section2.PageSetup.DifferentFirstPageHeaderFooter = -1;

                    float pointToCMRate = 0.035f;
                   


                    document.PageSetup.BottomMargin = 6.35f / pointToCMRate;
                    #endregion

                    // Setting Different First page Header & Footer
                    //document.Sections[1].Headers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range.Text = "First Page Header";
                    //document.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range.Text = "First Page Footer";

                    // Setting Other page Header & Footer
                   
                    //document.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Text = "Other Page Footer";

                    #region Inserting Water Mark Text

                    //THE LOGO IS ASSIGNED TO A SHAPE OBJECT SO THAT WE CAN USE ALL THE  
                    //SHAPE FORMATTING OPTIONS PRESENT FOR THE SHAPE OBJECT  
                    Shape logoWatermark = null;

                    winword.ActiveWindow.ActivePane.View.SeekView = WdSeekView.wdSeekCurrentPageHeader;

                    //INCLUDING THE TEXT WATER MARK TO THE DOCUMENT  
                    logoWatermark = winword.Selection.HeaderFooter.Shapes.AddTextEffect(
                        Microsoft.Office.Core.MsoPresetTextEffect.msoTextEffect1,
                        "DRAFT", "Bodoni MT", 60,
                        Microsoft.Office.Core.MsoTriState.msoTrue,
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        0, 0, ref missing);
                    logoWatermark.Select(ref missing);
                    logoWatermark.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                    logoWatermark.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                    logoWatermark.Fill.Solid();
                    logoWatermark.Fill.ForeColor.RGB = (Int32)WdColor.wdColorGray30;
                    logoWatermark.RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionMargin;

                    logoWatermark.RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionMargin;

                    logoWatermark.Left = (float)WdShapePosition.wdShapeCenter;
                    logoWatermark.Top = (float)WdShapePosition.wdShapeCenter;
                    logoWatermark.Height = winword.InchesToPoints(2.4f);
                    logoWatermark.Width = winword.InchesToPoints(6f);

                    //SETTING FOCUES BACK TO DOCUMENT  
                    winword.ActiveWindow.ActivePane.View.SeekView = WdSeekView.wdSeekMainDocument;

                    #endregion

                    #region COVER PAGE
                    //Add paragraph with Heading 1 style  
                    Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                    para1 = document.Content.Paragraphs.Add(ref missing);
                    para1 = document.Content.Paragraphs.Add(ref missing);
                    object styleHeading1 = "Normal";
                    para1.Range.set_Style(ref styleHeading1);
                    
                    string Space = "";

                    for(int k = 0; k < 5; k++)
                    {
                        Space += Environment.NewLine;
                    }

                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Font.Size = 15;
                    para1.Range.Text = Space + "THE";
                    para1.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Font.Size = 30;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "LAST WILL";
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Font.Size = 15;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "AND";
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Font.Size = 30;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "TESTAMENT";
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Font.Size = 15;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "OF" + Environment.NewLine;
                    para1.Range.InsertParagraphAfter();

////////////////////NAME
                    para1.Range.Font.Size = 30;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "ONG WING CHUN";
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Font.Size = 30;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "(900624-14-5747)";
                    para1.Range.InsertParagraphAfter();
                    #endregion

                    #region Hard Page Break
                    object oCollapseEnd = WdCollapseDirection.wdCollapseEnd;
                    object oPageBreak = WdBreakType.wdPageBreak;

                    object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
                    Range wrdRng = document.Bookmarks.get_Item(ref oEndOfDoc).Range;

                    wrdRng.Collapse(ref oCollapseEnd);
                    wrdRng.InsertBreak(ref oPageBreak);
                    wrdRng.Collapse(ref oCollapseEnd);
                    #endregion

                    //document.PageSetup.DifferentFirstPageHeaderFooter = -1;
                    
                    #region Page 1 Statement

                    Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                    object styleHeading2 = "Normal";
                    para2.Range.set_Style(ref styleHeading2);
                    para2.Range.Font.Name = "Times New Roman";
                    para2.Range.Font.Size = 12;
                    para2.Range.Font.Bold = 5;
                    para2.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                    para2.Range.Text = "LAST WILL AND TESTAMENT";
                    para2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para2.Range.ParagraphFormat.SpaceBefore = 0f;
                    para2.Range.ParagraphFormat.SpaceAfter = 0f;
                    para2.Range.InsertParagraphAfter();
               
                    para2.Range.Font.Name = "Times New Roman";
                    para2.Range.Font.Size = 12;
                    para2.Range.Font.Bold = 5;
                    para2.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                    para2.Range.Text = "OF";
                    para2.Range.ParagraphFormat.SpaceBefore = 0f;
                    para2.Range.ParagraphFormat.SpaceAfter = 0f;
                    para2.Range.InsertParagraphAfter();

                    para2.Range.Font.Name = "Times New Roman";
                    para2.Range.Font.Size = 12;
                    para2.Range.Font.Bold = 5;
                    para2.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                    para2.Range.Text = "ONG WING CHUN";
                    para2.Range.ParagraphFormat.SpaceBefore = 0f;
                    para2.Range.ParagraphFormat.SpaceAfter = 5f;
                    para2.Range.InsertParagraphAfter();

                    //para2.Range.Font.Name = "Times New Roman";
                    //para2.Range.Font.Size = 12;
                    para2.Range.Font.Bold = 0;
                    para2.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                    para2.Range.Text = Environment.NewLine + "This Will dated 			is made by me 	 (NRIC No. 	) born on 	of " + Environment.NewLine;
                    para2.Range.InsertParagraphAfter();


                    //Word.Document doc = winword.ActiveDocument;
                    //Word.Range rng = doc.Content;

                    //object oListName = "TreeList";
                    //Word.ListTemplate lstTemp = doc.ListTemplates.Add(ref oTrue, ref oListName);
                    //int i;

                    //rng.Text = "Level 1\rLevel 1.1\rLevel 1.2\rLevel 2\rLevel 2.1\rLevel 2.1.1";

                    //i = 1;
                    //lstTemp.ListLevels[i].NumberFormat = "%" + i.ToString() + ".";
                    //lstTemp.ListLevels[i].NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
                    //lstTemp.ListLevels[i].NumberPosition = winword.CentimetersToPoints(0.5f * (i - 1));
                    //lstTemp.ListLevels[i].TextPosition = winword.CentimetersToPoints(0.5f * i);
                    //i = 2;
                    //lstTemp.ListLevels[i].NumberFormat = "%" + (i - 1).ToString() + ".%" + i.ToString() + ".";
                    //lstTemp.ListLevels[i].NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
                    //lstTemp.ListLevels[i].NumberPosition = winword.CentimetersToPoints(0.5f * (i - 1));
                    //lstTemp.ListLevels[i].TextPosition = winword.CentimetersToPoints(0.5f * i);
                    //i = 3;
                    //lstTemp.ListLevels[i].NumberFormat = "%" + (i - 2).ToString() + "%" + (i - 1).ToString() + ".%" + i.ToString() + ".";
                    //lstTemp.ListLevels[i].NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
                    //lstTemp.ListLevels[i].NumberPosition = winword.CentimetersToPoints(0.5f * (i - 1));
                    //lstTemp.ListLevels[i].TextPosition = winword.CentimetersToPoints(0.5f * i);
                    //object oListApplyTo = Word.WdListApplyTo.wdListApplyToWholeList;
                    //object oListBehavior = Word.WdDefaultListBehavior.wdWord10ListBehavior;

                    //rng.ListFormat.ApplyListTemplate(lstTemp, ref oFalse, ref oListApplyTo, ref oListBehavior);


                    //Paragraph para3 = document.Content.Paragraphs.Add(ref missing);

                    //object oListName = "TreeList";
                    //Word.ListTemplate lstTemp = document.ListTemplates.Add(ref oTrue, ref oListName);
                    //int i;

                    //para3.Range.Text = "I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
                    //para3.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    //i = 1;
                    //lstTemp.ListLevels[i].NumberFormat = "%" + i.ToString() + ".";
                    //lstTemp.ListLevels[i].NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
                    //lstTemp.ListLevels[i].NumberPosition = winword.CentimetersToPoints(1f * (i - 1));
                    //lstTemp.ListLevels[i].TextPosition = winword.CentimetersToPoints(1f * i);

                    //object oListApplyTo = Word.WdListApplyTo.wdListApplyToWholeList;
                    //object oListBehavior = Word.WdDefaultListBehavior.wdWord10ListBehavior;
                    //para3.Range.ListFormat.ApplyListTemplate(lstTemp, ref oFalse, ref oListApplyTo, ref oListBehavior);

                    //winword.Selection.TypeParagraph();
                    //Paragraph para4 = document.Content.Paragraphs.Add(ref missing);
                    //para4.Range.Text = "LAST WILL AND TESTAMENT";
                    //para4.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    //para4.Range.InsertParagraphAfter();

                    //SETTING THE OUTLINE LEVEL  
                    //SELECT THE CONTENTS WHOSE OUTLINE LEVEL NEEDS TO BE CHANGED AND  
                    //SET THE VALUE  
                    //Paragraph para3 = document.Content.Paragraphs.Add(ref missing);


                    ////para3.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;

                    //para3.Range.Text = "I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
                    //para3.Range.InsertParagraphAfter();

                    //////////////////////////////////////////////////////////////////////////////////////////////
                    ///
                    //object oListName = "TreeList";
                    //Word.ListTemplate lstTemp = document.ListTemplates.Add(ref oTrue, ref oListName);
                    //int i;

                    //i = 1;
                    //lstTemp.ListLevels[i].NumberFormat = "%" + i.ToString() + ".";
                    //lstTemp.ListLevels[i].NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
                    //lstTemp.ListLevels[i].NumberPosition = winword.CentimetersToPoints(1f * (i - 1));
                    //lstTemp.ListLevels[i].TextPosition = winword.CentimetersToPoints(1f * i);

                    //object oListApplyTo = Word.WdListApplyTo.wdListApplyToWholeList;
                    //object oListBehavior = Word.WdDefaultListBehavior.wdWord10ListBehavior;
                    ////para3.Range.ListFormat.ApplyListTemplate(lstTemp, ref oFalse, ref oListApplyTo, ref oListBehavior);

                    //Word.Paragraph paragraph = null;
                    //Word.Range range = document.Content;
                    //paragraph = range.Paragraphs.Add();
                    //paragraph.Range.Text = "I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
                    //paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                    ////paragraph.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior);
                    ////paragraph.Range.ListFormat.ApplyListTemplateWithLevel(lstTemp, ref oFalse, ref oListApplyTo, ref oListBehavior);
                    //paragraph.Range.ListFormat.ApplyListTemplate(lstTemp, ref oFalse, ref oListApplyTo, ref oListBehavior);
                    //// ATTENTION: We have to outdent the paragraph AFTER its list format has been set, otherwise this has no effect.
                    //// Without this, the the indent of "Item 2" differs from the indent of "Item 1".
                    //paragraph.Outdent();

                    //paragraph.Range.InsertParagraphAfter();

                    //paragraph = range.Paragraphs.Add();
                    //paragraph.Range.Text = "Item 1.1";
                    //// ATTENTION: We have to indent the paragraph AFTER its text has been set, otherwise this has no effect.
                    //paragraph.Indent();
                    //paragraph.Range.InsertParagraphAfter();
                    //paragraph.Range.InsertParagraphAfter();
                    //paragraph.Range.Delete();
                    //paragraph = range.Paragraphs.Add();
                    //paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    //paragraph.Range.Text = "I appoint as my executor my 		 (NRIC No. 		) of 			 but if he/she is unwilling or unable to act for whatsoever reason, then I appoint as my executor my 		 of ";
                    //paragraph.Range.InsertParagraphAfter();

                    //paragraph = range.Paragraphs.Add();
                    //paragraph.Range.Text = "Item 2";
                    //paragraph.Outdent();





                    //para4.Range.Text = "I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
                    //para4.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                    //// Apply multi level list
                    //para4.Range.ListFormat.ApplyListTemplateWithLevel(
                    //    listGallery.ListTemplates[1],
                    //    ContinuePreviousList: false,
                    //    ApplyTo: WdListApplyTo.wdListApplyToWholeList,
                    //    DefaultListBehavior: WdDefaultListBehavior.wdWord10ListBehavior);

                    //para4.Range.ListFormat.ListIndent();
                    //para4.Range.Text = "2nd I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
                    //para4.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                    ListGallery listGallery = winword.ListGalleries[WdListGalleryType.wdNumberGallery];


                    Paragraph para4 = document.Content.Paragraphs.Add(ref missing);
                    para4.Range.Select();

                    // Apply multi level list
                    winword.Selection.Range.ListFormat.ApplyListTemplateWithLevel(listGallery.ListTemplates[1],ContinuePreviousList: false,
                        ApplyTo: WdListApplyTo.wdListApplyToWholeList,
                        DefaultListBehavior: WdDefaultListBehavior.wdWord10ListBehavior);

                    //First level
                    //winword.Selection.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    winword.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    winword.Selection.ParagraphFormat.SpaceBefore = 15f;
                    winword.Selection.ParagraphFormat.SpaceAfter = 15f;



                    winword.Selection.TypeText("I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.");  // Set text to key in//1.
                    winword.Selection.TypeParagraph();  // Simulate typing in MS Word
                                                        //winword.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                    // winword.Selection.TypeBackspace();

                    winword.Selection.ParagraphFormat.SpaceBefore = 15f;
                    winword.Selection.ParagraphFormat.SpaceAfter = 15f;
                    winword.Selection.TypeText("I appoint as my executor my 		 (NRIC No. 		) of 			 but if he/she is unwilling or unable to act for whatsoever reason, then I appoint as my executor my 		 of 	 	");  // Set text to key in//1.
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("In this Will unless it is specifically stated to the contrary, my Executor(s) shall also act as my Trustee(s).");
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("If my    fails to act as the Guardian of my infant children for whatsoever reason, then I appoint my              (NRIC No. 		) of 				as Guardian for as long as required by the law. However, if my 		is unable or unwilling to act for whatsoever reason, then I appoint my 				(NRIC No. 		) of				to act as Guardian for as long as required by the law.");
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("I hereby give and bequeath my entire estate (including all movable and immovable properties) whatsoever and wheresoever situated, subject to the Settlement of Debts which were not specifically disposed off hereby or by any of my codicil hereto (hereinafter referred to as “My Estate”) to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:");
                    winword.Selection.TypeParagraph();

                    winword.Selection.Range.ListFormat.ListIndent();

                    winword.Selection.TypeText("My ");
                    winword.Selection.TypeParagraph();

                    winword.Selection.Range.ListFormat.ListOutdent();
                    winword.Selection.TypeText("In the event any beneficiary(ies) predeceases me, the entitlement that such predeceased beneficiary(ies) would have received shall instead be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to herein. ");
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("If he/she does not survive me, then the benefit he/she would have received shall be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to under my residuary estate herein.");
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("I give my property known as 			 to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:");
                    winword.Selection.TypeParagraph();

                    winword.Selection.Range.ListFormat.ListIndent();
                    winword.Selection.TypeText("My 		(NRIC No.	)(1/2)");
                    winword.Selection.TypeParagraph();
                    winword.Selection.TypeText("My 		(NRIC No.	)(1/2)");
                    winword.Selection.TypeParagraph();

                    // winword.Selection.TypeBackspace();
                    winword.Selection.Range.ListFormat.ListOutdent();

                    winword.Selection.TypeText("In the event any beneficiary(ies) predeceases me, the entitlement that such predeceased beneficiary(ies) would have received shall instead be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to herein. ");
                    winword.Selection.InsertParagraphAfter();

                    winword.Selection.TypeText("If he/she does not survive me, then the benefit he/she would have received shall be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to under my residuary estate herein.");
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("I direct that any sums required to discharge a charge or to withdraw a lien attached to this property shall be paid out of my residuary estate.");
                    winword.Selection.TypeParagraph();

    
                    winword.Selection.TypeText("I give my property known as 			 to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:");
                    winword.Selection.TypeParagraph();
                    winword.Selection.TypeBackspace();

                    //winword.Selection.InsertParagraphAfter();
                    //winword.Selection.type
                    //winword.Selection.TypeBackspace();


                    //// Go to 2nd level
                    //winword.Selection.Range.ListFormat.ListIndent();

                    //winword.Selection.TypeText("Child Item A.1");//a.
                    //winword.Selection.TypeParagraph();
                    //winword.Selection.TypeText("Child Item A.2");//b.
                    //winword.Selection.TypeParagraph();

                    //// Back to 1st level
                    //winword.Selection.Range.ListFormat.ListOutdent();
                    //winword.Selection.TypeText("Root Item B");//2.
                    //winword.Selection.TypeParagraph();

                    //// Go to 2nd level
                    //winword.Selection.Range.ListFormat.ListIndent();
                    //winword.Selection.TypeText("Child Item B.1");//a.
                    //winword.Selection.TypeParagraph();
                    //winword.Selection.TypeText("Child Item B.2");//b.
                    //winword.Selection.TypeParagraph();

                    ////// Delete empty item generated by winword.Selection.TypeParagraph();
                    ////winword.Selection.TypeBackspace();

                    //winword.Selection.TypeParagraph();  // Simulate typing in MS Word

                    //Paragraph para5 = document.Content.Paragraphs.Add(ref missing);
                    //para5.Range.Select();

                    //// Apply multi level list
                    //winword.Selection.Range.ListFormat.ApplyListTemplateWithLevel(listGallery.ListTemplates[1], ContinuePreviousList: false,
                    //    ApplyTo: WdListApplyTo.wdListApplyToWholeList,
                    //    DefaultListBehavior: WdDefaultListBehavior.wdWord10ListBehavior);

                    ////First level
                    ////winword.Selection.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    //winword.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;






                    #endregion

                    #region Header Page No

                    ////Insert Next Page section break so that numbering can start at 1
                    //Word.Range rngPageNum = winword.Selection.Range;
                    //rngPageNum.InsertBreak(WdBreakType.wdSectionBreakNextPage);


                    foreach (Section section in document.Sections)
                    {
                        //Get the header range and add the header details.
                        Range headerRange = document.Sections[1].Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                        headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        headerRange.Font.ColorIndex = WdColorIndex.wdBlack;
                        headerRange.Font.Size = 11;
                        headerRange.Font.Name = "Calibri (Body)";

                        //use the field SectionPages instead of NumPages
                        object TotalPages = WdFieldType.wdFieldSectionPages;
                        object CurrentPage = WdFieldType.wdFieldPage;

                        headerRange.Fields.Add(headerRange, ref CurrentPage, ref missing, true);
                        headerRange.InsertBefore("PAGE ");
                        headerRange.Collapse(WdCollapseDirection.wdCollapseEnd);
                        //headerRange.Fields.Add(headerRange, ref TotalPages, ref missing, false);

                    }


                    #endregion

                    #region Footer Text

                    foreach (Section section in document.Sections)
                    {
                        //Get the header range and add the header details.  
                        Range footerRange = document.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        footerRange.Fields.Add(footerRange, WdFieldType.wdFieldPage);
                        footerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        footerRange.Font.ColorIndex = WdColorIndex.wdBlack;
                        footerRange.Font.Size = 11;
                        footerRange.Font.Name = "Calibri (Body)";
                        footerRange.Text = "TESTATOR                                                      WITHNESS 1                                                       WITHNESS 2";
                    }


                    #endregion

                    #region Saving Document
                    //Save the document  
                    object filename = sfd.FileName;
                    document.SaveAs2(ref filename);
                    #endregion

                    #region Release Object
                    document.Close(ref missing, ref missing, ref missing);
                    document = null;
                    winword.Quit(ref missing, ref missing, ref missing);
                    winword = null;
                    #endregion

                    frmLoading.CloseForm();

                    //MessageBox.Show("Document created successfully !");

                    if (File.Exists(sfd.FileName))
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);

                        System.Windows.Forms.Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    frmLoading.CloseForm();
                }
            }
        }

        private void txtHeaderRow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void LoadDetail(int rowIndex)
        {
            btnWill.Visible = false;

            if (rowIndex >= 0)
            {
                DataGridView dgv = dgvTestatorList;

                string index = dgv.Rows[rowIndex].Cells[HEADER_INDEX].Value.ToString();
                string TimeStamp = dgv.Rows[rowIndex].Cells[HEADER_TIMESTAMP].Value.ToString();
                string Testator = dgv.Rows[rowIndex].Cells[HEADER_TESTATOR].Value.ToString();


                dgvDetail.DataSource = null;

                if (dt_DataSource != null && dt_DataSource.Rows.Count > 0)
                {
                    DataTable dt = NewWillDetailTable();
                    DataRow detailRow;

                    int indexNo = 1;
                    foreach (DataRow row in dt_DataSource.Rows)
                    {
                        string source_Index = row[HEADER_INDEX].ToString();
                        string source_TimeStamp = row[HEADER_TIMESTAMP].ToString();
                        string source_Testator = row[PART_A + PART_A_TESTATOR + " " + HEADER_FULLNAME].ToString();

                        bool dataMatched = source_Index == index;
                        dataMatched &= TimeStamp == source_TimeStamp;
                        dataMatched &= Testator == source_Testator;

                        if(dataMatched)
                        {
                            int sourceRowIndex = dt_DataSource.Rows.IndexOf(row);

                            for(int col = 0; col < dt_DataSource.Columns.Count; col ++)
                            {
                                detailRow = dt.NewRow();
                                detailRow[HEADER_INDEX] = indexNo++;

                                detailRow[HEADER_INFORMATION] = dt_DataSource.Columns[col].ColumnName;
                                detailRow[HEADER_DESCRIPTION] = dt_DataSource.Rows[sourceRowIndex][col].ToString() ;

                                dt.Rows.Add(detailRow);
                            }
                            
                        }
                       

                    }

                    if(dt != null && dt.Rows.Count > 0)
                    {
                        btnWill.Visible = true;
                    }
                    else
                    {
                        btnWill.Visible = false;
                    }

                    dgvDetail.DataSource = dt;
                    DgvUIEdit(dgvDetail);
                    dgvDetail.ClearSelection();
                }
            }
        }

        private void dgvTestatorList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadDetail(e.RowIndex);
        }

        private void dgvTestatorList_SelectionChanged(object sender, EventArgs e)
        {
            //int rowIndex = -1;
            //DataGridView dgv = dgvTestatorList;

            //if (dgv.SelectedRows.Count <= 0 || dgv.CurrentRow == null)
            //{
            //    rowIndex = -1;
            //}
            //else
            //{
            //    rowIndex = dgv.CurrentRow.Index;

            //    LoadDetail(rowIndex);
            //}
        }

        private void btnWill_Click(object sender, EventArgs e)
        {
            
            DataTable dt = (DataTable)dgvDetail.DataSource;

            if (dt != null && dt.Rows.Count > 0)
            {              FinexWill frm = new FinexWill(dt)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Detail data not found.");
            }
        }

        private void FinexDataUpload_Load(object sender, EventArgs e)
        {
            #region Testing Shortcut
            path = "D:\\Users\\Jun\\Desktop\\(PERSONAL)\\SmartBase8\\Finex & CO\\Data\\BASIC WILL QUESTIONNAIRE .xlsx";

            if (path != null)
            {
                ReadExcel(path);
                btnWill.Visible = false;

                LoadDetail(0);

                DataTable dt = (DataTable)dgvDetail.DataSource;

                if (dt != null && dt.Rows.Count > 0)
                {
                    FinexWill frm = new FinexWill(dt)
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };

                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Detail data not found.");
                }
            }
            else
            {
                MessageBox.Show("path not found");
            }
            #endregion
        }

        private void dgvDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvDetail;

            int col = e.ColumnIndex;
            int row = e.RowIndex;

            lblSubList.Text = "COL: " + col + " ROW: " + row;

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                

                if(dgv.Columns[col].Name.Contains(HEADER_DESCRIPTION))
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = false;
                }
                else
                {
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.Rows[row].Cells[col].Selected = true;
                    dgv.ReadOnly = true;

                }

            }
        }

        private void FinexDataUpload_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Application.Exit();
            frmLogIn frm = new frmLogIn("Emmeline");
            frm.Show();
        }
    }
}
