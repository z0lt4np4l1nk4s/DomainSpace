namespace DomainSpace.Common.Options;

/// <summary>
/// Jwt options
/// </summary>
public class JwtTokenOptions
{
    /// <summary>
    /// Valid audience
    /// </summary>
    public string? Audience { get; set; }

    /// <summary>
    /// Valid issuer
    /// </summary>
    public string? Issuer { get; set; }

    /// <summary>
    /// Secret key
    /// </summary>
    public string SecretKey { get; set; } = default!;

    /// <summary>
    /// Validate issuer
    /// </summary>
    public bool ValidateIssuer { get; set; } = true;

    /// <summary>
    /// Validate audience
    /// </summary>
    public bool ValidateAudience { get; set; } = true;

    /// <summary>
    /// Validate lifetime
    /// </summary>
    public bool ValidateLifetime { get; set; } = true;

    /// <summary>
    /// Require expiration time
    /// </summary>
    public bool RequireExpirationTime { get; set; } = true;

    /// <summary>
    /// Refresh token length
    /// </summary>
    public int? RefreshTokenLength { get; set; }
}
