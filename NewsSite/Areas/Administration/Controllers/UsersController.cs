using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Areas.Administration.Models;

namespace NewsSite.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new UserInputModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserInputModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };

                var result = await userManager.CreateAsync(newUser, model.Password);
                
                if (result.Succeeded)
                {
                    var findRole = await roleManager.FindByIdAsync(model.RoleId);
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(newUser, findRole.Name);
                    newUserRole.Wait();
                    if (newUserRole.IsCompletedSuccessfully)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }

        public IActionResult Delete(string id)
        {
            var user = userManager.FindByIdAsync(id);
            user.Wait();
            userManager.DeleteAsync(user.Result);
            return View("Index");
        }
    }
}