using Microsoft.AspNetCore.Http;
using SaaV.SaaS.Core.Shared.Interfaces;
using System.Security.Claims;

namespace SaaV.SaaS.Infrastructure.Providers
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetTenantId()
        {
            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;

            if ((user.Identity == null) || !user.Identity.IsAuthenticated) return 0;
            return 1;

            var claim = user.FindFirst("miClaim");
            if (claim != null)
            {
                var valorDelClaim = claim.Value;
                // Luego puedes usar el valor del claim según necesites...
            }
        }
        public int TenantId => 1;
    }
}

      
