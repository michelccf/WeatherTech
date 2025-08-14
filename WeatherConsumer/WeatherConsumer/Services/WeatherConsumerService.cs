
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WeatherConsumer.Services
{
    public class WeatherConsumerService : BackgroundService
    {
        private readonly ILogger<WeatherConsumerService> _logger;
        private readonly ConnectionFactory _factory;

        public WeatherConsumerService(ConnectionFactory factory, ILogger<WeatherConsumerService> logger)
        {
            _factory = factory;
            _logger = logger;
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
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation($"Mensagem recebida: {message}");
            var channel = ((AsyncEventingBasicConsumer)sender).Channel;
            await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
         
        }
    }
}
