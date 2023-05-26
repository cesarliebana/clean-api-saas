using AutoMapper;
using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;

namespace SaaV.SaaS.Core.Domain.Handlers
{
    public class CreateDummyHandler : IRequestHandler<CreateDummyRequest, GetDummyResponse>
    {
        IDummyRepository _dummyRepository;
        IMapper _mapper;

        public CreateDummyHandler(IDummyRepository dummyRepository, IMapper mapper)
        {
            _dummyRepository = dummyRepository;
            _mapper = mapper;
        }

        public async Task<GetDummyResponse> Handle(CreateDummyRequest createDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = new Dummy(createDummyRequest.Name);
            _dummyRepository.Add(dummy);
            await _dummyRepository.SaveChangesAsync();

            return _mapper.Map<Dummy, GetDummyResponse>(dummy);
        }
    }
}
