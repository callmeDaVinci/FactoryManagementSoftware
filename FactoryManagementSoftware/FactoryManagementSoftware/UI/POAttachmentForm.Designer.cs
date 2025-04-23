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
            this.panelDragDrop = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.picDocument = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnBrowse = new Guna.UI.WinForms.GunaButton();
            this.btnViewSummary = new Guna.UI.WinForms.GunaButton();
            this.lblDragInstructions = new System.Windows.Forms.Label();
            this.panelDragDrop.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDocument)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDragDrop
            // 
            this.panelDragDrop.AllowDrop = true;
            this.panelDragDrop.BackColor = System.Drawing.Color.Transparent;
            this.panelDragDrop.Controls.Add(this.tableLayoutPanel3);
            this.panelDragDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDragDrop.Location = new System.Drawing.Point(15, 15);
            this.panelDragDrop.Margin = new System.Windows.Forms.Padding(5);
            this.panelDragDrop.Name = "panelDragDrop";
            this.panelDragDrop.Padding = new System.Windows.Forms.Padding(5);
            this.panelDragDrop.Size = new System.Drawing.Size(502, 246);
            this.panelDragDrop.TabIndex = 0;
            this.panelDragDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.POAttachmentForm_DragDrop);
            this.panelDragDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.POAttachmentForm_DragEnter);
            this.panelDragDrop.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDragDrop_Paint);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(492, 236);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // picDocument
            // 
            this.picDocument.BackColor = System.Drawing.Color.Transparent;
            this.picDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDocument.Image = global::FactoryManagementSoftware.Properties.Resources.icons8_pdf_1001;
            this.picDocument.Location = new System.Drawing.Point(5, 5);
            this.picDocument.Margin = new System.Windows.Forms.Padding(5);
            this.picDocument.Name = "picDocument";
            this.picDocument.Size = new System.Drawing.Size(482, 141);
            this.picDocument.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picDocument.TabIndex = 0;
            this.picDocument.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnBrowse, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnViewSummary, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 184);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(486, 49);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.AnimationHoverSpeed = 0.07F;
            this.btnBrowse.AnimationSpeed = 0.03F;
            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.btnBrowse.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowse.BorderSize = 1;
            this.btnBrowse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnBrowse.FocusedColor = System.Drawing.Color.Empty;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Image = null;
            this.btnBrowse.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnBrowse.ImageSize = new System.Drawing.Size(20, 20);
            this.btnBrowse.Location = new System.Drawing.Point(88, 5);
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
            // btnViewSummary
            // 
            this.btnViewSummary.AnimationHoverSpeed = 0.07F;
            this.btnViewSummary.AnimationSpeed = 0.03F;
            this.btnViewSummary.BackColor = System.Drawing.Color.Transparent;
            this.btnViewSummary.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(48)))), ((int)(((byte)(72)))));
            this.btnViewSummary.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(48)))), ((int)(((byte)(72)))));
            this.btnViewSummary.BorderSize = 1;
            this.btnViewSummary.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnViewSummary.FocusedColor = System.Drawing.Color.Empty;
            this.btnViewSummary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnViewSummary.ForeColor = System.Drawing.Color.White;
            this.btnViewSummary.Image = null;
            this.btnViewSummary.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnViewSummary.ImageSize = new System.Drawing.Size(20, 20);
            this.btnViewSummary.Location = new System.Drawing.Point(248, 5);
            this.btnViewSummary.Margin = new System.Windows.Forms.Padding(5);
            this.btnViewSummary.Name = "btnViewSummary";
            this.btnViewSummary.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btnViewSummary.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btnViewSummary.OnHoverForeColor = System.Drawing.Color.White;
            this.btnViewSummary.OnHoverImage = null;
            this.btnViewSummary.OnPressedColor = System.Drawing.Color.Black;
            this.btnViewSummary.Radius = 3;
            this.btnViewSummary.Size = new System.Drawing.Size(202, 39);
            this.btnViewSummary.TabIndex = 2;
            this.btnViewSummary.Text = "View Summary";
            this.btnViewSummary.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnViewSummary.Click += new System.EventHandler(this.btnViewSummary_Click);
            // 
            // lblDragInstructions
            // 
            this.lblDragInstructions.BackColor = System.Drawing.Color.Transparent;
            this.lblDragInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDragInstructions.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDragInstructions.ForeColor = System.Drawing.Color.White;
            this.lblDragInstructions.Location = new System.Drawing.Point(2, 153);
            this.lblDragInstructions.Margin = new System.Windows.Forms.Padding(2);
            this.lblDragInstructions.Name = "lblDragInstructions";
            this.lblDragInstructions.Size = new System.Drawing.Size(488, 26);
            this.lblDragInstructions.TabIndex = 1;
            this.lblDragInstructions.Text = "Drag and drop your PDF file here, or click below to browse.";
            this.lblDragInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // POAttachmentForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(24)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(532, 276);
            this.Controls.Add(this.panelDragDrop);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "POAttachmentForm";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "P/O Document Management";
            this.Load += new System.EventHandler(this.POAttachmentForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.POAttachmentForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.POAttachmentForm_DragEnter);
            this.panelDragDrop.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDocument)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelDragDrop;
        private System.Windows.Forms.PictureBox picDocument;
        private Guna.UI.WinForms.GunaButton btnBrowse;
        private System.Windows.Forms.Label lblDragInstructions;
        private Guna.UI.WinForms.GunaButton btnViewSummary;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}