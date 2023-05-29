using AutoMapper;
using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.Core.Shared.Interfaces;

namespace SaaV.SaaS.Core.Domain.Handlers
{
    public class CreateDummyHandler : IRequestHandler<CreateDummyRequest, GetDummyResponse>
    {
        private readonly IDummyRepository _dummyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITenantProvider _tenantProvider;

        public CreateDummyHandler(IDummyRepository dummyRepository, IUnitOfWork unitOfWork, IMapper mapper, ITenantProvider tenantProvider)
        {
            _dummyRepository = dummyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tenantProvider = tenantProvider;
        }

        public async Task<GetDummyResponse> Handle(CreateDummyRequest createDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = new Dummy(createDummyRequest.Name);
            dummy.TenantId = _tenantProvider.TenantId;
            _dummyRepository.Add(dummy);
            
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Dummy, GetDummyResponse>(dummy);
        }
    }
}
