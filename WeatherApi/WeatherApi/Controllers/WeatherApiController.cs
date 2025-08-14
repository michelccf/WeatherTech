using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using WeatherApi.Interfaces;
using WeatherApi.Models.Entities;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherApiController : ControllerBase
    {

        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherApiController> _logger;

        public WeatherApiController(ILogger<WeatherApiController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<WeatherEntity>> Get()
        {
            return Ok(_weatherService.GetWeather());
        }
    }
}
