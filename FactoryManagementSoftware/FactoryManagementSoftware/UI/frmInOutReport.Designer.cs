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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExcel = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.dgvInOutReport = new System.Windows.Forms.DataGridView();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEndOrName = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblStartOrCat = new System.Windows.Forms.Label();
            this.cbDO = new System.Windows.Forms.CheckBox();
            this.cbDaily = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cbMat = new System.Windows.Forms.CheckBox();
            this.cbPart = new System.Windows.Forms.CheckBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSortByItem = new System.Windows.Forms.CheckBox();
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
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(401, 56);
            this.cmbType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(341, 36);
            this.cmbType.TabIndex = 82;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInOutReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvInOutReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInOutReport.GridColor = System.Drawing.SystemColors.Control;
            this.dgvInOutReport.Location = new System.Drawing.Point(32, 178);
            this.dgvInOutReport.Margin = new System.Windows.Forms.Padding(2);
            this.dgvInOutReport.Name = "dgvInOutReport";
            this.dgvInOutReport.ReadOnly = true;
            this.dgvInOutReport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvInOutReport.RowHeadersVisible = false;
            this.dgvInOutReport.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvInOutReport.RowTemplate.Height = 40;
            this.dgvInOutReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInOutReport.Size = new System.Drawing.Size(1522, 630);
            this.dgvInOutReport.TabIndex = 78;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "dd-MM-yyyy";
            this.dtpEnd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(217, 58);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(156, 34);
            this.dtpEnd.TabIndex = 91;
            // 
            // lblEndOrName
            // 
            this.lblEndOrName.AutoSize = true;
            this.lblEndOrName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndOrName.Location = new System.Drawing.Point(213, 32);
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
            this.dtpStart.Location = new System.Drawing.Point(32, 58);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(157, 34);
            this.dtpStart.TabIndex = 89;
            // 
            // lblStartOrCat
            // 
            this.lblStartOrCat.AutoSize = true;
            this.lblStartOrCat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartOrCat.Location = new System.Drawing.Point(28, 32);
            this.lblStartOrCat.Name = "lblStartOrCat";
            this.lblStartOrCat.Size = new System.Drawing.Size(56, 23);
            this.lblStartOrCat.TabIndex = 88;
            this.lblStartOrCat.Text = "START";
            // 
            // cbDO
            // 
            this.cbDO.AutoSize = true;
            this.cbDO.Checked = true;
            this.cbDO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDO.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDO.Location = new System.Drawing.Point(32, 98);
            this.cbDO.Name = "cbDO";
            this.cbDO.Size = new System.Drawing.Size(241, 27);
            this.cbDO.TabIndex = 113;
            this.cbDO.Text = "DELIVERY ORDER ONLY";
            this.cbDO.UseVisualStyleBackColor = true;
            // 
            // cbDaily
            // 
            this.cbDaily.AutoSize = true;
            this.cbDaily.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDaily.Location = new System.Drawing.Point(286, 98);
            this.cbDaily.Name = "cbDaily";
            this.cbDaily.Size = new System.Drawing.Size(87, 27);
            this.cbDaily.TabIndex = 114;
            this.cbDaily.Text = "DAILY";
            this.cbDaily.UseVisualStyleBackColor = true;
            this.cbDaily.CheckedChanged += new System.EventHandler(this.cbDaily_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1462, 143);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 31);
            this.btnSearch.TabIndex = 116;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(1234, 143);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(224, 30);
            this.txtSearch.TabIndex = 115;
            // 
            // cbMat
            // 
            this.cbMat.AutoSize = true;
            this.cbMat.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMat.Location = new System.Drawing.Point(401, 25);
            this.cbMat.Name = "cbMat";
            this.cbMat.Size = new System.Drawing.Size(120, 27);
            this.cbMat.TabIndex = 117;
            this.cbMat.Text = "MATERIAL";
            this.cbMat.UseVisualStyleBackColor = true;
            this.cbMat.CheckedChanged += new System.EventHandler(this.cbMat_CheckedChanged);
            // 
            // cbPart
            // 
            this.cbPart.AutoSize = true;
            this.cbPart.Checked = true;
            this.cbPart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPart.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPart.Location = new System.Drawing.Point(536, 25);
            this.cbPart.Name = "cbPart";
            this.cbPart.Size = new System.Drawing.Size(76, 27);
            this.cbPart.TabIndex = 118;
            this.cbPart.Text = "PART";
            this.cbPart.UseVisualStyleBackColor = true;
            this.cbPart.CheckedChanged += new System.EventHandler(this.cbPart_CheckedChanged);
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(747, 48);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(132, 45);
            this.btnCheck.TabIndex = 119;
            this.btnCheck.Text = "CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 23);
            this.label1.TabIndex = 120;
            this.label1.Text = "IN OUT REPORT";
            // 
            // cbSortByItem
            // 
            this.cbSortByItem.AutoSize = true;
            this.cbSortByItem.Checked = true;
            this.cbSortByItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSortByItem.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortByItem.Location = new System.Drawing.Point(401, 99);
            this.cbSortByItem.Name = "cbSortByItem";
            this.cbSortByItem.Size = new System.Drawing.Size(164, 27);
            this.cbSortByItem.TabIndex = 121;
            this.cbSortByItem.Text = "SORT BY ITEM";
            this.cbSortByItem.UseVisualStyleBackColor = true;
            // 
            // frmInOutReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.cbSortByItem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.cbPart);
            this.Controls.Add(this.cbMat);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cbDaily);
            this.Controls.Add(this.cbDO);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lblEndOrName);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblStartOrCat);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.dgvInOutReport);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmInOutReport";
            this.Text = "frmInOutReport";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInOutReport_FormClosed);
            this.Load += new System.EventHandler(this.frmInOutReport_Load);
            this.Click += new System.EventHandler(this.frmInOutReport_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInOutReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExcel;
        public System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.DataGridView dgvInOutReport;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblEndOrName;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblStartOrCat;
        private System.Windows.Forms.CheckBox cbDO;
        private System.Windows.Forms.CheckBox cbDaily;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox cbMat;
        private System.Windows.Forms.CheckBox cbPart;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSortByItem;
    }
}