using Swashbuckle.AspNetCore.SwaggerGen;

namespace DomainSpace.WebApi.Infrastructure.OperationFilter;

/// <summary>
/// Swagger Error Codes operation filter
/// </summary>
public class SwaggerErrorCodesOperationFilter : IOperationFilter
{
    /// <inheritdoc cref="IOperationFilter.Apply(OpenApiOperation, OperationFilterContext)" />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var errorCodesAttributes = context
            .MethodInfo
            .GetCustomAttributes(typeof(SwaggerErrorCodesAttribute), false)
            .FirstOrDefault() as SwaggerErrorCodesAttribute;

        if (errorCodesAttributes != null && errorCodesAttributes.ErrorCodes.Any())
        {
            var description = new StringBuilder();

            description.AppendLine("<b>Custom Error Codes:</b><ul>");

            foreach (var code in errorCodesAttributes.ErrorCodes)
            {
                description.AppendLine($"<li>{code}</li>");
            }

            description.AppendLine("</ul>");

            if (operation.Description != null)
            {
                operation.Description += "<br>" + description.ToString();
            }
            else
            {
                operation.Description = description.ToString();
            }
        }
    }
}
