namespace DomainSpace.Model.Dto;

/// <summary>
/// Change password model
/// </summary>
public class ChangePasswordDto
{
    /// <summary>
    /// Old password
    /// </summary>
    public string OldPassword { get; set; } = default!;

    /// <summary>
    /// New password
    /// </summary>
    public string NewPassword { get; set; } = default!;

    /// <summary>
    /// Confirm new password
    /// </summary>
    public string ConfirmNewPassword { get; set; } = default!;
}
