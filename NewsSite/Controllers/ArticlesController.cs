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

        public IActionResult Create()
        {
            return View(new AddArticleModel());
        }

        [HttpPost]
        public IActionResult Create(AddArticleModel model)
        {
            if (ModelState.IsValid)
            {
                articlesService.Add(new
                AddArticleModel
                {
                    Title = model.Title,
                    Author = model.Author,
                    Content = model.Content,
                    Subtitle = model.Subtitle,
                });
                return View("All", articlesService.GetAll());
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            articlesService.Delete(id);
            return View("All", articlesService.GetAll());
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
    }
}
