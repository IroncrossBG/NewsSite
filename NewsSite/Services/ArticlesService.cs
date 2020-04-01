using NewsSite.Data;
using NewsSite.Models.Data;
using NewsSite.Models.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace NewsSite.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly ApplicationDbContext db;
        public ArticlesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddArticle(AddArticleModel model)
        {
            var newArticle = new Article
            {
                Title = model.Title,
                Subtitle = model.Subtitle,
                Author = model.Title,
                CreatedOn = DateTime.UtcNow,
                Content = model.Content,
            };

            await db.AddAsync(newArticle);
            await db.SaveChangesAsync();
        }

        public Article GetById(int id)
        {
            return this.db.Articles.FirstOrDefault(x => x.Id == id);
        }
    }
}
