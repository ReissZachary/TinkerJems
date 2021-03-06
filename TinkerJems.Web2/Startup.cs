using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Web2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GraphQL;
using TinkerJems.Web2.graphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Services;

namespace TinkerJems.Web2
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(getConnectionString())
                );
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(SeedData.SecurityPolicy_Add, p => p.RequireRole(SeedData.AdminRoleName));
                options.AddPolicy(SeedData.SecurityPolicy_Edit, p => p.RequireRole(SeedData.AdminRoleName));
                options.AddPolicy(SeedData.SecurityPolicy_Delete, p => p.RequireRole(SeedData.AdminRoleName));
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddRazorRuntimeCompilation();

            services.AddScoped<IJewelryRepository, ApplicationDbContext>();
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<TinkerJemsSchema>();
            services.AddGraphQL(o => {o.ExposeExceptions = true; })
                .AddGraphTypes(ServiceLifetime.Scoped);
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IAdviceService, AdviceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }            
            
            UpdateDatabase(app);
            
            app.UseGraphQL<TinkerJemsSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            SeedData.EnsureInitialized(userManager, roleManager);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private string getConnectionString()
        {
            if (Configuration["PostgresConnectionString"] == null)
            {
                var connectionUrl = Configuration["DATABASE_URL"];
                var uri = new Uri(connectionUrl);
                var host = uri.Host;
                var port = uri.Port;
                var database = uri.Segments.Last();
                var parts = uri.AbsoluteUri.Split(':', '/', '@');
                var user = parts[3];
                var password = parts[4];

                return $"host={host}; port={port}; database={database}; username={user}; password={password}; SSL Mode=Prefer; Trust Server Certificate=true";
            }
            else
                return Configuration["PostgresConnectionString"];
        }
    }
}
