using Microsoft.AspNetCore.Mvc;
using NewsSite.Models.Input;
using NewsSite.Models.View;
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
        private readonly ICategoryService categoryService;

        public ArticlesController(IArticlesService articlesService, ICategoryService categoryService)
        {
            this.articlesService = articlesService;
            this.categoryService = categoryService;
        }

        public IActionResult Create()
        {
            var result = new CreateEditArticleInputModel();
            result.Categories = categoryService.GetAll();
            return View(result);
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
                    CategoryId = model.CategoryId,
                    Categories = model.Categories,
                });
                return Redirect("All");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            articlesService.Delete(id);
            return Redirect("/Articles/All");
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
                CategoryId = article.CategoryId,
                Categories = categoryService.GetAll()
        });
        }

        [HttpPost]
        public IActionResult Edit(CreateEditArticleInputModel model)
        {
            articlesService.Edit(model);
            return Redirect("/Articles/All");
        }

        public IActionResult All()
        {
            return View(articlesService.GetAll());
        }
        public IActionResult Id(int id)
        {
            articlesService.IncreaseViews(id);
            var article = articlesService.GetById(id);
            var result = new ArticleViewModel()
            {
                Title = article.Title,
                Subtitle = article.Subtitle,
                Author = article.Author,
                CreatedOn = article.CreatedOn,
                Content = article.Content,
                Views = article.Views,
                CategoryId = article.CategoryId,
            };

            return View(result);
        }
    }
}
