using System;
using System.Security.Claims;

namespace CustomNetCoreIdentity.Infrastructure.Extensions
{
    /// <summary>
    /// Here we check if impersonation claim is available, then get original user id and login
    /// with that user. You will probably ask what is .IsImpersonating() method
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        //https://stackoverflow.com/a/35577673/809357
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);

            return claim?.Value;
        }

        public static bool IsImpersonating(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var isImpersonating = principal.HasClaim("IsImpersonating", "true");

            return isImpersonating;
        }
    }
}
