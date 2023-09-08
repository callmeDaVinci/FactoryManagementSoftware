namespace FactoryManagementSoftware
{
    partial class frmCartonSetting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvCarton = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddItem = new Guna.UI.WinForms.GunaGradientButton();
            this.btnSaveAndClose = new Guna.UI.WinForms.GunaGradientButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbCode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCarton
            // 
            this.dgvCarton.AllowUserToAddRows = false;
            this.dgvCarton.AllowUserToDeleteRows = false;
            this.dgvCarton.AllowUserToOrderColumns = true;
            this.dgvCarton.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvCarton.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCarton.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCarton.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarton.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCarton.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCarton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCarton.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCarton.Location = new System.Drawing.Point(0, 70);
            this.dgvCarton.Margin = new System.Windows.Forms.Padding(0);
            this.dgvCarton.MultiSelect = false;
            this.dgvCarton.Name = "dgvCarton";
            this.dgvCarton.RowHeadersVisible = false;
            this.dgvCarton.RowHeadersWidth = 51;
            this.dgvCarton.RowTemplate.Height = 50;
            this.dgvCarton.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCarton.Size = new System.Drawing.Size(925, 278);
            this.dgvCarton.TabIndex = 255;
            this.dgvCarton.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCarton_CellClick);
            this.dgvCarton.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCarton_CellMouseDown);
            this.dgvCarton.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCarton_EditingControlShowing);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddItem, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveAndClose, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(925, 60);
            this.tableLayoutPanel1.TabIndex = 262;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddItem.AnimationHoverSpeed = 0.07F;
            this.btnAddItem.AnimationSpeed = 0.03F;
            this.btnAddItem.BackColor = System.Drawing.Color.Transparent;
            this.btnAddItem.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(255)))), ((int)(((byte)(147)))));
            this.btnAddItem.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(255)))), ((int)(((byte)(147)))));
            this.btnAddItem.BorderColor = System.Drawing.Color.Black;
            this.btnAddItem.BorderSize = 1;
            this.btnAddItem.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAddItem.FocusedColor = System.Drawing.Color.Empty;
            this.btnAddItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAddItem.ForeColor = System.Drawing.Color.Black;
            this.btnAddItem.Image = null;
            this.btnAddItem.ImageSize = new System.Drawing.Size(20, 20);
            this.btnAddItem.Location = new System.Drawing.Point(615, 20);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnAddItem.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnAddItem.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnAddItem.OnHoverForeColor = System.Drawing.Color.White;
            this.btnAddItem.OnHoverImage = null;
            this.btnAddItem.OnPressedColor = System.Drawing.Color.Black;
            this.btnAddItem.Radius = 2;
            this.btnAddItem.Size = new System.Drawing.Size(150, 40);
            this.btnAddItem.TabIndex = 266;
            this.btnAddItem.Text = "Add To List";
            this.btnAddItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAndClose.AnimationHoverSpeed = 0.07F;
            this.btnSaveAndClose.AnimationSpeed = 0.03F;
            this.btnSaveAndClose.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveAndClose.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            this.btnSaveAndClose.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(127)))), ((int)(((byte)(255)))));
            this.btnSaveAndClose.BorderColor = System.Drawing.Color.Black;
            this.btnSaveAndClose.BorderSize = 1;
            this.btnSaveAndClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSaveAndClose.FocusedColor = System.Drawing.Color.Empty;
            this.btnSaveAndClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSaveAndClose.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSaveAndClose.Image = null;
            this.btnSaveAndClose.ImageSize = new System.Drawing.Size(20, 20);
            this.btnSaveAndClose.Location = new System.Drawing.Point(775, 20);
            this.btnSaveAndClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnSaveAndClose.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnSaveAndClose.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnSaveAndClose.OnHoverForeColor = System.Drawing.Color.White;
            this.btnSaveAndClose.OnHoverImage = null;
            this.btnSaveAndClose.OnPressedColor = System.Drawing.Color.Black;
            this.btnSaveAndClose.Radius = 2;
            this.btnSaveAndClose.Size = new System.Drawing.Size(150, 40);
            this.btnSaveAndClose.TabIndex = 266;
            this.btnSaveAndClose.Text = "Save";
            this.btnSaveAndClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnSaveAndClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbCategory, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbCode, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbName, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(605, 60);
            this.tableLayoutPanel2.TabIndex = 266;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 17);
            this.label3.TabIndex = 268;
            this.label3.Text = "CATEGORY";
            // 
            // cmbCategory
            // 
            this.cmbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(3, 33);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(144, 25);
            this.cmbCategory.TabIndex = 268;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // cmbCode
            // 
            this.cmbCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCode.FormattingEnabled = true;
            this.cmbCode.Location = new System.Drawing.Point(390, 33);
            this.cmbCode.Name = "cmbCode";
            this.cmbCode.Size = new System.Drawing.Size(212, 25);
            this.cmbCode.TabIndex = 268;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(390, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 268;
            this.label2.Text = "CODE";
            // 
            // cmbName
            // 
            this.cmbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new System.Drawing.Point(163, 33);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(211, 25);
            this.cmbName.TabIndex = 267;
            this.cmbName.SelectedIndexChanged += new System.EventHandler(this.cmbName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 267;
            this.label1.Text = "NAME";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dgvCarton, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(30, 30);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(30);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(925, 348);
            this.tableLayoutPanel3.TabIndex = 264;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(985, 408);
            this.tableLayoutPanel4.TabIndex = 265;
            // 
            // frmCartonSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(985, 408);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmCartonSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Packaging Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCartonSetting_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCartonSetting_FormClosed);
            this.Load += new System.EventHandler(this.frmItemGroupList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvCarton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbName;
        private System.Windows.Forms.Label label1;
        private Guna.UI.WinForms.GunaGradientButton btnSaveAndClose;
        private Guna.UI.WinForms.GunaGradientButton btnAddItem;
    }
}