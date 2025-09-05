namespace FactoryManagementSoftware.UI
{
    partial class frmOEMStockCount
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.tlpOEMItemStockCount = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFilter = new Guna.UI.WinForms.GunaGradientButton();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSearchClear = new System.Windows.Forms.Label();
            this.lblResultNo = new System.Windows.Forms.Label();
            this.lblSearchInfo = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.txtItemSearch = new System.Windows.Forms.TextBox();
            this.btnPreviousSearchResult = new System.Windows.Forms.Button();
            this.btnNextSearchResult = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cbMainCustomerOnly = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.btnExcel = new Guna.UI.WinForms.GunaGradientButton();
            this.btnLoad = new Guna.UI.WinForms.GunaGradientButton();
            this.dgvOEMItemStockCountList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnTallyAll = new Guna.UI.WinForms.GunaGradientButton();
            this.lblItemList = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbSpecialTypeColorMode = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRepeatedColorMode = new System.Windows.Forms.CheckBox();
            this.txtInactiveMonthsThreshold = new System.Windows.Forms.TextBox();
            this.cbRemoveNoDeliveredItem = new System.Windows.Forms.CheckBox();
            this.cbIncludeTerminated = new System.Windows.Forms.CheckBox();
            this.cbRemoveNoOrderItem = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tlpOEMItemStockCount.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOEMItemStockCountList)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpOEMItemStockCount
            // 
            this.tlpOEMItemStockCount.ColumnCount = 1;
            this.tlpOEMItemStockCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOEMItemStockCount.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tlpOEMItemStockCount.Controls.Add(this.dgvOEMItemStockCountList, 0, 4);
            this.tlpOEMItemStockCount.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tlpOEMItemStockCount.Controls.Add(this.groupBox6, 0, 2);
            this.tlpOEMItemStockCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOEMItemStockCount.Location = new System.Drawing.Point(15, 15);
            this.tlpOEMItemStockCount.Margin = new System.Windows.Forms.Padding(15);
            this.tlpOEMItemStockCount.Name = "tlpOEMItemStockCount";
            this.tlpOEMItemStockCount.RowCount = 5;
            this.tlpOEMItemStockCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpOEMItemStockCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpOEMItemStockCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpOEMItemStockCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpOEMItemStockCount.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOEMItemStockCount.Size = new System.Drawing.Size(1318, 691);
            this.tlpOEMItemStockCount.TabIndex = 164;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExcel, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnLoad, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 20);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1318, 80);
            this.tableLayoutPanel2.TabIndex = 160;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.AnimationHoverSpeed = 0.07F;
            this.btnFilter.AnimationSpeed = 0.03F;
            this.btnFilter.BackColor = System.Drawing.Color.Transparent;
            this.btnFilter.BaseColor1 = System.Drawing.Color.White;
            this.btnFilter.BaseColor2 = System.Drawing.Color.White;
            this.btnFilter.BorderColor = System.Drawing.Color.Black;
            this.btnFilter.BorderSize = 1;
            this.btnFilter.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFilter.FocusedColor = System.Drawing.Color.Empty;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnFilter.Image = null;
            this.btnFilter.ImageSize = new System.Drawing.Size(20, 20);
            this.btnFilter.Location = new System.Drawing.Point(385, 37);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnFilter.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnFilter.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFilter.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFilter.OnHoverImage = null;
            this.btnFilter.OnPressedColor = System.Drawing.Color.Black;
            this.btnFilter.Radius = 2;
            this.btnFilter.Size = new System.Drawing.Size(120, 40);
            this.btnFilter.TabIndex = 226;
            this.btnFilter.Text = "Search Filter";
            this.btnFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 390F));
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(515, 5);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(390, 70);
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
            this.tableLayoutPanel9.Size = new System.Drawing.Size(390, 34);
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
            this.lblSearchClear.Location = new System.Drawing.Point(270, 17);
            this.lblSearchClear.Margin = new System.Windows.Forms.Padding(0, 0, 6, 5);
            this.lblSearchClear.Name = "lblSearchClear";
            this.lblSearchClear.Size = new System.Drawing.Size(34, 12);
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
            this.lblResultNo.Location = new System.Drawing.Point(368, 20);
            this.lblResultNo.Margin = new System.Windows.Forms.Padding(0, 0, 6, 2);
            this.lblResultNo.Name = "lblResultNo";
            this.lblResultNo.Size = new System.Drawing.Size(16, 12);
            this.lblResultNo.TabIndex = 165;
            this.lblResultNo.Text = "#0";
            // 
            // lblSearchInfo
            // 
            this.lblSearchInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSearchInfo.AutoSize = true;
            this.lblSearchInfo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchInfo.Location = new System.Drawing.Point(0, 15);
            this.lblSearchInfo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblSearchInfo.Name = "lblSearchInfo";
            this.lblSearchInfo.Size = new System.Drawing.Size(0, 17);
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
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 34);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(390, 36);
            this.tableLayoutPanel8.TabIndex = 167;
            // 
            // txtItemSearch
            // 
            this.txtItemSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtItemSearch.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtItemSearch.Location = new System.Drawing.Point(2, 2);
            this.txtItemSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtItemSearch.Name = "txtItemSearch";
            this.txtItemSearch.Size = new System.Drawing.Size(306, 30);
            this.txtItemSearch.TabIndex = 155;
            this.txtItemSearch.Text = "Search";
            this.txtItemSearch.TextChanged += new System.EventHandler(this.txtNameSearch_TextChanged);
            this.txtItemSearch.Enter += new System.EventHandler(this.txtNameSearch_Enter);
            this.txtItemSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNameSearch_KeyDown);
            this.txtItemSearch.Leave += new System.EventHandler(this.txtNameSearch_Leave);
            // 
            // btnPreviousSearchResult
            // 
            this.btnPreviousSearchResult.BackColor = System.Drawing.Color.Transparent;
            this.btnPreviousSearchResult.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviousSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPreviousSearchResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviousSearchResult.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.btnPreviousSearchResult.ForeColor = System.Drawing.Color.DimGray;
            this.btnPreviousSearchResult.Image = global::FactoryManagementSoftware.Properties.Resources.icons8_chevron_up_16;
            this.btnPreviousSearchResult.Location = new System.Drawing.Point(314, 0);
            this.btnPreviousSearchResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 1);
            this.btnPreviousSearchResult.Name = "btnPreviousSearchResult";
            this.btnPreviousSearchResult.Size = new System.Drawing.Size(32, 35);
            this.btnPreviousSearchResult.TabIndex = 167;
            this.btnPreviousSearchResult.UseVisualStyleBackColor = false;
            this.btnPreviousSearchResult.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnNextSearchResult
            // 
            this.btnNextSearchResult.BackColor = System.Drawing.Color.Transparent;
            this.btnNextSearchResult.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNextSearchResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextSearchResult.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.btnNextSearchResult.ForeColor = System.Drawing.Color.DimGray;
            this.btnNextSearchResult.Image = global::FactoryManagementSoftware.Properties.Resources.icons8_chevron_down_16;
            this.btnNextSearchResult.Location = new System.Drawing.Point(354, 0);
            this.btnNextSearchResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 1);
            this.btnNextSearchResult.Name = "btnNextSearchResult";
            this.btnNextSearchResult.Size = new System.Drawing.Size(32, 35);
            this.btnNextSearchResult.TabIndex = 166;
            this.btnNextSearchResult.UseVisualStyleBackColor = false;
            this.btnNextSearchResult.Click += new System.EventHandler(this.button1_Click_1);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(250, 80);
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
            this.tableLayoutPanel5.Size = new System.Drawing.Size(250, 41);
            this.tableLayoutPanel5.TabIndex = 165;
            // 
            // cbMainCustomerOnly
            // 
            this.cbMainCustomerOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMainCustomerOnly.AutoSize = true;
            this.cbMainCustomerOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMainCustomerOnly.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMainCustomerOnly.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbMainCustomerOnly.Location = new System.Drawing.Point(99, 20);
            this.cbMainCustomerOnly.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.cbMainCustomerOnly.Name = "cbMainCustomerOnly";
            this.cbMainCustomerOnly.Size = new System.Drawing.Size(149, 21);
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
            this.label8.Location = new System.Drawing.Point(4, 21);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 0, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 17);
            this.label8.TabIndex = 149;
            this.label8.Text = "CUSTOMER";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(4, 41);
            this.cmbCustomer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(242, 25);
            this.cmbCustomer.TabIndex = 150;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            this.cmbCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCustomer_KeyDown);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.AnimationHoverSpeed = 0.07F;
            this.btnExcel.AnimationSpeed = 0.03F;
            this.btnExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExcel.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(241)))), ((int)(((byte)(218)))));
            this.btnExcel.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(241)))), ((int)(((byte)(218)))));
            this.btnExcel.BorderColor = System.Drawing.Color.Black;
            this.btnExcel.BorderSize = 1;
            this.btnExcel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExcel.FocusedColor = System.Drawing.Color.Empty;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnExcel.Image = null;
            this.btnExcel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnExcel.Location = new System.Drawing.Point(1190, 37);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnExcel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnExcel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnExcel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnExcel.OnHoverImage = null;
            this.btnExcel.OnPressedColor = System.Drawing.Color.Black;
            this.btnExcel.Radius = 2;
            this.btnExcel.Size = new System.Drawing.Size(126, 40);
            this.btnExcel.TabIndex = 225;
            this.btnExcel.Text = "Excel";
            this.btnExcel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnExcel.Visible = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.AnimationHoverSpeed = 0.07F;
            this.btnLoad.AnimationSpeed = 0.03F;
            this.btnLoad.BackColor = System.Drawing.Color.Transparent;
            this.btnLoad.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnLoad.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnLoad.BorderColor = System.Drawing.Color.Black;
            this.btnLoad.BorderSize = 1;
            this.btnLoad.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLoad.FocusedColor = System.Drawing.Color.Empty;
            this.btnLoad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Image = null;
            this.btnLoad.ImageSize = new System.Drawing.Size(20, 20);
            this.btnLoad.Location = new System.Drawing.Point(252, 37);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnLoad.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnLoad.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnLoad.OnHoverForeColor = System.Drawing.Color.White;
            this.btnLoad.OnHoverImage = null;
            this.btnLoad.OnPressedColor = System.Drawing.Color.Black;
            this.btnLoad.Radius = 2;
            this.btnLoad.Size = new System.Drawing.Size(126, 40);
            this.btnLoad.TabIndex = 226;
            this.btnLoad.Text = "Load";
            this.btnLoad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnLoad.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvOEMItemStockCountList
            // 
            this.dgvOEMItemStockCountList.AllowUserToAddRows = false;
            this.dgvOEMItemStockCountList.AllowUserToDeleteRows = false;
            this.dgvOEMItemStockCountList.AllowUserToOrderColumns = true;
            this.dgvOEMItemStockCountList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvOEMItemStockCountList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.dgvOEMItemStockCountList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvOEMItemStockCountList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOEMItemStockCountList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOEMItemStockCountList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOEMItemStockCountList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOEMItemStockCountList.GridColor = System.Drawing.Color.White;
            this.dgvOEMItemStockCountList.Location = new System.Drawing.Point(3, 271);
            this.dgvOEMItemStockCountList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvOEMItemStockCountList.Name = "dgvOEMItemStockCountList";
            this.dgvOEMItemStockCountList.RowHeadersVisible = false;
            this.dgvOEMItemStockCountList.RowHeadersWidth = 51;
            this.dgvOEMItemStockCountList.RowTemplate.Height = 60;
            this.dgvOEMItemStockCountList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOEMItemStockCountList.Size = new System.Drawing.Size(1312, 419);
            this.dgvOEMItemStockCountList.TabIndex = 152;
            this.dgvOEMItemStockCountList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOEMItemStockCountList_CellFormatting);
            this.dgvOEMItemStockCountList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOEMItemStockCountList_CellMouseDown);
            this.dgvOEMItemStockCountList.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvOEMItemStockCountList_CellValidating);
            this.dgvOEMItemStockCountList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOEMItemStockCountList_CellValueChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel4.Controls.Add(this.btnTallyAll, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblItemList, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2, 222);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1314, 46);
            this.tableLayoutPanel4.TabIndex = 162;
            // 
            // btnTallyAll
            // 
            this.btnTallyAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTallyAll.AnimationHoverSpeed = 0.07F;
            this.btnTallyAll.AnimationSpeed = 0.03F;
            this.btnTallyAll.BackColor = System.Drawing.Color.Transparent;
            this.btnTallyAll.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(160)))));
            this.btnTallyAll.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(160)))));
            this.btnTallyAll.BorderColor = System.Drawing.Color.Black;
            this.btnTallyAll.BorderSize = 1;
            this.btnTallyAll.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTallyAll.FocusedColor = System.Drawing.Color.Empty;
            this.btnTallyAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTallyAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnTallyAll.Image = null;
            this.btnTallyAll.ImageSize = new System.Drawing.Size(20, 20);
            this.btnTallyAll.Location = new System.Drawing.Point(1186, 3);
            this.btnTallyAll.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTallyAll.Name = "btnTallyAll";
            this.btnTallyAll.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnTallyAll.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnTallyAll.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnTallyAll.OnHoverForeColor = System.Drawing.Color.White;
            this.btnTallyAll.OnHoverImage = null;
            this.btnTallyAll.OnPressedColor = System.Drawing.Color.Black;
            this.btnTallyAll.Radius = 2;
            this.btnTallyAll.Size = new System.Drawing.Size(126, 40);
            this.btnTallyAll.TabIndex = 224;
            this.btnTallyAll.Text = "Tally All";
            this.btnTallyAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnTallyAll.Visible = false;
            this.btnTallyAll.Click += new System.EventHandler(this.btnTallyAll_Click);
            // 
            // lblItemList
            // 
            this.lblItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblItemList.AutoSize = true;
            this.lblItemList.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblItemList.Location = new System.Drawing.Point(3, 18);
            this.lblItemList.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblItemList.Name = "lblItemList";
            this.lblItemList.Size = new System.Drawing.Size(226, 23);
            this.lblItemList.TabIndex = 153;
            this.lblItemList.Text = "OEM Item Stock Count List";
            this.lblItemList.Click += new System.EventHandler(this.lblForecastType_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbSpecialTypeColorMode);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.cbRepeatedColorMode);
            this.groupBox6.Controls.Add(this.txtInactiveMonthsThreshold);
            this.groupBox6.Controls.Add(this.cbRemoveNoDeliveredItem);
            this.groupBox6.Controls.Add(this.cbIncludeTerminated);
            this.groupBox6.Controls.Add(this.cbRemoveNoOrderItem);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox6.Location = new System.Drawing.Point(2, 102);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.MinimumSize = new System.Drawing.Size(101, 85);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(1314, 116);
            this.groupBox6.TabIndex = 166;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "FILTER";
            // 
            // cbSpecialTypeColorMode
            // 
            this.cbSpecialTypeColorMode.AutoSize = true;
            this.cbSpecialTypeColorMode.Checked = true;
            this.cbSpecialTypeColorMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSpecialTypeColorMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSpecialTypeColorMode.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbSpecialTypeColorMode.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSpecialTypeColorMode.Location = new System.Drawing.Point(338, 53);
            this.cbSpecialTypeColorMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbSpecialTypeColorMode.Name = "cbSpecialTypeColorMode";
            this.cbSpecialTypeColorMode.Size = new System.Drawing.Size(143, 19);
            this.cbSpecialTypeColorMode.TabIndex = 169;
            this.cbSpecialTypeColorMode.Text = "Special Type Coloring";
            this.cbSpecialTypeColorMode.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbSpecialTypeColorMode.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(201, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 12);
            this.label5.TabIndex = 169;
            this.label5.Text = "months";
            // 
            // cbRepeatedColorMode
            // 
            this.cbRepeatedColorMode.AutoSize = true;
            this.cbRepeatedColorMode.Checked = true;
            this.cbRepeatedColorMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRepeatedColorMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRepeatedColorMode.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbRepeatedColorMode.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRepeatedColorMode.Location = new System.Drawing.Point(338, 25);
            this.cbRepeatedColorMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbRepeatedColorMode.Name = "cbRepeatedColorMode";
            this.cbRepeatedColorMode.Size = new System.Drawing.Size(153, 19);
            this.cbRepeatedColorMode.TabIndex = 168;
            this.cbRepeatedColorMode.Text = "Repeated Row Coloring";
            this.cbRepeatedColorMode.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRepeatedColorMode.UseVisualStyleBackColor = true;
            // 
            // txtInactiveMonthsThreshold
            // 
            this.txtInactiveMonthsThreshold.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.txtInactiveMonthsThreshold.Location = new System.Drawing.Point(158, 79);
            this.txtInactiveMonthsThreshold.Margin = new System.Windows.Forms.Padding(2);
            this.txtInactiveMonthsThreshold.Name = "txtInactiveMonthsThreshold";
            this.txtInactiveMonthsThreshold.Size = new System.Drawing.Size(38, 21);
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
            this.cbRemoveNoDeliveredItem.Location = new System.Drawing.Point(11, 81);
            this.cbRemoveNoDeliveredItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbRemoveNoDeliveredItem.Name = "cbRemoveNoDeliveredItem";
            this.cbRemoveNoDeliveredItem.Size = new System.Drawing.Size(143, 19);
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
            this.cbIncludeTerminated.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbIncludeTerminated.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeTerminated.Location = new System.Drawing.Point(11, 25);
            this.cbIncludeTerminated.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbIncludeTerminated.Name = "cbIncludeTerminated";
            this.cbIncludeTerminated.Size = new System.Drawing.Size(116, 19);
            this.cbIncludeTerminated.TabIndex = 165;
            this.cbIncludeTerminated.Text = "Terminated Item";
            this.cbIncludeTerminated.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbIncludeTerminated.UseVisualStyleBackColor = true;
            this.cbIncludeTerminated.CheckedChanged += new System.EventHandler(this.cbIncludeTerminated_CheckedChanged);
            // 
            // cbRemoveNoOrderItem
            // 
            this.cbRemoveNoOrderItem.AutoSize = true;
            this.cbRemoveNoOrderItem.Checked = true;
            this.cbRemoveNoOrderItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRemoveNoOrderItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRemoveNoOrderItem.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbRemoveNoOrderItem.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRemoveNoOrderItem.Location = new System.Drawing.Point(11, 53);
            this.cbRemoveNoOrderItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 7);
            this.cbRemoveNoOrderItem.Name = "cbRemoveNoOrderItem";
            this.cbRemoveNoOrderItem.Size = new System.Drawing.Size(165, 19);
            this.cbRemoveNoOrderItem.TabIndex = 167;
            this.cbRemoveNoOrderItem.Text = "Remove No Forecast Item";
            this.cbRemoveNoOrderItem.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbRemoveNoOrderItem.UseVisualStyleBackColor = true;
            this.cbRemoveNoOrderItem.CheckedChanged += new System.EventHandler(this.cbRemoveForecastInvalidItem_CheckedChanged);
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
            this.tableLayoutPanel10.Controls.Add(this.tlpOEMItemStockCount, 0, 0);
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
            // frmOEMStockCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.tableLayoutPanel10);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "frmOEMStockCount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOEMStockCount";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOEMStockCount_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmForecastReport_NEW_FormClosed);
            this.Load += new System.EventHandler(this.frmForecastReport_NEW_Load);
            this.Shown += new System.EventHandler(this.frmForecastReport_NEW_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmForecastReport_NEW_KeyDown);
            this.tlpOEMItemStockCount.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOEMItemStockCountList)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.TableLayoutPanel tlpOEMItemStockCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.DataGridView dgvOEMItemStockCountList;
        private System.Windows.Forms.Label lblItemList;
        private System.Windows.Forms.TextBox txtItemSearch;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbIncludeTerminated;
        private System.Windows.Forms.CheckBox cbRemoveNoOrderItem;
        private System.Windows.Forms.GroupBox groupBox6;
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
        private System.Windows.Forms.CheckBox cbSpecialTypeColorMode;
        private System.Windows.Forms.Label lblSearchClear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Guna.UI.WinForms.GunaGradientButton btnExcel;
        private Guna.UI.WinForms.GunaGradientButton btnLoad;
        private Guna.UI.WinForms.GunaGradientButton btnFilter;
        private Guna.UI.WinForms.GunaGradientButton btnTallyAll;
    }
}