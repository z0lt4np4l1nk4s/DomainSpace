namespace DomainSpace.Abstraction.Service;

/// <summary>
/// Joke service
/// </summary>
public interface IJokeService
{
    /// <summary>
    /// Get a joke
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Joke</returns>
    Task<JokeDto?> GetAsync( CancellationToken cancellationToken = default);
}
