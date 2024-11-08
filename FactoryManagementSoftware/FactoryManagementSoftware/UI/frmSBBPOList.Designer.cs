namespace FactoryManagementSoftware.UI
{
    partial class frmSBBPOList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpPOList = new System.Windows.Forms.TableLayoutPanel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.cbCompletedPO = new System.Windows.Forms.CheckBox();
            this.cbInProgressPO = new System.Windows.Forms.CheckBox();
            this.btnFilterApply = new System.Windows.Forms.Button();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.tlpList = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btnConfirmToAddDO = new System.Windows.Forms.Button();
            this.btnBackToPOList = new System.Windows.Forms.Button();
            this.dgvDOList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancelDOMode = new System.Windows.Forms.Button();
            this.btnAddDO = new System.Windows.Forms.Button();
            this.dgvPOList = new System.Windows.Forms.DataGridView();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.cbEditInPcsUnit = new System.Windows.Forms.CheckBox();
            this.cbEditInBagUnit = new System.Windows.Forms.CheckBox();
            this.lblSubList = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddNewPO = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblMainList = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpPOList.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpList.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDOList)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpPOList
            // 
            this.tlpPOList.ColumnCount = 1;
            this.tlpPOList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPOList.Controls.Add(this.gbFilter, 0, 1);
            this.tlpPOList.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpPOList.Controls.Add(this.tlpList, 0, 3);
            this.tlpPOList.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tlpPOList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPOList.Location = new System.Drawing.Point(30, 30);
            this.tlpPOList.Margin = new System.Windows.Forms.Padding(30);
            this.tlpPOList.Name = "tlpPOList";
            this.tlpPOList.RowCount = 4;
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPOList.Size = new System.Drawing.Size(1288, 661);
            this.tlpPOList.TabIndex = 171;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.cbCompletedPO);
            this.gbFilter.Controls.Add(this.cbInProgressPO);
            this.gbFilter.Controls.Add(this.btnFilterApply);
            this.gbFilter.Controls.Add(this.cmbCustomer);
            this.gbFilter.Controls.Add(this.lblCustomer);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 54);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1282, 119);
            this.gbFilter.TabIndex = 172;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "FILTER";
            // 
            // cbCompletedPO
            // 
            this.cbCompletedPO.AutoSize = true;
            this.cbCompletedPO.Location = new System.Drawing.Point(54, 69);
            this.cbCompletedPO.Name = "cbCompletedPO";
            this.cbCompletedPO.Size = new System.Drawing.Size(129, 21);
            this.cbCompletedPO.TabIndex = 158;
            this.cbCompletedPO.Text = "COMPLETED P/O";
            this.cbCompletedPO.UseVisualStyleBackColor = true;
            this.cbCompletedPO.CheckedChanged += new System.EventHandler(this.cbCompletedPO_CheckedChanged);
            // 
            // cbInProgressPO
            // 
            this.cbInProgressPO.AutoSize = true;
            this.cbInProgressPO.Checked = true;
            this.cbInProgressPO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInProgressPO.Location = new System.Drawing.Point(54, 29);
            this.cbInProgressPO.Name = "cbInProgressPO";
            this.cbInProgressPO.Size = new System.Drawing.Size(136, 21);
            this.cbInProgressPO.TabIndex = 157;
            this.cbInProgressPO.Text = "IN PROGRESS P/O";
            this.cbInProgressPO.UseVisualStyleBackColor = true;
            this.cbInProgressPO.CheckedChanged += new System.EventHandler(this.cbInProgressPO_CheckedChanged);
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFilterApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterApply.ForeColor = System.Drawing.Color.White;
            this.btnFilterApply.Location = new System.Drawing.Point(616, 54);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.Size = new System.Drawing.Size(124, 36);
            this.btnFilterApply.TabIndex = 145;
            this.btnFilterApply.Text = "APPLY";
            this.btnFilterApply.UseVisualStyleBackColor = false;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(272, 54);
            this.cmbCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(298, 25);
            this.cmbCustomer.TabIndex = 156;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(268, 31);
            this.lblCustomer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(75, 17);
            this.lblCustomer.TabIndex = 155;
            this.lblCustomer.Text = "CUSTOMER";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel2.Controls.Add(this.btnRefresh, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnBack, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExcel, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1282, 45);
            this.tableLayoutPanel2.TabIndex = 160;
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
            this.btnRefresh.Location = new System.Drawing.Point(185, 4);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(36, 36);
            this.btnRefresh.TabIndex = 174;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilter.BackColor = System.Drawing.Color.White;
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Location = new System.Drawing.Point(54, 4);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(124, 36);
            this.btnFilter.TabIndex = 172;
            this.btnFilter.Text = "SHOW FILTER...";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Image = global::FactoryManagementSoftware.Properties.Resources.icons8_back_32__1_;
            this.btnBack.Location = new System.Drawing.Point(4, 4);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(36, 36);
            this.btnBack.TabIndex = 142;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1155, 4);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 5);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(124, 36);
            this.btnExcel.TabIndex = 157;
            this.btnExcel.Text = "EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Visible = false;
            this.btnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // tlpList
            // 
            this.tlpList.ColumnCount = 4;
            this.tlpList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpList.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tlpList.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tlpList.Controls.Add(this.dgvItemList, 3, 0);
            this.tlpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpList.Location = new System.Drawing.Point(3, 229);
            this.tlpList.Name = "tlpList";
            this.tlpList.RowCount = 1;
            this.tlpList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpList.Size = new System.Drawing.Size(1282, 429);
            this.tlpList.TabIndex = 163;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.dgvDOList, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(321, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(248, 423);
            this.tableLayoutPanel6.TabIndex = 173;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel7.Controls.Add(this.btnConfirmToAddDO, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnBackToPOList, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 366);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(242, 54);
            this.tableLayoutPanel7.TabIndex = 172;
            // 
            // btnConfirmToAddDO
            // 
            this.btnConfirmToAddDO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmToAddDO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnConfirmToAddDO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmToAddDO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirmToAddDO.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmToAddDO.ForeColor = System.Drawing.Color.White;
            this.btnConfirmToAddDO.Location = new System.Drawing.Point(123, 9);
            this.btnConfirmToAddDO.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.btnConfirmToAddDO.Name = "btnConfirmToAddDO";
            this.btnConfirmToAddDO.Size = new System.Drawing.Size(116, 45);
            this.btnConfirmToAddDO.TabIndex = 172;
            this.btnConfirmToAddDO.Text = "D/O CONFIRM";
            this.btnConfirmToAddDO.UseVisualStyleBackColor = false;
            this.btnConfirmToAddDO.Click += new System.EventHandler(this.btnConfirmToAddDO_Click);
            // 
            // btnBackToPOList
            // 
            this.btnBackToPOList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackToPOList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnBackToPOList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackToPOList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBackToPOList.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToPOList.ForeColor = System.Drawing.Color.Black;
            this.btnBackToPOList.Location = new System.Drawing.Point(3, 9);
            this.btnBackToPOList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.btnBackToPOList.Name = "btnBackToPOList";
            this.btnBackToPOList.Size = new System.Drawing.Size(114, 45);
            this.btnBackToPOList.TabIndex = 172;
            this.btnBackToPOList.Text = "<- P/O LIST";
            this.btnBackToPOList.UseVisualStyleBackColor = false;
            this.btnBackToPOList.Click += new System.EventHandler(this.btnBackToPOList_Click);
            // 
            // dgvDOList
            // 
            this.dgvDOList.AllowUserToAddRows = false;
            this.dgvDOList.AllowUserToDeleteRows = false;
            this.dgvDOList.AllowUserToOrderColumns = true;
            this.dgvDOList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDOList.BackgroundColor = System.Drawing.Color.White;
            this.dgvDOList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvDOList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDOList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDOList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDOList.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDOList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDOList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvDOList.Location = new System.Drawing.Point(3, 1);
            this.dgvDOList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvDOList.Name = "dgvDOList";
            this.dgvDOList.ReadOnly = true;
            this.dgvDOList.RowHeadersVisible = false;
            this.dgvDOList.RowHeadersWidth = 51;
            this.dgvDOList.RowTemplate.Height = 50;
            this.dgvDOList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDOList.Size = new System.Drawing.Size(242, 361);
            this.dgvDOList.TabIndex = 156;
            this.dgvDOList.DataSourceChanged += new System.EventHandler(this.dgvDOList_DataSourceChanged);
            this.dgvDOList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDOList_CellClick);
            this.dgvDOList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDOList_DataBindingComplete);
            this.dgvDOList.SelectionChanged += new System.EventHandler(this.dgvDOList_SelectionChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.dgvPOList, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(312, 423);
            this.tableLayoutPanel3.TabIndex = 172;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel5.Controls.Add(this.btnCancelDOMode, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnAddDO, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 366);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(306, 54);
            this.tableLayoutPanel5.TabIndex = 172;
            // 
            // btnCancelDOMode
            // 
            this.btnCancelDOMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelDOMode.BackColor = System.Drawing.Color.White;
            this.btnCancelDOMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelDOMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelDOMode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelDOMode.ForeColor = System.Drawing.Color.Black;
            this.btnCancelDOMode.Location = new System.Drawing.Point(60, 9);
            this.btnCancelDOMode.Margin = new System.Windows.Forms.Padding(4, 1, 4, 0);
            this.btnCancelDOMode.Name = "btnCancelDOMode";
            this.btnCancelDOMode.Size = new System.Drawing.Size(82, 45);
            this.btnCancelDOMode.TabIndex = 172;
            this.btnCancelDOMode.Text = "CANCEL";
            this.btnCancelDOMode.UseVisualStyleBackColor = false;
            this.btnCancelDOMode.Visible = false;
            this.btnCancelDOMode.Click += new System.EventHandler(this.btnCancelDOMode_Click);
            // 
            // btnAddDO
            // 
            this.btnAddDO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnAddDO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddDO.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDO.ForeColor = System.Drawing.Color.Black;
            this.btnAddDO.Location = new System.Drawing.Point(153, 9);
            this.btnAddDO.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.btnAddDO.Name = "btnAddDO";
            this.btnAddDO.Size = new System.Drawing.Size(150, 45);
            this.btnAddDO.TabIndex = 172;
            this.btnAddDO.Text = "ADD D/O";
            this.btnAddDO.UseVisualStyleBackColor = false;
            this.btnAddDO.Click += new System.EventHandler(this.btnAddDO_Click);
            // 
            // dgvPOList
            // 
            this.dgvPOList.AllowUserToAddRows = false;
            this.dgvPOList.AllowUserToDeleteRows = false;
            this.dgvPOList.AllowUserToOrderColumns = true;
            this.dgvPOList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvPOList.BackgroundColor = System.Drawing.Color.White;
            this.dgvPOList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvPOList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvPOList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPOList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPOList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPOList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPOList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvPOList.Location = new System.Drawing.Point(3, 1);
            this.dgvPOList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvPOList.Name = "dgvPOList";
            this.dgvPOList.ReadOnly = true;
            this.dgvPOList.RowHeadersVisible = false;
            this.dgvPOList.RowHeadersWidth = 51;
            this.dgvPOList.RowTemplate.Height = 50;
            this.dgvPOList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPOList.Size = new System.Drawing.Size(306, 361);
            this.dgvPOList.TabIndex = 156;
            this.dgvPOList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPOList_CellClick);
            this.dgvPOList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPOList_CellContentClick);
            this.dgvPOList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPOList_CellDoubleClick);
            this.dgvPOList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPOList_CellFormatting);
            this.dgvPOList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPOList_CellMouseDown);
            this.dgvPOList.SelectionChanged += new System.EventHandler(this.dgvPOList_SelectionChanged);
            // 
            // dgvItemList
            // 
            this.dgvItemList.AllowUserToAddRows = false;
            this.dgvItemList.AllowUserToDeleteRows = false;
            this.dgvItemList.AllowUserToOrderColumns = true;
            this.dgvItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvItemList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvItemList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvItemList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItemList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvItemList.Location = new System.Drawing.Point(585, 1);
            this.dgvItemList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.RowHeadersVisible = false;
            this.dgvItemList.RowHeadersWidth = 51;
            this.dgvItemList.RowTemplate.Height = 50;
            this.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemList.Size = new System.Drawing.Size(694, 427);
            this.dgvItemList.TabIndex = 155;
            this.dgvItemList.DataSourceChanged += new System.EventHandler(this.dgvItemList_DataSourceChanged);
            this.dgvItemList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellEndEdit);
            this.dgvItemList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvItemList_CellFormatting);
            this.dgvItemList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPOItem_CellMouseDown);
            this.dgvItemList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvItemList_DataBindingComplete);
            this.dgvItemList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvItemList_EditingControlShowing);
            this.dgvItemList.SelectionChanged += new System.EventHandler(this.dgvItemList_SelectionChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel8, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblMainList, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 179);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1282, 44);
            this.tableLayoutPanel4.TabIndex = 167;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel8.Controls.Add(this.cbEditInPcsUnit, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.cbEditInBagUnit, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblSubList, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(584, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(695, 38);
            this.tableLayoutPanel8.TabIndex = 172;
            // 
            // cbEditInPcsUnit
            // 
            this.cbEditInPcsUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditInPcsUnit.AutoSize = true;
            this.cbEditInPcsUnit.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEditInPcsUnit.Location = new System.Drawing.Point(360, 14);
            this.cbEditInPcsUnit.Name = "cbEditInPcsUnit";
            this.cbEditInPcsUnit.Size = new System.Drawing.Size(132, 21);
            this.cbEditInPcsUnit.TabIndex = 159;
            this.cbEditInPcsUnit.Text = "EDIT IN PCS UNIT";
            this.cbEditInPcsUnit.UseVisualStyleBackColor = true;
            this.cbEditInPcsUnit.CheckedChanged += new System.EventHandler(this.cbEditInPcsUnit_CheckedChanged);
            // 
            // cbEditInBagUnit
            // 
            this.cbEditInBagUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditInBagUnit.AutoSize = true;
            this.cbEditInBagUnit.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEditInBagUnit.Location = new System.Drawing.Point(558, 14);
            this.cbEditInBagUnit.Name = "cbEditInBagUnit";
            this.cbEditInBagUnit.Size = new System.Drawing.Size(134, 21);
            this.cbEditInBagUnit.TabIndex = 158;
            this.cbEditInBagUnit.Text = "EDIT IN BAG UNIT";
            this.cbEditInBagUnit.UseVisualStyleBackColor = true;
            this.cbEditInBagUnit.CheckedChanged += new System.EventHandler(this.cbEditInBagUnit_CheckedChanged);
            // 
            // lblSubList
            // 
            this.lblSubList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSubList.AutoSize = true;
            this.lblSubList.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubList.Location = new System.Drawing.Point(2, 16);
            this.lblSubList.Margin = new System.Windows.Forms.Padding(2, 0, 2, 5);
            this.lblSubList.Name = "lblSubList";
            this.lblSubList.Size = new System.Drawing.Size(90, 17);
            this.lblSubList.TabIndex = 157;
            this.lblSubList.Text = "P/O ITEM LIST";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddNewPO, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(193, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(375, 38);
            this.tableLayoutPanel1.TabIndex = 172;
            // 
            // btnAddNewPO
            // 
            this.btnAddNewPO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewPO.BackColor = System.Drawing.Color.White;
            this.btnAddNewPO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewPO.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewPO.ForeColor = System.Drawing.Color.Black;
            this.btnAddNewPO.Location = new System.Drawing.Point(247, 1);
            this.btnAddNewPO.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnAddNewPO.Name = "btnAddNewPO";
            this.btnAddNewPO.Size = new System.Drawing.Size(124, 32);
            this.btnAddNewPO.TabIndex = 167;
            this.btnAddNewPO.Text = "+ NEW P/O";
            this.btnAddNewPO.UseVisualStyleBackColor = false;
            this.btnAddNewPO.Click += new System.EventHandler(this.btnAddNewPO_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackColor = System.Drawing.Color.White;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Location = new System.Drawing.Point(115, 1);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(124, 32);
            this.btnEdit.TabIndex = 167;
            this.btnEdit.Text = "EDIT";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblMainList
            // 
            this.lblMainList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMainList.AutoSize = true;
            this.lblMainList.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainList.Location = new System.Drawing.Point(2, 22);
            this.lblMainList.Margin = new System.Windows.Forms.Padding(2, 0, 2, 5);
            this.lblMainList.Name = "lblMainList";
            this.lblMainList.Size = new System.Drawing.Size(57, 17);
            this.lblMainList.TabIndex = 166;
            this.lblMainList.Text = "P/O LIST";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.tlpPOList, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1348, 721);
            this.tableLayoutPanel9.TabIndex = 172;
            // 
            // frmSBBPOList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.tableLayoutPanel9);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSBBPOList";
            this.Text = "PURCHASE ORDER LIST";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSPPPOList_FormClosed);
            this.Load += new System.EventHandler(this.frmSPPPOList_Load);
            this.Shown += new System.EventHandler(this.frmSBBPOList_Shown);
            this.tlpPOList.ResumeLayout(false);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tlpList.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDOList)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPOList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPOList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.TableLayoutPanel tlpList;
        private System.Windows.Forms.DataGridView dgvPOList;
        private System.Windows.Forms.DataGridView dgvItemList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnAddNewPO;
        private System.Windows.Forms.Label lblMainList;
        private System.Windows.Forms.Label lblSubList;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Button btnFilterApply;
        private System.Windows.Forms.CheckBox cbCompletedPO;
        private System.Windows.Forms.CheckBox cbInProgressPO;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnAddDO;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnCancelDOMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button btnConfirmToAddDO;
        private System.Windows.Forms.Button btnBackToPOList;
        private System.Windows.Forms.DataGridView dgvDOList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.CheckBox cbEditInPcsUnit;
        private System.Windows.Forms.CheckBox cbEditInBagUnit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
    }
}