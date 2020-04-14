using CustomIdentityWEB.Services;
using IdentityManager.DAL;
using IdentityManager.Entities.Custom;
using IdentityManager.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomIdentityWEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<RoleService>();
            services.AddScoped<UserRoleService>();
            services.AddScoped<ExternalLoginService>();

            services.AddTransient<IUserStore<SiteUser>, UserStore>();
            services.AddTransient<IRoleStore<SiteRole>, RoleStore>();

            services.AddIdentity<SiteUser, SiteRole>()
                    .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddAuthentication()
               .AddFacebook(facebookOptions =>
               {
                   // quick for now
                   facebookOptions.AppId = "1588121948019923";
                   facebookOptions.AppSecret = "4551f6292222a6c650384d43aefc697e";
               });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
