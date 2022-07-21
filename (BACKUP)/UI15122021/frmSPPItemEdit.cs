using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using System.Threading.Tasks;
using System.Linq;

namespace FactoryManagementSoftware.UI
{
    public partial class frmSPPItemEdit : Form
    {
        public frmSPPItemEdit()
        {
            InitializeComponent();
            tool.loadSPPItemNameDataToComboBox(cmbName);
            cmbName.SelectedIndex = -1;

            dt_Size = dalData.SizeSelect();
            dt_Type = dalData.TypeSelect();
            dt_Category = dalData.CategorySelect();
            dt_StdPacking = dalData.StdPackingSelect();
        }

        Text text = new Text();
        Tool tool = new Tool();

        itemDAL dalItem = new itemDAL();
        itemBLL uItem = new itemBLL();

        SPPDataDAL dalData = new SPPDataDAL();
        SPPDataBLL uData = new SPPDataBLL();

        DataTable dt_Size;
        DataTable dt_Type;
        DataTable dt_Category;
        DataTable dt_StdPacking;

        private int size_numerator = 0;
        private int size_denominator = 1;
        private string size_unit = null;

        private void btnORing_Click(object sender, EventArgs e)
        {
            lblType.Text = text.Type_Oring;
        }

        private void btnBush_Click(object sender, EventArgs e)
        {
            lblType.Text = text.Type_Bush;
        }

        private void btnGrip_Click(object sender, EventArgs e)
        {
            lblType.Text = text.Type_Grip;
        }

        private void btnCap_Click(object sender, EventArgs e)
        {
            lblType.Text = text.Type_Cap;
        }

        private void btnSocket_Click(object sender, EventArgs e)
        {
            lblType.Text = text.Type_EqualSocket;
        }

        private void btnElbow_Click(object sender, EventArgs e)
        {
            lblType.Text = text.Type_EqualElbow;
        }

        private void btnTee_Click(object sender, EventArgs e)
        {
            lblType.Text = text.Type_EqualTee;
        }

        private void btn20mm_Click(object sender, EventArgs e)
        {
            size_numerator = 20;
            size_denominator = 1;
            size_unit = text.Unit_Millimetre;

            lblSize.Text = size_denominator > 1 ? size_numerator + "/" + size_denominator + " " + size_unit : size_numerator + " " + size_unit;
        }

        private void btn25mm_Click(object sender, EventArgs e)
        {
            size_numerator = 25;
            size_denominator = 1;
            size_unit = text.Unit_Millimetre;

            lblSize.Text = size_denominator > 1 ? size_numerator + "/" + size_denominator + " " + size_unit : size_numerator + " " + size_unit;
        }

        private void btn32mm_Click(object sender, EventArgs e)
        {
            size_numerator = 32;
            size_denominator = 1;
            size_unit = text.Unit_Millimetre;

            lblSize.Text = size_denominator > 1 ? size_numerator + "/" + size_denominator + " " + size_unit : size_numerator + " " + size_unit;
        }

        private void btn50mm_Click(object sender, EventArgs e)
        {
            size_numerator = 50;
            size_denominator = 1;
            size_unit = text.Unit_Millimetre;

            lblSize.Text = size_denominator > 1 ? size_numerator + "/" + size_denominator + " " + size_unit : size_numerator + " " + size_unit;
        }

        private void btn63mm_Click(object sender, EventArgs e)
        {
            size_numerator = 63;
            size_denominator = 1;
            size_unit = text.Unit_Millimetre;

            lblSize.Text = size_denominator > 1 ? size_numerator + "/" + size_denominator + " " + size_unit : size_numerator + " " + size_unit;
        }

        private void btn90mm_Click(object sender, EventArgs e)
        {
            size_numerator = 90;
            size_denominator = 1;
            size_unit = text.Unit_Millimetre;

            lblSize.Text = size_denominator > 1 ? size_numerator + "/" + size_denominator + " " + size_unit : size_numerator + " " + size_unit;
        }

