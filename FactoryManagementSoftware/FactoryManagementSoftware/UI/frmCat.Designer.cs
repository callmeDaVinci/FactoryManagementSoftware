namespace FactoryManagementSoftware.UI
{
    partial class frmCat
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
            this.txtItemCatSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvItemCat = new System.Windows.Forms.DataGridView();
            this.item_cat_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_cat_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnItemCatReset = new System.Windows.Forms.Button();
            this.btnItemCatInsert = new System.Windows.Forms.Button();
            this.btnItemCatDelete = new System.Windows.Forms.Button();
            this.txtItemCat = new System.Windows.Forms.TextBox();
            this.lblItemCat = new System.Windows.Forms.Label();
            this.txtItemCatID = new System.Windows.Forms.TextBox();
            this.lblItemCatID = new System.Windows.Forms.Label();
            this.txtTrfCatSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTrfCat = new System.Windows.Forms.DataGridView();
            this.trf_cat_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_cat_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTrfCatReset = new System.Windows.Forms.Button();
            this.btnTrfCatInsert = new System.Windows.Forms.Button();
            this.btnTrfCatDelete = new System.Windows.Forms.Button();
            this.txtTrfCat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTrfCatID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemCat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrfCat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtItemCatSearch
            // 
            this.txtItemCatSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemCatSearch.Location = new System.Drawing.Point(179, 341);
            this.txtItemCatSearch.Name = "txtItemCatSearch";
            this.txtItemCatSearch.Size = new System.Drawing.Size(448, 38);
            this.txtItemCatSearch.TabIndex = 29;
            this.txtItemCatSearch.TextChanged += new System.EventHandler(this.txtItemCatSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(87, 341);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(86, 32);
            this.lblSearch.TabIndex = 28;
            this.lblSearch.Text = "Search";
            // 
            // dgvItemCat
            // 
            this.dgvItemCat.AllowUserToAddRows = false;
            this.dgvItemCat.AllowUserToDeleteRows = false;
            this.dgvItemCat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemCat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item_cat_id,
            this.item_cat_name});
            this.dgvItemCat.Location = new System.Drawing.Point(88, 384);
            this.dgvItemCat.Margin = new System.Windows.Forms.Padding(2);
            this.dgvItemCat.Name = "dgvItemCat";
            this.dgvItemCat.ReadOnly = true;
            this.dgvItemCat.RowTemplate.Height = 24;
            this.dgvItemCat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemCat.Size = new System.Drawing.Size(539, 290);
            this.dgvItemCat.TabIndex = 27;
            this.dgvItemCat.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemCat_CellMouseDoubleClick);
            // 
            // item_cat_id
            // 
            this.item_cat_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.item_cat_id.HeaderText = "ID";
            this.item_cat_id.Name = "item_cat_id";
            this.item_cat_id.ReadOnly = true;
            this.item_cat_id.Width = 56;
            // 
            // item_cat_name
            // 
            this.item_cat_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_cat_name.HeaderText = "Name";
            this.item_cat_name.Name = "item_cat_name";
            this.item_cat_name.ReadOnly = true;
            // 
            // btnItemCatReset
            // 
            this.btnItemCatReset.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemCatReset.Location = new System.Drawing.Point(516, 225);
            this.btnItemCatReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnItemCatReset.Name = "btnItemCatReset";
            this.btnItemCatReset.Size = new System.Drawing.Size(111, 48);
            this.btnItemCatReset.TabIndex = 26;
            this.btnItemCatReset.Text = "RESET";
            this.btnItemCatReset.UseVisualStyleBackColor = true;
            this.btnItemCatReset.Click += new System.EventHandler(this.btnItemCatReset_Click);
            // 
            // btnItemCatInsert
            // 
            this.btnItemCatInsert.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemCatInsert.Location = new System.Drawing.Point(370, 225);
            this.btnItemCatInsert.Margin = new System.Windows.Forms.Padding(2);
            this.btnItemCatInsert.Name = "btnItemCatInsert";
            this.btnItemCatInsert.Size = new System.Drawing.Size(111, 48);
            this.btnItemCatInsert.TabIndex = 25;
            this.btnItemCatInsert.Text = "ADD";
            this.btnItemCatInsert.UseVisualStyleBackColor = true;
            this.btnItemCatInsert.Click += new System.EventHandler(this.btnItemCatInsert_Click);
            // 
            // btnItemCatDelete
            // 
            this.btnItemCatDelete.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemCatDelete.Location = new System.Drawing.Point(140, 225);
            this.btnItemCatDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnItemCatDelete.Name = "btnItemCatDelete";
            this.btnItemCatDelete.Size = new System.Drawing.Size(111, 48);
            this.btnItemCatDelete.TabIndex = 24;
            this.btnItemCatDelete.Text = "DELETE";
            this.btnItemCatDelete.UseVisualStyleBackColor = true;
            this.btnItemCatDelete.Click += new System.EventHandler(this.btnItemCatDelete_Click);
            // 
            // txtItemCat
            // 
            this.txtItemCat.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemCat.Location = new System.Drawing.Point(260, 109);
            this.txtItemCat.Name = "txtItemCat";
            this.txtItemCat.Size = new System.Drawing.Size(367, 38);
            this.txtItemCat.TabIndex = 23;
            this.txtItemCat.TextChanged += new System.EventHandler(this.txtItemCat_TextChanged);
            // 
            // lblItemCat
            // 
            this.lblItemCat.AutoSize = true;
            this.lblItemCat.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCat.Location = new System.Drawing.Point(78, 109);
            this.lblItemCat.Name = "lblItemCat";
            this.lblItemCat.Size = new System.Drawing.Size(176, 32);
            this.lblItemCat.TabIndex = 22;
            this.lblItemCat.Text = "*Item Category";
            // 
            // txtItemCatID
            // 
            this.txtItemCatID.Enabled = false;
            this.txtItemCatID.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemCatID.Location = new System.Drawing.Point(260, 41);
            this.txtItemCatID.Name = "txtItemCatID";
            this.txtItemCatID.ReadOnly = true;
            this.txtItemCatID.Size = new System.Drawing.Size(367, 38);
            this.txtItemCatID.TabIndex = 21;
            // 
            // lblItemCatID
            // 
            this.lblItemCatID.AutoSize = true;
            this.lblItemCatID.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCatID.Location = new System.Drawing.Point(216, 41);
            this.lblItemCatID.Name = "lblItemCatID";
            this.lblItemCatID.Size = new System.Drawing.Size(38, 32);
            this.lblItemCatID.TabIndex = 20;
            this.lblItemCatID.Text = "ID";
            // 
            // txtTrfCatSearch
            // 
            this.txtTrfCatSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrfCatSearch.Location = new System.Drawing.Point(957, 341);
            this.txtTrfCatSearch.Name = "txtTrfCatSearch";
            this.txtTrfCatSearch.Size = new System.Drawing.Size(448, 38);
            this.txtTrfCatSearch.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(865, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 32);
            this.label1.TabIndex = 38;
            this.label1.Text = "Search";
            // 
            // dgvTrfCat
            // 
            this.dgvTrfCat.AllowUserToAddRows = false;
            this.dgvTrfCat.AllowUserToDeleteRows = false;
            this.dgvTrfCat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrfCat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trf_cat_id,
            this.trf_cat_name});
            this.dgvTrfCat.Location = new System.Drawing.Point(866, 384);
            this.dgvTrfCat.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTrfCat.Name = "dgvTrfCat";
            this.dgvTrfCat.ReadOnly = true;
            this.dgvTrfCat.RowTemplate.Height = 24;
            this.dgvTrfCat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrfCat.Size = new System.Drawing.Size(539, 290);
            this.dgvTrfCat.TabIndex = 37;
            this.dgvTrfCat.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTrfCat_CellMouseDoubleClick);
            // 
            // trf_cat_id
            // 
            this.trf_cat_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.trf_cat_id.HeaderText = "ID";
            this.trf_cat_id.Name = "trf_cat_id";
            this.trf_cat_id.ReadOnly = true;
            this.trf_cat_id.Width = 56;
            // 
            // trf_cat_name
            // 
            this.trf_cat_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.trf_cat_name.HeaderText = "Name";
            this.trf_cat_name.Name = "trf_cat_name";
            this.trf_cat_name.ReadOnly = true;
            // 
            // btnTrfCatReset
            // 
            this.btnTrfCatReset.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrfCatReset.Location = new System.Drawing.Point(1294, 225);
            this.btnTrfCatReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnTrfCatReset.Name = "btnTrfCatReset";
            this.btnTrfCatReset.Size = new System.Drawing.Size(111, 48);
            this.btnTrfCatReset.TabIndex = 36;
            this.btnTrfCatReset.Text = "RESET";
            this.btnTrfCatReset.UseVisualStyleBackColor = true;
            // 
            // btnTrfCatInsert
            // 
            this.btnTrfCatInsert.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrfCatInsert.Location = new System.Drawing.Point(1148, 225);
            this.btnTrfCatInsert.Margin = new System.Windows.Forms.Padding(2);
            this.btnTrfCatInsert.Name = "btnTrfCatInsert";
            this.btnTrfCatInsert.Size = new System.Drawing.Size(111, 48);
            this.btnTrfCatInsert.TabIndex = 35;
            this.btnTrfCatInsert.Text = "ADD";
            this.btnTrfCatInsert.UseVisualStyleBackColor = true;
            // 
            // btnTrfCatDelete
            // 
            this.btnTrfCatDelete.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrfCatDelete.Location = new System.Drawing.Point(918, 225);
            this.btnTrfCatDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnTrfCatDelete.Name = "btnTrfCatDelete";
            this.btnTrfCatDelete.Size = new System.Drawing.Size(111, 48);
            this.btnTrfCatDelete.TabIndex = 34;
            this.btnTrfCatDelete.Text = "DELETE";
            this.btnTrfCatDelete.UseVisualStyleBackColor = true;
            // 
            // txtTrfCat
            // 
            this.txtTrfCat.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrfCat.Location = new System.Drawing.Point(1038, 109);
            this.txtTrfCat.Name = "txtTrfCat";
            this.txtTrfCat.Size = new System.Drawing.Size(367, 38);
            this.txtTrfCat.TabIndex = 33;
            this.txtTrfCat.TextChanged += new System.EventHandler(this.txtTrfCat_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(877, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 32);
            this.label2.TabIndex = 32;
            this.label2.Text = "*Trf Category";
            // 
            // txtTrfCatID
            // 
            this.txtTrfCatID.Enabled = false;
            this.txtTrfCatID.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrfCatID.Location = new System.Drawing.Point(1038, 41);
            this.txtTrfCatID.Name = "txtTrfCatID";
            this.txtTrfCatID.ReadOnly = true;
            this.txtTrfCatID.Size = new System.Drawing.Size(367, 38);
            this.txtTrfCatID.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(994, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 32);
            this.label3.TabIndex = 30;
            this.label3.Text = "ID";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // frmCat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.txtTrfCatSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTrfCat);
            this.Controls.Add(this.btnTrfCatReset);
            this.Controls.Add(this.btnTrfCatInsert);
            this.Controls.Add(this.btnTrfCatDelete);
            this.Controls.Add(this.txtTrfCat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTrfCatID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtItemCatSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dgvItemCat);
            this.Controls.Add(this.btnItemCatReset);
            this.Controls.Add(this.btnItemCatInsert);
            this.Controls.Add(this.btnItemCatDelete);
            this.Controls.Add(this.txtItemCat);
            this.Controls.Add(this.lblItemCat);
            this.Controls.Add(this.txtItemCatID);
            this.Controls.Add(this.lblItemCatID);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCat";
            this.Text = "frmCat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCat_FormClosed);
            this.Load += new System.EventHandler(this.frmCat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemCat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrfCat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtItemCatSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvItemCat;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_cat_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_cat_name;
        private System.Windows.Forms.Button btnItemCatReset;
        private System.Windows.Forms.Button btnItemCatInsert;
        private System.Windows.Forms.Button btnItemCatDelete;
        private System.Windows.Forms.TextBox txtItemCat;
        private System.Windows.Forms.Label lblItemCat;
        private System.Windows.Forms.TextBox txtItemCatID;
        private System.Windows.Forms.Label lblItemCatID;
        private System.Windows.Forms.TextBox txtTrfCatSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTrfCat;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_cat_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_cat_name;
        private System.Windows.Forms.Button btnTrfCatReset;
        private System.Windows.Forms.Button btnTrfCatInsert;
        private System.Windows.Forms.Button btnTrfCatDelete;
        private System.Windows.Forms.TextBox txtTrfCat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTrfCatID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
    }
}