namespace FactoryManagementSoftware.UI
{
    partial class frmSBBPrice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnItemPrice = new System.Windows.Forms.Button();
            this.btnCustomerDiscount = new System.Windows.Forms.Button();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblMainList = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSetAll = new System.Windows.Forms.Button();
            this.txtSetAll = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnItemPrice
            // 
            this.btnItemPrice.BackColor = System.Drawing.Color.White;
            this.btnItemPrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnItemPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemPrice.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemPrice.ForeColor = System.Drawing.Color.Black;
            this.btnItemPrice.Location = new System.Drawing.Point(13, 19);
            this.btnItemPrice.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnItemPrice.Name = "btnItemPrice";
            this.btnItemPrice.Size = new System.Drawing.Size(191, 32);
            this.btnItemPrice.TabIndex = 168;
            this.btnItemPrice.Text = "ITEM PRICE";
            this.btnItemPrice.UseVisualStyleBackColor = false;
            this.btnItemPrice.Click += new System.EventHandler(this.btnItemPrice_Click);
            // 
            // btnCustomerDiscount
            // 
            this.btnCustomerDiscount.BackColor = System.Drawing.Color.White;
            this.btnCustomerDiscount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomerDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomerDiscount.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomerDiscount.ForeColor = System.Drawing.Color.Black;
            this.btnCustomerDiscount.Location = new System.Drawing.Point(212, 19);
            this.btnCustomerDiscount.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnCustomerDiscount.Name = "btnCustomerDiscount";
            this.btnCustomerDiscount.Size = new System.Drawing.Size(191, 32);
            this.btnCustomerDiscount.TabIndex = 169;
            this.btnCustomerDiscount.Text = "CUSTOMER DISCOUNT";
            this.btnCustomerDiscount.UseVisualStyleBackColor = false;
            this.btnCustomerDiscount.Click += new System.EventHandler(this.btnCustomerDiscount_Click);
            // 
            // dgvItemList
            // 
            this.dgvItemList.AllowDrop = true;
            this.dgvItemList.AllowUserToAddRows = false;
            this.dgvItemList.AllowUserToDeleteRows = false;
            this.dgvItemList.AllowUserToResizeColumns = false;
            this.dgvItemList.AllowUserToResizeRows = false;
            this.dgvItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvItemList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvItemList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvItemList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvItemList.GridColor = System.Drawing.Color.Silver;
            this.dgvItemList.Location = new System.Drawing.Point(8, 91);
            this.dgvItemList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvItemList.MultiSelect = false;
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.ReadOnly = true;
            this.dgvItemList.RowHeadersVisible = false;
            this.dgvItemList.RowTemplate.Height = 50;
            this.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemList.Size = new System.Drawing.Size(1333, 620);
            this.dgvItemList.TabIndex = 170;
            this.dgvItemList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellClick);
            this.dgvItemList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellEndEdit);
            this.dgvItemList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvItemList_EditingControlShowing);
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.lblCustomer.Location = new System.Drawing.Point(408, 11);
            this.lblCustomer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(56, 12);
            this.lblCustomer.TabIndex = 177;
            this.lblCustomer.Text = "CUSTOMER";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(410, 26);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbCustomer.Size = new System.Drawing.Size(313, 25);
            this.cmbCustomer.TabIndex = 178;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            this.cmbCustomer.Enter += new System.EventHandler(this.cmbCustomer_Enter_1);
            this.cmbCustomer.MouseEnter += new System.EventHandler(this.cmbCustomer_MouseEnter_1);
            // 
            // lblMainList
            // 
            this.lblMainList.AutoSize = true;
            this.lblMainList.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainList.Location = new System.Drawing.Point(4, 66);
            this.lblMainList.Margin = new System.Windows.Forms.Padding(2, 0, 2, 5);
            this.lblMainList.Name = "lblMainList";
            this.lblMainList.Size = new System.Drawing.Size(69, 19);
            this.lblMainList.TabIndex = 179;
            this.lblMainList.Text = "ITEM LIST";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(160)))), ((int)(((byte)(225)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Location = new System.Drawing.Point(1217, 58);
            this.btnSave.Margin = new System.Windows.Forms.Padding(1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 32);
            this.btnSave.TabIndex = 188;
            this.btnSave.Text = "SAVE";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSetAll
            // 
            this.btnSetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnSetAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSetAll.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetAll.ForeColor = System.Drawing.Color.White;
            this.btnSetAll.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSetAll.Location = new System.Drawing.Point(1050, 57);
            this.btnSetAll.Margin = new System.Windows.Forms.Padding(1);
            this.btnSetAll.Name = "btnSetAll";
            this.btnSetAll.Size = new System.Drawing.Size(124, 32);
            this.btnSetAll.TabIndex = 189;
            this.btnSetAll.Text = "SET ALL";
            this.btnSetAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSetAll.UseVisualStyleBackColor = false;
            this.btnSetAll.Visible = false;
            this.btnSetAll.Click += new System.EventHandler(this.btnSetAll_Click);
            // 
            // txtSetAll
            // 
            this.txtSetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSetAll.Location = new System.Drawing.Point(913, 57);
            this.txtSetAll.Name = "txtSetAll";
            this.txtSetAll.Size = new System.Drawing.Size(133, 25);
            this.txtSetAll.TabIndex = 190;
            this.txtSetAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSetAll.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSetAll_KeyPress);
            // 
            // frmSBBPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.txtSetAll);
            this.Controls.Add(this.btnSetAll);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblMainList);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.dgvItemList);
            this.Controls.Add(this.btnCustomerDiscount);
            this.Controls.Add(this.btnItemPrice);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSBBPrice";
            this.Text = "SBB Price";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSBBPrice_FormClosing);
            this.Load += new System.EventHandler(this.frmSBBPrice_Load);
            this.Shown += new System.EventHandler(this.frmSBBPrice_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnItemPrice;
        private System.Windows.Forms.Button btnCustomerDiscount;
        private System.Windows.Forms.DataGridView dgvItemList;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblMainList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSetAll;
        private System.Windows.Forms.TextBox txtSetAll;
    }
}