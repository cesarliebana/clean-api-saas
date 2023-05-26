using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;

namespace SaaV.SaaS.Core.Domain.Handlers
{
    public class GetAllDummiesHandler : IRequestHandler<GetAllDummiesRequest, GetAllDummiesResponse>
    {
        IDummyRepository _dummyRepository;

        public GetAllDummiesHandler(IDummyRepository dummyRepository)
        {
            _dummyRepository = dummyRepository;
        }

        public async Task<GetAllDummiesResponse> Handle(GetAllDummiesRequest getAllDummiesRequest, CancellationToken cancellationToken)
        {
            IList<DummyListItem> dummies = await _dummyRepository.GetAllAsync();
            
            return new GetAllDummiesResponse(dummies);
        }
    }
}
