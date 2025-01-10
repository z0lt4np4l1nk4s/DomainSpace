namespace DomainSpace.Model.Dto;

/// <summary>
/// Change role model
/// </summary>
public class ChangeRoleDto
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Role
    /// </summary>
    public string Role { get; set; } = default!;
}
