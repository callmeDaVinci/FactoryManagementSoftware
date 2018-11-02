namespace FactoryManagementSoftware.UI
{
    partial class frmJoinEdit
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbParentCat = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.cmbParentName = new System.Windows.Forms.ComboBox();
            this.cmbParentCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbChildCode = new System.Windows.Forms.ComboBox();
            this.cmbChildName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbChildCat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider6 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(890, 549);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 50);
            this.btnCancel.TabIndex = 59;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(228, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 23);
            this.label4.TabIndex = 56;
            this.label4.Text = "*Category";
            // 
            // cmbParentCat
            // 
            this.cmbParentCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParentCat.Enabled = false;
            this.cmbParentCat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParentCat.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbParentCat.FormattingEnabled = true;
            this.cmbParentCat.Items.AddRange(new object[] {
            "Part"});
            this.cmbParentCat.Location = new System.Drawing.Point(232, 44);
            this.cmbParentCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbParentCat.Name = "cmbParentCat";
            this.cmbParentCat.Size = new System.Drawing.Size(366, 36);
            this.cmbParentCat.TabIndex = 47;
            this.cmbParentCat.SelectedIndexChanged += new System.EventHandler(this.cmbParentCat_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(722, 548);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 52);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemName.Location = new System.Drawing.Point(228, 96);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(103, 23);
            this.lblItemName.TabIndex = 44;
            this.lblItemName.Text = "*Item Name";
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCode.Location = new System.Drawing.Point(637, 96);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(97, 23);
            this.lblItemCode.TabIndex = 42;
            this.lblItemCode.Text = "*Item Code";
            // 
            // cmbParentName
            // 
            this.cmbParentName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParentName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParentName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbParentName.FormattingEnabled = true;
            this.cmbParentName.Location = new System.Drawing.Point(232, 123);
            this.cmbParentName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbParentName.Name = "cmbParentName";
            this.cmbParentName.Size = new System.Drawing.Size(366, 36);
            this.cmbParentName.TabIndex = 60;
            this.cmbParentName.SelectedIndexChanged += new System.EventHandler(this.cmbParentName_SelectedIndexChanged);
            // 
            // cmbParentCode
            // 
            this.cmbParentCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParentCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParentCode.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbParentCode.FormattingEnabled = true;
            this.cmbParentCode.Location = new System.Drawing.Point(641, 123);
            this.cmbParentCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbParentCode.Name = "cmbParentCode";
            this.cmbParentCode.Size = new System.Drawing.Size(366, 36);
            this.cmbParentCode.TabIndex = 61;
            this.cmbParentCode.SelectedIndexChanged += new System.EventHandler(this.cmbParentCode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 81);
            this.label1.TabIndex = 62;
            this.label1.Text = "Parent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(51, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 81);
            this.label2.TabIndex = 69;
            this.label2.Text = "Child";
            // 
            // cmbChildCode
            // 
            this.cmbChildCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChildCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChildCode.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbChildCode.FormattingEnabled = true;
            this.cmbChildCode.Location = new System.Drawing.Point(641, 318);
            this.cmbChildCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbChildCode.Name = "cmbChildCode";
            this.cmbChildCode.Size = new System.Drawing.Size(366, 36);
            this.cmbChildCode.TabIndex = 68;
            this.cmbChildCode.SelectedIndexChanged += new System.EventHandler(this.cmbChildCode_SelectedIndexChanged);
            // 
            // cmbChildName
            // 
            this.cmbChildName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChildName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChildName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbChildName.FormattingEnabled = true;
            this.cmbChildName.Location = new System.Drawing.Point(232, 318);
            this.cmbChildName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbChildName.Name = "cmbChildName";
            this.cmbChildName.Size = new System.Drawing.Size(366, 36);
            this.cmbChildName.TabIndex = 67;
            this.cmbChildName.SelectedIndexChanged += new System.EventHandler(this.cmbChildName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(228, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 23);
            this.label3.TabIndex = 66;
            this.label3.Text = "*Category";
            // 
            // cmbChildCat
            // 
            this.cmbChildCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChildCat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChildCat.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbChildCat.FormattingEnabled = true;
            this.cmbChildCat.Location = new System.Drawing.Point(232, 239);
            this.cmbChildCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbChildCat.Name = "cmbChildCat";
            this.cmbChildCat.Size = new System.Drawing.Size(366, 36);
            this.cmbChildCat.TabIndex = 65;
            this.cmbChildCat.SelectedIndexChanged += new System.EventHandler(this.cmbChildCat_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(228, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 23);
            this.label5.TabIndex = 64;
            this.label5.Text = "*Item Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(637, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 23);
            this.label6.TabIndex = 63;
            this.label6.Text = "*Item Code";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(228, 415);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 23);
            this.label7.TabIndex = 70;
            this.label7.Text = "Qty";
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(232, 442);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(366, 36);
            this.txtQty.TabIndex = 72;
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
            // frmJoinEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1070, 622);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbChildCode);
            this.Controls.Add(this.cmbChildName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbChildCat);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbParentCode);
            this.Controls.Add(this.cmbParentName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbParentCat);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblItemName);
            this.Controls.Add(this.lblItemCode);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmJoinEdit";
            this.ShowIcon = false;
            this.Text = "Join Edit";
            this.Load += new System.EventHandler(this.frmJoinEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbParentCat;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.ComboBox cmbParentName;
        private System.Windows.Forms.ComboBox cmbParentCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbChildCode;
        private System.Windows.Forms.ComboBox cmbChildName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbChildCat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;
        private System.Windows.Forms.ErrorProvider errorProvider6;
    }
}