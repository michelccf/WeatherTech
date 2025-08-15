
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WeatherApi.Models.Entities;
using WeatherConsumer.Factorys;
using WeatherConsumer.Interfaces.Services;
using WeatherConsumer.Util;

namespace WeatherConsumer.Services
{
    public class WeatherConsumerService : BackgroundService
    {
        private readonly ILogger<WeatherConsumerService> _logger;
        private readonly ConnectionFactory _factory;
        private readonly IScopedFactory _scopedFactory;

        public WeatherConsumerService(ConnectionFactory factory, ILogger<WeatherConsumerService> logger, IScopedFactory scopedFactory)
        {
            _factory = factory;
            _logger = logger;
            _scopedFactory = scopedFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await using var connection = await _factory.CreateConnectionAsync();
                await using var channel = await connection.CreateChannelAsync();

                var consumer = new AsyncEventingBasicConsumer(channel);

                await channel.QueueDeclareAsync(
                    queue: "weather_queue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                await channel.BasicConsumeAsync(
                queue: "weather_queue",
                autoAck: false,
                consumer: consumer
                );
               
                    consumer.ReceivedAsync += OnMessageReceivedAsync;

                #if DEBUG
                while (true)
                {
                    Thread.Sleep(10);
                }
                #endif

                #if !DEBUG
                Task.Delay(Timeout.Infinite, stoppingToken);
                #endif
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs ea)
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                WeatherEntity x = JsonConvert.DeserializeObject<WeatherEntity>(message);

                using IServiceScope scope = _scopedFactory.CreateScope();

                scope.ServiceProvider.GetService<SqlDataContext>().Add(x);

                await scope.ServiceProvider.GetService<SqlDataContext>().SaveChangesAsync();


                _logger.LogInformation($"Mensagem recebida: {message}");
                var channel = ((AsyncEventingBasicConsumer)sender).Channel;
                await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
            catch(Exception ex)
            {}
         
        }
    }
}
