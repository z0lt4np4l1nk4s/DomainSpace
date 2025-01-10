namespace DomainSpace.Abstraction.Service;

/// <summary>
/// Hub service
/// </summary>
public interface IHubService
{
    /// <summary>
    /// Send like count changed asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task</returns>
    Task SendLikeCountChangedAsync(LikeCountChangedDto model, CancellationToken cancellationToken = default);
}
