using HtmlAgilityPack;
using NewsSite.Models.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NewsSite.Scrappers
{
    public class SegabgScrapper
    {        
        private readonly Dictionary<string, string> categoriesLinks = new Dictionary<string, string>()
        {
            { "observer", "https://www.segabg.com/category-observer" },
            { "bulgaria", "https://www.segabg.com/category-bulgaria" },
            { "economy", "https://www.segabg.com/category-economy" },
            { "foreign", "https://www.segabg.com/category-foreign-country" },
            { "culture", "https://www.segabg.com/category-culture" },
            { "education", "https://www.segabg.com/category-education" },
            { "health", "https://www.segabg.com/category-zdrave" }
        };

        public async Task<List<Dictionary<string, string>>> Run()
        {
            var observerLinks = await GetArticleLinks(categoriesLinks["observer"]);
            var bulgariaLinks = await GetArticleLinks(categoriesLinks["bulgaria"]);
            var economyLinks = await GetArticleLinks(categoriesLinks["economy"]);
            var foreignLinks = await GetArticleLinks(categoriesLinks["foreign"]);
            var cultureLinks = await GetArticleLinks(categoriesLinks["culture"]);
            var educationLinks = await GetArticleLinks(categoriesLinks["education"]);
            var healthLinks = await GetArticleLinks(categoriesLinks["health"]);

            var articles = new List<Dictionary<string, string>>();

            foreach (var link in observerLinks)
            {
                articles.Add(await ScrapeArticle(link, "observer"));
            }
            foreach (var link in bulgariaLinks)
            {
                articles.Add(await ScrapeArticle(link, "bulgaria"));
            }
            foreach (var link in economyLinks)
            {
                articles.Add(await ScrapeArticle(link, "economy"));
            }
            foreach (var link in foreignLinks)
            {
                articles.Add(await ScrapeArticle(link, "foreign"));
            }
            foreach (var link in cultureLinks)
            {
                articles.Add(await ScrapeArticle(link, "culture"));
            }
            foreach (var link in educationLinks)
            {
                articles.Add(await ScrapeArticle(link, "education"));
            }
            foreach (var link in healthLinks)
            {
                articles.Add(await ScrapeArticle(link, "health"));
            }

            articles.Reverse();
            return articles;
        }

        public async Task<List<string>> GetArticleLinks(string categoryLink)
        {
            var client = new HttpClient();
            var html = await client.GetAsync(categoryLink);
            var document = new HtmlDocument();
            document.LoadHtml(await html.Content.ReadAsStringAsync());

            var resultLinks = new List<string>();
            var mainClass = document.DocumentNode.SelectNodes("//div[contains(@class, 'title-wrap')]/div[contains(@class, 'title')]/a");

            for (int i = 0; i < mainClass.Count; i++)
            {
                string result = mainClass[i].GetAttributeValue("href", "");
                if (i != 0)
                {
                    result = string.Concat("https://www.segabg.com", result);
                }
                resultLinks.Add(result);
            }

            return resultLinks;
        }

        public async Task<Dictionary<string, string>> ScrapeArticle(string url, string category)
        {
            var client = new HttpClient();
            var html = await client.GetAsync(url);
            var document = new HtmlDocument();
            document.LoadHtml(await html.Content.ReadAsStringAsync());

            var result = new Dictionary<string, string>();
            var resultTitle = document.DocumentNode.SelectSingleNode("//h1[contains(@class, 'sega-title')]");
            var resultSubtitle = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'sega-under-title')]");
            var resultAuthor = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'sega-authors')]/span/a");
            var resultImageUrl = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'sega-image-wrap')]/a");
            var resultContent = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'sega-body')]");
            var resultDate = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'sega-article-date')]");

            if (resultTitle != null)
            {
                result.Add("Title", HttpUtility.HtmlDecode(resultTitle.InnerText));
            }
            else
            {
                result.Add("Title", "");
            }

            if (resultSubtitle != null)
            {
                result.Add("Subtitle", HttpUtility.HtmlDecode(resultSubtitle.InnerText));
            }
            else
            {
                result.Add("Subtitle", "");
            }

            if (resultAuthor != null)
            {
                result.Add("Author", HttpUtility.HtmlDecode(resultAuthor.InnerText));
            }
            else
            {
                result.Add("Author", "");
            }

            if (resultImageUrl != null)
            {
                result.Add("ImageUrl", resultImageUrl.GetAttributeValue("href", ""));
            }
            else
            {
                result.Add("ImageUrl", "");
            }

            if (resultContent != null)
            {
                result.Add("Content", resultContent.InnerHtml + "\nSEGABG.COM");
            }
            else
            {
                result.Add("Content", "");
            }

            if (resultDate != null)
            {
                result.Add("Date", resultDate.InnerText.Substring(0, 2));
            }
            else
            {
                result.Add("Date", "");
            }

            result.Add("Category", category);

            return result;
        }
    }
}
