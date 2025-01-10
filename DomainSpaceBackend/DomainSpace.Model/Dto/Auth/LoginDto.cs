namespace DomainSpace.Model.Dto;

/// <summary>
/// Login model
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = default!;
    
    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = default!;
}
