using app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileUploadController : ControllerBase
{
    private readonly ILogger<FileUploadController> logger;
    private readonly IConfiguration config;
    private readonly PdfDocumentService pdfDocumentService;
    private readonly int DocumentLineSplitMaxTokens;
    private readonly int DocumentParagraphSplitMaxLines;

    public FileUploadController(ILogger<FileUploadController> logger, IConfiguration config, PdfDocumentService pdfDocumentService)
    {
        this.logger = logger;

        this.config = config;
        this.pdfDocumentService = pdfDocumentService;
    }

    [HttpPost]
    public async Task<IActionResult> Import()
    {
        var files = Request.Form.Files;
        if (files == null || files.Any(file => file.Length == 0 || Path.GetExtension(file.FileName).ToLowerInvariant() != ".pdf"))
        {
            return BadRequest("Invalid file format. Please upload PDF files.");
        }
   
        foreach (var file in files)
        {
            await pdfDocumentService.ImportDocumentAsync(file, "DocumentMemory");
            logger.LogDebug("Document {fileName} imported.", file.FileName);
        }

        return Ok($"PDF files uploaded successfully.");
    }
}
