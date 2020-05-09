namespace FactoryManagementSoftware.UI
{
    partial class frmItemReport
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
            this.cbIncludeSubMat = new System.Windows.Forms.CheckBox();
            this.lblSubType = new System.Windows.Forms.Label();
            this.cmbSubType = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.btnCheck = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnExportAllToExcel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.cbGetItemReport = new System.Windows.Forms.CheckBox();
            this.cbImportItemData = new System.Windows.Forms.CheckBox();
            this.gbImportData = new System.Windows.Forms.GroupBox();
            this.btnUpLoad = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblTotalRowCount = new System.Windows.Forms.Label();
            this.btnUpdateData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.gbImportData.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbIncludeSubMat
            // 
            this.cbIncludeSubMat.AutoSize = true;
            this.cbIncludeSubMat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeSubMat.Location = new System.Drawing.Point(391, 112);
            this.cbIncludeSubMat.Name = "cbIncludeSubMat";
            this.cbIncludeSubMat.Size = new System.Drawing.Size(220, 27);
            this.cbIncludeSubMat.TabIndex = 121;
            this.cbIncludeSubMat.Text = "INCLUDE SUB MATERIAL";
            this.cbIncludeSubMat.UseVisualStyleBackColor = true;
            this.cbIncludeSubMat.Visible = false;
            this.cbIncludeSubMat.CheckedChanged += new System.EventHandler(this.cbIncludeSubMat_CheckedChanged);
            // 
            // lblSubType
            // 
            this.lblSubType.AutoSize = true;
            this.lblSubType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubType.Location = new System.Drawing.Point(387, 46);
            this.lblSubType.Name = "lblSubType";
            this.lblSubType.Size = new System.Drawing.Size(68, 19);
            this.lblSubType.TabIndex = 113;
            this.lblSubType.Text = "SUB TYPE";
            // 
            // cmbSubType
            // 
            this.cmbSubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubType.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbSubType.FormattingEnabled = true;
            this.cmbSubType.Location = new System.Drawing.Point(391, 69);
            this.cmbSubType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSubType.Name = "cmbSubType";
            this.cmbSubType.Size = new System.Drawing.Size(287, 36);
            this.cmbSubType.TabIndex = 112;
            this.cmbSubType.SelectedIndexChanged += new System.EventHandler(this.cmbSubType_SelectedIndexChanged);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(191, 69);
            this.cmbType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(189, 36);
            this.cmbType.TabIndex = 109;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(190, 45);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(39, 19);
            this.lblType.TabIndex = 110;
            this.lblType.Text = "TYPE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 19);
            this.label3.TabIndex = 120;
            this.label3.Text = "ITEM INFORMATION";
            // 
            // dgvItemList
            // 
            this.dgvItemList.AllowUserToAddRows = false;
            this.dgvItemList.AllowUserToDeleteRows = false;
            this.dgvItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvItemList.BackgroundColor = System.Drawing.Color.White;
            this.dgvItemList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvItemList.GridColor = System.Drawing.SystemColors.Control;
            this.dgvItemList.Location = new System.Drawing.Point(32, 175);
            this.dgvItemList.Margin = new System.Windows.Forms.Padding(2);
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.ReadOnly = true;
            this.dgvItemList.RowHeadersVisible = false;
            this.dgvItemList.RowTemplate.Height = 40;
            this.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemList.Size = new System.Drawing.Size(1522, 659);
            this.dgvItemList.TabIndex = 119;
            this.dgvItemList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellClick);
            this.dgvItemList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvItemList_CellFormatting);
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(696, 55);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(120, 50);
            this.btnCheck.TabIndex = 116;
            this.btnCheck.Text = "CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(32, 18);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1522, 23);
            this.progressBar1.TabIndex = 115;
            this.progressBar1.Visible = false;
            // 
            // btnExportAllToExcel
            // 
            this.btnExportAllToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAllToExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExportAllToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportAllToExcel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnExportAllToExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExportAllToExcel.Location = new System.Drawing.Point(1434, 55);
            this.btnExportAllToExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportAllToExcel.Name = "btnExportAllToExcel";
            this.btnExportAllToExcel.Size = new System.Drawing.Size(120, 50);
            this.btnExportAllToExcel.TabIndex = 111;
            this.btnExportAllToExcel.Text = "ALL";
            this.btnExportAllToExcel.UseVisualStyleBackColor = false;
            this.btnExportAllToExcel.Click += new System.EventHandler(this.btnExportAllToExcel_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(1303, 55);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 50);
            this.button1.TabIndex = 114;
            this.button1.Text = "EXCEL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            // 
            // cbGetItemReport
            // 
            this.cbGetItemReport.AutoSize = true;
            this.cbGetItemReport.Checked = true;
            this.cbGetItemReport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGetItemReport.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGetItemReport.Location = new System.Drawing.Point(32, 53);
            this.cbGetItemReport.Name = "cbGetItemReport";
            this.cbGetItemReport.Size = new System.Drawing.Size(142, 23);
            this.cbGetItemReport.TabIndex = 122;
            this.cbGetItemReport.Text = "GET ITEM REPORT";
            this.cbGetItemReport.UseVisualStyleBackColor = true;
            this.cbGetItemReport.CheckedChanged += new System.EventHandler(this.cbGetItemReport_CheckedChanged);
            // 
            // cbImportItemData
            // 
            this.cbImportItemData.AutoSize = true;
            this.cbImportItemData.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbImportItemData.Location = new System.Drawing.Point(32, 82);
            this.cbImportItemData.Name = "cbImportItemData";
            this.cbImportItemData.Size = new System.Drawing.Size(153, 23);
            this.cbImportItemData.TabIndex = 123;
            this.cbImportItemData.Text = "IMPORT ITEM DATA";
            this.cbImportItemData.UseVisualStyleBackColor = true;
            this.cbImportItemData.CheckedChanged += new System.EventHandler(this.cbImportItemData_CheckedChanged);
            // 
            // gbImportData
            // 
            this.gbImportData.Controls.Add(this.btnUpLoad);
            this.gbImportData.Controls.Add(this.btnBrowse);
            this.gbImportData.Controls.Add(this.txtPath);
            this.gbImportData.Location = new System.Drawing.Point(191, 46);
            this.gbImportData.Name = "gbImportData";
            this.gbImportData.Size = new System.Drawing.Size(928, 69);
            this.gbImportData.TabIndex = 124;
            this.gbImportData.TabStop = false;
            this.gbImportData.Text = "OPEN EXCEL";
            this.gbImportData.Visible = false;
            this.gbImportData.Enter += new System.EventHandler(this.gbImportData_Enter);
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnUpLoad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpLoad.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpLoad.ForeColor = System.Drawing.Color.White;
            this.btnUpLoad.Location = new System.Drawing.Point(805, 29);
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.Size = new System.Drawing.Size(117, 30);
            this.btnUpLoad.TabIndex = 1;
            this.btnUpLoad.Text = "UPLOAD";
            this.btnUpLoad.UseVisualStyleBackColor = false;
            this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.White;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowse.Location = new System.Drawing.Point(697, 29);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(102, 30);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "BROWSE";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPath.Location = new System.Drawing.Point(6, 29);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(672, 30);
            this.txtPath.TabIndex = 2;
            // 
            // lblTotalRowCount
            // 
            this.lblTotalRowCount.AutoSize = true;
            this.lblTotalRowCount.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRowCount.Location = new System.Drawing.Point(201, 121);
            this.lblTotalRowCount.Name = "lblTotalRowCount";
            this.lblTotalRowCount.Size = new System.Drawing.Size(40, 46);
            this.lblTotalRowCount.TabIndex = 42;
            this.lblTotalRowCount.Text = "0";
            // 
            // btnUpdateData
            // 
            this.btnUpdateData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnUpdateData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateData.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateData.ForeColor = System.Drawing.Color.Black;
            this.btnUpdateData.Location = new System.Drawing.Point(1434, 137);
            this.btnUpdateData.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateData.Name = "btnUpdateData";
            this.btnUpdateData.Size = new System.Drawing.Size(120, 30);
            this.btnUpdateData.TabIndex = 125;
            this.btnUpdateData.Text = "UPDATE DATA";
            this.btnUpdateData.UseVisualStyleBackColor = false;
            this.btnUpdateData.Visible = false;
            this.btnUpdateData.Click += new System.EventHandler(this.btnUpdateData_Click);
            // 
            // frmItemReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.btnUpdateData);
            this.Controls.Add(this.lblTotalRowCount);
            this.Controls.Add(this.gbImportData);
            this.Controls.Add(this.cbImportItemData);
            this.Controls.Add(this.cbGetItemReport);
            this.Controls.Add(this.cbIncludeSubMat);
            this.Controls.Add(this.lblSubType);
            this.Controls.Add(this.cmbSubType);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvItemList);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnExportAllToExcel);
            this.Controls.Add(this.button1);
            this.Name = "frmItemReport";
            this.Text = "frmItemReport";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmItemReport_FormClosed);
            this.Load += new System.EventHandler(this.frmItemReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.gbImportData.ResumeLayout(false);
            this.gbImportData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbIncludeSubMat;
        private System.Windows.Forms.Label lblSubType;
        public System.Windows.Forms.ComboBox cmbSubType;
        public System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvItemList;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnExportAllToExcel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.CheckBox cbImportItemData;
        private System.Windows.Forms.CheckBox cbGetItemReport;
        private System.Windows.Forms.GroupBox gbImportData;
        private System.Windows.Forms.Button btnUpLoad;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblTotalRowCount;
        private System.Windows.Forms.Button btnUpdateData;
    }
}