namespace FactoryManagementSoftware.UI
{
    partial class frmMachineScheduleVer2
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
            this.dgvMacSchedule = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cbItem = new System.Windows.Forms.CheckBox();
            this.cbSearchByJobNo = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbMacLocation = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMac = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbDraft = new System.Windows.Forms.CheckBox();
            this.cbCompleted = new System.Windows.Forms.CheckBox();
            this.cbCancelled = new System.Windows.Forms.CheckBox();
            this.cbPending = new System.Windows.Forms.CheckBox();
            this.cbRunning = new System.Windows.Forms.CheckBox();
            this.cbWarning = new System.Windows.Forms.CheckBox();
            this.lblResetAll = new System.Windows.Forms.Label();
            this.tlpButton = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.lblManualSummary = new System.Windows.Forms.Label();
            this.gunaGradientButton1 = new Guna.UI.WinForms.GunaGradientButton();
            this.btnExcel = new Guna.UI.WinForms.GunaGradientButton();
            this.btnNewJob = new Guna.UI.WinForms.GunaGradientButton();
            this.btnFilter = new Guna.UI.WinForms.GunaGradientButton();
            this.lblSmallFontRow = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFilterApply = new Guna.UI.WinForms.GunaGradientButton();
            this.tlpMainSchedule = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAdjustCollisionDateBySystem = new Guna.UI.WinForms.GunaGradientButton();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMatStock2 = new System.Windows.Forms.Label();
            this.lblTotalToUse2 = new System.Windows.Forms.Label();
            this.lblTotalUsed2 = new System.Windows.Forms.Label();
            this.lblTotalPlannedToUse2 = new System.Windows.Forms.Label();
            this.lblMat2 = new System.Windows.Forms.Label();
            this.lblMat1 = new System.Windows.Forms.Label();
            this.lblTotalUsed1 = new System.Windows.Forms.Label();
            this.lblTotalToUse1 = new System.Windows.Forms.Label();
            this.lblMatStock1 = new System.Windows.Forms.Label();
            this.lblTotalPlannedToUse1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMacSchedule)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpButton.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tlpMainSchedule.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMacSchedule
            // 
            this.dgvMacSchedule.AllowDrop = true;
            this.dgvMacSchedule.AllowUserToAddRows = false;
            this.dgvMacSchedule.AllowUserToDeleteRows = false;
            this.dgvMacSchedule.AllowUserToOrderColumns = true;
            this.dgvMacSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMacSchedule.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.dgvMacSchedule.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMacSchedule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMacSchedule.ColumnHeadersHeight = 50;
            this.dgvMacSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMacSchedule.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMacSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMacSchedule.GridColor = System.Drawing.Color.Silver;
            this.dgvMacSchedule.Location = new System.Drawing.Point(2, 268);
            this.dgvMacSchedule.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.dgvMacSchedule.MultiSelect = false;
            this.dgvMacSchedule.Name = "dgvMacSchedule";
            this.dgvMacSchedule.RowHeadersVisible = false;
            this.dgvMacSchedule.RowHeadersWidth = 51;
            this.dgvMacSchedule.RowTemplate.Height = 60;
            this.dgvMacSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMacSchedule.Size = new System.Drawing.Size(1545, 359);
            this.dgvMacSchedule.TabIndex = 109;
            this.dgvMacSchedule.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellClick);
            this.dgvMacSchedule.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellDoubleClick);
            this.dgvMacSchedule.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellEndEdit);
            this.dgvMacSchedule.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMacSchedule_CellFormatting);
            this.dgvMacSchedule.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMacSchedule_CellMouseDoubleClick);
            this.dgvMacSchedule.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSchedule_CellMouseDown);
            this.dgvMacSchedule.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellValueChanged);
            this.dgvMacSchedule.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvSchedule_EditingControlShowing);
            this.dgvMacSchedule.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            this.dgvMacSchedule.DragOver += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragOver);
            this.dgvMacSchedule.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvMacSchedule_MouseClick);
            this.dgvMacSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            this.dgvMacSchedule.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseMove);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 110;
            this.label1.Text = "Machine Schedule";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic);
            this.groupBox1.Location = new System.Drawing.Point(2, 49);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(1545, 114);
            this.groupBox1.TabIndex = 113;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTER";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.4692F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.67104F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.80734F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.92136F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 21);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1541, 90);
            this.tableLayoutPanel1.TabIndex = 115;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(1234, 3);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(305, 84);
            this.groupBox2.TabIndex = 115;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SEARCH";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.txtSearch, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(2, 17);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(301, 64);
            this.tableLayoutPanel6.TabIndex = 116;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.txtSearch.Location = new System.Drawing.Point(2, 29);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(297, 25);
            this.txtSearch.TabIndex = 86;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.cbItem, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbSearchByJobNo, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(301, 26);
            this.tableLayoutPanel5.TabIndex = 115;
            // 
            // cbItem
            // 
            this.cbItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbItem.AutoSize = true;
            this.cbItem.Checked = true;
            this.cbItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbItem.Location = new System.Drawing.Point(2, 3);
            this.cbItem.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbItem.Name = "cbItem";
            this.cbItem.Size = new System.Drawing.Size(59, 20);
            this.cbItem.TabIndex = 116;
            this.cbItem.Text = "Item";
            this.cbItem.UseVisualStyleBackColor = true;
            this.cbItem.CheckedChanged += new System.EventHandler(this.cbItem_CheckedChanged);
            // 
            // cbSearchByJobNo
            // 
            this.cbSearchByJobNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSearchByJobNo.AutoSize = true;
            this.cbSearchByJobNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSearchByJobNo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSearchByJobNo.Location = new System.Drawing.Point(152, 3);
            this.cbSearchByJobNo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbSearchByJobNo.Name = "cbSearchByJobNo";
            this.cbSearchByJobNo.Size = new System.Drawing.Size(77, 20);
            this.cbSearchByJobNo.TabIndex = 117;
            this.cbSearchByJobNo.Text = "Job No.";
            this.cbSearchByJobNo.UseVisualStyleBackColor = true;
            this.cbSearchByJobNo.CheckedChanged += new System.EventHandler(this.cbPlanningID_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(944, 3);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox3.Size = new System.Drawing.Size(286, 84);
            this.groupBox3.TabIndex = 115;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "LOCATION";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.cmbMacLocation, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmbMac, 2, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2, 17);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(282, 64);
            this.tableLayoutPanel4.TabIndex = 115;
            // 
            // cmbMacLocation
            // 
            this.cmbMacLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMacLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbMacLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMacLocation.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbMacLocation.FormattingEnabled = true;
            this.cmbMacLocation.Location = new System.Drawing.Point(2, 29);
            this.cmbMacLocation.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbMacLocation.Name = "cmbMacLocation";
            this.cmbMacLocation.Size = new System.Drawing.Size(133, 25);
            this.cmbMacLocation.TabIndex = 72;
            this.cmbMacLocation.SelectedIndexChanged += new System.EventHandler(this.cmbFactory_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 19);
            this.label4.TabIndex = 115;
            this.label4.Text = "Factory";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label5.Location = new System.Drawing.Point(146, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 116;
            this.label5.Text = "Machine";
            // 
            // cmbMac
            // 
            this.cmbMac.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMac.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbMac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMac.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbMac.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbMac.FormattingEnabled = true;
            this.cmbMac.Location = new System.Drawing.Point(146, 29);
            this.cmbMac.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbMac.Name = "cmbMac";
            this.cmbMac.Size = new System.Drawing.Size(134, 25);
            this.cmbMac.TabIndex = 57;
            this.cmbMac.SelectedIndexChanged += new System.EventHandler(this.cmbMachine_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel3);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox4.Location = new System.Drawing.Point(533, 3);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox4.Size = new System.Drawing.Size(407, 84);
            this.groupBox4.TabIndex = 116;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PERIOD";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel9, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.dtpTo, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.dtpFrom, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 17);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(403, 64);
            this.tableLayoutPanel3.TabIndex = 115;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.89781F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.10219F));
            this.tableLayoutPanel9.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(205, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(198, 26);
            this.tableLayoutPanel9.TabIndex = 169;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(157, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 169;
            this.label6.Text = "Today";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label3.Location = new System.Drawing.Point(2, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 19);
            this.label3.TabIndex = 117;
            this.label3.Text = "To";
            // 
            // dtpTo
            // 
            this.dtpTo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(207, 29);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(194, 25);
            this.dtpTo.TabIndex = 1;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CalendarMonthBackground = System.Drawing.Color.Transparent;
            this.dtpFrom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(2, 29);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(194, 25);
            this.dtpFrom.TabIndex = 0;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.Location = new System.Drawing.Point(2, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 19);
            this.label2.TabIndex = 116;
            this.label2.Text = "From";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel2);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox5.Location = new System.Drawing.Point(2, 3);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox5.Size = new System.Drawing.Size(527, 84);
            this.groupBox5.TabIndex = 117;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "STATUS";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel2.Controls.Add(this.cbDraft, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbCompleted, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbCancelled, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbPending, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbRunning, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbWarning, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 17);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(523, 64);
            this.tableLayoutPanel2.TabIndex = 115;
            // 
            // cbDraft
            // 
            this.cbDraft.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbDraft.AutoSize = true;
            this.cbDraft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDraft.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbDraft.Location = new System.Drawing.Point(351, 35);
            this.cbDraft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbDraft.Name = "cbDraft";
            this.cbDraft.Size = new System.Drawing.Size(62, 23);
            this.cbDraft.TabIndex = 169;
            this.cbDraft.Text = "Draft";
            this.cbDraft.UseVisualStyleBackColor = true;
            // 
            // cbCompleted
            // 
            this.cbCompleted.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCompleted.AutoSize = true;
            this.cbCompleted.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCompleted.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbCompleted.Location = new System.Drawing.Point(2, 35);
            this.cbCompleted.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbCompleted.Name = "cbCompleted";
            this.cbCompleted.Size = new System.Drawing.Size(98, 23);
            this.cbCompleted.TabIndex = 97;
            this.cbCompleted.Text = "Completed";
            this.cbCompleted.UseVisualStyleBackColor = true;
            this.cbCompleted.CheckedChanged += new System.EventHandler(this.cbCompleted_CheckedChanged);
            // 
            // cbCancelled
            // 
            this.cbCancelled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCancelled.AutoSize = true;
            this.cbCancelled.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCancelled.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbCancelled.Location = new System.Drawing.Point(179, 35);
            this.cbCancelled.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbCancelled.Name = "cbCancelled";
            this.cbCancelled.Size = new System.Drawing.Size(89, 23);
            this.cbCancelled.TabIndex = 98;
            this.cbCancelled.Text = "Cancelled";
            this.cbCancelled.UseVisualStyleBackColor = true;
            // 
            // cbPending
            // 
            this.cbPending.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPending.AutoSize = true;
            this.cbPending.Checked = true;
            this.cbPending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPending.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbPending.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbPending.Location = new System.Drawing.Point(2, 4);
            this.cbPending.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbPending.Name = "cbPending";
            this.cbPending.Size = new System.Drawing.Size(80, 23);
            this.cbPending.TabIndex = 94;
            this.cbPending.Text = "Pending";
            this.cbPending.UseVisualStyleBackColor = true;
            // 
            // cbRunning
            // 
            this.cbRunning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbRunning.AutoSize = true;
            this.cbRunning.Checked = true;
            this.cbRunning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRunning.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbRunning.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbRunning.Location = new System.Drawing.Point(179, 4);
            this.cbRunning.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbRunning.Name = "cbRunning";
            this.cbRunning.Size = new System.Drawing.Size(82, 23);
            this.cbRunning.TabIndex = 95;
            this.cbRunning.Text = "Running";
            this.cbRunning.UseVisualStyleBackColor = true;
            // 
            // cbWarning
            // 
            this.cbWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbWarning.AutoSize = true;
            this.cbWarning.Checked = true;
            this.cbWarning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWarning.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbWarning.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbWarning.ForeColor = System.Drawing.Color.Black;
            this.cbWarning.Location = new System.Drawing.Point(351, 4);
            this.cbWarning.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbWarning.Name = "cbWarning";
            this.cbWarning.Size = new System.Drawing.Size(82, 23);
            this.cbWarning.TabIndex = 96;
            this.cbWarning.Text = "Warning";
            this.cbWarning.UseVisualStyleBackColor = true;
            // 
            // lblResetAll
            // 
            this.lblResetAll.AutoSize = true;
            this.lblResetAll.BackColor = System.Drawing.Color.Transparent;
            this.lblResetAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblResetAll.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Underline);
            this.lblResetAll.ForeColor = System.Drawing.Color.Blue;
            this.lblResetAll.Location = new System.Drawing.Point(2, 3);
            this.lblResetAll.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblResetAll.Name = "lblResetAll";
            this.lblResetAll.Size = new System.Drawing.Size(61, 19);
            this.lblResetAll.TabIndex = 90;
            this.lblResetAll.Text = "Reset All";
            this.lblResetAll.Click += new System.EventHandler(this.ResetAll_Click);
            // 
            // tlpButton
            // 
            this.tlpButton.ColumnCount = 7;
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tlpButton.Controls.Add(this.groupBox6, 3, 0);
            this.tlpButton.Controls.Add(this.gunaGradientButton1, 2, 0);
            this.tlpButton.Controls.Add(this.btnExcel, 6, 0);
            this.tlpButton.Controls.Add(this.btnNewJob, 4, 0);
            this.tlpButton.Controls.Add(this.btnFilter, 0, 0);
            this.tlpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButton.Location = new System.Drawing.Point(0, 0);
            this.tlpButton.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButton.Name = "tlpButton";
            this.tlpButton.RowCount = 1;
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.Size = new System.Drawing.Size(1549, 46);
            this.tlpButton.TabIndex = 115;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tableLayoutPanel11);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(305, 0);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox6.Size = new System.Drawing.Size(984, 46);
            this.groupBox6.TabIndex = 169;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "MANUAL OPERATION SUMMARY";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.lblManualSummary, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(2, 17);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(980, 26);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // lblManualSummary
            // 
            this.lblManualSummary.AutoSize = true;
            this.lblManualSummary.BackColor = System.Drawing.Color.Transparent;
            this.lblManualSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblManualSummary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManualSummary.ForeColor = System.Drawing.Color.Blue;
            this.lblManualSummary.Location = new System.Drawing.Point(2, 3);
            this.lblManualSummary.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblManualSummary.Name = "lblManualSummary";
            this.lblManualSummary.Size = new System.Drawing.Size(396, 20);
            this.lblManualSummary.TabIndex = 170;
            this.lblManualSummary.Text = "Today (09 Sep) : 3 Manual ( A1(2) , B1 ) | This Week: 5 Total";
            this.lblManualSummary.Click += new System.EventHandler(this.LblManualSummary_Click);
            // 
            // gunaGradientButton1
            // 
            this.gunaGradientButton1.AnimationHoverSpeed = 0.07F;
            this.gunaGradientButton1.AnimationSpeed = 0.03F;
            this.gunaGradientButton1.BackColor = System.Drawing.Color.Transparent;
            this.gunaGradientButton1.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.gunaGradientButton1.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.gunaGradientButton1.BorderColor = System.Drawing.Color.Black;
            this.gunaGradientButton1.BorderSize = 1;
            this.gunaGradientButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaGradientButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGradientButton1.FocusedColor = System.Drawing.Color.Empty;
            this.gunaGradientButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaGradientButton1.ForeColor = System.Drawing.Color.White;
            this.gunaGradientButton1.Image = null;
            this.gunaGradientButton1.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaGradientButton1.Location = new System.Drawing.Point(157, 3);
            this.gunaGradientButton1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gunaGradientButton1.Name = "gunaGradientButton1";
            this.gunaGradientButton1.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.gunaGradientButton1.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.gunaGradientButton1.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaGradientButton1.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaGradientButton1.OnHoverImage = null;
            this.gunaGradientButton1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaGradientButton1.Radius = 2;
            this.gunaGradientButton1.Size = new System.Drawing.Size(141, 40);
            this.gunaGradientButton1.TabIndex = 225;
            this.gunaGradientButton1.Text = "Reload";
            this.gunaGradientButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gunaGradientButton1.Click += new System.EventHandler(this.gunaGradientButton1_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.AnimationHoverSpeed = 0.07F;
            this.btnExcel.AnimationSpeed = 0.03F;
            this.btnExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExcel.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(241)))), ((int)(((byte)(218)))));
            this.btnExcel.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(241)))), ((int)(((byte)(218)))));
            this.btnExcel.BorderColor = System.Drawing.Color.Black;
            this.btnExcel.BorderSize = 1;
            this.btnExcel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExcel.FocusedColor = System.Drawing.Color.Empty;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnExcel.Image = null;
            this.btnExcel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnExcel.Location = new System.Drawing.Point(1446, 3);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnExcel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnExcel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnExcel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnExcel.OnHoverImage = null;
            this.btnExcel.OnPressedColor = System.Drawing.Color.Black;
            this.btnExcel.Radius = 2;
            this.btnExcel.Size = new System.Drawing.Size(101, 40);
            this.btnExcel.TabIndex = 224;
            this.btnExcel.Text = "Excel";
            this.btnExcel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnNewJob
            // 
            this.btnNewJob.AnimationHoverSpeed = 0.07F;
            this.btnNewJob.AnimationSpeed = 0.03F;
            this.btnNewJob.BackColor = System.Drawing.Color.Transparent;
            this.btnNewJob.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(160)))));
            this.btnNewJob.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(160)))));
            this.btnNewJob.BorderColor = System.Drawing.Color.Black;
            this.btnNewJob.BorderSize = 1;
            this.btnNewJob.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnNewJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewJob.FocusedColor = System.Drawing.Color.Empty;
            this.btnNewJob.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNewJob.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnNewJob.Image = null;
            this.btnNewJob.ImageSize = new System.Drawing.Size(20, 20);
            this.btnNewJob.Location = new System.Drawing.Point(1296, 3);
            this.btnNewJob.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnNewJob.Name = "btnNewJob";
            this.btnNewJob.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnNewJob.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnNewJob.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnNewJob.OnHoverForeColor = System.Drawing.Color.White;
            this.btnNewJob.OnHoverImage = null;
            this.btnNewJob.OnPressedColor = System.Drawing.Color.Black;
            this.btnNewJob.Radius = 2;
            this.btnNewJob.Size = new System.Drawing.Size(136, 40);
            this.btnNewJob.TabIndex = 223;
            this.btnNewJob.Text = "New Job";
            this.btnNewJob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnNewJob.Click += new System.EventHandler(this.btnNewJob_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.AnimationHoverSpeed = 0.07F;
            this.btnFilter.AnimationSpeed = 0.03F;
            this.btnFilter.BackColor = System.Drawing.Color.Transparent;
            this.btnFilter.BaseColor1 = System.Drawing.Color.White;
            this.btnFilter.BaseColor2 = System.Drawing.Color.White;
            this.btnFilter.BorderColor = System.Drawing.Color.Black;
            this.btnFilter.BorderSize = 1;
            this.btnFilter.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFilter.FocusedColor = System.Drawing.Color.Empty;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnFilter.Image = null;
            this.btnFilter.ImageSize = new System.Drawing.Size(20, 20);
            this.btnFilter.Location = new System.Drawing.Point(2, 3);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnFilter.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnFilter.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFilter.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFilter.OnHoverImage = null;
            this.btnFilter.OnPressedColor = System.Drawing.Color.Black;
            this.btnFilter.Radius = 2;
            this.btnFilter.Size = new System.Drawing.Size(141, 40);
            this.btnFilter.TabIndex = 225;
            this.btnFilter.Text = "Show Filter";
            this.btnFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // lblSmallFontRow
            // 
            this.lblSmallFontRow.AutoSize = true;
            this.lblSmallFontRow.BackColor = System.Drawing.Color.Transparent;
            this.lblSmallFontRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSmallFontRow.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Underline);
            this.lblSmallFontRow.ForeColor = System.Drawing.Color.Blue;
            this.lblSmallFontRow.Location = new System.Drawing.Point(74, 3);
            this.lblSmallFontRow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblSmallFontRow.Name = "lblSmallFontRow";
            this.lblSmallFontRow.Size = new System.Drawing.Size(104, 19);
            this.lblSmallFontRow.TabIndex = 169;
            this.lblSmallFontRow.Text = "Small Font/Row";
            this.lblSmallFontRow.Click += new System.EventHandler(this.label7_Click);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 255F));
            this.tableLayoutPanel8.Controls.Add(this.lblSmallFontRow, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnFilterApply, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblResetAll, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 166);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1549, 46);
            this.tableLayoutPanel8.TabIndex = 116;
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.AnimationHoverSpeed = 0.07F;
            this.btnFilterApply.AnimationSpeed = 0.03F;
            this.btnFilterApply.BackColor = System.Drawing.Color.Transparent;
            this.btnFilterApply.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnFilterApply.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnFilterApply.BorderColor = System.Drawing.Color.Black;
            this.btnFilterApply.BorderSize = 1;
            this.btnFilterApply.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFilterApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFilterApply.FocusedColor = System.Drawing.Color.Empty;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilterApply.ForeColor = System.Drawing.Color.White;
            this.btnFilterApply.Image = null;
            this.btnFilterApply.ImageSize = new System.Drawing.Size(20, 20);
            this.btnFilterApply.Location = new System.Drawing.Point(1296, 3);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnFilterApply.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnFilterApply.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFilterApply.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFilterApply.OnHoverImage = null;
            this.btnFilterApply.OnPressedColor = System.Drawing.Color.Black;
            this.btnFilterApply.Radius = 2;
            this.btnFilterApply.Size = new System.Drawing.Size(251, 40);
            this.btnFilterApply.TabIndex = 224;
            this.btnFilterApply.Text = "Filter Apply";
            this.btnFilterApply.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilterApply.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // tlpMainSchedule
            // 
            this.tlpMainSchedule.ColumnCount = 1;
            this.tlpMainSchedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainSchedule.Controls.Add(this.label7, 0, 7);
            this.tlpMainSchedule.Controls.Add(this.btnAdjustCollisionDateBySystem, 0, 6);
            this.tlpMainSchedule.Controls.Add(this.tlpButton, 0, 0);
            this.tlpMainSchedule.Controls.Add(this.dgvMacSchedule, 0, 4);
            this.tlpMainSchedule.Controls.Add(this.groupBox1, 0, 1);
            this.tlpMainSchedule.Controls.Add(this.tableLayoutPanel8, 0, 2);
            this.tlpMainSchedule.Controls.Add(this.tableLayoutPanel10, 0, 3);
            this.tlpMainSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainSchedule.Location = new System.Drawing.Point(20, 10);
            this.tlpMainSchedule.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.tlpMainSchedule.Name = "tlpMainSchedule";
            this.tlpMainSchedule.RowCount = 8;
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMainSchedule.Size = new System.Drawing.Size(1549, 714);
            this.tlpMainSchedule.TabIndex = 117;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(1448, 698);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 170;
            this.label7.Text = "Switch To Old Version";
            this.label7.Click += new System.EventHandler(this.label7_Click_1);
            // 
            // btnAdjustCollisionDateBySystem
            // 
            this.btnAdjustCollisionDateBySystem.AnimationHoverSpeed = 0.07F;
            this.btnAdjustCollisionDateBySystem.AnimationSpeed = 0.03F;
            this.btnAdjustCollisionDateBySystem.BackColor = System.Drawing.Color.Transparent;
            this.btnAdjustCollisionDateBySystem.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(159)))));
            this.btnAdjustCollisionDateBySystem.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(159)))));
            this.btnAdjustCollisionDateBySystem.BorderColor = System.Drawing.Color.Black;
            this.btnAdjustCollisionDateBySystem.BorderSize = 1;
            this.btnAdjustCollisionDateBySystem.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdjustCollisionDateBySystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdjustCollisionDateBySystem.FocusedColor = System.Drawing.Color.Empty;
            this.btnAdjustCollisionDateBySystem.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnAdjustCollisionDateBySystem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnAdjustCollisionDateBySystem.Image = null;
            this.btnAdjustCollisionDateBySystem.ImageSize = new System.Drawing.Size(20, 20);
            this.btnAdjustCollisionDateBySystem.Location = new System.Drawing.Point(3, 641);
            this.btnAdjustCollisionDateBySystem.Name = "btnAdjustCollisionDateBySystem";
            this.btnAdjustCollisionDateBySystem.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnAdjustCollisionDateBySystem.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnAdjustCollisionDateBySystem.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnAdjustCollisionDateBySystem.OnHoverForeColor = System.Drawing.Color.White;
            this.btnAdjustCollisionDateBySystem.OnHoverImage = null;
            this.btnAdjustCollisionDateBySystem.OnPressedColor = System.Drawing.Color.Black;
            this.btnAdjustCollisionDateBySystem.Radius = 2;
            this.btnAdjustCollisionDateBySystem.Size = new System.Drawing.Size(1543, 50);
            this.btnAdjustCollisionDateBySystem.TabIndex = 225;
            this.btnAdjustCollisionDateBySystem.Text = "Adjust Collision Date";
            this.btnAdjustCollisionDateBySystem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnAdjustCollisionDateBySystem.Click += new System.EventHandler(this.btnAdjustCollisionDateBySystem_Click);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 6;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 238F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel10.Controls.Add(this.lblMatStock2, 5, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblTotalToUse2, 4, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblTotalUsed2, 3, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblTotalPlannedToUse2, 2, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblMat2, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblMat1, 1, 1);
            this.tableLayoutPanel10.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.lblTotalUsed1, 3, 1);
            this.tableLayoutPanel10.Controls.Add(this.lblTotalToUse1, 4, 1);
            this.tableLayoutPanel10.Controls.Add(this.lblMatStock1, 5, 1);
            this.tableLayoutPanel10.Controls.Add(this.lblTotalPlannedToUse1, 2, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(2, 215);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(1545, 49);
            this.tableLayoutPanel10.TabIndex = 119;
            // 
            // lblMatStock2
            // 
            this.lblMatStock2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMatStock2.AutoSize = true;
            this.lblMatStock2.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatStock2.ForeColor = System.Drawing.Color.Black;
            this.lblMatStock2.Location = new System.Drawing.Point(1297, 11);
            this.lblMatStock2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblMatStock2.Name = "lblMatStock2";
            this.lblMatStock2.Size = new System.Drawing.Size(41, 15);
            this.lblMatStock2.TabIndex = 171;
            this.lblMatStock2.Text = "Stock: ";
            // 
            // lblTotalToUse2
            // 
            this.lblTotalToUse2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalToUse2.AutoSize = true;
            this.lblTotalToUse2.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalToUse2.ForeColor = System.Drawing.Color.Black;
            this.lblTotalToUse2.Location = new System.Drawing.Point(997, 11);
            this.lblTotalToUse2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblTotalToUse2.Name = "lblTotalToUse2";
            this.lblTotalToUse2.Size = new System.Drawing.Size(80, 15);
            this.lblTotalToUse2.TabIndex = 170;
            this.lblTotalToUse2.Text = "Total To Use: ";
            // 
            // lblTotalUsed2
            // 
            this.lblTotalUsed2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalUsed2.AutoSize = true;
            this.lblTotalUsed2.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalUsed2.ForeColor = System.Drawing.Color.Black;
            this.lblTotalUsed2.Location = new System.Drawing.Point(697, 11);
            this.lblTotalUsed2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblTotalUsed2.Name = "lblTotalUsed2";
            this.lblTotalUsed2.Size = new System.Drawing.Size(68, 15);
            this.lblTotalUsed2.TabIndex = 171;
            this.lblTotalUsed2.Text = "Total Used: ";
            // 
            // lblTotalPlannedToUse2
            // 
            this.lblTotalPlannedToUse2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPlannedToUse2.AutoSize = true;
            this.lblTotalPlannedToUse2.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPlannedToUse2.ForeColor = System.Drawing.Color.Black;
            this.lblTotalPlannedToUse2.Location = new System.Drawing.Point(377, 11);
            this.lblTotalPlannedToUse2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblTotalPlannedToUse2.Name = "lblTotalPlannedToUse2";
            this.lblTotalPlannedToUse2.Size = new System.Drawing.Size(120, 15);
            this.lblTotalPlannedToUse2.TabIndex = 172;
            this.lblTotalPlannedToUse2.Text = "Total Planned To Use:";
            // 
            // lblMat2
            // 
            this.lblMat2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMat2.AutoSize = true;
            this.lblMat2.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMat2.ForeColor = System.Drawing.Color.Black;
            this.lblMat2.Location = new System.Drawing.Point(325, 11);
            this.lblMat2.Margin = new System.Windows.Forms.Padding(2, 0, 10, 3);
            this.lblMat2.Name = "lblMat2";
            this.lblMat2.Size = new System.Drawing.Size(40, 15);
            this.lblMat2.TabIndex = 173;
            this.lblMat2.Text = "Mat. : ";
            // 
            // lblMat1
            // 
            this.lblMat1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMat1.AutoSize = true;
            this.lblMat1.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMat1.ForeColor = System.Drawing.Color.Black;
            this.lblMat1.Location = new System.Drawing.Point(325, 31);
            this.lblMat1.Margin = new System.Windows.Forms.Padding(2, 0, 10, 3);
            this.lblMat1.Name = "lblMat1";
            this.lblMat1.Size = new System.Drawing.Size(40, 15);
            this.lblMat1.TabIndex = 172;
            this.lblMat1.Text = "Mat. : ";
            // 
            // lblTotalUsed1
            // 
            this.lblTotalUsed1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalUsed1.AutoSize = true;
            this.lblTotalUsed1.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalUsed1.ForeColor = System.Drawing.Color.Black;
            this.lblTotalUsed1.Location = new System.Drawing.Point(697, 31);
            this.lblTotalUsed1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblTotalUsed1.Name = "lblTotalUsed1";
            this.lblTotalUsed1.Size = new System.Drawing.Size(68, 15);
            this.lblTotalUsed1.TabIndex = 170;
            this.lblTotalUsed1.Text = "Total Used: ";
            // 
            // lblTotalToUse1
            // 
            this.lblTotalToUse1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalToUse1.AutoSize = true;
            this.lblTotalToUse1.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalToUse1.ForeColor = System.Drawing.Color.Black;
            this.lblTotalToUse1.Location = new System.Drawing.Point(997, 31);
            this.lblTotalToUse1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblTotalToUse1.Name = "lblTotalToUse1";
            this.lblTotalToUse1.Size = new System.Drawing.Size(80, 15);
            this.lblTotalToUse1.TabIndex = 169;
            this.lblTotalToUse1.Text = "Total To Use: ";
            // 
            // lblMatStock1
            // 
            this.lblMatStock1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMatStock1.AutoSize = true;
            this.lblMatStock1.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatStock1.ForeColor = System.Drawing.Color.Black;
            this.lblMatStock1.Location = new System.Drawing.Point(1297, 31);
            this.lblMatStock1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblMatStock1.Name = "lblMatStock1";
            this.lblMatStock1.Size = new System.Drawing.Size(41, 15);
            this.lblMatStock1.TabIndex = 170;
            this.lblMatStock1.Text = "Stock: ";
            // 
            // lblTotalPlannedToUse1
            // 
            this.lblTotalPlannedToUse1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPlannedToUse1.AutoSize = true;
            this.lblTotalPlannedToUse1.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPlannedToUse1.ForeColor = System.Drawing.Color.Black;
            this.lblTotalPlannedToUse1.Location = new System.Drawing.Point(377, 31);
            this.lblTotalPlannedToUse1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblTotalPlannedToUse1.Name = "lblTotalPlannedToUse1";
            this.lblTotalPlannedToUse1.Size = new System.Drawing.Size(120, 15);
            this.lblTotalPlannedToUse1.TabIndex = 171;
            this.lblTotalPlannedToUse1.Text = "Total Planned To Use:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.tlpMainSchedule, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1589, 734);
            this.tableLayoutPanel7.TabIndex = 168;
            // 
            // frmMachineScheduleVer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1589, 734);
            this.Controls.Add(this.tableLayoutPanel7);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMachineScheduleVer2";
            this.Text = "Machine Schedule";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMachineSchedule_FormClosed);
            this.Load += new System.EventHandler(this.frmMachineSchedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMacSchedule)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlpButton.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tlpMainSchedule.ResumeLayout(false);
            this.tlpMainSchedule.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMacSchedule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cmbMac;
        private System.Windows.Forms.ComboBox cmbMacLocation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbSearchByJobNo;
        private System.Windows.Forms.CheckBox cbItem;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblResetAll;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbCancelled;
        private System.Windows.Forms.CheckBox cbCompleted;
        private System.Windows.Forms.CheckBox cbWarning;
        private System.Windows.Forms.CheckBox cbRunning;
        private System.Windows.Forms.CheckBox cbPending;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tlpButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tlpMainSchedule;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotalPlannedToUse1;
        private System.Windows.Forms.Label lblTotalUsed1;
        private System.Windows.Forms.Label lblTotalToUse1;
        private System.Windows.Forms.Label lblMatStock1;
        private Guna.UI.WinForms.GunaGradientButton btnNewJob;
        private Guna.UI.WinForms.GunaGradientButton btnExcel;
        private Guna.UI.WinForms.GunaGradientButton btnFilter;
        private Guna.UI.WinForms.GunaGradientButton btnFilterApply;
        private System.Windows.Forms.CheckBox cbDraft;
        private Guna.UI.WinForms.GunaGradientButton btnAdjustCollisionDateBySystem;
        private Guna.UI.WinForms.GunaGradientButton gunaGradientButton1;
        private System.Windows.Forms.Label lblSmallFontRow;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Label lblManualSummary;
        private System.Windows.Forms.Label lblMatStock2;
        private System.Windows.Forms.Label lblTotalToUse2;
        private System.Windows.Forms.Label lblTotalUsed2;
        private System.Windows.Forms.Label lblTotalPlannedToUse2;
        private System.Windows.Forms.Label lblMat2;
        private System.Windows.Forms.Label lblMat1;
    }
}