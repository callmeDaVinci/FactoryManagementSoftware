namespace FactoryManagementSoftware.UI
{
    partial class frmSBBDeliveryPlanning
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
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.cmbEditUnit = new System.Windows.Forms.ComboBox();
            this.cbMergePO = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFillSettingReset = new System.Windows.Forms.Label();
            this.cbFillALL = new System.Windows.Forms.CheckBox();
            this.cmbPOSortBy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbProductSortBy = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFillALL = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblAssemblyNeeded = new System.Windows.Forms.Label();
            this.lblProductionAlert = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOpenDO = new System.Windows.Forms.Button();
            this.lblTotalBag = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.AllowUserToResizeColumns = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.BackgroundColor = System.Drawing.Color.White;
            this.dgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.GridColor = System.Drawing.Color.LightGray;
            this.dgvList.Location = new System.Drawing.Point(21, 139);
            this.dgvList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowTemplate.Height = 50;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvList.Size = new System.Drawing.Size(1303, 556);
            this.dgvList.TabIndex = 179;
            this.dgvList.MultiSelectChanged += new System.EventHandler(this.dgvList_MultiSelectChanged);
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellEndEdit);
            this.dgvList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvList_CellMouseDown);
            this.dgvList.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvList_ColumnDisplayIndexChanged);
            this.dgvList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvList_ColumnHeaderMouseClick);
            this.dgvList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvList_EditingControlShowing);
            this.dgvList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvList_MouseClick);
            // 
            // cmbEditUnit
            // 
            this.cmbEditUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditUnit.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbEditUnit.FormattingEnabled = true;
            this.cmbEditUnit.Location = new System.Drawing.Point(428, 40);
            this.cmbEditUnit.Name = "cmbEditUnit";
            this.cmbEditUnit.Size = new System.Drawing.Size(184, 25);
            this.cmbEditUnit.TabIndex = 180;
            this.cmbEditUnit.SelectedIndexChanged += new System.EventHandler(this.cmbEditUnit_SelectedIndexChanged);
            // 
            // cbMergePO
            // 
            this.cbMergePO.AutoSize = true;
            this.cbMergePO.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMergePO.Location = new System.Drawing.Point(657, 42);
            this.cbMergePO.Name = "cbMergePO";
            this.cbMergePO.Size = new System.Drawing.Size(252, 23);
            this.cbMergePO.TabIndex = 182;
            this.cbMergePO.Text = "MERGE PO WITH SAME CUSTOMER";
            this.cbMergePO.UseVisualStyleBackColor = true;
            this.cbMergePO.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 12);
            this.label1.TabIndex = 183;
            this.label1.Text = "PLANNING LIST";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(424, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 19);
            this.label2.TabIndex = 184;
            this.label2.Text = "UNIT";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblFillSettingReset);
            this.groupBox1.Controls.Add(this.cbFillALL);
            this.groupBox1.Controls.Add(this.cmbPOSortBy);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbProductSortBy);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbEditUnit);
            this.groupBox1.Controls.Add(this.btnFillALL);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbMergePO);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(21, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1303, 78);
            this.groupBox1.TabIndex = 185;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SETTING";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lblFillSettingReset
            // 
            this.lblFillSettingReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFillSettingReset.AutoSize = true;
            this.lblFillSettingReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFillSettingReset.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFillSettingReset.ForeColor = System.Drawing.Color.Blue;
            this.lblFillSettingReset.Location = new System.Drawing.Point(1253, 20);
            this.lblFillSettingReset.Name = "lblFillSettingReset";
            this.lblFillSettingReset.Size = new System.Drawing.Size(38, 15);
            this.lblFillSettingReset.TabIndex = 192;
            this.lblFillSettingReset.Text = "RESET";
            this.lblFillSettingReset.Visible = false;
            this.lblFillSettingReset.Click += new System.EventHandler(this.lblFillSettingReset_Click);
            // 
            // cbFillALL
            // 
            this.cbFillALL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFillALL.AutoSize = true;
            this.cbFillALL.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFillALL.Location = new System.Drawing.Point(1145, 17);
            this.cbFillALL.Name = "cbFillALL";
            this.cbFillALL.Size = new System.Drawing.Size(83, 23);
            this.cbFillALL.TabIndex = 191;
            this.cbFillALL.Text = "FILL ALL";
            this.cbFillALL.UseVisualStyleBackColor = true;
            this.cbFillALL.Visible = false;
            this.cbFillALL.CheckedChanged += new System.EventHandler(this.cbFillALL_CheckedChanged);
            // 
            // cmbPOSortBy
            // 
            this.cmbPOSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPOSortBy.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbPOSortBy.FormattingEnabled = true;
            this.cmbPOSortBy.Location = new System.Drawing.Point(220, 40);
            this.cmbPOSortBy.Name = "cmbPOSortBy";
            this.cmbPOSortBy.Size = new System.Drawing.Size(184, 25);
            this.cmbPOSortBy.TabIndex = 187;
            this.cmbPOSortBy.SelectedIndexChanged += new System.EventHandler(this.cmbPOSortBy_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(216, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 19);
            this.label4.TabIndex = 188;
            this.label4.Text = "P/O SORT BY";
            // 
            // cmbProductSortBy
            // 
            this.cmbProductSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductSortBy.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cmbProductSortBy.FormattingEnabled = true;
            this.cmbProductSortBy.Location = new System.Drawing.Point(13, 40);
            this.cmbProductSortBy.Name = "cmbProductSortBy";
            this.cmbProductSortBy.Size = new System.Drawing.Size(184, 25);
            this.cmbProductSortBy.TabIndex = 185;
            this.cmbProductSortBy.SelectedIndexChanged += new System.EventHandler(this.cmbProductSortBy_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 19);
            this.label3.TabIndex = 186;
            this.label3.Text = "PRODUCT SORT BY";
            // 
            // btnFillALL
            // 
            this.btnFillALL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFillALL.BackColor = System.Drawing.Color.White;
            this.btnFillALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFillALL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFillALL.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFillALL.ForeColor = System.Drawing.Color.Black;
            this.btnFillALL.Location = new System.Drawing.Point(1145, 40);
            this.btnFillALL.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnFillALL.Name = "btnFillALL";
            this.btnFillALL.Size = new System.Drawing.Size(151, 32);
            this.btnFillALL.TabIndex = 186;
            this.btnFillALL.Text = "FILL BY SYSTEM";
            this.btnFillALL.UseVisualStyleBackColor = false;
            this.btnFillALL.Visible = false;
            this.btnFillALL.Click += new System.EventHandler(this.btnFillALL_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(160)))), ((int)(((byte)(225)))));
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdate.Location = new System.Drawing.Point(1200, 102);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(1);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(124, 32);
            this.btnUpdate.TabIndex = 187;
            this.btnUpdate.Text = "SAVE";
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblAssemblyNeeded
            // 
            this.lblAssemblyNeeded.AutoSize = true;
            this.lblAssemblyNeeded.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAssemblyNeeded.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Underline);
            this.lblAssemblyNeeded.ForeColor = System.Drawing.Color.Red;
            this.lblAssemblyNeeded.Location = new System.Drawing.Point(146, 120);
            this.lblAssemblyNeeded.Name = "lblAssemblyNeeded";
            this.lblAssemblyNeeded.Size = new System.Drawing.Size(121, 15);
            this.lblAssemblyNeeded.TabIndex = 188;
            this.lblAssemblyNeeded.Text = "ASSEMBLY NEEDED !!!";
            this.lblAssemblyNeeded.Visible = false;
            this.lblAssemblyNeeded.Click += new System.EventHandler(this.lblAssemblyNeeded_Click);
            // 
            // lblProductionAlert
            // 
            this.lblProductionAlert.AutoSize = true;
            this.lblProductionAlert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductionAlert.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Underline);
            this.lblProductionAlert.ForeColor = System.Drawing.Color.Red;
            this.lblProductionAlert.Location = new System.Drawing.Point(146, 96);
            this.lblProductionAlert.Name = "lblProductionAlert";
            this.lblProductionAlert.Size = new System.Drawing.Size(139, 15);
            this.lblProductionAlert.TabIndex = 189;
            this.lblProductionAlert.Text = "PRODUCTION NEEDED !!!";
            this.lblProductionAlert.Visible = false;
            this.lblProductionAlert.Click += new System.EventHandler(this.lblProductionAlert_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_refresh_480;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(21, 100);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(35, 36);
            this.btnRefresh.TabIndex = 190;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOpenDO
            // 
            this.btnOpenDO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenDO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnOpenDO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenDO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenDO.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenDO.ForeColor = System.Drawing.Color.Black;
            this.btnOpenDO.Location = new System.Drawing.Point(1044, 102);
            this.btnOpenDO.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnOpenDO.Name = "btnOpenDO";
            this.btnOpenDO.Size = new System.Drawing.Size(151, 32);
            this.btnOpenDO.TabIndex = 192;
            this.btnOpenDO.Text = "ADD D/O";
            this.btnOpenDO.UseVisualStyleBackColor = false;
            this.btnOpenDO.Visible = false;
            this.btnOpenDO.Click += new System.EventHandler(this.btnOpenDO_Click);
            // 
            // lblTotalBag
            // 
            this.lblTotalBag.AutoSize = true;
            this.lblTotalBag.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBag.Location = new System.Drawing.Point(538, 111);
            this.lblTotalBag.Name = "lblTotalBag";
            this.lblTotalBag.Size = new System.Drawing.Size(221, 23);
            this.lblTotalBag.TabIndex = 193;
            this.lblTotalBag.Text = "0 BAG(s) / 0 PCS  SELECTED";
            // 
            // frmSBBDeliveryPlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.lblTotalBag);
            this.Controls.Add(this.btnOpenDO);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblProductionAlert);
            this.Controls.Add(this.lblAssemblyNeeded);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvList);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSBBDeliveryPlanning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "P/O VS STOCK";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSBBDeliveryPlanning_FormClosing);
            this.Load += new System.EventHandler(this.frmSBBDeliveryPlanning_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbEditUnit;
        private System.Windows.Forms.CheckBox cbMergePO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFillALL;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblAssemblyNeeded;
        private System.Windows.Forms.Label lblProductionAlert;
        private System.Windows.Forms.ComboBox cmbPOSortBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbProductSortBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox cbFillALL;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnOpenDO;
        private System.Windows.Forms.Label lblFillSettingReset;
        private System.Windows.Forms.Label lblTotalBag;
    }
}