namespace FactoryManagementSoftware.UI
{
    partial class frmJoin
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
            this.dgvJoin = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCat = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.join_parent_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.join_parent_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.join_child_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.join_child_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.join_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJoin)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvJoin
            // 
            this.dgvJoin.AllowUserToAddRows = false;
            this.dgvJoin.AllowUserToDeleteRows = false;
            this.dgvJoin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvJoin.BackgroundColor = System.Drawing.Color.White;
            this.dgvJoin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvJoin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJoin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.join_parent_code,
            this.join_parent_name,
            this.join_child_code,
            this.join_child_name,
            this.join_qty});
            this.dgvJoin.Location = new System.Drawing.Point(12, 118);
            this.dgvJoin.Name = "dgvJoin";
            this.dgvJoin.ReadOnly = true;
            this.dgvJoin.RowHeadersVisible = false;
            this.dgvJoin.RowTemplate.Height = 24;
            this.dgvJoin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJoin.Size = new System.Drawing.Size(1558, 573);
            this.dgvJoin.TabIndex = 0;
            this.dgvJoin.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJoin_CellClick);
            this.dgvJoin.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJoin_CellContentClick);
            this.dgvJoin.Sorted += new System.EventHandler(this.dgvJoin_Sorted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 28);
            this.label1.TabIndex = 36;
            this.label1.Text = "Category";
            // 
            // cmbCat
            // 
            this.cmbCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCat.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbCat.FormattingEnabled = true;
            this.cmbCat.Items.AddRange(new object[] {
            "Parent",
            "Child"});
            this.cmbCat.Location = new System.Drawing.Point(137, 59);
            this.cmbCat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbCat.Name = "cmbCat";
            this.cmbCat.Size = new System.Drawing.Size(363, 36);
            this.cmbCat.TabIndex = 35;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnDelete.Location = new System.Drawing.Point(1447, 56);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 50);
            this.btnDelete.TabIndex = 33;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnAdd.Location = new System.Drawing.Point(1299, 55);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(122, 52);
            this.btnAdd.TabIndex = 32;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(599, 59);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(363, 34);
            this.txtSearch.TabIndex = 31;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(523, 56);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(70, 28);
            this.lblSearch.TabIndex = 30;
            this.lblSearch.Text = "Search";
            // 
            // join_parent_code
            // 
            this.join_parent_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.join_parent_code.HeaderText = "Parent Code";
            this.join_parent_code.Name = "join_parent_code";
            this.join_parent_code.ReadOnly = true;
            // 
            // join_parent_name
            // 
            this.join_parent_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.join_parent_name.HeaderText = "Parent Name";
            this.join_parent_name.Name = "join_parent_name";
            this.join_parent_name.ReadOnly = true;
            // 
            // join_child_code
            // 
            this.join_child_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.join_child_code.HeaderText = "Child Code";
            this.join_child_code.Name = "join_child_code";
            this.join_child_code.ReadOnly = true;
            // 
            // join_child_name
            // 
            this.join_child_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.join_child_name.HeaderText = "Child Name";
            this.join_child_name.Name = "join_child_name";
            this.join_child_name.ReadOnly = true;
            // 
            // join_qty
            // 
            this.join_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.join_qty.HeaderText = "qty";
            this.join_qty.Name = "join_qty";
            this.join_qty.ReadOnly = true;
            this.join_qty.Width = 63;
            // 
            // frmJoin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCat);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dgvJoin);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmJoin";
            this.Text = "Join";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmJoin_FormClosed);
            this.Load += new System.EventHandler(this.frmJoin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJoin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvJoin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCat;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn join_parent_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn join_parent_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn join_child_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn join_child_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn join_qty;
    }
}