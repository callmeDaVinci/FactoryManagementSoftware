namespace FactoryManagementSoftware.UI
{
    partial class frmForecast
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvForecast = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCust = new System.Windows.Forms.ComboBox();
            this.cmbForecast1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbForecast2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbForecast3 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.cmbForecast4 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_one = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_two = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_three = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_four = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_updtd_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_updtd_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForecast)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvForecast
            // 
            this.dgvForecast.AllowUserToAddRows = false;
            this.dgvForecast.AllowUserToDeleteRows = false;
            this.dgvForecast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvForecast.BackgroundColor = System.Drawing.Color.White;
            this.dgvForecast.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForecast.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NO,
            this.item_code,
            this.item_name,
            this.forecast_one,
            this.forecast_two,
            this.forecast_three,
            this.forecast_four,
            this.forecast_updtd_date,
            this.forecast_updtd_by});
            this.dgvForecast.GridColor = System.Drawing.SystemColors.Control;
            this.dgvForecast.Location = new System.Drawing.Point(27, 199);
            this.dgvForecast.Name = "dgvForecast";
            this.dgvForecast.RowHeadersVisible = false;
            this.dgvForecast.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvForecast.RowTemplate.Height = 40;
            this.dgvForecast.Size = new System.Drawing.Size(1531, 492);
            this.dgvForecast.TabIndex = 0;
            this.dgvForecast.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvForecast_CellEndEdit);
            this.dgvForecast.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvForecast_EditingControlShowing);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "CUSTOMER";
            // 
            // cmbCust
            // 
            this.cmbCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCust.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCust.FormattingEnabled = true;
            this.cmbCust.Location = new System.Drawing.Point(27, 54);
            this.cmbCust.Name = "cmbCust";
            this.cmbCust.Size = new System.Drawing.Size(346, 39);
            this.cmbCust.TabIndex = 2;
            // 
            // cmbForecast1
            // 
            this.cmbForecast1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForecast1.Enabled = false;
            this.cmbForecast1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbForecast1.FormattingEnabled = true;
            this.cmbForecast1.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cmbForecast1.Location = new System.Drawing.Point(27, 141);
            this.cmbForecast1.Name = "cmbForecast1";
            this.cmbForecast1.Size = new System.Drawing.Size(218, 31);
            this.cmbForecast1.TabIndex = 4;
            this.cmbForecast1.SelectedIndexChanged += new System.EventHandler(this.cmbForecast1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "FORECAST 1";
            // 
            // cmbForecast2
            // 
            this.cmbForecast2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForecast2.Enabled = false;
            this.cmbForecast2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbForecast2.FormattingEnabled = true;
            this.cmbForecast2.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cmbForecast2.Location = new System.Drawing.Point(260, 141);
            this.cmbForecast2.Name = "cmbForecast2";
            this.cmbForecast2.Size = new System.Drawing.Size(219, 31);
            this.cmbForecast2.TabIndex = 6;
            this.cmbForecast2.SelectedIndexChanged += new System.EventHandler(this.cmbForecast2_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(256, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "FORECAST 2";
            // 
            // cmbForecast3
            // 
            this.cmbForecast3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForecast3.Enabled = false;
            this.cmbForecast3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbForecast3.FormattingEnabled = true;
            this.cmbForecast3.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cmbForecast3.Location = new System.Drawing.Point(495, 141);
            this.cmbForecast3.Name = "cmbForecast3";
            this.cmbForecast3.Size = new System.Drawing.Size(219, 31);
            this.cmbForecast3.TabIndex = 8;
            this.cmbForecast3.SelectedIndexChanged += new System.EventHandler(this.cmbForecast3_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(491, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "FORECAST 3";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(1254, 145);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(304, 38);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1250, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "SEARCH";
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(397, 41);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(122, 52);
            this.btnCheck.TabIndex = 68;
            this.btnCheck.Text = "CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnReset.Location = new System.Drawing.Point(1438, 42);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 50);
            this.btnReset.TabIndex = 89;
            this.btnReset.Text = "REPORT";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // cmbForecast4
            // 
            this.cmbForecast4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForecast4.Enabled = false;
            this.cmbForecast4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbForecast4.FormattingEnabled = true;
            this.cmbForecast4.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cmbForecast4.Location = new System.Drawing.Point(732, 141);
            this.cmbForecast4.Name = "cmbForecast4";
            this.cmbForecast4.Size = new System.Drawing.Size(219, 31);
            this.cmbForecast4.TabIndex = 91;
            this.cmbForecast4.SelectedIndexChanged += new System.EventHandler(this.cmbForecast4_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(730, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 19);
            this.label6.TabIndex = 90;
            this.label6.Text = "FORECAST 4";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // NO
            // 
            this.NO.HeaderText = "NO";
            this.NO.Name = "NO";
            // 
            // item_code
            // 
            this.item_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_code.HeaderText = "CODE";
            this.item_code.Name = "item_code";
            this.item_code.ReadOnly = true;
            // 
            // item_name
            // 
            this.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_name.HeaderText = "NAME";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            // 
            // forecast_one
            // 
            this.forecast_one.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.LightYellow;
            this.forecast_one.DefaultCellStyle = dataGridViewCellStyle9;
            this.forecast_one.HeaderText = "FORECAST 1";
            this.forecast_one.Name = "forecast_one";
            this.forecast_one.Width = 133;
            // 
            // forecast_two
            // 
            this.forecast_two.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LightCyan;
            this.forecast_two.DefaultCellStyle = dataGridViewCellStyle10;
            this.forecast_two.HeaderText = "FORECAST 2";
            this.forecast_two.Name = "forecast_two";
            this.forecast_two.Width = 133;
            // 
            // forecast_three
            // 
            this.forecast_three.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.LightYellow;
            this.forecast_three.DefaultCellStyle = dataGridViewCellStyle11;
            this.forecast_three.HeaderText = "FORECAST 3";
            this.forecast_three.Name = "forecast_three";
            this.forecast_three.Width = 133;
            // 
            // forecast_four
            // 
            this.forecast_four.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.LightCyan;
            this.forecast_four.DefaultCellStyle = dataGridViewCellStyle12;
            this.forecast_four.HeaderText = "FORECAST 4";
            this.forecast_four.Name = "forecast_four";
            this.forecast_four.Width = 133;
            // 
            // forecast_updtd_date
            // 
            this.forecast_updtd_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.forecast_updtd_date.HeaderText = "UPDATED DATE";
            this.forecast_updtd_date.Name = "forecast_updtd_date";
            this.forecast_updtd_date.ReadOnly = true;
            this.forecast_updtd_date.Width = 158;
            // 
            // forecast_updtd_by
            // 
            this.forecast_updtd_by.HeaderText = "BY";
            this.forecast_updtd_by.Name = "forecast_updtd_by";
            this.forecast_updtd_by.ReadOnly = true;
            // 
            // frmForecast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.cmbForecast4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbForecast3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbForecast2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbForecast1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbCust);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvForecast);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmForecast";
            this.Text = "frmForecast";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmForecast_FormClosed);
            this.Load += new System.EventHandler(this.frmForecast_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvForecast)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvForecast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCust;
        private System.Windows.Forms.ComboBox cmbForecast1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbForecast2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbForecast3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox cmbForecast4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_one;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_two;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_three;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_four;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_updtd_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_updtd_by;
    }
}