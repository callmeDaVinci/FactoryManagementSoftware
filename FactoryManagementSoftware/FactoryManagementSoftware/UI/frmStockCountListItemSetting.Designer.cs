using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    partial class frmStockCountListItemSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockCountListItemSetting));
            this.tableLayoutPanel28 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpStepPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gunaGroupBox4 = new Guna.UI.WinForms.GunaGroupBox();
            this.lblItem = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.txtUnitConversionRate = new Guna.UI.WinForms.GunaTextBox();
            this.txtCountUnit = new Guna.UI.WinForms.GunaTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCountUnit = new System.Windows.Forms.Label();
            this.lblUnitConversionRate = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbDefaultOutCat = new Guna.UI.WinForms.GunaComboBox();
            this.cmbDefaultInCat = new Guna.UI.WinForms.GunaComboBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.gunaGradientButton3 = new Guna.UI.WinForms.GunaGradientButton();
            this.lblStockLocation = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDefaultTo = new System.Windows.Forms.Label();
            this.lblDefaultFrom = new System.Windows.Forms.Label();
            this.cmbStockLocation = new Guna.UI.WinForms.GunaComboBox();
            this.tlpButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnInsert = new Guna.UI.WinForms.GunaGradientButton();
            this.btnCancel = new Guna.UI.WinForms.GunaGradientButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider6 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblSystemUnit = new System.Windows.Forms.Label();
            this.txtSystemUnit = new Guna.UI.WinForms.GunaTextBox();
            this.lblUnitExample = new System.Windows.Forms.Label();
            this.txtItem = new FactoryManagementSoftware.CustomTextBox();
            this.tableLayoutPanel28.SuspendLayout();
            this.tlpStepPanel.SuspendLayout();
            this.gunaGroupBox4.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel28
            // 
            this.tableLayoutPanel28.AutoScroll = true;
            this.tableLayoutPanel28.AutoSize = true;
            this.tableLayoutPanel28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.tableLayoutPanel28.ColumnCount = 1;
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel28.Controls.Add(this.tlpStepPanel, 0, 0);
            this.tableLayoutPanel28.Controls.Add(this.tlpButton, 0, 1);
            this.tableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel28.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel28.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel28.Name = "tableLayoutPanel28";
            this.tableLayoutPanel28.RowCount = 2;
            this.tableLayoutPanel28.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel28.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel28.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel28.Size = new System.Drawing.Size(493, 601);
            this.tableLayoutPanel28.TabIndex = 113;
            // 
            // tlpStepPanel
            // 
            this.tlpStepPanel.AutoScroll = true;
            this.tlpStepPanel.ColumnCount = 1;
            this.tlpStepPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStepPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpStepPanel.Controls.Add(this.gunaGroupBox4, 0, 0);
            this.tlpStepPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStepPanel.Location = new System.Drawing.Point(30, 10);
            this.tlpStepPanel.Margin = new System.Windows.Forms.Padding(30, 10, 30, 10);
            this.tlpStepPanel.Name = "tlpStepPanel";
            this.tlpStepPanel.RowCount = 1;
            this.tlpStepPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStepPanel.Size = new System.Drawing.Size(433, 511);
            this.tlpStepPanel.TabIndex = 228;
            // 
            // gunaGroupBox4
            // 
            this.gunaGroupBox4.BackColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.gunaGroupBox4.BorderColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox4.BorderSize = 1;
            this.gunaGroupBox4.Controls.Add(this.lblItem);
            this.gunaGroupBox4.Controls.Add(this.txtItem);
            this.gunaGroupBox4.Controls.Add(this.panel1);
            this.gunaGroupBox4.Controls.Add(this.tableLayoutPanel13);
            this.gunaGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox4.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.gunaGroupBox4.LineColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox4.Location = new System.Drawing.Point(5, 5);
            this.gunaGroupBox4.Margin = new System.Windows.Forms.Padding(5);
            this.gunaGroupBox4.Name = "gunaGroupBox4";
            this.gunaGroupBox4.Padding = new System.Windows.Forms.Padding(8, 43, 8, 5);
            this.gunaGroupBox4.Radius = 3;
            this.gunaGroupBox4.Size = new System.Drawing.Size(423, 501);
            this.gunaGroupBox4.TabIndex = 262;
            this.gunaGroupBox4.Text = "GENERAL";
            this.gunaGroupBox4.TextLocation = new System.Drawing.Point(10, 8);
            // 
            // lblItem
            // 
            this.lblItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblItem.AutoSize = true;
            this.lblItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblItem.Location = new System.Drawing.Point(11, 46);
            this.lblItem.Margin = new System.Windows.Forms.Padding(3);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(37, 19);
            this.lblItem.TabIndex = 2019;
            this.lblItem.Text = "Item";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Location = new System.Drawing.Point(14, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 47);
            this.panel1.TabIndex = 2018;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 1;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.lblUnitExample, 0, 6);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel4, 0, 5);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.cmbStockLocation, 0, 1);
            this.tableLayoutPanel13.Location = new System.Drawing.Point(11, 130);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 7;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(399, 345);
            this.tableLayoutPanel13.TabIndex = 258;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Controls.Add(this.txtSystemUnit, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtUnitConversionRate, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtCountUnit, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 170);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(399, 40);
            this.tableLayoutPanel4.TabIndex = 2022;
            // 
            // txtUnitConversionRate
            // 
            this.txtUnitConversionRate.BackColor = System.Drawing.Color.Transparent;
            this.txtUnitConversionRate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtUnitConversionRate.BaseColor = System.Drawing.Color.White;
            this.txtUnitConversionRate.BorderColor = System.Drawing.Color.Silver;
            this.txtUnitConversionRate.BorderSize = 1;
            this.txtUnitConversionRate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUnitConversionRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUnitConversionRate.FocusedBaseColor = System.Drawing.Color.White;
            this.txtUnitConversionRate.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtUnitConversionRate.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtUnitConversionRate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUnitConversionRate.ForeColor = System.Drawing.Color.Black;
            this.txtUnitConversionRate.Location = new System.Drawing.Point(126, 3);
            this.txtUnitConversionRate.MaximumSize = new System.Drawing.Size(0, 33);
            this.txtUnitConversionRate.Name = "txtUnitConversionRate";
            this.txtUnitConversionRate.PasswordChar = '\0';
            this.txtUnitConversionRate.Radius = 3;
            this.txtUnitConversionRate.SelectedText = "";
            this.txtUnitConversionRate.Size = new System.Drawing.Size(145, 33);
            this.txtUnitConversionRate.TabIndex = 2021;
            this.txtUnitConversionRate.Text = "1";
            this.txtUnitConversionRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUnitConversionRate.TextChanged += new System.EventHandler(this.txtUnitConversionRate_TextChanged);
            this.txtUnitConversionRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitConversionRate_KeyPress);
            // 
            // txtCountUnit
            // 
            this.txtCountUnit.BackColor = System.Drawing.Color.Transparent;
            this.txtCountUnit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtCountUnit.BaseColor = System.Drawing.Color.White;
            this.txtCountUnit.BorderColor = System.Drawing.Color.Silver;
            this.txtCountUnit.BorderSize = 1;
            this.txtCountUnit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCountUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCountUnit.FocusedBaseColor = System.Drawing.Color.White;
            this.txtCountUnit.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtCountUnit.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCountUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCountUnit.ForeColor = System.Drawing.Color.Black;
            this.txtCountUnit.Location = new System.Drawing.Point(3, 3);
            this.txtCountUnit.MaximumSize = new System.Drawing.Size(0, 33);
            this.txtCountUnit.Name = "txtCountUnit";
            this.txtCountUnit.PasswordChar = '\0';
            this.txtCountUnit.Radius = 3;
            this.txtCountUnit.SelectedText = "";
            this.txtCountUnit.Size = new System.Drawing.Size(107, 33);
            this.txtCountUnit.TabIndex = 2020;
            this.txtCountUnit.Text = "pcs";
            this.txtCountUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCountUnit.TextChanged += new System.EventHandler(this.txtCountUnit_TextChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.Controls.Add(this.lblSystemUnit, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblCountUnit, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblUnitConversionRate, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 140);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(399, 30);
            this.tableLayoutPanel3.TabIndex = 2021;
            // 
            // lblCountUnit
            // 
            this.lblCountUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCountUnit.AutoSize = true;
            this.lblCountUnit.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCountUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCountUnit.Location = new System.Drawing.Point(3, 8);
            this.lblCountUnit.Margin = new System.Windows.Forms.Padding(3);
            this.lblCountUnit.Name = "lblCountUnit";
            this.lblCountUnit.Size = new System.Drawing.Size(77, 19);
            this.lblCountUnit.TabIndex = 160;
            this.lblCountUnit.Text = "Count Unit";
            // 
            // lblUnitConversionRate
            // 
            this.lblUnitConversionRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUnitConversionRate.AutoSize = true;
            this.lblUnitConversionRate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblUnitConversionRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUnitConversionRate.Location = new System.Drawing.Point(126, 8);
            this.lblUnitConversionRate.Margin = new System.Windows.Forms.Padding(3);
            this.lblUnitConversionRate.Name = "lblUnitConversionRate";
            this.lblUnitConversionRate.Size = new System.Drawing.Size(139, 19);
            this.lblUnitConversionRate.TabIndex = 2022;
            this.lblUnitConversionRate.Text = "Unit Conversion Rate";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.cmbDefaultOutCat, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbDefaultInCat, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(399, 40);
            this.tableLayoutPanel2.TabIndex = 2022;
            // 
            // cmbDefaultOutCat
            // 
            this.cmbDefaultOutCat.BackColor = System.Drawing.Color.Transparent;
            this.cmbDefaultOutCat.BaseColor = System.Drawing.Color.White;
            this.cmbDefaultOutCat.BorderColor = System.Drawing.Color.Silver;
            this.cmbDefaultOutCat.BorderSize = 1;
            this.cmbDefaultOutCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDefaultOutCat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDefaultOutCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultOutCat.FocusedColor = System.Drawing.Color.Empty;
            this.cmbDefaultOutCat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDefaultOutCat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbDefaultOutCat.Location = new System.Drawing.Point(207, 3);
            this.cmbDefaultOutCat.Name = "cmbDefaultOutCat";
            this.cmbDefaultOutCat.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmbDefaultOutCat.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cmbDefaultOutCat.Radius = 3;
            this.cmbDefaultOutCat.Size = new System.Drawing.Size(189, 31);
            this.cmbDefaultOutCat.TabIndex = 2021;
            // 
            // cmbDefaultInCat
            // 
            this.cmbDefaultInCat.BackColor = System.Drawing.Color.Transparent;
            this.cmbDefaultInCat.BaseColor = System.Drawing.Color.White;
            this.cmbDefaultInCat.BorderColor = System.Drawing.Color.Silver;
            this.cmbDefaultInCat.BorderSize = 1;
            this.cmbDefaultInCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDefaultInCat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDefaultInCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultInCat.FocusedColor = System.Drawing.Color.Empty;
            this.cmbDefaultInCat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDefaultInCat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbDefaultInCat.Location = new System.Drawing.Point(3, 3);
            this.cmbDefaultInCat.Name = "cmbDefaultInCat";
            this.cmbDefaultInCat.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmbDefaultInCat.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cmbDefaultInCat.Radius = 3;
            this.cmbDefaultInCat.Size = new System.Drawing.Size(188, 31);
            this.cmbDefaultInCat.TabIndex = 2022;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel5.Controls.Add(this.gunaGradientButton3, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblStockLocation, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(399, 30);
            this.tableLayoutPanel5.TabIndex = 252;
            // 
            // gunaGradientButton3
            // 
            this.gunaGradientButton3.AnimationHoverSpeed = 0.07F;
            this.gunaGradientButton3.AnimationSpeed = 0.03F;
            this.gunaGradientButton3.BackColor = System.Drawing.Color.Transparent;
            this.gunaGradientButton3.BaseColor1 = System.Drawing.Color.Transparent;
            this.gunaGradientButton3.BaseColor2 = System.Drawing.Color.Transparent;
            this.gunaGradientButton3.BorderColor = System.Drawing.Color.Transparent;
            this.gunaGradientButton3.BorderSize = 1;
            this.gunaGradientButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaGradientButton3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGradientButton3.FocusedColor = System.Drawing.Color.Empty;
            this.gunaGradientButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaGradientButton3.ForeColor = System.Drawing.Color.White;
            this.gunaGradientButton3.Image = ((System.Drawing.Image)(resources.GetObject("gunaGradientButton3.Image")));
            this.gunaGradientButton3.ImageSize = new System.Drawing.Size(15, 15);
            this.gunaGradientButton3.Location = new System.Drawing.Point(362, 3);
            this.gunaGradientButton3.Name = "gunaGradientButton3";
            this.gunaGradientButton3.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.gunaGradientButton3.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.gunaGradientButton3.OnHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.gunaGradientButton3.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaGradientButton3.OnHoverImage = null;
            this.gunaGradientButton3.OnPressedColor = System.Drawing.Color.Black;
            this.gunaGradientButton3.Radius = 2;
            this.gunaGradientButton3.Size = new System.Drawing.Size(34, 24);
            this.gunaGradientButton3.TabIndex = 252;
            this.gunaGradientButton3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gunaGradientButton3.Visible = false;
            // 
            // lblStockLocation
            // 
            this.lblStockLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStockLocation.AutoSize = true;
            this.lblStockLocation.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblStockLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStockLocation.Location = new System.Drawing.Point(3, 8);
            this.lblStockLocation.Margin = new System.Windows.Forms.Padding(3);
            this.lblStockLocation.Name = "lblStockLocation";
            this.lblStockLocation.Size = new System.Drawing.Size(98, 19);
            this.lblStockLocation.TabIndex = 160;
            this.lblStockLocation.Text = "Stock Location";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblDefaultTo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDefaultFrom, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 70);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(399, 30);
            this.tableLayoutPanel1.TabIndex = 2020;
            // 
            // lblDefaultTo
            // 
            this.lblDefaultTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDefaultTo.AutoSize = true;
            this.lblDefaultTo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblDefaultTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDefaultTo.Location = new System.Drawing.Point(207, 8);
            this.lblDefaultTo.Margin = new System.Windows.Forms.Padding(3);
            this.lblDefaultTo.Name = "lblDefaultTo";
            this.lblDefaultTo.Size = new System.Drawing.Size(99, 19);
            this.lblDefaultTo.TabIndex = 160;
            this.lblDefaultTo.Text = "Default Out To";
            // 
            // lblDefaultFrom
            // 
            this.lblDefaultFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDefaultFrom.AutoSize = true;
            this.lblDefaultFrom.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblDefaultFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDefaultFrom.Location = new System.Drawing.Point(3, 8);
            this.lblDefaultFrom.Margin = new System.Windows.Forms.Padding(3);
            this.lblDefaultFrom.Name = "lblDefaultFrom";
            this.lblDefaultFrom.Size = new System.Drawing.Size(105, 19);
            this.lblDefaultFrom.TabIndex = 160;
            this.lblDefaultFrom.Text = "Default In From";
            // 
            // cmbStockLocation
            // 
            this.cmbStockLocation.BackColor = System.Drawing.Color.Transparent;
            this.cmbStockLocation.BaseColor = System.Drawing.Color.White;
            this.cmbStockLocation.BorderColor = System.Drawing.Color.Silver;
            this.cmbStockLocation.BorderSize = 1;
            this.cmbStockLocation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStockLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockLocation.FocusedColor = System.Drawing.Color.Empty;
            this.cmbStockLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStockLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbStockLocation.Location = new System.Drawing.Point(3, 33);
            this.cmbStockLocation.Name = "cmbStockLocation";
            this.cmbStockLocation.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmbStockLocation.OnHoverItemForeColor = System.Drawing.Color.White;
            this.cmbStockLocation.Radius = 3;
            this.cmbStockLocation.Size = new System.Drawing.Size(393, 31);
            this.cmbStockLocation.TabIndex = 253;
            this.cmbStockLocation.SelectedIndexChanged += new System.EventHandler(this.cmbStockLocation_SelectedIndexChanged);
            // 
            // tlpButton
            // 
            this.tlpButton.BackColor = System.Drawing.Color.White;
            this.tlpButton.ColumnCount = 5;
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tlpButton.Controls.Add(this.btnInsert, 3, 1);
            this.tlpButton.Controls.Add(this.btnCancel, 2, 1);
            this.tlpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButton.Location = new System.Drawing.Point(0, 531);
            this.tlpButton.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButton.Name = "tlpButton";
            this.tlpButton.RowCount = 3;
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpButton.Size = new System.Drawing.Size(493, 70);
            this.tlpButton.TabIndex = 221;
            // 
            // btnInsert
            // 
            this.btnInsert.AnimationHoverSpeed = 0.07F;
            this.btnInsert.AnimationSpeed = 0.03F;
            this.btnInsert.BackColor = System.Drawing.Color.Transparent;
            this.btnInsert.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            this.btnInsert.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            this.btnInsert.BorderColor = System.Drawing.Color.Black;
            this.btnInsert.BorderSize = 1;
            this.btnInsert.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnInsert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInsert.FocusedColor = System.Drawing.Color.Empty;
            this.btnInsert.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnInsert.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnInsert.Image = null;
            this.btnInsert.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnInsert.ImageSize = new System.Drawing.Size(25, 25);
            this.btnInsert.Location = new System.Drawing.Point(315, 15);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnInsert.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnInsert.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnInsert.OnHoverForeColor = System.Drawing.Color.White;
            this.btnInsert.OnHoverImage = null;
            this.btnInsert.OnPressedColor = System.Drawing.Color.Black;
            this.btnInsert.Radius = 2;
            this.btnInsert.Size = new System.Drawing.Size(140, 40);
            this.btnInsert.TabIndex = 222;
            this.btnInsert.Text = "Add Item";
            this.btnInsert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
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
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.FocusedColor = System.Drawing.Color.Empty;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Image = null;
            this.btnCancel.ImageSize = new System.Drawing.Size(20, 20);
            this.btnCancel.Location = new System.Drawing.Point(165, 15);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnCancel.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnCancel.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnCancel.OnHoverForeColor = System.Drawing.Color.White;
            this.btnCancel.OnHoverImage = null;
            this.btnCancel.OnPressedColor = System.Drawing.Color.Black;
            this.btnCancel.Radius = 2;
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.TabIndex = 223;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
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
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
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
            // lblSystemUnit
            // 
            this.lblSystemUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSystemUnit.AutoSize = true;
            this.lblSystemUnit.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSystemUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSystemUnit.Location = new System.Drawing.Point(287, 8);
            this.lblSystemUnit.Margin = new System.Windows.Forms.Padding(3);
            this.lblSystemUnit.Name = "lblSystemUnit";
            this.lblSystemUnit.Size = new System.Drawing.Size(83, 19);
            this.lblSystemUnit.TabIndex = 2023;
            this.lblSystemUnit.Text = "System Unit";
            // 
            // txtSystemUnit
            // 
            this.txtSystemUnit.BackColor = System.Drawing.Color.Transparent;
            this.txtSystemUnit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtSystemUnit.BaseColor = System.Drawing.Color.White;
            this.txtSystemUnit.BorderColor = System.Drawing.Color.Silver;
            this.txtSystemUnit.BorderSize = 1;
            this.txtSystemUnit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSystemUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSystemUnit.FocusedBaseColor = System.Drawing.Color.White;
            this.txtSystemUnit.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtSystemUnit.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSystemUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSystemUnit.ForeColor = System.Drawing.Color.Black;
            this.txtSystemUnit.Location = new System.Drawing.Point(287, 3);
            this.txtSystemUnit.MaximumSize = new System.Drawing.Size(0, 33);
            this.txtSystemUnit.Name = "txtSystemUnit";
            this.txtSystemUnit.PasswordChar = '\0';
            this.txtSystemUnit.Radius = 3;
            this.txtSystemUnit.SelectedText = "";
            this.txtSystemUnit.Size = new System.Drawing.Size(109, 33);
            this.txtSystemUnit.TabIndex = 2021;
            this.txtSystemUnit.Text = "pcs";
            this.txtSystemUnit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSystemUnit.TextChanged += new System.EventHandler(this.txtSystemUnit_TextChanged);
            // 
            // lblUnitExample
            // 
            this.lblUnitExample.AutoSize = true;
            this.lblUnitExample.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitExample.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUnitExample.Location = new System.Drawing.Point(3, 213);
            this.lblUnitExample.Margin = new System.Windows.Forms.Padding(3);
            this.lblUnitExample.Name = "lblUnitExample";
            this.lblUnitExample.Size = new System.Drawing.Size(57, 17);
            this.lblUnitExample.TabIndex = 161;
            this.lblUnitExample.Text = "Example:";
            // 
            // txtItem
            // 
            this.txtItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.txtItem.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.txtItem.Location = new System.Drawing.Point(22, 81);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(378, 20);
            this.txtItem.TabIndex = 2017;
            this.txtItem.Text = "Search (Item Name/Code)";
            this.txtItem.Values = null;
            this.txtItem.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            this.txtItem.Enter += new System.EventHandler(this.txtItemDescription_Enter);
            this.txtItem.Leave += new System.EventHandler(this.txtItemDescription_Leave);
            // 
            // frmStockCountListItemSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(493, 601);
            this.Controls.Add(this.tableLayoutPanel28);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmStockCountListItemSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Count Item Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStockCountListEditing_FormClosing);
            this.tableLayoutPanel28.ResumeLayout(false);
            this.tlpStepPanel.ResumeLayout(false);
            this.gunaGroupBox4.ResumeLayout(false);
            this.gunaGroupBox4.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel28;
        private System.Windows.Forms.TableLayoutPanel tlpButton;
        private Guna.UI.WinForms.GunaGradientButton btnCancel;
        private Guna.UI.WinForms.GunaGradientButton btnInsert;
        private System.Windows.Forms.TableLayoutPanel tlpStepPanel;
        //private CircleLabel circleLabelStep1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Guna.UI.WinForms.GunaGroupBox gunaGroupBox4;
        private Guna.UI.WinForms.GunaComboBox cmbStockLocation;
        private System.Windows.Forms.Label lblStockLocation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Guna.UI.WinForms.GunaGradientButton gunaGradientButton3;
        private ErrorProvider errorProvider2;
        private ErrorProvider errorProvider3;
        private ImageList imageList1;
        private ErrorProvider errorProvider4;
        private ErrorProvider errorProvider5;
        private ErrorProvider errorProvider6;
        private Label lblItem;
        private CustomTextBox txtItem;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel2;
        private Guna.UI.WinForms.GunaComboBox cmbDefaultOutCat;
        private Guna.UI.WinForms.GunaComboBox cmbDefaultInCat;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblDefaultFrom;
        private Guna.UI.WinForms.GunaTextBox txtUnitConversionRate;
        private Guna.UI.WinForms.GunaTextBox txtCountUnit;
        private Label lblDefaultTo;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lblCountUnit;
        private Label lblUnitConversionRate;
        private Guna.UI.WinForms.GunaTextBox txtSystemUnit;
        private Label lblSystemUnit;
        private Label lblUnitExample;
    }
}