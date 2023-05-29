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

        public DeleteDummyHandler(IDummyRepository dummyRepository, IUnitOfWork unitOfWork)
        {
            _dummyRepository = dummyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteDummyRequest deleteDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = await _dummyRepository.GetByIdAsync(deleteDummyRequest.Id) 
                ?? throw new ItemNotFoundException(typeof(Dummy), deleteDummyRequest.Id);
            
            dummy.MarkAsDeleted();
            
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
