namespace NewsSite.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NewsSite.API;

    public interface IWeatherService
    {
        Task<OpenWeatherMapRootObject> GetWeatherData(string cityName, string apiKey);
    }
}
