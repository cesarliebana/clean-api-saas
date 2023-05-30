using Microsoft.EntityFrameworkCore;
using SaaV.SaaS.Infrastructure.Persistence;

namespace SaaV.SaaS.UnitTests.Tests
{
    public class SqlLiteTest
    {
        public SqlLiteTest(SaaSDbContext dbContext)
        {
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();
        }
    }
}