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
            this.dgvForecast = new System.Windows.Forms.DataGridView();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_one = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_two = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_three = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_updtd_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_updtd_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCust = new System.Windows.Forms.ComboBox();
            this.cmbForecast1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbForecast2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbForecast3 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
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
            this.dgvForecast.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForecast.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item_code,
            this.item_name,
            this.forecast_one,
            this.forecast_two,
            this.forecast_three,
            this.forecast_updtd_date,
            this.forecast_updtd_by});
            this.dgvForecast.Location = new System.Drawing.Point(27, 199);
            this.dgvForecast.Name = "dgvForecast";
            this.dgvForecast.RowTemplate.Height = 24;
            this.dgvForecast.Size = new System.Drawing.Size(1531, 492);
            this.dgvForecast.TabIndex = 0;
            this.dgvForecast.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvForecast_CellEndEdit);
            this.dgvForecast.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvForecast_EditingControlShowing);
            // 
            // item_code
            // 
            this.item_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_code.HeaderText = "Item Code";
            this.item_code.Name = "item_code";
            this.item_code.ReadOnly = true;
            // 
            // item_name
            // 
            this.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_name.HeaderText = "Item Name";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            // 
            // forecast_one
            // 
            this.forecast_one.HeaderText = "Forecast 1";
            this.forecast_one.Name = "forecast_one";
            // 
            // forecast_two
            // 
            this.forecast_two.HeaderText = "Forecast 2";
            this.forecast_two.Name = "forecast_two";
            // 
            // forecast_three
            // 
            this.forecast_three.HeaderText = "Forecast 3";
            this.forecast_three.Name = "forecast_three";
            // 
            // forecast_updtd_date
            // 
            this.forecast_updtd_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.forecast_updtd_date.HeaderText = "Updated Date";
            this.forecast_updtd_date.Name = "forecast_updtd_date";
            this.forecast_updtd_date.ReadOnly = true;
            this.forecast_updtd_date.Width = 146;
            // 
            // forecast_updtd_by
            // 
            this.forecast_updtd_by.HeaderText = "By";
            this.forecast_updtd_by.Name = "forecast_updtd_by";
            this.forecast_updtd_by.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Customer";
            // 
            // cmbCust
            // 
            this.cmbCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCust.FormattingEnabled = true;
            this.cmbCust.Location = new System.Drawing.Point(27, 85);
            this.cmbCust.Name = "cmbCust";
            this.cmbCust.Size = new System.Drawing.Size(281, 31);
            this.cmbCust.TabIndex = 2;
            this.cmbCust.SelectedIndexChanged += new System.EventHandler(this.cmbCust_SelectedIndexChanged);
            // 
            // cmbForecast1
            // 
            this.cmbForecast1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cmbForecast1.Location = new System.Drawing.Point(668, 84);
            this.cmbForecast1.Name = "cmbForecast1";
            this.cmbForecast1.Size = new System.Drawing.Size(281, 31);
            this.cmbForecast1.TabIndex = 4;
            this.cmbForecast1.SelectedIndexChanged += new System.EventHandler(this.cmbForecast1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(664, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Forecast 1";
            // 
            // cmbForecast2
            // 
            this.cmbForecast2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cmbForecast2.Location = new System.Drawing.Point(973, 84);
            this.cmbForecast2.Name = "cmbForecast2";
            this.cmbForecast2.Size = new System.Drawing.Size(281, 31);
            this.cmbForecast2.TabIndex = 6;
            this.cmbForecast2.SelectedIndexChanged += new System.EventHandler(this.cmbForecast2_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(969, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Forecast 2";
            // 
            // cmbForecast3
            // 
            this.cmbForecast3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cmbForecast3.Location = new System.Drawing.Point(1275, 84);
            this.cmbForecast3.Name = "cmbForecast3";
            this.cmbForecast3.Size = new System.Drawing.Size(281, 31);
            this.cmbForecast3.TabIndex = 8;
            this.cmbForecast3.SelectedIndexChanged += new System.EventHandler(this.cmbForecast3_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1271, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Forecast 3";
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(333, 84);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(118, 32);
            this.btnCheck.TabIndex = 9;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(27, 163);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(519, 30);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "Search";
            // 
            // frmForecast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnCheck);
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
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_one;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_two;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_three;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_updtd_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_updtd_by;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label5;
    }
}