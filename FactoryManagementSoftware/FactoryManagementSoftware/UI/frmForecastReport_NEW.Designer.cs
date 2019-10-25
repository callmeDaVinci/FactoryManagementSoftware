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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.tlpForecastReport = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dgvForecastReport = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblUpdatedTime = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnFilterApply = new System.Windows.Forms.Button();
            this.btnExcelAll = new System.Windows.Forms.Button();
            this.cmbSoryBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAlertLevel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbForecastFrom = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbForecastTo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpOutFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpOutTo = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtNameSearch = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbDescending = new System.Windows.Forms.CheckBox();
            this.cbWithSubMat = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.tlpForecastReport.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForecastReport)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
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
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tlpForecastReport.Size = new System.Drawing.Size(1550, 829);
            this.tlpForecastReport.TabIndex = 164;
            this.tlpForecastReport.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpForecast_Paint);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 351F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel2.Controls.Add(this.btnExcelAll, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSearch, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExcel, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1544, 76);
            this.tableLayoutPanel2.TabIndex = 160;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 70);
            this.tableLayoutPanel1.TabIndex = 159;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 9);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 19);
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
            this.cmbCustomer.Size = new System.Drawing.Size(337, 31);
            this.cmbCustomer.TabIndex = 150;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1282, 39);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(124, 36);
            this.btnExcel.TabIndex = 157;
            this.btnExcel.Text = "EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(355, 39);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(124, 36);
            this.btnSearch.TabIndex = 142;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.BackColor = System.Drawing.Color.White;
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Location = new System.Drawing.Point(489, 39);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(125, 36);
            this.btnFilter.TabIndex = 151;
            this.btnFilter.Text = "MORE FILTERS ...";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // dgvForecastReport
            // 
            this.dgvForecastReport.AllowUserToAddRows = false;
            this.dgvForecastReport.AllowUserToDeleteRows = false;
            this.dgvForecastReport.AllowUserToOrderColumns = true;
            this.dgvForecastReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvForecastReport.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvForecastReport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvForecastReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForecastReport.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvForecastReport.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvForecastReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvForecastReport.GridColor = System.Drawing.Color.White;
            this.dgvForecastReport.Location = new System.Drawing.Point(4, 314);
            this.dgvForecastReport.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvForecastReport.Name = "dgvForecastReport";
            this.dgvForecastReport.ReadOnly = true;
            this.dgvForecastReport.RowHeadersVisible = false;
            this.dgvForecastReport.RowTemplate.Height = 60;
            this.dgvForecastReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvForecastReport.Size = new System.Drawing.Size(1542, 514);
            this.dgvForecastReport.TabIndex = 152;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 279F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1094F));
            this.tableLayoutPanel4.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnRefresh, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 270);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1544, 40);
            this.tableLayoutPanel4.TabIndex = 162;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(269, 19);
            this.label10.TabIndex = 153;
            this.label10.Text = "READY STOCK VERSUS FORECAST REPORT";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblUpdatedTime, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(324, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(322, 34);
            this.tableLayoutPanel3.TabIndex = 161;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 12);
            this.label9.TabIndex = 156;
            this.label9.Text = "LAST UPDATED:";
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
            this.btnRefresh.Location = new System.Drawing.Point(281, 2);
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
            this.gbFilter.Controls.Add(this.groupBox5);
            this.gbFilter.Controls.Add(this.groupBox4);
            this.gbFilter.Controls.Add(this.groupBox3);
            this.gbFilter.Controls.Add(this.groupBox2);
            this.gbFilter.Controls.Add(this.btnFilterApply);
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 85);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1544, 169);
            this.gbFilter.TabIndex = 144;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "FILTER";
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFilterApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterApply.ForeColor = System.Drawing.Color.White;
            this.btnFilterApply.Location = new System.Drawing.Point(1361, 113);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.Size = new System.Drawing.Size(124, 36);
            this.btnFilterApply.TabIndex = 145;
            this.btnFilterApply.Text = "APPLY";
            this.btnFilterApply.UseVisualStyleBackColor = false;
            // 
            // btnExcelAll
            // 
            this.btnExcelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelAll.BackColor = System.Drawing.Color.White;
            this.btnExcelAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcelAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelAll.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelAll.ForeColor = System.Drawing.Color.Black;
            this.btnExcelAll.Location = new System.Drawing.Point(1417, 39);
            this.btnExcelAll.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnExcelAll.Name = "btnExcelAll";
            this.btnExcelAll.Size = new System.Drawing.Size(124, 36);
            this.btnExcelAll.TabIndex = 165;
            this.btnExcelAll.Text = "EXCEL ALL";
            this.btnExcelAll.UseVisualStyleBackColor = false;
            this.btnExcelAll.Visible = false;
            // 
            // cmbSoryBy
            // 
            this.cmbSoryBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSoryBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSoryBy.FormattingEnabled = true;
            this.cmbSoryBy.Location = new System.Drawing.Point(16, 54);
            this.cmbSoryBy.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSoryBy.Name = "cmbSoryBy";
            this.cmbSoryBy.Size = new System.Drawing.Size(132, 25);
            this.cmbSoryBy.TabIndex = 147;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(99, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
            this.label1.TabIndex = 148;
            this.label1.Text = "CLEAR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 146;
            this.label2.Text = "SORT BY";
            // 
            // txtAlertLevel
            // 
            this.txtAlertLevel.Location = new System.Drawing.Point(169, 54);
            this.txtAlertLevel.Name = "txtAlertLevel";
            this.txtAlertLevel.Size = new System.Drawing.Size(109, 25);
            this.txtAlertLevel.TabIndex = 149;
            this.txtAlertLevel.Text = "0";
            this.txtAlertLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(165, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 19);
            this.label3.TabIndex = 150;
            this.label3.Text = "ALERT LEVEL";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbForecastTo);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbForecastFrom);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(8, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 115);
            this.groupBox2.TabIndex = 151;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FORECAST";
            // 
            // cmbForecastFrom
            // 
            this.cmbForecastFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbForecastFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbForecastFrom.FormattingEnabled = true;
            this.cmbForecastFrom.Location = new System.Drawing.Point(16, 54);
            this.cmbForecastFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbForecastFrom.Name = "cmbForecastFrom";
            this.cmbForecastFrom.Size = new System.Drawing.Size(132, 25);
            this.cmbForecastFrom.TabIndex = 149;
            this.cmbForecastFrom.SelectedIndexChanged += new System.EventHandler(this.cmbForecastFrom_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 19);
            this.label4.TabIndex = 148;
            this.label4.Text = "FROM";
            // 
            // cmbForecastTo
            // 
            this.cmbForecastTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbForecastTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbForecastTo.FormattingEnabled = true;
            this.cmbForecastTo.Location = new System.Drawing.Point(170, 54);
            this.cmbForecastTo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbForecastTo.Name = "cmbForecastTo";
            this.cmbForecastTo.Size = new System.Drawing.Size(132, 25);
            this.cmbForecastTo.TabIndex = 151;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(166, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 19);
            this.label6.TabIndex = 150;
            this.label6.Text = "TO";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpOutTo);
            this.groupBox3.Controls.Add(this.dtpOutFrom);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(344, 34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(317, 115);
            this.groupBox3.TabIndex = 152;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "OUT";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(156, 32);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 19);
            this.label7.TabIndex = 150;
            this.label7.Text = "TO";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(2, 32);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 19);
            this.label11.TabIndex = 148;
            this.label11.Text = "FROM";
            // 
            // dtpOutFrom
            // 
            this.dtpOutFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOutFrom.Location = new System.Drawing.Point(6, 54);
            this.dtpOutFrom.Name = "dtpOutFrom";
            this.dtpOutFrom.Size = new System.Drawing.Size(132, 25);
            this.dtpOutFrom.TabIndex = 153;
            // 
            // dtpOutTo
            // 
            this.dtpOutTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOutTo.Location = new System.Drawing.Point(160, 54);
            this.dtpOutTo.Name = "dtpOutTo";
            this.dtpOutTo.Size = new System.Drawing.Size(132, 25);
            this.dtpOutTo.TabIndex = 154;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtNameSearch);
            this.groupBox4.Location = new System.Drawing.Point(1011, 34);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(313, 115);
            this.groupBox4.TabIndex = 153;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ITEM";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbDescending);
            this.groupBox5.Controls.Add(this.cmbSoryBy);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtAlertLevel);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(677, 34);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(317, 115);
            this.groupBox5.TabIndex = 154;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "SORTING";
            // 
            // txtNameSearch
            // 
            this.txtNameSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameSearch.Location = new System.Drawing.Point(11, 54);
            this.txtNameSearch.Name = "txtNameSearch";
            this.txtNameSearch.Size = new System.Drawing.Size(286, 25);
            this.txtNameSearch.TabIndex = 155;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(7, 33);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 19);
            this.label12.TabIndex = 155;
            this.label12.Text = "NAME SEARCH";
            // 
            // cbDescending
            // 
            this.cbDescending.AutoSize = true;
            this.cbDescending.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDescending.Location = new System.Drawing.Point(16, 82);
            this.cbDescending.Name = "cbDescending";
            this.cbDescending.Size = new System.Drawing.Size(102, 23);
            this.cbDescending.TabIndex = 151;
            this.cbDescending.Text = "Descending";
            this.cbDescending.UseVisualStyleBackColor = true;
            // 
            // cbWithSubMat
            // 
            this.cbWithSubMat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWithSubMat.AutoSize = true;
            this.cbWithSubMat.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbWithSubMat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbWithSubMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWithSubMat.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbWithSubMat.Location = new System.Drawing.Point(203, 8);
            this.cbWithSubMat.Name = "cbWithSubMat";
            this.cbWithSubMat.Size = new System.Drawing.Size(18, 17);
            this.cbWithSubMat.TabIndex = 156;
            this.cbWithSubMat.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbWithSubMat.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel5.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbWithSubMat, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(339, 28);
            this.tableLayoutPanel5.TabIndex = 165;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(228, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 19);
            this.label5.TabIndex = 165;
            this.label5.Text = "WITH SUB MAT.";
            this.label5.Click += new System.EventHandler(this.label5_Click);
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
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmForecastReport_NEW_Load);
            this.tlpForecastReport.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvForecastReport)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
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
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DataGridView dgvForecastReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblUpdatedTime;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Button btnFilterApply;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label5;
    }
}