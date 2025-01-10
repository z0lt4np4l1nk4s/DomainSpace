namespace DomainSpace.WebApi.Controllers;

/// <summary>
/// Like controller
/// </summary>
[Route(ControllerNames.Like)]
[ApiController]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likeService;

    /// <summary>
    /// Constructor
    /// </summary>
    public LikeController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    /// <summary>
    /// Like
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("like")]
    public async Task<IActionResult> LikeAsync([FromBody] LikeDto model, CancellationToken cancellationToken = default)
    {
        var result = await _likeService.LikeAsync(model, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Dislike
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("dislike")]
    public async Task<IActionResult> DislikeAsync([FromBody] DislikeDto model, CancellationToken cancellationToken = default)
    {
        var result = await _likeService.DislikeAsync(model, cancellationToken);

        return Ok(result);
    }
}
