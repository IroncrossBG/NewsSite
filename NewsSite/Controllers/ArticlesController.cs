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

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(AddArticleModel model)
        {
            if (ModelState.IsValid)
            {
                articlesService.AddArticle(new
                AddArticleModel
                {
                    Title = model.Title,
                    Author = model.Author,
                    Content = model.Content,
                    Subtitle = model.Subtitle,
                });
            }
            return View();
        }

        public IActionResult All()
        {
            return View(articlesService.GetAll());
        }
        public IActionResult ById(int id)
        {
            var result = articlesService.GetById(id);
            return Json(result);
        }
        public IActionResult Delete(int id)
        {
            articlesService.Delete(id);
            return View("All");
        }
    }
}
