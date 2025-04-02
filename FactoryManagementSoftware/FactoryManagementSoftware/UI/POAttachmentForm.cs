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
using System.IO;
using System.Net.Http;
using System.Threading;

namespace FactoryManagementSoftware.UI
{
    public partial class POAttachmentForm : Form
    {
        #region Fields and Properties
        private readonly Tool tool = new Tool();
        private readonly Text text = new Text();
        private string path = string.Empty;
        private readonly string serverBasePath = @"\\ADMIN001\Admin Server\( OFFICE )\(ERP)\(PO_Documents)";
        private readonly HttpClient httpClient = new HttpClient();
        #endregion

        #region Initialization and Loading

        private void panelDragDrop_Paint(object sender, PaintEventArgs e)
        {
            // Create a pen with dash style
            using (Pen dashPen = new Pen(Color.Black, 1))
            {
                dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                // Draw dashed rectangle around the panel
                Rectangle rect = new Rectangle(0, 0, panelDragDrop.Width - 1, panelDragDrop.Height - 1);
                e.Graphics.DrawRectangle(dashPen, rect);
            }
        }

        public POAttachmentForm()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
            // Calculate center position
            int totalWidth = btnBrowse.Width + btnAdd.Width + 10; // 10px spacing
            int startX = (panelDragDrop.Width - totalWidth) / 2;

            // Set button positions
            btnBrowse.Left = startX;
            btnAdd.Left = startX + btnBrowse.Width + 10;

            // Add resize handler to keep buttons centered when form resizes
            this.Resize += (s, e) => {
                int newStartX = (panelDragDrop.Width - totalWidth) / 2;
                btnBrowse.Left = newStartX;
                btnAdd.Left = newStartX + btnBrowse.Width + 10;
            };

            // Set up drag and drop for the entire form
            this.AllowDrop = true;
            this.DragEnter += POAttachmentForm_DragEnter;
            this.DragDrop += POAttachmentForm_DragDrop;
            // In your form's constructor or initialization method

            // Position buttons in the bottom panel
            int totalButtonWidth = btnOpenSelected.Width + btnViewSummary.Width + btnRefreshList.Width + 20; // 20 for spacing
            startX = (panelButtons.Width - totalButtonWidth) / 2;

            btnOpenSelected.Left = startX;
            btnViewSummary.Left = btnOpenSelected.Right + 10;
            btnRefreshList.Left = btnViewSummary.Right + 10;

            // Vertical position (center in panel)
            int centerY = (panelButtons.Height - btnOpenSelected.Height) / 2;
            btnOpenSelected.Top = centerY;
            btnViewSummary.Top = centerY;
            btnRefreshList.Top = centerY;

            // Add resize handler for buttons in bottom panel
            panelButtons.Resize += (s, e) => {
                int newStartX = (panelButtons.Width - totalButtonWidth) / 2;
                btnOpenSelected.Left = newStartX;
                btnViewSummary.Left = btnOpenSelected.Right + 10;
                btnRefreshList.Left = btnViewSummary.Right + 10;
            };

            // Set up DataGridView event handler
            dgvMainList.CellDoubleClick += dgvMainList_CellDoubleClick;

            // Ensure server directory exists
            EnsureDirectoryExists(serverBasePath);

            // Load existing PDF files
            LoadPDFFilesToDataGridView();
        }
        #endregion

        private void SetupDataGridView()
        {
            // Clear any existing columns and data
            dgvMainList.Columns.Clear();
            dgvMainList.DataSource = null;

            // Add columns
            dgvMainList.Columns.Add("colFileName", "Filename");
            dgvMainList.Columns.Add("colDateModified", "Date Modified");
            dgvMainList.Columns.Add("colFileSize", "File Size");

            // Configure appearance
            dgvMainList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMainList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMainList.AllowUserToAddRows = false;
            dgvMainList.AllowUserToDeleteRows = false;
            dgvMainList.ReadOnly = true;
            dgvMainList.MultiSelect = false;
        }

        #region File Selection Methods
       

