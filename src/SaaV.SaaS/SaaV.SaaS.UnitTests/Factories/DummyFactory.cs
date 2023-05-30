using Bogus;
using SaaV.SaaS.Core.Domain.Requests;
using SaaV.SaaS.Core.Domain.Responses;

namespace SaaV.SaaS.UnitTests.Factories
{
    internal static class DummyFactory
    {
        internal static CreateDummyRequest GetCreateDummyRequest() 
        {
            Faker faker = new("es");
            return new (Name: faker.Name.FullName());
        }

        internal static UpdateDummyRequest GetUpdateDummyRequest(GetDummyResponse getDummyResponse)
        {
            Faker faker = new("es");
            return new(getDummyResponse.Id, faker.Name.FullName());
        }
    }
}
