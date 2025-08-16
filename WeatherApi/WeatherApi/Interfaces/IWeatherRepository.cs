using WeatherApi.Models.Entities;

namespace WeatherApi.Interfaces
{
    public interface IWeatherRepository
    {
        Task<WeatherEntity> GetWather();
        Task DeleteWeather();
    }
}
