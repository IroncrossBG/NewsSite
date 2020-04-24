namespace NewsSite.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using NewsSite.API;

    public class IpInfoService : IIpInfoService
    {
        static readonly HttpClient client = new HttpClient();
        static readonly IPinfo ipInfo = new IPinfo(client);

        public async Task<IPInfoRootObject> GetIpInfoAsync(string ip, string token)
        {
            return await ipInfo.GetIpDataAsync(ip, token);
        }
    }
}
