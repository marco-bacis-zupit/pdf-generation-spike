using System.Reflection;
using System.Runtime.Loader;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace TestDinkToPdf;

internal class CustomAssemblyLoadContext : AssemblyLoadContext
{
    public IntPtr LoadUnmanagedLibrary(string absolutePath)
    {
        return LoadUnmanagedDll(absolutePath);
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        return LoadUnmanagedDllFromPath(unmanagedDllName);
    }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        throw new NotImplementedException();
    }
}

public static class WkHtmlToPdf
{
    /// <summary>
    ///     Load pdf generator dlls
    /// </summary>
    public static void Preload()
    {
        var wkHtmlToPdfContext = new CustomAssemblyLoadContext();
        var architectureFolder = IntPtr.Size == 8 ? "64bit" : "32bit";
        var wkHtmlToPdfPath = Path.Combine(AppContext.BaseDirectory, "lib", architectureFolder, "libwkhtmltox");

        wkHtmlToPdfContext.LoadUnmanagedLibrary(wkHtmlToPdfPath);
    }
}

public class TemplateHelper
{
    private readonly RazorLightEngine _engine;
    private readonly ILogger<TemplateHelper> _logger;

    public TemplateHelper(ILogger<TemplateHelper> logger)
    {
        _logger = logger;
        _engine = new RazorLightEngineBuilder().UseEmbeddedResourcesProject(typeof(TemplateHelper)).UseMemoryCachingProvider().Build();
    }

    private async Task<string> Compile(string templateName, string html, object model)
    {
        return await _engine.CompileRenderStringAsync(templateName, html, model);
    }

    public async Task<string> CompileFromPath(string path, object model)
    {
        try
        {
            var html = await File.ReadAllTextAsync(path);
            html = html.Replace("@media", "@@media");
            var filename = Path.GetFileName(path);
            return await Compile(filename, html, model);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Email Template compilation error");
            return "Missing template";
        }
    }
}

public class PdfGenerator
    {
        private readonly IConverter _converter;
        private readonly TemplateHelper _templateHelper;
        private readonly ILogger<PdfGenerator> _logger;

        public PdfGenerator(IConverter converter, TemplateHelper templateHelper,ILogger<PdfGenerator> logger)
        {
            _converter = converter;
            _templateHelper = templateHelper;
            _logger = logger;
        }

        private string GetTemplatePath(string filename)
        {
            return Path.Combine("Templates", "pdf", filename);
        }

        public async Task<byte[]> CompileTemplateToPdf(
            string path,
            object model,
            PechkinPaperSize paperSize,
            Orientation orientation = Orientation.Landscape,
            MarginSettings margins = null,
            bool pageNumbers = false
        )
        {
            _logger.LogInformation($"Compiling template {path} to pdf");
            var template = await _templateHelper.CompileFromPath(GetTemplatePath(path), model);
            _logger.LogInformation($"Converting to pdf");
            byte[] ret = ConvertHtmlToPdf(template, paperSize, orientation, pageNumbers, margins);
            _logger.LogInformation($"Done");
            return ret;
        }

        public async Task<byte[]> CompileTemplateToPdf(
            string path,
            object model,
            Orientation orientation = Orientation.Landscape,
            bool pageNumbers = false,
            PaperKind paperKind = PaperKind.A4Plus,
            MarginSettings margins = null
        )
        {
            return await CompileTemplateToPdf(path, model, (PechkinPaperSize)paperKind, orientation, margins, pageNumbers);
        }

        public byte[] ConvertHtmlToPdf(
            string html,
            PechkinPaperSize paperSize,
            Orientation orientation = Orientation.Portrait,
            bool pageNumbers = false,
            MarginSettings margins = null
        )
        {
            var globals = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = orientation,
                PaperSize = paperSize,
            };
            if (margins != null)
            {
                globals.Margins = margins;
            }

            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = globals,
                Objects =
                {
                    new ObjectSettings
                    {
                        PagesCount = true,
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            if (pageNumbers)
                doc.Objects.First().FooterSettings = new FooterSettings()
                {
                    FontSize = 9,
                    Right = "Pagina [page] di [toPage]",
                    Line = true,
                    Spacing = 2.812
                };
            return _converter.Convert(doc);
        }
    }