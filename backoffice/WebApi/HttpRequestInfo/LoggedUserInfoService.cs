using System.Security.Claims;

namespace WebApi.HttpRequestInfo
{
    public class LoggedUserInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoggedUserInfoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public (string?, string?) GetLoggedUserIdentityIdAndRole(bool getRole = false)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = getRole ? _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value : null;
            return (userId, userRole);
        }
    }
}
