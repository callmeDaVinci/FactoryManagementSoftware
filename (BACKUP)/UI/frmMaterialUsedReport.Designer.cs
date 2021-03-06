﻿namespace FactoryManagementSoftware.UI
{
    partial class frmMaterialUsedReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.dgvMaterialUsedRecord = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_ord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_part_weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.material_used = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_wastage_allowed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.material_used_include_wastage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_material_used = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.cmbCust = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cbZeroCost = new System.Windows.Forms.CheckBox();
            this.cbSubMat = new System.Windows.Forms.CheckBox();
            this.btnExportAllToExcel = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblSearchError = new System.Windows.Forms.Label();
            this.cbBySearch = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialUsedRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(559, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 78;
            this.label1.Text = "START";
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(907, 42);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(122, 50);
            this.btnCheck.TabIndex = 77;
            this.btnCheck.Text = "CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.button2_Click);
            // 
            // dgvMaterialUsedRecord
            // 
            this.dgvMaterialUsedRecord.AllowUserToAddRows = false;
            this.dgvMaterialUsedRecord.AllowUserToDeleteRows = false;
            this.dgvMaterialUsedRecord.AllowUserToResizeRows = false;
            this.dgvMaterialUsedRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMaterialUsedRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaterialUsedRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterialUsedRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMaterialUsedRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialUsedRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.item_material,
            this.item_name,
            this.item_code,
            this.item_color,
            this.item_ord,
            this.item_part_weight,
            this.material_used,
            this.item_wastage_allowed,
            this.material_used_include_wastage,
            this.total_material_used});
            this.dgvMaterialUsedRecord.GridColor = System.Drawing.SystemColors.Control;
            this.dgvMaterialUsedRecord.Location = new System.Drawing.Point(31, 155);
            this.dgvMaterialUsedRecord.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMaterialUsedRecord.Name = "dgvMaterialUsedRecord";
            this.dgvMaterialUsedRecord.ReadOnly = true;
            this.dgvMaterialUsedRecord.RowHeadersVisible = false;
            this.dgvMaterialUsedRecord.RowTemplate.Height = 40;
            this.dgvMaterialUsedRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterialUsedRecord.Size = new System.Drawing.Size(1511, 653);
            this.dgvMaterialUsedRecord.TabIndex = 74;
            // 
            // no
            // 
            this.no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.no.HeaderText = "No.";
            this.no.Name = "no";
            this.no.ReadOnly = true;
            this.no.Width = 66;
            // 
            // item_material
            // 
            this.item_material.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.item_material.HeaderText = "Material Type";
            this.item_material.Name = "item_material";
            this.item_material.ReadOnly = true;
            this.item_material.Width = 129;
            // 
            // item_name
            // 
            this.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_name.HeaderText = "Name";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            // 
            // item_code
            // 
            this.item_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_code.HeaderText = "Code";
            this.item_code.Name = "item_code";
            this.item_code.ReadOnly = true;
            // 
            // item_color
            // 
            this.item_color.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.item_color.HeaderText = "Color";
            this.item_color.MinimumWidth = 80;
            this.item_color.Name = "item_color";
            this.item_color.ReadOnly = true;
            this.item_color.Width = 80;
            // 
            // item_ord
            // 
            this.item_ord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.item_ord.DefaultCellStyle = dataGridViewCellStyle2;
            this.item_ord.HeaderText = "Quantity Order";
            this.item_ord.MinimumWidth = 100;
            this.item_ord.Name = "item_ord";
            this.item_ord.ReadOnly = true;
            // 
            // item_part_weight
            // 
            this.item_part_weight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.item_part_weight.DefaultCellStyle = dataGridViewCellStyle3;
            this.item_part_weight.HeaderText = "Item Weight (Grams)";
            this.item_part_weight.MinimumWidth = 140;
            this.item_part_weight.Name = "item_part_weight";
            this.item_part_weight.ReadOnly = true;
            this.item_part_weight.Width = 140;
            // 
            // material_used
            // 
            this.material_used.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.material_used.DefaultCellStyle = dataGridViewCellStyle4;
            this.material_used.HeaderText = "Material Used (Kg)";
            this.material_used.MinimumWidth = 100;
            this.material_used.Name = "material_used";
            this.material_used.ReadOnly = true;
            // 
            // item_wastage_allowed
            // 
            this.item_wastage_allowed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.item_wastage_allowed.DefaultCellStyle = dataGridViewCellStyle5;
            this.item_wastage_allowed.HeaderText = "Wastage Allowed (%)";
            this.item_wastage_allowed.MinimumWidth = 130;
            this.item_wastage_allowed.Name = "item_wastage_allowed";
            this.item_wastage_allowed.ReadOnly = true;
            this.item_wastage_allowed.Width = 130;
            // 
            // material_used_include_wastage
            // 
            this.material_used_include_wastage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N3";
            dataGridViewCellStyle6.NullValue = null;
            this.material_used_include_wastage.DefaultCellStyle = dataGridViewCellStyle6;
            this.material_used_include_wastage.HeaderText = "Material Used (Wastage Included)";
            this.material_used_include_wastage.MinimumWidth = 200;
            this.material_used_include_wastage.Name = "material_used_include_wastage";
            this.material_used_include_wastage.ReadOnly = true;
            this.material_used_include_wastage.Width = 200;
            // 
            // total_material_used
            // 
            this.total_material_used.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N3";
            dataGridViewCellStyle7.NullValue = null;
            this.total_material_used.DefaultCellStyle = dataGridViewCellStyle7;
            this.total_material_used.HeaderText = "TotalMaterial Used (Kg)";
            this.total_material_used.MinimumWidth = 150;
            this.total_material_used.Name = "total_material_used";
            this.total_material_used.ReadOnly = true;
            this.total_material_used.Width = 150;
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "dd-MM-yyyy";
            this.dtpStart.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(563, 59);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(162, 30);
            this.dtpStart.TabIndex = 83;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // cmbCust
            // 
            this.cmbCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCust.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCust.FormattingEnabled = true;
            this.cmbCust.Location = new System.Drawing.Point(291, 58);
            this.cmbCust.Name = "cmbCust";
            this.cmbCust.Size = new System.Drawing.Size(263, 31);
            this.cmbCust.TabIndex = 85;
            this.cmbCust.SelectedIndexChanged += new System.EventHandler(this.cmbCust_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(287, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 19);
            this.label2.TabIndex = 84;
            this.label2.Text = "CUSTOMER";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "dd-MM-yyyy";
            this.dtpEnd.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(740, 60);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(162, 30);
            this.dtpEnd.TabIndex = 87;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(736, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 19);
            this.label3.TabIndex = 86;
            this.label3.Text = "END";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportToExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExportToExcel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToExcel.ForeColor = System.Drawing.Color.Black;
            this.btnExportToExcel.Location = new System.Drawing.Point(1422, 42);
            this.btnExportToExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(120, 48);
            this.btnExportToExcel.TabIndex = 88;
            this.btnExportToExcel.Text = "EXCEL";
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(31, 59);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(254, 31);
            this.cmbType.TabIndex = 90;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 19);
            this.label4.TabIndex = 89;
            this.label4.Text = "TYPE";
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.Location = new System.Drawing.Point(27, 130);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(45, 19);
            this.lblMonth.TabIndex = 91;
            this.lblMonth.Text = "START";
            // 
            // cbZeroCost
            // 
            this.cbZeroCost.AutoSize = true;
            this.cbZeroCost.Checked = true;
            this.cbZeroCost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbZeroCost.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbZeroCost.Location = new System.Drawing.Point(131, 93);
            this.cbZeroCost.Name = "cbZeroCost";
            this.cbZeroCost.Size = new System.Drawing.Size(112, 24);
            this.cbZeroCost.TabIndex = 92;
            this.cbZeroCost.Text = "ZERO COST";
            this.cbZeroCost.UseVisualStyleBackColor = true;
            this.cbZeroCost.Visible = false;
            // 
            // cbSubMat
            // 
            this.cbSubMat.AutoSize = true;
            this.cbSubMat.Checked = true;
            this.cbSubMat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSubMat.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSubMat.Location = new System.Drawing.Point(31, 93);
            this.cbSubMat.Name = "cbSubMat";
            this.cbSubMat.Size = new System.Drawing.Size(94, 24);
            this.cbSubMat.TabIndex = 93;
            this.cbSubMat.Text = "SUB MAT";
            this.cbSubMat.UseVisualStyleBackColor = true;
            // 
            // btnExportAllToExcel
            // 
            this.btnExportAllToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAllToExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExportAllToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportAllToExcel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportAllToExcel.ForeColor = System.Drawing.Color.Black;
            this.btnExportAllToExcel.Location = new System.Drawing.Point(1298, 42);
            this.btnExportAllToExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportAllToExcel.Name = "btnExportAllToExcel";
            this.btnExportAllToExcel.Size = new System.Drawing.Size(120, 48);
            this.btnExportAllToExcel.TabIndex = 94;
            this.btnExportAllToExcel.Text = "ALL";
            this.btnExportAllToExcel.UseVisualStyleBackColor = false;
            this.btnExportAllToExcel.Click += new System.EventHandler(this.btnExportAllToExcel_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(563, 59);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(339, 30);
            this.txtSearch.TabIndex = 95;
            this.txtSearch.Visible = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(559, 37);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(59, 19);
            this.lblSearch.TabIndex = 96;
            this.lblSearch.Text = "SEARCH";
            this.lblSearch.Visible = false;
            // 
            // lblSearchError
            // 
            this.lblSearchError.AutoSize = true;
            this.lblSearchError.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchError.ForeColor = System.Drawing.Color.Red;
            this.lblSearchError.Location = new System.Drawing.Point(780, 92);
            this.lblSearchError.Name = "lblSearchError";
            this.lblSearchError.Size = new System.Drawing.Size(122, 23);
            this.lblSearchError.TabIndex = 97;
            this.lblSearchError.Text = "INVALID ITEM!";
            this.lblSearchError.Visible = false;
            // 
            // cbBySearch
            // 
            this.cbBySearch.AutoSize = true;
            this.cbBySearch.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBySearch.Location = new System.Drawing.Point(442, 34);
            this.cbBySearch.Name = "cbBySearch";
            this.cbBySearch.Size = new System.Drawing.Size(112, 24);
            this.cbBySearch.TabIndex = 98;
            this.cbBySearch.Text = "BY SEARCH";
            this.cbBySearch.UseVisualStyleBackColor = true;
            this.cbBySearch.CheckedChanged += new System.EventHandler(this.cbBySearch_CheckedChanged);
            // 
            // frmMaterialUsedReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.cbBySearch);
            this.Controls.Add(this.lblSearchError);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnExportAllToExcel);
            this.Controls.Add(this.cbSubMat);
            this.Controls.Add(this.cbZeroCost);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnExportToExcel);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCust);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.dgvMaterialUsedRecord);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMaterialUsedReport";
            this.Text = "Material Used Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMaterialUsedReport_FormClosed);
            this.Load += new System.EventHandler(this.frmMaterialUsedReport_Load);
            this.Click += new System.EventHandler(this.frmMaterialUsedReport_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialUsedRecord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.DataGridView dgvMaterialUsedRecord;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.ComboBox cmbCust;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_material;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_color;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_ord;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_part_weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn material_used;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_wastage_allowed;
        private System.Windows.Forms.DataGridViewTextBoxColumn material_used_include_wastage;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_material_used;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.CheckBox cbZeroCost;
        private System.Windows.Forms.CheckBox cbSubMat;
        private System.Windows.Forms.Button btnExportAllToExcel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblSearchError;
        private System.Windows.Forms.CheckBox cbBySearch;
    }
}