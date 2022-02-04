using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Reflection;
using System.IO;
using DataTable = System.Data.DataTable;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using Font = System.Drawing.Font;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using FactoryManagementSoftware.Properties;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Globalization;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using Spire.Pdf;
using Spire.Pdf.Graphics;

namespace FactoryManagementSoftware.UI
{
    public partial class FinexWill : Form
    {
        Tool tool = new Tool();
        Text text = new Text();

        DataTable DT_DATA;
        private bool INDEX_PENDING_REAARANGE = false;

        private string MENU_PARA_REMOVE = "remove";
        private string MENU_PARA_ADD_SECTION = "add section";
        private string MENU_PARA_ADD_OUTDENT = "add outdent";
        private string MENU_PARA_ADD_INDENT = "add indent";
        private string MENU_PARA_ADD_SECOND_INDENT = "add second indent";

        private string MENU_SECTION_REMOVE = "remove section";

        public FinexWill()
        {
            InitializeComponent();
            GenerateWill();
        }

        public FinexWill(DataTable dt)
        {
            InitializeComponent();

            GenerateWill(dt);
            DT_DATA = dt;
        }

        #region Data String

        private DataTable dt_Will;

        private string dtHeader_TableCode = "TBL CODE";
        private string dtHeader_Index_Enable = "#Index Enable";
        private string dtHeader_Outdent_Index = "#Outdent";
        private string dtHeader_Indent_Index = "#Indent";
        private string dtHeader_Indent_Second_Index = "#Indent Second";
        private string dtHeader_Index = "INDEX";
        private string dtHeader_Para = "PARAGRAPH";
        //private string dtHeader_ListLayout = "LIST LAYOUT";
        //private string dtHeader_ListLayout_Indent_Text = "INDENT";
        //private string dtHeader_ListLayout_Outdent_Text = "OUTDENT";

        string Testator_Name = "ONG WING CHUN";
        string Testator_IC = "900624145747";
        string Testator_Address = "ASDASDASDASDSAD";
        string Testator_Birhtday = "";
        DateTime Testator_BirthDay_Datetime = DateTime.MaxValue;

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

        private string HEADER_PLANNER_IN_CHARGE = "PLANNER IN CHARGE";
        private string HEADER_CONTACT = "CONTACT";
        private string HEADER_EP_Code = "PLANNER CODE";
        private string HEADER_EP_PACKAGE = "PACKAGE";
        private string HEADER_DEATH_OF_MAIN_BENEFICIARY = "DEATH OF MAIN BENEFICIARY";
        private string HEADER_ADDITIONAL_INSTRUCTIONS = "ADDITIONAL INSTRUCTIONS";

        private string PROPERTY_SOLENAME = "SOLE";
        private string PROPERTY_JOINTNAME = "JOINT";

        private string TO_MAIN_BENEFICIARIES = "Give to the surviving main beneficiaries in proportion to their own shares";
        private string TO_CHILDREN = "Give to the beneficiary's own children equally";

        private string GUARDIAN_EXECUTOR_1 = "Same as Executor No. 1";
        private string GUARDIAN_EXECUTOR_2 = "Same as Executor No. 2";

        private string RESTING_METHOD_BURIAL = "BURIAL";
        private string RESTING_METHOD_CREMATION = "CREMATION";





        #endregion

        private void GenerateWill()
        {

            DateTime dateNow = DateTime.Now.Date;
            string Testator_FullName = "EMMELINE LEE";
            string Testator_IC = "950818875014";
            string Testator_Birhtday = "18th AUGUST,1995";
            string Testator_Address = "43, Jalan BS 2a";



            #region generate will
            string newLine = Environment.NewLine;
            string WillTitle = "LAST WILL AND TESTAMENT" + newLine+"OF"+newLine+ Testator_FullName + newLine;
            string WillStatement = "This Will dated "+ dateNow.ToString("dd MMMM,yyyy") + " is made by me " + Testator_FullName + " (NRIC No. " + Testator_IC + " ) born on "+ Testator_Birhtday + " of "+ Testator_Address;

            string firstSentence = @"
1.    I revoke all earlier wills and exclude my movable and immovable assets located in 
      any countryin which I have a separate Will made according to the laws of that country 
      before my demise.In the event I do not have a separate Will made according to the 
      laws of a particular country where my assets are located, then those assets shall 
      form part of this Will and shall be distributed accordingly. I hereby declare that 
      I am domiciled in Malaysia.";

            //rtbWill.Text = WillTitle + newLine + WillStatement + newLine + firstSentence;
            #endregion
        }

        private DataTable NewWillTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(dtHeader_TableCode, typeof(int));
            dt.Columns.Add(dtHeader_Index_Enable, typeof(bool));
            dt.Columns.Add(dtHeader_Outdent_Index, typeof(int));
            dt.Columns.Add(dtHeader_Indent_Index, typeof(int));
            dt.Columns.Add(dtHeader_Indent_Second_Index, typeof(int));
            dt.Columns.Add(dtHeader_Index, typeof(string));
            dt.Columns.Add(dtHeader_Para, typeof(string));
            //dt.Columns.Add(dtHeader_ListLayout, typeof(string));

            return dt;
        }

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            //dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //dgv.Columns[dtHeader_TableCode].Visible = false;
            //dgv.Columns[dtHeader_Outdent_Index].Visible = false;
            //dgv.Columns[dtHeader_Indent_Index].Visible = false;
            //dgv.Columns[dtHeader_Indent_Second_Index].Visible = false;
            //dgv.Columns[dtHeader_Index_Enable].Visible = false;

            dgv.Columns[dtHeader_TableCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[dtHeader_Outdent_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[dtHeader_Indent_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[dtHeader_Indent_Second_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[dtHeader_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[dtHeader_Para].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dgv.Columns[dtHeader_Para].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv.Columns[dtHeader_TableCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dtHeader_Outdent_Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dtHeader_Indent_Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns[dtHeader_Indent_Second_Index].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgv.Columns[dtHeader_Para].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns[dtHeader_Para].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //if(dgv.Rows.Count > 0)
            //{
            //    for(int i = 0; i < dgv.Rows.Count -1; i++)
            //    {
            //        DataGridViewRow row = dgv.Rows[i];
            //        row.MinimumHeight = 60;
            //    }
            //}
            ContentHighlight(dgv);
        }

        private int GetNewWillTableCode()
        {
            int NewTblCode = 1;

            if(dt_Will != null && dt_Will.Rows.Count > 0)
            {
                DataRow lastRow = dt_Will.Rows[dt_Will.Rows.Count - 1];

                NewTblCode = int.TryParse(lastRow[dtHeader_TableCode].ToString(), out int i) ? i+1 : 1;
            }

            return NewTblCode;
        }

        private string GetIndexSring(int outDentIndex, int inDentIndex, int SecondIndentIndex, bool indexEnable)
        {
            if(indexEnable)
            {
                string OutDent = "";
                string Indent = "";
                string SecondIndent = "";

                if (outDentIndex > 0)
                {
                    OutDent = outDentIndex + ". ";
                }

                if (inDentIndex > 0)
                {
                    Indent = ((char)(inDentIndex + 64)).ToString().ToLower() + ") ";
                }

                if (SecondIndentIndex > 0)
                {
                    SecondIndent = "(" + ToRoman(SecondIndentIndex).ToLower() + ")";
                }
                //ToRoman(indexNo).ToLower()
                return OutDent + Indent + SecondIndent;
            }
            else
            {
                return "";
            }
           

        }

        private void WillTableInsert(bool indexEnable,int Outdent_Index, int Intent_Index, string Para)
        {
            DataRow row = dt_Will.NewRow();

            row[dtHeader_TableCode] = GetNewWillTableCode();
            row[dtHeader_Index_Enable] = indexEnable;
            row[dtHeader_Outdent_Index] = Outdent_Index;
            row[dtHeader_Indent_Index] = Intent_Index;
            row[dtHeader_Indent_Second_Index] = 0;
            row[dtHeader_Index] = GetIndexSring(Outdent_Index,Intent_Index, 0, indexEnable);
            row[dtHeader_Para] = Para;

            dt_Will.Rows.Add(row);
        }

        private void WillTableInsert(bool indexEnable, int Outdent_Index, int Intent_Index, int Intent_Second_Index, string Para)
        {
            DataRow row = dt_Will.NewRow();

            row[dtHeader_TableCode] = GetNewWillTableCode();
            row[dtHeader_Outdent_Index] = Outdent_Index;
            row[dtHeader_Index_Enable] = indexEnable;
            row[dtHeader_Indent_Index] = Intent_Index;
            row[dtHeader_Indent_Second_Index] = Intent_Second_Index;
            row[dtHeader_Index] = GetIndexSring(Outdent_Index, Intent_Index, Intent_Second_Index, indexEnable);
            row[dtHeader_Para] = Para;

            dt_Will.Rows.Add(row);
        }


        private int MaleOrFemale(string IC)
        {
            int MaleOrFemale = 1;

            if(IC.Length > 0)
            {
                int lastICDigit = int.TryParse(IC[IC.Length - 1].ToString(), out int i) ? i : 0;

                if (lastICDigit % 2 == 0)
                {
                    //is even
                    MaleOrFemale = 0;
                }
            }

            return MaleOrFemale;
        }

        private void ContentHighlight(DataGridView dgv)
        {

            for (int i = 0; i < dgv.RowCount; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    int rowIndex = i;
                    int colIndex = j;


                    string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();
                    string colName = dgv.Columns[colIndex].Name;

                    if (colName.Contains(dtHeader_Para) && (cellValue.Contains("[") || cellValue.Contains("]")))
                    {
                        dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.Yellow;

                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.White;

                    }

                }
            }
        }

        private void GenerateWill(DataTable dt_Source)
        {
            #region Data String Declare

            dt_Will = NewWillTable();

            DateTime dateNow = DateTime.Now.Date;

            string Executor_1_FullName = "";
            string Executor_1_IC = "";
            string Executor_1_HP = "";
            string Executor_1_Email = "";
            string Executor_1_Relationship = "";
            string Executor_1_Address = "";

            string Executor_2_FullName = "";
            string Executor_2_IC = "";
            string Executor_2_HP = "";
            string Executor_2_Email = "";
            string Executor_2_Relationship = "";
            string Executor_2_Address = "";

            string TotalBeneficiaries = "";
            string TotalSubstituteBeneficiaries = "";
            string DeathOfMainBeneficiary = "";

            string Beneficiary_1_FullName = "";
            string Beneficiary_1_IC = "";
            string Beneficiary_1_Relationship = "";
            string Beneficiary_1_SharePercentage = "";

            string Beneficiary_2_FullName = "";
            string Beneficiary_2_IC = "";
            string Beneficiary_2_Relationship = "";
            string Beneficiary_2_SharePercentage = "";

            string Beneficiary_3_FullName = "";
            string Beneficiary_3_IC = "";
            string Beneficiary_3_Relationship = "";
            string Beneficiary_3_SharePercentage = "";
           
            string Beneficiary_4_FullName = "";
            string Beneficiary_4_IC = "";
            string Beneficiary_4_Relationship = "";
            string Beneficiary_4_SharePercentage = "";

            string Beneficiary_5_FullName = "";
            string Beneficiary_5_IC = "";
            string Beneficiary_5_Relationship = "";
            string Beneficiary_5_SharePercentage = "";

            string Substitute_Beneficiary_1_FullName = "";
            string Substitute_Beneficiary_1_IC = "";
            string Substitute_Beneficiary_1_Relationship = "";
            string Substitute_Beneficiary_1_SharePercentage = "";

            string Substitute_Beneficiary_2_FullName = "";
            string Substitute_Beneficiary_2_IC = "";
            string Substitute_Beneficiary_2_Relationship = "";
            string Substitute_Beneficiary_2_SharePercentage = "";

            string Substitute_Beneficiary_3_FullName = "";
            string Substitute_Beneficiary_3_IC = "";
            string Substitute_Beneficiary_3_Relationship = "";
            string Substitute_Beneficiary_3_SharePercentage = "";

            string Substitute_Beneficiary_4_FullName = "";
            string Substitute_Beneficiary_4_IC = "";
            string Substitute_Beneficiary_4_Relationship = "";
            string Substitute_Beneficiary_4_SharePercentage = "";

            string Substitute_Beneficiary_5_FullName = "";
            string Substitute_Beneficiary_5_IC = "";
            string Substitute_Beneficiary_5_Relationship = "";
            string Substitute_Beneficiary_5_SharePercentage = "";

            string Guardian_1_FullName = "";
            string Guardian_1_IC = "";
            string Guardian_1_HP = "";
            string Guardian_1_Email = "";
            string Guardian_1_Address = "";
            string Guardian_1_State = "";
            string Guardian_1_Postcode = "";
            string Guardian_1_Relationship = "";

            string Guardian_2_FullName = "";
            string Guardian_2_IC = "";
            string Guardian_2_HP = "";
            string Guardian_2_Email = "";
            string Guardian_2_Address = "";
            string Guardian_2_State = "";
            string Guardian_2_Postcode = "";
            string Guardian_2_Relationship = "";

            string ImmovableProperties_1_Address = "";
            string ImmovableProperties_1_State = "";
            string ImmovableProperties_1_Postcode = "";
            string ImmovableProperties_1_TitleDetail = "";
            string ImmovableProperties_1_Ownership = "";
            string ImmovableProperties_1_Beneficiaries = "";

            string ImmovableProperties_2_Address = "";
            string ImmovableProperties_2_State = "";
            string ImmovableProperties_2_Postcode = "";
            string ImmovableProperties_2_TitleDetail = "";
            string ImmovableProperties_2_Ownership = "";
            string ImmovableProperties_2_Beneficiaries = "";

            string ImmovableProperties_3_Address = "";
            string ImmovableProperties_3_State = "";
            string ImmovableProperties_3_Postcode = "";
            string ImmovableProperties_3_TitleDetail = "";
            string ImmovableProperties_3_Ownership = "";
            string ImmovableProperties_3_Beneficiaries = "";

            //string Wife_HouseholdExpenses = "";
            //string Children_HouseholdExpenses = "";
            //string Parents_HouseholdExpenses = "";
            //string Guardian_Allowance = "";

            string Insurance_1_Company = "";
            string Insurance_1_PolicyNo = "";

            string Insurance_2_Company = "";
            string Insurance_2_PolicyNo = "";

            string FinalRestingPlace_Religion = "";
            string FinalRestingPlace_Method = "";

            string Additional_Assets = "";

            int index_Outdent = 1;

            string newLine = Environment.NewLine + Environment.NewLine;

            #endregion

            #region Getting Data

            foreach (DataRow row in dt_Source.Rows)
            {
                string information = row[HEADER_INFORMATION].ToString();
                string description = row[HEADER_DESCRIPTION].ToString();

                //if(string.IsNullOrEmpty(description))
                //{
                //    description = "[EMPTY]";
                //}

                #region Testator

                if (information.Contains(PART_A_TESTATOR) && information.Contains(HEADER_FULLNAME))
                {
                    Testator_Name = description.ToUpper();
                }
                else if (information.Contains(PART_A_TESTATOR) && information.Contains(HEADER__IC))
                {
                    Testator_IC = description;
                }
                else if (information.Contains(PART_A_TESTATOR) && information.Contains(HEADER__BIRTHDAY))
                {
                    Testator_Birhtday = description;
                    Testator_BirthDay_Datetime = DateTime.TryParse(Testator_Birhtday, out Testator_BirthDay_Datetime) ? Testator_BirthDay_Datetime : DateTime.MaxValue;
                }
                else if (information.Contains(PART_A_TESTATOR) && information.Contains(HEADER__ADDRESS))
                {
                    Testator_Address = description.ToUpper();
                }

                #endregion

                #region Executor 1

                else if(information.Contains(PART_B1_EXECUTOR_1) && information.Contains(HEADER_FULLNAME))
                {
                    Executor_1_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_B1_EXECUTOR_1) && information.Contains(HEADER__IC))
                {
                    Executor_1_IC = description;
                }
                else if (information.Contains(PART_B1_EXECUTOR_1) && information.Contains(HEADER__HP))
                {
                    Executor_1_HP = description;
                }
                else if (information.Contains(PART_B1_EXECUTOR_1) && information.Contains(HEADER__EMAIL))
                {
                    Executor_1_Email = description;
                }
                else if (information.Contains(PART_B1_EXECUTOR_1) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Executor_1_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_B1_EXECUTOR_1) && information.Contains(HEADER__ADDRESS))
                {
                    Executor_1_Address = description.ToUpper();
                }

                #endregion

                #region Executor 2

                else if (information.Contains(PART_B2_EXECUTOR_2) && information.Contains(HEADER_FULLNAME))
                {
                    Executor_2_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_B2_EXECUTOR_2) && information.Contains(HEADER__IC))
                {
                    Executor_2_IC = description;
                }
                else if (information.Contains(PART_B2_EXECUTOR_2) && information.Contains(HEADER__HP))
                {
                    Executor_2_HP = description;
                }
                else if (information.Contains(PART_B2_EXECUTOR_2) && information.Contains(HEADER__EMAIL))
                {
                    Executor_2_Email = description;
                }
                else if (information.Contains(PART_B2_EXECUTOR_2) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Executor_2_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_B2_EXECUTOR_2) && information.Contains(HEADER__ADDRESS))
                {
                    Executor_2_Address = description.ToUpper();
                }

                #endregion

                #region Beneficiaries

                else if (information.Contains(PART_C0) && information.Contains(HEADER__TOTAL_BENEFICIARIES))
                {
                    TotalBeneficiaries = description;
                }
                else if (information.Contains(PART_C6) && information.Contains(HEADER_DEATH_OF_MAIN_BENEFICIARY))
                {
                    DeathOfMainBeneficiary = description;
                }
                #region Beneficiary 1

                else if (information.Contains(PART_C1) && information.Contains(HEADER_FULLNAME))
                {
                    Beneficiary_1_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_C1) && information.Contains(HEADER__IC))
                {
                    Beneficiary_1_IC = description;
                }
                else if (information.Contains(PART_C1) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Beneficiary_1_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_C1) && information.Contains(HEADER__SHARE_PERCENTAGE))
                {
                    Beneficiary_1_SharePercentage = description;
                }

                #endregion

                #region Beneficiary 2

