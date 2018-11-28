namespace FactoryManagementSoftware.UI
{
    partial class frmOrderActionHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAction = new System.Windows.Forms.DataGridView();
            this.order_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAction)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAction
            // 
            this.dgvAction.AllowUserToAddRows = false;
            this.dgvAction.AllowUserToDeleteRows = false;
            this.dgvAction.BackgroundColor = System.Drawing.Color.White;
            this.dgvAction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.order_id,
            this.date,
            this.by,
            this.action,
            this.note});
            this.dgvAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAction.GridColor = System.Drawing.SystemColors.Control;
            this.dgvAction.Location = new System.Drawing.Point(0, 0);
            this.dgvAction.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.dgvAction.Name = "dgvAction";
            this.dgvAction.ReadOnly = true;
            this.dgvAction.RowHeadersVisible = false;
            this.dgvAction.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvAction.RowTemplate.Height = 40;
            this.dgvAction.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAction.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAction.Size = new System.Drawing.Size(881, 470);
            this.dgvAction.TabIndex = 39;
            this.dgvAction.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAction_CellClick);
            this.dgvAction.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvAction_MouseClick);
            // 
            // order_id
            // 
            this.order_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.order_id.DefaultCellStyle = dataGridViewCellStyle1;
            this.order_id.HeaderText = "Order ID";
            this.order_id.Name = "order_id";
            this.order_id.ReadOnly = true;
            this.order_id.Width = 105;
            // 
            // date
            // 
            this.date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 75;
            // 
            // by
            // 
            this.by.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.by.DefaultCellStyle = dataGridViewCellStyle2;
            this.by.HeaderText = "By";
            this.by.Name = "by";
            this.by.ReadOnly = true;
            this.by.Width = 57;
            // 
            // action
            // 
            this.action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.action.HeaderText = "Action";
            this.action.Name = "action";
            this.action.ReadOnly = true;
            // 
            // note
            // 
            this.note.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.note.DefaultCellStyle = dataGridViewCellStyle3;
            this.note.HeaderText = "Note";
            this.note.Name = "note";
            this.note.ReadOnly = true;
            this.note.Width = 77;
            // 
            // frmOrderActionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(881, 470);
            this.Controls.Add(this.dgvAction);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "frmOrderActionHistory";
            this.Text = "frmOrderActionHistory";
            this.Load += new System.EventHandler(this.frmOrderActionHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn by;
        private System.Windows.Forms.DataGridViewTextBoxColumn action;
        private System.Windows.Forms.DataGridViewTextBoxColumn note;
    }
}