namespace FactoryManagementSoftware.UI
{
    partial class POAttachmentForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelDragDrop = new System.Windows.Forms.Panel();
            this.btnBrowse = new Guna.UI.WinForms.GunaButton();
            this.btnAdd = new Guna.UI.WinForms.GunaButton();
            this.lblDragInstructions = new System.Windows.Forms.Label();
            this.dgvMainList = new System.Windows.Forms.DataGridView();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnRefreshList = new Guna.UI.WinForms.GunaButton();
            this.btnViewSummary = new Guna.UI.WinForms.GunaButton();
            this.btnOpenSelected = new Guna.UI.WinForms.GunaButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.picDocument = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelDragDrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainList)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDocument)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelDragDrop, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvMainList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelButtons, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 247F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(658, 613);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelDragDrop
            // 
            this.panelDragDrop.AllowDrop = true;
            this.panelDragDrop.BackColor = System.Drawing.Color.White;
            this.panelDragDrop.Controls.Add(this.tableLayoutPanel3);
            this.panelDragDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDragDrop.Location = new System.Drawing.Point(5, 5);
            this.panelDragDrop.Margin = new System.Windows.Forms.Padding(5);
            this.panelDragDrop.Name = "panelDragDrop";
            this.panelDragDrop.Padding = new System.Windows.Forms.Padding(5);
            this.panelDragDrop.Size = new System.Drawing.Size(648, 237);
            this.panelDragDrop.TabIndex = 0;
            this.panelDragDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.POAttachmentForm_DragDrop);
            this.panelDragDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.POAttachmentForm_DragEnter);
            this.panelDragDrop.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDragDrop_Paint);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.AnimationHoverSpeed = 0.07F;
            this.btnBrowse.AnimationSpeed = 0.03F;
            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.BaseColor = System.Drawing.Color.White;
            this.btnBrowse.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowse.BorderSize = 1;
            this.btnBrowse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnBrowse.FocusedColor = System.Drawing.Color.Empty;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Image = null;
            this.btnBrowse.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnBrowse.ImageSize = new System.Drawing.Size(20, 20);
            this.btnBrowse.Location = new System.Drawing.Point(161, 5);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(5);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnBrowse.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnBrowse.OnHoverForeColor = System.Drawing.Color.White;
            this.btnBrowse.OnHoverImage = null;
            this.btnBrowse.OnPressedColor = System.Drawing.Color.Black;
            this.btnBrowse.Radius = 3;
            this.btnBrowse.Size = new System.Drawing.Size(150, 39);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AnimationHoverSpeed = 0.07F;
            this.btnAdd.AnimationSpeed = 0.03F;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAdd.BorderColor = System.Drawing.Color.Black;
            this.btnAdd.BorderSize = 1;
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.FocusedColor = System.Drawing.Color.Empty;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = null;
            this.btnAdd.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnAdd.ImageSize = new System.Drawing.Size(20, 20);
            this.btnAdd.Location = new System.Drawing.Point(321, 5);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnAdd.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnAdd.OnHoverForeColor = System.Drawing.Color.White;
            this.btnAdd.OnHoverImage = null;
            this.btnAdd.OnPressedColor = System.Drawing.Color.Black;
            this.btnAdd.Radius = 3;
            this.btnAdd.Size = new System.Drawing.Size(150, 39);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add Document";
            this.btnAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblDragInstructions
            // 
            this.lblDragInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDragInstructions.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDragInstructions.Location = new System.Drawing.Point(2, 144);
            this.lblDragInstructions.Margin = new System.Windows.Forms.Padding(2);
            this.lblDragInstructions.Name = "lblDragInstructions";
            this.lblDragInstructions.Size = new System.Drawing.Size(634, 26);
            this.lblDragInstructions.TabIndex = 1;
            this.lblDragInstructions.Text = "Drag and drop your PDF file here, or click below to browse.";
            this.lblDragInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvMainList
            // 
            this.dgvMainList.AllowUserToAddRows = false;
            this.dgvMainList.AllowUserToDeleteRows = false;
            this.dgvMainList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMainList.BackgroundColor = System.Drawing.Color.White;
            this.dgvMainList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMainList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMainList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMainList.Location = new System.Drawing.Point(3, 250);
            this.dgvMainList.MultiSelect = false;
            this.dgvMainList.Name = "dgvMainList";
            this.dgvMainList.ReadOnly = true;
            this.dgvMainList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMainList.RowHeadersVisible = false;
            this.dgvMainList.RowHeadersWidth = 51;
            this.dgvMainList.RowTemplate.Height = 24;
            this.dgvMainList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMainList.Size = new System.Drawing.Size(652, 300);
            this.dgvMainList.TabIndex = 1;
            this.dgvMainList.SelectionChanged += new System.EventHandler(this.dgvMainList_SelectionChanged);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.tableLayoutPanel2);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(3, 556);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(652, 54);
            this.panelButtons.TabIndex = 2;
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.AnimationHoverSpeed = 0.07F;
            this.btnRefreshList.AnimationSpeed = 0.03F;
            this.btnRefreshList.BackColor = System.Drawing.Color.Transparent;
            this.btnRefreshList.BaseColor = System.Drawing.Color.White;
            this.btnRefreshList.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRefreshList.BorderSize = 1;
            this.btnRefreshList.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRefreshList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefreshList.FocusedColor = System.Drawing.Color.Empty;
            this.btnRefreshList.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefreshList.ForeColor = System.Drawing.Color.Black;
            this.btnRefreshList.Image = null;
            this.btnRefreshList.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnRefreshList.ImageSize = new System.Drawing.Size(20, 20);
            this.btnRefreshList.Location = new System.Drawing.Point(5, 5);
            this.btnRefreshList.Margin = new System.Windows.Forms.Padding(5);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnRefreshList.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnRefreshList.OnHoverForeColor = System.Drawing.Color.White;
            this.btnRefreshList.OnHoverImage = null;
            this.btnRefreshList.OnPressedColor = System.Drawing.Color.Black;
            this.btnRefreshList.Radius = 3;
            this.btnRefreshList.Size = new System.Drawing.Size(200, 44);
            this.btnRefreshList.TabIndex = 3;
            this.btnRefreshList.Text = "Refresh";
            this.btnRefreshList.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            // 
            // btnViewSummary
            // 
            this.btnViewSummary.AnimationHoverSpeed = 0.07F;
            this.btnViewSummary.AnimationSpeed = 0.03F;
            this.btnViewSummary.BackColor = System.Drawing.Color.Transparent;
            this.btnViewSummary.BaseColor = System.Drawing.Color.White;
            this.btnViewSummary.BorderColor = System.Drawing.Color.DarkGray;
            this.btnViewSummary.BorderSize = 1;
            this.btnViewSummary.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnViewSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnViewSummary.FocusedColor = System.Drawing.Color.Empty;
            this.btnViewSummary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnViewSummary.ForeColor = System.Drawing.Color.Black;
            this.btnViewSummary.Image = null;
            this.btnViewSummary.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnViewSummary.ImageSize = new System.Drawing.Size(20, 20);
            this.btnViewSummary.Location = new System.Drawing.Point(445, 5);
            this.btnViewSummary.Margin = new System.Windows.Forms.Padding(5);
            this.btnViewSummary.Name = "btnViewSummary";
            this.btnViewSummary.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnViewSummary.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnViewSummary.OnHoverForeColor = System.Drawing.Color.White;
            this.btnViewSummary.OnHoverImage = null;
            this.btnViewSummary.OnPressedColor = System.Drawing.Color.Black;
            this.btnViewSummary.Radius = 3;
            this.btnViewSummary.Size = new System.Drawing.Size(202, 44);
            this.btnViewSummary.TabIndex = 2;
            this.btnViewSummary.Text = "View Summary";
            this.btnViewSummary.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnViewSummary.Click += new System.EventHandler(this.btnViewSummary_Click);
            // 
            // btnOpenSelected
            // 
            this.btnOpenSelected.AnimationHoverSpeed = 0.07F;
            this.btnOpenSelected.AnimationSpeed = 0.03F;
            this.btnOpenSelected.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenSelected.BaseColor = System.Drawing.Color.White;
            this.btnOpenSelected.BorderColor = System.Drawing.Color.DarkGray;
            this.btnOpenSelected.BorderSize = 1;
            this.btnOpenSelected.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOpenSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenSelected.FocusedColor = System.Drawing.Color.Empty;
            this.btnOpenSelected.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOpenSelected.ForeColor = System.Drawing.Color.Black;
            this.btnOpenSelected.Image = null;
            this.btnOpenSelected.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnOpenSelected.ImageSize = new System.Drawing.Size(20, 20);
            this.btnOpenSelected.Location = new System.Drawing.Point(225, 5);
            this.btnOpenSelected.Margin = new System.Windows.Forms.Padding(5);
            this.btnOpenSelected.Name = "btnOpenSelected";
            this.btnOpenSelected.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnOpenSelected.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnOpenSelected.OnHoverForeColor = System.Drawing.Color.White;
            this.btnOpenSelected.OnHoverImage = null;
            this.btnOpenSelected.OnPressedColor = System.Drawing.Color.Black;
            this.btnOpenSelected.Radius = 3;
            this.btnOpenSelected.Size = new System.Drawing.Size(200, 44);
            this.btnOpenSelected.TabIndex = 1;
            this.btnOpenSelected.Text = "Open Selected";
            this.btnOpenSelected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnOpenSelected.Click += new System.EventHandler(this.btnOpenSelected_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.btnRefreshList, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnViewSummary, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOpenSelected, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(652, 54);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.picDocument, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblDragInstructions, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(638, 227);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnAdd, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnBrowse, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 175);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(632, 49);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // picDocument
            // 
            this.picDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDocument.Image = global::FactoryManagementSoftware.Properties.Resources.icons8_pdf_100;
            this.picDocument.Location = new System.Drawing.Point(5, 5);
            this.picDocument.Margin = new System.Windows.Forms.Padding(5);
            this.picDocument.Name = "picDocument";
            this.picDocument.Size = new System.Drawing.Size(628, 132);
            this.picDocument.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDocument.TabIndex = 0;
            this.picDocument.TabStop = false;
            // 
            // POAttachmentForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(688, 643);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "POAttachmentForm";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "P/O Document Management";
            this.Load += new System.EventHandler(this.POAttachmentForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.POAttachmentForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.POAttachmentForm_DragEnter);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelDragDrop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainList)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDocument)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelDragDrop;
        private System.Windows.Forms.PictureBox picDocument;
        private Guna.UI.WinForms.GunaButton btnBrowse;
        private Guna.UI.WinForms.GunaButton btnAdd;
        private System.Windows.Forms.Label lblDragInstructions;
        private System.Windows.Forms.DataGridView dgvMainList;
        private System.Windows.Forms.Panel panelButtons;
        private Guna.UI.WinForms.GunaButton btnRefreshList;
        private Guna.UI.WinForms.GunaButton btnViewSummary;
        private Guna.UI.WinForms.GunaButton btnOpenSelected;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}