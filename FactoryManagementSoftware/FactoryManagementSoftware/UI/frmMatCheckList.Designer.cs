namespace FactoryManagementSoftware.UI
{
    partial class frmMatCheckList
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
            this.lblTrfDate = new System.Windows.Forms.Label();
            this.dtpTrfDate = new System.Windows.Forms.DateTimePicker();
            this.cbCheckAll = new System.Windows.Forms.CheckBox();
            this.btnAutoInOut = new System.Windows.Forms.Button();
            this.btnItemCheck = new System.Windows.Forms.Button();
            this.tlpMatChecklist = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTransfer = new System.Windows.Forms.DataGridView();
            this.dgvDeliver = new System.Windows.Forms.DataGridView();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnDeliverChecklist = new System.Windows.Forms.Button();
            this.btnTrfChecklist = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMatChecklist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliver)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTrfDate
            // 
            this.lblTrfDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrfDate.AutoSize = true;
            this.lblTrfDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrfDate.Location = new System.Drawing.Point(3, 27);
            this.lblTrfDate.Name = "lblTrfDate";
            this.lblTrfDate.Size = new System.Drawing.Size(89, 13);
            this.lblTrfDate.TabIndex = 141;
            this.lblTrfDate.Text = "TRANSFER DATE";
            this.lblTrfDate.Visible = false;
            // 
            // dtpTrfDate
            // 
            this.dtpTrfDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpTrfDate.CustomFormat = "ddMMMMyy";
            this.dtpTrfDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTrfDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTrfDate.Location = new System.Drawing.Point(6, 43);
            this.dtpTrfDate.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.dtpTrfDate.Name = "dtpTrfDate";
            this.dtpTrfDate.Size = new System.Drawing.Size(121, 21);
            this.dtpTrfDate.TabIndex = 141;
            this.dtpTrfDate.Visible = false;
            // 
            // cbCheckAll
            // 
            this.cbCheckAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbCheckAll.AutoSize = true;
            this.cbCheckAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCheckAll.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCheckAll.Location = new System.Drawing.Point(33, 11);
            this.cbCheckAll.Name = "cbCheckAll";
            this.cbCheckAll.Size = new System.Drawing.Size(94, 17);
            this.cbCheckAll.TabIndex = 141;
            this.cbCheckAll.Text = "CHECKED ALL";
            this.cbCheckAll.UseVisualStyleBackColor = true;
            this.cbCheckAll.Visible = false;
            this.cbCheckAll.CheckedChanged += new System.EventHandler(this.cbCheckAll_CheckedChanged);
            // 
            // btnAutoInOut
            // 
            this.btnAutoInOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoInOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnAutoInOut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAutoInOut.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoInOut.ForeColor = System.Drawing.Color.White;
            this.btnAutoInOut.Location = new System.Drawing.Point(1066, 41);
            this.btnAutoInOut.Margin = new System.Windows.Forms.Padding(4, 1, 4, 3);
            this.btnAutoInOut.Name = "btnAutoInOut";
            this.btnAutoInOut.Size = new System.Drawing.Size(120, 36);
            this.btnAutoInOut.TabIndex = 142;
            this.btnAutoInOut.Text = "AUTO IN/OUT";
            this.btnAutoInOut.UseVisualStyleBackColor = false;
            this.btnAutoInOut.Visible = false;
            this.btnAutoInOut.Click += new System.EventHandler(this.btnAutoInOut_Click);
            // 
            // btnItemCheck
            // 
            this.btnItemCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItemCheck.BackColor = System.Drawing.Color.White;
            this.btnItemCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemCheck.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemCheck.ForeColor = System.Drawing.Color.Black;
            this.btnItemCheck.Location = new System.Drawing.Point(4, 41);
            this.btnItemCheck.Margin = new System.Windows.Forms.Padding(4, 1, 4, 3);
            this.btnItemCheck.Name = "btnItemCheck";
            this.btnItemCheck.Size = new System.Drawing.Size(122, 36);
            this.btnItemCheck.TabIndex = 141;
            this.btnItemCheck.Text = "ITEM CHECK";
            this.btnItemCheck.UseVisualStyleBackColor = false;
            this.btnItemCheck.Click += new System.EventHandler(this.btnItemCheck_Click);
            // 
            // tlpMatChecklist
            // 
            this.tlpMatChecklist.ColumnCount = 2;
            this.tlpMatChecklist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMatChecklist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMatChecklist.Controls.Add(this.dgvTransfer, 0, 0);
            this.tlpMatChecklist.Controls.Add(this.dgvDeliver, 1, 0);
            this.tlpMatChecklist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMatChecklist.Location = new System.Drawing.Point(0, 130);
            this.tlpMatChecklist.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMatChecklist.Name = "tlpMatChecklist";
            this.tlpMatChecklist.RowCount = 1;
            this.tlpMatChecklist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMatChecklist.Size = new System.Drawing.Size(1320, 585);
            this.tlpMatChecklist.TabIndex = 136;
            // 
            // dgvTransfer
            // 
            this.dgvTransfer.AllowUserToAddRows = false;
            this.dgvTransfer.AllowUserToDeleteRows = false;
            this.dgvTransfer.AllowUserToOrderColumns = true;
            this.dgvTransfer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvTransfer.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvTransfer.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransfer.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTransfer.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransfer.GridColor = System.Drawing.Color.White;
            this.dgvTransfer.Location = new System.Drawing.Point(4, 5);
            this.dgvTransfer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 1);
            this.dgvTransfer.Name = "dgvTransfer";
            this.dgvTransfer.ReadOnly = true;
            this.dgvTransfer.RowHeadersVisible = false;
            this.dgvTransfer.RowTemplate.Height = 40;
            this.dgvTransfer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransfer.Size = new System.Drawing.Size(652, 579);
            this.dgvTransfer.TabIndex = 134;
            this.dgvTransfer.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTransfer_CellFormatting);
            // 
            // dgvDeliver
            // 
            this.dgvDeliver.AllowUserToAddRows = false;
            this.dgvDeliver.AllowUserToDeleteRows = false;
            this.dgvDeliver.AllowUserToOrderColumns = true;
            this.dgvDeliver.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDeliver.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvDeliver.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDeliver.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeliver.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDeliver.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDeliver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDeliver.GridColor = System.Drawing.Color.White;
            this.dgvDeliver.Location = new System.Drawing.Point(664, 5);
            this.dgvDeliver.Margin = new System.Windows.Forms.Padding(4, 5, 4, 1);
            this.dgvDeliver.Name = "dgvDeliver";
            this.dgvDeliver.ReadOnly = true;
            this.dgvDeliver.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDeliver.RowHeadersVisible = false;
            this.dgvDeliver.RowTemplate.Height = 40;
            this.dgvDeliver.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeliver.Size = new System.Drawing.Size(652, 579);
            this.dgvDeliver.TabIndex = 133;
            this.dgvDeliver.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeliver_CellClick);
            this.dgvDeliver.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDeliver_CellFormatting);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1196, 1);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(120, 36);
            this.btnExcel.TabIndex = 138;
            this.btnExcel.Text = "EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnDeliverChecklist
            // 
            this.btnDeliverChecklist.BackColor = System.Drawing.Color.White;
            this.btnDeliverChecklist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeliverChecklist.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeliverChecklist.ForeColor = System.Drawing.Color.Black;
            this.btnDeliverChecklist.Location = new System.Drawing.Point(184, 1);
            this.btnDeliverChecklist.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnDeliverChecklist.Name = "btnDeliverChecklist";
            this.btnDeliverChecklist.Size = new System.Drawing.Size(120, 36);
            this.btnDeliverChecklist.TabIndex = 136;
            this.btnDeliverChecklist.Text = "DELIVERY LIST";
            this.btnDeliverChecklist.UseVisualStyleBackColor = false;
            this.btnDeliverChecklist.Click += new System.EventHandler(this.btnDeliver_Click);
            // 
            // btnTrfChecklist
            // 
            this.btnTrfChecklist.BackColor = System.Drawing.Color.White;
            this.btnTrfChecklist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrfChecklist.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrfChecklist.ForeColor = System.Drawing.Color.Black;
            this.btnTrfChecklist.Location = new System.Drawing.Point(54, 1);
            this.btnTrfChecklist.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnTrfChecklist.Name = "btnTrfChecklist";
            this.btnTrfChecklist.Size = new System.Drawing.Size(120, 36);
            this.btnTrfChecklist.TabIndex = 135;
            this.btnTrfChecklist.Text = "PICKING LIST";
            this.btnTrfChecklist.UseVisualStyleBackColor = false;
            this.btnTrfChecklist.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_go_back_64;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(2, 2);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(38, 38);
            this.btnBack.TabIndex = 140;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnBack, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTrfChecklist, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExcel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDeliverChecklist, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1320, 50);
            this.tableLayoutPanel1.TabIndex = 141;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.Controls.Add(this.lblListTitle, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAutoInOut, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 50);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1320, 80);
            this.tableLayoutPanel2.TabIndex = 142;
            // 
            // lblListTitle
            // 
            this.lblListTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListTitle.Location = new System.Drawing.Point(3, 62);
            this.lblListTitle.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(71, 13);
            this.lblListTitle.TabIndex = 142;
            this.lblListTitle.Text = "PICKING LIST";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel3.Controls.Add(this.dtpTrfDate, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblTrfDate, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(930, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(130, 80);
            this.tableLayoutPanel3.TabIndex = 143;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel4.Controls.Add(this.btnItemCheck, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.cbCheckAll, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1190, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(130, 80);
            this.tableLayoutPanel4.TabIndex = 144;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.tlpMatChecklist, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(15);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1320, 715);
            this.tableLayoutPanel5.TabIndex = 143;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1350, 745);
            this.tableLayoutPanel6.TabIndex = 144;
            // 
            // frmMatCheckList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1350, 745);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMatCheckList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Internal Transfer Checklist";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMatCheckList_Load);
            this.tlpMatChecklist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliver)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpMatChecklist;
        private System.Windows.Forms.DataGridView dgvTransfer;
        private System.Windows.Forms.DataGridView dgvDeliver;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnDeliverChecklist;
        private System.Windows.Forms.Button btnTrfChecklist;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAutoInOut;
        private System.Windows.Forms.Button btnItemCheck;
        private System.Windows.Forms.CheckBox cbCheckAll;
        private System.Windows.Forms.DateTimePicker dtpTrfDate;
        private System.Windows.Forms.Label lblTrfDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    }
}