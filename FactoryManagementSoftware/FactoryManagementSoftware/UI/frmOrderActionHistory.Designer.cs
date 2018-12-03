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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAction = new System.Windows.Forms.DataGridView();
            this.order_action_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.added_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.added_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action_detail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.order_action_id,
            this.order_id,
            this.added_date,
            this.added_by,
            this.action,
            this.action_detail,
            this.action_from,
            this.action_to,
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
            this.dgvAction.Size = new System.Drawing.Size(1167, 597);
            this.dgvAction.TabIndex = 39;
            this.dgvAction.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAction_CellClick);
            this.dgvAction.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAction_CellMouseDown);
            this.dgvAction.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvAction_MouseClick);
            // 
            // order_action_id
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.order_action_id.DefaultCellStyle = dataGridViewCellStyle5;
            this.order_action_id.HeaderText = "Action ID";
            this.order_action_id.Name = "order_action_id";
            this.order_action_id.ReadOnly = true;
            // 
            // order_id
            // 
            this.order_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.order_id.DefaultCellStyle = dataGridViewCellStyle6;
            this.order_id.HeaderText = "Order ID";
            this.order_id.Name = "order_id";
            this.order_id.ReadOnly = true;
            this.order_id.Width = 105;
            // 
            // added_date
            // 
            this.added_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.added_date.HeaderText = "Date";
            this.added_date.Name = "added_date";
            this.added_date.ReadOnly = true;
            this.added_date.Width = 75;
            // 
            // added_by
            // 
            this.added_by.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.added_by.DefaultCellStyle = dataGridViewCellStyle7;
            this.added_by.HeaderText = "By";
            this.added_by.Name = "added_by";
            this.added_by.ReadOnly = true;
            this.added_by.Width = 57;
            // 
            // action
            // 
            this.action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.action.HeaderText = "Action";
            this.action.Name = "action";
            this.action.ReadOnly = true;
            // 
            // action_detail
            // 
            this.action_detail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.action_detail.HeaderText = "Detail";
            this.action_detail.Name = "action_detail";
            this.action_detail.ReadOnly = true;
            // 
            // action_from
            // 
            this.action_from.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.action_from.HeaderText = "From";
            this.action_from.Name = "action_from";
            this.action_from.ReadOnly = true;
            this.action_from.Width = 78;
            // 
            // action_to
            // 
            this.action_to.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.action_to.HeaderText = "To";
            this.action_to.Name = "action_to";
            this.action_to.ReadOnly = true;
            this.action_to.Width = 56;
            // 
            // note
            // 
            this.note.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.note.DefaultCellStyle = dataGridViewCellStyle8;
            this.note.HeaderText = "Note";
            this.note.Name = "note";
            this.note.ReadOnly = true;
            this.note.Width = 77;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // frmOrderActionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1167, 597);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn order_action_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn added_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn added_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn action;
        private System.Windows.Forms.DataGridViewTextBoxColumn action_detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn action_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn action_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn note;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}