using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewsSite.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Data.Seeders
{
    public class ArticleSeeder
    {
        public void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (context.Articles.Count() == 0)
                {
                    context.Articles.Add(
                        new Article
                        {
                            Title = "Coronavirus",
                            Author = "ASP.NET Core",
                            Content = "Corona",
                            CreatedOn = DateTime.UtcNow,
                            ModifiedOn = DateTime.UtcNow,
                            CategoryId = 3
                        });

                    context.Articles.Add(
                        new Article
                        {
                            Title = "Boiko Borisov",
                            Author = "ASP.NET Core",
                            Content = "Boiko",
                            CreatedOn = DateTime.UtcNow,
                            ModifiedOn = DateTime.UtcNow,
                            CategoryId = 1
                        });

                    context.Articles.Add(
                        new Article
                        {
                            Title = "Evropeiski",
                            Author = "ASP.NET Core",
                            Content = "EU",
                            CreatedOn = DateTime.UtcNow,
                            ModifiedOn = DateTime.UtcNow,
                            CategoryId = 2
                        });

                    context.SaveChanges();
                }
            }
        }
    }
}
