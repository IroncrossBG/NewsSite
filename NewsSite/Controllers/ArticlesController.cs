using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Models.Data;
using NewsSite.Models.Input;
using NewsSite.Models.View;
using NewsSite.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsSite.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesService;
        private readonly ICommentsService commentsService;
        private readonly UserManager<IdentityUser> userManager;

        public ArticlesController(IArticlesService articlesService, ICommentsService commentsService, UserManager<IdentityUser> userManager)
        {
            this.articlesService = articlesService;
            this.commentsService = commentsService;
            this.userManager = userManager;
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
                ImageUrl = article.ImageUrl,
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
                ImageUrl = article.ImageUrl,
                CategoryId = article.CategoryId,
                Category = article.Category,
                Comments = article.Comments
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Id(int id, string commentContent)
        {
            var result = new CreateEditCommentInputModel
            {
                ArticleId = id,
                Content = commentContent,
                User = await userManager.GetUserAsync(this.HttpContext.User),
            };
            commentsService.Create(result);
            return RedirectToAction("Id", id);
        }
    }
}