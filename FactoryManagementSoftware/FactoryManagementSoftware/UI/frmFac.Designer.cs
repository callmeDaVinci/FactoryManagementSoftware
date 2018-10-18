namespace FactoryManagementSoftware.UI
{
    partial class frmFac
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
            this.txtFacSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvFac = new System.Windows.Forms.DataGridView();
            this.dgvcFacID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcFacName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtFacName = new System.Windows.Forms.TextBox();
            this.lblFacName = new System.Windows.Forms.Label();
            this.txtFacID = new System.Windows.Forms.TextBox();
            this.lblFacID = new System.Windows.Forms.Label();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFacSearch
            // 
            this.txtFacSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacSearch.Location = new System.Drawing.Point(730, 50);
            this.txtFacSearch.Name = "txtFacSearch";
            this.txtFacSearch.Size = new System.Drawing.Size(787, 38);
            this.txtFacSearch.TabIndex = 19;
            this.txtFacSearch.TextChanged += new System.EventHandler(this.txtFacSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(638, 50);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(86, 32);
            this.lblSearch.TabIndex = 18;
            this.lblSearch.Text = "Search";
            // 
            // dgvFac
            // 
            this.dgvFac.AllowUserToAddRows = false;
            this.dgvFac.AllowUserToDeleteRows = false;
            this.dgvFac.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFac.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcFacID,
            this.dgvcFacName});
            this.dgvFac.Location = new System.Drawing.Point(645, 120);
            this.dgvFac.Margin = new System.Windows.Forms.Padding(2);
            this.dgvFac.Name = "dgvFac";
            this.dgvFac.ReadOnly = true;
            this.dgvFac.RowTemplate.Height = 24;
            this.dgvFac.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFac.Size = new System.Drawing.Size(872, 514);
            this.dgvFac.TabIndex = 17;
            this.dgvFac.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFac_CellMouseDoubleClick);
            // 
            // dgvcFacID
            // 
            this.dgvcFacID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgvcFacID.HeaderText = "ID";
            this.dgvcFacID.Name = "dgvcFacID";
            this.dgvcFacID.ReadOnly = true;
            this.dgvcFacID.Width = 56;
            // 
            // dgvcFacName
            // 
            this.dgvcFacName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvcFacName.HeaderText = "Factory Name";
            this.dgvcFacName.Name = "dgvcFacName";
            this.dgvcFacName.ReadOnly = true;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(466, 586);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(111, 48);
            this.btnReset.TabIndex = 16;
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
            this.btnInsert.TabIndex = 15;
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
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtFacName
            // 
            this.txtFacName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacName.Location = new System.Drawing.Point(210, 150);
            this.txtFacName.Name = "txtFacName";
            this.txtFacName.Size = new System.Drawing.Size(367, 38);
            this.txtFacName.TabIndex = 13;
            this.txtFacName.TextChanged += new System.EventHandler(this.txtFacName_TextChanged);
            // 
            // lblFacName
            // 
            this.lblFacName.AutoSize = true;
            this.lblFacName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacName.Location = new System.Drawing.Point(32, 150);
            this.lblFacName.Name = "lblFacName";
            this.lblFacName.Size = new System.Drawing.Size(172, 32);
            this.lblFacName.TabIndex = 12;
            this.lblFacName.Text = "*Factory Name";
            // 
            // txtFacID
            // 
            this.txtFacID.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacID.Location = new System.Drawing.Point(210, 50);
            this.txtFacID.Name = "txtFacID";
            this.txtFacID.ReadOnly = true;
            this.txtFacID.Size = new System.Drawing.Size(367, 38);
            this.txtFacID.TabIndex = 11;
            // 
            // lblFacID
            // 
            this.lblFacID.AutoSize = true;
            this.lblFacID.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacID.Location = new System.Drawing.Point(73, 50);
            this.lblFacID.Name = "lblFacID";
            this.lblFacID.Size = new System.Drawing.Size(131, 32);
            this.lblFacID.TabIndex = 10;
            this.lblFacID.Text = "*Factory ID";
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // frmFac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.txtFacSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dgvFac);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtFacName);
            this.Controls.Add(this.lblFacName);
            this.Controls.Add(this.txtFacID);
            this.Controls.Add(this.lblFacID);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmFac";
            this.Text = "Factory";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFac_FormClosed);
            this.Load += new System.EventHandler(this.frmFac_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFacSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvFac;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtFacName;
        private System.Windows.Forms.Label lblFacName;
        private System.Windows.Forms.TextBox txtFacID;
        private System.Windows.Forms.Label lblFacID;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcFacID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcFacName;
    }
}