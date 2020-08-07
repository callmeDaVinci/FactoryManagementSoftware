namespace FactoryManagementSoftware.UI
{
    partial class frmSBBDeliveredReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpReport = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDateType = new System.Windows.Forms.Label();
            this.cmbDateType = new System.Windows.Forms.ComboBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalBag = new System.Windows.Forms.Label();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMonthTo = new System.Windows.Forms.Label();
            this.cmbMonthTo = new System.Windows.Forms.ComboBox();
            this.lblYearTo = new System.Windows.Forms.Label();
            this.cmbYearTo = new System.Windows.Forms.ComboBox();
            this.gbItemType = new System.Windows.Forms.GroupBox();
            this.cbSortByCustomer = new System.Windows.Forms.CheckBox();
            this.cbSortBySize = new System.Windows.Forms.CheckBox();
            this.cbSortByType = new System.Windows.Forms.CheckBox();
            this.gbDatePeriod = new System.Windows.Forms.GroupBox();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.gbMonthYear = new System.Windows.Forms.GroupBox();
            this.lblMonthFrom = new System.Windows.Forms.Label();
            this.cmbMonthFrom = new System.Windows.Forms.ComboBox();
            this.lblYearFrom = new System.Windows.Forms.Label();
            this.cmbYearFrom = new System.Windows.Forms.ComboBox();
            this.btnFilterApply = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbMergeItem = new System.Windows.Forms.CheckBox();
            this.cbMergeCustomer = new System.Windows.Forms.CheckBox();
            this.tlpReport.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbItemType.SuspendLayout();
            this.gbDatePeriod.SuspendLayout();
            this.gbMonthYear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpReport
            // 
            this.tlpReport.ColumnCount = 1;
            this.tlpReport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpReport.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpReport.Controls.Add(this.dgvList, 0, 3);
            this.tlpReport.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tlpReport.Controls.Add(this.gbFilter, 0, 1);
            this.tlpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpReport.Location = new System.Drawing.Point(0, 0);
            this.tlpReport.Name = "tlpReport";
            this.tlpReport.RowCount = 4;
            this.tlpReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tlpReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tlpReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tlpReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpReport.Size = new System.Drawing.Size(1348, 721);
            this.tlpReport.TabIndex = 167;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnReload, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExcel, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1342, 63);
            this.tableLayoutPanel2.TabIndex = 160;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.lblDateType, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.cmbDateType, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.10526F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.89474F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(194, 57);
            this.tableLayoutPanel6.TabIndex = 168;
            // 
            // lblDateType
            // 
            this.lblDateType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDateType.AutoSize = true;
            this.lblDateType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateType.Location = new System.Drawing.Point(4, 4);
            this.lblDateType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateType.Name = "lblDateType";
            this.lblDateType.Size = new System.Drawing.Size(91, 19);
            this.lblDateType.TabIndex = 167;
            this.lblDateType.Text = "REPORT TYPE";
            // 
            // cmbDateType
            // 
            this.cmbDateType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDateType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDateType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateType.FormattingEnabled = true;
            this.cmbDateType.Location = new System.Drawing.Point(4, 25);
            this.cmbDateType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this.cmbDateType.Name = "cmbDateType";
            this.cmbDateType.Size = new System.Drawing.Size(186, 25);
            this.cmbDateType.TabIndex = 166;
            this.cmbDateType.SelectedIndexChanged += new System.EventHandler(this.cmbDateType_SelectedIndexChanged);
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReload.BackColor = System.Drawing.Color.Transparent;
            this.btnReload.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_refresh_480;
            this.btnReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.ForeColor = System.Drawing.Color.White;
            this.btnReload.Location = new System.Drawing.Point(337, 25);
            this.btnReload.Margin = new System.Windows.Forms.Padding(2);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(36, 36);
            this.btnReload.TabIndex = 174;
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1215, 26);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(124, 36);
            this.btnExcel.TabIndex = 157;
            this.btnExcel.Text = "EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Visible = false;
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.BackColor = System.Drawing.Color.White;
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Location = new System.Drawing.Point(203, 26);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(125, 36);
            this.btnFilter.TabIndex = 151;
            this.btnFilter.Text = "SHOW FILTERS";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvList.Location = new System.Drawing.Point(4, 260);
            this.dgvList.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowTemplate.Height = 50;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvList.Size = new System.Drawing.Size(1340, 460);
            this.dgvList.TabIndex = 152;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvList_ColumnAdded);
            this.dgvList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvList_ColumnHeaderMouseClick);
            this.dgvList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvList_MouseClick);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.lblTotalBag, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblReportTitle, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 221);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1342, 35);
            this.tableLayoutPanel4.TabIndex = 162;
            // 
            // lblTotalBag
            // 
            this.lblTotalBag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalBag.AutoSize = true;
            this.lblTotalBag.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBag.Location = new System.Drawing.Point(144, 16);
            this.lblTotalBag.Name = "lblTotalBag";
            this.lblTotalBag.Size = new System.Drawing.Size(180, 19);
            this.lblTotalBag.TabIndex = 194;
            this.lblTotalBag.Text = "0 BAG(s) / 0 PCS  SELECTED";
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReportTitle.AutoSize = true;
            this.lblReportTitle.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportTitle.Location = new System.Drawing.Point(4, 16);
            this.lblReportTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(130, 19);
            this.lblReportTitle.TabIndex = 153;
            this.lblReportTitle.Text = "DELIVERED REPORT";
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.groupBox1);
            this.gbFilter.Controls.Add(this.gbItemType);
            this.gbFilter.Controls.Add(this.gbDatePeriod);
            this.gbFilter.Controls.Add(this.gbMonthYear);
            this.gbFilter.Controls.Add(this.btnFilterApply);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 72);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1342, 143);
            this.gbFilter.TabIndex = 144;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "FILTER";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMonthTo);
            this.groupBox1.Controls.Add(this.cmbMonthTo);
            this.groupBox1.Controls.Add(this.lblYearTo);
            this.groupBox1.Controls.Add(this.cmbYearTo);
            this.groupBox1.Location = new System.Drawing.Point(236, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 115);
            this.groupBox1.TabIndex = 167;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TO";
            // 
            // lblMonthTo
            // 
            this.lblMonthTo.AutoSize = true;
            this.lblMonthTo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthTo.Location = new System.Drawing.Point(6, 34);
            this.lblMonthTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonthTo.Name = "lblMonthTo";
            this.lblMonthTo.Size = new System.Drawing.Size(60, 19);
            this.lblMonthTo.TabIndex = 153;
            this.lblMonthTo.Text = "MONTH";
            // 
            // cmbMonthTo
            // 
            this.cmbMonthTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthTo.FormattingEnabled = true;
            this.cmbMonthTo.Location = new System.Drawing.Point(10, 53);
            this.cmbMonthTo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonthTo.Name = "cmbMonthTo";
            this.cmbMonthTo.Size = new System.Drawing.Size(91, 25);
            this.cmbMonthTo.TabIndex = 154;
            this.cmbMonthTo.SelectedIndexChanged += new System.EventHandler(this.cmbMonthTo_SelectedIndexChanged);
            // 
            // lblYearTo
            // 
            this.lblYearTo.AutoSize = true;
            this.lblYearTo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearTo.Location = new System.Drawing.Point(109, 34);
            this.lblYearTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYearTo.Name = "lblYearTo";
            this.lblYearTo.Size = new System.Drawing.Size(41, 19);
            this.lblYearTo.TabIndex = 155;
            this.lblYearTo.Text = "YEAR";
            // 
            // cmbYearTo
            // 
            this.cmbYearTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbYearTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbYearTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearTo.FormattingEnabled = true;
            this.cmbYearTo.Location = new System.Drawing.Point(109, 53);
            this.cmbYearTo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbYearTo.Name = "cmbYearTo";
            this.cmbYearTo.Size = new System.Drawing.Size(106, 25);
            this.cmbYearTo.TabIndex = 156;
            this.cmbYearTo.SelectedIndexChanged += new System.EventHandler(this.cmbYearTo_SelectedIndexChanged);
            // 
            // gbItemType
            // 
            this.gbItemType.Controls.Add(this.cbMergeCustomer);
            this.gbItemType.Controls.Add(this.cbMergeItem);
            this.gbItemType.Controls.Add(this.cbSortByCustomer);
            this.gbItemType.Controls.Add(this.cbSortBySize);
            this.gbItemType.Controls.Add(this.cbSortByType);
            this.gbItemType.Location = new System.Drawing.Point(726, 24);
            this.gbItemType.Name = "gbItemType";
            this.gbItemType.Size = new System.Drawing.Size(306, 115);
            this.gbItemType.TabIndex = 166;
            this.gbItemType.TabStop = false;
            this.gbItemType.Text = "SORTING";
            // 
            // cbSortByCustomer
            // 
            this.cbSortByCustomer.AutoSize = true;
            this.cbSortByCustomer.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortByCustomer.Location = new System.Drawing.Point(18, 81);
            this.cbSortByCustomer.Name = "cbSortByCustomer";
            this.cbSortByCustomer.Size = new System.Drawing.Size(103, 21);
            this.cbSortByCustomer.TabIndex = 2;
            this.cbSortByCustomer.Text = "By Customer";
            this.cbSortByCustomer.UseVisualStyleBackColor = true;
            this.cbSortByCustomer.CheckedChanged += new System.EventHandler(this.cbSortByCustomer_CheckedChanged);
            // 
            // cbSortBySize
            // 
            this.cbSortBySize.AutoSize = true;
            this.cbSortBySize.Checked = true;
            this.cbSortBySize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSortBySize.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortBySize.Location = new System.Drawing.Point(18, 29);
            this.cbSortBySize.Name = "cbSortBySize";
            this.cbSortBySize.Size = new System.Drawing.Size(70, 21);
            this.cbSortBySize.TabIndex = 1;
            this.cbSortBySize.Text = "By Size";
            this.cbSortBySize.UseVisualStyleBackColor = true;
            this.cbSortBySize.CheckedChanged += new System.EventHandler(this.cbSortBySize_CheckedChanged);
            // 
            // cbSortByType
            // 
            this.cbSortByType.AutoSize = true;
            this.cbSortByType.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortByType.Location = new System.Drawing.Point(18, 54);
            this.cbSortByType.Name = "cbSortByType";
            this.cbSortByType.Size = new System.Drawing.Size(74, 21);
            this.cbSortByType.TabIndex = 0;
            this.cbSortByType.Text = "By Type";
            this.cbSortByType.UseVisualStyleBackColor = true;
            this.cbSortByType.CheckedChanged += new System.EventHandler(this.cbSortByType_CheckedChanged);
            // 
            // gbDatePeriod
            // 
            this.gbDatePeriod.Controls.Add(this.dtpDateTo);
            this.gbDatePeriod.Controls.Add(this.dtpDateFrom);
            this.gbDatePeriod.Controls.Add(this.lblDateTo);
            this.gbDatePeriod.Controls.Add(this.lblDateFrom);
            this.gbDatePeriod.Location = new System.Drawing.Point(464, 24);
            this.gbDatePeriod.Name = "gbDatePeriod";
            this.gbDatePeriod.Size = new System.Drawing.Size(256, 115);
            this.gbDatePeriod.TabIndex = 152;
            this.gbDatePeriod.TabStop = false;
            this.gbDatePeriod.Text = "DATE PERIOD";
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(128, 53);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(113, 25);
            this.dtpDateTo.TabIndex = 154;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(9, 53);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(113, 25);
            this.dtpDateFrom.TabIndex = 153;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTo.Location = new System.Drawing.Point(124, 32);
            this.lblDateTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(26, 19);
            this.lblDateTo.TabIndex = 150;
            this.lblDateTo.Text = "TO";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFrom.Location = new System.Drawing.Point(5, 31);
            this.lblDateFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(48, 19);
            this.lblDateFrom.TabIndex = 148;
            this.lblDateFrom.Text = "FROM";
            // 
            // gbMonthYear
            // 
            this.gbMonthYear.Controls.Add(this.lblMonthFrom);
            this.gbMonthYear.Controls.Add(this.cmbMonthFrom);
            this.gbMonthYear.Controls.Add(this.lblYearFrom);
            this.gbMonthYear.Controls.Add(this.cmbYearFrom);
            this.gbMonthYear.Location = new System.Drawing.Point(8, 24);
            this.gbMonthYear.Name = "gbMonthYear";
            this.gbMonthYear.Size = new System.Drawing.Size(222, 115);
            this.gbMonthYear.TabIndex = 151;
            this.gbMonthYear.TabStop = false;
            this.gbMonthYear.Text = "FROM";
            // 
            // lblMonthFrom
            // 
            this.lblMonthFrom.AutoSize = true;
            this.lblMonthFrom.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthFrom.Location = new System.Drawing.Point(6, 34);
            this.lblMonthFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonthFrom.Name = "lblMonthFrom";
            this.lblMonthFrom.Size = new System.Drawing.Size(60, 19);
            this.lblMonthFrom.TabIndex = 153;
            this.lblMonthFrom.Text = "MONTH";
            // 
            // cmbMonthFrom
            // 
            this.cmbMonthFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthFrom.FormattingEnabled = true;
            this.cmbMonthFrom.Location = new System.Drawing.Point(10, 53);
            this.cmbMonthFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonthFrom.Name = "cmbMonthFrom";
            this.cmbMonthFrom.Size = new System.Drawing.Size(91, 25);
            this.cmbMonthFrom.TabIndex = 154;
            this.cmbMonthFrom.SelectedIndexChanged += new System.EventHandler(this.cmbMonthFrom_SelectedIndexChanged);
            // 
            // lblYearFrom
            // 
            this.lblYearFrom.AutoSize = true;
            this.lblYearFrom.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearFrom.Location = new System.Drawing.Point(109, 34);
            this.lblYearFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYearFrom.Name = "lblYearFrom";
            this.lblYearFrom.Size = new System.Drawing.Size(41, 19);
            this.lblYearFrom.TabIndex = 155;
            this.lblYearFrom.Text = "YEAR";
            // 
            // cmbYearFrom
            // 
            this.cmbYearFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbYearFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbYearFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearFrom.FormattingEnabled = true;
            this.cmbYearFrom.Location = new System.Drawing.Point(109, 53);
            this.cmbYearFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbYearFrom.Name = "cmbYearFrom";
            this.cmbYearFrom.Size = new System.Drawing.Size(106, 25);
            this.cmbYearFrom.TabIndex = 156;
            this.cmbYearFrom.SelectedIndexChanged += new System.EventHandler(this.cmbYearFrom_SelectedIndexChanged);
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFilterApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterApply.ForeColor = System.Drawing.Color.White;
            this.btnFilterApply.Location = new System.Drawing.Point(1039, 95);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.Size = new System.Drawing.Size(124, 36);
            this.btnFilterApply.TabIndex = 145;
            this.btnFilterApply.Text = "APPLY";
            this.btnFilterApply.UseVisualStyleBackColor = false;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // cbMergeItem
            // 
            this.cbMergeItem.AutoSize = true;
            this.cbMergeItem.Checked = true;
            this.cbMergeItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMergeItem.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMergeItem.Location = new System.Drawing.Point(131, 29);
            this.cbMergeItem.Name = "cbMergeItem";
            this.cbMergeItem.Size = new System.Drawing.Size(134, 21);
            this.cbMergeItem.TabIndex = 3;
            this.cbMergeItem.Text = "Merge Same Item";
            this.cbMergeItem.UseVisualStyleBackColor = true;
            this.cbMergeItem.CheckedChanged += new System.EventHandler(this.cbMergeItem_CheckedChanged);
            // 
            // cbMergeCustomer
            // 
            this.cbMergeCustomer.AutoSize = true;
            this.cbMergeCustomer.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMergeCustomer.Location = new System.Drawing.Point(131, 56);
            this.cbMergeCustomer.Name = "cbMergeCustomer";
            this.cbMergeCustomer.Size = new System.Drawing.Size(165, 21);
            this.cbMergeCustomer.TabIndex = 4;
            this.cbMergeCustomer.Text = "Merge Same Customer";
            this.cbMergeCustomer.UseVisualStyleBackColor = true;
            this.cbMergeCustomer.CheckedChanged += new System.EventHandler(this.cbMergeCustomer_CheckedChanged);
            // 
            // frmSBBDeliveredReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.tlpReport);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSBBDeliveredReport";
            this.Text = "SBB Delivered Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSBBDeliveredReport_FormClosed);
            this.Load += new System.EventHandler(this.frmSBBDeliveredReport_Load);
            this.tlpReport.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbItemType.ResumeLayout(false);
            this.gbItemType.PerformLayout();
            this.gbDatePeriod.ResumeLayout(false);
            this.gbDatePeriod.PerformLayout();
            this.gbMonthYear.ResumeLayout(false);
            this.gbMonthYear.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpReport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblReportTitle;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMonthTo;
        private System.Windows.Forms.ComboBox cmbMonthTo;
        private System.Windows.Forms.Label lblYearTo;
        private System.Windows.Forms.ComboBox cmbYearTo;
        private System.Windows.Forms.GroupBox gbItemType;
        private System.Windows.Forms.GroupBox gbDatePeriod;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.GroupBox gbMonthYear;
        private System.Windows.Forms.Label lblMonthFrom;
        private System.Windows.Forms.ComboBox cmbMonthFrom;
        private System.Windows.Forms.Label lblYearFrom;
        private System.Windows.Forms.ComboBox cmbYearFrom;
        private System.Windows.Forms.Button btnFilterApply;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label lblDateType;
        private System.Windows.Forms.ComboBox cmbDateType;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.Label lblTotalBag;
        private System.Windows.Forms.CheckBox cbSortByCustomer;
        private System.Windows.Forms.CheckBox cbSortBySize;
        private System.Windows.Forms.CheckBox cbSortByType;
        private System.Windows.Forms.CheckBox cbMergeCustomer;
        private System.Windows.Forms.CheckBox cbMergeItem;
    }
}