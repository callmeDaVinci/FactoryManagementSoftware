namespace FactoryManagementSoftware.UI
{
    partial class frmPOPlanner
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPOPlanner));
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.cbMergePO = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFillSettingReset = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAssemblyNeeded = new System.Windows.Forms.Label();
            this.lblProductionAlert = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTotalBag = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpPoList = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.gunaGroupBox1 = new Guna.UI.WinForms.GunaGroupBox();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSearch = new Guna.UI.WinForms.GunaTextBox();
            this.gunaGradientButton1 = new Guna.UI.WinForms.GunaGradientButton();
            this.btnUpdate = new Guna.UI.WinForms.GunaGradientButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMainList = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gunaGradientButton2 = new Guna.UI.WinForms.GunaGradientButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCustomerType = new Guna.UI.WinForms.GunaComboBox();
            this.cmbCustomer = new Guna.UI.WinForms.GunaComboBox();
            this.btnFilter = new Guna.UI.WinForms.GunaGradientButton();
            this.btnLoadPO = new Guna.UI.WinForms.GunaGradientButton();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFilterApply = new Guna.UI.WinForms.GunaGradientButton();
            this.gunaGroupBox4 = new Guna.UI.WinForms.GunaGroupBox();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cmbEditUnit = new Guna.UI.WinForms.GunaComboBox();
            this.gunaGroupBox3 = new Guna.UI.WinForms.GunaGroupBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.gunaGroupBox5 = new Guna.UI.WinForms.GunaGroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRangeEnd = new System.Windows.Forms.Label();
            this.dtpRangeEnd = new Guna.UI.WinForms.GunaDateTimePicker();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.cbCustomPeriod = new System.Windows.Forms.CheckBox();
            this.cbAllTimeRange = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpRangeStart = new Guna.UI.WinForms.GunaDateTimePicker();
            this.lblRangeStart = new System.Windows.Forms.Label();
            this.gunaGroupBox2 = new Guna.UI.WinForms.GunaGroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbPOSortBy = new Guna.UI.WinForms.GunaComboBox();
            this.cmbProductSortBy = new Guna.UI.WinForms.GunaComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tlpPoList.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.gunaGroupBox1.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.gunaGroupBox4.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.gunaGroupBox3.SuspendLayout();
            this.gunaGroupBox5.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.gunaGroupBox2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.AllowUserToResizeColumns = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvList.GridColor = System.Drawing.Color.LightGray;
            this.dgvList.Location = new System.Drawing.Point(3, 361);
            this.dgvList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidth = 51;
            this.dgvList.RowTemplate.Height = 50;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvList.Size = new System.Drawing.Size(1308, 319);
            this.dgvList.TabIndex = 179;
            this.dgvList.MultiSelectChanged += new System.EventHandler(this.dgvList_MultiSelectChanged);
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellContentClick_3);
            this.dgvList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellEndEdit);
            this.dgvList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvList_CellMouseDown);
            this.dgvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellValueChanged);
            this.dgvList.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvList_ColumnAdded);
            this.dgvList.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvList_ColumnDisplayIndexChanged);
            this.dgvList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvList_ColumnHeaderMouseClick);
            this.dgvList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvList_EditingControlShowing);
            this.dgvList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvList_MouseClick);
            // 
            // cbMergePO
            // 
            this.cbMergePO.AutoSize = true;
            this.cbMergePO.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMergePO.Location = new System.Drawing.Point(3, 73);
            this.cbMergePO.Name = "cbMergePO";
            this.cbMergePO.Size = new System.Drawing.Size(165, 21);
            this.cbMergePO.TabIndex = 182;
            this.cbMergePO.Text = "Merge Same Customer";
            this.cbMergePO.UseVisualStyleBackColor = true;
            this.cbMergePO.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 17);
            this.label2.TabIndex = 184;
            this.label2.Text = "Unit";
            // 
            // lblFillSettingReset
            // 
            this.lblFillSettingReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFillSettingReset.AutoSize = true;
            this.lblFillSettingReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFillSettingReset.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFillSettingReset.ForeColor = System.Drawing.Color.Blue;
            this.lblFillSettingReset.Location = new System.Drawing.Point(99, 106);
            this.lblFillSettingReset.Margin = new System.Windows.Forms.Padding(3);
            this.lblFillSettingReset.Name = "lblFillSettingReset";
            this.lblFillSettingReset.Size = new System.Drawing.Size(38, 15);
            this.lblFillSettingReset.TabIndex = 192;
            this.lblFillSettingReset.Text = "RESET";
            this.lblFillSettingReset.Visible = false;
            this.lblFillSettingReset.Click += new System.EventHandler(this.lblFillSettingReset_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 17);
            this.label4.TabIndex = 188;
            this.label4.Text = "P/O Sort By";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 17);
            this.label3.TabIndex = 186;
            this.label3.Text = "Product Sort By";
            // 
            // lblAssemblyNeeded
            // 
            this.lblAssemblyNeeded.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAssemblyNeeded.AutoSize = true;
            this.lblAssemblyNeeded.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAssemblyNeeded.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Underline);
            this.lblAssemblyNeeded.ForeColor = System.Drawing.Color.Red;
            this.lblAssemblyNeeded.Location = new System.Drawing.Point(3, 5);
            this.lblAssemblyNeeded.Name = "lblAssemblyNeeded";
            this.lblAssemblyNeeded.Size = new System.Drawing.Size(121, 15);
            this.lblAssemblyNeeded.TabIndex = 188;
            this.lblAssemblyNeeded.Text = "ASSEMBLY NEEDED !!!";
            this.lblAssemblyNeeded.Visible = false;
            this.lblAssemblyNeeded.Click += new System.EventHandler(this.lblAssemblyNeeded_Click);
            // 
            // lblProductionAlert
            // 
            this.lblProductionAlert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblProductionAlert.AutoSize = true;
            this.lblProductionAlert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductionAlert.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Underline);
            this.lblProductionAlert.ForeColor = System.Drawing.Color.Red;
            this.lblProductionAlert.Location = new System.Drawing.Point(3, 30);
            this.lblProductionAlert.Name = "lblProductionAlert";
            this.lblProductionAlert.Size = new System.Drawing.Size(139, 15);
            this.lblProductionAlert.TabIndex = 189;
            this.lblProductionAlert.Text = "PRODUCTION NEEDED !!!";
            this.lblProductionAlert.Visible = false;
            this.lblProductionAlert.Click += new System.EventHandler(this.lblProductionAlert_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_refresh_480;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(2, 2);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(45, 45);
            this.btnRefresh.TabIndex = 190;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTotalBag
            // 
            this.lblTotalBag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalBag.AutoSize = true;
            this.lblTotalBag.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblTotalBag.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTotalBag.Location = new System.Drawing.Point(637, 17);
            this.lblTotalBag.Name = "lblTotalBag";
            this.lblTotalBag.Size = new System.Drawing.Size(151, 15);
            this.lblTotalBag.TabIndex = 193;
            this.lblTotalBag.Text = "0 BAG(s) / 0 PCS  SELECTED";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.tlpPoList, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1354, 721);
            this.tableLayoutPanel6.TabIndex = 194;
            // 
            // tlpPoList
            // 
            this.tlpPoList.ColumnCount = 1;
            this.tlpPoList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPoList.Controls.Add(this.dgvList, 0, 4);
            this.tlpPoList.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tlpPoList.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpPoList.Controls.Add(this.gbFilter, 0, 1);
            this.tlpPoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPoList.Location = new System.Drawing.Point(20, 20);
            this.tlpPoList.Margin = new System.Windows.Forms.Padding(20);
            this.tlpPoList.Name = "tlpPoList";
            this.tlpPoList.RowCount = 5;
            this.tlpPoList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpPoList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpPoList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpPoList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpPoList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPoList.Size = new System.Drawing.Size(1314, 681);
            this.tlpPoList.TabIndex = 172;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 499F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.gunaGroupBox1, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnUpdate, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnRefresh, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblMainList, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblTotalBag, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 310);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1314, 50);
            this.tableLayoutPanel4.TabIndex = 167;
            // 
            // gunaGroupBox1
            // 
            this.gunaGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox1.BaseColor = System.Drawing.Color.White;
            this.gunaGroupBox1.BorderColor = System.Drawing.Color.Black;
            this.gunaGroupBox1.BorderSize = 1;
            this.gunaGroupBox1.Controls.Add(this.tableLayoutPanel16);
            this.gunaGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox1.LineColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox1.Location = new System.Drawing.Point(140, 5);
            this.gunaGroupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.gunaGroupBox1.Name = "gunaGroupBox1";
            this.gunaGroupBox1.Padding = new System.Windows.Forms.Padding(3);
            this.gunaGroupBox1.Radius = 3;
            this.gunaGroupBox1.Size = new System.Drawing.Size(489, 40);
            this.gunaGroupBox1.TabIndex = 250;
            this.gunaGroupBox1.TextLocation = new System.Drawing.Point(10, 8);
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 2;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel16.Controls.Add(this.txtSearch, 1, 0);
            this.tableLayoutPanel16.Controls.Add(this.gunaGradientButton1, 0, 0);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 1;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(483, 34);
            this.tableLayoutPanel16.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.Transparent;
            this.txtSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtSearch.BaseColor = System.Drawing.Color.White;
            this.txtSearch.BorderColor = System.Drawing.Color.Transparent;
            this.txtSearch.BorderSize = 1;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.FocusedBaseColor = System.Drawing.Color.White;
            this.txtSearch.FocusedBorderColor = System.Drawing.Color.White;
            this.txtSearch.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.txtSearch.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtSearch.Location = new System.Drawing.Point(53, 3);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.Radius = 3;
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(427, 28);
            this.txtSearch.TabIndex = 245;
            this.txtSearch.Text = "Search (PO Number Or Item Name/Code)";
            // 
            // gunaGradientButton1
            // 
            this.gunaGradientButton1.AnimationHoverSpeed = 0.07F;
            this.gunaGradientButton1.AnimationSpeed = 0.03F;
            this.gunaGradientButton1.BackColor = System.Drawing.Color.Transparent;
            this.gunaGradientButton1.BaseColor1 = System.Drawing.Color.White;
            this.gunaGradientButton1.BaseColor2 = System.Drawing.Color.White;
            this.gunaGradientButton1.BorderColor = System.Drawing.Color.White;
            this.gunaGradientButton1.BorderSize = 1;
            this.gunaGradientButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaGradientButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGradientButton1.FocusedColor = System.Drawing.Color.Empty;
            this.gunaGradientButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaGradientButton1.ForeColor = System.Drawing.Color.White;
            this.gunaGradientButton1.Image = ((System.Drawing.Image)(resources.GetObject("gunaGradientButton1.Image")));
            this.gunaGradientButton1.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaGradientButton1.Location = new System.Drawing.Point(5, 5);
            this.gunaGradientButton1.Margin = new System.Windows.Forms.Padding(5);
            this.gunaGradientButton1.Name = "gunaGradientButton1";
            this.gunaGradientButton1.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.gunaGradientButton1.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.gunaGradientButton1.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.gunaGradientButton1.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaGradientButton1.OnHoverImage = null;
            this.gunaGradientButton1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaGradientButton1.Radius = 2;
            this.gunaGradientButton1.Size = new System.Drawing.Size(43, 24);
            this.gunaGradientButton1.TabIndex = 249;
            this.gunaGradientButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnUpdate
            // 
            this.btnUpdate.AnimationHoverSpeed = 0.07F;
            this.btnUpdate.AnimationSpeed = 0.03F;
            this.btnUpdate.BackColor = System.Drawing.Color.White;
            this.btnUpdate.BaseColor1 = System.Drawing.Color.Transparent;
            this.btnUpdate.BaseColor2 = System.Drawing.Color.Transparent;
            this.btnUpdate.BorderColor = System.Drawing.Color.Blue;
            this.btnUpdate.BorderSize = 1;
            this.btnUpdate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate.FocusedColor = System.Drawing.Color.Empty;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUpdate.ForeColor = System.Drawing.Color.Blue;
            this.btnUpdate.Image = null;
            this.btnUpdate.ImageSize = new System.Drawing.Size(20, 20);
            this.btnUpdate.Location = new System.Drawing.Point(1177, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnUpdate.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnUpdate.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnUpdate.OnHoverForeColor = System.Drawing.Color.White;
            this.btnUpdate.OnHoverImage = null;
            this.btnUpdate.OnPressedColor = System.Drawing.Color.Black;
            this.btnUpdate.Radius = 3;
            this.btnUpdate.Size = new System.Drawing.Size(134, 44);
            this.btnUpdate.TabIndex = 230;
            this.btnUpdate.Text = "Save";
            this.btnUpdate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel3.Controls.Add(this.lblProductionAlert, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblAssemblyNeeded, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1025, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(149, 50);
            this.tableLayoutPanel3.TabIndex = 232;
            // 
            // lblMainList
            // 
            this.lblMainList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMainList.AutoSize = true;
            this.lblMainList.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMainList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMainList.Location = new System.Drawing.Point(55, 13);
            this.lblMainList.Margin = new System.Windows.Forms.Padding(5);
            this.lblMainList.Name = "lblMainList";
            this.lblMainList.Size = new System.Drawing.Size(68, 23);
            this.lblMainList.TabIndex = 166;
            this.lblMainList.Text = "Planner";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.Controls.Add(this.gunaGradientButton2, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnLoadPO, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1308, 74);
            this.tableLayoutPanel2.TabIndex = 160;
            // 
            // gunaGradientButton2
            // 
            this.gunaGradientButton2.AnimationHoverSpeed = 0.07F;
            this.gunaGradientButton2.AnimationSpeed = 0.03F;
            this.gunaGradientButton2.BackColor = System.Drawing.Color.White;
            this.gunaGradientButton2.BaseColor1 = System.Drawing.Color.Transparent;
            this.gunaGradientButton2.BaseColor2 = System.Drawing.Color.Transparent;
            this.gunaGradientButton2.BorderColor = System.Drawing.Color.Black;
            this.gunaGradientButton2.BorderSize = 1;
            this.gunaGradientButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaGradientButton2.FocusedColor = System.Drawing.Color.Empty;
            this.gunaGradientButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaGradientButton2.ForeColor = System.Drawing.Color.Black;
            this.gunaGradientButton2.Image = null;
            this.gunaGradientButton2.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaGradientButton2.Location = new System.Drawing.Point(1173, 25);
            this.gunaGradientButton2.Margin = new System.Windows.Forms.Padding(5, 25, 5, 3);
            this.gunaGradientButton2.Name = "gunaGradientButton2";
            this.gunaGradientButton2.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.gunaGradientButton2.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.gunaGradientButton2.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaGradientButton2.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaGradientButton2.OnHoverImage = null;
            this.gunaGradientButton2.OnPressedColor = System.Drawing.Color.Black;
            this.gunaGradientButton2.Radius = 3;
            this.gunaGradientButton2.Size = new System.Drawing.Size(130, 46);
            this.gunaGradientButton2.TabIndex = 231;
            this.gunaGradientButton2.Text = "Add PO";
            this.gunaGradientButton2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbCustomerType, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbCustomer, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 74);
            this.tableLayoutPanel1.TabIndex = 231;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label7.Location = new System.Drawing.Point(103, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 19);
            this.label7.TabIndex = 232;
            this.label7.Text = "Customer";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label8.Location = new System.Drawing.Point(3, 14);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 19);
            this.label8.TabIndex = 231;
            this.label8.Text = "Type";
            // 
            // cmbCustomerType
            // 
            this.cmbCustomerType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCustomerType.BackColor = System.Drawing.Color.Transparent;
            this.cmbCustomerType.BaseColor = System.Drawing.Color.White;
            this.cmbCustomerType.BorderColor = System.Drawing.Color.Silver;
            this.cmbCustomerType.BorderSize = 1;
            this.cmbCustomerType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerType.FocusedColor = System.Drawing.Color.Empty;
            this.cmbCustomerType.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbCustomerType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbCustomerType.FormattingEnabled = true;
            this.cmbCustomerType.Location = new System.Drawing.Point(3, 36);
            this.cmbCustomerType.Name = "cmbCustomerType";
            this.cmbCustomerType.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmbCustomerType.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cmbCustomerType.Radius = 3;
            this.cmbCustomerType.Size = new System.Drawing.Size(94, 35);
            this.cmbCustomerType.TabIndex = 252;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCustomer.BackColor = System.Drawing.Color.Transparent;
            this.cmbCustomer.BaseColor = System.Drawing.Color.White;
            this.cmbCustomer.BorderColor = System.Drawing.Color.Silver;
            this.cmbCustomer.BorderSize = 1;
            this.cmbCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FocusedColor = System.Drawing.Color.Empty;
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(103, 36);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmbCustomer.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cmbCustomer.Radius = 3;
            this.cmbCustomer.Size = new System.Drawing.Size(244, 35);
            this.cmbCustomer.TabIndex = 251;
            // 
            // btnFilter
            // 
            this.btnFilter.AnimationHoverSpeed = 0.07F;
            this.btnFilter.AnimationSpeed = 0.03F;
            this.btnFilter.BackColor = System.Drawing.Color.Transparent;
            this.btnFilter.BaseColor1 = System.Drawing.Color.Transparent;
            this.btnFilter.BaseColor2 = System.Drawing.Color.Transparent;
            this.btnFilter.BorderColor = System.Drawing.Color.Black;
            this.btnFilter.BorderSize = 1;
            this.btnFilter.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFilter.FocusedColor = System.Drawing.Color.Empty;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnFilter.Image = null;
            this.btnFilter.ImageSize = new System.Drawing.Size(20, 20);
            this.btnFilter.Location = new System.Drawing.Point(495, 25);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(5, 25, 5, 3);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnFilter.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnFilter.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFilter.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFilter.OnHoverImage = null;
            this.btnFilter.OnPressedColor = System.Drawing.Color.Black;
            this.btnFilter.Radius = 3;
            this.btnFilter.Size = new System.Drawing.Size(130, 46);
            this.btnFilter.TabIndex = 230;
            this.btnFilter.Text = "Show Filter";
            this.btnFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnLoadPO
            // 
            this.btnLoadPO.AnimationHoverSpeed = 0.07F;
            this.btnLoadPO.AnimationSpeed = 0.03F;
            this.btnLoadPO.BackColor = System.Drawing.Color.Transparent;
            this.btnLoadPO.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(107)))), ((int)(((byte)(236)))));
            this.btnLoadPO.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnLoadPO.BorderColor = System.Drawing.Color.Black;
            this.btnLoadPO.BorderSize = 1;
            this.btnLoadPO.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLoadPO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoadPO.FocusedColor = System.Drawing.Color.Empty;
            this.btnLoadPO.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnLoadPO.ForeColor = System.Drawing.Color.White;
            this.btnLoadPO.Image = null;
            this.btnLoadPO.ImageSize = new System.Drawing.Size(20, 20);
            this.btnLoadPO.Location = new System.Drawing.Point(355, 25);
            this.btnLoadPO.Margin = new System.Windows.Forms.Padding(5, 25, 5, 3);
            this.btnLoadPO.Name = "btnLoadPO";
            this.btnLoadPO.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnLoadPO.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnLoadPO.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnLoadPO.OnHoverForeColor = System.Drawing.Color.White;
            this.btnLoadPO.OnHoverImage = null;
            this.btnLoadPO.OnPressedColor = System.Drawing.Color.Black;
            this.btnLoadPO.Radius = 3;
            this.btnLoadPO.Size = new System.Drawing.Size(130, 46);
            this.btnLoadPO.TabIndex = 228;
            this.btnLoadPO.Text = "Load";
            this.btnLoadPO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnLoadPO.Click += new System.EventHandler(this.LoadPO_Click);
            // 
            // gbFilter
            // 
            this.gbFilter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gbFilter.Controls.Add(this.tableLayoutPanel14);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 83);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1308, 194);
            this.gbFilter.TabIndex = 172;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "FILTER";
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tableLayoutPanel14.ColumnCount = 7;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.tableLayoutPanel15, 5, 0);
            this.tableLayoutPanel14.Controls.Add(this.gunaGroupBox4, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.gunaGroupBox3, 2, 0);
            this.tableLayoutPanel14.Controls.Add(this.gunaGroupBox5, 3, 0);
            this.tableLayoutPanel14.Controls.Add(this.gunaGroupBox2, 1, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel14.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(1302, 174);
            this.tableLayoutPanel14.TabIndex = 263;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 1;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Controls.Add(this.lblFillSettingReset, 0, 1);
            this.tableLayoutPanel15.Controls.Add(this.btnFilterApply, 0, 2);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(1100, 0);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 3;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(140, 174);
            this.tableLayoutPanel15.TabIndex = 234;
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.AnimationHoverSpeed = 0.07F;
            this.btnFilterApply.AnimationSpeed = 0.03F;
            this.btnFilterApply.BackColor = System.Drawing.Color.Transparent;
            this.btnFilterApply.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(107)))), ((int)(((byte)(236)))));
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
            this.btnFilterApply.Location = new System.Drawing.Point(3, 127);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnFilterApply.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnFilterApply.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFilterApply.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFilterApply.OnHoverImage = null;
            this.btnFilterApply.OnPressedColor = System.Drawing.Color.Black;
            this.btnFilterApply.Radius = 3;
            this.btnFilterApply.Size = new System.Drawing.Size(134, 46);
            this.btnFilterApply.TabIndex = 227;
            this.btnFilterApply.Text = "Apply";
            this.btnFilterApply.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // gunaGroupBox4
            // 
            this.gunaGroupBox4.BackColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox4.BaseColor = System.Drawing.Color.WhiteSmoke;
            this.gunaGroupBox4.BorderColor = System.Drawing.Color.LightGray;
            this.gunaGroupBox4.BorderSize = 1;
            this.gunaGroupBox4.Controls.Add(this.tableLayoutPanel13);
            this.gunaGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox4.LineColor = System.Drawing.Color.LightGray;
            this.gunaGroupBox4.Location = new System.Drawing.Point(3, 3);
            this.gunaGroupBox4.Name = "gunaGroupBox4";
            this.gunaGroupBox4.Padding = new System.Windows.Forms.Padding(3, 33, 3, 3);
            this.gunaGroupBox4.Radius = 3;
            this.gunaGroupBox4.Size = new System.Drawing.Size(214, 168);
            this.gunaGroupBox4.TabIndex = 262;
            this.gunaGroupBox4.Text = "SBB Settings";
            this.gunaGroupBox4.TextLocation = new System.Drawing.Point(10, 8);
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 1;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.checkBox1, 0, 3);
            this.tableLayoutPanel13.Controls.Add(this.cmbEditUnit, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.cbMergePO, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel13.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 4;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(208, 132);
            this.tableLayoutPanel13.TabIndex = 258;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(3, 106);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(159, 21);
            this.checkBox1.TabIndex = 195;
            this.checkBox1.Text = "Show Order Item Only";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // cmbEditUnit
            // 
            this.cmbEditUnit.BackColor = System.Drawing.Color.Transparent;
            this.cmbEditUnit.BaseColor = System.Drawing.Color.White;
            this.cmbEditUnit.BorderColor = System.Drawing.Color.Silver;
            this.cmbEditUnit.BorderSize = 1;
            this.cmbEditUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEditUnit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEditUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditUnit.FocusedColor = System.Drawing.Color.Empty;
            this.cmbEditUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEditUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbEditUnit.FormattingEnabled = true;
            this.cmbEditUnit.Location = new System.Drawing.Point(3, 28);
            this.cmbEditUnit.Name = "cmbEditUnit";
            this.cmbEditUnit.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmbEditUnit.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cmbEditUnit.Radius = 3;
            this.cmbEditUnit.Size = new System.Drawing.Size(202, 31);
            this.cmbEditUnit.TabIndex = 253;
            this.cmbEditUnit.SelectedIndexChanged += new System.EventHandler(this.cmbEditUnit_SelectedIndexChanged);
            // 
            // gunaGroupBox3
            // 
            this.gunaGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox3.BaseColor = System.Drawing.Color.WhiteSmoke;
            this.gunaGroupBox3.BorderColor = System.Drawing.Color.LightGray;
            this.gunaGroupBox3.BorderSize = 1;
            this.gunaGroupBox3.Controls.Add(this.tableLayoutPanel9);
            this.gunaGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox3.Enabled = false;
            this.gunaGroupBox3.LineColor = System.Drawing.Color.LightGray;
            this.gunaGroupBox3.Location = new System.Drawing.Point(523, 3);
            this.gunaGroupBox3.Name = "gunaGroupBox3";
            this.gunaGroupBox3.Padding = new System.Windows.Forms.Padding(3, 33, 3, 3);
            this.gunaGroupBox3.Radius = 3;
            this.gunaGroupBox3.Size = new System.Drawing.Size(344, 168);
            this.gunaGroupBox3.TabIndex = 260;
            this.gunaGroupBox3.Text = "Search";
            this.gunaGroupBox3.TextLocation = new System.Drawing.Point(10, 8);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(338, 132);
            this.tableLayoutPanel9.TabIndex = 258;
            // 
            // gunaGroupBox5
            // 
            this.gunaGroupBox5.BackColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox5.BaseColor = System.Drawing.Color.WhiteSmoke;
            this.gunaGroupBox5.BorderColor = System.Drawing.Color.LightGray;
            this.gunaGroupBox5.BorderSize = 1;
            this.gunaGroupBox5.Controls.Add(this.tableLayoutPanel8);
            this.gunaGroupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox5.Enabled = false;
            this.gunaGroupBox5.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.gunaGroupBox5.LineColor = System.Drawing.Color.LightGray;
            this.gunaGroupBox5.Location = new System.Drawing.Point(873, 3);
            this.gunaGroupBox5.Name = "gunaGroupBox5";
            this.gunaGroupBox5.Padding = new System.Windows.Forms.Padding(3, 33, 3, 3);
            this.gunaGroupBox5.Radius = 3;
            this.gunaGroupBox5.Size = new System.Drawing.Size(214, 168);
            this.gunaGroupBox5.TabIndex = 261;
            this.gunaGroupBox5.Text = "Date Range";
            this.gunaGroupBox5.TextLocation = new System.Drawing.Point(10, 8);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel12, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel10, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel11, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 4;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(208, 132);
            this.tableLayoutPanel8.TabIndex = 258;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.47917F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.52084F));
            this.tableLayoutPanel12.Controls.Add(this.lblRangeEnd, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.dtpRangeEnd, 1, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(5, 94);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel12.MaximumSize = new System.Drawing.Size(0, 43);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 1;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(198, 1);
            this.tableLayoutPanel12.TabIndex = 262;
            // 
            // lblRangeEnd
            // 
            this.lblRangeEnd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRangeEnd.AutoSize = true;
            this.lblRangeEnd.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRangeEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRangeEnd.Location = new System.Drawing.Point(13, 5);
            this.lblRangeEnd.Margin = new System.Windows.Forms.Padding(5);
            this.lblRangeEnd.Name = "lblRangeEnd";
            this.lblRangeEnd.Size = new System.Drawing.Size(30, 1);
            this.lblRangeEnd.TabIndex = 243;
            this.lblRangeEnd.Text = "End";
            this.lblRangeEnd.Visible = false;
            // 
            // dtpRangeEnd
            // 
            this.dtpRangeEnd.BackColor = System.Drawing.Color.Transparent;
            this.dtpRangeEnd.BaseColor = System.Drawing.Color.White;
            this.dtpRangeEnd.BorderColor = System.Drawing.Color.Silver;
            this.dtpRangeEnd.BorderSize = 1;
            this.dtpRangeEnd.CustomFormat = null;
            this.dtpRangeEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpRangeEnd.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpRangeEnd.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpRangeEnd.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dtpRangeEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpRangeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRangeEnd.Location = new System.Drawing.Point(53, 5);
            this.dtpRangeEnd.Margin = new System.Windows.Forms.Padding(5);
            this.dtpRangeEnd.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpRangeEnd.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpRangeEnd.Name = "dtpRangeEnd";
            this.dtpRangeEnd.OnHoverBaseColor = System.Drawing.Color.White;
            this.dtpRangeEnd.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpRangeEnd.OnHoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpRangeEnd.OnPressedColor = System.Drawing.Color.Black;
            this.dtpRangeEnd.Radius = 3;
            this.dtpRangeEnd.Size = new System.Drawing.Size(140, 1);
            this.dtpRangeEnd.TabIndex = 240;
            this.dtpRangeEnd.Text = "6/12/2023";
            this.dtpRangeEnd.Value = new System.DateTime(2023, 12, 6, 10, 56, 27, 880);
            this.dtpRangeEnd.Visible = false;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.95959F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.04041F));
            this.tableLayoutPanel10.Controls.Add(this.cbCustomPeriod, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.cbAllTimeRange, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(208, 36);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // cbCustomPeriod
            // 
            this.cbCustomPeriod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCustomPeriod.AutoSize = true;
            this.cbCustomPeriod.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.cbCustomPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbCustomPeriod.Location = new System.Drawing.Point(98, 7);
            this.cbCustomPeriod.Name = "cbCustomPeriod";
            this.cbCustomPeriod.Size = new System.Drawing.Size(74, 21);
            this.cbCustomPeriod.TabIndex = 239;
            this.cbCustomPeriod.Text = "Custom";
            this.cbCustomPeriod.UseVisualStyleBackColor = true;
            // 
            // cbAllTimeRange
            // 
            this.cbAllTimeRange.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbAllTimeRange.AutoSize = true;
            this.cbAllTimeRange.Checked = true;
            this.cbAllTimeRange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllTimeRange.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.cbAllTimeRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbAllTimeRange.Location = new System.Drawing.Point(10, 7);
            this.cbAllTimeRange.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.cbAllTimeRange.Name = "cbAllTimeRange";
            this.cbAllTimeRange.Size = new System.Drawing.Size(76, 21);
            this.cbAllTimeRange.TabIndex = 238;
            this.cbAllTimeRange.Text = "All Time";
            this.cbAllTimeRange.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.47917F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.52084F));
            this.tableLayoutPanel11.Controls.Add(this.dtpRangeStart, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblRangeStart, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(5, 41);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel11.MaximumSize = new System.Drawing.Size(0, 43);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(198, 43);
            this.tableLayoutPanel11.TabIndex = 1;
            // 
            // dtpRangeStart
            // 
            this.dtpRangeStart.BackColor = System.Drawing.Color.Transparent;
            this.dtpRangeStart.BaseColor = System.Drawing.Color.White;
            this.dtpRangeStart.BorderColor = System.Drawing.Color.Silver;
            this.dtpRangeStart.BorderSize = 1;
            this.dtpRangeStart.CustomFormat = null;
            this.dtpRangeStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpRangeStart.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpRangeStart.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpRangeStart.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dtpRangeStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpRangeStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRangeStart.Location = new System.Drawing.Point(53, 5);
            this.dtpRangeStart.Margin = new System.Windows.Forms.Padding(5);
            this.dtpRangeStart.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpRangeStart.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpRangeStart.Name = "dtpRangeStart";
            this.dtpRangeStart.OnHoverBaseColor = System.Drawing.Color.White;
            this.dtpRangeStart.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpRangeStart.OnHoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpRangeStart.OnPressedColor = System.Drawing.Color.Black;
            this.dtpRangeStart.Radius = 3;
            this.dtpRangeStart.Size = new System.Drawing.Size(140, 33);
            this.dtpRangeStart.TabIndex = 240;
            this.dtpRangeStart.Text = "6/12/2023";
            this.dtpRangeStart.Value = new System.DateTime(2023, 12, 6, 10, 56, 27, 880);
            this.dtpRangeStart.Visible = false;
            // 
            // lblRangeStart
            // 
            this.lblRangeStart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRangeStart.AutoSize = true;
            this.lblRangeStart.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRangeStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRangeStart.Location = new System.Drawing.Point(8, 13);
            this.lblRangeStart.Margin = new System.Windows.Forms.Padding(5);
            this.lblRangeStart.Name = "lblRangeStart";
            this.lblRangeStart.Size = new System.Drawing.Size(35, 17);
            this.lblRangeStart.TabIndex = 242;
            this.lblRangeStart.Text = "Start";
            this.lblRangeStart.Visible = false;
            // 
            // gunaGroupBox2
            // 
            this.gunaGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox2.BaseColor = System.Drawing.Color.WhiteSmoke;
            this.gunaGroupBox2.BorderColor = System.Drawing.Color.LightGray;
            this.gunaGroupBox2.BorderSize = 1;
            this.gunaGroupBox2.Controls.Add(this.tableLayoutPanel7);
            this.gunaGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox2.LineColor = System.Drawing.Color.LightGray;
            this.gunaGroupBox2.Location = new System.Drawing.Point(223, 3);
            this.gunaGroupBox2.Name = "gunaGroupBox2";
            this.gunaGroupBox2.Padding = new System.Windows.Forms.Padding(3, 33, 3, 3);
            this.gunaGroupBox2.Radius = 3;
            this.gunaGroupBox2.Size = new System.Drawing.Size(294, 168);
            this.gunaGroupBox2.TabIndex = 255;
            this.gunaGroupBox2.Text = "Sorting";
            this.gunaGroupBox2.TextLocation = new System.Drawing.Point(10, 8);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.cmbPOSortBy, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.cmbProductSortBy, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 5;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(288, 132);
            this.tableLayoutPanel7.TabIndex = 258;
            // 
            // cmbPOSortBy
            // 
            this.cmbPOSortBy.BackColor = System.Drawing.Color.Transparent;
            this.cmbPOSortBy.BaseColor = System.Drawing.Color.White;
            this.cmbPOSortBy.BorderColor = System.Drawing.Color.Silver;
            this.cmbPOSortBy.BorderSize = 1;
            this.cmbPOSortBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPOSortBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPOSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPOSortBy.FocusedColor = System.Drawing.Color.Empty;
            this.cmbPOSortBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPOSortBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbPOSortBy.FormattingEnabled = true;
            this.cmbPOSortBy.Location = new System.Drawing.Point(3, 28);
            this.cmbPOSortBy.Name = "cmbPOSortBy";
            this.cmbPOSortBy.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmbPOSortBy.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cmbPOSortBy.Radius = 3;
            this.cmbPOSortBy.Size = new System.Drawing.Size(282, 31);
            this.cmbPOSortBy.TabIndex = 253;
            this.cmbPOSortBy.SelectedIndexChanged += new System.EventHandler(this.cmbPOSortBy_SelectedIndexChanged);
            // 
            // cmbProductSortBy
            // 
            this.cmbProductSortBy.BackColor = System.Drawing.Color.Transparent;
            this.cmbProductSortBy.BaseColor = System.Drawing.Color.White;
            this.cmbProductSortBy.BorderColor = System.Drawing.Color.Silver;
            this.cmbProductSortBy.BorderSize = 1;
            this.cmbProductSortBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbProductSortBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProductSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductSortBy.FocusedColor = System.Drawing.Color.Empty;
            this.cmbProductSortBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbProductSortBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbProductSortBy.FormattingEnabled = true;
            this.cmbProductSortBy.Location = new System.Drawing.Point(3, 98);
            this.cmbProductSortBy.Name = "cmbProductSortBy";
            this.cmbProductSortBy.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmbProductSortBy.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cmbProductSortBy.Radius = 3;
            this.cmbProductSortBy.Size = new System.Drawing.Size(282, 31);
            this.cmbProductSortBy.TabIndex = 253;
            this.cmbProductSortBy.SelectedIndexChanged += new System.EventHandler(this.cmbProductSortBy_SelectedIndexChanged);
            // 
            // frmPOPlanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1354, 721);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmPOPlanner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "P/O Planner";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSBBDeliveryPlanning_FormClosing);
            this.Load += new System.EventHandler(this.frmSBBDeliveryPlanning_Load);
            this.Shown += new System.EventHandler(this.frmPOPlanner_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tlpPoList.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.gunaGroupBox1.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.gunaGroupBox4.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.gunaGroupBox3.ResumeLayout(false);
            this.gunaGroupBox5.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.gunaGroupBox2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox cbMergePO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAssemblyNeeded;
        private System.Windows.Forms.Label lblProductionAlert;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Label lblFillSettingReset;
        private System.Windows.Forms.Label lblTotalBag;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tlpPoList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Guna.UI.WinForms.GunaGradientButton btnUpdate;
        private System.Windows.Forms.Label lblMainList;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private Guna.UI.WinForms.GunaTextBox txtSearch;
        private Guna.UI.WinForms.GunaGradientButton gunaGradientButton1;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.Label lblRangeEnd;
        private Guna.UI.WinForms.GunaDateTimePicker dtpRangeEnd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.CheckBox cbCustomPeriod;
        private System.Windows.Forms.CheckBox cbAllTimeRange;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private Guna.UI.WinForms.GunaDateTimePicker dtpRangeStart;
        private System.Windows.Forms.Label lblRangeStart;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private Guna.UI.WinForms.GunaGradientButton btnFilterApply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Guna.UI.WinForms.GunaGradientButton btnLoadPO;
        private Guna.UI.WinForms.GunaGradientButton btnFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Guna.UI.WinForms.GunaComboBox cmbCustomerType;
        private Guna.UI.WinForms.GunaComboBox cmbCustomer;
        private Guna.UI.WinForms.GunaComboBox cmbEditUnit;
        private Guna.UI.WinForms.GunaComboBox cmbPOSortBy;
        private Guna.UI.WinForms.GunaComboBox cmbProductSortBy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.CheckBox checkBox1;
        private Guna.UI.WinForms.GunaGradientButton gunaGradientButton2;
    }
}