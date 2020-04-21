using NewsSite.Data;
using NewsSite.Models.Data;
using NewsSite.Scrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public class ScrapperService : IScrapperService
    {
        private readonly ApplicationDbContext db;
        private readonly ICategoryService categoryService;
        private readonly SegabgScrapper segabgScrapper;
        public ScrapperService(ApplicationDbContext db, ICategoryService categoryService)
        {
            this.db = db;
            this.categoryService = categoryService;
            segabgScrapper = new SegabgScrapper();
        }
        public async Task RunSegaScrapper(DateTime from, DateTime to)
        {
            var articlesRaw = segabgScrapper.Run(from, to).Result;

            foreach (var item in articlesRaw)
            {
                if (db.Articles.Any(x => x.Title == item["Title"]))
                {
                    continue;
                }

                var resultArticle = new Article();
                resultArticle.Title = item["Title"];
                resultArticle.Subtitle = item["Subtitle"];
                resultArticle.Author = item["Author"];
                resultArticle.ImageUrl = item["ImageUrl"];
                resultArticle.Content = item["Content"];
                resultArticle.CreatedOn = DateTime.Parse(item["DatePublished"]);
                resultArticle.ModifiedOn = DateTime.Parse(item["DateModified"]);
                switch (item["Category"])
                {
                    case "observer": resultArticle.CategoryId = categoryService.GetByNameAsync("Мнения", false).Id; break;
                    case "bulgaria": resultArticle.CategoryId = categoryService.GetByNameAsync("България", false).Id; break;
                    case "economy": resultArticle.CategoryId = categoryService.GetByNameAsync("Икономика", false).Id; break;
                    case "foreign": resultArticle.CategoryId = categoryService.GetByNameAsync("Свят", false).Id; break;
                    case "culture": resultArticle.CategoryId = categoryService.GetByNameAsync("Култура", false).Id; break;
                    case "education": resultArticle.CategoryId = categoryService.GetByNameAsync("Образование", false).Id; break;
                    case "health": resultArticle.CategoryId = categoryService.GetByNameAsync("Здравеопазване", false).Id; break;
                    default: resultArticle.CategoryId = categoryService.GetByNameAsync("Други", false).Id; break;
                }
                await db.AddAsync(resultArticle);
            }
            await db.SaveChangesAsync();
        }
    }
}
