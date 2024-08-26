using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    partial class frmSbbStdPackingSettings
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
            this.tableLayoutPanel28 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpStepPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gunaGroupBox4 = new Guna.UI.WinForms.GunaGroupBox();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtItemCode = new Guna.UI.WinForms.GunaTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtItemName = new Guna.UI.WinForms.GunaTextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPcsPerContainer = new Guna.UI.WinForms.GunaTextBox();
            this.txtPcsPerBag = new Guna.UI.WinForms.GunaTextBox();
            this.txtPcsPerPacket = new Guna.UI.WinForms.GunaTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSystemUnit = new System.Windows.Forms.Label();
            this.lblCountUnit = new System.Windows.Forms.Label();
            this.lblUnitConversionRate = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel28.SuspendLayout();
            this.tlpStepPanel.SuspendLayout();
            this.gunaGroupBox4.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
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
            this.tableLayoutPanel28.Size = new System.Drawing.Size(493, 398);
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
            this.tlpStepPanel.Size = new System.Drawing.Size(433, 308);
            this.tlpStepPanel.TabIndex = 228;
            // 
            // gunaGroupBox4
            // 
            this.gunaGroupBox4.BackColor = System.Drawing.Color.Transparent;
            this.gunaGroupBox4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.gunaGroupBox4.BorderColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox4.BorderSize = 1;
            this.gunaGroupBox4.Controls.Add(this.tableLayoutPanel13);
            this.gunaGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gunaGroupBox4.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold);
            this.gunaGroupBox4.LineColor = System.Drawing.Color.Gainsboro;
            this.gunaGroupBox4.Location = new System.Drawing.Point(5, 5);
            this.gunaGroupBox4.Margin = new System.Windows.Forms.Padding(5);
            this.gunaGroupBox4.Name = "gunaGroupBox4";
            this.gunaGroupBox4.Padding = new System.Windows.Forms.Padding(8, 43, 8, 5);
            this.gunaGroupBox4.Radius = 3;
            this.gunaGroupBox4.Size = new System.Drawing.Size(423, 298);
            this.gunaGroupBox4.TabIndex = 262;
            this.gunaGroupBox4.Text = "GENERAL";
            this.gunaGroupBox4.TextLocation = new System.Drawing.Point(10, 8);
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 1;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.txtItemCode, 0, 3);
            this.tableLayoutPanel13.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.txtItemName, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel4, 0, 5);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(8, 43);
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
            this.tableLayoutPanel13.Size = new System.Drawing.Size(407, 250);
            this.tableLayoutPanel13.TabIndex = 258;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(3, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 19);
            this.label2.TabIndex = 162;
            this.label2.Text = "Code";
            // 
            // txtItemCode
            // 
            this.txtItemCode.BackColor = System.Drawing.Color.Transparent;
            this.txtItemCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtItemCode.BaseColor = System.Drawing.Color.White;
            this.txtItemCode.BorderColor = System.Drawing.Color.Silver;
            this.txtItemCode.BorderSize = 1;
            this.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtItemCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemCode.FocusedBaseColor = System.Drawing.Color.White;
            this.txtItemCode.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtItemCode.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtItemCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtItemCode.ForeColor = System.Drawing.Color.Black;
            this.txtItemCode.Location = new System.Drawing.Point(3, 103);
            this.txtItemCode.MaximumSize = new System.Drawing.Size(0, 33);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.PasswordChar = '\0';
            this.txtItemCode.Radius = 3;
            this.txtItemCode.SelectedText = "";
            this.txtItemCode.Size = new System.Drawing.Size(401, 33);
            this.txtItemCode.TabIndex = 2023;
            this.txtItemCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 161;
            this.label1.Text = "Name";
            // 
            // txtItemName
            // 
            this.txtItemName.BackColor = System.Drawing.Color.Transparent;
            this.txtItemName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtItemName.BaseColor = System.Drawing.Color.White;
            this.txtItemName.BorderColor = System.Drawing.Color.Silver;
            this.txtItemName.BorderSize = 1;
            this.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtItemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemName.FocusedBaseColor = System.Drawing.Color.White;
            this.txtItemName.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtItemName.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtItemName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtItemName.ForeColor = System.Drawing.Color.Black;
            this.txtItemName.Location = new System.Drawing.Point(3, 33);
            this.txtItemName.MaximumSize = new System.Drawing.Size(0, 33);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.PasswordChar = '\0';
            this.txtItemName.Radius = 3;
            this.txtItemName.SelectedText = "";
            this.txtItemName.Size = new System.Drawing.Size(401, 33);
            this.txtItemName.TabIndex = 2022;
            this.txtItemName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Controls.Add(this.txtPcsPerContainer, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtPcsPerBag, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtPcsPerPacket, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 170);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(407, 40);
            this.tableLayoutPanel4.TabIndex = 2022;
            // 
            // txtPcsPerContainer
            // 
            this.txtPcsPerContainer.BackColor = System.Drawing.Color.Transparent;
            this.txtPcsPerContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtPcsPerContainer.BaseColor = System.Drawing.Color.White;
            this.txtPcsPerContainer.BorderColor = System.Drawing.Color.Silver;
            this.txtPcsPerContainer.BorderSize = 1;
            this.txtPcsPerContainer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPcsPerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPcsPerContainer.FocusedBaseColor = System.Drawing.Color.White;
            this.txtPcsPerContainer.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtPcsPerContainer.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPcsPerContainer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPcsPerContainer.ForeColor = System.Drawing.Color.Black;
            this.txtPcsPerContainer.Location = new System.Drawing.Point(293, 3);
            this.txtPcsPerContainer.MaximumSize = new System.Drawing.Size(0, 33);
            this.txtPcsPerContainer.Name = "txtPcsPerContainer";
            this.txtPcsPerContainer.PasswordChar = '\0';
            this.txtPcsPerContainer.Radius = 3;
            this.txtPcsPerContainer.SelectedText = "";
            this.txtPcsPerContainer.Size = new System.Drawing.Size(111, 33);
            this.txtPcsPerContainer.TabIndex = 2021;
            this.txtPcsPerContainer.Text = "0";
            this.txtPcsPerContainer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPcsPerContainer.TextChanged += new System.EventHandler(this.txtSystemUnit_TextChanged);
            this.txtPcsPerContainer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitConversionRate_KeyPress);
            // 
            // txtPcsPerBag
            // 
            this.txtPcsPerBag.BackColor = System.Drawing.Color.Transparent;
            this.txtPcsPerBag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtPcsPerBag.BaseColor = System.Drawing.Color.White;
            this.txtPcsPerBag.BorderColor = System.Drawing.Color.Silver;
            this.txtPcsPerBag.BorderSize = 1;
            this.txtPcsPerBag.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPcsPerBag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPcsPerBag.FocusedBaseColor = System.Drawing.Color.White;
            this.txtPcsPerBag.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtPcsPerBag.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPcsPerBag.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPcsPerBag.ForeColor = System.Drawing.Color.Black;
            this.txtPcsPerBag.Location = new System.Drawing.Point(129, 3);
            this.txtPcsPerBag.MaximumSize = new System.Drawing.Size(0, 33);
            this.txtPcsPerBag.Name = "txtPcsPerBag";
            this.txtPcsPerBag.PasswordChar = '\0';
            this.txtPcsPerBag.Radius = 3;
            this.txtPcsPerBag.SelectedText = "";
            this.txtPcsPerBag.Size = new System.Drawing.Size(148, 33);
            this.txtPcsPerBag.TabIndex = 2021;
            this.txtPcsPerBag.Text = "0";
            this.txtPcsPerBag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPcsPerBag.TextChanged += new System.EventHandler(this.txtUnitConversionRate_TextChanged);
            this.txtPcsPerBag.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitConversionRate_KeyPress);
            // 
            // txtPcsPerPacket
            // 
            this.txtPcsPerPacket.BackColor = System.Drawing.Color.Transparent;
            this.txtPcsPerPacket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.txtPcsPerPacket.BaseColor = System.Drawing.Color.White;
            this.txtPcsPerPacket.BorderColor = System.Drawing.Color.Silver;
            this.txtPcsPerPacket.BorderSize = 1;
            this.txtPcsPerPacket.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPcsPerPacket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPcsPerPacket.FocusedBaseColor = System.Drawing.Color.White;
            this.txtPcsPerPacket.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtPcsPerPacket.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPcsPerPacket.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPcsPerPacket.ForeColor = System.Drawing.Color.Black;
            this.txtPcsPerPacket.Location = new System.Drawing.Point(3, 3);
            this.txtPcsPerPacket.MaximumSize = new System.Drawing.Size(0, 33);
            this.txtPcsPerPacket.Name = "txtPcsPerPacket";
            this.txtPcsPerPacket.PasswordChar = '\0';
            this.txtPcsPerPacket.Radius = 3;
            this.txtPcsPerPacket.SelectedText = "";
            this.txtPcsPerPacket.Size = new System.Drawing.Size(110, 33);
            this.txtPcsPerPacket.TabIndex = 2020;
            this.txtPcsPerPacket.Text = "0";
            this.txtPcsPerPacket.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPcsPerPacket.TextChanged += new System.EventHandler(this.txtCountUnit_TextChanged);
            this.txtPcsPerPacket.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitConversionRate_KeyPress);
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(407, 30);
            this.tableLayoutPanel3.TabIndex = 2021;
            // 
            // lblSystemUnit
            // 
            this.lblSystemUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSystemUnit.AutoSize = true;
            this.lblSystemUnit.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblSystemUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSystemUnit.Location = new System.Drawing.Point(293, 8);
            this.lblSystemUnit.Margin = new System.Windows.Forms.Padding(3);
            this.lblSystemUnit.Name = "lblSystemUnit";
            this.lblSystemUnit.Size = new System.Drawing.Size(91, 19);
            this.lblSystemUnit.TabIndex = 2023;
            this.lblSystemUnit.Text = "pcs/container";
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
            this.lblCountUnit.Size = new System.Drawing.Size(74, 19);
            this.lblCountUnit.TabIndex = 160;
            this.lblCountUnit.Text = "pcs/packet";
            // 
            // lblUnitConversionRate
            // 
            this.lblUnitConversionRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUnitConversionRate.AutoSize = true;
            this.lblUnitConversionRate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblUnitConversionRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUnitConversionRate.Location = new System.Drawing.Point(129, 8);
            this.lblUnitConversionRate.Margin = new System.Windows.Forms.Padding(3);
            this.lblUnitConversionRate.Name = "lblUnitConversionRate";
            this.lblUnitConversionRate.Size = new System.Drawing.Size(57, 19);
            this.lblUnitConversionRate.TabIndex = 2022;
            this.lblUnitConversionRate.Text = "pcs/bag";
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
            this.tlpButton.Location = new System.Drawing.Point(0, 328);
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
            this.btnInsert.Text = "Update";
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
            // frmSbbStdPackingSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(168)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(493, 398);
            this.Controls.Add(this.tableLayoutPanel28);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSbbStdPackingSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SBB Standard Packing Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStockCountListEditing_FormClosing);
            this.tableLayoutPanel28.ResumeLayout(false);
            this.tlpStepPanel.ResumeLayout(false);
            this.gunaGroupBox4.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private ErrorProvider errorProvider2;
        private ErrorProvider errorProvider3;
        private ImageList imageList1;
        private ErrorProvider errorProvider4;
        private ErrorProvider errorProvider5;
        private ErrorProvider errorProvider6;
        private TableLayoutPanel tableLayoutPanel4;
        private Guna.UI.WinForms.GunaTextBox txtPcsPerBag;
        private Guna.UI.WinForms.GunaTextBox txtPcsPerPacket;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lblCountUnit;
        private Label lblUnitConversionRate;
        private Guna.UI.WinForms.GunaTextBox txtPcsPerContainer;
        private Label lblSystemUnit;
        private Label label2;
        private Guna.UI.WinForms.GunaTextBox txtItemCode;
        private Label label1;
        private Guna.UI.WinForms.GunaTextBox txtItemName;
    }
}