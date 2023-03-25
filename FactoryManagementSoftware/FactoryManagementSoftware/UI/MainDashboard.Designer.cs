namespace FactoryManagementSoftware.UI
{
    partial class MainDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDashboard));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCustToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemJoinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.custToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newItemListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iTEMToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dAILYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.forecastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pMMAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sBBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deliveryReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forecastReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialUsedReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productionReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sBBDeliveredReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usernameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fORECASTREPORTToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminToolStripMenuItem,
            this.iTEMToolStripMenuItem1,
            this.dAILYToolStripMenuItem,
            this.productionToolStripMenuItem,
            this.stockToolStripMenuItem,
            this.orderToolStripMenuItem1,
            this.forecastToolStripMenuItem,
            this.pMMAToolStripMenuItem,
            this.sBBToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.pOToolStripMenuItem,
            this.usernameToolStripMenuItem,
            this.fORECASTREPORTToolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1348, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripMenuItem,
            this.itemToolStripMenuItem,
            this.itemCustToolStripMenuItem,
            this.itemJoinToolStripMenuItem,
            this.facToolStripMenuItem,
            this.custToolStripMenuItem,
            this.categoryToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.mouldToolStripMenuItem,
            this.newItemListToolStripMenuItem});
            this.adminToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.adminToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.adminToolStripMenuItem.Text = "ADMIN";
            this.adminToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.adminToolStripMenuItem.Click += new System.EventHandler(this.adminToolStripMenuItem_Click);
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.userToolStripMenuItem.Text = "User";
            this.userToolStripMenuItem.Click += new System.EventHandler(this.userToolStripMenuItem_Click);
            // 
            // itemToolStripMenuItem
            // 
            this.itemToolStripMenuItem.Name = "itemToolStripMenuItem";
            this.itemToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.itemToolStripMenuItem.Text = "Item";
            this.itemToolStripMenuItem.Click += new System.EventHandler(this.itemToolStripMenuItem_Click);
            // 
            // itemCustToolStripMenuItem
            // 
            this.itemCustToolStripMenuItem.Name = "itemCustToolStripMenuItem";
            this.itemCustToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.itemCustToolStripMenuItem.Text = "Item Cust";
            this.itemCustToolStripMenuItem.Click += new System.EventHandler(this.itemCustToolStripMenuItem_Click);
            // 
            // itemJoinToolStripMenuItem
            // 
            this.itemJoinToolStripMenuItem.Name = "itemJoinToolStripMenuItem";
            this.itemJoinToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.itemJoinToolStripMenuItem.Text = "Item Join";
            this.itemJoinToolStripMenuItem.Click += new System.EventHandler(this.itemJoinToolStripMenuItem_Click);
            // 
            // facToolStripMenuItem
            // 
            this.facToolStripMenuItem.Name = "facToolStripMenuItem";
            this.facToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.facToolStripMenuItem.Text = "Factory";
            this.facToolStripMenuItem.Click += new System.EventHandler(this.facToolStripMenuItem_Click);
            // 
            // custToolStripMenuItem
            // 
            this.custToolStripMenuItem.Name = "custToolStripMenuItem";
            this.custToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.custToolStripMenuItem.Text = "Customer";
            this.custToolStripMenuItem.Click += new System.EventHandler(this.custToolStripMenuItem_Click);
            // 
            // categoryToolStripMenuItem
            // 
            this.categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
            this.categoryToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.categoryToolStripMenuItem.Text = "Category";
            this.categoryToolStripMenuItem.Click += new System.EventHandler(this.categoryToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.historyToolStripMenuItem.Text = "History";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.dataToolStripMenuItem.Text = "Data";
            this.dataToolStripMenuItem.Click += new System.EventHandler(this.dataToolStripMenuItem_Click);
            // 
            // mouldToolStripMenuItem
            // 
            this.mouldToolStripMenuItem.Name = "mouldToolStripMenuItem";
            this.mouldToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.mouldToolStripMenuItem.Text = "Mould";
            this.mouldToolStripMenuItem.Click += new System.EventHandler(this.mouldToolStripMenuItem_Click);
            // 
            // newItemListToolStripMenuItem
            // 
            this.newItemListToolStripMenuItem.Name = "newItemListToolStripMenuItem";
            this.newItemListToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.newItemListToolStripMenuItem.Text = "New Item List";
            this.newItemListToolStripMenuItem.Click += new System.EventHandler(this.newItemListToolStripMenuItem_Click);
            // 
            // iTEMToolStripMenuItem1
            // 
            this.iTEMToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.iTEMToolStripMenuItem1.Name = "iTEMToolStripMenuItem1";
            this.iTEMToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.iTEMToolStripMenuItem1.Text = "ITEM";
            this.iTEMToolStripMenuItem1.Click += new System.EventHandler(this.iTEMToolStripMenuItem1_Click);
            // 
            // dAILYToolStripMenuItem
            // 
            this.dAILYToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dAILYToolStripMenuItem.Name = "dAILYToolStripMenuItem";
            this.dAILYToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dAILYToolStripMenuItem.Text = "DAILY";
            this.dAILYToolStripMenuItem.Click += new System.EventHandler(this.dAILYToolStripMenuItem_Click);
            // 
            // productionToolStripMenuItem
            // 
            this.productionToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.productionToolStripMenuItem.Name = "productionToolStripMenuItem";
            this.productionToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.productionToolStripMenuItem.Text = "PRODUCTION";
            this.productionToolStripMenuItem.Click += new System.EventHandler(this.productionToolStripMenuItem_Click);
            // 
            // stockToolStripMenuItem
            // 
            this.stockToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.stockToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.stockToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.stockToolStripMenuItem.Name = "stockToolStripMenuItem";
            this.stockToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.stockToolStripMenuItem.Text = "STOCK";
            this.stockToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.stockToolStripMenuItem.Click += new System.EventHandler(this.stockToolStripMenuItem_Click);
            // 
            // orderToolStripMenuItem1
            // 
            this.orderToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.orderToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.orderToolStripMenuItem1.Name = "orderToolStripMenuItem1";
            this.orderToolStripMenuItem1.Size = new System.Drawing.Size(56, 20);
            this.orderToolStripMenuItem1.Text = "ORDER";
            this.orderToolStripMenuItem1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.orderToolStripMenuItem1.Click += new System.EventHandler(this.orderToolStripMenuItem1_Click);
            // 
            // forecastToolStripMenuItem
            // 
            this.forecastToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.forecastToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.forecastToolStripMenuItem.Name = "forecastToolStripMenuItem";
            this.forecastToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.forecastToolStripMenuItem.Text = "PO / FORECAST";
            this.forecastToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.forecastToolStripMenuItem.Click += new System.EventHandler(this.forecastToolStripMenuItem_Click);
            // 
            // pMMAToolStripMenuItem
            // 
            this.pMMAToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.pMMAToolStripMenuItem.Name = "pMMAToolStripMenuItem";
            this.pMMAToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.pMMAToolStripMenuItem.Text = "PMMA";
            this.pMMAToolStripMenuItem.Click += new System.EventHandler(this.pMMAToolStripMenuItem_Click);
            // 
            // sBBToolStripMenuItem
            // 
            this.sBBToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.sBBToolStripMenuItem.Name = "sBBToolStripMenuItem";
            this.sBBToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.sBBToolStripMenuItem.Text = "SBB";
            this.sBBToolStripMenuItem.Click += new System.EventHandler(this.sPPToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stockReportToolStripMenuItem,
            this.deliveryReportToolStripMenuItem,
            this.forecastReportToolStripMenuItem,
            this.materialUsedReportToolStripMenuItem,
            this.productionReportToolStripMenuItem,
            this.sBBDeliveredReportToolStripMenuItem});
            this.reportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.reportToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.reportToolStripMenuItem.Text = "REPORT";
            this.reportToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // stockReportToolStripMenuItem
            // 
            this.stockReportToolStripMenuItem.Name = "stockReportToolStripMenuItem";
            this.stockReportToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.stockReportToolStripMenuItem.Text = "Stock Report";
            this.stockReportToolStripMenuItem.Click += new System.EventHandler(this.stockReportToolStripMenuItem_Click);
            // 
            // deliveryReportToolStripMenuItem
            // 
            this.deliveryReportToolStripMenuItem.Name = "deliveryReportToolStripMenuItem";
            this.deliveryReportToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.deliveryReportToolStripMenuItem.Text = "In Out Report";
            this.deliveryReportToolStripMenuItem.Click += new System.EventHandler(this.inOutReportToolStripMenuItem_Click);
            // 
            // forecastReportToolStripMenuItem
            // 
            this.forecastReportToolStripMenuItem.Name = "forecastReportToolStripMenuItem";
            this.forecastReportToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.forecastReportToolStripMenuItem.Text = "Forecast Report";
            this.forecastReportToolStripMenuItem.Click += new System.EventHandler(this.forecastReportToolStripMenuItem_Click);
            // 
            // materialUsedReportToolStripMenuItem
            // 
            this.materialUsedReportToolStripMenuItem.Name = "materialUsedReportToolStripMenuItem";
            this.materialUsedReportToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.materialUsedReportToolStripMenuItem.Text = "Material Used Report";
            this.materialUsedReportToolStripMenuItem.Click += new System.EventHandler(this.materialUsedReportToolStripMenuItem_Click);
            // 
            // productionReportToolStripMenuItem
            // 
            this.productionReportToolStripMenuItem.Name = "productionReportToolStripMenuItem";
            this.productionReportToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.productionReportToolStripMenuItem.Text = "Production Report";
            this.productionReportToolStripMenuItem.Click += new System.EventHandler(this.productionReportToolStripMenuItem_Click);
            // 
            // sBBDeliveredReportToolStripMenuItem
            // 
            this.sBBDeliveredReportToolStripMenuItem.Name = "sBBDeliveredReportToolStripMenuItem";
            this.sBBDeliveredReportToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.sBBDeliveredReportToolStripMenuItem.Text = "SBB Delivered Report";
            this.sBBDeliveredReportToolStripMenuItem.Click += new System.EventHandler(this.sBBDeliveredReportToolStripMenuItem_Click);
            // 
            // pOToolStripMenuItem
            // 
            this.pOToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.pOToolStripMenuItem.Name = "pOToolStripMenuItem";
            this.pOToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
            this.pOToolStripMenuItem.Text = "PO";
            this.pOToolStripMenuItem.Click += new System.EventHandler(this.pOToolStripMenuItem_Click);
            // 
            // usernameToolStripMenuItem
            // 
            this.usernameToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.usernameToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameToolStripMenuItem.Name = "usernameToolStripMenuItem";
            this.usernameToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.usernameToolStripMenuItem.Text = "username";
            this.usernameToolStripMenuItem.Click += new System.EventHandler(this.usernameToolStripMenuItem_Click);
            // 
            // fORECASTREPORTToolStripMenuItem1
            // 
            this.fORECASTREPORTToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.fORECASTREPORTToolStripMenuItem1.Name = "fORECASTREPORTToolStripMenuItem1";
            this.fORECASTREPORTToolStripMenuItem1.Size = new System.Drawing.Size(117, 20);
            this.fORECASTREPORTToolStripMenuItem1.Text = "FORECAST REPORT";
            this.fORECASTREPORTToolStripMenuItem1.Click += new System.EventHandler(this.forecastReportToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 699);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1348, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // toolTip
            // 
            this.toolTip.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip_Popup);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // MainDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1348, 721);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainDashboard_FormClosed);
            this.Load += new System.EventHandler(this.MainDashboard_Load);
            this.Move += new System.EventHandler(this.MainDashboard_Move);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem custToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forecastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemJoinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem categoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemCustToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forecastReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem materialUsedReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deliveryReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pMMAToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem productionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dAILYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productionReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sBBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sBBDeliveredReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newItemListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iTEMToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem usernameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fORECASTREPORTToolStripMenuItem1;
    }
}



