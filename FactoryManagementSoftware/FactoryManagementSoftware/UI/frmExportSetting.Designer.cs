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
            this.lblDODate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbPrintPreview = new System.Windows.Forms.CheckBox();
            this.cbPrintFile = new System.Windows.Forms.CheckBox();
            this.cbOpenFile = new System.Windows.Forms.CheckBox();
            this.dtpDODate = new System.Windows.Forms.DateTimePicker();
            this.cbSaveInSingleFIle = new System.Windows.Forms.CheckBox();
            this.cbSplitByDocumentNo = new System.Windows.Forms.CheckBox();
            this.btnApply = new Guna.UI.WinForms.GunaGradientButton();
            this.btnCancel = new Guna.UI.WinForms.GunaGradientButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbPDF = new System.Windows.Forms.CheckBox();
            this.cbExcel = new System.Windows.Forms.CheckBox();
            this.tlpExportSettings = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tlpExportSettings.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDODate
            // 
            this.lblDODate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDODate.AutoSize = true;
            this.lblDODate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblDODate.Location = new System.Drawing.Point(3, 11);
            this.lblDODate.Name = "lblDODate";
            this.lblDODate.Size = new System.Drawing.Size(68, 19);
            this.lblDODate.TabIndex = 0;
            this.lblDODate.Text = "D/O Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbPrintPreview);
            this.groupBox1.Controls.Add(this.cbPrintFile);
            this.groupBox1.Controls.Add(this.cbOpenFile);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 279);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 140);
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
            this.dtpDODate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDODate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDODate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDODate.Location = new System.Drawing.Point(3, 33);
            this.dtpDODate.Name = "dtpDODate";
            this.dtpDODate.RightToLeftLayout = true;
            this.dtpDODate.Size = new System.Drawing.Size(368, 30);
            this.dtpDODate.TabIndex = 176;
            this.dtpDODate.ValueChanged += new System.EventHandler(this.dtpDODate_ValueChanged);
            // 
            // cbSaveInSingleFIle
            // 
            this.cbSaveInSingleFIle.AutoSize = true;
            this.cbSaveInSingleFIle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSaveInSingleFIle.Location = new System.Drawing.Point(13, 136);
            this.cbSaveInSingleFIle.Name = "cbSaveInSingleFIle";
            this.cbSaveInSingleFIle.Size = new System.Drawing.Size(139, 23);
            this.cbSaveInSingleFIle.TabIndex = 177;
            this.cbSaveInSingleFIle.Text = "Save in Single FIle";
            this.cbSaveInSingleFIle.UseVisualStyleBackColor = true;
            this.cbSaveInSingleFIle.CheckedChanged += new System.EventHandler(this.cbAllInOne_CheckedChanged);
            // 
            // cbSplitByDocumentNo
            // 
            this.cbSplitByDocumentNo.AutoSize = true;
            this.cbSplitByDocumentNo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSplitByDocumentNo.Location = new System.Drawing.Point(13, 107);
            this.cbSplitByDocumentNo.Name = "cbSplitByDocumentNo";
            this.cbSplitByDocumentNo.Size = new System.Drawing.Size(169, 23);
            this.cbSplitByDocumentNo.TabIndex = 178;
            this.cbSplitByDocumentNo.Text = "Split By Document No.";
            this.cbSplitByDocumentNo.UseVisualStyleBackColor = true;
            this.cbSplitByDocumentNo.CheckedChanged += new System.EventHandler(this.cbSeparate_CheckedChanged);
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
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApply.FocusedColor = System.Drawing.Color.Empty;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Image = null;
            this.btnApply.ImageSize = new System.Drawing.Size(20, 20);
            this.btnApply.Location = new System.Drawing.Point(5, 429);
            this.btnApply.Margin = new System.Windows.Forms.Padding(5);
            this.btnApply.Name = "btnApply";
            this.btnApply.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnApply.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnApply.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnApply.OnHoverForeColor = System.Drawing.Color.White;
            this.btnApply.OnHoverImage = null;
            this.btnApply.OnPressedColor = System.Drawing.Color.Black;
            this.btnApply.Radius = 2;
            this.btnApply.Size = new System.Drawing.Size(364, 40);
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
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.FocusedColor = System.Drawing.Color.Empty;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnCancel.Image = null;
            this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnCancel.Location = new System.Drawing.Point(5, 479);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnCancel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnCancel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnCancel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnCancel.OnHoverImage = null;
            this.btnCancel.OnPressedColor = System.Drawing.Color.Black;
            this.btnCancel.Radius = 2;
            this.btnCancel.Size = new System.Drawing.Size(364, 40);
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
            this.groupBox2.Controls.Add(this.cbSplitByDocumentNo);
            this.groupBox2.Controls.Add(this.cbSaveInSingleFIle);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 85);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 184);
            this.groupBox2.TabIndex = 232;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FILE TYPE";
            // 
            // cbPDF
            // 
            this.cbPDF.AutoSize = true;
            this.cbPDF.Checked = true;
            this.cbPDF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPDF.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbPDF.Location = new System.Drawing.Point(13, 58);
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
            // tlpExportSettings
            // 
            this.tlpExportSettings.ColumnCount = 1;
            this.tlpExportSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpExportSettings.Controls.Add(this.lblDODate, 0, 0);
            this.tlpExportSettings.Controls.Add(this.btnCancel, 0, 5);
            this.tlpExportSettings.Controls.Add(this.groupBox2, 0, 2);
            this.tlpExportSettings.Controls.Add(this.btnApply, 0, 4);
            this.tlpExportSettings.Controls.Add(this.dtpDODate, 0, 1);
            this.tlpExportSettings.Controls.Add(this.groupBox1, 0, 3);
            this.tlpExportSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpExportSettings.Location = new System.Drawing.Point(20, 20);
            this.tlpExportSettings.Margin = new System.Windows.Forms.Padding(20);
            this.tlpExportSettings.Name = "tlpExportSettings";
            this.tlpExportSettings.RowCount = 6;
            this.tlpExportSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpExportSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpExportSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpExportSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpExportSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpExportSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpExportSettings.Size = new System.Drawing.Size(374, 524);
            this.tlpExportSettings.TabIndex = 233;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tlpExportSettings, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(414, 564);
            this.tableLayoutPanel2.TabIndex = 234;
            // 
            // frmExportSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(414, 564);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmExportSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export Settings";
            this.Load += new System.EventHandler(this.frmExportSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tlpExportSettings.ResumeLayout(false);
            this.tlpExportSettings.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDODate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbPrintFile;
        private System.Windows.Forms.CheckBox cbOpenFile;
        private System.Windows.Forms.DateTimePicker dtpDODate;
        private System.Windows.Forms.CheckBox cbPrintPreview;
        private System.Windows.Forms.CheckBox cbSaveInSingleFIle;
        private System.Windows.Forms.CheckBox cbSplitByDocumentNo;
        private Guna.UI.WinForms.GunaGradientButton btnApply;
        private Guna.UI.WinForms.GunaGradientButton btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbExcel;
        private System.Windows.Forms.CheckBox cbPDF;
        private System.Windows.Forms.TableLayoutPanel tlpExportSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}