namespace DomainSpace.Common.Dto;

/// <summary>
/// Error message
/// </summary>
public class ErrorMessage
{
    /// <summary>
    /// Error code
    /// </summary>
    public string ErrorCode { get; set; } = default!;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = default!;
}
