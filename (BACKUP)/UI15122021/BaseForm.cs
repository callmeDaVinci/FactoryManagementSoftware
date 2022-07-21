using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryManagementSoftware.UI
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            AutoSize = false;
            Size = new Size(1600, 900);
            WindowState = FormWindowState.Maximized;
            Font = new Font("Segoe UI", 8, FontStyle.Regular);
            BackColor = Color.White;
        }
    }
}
