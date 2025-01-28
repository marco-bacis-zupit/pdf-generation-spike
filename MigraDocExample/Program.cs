using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.IO;
using MigraDoc.Rendering;
using MigraDocExample;
using PdfSharp.Pdf;
using PdfSharp.Quality;
using Styles = MigraDocExample.Styles;

#if CORE
// Core build does not use Windows fonts,
// so set a FontResolver that handles the fonts our samples need.
GlobalFontSettings.FontResolver = new SamplesFontResolver();
#endif

var document = new Document
{
    Info =
    {
        Title = "Offerta #14",
        Subject = "Offerta",
        Author = "Tassullo"
    }
};

Styles.DefineStyles(document);

Section section = document.AddSection();
Invoice.OfferMainInfo(section, Data.MainInfo);
Invoice.ArticlesTable(section, Data.Articles);
Invoice.ServicesTable(section, Data.Services);

// Write MigraDoc DDL to a file.
DdlWriter.WriteToFile(document, "MigraDoc.mdddl");

// Create a renderer for the MigraDoc document.
var pdfRenderer = new PdfDocumentRenderer
{
    // Associate the MigraDoc document with a renderer.
    Document = document,
    PdfDocument =
    {
        // Change some settings before rendering the MigraDoc document.
        PageLayout = PdfPageLayout.SinglePage,
        ViewerPreferences =
        {
            FitWindow = true
        }
    }
};

// Layout and render document to PDF.
pdfRenderer.RenderDocument();

// Save the document...
var filename = PdfFileUtility.GetTempPdfFullFileName("example.pdf");
pdfRenderer.PdfDocument.Save(filename);

// ...and start a viewer.
PdfFileUtility.ShowDocument(filename);