using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public class ExchangesService : IExchangesService
    {
        static readonly HttpClient client = new HttpClient();
        static readonly BnbCourses courses = new BnbCourses(client);
        static readonly Dictionary<string, List<List<string>>> exchanges = new Dictionary<string, List<List<string>>>();
        public void Add(string url)
        {
            string date = DateTime.Now.ToString("dd.MM.yyyy");
            if (!exchanges.ContainsKey(date))
            {
                var exchange = courses.GetExchangesAsync(url).Result;
                exchange.Remove(exchange.FirstOrDefault());
                exchanges.Add(date, exchange);
            }
            else
            {
                var exchange = courses.GetExchangesAsync(url).Result;
                exchange.Remove(exchange.FirstOrDefault());
                exchanges[date].AddRange(courses.GetExchangesAsync(url).Result);
            }
        }
        public List<List<string>> Get(string date)
        {
            if (exchanges.ContainsKey(date))
            {
                return exchanges[date];
            }
            else
            {
                throw new ArgumentException("Exchange doesn't exist!");
            }
        }

        public bool Check(string date)
        {
            return exchanges.ContainsKey(date);
        }
    }
}
