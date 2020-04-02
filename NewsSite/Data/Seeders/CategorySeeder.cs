using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NewsSite.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Data.Seeders
{
    public class CategorySeeder
    {
        public void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (context.Categories.Count() == 0)
                {
                    context.Categories.Add(
                    new Category
                    {
                        Name = "Bulgaria",
                        Description = "Bulgarian Category",
                    });

                    context.Categories.Add(
                    new Category
                    {
                        Name = "Europe",
                        Description = "Europe Category",
                    });

                    context.Categories.Add(
                    new Category
                    {
                        Name = "World",
                        Description = "World Category",
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
