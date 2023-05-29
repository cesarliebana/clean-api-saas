using SaaV.SaaS.Core.Shared.ValueObjects;

namespace SaaV.SaaS.Core.Shared.Interfaces
{
    public interface ICredentialProvider
    {
        public Credential Credential { get; }
    }
}
