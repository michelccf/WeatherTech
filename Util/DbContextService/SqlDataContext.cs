
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using WeatherApi.Models.Entities;


namespace Util.DbContextService
{
    public class SqlDataContext : DbContext
    {
        public DbSet<WeatherEntity> weather { get; set; }
        public DbSet<CurrentWeatherUnits> currentWeatherUnits { get; set; }
        public DbSet<CurrentWeather> curretWeather { get; set; }
        public SqlDataContext (DbContextOptions<SqlDataContext> options) : base(options) { }

    }
}
