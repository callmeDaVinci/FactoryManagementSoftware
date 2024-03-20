namespace FactoryManagementSoftware.UI
{
    partial class frmDeliveryDate
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
            this.lblDateType = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new Guna.UI.WinForms.GunaGradientButton();
            this.btnConfirm = new Guna.UI.WinForms.GunaGradientButton();
            this.SuspendLayout();
            // 
            // lblDateType
            // 
            this.lblDateType.AutoSize = true;
            this.lblDateType.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblDateType.Location = new System.Drawing.Point(10, 10);
            this.lblDateType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateType.Name = "lblDateType";
            this.lblDateType.Size = new System.Drawing.Size(99, 19);
            this.lblDateType.TabIndex = 174;
            this.lblDateType.Text = "Delivered Date";
            // 
            // dtpDate
            // 
            this.dtpDate.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDate.CustomFormat = "ddMMMMyy";
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(14, 32);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(294, 27);
            this.dtpDate.TabIndex = 176;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
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
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnCancel.Image = null;
            this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnCancel.Location = new System.Drawing.Point(14, 168);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnCancel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnCancel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnCancel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnCancel.OnHoverImage = null;
            this.btnCancel.OnPressedColor = System.Drawing.Color.Black;
            this.btnCancel.Radius = 2;
            this.btnCancel.Size = new System.Drawing.Size(294, 40);
            this.btnCancel.TabIndex = 237;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.AnimationHoverSpeed = 0.07F;
            this.btnConfirm.AnimationSpeed = 0.03F;
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(107)))), ((int)(((byte)(236)))));
            this.btnConfirm.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnConfirm.BorderColor = System.Drawing.Color.Black;
            this.btnConfirm.BorderSize = 1;
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnConfirm.FocusedColor = System.Drawing.Color.Empty;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Image = null;
            this.btnConfirm.ImageSize = new System.Drawing.Size(20, 20);
            this.btnConfirm.Location = new System.Drawing.Point(14, 100);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnConfirm.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnConfirm.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnConfirm.OnHoverForeColor = System.Drawing.Color.White;
            this.btnConfirm.OnHoverImage = null;
            this.btnConfirm.OnPressedColor = System.Drawing.Color.Black;
            this.btnConfirm.Radius = 2;
            this.btnConfirm.Size = new System.Drawing.Size(294, 40);
            this.btnConfirm.TabIndex = 236;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnConfirm.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // frmDeliveryDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(330, 234);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDateType);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDeliveryDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delivered Date";
            this.Load += new System.EventHandler(this.frmDeliveryDate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDateType;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private Guna.UI.WinForms.GunaGradientButton btnCancel;
        private Guna.UI.WinForms.GunaGradientButton btnConfirm;
    }
}