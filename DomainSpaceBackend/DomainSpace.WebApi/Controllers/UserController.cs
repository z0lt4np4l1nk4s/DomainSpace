namespace DomainSpace.WebApi.Controllers;

/// <summary>
/// User controller
/// </summary>
[Route(ControllerNames.User)]
[ApiController]
public class UserController : AuthorizedController
{
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor
    /// </summary>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get users paged
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpGet]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> GetPagedAsync([FromQuery] UserFilterDto param, CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetPagedAsync(param, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Get user by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (!IsAdmin)
        {
            id = UserId;
        }

        var result = await _userService.GetByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateUserDto model, CancellationToken cancellationToken = default)
    {
        var result = await _userService.UpdateAsync(id, model, cancellationToken);

        return result.ToActionResult();
    }
}
