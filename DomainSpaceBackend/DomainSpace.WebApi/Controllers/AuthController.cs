namespace DomainSpace.WebApi.Controllers;

/// <summary>
/// Auth controller
/// </summary>
[Route(ControllerNames.Auth)]
[ApiController]
public partial class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId
    {
        get
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var guid = Guid.Parse(userId);
            return guid;
        }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto model, CancellationToken cancellationToken = default)
    {
        var result = await _authService.LoginAsync(model, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Register
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model, CancellationToken cancellationToken = default)
    {
        var result = await _authService.RegisterAsync(model, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> LogoutAsync(CancellationToken cancellationToken = default)
    {
        var result = await _authService.LogoutAsync(UserId, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Refresh token
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequestDto model, CancellationToken cancellationToken = default)
    {
        var result = await _authService.RefreshTokenAsync(model, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Change role
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    [HttpPost("change-role")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> UpdateAsync([FromBody] ChangeRoleDto model, CancellationToken cancellationToken = default)
    {
        var result = await _authService.ChangeRoleAsync(model, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Change password
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto model, CancellationToken cancellationToken = default)
    {
        var result = await _authService.ChangePasswordAsync(UserId, model, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Confirm email address
    /// </summary>
    /// <param name="token">Token</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("confirm-email/{token}")]
    public async Task<IActionResult> ConfirmEmailAsync(string token, CancellationToken cancellationToken = default)
    {
        var decodedData = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var model = JsonSerializer.Deserialize<ConfirmEmailDto>(decodedData, jsonOptions);

        if (model == null)
        {
            return ServiceResult.Failure(ErrorDescriber.EmailConfirmationFailedErrorMessage()).ToActionResult();
        }

        var result = await _authService.ConfirmEmailAsync(model, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Reset password
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto model, CancellationToken cancellationToken = default)
    {
        model.Token = Encoding.UTF8.GetString(Convert.FromBase64String(model.Token));
        var result = await _authService.ResetPasswordAsync(model, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Verify reset password token
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("verify-reset-password-token")]
    public async Task<IActionResult> VerifyResetPasswordTokenAsync([FromBody] VerifyResetPasswordTokenDto model, CancellationToken cancellationToken = default)
    {
        model.Token = Encoding.UTF8.GetString(Convert.FromBase64String(model.Token));
        var result = await _authService.VerifyResetPasswordTokenAsync(model, cancellationToken);

        return result.ToActionResult();
    }

    /// <summary>
    /// Send reset password token
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Action result</returns>
    [HttpPost("send-reset-password-email")]
    public async Task<IActionResult> SendPasswordEmailAsync([FromBody] SendResetPasswordEmailDto model, CancellationToken cancellationToken = default)
    {
        var result = await _authService.SendResetPasswordEmailAsync(model, cancellationToken);

        return result.ToActionResult();
    }
}
