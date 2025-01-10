namespace DomainSpace.Abstraction.Service;

/// <summary>
/// User service
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Get users paged asynchronously
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paged list</returns>
    Task<PagedList<UserDto>> GetPagedAsync(UserFilterDto param, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user by identifier asynchronously
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update user asynchoronously
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> UpdateAsync(Guid id, UpdateUserDto model, CancellationToken cancellationToken = default);
}
