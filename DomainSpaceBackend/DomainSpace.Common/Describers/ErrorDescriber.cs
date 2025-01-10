namespace DomainSpace.Common.Describers;

/// <summary>
/// Error describers
/// </summary>
public static class ErrorDescriber
{
    /// <summary>
    /// Login failed
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage InvalidLoginErrorMessage() => new()
    {
        Description = "Invalid email or password."
    };

    /// <summary>
    /// Email address not confirmed
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage EmailAddressNotConfirmedErrorMessage() => new()
    {
        Description = "Please confirm your email address."
    };

    /// <summary>
    /// Login failed
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage PasswordTooWeakErrorMessage() => new()
    {
        Description = "The password is too weak."
    };

    /// <summary>
    /// Invalid refresh token
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage InvalidRefreshTokenErrorMessage() => new()
    {
        Description = "Invalid refresh token."
    };

    /// <summary>
    /// File extension not supported
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage FileExtensionNotSupportedErrorMessage() => new()
    {
        Description = "File extension not supported."
    };

    /// <summary>
    /// File too large
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage FileTooLargeErrorMessage() => new()
    {
        Description = "Too large file."
    };

    /// <summary>
    /// Unknown user
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage UnknownUserErrorMessage() => new()
    {
        Description = "Unknown user."
    };

    /// <summary>
    /// Unknown role
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage UnknownRoleErrorMessage() => new()
    {
        Description = "Unknown role."
    };

    /// <summary>
    /// Subject in use
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage SubjectInUserErrorMessage() => new()
    {
        Description = "The subject is in use."
    };

    /// <summary>
    /// Password reset
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage PasswordResetErrorMessage() => new()
    {
        Description = "Failed to update password."
    };

    /// <summary>
    /// Passwords don't match
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage PasswordsNotMatchingErrorMessage() => new()
    {
        Description = "The passwords are not matching."
    };

    /// <summary>
    /// Email confirmation failed
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage EmailConfirmationFailedErrorMessage() => new()
    {
        Description = "Email confirmation failed."
    };

    /// <summary>
    /// Invalid password reset code
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage InvalidPasswordResetCodeErrorMessage() => new()
    {
        Description = "Invalid password reset code."
    };

    /// <summary>
    /// Failed to send email
    /// </summary>
    /// <returns>Error message</returns>
    public static ErrorMessage FailedToSendEmailErrorMessage() => new()
    {
        Description = "Failed to send email."
    };
}
