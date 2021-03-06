﻿namespace NewsSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using NewsSite.Services;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string name)
        {
            var result = await categoryService.GetByNameAsync(name, true);
            if (result == null)
            {
                return View("ErrorStatus", 404);
            }

            return View(result);
        }
    }
}
