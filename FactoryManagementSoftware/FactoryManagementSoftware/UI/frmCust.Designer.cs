namespace FactoryManagementSoftware.UI
{
    partial class frmCust
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
            this.txtCustSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvCust = new System.Windows.Forms.DataGridView();
            this.dgvcCustID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcCustName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.lblCustName = new System.Windows.Forms.Label();
            this.txtCustID = new System.Windows.Forms.TextBox();
            this.lblCustID = new System.Windows.Forms.Label();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCustSearch
            // 
            this.txtCustSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustSearch.Location = new System.Drawing.Point(730, 50);
            this.txtCustSearch.Name = "txtCustSearch";
            this.txtCustSearch.Size = new System.Drawing.Size(787, 38);
            this.txtCustSearch.TabIndex = 29;
            this.txtCustSearch.TextChanged += new System.EventHandler(this.txtCustSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(638, 50);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(86, 32);
            this.lblSearch.TabIndex = 28;
            this.lblSearch.Text = "Search";
            // 
            // dgvCust
            // 
            this.dgvCust.AllowUserToAddRows = false;
            this.dgvCust.AllowUserToDeleteRows = false;
            this.dgvCust.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCust.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcCustID,
            this.dgvcCustName});
            this.dgvCust.Location = new System.Drawing.Point(645, 120);
            this.dgvCust.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCust.Name = "dgvCust";
            this.dgvCust.ReadOnly = true;
            this.dgvCust.RowTemplate.Height = 24;
            this.dgvCust.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCust.Size = new System.Drawing.Size(872, 514);
            this.dgvCust.TabIndex = 27;
            this.dgvCust.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCust_CellMouseDoubleClick);
            // 
            // dgvcCustID
            // 
            this.dgvcCustID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvcCustID.HeaderText = "ID";
            this.dgvcCustID.Name = "dgvcCustID";
            this.dgvcCustID.ReadOnly = true;
            this.dgvcCustID.Width = 56;
            // 
            // dgvcCustName
            // 
            this.dgvcCustName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvcCustName.HeaderText = "Customer Name";
            this.dgvcCustName.Name = "dgvcCustName";
            this.dgvcCustName.ReadOnly = true;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(466, 586);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(111, 48);
            this.btnReset.TabIndex = 26;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert.Location = new System.Drawing.Point(320, 586);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(111, 48);
            this.btnInsert.TabIndex = 25;
            this.btnInsert.Text = "ADD";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(90, 586);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(111, 48);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtCustName
            // 
            this.txtCustName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustName.Location = new System.Drawing.Point(210, 150);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(367, 38);
            this.txtCustName.TabIndex = 23;
            this.txtCustName.TextChanged += new System.EventHandler(this.txtCustName_TextChanged);
            // 
            // lblCustName
            // 
            this.lblCustName.AutoSize = true;
            this.lblCustName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustName.Location = new System.Drawing.Point(5, 150);
            this.lblCustName.Name = "lblCustName";
            this.lblCustName.Size = new System.Drawing.Size(199, 32);
            this.lblCustName.TabIndex = 22;
            this.lblCustName.Text = "*Customer Name";
            // 
            // txtCustID
            // 
            this.txtCustID.Enabled = false;
            this.txtCustID.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustID.Location = new System.Drawing.Point(210, 50);
            this.txtCustID.Name = "txtCustID";
            this.txtCustID.ReadOnly = true;
            this.txtCustID.Size = new System.Drawing.Size(367, 38);
            this.txtCustID.TabIndex = 21;
            // 
            // lblCustID
            // 
            this.lblCustID.AutoSize = true;
            this.lblCustID.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustID.Location = new System.Drawing.Point(46, 50);
            this.lblCustID.Name = "lblCustID";
            this.lblCustID.Size = new System.Drawing.Size(158, 32);
            this.lblCustID.TabIndex = 20;
            this.lblCustID.Text = "*Customer ID";
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // frmCust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.txtCustSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dgvCust);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtCustName);
            this.Controls.Add(this.lblCustName);
            this.Controls.Add(this.txtCustID);
            this.Controls.Add(this.lblCustID);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCust";
            this.Text = "frmCust";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCust_FormClosed);
            this.Load += new System.EventHandler(this.frmCust_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCust)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCustSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvCust;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label lblCustName;
        private System.Windows.Forms.TextBox txtCustID;
        private System.Windows.Forms.Label lblCustID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCustID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcCustName;
        private System.Windows.Forms.ErrorProvider errorProvider2;
    }
}