using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Shared.Exceptions;

namespace SaaV.SaaS.Core.Domain.Handlers
{
    public class DeleteDummyHandler : IRequestHandler<DeleteDummyRequest>
    {
        IDummyRepository _dummyRepository;

        public DeleteDummyHandler(IDummyRepository dummyRepository) 
        {
            _dummyRepository = dummyRepository;
        }

        public async Task Handle(DeleteDummyRequest deleteDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = await _dummyRepository.GetByIdAsync(deleteDummyRequest.Id) 
                ?? throw new ItemNotFoundException(typeof(Dummy), deleteDummyRequest.Id);
            
            dummy.MarkAsDeleted();
            await _dummyRepository.SaveChangesAsync();
        }
    }
}
