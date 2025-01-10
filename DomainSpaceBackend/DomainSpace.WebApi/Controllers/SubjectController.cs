namespace DomainSpace.WebApi.Controllers;

/// <summary>
/// Subject controller
/// </summary>
[Route(ControllerNames.Subject)]
[ApiController]
public class SubjectController : AuthorizedController
{
    private readonly ISubjectService _subjectService;

    /// <summary>
    /// Constructor
    /// </summary>
    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    /// <summary>
    /// Add subject
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Moderator)]
    public async Task<IActionResult> AddAsync([FromBody] AddSubjectDto model, CancellationToken cancellationToken = default)
    {
        if (!IsAdmin)
        {
            // None admin users can only add subjects to their own domain
            model.Domain = GetDomain;
        }

        model.UserId = UserId;

        var result = await _subjectService.AddAsync(model, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Get subjects paged
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetPagedAsync([FromQuery] SubjectFilterDto param, CancellationToken cancellationToken = default)
    {
        if (!IsAdmin)
        {
            param.Domain = GetDomain;
        }

        var result = await _subjectService.GetPagedAsync(param, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Get subject by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _subjectService.GetByIdAsync(id, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Update subject
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Moderator)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateSubjectDto model, CancellationToken cancellationToken = default)
    {
        var result = await _subjectService.UpdateAsync(id, model, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Delete subject
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Moderator)]
    public async Task<IActionResult> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _subjectService.RemoveAsync(id, cancellationToken);

        return result.ToActionResult();
    }
}
