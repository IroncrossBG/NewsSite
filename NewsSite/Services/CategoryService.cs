namespace NewsSite.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NewsSite.Data;
    using NewsSite.Models.Data;
    using NewsSite.Models.Input;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext db;
        public CategoryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(string name, string description)
        {
            var newCategory = new Category
            {
                Name = name,
                Description = description
            };

            await db.AddAsync(newCategory);
            await db.SaveChangesAsync();
        }
        public async Task<Category> GetByIdAsync(int id, bool returnArticles)
        {
            var category = await this.db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (returnArticles)
            {
                category.Articles = await this.db.Articles.Where(a => category.Id == a.CategoryId).ToArrayAsync();
            }
            return category;
        }

        public async Task<Category> GetByNameAsync(string name, bool returnArticles)
        {
            var category = await this.db.Categories.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            if (category == null)
            {
                return null;
            }
            else
            {
                if (returnArticles == true)
                {
                    category.Articles = await this.db.Articles.Where(a => category.Id == a.CategoryId).ToArrayAsync();
                }
                return category;
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
           await this.db.Categories.Select(x => new Category
           {
               Id = x.Id,
               Name = x.Name,
               Description = x.Description,
               Articles = this.db.Articles.Where(a => x.Id == a.CategoryId).ToArray()
           })
           .ToArrayAsync();
    }
}
