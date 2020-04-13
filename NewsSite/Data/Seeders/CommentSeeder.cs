using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NewsSite.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Data.Seeders
{
    public class CommentSeeder
    {
        public async Task Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (context.Comments.Count() == 0)
                {
                    var user = await userManager.FindByIdAsync("b7e74f1c-7809-4b89-acd6-b0f51cc44591");
                    await context.Comments.AddAsync(
                    new Comment
                    {
                        ArticleId = 1008,
                        Content = "TestTestTeTestTestTest",
                        CreatedOn = DateTime.UtcNow,
                        User = user
                    });

                    await context.Comments.AddAsync(
                    new Comment
                    {
                        ArticleId = 1008,
                        Content = "blahdwahkda",
                        CreatedOn = DateTime.UtcNow,
                        User = user
                    });

                    await context.Comments.AddAsync(
                    new Comment
                    {
                        ArticleId = 1008,
                        Content = "awdagawdasdawdawda",
                        CreatedOn = DateTime.UtcNow,
                        User = user
                    });

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
