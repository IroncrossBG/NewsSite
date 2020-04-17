using NewsSite.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface ISearchService
    {
        List<Article> Search(string input);
    }
}
