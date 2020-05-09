namespace FactoryManagementSoftware.UI
{
    partial class frmUserActionHistory
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
            this.dgvUserHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUserHistory
            // 
            this.dgvUserHistory.AllowUserToAddRows = false;
            this.dgvUserHistory.AllowUserToDeleteRows = false;
            this.dgvUserHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvUserHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUserHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUserHistory.GridColor = System.Drawing.SystemColors.Control;
            this.dgvUserHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvUserHistory.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.dgvUserHistory.Name = "dgvUserHistory";
            this.dgvUserHistory.ReadOnly = true;
            this.dgvUserHistory.RowHeadersVisible = false;
            this.dgvUserHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvUserHistory.RowTemplate.Height = 40;
            this.dgvUserHistory.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUserHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserHistory.Size = new System.Drawing.Size(1100, 660);
            this.dgvUserHistory.TabIndex = 40;
            // 
            // frmUserActionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 660);
            this.Controls.Add(this.dgvUserHistory);
            this.Name = "frmUserActionHistory";
            this.Text = "frmUserActionHistory";
            this.Load += new System.EventHandler(this.frmUserActionHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUserHistory;
    }
}