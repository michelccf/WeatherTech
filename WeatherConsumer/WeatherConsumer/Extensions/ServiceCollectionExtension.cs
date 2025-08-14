using Microsoft.Extensions.Options;
using WeatherApi.Models.Configs;

namespace WeatherApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddRabbit(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RabbitMqSettings>(config.GetSection("RabbitMq"));

            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
                return new RabbitMQ.Client.ConnectionFactory
                {
                    HostName = settings.HostName,
                    Port = settings.Port,
                    UserName = settings.UserName,
                    Password = settings.Password
                };
            });
        }
    }
}
