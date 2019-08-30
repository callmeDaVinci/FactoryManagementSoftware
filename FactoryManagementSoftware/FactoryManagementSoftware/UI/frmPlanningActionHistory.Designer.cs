namespace FactoryManagementSoftware.UI
{
    partial class frmPlanningActionHistory
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
            this.dgvActionHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActionHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvActionHistory
            // 
            this.dgvActionHistory.AllowUserToAddRows = false;
            this.dgvActionHistory.AllowUserToDeleteRows = false;
            this.dgvActionHistory.AllowUserToResizeColumns = false;
            this.dgvActionHistory.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.dgvActionHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvActionHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvActionHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvActionHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvActionHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActionHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvActionHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActionHistory.GridColor = System.Drawing.Color.White;
            this.dgvActionHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvActionHistory.Margin = new System.Windows.Forms.Padding(2);
            this.dgvActionHistory.MultiSelect = false;
            this.dgvActionHistory.Name = "dgvActionHistory";
            this.dgvActionHistory.ReadOnly = true;
            this.dgvActionHistory.RowHeadersVisible = false;
            this.dgvActionHistory.RowTemplate.Height = 40;
            this.dgvActionHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActionHistory.Size = new System.Drawing.Size(1234, 582);
            this.dgvActionHistory.TabIndex = 135;
            // 
            // frmPlanningActionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1234, 582);
            this.Controls.Add(this.dgvActionHistory);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmPlanningActionHistory";
            this.Text = "Action History";
            this.Load += new System.EventHandler(this.frmPlanningActionHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActionHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvActionHistory;
    }
}