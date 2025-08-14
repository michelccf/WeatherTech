using WeatherApi.Models.Entities;

namespace WeatherApi.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherEntity> GetWeather();
    }
}
