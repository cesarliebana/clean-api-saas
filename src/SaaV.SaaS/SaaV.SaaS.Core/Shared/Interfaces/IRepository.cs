using System.Linq.Expressions;
using SaaV.SaaS.Core.Shared.Entities;

namespace SaaV.SaaS.Core.Shared.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<bool> ExistsAsync(int id);

        Task<IList<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<TEntity?> GetByIdAsync(int id);

        void Add(TEntity entity);
    }
}
