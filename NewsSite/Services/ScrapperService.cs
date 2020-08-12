namespace NewsSite.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NewsSite.Data;
    using NewsSite.Models.Data;
    using NewsSite.Models.Info;
    using NewsSite.Scrappers;

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
            //var lastRun = await db.LastRun.Select(x => x.LastRun).FirstAsync();
            //if (DateTime.Now.Subtract(lastRun).TotalMinutes >= 30 || lastRun == null)
            //{
            var articlesRaw = await segabgScrapper.Run(from, to);

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
                Category category = new Category();
                switch (item["Category"])
                {
                    case "observer":
                        category = await categoryService.GetByNameAsync("Мнения", false);
                        break;
                    case "bulgaria":
                        category = await categoryService.GetByNameAsync("България", false);
                        break;
                    case "economy":
                        category = await categoryService.GetByNameAsync("Икономика", false);
                        break;
                    case "foreign":
                        category = await categoryService.GetByNameAsync("Свят", false);
                        break;
                    case "culture":
                        category = await categoryService.GetByNameAsync("Култура", false);
                        break;
                    case "education":
                        category = await categoryService.GetByNameAsync("Образование", false);
                        break;
                    case "health":
                        category = await categoryService.GetByNameAsync("Здравеопазване", false);
                        break;
                    default:
                        category = await categoryService.GetByNameAsync("Други", false);
                        break;
                }
                resultArticle.CategoryId = category.Id;

                await db.AddAsync(resultArticle);

                var lastRunInfo = new LastRunInfo();
                lastRunInfo.Name = "SEGA SCRAPPER";
                lastRunInfo.LastRun = DateTime.Now;
                await db.AddAsync(lastRunInfo);
            }

            await db.SaveChangesAsync();
            //}    
        }
    }
}
