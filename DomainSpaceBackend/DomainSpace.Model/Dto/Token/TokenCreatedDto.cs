namespace DomainSpace.Model.Dto;

/// <summary>
/// Token created model
/// </summary>
public class TokenCreatedDto
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Roles
    /// </summary>
    public IEnumerable<string> Roles { get; set; } = default!;

    /// <summary>
    /// Token
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Refresh token
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Expire time
    /// </summary>
    public DateTime ExpirationTime { get; set; }
}
