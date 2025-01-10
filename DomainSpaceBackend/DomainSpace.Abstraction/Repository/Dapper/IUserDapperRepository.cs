namespace DomainSpace.Abstraction.Repository.Dapper;

/// <summary>
/// User dapper repository
/// </summary>
public interface IUserDapperRepository
{
    /// <summary>
    /// Get users paged asynchronously
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paged list</returns>
    Task<PagedList<UserDto>> GetPagedAsync(UserFilterDto param, CancellationToken cancellationToken = default);

    /// <summary>
    /// Count users asynchronously
    /// </summary>
    /// <param name="param">Params</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Integer</returns>
    Task<int> CountAsync(UserFilterDto param, CancellationToken cancellationToken = default);
}
