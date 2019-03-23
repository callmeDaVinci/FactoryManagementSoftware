﻿namespace FactoryManagementSoftware.UI
{
    partial class frmStockReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TYPE = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.btnExportAllToExcel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSubType = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvStockReport = new System.Windows.Forms.DataGridView();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCheck = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockReport)).BeginInit();
            this.SuspendLayout();
            // 
            // TYPE
            // 
            this.TYPE.AutoSize = true;
            this.TYPE.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TYPE.Location = new System.Drawing.Point(23, 31);
            this.TYPE.Name = "TYPE";
            this.TYPE.Size = new System.Drawing.Size(47, 23);
            this.TYPE.TabIndex = 34;
            this.TYPE.Text = "TYPE";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(27, 58);
            this.cmbType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(188, 36);
            this.cmbType.TabIndex = 33;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // btnExportAllToExcel
            // 
            this.btnExportAllToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAllToExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExportAllToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportAllToExcel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportAllToExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExportAllToExcel.Location = new System.Drawing.Point(1457, 44);
            this.btnExportAllToExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportAllToExcel.Name = "btnExportAllToExcel";
            this.btnExportAllToExcel.Size = new System.Drawing.Size(92, 50);
            this.btnExportAllToExcel.TabIndex = 74;
            this.btnExportAllToExcel.Text = "ALL";
            this.btnExportAllToExcel.UseVisualStyleBackColor = false;
            this.btnExportAllToExcel.Click += new System.EventHandler(this.btnExportAllToExcel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(233, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 23);
            this.label2.TabIndex = 76;
            this.label2.Text = "SUB TYPE";
            // 
            // cmbSubType
            // 
            this.cmbSubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubType.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbSubType.FormattingEnabled = true;
            this.cmbSubType.Location = new System.Drawing.Point(237, 58);
            this.cmbSubType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSubType.Name = "cmbSubType";
            this.cmbSubType.Size = new System.Drawing.Size(287, 36);
            this.cmbSubType.TabIndex = 75;
            this.cmbSubType.SelectedIndexChanged += new System.EventHandler(this.cmbSubType_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(1322, 44);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 52);
            this.button1.TabIndex = 77;
            this.button1.Text = "EXCEL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // dgvStockReport
            // 
            this.dgvStockReport.AllowUserToAddRows = false;
            this.dgvStockReport.AllowUserToDeleteRows = false;
            this.dgvStockReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStockReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvStockReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStockReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStockReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockReport.EnableHeadersVisualStyles = false;
            this.dgvStockReport.Location = new System.Drawing.Point(27, 124);
            this.dgvStockReport.Name = "dgvStockReport";
            this.dgvStockReport.RowHeadersVisible = false;
            this.dgvStockReport.RowTemplate.Height = 24;
            this.dgvStockReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvStockReport.Size = new System.Drawing.Size(1522, 704);
            this.dgvStockReport.TabIndex = 78;
            this.dgvStockReport.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockReport_CellEndEdit);
            this.dgvStockReport.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvStockReport_CellValidating);
            this.dgvStockReport.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvStockReport_DefaultValuesNeeded);
            this.dgvStockReport.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvStockReport_EditingControlShowing);
            this.dgvStockReport.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvStockReport_SortCompare);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(27, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1522, 23);
            this.progressBar1.TabIndex = 79;
            this.progressBar1.Visible = false;
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(549, 51);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(92, 45);
            this.btnCheck.TabIndex = 101;
            this.btnCheck.Text = "CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // frmStockReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dgvStockReport);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSubType);
            this.Controls.Add(this.btnExportAllToExcel);
            this.Controls.Add(this.TYPE);
            this.Controls.Add(this.cmbType);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmStockReport";
            this.Text = "Stock Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmStockReport_FormClosed);
            this.Load += new System.EventHandler(this.frmStockReport_Load);
            this.Click += new System.EventHandler(this.frmStockReport_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TYPE;
        public System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button btnExportAllToExcel;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cmbSubType;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvStockReport;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCheck;
    }
}