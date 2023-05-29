namespace SaaV.SaaS.Core.Shared.Interfaces
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
