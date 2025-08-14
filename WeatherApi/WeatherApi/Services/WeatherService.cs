using System.Reflection;
using WeatherApi.Interfaces;
using WeatherApi.Models.Entities;

namespace WeatherApi.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public WeatherService(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherEntity> GetWeather()
        {
            return new WeatherEntity();
        }

        
    }
}
