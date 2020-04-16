﻿using HtmlAgilityPack;
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
        public BnbCourses(HttpClient client)
        {
            this.client = client;
            this.document = new HtmlDocument();
        }

        public async Task<List<List<string>>> GetExchangesAsync(string url)
        {
            var html = client.GetAsync(url);
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
    }
}
