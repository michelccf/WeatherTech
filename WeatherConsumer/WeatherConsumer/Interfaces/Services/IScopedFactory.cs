namespace WeatherConsumer.Interfaces.Services
{
    public interface IScopedFactory
    {
        T GetScopedService<T>() where T : class;
        IServiceScope CreateScope();
    }
}
