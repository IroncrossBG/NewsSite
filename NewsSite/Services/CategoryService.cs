using NewsSite.Data;
using NewsSite.Models.Data;
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