        private void btnCommonPart_Click(object sender, EventArgs e)
        {
            btnSocket.Enabled = false;
            btnElbow.Enabled = false;
            btnTee.Enabled = false;

            btnORing.Enabled = true;
            btnBush.Enabled = true;
            btnGrip.Enabled = true;
            btnCap.Enabled = true;

            btn20mm.Enabled = true;
            btn25mm.Enabled = true;
            btn32mm.Enabled = true;
            btn50mm.Enabled = true;
            btn63mm.Enabled = true;
            btn90mm.Enabled = true;

            lblCategory.Text = text.Cat_CommonPart;
            lblType.Text = "";
            lblSize.Text = "";
            size_numerator = 0;
        }

        private void btnBody_Click(object sender, EventArgs e)
        {
            btnSocket.Enabled = true;
            btnElbow.Enabled = true;
            btnTee.Enabled = true;

            btnORing.Enabled = false;
            btnBush.Enabled = false;
            btnGrip.Enabled = false;
            btnCap.Enabled = false;

            btn20mm.Enabled = true;
            btn25mm.Enabled = true;
            btn32mm.Enabled = true;
            btn50mm.Enabled = true;
            btn63mm.Enabled = true;
            btn90mm.Enabled = true;

            lblCategory.Text = text.Cat_Body;
            lblType.Text = "";
            lblSize.Text = "";
            size_numerator = 0;
        }

        private void btnAssembled_Click(object sender, EventArgs e)
        {
            btnSocket.Enabled = true;
            btnElbow.Enabled = true;
            btnTee.Enabled = true;

            btnORing.Enabled = false;
            btnBush.Enabled = false;
            btnGrip.Enabled = false;
            btnCap.Enabled = false;

            btn20mm.Enabled = true;
            btn25mm.Enabled = true;
            btn32mm.Enabled = true;
            btn50mm.Enabled = true;
            btn63mm.Enabled = true;
            btn90mm.Enabled = true;

            lblCategory.Text = text.Cat_Assembled;
            lblType.Text = "";
            lblSize.Text = "";
            size_numerator = 0;
        }

        private void btnReadyGoods_Click(object sender, EventArgs e)
        {
            btnSocket.Enabled = true;
            btnElbow.Enabled = true;
            btnTee.Enabled = true;

            btnORing.Enabled = false;
            btnBush.Enabled = false;
            btnGrip.Enabled = false;
            btnCap.Enabled = false;

            btn20mm.Enabled = true;
            btn25mm.Enabled = true;
            btn32mm.Enabled = true;
            btn50mm.Enabled = true;
            btn63mm.Enabled = true;
            btn90mm.Enabled = true;

            lblCategory.Text = text.Cat_ReadyGoods;
            lblType.Text = "";
            lblSize.Text = "";
            size_numerator = 0;
        }

        private void frmSPPItemEdit_Load(object sender, EventArgs e)
        {
            btnSocket.Enabled = false;
            btnElbow.Enabled = false;
            btnTee.Enabled = false;

            btnORing.Enabled = true;
            btnBush.Enabled = true;
            btnGrip.Enabled = true;
            btnCap.Enabled = true;

            btn20mm.Enabled = true;
            btn25mm.Enabled = true;
            btn32mm.Enabled = true;
            btn50mm.Enabled = true;
            btn63mm.Enabled = true;
            btn90mm.Enabled = true;

            lblCategory.Text = text.Cat_CommonPart;
            lblType.Text = "";
            lblSize.Text = "";
            size_numerator = 0;


        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearErrorSign();
            string keywords = cmbName.Text;

            if (!string.IsNullOrEmpty(keywords))
            {
                DataTable dt = dalItem.nameSearch(keywords);
                cmbCode.DataSource = dt;
                cmbCode.DisplayMember = "item_code";
                cmbCode.ValueMember = "item_code";
            }
            else
            {
                cmbCode.DataSource = null;
            }
        }

