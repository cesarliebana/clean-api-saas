using Microsoft.EntityFrameworkCore;
using SaaV.SaaS.Core.Domain;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.Infrastructure.Data;

namespace SaaV.SaaS.Infrastructure.Repositories
{
    public class DummyRepository : Repository<Dummy>, IDummyRepository
    {
        public DummyRepository(SaaSDbContext dbContext): base(dbContext)
        {
        }

        public Task<List<DummyListItem>> GetAllAsync()
        {
            return _dbContext.Dummies
                .Select(dummy => new DummyListItem(dummy.Id, dummy.Name))
                .ToListAsync();
        }
    }
}
