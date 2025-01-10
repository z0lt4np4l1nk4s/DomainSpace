namespace DomainSpace.Abstraction.Service;

/// <summary>
/// Content service
/// </summary>
public interface IContentService
{
    /// <summary>
    /// Add content asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="files">Files</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> AddAsync(AddContentDto model, List<AddFileDto> files, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update content asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> UpdateAsync(Guid id, UpdateContentDto model, List<AddFileDto> files, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update content likes count asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> UpdateLikeCountAsync(UpdateContentLikeCountDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove content asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> RemoveAsync(DeleteContentDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get content paged asynchronously
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paged list</returns>
    Task<PagedList<ContentDto>> GetPagedAsync(ContentFilterDto param, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get content by identifier asynchronously
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="observerId">Observer identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Content model</returns>
    Task<ContentDto?> GetByIdAsync(Guid id, Guid observerId, CancellationToken cancellationToken = default);
}
