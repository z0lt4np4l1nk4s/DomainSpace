namespace DomainSpace.Model.Filter;

/// <summary>
/// Content filter
/// </summary>
public class ContentFilterDto : BaseParams
{
    /// <summary>
    /// Observer identifier
    /// </summary>
    public Guid? ObserverId { get; set; }

    /// <summary>
    /// Subjects
    /// </summary>
    public string? SubjectIds { get; set; }

    /// <summary>
    /// Domain
    /// </summary>
    public string? Domain { get; set; }
}
