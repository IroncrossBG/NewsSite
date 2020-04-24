namespace NewsSite.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using NewsSite.API;
    public class WeatherService : IWeatherService
    {
        static readonly HttpClient client = new HttpClient();
        static readonly OpenWeatherMap weather = new OpenWeatherMap(client);
        static readonly Dictionary<string, DictionaryWeatherModel> weatherData = new Dictionary<string, DictionaryWeatherModel>();
        public async Task<OpenWeatherMapRootObject> GetWeatherData(string cityName, string apiKey)
        {
            if (weatherData.ContainsKey(cityName))
            {
                if (DateTime.UtcNow >= weatherData[cityName].ExpiresOn)
                {
                    var rootObject = await weather.GetWeatherDataAsync(cityName, apiKey);
                    if (rootObject.cod == 200)
                    {
                        weatherData[cityName] = new DictionaryWeatherModel
                        {
                            ExpiresOn = DateTime.UtcNow.AddHours(1),
                            RootObject = rootObject
                        };
                        return rootObject;
                    }
                    return null;
                }
                return weatherData[cityName].RootObject;
            }
            else
            {
                var rootObject = await weather.GetWeatherDataAsync(cityName, apiKey);
                if (rootObject.cod == 200)
                {
                    weatherData.Add(
                        cityName,
                        new DictionaryWeatherModel
                        {
                            ExpiresOn = DateTime.UtcNow.AddHours(1),
                            RootObject = rootObject
                        });
                    return rootObject;
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
