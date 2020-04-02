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
        public void Add(AddArticleModel model)
        {
            var newArticle = new Article
            {
                Title = model.Title,
                Subtitle = model.Subtitle,
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                Content = model.Content,
                CategoryId = model.CategoryId
            };

            db.Add(newArticle);
            db.SaveChanges();
        }

        public void Edit(AddArticleModel model)
        {
            var article = GetById(model.Id);
            article.Title = model.Title;
            article.Subtitle = model.Subtitle;
            article.Author = model.Author;
            article.Content = model.Content;
            article.ModifiedOn = DateTime.UtcNow;
            article.CategoryId = model.CategoryId;
            db.SaveChanges();
        }
        
        public void Delete(int id)
        {
            var article = this.db.Articles.FirstOrDefault(x => x.Id == id);
            if (article != null)
            {
                this.db.Remove(article);
                this.db.SaveChanges();
            }
        }

        public Article GetById(int id)
        {
            return this.db.Articles.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Article> GetAll()
        {
            var result = this.db.Articles.ToList();
            foreach (var item in result)
            {
                item.Category = this.db.Categories.FirstOrDefault(x => x.Id == item.CategoryId);
            }
            return result;
        }
    }
}
