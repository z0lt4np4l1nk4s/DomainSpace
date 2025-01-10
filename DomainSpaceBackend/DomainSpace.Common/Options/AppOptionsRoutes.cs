namespace DomainSpace.Common.Options;

/// <summary>
/// App options routes
/// </summary>
public class AppOptionsRoutes
{
    /// <summary>
    /// Login route
    /// </summary>
    public string? LoginRoute { get; set; }

    /// <summary>
    /// Register route
    /// </summary>
    public string? RegisterRoute { get; set; }

    /// <summary>
    /// Confirm email route
    /// </summary>
    public string? ConfirmEmailRoute { get; set; }

    /// <summary>
    /// Reset password route
    /// </summary>
    public string? ResetPasswordRoute { get; set; }
}
