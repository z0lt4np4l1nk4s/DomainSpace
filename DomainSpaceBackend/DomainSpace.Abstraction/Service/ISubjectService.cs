namespace DomainSpace.Abstraction.Service;

/// <summary>
/// Subject service
/// </summary>
public interface ISubjectService
{
    /// <summary>
    /// Add subject asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> AddAsync(AddSubjectDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add default subject asynchronously
    /// </summary>
    /// <param name="domain">Domain</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> AddDefaultAsync(string domain, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update subject asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> UpdateAsync(Guid id, UpdateSubjectDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove subject asynchronously
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> RemoveAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get subjects paged asynchronously
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paged list</returns>
    Task<PagedList<SubjectDto>> GetPagedAsync(SubjectFilterDto param, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get subject by identifier asynchronously
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Subject model</returns>
    Task<SubjectDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
