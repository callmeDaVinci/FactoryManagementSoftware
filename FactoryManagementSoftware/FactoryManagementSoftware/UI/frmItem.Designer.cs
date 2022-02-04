namespace FactoryManagementSoftware
{
    partial class frmItem
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtItemSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cmbCat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCust = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMat = new System.Windows.Forms.ComboBox();
            this.cmbMB = new System.Windows.Forms.ComboBox();
            this.txtPWpcs = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRWpcs = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRWshot = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPWshot = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnFastAdd = new System.Windows.Forms.Button();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbAssembly = new System.Windows.Forms.CheckBox();
            this.cbPigment = new System.Windows.Forms.CheckBox();
            this.cbMB = new System.Windows.Forms.CheckBox();
            this.btnJoin = new System.Windows.Forms.Button();
            this.lblParentCode = new System.Windows.Forms.Label();
            this.txtJoinQty = new System.Windows.Forms.TextBox();
            this.lblJoinQty = new System.Windows.Forms.Label();
            this.txtQuoRWpcs = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQuoPWpcs = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbProduction = new System.Windows.Forms.CheckBox();
            this.cbZeroCost = new System.Windows.Forms.CheckBox();
            this.txtPCSRate = new System.Windows.Forms.TextBox();
            this.lblPcsRate = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtItemSearch
            // 
            this.txtItemSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemSearch.Location = new System.Drawing.Point(410, 60);
            this.txtItemSearch.Name = "txtItemSearch";
            this.txtItemSearch.Size = new System.Drawing.Size(363, 34);
            this.txtItemSearch.TabIndex = 9;
            this.txtItemSearch.TextChanged += new System.EventHandler(this.txtItemSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(406, 34);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(72, 23);
            this.lblSearch.TabIndex = 8;
            this.lblSearch.Text = "SEARCH";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(239)))));
            this.btnAdd.Location = new System.Drawing.Point(1326, 42);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(122, 52);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnDelete.Location = new System.Drawing.Point(1608, 42);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 50);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnUpdate.Location = new System.Drawing.Point(1469, 42);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 50);
            this.btnUpdate.TabIndex = 22;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cmbCat
            // 
            this.cmbCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCat.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbCat.FormattingEnabled = true;
            this.cmbCat.Location = new System.Drawing.Point(27, 58);
            this.cmbCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbCat.Name = "cmbCat";
            this.cmbCat.Size = new System.Drawing.Size(363, 36);
            this.cmbCat.TabIndex = 28;
            this.cmbCat.SelectedIndexChanged += new System.EventHandler(this.cmbCat_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 23);
            this.label1.TabIndex = 29;
            this.label1.Text = "CATEGORY";
            // 
            // dgvItemList
            // 
            this.dgvItemList.AllowUserToAddRows = false;
            this.dgvItemList.AllowUserToDeleteRows = false;
            this.dgvItemList.AllowUserToResizeRows = false;
            this.dgvItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemList.BackgroundColor = System.Drawing.Color.White;
            this.dgvItemList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList.GridColor = System.Drawing.SystemColors.Control;
            this.dgvItemList.Location = new System.Drawing.Point(27, 313);
            this.dgvItemList.Margin = new System.Windows.Forms.Padding(2);
            this.dgvItemList.MultiSelect = false;
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.ReadOnly = true;
            this.dgvItemList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvItemList.RowHeadersVisible = false;
            this.dgvItemList.RowTemplate.Height = 40;
            this.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemList.Size = new System.Drawing.Size(1701, 368);
            this.dgvItemList.TabIndex = 30;
            this.dgvItemList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellContentClick);
            this.dgvItemList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 23);
            this.label2.TabIndex = 32;
            this.label2.Text = "CUSTOMER";
            // 
            // cmbCust
            // 
            this.cmbCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCust.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCust.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbCust.FormattingEnabled = true;
            this.cmbCust.Location = new System.Drawing.Point(27, 158);
            this.cmbCust.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbCust.Name = "cmbCust";
            this.cmbCust.Size = new System.Drawing.Size(363, 36);
            this.cmbCust.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(406, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 23);
            this.label3.TabIndex = 34;
            this.label3.Text = "ITEM CODE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(803, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 23);
            this.label4.TabIndex = 36;
            this.label4.Text = "ITEM NAME";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 23);
            this.label5.TabIndex = 38;
            this.label5.Text = "MATERIAL";
            // 
            // cmbMat
            // 
            this.cmbMat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMat.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbMat.FormattingEnabled = true;
            this.cmbMat.Location = new System.Drawing.Point(27, 236);
            this.cmbMat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbMat.Name = "cmbMat";
            this.cmbMat.Size = new System.Drawing.Size(363, 36);
            this.cmbMat.TabIndex = 37;
            // 
            // cmbMB
            // 
            this.cmbMB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMB.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbMB.FormattingEnabled = true;
            this.cmbMB.Location = new System.Drawing.Point(410, 236);
            this.cmbMB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbMB.Name = "cmbMB";
            this.cmbMB.Size = new System.Drawing.Size(363, 36);
            this.cmbMB.TabIndex = 39;
            // 
            // txtPWpcs
            // 
            this.txtPWpcs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPWpcs.Location = new System.Drawing.Point(1199, 238);
            this.txtPWpcs.Name = "txtPWpcs";
            this.txtPWpcs.Size = new System.Drawing.Size(64, 34);
            this.txtPWpcs.TabIndex = 42;
            this.txtPWpcs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPWpcs_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label7.Location = new System.Drawing.Point(1195, 214);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 19);
            this.label7.TabIndex = 41;
            this.label7.Text = "PW(PCS)";
            // 
            // txtRWpcs
            // 
            this.txtRWpcs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRWpcs.Location = new System.Drawing.Point(1269, 238);
            this.txtRWpcs.Name = "txtRWpcs";
            this.txtRWpcs.Size = new System.Drawing.Size(58, 34);
            this.txtRWpcs.TabIndex = 44;
            this.txtRWpcs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRWpcs_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label8.Location = new System.Drawing.Point(1265, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 19);
            this.label8.TabIndex = 43;
            this.label8.Text = "RW(PCS)";
            // 
            // txtRWshot
            // 
            this.txtRWshot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRWshot.Location = new System.Drawing.Point(1407, 238);
            this.txtRWshot.Name = "txtRWshot";
            this.txtRWshot.Size = new System.Drawing.Size(68, 34);
            this.txtRWshot.TabIndex = 48;
            this.txtRWshot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRWshot_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label9.Location = new System.Drawing.Point(1403, 212);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 19);
            this.label9.TabIndex = 47;
            this.label9.Text = "RW(SHOT)";
            // 
            // txtPWshot
            // 
            this.txtPWshot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPWshot.Location = new System.Drawing.Point(1333, 238);
            this.txtPWshot.Name = "txtPWshot";
            this.txtPWshot.Size = new System.Drawing.Size(68, 34);
            this.txtPWshot.TabIndex = 46;
            this.txtPWshot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPWshot_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label10.Location = new System.Drawing.Point(1329, 212);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 19);
            this.label10.TabIndex = 45;
            this.label10.Text = "PW(SHOT)";
            // 
            // txtCapacity
            // 
            this.txtCapacity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapacity.Location = new System.Drawing.Point(1481, 238);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(68, 34);
            this.txtCapacity.TabIndex = 50;
            this.txtCapacity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapacity_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label11.Location = new System.Drawing.Point(1477, 212);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 19);
            this.label11.TabIndex = 49;
            this.label11.Text = "CAPACITY";
            // 
            // btnFastAdd
            // 
            this.btnFastAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFastAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFastAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFastAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(239)))));
            this.btnFastAdd.Location = new System.Drawing.Point(1321, 123);
            this.btnFastAdd.Name = "btnFastAdd";
            this.btnFastAdd.Size = new System.Drawing.Size(89, 71);
            this.btnFastAdd.TabIndex = 51;
            this.btnFastAdd.Text = "FAST ADD";
            this.btnFastAdd.UseVisualStyleBackColor = false;
            this.btnFastAdd.Click += new System.EventHandler(this.btnFastAdd_Click);
            // 
            // txtItemCode
            // 
            this.txtItemCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemCode.Location = new System.Drawing.Point(410, 157);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(363, 34);
            this.txtItemCode.TabIndex = 52;
            // 
            // txtItemName
            // 
            this.txtItemName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemName.Location = new System.Drawing.Point(807, 157);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(269, 34);
            this.txtItemName.TabIndex = 53;
            // 
            // txtColor
            // 
            this.txtColor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColor.Location = new System.Drawing.Point(1104, 157);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(89, 34);
            this.txtColor.TabIndex = 55;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1100, 131);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 23);
            this.label12.TabIndex = 54;
            this.label12.Text = "COLOR";
            // 
            // cbAssembly
            // 
            this.cbAssembly.AutoSize = true;
            this.cbAssembly.Checked = true;
            this.cbAssembly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAssembly.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAssembly.Location = new System.Drawing.Point(1555, 131);
            this.cbAssembly.Name = "cbAssembly";
            this.cbAssembly.Size = new System.Drawing.Size(164, 27);
            this.cbAssembly.TabIndex = 73;
            this.cbAssembly.Text = "ASSEMBLY SET";
            this.cbAssembly.UseVisualStyleBackColor = true;
            // 
            // cbPigment
            // 
            this.cbPigment.AutoSize = true;
            this.cbPigment.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPigment.Location = new System.Drawing.Point(562, 212);
            this.cbPigment.Name = "cbPigment";
            this.cbPigment.Size = new System.Drawing.Size(86, 21);
            this.cbPigment.TabIndex = 76;
            this.cbPigment.Text = "PIGMENT";
            this.cbPigment.UseVisualStyleBackColor = true;
            this.cbPigment.CheckedChanged += new System.EventHandler(this.cbPigment_CheckedChanged);
            // 
            // cbMB
            // 
            this.cbMB.AutoSize = true;
            this.cbMB.Checked = true;
            this.cbMB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMB.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMB.Location = new System.Drawing.Point(410, 212);
            this.cbMB.Name = "cbMB";
            this.cbMB.Size = new System.Drawing.Size(126, 21);
            this.cbMB.TabIndex = 75;
            this.cbMB.Text = "MASTER BATCH";
            this.cbMB.UseVisualStyleBackColor = true;
            this.cbMB.CheckedChanged += new System.EventHandler(this.cbMB_CheckedChanged);
            // 
            // btnJoin
            // 
            this.btnJoin.BackColor = System.Drawing.Color.White;
            this.btnJoin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoin.ForeColor = System.Drawing.Color.Black;
            this.btnJoin.Location = new System.Drawing.Point(1421, 123);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(128, 71);
            this.btnJoin.TabIndex = 77;
            this.btnJoin.Text = "ADD JOIN";
            this.btnJoin.UseVisualStyleBackColor = false;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // lblParentCode
            // 
            this.lblParentCode.AutoSize = true;
            this.lblParentCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParentCode.Location = new System.Drawing.Point(510, 135);
            this.lblParentCode.Name = "lblParentCode";
            this.lblParentCode.Size = new System.Drawing.Size(72, 19);
            this.lblParentCode.TabIndex = 78;
            this.lblParentCode.Text = "(PARENT: )";
            // 
            // txtJoinQty
            // 
            this.txtJoinQty.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJoinQty.Location = new System.Drawing.Point(1223, 155);
            this.txtJoinQty.Name = "txtJoinQty";
            this.txtJoinQty.Size = new System.Drawing.Size(56, 36);
            this.txtJoinQty.TabIndex = 80;
            this.txtJoinQty.Visible = false;
            // 
            // lblJoinQty
            // 
            this.lblJoinQty.AutoSize = true;
            this.lblJoinQty.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJoinQty.Location = new System.Drawing.Point(1219, 128);
            this.lblJoinQty.Name = "lblJoinQty";
            this.lblJoinQty.Size = new System.Drawing.Size(37, 23);
            this.lblJoinQty.TabIndex = 79;
            this.lblJoinQty.Text = "Qty";
            this.lblJoinQty.Visible = false;
            // 
            // txtQuoRWpcs
            // 
            this.txtQuoRWpcs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuoRWpcs.Location = new System.Drawing.Point(1104, 238);
            this.txtQuoRWpcs.Name = "txtQuoRWpcs";
            this.txtQuoRWpcs.Size = new System.Drawing.Size(89, 34);
            this.txtQuoRWpcs.TabIndex = 84;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1104, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 19);
            this.label6.TabIndex = 83;
            this.label6.Text = "QuoRW(PCS)";
            // 
            // txtQuoPWpcs
            // 
            this.txtQuoPWpcs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuoPWpcs.Location = new System.Drawing.Point(1013, 238);
            this.txtQuoPWpcs.Name = "txtQuoPWpcs";
            this.txtQuoPWpcs.Size = new System.Drawing.Size(53, 34);
            this.txtQuoPWpcs.TabIndex = 82;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1009, 212);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 19);
            this.label13.TabIndex = 81;
            this.label13.Text = "QuoPW(PCS)";
            // 
            // cbProduction
            // 
            this.cbProduction.AutoSize = true;
            this.cbProduction.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProduction.Location = new System.Drawing.Point(1555, 163);
            this.cbProduction.Name = "cbProduction";
            this.cbProduction.Size = new System.Drawing.Size(197, 27);
            this.cbProduction.TabIndex = 85;
            this.cbProduction.Text = "PRODUCTION ITEM";
            this.cbProduction.UseVisualStyleBackColor = true;
            // 
            // cbZeroCost
            // 
            this.cbZeroCost.AutoSize = true;
            this.cbZeroCost.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbZeroCost.Location = new System.Drawing.Point(1597, 196);
            this.cbZeroCost.Name = "cbZeroCost";
            this.cbZeroCost.Size = new System.Drawing.Size(131, 27);
            this.cbZeroCost.TabIndex = 86;
            this.cbZeroCost.Text = "ZERO COST";
            this.cbZeroCost.UseVisualStyleBackColor = true;
            this.cbZeroCost.Visible = false;
            // 
            // txtPCSRate
            // 
            this.txtPCSRate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPCSRate.Location = new System.Drawing.Point(895, 238);
            this.txtPCSRate.Name = "txtPCSRate";
            this.txtPCSRate.Size = new System.Drawing.Size(81, 34);
            this.txtPCSRate.TabIndex = 90;
            // 
            // lblPcsRate
            // 
            this.lblPcsRate.AutoSize = true;
            this.lblPcsRate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPcsRate.Location = new System.Drawing.Point(891, 216);
            this.lblPcsRate.Name = "lblPcsRate";
            this.lblPcsRate.Size = new System.Drawing.Size(67, 19);
            this.lblPcsRate.TabIndex = 89;
            this.lblPcsRate.Text = "PCS RATE";
            // 
            // txtUnit
            // 
            this.txtUnit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnit.Location = new System.Drawing.Point(807, 238);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(82, 34);
            this.txtUnit.TabIndex = 88;
            this.txtUnit.Text = "piece";
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblUnit.Location = new System.Drawing.Point(803, 216);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(40, 19);
            this.lblUnit.TabIndex = 87;
            this.lblUnit.Text = "UNIT";
            // 
            // frmItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1761, 703);
            this.Controls.Add(this.txtPCSRate);
            this.Controls.Add(this.lblPcsRate);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.cbZeroCost);
            this.Controls.Add(this.cbProduction);
            this.Controls.Add(this.txtQuoRWpcs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtQuoPWpcs);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtJoinQty);
            this.Controls.Add(this.lblJoinQty);
            this.Controls.Add(this.lblParentCode);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.cbPigment);
            this.Controls.Add(this.cbMB);
            this.Controls.Add(this.cbAssembly);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.btnFastAdd);
            this.Controls.Add(this.txtCapacity);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRWshot);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPWshot);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtRWpcs);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPWpcs);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbMB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbMat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCust);
            this.Controls.Add(this.dgvItemList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCat);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtItemSearch);
            this.Controls.Add(this.lblSearch);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmItem_FormClosed);
            this.Load += new System.EventHandler(this.frmItem_Load);
            this.Click += new System.EventHandler(this.frmItem_Click);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtItemSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cmbCat;
        private System.Windows.Forms.DataGridView dgvItemList;
        private System.Windows.Forms.Button btnFastAdd;
        private System.Windows.Forms.TextBox txtCapacity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRWshot;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPWshot;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRWpcs;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPWpcs;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cmbMB;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cmbMat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cmbCust;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbAssembly;
        private System.Windows.Forms.CheckBox cbPigment;
        private System.Windows.Forms.CheckBox cbMB;
        private System.Windows.Forms.Label lblParentCode;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.TextBox txtJoinQty;
        private System.Windows.Forms.Label lblJoinQty;
        private System.Windows.Forms.TextBox txtQuoRWpcs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtQuoPWpcs;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cbProduction;
        private System.Windows.Forms.CheckBox cbZeroCost;
        private System.Windows.Forms.TextBox txtPCSRate;
        private System.Windows.Forms.Label lblPcsRate;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label lblUnit;
    }
}

