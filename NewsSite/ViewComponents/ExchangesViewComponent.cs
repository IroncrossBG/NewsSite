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
        private readonly IExchangesService exchangesService;
        public ExchangesViewComponent(IExchangesService exchangesService)
        {
            this.exchangesService = exchangesService;
        }
        public IViewComponentResult Invoke(string filter)
        {
            var baseExchange = new List<List<string>>();
            var modelExchanges = new List<ExchangesViewModel>();
            var date = DateTime.Now.ToString("dd.MM.yyyy");
            var dateMinus = DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy");

            if (!exchangesService.Check(date))
            {
                if (!exchangesService.Check(dateMinus))
                {
                    exchangesService.Add("https://www.bnb.bg/Statistics/StExternalSector/StExchangeRates/StERFixed/index.htm");
                    exchangesService.Add("https://www.bnb.bg/Statistics/StExternalSector/StExchangeRates/StERForeignCurrencies/index.htm");
                    baseExchange = exchangesService.Get(date);
                }
                else
                {
                    baseExchange = exchangesService.Get(dateMinus);
                }        
            }
            else
            {
                baseExchange = exchangesService.Get(date);
            }

            List<string> listFilter = filter.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var item in baseExchange)
            {
                if (listFilter.Count == 0)
                {
                    modelExchanges.Add(ConvertToModel(item));
                }
                else if (item.Any(x => listFilter.Contains(x)))
                {
                    modelExchanges.Add(ConvertToModel(item));
                }
            }

            if (listFilter.Count > 1)
            {
                modelExchanges = modelExchanges.OrderBy(x => listFilter.IndexOf(x.Code)).ToList();
            }
            return View(modelExchanges);
        }
        static protected ExchangesViewModel ConvertToModel(List<string> exchange)
        {
            var result = new ExchangesViewModel
            {
                Name = exchange[0],
                Code = exchange[1],
                PerUnit = exchange[2],
                Course = exchange[3],
                ReverseCourse = exchange[4]
            };
            return result;
        }
    }
}