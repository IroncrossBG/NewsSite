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
using NewsSite.Data.Seeders;
using NewsSite.Scrappers;

namespace NewsSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                endpoints.MapRazorPages();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }

            //SEEDERS
            var categorySeeder = new CategorySeeder();
            categorySeeder.Seed(app);

            var articleSeeder = new ArticleSeeder();
            articleSeeder.Seed(app);

            var commentsSeeder = new CommentSeeder();
            commentsSeeder.Seed(app);
        }
    }
}
