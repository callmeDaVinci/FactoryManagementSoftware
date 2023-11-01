using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FactoryManagementSoftware.BLL;
using FactoryManagementSoftware.DAL;
using FactoryManagementSoftware.Module;

namespace FactoryManagementSoftware.UI
{
    public partial class frmReadSemenyihData : Form
    {
        public frmReadSemenyihData()
        {
            InitializeComponent();
        }

        trfHistDAL dalTrfHist = new trfHistDAL();

        // Usage
        DataTable DT_TRF_HIST = new DataTable();
        // Populate your dataTable

        // Save DataTable to text file
        string savePath = "D:\\Users\\Jun\\Desktop\\SemenyihData.txt";


        // Read DataTable from text file
        string readPath = "G:\\Other computers\\SemenyihAdminPC\\Admin Server\\(DB TRANSFER)\\SemenyihData.txt";

        // Method to save DataTable to a text file
        public static void SaveDataTableToTextFile(DataTable dt, string filePath, char delimiter = ',')
        {
            StringBuilder sb = new StringBuilder();

            // Add headers
            string[] columnNames = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                columnNames[i] = $"\"{dt.Columns[i].ColumnName.Replace("\"", "\"\"")}\""; // Enclose in quotes and escape quotes
            }
            sb.AppendLine(string.Join(delimiter.ToString(), columnNames));

            // Add row data
            foreach (DataRow row in dt.Rows)
            {
                string[] fields = new string[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    // Remove newlines and enclose in quotes, also escape any existing quotes
                    fields[i] = $"\"{row[i].ToString().Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\"";
                }
                sb.AppendLine(string.Join(delimiter.ToString(), fields));
            }

            // Save to file
            File.WriteAllText(filePath, sb.ToString());
        }


        // Method to read data from a text file to DataTable
        public static DataTable ReadTextFileToDataTable(string filePath, char delimiter = ',')
        {
            DataTable dt = new DataTable();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string[] headers = ParseLine(sr.ReadLine(), delimiter);
                foreach (string header in headers)
                {
                    dt.Columns.Add(header.Trim('"')); // Remove enclosing quotes
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = ParseLine(sr.ReadLine(), delimiter);
                    if (true)//rows.Length == headers.Length
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim('"'); // Remove enclosing quotes
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        // Helper method to handle quoted fields
        private static string[] ParseLine(string line, char delimiter)
        {
            var fields = new List<string>();
            var field = new StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (inQuotes)
                {
                    if (i < line.Length - 1 && line[i] == '"' && line[i + 1] == '"') // Check for escaped quotes
                    {
                        field.Append('"');
                        i++; // Skip the next character
                    }
                    else if (line[i] == '"')
                    {
                        inQuotes = false;
                    }
                    else
                    {
                        field.Append(line[i]);
                    }
                }
                else
                {
                    if (line[i] == delimiter)
                    {
                        fields.Add(field.ToString());
                        field.Clear();
                    }
                    else if (line[i] == '"')
                    {
                        inQuotes = true;
                    }
                    else
                    {
                        field.Append(line[i]);
                    }
                }
            }
            fields.Add(field.ToString());

            return fields.ToArray();
        }


        private void frmReadSemenyihData_Load(object sender, EventArgs e)
        {
           
          

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmLoading.ShowLoadingScreen();

            DT_TRF_HIST = dalTrfHist.Select();

            
            SaveDataTableToTextFile(DT_TRF_HIST, savePath);
            frmLoading.CloseForm();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DataTable readDataTable = ReadTextFileToDataTable(readPath);
        }
    }
}
