using System.Security.Claims;

namespace WebApi.HttpRequestInfo;

public class LoggedUserInfoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public LoggedUserInfoService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public (string?, IEnumerable<Claim>?) GetLoggedUserIdentityIdAndRole(bool getRole = false)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRoles = getRole ? _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role) : null;
        return (userId, userRoles);
    }
}
