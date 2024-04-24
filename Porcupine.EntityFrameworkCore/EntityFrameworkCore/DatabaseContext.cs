using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Porcupine.Core.Common;
using Porcupine.Core.Shared.Utils.Interface;
using System.Reflection;

namespace Porcupine.EntityFrameworkCore.EntityFrameworkCore
{
    public class DatabaseContext : DbContext
    {
        private readonly IUtilityService _utilityService;

        public DatabaseContext(DbContextOptions options, IUtilityService utilityService) : base(options)
        {
            _utilityService = utilityService;
        }

        public DbSet<Core.Entities.User> Users { get; set; }
        public DbSet<Core.Entities.Group> Groups { get; set; }

        public DbSet<Core.Entities.Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _utilityService.GetUserId() ?? "Default";
                        entry.Entity.UpdatedBy = string.Empty;
                        entry.Entity.CreatedOn = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _utilityService.GetUserId() ?? "Default";
                        entry.Entity.UpdatedOn = DateTime.Now;
                        break;
                }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}