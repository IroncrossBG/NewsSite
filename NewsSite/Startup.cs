using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsSite.Data;
using NewsSite.Services;
using NewsSite.Models.Data;
using NewsSite.Scrappers;

namespace NewsSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            Task<IdentityResult> roleResult;

            string[] roles = { "Administrator", "Editor", "Moderator", "Member" };

            //ADD ROLES
            foreach (var role in roles)
            {
                Task<bool> hasRole = roleManager.RoleExistsAsync(role);
                hasRole.Wait();

                if (!hasRole.Result)
                {
                    roleResult = roleManager.CreateAsync(new IdentityRole(role));
                    roleResult.Wait();
                }
            }
        }

        private void CreateAdminAccount(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            Task<IdentityResult> roleResult;

            Task<IdentityUser> adminUser = userManager.FindByNameAsync("admin");
            adminUser.Wait();

            if (adminUser.Result == null)
            {
                IdentityUser administrator = new IdentityUser();
                administrator.Email = "admin@bgnovini.com";
                administrator.UserName = "admin";

                Task<IdentityResult> newUser = userManager.CreateAsync(administrator, "Admin!23");
                newUser.Wait();

                if (newUser.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Administrator");
                    newUserRole.Wait();
                }
            }
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddTransient<IArticlesService, ArticlesService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IScrapperService, ScrapperService>();
            services.AddSingleton<IIpInfoService, IpInfoService>();
            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddSingleton<IExchangesService, ExchangesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseExceptionHandler("/Home/Error");
            app.UseStatusCodePagesWithReExecute("/Home/ErrorStatus", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "administration",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            CreateRoles(serviceProvider);
            CreateAdminAccount(serviceProvider);
        }
    }
}
