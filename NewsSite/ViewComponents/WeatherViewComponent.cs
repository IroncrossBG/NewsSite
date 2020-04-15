using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IWeatherService weatherService;
        private readonly IConfiguration configuration;

        public WeatherViewComponent(IWeatherService categoryService, IConfiguration configuration)
        {
            this.weatherService = categoryService;
            this.configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var weather = weatherService.GetWeatherData("Sofia", configuration.GetSection("APIKeys").GetSection("OpenWeatherMap").Value);
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
