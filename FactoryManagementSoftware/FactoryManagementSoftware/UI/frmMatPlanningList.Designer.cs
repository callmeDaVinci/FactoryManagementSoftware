namespace FactoryManagementSoftware.UI
{
    partial class frmMatPlanningList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnCheckList = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnByPlan = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnByMat = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblUpdatedTime = new System.Windows.Forms.Label();
            this.tlpHeaderButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddMat = new System.Windows.Forms.Button();
            this.tlpMaterialList = new System.Windows.Forms.TableLayoutPanel();
            this.dgvMatListByPlan = new System.Windows.Forms.DataGridView();
            this.dgvMatListByMat = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tlpHeaderButton.SuspendLayout();
            this.tlpMaterialList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatListByPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatListByMat)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.Black;
            this.btnCheck.Location = new System.Drawing.Point(1240, 1);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(142, 47);
            this.btnCheck.TabIndex = 132;
            this.btnCheck.Text = "PREPARE";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnCheckList
            // 
            this.btnCheckList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(100)))));
            this.btnCheckList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckList.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckList.ForeColor = System.Drawing.Color.Black;
            this.btnCheckList.Location = new System.Drawing.Point(1090, 1);
            this.btnCheckList.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnCheckList.Name = "btnCheckList";
            this.btnCheckList.Size = new System.Drawing.Size(142, 47);
            this.btnCheckList.TabIndex = 135;
            this.btnCheckList.Text = "CHECK LIST";
            this.btnCheckList.UseVisualStyleBackColor = false;
            this.btnCheckList.Click += new System.EventHandler(this.btnCheckList_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 19);
            this.label2.TabIndex = 136;
            this.label2.Text = "MATERIAL REQUIRED LIST";
            // 
            // btnByPlan
            // 
            this.btnByPlan.BackColor = System.Drawing.Color.White;
            this.btnByPlan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnByPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnByPlan.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnByPlan.ForeColor = System.Drawing.Color.Black;
            this.btnByPlan.Location = new System.Drawing.Point(201, 1);
            this.btnByPlan.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnByPlan.Name = "btnByPlan";
            this.btnByPlan.Size = new System.Drawing.Size(145, 36);
            this.btnByPlan.TabIndex = 137;
            this.btnByPlan.Text = "BY PLAN";
            this.btnByPlan.UseVisualStyleBackColor = false;
            this.btnByPlan.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(1408, 94);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(145, 36);
            this.btnSave.TabIndex = 138;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnByMat
            // 
            this.btnByMat.BackColor = System.Drawing.Color.White;
            this.btnByMat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnByMat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnByMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnByMat.ForeColor = System.Drawing.Color.Black;
            this.btnByMat.Location = new System.Drawing.Point(47, 1);
            this.btnByMat.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnByMat.Name = "btnByMat";
            this.btnByMat.Size = new System.Drawing.Size(145, 36);
            this.btnByMat.TabIndex = 142;
            this.btnByMat.Text = "BY MATERIAL";
            this.btnByMat.UseVisualStyleBackColor = false;
            this.btnByMat.Click += new System.EventHandler(this.btnByMat_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_go_back_64;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(2, 2);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(38, 38);
            this.button2.TabIndex = 141;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::FactoryManagementSoftware.Properties.Resources.icons8_refresh_480;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(193, 94);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(36, 36);
            this.btnRefresh.TabIndex = 139;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(236, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 12);
            this.label7.TabIndex = 144;
            this.label7.Text = "LAST UPDATED:";
            // 
            // lblUpdatedTime
            // 
            this.lblUpdatedTime.AutoSize = true;
            this.lblUpdatedTime.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdatedTime.Location = new System.Drawing.Point(236, 113);
            this.lblUpdatedTime.Name = "lblUpdatedTime";
            this.lblUpdatedTime.Size = new System.Drawing.Size(125, 12);
            this.lblUpdatedTime.TabIndex = 143;
            this.lblUpdatedTime.Text = "SHOW DATA FOR THE PAST";
            // 
            // tlpHeaderButton
            // 
            this.tlpHeaderButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpHeaderButton.ColumnCount = 6;
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpHeaderButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpHeaderButton.Controls.Add(this.btnAddMat, 5, 0);
            this.tlpHeaderButton.Controls.Add(this.btnByMat, 1, 0);
            this.tlpHeaderButton.Controls.Add(this.btnByPlan, 2, 0);
            this.tlpHeaderButton.Controls.Add(this.btnCheckList, 3, 0);
            this.tlpHeaderButton.Controls.Add(this.btnCheck, 4, 0);
            this.tlpHeaderButton.Controls.Add(this.button2, 0, 0);
            this.tlpHeaderButton.Location = new System.Drawing.Point(21, 12);
            this.tlpHeaderButton.Name = "tlpHeaderButton";
            this.tlpHeaderButton.RowCount = 1;
            this.tlpHeaderButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeaderButton.Size = new System.Drawing.Size(1536, 52);
            this.tlpHeaderButton.TabIndex = 145;
            this.tlpHeaderButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tlpHeaderButton_MouseClick);
            // 
            // btnAddMat
            // 
            this.btnAddMat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMat.BackColor = System.Drawing.Color.White;
            this.btnAddMat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMat.ForeColor = System.Drawing.Color.Black;
            this.btnAddMat.Location = new System.Drawing.Point(1390, 1);
            this.btnAddMat.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnAddMat.Name = "btnAddMat";
            this.btnAddMat.Size = new System.Drawing.Size(142, 47);
            this.btnAddMat.TabIndex = 146;
            this.btnAddMat.Text = "ADD MATERIAL";
            this.btnAddMat.UseVisualStyleBackColor = false;
            this.btnAddMat.Click += new System.EventHandler(this.btnAddMat_Click);
            // 
            // tlpMaterialList
            // 
            this.tlpMaterialList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMaterialList.ColumnCount = 2;
            this.tlpMaterialList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMaterialList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMaterialList.Controls.Add(this.dgvMatListByPlan, 1, 0);
            this.tlpMaterialList.Controls.Add(this.dgvMatListByMat, 0, 0);
            this.tlpMaterialList.Location = new System.Drawing.Point(21, 136);
            this.tlpMaterialList.Margin = new System.Windows.Forms.Padding(4);
            this.tlpMaterialList.Name = "tlpMaterialList";
            this.tlpMaterialList.RowCount = 1;
            this.tlpMaterialList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMaterialList.Size = new System.Drawing.Size(1532, 691);
            this.tlpMaterialList.TabIndex = 146;
            // 
            // dgvMatListByPlan
            // 
            this.dgvMatListByPlan.AllowUserToAddRows = false;
            this.dgvMatListByPlan.AllowUserToDeleteRows = false;
            this.dgvMatListByPlan.AllowUserToOrderColumns = true;
            this.dgvMatListByPlan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMatListByPlan.BackgroundColor = System.Drawing.Color.DimGray;
            this.dgvMatListByPlan.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMatListByPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatListByPlan.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatListByPlan.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMatListByPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatListByPlan.GridColor = System.Drawing.Color.White;
            this.dgvMatListByPlan.Location = new System.Drawing.Point(770, 1);
            this.dgvMatListByPlan.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvMatListByPlan.Name = "dgvMatListByPlan";
            this.dgvMatListByPlan.ReadOnly = true;
            this.dgvMatListByPlan.RowHeadersVisible = false;
            this.dgvMatListByPlan.RowTemplate.Height = 40;
            this.dgvMatListByPlan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatListByPlan.Size = new System.Drawing.Size(758, 689);
            this.dgvMatListByPlan.TabIndex = 136;
            this.dgvMatListByPlan.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMatList_CellFormatting);
            this.dgvMatListByPlan.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMatListByPlan_CellMouseClick);
            this.dgvMatListByPlan.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMatListByPlan_CellMouseDown);
            this.dgvMatListByPlan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvMatListByPlan_MouseClick);
            // 
            // dgvMatListByMat
            // 
            this.dgvMatListByMat.AllowUserToAddRows = false;
            this.dgvMatListByMat.AllowUserToDeleteRows = false;
            this.dgvMatListByMat.AllowUserToResizeColumns = false;
            this.dgvMatListByMat.AllowUserToResizeRows = false;
            this.dgvMatListByMat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMatListByMat.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMatListByMat.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMatListByMat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatListByMat.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatListByMat.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMatListByMat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatListByMat.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvMatListByMat.Location = new System.Drawing.Point(3, 3);
            this.dgvMatListByMat.MultiSelect = false;
            this.dgvMatListByMat.Name = "dgvMatListByMat";
            this.dgvMatListByMat.ReadOnly = true;
            this.dgvMatListByMat.RowHeadersVisible = false;
            this.dgvMatListByMat.RowTemplate.Height = 40;
            this.dgvMatListByMat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatListByMat.Size = new System.Drawing.Size(760, 685);
            this.dgvMatListByMat.TabIndex = 111;
            this.dgvMatListByMat.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatList_CellEndEdit);
            this.dgvMatListByMat.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatList_CellEnter);
            this.dgvMatListByMat.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMatList_CellFormatting);
            this.dgvMatListByMat.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMatListByMat_CellMouseClick);
            this.dgvMatListByMat.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMatListByPlan_CellMouseDown);
            this.dgvMatListByMat.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatList_CellValueChanged);
            this.dgvMatListByMat.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvMatList_CurrentCellDirtyStateChanged);
            this.dgvMatListByMat.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMatList_DataError);
            this.dgvMatListByMat.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvMatListByMat_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // frmMatPlanningList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.tlpMaterialList);
            this.Controls.Add(this.tlpHeaderButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblUpdatedTime);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMatPlanningList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMatPlanningList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMatPlanningList_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMatPlanningList_MouseClick);
            this.tlpHeaderButton.ResumeLayout(false);
            this.tlpMaterialList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatListByPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatListByMat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnCheckList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnByPlan;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnByMat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblUpdatedTime;
        private System.Windows.Forms.TableLayoutPanel tlpHeaderButton;
        private System.Windows.Forms.Button btnAddMat;
        private System.Windows.Forms.TableLayoutPanel tlpMaterialList;
        private System.Windows.Forms.DataGridView dgvMatListByMat;
        private System.Windows.Forms.DataGridView dgvMatListByPlan;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}