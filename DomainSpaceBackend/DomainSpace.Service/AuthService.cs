namespace DomainSpace.Service;

/// <inheritdoc cref="IAuthService" />
public class AuthService : IAuthService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ISubjectService _subjectService;
    private readonly ISmtpService _smtpService;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly IMapper _mapper;
    private readonly AppOptions _appOptions;

    public AuthService(UserManager<UserEntity> userManager, ITokenService tokenService, ISubjectService subjectService, ISmtpService smtpService, SignInManager<UserEntity> signInManager, IMapper mapper, IOptions<AppOptions> appOptionsAccessor)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _subjectService = subjectService;
        _smtpService = smtpService;
        _signInManager = signInManager;
        _mapper = mapper;
        _appOptions = appOptionsAccessor.Value;
    }

    /// <inheritdoc cref="IAuthService.LoginAsync(LoginDto, CancellationToken)" />
    public async Task<ServiceResult<TokenCreatedDto>> LoginAsync(LoginDto model, CancellationToken cancellationToken = default)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            if(result.IsNotAllowed)
            {
                return ServiceResult<TokenCreatedDto>.Failure(ErrorDescriber.EmailAddressNotConfirmedErrorMessage());
            }

            return ServiceResult<TokenCreatedDto>.Failure(ErrorDescriber.InvalidLoginErrorMessage());
        }

        var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);

        var roles = await _signInManager.UserManager.GetRolesAsync(user);

        var token = _tokenService.CreateAync(new TokenOptionsDto
        {
            UserId = user.Id,
            Email = user.Email,
            Roles = roles
        });

        await _signInManager.UserManager.RemoveAuthenticationTokenAsync(user, LoginProviders.Password, TokenTypes.Refresh);
        await _signInManager.UserManager.SetAuthenticationTokenAsync(user, LoginProviders.Password, TokenTypes.Refresh, token.RefreshToken);

        return ServiceResult<TokenCreatedDto>.Success(token);
    }

    /// <inheritdoc cref="IAuthService.RegisterAsync(RegisterDto, CancellationToken)" />
    public async Task<ServiceResult> RegisterAsync(RegisterDto model, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<UserEntity>(model);
        user.Id = Guid.NewGuid();
        user.UserName = model.Email;
        user.DateOfBirth = user.DateOfBirth.ToUniversalTime();
        user.CreationTime = DateTime.UtcNow;

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var passwordErrors = result.Errors
                .Where(e => e.Code.Contains("Password"))
                .Select(e => e.Description)
                .ToList();

            if (passwordErrors.Any())
            {
                return ServiceResult.Failure(ErrorDescriber.PasswordTooWeakErrorMessage());
            }

            return ServiceResult.Failure();
        }

        await _userManager.AddToRoleAsync(user, RoleNames.User);
        await _subjectService.AddDefaultAsync(model.Email.Split("@")[1], cancellationToken);

        var fullName = $"{model.FirstName} {model.LastName}";

        var recipient = new EmailRecipientDto
        {
            UserId = user.Id,
            Name = fullName,
            Email = model.Email
        };

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var emailConfirmationModel = new ConfirmEmailDto
        {
            Email = model.Email,
            Token = token
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(emailConfirmationModel, jsonOptions);
        var encodedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

        var data = new
        {
            Name = fullName,
            ConfirmEmailUrl = $"{_appOptions.FrontendAppUrl}/{_appOptions.Routes?.ConfirmEmailRoute}/{encodedData}"
        };

        var sendEmailResult = await _smtpService.SendEmailAsync(EmailTemplates.WelcomeEmail, "Confirm email", recipient, data, cancellationToken);
        if (!sendEmailResult.IsSuccess)
        {
            return ServiceResult.Failure();
        }

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="IAuthService.LogoutAsync(Guid, CancellationToken)" />
    public async Task<ServiceResult> LogoutAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return ServiceResult.Failure();
        }

        await _userManager.RemoveAuthenticationTokenAsync(user, LoginProviders.Password, TokenTypes.Refresh);

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="IAuthService.RefreshTokenAsync(RefreshTokenRequestDto, CancellationToken)" />
    public async Task<ServiceResult<TokenCreatedDto>> RefreshTokenAsync(RefreshTokenRequestDto model, CancellationToken cancellationToken = default)
    {
        var splitted = model.RefreshToken.Split('|');

        if (splitted.Length != 2)
        {
            return ServiceResult<TokenCreatedDto>.Failure(ErrorDescriber.InvalidRefreshTokenErrorMessage());
        }

        var userId = splitted[0];

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return ServiceResult<TokenCreatedDto>.Failure(ErrorDescriber.InvalidRefreshTokenErrorMessage());
        }

        var refreshToken = await _userManager.GetAuthenticationTokenAsync(user, LoginProviders.Password, TokenTypes.Refresh);
        if (refreshToken.IsNullOrEmpty() || refreshToken != model.RefreshToken)
        {
            return ServiceResult<TokenCreatedDto>.Failure(ErrorDescriber.InvalidRefreshTokenErrorMessage());
        }

        var roles = await _userManager.GetRolesAsync(user);

        var token = _tokenService.CreateAync(new TokenOptionsDto
        {
            UserId = user.Id,
            Email = user.Email,
            Roles = roles
        });

        await _userManager.RemoveAuthenticationTokenAsync(user, LoginProviders.Password, TokenTypes.Refresh);
        await _userManager.SetAuthenticationTokenAsync(user, LoginProviders.Password, TokenTypes.Refresh, token.RefreshToken);

        return ServiceResult<TokenCreatedDto>.Success(token);
    }

    /// <inheritdoc cref="IAuthService.ChangeRoleAsync(ChangeRoleDto, CancellationToken)" />
    public async Task<ServiceResult> ChangeRoleAsync(ChangeRoleDto model, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(model.UserId.ToString());

        if (user == null)
        {
            return ServiceResult.Failure(ErrorDescriber.UnknownUserErrorMessage());
        }

        var roleResult = await _userManager.AddToRoleAsync(user, model.Role);

        if (!roleResult.Succeeded)
        {
            return ServiceResult.Failure(ErrorDescriber.UnknownRoleErrorMessage());
        }

        await _userManager.RemoveFromRoleAsync(user, RoleNames.User);

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="IAuthService.ChangePasswordAsync(Guid, ChangePasswordDto, CancellationToken)" />
    public async Task<ServiceResult> ChangePasswordAsync(Guid userId, ChangePasswordDto model, CancellationToken cancellationToken = default)
    {
        if (model.NewPassword != model.ConfirmNewPassword)
        {
            return ServiceResult.Failure(ErrorDescriber.PasswordsNotMatchingErrorMessage());
        }

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return ServiceResult.Failure();
        }

        var passwordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

        if (!passwordResult.Succeeded)
        {
            return ServiceResult.Failure(ErrorDescriber.PasswordResetErrorMessage());
        }

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="IAuthService.ConfirmEmailAsync(ConfirmEmailDto, CancellationToken)" />
    public async Task<ServiceResult> ConfirmEmailAsync(ConfirmEmailDto model, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return ServiceResult.Failure();
        }

        var result = await _userManager.ConfirmEmailAsync(user, model.Token);

        if (!result.Succeeded)
        {
            return ServiceResult.Failure();
        }

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="IAuthService.ResetPasswordAsync(ResetPasswordDto, CancellationToken)" />
    public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordDto model, CancellationToken cancellationToken = default)
    {
        if (model.Password != model.ConfirmPassword)
        {
            return ServiceResult.Failure(ErrorDescriber.PasswordsNotMatchingErrorMessage());
        }

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return ServiceResult.Failure(ErrorDescriber.PasswordResetErrorMessage());
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

        if (!result.Succeeded)
        {
            return ServiceResult.Failure();
        }

        return ServiceResult.Success(); ;
    }

    /// <inheritdoc cref="IAuthService.SendResetPasswordEmailAsync(SendResetPasswordEmailDto, CancellationToken)" />
    public async Task<ServiceResult> SendResetPasswordEmailAsync(SendResetPasswordEmailDto model, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return ServiceResult.Failure(ErrorDescriber.FailedToSendEmailErrorMessage());
        }

        var isEmailVerified = await _userManager.IsEmailConfirmedAsync(user);
        if (!isEmailVerified)
        {
            return ServiceResult.Failure(ErrorDescriber.FailedToSendEmailErrorMessage());
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        var fullName = $"{user.FirstName} {user.LastName}";

        var recipient = new EmailRecipientDto
        {
            UserId = user.Id,
            Name = fullName,
            Email = model.Email
        };

        var data = new
        {
            Name = fullName,
            ResetPasswordUrl = $"{_appOptions.FrontendAppUrl}/{_appOptions.Routes?.ResetPasswordRoute}/{encodedData}?email={user.Email}"
        };

        await _smtpService.SendEmailAsync(EmailTemplates.ResetPasswordEmail, "Password reset request", recipient, data, cancellationToken);

        return ServiceResult.Success();
    }

    /// <inheritdoc cref="IAuthService.VerifyResetPasswordTokenAsync(VerifyResetPasswordTokenDto, CancellationToken)" />
    public async Task<ServiceResult> VerifyResetPasswordTokenAsync(VerifyResetPasswordTokenDto model, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        var result = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "ResetPassword", model.Token);

        if (!result)
        {
            return ServiceResult.Failure(ErrorDescriber.InvalidPasswordResetCodeErrorMessage());
        }

        return ServiceResult.Success();
    }
}
