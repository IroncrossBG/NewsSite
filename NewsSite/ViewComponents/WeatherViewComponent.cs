using Microsoft.AspNetCore.Mvc;
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

        public WeatherViewComponent(IWeatherService categoryService)
        {
            this.weatherService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var weather = weatherService.GetWeatherData("Sofia", "");
            var result = weather.Result;
            var resultModel = new WeatherViewModel()
            {
                Condition = result.weather.FirstOrDefault().main,
                Temperature = result.main.temp,
                MinTemperature = result.main.temp_min,
                MaxTemperature = result.main.temp_max,
                FeelsLikeTemperature = result.main.feels_like,
                Pressure = result.main.pressure,
                Humidity = result.main.humidity,
                WindSpeed = result.wind.speed
            };

            return View(resultModel);
        }
    }
}
