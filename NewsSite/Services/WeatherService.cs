using NewsSite.API;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public class WeatherService : IWeatherService
    {
        static readonly HttpClient client = new HttpClient();
        static readonly OpenWeatherMap weather = new OpenWeatherMap(client);
        public Task<OpenWeatherMapRootObject> GetWeatherData(string cityName, string apiKey)
        {
            return weather.GetWeatherData(cityName, apiKey);
        }
    }
}
