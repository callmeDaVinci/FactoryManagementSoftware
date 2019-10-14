namespace FactoryManagementSoftware.UI
{
    partial class frmMatCheckList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbMatChecklist = new System.Windows.Forms.GroupBox();
            this.dtpTrfDate = new System.Windows.Forms.DateTimePicker();
            this.cbCheckAll = new System.Windows.Forms.CheckBox();
            this.btnAutoInOut = new System.Windows.Forms.Button();
            this.btnItemCheck = new System.Windows.Forms.Button();
            this.tlpMatChecklist = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTransfer = new System.Windows.Forms.DataGridView();
            this.dgvDeliver = new System.Windows.Forms.DataGridView();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnDeliverChecklist = new System.Windows.Forms.Button();
            this.btnTrfChecklist = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTrfDate = new System.Windows.Forms.Label();
            this.gbMatChecklist.SuspendLayout();
            this.tlpMatChecklist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliver)).BeginInit();
            this.SuspendLayout();
            // 
            // gbMatChecklist
            // 
            this.gbMatChecklist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMatChecklist.Controls.Add(this.lblTrfDate);
            this.gbMatChecklist.Controls.Add(this.dtpTrfDate);
            this.gbMatChecklist.Controls.Add(this.cbCheckAll);
            this.gbMatChecklist.Controls.Add(this.btnAutoInOut);
            this.gbMatChecklist.Controls.Add(this.btnItemCheck);
            this.gbMatChecklist.Controls.Add(this.tlpMatChecklist);
            this.gbMatChecklist.Location = new System.Drawing.Point(16, 81);
            this.gbMatChecklist.Margin = new System.Windows.Forms.Padding(4);
            this.gbMatChecklist.Name = "gbMatChecklist";
            this.gbMatChecklist.Padding = new System.Windows.Forms.Padding(4);
            this.gbMatChecklist.Size = new System.Drawing.Size(1551, 760);
            this.gbMatChecklist.TabIndex = 137;
            this.gbMatChecklist.TabStop = false;
            this.gbMatChecklist.Text = "MATERIAL CHECKLIST";
            // 
            // dtpTrfDate
            // 
            this.dtpTrfDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpTrfDate.CustomFormat = "ddMMMMyy";
            this.dtpTrfDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTrfDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTrfDate.Location = new System.Drawing.Point(1023, 39);
            this.dtpTrfDate.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.dtpTrfDate.Name = "dtpTrfDate";
            this.dtpTrfDate.Size = new System.Drawing.Size(158, 25);
            this.dtpTrfDate.TabIndex = 141;
            this.dtpTrfDate.Visible = false;
            // 
            // cbCheckAll
            // 
            this.cbCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCheckAll.AutoSize = true;
            this.cbCheckAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCheckAll.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCheckAll.Location = new System.Drawing.Point(1404, 68);
            this.cbCheckAll.Name = "cbCheckAll";
            this.cbCheckAll.Size = new System.Drawing.Size(118, 23);
            this.cbCheckAll.TabIndex = 141;
            this.cbCheckAll.Text = "CHECKED ALL";
            this.cbCheckAll.UseVisualStyleBackColor = true;
            this.cbCheckAll.Visible = false;
            this.cbCheckAll.CheckedChanged += new System.EventHandler(this.cbCheckAll_CheckedChanged);
            // 
            // btnAutoInOut
            // 
            this.btnAutoInOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoInOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnAutoInOut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAutoInOut.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoInOut.ForeColor = System.Drawing.Color.White;
            this.btnAutoInOut.Location = new System.Drawing.Point(1188, 28);
            this.btnAutoInOut.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnAutoInOut.Name = "btnAutoInOut";
            this.btnAutoInOut.Size = new System.Drawing.Size(163, 36);
            this.btnAutoInOut.TabIndex = 142;
            this.btnAutoInOut.Text = "AUTO IN/OUT";
            this.btnAutoInOut.UseVisualStyleBackColor = false;
            this.btnAutoInOut.Visible = false;
            this.btnAutoInOut.Click += new System.EventHandler(this.btnAutoInOut_Click);
            // 
            // btnItemCheck
            // 
            this.btnItemCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnItemCheck.BackColor = System.Drawing.Color.White;
            this.btnItemCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemCheck.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemCheck.ForeColor = System.Drawing.Color.Black;
            this.btnItemCheck.Location = new System.Drawing.Point(1359, 28);
            this.btnItemCheck.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnItemCheck.Name = "btnItemCheck";
            this.btnItemCheck.Size = new System.Drawing.Size(163, 36);
            this.btnItemCheck.TabIndex = 141;
            this.btnItemCheck.Text = "ITEM CHECK";
            this.btnItemCheck.UseVisualStyleBackColor = false;
            this.btnItemCheck.Click += new System.EventHandler(this.btnItemCheck_Click);
            // 
            // tlpMatChecklist
            // 
            this.tlpMatChecklist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMatChecklist.ColumnCount = 2;
            this.tlpMatChecklist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMatChecklist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMatChecklist.Controls.Add(this.dgvTransfer, 0, 0);
            this.tlpMatChecklist.Controls.Add(this.dgvDeliver, 1, 0);
            this.tlpMatChecklist.Location = new System.Drawing.Point(21, 105);
            this.tlpMatChecklist.Margin = new System.Windows.Forms.Padding(4);
            this.tlpMatChecklist.Name = "tlpMatChecklist";
            this.tlpMatChecklist.RowCount = 1;
            this.tlpMatChecklist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMatChecklist.Size = new System.Drawing.Size(1505, 633);
            this.tlpMatChecklist.TabIndex = 136;
            // 
            // dgvTransfer
            // 
            this.dgvTransfer.AllowUserToAddRows = false;
            this.dgvTransfer.AllowUserToDeleteRows = false;
            this.dgvTransfer.AllowUserToOrderColumns = true;
            this.dgvTransfer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvTransfer.BackgroundColor = System.Drawing.Color.DimGray;
            this.dgvTransfer.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransfer.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTransfer.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransfer.GridColor = System.Drawing.Color.White;
            this.dgvTransfer.Location = new System.Drawing.Point(4, 1);
            this.dgvTransfer.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvTransfer.Name = "dgvTransfer";
            this.dgvTransfer.ReadOnly = true;
            this.dgvTransfer.RowHeadersVisible = false;
            this.dgvTransfer.RowTemplate.Height = 40;
            this.dgvTransfer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransfer.Size = new System.Drawing.Size(744, 631);
            this.dgvTransfer.TabIndex = 134;
            this.dgvTransfer.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTransfer_CellFormatting);
            // 
            // dgvDeliver
            // 
            this.dgvDeliver.AllowUserToAddRows = false;
            this.dgvDeliver.AllowUserToDeleteRows = false;
            this.dgvDeliver.AllowUserToOrderColumns = true;
            this.dgvDeliver.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDeliver.BackgroundColor = System.Drawing.Color.DimGray;
            this.dgvDeliver.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvDeliver.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeliver.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDeliver.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDeliver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDeliver.GridColor = System.Drawing.Color.White;
            this.dgvDeliver.Location = new System.Drawing.Point(756, 1);
            this.dgvDeliver.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvDeliver.Name = "dgvDeliver";
            this.dgvDeliver.ReadOnly = true;
            this.dgvDeliver.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDeliver.RowHeadersVisible = false;
            this.dgvDeliver.RowTemplate.Height = 40;
            this.dgvDeliver.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeliver.Size = new System.Drawing.Size(745, 631);
            this.dgvDeliver.TabIndex = 133;
            this.dgvDeliver.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeliver_CellClick);
            this.dgvDeliver.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDeliver_CellFormatting);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1373, 10);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(165, 36);
            this.btnExcel.TabIndex = 138;
            this.btnExcel.Text = "EXPORT TO EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnDeliverChecklist
            // 
            this.btnDeliverChecklist.BackColor = System.Drawing.Color.White;
            this.btnDeliverChecklist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeliverChecklist.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeliverChecklist.ForeColor = System.Drawing.Color.Black;
            this.btnDeliverChecklist.Location = new System.Drawing.Point(241, 10);
            this.btnDeliverChecklist.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnDeliverChecklist.Name = "btnDeliverChecklist";
            this.btnDeliverChecklist.Size = new System.Drawing.Size(163, 36);
            this.btnDeliverChecklist.TabIndex = 136;
            this.btnDeliverChecklist.Text = "DELIVER CHECKLIST";
            this.btnDeliverChecklist.UseVisualStyleBackColor = false;
            this.btnDeliverChecklist.Click += new System.EventHandler(this.btnDeliver_Click);
            // 
            // btnTrfChecklist
            // 
            this.btnTrfChecklist.BackColor = System.Drawing.Color.White;
            this.btnTrfChecklist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrfChecklist.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrfChecklist.ForeColor = System.Drawing.Color.Black;
            this.btnTrfChecklist.Location = new System.Drawing.Point(70, 10);
            this.btnTrfChecklist.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnTrfChecklist.Name = "btnTrfChecklist";
            this.btnTrfChecklist.Size = new System.Drawing.Size(163, 36);
            this.btnTrfChecklist.TabIndex = 135;
            this.btnTrfChecklist.Text = "TRANSFER CHECKLIST";
            this.btnTrfChecklist.UseVisualStyleBackColor = false;
            this.btnTrfChecklist.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_chevron_left_100;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(16, 10);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(36, 36);
            this.btnBack.TabIndex = 140;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTrfDate
            // 
            this.lblTrfDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrfDate.AutoSize = true;
            this.lblTrfDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrfDate.Location = new System.Drawing.Point(1019, 17);
            this.lblTrfDate.Name = "lblTrfDate";
            this.lblTrfDate.Size = new System.Drawing.Size(108, 19);
            this.lblTrfDate.TabIndex = 141;
            this.lblTrfDate.Text = "TRANSFER DATE";
            this.lblTrfDate.Visible = false;
            // 
            // frmMatCheckList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.gbMatChecklist);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnDeliverChecklist);
            this.Controls.Add(this.btnTrfChecklist);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMatCheckList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checklist";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMatCheckList_Load);
            this.gbMatChecklist.ResumeLayout(false);
            this.gbMatChecklist.PerformLayout();
            this.tlpMatChecklist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliver)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMatChecklist;
        private System.Windows.Forms.TableLayoutPanel tlpMatChecklist;
        private System.Windows.Forms.DataGridView dgvTransfer;
        private System.Windows.Forms.DataGridView dgvDeliver;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnDeliverChecklist;
        private System.Windows.Forms.Button btnTrfChecklist;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAutoInOut;
        private System.Windows.Forms.Button btnItemCheck;
        private System.Windows.Forms.CheckBox cbCheckAll;
        private System.Windows.Forms.DateTimePicker dtpTrfDate;
        private System.Windows.Forms.Label lblTrfDate;
    }
}