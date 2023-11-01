namespace FactoryManagementSoftware.UI
{
    partial class frmReadSemenyihData
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
            this.btnConfirm = new Guna.UI.WinForms.GunaGradientButton();
            this.btnEdit = new Guna.UI.WinForms.GunaGradientButton();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.AnimationHoverSpeed = 0.07F;
            this.btnConfirm.AnimationSpeed = 0.03F;
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnConfirm.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnConfirm.BorderColor = System.Drawing.Color.Black;
            this.btnConfirm.BorderSize = 1;
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnConfirm.FocusedColor = System.Drawing.Color.Empty;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Image = null;
            this.btnConfirm.ImageSize = new System.Drawing.Size(20, 20);
            this.btnConfirm.Location = new System.Drawing.Point(755, 155);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnConfirm.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnConfirm.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnConfirm.OnHoverForeColor = System.Drawing.Color.White;
            this.btnConfirm.OnHoverImage = null;
            this.btnConfirm.OnPressedColor = System.Drawing.Color.Black;
            this.btnConfirm.Radius = 2;
            this.btnConfirm.Size = new System.Drawing.Size(146, 40);
            this.btnConfirm.TabIndex = 227;
            this.btnConfirm.Text = "Upload";
            this.btnConfirm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.AnimationHoverSpeed = 0.07F;
            this.btnEdit.AnimationSpeed = 0.03F;
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(160)))));
            this.btnEdit.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(160)))));
            this.btnEdit.BorderColor = System.Drawing.Color.Black;
            this.btnEdit.BorderSize = 1;
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEdit.FocusedColor = System.Drawing.Color.Empty;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnEdit.Image = null;
            this.btnEdit.ImageSize = new System.Drawing.Size(20, 20);
            this.btnEdit.Location = new System.Drawing.Point(401, 155);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnEdit.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnEdit.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnEdit.OnHoverForeColor = System.Drawing.Color.White;
            this.btnEdit.OnHoverImage = null;
            this.btnEdit.OnPressedColor = System.Drawing.Color.Black;
            this.btnEdit.Radius = 2;
            this.btnEdit.Size = new System.Drawing.Size(146, 40);
            this.btnEdit.TabIndex = 229;
            this.btnEdit.Text = "Export";
            this.btnEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // frmReadSemenyihData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1282, 703);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnConfirm);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmReadSemenyihData";
            this.Text = "frmReadSemenyihData";
            this.Load += new System.EventHandler(this.frmReadSemenyihData_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI.WinForms.GunaGradientButton btnConfirm;
        private Guna.UI.WinForms.GunaGradientButton btnEdit;
    }
}