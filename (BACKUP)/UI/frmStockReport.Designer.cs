namespace FactoryManagementSoftware.UI
{
    partial class frmStockReport
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
            this.components = new System.ComponentModel.Container();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.btnExportAllToExcel = new System.Windows.Forms.Button();
            this.lblSubType = new System.Windows.Forms.Label();
            this.cmbSubType = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.btnCheck = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dgvNewStock = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cbIncludeSubMat = new System.Windows.Forms.CheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblUpdatedTime = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbShowDuplicateData = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(23, 37);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(39, 19);
            this.lblType.TabIndex = 34;
            this.lblType.Text = "TYPE";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbType.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.ItemHeight = 23;
            this.cmbType.Location = new System.Drawing.Point(27, 58);
            this.cmbType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(189, 31);
            this.cmbType.TabIndex = 33;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // btnExportAllToExcel
            // 
            this.btnExportAllToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAllToExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExportAllToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportAllToExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportAllToExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExportAllToExcel.Location = new System.Drawing.Point(1429, 58);
            this.btnExportAllToExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportAllToExcel.Name = "btnExportAllToExcel";
            this.btnExportAllToExcel.Size = new System.Drawing.Size(124, 36);
            this.btnExportAllToExcel.TabIndex = 74;
            this.btnExportAllToExcel.Text = "EXCEL ALL";
            this.btnExportAllToExcel.UseVisualStyleBackColor = false;
            this.btnExportAllToExcel.Click += new System.EventHandler(this.btnExportAllToExcel_Click);
            // 
            // lblSubType
            // 
            this.lblSubType.AutoSize = true;
            this.lblSubType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubType.Location = new System.Drawing.Point(223, 37);
            this.lblSubType.Name = "lblSubType";
            this.lblSubType.Size = new System.Drawing.Size(68, 19);
            this.lblSubType.TabIndex = 76;
            this.lblSubType.Text = "SUB TYPE";
            // 
            // cmbSubType
            // 
            this.cmbSubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSubType.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbSubType.FormattingEnabled = true;
            this.cmbSubType.Location = new System.Drawing.Point(227, 58);
            this.cmbSubType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSubType.Name = "cmbSubType";
            this.cmbSubType.Size = new System.Drawing.Size(287, 31);
            this.cmbSubType.TabIndex = 75;
            this.cmbSubType.SelectedIndexChanged += new System.EventHandler(this.cmbSubType_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(1301, 58);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 36);
            this.button1.TabIndex = 77;
            this.button1.Text = "EXCEL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(681, 52);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(124, 37);
            this.btnCheck.TabIndex = 101;
            this.btnCheck.Text = "SEARCH";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(525, 59);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(151, 30);
            this.dtpEndDate.TabIndex = 102;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(521, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 19);
            this.label1.TabIndex = 103;
            this.label1.Text = "END DATE";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(27, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1526, 23);
            this.progressBar1.TabIndex = 79;
            this.progressBar1.Visible = false;
            // 
            // dgvNewStock
            // 
            this.dgvNewStock.AllowUserToAddRows = false;
            this.dgvNewStock.AllowUserToDeleteRows = false;
            this.dgvNewStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNewStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvNewStock.BackgroundColor = System.Drawing.Color.White;
            this.dgvNewStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNewStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNewStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvNewStock.GridColor = System.Drawing.SystemColors.Control;
            this.dgvNewStock.Location = new System.Drawing.Point(27, 162);
            this.dgvNewStock.Margin = new System.Windows.Forms.Padding(2);
            this.dgvNewStock.Name = "dgvNewStock";
            this.dgvNewStock.ReadOnly = true;
            this.dgvNewStock.RowHeadersVisible = false;
            this.dgvNewStock.RowTemplate.Height = 40;
            this.dgvNewStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNewStock.Size = new System.Drawing.Size(1522, 659);
            this.dgvNewStock.TabIndex = 104;
            this.dgvNewStock.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNewStock_CellDoubleClick);
            this.dgvNewStock.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvNewStock_CellFormatting);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 19);
            this.label3.TabIndex = 105;
            this.label3.Text = "STOCK REPORT";
            // 
            // cbIncludeSubMat
            // 
            this.cbIncludeSubMat.AutoSize = true;
            this.cbIncludeSubMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeSubMat.Location = new System.Drawing.Point(227, 96);
            this.cbIncludeSubMat.Name = "cbIncludeSubMat";
            this.cbIncludeSubMat.Size = new System.Drawing.Size(184, 23);
            this.cbIncludeSubMat.TabIndex = 106;
            this.cbIncludeSubMat.Text = "INCLUDE SUB MATERIAL";
            this.cbIncludeSubMat.UseVisualStyleBackColor = true;
            this.cbIncludeSubMat.Visible = false;
            this.cbIncludeSubMat.CheckedChanged += new System.EventHandler(this.cbIncludeSubMat_CheckedChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(810, 52);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(149, 19);
            this.lblInfo.TabIndex = 108;
            this.lblInfo.Text = "DATA AT AND BEFORE:";
            // 
            // lblUpdatedTime
            // 
            this.lblUpdatedTime.AutoSize = true;
            this.lblUpdatedTime.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdatedTime.Location = new System.Drawing.Point(810, 71);
            this.lblUpdatedTime.Name = "lblUpdatedTime";
            this.lblUpdatedTime.Size = new System.Drawing.Size(179, 19);
            this.lblUpdatedTime.TabIndex = 107;
            this.lblUpdatedTime.Text = "SHOW DATA FOR THE PAST";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // cbShowDuplicateData
            // 
            this.cbShowDuplicateData.AutoSize = true;
            this.cbShowDuplicateData.Checked = true;
            this.cbShowDuplicateData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowDuplicateData.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowDuplicateData.Location = new System.Drawing.Point(27, 96);
            this.cbShowDuplicateData.Name = "cbShowDuplicateData";
            this.cbShowDuplicateData.Size = new System.Drawing.Size(181, 23);
            this.cbShowDuplicateData.TabIndex = 109;
            this.cbShowDuplicateData.Text = "SHOW DUPLICATE ITEM";
            this.cbShowDuplicateData.UseVisualStyleBackColor = true;
            this.cbShowDuplicateData.CheckedChanged += new System.EventHandler(this.cbShowDuplicateData_CheckedChanged);
            // 
            // frmStockReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.cbShowDuplicateData);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblUpdatedTime);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbIncludeSubMat);
            this.Controls.Add(this.lblSubType);
            this.Controls.Add(this.cmbSubType);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvNewStock);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnExportAllToExcel);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmStockReport";
            this.Text = "Stock Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmStockReport_FormClosed);
            this.Load += new System.EventHandler(this.frmStockReport_Load);
            this.Click += new System.EventHandler(this.frmStockReport_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblType;
        public System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button btnExportAllToExcel;
        private System.Windows.Forms.Label lblSubType;
        public System.Windows.Forms.ComboBox cmbSubType;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView dgvNewStock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbIncludeSubMat;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblUpdatedTime;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.CheckBox cbShowDuplicateData;
    }
}