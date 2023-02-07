using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FactoryManagementSoftware.Module;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using System.Linq;
using FactoryManagementSoftware.DAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ExplorerBar;
using System.Security.Cryptography.X509Certificates;

namespace FactoryManagementSoftware.UI
{
    public partial class ExcelUpload : Form
    {
        Tool tool = new Tool();
        Text text = new Text();
        facStockDAL dalStock = new facStockDAL();
        itemDAL dalItem = new itemDAL();

        #region Data String

        private string path = null;
        private string excelName = null;

        //table header
        private string HEADER_INDEX = "#";
        private string HEADER_TIMESTAMP = "TIMESTAMP";
        private string HEADER_TESTATOR = "TESTATOR";
        private string HEADER_STATUS = "STATUS";

        private string HEADER_INFORMATION = "INFORMATION";
        private string HEADER_DESCRIPTION = "DESCRIPTION";

        #endregion

        DataTable dt_DataSource;

        DataTable DT_FAC_STOCK;

        public ExcelUpload()
        {
            InitializeComponent();

            DT_FAC_STOCK = dalStock.Select();
        }

        #region New Table Setting

        private DataTable NewDataSourceTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(text.Header_Index, typeof(int));
            dt.Columns.Add(text.Header_ItemCode, typeof(string));
            dt.Columns.Add(text.Header_ItemName, typeof(string));
            dt.Columns.Add(text.Header_CountedQty, typeof(decimal));
            dt.Columns.Add(text.Header_SystemQty, typeof(decimal));
            dt.Columns.Add(text.Header_Difference, typeof(decimal));
            dt.Columns.Add(text.Header_Fac, typeof(string));

            return dt;
        }

        private DataTable NewMainTestatorTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(HEADER_INDEX, typeof(int));
            dt.Columns.Add(HEADER_TIMESTAMP, typeof(string));
            dt.Columns.Add(HEADER_TESTATOR, typeof(string));
            dt.Columns.Add(HEADER_STATUS, typeof(string));

