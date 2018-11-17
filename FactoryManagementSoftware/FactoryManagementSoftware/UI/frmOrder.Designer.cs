namespace FactoryManagementSoftware.UI
{
    partial class frmOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOrd = new System.Windows.Forms.DataGridView();
            this.dgvOrderAlert = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.txtOrdSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ord_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_added_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_forecast_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_ord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_added_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_status = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderAlert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOrd
            // 
            this.dgvOrd.AllowUserToAddRows = false;
            this.dgvOrd.AllowUserToDeleteRows = false;
            this.dgvOrd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvOrd.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ord_id,
            this.ord_added_date,
            this.ord_forecast_date,
            this.ord_item_code,
            this.item_name,
            this.item_ord,
            this.ord_qty,
            this.ord_unit,
            this.to,
            this.ord_added_by,
            this.ord_note,
            this.ord_status});
            this.dgvOrd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvOrd.GridColor = System.Drawing.SystemColors.Control;
            this.dgvOrd.Location = new System.Drawing.Point(28, 117);
            this.dgvOrd.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOrd.MultiSelect = false;
            this.dgvOrd.Name = "dgvOrd";
            this.dgvOrd.RowHeadersVisible = false;
            this.dgvOrd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvOrd.RowTemplate.Height = 40;
            this.dgvOrd.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrd.Size = new System.Drawing.Size(1519, 361);
            this.dgvOrd.TabIndex = 19;
            this.dgvOrd.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrd_CellEnter);
            this.dgvOrd.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvOrd_EditingControlShowing);
            // 
            // dgvOrderAlert
            // 
            this.dgvOrderAlert.AllowUserToAddRows = false;
            this.dgvOrderAlert.AllowUserToDeleteRows = false;
            this.dgvOrderAlert.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrderAlert.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderAlert.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrderAlert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderAlert.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewComboBoxColumn1});
            this.dgvOrderAlert.GridColor = System.Drawing.SystemColors.Control;
            this.dgvOrderAlert.Location = new System.Drawing.Point(28, 517);
            this.dgvOrderAlert.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOrderAlert.Name = "dgvOrderAlert";
            this.dgvOrderAlert.ReadOnly = true;
            this.dgvOrderAlert.RowHeadersVisible = false;
            this.dgvOrderAlert.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvOrderAlert.RowTemplate.Height = 40;
            this.dgvOrderAlert.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderAlert.Size = new System.Drawing.Size(1519, 162);
            this.dgvOrderAlert.TabIndex = 80;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "Order ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 105;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Item Code";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Item Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn4.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 66;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn5.HeaderText = "By";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 57;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn6.HeaderText = "Note";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 77;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewComboBoxColumn1.HeaderText = "Status";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.ReadOnly = true;
            this.dataGridViewComboBoxColumn1.Width = 62;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // errorProvider4
            // 
            this.errorProvider4.ContainerControl = this;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnReset.Location = new System.Drawing.Point(1427, 41);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 50);
            this.btnReset.TabIndex = 82;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrder.ForeColor = System.Drawing.Color.White;
            this.btnOrder.Location = new System.Drawing.Point(1283, 41);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(2);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(122, 52);
            this.btnOrder.TabIndex = 81;
            this.btnOrder.Text = "ORDER";
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // txtOrdSearch
            // 
            this.txtOrdSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrdSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.txtOrdSearch.Location = new System.Drawing.Point(28, 55);
            this.txtOrdSearch.Name = "txtOrdSearch";
            this.txtOrdSearch.Size = new System.Drawing.Size(300, 38);
            this.txtOrdSearch.TabIndex = 84;
            this.txtOrdSearch.TextChanged += new System.EventHandler(this.txtOrdSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 83;
            this.label1.Text = "SEARCH";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 492);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 23);
            this.label2.TabIndex = 85;
            this.label2.Text = "ALERT";
            // 
            // ord_id
            // 
            this.ord_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_id.HeaderText = "ID";
            this.ord_id.Name = "ord_id";
            this.ord_id.Width = 56;
            // 
            // ord_added_date
            // 
            this.ord_added_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_added_date.HeaderText = "Added Date";
            this.ord_added_date.Name = "ord_added_date";
            this.ord_added_date.Width = 130;
            // 
            // ord_forecast_date
            // 
            this.ord_forecast_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_forecast_date.HeaderText = "Forecast Date";
            this.ord_forecast_date.Name = "ord_forecast_date";
            this.ord_forecast_date.Width = 143;
            // 
            // ord_item_code
            // 
            this.ord_item_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ord_item_code.HeaderText = "Code";
            this.ord_item_code.Name = "ord_item_code";
            // 
            // item_name
            // 
            this.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_name.HeaderText = "Name";
            this.item_name.Name = "item_name";
            // 
            // item_ord
            // 
            this.item_ord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.item_ord.HeaderText = "Ordered";
            this.item_ord.Name = "item_ord";
            this.item_ord.Width = 102;
            // 
            // ord_qty
            // 
            this.ord_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_qty.HeaderText = "Qty";
            this.ord_qty.Name = "ord_qty";
            this.ord_qty.Width = 66;
            // 
            // ord_unit
            // 
            this.ord_unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_unit.HeaderText = "Unit";
            this.ord_unit.Name = "ord_unit";
            this.ord_unit.Width = 71;
            // 
            // to
            // 
            this.to.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.to.HeaderText = "To";
            this.to.Name = "to";
            this.to.Width = 56;
            // 
            // ord_added_by
            // 
            this.ord_added_by.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_added_by.HeaderText = "By";
            this.ord_added_by.Name = "ord_added_by";
            this.ord_added_by.Width = 57;
            // 
            // ord_note
            // 
            this.ord_note.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_note.HeaderText = "Note";
            this.ord_note.Name = "ord_note";
            this.ord_note.Width = 77;
            // 
            // ord_status
            // 
            this.ord_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ord_status.DefaultCellStyle = dataGridViewCellStyle1;
            this.ord_status.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ord_status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ord_status.HeaderText = "Status";
            this.ord_status.Items.AddRange(new object[] {
            "Requesting",
            "Cancelled",
            "Approved",
            "Received"});
            this.ord_status.MaxDropDownItems = 4;
            this.ord_status.Name = "ord_status";
            this.ord_status.Width = 62;
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrdSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.dgvOrderAlert);
            this.Controls.Add(this.dgvOrd);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmOrder";
            this.Text = "Order";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOrder_FormClosed);
            this.Load += new System.EventHandler(this.frmOrder_Load);
            this.Click += new System.EventHandler(this.frmOrder_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderAlert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvOrd;
        private System.Windows.Forms.DataGridView dgvOrderAlert;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.TextBox txtOrdSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_added_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_forecast_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_ord;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn to;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_added_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_note;
        private System.Windows.Forms.DataGridViewComboBoxColumn ord_status;
    }
}