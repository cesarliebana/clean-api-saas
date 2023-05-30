using Mapster;
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
        private readonly ICredentialProvider _credentialProvider;

        public CreateDummyHandler(IDummyRepository dummyRepository, IUnitOfWork unitOfWork, ICredentialProvider credentialProvider)
        {
            _dummyRepository = dummyRepository;
            _unitOfWork = unitOfWork;
            _credentialProvider = credentialProvider;
        }

        public async Task<GetDummyResponse> Handle(CreateDummyRequest createDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = new Dummy(
                createDummyRequest.Name, 
                _credentialProvider.Credential.TenantId,
                _credentialProvider.Credential.UserId,
                _credentialProvider.Credential.UserName);

            _dummyRepository.Add(dummy);
            
            await _unitOfWork.SaveChangesAsync();

            return dummy.Adapt<GetDummyResponse>();
        }
    }
}
