using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SaaV.SaaS.Core.Domain;
using SaaV.SaaS.Core.Shared.Interfaces;

namespace SaaV.SaaS.Infrastructure.Persistence
{
    public class SaaSDbContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;

        public SaaSDbContext(DbContextOptions<SaaSDbContext> options, ITenantProvider tenantProvider) : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        #region DbSets
        public virtual DbSet<Dummy> Dummies { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Global Conventios
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
               
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    if (property.ClrType.Equals(typeof(DateTime))) property.SetColumnType("datetime");
                    else if (property.ClrType.Equals(typeof(decimal))) property.SetColumnType("decimal(10,2)");
                    else if (property.ClrType.Equals(typeof(string)))
                    {
                        property.IsNullable = false;
                        property.SetMaxLength(255);
                        property.SetIsUnicode(true);
                    }
                }
            }
            #endregion

            #region Entities
            modelBuilder
                .Entity<Dummy>()
                .HasQueryFilter(dummy => !dummy.IsDeleted && dummy.TenantId == _tenantProvider.TenantId);
            #endregion
        }
    }
}
