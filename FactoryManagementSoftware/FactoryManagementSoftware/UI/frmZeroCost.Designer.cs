namespace FactoryManagementSoftware.UI
{
    partial class frmZeroCost
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
            this.btnSwitchToStockCheck = new System.Windows.Forms.Button();
            this.btnSwitchToMatUsed = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStockMonth = new System.Windows.Forms.ComboBox();
            this.cmbStockYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.lblChangeDate = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.gbPMMA = new System.Windows.Forms.GroupBox();
            this.cbInOutUpdate = new System.Windows.Forms.CheckBox();
            this.btnLockData = new System.Windows.Forms.Button();
            this.cbEditMode = new System.Windows.Forms.CheckBox();
            this.tlpPMMA = new System.Windows.Forms.TableLayoutPanel();
            this.dgvMatStock = new System.Windows.Forms.DataGridView();
            this.dgvMatUsed = new System.Windows.Forms.DataGridView();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnAllExcel = new System.Windows.Forms.Button();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.cbFastMode = new System.Windows.Forms.CheckBox();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.gbPMMA.SuspendLayout();
            this.tlpPMMA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatUsed)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSwitchToStockCheck
            // 
            this.btnSwitchToStockCheck.BackColor = System.Drawing.Color.White;
            this.btnSwitchToStockCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitchToStockCheck.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitchToStockCheck.ForeColor = System.Drawing.Color.Black;
            this.btnSwitchToStockCheck.Location = new System.Drawing.Point(168, 27);
            this.btnSwitchToStockCheck.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnSwitchToStockCheck.Name = "btnSwitchToStockCheck";
            this.btnSwitchToStockCheck.Size = new System.Drawing.Size(149, 36);
            this.btnSwitchToStockCheck.TabIndex = 120;
            this.btnSwitchToStockCheck.Text = "STOCK CHECK";
            this.btnSwitchToStockCheck.UseVisualStyleBackColor = false;
            this.btnSwitchToStockCheck.Click += new System.EventHandler(this.btnStockCheck_Click);
            // 
            // btnSwitchToMatUsed
            // 
            this.btnSwitchToMatUsed.BackColor = System.Drawing.Color.White;
            this.btnSwitchToMatUsed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitchToMatUsed.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitchToMatUsed.ForeColor = System.Drawing.Color.Black;
            this.btnSwitchToMatUsed.Location = new System.Drawing.Point(325, 27);
            this.btnSwitchToMatUsed.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnSwitchToMatUsed.Name = "btnSwitchToMatUsed";
            this.btnSwitchToMatUsed.Size = new System.Drawing.Size(145, 36);
            this.btnSwitchToMatUsed.TabIndex = 121;
            this.btnSwitchToMatUsed.Text = "MATERIAL USED";
            this.btnSwitchToMatUsed.UseVisualStyleBackColor = false;
            this.btnSwitchToMatUsed.Click += new System.EventHandler(this.btnMatUsed_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 122;
            this.label1.Text = "MONTH";
            // 
            // cmbStockMonth
            // 
            this.cmbStockMonth.FormattingEnabled = true;
            this.cmbStockMonth.Location = new System.Drawing.Point(25, 64);
            this.cmbStockMonth.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStockMonth.Name = "cmbStockMonth";
            this.cmbStockMonth.Size = new System.Drawing.Size(91, 31);
            this.cmbStockMonth.TabIndex = 123;
            this.cmbStockMonth.SelectedIndexChanged += new System.EventHandler(this.cmbStockMonth_SelectedIndexChanged);
            // 
            // cmbStockYear
            // 
            this.cmbStockYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbStockYear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbStockYear.FormattingEnabled = true;
            this.cmbStockYear.Location = new System.Drawing.Point(124, 64);
            this.cmbStockYear.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStockYear.Name = "cmbStockYear";
            this.cmbStockYear.Size = new System.Drawing.Size(137, 31);
            this.cmbStockYear.TabIndex = 125;
            this.cmbStockYear.SelectedIndexChanged += new System.EventHandler(this.cmbStockYear_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(124, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 124;
            this.label2.Text = "YEAR";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(276, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 126;
            this.label3.Text = "FROM";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(280, 65);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(147, 30);
            this.dtpFrom.TabIndex = 127;
            // 
            // dtpTo
            // 
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(445, 64);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(4);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(139, 30);
            this.dtpTo.TabIndex = 129;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(441, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 17);
            this.label4.TabIndex = 128;
            this.label4.Text = "TO";
            // 
            // lblChangeDate
            // 
            this.lblChangeDate.AutoSize = true;
            this.lblChangeDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChangeDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeDate.ForeColor = System.Drawing.Color.Blue;
            this.lblChangeDate.Location = new System.Drawing.Point(484, 98);
            this.lblChangeDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChangeDate.Name = "lblChangeDate";
            this.lblChangeDate.Size = new System.Drawing.Size(93, 17);
            this.lblChangeDate.TabIndex = 130;
            this.lblChangeDate.Text = "CHANGE DATE";
            this.lblChangeDate.Click += new System.EventHandler(this.lblStockChangeDate_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(602, 58);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(167, 36);
            this.btnCheck.TabIndex = 131;
            this.btnCheck.Text = "CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // gbPMMA
            // 
            this.gbPMMA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPMMA.Controls.Add(this.cbInOutUpdate);
            this.gbPMMA.Controls.Add(this.btnLockData);
            this.gbPMMA.Controls.Add(this.cbEditMode);
            this.gbPMMA.Controls.Add(this.tlpPMMA);
            this.gbPMMA.Controls.Add(this.label1);
            this.gbPMMA.Controls.Add(this.cmbStockMonth);
            this.gbPMMA.Controls.Add(this.btnCheck);
            this.gbPMMA.Controls.Add(this.label2);
            this.gbPMMA.Controls.Add(this.lblChangeDate);
            this.gbPMMA.Controls.Add(this.cmbStockYear);
            this.gbPMMA.Controls.Add(this.dtpTo);
            this.gbPMMA.Controls.Add(this.label3);
            this.gbPMMA.Controls.Add(this.label4);
            this.gbPMMA.Controls.Add(this.dtpFrom);
            this.gbPMMA.Location = new System.Drawing.Point(13, 103);
            this.gbPMMA.Margin = new System.Windows.Forms.Padding(4);
            this.gbPMMA.Name = "gbPMMA";
            this.gbPMMA.Padding = new System.Windows.Forms.Padding(4);
            this.gbPMMA.Size = new System.Drawing.Size(1551, 737);
            this.gbPMMA.TabIndex = 133;
            this.gbPMMA.TabStop = false;
            this.gbPMMA.Text = "ZERO COST MATERIAL STOCK LIST";
            this.gbPMMA.Enter += new System.EventHandler(this.gbPMMA_Enter);
            // 
            // cbInOutUpdate
            // 
            this.cbInOutUpdate.AutoSize = true;
            this.cbInOutUpdate.Checked = true;
            this.cbInOutUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInOutUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbInOutUpdate.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.cbInOutUpdate.Location = new System.Drawing.Point(602, 36);
            this.cbInOutUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.cbInOutUpdate.Name = "cbInOutUpdate";
            this.cbInOutUpdate.Size = new System.Drawing.Size(124, 17);
            this.cbInOutUpdate.TabIndex = 138;
            this.cbInOutUpdate.Text = "IN/OUT DATA UPDATE";
            this.cbInOutUpdate.UseVisualStyleBackColor = true;
            this.cbInOutUpdate.CheckedChanged += new System.EventHandler(this.cbInOutUpdate_CheckedChanged);
            // 
            // btnLockData
            // 
            this.btnLockData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLockData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnLockData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLockData.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLockData.ForeColor = System.Drawing.Color.Black;
            this.btnLockData.Location = new System.Drawing.Point(1398, 58);
            this.btnLockData.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.btnLockData.Name = "btnLockData";
            this.btnLockData.Size = new System.Drawing.Size(124, 36);
            this.btnLockData.TabIndex = 137;
            this.btnLockData.Text = "LOCK DATA";
            this.btnLockData.UseVisualStyleBackColor = false;
            this.btnLockData.Click += new System.EventHandler(this.btnLockData_Click);
            // 
            // cbEditMode
            // 
            this.cbEditMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditMode.AutoSize = true;
            this.cbEditMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbEditMode.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.cbEditMode.Location = new System.Drawing.Point(1398, 35);
            this.cbEditMode.Margin = new System.Windows.Forms.Padding(4);
            this.cbEditMode.Name = "cbEditMode";
            this.cbEditMode.Size = new System.Drawing.Size(79, 17);
            this.cbEditMode.TabIndex = 136;
            this.cbEditMode.Text = "EDIT MODE";
            this.cbEditMode.UseVisualStyleBackColor = true;
            this.cbEditMode.Visible = false;
            this.cbEditMode.CheckedChanged += new System.EventHandler(this.cbEditMode_CheckedChanged);
            // 
            // tlpPMMA
            // 
            this.tlpPMMA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpPMMA.ColumnCount = 2;
            this.tlpPMMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPMMA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPMMA.Controls.Add(this.dgvMatStock, 0, 0);
            this.tlpPMMA.Controls.Add(this.dgvMatUsed, 1, 0);
            this.tlpPMMA.Location = new System.Drawing.Point(21, 130);
            this.tlpPMMA.Margin = new System.Windows.Forms.Padding(4);
            this.tlpPMMA.Name = "tlpPMMA";
            this.tlpPMMA.RowCount = 1;
            this.tlpPMMA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPMMA.Size = new System.Drawing.Size(1505, 585);
            this.tlpPMMA.TabIndex = 136;
            // 
            // dgvMatStock
            // 
            this.dgvMatStock.AllowUserToAddRows = false;
            this.dgvMatStock.AllowUserToDeleteRows = false;
            this.dgvMatStock.AllowUserToOrderColumns = true;
            this.dgvMatStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMatStock.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMatStock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvMatStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatStock.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatStock.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMatStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatStock.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvMatStock.Location = new System.Drawing.Point(4, 1);
            this.dgvMatStock.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvMatStock.Name = "dgvMatStock";
            this.dgvMatStock.ReadOnly = true;
            this.dgvMatStock.RowHeadersVisible = false;
            this.dgvMatStock.RowHeadersWidth = 51;
            this.dgvMatStock.RowTemplate.Height = 40;
            this.dgvMatStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatStock.Size = new System.Drawing.Size(744, 583);
            this.dgvMatStock.TabIndex = 134;
            this.dgvMatStock.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatStock_CellContentClick);
            this.dgvMatStock.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatStock_CellEndEdit);
            this.dgvMatStock.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatStock_CellEnter);
            this.dgvMatStock.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMatStock_CellFormatting);
            this.dgvMatStock.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMatStock_DataBindingComplete);
            this.dgvMatStock.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvMatStock_EditingControlShowing);
            // 
            // dgvMatUsed
            // 
            this.dgvMatUsed.AllowUserToAddRows = false;
            this.dgvMatUsed.AllowUserToDeleteRows = false;
            this.dgvMatUsed.AllowUserToOrderColumns = true;
            this.dgvMatUsed.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMatUsed.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMatUsed.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvMatUsed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatUsed.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatUsed.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMatUsed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatUsed.GridColor = System.Drawing.Color.White;
            this.dgvMatUsed.Location = new System.Drawing.Point(756, 1);
            this.dgvMatUsed.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvMatUsed.Name = "dgvMatUsed";
            this.dgvMatUsed.ReadOnly = true;
            this.dgvMatUsed.RowHeadersVisible = false;
            this.dgvMatUsed.RowHeadersWidth = 51;
            this.dgvMatUsed.RowTemplate.Height = 40;
            this.dgvMatUsed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatUsed.Size = new System.Drawing.Size(745, 583);
            this.dgvMatUsed.TabIndex = 133;
            this.dgvMatUsed.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatUsed_CellContentClick);
            this.dgvMatUsed.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMatUsed_CellFormatting);
            this.dgvMatUsed.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMatUsed_DataBindingComplete);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1370, 10);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(165, 36);
            this.btnExcel.TabIndex = 134;
            this.btnExcel.Text = "EXPORT TO EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnAllExcel
            // 
            this.btnAllExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAllExcel.BackColor = System.Drawing.Color.White;
            this.btnAllExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllExcel.ForeColor = System.Drawing.Color.Black;
            this.btnAllExcel.Location = new System.Drawing.Point(1169, 10);
            this.btnAllExcel.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnAllExcel.Name = "btnAllExcel";
            this.btnAllExcel.Size = new System.Drawing.Size(197, 36);
            this.btnAllExcel.TabIndex = 135;
            this.btnAllExcel.Text = "EXPORT ALL TO EXCEL";
            this.btnAllExcel.UseVisualStyleBackColor = false;
            this.btnAllExcel.Visible = false;
            this.btnAllExcel.Click += new System.EventHandler(this.btnExportAllToExcel_Click);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerDoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerProgressChanged);
            // 
            // cbFastMode
            // 
            this.cbFastMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFastMode.AutoSize = true;
            this.cbFastMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbFastMode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFastMode.Location = new System.Drawing.Point(1175, 52);
            this.cbFastMode.Margin = new System.Windows.Forms.Padding(4);
            this.cbFastMode.Name = "cbFastMode";
            this.cbFastMode.Size = new System.Drawing.Size(99, 21);
            this.cbFastMode.TabIndex = 137;
            this.cbFastMode.Text = "FAST MODE";
            this.cbFastMode.UseVisualStyleBackColor = true;
            this.cbFastMode.Visible = false;
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Items.AddRange(new object[] {
            "PMMA",
            "PERMABONN"});
            this.cmbSupplier.Location = new System.Drawing.Point(23, 32);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(137, 31);
            this.cmbSupplier.TabIndex = 138;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(34, 78);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1501, 21);
            this.progressBar1.TabIndex = 137;
            this.progressBar1.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 139;
            this.label5.Text = "Supplier";
            // 
            // frmZeroCost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.cbFastMode);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.gbPMMA);
            this.Controls.Add(this.btnAllExcel);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnSwitchToMatUsed);
            this.Controls.Add(this.btnSwitchToStockCheck);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmZeroCost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zero Cost Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmNewPMMA_FormClosed);
            this.Load += new System.EventHandler(this.frmNewPMMA_Load);
            this.gbPMMA.ResumeLayout(false);
            this.gbPMMA.PerformLayout();
            this.tlpPMMA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatUsed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSwitchToStockCheck;
        private System.Windows.Forms.Button btnSwitchToMatUsed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStockMonth;
        private System.Windows.Forms.ComboBox cmbStockYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblChangeDate;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.GroupBox gbPMMA;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnAllExcel;
        private System.Windows.Forms.DataGridView dgvMatUsed;
        private System.Windows.Forms.TableLayoutPanel tlpPMMA;
        private System.Windows.Forms.DataGridView dgvMatStock;
        private System.Windows.Forms.CheckBox cbEditMode;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.CheckBox cbFastMode;
        private System.Windows.Forms.Button btnLockData;
        private System.Windows.Forms.CheckBox cbInOutUpdate;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label5;
    }
}