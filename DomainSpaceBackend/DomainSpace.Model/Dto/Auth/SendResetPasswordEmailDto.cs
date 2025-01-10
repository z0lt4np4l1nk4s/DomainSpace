namespace DomainSpace.Model.Dto;

/// <summary>
/// Send reset password code model
/// </summary>
public class SendResetPasswordEmailDto
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = default!;
}
