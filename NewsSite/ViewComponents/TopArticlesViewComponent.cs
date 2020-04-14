using Microsoft.AspNetCore.Mvc;
using NewsSite.Models.View;
using NewsSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.ViewComponents
{
    [ViewComponent(Name = "TopArticles")]
    public class TopArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articlesService;

        public TopArticlesViewComponent(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IViewComponentResult Invoke()
        {
            var articles = articlesService
                .GetAll()
                .ToList();
            var topRead = articles
                .OrderByDescending(x => x.Views)
                .Take(10)
                .ToList();
            var topCommented = articles
                .Where(x => x.Comments.Count() > 0)
                .OrderByDescending(x => x.Comments.Count())
                .Take(10)
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
