namespace DomainSpace.Model.Dto;

/// <summary>
/// Token options model
/// </summary>
public class TokenOptionsDto
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Roles
    /// </summary>
    public IEnumerable<string> Roles { get; set; } = default!;

    /// <summary>
    /// Expiration
    /// </summary>
    public TimeSpan? Expiration { get; set; }
}
