namespace DomainSpace.Service;

/// <inheritdoc cref="ITokenService" />
public class JwtTokenService : ITokenService
{
    private readonly JwtTokenOptions _jwtTokenOptions;

    public JwtTokenService(IOptions<JwtTokenOptions> jwtTokenOptionsAccessor)
    {
        _jwtTokenOptions = jwtTokenOptionsAccessor.Value;
    }

    /// <inheritdoc cref="ITokenService.CreateAync(TokenOptionsDto)" />
    public TokenCreatedDto CreateAync(TokenOptionsDto options)
    {
        if (!options.Expiration.HasValue)
        {
            options.Expiration = TimeSpan.FromDays(1);
        }

        DateTime creationTime = DateTime.UtcNow;
        DateTime expirationTime = creationTime.Add(options.Expiration.Value);

        var model = new TokenCreatedDto
        {
            Token = CreateJwtToken(options, creationTime, expirationTime),
            RefreshToken = options.UserId.ToString() + "|" + Convert.ToBase64String(RandomNumberGenerator.GetBytes(_jwtTokenOptions.RefreshTokenLength ?? 64))
        };

        return model;
    }

    private string CreateJwtToken(TokenOptionsDto options, DateTime createTime, DateTime expireTime)
    {
        var signingKey = Encoding.ASCII.GetBytes(_jwtTokenOptions.SecretKey);
        var symmetricKey = new SymmetricSecurityKey(signingKey);
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>()
        {
            new(ClaimTypes.Email, options.Email),
            new(ClaimTypes.NameIdentifier, options.UserId.ToString()),
        };

        if (options.Roles != null && options.Roles.Any())
        {
            foreach (var role in options.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        var securityTokenDescriptora = new SecurityTokenDescriptor()
        {
            IssuedAt = createTime,
            NotBefore = createTime,
            Expires = expireTime,
            Issuer = _jwtTokenOptions.Issuer,
            Audience = _jwtTokenOptions.Audience,
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256),
        };

        var securityToken = tokenHandler.CreateToken(securityTokenDescriptora);
        var tokenString = tokenHandler.WriteToken(securityToken);

        return tokenString;
    }
}
