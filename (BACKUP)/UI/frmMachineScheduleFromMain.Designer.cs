namespace FactoryManagementSoftware.UI
{
    partial class frmMachineScheduleAdjustFromMain
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
            this.lblWarning = new System.Windows.Forms.Label();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblMachineSchedule = new System.Windows.Forms.Label();
            this.dgvMac = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cbIncludeSunday = new System.Windows.Forms.CheckBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpEstimateEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.btnAutoAdjust = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(203, 116);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(62, 28);
            this.lblWarning.TabIndex = 140;
            this.lblWarning.Text = "CODE";
            this.lblWarning.Visible = false;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMoveDown.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveDown.ForeColor = System.Drawing.Color.Black;
            this.btnMoveDown.Location = new System.Drawing.Point(897, 115);
            this.btnMoveDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(123, 27);
            this.btnMoveDown.TabIndex = 139;
            this.btnMoveDown.Text = "MOVE DOWN";
            this.btnMoveDown.UseVisualStyleBackColor = false;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMoveUp.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveUp.ForeColor = System.Drawing.Color.Black;
            this.btnMoveUp.Location = new System.Drawing.Point(1035, 115);
            this.btnMoveUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(123, 27);
            this.btnMoveUp.TabIndex = 138;
            this.btnMoveUp.Text = "MOVE UP";
            this.btnMoveUp.UseVisualStyleBackColor = false;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(937, 604);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 50);
            this.btnCancel.TabIndex = 137;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location = new System.Drawing.Point(1085, 604);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(120, 50);
            this.btnApply.TabIndex = 136;
            this.btnApply.Text = "APPLY";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblMachineSchedule
            // 
            this.lblMachineSchedule.AutoSize = true;
            this.lblMachineSchedule.Location = new System.Drawing.Point(41, 125);
            this.lblMachineSchedule.Name = "lblMachineSchedule";
            this.lblMachineSchedule.Size = new System.Drawing.Size(142, 19);
            this.lblMachineSchedule.TabIndex = 135;
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
            this.dgvMac.Location = new System.Drawing.Point(45, 146);
            this.dgvMac.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMac.MultiSelect = false;
            this.dgvMac.Name = "dgvMac";
            this.dgvMac.ReadOnly = true;
            this.dgvMac.RowHeadersVisible = false;
            this.dgvMac.RowTemplate.Height = 40;
            this.dgvMac.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMac.Size = new System.Drawing.Size(1160, 431);
            this.dgvMac.TabIndex = 134;
            this.dgvMac.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMac_CellFormatting);
            this.dgvMac.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMac_CellMouseDown);
            this.dgvMac.SelectionChanged += new System.EventHandler(this.dgvMac_SelectionChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_available_updates_96;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(1176, 114);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(29, 30);
            this.btnRefresh.TabIndex = 141;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // cbIncludeSunday
            // 
            this.cbIncludeSunday.AutoSize = true;
            this.cbIncludeSunday.Location = new System.Drawing.Point(267, 70);
            this.cbIncludeSunday.Name = "cbIncludeSunday";
            this.cbIncludeSunday.Size = new System.Drawing.Size(123, 23);
            this.cbIncludeSunday.TabIndex = 146;
            this.cbIncludeSunday.Text = "include Sunday";
            this.cbIncludeSunday.UseVisualStyleBackColor = true;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(263, 14);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(136, 19);
            this.lblEndDate.TabIndex = 145;
            this.lblEndDate.Text = "ESTIMATE END DATE";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(41, 14);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(81, 19);
            this.lblStartDate.TabIndex = 144;
            this.lblStartDate.Text = "START DATE";
            // 
            // dtpEstimateEndDate
            // 
            this.dtpEstimateEndDate.CalendarFont = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEstimateEndDate.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEstimateEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEstimateEndDate.Location = new System.Drawing.Point(267, 34);
            this.dtpEstimateEndDate.Name = "dtpEstimateEndDate";
            this.dtpEstimateEndDate.Size = new System.Drawing.Size(216, 30);
            this.dtpEstimateEndDate.TabIndex = 143;
            this.dtpEstimateEndDate.ValueChanged += new System.EventHandler(this.dtpEstimateEndDate_ValueChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarFont = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Black;
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(45, 34);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(216, 30);
            this.dtpStartDate.TabIndex = 142;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // btnAutoAdjust
            // 
            this.btnAutoAdjust.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoAdjust.BackColor = System.Drawing.Color.White;
            this.btnAutoAdjust.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAutoAdjust.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoAdjust.ForeColor = System.Drawing.Color.Black;
            this.btnAutoAdjust.Location = new System.Drawing.Point(759, 114);
            this.btnAutoAdjust.Margin = new System.Windows.Forms.Padding(2);
            this.btnAutoAdjust.Name = "btnAutoAdjust";
            this.btnAutoAdjust.Size = new System.Drawing.Size(123, 27);
            this.btnAutoAdjust.TabIndex = 147;
            this.btnAutoAdjust.Text = "AUTO ADJUST";
            this.btnAutoAdjust.UseVisualStyleBackColor = false;
            this.btnAutoAdjust.Click += new System.EventHandler(this.btnAutoAdjust_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            // 
            // frmMachineScheduleAdjustFromMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1258, 688);
            this.Controls.Add(this.btnAutoAdjust);
            this.Controls.Add(this.cbIncludeSunday);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpEstimateEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblMachineSchedule);
            this.Controls.Add(this.dgvMac);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMachineScheduleAdjustFromMain";
            this.Text = "Machine Schedule";
            this.Load += new System.EventHandler(this.frmMachineScheduleAdjustFromMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblMachineSchedule;
        private System.Windows.Forms.DataGridView dgvMac;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnAutoAdjust;
        private System.Windows.Forms.CheckBox cbIncludeSunday;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpEstimateEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
    }
}