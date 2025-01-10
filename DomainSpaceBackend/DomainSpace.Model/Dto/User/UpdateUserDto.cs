namespace DomainSpace.Model.Dto;

/// <summary>
/// Update user model
/// </summary>
public class UpdateUserDto
{
    /// <summary>
    /// First name
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; } = default!;
}
