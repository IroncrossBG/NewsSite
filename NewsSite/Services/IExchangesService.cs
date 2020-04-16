using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface IExchangesService
    {
        void Add(string url);
        List<List<string>> Get(string date);
        public bool Check(string date);
    }
}
