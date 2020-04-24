namespace NewsSite.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using NewsSite.API;

    public interface IIpInfoService
    {
        Task<IPInfoRootObject> GetIpInfoAsync(string ip, string token);
    }
}
