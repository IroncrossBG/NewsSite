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

        public List<List<string>> GetExchanges(string url)
        {
            return courses.GetExchangesAsync(url).Result;
        }
    }
}
