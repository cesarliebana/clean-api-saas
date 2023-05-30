using Mapster;
using Microsoft.EntityFrameworkCore;
using SaaV.SaaS.Core.Domain;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.Infrastructure.Persistence;

namespace SaaV.SaaS.Infrastructure.Repositories
{
    public class DummyRepository : Repository<Dummy>, IDummyRepository
    {
        public DummyRepository(SaaSDbContext dbContext): base(dbContext)
        {
        }

        public async Task<List<DummyListItem>> GetAllAsync()
        {
            return await _dbContext.Dummies.ProjectToType<DummyListItem>().ToListAsync();
        }
    }
}
