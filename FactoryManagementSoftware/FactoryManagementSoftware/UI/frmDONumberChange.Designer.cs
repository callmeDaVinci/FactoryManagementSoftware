namespace FactoryManagementSoftware.UI
{
    partial class frmDONumberChange
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
            this.btnConfirm = new System.Windows.Forms.Button();
            this.txtOldDONo = new System.Windows.Forms.TextBox();
            this.lblOldDONo = new System.Windows.Forms.Label();
            this.txtNewDONo = new System.Windows.Forms.TextBox();
            this.lblAvailableCheck = new System.Windows.Forms.Label();
            this.lblAvailableResult = new System.Windows.Forms.Label();
            this.lblNewDONo = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(26, 257);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 36);
            this.btnCancel.TabIndex = 169;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(168, 257);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(124, 36);
            this.btnConfirm.TabIndex = 168;
            this.btnConfirm.Text = "CONFIRM";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtOldDONo
            // 
            this.txtOldDONo.Location = new System.Drawing.Point(26, 64);
            this.txtOldDONo.Name = "txtOldDONo";
            this.txtOldDONo.Size = new System.Drawing.Size(266, 25);
            this.txtOldDONo.TabIndex = 170;
            this.txtOldDONo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOldDONo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOldDONo_KeyPress);
            // 
            // lblOldDONo
            // 
            this.lblOldDONo.AutoSize = true;
            this.lblOldDONo.Location = new System.Drawing.Point(22, 42);
            this.lblOldDONo.Name = "lblOldDONo";
            this.lblOldDONo.Size = new System.Drawing.Size(95, 19);
            this.lblOldDONo.TabIndex = 171;
            this.lblOldDONo.Text = "OLD D/O NO.";
            // 
            // txtNewDONo
            // 
            this.txtNewDONo.Location = new System.Drawing.Point(26, 134);
            this.txtNewDONo.Name = "txtNewDONo";
            this.txtNewDONo.Size = new System.Drawing.Size(266, 25);
            this.txtNewDONo.TabIndex = 172;
            this.txtNewDONo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNewDONo.TextChanged += new System.EventHandler(this.txtNewDONo_TextChanged);
            this.txtNewDONo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumeric_KeyPress);
            // 
            // lblAvailableCheck
            // 
            this.lblAvailableCheck.AutoSize = true;
            this.lblAvailableCheck.BackColor = System.Drawing.SystemColors.Window;
            this.lblAvailableCheck.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableCheck.ForeColor = System.Drawing.Color.Blue;
            this.lblAvailableCheck.Location = new System.Drawing.Point(181, 114);
            this.lblAvailableCheck.Name = "lblAvailableCheck";
            this.lblAvailableCheck.Size = new System.Drawing.Size(105, 15);
            this.lblAvailableCheck.TabIndex = 174;
            this.lblAvailableCheck.Text = "CHECK AVAILABLE";
            this.lblAvailableCheck.Click += new System.EventHandler(this.lblAvailableCheck_Click);
            // 
            // lblAvailableResult
            // 
            this.lblAvailableResult.AutoSize = true;
            this.lblAvailableResult.ForeColor = System.Drawing.Color.Green;
            this.lblAvailableResult.Location = new System.Drawing.Point(22, 162);
            this.lblAvailableResult.Name = "lblAvailableResult";
            this.lblAvailableResult.Size = new System.Drawing.Size(147, 19);
            this.lblAvailableResult.TabIndex = 175;
            this.lblAvailableResult.Text = "Number is AVAILABLE!";
            // 
            // lblNewDONo
            // 
            this.lblNewDONo.AutoSize = true;
            this.lblNewDONo.Location = new System.Drawing.Point(22, 112);
            this.lblNewDONo.Name = "lblNewDONo";
            this.lblNewDONo.Size = new System.Drawing.Size(97, 19);
            this.lblNewDONo.TabIndex = 176;
            this.lblNewDONo.Text = "NEW D/O NO.";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmDONumberChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(323, 321);
            this.Controls.Add(this.lblNewDONo);
            this.Controls.Add(this.lblAvailableResult);
            this.Controls.Add(this.lblAvailableCheck);
            this.Controls.Add(this.txtNewDONo);
            this.Controls.Add(this.lblOldDONo);
            this.Controls.Add(this.txtOldDONo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmDONumberChange";
            this.Text = "Change D/O Number";
            this.Load += new System.EventHandler(this.frmDONumberChange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox txtOldDONo;
        private System.Windows.Forms.Label lblOldDONo;
        private System.Windows.Forms.TextBox txtNewDONo;
        private System.Windows.Forms.Label lblAvailableCheck;
        private System.Windows.Forms.Label lblAvailableResult;
        private System.Windows.Forms.Label lblNewDONo;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}