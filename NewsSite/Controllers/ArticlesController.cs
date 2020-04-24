namespace NewsSite.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using NewsSite.Models.Data;
    using NewsSite.Models.Input;
    using NewsSite.Models.View;
    using NewsSite.Services;

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
        [HttpGet]
        public async Task<IActionResult> Id(int id)
        {
            var article = articlesService.GetByIdAsync(id);
            if (article == null)
            {
                return View("ErrorStatus", 404);
            }

            await articlesService.IncreaseViewsAsync(id);
            var result = new ArticleViewModel()
            {
                Id = article.Id,
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
            await commentsService.CreateAsync(result);
            return Redirect(string.Concat(id, "#comments"));
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public async Task<IActionResult> DeleteComment(int articleId, int commentId)
        {
            await commentsService.DeleteAsync(commentId);
            return Redirect(string.Concat("Id/", articleId, "#comments"));
        }
    }
}