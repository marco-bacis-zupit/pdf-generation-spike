using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace MigraDocExample;

public class Invoice
{

    public static void OfferMainInfo(Section section, OfferMainInfoResponse mainInfo)
    {
        section.AddImage("Assets/logo-tassullo-positive.png");
        
        Table table = section.AddTable();
        table.Style = "Table";
        
        
        Column column1 = table.AddColumn("9cm");
        Column column2 = table.AddColumn("9cm");
        column1.Format.Alignment = ParagraphAlignment.Left;
        column2.Format.Alignment = ParagraphAlignment.Right;
        
        Row row = table.AddRow();
        Paragraph companyInfo = row.Cells[0].AddParagraph($"{mainInfo.ContactFullName}\nVia Azienda 10, 00127 Roma (RM) - IT\nP.Iva 12345678901");
        
        companyInfo.Format.SpaceBefore = "0.2cm";
        
        Paragraph offerDetails = row.Cells[1].AddParagraph("# OFFERTA 14\nData 19 dicembre 2024");
        offerDetails.Format.Alignment = ParagraphAlignment.Right;
        
        offerDetails.Format.SpaceAfter = "0.5cm";

        table.Format.SpaceAfter = "1cm";
    }
    
    public static void ArticlesTable(Section section, OfferArticleSummaryResponse articles)
    {

        var title = section.AddParagraph("Articoli");
        title.Format.Font.Bold = true;
        title.Format.SpaceAfter = new Unit(0.5, UnitType.Centimeter);
        
        Table table = section.AddTable();
        table.Style = "Table";
        table.Borders.Color = Colors.LightGray;
        table.Borders.Width = 0.25;
        table.Borders.Left.Width = 0.5;
        table.Borders.Right.Width = 0.5;
        table.Rows.LeftIndent = 0;
        
        table.Rows.VerticalAlignment = VerticalAlignment.Center;

        // Add columns with specified widths
        table.AddColumn("2cm");
        table.AddColumn("3cm");
        table.AddColumn("2cm");
        table.AddColumn("2cm");
        table.AddColumn("1cm");
        table.AddColumn("2cm");
        table.AddColumn("2cm");
        table.AddColumn("2cm");
        table.AddColumn("2cm");

        Row header = table.AddRow();
        header.Borders.Left.Visible = false;
        header.Borders.Right.Visible = false;
        header.Format.Font.Bold = true;
        
        table.Columns[0].Format.Alignment = ParagraphAlignment.Left;
        table.Columns[1].Format.Alignment = ParagraphAlignment.Left;
        table.Columns[2].Format.Alignment = ParagraphAlignment.Left;
        table.Columns[3].Format.Alignment = ParagraphAlignment.Right;
        table.Columns[4].Format.Alignment = ParagraphAlignment.Right;
        table.Columns[5].Format.Alignment = ParagraphAlignment.Right;
        table.Columns[6].Format.Alignment = ParagraphAlignment.Left;
        table.Columns[7].Format.Alignment = ParagraphAlignment.Right;
        table.Columns[8].Format.Alignment = ParagraphAlignment.Right;
        
        header.Cells[0].AddParagraph("Cod. Art");
        header.Cells[1].AddParagraph("Descrizione");
        header.Cells[2].AddParagraph("Categoria");
        header.Cells[3].AddParagraph("Confezione");
        header.Cells[4].AddParagraph("Qta");
        header.Cells[5].AddParagraph("Listino (€/u.m.)");
        header.Cells[6].AddParagraph("Scontistica");
        header.Cells[7].AddParagraph("Prezzo netto");
        header.Cells[8].AddParagraph("Importo");
        
        header.Borders.Bottom.Width = 1.5;

        foreach (var item in articles.Articles)
        {
            var row = table.AddRow();
            row.Borders.Left.Visible = false;
            row.Borders.Right.Visible = false;
            row.Height = 30;
            
            row.Cells[0].AddParagraph(item.Code);
            row.Cells[1].AddParagraph(item.Description);
            row.Cells[2].AddParagraph("Cat. 01"); // Categoria
            row.Cells[3].AddParagraph($"{item.Quantity} Kg");
            row.Cells[4].AddParagraph("1");
            row.Cells[5].AddParagraph(string.Format("{0:0.00} €", item.Price));
            var discounts = item.Discounts.Select(d => (d * 100).ToString() + "%");
            row.Cells[6].AddParagraph(string.Join(", ", discounts)); // Scontistica
            row.Cells[7].AddParagraph(string.Format("{0:0.00} €", item.NetPrice));
            row.Cells[8].AddParagraph(string.Format("{0:0.00} €", item.TotalPrice));
        }
        
        var totalRow = table.AddRow();
        totalRow.Borders.Visible = false;
        totalRow.Height = 30;
        var totalText = string.Format("{0:0.00} €", articles.TotalAmount);
        var total = totalRow.Cells[8].AddParagraph(totalText);
        total.Format.Font.Bold = true;
    }

