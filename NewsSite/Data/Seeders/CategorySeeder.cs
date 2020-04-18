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
                        Name = "Мнения",
                        Description = "",
                    });

                    context.Categories.Add(
                    new Category
                    {
                        Name = "България",
                        Description = "",
                    });

                    context.Categories.Add(
                    new Category
                    {
                        Name = "Икономика",
                        Description = "",
                    });

                    context.Categories.Add(
                    new Category
                    {
                        Name = "Свят",
                        Description = "",
                    });

                    context.Categories.Add(
                    new Category
                    {
                        Name = "Култура",
                        Description = "",
                    });

                    context.Categories.Add(
                    new Category
                    {
                        Name = "Образование",
                        Description = "",
                    });

                    context.Categories.Add(
                    new Category
                    {
                        Name = "Здравеопазване",
                        Description = "",
                    });

                    context.Categories.Add(
                    new Category
                    {
                        Name = "Европа",
                        Description = "",
                    });


                    context.SaveChanges();
                }
            }
        }
    }
}
