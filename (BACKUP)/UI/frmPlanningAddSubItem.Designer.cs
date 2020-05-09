namespace FactoryManagementSoftware.UI
{
    partial class frmPlanningAddSubItem
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
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPlannedUse = new System.Windows.Forms.TextBox();
            this.cmbPartCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPartName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.txtAvailable = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRequiredQty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtShort = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 19);
            this.label2.TabIndex = 19;
            this.label2.Text = "CODE";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 200);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 19);
            this.label10.TabIndex = 24;
            this.label10.Text = "STOCK";
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(24, 222);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(101, 25);
            this.txtStock.TabIndex = 23;
            this.txtStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStock_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(133, 200);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 19);
            this.label9.TabIndex = 22;
            this.label9.Text = "PLANNED USE";
            // 
            // txtPlannedUse
            // 
            this.txtPlannedUse.Location = new System.Drawing.Point(137, 222);
            this.txtPlannedUse.Name = "txtPlannedUse";
            this.txtPlannedUse.Size = new System.Drawing.Size(101, 25);
            this.txtPlannedUse.TabIndex = 21;
            this.txtPlannedUse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPlannedUse.TextChanged += new System.EventHandler(this.txtPlannedUse_TextChanged);
            this.txtPlannedUse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlannedUse_KeyPress);
            // 
            // cmbPartCode
            // 
            this.cmbPartCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartCode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbPartCode.FormattingEnabled = true;
            this.cmbPartCode.Location = new System.Drawing.Point(24, 154);
            this.cmbPartCode.Name = "cmbPartCode";
            this.cmbPartCode.Size = new System.Drawing.Size(328, 25);
            this.cmbPartCode.TabIndex = 20;
            this.cmbPartCode.SelectedIndexChanged += new System.EventHandler(this.cmbPartCode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 19);
            this.label1.TabIndex = 17;
            this.label1.Text = "NAME";
            // 
            // cmbPartName
            // 
            this.cmbPartName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPartName.FormattingEnabled = true;
            this.cmbPartName.Location = new System.Drawing.Point(24, 89);
            this.cmbPartName.Name = "cmbPartName";
            this.cmbPartName.Size = new System.Drawing.Size(328, 25);
            this.cmbPartName.TabIndex = 18;
            this.cmbPartName.SelectedIndexChanged += new System.EventHandler(this.cmbPartName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 19);
            this.label3.TabIndex = 25;
            this.label3.Text = "CATEGORY";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(24, 28);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(328, 25);
            this.cmbCategory.TabIndex = 26;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // txtAvailable
            // 
            this.txtAvailable.Location = new System.Drawing.Point(251, 222);
            this.txtAvailable.Name = "txtAvailable";
            this.txtAvailable.Size = new System.Drawing.Size(101, 25);
            this.txtAvailable.TabIndex = 27;
            this.txtAvailable.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAvailable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAvailable_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 19);
            this.label4.TabIndex = 28;
            this.label4.Text = "AVAILABLE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 19);
            this.label5.TabIndex = 30;
            this.label5.Text = "REQUIRED QTY";
            // 
            // txtRequiredQty
            // 
            this.txtRequiredQty.BackColor = System.Drawing.SystemColors.Info;
            this.txtRequiredQty.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequiredQty.Location = new System.Drawing.Point(24, 292);
            this.txtRequiredQty.Name = "txtRequiredQty";
            this.txtRequiredQty.Size = new System.Drawing.Size(328, 34);
            this.txtRequiredQty.TabIndex = 29;
            this.txtRequiredQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRequiredQty.TextChanged += new System.EventHandler(this.txtRequiredQty_TextChanged);
            this.txtRequiredQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRequiredQty_KeyDown);
            this.txtRequiredQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRequiredQty_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 350);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 19);
            this.label6.TabIndex = 32;
            this.label6.Text = "SHORT";
            // 
            // txtShort
            // 
            this.txtShort.Location = new System.Drawing.Point(24, 372);
            this.txtShort.Name = "txtShort";
            this.txtShort.Size = new System.Drawing.Size(328, 25);
            this.txtShort.TabIndex = 31;
            this.txtShort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(232, 455);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 50);
            this.btnSave.TabIndex = 103;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(96, 455);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 50);
            this.btnCancel.TabIndex = 104;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // frmPlanningAddSubItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(386, 525);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtShort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRequiredQty);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAvailable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPlannedUse);
            this.Controls.Add(this.cmbPartCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPartName);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmPlanningAddSubItem";
            this.Text = "frmPlanningAddSubItem";
            this.Load += new System.EventHandler(this.frmPlanningAddSubItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPlannedUse;
        private System.Windows.Forms.ComboBox cmbPartCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPartName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.TextBox txtAvailable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRequiredQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtShort;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
    }
}