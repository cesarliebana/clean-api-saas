using AutoMapper;
using MediatR;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;
using SaaV.SaaS.Core.Shared.Exceptions;

namespace SaaV.SaaS.Core.Domain.Handlers
{
    public class UpdateDummyHandler : IRequestHandler<UpdateDummyRequest, GetDummyResponse>
    {
        IDummyRepository _dummyRepository;
        IMapper _mapper;

        public UpdateDummyHandler(IDummyRepository dummyRepository, IMapper mapper)
        {
            _dummyRepository = dummyRepository;
            _mapper = mapper;
        }

        public async Task<GetDummyResponse> Handle(UpdateDummyRequest updateDummyRequest, CancellationToken cancellationToken)
        {
            Dummy dummy = await _dummyRepository.GetByIdAsync(updateDummyRequest.Id)
                ?? throw new ItemNotFoundException(typeof(Dummy), updateDummyRequest.Id);

            dummy.Update(updateDummyRequest.Name);
            await _dummyRepository.SaveChangesAsync();

            return _mapper.Map<Dummy, GetDummyResponse>(dummy);
        }
    }
}
