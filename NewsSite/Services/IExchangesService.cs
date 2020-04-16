using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface IExchangesService
    {
        List<List<string>> GetExchanges(string url);
    }
}
