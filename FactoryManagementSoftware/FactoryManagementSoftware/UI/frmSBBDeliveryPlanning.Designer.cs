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
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.cmbEditUnit = new System.Windows.Forms.ComboBox();
            this.cbMergePO = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFillALL = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblAssemblyNeeded = new System.Windows.Forms.Label();
            this.lblProductionAlert = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvList.BackgroundColor = System.Drawing.Color.White;
            this.dgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.GridColor = System.Drawing.Color.LightGray;
            this.dgvList.Location = new System.Drawing.Point(21, 139);
            this.dgvList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowTemplate.Height = 50;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvList.Size = new System.Drawing.Size(1303, 556);
            this.dgvList.TabIndex = 179;
            this.dgvList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellEndEdit);
            this.dgvList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvList_CellFormatting);
            this.dgvList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvList_EditingControlShowing);
            // 
            // cmbEditUnit
            // 
            this.cmbEditUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEditUnit.FormattingEnabled = true;
            this.cmbEditUnit.Location = new System.Drawing.Point(14, 42);
            this.cmbEditUnit.Name = "cmbEditUnit";
            this.cmbEditUnit.Size = new System.Drawing.Size(184, 20);
            this.cmbEditUnit.TabIndex = 180;
            this.cmbEditUnit.SelectedIndexChanged += new System.EventHandler(this.cmbEditUnit_SelectedIndexChanged);
            // 
            // cbMergePO
            // 
            this.cbMergePO.AutoSize = true;
            this.cbMergePO.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMergePO.Location = new System.Drawing.Point(223, 39);
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
            this.label1.Location = new System.Drawing.Point(19, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 12);
            this.label1.TabIndex = 183;
            this.label1.Text = "PLANNING LIST";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 19);
            this.label2.TabIndex = 184;
            this.label2.Text = "UNIT";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbEditUnit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbMergePO);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(21, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1303, 78);
            this.groupBox1.TabIndex = 185;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SETTING";
            // 
            // btnFillALL
            // 
            this.btnFillALL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFillALL.BackColor = System.Drawing.Color.White;
            this.btnFillALL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFillALL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFillALL.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFillALL.ForeColor = System.Drawing.Color.Black;
            this.btnFillALL.Location = new System.Drawing.Point(1200, 102);
            this.btnFillALL.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnFillALL.Name = "btnFillALL";
            this.btnFillALL.Size = new System.Drawing.Size(124, 32);
            this.btnFillALL.TabIndex = 186;
            this.btnFillALL.Text = "FILL ALL";
            this.btnFillALL.UseVisualStyleBackColor = false;
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
            this.btnUpdate.Location = new System.Drawing.Point(1071, 102);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(1);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(124, 32);
            this.btnUpdate.TabIndex = 187;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblAssemblyNeeded
            // 
            this.lblAssemblyNeeded.AutoSize = true;
            this.lblAssemblyNeeded.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssemblyNeeded.ForeColor = System.Drawing.Color.Red;
            this.lblAssemblyNeeded.Location = new System.Drawing.Point(103, 115);
            this.lblAssemblyNeeded.Name = "lblAssemblyNeeded";
            this.lblAssemblyNeeded.Size = new System.Drawing.Size(145, 19);
            this.lblAssemblyNeeded.TabIndex = 188;
            this.lblAssemblyNeeded.Text = "ASSEMBLY NEEDED !!!";
            this.lblAssemblyNeeded.Visible = false;
            // 
            // lblProductionAlert
            // 
            this.lblProductionAlert.AutoSize = true;
            this.lblProductionAlert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductionAlert.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductionAlert.ForeColor = System.Drawing.Color.Red;
            this.lblProductionAlert.Location = new System.Drawing.Point(280, 115);
            this.lblProductionAlert.Name = "lblProductionAlert";
            this.lblProductionAlert.Size = new System.Drawing.Size(168, 19);
            this.lblProductionAlert.TabIndex = 189;
            this.lblProductionAlert.Text = "PRODUCTION NEEDED !!!";
            this.lblProductionAlert.Visible = false;
            this.lblProductionAlert.Click += new System.EventHandler(this.lblProductionAlert_Click);
            // 
            // frmSBBDeliveryPlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.lblProductionAlert);
            this.Controls.Add(this.lblAssemblyNeeded);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnFillALL);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvList);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSBBDeliveryPlanning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSBBDeliveryPlanning";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSBBDeliveryPlanning_FormClosing);
            this.Load += new System.EventHandler(this.frmSBBDeliveryPlanning_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.ComboBox cmbEditUnit;
        private System.Windows.Forms.CheckBox cbMergePO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFillALL;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblAssemblyNeeded;
        private System.Windows.Forms.Label lblProductionAlert;
    }
}