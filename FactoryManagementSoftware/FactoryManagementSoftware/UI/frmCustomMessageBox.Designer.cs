namespace FactoryManagementSoftware.UI
{
    partial class frmCustomMessageBox
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
            this.btnBackToSchedule = new System.Windows.Forms.Button();
            this.btnContinuePlanning = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBackToSchedule
            // 
            this.btnBackToSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackToSchedule.BackColor = System.Drawing.Color.White;
            this.btnBackToSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToSchedule.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToSchedule.ForeColor = System.Drawing.Color.Black;
            this.btnBackToSchedule.Location = new System.Drawing.Point(11, 154);
            this.btnBackToSchedule.Margin = new System.Windows.Forms.Padding(2);
            this.btnBackToSchedule.Name = "btnBackToSchedule";
            this.btnBackToSchedule.Size = new System.Drawing.Size(358, 35);
            this.btnBackToSchedule.TabIndex = 115;
            this.btnBackToSchedule.Text = "BACK TO SCHEDULE PAGE";
            this.btnBackToSchedule.UseVisualStyleBackColor = false;
            this.btnBackToSchedule.Click += new System.EventHandler(this.btnBackToSchedule_Click);
            // 
            // btnContinuePlanning
            // 
            this.btnContinuePlanning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinuePlanning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnContinuePlanning.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnContinuePlanning.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinuePlanning.ForeColor = System.Drawing.Color.Black;
            this.btnContinuePlanning.Location = new System.Drawing.Point(11, 104);
            this.btnContinuePlanning.Margin = new System.Windows.Forms.Padding(2);
            this.btnContinuePlanning.Name = "btnContinuePlanning";
            this.btnContinuePlanning.Size = new System.Drawing.Size(358, 35);
            this.btnContinuePlanning.TabIndex = 116;
            this.btnContinuePlanning.Text = "ADD ANOTHER NEW PLAN";
            this.btnContinuePlanning.UseVisualStyleBackColor = false;
            this.btnContinuePlanning.Click += new System.EventHandler(this.btnContinuePlanning_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(12, 28);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(126, 28);
            this.lblMessage.TabIndex = 117;
            this.lblMessage.Text = "Plan applied!";
            // 
            // frmCustomMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 209);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnContinuePlanning);
            this.Controls.Add(this.btnBackToSchedule);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmCustomMessageBox";
            this.Text = "Message";
            this.Load += new System.EventHandler(this.frmCustomMessageBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBackToSchedule;
        private System.Windows.Forms.Button btnContinuePlanning;
        private System.Windows.Forms.Label lblMessage;
    }
}