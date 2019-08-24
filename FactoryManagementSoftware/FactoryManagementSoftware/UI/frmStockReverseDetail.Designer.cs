namespace FactoryManagementSoftware.UI
{
    partial class frmStockReverseDetail
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
            this.lbltest = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblReadyStock = new System.Windows.Forms.Label();
            this.lblTotalIn = new System.Windows.Forms.Label();
            this.lblTotalOut = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCalculate = new System.Windows.Forms.Label();
            this.lblReverseStock = new System.Windows.Forms.Label();
            this.CALCULATION = new System.Windows.Forms.GroupBox();
            this.lblTrfHist = new System.Windows.Forms.Label();
            this.dgvTrfHist = new System.Windows.Forms.DataGridView();
            this.CALCULATION.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrfHist)).BeginInit();
            this.SuspendLayout();
            // 
            // lbltest
            // 
            this.lbltest.AutoSize = true;
            this.lbltest.Location = new System.Drawing.Point(6, 38);
            this.lbltest.Name = "lbltest";
            this.lbltest.Size = new System.Drawing.Size(116, 19);
            this.lbltest.TabIndex = 106;
            this.lbltest.Text = "CURRENT STOCK:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(54, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 19);
            this.label1.TabIndex = 107;
            this.label1.Text = "TOTAL IN:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(40, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 19);
            this.label2.TabIndex = 108;
            this.label2.Text = "TOTAL OUT:";
            // 
            // lblReadyStock
            // 
            this.lblReadyStock.Location = new System.Drawing.Point(128, 38);
            this.lblReadyStock.Name = "lblReadyStock";
            this.lblReadyStock.Size = new System.Drawing.Size(94, 19);
            this.lblReadyStock.TabIndex = 109;
            this.lblReadyStock.Text = "100000";
            this.lblReadyStock.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotalIn
            // 
            this.lblTotalIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblTotalIn.Location = new System.Drawing.Point(128, 57);
            this.lblTotalIn.Name = "lblTotalIn";
            this.lblTotalIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTotalIn.Size = new System.Drawing.Size(94, 19);
            this.lblTotalIn.TabIndex = 110;
            this.lblTotalIn.Text = "10000";
            this.lblTotalIn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotalOut
            // 
            this.lblTotalOut.ForeColor = System.Drawing.Color.Red;
            this.lblTotalOut.Location = new System.Drawing.Point(128, 76);
            this.lblTotalOut.Name = "lblTotalOut";
            this.lblTotalOut.Size = new System.Drawing.Size(94, 19);
            this.lblTotalOut.TabIndex = 111;
            this.lblTotalOut.Text = "10000";
            this.lblTotalOut.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 19);
            this.label3.TabIndex = 112;
            this.label3.Text = "REVERSE STOCK:";
            // 
            // lblCalculate
            // 
            this.lblCalculate.Location = new System.Drawing.Point(388, 38);
            this.lblCalculate.Name = "lblCalculate";
            this.lblCalculate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCalculate.Size = new System.Drawing.Size(198, 19);
            this.lblCalculate.TabIndex = 113;
            this.lblCalculate.Text = "1000 - 200 + 500 = ";
            this.lblCalculate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblReverseStock
            // 
            this.lblReverseStock.AutoSize = true;
            this.lblReverseStock.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReverseStock.Location = new System.Drawing.Point(592, 34);
            this.lblReverseStock.Name = "lblReverseStock";
            this.lblReverseStock.Size = new System.Drawing.Size(50, 23);
            this.lblReverseStock.TabIndex = 114;
            this.lblReverseStock.Text = "1300";
            // 
            // CALCULATION
            // 
            this.CALCULATION.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CALCULATION.Controls.Add(this.lbltest);
            this.CALCULATION.Controls.Add(this.label1);
            this.CALCULATION.Controls.Add(this.lblReverseStock);
            this.CALCULATION.Controls.Add(this.label2);
            this.CALCULATION.Controls.Add(this.lblCalculate);
            this.CALCULATION.Controls.Add(this.lblReadyStock);
            this.CALCULATION.Controls.Add(this.label3);
            this.CALCULATION.Controls.Add(this.lblTotalIn);
            this.CALCULATION.Controls.Add(this.lblTotalOut);
            this.CALCULATION.Location = new System.Drawing.Point(29, 23);
            this.CALCULATION.Name = "CALCULATION";
            this.CALCULATION.Size = new System.Drawing.Size(1065, 108);
            this.CALCULATION.TabIndex = 116;
            this.CALCULATION.TabStop = false;
            this.CALCULATION.Text = "CALCULATION";
            // 
            // lblTrfHist
            // 
            this.lblTrfHist.AutoSize = true;
            this.lblTrfHist.Location = new System.Drawing.Point(25, 156);
            this.lblTrfHist.Name = "lblTrfHist";
            this.lblTrfHist.Size = new System.Drawing.Size(323, 19);
            this.lblTrfHist.TabIndex = 115;
            this.lblTrfHist.Text = "TRANSFER HISTORY: 01/03/2019  -->  26/07/2019";
            // 
            // dgvTrfHist
            // 
            this.dgvTrfHist.AllowUserToAddRows = false;
            this.dgvTrfHist.AllowUserToDeleteRows = false;
            this.dgvTrfHist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTrfHist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvTrfHist.BackgroundColor = System.Drawing.Color.White;
            this.dgvTrfHist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTrfHist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrfHist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvTrfHist.GridColor = System.Drawing.SystemColors.Control;
            this.dgvTrfHist.Location = new System.Drawing.Point(29, 177);
            this.dgvTrfHist.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTrfHist.Name = "dgvTrfHist";
            this.dgvTrfHist.ReadOnly = true;
            this.dgvTrfHist.RowHeadersVisible = false;
            this.dgvTrfHist.RowTemplate.Height = 40;
            this.dgvTrfHist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrfHist.Size = new System.Drawing.Size(1065, 524);
            this.dgvTrfHist.TabIndex = 105;
            this.dgvTrfHist.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTrfHist_CellFormatting);
            // 
            // frmStockReverseDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1123, 712);
            this.Controls.Add(this.dgvTrfHist);
            this.Controls.Add(this.lblTrfHist);
            this.Controls.Add(this.CALCULATION);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmStockReverseDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmStockReverseDetail";
            this.CALCULATION.ResumeLayout(false);
            this.CALCULATION.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrfHist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbltest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblReadyStock;
        private System.Windows.Forms.Label lblTotalIn;
        private System.Windows.Forms.Label lblTotalOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCalculate;
        private System.Windows.Forms.Label lblReverseStock;
        private System.Windows.Forms.GroupBox CALCULATION;
        private System.Windows.Forms.Label lblTrfHist;
        private System.Windows.Forms.DataGridView dgvTrfHist;
    }
}