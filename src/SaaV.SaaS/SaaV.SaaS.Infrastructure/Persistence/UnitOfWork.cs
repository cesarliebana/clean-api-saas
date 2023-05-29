using SaaV.SaaS.Core.Shared.Interfaces;

namespace SaaV.SaaS.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly SaaSDbContext _dbContext;

        public UnitOfWork(SaaSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
