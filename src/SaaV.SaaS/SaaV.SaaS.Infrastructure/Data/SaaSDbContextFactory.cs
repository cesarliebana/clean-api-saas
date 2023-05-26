using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SaaV.SaaS.Infrastructure.Data
{
    public class SaaSDbContextFactory : IDesignTimeDbContextFactory<SaaSDbContext>
    {
        public SaaSDbContext CreateDbContext(string[]? args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<SaaSDbContextFactory>()
            .Build();

            DbContextOptionsBuilder<SaaSDbContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(config.GetConnectionString("VeriskConnectionString"));

            return new SaaSDbContext(optionsBuilder.Options);
        }
    }
}
