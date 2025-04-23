namespace FactoryManagementSoftware.UI
{
    partial class frmProductionRecordVer3
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvRecordHistory = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider6 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider7 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider8 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider9 = new System.Windows.Forms.ErrorProvider(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpItemSelectionTitle = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAddJob = new Guna.UI.WinForms.GunaGradientButton();
            this.lblItemListTitle = new System.Windows.Forms.Label();
            this.dgvActiveJobList = new System.Windows.Forms.DataGridView();
            this.tlpMainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel27 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txtActualRejectQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRejectRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalMaxOutputQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQtyStockedIn = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPendingStockIn = new System.Windows.Forms.TextBox();
            this.txtRejectedQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel28 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label81 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tlpButton = new System.Windows.Forms.TableLayoutPanel();
            this.lblFinalDataReviewString = new System.Windows.Forms.Label();
            this.btnEditJobSheet = new Guna.UI.WinForms.GunaGradientButton();
            this.btnAddJobSheet = new Guna.UI.WinForms.GunaGradientButton();
            this.lblFinalDataReview = new System.Windows.Forms.Label();
            this.cbFinalDataReview = new System.Windows.Forms.CheckBox();
            this.errorProvider10 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.errorProvider11 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtEfficiency = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecordHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider9)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpItemSelectionTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveJobList)).BeginInit();
            this.tlpMainPanel.SuspendLayout();
            this.tableLayoutPanel27.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel28.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider10)).BeginInit();
            this.tableLayoutPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider11)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRecordHistory
            // 
            this.dgvRecordHistory.AllowUserToAddRows = false;
            this.dgvRecordHistory.AllowUserToDeleteRows = false;
            this.dgvRecordHistory.AllowUserToOrderColumns = true;
            this.dgvRecordHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvRecordHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.dgvRecordHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecordHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvRecordHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecordHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecordHistory.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvRecordHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecordHistory.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvRecordHistory.Location = new System.Drawing.Point(8, 56);
            this.dgvRecordHistory.Margin = new System.Windows.Forms.Padding(8, 0, 8, 7);
            this.dgvRecordHistory.MultiSelect = false;
            this.dgvRecordHistory.Name = "dgvRecordHistory";
            this.dgvRecordHistory.ReadOnly = true;
            this.dgvRecordHistory.RowHeadersVisible = false;
            this.dgvRecordHistory.RowHeadersWidth = 51;
            this.dgvRecordHistory.RowTemplate.Height = 60;
            this.dgvRecordHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecordHistory.Size = new System.Drawing.Size(815, 400);
            this.dgvRecordHistory.TabIndex = 154;
            this.dgvRecordHistory.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRecordHistory_CellFormatting);
            this.dgvRecordHistory.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRecordHistory_CellMouseDown);
            this.dgvRecordHistory.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvRecordHistory_DataBindingComplete);
            this.dgvRecordHistory.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecordHistory_RowEnter);
            this.dgvRecordHistory.SelectionChanged += new System.EventHandler(this.dgvRecordHistory_SelectionChanged);
            this.dgvRecordHistory.Sorted += new System.EventHandler(this.dgvRecordHistory_Sorted);
            this.dgvRecordHistory.Click += new System.EventHandler(this.dgvRecordHistory_Click);
            this.dgvRecordHistory.DoubleClick += new System.EventHandler(this.dgvRecordHistory_DoubleClick);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // errorProvider4
            // 
            this.errorProvider4.ContainerControl = this;
            // 
            // errorProvider5
            // 
            this.errorProvider5.ContainerControl = this;
            // 
            // errorProvider6
            // 
            this.errorProvider6.ContainerControl = this;
            // 
            // errorProvider7
            // 
            this.errorProvider7.ContainerControl = this;
            // 
            // errorProvider8
            // 
            this.errorProvider8.ContainerControl = this;
            // 
            // errorProvider9
            // 
            this.errorProvider9.ContainerControl = this;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tlpItemSelectionTitle, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dgvActiveJobList, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(447, 643);
            this.tableLayoutPanel2.TabIndex = 259;
            // 
            // tlpItemSelectionTitle
            // 
            this.tlpItemSelectionTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(147)))));
            this.tlpItemSelectionTitle.ColumnCount = 3;
            this.tlpItemSelectionTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpItemSelectionTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpItemSelectionTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tlpItemSelectionTitle.Controls.Add(this.button2, 0, 0);
            this.tlpItemSelectionTitle.Controls.Add(this.btnAddJob, 2, 0);
            this.tlpItemSelectionTitle.Controls.Add(this.lblItemListTitle, 1, 0);
            this.tlpItemSelectionTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpItemSelectionTitle.Location = new System.Drawing.Point(0, 0);
            this.tlpItemSelectionTitle.Margin = new System.Windows.Forms.Padding(0);
            this.tlpItemSelectionTitle.Name = "tlpItemSelectionTitle";
            this.tlpItemSelectionTitle.RowCount = 1;
            this.tlpItemSelectionTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpItemSelectionTitle.Size = new System.Drawing.Size(447, 46);
            this.tlpItemSelectionTitle.TabIndex = 262;
            // 
            // button2
            // 
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_refresh_480;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(147)))));
            this.button2.Location = new System.Drawing.Point(8, 7);
            this.button2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 32);
            this.button2.TabIndex = 261;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnAddJob
            // 
            this.btnAddJob.AnimationHoverSpeed = 0.07F;
            this.btnAddJob.AnimationSpeed = 0.03F;
            this.btnAddJob.BackColor = System.Drawing.Color.Transparent;
            this.btnAddJob.BaseColor1 = System.Drawing.Color.WhiteSmoke;
            this.btnAddJob.BaseColor2 = System.Drawing.Color.WhiteSmoke;
            this.btnAddJob.BorderColor = System.Drawing.Color.Black;
            this.btnAddJob.BorderSize = 1;
            this.btnAddJob.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAddJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddJob.FocusedColor = System.Drawing.Color.Empty;
            this.btnAddJob.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddJob.ForeColor = System.Drawing.Color.Black;
            this.btnAddJob.Image = null;
            this.btnAddJob.ImageSize = new System.Drawing.Size(20, 20);
            this.btnAddJob.Location = new System.Drawing.Point(338, 7);
            this.btnAddJob.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnAddJob.Name = "btnAddJob";
            this.btnAddJob.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnAddJob.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnAddJob.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnAddJob.OnHoverForeColor = System.Drawing.Color.White;
            this.btnAddJob.OnHoverImage = null;
            this.btnAddJob.OnPressedColor = System.Drawing.Color.Black;
            this.btnAddJob.Radius = 2;
            this.btnAddJob.Size = new System.Drawing.Size(101, 32);
            this.btnAddJob.TabIndex = 262;
            this.btnAddJob.Text = "Add Job";
            this.btnAddJob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnAddJob.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // lblItemListTitle
            // 
            this.lblItemListTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblItemListTitle.AutoSize = true;
            this.lblItemListTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblItemListTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.lblItemListTitle.Location = new System.Drawing.Point(38, 11);
            this.lblItemListTitle.Margin = new System.Windows.Forms.Padding(2, 7, 2, 7);
            this.lblItemListTitle.Name = "lblItemListTitle";
            this.lblItemListTitle.Size = new System.Drawing.Size(150, 28);
            this.lblItemListTitle.TabIndex = 182;
            this.lblItemListTitle.Text = "Active Job List";
            // 
            // dgvActiveJobList
            // 
            this.dgvActiveJobList.AllowDrop = true;
            this.dgvActiveJobList.AllowUserToAddRows = false;
            this.dgvActiveJobList.AllowUserToDeleteRows = false;
            this.dgvActiveJobList.AllowUserToOrderColumns = true;
            this.dgvActiveJobList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvActiveJobList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvActiveJobList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvActiveJobList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvActiveJobList.ColumnHeadersHeight = 50;
            this.dgvActiveJobList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvActiveJobList.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvActiveJobList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActiveJobList.GridColor = System.Drawing.Color.Silver;
            this.dgvActiveJobList.Location = new System.Drawing.Point(8, 56);
            this.dgvActiveJobList.Margin = new System.Windows.Forms.Padding(8, 0, 8, 7);
            this.dgvActiveJobList.MultiSelect = false;
            this.dgvActiveJobList.Name = "dgvActiveJobList";
            this.dgvActiveJobList.ReadOnly = true;
            this.dgvActiveJobList.RowHeadersVisible = false;
            this.dgvActiveJobList.RowHeadersWidth = 51;
            this.dgvActiveJobList.RowTemplate.Height = 60;
            this.dgvActiveJobList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActiveJobList.Size = new System.Drawing.Size(431, 580);
            this.dgvActiveJobList.TabIndex = 262;
            this.dgvActiveJobList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvActiveJobList_CellMouseDown);
            this.dgvActiveJobList.SelectionChanged += new System.EventHandler(this.dgvActiveJobList_SelectionChanged);
            // 
            // tlpMainPanel
            // 
            this.tlpMainPanel.BackColor = System.Drawing.Color.Transparent;
            this.tlpMainPanel.ColumnCount = 3;
            this.tlpMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpMainPanel.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tlpMainPanel.Controls.Add(this.tableLayoutPanel27, 2, 0);
            this.tlpMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainPanel.Location = new System.Drawing.Point(30, 30);
            this.tlpMainPanel.Margin = new System.Windows.Forms.Padding(30);
            this.tlpMainPanel.Name = "tlpMainPanel";
            this.tlpMainPanel.RowCount = 1;
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainPanel.Size = new System.Drawing.Size(1288, 643);
            this.tlpMainPanel.TabIndex = 260;
            this.tlpMainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpMainPanel_Paint);
            this.tlpMainPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tlpMainPanel_MouseClick);
            // 
            // tableLayoutPanel27
            // 
            this.tableLayoutPanel27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.tableLayoutPanel27.ColumnCount = 1;
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel27.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel27.Controls.Add(this.tableLayoutPanel28, 0, 0);
            this.tableLayoutPanel27.Controls.Add(this.tlpButton, 0, 3);
            this.tableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel27.Location = new System.Drawing.Point(457, 0);
            this.tableLayoutPanel27.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel27.Name = "tableLayoutPanel27";
            this.tableLayoutPanel27.RowCount = 4;
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel27.Size = new System.Drawing.Size(831, 643);
            this.tableLayoutPanel27.TabIndex = 262;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.ColumnCount = 16;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.Controls.Add(this.label8, 8, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtEfficiency, 8, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtActualRejectQty, 12, 2);
            this.tableLayoutPanel3.Controls.Add(this.label7, 12, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtRejectRate, 14, 2);
            this.tableLayoutPanel3.Controls.Add(this.label6, 14, 1);
            this.tableLayoutPanel3.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtTotalMaxOutputQty, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label1, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtQtyStockedIn, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.label4, 6, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtPendingStockIn, 6, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtRejectedQty, 10, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 10, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 463);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(831, 80);
            this.tableLayoutPanel3.TabIndex = 322;
            this.tableLayoutPanel3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanel3_MouseClick);
            // 
            // txtActualRejectQty
            // 
            this.txtActualRejectQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtActualRejectQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(159)))));
            this.txtActualRejectQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtActualRejectQty.Location = new System.Drawing.Point(644, 37);
            this.txtActualRejectQty.Name = "txtActualRejectQty";
            this.txtActualRejectQty.Size = new System.Drawing.Size(94, 30);
            this.txtActualRejectQty.TabIndex = 318;
            this.txtActualRejectQty.Text = "0";
            this.txtActualRejectQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtActualRejectQty.TextChanged += new System.EventHandler(this.txtActualRejectQty_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label7.Location = new System.Drawing.Point(644, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 17);
            this.label7.TabIndex = 319;
            this.label7.Text = "Actual Rejects";
            // 
            // txtRejectRate
            // 
            this.txtRejectRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRejectRate.BackColor = System.Drawing.Color.White;
            this.txtRejectRate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRejectRate.Location = new System.Drawing.Point(754, 37);
            this.txtRejectRate.Name = "txtRejectRate";
            this.txtRejectRate.Size = new System.Drawing.Size(64, 30);
            this.txtRejectRate.TabIndex = 320;
            this.txtRejectRate.Text = "0";
            this.txtRejectRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label6.Location = new System.Drawing.Point(754, 16);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 17);
            this.label6.TabIndex = 321;
            this.label6.Text = "Reject %";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Italic);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label2.Location = new System.Drawing.Point(129, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 312;
            this.label2.Text = "Max Qty";
            // 
            // txtTotalMaxOutputQty
            // 
            this.txtTotalMaxOutputQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalMaxOutputQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtTotalMaxOutputQty.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.txtTotalMaxOutputQty.Location = new System.Drawing.Point(129, 37);
            this.txtTotalMaxOutputQty.Name = "txtTotalMaxOutputQty";
            this.txtTotalMaxOutputQty.Size = new System.Drawing.Size(94, 23);
            this.txtTotalMaxOutputQty.TabIndex = 264;
            this.txtTotalMaxOutputQty.Text = "0";
            this.txtTotalMaxOutputQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalMaxOutputQty.TextChanged += new System.EventHandler(this.txtTotalMaxOutputQty_TextChanged_1);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label1.Location = new System.Drawing.Point(239, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 314;
            this.label1.Text = "Stocked Qty";
            // 
            // txtQtyStockedIn
            // 
            this.txtQtyStockedIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQtyStockedIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(241)))), ((int)(((byte)(218)))));
            this.txtQtyStockedIn.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtQtyStockedIn.Location = new System.Drawing.Point(239, 37);
            this.txtQtyStockedIn.Name = "txtQtyStockedIn";
            this.txtQtyStockedIn.Size = new System.Drawing.Size(94, 30);
            this.txtQtyStockedIn.TabIndex = 313;
            this.txtQtyStockedIn.Text = "0";
            this.txtQtyStockedIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQtyStockedIn.TextChanged += new System.EventHandler(this.txtQtyStockedIn_TextChanged_1);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label4.Location = new System.Drawing.Point(349, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 316;
            this.label4.Text = "At Machine";
            // 
            // txtPendingStockIn
            // 
            this.txtPendingStockIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPendingStockIn.BackColor = System.Drawing.Color.White;
            this.txtPendingStockIn.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPendingStockIn.Location = new System.Drawing.Point(349, 37);
            this.txtPendingStockIn.Name = "txtPendingStockIn";
            this.txtPendingStockIn.Size = new System.Drawing.Size(94, 30);
            this.txtPendingStockIn.TabIndex = 315;
            this.txtPendingStockIn.Text = "0";
            this.txtPendingStockIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRejectedQty
            // 
            this.txtRejectedQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRejectedQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtRejectedQty.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.txtRejectedQty.Location = new System.Drawing.Point(539, 37);
            this.txtRejectedQty.Name = "txtRejectedQty";
            this.txtRejectedQty.Size = new System.Drawing.Size(89, 23);
            this.txtRejectedQty.TabIndex = 317;
            this.txtRejectedQty.Text = "0";
            this.txtRejectedQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRejectedQty.TextChanged += new System.EventHandler(this.txtRejectedQty_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Italic);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label5.Location = new System.Drawing.Point(539, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 318;
            this.label5.Text = "System Rejects";
            // 
            // tableLayoutPanel28
            // 
            this.tableLayoutPanel28.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel28.ColumnCount = 1;
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel28.Controls.Add(this.dgvRecordHistory, 0, 2);
            this.tableLayoutPanel28.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel28.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel28.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel28.Name = "tableLayoutPanel28";
            this.tableLayoutPanel28.RowCount = 3;
            this.tableLayoutPanel28.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel28.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel28.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel28.Size = new System.Drawing.Size(831, 463);
            this.tableLayoutPanel28.TabIndex = 263;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.Controls.Add(this.label81, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(831, 46);
            this.tableLayoutPanel1.TabIndex = 263;
            // 
            // label81
            // 
            this.label81.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label81.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label81.Location = new System.Drawing.Point(8, 14);
            this.label81.Margin = new System.Windows.Forms.Padding(8, 7, 8, 9);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(16, 23);
            this.label81.TabIndex = 262;
            this.label81.Text = "●";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.label3.Location = new System.Drawing.Point(34, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 7, 2, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 28);
            this.label3.TabIndex = 182;
            this.label3.Text = "Job Sheet Record";
            // 
            // tlpButton
            // 
            this.tlpButton.BackColor = System.Drawing.Color.White;
            this.tlpButton.ColumnCount = 8;
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 193F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlpButton.Controls.Add(this.lblFinalDataReviewString, 3, 1);
            this.tlpButton.Controls.Add(this.btnEditJobSheet, 4, 1);
            this.tlpButton.Controls.Add(this.btnAddJobSheet, 6, 1);
            this.tlpButton.Controls.Add(this.lblFinalDataReview, 1, 1);
            this.tlpButton.Controls.Add(this.cbFinalDataReview, 2, 1);
            this.tlpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButton.Location = new System.Drawing.Point(0, 563);
            this.tlpButton.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButton.Name = "tlpButton";
            this.tlpButton.RowCount = 3;
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpButton.Size = new System.Drawing.Size(831, 80);
            this.tlpButton.TabIndex = 262;
            this.tlpButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tlpButton_MouseClick);
            // 
            // lblFinalDataReviewString
            // 
            this.lblFinalDataReviewString.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFinalDataReviewString.AutoSize = true;
            this.lblFinalDataReviewString.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFinalDataReviewString.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblFinalDataReviewString.Location = new System.Drawing.Point(237, 31);
            this.lblFinalDataReviewString.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.lblFinalDataReviewString.Name = "lblFinalDataReviewString";
            this.lblFinalDataReviewString.Size = new System.Drawing.Size(15, 19);
            this.lblFinalDataReviewString.TabIndex = 324;
            this.lblFinalDataReviewString.Text = "-";
            // 
            // btnEditJobSheet
            // 
            this.btnEditJobSheet.AnimationHoverSpeed = 0.07F;
            this.btnEditJobSheet.AnimationSpeed = 0.03F;
            this.btnEditJobSheet.BackColor = System.Drawing.Color.Transparent;
            this.btnEditJobSheet.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(234)))), ((int)(((byte)(147)))));
            this.btnEditJobSheet.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(255)))), ((int)(((byte)(147)))));
            this.btnEditJobSheet.BorderColor = System.Drawing.Color.Black;
            this.btnEditJobSheet.BorderSize = 1;
            this.btnEditJobSheet.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEditJobSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditJobSheet.FocusedColor = System.Drawing.Color.Empty;
            this.btnEditJobSheet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEditJobSheet.ForeColor = System.Drawing.Color.Black;
            this.btnEditJobSheet.Image = null;
            this.btnEditJobSheet.ImageSize = new System.Drawing.Size(20, 20);
            this.btnEditJobSheet.Location = new System.Drawing.Point(463, 20);
            this.btnEditJobSheet.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditJobSheet.Name = "btnEditJobSheet";
            this.btnEditJobSheet.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnEditJobSheet.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnEditJobSheet.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnEditJobSheet.OnHoverForeColor = System.Drawing.Color.White;
            this.btnEditJobSheet.OnHoverImage = null;
            this.btnEditJobSheet.OnPressedColor = System.Drawing.Color.Black;
            this.btnEditJobSheet.Radius = 2;
            this.btnEditJobSheet.Size = new System.Drawing.Size(193, 40);
            this.btnEditJobSheet.TabIndex = 224;
            this.btnEditJobSheet.Text = "View/Edit Job Sheet";
            this.btnEditJobSheet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnEditJobSheet.Click += new System.EventHandler(this.btnEditJobSheet_Click);
            // 
            // btnAddJobSheet
            // 
            this.btnAddJobSheet.AnimationHoverSpeed = 0.07F;
            this.btnAddJobSheet.AnimationSpeed = 0.03F;
            this.btnAddJobSheet.BackColor = System.Drawing.Color.Transparent;
            this.btnAddJobSheet.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(107)))), ((int)(((byte)(234)))));
            this.btnAddJobSheet.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            this.btnAddJobSheet.BorderColor = System.Drawing.Color.Black;
            this.btnAddJobSheet.BorderSize = 1;
            this.btnAddJobSheet.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAddJobSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddJobSheet.FocusedColor = System.Drawing.Color.Empty;
            this.btnAddJobSheet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddJobSheet.ForeColor = System.Drawing.Color.White;
            this.btnAddJobSheet.Image = null;
            this.btnAddJobSheet.ImageSize = new System.Drawing.Size(20, 20);
            this.btnAddJobSheet.Location = new System.Drawing.Point(671, 20);
            this.btnAddJobSheet.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddJobSheet.Name = "btnAddJobSheet";
            this.btnAddJobSheet.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnAddJobSheet.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnAddJobSheet.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnAddJobSheet.OnHoverForeColor = System.Drawing.Color.White;
            this.btnAddJobSheet.OnHoverImage = null;
            this.btnAddJobSheet.OnPressedColor = System.Drawing.Color.Black;
            this.btnAddJobSheet.Radius = 2;
            this.btnAddJobSheet.Size = new System.Drawing.Size(150, 40);
            this.btnAddJobSheet.TabIndex = 222;
            this.btnAddJobSheet.Text = "Add Job Sheet";
            this.btnAddJobSheet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnAddJobSheet.Click += new System.EventHandler(this.btnAddJobSheet_Click);
            // 
            // lblFinalDataReview
            // 
            this.lblFinalDataReview.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFinalDataReview.AutoSize = true;
            this.lblFinalDataReview.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFinalDataReview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblFinalDataReview.Location = new System.Drawing.Point(19, 30);
            this.lblFinalDataReview.Margin = new System.Windows.Forms.Padding(3);
            this.lblFinalDataReview.Name = "lblFinalDataReview";
            this.lblFinalDataReview.Size = new System.Drawing.Size(123, 19);
            this.lblFinalDataReview.TabIndex = 323;
            this.lblFinalDataReview.Text = "Final Data Review: ";
            // 
            // cbFinalDataReview
            // 
            this.cbFinalDataReview.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbFinalDataReview.AutoSize = true;
            this.cbFinalDataReview.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbFinalDataReview.Location = new System.Drawing.Point(148, 28);
            this.cbFinalDataReview.Name = "cbFinalDataReview";
            this.cbFinalDataReview.Size = new System.Drawing.Size(83, 23);
            this.cbFinalDataReview.TabIndex = 319;
            this.cbFinalDataReview.Text = "Checked";
            this.cbFinalDataReview.UseVisualStyleBackColor = true;
            this.cbFinalDataReview.CheckedChanged += new System.EventHandler(this.cbFinalDataReview_CheckedChanged);
            // 
            // errorProvider10
            // 
            this.errorProvider10.ContainerControl = this;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.AutoScroll = true;
            this.tableLayoutPanel15.ColumnCount = 1;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Controls.Add(this.tlpMainPanel, 0, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(1348, 703);
            this.tableLayoutPanel15.TabIndex = 261;
            this.tableLayoutPanel15.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanel15_MouseClick);
            // 
            // errorProvider11
            // 
            this.errorProvider11.ContainerControl = this;
            // 
            // txtEfficiency
            // 
            this.txtEfficiency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEfficiency.BackColor = System.Drawing.Color.White;
            this.txtEfficiency.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEfficiency.Location = new System.Drawing.Point(459, 37);
            this.txtEfficiency.Name = "txtEfficiency";
            this.txtEfficiency.Size = new System.Drawing.Size(64, 30);
            this.txtEfficiency.TabIndex = 321;
            this.txtEfficiency.Text = "0";
            this.txtEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label8.Location = new System.Drawing.Point(459, 16);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 17);
            this.label8.TabIndex = 322;
            this.label8.Text = "Effi. %";
            // 
            // frmProductionRecordVer3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1348, 703);
            this.Controls.Add(this.tableLayoutPanel15);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmProductionRecordVer3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily Production Record";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProductionRecord_FormClosed);
            this.Load += new System.EventHandler(this.frmProductionRecord_Load);
            this.Shown += new System.EventHandler(this.frmProductionRecordVer3_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecordHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider9)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tlpItemSelectionTitle.ResumeLayout(false);
            this.tlpItemSelectionTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveJobList)).EndInit();
            this.tlpMainPanel.ResumeLayout(false);
            this.tableLayoutPanel27.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel28.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpButton.ResumeLayout(false);
            this.tlpButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider10)).EndInit();
            this.tableLayoutPanel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvRecordHistory;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;
        private System.Windows.Forms.ErrorProvider errorProvider6;
        private System.Windows.Forms.ErrorProvider errorProvider7;
        private System.Windows.Forms.ErrorProvider errorProvider8;
        private System.Windows.Forms.ErrorProvider errorProvider9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tlpMainPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ErrorProvider errorProvider10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel27;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel28;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.ErrorProvider errorProvider11;
        private System.Windows.Forms.DataGridView dgvActiveJobList;
        private Guna.UI.WinForms.GunaGradientButton btnAddJob;
        private System.Windows.Forms.TableLayoutPanel tlpItemSelectionTitle;
        private System.Windows.Forms.Label lblItemListTitle;
        private System.Windows.Forms.TableLayoutPanel tlpButton;
        private Guna.UI.WinForms.GunaGradientButton btnEditJobSheet;
        private Guna.UI.WinForms.GunaGradientButton btnAddJobSheet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.TextBox txtTotalMaxOutputQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRejectedQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPendingStockIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQtyStockedIn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRejectRate;
        private System.Windows.Forms.CheckBox cbFinalDataReview;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblFinalDataReview;
        private System.Windows.Forms.Label lblFinalDataReviewString;
        private System.Windows.Forms.TextBox txtActualRejectQty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEfficiency;
    }
}