            return dt;
        }

        private DataTable NewWillDetailTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(HEADER_INDEX, typeof(int));
            dt.Columns.Add(HEADER_INFORMATION, typeof(string));
            dt.Columns.Add(HEADER_DESCRIPTION, typeof(string));

            return dt;
        }

        #endregion

        private void DgvUIEdit(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6F, FontStyle.Regular);
            //dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 7F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;

            //dgv.Columns[header_ItemName].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgv.Columns[header_Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgv.Columns[header_Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgv.Columns[header_ItemCode].Visible = false;

            if(dgv == dgvList)
            {
                dgv.Columns[text.Header_ItemCode].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            txtPath.BackColor = SystemColors.Window;
            txtPath.Clear();
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "File Excel|*.xlsx";

            DialogResult re = fd.ShowDialog();
            excelName = fd.SafeFileName;

            if (re == DialogResult.OK)
            {
                path = fd.FileName;

                //string extension = System.IO.Path.GetExtension(path);
                //MessageBox.Show(extension);
                //if (".csv".Equals(extension))

                txtPath.Text = path;
                txtPath.ForeColor = Color.Black;
                //txtPath.BackColor = Color.DarkSeaGreen;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                frmLoading.ShowLoadingScreen();
                ReadExcel(path);
                frmLoading.CloseForm();
            }
            else
            {
                MessageBox.Show("path not found");
            }
        }

        private decimal GetFacStock(string itemCode, string FacName)
        {
            decimal facStock = -1;

            if(itemCode == "CONNECTOR LEFT 492/489")
            {
                float checkpoint = 1;
            }

            if(DT_FAC_STOCK != null)
            {
                foreach(DataRow row in DT_FAC_STOCK.Rows) 
                {
                    string DB_FacName = row["fac_name"].ToString().ToUpper().Replace(" ", "");
                    FacName = FacName.ToUpper().Replace(" ", "");

                    string DB_ItemCode = row[dalItem.ItemCode].ToString().ToUpper().Replace(" ", "");
                    itemCode = itemCode.ToUpper().Replace(" ", "");

                    //if (row[dalItem.ItemCode].ToString().Equals(itemCode) && row["fac_name"].ToString().ToUpper().Equals(FacName.ToUpper()))
                    //{
                    //    //stock_qty
                    //    facStock = decimal.TryParse(row["stock_qty"].ToString(), out decimal i) ? i : 0;
                    //    break;
                    //}

                    if (DB_ItemCode.Equals(itemCode) && DB_FacName.Equals(FacName))
                    {
                        //stock_qty
                        facStock = decimal.TryParse(row["stock_qty"].ToString(), out decimal i) ? i : 0;
                        break;
                    }

                }
            }

            return facStock;
        }

        private void ReadExcel(string Path)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type


            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            int row;
            int col;
            int totalRow = 0;
            int totalCol = 0;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(Path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            totalRow = range.Rows.Count;
            totalCol = range.Columns.Count;

            int headerRow = int.TryParse(txtHeaderRow.Text, out headerRow)? headerRow : 1;

            //bool headerFound = false;

            //for (col = 1; col <= totalCol; col++)
            //{
            //    string test = Convert.ToString((range.Cells[headerRow, col] as Excel.Range).Value2);

            //    if (test.ToUpper().Contains(PRIMARYKEY_TIMESTAMP))
            //    {
            //        headerFound = true;
            //        break;
            //    }
            //}

            if(true)//headerFound
            {
                int index = 1;

                if (dt_DataSource == null || dt_DataSource.Rows.Count <= 0)
                {
                    dt_DataSource = NewDataSourceTable();
                }
                else
                {
                    index = int.TryParse(dt_DataSource.Rows[dt_DataSource.Rows.Count - 1][HEADER_INDEX].ToString(), out index)? index + 1 : 1;
                }

                DataRow dtRow;


                for (row = headerRow + 1; row <= 200; row++)
                {
                    dtRow = dt_DataSource.NewRow();
                    //dtRow[text.Header_Index] = index++;
                    string itemCode = "";
                    string facName = "";
                    decimal CountedQty = 0;
                    decimal SystemQty = 0;

                    for (col = 1; col <= 10; col++)
                    {
                        string header = Convert.ToString((range.Cells[headerRow, col] as Excel.Range).Value2);

                        if (header != null)
                        {
                            header = header.ToUpper();

                            object value = (range.Cells[row, col] as Excel.Range).Value2;
                           
                            if (header.Contains(text.Header_Index))
                            {
                                string data = Convert.ToString(value);
                                dtRow[text.Header_Index] = int.TryParse(data, out int i)? i : 0;
                            }
                            else if (header.Contains(text.Header_ItemCode))
                            {
                                string data = Convert.ToString(value);

                                itemCode = data;

                                dtRow[text.Header_ItemCode] = data;

                                //get system stock qty by item code
                                //dtRow[text.Header_SystemQty] = decimal.TryParse(data, out decimal i) ? i : 0;

                            }
                            else if (header.Contains(text.Header_ItemName))
                            {
                                string data = Convert.ToString(value);
                                dtRow[text.Header_ItemName] = data;
                            }
                            else if (header.Contains(text.Header_CountedQty))
                            {
                                string data = Convert.ToString(value);
                                CountedQty = decimal.TryParse(data, out decimal i) ? i : 0;
                                dtRow[text.Header_CountedQty] = CountedQty;
                            }
                            else if (header.Contains(text.Header_Fac))
                            {
                                string data = Convert.ToString(value);
                                facName = data;
                                dtRow[text.Header_Fac] = data;
                            }

                            if(!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(facName))
                            {
                                SystemQty = GetFacStock(itemCode, facName);
                                dtRow[text.Header_SystemQty] = SystemQty;

                                dtRow[text.Header_Difference] = CountedQty - SystemQty;

                            }

                        }

                    }

                    dt_DataSource.Rows.Add(dtRow);

                }

            }
            //else
            //{
            //    MessageBox.Show("Header not found!\nPlease change the row of header in yellow field,\nor upload another file.");
            //}



            
            
            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            LoadDataToList();

            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void LoadDataToList()
        {
            dgvList.DataSource = null;

            //DataTable dt = NewMainTestatorTable();
            //DataRow dtRow;
            //foreach(DataRow row in dt_DataSource.Rows)
            //{
            //    dtRow = dt.NewRow();

            //    dtRow[HEADER_INDEX] = row[HEADER_INDEX];
            //    dtRow[HEADER_TIMESTAMP] = row[HEADER_TIMESTAMP];
            //    dtRow[HEADER_TESTATOR] = row[PART_A + PART_A_TESTATOR + " " + HEADER_FULLNAME];

            //    dt.Rows.Add(dtRow);
            //}
            dgvList.DataSource = dt_DataSource;

            DgvUIEdit(dgvList);

            dgvList.ClearSelection();

        }

        private void ExportToWord2()
        {
            string path = @"D:\Finex\Will";

            Directory.CreateDirectory(path);

            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = path,

                Filter = "Word Document (*.docx)|*.docx",
                FileName = "Testing" + ".docx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)//sfd.ShowDialog() == DialogResult.OK
            {
                try
                {
                    frmLoading.ShowLoadingScreen();

                    #region Creating Word Document
                    //Create an instance for word winword  
                    Word.Application winword = new Word.Application
                    {

                        //Set animation status for word application  
                        //ShowAnimation = false,

                        //Set status for word application is to be visible or not.  
                        Visible = false
                    };

                    
                    //THE LOCATION OF THE TEMPLATE FILE ON THE MACHINE  
                    //Object oTemplatePath = @"D:\Users\Jun\Desktop\Template free will - with 3 properties.dotx";
                    //ADDING A NEW DOCUMENT FROM A TEMPLATE  
                    //oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

                    //Create a missing variable for missing value  
                    object missing = Missing.Value;
                    object oFalse = false;
                    object oTrue = true;

                    //Create a new document  
                    Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                    document.Sections[1].PageSetup.DifferentFirstPageHeaderFooter = -1;
                    //Section section2 = document.Sections[0];
                    //section2.PageSetup.DifferentFirstPageHeaderFooter = -1;

                    float pointToCMRate = 0.035f;
                   


                    document.PageSetup.BottomMargin = 6.35f / pointToCMRate;
                    #endregion

                    // Setting Different First page Header & Footer
                    //document.Sections[1].Headers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range.Text = "First Page Header";
                    //document.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range.Text = "First Page Footer";

                    // Setting Other page Header & Footer
                   
                    //document.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range.Text = "Other Page Footer";

                    #region Inserting Water Mark Text

                    //THE LOGO IS ASSIGNED TO A SHAPE OBJECT SO THAT WE CAN USE ALL THE  
                    //SHAPE FORMATTING OPTIONS PRESENT FOR THE SHAPE OBJECT  
                    Shape logoWatermark = null;

                    winword.ActiveWindow.ActivePane.View.SeekView = WdSeekView.wdSeekCurrentPageHeader;

                    //INCLUDING THE TEXT WATER MARK TO THE DOCUMENT  
                    logoWatermark = winword.Selection.HeaderFooter.Shapes.AddTextEffect(
                        Microsoft.Office.Core.MsoPresetTextEffect.msoTextEffect1,
                        "DRAFT", "Bodoni MT", 60,
                        Microsoft.Office.Core.MsoTriState.msoTrue,
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        0, 0, ref missing);
                    logoWatermark.Select(ref missing);
                    logoWatermark.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                    logoWatermark.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
                    logoWatermark.Fill.Solid();
                    logoWatermark.Fill.ForeColor.RGB = (Int32)WdColor.wdColorGray30;
                    logoWatermark.RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionMargin;

                    logoWatermark.RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionMargin;

                    logoWatermark.Left = (float)WdShapePosition.wdShapeCenter;
                    logoWatermark.Top = (float)WdShapePosition.wdShapeCenter;
                    logoWatermark.Height = winword.InchesToPoints(2.4f);
                    logoWatermark.Width = winword.InchesToPoints(6f);

                    //SETTING FOCUES BACK TO DOCUMENT  
                    winword.ActiveWindow.ActivePane.View.SeekView = WdSeekView.wdSeekMainDocument;

                    #endregion

                    #region COVER PAGE
                    //Add paragraph with Heading 1 style  
                    Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                    para1 = document.Content.Paragraphs.Add(ref missing);
                    para1 = document.Content.Paragraphs.Add(ref missing);
                    object styleHeading1 = "Normal";
                    para1.Range.set_Style(ref styleHeading1);
                    
                    string Space = "";

                    for(int k = 0; k < 5; k++)
                    {
                        Space += Environment.NewLine;
                    }

                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Font.Size = 15;
                    para1.Range.Text = Space + "THE";
                    para1.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Font.Size = 30;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "LAST WILL";
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Font.Size = 15;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "AND";
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Font.Size = 30;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "TESTAMENT";
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Font.Size = 15;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "OF" + Environment.NewLine;
                    para1.Range.InsertParagraphAfter();

////////////////////NAME
                    para1.Range.Font.Size = 30;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "ONG WING CHUN";
                    para1.Range.InsertParagraphAfter();

                    para1.Range.Font.Size = 30;
                    para1.Range.Font.Name = "Bodoni MT";
                    para1.Range.Text = "(900624-14-5747)";
                    para1.Range.InsertParagraphAfter();
                    #endregion

                    #region Hard Page Break
                    object oCollapseEnd = WdCollapseDirection.wdCollapseEnd;
                    object oPageBreak = WdBreakType.wdPageBreak;

                    object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
                    Range wrdRng = document.Bookmarks.get_Item(ref oEndOfDoc).Range;

                    wrdRng.Collapse(ref oCollapseEnd);
                    wrdRng.InsertBreak(ref oPageBreak);
                    wrdRng.Collapse(ref oCollapseEnd);
                    #endregion

                    //document.PageSetup.DifferentFirstPageHeaderFooter = -1;
                    
                    #region Page 1 Statement

                    Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                    object styleHeading2 = "Normal";
                    para2.Range.set_Style(ref styleHeading2);
                    para2.Range.Font.Name = "Times New Roman";
                    para2.Range.Font.Size = 12;
                    para2.Range.Font.Bold = 5;
                    para2.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                    para2.Range.Text = "LAST WILL AND TESTAMENT";
                    para2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para2.Range.ParagraphFormat.SpaceBefore = 0f;
                    para2.Range.ParagraphFormat.SpaceAfter = 0f;
                    para2.Range.InsertParagraphAfter();
               
                    para2.Range.Font.Name = "Times New Roman";
                    para2.Range.Font.Size = 12;
                    para2.Range.Font.Bold = 5;
                    para2.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                    para2.Range.Text = "OF";
                    para2.Range.ParagraphFormat.SpaceBefore = 0f;
                    para2.Range.ParagraphFormat.SpaceAfter = 0f;
                    para2.Range.InsertParagraphAfter();

                    para2.Range.Font.Name = "Times New Roman";
                    para2.Range.Font.Size = 12;
                    para2.Range.Font.Bold = 5;
                    para2.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                    para2.Range.Text = "ONG WING CHUN";
                    para2.Range.ParagraphFormat.SpaceBefore = 0f;
                    para2.Range.ParagraphFormat.SpaceAfter = 5f;
                    para2.Range.InsertParagraphAfter();

                    //para2.Range.Font.Name = "Times New Roman";
                    //para2.Range.Font.Size = 12;
                    para2.Range.Font.Bold = 0;
                    para2.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                    para2.Range.Text = Environment.NewLine + "This Will dated 			is made by me 	 (NRIC No. 	) born on 	of " + Environment.NewLine;
                    para2.Range.InsertParagraphAfter();


                    //Word.Document doc = winword.ActiveDocument;
                    //Word.Range rng = doc.Content;

                    //object oListName = "TreeList";
                    //Word.ListTemplate lstTemp = doc.ListTemplates.Add(ref oTrue, ref oListName);
                    //int i;

                    //rng.Text = "Level 1\rLevel 1.1\rLevel 1.2\rLevel 2\rLevel 2.1\rLevel 2.1.1";

                    //i = 1;
                    //lstTemp.ListLevels[i].NumberFormat = "%" + i.ToString() + ".";
                    //lstTemp.ListLevels[i].NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
                    //lstTemp.ListLevels[i].NumberPosition = winword.CentimetersToPoints(0.5f * (i - 1));
                    //lstTemp.ListLevels[i].TextPosition = winword.CentimetersToPoints(0.5f * i);
                    //i = 2;
                    //lstTemp.ListLevels[i].NumberFormat = "%" + (i - 1).ToString() + ".%" + i.ToString() + ".";
                    //lstTemp.ListLevels[i].NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
                    //lstTemp.ListLevels[i].NumberPosition = winword.CentimetersToPoints(0.5f * (i - 1));
                    //lstTemp.ListLevels[i].TextPosition = winword.CentimetersToPoints(0.5f * i);
                    //i = 3;
                    //lstTemp.ListLevels[i].NumberFormat = "%" + (i - 2).ToString() + "%" + (i - 1).ToString() + ".%" + i.ToString() + ".";
                    //lstTemp.ListLevels[i].NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
                    //lstTemp.ListLevels[i].NumberPosition = winword.CentimetersToPoints(0.5f * (i - 1));
                    //lstTemp.ListLevels[i].TextPosition = winword.CentimetersToPoints(0.5f * i);
                    //object oListApplyTo = Word.WdListApplyTo.wdListApplyToWholeList;
                    //object oListBehavior = Word.WdDefaultListBehavior.wdWord10ListBehavior;

                    //rng.ListFormat.ApplyListTemplate(lstTemp, ref oFalse, ref oListApplyTo, ref oListBehavior);


                    //Paragraph para3 = document.Content.Paragraphs.Add(ref missing);

                    //object oListName = "TreeList";
                    //Word.ListTemplate lstTemp = document.ListTemplates.Add(ref oTrue, ref oListName);
                    //int i;

                    //para3.Range.Text = "I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
                    //para3.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    //i = 1;
                    //lstTemp.ListLevels[i].NumberFormat = "%" + i.ToString() + ".";
                    //lstTemp.ListLevels[i].NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
                    //lstTemp.ListLevels[i].NumberPosition = winword.CentimetersToPoints(1f * (i - 1));
                    //lstTemp.ListLevels[i].TextPosition = winword.CentimetersToPoints(1f * i);

                    //object oListApplyTo = Word.WdListApplyTo.wdListApplyToWholeList;
                    //object oListBehavior = Word.WdDefaultListBehavior.wdWord10ListBehavior;
                    //para3.Range.ListFormat.ApplyListTemplate(lstTemp, ref oFalse, ref oListApplyTo, ref oListBehavior);

                    //winword.Selection.TypeParagraph();
                    //Paragraph para4 = document.Content.Paragraphs.Add(ref missing);
                    //para4.Range.Text = "LAST WILL AND TESTAMENT";
                    //para4.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    //para4.Range.InsertParagraphAfter();

                    //SETTING THE OUTLINE LEVEL  
                    //SELECT THE CONTENTS WHOSE OUTLINE LEVEL NEEDS TO BE CHANGED AND  
                    //SET THE VALUE  
                    //Paragraph para3 = document.Content.Paragraphs.Add(ref missing);


                    ////para3.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;

                    //para3.Range.Text = "I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
                    //para3.Range.InsertParagraphAfter();

                    //////////////////////////////////////////////////////////////////////////////////////////////
                    ///
                    //object oListName = "TreeList";
                    //Word.ListTemplate lstTemp = document.ListTemplates.Add(ref oTrue, ref oListName);
                    //int i;

                    //i = 1;
                    //lstTemp.ListLevels[i].NumberFormat = "%" + i.ToString() + ".";
                    //lstTemp.ListLevels[i].NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
                    //lstTemp.ListLevels[i].NumberPosition = winword.CentimetersToPoints(1f * (i - 1));
                    //lstTemp.ListLevels[i].TextPosition = winword.CentimetersToPoints(1f * i);

                    //object oListApplyTo = Word.WdListApplyTo.wdListApplyToWholeList;
                    //object oListBehavior = Word.WdDefaultListBehavior.wdWord10ListBehavior;
                    ////para3.Range.ListFormat.ApplyListTemplate(lstTemp, ref oFalse, ref oListApplyTo, ref oListBehavior);

                    //Word.Paragraph paragraph = null;
                    //Word.Range range = document.Content;
                    //paragraph = range.Paragraphs.Add();
                    //paragraph.Range.Text = "I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
                    //paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                    ////paragraph.Range.ListFormat.ApplyBulletDefault(Word.WdDefaultListBehavior.wdWord10ListBehavior);
                    ////paragraph.Range.ListFormat.ApplyListTemplateWithLevel(lstTemp, ref oFalse, ref oListApplyTo, ref oListBehavior);
                    //paragraph.Range.ListFormat.ApplyListTemplate(lstTemp, ref oFalse, ref oListApplyTo, ref oListBehavior);
                    //// ATTENTION: We have to outdent the paragraph AFTER its list format has been set, otherwise this has no effect.
                    //// Without this, the the indent of "Item 2" differs from the indent of "Item 1".
                    //paragraph.Outdent();

                    //paragraph.Range.InsertParagraphAfter();

                    //paragraph = range.Paragraphs.Add();
                    //paragraph.Range.Text = "Item 1.1";
                    //// ATTENTION: We have to indent the paragraph AFTER its text has been set, otherwise this has no effect.
                    //paragraph.Indent();
                    //paragraph.Range.InsertParagraphAfter();
                    //paragraph.Range.InsertParagraphAfter();
                    //paragraph.Range.Delete();
                    //paragraph = range.Paragraphs.Add();
                    //paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    //paragraph.Range.Text = "I appoint as my executor my 		 (NRIC No. 		) of 			 but if he/she is unwilling or unable to act for whatsoever reason, then I appoint as my executor my 		 of ";
                    //paragraph.Range.InsertParagraphAfter();

                    //paragraph = range.Paragraphs.Add();
                    //paragraph.Range.Text = "Item 2";
                    //paragraph.Outdent();





                    //para4.Range.Text = "I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
                    //para4.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                    //// Apply multi level list
                    //para4.Range.ListFormat.ApplyListTemplateWithLevel(
                    //    listGallery.ListTemplates[1],
                    //    ContinuePreviousList: false,
                    //    ApplyTo: WdListApplyTo.wdListApplyToWholeList,
                    //    DefaultListBehavior: WdDefaultListBehavior.wdWord10ListBehavior);

                    //para4.Range.ListFormat.ListIndent();
                    //para4.Range.Text = "2nd I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.";
                    //para4.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                    ListGallery listGallery = winword.ListGalleries[WdListGalleryType.wdNumberGallery];


                    Paragraph para4 = document.Content.Paragraphs.Add(ref missing);
                    para4.Range.Select();

                    // Apply multi level list
                    winword.Selection.Range.ListFormat.ApplyListTemplateWithLevel(listGallery.ListTemplates[1],ContinuePreviousList: false,
                        ApplyTo: WdListApplyTo.wdListApplyToWholeList,
                        DefaultListBehavior: WdDefaultListBehavior.wdWord10ListBehavior);

                    //First level
                    //winword.Selection.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    winword.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    winword.Selection.ParagraphFormat.SpaceBefore = 15f;
                    winword.Selection.ParagraphFormat.SpaceAfter = 15f;



                    winword.Selection.TypeText("I revoke all earlier wills and exclude my movable and immovable assets located in any country in which I have a separate Will made according to the laws of that country before my demise. In the event I do not have a separate Will made according to the laws of a particular country where my assets are located, then those assets shall form part of this Will and shall be distributed accordingly. I hereby declare that I am domiciled in Malaysia.");  // Set text to key in//1.
                    winword.Selection.TypeParagraph();  // Simulate typing in MS Word
                                                        //winword.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                    // winword.Selection.TypeBackspace();

                    winword.Selection.ParagraphFormat.SpaceBefore = 15f;
                    winword.Selection.ParagraphFormat.SpaceAfter = 15f;
                    winword.Selection.TypeText("I appoint as my executor my 		 (NRIC No. 		) of 			 but if he/she is unwilling or unable to act for whatsoever reason, then I appoint as my executor my 		 of 	 	");  // Set text to key in//1.
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("In this Will unless it is specifically stated to the contrary, my Executor(s) shall also act as my Trustee(s).");
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("If my    fails to act as the Guardian of my infant children for whatsoever reason, then I appoint my              (NRIC No. 		) of 				as Guardian for as long as required by the law. However, if my 		is unable or unwilling to act for whatsoever reason, then I appoint my 				(NRIC No. 		) of				to act as Guardian for as long as required by the law.");
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("I hereby give and bequeath my entire estate (including all movable and immovable properties) whatsoever and wheresoever situated, subject to the Settlement of Debts which were not specifically disposed off hereby or by any of my codicil hereto (hereinafter referred to as “My Estate”) to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:");
                    winword.Selection.TypeParagraph();

                    winword.Selection.Range.ListFormat.ListIndent();

                    winword.Selection.TypeText("My ");
                    winword.Selection.TypeParagraph();

                    winword.Selection.Range.ListFormat.ListOutdent();
                    winword.Selection.TypeText("In the event any beneficiary(ies) predeceases me, the entitlement that such predeceased beneficiary(ies) would have received shall instead be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to herein. ");
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("If he/she does not survive me, then the benefit he/she would have received shall be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to under my residuary estate herein.");
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("I give my property known as 			 to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:");
                    winword.Selection.TypeParagraph();

                    winword.Selection.Range.ListFormat.ListIndent();
                    winword.Selection.TypeText("My 		(NRIC No.	)(1/2)");
                    winword.Selection.TypeParagraph();
                    winword.Selection.TypeText("My 		(NRIC No.	)(1/2)");
                    winword.Selection.TypeParagraph();

                    // winword.Selection.TypeBackspace();
                    winword.Selection.Range.ListFormat.ListOutdent();

                    winword.Selection.TypeText("In the event any beneficiary(ies) predeceases me, the entitlement that such predeceased beneficiary(ies) would have received shall instead be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to herein. ");
                    winword.Selection.InsertParagraphAfter();

                    winword.Selection.TypeText("If he/she does not survive me, then the benefit he/she would have received shall be given to the surviving beneficiary(ies) in the proportion that the surviving beneficiary(ies) is entitled to under my residuary estate herein.");
                    winword.Selection.TypeParagraph();

                    winword.Selection.TypeText("I direct that any sums required to discharge a charge or to withdraw a lien attached to this property shall be paid out of my residuary estate.");
                    winword.Selection.TypeParagraph();

    
                    winword.Selection.TypeText("I give my property known as 			 to the following beneficiary(ies) according to the entitlement(s) as stated hereunder:");
                    winword.Selection.TypeParagraph();
                    winword.Selection.TypeBackspace();

                    //winword.Selection.InsertParagraphAfter();
                    //winword.Selection.type
                    //winword.Selection.TypeBackspace();


                    //// Go to 2nd level
                    //winword.Selection.Range.ListFormat.ListIndent();

                    //winword.Selection.TypeText("Child Item A.1");//a.
                    //winword.Selection.TypeParagraph();
                    //winword.Selection.TypeText("Child Item A.2");//b.
                    //winword.Selection.TypeParagraph();

                    //// Back to 1st level
                    //winword.Selection.Range.ListFormat.ListOutdent();
                    //winword.Selection.TypeText("Root Item B");//2.
                    //winword.Selection.TypeParagraph();

                    //// Go to 2nd level
                    //winword.Selection.Range.ListFormat.ListIndent();
                    //winword.Selection.TypeText("Child Item B.1");//a.
                    //winword.Selection.TypeParagraph();
                    //winword.Selection.TypeText("Child Item B.2");//b.
                    //winword.Selection.TypeParagraph();

                    ////// Delete empty item generated by winword.Selection.TypeParagraph();
                    ////winword.Selection.TypeBackspace();

                    //winword.Selection.TypeParagraph();  // Simulate typing in MS Word

                    //Paragraph para5 = document.Content.Paragraphs.Add(ref missing);
                    //para5.Range.Select();

                    //// Apply multi level list
                    //winword.Selection.Range.ListFormat.ApplyListTemplateWithLevel(listGallery.ListTemplates[1], ContinuePreviousList: false,
                    //    ApplyTo: WdListApplyTo.wdListApplyToWholeList,
                    //    DefaultListBehavior: WdDefaultListBehavior.wdWord10ListBehavior);

                    ////First level
                    ////winword.Selection.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    //winword.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;






                    #endregion

                    #region Header Page No

                    ////Insert Next Page section break so that numbering can start at 1
                    //Word.Range rngPageNum = winword.Selection.Range;
                    //rngPageNum.InsertBreak(WdBreakType.wdSectionBreakNextPage);


                    foreach (Section section in document.Sections)
                    {
                        //Get the header range and add the header details.
                        Range headerRange = document.Sections[1].Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                        headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        headerRange.Font.ColorIndex = WdColorIndex.wdBlack;
                        headerRange.Font.Size = 11;
                        headerRange.Font.Name = "Calibri (Body)";

                        //use the field SectionPages instead of NumPages
                        object TotalPages = WdFieldType.wdFieldSectionPages;
                        object CurrentPage = WdFieldType.wdFieldPage;

                        headerRange.Fields.Add(headerRange, ref CurrentPage, ref missing, true);
                        headerRange.InsertBefore("PAGE ");
                        headerRange.Collapse(WdCollapseDirection.wdCollapseEnd);
                        //headerRange.Fields.Add(headerRange, ref TotalPages, ref missing, false);

                    }


                    #endregion

                    #region Footer Text

                    foreach (Section section in document.Sections)
                    {
                        //Get the header range and add the header details.  
                        Range footerRange = document.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        footerRange.Fields.Add(footerRange, WdFieldType.wdFieldPage);
                        footerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        footerRange.Font.ColorIndex = WdColorIndex.wdBlack;
                        footerRange.Font.Size = 11;
                        footerRange.Font.Name = "Calibri (Body)";
                        footerRange.Text = "TESTATOR                                                      WITHNESS 1                                                       WITHNESS 2";
                    }


                    #endregion

                    #region Saving Document
                    //Save the document  
                    object filename = sfd.FileName;
                    document.SaveAs2(ref filename);
                    #endregion

                    #region Release Object
                    document.Close(ref missing, ref missing, ref missing);
                    document = null;
                    winword.Quit(ref missing, ref missing, ref missing);
                    winword = null;
                    #endregion

                    frmLoading.CloseForm();

                    //MessageBox.Show("Document created successfully !");

                    if (File.Exists(sfd.FileName))
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);

                        System.Windows.Forms.Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    frmLoading.CloseForm();
                }
            }
        }

        private void txtHeaderRow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

      
        private void FinexDataUpload_Load(object sender, EventArgs e)
        {
            //#region Testing Shortcut
            //path = "D:\\Users\\Jun\\Desktop\\(PERSONAL)\\SmartBase8\\Finex & CO\\Data\\BASIC WILL QUESTIONNAIRE .xlsx";

            //if (path != null)
            //{
            //    ReadExcel(path);
            //    btnWill.Visible = false;

            //    LoadDetail(0);

            //    DataTable dt = (DataTable)dgvDetail.DataSource;

            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        FinexWill frm = new FinexWill(dt)
            //        {
            //            StartPosition = FormStartPosition.CenterScreen
            //        };

            //        frm.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Detail data not found.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("path not found");
            //}
            //#endregion
        }

       

        private void FinexDataUpload_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Application.Exit();
            frmLogIn frm = new frmLogIn("Emmeline");
            frm.Show();
        }

        private void btnStockTally_Click(object sender, EventArgs e)
        {
            frmInOutEdit frm = new frmInOutEdit(dt_DataSource, false, false);

            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();//Item Edit


            if (frmInOutEdit.TrfSuccess)
            {
                MessageBox.Show("Stock tally successfully!");
            }
        }
    }
}
