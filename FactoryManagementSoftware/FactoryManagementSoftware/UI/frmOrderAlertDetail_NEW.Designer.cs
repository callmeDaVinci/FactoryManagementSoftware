namespace FactoryManagementSoftware.UI
{
    partial class frmOrderAlertDetail_NEW
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvMaterialForecastInfo = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.tlpFilter = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbMonthYear = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbShowStillNeedQty = new System.Windows.Forms.CheckBox();
            this.cbShowForecastQty = new System.Windows.Forms.CheckBox();
            this.cbShowStock = new System.Windows.Forms.CheckBox();
            this.cbShowDeliveredQty = new System.Windows.Forms.CheckBox();
            this.btnFilterApply = new System.Windows.Forms.Button();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialForecastInfo)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tlpFilter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMaterialForecastInfo
            // 
            this.dgvMaterialForecastInfo.AllowUserToAddRows = false;
            this.dgvMaterialForecastInfo.AllowUserToDeleteRows = false;
            this.dgvMaterialForecastInfo.AllowUserToOrderColumns = true;
            this.dgvMaterialForecastInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMaterialForecastInfo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMaterialForecastInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterialForecastInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvMaterialForecastInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialForecastInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMaterialForecastInfo.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvMaterialForecastInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterialForecastInfo.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvMaterialForecastInfo.Location = new System.Drawing.Point(4, 159);
            this.dgvMaterialForecastInfo.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvMaterialForecastInfo.Name = "dgvMaterialForecastInfo";
            this.dgvMaterialForecastInfo.ReadOnly = true;
            this.dgvMaterialForecastInfo.RowHeadersVisible = false;
            this.dgvMaterialForecastInfo.RowHeadersWidth = 51;
            this.dgvMaterialForecastInfo.RowTemplate.Height = 60;
            this.dgvMaterialForecastInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterialForecastInfo.Size = new System.Drawing.Size(1255, 544);
            this.dgvMaterialForecastInfo.TabIndex = 152;
            this.dgvMaterialForecastInfo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMaterialForecastInfo_CellFormatting);
            this.dgvMaterialForecastInfo.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMaterialForecastInfo_DataBindingComplete);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 206F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 358F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblTotal, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 114);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1263, 44);
            this.tableLayoutPanel4.TabIndex = 162;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 17);
            this.label1.TabIndex = 168;
            this.label1.Text = "Material Forecast Detail List";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(1126, 21);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(134, 23);
            this.lblTotal.TabIndex = 155;
            this.lblTotal.Text = "TOTAL: 1000 KG";
            // 
            // tlpFilter
            // 
            this.tlpFilter.ColumnCount = 3;
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tlpFilter.Controls.Add(this.groupBox1, 0, 0);
            this.tlpFilter.Controls.Add(this.groupBox2, 1, 0);
            this.tlpFilter.Controls.Add(this.btnFilterApply, 2, 0);
            this.tlpFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilter.Location = new System.Drawing.Point(3, 40);
            this.tlpFilter.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.tlpFilter.Name = "tlpFilter";
            this.tlpFilter.RowCount = 1;
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.Size = new System.Drawing.Size(1260, 71);
            this.tlpFilter.TabIndex = 167;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbMonthYear);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 65);
            this.groupBox1.TabIndex = 166;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MONTH/YEAR";
            // 
            // cmbMonthYear
            // 
            this.cmbMonthYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthYear.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbMonthYear.FormattingEnabled = true;
            this.cmbMonthYear.Location = new System.Drawing.Point(19, 30);
            this.cmbMonthYear.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonthYear.Name = "cmbMonthYear";
            this.cmbMonthYear.Size = new System.Drawing.Size(96, 25);
            this.cmbMonthYear.TabIndex = 154;
            this.cmbMonthYear.SelectedIndexChanged += new System.EventHandler(this.cmbMonthYear_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbShowStillNeedQty);
            this.groupBox2.Controls.Add(this.cbShowForecastQty);
            this.groupBox2.Controls.Add(this.cbShowStock);
            this.groupBox2.Controls.Add(this.cbShowDeliveredQty);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(144, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(977, 65);
            this.groupBox2.TabIndex = 167;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FILTER";
            // 
            // cbShowStillNeedQty
            // 
            this.cbShowStillNeedQty.AutoSize = true;
            this.cbShowStillNeedQty.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowStillNeedQty.Checked = true;
            this.cbShowStillNeedQty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowStillNeedQty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowStillNeedQty.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowStillNeedQty.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowStillNeedQty.Location = new System.Drawing.Point(288, 30);
            this.cbShowStillNeedQty.Name = "cbShowStillNeedQty";
            this.cbShowStillNeedQty.Size = new System.Drawing.Size(133, 19);
            this.cbShowStillNeedQty.TabIndex = 160;
            this.cbShowStillNeedQty.Text = "Show Still Need Qty";
            this.cbShowStillNeedQty.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowStillNeedQty.UseVisualStyleBackColor = true;
            this.cbShowStillNeedQty.CheckedChanged += new System.EventHandler(this.cbShowStillNeedQty_CheckedChanged);
            // 
            // cbShowForecastQty
            // 
            this.cbShowForecastQty.AutoSize = true;
            this.cbShowForecastQty.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowForecastQty.Checked = true;
            this.cbShowForecastQty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowForecastQty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowForecastQty.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowForecastQty.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowForecastQty.Location = new System.Drawing.Point(17, 30);
            this.cbShowForecastQty.Name = "cbShowForecastQty";
            this.cbShowForecastQty.Size = new System.Drawing.Size(127, 19);
            this.cbShowForecastQty.TabIndex = 159;
            this.cbShowForecastQty.Text = "Show Forecast Qty";
            this.cbShowForecastQty.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowForecastQty.UseVisualStyleBackColor = true;
            this.cbShowForecastQty.CheckedChanged += new System.EventHandler(this.cbShowForecastQty_CheckedChanged);
            // 
            // cbShowStock
            // 
            this.cbShowStock.AutoSize = true;
            this.cbShowStock.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowStock.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowStock.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowStock.Location = new System.Drawing.Point(427, 30);
            this.cbShowStock.Name = "cbShowStock";
            this.cbShowStock.Size = new System.Drawing.Size(90, 19);
            this.cbShowStock.TabIndex = 158;
            this.cbShowStock.Text = "Show Stock";
            this.cbShowStock.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowStock.UseVisualStyleBackColor = true;
            this.cbShowStock.Visible = false;
            this.cbShowStock.CheckedChanged += new System.EventHandler(this.cbShowStock_CheckedChanged);
            // 
            // cbShowDeliveredQty
            // 
            this.cbShowDeliveredQty.AutoSize = true;
            this.cbShowDeliveredQty.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowDeliveredQty.Checked = true;
            this.cbShowDeliveredQty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowDeliveredQty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbShowDeliveredQty.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.cbShowDeliveredQty.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowDeliveredQty.Location = new System.Drawing.Point(150, 30);
            this.cbShowDeliveredQty.Name = "cbShowDeliveredQty";
            this.cbShowDeliveredQty.Size = new System.Drawing.Size(132, 19);
            this.cbShowDeliveredQty.TabIndex = 157;
            this.cbShowDeliveredQty.Text = "Show Delivered Qty";
            this.cbShowDeliveredQty.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbShowDeliveredQty.UseVisualStyleBackColor = true;
            this.cbShowDeliveredQty.CheckedChanged += new System.EventHandler(this.cbShowDeliveredQty_CheckedChanged);
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnFilterApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFilterApply.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterApply.ForeColor = System.Drawing.Color.White;
            this.btnFilterApply.Location = new System.Drawing.Point(1130, 30);
            this.btnFilterApply.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.Size = new System.Drawing.Size(125, 36);
            this.btnFilterApply.TabIndex = 145;
            this.btnFilterApply.Text = "APPLY";
            this.btnFilterApply.UseVisualStyleBackColor = false;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblMaterial, 0, 0);
            this.tlpMain.Controls.Add(this.dgvMaterialForecastInfo, 0, 3);
            this.tlpMain.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tlpMain.Controls.Add(this.tlpFilter, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(20, 20);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(20);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(1263, 704);
            this.tlpMain.TabIndex = 166;
            // 
            // lblMaterial
            // 
            this.lblMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this.lblMaterial.Location = new System.Drawing.Point(4, 9);
            this.lblMaterial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(108, 28);
            this.lblMaterial.TabIndex = 153;
            this.lblMaterial.Text = "code name";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tlpMain, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1303, 744);
            this.tableLayoutPanel2.TabIndex = 167;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightGray;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(209, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 17);
            this.label2.TabIndex = 169;
            this.label2.Text = "( Direct Use On (Parent) = Outgoing Product)";
            // 
            // frmOrderAlertDetail_NEW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1303, 744);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmOrderAlertDetail_NEW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Material Forecast Info";
            this.Load += new System.EventHandler(this.frmOrderAlertDetail_NEW_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialForecastInfo)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tlpFilter.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvMaterialForecastInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TableLayoutPanel tlpFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbMonthYear;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbShowDeliveredQty;
        private System.Windows.Forms.CheckBox cbShowStock;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button btnFilterApply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox cbShowStillNeedQty;
        private System.Windows.Forms.CheckBox cbShowForecastQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMaterial;
        private System.Windows.Forms.Label label2;
    }
}