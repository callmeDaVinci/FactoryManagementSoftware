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
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMatList
            // 
            this.dgvMatList.AllowUserToAddRows = false;
            this.dgvMatList.AllowUserToDeleteRows = false;
            this.dgvMatList.AllowUserToResizeColumns = false;
            this.dgvMatList.AllowUserToResizeRows = false;
            this.dgvMatList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMatList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMatList.BackgroundColor = System.Drawing.Color.White;
            this.dgvMatList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMatList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvMatList.GridColor = System.Drawing.SystemColors.Control;
            this.dgvMatList.Location = new System.Drawing.Point(0, 64);
            this.dgvMatList.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMatList.MultiSelect = false;
            this.dgvMatList.Name = "dgvMatList";
            this.dgvMatList.ReadOnly = true;
            this.dgvMatList.RowHeadersVisible = false;
            this.dgvMatList.RowTemplate.Height = 40;
            this.dgvMatList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatList.Size = new System.Drawing.Size(936, 491);
            this.dgvMatList.TabIndex = 110;
            this.dgvMatList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatList_CellContentClick);
            this.dgvMatList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMatList_CellFormatting);
            // 
            // cmbSort
            // 
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Location = new System.Drawing.Point(83, 13);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(177, 25);
            this.cmbSort.TabIndex = 111;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 19);
            this.label1.TabIndex = 112;
            this.label1.Text = "SORT BY:";
            // 
            // frmMatPlanningList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(936, 555);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.dgvMatList);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMatPlanningList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMatPlanningList";
            this.Load += new System.EventHandler(this.frmMatPlanningList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMatList;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Label label1;
    }
}