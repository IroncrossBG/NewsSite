using NewsSite.API;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public interface IWeatherService
    {
        Task<OpenWeatherMapRootObject> GetWeatherData(string cityName, string apiKey);
    }
}
