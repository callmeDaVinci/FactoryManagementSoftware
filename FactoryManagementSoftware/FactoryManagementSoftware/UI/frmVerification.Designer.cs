namespace FactoryManagementSoftware.UI
{
    partial class frmVerification
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnSwitchToMatUsed = new System.Windows.Forms.Button();
            this.lblWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(12, 44);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(301, 30);
            this.txtPassword.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(12, 29);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 12);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "PASSWORD";
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(213, 110);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(100, 36);
            this.btnCheck.TabIndex = 132;
            this.btnCheck.Text = "CONFIRM";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnSwitchToMatUsed
            // 
            this.btnSwitchToMatUsed.BackColor = System.Drawing.Color.White;
            this.btnSwitchToMatUsed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitchToMatUsed.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitchToMatUsed.ForeColor = System.Drawing.Color.Black;
            this.btnSwitchToMatUsed.Location = new System.Drawing.Point(105, 110);
            this.btnSwitchToMatUsed.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnSwitchToMatUsed.Name = "btnSwitchToMatUsed";
            this.btnSwitchToMatUsed.Size = new System.Drawing.Size(100, 36);
            this.btnSwitchToMatUsed.TabIndex = 133;
            this.btnSwitchToMatUsed.Text = "CANCEL";
            this.btnSwitchToMatUsed.UseVisualStyleBackColor = false;
            this.btnSwitchToMatUsed.Click += new System.EventHandler(this.btnSwitchToMatUsed_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Italic);
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(11, 77);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(39, 15);
            this.lblWarning.TabIndex = 134;
            this.lblWarning.Text = "label2";
            this.lblWarning.Visible = false;
            // 
            // frmVerification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(326, 165);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.btnSwitchToMatUsed);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            this.Name = "frmVerification";
            this.Text = "Verification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnSwitchToMatUsed;
        private System.Windows.Forms.Label lblWarning;
    }
}