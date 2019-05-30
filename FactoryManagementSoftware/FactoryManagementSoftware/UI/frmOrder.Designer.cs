﻿namespace FactoryManagementSoftware.UI
{
    partial class frmOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOrd = new System.Windows.Forms.DataGridView();
            this.ord_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_added_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_required_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_pending = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_received = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvOrderAlert = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnOrder = new System.Windows.Forms.Button();
            this.txtOrdSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbStatusSearch = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbZeroCost = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderAlert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrd
            // 
            this.dgvOrd.AllowUserToAddRows = false;
            this.dgvOrd.AllowUserToDeleteRows = false;
            this.dgvOrd.AllowUserToResizeRows = false;
            this.dgvOrd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrd.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvOrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ord_id,
            this.ord_added_date,
            this.ord_required_date,
            this.ord_item_code,
            this.item_name,
            this.ord_qty,
            this.ord_pending,
            this.ord_received,
            this.ord_unit,
            this.ord_status});
            this.dgvOrd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvOrd.GridColor = System.Drawing.SystemColors.Control;
            this.dgvOrd.Location = new System.Drawing.Point(28, 85);
            this.dgvOrd.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOrd.MultiSelect = false;
            this.dgvOrd.Name = "dgvOrd";
            this.dgvOrd.ReadOnly = true;
            this.dgvOrd.RowHeadersVisible = false;
            this.dgvOrd.RowTemplate.Height = 40;
            this.dgvOrd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrd.Size = new System.Drawing.Size(1519, 345);
            this.dgvOrd.TabIndex = 19;
            this.dgvOrd.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrd_CellDoubleClick);
            this.dgvOrd.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrd_CellMouseDown);
            this.dgvOrd.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvOrd_SortCompare);
            this.dgvOrd.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvOrd_MouseClick);
            // 
            // ord_id
            // 
            this.ord_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_id.HeaderText = "ID";
            this.ord_id.Name = "ord_id";
            this.ord_id.ReadOnly = true;
            this.ord_id.Width = 56;
            // 
            // ord_added_date
            // 
            this.ord_added_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_added_date.HeaderText = "Added Date";
            this.ord_added_date.Name = "ord_added_date";
            this.ord_added_date.ReadOnly = true;
            this.ord_added_date.Width = 130;
            // 
            // ord_required_date
            // 
            this.ord_required_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ord_required_date.HeaderText = "Date Required";
            this.ord_required_date.Name = "ord_required_date";
            this.ord_required_date.ReadOnly = true;
            this.ord_required_date.Width = 148;
            // 
            // ord_item_code
            // 
            this.ord_item_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ord_item_code.HeaderText = "Code";
            this.ord_item_code.Name = "ord_item_code";
            this.ord_item_code.ReadOnly = true;
            // 
            // item_name
            // 
            this.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_name.HeaderText = "Name";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            // 
            // ord_qty
            // 
            this.ord_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ord_qty.HeaderText = "Lot Order";
            this.ord_qty.Name = "ord_qty";
            this.ord_qty.ReadOnly = true;
            this.ord_qty.Width = 112;
            // 
            // ord_pending
            // 
            this.ord_pending.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_pending.HeaderText = "Lot Pending";
            this.ord_pending.Name = "ord_pending";
            this.ord_pending.ReadOnly = true;
            this.ord_pending.Width = 130;
            // 
            // ord_received
            // 
            this.ord_received.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_received.HeaderText = "Lot Received";
            this.ord_received.Name = "ord_received";
            this.ord_received.ReadOnly = true;
            this.ord_received.Width = 135;
            // 
            // ord_unit
            // 
            this.ord_unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_unit.HeaderText = "Unit";
            this.ord_unit.Name = "ord_unit";
            this.ord_unit.ReadOnly = true;
            this.ord_unit.Width = 71;
            // 
            // ord_status
            // 
            this.ord_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ord_status.DefaultCellStyle = dataGridViewCellStyle6;
            this.ord_status.HeaderText = "Status";
            this.ord_status.Name = "ord_status";
            this.ord_status.ReadOnly = true;
            this.ord_status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ord_status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ord_status.Width = 62;
            // 
            // dgvOrderAlert
            // 
            this.dgvOrderAlert.AllowUserToAddRows = false;
            this.dgvOrderAlert.AllowUserToDeleteRows = false;
            this.dgvOrderAlert.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrderAlert.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderAlert.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrderAlert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderAlert.GridColor = System.Drawing.SystemColors.Control;
            this.dgvOrderAlert.Location = new System.Drawing.Point(28, 515);
            this.dgvOrderAlert.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOrderAlert.Name = "dgvOrderAlert";
            this.dgvOrderAlert.ReadOnly = true;
            this.dgvOrderAlert.RowHeadersVisible = false;
            this.dgvOrderAlert.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvOrderAlert.RowTemplate.Height = 40;
            this.dgvOrderAlert.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderAlert.Size = new System.Drawing.Size(1519, 314);
            this.dgvOrderAlert.TabIndex = 80;
            this.dgvOrderAlert.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderAlert_CellDoubleClick);
            this.dgvOrderAlert.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOrderAlert_CellFormatting);
            this.dgvOrderAlert.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvOrderAlert_SortCompare);
            this.dgvOrderAlert.Sorted += new System.EventHandler(this.dgvOrderAlert_Sorted);
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
            // btnOrder
            // 
            this.btnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrder.ForeColor = System.Drawing.Color.White;
            this.btnOrder.Location = new System.Drawing.Point(1427, 28);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(2);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(120, 52);
            this.btnOrder.TabIndex = 81;
            this.btnOrder.Text = "NEW";
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // txtOrdSearch
            // 
            this.txtOrdSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrdSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.txtOrdSearch.Location = new System.Drawing.Point(383, 42);
            this.txtOrdSearch.Name = "txtOrdSearch";
            this.txtOrdSearch.Size = new System.Drawing.Size(370, 38);
            this.txtOrdSearch.TabIndex = 84;
            this.txtOrdSearch.TextChanged += new System.EventHandler(this.txtOrdSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(379, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 83;
            this.label1.Text = "SEARCH";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 494);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 19);
            this.label2.TabIndex = 85;
            this.label2.Text = "ALERT";
            // 
            // cmbStatusSearch
            // 
            this.cmbStatusSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatusSearch.FormattingEnabled = true;
            this.cmbStatusSearch.Items.AddRange(new object[] {
            "ALL",
            "REQUESTING",
            "PENDING",
            "RECEIVED",
            "CANCELLED"});
            this.cmbStatusSearch.Location = new System.Drawing.Point(28, 41);
            this.cmbStatusSearch.Name = "cmbStatusSearch";
            this.cmbStatusSearch.Size = new System.Drawing.Size(325, 39);
            this.cmbStatusSearch.TabIndex = 87;
            this.cmbStatusSearch.SelectedIndexChanged += new System.EventHandler(this.cmbStatusSearch_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 23);
            this.label3.TabIndex = 86;
            this.label3.Text = "STATUS";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnReset.Location = new System.Drawing.Point(1292, 30);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 50);
            this.btnReset.TabIndex = 88;
            this.btnReset.Text = "REPORT";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1479, 469);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(68, 31);
            this.btnSearch.TabIndex = 107;
            this.btnSearch.Text = "CHECK";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "ALL",
            "REQUESTING",
            "PENDING",
            "RECEIVED",
            "CANCELLED"});
            this.cmbType.Location = new System.Drawing.Point(1065, 473);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(233, 25);
            this.cmbType.TabIndex = 108;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1061, 451);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 19);
            this.label4.TabIndex = 109;
            this.label4.Text = "TYPE";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(947, 451);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 19);
            this.label5.TabIndex = 111;
            this.label5.Text = "CUSTOMER";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.textBox1.Location = new System.Drawing.Point(951, 473);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(108, 25);
            this.textBox1.TabIndex = 112;
            this.textBox1.Text = "PMMA";
            // 
            // cbZeroCost
            // 
            this.cbZeroCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbZeroCost.AutoSize = true;
            this.cbZeroCost.Location = new System.Drawing.Point(1328, 471);
            this.cbZeroCost.Name = "cbZeroCost";
            this.cbZeroCost.Size = new System.Drawing.Size(121, 27);
            this.cbZeroCost.TabIndex = 113;
            this.cbZeroCost.Text = "ZERO COST";
            this.cbZeroCost.UseVisualStyleBackColor = true;
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.cbZeroCost);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cmbStatusSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrdSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.dgvOrderAlert);
            this.Controls.Add(this.dgvOrd);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmOrder";
            this.Text = "Order";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOrder_FormClosed);
            this.Load += new System.EventHandler(this.frmOrder_Load);
            this.Click += new System.EventHandler(this.frmOrder_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderAlert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvOrd;
        private System.Windows.Forms.DataGridView dgvOrderAlert;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.TextBox txtOrdSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbStatusSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_added_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_required_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_pending;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_received;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_status;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label5;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox cbZeroCost;
    }
}