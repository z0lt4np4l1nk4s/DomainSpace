namespace DomainSpace.WebApi.Controllers;

/// <summary>
/// Content controller
/// </summary>
[Route(ControllerNames.Content)]
[ApiController]
public class ContentController : AuthorizedController
{
    private readonly IContentService _contentService;
    private readonly AppOptions _appOptions;

    /// <summary>
    /// Constructor
    /// </summary>
    public ContentController(IContentService contentService, IOptions<AppOptions> appOptionsAccessor)
    {
        _contentService = contentService;
        _appOptions = appOptionsAccessor.Value;
    }

    /// <summary>
    /// Get content paged
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpGet]
    public async Task<IActionResult> GetPagedAsymc([FromQuery] ContentFilterDto param, CancellationToken cancellationToken = default)
    {
        if (!IsAdmin)
        {
            // None admin users can only see content from their own domain
            param.Domain = GetDomain;
        }

        param.ObserverId = UserId;
        var result = await _contentService.GetPagedAsync(param, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Get content by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsymc(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _contentService.GetByIdAsync(id, UserId, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Add content
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="files">Files</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] AddContentDto model, [FromForm] List<IFormFile> files, CancellationToken cancellationToken = default)
    {
        var fileResult = await ProcessFiles(files, cancellationToken);

        if (!fileResult.IsSuccess)
        {
            return ServiceResult.Failure(fileResult.ErrorMessages).ToActionResult();
        }

        model.UserId = UserId;
        if (!IsAdmin)
        {
            model.Domain = GetDomain;
        }
        var result = await _contentService.AddAsync(model, fileResult.Result!, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Update content
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="model">Model</param>
    /// <param name="files">Files</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] UpdateContentDto model, [FromForm] List<IFormFile> files, CancellationToken cancellationToken = default)
    {
        var fileResult = await ProcessFiles(files, cancellationToken);

        if (!fileResult.IsSuccess)
        {
            return ServiceResult.Failure(fileResult.ErrorMessages).ToActionResult();
        }

        model.UserId = UserId;
        model.IsAdmin = IsAdmin;

        var result = await _contentService.UpdateAsync(id, model, fileResult.Result!, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Delete content
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var model = new DeleteContentDto
        {
            Id = id,
            UserId = UserId,
            IsModeratorOrAdmin = IsModerator || IsAdmin
        };

        var result = await _contentService.RemoveAsync(model, cancellationToken);

        return result.ToActionResult();
    }

    private async Task<ServiceResult<List<AddFileDto>>> ProcessFiles(List<IFormFile> files, CancellationToken cancellationToken = default)
    {
        var newFiles = new List<AddFileDto>();

        if (files != null && files.Any())
        {
            foreach (var file in files)
            {
                var extension = file.FileName.Split('.').Last();

                if (_appOptions.AllowedFileExtensions == null || !_appOptions.AllowedFileExtensions.Contains(extension))
                {
                    return ServiceResult<List<AddFileDto>>.Failure(ErrorDescriber.FileExtensionNotSupportedErrorMessage());
                }

                if (file.Length > _appOptions.MaxFileSize)
                {
                    return ServiceResult<List<AddFileDto>>.Failure(ErrorDescriber.FileTooLargeErrorMessage());
                }

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream, cancellationToken);

                var newFile = new AddFileDto
                {
                    UserId = UserId,
                    Name = file.FileName,
                    Extension = extension,
                    Size = file.Length,
                    Content = memoryStream.ToArray()
                };

                newFiles.Add(newFile);
            }
        }

        return ServiceResult<List<AddFileDto>>.Success(newFiles);
    }
}
