using PdfSharp.Fonts;

namespace MigraDocExample;

public class InterFontResolver : IFontResolver
{
    public string DefaultFontName => "Inter";

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        // Assuming only regular font is used
        string fontName = "Inter";
        if (isBold)
            fontName += " Bold";
        if (isItalic)
            fontName += " Italic";

        return new FontResolverInfo(fontName);
    }

    public byte[] GetFont(string faceName)
    {
        string fontPath = "Assets/Inter.ttf";
        if (!faceName.StartsWith(DefaultFontName))
            return null;

        return File.ReadAllBytes(fontPath);
    }
}