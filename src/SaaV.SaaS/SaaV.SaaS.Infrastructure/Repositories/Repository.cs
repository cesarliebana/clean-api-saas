using Microsoft.EntityFrameworkCore;
using SaaV.SaaS.Core.Shared.Entities;
using SaaV.SaaS.Core.Shared.Interfaces;
using SaaV.SaaS.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace SaaV.SaaS.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly SaaSDbContext _dbContext;

        public Repository(SaaSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region IRepository
        public void Add(T entity)
        {            
            _dbContext.Set<T>().Add(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Set<T>().AnyAsync(entity => entity.Id == id);
        }

        public async Task<IList<T>> GetByAsync(Expression<Func<T, bool>> filterExpression)
        {
            return await _dbContext.Set<T>().Where(filterExpression).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
