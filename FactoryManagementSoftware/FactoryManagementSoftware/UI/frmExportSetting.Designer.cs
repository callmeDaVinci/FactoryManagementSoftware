namespace FactoryManagementSoftware.UI
{
    partial class frmExportSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbPrintPreview = new System.Windows.Forms.CheckBox();
            this.cbPrintFile = new System.Windows.Forms.CheckBox();
            this.cbOpenFile = new System.Windows.Forms.CheckBox();
            this.dtpDODate = new System.Windows.Forms.DateTimePicker();
            this.cbAllInOne = new System.Windows.Forms.CheckBox();
            this.cbSeparate = new System.Windows.Forms.CheckBox();
            this.btnApply = new Guna.UI.WinForms.GunaGradientButton();
            this.btnCancel = new Guna.UI.WinForms.GunaGradientButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbPDF = new System.Windows.Forms.CheckBox();
            this.cbExcel = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.Location = new System.Drawing.Point(42, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "D/O Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbPrintPreview);
            this.groupBox1.Controls.Add(this.cbPrintFile);
            this.groupBox1.Controls.Add(this.cbOpenFile);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(44, 251);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 110);
            this.groupBox1.TabIndex = 175;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AFTER EXPORT";
            // 
            // cbPrintPreview
            // 
            this.cbPrintPreview.AutoSize = true;
            this.cbPrintPreview.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbPrintPreview.Location = new System.Drawing.Point(99, 66);
            this.cbPrintPreview.Name = "cbPrintPreview";
            this.cbPrintPreview.Size = new System.Drawing.Size(111, 23);
            this.cbPrintPreview.TabIndex = 2;
            this.cbPrintPreview.Text = "Print Preview";
            this.cbPrintPreview.UseVisualStyleBackColor = true;
            this.cbPrintPreview.Visible = false;
            this.cbPrintPreview.CheckedChanged += new System.EventHandler(this.cbPrintPreview_CheckedChanged);
            // 
            // cbPrintFile
            // 
            this.cbPrintFile.AutoSize = true;
            this.cbPrintFile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbPrintFile.Location = new System.Drawing.Point(15, 66);
            this.cbPrintFile.Name = "cbPrintFile";
            this.cbPrintFile.Size = new System.Drawing.Size(60, 23);
            this.cbPrintFile.TabIndex = 1;
            this.cbPrintFile.Text = "Print";
            this.cbPrintFile.UseVisualStyleBackColor = true;
            this.cbPrintFile.Visible = false;
            this.cbPrintFile.CheckedChanged += new System.EventHandler(this.cbPrintFile_CheckedChanged);
            // 
            // cbOpenFile
            // 
            this.cbOpenFile.AutoSize = true;
            this.cbOpenFile.Checked = true;
            this.cbOpenFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOpenFile.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbOpenFile.Location = new System.Drawing.Point(15, 28);
            this.cbOpenFile.Name = "cbOpenFile";
            this.cbOpenFile.Size = new System.Drawing.Size(89, 23);
            this.cbOpenFile.TabIndex = 0;
            this.cbOpenFile.Text = "Open File";
            this.cbOpenFile.UseVisualStyleBackColor = true;
            // 
            // dtpDODate
            // 
            this.dtpDODate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDODate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDODate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDODate.Location = new System.Drawing.Point(44, 44);
            this.dtpDODate.Name = "dtpDODate";
            this.dtpDODate.RightToLeftLayout = true;
            this.dtpDODate.Size = new System.Drawing.Size(347, 27);
            this.dtpDODate.TabIndex = 176;
            this.dtpDODate.ValueChanged += new System.EventHandler(this.dtpDODate_ValueChanged);
            // 
            // cbAllInOne
            // 
            this.cbAllInOne.AutoSize = true;
            this.cbAllInOne.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbAllInOne.Location = new System.Drawing.Point(33, 87);
            this.cbAllInOne.Name = "cbAllInOne";
            this.cbAllInOne.Size = new System.Drawing.Size(124, 23);
            this.cbAllInOne.TabIndex = 177;
            this.cbAllInOne.Text = "All in One Excel";
            this.cbAllInOne.UseVisualStyleBackColor = true;
            this.cbAllInOne.Visible = false;
            this.cbAllInOne.CheckedChanged += new System.EventHandler(this.cbAllInOne_CheckedChanged);
            // 
            // cbSeparate
            // 
            this.cbSeparate.AutoSize = true;
            this.cbSeparate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSeparate.Location = new System.Drawing.Point(33, 58);
            this.cbSeparate.Name = "cbSeparate";
            this.cbSeparate.Size = new System.Drawing.Size(84, 23);
            this.cbSeparate.TabIndex = 178;
            this.cbSeparate.Text = "Separate";
            this.cbSeparate.UseVisualStyleBackColor = true;
            this.cbSeparate.Visible = false;
            this.cbSeparate.CheckedChanged += new System.EventHandler(this.cbSeparate_CheckedChanged);
            // 
            // btnApply
            // 
            this.btnApply.AnimationHoverSpeed = 0.07F;
            this.btnApply.AnimationSpeed = 0.03F;
            this.btnApply.BackColor = System.Drawing.Color.Transparent;
            this.btnApply.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(107)))), ((int)(((byte)(236)))));
            this.btnApply.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnApply.BorderColor = System.Drawing.Color.Black;
            this.btnApply.BorderSize = 1;
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnApply.FocusedColor = System.Drawing.Color.Empty;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Image = null;
            this.btnApply.ImageSize = new System.Drawing.Size(20, 20);
            this.btnApply.Location = new System.Drawing.Point(46, 430);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnApply.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnApply.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnApply.OnHoverForeColor = System.Drawing.Color.White;
            this.btnApply.OnHoverImage = null;
            this.btnApply.OnPressedColor = System.Drawing.Color.Black;
            this.btnApply.Radius = 2;
            this.btnApply.Size = new System.Drawing.Size(345, 40);
            this.btnApply.TabIndex = 228;
            this.btnApply.Text = "Apply";
            this.btnApply.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AnimationHoverSpeed = 0.07F;
            this.btnCancel.AnimationSpeed = 0.03F;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BaseColor1 = System.Drawing.Color.White;
            this.btnCancel.BaseColor2 = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.FocusedColor = System.Drawing.Color.Empty;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnCancel.Image = null;
            this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnCancel.Location = new System.Drawing.Point(46, 491);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnCancel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnCancel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnCancel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnCancel.OnHoverImage = null;
            this.btnCancel.OnPressedColor = System.Drawing.Color.Black;
            this.btnCancel.Radius = 2;
            this.btnCancel.Size = new System.Drawing.Size(345, 40);
            this.btnCancel.TabIndex = 231;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnCancel.Click += new System.EventHandler(this.btnCancelDOMode_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.cbPDF);
            this.groupBox2.Controls.Add(this.cbExcel);
            this.groupBox2.Controls.Add(this.cbSeparate);
            this.groupBox2.Controls.Add(this.cbAllInOne);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(46, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(345, 138);
            this.groupBox2.TabIndex = 232;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "D/O FORMAT";
            // 
            // cbPDF
            // 
            this.cbPDF.AutoSize = true;
            this.cbPDF.Checked = true;
            this.cbPDF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPDF.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbPDF.Location = new System.Drawing.Point(197, 29);
            this.cbPDF.Name = "cbPDF";
            this.cbPDF.Size = new System.Drawing.Size(56, 23);
            this.cbPDF.TabIndex = 179;
            this.cbPDF.Text = "PDF";
            this.cbPDF.UseVisualStyleBackColor = true;
            this.cbPDF.CheckedChanged += new System.EventHandler(this.cbPDF_CheckedChanged);
            // 
            // cbExcel
            // 
            this.cbExcel.AutoSize = true;
            this.cbExcel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbExcel.Location = new System.Drawing.Point(13, 29);
            this.cbExcel.Name = "cbExcel";
            this.cbExcel.Size = new System.Drawing.Size(60, 23);
            this.cbExcel.TabIndex = 2;
            this.cbExcel.Text = "Excel";
            this.cbExcel.UseVisualStyleBackColor = true;
            this.cbExcel.CheckedChanged += new System.EventHandler(this.cbExcel_CheckedChanged);
            // 
            // frmExportSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(433, 564);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.dtpDODate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmExportSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export Setting";
            this.Load += new System.EventHandler(this.frmExportSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbPrintFile;
        private System.Windows.Forms.CheckBox cbOpenFile;
        private System.Windows.Forms.DateTimePicker dtpDODate;
        private System.Windows.Forms.CheckBox cbPrintPreview;
        private System.Windows.Forms.CheckBox cbAllInOne;
        private System.Windows.Forms.CheckBox cbSeparate;
        private Guna.UI.WinForms.GunaGradientButton btnApply;
        private Guna.UI.WinForms.GunaGradientButton btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbExcel;
        private System.Windows.Forms.CheckBox cbPDF;
    }
}