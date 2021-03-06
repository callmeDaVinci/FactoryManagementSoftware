﻿namespace FactoryManagementSoftware.UI
{
    partial class frmNewOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrdSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOrder = new System.Windows.Forms.Button();
            this.dgvOrderAlert = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvOrd = new System.Windows.Forms.DataGridView();
            this.ord_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_added_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_forecast_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_ord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.received = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_added_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ord_status = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderAlert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrd)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 23);
            this.label2.TabIndex = 92;
            this.label2.Text = "ALERT";
            // 
            // txtOrdSearch
            // 
            this.txtOrdSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrdSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.txtOrdSearch.Location = new System.Drawing.Point(34, 52);
            this.txtOrdSearch.Name = "txtOrdSearch";
            this.txtOrdSearch.Size = new System.Drawing.Size(300, 38);
            this.txtOrdSearch.TabIndex = 91;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 90;
            this.label1.Text = "SEARCH";
            // 
            // btnOrder
            // 
            this.btnOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrder.ForeColor = System.Drawing.Color.White;
            this.btnOrder.Location = new System.Drawing.Point(1384, 38);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(2);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(169, 52);
            this.btnOrder.TabIndex = 88;
            this.btnOrder.Text = "NEW ORDER";
            this.btnOrder.UseVisualStyleBackColor = false;
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
            this.dgvOrderAlert.Location = new System.Drawing.Point(34, 514);
            this.dgvOrderAlert.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOrderAlert.Name = "dgvOrderAlert";
            this.dgvOrderAlert.ReadOnly = true;
            this.dgvOrderAlert.RowHeadersVisible = false;
            this.dgvOrderAlert.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvOrderAlert.RowTemplate.Height = 40;
            this.dgvOrderAlert.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderAlert.Size = new System.Drawing.Size(1519, 162);
            this.dgvOrderAlert.TabIndex = 87;
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
            this.ord_qty,
            this.item_ord,
            this.received,
            this.ord_unit,
            this.to,
            this.ord_added_by,
            this.ord_note,
            this.ord_status});
            this.dgvOrd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvOrd.GridColor = System.Drawing.SystemColors.Control;
            this.dgvOrd.Location = new System.Drawing.Point(34, 114);
            this.dgvOrd.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOrd.MultiSelect = false;
            this.dgvOrd.Name = "dgvOrd";
            this.dgvOrd.RowHeadersVisible = false;
            this.dgvOrd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvOrd.RowTemplate.Height = 40;
            this.dgvOrd.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrd.Size = new System.Drawing.Size(1519, 361);
            this.dgvOrd.TabIndex = 86;
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
            this.ord_added_date.Width = 119;
            // 
            // ord_forecast_date
            // 
            this.ord_forecast_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_forecast_date.HeaderText = "Date Required";
            this.ord_forecast_date.Name = "ord_forecast_date";
            this.ord_forecast_date.Width = 136;
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
            // ord_qty
            // 
            this.ord_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ord_qty.HeaderText = "Order Qty";
            this.ord_qty.Name = "ord_qty";
            this.ord_qty.Width = 106;
            // 
            // item_ord
            // 
            this.item_ord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.item_ord.HeaderText = "Pending";
            this.item_ord.Name = "item_ord";
            this.item_ord.Width = 101;
            // 
            // received
            // 
            this.received.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.received.HeaderText = "Received";
            this.received.Name = "received";
            this.received.Width = 106;
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
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ord_status.DefaultCellStyle = dataGridViewCellStyle2;
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
            // frmNewOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrdSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.dgvOrderAlert);
            this.Controls.Add(this.dgvOrd);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmNewOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNewOrder";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderAlert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrdSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.DataGridView dgvOrderAlert;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridView dgvOrd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_added_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_forecast_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_ord;
        private System.Windows.Forms.DataGridViewTextBoxColumn received;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn to;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_added_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn ord_note;
        private System.Windows.Forms.DataGridViewComboBoxColumn ord_status;
    }
}