                else if (information.Contains(PART_C2) && information.Contains(HEADER_FULLNAME))
                {
                    Beneficiary_2_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_C2) && information.Contains(HEADER__IC))
                {
                    Beneficiary_2_IC = description;
                }
                else if (information.Contains(PART_C2) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Beneficiary_2_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_C2) && information.Contains(HEADER__SHARE_PERCENTAGE))
                {
                    Beneficiary_2_SharePercentage = description;
                }

                #endregion

                #region Beneficiary 3

                else if (information.Contains(PART_C3) && information.Contains(HEADER_FULLNAME))
                {
                    Beneficiary_3_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_C3) && information.Contains(HEADER__IC))
                {
                    Beneficiary_3_IC = description;
                }
                else if (information.Contains(PART_C3) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Beneficiary_3_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_C3) && information.Contains(HEADER__SHARE_PERCENTAGE))
                {
                    Beneficiary_3_SharePercentage = description;
                }

                #endregion

                #region Beneficiary 4

                else if (information.Contains(PART_C4) && information.Contains(HEADER_FULLNAME))
                {
                    Beneficiary_4_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_C4) && information.Contains(HEADER__IC))
                {
                    Beneficiary_4_IC = description;
                }
                else if (information.Contains(PART_C4) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Beneficiary_4_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_C4) && information.Contains(HEADER__SHARE_PERCENTAGE))
                {
                    Beneficiary_4_SharePercentage = description;
                }

                #endregion

                #region Beneficiary 5

                else if (information.Contains(PART_C5) && information.Contains(HEADER_FULLNAME))
                {
                    Beneficiary_5_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_C5) && information.Contains(HEADER__IC))
                {
                    Beneficiary_5_IC = description;
                }
                else if (information.Contains(PART_C5) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Beneficiary_5_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_C5) && information.Contains(HEADER__SHARE_PERCENTAGE))
                {
                    Beneficiary_5_SharePercentage = description;
                }

                #endregion

                if (information.Contains(PART_C1_0) && information.Contains(HEADER__TOTAL_BENEFICIARIES))
                {
                    TotalSubstituteBeneficiaries = description;
                }

                #region Substitude Substitute_Beneficiary 1

                else if (information.Contains(PART_C1_1) && information.Contains(HEADER_FULLNAME))
                {
                    Substitute_Beneficiary_1_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_C1_1) && information.Contains(HEADER__IC))
                {
                    Substitute_Beneficiary_1_IC = description;
                }
                else if (information.Contains(PART_C1_1) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Substitute_Beneficiary_1_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_C1_1) && information.Contains(HEADER__SHARE_PERCENTAGE))
                {
                    Substitute_Beneficiary_1_SharePercentage = description;
                }

                #endregion

                #region Substitude Substitute_Beneficiary 2

                else if (information.Contains(PART_C1_2) && information.Contains(HEADER_FULLNAME))
                {
                    Substitute_Beneficiary_2_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_C1_2) && information.Contains(HEADER__IC))
                {
                    Substitute_Beneficiary_2_IC = description;
                }
                else if (information.Contains(PART_C1_2) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Substitute_Beneficiary_2_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_C1_2) && information.Contains(HEADER__SHARE_PERCENTAGE))
                {
                    Substitute_Beneficiary_2_SharePercentage = description;
                }

                #endregion

                #region Substitude Substitute_Beneficiary 3

                else if (information.Contains(PART_C1_3) && information.Contains(HEADER_FULLNAME))
                {
                    Substitute_Beneficiary_3_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_C1_3) && information.Contains(HEADER__IC))
                {
                    Substitute_Beneficiary_3_IC = description;
                }
                else if (information.Contains(PART_C1_3) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Substitute_Beneficiary_3_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_C1_3) && information.Contains(HEADER__SHARE_PERCENTAGE))
                {
                    Substitute_Beneficiary_3_SharePercentage = description;
                }

                #endregion

                #region Substitude Substitute_Beneficiary 4

                else if (information.Contains(PART_C1_4) && information.Contains(HEADER_FULLNAME))
                {
                    Substitute_Beneficiary_4_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_C1_4) && information.Contains(HEADER__IC))
                {
                    Substitute_Beneficiary_4_IC = description;
                }
                else if (information.Contains(PART_C1_4) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Substitute_Beneficiary_4_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_C1_4) && information.Contains(HEADER__SHARE_PERCENTAGE))
                {
                    Substitute_Beneficiary_4_SharePercentage = description;
                }

                #endregion

                #region Substitude Substitute_Beneficiary 5

                else if (information.Contains(PART_C1_5) && information.Contains(HEADER_FULLNAME))
                {
                    Substitute_Beneficiary_5_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_C1_5) && information.Contains(HEADER__IC))
                {
                    Substitute_Beneficiary_5_IC = description;
                }
                else if (information.Contains(PART_C1_5) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Substitute_Beneficiary_5_Relationship = description.ToLower();
                }
                else if (information.Contains(PART_C1_5) && information.Contains(HEADER__SHARE_PERCENTAGE))
                {
                    Substitute_Beneficiary_5_SharePercentage = description;
                }

                #endregion

                #endregion

                #region Guardian 1

                else if (information.Contains(PART_D1_GUARDIAN_1) && information.Contains(HEADER_FULLNAME))
                {
                    Guardian_1_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_D1_GUARDIAN_1) && information.Contains(HEADER__IC))
                {
                    Guardian_1_IC = description;
                }
                else if (information.Contains(PART_D1_GUARDIAN_1) && information.Contains(HEADER__HP))
                {
                    Guardian_1_HP = description;
                }
                else if (information.Contains(PART_D1_GUARDIAN_1) && information.Contains(HEADER__EMAIL))
                {
                    Guardian_1_Email = description;
                }
                else if (information.Contains(PART_D1_GUARDIAN_1) && information.Contains(HEADER__ADDRESS))
                {
                    Guardian_1_Address = description.ToUpper();
                }
                else if (information.Contains(PART_D1_GUARDIAN_1) && information.Contains(HEADER__STATE))
                {
                    Guardian_1_State = description;
                }
                else if (information.Contains(PART_D1_GUARDIAN_1) && information.Contains(HEADER__POSTCODE))
                {
                    Guardian_1_Postcode = description;
                }
                else if (information.Contains(PART_D1_GUARDIAN_1) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Guardian_2_Relationship = description.ToLower();
                }


                #endregion

                #region Guardian 2

                else if (information.Contains(PART_D2_GUARDIAN_2) && information.Contains(HEADER_FULLNAME))
                {
                    Guardian_2_FullName = description.ToUpper();
                }
                else if (information.Contains(PART_D2_GUARDIAN_2) && information.Contains(HEADER__IC))
                {
                    Guardian_2_IC = description;
                }
                else if (information.Contains(PART_D2_GUARDIAN_2) && information.Contains(HEADER__HP))
                {
                    Guardian_2_HP = description;
                }
                else if (information.Contains(PART_D2_GUARDIAN_2) && information.Contains(HEADER__EMAIL))
                {
                    Guardian_2_Email = description;
                }
                else if (information.Contains(PART_D2_GUARDIAN_2) && information.Contains(HEADER__ADDRESS))
                {
                    Guardian_2_Address = description.ToUpper();
                }
                else if (information.Contains(PART_D2_GUARDIAN_2) && information.Contains(HEADER__STATE))
                {
                    Guardian_2_State = description;
                }
                else if (information.Contains(PART_D2_GUARDIAN_2) && information.Contains(HEADER__POSTCODE))
                {
                    Guardian_2_Postcode = description;
                }
                else if (information.Contains(PART_D2_GUARDIAN_2) && information.Contains(HEADER__RELATIONSHIP))
                {
                    Guardian_2_Relationship = description.ToLower();
                }


                #endregion

                #region Immovable Properties 1

                else if (information.Contains(PART_E1_PROPERTIES_1) && information.Contains(HEADER__PROPERTY_ADDRESS))
                {
                    ImmovableProperties_1_Address = description.ToUpper();
                }
                else if (information.Contains(PART_E1_PROPERTIES_1) && information.Contains(HEADER__STATE))
                {
                    ImmovableProperties_1_State = description;
                }
                else if (information.Contains(PART_E1_PROPERTIES_1) && information.Contains(HEADER__POSTCODE))
                {
                    ImmovableProperties_1_Postcode = description;
                }
                else if (information.Contains(PART_E1_PROPERTIES_1) && information.Contains(HEADER__TITLE_DETAILS))
                {
                    ImmovableProperties_1_TitleDetail = description;
                }
                else if (information.Contains(PART_E1_PROPERTIES_1) && information.Contains(HEADER__OWNERSHIP))
                {
                    ImmovableProperties_1_Ownership = description;
                }
                else if (information.Contains(PART_E1_PROPERTIES_1) && information.Contains(HEADER__PROPERTY_BENEFICIARIES))
                {
                    ImmovableProperties_1_Beneficiaries = description;
                }

                #endregion

                #region Immovable Properties 2

                else if (information.Contains(PART_E2_PROPERTIES_2) && information.Contains(HEADER__PROPERTY_ADDRESS))
                {
                    ImmovableProperties_2_Address = description.ToUpper();
                }
                else if (information.Contains(PART_E2_PROPERTIES_2) && information.Contains(HEADER__STATE))
                {
                    ImmovableProperties_2_State = description;
                }
                else if (information.Contains(PART_E2_PROPERTIES_2) && information.Contains(HEADER__POSTCODE))
                {
                    ImmovableProperties_2_Postcode = description;
                }
                else if (information.Contains(PART_E2_PROPERTIES_2) && information.Contains(HEADER__TITLE_DETAILS))
                {
                    ImmovableProperties_2_TitleDetail = description;
                }
                else if (information.Contains(PART_E2_PROPERTIES_2) && information.Contains(HEADER__OWNERSHIP))
                {
                    ImmovableProperties_2_Ownership = description;
                }
                else if (information.Contains(PART_E2_PROPERTIES_2) && information.Contains(HEADER__PROPERTY_BENEFICIARIES))
                {
                    ImmovableProperties_2_Beneficiaries = description;
                }

                #endregion

                #region Immovable Properties 3

                else if (information.Contains(PART_E3_PROPERTIES_3) && information.Contains(HEADER__PROPERTY_ADDRESS))
                {
                    ImmovableProperties_3_Address = description.ToUpper();
                }
                else if (information.Contains(PART_E3_PROPERTIES_3) && information.Contains(HEADER__STATE))
                {
                    ImmovableProperties_3_State = description;
                }
                else if (information.Contains(PART_E3_PROPERTIES_3) && information.Contains(HEADER__POSTCODE))
                {
                    ImmovableProperties_3_Postcode = description;
                }
                else if (information.Contains(PART_E3_PROPERTIES_3) && information.Contains(HEADER__TITLE_DETAILS))
                {
                    ImmovableProperties_3_TitleDetail = description;
                }
                else if (information.Contains(PART_E3_PROPERTIES_3) && information.Contains(HEADER__OWNERSHIP))
                {
                    ImmovableProperties_3_Ownership = description;
                }
                else if (information.Contains(PART_E3_PROPERTIES_3) && information.Contains(HEADER__PROPERTY_BENEFICIARIES))
                {
                    ImmovableProperties_3_Beneficiaries = description;
                }

                #endregion

                #region Household Expenses

                //else if (information.Contains(PART_F1_FUNDING_WIFE) && information.Contains(HEADER__HOUSEHOLD_EXPENSES))
                //{
                //    Wife_HouseholdExpenses = description;
                //}
                //else if (information.Contains(PART_F2_FUNDING_CHILDREN) && information.Contains(HEADER__HOUSEHOLD_EXPENSES))
                //{
                //    Children_HouseholdExpenses = description;
                //}
                //else if (information.Contains(PART_F3_FUNDING_PARENTS) && information.Contains(HEADER__HOUSEHOLD_EXPENSES))
                //{
                //    Parents_HouseholdExpenses = description;
                //}
                //else if (information.Contains(PART_F4_FUNDING_GUARDIAN) && information.Contains(HEADER__GUARDIAN_ALLOWANCE))
                //{
                //    Guardian_Allowance = description;
                //}


                #endregion

                #region Insurance 1

                else if (information.Contains(PART_G1_INSURANCE_1) && information.Contains(HEADER__INSURANCE_COMPANY))
                {
                    Insurance_1_Company = description;
                }
                else if (information.Contains(PART_G1_INSURANCE_1) && information.Contains(HEADER__INSURANCE_POLICY_NO))
                {
                    Insurance_1_PolicyNo = description;
                }

                #endregion

                #region Insurance 2

                else if (information.Contains(PART_G2_INSURANCE_2) && information.Contains(HEADER__INSURANCE_COMPANY))
                {
                    Insurance_2_Company = description;
                }
                else if (information.Contains(PART_G2_INSURANCE_2) && information.Contains(HEADER__INSURANCE_POLICY_NO))
                {
                    Insurance_2_PolicyNo = description;
                }

                #endregion

                #region Final Resting Place

                else if (information.Contains(PART_H2) && information.Contains(HEADER__RELIGION))
                {
                    FinalRestingPlace_Religion = description;
                }
                else if (information.Contains(PART_H3) && information.Contains(HEADER__RESTING_METHOD))
                {
                    FinalRestingPlace_Method = description;
                }

                #endregion

                #region Additional Assets

                else if (information.Contains(PART_J1) && information.Contains(HEADER__ASSETS))
                {
                    Additional_Assets = description;
                }
                else if (information.Contains(PART_J2) && information.Contains(HEADER__ASSETS))
                {
                    Additional_Assets = description;
                }
                #endregion

            }


            #endregion

            #region Generate will

            int totalBeneficiary = int.TryParse(TotalBeneficiaries, out totalBeneficiary) ? totalBeneficiary : 0;
            int totalSubstituteBeneficiary = int.TryParse(TotalSubstituteBeneficiaries, out totalSubstituteBeneficiary) ? totalSubstituteBeneficiary : 0;

            string ifNotSuriveStatement_Individual = "";
            string ifNotSuriveStatement_NonIndividual = "";

            if (DeathOfMainBeneficiary.Contains(TO_MAIN_BENEFICIARIES))
            {
                ifNotSuriveStatement_Individual = "If [1he/she] does not survive me, then the benefit [1he/she] would have received shall be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to under my residuary estate herein.";

                ifNotSuriveStatement_NonIndividual = "In the event any beneficiary(ies) predeceases me, the entitlement that such predeceased beneficiary(ies) would have received shall instead be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to herein. ";
            }
            else
            {
                ifNotSuriveStatement_Individual = "If [1he/she] does not survive me leaving children, then the benefit [1he/she] would have received shall be given to [2his/her] surviving children in equal shares. If [1he/she] does not survive me leaving no children, then the benefit [1he/she] would have received shall be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to under my residuary estate herein.";

                ifNotSuriveStatement_NonIndividual = "In the event any beneficiary(ies) predeceases me leaving children, the entitlement that such predeceased beneficiary(ies) would have received shall instead be given his/her surviving children in equal shares. In the event any beneficiary(ies) predeceases me leaving no children, the entitlement that such predeceased beneficiary(ies) would have received shall instead be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to herein.";
            }

            #region Revoke Statement

            string RevokeStatement = "I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
            WillTableInsert(true, index_Outdent++, 0, RevokeStatement);

            #endregion

            #region Executor

            string ExecutorStatement = "I appoint as my executor my [1a] [1b] (NRIC No.[1c]) of [1d]";

            ExecutorStatement = ExecutorStatement.Replace("[1a]", Executor_1_Relationship);
            ExecutorStatement = ExecutorStatement.Replace("[1b]", Executor_1_FullName);
            ExecutorStatement = ExecutorStatement.Replace("[1c]", ApplyNewICFormat(Executor_1_IC));
            ExecutorStatement = ExecutorStatement.Replace("[1d]", Executor_1_Address);

            if (Executor_2_FullName != "")
            {
                ExecutorStatement += " but if [1he/she] is unwilling or unable to act for whatsoever reason, then I appoint as my executor my [2a] [2b] (NRIC No.[2c]) of [2d].";

                ExecutorStatement = ExecutorStatement.Replace("[1he/she]", MaleOrFemale(Executor_1_IC) == 1? "he":"she");
                ExecutorStatement = ExecutorStatement.Replace("[2a]", Executor_2_Relationship);
                ExecutorStatement = ExecutorStatement.Replace("[2b]", Executor_2_FullName);
                ExecutorStatement = ExecutorStatement.Replace("[2c]", ApplyNewICFormat(Executor_2_IC));
                ExecutorStatement = ExecutorStatement.Replace("[2d]", Executor_2_Address);
            }
            else
            {
                ExecutorStatement += ".";
            }

            WillTableInsert(true, index_Outdent++, 0, ExecutorStatement);

            #endregion

            #region Executor act as Trustee

            string TrusteeStatement = "In this Will unless it is specifically stated to the contrary, my Executor(s) shall also act as my Trustee(s).";

            WillTableInsert(true, index_Outdent++, 0, TrusteeStatement);

            #endregion

            #region Guardian

            string GuardianStatement = "If my [HUSBAND/ WIFE/ EX-HUSBAND/ EX-WIFE] fails to act as the Guardian of my infant children for whatsoever reason, then I appoint my [1a] [1b] (NRIC No.[1c]) of [1d] as Guardian for as long as required by the law. However, if my [1a] is unable or unwilling to act for whatsoever reason, then I appoint my [2a] [2b] (NRIC No.[2c]) of [2d] to act as Guardian for as long as required by the law.";

            GuardianStatement = GuardianStatement.Replace("[HUSBAND/ WIFE/ EX-HUSBAND/ EX-WIFE]", MaleOrFemale(Testator_IC) == 1 ? "[WIFE/ EX-WIFE]" : "[HUSBAND/ EX-HUSBAND]");
            GuardianStatement = GuardianStatement.Replace("[1a]", Guardian_1_Relationship);
            GuardianStatement = GuardianStatement.Replace("[1b]", Guardian_1_FullName);
            GuardianStatement = GuardianStatement.Replace("[1c]", ApplyNewICFormat(Guardian_1_IC));
            GuardianStatement = GuardianStatement.Replace("[1d]", Guardian_1_Address);

            GuardianStatement = GuardianStatement.Replace("[2a]", Guardian_2_Relationship);
            GuardianStatement = GuardianStatement.Replace("[2b]", Guardian_2_FullName);
            GuardianStatement = GuardianStatement.Replace("[2c]", ApplyNewICFormat(Guardian_2_IC));
            GuardianStatement = GuardianStatement.Replace("[2d]", Guardian_2_Address);

            WillTableInsert(true, index_Outdent++, 0, GuardianStatement);

            #endregion

            #region Joint Bank Account

            string JointBankStatement = "[]I give the moneys standing to my credit in all my joint bank accounts to the respective joint account holder(s), if more than one in equal shares.";

            WillTableInsert(true, index_Outdent++, 0, JointBankStatement);

            #endregion

            #region Bank Account

            string BankStatement = "I give the moneys standing to my credit in all my bank accounts to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:";

            WillTableInsert(true, index_Outdent, 0, BankStatement);

            if (totalBeneficiary <= 1)
            {
                BankStatement = "My [1a] [1b] (NRIC No. [1c])(100%).";

                BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));
                WillTableInsert(true, index_Outdent, 1, BankStatement);


                BankStatement = ifNotSuriveStatement_Individual;

                BankStatement = BankStatement.Replace("[1he/she]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "he" : "she");
                BankStatement = BankStatement.Replace("[2his/her]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "his" : "her");

                WillTableInsert(false, index_Outdent, 0, BankStatement);

            }
            else
            {
                if (totalBeneficiary == 2)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true,index_Outdent, 1, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                    WillTableInsert(true, index_Outdent, 2, BankStatement);

                }
                else if (totalBeneficiary == 3)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true, index_Outdent, 1, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                    WillTableInsert(true, index_Outdent, 2, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                    WillTableInsert(true, index_Outdent, 3, BankStatement);

                }
                else if (totalBeneficiary == 4)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true, index_Outdent, 1, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                    WillTableInsert(true, index_Outdent, 2, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                    WillTableInsert(true, index_Outdent, 3, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                    WillTableInsert(true, index_Outdent, 4, BankStatement);
                }
                else if (totalBeneficiary == 5)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true, index_Outdent, 1, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                    WillTableInsert(true, index_Outdent, 2, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                    WillTableInsert(true, index_Outdent, 3, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                    WillTableInsert(true, index_Outdent, 4, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_5_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_5_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_5_IC));

                    WillTableInsert(true, index_Outdent, 5, BankStatement);
                }

                BankStatement = ifNotSuriveStatement_NonIndividual;

                WillTableInsert(false, index_Outdent, 0, BankStatement);
            }

            BankStatement = "[]The expression ‘all bank accounts’ in this clause shall include all my joint bank accounts / exclude any account which has been specifically mentioned in this Will.";

            WillTableInsert(false, index_Outdent, 0, BankStatement);
            index_Outdent++;

            #endregion

            #region Insurance Nomination not take effect

            string NominationNotEffectStatement = "If the nomination(s) made by me in all my insurance policy(ies) do(es) not take effect for whatsoever reason, then I give the benefits of the nomination(s) to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:";

           WillTableInsert(true ,index_Outdent, 0, NominationNotEffectStatement);

            if (totalBeneficiary <= 1)
            {
                BankStatement = "My [1a] [1b] (NRIC No. [1c])(100%).";

                BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

               WillTableInsert(true ,index_Outdent, 1, BankStatement);


                BankStatement = ifNotSuriveStatement_Individual;

                BankStatement = BankStatement.Replace("[1he/she]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "he" : "she");
                BankStatement = BankStatement.Replace("[2his/her]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "his" : "her");

                WillTableInsert(false, index_Outdent, 0, BankStatement);

            }
            else
            {
                if (totalBeneficiary == 2)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                   WillTableInsert(true ,index_Outdent, 1, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                   WillTableInsert(true ,index_Outdent, 2, BankStatement);

                }
                else if (totalBeneficiary == 3)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                   WillTableInsert(true ,index_Outdent, 1, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                   WillTableInsert(true ,index_Outdent, 2, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                   WillTableInsert(true ,index_Outdent, 3, BankStatement);

                }
                else if (totalBeneficiary == 4)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                   WillTableInsert(true ,index_Outdent, 1, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                   WillTableInsert(true ,index_Outdent, 2, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                   WillTableInsert(true ,index_Outdent, 3, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                   WillTableInsert(true ,index_Outdent, 4, BankStatement);
                }
                else if (totalBeneficiary == 5)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                   WillTableInsert(true ,index_Outdent, 1, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                   WillTableInsert(true ,index_Outdent, 2, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                   WillTableInsert(true ,index_Outdent, 3, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                   WillTableInsert(true ,index_Outdent, 4, BankStatement);

                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_5_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_5_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_5_IC));

                   WillTableInsert(true ,index_Outdent, 5, BankStatement);
                }

                BankStatement = ifNotSuriveStatement_NonIndividual;

                WillTableInsert(false, index_Outdent, 0, BankStatement);
            }

            index_Outdent++;

            #endregion

            #region Property

            string PropertyStatement = "";

            if(!string.IsNullOrEmpty(ImmovableProperties_1_Address))
            {
                string JointOrSole = "";
                string propertiesOwnerShip = ImmovableProperties_1_Ownership.ToUpper();
                string propertyAddress = ImmovableProperties_1_Address;
                string propertyTitle = ImmovableProperties_1_TitleDetail;
                string propertyBeneficiaries = ImmovableProperties_1_Beneficiaries;

                if (propertiesOwnerShip.Contains(PROPERTY_SOLENAME))
                {
                    JointOrSole = "my property";
                }
                else if (propertiesOwnerShip.Contains(PROPERTY_JOINTNAME))
                {
                    JointOrSole = "my undivided share in the property";
                }

                if(!string.IsNullOrEmpty(propertyTitle))
                {
                    propertyAddress += " held under " + propertyTitle;
                }

                PropertyStatement = "I give [1a] known as [1b] to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:";

                PropertyStatement = PropertyStatement.Replace("[1a]", JointOrSole);
                PropertyStatement = PropertyStatement.Replace("[1b]", propertyAddress);

               WillTableInsert(true ,index_Outdent, 0, PropertyStatement);

                //if(!string.IsNullOrEmpty(propertyBeneficiaries))
                //{
                //    PropertyStatement = "[ [1a] ]";

                //    PropertyStatement = PropertyStatement.Replace("[1a]", propertyBeneficiaries);

                //   WillTableInsert(true ,index_Outdent, 1, PropertyStatement);

                //    PropertyStatement =  ifNotSuriveStatement_Individual;

                //    WillTableInsert(false, index_Outdent, 0, PropertyStatement);

                //}

                if (totalBeneficiary <= 1)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(100%).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                   WillTableInsert(true ,index_Outdent, 1, BankStatement);


                    BankStatement = ifNotSuriveStatement_Individual;

                    BankStatement = BankStatement.Replace("[1he/she]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "he" : "she");
                    BankStatement = BankStatement.Replace("[2his/her]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "his" : "her");

                    WillTableInsert(false, index_Outdent, 0, BankStatement);

                }
                else
                {
                    if (totalBeneficiary == 2)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                       WillTableInsert(true ,index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                       WillTableInsert(true ,index_Outdent, 2, BankStatement);

                    }
                    else if (totalBeneficiary == 3)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                       WillTableInsert(true ,index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                       WillTableInsert(true ,index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                       WillTableInsert(true ,index_Outdent, 3, BankStatement);

                    }
                    else if (totalBeneficiary == 4)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                       WillTableInsert(true ,index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                       WillTableInsert(true ,index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                       WillTableInsert(true ,index_Outdent, 3, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                       WillTableInsert(true ,index_Outdent, 4, BankStatement);
                    }
                    else if (totalBeneficiary == 5)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                       WillTableInsert(true ,index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                       WillTableInsert(true ,index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                       WillTableInsert(true ,index_Outdent, 3, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                       WillTableInsert(true ,index_Outdent, 4, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_5_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_5_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_5_IC));

                       WillTableInsert(true ,index_Outdent, 5, BankStatement);
                    }

                    BankStatement = ifNotSuriveStatement_NonIndividual;

                    WillTableInsert(false, index_Outdent, 0, BankStatement);
                }

                PropertyStatement = "I direct that any sums required to discharge a charge or to withdraw a lien attached to this property shall be paid out of my residuary estate.";

                WillTableInsert(false, index_Outdent, 0, PropertyStatement);
                index_Outdent++;

            }

            if (!string.IsNullOrEmpty(ImmovableProperties_2_Address))
            {
                string JointOrSole = "";
                string propertiesOwnerShip = ImmovableProperties_2_Ownership.ToUpper();
                string propertyAddress = ImmovableProperties_2_Address;
                string propertyTitle = ImmovableProperties_2_TitleDetail;
                string propertyBeneficiaries = ImmovableProperties_2_Beneficiaries;

                if (propertiesOwnerShip.Contains(PROPERTY_SOLENAME))
                {
                    JointOrSole = "my property";
                }
                else if (propertiesOwnerShip.Contains(PROPERTY_JOINTNAME))
                {
                    JointOrSole = "my undivided share in the property";
                }

                if (!string.IsNullOrEmpty(propertyTitle))
                {
                    propertyAddress += " held under " + propertyTitle;
                }

                PropertyStatement = "I give [1a] known as [1b] to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:";

                PropertyStatement = PropertyStatement.Replace("[1a]", JointOrSole);
                PropertyStatement = PropertyStatement.Replace("[1b]", propertyAddress);

               WillTableInsert(true ,index_Outdent, 0, PropertyStatement);

                //if (!string.IsNullOrEmpty(propertyBeneficiaries))
                //{
                //    PropertyStatement = "[ [1a] ]";

                //    PropertyStatement = PropertyStatement.Replace("[1a]", propertyBeneficiaries);

                //   WillTableInsert(true ,index_Outdent, 1, PropertyStatement);

                //    PropertyStatement = ifNotSuriveStatement_Individual;

                //    WillTableInsert(false, index_Outdent, 0, PropertyStatement);

                //}

                if (totalBeneficiary <= 1)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(100%).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true, index_Outdent, 1, BankStatement);


                    BankStatement = ifNotSuriveStatement_Individual;

                    BankStatement = BankStatement.Replace("[1he/she]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "he" : "she");
                    BankStatement = BankStatement.Replace("[2his/her]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "his" : "her");

                    WillTableInsert(false, index_Outdent, 0, BankStatement);

                }
                else
                {
                    if (totalBeneficiary == 2)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                    }
                    else if (totalBeneficiary == 3)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                        WillTableInsert(true, index_Outdent, 3, BankStatement);

                    }
                    else if (totalBeneficiary == 4)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                        WillTableInsert(true, index_Outdent, 3, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                        WillTableInsert(true, index_Outdent, 4, BankStatement);
                    }
                    else if (totalBeneficiary == 5)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                        WillTableInsert(true, index_Outdent, 3, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                        WillTableInsert(true, index_Outdent, 4, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_5_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_5_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_5_IC));

                        WillTableInsert(true, index_Outdent, 5, BankStatement);
                    }

                    BankStatement = ifNotSuriveStatement_NonIndividual;

                    WillTableInsert(false, index_Outdent, 0, BankStatement);
                }

                PropertyStatement = "I direct that any sums required to discharge a charge or to withdraw a lien attached to this property shall be paid out of my residuary estate.";

                WillTableInsert(false, index_Outdent, 0, PropertyStatement);
                index_Outdent++;

            }

            if (!string.IsNullOrEmpty(ImmovableProperties_3_Address))
            {
                string JointOrSole = "";
                string propertiesOwnerShip = ImmovableProperties_3_Ownership.ToUpper();
                string propertyAddress = ImmovableProperties_3_Address;
                string propertyTitle = ImmovableProperties_3_TitleDetail;
                string propertyBeneficiaries = ImmovableProperties_3_Beneficiaries;

                if (propertiesOwnerShip.Contains(PROPERTY_SOLENAME))
                {
                    JointOrSole = "my property";
                }
                else if (propertiesOwnerShip.Contains(PROPERTY_JOINTNAME))
                {
                    JointOrSole = "my undivided share in the property";
                }

                if (!string.IsNullOrEmpty(propertyTitle))
                {
                    propertyAddress += " held under " + propertyTitle;
                }

                PropertyStatement = "I give [1a] known as [1b] to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:";

                PropertyStatement = PropertyStatement.Replace("[1a]", JointOrSole);
                PropertyStatement = PropertyStatement.Replace("[1b]", propertyAddress);

               WillTableInsert(true ,index_Outdent, 0, PropertyStatement);

                //if (!string.IsNullOrEmpty(propertyBeneficiaries))
                //{
                //    PropertyStatement = "[ [1a] ]";

                //    PropertyStatement = PropertyStatement.Replace("[1a]", propertyBeneficiaries);

                //   WillTableInsert(true ,index_Outdent, 1, PropertyStatement);

                //    PropertyStatement = ifNotSuriveStatement_Individual;

                //    WillTableInsert(false, index_Outdent, 0, PropertyStatement);

                //}

                if (totalBeneficiary <= 1)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(100%).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true, index_Outdent, 1, BankStatement);


                    BankStatement = ifNotSuriveStatement_Individual;

                    BankStatement = BankStatement.Replace("[1he/she]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "he" : "she");
                    BankStatement = BankStatement.Replace("[2his/her]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "his" : "her");

                    WillTableInsert(false, index_Outdent, 0, BankStatement);

                }
                else
                {
                    if (totalBeneficiary == 2)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                    }
                    else if (totalBeneficiary == 3)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                        WillTableInsert(true, index_Outdent, 3, BankStatement);

                    }
                    else if (totalBeneficiary == 4)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                        WillTableInsert(true, index_Outdent, 3, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                        WillTableInsert(true, index_Outdent, 4, BankStatement);
                    }
                    else if (totalBeneficiary == 5)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                        WillTableInsert(true, index_Outdent, 3, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                        WillTableInsert(true, index_Outdent, 4, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_5_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_5_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_5_IC));

                        WillTableInsert(true, index_Outdent, 5, BankStatement);
                    }

                    BankStatement = ifNotSuriveStatement_NonIndividual;

                    WillTableInsert(false, index_Outdent, 0, BankStatement);
                }

                PropertyStatement = "I direct that any sums required to discharge a charge or to withdraw a lien attached to this property shall be paid out of my residuary estate.";

                WillTableInsert(false, index_Outdent, 0, PropertyStatement);
                index_Outdent++;
            }

            if (string.IsNullOrEmpty(ImmovableProperties_1_Address) && string.IsNullOrEmpty(ImmovableProperties_2_Address) && string.IsNullOrEmpty(ImmovableProperties_3_Address))
            {
                //No individual property
                PropertyStatement = "[]I give all my immovable properties wheresoever situated to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:";

                WillTableInsert(true ,index_Outdent, 0, PropertyStatement);

                if (totalBeneficiary <= 1)
                {
                    BankStatement = "My [1a] [1b] (NRIC No. [1c])(100%).";

                    BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                    BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true, index_Outdent, 1, BankStatement);


                    BankStatement = ifNotSuriveStatement_Individual;

                    BankStatement = BankStatement.Replace("[1he/she]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "he" : "she");
                    BankStatement = BankStatement.Replace("[2his/her]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "his" : "her");

                    WillTableInsert(false, index_Outdent, 0, BankStatement);

                }
                else
                {
                    if (totalBeneficiary == 2)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                    }
                    else if (totalBeneficiary == 3)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                        WillTableInsert(true, index_Outdent, 3, BankStatement);

                    }
                    else if (totalBeneficiary == 4)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                        WillTableInsert(true, index_Outdent, 3, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                        WillTableInsert(true, index_Outdent, 4, BankStatement);
                    }
                    else if (totalBeneficiary == 5)
                    {
                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_1_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_1_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                        WillTableInsert(true, index_Outdent, 1, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_2_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_2_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                        WillTableInsert(true, index_Outdent, 2, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_3_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_3_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                        WillTableInsert(true, index_Outdent, 3, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_4_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_4_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                        WillTableInsert(true, index_Outdent, 4, BankStatement);

                        BankStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share).";

                        BankStatement = BankStatement.Replace("[1a]", Beneficiary_5_Relationship);
                        BankStatement = BankStatement.Replace("[1b]", Beneficiary_5_FullName);
                        BankStatement = BankStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_5_IC));

                        WillTableInsert(true, index_Outdent, 5, BankStatement);
                    }

                    BankStatement = ifNotSuriveStatement_NonIndividual;

                    WillTableInsert(false, index_Outdent, 0, BankStatement);
                }

                BankStatement = "I direct that any sums required to discharge a charge or to withdraw a lien attached to this property shall be paid out of my residuary estate.";

                WillTableInsert(false, index_Outdent, 0, BankStatement);
                //I direct that any sums required to discharge a charge or to withdraw a lien attached to this property shall be paid out of my residuary estate.
            }
            index_Outdent++;
            #endregion

            #region Business

            string BusinessStatement = "[I give the business of 		 (Registration No. 	) located at 	including all assets of the business to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:]";

            WillTableInsert(true ,index_Outdent++, 0, BusinessStatement);

            #endregion

            #region Vehicle

            string VehicleStatement = "I give to my [] all my motor vehicles.";

           WillTableInsert(true ,index_Outdent, 0, VehicleStatement);

            VehicleStatement = "If [he/she] does not survive me, then the benefit [he/she] would have received shall be given to my []";
            WillTableInsert(false, index_Outdent, 0, VehicleStatement);

            index_Outdent++;

            #endregion

            #region TheRestOfEstateStatement

            string TheRestOfEstateStatement = "My Executor and Trustee shall hold the rest of my estate on trust to retain or sell it and:";
            WillTableInsert(true , index_Outdent, 0, TheRestOfEstateStatement);


            TheRestOfEstateStatement = "To pay debts including any sums required to secure a discharge of any charge or a withdrawal of any lien on any of my immovable properties, funeral and executorship expenses;";
            WillTableInsert(true , index_Outdent, 1, TheRestOfEstateStatement);

            TheRestOfEstateStatement = "To hold on trust the entitlement of any minor beneficiary(ies) under my Will until such beneficiary(ies) attains 18 years of age(if applicable);";

            WillTableInsert(true , index_Outdent, 2, TheRestOfEstateStatement);


            TheRestOfEstateStatement = "To [1a] the residue(‘my residuary estate’) [1b]";

            if (totalBeneficiary <= 1)
            {
                TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]","give");
                TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", "to [1c] [1d] (NRIC No. [1e]);");

                TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", Beneficiary_1_Relationship);
                TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1d]", Beneficiary_1_FullName);
                TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1e]", ApplyNewICFormat(Beneficiary_1_IC));

                WillTableInsert(true , index_Outdent, 3, TheRestOfEstateStatement);


                TheRestOfEstateStatement = "But if [1he/she] does not survive me, to [1a] the residue(‘my residuary estate’) [1b]";

                TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1he/she]", MaleOrFemale(Beneficiary_1_IC) == 1 ? "he" : "she");

                if (totalSubstituteBeneficiary <= 1)
                {
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", "give");
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", "to [1c] [1d] (NRIC No. [1e]);");

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", Substitute_Beneficiary_1_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1d]", Substitute_Beneficiary_1_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1e]", ApplyNewICFormat(Substitute_Beneficiary_1_IC));

                    WillTableInsert(true , index_Outdent, 4, TheRestOfEstateStatement);

                }
                else
                {
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", "divide");
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", "among the following beneficiaries named below in the shares indicated:");

                    WillTableInsert(true, index_Outdent, 4, TheRestOfEstateStatement);

                    //second indent level
                    if (totalSubstituteBeneficiary == 2)
                    {
                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share);and";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_1_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_1_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_1_IC));

                        WillTableInsert(true , index_Outdent, 4, 1, TheRestOfEstateStatement);

                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share).";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_2_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_2_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_2_IC));

                        WillTableInsert(true , index_Outdent, 4, 2, TheRestOfEstateStatement);

                    }
                    else if (totalSubstituteBeneficiary == 3)
                    {
                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_1_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_1_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_1_IC));

                        WillTableInsert(true , index_Outdent, 4, 1, TheRestOfEstateStatement);


                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_2_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_2_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_2_IC));

                        WillTableInsert(true , index_Outdent, 4, 2, TheRestOfEstateStatement);


                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share).";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_3_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_3_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_3_IC));

                        WillTableInsert(true , index_Outdent, 4, 3, TheRestOfEstateStatement);


                    }
                    else if (totalSubstituteBeneficiary == 4)
                    {
                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_1_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_1_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_1_IC));

                        WillTableInsert(true , index_Outdent, 4, 1, TheRestOfEstateStatement);


                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_2_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_2_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_2_IC));

                        WillTableInsert(true , index_Outdent, 4, 2, TheRestOfEstateStatement);


                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_3_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_3_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_3_IC));

                        WillTableInsert(true , index_Outdent, 4, 3, TheRestOfEstateStatement);


                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share).";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_4_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_4_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_4_IC));

                        WillTableInsert(true , index_Outdent, 4, 4, TheRestOfEstateStatement);

                    }
                    else if (totalSubstituteBeneficiary == 5)
                    {
                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_1_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_1_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_1_IC));

                        WillTableInsert(true , index_Outdent, 4, 1, TheRestOfEstateStatement);


                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_2_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_2_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_2_IC));

                        WillTableInsert(true , index_Outdent, 4, 2, TheRestOfEstateStatement);


                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_3_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_3_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_3_IC));

                        WillTableInsert(true , index_Outdent, 4, 3, TheRestOfEstateStatement);


                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_4_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_4_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_4_IC));

                        WillTableInsert(true , index_Outdent, 4, 4, TheRestOfEstateStatement);


                        TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share).";

                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_5_Relationship);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Substitute_Beneficiary_5_FullName);
                        TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", ApplyNewICFormat(Substitute_Beneficiary_5_IC));

                        WillTableInsert(true , index_Outdent, 4, 5, TheRestOfEstateStatement);

                    }

                    TheRestOfEstateStatement = ifNotSuriveStatement_NonIndividual;

                    WillTableInsert(false, index_Outdent, 0, TheRestOfEstateStatement);

                }


            }
            else
            {
                TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", "divide");
                TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", "among the following beneficiaries named below in the shares indicated:");

                WillTableInsert(true , index_Outdent, 3, TheRestOfEstateStatement);

                if (totalBeneficiary == 2)
                {
                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share);and";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_1_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true, index_Outdent, 3, 1, TheRestOfEstateStatement);


                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/2 share).";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_2_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                    WillTableInsert(true, index_Outdent, 3, 2, TheRestOfEstateStatement);


                }
                else if (totalBeneficiary == 3)
                {
                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_1_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true, index_Outdent, 3, 1, TheRestOfEstateStatement);
                    WillTableInsert(true, index_Outdent, 3, 1, TheRestOfEstateStatement);

                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share);and";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_2_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                    WillTableInsert(true, index_Outdent, 3, 2, TheRestOfEstateStatement);

                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/3 share).";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_3_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_3_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                    WillTableInsert(true, index_Outdent, 3, 3, TheRestOfEstateStatement);

                }
                else if (totalBeneficiary == 4)
                {
                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_1_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true, index_Outdent, 3, 1, TheRestOfEstateStatement);

                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_2_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                    WillTableInsert(true, index_Outdent, 3, 2, TheRestOfEstateStatement);

                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share);and";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_3_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_3_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                    WillTableInsert(true, index_Outdent, 3, 3, TheRestOfEstateStatement);

                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/4 share).";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_4_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_4_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                    WillTableInsert(true, index_Outdent, 3, 4, TheRestOfEstateStatement);
                }
                else if (totalBeneficiary == 5)
                {
                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_1_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_1_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_1_IC));

                    WillTableInsert(true, index_Outdent, 3, 1, TheRestOfEstateStatement);

                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_2_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_2_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_2_IC));

                    WillTableInsert(true, index_Outdent, 3, 2, TheRestOfEstateStatement);

                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_3_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_3_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_3_IC));

                    WillTableInsert(true, index_Outdent, 3, 3, TheRestOfEstateStatement);

                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share);and";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_4_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_4_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_4_IC));

                    WillTableInsert(true, index_Outdent, 3, 4, TheRestOfEstateStatement);

                    TheRestOfEstateStatement = "My [1a] [1b] (NRIC No. [1c])(1/5 share).";

                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1a]", Beneficiary_5_Relationship);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1b]", Beneficiary_5_FullName);
                    TheRestOfEstateStatement = TheRestOfEstateStatement.Replace("[1c]", ApplyNewICFormat(Beneficiary_5_IC));

                    WillTableInsert(true, index_Outdent, 3, 5, TheRestOfEstateStatement);
                }

                TheRestOfEstateStatement = ifNotSuriveStatement_NonIndividual;

                WillTableInsert(false, index_Outdent, 0, TheRestOfEstateStatement);

            }

            index_Outdent++;
            #endregion

            #region Insurance

            //string InsuranceStatement = "I wish the moneys that will be paid out from the Insurance Policy(ies) assuring my life Policy No. [1a] with [1b] Berhad [1c] shall be utilized to pay debts including any sums required to secure a discharge of any charge or a withdrawal of any lien on any of my immovable properties, funeral and executorship expenses. If the moneys stated herein is insufficient to pay off the said payment, it shall be made from my residuary estate.";
            string InsuranceStatement = "It is my wish without legal obligation that the moneys that will be paid out from the Insurance Policy(ies) assuring my life Policy No. [1a] with [1b] Berhad [1c] shall be utilized to pay debts including any sums required to secure a discharge of any charge or a withdrawal of any lien on any of my immovable properties, funeral and executorship expenses. If the moneys stated herein is insufficient to pay off the said payment, it shall be made from my residuary estate.";

            InsuranceStatement = InsuranceStatement.Replace("[1a]", string.IsNullOrEmpty(Insurance_1_PolicyNo) ? "[1a]": Insurance_1_PolicyNo);

            if(Insurance_1_Company.ToUpper().Contains("BERHAD"))
            {
                Insurance_1_Company = Insurance_1_Company.Replace("BERHAD","");
                Insurance_1_Company = Insurance_1_Company.Replace("Berhad","");
                Insurance_1_Company = Insurance_1_Company.Replace("berhad","");

            }

            InsuranceStatement = InsuranceStatement.Replace("[1b]", string.IsNullOrEmpty(Insurance_1_Company) ? "[1b]" : Insurance_1_Company);

            if(!string.IsNullOrEmpty(Insurance_2_PolicyNo))
            {
                InsuranceStatement = InsuranceStatement.Replace("[1c]", "and Policy No. [2a] with [2b] Berhad");
                InsuranceStatement = InsuranceStatement.Replace("[2a]", string.IsNullOrEmpty(Insurance_2_PolicyNo) ? "[2a]" : Insurance_2_PolicyNo);

                if (Insurance_2_Company.ToUpper().Contains("BERHAD"))
                {
                    Insurance_2_Company = Insurance_2_Company.Replace("BERHAD", "");
                    Insurance_2_Company = Insurance_2_Company.Replace("Berhad", "");
                    Insurance_2_Company = Insurance_2_Company.Replace("berhad", "");

                }

                InsuranceStatement = InsuranceStatement.Replace("[2b]", string.IsNullOrEmpty(Insurance_2_Company) ? "[2b]" : Insurance_2_Company);
            }
            else
            {
                InsuranceStatement = InsuranceStatement.Replace("[1c]","");
            }


            WillTableInsert(true ,index_Outdent++, 0, InsuranceStatement);

            #endregion

            #region Resting Method

            string RestingMethodStatement = "I hereby direct that my body be [1a] [1b]";

            RestingMethodStatement = RestingMethodStatement.Replace("[1a]", string.IsNullOrEmpty(FinalRestingPlace_Method) ? "[1a]" : FinalRestingPlace_Method);

            if(FinalRestingPlace_Method.ToUpper().Contains("CREMATED"))
            {
                RestingMethodStatement = RestingMethodStatement.Replace("[1b]", "and that my ashes be disposed of in such manner as my executor or executor(s) shall, in their discretion, deem appropriate.");

            }
            else if (FinalRestingPlace_Method.ToUpper().Contains("BURIED"))
            {
                RestingMethodStatement = RestingMethodStatement.Replace("[1b]", "and that my body be disposed of at such location as my executor or executor(s) shall, in their discretion, deem appropriate.");
            }

            WillTableInsert(true ,index_Outdent++, 0, RestingMethodStatement);

            #endregion

            #region Resting Religion

            string RestingReligonStatement = "[I request a [1a] funeral service.]";

            RestingReligonStatement = RestingReligonStatement.Replace("[1a]", string.IsNullOrEmpty(FinalRestingPlace_Religion) ? "[1a]" : FinalRestingPlace_Religion);

            WillTableInsert(true ,index_Outdent++, 0, RestingReligonStatement);

            #endregion

            #region Determining Entitlement

            string DeterminingEntitlement = "For the purpose of determining entitlement under my Will any beneficiary(ies) who does not live for more than THIRTY(30) days from the date of my death shall be deemed to have died before me.";

            WillTableInsert(true ,index_Outdent++, 0, DeterminingEntitlement);

            #endregion

            string WillContent = RevokeStatement + newLine + ExecutorStatement + newLine + TrusteeStatement;

            WillContent += newLine + GuardianStatement;
            //WillContent += newLine + PropertyStatement;
            //rtbWill.Text = WillContent;

            #endregion

            dgvWillDraft.DataSource = dt_Will;
            DgvUIEdit(dgvWillDraft);
            dgvWillDraft.ClearSelection();
        }

        private void FinexWill_Load(object sender, EventArgs e)
        {
            dgvWillDraft.ClearSelection();
        }

        private float StringWidthMeasuring(string Text)
        {
            Graphics g = CreateGraphics();

            // Set up string.
            Font stringFont = new Font("Times New Roman", 12);

            // Measure string.
            SizeF stringSize = new SizeF();

            stringSize = g.MeasureString(Text, stringFont);

            return stringSize.Width;
        }

        private void btnWill_Click(object sender, EventArgs e)
        {
            if(INDEX_PENDING_REAARANGE)
            {
                RearrangeIndex(dgvWillDraft);
                RearrangeOrPDF(true);
            }
            else
            {
                ExportToExcel();

            }
        }

        #region DO Setting

        #region Excel marking

        int itemRowOffset = 16;
        int maxRow = 20;

        string indexColStart = "a";
        string indexColEnd = ":a";

        string ItemColStart = "b";
        string ItemColEnd = ":d";

        string DescriptionColStart = "e";
        string DescriptionColEnd = ":o";

        string pcsColStart = "p";
        string pcsColEnd = ":q";

        string pcsUnitColStart = "r";
        string pcsUnitColEnd = ":r";

        string remarkColStart = "t";
        string remarkColEnd = ":w";

        string poColStart = "t";
        string poColEnd = ":w";

        string areaDONoData = "t7:w7";
        string areaDateData = "t8:w8";
        string areaPONoData = "t9:w9";
        string areaPageData = "t10:w10";



        string areaBillName = "a7:g8";
        string areaBillLine1 = "a9:g9";
        string areaBillLine2 = "a10:g10";
        string areaBillLine3 = "a11:g11";
        string areaBillLine4 = "a12:g12";
        string areaBillLine5 = "a13:g13";
        string areaBillTel = "a15:g15";

        string areaDeliveryName = "i7:o8";
        string areaDeliveryLine1 = "i9:o9";
        string areaDeliveryLine2 = "i10:o10";
        string areaDeliveryLine3 = "i11:o11";
        string areaDeliveryLine4 = "i12:o12";
        string areaDeliveryLine5 = "i13:o13";
        string areaDeliveryTel = "i15:o15";

        string areaTransporterName = "q14:w15";
        string areaTransporterTitle = "q13:w13";

        string areaTotalData = "s37:w37";
        string areaOwnDORemark = "b37:o37";


        #endregion

        private void copyAlltoClipboard()
        {
            ////dgvNewStock.Columns.Remove(headerParentColor);
            ////dgvNewStock.Columns.Remove(headerRepeat);
            //dgvDOList.SelectAll();
            ////dgvNewStock.sele
            //DataObject dataObj = dgvDOList.GetClipboardContent();
            //if (dataObj != null)
            //    Clipboard.SetDataObject(dataObj);
        }

        private void ExcelPageSetup(Worksheet xlWorkSheet)
        {
            //Page setup
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;
            xlWorkSheet.PageSetup.CenterHorizontally = true;
            //xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;
            xlWorkSheet.PageSetup.FitToPagesWide = false;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 0.8 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 1 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 0 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 0.4 / pointToCMRate;

            //xlWorkSheet.PageSetup.PrintTitleRows = "$1:$1";

            xlWorkSheet.PageSetup.PrintArea = "a1:al48";


        }

        private void InitialDOFormat(Worksheet xlWorkSheet)
        {
            #region Sheet Area

            string sheetWidth = "a1:x1";
            string letterHeadArea = "a1:a16";
            string pageString = "v1:v1";
            string pageNo = "w1:x1";

            string DOInfo = "a11:a16";
            string Divider = "a18:a18";
            string tableHeader = "a19:a20";
            string itemList = "a21:a36";
            string signingArea = "a37:a44";
            string wholeSheetArea = "a1:x44";
            string littleSpace_1 = "a10:x10";
            string littleSpace_2 = "a17:x17";
            string companyName_CN = "F1:S2";
            string companyName_EN = "F3:S4";
            string company_Registration = "F5:S5";
            string company_AddressAndContact = "F6:S8";
            string DONoArea = "u13:x13";
            string PONoArea = "u14:x14";
            string DODateArea = "u15:x15";
            string CustFullNameArea = "c11:M11";
            string AddressArea_1 = "c12:M12";
            string AddressArea_2 = "c13:M13";
            string postalAndCity = "c14:M14";
            string state = "c15:M15";
            string contact = "c16:M16";



            #endregion

            int itemRowOffset = 20;
            int maxRow = 16;
            int itemFontSize = 10;

            string indexColStart = "a";
            string indexColEnd = ":a";

            string codeColStart = "b";
            string codeColEnd = ":d";

            string nameColStart = "e";
            string nameColEnd = ":m";

            string qtyColStart = "n";
            string qtyColEnd = ":o";

            string pcsColStart = "p";
            string pcsColEnd = ":p";

            string remarkColStart = "t";
            string remarkColEnd = ":w";

            #region sheet size adjustment

            Range DOFormat = xlWorkSheet.get_Range(sheetWidth).Cells;
            DOFormat.ColumnWidth = 3;

            DOFormat = xlWorkSheet.get_Range(letterHeadArea).Cells;
            DOFormat.RowHeight = 15;

            DOFormat = xlWorkSheet.get_Range(DOInfo).Cells;
            DOFormat.RowHeight = 18.5;

            DOFormat = xlWorkSheet.get_Range(littleSpace_1).Cells;
            DOFormat.RowHeight = 8;

            DOFormat = xlWorkSheet.get_Range(littleSpace_2).Cells;
            DOFormat.RowHeight = 4.2;

            DOFormat = xlWorkSheet.get_Range(Divider).Cells;
            DOFormat.RowHeight = 5.4;

            DOFormat = xlWorkSheet.get_Range(tableHeader).Cells;
            DOFormat.RowHeight = 15;

            DOFormat = xlWorkSheet.get_Range(itemList).Cells;
            DOFormat.RowHeight = 24.6;

            DOFormat = xlWorkSheet.get_Range(signingArea).Cells;
            DOFormat.RowHeight = 15;

            DOFormat = xlWorkSheet.get_Range(wholeSheetArea).Cells;
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 10;
            xlWorkSheet.PageSetup.PrintArea = wholeSheetArea;

            #endregion

            #region LOGO

            //string tempPath = Path.GetTempFileName();
            //Resources.safety_logo.Save(tempPath + "safety-logo.png");
            //string filePath = tempPath + "safety-logo.png";
            ////var filePath = @"D:\CodeBase\FactoryManagementSoftware\FactoryManagementSoftware\FactoryManagementSoftware\Resources\safety-logo.png";
            //xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 10, 20, 88f, 49f);

            #endregion

            #region LETTER HEAD

            DOFormat = xlWorkSheet.get_Range("a3:a3").Cells;
            DOFormat.RowHeight = 19.2;

            DOFormat = xlWorkSheet.get_Range("a4:a4").Cells;
            DOFormat.RowHeight = 0;

            DOFormat = xlWorkSheet.get_Range("a5:a5").Cells;
            DOFormat.RowHeight = 11.4;

            DOFormat = xlWorkSheet.get_Range("a6:a8").Cells;
            DOFormat.RowHeight = 20.4;

            DOFormat = xlWorkSheet.get_Range(companyName_CN).Cells;
            DOFormat.Merge();
            // DOFormat.Value = text.Company_Name_CN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 26;
            DOFormat.Font.Name = text.Font_Type_KaiTi;
            DOFormat.Font.Color = Color.Red;

            DOFormat = xlWorkSheet.get_Range(pageString).Cells;
            DOFormat.Value = "PG.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(pageNo).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;

            DOFormat.Font.Name = "Cambria";
            DOFormat = xlWorkSheet.get_Range(companyName_EN).Cells;
            DOFormat.Merge();
            //DOFormat.Value = text.Company_Name_EN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(company_Registration).Cells;
            DOFormat.Merge();
            //DOFormat.Value = text.Company_RegistrationNo;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10.5;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(company_AddressAndContact).Cells;
            DOFormat.Merge();
            //DOFormat.Value = text.Company_AddressAndContact;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            #endregion

            string ownDORemark = "b37:o37";
            DOFormat = xlWorkSheet.get_Range(ownDORemark).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o10:x12").Cells;
            DOFormat.Merge();
            DOFormat.Value = "DELIVERY ORDER";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = "Arial Black";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o13:s13").Cells;
            DOFormat.Merge();
            DOFormat.Value = "No.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o14:s14").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Your Order No.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o15:s15").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Date";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("t13:t13").Cells;
            DOFormat.Value = ":";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("t14:t14").Cells;
            DOFormat.Value = ":";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("t15:t15").Cells;
            DOFormat.Value = ":";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(DONoArea).Cells;
            DOFormat.Merge();
            //DOFormat.NumberFormat = 
            // DOFormat.Value = "14245";
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(PONoArea).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "44946";
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            //DO DATE//////////////////////////////////////////////////////////////////////////////////////////////////////
            DOFormat = xlWorkSheet.get_Range(DODateArea).Cells;
            DOFormat.Merge();
            DOFormat.EntireRow.NumberFormat = "DD/MM/YYYY";
            //DOFormat.Value = DODate.ToOADate();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range("a11:b11").Cells;
            DOFormat.Merge();
            DOFormat.Value = "M/S:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Italic = true;

            DOFormat = xlWorkSheet.get_Range("a16:b16").Cells;
            DOFormat.Merge();
            DOFormat.Value = "TEL:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Italic = true;

            DOFormat = xlWorkSheet.get_Range(CustFullNameArea).Cells;
            DOFormat.Merge();
            // DOFormat.Value = "PERMABONN SDN. BHD.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(AddressArea_1).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "NO.2, JALAN MERANTI JAYA";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(AddressArea_2).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "MERANTI JAYA INDUSTRIAL PARK";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(postalAndCity).Cells;
            DOFormat.Merge();
            // DOFormat.Value = "47120 PUCHONG";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(state).Cells;
            DOFormat.Merge();
            // DOFormat.Value = "SELANGOR";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(contact).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "03-87259657";
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            Color color = Color.Black;
            DOFormat = xlWorkSheet.get_Range("a18:w18").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;




            DOFormat = xlWorkSheet.get_Range("b19:l20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "DESCRIPTION";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("n19:q20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "QUANTITY";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("r19:w20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "REMARK";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("a20:w20").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range("a19:a20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "#";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("t36:w36").Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("a20:w20").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;



            for (int i = 1; i <= maxRow; i++)
            {
                string area = indexColStart + (itemRowOffset + i).ToString() + indexColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;

                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = 9;
                DOFormat.Font.Name = "Cambria";

                area = codeColStart + (itemRowOffset + i).ToString() + codeColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                // DOFormat.Value = "SBBEE032";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = 7;
                DOFormat.Font.Name = "Cambria";


                area = nameColStart + (itemRowOffset + i).ToString() + nameColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                // DOFormat.Value = "25 MM EQUAL ELBOW";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";

                area = qtyColStart + (itemRowOffset + i).ToString() + qtyColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "1200";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";

                area = pcsColStart + (itemRowOffset + i).ToString() + pcsColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                //DOFormat.Value = "PCS";
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";

                area = remarkColStart + (itemRowOffset + i).ToString() + remarkColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "3 BAGS";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";
            }

            DOFormat = xlWorkSheet.get_Range("a36:w36").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range("a38:h38").Cells;
            DOFormat.Merge();
            DOFormat.Value = "SAFETY PLASTICS SDN. BHD.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("p38:x38").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Goods checked and received by";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range("a44:c44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Issued By";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("f44:h44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Store";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("k44:m44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Lorry";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("p44:w44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Customer's Chop & Signature";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

        }

        private void InitialDOFormatWithMultiPO(Worksheet xlWorkSheet)
        {
            #region Sheet Area

            string sheetWidth = "a1:x1";
            string letterHeadArea = "a1:a16";
            string pageString = "v1:v1";
            string pageNo = "w1:x1";

            string DOInfo = "a11:a16";
            string Divider = "a18:a18";
            string tableHeader = "a19:a20";
            string itemList = "a21:a36";
            string signingArea = "a37:a44";
            string wholeSheetArea = "a1:x44";
            string littleSpace_1 = "a10:x10";
            string littleSpace_2 = "a17:x17";
            string companyName_CN = "F1:S2";
            string companyName_EN = "F3:S4";
            string company_Registration = "F5:S5";
            string company_AddressAndContact = "F6:S8";
            string DONoArea = "u13:x13";
            string PONoArea = "u14:x14";
            string DODateArea = "u15:x15";
            string CustFullNameArea = "c11:M11";
            string AddressArea_1 = "c12:M12";
            string AddressArea_2 = "c13:M13";
            string postalAndCity = "c14:M14";
            string state = "c15:M15";
            string contact = "c16:M16";

            #endregion

            int itemRowOffset = 20;
            int maxRow = 16;
            int itemFontSize = 10;


            string indexColStart = "a";
            string indexColEnd = ":a";

            string codeColStart = "b";
            string codeColEnd = ":d";

            string nameColStart = "e";
            string nameColEnd = ":m";

            string qtyColStart = "n";
            string qtyColEnd = ":o";

            string pcsColStart = "p";
            string pcsColEnd = ":p";

            string remarkColStart = "r";
            string remarkColEnd = ":t";

            string poColStart = "u";
            string pocolEnd = ":w";

            #region sheet size adjustment

            Range DOFormat = xlWorkSheet.get_Range(sheetWidth).Cells;
            DOFormat.ColumnWidth = 3;

            DOFormat = xlWorkSheet.get_Range(letterHeadArea).Cells;
            DOFormat.RowHeight = 15;

            DOFormat = xlWorkSheet.get_Range(DOInfo).Cells;
            DOFormat.RowHeight = 18.5;

            DOFormat = xlWorkSheet.get_Range(littleSpace_1).Cells;
            DOFormat.RowHeight = 8;

            DOFormat = xlWorkSheet.get_Range(littleSpace_2).Cells;
            DOFormat.RowHeight = 4.2;

            DOFormat = xlWorkSheet.get_Range(Divider).Cells;
            DOFormat.RowHeight = 5.4;

            DOFormat = xlWorkSheet.get_Range(tableHeader).Cells;
            DOFormat.RowHeight = 15;

            DOFormat = xlWorkSheet.get_Range(itemList).Cells;
            DOFormat.RowHeight = 24.6;

            DOFormat = xlWorkSheet.get_Range(signingArea).Cells;
            DOFormat.RowHeight = 15;

            DOFormat = xlWorkSheet.get_Range(wholeSheetArea).Cells;
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 10;
            xlWorkSheet.PageSetup.PrintArea = wholeSheetArea;

            #endregion

            #region LOGO

            //string tempPath = Path.GetTempFileName();
            //Resources.safety_logo.Save(tempPath + "safety-logo.png");
            //string filePath = tempPath + "safety-logo.png";
            ////var filePath = @"D:\CodeBase\FactoryManagementSoftware\FactoryManagementSoftware\FactoryManagementSoftware\Resources\safety-logo.png";
            //xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 10, 20, 88f, 49f);

            #endregion

            #region LETTER HEAD

            DOFormat = xlWorkSheet.get_Range("a3:a3").Cells;
            DOFormat.RowHeight = 19.2;

            DOFormat = xlWorkSheet.get_Range("a4:a4").Cells;
            DOFormat.RowHeight = 0;

            DOFormat = xlWorkSheet.get_Range("a5:a5").Cells;
            DOFormat.RowHeight = 11.4;

            DOFormat = xlWorkSheet.get_Range("a6:a8").Cells;
            DOFormat.RowHeight = 20.4;

            DOFormat = xlWorkSheet.get_Range(companyName_CN).Cells;
            DOFormat.Merge();
            // DOFormat.Value = text.Company_Name_CN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 26;
            DOFormat.Font.Name = text.Font_Type_KaiTi;
            DOFormat.Font.Color = Color.Red;

            DOFormat = xlWorkSheet.get_Range(pageString).Cells;
            DOFormat.Value = "PG.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(pageNo).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 10;

            DOFormat.Font.Name = "Cambria";
            DOFormat = xlWorkSheet.get_Range(companyName_EN).Cells;
            DOFormat.Merge();
            //DOFormat.Value = text.Company_Name_EN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(company_Registration).Cells;
            DOFormat.Merge();
            //DOFormat.Value = text.Company_RegistrationNo;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10.5;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(company_AddressAndContact).Cells;
            DOFormat.Merge();
            //DOFormat.Value = text.Company_AddressAndContact;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 11;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            #endregion

            #region DO Info
            DOFormat = xlWorkSheet.get_Range("o10:x12").Cells;
            DOFormat.Merge();
            DOFormat.Value = "DELIVERY ORDER";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = "Arial Black";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o13:s13").Cells;
            DOFormat.Merge();
            DOFormat.Value = "No.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o14:s14").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Your Order No.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("o15:s15").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Date";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("t13:t13").Cells;
            DOFormat.Value = ":";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("t14:t14").Cells;
            DOFormat.Value = ":";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range("t15:t15").Cells;
            DOFormat.Value = ":";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(DONoArea).Cells;
            DOFormat.Merge();
            //DOFormat.NumberFormat = 
            // DOFormat.Value = "14245";
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(PONoArea).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "44946";
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            //DO DATE//////////////////////////////////////////////////////////////////////////////////////////////////////
            DOFormat = xlWorkSheet.get_Range(DODateArea).Cells;
            DOFormat.Merge();
            DOFormat.EntireRow.NumberFormat = "DD/MM/YYYY";
            //DOFormat.Value = DODate.ToOADate();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range("a11:b11").Cells;
            DOFormat.Merge();
            DOFormat.Value = "M/S:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Italic = true;

            DOFormat = xlWorkSheet.get_Range("a16:b16").Cells;
            DOFormat.Merge();
            DOFormat.Value = "TEL:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Italic = true;

            DOFormat = xlWorkSheet.get_Range(CustFullNameArea).Cells;
            DOFormat.Merge();
            // DOFormat.Value = "PERMABONN SDN. BHD.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(AddressArea_1).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "NO.2, JALAN MERANTI JAYA";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(AddressArea_2).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "MERANTI JAYA INDUSTRIAL PARK";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(postalAndCity).Cells;
            DOFormat.Merge();
            // DOFormat.Value = "47120 PUCHONG";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(state).Cells;
            DOFormat.Merge();
            // DOFormat.Value = "SELANGOR";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(contact).Cells;
            DOFormat.Merge();
            //DOFormat.Value = "03-87259657";
            DOFormat.NumberFormat = "@";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            #endregion

            Color color = Color.Black;
            DOFormat = xlWorkSheet.get_Range("a18:w18").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range("a19:a20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "#";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("b19:l20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "DESCRIPTION";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("n19:p20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "QUANTITY";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("r19:t20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "REMARK";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("u19:w20").Cells;
            DOFormat.Merge();
            DOFormat.Value = "P/O NO.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("a20:w20").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range("r36:w36").Cells;
            DOFormat.Merge();

            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            for (int i = 1; i <= maxRow; i++)
            {
                string area = indexColStart + (itemRowOffset + i).ToString() + indexColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;

                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = 9;
                DOFormat.Font.Name = "Cambria";

                area = codeColStart + (itemRowOffset + i).ToString() + codeColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                // DOFormat.Value = "SBBEE032";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = 7;
                DOFormat.Font.Name = "Cambria";

                area = nameColStart + (itemRowOffset + i).ToString() + nameColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                // DOFormat.Value = "25 MM EQUAL ELBOW";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";

                area = qtyColStart + (itemRowOffset + i).ToString() + qtyColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "1200";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";

                area = pcsColStart + (itemRowOffset + i).ToString() + pcsColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                //DOFormat.Value = "PCS";
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";

                area = remarkColStart + (itemRowOffset + i).ToString() + remarkColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "3 BAGS";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = itemFontSize;
                DOFormat.Font.Name = "Cambria";

                area = poColStart + (itemRowOffset + i).ToString() + pocolEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "PO No";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
                DOFormat.Font.Size = 7;
                DOFormat.Font.Name = "Cambria";
                DOFormat.Font.Italic = true;
            }

            DOFormat = xlWorkSheet.get_Range("a36:w36").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range("a38:h38").Cells;
            DOFormat.Merge();
            DOFormat.Value = "SAFETY PLASTICS SDN. BHD.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("p38:x38").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Goods checked and received by";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range("a44:c44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Issued By";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("f44:h44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Store";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("k44:m44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Lorry";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("p44:w44").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Customer's Chop & Signature";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

        }

        private void ExcelRowHeight(Worksheet xlWorkSheet, string RangeString, double rowHeight)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.RowHeight = rowHeight;
        }

        private void ExcelColumnWidth(Worksheet xlWorkSheet, string RangeString, double colWidth)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.ColumnWidth = colWidth;
        }

        private void ExcelMergeandAlign(Worksheet xlWorkSheet, string RangeString, XlHAlign hl, XlVAlign va)
        {
            Range range = xlWorkSheet.get_Range(RangeString).Cells;
            range.Merge();

            range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            range.VerticalAlignment = XlVAlign.xlVAlignCenter;

            range.Font.Size = 9;
            range.Font.Name = "Cambria";
        }

        private void NewInitialDOFormat(Worksheet xlWorkSheet, bool MultiPo)
        {
            if (MultiPo)
            {
                DescriptionColStart = "e";
                DescriptionColEnd = ":k";

                pcsColStart = "l";
                pcsColEnd = ":m";

                pcsUnitColStart = "n";
                pcsUnitColEnd = ":n";

                remarkColStart = "p";
                remarkColEnd = ":s";
            }
            else
            {
                DescriptionColStart = "e";
                DescriptionColEnd = ":o";

                pcsColStart = "p";
                pcsColEnd = ":q";

                pcsUnitColStart = "r";
                pcsUnitColEnd = ":r";

                remarkColStart = "t";
                remarkColEnd = ":w";

            }



            #region sheet size adjustment

            string sheetWidth = "a1:w1";
            string wholeSheetArea = "a1:w45";

            //Company Info
            string rowCompanyNameCN = "a1:w1";
            string rowCompanyNameEN = "a2:w2";
            string rowSyarikatNo = "a3:w3";
            string rowAddress1 = "a4:w4";
            string rowAddress2 = "a5:w5";
            string HeadOffice = "a4:e4";
            string SemenyihBranch = "a5:e5";
            string HeadOfficeAddress = "f4:w4";
            string SemenyihAddress = "f5:w5";

            //DO Info
            string areaBillTo = "a6:g6";
            string areaDeliveryTo = "i6:o6";
            string rowDeliveryOrder = "a6:w6";
            string areaDeliveryOrder = "q6:w6";
            string areaDOInfo = "a7:w14";

            string areaDONo = "q7:s7";
            string areaDate = "q8:s8";
            string areaPONo = "q9:s9";
            string areaPage = "q10:s10";



            XlHAlign alignLeft = XlHAlign.xlHAlignLeft;
            XlHAlign alignRight = XlHAlign.xlHAlignRight;
            XlVAlign alignVCenter = XlVAlign.xlVAlignCenter;

            ExcelMergeandAlign(xlWorkSheet, areaBillLine1, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine2, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine3, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine4, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillLine5, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaBillTel, alignLeft, alignVCenter);

            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine1, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine2, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine3, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine4, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryLine5, alignLeft, alignVCenter);
            ExcelMergeandAlign(xlWorkSheet, areaDeliveryTel, alignLeft, alignVCenter);

            string rowDOInfoLastRow = "a15:w15";

            ExcelMergeandAlign(xlWorkSheet, areaTransporterTitle, alignLeft, alignVCenter);

            string rowListTitle = "a16:w16";
            string areaIndex = "a16:a16";
            string areaItem = "b16:d16";
            string areaDescription = "e16:o16";
            string areaQuantity = "p16:r16";
            string areaRemark = "t16:w16";
            string areaPO = "t16:w16";

            ExcelColumnWidth(xlWorkSheet, sheetWidth, 3);

            Range DOFormat = xlWorkSheet.get_Range(wholeSheetArea).Cells;
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 9;
            xlWorkSheet.PageSetup.PrintArea = wholeSheetArea;



            #endregion

            #region LOGO

            string tempPath = Path.GetTempFileName();
            Resources.safetyblacklogo.Save(tempPath + "safetyblacklogo.png");
            string filePath = tempPath + "safetyblacklogo.png";

            //var filePath = @"D:\CodeBase\FactoryManagementSoftware\FactoryManagementSoftware\FactoryManagementSoftware\Resources\safety-logo-black.png";
            xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 10, 2, 88f, 49f);

            #endregion

            #region LETTER HEAD

            DOFormat = xlWorkSheet.get_Range(rowCompanyNameCN).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 21.60;
            DOFormat.Value = text.Company_Name_CN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 22;
            DOFormat.Font.Name = text.Font_Type_KaiTi;

            DOFormat = xlWorkSheet.get_Range(rowCompanyNameEN).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 18.60;
            DOFormat.Value = text.Company_Name_EN;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 20;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(rowSyarikatNo).Cells;
            DOFormat.Merge();
            DOFormat.RowHeight = 15.60;
            DOFormat.Value = text.Company_RegistrationNo;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            ExcelRowHeight(xlWorkSheet, rowAddress1, 22.20);
            ExcelRowHeight(xlWorkSheet, rowAddress2, 27.60);

            DOFormat = xlWorkSheet.get_Range(HeadOffice).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Head Office:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(SemenyihBranch).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Semenyih Branch:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(HeadOfficeAddress).Cells;
            DOFormat.Merge();
            DOFormat.Value = text.Company_Head_AddressAndContact;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            DOFormat = xlWorkSheet.get_Range(SemenyihAddress).Cells;
            DOFormat.Merge();
            DOFormat.Value = text.Company_Semenyih_AddressAndContact;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 8;
            DOFormat.Font.Name = text.Font_Type_TimesNewRoman;

            Color color = Color.Black;
            DOFormat = xlWorkSheet.get_Range(rowAddress2).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            #endregion

            #region DO Info

            ExcelRowHeight(xlWorkSheet, rowDeliveryOrder, 37.8);
            ExcelRowHeight(xlWorkSheet, areaDOInfo, 13.2);
            ExcelRowHeight(xlWorkSheet, rowDOInfoLastRow, 27);

            DOFormat = xlWorkSheet.get_Range(areaBillTo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "BILL TO:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaDeliveryTo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "DELIVERY TO:";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range(areaDeliveryOrder).Cells;
            DOFormat.Merge();
            DOFormat.Value = "DELIVERY ORDER";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 15;
            DOFormat.Font.Name = "Arial Black";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaBillName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;

            DOFormat = xlWorkSheet.get_Range(areaDeliveryName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;

            DOFormat = xlWorkSheet.get_Range(areaDONo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "No. :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaDate).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Date :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaPONo).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Your P/O No. :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaPage).Cells;
            DOFormat.Merge();
            DOFormat.Value = "Page :";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaDONoData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "00345";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range(areaDateData).Cells;
            DOFormat.Merge();
            DOFormat.EntireRow.NumberFormat = "DD/MM/YYYY";
            //DOFormat.Value = DateTime.Now.Date.ToString("dd/MM/yyyy");
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaPONoData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "PO01232";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range(areaPageData).Cells;
            DOFormat.Merge();
            DOFormat.NumberFormat = "@";
            //DOFormat.Value = "1    of   3";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";

            DOFormat = xlWorkSheet.get_Range(areaTransporterName).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 16;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.WrapText = true;

            #endregion

            #region Item List

            int itemListFontSize = 9;

            if (MultiPo)
            {
                itemListFontSize = 8;

                DOFormat = xlWorkSheet.get_Range(areaPO).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = 8;

                DOFormat.Value = "YOUR P/O NO.";


                areaDescription = "e16:k16";
                areaQuantity = "l16:n16";
                areaRemark = "p16:s16";


            }

            //item list title
            DOFormat = xlWorkSheet.get_Range(rowListTitle).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;
            DOFormat.RowHeight = 19.80;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;
            DOFormat.HorizontalAlignment = alignLeft;
            DOFormat.VerticalAlignment = alignVCenter;

            //item list content
            DOFormat = xlWorkSheet.get_Range("a17:w36").Cells;
            DOFormat.RowHeight = 18;
            DOFormat.Font.Size = itemListFontSize;
            DOFormat.Font.Name = "Cambria";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;


            DOFormat = xlWorkSheet.get_Range(areaIndex).Cells;
            DOFormat.Merge();
            DOFormat.Value = "#";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(areaItem).Cells;
            DOFormat.Merge();
            DOFormat.Value = "ITEM";

            DOFormat = xlWorkSheet.get_Range(areaDescription).Cells;
            DOFormat.Merge();
            DOFormat.Value = "DESCRIPTION";

            DOFormat = xlWorkSheet.get_Range(areaQuantity).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = alignRight;
            DOFormat.Value = "QUANTITY";

            DOFormat = xlWorkSheet.get_Range(areaRemark).Cells;
            DOFormat.Merge();
            DOFormat.Value = "REMARK";

            for (int i = 1; i <= maxRow; i++)
            {
                string area = indexColStart + (itemRowOffset + i).ToString() + indexColEnd + (itemRowOffset + i).ToString();

                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;


                //DOFormat.Value = i;

                area = ItemColStart + (itemRowOffset + i).ToString() + ItemColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.Font.Size = 8;
                //DOFormat.Value = "SBBEE 32";

                area = DescriptionColStart + (itemRowOffset + i).ToString() + DescriptionColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "25 MM EQUAL ELBOW";

                area = pcsColStart + (itemRowOffset + i).ToString() + pcsColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
                //DOFormat.Value = "1200";

                area = pcsUnitColStart + (itemRowOffset + i).ToString() + pcsUnitColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                // DOFormat.Value = "PCS";
                DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;

                area = remarkColStart + (itemRowOffset + i).ToString() + remarkColEnd + (itemRowOffset + i).ToString();
                DOFormat = xlWorkSheet.get_Range(area).Cells;
                DOFormat.Merge();
                //DOFormat.Value = "3 BAGS";

                if (MultiPo)
                {
                    area = poColStart + (itemRowOffset + i).ToString() + poColEnd + (itemRowOffset + i).ToString();
                    DOFormat = xlWorkSheet.get_Range(area).Cells;
                    DOFormat.Merge();
                    //DOFormat.Value = "23232323";
                }



            }

            //Total 
            DOFormat = xlWorkSheet.get_Range("a37:w37").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;
            //DOFormat.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlDot;

            DOFormat.RowHeight = 26.40;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            //DOFormat.Borders.LineStyle = XlLineStyle.xlDashDot;

            DOFormat = xlWorkSheet.get_Range(areaTotalData).Cells;
            DOFormat.Merge();
            DOFormat.Font.Bold = true;
            DOFormat.Font.Italic = true;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            string ownDORemark = "b37:o37";
            DOFormat = xlWorkSheet.get_Range(ownDORemark).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Font.Size = 9;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;



            #endregion

            #region Sign And Chop

            DOFormat = xlWorkSheet.get_Range("a39:j45").Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeLeft].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeRight].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range("a39:j39").Cells;
            DOFormat.Merge();
            DOFormat.Value = "SAFETY PLASTICS SDN. BHD.";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Font.Bold = true;

            DOFormat = xlWorkSheet.get_Range("p39:w39").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Goods checked and received by";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignBottom;
            DOFormat.Font.Size = 12;
            DOFormat.Font.Name = "Cambria";


            DOFormat = xlWorkSheet.get_Range("b45:d45").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Issued By";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("g45:i45").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Lorry";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range("p45:w45").Cells;
            DOFormat.Merge();
            DOFormat.Value = "Customer's Chop & Signature";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
            DOFormat.Font.Size = 10;
            DOFormat.Font.Name = "Cambria";
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            #endregion

        }

        private void RowBorder(Worksheet xlWorkSheet, string area)
        {
            Range range = xlWorkSheet.get_Range(area).Cells;
            range.Borders[XlBordersIndex.xlEdgeBottom].Color = Color.Silver;
            range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlDot;
        }

        private void InsertToSheet(Worksheet xlWorkSheet, string area, string value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Value = value;
        }

        private void InsertToSheet(Worksheet xlWorkSheet, string area, double value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Value = value;
        }

        private void InsertToSheet(Worksheet xlWorkSheet, string area, DateTime value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Value = value;
        }
        #endregion

        #region Will Excel Format Setting

        #region Will Data Setting

        #region Common Setting

        private string Will_Cover_Area = "a1:ak46";
        private string Will_Content_Area = "a1:ai46";
        private int Font_Size_Small = 15;
        private int Font_Size_Big = 30;
        private double Page_Column_Width = 1.8;
        private double Page_Row_Height = 16.8;

        #endregion

        #region COVER PAGE

        private string Cover_THE = "a12:ak12";
        private string Cover_LASTWILL = "a14:ak15";
        private string Cover_AND = "a17:ak17";
        private string Cover_TESTAMENT = "a19:ak20";
        private string Cover_OF = "a22:ak22";
        private string Cover_TESTATOR_NAME = "a24:ak25";
        private string Cover_TESTATOR_IC = "a26:ak27";

        private string Cover_Text_THE = "THE";
        private string Cover_Text_LastWill = "LAST WILL";
        private string Cover_Text_And = "AND";
        private string Cover_Text_Testament = "TESTAMENT";
        private string Cover_Text_Of = "OF";

        #endregion

        #region Content Page

        private int Content_Outdent_Width = 750;
        private int Content_Indent_Width = 700;
        private int Content_Indent_Second_Width = 650;
        private int Content_Row_Offset = 3;
        private int Content_UsableRow_Max = 40;
        private int Content_Statement_Width = 856;
        private int Content_Statement_Row_Start = 8;

        private string Content_PageNoArea = "c1:ai1";
        private string Content_PageNo = "b1:c1";
        private string Content_CenterLine = "d1:d1";
        private string Content_PageText = "e1:ai1";

        private string Content_LastWillAndTestament = "c4:ai4";
        private string Content_LastWillAndTestament_Text = "LAST WILL AND TESTAMENT";
        private string Content_Of = "c5:ai5";
        private string Content_Of_Text = "OF";
        private string Content_TESTATOR_NAME = "c6:ai6";

        private string Content_WillStatement_Start = "c";
        private string Content_WillStatement_End = ":ai";

        private string Content_WillStatement_DateSpace = "                                            ";

        private string Content_Outdent_Index_Col_Start = "d";
        private string Content_Outdent_Index_Col_End = ":d";
        private string Content_Indent_Index_Col_Start = "f";
        private string Content_Indent_Index_Col_End = ":f";
        private string Content_Indent_Second_Index_Col_Start = "g";
        private string Content_Indent_Second_Index_Col_End = ":g";

        private string Content_Outdent_Start = "f";
        private string Content_Outdent_End = ":ai";

        private string Content_Indent_Start = "g";
        private string Content_Indent_End = ":ai";

        private string Content_Indent_Second_Start = "h";
        private string Content_Indent_Second_End = ":ai";

        private string Content_Testator_Sign = "c43:m46";
        private string Content_Witness1_Sign = "n43:x46";
        private string Content_Witness2_Sign = "y43:ai46";

        private string Content_Testator_Text = "TESTATOR";
        private string Content_Witness1_Text = "WITNESS 1";
        private string Content_Witness2_Text = "WITNESS 2";

        private string Content_BlankStatement_Start = "c";
        private string Content_BlankStatement_End = ":ai";
        private string Content_BlankStatement_Text = "'-------------------- THE REST OF THE PAGE IS INTENTIONALLY LEFT BLANK ---------------------";

        #endregion

        #region Signing Page
        private int Signing_TestatorNameMax = 234;

        private string Signing_InWitness = "c4:k4";
        private string Signing_InWitness_Text = "IN WITNESS WHEREOF";
        private string Signing_IhaveHereunto = "l4:ai4";
        private string Signing_IhaveHereunto_Text = "I have hereunto set my hand this           day of";
        private string Signing_Year = "c5:ai5";

        private string Signing_SignedBy = "c7:g7";
        private string Signing_SignedBy_Text = "SIGNED BY";
        private string Signing_SignedBy_Name = "h7:q7";
        private string Signing_SignedBy_IC = "c8:q8";
        private string Signing_SignedBy_Statement = "c9:q14";
        private string Signing_SignedBy_Statement_Male = "as his last will in the presence of us both present at the same time who at his request and in her presence and in the presence of each other have hereunto subscribed our names as witnesses.";
        private string Signing_SignedBy_Statement_Female = "as her last will in the presence of us both present at the same time who at her request and in her presence and in the presence of each other have hereunto subscribed our names as witnesses.";

        private string Signing_SignedBy_Divide= "s7:s14";
        private string Signing_SignedBy_Divide_Text= ")";

        private string Signing_Signature_Testator= "v13:ai13";
        private string Signing_Signature_Testator_Text= "(Signature of the Testator)";

        private string Signing_Signature_Witness_1 = "c21:p21";
        private string Signing_Signature_Witness_1_Text = "Signature of First Witness";
        private string Signing_Signature_Witness_2 = "v21:ai21";
        private string Signing_Signature_Witness_2_Text = "Signature of Second Witness";

        private string Signing_Name_Witness_1 = "c25:p25";
        private string Signing_Name_Witness_1_Text = "First Witness Full Name";
        private string Signing_Name_Witness_2= "v25:ai25";
        private string Signing_Name_Witness_2_Text = "Second Witness Full Name";

        private string Signing_IC_Witness_1 = "c29:p29";
        private string Signing_IC_Witness_1_Text = "First Witness NRIC No.";
        private string Signing_IC_Witness_2 = "v29:ai29";
        private string Signing_IC_Witness_2_Text = "Second Witness NRIC No.";

        private string Signing_Address_Witness_1 = "c35:p35";
        private string Signing_Address_Witness_1_Text = "First Witness Address";
        private string Signing_Address_Witness_2 = "v35:ai35";
        private string Signing_Address_Witness_2_Text = "Second Witness Address";

        private string Signing_Contact_Witness_1 = "c39:p39";
        private string Signing_Contact_Witness_1_Text = "First Witness Contact No.";
        private string Signing_Contact_Witness_2 = "v39:ai39";
        private string Signing_Contact_Witness_2_Text = "Second Witness Contact No.";
        #endregion

        #endregion

        #region Cover

        private void InitialCoverFormat(Worksheet xlWorkSheet)
        {
            //Page setup
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;
            xlWorkSheet.PageSetup.CenterHorizontally = true;
            //xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;
            xlWorkSheet.PageSetup.FitToPagesWide = false;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 0.8 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 1 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 0 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0.4 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 0.4 / pointToCMRate;

            xlWorkSheet.PageSetup.PrintArea = Will_Cover_Area;

            Range DOFormat = xlWorkSheet.get_Range(Will_Cover_Area).Cells;
            DOFormat.ColumnWidth = Page_Column_Width;
            DOFormat.RowHeight = Page_Row_Height;
            DOFormat.Interior.Color = Color.White;

            DOFormat = xlWorkSheet.get_Range(Cover_THE).Cells;
            DOFormat.Merge();
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Bodoni MT";
            DOFormat.Font.Size = Font_Size_Small;
            DOFormat.Value = Cover_Text_THE;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(Cover_LASTWILL).Cells;
            DOFormat.Merge();
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Bodoni MT";
            DOFormat.Font.Size = Font_Size_Big;
            DOFormat.Value = Cover_Text_LastWill;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(Cover_AND).Cells;
            DOFormat.Merge();
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Bodoni MT";
            DOFormat.Font.Size = Font_Size_Small;
            DOFormat.Value = Cover_Text_And;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(Cover_TESTAMENT).Cells;
            DOFormat.Merge();
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Bodoni MT";
            DOFormat.Font.Size = Font_Size_Big;
            DOFormat.Value = Cover_Text_Testament;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(Cover_OF).Cells;
            DOFormat.Merge();
            DOFormat.Interior.Color = Color.White;
            DOFormat.Font.Name = "Bodoni MT";
            DOFormat.Font.Size = Font_Size_Small;
            DOFormat.Value = Cover_Text_Of;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            #region LOGO

            string tempPath = Path.GetTempFileName();
            Resources.WillCoverImage.Save(tempPath + "will cover image.png");
            string filePath = tempPath + "will cover image.png";

            xlWorkSheet.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, -1, 772);

            #endregion
        }

        private void CoverNameInsert(Worksheet xlWorkSheet, string Name)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(Cover_TESTATOR_NAME).Cells;
            DataInsertArea.Merge();
            DataInsertArea.Interior.Color = Color.White;
            DataInsertArea.Font.Name = "Bodoni MT";
            DataInsertArea.Font.Size = Font_Size_Big;
            DataInsertArea.Value = Name.ToUpper();
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignCenter;
        }

        private void CoverICInsert(Worksheet xlWorkSheet, string IC)
        {
            int length = IC.Length;

            if (Regex.IsMatch(IC, @"^\d+$") && IC.Length == 12)
            {
                string temp = "";
                for(int i=0; i < IC.Length; i++)
                {
                    temp += IC[i].ToString();

                    if(i == 5 || i == 7)
                    {
                        temp += "-";

                    }
                }

                IC = temp;
            }

            Range DataInsertAre = xlWorkSheet.get_Range(Cover_TESTATOR_IC).Cells;
            DataInsertAre.Merge();
            DataInsertAre.Interior.Color = Color.White;
            DataInsertAre.NumberFormat = "@";
            DataInsertAre.Font.Name = "Bodoni MT";
            DataInsertAre.Font.Size = Font_Size_Big;
            DataInsertAre.Value = "(" + IC + ")";
            DataInsertAre.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DataInsertAre.VerticalAlignment = XlVAlign.xlVAlignCenter;
        }

        #endregion

        #region Content

        private void InitialContentFormat(Worksheet xlWorkSheet, int PageNo)
        {
            //Page setup
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;
            xlWorkSheet.PageSetup.CenterHorizontally = true;
            //xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;
            xlWorkSheet.PageSetup.FitToPagesWide = false;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 0.8 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 1 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 2 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 1.5 / pointToCMRate;

            xlWorkSheet.PageSetup.PrintArea = Will_Content_Area;

            Range DOFormat = xlWorkSheet.get_Range(Will_Cover_Area).Cells;
            DOFormat.ColumnWidth = Page_Column_Width;
            DOFormat.RowHeight = Page_Row_Height;
            DOFormat.Interior.Color = Color.White;

            DOFormat = xlWorkSheet.get_Range(Content_PageNo).Cells;
            DOFormat.Merge();
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Bold = true;
            DOFormat.Font.Size = 11;
            DOFormat.Value = PageNo.ToString();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(Content_CenterLine).Cells;
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Bold = true;
            DOFormat.Font.Size = 11;
            DOFormat.Value = "|";
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;

            DOFormat = xlWorkSheet.get_Range(Content_PageText).Cells;
            DOFormat.Merge();
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 11;
            DOFormat.Value = "P a g e";
            DOFormat.Font.Color = ColorTranslator.ToOle(Color.DimGray);
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            Color color = Color.WhiteSmoke;
            DOFormat = xlWorkSheet.get_Range(Content_PageNoArea).Cells;
            DOFormat.Borders[XlBordersIndex.xlEdgeBottom].Color = color;

            DOFormat = xlWorkSheet.get_Range(Content_Testator_Sign).Cells;
            DOFormat.Merge();
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 11;
            DOFormat.Value = Content_Testator_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;

            DOFormat = xlWorkSheet.get_Range(Content_Witness1_Sign).Cells;
            DOFormat.Merge();
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 11;
            DOFormat.Value = Content_Witness1_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;

            DOFormat = xlWorkSheet.get_Range(Content_Witness2_Sign).Cells;
            DOFormat.Merge();
            DOFormat.Font.Name = "Calibri";
            DOFormat.Font.Size = 11;
            DOFormat.Value = Content_Witness2_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignTop;
        }

        private void BlankStatementInsert(Worksheet xlWorkSheet, int rowIndex)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(Content_BlankStatement_Start + rowIndex + Content_BlankStatement_End + rowIndex).Cells;
            DataInsertArea.Merge();
            DataInsertArea.Value = Content_BlankStatement_Text;
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignRight;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignCenter;
        }

        private void WillStatementHeaderInsert(Worksheet xlWorkSheet)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(Content_LastWillAndTestament).Cells;
            DataInsertArea.Merge();
            DataInsertArea.Font.Bold = true;
            DataInsertArea.Font.Underline = true;
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DataInsertArea.Value = Content_LastWillAndTestament_Text;

            DataInsertArea = xlWorkSheet.get_Range(Content_Of).Cells;
            DataInsertArea.Merge();
            DataInsertArea.Font.Bold = true;
            DataInsertArea.Font.Underline = true;
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DataInsertArea.Value = Content_Of_Text;

            DataInsertArea = xlWorkSheet.get_Range(Content_TESTATOR_NAME).Cells;
            DataInsertArea.Merge();
            DataInsertArea.Font.Bold = true;
            DataInsertArea.Font.Underline = true;
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DataInsertArea.Value = Testator_Name.ToUpper();

        }

        private string GetBirthDateFromIC(string IC)
        {
            string BirthDate = "Invalid_IC_Format";
            string BirthYear = "";

            if (IC.Length >= 6)
            {
                string ThisYear = DateTime.Now.Year.ToString();

                int ThisYearLastTwoDigits = int.TryParse(ThisYear[ThisYear.Length - 2].ToString() + ThisYear[ThisYear.Length - 1].ToString(), out int i) ? i : 21;

                int ThisYearFirstTwoDigits = 20;

                if (ThisYear.Length >= 2)
                {
                    ThisYearFirstTwoDigits = int.TryParse(ThisYear[0].ToString() + ThisYear[1].ToString(), out i) ? i : 20;
                }

                int ICFirstTwoDigits = int.TryParse(IC[0].ToString() + IC[1].ToString(), out i) ? i : ThisYearFirstTwoDigits;

                if(ICFirstTwoDigits > ThisYearLastTwoDigits)
                {
                    BirthYear = (ThisYearFirstTwoDigits - 1).ToString() + ICFirstTwoDigits.ToString();
                }
                else
                {
                    BirthYear = ThisYearFirstTwoDigits.ToString() + ICFirstTwoDigits.ToString();

                }

                int ICMonthDigits = int.TryParse(IC[2].ToString() + IC[3].ToString(), out i) ? i : 1;

                int ICDayDigits = int.TryParse(IC[4].ToString() + IC[5].ToString(), out i) ? i : 1;

                string BirthMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(ICMonthDigits);
                string BirthDay = ICDayDigits.ToString();

                if(ICDayDigits < 10)
                {
                    BirthDay = "0" + ICDayDigits.ToString();
                }

                string GetDaySuffix(int day)
                {
                    switch (day)
                    {
                        case 1:
                        case 21:
                        case 31:
                            return "st";
                        case 2:
                        case 22:
                            return "nd";
                        case 3:
                        case 23:
                            return "rd";
                        default:
                            return "th";
                    }
                }

                BirthDate = BirthDay+ GetDaySuffix(ICDayDigits) + " " + BirthMonth + ", " + BirthYear;
            }


            
            return BirthDate;
        }

        private string GetBirthDateFromDatetime(DateTime BirthDay)
        {
            string GetDaySuffix(int day)
            {
                switch (day)
                {
                    case 1:
                    case 21:
                    case 31:
                        return "st";
                    case 2:
                    case 22:
                        return "nd";
                    case 3:
                    case 23:
                        return "rd";
                    default:
                        return "th";
                }
            }
            string BirthMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Testator_BirthDay_Datetime.Month);
            int ICDayDigits = Testator_BirthDay_Datetime.Day;

            string birthDay_String = ICDayDigits + GetDaySuffix(ICDayDigits) + " " + BirthMonth + ", " + Testator_BirthDay_Datetime.Year;

            return birthDay_String;
        }

        private int WillStatementContentInsert(Worksheet xlWorkSheet)
        {
            int row_int_toAdd = 0;
            string IC = Testator_IC;

            int length = IC.Length;

            if (Regex.IsMatch(IC, @"^\d+$") && IC.Length == 12)
            {
                string temp = "";
                for (int i = 0; i < IC.Length; i++)
                {
                    temp += IC[i].ToString();

                    if (i == 5 || i == 7)
                    {
                        temp += "-";

                    }
                }

                IC = temp;
            }

            string BirthDate = "";

            if(Testator_BirthDay_Datetime != DateTime.MaxValue)
            {
                BirthDate = GetBirthDateFromDatetime(Testator_BirthDay_Datetime);
            }
            else
            {
                BirthDate = GetBirthDateFromIC(IC);
            }


            string Statement = "This Will dated" +Content_WillStatement_DateSpace+"is made by me "+Testator_Name.ToUpper()+" (NRIC No."+ IC + ") born on " +BirthDate + " of " + Testator_Address +".";

            float textWidth = StringWidthMeasuring(Statement);
            int quotient = (int)textWidth / Content_Statement_Width;
            int remainder = (int)textWidth * Content_Statement_Width;

            if (quotient > 0)
            {
                if (remainder > 0)
                {
                    row_int_toAdd = quotient;
                }
                else
                {
                    row_int_toAdd = quotient - 1;
                }
            }

            string RowToInsert = Content_WillStatement_Start + Content_Statement_Row_Start + Content_WillStatement_End + (Content_Statement_Row_Start + row_int_toAdd).ToString();

            Range DataInsertArea = xlWorkSheet.get_Range(RowToInsert).Cells;
            DataInsertArea.Merge();
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignJustify;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignTop;
            DataInsertArea.Value = Statement;

            return Content_Statement_Row_Start + row_int_toAdd;
        }

        private void WillContentInsert(Worksheet xlWorkSheet, string area, string value)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.Merge();
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignJustify;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignTop;

            DataInsertArea.Value = value;

            if(value.Contains("[") || value.Contains("]"))
            {
                DataInsertArea.Interior.Color = Color.Yellow;

            }
        }

        private void WillContentOutDentIndexInsert(Worksheet xlWorkSheet, string area, int indexNo, bool Index_Enable)
        {
            if(indexNo != 0 && Index_Enable)
            {
                Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
                DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignTop;
                DataInsertArea.NumberFormat = "@";
                DataInsertArea.Value = indexNo.ToString() + ".";
            }
           
        }

        private void WillContentInDentIndexInsert(Worksheet xlWorkSheet, string area, int indexNo)
        {
            //char c = 'A';
            ////char c = 'b'; you may use lower case character.
            //int index = char.ToUpper(c) - 64;//index == 1

            char IndexAlphbet = (char) (indexNo + 64);

            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignTop;
            DataInsertArea.NumberFormat = "@";
            DataInsertArea.Value = IndexAlphbet.ToString().ToLower() + ")";
        }

        public string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) return "";
            if (number < 1) return "";
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            return "";
        }

        private void WillContentInDentSecondIndexInsert(Worksheet xlWorkSheet, string area, int indexNo)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(area).Cells;
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignTop;
            DataInsertArea.NumberFormat = "@";
            DataInsertArea.Value = "("+ToRoman(indexNo).ToLower() + ")";
        }

        #endregion

        #region Signing Page

        private void InitialSigningPageFormat(Worksheet xlWorkSheet)
        {
            //Page setup
            xlWorkSheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
            xlWorkSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.Zoom = false;
            xlWorkSheet.PageSetup.CenterHorizontally = true;
            //xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;
            xlWorkSheet.PageSetup.FitToPagesWide = false;

            double pointToCMRate = 0.035;
            xlWorkSheet.PageSetup.TopMargin = 0.8 / pointToCMRate;
            xlWorkSheet.PageSetup.BottomMargin = 1 / pointToCMRate;
            xlWorkSheet.PageSetup.HeaderMargin = 2 / pointToCMRate;
            xlWorkSheet.PageSetup.FooterMargin = 0 / pointToCMRate;
            xlWorkSheet.PageSetup.LeftMargin = 0 / pointToCMRate;
            xlWorkSheet.PageSetup.RightMargin = 1.5 / pointToCMRate;

            xlWorkSheet.PageSetup.PrintArea = Will_Content_Area;

            Range DOFormat = xlWorkSheet.get_Range(Will_Cover_Area).Cells;
            DOFormat.ColumnWidth = Page_Column_Width;
            DOFormat.RowHeight = Page_Row_Height;
            DOFormat.Interior.Color = Color.White;


            DOFormat = xlWorkSheet.get_Range(Signing_InWitness).Cells;
            DOFormat.Merge();
            DOFormat.Font.Name = "Times New Roman";
            DOFormat.Font.Bold = true;
            DOFormat.Font.Size = 11;
            DOFormat.Value = Signing_InWitness_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(Signing_IhaveHereunto).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_IhaveHereunto_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(Signing_Year).Cells;
            DOFormat.Merge();
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Value = ", " + DateTime.Now.Year.ToString();

            DOFormat = xlWorkSheet.get_Range(Signing_SignedBy).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_SignedBy_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            DOFormat = xlWorkSheet.get_Range(Signing_SignedBy_Divide).Cells;
            DOFormat.Value = Signing_SignedBy_Divide_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;

            Color color = Color.Black;

            DOFormat = xlWorkSheet.get_Range(Signing_Signature_Testator).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_Signature_Testator_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range(Signing_Signature_Witness_1).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_Signature_Witness_1_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range(Signing_Signature_Witness_2).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_Signature_Witness_2_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range(Signing_Name_Witness_1).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_Name_Witness_1_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range(Signing_Name_Witness_2).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_Name_Witness_2_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range(Signing_IC_Witness_1).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_IC_Witness_1_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range(Signing_IC_Witness_2).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_IC_Witness_2_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range(Signing_Address_Witness_1).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_Address_Witness_1_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range(Signing_Address_Witness_2).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_Address_Witness_2_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range(Signing_Contact_Witness_1).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_Contact_Witness_1_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;

            DOFormat = xlWorkSheet.get_Range(Signing_Contact_Witness_2).Cells;
            DOFormat.Merge();
            DOFormat.Value = Signing_Contact_Witness_2_Text;
            DOFormat.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            DOFormat.VerticalAlignment = XlVAlign.xlVAlignCenter;
            DOFormat.Borders[XlBordersIndex.xlEdgeTop].Color = color;
        }

        private void SigningTestatorNameInsert(Worksheet xlWorkSheet, string Name)
        {
            Range DataInsertArea = xlWorkSheet.get_Range(Signing_SignedBy_Name).Cells;
            DataInsertArea.Merge();
            DataInsertArea.Font.Bold = true;
            DataInsertArea.Style.ShrinkToFit = true;
            DataInsertArea.Value = Name.ToUpper();
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignCenter;
        }

        private string ApplyNewICFormat(string IC)
        {
            int length = IC.Length;

            if (Regex.IsMatch(IC, @"^\d+$") && IC.Length == 12)
            {
                string temp = "";
                for (int i = 0; i < IC.Length; i++)
                {
                    temp += IC[i].ToString();

                    if (i == 5 || i == 7)
                    {
                        temp += "-";

                    }
                }

                IC = temp;
            }

            return IC;
        }

        private void SigningTestatorICInsert(Worksheet xlWorkSheet, string IC)
        {
            int length = IC.Length;

            IC = ApplyNewICFormat(IC);

            Range DataInsertArea = xlWorkSheet.get_Range(Signing_SignedBy_IC).Cells;
            DataInsertArea.Merge();
            DataInsertArea.Font.Bold = true;
            DataInsertArea.Value = "NRIC No. " +IC;
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignCenter;
        }

        private void SigningTestatorStatementInsert(Worksheet xlWorkSheet, string IC)
        {
            int length = IC.Length;

            string Statement = Signing_SignedBy_Statement_Male;

            //int lastICDigit = int.TryParse(IC[IC.Length - 1].ToString(), out int i) ? i : 0;

            //if (lastICDigit % 2 == 0)
            //{
            //    //is even
            //    Statement = Signing_SignedBy_Statement_Female;
            //}

            if(MaleOrFemale(IC) == 0)
            {
                Statement = Signing_SignedBy_Statement_Female;
            }

            Range DataInsertArea = xlWorkSheet.get_Range(Signing_SignedBy_Statement).Cells;
            DataInsertArea.Merge();
            DataInsertArea.Value = Statement;
            DataInsertArea.HorizontalAlignment = XlHAlign.xlHAlignJustify;
            DataInsertArea.VerticalAlignment = XlVAlign.xlVAlignTop;
        }
        #endregion

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

        private int AddNewContentSheet(Workbook xlWorkBook, Worksheet xlWorkSheet, int sheetNo)
        {
            //add new sheet
            xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
            sheetNo++;

            xlWorkSheet.Name = "Sheet" + sheetNo;

            InitialContentFormat(xlWorkSheet, sheetNo - 1);

            return sheetNo;
        }

        private static Image DrawText(String text, Font font, Color textColor, Color backColor, double height, double width)
        {
            //create a bitmap image with specified width and height
            Image img = new Bitmap((int)width, (int)height);
            Graphics drawing = Graphics.FromImage(img);
            //get the size of text
            SizeF textSize = drawing.MeasureString(text, font);
            //set rotation point
            drawing.TranslateTransform(((int)width - textSize.Width) / 2, ((int)height - textSize.Height) / 2);
            //rotate text
            drawing.RotateTransform(-45);
            //reset translate transform
            drawing.TranslateTransform(-((int)width - textSize.Width) / 2, -((int)height - textSize.Height) / 2);
            //paint the background
            drawing.Clear(backColor);
            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);
            //draw text on the image at center position
            drawing.DrawString(text, font, textBrush, ((int)width - textSize.Width) / 2, ((int)height - textSize.Height) / 2);
            drawing.Save();
            return img;
        }

        private void ExportToExcel()
        {
            if (dt_Will != null && dt_Will.Rows.Count > 0)//dt_Will != null && dt_Will.Rows.Count > 0
            {
                
                string Path_PDF = @"D:\FinexWill\(Draft)\"+Testator_Name+"("+ApplyNewICFormat(Testator_IC) +@")\";
                string Path_Excel = Path_PDF;

                string ExcelFileName = Testator_Name + DateTime.Now.ToString(" dd_MM_yy hh-mm-ss") + ".xls";

                Directory.CreateDirectory(Path_PDF);
                //Directory.CreateDirectory(Path_Excel);

                SaveFileDialog sfd_PDF = new SaveFileDialog
                {
                    InitialDirectory = Path_PDF
                };

                sfd_PDF.Filter = "PDF (*.pdf)|*.pdf";
                sfd_PDF.FileName = Testator_Name + DateTime.Now.ToString(" dd_MM_yy hh-mm-ss") + ".pdf";

                if (sfd_PDF.ShowDialog() == DialogResult.OK)
                {
                    frmLoading.ShowLoadingScreen();
                    //tool.historyRecord(text.Excel, text.getExcelString(sfd_PDF.FileName), DateTime.Now, MainDashboard.USER_ID);

                    // Copy DataGridView results to clipboard
                    //copyAlltoClipboard();

                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    object misValue = Missing.Value;

                    #region Create Excel

                    Excel.Application xlexcel = new Excel.Application
                    {
                        PrintCommunication = false,
                        ScreenUpdating = false,
                        DisplayAlerts = false // Without this you will get two confirm overwrite prompts
                    };

                    Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);

                    xlexcel.StandardFont = "Times New Roman";
                    xlexcel.StandardFontSize = 12;
                    xlexcel.Calculation = XlCalculation.xlCalculationManual;
                    xlexcel.PrintCommunication = false;

                    Worksheet xlWorkSheet = xlWorkBook.ActiveSheet as Worksheet;

                    #endregion

                    int sheetNo = 0;
                    int rowNo = 1;
                    string RowToInsert = "";

                    if (sheetNo > 0)
                    {
                        xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);

                    }
                    sheetNo++;

                    #region Cover Page

                    xlexcel.PrintCommunication = true;
                    xlWorkSheet.Name = "Cover";

                    InitialCoverFormat(xlWorkSheet);

                    CoverNameInsert(xlWorkSheet, Testator_Name);
                    CoverICInsert(xlWorkSheet, Testator_IC);

                    #endregion


                    #region Content Page

                    #region Adding Will Statement

                    xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                    sheetNo++;

                    xlWorkSheet.Name = "Page " + (sheetNo - 1).ToString();

                    InitialContentFormat(xlWorkSheet, sheetNo - 1);

                    //insert Will Statement and Header
                    WillStatementHeaderInsert(xlWorkSheet);

                    rowNo = WillStatementContentInsert(xlWorkSheet) + 2;

                    #endregion

                    foreach (DataRow row in dt_Will.Rows) //foreach(DataRow row in dt_Will.Rows)
                    {
                        #region Getting Data From Datatable

                        string Para = row[dtHeader_Para].ToString();
                        int Outdent_Index = int.TryParse(row[dtHeader_Outdent_Index].ToString(), out int i)? i : -1;
                        int Indent_Index = int.TryParse(row[dtHeader_Indent_Index].ToString(), out i)? i : 0;
                        int Indent_Second_Index = int.TryParse(row[dtHeader_Indent_Second_Index].ToString(), out i) ? i : 0;
                        bool Index_Enable = bool.TryParse(row[dtHeader_Index_Enable].ToString(), out Index_Enable) ? Index_Enable : false;

                        float textWidth = StringWidthMeasuring(Para);

                       //MessageBox.Show(StringWidthMeasuring("If my [HUSBAND/ EX-HUSBAND] fails to act as the Guardian of my infant children for").ToString());

                        int row_int_toAdd = 0;
                        int quotient = 0;
                        int remainder = 0;
                        #endregion



                        if (Indent_Index == 0)
                        {
                            #region Para Size Measuring

                            quotient = (int)textWidth / Content_Outdent_Width;
                            remainder = (int)textWidth * Content_Outdent_Width;

                            if (quotient > 0)
                            {
                                if (remainder > 0)
                                {
                                    row_int_toAdd = quotient;
                                }
                                else
                                {
                                    row_int_toAdd = quotient - 1;
                                }
                            }

                            #endregion

                            #region Check if new Sheet needed

                            if (rowNo + row_int_toAdd > Content_UsableRow_Max)
                            {
                                #region Adding New Content Sheet

                                xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                                sheetNo++;

                                xlWorkSheet.Name = "Page " + (sheetNo - 1).ToString();

                                InitialContentFormat(xlWorkSheet, sheetNo - 1);

                                rowNo = Content_Row_Offset + 1;

                                #endregion
                            }

                            #endregion

                            #region Outdent Content Insert

                            RowToInsert = Content_Outdent_Start + rowNo.ToString() + Content_Outdent_End + (rowNo + row_int_toAdd).ToString();

                            WillContentInsert(xlWorkSheet, RowToInsert, Para);

                            RowToInsert = Content_Outdent_Index_Col_Start + rowNo.ToString() + Content_Outdent_Index_Col_End + rowNo.ToString();

                            WillContentOutDentIndexInsert(xlWorkSheet, RowToInsert, Outdent_Index, Index_Enable);

                            rowNo = rowNo + row_int_toAdd + 2; 

                            #endregion
                        }
                        else
                        {
                            if(Indent_Second_Index > 0)
                            {
                                #region Para Size Measuring

                                quotient = (int)textWidth / Content_Indent_Second_Width;
                                remainder = (int)textWidth * Content_Indent_Second_Width;

                                if (quotient > 0)
                                {
                                    if (remainder > 0)
                                    {
                                        row_int_toAdd = quotient;
                                    }
                                    else
                                    {
                                        row_int_toAdd = quotient - 1;
                                    }
                                }

                                #endregion

                                #region Check if new Sheet needed

                                if (rowNo + row_int_toAdd > Content_UsableRow_Max)
                                {
                                    #region Adding New Content Sheet

                                    xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                                    sheetNo++;

                                    xlWorkSheet.Name = "Page " + (sheetNo - 1).ToString();

                                    InitialContentFormat(xlWorkSheet, sheetNo - 1);

                                    rowNo = Content_Row_Offset + 1;

                                    #endregion
                                }

                                #endregion

                                #region Second Indent Content Insert

                                RowToInsert = Content_Indent_Second_Start + rowNo.ToString() + Content_Indent_Second_End + (rowNo + row_int_toAdd).ToString();

                                WillContentInsert(xlWorkSheet, RowToInsert, Para);

                                RowToInsert = Content_Indent_Second_Index_Col_Start + rowNo.ToString() + Content_Indent_Second_Index_Col_End + rowNo.ToString();

                                WillContentInDentSecondIndexInsert(xlWorkSheet, RowToInsert, Indent_Second_Index);

                                rowNo = rowNo + row_int_toAdd + 2;

                                #endregion
                            }
                            else
                            {
                                #region Para Size Measuring

                                quotient = (int)textWidth / Content_Indent_Width;
                                remainder = (int)textWidth * Content_Indent_Width;

                                if (quotient > 0)
                                {
                                    if (remainder > 0)
                                    {
                                        row_int_toAdd = quotient;
                                    }
                                    else
                                    {
                                        row_int_toAdd = quotient - 1;
                                    }
                                }

                                #endregion

                                #region Check if new Sheet needed

                                if (rowNo + row_int_toAdd > Content_UsableRow_Max)
                                {
                                    #region Adding New Content Sheet

                                    xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                                    sheetNo++;

                                    xlWorkSheet.Name = "Page " + (sheetNo - 1).ToString();

                                    InitialContentFormat(xlWorkSheet, sheetNo - 1);

                                    rowNo = Content_Row_Offset + 1;

                                    #endregion
                                }

                                #endregion

                                #region Indent Content Insert

                                RowToInsert = Content_Indent_Start + rowNo.ToString() + Content_Indent_End + (rowNo + row_int_toAdd).ToString();

                                WillContentInsert(xlWorkSheet, RowToInsert, Para);

                                RowToInsert = Content_Indent_Index_Col_Start + rowNo.ToString() + Content_Indent_Index_Col_End + rowNo.ToString();

                                WillContentInDentIndexInsert(xlWorkSheet, RowToInsert, Indent_Index);

                                rowNo = rowNo + row_int_toAdd + 2;

                                #endregion
                            }

                        }
                    }
                    rowNo++;
                    #region Check if new Sheet needed

                    if (rowNo > Content_UsableRow_Max)
                    {
                        #region Adding New Content Sheet

                        xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                        sheetNo++;

                        xlWorkSheet.Name = "Page " + (sheetNo - 1).ToString();

                        InitialContentFormat(xlWorkSheet, sheetNo - 1);

                        rowNo = Content_Row_Offset + 1;

                        #endregion
                    }

                    #endregion

                    BlankStatementInsert(xlWorkSheet, rowNo);

                    #endregion

                    #region Signing Page

                    xlWorkSheet = xlWorkBook.Sheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
                    sheetNo++;

                    xlWorkSheet.Name = "Signing Page";

                    InitialSigningPageFormat(xlWorkSheet);

                    SigningTestatorNameInsert(xlWorkSheet, Testator_Name);
                    SigningTestatorICInsert(xlWorkSheet, Testator_IC);
                    SigningTestatorStatementInsert(xlWorkSheet, Testator_IC);

                    #endregion

                    #region Save File & Release Object

                    //tool.historyRecord(text.DO_Exported, text.GetDOExportDetail(openFile, printFile, printPreview), DateTime.Now, MainDashboard.USER_ID, dalSPP.DOTableName, Convert.ToInt32(DONo));
                    frmLoading.CloseForm();

                    if (sheetNo > 0)
                    {
                        xlWorkBook.SaveAs(Path_Excel + ExcelFileName, XlFileFormat.xlWorkbookNormal,
                       misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                        xlWorkBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, sfd_PDF.FileName);

                        #region WATERMARK

                        if(cbDraftWaterMark.Checked)
                        {
                            PdfDocument pdf = new PdfDocument();
                            pdf.LoadFromFile(sfd_PDF.FileName);

                            foreach (PdfPageBase page in pdf.Pages)
                            {
                                PdfTilingBrush brush = new PdfTilingBrush(new SizeF(page.Canvas.ClientSize.Width, page.Canvas.ClientSize.Height / 3));
                                brush.Graphics.SetTransparency(0.3f);
                                brush.Graphics.Save();
                                brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
                                brush.Graphics.RotateTransform(-30);
                                brush.Graphics.DrawString("DRAFT", new PdfFont(PdfFontFamily.Helvetica, 50), PdfBrushes.Black, 0, 0, new PdfStringFormat(PdfTextAlignment.Center));
                                brush.Graphics.Restore();
                                brush.Graphics.SetTransparency(1);

                                page.Canvas.DrawRectangle(brush, new RectangleF(new PointF(0, 0), page.Canvas.ClientSize));
                            }

                            pdf.SaveToFile(sfd_PDF.FileName);
                        }
                      
                        System.Diagnostics.Process.Start(sfd_PDF.FileName);
                        #endregion
                    }

                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkSheet);

                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    #endregion

                }
            }
            else
            {
                MessageBox.Show("Will content not found!");
            }

           

        }

        private void dgvWillDraft_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = dgvWillDraft;

            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            string cellValue = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();
            string colName = dgv.Columns[colIndex].Name;

            if (colName.Contains(dtHeader_Para) && (cellValue.Contains("[") || cellValue.Contains("]")))
            {
                dgv.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.Yellow;

            }
        }

        private void SelectAllRowWithSameRowIndex(DataGridView dgv, int selectedRow)
        {
            if (selectedRow != -1)
            {
                DataTable dt = (DataTable)dgv.DataSource;

                int OutdentIndex = int.TryParse(dgv.Rows[selectedRow].Cells[dtHeader_Outdent_Index].Value.ToString(), out OutdentIndex)? OutdentIndex: 0;

                //if(OutdentIndex == 0)
                //{
                //    //find previous index
                //    for (int i = selectedRow; i >= 0; i--)
                //    {
                //        int Index_Looping = int.TryParse(dgv.Rows[i].Cells[dtHeader_Outdent_Index].Value.ToString(), out Index_Looping) ? Index_Looping : 0;

                //        if (Index_Looping > 0)
                //        {
                //            OutdentIndex = Index_Looping;
                //            break;
                //        }
                //    }
                //}

                //find all same index row
                bool indexFound = false;

                dgv.MultiSelect = true;

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    int Index_Looping = int.TryParse(dgv.Rows[i].Cells[dtHeader_Outdent_Index].Value.ToString(), out Index_Looping) ? Index_Looping : 0;

                    if (Index_Looping == 0 && indexFound)
                    {
                        dgv.Rows[i].Selected = true;
                    }
                    else if (Index_Looping == OutdentIndex)
                    {
                        indexFound = true;
                        dgv.Rows[i].Selected = true;
                    }
                    else if (Index_Looping > OutdentIndex)
                    {
                        break;
                    }

                }





            }
        }

        private void SelectAllRowWithSameSectionIndex(DataGridView dgv, int sectionRowIndex)
        {
            dgv.ClearSelection();

            if (sectionRowIndex > 0)
            {
                DataTable dt = (DataTable)dgv.DataSource;

                int OutdentIndex = sectionRowIndex;

                //find all same index row
                bool indexFound = false;

                dgv.MultiSelect = true;

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    int Index_Looping = int.TryParse(dgv.Rows[i].Cells[dtHeader_Outdent_Index].Value.ToString(), out Index_Looping) ? Index_Looping : 0;

                    if (Index_Looping == 0 && indexFound)
                    {
                        dgv.Rows[i].Selected = true;
                    }
                    else if (Index_Looping == OutdentIndex)
                    {
                        indexFound = true;
                        dgv.Rows[i].Selected = true;
                    }
                    else if (Index_Looping > OutdentIndex)
                    {
                        break;
                    }

                }





            }
        }

        private void dgvWillDraft_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = dgvWillDraft;

            int col = e.ColumnIndex;
            int row = e.RowIndex;

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (dgv.Columns[col].Name.Contains(dtHeader_Para))
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

                    //select all rows with same index
                    SelectAllRowWithSameRowIndex(dgv, row);

                }

            }
        }

        private void btnDraftWill_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Redraft will lose all the unsaved changes.\nAre you sure you want to redraft will?", "Message",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                dgvWillDraft.DataSource = null;
                GenerateWill(DT_DATA);
            }
                
        }

        #region Drag And Drop

        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;


        private void dgvWillDraft_DragOver(object sender, DragEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Point clientPoint = dgv.PointToClient(new Point(e.X, e.Y));
            int indexToDrop = dgv.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            string first_MacID = "1";
            string second_MacID = "2";

            if (dgv.HitTest(clientPoint.X, clientPoint.Y).Type == DataGridViewHitTestType.None)
            {
                //get index of last row of schedule list
                indexToDrop = dgv.Rows.Count - 1;
            }
            else if (indexToDrop < 0)
            {
                //get index of first row of schedule list
                indexToDrop = 0;
            }

            if (indexToDrop != -1 && rowIndexFromMouseDown != -1)
            {
                //first_MacID = dgv.Rows[rowIndexFromMouseDown].Cells[header_Mac].Value.ToString();
                //second_MacID = dgv.Rows[indexToDrop].Cells[header_Mac].Value.ToString();
            }

            e.Effect = DragDropEffects.Move;

        }

        private void dgvWillDraft_DragDrop(object sender, DragEventArgs e)
        {

            DataGridView dgv = (DataGridView)sender;

            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dgv.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
                dgv.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            //lblMoveText.Text = rowIndexOfItemUnderMouseToDrop.ToString();

            //if (rowIndexFromMouseDown <= dgvItemList.Rows.Count - 1)
            //{
            //    addedStatus = dgvItemList.Rows[rowIndexFromMouseDown].Cells[header_AddedStatus].Value.ToString();
            //}

            // If the drag operation was a move then remove and insert the row.
  
            if (e.Effect == DragDropEffects.Move)
            {
                int sectionIndex_From = int.TryParse(dgv.Rows[rowIndexFromMouseDown].Cells[dtHeader_Outdent_Index].Value.ToString(), out int i) ? i : 0;
                int sectionIndex_To = int.TryParse(dgv.Rows[rowIndexOfItemUnderMouseToDrop].Cells[dtHeader_Outdent_Index].Value.ToString(), out i) ? i : 0;

                int IndentIndex_From = int.TryParse(dgv.Rows[rowIndexFromMouseDown].Cells[dtHeader_Indent_Index].Value.ToString(), out  i) ? i : 0;
                int IndentIndex_To = int.TryParse(dgv.Rows[rowIndexOfItemUnderMouseToDrop].Cells[dtHeader_Indent_Index].Value.ToString(), out i) ? i : 0;

                int SecondIndentIndex_From = int.TryParse(dgv.Rows[rowIndexFromMouseDown].Cells[dtHeader_Indent_Second_Index].Value.ToString(), out i) ? i : 0;
                int SecondIndentIndex_To = int.TryParse(dgv.Rows[rowIndexOfItemUnderMouseToDrop].Cells[dtHeader_Indent_Second_Index].Value.ToString(), out i) ? i : 0;

                if (sectionIndex_From != sectionIndex_To)
                {
                    if (rowIndexFromMouseDown > rowIndexOfItemUnderMouseToDrop)
                    {
                        RowMoveUp(sectionIndex_From, sectionIndex_To);
                    }
                    else
                    {
                        RowMoveDown(sectionIndex_From, sectionIndex_To);
                    }

                    SelectAllRowWithSameSectionIndex(dgv, sectionIndex_From);
                }
                else 
                {
                    IndentRowMove(rowIndexFromMouseDown, rowIndexOfItemUnderMouseToDrop);
                }
              

                RearrangeOrPDF(false);
            }
        }

        private void dgvWillDraft_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.  
                    DragDropEffects dropEffect = dgv.DoDragDrop(
                         dgv.Rows[rowIndexFromMouseDown],
                         DragDropEffects.Move | DragDropEffects.Copy);
                }
            }
        }

        private void RemoveSection(DataGridView dgv, int SectionIndex)
        {
            DataTable dt = (DataTable)dgv.DataSource;

            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {

                int index_Loop = int.TryParse(row[dtHeader_Outdent_Index].ToString(), out int i) ? i : 0;

                if(index_Loop == SectionIndex)
                {
                    row.Delete();
                }
            }

            dt.AcceptChanges();

            RearrangeOrPDF(false);
        }

        private void RemoveRow(DataGridView dgv, int rowIndex)
        {
            DataTable dt = (DataTable)dgv.DataSource;

            dt.AcceptChanges();
            dt.Rows[rowIndex].Delete();
            dt.AcceptChanges();

            RearrangeOrPDF(false);

        }

        private int GetNewParaOutdentIndex(DataTable dt)
        {
            int Outdent_Index = 1;

            foreach(DataRow row in dt.Rows)
            {
                int Looping_Index = int.TryParse(row[dtHeader_Outdent_Index].ToString(), out int i) ? i : 0;

                if(Looping_Index > Outdent_Index)
                {
                    Outdent_Index = Looping_Index;
                }
            }

            return ++Outdent_Index;
        }

        private void ParaAddOutdent(DataGridView dgv, int rowIndex)
        {

            //DataTable dt = (DataTable) dgv.DataSource;

            //DataRow row = dt.NewRow();

            //row[dtHeader_TableCode] = GetNewWillTableCode();

            //int newOutdentIndex = GetNewParaOutdentIndex(dt);

            //row[dtHeader_Index_Enable] = newOutdentIndex;
            //row[dtHeader_Outdent_Index] = 0;
            //row[dtHeader_Indent_Index] = 0;
            //row[dtHeader_Indent_Second_Index] = 0;
            //row[dtHeader_Index] = GetIndexSring(newOutdentIndex, 0, 0, indexEnable);

            ////row[dtHeader_Para] = Para;

            //dt.Rows.InsertAt(dr, 1);

        }

        private void ParaAddIndent(DataGridView dgv, int rowIndex)
        {

        }

        private void ParaAddSecondIndent(DataGridView dgv, int rowIndex)
        {

        }

        private void dgvWillDraft_MouseDown(object sender, MouseEventArgs e)
        {

            DataGridView dgv = (DataGridView)sender;

            //dragFromItemList = false;

            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dgv.HitTest(e.X, e.Y).RowIndex;

            //lblDragText.Text = rowIndexFromMouseDown.ToString();

            int colIndex = dgv.HitTest(e.X, e.Y).ColumnIndex;

            if (e.Button == MouseButtons.Right && rowIndexFromMouseDown >= 0)
            {
                ContextMenuStrip my_menu = new ContextMenuStrip();

                dgv.CurrentCell = dgv.Rows[rowIndexFromMouseDown].Cells[colIndex];

                //Can leave these here -doesn't hurt
                dgv.Rows[rowIndexFromMouseDown].Selected = true;
                dgv.Focus();

                if(dgv.Columns[colIndex].Name.Contains(dtHeader_Para))
                {
                    my_menu.Items.Add(MENU_PARA_REMOVE).Name = MENU_PARA_REMOVE;

                    my_menu.Items.Add(MENU_PARA_ADD_OUTDENT).Name = MENU_PARA_ADD_OUTDENT;
                    my_menu.Items.Add(MENU_PARA_ADD_INDENT).Name = MENU_PARA_ADD_INDENT;
                    my_menu.Items.Add(MENU_PARA_ADD_SECOND_INDENT).Name = MENU_PARA_ADD_SECOND_INDENT;
                }
                else
                {
                    my_menu.Items.Add(MENU_SECTION_REMOVE).Name = MENU_SECTION_REMOVE;
                    //my_menu.Items.Add(MENU_PARA_ADD_SECTION).Name = MENU_PARA_ADD_SECTION;
                    SelectAllRowWithSameRowIndex(dgv,rowIndexFromMouseDown);
                }

                
                

                my_menu.Show(Cursor.Position.X, Cursor.Position.Y);

                my_menu.ItemClicked += new ToolStripItemClickedEventHandler(Schedule_ItemClicked);

            }

            else if (rowIndexFromMouseDown != -1)
            {
                //int itemListRowIndex = int.TryParse(dgv.Rows[rowIndexFromMouseDown].Cells[header_Index].Value.ToString(), out itemListRowIndex) ? itemListRowIndex : -1;
               
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            }

            else
            {
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
            }



        }

        private void Schedule_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type

                DataGridView dgv = dgvWillDraft;

                int rowIndex = dgv.CurrentCell.RowIndex;
                int sectionIndex = int.TryParse(dgv.Rows[rowIndex].Cells[dtHeader_Outdent_Index].Value.ToString(), out int i) ? i : 0;

                if (rowIndex >= 0)
                {
                    string ClickedItem = e.ClickedItem.Name.ToString();

                    if(ClickedItem == MENU_SECTION_REMOVE)
                    {
                        RemoveSection(dgv, sectionIndex);
                    }
                    else if (ClickedItem == MENU_PARA_REMOVE)
                    {
                        RemoveRow(dgv, rowIndex);
                    }
                }



                Cursor = Cursors.Arrow; // change cursor to normal type
            }
            catch (Exception ex)
            {
                tool.saveToTextAndMessageToUser(ex);
            }
        }

        private void RearrangeIndex(DataGridView dgv)
        {
            DataTable dt = (DataTable) dgv.DataSource;

            int index = 0;
            int Indent_Index = 0;
            int Second_Indent_Index = 1;

            int old_Index = -1;
            int old_Indent_Index = -1;

            foreach (DataRow row in dt.Rows)
            {
                int Index_Looping = int.TryParse(row[dtHeader_Outdent_Index].ToString(), out int i) ? i : 0;
                int Indent_Index_Looping = int.TryParse(row[dtHeader_Indent_Index].ToString(), out i) ? i : 0;

                int Index_Intent = int.TryParse(row[dtHeader_Indent_Index].ToString(), out i) ? i : 0;
                int Index_Intent_Second = int.TryParse(row[dtHeader_Indent_Second_Index].ToString(), out i) ? i : 0;
                bool indexEnable = bool.TryParse(row[dtHeader_Index_Enable].ToString(), out indexEnable) ? indexEnable : false;

                if (old_Index != Index_Looping)
                {
                    old_Index = Index_Looping;
                    index++;
                    Indent_Index = 0;
                    old_Indent_Index = -1;
                }

                if(Indent_Index_Looping == 0)
                {
                    Indent_Index = 0;
                }
                else if (old_Indent_Index != Indent_Index_Looping)
                {
                    old_Indent_Index = Indent_Index_Looping;
                    Indent_Index++;
                }

                if (Index_Intent_Second == 0)
                {
                    Second_Indent_Index = 1;
                }
                else
                {
                    Index_Intent_Second = Second_Indent_Index++;
                }

                //change Index
                row[dtHeader_Outdent_Index] = index;
                row[dtHeader_Indent_Index] = Indent_Index;
                row[dtHeader_Indent_Second_Index] = Index_Intent_Second;
                row[dtHeader_Index] = GetIndexSring(index, Indent_Index, Index_Intent_Second, indexEnable);
            }

        }

        private void RowMoveUp(int sectionIndex_From, int sectionIndex_To)
        {
            if (sectionIndex_From != sectionIndex_To)
            {
                DataTable dt = (DataTable)dgvWillDraft.DataSource;

                int RowIndex_To = -1;
                int RowIndex_From = -1; 

                foreach (DataRow row in dt.Rows)
                {
                    int index_Looping = int.TryParse(row[dtHeader_Outdent_Index].ToString(), out int i) ? i : -1;

                    if(index_Looping == sectionIndex_To)
                    {
                        RowIndex_To = dt.Rows.IndexOf(row);
                        break;
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    int index_Looping = int.TryParse(row[dtHeader_Outdent_Index].ToString(), out int i) ? i : -1;

                    if (index_Looping == sectionIndex_From && dt.Rows.IndexOf(row) > RowIndex_To)
                    {
                        RowIndex_From = dt.Rows.IndexOf(row);
                        break;
                    }
                }

                if(RowIndex_To != -1 && RowIndex_From !=-1 && RowIndex_From > RowIndex_To)
                {
                    DataRow DataRow_From = dt.Rows[RowIndex_From];
                    DataRow DataRow_To = dt.Rows[RowIndex_To];

                    DataRow newRow = dt.NewRow();
                    newRow.ItemArray = DataRow_From.ItemArray; // copying data

                    dt.Rows.Remove(DataRow_From);

                    int rowCount = dt.Rows.Count;

                    if (RowIndex_To > rowCount)
                    {
                        RowIndex_To = rowCount;
                    }

                    dt.Rows.InsertAt(newRow, RowIndex_To); // index 2

                    dt.AcceptChanges();

                    RowMoveUp(sectionIndex_From, sectionIndex_To);
                }
              
            }

        }

        private void RowMoveDown(int sectionIndex_From, int sectionIndex_To)
        {
            if (sectionIndex_From != sectionIndex_To)
            {
                DataTable dt = (DataTable)dgvWillDraft.DataSource;

                int RowIndex_To = -1;
                int RowIndex_From = -1;

                foreach (DataRow row in dt.Rows)
                {
                    int index_Looping = int.TryParse(row[dtHeader_Outdent_Index].ToString(), out int i) ? i : -1;

                    if (index_Looping == sectionIndex_To)
                    {
                        RowIndex_To = dt.Rows.IndexOf(row);
                        break;
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    int index_Looping = int.TryParse(row[dtHeader_Outdent_Index].ToString(), out int i) ? i : -1;

                    if (index_Looping == sectionIndex_From && dt.Rows.IndexOf(row) < RowIndex_To)
                    {
                        RowIndex_From = dt.Rows.IndexOf(row);
                        break;
                    }
                }

                for(int i = dt.Rows.Count -1; i >= 0; i--)
                {
                    int index_Looping = int.TryParse(dt.Rows[i][dtHeader_Outdent_Index].ToString(), out int j) ? j : -1;

                    if (index_Looping == sectionIndex_From && i < RowIndex_To)
                    {
                        RowIndex_From = i;
                        break;
                    }
                }

                if (RowIndex_To != -1 && RowIndex_From != -1 && RowIndex_From < RowIndex_To)
                {
                    DataRow DataRow_From = dt.Rows[RowIndex_From];
                    DataRow DataRow_To = dt.Rows[RowIndex_To];

                    DataRow newRow = dt.NewRow();
                    newRow.ItemArray = DataRow_From.ItemArray; // copying data

                    dt.Rows.Remove(DataRow_From);

                    int rowCount = dt.Rows.Count;

                    if (RowIndex_To > rowCount)
                    {
                        RowIndex_To = rowCount;
                    }

                    dt.Rows.InsertAt(newRow, RowIndex_To); // index 2

                    dt.AcceptChanges();

                    RowMoveDown(sectionIndex_From, sectionIndex_To);
                }

            }

        }

        private void IndentRowMove(int row_1, int row_2)
        {

            if (row_1 != row_2)
            {
                DataTable dt = (DataTable)dgvWillDraft.DataSource;
                DataRow selected_Row = dt.Rows[row_1];
                DataRow target_Row = dt.Rows[row_2];

                DataRow newRow = dt.NewRow();
                newRow.ItemArray = selected_Row.ItemArray; // copying data

                dt.Rows.Remove(selected_Row);

                int rowCount = dt.Rows.Count;

                if (row_2 > rowCount)
                {
                    row_2 = rowCount;
                }

                dt.Rows.InsertAt(newRow, row_2); // index 2

                dt.AcceptChanges();

                rowCount = dt.Rows.Count;

                if (row_2 > rowCount)
                {
                    row_2 = rowCount;
                }

                dgvWillDraft.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvWillDraft.ClearSelection();
                dgvWillDraft.Rows[row_2].Selected = true;
            }

        }

        #endregion

        private void RearrangeOrPDF(bool PDFReady)
        {
            if(PDFReady)
            {
                INDEX_PENDING_REAARANGE = false;
                btnPDF.Text = "PDF";
            }
            else
            {
                btnPDF.Text = "INDEX REARRANGE";
                INDEX_PENDING_REAARANGE = true;
            }
        }

        private void btnIndexRearrange_Click(object sender, EventArgs e)
        {
            RearrangeIndex(dgvWillDraft);
           
        }

        private void dgvWillDraft_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DgvUIEdit(dgvWillDraft);

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
