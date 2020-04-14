using Microsoft.AspNetCore.Mvc;
using NewsSite.Models.View;
using NewsSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.ViewComponents
{
    [ViewComponent(Name = "LastArticles")]
    public class LastArticlesViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public LastArticlesViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IViewComponentResult Invoke(string categoryName)
        {
            var category = categoryService.GetByName(categoryName, true);
            var articles = category.Articles.OrderByDescending(x => x.CreatedOn).Take(5).ToList();
            var resultModel = new LastArticlesViewModel
            {
                Title = category.Name,
                Articles = articles
            };

            return View(resultModel);
        }
    }
}
