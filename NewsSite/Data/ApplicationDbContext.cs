using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsSite.Models.Data;

namespace NewsSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Article> Articles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Article>().HasData(
                new Article { Id = 1, Title = "Коронавирус", Author = "ASP.NET Core", Content = "Корона", CreatedOn = DateTime.UtcNow, ModifiedOn = DateTime.UtcNow },
                new Article { Id = 2, Title = "Борисов", Author = "ASP.NET Core", Content = "Бойко", CreatedOn = DateTime.UtcNow, ModifiedOn = DateTime.UtcNow },
                new Article { Id = 3, Title = "България", Author = "ASP.NET Core", Content = "БГ", CreatedOn = DateTime.UtcNow, ModifiedOn = DateTime.UtcNow }
                );
        }
    }
}
