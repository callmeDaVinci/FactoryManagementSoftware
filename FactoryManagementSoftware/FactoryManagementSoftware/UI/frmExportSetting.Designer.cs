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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbPrintFile = new System.Windows.Forms.CheckBox();
            this.cbOpenFile = new System.Windows.Forms.CheckBox();
            this.dtpDODate = new System.Windows.Forms.DateTimePicker();
            this.cbPrintPreview = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "D/O DATE";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(44, 254);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 1, 4, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(245, 36);
            this.btnCancel.TabIndex = 174;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancelDOMode_Click);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location = new System.Drawing.Point(44, 213);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(245, 36);
            this.btnApply.TabIndex = 173;
            this.btnApply.Text = "APPLY";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbPrintPreview);
            this.groupBox1.Controls.Add(this.cbPrintFile);
            this.groupBox1.Controls.Add(this.cbOpenFile);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(44, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 110);
            this.groupBox1.TabIndex = 175;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AFTER EXPORT";
            // 
            // cbPrintFile
            // 
            this.cbPrintFile.AutoSize = true;
            this.cbPrintFile.Checked = true;
            this.cbPrintFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPrintFile.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrintFile.Location = new System.Drawing.Point(15, 66);
            this.cbPrintFile.Name = "cbPrintFile";
            this.cbPrintFile.Size = new System.Drawing.Size(68, 23);
            this.cbPrintFile.TabIndex = 1;
            this.cbPrintFile.Text = "PRINT";
            this.cbPrintFile.UseVisualStyleBackColor = true;
            this.cbPrintFile.CheckedChanged += new System.EventHandler(this.cbPrintFile_CheckedChanged);
            // 
            // cbOpenFile
            // 
            this.cbOpenFile.AutoSize = true;
            this.cbOpenFile.Checked = true;
            this.cbOpenFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOpenFile.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOpenFile.Location = new System.Drawing.Point(15, 28);
            this.cbOpenFile.Name = "cbOpenFile";
            this.cbOpenFile.Size = new System.Drawing.Size(96, 23);
            this.cbOpenFile.TabIndex = 0;
            this.cbOpenFile.Text = "OPEN FILE";
            this.cbOpenFile.UseVisualStyleBackColor = true;
            // 
            // dtpDODate
            // 
            this.dtpDODate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDODate.Location = new System.Drawing.Point(44, 37);
            this.dtpDODate.Name = "dtpDODate";
            this.dtpDODate.RightToLeftLayout = true;
            this.dtpDODate.Size = new System.Drawing.Size(245, 25);
            this.dtpDODate.TabIndex = 176;
            // 
            // cbPrintPreview
            // 
            this.cbPrintPreview.AutoSize = true;
            this.cbPrintPreview.Checked = true;
            this.cbPrintPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPrintPreview.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrintPreview.Location = new System.Drawing.Point(101, 66);
            this.cbPrintPreview.Name = "cbPrintPreview";
            this.cbPrintPreview.Size = new System.Drawing.Size(128, 23);
            this.cbPrintPreview.TabIndex = 2;
            this.cbPrintPreview.Text = "PRINT PREVIEW";
            this.cbPrintPreview.UseVisualStyleBackColor = true;
            this.cbPrintPreview.CheckedChanged += new System.EventHandler(this.cbPrintPreview_CheckedChanged);
            // 
            // frmExportSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(340, 324);
            this.Controls.Add(this.dtpDODate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmExportSetting";
            this.Text = "Export Setting";
            this.Load += new System.EventHandler(this.frmExportSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbPrintFile;
        private System.Windows.Forms.CheckBox cbOpenFile;
        private System.Windows.Forms.DateTimePicker dtpDODate;
        private System.Windows.Forms.CheckBox cbPrintPreview;
    }
}