using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        public IActionResult Id(int id)
        {
            var article = articlesService.GetById(id);
            if (article == null)
            {
                return View("ErrorStatus", 404);
            }

            articlesService.IncreaseViews(id);
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
        [Authorize(Roles = "Administrator, Editor, Member")]
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