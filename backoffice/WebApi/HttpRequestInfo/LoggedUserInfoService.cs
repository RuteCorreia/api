using System.Security.Claims;

namespace WebApi.HttpRequestInfo;

public class LoggedUserInfoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public LoggedUserInfoService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public (string?, IEnumerable<string>?, string?) GetLoggedUserIdentityIdAndRole()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var idEmpresa = _httpContextAccessor.HttpContext?.User.FindFirst("IdEmpresa")?.Value;
        var userRoles = _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role);
        var roleNames = userRoles?.Select(r => r.Value).ToList();
        return (userId, roleNames, idEmpresa);
    }
}
