namespace DomainSpace.Abstraction.Service;

/// <summary>
/// File service
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Add file asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> AddAsync(string domain, List<AddFileDto> model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove files by owner asynchronously
    /// </summary>
    /// <param name="ownerId">Owner identifier</param>
    /// <param name="ownerType">Owner type</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> RemoveByOwnerAsync(Guid ownerId, string ownerType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all files asynchronously
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List</returns>
    Task<List<FileDto>> GetAllAsync(FileFilterDto param, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update files asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> UpdateFilesAsync(UpdateFilesDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Download all asynchronously
    /// </summary>
    /// <param name="contentId">Content identifier</param>
    /// <param name="zipStream">Zip stream</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> DownloadAllAsync(Guid contentId, MemoryStream zipStream, CancellationToken cancellationToken = default); 
}
