using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FactoryManagementSoftware.UI
{
    public class RoundButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create a new pen and brush
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.Black);

            // Draw the circle
            e.Graphics.DrawEllipse(pen, 0, 0, this.Width - 1, this.Height - 1);

            // Draw the text
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(this.Text, this.Font, brush, new RectangleF(0, 0, this.Width, this.Height), stringFormat);
        }

    }
}
