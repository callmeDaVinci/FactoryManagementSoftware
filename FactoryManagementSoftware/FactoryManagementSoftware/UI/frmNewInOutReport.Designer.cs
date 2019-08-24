namespace FactoryManagementSoftware.UI
{
    partial class frmNewInOutReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEndOrName = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblStartOrCat = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.dgvInOutReport = new System.Windows.Forms.DataGridView();
            this.cbPart = new System.Windows.Forms.CheckBox();
            this.cbMat = new System.Windows.Forms.CheckBox();
            this.cbOut = new System.Windows.Forms.CheckBox();
            this.cbIn = new System.Windows.Forms.CheckBox();
            this.lblNoData = new System.Windows.Forms.Label();
            this.cbDaily = new System.Windows.Forms.CheckBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.cbSearch = new System.Windows.Forms.CheckBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblInvalidSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInOutReport)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "dd-MM-yyyy";
            this.dtpEnd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(599, 52);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(156, 34);
            this.dtpEnd.TabIndex = 95;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // lblEndOrName
            // 
            this.lblEndOrName.AutoSize = true;
            this.lblEndOrName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndOrName.Location = new System.Drawing.Point(595, 26);
            this.lblEndOrName.Name = "lblEndOrName";
            this.lblEndOrName.Size = new System.Drawing.Size(44, 23);
            this.lblEndOrName.TabIndex = 94;
            this.lblEndOrName.Text = "END";
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "dd-MM-yyyy";
            this.dtpStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(414, 52);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(157, 34);
            this.dtpStart.TabIndex = 93;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // lblStartOrCat
            // 
            this.lblStartOrCat.AutoSize = true;
            this.lblStartOrCat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartOrCat.Location = new System.Drawing.Point(410, 26);
            this.lblStartOrCat.Name = "lblStartOrCat";
            this.lblStartOrCat.Size = new System.Drawing.Size(56, 23);
            this.lblStartOrCat.TabIndex = 92;
            this.lblStartOrCat.Text = "START";
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(779, 34);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(120, 52);
            this.btnCheck.TabIndex = 121;
            this.btnCheck.Text = "CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(34, 50);
            this.cmbType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(348, 36);
            this.cmbType.TabIndex = 120;
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInOutReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInOutReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInOutReport.GridColor = System.Drawing.SystemColors.Control;
            this.dgvInOutReport.Location = new System.Drawing.Point(34, 136);
            this.dgvInOutReport.Margin = new System.Windows.Forms.Padding(2);
            this.dgvInOutReport.Name = "dgvInOutReport";
            this.dgvInOutReport.ReadOnly = true;
            this.dgvInOutReport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvInOutReport.RowHeadersVisible = false;
            this.dgvInOutReport.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvInOutReport.RowTemplate.Height = 40;
            this.dgvInOutReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInOutReport.Size = new System.Drawing.Size(1522, 683);
            this.dgvInOutReport.TabIndex = 122;
            this.dgvInOutReport.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvInOutReport_CellFormatting);
            this.dgvInOutReport.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvInOutReport_SortCompare);
            // 
            // cbPart
            // 
            this.cbPart.AutoSize = true;
            this.cbPart.Checked = true;
            this.cbPart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPart.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPart.Location = new System.Drawing.Point(169, 22);
            this.cbPart.Name = "cbPart";
            this.cbPart.Size = new System.Drawing.Size(76, 27);
            this.cbPart.TabIndex = 124;
            this.cbPart.Text = "PART";
            this.cbPart.UseVisualStyleBackColor = true;
            this.cbPart.CheckedChanged += new System.EventHandler(this.cbPart_CheckedChanged);
            // 
            // cbMat
            // 
            this.cbMat.AutoSize = true;
            this.cbMat.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMat.Location = new System.Drawing.Point(34, 22);
            this.cbMat.Name = "cbMat";
            this.cbMat.Size = new System.Drawing.Size(120, 27);
            this.cbMat.TabIndex = 123;
            this.cbMat.Text = "MATERIAL";
            this.cbMat.UseVisualStyleBackColor = true;
            this.cbMat.CheckedChanged += new System.EventHandler(this.cbMat_CheckedChanged);
            // 
            // cbOut
            // 
            this.cbOut.AutoSize = true;
            this.cbOut.Checked = true;
            this.cbOut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOut.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOut.Location = new System.Drawing.Point(169, 93);
            this.cbOut.Name = "cbOut";
            this.cbOut.Size = new System.Drawing.Size(65, 27);
            this.cbOut.TabIndex = 126;
            this.cbOut.Text = "OUT";
            this.cbOut.UseVisualStyleBackColor = true;
            this.cbOut.CheckedChanged += new System.EventHandler(this.cbOut_CheckedChanged);
            // 
            // cbIn
            // 
            this.cbIn.AutoSize = true;
            this.cbIn.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIn.Location = new System.Drawing.Point(34, 93);
            this.cbIn.Name = "cbIn";
            this.cbIn.Size = new System.Drawing.Size(54, 27);
            this.cbIn.TabIndex = 125;
            this.cbIn.Text = "IN";
            this.cbIn.UseVisualStyleBackColor = true;
            this.cbIn.CheckedChanged += new System.EventHandler(this.cbIn_CheckedChanged);
            // 
            // lblNoData
            // 
            this.lblNoData.AutoSize = true;
            this.lblNoData.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoData.ForeColor = System.Drawing.Color.Red;
            this.lblNoData.Location = new System.Drawing.Point(931, 50);
            this.lblNoData.Name = "lblNoData";
            this.lblNoData.Size = new System.Drawing.Size(87, 23);
            this.lblNoData.TabIndex = 127;
            this.lblNoData.Text = "NO DATA!";
            this.lblNoData.Visible = false;
            // 
            // cbDaily
            // 
            this.cbDaily.AutoSize = true;
            this.cbDaily.Checked = true;
            this.cbDaily.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDaily.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDaily.Location = new System.Drawing.Point(288, 93);
            this.cbDaily.Name = "cbDaily";
            this.cbDaily.Size = new System.Drawing.Size(87, 27);
            this.cbDaily.TabIndex = 128;
            this.cbDaily.Text = "DAILY";
            this.cbDaily.UseVisualStyleBackColor = true;
            this.cbDaily.CheckedChanged += new System.EventHandler(this.cbDaily_CheckedChanged);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1436, 41);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(120, 52);
            this.btnExcel.TabIndex = 129;
            this.btnExcel.Text = "EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // cbSearch
            // 
            this.cbSearch.AutoSize = true;
            this.cbSearch.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSearch.Location = new System.Drawing.Point(260, 22);
            this.cbSearch.Name = "cbSearch";
            this.cbSearch.Size = new System.Drawing.Size(131, 27);
            this.cbSearch.TabIndex = 130;
            this.cbSearch.Text = "BY SEARCH";
            this.cbSearch.UseVisualStyleBackColor = true;
            this.cbSearch.CheckedChanged += new System.EventHandler(this.cbSearch_CheckedChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.LightGray;
            this.txtSearch.Location = new System.Drawing.Point(34, 48);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(348, 38);
            this.txtSearch.TabIndex = 131;
            this.txtSearch.Tag = "";
            this.txtSearch.Text = "SEARCH";
            this.txtSearch.Visible = false;
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // lblInvalidSearch
            // 
            this.lblInvalidSearch.AutoSize = true;
            this.lblInvalidSearch.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvalidSearch.ForeColor = System.Drawing.Color.Red;
            this.lblInvalidSearch.Location = new System.Drawing.Point(931, 52);
            this.lblInvalidSearch.Name = "lblInvalidSearch";
            this.lblInvalidSearch.Size = new System.Drawing.Size(122, 23);
            this.lblInvalidSearch.TabIndex = 132;
            this.lblInvalidSearch.Text = "INVALID ITEM!";
            this.lblInvalidSearch.Visible = false;
            // 
            // frmNewInOutReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.lblInvalidSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cbSearch);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.cbDaily);
            this.Controls.Add(this.lblNoData);
            this.Controls.Add(this.cbOut);
            this.Controls.Add(this.cbIn);
            this.Controls.Add(this.cbPart);
            this.Controls.Add(this.cbMat);
            this.Controls.Add(this.dgvInOutReport);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lblEndOrName);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblStartOrCat);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmNewInOutReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNewInOut";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmNewInOut_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInOutReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblEndOrName;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblStartOrCat;
        private System.Windows.Forms.Button btnCheck;
        public System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.DataGridView dgvInOutReport;
        private System.Windows.Forms.CheckBox cbPart;
        private System.Windows.Forms.CheckBox cbMat;
        private System.Windows.Forms.CheckBox cbOut;
        private System.Windows.Forms.CheckBox cbIn;
        private System.Windows.Forms.Label lblNoData;
        private System.Windows.Forms.CheckBox cbDaily;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.CheckBox cbSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblInvalidSearch;
    }
}