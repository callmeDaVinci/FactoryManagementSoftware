namespace FactoryManagementSoftware.UI
{
    partial class frmProPackaging
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPackaging = new System.Windows.Forms.DataGridView();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbPackingCode = new System.Windows.Forms.ComboBox();
            this.lblPcs = new System.Windows.Forms.Label();
            this.cmbPackingName = new System.Windows.Forms.ComboBox();
            this.txtPackagingMax = new System.Windows.Forms.TextBox();
            this.txtTotalBox = new System.Windows.Forms.TextBox();
            this.lblTotalBox = new System.Windows.Forms.Label();
            this.lblBoxName = new System.Windows.Forms.Label();
            this.lblBoxCode = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackaging)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPackaging
            // 
            this.dgvPackaging.AllowUserToAddRows = false;
            this.dgvPackaging.AllowUserToDeleteRows = false;
            this.dgvPackaging.AllowUserToOrderColumns = true;
            this.dgvPackaging.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvPackaging.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPackaging.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvPackaging.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPackaging.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPackaging.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvPackaging.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPackaging.Location = new System.Drawing.Point(13, 134);
            this.dgvPackaging.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvPackaging.MultiSelect = false;
            this.dgvPackaging.Name = "dgvPackaging";
            this.dgvPackaging.ReadOnly = true;
            this.dgvPackaging.RowHeadersVisible = false;
            this.dgvPackaging.RowTemplate.Height = 40;
            this.dgvPackaging.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPackaging.Size = new System.Drawing.Size(848, 315);
            this.dgvPackaging.TabIndex = 154;
            this.dgvPackaging.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPackaging_CellDoubleClick);
            this.dgvPackaging.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPackaging_CellMouseDown);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.White;
            this.btnAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.ForeColor = System.Drawing.Color.Black;
            this.btnAddItem.Location = new System.Drawing.Point(736, 67);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(125, 36);
            this.btnAddItem.TabIndex = 166;
            this.btnAddItem.Text = "ADD ITEM";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnDone
            // 
            this.btnDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.ForeColor = System.Drawing.Color.White;
            this.btnDone.Location = new System.Drawing.Point(736, 451);
            this.btnDone.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(125, 36);
            this.btnDone.TabIndex = 167;
            this.btnDone.Text = "DONE";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnSaveAndStock_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(604, 451);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 36);
            this.button1.TabIndex = 168;
            this.button1.Text = "CANCEL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // cmbPackingCode
            // 
            this.cmbPackingCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPackingCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPackingCode.FormattingEnabled = true;
            this.cmbPackingCode.Location = new System.Drawing.Point(61, 78);
            this.cmbPackingCode.Name = "cmbPackingCode";
            this.cmbPackingCode.Size = new System.Drawing.Size(283, 25);
            this.cmbPackingCode.TabIndex = 250;
            // 
            // lblPcs
            // 
            this.lblPcs.AutoSize = true;
            this.lblPcs.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPcs.Location = new System.Drawing.Point(414, 43);
            this.lblPcs.Name = "lblPcs";
            this.lblPcs.Size = new System.Drawing.Size(74, 19);
            this.lblPcs.TabIndex = 249;
            this.lblPcs.Text = "QTY / BOX";
            // 
            // cmbPackingName
            // 
            this.cmbPackingName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPackingName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPackingName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPackingName.FormattingEnabled = true;
            this.cmbPackingName.Location = new System.Drawing.Point(61, 43);
            this.cmbPackingName.Name = "cmbPackingName";
            this.cmbPackingName.Size = new System.Drawing.Size(283, 25);
            this.cmbPackingName.TabIndex = 248;
            this.cmbPackingName.SelectedIndexChanged += new System.EventHandler(this.cmbPackingName_SelectedIndexChanged);
            // 
            // txtPackagingMax
            // 
            this.txtPackagingMax.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackagingMax.Location = new System.Drawing.Point(494, 40);
            this.txtPackagingMax.Name = "txtPackagingMax";
            this.txtPackagingMax.Size = new System.Drawing.Size(123, 25);
            this.txtPackagingMax.TabIndex = 247;
            this.txtPackagingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPackagingMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPackingQty_KeyPress);
            // 
            // txtTotalBox
            // 
            this.txtTotalBox.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBox.Location = new System.Drawing.Point(494, 78);
            this.txtTotalBox.Name = "txtTotalBox";
            this.txtTotalBox.Size = new System.Drawing.Size(123, 25);
            this.txtTotalBox.TabIndex = 251;
            this.txtTotalBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalBox.TextChanged += new System.EventHandler(this.txtTotalBox_TextChanged);
            this.txtTotalBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalBox_KeyPress);
            // 
            // lblTotalBox
            // 
            this.lblTotalBox.AutoSize = true;
            this.lblTotalBox.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBox.Location = new System.Drawing.Point(412, 78);
            this.lblTotalBox.Name = "lblTotalBox";
            this.lblTotalBox.Size = new System.Drawing.Size(76, 19);
            this.lblTotalBox.TabIndex = 252;
            this.lblTotalBox.Text = "TOTAL QTY";
            this.lblTotalBox.Click += new System.EventHandler(this.lblTotalBox_Click);
            // 
            // lblBoxName
            // 
            this.lblBoxName.AutoSize = true;
            this.lblBoxName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoxName.Location = new System.Drawing.Point(9, 46);
            this.lblBoxName.Name = "lblBoxName";
            this.lblBoxName.Size = new System.Drawing.Size(48, 19);
            this.lblBoxName.TabIndex = 253;
            this.lblBoxName.Text = "NAME";
            // 
            // lblBoxCode
            // 
            this.lblBoxCode.AutoSize = true;
            this.lblBoxCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoxCode.Location = new System.Drawing.Point(12, 88);
            this.lblBoxCode.Name = "lblBoxCode";
            this.lblBoxCode.Size = new System.Drawing.Size(46, 19);
            this.lblBoxCode.TabIndex = 254;
            this.lblBoxCode.Text = "CODE";
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // frmProPackaging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(874, 546);
            this.Controls.Add(this.lblBoxCode);
            this.Controls.Add(this.lblBoxName);
            this.Controls.Add(this.lblTotalBox);
            this.Controls.Add(this.txtTotalBox);
            this.Controls.Add(this.cmbPackingCode);
            this.Controls.Add(this.lblPcs);
            this.Controls.Add(this.cmbPackingName);
            this.Controls.Add(this.txtPackagingMax);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.dgvPackaging);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmProPackaging";
            this.Text = "Packaging Data";
            this.Load += new System.EventHandler(this.frmProPackaging_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackaging)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPackaging;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbPackingCode;
        private System.Windows.Forms.Label lblPcs;
        private System.Windows.Forms.ComboBox cmbPackingName;
        private System.Windows.Forms.TextBox txtPackagingMax;
        private System.Windows.Forms.TextBox txtTotalBox;
        private System.Windows.Forms.Label lblTotalBox;
        private System.Windows.Forms.Label lblBoxName;
        private System.Windows.Forms.Label lblBoxCode;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}