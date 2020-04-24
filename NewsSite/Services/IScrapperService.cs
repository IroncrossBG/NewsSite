namespace NewsSite.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IScrapperService
    {
        Task RunSegaScrapper(DateTime from, DateTime to);
    }
}
