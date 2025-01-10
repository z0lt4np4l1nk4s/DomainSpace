namespace DomainSpace.Model.Dto;

/// <summary>
/// Reset password model
/// </summary>
public class ResetPasswordDto
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; set; } = default!;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Confirm password
    /// </summary>
    public string ConfirmPassword { get; set; } = default!;
}
