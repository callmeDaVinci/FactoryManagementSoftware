namespace FactoryManagementSoftware.UI
{
    partial class frmStockCountManagementVer2
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
            this.tlpPOList = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveRecord = new Guna.UI.WinForms.GunaGradientButton();
            this.btnStockUpdate = new Guna.UI.WinForms.GunaGradientButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new Guna.UI.WinForms.GunaGradientButton();
            this.btnAddItem = new Guna.UI.WinForms.GunaGradientButton();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbCustomer = new Guna.UI.WinForms.GunaComboBox();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.lblStockCountList = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblStockCountDate = new System.Windows.Forms.Label();
            this.dtpStockCountDate = new Guna.UI.WinForms.GunaDateTimePicker();
            this.dgvStockCountList = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tlpPOList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockCountList)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpPOList
            // 
            this.tlpPOList.ColumnCount = 1;
            this.tlpPOList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPOList.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tlpPOList.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tlpPOList.Controls.Add(this.dgvStockCountList, 0, 1);
            this.tlpPOList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPOList.Location = new System.Drawing.Point(20, 20);
            this.tlpPOList.Margin = new System.Windows.Forms.Padding(20);
            this.tlpPOList.Name = "tlpPOList";
            this.tlpPOList.RowCount = 3;
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpPOList.Size = new System.Drawing.Size(1242, 663);
            this.tlpPOList.TabIndex = 172;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tableLayoutPanel1.Controls.Add(this.btnSaveRecord, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStockUpdate, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 613);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1242, 50);
            this.tableLayoutPanel1.TabIndex = 174;
            // 
            // btnSaveRecord
            // 
            this.btnSaveRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRecord.AnimationHoverSpeed = 0.07F;
            this.btnSaveRecord.AnimationSpeed = 0.03F;
            this.btnSaveRecord.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveRecord.BaseColor1 = System.Drawing.Color.White;
            this.btnSaveRecord.BaseColor2 = System.Drawing.Color.White;
            this.btnSaveRecord.BorderColor = System.Drawing.Color.Black;
            this.btnSaveRecord.BorderSize = 1;
            this.btnSaveRecord.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSaveRecord.FocusedColor = System.Drawing.Color.Empty;
            this.btnSaveRecord.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSaveRecord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnSaveRecord.Image = null;
            this.btnSaveRecord.ImageSize = new System.Drawing.Size(20, 20);
            this.btnSaveRecord.Location = new System.Drawing.Point(827, 5);
            this.btnSaveRecord.Margin = new System.Windows.Forms.Padding(5);
            this.btnSaveRecord.Name = "btnSaveRecord";
            this.btnSaveRecord.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnSaveRecord.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnSaveRecord.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnSaveRecord.OnHoverForeColor = System.Drawing.Color.White;
            this.btnSaveRecord.OnHoverImage = null;
            this.btnSaveRecord.OnPressedColor = System.Drawing.Color.Black;
            this.btnSaveRecord.Radius = 3;
            this.btnSaveRecord.Size = new System.Drawing.Size(130, 40);
            this.btnSaveRecord.TabIndex = 232;
            this.btnSaveRecord.Text = "Save Record";
            this.btnSaveRecord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnSaveRecord.Visible = false;
            // 
            // btnStockUpdate
            // 
            this.btnStockUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStockUpdate.AnimationHoverSpeed = 0.07F;
            this.btnStockUpdate.AnimationSpeed = 0.03F;
            this.btnStockUpdate.BackColor = System.Drawing.Color.Transparent;
            this.btnStockUpdate.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(107)))), ((int)(((byte)(236)))));
            this.btnStockUpdate.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnStockUpdate.BorderColor = System.Drawing.Color.Black;
            this.btnStockUpdate.BorderSize = 1;
            this.btnStockUpdate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnStockUpdate.FocusedColor = System.Drawing.Color.Empty;
            this.btnStockUpdate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnStockUpdate.ForeColor = System.Drawing.Color.White;
            this.btnStockUpdate.Image = null;
            this.btnStockUpdate.ImageSize = new System.Drawing.Size(20, 20);
            this.btnStockUpdate.Location = new System.Drawing.Point(967, 5);
            this.btnStockUpdate.Margin = new System.Windows.Forms.Padding(5);
            this.btnStockUpdate.Name = "btnStockUpdate";
            this.btnStockUpdate.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnStockUpdate.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnStockUpdate.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnStockUpdate.OnHoverForeColor = System.Drawing.Color.White;
            this.btnStockUpdate.OnHoverImage = null;
            this.btnStockUpdate.OnPressedColor = System.Drawing.Color.Black;
            this.btnStockUpdate.Radius = 3;
            this.btnStockUpdate.Size = new System.Drawing.Size(270, 40);
            this.btnStockUpdate.TabIndex = 227;
            this.btnStockUpdate.Text = "Save & Update Stock";
            this.btnStockUpdate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnStockUpdate.Click += new System.EventHandler(this.btnStockUpdate_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel4.Controls.Add(this.btnEdit, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnAddItem, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel13, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1242, 100);
            this.tableLayoutPanel4.TabIndex = 167;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.AnimationHoverSpeed = 0.07F;
            this.btnEdit.AnimationSpeed = 0.03F;
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.BaseColor1 = System.Drawing.Color.White;
            this.btnEdit.BaseColor2 = System.Drawing.Color.White;
            this.btnEdit.BorderColor = System.Drawing.Color.Black;
            this.btnEdit.BorderSize = 1;
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEdit.FocusedColor = System.Drawing.Color.Empty;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnEdit.Image = null;
            this.btnEdit.ImageSize = new System.Drawing.Size(20, 20);
            this.btnEdit.Location = new System.Drawing.Point(967, 55);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnEdit.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnEdit.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnEdit.OnHoverForeColor = System.Drawing.Color.White;
            this.btnEdit.OnHoverImage = null;
            this.btnEdit.OnPressedColor = System.Drawing.Color.Black;
            this.btnEdit.Radius = 3;
            this.btnEdit.Size = new System.Drawing.Size(130, 40);
            this.btnEdit.TabIndex = 231;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnEdit.Visible = false;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddItem.AnimationHoverSpeed = 0.07F;
            this.btnAddItem.AnimationSpeed = 0.03F;
            this.btnAddItem.BackColor = System.Drawing.Color.Transparent;
            this.btnAddItem.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(160)))));
            this.btnAddItem.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(160)))));
            this.btnAddItem.BorderColor = System.Drawing.Color.Black;
            this.btnAddItem.BorderSize = 1;
            this.btnAddItem.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAddItem.FocusedColor = System.Drawing.Color.Empty;
            this.btnAddItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(33)))), ((int)(((byte)(71)))));
            this.btnAddItem.Image = null;
            this.btnAddItem.ImageSize = new System.Drawing.Size(20, 20);
            this.btnAddItem.Location = new System.Drawing.Point(1107, 55);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnAddItem.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnAddItem.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnAddItem.OnHoverForeColor = System.Drawing.Color.White;
            this.btnAddItem.OnHoverImage = null;
            this.btnAddItem.OnPressedColor = System.Drawing.Color.Black;
            this.btnAddItem.Radius = 2;
            this.btnAddItem.Size = new System.Drawing.Size(130, 40);
            this.btnAddItem.TabIndex = 224;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnAddItem.Visible = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 1;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.cmbCustomer, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel15, 0, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 2;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(394, 94);
            this.tableLayoutPanel13.TabIndex = 258;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.BackColor = System.Drawing.Color.Transparent;
            this.cmbCustomer.BaseColor = System.Drawing.Color.White;
            this.cmbCustomer.BorderColor = System.Drawing.Color.Silver;
            this.cmbCustomer.BorderSize = 1;
            this.cmbCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FocusedColor = System.Drawing.Color.Empty;
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbCustomer.Location = new System.Drawing.Point(5, 54);
            this.cmbCustomer.Margin = new System.Windows.Forms.Padding(5);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmbCustomer.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cmbCustomer.Radius = 3;
            this.cmbCustomer.Size = new System.Drawing.Size(384, 31);
            this.cmbCustomer.TabIndex = 253;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbStockCountList_SelectedIndexChanged);
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 3;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel15.Controls.Add(this.lblStockCountList, 0, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(394, 49);
            this.tableLayoutPanel15.TabIndex = 263;
            // 
            // lblStockCountList
            // 
            this.lblStockCountList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStockCountList.AutoSize = true;
            this.lblStockCountList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStockCountList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStockCountList.Location = new System.Drawing.Point(3, 26);
            this.lblStockCountList.Margin = new System.Windows.Forms.Padding(3);
            this.lblStockCountList.Name = "lblStockCountList";
            this.lblStockCountList.Size = new System.Drawing.Size(72, 20);
            this.lblStockCountList.TabIndex = 166;
            this.lblStockCountList.Text = "Customer";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblStockCountDate, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dtpStockCountDate, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(423, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(294, 94);
            this.tableLayoutPanel3.TabIndex = 259;
            // 
            // lblStockCountDate
            // 
            this.lblStockCountDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStockCountDate.AutoSize = true;
            this.lblStockCountDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStockCountDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStockCountDate.Location = new System.Drawing.Point(3, 26);
            this.lblStockCountDate.Margin = new System.Windows.Forms.Padding(3);
            this.lblStockCountDate.Name = "lblStockCountDate";
            this.lblStockCountDate.Size = new System.Drawing.Size(124, 20);
            this.lblStockCountDate.TabIndex = 166;
            this.lblStockCountDate.Text = "Stock Count Date";
            // 
            // dtpStockCountDate
            // 
            this.dtpStockCountDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStockCountDate.BackColor = System.Drawing.Color.Transparent;
            this.dtpStockCountDate.BaseColor = System.Drawing.Color.White;
            this.dtpStockCountDate.BorderColor = System.Drawing.Color.Silver;
            this.dtpStockCountDate.BorderSize = 1;
            this.dtpStockCountDate.CustomFormat = null;
            this.dtpStockCountDate.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpStockCountDate.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpStockCountDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpStockCountDate.ForeColor = System.Drawing.Color.Black;
            this.dtpStockCountDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStockCountDate.Location = new System.Drawing.Point(3, 52);
            this.dtpStockCountDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStockCountDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStockCountDate.Name = "dtpStockCountDate";
            this.dtpStockCountDate.OnHoverBaseColor = System.Drawing.Color.White;
            this.dtpStockCountDate.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpStockCountDate.OnHoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtpStockCountDate.OnPressedColor = System.Drawing.Color.Black;
            this.dtpStockCountDate.Radius = 3;
            this.dtpStockCountDate.Size = new System.Drawing.Size(288, 31);
            this.dtpStockCountDate.TabIndex = 260;
            this.dtpStockCountDate.Text = "16/2/2024";
            this.dtpStockCountDate.Value = new System.DateTime(2024, 2, 16, 10, 40, 54, 609);
            // 
            // dgvStockCountList
            // 
            this.dgvStockCountList.AllowUserToAddRows = false;
            this.dgvStockCountList.AllowUserToDeleteRows = false;
            this.dgvStockCountList.AllowUserToOrderColumns = true;
            this.dgvStockCountList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvStockCountList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvStockCountList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvStockCountList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvStockCountList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockCountList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStockCountList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStockCountList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStockCountList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvStockCountList.Location = new System.Drawing.Point(5, 105);
            this.dgvStockCountList.Margin = new System.Windows.Forms.Padding(5);
            this.dgvStockCountList.MultiSelect = false;
            this.dgvStockCountList.Name = "dgvStockCountList";
            this.dgvStockCountList.RowHeadersVisible = false;
            this.dgvStockCountList.RowHeadersWidth = 51;
            this.dgvStockCountList.RowTemplate.Height = 50;
            this.dgvStockCountList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockCountList.Size = new System.Drawing.Size(1232, 503);
            this.dgvStockCountList.TabIndex = 156;
            this.dgvStockCountList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockCountList_CellClick);
            this.dgvStockCountList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockCountList_CellEndEdit);
            this.dgvStockCountList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvStockCountList_EditingControlShowing);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.tlpPOList, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1282, 703);
            this.tableLayoutPanel6.TabIndex = 173;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmStockCountManagementVer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1282, 703);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "frmStockCountManagementVer2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Count Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStockCountManagement_FormClosing);
            this.Load += new System.EventHandler(this.frmStockCountManagementVer2_Load);
            this.Shown += new System.EventHandler(this.frmStockCountManagement_Shown);
            this.tlpPOList.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockCountList)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPOList;
        private System.Windows.Forms.DataGridView dgvStockCountList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblStockCountList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private Guna.UI.WinForms.GunaComboBox cmbCustomer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private Guna.UI.WinForms.GunaGradientButton btnAddItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblStockCountDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guna.UI.WinForms.GunaGradientButton btnStockUpdate;
        private Guna.UI.WinForms.GunaGradientButton btnEdit;
        private Guna.UI.WinForms.GunaGradientButton btnSaveRecord;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Guna.UI.WinForms.GunaDateTimePicker dtpStockCountDate;
    }
}