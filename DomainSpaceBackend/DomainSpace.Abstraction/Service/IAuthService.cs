namespace DomainSpace.Abstraction.Service;

/// <summary>
/// Auth service
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Login asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult<TokenCreatedDto>> LoginAsync(LoginDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Register asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> RegisterAsync(RegisterDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Refresh token asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult<TokenCreatedDto>> RefreshTokenAsync(RefreshTokenRequestDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Logout asynchronously
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> LogoutAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Change role asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> ChangeRoleAsync(ChangeRoleDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Change password asynchronously
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> ChangePasswordAsync(Guid userId, ChangePasswordDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Confirm email asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> ConfirmEmailAsync(ConfirmEmailDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reset password asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> ResetPasswordAsync(ResetPasswordDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send reset password code asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> SendResetPasswordEmailAsync(SendResetPasswordEmailDto model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verify reset password code asynchronously
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service result</returns>
    Task<ServiceResult> VerifyResetPasswordTokenAsync(VerifyResetPasswordTokenDto model, CancellationToken cancellationToken = default);
}
