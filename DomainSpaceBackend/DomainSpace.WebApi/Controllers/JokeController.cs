namespace DomainSpace.WebApi.Controllers;

/// <summary>
/// Joke controller
/// </summary>
[Route(ControllerNames.Joke)]
[ApiController]
public class JokeController : ControllerBase
{
    private readonly IJokeService _jokeService;

    /// <summary>
    /// Constructor
    /// </summary>
    public JokeController(IJokeService jokeService)
    {
        _jokeService = jokeService;
    }

    /// <summary>
    /// Get joke
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
    {
        var result = await _jokeService.GetAsync(cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
