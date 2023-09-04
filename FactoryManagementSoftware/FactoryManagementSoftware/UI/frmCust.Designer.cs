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
            this.btnReset = new System.Windows.Forms.Button();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.lblCustName = new System.Windows.Forms.Label();
            this.txtCustID = new System.Windows.Forms.TextBox();
            this.lblCustID = new System.Windows.Forms.Label();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMain = new System.Windows.Forms.ComboBox();
            this.btnInsert = new Guna.UI.WinForms.GunaGradientButton();
            this.btnDelete = new Guna.UI.WinForms.GunaGradientButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCustSearch
            // 
            this.txtCustSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtCustSearch.Location = new System.Drawing.Point(452, 47);
            this.txtCustSearch.Name = "txtCustSearch";
            this.txtCustSearch.Size = new System.Drawing.Size(583, 34);
            this.txtCustSearch.TabIndex = 29;
            this.txtCustSearch.TextChanged += new System.EventHandler(this.txtCustSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.Location = new System.Drawing.Point(448, 21);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(61, 23);
            this.lblSearch.TabIndex = 28;
            this.lblSearch.Text = "Search";
            // 
            // dgvCust
            // 
            this.dgvCust.AllowUserToAddRows = false;
            this.dgvCust.AllowUserToDeleteRows = false;
            this.dgvCust.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvCust.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCust.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCust.Location = new System.Drawing.Point(452, 134);
            this.dgvCust.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCust.Name = "dgvCust";
            this.dgvCust.ReadOnly = true;
            this.dgvCust.RowHeadersWidth = 51;
            this.dgvCust.RowTemplate.Height = 24;
            this.dgvCust.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCust.Size = new System.Drawing.Size(583, 416);
            this.dgvCust.TabIndex = 27;
            this.dgvCust.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCust_CellMouseDoubleClick);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReset.Location = new System.Drawing.Point(924, 96);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(111, 35);
            this.btnReset.TabIndex = 26;
            this.btnReset.Text = "Reload";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtCustName
            // 
            this.txtCustName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtCustName.Location = new System.Drawing.Point(53, 134);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(367, 34);
            this.txtCustName.TabIndex = 23;
            this.txtCustName.TextChanged += new System.EventHandler(this.txtCustName_TextChanged);
            // 
            // lblCustName
            // 
            this.lblCustName.AutoSize = true;
            this.lblCustName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustName.Location = new System.Drawing.Point(49, 108);
            this.lblCustName.Name = "lblCustName";
            this.lblCustName.Size = new System.Drawing.Size(135, 23);
            this.lblCustName.TabIndex = 22;
            this.lblCustName.Text = "Customer Name";
            // 
            // txtCustID
            // 
            this.txtCustID.Enabled = false;
            this.txtCustID.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtCustID.Location = new System.Drawing.Point(53, 51);
            this.txtCustID.Name = "txtCustID";
            this.txtCustID.ReadOnly = true;
            this.txtCustID.Size = new System.Drawing.Size(367, 34);
            this.txtCustID.TabIndex = 21;
            // 
            // lblCustID
            // 
            this.lblCustID.AutoSize = true;
            this.lblCustID.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustID.Location = new System.Drawing.Point(49, 29);
            this.lblCustID.Name = "lblCustID";
            this.lblCustID.Size = new System.Drawing.Size(106, 23);
            this.lblCustID.TabIndex = 20;
            this.lblCustID.Text = "Customer ID";
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(49, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 30;
            this.label1.Text = "Main";
            // 
            // cmbMain
            // 
            this.cmbMain.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbMain.FormattingEnabled = true;
            this.cmbMain.Location = new System.Drawing.Point(53, 225);
            this.cmbMain.Name = "cmbMain";
            this.cmbMain.Size = new System.Drawing.Size(367, 36);
            this.cmbMain.TabIndex = 31;
            // 
            // btnInsert
            // 
            this.btnInsert.AnimationHoverSpeed = 0.07F;
            this.btnInsert.AnimationSpeed = 0.03F;
            this.btnInsert.BackColor = System.Drawing.Color.Transparent;
            this.btnInsert.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnInsert.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(127)))), ((int)(((byte)(236)))));
            this.btnInsert.BorderColor = System.Drawing.Color.Black;
            this.btnInsert.BorderSize = 1;
            this.btnInsert.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnInsert.FocusedColor = System.Drawing.Color.Empty;
            this.btnInsert.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnInsert.ForeColor = System.Drawing.Color.White;
            this.btnInsert.Image = null;
            this.btnInsert.ImageSize = new System.Drawing.Size(20, 20);
            this.btnInsert.Location = new System.Drawing.Point(53, 310);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnInsert.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnInsert.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnInsert.OnHoverForeColor = System.Drawing.Color.White;
            this.btnInsert.OnHoverImage = null;
            this.btnInsert.OnPressedColor = System.Drawing.Color.Black;
            this.btnInsert.Radius = 2;
            this.btnInsert.Size = new System.Drawing.Size(367, 40);
            this.btnInsert.TabIndex = 226;
            this.btnInsert.Text = "Add";
            this.btnInsert.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AnimationHoverSpeed = 0.07F;
            this.btnDelete.AnimationSpeed = 0.03F;
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(159)))));
            this.btnDelete.BaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(147)))), ((int)(((byte)(168)))));
            this.btnDelete.BorderColor = System.Drawing.Color.Black;
            this.btnDelete.BorderSize = 1;
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelete.FocusedColor = System.Drawing.Color.Empty;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDelete.Image = null;
            this.btnDelete.ImageSize = new System.Drawing.Size(20, 20);
            this.btnDelete.Location = new System.Drawing.Point(53, 368);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.OnHoverBaseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(145)))), ((int)(((byte)(221)))));
            this.btnDelete.OnHoverBaseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(255)))));
            this.btnDelete.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnDelete.OnHoverForeColor = System.Drawing.Color.White;
            this.btnDelete.OnHoverImage = null;
            this.btnDelete.OnPressedColor = System.Drawing.Color.Black;
            this.btnDelete.Radius = 2;
            this.btnDelete.Size = new System.Drawing.Size(367, 40);
            this.btnDelete.TabIndex = 227;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmCust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1084, 583);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.cmbMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCustSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dgvCust);
            this.Controls.Add(this.btnReset);
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
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label lblCustName;
        private System.Windows.Forms.TextBox txtCustID;
        private System.Windows.Forms.Label lblCustID;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ComboBox cmbMain;
        private System.Windows.Forms.Label label1;
        private Guna.UI.WinForms.GunaGradientButton btnInsert;
        private Guna.UI.WinForms.GunaGradientButton btnDelete;
    }
}