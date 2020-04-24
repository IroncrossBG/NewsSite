namespace NewsSite.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using NewsSite.Models.View;
    using NewsSite.Services;

    [ViewComponent(Name = "LastArticles")]
    public class LastArticlesViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public LastArticlesViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryName)
        {
            var category = await categoryService.GetByNameAsync(categoryName, true);
            if (category != null)
            {
                var articles = category.Articles.OrderByDescending(x => x.CreatedOn).Take(5).ToList();
                var resultModel = new LastArticlesViewModel
                {
                    Title = category.Name,
                    Articles = articles
                };
                return View(resultModel);
            }
            return View();
        }
    }
}
