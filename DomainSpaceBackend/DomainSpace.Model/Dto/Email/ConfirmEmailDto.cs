namespace DomainSpace.Model.Dto;

/// <summary>
/// Confirm email model
/// </summary>
public class ConfirmEmailDto
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; set; } = default!;
}
