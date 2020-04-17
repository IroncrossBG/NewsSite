using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Services;

namespace NewsSite.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }
        [HttpGet]
        public IActionResult Index(string q)
        {
            string query = Request.Query["q"].ToString();
            var searchResult = searchService.Search(query);

            return View();
        }
    }
}