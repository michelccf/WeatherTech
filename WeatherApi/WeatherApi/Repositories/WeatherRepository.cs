using Microsoft.EntityFrameworkCore;
using Util.DbContextService;
using WeatherApi.Interfaces;
using WeatherApi.Models.Entities;

namespace WeatherApi.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly SqlDataContext _sqlDataContext;
        public WeatherRepository(SqlDataContext sqlDataContext) 
        {
            _sqlDataContext = sqlDataContext;
        }

        public async Task DeleteWeather()
        {
            _sqlDataContext.weather.ExecuteDelete();
            _sqlDataContext.currentWeatherUnits.ExecuteDelete();
            _sqlDataContext.curretWeather.ExecuteDelete();
        }

        public async Task<WeatherEntity> GetWather()
        {
            WeatherEntity result = _sqlDataContext.weather.FirstOrDefault();
            result.current_weather = _sqlDataContext.curretWeather.FirstOrDefault();
            result.current_weather_units = _sqlDataContext.currentWeatherUnits.FirstOrDefault();
            if (result == null)
                return new WeatherEntity();

            return result;
        }
    }
}
