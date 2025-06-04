using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using ASimmo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASimmo
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
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddRazorPages();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdminAccess", policy => policy.RequireRole("Admin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
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
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            var scope = scopeFactory.CreateScope();
            CreateRoles(scope).GetAwaiter().GetResult();
        }
        private async Task CreateRoles(IServiceScope scope)
        {

            var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "Agent", "Membre" };

            IdentityResult roleResult;
            foreach(var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);

                if(!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }

            }
            IdentityUser admin = await UserManager.FindByEmailAsync(Configuration.GetSection("DefaultUsers").GetSection("admin").GetValue<string>("UserEmail"));

            if(admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = Configuration.GetSection("DefaultUsers").GetSection("admin")["UserEmail"],
                    Email = Configuration.GetSection("DefaultUsers").GetSection("admin")["UserEmail"]

                };
                await UserManager.CreateAsync(admin, Configuration.GetSection("DefaultUsers").GetSection("admin").GetValue<string>("UserPassword"));
            }
            await UserManager.AddToRoleAsync(admin, roleNames[0]);

            IdentityUser agent1 = await UserManager.FindByEmailAsync(Configuration.GetSection("DefaultUsers").GetSection("agent1")["UserEmail"]);

            if (agent1 == null)
            {
                agent1 = new IdentityUser
                {
                    UserName = Configuration.GetSection("DefaultUsers").GetSection("agent1")["UserEmail"],
                    Email = Configuration.GetSection("DefaultUsers").GetSection("agent1")["UserEmail"]

            };
                await UserManager.CreateAsync(agent1, Configuration.GetSection("DefaultUsers").GetSection("agent1").GetValue<string>("UserPassword"));
            }
            await UserManager.AddToRoleAsync(agent1, roleNames[1]);

            IdentityUser agent2 = await UserManager.FindByEmailAsync(Configuration.GetSection("DefaultUsers").GetSection("agent2").GetValue<string>("UserEmail"));

            if (agent2 == null)
            {
                agent2 = new IdentityUser
                {
                    UserName = Configuration.GetSection("DefaultUsers").GetSection("agent2").GetValue<string>("UserEmail"),
                    Email = Configuration.GetSection("DefaultUsers").GetSection("agent2").GetValue<string>("UserEmail")

                };
                await UserManager.CreateAsync(agent2, Configuration.GetSection("DefaultUsers").GetSection("agent2").GetValue<string>("UserPassword"));
            }
            await UserManager.AddToRoleAsync(agent2, roleNames[1]);

            IdentityUser membre = await UserManager.FindByEmailAsync(Configuration.GetSection("DefaultUsers").GetSection("membre").GetValue<string>("UserEmail"));

            if (membre == null)
            {
                membre = new IdentityUser
                {
                    UserName = Configuration.GetSection("DefaultUsers").GetSection("membre").GetValue<string>("UserEmail"),
                    Email = Configuration.GetSection("DefaultUsers").GetSection("membre").GetValue<string>("UserEmail")

                };
                await UserManager.CreateAsync(membre, Configuration.GetSection("DefaultUsers").GetSection("membre").GetValue<string>("UserPassword"));
            }
            await UserManager.AddToRoleAsync(membre, roleNames[2]);
        }
    }
}
