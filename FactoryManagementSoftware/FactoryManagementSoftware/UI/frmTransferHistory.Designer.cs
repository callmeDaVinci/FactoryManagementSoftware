namespace FactoryManagementSoftware.UI
{
    partial class frmTransferHistory
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
            this.dgvTrf = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.cmbTrfToCategory = new System.Windows.Forms.ComboBox();
            this.cmbTrfTo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblItemCategory = new System.Windows.Forms.Label();
            this.cmbTrfFromCategory = new System.Windows.Forms.ComboBox();
            this.cmbTrfFrom = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbTrfItemCat = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbAddedType = new System.Windows.Forms.CheckBox();
            this.cbTransferType = new System.Windows.Forms.CheckBox();
            this.cbCalculateTotal = new System.Windows.Forms.CheckBox();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrf)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTrf
            // 
            this.dgvTrf.AllowUserToAddRows = false;
            this.dgvTrf.AllowUserToDeleteRows = false;
            this.dgvTrf.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.dgvTrf.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTrf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTrf.BackgroundColor = System.Drawing.Color.White;
            this.dgvTrf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTrf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvTrf.GridColor = System.Drawing.SystemColors.Control;
            this.dgvTrf.Location = new System.Drawing.Point(2, 30);
            this.dgvTrf.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTrf.Name = "dgvTrf";
            this.dgvTrf.ReadOnly = true;
            this.dgvTrf.RowHeadersVisible = false;
            this.dgvTrf.RowTemplate.Height = 40;
            this.dgvTrf.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrf.Size = new System.Drawing.Size(1521, 583);
            this.dgvTrf.TabIndex = 37;
            this.dgvTrf.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTrf_CellFormatting);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 19);
            this.label3.TabIndex = 62;
            this.label3.Text = "TRANSFER HISTORY";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.dgvTrf, 0, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(30, 226);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.561403F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.4386F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1525, 615);
            this.tableLayoutPanel5.TabIndex = 88;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbTransferType);
            this.groupBox1.Controls.Add(this.cbAddedType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.cmbTrfItemCat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbTrfToCategory);
            this.groupBox1.Controls.Add(this.cmbTrfTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblItemCategory);
            this.groupBox1.Controls.Add(this.cmbTrfFromCategory);
            this.groupBox1.Controls.Add(this.cmbTrfFrom);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Location = new System.Drawing.Point(30, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1525, 134);
            this.groupBox1.TabIndex = 89;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTER";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.txtSearch.Location = new System.Drawing.Point(1125, 59);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(394, 25);
            this.txtSearch.TabIndex = 86;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1125, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 19);
            this.label5.TabIndex = 85;
            this.label5.Text = "SEARCH (BY CODE/NAME/ID)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(201, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 19);
            this.label1.TabIndex = 74;
            this.label1.Text = "END DATE";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "ddMMMMyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(205, 59);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(186, 25);
            this.dtpEndDate.TabIndex = 73;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // cmbTrfToCategory
            // 
            this.cmbTrfToCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfToCategory.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfToCategory.FormattingEnabled = true;
            this.cmbTrfToCategory.Location = new System.Drawing.Point(642, 59);
            this.cmbTrfToCategory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfToCategory.Name = "cmbTrfToCategory";
            this.cmbTrfToCategory.Size = new System.Drawing.Size(184, 25);
            this.cmbTrfToCategory.TabIndex = 72;
            this.cmbTrfToCategory.SelectedIndexChanged += new System.EventHandler(this.cmbTrfToCategory_SelectedIndexChanged);
            // 
            // cmbTrfTo
            // 
            this.cmbTrfTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfTo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfTo.FormattingEnabled = true;
            this.cmbTrfTo.Location = new System.Drawing.Point(642, 93);
            this.cmbTrfTo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfTo.Name = "cmbTrfTo";
            this.cmbTrfTo.Size = new System.Drawing.Size(184, 25);
            this.cmbTrfTo.TabIndex = 71;
            this.cmbTrfTo.SelectedIndexChanged += new System.EventHandler(this.cmbTrfTo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(642, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 19);
            this.label2.TabIndex = 70;
            this.label2.Text = "TO";
            // 
            // lblItemCategory
            // 
            this.lblItemCategory.AutoSize = true;
            this.lblItemCategory.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCategory.Location = new System.Drawing.Point(432, 36);
            this.lblItemCategory.Name = "lblItemCategory";
            this.lblItemCategory.Size = new System.Drawing.Size(48, 19);
            this.lblItemCategory.TabIndex = 67;
            this.lblItemCategory.Text = "FROM";
            // 
            // cmbTrfFromCategory
            // 
            this.cmbTrfFromCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfFromCategory.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfFromCategory.FormattingEnabled = true;
            this.cmbTrfFromCategory.Location = new System.Drawing.Point(436, 59);
            this.cmbTrfFromCategory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfFromCategory.Name = "cmbTrfFromCategory";
            this.cmbTrfFromCategory.Size = new System.Drawing.Size(184, 25);
            this.cmbTrfFromCategory.TabIndex = 69;
            this.cmbTrfFromCategory.SelectedIndexChanged += new System.EventHandler(this.cmbTrfFromCategory_SelectedIndexChanged);
            // 
            // cmbTrfFrom
            // 
            this.cmbTrfFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfFrom.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfFrom.FormattingEnabled = true;
            this.cmbTrfFrom.Location = new System.Drawing.Point(436, 92);
            this.cmbTrfFrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfFrom.Name = "cmbTrfFrom";
            this.cmbTrfFrom.Size = new System.Drawing.Size(184, 25);
            this.cmbTrfFrom.TabIndex = 68;
            this.cmbTrfFrom.SelectedIndexChanged += new System.EventHandler(this.cmbTrfFrom_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 19);
            this.label7.TabIndex = 65;
            this.label7.Text = "START DATE";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "ddMMMMyy";
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(12, 59);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(186, 25);
            this.dtpStartDate.TabIndex = 66;
            this.dtpStartDate.Value = new System.DateTime(2019, 2, 28, 0, 0, 0, 0);
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // cmbTrfItemCat
            // 
            this.cmbTrfItemCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrfItemCat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrfItemCat.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbTrfItemCat.FormattingEnabled = true;
            this.cmbTrfItemCat.Location = new System.Drawing.Point(871, 59);
            this.cmbTrfItemCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTrfItemCat.Name = "cmbTrfItemCat";
            this.cmbTrfItemCat.Size = new System.Drawing.Size(239, 25);
            this.cmbTrfItemCat.TabIndex = 57;
            this.cmbTrfItemCat.SelectedIndexChanged += new System.EventHandler(this.cmbTrfItemCat_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(870, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 19);
            this.label4.TabIndex = 58;
            this.label4.Text = "CATEGORY";
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(1435, 167);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(120, 50);
            this.btnCheck.TabIndex = 102;
            this.btnCheck.Text = "APPLY";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 19);
            this.label6.TabIndex = 87;
            this.label6.Text = "DATE TYPE:";
            // 
            // cbAddedType
            // 
            this.cbAddedType.AutoSize = true;
            this.cbAddedType.Location = new System.Drawing.Point(92, 98);
            this.cbAddedType.Name = "cbAddedType";
            this.cbAddedType.Size = new System.Drawing.Size(77, 23);
            this.cbAddedType.TabIndex = 88;
            this.cbAddedType.Text = "ADDED";
            this.cbAddedType.UseVisualStyleBackColor = true;
            this.cbAddedType.CheckedChanged += new System.EventHandler(this.cbAddedType_CheckedChanged);
            // 
            // cbTransferType
            // 
            this.cbTransferType.AutoSize = true;
            this.cbTransferType.Checked = true;
            this.cbTransferType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTransferType.Location = new System.Drawing.Point(176, 98);
            this.cbTransferType.Name = "cbTransferType";
            this.cbTransferType.Size = new System.Drawing.Size(94, 23);
            this.cbTransferType.TabIndex = 89;
            this.cbTransferType.Text = "TRANSFER";
            this.cbTransferType.UseVisualStyleBackColor = true;
            this.cbTransferType.CheckedChanged += new System.EventHandler(this.cbTransferType_CheckedChanged);
            // 
            // cbCalculateTotal
            // 
            this.cbCalculateTotal.AutoSize = true;
            this.cbCalculateTotal.Location = new System.Drawing.Point(30, 197);
            this.cbCalculateTotal.Name = "cbCalculateTotal";
            this.cbCalculateTotal.Size = new System.Drawing.Size(178, 23);
            this.cbCalculateTotal.TabIndex = 90;
            this.cbCalculateTotal.Text = "CALCULATE TOTAL QTY:";
            this.cbCalculateTotal.UseVisualStyleBackColor = true;
            this.cbCalculateTotal.CheckedChanged += new System.EventHandler(this.cbCalculateTotal_CheckedChanged);
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQty.Location = new System.Drawing.Point(214, 188);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(296, 32);
            this.lblTotalQty.TabIndex = 103;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label8.Location = new System.Drawing.Point(1447, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 19);
            this.label8.TabIndex = 90;
            this.label8.Text = "RESET ALL";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label9.Location = new System.Drawing.Point(575, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 19);
            this.label9.TabIndex = 91;
            this.label9.Text = "RESET";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label10.Location = new System.Drawing.Point(781, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 19);
            this.label10.TabIndex = 92;
            this.label10.Text = "RESET";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // frmTransferHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.lblTotalQty);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.cbCalculateTotal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmTransferHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTransferHistory";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTransferHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrf)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTrf;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cmbTrfItemCat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblItemCategory;
        private System.Windows.Forms.ComboBox cmbTrfFromCategory;
        private System.Windows.Forms.ComboBox cmbTrfFrom;
        private System.Windows.Forms.ComboBox cmbTrfToCategory;
        private System.Windows.Forms.ComboBox cmbTrfTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.CheckBox cbTransferType;
        private System.Windows.Forms.CheckBox cbAddedType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbCalculateTotal;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
    }
}