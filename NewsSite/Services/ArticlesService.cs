namespace NewsSite.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using NewsSite.Data;
    using NewsSite.Models.Data;
    using NewsSite.Models.Input;

    public class ArticlesService : IArticlesService
    {
        private readonly ApplicationDbContext db;
        public ArticlesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(CreateEditArticleInputModel model)
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

            await db.AddAsync(newArticle);
            await db.SaveChangesAsync();
        }

        public async Task EditAsync(CreateEditArticleInputModel model)
        {
            var article = await this.db.Articles.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (article != null)
            {
                article.Title = model.Title;
                article.Subtitle = model.Subtitle;
                article.Author = model.Author;
                article.Content = model.Content;
                article.ModifiedOn = DateTime.UtcNow;
                article.ImageUrl = model.ImageUrl;
                article.CategoryId = model.CategoryId;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var article = await this.db.Articles.FirstOrDefaultAsync(x => x.Id == id);
            if (article != null)
            {
                this.db.Remove(article);
                await this.db.SaveChangesAsync();
            }
        }
        public async Task IncreaseViewsAsync(int id)
        {
            var article = await this.db.Articles.FirstOrDefaultAsync(x => x.Id == id);
            if (article != null)
            {
                article.Views += 1;
                await db.SaveChangesAsync();
            }
        }

        public Article GetByIdAsync(int id)
        {
            var article = this.db.Articles.Select(x => new Article
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
            if (article == null)
            {
                return null;
            }
            return article;
        }


        public async Task<List<Article>> GetAllAsync()
        {
            var result = await this.db.Articles.Select(x => new Article
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
            .ToListAsync();
            return result;
        }
    }
}
