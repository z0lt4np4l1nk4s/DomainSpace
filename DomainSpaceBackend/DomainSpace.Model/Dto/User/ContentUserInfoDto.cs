namespace DomainSpace.Model.Dto;

/// <summary>
/// Content user info model
/// </summary>
public class ContentUserInfoDto
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// First name
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; } = default!;
}
