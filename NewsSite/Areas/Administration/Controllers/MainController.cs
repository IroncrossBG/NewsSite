namespace NewsSite.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using NewsSite.Areas.Administration.Models;
    using NewsSite.Data;
    using NewsSite.Services;

    [Area("Administration")]
    [Authorize(Roles = "Administrator, Editor")]
    public class MainController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IScrapperService scrapperService;

        public MainController(ApplicationDbContext db, UserManager<IdentityUser> userManager, IScrapperService scrapperService)
        {
            this.db = db;
            this.userManager = userManager;
            this.scrapperService = scrapperService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new MainIndexViewModel()
            {
                numberOfArticles = await db.Articles.CountAsync(),
                numberOfUsers = await userManager.Users.CountAsync()
            };
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Scrapper()
        {
            await scrapperService.RunSegaScrapper(DateTime.Now.AddDays(-10), DateTime.Now);
            return RedirectToAction("All", "Editor");
        }
    }
}