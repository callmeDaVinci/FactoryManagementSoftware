namespace FactoryManagementSoftware.UI
{
    partial class frmPlanningType
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
            this.btnAssemblyLine = new System.Windows.Forms.Button();
            this.btnMoldingMachine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAssemblyLine
            // 
            this.btnAssemblyLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnAssemblyLine.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAssemblyLine.ForeColor = System.Drawing.Color.Black;
            this.btnAssemblyLine.Image = global::FactoryManagementSoftware.Properties.Resources.Picture4;
            this.btnAssemblyLine.Location = new System.Drawing.Point(10, 182);
            this.btnAssemblyLine.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnAssemblyLine.Name = "btnAssemblyLine";
            this.btnAssemblyLine.Size = new System.Drawing.Size(273, 167);
            this.btnAssemblyLine.TabIndex = 148;
            this.btnAssemblyLine.Text = "ASSEMBLY LINE";
            this.btnAssemblyLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAssemblyLine.UseVisualStyleBackColor = false;
            this.btnAssemblyLine.Click += new System.EventHandler(this.btnAssemblyLine_Click);
            // 
            // btnMoldingMachine
            // 
            this.btnMoldingMachine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(203)))), ((int)(((byte)(110)))));
            this.btnMoldingMachine.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoldingMachine.ForeColor = System.Drawing.Color.Black;
            this.btnMoldingMachine.Image = global::FactoryManagementSoftware.Properties.Resources.Picture3;
            this.btnMoldingMachine.Location = new System.Drawing.Point(10, 11);
            this.btnMoldingMachine.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnMoldingMachine.Name = "btnMoldingMachine";
            this.btnMoldingMachine.Size = new System.Drawing.Size(273, 167);
            this.btnMoldingMachine.TabIndex = 147;
            this.btnMoldingMachine.Text = "MOLDING MACHINE";
            this.btnMoldingMachine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMoldingMachine.UseVisualStyleBackColor = false;
            this.btnMoldingMachine.Click += new System.EventHandler(this.btnMoldingMachine_Click);
            // 
            // frmPlanningType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(293, 360);
            this.Controls.Add(this.btnAssemblyLine);
            this.Controls.Add(this.btnMoldingMachine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlanningType";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLANNING TYPE";
            this.Load += new System.EventHandler(this.frmPlanningType_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnMoldingMachine;
        private System.Windows.Forms.Button btnAssemblyLine;
    }
}