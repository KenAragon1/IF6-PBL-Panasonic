namespace panasonic.Helpers;


public interface IUserClaimHelper
{
    string? GetUserClaim(string ClaimType);
}

public class UserClaimHelper : IUserClaimHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserClaimHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserClaim(string ClaimType)
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity != null && user.Identity.IsAuthenticated)
        {
            var claim = user.FindFirst(ClaimType);
            return claim?.Value;
        }

        return null;
    }
}