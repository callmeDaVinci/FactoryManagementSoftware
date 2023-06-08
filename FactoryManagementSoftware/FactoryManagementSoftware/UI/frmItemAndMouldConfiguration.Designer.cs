namespace FactoryManagementSoftware.UI
{
    partial class frmItemAndMouldConfiguration
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
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvSuggestionList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel25 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvMouldList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tlpButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new Guna.UI.WinForms.GunaGradientButton();
            this.btnCancel = new Guna.UI.WinForms.GunaGradientButton();
            this.tlpLeftRight = new System.Windows.Forms.TableLayoutPanel();
            this.btnMoveRight = new Guna.UI.WinForms.GunaGradientButton();
            this.btnMoveLeft = new Guna.UI.WinForms.GunaGradientButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel10.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuggestionList)).BeginInit();
            this.tableLayoutPanel25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMouldList)).BeginInit();
            this.tlpButton.SuspendLayout();
            this.tlpLeftRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.tlpMain, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(1094, 607);
            this.tableLayoutPanel10.TabIndex = 114;
            this.tableLayoutPanel10.Click += new System.EventHandler(this.tableLayoutPanel10_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpMain.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpMain.Controls.Add(this.tableLayoutPanel25, 2, 0);
            this.tlpMain.Controls.Add(this.tlpLeftRight, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(20, 20);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(20);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1054, 567);
            this.tlpMain.TabIndex = 111;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvSuggestionList, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(403, 561);
            this.tableLayoutPanel1.TabIndex = 183;
            this.tableLayoutPanel1.Click += new System.EventHandler(this.tableLayoutPanel1_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7.2F);
            this.label5.Location = new System.Drawing.Point(0, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 183;
            this.label5.Text = "Suggestion List";
            // 
            // dgvSuggestionList
            // 
            this.dgvSuggestionList.AllowUserToAddRows = false;
            this.dgvSuggestionList.AllowUserToDeleteRows = false;
            this.dgvSuggestionList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvSuggestionList.BackgroundColor = System.Drawing.Color.DimGray;
            this.dgvSuggestionList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSuggestionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuggestionList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvSuggestionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSuggestionList.GridColor = System.Drawing.SystemColors.Control;
            this.dgvSuggestionList.Location = new System.Drawing.Point(2, 32);
            this.dgvSuggestionList.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSuggestionList.Name = "dgvSuggestionList";
            this.dgvSuggestionList.ReadOnly = true;
            this.dgvSuggestionList.RowHeadersVisible = false;
            this.dgvSuggestionList.RowHeadersWidth = 51;
            this.dgvSuggestionList.RowTemplate.Height = 40;
            this.dgvSuggestionList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuggestionList.Size = new System.Drawing.Size(399, 527);
            this.dgvSuggestionList.TabIndex = 183;
            this.dgvSuggestionList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSuggestionList_CellMouseClick);
            this.dgvSuggestionList.SelectionChanged += new System.EventHandler(this.dgvSuggestionList_SelectionChanged);
            this.dgvSuggestionList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvSuggestionList_MouseClick);
            // 
            // tableLayoutPanel25
            // 
            this.tableLayoutPanel25.ColumnCount = 1;
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel25.Controls.Add(this.dgvMouldList, 0, 1);
            this.tableLayoutPanel25.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel25.Controls.Add(this.tlpButton, 0, 2);
            this.tableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel25.Location = new System.Drawing.Point(442, 3);
            this.tableLayoutPanel25.Name = "tableLayoutPanel25";
            this.tableLayoutPanel25.RowCount = 3;
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel25.Size = new System.Drawing.Size(609, 561);
            this.tableLayoutPanel25.TabIndex = 182;
            this.tableLayoutPanel25.Click += new System.EventHandler(this.tableLayoutPanel25_Click);
            // 
            // dgvMouldList
            // 
            this.dgvMouldList.AllowUserToAddRows = false;
            this.dgvMouldList.AllowUserToDeleteRows = false;
            this.dgvMouldList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMouldList.BackgroundColor = System.Drawing.Color.DimGray;
            this.dgvMouldList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMouldList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMouldList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMouldList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMouldList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMouldList.GridColor = System.Drawing.Color.DimGray;
            this.dgvMouldList.Location = new System.Drawing.Point(2, 32);
            this.dgvMouldList.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMouldList.Name = "dgvMouldList";
            this.dgvMouldList.ReadOnly = true;
            this.dgvMouldList.RowHeadersVisible = false;
            this.dgvMouldList.RowHeadersWidth = 51;
            this.dgvMouldList.RowTemplate.Height = 50;
            this.dgvMouldList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMouldList.Size = new System.Drawing.Size(605, 477);
            this.dgvMouldList.TabIndex = 184;
            this.dgvMouldList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMouldList_CellClick);
            this.dgvMouldList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMouldList_CellFormatting);
            this.dgvMouldList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMouldList_CellMouseDown);
            this.dgvMouldList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMouldList_CellValueChanged);
            this.dgvMouldList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMouldList_DataBindingComplete);
            this.dgvMouldList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvMouldList_EditingControlShowing);
            this.dgvMouldList.SelectionChanged += new System.EventHandler(this.dgvMouldList_SelectionChanged);
            this.dgvMouldList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvMouldList_MouseClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.2F);
            this.label1.Location = new System.Drawing.Point(0, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 184;
            this.label1.Text = "Mould List";
            // 
            // tlpButton
            // 
            this.tlpButton.ColumnCount = 4;
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpButton.Controls.Add(this.btnSave, 3, 0);
            this.tlpButton.Controls.Add(this.btnCancel, 1, 0);
            this.tlpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButton.Location = new System.Drawing.Point(0, 511);
            this.tlpButton.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButton.Name = "tlpButton";
            this.tlpButton.RowCount = 1;
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.Size = new System.Drawing.Size(609, 50);
            this.tlpButton.TabIndex = 183;
            this.tlpButton.Click += new System.EventHandler(this.tlpButton_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AnimationHoverSpeed = 0.07F;
            this.btnSave.AnimationSpeed = 0.03F;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSave.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(160)))), ((int)(((byte)(209)))));
            this.btnSave.BorderColor = System.Drawing.Color.Black;
            this.btnSave.BorderSize = 1;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.FocusedColor = System.Drawing.Color.Empty;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.btnSave.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSave.Image = null;
            this.btnSave.ImageSize = new System.Drawing.Size(20, 20);
            this.btnSave.Location = new System.Drawing.Point(509, 20);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnSave.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnSave.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnSave.OnHoverForeColor = System.Drawing.Color.White;
            this.btnSave.OnHoverImage = null;
            this.btnSave.OnPressedColor = System.Drawing.Color.Black;
            this.btnSave.Radius = 2;
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 180;
            this.btnSave.Text = "SAVE";
            this.btnSave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AnimationHoverSpeed = 0.07F;
            this.btnCancel.AnimationSpeed = 0.03F;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BaseColor1 = System.Drawing.Color.White;
            this.btnCancel.BaseColor2 = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.FocusedColor = System.Drawing.Color.Empty;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Image = null;
            this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnCancel.Location = new System.Drawing.Point(404, 20);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnCancel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnCancel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnCancel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnCancel.OnHoverImage = null;
            this.btnCancel.OnPressedColor = System.Drawing.Color.Black;
            this.btnCancel.Radius = 2;
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 182;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tlpLeftRight
            // 
            this.tlpLeftRight.ColumnCount = 1;
            this.tlpLeftRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeftRight.Controls.Add(this.btnMoveRight, 0, 1);
            this.tlpLeftRight.Controls.Add(this.btnMoveLeft, 0, 3);
            this.tlpLeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLeftRight.Location = new System.Drawing.Point(409, 0);
            this.tlpLeftRight.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLeftRight.Name = "tlpLeftRight";
            this.tlpLeftRight.RowCount = 5;
            this.tlpLeftRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLeftRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpLeftRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpLeftRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpLeftRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLeftRight.Size = new System.Drawing.Size(30, 567);
            this.tlpLeftRight.TabIndex = 184;
            this.tlpLeftRight.Click += new System.EventHandler(this.tableLayoutPanel2_Click);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.AnimationHoverSpeed = 0.07F;
            this.btnMoveRight.AnimationSpeed = 0.03F;
            this.btnMoveRight.BackColor = System.Drawing.Color.Transparent;
            this.btnMoveRight.BaseColor1 = System.Drawing.Color.White;
            this.btnMoveRight.BaseColor2 = System.Drawing.Color.White;
            this.btnMoveRight.BorderColor = System.Drawing.Color.Black;
            this.btnMoveRight.BorderSize = 1;
            this.btnMoveRight.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMoveRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMoveRight.FocusedColor = System.Drawing.Color.Empty;
            this.btnMoveRight.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.btnMoveRight.ForeColor = System.Drawing.Color.Black;
            this.btnMoveRight.Image = global::FactoryManagementSoftware.Properties.Resources.icons8_more_than_25;
            this.btnMoveRight.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnMoveRight.ImageSize = new System.Drawing.Size(20, 20);
            this.btnMoveRight.Location = new System.Drawing.Point(2, 251);
            this.btnMoveRight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnMoveRight.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnMoveRight.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnMoveRight.OnHoverForeColor = System.Drawing.Color.White;
            this.btnMoveRight.OnHoverImage = null;
            this.btnMoveRight.OnPressedColor = System.Drawing.Color.Black;
            this.btnMoveRight.Radius = 2;
            this.btnMoveRight.Size = new System.Drawing.Size(26, 25);
            this.btnMoveRight.TabIndex = 183;
            this.btnMoveRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.AnimationHoverSpeed = 0.07F;
            this.btnMoveLeft.AnimationSpeed = 0.03F;
            this.btnMoveLeft.BackColor = System.Drawing.Color.Transparent;
            this.btnMoveLeft.BaseColor1 = System.Drawing.Color.White;
            this.btnMoveLeft.BaseColor2 = System.Drawing.Color.White;
            this.btnMoveLeft.BorderColor = System.Drawing.Color.Black;
            this.btnMoveLeft.BorderSize = 1;
            this.btnMoveLeft.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMoveLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMoveLeft.FocusedColor = System.Drawing.Color.Empty;
            this.btnMoveLeft.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.btnMoveLeft.ForeColor = System.Drawing.Color.Black;
            this.btnMoveLeft.Image = global::FactoryManagementSoftware.Properties.Resources.icons8_less_than_25;
            this.btnMoveLeft.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnMoveLeft.ImageSize = new System.Drawing.Size(20, 20);
            this.btnMoveLeft.Location = new System.Drawing.Point(2, 291);
            this.btnMoveLeft.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnMoveLeft.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnMoveLeft.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnMoveLeft.OnHoverForeColor = System.Drawing.Color.White;
            this.btnMoveLeft.OnHoverImage = null;
            this.btnMoveLeft.OnPressedColor = System.Drawing.Color.Black;
            this.btnMoveLeft.Radius = 2;
            this.btnMoveLeft.Size = new System.Drawing.Size(26, 25);
            this.btnMoveLeft.TabIndex = 184;
            this.btnMoveLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            this.btnMoveLeft.MouseEnter += new System.EventHandler(this.btnMoveLeft_MouseEnter);
            this.btnMoveLeft.MouseLeave += new System.EventHandler(this.btnMoveLeft_MouseLeave);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // frmItemAndMouldConfiguration
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1094, 607);
            this.Controls.Add(this.tableLayoutPanel10);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.Name = "frmItemAndMouldConfiguration";
            this.Text = "Item-Mould Configuration";
            this.Load += new System.EventHandler(this.frmItemAndMouldConfiguration_Load);
            this.Click += new System.EventHandler(this.frmItemAndMouldConfiguration_Click);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuggestionList)).EndInit();
            this.tableLayoutPanel25.ResumeLayout(false);
            this.tableLayoutPanel25.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMouldList)).EndInit();
            this.tlpButton.ResumeLayout(false);
            this.tlpLeftRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvSuggestionList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel25;
        private System.Windows.Forms.TableLayoutPanel tlpButton;
        private Guna.UI.WinForms.GunaGradientButton btnSave;
        private Guna.UI.WinForms.GunaGradientButton btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMouldList;
        private System.Windows.Forms.TableLayoutPanel tlpLeftRight;
        private Guna.UI.WinForms.GunaGradientButton btnMoveRight;
        private Guna.UI.WinForms.GunaGradientButton btnMoveLeft;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}