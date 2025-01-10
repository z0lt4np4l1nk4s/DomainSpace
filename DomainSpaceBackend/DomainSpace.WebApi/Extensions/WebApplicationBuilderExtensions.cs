namespace DomainSpace.WebApi.Extensions;

/// <summary>
/// Web application builder extensions
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Register repositories
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>
    public static WebApplicationBuilder RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IContentDapperRepository, ContentDapperRepository>();
        builder.Services.AddScoped<IUserDapperRepository, UserDapperRepository>();

        return builder;
    }

    /// <summary>
    /// Register services
    /// </summary>
    /// <param name="builder">Web application builder</param>
    /// <returns>Web application builder</returns>
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ITokenService, JwtTokenService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IContentService, ContentService>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<ISubjectService, SubjectService>();
        builder.Services.AddScoped<ISmtpService, SmtpService>();
        builder.Services.AddScoped<IHubService, HubService>();
        builder.Services.AddScoped<ILikeService, LikeService>();

        return builder;
    }
}
