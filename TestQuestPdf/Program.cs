// See https://aka.ms/new-console-template for more information

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

new InvoiceDocument(InvoiceDocumentDataSource.GetInvoiceDetails()).GeneratePdf("invoice.pdf");