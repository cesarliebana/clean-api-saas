using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.Core.Shared.Interfaces;

namespace SaaV.SaaS.Core.Domain
{
    public interface IDummyRepository : IRepository<Dummy>
    {
        Task<List<DummyListItem>> GetAllAsync();
    }
}
