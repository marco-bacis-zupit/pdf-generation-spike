// MigraDoc - Creating Documents on the Fly
// See the LICENSE file in the solution root for more information.

using MigraDoc.DocumentObjectModel;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

namespace MigraDocExample
{

    public class Styles
    {
        public static void DefineStyles(Document document)
        {
            // Get the predefined style Normal.
            var style = document.Styles[StyleNames.Normal]!;
            style.Font.Name = "Inter";

            style = document.Styles[StyleNames.Heading1]!;
            style.Font.Name = "Inter";
            style.Font.Size = 16;
            style.Font.Color = CustomColors.Black;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 6;
            // Set KeepWithNext for all headings to prevent headings from appearing all alone
            // at the bottom of a page. The other headings inherit this from Heading1.
            style.ParagraphFormat.KeepWithNext = true;

            style = document.Styles[StyleNames.Heading2]!;
            style.Font.Size = 14;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles[StyleNames.Heading3]!;
            style.Font.Size = 12;
            style.Font.Italic = true;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 3;

            style = document.Styles[StyleNames.Header]!;
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer]!;
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);
        }
    }
}