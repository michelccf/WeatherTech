using Microsoft.EntityFrameworkCore;
using WeatherApi.Models.Entities;
using WeatherConsumer.Interfaces.Repositories;
using WeatherConsumer.Util;

namespace WeatherConsumer.Repositories
{
    public class WeatherReapository : IWeatherReapository
    {
        private readonly SqlDataContext _sqlDataContext;

        public WeatherReapository(SqlDataContext sqlDataContext)
        {
            _sqlDataContext = _sqlDataContext;
        }

        public async Task<WeatherEntity> GetWeather()
        {
            WeatherEntity result = _sqlDataContext.weather.FirstOrDefault();
            _sqlDataContext.weather.ExecuteDelete();
            _sqlDataContext.currentWeatherUnits.ExecuteDelete();
            _sqlDataContext.curretWeather.ExecuteDelete();
            return result;
        }

    }
}
