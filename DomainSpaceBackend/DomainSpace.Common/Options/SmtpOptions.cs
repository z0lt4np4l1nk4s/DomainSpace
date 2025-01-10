namespace DomainSpace.Common.Options;

/// <summary>
/// Smtp options
/// </summary>
public class SmtpOptions
{
    /// <summary>
    /// Host
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    /// Port
    /// </summary>
    public int? Port { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// From name
    /// </summary>
    public string? FromName { get; set; }

    /// <summary>
    /// From email
    /// </summary>
    public string? FromEmail { get; set; }

    /// <summary>
    /// Use SSL
    /// </summary>
    public bool UseSsl { get; set; }

    /// <summary>
    /// Require auth
    /// </summary>
    public bool RequireAuth { get; set; } = true;
}
