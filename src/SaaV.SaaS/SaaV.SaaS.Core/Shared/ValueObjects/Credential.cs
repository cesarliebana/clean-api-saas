namespace SaaV.SaaS.Core.Shared.ValueObjects
{
    public record struct Credential(string UserId, string UserName, int TenantId);
}
