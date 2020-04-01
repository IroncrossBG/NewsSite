using Microsoft.AspNetCore.Mvc;
using NewsSite.Models.Input;
using NewsSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IActionResult New()
        {
            return View();
        }

        public IActionResult Add()
        {
            articlesService.AddArticle(new
                AddArticleModel
            { 
                Title = "Test title",
                Author = "Author",
                Content = "Content",
                Subtitle = "Test subtitle",
            });
            return this.Ok();
        }
        public IActionResult All()
        {
            return View();
        }
    }
}
