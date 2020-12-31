# Password Protected PDF Demo

This is a sample application that generates a password protected pdf.

* I have used [mpdf with yii](https://github.com/kartik-v/yii2-mpdf) a few years back
* I wanted to create Password Protected PDF using .Net MVC
* I have used [PdfSharp](http://www.pdfsharp.net/) library for this
* For the sample, I am passing hardcoded string only to create the PDF
* Here is the Sample code that does the job and downloads the file or checkout GeneratePdf Action in HomeController

````Csharp
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
````