    public static void ServicesTable(Section section, OfferServicesSummaryResponse services)
    {
        var title = section.AddParagraph("Servizi");
        title.Format.Font.Bold = true;
        title.Format.SpaceAfter = new Unit(0.5, UnitType.Centimeter);
        
        Table table = section.AddTable();
        table.Style = "Table";
        table.Borders.Color = Colors.LightGray;
        table.Borders.Width = 0.25;
        table.Borders.Left.Width = 0.5;
        table.Borders.Right.Width = 0.5;
        table.Rows.LeftIndent = 0;
        
        table.Rows.VerticalAlignment = VerticalAlignment.Center;

        // Add columns with specified widths
        table.AddColumn("2cm");
        table.AddColumn("3cm");
        table.AddColumn("2cm");
        table.AddColumn("2cm");
        table.AddColumn("1cm");
        table.AddColumn("2cm");
        table.AddColumn("2cm");
        table.AddColumn("2cm");
        table.AddColumn("2cm");

        Row header = table.AddRow();
        header.Borders.Left.Visible = false;
        header.Borders.Right.Visible = false;
        header.Format.Font.Bold = true;
        
        table.Columns[0].Format.Alignment = ParagraphAlignment.Left;
        table.Columns[1].Format.Alignment = ParagraphAlignment.Left;
        table.Columns[2].Format.Alignment = ParagraphAlignment.Left;
        table.Columns[3].Format.Alignment = ParagraphAlignment.Right;
        table.Columns[4].Format.Alignment = ParagraphAlignment.Right;
        table.Columns[5].Format.Alignment = ParagraphAlignment.Right;
        
        header.Cells[0].AddParagraph("Cod. Art");
        header.Cells[1].AddParagraph("Descrizione");
        header.Cells[2].AddParagraph("Categoria");
        header.Cells[3].AddParagraph("Qta");
        header.Cells[4].AddParagraph("Listino");
        header.Cells[5].AddParagraph("Prezzo netto");
        
        header.Borders.Bottom.Width = 1.5;

        foreach (var item in services.Services)
        {
            var row = table.AddRow();
            row.Borders.Left.Visible = false;
            row.Borders.Right.Visible = false;
            row.Height = 30;
            
            row.Cells[0].AddParagraph(item.BusinessCentralCode);
            row.Cells[1].AddParagraph(item.Description);
            row.Cells[2].AddParagraph(item.TypeDescription); // Categoria
            row.Cells[3].AddParagraph($"{item.Quantity}");
            row.Cells[5].AddParagraph(string.Format("{0:0.00} €", item.Price));
            row.Cells[8].AddParagraph(string.Format("{0:0.00} €", item.TotalPrice));
        }
        
        var totalRow = table.AddRow();
        totalRow.Borders.Visible = false;
        totalRow.Height = 30;
        var totalText = string.Format("{0:0.00} €", services.TotalAmount);
        var total = totalRow.Cells[8].AddParagraph(totalText);
        total.Format.Font.Bold = true;
    }
}