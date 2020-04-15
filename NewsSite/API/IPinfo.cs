using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsSite.API
{
    public class IPinfo
    {
        private readonly HttpClient client;

        public IPinfo(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IPInfoRootObject> GetIpData(string ip, string token)
        {
            try
            {
                var url = string.Concat("https://ipinfo.io/", ip, "?token=", token);
                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IPInfoRootObject>(responseContent);
                return result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }

    public class IPInfoRootObject
    {
        public string ip { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }
        public string postal { get; set; }
        public string timezone { get; set; }
    }
}
