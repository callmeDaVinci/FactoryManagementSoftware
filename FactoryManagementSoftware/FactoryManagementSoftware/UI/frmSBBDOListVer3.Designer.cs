namespace FactoryManagementSoftware.UI
{
    partial class frmSBBDOListVer3
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpPOList = new System.Windows.Forms.TableLayoutPanel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnFilterApply = new Guna.UI.WinForms.GunaGradientButton();
            this.cbRemoved = new System.Windows.Forms.CheckBox();
            this.cbCompleted = new System.Windows.Forms.CheckBox();
            this.cbInProgress = new System.Windows.Forms.CheckBox();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRefresh = new Guna.UI.WinForms.GunaGradientButton();
            this.btnFilter = new Guna.UI.WinForms.GunaGradientButton();
            this.tlpList = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDOList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new Guna.UI.WinForms.GunaGradientButton();
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
            this.tableLayoutPanel2.SuspendLayout();
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
            this.tlpPOList.Controls.Add(this.tlpList, 0, 2);
            this.tlpPOList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPOList.Location = new System.Drawing.Point(20, 20);
            this.tlpPOList.Margin = new System.Windows.Forms.Padding(20);
            this.tlpPOList.Name = "tlpPOList";
            this.tlpPOList.RowCount = 3;
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPOList.Size = new System.Drawing.Size(1308, 663);
            this.tlpPOList.TabIndex = 172;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnFilterApply);
            this.gbFilter.Controls.Add(this.cbRemoved);
            this.gbFilter.Controls.Add(this.cbCompleted);
            this.gbFilter.Controls.Add(this.cbInProgress);
            this.gbFilter.Controls.Add(this.cmbCustomer);
            this.gbFilter.Controls.Add(this.lblCustomer);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(3, 53);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1302, 119);
            this.gbFilter.TabIndex = 172;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "FILTER";
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
            this.btnFilterApply.Location = new System.Drawing.Point(508, 41);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnFilterApply.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnFilterApply.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFilterApply.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFilterApply.OnHoverImage = null;
            this.btnFilterApply.OnPressedColor = System.Drawing.Color.Black;
            this.btnFilterApply.Radius = 2;
            this.btnFilterApply.Size = new System.Drawing.Size(130, 40);
            this.btnFilterApply.TabIndex = 227;
            this.btnFilterApply.Text = "Apply";
            this.btnFilterApply.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // cbRemoved
            // 
            this.cbRemoved.AutoSize = true;
            this.cbRemoved.ForeColor = System.Drawing.Color.Red;
            this.cbRemoved.Location = new System.Drawing.Point(54, 78);
            this.cbRemoved.Name = "cbRemoved";
            this.cbRemoved.Size = new System.Drawing.Size(86, 21);
            this.cbRemoved.TabIndex = 159;
            this.cbRemoved.Text = "Cancelled";
            this.cbRemoved.UseVisualStyleBackColor = true;
            // 
            // cbCompleted
            // 
            this.cbCompleted.AutoSize = true;
            this.cbCompleted.ForeColor = System.Drawing.Color.Green;
            this.cbCompleted.Location = new System.Drawing.Point(54, 51);
            this.cbCompleted.Name = "cbCompleted";
            this.cbCompleted.Size = new System.Drawing.Size(94, 21);
            this.cbCompleted.TabIndex = 158;
            this.cbCompleted.Text = "Completed";
            this.cbCompleted.UseVisualStyleBackColor = true;
            // 
            // cbInProgress
            // 
            this.cbInProgress.AutoSize = true;
            this.cbInProgress.Checked = true;
            this.cbInProgress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInProgress.Location = new System.Drawing.Point(54, 24);
            this.cbInProgress.Name = "cbInProgress";
            this.cbInProgress.Size = new System.Drawing.Size(97, 21);
            this.cbInProgress.TabIndex = 157;
            this.cbInProgress.Text = "In progress";
            this.cbInProgress.UseVisualStyleBackColor = true;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(184, 41);
            this.cmbCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(298, 25);
            this.cmbCustomer.TabIndex = 156;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(181, 20);
            this.lblCustomer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(64, 17);
            this.lblCustomer.TabIndex = 155;
            this.lblCustomer.Text = "Customer";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnRefresh, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 0, 0);
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
            this.btnRefresh.Radius = 2;
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
            this.btnFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
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
            this.btnFilter.Radius = 2;
            this.btnFilter.Size = new System.Drawing.Size(130, 40);
            this.btnFilter.TabIndex = 230;
            this.btnFilter.Text = "Show Filter";
            this.btnFilter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
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
            this.tlpList.Location = new System.Drawing.Point(3, 178);
            this.tlpList.Name = "tlpList";
            this.tlpList.RowCount = 1;
            this.tlpList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpList.Size = new System.Drawing.Size(1302, 482);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(646, 482);
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
            this.dgvDOList.Size = new System.Drawing.Size(636, 422);
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
            this.tableLayoutPanel4.Controls.Add(this.btnCancel, 2, 0);
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
            // btnCancel
            // 
            this.btnCancel.AnimationHoverSpeed = 0.07F;
            this.btnCancel.AnimationSpeed = 0.03F;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(172)))), ((int)(((byte)(159)))));
            this.btnCancel.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(159)))));
            this.btnCancel.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.FocusedColor = System.Drawing.Color.Empty;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnCancel.Image = null;
            this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnCancel.Location = new System.Drawing.Point(371, 5);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnCancel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnCancel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnCancel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnCancel.OnHoverImage = null;
            this.btnCancel.OnPressedColor = System.Drawing.Color.Black;
            this.btnCancel.Radius = 2;
            this.btnCancel.Size = new System.Drawing.Size(130, 40);
            this.btnCancel.TabIndex = 229;
            this.btnCancel.Text = "Remove D/O";
            this.btnCancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnRemove_Click);
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
            this.btnExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
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
            this.btnExcel.Text = "Select";
            this.btnExcel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // lblMainList
            // 
            this.lblMainList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMainList.AutoSize = true;
            this.lblMainList.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMainList.Location = new System.Drawing.Point(5, 22);
            this.lblMainList.Margin = new System.Windows.Forms.Padding(5);
            this.lblMainList.Name = "lblMainList";
            this.lblMainList.Size = new System.Drawing.Size(72, 23);
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(646, 482);
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
            this.dgvDOItemList.Size = new System.Drawing.Size(636, 422);
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
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
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
            this.btnEdit.Radius = 2;
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
            this.lblSubList.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubList.Location = new System.Drawing.Point(5, 22);
            this.lblSubList.Margin = new System.Windows.Forms.Padding(5);
            this.lblSubList.Name = "lblSubList";
            this.lblSubList.Size = new System.Drawing.Size(112, 23);
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
            // frmSBBDOListVer3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1348, 703);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "frmSBBDOListVer3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delivery Order Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSBBDOListVer2_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSPPDOList_FormClosed);
            this.Load += new System.EventHandler(this.frmSPPDOList_Load);
            this.Shown += new System.EventHandler(this.frmSBBDOListVer2_Shown);
            this.tlpPOList.ResumeLayout(false);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox cbCompleted;
        private System.Windows.Forms.CheckBox cbInProgress;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tlpList;
        private System.Windows.Forms.DataGridView dgvDOList;
        private System.Windows.Forms.DataGridView dgvDOItemList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblSubList;
        private System.Windows.Forms.Label lblMainList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.CheckBox cbRemoved;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Guna.UI.WinForms.GunaGradientButton btnFilterApply;
        private Guna.UI.WinForms.GunaGradientButton btnCancel;
        private Guna.UI.WinForms.GunaGradientButton btnEdit;
        private Guna.UI.WinForms.GunaGradientButton btnExcel;
        private Guna.UI.WinForms.GunaGradientButton btnFilter;
        private Guna.UI.WinForms.GunaGradientButton btnRefresh;
    }
}