namespace FactoryManagementSoftware.UI
{
    partial class frmMacScheduleExcelDownload
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSTTypeColorMat = new System.Windows.Forms.CheckBox();
            this.cbSTTypePart = new System.Windows.Forms.CheckBox();
            this.cbSTTypeRawMat = new System.Windows.Forms.CheckBox();
            this.cbMacSchedule = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbStockTakeList = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(4, 362);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 1, 4, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(304, 36);
            this.btnCancel.TabIndex = 174;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancelDOMode_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDownload.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            this.btnDownload.Location = new System.Drawing.Point(4, 317);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(304, 36);
            this.btnDownload.TabIndex = 173;
            this.btnDownload.Text = "DOWNLOAD";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSTTypeColorMat);
            this.groupBox1.Controls.Add(this.cbSTTypePart);
            this.groupBox1.Controls.Add(this.cbSTTypeRawMat);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(39, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 110);
            this.groupBox1.TabIndex = 175;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ITEM TYPE";
            // 
            // cbSTTypeColorMat
            // 
            this.cbSTTypeColorMat.AutoSize = true;
            this.cbSTTypeColorMat.Enabled = false;
            this.cbSTTypeColorMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSTTypeColorMat.Location = new System.Drawing.Point(16, 74);
            this.cbSTTypeColorMat.Name = "cbSTTypeColorMat";
            this.cbSTTypeColorMat.Size = new System.Drawing.Size(114, 21);
            this.cbSTTypeColorMat.TabIndex = 0;
            this.cbSTTypeColorMat.Text = "Color Material";
            this.cbSTTypeColorMat.UseVisualStyleBackColor = true;
            this.cbSTTypeColorMat.CheckedChanged += new System.EventHandler(this.cbSTTypeColorMat_CheckedChanged);
            // 
            // cbSTTypePart
            // 
            this.cbSTTypePart.AutoSize = true;
            this.cbSTTypePart.Enabled = false;
            this.cbSTTypePart.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSTTypePart.Location = new System.Drawing.Point(16, 20);
            this.cbSTTypePart.Name = "cbSTTypePart";
            this.cbSTTypePart.Size = new System.Drawing.Size(53, 21);
            this.cbSTTypePart.TabIndex = 177;
            this.cbSTTypePart.Text = "Part";
            this.cbSTTypePart.UseVisualStyleBackColor = true;
            this.cbSTTypePart.CheckedChanged += new System.EventHandler(this.cbSTTypePart_CheckedChanged);
            // 
            // cbSTTypeRawMat
            // 
            this.cbSTTypeRawMat.AutoSize = true;
            this.cbSTTypeRawMat.Enabled = false;
            this.cbSTTypeRawMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSTTypeRawMat.Location = new System.Drawing.Point(16, 47);
            this.cbSTTypeRawMat.Name = "cbSTTypeRawMat";
            this.cbSTTypeRawMat.Size = new System.Drawing.Size(106, 21);
            this.cbSTTypeRawMat.TabIndex = 2;
            this.cbSTTypeRawMat.Text = "Raw Material";
            this.cbSTTypeRawMat.UseVisualStyleBackColor = true;
            this.cbSTTypeRawMat.CheckedChanged += new System.EventHandler(this.cbSTTypeRawMat_CheckedChanged);
            // 
            // cbMacSchedule
            // 
            this.cbMacSchedule.AutoSize = true;
            this.cbMacSchedule.Checked = true;
            this.cbMacSchedule.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMacSchedule.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMacSchedule.Location = new System.Drawing.Point(16, 29);
            this.cbMacSchedule.Name = "cbMacSchedule";
            this.cbMacSchedule.Size = new System.Drawing.Size(135, 21);
            this.cbMacSchedule.TabIndex = 178;
            this.cbMacSchedule.Text = "Machine Schedule";
            this.cbMacSchedule.UseVisualStyleBackColor = true;
            this.cbMacSchedule.CheckedChanged += new System.EventHandler(this.cbMacSchedule_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbStockTakeList);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.cbMacSchedule);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 310);
            this.groupBox2.TabIndex = 178;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "REPORT TYPE";
            // 
            // cbStockTakeList
            // 
            this.cbStockTakeList.AutoSize = true;
            this.cbStockTakeList.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockTakeList.Location = new System.Drawing.Point(16, 67);
            this.cbStockTakeList.Name = "cbStockTakeList";
            this.cbStockTakeList.Size = new System.Drawing.Size(108, 21);
            this.cbStockTakeList.TabIndex = 179;
            this.cbStockTakeList.Text = "Stocktake List";
            this.cbStockTakeList.UseVisualStyleBackColor = true;
            this.cbStockTakeList.CheckedChanged += new System.EventHandler(this.cbStockTakeList_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnDownload, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(312, 406);
            this.tableLayoutPanel1.TabIndex = 179;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(342, 436);
            this.tableLayoutPanel2.TabIndex = 180;
            // 
            // frmMacScheduleExcelDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(342, 436);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMacScheduleExcelDownload";
            this.Text = "Export Setting";
            this.Load += new System.EventHandler(this.frmExportSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbSTTypeColorMat;
        private System.Windows.Forms.CheckBox cbSTTypeRawMat;
        private System.Windows.Forms.CheckBox cbSTTypePart;
        private System.Windows.Forms.CheckBox cbMacSchedule;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbStockTakeList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}