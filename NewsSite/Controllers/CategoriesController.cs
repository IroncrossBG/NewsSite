using Microsoft.AspNetCore.Mvc;
using NewsSite.Models.Input;
using NewsSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public IActionResult Create()
        {
            return View(new CreateEditCategoryInputModel());
        }

        [HttpPost]
        public IActionResult Create(CreateEditCategoryInputModel model)
        {
            if (ModelState.IsValid)
            {
                categoryService.Add(new
                CreateEditCategoryInputModel
                {
                    Name = model.Name,
                    Description = model.Description
                });
                return Redirect("Home");
            }
            return View(model);
        }
    }
}
