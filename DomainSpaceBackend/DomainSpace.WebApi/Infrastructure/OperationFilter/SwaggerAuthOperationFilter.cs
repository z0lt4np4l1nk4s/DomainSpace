using Swashbuckle.AspNetCore.SwaggerGen;

namespace DomainSpace.WebApi.Infrastructure.OperationFilter;

/// <summary>
/// Swagger Auth Operation Filter
/// </summary>
public class SwaggerAuthOperationFilter : IOperationFilter
{
    /// <inheritdoc cref="IOperationFilter.Apply(OpenApiOperation, OperationFilterContext)" />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var actionMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
        var isAuthorized = actionMetadata.Any(metadataItem => metadataItem is AuthorizeAttribute);
        var allowAnonymous = actionMetadata.Any(metadataItem => metadataItem is AllowAnonymousAttribute);

        if (!isAuthorized || allowAnonymous)
        {
            return;
        }

        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new() {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, Array.Empty<string>()
                }
            }
        };
    }
}
