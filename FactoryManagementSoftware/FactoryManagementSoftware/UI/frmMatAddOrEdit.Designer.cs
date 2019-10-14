namespace FactoryManagementSoftware.UI
{
    partial class frmMatAddOrEdit
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
            this.gbTest = new System.Windows.Forms.GroupBox();
            this.txtChildCode = new System.Windows.Forms.TextBox();
            this.txtParentCode = new System.Windows.Forms.TextBox();
            this.txtTestChildQty = new System.Windows.Forms.TextBox();
            this.txtTestParentQty = new System.Windows.Forms.TextBox();
            this.gbJoin = new System.Windows.Forms.GroupBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMatUseQty = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbChildCat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbChildName = new System.Windows.Forms.ComboBox();
            this.cmbChildCode = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTargetQty = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAblePro = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMac = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFac = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPlanID = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbParentCat = new System.Windows.Forms.ComboBox();
            this.cmbParentCode = new System.Windows.Forms.ComboBox();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.cmbParentName = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbEditJoin = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider6 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider7 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider8 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbTest.SuspendLayout();
            this.gbJoin.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTest
            // 
            this.gbTest.Controls.Add(this.txtChildCode);
            this.gbTest.Controls.Add(this.txtParentCode);
            this.gbTest.Controls.Add(this.txtTestChildQty);
            this.gbTest.Controls.Add(this.txtTestParentQty);
            this.gbTest.Enabled = false;
            this.gbTest.Location = new System.Drawing.Point(12, 614);
            this.gbTest.Name = "gbTest";
            this.gbTest.Size = new System.Drawing.Size(887, 114);
            this.gbTest.TabIndex = 86;
            this.gbTest.TabStop = false;
            this.gbTest.Text = "TEST";
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
            // 
            // gbJoin
            // 
            this.gbJoin.Controls.Add(this.txtMin);
            this.gbJoin.Controls.Add(this.txtMax);
            this.gbJoin.Controls.Add(this.label6);
            this.gbJoin.Controls.Add(this.label2);
            this.gbJoin.Controls.Add(this.txtQty);
            this.gbJoin.Controls.Add(this.label7);
            this.gbJoin.Enabled = false;
            this.gbJoin.Location = new System.Drawing.Point(12, 489);
            this.gbJoin.Name = "gbJoin";
            this.gbJoin.Size = new System.Drawing.Size(887, 110);
            this.gbJoin.TabIndex = 85;
            this.gbJoin.TabStop = false;
            this.gbJoin.Text = "JOIN";
            // 
            // txtMin
            // 
            this.txtMin.BackColor = System.Drawing.Color.White;
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
            this.txtMax.BackColor = System.Drawing.Color.White;
            this.txtMax.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMax.Location = new System.Drawing.Point(10, 55);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(188, 36);
            this.txtMax.TabIndex = 78;
            this.txtMax.Text = "1";
            this.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMax.TextChanged += new System.EventHandler(this.txtMax_TextChanged);
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
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.White;
            this.txtQty.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(457, 55);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(414, 36);
            this.txtQty.TabIndex = 72;
            this.txtQty.Text = "1";
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMatUseQty);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbChildCat);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbChildName);
            this.groupBox2.Controls.Add(this.cmbChildCode);
            this.groupBox2.Location = new System.Drawing.Point(463, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(436, 431);
            this.groupBox2.TabIndex = 84;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MATERIAL/CHILD";
            // 
            // txtMatUseQty
            // 
            this.txtMatUseQty.BackColor = System.Drawing.SystemColors.Info;
            this.txtMatUseQty.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatUseQty.Location = new System.Drawing.Point(6, 301);
            this.txtMatUseQty.Name = "txtMatUseQty";
            this.txtMatUseQty.Size = new System.Drawing.Size(414, 30);
            this.txtMatUseQty.TabIndex = 81;
            this.txtMatUseQty.Text = "1";
            this.txtMatUseQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMatUseQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox7_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(2, 279);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(245, 19);
            this.label15.TabIndex = 80;
            this.label15.Text = "*MATERIAL USE QUANTITY (KG/PIECE)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 64;
            this.label1.Text = "*CATEGORY";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 19);
            this.label3.TabIndex = 62;
            this.label3.Text = "*CODE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 19);
            this.label5.TabIndex = 63;
            this.label5.Text = "*NAME";
            // 
            // cmbChildName
            // 
            this.cmbChildName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbChildName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbChildName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChildName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbChildName.FormattingEnabled = true;
            this.cmbChildName.Location = new System.Drawing.Point(6, 145);
            this.cmbChildName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbChildName.Name = "cmbChildName";
            this.cmbChildName.Size = new System.Drawing.Size(414, 31);
            this.cmbChildName.TabIndex = 67;
            this.cmbChildName.SelectedIndexChanged += new System.EventHandler(this.cmbChildName_SelectedIndexChanged);
            // 
            // cmbChildCode
            // 
            this.cmbChildCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChildCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChildCode.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbChildCode.FormattingEnabled = true;
            this.cmbChildCode.Location = new System.Drawing.Point(6, 225);
            this.cmbChildCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbChildCode.Name = "cmbChildCode";
            this.cmbChildCode.Size = new System.Drawing.Size(414, 31);
            this.cmbChildCode.TabIndex = 68;
            this.cmbChildCode.SelectedIndexChanged += new System.EventHandler(this.cmbChildCode_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEnd);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtStart);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtTargetQty);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtAblePro);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtMac);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtFac);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmbPlanID);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbParentCat);
            this.groupBox1.Controls.Add(this.cmbParentCode);
            this.groupBox1.Controls.Add(this.lblItemCode);
            this.groupBox1.Controls.Add(this.lblItemName);
            this.groupBox1.Controls.Add(this.cmbParentName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 431);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PART/PARENT";
            // 
            // txtEnd
            // 
            this.txtEnd.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnd.Location = new System.Drawing.Point(223, 385);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(194, 25);
            this.txtEnd.TabIndex = 91;
            this.txtEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEnd_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(219, 363);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 19);
            this.label14.TabIndex = 90;
            this.label14.Text = "END";
            // 
            // txtStart
            // 
            this.txtStart.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStart.Location = new System.Drawing.Point(10, 385);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(194, 25);
            this.txtStart.TabIndex = 89;
            this.txtStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStart_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 363);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 19);
            this.label13.TabIndex = 88;
            this.label13.Text = "START";
            // 
            // txtTargetQty
            // 
            this.txtTargetQty.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTargetQty.Location = new System.Drawing.Point(331, 304);
            this.txtTargetQty.Name = "txtTargetQty";
            this.txtTargetQty.Size = new System.Drawing.Size(86, 25);
            this.txtTargetQty.TabIndex = 87;
            this.txtTargetQty.Text = "1";
            this.txtTargetQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTargetQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTargetQty_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(327, 282);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 19);
            this.label11.TabIndex = 86;
            this.label11.Text = "TARGET";
            // 
            // txtAblePro
            // 
            this.txtAblePro.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAblePro.Location = new System.Drawing.Point(223, 304);
            this.txtAblePro.Name = "txtAblePro";
            this.txtAblePro.Size = new System.Drawing.Size(86, 25);
            this.txtAblePro.TabIndex = 85;
            this.txtAblePro.Text = "1";
            this.txtAblePro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAblePro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAblePro_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(219, 282);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 19);
            this.label12.TabIndex = 84;
            this.label12.Text = "ABLE PRO.";
            // 
            // txtMac
            // 
            this.txtMac.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMac.Location = new System.Drawing.Point(158, 305);
            this.txtMac.Name = "txtMac";
            this.txtMac.Size = new System.Drawing.Size(46, 25);
            this.txtMac.TabIndex = 83;
            this.txtMac.Text = "1";
            this.txtMac.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMac.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMac_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(154, 282);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 19);
            this.label10.TabIndex = 82;
            this.label10.Text = "MAC.";
            // 
            // txtFac
            // 
            this.txtFac.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFac.Location = new System.Drawing.Point(106, 305);
            this.txtFac.Name = "txtFac";
            this.txtFac.Size = new System.Drawing.Size(46, 25);
            this.txtFac.TabIndex = 81;
            this.txtFac.Text = "1";
            this.txtFac.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFac.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFac_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(102, 282);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 19);
            this.label9.TabIndex = 80;
            this.label9.Text = "FAC.";
            // 
            // cmbPlanID
            // 
            this.cmbPlanID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlanID.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPlanID.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbPlanID.FormattingEnabled = true;
            this.cmbPlanID.Location = new System.Drawing.Point(10, 305);
            this.cmbPlanID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbPlanID.Name = "cmbPlanID";
            this.cmbPlanID.Size = new System.Drawing.Size(79, 31);
            this.cmbPlanID.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 282);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 19);
            this.label8.TabIndex = 62;
            this.label8.Text = "*PLAN ID";
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
            // cmbParentCode
            // 
            this.cmbParentCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParentCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParentCode.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbParentCode.FormattingEnabled = true;
            this.cmbParentCode.Location = new System.Drawing.Point(10, 225);
            this.cmbParentCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbParentCode.Name = "cmbParentCode";
            this.cmbParentCode.Size = new System.Drawing.Size(407, 31);
            this.cmbParentCode.TabIndex = 61;
            this.cmbParentCode.SelectedIndexChanged += new System.EventHandler(this.cmbParentCode_SelectedIndexChanged);
            // 
            // lblItemCode
            // 
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCode.Location = new System.Drawing.Point(6, 202);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(52, 19);
            this.lblItemCode.TabIndex = 42;
            this.lblItemCode.Text = "*CODE";
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemName.Location = new System.Drawing.Point(6, 122);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(54, 19);
            this.lblItemName.TabIndex = 44;
            this.lblItemName.Text = "*NAME";
            // 
            // cmbParentName
            // 
            this.cmbParentName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbParentName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbParentName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParentName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbParentName.FormattingEnabled = true;
            this.cmbParentName.Location = new System.Drawing.Point(10, 145);
            this.cmbParentName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbParentName.Name = "cmbParentName";
            this.cmbParentName.Size = new System.Drawing.Size(407, 31);
            this.cmbParentName.TabIndex = 60;
            this.cmbParentName.SelectedIndexChanged += new System.EventHandler(this.cmbParentName_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(637, 763);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 50);
            this.btnCancel.TabIndex = 82;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(761, 763);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 50);
            this.btnSave.TabIndex = 81;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbEditJoin
            // 
            this.cbEditJoin.AutoSize = true;
            this.cbEditJoin.Location = new System.Drawing.Point(12, 460);
            this.cbEditJoin.Name = "cbEditJoin";
            this.cbEditJoin.Size = new System.Drawing.Size(115, 23);
            this.cbEditJoin.TabIndex = 87;
            this.cbEditJoin.Text = "Edit Join Data";
            this.cbEditJoin.UseVisualStyleBackColor = true;
            this.cbEditJoin.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
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
            // errorProvider7
            // 
            this.errorProvider7.ContainerControl = this;
            // 
            // errorProvider8
            // 
            this.errorProvider8.ContainerControl = this;
            // 
            // frmMatAddOrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(917, 839);
            this.Controls.Add(this.cbEditJoin);
            this.Controls.Add(this.gbTest);
            this.Controls.Add(this.gbJoin);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMatAddOrEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Material Edit";
            this.Load += new System.EventHandler(this.frmMatAddOrEdit_Load);
            this.gbTest.ResumeLayout(false);
            this.gbTest.PerformLayout();
            this.gbJoin.ResumeLayout(false);
            this.gbJoin.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTest;
        private System.Windows.Forms.TextBox txtChildCode;
        private System.Windows.Forms.TextBox txtParentCode;
        private System.Windows.Forms.TextBox txtTestChildQty;
        private System.Windows.Forms.TextBox txtTestParentQty;
        private System.Windows.Forms.GroupBox gbJoin;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbChildCat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbChildName;
        private System.Windows.Forms.ComboBox cmbChildCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbParentCat;
        private System.Windows.Forms.ComboBox cmbParentCode;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.ComboBox cmbParentName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTargetQty;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAblePro;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMac;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFac;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbPlanID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbEditJoin;
        private System.Windows.Forms.TextBox txtMatUseQty;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;
        private System.Windows.Forms.ErrorProvider errorProvider6;
        private System.Windows.Forms.ErrorProvider errorProvider7;
        private System.Windows.Forms.ErrorProvider errorProvider8;
    }
}