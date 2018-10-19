namespace FactoryManagementSoftware.UI
{
    partial class frmInOut
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
            this.dgvTrf = new System.Windows.Forms.DataGridView();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.lblInOutItemName = new System.Windows.Forms.Label();
            this.lblInOutTransferDate = new System.Windows.Forms.Label();
            this.dtpTrfDate = new System.Windows.Forms.DateTimePicker();
            this.cmbTrfItemName = new System.Windows.Forms.ComboBox();
            this.cmbTrfItemCode = new System.Windows.Forms.ComboBox();
            this.lblInOutItemCode = new System.Windows.Forms.Label();
            this.cmbTrfFromCategory = new System.Windows.Forms.ComboBox();
            this.lblInOutFrom = new System.Windows.Forms.Label();
            this.cmbTrfFrom = new System.Windows.Forms.ComboBox();
            this.cmbTrfTo = new System.Windows.Forms.ComboBox();
            this.cmbTrfToCategory = new System.Windows.Forms.ComboBox();
            this.lblInOutTo = new System.Windows.Forms.Label();
            this.cmbTrfQtyUnit = new System.Windows.Forms.ComboBox();
            this.lblInOutQuantity = new System.Windows.Forms.Label();
            this.txtTrfQty = new System.Windows.Forms.TextBox();
            this.txtTrfNote = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvFactoryStock = new System.Windows.Forms.DataGridView();
            this.dgvItem = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvcItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcOrd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Added_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Trf_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.From = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.To = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrfQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Added_By = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactoryStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTrf
            // 
            this.dgvTrf.AllowUserToAddRows = false;
            this.dgvTrf.AllowUserToDeleteRows = false;
            this.dgvTrf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrf.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Added_Date,
            this.Trf_Date,
            this.ItemCode,
            this.ItemName,
            this.From,
            this.To,
            this.TrfQty,
            this.Unit,
            this.Added_By});
            this.dgvTrf.Location = new System.Drawing.Point(631, 406);
            this.dgvTrf.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTrf.Name = "dgvTrf";
            this.dgvTrf.ReadOnly = true;
            this.dgvTrf.RowTemplate.Height = 24;
            this.dgvTrf.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrf.Size = new System.Drawing.Size(916, 237);
            this.dgvTrf.TabIndex = 37;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransfer.Location = new System.Drawing.Point(390, 595);
            this.btnTransfer.Margin = new System.Windows.Forms.Padding(2);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(168, 48);
            this.btnTransfer.TabIndex = 36;
            this.btnTransfer.Text = "TRANSFER";
            this.btnTransfer.UseVisualStyleBackColor = true;
            // 
            // lblInOutItemName
            // 
            this.lblInOutItemName.AutoSize = true;
            this.lblInOutItemName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutItemName.Location = new System.Drawing.Point(41, 120);
            this.lblInOutItemName.Name = "lblInOutItemName";
            this.lblInOutItemName.Size = new System.Drawing.Size(144, 32);
            this.lblInOutItemName.TabIndex = 32;
            this.lblInOutItemName.Text = "*Item Name";
            // 
            // lblInOutTransferDate
            // 
            this.lblInOutTransferDate.AutoSize = true;
            this.lblInOutTransferDate.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutTransferDate.Location = new System.Drawing.Point(19, 59);
            this.lblInOutTransferDate.Name = "lblInOutTransferDate";
            this.lblInOutTransferDate.Size = new System.Drawing.Size(166, 32);
            this.lblInOutTransferDate.TabIndex = 30;
            this.lblInOutTransferDate.Text = "*Transfer Date";
            // 
            // dtpTrfDate
            // 
            this.dtpTrfDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTrfDate.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTrfDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTrfDate.Location = new System.Drawing.Point(191, 59);
            this.dtpTrfDate.Name = "dtpTrfDate";
            this.dtpTrfDate.Size = new System.Drawing.Size(367, 38);
            this.dtpTrfDate.TabIndex = 40;
            // 
            // cmbTrfItemName
            // 
            this.cmbTrfItemName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfItemName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfItemName.FormattingEnabled = true;
            this.cmbTrfItemName.Location = new System.Drawing.Point(191, 120);
            this.cmbTrfItemName.Name = "cmbTrfItemName";
            this.cmbTrfItemName.Size = new System.Drawing.Size(367, 39);
            this.cmbTrfItemName.TabIndex = 41;
            // 
            // cmbTrfItemCode
            // 
            this.cmbTrfItemCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfItemCode.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfItemCode.FormattingEnabled = true;
            this.cmbTrfItemCode.Location = new System.Drawing.Point(191, 181);
            this.cmbTrfItemCode.Name = "cmbTrfItemCode";
            this.cmbTrfItemCode.Size = new System.Drawing.Size(367, 39);
            this.cmbTrfItemCode.TabIndex = 43;
            // 
            // lblInOutItemCode
            // 
            this.lblInOutItemCode.AutoSize = true;
            this.lblInOutItemCode.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutItemCode.Location = new System.Drawing.Point(49, 181);
            this.lblInOutItemCode.Name = "lblInOutItemCode";
            this.lblInOutItemCode.Size = new System.Drawing.Size(136, 32);
            this.lblInOutItemCode.TabIndex = 42;
            this.lblInOutItemCode.Text = "*Item Code";
            // 
            // cmbTrfFromCategory
            // 
            this.cmbTrfFromCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfFromCategory.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfFromCategory.FormattingEnabled = true;
            this.cmbTrfFromCategory.Location = new System.Drawing.Point(191, 242);
            this.cmbTrfFromCategory.Name = "cmbTrfFromCategory";
            this.cmbTrfFromCategory.Size = new System.Drawing.Size(168, 39);
            this.cmbTrfFromCategory.TabIndex = 45;
            // 
            // lblInOutFrom
            // 
            this.lblInOutFrom.AutoSize = true;
            this.lblInOutFrom.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutFrom.Location = new System.Drawing.Point(105, 242);
            this.lblInOutFrom.Name = "lblInOutFrom";
            this.lblInOutFrom.Size = new System.Drawing.Size(80, 32);
            this.lblInOutFrom.TabIndex = 44;
            this.lblInOutFrom.Text = "*From";
            // 
            // cmbTrfFrom
            // 
            this.cmbTrfFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfFrom.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfFrom.FormattingEnabled = true;
            this.cmbTrfFrom.Location = new System.Drawing.Point(390, 242);
            this.cmbTrfFrom.Name = "cmbTrfFrom";
            this.cmbTrfFrom.Size = new System.Drawing.Size(168, 39);
            this.cmbTrfFrom.TabIndex = 46;
            // 
            // cmbTrfTo
            // 
            this.cmbTrfTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfTo.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfTo.FormattingEnabled = true;
            this.cmbTrfTo.Location = new System.Drawing.Point(390, 306);
            this.cmbTrfTo.Name = "cmbTrfTo";
            this.cmbTrfTo.Size = new System.Drawing.Size(168, 39);
            this.cmbTrfTo.TabIndex = 49;
            // 
            // cmbTrfToCategory
            // 
            this.cmbTrfToCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfToCategory.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfToCategory.FormattingEnabled = true;
            this.cmbTrfToCategory.Location = new System.Drawing.Point(191, 306);
            this.cmbTrfToCategory.Name = "cmbTrfToCategory";
            this.cmbTrfToCategory.Size = new System.Drawing.Size(168, 39);
            this.cmbTrfToCategory.TabIndex = 48;
            // 
            // lblInOutTo
            // 
            this.lblInOutTo.AutoSize = true;
            this.lblInOutTo.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutTo.Location = new System.Drawing.Point(135, 306);
            this.lblInOutTo.Name = "lblInOutTo";
            this.lblInOutTo.Size = new System.Drawing.Size(50, 32);
            this.lblInOutTo.TabIndex = 47;
            this.lblInOutTo.Text = "*To";
            // 
            // cmbTrfQtyUnit
            // 
            this.cmbTrfQtyUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfQtyUnit.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfQtyUnit.FormattingEnabled = true;
            this.cmbTrfQtyUnit.Items.AddRange(new object[] {
            "kg",
            "g",
            "set",
            "box",
            "piece"});
            this.cmbTrfQtyUnit.Location = new System.Drawing.Point(390, 368);
            this.cmbTrfQtyUnit.Name = "cmbTrfQtyUnit";
            this.cmbTrfQtyUnit.Size = new System.Drawing.Size(168, 39);
            this.cmbTrfQtyUnit.TabIndex = 52;
            // 
            // lblInOutQuantity
            // 
            this.lblInOutQuantity.AutoSize = true;
            this.lblInOutQuantity.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutQuantity.Location = new System.Drawing.Point(68, 368);
            this.lblInOutQuantity.Name = "lblInOutQuantity";
            this.lblInOutQuantity.Size = new System.Drawing.Size(117, 32);
            this.lblInOutQuantity.TabIndex = 50;
            this.lblInOutQuantity.Text = "*Quantity";
            // 
            // txtTrfQty
            // 
            this.txtTrfQty.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrfQty.Location = new System.Drawing.Point(191, 369);
            this.txtTrfQty.Name = "txtTrfQty";
            this.txtTrfQty.Size = new System.Drawing.Size(168, 38);
            this.txtTrfQty.TabIndex = 53;
            // 
            // txtTrfNote
            // 
            this.txtTrfNote.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrfNote.Location = new System.Drawing.Point(191, 431);
            this.txtTrfNote.Multiline = true;
            this.txtTrfNote.Name = "txtTrfNote";
            this.txtTrfNote.Size = new System.Drawing.Size(367, 116);
            this.txtTrfNote.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(117, 431);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 32);
            this.label1.TabIndex = 54;
            this.label1.Text = "Note";
            // 
            // dgvFactoryStock
            // 
            this.dgvFactoryStock.AllowUserToAddRows = false;
            this.dgvFactoryStock.AllowUserToDeleteRows = false;
            this.dgvFactoryStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFactoryStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Factory,
            this.QTY});
            this.dgvFactoryStock.Location = new System.Drawing.Point(1258, 105);
            this.dgvFactoryStock.Margin = new System.Windows.Forms.Padding(2);
            this.dgvFactoryStock.Name = "dgvFactoryStock";
            this.dgvFactoryStock.ReadOnly = true;
            this.dgvFactoryStock.RowTemplate.Height = 24;
            this.dgvFactoryStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFactoryStock.Size = new System.Drawing.Size(289, 240);
            this.dgvFactoryStock.TabIndex = 56;
            // 
            // dgvItem
            // 
            this.dgvItem.AllowUserToAddRows = false;
            this.dgvItem.AllowUserToDeleteRows = false;
            this.dgvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcItemCode,
            this.dgvcItemName,
            this.dgvcQty,
            this.dgvcOrd});
            this.dgvItem.Location = new System.Drawing.Point(631, 105);
            this.dgvItem.Margin = new System.Windows.Forms.Padding(2);
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.ReadOnly = true;
            this.dgvItem.RowTemplate.Height = 24;
            this.dgvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItem.Size = new System.Drawing.Size(602, 240);
            this.dgvItem.TabIndex = 58;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(725, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(508, 38);
            this.textBox1.TabIndex = 60;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(633, 59);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(86, 32);
            this.lblSearch.TabIndex = 59;
            this.lblSearch.Text = "Search";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(1252, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 32);
            this.label2.TabIndex = 61;
            this.label2.Text = "Qty in each Factory";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(633, 368);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 32);
            this.label3.TabIndex = 62;
            this.label3.Text = "Tranfer History";
            // 
            // dgvcItemCode
            // 
            this.dgvcItemCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvcItemCode.HeaderText = "Code";
            this.dgvcItemCode.Name = "dgvcItemCode";
            this.dgvcItemCode.ReadOnly = true;
            // 
            // dgvcItemName
            // 
            this.dgvcItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvcItemName.HeaderText = "Name";
            this.dgvcItemName.Name = "dgvcItemName";
            this.dgvcItemName.ReadOnly = true;
            // 
            // dgvcQty
            // 
            this.dgvcQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvcQty.HeaderText = "Qty";
            this.dgvcQty.Name = "dgvcQty";
            this.dgvcQty.ReadOnly = true;
            this.dgvcQty.Width = 66;
            // 
            // dgvcOrd
            // 
            this.dgvcOrd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvcOrd.HeaderText = "Order";
            this.dgvcOrd.Name = "dgvcOrd";
            this.dgvcOrd.ReadOnly = true;
            this.dgvcOrd.Width = 83;
            // 
            // Factory
            // 
            this.Factory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Factory.HeaderText = "Factory";
            this.Factory.Name = "Factory";
            this.Factory.ReadOnly = true;
            // 
            // QTY
            // 
            this.QTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.QTY.HeaderText = "Qty";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 66;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 56;
            // 
            // Added_Date
            // 
            this.Added_Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Added_Date.HeaderText = "Added_Date";
            this.Added_Date.Name = "Added_Date";
            this.Added_Date.ReadOnly = true;
            this.Added_Date.Width = 132;
            // 
            // Trf_Date
            // 
            this.Trf_Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Trf_Date.HeaderText = "Trf_Date";
            this.Trf_Date.Name = "Trf_Date";
            this.Trf_Date.ReadOnly = true;
            this.Trf_Date.Width = 101;
            // 
            // ItemCode
            // 
            this.ItemCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemCode.HeaderText = "Item Code";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // From
            // 
            this.From.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.From.HeaderText = "From";
            this.From.Name = "From";
            this.From.ReadOnly = true;
            this.From.Width = 78;
            // 
            // To
            // 
            this.To.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.To.HeaderText = "To";
            this.To.Name = "To";
            this.To.ReadOnly = true;
            this.To.Width = 56;
            // 
            // TrfQty
            // 
            this.TrfQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TrfQty.HeaderText = "Qty";
            this.TrfQty.Name = "TrfQty";
            this.TrfQty.ReadOnly = true;
            this.TrfQty.Width = 66;
            // 
            // Unit
            // 
            this.Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 71;
            // 
            // Added_By
            // 
            this.Added_By.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Added_By.HeaderText = "By";
            this.Added_By.Name = "Added_By";
            this.Added_By.ReadOnly = true;
            this.Added_By.Width = 57;
            // 
            // frmInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dgvItem);
            this.Controls.Add(this.dgvFactoryStock);
            this.Controls.Add(this.txtTrfNote);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTrfQty);
            this.Controls.Add(this.cmbTrfQtyUnit);
            this.Controls.Add(this.lblInOutQuantity);
            this.Controls.Add(this.cmbTrfTo);
            this.Controls.Add(this.cmbTrfToCategory);
            this.Controls.Add(this.lblInOutTo);
            this.Controls.Add(this.cmbTrfFrom);
            this.Controls.Add(this.cmbTrfFromCategory);
            this.Controls.Add(this.lblInOutFrom);
            this.Controls.Add(this.cmbTrfItemCode);
            this.Controls.Add(this.lblInOutItemCode);
            this.Controls.Add(this.cmbTrfItemName);
            this.Controls.Add(this.dtpTrfDate);
            this.Controls.Add(this.dgvTrf);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.lblInOutItemName);
            this.Controls.Add(this.lblInOutTransferDate);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmInOut";
            this.Text = "frmInOut";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInOut_FormClosed);
            this.Load += new System.EventHandler(this.frmInOut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactoryStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvTrf;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Label lblInOutItemName;
        private System.Windows.Forms.Label lblInOutTransferDate;
        private System.Windows.Forms.DateTimePicker dtpTrfDate;
        private System.Windows.Forms.ComboBox cmbTrfItemName;
        private System.Windows.Forms.ComboBox cmbTrfItemCode;
        private System.Windows.Forms.Label lblInOutItemCode;
        private System.Windows.Forms.ComboBox cmbTrfFromCategory;
        private System.Windows.Forms.Label lblInOutFrom;
        private System.Windows.Forms.ComboBox cmbTrfFrom;
        private System.Windows.Forms.ComboBox cmbTrfTo;
        private System.Windows.Forms.ComboBox cmbTrfToCategory;
        private System.Windows.Forms.Label lblInOutTo;
        private System.Windows.Forms.ComboBox cmbTrfQtyUnit;
        private System.Windows.Forms.Label lblInOutQuantity;
        private System.Windows.Forms.TextBox txtTrfQty;
        private System.Windows.Forms.TextBox txtTrfNote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvFactoryStock;
        private System.Windows.Forms.DataGridView dgvItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factory;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcOrd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Added_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Trf_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn From;
        private System.Windows.Forms.DataGridViewTextBoxColumn To;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrfQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Added_By;
    }
}