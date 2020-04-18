using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewsSite.API;
using NewsSite.Models.View;
using NewsSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.ViewComponents
{
    [ViewComponent(Name = "Weather")]
    public class WeatherViewComponent : ViewComponent
    {
        private readonly IIpInfoService ipInfoService;
        private readonly IWeatherService weatherService;
        private readonly IConfiguration configuration;

        public WeatherViewComponent(IIpInfoService ipInfoService, IWeatherService categoryService, IConfiguration configuration)
        {
            this.ipInfoService = ipInfoService;
            this.weatherService = categoryService;
            this.configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            var weather = new OpenWeatherMapRootObject();

            if (ip == "::1" || ip.StartsWith("192"))
            {
                weather = weatherService.GetWeatherData("Sofia", configuration.GetSection("APIKeys").GetSection("OpenWeatherMap").Value);
            }
            else
            {
                var ipInfo = ipInfoService.GetIpInfo(ip, configuration.GetSection("APIKeys").GetSection("IpInfo").Value);
                weather = weatherService.GetWeatherData(ipInfo.Result.city, configuration.GetSection("APIKeys").GetSection("OpenWeatherMap").Value);
            }

            var result = weather;
            var resultModel = new WeatherViewModel()
            {
                City = result.name,
                Weather = result.weather.FirstOrDefault(),
                Temperature = Math.Round(result.main.temp),
                MinTemperature = Math.Round(result.main.temp_min),
                MaxTemperature = Math.Round(result.main.temp_max),
                FeelsLikeTemperature = Math.Round(result.main.feels_like),
                Pressure = result.main.pressure,
                Humidity = result.main.humidity,
                WindSpeed = Math.Round(result.wind.speed * 3.6),
            };
            resultModel.IconUrl = string.Concat("https://openweathermap.org/img/wn/", resultModel.Weather.icon, "@2x.png");

            return View(resultModel);
        }
    }
}
