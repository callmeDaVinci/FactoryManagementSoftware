using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FactoryManagementSoftware.DAL
{
    internal static class Utilities
    {
        public static void CopyHtmlToClipBoard(string htmlFragment)
        {
            string headerFormat
              = "Version:0.9\r\nStartHTML:{0:000000}\r\nEndHTML:{1:000000}"
              + "\r\nStartFragment:{2:000000}\r\nEndFragment:{3:000000}\r\n";

            string htmlHeader
              = "<html>\r\n<head>\r\n"
              + "<meta http-equiv=\"Content-Type\""
              + " content=\"text/html; charset=utf-8\">\r\n"
              + "<title>HTML clipboard</title>\r\n</head>\r\n<body>\r\n"
              + "<!--StartFragment-->";

            string htmlFooter = "<!--EndFragment-->\r\n</body>\r\n</html>\r\n";
            string headerSample = String.Format(headerFormat, 0, 0, 0, 0);

            Encoding encoding = Encoding.UTF8;
            int headerSize = encoding.GetByteCount(headerSample);
            int htmlHeaderSize = encoding.GetByteCount(htmlHeader);
            int htmlFragmentSize = encoding.GetByteCount(htmlFragment);
            int htmlFooterSize = encoding.GetByteCount(htmlFooter);

            string htmlResult
              = String.Format(
                  CultureInfo.InvariantCulture,
                  headerFormat,
                  /* StartHTML     */ headerSize,
                  /* EndHTML       */ headerSize + htmlHeaderSize + htmlFragmentSize + htmlFooterSize,
                  /* StartFragment */ headerSize + htmlHeaderSize,
                  /* EndFragment   */ headerSize + htmlHeaderSize + htmlFragmentSize)
              + htmlHeader
              + htmlFragment
              + htmlFooter;

            DataObject obj = new DataObject();
            obj.SetData(DataFormats.Html, new MemoryStream(encoding.GetBytes(htmlResult)));
            Clipboard.SetDataObject(obj, true);
        }
    }
}
