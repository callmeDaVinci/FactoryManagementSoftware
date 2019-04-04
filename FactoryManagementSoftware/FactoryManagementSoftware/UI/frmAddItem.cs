using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace FactoryManagementSoftware.UI
{
    public partial class frmAddItem : Form
    {
        public frmAddItem()
        {
            InitializeComponent();
        }

        private string path = null;
        private string excelName = null;
        private DataTable dt;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "File Excel|*.xlsx";

            DialogResult re = fd.ShowDialog();
            excelName = fd.SafeFileName;

            if(re == DialogResult.OK)
            {
                path = fd.FileName;
                txtPath.Text = path;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if(path != null)
            {
                ReadExcel(path);
            }
            else
            {
                MessageBox.Show("path not found");
            }
        }

        string code, name;

        private void frmAddItem_Load(object sender, EventArgs e)
        {

        }

        private void addNewDataTable()
        {
            dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Name");
            dt.Columns.Add("Marks");
            DataRow _ravi = dt.NewRow();
            _ravi["Name"] = "ravi";
            _ravi["Marks"] = "500";
            dt.Rows.Add(_ravi);
        }

        public void ReadExcel(string Path)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type

            DataTable dt = new DataTable();
            dt.Clear();

            dt.Columns.Add("Code");
            dt.Columns.Add("Name");

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            int rCnt;
            int cCnt;
            int rw = 0;
            int cl = 0;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(Path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;


            //int r = 2;
            for (rCnt = 2; rCnt <= rw; rCnt++)
            {

                for (cCnt = 1; cCnt <= cl; cCnt++)
                {
                    code = Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2);
                    name = Convert.ToString((range.Cells[rCnt, 2] as Excel.Range).Value2);
                }

                if (!string.IsNullOrEmpty(code))
                {
                    DataRow _ravi = dt.NewRow();
                    _ravi["Code"] = code;
                    _ravi["Name"] = name;
                    dt.Rows.Add(_ravi);
                    code = null;
                }
            }

            dataGridView_List_acc.DataSource = dt;
            dgvUIEdit(dataGridView_List_acc);

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void dataGridView_List_acc_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            object tempObject1 = e.CellValue1;
            object tempObject2 = e.CellValue2;
            if (!(tempObject1 is null) && !(tempObject2 is null))
            {
                if (float.TryParse(tempObject1.ToString(), out float tmp) && float.TryParse(tempObject2.ToString(), out tmp))
                {
                    e.SortResult = float.Parse(tempObject1.ToString()).CompareTo(float.Parse(tempObject2.ToString()));
                    e.Handled = true;//pass by the default sorting
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataTable dt = dataGridView_List_acc.DataSource as DataTable;
            //Create the new row
            DataRow row = dt.NewRow();

            

            //Add the row to data table
            dt.Rows.Add(row);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView_List_acc.DataSource as DataTable;
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
        }

        private void dgvUIEdit(DataGridView dgv)
        {
            dgv.Columns["Code"].HeaderText = "CODE";
            dgv.Columns["Name"].HeaderText = "NAME";

            dgv.Columns["Code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

    }
}
