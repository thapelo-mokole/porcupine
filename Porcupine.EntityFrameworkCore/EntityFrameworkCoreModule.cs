using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Porcupine.Core.Shared;
using Porcupine.EntityFrameworkCore.EntityFrameworkCore;
using Porcupine.EntityFrameworkCore.Repositories.Groups;
using Porcupine.EntityFrameworkCore.Repositories.Permissions;
using Porcupine.EntityFrameworkCore.Repositories.Users;

namespace Porcupine.EntityFrameworkCore
{
    public static class EntityFrameworkCoreModule
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);

            services.AddRepositories();

            return services;
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfig = configuration.GetSection("Database").Get<DatabaseConfiguration>();

            services.AddDbContext<DatabaseContext>(options =>
                   options.UseSqlServer(databaseConfig.ConnectionString,
                       opt => opt.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
        }
    }
}