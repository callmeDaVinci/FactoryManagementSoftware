namespace FactoryManagementSoftware.UI
{
    partial class frmUserEdit
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
            this.lblPermissions = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbPermissions = new System.Windows.Forms.ComboBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbPasswordConfirm = new System.Windows.Forms.CheckBox();
            this.txtPasswordConfirm = new System.Windows.Forms.TextBox();
            this.lblPasswordConfirm = new System.Windows.Forms.Label();
            this.cbNewPass = new System.Windows.Forms.CheckBox();
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.cbOldPassword = new System.Windows.Forms.CheckBox();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPermissions
            // 
            this.lblPermissions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPermissions.AutoSize = true;
            this.lblPermissions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPermissions.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblPermissions.Location = new System.Drawing.Point(3, 12);
            this.lblPermissions.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblPermissions.Name = "lblPermissions";
            this.lblPermissions.Size = new System.Drawing.Size(80, 15);
            this.lblPermissions.TabIndex = 18;
            this.lblPermissions.Text = "PERMISSIONS";
            this.lblPermissions.Click += new System.EventHandler(this.lblPermissions_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(2, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 37);
            this.button1.TabIndex = 19;
            this.button1.TabStop = false;
            this.button1.Text = "CANCEL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbPermissions
            // 
            this.cmbPermissions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPermissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPermissions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPermissions.FormattingEnabled = true;
            this.cmbPermissions.Items.AddRange(new object[] {
            "Access Denied",
            "Read Only",
            "Stock Control Only",
            "Full Control",
            "Admin",
            "Super Admin"});
            this.cmbPermissions.Location = new System.Drawing.Point(3, 34);
            this.cmbPermissions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbPermissions.Name = "cmbPermissions";
            this.cmbPermissions.Size = new System.Drawing.Size(335, 28);
            this.cmbPermissions.TabIndex = 105;
            this.cmbPermissions.SelectedIndexChanged += new System.EventHandler(this.cmbPermissions_SelectedIndexChanged);
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblNewPassword.Location = new System.Drawing.Point(3, 6);
            this.lblNewPassword.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(97, 15);
            this.lblNewPassword.TabIndex = 16;
            this.lblNewPassword.Text = "NEW PASSWORD";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(106, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(233, 37);
            this.btnSave.TabIndex = 106;
            this.btnSave.Text = "SIGN UP";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsername.Location = new System.Drawing.Point(3, 34);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(335, 30);
            this.txtUsername.TabIndex = 101;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblUserName.Location = new System.Drawing.Point(3, 12);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(68, 15);
            this.lblUserName.TabIndex = 14;
            this.lblUserName.Text = "USERNAME";
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
            // cbPasswordConfirm
            // 
            this.cbPasswordConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPasswordConfirm.AutoSize = true;
            this.cbPasswordConfirm.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPasswordConfirm.ForeColor = System.Drawing.Color.DimGray;
            this.cbPasswordConfirm.Location = new System.Drawing.Point(275, 3);
            this.cbPasswordConfirm.Name = "cbPasswordConfirm";
            this.cbPasswordConfirm.Size = new System.Drawing.Size(57, 18);
            this.cbPasswordConfirm.TabIndex = 13;
            this.cbPasswordConfirm.TabStop = false;
            this.cbPasswordConfirm.Text = "Show";
            this.cbPasswordConfirm.UseVisualStyleBackColor = true;
            this.cbPasswordConfirm.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtPasswordConfirm
            // 
            this.txtPasswordConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPasswordConfirm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPasswordConfirm.Location = new System.Drawing.Point(3, 34);
            this.txtPasswordConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPasswordConfirm.Name = "txtPasswordConfirm";
            this.txtPasswordConfirm.PasswordChar = '*';
            this.txtPasswordConfirm.Size = new System.Drawing.Size(335, 30);
            this.txtPasswordConfirm.TabIndex = 104;
            // 
            // lblPasswordConfirm
            // 
            this.lblPasswordConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPasswordConfirm.AutoSize = true;
            this.lblPasswordConfirm.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblPasswordConfirm.Location = new System.Drawing.Point(3, 6);
            this.lblPasswordConfirm.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblPasswordConfirm.Name = "lblPasswordConfirm";
            this.lblPasswordConfirm.Size = new System.Drawing.Size(153, 15);
            this.lblPasswordConfirm.TabIndex = 17;
            this.lblPasswordConfirm.Text = "NEW PASSWORD CONFIRM";
            // 
            // cbNewPass
            // 
            this.cbNewPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbNewPass.AutoSize = true;
            this.cbNewPass.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNewPass.ForeColor = System.Drawing.Color.DimGray;
            this.cbNewPass.Location = new System.Drawing.Point(275, 3);
            this.cbNewPass.Name = "cbNewPass";
            this.cbNewPass.Size = new System.Drawing.Size(57, 18);
            this.cbNewPass.TabIndex = 12;
            this.cbNewPass.TabStop = false;
            this.cbNewPass.Text = "Show";
            this.cbNewPass.UseVisualStyleBackColor = true;
            this.cbNewPass.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // errorProvider4
            // 
            this.errorProvider4.ContainerControl = this;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblUserName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtUsername, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(341, 74);
            this.tableLayoutPanel1.TabIndex = 55;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.txtNewPassword, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 163);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(341, 74);
            this.tableLayoutPanel2.TabIndex = 99;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel3.Controls.Add(this.cbNewPass, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblNewPassword, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(335, 24);
            this.tableLayoutPanel3.TabIndex = 999;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtOldPassword, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 83);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(341, 74);
            this.tableLayoutPanel4.TabIndex = 57;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel5.Controls.Add(this.lblOldPassword, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbOldPassword, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(335, 24);
            this.tableLayoutPanel5.TabIndex = 57;
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblOldPassword.Location = new System.Drawing.Point(3, 6);
            this.lblOldPassword.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(94, 15);
            this.lblOldPassword.TabIndex = 15;
            this.lblOldPassword.Text = "OLD PASSWORD";
            // 
            // cbOldPassword
            // 
            this.cbOldPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOldPassword.AutoSize = true;
            this.cbOldPassword.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOldPassword.ForeColor = System.Drawing.Color.DimGray;
            this.cbOldPassword.Location = new System.Drawing.Point(275, 3);
            this.cbOldPassword.Name = "cbOldPassword";
            this.cbOldPassword.Size = new System.Drawing.Size(57, 18);
            this.cbOldPassword.TabIndex = 11;
            this.cbOldPassword.TabStop = false;
            this.cbOldPassword.Text = "Show";
            this.cbOldPassword.UseVisualStyleBackColor = true;
            this.cbOldPassword.CheckedChanged += new System.EventHandler(this.cbOldPassword_CheckedChanged);
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOldPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtOldPassword.Location = new System.Drawing.Point(3, 34);
            this.txtOldPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(335, 30);
            this.txtOldPassword.TabIndex = 102;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.txtPasswordConfirm, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 243);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(341, 74);
            this.tableLayoutPanel6.TabIndex = 58;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel7.Controls.Add(this.cbPasswordConfirm, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblPasswordConfirm, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(335, 24);
            this.tableLayoutPanel7.TabIndex = 57;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.cmbPermissions, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.lblPermissions, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 323);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(341, 74);
            this.tableLayoutPanel8.TabIndex = 59;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 3;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.tableLayoutPanel9.Controls.Add(this.btnSave, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 403);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(341, 62);
            this.tableLayoutPanel9.TabIndex = 60;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpMain.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tlpMain.Controls.Add(this.tableLayoutPanel9, 0, 5);
            this.tlpMain.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tlpMain.Controls.Add(this.tableLayoutPanel8, 0, 4);
            this.tlpMain.Controls.Add(this.tableLayoutPanel6, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(15, 15);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(15);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(347, 468);
            this.tlpMain.TabIndex = 61;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.tlpMain, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(377, 498);
            this.tableLayoutPanel10.TabIndex = 62;
            // 
            // errorProvider5
            // 
            this.errorProvider5.ContainerControl = this;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewPassword.Location = new System.Drawing.Point(3, 34);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(335, 30);
            this.txtNewPassword.TabIndex = 103;
            // 
            // frmUserEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(377, 498);
            this.Controls.Add(this.tableLayoutPanel10);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserEdit";
            this.Text = "NEW USER";
            this.Load += new System.EventHandler(this.frmUserEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPermissions;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbPermissions;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.CheckBox cbPasswordConfirm;
        private System.Windows.Forms.TextBox txtPasswordConfirm;
        private System.Windows.Forms.Label lblPasswordConfirm;
        private System.Windows.Forms.CheckBox cbNewPass;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.CheckBox cbOldPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.ErrorProvider errorProvider5;
        private System.Windows.Forms.TextBox txtNewPassword;
    }
}