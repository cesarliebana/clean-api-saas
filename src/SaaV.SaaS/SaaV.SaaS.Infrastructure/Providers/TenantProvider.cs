using SaaV.SaaS.Core.Shared.Interfaces;

namespace SaaV.SaaS.Infrastructure.Providers
{
    public class TenantProvider : ITenantProvider
    {
        public int GetTenantId()
        {
            return 1;
        }
    }
}
