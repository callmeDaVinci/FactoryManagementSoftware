namespace FactoryManagementSoftware.UI
{
    partial class FinexWill
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
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDraftWill = new System.Windows.Forms.Button();
            this.btnPDF = new System.Windows.Forms.Button();
            this.cbDraftWaterMark = new System.Windows.Forms.CheckBox();
            this.dgvWillDraft = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDraftVersion = new System.Windows.Forms.Label();
            this.tlpBase.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWillDraft)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpBase
            // 
            this.tlpBase.ColumnCount = 1;
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpBase.Controls.Add(this.dgvWillDraft, 0, 1);
            this.tlpBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBase.Location = new System.Drawing.Point(30, 30);
            this.tlpBase.Margin = new System.Windows.Forms.Padding(30);
            this.tlpBase.Name = "tlpBase";
            this.tlpBase.RowCount = 2;
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBase.Size = new System.Drawing.Size(1288, 661);
            this.tlpBase.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 226F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 699F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 168F));
            this.tableLayoutPanel1.Controls.Add(this.btnDraftWill, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPDF, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbDraftWaterMark, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1288, 81);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // btnDraftWill
            // 
            this.btnDraftWill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDraftWill.BackColor = System.Drawing.Color.White;
            this.btnDraftWill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDraftWill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDraftWill.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDraftWill.ForeColor = System.Drawing.Color.Black;
            this.btnDraftWill.Location = new System.Drawing.Point(0, 44);
            this.btnDraftWill.Margin = new System.Windows.Forms.Padding(0, 1, 0, 5);
            this.btnDraftWill.Name = "btnDraftWill";
            this.btnDraftWill.Size = new System.Drawing.Size(91, 32);
            this.btnDraftWill.TabIndex = 187;
            this.btnDraftWill.Text = "REDRAFT";
            this.btnDraftWill.UseVisualStyleBackColor = false;
            this.btnDraftWill.Click += new System.EventHandler(this.btnDraftWill_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(118)))), ((int)(((byte)(117)))));
            this.btnPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPDF.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPDF.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPDF.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnPDF.Location = new System.Drawing.Point(1124, 44);
            this.btnPDF.Margin = new System.Windows.Forms.Padding(0, 1, 0, 5);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(164, 32);
            this.btnPDF.TabIndex = 175;
            this.btnPDF.Text = "PDF";
            this.btnPDF.UseVisualStyleBackColor = false;
            this.btnPDF.Click += new System.EventHandler(this.btnWill_Click);
            // 
            // cbDraftWaterMark
            // 
            this.cbDraftWaterMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDraftWaterMark.AutoSize = true;
            this.cbDraftWaterMark.Checked = true;
            this.cbDraftWaterMark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDraftWaterMark.Location = new System.Drawing.Point(1045, 55);
            this.cbDraftWaterMark.Name = "cbDraftWaterMark";
            this.cbDraftWaterMark.Size = new System.Drawing.Size(72, 23);
            this.cbDraftWaterMark.TabIndex = 188;
            this.cbDraftWaterMark.Text = "DRAFT";
            this.cbDraftWaterMark.UseVisualStyleBackColor = true;
            // 
            // dgvWillDraft
            // 
            this.dgvWillDraft.AllowDrop = true;
            this.dgvWillDraft.AllowUserToAddRows = false;
            this.dgvWillDraft.AllowUserToDeleteRows = false;
            this.dgvWillDraft.AllowUserToOrderColumns = true;
            this.dgvWillDraft.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvWillDraft.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvWillDraft.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvWillDraft.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWillDraft.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWillDraft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWillDraft.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWillDraft.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWillDraft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWillDraft.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvWillDraft.Location = new System.Drawing.Point(0, 82);
            this.dgvWillDraft.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.dgvWillDraft.Name = "dgvWillDraft";
            this.dgvWillDraft.RowHeadersVisible = false;
            this.dgvWillDraft.RowTemplate.Height = 80;
            this.dgvWillDraft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWillDraft.Size = new System.Drawing.Size(1288, 579);
            this.dgvWillDraft.TabIndex = 156;
            this.dgvWillDraft.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWillDraft_CellClick);
            this.dgvWillDraft.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWillDraft_CellEndEdit);
            this.dgvWillDraft.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvWillDraft_CellFormatting);
            this.dgvWillDraft.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvWillDraft_DragDrop);
            this.dgvWillDraft.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvWillDraft_DragOver);
            this.dgvWillDraft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvWillDraft_MouseDown);
            this.dgvWillDraft.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvWillDraft_MouseMove);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tlpBase, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1348, 721);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblDraftVersion, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(114, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(220, 75);
            this.tableLayoutPanel3.TabIndex = 189;
            // 
            // lblDraftVersion
            // 
            this.lblDraftVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDraftVersion.AutoSize = true;
            this.lblDraftVersion.Location = new System.Drawing.Point(0, 18);
            this.lblDraftVersion.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblDraftVersion.Name = "lblDraftVersion";
            this.lblDraftVersion.Size = new System.Drawing.Size(89, 19);
            this.lblDraftVersion.TabIndex = 0;
            this.lblDraftVersion.Text = "Draft Version";
            // 
            // FinexWill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FinexWill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WILL";
            this.Load += new System.EventHandler(this.FinexWill_Load);
            this.tlpBase.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWillDraft)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpBase;
        private System.Windows.Forms.Button btnPDF;
        private System.Windows.Forms.DataGridView dgvWillDraft;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnDraftWill;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox cbDraftWaterMark;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblDraftVersion;
    }
}