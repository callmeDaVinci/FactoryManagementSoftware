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
            this.cmbMac = new System.Windows.Forms.ComboBox();
            this.cmbMacLocation = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
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
            this.btnExcel = new Guna.UI.WinForms.GunaGradientButton();
            this.btnNewJob = new Guna.UI.WinForms.GunaGradientButton();
            this.btnFilter = new Guna.UI.WinForms.GunaGradientButton();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFilterApply = new Guna.UI.WinForms.GunaGradientButton();
            this.tlpMainSchedule = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdjustCollisionDateBySystem = new Guna.UI.WinForms.GunaGradientButton();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMatStock = new System.Windows.Forms.Label();
            this.lblTotalUsed = new System.Windows.Forms.Label();
            this.lblTotalToUse = new System.Windows.Forms.Label();
            this.lblTotalPlannedToUse = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel8.SuspendLayout();
            this.tlpMainSchedule.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMacSchedule
            // 
            this.dgvMacSchedule.AllowUserToAddRows = false;
            this.dgvMacSchedule.AllowUserToDeleteRows = false;
            this.dgvMacSchedule.AllowUserToOrderColumns = true;
            this.dgvMacSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMacSchedule.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.dgvMacSchedule.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMacSchedule.ColumnHeadersHeight = 50;
            this.dgvMacSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMacSchedule.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMacSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMacSchedule.GridColor = System.Drawing.Color.Silver;
            this.dgvMacSchedule.Location = new System.Drawing.Point(2, 258);
            this.dgvMacSchedule.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.dgvMacSchedule.Name = "dgvMacSchedule";
            this.dgvMacSchedule.RowHeadersVisible = false;
            this.dgvMacSchedule.RowHeadersWidth = 51;
            this.dgvMacSchedule.RowTemplate.Height = 60;
            this.dgvMacSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMacSchedule.Size = new System.Drawing.Size(1281, 375);
            this.dgvMacSchedule.TabIndex = 109;
            this.dgvMacSchedule.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellClick);
            this.dgvMacSchedule.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellDoubleClick);
            this.dgvMacSchedule.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellEndEdit);
            this.dgvMacSchedule.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSchedule_CellMouseDown);
            this.dgvMacSchedule.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellValueChanged);
            this.dgvMacSchedule.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvSchedule_EditingControlShowing);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 34);
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
            this.groupBox1.Size = new System.Drawing.Size(1281, 114);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1277, 90);
            this.tableLayoutPanel1.TabIndex = 115;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(1023, 3);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(252, 84);
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
            this.tableLayoutPanel6.Size = new System.Drawing.Size(248, 64);
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
            this.txtSearch.Size = new System.Drawing.Size(244, 25);
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
            this.tableLayoutPanel5.Size = new System.Drawing.Size(248, 26);
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
            this.cbSearchByJobNo.Location = new System.Drawing.Point(126, 3);
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
            this.groupBox3.Location = new System.Drawing.Point(783, 3);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox3.Size = new System.Drawing.Size(236, 84);
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
            this.tableLayoutPanel4.Controls.Add(this.cmbMac, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.cmbMacLocation, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2, 17);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(232, 64);
            this.tableLayoutPanel4.TabIndex = 115;
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
            this.cmbMac.Location = new System.Drawing.Point(121, 29);
            this.cmbMac.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbMac.Name = "cmbMac";
            this.cmbMac.Size = new System.Drawing.Size(109, 25);
            this.cmbMac.TabIndex = 57;
            this.cmbMac.SelectedIndexChanged += new System.EventHandler(this.cmbMachine_SelectedIndexChanged);
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
            this.cmbMacLocation.Size = new System.Drawing.Size(108, 25);
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
            this.label5.Location = new System.Drawing.Point(121, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 116;
            this.label5.Text = "Machine";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel3);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.groupBox4.Location = new System.Drawing.Point(442, 3);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox4.Size = new System.Drawing.Size(337, 84);
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(333, 64);
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
            this.tableLayoutPanel9.Location = new System.Drawing.Point(170, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(163, 26);
            this.tableLayoutPanel9.TabIndex = 169;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(122, 8);
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
            this.dtpTo.Location = new System.Drawing.Point(172, 29);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(159, 25);
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
            this.dtpFrom.Size = new System.Drawing.Size(159, 25);
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
            this.groupBox5.Size = new System.Drawing.Size(436, 84);
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(432, 64);
            this.tableLayoutPanel2.TabIndex = 115;
            // 
            // cbDraft
            // 
            this.cbDraft.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbDraft.AutoSize = true;
            this.cbDraft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDraft.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbDraft.Location = new System.Drawing.Point(290, 35);
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
            this.cbCancelled.Location = new System.Drawing.Point(148, 35);
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
            this.cbRunning.Location = new System.Drawing.Point(148, 4);
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
            this.cbWarning.Location = new System.Drawing.Point(290, 4);
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
            this.lblResetAll.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblResetAll.Location = new System.Drawing.Point(2, 3);
            this.lblResetAll.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblResetAll.Name = "lblResetAll";
            this.lblResetAll.Size = new System.Drawing.Size(72, 19);
            this.lblResetAll.TabIndex = 90;
            this.lblResetAll.Text = "RESET ALL";
            this.lblResetAll.Click += new System.EventHandler(this.ResetAll_Click);
            // 
            // tlpButton
            // 
            this.tlpButton.ColumnCount = 5;
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlpButton.Controls.Add(this.btnExcel, 4, 0);
            this.tlpButton.Controls.Add(this.btnNewJob, 2, 0);
            this.tlpButton.Controls.Add(this.btnFilter, 0, 0);
            this.tlpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButton.Location = new System.Drawing.Point(0, 0);
            this.tlpButton.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButton.Name = "tlpButton";
            this.tlpButton.RowCount = 1;
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.Size = new System.Drawing.Size(1285, 46);
            this.tlpButton.TabIndex = 115;
            // 
            // btnExcel
            // 
            this.btnExcel.AnimationHoverSpeed = 0.07F;
            this.btnExcel.AnimationSpeed = 0.03F;
            this.btnExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExcel.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnExcel.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnExcel.BorderColor = System.Drawing.Color.Black;
            this.btnExcel.BorderSize = 1;
            this.btnExcel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExcel.FocusedColor = System.Drawing.Color.Empty;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Image = null;
            this.btnExcel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnExcel.Location = new System.Drawing.Point(1182, 3);
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
            this.btnNewJob.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(241)))), ((int)(((byte)(154)))));
            this.btnNewJob.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(241)))), ((int)(((byte)(154)))));
            this.btnNewJob.BorderColor = System.Drawing.Color.Black;
            this.btnNewJob.BorderSize = 1;
            this.btnNewJob.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnNewJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewJob.FocusedColor = System.Drawing.Color.Empty;
            this.btnNewJob.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNewJob.ForeColor = System.Drawing.Color.Black;
            this.btnNewJob.Image = null;
            this.btnNewJob.ImageSize = new System.Drawing.Size(20, 20);
            this.btnNewJob.Location = new System.Drawing.Point(1035, 3);
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
            this.btnNewJob.Click += new System.EventHandler(this.btnPlan_Click);
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
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
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
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 179F));
            this.tableLayoutPanel8.Controls.Add(this.btnFilterApply, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblResetAll, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 166);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1285, 46);
            this.tableLayoutPanel8.TabIndex = 116;
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.AnimationHoverSpeed = 0.07F;
            this.btnFilterApply.AnimationSpeed = 0.03F;
            this.btnFilterApply.BackColor = System.Drawing.Color.Transparent;
            this.btnFilterApply.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFilterApply.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(160)))), ((int)(((byte)(209)))));
            this.btnFilterApply.BorderColor = System.Drawing.Color.Black;
            this.btnFilterApply.BorderSize = 1;
            this.btnFilterApply.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFilterApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFilterApply.FocusedColor = System.Drawing.Color.Empty;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilterApply.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnFilterApply.Image = null;
            this.btnFilterApply.ImageSize = new System.Drawing.Size(20, 20);
            this.btnFilterApply.Location = new System.Drawing.Point(1108, 3);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnFilterApply.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnFilterApply.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFilterApply.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFilterApply.OnHoverImage = null;
            this.btnFilterApply.OnPressedColor = System.Drawing.Color.Black;
            this.btnFilterApply.Radius = 2;
            this.btnFilterApply.Size = new System.Drawing.Size(175, 40);
            this.btnFilterApply.TabIndex = 224;
            this.btnFilterApply.Text = "Filter Apply";
            this.btnFilterApply.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilterApply.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // tlpMainSchedule
            // 
            this.tlpMainSchedule.ColumnCount = 1;
            this.tlpMainSchedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainSchedule.Controls.Add(this.btnAdjustCollisionDateBySystem, 0, 6);
            this.tlpMainSchedule.Controls.Add(this.tlpButton, 0, 0);
            this.tlpMainSchedule.Controls.Add(this.dgvMacSchedule, 0, 4);
            this.tlpMainSchedule.Controls.Add(this.groupBox1, 0, 1);
            this.tlpMainSchedule.Controls.Add(this.tableLayoutPanel8, 0, 2);
            this.tlpMainSchedule.Controls.Add(this.tableLayoutPanel10, 0, 3);
            this.tlpMainSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainSchedule.Location = new System.Drawing.Point(14, 17);
            this.tlpMainSchedule.Margin = new System.Windows.Forms.Padding(14, 17, 14, 17);
            this.tlpMainSchedule.Name = "tlpMainSchedule";
            this.tlpMainSchedule.RowCount = 7;
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpMainSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpMainSchedule.Size = new System.Drawing.Size(1285, 700);
            this.tlpMainSchedule.TabIndex = 117;
            // 
            // btnAdjustCollisionDateBySystem
            // 
            this.btnAdjustCollisionDateBySystem.AnimationHoverSpeed = 0.07F;
            this.btnAdjustCollisionDateBySystem.AnimationSpeed = 0.03F;
            this.btnAdjustCollisionDateBySystem.BackColor = System.Drawing.Color.Transparent;
            this.btnAdjustCollisionDateBySystem.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            this.btnAdjustCollisionDateBySystem.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(241)))), ((int)(((byte)(154)))));
            this.btnAdjustCollisionDateBySystem.BorderColor = System.Drawing.Color.Black;
            this.btnAdjustCollisionDateBySystem.BorderSize = 1;
            this.btnAdjustCollisionDateBySystem.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdjustCollisionDateBySystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdjustCollisionDateBySystem.FocusedColor = System.Drawing.Color.Empty;
            this.btnAdjustCollisionDateBySystem.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnAdjustCollisionDateBySystem.ForeColor = System.Drawing.Color.Black;
            this.btnAdjustCollisionDateBySystem.Image = null;
            this.btnAdjustCollisionDateBySystem.ImageSize = new System.Drawing.Size(20, 20);
            this.btnAdjustCollisionDateBySystem.Location = new System.Drawing.Point(3, 647);
            this.btnAdjustCollisionDateBySystem.Name = "btnAdjustCollisionDateBySystem";
            this.btnAdjustCollisionDateBySystem.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnAdjustCollisionDateBySystem.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnAdjustCollisionDateBySystem.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnAdjustCollisionDateBySystem.OnHoverForeColor = System.Drawing.Color.White;
            this.btnAdjustCollisionDateBySystem.OnHoverImage = null;
            this.btnAdjustCollisionDateBySystem.OnPressedColor = System.Drawing.Color.Black;
            this.btnAdjustCollisionDateBySystem.Radius = 2;
            this.btnAdjustCollisionDateBySystem.Size = new System.Drawing.Size(1279, 50);
            this.btnAdjustCollisionDateBySystem.TabIndex = 225;
            this.btnAdjustCollisionDateBySystem.Text = "Adjust Collision Date";
            this.btnAdjustCollisionDateBySystem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 5;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.Controls.Add(this.lblMatStock, 4, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblTotalUsed, 2, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblTotalToUse, 3, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblTotalPlannedToUse, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(2, 215);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(1281, 39);
            this.tableLayoutPanel10.TabIndex = 119;
            // 
            // lblMatStock
            // 
            this.lblMatStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMatStock.AutoSize = true;
            this.lblMatStock.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatStock.ForeColor = System.Drawing.Color.Black;
            this.lblMatStock.Location = new System.Drawing.Point(983, 21);
            this.lblMatStock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblMatStock.Name = "lblMatStock";
            this.lblMatStock.Size = new System.Drawing.Size(41, 15);
            this.lblMatStock.TabIndex = 170;
            this.lblMatStock.Text = "Stock: ";
            // 
            // lblTotalUsed
            // 
            this.lblTotalUsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalUsed.AutoSize = true;
            this.lblTotalUsed.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalUsed.ForeColor = System.Drawing.Color.Black;
            this.lblTotalUsed.Location = new System.Drawing.Point(383, 21);
            this.lblTotalUsed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblTotalUsed.Name = "lblTotalUsed";
            this.lblTotalUsed.Size = new System.Drawing.Size(68, 15);
            this.lblTotalUsed.TabIndex = 170;
            this.lblTotalUsed.Text = "Total Used: ";
            // 
            // lblTotalToUse
            // 
            this.lblTotalToUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalToUse.AutoSize = true;
            this.lblTotalToUse.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalToUse.ForeColor = System.Drawing.Color.Black;
            this.lblTotalToUse.Location = new System.Drawing.Point(683, 21);
            this.lblTotalToUse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblTotalToUse.Name = "lblTotalToUse";
            this.lblTotalToUse.Size = new System.Drawing.Size(80, 15);
            this.lblTotalToUse.TabIndex = 169;
            this.lblTotalToUse.Text = "Total To Use: ";
            // 
            // lblTotalPlannedToUse
            // 
            this.lblTotalPlannedToUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPlannedToUse.AutoSize = true;
            this.lblTotalPlannedToUse.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPlannedToUse.ForeColor = System.Drawing.Color.Black;
            this.lblTotalPlannedToUse.Location = new System.Drawing.Point(83, 21);
            this.lblTotalPlannedToUse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblTotalPlannedToUse.Name = "lblTotalPlannedToUse";
            this.lblTotalPlannedToUse.Size = new System.Drawing.Size(120, 15);
            this.lblTotalPlannedToUse.TabIndex = 171;
            this.lblTotalPlannedToUse.Text = "Total Planned To Use:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.tlpMainSchedule, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1313, 734);
            this.tableLayoutPanel7.TabIndex = 168;
            // 
            // frmMachineScheduleVer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1313, 734);
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
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tlpMainSchedule.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblTotalPlannedToUse;
        private System.Windows.Forms.Label lblTotalUsed;
        private System.Windows.Forms.Label lblTotalToUse;
        private System.Windows.Forms.Label lblMatStock;
        private Guna.UI.WinForms.GunaGradientButton btnNewJob;
        private Guna.UI.WinForms.GunaGradientButton btnExcel;
        private Guna.UI.WinForms.GunaGradientButton btnFilter;
        private Guna.UI.WinForms.GunaGradientButton btnFilterApply;
        private System.Windows.Forms.CheckBox cbDraft;
        private Guna.UI.WinForms.GunaGradientButton btnAdjustCollisionDateBySystem;
    }
}