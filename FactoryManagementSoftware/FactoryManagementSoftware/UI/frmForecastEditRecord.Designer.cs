namespace FactoryManagementSoftware.UI
{
    partial class frmForecastEditRecord
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
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpForecastReport = new System.Windows.Forms.TableLayoutPanel();
            this.dgvForecastRecord = new System.Windows.Forms.DataGridView();
            this.lblPart = new System.Windows.Forms.Label();
            this.tableLayoutPanel10.SuspendLayout();
            this.tlpForecastReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForecastRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.tlpForecastReport, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 721F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(894, 509);
            this.tableLayoutPanel10.TabIndex = 169;
            // 
            // tlpForecastReport
            // 
            this.tlpForecastReport.ColumnCount = 1;
            this.tlpForecastReport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForecastReport.Controls.Add(this.lblPart, 0, 0);
            this.tlpForecastReport.Controls.Add(this.dgvForecastRecord, 0, 1);
            this.tlpForecastReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpForecastReport.Location = new System.Drawing.Point(15, 15);
            this.tlpForecastReport.Margin = new System.Windows.Forms.Padding(15);
            this.tlpForecastReport.Name = "tlpForecastReport";
            this.tlpForecastReport.RowCount = 2;
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpForecastReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpForecastReport.Size = new System.Drawing.Size(864, 479);
            this.tlpForecastReport.TabIndex = 164;
            // 
            // dgvForecastRecord
            // 
            this.dgvForecastRecord.AllowUserToAddRows = false;
            this.dgvForecastRecord.AllowUserToDeleteRows = false;
            this.dgvForecastRecord.AllowUserToOrderColumns = true;
            this.dgvForecastRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvForecastRecord.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvForecastRecord.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvForecastRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForecastRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvForecastRecord.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvForecastRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvForecastRecord.GridColor = System.Drawing.Color.White;
            this.dgvForecastRecord.Location = new System.Drawing.Point(3, 37);
            this.dgvForecastRecord.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvForecastRecord.Name = "dgvForecastRecord";
            this.dgvForecastRecord.RowHeadersVisible = false;
            this.dgvForecastRecord.RowHeadersWidth = 51;
            this.dgvForecastRecord.RowTemplate.Height = 30;
            this.dgvForecastRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvForecastRecord.Size = new System.Drawing.Size(858, 441);
            this.dgvForecastRecord.TabIndex = 152;
            this.dgvForecastRecord.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvForecastRecord_DataBindingComplete);
            // 
            // lblPart
            // 
            this.lblPart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPart.AutoSize = true;
            this.lblPart.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPart.Location = new System.Drawing.Point(3, 9);
            this.lblPart.Name = "lblPart";
            this.lblPart.Size = new System.Drawing.Size(231, 17);
            this.lblPart.TabIndex = 170;
            this.lblPart.Text = "BLOWER OUTLET UNIT (V96LAR000)";
            // 
            // frmForecastEditRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(894, 509);
            this.Controls.Add(this.tableLayoutPanel10);
            this.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.Name = "frmForecastEditRecord";
            this.Text = "Forecast Record";
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tlpForecastReport.ResumeLayout(false);
            this.tlpForecastReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForecastRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tlpForecastReport;
        private System.Windows.Forms.Label lblPart;
        private System.Windows.Forms.DataGridView dgvForecastRecord;
    }
}