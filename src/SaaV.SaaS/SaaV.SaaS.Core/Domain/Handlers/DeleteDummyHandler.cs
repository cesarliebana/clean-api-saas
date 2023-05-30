using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Shared.Exceptions;
using SaaV.SaaS.Core.Shared.Interfaces;

namespace SaaV.SaaS.Core.Domain.Handlers
{
    public class DeleteDummyHandler : IRequestHandler<DeleteDummyRequest>
    {
        private readonly IDummyRepository _dummyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICredentialProvider _credentialProvider;

        public DeleteDummyHandler(IDummyRepository dummyRepository, IUnitOfWork unitOfWork, ICredentialProvider credentialProvider)
        {
            _dummyRepository = dummyRepository;
            _unitOfWork = unitOfWork;
            _credentialProvider = credentialProvider;
        }

        public async Task Handle(DeleteDummyRequest deleteDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = await _dummyRepository.GetByIdAsync(deleteDummyRequest.Id) 
                ?? throw new ItemNotFoundException(typeof(Dummy), deleteDummyRequest.Id);
            
            dummy.MarkAsDeleted(_credentialProvider.Credential.UserId, _credentialProvider.Credential.UserName);
            
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
