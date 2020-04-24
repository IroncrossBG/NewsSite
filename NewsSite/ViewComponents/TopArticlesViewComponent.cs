namespace NewsSite.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using NewsSite.Models.View;
    using NewsSite.Services;

    [ViewComponent(Name = "TopArticles")]
    public class TopArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articlesService;

        public TopArticlesViewComponent(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var articles = await articlesService
                .GetAllAsync();
            var topRead = articles
                .OrderByDescending(x => x.Views)
                .Take(count)
                .ToList();
            var topCommented = articles
                .Where(x => x.Comments.Count() > 0)
                .OrderByDescending(x => x.Comments.Count())
                .Take(count)
                .ToList();

            var resultModel = new TopArticlesViewModel
            {
                TopRead = topRead,
                TopCommented = topCommented
            };

            return View(resultModel);
        }
    }
}
