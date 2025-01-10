namespace DomainSpace.Common.Options;

/// <summary>
/// App options
/// </summary>
public class AppOptions
{
    /// <summary>
    /// App name
    /// </summary>
    public string? AppName { get; set; }

    /// <summary>
    /// Frontend app url
    /// </summary>
    public string? FrontendAppUrl { get; set; }

    /// <summary>
    /// Files prefix url
    /// </summary>
    public string? FilesPrefixUrl { get; set; }

    /// <summary>
    /// Routes
    /// </summary>
    public AppOptionsRoutes? Routes { get; set; }

    /// <summary>
    /// Allowed file extensions
    /// </summary>
    public List<string>? AllowedFileExtensions { get; set; }

    /// <summary>
    /// Max file size
    /// </summary>
    public long MaxFileSize { get; set; }
}
