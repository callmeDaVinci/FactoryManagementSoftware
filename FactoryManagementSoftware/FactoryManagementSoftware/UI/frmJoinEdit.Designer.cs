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
            this.cmbChildCode = new System.Windows.Forms.ComboBox();
            this.cmbChildName = new System.Windows.Forms.ComboBox();
            this.cmbChildCat = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtChildQty = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider6 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtChildCode = new System.Windows.Forms.TextBox();
            this.txtParentCode = new System.Windows.Forms.TextBox();
            this.txtTestChildQty = new System.Windows.Forms.TextBox();
            this.txtTestParentQty = new System.Windows.Forms.TextBox();
            this.errorProvider7 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider8 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(640, 591);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 50);
            this.btnCancel.TabIndex = 59;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 19);
            this.label4.TabIndex = 56;
            this.label4.Text = "*CATEGORY";
            // 
            // cmbParentCat
            // 
            this.cmbParentCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParentCat.Enabled = false;
            this.cmbParentCat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParentCat.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbParentCat.FormattingEnabled = true;
            this.cmbParentCat.Items.AddRange(new object[] {
            "Part"});
            this.cmbParentCat.Location = new System.Drawing.Point(10, 65);
            this.cmbParentCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbParentCat.Name = "cmbParentCat";
            this.cmbParentCat.Size = new System.Drawing.Size(407, 31);
            this.cmbParentCat.TabIndex = 47;
            this.cmbParentCat.SelectedIndexChanged += new System.EventHandler(this.cmbParentCat_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(764, 591);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 50);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemName.Location = new System.Drawing.Point(4, 126);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(54, 19);
            this.lblItemName.TabIndex = 44;
            this.lblItemName.Text = "*NAME";
            this.lblItemName.Click += new System.EventHandler(this.lblItemName_Click);
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCode.Location = new System.Drawing.Point(6, 205);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(52, 19);
            this.lblItemCode.TabIndex = 42;
            this.lblItemCode.Text = "*CODE";
            this.lblItemCode.Click += new System.EventHandler(this.lblItemCode_Click);
            // 
            // cmbParentName
            // 
            this.cmbParentName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbParentName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbParentName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParentName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbParentName.FormattingEnabled = true;
            this.cmbParentName.Location = new System.Drawing.Point(10, 149);
            this.cmbParentName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbParentName.Name = "cmbParentName";
            this.cmbParentName.Size = new System.Drawing.Size(407, 31);
            this.cmbParentName.TabIndex = 60;
            this.cmbParentName.SelectedIndexChanged += new System.EventHandler(this.cmbParentName_SelectedIndexChanged);
            // 
            // cmbParentCode
            // 
            this.cmbParentCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParentCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParentCode.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbParentCode.FormattingEnabled = true;
            this.cmbParentCode.Location = new System.Drawing.Point(10, 228);
            this.cmbParentCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbParentCode.Name = "cmbParentCode";
            this.cmbParentCode.Size = new System.Drawing.Size(407, 31);
            this.cmbParentCode.TabIndex = 61;
            this.cmbParentCode.SelectedIndexChanged += new System.EventHandler(this.cmbParentCode_SelectedIndexChanged);
            // 
            // cmbChildCode
            // 
            this.cmbChildCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChildCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChildCode.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbChildCode.FormattingEnabled = true;
            this.cmbChildCode.Location = new System.Drawing.Point(6, 228);
            this.cmbChildCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbChildCode.Name = "cmbChildCode";
            this.cmbChildCode.Size = new System.Drawing.Size(414, 31);
            this.cmbChildCode.TabIndex = 68;
            this.cmbChildCode.SelectedIndexChanged += new System.EventHandler(this.cmbChildCode_SelectedIndexChanged);
            // 
            // cmbChildName
            // 
            this.cmbChildName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbChildName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbChildName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChildName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbChildName.FormattingEnabled = true;
            this.cmbChildName.Location = new System.Drawing.Point(6, 149);
            this.cmbChildName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbChildName.Name = "cmbChildName";
            this.cmbChildName.Size = new System.Drawing.Size(414, 31);
            this.cmbChildName.TabIndex = 67;
            this.cmbChildName.SelectedIndexChanged += new System.EventHandler(this.cmbChildName_SelectedIndexChanged);
            // 
            // cmbChildCat
            // 
            this.cmbChildCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChildCat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChildCat.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbChildCat.FormattingEnabled = true;
            this.cmbChildCat.Location = new System.Drawing.Point(6, 65);
            this.cmbChildCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbChildCat.Name = "cmbChildCat";
            this.cmbChildCat.Size = new System.Drawing.Size(414, 31);
            this.cmbChildCat.TabIndex = 65;
            this.cmbChildCat.SelectedIndexChanged += new System.EventHandler(this.cmbChildCat_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(457, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 19);
            this.label7.TabIndex = 70;
            this.label7.Text = "CHILD QTY";
            // 
            // txtChildQty
            // 
            this.txtChildQty.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChildQty.Location = new System.Drawing.Point(457, 55);
            this.txtChildQty.Name = "txtChildQty";
            this.txtChildQty.Size = new System.Drawing.Size(414, 36);
            this.txtChildQty.TabIndex = 72;
            this.txtChildQty.Text = "1";
            this.txtChildQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtChildQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbParentCat);
            this.groupBox1.Controls.Add(this.cmbParentCode);
            this.groupBox1.Controls.Add(this.lblItemCode);
            this.groupBox1.Controls.Add(this.lblItemName);
            this.groupBox1.Controls.Add(this.cmbParentName);
            this.groupBox1.Location = new System.Drawing.Point(15, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 294);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PARENT";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbChildCat);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbChildName);
            this.groupBox2.Controls.Add(this.cmbChildCode);
            this.groupBox2.Location = new System.Drawing.Point(466, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(436, 294);
            this.groupBox2.TabIndex = 74;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CHILD";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 64;
            this.label1.Text = "*CATEGORY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 19);
            this.label3.TabIndex = 62;
            this.label3.Text = "*CODE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 19);
            this.label5.TabIndex = 63;
            this.label5.Text = "*NAME";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtMin);
            this.groupBox3.Controls.Add(this.txtMax);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtChildQty);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(15, 322);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(887, 110);
            this.groupBox3.TabIndex = 75;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "JOIN";
            // 
            // txtMin
            // 
            this.txtMin.Enabled = false;
            this.txtMin.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMin.Location = new System.Drawing.Point(223, 55);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(194, 36);
            this.txtMin.TabIndex = 79;
            this.txtMin.Text = "1";
            this.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMin.TextChanged += new System.EventHandler(this.txtMin_TextChanged);
            this.txtMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMin_KeyPress);
            // 
            // txtMax
            // 
            this.txtMax.Enabled = false;
            this.txtMax.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMax.Location = new System.Drawing.Point(10, 55);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(188, 36);
            this.txtMax.TabIndex = 78;
            this.txtMax.Text = "1";
            this.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMax.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMax_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(219, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 19);
            this.label6.TabIndex = 75;
            this.label6.Text = "PARENT QTY (MIN)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 19);
            this.label2.TabIndex = 73;
            this.label2.Text = "PARENT QTY (MAX)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtChildCode);
            this.groupBox4.Controls.Add(this.txtParentCode);
            this.groupBox4.Controls.Add(this.txtTestChildQty);
            this.groupBox4.Controls.Add(this.txtTestParentQty);
            this.groupBox4.Location = new System.Drawing.Point(15, 447);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(887, 114);
            this.groupBox4.TabIndex = 80;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "TEST";
            // 
            // txtChildCode
            // 
            this.txtChildCode.Enabled = false;
            this.txtChildCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChildCode.Location = new System.Drawing.Point(536, 34);
            this.txtChildCode.Multiline = true;
            this.txtChildCode.Name = "txtChildCode";
            this.txtChildCode.Size = new System.Drawing.Size(335, 63);
            this.txtChildCode.TabIndex = 82;
            this.txtChildCode.Text = "CHILD(CODE)";
            // 
            // txtParentCode
            // 
            this.txtParentCode.Enabled = false;
            this.txtParentCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParentCode.Location = new System.Drawing.Point(95, 34);
            this.txtParentCode.Multiline = true;
            this.txtParentCode.Name = "txtParentCode";
            this.txtParentCode.Size = new System.Drawing.Size(338, 63);
            this.txtParentCode.TabIndex = 80;
            this.txtParentCode.Text = "PARENT(CODE)";
            // 
            // txtTestChildQty
            // 
            this.txtTestChildQty.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTestChildQty.Location = new System.Drawing.Point(451, 34);
            this.txtTestChildQty.Name = "txtTestChildQty";
            this.txtTestChildQty.Size = new System.Drawing.Size(79, 36);
            this.txtTestChildQty.TabIndex = 81;
            this.txtTestChildQty.Text = "1";
            this.txtTestChildQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTestChildQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTestChildQty_KeyPress);
            // 
            // txtTestParentQty
            // 
            this.txtTestParentQty.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTestParentQty.Location = new System.Drawing.Point(10, 34);
            this.txtTestParentQty.Name = "txtTestParentQty";
            this.txtTestParentQty.Size = new System.Drawing.Size(79, 36);
            this.txtTestParentQty.TabIndex = 72;
            this.txtTestParentQty.Text = "1";
            this.txtTestParentQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTestParentQty.TextChanged += new System.EventHandler(this.txtTestParentQty_TextChanged);
            this.txtTestParentQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTestParentQty_KeyPress);
            // 
            // errorProvider7
            // 
            this.errorProvider7.ContainerControl = this;
            // 
            // errorProvider8
            // 
            this.errorProvider8.ContainerControl = this;
            // 
            // frmJoinEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(917, 667);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmJoinEdit";
            this.ShowIcon = false;
            this.Text = "JOIN EDIT";
            this.Load += new System.EventHandler(this.frmJoinEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ComboBox cmbChildCode;
        private System.Windows.Forms.ComboBox cmbChildName;
        private System.Windows.Forms.ComboBox cmbChildCat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtChildQty;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;
        private System.Windows.Forms.ErrorProvider errorProvider6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtTestChildQty;
        private System.Windows.Forms.TextBox txtTestParentQty;
        private System.Windows.Forms.TextBox txtChildCode;
        private System.Windows.Forms.TextBox txtParentCode;
        private System.Windows.Forms.ErrorProvider errorProvider7;
        private System.Windows.Forms.ErrorProvider errorProvider8;
    }
}