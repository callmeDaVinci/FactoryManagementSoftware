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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbForecast = new System.Windows.Forms.ComboBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvMaterialUsedForecast = new System.Windows.Forms.DataGridView();
            this.lblDGV = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialUsedForecast)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbForecast
            // 
            this.cmbForecast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForecast.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbForecast.FormattingEnabled = true;
            this.cmbForecast.Location = new System.Drawing.Point(25, 46);
            this.cmbForecast.Name = "cmbForecast";
            this.cmbForecast.Size = new System.Drawing.Size(302, 39);
            this.cmbForecast.TabIndex = 88;
            this.cmbForecast.SelectedIndexChanged += new System.EventHandler(this.cmbForecast_SelectedIndexChanged);
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(355, 33);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(122, 52);
            this.btnCheck.TabIndex = 86;
            this.btnCheck.Text = "CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 23);
            this.label4.TabIndex = 90;
            this.label4.Text = "FORECAST";
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterialUsedForecast.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMaterialUsedForecast.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialUsedForecast.GridColor = System.Drawing.SystemColors.Control;
            this.dgvMaterialUsedForecast.Location = new System.Drawing.Point(25, 137);
            this.dgvMaterialUsedForecast.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMaterialUsedForecast.Name = "dgvMaterialUsedForecast";
            this.dgvMaterialUsedForecast.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvMaterialUsedForecast.RowHeadersVisible = false;
            this.dgvMaterialUsedForecast.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMaterialUsedForecast.RowTemplate.Height = 40;
            this.dgvMaterialUsedForecast.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMaterialUsedForecast.Size = new System.Drawing.Size(1533, 691);
            this.dgvMaterialUsedForecast.TabIndex = 103;
            // 
            // lblDGV
            // 
            this.lblDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDGV.AutoSize = true;
            this.lblDGV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDGV.Location = new System.Drawing.Point(21, 112);
            this.lblDGV.Name = "lblDGV";
            this.lblDGV.Size = new System.Drawing.Size(219, 23);
            this.lblDGV.TabIndex = 102;
            this.lblDGV.Text = "MATERIAL USED FORECAST";
            // 
            // frmOrderAlertDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.dgvMaterialUsedForecast);
            this.Controls.Add(this.lblDGV);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbForecast);
            this.Controls.Add(this.btnCheck);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmOrderAlertDetail";
            this.Text = "frmOrderAlertDetail";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialUsedForecast)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbForecast;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvMaterialUsedForecast;
        private System.Windows.Forms.Label lblDGV;
    }
}