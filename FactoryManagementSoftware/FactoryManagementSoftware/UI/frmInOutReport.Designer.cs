namespace FactoryManagementSoftware.UI
{
    partial class frmInOutReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExcel = new System.Windows.Forms.Button();
            this.lblSubType = new System.Windows.Forms.Label();
            this.cmbSubType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.dgvInOutReport = new System.Windows.Forms.DataGridView();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEndOrName = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblStartOrCat = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbBy = new System.Windows.Forms.ComboBox();
            this.cmbName = new System.Windows.Forms.ComboBox();
            this.cmbCat = new System.Windows.Forms.ComboBox();
            this.cmbCode = new System.Windows.Forms.ComboBox();
            this.lblCode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInOutReport)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1434, 40);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(120, 52);
            this.btnExcel.TabIndex = 84;
            this.btnExcel.Text = "EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // lblSubType
            // 
            this.lblSubType.AutoSize = true;
            this.lblSubType.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubType.Location = new System.Drawing.Point(290, 112);
            this.lblSubType.Name = "lblSubType";
            this.lblSubType.Size = new System.Drawing.Size(83, 23);
            this.lblSubType.TabIndex = 83;
            this.lblSubType.Text = "SUB TYPE";
            // 
            // cmbSubType
            // 
            this.cmbSubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubType.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbSubType.FormattingEnabled = true;
            this.cmbSubType.Location = new System.Drawing.Point(294, 139);
            this.cmbSubType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSubType.Name = "cmbSubType";
            this.cmbSubType.Size = new System.Drawing.Size(241, 36);
            this.cmbSubType.TabIndex = 82;
            this.cmbSubType.SelectedIndexChanged += new System.EventHandler(this.cmbSubType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(28, 112);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(47, 23);
            this.lblType.TabIndex = 80;
            this.lblType.Text = "TYPE";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(32, 139);
            this.cmbType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(241, 36);
            this.cmbType.TabIndex = 79;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // dgvInOutReport
            // 
            this.dgvInOutReport.AllowUserToAddRows = false;
            this.dgvInOutReport.AllowUserToDeleteRows = false;
            this.dgvInOutReport.AllowUserToResizeRows = false;
            this.dgvInOutReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInOutReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvInOutReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInOutReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInOutReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInOutReport.GridColor = System.Drawing.SystemColors.Control;
            this.dgvInOutReport.Location = new System.Drawing.Point(32, 196);
            this.dgvInOutReport.Margin = new System.Windows.Forms.Padding(2);
            this.dgvInOutReport.Name = "dgvInOutReport";
            this.dgvInOutReport.ReadOnly = true;
            this.dgvInOutReport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvInOutReport.RowHeadersVisible = false;
            this.dgvInOutReport.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvInOutReport.RowTemplate.Height = 40;
            this.dgvInOutReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInOutReport.Size = new System.Drawing.Size(1522, 629);
            this.dgvInOutReport.TabIndex = 78;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "dd-MM-yyyy";
            this.dtpEnd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(556, 58);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(241, 34);
            this.dtpEnd.TabIndex = 91;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // lblEndOrName
            // 
            this.lblEndOrName.AutoSize = true;
            this.lblEndOrName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndOrName.Location = new System.Drawing.Point(552, 29);
            this.lblEndOrName.Name = "lblEndOrName";
            this.lblEndOrName.Size = new System.Drawing.Size(44, 23);
            this.lblEndOrName.TabIndex = 90;
            this.lblEndOrName.Text = "END";
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "dd-MM-yyyy";
            this.dtpStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(294, 58);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(241, 34);
            this.dtpStart.TabIndex = 89;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // lblStartOrCat
            // 
            this.lblStartOrCat.AutoSize = true;
            this.lblStartOrCat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartOrCat.Location = new System.Drawing.Point(290, 29);
            this.lblStartOrCat.Name = "lblStartOrCat";
            this.lblStartOrCat.Size = new System.Drawing.Size(56, 23);
            this.lblStartOrCat.TabIndex = 88;
            this.lblStartOrCat.Text = "START";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 23);
            this.label4.TabIndex = 93;
            this.label4.Text = "BY";
            // 
            // cmbBy
            // 
            this.cmbBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBy.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbBy.FormattingEnabled = true;
            this.cmbBy.Location = new System.Drawing.Point(32, 56);
            this.cmbBy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbBy.Name = "cmbBy";
            this.cmbBy.Size = new System.Drawing.Size(241, 36);
            this.cmbBy.TabIndex = 92;
            this.cmbBy.SelectedIndexChanged += new System.EventHandler(this.cmbBy_SelectedIndexChanged);
            // 
            // cmbName
            // 
            this.cmbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new System.Drawing.Point(556, 56);
            this.cmbName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(241, 36);
            this.cmbName.TabIndex = 95;
            this.cmbName.SelectedIndexChanged += new System.EventHandler(this.cmbName_SelectedIndexChanged);
            // 
            // cmbCat
            // 
            this.cmbCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCat.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbCat.FormattingEnabled = true;
            this.cmbCat.Location = new System.Drawing.Point(294, 56);
            this.cmbCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbCat.Name = "cmbCat";
            this.cmbCat.Size = new System.Drawing.Size(241, 36);
            this.cmbCat.TabIndex = 94;
            this.cmbCat.SelectedIndexChanged += new System.EventHandler(this.cmbCat_SelectedIndexChanged);
            // 
            // cmbCode
            // 
            this.cmbCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCode.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbCode.FormattingEnabled = true;
            this.cmbCode.Location = new System.Drawing.Point(820, 56);
            this.cmbCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbCode.Name = "cmbCode";
            this.cmbCode.Size = new System.Drawing.Size(241, 36);
            this.cmbCode.TabIndex = 96;
            this.cmbCode.SelectedIndexChanged += new System.EventHandler(this.cmbCode_SelectedIndexChanged);
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(816, 29);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(55, 23);
            this.lblCode.TabIndex = 97;
            this.lblCode.Text = "CODE";
            // 
            // frmInOutReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.cmbCode);
            this.Controls.Add(this.cmbName);
            this.Controls.Add(this.cmbCat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbBy);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lblEndOrName);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblStartOrCat);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.lblSubType);
            this.Controls.Add(this.cmbSubType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.dgvInOutReport);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmInOutReport";
            this.Text = "frmInOutReport";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInOutReport_FormClosed);
            this.Click += new System.EventHandler(this.frmInOutReport_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInOutReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Label lblSubType;
        public System.Windows.Forms.ComboBox cmbSubType;
        private System.Windows.Forms.Label lblType;
        public System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.DataGridView dgvInOutReport;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblEndOrName;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblStartOrCat;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cmbBy;
        public System.Windows.Forms.ComboBox cmbName;
        public System.Windows.Forms.ComboBox cmbCat;
        public System.Windows.Forms.ComboBox cmbCode;
        private System.Windows.Forms.Label lblCode;
    }
}