using Microsoft.AspNetCore.Http;
using NewsSite.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface IIpInfoService
    {
        Task<IPInfoRootObject> GetIpInfo(string ip, string token);
    }
}
