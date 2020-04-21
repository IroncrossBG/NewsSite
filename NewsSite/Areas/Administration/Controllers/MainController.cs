using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Areas.Administration.Models;
using NewsSite.Data;
using NewsSite.Services;

namespace NewsSite.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator, Editor")]
    public class MainController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<IdentityUser> userManager;

        public MainController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var model = new MainIndexViewModel()
            {
                numberOfArticles = db.Articles.Count(),
                numberOfUsers = userManager.Users.Count()
            };
            return View(model);
        }
    }
}