        private void POAttachmentForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1 && Path.GetExtension(files[0]).ToLower() == ".pdf")
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void POAttachmentForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                path = files[0];
                lblDragInstructions.Text = Path.GetFileName(path);

                // Assuming you have a PictureBox named pictureBox1 in your form
                picDocument.Image = Properties.Resources.icons8_check_file_100;
                btnAdd.Enabled = true; // Enable the Add button
            }
        }
        #endregion

        #region File Saving and Processing
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                lblDragInstructions.ForeColor = Color.Red;
                lblDragInstructions.Text = "Please select a valid PDF file.";
                btnAdd.Enabled = false;
                return;
            }
            try
            {
                // Get original filename
                string originalFileName = Path.GetFileName(path);

                // Create a unique filename based on original name to avoid overwrites
                string poNumber = GetCurrentPONumber();
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string fileName = $"{fileNameWithoutExt}_{timestamp}.pdf";

                string destinationPath = Path.Combine(serverBasePath, fileName);

                // Copy file with progress indication
                using (var progressForm = new FileProgressForm())
                {
                    progressForm.Show(this);
                    CopyFileWithProgress(path, destinationPath, progressForm.UpdateProgress);
                    progressForm.Close();
                }

                // Save the file path to database
                SavePODocumentPath(poNumber, destinationPath);

                //// Generate AI summary if option is selected
                //if (chkGenerateSummary.Checked)
                //{
                //    GeneratePOSummary(destinationPath);
                //}

                MessageBox.Show("P/O document saved successfully.", "Success",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the DataGridView to show the newly added file
                LoadPDFFilesToDataGridView();

                // After successful add
                lblDragInstructions.ForeColor = SystemColors.ControlText; // Reset color
                lblDragInstructions.Text = "Drag and drop your PDF file here, or click below to browse.";
                picDocument.Image = Properties.Resources.icons8_pdf_100;

                path = string.Empty;
                btnAdd.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving document: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to copy file with progress
        private void CopyFileWithProgress(string sourcePath, string destinationPath, Action<int> progressCallback)
        {
            const int bufferSize = 1024 * 1024; // 1MB buffer
            byte[] buffer = new byte[bufferSize];

            using (FileStream source = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            using (FileStream destination = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
            {
                long fileLength = source.Length;
                long totalBytes = 0;
                int currentBlockSize;

                while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    destination.Write(buffer, 0, currentBlockSize);
                    totalBytes += currentBlockSize;
                    int progressPercentage = (int)((totalBytes * 100) / fileLength);
                    progressCallback(progressPercentage);

                    // Allow UI to update
                    Application.DoEvents();
                }
            }
        }

        // Placeholder for database integration
        private string GetCurrentPONumber()
        {
            // In the future, get the current PO number from your form context
            return DateTime.Now.ToString("yyyyMMdd");
        }

        // Placeholder for database integration
        private void SavePODocumentPath(string poNumber, string filePath)
        {
            // In the future, implement your database saving logic here
        }

        // Helper method to ensure directory exists
        private void EnsureDirectoryExists(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating directory: {ex.Message}\nPlease ensure you have proper network permissions.",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region PDF File Listing

        private void LoadPDFFilesToDataGridView()
        {
            try
            {
                SetupDataGridView();

                // Clear existing data
                dgvMainList.Rows.Clear();
                // Get all PDF files from the server directory
                if (!Directory.Exists(serverBasePath))
                {
                    MessageBox.Show("Server directory not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] pdfFiles = Directory.GetFiles(serverBasePath, "*.pdf");
                List<FileInfo> fileInfoList = new List<FileInfo>();

                foreach (string filePath in pdfFiles)
                {
                    fileInfoList.Add(new FileInfo(filePath));
                }

                // Sort by last write time descending (newest first)
                fileInfoList = fileInfoList.OrderByDescending(f => f.LastWriteTime).ToList();

                // Add to DataGridView
                foreach (FileInfo fileInfo in fileInfoList)
                {
                    int rowIndex = dgvMainList.Rows.Add();
                    DataGridViewRow row = dgvMainList.Rows[rowIndex];

                    row.Cells["colFileName"].Value = fileInfo.Name;
                    row.Cells["colDateModified"].Value = fileInfo.LastWriteTime.ToString("MM/dd/yyyy hh:mm tt");
                    row.Cells["colFileSize"].Value = FormatFileSize(fileInfo.Length);

                    // Add a hidden value for the full path
                    row.Tag = fileInfo.FullName;
                }

                // Enable/disable buttons based on selection
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PDF files: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateButtonStates()
        {
            bool hasSelection = dgvMainList.SelectedRows.Count > 0;
            btnOpenSelected.Enabled = hasSelection;
            btnViewSummary.Enabled = hasSelection;
        }

        // Helper method to set up DataGridView columns and formatting
      

        // Helper method to format file size (e.g., "1.5 MB" instead of bytes)
        private string FormatFileSize(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            decimal number = bytes;

            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }

            return $"{number:n1} {suffixes[counter]}";
        }

        private void dgvMainList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    string filePath = dgvMainList.Rows[e.RowIndex].Tag.ToString();
                    System.Diagnostics.Process.Start(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening PDF: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnViewSummary_Click(object sender, EventArgs e)
        {
            if (dgvMainList.SelectedRows.Count > 0)
            {
                try
                {
                    string filePath = dgvMainList.SelectedRows[0].Tag.ToString();
                    string summaryPath = Path.Combine(
                        Path.GetDirectoryName(filePath),
                        Path.GetFileNameWithoutExtension(filePath) + "_summary.txt");

                    if (File.Exists(summaryPath))
                    {
                        string summaryContent = File.ReadAllText(summaryPath);
                        using (var summaryForm = new POSummaryViewForm(summaryContent))
                        {
                            summaryForm.ShowDialog(this);
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("No summary exists for this PO. Would you like to generate one now?",
                            "Generate Summary", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            GeneratePOSummary(filePath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error viewing summary: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region AI PO Summary Generation
        private async void GeneratePOSummary(string pdfFilePath)
        {
            try
            {
                using (var loadingForm = new LoadingForm("Generating PO summary..."))
                {
                    loadingForm.Show(this);

                    // Generate AI summary asynchronously
                    string summary = await Task.Run(() => ExtractPOSummaryWithAI(pdfFilePath));

                    // Save summary to file
                    string summaryPath = Path.Combine(
                        Path.GetDirectoryName(pdfFilePath),
                        Path.GetFileNameWithoutExtension(pdfFilePath) + "_summary.txt");

                    File.WriteAllText(summaryPath, summary);

                    loadingForm.Close();

                    // Refresh the list to show summary status
                    LoadPDFFilesToDataGridView();

                    // Show the summary
                    using (var summaryForm = new POSummaryViewForm(summary))
                    {
                        summaryForm.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating summary: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ExtractPOSummaryWithAI(string pdfFilePath)
        {
            // This is a placeholder for AI integration
            // In the next step, we'll implement actual API integration

            // Simulate processing time
            Thread.Sleep(2000);

            // Return placeholder data
            return "PO Summary Details (Placeholder)\n\n" +
                   "PO Number: " + Path.GetFileNameWithoutExtension(pdfFilePath) + "\n" +
                   "Customer: Sample Customer\n" +
                   "Delivery Address: 123 Main Street, Anytown\n\n" +
                   "Items:\n" +
                   "- Item 1: Widget A (10 units)\n" +
                   "- Item 2: Widget B (5 units)\n\n" +
                   "Special Instructions: Handle with care";
        }
        #endregion

        #region Helper Forms
        // Progress form for file copying
        public class FileProgressForm : Form
        {
            private ProgressBar progressBar;

            public FileProgressForm()
            {
                this.Text = "Copying File...";
                this.Size = new Size(400, 100);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                progressBar = new ProgressBar();
                progressBar.Minimum = 0;
                progressBar.Maximum = 100;
                progressBar.Value = 0;
                progressBar.Dock = DockStyle.Fill;
                progressBar.Margin = new Padding(10);

                this.Controls.Add(progressBar);
            }

            public void UpdateProgress(int percentage)
            {
                if (progressBar.InvokeRequired)
                {
                    progressBar.Invoke(new Action<int>(UpdateProgress), percentage);
                }
                else
                {
                    progressBar.Value = percentage;
                }
            }
        }

        // Loading form for AI processing
        public class LoadingForm : Form
        {
            public LoadingForm(string message)
            {
                this.Text = "Processing";
                this.Size = new Size(400, 120);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                Label lblMessage = new Label();
                lblMessage.Text = message;
                lblMessage.AutoSize = false;
                lblMessage.TextAlign = ContentAlignment.MiddleCenter;
                lblMessage.Dock = DockStyle.Top;
                lblMessage.Height = 40;

                PictureBox spinner = new PictureBox();
                // You'd need to add a spinner/loading GIF to your resources
                // spinner.Image = Properties.Resources.loading;
                spinner.SizeMode = PictureBoxSizeMode.CenterImage;
                spinner.Dock = DockStyle.Fill;

                this.Controls.Add(spinner);
                this.Controls.Add(lblMessage);
            }
        }

        // Summary view form
        public class POSummaryViewForm : Form
        {
            public POSummaryViewForm(string summaryContent)
            {
                this.Text = "PO Summary";
                this.Size = new Size(600, 400);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.StartPosition = FormStartPosition.CenterParent;
                this.MinimizeBox = false;

                TextBox txtSummary = new TextBox();
                txtSummary.Multiline = true;
                txtSummary.ReadOnly = true;
                txtSummary.ScrollBars = ScrollBars.Vertical;
                txtSummary.Dock = DockStyle.Fill;
                txtSummary.Text = summaryContent;
                txtSummary.Font = new Font("Consolas", 10);

                this.Controls.Add(txtSummary);
            }
        }
        #endregion

        private void btnOpenSelected_Click(object sender, EventArgs e)
        {
            if (dgvMainList.SelectedRows.Count > 0)
            {
                try
                {
                    // Changed this line to use Tag instead of a column
                    string filePath = dgvMainList.SelectedRows[0].Tag.ToString();
                    System.Diagnostics.Process.Start(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening PDF: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            LoadPDFFilesToDataGridView();
        }


        private void dgvMainList_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void POAttachmentForm_Load(object sender, EventArgs e)
        {
            // Center buttons at the bottom
            int totalWidth = btnOpenSelected.Width + btnViewSummary.Width + btnRefreshList.Width + 20; // 20px for spacing
            int startPos = (this.ClientSize.Width - totalWidth) / 2;

            btnOpenSelected.Left = startPos;
            btnViewSummary.Left = btnOpenSelected.Right + 10;
            btnRefreshList.Left = btnViewSummary.Right + 10;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                Title = "Select P/O PDF"
            };

            DialogResult re = fd.ShowDialog();
            if (re == DialogResult.OK)
            {
                path = fd.FileName;
                lblDragInstructions.Text = Path.GetFileName(path);
                picDocument.Image = Properties.Resources.icons8_check_file_100;

                btnAdd.Enabled = true; // Enable the Add button
            }
        }

    }
}