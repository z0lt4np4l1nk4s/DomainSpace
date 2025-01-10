namespace DomainSpace.WebApi.Controllers;

/// <summary>
/// File controller
/// </summary>
[Route(ControllerNames.File)]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    /// <summary>
    /// Constructor
    /// </summary>
    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>
    /// Download all files
    /// </summary>
    /// <param name="contentId">Content identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpGet("download-all/{contentId}")]
    public async Task<IActionResult> DownloadAllFilesAsync(Guid contentId, CancellationToken cancellationToken = default)
    {
        using var zipStream = new MemoryStream();

        var result = await _fileService.DownloadAllAsync(contentId, zipStream, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound();
        }

        zipStream.Seek(0, SeekOrigin.Begin);

        return File(zipStream.ToArray(), "application/zip", $"DomainSpace_{DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss")}.zip");
    }
}
