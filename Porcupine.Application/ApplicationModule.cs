using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Porcupine.Application.Common;
using Porcupine.Application.Contracts.Models.Groups;
using Porcupine.Application.Contracts.Models.Permissions;
using Porcupine.Application.Contracts.Models.Users;
using Porcupine.Application.Services;
using Porcupine.Core.Shared.Utils.Implementation;
using Porcupine.Core.Shared.Utils.Interface;

namespace Porcupine.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
        {
            // Add IHttpContextAccessor to the service collection
            services.AddHttpContextAccessor();

            services.AddServices(env);

            services.RegisterAutoMapper();

            return services;
        }

        private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IPermissionAppService, PermissionAppService>();
            services.AddScoped<IGroupAppService, GroupAppService>();
            services.AddScoped<IUtilityService, UtilityService>();
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IMappingProfilesMarker));
        }
    }
}