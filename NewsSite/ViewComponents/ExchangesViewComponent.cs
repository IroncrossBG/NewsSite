using Microsoft.AspNetCore.Mvc;
using NewsSite.Models.View;
using NewsSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsSite.ViewComponents
{
    [ViewComponent(Name = "Exchanges")]
    public class ExchangesViewComponent : ViewComponent
    {
        private readonly IExchangesService exchanges;

        public ExchangesViewComponent(IExchangesService exchanges)
        {
            this.exchanges = exchanges;
        }

        public IViewComponentResult Invoke()
        {
            var rawExchanges = exchanges.GetExchanges("https://www.bnb.bg/Statistics/StExternalSector/StExchangeRates/StERFixed/index.htm");
            var modelExchanges = new List<ExchangesViewModel>();

            rawExchanges.RemoveAt(0);

            foreach (var item in rawExchanges)
            {
                modelExchanges.Add(new ExchangesViewModel
                {
                    Name = item[0],
                    Code = item[1],
                    PerUnit = int.Parse(item[2]),
                    Course = double.Parse(item[3]),
                    ReverseCourse = double.Parse(item[4])
                });
            }

            return View(modelExchanges);
        }
    }
}
