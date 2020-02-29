namespace FactoryManagementSoftware.UI
{
    partial class frmInOutEdit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblUnit = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTrfFromCategory = new System.Windows.Forms.ComboBox();
            this.cmbTrfFrom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTrfQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblItemCategory = new System.Windows.Forms.Label();
            this.cmbTrfItemCat = new System.Windows.Forms.ComboBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblItemCode = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpTrfDate = new System.Windows.Forms.DateTimePicker();
            this.cmbTrfItemName = new System.Windows.Forms.ComboBox();
            this.cmbTrfItemCode = new System.Windows.Forms.ComboBox();
            this.cmbTrfToCategory = new System.Windows.Forms.ComboBox();
            this.cmbTrfTo = new System.Windows.Forms.ComboBox();
            this.cmbTrfQtyUnit = new System.Windows.Forms.ComboBox();
            this.txtTrfNote = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.lblCategoryError = new System.Windows.Forms.Label();
            this.lblTrfDateError = new System.Windows.Forms.Label();
            this.lblNameError = new System.Windows.Forms.Label();
            this.lblCodeError = new System.Windows.Forms.Label();
            this.lblLocationFromError = new System.Windows.Forms.Label();
            this.lblLocationToError = new System.Windows.Forms.Label();
            this.lblQuantityError = new System.Windows.Forms.Label();
            this.lblUnitError = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider6 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider7 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnEdit = new System.Windows.Forms.Button();
            this.dgvTransfer = new System.Windows.Forms.DataGridView();
            this.label15 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnIN = new System.Windows.Forms.Button();
            this.btnOUT = new System.Windows.Forms.Button();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPartCodeReset = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransfer)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUnit
            // 
            this.lblUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnit.Location = new System.Drawing.Point(182, 5);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(40, 19);
            this.lblUnit.TabIndex = 60;
            this.lblUnit.Text = "UNIT";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnCancel.Location = new System.Drawing.Point(1493, 729);
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
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 24);
            this.label4.TabIndex = 56;
            this.label4.Text = "CATEGORY";
            // 
            // cmbTrfFromCategory
            // 
            this.cmbTrfFromCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfFromCategory.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfFromCategory.FormattingEnabled = true;
            this.cmbTrfFromCategory.Location = new System.Drawing.Point(3, 4);
            this.cmbTrfFromCategory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfFromCategory.Name = "cmbTrfFromCategory";
            this.cmbTrfFromCategory.Size = new System.Drawing.Size(207, 31);
            this.cmbTrfFromCategory.TabIndex = 55;
            this.cmbTrfFromCategory.SelectedIndexChanged += new System.EventHandler(this.cmbTrfFromCategory_SelectedIndexChanged);
            // 
            // cmbTrfFrom
            // 
            this.cmbTrfFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfFrom.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfFrom.FormattingEnabled = true;
            this.cmbTrfFrom.Location = new System.Drawing.Point(216, 4);
            this.cmbTrfFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfFrom.Name = "cmbTrfFrom";
            this.cmbTrfFrom.Size = new System.Drawing.Size(207, 31);
            this.cmbTrfFrom.TabIndex = 53;
            this.cmbTrfFrom.SelectedIndexChanged += new System.EventHandler(this.cmbTrfFrom_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 19);
            this.label2.TabIndex = 51;
            this.label2.Text = "TO";
            // 
            // txtTrfQty
            // 
            this.txtTrfQty.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrfQty.Location = new System.Drawing.Point(3, 4);
            this.txtTrfQty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTrfQty.Name = "txtTrfQty";
            this.txtTrfQty.Size = new System.Drawing.Size(207, 30);
            this.txtTrfQty.TabIndex = 50;
            this.txtTrfQty.TextChanged += new System.EventHandler(this.txtTrfQty_TextChanged);
            this.txtTrfQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrfQty_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 49;
            this.label1.Text = "QUANTITY";
            // 
            // lblItemCategory
            // 
            this.lblItemCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblItemCategory.AutoSize = true;
            this.lblItemCategory.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCategory.Location = new System.Drawing.Point(3, 5);
            this.lblItemCategory.Name = "lblItemCategory";
            this.lblItemCategory.Size = new System.Drawing.Size(48, 19);
            this.lblItemCategory.TabIndex = 48;
            this.lblItemCategory.Text = "FROM";
            // 
            // cmbTrfItemCat
            // 
            this.cmbTrfItemCat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTrfItemCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfItemCat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfItemCat.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbTrfItemCat.FormattingEnabled = true;
            this.cmbTrfItemCat.Location = new System.Drawing.Point(3, 4);
            this.cmbTrfItemCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfItemCat.Name = "cmbTrfItemCat";
            this.cmbTrfItemCat.Size = new System.Drawing.Size(207, 31);
            this.cmbTrfItemCat.TabIndex = 47;
            this.cmbTrfItemCat.SelectedIndexChanged += new System.EventHandler(this.cmbTrfItemCat_SelectedIndexChanged);
            // 
            // lblItemName
            // 
            this.lblItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblItemName.AutoSize = true;
            this.lblItemName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemName.Location = new System.Drawing.Point(3, 5);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(48, 19);
            this.lblItemName.TabIndex = 44;
            this.lblItemName.Text = "NAME";
            // 
            // lblItemCode
            // 
            this.lblItemCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblItemCode.AutoSize = true;
            this.lblItemCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCode.Location = new System.Drawing.Point(3, 5);
            this.lblItemCode.Name = "lblItemCode";
            this.lblItemCode.Size = new System.Drawing.Size(46, 19);
            this.lblItemCode.TabIndex = 42;
            this.lblItemCode.Text = "CODE";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(181, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 19);
            this.label7.TabIndex = 63;
            this.label7.Text = "TRANSFER DATE";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // dtpTrfDate
            // 
            this.dtpTrfDate.CustomFormat = "ddMMMMyy";
            this.dtpTrfDate.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTrfDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTrfDate.Location = new System.Drawing.Point(216, 3);
            this.dtpTrfDate.Name = "dtpTrfDate";
            this.dtpTrfDate.Size = new System.Drawing.Size(207, 30);
            this.dtpTrfDate.TabIndex = 64;
            this.dtpTrfDate.ValueChanged += new System.EventHandler(this.dtpTrfDate_ValueChanged);
            // 
            // cmbTrfItemName
            // 
            this.cmbTrfItemName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTrfItemName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTrfItemName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfItemName.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbTrfItemName.FormattingEnabled = true;
            this.cmbTrfItemName.Location = new System.Drawing.Point(3, 117);
            this.cmbTrfItemName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfItemName.Name = "cmbTrfItemName";
            this.cmbTrfItemName.Size = new System.Drawing.Size(426, 31);
            this.cmbTrfItemName.TabIndex = 65;
            this.cmbTrfItemName.SelectedIndexChanged += new System.EventHandler(this.cmbTrfItemName_SelectedIndexChanged);
            this.cmbTrfItemName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbTrfItemName_KeyPress);
            // 
            // cmbTrfItemCode
            // 
            this.cmbTrfItemCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfItemCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfItemCode.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbTrfItemCode.FormattingEnabled = true;
            this.cmbTrfItemCode.Location = new System.Drawing.Point(3, 200);
            this.cmbTrfItemCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfItemCode.Name = "cmbTrfItemCode";
            this.cmbTrfItemCode.Size = new System.Drawing.Size(426, 31);
            this.cmbTrfItemCode.TabIndex = 66;
            this.cmbTrfItemCode.SelectedIndexChanged += new System.EventHandler(this.cmbTrfItemCode_SelectedIndexChanged);
            // 
            // cmbTrfToCategory
            // 
            this.cmbTrfToCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfToCategory.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfToCategory.FormattingEnabled = true;
            this.cmbTrfToCategory.Location = new System.Drawing.Point(3, 4);
            this.cmbTrfToCategory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfToCategory.Name = "cmbTrfToCategory";
            this.cmbTrfToCategory.Size = new System.Drawing.Size(207, 31);
            this.cmbTrfToCategory.TabIndex = 68;
            this.cmbTrfToCategory.SelectedIndexChanged += new System.EventHandler(this.cmbTrfToCategory_SelectedIndexChanged);
            // 
            // cmbTrfTo
            // 
            this.cmbTrfTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfTo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfTo.FormattingEnabled = true;
            this.cmbTrfTo.Location = new System.Drawing.Point(216, 4);
            this.cmbTrfTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfTo.Name = "cmbTrfTo";
            this.cmbTrfTo.Size = new System.Drawing.Size(207, 31);
            this.cmbTrfTo.TabIndex = 67;
            this.cmbTrfTo.SelectedIndexChanged += new System.EventHandler(this.cmbTrfTo_SelectedIndexChanged);
            // 
            // cmbTrfQtyUnit
            // 
            this.cmbTrfQtyUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfQtyUnit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfQtyUnit.FormattingEnabled = true;
            this.cmbTrfQtyUnit.Location = new System.Drawing.Point(216, 4);
            this.cmbTrfQtyUnit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfQtyUnit.Name = "cmbTrfQtyUnit";
            this.cmbTrfQtyUnit.Size = new System.Drawing.Size(207, 31);
            this.cmbTrfQtyUnit.TabIndex = 69;
            this.cmbTrfQtyUnit.SelectedIndexChanged += new System.EventHandler(this.cmbTrfQtyUnit_SelectedIndexChanged);
            // 
            // txtTrfNote
            // 
            this.txtTrfNote.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrfNote.Location = new System.Drawing.Point(3, 608);
            this.txtTrfNote.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTrfNote.Multiline = true;
            this.txtTrfNote.Name = "txtTrfNote";
            this.txtTrfNote.Size = new System.Drawing.Size(426, 68);
            this.txtTrfNote.TabIndex = 71;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 585);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 19);
            this.label3.TabIndex = 70;
            this.label3.Text = "NOTE";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1367, 729);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 50);
            this.button2.TabIndex = 72;
            this.button2.Text = "TRANSFER";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // lblCategoryError
            // 
            this.lblCategoryError.AutoSize = true;
            this.lblCategoryError.ForeColor = System.Drawing.Color.Red;
            this.lblCategoryError.Location = new System.Drawing.Point(74, 0);
            this.lblCategoryError.Name = "lblCategoryError";
            this.lblCategoryError.Size = new System.Drawing.Size(18, 20);
            this.lblCategoryError.TabIndex = 73;
            this.lblCategoryError.Text = "*";
            // 
            // lblTrfDateError
            // 
            this.lblTrfDateError.AutoSize = true;
            this.lblTrfDateError.ForeColor = System.Drawing.Color.Red;
            this.lblTrfDateError.Location = new System.Drawing.Point(295, 0);
            this.lblTrfDateError.Name = "lblTrfDateError";
            this.lblTrfDateError.Size = new System.Drawing.Size(13, 20);
            this.lblTrfDateError.TabIndex = 74;
            this.lblTrfDateError.Text = "*";
            this.lblTrfDateError.Click += new System.EventHandler(this.label8_Click);
            // 
            // lblNameError
            // 
            this.lblNameError.AutoSize = true;
            this.lblNameError.ForeColor = System.Drawing.Color.Red;
            this.lblNameError.Location = new System.Drawing.Point(58, 0);
            this.lblNameError.Name = "lblNameError";
            this.lblNameError.Size = new System.Drawing.Size(18, 20);
            this.lblNameError.TabIndex = 75;
            this.lblNameError.Text = "*";
            // 
            // lblCodeError
            // 
            this.lblCodeError.AutoSize = true;
            this.lblCodeError.ForeColor = System.Drawing.Color.Red;
            this.lblCodeError.Location = new System.Drawing.Point(58, 0);
            this.lblCodeError.Name = "lblCodeError";
            this.lblCodeError.Size = new System.Drawing.Size(18, 20);
            this.lblCodeError.TabIndex = 76;
            this.lblCodeError.Text = "*";
            // 
            // lblLocationFromError
            // 
            this.lblLocationFromError.AutoSize = true;
            this.lblLocationFromError.ForeColor = System.Drawing.Color.Red;
            this.lblLocationFromError.Location = new System.Drawing.Point(58, 0);
            this.lblLocationFromError.Name = "lblLocationFromError";
            this.lblLocationFromError.Size = new System.Drawing.Size(18, 20);
            this.lblLocationFromError.TabIndex = 77;
            this.lblLocationFromError.Text = "*";
            // 
            // lblLocationToError
            // 
            this.lblLocationToError.AutoSize = true;
            this.lblLocationToError.ForeColor = System.Drawing.Color.Red;
            this.lblLocationToError.Location = new System.Drawing.Point(36, 0);
            this.lblLocationToError.Name = "lblLocationToError";
            this.lblLocationToError.Size = new System.Drawing.Size(18, 20);
            this.lblLocationToError.TabIndex = 78;
            this.lblLocationToError.Text = "*";
            // 
            // lblQuantityError
            // 
            this.lblQuantityError.AutoSize = true;
            this.lblQuantityError.ForeColor = System.Drawing.Color.Red;
            this.lblQuantityError.Location = new System.Drawing.Point(85, 0);
            this.lblQuantityError.Name = "lblQuantityError";
            this.lblQuantityError.Size = new System.Drawing.Size(18, 20);
            this.lblQuantityError.TabIndex = 79;
            this.lblQuantityError.Text = "*";
            // 
            // lblUnitError
            // 
            this.lblUnitError.AutoSize = true;
            this.lblUnitError.ForeColor = System.Drawing.Color.Red;
            this.lblUnitError.Location = new System.Drawing.Point(228, 0);
            this.lblUnitError.Name = "lblUnitError";
            this.lblUnitError.Size = new System.Drawing.Size(18, 20);
            this.lblUnitError.TabIndex = 80;
            this.lblUnitError.Text = "*";
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
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(301, 2);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(122, 50);
            this.btnEdit.TabIndex = 81;
            this.btnEdit.Text = "ADD";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // dgvTransfer
            // 
            this.dgvTransfer.AllowUserToAddRows = false;
            this.dgvTransfer.AllowUserToDeleteRows = false;
            this.dgvTransfer.AllowUserToResizeRows = false;
            this.dgvTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTransfer.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransfer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTransfer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransfer.GridColor = System.Drawing.SystemColors.Control;
            this.dgvTransfer.Location = new System.Drawing.Point(486, 40);
            this.dgvTransfer.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTransfer.Name = "dgvTransfer";
            this.dgvTransfer.ReadOnly = true;
            this.dgvTransfer.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvTransfer.RowHeadersVisible = false;
            this.dgvTransfer.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTransfer.RowTemplate.Height = 40;
            this.dgvTransfer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransfer.Size = new System.Drawing.Size(1113, 646);
            this.dgvTransfer.TabIndex = 102;
            this.dgvTransfer.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTransfer_CellContentClick);
            this.dgvTransfer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTransfer_CellDoubleClick);
            this.dgvTransfer.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTransfer_CellMouseDown);
            this.dgvTransfer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvTransfer_MouseClick);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(482, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 23);
            this.label15.TabIndex = 103;
            this.label15.Text = "TRANSFER LIST";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnDelete.Location = new System.Drawing.Point(177, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 50);
            this.btnDelete.TabIndex = 104;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(636, 13);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(140, 23);
            this.label16.TabIndex = 105;
            this.label16.Text = "RED: STOCK OUT";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label17.Location = new System.Drawing.Point(825, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(146, 23);
            this.label17.TabIndex = 106;
            this.label17.Text = "GREEN: STOCK IN";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAll.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAll.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnClearAll.Location = new System.Drawing.Point(1503, 7);
            this.btnClearAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(96, 27);
            this.btnClearAll.TabIndex = 107;
            this.btnClearAll.Text = "CLEAR ALL";
            this.btnClearAll.UseVisualStyleBackColor = false;
            this.btnClearAll.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnIN
            // 
            this.btnIN.BackColor = System.Drawing.Color.Transparent;
            this.btnIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIN.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnIN.Location = new System.Drawing.Point(2, 2);
            this.btnIN.Margin = new System.Windows.Forms.Padding(2);
            this.btnIN.Name = "btnIN";
            this.btnIN.Size = new System.Drawing.Size(104, 52);
            this.btnIN.TabIndex = 108;
            this.btnIN.Text = "IN";
            this.btnIN.UseVisualStyleBackColor = false;
            this.btnIN.Click += new System.EventHandler(this.btnIN_Click);
            // 
            // btnOUT
            // 
            this.btnOUT.BackColor = System.Drawing.Color.Transparent;
            this.btnOUT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOUT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOUT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnOUT.Location = new System.Drawing.Point(110, 2);
            this.btnOUT.Margin = new System.Windows.Forms.Padding(2);
            this.btnOUT.Name = "btnOUT";
            this.btnOUT.Size = new System.Drawing.Size(107, 52);
            this.btnOUT.TabIndex = 109;
            this.btnOUT.Text = "OUT";
            this.btnOUT.UseVisualStyleBackColor = false;
            this.btnOUT.Click += new System.EventHandler(this.btnOUT_Click);
            // 
            // btnSwitch
            // 
            this.btnSwitch.BackColor = System.Drawing.Color.Transparent;
            this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSwitch.Location = new System.Drawing.Point(221, 2);
            this.btnSwitch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(122, 52);
            this.btnSwitch.TabIndex = 110;
            this.btnSwitch.Text = "SWITCH";
            this.btnSwitch.UseVisualStyleBackColor = false;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.71963F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.28037F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel1.Controls.Add(this.lblPartCodeReset, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCategoryError, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTrfDateError, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(426, 24);
            this.tableLayoutPanel1.TabIndex = 111;
            // 
            // lblPartCodeReset
            // 
            this.lblPartCodeReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPartCodeReset.AutoSize = true;
            this.lblPartCodeReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPartCodeReset.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartCodeReset.ForeColor = System.Drawing.Color.Blue;
            this.lblPartCodeReset.Location = new System.Drawing.Point(370, 5);
            this.lblPartCodeReset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPartCodeReset.Name = "lblPartCodeReset";
            this.lblPartCodeReset.Size = new System.Drawing.Size(52, 19);
            this.lblPartCodeReset.TabIndex = 149;
            this.lblPartCodeReset.Text = "TODAY";
            this.lblPartCodeReset.Click += new System.EventHandler(this.lblPartCodeReset_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.dtpTrfDate, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbTrfItemCat, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(426, 47);
            this.tableLayoutPanel2.TabIndex = 112;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.9108F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.0892F));
            this.tableLayoutPanel3.Controls.Add(this.lblItemName, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblNameError, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 86);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(426, 24);
            this.tableLayoutPanel3.TabIndex = 113;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.9108F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.0892F));
            this.tableLayoutPanel4.Controls.Add(this.lblItemCode, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblCodeError, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 169);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(426, 24);
            this.tableLayoutPanel4.TabIndex = 114;
            this.tableLayoutPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel4_Paint);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.18033F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.81967F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 206F));
            this.tableLayoutPanel5.Controls.Add(this.btnIN, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnOUT, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnSwitch, 2, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 266);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(426, 56);
            this.tableLayoutPanel5.TabIndex = 115;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.9108F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.0892F));
            this.tableLayoutPanel6.Controls.Add(this.lblItemCategory, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblLocationFromError, 1, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 328);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(426, 24);
            this.tableLayoutPanel6.TabIndex = 116;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.cmbTrfFromCategory, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cmbTrfFrom, 1, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 358);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(426, 35);
            this.tableLayoutPanel7.TabIndex = 117;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.981221F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 92.01878F));
            this.tableLayoutPanel8.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblLocationToError, 1, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 411);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(426, 24);
            this.tableLayoutPanel8.TabIndex = 118;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.cmbTrfToCategory, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.cmbTrfTo, 1, 0);
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 441);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(426, 35);
            this.tableLayoutPanel9.TabIndex = 119;
            this.tableLayoutPanel9.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel9_Paint);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel10.ColumnCount = 4;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.95959F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.04041F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel10.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblQuantityError, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblUnit, 2, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblUnitError, 3, 0);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 494);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(426, 24);
            this.tableLayoutPanel10.TabIndex = 120;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.txtTrfQty, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.cmbTrfQtyUnit, 1, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 524);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(426, 35);
            this.tableLayoutPanel11.TabIndex = 121;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.37037F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.62963F));
            this.tableLayoutPanel12.Controls.Add(this.btnEdit, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.btnDelete, 0, 0);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 683);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 1;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(426, 56);
            this.tableLayoutPanel12.TabIndex = 122;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel13.ColumnCount = 1;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel12, 0, 15);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel11, 0, 12);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel10, 0, 11);
            this.tableLayoutPanel13.Controls.Add(this.cmbTrfItemName, 0, 3);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel9, 0, 10);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel4, 0, 4);
            this.tableLayoutPanel13.Controls.Add(this.txtTrfNote, 0, 14);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel8, 0, 9);
            this.tableLayoutPanel13.Controls.Add(this.label3, 0, 13);
            this.tableLayoutPanel13.Controls.Add(this.cmbTrfItemCode, 0, 5);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel7, 0, 8);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel5, 0, 6);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel6, 0, 7);
            this.tableLayoutPanel13.Location = new System.Drawing.Point(26, 13);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 16;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(432, 766);
            this.tableLayoutPanel13.TabIndex = 123;
            this.tableLayoutPanel13.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel13_Paint);
            // 
            // frmInOutEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1632, 803);
            this.Controls.Add(this.tableLayoutPanel13);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dgvTransfer);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "frmInOutEdit";
            this.Text = "frmInOutEdit";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInOutEdit_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmInOutEdit_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransfer)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTrfFromCategory;
        private System.Windows.Forms.ComboBox cmbTrfFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTrfQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblItemCategory;
        public System.Windows.Forms.ComboBox cmbTrfItemCat;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label lblItemCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpTrfDate;
        public System.Windows.Forms.ComboBox cmbTrfItemName;
        public System.Windows.Forms.ComboBox cmbTrfItemCode;
        private System.Windows.Forms.ComboBox cmbTrfToCategory;
        private System.Windows.Forms.ComboBox cmbTrfTo;
        private System.Windows.Forms.ComboBox cmbTrfQtyUnit;
        private System.Windows.Forms.TextBox txtTrfNote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblCategoryError;
        private System.Windows.Forms.Label lblTrfDateError;
        private System.Windows.Forms.Label lblNameError;
        private System.Windows.Forms.Label lblCodeError;
        private System.Windows.Forms.Label lblLocationFromError;
        private System.Windows.Forms.Label lblLocationToError;
        private System.Windows.Forms.Label lblQuantityError;
        private System.Windows.Forms.Label lblUnitError;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;
        private System.Windows.Forms.ErrorProvider errorProvider6;
        private System.Windows.Forms.ErrorProvider errorProvider7;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.DataGridView dgvTransfer;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button btnOUT;
        private System.Windows.Forms.Button btnIN;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label lblPartCodeReset;
    }
}