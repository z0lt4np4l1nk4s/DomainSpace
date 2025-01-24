namespace DomainSpace.Model.Dto;

/// <summary>
/// Token created model
/// </summary>
public class TokenCreatedDto
{
    /// <summary>
    /// Token
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Refresh token
    /// </summary>
    public string? RefreshToken { get; set; }
}
