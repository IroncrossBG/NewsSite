using NewsSite.Data;
using NewsSite.Models.Data;
using NewsSite.Models.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace NewsSite.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly ApplicationDbContext db;
        public ArticlesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Add(CreateEditArticleInputModel model)
        {
            var newArticle = new Article
            {
                Title = model.Title,
                Subtitle = model.Subtitle,
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                Content = model.Content,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId
            };

            db.Add(newArticle);
            db.SaveChanges();
        }

        public void Edit(CreateEditArticleInputModel model)
        {
            var article = this.db.Articles.FirstOrDefault(x => x.Id == model.Id);
            article.Title = model.Title;
            article.Subtitle = model.Subtitle;
            article.Author = model.Author;
            article.Content = model.Content;
            article.ModifiedOn = DateTime.UtcNow;
            article.ImageUrl = model.ImageUrl;
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
        public void IncreaseViews(int id)
        {
            this.db.Articles.FirstOrDefault(x => x.Id == id).Views += 1;
            db.SaveChanges();
        }

        public Article GetById(int id) =>
             this.db.Articles.Select(x => new Article
             {
                 Id = x.Id,
                 Title = x.Title,
                 Subtitle = x.Subtitle,
                 Author = x.Author,
                 CreatedOn = x.CreatedOn,
                 ModifiedOn = x.ModifiedOn,
                 Content = x.Content,
                 Views = x.Views,
                 ImageUrl = x.ImageUrl,
                 CategoryId = x.CategoryId,
                 Category = x.Category,
                 Comments = this.db.Comments.Where(a => x.Id == a.ArticleId).Select(y => new Comment
                 {
                     Id = y.Id,
                     Article = this.db.Articles.FirstOrDefault(ca => ca.Id == y.ArticleId),
                     ArticleId = y.ArticleId,
                     Content = y.Content,
                     CreatedOn = y.CreatedOn,
                     User = y.User,
                 }).ToArray()
             })
            .FirstOrDefault(x => x.Id == id);

        public IEnumerable<Article> GetAll()
            => this.db.Articles.Select(x => new Article
            {
                Id = x.Id,
                Title = x.Title,
                Subtitle = x.Subtitle,
                Author = x.Author,
                CreatedOn = x.CreatedOn,
                ModifiedOn = x.ModifiedOn,
                Content = x.Content,
                Views = x.Views,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId,
                Category = x.Category,
                Comments = this.db.Comments.Where(a => x.Id == a.ArticleId).Select(y => new Comment
                {
                    Id = y.Id,
                    Article = this.db.Articles.FirstOrDefault(ca => ca.Id == y.ArticleId),
                    ArticleId = y.ArticleId,
                    Content = y.Content,
                    CreatedOn = y.CreatedOn,
                    User = y.User,
                }).ToArray()
            })
            .ToArray();
    }
}
