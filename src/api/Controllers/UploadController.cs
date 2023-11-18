using app.Services;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileUploadController : ControllerBase
{
  private readonly ILogger<FileUploadController> logger;
  private readonly PdfDocumentService pdfDocumentService;

  public FileUploadController(ILogger<FileUploadController> logger, PdfDocumentService pdfDocumentService)
  {
    this.logger = logger;
    this.pdfDocumentService = pdfDocumentService;
  }

  [HttpPost]
  public async Task<IActionResult> Import(IEnumerable<IFormFile> files)
  {
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
