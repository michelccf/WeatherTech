using WeatherConsumer.Interfaces.Services;

namespace WeatherConsumer.Factorys
{
    public class ScopedFactory : IScopedFactory
    {
        private readonly IServiceScopeFactory _serviceProvider;
        public ScopedFactory(IServiceScopeFactory serviceProvider) => _serviceProvider = serviceProvider;

        public IServiceScope CreateScope()
        {
            return _serviceProvider.CreateScope();
        }

        public T GetScopedService<T>() where T : class
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                return scope.ServiceProvider.GetService<T>();
            }
        }
    }
}
