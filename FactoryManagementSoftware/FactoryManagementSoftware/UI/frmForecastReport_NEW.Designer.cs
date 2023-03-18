﻿namespace FactoryManagementSoftware.UI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.tlpForecastReport = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSearchClear = new System.Windows.Forms.Label();
            this.lblResultNo = new System.Windows.Forms.Label();
            this.lblSearchInfo = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.txtItemSearch = new System.Windows.Forms.TextBox();
            this.btnPreviousSearchResult = new System.Windows.Forms.Button();
            this.btnNextSearchResult = new System.Windows.Forms.Button();
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
            this.cbDeductEstimate = new System.Windows.Forms.CheckBox();
            this.cbIncludeMacRecord = new System.Windows.Forms.CheckBox();
            this.cbShowProDayNeeded = new System.Windows.Forms.CheckBox();
            this.cbSpecialTypeColorMode = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRepeatedColorMode = new System.Windows.Forms.CheckBox();
            this.txtInactiveMonthsThreshold = new System.Windows.Forms.TextBox();
            this.cbRemoveNoDeliveredItem = new System.Windows.Forms.CheckBox();
            this.cbIncludeTerminated = new System.Windows.Forms.CheckBox();
            this.cbIncludeProInfo = new System.Windows.Forms.CheckBox();
            this.cbRemoveNoOrderItem = new System.Windows.Forms.CheckBox();
            this.cbWithSubMat = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbShowInsufficientOnly = new System.Windows.Forms.CheckBox();
            this.cbShowToOrderItem = new System.Windows.Forms.CheckBox();
            this.cbShowToAssemblyItem = new System.Windows.Forms.CheckBox();
            this.cbShowToProduceItem = new System.Windows.Forms.CheckBox();
            this.cbSortByBalance = new System.Windows.Forms.CheckBox();
            this.cbSortByToDOType = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCurrentMonth = new System.Windows.Forms.Label();
            this.cmbForecastTo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbForecastFrom = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
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
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmbSummaryMonthBalSort = new System.Windows.Forms.ComboBox();
            this.tlpForecastReport.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
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
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpForecastReport
            // 
            this.tlpForecastReport.ColumnCount = 1;
            this.tlpForecastReport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForecastReport.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpForecastReport.Controls.Add(this.dgvForecastReport, 0, 3);
            this.tlpForecastReport.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tlpForecastReport.Controls.Add(this.gbFilter, 0, 1);
            this.tlpForecastReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpForecastReport.Location = new System.Drawing.Point(15, 15);
            this.tlpForecastReport.Margin = new System.Windows.Forms.Padding(15);
            this.tlpForecastReport.Name = "tlpForecastReport";
            this.tlpForecastReport.RowCount = 4;
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForecastReport.Size = new System.Drawing.Size(1318, 691);
            this.tlpForecastReport.TabIndex = 164;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 272F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExcelAll, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSummary, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFullReport, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExcel, 7, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1318, 80);
            this.tableLayoutPanel2.TabIndex = 160;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(667, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(300, 80);
            this.tableLayoutPanel7.TabIndex = 166;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 3;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel9.Controls.Add(this.lblSearchClear, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.lblResultNo, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.lblSearchInfo, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(300, 44);
            this.tableLayoutPanel9.TabIndex = 166;
            // 
            // lblSearchClear
            // 
            this.lblSearchClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchClear.AutoSize = true;
            this.lblSearchClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSearchClear.Font = new System.Drawing.Font("Segoe UI", 6F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchClear.ForeColor = System.Drawing.Color.Blue;
            this.lblSearchClear.Location = new System.Drawing.Point(185, 31);
            this.lblSearchClear.Margin = new System.Windows.Forms.Padding(0, 0, 6, 2);
            this.lblSearchClear.Name = "lblSearchClear";
            this.lblSearchClear.Size = new System.Drawing.Size(29, 11);
            this.lblSearchClear.TabIndex = 169;
            this.lblSearchClear.Text = "CLEAR";
            this.lblSearchClear.Visible = false;
            this.lblSearchClear.Click += new System.EventHandler(this.label9_Click);
            // 
            // lblResultNo
            // 
            this.lblResultNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResultNo.AutoSize = true;
            this.lblResultNo.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultNo.Location = new System.Drawing.Point(280, 31);
            this.lblResultNo.Margin = new System.Windows.Forms.Padding(0, 0, 6, 2);
            this.lblResultNo.Name = "lblResultNo";
            this.lblResultNo.Size = new System.Drawing.Size(14, 11);
            this.lblResultNo.TabIndex = 165;
            this.lblResultNo.Text = "#0";
            // 
            // lblSearchInfo
            // 
            this.lblSearchInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSearchInfo.AutoSize = true;
            this.lblSearchInfo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchInfo.Location = new System.Drawing.Point(0, 29);
            this.lblSearchInfo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblSearchInfo.Name = "lblSearchInfo";
            this.lblSearchInfo.Size = new System.Drawing.Size(0, 13);
            this.lblSearchInfo.TabIndex = 156;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel8.Controls.Add(this.txtItemSearch, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnPreviousSearchResult, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnNextSearchResult, 2, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 44);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(300, 36);
            this.tableLayoutPanel8.TabIndex = 167;
            // 
            // txtItemSearch
            // 
            this.txtItemSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemSearch.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtItemSearch.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtItemSearch.Location = new System.Drawing.Point(2, 2);
            this.txtItemSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtItemSearch.Name = "txtItemSearch";
            this.txtItemSearch.Size = new System.Drawing.Size(216, 22);
            this.txtItemSearch.TabIndex = 155;
            this.txtItemSearch.Text = "Search";
            this.txtItemSearch.TextChanged += new System.EventHandler(this.txtNameSearch_TextChanged);
            this.txtItemSearch.Enter += new System.EventHandler(this.txtNameSearch_Enter);
            this.txtItemSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNameSearch_KeyDown);
            this.txtItemSearch.Leave += new System.EventHandler(this.txtNameSearch_Leave);
            // 
            // btnPreviousSearchResult
            // 
            this.btnPreviousSearchResult.BackColor = System.Drawing.Color.White;
            this.btnPreviousSearchResult.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviousSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPreviousSearchResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviousSearchResult.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnPreviousSearchResult.ForeColor = System.Drawing.Color.Black;
            this.btnPreviousSearchResult.Image = global::FactoryManagementSoftware.Properties.Resources.icons8_chevron_up_16;
            this.btnPreviousSearchResult.Location = new System.Drawing.Point(224, 0);
            this.btnPreviousSearchResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 1);
            this.btnPreviousSearchResult.Name = "btnPreviousSearchResult";
            this.btnPreviousSearchResult.Size = new System.Drawing.Size(32, 35);
            this.btnPreviousSearchResult.TabIndex = 167;
            this.btnPreviousSearchResult.UseVisualStyleBackColor = false;
            this.btnPreviousSearchResult.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnNextSearchResult
            // 
            this.btnNextSearchResult.BackColor = System.Drawing.Color.White;
            this.btnNextSearchResult.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNextSearchResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextSearchResult.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.btnNextSearchResult.ForeColor = System.Drawing.Color.Black;
            this.btnNextSearchResult.Image = global::FactoryManagementSoftware.Properties.Resources.icons8_chevron_down_16;
            this.btnNextSearchResult.Location = new System.Drawing.Point(264, 0);
            this.btnNextSearchResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 1);
            this.btnNextSearchResult.Name = "btnNextSearchResult";
            this.btnNextSearchResult.Size = new System.Drawing.Size(32, 35);
            this.btnNextSearchResult.TabIndex = 166;
            this.btnNextSearchResult.UseVisualStyleBackColor = false;
            this.btnNextSearchResult.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnExcelAll
            // 
            this.btnExcelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelAll.BackColor = System.Drawing.Color.White;
            this.btnExcelAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcelAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelAll.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcelAll.ForeColor = System.Drawing.Color.Black;
            this.btnExcelAll.Location = new System.Drawing.Point(1183, 43);
            this.btnExcelAll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 1);
            this.btnExcelAll.Name = "btnExcelAll";
            this.btnExcelAll.Size = new System.Drawing.Size(1, 36);
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
            this.btnSummary.Location = new System.Drawing.Point(406, 43);
            this.btnSummary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 1);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(122, 36);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(272, 80);
            this.tableLayoutPanel1.TabIndex = 159;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.cbMainCustomerOnly, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(272, 41);
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
            this.cbMainCustomerOnly.Location = new System.Drawing.Point(139, 24);
            this.cbMainCustomerOnly.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.cbMainCustomerOnly.Name = "cbMainCustomerOnly";
            this.cbMainCustomerOnly.Size = new System.Drawing.Size(131, 17);
            this.cbMainCustomerOnly.TabIndex = 167;
            this.cbMainCustomerOnly.Text = "Main Customer Only";
            this.cbMainCustomerOnly.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbMainCustomerOnly.UseVisualStyleBackColor = true;
            this.cbMainCustomerOnly.Visible = false;
            this.cbMainCustomerOnly.CheckedChanged += new System.EventHandler(this.cbMainCustomerOnly_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 28);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 149;
            this.label8.Text = "CUSTOMER";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.Enabled = false;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(4, 41);
            this.cmbCustomer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(264, 21);
            this.cmbCustomer.TabIndex = 150;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            this.cmbCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCustomer_KeyDown);
            // 
            // btnFullReport
            // 
            this.btnFullReport.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFullReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFullReport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFullReport.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFullReport.ForeColor = System.Drawing.Color.White;
            this.btnFullReport.Location = new System.Drawing.Point(276, 43);
            this.btnFullReport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 1);
            this.btnFullReport.Name = "btnFullReport";
            this.btnFullReport.Size = new System.Drawing.Size(122, 36);
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
            this.btnFilter.Location = new System.Drawing.Point(536, 43);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 1);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(122, 36);
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
            this.btnExcel.Location = new System.Drawing.Point(1193, 43);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 0, 0, 1);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(125, 36);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvForecastReport.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvForecastReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvForecastReport.GridColor = System.Drawing.Color.White;
            this.dgvForecastReport.Location = new System.Drawing.Point(3, 337);
            this.dgvForecastReport.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvForecastReport.Name = "dgvForecastReport";
            this.dgvForecastReport.RowHeadersVisible = false;
            this.dgvForecastReport.RowHeadersWidth = 51;
            this.dgvForecastReport.RowTemplate.Height = 60;
            this.dgvForecastReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvForecastReport.Size = new System.Drawing.Size(1312, 353);
            this.dgvForecastReport.TabIndex = 152;
            this.dgvForecastReport.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvForecastReport_CellContentClick);
            this.dgvForecastReport.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvForecastReport_CellEndEdit);
            this.dgvForecastReport.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvForecastReport_CellFormatting);
            this.dgvForecastReport.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvForecastReport_CellMouseDown);
            this.dgvForecastReport.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvForecastReport_CellValueChanged);
            this.dgvForecastReport.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvForecastReport_EditingControlShowing);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 168F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1031F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel4.Controls.Add(this.lblForecastType, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnRefresh, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2, 302);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1314, 32);
            this.tableLayoutPanel4.TabIndex = 162;
            // 
            // lblForecastType
            // 
            this.lblForecastType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblForecastType.AutoSize = true;
            this.lblForecastType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForecastType.Location = new System.Drawing.Point(3, 9);
            this.lblForecastType.Name = "lblForecastType";
            this.lblForecastType.Size = new System.Drawing.Size(105, 13);
            this.lblForecastType.TabIndex = 153;
            this.lblForecastType.Text = "FORECAST REPORT";
            this.lblForecastType.Click += new System.EventHandler(this.lblForecastType_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblLastUpdated, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblUpdatedTime, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(170, 2);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(110, 28);
            this.tableLayoutPanel3.TabIndex = 161;
            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLastUpdated.AutoSize = true;
            this.lblLastUpdated.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastUpdated.Location = new System.Drawing.Point(2, 1);
            this.lblLastUpdated.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.Size = new System.Drawing.Size(60, 11);
            this.lblLastUpdated.TabIndex = 156;
            this.lblLastUpdated.Text = "LAST UPDATED:";
            this.lblLastUpdated.Visible = false;
            // 
            // lblUpdatedTime
            // 
            this.lblUpdatedTime.AutoSize = true;
            this.lblUpdatedTime.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdatedTime.Location = new System.Drawing.Point(2, 12);
            this.lblUpdatedTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdatedTime.Name = "lblUpdatedTime";
            this.lblUpdatedTime.Size = new System.Drawing.Size(99, 11);
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
            this.btnRefresh.Location = new System.Drawing.Point(285, 2);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(28, 27);
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
            this.gbFilter.Location = new System.Drawing.Point(2, 82);
            this.gbFilter.Margin = new System.Windows.Forms.Padding(2);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Padding = new System.Windows.Forms.Padding(2);
            this.gbFilter.Size = new System.Drawing.Size(1314, 216);
            this.gbFilter.TabIndex = 144;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "FILTER";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 5;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 181F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 189F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 248F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 228F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 464F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel6.Controls.Add(this.groupBox6, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox1, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox5, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(2, 16);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1310, 198);
            this.tableLayoutPanel6.TabIndex = 165;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbDeductEstimate);
            this.groupBox6.Controls.Add(this.cbIncludeMacRecord);
            this.groupBox6.Controls.Add(this.cbShowProDayNeeded);
            this.groupBox6.Controls.Add(this.cbSpecialTypeColorMode);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.cbRepeatedColorMode);
            this.groupBox6.Controls.Add(this.txtInactiveMonthsThreshold);
            this.groupBox6.Controls.Add(this.cbRemoveNoDeliveredItem);
            this.groupBox6.Controls.Add(this.cbIncludeTerminated);
            this.groupBox6.Controls.Add(this.cbIncludeProInfo);
            this.groupBox6.Controls.Add(this.cbRemoveNoOrderItem);
            this.groupBox6.Controls.Add(this.cbWithSubMat);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox6.Location = new System.Drawing.Point(848, 2);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.MinimumSize = new System.Drawing.Size(101, 85);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(460, 194);
            this.groupBox6.TabIndex = 166;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "OTHER";
            // 
            // cbDeductEstimate
            // 
            this.cbDeductEstimate.AutoSize = true;
            this.cbDeductEstimate.Checked = true;
            this.cbDeductEstimate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeductEstimate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDeductEstimate.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbDeductEstimate.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbDeductEstimate.Location = new System.Drawing.Point(258, 141);
            this.cbDeductEstimate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbDeductEstimate.Name = "cbDeductEstimate";
            this.cbDeductEstimate.Size = new System.Drawing.Size(159, 16);
            this.cbDeductEstimate.TabIndex = 172;
            this.cbDeductEstimate.Text = "Deduct Estimate If No Forecast";
            this.cbDeductEstimate.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbDeductEstimate.UseVisualStyleBackColor = true;
            // 
            // cbIncludeMacRecord
            // 
            this.cbIncludeMacRecord.AutoSize = true;
            this.cbIncludeMacRecord.Checked = true;
            this.cbIncludeMacRecord.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeMacRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbIncludeMacRecord.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeMacRecord.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeMacRecord.Location = new System.Drawing.Point(258, 109);
            this.cbIncludeMacRecord.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbIncludeMacRecord.Name = "cbIncludeMacRecord";
            this.cbIncludeMacRecord.Size = new System.Drawing.Size(130, 17);
            this.cbIncludeMacRecord.TabIndex = 171;
            this.cbIncludeMacRecord.Text = "Include Mac. Record";
            this.cbIncludeMacRecord.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeMacRecord.UseVisualStyleBackColor = true;
            // 
            // cbShowProDayNeeded
            // 
            this.cbShowProDayNeeded.AutoSize = true;
            this.cbShowProDayNeeded.Checked = true;
            this.cbShowProDayNeeded.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowProDayNeeded.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowProDayNeeded.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowProDayNeeded.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowProDayNeeded.Location = new System.Drawing.Point(258, 84);
            this.cbShowProDayNeeded.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbShowProDayNeeded.Name = "cbShowProDayNeeded";
            this.cbShowProDayNeeded.Size = new System.Drawing.Size(143, 17);
            this.cbShowProDayNeeded.TabIndex = 170;
            this.cbShowProDayNeeded.Text = "Show Pro. Day Needed";
            this.cbShowProDayNeeded.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowProDayNeeded.UseVisualStyleBackColor = true;
            // 
            // cbSpecialTypeColorMode
            // 
            this.cbSpecialTypeColorMode.AutoSize = true;
            this.cbSpecialTypeColorMode.Checked = true;
            this.cbSpecialTypeColorMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSpecialTypeColorMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSpecialTypeColorMode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSpecialTypeColorMode.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSpecialTypeColorMode.Location = new System.Drawing.Point(258, 54);
            this.cbSpecialTypeColorMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbSpecialTypeColorMode.Name = "cbSpecialTypeColorMode";
            this.cbSpecialTypeColorMode.Size = new System.Drawing.Size(136, 17);
            this.cbSpecialTypeColorMode.TabIndex = 169;
            this.cbSpecialTypeColorMode.Text = "Special Type Coloring";
            this.cbSpecialTypeColorMode.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSpecialTypeColorMode.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(197, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 11);
            this.label5.TabIndex = 169;
            this.label5.Text = "months";
            // 
            // cbRepeatedColorMode
            // 
            this.cbRepeatedColorMode.AutoSize = true;
            this.cbRepeatedColorMode.Checked = true;
            this.cbRepeatedColorMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRepeatedColorMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRepeatedColorMode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRepeatedColorMode.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRepeatedColorMode.Location = new System.Drawing.Point(258, 25);
            this.cbRepeatedColorMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbRepeatedColorMode.Name = "cbRepeatedColorMode";
            this.cbRepeatedColorMode.Size = new System.Drawing.Size(149, 17);
            this.cbRepeatedColorMode.TabIndex = 168;
            this.cbRepeatedColorMode.Text = "Repeated Row Coloring";
            this.cbRepeatedColorMode.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRepeatedColorMode.UseVisualStyleBackColor = true;
            // 
            // txtInactiveMonthsThreshold
            // 
            this.txtInactiveMonthsThreshold.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.txtInactiveMonthsThreshold.Location = new System.Drawing.Point(154, 139);
            this.txtInactiveMonthsThreshold.Margin = new System.Windows.Forms.Padding(2);
            this.txtInactiveMonthsThreshold.Name = "txtInactiveMonthsThreshold";
            this.txtInactiveMonthsThreshold.Size = new System.Drawing.Size(38, 18);
            this.txtInactiveMonthsThreshold.TabIndex = 169;
            this.txtInactiveMonthsThreshold.Text = "3";
            this.txtInactiveMonthsThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbRemoveNoDeliveredItem
            // 
            this.cbRemoveNoDeliveredItem.AutoSize = true;
            this.cbRemoveNoDeliveredItem.Checked = true;
            this.cbRemoveNoDeliveredItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRemoveNoDeliveredItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRemoveNoDeliveredItem.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbRemoveNoDeliveredItem.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRemoveNoDeliveredItem.Location = new System.Drawing.Point(11, 139);
            this.cbRemoveNoDeliveredItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbRemoveNoDeliveredItem.Name = "cbRemoveNoDeliveredItem";
            this.cbRemoveNoDeliveredItem.Size = new System.Drawing.Size(119, 16);
            this.cbRemoveNoDeliveredItem.TabIndex = 168;
            this.cbRemoveNoDeliveredItem.Text = "Remove No Delivered";
            this.cbRemoveNoDeliveredItem.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRemoveNoDeliveredItem.UseVisualStyleBackColor = true;
            this.cbRemoveNoDeliveredItem.CheckedChanged += new System.EventHandler(this.cbRemoveNoDeliveredItem_CheckedChanged);
            // 
            // cbIncludeTerminated
            // 
            this.cbIncludeTerminated.AutoSize = true;
            this.cbIncludeTerminated.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbIncludeTerminated.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeTerminated.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeTerminated.Location = new System.Drawing.Point(11, 25);
            this.cbIncludeTerminated.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbIncludeTerminated.Name = "cbIncludeTerminated";
            this.cbIncludeTerminated.Size = new System.Drawing.Size(149, 17);
            this.cbIncludeTerminated.TabIndex = 165;
            this.cbIncludeTerminated.Text = "Include Terminated Item";
            this.cbIncludeTerminated.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeTerminated.UseVisualStyleBackColor = true;
            this.cbIncludeTerminated.CheckedChanged += new System.EventHandler(this.cbIncludeTerminated_CheckedChanged);
            // 
            // cbIncludeProInfo
            // 
            this.cbIncludeProInfo.AutoSize = true;
            this.cbIncludeProInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbIncludeProInfo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeProInfo.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeProInfo.Location = new System.Drawing.Point(11, 54);
            this.cbIncludeProInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbIncludeProInfo.Name = "cbIncludeProInfo";
            this.cbIncludeProInfo.Size = new System.Drawing.Size(108, 17);
            this.cbIncludeProInfo.TabIndex = 166;
            this.cbIncludeProInfo.Text = "Include Pro Info";
            this.cbIncludeProInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeProInfo.UseVisualStyleBackColor = true;
            this.cbIncludeProInfo.CheckedChanged += new System.EventHandler(this.cbIncludeProInfo_CheckedChanged);
            // 
            // cbRemoveNoOrderItem
            // 
            this.cbRemoveNoOrderItem.AutoSize = true;
            this.cbRemoveNoOrderItem.Checked = true;
            this.cbRemoveNoOrderItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRemoveNoOrderItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRemoveNoOrderItem.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRemoveNoOrderItem.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRemoveNoOrderItem.Location = new System.Drawing.Point(11, 109);
            this.cbRemoveNoOrderItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbRemoveNoOrderItem.Name = "cbRemoveNoOrderItem";
            this.cbRemoveNoOrderItem.Size = new System.Drawing.Size(155, 17);
            this.cbRemoveNoOrderItem.TabIndex = 167;
            this.cbRemoveNoOrderItem.Text = "Remove No Forecast Item";
            this.cbRemoveNoOrderItem.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRemoveNoOrderItem.UseVisualStyleBackColor = true;
            this.cbRemoveNoOrderItem.CheckedChanged += new System.EventHandler(this.cbRemoveForecastInvalidItem_CheckedChanged);
            // 
            // cbWithSubMat
            // 
            this.cbWithSubMat.AutoSize = true;
            this.cbWithSubMat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbWithSubMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWithSubMat.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbWithSubMat.Location = new System.Drawing.Point(11, 84);
            this.cbWithSubMat.Margin = new System.Windows.Forms.Padding(2);
            this.cbWithSubMat.Name = "cbWithSubMat";
            this.cbWithSubMat.Size = new System.Drawing.Size(128, 17);
            this.cbWithSubMat.TabIndex = 156;
            this.cbWithSubMat.Text = "Show Sub Materials";
            this.cbWithSubMat.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbWithSubMat.UseVisualStyleBackColor = true;
            this.cbWithSubMat.CheckedChanged += new System.EventHandler(this.cbWithSubMat_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSummaryMonthBalSort);
            this.groupBox1.Controls.Add(this.cbShowInsufficientOnly);
            this.groupBox1.Controls.Add(this.cbShowToOrderItem);
            this.groupBox1.Controls.Add(this.cbShowToAssemblyItem);
            this.groupBox1.Controls.Add(this.cbShowToProduceItem);
            this.groupBox1.Controls.Add(this.cbSortByBalance);
            this.groupBox1.Controls.Add(this.cbSortByToDOType);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(620, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.MinimumSize = new System.Drawing.Size(101, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(224, 194);
            this.groupBox1.TabIndex = 165;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SUMMARY LIST";
            // 
            // cbShowInsufficientOnly
            // 
            this.cbShowInsufficientOnly.AutoSize = true;
            this.cbShowInsufficientOnly.Checked = true;
            this.cbShowInsufficientOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowInsufficientOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowInsufficientOnly.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowInsufficientOnly.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowInsufficientOnly.Location = new System.Drawing.Point(14, 160);
            this.cbShowInsufficientOnly.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbShowInsufficientOnly.Name = "cbShowInsufficientOnly";
            this.cbShowInsufficientOnly.Size = new System.Drawing.Size(123, 16);
            this.cbShowInsufficientOnly.TabIndex = 171;
            this.cbShowInsufficientOnly.Text = "Show Insufficient Only";
            this.cbShowInsufficientOnly.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowInsufficientOnly.UseVisualStyleBackColor = true;
            // 
            // cbShowToOrderItem
            // 
            this.cbShowToOrderItem.AutoSize = true;
            this.cbShowToOrderItem.Checked = true;
            this.cbShowToOrderItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowToOrderItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowToOrderItem.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowToOrderItem.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowToOrderItem.Location = new System.Drawing.Point(14, 134);
            this.cbShowToOrderItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbShowToOrderItem.Name = "cbShowToOrderItem";
            this.cbShowToOrderItem.Size = new System.Drawing.Size(116, 16);
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
            this.cbShowToAssemblyItem.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowToAssemblyItem.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowToAssemblyItem.Location = new System.Drawing.Point(14, 106);
            this.cbShowToAssemblyItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbShowToAssemblyItem.Name = "cbShowToAssemblyItem";
            this.cbShowToAssemblyItem.Size = new System.Drawing.Size(132, 16);
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
            this.cbShowToProduceItem.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowToProduceItem.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowToProduceItem.Location = new System.Drawing.Point(14, 78);
            this.cbShowToProduceItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbShowToProduceItem.Name = "cbShowToProduceItem";
            this.cbShowToProduceItem.Size = new System.Drawing.Size(128, 16);
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
            this.cbSortByBalance.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbSortByBalance.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSortByBalance.Location = new System.Drawing.Point(14, 50);
            this.cbSortByBalance.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbSortByBalance.Name = "cbSortByBalance";
            this.cbSortByBalance.Size = new System.Drawing.Size(92, 16);
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
            this.cbSortByToDOType.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbSortByToDOType.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSortByToDOType.Location = new System.Drawing.Point(14, 25);
            this.cbSortByToDOType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbSortByToDOType.Name = "cbSortByToDOType";
            this.cbSortByToDOType.Size = new System.Drawing.Size(109, 16);
            this.cbSortByToDOType.TabIndex = 166;
            this.cbSortByToDOType.Text = "Sort By To DO Type";
            this.cbSortByToDOType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSortByToDOType.UseVisualStyleBackColor = true;
            this.cbSortByToDOType.CheckedChanged += new System.EventHandler(this.cbSortByToDOType_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCurrentMonth);
            this.groupBox2.Controls.Add(this.cmbForecastTo);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbForecastFrom);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(2, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(177, 194);
            this.groupBox2.TabIndex = 151;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FORECAST PERIOD";
            // 
            // lblCurrentMonth
            // 
            this.lblCurrentMonth.AutoSize = true;
            this.lblCurrentMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCurrentMonth.Font = new System.Drawing.Font("Segoe UI", 7F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))));
            this.lblCurrentMonth.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrentMonth.Location = new System.Drawing.Point(61, 23);
            this.lblCurrentMonth.Name = "lblCurrentMonth";
            this.lblCurrentMonth.Size = new System.Drawing.Size(70, 12);
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
            this.cmbForecastTo.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.cmbForecastTo.FormattingEnabled = true;
            this.cmbForecastTo.Location = new System.Drawing.Point(47, 70);
            this.cmbForecastTo.Name = "cmbForecastTo";
            this.cmbForecastTo.Size = new System.Drawing.Size(98, 19);
            this.cmbForecastTo.TabIndex = 151;
            this.cmbForecastTo.SelectedIndexChanged += new System.EventHandler(this.cmbForecastTo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 150;
            this.label6.Text = "TO";
            // 
            // cmbForecastFrom
            // 
            this.cmbForecastFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbForecastFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbForecastFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForecastFrom.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.cmbForecastFrom.FormattingEnabled = true;
            this.cmbForecastFrom.Location = new System.Drawing.Point(47, 40);
            this.cmbForecastFrom.Name = "cmbForecastFrom";
            this.cmbForecastFrom.Size = new System.Drawing.Size(98, 19);
            this.cmbForecastFrom.TabIndex = 149;
            this.cmbForecastFrom.SelectedIndexChanged += new System.EventHandler(this.cmbForecastFrom_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 148;
            this.label4.Text = "FROM";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Controls.Add(this.cbDescending);
            this.groupBox5.Controls.Add(this.cmbSoryBy);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtAlertLevel);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox5.Location = new System.Drawing.Point(372, 2);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(244, 194);
            this.groupBox5.TabIndex = 154;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "SORTING";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.checkBox1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.checkBox1.Location = new System.Drawing.Point(12, 134);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(123, 16);
            this.checkBox1.TabIndex = 172;
            this.checkBox1.Text = "Show Insufficient Only";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cbDescending
            // 
            this.cbDescending.AutoSize = true;
            this.cbDescending.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDescending.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.cbDescending.Location = new System.Drawing.Point(12, 69);
            this.cbDescending.Margin = new System.Windows.Forms.Padding(2);
            this.cbDescending.Name = "cbDescending";
            this.cbDescending.Size = new System.Drawing.Size(66, 15);
            this.cbDescending.TabIndex = 151;
            this.cbDescending.Text = "descending";
            this.cbDescending.UseVisualStyleBackColor = true;
            this.cbDescending.CheckedChanged += new System.EventHandler(this.cbDescending_CheckedChanged);
            // 
            // cmbSoryBy
            // 
            this.cmbSoryBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSoryBy.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.cmbSoryBy.FormattingEnabled = true;
            this.cmbSoryBy.Location = new System.Drawing.Point(12, 44);
            this.cmbSoryBy.Name = "cmbSoryBy";
            this.cmbSoryBy.Size = new System.Drawing.Size(118, 19);
            this.cmbSoryBy.TabIndex = 147;
            this.cmbSoryBy.SelectedIndexChanged += new System.EventHandler(this.cmbSoryBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 146;
            this.label2.Text = "SORT BY";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(100, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 148;
            this.label1.Text = "clear";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtAlertLevel
            // 
            this.txtAlertLevel.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.txtAlertLevel.Location = new System.Drawing.Point(141, 43);
            this.txtAlertLevel.Margin = new System.Windows.Forms.Padding(2);
            this.txtAlertLevel.Name = "txtAlertLevel";
            this.txtAlertLevel.Size = new System.Drawing.Size(86, 18);
            this.txtAlertLevel.TabIndex = 149;
            this.txtAlertLevel.Text = "0";
            this.txtAlertLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAlertLevel.TextChanged += new System.EventHandler(this.txtAlertLevel_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(138, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
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
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(183, 2);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(185, 194);
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
            this.lblChangeDate.Location = new System.Drawing.Point(52, 101);
            this.lblChangeDate.Name = "lblChangeDate";
            this.lblChangeDate.Size = new System.Drawing.Size(85, 13);
            this.lblChangeDate.TabIndex = 165;
            this.lblChangeDate.Text = "PMMA Date Edit";
            this.lblChangeDate.Visible = false;
            this.lblChangeDate.Click += new System.EventHandler(this.lblChangeDate_Click);
            // 
            // dtpOutTo
            // 
            this.dtpOutTo.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.dtpOutTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOutTo.Location = new System.Drawing.Point(52, 68);
            this.dtpOutTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtpOutTo.Name = "dtpOutTo";
            this.dtpOutTo.Size = new System.Drawing.Size(129, 18);
            this.dtpOutTo.TabIndex = 154;
            this.dtpOutTo.ValueChanged += new System.EventHandler(this.dtpOutTo_ValueChanged);
            // 
            // dtpOutFrom
            // 
            this.dtpOutFrom.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.dtpOutFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOutFrom.Location = new System.Drawing.Point(54, 38);
            this.dtpOutFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtpOutFrom.Name = "dtpOutFrom";
            this.dtpOutFrom.Size = new System.Drawing.Size(127, 18);
            this.dtpOutFrom.TabIndex = 153;
            this.dtpOutFrom.ValueChanged += new System.EventHandler(this.dtpOutFrom_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(26, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 150;
            this.label7.Text = "TO";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(11, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 148;
            this.label11.Text = "FROM";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.tlpForecastReport, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 721F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(1348, 721);
            this.tableLayoutPanel10.TabIndex = 168;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // cmbSummaryMonthBalSort
            // 
            this.cmbSummaryMonthBalSort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSummaryMonthBalSort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSummaryMonthBalSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSummaryMonthBalSort.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.cmbSummaryMonthBalSort.FormattingEnabled = true;
            this.cmbSummaryMonthBalSort.Location = new System.Drawing.Point(106, 48);
            this.cmbSummaryMonthBalSort.Name = "cmbSummaryMonthBalSort";
            this.cmbSummaryMonthBalSort.Size = new System.Drawing.Size(62, 19);
            this.cmbSummaryMonthBalSort.TabIndex = 169;
            this.cmbSummaryMonthBalSort.SelectedIndexChanged += new System.EventHandler(this.cmbSummaryMonthBalSort_SelectedIndexChanged);
            // 
            // frmForecastReport_NEW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.tableLayoutPanel10);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "frmForecastReport_NEW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmForecastReport_NEW";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmForecastReport_NEW_FormClosed);
            this.Load += new System.EventHandler(this.frmForecastReport_NEW_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmForecastReport_NEW_KeyDown);
            this.tlpForecastReport.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
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
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txtItemSearch;
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
        private System.Windows.Forms.CheckBox cbSortByBalance;
        private System.Windows.Forms.CheckBox cbSortByToDOType;
        private System.Windows.Forms.CheckBox cbShowToProduceItem;
        private System.Windows.Forms.CheckBox cbShowToOrderItem;
        private System.Windows.Forms.CheckBox cbShowToAssemblyItem;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.CheckBox cbMainCustomerOnly;
        private System.Windows.Forms.Label lblSearchInfo;
        private System.Windows.Forms.Button btnPreviousSearchResult;
        private System.Windows.Forms.Button btnNextSearchResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label lblResultNo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.CheckBox cbRepeatedColorMode;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.CheckBox cbRemoveNoDeliveredItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInactiveMonthsThreshold;
        private System.Windows.Forms.CheckBox cbShowProDayNeeded;
        private System.Windows.Forms.CheckBox cbSpecialTypeColorMode;
        private System.Windows.Forms.CheckBox cbIncludeMacRecord;
        private System.Windows.Forms.CheckBox cbDeductEstimate;
        private System.Windows.Forms.CheckBox cbShowInsufficientOnly;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lblSearchClear;
        private System.Windows.Forms.ComboBox cmbSummaryMonthBalSort;
    }
}