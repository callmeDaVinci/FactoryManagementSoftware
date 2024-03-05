namespace FactoryManagementSoftware.UI
{
    partial class frmDONumberChange_v2
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
            this.lblOldDONo = new System.Windows.Forms.Label();
            this.lblAvailableCheck = new System.Windows.Forms.Label();
            this.lblAvailableResult = new System.Windows.Forms.Label();
            this.lblNewDONo = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtOldDONo = new Guna.UI.WinForms.GunaTextBox();
            this.txtNewDONo = new Guna.UI.WinForms.GunaTextBox();
            this.btnConfirm = new Guna.UI.WinForms.GunaGradientButton();
            this.btnCancel = new Guna.UI.WinForms.GunaGradientButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOldDONo
            // 
            this.lblOldDONo.AutoSize = true;
            this.lblOldDONo.Location = new System.Drawing.Point(22, 42);
            this.lblOldDONo.Name = "lblOldDONo";
            this.lblOldDONo.Size = new System.Drawing.Size(88, 17);
            this.lblOldDONo.TabIndex = 171;
            this.lblOldDONo.Text = "OLD D/O NO.";
            // 
            // lblAvailableCheck
            // 
            this.lblAvailableCheck.AutoSize = true;
            this.lblAvailableCheck.BackColor = System.Drawing.Color.Transparent;
            this.lblAvailableCheck.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableCheck.ForeColor = System.Drawing.Color.Blue;
            this.lblAvailableCheck.Location = new System.Drawing.Point(23, 168);
            this.lblAvailableCheck.Name = "lblAvailableCheck";
            this.lblAvailableCheck.Size = new System.Drawing.Size(105, 15);
            this.lblAvailableCheck.TabIndex = 174;
            this.lblAvailableCheck.Text = "CHECK AVAILABLE";
            this.lblAvailableCheck.Click += new System.EventHandler(this.lblAvailableCheck_Click);
            // 
            // lblAvailableResult
            // 
            this.lblAvailableResult.AutoSize = true;
            this.lblAvailableResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(241)))), ((int)(((byte)(218)))));
            this.lblAvailableResult.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblAvailableResult.ForeColor = System.Drawing.Color.Black;
            this.lblAvailableResult.Location = new System.Drawing.Point(144, 164);
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
            this.lblNewDONo.Size = new System.Drawing.Size(92, 17);
            this.lblNewDONo.TabIndex = 176;
            this.lblNewDONo.Text = "NEW D/O NO.";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtOldDONo
            // 
            this.txtOldDONo.BackColor = System.Drawing.Color.Transparent;
            this.txtOldDONo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtOldDONo.BaseColor = System.Drawing.Color.White;
            this.txtOldDONo.BorderColor = System.Drawing.Color.Silver;
            this.txtOldDONo.BorderSize = 1;
            this.txtOldDONo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOldDONo.FocusedBaseColor = System.Drawing.Color.White;
            this.txtOldDONo.FocusedBorderColor = System.Drawing.Color.White;
            this.txtOldDONo.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtOldDONo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtOldDONo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtOldDONo.Location = new System.Drawing.Point(25, 62);
            this.txtOldDONo.Name = "txtOldDONo";
            this.txtOldDONo.PasswordChar = '\0';
            this.txtOldDONo.Radius = 3;
            this.txtOldDONo.ReadOnly = true;
            this.txtOldDONo.SelectedText = "";
            this.txtOldDONo.Size = new System.Drawing.Size(266, 28);
            this.txtOldDONo.TabIndex = 2007;
            this.txtOldDONo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNewDONo
            // 
            this.txtNewDONo.BackColor = System.Drawing.Color.Transparent;
            this.txtNewDONo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtNewDONo.BaseColor = System.Drawing.Color.White;
            this.txtNewDONo.BorderColor = System.Drawing.Color.Silver;
            this.txtNewDONo.BorderSize = 1;
            this.txtNewDONo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewDONo.FocusedBaseColor = System.Drawing.Color.White;
            this.txtNewDONo.FocusedBorderColor = System.Drawing.Color.White;
            this.txtNewDONo.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtNewDONo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtNewDONo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNewDONo.Location = new System.Drawing.Point(25, 133);
            this.txtNewDONo.Name = "txtNewDONo";
            this.txtNewDONo.PasswordChar = '\0';
            this.txtNewDONo.Radius = 3;
            this.txtNewDONo.SelectedText = "";
            this.txtNewDONo.Size = new System.Drawing.Size(266, 28);
            this.txtNewDONo.TabIndex = 2008;
            this.txtNewDONo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AnimationHoverSpeed = 0.07F;
            this.btnConfirm.AnimationSpeed = 0.03F;
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            this.btnConfirm.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            this.btnConfirm.BorderColor = System.Drawing.Color.Black;
            this.btnConfirm.BorderSize = 1;
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnConfirm.FocusedColor = System.Drawing.Color.Empty;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnConfirm.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnConfirm.Image = null;
            this.btnConfirm.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnConfirm.ImageSize = new System.Drawing.Size(25, 25);
            this.btnConfirm.Location = new System.Drawing.Point(164, 254);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnConfirm.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnConfirm.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnConfirm.OnHoverForeColor = System.Drawing.Color.White;
            this.btnConfirm.OnHoverImage = null;
            this.btnConfirm.OnPressedColor = System.Drawing.Color.Black;
            this.btnConfirm.Radius = 5;
            this.btnConfirm.Size = new System.Drawing.Size(124, 39);
            this.btnConfirm.TabIndex = 2009;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AnimationHoverSpeed = 0.07F;
            this.btnCancel.AnimationSpeed = 0.03F;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BaseColor1 = System.Drawing.Color.White;
            this.btnCancel.BaseColor2 = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.FocusedColor = System.Drawing.Color.Empty;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Image = null;
            this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnCancel.Location = new System.Drawing.Point(26, 254);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnCancel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnCancel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnCancel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnCancel.OnHoverImage = null;
            this.btnCancel.OnPressedColor = System.Drawing.Color.Black;
            this.btnCancel.Radius = 5;
            this.btnCancel.Size = new System.Drawing.Size(124, 39);
            this.btnCancel.TabIndex = 2010;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmDONumberChange_v2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(323, 321);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtNewDONo);
            this.Controls.Add(this.txtOldDONo);
            this.Controls.Add(this.lblNewDONo);
            this.Controls.Add(this.lblAvailableResult);
            this.Controls.Add(this.lblAvailableCheck);
            this.Controls.Add(this.lblOldDONo);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDONumberChange_v2";
            this.Text = "Change D/O Number";
            this.Load += new System.EventHandler(this.frmDONumberChange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblOldDONo;
        private System.Windows.Forms.Label lblAvailableCheck;
        private System.Windows.Forms.Label lblAvailableResult;
        private System.Windows.Forms.Label lblNewDONo;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Guna.UI.WinForms.GunaTextBox txtNewDONo;
        private Guna.UI.WinForms.GunaTextBox txtOldDONo;
        private Guna.UI.WinForms.GunaGradientButton btnConfirm;
        private Guna.UI.WinForms.GunaGradientButton btnCancel;
    }
}