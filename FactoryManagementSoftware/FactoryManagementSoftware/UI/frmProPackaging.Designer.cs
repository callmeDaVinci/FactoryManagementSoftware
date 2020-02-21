﻿namespace FactoryManagementSoftware.UI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.label4 = new System.Windows.Forms.Label();
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPackaging.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPackaging.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPackaging.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPackaging.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPackaging.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPackaging.Location = new System.Drawing.Point(13, 67);
            this.dgvPackaging.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvPackaging.MultiSelect = false;
            this.dgvPackaging.Name = "dgvPackaging";
            this.dgvPackaging.ReadOnly = true;
            this.dgvPackaging.RowHeadersVisible = false;
            this.dgvPackaging.RowTemplate.Height = 40;
            this.dgvPackaging.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPackaging.Size = new System.Drawing.Size(848, 329);
            this.dgvPackaging.TabIndex = 154;
            this.dgvPackaging.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPackaging_CellMouseDown);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.White;
            this.btnAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.ForeColor = System.Drawing.Color.Black;
            this.btnAddItem.Location = new System.Drawing.Point(736, 24);
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
            this.btnDone.Location = new System.Drawing.Point(736, 398);
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
            this.button1.Location = new System.Drawing.Point(604, 398);
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
            this.cmbPackingCode.Location = new System.Drawing.Point(329, 38);
            this.cmbPackingCode.Name = "cmbPackingCode";
            this.cmbPackingCode.Size = new System.Drawing.Size(187, 25);
            this.cmbPackingCode.TabIndex = 250;
            // 
            // lblPcs
            // 
            this.lblPcs.AutoSize = true;
            this.lblPcs.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPcs.Location = new System.Drawing.Point(12, 16);
            this.lblPcs.Name = "lblPcs";
            this.lblPcs.Size = new System.Drawing.Size(33, 19);
            this.lblPcs.TabIndex = 249;
            this.lblPcs.Text = "PCS";
            // 
            // cmbPackingName
            // 
            this.cmbPackingName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPackingName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPackingName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPackingName.FormattingEnabled = true;
            this.cmbPackingName.Location = new System.Drawing.Point(96, 38);
            this.cmbPackingName.Name = "cmbPackingName";
            this.cmbPackingName.Size = new System.Drawing.Size(227, 25);
            this.cmbPackingName.TabIndex = 248;
            this.cmbPackingName.SelectedIndexChanged += new System.EventHandler(this.cmbPackingName_SelectedIndexChanged);
            // 
            // txtPackagingMax
            // 
            this.txtPackagingMax.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackagingMax.Location = new System.Drawing.Point(13, 38);
            this.txtPackagingMax.Name = "txtPackagingMax";
            this.txtPackagingMax.Size = new System.Drawing.Size(57, 25);
            this.txtPackagingMax.TabIndex = 247;
            this.txtPackagingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPackagingMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPackingQty_KeyPress);
            // 
            // txtTotalBox
            // 
            this.txtTotalBox.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBox.Location = new System.Drawing.Point(542, 38);
            this.txtTotalBox.Name = "txtTotalBox";
            this.txtTotalBox.Size = new System.Drawing.Size(89, 25);
            this.txtTotalBox.TabIndex = 251;
            this.txtTotalBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalBox_KeyPress);
            // 
            // lblTotalBox
            // 
            this.lblTotalBox.AutoSize = true;
            this.lblTotalBox.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBox.Location = new System.Drawing.Point(538, 16);
            this.lblTotalBox.Name = "lblTotalBox";
            this.lblTotalBox.Size = new System.Drawing.Size(78, 19);
            this.lblTotalBox.TabIndex = 252;
            this.lblTotalBox.Text = "TOTAL BOX";
            // 
            // lblBoxName
            // 
            this.lblBoxName.AutoSize = true;
            this.lblBoxName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoxName.Location = new System.Drawing.Point(92, 16);
            this.lblBoxName.Name = "lblBoxName";
            this.lblBoxName.Size = new System.Drawing.Size(79, 19);
            this.lblBoxName.TabIndex = 253;
            this.lblBoxName.Text = "BOX NAME";
            // 
            // lblBoxCode
            // 
            this.lblBoxCode.AutoSize = true;
            this.lblBoxCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoxCode.Location = new System.Drawing.Point(329, 16);
            this.lblBoxCode.Name = "lblBoxCode";
            this.lblBoxCode.Size = new System.Drawing.Size(77, 19);
            this.lblBoxCode.TabIndex = 254;
            this.lblBoxCode.Text = "BOX CODE";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(76, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 19);
            this.label4.TabIndex = 255;
            this.label4.Text = "/";
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
            this.ClientSize = new System.Drawing.Size(874, 451);
            this.Controls.Add(this.label4);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}