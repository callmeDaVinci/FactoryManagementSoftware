using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using System.Linq;
using System.Threading;
using Syncfusion.XlsIO.Parser.Biff_Records;
using Guna.UI.WinForms;
using System.Configuration;
using Syncfusion.XlsIO;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace FactoryManagementSoftware.UI
{
    public partial class frmDOFormat : Form
    {
        #region Default Settings

        Tool tool = new Tool();
        Text text = new Text();
        doFormatDAL dalDoFormat = new doFormatDAL();

        private bool DATA_SAVED = false;

        private bool EDIT_MODE = false;

        public frmDOFormat()
        {
            InitializeComponent();
            InitialSettings();
            EDIT_MODE = false;
            TABLE_CODE = -1;
        }

        private int TABLE_CODE;
        public frmDOFormat(int format_tbl_code)
        {
            InitializeComponent();

            EDIT_MODE = true;
            TABLE_CODE = format_tbl_code;

            InitialSettings();
           
        }

        #endregion

        #region UI/UX
        private void InitialSettings()
        {
            cmbResetPeriod.Text = "Never";
            LoadData(TABLE_CODE);
        }

        #region Appearance

        #endregion

        #region Event Handlers

        private void frmDOEditing_Load(object sender, EventArgs e)
        {


        }

        private void frmDOEditing_Shown(object sender, EventArgs e)
        {


        }

        private void frmDOEditing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved D/O data. Leave without saving? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                }
                else
                {
                    e.Cancel = true;
                }

            }
        }

        private void OnlyNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void UpdatePreview(object sender, EventArgs e)
        {
            RunningNumberPreview(); // Call the preview method
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            // VALIDATION
            if (!ValidateForm())
            {
                return; // Exit if validation fails
            }

            DialogResult confirmationResult;

            if (EDIT_MODE && TABLE_CODE > 0)
            {
                // CONFIRMATION for updating
                confirmationResult = MessageBox.Show(
                    "Are you sure you want to update this DO format?",
                    "Confirm Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
            }
            else
            {
                // CONFIRMATION for saving new format
                confirmationResult = MessageBox.Show(
                    "Are you sure you want to save this new DO format?",
                    "Confirm Save",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
            }

            // Check if the user confirmed the action
            if (confirmationResult == DialogResult.Yes)
            {
                //key in password
                frmVerification frm = new frmVerification(text.PW_Level_3)
                {
                    StartPosition = FormStartPosition.CenterScreen
                };

                frm.ShowDialog();

                if (frmVerification.PASSWORD_MATCHED)
                {
                    bool saveSuccess = false;

                    // INSERT OR UPDATE TO DATABASE
                    if (EDIT_MODE && TABLE_CODE > 0)
                    {
                        // Update logic here if applicable
                        saveSuccess = Update();  // Assuming you have an Update method for editing existing entries
                    }
                    else
                    {
                        saveSuccess = Insert();  // Save new format
                    }

                    if (saveSuccess)
                    {
                        DATA_SAVED = true;
                        MessageBox.Show("DO format has been successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
               
              
            }
        }


        // Validation Method
        private bool ValidateForm()
        {
            // Validate Date Format (it should not be empty)
            if (string.IsNullOrEmpty(txtDateFormat.Text))
            {
                MessageBox.Show("Date Format is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDateFormat.Focus();
                return false;
            }

            // Validate Number Length (it must be greater than 0)
            if (!int.TryParse(txtNumberLength.Text, out int numberLength) || numberLength <= 0)
            {
                MessageBox.Show("Number Length must be a valid number greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumberLength.Focus();
                return false;
            }

            // Validate Prefix (Optional, but you can enforce checks if needed)
            if (string.IsNullOrEmpty(txtPrefix.Text))
            {
                var result = MessageBox.Show("Prefix is empty. Do you want to continue without a prefix?",
                                             "Confirm Prefix",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    txtPrefix.Focus();
                    return false;
                }
            }

            // Validate DO Type (If required)
            if (string.IsNullOrEmpty(txtDOType.Text))
            {
                MessageBox.Show("DO Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDOType.Focus();
                return false;
            }

            // You can add more validations as necessary based on your requirements

            return true; // All validations passed
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!DATA_SAVED)
            {
                DialogResult dialogResult = MessageBox.Show("Unsaved D/O data. Leave without saving? ", "Message",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Close();
                }


            }
        }


        #endregion

        #endregion

        #region Logic
        private void RunningNumberPreview()
        {
            string prefix = txtPrefix.Text;
            string dateFormat = txtDateFormat.Text;
            string suffix = txtSuffix.Text;

            string nextNumber = txtNextNumber.Text;
            int numberLength = int.TryParse(txtNumberLength.Text, out numberLength) ? numberLength : 1;

            if (numberLength <= 0)
            {
                numberLength = 1;
            }

            DateTime dateToday = DateTime.Today;

            // Replace custom date placeholders with .NET's DateTime format specifiers
            if (!string.IsNullOrEmpty(dateFormat))
            {
                // Replace placeholders with DateTime format strings
                dateFormat = dateFormat
                    .Replace("[yyyy]", "yyyy")
                    .Replace("[yy]", "yy")
                    .Replace("[mm]", "MM")
                    .Replace("[mmm]", "MMM")
                    .Replace("[dd]", "dd")
                    .Replace("[d]", "d");
            }

            // Format the date using the updated format string
            string date = "";
            try
            {
                if (!string.IsNullOrEmpty(dateFormat))
                {
                    date = dateToday.ToString(dateFormat);
                }
            }
            catch (FormatException)
            {
                // Handle invalid date format
                string message = "Invalid date format. Please use valid date format patterns.\n\n" +
                "For example,\n[dd] = 01, [d] = 1;\n[mm] = 10, [mmm] = Oct\n[yy] = 24, [yyyy] = 2024.\n\n" +
                "Allowed symbols: '/', '-'.";

                if (txtDateFormat.Text != "[")
                {
                    MessageBox.Show(message, "Date Format Error");
                }


                return;
            }

            // Format the next number to the specified length, padding with leading zeroes if necessary
            string formattedNumber = nextNumber.PadLeft(numberLength, '0');

            // Combine the parts: Prefix + Date + Formatted Number + Suffix
            string numberPreview = prefix + date + formattedNumber + suffix;

            // Display the preview (for example, in a label or a textbox)
            txtRunningNumberPreview.Text = numberPreview;
        }

        #endregion

        #region Database

        private void LoadData(int tblCode)
        {
            if(EDIT_MODE && tblCode > 0)
            {
                DataTable dt_DOFormat = dalDoFormat.SelectAll();

                foreach(DataRow row in dt_DOFormat.Rows)
                {
                    if(tblCode.ToString() == row[dalDoFormat.tblCode].ToString())
                    {
                        txtDOType.Text = row[dalDoFormat.doType].ToString();
                        txtDOVersionControl.Text = row[dalDoFormat.versionControl].ToString();
                        txtPrefix.Text = row[dalDoFormat.prefix].ToString();
                        txtDateFormat.Text = row[dalDoFormat.dateFormat].ToString();
                        txtSuffix.Text = row[dalDoFormat.suffix].ToString();
                        txtNumberLength.Text = row[dalDoFormat.runningNumberLength].ToString();

                        int nextNumber = int.TryParse(row[dalDoFormat.lastNumber].ToString(),out nextNumber) ? nextNumber : 0;
                        nextNumber++;
                        txtNextNumber.Text = nextNumber.ToString();


                        bool MonthlyReset = bool.TryParse(row[dalDoFormat.isMonthlyReset].ToString(), out MonthlyReset) ? MonthlyReset : false;
                        bool YearlyReset = bool.TryParse(row[dalDoFormat.isYearlyReset].ToString(), out YearlyReset) ? YearlyReset : false;

                        cmbResetPeriod.Text = YearlyReset? "Yearly" : MonthlyReset ? "Monthly" : "None";


                        RunningNumberPreview();

                        break;
                    }
                }
            }
        }

        private bool Insert()
        {
            DateTime DateNow = DateTime.Now;

            // Fetch and parse fields
            string prefix = txtPrefix.Text;
            string dateFormat = txtDateFormat.Text;
            string suffix = txtSuffix.Text;
            int runningNumberLength = int.TryParse(txtNumberLength.Text, out int i) ? i : 1;
            int nextNumber = 0;


            // Format the next number with leading zeros, based on running number length
            string formattedNumber = nextNumber.ToString().PadLeft(runningNumberLength, '0');

            // Combine prefix, date, formatted number, and suffix into no_format
            string noFormat = prefix + dateFormat + formattedNumber + suffix;

            // Create new format and set properties
            doFormatBLL newFormat = new doFormatBLL()
            {
                letter_head_tbl_code = -1,
                sheet_format_tbl_code = -1,
                do_type = txtDOType.Text,
                prefix = prefix,
                date_format = txtDateFormat.Text, // Keep raw date format for future use
                suffix = suffix,
                no_format = noFormat, // Combined format
                running_number_length = runningNumberLength,
                next_number = nextNumber,
                reset_running_number = cmbResetPeriod.Text != "Never" && cmbResetPeriod.Text != "" ? "True" : "False",
                remark = "",
                updated_by = MainDashboard.USER_ID,
                updated_date = DateNow,
                isMonthlyReset = cmbResetPeriod.Text == "Monthly",
                isYearlyReset = cmbResetPeriod.Text == "Yearly",
                last_reset_date = DateNow,
                version_control = txtDOVersionControl.Text
            };

            // Insert data using the DAL
            return dalDoFormat.Insert(newFormat);
        }

        private bool Update()
        {
            DateTime DateNow = DateTime.Now;

            // Fetch and parse fields
            string prefix = txtPrefix.Text;
            string dateFormat = txtDateFormat.Text;
            string suffix = txtSuffix.Text;
            int runningNumberLength = int.TryParse(txtNumberLength.Text, out int i) ? i : 1;
            int nextNumber = 0;


            // Format the next number with leading zeros, based on running number length
            string formattedNumber = nextNumber.ToString().PadLeft(runningNumberLength, '0');

            // Combine prefix, date, formatted number, and suffix into no_format
            string noFormat = prefix + dateFormat + formattedNumber + suffix;

            // Create new format and set properties
            doFormatBLL newFormat = new doFormatBLL()
            {
                tbl_code = TABLE_CODE,
                letter_head_tbl_code = -1,
                sheet_format_tbl_code = -1,
                do_type = txtDOType.Text,
                prefix = prefix,
                date_format = txtDateFormat.Text, // Keep raw date format for future use
                suffix = suffix,
                no_format = noFormat, // Combined format
                running_number_length = runningNumberLength,
                next_number = nextNumber,
                reset_running_number = cmbResetPeriod.Text != "Never" && cmbResetPeriod.Text != "" ? "True" : "False",
                remark = "",
                updated_by = MainDashboard.USER_ID,
                updated_date = DateNow,
                isMonthlyReset = cmbResetPeriod.Text == "Monthly",
                isYearlyReset = cmbResetPeriod.Text == "Yearly",
                last_reset_date = DateNow,
                version_control = txtDOVersionControl.Text
            };

            // Insert data using the DAL
            return dalDoFormat.Update(newFormat);
        }
        #endregion
    }
}