        private void ClearErrorSign()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            errorProvider7.Clear();
        }

        private bool Validation()
        {
            bool result = true;

            if(cbUpdateItemInfo.Checked)
            {
                if (string.IsNullOrEmpty(cmbName.Text))
                {
                    result = false;
                    errorProvider1.SetError(lblItemName, "Item Name Required");
                }

                if (string.IsNullOrEmpty(cmbCode.Text))
                {
                    result = false;
                    errorProvider2.SetError(lblItemCode, "Item Code Required");
                }

                if (string.IsNullOrEmpty(lblCategory.Text))
                {
                    result = false;
                    errorProvider3.SetError(lblCategory, "Item Category Required");
                }

                if (string.IsNullOrEmpty(lblType.Text))
                {
                    result = false;
                    errorProvider4.SetError(lblType, "Item Type Required");
                }

                if (string.IsNullOrEmpty(lblSize.Text))
                {
                    result = false;
                    errorProvider5.SetError(lblSize, "Item Size Required");
                }
            }
          

            if (cbUpdateStdPacking.Checked)
            {
                if (string.IsNullOrEmpty(txtQtyPerPacket.Text))
                {
                    result = false;
                    errorProvider6.SetError(lblQtyPerPacket, "Qty per packet required");
                }

                if (string.IsNullOrEmpty(txtQtyPerBag.Text))
                {
                    result = false;
                    errorProvider7.SetError(lblQtyPerBag, "Qty per bag required");

                }
            }

                return result;

        }

        private void cmbCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearErrorSign();
            string itemCode = cmbCode.Text;

