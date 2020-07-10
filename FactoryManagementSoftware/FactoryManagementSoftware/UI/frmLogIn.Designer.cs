namespace FactoryManagementSoftware.UI
{
    partial class frmLogIn
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbtnLogin = new Guna.UI.WinForms.GunaGradientButton();
            this.gtxtUsername = new Guna.UI.WinForms.GunaTextBox();
            this.gtxtPassword = new Guna.UI.WinForms.GunaTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.lblDate = new System.Windows.Forms.Label();
            this.gunaPictureBox1 = new Guna.UI.WinForms.GunaPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.gunaPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gunaPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(176)))), ((int)(((byte)(242)))));
            this.label1.Location = new System.Drawing.Point(295, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(176)))), ((int)(((byte)(242)))));
            this.label2.Location = new System.Drawing.Point(295, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // gbtnLogin
            // 
            this.gbtnLogin.AnimationHoverSpeed = 0.07F;
            this.gbtnLogin.AnimationSpeed = 0.03F;
            this.gbtnLogin.BackColor = System.Drawing.Color.Transparent;
            this.gbtnLogin.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(36)))), ((int)(((byte)(177)))));
            this.gbtnLogin.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(54)))), ((int)(((byte)(252)))));
            this.gbtnLogin.BorderColor = System.Drawing.Color.Black;
            this.gbtnLogin.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gbtnLogin.FocusedColor = System.Drawing.Color.Empty;
            this.gbtnLogin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gbtnLogin.ForeColor = System.Drawing.Color.White;
            this.gbtnLogin.Image = null;
            this.gbtnLogin.ImageSize = new System.Drawing.Size(20, 20);
            this.gbtnLogin.Location = new System.Drawing.Point(299, 332);
            this.gbtnLogin.Name = "gbtnLogin";
            this.gbtnLogin.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.gbtnLogin.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.gbtnLogin.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gbtnLogin.OnHoverForeColor = System.Drawing.Color.White;
            this.gbtnLogin.OnHoverImage = null;
            this.gbtnLogin.OnPressedColor = System.Drawing.Color.Black;
            this.gbtnLogin.Radius = 2;
            this.gbtnLogin.Size = new System.Drawing.Size(248, 37);
            this.gbtnLogin.TabIndex = 8;
            this.gbtnLogin.Text = "LOGIN";
            this.gbtnLogin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gbtnLogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // gtxtUsername
            // 
            this.gtxtUsername.BackColor = System.Drawing.Color.Transparent;
            this.gtxtUsername.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(41)))), ((int)(((byte)(86)))));
            this.gtxtUsername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(50)))), ((int)(((byte)(101)))));
            this.gtxtUsername.BorderSize = 1;
            this.gtxtUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gtxtUsername.FocusedBaseColor = System.Drawing.Color.White;
            this.gtxtUsername.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gtxtUsername.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.gtxtUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gtxtUsername.ForeColor = System.Drawing.Color.White;
            this.gtxtUsername.Location = new System.Drawing.Point(299, 160);
            this.gtxtUsername.Name = "gtxtUsername";
            this.gtxtUsername.PasswordChar = '\0';
            this.gtxtUsername.Radius = 2;
            this.gtxtUsername.SelectedText = "";
            this.gtxtUsername.Size = new System.Drawing.Size(248, 37);
            this.gtxtUsername.TabIndex = 6;
            this.gtxtUsername.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.gtxtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // gtxtPassword
            // 
            this.gtxtPassword.BackColor = System.Drawing.Color.Transparent;
            this.gtxtPassword.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(41)))), ((int)(((byte)(86)))));
            this.gtxtPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(50)))), ((int)(((byte)(101)))));
            this.gtxtPassword.BorderSize = 1;
            this.gtxtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gtxtPassword.FocusedBaseColor = System.Drawing.Color.White;
            this.gtxtPassword.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gtxtPassword.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.gtxtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gtxtPassword.ForeColor = System.Drawing.Color.White;
            this.gtxtPassword.Location = new System.Drawing.Point(299, 241);
            this.gtxtPassword.Name = "gtxtPassword";
            this.gtxtPassword.PasswordChar = '*';
            this.gtxtPassword.Radius = 2;
            this.gtxtPassword.SelectedText = "";
            this.gtxtPassword.Size = new System.Drawing.Size(248, 37);
            this.gtxtPassword.TabIndex = 7;
            this.gtxtPassword.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.gtxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(293, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 35);
            this.label3.TabIndex = 10;
            this.label3.Text = "Welcome Back :)";
            // 
            // gunaPanel1
            // 
            this.gunaPanel1.Controls.Add(this.lblDate);
            this.gunaPanel1.Controls.Add(this.label3);
            this.gunaPanel1.Controls.Add(this.gunaPictureBox1);
            this.gunaPanel1.Controls.Add(this.gtxtPassword);
            this.gunaPanel1.Controls.Add(this.gtxtUsername);
            this.gunaPanel1.Controls.Add(this.gbtnLogin);
            this.gunaPanel1.Controls.Add(this.label2);
            this.gunaPanel1.Controls.Add(this.label1);
            this.gunaPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaPanel1.Location = new System.Drawing.Point(0, 0);
            this.gunaPanel1.Name = "gunaPanel1";
            this.gunaPanel1.Size = new System.Drawing.Size(846, 529);
            this.gunaPanel1.TabIndex = 9;
            this.gunaPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.gunaPanel1_Paint);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(176)))), ((int)(((byte)(242)))));
            this.lblDate.Location = new System.Drawing.Point(295, 84);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(75, 20);
            this.lblDate.TabIndex = 11;
            this.lblDate.Text = "Username";
            this.lblDate.Visible = false;
            // 
            // gunaPictureBox1
            // 
            this.gunaPictureBox1.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.Picture12;
            this.gunaPictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gunaPictureBox1.BaseColor = System.Drawing.Color.White;
            this.gunaPictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gunaPictureBox1.Location = new System.Drawing.Point(0, 423);
            this.gunaPictureBox1.Name = "gunaPictureBox1";
            this.gunaPictureBox1.Size = new System.Drawing.Size(846, 106);
            this.gunaPictureBox1.TabIndex = 9;
            this.gunaPictureBox1.TabStop = false;
            // 
            // frmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(74)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(846, 529);
            this.Controls.Add(this.gunaPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Management System";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLogIn_FormClosed);
            this.Load += new System.EventHandler(this.frmLogIn_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLogIn_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.gunaPanel1.ResumeLayout(false);
            this.gunaPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gunaPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private Guna.UI.WinForms.GunaPanel gunaPanel1;
        private System.Windows.Forms.Label label3;
        private Guna.UI.WinForms.GunaPictureBox gunaPictureBox1;
        private Guna.UI.WinForms.GunaTextBox gtxtPassword;
        private Guna.UI.WinForms.GunaTextBox gtxtUsername;
        private Guna.UI.WinForms.GunaGradientButton gbtnLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDate;
    }
}