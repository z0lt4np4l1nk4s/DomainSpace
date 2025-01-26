var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

if (builder.Environment.IsProduction())
{
    builder.Configuration
        .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
}
else if (builder.Environment.IsDevelopment())
{
    builder.Configuration
        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
}

// App options
var appOptionsSection = builder.Configuration.GetSection("AppOptions");
var _appOptions = appOptionsSection.Get<AppOptions>();
builder.Services.Configure<AppOptions>(appOptionsSection);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
                .WithOrigins(_appOptions.FrontendAppUrl ?? "")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "DomainSpace WebApi", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = HeaderNames.Authorization,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.OperationFilter<SwaggerAuthOperationFilter>();
    options.OperationFilter<SwaggerErrorCodesOperationFilter>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Jwt token options
var _jwtTokenOptionsSection = builder.Configuration.GetSection("JwtTokenOptions");
var _jwtTokenOptions = _jwtTokenOptionsSection.Get<JwtTokenOptions>();
builder.Services.Configure<JwtTokenOptions>(_jwtTokenOptionsSection);

// Smtp options
var _smtpOptionsSection = builder.Configuration.GetSection("SmtpOptions");
builder.Services.Configure<SmtpOptions>(_smtpOptionsSection);

// Mapster
var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
typeAdapterConfig.Scan(typeof(MapRegister).Assembly);
var mapperConfig = new Mapper(typeAdapterConfig);
builder.Services.AddSingleton<IMapper>(mapperConfig);

// SignalR
builder.Services.AddSignalR();

// Register middlewares
builder.RegisterRepositories();
builder.RegisterServices();

// Configure EF Core to use PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IDbConnection>(options => new NpgsqlConnection(connectionString));

builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = _jwtTokenOptions.ValidateIssuer,
            ValidateAudience = _jwtTokenOptions.ValidateAudience,
            ValidateLifetime = _jwtTokenOptions.ValidateLifetime,
            RequireExpirationTime = _jwtTokenOptions.RequireExpirationTime,
            ValidIssuer = _jwtTokenOptions.Issuer,
            ValidAudience = _jwtTokenOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtTokenOptions.SecretKey))
        };
    });

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.MediaTypeOptions.AddText("application/json");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});


var app = builder.Build();

app.UseCors();

var webRootPath = app.Environment.WebRootPath ?? Directory.GetCurrentDirectory();
Directory.CreateDirectory(Path.Combine(webRootPath, "Files", "Public"));
Directory.CreateDirectory(Path.Combine(webRootPath, "Files", "Private"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "Files/Public")),
    RequestPath = "/Files"
});

app.MapControllers();
app.MapHub<HubService>("/hub");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var contect = services.GetRequiredService<ApplicationDbContext>();
    contect.Database.Migrate();
}

app.Run();
