using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;


namespace FactoryManagementSoftware.UI
{
    public partial class BaseForm : Form
    {
        Tool tool = new Tool();
        Text text = new Text();

        public BaseForm()
        {
            InitializeComponent();
        }


    }
}
