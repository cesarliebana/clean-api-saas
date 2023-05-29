using AutoMapper;
using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.Core.Shared.Exceptions;
using SaaV.SaaS.Core.Shared.Interfaces;

namespace SaaV.SaaS.Core.Domain.Handlers
{
    public class UpdateDummyHandler : IRequestHandler<UpdateDummyRequest, GetDummyResponse>
    {
        private readonly IDummyRepository _dummyRepository;
        private readonly ICredentialProvider _credentialProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDummyHandler(IDummyRepository dummyRepository, IUnitOfWork unitOfWork, IMapper mapper, ICredentialProvider credentialProvider)
        {
            _dummyRepository = dummyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _credentialProvider = credentialProvider;
        }

        public async Task<GetDummyResponse> Handle(UpdateDummyRequest updateDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = await _dummyRepository.GetByIdAsync(updateDummyRequest.Id)
                ?? throw new ItemNotFoundException(typeof(Dummy), updateDummyRequest.Id);

            dummy.Update(_credentialProvider.Credential, updateDummyRequest.Name);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Dummy, GetDummyResponse>(dummy);
        }
    }
}
