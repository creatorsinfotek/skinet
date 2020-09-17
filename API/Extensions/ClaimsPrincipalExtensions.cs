using System.Linq;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetreiveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(f => f.Type == ClaimTypes.Email)?.Value;
        } 
    }
}