namespace FactoryManagementSoftware.UI
{
    partial class frmProductionRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.dgvRecordHistory = new System.Windows.Forms.DataGridView();
            this.dgvMeterReading = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnSaveAndStock = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPartNameHeader = new System.Windows.Forms.Label();
            this.lblPartCodeHeader = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblShift = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblMeterStartAt = new System.Windows.Forms.Label();
            this.cbMorning = new System.Windows.Forms.CheckBox();
            this.cbNight = new System.Windows.Forms.CheckBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.lblTotalStockIn = new System.Windows.Forms.Label();
            this.lblBalanceCurrent = new System.Windows.Forms.Label();
            this.lblBalanceLast = new System.Windows.Forms.Label();
            this.dtpProDate = new System.Windows.Forms.DateTimePicker();
            this.txtProLotNo = new System.Windows.Forms.TextBox();
            this.txtPackingQty = new System.Windows.Forms.TextBox();
            this.cmbPackingName = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtBalanceOfThisShift = new System.Windows.Forms.TextBox();
            this.txtTotalReject = new System.Windows.Forms.TextBox();
            this.txtFullBox = new System.Windows.Forms.TextBox();
            this.txtRejectPercentage = new System.Windows.Forms.TextBox();
            this.txtTotalStockIn = new System.Windows.Forms.TextBox();
            this.lblFullCarton = new System.Windows.Forms.Label();
            this.txtBalanceOfLastShift = new System.Windows.Forms.TextBox();
            this.txtTotalShot = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtMeterStart = new System.Windows.Forms.TextBox();
            this.txtColorMatLotNo = new System.Windows.Forms.TextBox();
            this.txtRawMatLotNo = new System.Windows.Forms.TextBox();
            this.cmbPackingCode = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblPartName = new System.Windows.Forms.Label();
            this.lblPartCode = new System.Windows.Forms.Label();
            this.lblPW = new System.Windows.Forms.Label();
            this.lblRawMat = new System.Windows.Forms.Label();
            this.lblColorMat = new System.Windows.Forms.Label();
            this.lblCavity = new System.Windows.Forms.Label();
            this.lblRW = new System.Windows.Forms.Label();
            this.lblColorUsage = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPlanID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSheetID = new System.Windows.Forms.TextBox();
            this.tlpSheet = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewSheet = new System.Windows.Forms.Button();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider6 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider7 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider8 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider9 = new System.Windows.Forms.ErrorProvider(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecordHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeterReading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tlpSheet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider9)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvItemList
            // 
            this.dgvItemList.AllowUserToAddRows = false;
            this.dgvItemList.AllowUserToDeleteRows = false;
            this.dgvItemList.AllowUserToOrderColumns = true;
            this.dgvItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvItemList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItemList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvItemList.Location = new System.Drawing.Point(22, 65);
            this.dgvItemList.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvItemList.MultiSelect = false;
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.ReadOnly = true;
            this.dgvItemList.RowHeadersVisible = false;
            this.dgvItemList.RowTemplate.Height = 40;
            this.dgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemList.Size = new System.Drawing.Size(678, 436);
            this.dgvItemList.TabIndex = 153;
            this.dgvItemList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemList_CellMouseDown);
            this.dgvItemList.SelectionChanged += new System.EventHandler(this.dgvItemList_SelectionChanged);
            // 
            // dgvRecordHistory
            // 
            this.dgvRecordHistory.AllowUserToAddRows = false;
            this.dgvRecordHistory.AllowUserToDeleteRows = false;
            this.dgvRecordHistory.AllowUserToOrderColumns = true;
            this.dgvRecordHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvRecordHistory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecordHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecordHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecordHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecordHistory.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRecordHistory.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dgvRecordHistory.Location = new System.Drawing.Point(22, 547);
            this.dgvRecordHistory.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvRecordHistory.MultiSelect = false;
            this.dgvRecordHistory.Name = "dgvRecordHistory";
            this.dgvRecordHistory.ReadOnly = true;
            this.dgvRecordHistory.RowHeadersVisible = false;
            this.dgvRecordHistory.RowTemplate.Height = 40;
            this.dgvRecordHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecordHistory.Size = new System.Drawing.Size(677, 222);
            this.dgvRecordHistory.TabIndex = 154;
            this.dgvRecordHistory.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecordHistory_RowEnter);
            this.dgvRecordHistory.SelectionChanged += new System.EventHandler(this.dgvRecordHistory_SelectionChanged);
            // 
            // dgvMeterReading
            // 
            this.dgvMeterReading.AllowUserToAddRows = false;
            this.dgvMeterReading.AllowUserToDeleteRows = false;
            this.dgvMeterReading.AllowUserToOrderColumns = true;
            this.dgvMeterReading.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMeterReading.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMeterReading.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMeterReading.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMeterReading.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMeterReading.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMeterReading.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvMeterReading.Location = new System.Drawing.Point(17, 261);
            this.dgvMeterReading.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.dgvMeterReading.Name = "dgvMeterReading";
            this.dgvMeterReading.RowHeadersVisible = false;
            this.dgvMeterReading.RowTemplate.Height = 40;
            this.dgvMeterReading.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMeterReading.Size = new System.Drawing.Size(519, 458);
            this.dgvMeterReading.TabIndex = 155;
            this.dgvMeterReading.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMeterReading_CellEndEdit);
            this.dgvMeterReading.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMeterReading_CellFormatting);
            this.dgvMeterReading.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvMeterReading_EditingControlShowing);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 19);
            this.label1.TabIndex = 162;
            this.label1.Text = "PRODUCTION LIST";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 527);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 19);
            this.label2.TabIndex = 163;
            this.label2.Text = "PRODUCTION DAILY RECORD";
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(538, 27);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(162, 36);
            this.btnPrint.TabIndex = 164;
            this.btnPrint.Text = "PRINT JOB SHEET";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.White;
            this.btnAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.ForeColor = System.Drawing.Color.Black;
            this.btnAddItem.Location = new System.Drawing.Point(407, 27);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(125, 36);
            this.btnAddItem.TabIndex = 165;
            this.btnAddItem.Text = "ADD ITEM";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnSaveAndStock
            // 
            this.btnSaveAndStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnSaveAndStock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveAndStock.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAndStock.ForeColor = System.Drawing.Color.White;
            this.btnSaveAndStock.Location = new System.Drawing.Point(569, 614);
            this.btnSaveAndStock.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.btnSaveAndStock.Name = "btnSaveAndStock";
            this.btnSaveAndStock.Size = new System.Drawing.Size(252, 36);
            this.btnSaveAndStock.TabIndex = 166;
            this.btnSaveAndStock.Text = "SAVE && STOCK";
            this.btnSaveAndStock.UseVisualStyleBackColor = false;
            this.btnSaveAndStock.Click += new System.EventHandler(this.btnSaveAndStock_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(569, 664);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(252, 36);
            this.btnSave.TabIndex = 167;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(569, 712);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(252, 36);
            this.btnCancel.TabIndex = 168;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(214, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 19);
            this.label3.TabIndex = 169;
            this.label3.Text = "PRO. LOT NO.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 19);
            this.label4.TabIndex = 170;
            this.label4.Text = "CUSTOMER";
            // 
            // lblPartNameHeader
            // 
            this.lblPartNameHeader.AutoSize = true;
            this.lblPartNameHeader.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartNameHeader.Location = new System.Drawing.Point(13, 85);
            this.lblPartNameHeader.Name = "lblPartNameHeader";
            this.lblPartNameHeader.Size = new System.Drawing.Size(82, 19);
            this.lblPartNameHeader.TabIndex = 171;
            this.lblPartNameHeader.Text = "PART NAME";
            // 
            // lblPartCodeHeader
            // 
            this.lblPartCodeHeader.AutoSize = true;
            this.lblPartCodeHeader.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartCodeHeader.Location = new System.Drawing.Point(13, 104);
            this.lblPartCodeHeader.Name = "lblPartCodeHeader";
            this.lblPartCodeHeader.Size = new System.Drawing.Size(80, 19);
            this.lblPartCodeHeader.TabIndex = 172;
            this.lblPartCodeHeader.Text = "PART CODE";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(288, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 19);
            this.label7.TabIndex = 173;
            this.label7.Text = "RW PER SHOT(G)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 19);
            this.label8.TabIndex = 173;
            this.label8.Text = "PW PER SHOT(G)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(13, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 19);
            this.label10.TabIndex = 175;
            this.label10.Text = "RAW MAT.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(13, 163);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 19);
            this.label11.TabIndex = 176;
            this.label11.Text = "COLOR MAT.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(288, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 19);
            this.label12.TabIndex = 177;
            this.label12.Text = "USAGE%";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(360, 3);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(41, 19);
            this.lblDate.TabIndex = 178;
            this.lblDate.Text = "DATE";
            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShift.Location = new System.Drawing.Point(565, 2);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(44, 19);
            this.lblShift.TabIndex = 179;
            this.lblShift.Text = "SHIFT";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(288, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 19);
            this.label15.TabIndex = 180;
            this.label15.Text = "CAVITY";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(13, 191);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 19);
            this.label16.TabIndex = 181;
            this.label16.Text = "PACKING";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(565, 76);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(129, 19);
            this.label17.TabIndex = 182;
            this.label17.Text = "RAW MAT. LOT NO.";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(565, 134);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(146, 19);
            this.label18.TabIndex = 183;
            this.label18.Text = "COLOR MAT. LOT NO.";
            // 
            // lblMeterStartAt
            // 
            this.lblMeterStartAt.AutoSize = true;
            this.lblMeterStartAt.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMeterStartAt.Location = new System.Drawing.Point(565, 191);
            this.lblMeterStartAt.Name = "lblMeterStartAt";
            this.lblMeterStartAt.Size = new System.Drawing.Size(110, 19);
            this.lblMeterStartAt.TabIndex = 184;
            this.lblMeterStartAt.Text = "METER START AT";
            this.lblMeterStartAt.Click += new System.EventHandler(this.label19_Click);
            // 
            // cbMorning
            // 
            this.cbMorning.AutoSize = true;
            this.cbMorning.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMorning.Location = new System.Drawing.Point(569, 26);
            this.cbMorning.Name = "cbMorning";
            this.cbMorning.Size = new System.Drawing.Size(97, 23);
            this.cbMorning.TabIndex = 201;
            this.cbMorning.Text = "MORNING";
            this.cbMorning.UseVisualStyleBackColor = true;
            this.cbMorning.CheckedChanged += new System.EventHandler(this.cbMorning_CheckedChanged);
            // 
            // cbNight
            // 
            this.cbNight.AutoSize = true;
            this.cbNight.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNight.Location = new System.Drawing.Point(695, 25);
            this.cbNight.Name = "cbNight";
            this.cbNight.Size = new System.Drawing.Size(72, 23);
            this.cbNight.TabIndex = 202;
            this.cbNight.Text = "NIGHT";
            this.cbNight.UseVisualStyleBackColor = true;
            this.cbNight.CheckedChanged += new System.EventHandler(this.cbNight_CheckedChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(697, 472);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(67, 19);
            this.label41.TabIndex = 203;
            this.label41.Text = "REJECT %";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(566, 472);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(94, 19);
            this.label42.TabIndex = 214;
            this.label42.Text = "TOTAL REJECT";
            // 
            // lblTotalStockIn
            // 
            this.lblTotalStockIn.AutoSize = true;
            this.lblTotalStockIn.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStockIn.Location = new System.Drawing.Point(697, 400);
            this.lblTotalStockIn.Name = "lblTotalStockIn";
            this.lblTotalStockIn.Size = new System.Drawing.Size(110, 19);
            this.lblTotalStockIn.TabIndex = 213;
            this.lblTotalStockIn.Text = "TOTAL STOCK IN";
            // 
            // lblBalanceCurrent
            // 
            this.lblBalanceCurrent.AutoSize = true;
            this.lblBalanceCurrent.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceCurrent.Location = new System.Drawing.Point(566, 328);
            this.lblBalanceCurrent.Name = "lblBalanceCurrent";
            this.lblBalanceCurrent.Size = new System.Drawing.Size(143, 19);
            this.lblBalanceCurrent.TabIndex = 211;
            this.lblBalanceCurrent.Text = "BALANCE(THIS SHIFT)";
            // 
            // lblBalanceLast
            // 
            this.lblBalanceLast.AutoSize = true;
            this.lblBalanceLast.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceLast.Location = new System.Drawing.Point(566, 261);
            this.lblBalanceLast.Name = "lblBalanceLast";
            this.lblBalanceLast.Size = new System.Drawing.Size(145, 19);
            this.lblBalanceLast.TabIndex = 209;
            this.lblBalanceLast.Text = "BALANCE(LAST SHIFT)";
            // 
            // dtpProDate
            // 
            this.dtpProDate.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpProDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProDate.Location = new System.Drawing.Point(359, 24);
            this.dtpProDate.Name = "dtpProDate";
            this.dtpProDate.Size = new System.Drawing.Size(177, 25);
            this.dtpProDate.TabIndex = 215;
            this.dtpProDate.ValueChanged += new System.EventHandler(this.dtpProDate_ValueChanged);
            // 
            // txtProLotNo
            // 
            this.txtProLotNo.BackColor = System.Drawing.SystemColors.Info;
            this.txtProLotNo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProLotNo.Location = new System.Drawing.Point(218, 24);
            this.txtProLotNo.Name = "txtProLotNo";
            this.txtProLotNo.Size = new System.Drawing.Size(135, 25);
            this.txtProLotNo.TabIndex = 216;
            this.txtProLotNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPackingQty
            // 
            this.txtPackingQty.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackingQty.Location = new System.Drawing.Point(17, 213);
            this.txtPackingQty.Name = "txtPackingQty";
            this.txtPackingQty.Size = new System.Drawing.Size(57, 25);
            this.txtPackingQty.TabIndex = 220;
            this.txtPackingQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPackingQty.TextChanged += new System.EventHandler(this.txtPackingQty_TextChanged);
            this.txtPackingQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPackingQty_KeyPress);
            // 
            // cmbPackingName
            // 
            this.cmbPackingName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPackingName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPackingName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPackingName.FormattingEnabled = true;
            this.cmbPackingName.Location = new System.Drawing.Point(116, 213);
            this.cmbPackingName.Name = "cmbPackingName";
            this.cmbPackingName.Size = new System.Drawing.Size(227, 25);
            this.cmbPackingName.TabIndex = 221;
            this.cmbPackingName.SelectedIndexChanged += new System.EventHandler(this.cmbPackingName_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(78, 216);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 19);
            this.label20.TabIndex = 222;
            this.label20.Text = "PCS/";
            // 
            // txtBalanceOfThisShift
            // 
            this.txtBalanceOfThisShift.BackColor = System.Drawing.SystemColors.Info;
            this.txtBalanceOfThisShift.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalanceOfThisShift.Location = new System.Drawing.Point(570, 350);
            this.txtBalanceOfThisShift.Name = "txtBalanceOfThisShift";
            this.txtBalanceOfThisShift.Size = new System.Drawing.Size(252, 25);
            this.txtBalanceOfThisShift.TabIndex = 225;
            this.txtBalanceOfThisShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBalanceOfThisShift.TextChanged += new System.EventHandler(this.txtBalanceOfThisShift_TextChanged);
            this.txtBalanceOfThisShift.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBalanceOfThisShift_KeyPress);
            // 
            // txtTotalReject
            // 
            this.txtTotalReject.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalReject.Location = new System.Drawing.Point(570, 494);
            this.txtTotalReject.Name = "txtTotalReject";
            this.txtTotalReject.Size = new System.Drawing.Size(121, 25);
            this.txtTotalReject.TabIndex = 227;
            this.txtTotalReject.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalReject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalReject_KeyPress);
            // 
            // txtFullBox
            // 
            this.txtFullBox.BackColor = System.Drawing.SystemColors.Info;
            this.txtFullBox.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullBox.Location = new System.Drawing.Point(570, 422);
            this.txtFullBox.Name = "txtFullBox";
            this.txtFullBox.Size = new System.Drawing.Size(121, 25);
            this.txtFullBox.TabIndex = 229;
            this.txtFullBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFullBox.TextChanged += new System.EventHandler(this.txtFullBox_TextChanged);
            this.txtFullBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFullBox_KeyPress);
            // 
            // txtRejectPercentage
            // 
            this.txtRejectPercentage.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRejectPercentage.Location = new System.Drawing.Point(701, 494);
            this.txtRejectPercentage.Name = "txtRejectPercentage";
            this.txtRejectPercentage.Size = new System.Drawing.Size(121, 25);
            this.txtRejectPercentage.TabIndex = 230;
            this.txtRejectPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRejectPercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRejectPercentage_KeyPress);
            // 
            // txtTotalStockIn
            // 
            this.txtTotalStockIn.BackColor = System.Drawing.SystemColors.Info;
            this.txtTotalStockIn.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalStockIn.Location = new System.Drawing.Point(701, 422);
            this.txtTotalStockIn.Name = "txtTotalStockIn";
            this.txtTotalStockIn.Size = new System.Drawing.Size(121, 25);
            this.txtTotalStockIn.TabIndex = 231;
            this.txtTotalStockIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalStockIn.TextChanged += new System.EventHandler(this.txtTotalStockIn_TextChanged);
            this.txtTotalStockIn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalStockIn_KeyPress);
            // 
            // lblFullCarton
            // 
            this.lblFullCarton.AutoSize = true;
            this.lblFullCarton.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullCarton.Location = new System.Drawing.Point(566, 400);
            this.lblFullCarton.Name = "lblFullCarton";
            this.lblFullCarton.Size = new System.Drawing.Size(96, 19);
            this.lblFullCarton.TabIndex = 232;
            this.lblFullCarton.Text = "FULL CARTON";
            // 
            // txtBalanceOfLastShift
            // 
            this.txtBalanceOfLastShift.BackColor = System.Drawing.SystemColors.Info;
            this.txtBalanceOfLastShift.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalanceOfLastShift.Location = new System.Drawing.Point(570, 283);
            this.txtBalanceOfLastShift.Name = "txtBalanceOfLastShift";
            this.txtBalanceOfLastShift.Size = new System.Drawing.Size(252, 25);
            this.txtBalanceOfLastShift.TabIndex = 233;
            this.txtBalanceOfLastShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBalanceOfLastShift.TextChanged += new System.EventHandler(this.txtBalanceOfLastShift_TextChanged);
            this.txtBalanceOfLastShift.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBalanceOfLastShift_KeyPress);
            // 
            // txtTotalShot
            // 
            this.txtTotalShot.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalShot.Location = new System.Drawing.Point(415, 723);
            this.txtTotalShot.Name = "txtTotalShot";
            this.txtTotalShot.Size = new System.Drawing.Size(121, 25);
            this.txtTotalShot.TabIndex = 235;
            this.txtTotalShot.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotalShot.TextChanged += new System.EventHandler(this.txtTotalShot_TextChanged);
            this.txtTotalShot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalShot_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(324, 723);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(85, 19);
            this.label22.TabIndex = 234;
            this.label22.Text = "TOTAL SHOT";
            // 
            // txtMeterStart
            // 
            this.txtMeterStart.BackColor = System.Drawing.SystemColors.Info;
            this.txtMeterStart.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMeterStart.Location = new System.Drawing.Point(569, 213);
            this.txtMeterStart.Name = "txtMeterStart";
            this.txtMeterStart.Size = new System.Drawing.Size(252, 25);
            this.txtMeterStart.TabIndex = 243;
            this.txtMeterStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMeterStart.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            this.txtMeterStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMeterStart_KeyPress);
            // 
            // txtColorMatLotNo
            // 
            this.txtColorMatLotNo.BackColor = System.Drawing.SystemColors.Info;
            this.txtColorMatLotNo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColorMatLotNo.Location = new System.Drawing.Point(569, 156);
            this.txtColorMatLotNo.Name = "txtColorMatLotNo";
            this.txtColorMatLotNo.Size = new System.Drawing.Size(252, 25);
            this.txtColorMatLotNo.TabIndex = 244;
            this.txtColorMatLotNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRawMatLotNo
            // 
            this.txtRawMatLotNo.BackColor = System.Drawing.SystemColors.Info;
            this.txtRawMatLotNo.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRawMatLotNo.Location = new System.Drawing.Point(569, 98);
            this.txtRawMatLotNo.Name = "txtRawMatLotNo";
            this.txtRawMatLotNo.Size = new System.Drawing.Size(252, 25);
            this.txtRawMatLotNo.TabIndex = 245;
            this.txtRawMatLotNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbPackingCode
            // 
            this.cmbPackingCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPackingCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPackingCode.FormattingEnabled = true;
            this.cmbPackingCode.Location = new System.Drawing.Point(349, 213);
            this.cmbPackingCode.Name = "cmbPackingCode";
            this.cmbPackingCode.Size = new System.Drawing.Size(187, 25);
            this.cmbPackingCode.TabIndex = 246;
            this.cmbPackingCode.SelectedIndexChanged += new System.EventHandler(this.cmbPackingCode_SelectedIndexChanged);
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.Location = new System.Drawing.Point(132, 66);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(0, 19);
            this.lblCustomer.TabIndex = 247;
            // 
            // lblPartName
            // 
            this.lblPartName.AutoSize = true;
            this.lblPartName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartName.Location = new System.Drawing.Point(132, 85);
            this.lblPartName.Name = "lblPartName";
            this.lblPartName.Size = new System.Drawing.Size(0, 19);
            this.lblPartName.TabIndex = 248;
            // 
            // lblPartCode
            // 
            this.lblPartCode.AutoSize = true;
            this.lblPartCode.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartCode.Location = new System.Drawing.Point(132, 106);
            this.lblPartCode.Name = "lblPartCode";
            this.lblPartCode.Size = new System.Drawing.Size(0, 19);
            this.lblPartCode.TabIndex = 249;
            // 
            // lblPW
            // 
            this.lblPW.AutoSize = true;
            this.lblPW.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPW.Location = new System.Drawing.Point(132, 125);
            this.lblPW.Name = "lblPW";
            this.lblPW.Size = new System.Drawing.Size(0, 19);
            this.lblPW.TabIndex = 250;
            // 
            // lblRawMat
            // 
            this.lblRawMat.AutoSize = true;
            this.lblRawMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRawMat.Location = new System.Drawing.Point(132, 144);
            this.lblRawMat.Name = "lblRawMat";
            this.lblRawMat.Size = new System.Drawing.Size(0, 19);
            this.lblRawMat.TabIndex = 251;
            // 
            // lblColorMat
            // 
            this.lblColorMat.AutoSize = true;
            this.lblColorMat.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorMat.Location = new System.Drawing.Point(132, 163);
            this.lblColorMat.Name = "lblColorMat";
            this.lblColorMat.Size = new System.Drawing.Size(0, 19);
            this.lblColorMat.TabIndex = 252;
            // 
            // lblCavity
            // 
            this.lblCavity.AutoSize = true;
            this.lblCavity.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCavity.Location = new System.Drawing.Point(411, 66);
            this.lblCavity.Name = "lblCavity";
            this.lblCavity.Size = new System.Drawing.Size(0, 19);
            this.lblCavity.TabIndex = 253;
            // 
            // lblRW
            // 
            this.lblRW.AutoSize = true;
            this.lblRW.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRW.Location = new System.Drawing.Point(411, 125);
            this.lblRW.Name = "lblRW";
            this.lblRW.Size = new System.Drawing.Size(0, 19);
            this.lblRW.TabIndex = 254;
            // 
            // lblColorUsage
            // 
            this.lblColorUsage.AutoSize = true;
            this.lblColorUsage.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorUsage.Location = new System.Drawing.Point(411, 163);
            this.lblColorUsage.Name = "lblColorUsage";
            this.lblColorUsage.Size = new System.Drawing.Size(0, 19);
            this.lblColorUsage.TabIndex = 255;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtPlanID);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtSheetID);
            this.panel1.Controls.Add(this.dgvMeterReading);
            this.panel1.Controls.Add(this.lblColorUsage);
            this.panel1.Controls.Add(this.btnSaveAndStock);
            this.panel1.Controls.Add(this.lblRW);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.lblCavity);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.lblColorMat);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblRawMat);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblPW);
            this.panel1.Controls.Add(this.lblPartNameHeader);
            this.panel1.Controls.Add(this.lblPartCode);
            this.panel1.Controls.Add(this.lblPartCodeHeader);
            this.panel1.Controls.Add(this.lblPartName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblCustomer);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbPackingCode);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtRawMatLotNo);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtColorMatLotNo);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtMeterStart);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.txtTotalShot);
            this.panel1.Controls.Add(this.lblShift);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtBalanceOfLastShift);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.lblFullCarton);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.txtTotalStockIn);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.txtRejectPercentage);
            this.panel1.Controls.Add(this.lblMeterStartAt);
            this.panel1.Controls.Add(this.txtFullBox);
            this.panel1.Controls.Add(this.cbMorning);
            this.panel1.Controls.Add(this.txtTotalReject);
            this.panel1.Controls.Add(this.cbNight);
            this.panel1.Controls.Add(this.txtBalanceOfThisShift);
            this.panel1.Controls.Add(this.label41);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.lblBalanceLast);
            this.panel1.Controls.Add(this.cmbPackingName);
            this.panel1.Controls.Add(this.lblBalanceCurrent);
            this.panel1.Controls.Add(this.txtPackingQty);
            this.panel1.Controls.Add(this.lblTotalStockIn);
            this.panel1.Controls.Add(this.txtProLotNo);
            this.panel1.Controls.Add(this.label42);
            this.panel1.Controls.Add(this.dtpProDate);
            this.panel1.Location = new System.Drawing.Point(3, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(842, 682);
            this.panel1.TabIndex = 256;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 19);
            this.label6.TabIndex = 259;
            this.label6.Text = "PLAN ID";
            // 
            // txtPlanID
            // 
            this.txtPlanID.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlanID.Location = new System.Drawing.Point(17, 24);
            this.txtPlanID.Name = "txtPlanID";
            this.txtPlanID.Size = new System.Drawing.Size(96, 25);
            this.txtPlanID.TabIndex = 258;
            this.txtPlanID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPlanID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlanID_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(112, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 19);
            this.label5.TabIndex = 257;
            this.label5.Text = "SHEET ID";
            // 
            // txtSheetID
            // 
            this.txtSheetID.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSheetID.Location = new System.Drawing.Point(116, 24);
            this.txtSheetID.Name = "txtSheetID";
            this.txtSheetID.Size = new System.Drawing.Size(96, 25);
            this.txtSheetID.TabIndex = 256;
            this.txtSheetID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSheetID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSheetID_KeyPress);
            // 
            // tlpSheet
            // 
            this.tlpSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpSheet.ColumnCount = 1;
            this.tlpSheet.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSheet.Controls.Add(this.btnNewSheet, 0, 0);
            this.tlpSheet.Controls.Add(this.panel1, 0, 1);
            this.tlpSheet.Location = new System.Drawing.Point(707, 27);
            this.tlpSheet.Name = "tlpSheet";
            this.tlpSheet.RowCount = 2;
            this.tlpSheet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.69036F));
            this.tlpSheet.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.30965F));
            this.tlpSheet.Size = new System.Drawing.Size(848, 788);
            this.tlpSheet.TabIndex = 257;
            this.tlpSheet.Paint += new System.Windows.Forms.PaintEventHandler(this.tlpSheet_Paint);
            // 
            // btnNewSheet
            // 
            this.btnNewSheet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNewSheet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnNewSheet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewSheet.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNewSheet.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewSheet.ForeColor = System.Drawing.Color.White;
            this.btnNewSheet.Location = new System.Drawing.Point(289, 1);
            this.btnNewSheet.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnNewSheet.Name = "btnNewSheet";
            this.btnNewSheet.Size = new System.Drawing.Size(269, 97);
            this.btnNewSheet.TabIndex = 257;
            this.btnNewSheet.Text = "ADD NEW SHEET";
            this.btnNewSheet.UseVisualStyleBackColor = false;
            this.btnNewSheet.Click += new System.EventHandler(this.btnNewSheet_Click);
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
            // errorProvider5
            // 
            this.errorProvider5.ContainerControl = this;
            // 
            // errorProvider6
            // 
            this.errorProvider6.ContainerControl = this;
            // 
            // errorProvider7
            // 
            this.errorProvider7.ContainerControl = this;
            // 
            // errorProvider8
            // 
            this.errorProvider8.ContainerControl = this;
            // 
            // errorProvider9
            // 
            this.errorProvider9.ContainerControl = this;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmProductionRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.tlpSheet);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvRecordHistory);
            this.Controls.Add(this.dgvItemList);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmProductionRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Production Record";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProductionRecord_FormClosed);
            this.Load += new System.EventHandler(this.frmProductionRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecordHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeterReading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tlpSheet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider9)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvItemList;
        private System.Windows.Forms.DataGridView dgvRecordHistory;
        private System.Windows.Forms.DataGridView dgvMeterReading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnSaveAndStock;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPartNameHeader;
        private System.Windows.Forms.Label lblPartCodeHeader;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblMeterStartAt;
        private System.Windows.Forms.CheckBox cbMorning;
        private System.Windows.Forms.CheckBox cbNight;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label lblTotalStockIn;
        private System.Windows.Forms.Label lblBalanceCurrent;
        private System.Windows.Forms.Label lblBalanceLast;
        private System.Windows.Forms.DateTimePicker dtpProDate;
        private System.Windows.Forms.TextBox txtProLotNo;
        private System.Windows.Forms.TextBox txtPackingQty;
        private System.Windows.Forms.ComboBox cmbPackingName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtBalanceOfThisShift;
        private System.Windows.Forms.TextBox txtTotalReject;
        private System.Windows.Forms.TextBox txtFullBox;
        private System.Windows.Forms.TextBox txtRejectPercentage;
        private System.Windows.Forms.TextBox txtTotalStockIn;
        private System.Windows.Forms.Label lblFullCarton;
        private System.Windows.Forms.TextBox txtBalanceOfLastShift;
        private System.Windows.Forms.TextBox txtTotalShot;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtMeterStart;
        private System.Windows.Forms.TextBox txtColorMatLotNo;
        private System.Windows.Forms.TextBox txtRawMatLotNo;
        private System.Windows.Forms.ComboBox cmbPackingCode;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblPartName;
        private System.Windows.Forms.Label lblPartCode;
        private System.Windows.Forms.Label lblPW;
        private System.Windows.Forms.Label lblRawMat;
        private System.Windows.Forms.Label lblColorMat;
        private System.Windows.Forms.Label lblCavity;
        private System.Windows.Forms.Label lblRW;
        private System.Windows.Forms.Label lblColorUsage;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TableLayoutPanel tlpSheet;
        private System.Windows.Forms.Button btnNewSheet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;
        private System.Windows.Forms.ErrorProvider errorProvider6;
        private System.Windows.Forms.ErrorProvider errorProvider7;
        private System.Windows.Forms.ErrorProvider errorProvider8;
        private System.Windows.Forms.ErrorProvider errorProvider9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPlanID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSheetID;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Timer timer1;
    }
}