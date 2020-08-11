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
                    switch (item["Category"])
                    {
                        case "observer":
                            resultArticle.CategoryId = categoryService.GetByNameAsync("Мнения", false).Result.Id;
                            break;
                        case "bulgaria":
                            resultArticle.CategoryId = categoryService.GetByNameAsync("България", false).Result.Id;
                            break;
                        case "economy":
                            resultArticle.CategoryId = categoryService.GetByNameAsync("Икономика", false).Result.Id;
                            break;
                        case "foreign":
                            resultArticle.CategoryId = categoryService.GetByNameAsync("Свят", false).Result.Id;
                            break;
                        case "culture":
                            resultArticle.CategoryId = categoryService.GetByNameAsync("Култура", false).Result.Id;
                            break;
                        case "education":
                            resultArticle.CategoryId = categoryService.GetByNameAsync("Образование", false).Result.Id;
                            break;
                        case "health":
                            resultArticle.CategoryId = categoryService.GetByNameAsync("Здравеопазване", false).Result.Id;
                            break;
                        default:
                            resultArticle.CategoryId = categoryService.GetByNameAsync("Други", false).Result.Id;
                            break;
                    }

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
