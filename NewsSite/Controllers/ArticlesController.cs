﻿using Microsoft.AspNetCore.Mvc;
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

        public ArticlesController(IArticlesService articlesService, ICategoryService categoryService)
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
                    CategoryId = model.CategoryId,
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
                CategoryId = article.CategoryId
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
                Category = article.Category
            };

            return View(result);
        }
    }
}
