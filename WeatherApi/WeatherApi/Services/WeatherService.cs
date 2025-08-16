using System.Reflection;
using System.Text.Json.Serialization;
using WeatherApi.Interfaces;
using WeatherApi.Models.Entities;
using Newtonsoft.Json;

namespace WeatherApi.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMessageBroker _messageBroker;
        private readonly IWeatherRepository _weatherRepository;
        private HttpClient httpClient;
        private string weatherApiUrl = "https://api.open-meteo.com/v1/forecast?latitude=-23.55&longitude=-46.63&current_weather=true";

        public WeatherService(IHttpClientFactory httpClientFactory, IMessageBroker messageBroker, IWeatherRepository weatherRepository) 
        {
            _httpClientFactory = httpClientFactory;
            httpClient = _httpClientFactory.CreateClient("GenericPolly");
            _messageBroker = messageBroker;
            _weatherRepository = weatherRepository;
        }

        public async Task<WeatherEntity> GetWeather()
        {
           return await _weatherRepository.GetWather();
        }

        public async Task<WeatherEntity> ProduceWeather()
        {
            HttpResponseMessage message = await httpClient.GetAsync(weatherApiUrl);
            message.EnsureSuccessStatusCode();

            string result = await message.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(result))
                _weatherRepository.DeleteWeather();

            WeatherEntity resultObj = JsonConvert.DeserializeObject<WeatherEntity>(result);

            _messageBroker.MessageProducer(resultObj);

            return resultObj;
        }



        
    }
}
