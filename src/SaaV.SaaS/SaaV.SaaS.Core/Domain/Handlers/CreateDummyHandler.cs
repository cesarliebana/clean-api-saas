using AutoMapper;
using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.Core.Shared.Interfaces;

namespace SaaV.SaaS.Core.Domain.Handlers
{
    public class CreateDummyHandler : IRequestHandler<CreateDummyRequest, GetDummyResponse>
    {
        IDummyRepository _dummyRepository;
        IMapper _mapper;
        ITenantProvider _tenantProvider;

        public CreateDummyHandler(IDummyRepository dummyRepository, IMapper mapper, ITenantProvider tenantProvider)
        {
            _dummyRepository = dummyRepository;
            _mapper = mapper;
            _tenantProvider = tenantProvider;
        }

        public async Task<GetDummyResponse> Handle(CreateDummyRequest createDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = new Dummy(createDummyRequest.Name);
            dummy.TenantId = _tenantProvider.GetTenantId();
            _dummyRepository.Add(dummy);
            await _dummyRepository.SaveChangesAsync();

            return _mapper.Map<Dummy, GetDummyResponse>(dummy);
        }
    }
}
