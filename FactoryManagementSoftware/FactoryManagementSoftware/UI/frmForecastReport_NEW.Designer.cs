namespace FactoryManagementSoftware.UI
{
    partial class frmForecastReport_NEW
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.tlpForecastReport = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExcelAll = new System.Windows.Forms.Button();
            this.btnSummary = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cbMainCustomerOnly = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.btnFullReport = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.dgvForecastReport = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblForecastType = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLastUpdated = new System.Windows.Forms.Label();
            this.lblUpdatedTime = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbIncludeTerminated = new System.Windows.Forms.CheckBox();
            this.cbIncludeProInfo = new System.Windows.Forms.CheckBox();
            this.cbWithSubMat = new System.Windows.Forms.CheckBox();
            this.cbRemoveNoOrderItem = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbShowToOrderItem = new System.Windows.Forms.CheckBox();
            this.cbShowToAssemblyItem = new System.Windows.Forms.CheckBox();
            this.cbShowToProduceItem = new System.Windows.Forms.CheckBox();
            this.cbSortByBalance = new System.Windows.Forms.CheckBox();
            this.cbSortByToDOType = new System.Windows.Forms.CheckBox();
            this.cbShowRawMaterial = new System.Windows.Forms.CheckBox();
            this.cbShowInsufficientOnly = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCurrentMonth = new System.Windows.Forms.Label();
            this.cmbForecastTo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbForecastFrom = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNameSearch = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbDescending = new System.Windows.Forms.CheckBox();
            this.cmbSoryBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAlertLevel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblChangeDate = new System.Windows.Forms.Label();
            this.dtpOutTo = new System.Windows.Forms.DateTimePicker();
            this.dtpOutFrom = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tlpForecastReport.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForecastReport)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.tlpForecastReport.Controls.Add(this.dgvForecastReport, 0, 3);
            this.tlpForecastReport.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tlpForecastReport.Controls.Add(this.gbFilter, 0, 1);
            this.tlpForecastReport.Location = new System.Drawing.Point(12, 12);
            this.tlpForecastReport.Name = "tlpForecastReport";
            this.tlpForecastReport.RowCount = 4;
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 257F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tlpForecastReport.Size = new System.Drawing.Size(1547, 829);
            this.tlpForecastReport.TabIndex = 164;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.Controls.Add(this.btnExcelAll, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSummary, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFullReport, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExcel, 5, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1547, 76);
            this.tableLayoutPanel2.TabIndex = 160;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // btnExcelAll
            // 
            this.btnExcelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelAll.BackColor = System.Drawing.Color.White;
            this.btnExcelAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcelAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelAll.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelAll.ForeColor = System.Drawing.Color.Black;
            this.btnExcelAll.Location = new System.Drawing.Point(1272, 38);
            this.btnExcelAll.Margin = new System.Windows.Forms.Padding(5, 0, 5, 2);
            this.btnExcelAll.Name = "btnExcelAll";
            this.btnExcelAll.Size = new System.Drawing.Size(130, 36);
            this.btnExcelAll.TabIndex = 165;
            this.btnExcelAll.Text = "EXCEL ALL";
            this.btnExcelAll.UseVisualStyleBackColor = false;
            this.btnExcelAll.Visible = false;
            this.btnExcelAll.Click += new System.EventHandler(this.btnExcelAll_Click);
            // 
            // btnSummary
            // 
            this.btnSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnSummary.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSummary.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSummary.ForeColor = System.Drawing.Color.Black;
            this.btnSummary.Location = new System.Drawing.Point(495, 38);
            this.btnSummary.Margin = new System.Windows.Forms.Padding(5, 0, 5, 2);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(130, 36);
            this.btnSummary.TabIndex = 165;
            this.btnSummary.Text = "SUMMARY";
            this.btnSummary.UseVisualStyleBackColor = false;
            this.btnSummary.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbCustomer, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.31579F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.68421F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 76);
            this.tableLayoutPanel1.TabIndex = 159;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.cbMainCustomerOnly, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(350, 39);
            this.tableLayoutPanel5.TabIndex = 165;
            // 
            // cbMainCustomerOnly
            // 
            this.cbMainCustomerOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMainCustomerOnly.AutoSize = true;
            this.cbMainCustomerOnly.Checked = true;
            this.cbMainCustomerOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMainCustomerOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMainCustomerOnly.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMainCustomerOnly.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbMainCustomerOnly.Location = new System.Drawing.Point(198, 18);
            this.cbMainCustomerOnly.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.cbMainCustomerOnly.Name = "cbMainCustomerOnly";
            this.cbMainCustomerOnly.Size = new System.Drawing.Size(149, 21);
            this.cbMainCustomerOnly.TabIndex = 167;
            this.cbMainCustomerOnly.Text = "Main Customer Only";
            this.cbMainCustomerOnly.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbMainCustomerOnly.UseVisualStyleBackColor = true;
            this.cbMainCustomerOnly.CheckedChanged += new System.EventHandler(this.cbMainCustomerOnly_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 22);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 17);
            this.label8.TabIndex = 149;
            this.label8.Text = "CUSTOMER";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(5, 39);
            this.cmbCustomer.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(336, 31);
            this.cmbCustomer.TabIndex = 150;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            this.cmbCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCustomer_KeyDown);
            // 
            // btnFullReport
            // 
            this.btnFullReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFullReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFullReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFullReport.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFullReport.ForeColor = System.Drawing.Color.White;
            this.btnFullReport.Location = new System.Drawing.Point(355, 38);
            this.btnFullReport.Margin = new System.Windows.Forms.Padding(5, 0, 5, 2);
            this.btnFullReport.Name = "btnFullReport";
            this.btnFullReport.Size = new System.Drawing.Size(130, 36);
            this.btnFullReport.TabIndex = 142;
            this.btnFullReport.Text = "FULL REPORT";
            this.btnFullReport.UseVisualStyleBackColor = false;
            this.btnFullReport.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.BackColor = System.Drawing.Color.White;
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Location = new System.Drawing.Point(635, 38);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(5, 0, 5, 2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(130, 36);
            this.btnFilter.TabIndex = 151;
            this.btnFilter.Text = "SEARCH FILTER";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1412, 38);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 2);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(130, 36);
            this.btnExcel.TabIndex = 157;
            this.btnExcel.Text = "EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // dgvForecastReport
            // 
            this.dgvForecastReport.AllowUserToAddRows = false;
            this.dgvForecastReport.AllowUserToDeleteRows = false;
            this.dgvForecastReport.AllowUserToOrderColumns = true;
            this.dgvForecastReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvForecastReport.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvForecastReport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvForecastReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForecastReport.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvForecastReport.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvForecastReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvForecastReport.GridColor = System.Drawing.Color.White;
            this.dgvForecastReport.Location = new System.Drawing.Point(4, 392);
            this.dgvForecastReport.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvForecastReport.Name = "dgvForecastReport";
            this.dgvForecastReport.ReadOnly = true;
            this.dgvForecastReport.RowHeadersVisible = false;
            this.dgvForecastReport.RowHeadersWidth = 51;
            this.dgvForecastReport.RowTemplate.Height = 60;
            this.dgvForecastReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvForecastReport.Size = new System.Drawing.Size(1539, 436);
            this.dgvForecastReport.TabIndex = 152;
            this.dgvForecastReport.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvForecastReport_CellFormatting);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1254F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.lblForecastType, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnRefresh, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 342);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1541, 46);
            this.tableLayoutPanel4.TabIndex = 162;
            // 
            // lblForecastType
            // 
            this.lblForecastType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblForecastType.AutoSize = true;
            this.lblForecastType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForecastType.Location = new System.Drawing.Point(4, 14);
            this.lblForecastType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblForecastType.Name = "lblForecastType";
            this.lblForecastType.Size = new System.Drawing.Size(119, 17);
            this.lblForecastType.TabIndex = 153;
            this.lblForecastType.Text = "FORECAST REPORT";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblLastUpdated, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblUpdatedTime, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(140, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(142, 40);
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
            this.btnRefresh.Location = new System.Drawing.Point(289, 5);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(36, 36);
            this.btnRefresh.TabIndex = 154;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Visible = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.tableLayoutPanel6);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 85);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1541, 251);
            this.gbFilter.TabIndex = 144;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "FILTER";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 7;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 284F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 233F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 236F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.groupBox6, 5, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox1, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox4, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox5, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1535, 227);
            this.tableLayoutPanel6.TabIndex = 165;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbIncludeTerminated);
            this.groupBox6.Controls.Add(this.cbIncludeProInfo);
            this.groupBox6.Controls.Add(this.cbWithSubMat);
            this.groupBox6.Controls.Add(this.cbRemoveNoOrderItem);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(1180, 3);
            this.groupBox6.MinimumSize = new System.Drawing.Size(130, 115);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(230, 221);
            this.groupBox6.TabIndex = 166;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "OTHER";
            // 
            // cbIncludeTerminated
            // 
            this.cbIncludeTerminated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbIncludeTerminated.AutoSize = true;
            this.cbIncludeTerminated.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbIncludeTerminated.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeTerminated.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeTerminated.Location = new System.Drawing.Point(17, 53);
            this.cbIncludeTerminated.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbIncludeTerminated.Name = "cbIncludeTerminated";
            this.cbIncludeTerminated.Size = new System.Drawing.Size(169, 21);
            this.cbIncludeTerminated.TabIndex = 165;
            this.cbIncludeTerminated.Text = "Include Terminated Item";
            this.cbIncludeTerminated.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeTerminated.UseVisualStyleBackColor = true;
            this.cbIncludeTerminated.CheckedChanged += new System.EventHandler(this.cbIncludeTerminated_CheckedChanged);
            // 
            // cbIncludeProInfo
            // 
            this.cbIncludeProInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbIncludeProInfo.AutoSize = true;
            this.cbIncludeProInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbIncludeProInfo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeProInfo.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeProInfo.Location = new System.Drawing.Point(17, 106);
            this.cbIncludeProInfo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbIncludeProInfo.Name = "cbIncludeProInfo";
            this.cbIncludeProInfo.Size = new System.Drawing.Size(121, 21);
            this.cbIncludeProInfo.TabIndex = 166;
            this.cbIncludeProInfo.Text = "include Pro Info";
            this.cbIncludeProInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeProInfo.UseVisualStyleBackColor = true;
            this.cbIncludeProInfo.CheckedChanged += new System.EventHandler(this.cbIncludeProInfo_CheckedChanged);
            // 
            // cbWithSubMat
            // 
            this.cbWithSubMat.AutoSize = true;
            this.cbWithSubMat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbWithSubMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWithSubMat.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbWithSubMat.Location = new System.Drawing.Point(17, 25);
            this.cbWithSubMat.Name = "cbWithSubMat";
            this.cbWithSubMat.Size = new System.Drawing.Size(145, 21);
            this.cbWithSubMat.TabIndex = 156;
            this.cbWithSubMat.Text = "Show Sub Materials";
            this.cbWithSubMat.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbWithSubMat.UseVisualStyleBackColor = true;
            this.cbWithSubMat.CheckedChanged += new System.EventHandler(this.cbWithSubMat_CheckedChanged);
            // 
            // cbRemoveNoOrderItem
            // 
            this.cbRemoveNoOrderItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbRemoveNoOrderItem.AutoSize = true;
            this.cbRemoveNoOrderItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRemoveNoOrderItem.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRemoveNoOrderItem.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRemoveNoOrderItem.Location = new System.Drawing.Point(17, 79);
            this.cbRemoveNoOrderItem.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbRemoveNoOrderItem.Name = "cbRemoveNoOrderItem";
            this.cbRemoveNoOrderItem.Size = new System.Drawing.Size(181, 21);
            this.cbRemoveNoOrderItem.TabIndex = 167;
            this.cbRemoveNoOrderItem.Text = "Remove No Forecast Item";
            this.cbRemoveNoOrderItem.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRemoveNoOrderItem.UseVisualStyleBackColor = true;
            this.cbRemoveNoOrderItem.CheckedChanged += new System.EventHandler(this.cbRemoveForecastInvalidItem_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbShowToOrderItem);
            this.groupBox1.Controls.Add(this.cbShowToAssemblyItem);
            this.groupBox1.Controls.Add(this.cbShowToProduceItem);
            this.groupBox1.Controls.Add(this.cbSortByBalance);
            this.groupBox1.Controls.Add(this.cbSortByToDOType);
            this.groupBox1.Controls.Add(this.cbShowRawMaterial);
            this.groupBox1.Controls.Add(this.cbShowInsufficientOnly);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(947, 3);
            this.groupBox1.MinimumSize = new System.Drawing.Size(130, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 221);
            this.groupBox1.TabIndex = 165;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SUMMARY LIST";
            // 
            // cbShowToOrderItem
            // 
            this.cbShowToOrderItem.AutoSize = true;
            this.cbShowToOrderItem.Checked = true;
            this.cbShowToOrderItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowToOrderItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowToOrderItem.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowToOrderItem.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowToOrderItem.Location = new System.Drawing.Point(18, 159);
            this.cbShowToOrderItem.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbShowToOrderItem.Name = "cbShowToOrderItem";
            this.cbShowToOrderItem.Size = new System.Drawing.Size(153, 21);
            this.cbShowToOrderItem.TabIndex = 170;
            this.cbShowToOrderItem.Text = "Show To Order Items";
            this.cbShowToOrderItem.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowToOrderItem.UseVisualStyleBackColor = true;
            this.cbShowToOrderItem.CheckedChanged += new System.EventHandler(this.cbShowToOrderItem_CheckedChanged);
            // 
            // cbShowToAssemblyItem
            // 
            this.cbShowToAssemblyItem.AutoSize = true;
            this.cbShowToAssemblyItem.Checked = true;
            this.cbShowToAssemblyItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowToAssemblyItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowToAssemblyItem.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowToAssemblyItem.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowToAssemblyItem.Location = new System.Drawing.Point(18, 133);
            this.cbShowToAssemblyItem.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbShowToAssemblyItem.Name = "cbShowToAssemblyItem";
            this.cbShowToAssemblyItem.Size = new System.Drawing.Size(173, 21);
            this.cbShowToAssemblyItem.TabIndex = 170;
            this.cbShowToAssemblyItem.Text = "Show To Assembly Items";
            this.cbShowToAssemblyItem.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowToAssemblyItem.UseVisualStyleBackColor = true;
            this.cbShowToAssemblyItem.CheckedChanged += new System.EventHandler(this.cbShowToAssemblyItem_CheckedChanged);
            // 
            // cbShowToProduceItem
            // 
            this.cbShowToProduceItem.AutoSize = true;
            this.cbShowToProduceItem.Checked = true;
            this.cbShowToProduceItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowToProduceItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowToProduceItem.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowToProduceItem.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowToProduceItem.Location = new System.Drawing.Point(18, 106);
            this.cbShowToProduceItem.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbShowToProduceItem.Name = "cbShowToProduceItem";
            this.cbShowToProduceItem.Size = new System.Drawing.Size(166, 21);
            this.cbShowToProduceItem.TabIndex = 169;
            this.cbShowToProduceItem.Text = "Show To Produce Items";
            this.cbShowToProduceItem.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowToProduceItem.UseVisualStyleBackColor = true;
            this.cbShowToProduceItem.CheckedChanged += new System.EventHandler(this.cbShowToProduceItem_CheckedChanged);
            // 
            // cbSortByBalance
            // 
            this.cbSortByBalance.AutoSize = true;
            this.cbSortByBalance.Checked = true;
            this.cbSortByBalance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSortByBalance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSortByBalance.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortByBalance.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSortByBalance.Location = new System.Drawing.Point(18, 79);
            this.cbSortByBalance.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbSortByBalance.Name = "cbSortByBalance";
            this.cbSortByBalance.Size = new System.Drawing.Size(119, 21);
            this.cbSortByBalance.TabIndex = 168;
            this.cbSortByBalance.Text = "Sort By Balance";
            this.cbSortByBalance.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSortByBalance.UseVisualStyleBackColor = true;
            this.cbSortByBalance.CheckedChanged += new System.EventHandler(this.cbSortByBalance_CheckedChanged);
            // 
            // cbSortByToDOType
            // 
            this.cbSortByToDOType.AutoSize = true;
            this.cbSortByToDOType.Checked = true;
            this.cbSortByToDOType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSortByToDOType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSortByToDOType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortByToDOType.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSortByToDOType.Location = new System.Drawing.Point(18, 52);
            this.cbSortByToDOType.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbSortByToDOType.Name = "cbSortByToDOType";
            this.cbSortByToDOType.Size = new System.Drawing.Size(143, 21);
            this.cbSortByToDOType.TabIndex = 166;
            this.cbSortByToDOType.Text = "Sort By To DO Type";
            this.cbSortByToDOType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSortByToDOType.UseVisualStyleBackColor = true;
            this.cbSortByToDOType.CheckedChanged += new System.EventHandler(this.cbSortByToDOType_CheckedChanged);
            // 
            // cbShowRawMaterial
            // 
            this.cbShowRawMaterial.AutoSize = true;
            this.cbShowRawMaterial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowRawMaterial.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowRawMaterial.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowRawMaterial.Location = new System.Drawing.Point(18, 185);
            this.cbShowRawMaterial.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbShowRawMaterial.Name = "cbShowRawMaterial";
            this.cbShowRawMaterial.Size = new System.Drawing.Size(141, 21);
            this.cbShowRawMaterial.TabIndex = 167;
            this.cbShowRawMaterial.Text = "Show Raw Material";
            this.cbShowRawMaterial.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowRawMaterial.UseVisualStyleBackColor = true;
            this.cbShowRawMaterial.CheckedChanged += new System.EventHandler(this.cbShowRawMaterial_CheckedChanged);
            // 
            // cbShowInsufficientOnly
            // 
            this.cbShowInsufficientOnly.AutoSize = true;
            this.cbShowInsufficientOnly.Checked = true;
            this.cbShowInsufficientOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowInsufficientOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowInsufficientOnly.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowInsufficientOnly.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowInsufficientOnly.Location = new System.Drawing.Point(18, 25);
            this.cbShowInsufficientOnly.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.cbShowInsufficientOnly.Name = "cbShowInsufficientOnly";
            this.cbShowInsufficientOnly.Size = new System.Drawing.Size(185, 21);
            this.cbShowInsufficientOnly.TabIndex = 167;
            this.cbShowInsufficientOnly.Text = "Show Insufficient Item Only";
            this.cbShowInsufficientOnly.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowInsufficientOnly.UseVisualStyleBackColor = true;
            this.cbShowInsufficientOnly.CheckedChanged += new System.EventHandler(this.cbShowInsufficientOnly_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCurrentMonth);
            this.groupBox2.Controls.Add(this.cmbForecastTo);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbForecastFrom);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 221);
            this.groupBox2.TabIndex = 151;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FORECAST PERIOD";
            // 
            // lblCurrentMonth
            // 
            this.lblCurrentMonth.AutoSize = true;
            this.lblCurrentMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCurrentMonth.Font = new System.Drawing.Font("Segoe UI", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentMonth.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrentMonth.Location = new System.Drawing.Point(88, 33);
            this.lblCurrentMonth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentMonth.Name = "lblCurrentMonth";
            this.lblCurrentMonth.Size = new System.Drawing.Size(90, 17);
            this.lblCurrentMonth.TabIndex = 166;
            this.lblCurrentMonth.Text = "Current Month";
            this.lblCurrentMonth.Visible = false;
            this.lblCurrentMonth.Click += new System.EventHandler(this.lblCurrentMonth_Click);
            // 
            // cmbForecastTo
            // 
            this.cmbForecastTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbForecastTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbForecastTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForecastTo.Enabled = false;
            this.cmbForecastTo.FormattingEnabled = true;
            this.cmbForecastTo.Location = new System.Drawing.Point(61, 95);
            this.cmbForecastTo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbForecastTo.Name = "cmbForecastTo";
            this.cmbForecastTo.Size = new System.Drawing.Size(127, 25);
            this.cmbForecastTo.TabIndex = 151;
            this.cmbForecastTo.SelectedIndexChanged += new System.EventHandler(this.cmbForecastTo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(30, 95);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 17);
            this.label6.TabIndex = 150;
            this.label6.Text = "TO";
            // 
            // cmbForecastFrom
            // 
            this.cmbForecastFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbForecastFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbForecastFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForecastFrom.FormattingEnabled = true;
            this.cmbForecastFrom.Location = new System.Drawing.Point(60, 54);
            this.cmbForecastFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbForecastFrom.Name = "cmbForecastFrom";
            this.cmbForecastFrom.Size = new System.Drawing.Size(128, 25);
            this.cmbForecastFrom.TabIndex = 149;
            this.cmbForecastFrom.SelectedIndexChanged += new System.EventHandler(this.cmbForecastFrom_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 148;
            this.label4.Text = "FROM";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtNameSearch);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(717, 3);
            this.groupBox4.MinimumSize = new System.Drawing.Size(130, 115);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(224, 221);
            this.groupBox4.TabIndex = 153;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SEARCH";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 32);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 17);
            this.label12.TabIndex = 155;
            this.label12.Text = "NAME/CODE";
            // 
            // txtNameSearch
            // 
            this.txtNameSearch.Location = new System.Drawing.Point(17, 54);
            this.txtNameSearch.Name = "txtNameSearch";
            this.txtNameSearch.Size = new System.Drawing.Size(200, 25);
            this.txtNameSearch.TabIndex = 155;
            this.txtNameSearch.TextChanged += new System.EventHandler(this.txtNameSearch_TextChanged);
            this.txtNameSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNameSearch_KeyDown);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbDescending);
            this.groupBox5.Controls.Add(this.cmbSoryBy);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtAlertLevel);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(433, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(278, 221);
            this.groupBox5.TabIndex = 154;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "SORTING";
            // 
            // cbDescending
            // 
            this.cbDescending.AutoSize = true;
            this.cbDescending.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDescending.Location = new System.Drawing.Point(16, 82);
            this.cbDescending.Name = "cbDescending";
            this.cbDescending.Size = new System.Drawing.Size(97, 21);
            this.cbDescending.TabIndex = 151;
            this.cbDescending.Text = "descending";
            this.cbDescending.UseVisualStyleBackColor = true;
            this.cbDescending.CheckedChanged += new System.EventHandler(this.cbDescending_CheckedChanged);
            // 
            // cmbSoryBy
            // 
            this.cmbSoryBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSoryBy.FormattingEnabled = true;
            this.cmbSoryBy.Location = new System.Drawing.Point(16, 54);
            this.cmbSoryBy.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSoryBy.Name = "cmbSoryBy";
            this.cmbSoryBy.Size = new System.Drawing.Size(150, 25);
            this.cmbSoryBy.TabIndex = 147;
            this.cmbSoryBy.SelectedIndexChanged += new System.EventHandler(this.cmbSoryBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 146;
            this.label2.Text = "SORT BY";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(129, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 148;
            this.label1.Text = "clear";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtAlertLevel
            // 
            this.txtAlertLevel.Location = new System.Drawing.Point(173, 54);
            this.txtAlertLevel.Name = "txtAlertLevel";
            this.txtAlertLevel.Size = new System.Drawing.Size(92, 25);
            this.txtAlertLevel.TabIndex = 149;
            this.txtAlertLevel.Text = "0";
            this.txtAlertLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAlertLevel.TextChanged += new System.EventHandler(this.txtAlertLevel_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(178, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 150;
            this.label3.Text = "ALERT LEVEL";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblChangeDate);
            this.groupBox3.Controls.Add(this.dtpOutTo);
            this.groupBox3.Controls.Add(this.dtpOutFrom);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(213, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(214, 221);
            this.groupBox3.TabIndex = 152;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "OUTGOING PERIOD";
            // 
            // lblChangeDate
            // 
            this.lblChangeDate.AutoSize = true;
            this.lblChangeDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChangeDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeDate.ForeColor = System.Drawing.Color.Blue;
            this.lblChangeDate.Location = new System.Drawing.Point(83, 121);
            this.lblChangeDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChangeDate.Name = "lblChangeDate";
            this.lblChangeDate.Size = new System.Drawing.Size(99, 17);
            this.lblChangeDate.TabIndex = 165;
            this.lblChangeDate.Text = "PMMA Date Edit";
            this.lblChangeDate.Visible = false;
            this.lblChangeDate.Click += new System.EventHandler(this.lblChangeDate_Click);
            // 
            // dtpOutTo
            // 
            this.dtpOutTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOutTo.Location = new System.Drawing.Point(67, 92);
            this.dtpOutTo.Name = "dtpOutTo";
            this.dtpOutTo.Size = new System.Drawing.Size(126, 25);
            this.dtpOutTo.TabIndex = 154;
            this.dtpOutTo.ValueChanged += new System.EventHandler(this.dtpOutTo_ValueChanged);
            // 
            // dtpOutFrom
            // 
            this.dtpOutFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOutFrom.Location = new System.Drawing.Point(69, 51);
            this.dtpOutFrom.Name = "dtpOutFrom";
            this.dtpOutFrom.Size = new System.Drawing.Size(124, 25);
            this.dtpOutFrom.TabIndex = 153;
            this.dtpOutFrom.ValueChanged += new System.EventHandler(this.dtpOutFrom_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(33, 95);
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
            this.label11.Location = new System.Drawing.Point(14, 54);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 17);
            this.label11.TabIndex = 148;
            this.label11.Text = "FROM";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmForecastReport_NEW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.tlpForecastReport);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmForecastReport_NEW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmForecastReport_NEW";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmForecastReport_NEW_FormClosed);
            this.Load += new System.EventHandler(this.frmForecastReport_NEW_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmForecastReport_NEW_KeyDown);
            this.tlpForecastReport.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForecastReport)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.TableLayoutPanel tlpForecastReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnFullReport;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DataGridView dgvForecastReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblForecastType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblLastUpdated;
        private System.Windows.Forms.Label lblUpdatedTime;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Button btnExcelAll;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmbSoryBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAlertLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNameSearch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpOutTo;
        private System.Windows.Forms.DateTimePicker dtpOutFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbForecastTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbForecastFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbDescending;
        private System.Windows.Forms.CheckBox cbWithSubMat;
        private System.Windows.Forms.Label lblChangeDate;
        private System.Windows.Forms.Label lblCurrentMonth;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.CheckBox cbIncludeTerminated;
        private System.Windows.Forms.CheckBox cbIncludeProInfo;
        private System.Windows.Forms.CheckBox cbRemoveNoOrderItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbShowInsufficientOnly;
        private System.Windows.Forms.CheckBox cbShowRawMaterial;
        private System.Windows.Forms.CheckBox cbSortByBalance;
        private System.Windows.Forms.CheckBox cbSortByToDOType;
        private System.Windows.Forms.CheckBox cbShowToProduceItem;
        private System.Windows.Forms.CheckBox cbShowToOrderItem;
        private System.Windows.Forms.CheckBox cbShowToAssemblyItem;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.CheckBox cbMainCustomerOnly;
    }
}