namespace DomainSpace.Model.Dto;

/// <summary>
/// Verify reset password code model
/// </summary>
public class VerifyResetPasswordTokenDto
{
    /// <summary>
    /// Emails
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; set; } = default!;
}
