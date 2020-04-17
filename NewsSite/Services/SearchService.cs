using NewsSite.Data;
using NewsSite.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NewsSite.Services
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext db;

        public SearchService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public List<Article> Search(string input)
        {
            var searchQuery = input.Split(' ').Select(x => x.ToLower()).ToList();

            List<Article> articlesResult = db.Articles
                .AsEnumerable()
                .Where(a => searchQuery.Any(x => a.Title.ToLower().Contains(x)) || searchQuery.Any(y => a.Content.ToLower().Contains(y)))
                .ToList();

            return articlesResult;
        }
    }
}
