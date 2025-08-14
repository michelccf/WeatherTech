using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using WeatherApi.Models.Configs;
using WeatherApi.Models.Entities;
using Newtonsoft.Json;
using WeatherApi.Interfaces;

namespace WeatherApi.Services
{
    public class MessageBroker : IMessageBroker
    {
        private ConnectionFactory _connectionFactory;

        private readonly string _queueName;
        public MessageBroker(ConnectionFactory connectionFactory, IOptions<RabbitMqSettings> options) 
        {
            _connectionFactory = connectionFactory;
            _queueName = options.Value.QueueName;
        }
        public async Task MessageProducer(WeatherEntity message)
        {
            using IConnection connection = await _connectionFactory.CreateConnectionAsync();
            using IChannel channel = await connection.CreateChannelAsync();
            
            await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            // Publica a mensagem
             await channel.BasicPublishAsync(
                "",
                _queueName,
                false,
                basicProperties: new BasicProperties { Persistent = true },
                body
            );
        }
    }
}
