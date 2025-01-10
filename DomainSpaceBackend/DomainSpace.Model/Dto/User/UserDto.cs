namespace DomainSpace.Model.Dto;

/// <summary>
/// User model
/// </summary>
public class UserDto
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// First name
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Roles
    /// </summary>
    public List<string> Roles { get; set; } = default!;
}
