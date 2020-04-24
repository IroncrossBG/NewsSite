namespace NewsSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NewsSite.Models.Input;
    using NewsSite.Services;

    [Area("Administration")]
    [Authorize(Roles = "Administrator, Editor")]
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
        public async Task<IActionResult> Create(CreateEditArticleInputModel model)
        {
            if (ModelState.IsValid)
            {
                await articlesService.AddAsync(new
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

        public async Task<IActionResult> Delete(int id)
        {
            await articlesService.DeleteAsync(id);
            return RedirectToAction("All");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var article = articlesService.GetByIdAsync(id);
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
        public async Task<IActionResult> Edit(CreateEditArticleInputModel model)
        {
            await articlesService.EditAsync(model);
            return RedirectToAction("All");
        }

        public async Task<IActionResult> All()
        {
            return View(await articlesService.GetAllAsync());
        }
    }
}