namespace FactoryManagementSoftware.UI
{
    partial class frmDialog
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
            this.lblText = new System.Windows.Forms.Label();
            this.btnIns = new System.Windows.Forms.Button();
            this.btnTube = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.Location = new System.Drawing.Point(36, 30);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(152, 28);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "Type of coil top:";
            // 
            // btnIns
            // 
            this.btnIns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnIns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIns.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIns.ForeColor = System.Drawing.Color.White;
            this.btnIns.Location = new System.Drawing.Point(159, 95);
            this.btnIns.Margin = new System.Windows.Forms.Padding(2);
            this.btnIns.Name = "btnIns";
            this.btnIns.Size = new System.Drawing.Size(82, 40);
            this.btnIns.TabIndex = 105;
            this.btnIns.Text = "INS";
            this.btnIns.UseVisualStyleBackColor = false;
            this.btnIns.Click += new System.EventHandler(this.btnIns_Click);
            // 
            // btnTube
            // 
            this.btnTube.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTube.BackColor = System.Drawing.Color.Transparent;
            this.btnTube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTube.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTube.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(41)))), ((int)(((byte)(62)))));
            this.btnTube.Location = new System.Drawing.Point(41, 96);
            this.btnTube.Margin = new System.Windows.Forms.Padding(2);
            this.btnTube.Name = "btnTube";
            this.btnTube.Size = new System.Drawing.Size(80, 38);
            this.btnTube.TabIndex = 106;
            this.btnTube.Text = "TUBE";
            this.btnTube.UseVisualStyleBackColor = false;
            this.btnTube.Click += new System.EventHandler(this.btnTube_Click);
            // 
            // frmDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(293, 157);
            this.Controls.Add(this.btnIns);
            this.Controls.Add(this.btnTube);
            this.Controls.Add(this.lblText);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button btnIns;
        private System.Windows.Forms.Button btnTube;
    }
}