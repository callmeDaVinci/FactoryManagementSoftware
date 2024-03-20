namespace FactoryManagementSoftware.UI
{
    partial class frmDOCompleteSetting
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
            this.dtpDeliveredDate = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new Guna.UI.WinForms.GunaGradientButton();
            this.btnFilterApply = new Guna.UI.WinForms.GunaGradientButton();
            this.lblDeliveredDate = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbFrom = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbToCat = new System.Windows.Forms.ComboBox();
            this.cmbTo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDeliveredDate
            // 
            this.dtpDeliveredDate.CustomFormat = "ddMMMMyy";
            this.dtpDeliveredDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDeliveredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeliveredDate.Location = new System.Drawing.Point(49, 57);
            this.dtpDeliveredDate.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.dtpDeliveredDate.Name = "dtpDeliveredDate";
            this.dtpDeliveredDate.Size = new System.Drawing.Size(239, 27);
            this.dtpDeliveredDate.TabIndex = 176;
            this.dtpDeliveredDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
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
            this.btnCancel.Location = new System.Drawing.Point(238, 454);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnCancel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnCancel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnCancel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnCancel.OnHoverImage = null;
            this.btnCancel.OnPressedColor = System.Drawing.Color.Black;
            this.btnCancel.Radius = 2;
            this.btnCancel.Size = new System.Drawing.Size(157, 40);
            this.btnCancel.TabIndex = 241;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.AnimationHoverSpeed = 0.07F;
            this.btnFilterApply.AnimationSpeed = 0.03F;
            this.btnFilterApply.BackColor = System.Drawing.Color.Transparent;
            this.btnFilterApply.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(107)))), ((int)(((byte)(236)))));
            this.btnFilterApply.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnFilterApply.BorderColor = System.Drawing.Color.Black;
            this.btnFilterApply.BorderSize = 1;
            this.btnFilterApply.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFilterApply.FocusedColor = System.Drawing.Color.Empty;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFilterApply.ForeColor = System.Drawing.Color.White;
            this.btnFilterApply.Image = null;
            this.btnFilterApply.ImageSize = new System.Drawing.Size(20, 20);
            this.btnFilterApply.Location = new System.Drawing.Point(402, 454);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnFilterApply.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnFilterApply.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnFilterApply.OnHoverForeColor = System.Drawing.Color.White;
            this.btnFilterApply.OnHoverImage = null;
            this.btnFilterApply.OnPressedColor = System.Drawing.Color.Black;
            this.btnFilterApply.Radius = 2;
            this.btnFilterApply.Size = new System.Drawing.Size(157, 40);
            this.btnFilterApply.TabIndex = 240;
            this.btnFilterApply.Text = "Next";
            this.btnFilterApply.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // lblDeliveredDate
            // 
            this.lblDeliveredDate.AutoSize = true;
            this.lblDeliveredDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDeliveredDate.Location = new System.Drawing.Point(45, 34);
            this.lblDeliveredDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeliveredDate.Name = "lblDeliveredDate";
            this.lblDeliveredDate.Size = new System.Drawing.Size(109, 20);
            this.lblDeliveredDate.TabIndex = 238;
            this.lblDeliveredDate.Text = "Delivered Date";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.cmbFrom);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBox2.Location = new System.Drawing.Point(49, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(510, 92);
            this.groupBox2.TabIndex = 243;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "From (DEFAULT)";
            // 
            // cmbFrom
            // 
            this.cmbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFrom.FormattingEnabled = true;
            this.cmbFrom.Location = new System.Drawing.Point(20, 36);
            this.cmbFrom.Name = "cmbFrom";
            this.cmbFrom.Size = new System.Drawing.Size(469, 28);
            this.cmbFrom.TabIndex = 245;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbToCat);
            this.groupBox1.Controls.Add(this.cmbTo);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBox1.Location = new System.Drawing.Point(49, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 143);
            this.groupBox1.TabIndex = 242;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "To (DEFAULT)";
            // 
            // cmbToCat
            // 
            this.cmbToCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToCat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbToCat.FormattingEnabled = true;
            this.cmbToCat.Location = new System.Drawing.Point(20, 39);
            this.cmbToCat.Name = "cmbToCat";
            this.cmbToCat.Size = new System.Drawing.Size(469, 28);
            this.cmbToCat.TabIndex = 246;
            this.cmbToCat.SelectedIndexChanged += new System.EventHandler(this.cmbToCat_SelectedIndexChanged);
            // 
            // cmbTo
            // 
            this.cmbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbTo.FormattingEnabled = true;
            this.cmbTo.Location = new System.Drawing.Point(20, 76);
            this.cmbTo.Name = "cmbTo";
            this.cmbTo.Size = new System.Drawing.Size(469, 28);
            this.cmbTo.TabIndex = 247;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 121);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 17);
            this.label1.TabIndex = 248;
            this.label1.Text = "Set your default \'from\' and \'to\' locations here to apply to all delivery transact" +
    "ions.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(51, 385);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(461, 17);
            this.label2.TabIndex = 249;
            this.label2.Text = "If you want to change location for some cases, you can change it at the next step" +
    ".";
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
            // frmDOCompleteSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(604, 554);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFilterApply);
            this.Controls.Add(this.lblDeliveredDate);
            this.Controls.Add(this.dtpDeliveredDate);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDOCompleteSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DO Complete Settings";
            this.Load += new System.EventHandler(this.frmDeliveryDate_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtpDeliveredDate;
        private Guna.UI.WinForms.GunaGradientButton btnCancel;
        private Guna.UI.WinForms.GunaGradientButton btnFilterApply;
        private System.Windows.Forms.Label lblDeliveredDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbFrom;
        private System.Windows.Forms.ComboBox cmbToCat;
        private System.Windows.Forms.ComboBox cmbTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
    }
}