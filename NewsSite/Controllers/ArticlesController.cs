using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Controllers
{
    public class ArticlesController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
