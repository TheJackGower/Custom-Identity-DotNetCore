using CustomNetCoreIdentity.Application.Services;
using CustomNetCoreIdentity.Domain.Entities;
using CustomNetCoreIdentity.Domain.Interfaces.Repositories;
using CustomNetCoreIdentity.Infrastructure.Repositories;
using IdentityManager.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace CustomNetCoreIdentity.Infrastructure
{
    public static class DependencyContainer
    {
        /// <summary>
        /// Connects our interfaces and their implementations from multiple projects 
        /// into single point of reference. That is the purpose of IoC layer.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(IServiceCollection services)
        {
            // Register Repo
            services.AddScoped<ISiteUserRepository, SiteUserRepository>();
            services.AddScoped<ISiteRoleRepository, SiteRoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IExternalLoginRepository, ExternalLoginRepository>();

            services.AddTransient<IUserStore<SiteUser>, UserStore>();
            services.AddTransient<IRoleStore<SiteRole>, RoleStore>();

            services.AddIdentity<SiteUser, SiteRole>()
                    .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
