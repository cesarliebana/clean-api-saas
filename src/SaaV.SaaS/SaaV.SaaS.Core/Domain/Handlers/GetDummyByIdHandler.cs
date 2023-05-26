using AutoMapper;
using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.Core.Shared.Exceptions;

namespace SaaV.SaaS.Core.Domain.Handlers
{
    public class GetDummyByIdHandler : IRequestHandler<GetDummyByIdRequest, GetDummyResponse>
    {
        IDummyRepository _dummyRepository;
        IMapper _mapper;

        public GetDummyByIdHandler(IDummyRepository dummyRepository, IMapper mapper)
        {
            _dummyRepository = dummyRepository;
            _mapper = mapper;
        }

        public async Task<GetDummyResponse> Handle(GetDummyByIdRequest getDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = await _dummyRepository.GetByIdAsync(getDummyRequest.Id)
                ?? throw new ItemNotFoundException(typeof(Dummy), getDummyRequest.Id);

            return _mapper.Map<Dummy, GetDummyResponse>(dummy);
        }
    }
}
