namespace Slay.Utilities.Extensions
{
    using System;
    using System.Linq;
    using System.Security.Claims;

    public static class IdentityExtensions
    {
        public static string GetUserId(this ClaimsPrincipal @this)
        {
            if (@this.IsNull())
            {
                throw new ArgumentNullException(nameof(@this));
            }

            return @this.Claims.First(claim => claim.Type == "sub").Value;
        }
    }
}
