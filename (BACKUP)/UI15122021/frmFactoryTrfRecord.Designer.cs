namespace FactoryManagementSoftware.UI
{
    partial class frmFactoryTrfRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTrf = new System.Windows.Forms.DataGridView();
            this.trf_hist_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_added_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_trf_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_added_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrf)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTrf
            // 
            this.dgvTrf.AllowUserToAddRows = false;
            this.dgvTrf.AllowUserToDeleteRows = false;
            this.dgvTrf.BackgroundColor = System.Drawing.Color.White;
            this.dgvTrf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTrf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrf.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trf_hist_id,
            this.trf_hist_added_date,
            this.trf_hist_trf_date,
            this.trf_hist_item_code,
            this.trf_hist_item_name,
            this.trf_hist_from,
            this.trf_hist_to,
            this.trf_hist_qty,
            this.trf_hist_unit,
            this.trf_hist_note,
            this.trf_hist_added_by,
            this.trf_result});
            this.dgvTrf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrf.GridColor = System.Drawing.SystemColors.Control;
            this.dgvTrf.Location = new System.Drawing.Point(0, 0);
            this.dgvTrf.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvTrf.Name = "dgvTrf";
            this.dgvTrf.ReadOnly = true;
            this.dgvTrf.RowHeadersVisible = false;
            this.dgvTrf.RowTemplate.Height = 40;
            this.dgvTrf.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrf.Size = new System.Drawing.Size(1344, 330);
            this.dgvTrf.TabIndex = 38;
            // 
            // trf_hist_id
            // 
            this.trf_hist_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_id.DefaultCellStyle = dataGridViewCellStyle31;
            this.trf_hist_id.HeaderText = "ID";
            this.trf_hist_id.Name = "trf_hist_id";
            this.trf_hist_id.ReadOnly = true;
            this.trf_hist_id.Width = 56;
            // 
            // trf_hist_added_date
            // 
            this.trf_hist_added_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_added_date.DefaultCellStyle = dataGridViewCellStyle32;
            this.trf_hist_added_date.HeaderText = "Added_Date";
            this.trf_hist_added_date.Name = "trf_hist_added_date";
            this.trf_hist_added_date.ReadOnly = true;
            this.trf_hist_added_date.Width = 132;
            // 
            // trf_hist_trf_date
            // 
            this.trf_hist_trf_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            dataGridViewCellStyle33.NullValue = null;
            this.trf_hist_trf_date.DefaultCellStyle = dataGridViewCellStyle33;
            this.trf_hist_trf_date.HeaderText = "Trf_Date";
            this.trf_hist_trf_date.Name = "trf_hist_trf_date";
            this.trf_hist_trf_date.ReadOnly = true;
            this.trf_hist_trf_date.Width = 101;
            // 
            // trf_hist_item_code
            // 
            this.trf_hist_item_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_item_code.DefaultCellStyle = dataGridViewCellStyle34;
            this.trf_hist_item_code.HeaderText = "Code";
            this.trf_hist_item_code.Name = "trf_hist_item_code";
            this.trf_hist_item_code.ReadOnly = true;
            // 
            // trf_hist_item_name
            // 
            this.trf_hist_item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_item_name.DefaultCellStyle = dataGridViewCellStyle35;
            this.trf_hist_item_name.HeaderText = "Name";
            this.trf_hist_item_name.Name = "trf_hist_item_name";
            this.trf_hist_item_name.ReadOnly = true;
            // 
            // trf_hist_from
            // 
            this.trf_hist_from.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_from.DefaultCellStyle = dataGridViewCellStyle36;
            this.trf_hist_from.HeaderText = "From";
            this.trf_hist_from.Name = "trf_hist_from";
            this.trf_hist_from.ReadOnly = true;
            this.trf_hist_from.Width = 78;
            // 
            // trf_hist_to
            // 
            this.trf_hist_to.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_to.DefaultCellStyle = dataGridViewCellStyle37;
            this.trf_hist_to.HeaderText = "To";
            this.trf_hist_to.Name = "trf_hist_to";
            this.trf_hist_to.ReadOnly = true;
            this.trf_hist_to.Width = 56;
            // 
            // trf_hist_qty
            // 
            this.trf_hist_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_qty.DefaultCellStyle = dataGridViewCellStyle38;
            this.trf_hist_qty.HeaderText = "Qty";
            this.trf_hist_qty.Name = "trf_hist_qty";
            this.trf_hist_qty.ReadOnly = true;
            this.trf_hist_qty.Width = 66;
            // 
            // trf_hist_unit
            // 
            this.trf_hist_unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_unit.DefaultCellStyle = dataGridViewCellStyle39;
            this.trf_hist_unit.HeaderText = "Unit";
            this.trf_hist_unit.Name = "trf_hist_unit";
            this.trf_hist_unit.ReadOnly = true;
            this.trf_hist_unit.Width = 71;
            // 
            // trf_hist_note
            // 
            this.trf_hist_note.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.trf_hist_note.HeaderText = "Note";
            this.trf_hist_note.Name = "trf_hist_note";
            this.trf_hist_note.ReadOnly = true;
            this.trf_hist_note.Width = 67;
            // 
            // trf_hist_added_by
            // 
            this.trf_hist_added_by.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_added_by.DefaultCellStyle = dataGridViewCellStyle40;
            this.trf_hist_added_by.HeaderText = "By";
            this.trf_hist_added_by.Name = "trf_hist_added_by";
            this.trf_hist_added_by.ReadOnly = true;
            this.trf_hist_added_by.Width = 57;
            // 
            // trf_result
            // 
            this.trf_result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.trf_result.HeaderText = "Result";
            this.trf_result.Name = "trf_result";
            this.trf_result.ReadOnly = true;
            this.trf_result.Width = 77;
            // 
            // frmFactoryTrfRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1344, 330);
            this.Controls.Add(this.dgvTrf);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmFactoryTrfRecord";
            this.Text = "frmFactoryTrfRecord";
            this.Load += new System.EventHandler(this.frmFactoryTrfRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTrf;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_added_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_trf_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_note;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_added_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_result;
    }
}