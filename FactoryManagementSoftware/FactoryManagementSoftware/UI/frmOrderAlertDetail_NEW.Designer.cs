namespace FactoryManagementSoftware.UI
{
    partial class frmOrderAlertDetail_NEW
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMaterialForecastInfo = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.tlpFilter = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbMonthFrom = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbYearFrom = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbZeroStockType = new System.Windows.Forms.CheckBox();
            this.cbShowDeliveredQty = new System.Windows.Forms.CheckBox();
            this.btnFilterApply = new System.Windows.Forms.Button();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvMaterialForecastSummary = new System.Windows.Forms.DataGridView();
            this.cbShowForecastQty = new System.Windows.Forms.CheckBox();
            this.cbShowStillNeedQty = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialForecastInfo)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tlpFilter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialForecastSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMaterialForecastInfo
            // 
            this.dgvMaterialForecastInfo.AllowUserToAddRows = false;
            this.dgvMaterialForecastInfo.AllowUserToDeleteRows = false;
            this.dgvMaterialForecastInfo.AllowUserToOrderColumns = true;
            this.dgvMaterialForecastInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMaterialForecastInfo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMaterialForecastInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterialForecastInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMaterialForecastInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialForecastInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMaterialForecastInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMaterialForecastInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterialForecastInfo.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvMaterialForecastInfo.Location = new System.Drawing.Point(4, 271);
            this.dgvMaterialForecastInfo.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvMaterialForecastInfo.Name = "dgvMaterialForecastInfo";
            this.dgvMaterialForecastInfo.ReadOnly = true;
            this.dgvMaterialForecastInfo.RowHeadersVisible = false;
            this.dgvMaterialForecastInfo.RowHeadersWidth = 51;
            this.dgvMaterialForecastInfo.RowTemplate.Height = 60;
            this.dgvMaterialForecastInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterialForecastInfo.Size = new System.Drawing.Size(911, 442);
            this.dgvMaterialForecastInfo.TabIndex = 152;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.lblMaterial, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblTotal, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 220);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(919, 50);
            this.tableLayoutPanel4.TabIndex = 162;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(782, 27);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(134, 23);
            this.lblTotal.TabIndex = 155;
            this.lblTotal.Text = "TOTAL: 1000 KG";
            this.lblTotal.Visible = false;
            // 
            // lblMaterial
            // 
            this.lblMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterial.Location = new System.Drawing.Point(4, 27);
            this.lblMaterial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(95, 23);
            this.lblMaterial.TabIndex = 153;
            this.lblMaterial.Text = "code name";
            // 
            // tlpFilter
            // 
            this.tlpFilter.ColumnCount = 3;
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 472F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 244F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpFilter.Controls.Add(this.groupBox1, 0, 0);
            this.tlpFilter.Controls.Add(this.groupBox2, 1, 0);
            this.tlpFilter.Controls.Add(this.btnFilterApply, 2, 0);
            this.tlpFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilter.Location = new System.Drawing.Point(3, 123);
            this.tlpFilter.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.tlpFilter.Name = "tlpFilter";
            this.tlpFilter.RowCount = 1;
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.Size = new System.Drawing.Size(916, 94);
            this.tlpFilter.TabIndex = 167;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbMonthFrom);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbYearFrom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 88);
            this.groupBox1.TabIndex = 166;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MONTH";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 6.5F);
            this.label4.Location = new System.Drawing.Point(16, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 153;
            this.label4.Text = "Month";
            // 
            // cmbMonthFrom
            // 
            this.cmbMonthFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthFrom.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbMonthFrom.FormattingEnabled = true;
            this.cmbMonthFrom.Location = new System.Drawing.Point(18, 45);
            this.cmbMonthFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonthFrom.Name = "cmbMonthFrom";
            this.cmbMonthFrom.Size = new System.Drawing.Size(62, 25);
            this.cmbMonthFrom.TabIndex = 154;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 6.5F);
            this.label6.Location = new System.Drawing.Point(85, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 155;
            this.label6.Text = "Year";
            // 
            // cmbYearFrom
            // 
            this.cmbYearFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbYearFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbYearFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearFrom.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbYearFrom.FormattingEnabled = true;
            this.cmbYearFrom.Location = new System.Drawing.Point(88, 45);
            this.cmbYearFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbYearFrom.Name = "cmbYearFrom";
            this.cmbYearFrom.Size = new System.Drawing.Size(81, 25);
            this.cmbYearFrom.TabIndex = 156;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbShowStillNeedQty);
            this.groupBox2.Controls.Add(this.cbShowForecastQty);
            this.groupBox2.Controls.Add(this.cbZeroStockType);
            this.groupBox2.Controls.Add(this.cbShowDeliveredQty);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(203, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(466, 88);
            this.groupBox2.TabIndex = 167;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CALCULATION";
            // 
            // cbZeroStockType
            // 
            this.cbZeroStockType.AutoSize = true;
            this.cbZeroStockType.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbZeroStockType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbZeroStockType.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbZeroStockType.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbZeroStockType.Location = new System.Drawing.Point(22, 51);
            this.cbZeroStockType.Name = "cbZeroStockType";
            this.cbZeroStockType.Size = new System.Drawing.Size(112, 19);
            this.cbZeroStockType.TabIndex = 158;
            this.cbZeroStockType.Text = "Zero Cost Stock";
            this.cbZeroStockType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbZeroStockType.UseVisualStyleBackColor = true;
            // 
            // cbShowDeliveredQty
            // 
            this.cbShowDeliveredQty.AutoSize = true;
            this.cbShowDeliveredQty.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowDeliveredQty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowDeliveredQty.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowDeliveredQty.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowDeliveredQty.Location = new System.Drawing.Point(155, 22);
            this.cbShowDeliveredQty.Name = "cbShowDeliveredQty";
            this.cbShowDeliveredQty.Size = new System.Drawing.Size(132, 19);
            this.cbShowDeliveredQty.TabIndex = 157;
            this.cbShowDeliveredQty.Text = "Show Delivered Qty";
            this.cbShowDeliveredQty.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowDeliveredQty.UseVisualStyleBackColor = true;
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilterApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFilterApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterApply.ForeColor = System.Drawing.Color.White;
            this.btnFilterApply.Location = new System.Drawing.Point(677, 53);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.Size = new System.Drawing.Size(125, 36);
            this.btnFilterApply.TabIndex = 145;
            this.btnFilterApply.Text = "APPLY";
            this.btnFilterApply.UseVisualStyleBackColor = false;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.label1, 0, 0);
            this.tlpMain.Controls.Add(this.tlpFilter, 0, 2);
            this.tlpMain.Controls.Add(this.dgvMaterialForecastSummary, 0, 1);
            this.tlpMain.Controls.Add(this.dgvMaterialForecastInfo, 0, 4);
            this.tlpMain.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(15, 15);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(15);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(919, 714);
            this.tlpMain.TabIndex = 166;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tlpMain, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(949, 744);
            this.tableLayoutPanel2.TabIndex = 167;
            // 
            // dgvMaterialForecastSummary
            // 
            this.dgvMaterialForecastSummary.AllowUserToAddRows = false;
            this.dgvMaterialForecastSummary.AllowUserToDeleteRows = false;
            this.dgvMaterialForecastSummary.AllowUserToOrderColumns = true;
            this.dgvMaterialForecastSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMaterialForecastSummary.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMaterialForecastSummary.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterialForecastSummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMaterialForecastSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialForecastSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMaterialForecastSummary.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMaterialForecastSummary.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvMaterialForecastSummary.Location = new System.Drawing.Point(4, 38);
            this.dgvMaterialForecastSummary.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvMaterialForecastSummary.Name = "dgvMaterialForecastSummary";
            this.dgvMaterialForecastSummary.ReadOnly = true;
            this.dgvMaterialForecastSummary.RowHeadersVisible = false;
            this.dgvMaterialForecastSummary.RowHeadersWidth = 51;
            this.dgvMaterialForecastSummary.RowTemplate.Height = 60;
            this.dgvMaterialForecastSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterialForecastSummary.Size = new System.Drawing.Size(911, 81);
            this.dgvMaterialForecastSummary.TabIndex = 168;
            this.dgvMaterialForecastSummary.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMaterialForecastSummary_CellFormatting);
            // 
            // cbShowForecastQty
            // 
            this.cbShowForecastQty.AutoSize = true;
            this.cbShowForecastQty.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowForecastQty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowForecastQty.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowForecastQty.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowForecastQty.Location = new System.Drawing.Point(22, 22);
            this.cbShowForecastQty.Name = "cbShowForecastQty";
            this.cbShowForecastQty.Size = new System.Drawing.Size(127, 19);
            this.cbShowForecastQty.TabIndex = 159;
            this.cbShowForecastQty.Text = "Show Forecast Qty";
            this.cbShowForecastQty.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowForecastQty.UseVisualStyleBackColor = true;
            // 
            // cbShowStillNeedQty
            // 
            this.cbShowStillNeedQty.AutoSize = true;
            this.cbShowStillNeedQty.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowStillNeedQty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowStillNeedQty.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowStillNeedQty.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowStillNeedQty.Location = new System.Drawing.Point(293, 22);
            this.cbShowStillNeedQty.Name = "cbShowStillNeedQty";
            this.cbShowStillNeedQty.Size = new System.Drawing.Size(133, 19);
            this.cbShowStillNeedQty.TabIndex = 160;
            this.cbShowStillNeedQty.Text = "Show Still Need Qty";
            this.cbShowStillNeedQty.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowStillNeedQty.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 8F);
            this.label1.Location = new System.Drawing.Point(4, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 19);
            this.label1.TabIndex = 168;
            this.label1.Text = "Material Forecast Summary";
            // 
            // frmOrderAlertDetail_NEW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(949, 744);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmOrderAlertDetail_NEW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Material Forecast Info";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialForecastInfo)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tlpFilter.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialForecastSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvMaterialForecastInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblMaterial;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TableLayoutPanel tlpFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbMonthFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbYearFrom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbShowDeliveredQty;
        private System.Windows.Forms.CheckBox cbZeroStockType;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button btnFilterApply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dgvMaterialForecastSummary;
        private System.Windows.Forms.CheckBox cbShowStillNeedQty;
        private System.Windows.Forms.CheckBox cbShowForecastQty;
        private System.Windows.Forms.Label label1;
    }
}