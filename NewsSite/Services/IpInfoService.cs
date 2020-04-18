using Microsoft.AspNetCore.Http;
using NewsSite.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public class IpInfoService : IIpInfoService
    {
        static readonly HttpClient client = new HttpClient();
        static readonly IPinfo ipInfo = new IPinfo(client);
        public async Task<IPInfoRootObject> GetIpInfo(string ip, string token)
        {
            return await ipInfo.GetIpData(ip, token);
        }
    }
}
