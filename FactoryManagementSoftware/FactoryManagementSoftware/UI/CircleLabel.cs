using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public class CircleLabel : Label
    {
        public Color CircleBackColor { get; set; } = Color.White; // Default color

        public CircleLabel()
        {
            this.DoubleBuffered = true; // Helps to avoid flicker
            this.BackColor = Color.Transparent; // The entire control's background is transparent
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Create a new pen for the border and brush for background and text
            using (Pen pen = new Pen(this.ForeColor))
            using (SolidBrush backgroundBrush = new SolidBrush(this.CircleBackColor)) // Using CircleBackColor
            using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
            {
                // Draw the filled circle
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Better circle rendering
                e.Graphics.FillEllipse(backgroundBrush, 0, 0, this.Width - 1, this.Height - 1);

                // Draw the border of the circle
                e.Graphics.DrawEllipse(pen, 0, 0, this.Width - 1, this.Height - 1);

                // Draw the text
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(this.Text, this.Font, textBrush, new RectangleF(0, 0, this.Width, this.Height), stringFormat);
            }
        }
    }


}
