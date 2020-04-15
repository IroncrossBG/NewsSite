using NewsSite.API;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public class WeatherService : IWeatherService
    {
        static readonly HttpClient client = new HttpClient();
        static readonly OpenWeatherMap weather = new OpenWeatherMap(client);
        static readonly Dictionary<string, DictionaryWeatherModel> weatherData = new Dictionary<string, DictionaryWeatherModel>();
        public OpenWeatherMapRootObject GetWeatherData(string cityName, string apiKey)
        {
            if (weatherData.ContainsKey(cityName))
            {
                if (DateTime.Now >= weatherData[cityName].ExpiresOn)
                {
                    var rootObject = weather.GetWeatherData(cityName, apiKey);
                    if (rootObject.Result.cod == 200)
                    {
                        weatherData[cityName] = new DictionaryWeatherModel
                        {
                            ExpiresOn = DateTime.UtcNow.AddHours(1),
                            RootObject = rootObject.Result
                        };
                        return rootObject.Result;
                    }
                    return null;
                }
                return weatherData[cityName].RootObject;
            }
            else
            {
                var rootObject = weather.GetWeatherData(cityName, apiKey);
                if (rootObject.Result.cod == 200)
                {
                    weatherData.Add(cityName, new DictionaryWeatherModel
                    {
                        ExpiresOn = DateTime.UtcNow.AddHours(1),
                        RootObject = rootObject.Result
                    });
                    return rootObject.Result;
                }
                return null;
            }
        }

        public class DictionaryWeatherModel
        {
            public DateTime ExpiresOn { get; set; }

            public OpenWeatherMapRootObject RootObject { get; set; }
        }
    }
}
