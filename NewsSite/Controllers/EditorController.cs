using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Models.Input;
using NewsSite.Services;

namespace NewsSite.Controllers
{
    public class EditorController : Controller
    {
        private readonly IArticlesService articlesService;

        public EditorController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }
        public IActionResult Create()
        {
            return View(new CreateEditArticleInputModel());
        }

        [HttpPost]
        public IActionResult Create(CreateEditArticleInputModel model)
        {
            if (ModelState.IsValid)
            {
                articlesService.Add(new
                CreateEditArticleInputModel
                {
                    Title = model.Title,
                    Author = model.Author,
                    Content = model.Content,
                    Subtitle = model.Subtitle,
                    ImageUrl = model.ImageUrl,
                    CategoryId = model.CategoryId,
                });
                return RedirectToAction("All");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            articlesService.Delete(id);
            return RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            var article = articlesService.GetById(id);
            return View("Create", new CreateEditArticleInputModel
            {
                Id = article.Id,
                Title = article.Title,
                Author = article.Author,
                Subtitle = article.Subtitle,
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                CategoryId = article.CategoryId
            });
        }

        [HttpPost]
        public IActionResult Edit(CreateEditArticleInputModel model)
        {
            articlesService.Edit(model);
            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            return View(articlesService.GetAll());
        }
    }
}