using NewsSite.API;

namespace NewsSite.Models.View
{
    public class WeatherViewModel
    {
        public string City { get; set; }
        public Weather Weather { get; set; }
        public double Temperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double FeelsLikeTemperature { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string IconUrl { get; set; }
    }
}
