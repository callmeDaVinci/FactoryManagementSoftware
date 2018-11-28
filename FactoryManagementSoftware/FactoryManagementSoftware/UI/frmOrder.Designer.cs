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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btnOrder = new System.Windows.Forms.Button();
            this.txtOrdSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSearchBy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSearchCat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ord_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_added_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_required_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_pending = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_received = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dgvOrd.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOrd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ord_id,
            this.ord_added_date,
            this.ord_required_date,
            this.ord_item_code,
            this.item_name,
            this.ord_qty,
            this.ord_pending,
            this.ord_received,
            this.ord_unit,
            this.ord_status});
            this.dgvOrd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvOrd.GridColor = System.Drawing.SystemColors.Control;
            this.dgvOrd.Location = new System.Drawing.Point(28, 117);
            this.dgvOrd.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOrd.MultiSelect = false;
            this.dgvOrd.Name = "dgvOrd";
            this.dgvOrd.ReadOnly = true;
            this.dgvOrd.RowHeadersVisible = false;
            this.dgvOrd.RowTemplate.Height = 40;
            this.dgvOrd.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrd.Size = new System.Drawing.Size(1519, 361);
            this.dgvOrd.TabIndex = 19;
            this.dgvOrd.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrd_CellDoubleClick);
            this.dgvOrd.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrd_CellEnter);
            this.dgvOrd.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrd_CellMouseDown);
            this.dgvOrd.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvOrd_EditingControlShowing);
            this.dgvOrd.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvOrd_MouseClick);
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
            // btnOrder
            // 
            this.btnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrder.ForeColor = System.Drawing.Color.White;
            this.btnOrder.Location = new System.Drawing.Point(1385, 41);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(2);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(162, 52);
            this.btnOrder.TabIndex = 81;
            this.btnOrder.Text = "NEW ORDER";
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // txtOrdSearch
            // 
            this.txtOrdSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrdSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.txtOrdSearch.Location = new System.Drawing.Point(724, 56);
            this.txtOrdSearch.Name = "txtOrdSearch";
            this.txtOrdSearch.Size = new System.Drawing.Size(370, 38);
            this.txtOrdSearch.TabIndex = 84;
            this.txtOrdSearch.TextChanged += new System.EventHandler(this.txtOrdSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(720, 30);
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
            // cmbSearchBy
            // 
            this.cmbSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchBy.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchBy.FormattingEnabled = true;
            this.cmbSearchBy.Items.AddRange(new object[] {
            "Item Code",
            "Item Name"});
            this.cmbSearchBy.Location = new System.Drawing.Point(374, 55);
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.Size = new System.Drawing.Size(325, 39);
            this.cmbSearchBy.TabIndex = 89;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(370, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 23);
            this.label4.TabIndex = 88;
            this.label4.Text = "BY";
            // 
            // cmbSearchCat
            // 
            this.cmbSearchCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchCat.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchCat.FormattingEnabled = true;
            this.cmbSearchCat.Location = new System.Drawing.Point(28, 55);
            this.cmbSearchCat.Name = "cmbSearchCat";
            this.cmbSearchCat.Size = new System.Drawing.Size(325, 39);
            this.cmbSearchCat.TabIndex = 87;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 23);
            this.label3.TabIndex = 86;
            this.label3.Text = "CATEGORY";
            // 
            // ord_id
            // 
            this.ord_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_id.HeaderText = "ID";
            this.ord_id.Name = "ord_id";
            this.ord_id.ReadOnly = true;
            this.ord_id.Width = 56;
            // 
            // ord_added_date
            // 
            this.ord_added_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_added_date.HeaderText = "Added Date";
            this.ord_added_date.Name = "ord_added_date";
            this.ord_added_date.ReadOnly = true;
            this.ord_added_date.Width = 130;
            // 
            // ord_required_date
            // 
            this.ord_required_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ord_required_date.HeaderText = "Date Required";
            this.ord_required_date.Name = "ord_required_date";
            this.ord_required_date.ReadOnly = true;
            this.ord_required_date.Width = 148;
            // 
            // ord_item_code
            // 
            this.ord_item_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ord_item_code.HeaderText = "Code";
            this.ord_item_code.Name = "ord_item_code";
            this.ord_item_code.ReadOnly = true;
            // 
            // item_name
            // 
            this.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_name.HeaderText = "Name";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            // 
            // ord_qty
            // 
            this.ord_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ord_qty.HeaderText = "Order Qty";
            this.ord_qty.Name = "ord_qty";
            this.ord_qty.ReadOnly = true;
            this.ord_qty.Width = 115;
            // 
            // ord_pending
            // 
            this.ord_pending.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_pending.HeaderText = "Pending";
            this.ord_pending.Name = "ord_pending";
            this.ord_pending.ReadOnly = true;
            this.ord_pending.Width = 101;
            // 
            // ord_received
            // 
            this.ord_received.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_received.HeaderText = "Received";
            this.ord_received.Name = "ord_received";
            this.ord_received.ReadOnly = true;
            this.ord_received.Width = 106;
            // 
            // ord_unit
            // 
            this.ord_unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_unit.HeaderText = "Unit";
            this.ord_unit.Name = "ord_unit";
            this.ord_unit.ReadOnly = true;
            this.ord_unit.Width = 71;
            // 
            // ord_status
            // 
            this.ord_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ord_status.DefaultCellStyle = dataGridViewCellStyle2;
            this.ord_status.HeaderText = "Status";
            this.ord_status.Name = "ord_status";
            this.ord_status.ReadOnly = true;
            this.ord_status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ord_status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ord_status.Width = 62;
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.cmbSearchBy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSearchCat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrdSearch);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.TextBox txtOrdSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSearchBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSearchCat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_added_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_required_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_pending;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_received;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_status;
    }
}