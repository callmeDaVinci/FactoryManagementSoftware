namespace FactoryManagementSoftware.UI
{
    partial class frmMatPlanningList
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
            this.dgvMatList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMatList
            // 
            this.dgvMatList.AllowUserToAddRows = false;
            this.dgvMatList.AllowUserToDeleteRows = false;
            this.dgvMatList.AllowUserToResizeColumns = false;
            this.dgvMatList.AllowUserToResizeRows = false;
            this.dgvMatList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMatList.BackgroundColor = System.Drawing.Color.White;
            this.dgvMatList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMatList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvMatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatList.GridColor = System.Drawing.SystemColors.Control;
            this.dgvMatList.Location = new System.Drawing.Point(0, 0);
            this.dgvMatList.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMatList.MultiSelect = false;
            this.dgvMatList.Name = "dgvMatList";
            this.dgvMatList.ReadOnly = true;
            this.dgvMatList.RowHeadersVisible = false;
            this.dgvMatList.RowTemplate.Height = 40;
            this.dgvMatList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatList.Size = new System.Drawing.Size(410, 469);
            this.dgvMatList.TabIndex = 110;
            this.dgvMatList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatList_CellContentClick);
            // 
            // frmMatPlanningList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(410, 469);
            this.Controls.Add(this.dgvMatList);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMatPlanningList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMatPlanningList";
            this.Load += new System.EventHandler(this.frmMatPlanningList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMatList;
    }
}