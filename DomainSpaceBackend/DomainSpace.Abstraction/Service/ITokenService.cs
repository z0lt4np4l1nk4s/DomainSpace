namespace DomainSpace.Abstraction.Service;

/// <summary>
/// Token service
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Create token
    /// </summary>
    /// <param name="options">Token options</param>
    /// <returns>Token model</returns>
    TokenCreatedDto CreateAync(TokenOptionsDto options);
}
