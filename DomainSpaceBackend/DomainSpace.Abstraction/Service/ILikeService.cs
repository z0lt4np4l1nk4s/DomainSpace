namespace DomainSpace.Abstraction.Service;

/// <summary>
/// Like service
/// </summary>
public interface ILikeService
{
    /// <summary>
    /// Like asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> LikeAsync(LikeDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Dislike asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> DislikeAsync(DislikeDto model, CancellationToken cancellationToken = default);
}
