using WeatherApi.Models.Entities;

namespace WeatherConsumer.Interfaces.Repositories
{
    public interface IWeatherReapository
    {
        Task<WeatherEntity> GetWeather();
    }
}
