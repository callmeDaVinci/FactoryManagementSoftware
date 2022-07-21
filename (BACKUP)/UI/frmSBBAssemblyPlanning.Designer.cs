namespace FactoryManagementSoftware.UI
{
    partial class frmSBBAssemblyPlanning
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
            this.tlpPOList = new System.Windows.Forms.TableLayoutPanel();
            this.tlpSetting = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbSiteLocation = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbStockInclude = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnText = new System.Windows.Forms.Button();
            this.cbWithStdPacking = new System.Windows.Forms.CheckBox();
            this.lblSubList = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbSameDay = new System.Windows.Forms.CheckBox();
            this.lblTimesNeeded = new System.Windows.Forms.Label();
            this.cbSkipSunday = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMainList = new System.Windows.Forms.Label();
            this.cbMaxAvailability = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tlpMaterialList = new System.Windows.Forms.TableLayoutPanel();
            this.tlpList = new System.Windows.Forms.TableLayoutPanel();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.dgvMatList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPreviousStep = new System.Windows.Forms.Button();
            this.btnNextStep = new System.Windows.Forms.Button();
            this.tlpPOList.SuspendLayout();
            this.tlpSetting.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpMaterialList.SuspendLayout();
            this.tlpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatList)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpPOList
            // 
            this.tlpPOList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpPOList.ColumnCount = 3;
            this.tlpPOList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpPOList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tlpPOList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpPOList.Controls.Add(this.tlpSetting, 2, 0);
            this.tlpPOList.Controls.Add(this.dgvItemList, 0, 1);
            this.tlpPOList.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpPOList.Controls.Add(this.tlpMaterialList, 2, 1);
            this.tlpPOList.Location = new System.Drawing.Point(8, 12);
            this.tlpPOList.Name = "tlpPOList";
            this.tlpPOList.RowCount = 2;
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tlpPOList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPOList.Size = new System.Drawing.Size(1328, 697);
            this.tlpPOList.TabIndex = 173;
            // 
            // tlpSetting
            // 
            this.tlpSetting.ColumnCount = 3;
            this.tlpSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tlpSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSetting.Controls.Add(this.tableLayoutPanel7, 1, 0);
            this.tlpSetting.Controls.Add(this.lblSubList, 0, 0);
            this.tlpSetting.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tlpSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSetting.Location = new System.Drawing.Point(536, 3);
            this.tlpSetting.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.tlpSetting.Name = "tlpSetting";
            this.tlpSetting.RowCount = 1;
            this.tlpSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSetting.Size = new System.Drawing.Size(789, 69);
            this.tlpSetting.TabIndex = 174;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 153F));
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(97, 3);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(341, 66);
            this.tableLayoutPanel7.TabIndex = 180;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.cmbSiteLocation, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(244, 60);
            this.tableLayoutPanel3.TabIndex = 175;
            // 
            // cmbSiteLocation
            // 
            this.cmbSiteLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSiteLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSiteLocation.FormattingEnabled = true;
            this.cmbSiteLocation.Location = new System.Drawing.Point(3, 29);
            this.cmbSiteLocation.Name = "cmbSiteLocation";
            this.cmbSiteLocation.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbSiteLocation.Size = new System.Drawing.Size(238, 25);
            this.cmbSiteLocation.TabIndex = 176;
            this.cmbSiteLocation.SelectedIndexChanged += new System.EventHandler(this.cmbSiteLocation_SelectedIndexChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbStockInclude, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(244, 26);
            this.tableLayoutPanel4.TabIndex = 176;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 6F);
            this.label2.Location = new System.Drawing.Point(2, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 12);
            this.label2.TabIndex = 176;
            this.label2.Text = "ASSEMBLY SITE";
            // 
            // cbStockInclude
            // 
            this.cbStockInclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbStockInclude.AutoSize = true;
            this.cbStockInclude.Location = new System.Drawing.Point(129, 3);
            this.cbStockInclude.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.cbStockInclude.Name = "cbStockInclude";
            this.cbStockInclude.Size = new System.Drawing.Size(112, 23);
            this.cbStockInclude.TabIndex = 177;
            this.cbStockInclude.Text = "Stock Include";
            this.cbStockInclude.UseVisualStyleBackColor = true;
            this.cbStockInclude.CheckedChanged += new System.EventHandler(this.cbStockInclude_CheckedChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.btnText, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.cbWithStdPacking, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(191, 3);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(147, 63);
            this.tableLayoutPanel5.TabIndex = 179;
            // 
            // btnText
            // 
            this.btnText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnText.BackColor = System.Drawing.Color.White;
            this.btnText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnText.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnText.ForeColor = System.Drawing.Color.Black;
            this.btnText.Location = new System.Drawing.Point(7, 26);
            this.btnText.Margin = new System.Windows.Forms.Padding(4, 1, 3, 0);
            this.btnText.Name = "btnText";
            this.btnText.Size = new System.Drawing.Size(137, 32);
            this.btnText.TabIndex = 167;
            this.btnText.Text = "TEXT";
            this.btnText.UseVisualStyleBackColor = false;
            this.btnText.Click += new System.EventHandler(this.btnText_Click);
            // 
            // cbWithStdPacking
            // 
            this.cbWithStdPacking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWithStdPacking.AutoSize = true;
            this.cbWithStdPacking.Checked = true;
            this.cbWithStdPacking.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWithStdPacking.Location = new System.Drawing.Point(9, 2);
            this.cbWithStdPacking.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cbWithStdPacking.Name = "cbWithStdPacking";
            this.cbWithStdPacking.Size = new System.Drawing.Size(135, 23);
            this.cbWithStdPacking.TabIndex = 178;
            this.cbWithStdPacking.Text = "With Std Packing";
            this.cbWithStdPacking.UseVisualStyleBackColor = true;
            // 
            // lblSubList
            // 
            this.lblSubList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSubList.AutoSize = true;
            this.lblSubList.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubList.Location = new System.Drawing.Point(2, 26);
            this.lblSubList.Margin = new System.Windows.Forms.Padding(2, 0, 2, 5);
            this.lblSubList.Name = "lblSubList";
            this.lblSubList.Size = new System.Drawing.Size(76, 38);
            this.lblSubList.TabIndex = 166;
            this.lblSubList.Text = "MATERIAL LIST";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 238F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.cbSameDay, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblTimesNeeded, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbSkipSunday, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(444, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(342, 63);
            this.tableLayoutPanel2.TabIndex = 193;
            // 
            // cbSameDay
            // 
            this.cbSameDay.AutoSize = true;
            this.cbSameDay.Checked = true;
            this.cbSameDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSameDay.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSameDay.Location = new System.Drawing.Point(241, 33);
            this.cbSameDay.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.cbSameDay.Name = "cbSameDay";
            this.cbSameDay.Size = new System.Drawing.Size(79, 23);
            this.cbSameDay.TabIndex = 195;
            this.cbSameDay.Text = "Same Day";
            this.cbSameDay.UseVisualStyleBackColor = true;
            this.cbSameDay.CheckedChanged += new System.EventHandler(this.cbSameDay_CheckedChanged);
            // 
            // lblTimesNeeded
            // 
            this.lblTimesNeeded.AutoSize = true;
            this.lblTimesNeeded.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTimesNeeded.Location = new System.Drawing.Point(2, 32);
            this.lblTimesNeeded.Margin = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.lblTimesNeeded.Name = "lblTimesNeeded";
            this.lblTimesNeeded.Size = new System.Drawing.Size(161, 23);
            this.lblTimesNeeded.TabIndex = 194;
            this.lblTimesNeeded.Text = "00 Day(s) && 00 Hr(s)";
            // 
            // cbSkipSunday
            // 
            this.cbSkipSunday.AutoSize = true;
            this.cbSkipSunday.Checked = true;
            this.cbSkipSunday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSkipSunday.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSkipSunday.Location = new System.Drawing.Point(241, 3);
            this.cbSkipSunday.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.cbSkipSunday.Name = "cbSkipSunday";
            this.cbSkipSunday.Size = new System.Drawing.Size(79, 23);
            this.cbSkipSunday.TabIndex = 195;
            this.cbSkipSunday.Text = "Skip Sunday";
            this.cbSkipSunday.UseVisualStyleBackColor = true;
            this.cbSkipSunday.CheckedChanged += new System.EventHandler(this.cbSkipSunday_CheckedChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(2, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 19);
            this.label7.TabIndex = 192;
            this.label7.Text = "Times Needed:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // dgvItemList
            // 
            this.dgvItemList.AllowDrop = true;
            this.dgvItemList.AllowUserToAddRows = false;
            this.dgvItemList.AllowUserToDeleteRows = false;
            this.dgvItemList.AllowUserToOrderColumns = true;
            this.dgvItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvItemList.BackgroundColor = System.Drawing.Color.White;
            this.dgvItemList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvItemList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItemList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvItemList.Location = new System.Drawing.Point(3, 73);
            this.dgvItemList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.RowHeadersVisible = false;
            this.dgvItemList.RowTemplate.Height = 50;
            this.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvItemList.Size = new System.Drawing.Size(524, 621);
            this.dgvItemList.TabIndex = 156;
            this.dgvItemList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellClick);
            this.dgvItemList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellContentClick);
            this.dgvItemList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellEndEdit);
            this.dgvItemList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemList_CellMouseClick);
            this.dgvItemList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemList_CellMouseDown);
            this.dgvItemList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvItemList_EditingControlShowing);
            this.dgvItemList.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvItemList_DragDrop);
            this.dgvItemList.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvItemList_DragOver);
            this.dgvItemList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvItemList_MouseDown);
            this.dgvItemList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvItemList_MouseMove);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel1.Controls.Add(this.lblMainList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbMaxAvailability, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(524, 69);
            this.tableLayoutPanel1.TabIndex = 172;
            // 
            // lblMainList
            // 
            this.lblMainList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMainList.AutoSize = true;
            this.lblMainList.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainList.Location = new System.Drawing.Point(2, 45);
            this.lblMainList.Margin = new System.Windows.Forms.Padding(2, 0, 2, 5);
            this.lblMainList.Name = "lblMainList";
            this.lblMainList.Size = new System.Drawing.Size(69, 19);
            this.lblMainList.TabIndex = 166;
            this.lblMainList.Text = "ITEM LIST";
            // 
            // cbMaxAvailability
            // 
            this.cbMaxAvailability.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMaxAvailability.AutoSize = true;
            this.cbMaxAvailability.Location = new System.Drawing.Point(78, 43);
            this.cbMaxAvailability.Name = "cbMaxAvailability";
            this.cbMaxAvailability.Size = new System.Drawing.Size(281, 23);
            this.cbMaxAvailability.TabIndex = 176;
            this.cbMaxAvailability.Text = "MAX Availability (Assembly all body part)";
            this.cbMaxAvailability.UseVisualStyleBackColor = true;
            this.cbMaxAvailability.CheckedChanged += new System.EventHandler(this.cbMaxAvailability_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(396, 32);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(124, 32);
            this.btnAdd.TabIndex = 167;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAddNewDO_Click);
            // 
            // tlpMaterialList
            // 
            this.tlpMaterialList.ColumnCount = 1;
            this.tlpMaterialList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMaterialList.Controls.Add(this.tlpList, 0, 0);
            this.tlpMaterialList.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tlpMaterialList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMaterialList.Location = new System.Drawing.Point(536, 72);
            this.tlpMaterialList.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tlpMaterialList.Name = "tlpMaterialList";
            this.tlpMaterialList.RowCount = 2;
            this.tlpMaterialList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMaterialList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpMaterialList.Size = new System.Drawing.Size(789, 622);
            this.tlpMaterialList.TabIndex = 173;
            // 
            // tlpList
            // 
            this.tlpList.ColumnCount = 2;
            this.tlpList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpList.Controls.Add(this.dgvSchedule, 1, 0);
            this.tlpList.Controls.Add(this.dgvMatList, 0, 0);
            this.tlpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpList.Location = new System.Drawing.Point(0, 0);
            this.tlpList.Margin = new System.Windows.Forms.Padding(0);
            this.tlpList.Name = "tlpList";
            this.tlpList.RowCount = 1;
            this.tlpList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpList.Size = new System.Drawing.Size(789, 573);
            this.tlpList.TabIndex = 174;
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.AllowDrop = true;
            this.dgvSchedule.AllowUserToAddRows = false;
            this.dgvSchedule.AllowUserToDeleteRows = false;
            this.dgvSchedule.AllowUserToResizeColumns = false;
            this.dgvSchedule.AllowUserToResizeRows = false;
            this.dgvSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvSchedule.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvSchedule.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvSchedule.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSchedule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSchedule.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSchedule.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvSchedule.Location = new System.Drawing.Point(397, 1);
            this.dgvSchedule.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvSchedule.MultiSelect = false;
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.ReadOnly = true;
            this.dgvSchedule.RowHeadersVisible = false;
            this.dgvSchedule.RowTemplate.Height = 50;
            this.dgvSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchedule.Size = new System.Drawing.Size(389, 571);
            this.dgvSchedule.TabIndex = 156;
            this.dgvSchedule.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvSchedule_DragDrop);
            this.dgvSchedule.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvSchedule_DragOver);
            this.dgvSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvSchedule_MouseDown);
            this.dgvSchedule.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvSchedule_MouseMove);
            // 
            // dgvMatList
            // 
            this.dgvMatList.AllowDrop = true;
            this.dgvMatList.AllowUserToAddRows = false;
            this.dgvMatList.AllowUserToDeleteRows = false;
            this.dgvMatList.AllowUserToResizeColumns = false;
            this.dgvMatList.AllowUserToResizeRows = false;
            this.dgvMatList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMatList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMatList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMatList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMatList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMatList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatList.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMatList.Location = new System.Drawing.Point(3, 1);
            this.dgvMatList.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dgvMatList.MultiSelect = false;
            this.dgvMatList.Name = "dgvMatList";
            this.dgvMatList.ReadOnly = true;
            this.dgvMatList.RowHeadersVisible = false;
            this.dgvMatList.RowTemplate.Height = 50;
            this.dgvMatList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatList.Size = new System.Drawing.Size(388, 571);
            this.dgvMatList.TabIndex = 155;
            this.dgvMatList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellContentClick);
            this.dgvMatList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatList_CellEnter);
            this.dgvMatList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatList_CellValueChanged);
            this.dgvMatList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMatList_DataBindingComplete);
            this.dgvMatList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvMatList_EditingControlShowing);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel6.Controls.Add(this.btnPreviousStep, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnNextStep, 2, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 573);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(789, 49);
            this.tableLayoutPanel6.TabIndex = 174;
            // 
            // btnPreviousStep
            // 
            this.btnPreviousStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviousStep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnPreviousStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviousStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviousStep.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreviousStep.ForeColor = System.Drawing.Color.Black;
            this.btnPreviousStep.Location = new System.Drawing.Point(4, 9);
            this.btnPreviousStep.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnPreviousStep.Name = "btnPreviousStep";
            this.btnPreviousStep.Size = new System.Drawing.Size(142, 35);
            this.btnPreviousStep.TabIndex = 175;
            this.btnPreviousStep.Text = "<  MATERIAL LIST";
            this.btnPreviousStep.UseVisualStyleBackColor = false;
            this.btnPreviousStep.Visible = false;
            this.btnPreviousStep.Click += new System.EventHandler(this.btnPreviousStep_Click);
            // 
            // btnNextStep
            // 
            this.btnNextStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextStep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnNextStep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextStep.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextStep.ForeColor = System.Drawing.Color.Black;
            this.btnNextStep.Location = new System.Drawing.Point(643, 9);
            this.btnNextStep.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(142, 35);
            this.btnNextStep.TabIndex = 174;
            this.btnNextStep.Text = "DATE SELECT  >";
            this.btnNextStep.UseVisualStyleBackColor = false;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // frmSBBAssemblyPlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.tlpPOList);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSBBAssemblyPlanning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSBBAssemblyPlanning";
            this.Load += new System.EventHandler(this.frmSBBAssemblyPlanning_Load);
            this.tlpPOList.ResumeLayout(false);
            this.tlpSetting.ResumeLayout(false);
            this.tlpSetting.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpMaterialList.ResumeLayout(false);
            this.tlpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatList)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPOList;
        private System.Windows.Forms.TableLayoutPanel tlpMaterialList;
        private System.Windows.Forms.DataGridView dgvItemList;
        private System.Windows.Forms.DataGridView dgvMatList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblMainList;
        private System.Windows.Forms.TableLayoutPanel tlpSetting;
        private System.Windows.Forms.Label lblSubList;
        private System.Windows.Forms.Button btnText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSiteLocation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox cbMaxAvailability;
        private System.Windows.Forms.CheckBox cbStockInclude;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.CheckBox cbWithStdPacking;
        private System.Windows.Forms.Button btnNextStep;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button btnPreviousStep;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tlpList;
        private System.Windows.Forms.DataGridView dgvSchedule;
        private System.Windows.Forms.Label lblTimesNeeded;
        private System.Windows.Forms.CheckBox cbSkipSunday;
        private System.Windows.Forms.CheckBox cbSameDay;
    }
}