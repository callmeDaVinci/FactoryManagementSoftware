namespace FactoryManagementSoftware.UI
{
    partial class frmMatPlanningList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPrepare = new System.Windows.Forms.Button();
            this.btnCheckList = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnByPlan = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnByMat = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblUpdatedTime = new System.Windows.Forms.Label();
            this.tlpHeaderButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddMat = new System.Windows.Forms.Button();
            this.lblFilter = new System.Windows.Forms.Label();
            this.tlpMaterialList = new System.Windows.Forms.TableLayoutPanel();
            this.dgvMatListByPlan = new System.Windows.Forms.DataGridView();
            this.dgvMatListByMat = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFilterApply = new System.Windows.Forms.Button();
            this.cbFilterCarton = new System.Windows.Forms.CheckBox();
            this.cbFilterPart = new System.Windows.Forms.CheckBox();
            this.cbFilterSubMaterial = new System.Windows.Forms.CheckBox();
            this.cbFilterPigment = new System.Windows.Forms.CheckBox();
            this.cbFilterRawMaterial = new System.Windows.Forms.CheckBox();
            this.cbFilterMasterBatch = new System.Windows.Forms.CheckBox();
            this.tlpHeaderButton.SuspendLayout();
            this.tlpMaterialList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatListByPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatListByMat)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrepare
            // 
            this.btnPrepare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrepare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnPrepare.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrepare.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrepare.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrepare.ForeColor = System.Drawing.Color.Black;
            this.btnPrepare.Location = new System.Drawing.Point(1252, 23);
            this.btnPrepare.Margin = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.btnPrepare.Name = "btnPrepare";
            this.btnPrepare.Size = new System.Drawing.Size(140, 40);
            this.btnPrepare.TabIndex = 132;
            this.btnPrepare.Text = "PREPARE";
            this.btnPrepare.UseVisualStyleBackColor = false;
            this.btnPrepare.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnCheckList
            // 
            this.btnCheckList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnCheckList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckList.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckList.ForeColor = System.Drawing.Color.White;
            this.btnCheckList.Location = new System.Drawing.Point(1103, 23);
            this.btnCheckList.Margin = new System.Windows.Forms.Padding(4, 2, 0, 1);
            this.btnCheckList.Name = "btnCheckList";
            this.btnCheckList.Size = new System.Drawing.Size(140, 40);
            this.btnCheckList.TabIndex = 135;
            this.btnCheckList.Text = "CHECK LIST";
            this.btnCheckList.UseVisualStyleBackColor = false;
            this.btnCheckList.Click += new System.EventHandler(this.btnCheckList_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 38);
            this.label2.TabIndex = 136;
            this.label2.Text = "MATERIAL REQUIRED LIST";
            // 
            // btnByPlan
            // 
            this.btnByPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnByPlan.BackColor = System.Drawing.Color.White;
            this.btnByPlan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnByPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnByPlan.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnByPlan.ForeColor = System.Drawing.Color.Black;
            this.btnByPlan.Location = new System.Drawing.Point(197, 23);
            this.btnByPlan.Margin = new System.Windows.Forms.Padding(0, 1, 4, 1);
            this.btnByPlan.Name = "btnByPlan";
            this.btnByPlan.Size = new System.Drawing.Size(140, 40);
            this.btnByPlan.TabIndex = 137;
            this.btnByPlan.Text = "SORT BY PLAN";
            this.btnByPlan.UseVisualStyleBackColor = false;
            this.btnByPlan.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1402, 1);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 35);
            this.btnSave.TabIndex = 138;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnByMat
            // 
            this.btnByMat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnByMat.BackColor = System.Drawing.Color.White;
            this.btnByMat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnByMat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnByMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnByMat.ForeColor = System.Drawing.Color.Black;
            this.btnByMat.Location = new System.Drawing.Point(47, 23);
            this.btnByMat.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnByMat.Name = "btnByMat";
            this.btnByMat.Size = new System.Drawing.Size(140, 40);
            this.btnByMat.TabIndex = 142;
            this.btnByMat.Text = "SORT BY MATERIAL";
            this.btnByMat.UseVisualStyleBackColor = false;
            this.btnByMat.Click += new System.EventHandler(this.btnByMat_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_go_back_64;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(2, 24);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(38, 38);
            this.button2.TabIndex = 141;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_refresh_480;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(120, 2);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(36, 36);
            this.btnRefresh.TabIndex = 139;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 12);
            this.label7.TabIndex = 144;
            this.label7.Text = "LAST UPDATED:";
            // 
            // lblUpdatedTime
            // 
            this.lblUpdatedTime.AutoSize = true;
            this.lblUpdatedTime.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdatedTime.Location = new System.Drawing.Point(3, 20);
            this.lblUpdatedTime.Name = "lblUpdatedTime";
            this.lblUpdatedTime.Size = new System.Drawing.Size(125, 12);
            this.lblUpdatedTime.TabIndex = 143;
            this.lblUpdatedTime.Text = "SHOW DATA FOR THE PAST";
            // 
            // tlpHeaderButton
            // 
            this.tlpHeaderButton.ColumnCount = 7;
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpHeaderButton.Controls.Add(this.btnAddMat, 6, 0);
            this.tlpHeaderButton.Controls.Add(this.btnByMat, 1, 0);
            this.tlpHeaderButton.Controls.Add(this.btnByPlan, 2, 0);
            this.tlpHeaderButton.Controls.Add(this.btnCheckList, 4, 0);
            this.tlpHeaderButton.Controls.Add(this.btnPrepare, 5, 0);
            this.tlpHeaderButton.Controls.Add(this.button2, 0, 0);
            this.tlpHeaderButton.Controls.Add(this.lblFilter, 3, 0);
            this.tlpHeaderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeaderButton.Location = new System.Drawing.Point(20, 3);
            this.tlpHeaderButton.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.tlpHeaderButton.Name = "tlpHeaderButton";
            this.tlpHeaderButton.RowCount = 1;
            this.tlpHeaderButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeaderButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tlpHeaderButton.Size = new System.Drawing.Size(1542, 64);
            this.tlpHeaderButton.TabIndex = 145;
            this.tlpHeaderButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tlpHeaderButton_MouseClick);
            // 
            // btnAddMat
            // 
            this.btnAddMat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMat.BackColor = System.Drawing.Color.White;
            this.btnAddMat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMat.ForeColor = System.Drawing.Color.Black;
            this.btnAddMat.Location = new System.Drawing.Point(1402, 23);
            this.btnAddMat.Margin = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.btnAddMat.Name = "btnAddMat";
            this.btnAddMat.Size = new System.Drawing.Size(140, 40);
            this.btnAddMat.TabIndex = 146;
            this.btnAddMat.Text = "ADD MATERIAL";
            this.btnAddMat.UseVisualStyleBackColor = false;
            this.btnAddMat.Click += new System.EventHandler(this.btnAddMat_Click);
            // 
            // lblFilter
            // 
            this.lblFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFilter.AutoSize = true;
            this.lblFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.ForeColor = System.Drawing.Color.Blue;
            this.lblFilter.Location = new System.Drawing.Point(354, 40);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(91, 19);
            this.lblFilter.TabIndex = 147;
            this.lblFilter.Text = "MORE FILTER";
            this.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFilter.Click += new System.EventHandler(this.lblFilter_Click);
            // 
            // tlpMaterialList
            // 
            this.tlpMaterialList.ColumnCount = 2;
            this.tlpMaterialList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMaterialList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMaterialList.Controls.Add(this.dgvMatListByPlan, 1, 0);
            this.tlpMaterialList.Controls.Add(this.dgvMatListByMat, 0, 0);
            this.tlpMaterialList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMaterialList.Location = new System.Drawing.Point(20, 230);
            this.tlpMaterialList.Margin = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.tlpMaterialList.Name = "tlpMaterialList";
            this.tlpMaterialList.RowCount = 1;
            this.tlpMaterialList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMaterialList.Size = new System.Drawing.Size(1542, 603);
            this.tlpMaterialList.TabIndex = 146;
            // 
            // dgvMatListByPlan
            // 
            this.dgvMatListByPlan.AllowUserToAddRows = false;
            this.dgvMatListByPlan.AllowUserToDeleteRows = false;
            this.dgvMatListByPlan.AllowUserToOrderColumns = true;
            this.dgvMatListByPlan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMatListByPlan.BackgroundColor = System.Drawing.Color.DimGray;
            this.dgvMatListByPlan.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMatListByPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatListByPlan.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatListByPlan.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMatListByPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatListByPlan.GridColor = System.Drawing.Color.White;
            this.dgvMatListByPlan.Location = new System.Drawing.Point(771, 0);
            this.dgvMatListByPlan.Margin = new System.Windows.Forms.Padding(0);
            this.dgvMatListByPlan.Name = "dgvMatListByPlan";
            this.dgvMatListByPlan.ReadOnly = true;
            this.dgvMatListByPlan.RowHeadersVisible = false;
            this.dgvMatListByPlan.RowTemplate.Height = 40;
            this.dgvMatListByPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatListByPlan.Size = new System.Drawing.Size(771, 603);
            this.dgvMatListByPlan.TabIndex = 136;
            this.dgvMatListByPlan.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMatList_CellFormatting);
            this.dgvMatListByPlan.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMatListByPlan_CellMouseClick);
            this.dgvMatListByPlan.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMatListByPlan_CellMouseDown);
            this.dgvMatListByPlan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvMatListByPlan_MouseClick);
            // 
            // dgvMatListByMat
            // 
            this.dgvMatListByMat.AllowUserToAddRows = false;
            this.dgvMatListByMat.AllowUserToDeleteRows = false;
            this.dgvMatListByMat.AllowUserToResizeColumns = false;
            this.dgvMatListByMat.AllowUserToResizeRows = false;
            this.dgvMatListByMat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMatListByMat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMatListByMat.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMatListByMat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatListByMat.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatListByMat.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMatListByMat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatListByMat.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvMatListByMat.Location = new System.Drawing.Point(0, 0);
            this.dgvMatListByMat.Margin = new System.Windows.Forms.Padding(0);
            this.dgvMatListByMat.MultiSelect = false;
            this.dgvMatListByMat.Name = "dgvMatListByMat";
            this.dgvMatListByMat.ReadOnly = true;
            this.dgvMatListByMat.RowHeadersVisible = false;
            this.dgvMatListByMat.RowTemplate.Height = 40;
            this.dgvMatListByMat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatListByMat.Size = new System.Drawing.Size(771, 603);
            this.dgvMatListByMat.TabIndex = 111;
            this.dgvMatListByMat.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatList_CellEndEdit);
            this.dgvMatListByMat.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatList_CellEnter);
            this.dgvMatListByMat.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMatList_CellFormatting);
            this.dgvMatListByMat.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMatListByMat_CellMouseClick);
            this.dgvMatListByMat.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMatListByPlan_CellMouseDown);
            this.dgvMatListByMat.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatList_CellValueChanged);
            this.dgvMatListByMat.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvMatList_CurrentCellDirtyStateChanged);
            this.dgvMatListByMat.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMatList_DataError);
            this.dgvMatListByMat.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvMatListByMat_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tlpMaterialList, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tlpHeaderButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(20, 20, 3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1582, 853);
            this.tableLayoutPanel1.TabIndex = 147;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnRefresh, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSave, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(20, 173);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1542, 44);
            this.tableLayoutPanel3.TabIndex = 149;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblUpdatedTime, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(166, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1223, 38);
            this.tableLayoutPanel2.TabIndex = 148;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFilterApply);
            this.groupBox1.Controls.Add(this.cbFilterCarton);
            this.groupBox1.Controls.Add(this.cbFilterPart);
            this.groupBox1.Controls.Add(this.cbFilterSubMaterial);
            this.groupBox1.Controls.Add(this.cbFilterPigment);
            this.groupBox1.Controls.Add(this.cbFilterRawMaterial);
            this.groupBox1.Controls.Add(this.cbFilterMasterBatch);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.groupBox1.Location = new System.Drawing.Point(20, 73);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(20, 3, 20, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1542, 91);
            this.groupBox1.TabIndex = 150;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTER";
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.BackColor = System.Drawing.Color.White;
            this.btnFilterApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterApply.ForeColor = System.Drawing.Color.Black;
            this.btnFilterApply.Location = new System.Drawing.Point(684, 29);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.Size = new System.Drawing.Size(140, 40);
            this.btnFilterApply.TabIndex = 148;
            this.btnFilterApply.Text = "APPLY";
            this.btnFilterApply.UseVisualStyleBackColor = false;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // cbFilterCarton
            // 
            this.cbFilterCarton.AutoSize = true;
            this.cbFilterCarton.Location = new System.Drawing.Point(572, 39);
            this.cbFilterCarton.Name = "cbFilterCarton";
            this.cbFilterCarton.Size = new System.Drawing.Size(73, 23);
            this.cbFilterCarton.TabIndex = 11;
            this.cbFilterCarton.Text = "Carton";
            this.cbFilterCarton.UseVisualStyleBackColor = true;
            this.cbFilterCarton.CheckedChanged += new System.EventHandler(this.cbFilterCarton_CheckedChanged_1);
            // 
            // cbFilterPart
            // 
            this.cbFilterPart.AutoSize = true;
            this.cbFilterPart.Checked = true;
            this.cbFilterPart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterPart.Location = new System.Drawing.Point(358, 39);
            this.cbFilterPart.Name = "cbFilterPart";
            this.cbFilterPart.Size = new System.Drawing.Size(56, 23);
            this.cbFilterPart.TabIndex = 10;
            this.cbFilterPart.Text = "Part";
            this.cbFilterPart.UseVisualStyleBackColor = true;
            this.cbFilterPart.CheckedChanged += new System.EventHandler(this.cbFilterPart_CheckedChanged_1);
            // 
            // cbFilterSubMaterial
            // 
            this.cbFilterSubMaterial.AutoSize = true;
            this.cbFilterSubMaterial.Checked = true;
            this.cbFilterSubMaterial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterSubMaterial.Location = new System.Drawing.Point(444, 39);
            this.cbFilterSubMaterial.Name = "cbFilterSubMaterial";
            this.cbFilterSubMaterial.Size = new System.Drawing.Size(108, 23);
            this.cbFilterSubMaterial.TabIndex = 9;
            this.cbFilterSubMaterial.Text = "Sub Material";
            this.cbFilterSubMaterial.UseVisualStyleBackColor = true;
            this.cbFilterSubMaterial.CheckedChanged += new System.EventHandler(this.cbFilterSubMaterial_CheckedChanged_1);
            // 
            // cbFilterPigment
            // 
            this.cbFilterPigment.AutoSize = true;
            this.cbFilterPigment.Checked = true;
            this.cbFilterPigment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterPigment.Location = new System.Drawing.Point(255, 39);
            this.cbFilterPigment.Name = "cbFilterPigment";
            this.cbFilterPigment.Size = new System.Drawing.Size(82, 23);
            this.cbFilterPigment.TabIndex = 8;
            this.cbFilterPigment.Text = "Pigment";
            this.cbFilterPigment.UseVisualStyleBackColor = true;
            this.cbFilterPigment.CheckedChanged += new System.EventHandler(this.cbFilterPigment_CheckedChanged_1);
            // 
            // cbFilterRawMaterial
            // 
            this.cbFilterRawMaterial.AutoSize = true;
            this.cbFilterRawMaterial.Checked = true;
            this.cbFilterRawMaterial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterRawMaterial.Location = new System.Drawing.Point(6, 39);
            this.cbFilterRawMaterial.Name = "cbFilterRawMaterial";
            this.cbFilterRawMaterial.Size = new System.Drawing.Size(110, 23);
            this.cbFilterRawMaterial.TabIndex = 7;
            this.cbFilterRawMaterial.Text = "Raw Material";
            this.cbFilterRawMaterial.UseVisualStyleBackColor = true;
            this.cbFilterRawMaterial.CheckedChanged += new System.EventHandler(this.cbFilterRawMaterial_CheckedChanged_1);
            // 
            // cbFilterMasterBatch
            // 
            this.cbFilterMasterBatch.AutoSize = true;
            this.cbFilterMasterBatch.Checked = true;
            this.cbFilterMasterBatch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFilterMasterBatch.Location = new System.Drawing.Point(122, 39);
            this.cbFilterMasterBatch.Name = "cbFilterMasterBatch";
            this.cbFilterMasterBatch.Size = new System.Drawing.Size(112, 23);
            this.cbFilterMasterBatch.TabIndex = 6;
            this.cbFilterMasterBatch.Text = "Master Batch";
            this.cbFilterMasterBatch.UseVisualStyleBackColor = true;
            this.cbFilterMasterBatch.CheckedChanged += new System.EventHandler(this.cbFilterMasterBatch_CheckedChanged_1);
            // 
            // frmMatPlanningList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMatPlanningList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMatPlanningList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMatPlanningList_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMatPlanningList_MouseClick);
            this.tlpHeaderButton.ResumeLayout(false);
            this.tlpHeaderButton.PerformLayout();
            this.tlpMaterialList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatListByPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatListByMat)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPrepare;
        private System.Windows.Forms.Button btnCheckList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnByPlan;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnByMat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblUpdatedTime;
        private System.Windows.Forms.TableLayoutPanel tlpHeaderButton;
        private System.Windows.Forms.Button btnAddMat;
        private System.Windows.Forms.TableLayoutPanel tlpMaterialList;
        private System.Windows.Forms.DataGridView dgvMatListByMat;
        private System.Windows.Forms.DataGridView dgvMatListByPlan;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbFilterMasterBatch;
        private System.Windows.Forms.CheckBox cbFilterCarton;
        private System.Windows.Forms.CheckBox cbFilterPart;
        private System.Windows.Forms.CheckBox cbFilterSubMaterial;
        private System.Windows.Forms.CheckBox cbFilterPigment;
        private System.Windows.Forms.CheckBox cbFilterRawMaterial;
        private System.Windows.Forms.Button btnFilterApply;
    }
}