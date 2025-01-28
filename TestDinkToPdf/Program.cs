// See https://aka.ms/new-console-template for more information


using System.Net.Mime;
using DinkToPdf;
using HelloMigraDoc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TestDinkToPdf;

var loggerFactory = LoggerFactory.Create(logging => logging.AddConsole());
    

var converter = new SynchronizedConverter(new PdfTools());


var helperLogger = new Logger<TemplateHelper>(loggerFactory);
var templateHelper = new TemplateHelper(helperLogger);

var generatorLogger = new Logger<PdfGenerator>(loggerFactory);
var generator = new PdfGenerator(converter, templateHelper, generatorLogger);


var path = Environment.CurrentDirectory + "/Templates/pdf/template.html";

var template = await templateHelper.CompileFromPath(path, new {});

var pdf = await generator.CompileTemplateToPdf(path, new { }, pageNumbers:true);

File.WriteAllText("example.html", template);
File.WriteAllBytes("example.pdf", pdf);