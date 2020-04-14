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

        public IViewComponentResult Invoke()
        {
            var articles = articlesService
                .GetAll()
                .OrderByDescending(x => x.CreatedOn)
                .ToArray();

            var resultModel = new MainArticlesViewModel
            {
                Articles = articles
            };

            return View(resultModel);
        }
    }
}
