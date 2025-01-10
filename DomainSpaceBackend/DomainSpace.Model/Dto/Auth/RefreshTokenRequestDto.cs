namespace DomainSpace.Model.Dto;

/// <summary>
/// Refresh token request model
/// </summary>
public class RefreshTokenRequestDto
{
    /// <summary>
    /// Refresh token
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}
