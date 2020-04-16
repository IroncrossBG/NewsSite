using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsSite
{
    public class BnbCourses
    {
        private readonly HttpClient client;
        private readonly HtmlDocument document;
        private readonly string Url;
        public BnbCourses(HttpClient client, string url)
        {
            this.client = client;
            this.document = new HtmlDocument();
            this.Url = url;
        }

        public async Task<List<List<string>>> GetExchangesRawAsync()
        {
            var html = client.GetAsync(Url);
            document.LoadHtml(await html.Result.Content.ReadAsStringAsync());

            var coursesRaw = new List<List<string>>();
            var trList = document.DocumentNode.SelectNodes("//tr");

            if (trList.Count == 0)
            {
                throw new Exception("No courses!");
            }
            foreach (var childTr in trList)
            {
                var resultCourse = new List<string>();
                foreach (var childTd in childTr.ChildNodes)
                {
                    if (childTd.Name == "td")
                    {
                        resultCourse.Add(childTd.FirstChild.InnerText);
                    }
                }
                coursesRaw.Add(resultCourse);
            }
            return coursesRaw;
        }

        public async Task<List<ExchangeCourseModel>> GetExchangesAsync()
        {
            var rawExchanges = await GetExchangesRawAsync();
            var modelExchanges = new List<ExchangeCourseModel>();

            rawExchanges.RemoveAt(0);

            foreach (var item in rawExchanges)
            {
                modelExchanges.Add(new ExchangeCourseModel
                {
                    Name = item[0],
                    Code = item[1],
                    PerUnit = int.Parse(item[2]),
                    Course = double.Parse(item[3]),
                    ReverseCourse = double.Parse(item[4])
                });
            }

            return modelExchanges;
        }

        public class ExchangeCourseModel
        {
            public string Name { get; set; }

            public string Code { get; set; }

            public int PerUnit { get; set; }

            public double Course { get; set; }

            public double ReverseCourse { get; set; }
        }

    }
}
