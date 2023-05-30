using Mapster;
using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.Core.Shared.Exceptions;

namespace SaaV.SaaS.Core.Domain.Handlers
{
    public class GetDummyByIdHandler : IRequestHandler<GetDummyByIdRequest, GetDummyResponse>
    {
        IDummyRepository _dummyRepository;

        public GetDummyByIdHandler(IDummyRepository dummyRepository)
        {
            _dummyRepository = dummyRepository;
        }

        public async Task<GetDummyResponse> Handle(GetDummyByIdRequest getDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = await _dummyRepository.GetByIdAsync(getDummyRequest.Id)
                ?? throw new ItemNotFoundException(typeof(Dummy), getDummyRequest.Id);

            return dummy.Adapt<GetDummyResponse>();
        }
    }
}
