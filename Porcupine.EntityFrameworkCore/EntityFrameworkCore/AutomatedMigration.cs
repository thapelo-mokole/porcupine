using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Porcupine.EntityFrameworkCore.EntityFrameworkCore
{
    public static class AutomatedMigration
    {
        public static async Task MigrateAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<DatabaseContext>();

            if (context.Database.IsSqlServer()) await context.Database.MigrateAsync();

            await DatabaseContextSeed.SeedDatabaseAsync(context);
        }
    }
}