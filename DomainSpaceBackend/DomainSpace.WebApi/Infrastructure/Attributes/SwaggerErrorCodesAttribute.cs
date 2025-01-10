namespace DomainSpace.WebApi.Infrastructure.Attributes;

/// <summary>
/// Swagger error codes attribute
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class SwaggerErrorCodesAttribute : Attribute
{
    /// <summary>
    /// Error codes
    /// </summary>
    public List<string> ErrorCodes { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="errorCodes">Error codes</param>
    public SwaggerErrorCodesAttribute(params string[] errorCodes)
    {
        ErrorCodes = errorCodes.ToList();
    }
}
