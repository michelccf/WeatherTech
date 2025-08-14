using Polly;

namespace WeatherApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddPolly(this IServiceCollection services)
        {
            services.AddHttpClient("GenericPolly").AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: attempt => TimeSpan
            .FromSeconds(Math.Pow(2,attempt)),
            onRetry: (outcome, delay, attempt, context) =>
            {
                Console.WriteLine($"Tentativa {attempt} falhou. Retentando em {delay.TotalSeconds}s...");
            })).AddTransientHttpErrorPolicy(policy =>
                    policy.CircuitBreakerAsync(
                        handledEventsAllowedBeforeBreaking: 2,
                        durationOfBreak: TimeSpan.FromSeconds(30)
            ));
        }
    }
}
