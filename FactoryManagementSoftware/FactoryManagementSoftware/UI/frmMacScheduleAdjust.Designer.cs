namespace FactoryManagementSoftware.UI
{
    partial class frmMacScheduleAdjust
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
            this.lblMachineSchedule = new System.Windows.Forms.Label();
            this.dgvMac = new System.Windows.Forms.DataGridView();
            this.cmbMacID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEstimateEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.cbIncludeSunday = new System.Windows.Forms.CheckBox();
            this.txtPartCode = new System.Windows.Forms.TextBox();
            this.txtPartName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInsertAbove = new System.Windows.Forms.Button();
            this.btnInsertBelow = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.txtDayRequired = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHourRequired = new System.Windows.Forms.TextBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnAutoAdjust = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMachineSchedule
            // 
            this.lblMachineSchedule.AutoSize = true;
            this.lblMachineSchedule.Location = new System.Drawing.Point(32, 157);
            this.lblMachineSchedule.Name = "lblMachineSchedule";
            this.lblMachineSchedule.Size = new System.Drawing.Size(142, 19);
            this.lblMachineSchedule.TabIndex = 113;
            this.lblMachineSchedule.Text = "MACHINE SCHEDULE";
            // 
            // dgvMac
            // 
            this.dgvMac.AllowUserToAddRows = false;
            this.dgvMac.AllowUserToDeleteRows = false;
            this.dgvMac.AllowUserToResizeColumns = false;
            this.dgvMac.AllowUserToResizeRows = false;
            this.dgvMac.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMac.BackgroundColor = System.Drawing.Color.White;
            this.dgvMac.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMac.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMac.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvMac.GridColor = System.Drawing.Color.White;
            this.dgvMac.Location = new System.Drawing.Point(36, 178);
            this.dgvMac.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMac.MultiSelect = false;
            this.dgvMac.Name = "dgvMac";
            this.dgvMac.ReadOnly = true;
            this.dgvMac.RowHeadersVisible = false;
            this.dgvMac.RowTemplate.Height = 40;
            this.dgvMac.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMac.Size = new System.Drawing.Size(1066, 379);
            this.dgvMac.TabIndex = 112;
            this.dgvMac.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMac_CellClick);
            this.dgvMac.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMac_CellFormatting);
            this.dgvMac.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMac_CellMouseDown);
            // 
            // cmbMacID
            // 
            this.cmbMacID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMacID.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMacID.FormattingEnabled = true;
            this.cmbMacID.Location = new System.Drawing.Point(36, 46);
            this.cmbMacID.Name = "cmbMacID";
            this.cmbMacID.Size = new System.Drawing.Size(57, 25);
            this.cmbMacID.TabIndex = 114;
            this.cmbMacID.SelectedIndexChanged += new System.EventHandler(this.cmbMacID_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 19);
            this.label1.TabIndex = 115;
            this.label1.Text = "MAC.";
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(982, 614);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(120, 50);
            this.btnCheck.TabIndex = 116;
            this.btnCheck.Text = "APPLY";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(838, 614);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 50);
            this.button1.TabIndex = 117;
            this.button1.Text = "CANCEL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarFont = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Black;
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(630, 41);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(216, 30);
            this.dtpStartDate.TabIndex = 118;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // dtpEstimateEndDate
            // 
            this.dtpEstimateEndDate.CalendarFont = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEstimateEndDate.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEstimateEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEstimateEndDate.Location = new System.Drawing.Point(852, 39);
            this.dtpEstimateEndDate.Name = "dtpEstimateEndDate";
            this.dtpEstimateEndDate.Size = new System.Drawing.Size(216, 30);
            this.dtpEstimateEndDate.TabIndex = 119;
            this.dtpEstimateEndDate.ValueChanged += new System.EventHandler(this.dtpEstimateEndDate_ValueChanged);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(848, 19);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(136, 19);
            this.lblEndDate.TabIndex = 121;
            this.lblEndDate.Text = "ESTIMATE END DATE";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(626, 19);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(81, 19);
            this.lblStartDate.TabIndex = 120;
            this.lblStartDate.Text = "START DATE";
            // 
            // cbIncludeSunday
            // 
            this.cbIncludeSunday.AutoSize = true;
            this.cbIncludeSunday.Location = new System.Drawing.Point(852, 75);
            this.cbIncludeSunday.Name = "cbIncludeSunday";
            this.cbIncludeSunday.Size = new System.Drawing.Size(123, 23);
            this.cbIncludeSunday.TabIndex = 122;
            this.cbIncludeSunday.Text = "include Sunday";
            this.cbIncludeSunday.UseVisualStyleBackColor = true;
            this.cbIncludeSunday.CheckedChanged += new System.EventHandler(this.cbIncludeSunday_CheckedChanged);
            // 
            // txtPartCode
            // 
            this.txtPartCode.Enabled = false;
            this.txtPartCode.Location = new System.Drawing.Point(104, 46);
            this.txtPartCode.Name = "txtPartCode";
            this.txtPartCode.Size = new System.Drawing.Size(176, 25);
            this.txtPartCode.TabIndex = 123;
            this.txtPartCode.Text = "Part Code";
            // 
            // txtPartName
            // 
            this.txtPartName.Enabled = false;
            this.txtPartName.Location = new System.Drawing.Point(298, 46);
            this.txtPartName.Name = "txtPartName";
            this.txtPartName.Size = new System.Drawing.Size(176, 25);
            this.txtPartName.TabIndex = 124;
            this.txtPartName.Text = "PartName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 19);
            this.label2.TabIndex = 125;
            this.label2.Text = "CODE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(294, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 19);
            this.label3.TabIndex = 126;
            this.label3.Text = "NAME";
            // 
            // btnInsertAbove
            // 
            this.btnInsertAbove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsertAbove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnInsertAbove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInsertAbove.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertAbove.ForeColor = System.Drawing.Color.Black;
            this.btnInsertAbove.Location = new System.Drawing.Point(977, 147);
            this.btnInsertAbove.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsertAbove.Name = "btnInsertAbove";
            this.btnInsertAbove.Size = new System.Drawing.Size(125, 27);
            this.btnInsertAbove.TabIndex = 127;
            this.btnInsertAbove.Text = "INSERT ABOVE";
            this.btnInsertAbove.UseVisualStyleBackColor = false;
            this.btnInsertAbove.Click += new System.EventHandler(this.btnInsertAbove_Click);
            // 
            // btnInsertBelow
            // 
            this.btnInsertBelow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsertBelow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnInsertBelow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInsertBelow.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertBelow.ForeColor = System.Drawing.Color.Black;
            this.btnInsertBelow.Location = new System.Drawing.Point(833, 147);
            this.btnInsertBelow.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsertBelow.Name = "btnInsertBelow";
            this.btnInsertBelow.Size = new System.Drawing.Size(125, 27);
            this.btnInsertBelow.TabIndex = 128;
            this.btnInsertBelow.Text = "INSERT BELOW";
            this.btnInsertBelow.UseVisualStyleBackColor = false;
            this.btnInsertBelow.Click += new System.EventHandler(this.btnInsertBelow_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(479, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 19);
            this.label4.TabIndex = 130;
            this.label4.Text = "DAY";
            // 
            // txtDayRequired
            // 
            this.txtDayRequired.Enabled = false;
            this.txtDayRequired.Location = new System.Drawing.Point(483, 46);
            this.txtDayRequired.Name = "txtDayRequired";
            this.txtDayRequired.Size = new System.Drawing.Size(59, 25);
            this.txtDayRequired.TabIndex = 129;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(544, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 19);
            this.label5.TabIndex = 132;
            this.label5.Text = "HOUR";
            // 
            // txtHourRequired
            // 
            this.txtHourRequired.Enabled = false;
            this.txtHourRequired.Location = new System.Drawing.Point(548, 46);
            this.txtHourRequired.Name = "txtHourRequired";
            this.txtHourRequired.Size = new System.Drawing.Size(59, 25);
            this.txtHourRequired.TabIndex = 131;
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(218, 120);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(62, 28);
            this.lblWarning.TabIndex = 133;
            this.lblWarning.Text = "CODE";
            this.lblWarning.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_available_updates_96;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1073, 39);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 30);
            this.button2.TabIndex = 135;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // btnAutoAdjust
            // 
            this.btnAutoAdjust.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoAdjust.BackColor = System.Drawing.Color.White;
            this.btnAutoAdjust.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAutoAdjust.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoAdjust.ForeColor = System.Drawing.Color.Black;
            this.btnAutoAdjust.Location = new System.Drawing.Point(693, 147);
            this.btnAutoAdjust.Margin = new System.Windows.Forms.Padding(2);
            this.btnAutoAdjust.Name = "btnAutoAdjust";
            this.btnAutoAdjust.Size = new System.Drawing.Size(123, 27);
            this.btnAutoAdjust.TabIndex = 148;
            this.btnAutoAdjust.Text = "AUTO ADJUST";
            this.btnAutoAdjust.UseVisualStyleBackColor = false;
            this.btnAutoAdjust.Click += new System.EventHandler(this.btnAutoAdjust_Click);
            // 
            // frmMacScheduleAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1153, 685);
            this.Controls.Add(this.btnAutoAdjust);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHourRequired);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDayRequired);
            this.Controls.Add(this.btnInsertBelow);
            this.Controls.Add(this.btnInsertAbove);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPartName);
            this.Controls.Add(this.txtPartCode);
            this.Controls.Add(this.cbIncludeSunday);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpEstimateEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbMacID);
            this.Controls.Add(this.lblMachineSchedule);
            this.Controls.Add(this.dgvMac);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMacScheduleAdjust";
            this.Text = "MACHINE SCHEDULE";
            this.Load += new System.EventHandler(this.frmMacScheduleAdjust_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMachineSchedule;
        private System.Windows.Forms.DataGridView dgvMac;
        private System.Windows.Forms.ComboBox cmbMacID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEstimateEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.CheckBox cbIncludeSunday;
        private System.Windows.Forms.TextBox txtPartCode;
        private System.Windows.Forms.TextBox txtPartName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnInsertAbove;
        private System.Windows.Forms.Button btnInsertBelow;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHourRequired;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDayRequired;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.Button btnAutoAdjust;
    }
}