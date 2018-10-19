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
            this.dgvcCustID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcCustName = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvItem = new System.Windows.Forms.DataGridView();
            this.dgvcItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcOrd = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dgvcCustID,
            this.dgvcCustName});
            this.dgvTrf.Location = new System.Drawing.Point(675, 306);
            this.dgvTrf.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTrf.Name = "dgvTrf";
            this.dgvTrf.ReadOnly = true;
            this.dgvTrf.RowTemplate.Height = 24;
            this.dgvTrf.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrf.Size = new System.Drawing.Size(872, 337);
            this.dgvTrf.TabIndex = 37;
            // 
            // dgvcCustID
            // 
            this.dgvcCustID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvcCustID.HeaderText = "ID";
            this.dgvcCustID.Name = "dgvcCustID";
            this.dgvcCustID.ReadOnly = true;
            this.dgvcCustID.Width = 56;
            // 
            // dgvcCustName
            // 
            this.dgvcCustName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvcCustName.HeaderText = "Customer Name";
            this.dgvcCustName.Name = "dgvcCustName";
            this.dgvcCustName.ReadOnly = true;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransfer.Location = new System.Drawing.Point(439, 595);
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
            this.lblInOutItemName.Location = new System.Drawing.Point(90, 120);
            this.lblInOutItemName.Name = "lblInOutItemName";
            this.lblInOutItemName.Size = new System.Drawing.Size(144, 32);
            this.lblInOutItemName.TabIndex = 32;
            this.lblInOutItemName.Text = "*Item Name";
            // 
            // lblInOutTransferDate
            // 
            this.lblInOutTransferDate.AutoSize = true;
            this.lblInOutTransferDate.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutTransferDate.Location = new System.Drawing.Point(68, 59);
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
            this.dtpTrfDate.Location = new System.Drawing.Point(240, 59);
            this.dtpTrfDate.Name = "dtpTrfDate";
            this.dtpTrfDate.Size = new System.Drawing.Size(367, 38);
            this.dtpTrfDate.TabIndex = 40;
            // 
            // cmbTrfItemName
            // 
            this.cmbTrfItemName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfItemName.FormattingEnabled = true;
            this.cmbTrfItemName.Location = new System.Drawing.Point(240, 120);
            this.cmbTrfItemName.Name = "cmbTrfItemName";
            this.cmbTrfItemName.Size = new System.Drawing.Size(367, 39);
            this.cmbTrfItemName.TabIndex = 41;
            // 
            // cmbTrfItemCode
            // 
            this.cmbTrfItemCode.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfItemCode.FormattingEnabled = true;
            this.cmbTrfItemCode.Location = new System.Drawing.Point(240, 181);
            this.cmbTrfItemCode.Name = "cmbTrfItemCode";
            this.cmbTrfItemCode.Size = new System.Drawing.Size(367, 39);
            this.cmbTrfItemCode.TabIndex = 43;
            // 
            // lblInOutItemCode
            // 
            this.lblInOutItemCode.AutoSize = true;
            this.lblInOutItemCode.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutItemCode.Location = new System.Drawing.Point(98, 181);
            this.lblInOutItemCode.Name = "lblInOutItemCode";
            this.lblInOutItemCode.Size = new System.Drawing.Size(136, 32);
            this.lblInOutItemCode.TabIndex = 42;
            this.lblInOutItemCode.Text = "*Item Code";
            // 
            // cmbTrfFromCategory
            // 
            this.cmbTrfFromCategory.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfFromCategory.FormattingEnabled = true;
            this.cmbTrfFromCategory.Location = new System.Drawing.Point(240, 242);
            this.cmbTrfFromCategory.Name = "cmbTrfFromCategory";
            this.cmbTrfFromCategory.Size = new System.Drawing.Size(168, 39);
            this.cmbTrfFromCategory.TabIndex = 45;
            // 
            // lblInOutFrom
            // 
            this.lblInOutFrom.AutoSize = true;
            this.lblInOutFrom.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutFrom.Location = new System.Drawing.Point(154, 242);
            this.lblInOutFrom.Name = "lblInOutFrom";
            this.lblInOutFrom.Size = new System.Drawing.Size(80, 32);
            this.lblInOutFrom.TabIndex = 44;
            this.lblInOutFrom.Text = "*From";
            // 
            // cmbTrfFrom
            // 
            this.cmbTrfFrom.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfFrom.FormattingEnabled = true;
            this.cmbTrfFrom.Location = new System.Drawing.Point(439, 242);
            this.cmbTrfFrom.Name = "cmbTrfFrom";
            this.cmbTrfFrom.Size = new System.Drawing.Size(168, 39);
            this.cmbTrfFrom.TabIndex = 46;
            // 
            // cmbTrfTo
            // 
            this.cmbTrfTo.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfTo.FormattingEnabled = true;
            this.cmbTrfTo.Location = new System.Drawing.Point(439, 306);
            this.cmbTrfTo.Name = "cmbTrfTo";
            this.cmbTrfTo.Size = new System.Drawing.Size(168, 39);
            this.cmbTrfTo.TabIndex = 49;
            // 
            // cmbTrfToCategory
            // 
            this.cmbTrfToCategory.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfToCategory.FormattingEnabled = true;
            this.cmbTrfToCategory.Location = new System.Drawing.Point(240, 306);
            this.cmbTrfToCategory.Name = "cmbTrfToCategory";
            this.cmbTrfToCategory.Size = new System.Drawing.Size(168, 39);
            this.cmbTrfToCategory.TabIndex = 48;
            // 
            // lblInOutTo
            // 
            this.lblInOutTo.AutoSize = true;
            this.lblInOutTo.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutTo.Location = new System.Drawing.Point(184, 306);
            this.lblInOutTo.Name = "lblInOutTo";
            this.lblInOutTo.Size = new System.Drawing.Size(50, 32);
            this.lblInOutTo.TabIndex = 47;
            this.lblInOutTo.Text = "*To";
            // 
            // cmbTrfQtyUnit
            // 
            this.cmbTrfQtyUnit.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfQtyUnit.FormattingEnabled = true;
            this.cmbTrfQtyUnit.Items.AddRange(new object[] {
            "kg",
            "g",
            "set",
            "box",
            "piece"});
            this.cmbTrfQtyUnit.Location = new System.Drawing.Point(439, 368);
            this.cmbTrfQtyUnit.Name = "cmbTrfQtyUnit";
            this.cmbTrfQtyUnit.Size = new System.Drawing.Size(168, 39);
            this.cmbTrfQtyUnit.TabIndex = 52;
            // 
            // lblInOutQuantity
            // 
            this.lblInOutQuantity.AutoSize = true;
            this.lblInOutQuantity.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutQuantity.Location = new System.Drawing.Point(117, 368);
            this.lblInOutQuantity.Name = "lblInOutQuantity";
            this.lblInOutQuantity.Size = new System.Drawing.Size(117, 32);
            this.lblInOutQuantity.TabIndex = 50;
            this.lblInOutQuantity.Text = "*Quantity";
            // 
            // txtTrfQty
            // 
            this.txtTrfQty.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrfQty.Location = new System.Drawing.Point(240, 369);
            this.txtTrfQty.Name = "txtTrfQty";
            this.txtTrfQty.Size = new System.Drawing.Size(168, 38);
            this.txtTrfQty.TabIndex = 53;
            // 
            // txtTrfNote
            // 
            this.txtTrfNote.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrfNote.Location = new System.Drawing.Point(240, 431);
            this.txtTrfNote.Multiline = true;
            this.txtTrfNote.Name = "txtTrfNote";
            this.txtTrfNote.Size = new System.Drawing.Size(367, 116);
            this.txtTrfNote.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(166, 431);
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
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvFactoryStock.Location = new System.Drawing.Point(675, 59);
            this.dgvFactoryStock.Margin = new System.Windows.Forms.Padding(2);
            this.dgvFactoryStock.Name = "dgvFactoryStock";
            this.dgvFactoryStock.ReadOnly = true;
            this.dgvFactoryStock.RowTemplate.Height = 24;
            this.dgvFactoryStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFactoryStock.Size = new System.Drawing.Size(423, 228);
            this.dgvFactoryStock.TabIndex = 56;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 56;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Customer Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
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
            this.dgvItem.Location = new System.Drawing.Point(1121, 59);
            this.dgvItem.Margin = new System.Windows.Forms.Padding(2);
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.ReadOnly = true;
            this.dgvItem.RowTemplate.Height = 24;
            this.dgvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItem.Size = new System.Drawing.Size(426, 228);
            this.dgvItem.TabIndex = 58;
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
            this.dgvcQty.HeaderText = "Quantity";
            this.dgvcQty.Name = "dgvcQty";
            this.dgvcQty.ReadOnly = true;
            this.dgvcQty.Width = 105;
            // 
            // dgvcOrd
            // 
            this.dgvcOrd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvcOrd.HeaderText = "Order";
            this.dgvcOrd.Name = "dgvcOrd";
            this.dgvcOrd.ReadOnly = true;
            this.dgvcOrd.Width = 83;
            // 
            // frmInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1582, 703);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactoryStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvTrf;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCustID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCustName;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridView dgvItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcOrd;
    }
}