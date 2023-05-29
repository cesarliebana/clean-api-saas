using Microsoft.AspNetCore.Http;
using SaaV.SaaS.Core.Shared.Interfaces;
using SaaV.SaaS.Core.Shared.ValueObjects;
using System.Security.Claims;

namespace SaaV.SaaS.Infrastructure.Providers
{
    public class CredentialProvider : ICredentialProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Credential? _credential = null;

        public CredentialProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Credential GetCredential()
        {
            if (_credential.HasValue) return _credential.Value;

            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            if ((user.Identity == null) || !user.Identity.IsAuthenticated) throw new Exception();

            int tenantId = 0;
            string userId = string.Empty;
            string userName = string.Empty;
            
            Claim? claim = user.FindFirst(ClaimTypes.GroupSid);
            if (claim != null) tenantId = Int32.Parse(claim.Value);

            claim = user.FindFirst(ClaimTypes.Sid);
            if (claim != null) userId = claim.Value;

            claim = user.FindFirst(ClaimTypes.Name);
            if (claim != null) userName = claim.Value;

            _credential = new Credential(userId, userName, tenantId);

            return _credential.Value;
        }
        public Credential Credential { get { return GetCredential(); } }
    }
}

      
