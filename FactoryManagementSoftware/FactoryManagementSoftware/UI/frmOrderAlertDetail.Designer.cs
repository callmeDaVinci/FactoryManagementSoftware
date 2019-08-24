namespace FactoryManagementSoftware.UI
{
    partial class frmOrderAlertDetail
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
            this.cmbForecast = new System.Windows.Forms.ComboBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvMaterialUsedForecast = new System.Windows.Forms.DataGridView();
            this.lblDGV = new System.Windows.Forms.Label();
            this.dgvOrderAlert = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialUsedForecast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderAlert)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbForecast
            // 
            this.cmbForecast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForecast.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbForecast.FormattingEnabled = true;
            this.cmbForecast.Location = new System.Drawing.Point(25, 41);
            this.cmbForecast.Name = "cmbForecast";
            this.cmbForecast.Size = new System.Drawing.Size(191, 31);
            this.cmbForecast.TabIndex = 88;
            this.cmbForecast.SelectedIndexChanged += new System.EventHandler(this.cmbForecast_SelectedIndexChanged);
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(221, 31);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(122, 44);
            this.btnCheck.TabIndex = 86;
            this.btnCheck.Text = "CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 19);
            this.label4.TabIndex = 90;
            this.label4.Text = "FORECAST MONTH";
            // 
            // dgvMaterialUsedForecast
            // 
            this.dgvMaterialUsedForecast.AllowUserToAddRows = false;
            this.dgvMaterialUsedForecast.AllowUserToDeleteRows = false;
            this.dgvMaterialUsedForecast.AllowUserToResizeRows = false;
            this.dgvMaterialUsedForecast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMaterialUsedForecast.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaterialUsedForecast.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterialUsedForecast.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMaterialUsedForecast.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialUsedForecast.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvMaterialUsedForecast.GridColor = System.Drawing.SystemColors.Control;
            this.dgvMaterialUsedForecast.Location = new System.Drawing.Point(25, 310);
            this.dgvMaterialUsedForecast.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMaterialUsedForecast.Name = "dgvMaterialUsedForecast";
            this.dgvMaterialUsedForecast.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvMaterialUsedForecast.RowHeadersVisible = false;
            this.dgvMaterialUsedForecast.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMaterialUsedForecast.RowTemplate.Height = 40;
            this.dgvMaterialUsedForecast.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMaterialUsedForecast.Size = new System.Drawing.Size(1533, 520);
            this.dgvMaterialUsedForecast.TabIndex = 103;
            // 
            // lblDGV
            // 
            this.lblDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDGV.AutoSize = true;
            this.lblDGV.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDGV.Location = new System.Drawing.Point(21, 289);
            this.lblDGV.Name = "lblDGV";
            this.lblDGV.Size = new System.Drawing.Size(179, 19);
            this.lblDGV.TabIndex = 102;
            this.lblDGV.Text = "MATERIAL USED FORECAST";
            // 
            // dgvOrderAlert
            // 
            this.dgvOrderAlert.AllowUserToAddRows = false;
            this.dgvOrderAlert.AllowUserToDeleteRows = false;
            this.dgvOrderAlert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrderAlert.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderAlert.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrderAlert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderAlert.GridColor = System.Drawing.SystemColors.Control;
            this.dgvOrderAlert.Location = new System.Drawing.Point(25, 104);
            this.dgvOrderAlert.Margin = new System.Windows.Forms.Padding(2);
            this.dgvOrderAlert.Name = "dgvOrderAlert";
            this.dgvOrderAlert.ReadOnly = true;
            this.dgvOrderAlert.RowHeadersVisible = false;
            this.dgvOrderAlert.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvOrderAlert.RowTemplate.Height = 40;
            this.dgvOrderAlert.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOrderAlert.Size = new System.Drawing.Size(1533, 167);
            this.dgvOrderAlert.TabIndex = 104;
            this.dgvOrderAlert.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderAlert_CellContentClick_1);
            this.dgvOrderAlert.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvOrderAlert_CellFormatting);
            this.dgvOrderAlert.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrderAlert_CellMouseDown);
            this.dgvOrderAlert.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderAlert_CellMouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 19);
            this.label1.TabIndex = 105;
            this.label1.Text = "ORDER ALERT";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1133, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(425, 19);
            this.label2.TabIndex = 106;
            this.label2.Text = "RIGHT CLICK THE CELL FOR MONTHLY MATERIAL USED QUANTITY";
            // 
            // frmOrderAlertDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvOrderAlert);
            this.Controls.Add(this.dgvMaterialUsedForecast);
            this.Controls.Add(this.lblDGV);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbForecast);
            this.Controls.Add(this.btnCheck);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmOrderAlertDetail";
            this.Text = "frmOrderAlertDetail";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmOrderAlertDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialUsedForecast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderAlert)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbForecast;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvMaterialUsedForecast;
        private System.Windows.Forms.Label lblDGV;
        private System.Windows.Forms.DataGridView dgvOrderAlert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}