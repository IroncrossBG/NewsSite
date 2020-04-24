namespace NewsSite.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IExchangesService
    {
        Task Add(string url);

        List<List<string>> Get(string date);

        bool Check(string date);
    }
}
