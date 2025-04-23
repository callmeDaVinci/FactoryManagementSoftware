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
using Newtonsoft.Json;
using System.Net.Http.Headers;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


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

        private Color _dashColor = Color.FromArgb(90, 171, 234);

        private void panelDragDrop_Paint(object sender, PaintEventArgs e)
        {
            // Create a pen with dash style
            using (Pen dashPen = new Pen(_dashColor, 1))
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

            // Set up drag and drop for the entire form
            this.AllowDrop = true;
            this.DragEnter += POAttachmentForm_DragEnter;
            this.DragDrop += POAttachmentForm_DragDrop;

            btnViewSummary.Enabled = false;
        }
        #endregion

        #region File Selection Methods


        private void POAttachmentForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1 && System.IO.Path.GetExtension(files[0]).ToLower() == ".pdf")
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
                lblDragInstructions.Text = System.IO.Path.GetFileName(path);

                PDFUploadSuccessful();
            }
        }
        #endregion

        #region File Saving and Processing

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


        #region PDF Processing and Summary Generation
        private async void btnViewSummary_Click(object sender, EventArgs e)
        {
            // Check if a file is selected
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                lblDragInstructions.ForeColor = Color.Red;
                lblDragInstructions.Text = "Please select a valid PDF file";
                btnViewSummary.Enabled = false;
                return;
            }

            try
            {
                using (var loadingForm = new LoadingForm("Analyzing PO document..."))
                {
                    loadingForm.Show(this);

                    // First, check if the PDF is scan-based or text-based
                    bool isTextBased = IsPdfTextBased(path);

                    if (isTextBased)
                    {
                        // Extract text using iTextSharp
                        string pdfText = ExtractTextFromPdf(path);

                        // Check if we got enough meaningful text
                        if (!string.IsNullOrWhiteSpace(pdfText) && pdfText.Length > 100)
                        {
                            // Analyze the extracted text with GPT
                            string summary = await AnalyzeTextWithGPT(pdfText, System.IO.Path.GetFileName(path));
                            loadingForm.Close();
                            ShowSummaryDialog(summary);
                            return;
                        }
                    }

                    // If we get here, it's a scan-based PDF or text extraction failed
                    loadingForm.Close();

                    // Ask if the user wants to enter data manually
                    DialogResult result = MessageBox.Show(
                        "This appears to be a scanned document or text extraction failed. Would you like to enter PO details manually?",
                        "Manual Entry Required",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        //ShowManualEntryForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Check if a PDF contains actual text or is just scanned images
        private bool IsPdfTextBased(string filePath)
        {
            try
            {
                // Use iTextSharp to check for text
                using (PdfReader reader = new PdfReader(filePath))
                {
                    // Check a few pages (or all if the document is small)
                    int pagesToCheck = Math.Min(reader.NumberOfPages, 3);

                    for (int i = 1; i <= pagesToCheck; i++)
                    {
                        string pageText = PdfTextExtractor.GetTextFromPage(reader, i);

                        // If we find a reasonable amount of text on any page, consider it text-based
                        if (pageText.Length > 100)
                        {
                            return true;
                        }
                    }

                    // Additional check for text objects (more thorough)
                    PdfDictionary page = reader.GetPageN(1);
                    PdfDictionary resources = page.GetAsDict(PdfName.RESOURCES);

                    if (resources != null)
                    {
                        PdfDictionary fonts = resources.GetAsDict(PdfName.FONT);
                        if (fonts != null && fonts.Size > 0)
                        {
                            // The presence of fonts is a good indicator of text
                            return true;
                        }
                    }

                    // If we get here, it's likely a scan-based PDF
                    return false;
                }
            }
            catch (Exception)
            {
                // If there's any error in analysis, assume it's not text-based to be safe
                return false;
            }
        }

        private async Task<string> AnalyzeTextWithGPT(string pdfText, string fileName)
        {
            using (HttpClient client = new HttpClient())
            {
              
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "");//

                // Create prompt with clearer guidance about customer identification
                string prompt = $"Extract information from this Purchase Order text (from file \"{fileName}\").\n\n" +
                                "IMPORTANT INSTRUCTIONS:\n" +
                                "1. In purchase orders, the entity ISSUING the PO is the CUSTOMER.\n" +
                                "2. The entity RECEIVING the PO is the SUPPLIER or VENDOR.\n" +
                                "3. SAFETY PLASTICS SDN BHD is the SUPPLIER. If you see it in the document, it is NOT the customer.\n" +
                                "4. The customer is the company SENDING the order TO Safety Plastics.\n\n" +
                                "Format your response exactly like this example, with plain text only:\n\n" +
                                "Customer Name: [Name of the company SENDING the PO TO Safety Plastics]\n\n" +
                                "Delivery Address:\n" +
                                "[Full Address where goods will be delivered]\n\n" +
                                "PO Number: [Number]\n\n" +
                                "Items:\n" +
                                "1. [Item Name] - [Quantity] - [Price]\n" +
                                "2. [Item Name] - [Quantity] - [Price]\n\n" +
                                "Total Price: [Amount]\n\n" +
                                "Remarks: [Any remarks or None if none]\n\n" +
                                "Here's the extracted text:\n\n" + pdfText;

                // Create request body with stronger system instruction
                var requestBody = new
                {
                    model = "gpt-4o",
                    messages = new[]
                    {
                new {
                    role = "system",
                    content = "You are an expert in purchase order analysis. IMPORTANT: In a PO, the customer is the company SENDING the order, and the supplier is the company RECEIVING the order. SAFETY PLASTICS SDN BHD is the supplier, not the customer. The customer is whoever is sending the PO to Safety Plastics."
                },
                new { role = "user", content = prompt }
            },
                    max_tokens = 1000
                };

                // Convert to JSON
                string jsonRequest = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Send request to OpenAI
                HttpResponseMessage response = await client.PostAsync(
                    "https://api.openai.com/v1/chat/completions", content);

                // Get and parse response
                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseJson);
                    return jsonResponse.choices[0].message.content;
                }
                else
                {
                    throw new Exception($"API Error: {responseJson}");
                }
            }
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

       
        #endregion

        private void POAttachmentForm_Load(object sender, EventArgs e)
        {

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
                lblDragInstructions.Text = System.IO.Path.GetFileName(path);

                PDFUploadSuccessful();
            }
        }

        private void PDFUploadSuccessful()
        {
            picDocument.Image = Properties.Resources.icons8_document_100;
            // for example, set the panel’s border color to green
            _dashColor = Color.Green;
            // force a repaint so the change appears immediately
            panelDragDrop.Invalidate();

            btnViewSummary.Enabled = true; // Enable the Add button
        }
        #region Chatgpt AI

        private async Task TestAPIConnection()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo", // Make sure you have access to this model
                    messages = new[]
                    {
                new { role = "user", content = "Hello, how are you?" }
            },
                    max_tokens = 50
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseJson = await response.Content.ReadAsStringAsync();

                string message = "";

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success! " + responseJson);
                    MessageBox.Show("Success! " + responseJson);
                }
                else
                {
                    Console.WriteLine("Error: " + responseJson);
                    MessageBox.Show("Error! " + responseJson);

                }


            }
        }


        private async Task<string> AnalyzePDFWithAI(string filePath)
        {
            try
            {
                // Convert the first page of the PDF to an image
                Bitmap pdfImage = ConvertPDFToImage(filePath, 1); // Get first page

                // Save the image to a temporary file
                string tempImagePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetFileNameWithoutExtension(filePath) + ".png");
                pdfImage.Save(tempImagePath, System.Drawing.Imaging.ImageFormat.Png);

                // Read the image bytes
                byte[] imageBytes = File.ReadAllBytes(tempImagePath);
                string base64Image = Convert.ToBase64String(imageBytes);

                // Set up the HTTP client
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", "");

                    // Create request with image instead of PDF
                    var requestBody = new
                    {
                        model = "gpt-4o",
                        messages = new object[]
                        {
                    new
                    {
                        role = "system",
                        content = "You are an expert in purchase order analysis. IMPORTANT: In a PO, the customer is the company SENDING the order, and the supplier is the company RECEIVING the order. SAFETY PLASTICS SDN BHD may be the supplier, not the customer. The customer is whoever is sending the PO to Safety Plastics."
                    },
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new
                            {
                                type = "text",
                                text = "Analyze this purchase order image and extract information in a plain text format with NO formatting, markdown, or special characters:\n\n" +
                                       "Customer Name: [Name of company SENDING the PO]\n\n" +
                                       "Delivery Address:\n" +
                                       "[Full Address]\n\n" +
                                       "PO Number: [Number]\n\n" +
                                       "Items:\n" +
                                       "1. [Item Name] - [Quantity] - [Price]\n" +
                                       "[...etc for all items]\n\n" +
                                       "Total Price: [Amount]\n\n" +
                                       "Remarks: [Any remarks or None if none]"
                            },
                            new
                            {
                                type = "image_url",
                                image_url = new
                                {
                                    url = $"data:image/png;base64,{base64Image}"
                                }
                            }
                        }
                    }
                        },
                        max_tokens = 1000
                    };

                    // Send request to OpenAI
                    string jsonRequest = JsonConvert.SerializeObject(requestBody);
                    HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(
                        "https://api.openai.com/v1/chat/completions", content);

                    string responseJson = await response.Content.ReadAsStringAsync();

                    // Clean up temporary file
                    try { File.Delete(tempImagePath); } catch { }

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseJson);
                        return jsonResponse.choices[0].message.content;
                    }
                    else
                    {
                        throw new Exception($"API Error: {responseJson}");
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error processing PDF: {ex.Message}";
            }
        }

        // Helper method to convert PDF to image
        private Bitmap ConvertPDFToImage(string pdfPath, int pageNumber)
        {
            // Using GhostscriptSharp or other PDF-to-image libraries
            // For this example, I'll use a simpler approach with iTextSharp

            using (var reader = new PdfReader(pdfPath))
            {
                // Load page
                var page = reader.GetPageN(pageNumber);
                var pageSize = reader.GetPageSize(pageNumber);

                // Create bitmap
                var bitmap = new Bitmap((int)pageSize.Width, (int)pageSize.Height);
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);

                    // This is a simple representation - for better quality, 
                    // you would use a proper PDF rendering library
                    var parser = new PdfReaderContentParser(reader);
                    var strategy = parser.ProcessContent(pageNumber, new SimpleTextExtractionStrategy());

                    // Draw some basic text
                    g.DrawString(strategy.GetResultantText(),
                        new Font("Arial", 12), Brushes.Black, 10, 10);
                }

                return bitmap;
            }

            // Note: For a production application, consider using a more robust 
            // PDF rendering solution like Ghostscript, PDFium, or a commercial library
        }

        private void ShowSummaryDialog(string summary)
        {
            Form summaryForm = new Form
            {
                Text = "PO Summary",
                Size = new Size(700, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            TextBox txtSummary = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 11),
                Text = summary,
                ScrollBars = ScrollBars.Vertical
            };

            Button btnCopy = new Button
            {
                Text = "Copy to Clipboard",
                Dock = DockStyle.Bottom,
                Height = 40
            };

            btnCopy.Click += (s, e) => {
                Clipboard.SetText(summary);
                MessageBox.Show("Copied to clipboard!");
            };

            summaryForm.Controls.Add(txtSummary);
            summaryForm.Controls.Add(btnCopy);

            summaryForm.ShowDialog();
        }


        #endregion

        #region extract text from pdf
        private string ExtractTextFromPdf(string pdfPath)
        {
            StringBuilder textBuilder = new StringBuilder();

            try
            {
                using (PdfReader reader = new PdfReader(pdfPath))
                {
                    int numberOfPages = reader.NumberOfPages;
                    for (int page = 1; page <= numberOfPages; page++)
                    {
                        // Extract text from each page
                        string pageText = PdfTextExtractor.GetTextFromPage(reader, page);
                        textBuilder.AppendLine(pageText);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error extracting text from PDF: " + ex.Message,
                                "Extraction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return textBuilder.ToString();
        }

        #endregion

    }
}