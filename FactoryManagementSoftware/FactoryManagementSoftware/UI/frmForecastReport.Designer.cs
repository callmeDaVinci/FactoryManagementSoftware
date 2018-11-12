namespace FactoryManagementSoftware.UI
{
    partial class frmForecastReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvForecastReport = new System.Windows.Forms.DataGridView();
            this.btnCheck = new System.Windows.Forms.Button();
            this.cmbCust = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbOrder = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.item_material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mc_ton = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_part_weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_runner_weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_one = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oSant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shotOne = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateRequired = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_two = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shotTwo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forecast_three = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForecastReport)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvForecastReport
            // 
            this.dgvForecastReport.AllowUserToAddRows = false;
            this.dgvForecastReport.AllowUserToDeleteRows = false;
            this.dgvForecastReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvForecastReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvForecastReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvForecastReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvForecastReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForecastReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item_material,
            this.item_code,
            this.item_name,
            this.item_color,
            this.mc_ton,
            this.item_part_weight,
            this.item_runner_weight,
            this.item_batch,
            this.stock_qty,
            this.forecast_one,
            this.outStock,
            this.oSant,
            this.shotOne,
            this.dateRequired,
            this.forecast_two,
            this.shotTwo,
            this.forecast_three});
            this.dgvForecastReport.EnableHeadersVisualStyles = false;
            this.dgvForecastReport.Location = new System.Drawing.Point(23, 130);
            this.dgvForecastReport.Name = "dgvForecastReport";
            this.dgvForecastReport.ReadOnly = true;
            this.dgvForecastReport.RowTemplate.Height = 24;
            this.dgvForecastReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvForecastReport.Size = new System.Drawing.Size(1534, 546);
            this.dgvForecastReport.TabIndex = 0;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(879, 73);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(118, 32);
            this.btnCheck.TabIndex = 12;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // cmbCust
            // 
            this.cmbCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCust.FormattingEnabled = true;
            this.cmbCust.Location = new System.Drawing.Point(23, 73);
            this.cmbCust.Name = "cmbCust";
            this.cmbCust.Size = new System.Drawing.Size(281, 31);
            this.cmbCust.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "Customer";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(1237, 72);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(320, 30);
            this.txtSearch.TabIndex = 13;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1079, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 23);
            this.label2.TabIndex = 14;
            this.label2.Text = "Item Name Search";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1439, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 32);
            this.button1.TabIndex = 15;
            this.button1.Text = "PDF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbSort
            // 
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Items.AddRange(new object[] {
            "Default",
            "Ready Stock",
            "Forecast One",
            "Forecast Two",
            "Forecast Three",
            "Shot One",
            "Shot Two"});
            this.cmbSort.Location = new System.Drawing.Point(502, 74);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(159, 31);
            this.cmbSort.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(498, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 23);
            this.label3.TabIndex = 16;
            this.label3.Text = "Sort";
            // 
            // cmbOrder
            // 
            this.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrder.FormattingEnabled = true;
            this.cmbOrder.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
            this.cmbOrder.Location = new System.Drawing.Point(683, 74);
            this.cmbOrder.Name = "cmbOrder";
            this.cmbOrder.Size = new System.Drawing.Size(159, 31);
            this.cmbOrder.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(679, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 23);
            this.label4.TabIndex = 18;
            this.label4.Text = "Order";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "All",
            "Non-Assembly",
            "Assembly"});
            this.cmbType.Location = new System.Drawing.Point(320, 74);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(159, 31);
            this.cmbType.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(316, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 23);
            this.label5.TabIndex = 20;
            this.label5.Text = "Type";
            // 
            // item_material
            // 
            this.item_material.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.item_material.HeaderText = "Material Type";
            this.item_material.Name = "item_material";
            this.item_material.ReadOnly = true;
            this.item_material.Width = 141;
            // 
            // item_code
            // 
            this.item_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.item_code.HeaderText = "Part Number";
            this.item_code.Name = "item_code";
            this.item_code.ReadOnly = true;
            this.item_code.Width = 137;
            // 
            // item_name
            // 
            this.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item_name.HeaderText = "Part Name";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            // 
            // item_color
            // 
            this.item_color.HeaderText = "Color";
            this.item_color.Name = "item_color";
            this.item_color.ReadOnly = true;
            // 
            // mc_ton
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.mc_ton.DefaultCellStyle = dataGridViewCellStyle2;
            this.mc_ton.HeaderText = "MC TON";
            this.mc_ton.Name = "mc_ton";
            this.mc_ton.ReadOnly = true;
            this.mc_ton.Width = 5;
            // 
            // item_part_weight
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.item_part_weight.DefaultCellStyle = dataGridViewCellStyle3;
            this.item_part_weight.HeaderText = "Part Weight(g)";
            this.item_part_weight.Name = "item_part_weight";
            this.item_part_weight.ReadOnly = true;
            // 
            // item_runner_weight
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightYellow;
            this.item_runner_weight.DefaultCellStyle = dataGridViewCellStyle4;
            this.item_runner_weight.HeaderText = "Runner Weight(g)";
            this.item_runner_weight.Name = "item_runner_weight";
            this.item_runner_weight.ReadOnly = true;
            // 
            // item_batch
            // 
            this.item_batch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.item_batch.HeaderText = "M Batch Code";
            this.item_batch.Name = "item_batch";
            this.item_batch.ReadOnly = true;
            this.item_batch.Width = 135;
            // 
            // stock_qty
            // 
            this.stock_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Pink;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stock_qty.DefaultCellStyle = dataGridViewCellStyle5;
            this.stock_qty.HeaderText = "Ready Stock";
            this.stock_qty.Name = "stock_qty";
            this.stock_qty.ReadOnly = true;
            this.stock_qty.Width = 5;
            // 
            // forecast_one
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightYellow;
            this.forecast_one.DefaultCellStyle = dataGridViewCellStyle6;
            this.forecast_one.HeaderText = "F/cast 1";
            this.forecast_one.Name = "forecast_one";
            this.forecast_one.ReadOnly = true;
            // 
            // outStock
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightYellow;
            this.outStock.DefaultCellStyle = dataGridViewCellStyle7;
            this.outStock.HeaderText = "Out";
            this.outStock.Name = "outStock";
            this.outStock.ReadOnly = true;
            // 
            // oSant
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.LightYellow;
            this.oSant.DefaultCellStyle = dataGridViewCellStyle8;
            this.oSant.HeaderText = "O/sant";
            this.oSant.Name = "oSant";
            this.oSant.ReadOnly = true;
            // 
            // shotOne
            // 
            this.shotOne.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.LightYellow;
            this.shotOne.DefaultCellStyle = dataGridViewCellStyle9;
            this.shotOne.HeaderText = "SHOT 1";
            this.shotOne.Name = "shotOne";
            this.shotOne.ReadOnly = true;
            this.shotOne.Width = 88;
            // 
            // dateRequired
            // 
            this.dateRequired.HeaderText = "Date Required";
            this.dateRequired.Name = "dateRequired";
            this.dateRequired.ReadOnly = true;
            // 
            // forecast_two
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LightCyan;
            this.forecast_two.DefaultCellStyle = dataGridViewCellStyle10;
            this.forecast_two.HeaderText = "F/cast 2";
            this.forecast_two.Name = "forecast_two";
            this.forecast_two.ReadOnly = true;
            // 
            // shotTwo
            // 
            this.shotTwo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.LightCyan;
            this.shotTwo.DefaultCellStyle = dataGridViewCellStyle11;
            this.shotTwo.HeaderText = "SHOT 2";
            this.shotTwo.Name = "shotTwo";
            this.shotTwo.ReadOnly = true;
            this.shotTwo.Width = 88;
            // 
            // forecast_three
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.PeachPuff;
            this.forecast_three.DefaultCellStyle = dataGridViewCellStyle12;
            this.forecast_three.HeaderText = "F/cast 3";
            this.forecast_three.Name = "forecast_three";
            this.forecast_three.ReadOnly = true;
            // 
            // frmForecastReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 703);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbOrder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.cmbCust);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvForecastReport);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmForecastReport";
            this.Text = "frmForecastReport";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmForecastReport_FormClosed);
            this.Load += new System.EventHandler(this.frmForecastReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvForecastReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvForecastReport;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ComboBox cmbCust;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbOrder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_material;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_color;
        private System.Windows.Forms.DataGridViewTextBoxColumn mc_ton;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_part_weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_runner_weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_one;
        private System.Windows.Forms.DataGridViewTextBoxColumn outStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn oSant;
        private System.Windows.Forms.DataGridViewTextBoxColumn shotOne;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateRequired;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_two;
        private System.Windows.Forms.DataGridViewTextBoxColumn shotTwo;
        private System.Windows.Forms.DataGridViewTextBoxColumn forecast_three;
    }
}