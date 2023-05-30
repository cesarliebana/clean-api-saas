using Bogus;
using SaaV.SaaS.Core.Shared.Interfaces;
using SaaV.SaaS.Core.Shared.ValueObjects;

namespace SaaV.SaaS.UnitTest.Providers
{
    public class CredentialProvider : ICredentialProvider
    {
        public Credential Credential 
        {
            get
            {
                Faker faker = new("es");
                return new Credential(
                    Guid.NewGuid().ToString(),
                    $"{faker.Name.FirstName()}.{faker.Name.LastName}",
                    1);
            } 
        }
    }
}

      
