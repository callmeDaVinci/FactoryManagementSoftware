namespace FactoryManagementSoftware.UI
{
    partial class frmTimeNeededSetting
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
            this.txtManpower = new System.Windows.Forms.TextBox();
            this.txtPcsPerManHour = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHoursPerDay = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtManpower
            // 
            this.txtManpower.BackColor = System.Drawing.SystemColors.Info;
            this.txtManpower.Location = new System.Drawing.Point(31, 73);
            this.txtManpower.Name = "txtManpower";
            this.txtManpower.Size = new System.Drawing.Size(54, 25);
            this.txtManpower.TabIndex = 187;
            this.txtManpower.Text = "4";
            this.txtManpower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPcsPerManHour
            // 
            this.txtPcsPerManHour.BackColor = System.Drawing.SystemColors.Info;
            this.txtPcsPerManHour.Location = new System.Drawing.Point(31, 31);
            this.txtPcsPerManHour.Name = "txtPcsPerManHour";
            this.txtPcsPerManHour.Size = new System.Drawing.Size(54, 25);
            this.txtPcsPerManHour.TabIndex = 186;
            this.txtPcsPerManHour.Text = "100";
            this.txtPcsPerManHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(90, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 19);
            this.label1.TabIndex = 189;
            this.label1.Text = "pcs/man hour";
            // 
            // txtHoursPerDay
            // 
            this.txtHoursPerDay.BackColor = System.Drawing.SystemColors.Info;
            this.txtHoursPerDay.Location = new System.Drawing.Point(31, 117);
            this.txtHoursPerDay.Name = "txtHoursPerDay";
            this.txtHoursPerDay.Size = new System.Drawing.Size(54, 25);
            this.txtHoursPerDay.TabIndex = 188;
            this.txtHoursPerDay.Text = "10";
            this.txtHoursPerDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(93, 120);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 19);
            this.label4.TabIndex = 191;
            this.label4.Text = "hours/day";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(90, 76);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 190;
            this.label3.Text = "manpower";
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(139)))), ((int)(((byte)(209)))));
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(13, 185);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(194, 35);
            this.btnConfirm.TabIndex = 192;
            this.btnConfirm.Text = "CONFIRM";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(13, 226);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 1, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(194, 35);
            this.btnCancel.TabIndex = 193;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmTimeNeededSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(220, 275);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtManpower);
            this.Controls.Add(this.txtPcsPerManHour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHoursPerDay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTimeNeededSetting";
            this.Text = "SETTING";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTimeNeededSetting_FormClosing);
            this.Shown += new System.EventHandler(this.frmTimeNeededSetting_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtManpower;
        private System.Windows.Forms.TextBox txtPcsPerManHour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHoursPerDay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}