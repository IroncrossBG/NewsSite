using NewsSite.Data;
using NewsSite.Models.Data;
using NewsSite.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsSite.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext db;
        public CategoryService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Add(CreateEditCategoryInputModel model)
        {
            var newCategory = new Category
            {
                Name = model.Name,
                Description = model.Description
            };

            db.Add(newCategory);
            db.SaveChanges();
        }

        public void Edit(CreateEditCategoryInputModel model)
        {
            var category = GetById(model.Id, false);
            category.Name = model.Name;
            category.Description = model.Description;
            db.SaveChanges();
        }

        public Category GetById(int id, bool returnArticles)
        {
            var category = this.db.Categories.FirstOrDefault(x => x.Id == id);
            if (returnArticles)
            {
                category.Articles = this.db.Articles.Where(a => category.Id == a.CategoryId).ToArray();
            }
            return category;
        }

        public Category GetByName(string name, bool returnArticles)
        {
            var category = this.db.Categories.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            if (returnArticles)
            {
                category.Articles = this.db.Articles.Where(a => category.Id == a.CategoryId).ToArray();
            }
            return category;
        }

        public IEnumerable<Category> GetAll() =>
           this.db.Categories.Select(x => new Category
           {
               Id = x.Id,
               Name = x.Name,
               Description = x.Description,
               Articles = this.db.Articles.Where(a => x.Id == a.CategoryId).ToArray()
           })
           .ToArray();
    }
}
