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
            var category = GetById(model.Id);
            category.Name = model.Name;
            category.Description = model.Description;
            db.SaveChanges();
        }

        public Category GetById(int id)
        {
            return this.db.Categories.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return this.db.Categories.ToList();
        }
    }
}
