using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.Module;
using System.Data;
using System.Windows.Forms;
using System;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Button = System.Windows.Forms.Button;

namespace FactoryManagementSoftware.UI
{
    public partial class frmNewSBBAddItem : Form
    {
        public frmNewSBBAddItem()
        {
            InitializeComponent();
            dt_ReadyGoods = dalItem.SPPReadyGoodsSelect();
        }

        SBBDataDAL dalData = new SBBDataDAL();
        itemDAL dalItem = new itemDAL();
        Tool tool = new Tool();
        Text text = new Text();
        userDAL dalUser = new userDAL();
        System.Data.DataTable dt_ReadyGoods;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string itemSize = "";
        private string itemType = "";
        static public string itemCode = "";

        private void ButtonTypeSelected(Button selectedBtn)
        {
            btnEqualElbow.FlatAppearance.BorderColor = Color.Black;
            btnEqualElbow.FlatAppearance.BorderSize = 1;

            btnEqualTee.FlatAppearance.BorderColor = Color.Black;
            btnEqualTee.FlatAppearance.BorderSize = 1;

            btnEqualSocket.FlatAppearance.BorderColor = Color.Black;
            btnEqualSocket.FlatAppearance.BorderSize = 1;

            selectedBtn.FlatAppearance.BorderColor = Color.FromArgb(52, 139, 209);
            selectedBtn.FlatAppearance.BorderSize = 2;
            ActiveControl = gbType;
        }

        private void ButtonSizeSelected(Button selectedBtn)
        {
            btn20MM.FlatAppearance.BorderColor = Color.Black;
            btn20MM.FlatAppearance.BorderSize = 1;

            btn25MM.FlatAppearance.BorderColor = Color.Black;
            btn25MM.FlatAppearance.BorderSize = 1;

            btn32MM.FlatAppearance.BorderColor = Color.Black;
            btn32MM.FlatAppearance.BorderSize = 1;

            btn50MM.FlatAppearance.BorderColor = Color.Black;
            btn50MM.FlatAppearance.BorderSize = 1;

            btn63MM.FlatAppearance.BorderColor = Color.Black;
            btn63MM.FlatAppearance.BorderSize = 1;

            selectedBtn.FlatAppearance.BorderColor = Color.FromArgb(52, 139, 209);
            selectedBtn.FlatAppearance.BorderSize = 2;
            ActiveControl = gbSize;
        }

        private void btnEqualSocket_Click(object sender, EventArgs e)
        {
            itemType = text.Type_EqualSocket;

            ButtonTypeSelected(btnEqualSocket);

            GetItemCode();
        }

        private void btnEqualElbow_Click(object sender, EventArgs e)
        {
            itemType = text.Type_EqualElbow;

            ButtonTypeSelected(btnEqualElbow);


            GetItemCode();
        }

        private void btnEqualTee_Click(object sender, EventArgs e)
        {
            itemType = text.Type_EqualTee;

            ButtonTypeSelected(btnEqualTee);

            GetItemCode();

        }
        private string GetItemCode()
        {
            if (!string.IsNullOrEmpty(itemType) && !string.IsNullOrEmpty(itemSize))
            {
                foreach (DataRow row in dt_ReadyGoods.Rows)
                {
                    if (row["TYPE"].ToString() == itemType && row["SIZE"].ToString() == itemSize)
                    {
                        return row["CODE"].ToString();
                    }
                }

            }

            return "";
        }

        private void btn20MM_Click(object sender, EventArgs e)
        {
            itemSize = "20";
            ButtonSizeSelected(btn20MM);
            GetItemCode();
        }

        private void btn25MM_Click(object sender, EventArgs e)
        {
            itemSize = "25";
            ButtonSizeSelected(btn25MM);
            GetItemCode();
        }

        private void btn32MM_Click(object sender, EventArgs e)
        {
            itemSize = "32";
            ButtonSizeSelected(btn32MM);
            GetItemCode();
        }

        private void btn50MM_Click(object sender, EventArgs e)
        {
            itemSize = "50";
            ButtonSizeSelected(btn50MM);
            GetItemCode();
        }

        private void btn63MM_Click(object sender, EventArgs e)
        {
            itemSize = "63";
            ButtonSizeSelected(btn63MM);
            GetItemCode();
        }

        private void btn90MM_Click(object sender, EventArgs e)
        {
            //lblSize.Text = "90";
            //GetItemCode();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string Code = GetItemCode();

            if(Code == "")
            {
                MessageBox.Show("Please select item type and item size.");
            }
            else
            {
                itemCode = Code;
                Close();
            }
        }

        private void frmNewSBBAddItem_Load(object sender, EventArgs e)
        {
            ActiveControl = gbType;
        }
    }
}
