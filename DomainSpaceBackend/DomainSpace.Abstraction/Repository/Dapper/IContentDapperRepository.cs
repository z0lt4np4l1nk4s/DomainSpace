namespace DomainSpace.Abstraction.Repository.Dapper;

/// <summary>
/// Content dapper repository
/// </summary>
public interface IContentDapperRepository
{
    /// <summary>
    /// Get content by identifier asynchronously
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="observerId">Observer identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Content model</returns>
    Task<ContentDto?> GetByIdAsync(Guid id, Guid observerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get content paged asynchronously
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paged list</returns>
    Task<PagedList<ContentDto>> GetPagedAsync(ContentFilterDto param, CancellationToken cancellationToken = default);

    /// <summary>
    /// Count content asynchronously
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Integer</returns>
    Task<int> CountAsync(ContentFilterDto param, CancellationToken cancellationToken = default);
}
