using System.Security.Claims;

namespace TaskService.Infrastructure.Security
{
    public static class ClaimExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
