namespace NewsSite.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NewsSite.Models.Data;

    public interface ISearchService
    {
        List<Article> Search(string input);
    }
}
