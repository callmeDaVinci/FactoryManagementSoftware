namespace FactoryManagementSoftware.UI
{
    partial class frmChildStockOutRecord
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
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.child_trf_hist_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.child_trf_hist_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.child_trf_hist_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.child_trf_hist_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.child_trf_hist_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.child_trf_hist_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.child_trf_hist_result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.child_trf_hist_id,
            this.child_trf_hist_code,
            this.item_name,
            this.child_trf_hist_from,
            this.child_trf_hist_to,
            this.child_trf_hist_qty,
            this.child_trf_hist_unit,
            this.child_trf_hist_result});
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.GridColor = System.Drawing.SystemColors.Control;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Margin = new System.Windows.Forms.Padding(2);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 40;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(898, 401);
            this.dgvData.TabIndex = 57;
            // 
            // child_trf_hist_id
            // 
            this.child_trf_hist_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.child_trf_hist_id.HeaderText = "ID";
            this.child_trf_hist_id.Name = "child_trf_hist_id";
            this.child_trf_hist_id.ReadOnly = true;
            this.child_trf_hist_id.Width = 56;
            // 
            // child_trf_hist_code
            // 
            this.child_trf_hist_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.child_trf_hist_code.HeaderText = "Code";
            this.child_trf_hist_code.Name = "child_trf_hist_code";
            this.child_trf_hist_code.ReadOnly = true;
            this.child_trf_hist_code.Width = 79;
            // 
            // item_name
            // 
            this.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_name.HeaderText = "Name";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            // 
            // child_trf_hist_from
            // 
            this.child_trf_hist_from.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.child_trf_hist_from.HeaderText = "From";
            this.child_trf_hist_from.Name = "child_trf_hist_from";
            this.child_trf_hist_from.ReadOnly = true;
            this.child_trf_hist_from.Width = 78;
            // 
            // child_trf_hist_to
            // 
            this.child_trf_hist_to.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.child_trf_hist_to.HeaderText = "To";
            this.child_trf_hist_to.Name = "child_trf_hist_to";
            this.child_trf_hist_to.ReadOnly = true;
            this.child_trf_hist_to.Width = 56;
            // 
            // child_trf_hist_qty
            // 
            this.child_trf_hist_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.child_trf_hist_qty.DefaultCellStyle = dataGridViewCellStyle2;
            this.child_trf_hist_qty.HeaderText = "Qty";
            this.child_trf_hist_qty.Name = "child_trf_hist_qty";
            this.child_trf_hist_qty.ReadOnly = true;
            this.child_trf_hist_qty.Width = 66;
            // 
            // child_trf_hist_unit
            // 
            this.child_trf_hist_unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.child_trf_hist_unit.HeaderText = "Unit";
            this.child_trf_hist_unit.Name = "child_trf_hist_unit";
            this.child_trf_hist_unit.ReadOnly = true;
            this.child_trf_hist_unit.Width = 71;
            // 
            // child_trf_hist_result
            // 
            this.child_trf_hist_result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.child_trf_hist_result.HeaderText = "Result";
            this.child_trf_hist_result.Name = "child_trf_hist_result";
            this.child_trf_hist_result.ReadOnly = true;
            this.child_trf_hist_result.Width = 85;
            // 
            // frmChildStockOutRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(898, 401);
            this.Controls.Add(this.dgvData);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmChildStockOutRecord";
            this.Text = "frmChildStockOutRecord";
            this.Load += new System.EventHandler(this.frmChildStockOutRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewTextBoxColumn child_trf_hist_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn child_trf_hist_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn child_trf_hist_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn child_trf_hist_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn child_trf_hist_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn child_trf_hist_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn child_trf_hist_result;
    }
}