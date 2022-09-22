namespace FactoryManagementSoftware.UI
{
    partial class frmMaterialUsedReport_NEW
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpForecastReport = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSummary = new System.Windows.Forms.CheckBox();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblZeroCostOnly = new System.Windows.Forms.Label();
            this.cbZeroCostOnly = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvMatUsedReport = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLastUpdated = new System.Windows.Forms.Label();
            this.lblUpdatedTime = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.tlpFilter = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbStockType = new System.Windows.Forms.CheckBox();
            this.cbForecastDeductStock = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbMonthTo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbYearTo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbMonthFrom = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbYearFrom = new System.Windows.Forms.ComboBox();
            this.gbMonthYear = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurrentMonth = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.btnFilterApply = new System.Windows.Forms.Button();
            this.gbItemType = new System.Windows.Forms.GroupBox();
            this.cbSubMat = new System.Windows.Forms.CheckBox();
            this.cbPigment = new System.Windows.Forms.CheckBox();
            this.cbMasterBatch = new System.Windows.Forms.CheckBox();
            this.cbRawMat = new System.Windows.Forms.CheckBox();
            this.gbDatePeriod = new System.Windows.Forms.GroupBox();
            this.lblChangeDate = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tlpForecastReport.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatUsedReport)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.tlpFilter.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbMonthYear.SuspendLayout();
            this.gbItemType.SuspendLayout();
            this.gbDatePeriod.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpForecastReport
            // 
            this.tlpForecastReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpForecastReport.ColumnCount = 1;
            this.tlpForecastReport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForecastReport.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpForecastReport.Controls.Add(this.dgvMatUsedReport, 0, 3);
            this.tlpForecastReport.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tlpForecastReport.Controls.Add(this.gbFilter, 0, 1);
            this.tlpForecastReport.Location = new System.Drawing.Point(16, 12);
            this.tlpForecastReport.Name = "tlpForecastReport";
            this.tlpForecastReport.RowCount = 4;
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tlpForecastReport.Size = new System.Drawing.Size(1550, 829);
            this.tlpForecastReport.TabIndex = 165;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 222F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExcel, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSearch, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1544, 76);
            this.tableLayoutPanel2.TabIndex = 160;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.cmbReportType, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(353, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(216, 70);
            this.tableLayoutPanel6.TabIndex = 166;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.85185F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.14815F));
            this.tableLayoutPanel7.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cbSummary, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(216, 35);
            this.tableLayoutPanel7.TabIndex = 166;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 167;
            this.label1.Text = "REPORT TYPE";
            // 
            // cbSummary
            // 
            this.cbSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSummary.AutoSize = true;
            this.cbSummary.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.cbSummary.Location = new System.Drawing.Point(130, 14);
            this.cbSummary.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.cbSummary.Name = "cbSummary";
            this.cbSummary.Size = new System.Drawing.Size(83, 21);
            this.cbSummary.TabIndex = 168;
            this.cbSummary.Text = "summary";
            this.cbSummary.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cbSummary.UseVisualStyleBackColor = true;
            this.cbSummary.Visible = false;
            this.cbSummary.CheckedChanged += new System.EventHandler(this.cbSummary_CheckedChanged);
            // 
            // cmbReportType
            // 
            this.cmbReportType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReportType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbReportType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Location = new System.Drawing.Point(4, 39);
            this.cmbReportType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(208, 31);
            this.cmbReportType.TabIndex = 166;
            this.cmbReportType.SelectedIndexChanged += new System.EventHandler(this.cmbReportType_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbCustomer, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.57143F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.42857F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(344, 70);
            this.tableLayoutPanel1.TabIndex = 159;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel5.Controls.Add(this.lblZeroCostOnly, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbZeroCostOnly, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(338, 28);
            this.tableLayoutPanel5.TabIndex = 165;
            // 
            // lblZeroCostOnly
            // 
            this.lblZeroCostOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblZeroCostOnly.AutoSize = true;
            this.lblZeroCostOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblZeroCostOnly.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZeroCostOnly.Location = new System.Drawing.Point(224, 11);
            this.lblZeroCostOnly.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblZeroCostOnly.Name = "lblZeroCostOnly";
            this.lblZeroCostOnly.Size = new System.Drawing.Size(90, 17);
            this.lblZeroCostOnly.TabIndex = 165;
            this.lblZeroCostOnly.Text = "zero cost only";
            this.lblZeroCostOnly.Click += new System.EventHandler(this.lblZeroCostOnly_Click);
            // 
            // cbZeroCostOnly
            // 
            this.cbZeroCostOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbZeroCostOnly.AutoSize = true;
            this.cbZeroCostOnly.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbZeroCostOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbZeroCostOnly.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbZeroCostOnly.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbZeroCostOnly.Location = new System.Drawing.Point(199, 8);
            this.cbZeroCostOnly.Name = "cbZeroCostOnly";
            this.cbZeroCostOnly.Size = new System.Drawing.Size(18, 17);
            this.cbZeroCostOnly.TabIndex = 156;
            this.cbZeroCostOnly.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbZeroCostOnly.UseVisualStyleBackColor = true;
            this.cbZeroCostOnly.CheckedChanged += new System.EventHandler(this.cbZeroCostOnly_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 11);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 17);
            this.label8.TabIndex = 149;
            this.label8.Text = "CUSTOMER";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(4, 38);
            this.cmbCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(336, 31);
            this.cmbCustomer.TabIndex = 150;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1417, 39);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(124, 36);
            this.btnExcel.TabIndex = 157;
            this.btnExcel.Text = "EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.BackColor = System.Drawing.Color.White;
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Location = new System.Drawing.Point(682, 39);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(125, 36);
            this.btnFilter.TabIndex = 151;
            this.btnFilter.Text = "MORE FILTERS ...";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(576, 39);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 36);
            this.btnSearch.TabIndex = 142;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvMatUsedReport
            // 
            this.dgvMatUsedReport.AllowUserToAddRows = false;
            this.dgvMatUsedReport.AllowUserToDeleteRows = false;
            this.dgvMatUsedReport.AllowUserToOrderColumns = true;
            this.dgvMatUsedReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMatUsedReport.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMatUsedReport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMatUsedReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMatUsedReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatUsedReport.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatUsedReport.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMatUsedReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatUsedReport.GridColor = System.Drawing.Color.White;
            this.dgvMatUsedReport.Location = new System.Drawing.Point(4, 314);
            this.dgvMatUsedReport.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvMatUsedReport.Name = "dgvMatUsedReport";
            this.dgvMatUsedReport.ReadOnly = true;
            this.dgvMatUsedReport.RowHeadersVisible = false;
            this.dgvMatUsedReport.RowHeadersWidth = 51;
            this.dgvMatUsedReport.RowTemplate.Height = 60;
            this.dgvMatUsedReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatUsedReport.Size = new System.Drawing.Size(1542, 514);
            this.dgvMatUsedReport.TabIndex = 152;
            this.dgvMatUsedReport.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMatUsedReport_CellFormatting);
            this.dgvMatUsedReport.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMatUsedReport_DataBindingComplete);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 232F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 168F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1100F));
            this.tableLayoutPanel4.Controls.Add(this.lblNote, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblReportTitle, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnRefresh, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 270);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1544, 40);
            this.tableLayoutPanel4.TabIndex = 162;
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(1281, 28);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(260, 12);
            this.lblNote.TabIndex = 165;
            this.lblNote.Text = "*Maximum delivered qty per month within past 6 months.";
            this.lblNote.Visible = false;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReportTitle.AutoSize = true;
            this.lblReportTitle.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportTitle.Location = new System.Drawing.Point(4, 11);
            this.lblReportTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(156, 17);
            this.lblReportTitle.TabIndex = 153;
            this.lblReportTitle.Text = "MATERIAL  USED REPORT";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblLastUpdated, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblUpdatedTime, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(279, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(159, 34);
            this.tableLayoutPanel3.TabIndex = 161;
            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLastUpdated.AutoSize = true;
            this.lblLastUpdated.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastUpdated.Location = new System.Drawing.Point(3, 4);
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.Size = new System.Drawing.Size(73, 12);
            this.lblLastUpdated.TabIndex = 156;
            this.lblLastUpdated.Text = "LAST UPDATED:";
            this.lblLastUpdated.Visible = false;
            // 
            // lblUpdatedTime
            // 
            this.lblUpdatedTime.AutoSize = true;
            this.lblUpdatedTime.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdatedTime.Location = new System.Drawing.Point(3, 16);
            this.lblUpdatedTime.Name = "lblUpdatedTime";
            this.lblUpdatedTime.Size = new System.Drawing.Size(125, 12);
            this.lblUpdatedTime.TabIndex = 155;
            this.lblUpdatedTime.Text = "SHOW DATA FOR THE PAST";
            this.lblUpdatedTime.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_refresh_480;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(234, 2);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(36, 36);
            this.btnRefresh.TabIndex = 154;
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // gbFilter
            // 
            this.gbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFilter.Controls.Add(this.tlpFilter);
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 85);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1544, 169);
            this.gbFilter.TabIndex = 144;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "FILTER";
            // 
            // tlpFilter
            // 
            this.tlpFilter.ColumnCount = 6;
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 274F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 310F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.Controls.Add(this.groupBox2, 4, 0);
            this.tlpFilter.Controls.Add(this.groupBox1, 1, 0);
            this.tlpFilter.Controls.Add(this.gbMonthYear, 0, 0);
            this.tlpFilter.Controls.Add(this.btnFilterApply, 5, 0);
            this.tlpFilter.Controls.Add(this.gbItemType, 3, 0);
            this.tlpFilter.Controls.Add(this.gbDatePeriod, 2, 0);
            this.tlpFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilter.Location = new System.Drawing.Point(3, 21);
            this.tlpFilter.Name = "tlpFilter";
            this.tlpFilter.RowCount = 1;
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.Size = new System.Drawing.Size(1538, 145);
            this.tlpFilter.TabIndex = 167;
            this.tlpFilter.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpFilter_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbStockType);
            this.groupBox2.Controls.Add(this.cbForecastDeductStock);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(1067, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 139);
            this.groupBox2.TabIndex = 167;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FORECAST TYPE";
            // 
            // cbStockType
            // 
            this.cbStockType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbStockType.AutoSize = true;
            this.cbStockType.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbStockType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbStockType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockType.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbStockType.Location = new System.Drawing.Point(23, 95);
            this.cbStockType.Name = "cbStockType";
            this.cbStockType.Size = new System.Drawing.Size(105, 21);
            this.cbStockType.TabIndex = 158;
            this.cbStockType.Text = "ZERO STOCK";
            this.cbStockType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbStockType.UseVisualStyleBackColor = true;
            // 
            // cbForecastDeductStock
            // 
            this.cbForecastDeductStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbForecastDeductStock.AutoSize = true;
            this.cbForecastDeductStock.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbForecastDeductStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbForecastDeductStock.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbForecastDeductStock.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbForecastDeductStock.Location = new System.Drawing.Point(23, 59);
            this.cbForecastDeductStock.Name = "cbForecastDeductStock";
            this.cbForecastDeductStock.Size = new System.Drawing.Size(186, 21);
            this.cbForecastDeductStock.TabIndex = 157;
            this.cbForecastDeductStock.Text = "FORECAST - READY STOCK";
            this.cbForecastDeductStock.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbForecastDeductStock.UseVisualStyleBackColor = true;
            this.cbForecastDeductStock.CheckedChanged += new System.EventHandler(this.cbForecastDeduteStock_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cmbMonthTo);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.cmbYearTo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbMonthFrom);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbYearFrom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(243, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 139);
            this.groupBox1.TabIndex = 166;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MONTH/YEAR";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(27, 106);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 15);
            this.label9.TabIndex = 162;
            this.label9.Text = "TO:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(62, 80);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 15);
            this.label10.TabIndex = 158;
            this.label10.Text = "MONTH";
            // 
            // cmbMonthTo
            // 
            this.cmbMonthTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthTo.FormattingEnabled = true;
            this.cmbMonthTo.Location = new System.Drawing.Point(64, 101);
            this.cmbMonthTo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonthTo.Name = "cmbMonthTo";
            this.cmbMonthTo.Size = new System.Drawing.Size(62, 25);
            this.cmbMonthTo.TabIndex = 159;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(131, 80);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 15);
            this.label12.TabIndex = 160;
            this.label12.Text = "YEAR";
            // 
            // cmbYearTo
            // 
            this.cmbYearTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbYearTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbYearTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearTo.FormattingEnabled = true;
            this.cmbYearTo.Location = new System.Drawing.Point(134, 101);
            this.cmbYearTo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbYearTo.Name = "cmbYearTo";
            this.cmbYearTo.Size = new System.Drawing.Size(81, 25);
            this.cmbYearTo.TabIndex = 161;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 15);
            this.label5.TabIndex = 157;
            this.label5.Text = "FROM:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(62, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 153;
            this.label4.Text = "MONTH";
            // 
            // cmbMonthFrom
            // 
            this.cmbMonthFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthFrom.FormattingEnabled = true;
            this.cmbMonthFrom.Location = new System.Drawing.Point(64, 44);
            this.cmbMonthFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonthFrom.Name = "cmbMonthFrom";
            this.cmbMonthFrom.Size = new System.Drawing.Size(62, 25);
            this.cmbMonthFrom.TabIndex = 154;
            this.cmbMonthFrom.SelectedIndexChanged += new System.EventHandler(this.cmbMonthFrom_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(131, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 15);
            this.label6.TabIndex = 155;
            this.label6.Text = "YEAR";
            // 
            // cmbYearFrom
            // 
            this.cmbYearFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbYearFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbYearFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearFrom.FormattingEnabled = true;
            this.cmbYearFrom.Location = new System.Drawing.Point(134, 44);
            this.cmbYearFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbYearFrom.Name = "cmbYearFrom";
            this.cmbYearFrom.Size = new System.Drawing.Size(81, 25);
            this.cmbYearFrom.TabIndex = 156;
            // 
            // gbMonthYear
            // 
            this.gbMonthYear.Controls.Add(this.label2);
            this.gbMonthYear.Controls.Add(this.lblCurrentMonth);
            this.gbMonthYear.Controls.Add(this.cmbMonth);
            this.gbMonthYear.Controls.Add(this.label3);
            this.gbMonthYear.Controls.Add(this.cmbYear);
            this.gbMonthYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMonthYear.Location = new System.Drawing.Point(3, 3);
            this.gbMonthYear.Name = "gbMonthYear";
            this.gbMonthYear.Size = new System.Drawing.Size(234, 139);
            this.gbMonthYear.TabIndex = 151;
            this.gbMonthYear.TabStop = false;
            this.gbMonthYear.Text = "MONTH/YEAR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 153;
            this.label2.Text = "MONTH";
            // 
            // lblCurrentMonth
            // 
            this.lblCurrentMonth.AutoSize = true;
            this.lblCurrentMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCurrentMonth.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMonth.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrentMonth.Location = new System.Drawing.Point(17, 82);
            this.lblCurrentMonth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentMonth.Name = "lblCurrentMonth";
            this.lblCurrentMonth.Size = new System.Drawing.Size(69, 17);
            this.lblCurrentMonth.TabIndex = 166;
            this.lblCurrentMonth.Text = "this month";
            this.lblCurrentMonth.Click += new System.EventHandler(this.lblCurrentMonth_Click);
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(21, 53);
            this.cmbMonth.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(91, 25);
            this.cmbMonth.TabIndex = 154;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(120, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 155;
            this.label3.Text = "YEAR";
            // 
            // cmbYear
            // 
            this.cmbYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbYear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(120, 53);
            this.cmbYear.Margin = new System.Windows.Forms.Padding(4);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(97, 25);
            this.cmbYear.TabIndex = 156;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilterApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFilterApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterApply.ForeColor = System.Drawing.Color.White;
            this.btnFilterApply.Location = new System.Drawing.Point(1308, 104);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.Size = new System.Drawing.Size(124, 36);
            this.btnFilterApply.TabIndex = 145;
            this.btnFilterApply.Text = "APPLY";
            this.btnFilterApply.UseVisualStyleBackColor = false;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // gbItemType
            // 
            this.gbItemType.Controls.Add(this.cbSubMat);
            this.gbItemType.Controls.Add(this.cbPigment);
            this.gbItemType.Controls.Add(this.cbMasterBatch);
            this.gbItemType.Controls.Add(this.cbRawMat);
            this.gbItemType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbItemType.Location = new System.Drawing.Point(757, 3);
            this.gbItemType.Name = "gbItemType";
            this.gbItemType.Size = new System.Drawing.Size(304, 139);
            this.gbItemType.TabIndex = 166;
            this.gbItemType.TabStop = false;
            this.gbItemType.Text = "ITEM TYPE";
            // 
            // cbSubMat
            // 
            this.cbSubMat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSubMat.AutoSize = true;
            this.cbSubMat.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSubMat.Checked = true;
            this.cbSubMat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSubMat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSubMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSubMat.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSubMat.Location = new System.Drawing.Point(173, 95);
            this.cbSubMat.Name = "cbSubMat";
            this.cbSubMat.Size = new System.Drawing.Size(115, 21);
            this.cbSubMat.TabIndex = 160;
            this.cbSubMat.Text = "SUB MATERIAL";
            this.cbSubMat.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSubMat.UseVisualStyleBackColor = true;
            this.cbSubMat.CheckedChanged += new System.EventHandler(this.cbSubMat_CheckedChanged);
            // 
            // cbPigment
            // 
            this.cbPigment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPigment.AutoSize = true;
            this.cbPigment.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbPigment.Checked = true;
            this.cbPigment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPigment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbPigment.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPigment.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbPigment.Location = new System.Drawing.Point(24, 95);
            this.cbPigment.Name = "cbPigment";
            this.cbPigment.Size = new System.Drawing.Size(85, 21);
            this.cbPigment.TabIndex = 159;
            this.cbPigment.Text = "PIGMENT";
            this.cbPigment.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbPigment.UseVisualStyleBackColor = true;
            this.cbPigment.CheckedChanged += new System.EventHandler(this.cbPigment_CheckedChanged);
            // 
            // cbMasterBatch
            // 
            this.cbMasterBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMasterBatch.AutoSize = true;
            this.cbMasterBatch.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbMasterBatch.Checked = true;
            this.cbMasterBatch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMasterBatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMasterBatch.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMasterBatch.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbMasterBatch.Location = new System.Drawing.Point(172, 59);
            this.cbMasterBatch.Name = "cbMasterBatch";
            this.cbMasterBatch.Size = new System.Drawing.Size(120, 21);
            this.cbMasterBatch.TabIndex = 158;
            this.cbMasterBatch.Text = "MASTER BATCH";
            this.cbMasterBatch.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbMasterBatch.UseVisualStyleBackColor = true;
            this.cbMasterBatch.CheckedChanged += new System.EventHandler(this.cbMasterBatch_CheckedChanged);
            // 
            // cbRawMat
            // 
            this.cbRawMat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRawMat.AutoSize = true;
            this.cbRawMat.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRawMat.Checked = true;
            this.cbRawMat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRawMat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRawMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRawMat.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRawMat.Location = new System.Drawing.Point(27, 59);
            this.cbRawMat.Name = "cbRawMat";
            this.cbRawMat.Size = new System.Drawing.Size(119, 21);
            this.cbRawMat.TabIndex = 157;
            this.cbRawMat.Text = "RAW MATERIAL";
            this.cbRawMat.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRawMat.UseVisualStyleBackColor = true;
            this.cbRawMat.CheckedChanged += new System.EventHandler(this.cbRawMat_CheckedChanged);
            // 
            // gbDatePeriod
            // 
            this.gbDatePeriod.Controls.Add(this.lblChangeDate);
            this.gbDatePeriod.Controls.Add(this.dtpTo);
            this.gbDatePeriod.Controls.Add(this.dtpFrom);
            this.gbDatePeriod.Controls.Add(this.label7);
            this.gbDatePeriod.Controls.Add(this.label11);
            this.gbDatePeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDatePeriod.Location = new System.Drawing.Point(483, 3);
            this.gbDatePeriod.Name = "gbDatePeriod";
            this.gbDatePeriod.Size = new System.Drawing.Size(268, 139);
            this.gbDatePeriod.TabIndex = 152;
            this.gbDatePeriod.TabStop = false;
            this.gbDatePeriod.Text = "DATE PERIOD";
            // 
            // lblChangeDate
            // 
            this.lblChangeDate.AutoSize = true;
            this.lblChangeDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChangeDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeDate.ForeColor = System.Drawing.Color.Blue;
            this.lblChangeDate.Location = new System.Drawing.Point(167, 82);
            this.lblChangeDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChangeDate.Name = "lblChangeDate";
            this.lblChangeDate.Size = new System.Drawing.Size(80, 17);
            this.lblChangeDate.TabIndex = 165;
            this.lblChangeDate.Text = "change date";
            this.lblChangeDate.Visible = false;
            this.lblChangeDate.Click += new System.EventHandler(this.lblChangeDate_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(138, 54);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(113, 25);
            this.dtpTo.TabIndex = 154;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(19, 54);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(113, 25);
            this.dtpFrom.TabIndex = 153;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(134, 33);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 17);
            this.label7.TabIndex = 150;
            this.label7.Text = "TO";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 32);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 17);
            this.label11.TabIndex = 148;
            this.label11.Text = "FROM";
            // 
            // frmMaterialUsedReport_NEW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.tlpForecastReport);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMaterialUsedReport_NEW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMaterialUsedReport_NEW";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMaterialUsedReport_NEW_FormClosed);
            this.Load += new System.EventHandler(this.frmMaterialUsedReport_NEW_Load);
            this.tlpForecastReport.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatUsedReport)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.tlpFilter.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbMonthYear.ResumeLayout(false);
            this.gbMonthYear.PerformLayout();
            this.gbItemType.ResumeLayout(false);
            this.gbItemType.PerformLayout();
            this.gbDatePeriod.ResumeLayout(false);
            this.gbDatePeriod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpForecastReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lblZeroCostOnly;
        private System.Windows.Forms.CheckBox cbZeroCostOnly;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.DataGridView dgvMatUsedReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblReportTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblLastUpdated;
        private System.Windows.Forms.Label lblUpdatedTime;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.GroupBox gbDatePeriod;
        private System.Windows.Forms.Label lblChangeDate;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gbMonthYear;
        private System.Windows.Forms.Label lblCurrentMonth;
        private System.Windows.Forms.Button btnFilterApply;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.GroupBox gbItemType;
        private System.Windows.Forms.CheckBox cbSubMat;
        private System.Windows.Forms.CheckBox cbPigment;
        private System.Windows.Forms.CheckBox cbMasterBatch;
        private System.Windows.Forms.CheckBox cbRawMat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.CheckBox cbSummary;
        private System.Windows.Forms.TableLayoutPanel tlpFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbMonthFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbYearFrom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbMonthTo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbYearTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbForecastDeductStock;
        private System.Windows.Forms.CheckBox cbStockType;
    }
}