﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsSite.Models;
using NewsSite.Services;

namespace NewsSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScrapperService scrapperService;

        public HomeController(ILogger<HomeController> logger, IScrapperService scrapperService)
        {
            _logger = logger;
            this.scrapperService = scrapperService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Scrapper()
        {
            scrapperService.RunSegaScrapper(DateTime.Now.AddDays(-10), DateTime.Now);
            return Redirect("/Editor/All");
        }
    }
}
