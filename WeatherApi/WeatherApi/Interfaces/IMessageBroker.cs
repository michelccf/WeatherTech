using WeatherApi.Models.Entities;

namespace WeatherApi.Interfaces
{
    public interface IMessageBroker
    {
        Task MessageProducer(WeatherEntity message);
    }
}
