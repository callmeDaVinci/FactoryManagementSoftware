namespace FactoryManagementSoftware.UI
{
    partial class frmInOut
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTrf = new System.Windows.Forms.DataGridView();
            this.trf_hist_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_added_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_trf_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_hist_added_by = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trf_result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFactoryStock = new System.Windows.Forms.DataGridView();
            this.fac_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvItem = new System.Windows.Forms.DataGridView();
            this.item_cat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_ord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvTotal = new System.Windows.Forms.DataGridView();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSearchCat = new System.Windows.Forms.ComboBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactoryStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTrf
            // 
            this.dgvTrf.AllowUserToAddRows = false;
            this.dgvTrf.AllowUserToDeleteRows = false;
            this.dgvTrf.AllowUserToResizeRows = false;
            this.dgvTrf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTrf.BackgroundColor = System.Drawing.Color.White;
            this.dgvTrf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTrf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrf.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trf_hist_id,
            this.trf_hist_added_date,
            this.trf_hist_trf_date,
            this.trf_hist_item_code,
            this.trf_hist_item_name,
            this.trf_hist_from,
            this.trf_hist_to,
            this.trf_hist_qty,
            this.trf_hist_unit,
            this.trf_hist_note,
            this.trf_hist_added_by,
            this.trf_result});
            this.dgvTrf.GridColor = System.Drawing.SystemColors.Control;
            this.dgvTrf.Location = new System.Drawing.Point(36, 483);
            this.dgvTrf.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTrf.Name = "dgvTrf";
            this.dgvTrf.ReadOnly = true;
            this.dgvTrf.RowHeadersVisible = false;
            this.dgvTrf.RowTemplate.Height = 40;
            this.dgvTrf.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrf.Size = new System.Drawing.Size(1511, 376);
            this.dgvTrf.TabIndex = 37;
            this.dgvTrf.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTrf_CellMouseDown);
            this.dgvTrf.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvTrf_MouseClick);
            // 
            // trf_hist_id
            // 
            this.trf_hist_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_id.DefaultCellStyle = dataGridViewCellStyle1;
            this.trf_hist_id.HeaderText = "ID";
            this.trf_hist_id.Name = "trf_hist_id";
            this.trf_hist_id.ReadOnly = true;
            this.trf_hist_id.Width = 56;
            // 
            // trf_hist_added_date
            // 
            this.trf_hist_added_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_added_date.DefaultCellStyle = dataGridViewCellStyle2;
            this.trf_hist_added_date.HeaderText = "Added_Date";
            this.trf_hist_added_date.Name = "trf_hist_added_date";
            this.trf_hist_added_date.ReadOnly = true;
            this.trf_hist_added_date.Width = 132;
            // 
            // trf_hist_trf_date
            // 
            this.trf_hist_trf_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            dataGridViewCellStyle3.NullValue = null;
            this.trf_hist_trf_date.DefaultCellStyle = dataGridViewCellStyle3;
            this.trf_hist_trf_date.HeaderText = "Trf_Date";
            this.trf_hist_trf_date.Name = "trf_hist_trf_date";
            this.trf_hist_trf_date.ReadOnly = true;
            this.trf_hist_trf_date.Width = 101;
            // 
            // trf_hist_item_code
            // 
            this.trf_hist_item_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_item_code.DefaultCellStyle = dataGridViewCellStyle4;
            this.trf_hist_item_code.HeaderText = "Code";
            this.trf_hist_item_code.Name = "trf_hist_item_code";
            this.trf_hist_item_code.ReadOnly = true;
            // 
            // trf_hist_item_name
            // 
            this.trf_hist_item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_item_name.DefaultCellStyle = dataGridViewCellStyle5;
            this.trf_hist_item_name.HeaderText = "Name";
            this.trf_hist_item_name.Name = "trf_hist_item_name";
            this.trf_hist_item_name.ReadOnly = true;
            // 
            // trf_hist_from
            // 
            this.trf_hist_from.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_from.DefaultCellStyle = dataGridViewCellStyle6;
            this.trf_hist_from.HeaderText = "From";
            this.trf_hist_from.Name = "trf_hist_from";
            this.trf_hist_from.ReadOnly = true;
            this.trf_hist_from.Width = 78;
            // 
            // trf_hist_to
            // 
            this.trf_hist_to.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_to.DefaultCellStyle = dataGridViewCellStyle7;
            this.trf_hist_to.HeaderText = "To";
            this.trf_hist_to.Name = "trf_hist_to";
            this.trf_hist_to.ReadOnly = true;
            this.trf_hist_to.Width = 56;
            // 
            // trf_hist_qty
            // 
            this.trf_hist_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_qty.DefaultCellStyle = dataGridViewCellStyle8;
            this.trf_hist_qty.HeaderText = "Qty";
            this.trf_hist_qty.Name = "trf_hist_qty";
            this.trf_hist_qty.ReadOnly = true;
            this.trf_hist_qty.Width = 66;
            // 
            // trf_hist_unit
            // 
            this.trf_hist_unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_unit.DefaultCellStyle = dataGridViewCellStyle9;
            this.trf_hist_unit.HeaderText = "Unit";
            this.trf_hist_unit.Name = "trf_hist_unit";
            this.trf_hist_unit.ReadOnly = true;
            this.trf_hist_unit.Width = 71;
            // 
            // trf_hist_note
            // 
            this.trf_hist_note.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.trf_hist_note.HeaderText = "Note";
            this.trf_hist_note.Name = "trf_hist_note";
            this.trf_hist_note.ReadOnly = true;
            this.trf_hist_note.Width = 77;
            // 
            // trf_hist_added_by
            // 
            this.trf_hist_added_by.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.trf_hist_added_by.DefaultCellStyle = dataGridViewCellStyle10;
            this.trf_hist_added_by.HeaderText = "By";
            this.trf_hist_added_by.Name = "trf_hist_added_by";
            this.trf_hist_added_by.ReadOnly = true;
            this.trf_hist_added_by.Width = 57;
            // 
            // trf_result
            // 
            this.trf_result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.trf_result.HeaderText = "Result";
            this.trf_result.Name = "trf_result";
            this.trf_result.ReadOnly = true;
            this.trf_result.Width = 85;
            // 
            // dgvFactoryStock
            // 
            this.dgvFactoryStock.AllowUserToAddRows = false;
            this.dgvFactoryStock.AllowUserToDeleteRows = false;
            this.dgvFactoryStock.AllowUserToResizeRows = false;
            this.dgvFactoryStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFactoryStock.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFactoryStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvFactoryStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFactoryStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fac_name,
            this.stock_qty});
            this.dgvFactoryStock.EnableHeadersVisualStyles = false;
            this.dgvFactoryStock.GridColor = System.Drawing.SystemColors.Control;
            this.dgvFactoryStock.Location = new System.Drawing.Point(1230, 171);
            this.dgvFactoryStock.Margin = new System.Windows.Forms.Padding(2);
            this.dgvFactoryStock.Name = "dgvFactoryStock";
            this.dgvFactoryStock.ReadOnly = true;
            this.dgvFactoryStock.RowHeadersVisible = false;
            this.dgvFactoryStock.RowTemplate.Height = 40;
            this.dgvFactoryStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFactoryStock.Size = new System.Drawing.Size(317, 199);
            this.dgvFactoryStock.TabIndex = 56;
            this.dgvFactoryStock.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFactoryStock_CellDoubleClick);
            this.dgvFactoryStock.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvFactoryStock_MouseClick);
            // 
            // fac_name
            // 
            this.fac_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fac_name.HeaderText = "Factory";
            this.fac_name.Name = "fac_name";
            this.fac_name.ReadOnly = true;
            // 
            // stock_qty
            // 
            this.stock_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.stock_qty.HeaderText = "Qty";
            this.stock_qty.Name = "stock_qty";
            this.stock_qty.ReadOnly = true;
            this.stock_qty.Width = 66;
            // 
            // dgvItem
            // 
            this.dgvItem.AllowUserToAddRows = false;
            this.dgvItem.AllowUserToDeleteRows = false;
            this.dgvItem.AllowUserToResizeRows = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.dgvItem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItem.BackgroundColor = System.Drawing.Color.White;
            this.dgvItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item_cat,
            this.item_code,
            this.item_name,
            this.item_ord,
            this.item_qty});
            this.dgvItem.GridColor = System.Drawing.SystemColors.Control;
            this.dgvItem.Location = new System.Drawing.Point(36, 139);
            this.dgvItem.Margin = new System.Windows.Forms.Padding(2);
            this.dgvItem.MultiSelect = false;
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.ReadOnly = true;
            this.dgvItem.RowHeadersVisible = false;
            this.dgvItem.RowTemplate.Height = 40;
            this.dgvItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItem.Size = new System.Drawing.Size(1169, 307);
            this.dgvItem.TabIndex = 58;
            this.dgvItem.SelectionChanged += new System.EventHandler(this.dgvItem_SelectionChanged);
            this.dgvItem.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvItem_SortCompare);
            this.dgvItem.Sorted += new System.EventHandler(this.dgvItem_Sorted);
            this.dgvItem.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvItem_MouseClick);
            this.dgvItem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvItem_MouseDoubleClick);
            // 
            // item_cat
            // 
            this.item_cat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            this.item_cat.DefaultCellStyle = dataGridViewCellStyle14;
            this.item_cat.HeaderText = "Category";
            this.item_cat.Name = "item_cat";
            this.item_cat.ReadOnly = true;
            // 
            // item_code
            // 
            this.item_code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            this.item_code.DefaultCellStyle = dataGridViewCellStyle15;
            this.item_code.HeaderText = "Code";
            this.item_code.Name = "item_code";
            this.item_code.ReadOnly = true;
            // 
            // item_name
            // 
            this.item_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            this.item_name.DefaultCellStyle = dataGridViewCellStyle16;
            this.item_name.HeaderText = "Name";
            this.item_name.Name = "item_name";
            this.item_name.ReadOnly = true;
            // 
            // item_ord
            // 
            this.item_ord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            this.item_ord.DefaultCellStyle = dataGridViewCellStyle17;
            this.item_ord.HeaderText = "Order Pending";
            this.item_ord.Name = "item_ord";
            this.item_ord.ReadOnly = true;
            this.item_ord.Width = 150;
            // 
            // item_qty
            // 
            this.item_qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            this.item_qty.DefaultCellStyle = dataGridViewCellStyle18;
            this.item_qty.HeaderText = "Qty";
            this.item_qty.Name = "item_qty";
            this.item_qty.ReadOnly = true;
            this.item_qty.Width = 66;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.txtSearch.Location = new System.Drawing.Point(386, 55);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(319, 38);
            this.txtSearch.TabIndex = 60;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(382, 29);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(72, 23);
            this.lblSearch.TabIndex = 59;
            this.lblSearch.Text = "SEARCH";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(20)))), ((int)(((byte)(31)))));
            this.label2.Location = new System.Drawing.Point(1226, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 23);
            this.label2.TabIndex = 61;
            this.label2.Text = "QTY EACH FACTORY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(32, 458);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 23);
            this.label3.TabIndex = 62;
            this.label3.Text = "TRANSFER HISTORY";
            // 
            // dgvTotal
            // 
            this.dgvTotal.AllowUserToAddRows = false;
            this.dgvTotal.AllowUserToDeleteRows = false;
            this.dgvTotal.AllowUserToResizeRows = false;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTotal.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTotal.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTotal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTotal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Total});
            this.dgvTotal.EnableHeadersVisualStyles = false;
            this.dgvTotal.GridColor = System.Drawing.SystemColors.Control;
            this.dgvTotal.Location = new System.Drawing.Point(1230, 377);
            this.dgvTotal.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTotal.MultiSelect = false;
            this.dgvTotal.Name = "dgvTotal";
            this.dgvTotal.ReadOnly = true;
            this.dgvTotal.RowHeadersVisible = false;
            this.dgvTotal.RowTemplate.Height = 40;
            this.dgvTotal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTotal.Size = new System.Drawing.Size(317, 69);
            this.dgvTotal.TabIndex = 66;
            // 
            // Total
            // 
            this.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Total.DefaultCellStyle = dataGridViewCellStyle21;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransfer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransfer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransfer.ForeColor = System.Drawing.Color.White;
            this.btnTransfer.Location = new System.Drawing.Point(1283, 41);
            this.btnTransfer.Margin = new System.Windows.Forms.Padding(2);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(122, 52);
            this.btnTransfer.TabIndex = 67;
            this.btnTransfer.Text = "TRANSFER";
            this.btnTransfer.UseVisualStyleBackColor = false;
            this.btnTransfer.Click += new System.EventHandler(this.transfer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 23);
            this.label1.TabIndex = 69;
            this.label1.Text = "CATEGORY";
            // 
            // cmbSearchCat
            // 
            this.cmbSearchCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchCat.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchCat.FormattingEnabled = true;
            this.cmbSearchCat.Location = new System.Drawing.Point(36, 54);
            this.cmbSearchCat.Name = "cmbSearchCat";
            this.cmbSearchCat.Size = new System.Drawing.Size(325, 39);
            this.cmbSearchCat.TabIndex = 70;
            this.cmbSearchCat.SelectedIndexChanged += new System.EventHandler(this.cmbSearchCat_SelectedIndexChanged);
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnReport.Location = new System.Drawing.Point(1427, 41);
            this.btnReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(120, 50);
            this.btnReport.TabIndex = 73;
            this.btnReport.Text = "REPORT";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(32, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 23);
            this.label5.TabIndex = 74;
            this.label5.Text = "ITEM INFORMATION";
            // 
            // frmInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.cmbSearchCat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.dgvTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dgvItem);
            this.Controls.Add(this.dgvFactoryStock);
            this.Controls.Add(this.dgvTrf);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmInOut";
            this.Text = "Stock";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInOut_FormClosed);
            this.Load += new System.EventHandler(this.frmInOut_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmInOut_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactoryStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvTrf;
        private System.Windows.Forms.DataGridView dgvFactoryStock;
        private System.Windows.Forms.DataGridView dgvItem;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn fac_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock_qty;
        private System.Windows.Forms.DataGridView dgvTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.ComboBox cmbSearchCat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_added_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_trf_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_from;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_note;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_hist_added_by;
        private System.Windows.Forms.DataGridViewTextBoxColumn trf_result;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_cat;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_ord;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_qty;
    }
}