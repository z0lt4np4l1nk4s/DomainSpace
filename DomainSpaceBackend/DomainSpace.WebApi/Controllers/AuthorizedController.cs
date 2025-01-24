namespace DomainSpace.WebApi.Controllers;

/// <summary>
/// Authorized controller
/// </summary>
[ApiController]
[Authorize]
public class AuthorizedController : ControllerBase
{
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid UserId
    {
        get
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var guid = Guid.Parse(userId);
            return guid;
        }
    }

    /// <summary>
    /// Is admin
    /// </summary>
    public bool IsAdmin
    {
        get
        {
            return User.IsInRole(RoleNames.Admin);
        }
    }

    /// <summary>
    /// Is moderator
    /// </summary>
    public bool IsModerator
    {
        get
        {
            return User.IsInRole(RoleNames.Moderator);
        }
    }

    /// <summary>
    /// Get domain
    /// </summary>
    public string GetDomain
    {
        get
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var splitted = email.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);

            if (splitted.Length != 2)
            {
                return "Unknown";
            }

            return splitted[1];
        }
    }
}