            if(!string.IsNullOrEmpty(itemCode) && itemCode.Length == 5)
            {
                string size = itemCode[2].ToString() + itemCode[3].ToString();
                int sizeINT = 0;
                sizeINT = int.TryParse(size, out sizeINT) ? sizeINT : 0;

                if(sizeINT > 0)
                {
                    size_numerator = sizeINT;
                    size_denominator = 1;
                    size_unit = text.Unit_Millimetre;

                    lblSize.Text = size_denominator > 1 ? size_numerator + "/" + size_denominator + " " + size_unit : size_numerator + " " + size_unit;
                }
                else
                {
                    size_numerator = 0;
                  
                    lblSize.Text = "";
                }

                string type = itemCode[4].ToString();

                if(type == "C")
                {
                    lblType.Text = text.Type_Cap;
                }
                else if (type == "G")
                {
                    lblType.Text = text.Type_Grip;
                }
                else if (type == "B")
                {
                    lblType.Text = text.Type_Bush;
                }
                else if (type == "R")
                {
                    lblType.Text = text.Type_Oring;
                }
                else
                {
                    //lblType.Text = "";
                }
            }
            else
            {
                size_numerator = 0;

                lblSize.Text = "";

                //lblType.Text = "";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                bool success = false;

                if(cbUpdateItemInfo.Checked)
                {
                    string sizeTableCode = null;
                    int size_INT = 0;

                    foreach (DataRow row in dt_Size.Rows)
                    {
                        if (row[dalData.SizeNumerator].ToString() == size_numerator.ToString())
                        {
                            sizeTableCode = row[dalData.TableCode].ToString();

                            size_INT = int.TryParse(sizeTableCode, out size_INT) ? size_INT : 0;

                            break;
                        }
                    }

                    string typeTableCode = null;
                    int type_INT = 0;

                    foreach (DataRow row in dt_Type.Rows)
                    {
                        if (row[dalData.TypeName].ToString() == lblType.Text)
                        {
                            typeTableCode = row[dalData.TableCode].ToString();

                            type_INT = int.TryParse(typeTableCode, out type_INT) ? type_INT : 0;

                            break;
                        }
                    }

                    string categoryTableCode = null;
                    int category_INT = 0;

                    foreach (DataRow row in dt_Category.Rows)
                    {
                        if (row[dalData.CategoryName].ToString() == lblCategory.Text)
                        {
                            categoryTableCode = row[dalData.TableCode].ToString();

                            category_INT = int.TryParse(categoryTableCode, out category_INT) ? category_INT : 0;

                            break;
                        }
                    }

                    //MessageBox.Show("category: " + category_INT + " type: "+type_INT +" size: "+size_INT);

                    uItem.item_code = cmbCode.Text;
                    uItem.Size_tbl_code_1 = size_INT;
                    uItem.Type_tbl_code = type_INT;
                    uItem.Category_tbl_code = category_INT;

                    success = dalItem.SPPUpdate(uItem);
                    //if data is updated successfully then the value = true else false
                    if (success == true)
                    {
                        //data updated successfully
                        MessageBox.Show("Item successfully updated ");

                    }
                    else
                    {
                        //failed to update user
                        MessageBox.Show("Failed to updated item");
                    }
                }

                if(cbUpdateStdPacking.Checked)
                {
                    uData.Item_code = cmbCode.Text;
                    uData.Qty_Per_Packet = Convert.ToInt32(txtQtyPerPacket.Text);
                    uData.Qty_Per_Bag = Convert.ToInt32(txtQtyPerBag.Text);
                    uData.Updated_Date = DateTime.Now;
                    uData.Updated_By = MainDashboard.USER_ID;

                    int tableCode = -1;
                    foreach(DataRow row in dt_StdPacking.Rows)
                    {
                        if(row[dalData.ItemCode].ToString() == uData.Item_code)
                        {
                            tableCode = int.TryParse(row[dalData.TableCode].ToString(), out tableCode) ? tableCode : -1;
                        }
                    }

                    if (tableCode > 0)//update data
                    {
                        uData.Table_Code = tableCode;
                        success = dalData.StdPackingUpdate(uData);
                    }
                    else
                    {
                        success = dalData.InsertStdPacking(uData);
                    }

                    if (success == true)
                    {
                        //data updated successfully
                        MessageBox.Show("Item std packing successfully updated ");
                        dt_StdPacking = dalData.StdPackingSelect();
                    }
                    else
                    {
                        //failed to update user
                        MessageBox.Show("Failed to updated item std packing");
                    }
                }
            }
        }

        private void lblCategory_Click(object sender, EventArgs e)
        {

        }

        private void lblType_TextChanged(object sender, EventArgs e)
        {
            ClearErrorSign();
        }

        private void lblSize_TextChanged(object sender, EventArgs e)
        {
            ClearErrorSign();
        }

        private void lblCategory_TextChanged(object sender, EventArgs e)
        {
            ClearErrorSign();
        }

        private void txtQtyPerPacket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtQtyPerBag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void cbUpdateStdPacking_CheckedChanged(object sender, EventArgs e)
        {
            if(cbUpdateStdPacking.Checked)
            {
                lblQtyPerPacket.Visible = true;
                lblQtyPerBag.Visible = true;
                txtQtyPerPacket.Visible = true;
                txtQtyPerBag.Visible = true;

                btnUpdate.Visible = true;
                btnCancel.Visible = true;
            }
            else
            {
                lblQtyPerPacket.Visible = false;
                lblQtyPerBag.Visible = false;
                txtQtyPerPacket.Visible = false;
                txtQtyPerBag.Visible = false;

                if(!cbUpdateItemInfo.Checked)
                {
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                }
            }
        }

        private void cbUpdateItemInfo_CheckedChanged(object sender, EventArgs e)
        {
            if(cbUpdateItemInfo.Checked)
            {
                btnUpdate.Visible = true;
                btnCancel.Visible = true;
            }
            else if(!cbUpdateStdPacking.Checked)
            {
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
            }
        }
    }
}
