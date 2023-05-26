using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SaaV.SaaS.Infrastructure.Providers;

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
            optionsBuilder.UseSqlServer(config.GetConnectionString("SaaSConnectionString"));

            return new SaaSDbContext(optionsBuilder.Options, new TenantProvider());
        }
    }
}
