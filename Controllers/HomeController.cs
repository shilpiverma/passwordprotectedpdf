using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shilpi.PasswordProtectPdf.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult GeneratePdf() {
            PdfDocument pdfDocument = new PdfDocument();
            var page = pdfDocument.AddPage();
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            var gfx = XGraphics.FromPdfPage(page);
            gfx.DrawString("Hello World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);
            //pdfDocument.Save("test.pdf");
            PdfSecuritySettings securitySettings = pdfDocument.SecuritySettings;

            securitySettings.UserPassword = "password";
            securitySettings.OwnerPassword = "owner";

            MemoryStream stream = new MemoryStream();
            pdfDocument.Save(stream, false);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", stream.Length.ToString());
            Response.BinaryWrite(stream.ToArray());
            Response.AddHeader("Content-Type", "application/octet-stream");
            Response.AddHeader("Content-Transfer-Encoding", "Binary");
            Response.AddHeader("Content-disposition", "attachment; filename=\"my_secure_pdf.pdf\"");
            //Response.WriteFile(HttpRuntime.AppDomainAppPath + @"ideaPark\DesktopModules\ResourceModule\pdf_resources\IdeaPark_ER_diagram.pdf");
            Response.Flush();
            stream.Close();
            Response.End();
            return View();
        }


        public ActionResult DownloadPdf()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}