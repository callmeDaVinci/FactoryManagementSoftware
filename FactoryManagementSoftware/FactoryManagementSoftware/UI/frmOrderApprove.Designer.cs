namespace FactoryManagementSoftware.UI
{
    partial class frmOrderApprove
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
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.cmbQtyUnit = new System.Windows.Forms.ComboBox();
            this.lblInOutQuantity = new System.Windows.Forms.Label();
            this.lblInOutItemCode = new System.Windows.Forms.Label();
            this.dtpDateRequired = new System.Windows.Forms.DateTimePicker();
            this.lblInOutItemName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApprove
            // 
            this.btnApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApprove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApprove.ForeColor = System.Drawing.Color.White;
            this.btnApprove.Location = new System.Drawing.Point(524, 521);
            this.btnApprove.Margin = new System.Windows.Forms.Padding(2);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(135, 52);
            this.btnApprove.TabIndex = 122;
            this.btnApprove.Text = "APPROVE";
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnCancel.Location = new System.Drawing.Point(681, 522);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 50);
            this.btnCancel.TabIndex = 121;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(431, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 23);
            this.label8.TabIndex = 118;
            this.label8.Text = "UNIT";
            // 
            // txtNote
            // 
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.Location = new System.Drawing.Point(26, 342);
            this.txtNote.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(775, 80);
            this.txtNote.TabIndex = 114;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 23);
            this.label2.TabIndex = 113;
            this.label2.Text = "NOTE";
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(26, 238);
            this.txtQty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(366, 34);
            this.txtQty.TabIndex = 112;
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // cmbQtyUnit
            // 
            this.cmbQtyUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQtyUnit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbQtyUnit.FormattingEnabled = true;
            this.cmbQtyUnit.Items.AddRange(new object[] {
            "kg",
            "g",
            "set",
            "box",
            "piece"});
            this.cmbQtyUnit.Location = new System.Drawing.Point(435, 236);
            this.cmbQtyUnit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbQtyUnit.Name = "cmbQtyUnit";
            this.cmbQtyUnit.Size = new System.Drawing.Size(366, 36);
            this.cmbQtyUnit.TabIndex = 111;
            // 
            // lblInOutQuantity
            // 
            this.lblInOutQuantity.AutoSize = true;
            this.lblInOutQuantity.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutQuantity.Location = new System.Drawing.Point(22, 209);
            this.lblInOutQuantity.Name = "lblInOutQuantity";
            this.lblInOutQuantity.Size = new System.Drawing.Size(91, 23);
            this.lblInOutQuantity.TabIndex = 110;
            this.lblInOutQuantity.Text = "QUANTITY";
            // 
            // lblInOutItemCode
            // 
            this.lblInOutItemCode.AutoSize = true;
            this.lblInOutItemCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutItemCode.Location = new System.Drawing.Point(431, 121);
            this.lblInOutItemCode.Name = "lblInOutItemCode";
            this.lblInOutItemCode.Size = new System.Drawing.Size(55, 23);
            this.lblInOutItemCode.TabIndex = 108;
            this.lblInOutItemCode.Text = "CODE";
            // 
            // dtpDateRequired
            // 
            this.dtpDateRequired.CustomFormat = "dd/MM/yyyy";
            this.dtpDateRequired.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateRequired.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateRequired.Location = new System.Drawing.Point(435, 66);
            this.dtpDateRequired.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpDateRequired.Name = "dtpDateRequired";
            this.dtpDateRequired.Size = new System.Drawing.Size(366, 34);
            this.dtpDateRequired.TabIndex = 106;
            // 
            // lblInOutItemName
            // 
            this.lblInOutItemName.AutoSize = true;
            this.lblInOutItemName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOutItemName.Location = new System.Drawing.Point(22, 121);
            this.lblInOutItemName.Name = "lblInOutItemName";
            this.lblInOutItemName.Size = new System.Drawing.Size(58, 23);
            this.lblInOutItemName.TabIndex = 105;
            this.lblInOutItemName.Text = "NAME";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(431, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 23);
            this.label1.TabIndex = 104;
            this.label1.Text = "DATE REQUIRED";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 23);
            this.label5.TabIndex = 123;
            this.label5.Text = "ORDER ID";
            // 
            // txtOrderID
            // 
            this.txtOrderID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderID.Location = new System.Drawing.Point(26, 66);
            this.txtOrderID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.ReadOnly = true;
            this.txtOrderID.Size = new System.Drawing.Size(366, 34);
            this.txtOrderID.TabIndex = 124;
            // 
            // txtItemName
            // 
            this.txtItemName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemName.Location = new System.Drawing.Point(26, 148);
            this.txtItemName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.ReadOnly = true;
            this.txtItemName.Size = new System.Drawing.Size(366, 34);
            this.txtItemName.TabIndex = 125;
            // 
            // txtItemCode
            // 
            this.txtItemCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemCode.Location = new System.Drawing.Point(435, 148);
            this.txtItemCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.ReadOnly = true;
            this.txtItemCode.Size = new System.Drawing.Size(366, 34);
            this.txtItemCode.TabIndex = 126;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmOrderApprove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(829, 620);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.txtOrderID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.cmbQtyUnit);
            this.Controls.Add(this.lblInOutQuantity);
            this.Controls.Add(this.lblInOutItemCode);
            this.Controls.Add(this.dtpDateRequired);
            this.Controls.Add(this.lblInOutItemName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrderApprove";
            this.Text = "Order Approve";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.ComboBox cmbQtyUnit;
        private System.Windows.Forms.Label lblInOutQuantity;
        private System.Windows.Forms.Label lblInOutItemCode;
        private System.Windows.Forms.DateTimePicker dtpDateRequired;
        private System.Windows.Forms.Label lblInOutItemName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}