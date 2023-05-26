using AutoMapper;
using SaaV.SaaS.Core.Domain;
using SaaV.SaaS.Core.Domain.Responses;

namespace SaaV.SaaS.Core.Shared.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Dummies mappings
            CreateMap<Dummy, GetDummyResponse>();
        }
    }
}
