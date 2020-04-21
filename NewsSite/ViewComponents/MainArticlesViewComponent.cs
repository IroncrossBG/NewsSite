using Microsoft.AspNetCore.Mvc;
using NewsSite.Models.View;
using NewsSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.ViewComponents
{
    [ViewComponent(Name = "MainArticles")]
    public class MainArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articlesService;

        public MainArticlesViewComponent(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await articlesService
                .GetAllAsync();
            articles = articles
                .OrderByDescending(x => x.CreatedOn)
                .ToList();

            var resultModel = new MainArticlesViewModel
            {
                Articles = articles
            };

            return View(resultModel);
        }
    }
}
