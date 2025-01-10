namespace DomainSpace.Model.Dto;

/// <summary>
/// Register model
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// First name
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Date of birth
    /// </summary>
    public DateTime DateOfBirth { get; set; } = default!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = default!;
}
