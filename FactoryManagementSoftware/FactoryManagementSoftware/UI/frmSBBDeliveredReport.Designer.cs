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
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalBag = new System.Windows.Forms.Label();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cb23to22 = new System.Windows.Forms.CheckBox();
            this.cb1to31 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDeliveredUnitInSales = new System.Windows.Forms.CheckBox();
            this.cbDeliveredQtyInPcs = new System.Windows.Forms.CheckBox();
            this.cbDeliveredQtyInBag = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMonthTo = new System.Windows.Forms.Label();
            this.cmbMonthTo = new System.Windows.Forms.ComboBox();
            this.lblYearTo = new System.Windows.Forms.Label();
            this.cmbYearTo = new System.Windows.Forms.ComboBox();
            this.gbItemType = new System.Windows.Forms.GroupBox();
            this.cbMergeCustomer = new System.Windows.Forms.CheckBox();
            this.cbMergeItem = new System.Windows.Forms.CheckBox();
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDateType = new System.Windows.Forms.Label();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbItemType.SuspendLayout();
            this.gbDatePeriod.SuspendLayout();
            this.gbMonthYear.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpReport
            // 
            this.tlpReport.ColumnCount = 1;
            this.tlpReport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpReport.Controls.Add(this.dgvList, 0, 3);
            this.tlpReport.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tlpReport.Controls.Add(this.gbFilter, 0, 1);
            this.tlpReport.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpReport.Location = new System.Drawing.Point(10, 10);
            this.tlpReport.Margin = new System.Windows.Forms.Padding(10);
            this.tlpReport.Name = "tlpReport";
            this.tlpReport.RowCount = 4;
            this.tlpReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tlpReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tlpReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tlpReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpReport.Size = new System.Drawing.Size(1328, 701);
            this.tlpReport.TabIndex = 167;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
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
            this.dgvList.Location = new System.Drawing.Point(0, 259);
            this.dgvList.Margin = new System.Windows.Forms.Padding(0);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowTemplate.Height = 50;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvList.Size = new System.Drawing.Size(1328, 442);
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1322, 35);
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
            this.gbFilter.Controls.Add(this.groupBox3);
            this.gbFilter.Controls.Add(this.groupBox2);
            this.gbFilter.Controls.Add(this.groupBox1);
            this.gbFilter.Controls.Add(this.gbItemType);
            this.gbFilter.Controls.Add(this.gbDatePeriod);
            this.gbFilter.Controls.Add(this.gbMonthYear);
            this.gbFilter.Controls.Add(this.btnFilterApply);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 72);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1322, 143);
            this.gbFilter.TabIndex = 144;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "FILTER";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cb23to22);
            this.groupBox3.Controls.Add(this.cb1to31);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.groupBox3.Location = new System.Drawing.Point(394, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(147, 106);
            this.groupBox3.TabIndex = 155;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MONTHLY DATE SETTING";
            // 
            // cb23to22
            // 
            this.cb23to22.AutoSize = true;
            this.cb23to22.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb23to22.Location = new System.Drawing.Point(31, 70);
            this.cb23to22.Name = "cb23to22";
            this.cb23to22.Size = new System.Drawing.Size(64, 19);
            this.cb23to22.TabIndex = 6;
            this.cb23to22.Text = "23 - 22";
            this.cb23to22.UseVisualStyleBackColor = true;
            this.cb23to22.CheckedChanged += new System.EventHandler(this.cb23to22_CheckedChanged);
            // 
            // cb1to31
            // 
            this.cb1to31.AutoSize = true;
            this.cb1to31.Checked = true;
            this.cb1to31.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb1to31.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb1to31.Location = new System.Drawing.Point(31, 38);
            this.cb1to31.Name = "cb1to31";
            this.cb1to31.Size = new System.Drawing.Size(92, 19);
            this.cb1to31.TabIndex = 5;
            this.cb1to31.Text = "1 - 30 OR 31";
            this.cb1to31.UseVisualStyleBackColor = true;
            this.cb1to31.CheckedChanged += new System.EventHandler(this.cb1to31_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDeliveredUnitInSales);
            this.groupBox2.Controls.Add(this.cbDeliveredQtyInPcs);
            this.groupBox2.Controls.Add(this.cbDeliveredQtyInBag);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.groupBox2.Location = new System.Drawing.Point(1047, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(111, 106);
            this.groupBox2.TabIndex = 168;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DELIVERED UNIT";
            // 
            // cbDeliveredUnitInSales
            // 
            this.cbDeliveredUnitInSales.AutoSize = true;
            this.cbDeliveredUnitInSales.Checked = true;
            this.cbDeliveredUnitInSales.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeliveredUnitInSales.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeliveredUnitInSales.Location = new System.Drawing.Point(16, 79);
            this.cbDeliveredUnitInSales.Name = "cbDeliveredUnitInSales";
            this.cbDeliveredUnitInSales.Size = new System.Drawing.Size(61, 19);
            this.cbDeliveredUnitInSales.TabIndex = 4;
            this.cbDeliveredUnitInSales.Text = "SALES";
            this.cbDeliveredUnitInSales.UseVisualStyleBackColor = true;
            this.cbDeliveredUnitInSales.Visible = false;
            // 
            // cbDeliveredQtyInPcs
            // 
            this.cbDeliveredQtyInPcs.AutoSize = true;
            this.cbDeliveredQtyInPcs.Checked = true;
            this.cbDeliveredQtyInPcs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeliveredQtyInPcs.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeliveredQtyInPcs.Location = new System.Drawing.Point(16, 29);
            this.cbDeliveredQtyInPcs.Name = "cbDeliveredQtyInPcs";
            this.cbDeliveredQtyInPcs.Size = new System.Drawing.Size(50, 19);
            this.cbDeliveredQtyInPcs.TabIndex = 3;
            this.cbDeliveredQtyInPcs.Text = "PCS";
            this.cbDeliveredQtyInPcs.UseVisualStyleBackColor = true;
            this.cbDeliveredQtyInPcs.CheckedChanged += new System.EventHandler(this.cbDeliveredQtyInPcs_CheckedChanged);
            // 
            // cbDeliveredQtyInBag
            // 
            this.cbDeliveredQtyInBag.AutoSize = true;
            this.cbDeliveredQtyInBag.Checked = true;
            this.cbDeliveredQtyInBag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeliveredQtyInBag.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeliveredQtyInBag.Location = new System.Drawing.Point(16, 54);
            this.cbDeliveredQtyInBag.Name = "cbDeliveredQtyInBag";
            this.cbDeliveredQtyInBag.Size = new System.Drawing.Size(52, 19);
            this.cbDeliveredQtyInBag.TabIndex = 2;
            this.cbDeliveredQtyInBag.Text = "BAG";
            this.cbDeliveredQtyInBag.UseVisualStyleBackColor = true;
            this.cbDeliveredQtyInBag.CheckedChanged += new System.EventHandler(this.cbDeliveredQtyInBag_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMonthTo);
            this.groupBox1.Controls.Add(this.cmbMonthTo);
            this.groupBox1.Controls.Add(this.lblYearTo);
            this.groupBox1.Controls.Add(this.cmbYearTo);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.groupBox1.Location = new System.Drawing.Point(203, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 106);
            this.groupBox1.TabIndex = 167;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TO";
            // 
            // lblMonthTo
            // 
            this.lblMonthTo.AutoSize = true;
            this.lblMonthTo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthTo.Location = new System.Drawing.Point(11, 69);
            this.lblMonthTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonthTo.Name = "lblMonthTo";
            this.lblMonthTo.Size = new System.Drawing.Size(60, 19);
            this.lblMonthTo.TabIndex = 153;
            this.lblMonthTo.Text = "MONTH";
            // 
            // cmbMonthTo
            // 
            this.cmbMonthTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthTo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbMonthTo.FormattingEnabled = true;
            this.cmbMonthTo.Location = new System.Drawing.Point(79, 69);
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
            this.lblYearTo.Location = new System.Drawing.Point(30, 34);
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
            this.cmbYearTo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbYearTo.FormattingEnabled = true;
            this.cmbYearTo.Location = new System.Drawing.Point(79, 34);
            this.cmbYearTo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbYearTo.Name = "cmbYearTo";
            this.cmbYearTo.Size = new System.Drawing.Size(91, 25);
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
            this.gbItemType.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.gbItemType.Location = new System.Drawing.Point(754, 24);
            this.gbItemType.Name = "gbItemType";
            this.gbItemType.Size = new System.Drawing.Size(287, 106);
            this.gbItemType.TabIndex = 166;
            this.gbItemType.TabStop = false;
            this.gbItemType.Text = "SORTING";
            // 
            // cbMergeCustomer
            // 
            this.cbMergeCustomer.AutoSize = true;
            this.cbMergeCustomer.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMergeCustomer.Location = new System.Drawing.Point(131, 56);
            this.cbMergeCustomer.Name = "cbMergeCustomer";
            this.cbMergeCustomer.Size = new System.Drawing.Size(150, 19);
            this.cbMergeCustomer.TabIndex = 4;
            this.cbMergeCustomer.Text = "Merge Same Customer";
            this.cbMergeCustomer.UseVisualStyleBackColor = true;
            this.cbMergeCustomer.CheckedChanged += new System.EventHandler(this.cbMergeCustomer_CheckedChanged);
            // 
            // cbMergeItem
            // 
            this.cbMergeItem.AutoSize = true;
            this.cbMergeItem.Checked = true;
            this.cbMergeItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMergeItem.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMergeItem.Location = new System.Drawing.Point(131, 29);
            this.cbMergeItem.Name = "cbMergeItem";
            this.cbMergeItem.Size = new System.Drawing.Size(122, 19);
            this.cbMergeItem.TabIndex = 3;
            this.cbMergeItem.Text = "Merge Same Item";
            this.cbMergeItem.UseVisualStyleBackColor = true;
            this.cbMergeItem.CheckedChanged += new System.EventHandler(this.cbMergeItem_CheckedChanged);
            // 
            // cbSortByCustomer
            // 
            this.cbSortByCustomer.AutoSize = true;
            this.cbSortByCustomer.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortByCustomer.Location = new System.Drawing.Point(18, 81);
            this.cbSortByCustomer.Name = "cbSortByCustomer";
            this.cbSortByCustomer.Size = new System.Drawing.Size(97, 19);
            this.cbSortByCustomer.TabIndex = 2;
            this.cbSortByCustomer.Text = "By Customer";
            this.cbSortByCustomer.UseVisualStyleBackColor = true;
            this.cbSortByCustomer.CheckedChanged += new System.EventHandler(this.cbSortByCustomer_CheckedChanged);
            // 
            // cbSortBySize
            // 
            this.cbSortBySize.AutoSize = true;
            this.cbSortBySize.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortBySize.Location = new System.Drawing.Point(18, 29);
            this.cbSortBySize.Name = "cbSortBySize";
            this.cbSortBySize.Size = new System.Drawing.Size(65, 19);
            this.cbSortBySize.TabIndex = 1;
            this.cbSortBySize.Text = "By Size";
            this.cbSortBySize.UseVisualStyleBackColor = true;
            this.cbSortBySize.CheckedChanged += new System.EventHandler(this.cbSortBySize_CheckedChanged);
            // 
            // cbSortByType
            // 
            this.cbSortByType.AutoSize = true;
            this.cbSortByType.Checked = true;
            this.cbSortByType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSortByType.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortByType.Location = new System.Drawing.Point(18, 54);
            this.cbSortByType.Name = "cbSortByType";
            this.cbSortByType.Size = new System.Drawing.Size(69, 19);
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
            this.gbDatePeriod.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.gbDatePeriod.Location = new System.Drawing.Point(547, 24);
            this.gbDatePeriod.Name = "gbDatePeriod";
            this.gbDatePeriod.Size = new System.Drawing.Size(201, 106);
            this.gbDatePeriod.TabIndex = 152;
            this.gbDatePeriod.TabStop = false;
            this.gbDatePeriod.Text = "DATE PERIOD";
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CalendarFont = new System.Drawing.Font("Segoe UI", 7.8F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(71, 71);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(113, 21);
            this.dtpDateTo.TabIndex = 154;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CalendarFont = new System.Drawing.Font("Segoe UI", 7.8F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(71, 34);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(113, 21);
            this.dtpDateFrom.TabIndex = 153;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTo.Location = new System.Drawing.Point(36, 72);
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
            this.lblDateFrom.Location = new System.Drawing.Point(16, 34);
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
            this.gbMonthYear.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.gbMonthYear.Location = new System.Drawing.Point(8, 24);
            this.gbMonthYear.Name = "gbMonthYear";
            this.gbMonthYear.Size = new System.Drawing.Size(188, 106);
            this.gbMonthYear.TabIndex = 151;
            this.gbMonthYear.TabStop = false;
            this.gbMonthYear.Text = "FROM";
            // 
            // lblMonthFrom
            // 
            this.lblMonthFrom.AutoSize = true;
            this.lblMonthFrom.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthFrom.Location = new System.Drawing.Point(13, 69);
            this.lblMonthFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonthFrom.Name = "lblMonthFrom";
            this.lblMonthFrom.Size = new System.Drawing.Size(60, 19);
            this.lblMonthFrom.TabIndex = 153;
            this.lblMonthFrom.Text = "MONTH";
            // 
            // cmbMonthFrom
            // 
            this.cmbMonthFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthFrom.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbMonthFrom.FormattingEnabled = true;
            this.cmbMonthFrom.Location = new System.Drawing.Point(81, 70);
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
            this.lblYearFrom.Location = new System.Drawing.Point(32, 34);
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
            this.cmbYearFrom.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbYearFrom.FormattingEnabled = true;
            this.cmbYearFrom.Location = new System.Drawing.Point(81, 34);
            this.cmbYearFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbYearFrom.Name = "cmbYearFrom";
            this.cmbYearFrom.Size = new System.Drawing.Size(91, 25);
            this.cmbYearFrom.TabIndex = 156;
            this.cmbYearFrom.SelectedIndexChanged += new System.EventHandler(this.cmbYearFrom_SelectedIndexChanged);
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFilterApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterApply.ForeColor = System.Drawing.Color.White;
            this.btnFilterApply.Location = new System.Drawing.Point(1165, 88);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.Size = new System.Drawing.Size(124, 36);
            this.btnFilterApply.TabIndex = 145;
            this.btnFilterApply.Text = "APPLY";
            this.btnFilterApply.UseVisualStyleBackColor = false;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1322, 63);
            this.tableLayoutPanel2.TabIndex = 160;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.lblDateType, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.cmbReportType, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.10526F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.89474F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(200, 63);
            this.tableLayoutPanel6.TabIndex = 168;
            // 
            // lblDateType
            // 
            this.lblDateType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDateType.AutoSize = true;
            this.lblDateType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateType.Location = new System.Drawing.Point(4, 7);
            this.lblDateType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateType.Name = "lblDateType";
            this.lblDateType.Size = new System.Drawing.Size(91, 19);
            this.lblDateType.TabIndex = 167;
            this.lblDateType.Text = "REPORT TYPE";
            // 
            // cmbReportType
            // 
            this.cmbReportType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReportType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbReportType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Location = new System.Drawing.Point(4, 28);
            this.cmbReportType.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(192, 25);
            this.cmbReportType.TabIndex = 166;
            this.cmbReportType.SelectedIndexChanged += new System.EventHandler(this.cmbDateType_SelectedIndexChanged);
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
            this.btnExcel.Location = new System.Drawing.Point(1195, 26);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tlpReport, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1348, 721);
            this.tableLayoutPanel1.TabIndex = 168;
            // 
            // frmSBBDeliveredReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSBBDeliveredReport";
            this.Text = "SBB Delivered Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSBBDeliveredReport_FormClosed);
            this.Load += new System.EventHandler(this.frmSBBDeliveredReport_Load);
            this.tlpReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbItemType.ResumeLayout(false);
            this.gbItemType.PerformLayout();
            this.gbDatePeriod.ResumeLayout(false);
            this.gbDatePeriod.PerformLayout();
            this.gbMonthYear.ResumeLayout(false);
            this.gbMonthYear.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.Label lblTotalBag;
        private System.Windows.Forms.CheckBox cbSortByCustomer;
        private System.Windows.Forms.CheckBox cbSortBySize;
        private System.Windows.Forms.CheckBox cbSortByType;
        private System.Windows.Forms.CheckBox cbMergeCustomer;
        private System.Windows.Forms.CheckBox cbMergeItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbDeliveredQtyInPcs;
        private System.Windows.Forms.CheckBox cbDeliveredQtyInBag;
        private System.Windows.Forms.CheckBox cbDeliveredUnitInSales;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cb23to22;
        private System.Windows.Forms.CheckBox cb1to31;
    }
}