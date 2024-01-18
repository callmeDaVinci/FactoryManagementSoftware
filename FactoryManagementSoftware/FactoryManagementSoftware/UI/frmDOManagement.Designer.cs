namespace FactoryManagementSoftware.UI
{
    partial class frmDOManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDOManagement));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpPOList = new System.Windows.Forms.TableLayoutPanel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.gunaGroupBox4 = new Guna.UI.WinForms.GunaGroupBox();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.gunaComboBox2 = new Guna.UI.WinForms.GunaComboBox();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.cbRemoved = new System.Windows.Forms.CheckBox();
            this.cbInProgress = new System.Windows.Forms.CheckBox();
            this.cbCompleted = new System.Windows.Forms.CheckBox();
            this.btnFilterApply = new Guna.UI.WinForms.GunaGradientButton();
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
            this.txtFrom = new Guna.UI.WinForms.GunaTextBox();
            this.gunaComboBox1 = new Guna.UI.WinForms.GunaComboBox();
            this.gunaGroupBox3 = new Guna.UI.WinForms.GunaGroupBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.txtTo = new Guna.UI.WinForms.GunaTextBox();
            this.cmbCustomer = new Guna.UI.WinForms.GunaComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRefresh = new Guna.UI.WinForms.GunaGradientButton();
            this.btnFilter = new Guna.UI.WinForms.GunaGradientButton();
            this.gunaGroupBox1 = new Guna.UI.WinForms.GunaGroupBox();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSearch = new Guna.UI.WinForms.GunaTextBox();
            this.gunaGradientButton1 = new Guna.UI.WinForms.GunaGradientButton();
            this.tlpList = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDOList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewJob = new Guna.UI.WinForms.GunaGradientButton();
            this.btnExcel = new Guna.UI.WinForms.GunaGradientButton();
            this.lblMainList = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDOItemList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new Guna.UI.WinForms.GunaGradientButton();
            this.lblSubList = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpPOList.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.gunaGroupBox4.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.gunaGroupBox5.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.gunaGroupBox2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.gunaGroupBox3.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gunaGroupBox1.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tlpList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDOList)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDOItemList)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpPOList
            // 
            this.tlpPOList.ColumnCount = 1;
            this.tlpPOList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPOList.Controls.Add(this.gbFilter, 0, 1);
            this.tlpPOList.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpPOList.Controls.Add(this.tlpList, 0, 3);
            this.tlpPOList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPOList.Location = new System.Drawing.Point(20, 20);
            this.tlpPOList.Margin = new System.Windows.Forms.Padding(20);
            this.tlpPOList.Name = "tlpPOList";
            this.tlpPOList.RowCount = 4;
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPOList.Size = new System.Drawing.Size(1308, 663);
            this.tlpPOList.TabIndex = 172;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.tableLayoutPanel14);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 53);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1302, 204);
            this.gbFilter.TabIndex = 172;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "FILTER";
            this.gbFilter.Enter += new System.EventHandler(this.gbFilter_Enter);
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 7;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 233F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 233F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 233F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.gunaGroupBox4, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.btnFilterApply, 5, 0);
            this.tableLayoutPanel14.Controls.Add(this.gunaGroupBox5, 3, 0);
            this.tableLayoutPanel14.Controls.Add(this.gunaGroupBox2, 1, 0);
            this.tableLayoutPanel14.Controls.Add(this.gunaGroupBox3, 2, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(1296, 184);
            this.tableLayoutPanel14.TabIndex = 263;
            // 
            // gunaGroupBox4
            // 
            this.gunaGroupBox4.BackColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.gunaGroupBox4.BorderColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox4.BorderSize = 1;
            this.gunaGroupBox4.Controls.Add(this.tableLayoutPanel13);
            this.gunaGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox4.LineColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox4.Location = new System.Drawing.Point(3, 3);
            this.gunaGroupBox4.Name = "gunaGroupBox4";
            this.gunaGroupBox4.Padding = new System.Windows.Forms.Padding(3, 33, 3, 3);
            this.gunaGroupBox4.Radius = 3;
            this.gunaGroupBox4.Size = new System.Drawing.Size(227, 178);
            this.gunaGroupBox4.TabIndex = 262;
            this.gunaGroupBox4.Text = "D/O Type & Status";
            this.gunaGroupBox4.TextLocation = new System.Drawing.Point(10, 8);
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 1;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.gunaComboBox2, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel15, 0, 2);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 3;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(221, 142);
            this.tableLayoutPanel13.TabIndex = 258;
            // 
            // gunaComboBox2
            // 
            this.gunaComboBox2.BackColor = System.Drawing.Color.Transparent;
            this.gunaComboBox2.BaseColor = System.Drawing.Color.White;
            this.gunaComboBox2.BorderColor = System.Drawing.Color.Silver;
            this.gunaComboBox2.BorderSize = 1;
            this.gunaComboBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.gunaComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gunaComboBox2.FocusedColor = System.Drawing.Color.Empty;
            this.gunaComboBox2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.gunaComboBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gunaComboBox2.Items.AddRange(new object[] {
            "Internal Transfer Note"});
            this.gunaComboBox2.Location = new System.Drawing.Point(5, 5);
            this.gunaComboBox2.Margin = new System.Windows.Forms.Padding(5);
            this.gunaComboBox2.Name = "gunaComboBox2";
            this.gunaComboBox2.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaComboBox2.OnHoverItemForeColor = System.Drawing.Color.White;
            this.gunaComboBox2.Radius = 3;
            this.gunaComboBox2.Size = new System.Drawing.Size(211, 26);
            this.gunaComboBox2.StartIndex = 0;
            this.gunaComboBox2.TabIndex = 253;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.0362F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.9638F));
            this.tableLayoutPanel15.Controls.Add(this.cbRemoved, 1, 0);
            this.tableLayoutPanel15.Controls.Add(this.cbInProgress, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.cbCompleted, 0, 1);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(0, 46);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 2;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(221, 96);
            this.tableLayoutPanel15.TabIndex = 263;
            // 
            // cbRemoved
            // 
            this.cbRemoved.AutoSize = true;
            this.cbRemoved.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRemoved.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cbRemoved.Location = new System.Drawing.Point(125, 5);
            this.cbRemoved.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.cbRemoved.Name = "cbRemoved";
            this.cbRemoved.Size = new System.Drawing.Size(86, 21);
            this.cbRemoved.TabIndex = 248;
            this.cbRemoved.Text = "Cancelled";
            this.cbRemoved.UseVisualStyleBackColor = true;
            // 
            // cbInProgress
            // 
            this.cbInProgress.AutoSize = true;
            this.cbInProgress.Checked = true;
            this.cbInProgress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInProgress.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbInProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbInProgress.Location = new System.Drawing.Point(10, 5);
            this.cbInProgress.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.cbInProgress.Name = "cbInProgress";
            this.cbInProgress.Size = new System.Drawing.Size(97, 21);
            this.cbInProgress.TabIndex = 246;
            this.cbInProgress.Text = "In progress";
            this.cbInProgress.UseVisualStyleBackColor = true;
            // 
            // cbCompleted
            // 
            this.cbCompleted.AutoSize = true;
            this.cbCompleted.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompleted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cbCompleted.Location = new System.Drawing.Point(10, 45);
            this.cbCompleted.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.cbCompleted.Name = "cbCompleted";
            this.cbCompleted.Size = new System.Drawing.Size(94, 21);
            this.cbCompleted.TabIndex = 247;
            this.cbCompleted.Text = "Completed";
            this.cbCompleted.UseVisualStyleBackColor = true;
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterApply.AnimationHoverSpeed = 0.07F;
            this.btnFilterApply.AnimationSpeed = 0.03F;
            this.btnFilterApply.BackColor = System.Drawing.Color.Transparent;
            this.btnFilterApply.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(107)))), ((int)(((byte)(236)))));
            this.btnFilterApply.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnFilterApply.BorderColor = System.Drawing.Color.Black;
            this.btnFilterApply.BorderSize = 1;
            this.btnFilterApply.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFilterApply.FocusedColor = System.Drawing.Color.Empty;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilterApply.ForeColor = System.Drawing.Color.White;
            this.btnFilterApply.Image = null;
            this.btnFilterApply.ImageSize = new System.Drawing.Size(20, 20);
            this.btnFilterApply.Location = new System.Drawing.Point(1071, 3);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnFilterApply.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnFilterApply.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFilterApply.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFilterApply.OnHoverImage = null;
            this.btnFilterApply.OnPressedColor = System.Drawing.Color.Black;
            this.btnFilterApply.Radius = 3;
            this.btnFilterApply.Size = new System.Drawing.Size(136, 40);
            this.btnFilterApply.TabIndex = 227;
            this.btnFilterApply.Text = "Apply";
            this.btnFilterApply.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // gunaGroupBox5
            // 
            this.gunaGroupBox5.BackColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox5.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.gunaGroupBox5.BorderColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox5.BorderSize = 1;
            this.gunaGroupBox5.Controls.Add(this.tableLayoutPanel8);
            this.gunaGroupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox5.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.gunaGroupBox5.LineColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox5.Location = new System.Drawing.Point(819, 3);
            this.gunaGroupBox5.Name = "gunaGroupBox5";
            this.gunaGroupBox5.Padding = new System.Windows.Forms.Padding(3, 33, 3, 3);
            this.gunaGroupBox5.Radius = 3;
            this.gunaGroupBox5.Size = new System.Drawing.Size(227, 178);
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
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(221, 142);
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
            this.tableLayoutPanel12.Size = new System.Drawing.Size(211, 43);
            this.tableLayoutPanel12.TabIndex = 262;
            // 
            // lblRangeEnd
            // 
            this.lblRangeEnd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRangeEnd.AutoSize = true;
            this.lblRangeEnd.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRangeEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRangeEnd.Location = new System.Drawing.Point(16, 13);
            this.lblRangeEnd.Margin = new System.Windows.Forms.Padding(5);
            this.lblRangeEnd.Name = "lblRangeEnd";
            this.lblRangeEnd.Size = new System.Drawing.Size(30, 17);
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
            this.dtpRangeEnd.Location = new System.Drawing.Point(56, 5);
            this.dtpRangeEnd.Margin = new System.Windows.Forms.Padding(5);
            this.dtpRangeEnd.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpRangeEnd.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpRangeEnd.Name = "dtpRangeEnd";
            this.dtpRangeEnd.OnHoverBaseColor = System.Drawing.Color.White;
            this.dtpRangeEnd.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpRangeEnd.OnHoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpRangeEnd.OnPressedColor = System.Drawing.Color.Black;
            this.dtpRangeEnd.Radius = 3;
            this.dtpRangeEnd.Size = new System.Drawing.Size(150, 33);
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
            this.tableLayoutPanel10.Size = new System.Drawing.Size(221, 36);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // cbCustomPeriod
            // 
            this.cbCustomPeriod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCustomPeriod.AutoSize = true;
            this.cbCustomPeriod.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.cbCustomPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbCustomPeriod.Location = new System.Drawing.Point(104, 7);
            this.cbCustomPeriod.Name = "cbCustomPeriod";
            this.cbCustomPeriod.Size = new System.Drawing.Size(74, 21);
            this.cbCustomPeriod.TabIndex = 239;
            this.cbCustomPeriod.Text = "Custom";
            this.cbCustomPeriod.UseVisualStyleBackColor = true;
            this.cbCustomPeriod.CheckedChanged += new System.EventHandler(this.cbCustomPeriod_CheckedChanged);
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
            this.cbAllTimeRange.CheckedChanged += new System.EventHandler(this.cbAllTimeRange_CheckedChanged);
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
            this.tableLayoutPanel11.Size = new System.Drawing.Size(211, 43);
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
            this.dtpRangeStart.Location = new System.Drawing.Point(56, 5);
            this.dtpRangeStart.Margin = new System.Windows.Forms.Padding(5);
            this.dtpRangeStart.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpRangeStart.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpRangeStart.Name = "dtpRangeStart";
            this.dtpRangeStart.OnHoverBaseColor = System.Drawing.Color.White;
            this.dtpRangeStart.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpRangeStart.OnHoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpRangeStart.OnPressedColor = System.Drawing.Color.Black;
            this.dtpRangeStart.Radius = 3;
            this.dtpRangeStart.Size = new System.Drawing.Size(150, 33);
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
            this.lblRangeStart.Location = new System.Drawing.Point(11, 13);
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
            this.gunaGroupBox2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.gunaGroupBox2.BorderColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox2.BorderSize = 1;
            this.gunaGroupBox2.Controls.Add(this.tableLayoutPanel7);
            this.gunaGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox2.LineColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox2.Location = new System.Drawing.Point(236, 3);
            this.gunaGroupBox2.Name = "gunaGroupBox2";
            this.gunaGroupBox2.Padding = new System.Windows.Forms.Padding(3, 33, 3, 3);
            this.gunaGroupBox2.Radius = 3;
            this.gunaGroupBox2.Size = new System.Drawing.Size(227, 178);
            this.gunaGroupBox2.TabIndex = 255;
            this.gunaGroupBox2.Text = "From";
            this.gunaGroupBox2.TextLocation = new System.Drawing.Point(10, 8);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.txtFrom, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.gunaComboBox1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(221, 142);
            this.tableLayoutPanel7.TabIndex = 258;
            // 
            // txtFrom
            // 
            this.txtFrom.BackColor = System.Drawing.Color.Transparent;
            this.txtFrom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtFrom.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtFrom.BorderColor = System.Drawing.Color.Silver;
            this.txtFrom.BorderSize = 1;
            this.txtFrom.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFrom.FocusedBaseColor = System.Drawing.Color.White;
            this.txtFrom.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtFrom.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtFrom.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.txtFrom.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtFrom.Location = new System.Drawing.Point(5, 41);
            this.txtFrom.Margin = new System.Windows.Forms.Padding(5);
            this.txtFrom.MaximumSize = new System.Drawing.Size(0, 33);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.PasswordChar = '\0';
            this.txtFrom.Radius = 3;
            this.txtFrom.SelectedText = "";
            this.txtFrom.Size = new System.Drawing.Size(211, 33);
            this.txtFrom.TabIndex = 250;
            this.txtFrom.Text = "Custom \"From\" :";
            this.txtFrom.Enter += new System.EventHandler(this.txtFrom_Enter);
            this.txtFrom.Leave += new System.EventHandler(this.txtFrom_Leave);
            // 
            // gunaComboBox1
            // 
            this.gunaComboBox1.BackColor = System.Drawing.Color.Transparent;
            this.gunaComboBox1.BaseColor = System.Drawing.Color.White;
            this.gunaComboBox1.BorderColor = System.Drawing.Color.Silver;
            this.gunaComboBox1.BorderSize = 1;
            this.gunaComboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.gunaComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gunaComboBox1.FocusedColor = System.Drawing.Color.Empty;
            this.gunaComboBox1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.gunaComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gunaComboBox1.FormattingEnabled = true;
            this.gunaComboBox1.Items.AddRange(new object[] {
            "All"});
            this.gunaComboBox1.Location = new System.Drawing.Point(5, 5);
            this.gunaComboBox1.Margin = new System.Windows.Forms.Padding(5);
            this.gunaComboBox1.Name = "gunaComboBox1";
            this.gunaComboBox1.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaComboBox1.OnHoverItemForeColor = System.Drawing.Color.White;
            this.gunaComboBox1.Radius = 3;
            this.gunaComboBox1.Size = new System.Drawing.Size(211, 26);
            this.gunaComboBox1.StartIndex = 0;
            this.gunaComboBox1.TabIndex = 252;
            // 
            // gunaGroupBox3
            // 
            this.gunaGroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.gunaGroupBox3.BorderColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox3.BorderSize = 1;
            this.gunaGroupBox3.Controls.Add(this.tableLayoutPanel9);
            this.gunaGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox3.LineColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox3.Location = new System.Drawing.Point(469, 3);
            this.gunaGroupBox3.Name = "gunaGroupBox3";
            this.gunaGroupBox3.Padding = new System.Windows.Forms.Padding(3, 33, 3, 3);
            this.gunaGroupBox3.Radius = 3;
            this.gunaGroupBox3.Size = new System.Drawing.Size(344, 178);
            this.gunaGroupBox3.TabIndex = 260;
            this.gunaGroupBox3.Text = "To";
            this.gunaGroupBox3.TextLocation = new System.Drawing.Point(10, 8);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.txtTo, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.cmbCustomer, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(338, 142);
            this.tableLayoutPanel9.TabIndex = 258;
            // 
            // txtTo
            // 
            this.txtTo.BackColor = System.Drawing.Color.Transparent;
            this.txtTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtTo.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTo.BorderColor = System.Drawing.Color.Silver;
            this.txtTo.BorderSize = 1;
            this.txtTo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTo.FocusedBaseColor = System.Drawing.Color.White;
            this.txtTo.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtTo.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtTo.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.txtTo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtTo.Location = new System.Drawing.Point(5, 41);
            this.txtTo.Margin = new System.Windows.Forms.Padding(5);
            this.txtTo.MaximumSize = new System.Drawing.Size(1000, 33);
            this.txtTo.Name = "txtTo";
            this.txtTo.PasswordChar = '\0';
            this.txtTo.Radius = 3;
            this.txtTo.SelectedText = "";
            this.txtTo.Size = new System.Drawing.Size(328, 33);
            this.txtTo.TabIndex = 250;
            this.txtTo.Text = "Custom \"To\" :";
            this.txtTo.Enter += new System.EventHandler(this.txtTo_Enter);
            this.txtTo.Leave += new System.EventHandler(this.txtTo_Leave);
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.BackColor = System.Drawing.Color.Transparent;
            this.cmbCustomer.BaseColor = System.Drawing.Color.White;
            this.cmbCustomer.BorderColor = System.Drawing.Color.Silver;
            this.cmbCustomer.BorderSize = 1;
            this.cmbCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FocusedColor = System.Drawing.Color.Empty;
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Items.AddRange(new object[] {
            "All"});
            this.cmbCustomer.Location = new System.Drawing.Point(5, 5);
            this.cmbCustomer.Margin = new System.Windows.Forms.Padding(5);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmbCustomer.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cmbCustomer.Radius = 3;
            this.cmbCustomer.Size = new System.Drawing.Size(328, 26);
            this.cmbCustomer.StartIndex = 0;
            this.cmbCustomer.TabIndex = 251;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 369F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnRefresh, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.gunaGroupBox1, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1308, 50);
            this.tableLayoutPanel2.TabIndex = 160;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AnimationHoverSpeed = 0.07F;
            this.btnRefresh.AnimationSpeed = 0.03F;
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(107)))), ((int)(((byte)(236)))));
            this.btnRefresh.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnRefresh.BorderColor = System.Drawing.Color.Black;
            this.btnRefresh.BorderSize = 1;
            this.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefresh.FocusedColor = System.Drawing.Color.Empty;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Image = null;
            this.btnRefresh.ImageSize = new System.Drawing.Size(20, 20);
            this.btnRefresh.Location = new System.Drawing.Point(145, 5);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnRefresh.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnRefresh.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnRefresh.OnHoverForeColor = System.Drawing.Color.White;
            this.btnRefresh.OnHoverImage = null;
            this.btnRefresh.OnPressedColor = System.Drawing.Color.Black;
            this.btnRefresh.Radius = 3;
            this.btnRefresh.Size = new System.Drawing.Size(130, 40);
            this.btnRefresh.TabIndex = 228;
            this.btnRefresh.Text = "Reload";
            this.btnRefresh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
            this.btnFilter.Location = new System.Drawing.Point(5, 5);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(5);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnFilter.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnFilter.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFilter.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFilter.OnHoverImage = null;
            this.btnFilter.OnPressedColor = System.Drawing.Color.Black;
            this.btnFilter.Radius = 3;
            this.btnFilter.Size = new System.Drawing.Size(130, 40);
            this.btnFilter.TabIndex = 230;
            this.btnFilter.Text = "Show Filter";
            this.btnFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
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
            this.gunaGroupBox1.Location = new System.Drawing.Point(285, 5);
            this.gunaGroupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.gunaGroupBox1.Name = "gunaGroupBox1";
            this.gunaGroupBox1.Padding = new System.Windows.Forms.Padding(3);
            this.gunaGroupBox1.Radius = 3;
            this.gunaGroupBox1.Size = new System.Drawing.Size(359, 40);
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
            this.tableLayoutPanel16.Size = new System.Drawing.Size(353, 34);
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
            this.txtSearch.Size = new System.Drawing.Size(297, 28);
            this.txtSearch.TabIndex = 245;
            this.txtSearch.Text = "Search (DO Number Or Item Name/Code)";
            this.txtSearch.Enter += new System.EventHandler(this.gunaTextBox1_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
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
            // tlpList
            // 
            this.tlpList.ColumnCount = 3;
            this.tlpList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpList.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpList.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tlpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpList.Location = new System.Drawing.Point(3, 293);
            this.tlpList.Name = "tlpList";
            this.tlpList.RowCount = 1;
            this.tlpList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpList.Size = new System.Drawing.Size(1302, 367);
            this.tlpList.TabIndex = 163;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvDOList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(646, 367);
            this.tableLayoutPanel1.TabIndex = 156;
            // 
            // dgvDOList
            // 
            this.dgvDOList.AllowUserToAddRows = false;
            this.dgvDOList.AllowUserToDeleteRows = false;
            this.dgvDOList.AllowUserToOrderColumns = true;
            this.dgvDOList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDOList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDOList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvDOList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDOList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDOList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDOList.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDOList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDOList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvDOList.Location = new System.Drawing.Point(5, 55);
            this.dgvDOList.Margin = new System.Windows.Forms.Padding(5);
            this.dgvDOList.Name = "dgvDOList";
            this.dgvDOList.ReadOnly = true;
            this.dgvDOList.RowHeadersVisible = false;
            this.dgvDOList.RowHeadersWidth = 51;
            this.dgvDOList.RowTemplate.Height = 50;
            this.dgvDOList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDOList.Size = new System.Drawing.Size(636, 307);
            this.dgvDOList.TabIndex = 156;
            this.dgvDOList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDOList_CellClick);
            this.dgvDOList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDOList_CellDoubleClick);
            this.dgvDOList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDOList_CellMouseDown);
            this.dgvDOList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDOList_DataBindingComplete);
            this.dgvDOList.SelectionChanged += new System.EventHandler(this.dgvDOList_SelectionChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 216F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel4.Controls.Add(this.btnNewJob, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnExcel, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblMainList, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(646, 50);
            this.tableLayoutPanel4.TabIndex = 167;
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
            this.btnNewJob.Location = new System.Drawing.Point(371, 5);
            this.btnNewJob.Margin = new System.Windows.Forms.Padding(5);
            this.btnNewJob.Name = "btnNewJob";
            this.btnNewJob.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnNewJob.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnNewJob.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnNewJob.OnHoverForeColor = System.Drawing.Color.White;
            this.btnNewJob.OnHoverImage = null;
            this.btnNewJob.OnPressedColor = System.Drawing.Color.Black;
            this.btnNewJob.Radius = 2;
            this.btnNewJob.Size = new System.Drawing.Size(130, 40);
            this.btnNewJob.TabIndex = 224;
            this.btnNewJob.Text = "New D/O";
            this.btnNewJob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnNewJob.Click += new System.EventHandler(this.btnNewJob_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.AnimationHoverSpeed = 0.07F;
            this.btnExcel.AnimationSpeed = 0.03F;
            this.btnExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExcel.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(221)))), ((int)(((byte)(218)))));
            this.btnExcel.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(241)))), ((int)(((byte)(218)))));
            this.btnExcel.BorderColor = System.Drawing.Color.Black;
            this.btnExcel.BorderSize = 1;
            this.btnExcel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExcel.FocusedColor = System.Drawing.Color.Empty;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnExcel.Image = null;
            this.btnExcel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnExcel.Location = new System.Drawing.Point(511, 5);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(5);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnExcel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnExcel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnExcel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnExcel.OnHoverImage = null;
            this.btnExcel.OnPressedColor = System.Drawing.Color.Black;
            this.btnExcel.Radius = 2;
            this.btnExcel.Size = new System.Drawing.Size(130, 40);
            this.btnExcel.TabIndex = 230;
            this.btnExcel.Text = "Excel";
            this.btnExcel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // lblMainList
            // 
            this.lblMainList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMainList.AutoSize = true;
            this.lblMainList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMainList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMainList.Location = new System.Drawing.Point(5, 25);
            this.lblMainList.Margin = new System.Windows.Forms.Padding(5);
            this.lblMainList.Name = "lblMainList";
            this.lblMainList.Size = new System.Drawing.Size(63, 20);
            this.lblMainList.TabIndex = 166;
            this.lblMainList.Text = "D/O List";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dgvDOItemList, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(656, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(646, 367);
            this.tableLayoutPanel3.TabIndex = 157;
            // 
            // dgvDOItemList
            // 
            this.dgvDOItemList.AllowUserToAddRows = false;
            this.dgvDOItemList.AllowUserToDeleteRows = false;
            this.dgvDOItemList.AllowUserToOrderColumns = true;
            this.dgvDOItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDOItemList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.dgvDOItemList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDOItemList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDOItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDOItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDOItemList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDOItemList.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvDOItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDOItemList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvDOItemList.Location = new System.Drawing.Point(5, 55);
            this.dgvDOItemList.Margin = new System.Windows.Forms.Padding(5);
            this.dgvDOItemList.Name = "dgvDOItemList";
            this.dgvDOItemList.RowHeadersVisible = false;
            this.dgvDOItemList.RowHeadersWidth = 51;
            this.dgvDOItemList.RowTemplate.Height = 50;
            this.dgvDOItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDOItemList.Size = new System.Drawing.Size(636, 307);
            this.dgvDOItemList.TabIndex = 155;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel5.Controls.Add(this.btnEdit, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblSubList, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(646, 50);
            this.tableLayoutPanel5.TabIndex = 156;
            // 
            // btnEdit
            // 
            this.btnEdit.AnimationHoverSpeed = 0.07F;
            this.btnEdit.AnimationSpeed = 0.03F;
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.BaseColor1 = System.Drawing.Color.White;
            this.btnEdit.BaseColor2 = System.Drawing.Color.White;
            this.btnEdit.BorderColor = System.Drawing.Color.Black;
            this.btnEdit.BorderSize = 1;
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEdit.FocusedColor = System.Drawing.Color.Empty;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnEdit.Image = null;
            this.btnEdit.ImageSize = new System.Drawing.Size(20, 20);
            this.btnEdit.Location = new System.Drawing.Point(511, 5);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnEdit.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnEdit.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnEdit.OnHoverForeColor = System.Drawing.Color.White;
            this.btnEdit.OnHoverImage = null;
            this.btnEdit.OnPressedColor = System.Drawing.Color.Black;
            this.btnEdit.Radius = 3;
            this.btnEdit.Size = new System.Drawing.Size(130, 40);
            this.btnEdit.TabIndex = 230;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblSubList
            // 
            this.lblSubList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSubList.AutoSize = true;
            this.lblSubList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSubList.Location = new System.Drawing.Point(5, 25);
            this.lblSubList.Margin = new System.Windows.Forms.Padding(5);
            this.lblSubList.Name = "lblSubList";
            this.lblSubList.Size = new System.Drawing.Size(97, 20);
            this.lblSubList.TabIndex = 157;
            this.lblSubList.Text = "D/O Item List";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.tlpPOList, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1348, 703);
            this.tableLayoutPanel6.TabIndex = 173;
            // 
            // frmDOManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1348, 703);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "frmDOManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delivery Order Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSBBDOListVer2_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSPPDOList_FormClosed);
            this.Load += new System.EventHandler(this.frmSPPDOList_Load);
            this.Shown += new System.EventHandler(this.frmSBBDOListVer2_Shown);
            this.tlpPOList.ResumeLayout(false);
            this.gbFilter.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.gunaGroupBox4.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
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
            this.gunaGroupBox3.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.gunaGroupBox1.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tlpList.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDOList)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDOItemList)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPOList;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tlpList;
        private System.Windows.Forms.DataGridView dgvDOList;
        private System.Windows.Forms.DataGridView dgvDOItemList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblSubList;
        private System.Windows.Forms.Label lblMainList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Guna.UI.WinForms.GunaGradientButton btnFilterApply;
        private Guna.UI.WinForms.GunaGradientButton btnEdit;
        private Guna.UI.WinForms.GunaGradientButton btnExcel;
        private Guna.UI.WinForms.GunaGradientButton btnFilter;
        private Guna.UI.WinForms.GunaGradientButton btnRefresh;
        private System.Windows.Forms.Label lblRangeEnd;
        private System.Windows.Forms.Label lblRangeStart;
        private Guna.UI.WinForms.GunaDateTimePicker dtpRangeStart;
        private System.Windows.Forms.CheckBox cbCustomPeriod;
        private System.Windows.Forms.CheckBox cbAllTimeRange;
        private Guna.UI.WinForms.GunaTextBox txtSearch;
        private System.Windows.Forms.CheckBox cbRemoved;
        private System.Windows.Forms.CheckBox cbCompleted;
        private System.Windows.Forms.CheckBox cbInProgress;
        private Guna.UI.WinForms.GunaComboBox gunaComboBox2;
        private Guna.UI.WinForms.GunaComboBox gunaComboBox1;
        private Guna.UI.WinForms.GunaComboBox cmbCustomer;
        private Guna.UI.WinForms.GunaTextBox txtFrom;
        private Guna.UI.WinForms.GunaGradientButton gunaGradientButton1;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private Guna.UI.WinForms.GunaTextBox txtTo;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private Guna.UI.WinForms.GunaDateTimePicker dtpRangeEnd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private Guna.UI.WinForms.GunaGradientButton btnNewJob;
    }
}