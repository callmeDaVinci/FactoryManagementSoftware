namespace FactoryManagementSoftware.UI
{
    partial class frmTargetQtyConfirm
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
            this.txtAbleToProduceQty = new System.Windows.Forms.TextBox();
            this.txtTargetQty = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAbleToProduceQty
            // 
            this.txtAbleToProduceQty.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAbleToProduceQty.Location = new System.Drawing.Point(23, 169);
            this.txtAbleToProduceQty.Name = "txtAbleToProduceQty";
            this.txtAbleToProduceQty.Size = new System.Drawing.Size(380, 38);
            this.txtAbleToProduceQty.TabIndex = 19;
            this.txtAbleToProduceQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAbleToProduceQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAbleToProduceQty_KeyPress);
            // 
            // txtTargetQty
            // 
            this.txtTargetQty.BackColor = System.Drawing.SystemColors.Info;
            this.txtTargetQty.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTargetQty.Location = new System.Drawing.Point(23, 82);
            this.txtTargetQty.Name = "txtTargetQty";
            this.txtTargetQty.Size = new System.Drawing.Size(380, 34);
            this.txtTargetQty.TabIndex = 17;
            this.txtTargetQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTargetQty.TextChanged += new System.EventHandler(this.txtTargetQty_TextChanged);
            this.txtTargetQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTargetQty_KeyDown);
            this.txtTargetQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTargetQty_KeyPress);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(18, 61);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(85, 19);
            this.label27.TabIndex = 18;
            this.label27.Text = "TARGET QTY";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(19, 147);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(128, 19);
            this.label28.TabIndex = 20;
            this.label28.Text = "ABLE TO PRODUCE";
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(283, 254);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(120, 50);
            this.btnCheck.TabIndex = 105;
            this.btnCheck.Text = "CONFIRM";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(23, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 52);
            this.label1.TabIndex = 106;
            this.label1.Text = "Please confirm the target quantity before starting material stock check :";
            // 
            // frmTargetQtyConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(428, 315);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAbleToProduceQty);
            this.Controls.Add(this.txtTargetQty);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label28);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmTargetQtyConfirm";
            this.Text = "Quantity Confirm";
            this.Load += new System.EventHandler(this.frmTargetQtyConfirm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtAbleToProduceQty;
        private System.Windows.Forms.TextBox txtTargetQty;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label1;
    }
}